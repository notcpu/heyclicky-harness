using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002FC RID: 764
  public readonly struct ArcScreenMetrics : IEquatable<ArcScreenMetrics>
  {
    // Token: 0x06002161 RID: 8545 RVA: 0x0061C280 File Offset: 0x0061C280
    public ArcScreenMetrics(double PanelWidth, double PanelHeight, double MenuBarHeight, ArcRect? HardwareNotch = null, ArcRect? NotchSurface = null)
    {
      this.PanelWidth = PanelWidth;
      this.PanelHeight = PanelHeight;
      this.MenuBarHeight = MenuBarHeight;
      this.HardwareNotch = HardwareNotch;
      this.NotchSurface = NotchSurface;
    }

    // Token: 0x17000628 RID: 1576
    // (get) Token: 0x06002162 RID: 8546 RVA: 0x0061C2A8 File Offset: 0x0061C2A8
    // (set) Token: 0x06002163 RID: 8547 RVA: 0x0061C2B0 File Offset: 0x0061C2B0
    public double PanelWidth { get; set; }

    // Token: 0x17000629 RID: 1577
    // (get) Token: 0x06002164 RID: 8548 RVA: 0x0061C2BC File Offset: 0x0061C2BC
    // (set) Token: 0x06002165 RID: 8549 RVA: 0x0061C2C4 File Offset: 0x0061C2C4
    public double PanelHeight { get; set; }

    // Token: 0x1700062A RID: 1578
    // (get) Token: 0x06002166 RID: 8550 RVA: 0x0061C2D0 File Offset: 0x0061C2D0
    // (set) Token: 0x06002167 RID: 8551 RVA: 0x0061C2D8 File Offset: 0x0061C2D8
    public double MenuBarHeight { get; set; }

    // Token: 0x1700062B RID: 1579
    // (get) Token: 0x06002168 RID: 8552 RVA: 0x0061C2E4 File Offset: 0x0061C2E4
    // (set) Token: 0x06002169 RID: 8553 RVA: 0x0061C2EC File Offset: 0x0061C2EC
    public ArcRect? HardwareNotch { get; set; }

    // Token: 0x1700062C RID: 1580
    // (get) Token: 0x0600216A RID: 8554 RVA: 0x0061C2F8 File Offset: 0x0061C2F8
    // (set) Token: 0x0600216B RID: 8555 RVA: 0x0061C300 File Offset: 0x0061C300
    public ArcRect? NotchSurface { get; set; }

    // Token: 0x1700062D RID: 1581
    // (get) Token: 0x0600216C RID: 8556 RVA: 0x0061C30C File Offset: 0x0061C30C
    public ArcRect NotchTriggerFrame
    {
      get
      {
        ArcRect? hardwareNotch = this.HardwareNotch;
        if (hardwareNotch != null)
        {
          return hardwareNotch.GetValueOrDefault().Inset(-6.0, 0.0);
        }
        return new ArcRect(this.PanelWidth / 2.0 - 110.0, 0.0, 220.0, Math.Max(8.0, this.MenuBarHeight * 0.6));
      }
    }

    // Token: 0x0600216D RID: 8557 RVA: 0x0061C39C File Offset: 0x0061C39C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ArcScreenMetrics");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600216E RID: 8558 RVA: 0x0061C3E8 File Offset: 0x0061C3E8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("PanelWidth = ");
      builder.Append(this.PanelWidth.ToString());
      builder.Append(", PanelHeight = ");
      builder.Append(this.PanelHeight.ToString());
      builder.Append(", MenuBarHeight = ");
      builder.Append(this.MenuBarHeight.ToString());
      builder.Append(", HardwareNotch = ");
      builder.Append(this.HardwareNotch.ToString());
      builder.Append(", NotchSurface = ");
      builder.Append(this.NotchSurface.ToString());
      builder.Append(", NotchTriggerFrame = ");
      builder.Append(this.NotchTriggerFrame.ToString());
      return true;
    }

    // Token: 0x0600216F RID: 8559 RVA: 0x0061C4E0 File Offset: 0x0061C4E0
    [CompilerGenerated]
    public static bool operator !=(ArcScreenMetrics left, ArcScreenMetrics right)
    {
      return !(left == right);
    }

    // Token: 0x06002170 RID: 8560 RVA: 0x0061C4EC File Offset: 0x0061C4EC
    [CompilerGenerated]
    public static bool operator ==(ArcScreenMetrics left, ArcScreenMetrics right)
    {
      return left.Equals(right);
    }

    // Token: 0x06002171 RID: 8561 RVA: 0x0061C4F8 File Offset: 0x0061C4F8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<double>.Default.GetHashCode(this.<PanelWidth>k__BackingField) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<PanelHeight>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<MenuBarHeight>k__BackingField)) * -1521134295 + EqualityComparer<ArcRect?>.Default.GetHashCode(this.<HardwareNotch>k__BackingField)) * -1521134295 + EqualityComparer<ArcRect?>.Default.GetHashCode(this.<NotchSurface>k__BackingField);
    }

    // Token: 0x06002172 RID: 8562 RVA: 0x0061C574 File Offset: 0x0061C574
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is ArcScreenMetrics && this.Equals((ArcScreenMetrics)obj);
    }

    // Token: 0x06002173 RID: 8563 RVA: 0x0061C58C File Offset: 0x0061C58C
    [CompilerGenerated]
    public bool Equals(ArcScreenMetrics other)
    {
      return EqualityComparer<double>.Default.Equals(this.<PanelWidth>k__BackingField, other.<PanelWidth>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<PanelHeight>k__BackingField, other.<PanelHeight>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<MenuBarHeight>k__BackingField, other.<MenuBarHeight>k__BackingField) && EqualityComparer<ArcRect?>.Default.Equals(this.<HardwareNotch>k__BackingField, other.<HardwareNotch>k__BackingField) && EqualityComparer<ArcRect?>.Default.Equals(this.<NotchSurface>k__BackingField, other.<NotchSurface>k__BackingField);
    }

    // Token: 0x06002174 RID: 8564 RVA: 0x0061C614 File Offset: 0x0061C614
    [CompilerGenerated]
    public void Deconstruct(out double PanelWidth, out double PanelHeight, out double MenuBarHeight, out ArcRect? HardwareNotch, out ArcRect? NotchSurface)
    {
      PanelWidth = this.PanelWidth;
      PanelHeight = this.PanelHeight;
      MenuBarHeight = this.MenuBarHeight;
      HardwareNotch = this.HardwareNotch;
      NotchSurface = this.NotchSurface;
    }

    // Token: 0x0400100D RID: 4109
    public static readonly ArcScreenMetrics Fallback = new ArcScreenMetrics(1512.0, 982.0, 32.0, null, null);
  }
}
