using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Base;
using AutoMapper;

namespace Implement
{
    public class BaseSerice
    {
        protected IUnitOfWork unitOfWork = new UnitOfWork();
        public virtual T Get<S, T>(Func<S, bool> where)
           where T : class
            where S : class
        {
            Mapper.Initialize(x => x.CreateMap<S, T>());
            var resquest = unitOfWork.GetRepository<S>().dbSet.FirstOrDefault(where);
            var back = Mapper.Map<S, T>(resquest);
            return back;
        }
    }
}
