using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Admiralfeb_dev.Shared;
public class Project
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("url")]
    public string Url { get; set; }

    [BsonElement("gitUrl")]
    public string GitUrl { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("technologies")]
    public List<string> Technologies { get; set; }
}
