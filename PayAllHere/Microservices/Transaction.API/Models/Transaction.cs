using MongoDB.Bson.Serialization.Attributes;

namespace Transaction.API.Models
{
    public class Transaction
    {
        [BsonId]
        public string Id { get; set; }

        public double Value { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string UserId { get; set; }

        public string TransactionId { get; set; }

        public bool Validated { get; set; }
    }
}
