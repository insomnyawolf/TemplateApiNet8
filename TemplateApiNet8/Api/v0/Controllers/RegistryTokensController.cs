using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using TemplateApiNet8.Api.Shared;
using TemplateApiNet8.Database;

namespace TemplateApiNet8.Api.v0.Controllers.Default;

[ApiV0]
[ApiController]
public class RegistryTokensController : BaseController<RegistryTokensController>
{
    public DatabaseContext DatabaseContext { get; set; }
    public RegistryTokensController(DatabaseContext DatabaseContext, IServiceProvider IServiceProvider) : base(IServiceProvider)
    {
        this.DatabaseContext = DatabaseContext;
    }

    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation(Summary = nameof(CreateToken), Description = "Sample Description")]
    public async Task<string> CreateToken(CancellationToken cancellationToken = default)
    {
        var newToken = Guid.NewGuid().ToString();

        var tokenItem = new Database.Models.RegistryToken
        {
            Token = newToken
        };

        DatabaseContext.RegistryTokens.Add(tokenItem);

        await DatabaseContext.SaveChangesAsync();

        return newToken;
    }

    [HttpDelete]
    [AllowAnonymous]
    [SwaggerOperation(Summary = nameof(ConsumeToken), Description = "Sample Description")]
    public async Task<ActionResult> ConsumeToken(string token, CancellationToken cancellationToken = default)
    {
        var query = DatabaseContext.RegistryTokens.Where(item => item.Token == token);

        var dbItem = await query.FirstOrDefaultAsync();

        if (dbItem is null)
        {
            return Unauthorized();
        }

        var files = await query.ExecuteDeleteAsync();

        return Ok();
    }
}
