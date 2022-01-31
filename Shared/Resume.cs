using System.Text.Json;

namespace Admiralfeb_dev.Shared;
public sealed class Resume
{
    public Resume(IEnumerable<Experience> experiences, IEnumerable<SkillItem> skills)
    {
        this.Experiences = experiences;
        this.Skills = skills;
    }

    public IEnumerable<Experience> Experiences { get; set; }
    public IEnumerable<SkillItem> Skills { get; set; }

    public sealed override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
public class Experience : DatabaseItem
{
    public Experience(string id) : base(id) { }
    public string Title { get; set; } = "";
    public string Place { get; set; } = "";
    public string StartDate { get; set; } = "";
    public string EndDate { get; set; } = "Current";
    public string Description { get; set; } = "";
    public IEnumerable<string> Responsibilities { get; set; } = new List<string>();
    public int DisplayOrder { get; set; }
}

public class SkillItem : DatabaseItem
{
    public SkillItem(string id) : base(id) { }
    public string Skill { get; set; } = "";

    public List<string>? Ul { get; set; }
}
