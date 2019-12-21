﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AuthenticationService
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserDataResponse", Namespace="http://schemas.datacontract.org/2004/07/Backend.DataContracts")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AuthenticationService.UserDataRequest))]
    public partial class UserDataResponse : object
    {
        
        private string AddressField;
        
        private string AreaField;
        
        private string EmailField;
        
        private string NameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address
        {
            get
            {
                return this.AddressField;
            }
            set
            {
                this.AddressField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Area
        {
            get
            {
                return this.AreaField;
            }
            set
            {
                this.AreaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email
        {
            get
            {
                return this.EmailField;
            }
            set
            {
                this.EmailField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserDataRequest", Namespace="http://schemas.datacontract.org/2004/07/Backend.DataContracts")]
    public partial class UserDataRequest : AuthenticationService.UserDataResponse
    {
        
        private string PasswordField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password
        {
            get
            {
                return this.PasswordField;
            }
            set
            {
                this.PasswordField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AuthenticationService.IAuthenticationService")]
    public interface IAuthenticationService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthenticationService/Login", ReplyAction="http://tempuri.org/IAuthenticationService/LoginResponse")]
        System.Threading.Tasks.Task<string> LoginAsync(string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthenticationService/Register", ReplyAction="http://tempuri.org/IAuthenticationService/RegisterResponse")]
        System.Threading.Tasks.Task<string> RegisterAsync(AuthenticationService.UserDataRequest userData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthenticationService/RegisterAdmin", ReplyAction="http://tempuri.org/IAuthenticationService/RegisterAdminResponse")]
        System.Threading.Tasks.Task<string> RegisterAdminAsync(string adminToken, AuthenticationService.UserDataRequest adminData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthenticationService/RefreshToken", ReplyAction="http://tempuri.org/IAuthenticationService/RefreshTokenResponse")]
        System.Threading.Tasks.Task<string> RefreshTokenAsync(string token);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1")]
    public interface IAuthenticationServiceChannel : AuthenticationService.IAuthenticationService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1")]
    public partial class AuthenticationServiceClient : System.ServiceModel.ClientBase<AuthenticationService.IAuthenticationService>, AuthenticationService.IAuthenticationService
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public AuthenticationServiceClient() : 
                base(AuthenticationServiceClient.GetDefaultBinding(), AuthenticationServiceClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_IAuthenticationService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AuthenticationServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(AuthenticationServiceClient.GetBindingForEndpoint(endpointConfiguration), AuthenticationServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AuthenticationServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(AuthenticationServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AuthenticationServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(AuthenticationServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AuthenticationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<string> LoginAsync(string email, string password)
        {
            return base.Channel.LoginAsync(email, password);
        }
        
        public System.Threading.Tasks.Task<string> RegisterAsync(AuthenticationService.UserDataRequest userData)
        {
            return base.Channel.RegisterAsync(userData);
        }
        
        public System.Threading.Tasks.Task<string> RegisterAdminAsync(string adminToken, AuthenticationService.UserDataRequest adminData)
        {
            return base.Channel.RegisterAdminAsync(adminToken, adminData);
        }
        
        public System.Threading.Tasks.Task<string> RefreshTokenAsync(string token)
        {
            return base.Channel.RefreshTokenAsync(token);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IAuthenticationService))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IAuthenticationService))
            {
                return new System.ServiceModel.EndpointAddress("http://0.0.0.0:8082/svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return AuthenticationServiceClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IAuthenticationService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return AuthenticationServiceClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IAuthenticationService);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_IAuthenticationService,
        }
    }
}