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
                new Capacity() { ID = 2, Volume = 3/4, Description = "3/4 Litro" },
                new Capacity() { ID = 3, Volume = 1/2, Description = "1/2 Litro" },
                new Capacity() { ID = 4, Volume = 1/4, Description = "1/4 Litro" },
                new Capacity() { ID = 5, Volume = 20, Description = "20 Litros" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category() { ID = 1, Description = "Linea Arquitectónica" },
                new Category() { ID = 2, Description = "Linea Industria" },
                new Category() { ID = 3, Description = "Linea Automotor" },
                new Category() { ID = 4, Description = "Linea Diluyentes" },
                new Category() { ID = 5, Description = "Linea Autopolish" }
            );

            //modelBuilder.Entity<Color>().HasData(
            //    new Color() { ID = 1, Name = "Rojo", Hex = "#ff0000" },
            //    new Color() { ID = 2, Name = "Negro", Hex = "#000000" },
            //    new Color() { ID = 3, Name = "Verde", Hex = "#00ff00" },
            //    new Color() { ID = 3, Name = "Amarillo", Hex = "#eeff00" }
            //);

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
