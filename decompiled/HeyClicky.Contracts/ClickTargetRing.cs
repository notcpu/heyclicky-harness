using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x02000024 RID: 36
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ClickTargetRing : IEquatable<ClickTargetRing>
  {
    // Token: 0x060001CA RID: 458 RVA: 0x0001F61C File Offset: 0x0001F61C
    public ClickTargetRing(PointD CenterPx, double RadiusPx, RectD? ControlFramePx, string Label)
    {
      this.CenterPx = CenterPx;
      this.RadiusPx = RadiusPx;
      this.ControlFramePx = ControlFramePx;
      this.Label = Label;
      base..ctor();
    }

    // Token: 0x17000078 RID: 120
    // (get) Token: 0x060001CB RID: 459 RVA: 0x0001F644 File Offset: 0x0001F644
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ClickTargetRing);
      }
    }

    // Token: 0x17000079 RID: 121
    // (get) Token: 0x060001CC RID: 460 RVA: 0x0001F650 File Offset: 0x0001F650
    // (set) Token: 0x060001CD RID: 461 RVA: 0x0001F658 File Offset: 0x0001F658
    public PointD CenterPx { get; set; }

    // Token: 0x1700007A RID: 122
    // (get) Token: 0x060001CE RID: 462 RVA: 0x0001F664 File Offset: 0x0001F664
    // (set) Token: 0x060001CF RID: 463 RVA: 0x0001F66C File Offset: 0x0001F66C
    public double RadiusPx { get; set; }

    // Token: 0x1700007B RID: 123
    // (get) Token: 0x060001D0 RID: 464 RVA: 0x0001F678 File Offset: 0x0001F678
    // (set) Token: 0x060001D1 RID: 465 RVA: 0x0001F680 File Offset: 0x0001F680
    public RectD? ControlFramePx { get; set; }

    // Token: 0x1700007C RID: 124
    // (get) Token: 0x060001D2 RID: 466 RVA: 0x0001F68C File Offset: 0x0001F68C
    // (set) Token: 0x060001D3 RID: 467 RVA: 0x0001F694 File Offset: 0x0001F694
    public string Label { get; set; }

    // Token: 0x060001D4 RID: 468 RVA: 0x0001F6A0 File Offset: 0x0001F6A0
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickTargetRing");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060001D5 RID: 469 RVA: 0x0001F6EC File Offset: 0x0001F6EC
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("CenterPx = ");
      builder.Append(this.CenterPx.ToString());
      builder.Append(", RadiusPx = ");
      builder.Append(this.RadiusPx.ToString());
      builder.Append(", ControlFramePx = ");
      builder.Append(this.ControlFramePx.ToString());
      builder.Append(", Label = ");
      builder.Append(this.Label);
      return true;
    }

    // Token: 0x060001D6 RID: 470 RVA: 0x0001F790 File Offset: 0x0001F790
    [CompilerGenerated]
    public static bool operator !=(ClickTargetRing left, ClickTargetRing right)
    {
      return !(left == right);
    }

    // Token: 0x060001D7 RID: 471 RVA: 0x0001F79C File Offset: 0x0001F79C
    [CompilerGenerated]
    public static bool operator ==(ClickTargetRing left, ClickTargetRing right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060001D8 RID: 472 RVA: 0x0001F7B0 File Offset: 0x0001F7B0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<PointD>.Default.GetHashCode(this.<CenterPx>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<RadiusPx>k__BackingField)) * -1521134295 + EqualityComparer<RectD?>.Default.GetHashCode(this.<ControlFramePx>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Label>k__BackingField);
    }

    // Token: 0x060001D9 RID: 473 RVA: 0x0001F82C File Offset: 0x0001F82C
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickTargetRing);
    }

    // Token: 0x060001DA RID: 474 RVA: 0x0001F83C File Offset: 0x0001F83C
    [CompilerGenerated]
    public bool Equals(ClickTargetRing other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<PointD>.Default.Equals(this.<CenterPx>k__BackingField, other.<CenterPx>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<RadiusPx>k__BackingField, other.<RadiusPx>k__BackingField) && EqualityComparer<RectD?>.Default.Equals(this.<ControlFramePx>k__BackingField, other.<ControlFramePx>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Label>k__BackingField, other.<Label>k__BackingField));
    }

    // Token: 0x060001DC RID: 476 RVA: 0x0001F8D0 File Offset: 0x0001F8D0
    [CompilerGenerated]
    private ClickTargetRing([Nullable(1)] ClickTargetRing original)
    {
      this.CenterPx = original.<CenterPx>k__BackingField;
      this.RadiusPx = original.<RadiusPx>k__BackingField;
      this.ControlFramePx = original.<ControlFramePx>k__BackingField;
      this.Label = original.<Label>k__BackingField;
    }

    // Token: 0x060001DD RID: 477 RVA: 0x0001F908 File Offset: 0x0001F908
    [CompilerGenerated]
    public void Deconstruct(out PointD CenterPx, out double RadiusPx, out RectD? ControlFramePx, out string Label)
    {
      CenterPx = this.CenterPx;
      RadiusPx = this.RadiusPx;
      ControlFramePx = this.ControlFramePx;
      Label = this.Label;
    }
  }
}
