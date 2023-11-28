using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Log_Transacciones.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class Log
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id_Log { get; set; } = null!;

        [BsonElement("num_doc"), BsonRepresentation(BsonType.String)]
        public string Num_Doc { get; set; } = null!;

        [BsonElement("tipo_transaccion"), BsonRepresentation(BsonType.String)]
        public string Tipo_Transaccion { get; set; } = null!;

        [BsonElement("fecha"), BsonRepresentation(BsonType.DateTime)]
        public DateTime Fecha { get; set; }
    }
}
