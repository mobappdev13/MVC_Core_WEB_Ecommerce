using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ecommerce01.Models;

namespace Ecommerce01.Classes 
{
    public class DropDownHelper : IDisposable
    {
        private static Ecommerce01Context db = new Ecommerce01Context();

        public static List<Departament> GetDepartaments()
        {
            var departments = db.Departaments.ToList();
            departments.Add(new Departament
            {
                DepartamentId = 0,
                Name = "[Selezione una Regione...     ]"
            });
            return departments = departments.OrderBy(d => d.Name).ToList();
        }

        public static List<Province> GetProvinces()
        {
            var provinces = db.Provinces.ToList();
            provinces.Add(new Province
            {
                ProvinceId = 0,
                Name = "[Selezione una Provincia...     ]"
            });
            return provinces = provinces.OrderBy(p => p.Name).ToList();
        }

        public static List<City> GetCities()
        {
            var cities = db.Cities.ToList();
            cities.Add(new City
            {
                CityId = 0,
                Name = "[Selezione una Città...     ]"
            });
            return cities = cities.OrderBy(c => c.Name).ToList();
        }

        public static List<Company> GetCompanies()
        {
            var companies = db.Companies.ToList();
            companies.Add(new Company
            {
                CompanyId = 0,
                Name = "[Selezione una Azienda...     ]"
            });
            return companies = companies.OrderBy(c => c.Name).ToList();
        }

        public static List<Category> GetCategories(int companyId)
        {
            var categories = db.Categories
                .Where(t => t.CompanyId == companyId)
                .ToList();
            categories.Add(new Category
            {
                CategoryId = 0,
                Description = "[Selezione una categoria...   ]"
            });
            return categories = categories.OrderBy(c => c.Description).ToList();
        }

        public static List<Tax> GetTaxes(int companyId)
        {
            var taxes = db.Taxes
                .Where(t => t.CompanyId == companyId)                
                .ToList();
            taxes.Add(new Tax
            {
                TaxId = 0,
                Description = "[Selezione una tassa...   ]"
            });
            return taxes = taxes.OrderBy(c => c.Description).ToList();
        }

        

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
