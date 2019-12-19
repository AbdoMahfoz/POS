using System;
using System.Runtime.Serialization;

namespace Backend.DataContracts
{
    [DataContract]
    public class ItemRequest
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Price { get; set; }
        [DataMember]
        public string[] Categories { get; set; }
        [DataMember]
        public string Base64Image { get; set; }
    }
    [DataContract]
    public class ItemResult : ItemRequest
    {
        [DataMember]
        public int Id { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}