using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001BB RID: 443
  [NullableContext(1)]
  [Nullable(0)]
  public abstract class ClickyInkNoteTarget : IEquatable<ClickyInkNoteTarget>
  {
    // Token: 0x1700042C RID: 1068
    // (get) Token: 0x06001767 RID: 5991 RVA: 0x005C07DC File Offset: 0x005C07DC
    [CompilerGenerated]
    protected virtual Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkNoteTarget);
      }
    }

    // Token: 0x06001768 RID: 5992 RVA: 0x005C07E8 File Offset: 0x005C07E8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkNoteTarget");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001769 RID: 5993 RVA: 0x005C0834 File Offset: 0x005C0834
    [CompilerGenerated]
    protected virtual bool PrintMembers(StringBuilder builder)
    {
      return false;
    }

    // Token: 0x0600176A RID: 5994 RVA: 0x005C0838 File Offset: 0x005C0838
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkNoteTarget left, ClickyInkNoteTarget right)
    {
      return !(left == right);
    }

    // Token: 0x0600176B RID: 5995 RVA: 0x005C0844 File Offset: 0x005C0844
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkNoteTarget left, ClickyInkNoteTarget right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600176C RID: 5996 RVA: 0x005C0858 File Offset: 0x005C0858
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
    }

    // Token: 0x0600176D RID: 5997 RVA: 0x005C086C File Offset: 0x005C086C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkNoteTarget);
    }

    // Token: 0x0600176E RID: 5998 RVA: 0x005C087C File Offset: 0x005C087C
    [NullableContext(2)]
    [CompilerGenerated]
    public virtual bool Equals(ClickyInkNoteTarget other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract);
    }

    // Token: 0x06001770 RID: 6000 RVA: 0x005C089C File Offset: 0x005C089C
    [CompilerGenerated]
    protected ClickyInkNoteTarget(ClickyInkNoteTarget original)
    {
    }

    // Token: 0x06001771 RID: 6001 RVA: 0x005C08A4 File Offset: 0x005C08A4
    protected ClickyInkNoteTarget()
    {
    }

    // Token: 0x02000777 RID: 1911
    [Nullable(0)]
    public sealed class Clicky : ClickyInkNoteTarget, IEquatable<ClickyInkNoteTarget.Clicky>
    {
      // Token: 0x06003567 RID: 13671 RVA: 0x0067C4FC File Offset: 0x0067C4FC
      public Clicky(string Slug)
      {
        this.Slug = Slug;
        base..ctor();
      }

      // Token: 0x170007B6 RID: 1974
      // (get) Token: 0x06003568 RID: 13672 RVA: 0x0067C50C File Offset: 0x0067C50C
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkNoteTarget.Clicky);
        }
      }

      // Token: 0x170007B7 RID: 1975
      // (get) Token: 0x06003569 RID: 13673 RVA: 0x0067C518 File Offset: 0x0067C518
      // (set) Token: 0x0600356A RID: 13674 RVA: 0x0067C520 File Offset: 0x0067C520
      public string Slug { get; set; }

      // Token: 0x0600356B RID: 13675 RVA: 0x0067C52C File Offset: 0x0067C52C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Clicky");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600356C RID: 13676 RVA: 0x0067C578 File Offset: 0x0067C578
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Slug = ");
        builder.Append(this.Slug);
        return true;
      }

      // Token: 0x0600356D RID: 13677 RVA: 0x0067C5AC File Offset: 0x0067C5AC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkNoteTarget.Clicky left, ClickyInkNoteTarget.Clicky right)
      {
        return !(left == right);
      }

      // Token: 0x0600356E RID: 13678 RVA: 0x0067C5B8 File Offset: 0x0067C5B8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkNoteTarget.Clicky left, ClickyInkNoteTarget.Clicky right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600356F RID: 13679 RVA: 0x0067C5CC File Offset: 0x0067C5CC
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Slug>k__BackingField);
      }

      // Token: 0x06003570 RID: 13680 RVA: 0x0067C5EC File Offset: 0x0067C5EC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkNoteTarget.Clicky);
      }

      // Token: 0x06003571 RID: 13681 RVA: 0x0067C5FC File Offset: 0x0067C5FC
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ClickyInkNoteTarget other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003572 RID: 13682 RVA: 0x0067C608 File Offset: 0x0067C608
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkNoteTarget.Clicky other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Slug>k__BackingField, other.<Slug>k__BackingField));
      }

      // Token: 0x06003574 RID: 13684 RVA: 0x0067C63C File Offset: 0x0067C63C
      [CompilerGenerated]
      private Clicky(ClickyInkNoteTarget.Clicky original)
        : base(original)
      {
        this.Slug = original.<Slug>k__BackingField;
      }

      // Token: 0x06003575 RID: 13685 RVA: 0x0067C654 File Offset: 0x0067C654
      [CompilerGenerated]
      public void Deconstruct(out string Slug)
      {
        Slug = this.Slug;
      }
    }

    // Token: 0x02000778 RID: 1912
    [Nullable(0)]
    public sealed class Job : ClickyInkNoteTarget, IEquatable<ClickyInkNoteTarget.Job>
    {
      // Token: 0x06003576 RID: 13686 RVA: 0x0067C660 File Offset: 0x0067C660
      public Job(string Id)
      {
        this.Id = Id;
        base..ctor();
      }

      // Token: 0x170007B8 RID: 1976
      // (get) Token: 0x06003577 RID: 13687 RVA: 0x0067C670 File Offset: 0x0067C670
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkNoteTarget.Job);
        }
      }

      // Token: 0x170007B9 RID: 1977
      // (get) Token: 0x06003578 RID: 13688 RVA: 0x0067C67C File Offset: 0x0067C67C
      // (set) Token: 0x06003579 RID: 13689 RVA: 0x0067C684 File Offset: 0x0067C684
      public string Id { get; set; }

      // Token: 0x0600357A RID: 13690 RVA: 0x0067C690 File Offset: 0x0067C690
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Job");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600357B RID: 13691 RVA: 0x0067C6DC File Offset: 0x0067C6DC
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Id = ");
        builder.Append(this.Id);
        return true;
      }

      // Token: 0x0600357C RID: 13692 RVA: 0x0067C710 File Offset: 0x0067C710
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkNoteTarget.Job left, ClickyInkNoteTarget.Job right)
      {
        return !(left == right);
      }

      // Token: 0x0600357D RID: 13693 RVA: 0x0067C71C File Offset: 0x0067C71C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkNoteTarget.Job left, ClickyInkNoteTarget.Job right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600357E RID: 13694 RVA: 0x0067C730 File Offset: 0x0067C730
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField);
      }

      // Token: 0x0600357F RID: 13695 RVA: 0x0067C750 File Offset: 0x0067C750
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkNoteTarget.Job);
      }

      // Token: 0x06003580 RID: 13696 RVA: 0x0067C760 File Offset: 0x0067C760
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ClickyInkNoteTarget other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003581 RID: 13697 RVA: 0x0067C76C File Offset: 0x0067C76C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkNoteTarget.Job other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField));
      }

      // Token: 0x06003583 RID: 13699 RVA: 0x0067C7A0 File Offset: 0x0067C7A0
      [CompilerGenerated]
      private Job(ClickyInkNoteTarget.Job original)
        : base(original)
      {
        this.Id = original.<Id>k__BackingField;
      }

      // Token: 0x06003584 RID: 13700 RVA: 0x0067C7B8 File Offset: 0x0067C7B8
      [CompilerGenerated]
      public void Deconstruct(out string Id)
      {
        Id = this.Id;
      }
    }
  }
}
