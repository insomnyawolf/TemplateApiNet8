using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using TemplateApiNet6.Database;
using TemplateApiNet6.Database.Infraestructure;

namespace TemplateApiNet6.Api.Shared;

[Route("api/v{version:apiVersion}/[controller]")]
public class BaseODataController<TEntity> : ODataController where TEntity : class
{
    public DbContext DbContext { get; set; }
    public BaseODataController(DbContext DbContext)
    {
        this.DbContext = DbContext;
    }

    [HttpGet]
    [EnableQuery]
    public List<TEntity> Get()
    {
        return DbContext.Set<TEntity>().ToList();
    }
}
