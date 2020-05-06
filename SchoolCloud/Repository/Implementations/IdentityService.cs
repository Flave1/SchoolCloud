using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;  
using SchoolCloud.Repository.Services;
using SchoolCloud.DomainObjects;
using SchoolCloud.Options;
using SchoolCloud.Data;
using SchoolCloud.Contract.RequestObjs;
using SchoolCloud.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SchoolCloud.ErrorHandler;

namespace SchoolCloud.Repository.Implementations
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly DataContext _dataContext;
        private readonly RoleManager<IdentityRole> _roleManager; 
        private readonly IStudentService _studentService;
        private readonly ILogger _logger;
        private readonly IStaffService _staffService;
        public IdentityService(UserManager<ApplicationUser> userManager, JwtSettings jwtSettings, TokenValidationParameters tokenValidationParameters,
            DataContext dataContext, RoleManager<IdentityRole> roleManager, IStudentService studentService,
            ILoggerFactory loggerFactory, IStaffService staffService)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            _dataContext = dataContext;
            _roleManager = roleManager;
            _studentService = studentService;
            _staffService = staffService;
            _logger = loggerFactory.CreateLogger(typeof(IdentityService));
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User Does Not Exist" }
                };
            }
           

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User/Password Combination is wrong" }
                };
            }

            if (user.IsFirstTimeLogin)
            {
                return new AuthenticationResult
                {
                    IsFirstLogin = true,
                    Email = email
                };
            }

            return await GenerateAuthenticationResultForUserAsync(user);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string refreshToken, string token)
        {

            var validatedToken = GetClaimsPrincipalFromToken(token);
            if (validatedToken == null)
                return new AuthenticationResult { Errors = new[] { "Invalid Token" } };

            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                 .AddDays(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
                return new AuthenticationResult { Errors = new[] { "This Token Hasn't Expired Yet" } };

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value;

            var storedRefreshToken = _dataContext.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken);

            if (storedRefreshToken == null)
                return new AuthenticationResult { Errors = new[] { "This Refresh Token does not Exist" } };

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
                return new AuthenticationResult { Errors = new[] { "This Refresh Token has Expired" } };

            if (storedRefreshToken.Invalidated)
                return new AuthenticationResult { Errors = new[] { "This Refresh Token has been Invalidated" } };

            if (storedRefreshToken.Used)
                return new AuthenticationResult { Errors = new[] { "This Refresh Token has been Used" } };

            if (storedRefreshToken.JwtId != jti)
                return new AuthenticationResult { Errors = new[] { "This Refresh Token Does not match this JWT" } };

            storedRefreshToken.Used = true;
            _dataContext.Update(storedRefreshToken);
            await _dataContext.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.SingleOrDefault(x => x.Type == "id").Value);

            return await GenerateAuthenticationResultForUserAsync(user);
        }

        private ClaimsPrincipal GetClaimsPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                    return null;
                else
                    return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return validatedToken is JwtSecurityToken jwtSecurityToken &&
                            jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                            StringComparison.InvariantCultureIgnoreCase);
        }
        public async Task<AuthenticationResult> RegisterAsync(UserRegistrationReqObj userRegistration)
        {
            try
            {
                var existingUser = await _userManager.FindByEmailAsync(userRegistration.Email);

                if (existingUser != null)
                {
                    return new AuthenticationResult
                    {
                        Errors = new[] { "User with this email address already exist" }
                    };
                }

                var user = new ApplicationUser
                {
                    Email = userRegistration.Email,
                    UserName = userRegistration.Email,
                };

                var createdUser = await _userManager.CreateAsync(user, userRegistration.Password);


                if (!createdUser.Succeeded)
                {
                    return new AuthenticationResult
                    {
                        Errors = createdUser.Errors.Select(x => x.Description)
                    };
                }

                return await GenerateAuthenticationResultForUserAsync(user);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id)
            };

            var userClaims = await _userManager.GetClaimsAsync(user);

            claims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole) ?? null);

                var role = await _roleManager.FindByNameAsync(userRole);

                if (role == null)
                {
                    continue;
                }
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                foreach (var roleClaim in roleClaims)
                {
                    if (claims.Contains(roleClaim)) continue;
                    claims.Add(roleClaim);
                }
            }

            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifeSpan),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };


            var token = tokenHandler.CreateToken(tokenDecriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddSeconds(6),
            };

            try
            {
                await _dataContext.RefreshTokens.AddAsync(refreshToken);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { $"Something went wrong: {ex.InnerException.Message}" }
                };
            }

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token,
            };


        }

        public async Task<AuthenticationResult> BurserRegistrationAsync(string email, string usertType)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this email address already exist" }
                };
            }

            var user = new ApplicationUser
            {
                Email = email,
                UserName = email,
                IsFirstTimeLogin = true,
                UserType = (UserType)Convert.ToInt16(usertType)
            };


            IdentityResult createdUser = new IdentityResult();
            Student student = new Student();
            Staff staff = new Staff();
            using (var transaction = new DataContext().Database.BeginTransaction())
            { 
                try
                { 
                    createdUser = await _userManager.CreateAsync(user, "000@Pass"); 
                    if (usertType.ToString().ToLower().Trim() == UserType.Student.ToString().ToLower().Trim())
                    { 
                        student.Email = email;
                        student.Status = StudentStatus.InActive;
                        await _studentService.AddStudentAsync(student);
                        await _userManager.AddToRoleAsync(user, DefaultRoles.Student.ToString());
                    }
                    else
                    {
                        staff.Email = email;
                        staff.Status = StaffStatus.InActive;
                        await _userManager.AddToRoleAsync(user, DefaultRoles.Teacher.ToString());
                    }  
                    await transaction.CommitAsync(); 
                }
                catch (SqlException ex) 
                { 
                    await transaction.RollbackAsync();
                    var errorId = ErrorID.Generate(4);
                    _logger.LogInformation($"Unabe tp porocess request ErrorId : {errorId} Exceoption : {ex?.Message ?? ex?.InnerException?.Message}");
                }
                finally { await transaction.DisposeAsync(); }
            }

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            return await GenerateAuthenticationResultForUserAsync(user);
        }

        public async Task<AuthenticationResult> FirstTimeLoginChangePasswsord(ChangePassword pass)
        { 
           var user = await _userManager.FindByEmailAsync(pass.Email);

           if(pass.NewPassword.Trim().ToLower() == "000@Pass".Trim().ToLower())
            {
                return new AuthenticationResult
                { 
                    Errors = new[] { "Password has to be changed to a new one" }
                };
            }

            var userPassword = await _userManager.CheckPasswordAsync(user, pass.OldPassword);

            if (!userPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This password is not associated to this account" }
                };
            }

             
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
           
            var changepassword = await _userManager.ResetPasswordAsync(user, token, pass.NewPassword);

            if (!changepassword.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = changepassword.Errors.Select(x => x.Description)
                };
            }
            user.IsFirstTimeLogin = false;
            var updated = await _userManager.UpdateAsync(user);
            if (!updated.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = updated.Errors.Select(x => x.Description)
                };
            }
            return await GenerateAuthenticationResultForUserAsync(user);
        }

        public async Task<AuthenticationResult> ChangePasswsord(ChangePassword pass)
        {
            var user = await _userManager.FindByEmailAsync(pass.Email);
             
            var userPassword = await _userManager.CheckPasswordAsync(user, pass.OldPassword);

            if (!userPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This password is not associated to this account" }
                };
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var changepassword = await _userManager.ResetPasswordAsync(user, token, pass.NewPassword);

            if (!changepassword.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = changepassword.Errors.Select(x => x.Description)
                };
            }

            return await GenerateAuthenticationResultForUserAsync(user);
        }

        public async Task<bool> CheckUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null) return true;
            return false;
        }

        public async Task<UserDataRespone> FetchLoggedInUserDetails(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                var userRoles = await _userManager?.GetRolesAsync(user);
                var userdata = new UserDataRespone
                {
                    Email = user.Email,
                    UserId = user.Id,
                    UserType = user?.UserType.ToString() ?? user?.UserType.ToString(),
                    Roles = userRoles
                };
                return userdata;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
