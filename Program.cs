using ScreenshotManager;

static int Usage()
{
    Console.WriteLine("Screenshot Manager 1.0.0 — Radwan Abdulhadi Ahmed / @rad03i2");
    Console.WriteLine("Usage:\n  screenshot-manager scan <folder> [--recursive] [--json file]\n  screenshot-manager search <folder> <query> [--recursive]\n  screenshot-manager organize <folder> <destination> [--recursive] [--apply]");
    return 2;
}

try
{
    if (args.Length < 2 || args[0] is "--help" or "-h") return Usage();
    var command = args[0].ToLowerInvariant(); var folder = args[1];
    var recursive = args.Contains("--recursive");
    var entries = ScreenshotCatalog.Scan(folder, recursive);
    if (command == "scan")
    {
        foreach (var e in entries) Console.WriteLine($"{e.ModifiedUtc:u}  {e.Size,12:N0}  {e.Name}");
        var ji = Array.IndexOf(args, "--json");
        if (ji >= 0) { if (ji + 1 >= args.Length) return Usage(); ScreenshotCatalog.ExportJson(entries, args[ji + 1]); }
        Console.WriteLine($"\n{entries.Count} image(s), {entries.Sum(e => e.Size):N0} bytes."); return 0;
    }
    if (command == "search")
    {
        if (args.Length < 3) return Usage(); var found = ScreenshotCatalog.Search(entries, args[2]);
        foreach (var e in found) Console.WriteLine(e.Path); Console.WriteLine($"{found.Count} match(es)."); return 0;
    }
    if (command == "organize")
    {
        if (args.Length < 3) return Usage(); var plan = Organizer.Plan(entries, args[2]);
        foreach (var a in plan) Console.WriteLine($"{a.Source} -> {a.Destination}");
        if (!args.Contains("--apply")) { Console.WriteLine($"\nPreview only: {plan.Count} move(s). Add --apply to execute."); return 0; }
        var result = Organizer.Apply(plan); Console.WriteLine($"Moved: {result.Moved}; skipped: {result.Skipped}; errors: {result.Errors.Count}");
        foreach (var error in result.Errors) Console.Error.WriteLine(error); return result.Errors.Count == 0 ? 0 : 1;
    }
    return Usage();
}
catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException)
{ Console.Error.WriteLine($"Error: {ex.Message}"); return 1; }
