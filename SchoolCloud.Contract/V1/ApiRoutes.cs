using System;
using System.Collections.Generic;

namespace SchoolCloud.Contract.V1
{
    public class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;



        public static class Identity
        {
            public const string LOGIN = Base + "/identity/login";
            public const string REGISTER = Base + "/identity/register";
            public const string REFRESHTOKEN = Base + "/identity/refresh";
            public const string BURSER_REGISTRATION = Base + "/identity/burser/user/create";
            public const string FIRST_LOGIN_CHANGE_PASS = Base + "/identity/firstLogin/changePassword";
            public const string CHANGE_PASSWORD = Base + "/identity/changePassword";
            public const string USERPROFILE = Base + "/identity/profile";
            public const string CONFIRM_EMAIL = Base + "/identity/confirm/email";
        }

        public static class Staff
        {
            public const string SEARCH = Base + "/staff/search";
        }

    }
}
