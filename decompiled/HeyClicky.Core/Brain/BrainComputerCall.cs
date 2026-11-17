using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001FE RID: 510
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BrainComputerCall : IEquatable<BrainComputerCall>
  {
    // Token: 0x06001D94 RID: 7572 RVA: 0x003E95A8 File Offset: 0x003E95A8
    public BrainComputerCall(string CallId, IReadOnlyList<JsonObject> Actions, IReadOnlyList<JsonObject> PendingSafetyChecks)
    {
      this.CallId = CallId;
      this.Actions = Actions;
      this.PendingSafetyChecks = PendingSafetyChecks;
      base..ctor();
    }

    // Token: 0x17000680 RID: 1664
    // (get) Token: 0x06001D95 RID: 7573 RVA: 0x003E95C8 File Offset: 0x003E95C8
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(BrainComputerCall);
      }
    }

    // Token: 0x17000681 RID: 1665
    // (get) Token: 0x06001D96 RID: 7574 RVA: 0x003E95D4 File Offset: 0x003E95D4
    // (set) Token: 0x06001D97 RID: 7575 RVA: 0x003E95DC File Offset: 0x003E95DC
    public string CallId { get; set; }

    // Token: 0x17000682 RID: 1666
    // (get) Token: 0x06001D98 RID: 7576 RVA: 0x003E95E8 File Offset: 0x003E95E8
    // (set) Token: 0x06001D99 RID: 7577 RVA: 0x003E95F0 File Offset: 0x003E95F0
    public IReadOnlyList<JsonObject> Actions { get; set; }

    // Token: 0x17000683 RID: 1667
    // (get) Token: 0x06001D9A RID: 7578 RVA: 0x003E95FC File Offset: 0x003E95FC
    // (set) Token: 0x06001D9B RID: 7579 RVA: 0x003E9604 File Offset: 0x003E9604
    public IReadOnlyList<JsonObject> PendingSafetyChecks { get; set; }

    // Token: 0x06001D9C RID: 7580 RVA: 0x003E9610 File Offset: 0x003E9610
    [NullableContext(2)]
    public static BrainComputerCall FromEvent(JsonElement call)
    {
      JsonElement jsonElement;
      if (call.ValueKind != JsonValueKind.Object || !call.TryGetProperty("call_id", out jsonElement) || jsonElement.ValueKind != JsonValueKind.String)
      {
        return null;
      }
      return new BrainComputerCall(jsonElement.GetString(), BrainComputerCall.<FromEvent>g__Objects|15_0(call, "actions"), BrainComputerCall.<FromEvent>g__Objects|15_0(call, "pending_safety_checks"));
    }

    // Token: 0x06001D9D RID: 7581 RVA: 0x003E9668 File Offset: 0x003E9668
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("BrainComputerCall");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001D9E RID: 7582 RVA: 0x003E96B4 File Offset: 0x003E96B4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("CallId = ");
      builder.Append(this.CallId);
      builder.Append(", Actions = ");
      builder.Append(this.Actions);
      builder.Append(", PendingSafetyChecks = ");
      builder.Append(this.PendingSafetyChecks);
      return true;
    }

    // Token: 0x06001D9F RID: 7583 RVA: 0x003E9714 File Offset: 0x003E9714
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(BrainComputerCall left, BrainComputerCall right)
    {
      return !(left == right);
    }

    // Token: 0x06001DA0 RID: 7584 RVA: 0x003E9720 File Offset: 0x003E9720
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(BrainComputerCall left, BrainComputerCall right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001DA1 RID: 7585 RVA: 0x003E9734 File Offset: 0x003E9734
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CallId>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<JsonObject>>.Default.GetHashCode(this.<Actions>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<JsonObject>>.Default.GetHashCode(this.<PendingSafetyChecks>k__BackingField);
    }

    // Token: 0x06001DA2 RID: 7586 RVA: 0x003E9798 File Offset: 0x003E9798
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as BrainComputerCall);
    }

    // Token: 0x06001DA3 RID: 7587 RVA: 0x003E97A8 File Offset: 0x003E97A8
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(BrainComputerCall other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<CallId>k__BackingField, other.<CallId>k__BackingField) && EqualityComparer<IReadOnlyList<JsonObject>>.Default.Equals(this.<Actions>k__BackingField, other.<Actions>k__BackingField) && EqualityComparer<IReadOnlyList<JsonObject>>.Default.Equals(this.<PendingSafetyChecks>k__BackingField, other.<PendingSafetyChecks>k__BackingField));
    }

    // Token: 0x06001DA5 RID: 7589 RVA: 0x003E9824 File Offset: 0x003E9824
    [CompilerGenerated]
    private BrainComputerCall(BrainComputerCall original)
    {
      this.CallId = original.<CallId>k__BackingField;
      this.Actions = original.<Actions>k__BackingField;
      this.PendingSafetyChecks = original.<PendingSafetyChecks>k__BackingField;
    }

    // Token: 0x06001DA6 RID: 7590 RVA: 0x003E9850 File Offset: 0x003E9850
    [CompilerGenerated]
    public void Deconstruct(out string CallId, out IReadOnlyList<JsonObject> Actions, out IReadOnlyList<JsonObject> PendingSafetyChecks)
    {
      CallId = this.CallId;
      Actions = this.Actions;
      PendingSafetyChecks = this.PendingSafetyChecks;
    }

    // Token: 0x06001DA7 RID: 7591 RVA: 0x003E986C File Offset: 0x003E986C
    [CompilerGenerated]
    internal static List<JsonObject> <FromEvent>g__Objects|15_0(JsonElement call, string name)
    {
      JsonElement jsonElement;
      if (!call.TryGetProperty(name, out jsonElement) || jsonElement.ValueKind != JsonValueKind.Array)
      {
        return new List<JsonObject>();
      }
      return (from e in jsonElement.EnumerateArray()
        where e.ValueKind == JsonValueKind.Object
        select (JsonObject)JsonNode.Parse(e.GetRawText(), null, default(JsonDocumentOptions))).ToList<JsonObject>();
    }
  }
}
