using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using WebAPI.Data.Models;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Services;

// Provide basic method for CRUD operations other than that the derived class will provide the rest.
public abstract class Service {}
public abstract class Service<TModel, TContext> : Service where TContext : DbContext where TModel : Model
{
    protected TContext Context { get; init; }
    protected abstract DbSet<TModel> Data { get; }

    public Service(TContext context) {
        Context = context;
    }

    public virtual async Task<IDbContextTransaction> CreateTransaction() {
        return await Context.Database.BeginTransactionAsync();
    }

    public virtual async Task<TModel> Add(TModel model) {
        var entity = Data.Add(model).Entity;
        await Context.SaveChangesAsync();
        return entity;
    }
    public virtual async Task<TModel> Get(int id) {
        return await Data.FindById(id);
    }
    public virtual async Task<TModel> Update(int id, TModel model) {
        var entity = await Data.FindById(id);

        if (entity == null) {
            return await Add(model);
        } else {
            foreach (var field in entity.GetType().GetFields()) {
                field.SetValue(entity, field.GetValue(model));
            }

            await Context.SaveChangesAsync();
            return entity;
        }
    }
    public virtual async Task<TModel> Delete(int id) {
        var entity = Data.Remove(await Data.FindById(id)).Entity;
        await Context.SaveChangesAsync();

        return entity;
    }
}