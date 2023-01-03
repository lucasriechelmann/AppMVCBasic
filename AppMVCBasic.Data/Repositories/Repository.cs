using AppMVCBasic.Business.Interfaces;
using AppMVCBasic.Business.Models;
using AppMVCBasic.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppMVCBasic.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly AspMVCBasicDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(AspMVCBasicDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }
        public virtual async Task<List<TEntity>> GetAsync()
        {
            return await DbSet.ToListAsync();
        }
        public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
        public virtual async Task AddAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveAsync();
        }
        public virtual async Task UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveAsync();
        }
        public virtual async Task DeleteAsync(Guid id)
        {
            DbSet.Remove(new TEntity() { Id = id });
            await SaveAsync();
        }
        public async Task<int> SaveAsync()
        {
            return await Db.SaveChangesAsync();
        }        
        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
