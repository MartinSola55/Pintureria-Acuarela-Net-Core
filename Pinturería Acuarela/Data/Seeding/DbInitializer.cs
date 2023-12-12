using Microsoft.EntityFrameworkCore;
using Pintureria_Acuarela.Models;

namespace Pintureria_Acuarela.Data.Seeding
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
                new Brand() { ID = 2, Name = "Sherwin Williams" },
                new Brand() { ID = 3, Name = "Venier" },
                new Brand() { ID = 4, Name = "Megaflex" },
                new Brand() { ID = 5, Name = "Quimex" },
                new Brand() { ID = 6, Name = "Colorin" },
                new Brand() { ID = 7, Name = "Plavicon" },
                new Brand() { ID = 8, Name = "Sinteplast" },
                new Brand() { ID = 9, Name = "Colvinil" },
                new Brand() { ID = 10, Name = "Petrilac" },
                new Brand() { ID = 11, Name = "Magiplast" },
                new Brand() { ID = 12, Name = "Varios" },
                new Brand() { ID = 13, Name = "El Galgo" },
                new Brand() { ID = 14, Name = "Poxipol" },
                new Brand() { ID = 15, Name = "Siloc" },
                new Brand() { ID = 16, Name = "Pinciroli" },
                new Brand() { ID = 17, Name = "Espatulas" },
                new Brand() { ID = 18, Name = "Abrasivos" },
                new Brand() { ID = 19, Name = "JMG" },
                new Brand() { ID = 20, Name = "Trimas" },
                new Brand() { ID = 21, Name = "Ferrobet" },
                new Brand() { ID = 22, Name = "Cautisol" },
                new Brand() { ID = 23, Name = "Mangueras" },
                new Brand() { ID = 24, Name = "Zeocar" },
                new Brand() { ID = 25, Name = "Dowen Pagio" },
                new Brand() { ID = 26, Name = "Bosch" },
                new Brand() { ID = 27, Name = "Daihatsu" },
                new Brand() { ID = 28, Name = "Makita" }
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
