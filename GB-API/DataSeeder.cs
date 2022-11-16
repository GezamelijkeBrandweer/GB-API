using GB_API.Server.Data;
using GB_API.Server.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace GB_API;

public static class DataSeeder
{
    public static void Seed(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<MICDbContext>();

        EmptyTables(context);
        
        context.Database.EnsureCreated();
        
        SetUpDiensten(context);
        SetUpExcelWorkSheet(context);
        SetUpKarakteristiekIntensiteiten(context);
        SetUpMeldingIntensiteiten(context);
    }
    
    private static void SetUpExcelWorkSheet(MICDbContext context)
    {
        var xlApp = new Application();
        var fullPath= Path.GetFullPath("./LMC-Data/LMC10.0.1.xlsx");
        var xlWorkbook = xlApp.Workbooks.Open(fullPath);
        _Worksheet xlWorksheetMelding = xlWorkbook.Sheets[3] as Worksheet ?? throw new InvalidOperationException();
        _Worksheet xlWorksheetKarakteristieken = xlWorkbook.Sheets[4] as Worksheet ?? throw new InvalidOperationException();
        LoadAllMeldingsclassificaties(context, xlWorksheetMelding);
        LoadAllKarakteristieken(context, xlWorksheetKarakteristieken);
        xlApp.Workbooks.Close();
    }

    private static void LoadAllMeldingsclassificaties(MICDbContext context, _Worksheet xlWorksheet)
    {
        if (context.MeldingsClassificaties.FirstOrDefault() != null) return;
        var meldingList = new List<Meldingsclassificatie>();
        var xlRange = xlWorksheet.UsedRange;
        var rowCount = xlRange.Rows.Count;
        for (var i = 2; i <= rowCount; i++)
        {
            var niveau1 = ((xlRange.Cells[i, 1] as Range)?.Value ?? "").ToString()!;
            var niveau2 = ((xlRange.Cells[i, 2] as Range)?.Value ?? "").ToString()!;
            var niveau3 = ((xlRange.Cells[i, 3] as Range)?.Value ?? "").ToString()!;
            var afkorting = ((xlRange.Cells[i, 7] as Range)?.Value ?? "").ToString()!;
            var presentatieTekst = ((xlRange.Cells[i, 8] as Range)?.Value ?? "").ToString()!;
            var definitie = ((xlRange.Cells[i, 12] as Range)?.Value ?? "").ToString()!;
            meldingList.Add(new Meldingsclassificatie(niveau1,niveau2,niveau3, afkorting, presentatieTekst, definitie));
        }
        context.MeldingsClassificaties.AddRange(meldingList);
        context.SaveChanges();
        
    }
    private static void LoadAllKarakteristieken(MICDbContext context, _Worksheet xlWorksheet)
    {
        var karakteristiek = context.Karakteristieks.FirstOrDefault();
        if (karakteristiek != null) return;
        var karakteristiekList = new List<Karakteristiek>();
        var xlRange = xlWorksheet.UsedRange;
        var rowCount = xlRange.Rows.Count;
        for (var i = 2; i <= rowCount; i++)
        {
            var volgString = (xlRange.Cells[i, 3] as Range)!.Value ?? "0";
            int volgNr;
            if (!int.TryParse(volgString.ToString(), out volgNr)) volgNr = 0;
            var naam = (xlRange.Cells[i, 1] as Range).Value.ToString();
            var type = ((xlRange.Cells[i, 2] as Range).Value ?? "").ToString();
            var waarde = ((xlRange.Cells[i, 4] as Range).Value ?? "").ToString();
            karakteristiekList.Add(new Karakteristiek(naam,type, waarde, volgNr));
        }
        context.Karakteristieks.AddRange(karakteristiekList);
        context.SaveChanges();
    }
    private static void SetUpDiensten(MICDbContext context)
    {
        var dienstName = new List<string>()
        {
            "Brandweer", "Politie", "Ambulance", "Marechaussee", "Waterpolitie", "Handhaving"
        };
        var ietsjes = dienstName.ConvertAll(s => new Dienst(s));
        context.Diensten.AddRange(ietsjes);
        context.SaveChanges();
    }

    private static void SetUpKarakteristiekIntensiteiten(MICDbContext context)
    {
        var random = new Random();
        var karakteristieken = context.Karakteristieks.ToList();
        var diensten = context.Diensten.ToList();
        var KIntensiteiten = 
            from karakteristiek in karakteristieken
            from dienst in diensten
            select new KarakteristiekIntensiteit(random.Next(0, 16), dienst, karakteristiek);
        context.KarakteristiekIntensiteiten.AddRange(KIntensiteiten);
        context.SaveChanges();
    }

    private static void SetUpMeldingIntensiteiten(MICDbContext context)
    {
        var random = new Random();
        var meldingen = context.MeldingsClassificaties.ToList();
        var diensten = context.Diensten.ToList();
        var mIntensiteiten = 
            from dienst in diensten
            from melding in meldingen
            select new MeldingsclassificatieIntensiteit(random.Next(0, 51), dienst, melding);
        context.MeldingIntensiteiten.AddRange(mIntensiteiten);
        context.SaveChanges();
    }

    private static void EmptyTables(DbContext context)
    {
        const string databaseSchema = "MIC-DB";
        var tableNames = context.Model.GetEntityTypes()
            .Select(t => t.GetTableName())
            .Distinct()
            .ToList();
        tableNames.ForEach(tableName => context.Database.ExecuteSqlRaw($"TRUNCATE TABLE \"{databaseSchema}\".\"{tableName}\" RESTART IDENTITY CASCADE"));
    }
}