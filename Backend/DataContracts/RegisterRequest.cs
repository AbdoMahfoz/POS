using System.Runtime.Serialization;

namespace Backend.DataContracts
{
    [DataContract]
    public class RegisterRequest
    {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Area { get; set; }
    }
}