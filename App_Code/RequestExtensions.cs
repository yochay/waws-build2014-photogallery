using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.Data;


public static class RequestExtensions
{
    public static string GetCurrentUser(this HttpRequestBase request, HttpResponseBase response = null)
    {
        string userName;
        try
        {
            if (request.Cookies["GuidUser"] != null)
            {
                userName = request.Cookies["GuidUser"].Value;
                var db = Database.Open("PhotoGallery");
                var guidUser = db.QuerySingle("SELECT * FROM GuidUsers WHERE UserName = @0", userName);
                if (guidUser == null || guidUser.TotalLikes > 5)
                {
                    userName = CreateNewUser();
                }
            }
            else
            {
                userName = CreateNewUser();
            }
        }
        catch (Exception)
        {
            userName = CreateNewUser();
        }
        if (response != null)
            response.AddUser(userName);
        return userName;
    }

    private static string CreateNewUser()
    {
        var newUser = Guid.NewGuid();
        var db = Database.Open("PhotoGallery");
        db.Execute(@"INSERT INTO GuidUsers (UserName, TotalLikes) VALUES (@0, @1)", newUser.ToString(), 0);
        return newUser.ToString();
    }
}