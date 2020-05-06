using Microsoft.AspNetCore.Http;
using SchoolCloud.Contract.RequestObjs;
using SchoolCloud.DomainObjects;
using System;
using System.Threading.Tasks;

namespace SchoolCloud.Repository.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(UserRegistrationReqObj userRegistration);
        Task<AuthenticationResult> BurserRegistrationAsync(string email, string userType);
        Task<AuthenticationResult> FirstTimeLoginChangePasswsord(ChangePassword pass);
        Task<AuthenticationResult> ChangePasswsord(ChangePassword pass);
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string refreshToken, string token);
        Task<bool> CheckUserAsync(string email);
        Task<UserDataRespone> FetchLoggedInUserDetails(string userId);
    }
}
