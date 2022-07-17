using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using FluentValidation;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Models;
using WebAPI.Services;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class ApiControllerBase<TModel> : ControllerBase where TModel : Model
{
    public abstract DbSet<TModel> DataSet { get; }
    public ShopContext Context { get; init; }

    public ApiControllerBase(ShopContext context)
    {
        Context = context;
    }

    [HttpGet("{id:int}")]
    [ProducesErrorResponseType(typeof(NotFoundResult))]
    public async Task<IActionResult> Get([FromRoute] int id) {
        return Ok(await DataSet.FindById(id).SingleAsync());
    }
    [HttpGet()]
    public async Task<IActionResult> GetAll() {
        var result = await DataSet.ToArrayAsync();
        if (result == null || result.Length == 0) {
            return NoContent();
        }

        return Ok(result);
    }

    [HttpPost]
    public virtual async Task<IActionResult> Add(
        [FromBody] TModel model, 
        [FromServices] IValidator<TModel> validator) 
    {
        try {
            await validator.ValidateAndThrowAsync(model);
            DataSet.Add(model);
            await Context.SaveChangesAsync();

            return Created(new Uri(Request.GetDisplayUrl() + model.Id), model);
        } catch (ValidationException e) {
            return BadRequest(e.Message);
        }

    }

    [HttpPut("{id:int}")]
    public virtual async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] TModel model, 
        [FromServices] IValidator<TModel> validator) 
    {
        try {
            await validator.ValidateAndThrowAsync(model);
            if (model.Id != id)
                return Conflict("Id provided in the model is not the same as the id in route!");
            
            var result = DataSet.Update(model);
            await Context.SaveChangesAsync();

            if (result.State == EntityState.Added) {
                return Created(new Uri(Request.GetDisplayUrl() + result.Entity.Id), result.Entity);
            } else {
                return NoContent();
            }
        } catch (ValidationException e) {
            return BadRequest(e.Message);
        } catch (DBConcurrencyException e) {
            return Conflict(e.ToString());
        }
    }
    [HttpPut]
    public virtual async Task<IActionResult> UpdateRange(
        [FromBody] TModel[] models, 
        [FromServices] IValidator<TModel> validator) 
    {
        try {
            var createdEntities = new List<TModel>();

            foreach (var model in models) {
                await validator.ValidateAndThrowAsync(model);
                var result = DataSet.Update(model);
                if (result.State == EntityState.Added) {
                    createdEntities.Add(result.Entity);
                }
            }
            
            await Context.SaveChangesAsync();
            
            if (createdEntities.Count > 0) {
                return Created(new Uri(Request.GetDisplayUrl()), createdEntities);
            }

            return NoContent();
        } catch (ValidationException e) {
            return BadRequest(e.Message);
        } catch (DBConcurrencyException e) {
            return Conflict(e.ToString());
        }
    }
    [HttpDelete("{id:int}")]
    public virtual async Task<IActionResult> Delete([FromRoute] int id) {
        try {
            var entity = await DataSet.FindById(id).SingleAsync();
            DataSet.Remove(entity);
            await Context.SaveChangesAsync();

            return NoContent();
        } catch (InvalidOperationException) {
            return NotFound($"Can't find entity with id {id} to delete!");
        }
    }
    [HttpDelete]
    public virtual async Task<IActionResult> DeleteRange([FromBody] int[] idArr) {
        foreach (var id in idArr) {
            var result = await Delete(id);
            if (result.GetType() == typeof(NotFoundObjectResult)) {
                return result;
            }
        }

        await Context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id:int}")]
    public virtual async Task<IActionResult> Patch(
        [FromRoute] int id,
        [FromBody] JsonPatchDocument<TModel> patchDocument,
        [FromServices] IValidator<TModel> validator) 
    {
        try {
            var entity = await DataSet.FindById(id).SingleAsync();
            patchDocument.ApplyTo(entity);
            
            await validator.ValidateAndThrowAsync(entity);
            await Context.SaveChangesAsync();
            
            return Ok(entity);
        } catch (InvalidOperationException) {
            return NotFound();
        } catch (ValidationException e) {
            return BadRequest($"Malformed patch document for {typeof(TModel).Name}! Reasons: \n{e.Message}");
        }
    }
}