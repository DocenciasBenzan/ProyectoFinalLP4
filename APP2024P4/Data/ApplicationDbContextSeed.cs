using APP2024P4.Data.Entities;

namespace APP2024P4.Data
{
    public static class ApplicationDbContextSeed
    {
        public static void Run(ApplicationDbContext context)
        {
            if (!context.Categorias.Any())
            {
                var categorias = new List<Categoria>()
                {
                    new(){ Nombre = "No definida" }, //1
                    new(){ Nombre = "Storage" }, //2
                    new(){ Nombre = "CPU" }, //3
                    new(){ Nombre = "GPU" }, //4
                    new(){ Nombre = "RAM" }, //5
                };
                context.Categorias.AddRange(categorias);
                context.SaveChanges();
            }
        }
    }
}
