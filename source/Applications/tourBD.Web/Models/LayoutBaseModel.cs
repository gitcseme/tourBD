using System;
using System.Collections.Generic;

namespace tourBD.Web.Models
{
    public class LayoutBaseModel
    {
        public int NewNotifications { get; set; }
        public List<NotificationViewModel> UserNotifications { get; set; } = new List<NotificationViewModel>();
    }

    public class NotificationViewModel
    {
        public Guid NotificationId { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string Time { get; set; }
        public string SourceLink { get; set; }
        public bool IsSeen { get; set; }
    }
}
