using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace YASop.AdminUI.Code
{
    public class AuthUserInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class AuthHelper
    {
        private static string SessionKey = "LoggedUser";
        public bool IsAuthenticated => Session[SessionKey] != null;

        private static HttpSessionState Session => HttpContext.Current.Session;

        public void LogOff()
        {
            Session[SessionKey] = null;
        }
        public void Authenticate(string name, Guid id)
        {
            Session[SessionKey] = name + "~" + id;
        }
        public AuthUserInfo CurrentUser()
        {
            var s = Session[SessionKey] as string;
            if (string.IsNullOrWhiteSpace(s)) HttpContext.Current.Response.Redirect("/Account/Login/");
            return new AuthUserInfo {Id = Guid.Parse(s.Split('~')[1]),Name = s.Split('~')[0]};
        }

    }
}