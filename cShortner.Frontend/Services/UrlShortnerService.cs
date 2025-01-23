using System;
using cShortner.Frontend.Models;

namespace cShortner.Frontend.Services;

public class UrlShortnerService : IUrlShortnerService
{
    private readonly List<UrlRecord> _urlRecords;

    public event Action? OnChange;

    public UrlShortnerService()
    {
        _urlRecords = [];
    }

    public string Add(string ogUrl)
    {
        var existingRecord = _urlRecords.FirstOrDefault(record => record.OriginalUrl == ogUrl);
        if (existingRecord != null)
        {
            return existingRecord.NewUrl;
        }

        string newUrl;
        do
        {
            newUrl = GenerateUniqueKey();
        } while (_urlRecords.Any(record => record.NewUrl == newUrl));

        var newRecord = new UrlRecord
        {
            Id = _urlRecords.Count.ToString(),
            OriginalUrl = ogUrl,
            NewUrl = $"http://localhost:5149/s/{newUrl}",
            CreatedAt = DateTime.Now,
            Clicks = 0,
        };

        _urlRecords.Add(newRecord);
        OnChange?.Invoke();
        return $"http://localhost:5149/s/{newUrl}";
    }

    private static string GenerateUniqueKey()
    {
        var random = new Random();
        return random.Next(100000, 1000000).ToString();
    }

    public IEnumerable<UrlRecord> GetAll()
    {
        return [.. _urlRecords];
    }

    public IEnumerable<UrlRecord> GetTop5MostClicked()
    {
        return [.. _urlRecords.OrderByDescending(record => record.Clicks).Take(5)];
    }

    public string? GetOriginalUrl(string shortCode)
    {
        var record = _urlRecords.FirstOrDefault(record => record.NewUrl.EndsWith($"/{shortCode}"));
        if (record != null)
        {
            record.Clicks++;

            OnChange?.Invoke();

            return record.OriginalUrl;
        }

        return null;
    }
}
