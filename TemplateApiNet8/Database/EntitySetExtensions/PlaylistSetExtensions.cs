using TemplateApiNet6.Database.Models;

namespace TemplateApiNet6.Database.EntitySetExtensions;

public static class PlaylistSetExtensions
{
    public static IQueryable<Playlist> FilterById(this IQueryable<Playlist> source, int id)
    {
        return source.Where(item => item.PlaylistId == id);
    }
}
