using Domain.Interface;
using Domain.Entities;
using Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity>(BlogDbContext context) : IGenericRepository<TEntity> where TEntity : class
    {
        internal readonly DbSet<TEntity> DbSet = context.Set<TEntity>();
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var entry = await DbSet.AddAsync(entity);
            return entry.Entity;
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            IQueryable<TEntity> query = DbSet;

            if (typeof(TEntity) == typeof(User))
            {
                query = query.Include("UserRoles.Role");
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            if (typeof(TEntity) == typeof(User))
            {
                return await DbSet
                    .Include("UserRoles.Role")
                    .FirstOrDefaultAsync(entity => EF.Property<int>(entity, "Id") == id);
            }

            return await DbSet.FindAsync(id);
        }

        public TEntity Update(TEntity entity)
        {
            var entry = DbSet.Update(entity);
            return entry.Entity;
        }
    }
}
