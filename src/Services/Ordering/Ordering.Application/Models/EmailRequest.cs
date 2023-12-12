
namespace Ordering.Application.Models
{
    public class EmailRequest
    {
        public string Body { get; set; }
        public string Subject { get; set; }
        public string ToEmail { get; set; }
    }
}
