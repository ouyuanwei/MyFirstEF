using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Base
{
   public class GenericRepository<TEntity> where TEntity:class
    {
        private MyDbContext context;
        public DbSet<TEntity> dbSet;

        private GenericRepository() { }
        public GenericRepository(MyDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = this.dbSet.Find(id);
            
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if(this.context.Entry(entityToDelete).State==EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public virtual void Insert(TEntity entityToInsert)
        {
            dbSet.Add(entityToInsert);
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
