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
            ContextKey = "Ecommerce01.Models.Ecommerce01Context";
        }

        protected override void Seed(Ecommerce01.Models.Ecommerce01Context context)
        {
            //  This method will be called after migrating to the latest version.
            //1
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

            //Provinces 2
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
                },
                 new Province()
                 {
                     ProvinceId = 5,
                     Name = "Pisa",
                     SigCap = "56100",
                     TwoInitial = "PI",
                     Latitud = 43.7161350m,
                     Longitud = 10.39660243m,
                     DepartamentId = 3

                 }
                );

            //  Cities 3
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

                },
                new City()
                {
                    CityId = 5,
                    Name = "Volterra",
                    SigCap = "56048",
                    DepartamentId = 3,
                    ProvinceId = 5,
                    Latitud = 43.4014259m,
                    Longitud = 10.8611111m

                }
                );

            //  Companies 4
            //attention ~/Content/Logos/1.jpg
            context.Companies.AddOrUpdate(c => c.CompanyId,
              new Company()
              {
                  CompanyId = 1,
                  Name = "Scarpe Friulane DEFAULT",
                  Phone = "0432127654",
                  AddressO = "Via delle Scarpe 33",
                  AddressL = "Via Udine 129",
                  Logo = "~/Content/Logos/1.jpg",
                  DepartamentId = 1,
                  ProvinceId = 1,
                  CityId = 1,
                  Locality = String.Empty,
                  PartitaIva = "Default 123",
                  CodiceFiscale = "Default as Iva",
                  PhoneMobil = "345413323890",
                  Fax = "0432127654",
                  Email = "scarpefriulane@scarpe.com",
                  http = "http://www.scarpefriulane.com"

              },
            new Company()
            {
                CompanyId = 2,
                Name = "Caffè Colombiano DEFAULT",
                Phone = "058856777654",
                AddressO = "Via del Caffè 61",
                AddressL = "Via Pisa 123",
                Logo = "~/Content/Logos/2.jpg",
                DepartamentId = 3,
                ProvinceId = 5,
                CityId = 5,
                Locality = String.Empty,
                PartitaIva = "Default 5678",
                CodiceFiscale = "Default as Iva",
                PhoneMobil = "34543560991",
                Fax = "058856777654",
                Email = "caffetoscana@caffe.com",
                http = "http://www.vendocaffetoscana.com"

            }
            );

            // Categories 5
            context.Categories.AddOrUpdate(c => c.CategoryId,
            new Category()
            {
                CategoryId = 1,
                Description = "Scarpe Donna Tipo Sportivo",
                CompanyId = 1
            },
            new Category()
            {
                CategoryId = 2,
                Description = "Scarpe Uomo Tipo Lavoro",
                CompanyId = 1
            },
            new Category()
            {
                CategoryId = 3,
                Description = "Scarpe Uomo tipo Gimnastica",
                CompanyId = 1
            },
            new Category()
            {
                CategoryId = 4,
                Description = "Scarpe da ballo Uomo",
                CompanyId = 1
            },
            new Category()
            {
                CategoryId = 5,
                Description = "Scarpe da ballo Donna",
                CompanyId = 1
            },
            new Category()
            {
                CategoryId = 6,
                Description = "Scarpe Sportive Uomo",
                CompanyId = 1
            },
             new Category()
             {
                 CategoryId = 7,
                 Description = "Scarpe Donna Tipo Lavoro",
                 CompanyId = 1
             },
              new Category()
              {
                  CategoryId = 8,
                  Description = "Scarpa Tacco Donna",
                  CompanyId = 1
              }
            );

            // Taxes 6
            context.Taxes.AddOrUpdate(t => t.TaxId,
            new Tax()
            {
                TaxId = 1,
                Type = "T",
                Description = "Iva 4% - superagevolata",
                Rate = 0.04,
                CompanyId = 1
            },
           new Tax()
           {
               TaxId = 2,
               Type = "T",
               Description = "Iva 5% - nuova agevolata",
               Rate = 0.05,
               CompanyId = 1
           },
            new Tax()
            {
                TaxId = 3,
                Type = "T",
                Description = "Iva 10% - ridotta",
                Rate = 0.10,
                CompanyId = 1
            },
            new Tax()
            {
                TaxId = 4,
                Type = "T",
                Description = "Iva 22% - ordinaria",
                Rate = 0.22,
                CompanyId = 1
            },
            new Tax()
            {
                TaxId = 5,
                Type = "T",
                Description = "Iva inclusa",
                Rate = 0.00,
                CompanyId = 1
            },
             new Tax()
             {
                 TaxId = 6,
                 Type = "S",
                 Description = "Sconto 33% - 3x2 stagionale",
                 Rate = 0.33,
                 CompanyId = 1
             },
             new Tax()
             {
                 TaxId = 7,
                 Type = "S",
                 Description = "Sconto 8% - Donne Marzo",
                 Rate = 0.08,
                 CompanyId = 1
             },
             new Tax()
             {
                 TaxId = 8,
                 Type = "S",
                 Description = "Sconto 5% - Natale",
                 Rate = 0.05,
                 CompanyId = 1
             }
            );



            //Products 7
            context.Products.AddOrUpdate(p => p.ProductId,

                  new Product()
                  {
                      ProductId = 1,
                      CompanyId = 1,
                      CategoryId = 8,
                      Description = "Old Woman, Con Tacco Plataforma aperte",
                      BarCode = "803208903451",
                      VendorPrice = 0,
                      VendorProductCode = String.Empty,
                      Price = 69.90m,
                      TaxId = 4,
                      Image = "~/Content/Products/1.jpg",
                      Remarks = String.Empty,
                      ReorderPoint = 3

                  },
                  new Product()
                  {
                      ProductId = 2,
                      CompanyId = 1,
                      CategoryId = 1,
                      Description = "Vans, da Ginnastica Basse Unisex Adulto",
                      BarCode = "8032089000031",
                      VendorPrice = 0,
                      VendorProductCode = String.Empty,
                      Price = 72.90m,
                      TaxId = 4,
                      Image = "~/Content/Products/2.jpg",
                      Remarks = String.Empty,
                      ReorderPoint = 8
                  },
                   new Product()
                   {
                       ProductId = 3,
                       CompanyId = 1,
                       CategoryId = 4,
                       Description = "SanSha Scarpe da Ballo Uomo",
                       BarCode = "8032089000091",
                       VendorPrice = 0,
                       VendorProductCode = String.Empty,
                       Price = 29.90m,
                       TaxId = 4,
                       Image = "~/Content/Products/3.jpg",
                       Remarks = String.Empty,
                       ReorderPoint = 5
                   },

                  new Product()
                  {
                      ProductId = 4,
                      CompanyId = 1,
                      CategoryId = 8,
                      Description = "Hermosas Scarpe tacco Donna Eleganti",
                      BarCode = "8032089034561",
                      VendorPrice = 0,
                      VendorProductCode = String.Empty,
                      Price = 39.90m,
                      TaxId = 4,
                      Image = "~/Content/Products/4.jpg",
                      Remarks = String.Empty,
                      ReorderPoint = 10
                  }
               );

            //Warehouses 8
            context.Warehouses.AddOrUpdate(w => w.WarehouseId,
               new Warehouse()
               {
                   WarehouseId = 1,
                   CompanyId = 1,
                   Name = "Outlet Palmanova DEFAULT",
                   Phone = "04325678904",
                   Address = "Via degli Outlet a Palma 42",
                   DepartamentId = 1,
                   ProvinceId = 1,
                   CityId = 1

               },
                new Warehouse()
                {
                    WarehouseId = 2,
                    CompanyId = 1,
                    Name = "Iper COM Palmanova DEFAULT",
                    Phone = "04324567890",
                    Address = "Via degli Iper 77",
                    DepartamentId = 1,
                    ProvinceId = 1,
                    CityId = 1

                },
                new Warehouse()
                {
                    WarehouseId = 3,
                    CompanyId = 2,
                    Name = "Iper COM VOLTERRA DEFAULT",
                    Phone = "0588449988770",
                    Address = "Via degli Iper Volterra 12",
                    DepartamentId = 3,
                    ProvinceId = 5,
                    CityId = 5

                }
               );

            //pay attention supplierproduct
            //Inventories 9
            context.Inventories.AddOrUpdate(i => i.InventoryId,
              new Inventory()
              {
                  InventoryId = 1,
                  WarehouseId = 1,
                  ProductId = 1,
                  Stock = 10.00
              },
               new Inventory()
               {
                   InventoryId = 2,
                   WarehouseId = 1,
                   ProductId = 2,
                   Stock = 9.00
               },

               new Inventory()
               {
                   InventoryId = 3,
                   WarehouseId = 2,
                   ProductId = 2,
                   Stock = 100.00
               }
              );

            //States 10
            context.States.AddOrUpdate(s => s.StateId,
             new State()
             {
                 StateId = 1,
                 Description = "Creata"
             }
             );


            //users 11, suppliers 12, customer 13, CompanyCus 14, CompanySupp 15
        }
    }
}

