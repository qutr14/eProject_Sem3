namespace DataAPI.Models
{
    public class ResultInfo
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
    public class AdminResult
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
