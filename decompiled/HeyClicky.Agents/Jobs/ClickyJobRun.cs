using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x0200003A RID: 58
  [NullableContext(2)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class ClickyJobRun : IEquatable<ClickyJobRun>
  {
    // Token: 0x17000181 RID: 385
    // (get) Token: 0x0600059C RID: 1436 RVA: 0x000F1844 File Offset: 0x000F1844
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ClickyJobRun);
      }
    }

    // Token: 0x17000182 RID: 386
    // (get) Token: 0x0600059D RID: 1437 RVA: 0x000F1850 File Offset: 0x000F1850
    // (set) Token: 0x0600059E RID: 1438 RVA: 0x000F1858 File Offset: 0x000F1858
    public string ThreadId { get; set; }

    // Token: 0x17000183 RID: 387
    // (get) Token: 0x0600059F RID: 1439 RVA: 0x000F1864 File Offset: 0x000F1864
    // (set) Token: 0x060005A0 RID: 1440 RVA: 0x000F186C File Offset: 0x000F186C
    public DateTimeOffset StartedAt { get; set; }

    // Token: 0x17000184 RID: 388
    // (get) Token: 0x060005A1 RID: 1441 RVA: 0x000F1878 File Offset: 0x000F1878
    // (set) Token: 0x060005A2 RID: 1442 RVA: 0x000F1880 File Offset: 0x000F1880
    [Nullable(1)]
    [RequiredMember]
    public string Ask
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000185 RID: 389
    // (get) Token: 0x060005A3 RID: 1443 RVA: 0x000F188C File Offset: 0x000F188C
    // (set) Token: 0x060005A4 RID: 1444 RVA: 0x000F1894 File Offset: 0x000F1894
    public DateTimeOffset? FinishedAt { get; set; }

    // Token: 0x17000186 RID: 390
    // (get) Token: 0x060005A5 RID: 1445 RVA: 0x000F18A0 File Offset: 0x000F18A0
    // (set) Token: 0x060005A6 RID: 1446 RVA: 0x000F18A8 File Offset: 0x000F18A8
    public ClickyJobOutcome Outcome { get; set; }

    // Token: 0x17000187 RID: 391
    // (get) Token: 0x060005A7 RID: 1447 RVA: 0x000F18B4 File Offset: 0x000F18B4
    // (set) Token: 0x060005A8 RID: 1448 RVA: 0x000F18BC File Offset: 0x000F18BC
    public string Summary { get; set; }

    // Token: 0x060005A9 RID: 1449 RVA: 0x000F18C8 File Offset: 0x000F18C8
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyJobRun");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060005AA RID: 1450 RVA: 0x000F1914 File Offset: 0x000F1914
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ThreadId = ");
      builder.Append(this.ThreadId);
      builder.Append(", StartedAt = ");
      builder.Append(this.StartedAt.ToString());
      builder.Append(", Ask = ");
      builder.Append(this.Ask);
      builder.Append(", FinishedAt = ");
      builder.Append(this.FinishedAt.ToString());
      builder.Append(", Outcome = ");
      builder.Append(this.Outcome.ToString());
      builder.Append(", Summary = ");
      builder.Append(this.Summary);
      return true;
    }

    // Token: 0x060005AB RID: 1451 RVA: 0x000F19E8 File Offset: 0x000F19E8
    [CompilerGenerated]
    public static bool operator !=(ClickyJobRun left, ClickyJobRun right)
    {
      return !(left == right);
    }

    // Token: 0x060005AC RID: 1452 RVA: 0x000F19F4 File Offset: 0x000F19F4
    [CompilerGenerated]
    public static bool operator ==(ClickyJobRun left, ClickyJobRun right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060005AD RID: 1453 RVA: 0x000F1A08 File Offset: 0x000F1A08
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<StartedAt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Ask>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<FinishedAt>k__BackingField)) * -1521134295 + EqualityComparer<ClickyJobOutcome>.Default.GetHashCode(this.<Outcome>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Summary>k__BackingField);
    }

    // Token: 0x060005AE RID: 1454 RVA: 0x000F1AB0 File Offset: 0x000F1AB0
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyJobRun);
    }

    // Token: 0x060005AF RID: 1455 RVA: 0x000F1AC0 File Offset: 0x000F1AC0
    [CompilerGenerated]
    public bool Equals(ClickyJobRun other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<StartedAt>k__BackingField, other.<StartedAt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Ask>k__BackingField, other.<Ask>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<FinishedAt>k__BackingField, other.<FinishedAt>k__BackingField) && EqualityComparer<ClickyJobOutcome>.Default.Equals(this.<Outcome>k__BackingField, other.<Outcome>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Summary>k__BackingField, other.<Summary>k__BackingField));
    }

    // Token: 0x060005B1 RID: 1457 RVA: 0x000F1B8C File Offset: 0x000F1B8C
    [CompilerGenerated]
    [SetsRequiredMembers]
    private ClickyJobRun([Nullable(1)] ClickyJobRun original)
    {
      this.ThreadId = original.<ThreadId>k__BackingField;
      this.StartedAt = original.<StartedAt>k__BackingField;
      this.Ask = original.<Ask>k__BackingField;
      this.FinishedAt = original.<FinishedAt>k__BackingField;
      this.Outcome = original.<Outcome>k__BackingField;
      this.Summary = original.<Summary>k__BackingField;
    }

    // Token: 0x060005B2 RID: 1458 RVA: 0x000F1BE8 File Offset: 0x000F1BE8
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public ClickyJobRun()
    {
    }
  }
}
