﻿using APP2024P4.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Data.Context
{
    public interface IAppDbContext
    {
        DbSet<Categoria> Categorias { get; set; }
        DbSet<Marca> Marcas { get; set; }
        DbSet<Modelo> Modelos { get; set; }
        DbSet<Producto> Productos { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}