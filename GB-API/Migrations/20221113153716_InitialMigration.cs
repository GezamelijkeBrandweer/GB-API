using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GBAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MIC-DB");

            migrationBuilder.CreateTable(
                name: "dienst",
                schema: "MIC-DB",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dienst", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "karakteristiek",
                schema: "MIC-DB",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Naam = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Waarde = table.Column<string>(type: "text", nullable: false),
                    VolgNr = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_karakteristiek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "locatie",
                schema: "MIC-DB",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Straat = table.Column<string>(type: "text", nullable: false),
                    Postcode = table.Column<string>(type: "text", nullable: false),
                    Huisnummer = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longtitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locatie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "meldingClassificatie",
                schema: "MIC-DB",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Niveau1 = table.Column<string>(type: "text", nullable: false),
                    Niveau2 = table.Column<string>(type: "text", nullable: false),
                    Niveau3 = table.Column<string>(type: "text", nullable: false),
                    Afkorting = table.Column<string>(type: "text", nullable: false),
                    Definitie = table.Column<string>(type: "text", nullable: false),
                    PresentatieTekst = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meldingClassificatie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kIntensiteit",
                schema: "MIC-DB",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Punten = table.Column<int>(type: "integer", nullable: false),
                    DienstId = table.Column<long>(type: "bigint", nullable: false),
                    KarakteristiekId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kIntensiteit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kIntensiteit_dienst_DienstId",
                        column: x => x.DienstId,
                        principalSchema: "MIC-DB",
                        principalTable: "dienst",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kIntensiteit_karakteristiek_KarakteristiekId",
                        column: x => x.KarakteristiekId,
                        principalSchema: "MIC-DB",
                        principalTable: "karakteristiek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "incident",
                schema: "MIC-DB",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LocatieId = table.Column<long>(type: "bigint", nullable: false),
                    MeldingsclassificatieId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_incident", x => x.Id);
                    table.ForeignKey(
                        name: "FK_incident_locatie_LocatieId",
                        column: x => x.LocatieId,
                        principalSchema: "MIC-DB",
                        principalTable: "locatie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_incident_meldingClassificatie_MeldingsclassificatieId",
                        column: x => x.MeldingsclassificatieId,
                        principalSchema: "MIC-DB",
                        principalTable: "meldingClassificatie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mIntensiteit",
                schema: "MIC-DB",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Punten = table.Column<int>(type: "integer", nullable: false),
                    DienstId = table.Column<long>(type: "bigint", nullable: false),
                    MeldingsclassificatieId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mIntensiteit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mIntensiteit_dienst_DienstId",
                        column: x => x.DienstId,
                        principalSchema: "MIC-DB",
                        principalTable: "dienst",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mIntensiteit_meldingClassificatie_MeldingsclassificatieId",
                        column: x => x.MeldingsclassificatieId,
                        principalSchema: "MIC-DB",
                        principalTable: "meldingClassificatie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "intensiteit",
                schema: "MIC-DB",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DienstId = table.Column<long>(type: "bigint", nullable: false),
                    IncidentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_intensiteit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_intensiteit_dienst_DienstId",
                        column: x => x.DienstId,
                        principalSchema: "MIC-DB",
                        principalTable: "dienst",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_intensiteit_incident_IncidentId",
                        column: x => x.IncidentId,
                        principalSchema: "MIC-DB",
                        principalTable: "incident",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Karakteristiek_Incident",
                schema: "MIC-DB",
                columns: table => new
                {
                    KarakteristiekenId = table.Column<long>(type: "bigint", nullable: false),
                    incidentsId = table.Column<long>(name: "_incidentsId", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karakteristiek_Incident", x => new { x.KarakteristiekenId, x.incidentsId });
                    table.ForeignKey(
                        name: "FK_Karakteristiek_Incident_incident__incidentsId",
                        column: x => x.incidentsId,
                        principalSchema: "MIC-DB",
                        principalTable: "incident",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Karakteristiek_Incident_karakteristiek_KarakteristiekenId",
                        column: x => x.KarakteristiekenId,
                        principalSchema: "MIC-DB",
                        principalTable: "karakteristiek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_incident_LocatieId",
                schema: "MIC-DB",
                table: "incident",
                column: "LocatieId");

            migrationBuilder.CreateIndex(
                name: "IX_incident_MeldingsclassificatieId",
                schema: "MIC-DB",
                table: "incident",
                column: "MeldingsclassificatieId");

            migrationBuilder.CreateIndex(
                name: "IX_intensiteit_DienstId",
                schema: "MIC-DB",
                table: "intensiteit",
                column: "DienstId");

            migrationBuilder.CreateIndex(
                name: "IX_intensiteit_IncidentId",
                schema: "MIC-DB",
                table: "intensiteit",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Karakteristiek_Incident__incidentsId",
                schema: "MIC-DB",
                table: "Karakteristiek_Incident",
                column: "_incidentsId");

            migrationBuilder.CreateIndex(
                name: "IX_kIntensiteit_DienstId",
                schema: "MIC-DB",
                table: "kIntensiteit",
                column: "DienstId");

            migrationBuilder.CreateIndex(
                name: "IX_kIntensiteit_KarakteristiekId",
                schema: "MIC-DB",
                table: "kIntensiteit",
                column: "KarakteristiekId");

            migrationBuilder.CreateIndex(
                name: "IX_mIntensiteit_DienstId",
                schema: "MIC-DB",
                table: "mIntensiteit",
                column: "DienstId");

            migrationBuilder.CreateIndex(
                name: "IX_mIntensiteit_MeldingsclassificatieId",
                schema: "MIC-DB",
                table: "mIntensiteit",
                column: "MeldingsclassificatieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "intensiteit",
                schema: "MIC-DB");

            migrationBuilder.DropTable(
                name: "Karakteristiek_Incident",
                schema: "MIC-DB");

            migrationBuilder.DropTable(
                name: "kIntensiteit",
                schema: "MIC-DB");

            migrationBuilder.DropTable(
                name: "mIntensiteit",
                schema: "MIC-DB");

            migrationBuilder.DropTable(
                name: "incident",
                schema: "MIC-DB");

            migrationBuilder.DropTable(
                name: "karakteristiek",
                schema: "MIC-DB");

            migrationBuilder.DropTable(
                name: "dienst",
                schema: "MIC-DB");

            migrationBuilder.DropTable(
                name: "locatie",
                schema: "MIC-DB");

            migrationBuilder.DropTable(
                name: "meldingClassificatie",
                schema: "MIC-DB");
        }
    }
}
