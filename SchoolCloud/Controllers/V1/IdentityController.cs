using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SchoolCloud.Contract.RequestObjs;
using SchoolCloud.Contract.V1;
using SchoolCloud.Data;
using SchoolCloud.DomainObjects;
using SchoolCloud.Repository.Services;

namespace Libraryhub.Controllers.V1
{
    
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        { 
            _identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.REGISTER)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationReqObj regRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new AuthFailedResponse
                    {
                        Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                    });
                }
                var authResponse = await _identityService.RegisterAsync(regRequest);
                if (!authResponse.Success)
                {
                    return BadRequest(new AuthFailedResponse
                    {
                        Errors = authResponse.Errors
                    });
                }

                return Ok(new AuthSuccessResponse
                {
                    Token = authResponse.Token,
                    RefreshToken = authResponse.RefreshToken
                });
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        [HttpPost(ApiRoutes.Identity.LOGIN)]
        public async Task<IActionResult> Login([FromBody] UserLoginReqObj request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);
            if (!authResponse.Success && !authResponse.IsFirstLogin)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }
            if (authResponse.IsFirstLogin)
            {
                return Ok(new ConfirnmationResponse
                {
                    IsFirstTimeLogin = true,
                    Email = authResponse.Email
                });
            }
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        [HttpPost(ApiRoutes.Identity.REFRESHTOKEN)]
        public async Task<IActionResult> Refresh([FromBody] UserRefreshTokenReqObj request)
        {

            var authResponse = await _identityService.RefreshTokenAsync(request.RefreshToken, request.Token);
            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        [HttpPost(ApiRoutes.Identity.BURSER_REGISTRATION)]
        public async Task<IActionResult> BurserRegistration([FromBody] BurserRegstrationReqObj request)
        { 

            var authResponse = await _identityService.BurserRegistrationAsync(request.Email, request.UserType);
             
            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        [HttpPost(ApiRoutes.Identity.FIRST_LOGIN_CHANGE_PASS)]
        public async Task<IActionResult> FirstTimeLogin([FromBody] ChangePassword request)
        { 

            if (!ModelState.IsValid)
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });

            var authResponse = await _identityService.FirstTimeLoginChangePasswsord(request);
            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        [HttpPost(ApiRoutes.Identity.CHANGE_PASSWORD)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword request)
        {

            if (request.Email.Length < 1)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = new[] { "Email Required" }
                });
            }

            if (request.OldPassword.Length < 1)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = new[] { "Old Password Required" }
                });
            }

            if (request.NewPassword.Length < 1)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = new[] { "New Password Required" }
                });
            }

            var authResponse = await _identityService.ChangePasswsord(request);
            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        [HttpPost(ApiRoutes.Identity.CONFIRM_EMAIL)]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirnmationRequest request)
        {

            if (request.Email.Length < 1)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = new[] { "Email Required change password" }
                });
            }
             

            var userExist = await _identityService.CheckUserAsync(request.Email);
            if (!userExist)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = new[] { "Email not found in school datatbase" }
                }) ;
            }
            return Ok(new ConfirnmationResponse
            {
                Email = request.Email
            });
        }

        public string token { get; set; }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(ApiRoutes.Identity.USERPROFILE)]
        public async Task<ActionResult<UserDataRespone>> GetUserProfile()
        {
            string userId = HttpContext.User?.FindFirst(c => c.Type == "id").Value;

            var profile = await _identityService.FetchLoggedInUserDetails(userId);
            if(profile != null)
            {
                return Ok(profile);
            }
            return BadRequest("Unable to fetch details");
            
        }
    }
}