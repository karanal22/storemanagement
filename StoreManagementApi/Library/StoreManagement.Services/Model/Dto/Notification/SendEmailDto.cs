using System.Collections.Generic;

namespace StoreManagement.Services.Model.Dto.Notification
{
    public class SendEmailDto
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Tos { get; set; }
        public string ReplyTo { get; set; }
        public List<string> Cc { get; set; }
    }
}
