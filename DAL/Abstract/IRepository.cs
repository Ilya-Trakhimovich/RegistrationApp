using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IRepository<T> : IDisposable  where T : class
    {
        IEnumerable<T> Items { get; }    
        T GetItem(int? id);
        void Create(T item);
        void Update(T item);
        void DeleteItem(int id);  
    }
}
