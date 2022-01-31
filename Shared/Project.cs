namespace Admiralfeb_dev.Shared;
public sealed class Project : DatabaseItem
{
    public Project(string id) : base(id) { }

    public string Name { get; set; } = "";

    public string Url { get; set; } = "";

    public string GitUrl { get; set; } = "https://github.com/admiralfeb";

    public string Description { get; set; } = "";

    public IEnumerable<string> Technologies { get; set; } = new List<string>();
}
