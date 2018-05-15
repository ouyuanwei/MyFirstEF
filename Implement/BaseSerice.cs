using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Base;
using System.Reflection;

namespace Implement
{
   public  class BaseSerice
    {
        protected IUnitOfWork unitOfWork =new UnitOfWork();
    }
}
