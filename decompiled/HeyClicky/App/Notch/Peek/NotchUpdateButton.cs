using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000189 RID: 393
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchUpdateButton : IEquatable<NotchUpdateButton>
  {
    // Token: 0x0600155A RID: 5466 RVA: 0x005B4CE4 File Offset: 0x005B4CE4
    public NotchUpdateButton(string Title, bool IsPrimary, Action Action)
    {
      this.Title = Title;
      this.IsPrimary = IsPrimary;
      this.Action = Action;
      base..ctor();
    }

    // Token: 0x170003D7 RID: 983
    // (get) Token: 0x0600155B RID: 5467 RVA: 0x005B4D04 File Offset: 0x005B4D04
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(NotchUpdateButton);
      }
    }

    // Token: 0x170003D8 RID: 984
    // (get) Token: 0x0600155C RID: 5468 RVA: 0x005B4D10 File Offset: 0x005B4D10
    // (set) Token: 0x0600155D RID: 5469 RVA: 0x005B4D18 File Offset: 0x005B4D18
    public string Title { get; set; }

    // Token: 0x170003D9 RID: 985
    // (get) Token: 0x0600155E RID: 5470 RVA: 0x005B4D24 File Offset: 0x005B4D24
    // (set) Token: 0x0600155F RID: 5471 RVA: 0x005B4D2C File Offset: 0x005B4D2C
    public bool IsPrimary { get; set; }

    // Token: 0x170003DA RID: 986
    // (get) Token: 0x06001560 RID: 5472 RVA: 0x005B4D38 File Offset: 0x005B4D38
    // (set) Token: 0x06001561 RID: 5473 RVA: 0x005B4D40 File Offset: 0x005B4D40
    public Action Action { get; set; }

    // Token: 0x06001562 RID: 5474 RVA: 0x005B4D4C File Offset: 0x005B4D4C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("NotchUpdateButton");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001563 RID: 5475 RVA: 0x005B4D98 File Offset: 0x005B4D98
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Title = ");
      builder.Append(this.Title);
      builder.Append(", IsPrimary = ");
      builder.Append(this.IsPrimary.ToString());
      builder.Append(", Action = ");
      builder.Append(this.Action);
      return true;
    }

    // Token: 0x06001564 RID: 5476 RVA: 0x005B4E04 File Offset: 0x005B4E04
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(NotchUpdateButton left, NotchUpdateButton right)
    {
      return !(left == right);
    }

    // Token: 0x06001565 RID: 5477 RVA: 0x005B4E10 File Offset: 0x005B4E10
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(NotchUpdateButton left, NotchUpdateButton right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001566 RID: 5478 RVA: 0x005B4E24 File Offset: 0x005B4E24
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsPrimary>k__BackingField)) * -1521134295 + EqualityComparer<Action>.Default.GetHashCode(this.<Action>k__BackingField);
    }

    // Token: 0x06001567 RID: 5479 RVA: 0x005B4E88 File Offset: 0x005B4E88
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as NotchUpdateButton);
    }

    // Token: 0x06001568 RID: 5480 RVA: 0x005B4E98 File Offset: 0x005B4E98
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(NotchUpdateButton other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsPrimary>k__BackingField, other.<IsPrimary>k__BackingField) && EqualityComparer<Action>.Default.Equals(this.<Action>k__BackingField, other.<Action>k__BackingField));
    }

    // Token: 0x0600156A RID: 5482 RVA: 0x005B4F14 File Offset: 0x005B4F14
    [CompilerGenerated]
    private NotchUpdateButton(NotchUpdateButton original)
    {
      this.Title = original.<Title>k__BackingField;
      this.IsPrimary = original.<IsPrimary>k__BackingField;
      this.Action = original.<Action>k__BackingField;
    }

    // Token: 0x0600156B RID: 5483 RVA: 0x005B4F40 File Offset: 0x005B4F40
    [CompilerGenerated]
    public void Deconstruct(out string Title, out bool IsPrimary, out Action Action)
    {
      Title = this.Title;
      IsPrimary = this.IsPrimary;
      Action = this.Action;
    }
  }
}
