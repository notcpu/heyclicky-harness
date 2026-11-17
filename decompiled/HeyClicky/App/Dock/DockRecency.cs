using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000303 RID: 771
  public readonly struct DockRecency : IComparable<DockRecency>, IEquatable<DockRecency>
  {
    // Token: 0x060021A4 RID: 8612 RVA: 0x0061D5E0 File Offset: 0x0061D5E0
    public DockRecency(bool IsActive, DateTimeOffset Date)
    {
      this.IsActive = IsActive;
      this.Date = Date;
    }

    // Token: 0x17000637 RID: 1591
    // (get) Token: 0x060021A5 RID: 8613 RVA: 0x0061D5F0 File Offset: 0x0061D5F0
    // (set) Token: 0x060021A6 RID: 8614 RVA: 0x0061D5F8 File Offset: 0x0061D5F8
    public bool IsActive { get; set; }

    // Token: 0x17000638 RID: 1592
    // (get) Token: 0x060021A7 RID: 8615 RVA: 0x0061D604 File Offset: 0x0061D604
    // (set) Token: 0x060021A8 RID: 8616 RVA: 0x0061D60C File Offset: 0x0061D60C
    public DateTimeOffset Date { get; set; }

    // Token: 0x060021A9 RID: 8617 RVA: 0x0061D618 File Offset: 0x0061D618
    public int CompareTo(DockRecency other)
    {
      if (this.IsActive == other.IsActive)
      {
        return this.Date.CompareTo(other.Date);
      }
      if (!this.IsActive)
      {
        return -1;
      }
      return 1;
    }

    // Token: 0x060021AA RID: 8618 RVA: 0x0061D658 File Offset: 0x0061D658
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DockRecency");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060021AB RID: 8619 RVA: 0x0061D6A4 File Offset: 0x0061D6A4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("IsActive = ");
      builder.Append(this.IsActive.ToString());
      builder.Append(", Date = ");
      builder.Append(this.Date.ToString());
      return true;
    }

    // Token: 0x060021AC RID: 8620 RVA: 0x0061D700 File Offset: 0x0061D700
    [CompilerGenerated]
    public static bool operator !=(DockRecency left, DockRecency right)
    {
      return !(left == right);
    }

    // Token: 0x060021AD RID: 8621 RVA: 0x0061D70C File Offset: 0x0061D70C
    [CompilerGenerated]
    public static bool operator ==(DockRecency left, DockRecency right)
    {
      return left.Equals(right);
    }

    // Token: 0x060021AE RID: 8622 RVA: 0x0061D718 File Offset: 0x0061D718
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<bool>.Default.GetHashCode(this.<IsActive>k__BackingField) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<Date>k__BackingField);
    }

    // Token: 0x060021AF RID: 8623 RVA: 0x0061D744 File Offset: 0x0061D744
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is DockRecency && this.Equals((DockRecency)obj);
    }

    // Token: 0x060021B0 RID: 8624 RVA: 0x0061D75C File Offset: 0x0061D75C
    [CompilerGenerated]
    public bool Equals(DockRecency other)
    {
      return EqualityComparer<bool>.Default.Equals(this.<IsActive>k__BackingField, other.<IsActive>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<Date>k__BackingField, other.<Date>k__BackingField);
    }

    // Token: 0x060021B1 RID: 8625 RVA: 0x0061D790 File Offset: 0x0061D790
    [CompilerGenerated]
    public void Deconstruct(out bool IsActive, out DateTimeOffset Date)
    {
      IsActive = this.IsActive;
      Date = this.Date;
    }
  }
}
