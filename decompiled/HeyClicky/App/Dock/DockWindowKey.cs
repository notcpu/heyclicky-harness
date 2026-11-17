using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.Agents.Jobs;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000304 RID: 772
  [NullableContext(1)]
  [Nullable(0)]
  public readonly struct DockWindowKey : IEquatable<DockWindowKey>
  {
    // Token: 0x060021B2 RID: 8626 RVA: 0x0061D7A8 File Offset: 0x0061D7A8
    public DockWindowKey(string Key, string RailId, [Nullable(2)] string ClickySlug, [Nullable(2)] ClickyJob Job)
    {
      this.Key = Key;
      this.RailId = RailId;
      this.ClickySlug = ClickySlug;
      this.Job = Job;
    }

    // Token: 0x17000639 RID: 1593
    // (get) Token: 0x060021B3 RID: 8627 RVA: 0x0061D7C8 File Offset: 0x0061D7C8
    // (set) Token: 0x060021B4 RID: 8628 RVA: 0x0061D7D0 File Offset: 0x0061D7D0
    public string Key { get; set; }

    // Token: 0x1700063A RID: 1594
    // (get) Token: 0x060021B5 RID: 8629 RVA: 0x0061D7DC File Offset: 0x0061D7DC
    // (set) Token: 0x060021B6 RID: 8630 RVA: 0x0061D7E4 File Offset: 0x0061D7E4
    public string RailId { get; set; }

    // Token: 0x1700063B RID: 1595
    // (get) Token: 0x060021B7 RID: 8631 RVA: 0x0061D7F0 File Offset: 0x0061D7F0
    // (set) Token: 0x060021B8 RID: 8632 RVA: 0x0061D7F8 File Offset: 0x0061D7F8
    [Nullable(2)]
    public string ClickySlug
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700063C RID: 1596
    // (get) Token: 0x060021B9 RID: 8633 RVA: 0x0061D804 File Offset: 0x0061D804
    // (set) Token: 0x060021BA RID: 8634 RVA: 0x0061D80C File Offset: 0x0061D80C
    [Nullable(2)]
    public ClickyJob Job
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700063D RID: 1597
    // (get) Token: 0x060021BB RID: 8635 RVA: 0x0061D818 File Offset: 0x0061D818
    public bool IsJob
    {
      get
      {
        return this.Job != null;
      }
    }

    // Token: 0x060021BC RID: 8636 RVA: 0x0061D828 File Offset: 0x0061D828
    [NullableContext(0)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DockWindowKey");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060021BD RID: 8637 RVA: 0x0061D874 File Offset: 0x0061D874
    [NullableContext(0)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("Key = ");
      builder.Append(this.Key);
      builder.Append(", RailId = ");
      builder.Append(this.RailId);
      builder.Append(", ClickySlug = ");
      builder.Append(this.ClickySlug);
      builder.Append(", Job = ");
      builder.Append(this.Job);
      builder.Append(", IsJob = ");
      builder.Append(this.IsJob.ToString());
      return true;
    }

    // Token: 0x060021BE RID: 8638 RVA: 0x0061D910 File Offset: 0x0061D910
    [CompilerGenerated]
    public static bool operator !=(DockWindowKey left, DockWindowKey right)
    {
      return !(left == right);
    }

    // Token: 0x060021BF RID: 8639 RVA: 0x0061D91C File Offset: 0x0061D91C
    [CompilerGenerated]
    public static bool operator ==(DockWindowKey left, DockWindowKey right)
    {
      return left.Equals(right);
    }

    // Token: 0x060021C0 RID: 8640 RVA: 0x0061D928 File Offset: 0x0061D928
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<string>.Default.GetHashCode(this.<Key>k__BackingField) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RailId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField)) * -1521134295 + EqualityComparer<ClickyJob>.Default.GetHashCode(this.<Job>k__BackingField);
    }

    // Token: 0x060021C1 RID: 8641 RVA: 0x0061D98C File Offset: 0x0061D98C
    [NullableContext(0)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is DockWindowKey && this.Equals((DockWindowKey)obj);
    }

    // Token: 0x060021C2 RID: 8642 RVA: 0x0061D9A4 File Offset: 0x0061D9A4
    [CompilerGenerated]
    public bool Equals(DockWindowKey other)
    {
      return EqualityComparer<string>.Default.Equals(this.<Key>k__BackingField, other.<Key>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RailId>k__BackingField, other.<RailId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField) && EqualityComparer<ClickyJob>.Default.Equals(this.<Job>k__BackingField, other.<Job>k__BackingField);
    }

    // Token: 0x060021C3 RID: 8643 RVA: 0x0061DA14 File Offset: 0x0061DA14
    [CompilerGenerated]
    public void Deconstruct(out string Key, out string RailId, [Nullable(2)] out string ClickySlug, [Nullable(2)] out ClickyJob Job)
    {
      Key = this.Key;
      RailId = this.RailId;
      ClickySlug = this.ClickySlug;
      Job = this.Job;
    }
  }
}
