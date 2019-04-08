namespace TripWMe.Domain
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public int EntityId { get; set; }
        public string ColumnName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
