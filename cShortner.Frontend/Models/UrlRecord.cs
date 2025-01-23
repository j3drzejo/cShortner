using System;

namespace cShortner.Frontend.Models;

public class UrlRecord
{
    public required string Id { get; set; }
    public required string OriginalUrl { get; set; }
    public required string NewUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Clicks { get; set; }
}
