using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using WebAPI.Data.Models;
using WebAPI.Data.Repos;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Services;

public abstract class Service {}
public abstract class Service<R, M> : Service where R : Repository<M> where M : Model
{
    protected R Repository { get; private set; }
    public async Task<IDbContextTransaction> CreateTransaction() => await Repository.StartTransaction();
    public Service(R repository) {
        this.Repository = repository;
    }

    public virtual async Task<M> GetModel(int id) {
        return await Repository.FindById(id);
    }
    public async Task<IEnumerable<M>> GetModel() {
        return await Repository.FindAll();
    }

    // Update using reflection
    public virtual async Task<M> UpdateModel(M model, object updateInfo) {
        model.AssignNotDefaultProperties(updateInfo);
        await Repository.Save();

        return model;
    }
    public virtual async Task<M> UpdateModel(int modelId, object updateInfo) {
        var model = await UpdateModel(await Repository.FindById(modelId), updateInfo);
        await Repository.Save();

        return model;
    }
    public virtual async Task AddModel(params M[] models) {
        Repository.AddRange(models);
        await Repository.Save();
    }
    public virtual async Task<IEnumerable<M>> AddModel(IEnumerable<M> models) {
        Repository.AddRange(models.ToArray());
        await Repository.Save();

        return models;
    }
    public virtual async Task<M?> DeleteModel(int id) {
        var entity = await Repository.DeleteById(id);
        await Repository.Save();

        return entity;
    }
    public virtual async Task<M> DeleteModel(M model) {
        Repository.Delete(model);
        await Repository.Save();

        return model;
    }

    public async Task ClearData() {
        await this.Repository.DeleteRange(e => true);
    }
}