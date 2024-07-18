namespace Order_Management.HandlingErrors
{
    public class ApisResponse
    {

        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApisResponse( int statusCode , string? message = null)
        {
          
            StatusCode = statusCode ;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);

        }


        private string? GetDefaultMessageForStatusCode(int statuscode)
        {
            return statuscode switch
            {
                400 => "Bad Request",
                401 => " You Are not Authrized",
                404 => " Resource Not Found",
                500 => " Internal Server Error",
                _ => null
            };
        }

    }
}
