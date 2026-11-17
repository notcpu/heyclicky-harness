using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200020B RID: 523
  [NullableContext(1)]
  [Nullable(0)]
  public abstract class ClickyEdgeStackInkPresentation : IEquatable<ClickyEdgeStackInkPresentation>
  {
    // Token: 0x1700046E RID: 1134
    // (get) Token: 0x06001910 RID: 6416 RVA: 0x005CEAE4 File Offset: 0x005CEAE4
    [CompilerGenerated]
    protected virtual Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyEdgeStackInkPresentation);
      }
    }

    // Token: 0x06001911 RID: 6417 RVA: 0x005CEAF0 File Offset: 0x005CEAF0
    public static ClickyEdgeStackInkPresentation InFan(bool isFront)
    {
      return new ClickyEdgeStackInkPresentation.Fanned(isFront);
    }

    // Token: 0x1700046F RID: 1135
    // (get) Token: 0x06001912 RID: 6418 RVA: 0x005CEAF8 File Offset: 0x005CEAF8
    public bool LetsThePenMove
    {
      get
      {
        if (!(this is ClickyEdgeStackInkPresentation.Pulled))
        {
          ClickyEdgeStackInkPresentation.Fanned fanned = this as ClickyEdgeStackInkPresentation.Fanned;
          return fanned != null && fanned.IsFront;
        }
        return true;
      }
    }

    // Token: 0x06001913 RID: 6419 RVA: 0x005CEB24 File Offset: 0x005CEB24
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyEdgeStackInkPresentation");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001914 RID: 6420 RVA: 0x005CEB70 File Offset: 0x005CEB70
    [CompilerGenerated]
    protected virtual bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("LetsThePenMove = ");
      builder.Append(this.LetsThePenMove.ToString());
      return true;
    }

    // Token: 0x06001915 RID: 6421 RVA: 0x005CEBAC File Offset: 0x005CEBAC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyEdgeStackInkPresentation left, ClickyEdgeStackInkPresentation right)
    {
      return !(left == right);
    }

    // Token: 0x06001916 RID: 6422 RVA: 0x005CEBB8 File Offset: 0x005CEBB8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyEdgeStackInkPresentation left, ClickyEdgeStackInkPresentation right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001917 RID: 6423 RVA: 0x005CEBCC File Offset: 0x005CEBCC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
    }

    // Token: 0x06001918 RID: 6424 RVA: 0x005CEBE0 File Offset: 0x005CEBE0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyEdgeStackInkPresentation);
    }

    // Token: 0x06001919 RID: 6425 RVA: 0x005CEBF0 File Offset: 0x005CEBF0
    [NullableContext(2)]
    [CompilerGenerated]
    public virtual bool Equals(ClickyEdgeStackInkPresentation other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract);
    }

    // Token: 0x0600191B RID: 6427 RVA: 0x005CEC10 File Offset: 0x005CEC10
    [CompilerGenerated]
    protected ClickyEdgeStackInkPresentation(ClickyEdgeStackInkPresentation original)
    {
    }

    // Token: 0x0600191C RID: 6428 RVA: 0x005CEC18 File Offset: 0x005CEC18
    protected ClickyEdgeStackInkPresentation()
    {
    }

    // Token: 0x04000C38 RID: 3128
    public static readonly ClickyEdgeStackInkPresentation AtRest = new ClickyEdgeStackInkPresentation.Rest();

    // Token: 0x04000C39 RID: 3129
    public static readonly ClickyEdgeStackInkPresentation PulledOut = new ClickyEdgeStackInkPresentation.Pulled();

    // Token: 0x020007A9 RID: 1961
    [Nullable(0)]
    public sealed class Rest : ClickyEdgeStackInkPresentation, IEquatable<ClickyEdgeStackInkPresentation.Rest>
    {
      // Token: 0x170007DD RID: 2013
      // (get) Token: 0x06003678 RID: 13944 RVA: 0x0067E6F0 File Offset: 0x0067E6F0
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyEdgeStackInkPresentation.Rest);
        }
      }

      // Token: 0x06003679 RID: 13945 RVA: 0x0067E6FC File Offset: 0x0067E6FC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Rest");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600367A RID: 13946 RVA: 0x0067E748 File Offset: 0x0067E748
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x0600367B RID: 13947 RVA: 0x0067E754 File Offset: 0x0067E754
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyEdgeStackInkPresentation.Rest left, ClickyEdgeStackInkPresentation.Rest right)
      {
        return !(left == right);
      }

      // Token: 0x0600367C RID: 13948 RVA: 0x0067E760 File Offset: 0x0067E760
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyEdgeStackInkPresentation.Rest left, ClickyEdgeStackInkPresentation.Rest right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600367D RID: 13949 RVA: 0x0067E774 File Offset: 0x0067E774
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x0600367E RID: 13950 RVA: 0x0067E77C File Offset: 0x0067E77C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyEdgeStackInkPresentation.Rest);
      }

      // Token: 0x0600367F RID: 13951 RVA: 0x0067E78C File Offset: 0x0067E78C
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ClickyEdgeStackInkPresentation other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003680 RID: 13952 RVA: 0x0067E798 File Offset: 0x0067E798
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyEdgeStackInkPresentation.Rest other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x06003682 RID: 13954 RVA: 0x0067E7B0 File Offset: 0x0067E7B0
      [CompilerGenerated]
      private Rest(ClickyEdgeStackInkPresentation.Rest original)
        : base(original)
      {
      }

      // Token: 0x06003683 RID: 13955 RVA: 0x0067E7BC File Offset: 0x0067E7BC
      public Rest()
      {
      }
    }

    // Token: 0x020007AA RID: 1962
    [Nullable(0)]
    public sealed class Fanned : ClickyEdgeStackInkPresentation, IEquatable<ClickyEdgeStackInkPresentation.Fanned>
    {
      // Token: 0x06003684 RID: 13956 RVA: 0x0067E7C4 File Offset: 0x0067E7C4
      public Fanned(bool IsFront)
      {
        this.IsFront = IsFront;
        base..ctor();
      }

      // Token: 0x170007DE RID: 2014
      // (get) Token: 0x06003685 RID: 13957 RVA: 0x0067E7D4 File Offset: 0x0067E7D4
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyEdgeStackInkPresentation.Fanned);
        }
      }

      // Token: 0x170007DF RID: 2015
      // (get) Token: 0x06003686 RID: 13958 RVA: 0x0067E7E0 File Offset: 0x0067E7E0
      // (set) Token: 0x06003687 RID: 13959 RVA: 0x0067E7E8 File Offset: 0x0067E7E8
      public bool IsFront { get; set; }

      // Token: 0x06003688 RID: 13960 RVA: 0x0067E7F4 File Offset: 0x0067E7F4
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Fanned");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003689 RID: 13961 RVA: 0x0067E840 File Offset: 0x0067E840
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("IsFront = ");
        builder.Append(this.IsFront.ToString());
        return true;
      }

      // Token: 0x0600368A RID: 13962 RVA: 0x0067E88C File Offset: 0x0067E88C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyEdgeStackInkPresentation.Fanned left, ClickyEdgeStackInkPresentation.Fanned right)
      {
        return !(left == right);
      }

      // Token: 0x0600368B RID: 13963 RVA: 0x0067E898 File Offset: 0x0067E898
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyEdgeStackInkPresentation.Fanned left, ClickyEdgeStackInkPresentation.Fanned right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600368C RID: 13964 RVA: 0x0067E8AC File Offset: 0x0067E8AC
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsFront>k__BackingField);
      }

      // Token: 0x0600368D RID: 13965 RVA: 0x0067E8CC File Offset: 0x0067E8CC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyEdgeStackInkPresentation.Fanned);
      }

      // Token: 0x0600368E RID: 13966 RVA: 0x0067E8DC File Offset: 0x0067E8DC
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ClickyEdgeStackInkPresentation other)
      {
        return this.Equals(other);
      }

      // Token: 0x0600368F RID: 13967 RVA: 0x0067E8E8 File Offset: 0x0067E8E8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyEdgeStackInkPresentation.Fanned other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<bool>.Default.Equals(this.<IsFront>k__BackingField, other.<IsFront>k__BackingField));
      }

      // Token: 0x06003691 RID: 13969 RVA: 0x0067E91C File Offset: 0x0067E91C
      [CompilerGenerated]
      private Fanned(ClickyEdgeStackInkPresentation.Fanned original)
        : base(original)
      {
        this.IsFront = original.<IsFront>k__BackingField;
      }

      // Token: 0x06003692 RID: 13970 RVA: 0x0067E934 File Offset: 0x0067E934
      [CompilerGenerated]
      public void Deconstruct(out bool IsFront)
      {
        IsFront = this.IsFront;
      }
    }

    // Token: 0x020007AB RID: 1963
    [Nullable(0)]
    public sealed class Pulled : ClickyEdgeStackInkPresentation, IEquatable<ClickyEdgeStackInkPresentation.Pulled>
    {
      // Token: 0x170007E0 RID: 2016
      // (get) Token: 0x06003693 RID: 13971 RVA: 0x0067E940 File Offset: 0x0067E940
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyEdgeStackInkPresentation.Pulled);
        }
      }

      // Token: 0x06003694 RID: 13972 RVA: 0x0067E94C File Offset: 0x0067E94C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Pulled");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003695 RID: 13973 RVA: 0x0067E998 File Offset: 0x0067E998
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x06003696 RID: 13974 RVA: 0x0067E9A4 File Offset: 0x0067E9A4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyEdgeStackInkPresentation.Pulled left, ClickyEdgeStackInkPresentation.Pulled right)
      {
        return !(left == right);
      }

      // Token: 0x06003697 RID: 13975 RVA: 0x0067E9B0 File Offset: 0x0067E9B0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyEdgeStackInkPresentation.Pulled left, ClickyEdgeStackInkPresentation.Pulled right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003698 RID: 13976 RVA: 0x0067E9C4 File Offset: 0x0067E9C4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x06003699 RID: 13977 RVA: 0x0067E9CC File Offset: 0x0067E9CC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyEdgeStackInkPresentation.Pulled);
      }

      // Token: 0x0600369A RID: 13978 RVA: 0x0067E9DC File Offset: 0x0067E9DC
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ClickyEdgeStackInkPresentation other)
      {
        return this.Equals(other);
      }

      // Token: 0x0600369B RID: 13979 RVA: 0x0067E9E8 File Offset: 0x0067E9E8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyEdgeStackInkPresentation.Pulled other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x0600369D RID: 13981 RVA: 0x0067EA00 File Offset: 0x0067EA00
      [CompilerGenerated]
      private Pulled(ClickyEdgeStackInkPresentation.Pulled original)
        : base(original)
      {
      }

      // Token: 0x0600369E RID: 13982 RVA: 0x0067EA0C File Offset: 0x0067EA0C
      public Pulled()
      {
      }
    }
  }
}
