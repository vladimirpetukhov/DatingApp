using Microsoft.AspNetCore.Http;

namespace DateApp.API.Helpers {
    public static class Extensions {
        public static void AddAppError (this HttpResponse response, string message) 
        {
            response.Headers.Add("ApplicationError",message);
            response.Headers.Add("Access-Control-Expose-Headers","Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin","*");
        }
    }
}