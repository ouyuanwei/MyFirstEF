using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.BaseEntity
{
   public  class BaseCache
    {
        public DateTime OverdueTime { set; get; }
    }
    public class BaseCache<T>:BaseCache
    {
        public T Data { set; get; }
        public BaseCache()
        {
        }
        public BaseCache(T data)
        {
            this.Data = data;
        }
    }
}
