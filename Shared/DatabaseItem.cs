using System.Text.Json;

namespace Admiralfeb_dev.Shared;

public abstract class DatabaseItem
{
    public string Id { get; set; } = "";

    protected DatabaseItem(string id)
    {
        this.Id = id;
    }

    public sealed override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
