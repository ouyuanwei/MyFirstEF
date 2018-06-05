using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Base;
using AutoMapper;

namespace Implement
{
    public class BaseService
    {
        protected IUnitOfWork unitOfWork = new UnitOfWork();
        public virtual T Get<S, T>(Func<S, bool> where)
           where T : class
            where S : class
        {
            var resquest = unitOfWork.GetRepository<S>().dbSet.FirstOrDefault(where);
            var map = new MapperConfiguration(x => x.CreateMap<S, T>()).CreateMapper();
            var back = map.Map<T>(resquest);
            return back;
        }
    }
}
