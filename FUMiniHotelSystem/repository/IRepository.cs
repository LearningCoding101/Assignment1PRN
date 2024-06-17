using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FUMiniHotelSystem {
    public interface IRepository<T> where T : class {
        internal List<T> GetAll();
        internal T GetById(int id);
        internal void Add(T entity);
        internal void Update(T entity);
        internal void Delete(int id);
    }
}
