using Microsoft.AspNetCore.Mvc.Filters;

namespace TemplateApiNet6.Database.Infraestructure;

public class DatabaseContextConfigurationFilter : ActionFilterAttribute
{
    public DatabaseContext DatabaseContext { get; set; }
    public DatabaseContextConfigurationFilter(DatabaseContext DatabaseContext)
    {
        this.DatabaseContext = DatabaseContext;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var user = context.HttpContext.User;

        var claim = user.Claims.SingleOrDefault(claim => claim.Type == "Tennant");

        if (claim != null)
        {
            var tennant = claim.Value;
            DatabaseContext.TenantId = int.Parse(tennant);
        }

        base.OnActionExecuting(context);
    }
}
