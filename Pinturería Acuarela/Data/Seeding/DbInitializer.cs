using Microsoft.EntityFrameworkCore;
using Pinturería_Acuarela.Models;

namespace Pinturería_Acuarela.Data.Seeding
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<Brand>().HasData(
                new Brand() { ID = 1, Name = "Tersuave" },
                new Brand() { ID = 2, Name = "Alba" },
                new Brand() { ID = 3, Name = "Black Decker" },
                new Brand() { ID = 4, Name = "Colorin" }
            );
            
            modelBuilder.Entity<Business>().HasData(
                new Business() { ID = 1, Address = "San Carlos Centro" },
                new Business() { ID = 2, Address = "San Jerónimo Norte" },
                new Business() { ID = 3, Address = "Franck" },
                new Business() { ID = 4, Address = "Depósito" }
            );

            modelBuilder.Entity<Capacity>().HasData(
                new Capacity() { ID = 1, Volume = 1, Description = "1 Litro" },
                new Capacity() { ID = 2, Volume = 0.75f, Description = "3/4 Litro" },
                new Capacity() { ID = 3, Volume = 0.5f, Description = "1/2 Litro" },
                new Capacity() { ID = 4, Volume = 0.25f, Description = "1/4 Litro" },
                new Capacity() { ID = 5, Volume = 20, Description = "20 Litros" },
                new Capacity() { ID = 6, Volume = 4, Description = "4 Litros" },
                new Capacity() { ID = 7, Volume = 10, Description = "10 Litros" },
                new Capacity() { ID = 8, Volume = 16, Description = "16 Litros" },
                new Capacity() { ID = 9, Volume = 32, Description = "32 Litros" },
                new Capacity() { ID = 10, Volume = 0.44f, Description = "440 cc" },
                new Capacity() { ID = 11, Volume = 0.25f, Description = "250 cc" },
                new Capacity() { ID = 12, Volume = 0.12f, Description = "120 cc" },
                new Capacity() { ID = 13, Volume = 0.03f, Description = "30 cc" },
                new Capacity() { ID = 14, Volume = 2, Description = "2 Litros" },
                new Capacity() { ID = 15, Volume = 2.5f, Description = "2.5 Litros" },
                new Capacity() { ID = 16, Volume = 24, Description = "24 Litros" },
                new Capacity() { ID = 17, Volume = 25, Description = "25 Litros" },
                new Capacity() { ID = 18, Volume = 5, Description = "5 Litros" },
                new Capacity() { ID = 19, Volume = 6, Description = "6 Litros" },
                new Capacity() { ID = 20, Volume = 30, Description = "30 Litros" },
                new Capacity() { ID = 21, Volume = 36, Description = "36 Litros" },
                new Capacity() { ID = 22, Volume = 40, Description = "40 Litros" },
                new Capacity() { ID = 23, Volume = 0.06f, Description = "60 cc" },
                new Capacity() { ID = 24, Volume = 0.3f, Description = "300 gramos" },
                new Capacity() { ID = 25, Volume = 0.6f, Description = "600 gramos" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category() { ID = 1, Description = "Linea Arquitectónica" },
                new Category() { ID = 2, Description = "Linea Industria" },
                new Category() { ID = 3, Description = "Linea Automotor" },
                new Category() { ID = 4, Description = "Linea Diluyentes" },
                new Category() { ID = 5, Description = "Linea Autopolish" }
            );

            modelBuilder.Entity<Subcategory>().HasData(
                new Subcategory() { ID = 1, Description = "Sintéticos" },
                new Subcategory() { ID = 2, Description = "Aerosoles" },
                new Subcategory() { ID = 3, Description = "Maderas" },
                new Subcategory() { ID = 4, Description = "Látex" },
                new Subcategory() { ID = 5, Description = "Impermeabilizantes" },
                new Subcategory() { ID = 6, Description = "Especiales" },
                new Subcategory() { ID = 7, Description = "Accesorios Látex" },
                new Subcategory() { ID = 8, Description = "Accesorios" },
                new Subcategory() { ID = 9, Description = "Selladores" },
                new Subcategory() { ID = 10, Description = "Hay color  - Sistema tintométrico" },
                new Subcategory() { ID = 11, Description = "Pintura en polvo" },
                new Subcategory() { ID = 12, Description = "Terplast revoques plásticos" },
                new Subcategory() { ID = 13, Description = "Xtreme" }
            );
        }
    }

}
