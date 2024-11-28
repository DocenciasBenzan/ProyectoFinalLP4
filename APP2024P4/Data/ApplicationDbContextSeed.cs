using APP2024P4.Data.dbcontext;
using APP2024P4.Data.Entities;

namespace APP2024P4.Data;

public static class ApplicationDbContextSeed
{
    public static void Run(DatabaseApp context)
    {
        if (!context.Categorias.Any())
        {
            var categorias = new List<Categoria>()
            {
                new(){ Nombre = "No definida"},//1
                new(){ Nombre = "Alimentos"}   //2
            };
            context.Categorias.AddRange(categorias);
            context.SaveChanges();
        }

    }
}