using ScreenshotManager;

public sealed class CoreTests : IDisposable
{
    private readonly string _root = Path.Combine(Path.GetTempPath(), "screenshot-manager-" + Guid.NewGuid());
    public CoreTests() => Directory.CreateDirectory(_root);
    public void Dispose() => Directory.Delete(_root, true);

    [Fact] public void Scan_FiltersNonImages()
    {
        File.WriteAllText(Path.Combine(_root, "shot.png"), "x"); File.WriteAllText(Path.Combine(_root, "note.txt"), "x");
        var result = ScreenshotCatalog.Scan(_root); Assert.Single(result); Assert.Equal("shot.png", result[0].Name);
    }
    [Fact] public void Search_IsCaseInsensitive()
    {
        File.WriteAllText(Path.Combine(_root, "Meeting.PNG"), "x");
        Assert.Single(ScreenshotCatalog.Search(ScreenshotCatalog.Scan(_root), "meeting"));
    }
    [Fact] public void Organize_PreviewDoesNotMove_ApplyDoes()
    {
        var source = Path.Combine(_root, "shot.png"); File.WriteAllText(source, "x"); var dest = Path.Combine(_root, "organized");
        var plan = Organizer.Plan(ScreenshotCatalog.Scan(_root), dest); Assert.True(File.Exists(source)); Assert.Single(plan);
        var result = Organizer.Apply(plan); Assert.Equal(1, result.Moved); Assert.False(File.Exists(source)); Assert.True(File.Exists(plan[0].Destination));
    }
    [Fact] public void RecursiveScan_IsOptional()
    {
        var nested = Path.Combine(_root, "nested"); Directory.CreateDirectory(nested); File.WriteAllText(Path.Combine(nested, "x.jpg"), "x");
        Assert.Empty(ScreenshotCatalog.Scan(_root)); Assert.Single(ScreenshotCatalog.Scan(_root, true));
    }
}
