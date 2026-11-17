using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace HeyClicky
{
  // Token: 0x02000020 RID: 32
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DrawingFeedResult : IEquatable<DrawingFeedResult>
  {
    // Token: 0x06000162 RID: 354 RVA: 0x0001EE6C File Offset: 0x0001EE6C
    public DrawingFeedResult(IReadOnlyList<string> Lines, IReadOnlyList<DrawingAction> Actions, int MarkCount, JsonElement Raw)
    {
      this.Lines = Lines;
      this.Actions = Actions;
      this.MarkCount = MarkCount;
      this.Raw = Raw;
      base..ctor();
    }

    // Token: 0x1700005E RID: 94
    // (get) Token: 0x06000163 RID: 355 RVA: 0x0001EE94 File Offset: 0x0001EE94
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(DrawingFeedResult);
      }
    }

    // Token: 0x1700005F RID: 95
    // (get) Token: 0x06000164 RID: 356 RVA: 0x0001EEA0 File Offset: 0x0001EEA0
    // (set) Token: 0x06000165 RID: 357 RVA: 0x0001EEA8 File Offset: 0x0001EEA8
    public IReadOnlyList<string> Lines { get; set; }

    // Token: 0x17000060 RID: 96
    // (get) Token: 0x06000166 RID: 358 RVA: 0x0001EEB4 File Offset: 0x0001EEB4
    // (set) Token: 0x06000167 RID: 359 RVA: 0x0001EEBC File Offset: 0x0001EEBC
    public IReadOnlyList<DrawingAction> Actions { get; set; }

    // Token: 0x17000061 RID: 97
    // (get) Token: 0x06000168 RID: 360 RVA: 0x0001EEC8 File Offset: 0x0001EEC8
    // (set) Token: 0x06000169 RID: 361 RVA: 0x0001EED0 File Offset: 0x0001EED0
    public int MarkCount { get; set; }

    // Token: 0x17000062 RID: 98
    // (get) Token: 0x0600016A RID: 362 RVA: 0x0001EEDC File Offset: 0x0001EEDC
    // (set) Token: 0x0600016B RID: 363 RVA: 0x0001EEE4 File Offset: 0x0001EEE4
    public JsonElement Raw { get; set; }

    // Token: 0x17000063 RID: 99
    // (get) Token: 0x0600016C RID: 364 RVA: 0x0001EEF0 File Offset: 0x0001EEF0
    public int InkedMarkCount
    {
      get
      {
        JsonElement jsonElement;
        if (this.Raw.ValueKind != JsonValueKind.Object || !this.Raw.TryGetProperty("inked", out jsonElement) || jsonElement.ValueKind != JsonValueKind.Number)
        {
          return this.MarkCount;
        }
        return jsonElement.GetInt32();
      }
    }

    // Token: 0x17000064 RID: 100
    // (get) Token: 0x0600016D RID: 365 RVA: 0x0001EF40 File Offset: 0x0001EF40
    public bool DrawsPicture
    {
      get
      {
        JsonElement jsonElement;
        return this.Raw.ValueKind == JsonValueKind.Object && this.Raw.TryGetProperty("picture", out jsonElement) && jsonElement.ValueKind == JsonValueKind.True;
      }
    }

    // Token: 0x0600016E RID: 366 RVA: 0x0001EF84 File Offset: 0x0001EF84
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DrawingFeedResult");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600016F RID: 367 RVA: 0x0001EFD0 File Offset: 0x0001EFD0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Lines = ");
      builder.Append(this.Lines);
      builder.Append(", Actions = ");
      builder.Append(this.Actions);
      builder.Append(", MarkCount = ");
      builder.Append(this.MarkCount.ToString());
      builder.Append(", Raw = ");
      builder.Append(this.Raw.ToString());
      builder.Append(", InkedMarkCount = ");
      builder.Append(this.InkedMarkCount.ToString());
      builder.Append(", DrawsPicture = ");
      builder.Append(this.DrawsPicture.ToString());
      return true;
    }

    // Token: 0x06000170 RID: 368 RVA: 0x0001F0B4 File Offset: 0x0001F0B4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(DrawingFeedResult left, DrawingFeedResult right)
    {
      return !(left == right);
    }

    // Token: 0x06000171 RID: 369 RVA: 0x0001F0C0 File Offset: 0x0001F0C0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(DrawingFeedResult left, DrawingFeedResult right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000172 RID: 370 RVA: 0x0001F0D4 File Offset: 0x0001F0D4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Lines>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<DrawingAction>>.Default.GetHashCode(this.<Actions>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MarkCount>k__BackingField)) * -1521134295 + EqualityComparer<JsonElement>.Default.GetHashCode(this.<Raw>k__BackingField);
    }

    // Token: 0x06000173 RID: 371 RVA: 0x0001F150 File Offset: 0x0001F150
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as DrawingFeedResult);
    }

    // Token: 0x06000174 RID: 372 RVA: 0x0001F160 File Offset: 0x0001F160
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(DrawingFeedResult other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Lines>k__BackingField, other.<Lines>k__BackingField) && EqualityComparer<IReadOnlyList<DrawingAction>>.Default.Equals(this.<Actions>k__BackingField, other.<Actions>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MarkCount>k__BackingField, other.<MarkCount>k__BackingField) && EqualityComparer<JsonElement>.Default.Equals(this.<Raw>k__BackingField, other.<Raw>k__BackingField));
    }

    // Token: 0x06000176 RID: 374 RVA: 0x0001F1F4 File Offset: 0x0001F1F4
    [CompilerGenerated]
    private DrawingFeedResult(DrawingFeedResult original)
    {
      this.Lines = original.<Lines>k__BackingField;
      this.Actions = original.<Actions>k__BackingField;
      this.MarkCount = original.<MarkCount>k__BackingField;
      this.Raw = original.<Raw>k__BackingField;
    }

    // Token: 0x06000177 RID: 375 RVA: 0x0001F22C File Offset: 0x0001F22C
    [CompilerGenerated]
    public void Deconstruct(out IReadOnlyList<string> Lines, out IReadOnlyList<DrawingAction> Actions, out int MarkCount, out JsonElement Raw)
    {
      Lines = this.Lines;
      Actions = this.Actions;
      MarkCount = this.MarkCount;
      Raw = this.Raw;
    }
  }
}
