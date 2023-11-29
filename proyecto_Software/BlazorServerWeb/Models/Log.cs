using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace BlazorServerWeb.Models
{
    public class Log
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonElement("_id")]
        public string Id_Log { get; set; } = Guid.NewGuid().ToString();

        [BsonElement("num_doc"), BsonRepresentation(BsonType.String)]
        public string Num_Doc { get; set; } = null!;

        [BsonElement("tipo_transaccion"), BsonRepresentation(BsonType.String)]
        public string Tipo_Transaccion { get; set; } = null!;

        [BsonElement("fecha"), BsonRepresentation(BsonType.DateTime)]
        public DateTime Fecha { get; set; }
    }
}
