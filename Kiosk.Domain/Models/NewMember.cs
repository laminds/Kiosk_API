using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kiosk.Domain.Models
{
    public partial class NewMember : BaseEntity
    {
        public int NewMemberId { get; set; }
        
    }
}