using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourBD.Core.Seeds;
using tourBD.NotificationChannel.Contexts;

namespace tourBD.NotificationChannel.Seeds
{
    public class NotificationSeed : DataSeed
    {
        public NotificationSeed(NotificationContext notificationContext) 
            : base(notificationContext)
        {
        }

        public override Task SeedAsync()
        {
            throw new NotImplementedException();
        }
    }
}
