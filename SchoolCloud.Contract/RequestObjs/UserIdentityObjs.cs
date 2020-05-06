using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolCloud.Contract.RequestObjs
{
    public class UserRegistrationReqObj
    {
        [EmailAddress]
        public string Email { get; set; } 
        public string Password { get; set; } 
    }

    public class BurserRegstrationReqObj
    {
        [EmailAddress]
        public string Email { get; set; }
        public string UserType { get; set; } 
    }

    public class ChangePassword
    {
        [EmailAddress]
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
     
    public class UserLoginReqObj
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserRefreshTokenReqObj
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }

    public class AuthSuccessResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; } 
    }

    public class ConfirnmationResponse
    {
        public string Email { get; set; }
        public bool IsFirstTimeLogin { get; set; }
    }

    public class ConfirnmationRequest
    {
        public string Email { get; set; }
    }

    public class UserDataRespone
    {
        public string UserId { get; set; }
        public string PlatformUserId { get; set; }
        public string Email { get; set; } 
        public string UserType { get; set; }
        public byte[] ProfileImage { get; set; }
        public IList<string> Roles { get; set; }
    }
}
