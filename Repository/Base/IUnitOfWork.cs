using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Base
{
    public interface IUnitOfWork
    {
        void Save();
        void BeginTranscation();
        void Commit();
        void Dispose();
        GenericRepository<T> GetRepository<T>() where T : class;
    }
}
