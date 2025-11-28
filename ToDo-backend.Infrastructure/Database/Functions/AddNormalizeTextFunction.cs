namespace ToDo_backend.Infrastructure.Database.Functions;

using Microsoft.EntityFrameworkCore.Migrations;

public partial class AddNormalizeTextFunction : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"
            CREATE OR REPLACE FUNCTION remove_diacritics(input TEXT)
            RETURNS TEXT AS $$
            BEGIN
                RETURN translate(
                    input,
                    'ÁÉÍÓÚáéíóúÑñÜü',
                    'AEIOUaeiouNnUu'
                );
            END;
            $$ LANGUAGE plpgsql IMMUTABLE;
        ");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("DROP FUNCTION IF EXISTS remove_diacritics(text);");
    }
}
