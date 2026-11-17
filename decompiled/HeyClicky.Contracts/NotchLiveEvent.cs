using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x0200002A RID: 42
  [NullableContext(1)]
  [Nullable(0)]
  public abstract class NotchLiveEvent : IEquatable<NotchLiveEvent>
  {
    // Token: 0x1700008C RID: 140
    // (get) Token: 0x06000211 RID: 529 RVA: 0x00020068 File Offset: 0x00020068
    [CompilerGenerated]
    protected virtual Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(NotchLiveEvent);
      }
    }

    // Token: 0x06000212 RID: 530 RVA: 0x00020074 File Offset: 0x00020074
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("NotchLiveEvent");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000213 RID: 531 RVA: 0x000200C0 File Offset: 0x000200C0
    [CompilerGenerated]
    protected virtual bool PrintMembers(StringBuilder builder)
    {
      return false;
    }

    // Token: 0x06000214 RID: 532 RVA: 0x000200C4 File Offset: 0x000200C4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(NotchLiveEvent left, NotchLiveEvent right)
    {
      return !(left == right);
    }

    // Token: 0x06000215 RID: 533 RVA: 0x000200D0 File Offset: 0x000200D0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(NotchLiveEvent left, NotchLiveEvent right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000216 RID: 534 RVA: 0x000200E4 File Offset: 0x000200E4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
    }

    // Token: 0x06000217 RID: 535 RVA: 0x000200F8 File Offset: 0x000200F8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as NotchLiveEvent);
    }

    // Token: 0x06000218 RID: 536 RVA: 0x00020108 File Offset: 0x00020108
    [NullableContext(2)]
    [CompilerGenerated]
    public virtual bool Equals(NotchLiveEvent other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract);
    }

    // Token: 0x0600021A RID: 538 RVA: 0x00020128 File Offset: 0x00020128
    [CompilerGenerated]
    protected NotchLiveEvent(NotchLiveEvent original)
    {
    }

    // Token: 0x0600021B RID: 539 RVA: 0x00020130 File Offset: 0x00020130
    protected NotchLiveEvent()
    {
    }

    // Token: 0x02000038 RID: 56
    [Nullable(0)]
    public sealed class ActivityListening : NotchLiveEvent, IEquatable<NotchLiveEvent.ActivityListening>
    {
      // Token: 0x170000AE RID: 174
      // (get) Token: 0x060002CD RID: 717 RVA: 0x0002214C File Offset: 0x0002214C
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.ActivityListening);
        }
      }

      // Token: 0x060002CE RID: 718 RVA: 0x00022158 File Offset: 0x00022158
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ActivityListening");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060002CF RID: 719 RVA: 0x000221A4 File Offset: 0x000221A4
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x060002D0 RID: 720 RVA: 0x000221B0 File Offset: 0x000221B0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.ActivityListening left, NotchLiveEvent.ActivityListening right)
      {
        return !(left == right);
      }

      // Token: 0x060002D1 RID: 721 RVA: 0x000221BC File Offset: 0x000221BC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.ActivityListening left, NotchLiveEvent.ActivityListening right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060002D2 RID: 722 RVA: 0x000221D0 File Offset: 0x000221D0
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x060002D3 RID: 723 RVA: 0x000221D8 File Offset: 0x000221D8
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.ActivityListening);
      }

      // Token: 0x060002D4 RID: 724 RVA: 0x000221E8 File Offset: 0x000221E8
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x060002D5 RID: 725 RVA: 0x000221F4 File Offset: 0x000221F4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.ActivityListening other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x060002D7 RID: 727 RVA: 0x0002220C File Offset: 0x0002220C
      [CompilerGenerated]
      private ActivityListening(NotchLiveEvent.ActivityListening original)
        : base(original)
      {
      }

      // Token: 0x060002D8 RID: 728 RVA: 0x00022218 File Offset: 0x00022218
      public ActivityListening()
      {
      }
    }

    // Token: 0x02000039 RID: 57
    [Nullable(0)]
    public sealed class ActivityThinking : NotchLiveEvent, IEquatable<NotchLiveEvent.ActivityThinking>
    {
      // Token: 0x170000AF RID: 175
      // (get) Token: 0x060002D9 RID: 729 RVA: 0x00022220 File Offset: 0x00022220
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.ActivityThinking);
        }
      }

      // Token: 0x060002DA RID: 730 RVA: 0x0002222C File Offset: 0x0002222C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ActivityThinking");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060002DB RID: 731 RVA: 0x00022278 File Offset: 0x00022278
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x060002DC RID: 732 RVA: 0x00022284 File Offset: 0x00022284
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.ActivityThinking left, NotchLiveEvent.ActivityThinking right)
      {
        return !(left == right);
      }

      // Token: 0x060002DD RID: 733 RVA: 0x00022290 File Offset: 0x00022290
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.ActivityThinking left, NotchLiveEvent.ActivityThinking right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060002DE RID: 734 RVA: 0x000222A4 File Offset: 0x000222A4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x060002DF RID: 735 RVA: 0x000222AC File Offset: 0x000222AC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.ActivityThinking);
      }

      // Token: 0x060002E0 RID: 736 RVA: 0x000222BC File Offset: 0x000222BC
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x060002E1 RID: 737 RVA: 0x000222C8 File Offset: 0x000222C8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.ActivityThinking other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x060002E3 RID: 739 RVA: 0x000222E0 File Offset: 0x000222E0
      [CompilerGenerated]
      private ActivityThinking(NotchLiveEvent.ActivityThinking original)
        : base(original)
      {
      }

      // Token: 0x060002E4 RID: 740 RVA: 0x000222EC File Offset: 0x000222EC
      public ActivityThinking()
      {
      }
    }

    // Token: 0x0200003A RID: 58
    [Nullable(0)]
    public sealed class ActivitySpeaking : NotchLiveEvent, IEquatable<NotchLiveEvent.ActivitySpeaking>
    {
      // Token: 0x170000B0 RID: 176
      // (get) Token: 0x060002E5 RID: 741 RVA: 0x000222F4 File Offset: 0x000222F4
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.ActivitySpeaking);
        }
      }

      // Token: 0x060002E6 RID: 742 RVA: 0x00022300 File Offset: 0x00022300
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ActivitySpeaking");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060002E7 RID: 743 RVA: 0x0002234C File Offset: 0x0002234C
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x060002E8 RID: 744 RVA: 0x00022358 File Offset: 0x00022358
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.ActivitySpeaking left, NotchLiveEvent.ActivitySpeaking right)
      {
        return !(left == right);
      }

      // Token: 0x060002E9 RID: 745 RVA: 0x00022364 File Offset: 0x00022364
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.ActivitySpeaking left, NotchLiveEvent.ActivitySpeaking right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060002EA RID: 746 RVA: 0x00022378 File Offset: 0x00022378
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x060002EB RID: 747 RVA: 0x00022380 File Offset: 0x00022380
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.ActivitySpeaking);
      }

      // Token: 0x060002EC RID: 748 RVA: 0x00022390 File Offset: 0x00022390
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x060002ED RID: 749 RVA: 0x0002239C File Offset: 0x0002239C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.ActivitySpeaking other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x060002EF RID: 751 RVA: 0x000223B4 File Offset: 0x000223B4
      [CompilerGenerated]
      private ActivitySpeaking(NotchLiveEvent.ActivitySpeaking original)
        : base(original)
      {
      }

      // Token: 0x060002F0 RID: 752 RVA: 0x000223C0 File Offset: 0x000223C0
      public ActivitySpeaking()
      {
      }
    }

    // Token: 0x0200003B RID: 59
    [Nullable(0)]
    public sealed class ActivityCustomStatus : NotchLiveEvent, IEquatable<NotchLiveEvent.ActivityCustomStatus>
    {
      // Token: 0x060002F1 RID: 753 RVA: 0x000223C8 File Offset: 0x000223C8
      public ActivityCustomStatus(string Label)
      {
        this.Label = Label;
        base..ctor();
      }

      // Token: 0x170000B1 RID: 177
      // (get) Token: 0x060002F2 RID: 754 RVA: 0x000223D8 File Offset: 0x000223D8
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.ActivityCustomStatus);
        }
      }

      // Token: 0x170000B2 RID: 178
      // (get) Token: 0x060002F3 RID: 755 RVA: 0x000223E4 File Offset: 0x000223E4
      // (set) Token: 0x060002F4 RID: 756 RVA: 0x000223EC File Offset: 0x000223EC
      public string Label { get; set; }

      // Token: 0x060002F5 RID: 757 RVA: 0x000223F8 File Offset: 0x000223F8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ActivityCustomStatus");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060002F6 RID: 758 RVA: 0x00022444 File Offset: 0x00022444
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Label = ");
        builder.Append(this.Label);
        return true;
      }

      // Token: 0x060002F7 RID: 759 RVA: 0x00022478 File Offset: 0x00022478
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.ActivityCustomStatus left, NotchLiveEvent.ActivityCustomStatus right)
      {
        return !(left == right);
      }

      // Token: 0x060002F8 RID: 760 RVA: 0x00022484 File Offset: 0x00022484
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.ActivityCustomStatus left, NotchLiveEvent.ActivityCustomStatus right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060002F9 RID: 761 RVA: 0x00022498 File Offset: 0x00022498
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Label>k__BackingField);
      }

      // Token: 0x060002FA RID: 762 RVA: 0x000224B8 File Offset: 0x000224B8
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.ActivityCustomStatus);
      }

      // Token: 0x060002FB RID: 763 RVA: 0x000224C8 File Offset: 0x000224C8
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x060002FC RID: 764 RVA: 0x000224D4 File Offset: 0x000224D4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.ActivityCustomStatus other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Label>k__BackingField, other.<Label>k__BackingField));
      }

      // Token: 0x060002FE RID: 766 RVA: 0x00022508 File Offset: 0x00022508
      [CompilerGenerated]
      private ActivityCustomStatus(NotchLiveEvent.ActivityCustomStatus original)
        : base(original)
      {
        this.Label = original.<Label>k__BackingField;
      }

      // Token: 0x060002FF RID: 767 RVA: 0x00022520 File Offset: 0x00022520
      [CompilerGenerated]
      public void Deconstruct(out string Label)
      {
        Label = this.Label;
      }
    }

    // Token: 0x0200003C RID: 60
    [Nullable(0)]
    public sealed class Dictation : NotchLiveEvent, IEquatable<NotchLiveEvent.Dictation>
    {
      // Token: 0x06000300 RID: 768 RVA: 0x0002252C File Offset: 0x0002252C
      public Dictation(string Label, bool IsRecording)
      {
        this.Label = Label;
        this.IsRecording = IsRecording;
        base..ctor();
      }

      // Token: 0x170000B3 RID: 179
      // (get) Token: 0x06000301 RID: 769 RVA: 0x00022544 File Offset: 0x00022544
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.Dictation);
        }
      }

      // Token: 0x170000B4 RID: 180
      // (get) Token: 0x06000302 RID: 770 RVA: 0x00022550 File Offset: 0x00022550
      // (set) Token: 0x06000303 RID: 771 RVA: 0x00022558 File Offset: 0x00022558
      public string Label { get; set; }

      // Token: 0x170000B5 RID: 181
      // (get) Token: 0x06000304 RID: 772 RVA: 0x00022564 File Offset: 0x00022564
      // (set) Token: 0x06000305 RID: 773 RVA: 0x0002256C File Offset: 0x0002256C
      public bool IsRecording { get; set; }

      // Token: 0x06000306 RID: 774 RVA: 0x00022578 File Offset: 0x00022578
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Dictation");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000307 RID: 775 RVA: 0x000225C4 File Offset: 0x000225C4
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Label = ");
        builder.Append(this.Label);
        builder.Append(", IsRecording = ");
        builder.Append(this.IsRecording.ToString());
        return true;
      }

      // Token: 0x06000308 RID: 776 RVA: 0x00022628 File Offset: 0x00022628
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.Dictation left, NotchLiveEvent.Dictation right)
      {
        return !(left == right);
      }

      // Token: 0x06000309 RID: 777 RVA: 0x00022634 File Offset: 0x00022634
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.Dictation left, NotchLiveEvent.Dictation right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600030A RID: 778 RVA: 0x00022648 File Offset: 0x00022648
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Label>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsRecording>k__BackingField);
      }

      // Token: 0x0600030B RID: 779 RVA: 0x00022680 File Offset: 0x00022680
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.Dictation);
      }

      // Token: 0x0600030C RID: 780 RVA: 0x00022690 File Offset: 0x00022690
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x0600030D RID: 781 RVA: 0x0002269C File Offset: 0x0002269C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.Dictation other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Label>k__BackingField, other.<Label>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsRecording>k__BackingField, other.<IsRecording>k__BackingField));
      }

      // Token: 0x0600030F RID: 783 RVA: 0x000226F0 File Offset: 0x000226F0
      [CompilerGenerated]
      private Dictation(NotchLiveEvent.Dictation original)
        : base(original)
      {
        this.Label = original.<Label>k__BackingField;
        this.IsRecording = original.<IsRecording>k__BackingField;
      }

      // Token: 0x06000310 RID: 784 RVA: 0x00022714 File Offset: 0x00022714
      [CompilerGenerated]
      public void Deconstruct(out string Label, out bool IsRecording)
      {
        Label = this.Label;
        IsRecording = this.IsRecording;
      }
    }

    // Token: 0x0200003D RID: 61
    [Nullable(0)]
    public sealed class AgentMultiActivity : NotchLiveEvent, IEquatable<NotchLiveEvent.AgentMultiActivity>
    {
      // Token: 0x06000311 RID: 785 RVA: 0x00022728 File Offset: 0x00022728
      public AgentMultiActivity(string LongestLabel, int Count)
      {
        this.LongestLabel = LongestLabel;
        this.Count = Count;
        base..ctor();
      }

      // Token: 0x170000B6 RID: 182
      // (get) Token: 0x06000312 RID: 786 RVA: 0x00022740 File Offset: 0x00022740
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.AgentMultiActivity);
        }
      }

      // Token: 0x170000B7 RID: 183
      // (get) Token: 0x06000313 RID: 787 RVA: 0x0002274C File Offset: 0x0002274C
      // (set) Token: 0x06000314 RID: 788 RVA: 0x00022754 File Offset: 0x00022754
      public string LongestLabel { get; set; }

      // Token: 0x170000B8 RID: 184
      // (get) Token: 0x06000315 RID: 789 RVA: 0x00022760 File Offset: 0x00022760
      // (set) Token: 0x06000316 RID: 790 RVA: 0x00022768 File Offset: 0x00022768
      public int Count { get; set; }

      // Token: 0x06000317 RID: 791 RVA: 0x00022774 File Offset: 0x00022774
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("AgentMultiActivity");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000318 RID: 792 RVA: 0x000227C0 File Offset: 0x000227C0
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("LongestLabel = ");
        builder.Append(this.LongestLabel);
        builder.Append(", Count = ");
        builder.Append(this.Count.ToString());
        return true;
      }

      // Token: 0x06000319 RID: 793 RVA: 0x00022824 File Offset: 0x00022824
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.AgentMultiActivity left, NotchLiveEvent.AgentMultiActivity right)
      {
        return !(left == right);
      }

      // Token: 0x0600031A RID: 794 RVA: 0x00022830 File Offset: 0x00022830
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.AgentMultiActivity left, NotchLiveEvent.AgentMultiActivity right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600031B RID: 795 RVA: 0x00022844 File Offset: 0x00022844
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LongestLabel>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Count>k__BackingField);
      }

      // Token: 0x0600031C RID: 796 RVA: 0x0002287C File Offset: 0x0002287C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.AgentMultiActivity);
      }

      // Token: 0x0600031D RID: 797 RVA: 0x0002288C File Offset: 0x0002288C
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x0600031E RID: 798 RVA: 0x00022898 File Offset: 0x00022898
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.AgentMultiActivity other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<LongestLabel>k__BackingField, other.<LongestLabel>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Count>k__BackingField, other.<Count>k__BackingField));
      }

      // Token: 0x06000320 RID: 800 RVA: 0x000228EC File Offset: 0x000228EC
      [CompilerGenerated]
      private AgentMultiActivity(NotchLiveEvent.AgentMultiActivity original)
        : base(original)
      {
        this.LongestLabel = original.<LongestLabel>k__BackingField;
        this.Count = original.<Count>k__BackingField;
      }

      // Token: 0x06000321 RID: 801 RVA: 0x00022910 File Offset: 0x00022910
      [CompilerGenerated]
      public void Deconstruct(out string LongestLabel, out int Count)
      {
        LongestLabel = this.LongestLabel;
        Count = this.Count;
      }
    }

    // Token: 0x0200003E RID: 62
    [Nullable(0)]
    public sealed class AgentTask : NotchLiveEvent, IEquatable<NotchLiveEvent.AgentTask>
    {
      // Token: 0x06000322 RID: 802 RVA: 0x00022924 File Offset: 0x00022924
      public AgentTask(string TaskId, string Title, string Status)
      {
        this.TaskId = TaskId;
        this.Title = Title;
        this.Status = Status;
        base..ctor();
      }

      // Token: 0x170000B9 RID: 185
      // (get) Token: 0x06000323 RID: 803 RVA: 0x00022944 File Offset: 0x00022944
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.AgentTask);
        }
      }

      // Token: 0x170000BA RID: 186
      // (get) Token: 0x06000324 RID: 804 RVA: 0x00022950 File Offset: 0x00022950
      // (set) Token: 0x06000325 RID: 805 RVA: 0x00022958 File Offset: 0x00022958
      public string TaskId { get; set; }

      // Token: 0x170000BB RID: 187
      // (get) Token: 0x06000326 RID: 806 RVA: 0x00022964 File Offset: 0x00022964
      // (set) Token: 0x06000327 RID: 807 RVA: 0x0002296C File Offset: 0x0002296C
      public string Title { get; set; }

      // Token: 0x170000BC RID: 188
      // (get) Token: 0x06000328 RID: 808 RVA: 0x00022978 File Offset: 0x00022978
      // (set) Token: 0x06000329 RID: 809 RVA: 0x00022980 File Offset: 0x00022980
      public string Status { get; set; }

      // Token: 0x0600032A RID: 810 RVA: 0x0002298C File Offset: 0x0002298C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("AgentTask");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600032B RID: 811 RVA: 0x000229D8 File Offset: 0x000229D8
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("TaskId = ");
        builder.Append(this.TaskId);
        builder.Append(", Title = ");
        builder.Append(this.Title);
        builder.Append(", Status = ");
        builder.Append(this.Status);
        return true;
      }

      // Token: 0x0600032C RID: 812 RVA: 0x00022A48 File Offset: 0x00022A48
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.AgentTask left, NotchLiveEvent.AgentTask right)
      {
        return !(left == right);
      }

      // Token: 0x0600032D RID: 813 RVA: 0x00022A54 File Offset: 0x00022A54
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.AgentTask left, NotchLiveEvent.AgentTask right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600032E RID: 814 RVA: 0x00022A68 File Offset: 0x00022A68
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TaskId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Status>k__BackingField);
      }

      // Token: 0x0600032F RID: 815 RVA: 0x00022AC0 File Offset: 0x00022AC0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.AgentTask);
      }

      // Token: 0x06000330 RID: 816 RVA: 0x00022AD0 File Offset: 0x00022AD0
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000331 RID: 817 RVA: 0x00022ADC File Offset: 0x00022ADC
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.AgentTask other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<TaskId>k__BackingField, other.<TaskId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField));
      }

      // Token: 0x06000333 RID: 819 RVA: 0x00022B48 File Offset: 0x00022B48
      [CompilerGenerated]
      private AgentTask(NotchLiveEvent.AgentTask original)
        : base(original)
      {
        this.TaskId = original.<TaskId>k__BackingField;
        this.Title = original.<Title>k__BackingField;
        this.Status = original.<Status>k__BackingField;
      }

      // Token: 0x06000334 RID: 820 RVA: 0x00022B78 File Offset: 0x00022B78
      [CompilerGenerated]
      public void Deconstruct(out string TaskId, out string Title, out string Status)
      {
        TaskId = this.TaskId;
        Title = this.Title;
        Status = this.Status;
      }
    }

    // Token: 0x0200003F RID: 63
    [Nullable(0)]
    public sealed class IntegrationSuggestion : NotchLiveEvent, IEquatable<NotchLiveEvent.IntegrationSuggestion>
    {
      // Token: 0x06000335 RID: 821 RVA: 0x00022B94 File Offset: 0x00022B94
      public IntegrationSuggestion(string Toolkit, string Title)
      {
        this.Toolkit = Toolkit;
        this.Title = Title;
        base..ctor();
      }

      // Token: 0x170000BD RID: 189
      // (get) Token: 0x06000336 RID: 822 RVA: 0x00022BAC File Offset: 0x00022BAC
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.IntegrationSuggestion);
        }
      }

      // Token: 0x170000BE RID: 190
      // (get) Token: 0x06000337 RID: 823 RVA: 0x00022BB8 File Offset: 0x00022BB8
      // (set) Token: 0x06000338 RID: 824 RVA: 0x00022BC0 File Offset: 0x00022BC0
      public string Toolkit { get; set; }

      // Token: 0x170000BF RID: 191
      // (get) Token: 0x06000339 RID: 825 RVA: 0x00022BCC File Offset: 0x00022BCC
      // (set) Token: 0x0600033A RID: 826 RVA: 0x00022BD4 File Offset: 0x00022BD4
      public string Title { get; set; }

      // Token: 0x0600033B RID: 827 RVA: 0x00022BE0 File Offset: 0x00022BE0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("IntegrationSuggestion");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600033C RID: 828 RVA: 0x00022C2C File Offset: 0x00022C2C
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Toolkit = ");
        builder.Append(this.Toolkit);
        builder.Append(", Title = ");
        builder.Append(this.Title);
        return true;
      }

      // Token: 0x0600033D RID: 829 RVA: 0x00022C84 File Offset: 0x00022C84
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.IntegrationSuggestion left, NotchLiveEvent.IntegrationSuggestion right)
      {
        return !(left == right);
      }

      // Token: 0x0600033E RID: 830 RVA: 0x00022C90 File Offset: 0x00022C90
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.IntegrationSuggestion left, NotchLiveEvent.IntegrationSuggestion right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600033F RID: 831 RVA: 0x00022CA4 File Offset: 0x00022CA4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Toolkit>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField);
      }

      // Token: 0x06000340 RID: 832 RVA: 0x00022CDC File Offset: 0x00022CDC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.IntegrationSuggestion);
      }

      // Token: 0x06000341 RID: 833 RVA: 0x00022CEC File Offset: 0x00022CEC
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000342 RID: 834 RVA: 0x00022CF8 File Offset: 0x00022CF8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.IntegrationSuggestion other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Toolkit>k__BackingField, other.<Toolkit>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField));
      }

      // Token: 0x06000344 RID: 836 RVA: 0x00022D4C File Offset: 0x00022D4C
      [CompilerGenerated]
      private IntegrationSuggestion(NotchLiveEvent.IntegrationSuggestion original)
        : base(original)
      {
        this.Toolkit = original.<Toolkit>k__BackingField;
        this.Title = original.<Title>k__BackingField;
      }

      // Token: 0x06000345 RID: 837 RVA: 0x00022D70 File Offset: 0x00022D70
      [CompilerGenerated]
      public void Deconstruct(out string Toolkit, out string Title)
      {
        Toolkit = this.Toolkit;
        Title = this.Title;
      }
    }

    // Token: 0x02000040 RID: 64
    [NullableContext(2)]
    [Nullable(0)]
    public sealed class IntegrationConnected : NotchLiveEvent, IEquatable<NotchLiveEvent.IntegrationConnected>
    {
      // Token: 0x06000346 RID: 838 RVA: 0x00022D84 File Offset: 0x00022D84
      public IntegrationConnected([Nullable(1)] string Toolkit, string DisplayName = null, string LogoUrl = null)
      {
        this.Toolkit = Toolkit;
        this.DisplayName = DisplayName;
        this.LogoUrl = LogoUrl;
        base..ctor();
      }

      // Token: 0x170000C0 RID: 192
      // (get) Token: 0x06000347 RID: 839 RVA: 0x00022DA4 File Offset: 0x00022DA4
      [Nullable(1)]
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [NullableContext(1)]
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.IntegrationConnected);
        }
      }

      // Token: 0x170000C1 RID: 193
      // (get) Token: 0x06000348 RID: 840 RVA: 0x00022DB0 File Offset: 0x00022DB0
      // (set) Token: 0x06000349 RID: 841 RVA: 0x00022DB8 File Offset: 0x00022DB8
      [Nullable(1)]
      public string Toolkit
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x170000C2 RID: 194
      // (get) Token: 0x0600034A RID: 842 RVA: 0x00022DC4 File Offset: 0x00022DC4
      // (set) Token: 0x0600034B RID: 843 RVA: 0x00022DCC File Offset: 0x00022DCC
      public string DisplayName { get; set; }

      // Token: 0x170000C3 RID: 195
      // (get) Token: 0x0600034C RID: 844 RVA: 0x00022DD8 File Offset: 0x00022DD8
      // (set) Token: 0x0600034D RID: 845 RVA: 0x00022DE0 File Offset: 0x00022DE0
      public string LogoUrl { get; set; }

      // Token: 0x0600034E RID: 846 RVA: 0x00022DEC File Offset: 0x00022DEC
      [NullableContext(1)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("IntegrationConnected");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600034F RID: 847 RVA: 0x00022E38 File Offset: 0x00022E38
      [NullableContext(1)]
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Toolkit = ");
        builder.Append(this.Toolkit);
        builder.Append(", DisplayName = ");
        builder.Append(this.DisplayName);
        builder.Append(", LogoUrl = ");
        builder.Append(this.LogoUrl);
        return true;
      }

      // Token: 0x06000350 RID: 848 RVA: 0x00022EA8 File Offset: 0x00022EA8
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.IntegrationConnected left, NotchLiveEvent.IntegrationConnected right)
      {
        return !(left == right);
      }

      // Token: 0x06000351 RID: 849 RVA: 0x00022EB4 File Offset: 0x00022EB4
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.IntegrationConnected left, NotchLiveEvent.IntegrationConnected right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000352 RID: 850 RVA: 0x00022EC8 File Offset: 0x00022EC8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Toolkit>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LogoUrl>k__BackingField);
      }

      // Token: 0x06000353 RID: 851 RVA: 0x00022F20 File Offset: 0x00022F20
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.IntegrationConnected);
      }

      // Token: 0x06000354 RID: 852 RVA: 0x00022F30 File Offset: 0x00022F30
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000355 RID: 853 RVA: 0x00022F3C File Offset: 0x00022F3C
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.IntegrationConnected other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Toolkit>k__BackingField, other.<Toolkit>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LogoUrl>k__BackingField, other.<LogoUrl>k__BackingField));
      }

      // Token: 0x06000357 RID: 855 RVA: 0x00022FA8 File Offset: 0x00022FA8
      [CompilerGenerated]
      private IntegrationConnected([Nullable(1)] NotchLiveEvent.IntegrationConnected original)
        : base(original)
      {
        this.Toolkit = original.<Toolkit>k__BackingField;
        this.DisplayName = original.<DisplayName>k__BackingField;
        this.LogoUrl = original.<LogoUrl>k__BackingField;
      }

      // Token: 0x06000358 RID: 856 RVA: 0x00022FD8 File Offset: 0x00022FD8
      [CompilerGenerated]
      public void Deconstruct([Nullable(1)] out string Toolkit, out string DisplayName, out string LogoUrl)
      {
        Toolkit = this.Toolkit;
        DisplayName = this.DisplayName;
        LogoUrl = this.LogoUrl;
      }
    }

    // Token: 0x02000041 RID: 65
    [Nullable(0)]
    public sealed class MicrophoneTrouble : NotchLiveEvent, IEquatable<NotchLiveEvent.MicrophoneTrouble>
    {
      // Token: 0x06000359 RID: 857 RVA: 0x00022FF4 File Offset: 0x00022FF4
      public MicrophoneTrouble(string Message, [Nullable(2)] string Subtitle = null, bool ShowsMicrophoneSettingsAction = true)
      {
        this.Message = Message;
        this.Subtitle = Subtitle;
        this.ShowsMicrophoneSettingsAction = ShowsMicrophoneSettingsAction;
        base..ctor();
      }

      // Token: 0x170000C4 RID: 196
      // (get) Token: 0x0600035A RID: 858 RVA: 0x00023014 File Offset: 0x00023014
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.MicrophoneTrouble);
        }
      }

      // Token: 0x170000C5 RID: 197
      // (get) Token: 0x0600035B RID: 859 RVA: 0x00023020 File Offset: 0x00023020
      // (set) Token: 0x0600035C RID: 860 RVA: 0x00023028 File Offset: 0x00023028
      public string Message { get; set; }

      // Token: 0x170000C6 RID: 198
      // (get) Token: 0x0600035D RID: 861 RVA: 0x00023034 File Offset: 0x00023034
      // (set) Token: 0x0600035E RID: 862 RVA: 0x0002303C File Offset: 0x0002303C
      [Nullable(2)]
      public string Subtitle
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x170000C7 RID: 199
      // (get) Token: 0x0600035F RID: 863 RVA: 0x00023048 File Offset: 0x00023048
      // (set) Token: 0x06000360 RID: 864 RVA: 0x00023050 File Offset: 0x00023050
      public bool ShowsMicrophoneSettingsAction { get; set; }

      // Token: 0x06000361 RID: 865 RVA: 0x0002305C File Offset: 0x0002305C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("MicrophoneTrouble");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000362 RID: 866 RVA: 0x000230A8 File Offset: 0x000230A8
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Message = ");
        builder.Append(this.Message);
        builder.Append(", Subtitle = ");
        builder.Append(this.Subtitle);
        builder.Append(", ShowsMicrophoneSettingsAction = ");
        builder.Append(this.ShowsMicrophoneSettingsAction.ToString());
        return true;
      }

      // Token: 0x06000363 RID: 867 RVA: 0x00023124 File Offset: 0x00023124
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.MicrophoneTrouble left, NotchLiveEvent.MicrophoneTrouble right)
      {
        return !(left == right);
      }

      // Token: 0x06000364 RID: 868 RVA: 0x00023130 File Offset: 0x00023130
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.MicrophoneTrouble left, NotchLiveEvent.MicrophoneTrouble right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000365 RID: 869 RVA: 0x00023144 File Offset: 0x00023144
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Message>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Subtitle>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ShowsMicrophoneSettingsAction>k__BackingField);
      }

      // Token: 0x06000366 RID: 870 RVA: 0x0002319C File Offset: 0x0002319C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.MicrophoneTrouble);
      }

      // Token: 0x06000367 RID: 871 RVA: 0x000231AC File Offset: 0x000231AC
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000368 RID: 872 RVA: 0x000231B8 File Offset: 0x000231B8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.MicrophoneTrouble other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Message>k__BackingField, other.<Message>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Subtitle>k__BackingField, other.<Subtitle>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<ShowsMicrophoneSettingsAction>k__BackingField, other.<ShowsMicrophoneSettingsAction>k__BackingField));
      }

      // Token: 0x0600036A RID: 874 RVA: 0x00023224 File Offset: 0x00023224
      [CompilerGenerated]
      private MicrophoneTrouble(NotchLiveEvent.MicrophoneTrouble original)
        : base(original)
      {
        this.Message = original.<Message>k__BackingField;
        this.Subtitle = original.<Subtitle>k__BackingField;
        this.ShowsMicrophoneSettingsAction = original.<ShowsMicrophoneSettingsAction>k__BackingField;
      }

      // Token: 0x0600036B RID: 875 RVA: 0x00023254 File Offset: 0x00023254
      [CompilerGenerated]
      public void Deconstruct(out string Message, [Nullable(2)] out string Subtitle, out bool ShowsMicrophoneSettingsAction)
      {
        Message = this.Message;
        Subtitle = this.Subtitle;
        ShowsMicrophoneSettingsAction = this.ShowsMicrophoneSettingsAction;
      }
    }

    // Token: 0x02000042 RID: 66
    [Nullable(0)]
    public sealed class VoiceReplyMutedCopiedToClipboard : NotchLiveEvent, IEquatable<NotchLiveEvent.VoiceReplyMutedCopiedToClipboard>
    {
      // Token: 0x0600036C RID: 876 RVA: 0x00023270 File Offset: 0x00023270
      public VoiceReplyMutedCopiedToClipboard(string Kind = "zero_volume")
      {
        this.Kind = Kind;
        base..ctor();
      }

      // Token: 0x170000C8 RID: 200
      // (get) Token: 0x0600036D RID: 877 RVA: 0x00023280 File Offset: 0x00023280
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.VoiceReplyMutedCopiedToClipboard);
        }
      }

      // Token: 0x170000C9 RID: 201
      // (get) Token: 0x0600036E RID: 878 RVA: 0x0002328C File Offset: 0x0002328C
      // (set) Token: 0x0600036F RID: 879 RVA: 0x00023294 File Offset: 0x00023294
      public string Kind { get; set; }

      // Token: 0x06000370 RID: 880 RVA: 0x000232A0 File Offset: 0x000232A0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("VoiceReplyMutedCopiedToClipboard");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000371 RID: 881 RVA: 0x000232EC File Offset: 0x000232EC
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Kind = ");
        builder.Append(this.Kind);
        return true;
      }

      // Token: 0x06000372 RID: 882 RVA: 0x00023320 File Offset: 0x00023320
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.VoiceReplyMutedCopiedToClipboard left, NotchLiveEvent.VoiceReplyMutedCopiedToClipboard right)
      {
        return !(left == right);
      }

      // Token: 0x06000373 RID: 883 RVA: 0x0002332C File Offset: 0x0002332C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.VoiceReplyMutedCopiedToClipboard left, NotchLiveEvent.VoiceReplyMutedCopiedToClipboard right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000374 RID: 884 RVA: 0x00023340 File Offset: 0x00023340
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Kind>k__BackingField);
      }

      // Token: 0x06000375 RID: 885 RVA: 0x00023360 File Offset: 0x00023360
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.VoiceReplyMutedCopiedToClipboard);
      }

      // Token: 0x06000376 RID: 886 RVA: 0x00023370 File Offset: 0x00023370
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000377 RID: 887 RVA: 0x0002337C File Offset: 0x0002337C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.VoiceReplyMutedCopiedToClipboard other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField));
      }

      // Token: 0x06000379 RID: 889 RVA: 0x000233B0 File Offset: 0x000233B0
      [CompilerGenerated]
      private VoiceReplyMutedCopiedToClipboard(NotchLiveEvent.VoiceReplyMutedCopiedToClipboard original)
        : base(original)
      {
        this.Kind = original.<Kind>k__BackingField;
      }

      // Token: 0x0600037A RID: 890 RVA: 0x000233C8 File Offset: 0x000233C8
      [CompilerGenerated]
      public void Deconstruct(out string Kind)
      {
        Kind = this.Kind;
      }
    }

    // Token: 0x02000043 RID: 67
    [Nullable(0)]
    public sealed class DictationCopiedToClipboard : NotchLiveEvent, IEquatable<NotchLiveEvent.DictationCopiedToClipboard>
    {
      // Token: 0x170000CA RID: 202
      // (get) Token: 0x0600037B RID: 891 RVA: 0x000233D4 File Offset: 0x000233D4
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.DictationCopiedToClipboard);
        }
      }

      // Token: 0x0600037C RID: 892 RVA: 0x000233E0 File Offset: 0x000233E0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("DictationCopiedToClipboard");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600037D RID: 893 RVA: 0x0002342C File Offset: 0x0002342C
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x0600037E RID: 894 RVA: 0x00023438 File Offset: 0x00023438
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.DictationCopiedToClipboard left, NotchLiveEvent.DictationCopiedToClipboard right)
      {
        return !(left == right);
      }

      // Token: 0x0600037F RID: 895 RVA: 0x00023444 File Offset: 0x00023444
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.DictationCopiedToClipboard left, NotchLiveEvent.DictationCopiedToClipboard right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000380 RID: 896 RVA: 0x00023458 File Offset: 0x00023458
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x06000381 RID: 897 RVA: 0x00023460 File Offset: 0x00023460
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.DictationCopiedToClipboard);
      }

      // Token: 0x06000382 RID: 898 RVA: 0x00023470 File Offset: 0x00023470
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000383 RID: 899 RVA: 0x0002347C File Offset: 0x0002347C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.DictationCopiedToClipboard other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x06000385 RID: 901 RVA: 0x00023494 File Offset: 0x00023494
      [CompilerGenerated]
      private DictationCopiedToClipboard(NotchLiveEvent.DictationCopiedToClipboard original)
        : base(original)
      {
      }

      // Token: 0x06000386 RID: 902 RVA: 0x000234A0 File Offset: 0x000234A0
      public DictationCopiedToClipboard()
      {
      }
    }

    // Token: 0x02000044 RID: 68
    [Nullable(0)]
    public sealed class AppUpdated : NotchLiveEvent, IEquatable<NotchLiveEvent.AppUpdated>
    {
      // Token: 0x06000387 RID: 903 RVA: 0x000234A8 File Offset: 0x000234A8
      public AppUpdated(string Version)
      {
        this.Version = Version;
        base..ctor();
      }

      // Token: 0x170000CB RID: 203
      // (get) Token: 0x06000388 RID: 904 RVA: 0x000234B8 File Offset: 0x000234B8
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.AppUpdated);
        }
      }

      // Token: 0x170000CC RID: 204
      // (get) Token: 0x06000389 RID: 905 RVA: 0x000234C4 File Offset: 0x000234C4
      // (set) Token: 0x0600038A RID: 906 RVA: 0x000234CC File Offset: 0x000234CC
      public string Version { get; set; }

      // Token: 0x0600038B RID: 907 RVA: 0x000234D8 File Offset: 0x000234D8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("AppUpdated");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600038C RID: 908 RVA: 0x00023524 File Offset: 0x00023524
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Version = ");
        builder.Append(this.Version);
        return true;
      }

      // Token: 0x0600038D RID: 909 RVA: 0x00023558 File Offset: 0x00023558
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.AppUpdated left, NotchLiveEvent.AppUpdated right)
      {
        return !(left == right);
      }

      // Token: 0x0600038E RID: 910 RVA: 0x00023564 File Offset: 0x00023564
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.AppUpdated left, NotchLiveEvent.AppUpdated right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600038F RID: 911 RVA: 0x00023578 File Offset: 0x00023578
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Version>k__BackingField);
      }

      // Token: 0x06000390 RID: 912 RVA: 0x00023598 File Offset: 0x00023598
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.AppUpdated);
      }

      // Token: 0x06000391 RID: 913 RVA: 0x000235A8 File Offset: 0x000235A8
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000392 RID: 914 RVA: 0x000235B4 File Offset: 0x000235B4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.AppUpdated other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Version>k__BackingField, other.<Version>k__BackingField));
      }

      // Token: 0x06000394 RID: 916 RVA: 0x000235E8 File Offset: 0x000235E8
      [CompilerGenerated]
      private AppUpdated(NotchLiveEvent.AppUpdated original)
        : base(original)
      {
        this.Version = original.<Version>k__BackingField;
      }

      // Token: 0x06000395 RID: 917 RVA: 0x00023600 File Offset: 0x00023600
      [CompilerGenerated]
      public void Deconstruct(out string Version)
      {
        Version = this.Version;
      }
    }

    // Token: 0x02000045 RID: 69
    [NullableContext(2)]
    [Nullable(0)]
    public sealed class MeetingCountdown : NotchLiveEvent, IEquatable<NotchLiveEvent.MeetingCountdown>
    {
      // Token: 0x06000396 RID: 918 RVA: 0x0002360C File Offset: 0x0002360C
      public MeetingCountdown([Nullable(1)] string Title, DateTimeOffset StartsAt, Uri JoinUrl, string ActionKind = null)
      {
        this.Title = Title;
        this.StartsAt = StartsAt;
        this.JoinUrl = JoinUrl;
        this.ActionKind = ActionKind;
        base..ctor();
      }

      // Token: 0x170000CD RID: 205
      // (get) Token: 0x06000397 RID: 919 RVA: 0x00023634 File Offset: 0x00023634
      [Nullable(1)]
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [NullableContext(1)]
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.MeetingCountdown);
        }
      }

      // Token: 0x170000CE RID: 206
      // (get) Token: 0x06000398 RID: 920 RVA: 0x00023640 File Offset: 0x00023640
      // (set) Token: 0x06000399 RID: 921 RVA: 0x00023648 File Offset: 0x00023648
      [Nullable(1)]
      public string Title
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x170000CF RID: 207
      // (get) Token: 0x0600039A RID: 922 RVA: 0x00023654 File Offset: 0x00023654
      // (set) Token: 0x0600039B RID: 923 RVA: 0x0002365C File Offset: 0x0002365C
      public DateTimeOffset StartsAt { get; set; }

      // Token: 0x170000D0 RID: 208
      // (get) Token: 0x0600039C RID: 924 RVA: 0x00023668 File Offset: 0x00023668
      // (set) Token: 0x0600039D RID: 925 RVA: 0x00023670 File Offset: 0x00023670
      public Uri JoinUrl { get; set; }

      // Token: 0x170000D1 RID: 209
      // (get) Token: 0x0600039E RID: 926 RVA: 0x0002367C File Offset: 0x0002367C
      // (set) Token: 0x0600039F RID: 927 RVA: 0x00023684 File Offset: 0x00023684
      public string ActionKind { get; set; }

      // Token: 0x060003A0 RID: 928 RVA: 0x00023690 File Offset: 0x00023690
      [NullableContext(1)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("MeetingCountdown");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060003A1 RID: 929 RVA: 0x000236DC File Offset: 0x000236DC
      [NullableContext(1)]
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Title = ");
        builder.Append(this.Title);
        builder.Append(", StartsAt = ");
        builder.Append(this.StartsAt.ToString());
        builder.Append(", JoinUrl = ");
        builder.Append(this.JoinUrl);
        builder.Append(", ActionKind = ");
        builder.Append(this.ActionKind);
        return true;
      }

      // Token: 0x060003A2 RID: 930 RVA: 0x00023774 File Offset: 0x00023774
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.MeetingCountdown left, NotchLiveEvent.MeetingCountdown right)
      {
        return !(left == right);
      }

      // Token: 0x060003A3 RID: 931 RVA: 0x00023780 File Offset: 0x00023780
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.MeetingCountdown left, NotchLiveEvent.MeetingCountdown right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060003A4 RID: 932 RVA: 0x00023794 File Offset: 0x00023794
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<StartsAt>k__BackingField)) * -1521134295 + EqualityComparer<Uri>.Default.GetHashCode(this.<JoinUrl>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ActionKind>k__BackingField);
      }

      // Token: 0x060003A5 RID: 933 RVA: 0x00023804 File Offset: 0x00023804
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.MeetingCountdown);
      }

      // Token: 0x060003A6 RID: 934 RVA: 0x00023814 File Offset: 0x00023814
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x060003A7 RID: 935 RVA: 0x00023820 File Offset: 0x00023820
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.MeetingCountdown other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<StartsAt>k__BackingField, other.<StartsAt>k__BackingField) && EqualityComparer<Uri>.Default.Equals(this.<JoinUrl>k__BackingField, other.<JoinUrl>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ActionKind>k__BackingField, other.<ActionKind>k__BackingField));
      }

      // Token: 0x060003A9 RID: 937 RVA: 0x000238A4 File Offset: 0x000238A4
      [CompilerGenerated]
      private MeetingCountdown([Nullable(1)] NotchLiveEvent.MeetingCountdown original)
        : base(original)
      {
        this.Title = original.<Title>k__BackingField;
        this.StartsAt = original.<StartsAt>k__BackingField;
        this.JoinUrl = original.<JoinUrl>k__BackingField;
        this.ActionKind = original.<ActionKind>k__BackingField;
      }

      // Token: 0x060003AA RID: 938 RVA: 0x000238E0 File Offset: 0x000238E0
      [CompilerGenerated]
      public void Deconstruct([Nullable(1)] out string Title, out DateTimeOffset StartsAt, out Uri JoinUrl, out string ActionKind)
      {
        Title = this.Title;
        StartsAt = this.StartsAt;
        JoinUrl = this.JoinUrl;
        ActionKind = this.ActionKind;
      }
    }

    // Token: 0x02000046 RID: 70
    [Nullable(0)]
    public sealed class ClickyUpdate : NotchLiveEvent, IEquatable<NotchLiveEvent.ClickyUpdate>
    {
      // Token: 0x060003AB RID: 939 RVA: 0x00023908 File Offset: 0x00023908
      public ClickyUpdate(string RailKey, string Kind)
      {
        this.RailKey = RailKey;
        this.Kind = Kind;
        base..ctor();
      }

      // Token: 0x170000D2 RID: 210
      // (get) Token: 0x060003AC RID: 940 RVA: 0x00023920 File Offset: 0x00023920
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.ClickyUpdate);
        }
      }

      // Token: 0x170000D3 RID: 211
      // (get) Token: 0x060003AD RID: 941 RVA: 0x0002392C File Offset: 0x0002392C
      // (set) Token: 0x060003AE RID: 942 RVA: 0x00023934 File Offset: 0x00023934
      public string RailKey { get; set; }

      // Token: 0x170000D4 RID: 212
      // (get) Token: 0x060003AF RID: 943 RVA: 0x00023940 File Offset: 0x00023940
      // (set) Token: 0x060003B0 RID: 944 RVA: 0x00023948 File Offset: 0x00023948
      public string Kind { get; set; }

      // Token: 0x060003B1 RID: 945 RVA: 0x00023954 File Offset: 0x00023954
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ClickyUpdate");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060003B2 RID: 946 RVA: 0x000239A0 File Offset: 0x000239A0
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("RailKey = ");
        builder.Append(this.RailKey);
        builder.Append(", Kind = ");
        builder.Append(this.Kind);
        return true;
      }

      // Token: 0x060003B3 RID: 947 RVA: 0x000239F8 File Offset: 0x000239F8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.ClickyUpdate left, NotchLiveEvent.ClickyUpdate right)
      {
        return !(left == right);
      }

      // Token: 0x060003B4 RID: 948 RVA: 0x00023A04 File Offset: 0x00023A04
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.ClickyUpdate left, NotchLiveEvent.ClickyUpdate right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060003B5 RID: 949 RVA: 0x00023A18 File Offset: 0x00023A18
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RailKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Kind>k__BackingField);
      }

      // Token: 0x060003B6 RID: 950 RVA: 0x00023A50 File Offset: 0x00023A50
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.ClickyUpdate);
      }

      // Token: 0x060003B7 RID: 951 RVA: 0x00023A60 File Offset: 0x00023A60
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x060003B8 RID: 952 RVA: 0x00023A6C File Offset: 0x00023A6C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.ClickyUpdate other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<RailKey>k__BackingField, other.<RailKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField));
      }

      // Token: 0x060003BA RID: 954 RVA: 0x00023AC0 File Offset: 0x00023AC0
      [CompilerGenerated]
      private ClickyUpdate(NotchLiveEvent.ClickyUpdate original)
        : base(original)
      {
        this.RailKey = original.<RailKey>k__BackingField;
        this.Kind = original.<Kind>k__BackingField;
      }

      // Token: 0x060003BB RID: 955 RVA: 0x00023AE4 File Offset: 0x00023AE4
      [CompilerGenerated]
      public void Deconstruct(out string RailKey, out string Kind)
      {
        RailKey = this.RailKey;
        Kind = this.Kind;
      }
    }

    // Token: 0x02000047 RID: 71
    [Nullable(0)]
    public sealed class OnboardingTutorialInvite : NotchLiveEvent, IEquatable<NotchLiveEvent.OnboardingTutorialInvite>
    {
      // Token: 0x170000D5 RID: 213
      // (get) Token: 0x060003BC RID: 956 RVA: 0x00023AF8 File Offset: 0x00023AF8
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.OnboardingTutorialInvite);
        }
      }

      // Token: 0x060003BD RID: 957 RVA: 0x00023B04 File Offset: 0x00023B04
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("OnboardingTutorialInvite");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060003BE RID: 958 RVA: 0x00023B50 File Offset: 0x00023B50
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x060003BF RID: 959 RVA: 0x00023B5C File Offset: 0x00023B5C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.OnboardingTutorialInvite left, NotchLiveEvent.OnboardingTutorialInvite right)
      {
        return !(left == right);
      }

      // Token: 0x060003C0 RID: 960 RVA: 0x00023B68 File Offset: 0x00023B68
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.OnboardingTutorialInvite left, NotchLiveEvent.OnboardingTutorialInvite right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060003C1 RID: 961 RVA: 0x00023B7C File Offset: 0x00023B7C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x060003C2 RID: 962 RVA: 0x00023B84 File Offset: 0x00023B84
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.OnboardingTutorialInvite);
      }

      // Token: 0x060003C3 RID: 963 RVA: 0x00023B94 File Offset: 0x00023B94
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x060003C4 RID: 964 RVA: 0x00023BA0 File Offset: 0x00023BA0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.OnboardingTutorialInvite other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x060003C6 RID: 966 RVA: 0x00023BB8 File Offset: 0x00023BB8
      [CompilerGenerated]
      private OnboardingTutorialInvite(NotchLiveEvent.OnboardingTutorialInvite original)
        : base(original)
      {
      }

      // Token: 0x060003C7 RID: 967 RVA: 0x00023BC4 File Offset: 0x00023BC4
      public OnboardingTutorialInvite()
      {
      }
    }

    // Token: 0x02000048 RID: 72
    [Nullable(0)]
    public sealed class OnboardingTutorialResume : NotchLiveEvent, IEquatable<NotchLiveEvent.OnboardingTutorialResume>
    {
      // Token: 0x170000D6 RID: 214
      // (get) Token: 0x060003C8 RID: 968 RVA: 0x00023BCC File Offset: 0x00023BCC
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.OnboardingTutorialResume);
        }
      }

      // Token: 0x060003C9 RID: 969 RVA: 0x00023BD8 File Offset: 0x00023BD8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("OnboardingTutorialResume");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060003CA RID: 970 RVA: 0x00023C24 File Offset: 0x00023C24
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x060003CB RID: 971 RVA: 0x00023C30 File Offset: 0x00023C30
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.OnboardingTutorialResume left, NotchLiveEvent.OnboardingTutorialResume right)
      {
        return !(left == right);
      }

      // Token: 0x060003CC RID: 972 RVA: 0x00023C3C File Offset: 0x00023C3C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.OnboardingTutorialResume left, NotchLiveEvent.OnboardingTutorialResume right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060003CD RID: 973 RVA: 0x00023C50 File Offset: 0x00023C50
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x060003CE RID: 974 RVA: 0x00023C58 File Offset: 0x00023C58
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.OnboardingTutorialResume);
      }

      // Token: 0x060003CF RID: 975 RVA: 0x00023C68 File Offset: 0x00023C68
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x060003D0 RID: 976 RVA: 0x00023C74 File Offset: 0x00023C74
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.OnboardingTutorialResume other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x060003D2 RID: 978 RVA: 0x00023C8C File Offset: 0x00023C8C
      [CompilerGenerated]
      private OnboardingTutorialResume(NotchLiveEvent.OnboardingTutorialResume original)
        : base(original)
      {
      }

      // Token: 0x060003D3 RID: 979 RVA: 0x00023C98 File Offset: 0x00023C98
      public OnboardingTutorialResume()
      {
      }
    }

    // Token: 0x02000049 RID: 73
    [Nullable(0)]
    public sealed class IntroHatchPeek : NotchLiveEvent, IEquatable<NotchLiveEvent.IntroHatchPeek>
    {
      // Token: 0x170000D7 RID: 215
      // (get) Token: 0x060003D4 RID: 980 RVA: 0x00023CA0 File Offset: 0x00023CA0
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.IntroHatchPeek);
        }
      }

      // Token: 0x060003D5 RID: 981 RVA: 0x00023CAC File Offset: 0x00023CAC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("IntroHatchPeek");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060003D6 RID: 982 RVA: 0x00023CF8 File Offset: 0x00023CF8
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x060003D7 RID: 983 RVA: 0x00023D04 File Offset: 0x00023D04
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.IntroHatchPeek left, NotchLiveEvent.IntroHatchPeek right)
      {
        return !(left == right);
      }

      // Token: 0x060003D8 RID: 984 RVA: 0x00023D10 File Offset: 0x00023D10
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.IntroHatchPeek left, NotchLiveEvent.IntroHatchPeek right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060003D9 RID: 985 RVA: 0x00023D24 File Offset: 0x00023D24
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x060003DA RID: 986 RVA: 0x00023D2C File Offset: 0x00023D2C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.IntroHatchPeek);
      }

      // Token: 0x060003DB RID: 987 RVA: 0x00023D3C File Offset: 0x00023D3C
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x060003DC RID: 988 RVA: 0x00023D48 File Offset: 0x00023D48
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.IntroHatchPeek other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x060003DE RID: 990 RVA: 0x00023D60 File Offset: 0x00023D60
      [CompilerGenerated]
      private IntroHatchPeek(NotchLiveEvent.IntroHatchPeek original)
        : base(original)
      {
      }

      // Token: 0x060003DF RID: 991 RVA: 0x00023D6C File Offset: 0x00023D6C
      public IntroHatchPeek()
      {
      }
    }

    // Token: 0x0200004A RID: 74
    [Nullable(0)]
    public sealed class MorningSuggestions : NotchLiveEvent, IEquatable<NotchLiveEvent.MorningSuggestions>
    {
      // Token: 0x060003E0 RID: 992 RVA: 0x00023D74 File Offset: 0x00023D74
      public MorningSuggestions(int Count)
      {
        this.Count = Count;
        base..ctor();
      }

      // Token: 0x170000D8 RID: 216
      // (get) Token: 0x060003E1 RID: 993 RVA: 0x00023D84 File Offset: 0x00023D84
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.MorningSuggestions);
        }
      }

      // Token: 0x170000D9 RID: 217
      // (get) Token: 0x060003E2 RID: 994 RVA: 0x00023D90 File Offset: 0x00023D90
      // (set) Token: 0x060003E3 RID: 995 RVA: 0x00023D98 File Offset: 0x00023D98
      public int Count { get; set; }

      // Token: 0x060003E4 RID: 996 RVA: 0x00023DA4 File Offset: 0x00023DA4
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("MorningSuggestions");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060003E5 RID: 997 RVA: 0x00023DF0 File Offset: 0x00023DF0
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Count = ");
        builder.Append(this.Count.ToString());
        return true;
      }

      // Token: 0x060003E6 RID: 998 RVA: 0x00023E3C File Offset: 0x00023E3C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.MorningSuggestions left, NotchLiveEvent.MorningSuggestions right)
      {
        return !(left == right);
      }

      // Token: 0x060003E7 RID: 999 RVA: 0x00023E48 File Offset: 0x00023E48
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.MorningSuggestions left, NotchLiveEvent.MorningSuggestions right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060003E8 RID: 1000 RVA: 0x00023E5C File Offset: 0x00023E5C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Count>k__BackingField);
      }

      // Token: 0x060003E9 RID: 1001 RVA: 0x00023E7C File Offset: 0x00023E7C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.MorningSuggestions);
      }

      // Token: 0x060003EA RID: 1002 RVA: 0x00023E8C File Offset: 0x00023E8C
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x060003EB RID: 1003 RVA: 0x00023E98 File Offset: 0x00023E98
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.MorningSuggestions other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Count>k__BackingField, other.<Count>k__BackingField));
      }

      // Token: 0x060003ED RID: 1005 RVA: 0x00023ECC File Offset: 0x00023ECC
      [CompilerGenerated]
      private MorningSuggestions(NotchLiveEvent.MorningSuggestions original)
        : base(original)
      {
        this.Count = original.<Count>k__BackingField;
      }

      // Token: 0x060003EE RID: 1006 RVA: 0x00023EE4 File Offset: 0x00023EE4
      [CompilerGenerated]
      public void Deconstruct(out int Count)
      {
        Count = this.Count;
      }
    }

    // Token: 0x0200004B RID: 75
    [Nullable(0)]
    public sealed class RestingMark : NotchLiveEvent, IEquatable<NotchLiveEvent.RestingMark>
    {
      // Token: 0x170000DA RID: 218
      // (get) Token: 0x060003EF RID: 1007 RVA: 0x00023EF0 File Offset: 0x00023EF0
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.RestingMark);
        }
      }

      // Token: 0x060003F0 RID: 1008 RVA: 0x00023EFC File Offset: 0x00023EFC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("RestingMark");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060003F1 RID: 1009 RVA: 0x00023F48 File Offset: 0x00023F48
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x060003F2 RID: 1010 RVA: 0x00023F54 File Offset: 0x00023F54
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.RestingMark left, NotchLiveEvent.RestingMark right)
      {
        return !(left == right);
      }

      // Token: 0x060003F3 RID: 1011 RVA: 0x00023F60 File Offset: 0x00023F60
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.RestingMark left, NotchLiveEvent.RestingMark right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060003F4 RID: 1012 RVA: 0x00023F74 File Offset: 0x00023F74
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x060003F5 RID: 1013 RVA: 0x00023F7C File Offset: 0x00023F7C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.RestingMark);
      }

      // Token: 0x060003F6 RID: 1014 RVA: 0x00023F8C File Offset: 0x00023F8C
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x060003F7 RID: 1015 RVA: 0x00023F98 File Offset: 0x00023F98
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.RestingMark other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x060003F9 RID: 1017 RVA: 0x00023FB0 File Offset: 0x00023FB0
      [CompilerGenerated]
      private RestingMark(NotchLiveEvent.RestingMark original)
        : base(original)
      {
      }

      // Token: 0x060003FA RID: 1018 RVA: 0x00023FBC File Offset: 0x00023FBC
      public RestingMark()
      {
      }
    }

    // Token: 0x0200004C RID: 76
    [Nullable(0)]
    public sealed class AgentPeek : NotchLiveEvent, IEquatable<NotchLiveEvent.AgentPeek>
    {
      // Token: 0x060003FB RID: 1019 RVA: 0x00023FC4 File Offset: 0x00023FC4
      public AgentPeek(double ListContentHeight = 0.0)
      {
        this.ListContentHeight = ListContentHeight;
        base..ctor();
      }

      // Token: 0x170000DB RID: 219
      // (get) Token: 0x060003FC RID: 1020 RVA: 0x00023FD4 File Offset: 0x00023FD4
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.AgentPeek);
        }
      }

      // Token: 0x170000DC RID: 220
      // (get) Token: 0x060003FD RID: 1021 RVA: 0x00023FE0 File Offset: 0x00023FE0
      // (set) Token: 0x060003FE RID: 1022 RVA: 0x00023FE8 File Offset: 0x00023FE8
      public double ListContentHeight { get; set; }

      // Token: 0x060003FF RID: 1023 RVA: 0x00023FF4 File Offset: 0x00023FF4
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("AgentPeek");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000400 RID: 1024 RVA: 0x00024040 File Offset: 0x00024040
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("ListContentHeight = ");
        builder.Append(this.ListContentHeight.ToString());
        return true;
      }

      // Token: 0x06000401 RID: 1025 RVA: 0x0002408C File Offset: 0x0002408C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.AgentPeek left, NotchLiveEvent.AgentPeek right)
      {
        return !(left == right);
      }

      // Token: 0x06000402 RID: 1026 RVA: 0x00024098 File Offset: 0x00024098
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.AgentPeek left, NotchLiveEvent.AgentPeek right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000403 RID: 1027 RVA: 0x000240AC File Offset: 0x000240AC
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<ListContentHeight>k__BackingField);
      }

      // Token: 0x06000404 RID: 1028 RVA: 0x000240CC File Offset: 0x000240CC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.AgentPeek);
      }

      // Token: 0x06000405 RID: 1029 RVA: 0x000240DC File Offset: 0x000240DC
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000406 RID: 1030 RVA: 0x000240E8 File Offset: 0x000240E8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.AgentPeek other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<double>.Default.Equals(this.<ListContentHeight>k__BackingField, other.<ListContentHeight>k__BackingField));
      }

      // Token: 0x06000408 RID: 1032 RVA: 0x0002411C File Offset: 0x0002411C
      [CompilerGenerated]
      private AgentPeek(NotchLiveEvent.AgentPeek original)
        : base(original)
      {
        this.ListContentHeight = original.<ListContentHeight>k__BackingField;
      }

      // Token: 0x06000409 RID: 1033 RVA: 0x00024134 File Offset: 0x00024134
      [CompilerGenerated]
      public void Deconstruct(out double ListContentHeight)
      {
        ListContentHeight = this.ListContentHeight;
      }
    }

    // Token: 0x0200004D RID: 77
    [Nullable(0)]
    public sealed class HandoffSent : NotchLiveEvent, IEquatable<NotchLiveEvent.HandoffSent>
    {
      // Token: 0x0600040A RID: 1034 RVA: 0x00024140 File Offset: 0x00024140
      public HandoffSent(string TargetDisplayName)
      {
        this.TargetDisplayName = TargetDisplayName;
        base..ctor();
      }

      // Token: 0x170000DD RID: 221
      // (get) Token: 0x0600040B RID: 1035 RVA: 0x00024150 File Offset: 0x00024150
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.HandoffSent);
        }
      }

      // Token: 0x170000DE RID: 222
      // (get) Token: 0x0600040C RID: 1036 RVA: 0x0002415C File Offset: 0x0002415C
      // (set) Token: 0x0600040D RID: 1037 RVA: 0x00024164 File Offset: 0x00024164
      public string TargetDisplayName { get; set; }

      // Token: 0x0600040E RID: 1038 RVA: 0x00024170 File Offset: 0x00024170
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("HandoffSent");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600040F RID: 1039 RVA: 0x000241BC File Offset: 0x000241BC
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("TargetDisplayName = ");
        builder.Append(this.TargetDisplayName);
        return true;
      }

      // Token: 0x06000410 RID: 1040 RVA: 0x000241F0 File Offset: 0x000241F0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.HandoffSent left, NotchLiveEvent.HandoffSent right)
      {
        return !(left == right);
      }

      // Token: 0x06000411 RID: 1041 RVA: 0x000241FC File Offset: 0x000241FC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.HandoffSent left, NotchLiveEvent.HandoffSent right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000412 RID: 1042 RVA: 0x00024210 File Offset: 0x00024210
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TargetDisplayName>k__BackingField);
      }

      // Token: 0x06000413 RID: 1043 RVA: 0x00024230 File Offset: 0x00024230
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.HandoffSent);
      }

      // Token: 0x06000414 RID: 1044 RVA: 0x00024240 File Offset: 0x00024240
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000415 RID: 1045 RVA: 0x0002424C File Offset: 0x0002424C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.HandoffSent other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<TargetDisplayName>k__BackingField, other.<TargetDisplayName>k__BackingField));
      }

      // Token: 0x06000417 RID: 1047 RVA: 0x00024280 File Offset: 0x00024280
      [CompilerGenerated]
      private HandoffSent(NotchLiveEvent.HandoffSent original)
        : base(original)
      {
        this.TargetDisplayName = original.<TargetDisplayName>k__BackingField;
      }

      // Token: 0x06000418 RID: 1048 RVA: 0x00024298 File Offset: 0x00024298
      [CompilerGenerated]
      public void Deconstruct(out string TargetDisplayName)
      {
        TargetDisplayName = this.TargetDisplayName;
      }
    }

    // Token: 0x0200004E RID: 78
    [Nullable(0)]
    public sealed class HandoffQueued : NotchLiveEvent, IEquatable<NotchLiveEvent.HandoffQueued>
    {
      // Token: 0x06000419 RID: 1049 RVA: 0x000242A4 File Offset: 0x000242A4
      public HandoffQueued(int Count)
      {
        this.Count = Count;
        base..ctor();
      }

      // Token: 0x170000DF RID: 223
      // (get) Token: 0x0600041A RID: 1050 RVA: 0x000242B4 File Offset: 0x000242B4
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.HandoffQueued);
        }
      }

      // Token: 0x170000E0 RID: 224
      // (get) Token: 0x0600041B RID: 1051 RVA: 0x000242C0 File Offset: 0x000242C0
      // (set) Token: 0x0600041C RID: 1052 RVA: 0x000242C8 File Offset: 0x000242C8
      public int Count { get; set; }

      // Token: 0x0600041D RID: 1053 RVA: 0x000242D4 File Offset: 0x000242D4
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("HandoffQueued");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600041E RID: 1054 RVA: 0x00024320 File Offset: 0x00024320
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Count = ");
        builder.Append(this.Count.ToString());
        return true;
      }

      // Token: 0x0600041F RID: 1055 RVA: 0x0002436C File Offset: 0x0002436C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.HandoffQueued left, NotchLiveEvent.HandoffQueued right)
      {
        return !(left == right);
      }

      // Token: 0x06000420 RID: 1056 RVA: 0x00024378 File Offset: 0x00024378
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.HandoffQueued left, NotchLiveEvent.HandoffQueued right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000421 RID: 1057 RVA: 0x0002438C File Offset: 0x0002438C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Count>k__BackingField);
      }

      // Token: 0x06000422 RID: 1058 RVA: 0x000243AC File Offset: 0x000243AC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.HandoffQueued);
      }

      // Token: 0x06000423 RID: 1059 RVA: 0x000243BC File Offset: 0x000243BC
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000424 RID: 1060 RVA: 0x000243C8 File Offset: 0x000243C8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.HandoffQueued other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Count>k__BackingField, other.<Count>k__BackingField));
      }

      // Token: 0x06000426 RID: 1062 RVA: 0x000243FC File Offset: 0x000243FC
      [CompilerGenerated]
      private HandoffQueued(NotchLiveEvent.HandoffQueued original)
        : base(original)
      {
        this.Count = original.<Count>k__BackingField;
      }

      // Token: 0x06000427 RID: 1063 RVA: 0x00024414 File Offset: 0x00024414
      [CompilerGenerated]
      public void Deconstruct(out int Count)
      {
        Count = this.Count;
      }
    }

    // Token: 0x0200004F RID: 79
    [Nullable(0)]
    public sealed class SidecarLearningSuggestion : NotchLiveEvent, IEquatable<NotchLiveEvent.SidecarLearningSuggestion>
    {
      // Token: 0x06000428 RID: 1064 RVA: 0x00024420 File Offset: 0x00024420
      public SidecarLearningSuggestion(string AppDisplayName, [Nullable(2)] string AppIconPath = null)
      {
        this.AppDisplayName = AppDisplayName;
        this.AppIconPath = AppIconPath;
        base..ctor();
      }

      // Token: 0x170000E1 RID: 225
      // (get) Token: 0x06000429 RID: 1065 RVA: 0x00024438 File Offset: 0x00024438
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.SidecarLearningSuggestion);
        }
      }

      // Token: 0x170000E2 RID: 226
      // (get) Token: 0x0600042A RID: 1066 RVA: 0x00024444 File Offset: 0x00024444
      // (set) Token: 0x0600042B RID: 1067 RVA: 0x0002444C File Offset: 0x0002444C
      public string AppDisplayName { get; set; }

      // Token: 0x170000E3 RID: 227
      // (get) Token: 0x0600042C RID: 1068 RVA: 0x00024458 File Offset: 0x00024458
      // (set) Token: 0x0600042D RID: 1069 RVA: 0x00024460 File Offset: 0x00024460
      [Nullable(2)]
      public string AppIconPath
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x0600042E RID: 1070 RVA: 0x0002446C File Offset: 0x0002446C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("SidecarLearningSuggestion");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600042F RID: 1071 RVA: 0x000244B8 File Offset: 0x000244B8
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("AppDisplayName = ");
        builder.Append(this.AppDisplayName);
        builder.Append(", AppIconPath = ");
        builder.Append(this.AppIconPath);
        return true;
      }

      // Token: 0x06000430 RID: 1072 RVA: 0x00024510 File Offset: 0x00024510
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.SidecarLearningSuggestion left, NotchLiveEvent.SidecarLearningSuggestion right)
      {
        return !(left == right);
      }

      // Token: 0x06000431 RID: 1073 RVA: 0x0002451C File Offset: 0x0002451C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.SidecarLearningSuggestion left, NotchLiveEvent.SidecarLearningSuggestion right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000432 RID: 1074 RVA: 0x00024530 File Offset: 0x00024530
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AppDisplayName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AppIconPath>k__BackingField);
      }

      // Token: 0x06000433 RID: 1075 RVA: 0x00024568 File Offset: 0x00024568
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.SidecarLearningSuggestion);
      }

      // Token: 0x06000434 RID: 1076 RVA: 0x00024578 File Offset: 0x00024578
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000435 RID: 1077 RVA: 0x00024584 File Offset: 0x00024584
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.SidecarLearningSuggestion other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<AppDisplayName>k__BackingField, other.<AppDisplayName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AppIconPath>k__BackingField, other.<AppIconPath>k__BackingField));
      }

      // Token: 0x06000437 RID: 1079 RVA: 0x000245D8 File Offset: 0x000245D8
      [CompilerGenerated]
      private SidecarLearningSuggestion(NotchLiveEvent.SidecarLearningSuggestion original)
        : base(original)
      {
        this.AppDisplayName = original.<AppDisplayName>k__BackingField;
        this.AppIconPath = original.<AppIconPath>k__BackingField;
      }

      // Token: 0x06000438 RID: 1080 RVA: 0x000245FC File Offset: 0x000245FC
      [CompilerGenerated]
      public void Deconstruct(out string AppDisplayName, [Nullable(2)] out string AppIconPath)
      {
        AppDisplayName = this.AppDisplayName;
        AppIconPath = this.AppIconPath;
      }
    }

    // Token: 0x02000050 RID: 80
    [Nullable(0)]
    public sealed class SpotifyVoiceTip : NotchLiveEvent, IEquatable<NotchLiveEvent.SpotifyVoiceTip>
    {
      // Token: 0x170000E4 RID: 228
      // (get) Token: 0x06000439 RID: 1081 RVA: 0x00024610 File Offset: 0x00024610
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.SpotifyVoiceTip);
        }
      }

      // Token: 0x0600043A RID: 1082 RVA: 0x0002461C File Offset: 0x0002461C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("SpotifyVoiceTip");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600043B RID: 1083 RVA: 0x00024668 File Offset: 0x00024668
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x0600043C RID: 1084 RVA: 0x00024674 File Offset: 0x00024674
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.SpotifyVoiceTip left, NotchLiveEvent.SpotifyVoiceTip right)
      {
        return !(left == right);
      }

      // Token: 0x0600043D RID: 1085 RVA: 0x00024680 File Offset: 0x00024680
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.SpotifyVoiceTip left, NotchLiveEvent.SpotifyVoiceTip right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600043E RID: 1086 RVA: 0x00024694 File Offset: 0x00024694
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x0600043F RID: 1087 RVA: 0x0002469C File Offset: 0x0002469C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.SpotifyVoiceTip);
      }

      // Token: 0x06000440 RID: 1088 RVA: 0x000246AC File Offset: 0x000246AC
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000441 RID: 1089 RVA: 0x000246B8 File Offset: 0x000246B8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.SpotifyVoiceTip other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x06000443 RID: 1091 RVA: 0x000246D0 File Offset: 0x000246D0
      [CompilerGenerated]
      private SpotifyVoiceTip(NotchLiveEvent.SpotifyVoiceTip original)
        : base(original)
      {
      }

      // Token: 0x06000444 RID: 1092 RVA: 0x000246DC File Offset: 0x000246DC
      public SpotifyVoiceTip()
      {
      }
    }

    // Token: 0x02000051 RID: 81
    [Nullable(0)]
    public sealed class DictionaryWordLearned : NotchLiveEvent, IEquatable<NotchLiveEvent.DictionaryWordLearned>
    {
      // Token: 0x06000445 RID: 1093 RVA: 0x000246E4 File Offset: 0x000246E4
      public DictionaryWordLearned(string Word)
      {
        this.Word = Word;
        base..ctor();
      }

      // Token: 0x170000E5 RID: 229
      // (get) Token: 0x06000446 RID: 1094 RVA: 0x000246F4 File Offset: 0x000246F4
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.DictionaryWordLearned);
        }
      }

      // Token: 0x170000E6 RID: 230
      // (get) Token: 0x06000447 RID: 1095 RVA: 0x00024700 File Offset: 0x00024700
      // (set) Token: 0x06000448 RID: 1096 RVA: 0x00024708 File Offset: 0x00024708
      public string Word { get; set; }

      // Token: 0x06000449 RID: 1097 RVA: 0x00024714 File Offset: 0x00024714
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("DictionaryWordLearned");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600044A RID: 1098 RVA: 0x00024760 File Offset: 0x00024760
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Word = ");
        builder.Append(this.Word);
        return true;
      }

      // Token: 0x0600044B RID: 1099 RVA: 0x00024794 File Offset: 0x00024794
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.DictionaryWordLearned left, NotchLiveEvent.DictionaryWordLearned right)
      {
        return !(left == right);
      }

      // Token: 0x0600044C RID: 1100 RVA: 0x000247A0 File Offset: 0x000247A0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.DictionaryWordLearned left, NotchLiveEvent.DictionaryWordLearned right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600044D RID: 1101 RVA: 0x000247B4 File Offset: 0x000247B4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Word>k__BackingField);
      }

      // Token: 0x0600044E RID: 1102 RVA: 0x000247D4 File Offset: 0x000247D4
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.DictionaryWordLearned);
      }

      // Token: 0x0600044F RID: 1103 RVA: 0x000247E4 File Offset: 0x000247E4
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000450 RID: 1104 RVA: 0x000247F0 File Offset: 0x000247F0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.DictionaryWordLearned other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Word>k__BackingField, other.<Word>k__BackingField));
      }

      // Token: 0x06000452 RID: 1106 RVA: 0x00024824 File Offset: 0x00024824
      [CompilerGenerated]
      private DictionaryWordLearned(NotchLiveEvent.DictionaryWordLearned original)
        : base(original)
      {
        this.Word = original.<Word>k__BackingField;
      }

      // Token: 0x06000453 RID: 1107 RVA: 0x0002483C File Offset: 0x0002483C
      [CompilerGenerated]
      public void Deconstruct(out string Word)
      {
        Word = this.Word;
      }
    }

    // Token: 0x02000052 RID: 82
    [Nullable(0)]
    public sealed class DocumentFolderAccessDenied : NotchLiveEvent, IEquatable<NotchLiveEvent.DocumentFolderAccessDenied>
    {
      // Token: 0x06000454 RID: 1108 RVA: 0x00024848 File Offset: 0x00024848
      public DocumentFolderAccessDenied(string Subtitle, bool ShowsDontShowAgain, [Nullable(2)] string Path = null)
      {
        this.Subtitle = Subtitle;
        this.ShowsDontShowAgain = ShowsDontShowAgain;
        this.Path = Path;
        base..ctor();
      }

      // Token: 0x170000E7 RID: 231
      // (get) Token: 0x06000455 RID: 1109 RVA: 0x00024868 File Offset: 0x00024868
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.DocumentFolderAccessDenied);
        }
      }

      // Token: 0x170000E8 RID: 232
      // (get) Token: 0x06000456 RID: 1110 RVA: 0x00024874 File Offset: 0x00024874
      // (set) Token: 0x06000457 RID: 1111 RVA: 0x0002487C File Offset: 0x0002487C
      public string Subtitle { get; set; }

      // Token: 0x170000E9 RID: 233
      // (get) Token: 0x06000458 RID: 1112 RVA: 0x00024888 File Offset: 0x00024888
      // (set) Token: 0x06000459 RID: 1113 RVA: 0x00024890 File Offset: 0x00024890
      public bool ShowsDontShowAgain { get; set; }

      // Token: 0x170000EA RID: 234
      // (get) Token: 0x0600045A RID: 1114 RVA: 0x0002489C File Offset: 0x0002489C
      // (set) Token: 0x0600045B RID: 1115 RVA: 0x000248A4 File Offset: 0x000248A4
      [Nullable(2)]
      public string Path
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x0600045C RID: 1116 RVA: 0x000248B0 File Offset: 0x000248B0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("DocumentFolderAccessDenied");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600045D RID: 1117 RVA: 0x000248FC File Offset: 0x000248FC
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Subtitle = ");
        builder.Append(this.Subtitle);
        builder.Append(", ShowsDontShowAgain = ");
        builder.Append(this.ShowsDontShowAgain.ToString());
        builder.Append(", Path = ");
        builder.Append(this.Path);
        return true;
      }

      // Token: 0x0600045E RID: 1118 RVA: 0x00024978 File Offset: 0x00024978
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.DocumentFolderAccessDenied left, NotchLiveEvent.DocumentFolderAccessDenied right)
      {
        return !(left == right);
      }

      // Token: 0x0600045F RID: 1119 RVA: 0x00024984 File Offset: 0x00024984
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.DocumentFolderAccessDenied left, NotchLiveEvent.DocumentFolderAccessDenied right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000460 RID: 1120 RVA: 0x00024998 File Offset: 0x00024998
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Subtitle>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ShowsDontShowAgain>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Path>k__BackingField);
      }

      // Token: 0x06000461 RID: 1121 RVA: 0x000249F0 File Offset: 0x000249F0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.DocumentFolderAccessDenied);
      }

      // Token: 0x06000462 RID: 1122 RVA: 0x00024A00 File Offset: 0x00024A00
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000463 RID: 1123 RVA: 0x00024A0C File Offset: 0x00024A0C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.DocumentFolderAccessDenied other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Subtitle>k__BackingField, other.<Subtitle>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<ShowsDontShowAgain>k__BackingField, other.<ShowsDontShowAgain>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Path>k__BackingField, other.<Path>k__BackingField));
      }

      // Token: 0x06000465 RID: 1125 RVA: 0x00024A78 File Offset: 0x00024A78
      [CompilerGenerated]
      private DocumentFolderAccessDenied(NotchLiveEvent.DocumentFolderAccessDenied original)
        : base(original)
      {
        this.Subtitle = original.<Subtitle>k__BackingField;
        this.ShowsDontShowAgain = original.<ShowsDontShowAgain>k__BackingField;
        this.Path = original.<Path>k__BackingField;
      }

      // Token: 0x06000466 RID: 1126 RVA: 0x00024AA8 File Offset: 0x00024AA8
      [CompilerGenerated]
      public void Deconstruct(out string Subtitle, out bool ShowsDontShowAgain, [Nullable(2)] out string Path)
      {
        Subtitle = this.Subtitle;
        ShowsDontShowAgain = this.ShowsDontShowAgain;
        Path = this.Path;
      }
    }

    // Token: 0x02000053 RID: 83
    [NullableContext(2)]
    [Nullable(0)]
    public sealed class IntroSkillPing : NotchLiveEvent, IEquatable<NotchLiveEvent.IntroSkillPing>
    {
      // Token: 0x06000467 RID: 1127 RVA: 0x00024AC4 File Offset: 0x00024AC4
      public IntroSkillPing(int WiggleToken = 0, bool LastToggleAdded = true, double ScaleBias = 0.0, string GlyphColorHex = null)
      {
        this.WiggleToken = WiggleToken;
        this.LastToggleAdded = LastToggleAdded;
        this.ScaleBias = ScaleBias;
        this.GlyphColorHex = GlyphColorHex;
        base..ctor();
      }

      // Token: 0x170000EB RID: 235
      // (get) Token: 0x06000468 RID: 1128 RVA: 0x00024AEC File Offset: 0x00024AEC
      [Nullable(1)]
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [NullableContext(1)]
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.IntroSkillPing);
        }
      }

      // Token: 0x170000EC RID: 236
      // (get) Token: 0x06000469 RID: 1129 RVA: 0x00024AF8 File Offset: 0x00024AF8
      // (set) Token: 0x0600046A RID: 1130 RVA: 0x00024B00 File Offset: 0x00024B00
      public int WiggleToken { get; set; }

      // Token: 0x170000ED RID: 237
      // (get) Token: 0x0600046B RID: 1131 RVA: 0x00024B0C File Offset: 0x00024B0C
      // (set) Token: 0x0600046C RID: 1132 RVA: 0x00024B14 File Offset: 0x00024B14
      public bool LastToggleAdded { get; set; }

      // Token: 0x170000EE RID: 238
      // (get) Token: 0x0600046D RID: 1133 RVA: 0x00024B20 File Offset: 0x00024B20
      // (set) Token: 0x0600046E RID: 1134 RVA: 0x00024B28 File Offset: 0x00024B28
      public double ScaleBias { get; set; }

      // Token: 0x170000EF RID: 239
      // (get) Token: 0x0600046F RID: 1135 RVA: 0x00024B34 File Offset: 0x00024B34
      // (set) Token: 0x06000470 RID: 1136 RVA: 0x00024B3C File Offset: 0x00024B3C
      public string GlyphColorHex { get; set; }

      // Token: 0x06000471 RID: 1137 RVA: 0x00024B48 File Offset: 0x00024B48
      [NullableContext(1)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("IntroSkillPing");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000472 RID: 1138 RVA: 0x00024B94 File Offset: 0x00024B94
      [NullableContext(1)]
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("WiggleToken = ");
        builder.Append(this.WiggleToken.ToString());
        builder.Append(", LastToggleAdded = ");
        builder.Append(this.LastToggleAdded.ToString());
        builder.Append(", ScaleBias = ");
        builder.Append(this.ScaleBias.ToString());
        builder.Append(", GlyphColorHex = ");
        builder.Append(this.GlyphColorHex);
        return true;
      }

      // Token: 0x06000473 RID: 1139 RVA: 0x00024C48 File Offset: 0x00024C48
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.IntroSkillPing left, NotchLiveEvent.IntroSkillPing right)
      {
        return !(left == right);
      }

      // Token: 0x06000474 RID: 1140 RVA: 0x00024C54 File Offset: 0x00024C54
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.IntroSkillPing left, NotchLiveEvent.IntroSkillPing right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000475 RID: 1141 RVA: 0x00024C68 File Offset: 0x00024C68
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<WiggleToken>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<LastToggleAdded>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<ScaleBias>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GlyphColorHex>k__BackingField);
      }

      // Token: 0x06000476 RID: 1142 RVA: 0x00024CD8 File Offset: 0x00024CD8
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.IntroSkillPing);
      }

      // Token: 0x06000477 RID: 1143 RVA: 0x00024CE8 File Offset: 0x00024CE8
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000478 RID: 1144 RVA: 0x00024CF4 File Offset: 0x00024CF4
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.IntroSkillPing other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<WiggleToken>k__BackingField, other.<WiggleToken>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<LastToggleAdded>k__BackingField, other.<LastToggleAdded>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<ScaleBias>k__BackingField, other.<ScaleBias>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<GlyphColorHex>k__BackingField, other.<GlyphColorHex>k__BackingField));
      }

      // Token: 0x0600047A RID: 1146 RVA: 0x00024D78 File Offset: 0x00024D78
      [CompilerGenerated]
      private IntroSkillPing([Nullable(1)] NotchLiveEvent.IntroSkillPing original)
        : base(original)
      {
        this.WiggleToken = original.<WiggleToken>k__BackingField;
        this.LastToggleAdded = original.<LastToggleAdded>k__BackingField;
        this.ScaleBias = original.<ScaleBias>k__BackingField;
        this.GlyphColorHex = original.<GlyphColorHex>k__BackingField;
      }

      // Token: 0x0600047B RID: 1147 RVA: 0x00024DB4 File Offset: 0x00024DB4
      [CompilerGenerated]
      public void Deconstruct(out int WiggleToken, out bool LastToggleAdded, out double ScaleBias, out string GlyphColorHex)
      {
        WiggleToken = this.WiggleToken;
        LastToggleAdded = this.LastToggleAdded;
        ScaleBias = this.ScaleBias;
        GlyphColorHex = this.GlyphColorHex;
      }
    }

    // Token: 0x02000054 RID: 84
    [Nullable(0)]
    public sealed class SkillPinned : NotchLiveEvent, IEquatable<NotchLiveEvent.SkillPinned>
    {
      // Token: 0x0600047C RID: 1148 RVA: 0x00024DD8 File Offset: 0x00024DD8
      public SkillPinned(int Count, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> Names = null)
      {
        this.Count = Count;
        this.Names = Names;
        base..ctor();
      }

      // Token: 0x170000F0 RID: 240
      // (get) Token: 0x0600047D RID: 1149 RVA: 0x00024DF0 File Offset: 0x00024DF0
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchLiveEvent.SkillPinned);
        }
      }

      // Token: 0x170000F1 RID: 241
      // (get) Token: 0x0600047E RID: 1150 RVA: 0x00024DFC File Offset: 0x00024DFC
      // (set) Token: 0x0600047F RID: 1151 RVA: 0x00024E04 File Offset: 0x00024E04
      public int Count { get; set; }

      // Token: 0x170000F2 RID: 242
      // (get) Token: 0x06000480 RID: 1152 RVA: 0x00024E10 File Offset: 0x00024E10
      // (set) Token: 0x06000481 RID: 1153 RVA: 0x00024E18 File Offset: 0x00024E18
      [Nullable(new byte[] { 2, 1 })]
      public IReadOnlyList<string> Names
      {
        [return: Nullable(new byte[] { 2, 1 })]
        get;
        [param: Nullable(new byte[] { 2, 1 })]
        set;
      }

      // Token: 0x06000482 RID: 1154 RVA: 0x00024E24 File Offset: 0x00024E24
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("SkillPinned");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000483 RID: 1155 RVA: 0x00024E70 File Offset: 0x00024E70
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Count = ");
        builder.Append(this.Count.ToString());
        builder.Append(", Names = ");
        builder.Append(this.Names);
        return true;
      }

      // Token: 0x06000484 RID: 1156 RVA: 0x00024ED4 File Offset: 0x00024ED4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchLiveEvent.SkillPinned left, NotchLiveEvent.SkillPinned right)
      {
        return !(left == right);
      }

      // Token: 0x06000485 RID: 1157 RVA: 0x00024EE0 File Offset: 0x00024EE0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchLiveEvent.SkillPinned left, NotchLiveEvent.SkillPinned right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000486 RID: 1158 RVA: 0x00024EF4 File Offset: 0x00024EF4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Count>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Names>k__BackingField);
      }

      // Token: 0x06000487 RID: 1159 RVA: 0x00024F2C File Offset: 0x00024F2C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchLiveEvent.SkillPinned);
      }

      // Token: 0x06000488 RID: 1160 RVA: 0x00024F3C File Offset: 0x00024F3C
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(NotchLiveEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000489 RID: 1161 RVA: 0x00024F48 File Offset: 0x00024F48
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchLiveEvent.SkillPinned other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Count>k__BackingField, other.<Count>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Names>k__BackingField, other.<Names>k__BackingField));
      }

      // Token: 0x0600048B RID: 1163 RVA: 0x00024F9C File Offset: 0x00024F9C
      [CompilerGenerated]
      private SkillPinned(NotchLiveEvent.SkillPinned original)
        : base(original)
      {
        this.Count = original.<Count>k__BackingField;
        this.Names = original.<Names>k__BackingField;
      }

      // Token: 0x0600048C RID: 1164 RVA: 0x00024FC0 File Offset: 0x00024FC0
      [CompilerGenerated]
      public void Deconstruct(out int Count, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<string> Names)
      {
        Count = this.Count;
        Names = this.Names;
      }
    }
  }
}
