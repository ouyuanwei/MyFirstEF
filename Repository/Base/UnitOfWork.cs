using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private MyDbContext context = new MyDbContext();
        private Stack saveList = new Stack();
        public void BeginTranscation()
        {
            this.saveList.Push(null);
        }

        public void Commit()
        {
            if(this.saveList.Count>0)
            {
                this.saveList.Pop();
            }
            this.Save();
        }
        private bool disposed = false;
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }
       protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if(disposing)
                {
                    this.context.Dispose();
                }
            }
            this.disposed = true;
        }
        public GenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(context);
        }

        public void Save()
        {
            try
            {
                if (this.saveList.Count == 0)
                {
                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
