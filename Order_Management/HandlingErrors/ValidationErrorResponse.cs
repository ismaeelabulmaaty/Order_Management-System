namespace Order_Management.HandlingErrors
{
    public class ValidationErrorResponse : ApisResponse
    {


        public IEnumerable<string> Errors { get; set; }
        public ValidationErrorResponse() : base(400)
        {

            Errors = new List<string>();
        }
    }
}
