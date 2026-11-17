using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Drive
{
  // Token: 0x02000191 RID: 401
  [NullableContext(1)]
  [Nullable(0)]
  public abstract class MacStepAnswer : IEquatable<MacStepAnswer>
  {
    // Token: 0x17000558 RID: 1368
    // (get) Token: 0x060017C0 RID: 6080 RVA: 0x003C5844 File Offset: 0x003C5844
    [CompilerGenerated]
    protected virtual Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(MacStepAnswer);
      }
    }

    // Token: 0x060017C1 RID: 6081 RVA: 0x003C5850 File Offset: 0x003C5850
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("MacStepAnswer");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060017C2 RID: 6082 RVA: 0x003C589C File Offset: 0x003C589C
    [CompilerGenerated]
    protected virtual bool PrintMembers(StringBuilder builder)
    {
      return false;
    }

    // Token: 0x060017C3 RID: 6083 RVA: 0x003C58A0 File Offset: 0x003C58A0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(MacStepAnswer left, MacStepAnswer right)
    {
      return !(left == right);
    }

    // Token: 0x060017C4 RID: 6084 RVA: 0x003C58AC File Offset: 0x003C58AC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(MacStepAnswer left, MacStepAnswer right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060017C5 RID: 6085 RVA: 0x003C58C0 File Offset: 0x003C58C0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
    }

    // Token: 0x060017C6 RID: 6086 RVA: 0x003C58D4 File Offset: 0x003C58D4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as MacStepAnswer);
    }

    // Token: 0x060017C7 RID: 6087 RVA: 0x003C58E4 File Offset: 0x003C58E4
    [NullableContext(2)]
    [CompilerGenerated]
    public virtual bool Equals(MacStepAnswer other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract);
    }

    // Token: 0x060017C9 RID: 6089 RVA: 0x003C5904 File Offset: 0x003C5904
    [CompilerGenerated]
    protected MacStepAnswer(MacStepAnswer original)
    {
    }

    // Token: 0x060017CA RID: 6090 RVA: 0x003C590C File Offset: 0x003C590C
    protected MacStepAnswer()
    {
    }

    // Token: 0x04000A3C RID: 2620
    public static readonly MacStepAnswer DeclinedAnswer = new MacStepAnswer.Declined();

    // Token: 0x04000A3D RID: 2621
    public static readonly MacStepAnswer NoAnswerAnswer = new MacStepAnswer.NoAnswer();

    // Token: 0x04000A3E RID: 2622
    public static readonly MacStepAnswer NotYetAnswer = new MacStepAnswer.NotYet();

    // Token: 0x04000A3F RID: 2623
    public static readonly MacStepAnswer BeingDrivenAnswer = new MacStepAnswer.BeingDriven();

    // Token: 0x04000A40 RID: 2624
    public static readonly MacStepAnswer CouldNotStartAnswer = new MacStepAnswer.CouldNotStart();

    // Token: 0x0200056E RID: 1390
    [Nullable(0)]
    public sealed class Drove : MacStepAnswer, IEquatable<MacStepAnswer.Drove>
    {
      // Token: 0x060034D5 RID: 13525 RVA: 0x00440ADC File Offset: 0x00440ADC
      public Drove(string Report)
      {
        this.Report = Report;
        base..ctor();
      }

      // Token: 0x1700095C RID: 2396
      // (get) Token: 0x060034D6 RID: 13526 RVA: 0x00440AEC File Offset: 0x00440AEC
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(MacStepAnswer.Drove);
        }
      }

      // Token: 0x1700095D RID: 2397
      // (get) Token: 0x060034D7 RID: 13527 RVA: 0x00440AF8 File Offset: 0x00440AF8
      // (set) Token: 0x060034D8 RID: 13528 RVA: 0x00440B00 File Offset: 0x00440B00
      public string Report { get; set; }

      // Token: 0x060034D9 RID: 13529 RVA: 0x00440B0C File Offset: 0x00440B0C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Drove");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060034DA RID: 13530 RVA: 0x00440B58 File Offset: 0x00440B58
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Report = ");
        builder.Append(this.Report);
        return true;
      }

      // Token: 0x060034DB RID: 13531 RVA: 0x00440B8C File Offset: 0x00440B8C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(MacStepAnswer.Drove left, MacStepAnswer.Drove right)
      {
        return !(left == right);
      }

      // Token: 0x060034DC RID: 13532 RVA: 0x00440B98 File Offset: 0x00440B98
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(MacStepAnswer.Drove left, MacStepAnswer.Drove right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060034DD RID: 13533 RVA: 0x00440BAC File Offset: 0x00440BAC
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Report>k__BackingField);
      }

      // Token: 0x060034DE RID: 13534 RVA: 0x00440BCC File Offset: 0x00440BCC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as MacStepAnswer.Drove);
      }

      // Token: 0x060034DF RID: 13535 RVA: 0x00440BDC File Offset: 0x00440BDC
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(MacStepAnswer other)
      {
        return this.Equals(other);
      }

      // Token: 0x060034E0 RID: 13536 RVA: 0x00440BE8 File Offset: 0x00440BE8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(MacStepAnswer.Drove other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Report>k__BackingField, other.<Report>k__BackingField));
      }

      // Token: 0x060034E2 RID: 13538 RVA: 0x00440C1C File Offset: 0x00440C1C
      [CompilerGenerated]
      private Drove(MacStepAnswer.Drove original)
        : base(original)
      {
        this.Report = original.<Report>k__BackingField;
      }

      // Token: 0x060034E3 RID: 13539 RVA: 0x00440C34 File Offset: 0x00440C34
      [CompilerGenerated]
      public void Deconstruct(out string Report)
      {
        Report = this.Report;
      }
    }

    // Token: 0x0200056F RID: 1391
    [Nullable(0)]
    public sealed class Declined : MacStepAnswer, IEquatable<MacStepAnswer.Declined>
    {
      // Token: 0x1700095E RID: 2398
      // (get) Token: 0x060034E4 RID: 13540 RVA: 0x00440C40 File Offset: 0x00440C40
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(MacStepAnswer.Declined);
        }
      }

      // Token: 0x060034E5 RID: 13541 RVA: 0x00440C4C File Offset: 0x00440C4C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Declined");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060034E6 RID: 13542 RVA: 0x00440C98 File Offset: 0x00440C98
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x060034E7 RID: 13543 RVA: 0x00440CA4 File Offset: 0x00440CA4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(MacStepAnswer.Declined left, MacStepAnswer.Declined right)
      {
        return !(left == right);
      }

      // Token: 0x060034E8 RID: 13544 RVA: 0x00440CB0 File Offset: 0x00440CB0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(MacStepAnswer.Declined left, MacStepAnswer.Declined right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060034E9 RID: 13545 RVA: 0x00440CC4 File Offset: 0x00440CC4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x060034EA RID: 13546 RVA: 0x00440CCC File Offset: 0x00440CCC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as MacStepAnswer.Declined);
      }

      // Token: 0x060034EB RID: 13547 RVA: 0x00440CDC File Offset: 0x00440CDC
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(MacStepAnswer other)
      {
        return this.Equals(other);
      }

      // Token: 0x060034EC RID: 13548 RVA: 0x00440CE8 File Offset: 0x00440CE8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(MacStepAnswer.Declined other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x060034EE RID: 13550 RVA: 0x00440D00 File Offset: 0x00440D00
      [CompilerGenerated]
      private Declined(MacStepAnswer.Declined original)
        : base(original)
      {
      }

      // Token: 0x060034EF RID: 13551 RVA: 0x00440D0C File Offset: 0x00440D0C
      public Declined()
      {
      }
    }

    // Token: 0x02000570 RID: 1392
    [Nullable(0)]
    public sealed class NoAnswer : MacStepAnswer, IEquatable<MacStepAnswer.NoAnswer>
    {
      // Token: 0x1700095F RID: 2399
      // (get) Token: 0x060034F0 RID: 13552 RVA: 0x00440D14 File Offset: 0x00440D14
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(MacStepAnswer.NoAnswer);
        }
      }

      // Token: 0x060034F1 RID: 13553 RVA: 0x00440D20 File Offset: 0x00440D20
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("NoAnswer");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060034F2 RID: 13554 RVA: 0x00440D6C File Offset: 0x00440D6C
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x060034F3 RID: 13555 RVA: 0x00440D78 File Offset: 0x00440D78
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(MacStepAnswer.NoAnswer left, MacStepAnswer.NoAnswer right)
      {
        return !(left == right);
      }

      // Token: 0x060034F4 RID: 13556 RVA: 0x00440D84 File Offset: 0x00440D84
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(MacStepAnswer.NoAnswer left, MacStepAnswer.NoAnswer right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060034F5 RID: 13557 RVA: 0x00440D98 File Offset: 0x00440D98
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x060034F6 RID: 13558 RVA: 0x00440DA0 File Offset: 0x00440DA0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as MacStepAnswer.NoAnswer);
      }

      // Token: 0x060034F7 RID: 13559 RVA: 0x00440DB0 File Offset: 0x00440DB0
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(MacStepAnswer other)
      {
        return this.Equals(other);
      }

      // Token: 0x060034F8 RID: 13560 RVA: 0x00440DBC File Offset: 0x00440DBC
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(MacStepAnswer.NoAnswer other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x060034FA RID: 13562 RVA: 0x00440DD4 File Offset: 0x00440DD4
      [CompilerGenerated]
      private NoAnswer(MacStepAnswer.NoAnswer original)
        : base(original)
      {
      }

      // Token: 0x060034FB RID: 13563 RVA: 0x00440DE0 File Offset: 0x00440DE0
      public NoAnswer()
      {
      }
    }

    // Token: 0x02000571 RID: 1393
    [Nullable(0)]
    public sealed class NotYet : MacStepAnswer, IEquatable<MacStepAnswer.NotYet>
    {
      // Token: 0x17000960 RID: 2400
      // (get) Token: 0x060034FC RID: 13564 RVA: 0x00440DE8 File Offset: 0x00440DE8
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(MacStepAnswer.NotYet);
        }
      }

      // Token: 0x060034FD RID: 13565 RVA: 0x00440DF4 File Offset: 0x00440DF4
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("NotYet");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060034FE RID: 13566 RVA: 0x00440E40 File Offset: 0x00440E40
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x060034FF RID: 13567 RVA: 0x00440E4C File Offset: 0x00440E4C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(MacStepAnswer.NotYet left, MacStepAnswer.NotYet right)
      {
        return !(left == right);
      }

      // Token: 0x06003500 RID: 13568 RVA: 0x00440E58 File Offset: 0x00440E58
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(MacStepAnswer.NotYet left, MacStepAnswer.NotYet right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003501 RID: 13569 RVA: 0x00440E6C File Offset: 0x00440E6C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x06003502 RID: 13570 RVA: 0x00440E74 File Offset: 0x00440E74
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as MacStepAnswer.NotYet);
      }

      // Token: 0x06003503 RID: 13571 RVA: 0x00440E84 File Offset: 0x00440E84
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(MacStepAnswer other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003504 RID: 13572 RVA: 0x00440E90 File Offset: 0x00440E90
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(MacStepAnswer.NotYet other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x06003506 RID: 13574 RVA: 0x00440EA8 File Offset: 0x00440EA8
      [CompilerGenerated]
      private NotYet(MacStepAnswer.NotYet original)
        : base(original)
      {
      }

      // Token: 0x06003507 RID: 13575 RVA: 0x00440EB4 File Offset: 0x00440EB4
      public NotYet()
      {
      }
    }

    // Token: 0x02000572 RID: 1394
    [Nullable(0)]
    public sealed class BeingDriven : MacStepAnswer, IEquatable<MacStepAnswer.BeingDriven>
    {
      // Token: 0x17000961 RID: 2401
      // (get) Token: 0x06003508 RID: 13576 RVA: 0x00440EBC File Offset: 0x00440EBC
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(MacStepAnswer.BeingDriven);
        }
      }

      // Token: 0x06003509 RID: 13577 RVA: 0x00440EC8 File Offset: 0x00440EC8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("BeingDriven");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600350A RID: 13578 RVA: 0x00440F14 File Offset: 0x00440F14
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x0600350B RID: 13579 RVA: 0x00440F20 File Offset: 0x00440F20
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(MacStepAnswer.BeingDriven left, MacStepAnswer.BeingDriven right)
      {
        return !(left == right);
      }

      // Token: 0x0600350C RID: 13580 RVA: 0x00440F2C File Offset: 0x00440F2C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(MacStepAnswer.BeingDriven left, MacStepAnswer.BeingDriven right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600350D RID: 13581 RVA: 0x00440F40 File Offset: 0x00440F40
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x0600350E RID: 13582 RVA: 0x00440F48 File Offset: 0x00440F48
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as MacStepAnswer.BeingDriven);
      }

      // Token: 0x0600350F RID: 13583 RVA: 0x00440F58 File Offset: 0x00440F58
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(MacStepAnswer other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003510 RID: 13584 RVA: 0x00440F64 File Offset: 0x00440F64
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(MacStepAnswer.BeingDriven other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x06003512 RID: 13586 RVA: 0x00440F7C File Offset: 0x00440F7C
      [CompilerGenerated]
      private BeingDriven(MacStepAnswer.BeingDriven original)
        : base(original)
      {
      }

      // Token: 0x06003513 RID: 13587 RVA: 0x00440F88 File Offset: 0x00440F88
      public BeingDriven()
      {
      }
    }

    // Token: 0x02000573 RID: 1395
    [Nullable(0)]
    public sealed class CouldNotStart : MacStepAnswer, IEquatable<MacStepAnswer.CouldNotStart>
    {
      // Token: 0x17000962 RID: 2402
      // (get) Token: 0x06003514 RID: 13588 RVA: 0x00440F90 File Offset: 0x00440F90
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(MacStepAnswer.CouldNotStart);
        }
      }

      // Token: 0x06003515 RID: 13589 RVA: 0x00440F9C File Offset: 0x00440F9C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("CouldNotStart");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003516 RID: 13590 RVA: 0x00440FE8 File Offset: 0x00440FE8
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x06003517 RID: 13591 RVA: 0x00440FF4 File Offset: 0x00440FF4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(MacStepAnswer.CouldNotStart left, MacStepAnswer.CouldNotStart right)
      {
        return !(left == right);
      }

      // Token: 0x06003518 RID: 13592 RVA: 0x00441000 File Offset: 0x00441000
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(MacStepAnswer.CouldNotStart left, MacStepAnswer.CouldNotStart right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003519 RID: 13593 RVA: 0x00441014 File Offset: 0x00441014
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x0600351A RID: 13594 RVA: 0x0044101C File Offset: 0x0044101C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as MacStepAnswer.CouldNotStart);
      }

      // Token: 0x0600351B RID: 13595 RVA: 0x0044102C File Offset: 0x0044102C
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(MacStepAnswer other)
      {
        return this.Equals(other);
      }

      // Token: 0x0600351C RID: 13596 RVA: 0x00441038 File Offset: 0x00441038
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(MacStepAnswer.CouldNotStart other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x0600351E RID: 13598 RVA: 0x00441050 File Offset: 0x00441050
      [CompilerGenerated]
      private CouldNotStart(MacStepAnswer.CouldNotStart original)
        : base(original)
      {
      }

      // Token: 0x0600351F RID: 13599 RVA: 0x0044105C File Offset: 0x0044105C
      public CouldNotStart()
      {
      }
    }
  }
}
