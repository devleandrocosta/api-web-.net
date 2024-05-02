using MongoDB.Bson.Serialization.Attributes;

namespace APImongo.Entities
{
    public class Pessoa
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nome"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string? Nome { get; set; }

        [BsonElement("sexo"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string? Sexo { get; set; }

        [BsonElement("telefone"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string? Telefone { get; set; }
    }
}
