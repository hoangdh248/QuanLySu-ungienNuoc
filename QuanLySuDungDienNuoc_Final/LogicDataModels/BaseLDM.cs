using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicDataModels
{
    public class BaseLDM<T> where T : class
    {
        public virtual List<T> GetElements()
        {
            return null;
        }
        public virtual List<T> GetElements(int id)
        {
            return null;
        }
        public virtual T GetElement(int id)
        {
            return default(T);
        }
        public virtual bool Delete(int id)
        {
            return default(bool);
        }
        public virtual T Insert (T obj)
        {
            return default(T);
        }
        public virtual bool Update (T obj)
        {
            return default(bool);
        }

    }
}
