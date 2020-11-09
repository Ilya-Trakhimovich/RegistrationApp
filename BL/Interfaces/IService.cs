using BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IService<T>
    {
        void Create(T item);
        T GetItem(int? id);
        void Update(T item);
        void Delete(int id);
    }
}
