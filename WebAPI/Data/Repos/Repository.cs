using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebAPI.Data.Models;

namespace WebAPI.Data.Repos;

public class Repository<T> where T : Model
{
    public IQueryable<T> Query { get => DataSet; }

    protected DbSet<T> DataSet { get; init; }
    protected DbContext Context { get; init; }

    public Repository(DbContext dbContext) 
        => (Context, DataSet) = (dbContext, dbContext.Set<T>());

    public virtual async Task<List<T>> FindAll() => await DataSet.ToListAsync();
    public virtual async Task<T> FindById(int id) => await DataSet.Where(t => t.Id == id).SingleAsync();
    public virtual async Task<List<T>> FindBy(Expression<Func<T, bool>> predicate) 
        => await DataSet.Where(predicate).ToListAsync();
    
    public virtual void Add(T entity) => DataSet.Add(entity);
    public virtual void AddRange(params T[] entities) => DataSet.AddRange();

    public virtual void Update(T entity) => DataSet.Update(entity);
    public virtual async Task UpdateRange(Expression<Func<T, bool>> predicate) => DataSet.UpdateRange(await FindBy(predicate));
    public virtual void UpdateRange(params T[] entities) => DataSet.UpdateRange(entities);

    public virtual void Delete(T entity) => DataSet.Remove(entity);
    public virtual async Task DeleteRange(Expression<Func<T, bool>> predicate) => DataSet.RemoveRange(await FindBy(predicate));
    public virtual void DeleteRange(params T[] entities) => DataSet.RemoveRange(entities);
    public virtual async Task<T?> DeleteById(int id) {
        var entity = await FindById(id);
        if (entity == null) {
            return null;
        }

        DataSet.Remove(entity);
        return entity;
    }

    public virtual async Task LoadCollection<C>(
        T entity, 
        Expression<Func<T, IEnumerable<C>>> predicate, 
        int limit = 0, 
        int skip = 0,
        Func<C, bool>? filter = null) 
        where C : Model {
            
        var query = Context.Entry(entity).Collection(predicate).Query();
        if (filter != null) query = query.Where(e => filter(e));
        if (skip > 0) query = query.Skip(skip);
        if (limit > 0) query = query.Take(limit);

        await query.LoadAsync();
    }
    public virtual async Task LoadReference<R>(T entity, Expression<Func<T, R?>> predicate) where R : Model
        => await Context.Entry(entity).Reference(predicate).LoadAsync();
        
    public async Task<IDbContextTransaction> StartTransaction() {
        return await Context.Database.BeginTransactionAsync();
    }
    
    public async Task<int> Count() => await DataSet.CountAsync();
    public async Task Save() => await Context.SaveChangesAsync();
}