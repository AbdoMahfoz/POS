using Backend.DataContracts;
using Backend.Security.Interfaces;
using System;
using System.ServiceModel;

namespace Backend.Services
{
    [ServiceBehavior]
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuth Auth;
        private readonly ITokenizer Tokenizer;
        public AuthenticationService(IAuth Auth, ITokenizer Tokenizer)
        {
            this.Tokenizer = Tokenizer;
            this.Auth = Auth;
        }
        public bool IsAdmin(string token)
        {
            try
            {
                Auth.EnsureAuthorizedAsAdmin(token);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public string Login(string email, string password)
        {
            return Auth.Login(email, password);
        }
        public string RefreshToken(string token)
        {
            var res = Tokenizer.DeTokenize(token);
            if (res == null) throw new FaultException("Bad token");
            return Tokenizer.Tokenize(res);
        }
        public string Register(UserDataRequest userData)
        {
            Auth.Register(userData, false);
            return Login(userData.Email, userData.Password);
        }
        public string RegisterAdmin(string adminToken, UserDataRequest adminData)
        {
            Auth.EnsureAuthorizedAsAdmin(adminToken);
            Auth.Register(adminData, true);
            return Login(adminData.Email, adminData.Password);
        }
    }
}
