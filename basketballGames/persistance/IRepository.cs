using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistance
{
    public interface IRepository<ID, T>
    {
        public void Save(T elem);
        public void Delete(ID id);
        public void Update(T elem);
        public IList<T> FindAll();
        public T FindOne(ID id);
    }
}
