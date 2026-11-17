using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001FD RID: 509
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BrainToolCall : IEquatable<BrainToolCall>
  {
    // Token: 0x06001D7F RID: 7551 RVA: 0x003E9204 File Offset: 0x003E9204
    public BrainToolCall(string CallId, string Name, string Arguments, bool IsAsync)
    {
      this.CallId = CallId;
      this.Name = Name;
      this.Arguments = Arguments;
      this.IsAsync = IsAsync;
      base..ctor();
    }

    // Token: 0x1700067B RID: 1659
    // (get) Token: 0x06001D80 RID: 7552 RVA: 0x003E922C File Offset: 0x003E922C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(BrainToolCall);
      }
    }

    // Token: 0x1700067C RID: 1660
    // (get) Token: 0x06001D81 RID: 7553 RVA: 0x003E9238 File Offset: 0x003E9238
    // (set) Token: 0x06001D82 RID: 7554 RVA: 0x003E9240 File Offset: 0x003E9240
    public string CallId { get; set; }

    // Token: 0x1700067D RID: 1661
    // (get) Token: 0x06001D83 RID: 7555 RVA: 0x003E924C File Offset: 0x003E924C
    // (set) Token: 0x06001D84 RID: 7556 RVA: 0x003E9254 File Offset: 0x003E9254
    public string Name { get; set; }

    // Token: 0x1700067E RID: 1662
    // (get) Token: 0x06001D85 RID: 7557 RVA: 0x003E9260 File Offset: 0x003E9260
    // (set) Token: 0x06001D86 RID: 7558 RVA: 0x003E9268 File Offset: 0x003E9268
    public string Arguments { get; set; }

    // Token: 0x1700067F RID: 1663
    // (get) Token: 0x06001D87 RID: 7559 RVA: 0x003E9274 File Offset: 0x003E9274
    // (set) Token: 0x06001D88 RID: 7560 RVA: 0x003E927C File Offset: 0x003E927C
    public bool IsAsync { get; set; }

    // Token: 0x06001D89 RID: 7561 RVA: 0x003E9288 File Offset: 0x003E9288
    [NullableContext(2)]
    public static BrainToolCall FromEvent(JsonElement call)
    {
      if (call.ValueKind != JsonValueKind.Object)
      {
        return null;
      }
      JsonElement jsonElement;
      if (!call.TryGetProperty("call_id", out jsonElement) || jsonElement.ValueKind != JsonValueKind.String)
      {
        return null;
      }
      JsonElement jsonElement2;
      if (!call.TryGetProperty("name", out jsonElement2) || jsonElement2.ValueKind != JsonValueKind.String)
      {
        return null;
      }
      JsonElement jsonElement3;
      string text = ((call.TryGetProperty("arguments", out jsonElement3) && jsonElement3.ValueKind == JsonValueKind.String) ? jsonElement3.GetString() : "{}");
      JsonElement jsonElement4;
      bool flag = call.TryGetProperty("async", out jsonElement4) && jsonElement4.ValueKind == JsonValueKind.True;
      return new BrainToolCall(jsonElement.GetString(), jsonElement2.GetString(), text, flag);
    }

    // Token: 0x06001D8A RID: 7562 RVA: 0x003E9338 File Offset: 0x003E9338
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("BrainToolCall");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001D8B RID: 7563 RVA: 0x003E9384 File Offset: 0x003E9384
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("CallId = ");
      builder.Append(this.CallId);
      builder.Append(", Name = ");
      builder.Append(this.Name);
      builder.Append(", Arguments = ");
      builder.Append(this.Arguments);
      builder.Append(", IsAsync = ");
      builder.Append(this.IsAsync.ToString());
      return true;
    }

    // Token: 0x06001D8C RID: 7564 RVA: 0x003E940C File Offset: 0x003E940C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(BrainToolCall left, BrainToolCall right)
    {
      return !(left == right);
    }

    // Token: 0x06001D8D RID: 7565 RVA: 0x003E9418 File Offset: 0x003E9418
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(BrainToolCall left, BrainToolCall right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001D8E RID: 7566 RVA: 0x003E942C File Offset: 0x003E942C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CallId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Arguments>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsAsync>k__BackingField);
    }

    // Token: 0x06001D8F RID: 7567 RVA: 0x003E94A8 File Offset: 0x003E94A8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as BrainToolCall);
    }

    // Token: 0x06001D90 RID: 7568 RVA: 0x003E94B8 File Offset: 0x003E94B8
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(BrainToolCall other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<CallId>k__BackingField, other.<CallId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Arguments>k__BackingField, other.<Arguments>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsAsync>k__BackingField, other.<IsAsync>k__BackingField));
    }

    // Token: 0x06001D92 RID: 7570 RVA: 0x003E954C File Offset: 0x003E954C
    [CompilerGenerated]
    private BrainToolCall(BrainToolCall original)
    {
      this.CallId = original.<CallId>k__BackingField;
      this.Name = original.<Name>k__BackingField;
      this.Arguments = original.<Arguments>k__BackingField;
      this.IsAsync = original.<IsAsync>k__BackingField;
    }

    // Token: 0x06001D93 RID: 7571 RVA: 0x003E9584 File Offset: 0x003E9584
    [CompilerGenerated]
    public void Deconstruct(out string CallId, out string Name, out string Arguments, out bool IsAsync)
    {
      CallId = this.CallId;
      Name = this.Name;
      Arguments = this.Arguments;
      IsAsync = this.IsAsync;
    }
  }
}
