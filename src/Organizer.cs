namespace ScreenshotManager;

public static class Organizer
{
    public static IReadOnlyList<OrganizeAction> Plan(IEnumerable<ScreenshotEntry> entries, string destinationRoot)
    {
        var root = Path.GetFullPath(destinationRoot);
        return entries.Select(e => {
            var folder = Path.Combine(root, e.ModifiedUtc.ToLocalTime().ToString("yyyy-MM"));
            var destination = UniquePath(folder, e.Name, e.Path);
            return new OrganizeAction(e.Path, destination);
        }).ToArray();
    }

    public static OrganizeResult Apply(IEnumerable<OrganizeAction> actions)
    {
        var moved = 0; var skipped = 0; var errors = new List<string>();
        foreach (var action in actions)
        {
            try
            {
                if (!File.Exists(action.Source)) { skipped++; continue; }
                Directory.CreateDirectory(Path.GetDirectoryName(action.Destination)!);
                if (File.Exists(action.Destination)) { skipped++; continue; }
                File.Move(action.Source, action.Destination);
                moved++;
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            { errors.Add($"{action.Source}: {ex.Message}"); }
        }
        return new OrganizeResult(moved, skipped, errors);
    }

    private static string UniquePath(string folder, string name, string source)
    {
        var candidate = Path.Combine(folder, name);
        if (string.Equals(Path.GetFullPath(candidate), Path.GetFullPath(source), StringComparison.OrdinalIgnoreCase) || !File.Exists(candidate)) return candidate;
        var stem = Path.GetFileNameWithoutExtension(name); var ext = Path.GetExtension(name); var i = 1;
        do candidate = Path.Combine(folder, $"{stem} ({i++}){ext}"); while (File.Exists(candidate));
        return candidate;
    }
}
