using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x02000007 RID: 7
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DisplayInfo : IEquatable<DisplayInfo>
  {
    // Token: 0x06000052 RID: 82 RVA: 0x0001CDC4 File Offset: 0x0001CDC4
    public DisplayInfo(int Index, string DeviceName, RectD BoundsPx, RectD WorkAreaPx, double Scale, bool IsPrimary)
    {
      this.Index = Index;
      this.DeviceName = DeviceName;
      this.BoundsPx = BoundsPx;
      this.WorkAreaPx = WorkAreaPx;
      this.Scale = Scale;
      this.IsPrimary = IsPrimary;
      base..ctor();
    }

    // Token: 0x17000017 RID: 23
    // (get) Token: 0x06000053 RID: 83 RVA: 0x0001CDFC File Offset: 0x0001CDFC
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(DisplayInfo);
      }
    }

    // Token: 0x17000018 RID: 24
    // (get) Token: 0x06000054 RID: 84 RVA: 0x0001CE08 File Offset: 0x0001CE08
    // (set) Token: 0x06000055 RID: 85 RVA: 0x0001CE10 File Offset: 0x0001CE10
    public int Index { get; set; }

    // Token: 0x17000019 RID: 25
    // (get) Token: 0x06000056 RID: 86 RVA: 0x0001CE1C File Offset: 0x0001CE1C
    // (set) Token: 0x06000057 RID: 87 RVA: 0x0001CE24 File Offset: 0x0001CE24
    public string DeviceName { get; set; }

    // Token: 0x1700001A RID: 26
    // (get) Token: 0x06000058 RID: 88 RVA: 0x0001CE30 File Offset: 0x0001CE30
    // (set) Token: 0x06000059 RID: 89 RVA: 0x0001CE38 File Offset: 0x0001CE38
    public RectD BoundsPx { get; set; }

    // Token: 0x1700001B RID: 27
    // (get) Token: 0x0600005A RID: 90 RVA: 0x0001CE44 File Offset: 0x0001CE44
    // (set) Token: 0x0600005B RID: 91 RVA: 0x0001CE4C File Offset: 0x0001CE4C
    public RectD WorkAreaPx { get; set; }

    // Token: 0x1700001C RID: 28
    // (get) Token: 0x0600005C RID: 92 RVA: 0x0001CE58 File Offset: 0x0001CE58
    // (set) Token: 0x0600005D RID: 93 RVA: 0x0001CE60 File Offset: 0x0001CE60
    public double Scale { get; set; }

    // Token: 0x1700001D RID: 29
    // (get) Token: 0x0600005E RID: 94 RVA: 0x0001CE6C File Offset: 0x0001CE6C
    // (set) Token: 0x0600005F RID: 95 RVA: 0x0001CE74 File Offset: 0x0001CE74
    public bool IsPrimary { get; set; }

    // Token: 0x06000060 RID: 96 RVA: 0x0001CE80 File Offset: 0x0001CE80
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DisplayInfo");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000061 RID: 97 RVA: 0x0001CECC File Offset: 0x0001CECC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Index = ");
      builder.Append(this.Index.ToString());
      builder.Append(", DeviceName = ");
      builder.Append(this.DeviceName);
      builder.Append(", BoundsPx = ");
      builder.Append(this.BoundsPx.ToString());
      builder.Append(", WorkAreaPx = ");
      builder.Append(this.WorkAreaPx.ToString());
      builder.Append(", Scale = ");
      builder.Append(this.Scale.ToString());
      builder.Append(", IsPrimary = ");
      builder.Append(this.IsPrimary.ToString());
      return true;
    }

    // Token: 0x06000062 RID: 98 RVA: 0x0001CFBC File Offset: 0x0001CFBC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(DisplayInfo left, DisplayInfo right)
    {
      return !(left == right);
    }

    // Token: 0x06000063 RID: 99 RVA: 0x0001CFC8 File Offset: 0x0001CFC8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(DisplayInfo left, DisplayInfo right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000064 RID: 100 RVA: 0x0001CFDC File Offset: 0x0001CFDC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Index>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DeviceName>k__BackingField)) * -1521134295 + EqualityComparer<RectD>.Default.GetHashCode(this.<BoundsPx>k__BackingField)) * -1521134295 + EqualityComparer<RectD>.Default.GetHashCode(this.<WorkAreaPx>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Scale>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsPrimary>k__BackingField);
    }

    // Token: 0x06000065 RID: 101 RVA: 0x0001D084 File Offset: 0x0001D084
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as DisplayInfo);
    }

    // Token: 0x06000066 RID: 102 RVA: 0x0001D094 File Offset: 0x0001D094
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(DisplayInfo other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<Index>k__BackingField, other.<Index>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DeviceName>k__BackingField, other.<DeviceName>k__BackingField) && EqualityComparer<RectD>.Default.Equals(this.<BoundsPx>k__BackingField, other.<BoundsPx>k__BackingField) && EqualityComparer<RectD>.Default.Equals(this.<WorkAreaPx>k__BackingField, other.<WorkAreaPx>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Scale>k__BackingField, other.<Scale>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsPrimary>k__BackingField, other.<IsPrimary>k__BackingField));
    }

    // Token: 0x06000068 RID: 104 RVA: 0x0001D160 File Offset: 0x0001D160
    [CompilerGenerated]
    private DisplayInfo(DisplayInfo original)
    {
      this.Index = original.<Index>k__BackingField;
      this.DeviceName = original.<DeviceName>k__BackingField;
      this.BoundsPx = original.<BoundsPx>k__BackingField;
      this.WorkAreaPx = original.<WorkAreaPx>k__BackingField;
      this.Scale = original.<Scale>k__BackingField;
      this.IsPrimary = original.<IsPrimary>k__BackingField;
    }

    // Token: 0x06000069 RID: 105 RVA: 0x0001D1BC File Offset: 0x0001D1BC
    [CompilerGenerated]
    public void Deconstruct(out int Index, out string DeviceName, out RectD BoundsPx, out RectD WorkAreaPx, out double Scale, out bool IsPrimary)
    {
      Index = this.Index;
      DeviceName = this.DeviceName;
      BoundsPx = this.BoundsPx;
      WorkAreaPx = this.WorkAreaPx;
      Scale = this.Scale;
      IsPrimary = this.IsPrimary;
    }
  }
}
