﻿using Backend.DataContracts;
using Backend.Security.Interfaces;
using System.ServiceModel;

namespace Backend.Services
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface ITestService : IAuthenticatedService
    {
        [OperationContract]
        string TestAuthentication();
        [OperationContract]
        UserDataResponse GetCurrentUser();
    }
}
