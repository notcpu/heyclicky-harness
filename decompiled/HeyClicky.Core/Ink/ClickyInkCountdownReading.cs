using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000142 RID: 322
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkCountdownReading : IEquatable<ClickyInkCountdownReading>
  {
    // Token: 0x170004C1 RID: 1217
    // (get) Token: 0x0600143D RID: 5181 RVA: 0x003AC324 File Offset: 0x003AC324
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkCountdownReading);
      }
    }

    // Token: 0x170004C2 RID: 1218
    // (get) Token: 0x0600143E RID: 5182 RVA: 0x003AC330 File Offset: 0x003AC330
    public string Big { get; }

    // Token: 0x170004C3 RID: 1219
    // (get) Token: 0x0600143F RID: 5183 RVA: 0x003AC338 File Offset: 0x003AC338
    public string Unit { get; }

    // Token: 0x170004C4 RID: 1220
    // (get) Token: 0x06001440 RID: 5184 RVA: 0x003AC340 File Offset: 0x003AC340
    [Nullable(2)]
    public string DateLine
    {
      [NullableContext(2)]
      get;
    }

    // Token: 0x170004C5 RID: 1221
    // (get) Token: 0x06001441 RID: 5185 RVA: 0x003AC348 File Offset: 0x003AC348
    public bool IsPast { get; }

    // Token: 0x170004C6 RID: 1222
    // (get) Token: 0x06001442 RID: 5186 RVA: 0x003AC350 File Offset: 0x003AC350
    public bool IsClose { get; }

    // Token: 0x06001443 RID: 5187 RVA: 0x003AC358 File Offset: 0x003AC358
    public ClickyInkCountdownReading(ClickyInkDocument.Block block)
    {
      string text;
      if ((text = block["to"]) == null && (text = block["due"]) == null && (text = block["at"]) == null)
      {
        text = block["date"] ?? block["when"];
      }
      string text2 = text;
      DateTimeOffset? dateTimeOffset = ClickyInkDates.Parse(text2, null);
      if (dateTimeOffset != null)
      {
        DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
        ClickyInkDates.Remaining remaining = ClickyInkDates.RemainingUntil(valueOrDefault, null);
        this.Big = remaining.Big;
        this.Unit = remaining.Unit;
        this.IsPast = remaining.IsPast;
        this.IsClose = remaining.IsClose;
        this.DateLine = ClickyInkDates.ShortLine(valueOrDefault);
        return;
      }
      string text3 = block["left"];
      if (text3 != null)
      {
        string text4 = text3.SplitOmittingEmpty(' ').FirstOrDefault<string>();
        if (text4 != null && ClickyInkNumber.Parse(text4) != null)
        {
          this.Big = text4;
          this.Unit = string.Join(" ", text3.SplitOmittingEmpty(' ').Skip<string>(1));
          this.IsPast = false;
          this.IsClose = this.Unit.HasPrefix("h") || this.Unit.HasPrefix("min");
          this.DateLine = text2;
          return;
        }
      }
      this.Big = "";
      this.Unit = "";
      this.IsPast = false;
      this.IsClose = false;
      this.DateLine = text2 ?? block["left"];
    }

    // Token: 0x06001444 RID: 5188 RVA: 0x003AC4FC File Offset: 0x003AC4FC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkCountdownReading");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001445 RID: 5189 RVA: 0x003AC548 File Offset: 0x003AC548
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Big = ");
      builder.Append(this.Big);
      builder.Append(", Unit = ");
      builder.Append(this.Unit);
      builder.Append(", DateLine = ");
      builder.Append(this.DateLine);
      builder.Append(", IsPast = ");
      builder.Append(this.IsPast.ToString());
      builder.Append(", IsClose = ");
      builder.Append(this.IsClose.ToString());
      return true;
    }

    // Token: 0x06001446 RID: 5190 RVA: 0x003AC5F4 File Offset: 0x003AC5F4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkCountdownReading left, ClickyInkCountdownReading right)
    {
      return !(left == right);
    }

    // Token: 0x06001447 RID: 5191 RVA: 0x003AC600 File Offset: 0x003AC600
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkCountdownReading left, ClickyInkCountdownReading right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001448 RID: 5192 RVA: 0x003AC614 File Offset: 0x003AC614
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Big>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Unit>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DateLine>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsPast>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsClose>k__BackingField);
    }

    // Token: 0x06001449 RID: 5193 RVA: 0x003AC6A4 File Offset: 0x003AC6A4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkCountdownReading);
    }

    // Token: 0x0600144A RID: 5194 RVA: 0x003AC6B4 File Offset: 0x003AC6B4
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyInkCountdownReading other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Big>k__BackingField, other.<Big>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Unit>k__BackingField, other.<Unit>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DateLine>k__BackingField, other.<DateLine>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsPast>k__BackingField, other.<IsPast>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsClose>k__BackingField, other.<IsClose>k__BackingField));
    }

    // Token: 0x0600144C RID: 5196 RVA: 0x003AC764 File Offset: 0x003AC764
    [CompilerGenerated]
    private ClickyInkCountdownReading(ClickyInkCountdownReading original)
    {
      this.Big = original.<Big>k__BackingField;
      this.Unit = original.<Unit>k__BackingField;
      this.DateLine = original.<DateLine>k__BackingField;
      this.IsPast = original.<IsPast>k__BackingField;
      this.IsClose = original.<IsClose>k__BackingField;
    }
  }
}
