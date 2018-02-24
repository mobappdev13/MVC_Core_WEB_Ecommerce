namespace Ecommerce01.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Ecommerce01.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Ecommerce01.Models.Ecommerce01Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Ecommerce01.Models.Ecommerce01Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Departaments.AddOrUpdate(d => d.DepartamentId,
                            new Departament()
                            {
                                DepartamentId = 1,
                                Name = "Friuli-Venezia Giulia",
                                Latitud = 45.636111m,
                                Longitud = 13.804167m
                            },
                             new Departament()
                             {
                                 DepartamentId = 2,
                                 Name = "Veneto",
                                 Latitud = 45.439722m,
                                 Longitud = 12.331944m
                             },
                             new Departament()
                             {
                                 DepartamentId = 3,
                                 Name = "Toscana",
                                 Latitud = 43.771389m,
                                 Longitud = 11.254167m
                             }
                            );

            //
            context.Provinces.AddOrUpdate(p => p.ProvinceId,
                new Province()
                {
                    ProvinceId = 1,
                    Name = "Udine",
                    SigCap = "33100",
                    TwoInitial = "UD",
                    Latitud = 46.140797m,
                    Longitud = 13.16629m,
                    DepartamentId = 1
                },
                new Province()
                {
                    ProvinceId = 2,
                    Name = "Pordenone",
                    SigCap = "33170",
                    TwoInitial = "PN",
                    Latitud = 46.037886m,
                    Longitud = 12.710835m,
                    DepartamentId = 1
                },
                new Province()
                {
                    ProvinceId = 3,
                    Name = "Trieste",
                    SigCap = "34100",
                    TwoInitial = "TS",
                    Latitud = 45.689482m,
                    Longitud = 13.783307m,
                    DepartamentId = 1
                },
                new Province()
                {
                    ProvinceId = 4,
                    Name = "Gorizia",
                    SigCap = "34170",
                    TwoInitial = "GO",
                    Latitud = 45.90539m,
                    Longitud = 13.516373m,
                    DepartamentId = 1
                }
                );

            context.Cities.AddOrUpdate(p => p.CityId,
                new City()
                {
                    CityId = 1,
                    Name = "Palmanova",
                    SigCap = "33057",
                    DepartamentId = 1,
                    ProvinceId = 1,
                    Latitud = 45.909424m,
                    Longitud = 13.305729m
                },
                new City()
                {
                    CityId = 2,
                    Name = "Sacile",
                    SigCap = "33077",
                    DepartamentId = 1,
                    ProvinceId = 2,
                    Latitud = 45.954053m,
                    Longitud = 12.508961m
                },
                new City()
                {
                    CityId = 3,
                    Name = "Muggia",
                    SigCap = "34015",
                    DepartamentId = 1,
                    ProvinceId = 3,
                    Latitud = 45.603154m,
                    Longitud = 13.766797m

                },
                new City()
                {
                    CityId = 4,
                    Name = "Cormons",
                    SigCap = "34071",
                    DepartamentId = 1,
                    ProvinceId = 4,
                    Latitud = 45.95531m,
                    Longitud = 13.46683m

                }
                );

        }
    }
}

