using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Domain.Entities
{
    public class Notifications : BaseEntity
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime ReadDate { get; set; }
        public Guid UserID { get; set; }
        [ForeignKey("UserID")]
        public AppUser AppUser { get; set; }
        public bool IsRead { get; set; }
        public NotificationType NotificationType { get; set; }
    }

    public enum NotificationType
    {
        GidenPara = 0,
        GelenPara = 1,
        KrediSonuc = 2,
        OdemeBildirim = 3
    }
}
