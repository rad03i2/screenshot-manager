using System.Text.Json;

namespace ScreenshotManager;

public static class ScreenshotCatalog
{
    private static readonly HashSet<string> Extensions = new(StringComparer.OrdinalIgnoreCase)
        { ".png", ".jpg", ".jpeg", ".webp", ".bmp", ".gif" };

    public static IReadOnlyList<ScreenshotEntry> Scan(string root, bool recursive = false)
    {
        var fullRoot = Path.GetFullPath(root);
        if (!Directory.Exists(fullRoot)) throw new DirectoryNotFoundException($"Folder not found: {fullRoot}");
        var option = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
        return Directory.EnumerateFiles(fullRoot, "*", option)
            .Where(p => Extensions.Contains(Path.GetExtension(p)))
            .Select(p => { var f = new FileInfo(p); return new ScreenshotEntry(f.FullName, f.Name, f.Length, f.LastWriteTimeUtc, f.Extension.ToLowerInvariant()); })
            .OrderByDescending(x => x.ModifiedUtc).ThenBy(x => x.Name, StringComparer.OrdinalIgnoreCase).ToArray();
    }

    public static IReadOnlyList<ScreenshotEntry> Search(IEnumerable<ScreenshotEntry> entries, string query) =>
        entries.Where(e => e.Name.Contains(query, StringComparison.OrdinalIgnoreCase)).ToArray();

    public static void ExportJson(IEnumerable<ScreenshotEntry> entries, string output)
    {
        var json = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(output, json);
    }
}
