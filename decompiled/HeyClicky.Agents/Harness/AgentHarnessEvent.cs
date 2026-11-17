using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.Agents.Jobs;

namespace HeyClicky.Agents.Harness
{
  // Token: 0x02000063 RID: 99
  [NullableContext(1)]
  [Nullable(0)]
  public abstract class AgentHarnessEvent : IEquatable<AgentHarnessEvent>
  {
    // Token: 0x17000209 RID: 521
    // (get) Token: 0x06000812 RID: 2066 RVA: 0x000FF570 File Offset: 0x000FF570
    [CompilerGenerated]
    protected virtual Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentHarnessEvent);
      }
    }

    // Token: 0x06000813 RID: 2067 RVA: 0x000FF57C File Offset: 0x000FF57C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentHarnessEvent");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000814 RID: 2068 RVA: 0x000FF5C8 File Offset: 0x000FF5C8
    [CompilerGenerated]
    protected virtual bool PrintMembers(StringBuilder builder)
    {
      return false;
    }

    // Token: 0x06000815 RID: 2069 RVA: 0x000FF5CC File Offset: 0x000FF5CC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentHarnessEvent left, AgentHarnessEvent right)
    {
      return !(left == right);
    }

    // Token: 0x06000816 RID: 2070 RVA: 0x000FF5D8 File Offset: 0x000FF5D8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentHarnessEvent left, AgentHarnessEvent right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000817 RID: 2071 RVA: 0x000FF5EC File Offset: 0x000FF5EC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
    }

    // Token: 0x06000818 RID: 2072 RVA: 0x000FF600 File Offset: 0x000FF600
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentHarnessEvent);
    }

    // Token: 0x06000819 RID: 2073 RVA: 0x000FF610 File Offset: 0x000FF610
    [NullableContext(2)]
    [CompilerGenerated]
    public virtual bool Equals(AgentHarnessEvent other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract);
    }

    // Token: 0x0600081B RID: 2075 RVA: 0x000FF630 File Offset: 0x000FF630
    [CompilerGenerated]
    protected AgentHarnessEvent(AgentHarnessEvent original)
    {
    }

    // Token: 0x0600081C RID: 2076 RVA: 0x000FF638 File Offset: 0x000FF638
    protected AgentHarnessEvent()
    {
    }

    // Token: 0x020001AA RID: 426
    [Nullable(0)]
    public sealed class TurnStats : AgentHarnessEvent, IEquatable<AgentHarnessEvent.TurnStats>
    {
      // Token: 0x06000E63 RID: 3683 RVA: 0x00118DE4 File Offset: 0x00118DE4
      public TurnStats(IReadOnlyDictionary<string, double> Stats)
      {
        this.Stats = Stats;
        base..ctor();
      }

      // Token: 0x170002DF RID: 735
      // (get) Token: 0x06000E64 RID: 3684 RVA: 0x00118DF4 File Offset: 0x00118DF4
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(AgentHarnessEvent.TurnStats);
        }
      }

      // Token: 0x170002E0 RID: 736
      // (get) Token: 0x06000E65 RID: 3685 RVA: 0x00118E00 File Offset: 0x00118E00
      // (set) Token: 0x06000E66 RID: 3686 RVA: 0x00118E08 File Offset: 0x00118E08
      public IReadOnlyDictionary<string, double> Stats { get; set; }

      // Token: 0x06000E67 RID: 3687 RVA: 0x00118E14 File Offset: 0x00118E14
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("TurnStats");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000E68 RID: 3688 RVA: 0x00118E60 File Offset: 0x00118E60
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Stats = ");
        builder.Append(this.Stats);
        return true;
      }

      // Token: 0x06000E69 RID: 3689 RVA: 0x00118E94 File Offset: 0x00118E94
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(AgentHarnessEvent.TurnStats left, AgentHarnessEvent.TurnStats right)
      {
        return !(left == right);
      }

      // Token: 0x06000E6A RID: 3690 RVA: 0x00118EA0 File Offset: 0x00118EA0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(AgentHarnessEvent.TurnStats left, AgentHarnessEvent.TurnStats right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000E6B RID: 3691 RVA: 0x00118EB4 File Offset: 0x00118EB4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<IReadOnlyDictionary<string, double>>.Default.GetHashCode(this.<Stats>k__BackingField);
      }

      // Token: 0x06000E6C RID: 3692 RVA: 0x00118ED4 File Offset: 0x00118ED4
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as AgentHarnessEvent.TurnStats);
      }

      // Token: 0x06000E6D RID: 3693 RVA: 0x00118EE4 File Offset: 0x00118EE4
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(AgentHarnessEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000E6E RID: 3694 RVA: 0x00118EF0 File Offset: 0x00118EF0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(AgentHarnessEvent.TurnStats other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<IReadOnlyDictionary<string, double>>.Default.Equals(this.<Stats>k__BackingField, other.<Stats>k__BackingField));
      }

      // Token: 0x06000E70 RID: 3696 RVA: 0x00118F24 File Offset: 0x00118F24
      [CompilerGenerated]
      private TurnStats(AgentHarnessEvent.TurnStats original)
        : base(original)
      {
        this.Stats = original.<Stats>k__BackingField;
      }

      // Token: 0x06000E71 RID: 3697 RVA: 0x00118F3C File Offset: 0x00118F3C
      [CompilerGenerated]
      public void Deconstruct(out IReadOnlyDictionary<string, double> Stats)
      {
        Stats = this.Stats;
      }
    }

    // Token: 0x020001AB RID: 427
    [Nullable(0)]
    public sealed class Commentary : AgentHarnessEvent, IEquatable<AgentHarnessEvent.Commentary>
    {
      // Token: 0x06000E72 RID: 3698 RVA: 0x00118F48 File Offset: 0x00118F48
      public Commentary(string Text)
      {
        this.Text = Text;
        base..ctor();
      }

      // Token: 0x170002E1 RID: 737
      // (get) Token: 0x06000E73 RID: 3699 RVA: 0x00118F58 File Offset: 0x00118F58
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(AgentHarnessEvent.Commentary);
        }
      }

      // Token: 0x170002E2 RID: 738
      // (get) Token: 0x06000E74 RID: 3700 RVA: 0x00118F64 File Offset: 0x00118F64
      // (set) Token: 0x06000E75 RID: 3701 RVA: 0x00118F6C File Offset: 0x00118F6C
      public string Text { get; set; }

      // Token: 0x06000E76 RID: 3702 RVA: 0x00118F78 File Offset: 0x00118F78
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Commentary");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000E77 RID: 3703 RVA: 0x00118FC4 File Offset: 0x00118FC4
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Text = ");
        builder.Append(this.Text);
        return true;
      }

      // Token: 0x06000E78 RID: 3704 RVA: 0x00118FF8 File Offset: 0x00118FF8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(AgentHarnessEvent.Commentary left, AgentHarnessEvent.Commentary right)
      {
        return !(left == right);
      }

      // Token: 0x06000E79 RID: 3705 RVA: 0x00119004 File Offset: 0x00119004
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(AgentHarnessEvent.Commentary left, AgentHarnessEvent.Commentary right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000E7A RID: 3706 RVA: 0x00119018 File Offset: 0x00119018
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField);
      }

      // Token: 0x06000E7B RID: 3707 RVA: 0x00119038 File Offset: 0x00119038
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as AgentHarnessEvent.Commentary);
      }

      // Token: 0x06000E7C RID: 3708 RVA: 0x00119048 File Offset: 0x00119048
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(AgentHarnessEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000E7D RID: 3709 RVA: 0x00119054 File Offset: 0x00119054
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(AgentHarnessEvent.Commentary other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField));
      }

      // Token: 0x06000E7F RID: 3711 RVA: 0x00119088 File Offset: 0x00119088
      [CompilerGenerated]
      private Commentary(AgentHarnessEvent.Commentary original)
        : base(original)
      {
        this.Text = original.<Text>k__BackingField;
      }

      // Token: 0x06000E80 RID: 3712 RVA: 0x001190A0 File Offset: 0x001190A0
      [CompilerGenerated]
      public void Deconstruct(out string Text)
      {
        Text = this.Text;
      }
    }

    // Token: 0x020001AC RID: 428
    [Nullable(0)]
    public sealed class Thinking : AgentHarnessEvent, IEquatable<AgentHarnessEvent.Thinking>
    {
      // Token: 0x06000E81 RID: 3713 RVA: 0x001190AC File Offset: 0x001190AC
      public Thinking(string Heading)
      {
        this.Heading = Heading;
        base..ctor();
      }

      // Token: 0x170002E3 RID: 739
      // (get) Token: 0x06000E82 RID: 3714 RVA: 0x001190BC File Offset: 0x001190BC
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(AgentHarnessEvent.Thinking);
        }
      }

      // Token: 0x170002E4 RID: 740
      // (get) Token: 0x06000E83 RID: 3715 RVA: 0x001190C8 File Offset: 0x001190C8
      // (set) Token: 0x06000E84 RID: 3716 RVA: 0x001190D0 File Offset: 0x001190D0
      public string Heading { get; set; }

      // Token: 0x06000E85 RID: 3717 RVA: 0x001190DC File Offset: 0x001190DC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Thinking");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000E86 RID: 3718 RVA: 0x00119128 File Offset: 0x00119128
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Heading = ");
        builder.Append(this.Heading);
        return true;
      }

      // Token: 0x06000E87 RID: 3719 RVA: 0x0011915C File Offset: 0x0011915C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(AgentHarnessEvent.Thinking left, AgentHarnessEvent.Thinking right)
      {
        return !(left == right);
      }

      // Token: 0x06000E88 RID: 3720 RVA: 0x00119168 File Offset: 0x00119168
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(AgentHarnessEvent.Thinking left, AgentHarnessEvent.Thinking right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000E89 RID: 3721 RVA: 0x0011917C File Offset: 0x0011917C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Heading>k__BackingField);
      }

      // Token: 0x06000E8A RID: 3722 RVA: 0x0011919C File Offset: 0x0011919C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as AgentHarnessEvent.Thinking);
      }

      // Token: 0x06000E8B RID: 3723 RVA: 0x001191AC File Offset: 0x001191AC
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(AgentHarnessEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000E8C RID: 3724 RVA: 0x001191B8 File Offset: 0x001191B8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(AgentHarnessEvent.Thinking other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Heading>k__BackingField, other.<Heading>k__BackingField));
      }

      // Token: 0x06000E8E RID: 3726 RVA: 0x001191EC File Offset: 0x001191EC
      [CompilerGenerated]
      private Thinking(AgentHarnessEvent.Thinking original)
        : base(original)
      {
        this.Heading = original.<Heading>k__BackingField;
      }

      // Token: 0x06000E8F RID: 3727 RVA: 0x00119204 File Offset: 0x00119204
      [CompilerGenerated]
      public void Deconstruct(out string Heading)
      {
        Heading = this.Heading;
      }
    }

    // Token: 0x020001AD RID: 429
    [Nullable(0)]
    public sealed class Plan : AgentHarnessEvent, IEquatable<AgentHarnessEvent.Plan>
    {
      // Token: 0x06000E90 RID: 3728 RVA: 0x00119210 File Offset: 0x00119210
      public Plan([TupleElementNames(new string[] { "Text", "Status" })] [Nullable(new byte[] { 1, 0, 1, 1 })] IReadOnlyList<ValueTuple<string, string>> Steps)
      {
        this.Steps = Steps;
        base..ctor();
      }

      // Token: 0x170002E5 RID: 741
      // (get) Token: 0x06000E91 RID: 3729 RVA: 0x00119220 File Offset: 0x00119220
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(AgentHarnessEvent.Plan);
        }
      }

      // Token: 0x170002E6 RID: 742
      // (get) Token: 0x06000E92 RID: 3730 RVA: 0x0011922C File Offset: 0x0011922C
      // (set) Token: 0x06000E93 RID: 3731 RVA: 0x00119234 File Offset: 0x00119234
      [TupleElementNames(new string[] { "Text", "Status" })]
      [Nullable(new byte[] { 1, 0, 1, 1 })]
      public IReadOnlyList<ValueTuple<string, string>> Steps
      {
        [return: TupleElementNames(new string[] { "Text", "Status" })]
        [return: Nullable(new byte[] { 1, 0, 1, 1 })]
        get;
        [param: TupleElementNames(new string[] { "Text", "Status" })]
        [param: Nullable(new byte[] { 1, 0, 1, 1 })]
        set;
      }

      // Token: 0x06000E94 RID: 3732 RVA: 0x00119240 File Offset: 0x00119240
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Plan");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000E95 RID: 3733 RVA: 0x0011928C File Offset: 0x0011928C
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Steps = ");
        builder.Append(this.Steps);
        return true;
      }

      // Token: 0x06000E96 RID: 3734 RVA: 0x001192C0 File Offset: 0x001192C0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(AgentHarnessEvent.Plan left, AgentHarnessEvent.Plan right)
      {
        return !(left == right);
      }

      // Token: 0x06000E97 RID: 3735 RVA: 0x001192CC File Offset: 0x001192CC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(AgentHarnessEvent.Plan left, AgentHarnessEvent.Plan right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000E98 RID: 3736 RVA: 0x001192E0 File Offset: 0x001192E0
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<IReadOnlyList<ValueTuple<string, string>>>.Default.GetHashCode(this.<Steps>k__BackingField);
      }

      // Token: 0x06000E99 RID: 3737 RVA: 0x00119300 File Offset: 0x00119300
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as AgentHarnessEvent.Plan);
      }

      // Token: 0x06000E9A RID: 3738 RVA: 0x00119310 File Offset: 0x00119310
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(AgentHarnessEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000E9B RID: 3739 RVA: 0x0011931C File Offset: 0x0011931C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(AgentHarnessEvent.Plan other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<IReadOnlyList<ValueTuple<string, string>>>.Default.Equals(this.<Steps>k__BackingField, other.<Steps>k__BackingField));
      }

      // Token: 0x06000E9D RID: 3741 RVA: 0x00119350 File Offset: 0x00119350
      [CompilerGenerated]
      private Plan(AgentHarnessEvent.Plan original)
        : base(original)
      {
        this.Steps = original.<Steps>k__BackingField;
      }

      // Token: 0x06000E9E RID: 3742 RVA: 0x00119368 File Offset: 0x00119368
      [CompilerGenerated]
      public void Deconstruct([TupleElementNames(new string[] { "Text", "Status" })] [Nullable(new byte[] { 1, 0, 1, 1 })] out IReadOnlyList<ValueTuple<string, string>> Steps)
      {
        Steps = this.Steps;
      }
    }

    // Token: 0x020001AE RID: 430
    [Nullable(0)]
    public sealed class ToolStarted : AgentHarnessEvent, IEquatable<AgentHarnessEvent.ToolStarted>
    {
      // Token: 0x06000E9F RID: 3743 RVA: 0x00119374 File Offset: 0x00119374
      public ToolStarted(string Id, string Tool, [Nullable(2)] string Server, [Nullable(2)] string Command)
      {
        this.Id = Id;
        this.Tool = Tool;
        this.Server = Server;
        this.Command = Command;
        base..ctor();
      }

      // Token: 0x170002E7 RID: 743
      // (get) Token: 0x06000EA0 RID: 3744 RVA: 0x0011939C File Offset: 0x0011939C
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(AgentHarnessEvent.ToolStarted);
        }
      }

      // Token: 0x170002E8 RID: 744
      // (get) Token: 0x06000EA1 RID: 3745 RVA: 0x001193A8 File Offset: 0x001193A8
      // (set) Token: 0x06000EA2 RID: 3746 RVA: 0x001193B0 File Offset: 0x001193B0
      public string Id { get; set; }

      // Token: 0x170002E9 RID: 745
      // (get) Token: 0x06000EA3 RID: 3747 RVA: 0x001193BC File Offset: 0x001193BC
      // (set) Token: 0x06000EA4 RID: 3748 RVA: 0x001193C4 File Offset: 0x001193C4
      public string Tool { get; set; }

      // Token: 0x170002EA RID: 746
      // (get) Token: 0x06000EA5 RID: 3749 RVA: 0x001193D0 File Offset: 0x001193D0
      // (set) Token: 0x06000EA6 RID: 3750 RVA: 0x001193D8 File Offset: 0x001193D8
      [Nullable(2)]
      public string Server
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x170002EB RID: 747
      // (get) Token: 0x06000EA7 RID: 3751 RVA: 0x001193E4 File Offset: 0x001193E4
      // (set) Token: 0x06000EA8 RID: 3752 RVA: 0x001193EC File Offset: 0x001193EC
      [Nullable(2)]
      public string Command
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x06000EA9 RID: 3753 RVA: 0x001193F8 File Offset: 0x001193F8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ToolStarted");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000EAA RID: 3754 RVA: 0x00119444 File Offset: 0x00119444
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Id = ");
        builder.Append(this.Id);
        builder.Append(", Tool = ");
        builder.Append(this.Tool);
        builder.Append(", Server = ");
        builder.Append(this.Server);
        builder.Append(", Command = ");
        builder.Append(this.Command);
        return true;
      }

      // Token: 0x06000EAB RID: 3755 RVA: 0x001194CC File Offset: 0x001194CC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(AgentHarnessEvent.ToolStarted left, AgentHarnessEvent.ToolStarted right)
      {
        return !(left == right);
      }

      // Token: 0x06000EAC RID: 3756 RVA: 0x001194D8 File Offset: 0x001194D8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(AgentHarnessEvent.ToolStarted left, AgentHarnessEvent.ToolStarted right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000EAD RID: 3757 RVA: 0x001194EC File Offset: 0x001194EC
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Tool>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Server>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Command>k__BackingField);
      }

      // Token: 0x06000EAE RID: 3758 RVA: 0x0011955C File Offset: 0x0011955C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as AgentHarnessEvent.ToolStarted);
      }

      // Token: 0x06000EAF RID: 3759 RVA: 0x0011956C File Offset: 0x0011956C
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(AgentHarnessEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000EB0 RID: 3760 RVA: 0x00119578 File Offset: 0x00119578
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(AgentHarnessEvent.ToolStarted other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Tool>k__BackingField, other.<Tool>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Server>k__BackingField, other.<Server>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Command>k__BackingField, other.<Command>k__BackingField));
      }

      // Token: 0x06000EB2 RID: 3762 RVA: 0x001195FC File Offset: 0x001195FC
      [CompilerGenerated]
      private ToolStarted(AgentHarnessEvent.ToolStarted original)
        : base(original)
      {
        this.Id = original.<Id>k__BackingField;
        this.Tool = original.<Tool>k__BackingField;
        this.Server = original.<Server>k__BackingField;
        this.Command = original.<Command>k__BackingField;
      }

      // Token: 0x06000EB3 RID: 3763 RVA: 0x00119638 File Offset: 0x00119638
      [CompilerGenerated]
      public void Deconstruct(out string Id, out string Tool, [Nullable(2)] out string Server, [Nullable(2)] out string Command)
      {
        Id = this.Id;
        Tool = this.Tool;
        Server = this.Server;
        Command = this.Command;
      }
    }

    // Token: 0x020001AF RID: 431
    [Nullable(0)]
    public sealed class ToolFinished : AgentHarnessEvent, IEquatable<AgentHarnessEvent.ToolFinished>
    {
      // Token: 0x06000EB4 RID: 3764 RVA: 0x0011965C File Offset: 0x0011965C
      public ToolFinished(string Id, string Tool, [Nullable(2)] string Server, [Nullable(2)] string Command)
      {
        this.Id = Id;
        this.Tool = Tool;
        this.Server = Server;
        this.Command = Command;
        base..ctor();
      }

      // Token: 0x170002EC RID: 748
      // (get) Token: 0x06000EB5 RID: 3765 RVA: 0x00119684 File Offset: 0x00119684
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(AgentHarnessEvent.ToolFinished);
        }
      }

      // Token: 0x170002ED RID: 749
      // (get) Token: 0x06000EB6 RID: 3766 RVA: 0x00119690 File Offset: 0x00119690
      // (set) Token: 0x06000EB7 RID: 3767 RVA: 0x00119698 File Offset: 0x00119698
      public string Id { get; set; }

      // Token: 0x170002EE RID: 750
      // (get) Token: 0x06000EB8 RID: 3768 RVA: 0x001196A4 File Offset: 0x001196A4
      // (set) Token: 0x06000EB9 RID: 3769 RVA: 0x001196AC File Offset: 0x001196AC
      public string Tool { get; set; }

      // Token: 0x170002EF RID: 751
      // (get) Token: 0x06000EBA RID: 3770 RVA: 0x001196B8 File Offset: 0x001196B8
      // (set) Token: 0x06000EBB RID: 3771 RVA: 0x001196C0 File Offset: 0x001196C0
      [Nullable(2)]
      public string Server
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x170002F0 RID: 752
      // (get) Token: 0x06000EBC RID: 3772 RVA: 0x001196CC File Offset: 0x001196CC
      // (set) Token: 0x06000EBD RID: 3773 RVA: 0x001196D4 File Offset: 0x001196D4
      [Nullable(2)]
      public string Command
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x06000EBE RID: 3774 RVA: 0x001196E0 File Offset: 0x001196E0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ToolFinished");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000EBF RID: 3775 RVA: 0x0011972C File Offset: 0x0011972C
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Id = ");
        builder.Append(this.Id);
        builder.Append(", Tool = ");
        builder.Append(this.Tool);
        builder.Append(", Server = ");
        builder.Append(this.Server);
        builder.Append(", Command = ");
        builder.Append(this.Command);
        return true;
      }

      // Token: 0x06000EC0 RID: 3776 RVA: 0x001197B4 File Offset: 0x001197B4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(AgentHarnessEvent.ToolFinished left, AgentHarnessEvent.ToolFinished right)
      {
        return !(left == right);
      }

      // Token: 0x06000EC1 RID: 3777 RVA: 0x001197C0 File Offset: 0x001197C0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(AgentHarnessEvent.ToolFinished left, AgentHarnessEvent.ToolFinished right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000EC2 RID: 3778 RVA: 0x001197D4 File Offset: 0x001197D4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Tool>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Server>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Command>k__BackingField);
      }

      // Token: 0x06000EC3 RID: 3779 RVA: 0x00119844 File Offset: 0x00119844
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as AgentHarnessEvent.ToolFinished);
      }

      // Token: 0x06000EC4 RID: 3780 RVA: 0x00119854 File Offset: 0x00119854
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(AgentHarnessEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000EC5 RID: 3781 RVA: 0x00119860 File Offset: 0x00119860
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(AgentHarnessEvent.ToolFinished other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Tool>k__BackingField, other.<Tool>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Server>k__BackingField, other.<Server>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Command>k__BackingField, other.<Command>k__BackingField));
      }

      // Token: 0x06000EC7 RID: 3783 RVA: 0x001198E4 File Offset: 0x001198E4
      [CompilerGenerated]
      private ToolFinished(AgentHarnessEvent.ToolFinished original)
        : base(original)
      {
        this.Id = original.<Id>k__BackingField;
        this.Tool = original.<Tool>k__BackingField;
        this.Server = original.<Server>k__BackingField;
        this.Command = original.<Command>k__BackingField;
      }

      // Token: 0x06000EC8 RID: 3784 RVA: 0x00119920 File Offset: 0x00119920
      [CompilerGenerated]
      public void Deconstruct(out string Id, out string Tool, [Nullable(2)] out string Server, [Nullable(2)] out string Command)
      {
        Id = this.Id;
        Tool = this.Tool;
        Server = this.Server;
        Command = this.Command;
      }
    }

    // Token: 0x020001B0 RID: 432
    [Nullable(0)]
    public sealed class WebSearch : AgentHarnessEvent, IEquatable<AgentHarnessEvent.WebSearch>
    {
      // Token: 0x06000EC9 RID: 3785 RVA: 0x00119944 File Offset: 0x00119944
      public WebSearch(string Id, string Query, [Nullable(2)] string Url)
      {
        this.Id = Id;
        this.Query = Query;
        this.Url = Url;
        base..ctor();
      }

      // Token: 0x170002F1 RID: 753
      // (get) Token: 0x06000ECA RID: 3786 RVA: 0x00119964 File Offset: 0x00119964
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(AgentHarnessEvent.WebSearch);
        }
      }

      // Token: 0x170002F2 RID: 754
      // (get) Token: 0x06000ECB RID: 3787 RVA: 0x00119970 File Offset: 0x00119970
      // (set) Token: 0x06000ECC RID: 3788 RVA: 0x00119978 File Offset: 0x00119978
      public string Id { get; set; }

      // Token: 0x170002F3 RID: 755
      // (get) Token: 0x06000ECD RID: 3789 RVA: 0x00119984 File Offset: 0x00119984
      // (set) Token: 0x06000ECE RID: 3790 RVA: 0x0011998C File Offset: 0x0011998C
      public string Query { get; set; }

      // Token: 0x170002F4 RID: 756
      // (get) Token: 0x06000ECF RID: 3791 RVA: 0x00119998 File Offset: 0x00119998
      // (set) Token: 0x06000ED0 RID: 3792 RVA: 0x001199A0 File Offset: 0x001199A0
      [Nullable(2)]
      public string Url
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x06000ED1 RID: 3793 RVA: 0x001199AC File Offset: 0x001199AC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("WebSearch");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000ED2 RID: 3794 RVA: 0x001199F8 File Offset: 0x001199F8
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Id = ");
        builder.Append(this.Id);
        builder.Append(", Query = ");
        builder.Append(this.Query);
        builder.Append(", Url = ");
        builder.Append(this.Url);
        return true;
      }

      // Token: 0x06000ED3 RID: 3795 RVA: 0x00119A68 File Offset: 0x00119A68
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(AgentHarnessEvent.WebSearch left, AgentHarnessEvent.WebSearch right)
      {
        return !(left == right);
      }

      // Token: 0x06000ED4 RID: 3796 RVA: 0x00119A74 File Offset: 0x00119A74
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(AgentHarnessEvent.WebSearch left, AgentHarnessEvent.WebSearch right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000ED5 RID: 3797 RVA: 0x00119A88 File Offset: 0x00119A88
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Query>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Url>k__BackingField);
      }

      // Token: 0x06000ED6 RID: 3798 RVA: 0x00119AE0 File Offset: 0x00119AE0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as AgentHarnessEvent.WebSearch);
      }

      // Token: 0x06000ED7 RID: 3799 RVA: 0x00119AF0 File Offset: 0x00119AF0
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(AgentHarnessEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000ED8 RID: 3800 RVA: 0x00119AFC File Offset: 0x00119AFC
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(AgentHarnessEvent.WebSearch other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Query>k__BackingField, other.<Query>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Url>k__BackingField, other.<Url>k__BackingField));
      }

      // Token: 0x06000EDA RID: 3802 RVA: 0x00119B68 File Offset: 0x00119B68
      [CompilerGenerated]
      private WebSearch(AgentHarnessEvent.WebSearch original)
        : base(original)
      {
        this.Id = original.<Id>k__BackingField;
        this.Query = original.<Query>k__BackingField;
        this.Url = original.<Url>k__BackingField;
      }

      // Token: 0x06000EDB RID: 3803 RVA: 0x00119B98 File Offset: 0x00119B98
      [CompilerGenerated]
      public void Deconstruct(out string Id, out string Query, [Nullable(2)] out string Url)
      {
        Id = this.Id;
        Query = this.Query;
        Url = this.Url;
      }
    }

    // Token: 0x020001B1 RID: 433
    [Nullable(0)]
    public sealed class ProgramBatch : AgentHarnessEvent, IEquatable<AgentHarnessEvent.ProgramBatch>
    {
      // Token: 0x06000EDC RID: 3804 RVA: 0x00119BB4 File Offset: 0x00119BB4
      public ProgramBatch(ClickyProgramBatch Batch)
      {
        this.Batch = Batch;
        base..ctor();
      }

      // Token: 0x170002F5 RID: 757
      // (get) Token: 0x06000EDD RID: 3805 RVA: 0x00119BC4 File Offset: 0x00119BC4
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(AgentHarnessEvent.ProgramBatch);
        }
      }

      // Token: 0x170002F6 RID: 758
      // (get) Token: 0x06000EDE RID: 3806 RVA: 0x00119BD0 File Offset: 0x00119BD0
      // (set) Token: 0x06000EDF RID: 3807 RVA: 0x00119BD8 File Offset: 0x00119BD8
      public ClickyProgramBatch Batch { get; set; }

      // Token: 0x06000EE0 RID: 3808 RVA: 0x00119BE4 File Offset: 0x00119BE4
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ProgramBatch");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000EE1 RID: 3809 RVA: 0x00119C30 File Offset: 0x00119C30
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Batch = ");
        builder.Append(this.Batch);
        return true;
      }

      // Token: 0x06000EE2 RID: 3810 RVA: 0x00119C64 File Offset: 0x00119C64
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(AgentHarnessEvent.ProgramBatch left, AgentHarnessEvent.ProgramBatch right)
      {
        return !(left == right);
      }

      // Token: 0x06000EE3 RID: 3811 RVA: 0x00119C70 File Offset: 0x00119C70
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(AgentHarnessEvent.ProgramBatch left, AgentHarnessEvent.ProgramBatch right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000EE4 RID: 3812 RVA: 0x00119C84 File Offset: 0x00119C84
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<ClickyProgramBatch>.Default.GetHashCode(this.<Batch>k__BackingField);
      }

      // Token: 0x06000EE5 RID: 3813 RVA: 0x00119CA4 File Offset: 0x00119CA4
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as AgentHarnessEvent.ProgramBatch);
      }

      // Token: 0x06000EE6 RID: 3814 RVA: 0x00119CB4 File Offset: 0x00119CB4
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(AgentHarnessEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000EE7 RID: 3815 RVA: 0x00119CC0 File Offset: 0x00119CC0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(AgentHarnessEvent.ProgramBatch other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<ClickyProgramBatch>.Default.Equals(this.<Batch>k__BackingField, other.<Batch>k__BackingField));
      }

      // Token: 0x06000EE9 RID: 3817 RVA: 0x00119CF4 File Offset: 0x00119CF4
      [CompilerGenerated]
      private ProgramBatch(AgentHarnessEvent.ProgramBatch original)
        : base(original)
      {
        this.Batch = original.<Batch>k__BackingField;
      }

      // Token: 0x06000EEA RID: 3818 RVA: 0x00119D0C File Offset: 0x00119D0C
      [CompilerGenerated]
      public void Deconstruct(out ClickyProgramBatch Batch)
      {
        Batch = this.Batch;
      }
    }

    // Token: 0x020001B2 RID: 434
    [Nullable(0)]
    public sealed class AnswerDelta : AgentHarnessEvent, IEquatable<AgentHarnessEvent.AnswerDelta>
    {
      // Token: 0x06000EEB RID: 3819 RVA: 0x00119D18 File Offset: 0x00119D18
      public AnswerDelta(string Text)
      {
        this.Text = Text;
        base..ctor();
      }

      // Token: 0x170002F7 RID: 759
      // (get) Token: 0x06000EEC RID: 3820 RVA: 0x00119D28 File Offset: 0x00119D28
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(AgentHarnessEvent.AnswerDelta);
        }
      }

      // Token: 0x170002F8 RID: 760
      // (get) Token: 0x06000EED RID: 3821 RVA: 0x00119D34 File Offset: 0x00119D34
      // (set) Token: 0x06000EEE RID: 3822 RVA: 0x00119D3C File Offset: 0x00119D3C
      public string Text { get; set; }

      // Token: 0x06000EEF RID: 3823 RVA: 0x00119D48 File Offset: 0x00119D48
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("AnswerDelta");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000EF0 RID: 3824 RVA: 0x00119D94 File Offset: 0x00119D94
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Text = ");
        builder.Append(this.Text);
        return true;
      }

      // Token: 0x06000EF1 RID: 3825 RVA: 0x00119DC8 File Offset: 0x00119DC8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(AgentHarnessEvent.AnswerDelta left, AgentHarnessEvent.AnswerDelta right)
      {
        return !(left == right);
      }

      // Token: 0x06000EF2 RID: 3826 RVA: 0x00119DD4 File Offset: 0x00119DD4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(AgentHarnessEvent.AnswerDelta left, AgentHarnessEvent.AnswerDelta right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000EF3 RID: 3827 RVA: 0x00119DE8 File Offset: 0x00119DE8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField);
      }

      // Token: 0x06000EF4 RID: 3828 RVA: 0x00119E08 File Offset: 0x00119E08
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as AgentHarnessEvent.AnswerDelta);
      }

      // Token: 0x06000EF5 RID: 3829 RVA: 0x00119E18 File Offset: 0x00119E18
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(AgentHarnessEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000EF6 RID: 3830 RVA: 0x00119E24 File Offset: 0x00119E24
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(AgentHarnessEvent.AnswerDelta other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField));
      }

      // Token: 0x06000EF8 RID: 3832 RVA: 0x00119E58 File Offset: 0x00119E58
      [CompilerGenerated]
      private AnswerDelta(AgentHarnessEvent.AnswerDelta original)
        : base(original)
      {
        this.Text = original.<Text>k__BackingField;
      }

      // Token: 0x06000EF9 RID: 3833 RVA: 0x00119E70 File Offset: 0x00119E70
      [CompilerGenerated]
      public void Deconstruct(out string Text)
      {
        Text = this.Text;
      }
    }

    // Token: 0x020001B3 RID: 435
    [Nullable(0)]
    public sealed class Finished : AgentHarnessEvent, IEquatable<AgentHarnessEvent.Finished>
    {
      // Token: 0x06000EFA RID: 3834 RVA: 0x00119E7C File Offset: 0x00119E7C
      public Finished(string Status, [Nullable(2)] string Error)
      {
        this.Status = Status;
        this.Error = Error;
        base..ctor();
      }

      // Token: 0x170002F9 RID: 761
      // (get) Token: 0x06000EFB RID: 3835 RVA: 0x00119E94 File Offset: 0x00119E94
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(AgentHarnessEvent.Finished);
        }
      }

      // Token: 0x170002FA RID: 762
      // (get) Token: 0x06000EFC RID: 3836 RVA: 0x00119EA0 File Offset: 0x00119EA0
      // (set) Token: 0x06000EFD RID: 3837 RVA: 0x00119EA8 File Offset: 0x00119EA8
      public string Status { get; set; }

      // Token: 0x170002FB RID: 763
      // (get) Token: 0x06000EFE RID: 3838 RVA: 0x00119EB4 File Offset: 0x00119EB4
      // (set) Token: 0x06000EFF RID: 3839 RVA: 0x00119EBC File Offset: 0x00119EBC
      [Nullable(2)]
      public string Error
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x06000F00 RID: 3840 RVA: 0x00119EC8 File Offset: 0x00119EC8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Finished");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000F01 RID: 3841 RVA: 0x00119F14 File Offset: 0x00119F14
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Status = ");
        builder.Append(this.Status);
        builder.Append(", Error = ");
        builder.Append(this.Error);
        return true;
      }

      // Token: 0x06000F02 RID: 3842 RVA: 0x00119F6C File Offset: 0x00119F6C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(AgentHarnessEvent.Finished left, AgentHarnessEvent.Finished right)
      {
        return !(left == right);
      }

      // Token: 0x06000F03 RID: 3843 RVA: 0x00119F78 File Offset: 0x00119F78
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(AgentHarnessEvent.Finished left, AgentHarnessEvent.Finished right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000F04 RID: 3844 RVA: 0x00119F8C File Offset: 0x00119F8C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Status>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Error>k__BackingField);
      }

      // Token: 0x06000F05 RID: 3845 RVA: 0x00119FC4 File Offset: 0x00119FC4
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as AgentHarnessEvent.Finished);
      }

      // Token: 0x06000F06 RID: 3846 RVA: 0x00119FD4 File Offset: 0x00119FD4
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(AgentHarnessEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000F07 RID: 3847 RVA: 0x00119FE0 File Offset: 0x00119FE0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(AgentHarnessEvent.Finished other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Error>k__BackingField, other.<Error>k__BackingField));
      }

      // Token: 0x06000F09 RID: 3849 RVA: 0x0011A034 File Offset: 0x0011A034
      [CompilerGenerated]
      private Finished(AgentHarnessEvent.Finished original)
        : base(original)
      {
        this.Status = original.<Status>k__BackingField;
        this.Error = original.<Error>k__BackingField;
      }

      // Token: 0x06000F0A RID: 3850 RVA: 0x0011A058 File Offset: 0x0011A058
      [CompilerGenerated]
      public void Deconstruct(out string Status, [Nullable(2)] out string Error)
      {
        Status = this.Status;
        Error = this.Error;
      }
    }

    // Token: 0x020001B4 RID: 436
    [Nullable(0)]
    public sealed class NotePreview : AgentHarnessEvent, IEquatable<AgentHarnessEvent.NotePreview>
    {
      // Token: 0x06000F0B RID: 3851 RVA: 0x0011A06C File Offset: 0x0011A06C
      public NotePreview(string Path, string Text)
      {
        this.Path = Path;
        this.Text = Text;
        base..ctor();
      }

      // Token: 0x170002FC RID: 764
      // (get) Token: 0x06000F0C RID: 3852 RVA: 0x0011A084 File Offset: 0x0011A084
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(AgentHarnessEvent.NotePreview);
        }
      }

      // Token: 0x170002FD RID: 765
      // (get) Token: 0x06000F0D RID: 3853 RVA: 0x0011A090 File Offset: 0x0011A090
      // (set) Token: 0x06000F0E RID: 3854 RVA: 0x0011A098 File Offset: 0x0011A098
      public string Path { get; set; }

      // Token: 0x170002FE RID: 766
      // (get) Token: 0x06000F0F RID: 3855 RVA: 0x0011A0A4 File Offset: 0x0011A0A4
      // (set) Token: 0x06000F10 RID: 3856 RVA: 0x0011A0AC File Offset: 0x0011A0AC
      public string Text { get; set; }

      // Token: 0x06000F11 RID: 3857 RVA: 0x0011A0B8 File Offset: 0x0011A0B8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("NotePreview");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000F12 RID: 3858 RVA: 0x0011A104 File Offset: 0x0011A104
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Path = ");
        builder.Append(this.Path);
        builder.Append(", Text = ");
        builder.Append(this.Text);
        return true;
      }

      // Token: 0x06000F13 RID: 3859 RVA: 0x0011A15C File Offset: 0x0011A15C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(AgentHarnessEvent.NotePreview left, AgentHarnessEvent.NotePreview right)
      {
        return !(left == right);
      }

      // Token: 0x06000F14 RID: 3860 RVA: 0x0011A168 File Offset: 0x0011A168
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(AgentHarnessEvent.NotePreview left, AgentHarnessEvent.NotePreview right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000F15 RID: 3861 RVA: 0x0011A17C File Offset: 0x0011A17C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Path>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField);
      }

      // Token: 0x06000F16 RID: 3862 RVA: 0x0011A1B4 File Offset: 0x0011A1B4
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as AgentHarnessEvent.NotePreview);
      }

      // Token: 0x06000F17 RID: 3863 RVA: 0x0011A1C4 File Offset: 0x0011A1C4
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(AgentHarnessEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000F18 RID: 3864 RVA: 0x0011A1D0 File Offset: 0x0011A1D0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(AgentHarnessEvent.NotePreview other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Path>k__BackingField, other.<Path>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField));
      }

      // Token: 0x06000F1A RID: 3866 RVA: 0x0011A224 File Offset: 0x0011A224
      [CompilerGenerated]
      private NotePreview(AgentHarnessEvent.NotePreview original)
        : base(original)
      {
        this.Path = original.<Path>k__BackingField;
        this.Text = original.<Text>k__BackingField;
      }

      // Token: 0x06000F1B RID: 3867 RVA: 0x0011A248 File Offset: 0x0011A248
      [CompilerGenerated]
      public void Deconstruct(out string Path, out string Text)
      {
        Path = this.Path;
        Text = this.Text;
      }
    }

    // Token: 0x020001B5 RID: 437
    [Nullable(0)]
    public sealed class NoteSaved : AgentHarnessEvent, IEquatable<AgentHarnessEvent.NoteSaved>
    {
      // Token: 0x06000F1C RID: 3868 RVA: 0x0011A25C File Offset: 0x0011A25C
      public NoteSaved(string Path)
      {
        this.Path = Path;
        base..ctor();
      }

      // Token: 0x170002FF RID: 767
      // (get) Token: 0x06000F1D RID: 3869 RVA: 0x0011A26C File Offset: 0x0011A26C
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(AgentHarnessEvent.NoteSaved);
        }
      }

      // Token: 0x17000300 RID: 768
      // (get) Token: 0x06000F1E RID: 3870 RVA: 0x0011A278 File Offset: 0x0011A278
      // (set) Token: 0x06000F1F RID: 3871 RVA: 0x0011A280 File Offset: 0x0011A280
      public string Path { get; set; }

      // Token: 0x06000F20 RID: 3872 RVA: 0x0011A28C File Offset: 0x0011A28C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("NoteSaved");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000F21 RID: 3873 RVA: 0x0011A2D8 File Offset: 0x0011A2D8
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Path = ");
        builder.Append(this.Path);
        return true;
      }

      // Token: 0x06000F22 RID: 3874 RVA: 0x0011A30C File Offset: 0x0011A30C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(AgentHarnessEvent.NoteSaved left, AgentHarnessEvent.NoteSaved right)
      {
        return !(left == right);
      }

      // Token: 0x06000F23 RID: 3875 RVA: 0x0011A318 File Offset: 0x0011A318
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(AgentHarnessEvent.NoteSaved left, AgentHarnessEvent.NoteSaved right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000F24 RID: 3876 RVA: 0x0011A32C File Offset: 0x0011A32C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Path>k__BackingField);
      }

      // Token: 0x06000F25 RID: 3877 RVA: 0x0011A34C File Offset: 0x0011A34C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as AgentHarnessEvent.NoteSaved);
      }

      // Token: 0x06000F26 RID: 3878 RVA: 0x0011A35C File Offset: 0x0011A35C
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(AgentHarnessEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000F27 RID: 3879 RVA: 0x0011A368 File Offset: 0x0011A368
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(AgentHarnessEvent.NoteSaved other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Path>k__BackingField, other.<Path>k__BackingField));
      }

      // Token: 0x06000F29 RID: 3881 RVA: 0x0011A39C File Offset: 0x0011A39C
      [CompilerGenerated]
      private NoteSaved(AgentHarnessEvent.NoteSaved original)
        : base(original)
      {
        this.Path = original.<Path>k__BackingField;
      }

      // Token: 0x06000F2A RID: 3882 RVA: 0x0011A3B4 File Offset: 0x0011A3B4
      [CompilerGenerated]
      public void Deconstruct(out string Path)
      {
        Path = this.Path;
      }
    }

    // Token: 0x020001B6 RID: 438
    [Nullable(0)]
    public sealed class ConnectedAppsUnreachable : AgentHarnessEvent, IEquatable<AgentHarnessEvent.ConnectedAppsUnreachable>
    {
      // Token: 0x17000301 RID: 769
      // (get) Token: 0x06000F2B RID: 3883 RVA: 0x0011A3C0 File Offset: 0x0011A3C0
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(AgentHarnessEvent.ConnectedAppsUnreachable);
        }
      }

      // Token: 0x06000F2C RID: 3884 RVA: 0x0011A3CC File Offset: 0x0011A3CC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ConnectedAppsUnreachable");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000F2D RID: 3885 RVA: 0x0011A418 File Offset: 0x0011A418
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x06000F2E RID: 3886 RVA: 0x0011A424 File Offset: 0x0011A424
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(AgentHarnessEvent.ConnectedAppsUnreachable left, AgentHarnessEvent.ConnectedAppsUnreachable right)
      {
        return !(left == right);
      }

      // Token: 0x06000F2F RID: 3887 RVA: 0x0011A430 File Offset: 0x0011A430
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(AgentHarnessEvent.ConnectedAppsUnreachable left, AgentHarnessEvent.ConnectedAppsUnreachable right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000F30 RID: 3888 RVA: 0x0011A444 File Offset: 0x0011A444
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x06000F31 RID: 3889 RVA: 0x0011A44C File Offset: 0x0011A44C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as AgentHarnessEvent.ConnectedAppsUnreachable);
      }

      // Token: 0x06000F32 RID: 3890 RVA: 0x0011A45C File Offset: 0x0011A45C
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(AgentHarnessEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000F33 RID: 3891 RVA: 0x0011A468 File Offset: 0x0011A468
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(AgentHarnessEvent.ConnectedAppsUnreachable other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x06000F35 RID: 3893 RVA: 0x0011A480 File Offset: 0x0011A480
      [CompilerGenerated]
      private ConnectedAppsUnreachable(AgentHarnessEvent.ConnectedAppsUnreachable original)
        : base(original)
      {
      }

      // Token: 0x06000F36 RID: 3894 RVA: 0x0011A48C File Offset: 0x0011A48C
      public ConnectedAppsUnreachable()
      {
      }
    }

    // Token: 0x020001B7 RID: 439
    [Nullable(0)]
    public sealed class Compacted : AgentHarnessEvent, IEquatable<AgentHarnessEvent.Compacted>
    {
      // Token: 0x17000302 RID: 770
      // (get) Token: 0x06000F37 RID: 3895 RVA: 0x0011A494 File Offset: 0x0011A494
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(AgentHarnessEvent.Compacted);
        }
      }

      // Token: 0x06000F38 RID: 3896 RVA: 0x0011A4A0 File Offset: 0x0011A4A0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Compacted");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000F39 RID: 3897 RVA: 0x0011A4EC File Offset: 0x0011A4EC
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x06000F3A RID: 3898 RVA: 0x0011A4F8 File Offset: 0x0011A4F8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(AgentHarnessEvent.Compacted left, AgentHarnessEvent.Compacted right)
      {
        return !(left == right);
      }

      // Token: 0x06000F3B RID: 3899 RVA: 0x0011A504 File Offset: 0x0011A504
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(AgentHarnessEvent.Compacted left, AgentHarnessEvent.Compacted right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000F3C RID: 3900 RVA: 0x0011A518 File Offset: 0x0011A518
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x06000F3D RID: 3901 RVA: 0x0011A520 File Offset: 0x0011A520
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as AgentHarnessEvent.Compacted);
      }

      // Token: 0x06000F3E RID: 3902 RVA: 0x0011A530 File Offset: 0x0011A530
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(AgentHarnessEvent other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000F3F RID: 3903 RVA: 0x0011A53C File Offset: 0x0011A53C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(AgentHarnessEvent.Compacted other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x06000F41 RID: 3905 RVA: 0x0011A554 File Offset: 0x0011A554
      [CompilerGenerated]
      private Compacted(AgentHarnessEvent.Compacted original)
        : base(original)
      {
      }

      // Token: 0x06000F42 RID: 3906 RVA: 0x0011A560 File Offset: 0x0011A560
      public Compacted()
      {
      }
    }
  }
}
