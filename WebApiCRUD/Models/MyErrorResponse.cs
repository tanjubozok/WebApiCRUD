namespace WebApiCRUD.Models
{
    public class MyErrorResponse
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}