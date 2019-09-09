using Common.Models;
using Common.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Storage
{
    public abstract class GenericRepository<TEntity>
         : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        protected DbContext Context { get; }

        public bool IsAtomic { get; }

        public GenericRepository(DbContext context)
            : this(context, true) { }

        public GenericRepository(DbContext context, bool isAtomic)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            IsAtomic = isAtomic;
        }

        public virtual async Task<long> Add(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);

            if (IsAtomic) await Context.SaveChangesAsync();

            return entity.Id;
        }

        public virtual async Task<IQueryable<TEntity>> GetAll()
            => await Task.Run(() => Context.Set<TEntity>().AsQueryable());

        public virtual async Task<TEntity> GetById(long entitykey)
            => await Context.Set<TEntity>().FindAsync(entitykey);

        public virtual async Task Remove(long entityKey)
        {
            var entity = await GetById(entityKey);
            await Remove(entity);
        }

        public virtual async Task Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            if (IsAtomic) await Context.SaveChangesAsync();
        }

        public virtual async Task Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            if (IsAtomic) await Context.SaveChangesAsync();
        }
    }
}
