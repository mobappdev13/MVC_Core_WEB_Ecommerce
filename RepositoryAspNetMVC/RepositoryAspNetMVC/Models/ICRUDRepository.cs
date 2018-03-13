using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryAspNetMVC.Models
{
    public interface ICRUDRepository<T> where T: class
    {
        IEnumerable<T> findAll();
        T find(object id);
        void create(T obj);
        void delete(object id);
        void update(T obj);
        long count();
    }
}
