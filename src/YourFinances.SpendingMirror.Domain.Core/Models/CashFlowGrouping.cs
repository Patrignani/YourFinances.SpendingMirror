using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace YourFinances.SpendingMirror.Domain.Core.Models
{
    public class CashFlowGrouping
    {
        [BsonId()]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonElement("active")]
        public bool Active { get; set; }
        [BsonElement("identification")]
        public string Identification { get; set; }
        [BsonElement("accountId")]
        public int AccountId { get; set; }
        [BsonElement("typeBox")]
        public int TypeBox { get; set; }
        [BsonElement("editionUserId")]
        public int EditionUserId { get; set; }
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; }
        [BsonElement("__v")]
        public int Version { get; set; }


    }
}
