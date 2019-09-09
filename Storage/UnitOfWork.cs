using Autofac;
using Common.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Storage
{
    public class UnitOfWork :
        IUnitOfWork
    {
        private DbContext Context { get; }
        private IComponentContext ComponentContext { get; }

        private Dictionary<Type, IRepository> Repositories;

        public UnitOfWork(DbContext context, IComponentContext componentContext)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            ComponentContext = componentContext ?? throw new ArgumentNullException(nameof(componentContext));
            Repositories = new Dictionary<Type, IRepository>();
        }

        public TRepository GetRepository<TRepository>() where TRepository : IRepository
        {
            if (!Repositories.ContainsKey(typeof(TRepository)))
            {
                var repostory = ComponentContext.Resolve<TRepository>(
                    new TypedParameter(typeof(DbContext), Context),
                    new TypedParameter(typeof(bool), false));

                Repositories.Add(typeof(TRepository), repostory);
            }

            return (TRepository)Repositories[typeof(TRepository)];
        }

        public async Task SaveChanges()
            => await Context.SaveChangesAsync();
    }
}
