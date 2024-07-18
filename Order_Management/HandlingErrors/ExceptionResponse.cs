namespace Order_Management.HandlingErrors
{
    public class ExceptionResponse : ApisResponse
    {

        public string? Details { get; set; }

        public ExceptionResponse(int statuscode, string? message = null, string? details = null) : base(statuscode, message)
        {

            Details = details;
        }
    }
}
