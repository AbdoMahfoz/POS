using System.Runtime.Serialization;

namespace Backend.DataContracts
{
    [DataContract]
    public class UserDataResponse
    {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Area { get; set; }
    }
    [DataContract]
    public class UserDataRequest : UserDataResponse
    {
        [DataMember]
        public string Password { get; set; }
    }
}