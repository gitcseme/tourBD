using System.Collections.Generic;

namespace tourBD.Web.Models
{
    public class LayoutBaseModel
    {
        public List<NotificationViewModel> UserNotifications { get; set; } = new List<NotificationViewModel>();
    }

    public class NotificationViewModel
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Time { get; set; }
    }
}
