using System;
using TripWMe.CoreHelpers.Attributes;

namespace TripWMe.Domain.Admin
{
    [Auditable("NON")]
    public class AuditLog
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public DateTime DateTime { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
    }
}
