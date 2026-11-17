using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;

namespace HeyClicky.Core.Worker
{
  // Token: 0x0200003C RID: 60
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PlanUsageBucket : IEquatable<PlanUsageBucket>
  {
    // Token: 0x06000211 RID: 529 RVA: 0x00350554 File Offset: 0x00350554
    public PlanUsageBucket(int Used, int Cap)
    {
      this.Used = Used;
      this.Cap = Cap;
      base..ctor();
    }

    // Token: 0x17000063 RID: 99
    // (get) Token: 0x06000212 RID: 530 RVA: 0x0035056C File Offset: 0x0035056C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(PlanUsageBucket);
      }
    }

    // Token: 0x17000064 RID: 100
    // (get) Token: 0x06000213 RID: 531 RVA: 0x00350578 File Offset: 0x00350578
    // (set) Token: 0x06000214 RID: 532 RVA: 0x00350580 File Offset: 0x00350580
    [JsonConverter(typeof(SaturatingInt32Converter))]
    public int Used { get; set; }

    // Token: 0x17000065 RID: 101
    // (get) Token: 0x06000215 RID: 533 RVA: 0x0035058C File Offset: 0x0035058C
    // (set) Token: 0x06000216 RID: 534 RVA: 0x00350594 File Offset: 0x00350594
    [JsonConverter(typeof(SaturatingInt32Converter))]
    public int Cap { get; set; }

    // Token: 0x06000217 RID: 535 RVA: 0x003505A0 File Offset: 0x003505A0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PlanUsageBucket");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000218 RID: 536 RVA: 0x003505EC File Offset: 0x003505EC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Used = ");
      builder.Append(this.Used.ToString());
      builder.Append(", Cap = ");
      builder.Append(this.Cap.ToString());
      return true;
    }

    // Token: 0x06000219 RID: 537 RVA: 0x00350650 File Offset: 0x00350650
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(PlanUsageBucket left, PlanUsageBucket right)
    {
      return !(left == right);
    }

    // Token: 0x0600021A RID: 538 RVA: 0x0035065C File Offset: 0x0035065C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(PlanUsageBucket left, PlanUsageBucket right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600021B RID: 539 RVA: 0x00350670 File Offset: 0x00350670
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Used>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Cap>k__BackingField);
    }

    // Token: 0x0600021C RID: 540 RVA: 0x003506B0 File Offset: 0x003506B0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PlanUsageBucket);
    }

    // Token: 0x0600021D RID: 541 RVA: 0x003506C0 File Offset: 0x003506C0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(PlanUsageBucket other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<Used>k__BackingField, other.<Used>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Cap>k__BackingField, other.<Cap>k__BackingField));
    }

    // Token: 0x0600021F RID: 543 RVA: 0x00350724 File Offset: 0x00350724
    [CompilerGenerated]
    private PlanUsageBucket(PlanUsageBucket original)
    {
      this.Used = original.<Used>k__BackingField;
      this.Cap = original.<Cap>k__BackingField;
    }

    // Token: 0x06000220 RID: 544 RVA: 0x00350744 File Offset: 0x00350744
    [CompilerGenerated]
    public void Deconstruct(out int Used, out int Cap)
    {
      Used = this.Used;
      Cap = this.Cap;
    }
  }
}
