using System.Text.Json;
using GSM.Shared.Setup.Audits.Entities;
using GSM.Shared.Setup.Audits.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GSM.Shared.Setup.Audits.Entries;

public class AuditEntry
{
    public AuditEntry(EntityEntry entry)
    {
        Entry = entry;
        TableName = string.Empty;
        UserId = string.Empty;
        KeyValues = new Dictionary<string, object?>();
        OldValues = new Dictionary<string, object?>();
        NewValues = new Dictionary<string, object?>();
    }

    public EntityEntry Entry { get; }
    public string UserId { get; set; }
    public string TableName { get; set; }
    public Dictionary<string, object?> KeyValues { get; }
    public Dictionary<string, object?> OldValues { get; }
    public Dictionary<string, object?> NewValues { get; }
    public AuditType AuditType { get; set; }
    public List<string> ChangedColumns { get; } = new List<string>();
    
    public Audit ToAudit()
    {
        var audit = new Audit(
            userId: UserId,
            type: AuditType.ToString(),
            tableName: TableName,
            dateTime: DateTime.Now,
            primaryKey: JsonSerializer.Serialize(KeyValues),
            oldValues: OldValues.Count == 0 ? null : JsonSerializer.Serialize(OldValues),
            newValues: NewValues.Count == 0 ? null : JsonSerializer.Serialize(NewValues),
            affectedColumns: ChangedColumns.Count == 0 ? null : JsonSerializer.Serialize(ChangedColumns)
        );
        return audit;
    }
}