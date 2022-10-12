using GB_API.Server.Data;
using GB_API.Server.Domain;
using Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace GB_API;

public static class DataSeeder
{
    
    public static void Seed(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<MICDbContext>();
        context.Database.EnsureCreated();
        SetUpExcelWorkSheet(context);
    }
    
    private static void SetUpExcelWorkSheet(MICDbContext context)
    {
        var xlApp = new Application();
        var xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\Dylla\RiderProjects\GB-API\GB-API\LMC-Data\LMC10.0.1.xlsx");
        _Worksheet xlWorksheet = xlWorkbook.Sheets[3] as Worksheet ?? throw new InvalidOperationException();
        LoadAllKarakteristieken(context, xlWorksheet);
        LoadAllMeldingsclassificaties(context, xlWorksheet);
    }

    private static void LoadAllMeldingsclassificaties(MICDbContext context, _Worksheet xlWorksheet)
    {
        if (context.MeldingsClassificaties.FirstOrDefault() != null) return;
        var meldingList = new List<MeldingsClassificaties>();
        var xlRange = xlWorksheet.UsedRange;
        var rowCount = xlRange.Rows.Count;
        for (var i = 2; i <= rowCount; i++)
        {
            var niveau1 = ((xlRange.Cells[i, 1] as Range)?.Value ?? "").ToString();
            var niveau2 = ((xlRange.Cells[i, 2] as Range)?.Value ?? "").ToString();
            var niveau3 = ((xlRange.Cells[i, 3] as Range)?.Value ?? "").ToString();
            var afkorting = ((xlRange.Cells[i, 7] as Range)?.Value ?? "").ToString();
            var presentatieTekst = ((xlRange.Cells[i, 8] as Range)?.Value ?? "").ToString();
            var definitie = ((xlRange.Cells[i, 12] as Range)?.Value ?? "").ToString();
            meldingList.Add(new MeldingsClassificaties(niveau1,niveau2,niveau3, afkorting, presentatieTekst, definitie));
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
            var volgString = (xlRange.Cells[i, 3] as Range).Value ?? "0";
            int volgNr;
            if (!int.TryParse(volgString.ToString(), out volgNr)) volgNr = 0;
            var naam = (xlRange.Cells[i, 1] as Range).Value.ToString();
            var type = ((xlRange.Cells[i, 2] as Range).Value ?? "").ToString();
            var waarde = ((xlRange.Cells[i, 4] as Range).Value ?? "").ToString();
            karakteristiekList.Add(new Karakteristiek(naam,type, volgNr, waarde));
        }
        context.Karakteristieks.AddRange(karakteristiekList);
        context.SaveChanges();
    }
}