using System.ComponentModel.DataAnnotations.Schema;

namespace PassIn.Infrastructure.Entities
{
    public class Checkin
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Created_At { get; set; }
        public Guid Attendee_Id { get; set; }
        [ForeignKey("Attendee_Id")]
        public Attendee Attendee { get; set; } = default;
    }
}
