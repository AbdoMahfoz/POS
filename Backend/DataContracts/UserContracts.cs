using System.Runtime.Serialization;

namespace Backend.DataContracts
{
    [DataContract]
    public enum PurchaseMethod { CreditCard, Cash }
    [DataContract]
    public class ItemResult
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Price { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string Base64Image { get; set; }
    }
}