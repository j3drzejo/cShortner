using System;
using cShortner.Frontend.Models;

namespace cShortner.Frontend.Services;

public interface IUrlShortnerService
{
    public string Add(string OgUrl);
    public IEnumerable<UrlRecord> GetAll();
    public IEnumerable<UrlRecord> GetTop5MostClicked();
    public string? GetOriginalUrl(string shortCode);
    event Action OnChange;
}
