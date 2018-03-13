using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RepositoryAspNetMVC.Models;

namespace RepositoryAspNetMVC.Models
{
    public class CRUDRepository<T> : ICRUDRepository<T> where T : class
    {

        private Ecommerce19Entities db;

        private DbSet<T> table;

        //ctor
        public CRUDRepository()
        {
            db = new Ecommerce19Entities();
            table = db.Set<T>();
        }


        //interface method's
        public long count()
        {
            return table.Count(); 
        }

        //interface method's
        public void create(T obj)
        {
            table.Add(obj);
            //todo try catch
            db.SaveChanges();
        }

        //interface method's
        public void delete(object id)
        {
            T tRow = table.Find(id);
            table.Remove(tRow);
            //todo try catch
            db.SaveChanges();
        }

        //interface method's
        public T find(object id)
        {
            // todo try catch
                return table.Find(id);
           
            //catch (Exception)
            //{
            //    return null;
            //}
        }

        //interface method's
        public IEnumerable<T> findAll()
        {
            return table.ToList();
        }

        //interface method's
        public void update(T obj)
        {
            table.Attach(obj);
            db.Entry(obj).State = EntityState.Modified;
            //todo try catch
            db.SaveChanges();
        }
    }
}