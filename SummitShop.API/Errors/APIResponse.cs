
namespace SummitShop.API.Errors
{
    public class APIResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public APIResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request, You have made.",
                401 => "Unauthorized. Access is denied.",
                403 => "Forbidden. You don't have permission to access this resource.",
                404 => "Not Found. The resource you are looking for could not be found.",
                500 => "Internal Server Error. Something went wrong on our end.",
                502 => "Bad Gateway. Invalid response received from upstream server.",
                503 => "Service Unavailable. The server is currently unavailable.",
                504 => "Gateway Timeout. The server did not receive a timely response.",
                _ => null
            };
        }
    }
}
