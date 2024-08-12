using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Collections.Immutable;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Xabbo;

/// <summary>
/// Represents a hotel region.
/// </summary>
public sealed record Hotel
{
    /// <summary>
    /// Represents no particular hotel.
    /// </summary>
    public static readonly Hotel None = new();

    private static readonly Lazy<ImmutableDictionary<string, Hotel>> _hotels = new(LoadHotels);

    /// <summary>
    /// Contains the definitions of all hotels, mapped by their identifier.
    /// </summary>
    public static ImmutableDictionary<string, Hotel> All => _hotels.Value;

    private static ImmutableDictionary<string, Hotel> LoadHotels()
    {
        string basePath = Environment.ExpandEnvironmentVariables(
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? @"%LOCALAPPDATA%\xabbo"
            : @"%HOME%/.local/xabbo");

        FileInfo fileHotelsOverride = new(Path.Join(basePath, "hotels.override.json"));
        FileInfo fileHotels = new(Path.Join(basePath, "hotels.json"));

        List<Hotel>? hotels = [
            new("US", "us"),
            new("Spain", domain: "es"),
            new("Finland", domain: "fi"),
            new("Italy", domain: "it"),
            new("Netherlands", domain: "nl"),
            new("Germany", domain: "de"),
            new("France", domain: "fr"),
            new("Brazil", identifier: "br", domain: "com.br"),
            new("Turkey", identifier: "tr", domain: "com.tr"),
            new("Sandbox", "s2", subdomain: "sandbox"),
            new("Origins (US)", "ous", subdomain: "origins")
        ];

        if (fileHotelsOverride.Exists)
            hotels = JsonSerializer.Deserialize<List<Hotel>>(File.ReadAllText(fileHotelsOverride.FullName)) ?? [];
        if (fileHotels.Exists)
            hotels.AddRange(JsonSerializer.Deserialize<List<Hotel>>(File.ReadAllText(fileHotelsOverride.FullName)) ?? []);

        return hotels.ToImmutableDictionary(x => x.Identifier, StringComparer.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// Gets the name of this hotel, e.g. "US", "Netherlands".
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the identifier of this hotel, e.g. "us", "nl".
    /// </summary>
    public string Identifier { get; init; } = string.Empty;

    /// <summary>
    /// Gets the subdomain of this hotel, e.g. "www", "sandbox".
    /// </summary>
    public string Subdomain { get; init; } = string.Empty;

    /// <summary>
    /// Gets the top-level domain of this hotel, e.g. "com", "com.br".
    /// </summary>
    public string Domain { get; init; } = string.Empty;

    /// <summary>
    /// Gets the hostname for this hotel, e.g. "habbo".
    /// </summary>
    public string HostName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the web host for this hotel, e.g. "www.habbo.com".
    /// </summary>
    public string WebHost { get; } = string.Empty;

    /// <summary>
    /// Gets the game host for this hotel.
    /// </summary>
    public string GameHost { get; init; } = string.Empty;

    /// <summary>
    /// Creates a new hotel instance.
    /// </summary>
    private Hotel() { }

    /// <summary>
    /// Creates a new hotel instance.
    /// </summary>
    /// <param name="name">The name of the hotel.</param>
    /// <param name="identifier">The short identifier of the hotel, e.g. "us".</param>
    /// <param name="subdomain">The subdomain, e.g. "www", "sandbox".</param>
    /// <param name="domain">The top-level domain, e.g. "com", "com.br".</param>
    /// <param name="host">The hostname, e.g. "habbo".</param>
    /// <param name="gameHost">The game host, defaults to "game-{identifier}.habbo.com".</param>
    [JsonConstructor]
    public Hotel(
        string name, string? identifier = null,
        string subdomain = "www", string domain = "com",
        string host = "habbo", string? gameHost = null)
    {
        identifier ??= domain;
        gameHost ??= $"game-{identifier}.{host}.com";

        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(identifier);
        ArgumentException.ThrowIfNullOrWhiteSpace(subdomain);
        ArgumentException.ThrowIfNullOrWhiteSpace(domain);
        ArgumentException.ThrowIfNullOrWhiteSpace(host);
        ArgumentException.ThrowIfNullOrWhiteSpace(gameHost);

        Name = name;
        Identifier = identifier;
        Subdomain = subdomain;
        Domain = domain;
        HostName = host;

        WebHost = "";
        if (!string.IsNullOrWhiteSpace(Subdomain))
            WebHost += $"{Subdomain}.";
        WebHost += $"{HostName}.{Domain}";
        GameHost = gameHost;
    }

    /// <summary>
    /// Gets the hotel with the specified identifier, e.g. "us".
    /// </summary>
    /// <returns>
    /// The matching Hotel, or <see cref="None"/> if it was not found.
    /// </returns>
    public static Hotel FromIdentifier(string identifier) =>
        All.TryGetValue(identifier, out Hotel? hotel) ? hotel : None;

    /// <summary>
    /// Gets the hotel with the specified game host, e.g. "game-us.habbo.com".
    /// </summary>
    /// <returns>
    /// The matching Hotel, or <see cref="None"/> if it was not found.
    /// </returns>
    public static Hotel FromGameHost(string gameHost) =>
        All.Values.FirstOrDefault(x => x.GameHost.Equals(gameHost, StringComparison.OrdinalIgnoreCase)) ?? None;
}
