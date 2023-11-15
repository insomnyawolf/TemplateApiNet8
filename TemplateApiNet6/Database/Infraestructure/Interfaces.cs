namespace TemplateApiNet6.Database.Infraestructure;

public interface ISoftDeleted
{
    public bool IsDeleted { get; set; }
}

public interface IMultiTenant
{
    public int TenantId { get; set; }
}
