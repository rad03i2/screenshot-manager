namespace ScreenshotManager;

public sealed record ScreenshotEntry(string Path, string Name, long Size, DateTime ModifiedUtc, string Extension);
public sealed record OrganizeAction(string Source, string Destination);
public sealed record OrganizeResult(int Moved, int Skipped, IReadOnlyList<string> Errors);
