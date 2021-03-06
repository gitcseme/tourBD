﻿using System;
using System.ComponentModel.DataAnnotations;
using tourBD.Core;

namespace tourBD.NotificationChannel.Entities
{
    public class Notification : EntityBase<Guid>
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string NotifierImageUrl { get; set; }
        
        [StringLength(100)]
        public string Message { get; set; }
        
        [StringLength(100)]
        public string SourceLink { get; set; }

        public bool Seen { get; set; }
    }
}
