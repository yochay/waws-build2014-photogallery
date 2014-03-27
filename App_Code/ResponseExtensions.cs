using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public static class ResponseExtensions
{
    public static void AddUser(this HttpResponseBase response, string userName)
    {
        var userCookie = new HttpCookie("GuidUser")
        {
            Value = userName,
            Expires = DateTime.UtcNow.AddYears(1)
        };
        response.Cookies.Add(userCookie);
    }
}