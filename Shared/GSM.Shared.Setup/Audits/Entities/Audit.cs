namespace GSM.Shared.Setup.Audits.Entities;

public class Audit
{
    public Audit(string userId, string type, string tableName, string oldValues, string newValues, string affectedColumns, string primaryKey)
    {
        UserId = userId;
        Type = type;
        TableName = tableName;
        OldValues = oldValues;
        NewValues = newValues;
        AffectedColumns = affectedColumns;
        PrimaryKey = primaryKey;
    }
    
    public Audit(string userId, string type, string tableName, DateTime dateTime, string? oldValues, string? newValues, string? affectedColumns, string primaryKey)
    {
        UserId = userId;
        Type = type;
        TableName = tableName;
        OldValues = oldValues;
        NewValues = newValues;
        AffectedColumns = affectedColumns;
        PrimaryKey = primaryKey;
        DateTime = dateTime;
    }

    public int Id { get; set; }
    public string UserId { get; set; }
    public string Type { get; set; }
    public string TableName { get; set; }
    public DateTime DateTime { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? AffectedColumns { get; set; }
    public string PrimaryKey { get; set; }
}