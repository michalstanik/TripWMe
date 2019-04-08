namespace TripWMe.Domain
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public string ColumnName { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
    }
}
