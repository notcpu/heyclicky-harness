using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Notch
{
  // Token: 0x02000102 RID: 258
  [NullableContext(1)]
  [Nullable(0)]
  public abstract class ChatRow : IEquatable<ChatRow>
  {
    // Token: 0x170003FE RID: 1022
    // (get) Token: 0x0600110C RID: 4364 RVA: 0x00399058 File Offset: 0x00399058
    [CompilerGenerated]
    protected virtual Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ChatRow);
      }
    }

    // Token: 0x170003FF RID: 1023
    // (get) Token: 0x0600110D RID: 4365
    public abstract string Id { get; }

    // Token: 0x0600110E RID: 4366 RVA: 0x00399064 File Offset: 0x00399064
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ChatRow");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600110F RID: 4367 RVA: 0x003990B0 File Offset: 0x003990B0
    [CompilerGenerated]
    protected virtual bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      return true;
    }

    // Token: 0x06001110 RID: 4368 RVA: 0x003990D4 File Offset: 0x003990D4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ChatRow left, ChatRow right)
    {
      return !(left == right);
    }

    // Token: 0x06001111 RID: 4369 RVA: 0x003990E0 File Offset: 0x003990E0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ChatRow left, ChatRow right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001112 RID: 4370 RVA: 0x003990F4 File Offset: 0x003990F4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
    }

    // Token: 0x06001113 RID: 4371 RVA: 0x00399108 File Offset: 0x00399108
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ChatRow);
    }

    // Token: 0x06001114 RID: 4372 RVA: 0x00399118 File Offset: 0x00399118
    [NullableContext(2)]
    [CompilerGenerated]
    public virtual bool Equals(ChatRow other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract);
    }

    // Token: 0x06001116 RID: 4374 RVA: 0x00399138 File Offset: 0x00399138
    [CompilerGenerated]
    protected ChatRow(ChatRow original)
    {
    }

    // Token: 0x06001117 RID: 4375 RVA: 0x00399140 File Offset: 0x00399140
    protected ChatRow()
    {
    }

    // Token: 0x02000432 RID: 1074
    [Nullable(0)]
    public sealed class Separator : ChatRow, IEquatable<ChatRow.Separator>
    {
      // Token: 0x06002D2B RID: 11563 RVA: 0x0042D4C4 File Offset: 0x0042D4C4
      public Separator(DateTimeOffset Date)
      {
        this.Date = Date;
        base..ctor();
      }

      // Token: 0x17000845 RID: 2117
      // (get) Token: 0x06002D2C RID: 11564 RVA: 0x0042D4D4 File Offset: 0x0042D4D4
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ChatRow.Separator);
        }
      }

      // Token: 0x17000846 RID: 2118
      // (get) Token: 0x06002D2D RID: 11565 RVA: 0x0042D4E0 File Offset: 0x0042D4E0
      // (set) Token: 0x06002D2E RID: 11566 RVA: 0x0042D4E8 File Offset: 0x0042D4E8
      public DateTimeOffset Date { get; set; }

      // Token: 0x17000847 RID: 2119
      // (get) Token: 0x06002D2F RID: 11567 RVA: 0x0042D4F4 File Offset: 0x0042D4F4
      public override string Id
      {
        get
        {
          return "separator-" + this.Date.ToUnixTimeMilliseconds().ToString();
        }
      }

      // Token: 0x06002D30 RID: 11568 RVA: 0x0042D524 File Offset: 0x0042D524
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Separator");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002D31 RID: 11569 RVA: 0x0042D570 File Offset: 0x0042D570
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Date = ");
        builder.Append(this.Date.ToString());
        return true;
      }

      // Token: 0x06002D32 RID: 11570 RVA: 0x0042D5BC File Offset: 0x0042D5BC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ChatRow.Separator left, ChatRow.Separator right)
      {
        return !(left == right);
      }

      // Token: 0x06002D33 RID: 11571 RVA: 0x0042D5C8 File Offset: 0x0042D5C8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ChatRow.Separator left, ChatRow.Separator right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002D34 RID: 11572 RVA: 0x0042D5DC File Offset: 0x0042D5DC
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<Date>k__BackingField);
      }

      // Token: 0x06002D35 RID: 11573 RVA: 0x0042D5FC File Offset: 0x0042D5FC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ChatRow.Separator);
      }

      // Token: 0x06002D36 RID: 11574 RVA: 0x0042D60C File Offset: 0x0042D60C
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ChatRow other)
      {
        return this.Equals(other);
      }

      // Token: 0x06002D37 RID: 11575 RVA: 0x0042D618 File Offset: 0x0042D618
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ChatRow.Separator other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<Date>k__BackingField, other.<Date>k__BackingField));
      }

      // Token: 0x06002D39 RID: 11577 RVA: 0x0042D64C File Offset: 0x0042D64C
      [CompilerGenerated]
      private Separator(ChatRow.Separator original)
        : base(original)
      {
        this.Date = original.<Date>k__BackingField;
      }

      // Token: 0x06002D3A RID: 11578 RVA: 0x0042D664 File Offset: 0x0042D664
      [CompilerGenerated]
      public void Deconstruct(out DateTimeOffset Date)
      {
        Date = this.Date;
      }
    }

    // Token: 0x02000433 RID: 1075
    [Nullable(0)]
    public sealed class Message : ChatRow, IEquatable<ChatRow.Message>
    {
      // Token: 0x06002D3B RID: 11579 RVA: 0x0042D674 File Offset: 0x0042D674
      public Message(ConversationMessage Value, bool IsLastOfRun, bool IsFirstOfRun)
      {
        this.Value = Value;
        this.IsLastOfRun = IsLastOfRun;
        this.IsFirstOfRun = IsFirstOfRun;
        base..ctor();
      }

      // Token: 0x17000848 RID: 2120
      // (get) Token: 0x06002D3C RID: 11580 RVA: 0x0042D694 File Offset: 0x0042D694
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ChatRow.Message);
        }
      }

      // Token: 0x17000849 RID: 2121
      // (get) Token: 0x06002D3D RID: 11581 RVA: 0x0042D6A0 File Offset: 0x0042D6A0
      // (set) Token: 0x06002D3E RID: 11582 RVA: 0x0042D6A8 File Offset: 0x0042D6A8
      public ConversationMessage Value { get; set; }

      // Token: 0x1700084A RID: 2122
      // (get) Token: 0x06002D3F RID: 11583 RVA: 0x0042D6B4 File Offset: 0x0042D6B4
      // (set) Token: 0x06002D40 RID: 11584 RVA: 0x0042D6BC File Offset: 0x0042D6BC
      public bool IsLastOfRun { get; set; }

      // Token: 0x1700084B RID: 2123
      // (get) Token: 0x06002D41 RID: 11585 RVA: 0x0042D6C8 File Offset: 0x0042D6C8
      // (set) Token: 0x06002D42 RID: 11586 RVA: 0x0042D6D0 File Offset: 0x0042D6D0
      public bool IsFirstOfRun { get; set; }

      // Token: 0x1700084C RID: 2124
      // (get) Token: 0x06002D43 RID: 11587 RVA: 0x0042D6DC File Offset: 0x0042D6DC
      public override string Id
      {
        get
        {
          return this.Value.RowId;
        }
      }

      // Token: 0x06002D44 RID: 11588 RVA: 0x0042D6EC File Offset: 0x0042D6EC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Message");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002D45 RID: 11589 RVA: 0x0042D738 File Offset: 0x0042D738
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Value = ");
        builder.Append(this.Value);
        builder.Append(", IsLastOfRun = ");
        builder.Append(this.IsLastOfRun.ToString());
        builder.Append(", IsFirstOfRun = ");
        builder.Append(this.IsFirstOfRun.ToString());
        return true;
      }

      // Token: 0x06002D46 RID: 11590 RVA: 0x0042D7C4 File Offset: 0x0042D7C4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ChatRow.Message left, ChatRow.Message right)
      {
        return !(left == right);
      }

      // Token: 0x06002D47 RID: 11591 RVA: 0x0042D7D0 File Offset: 0x0042D7D0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ChatRow.Message left, ChatRow.Message right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002D48 RID: 11592 RVA: 0x0042D7E4 File Offset: 0x0042D7E4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((base.GetHashCode() * -1521134295 + EqualityComparer<ConversationMessage>.Default.GetHashCode(this.<Value>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsLastOfRun>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsFirstOfRun>k__BackingField);
      }

      // Token: 0x06002D49 RID: 11593 RVA: 0x0042D83C File Offset: 0x0042D83C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ChatRow.Message);
      }

      // Token: 0x06002D4A RID: 11594 RVA: 0x0042D84C File Offset: 0x0042D84C
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ChatRow other)
      {
        return this.Equals(other);
      }

      // Token: 0x06002D4B RID: 11595 RVA: 0x0042D858 File Offset: 0x0042D858
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ChatRow.Message other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<ConversationMessage>.Default.Equals(this.<Value>k__BackingField, other.<Value>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsLastOfRun>k__BackingField, other.<IsLastOfRun>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsFirstOfRun>k__BackingField, other.<IsFirstOfRun>k__BackingField));
      }

      // Token: 0x06002D4D RID: 11597 RVA: 0x0042D8C4 File Offset: 0x0042D8C4
      [CompilerGenerated]
      private Message(ChatRow.Message original)
        : base(original)
      {
        this.Value = original.<Value>k__BackingField;
        this.IsLastOfRun = original.<IsLastOfRun>k__BackingField;
        this.IsFirstOfRun = original.<IsFirstOfRun>k__BackingField;
      }

      // Token: 0x06002D4E RID: 11598 RVA: 0x0042D8F4 File Offset: 0x0042D8F4
      [CompilerGenerated]
      public void Deconstruct(out ConversationMessage Value, out bool IsLastOfRun, out bool IsFirstOfRun)
      {
        Value = this.Value;
        IsLastOfRun = this.IsLastOfRun;
        IsFirstOfRun = this.IsFirstOfRun;
      }
    }

    // Token: 0x02000434 RID: 1076
    [Nullable(0)]
    public sealed class HandOff : ChatRow, IEquatable<ChatRow.HandOff>
    {
      // Token: 0x06002D4F RID: 11599 RVA: 0x0042D910 File Offset: 0x0042D910
      public HandOff(ChatHandOff Entry)
      {
        this.Entry = Entry;
        base..ctor();
      }

      // Token: 0x1700084D RID: 2125
      // (get) Token: 0x06002D50 RID: 11600 RVA: 0x0042D920 File Offset: 0x0042D920
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ChatRow.HandOff);
        }
      }

      // Token: 0x1700084E RID: 2126
      // (get) Token: 0x06002D51 RID: 11601 RVA: 0x0042D92C File Offset: 0x0042D92C
      // (set) Token: 0x06002D52 RID: 11602 RVA: 0x0042D934 File Offset: 0x0042D934
      public ChatHandOff Entry { get; set; }

      // Token: 0x1700084F RID: 2127
      // (get) Token: 0x06002D53 RID: 11603 RVA: 0x0042D940 File Offset: 0x0042D940
      public override string Id
      {
        get
        {
          return "handoff-" + this.Entry.Id;
        }
      }

      // Token: 0x06002D54 RID: 11604 RVA: 0x0042D958 File Offset: 0x0042D958
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("HandOff");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002D55 RID: 11605 RVA: 0x0042D9A4 File Offset: 0x0042D9A4
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Entry = ");
        builder.Append(this.Entry);
        return true;
      }

      // Token: 0x06002D56 RID: 11606 RVA: 0x0042D9D8 File Offset: 0x0042D9D8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ChatRow.HandOff left, ChatRow.HandOff right)
      {
        return !(left == right);
      }

      // Token: 0x06002D57 RID: 11607 RVA: 0x0042D9E4 File Offset: 0x0042D9E4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ChatRow.HandOff left, ChatRow.HandOff right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002D58 RID: 11608 RVA: 0x0042D9F8 File Offset: 0x0042D9F8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<ChatHandOff>.Default.GetHashCode(this.<Entry>k__BackingField);
      }

      // Token: 0x06002D59 RID: 11609 RVA: 0x0042DA18 File Offset: 0x0042DA18
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ChatRow.HandOff);
      }

      // Token: 0x06002D5A RID: 11610 RVA: 0x0042DA28 File Offset: 0x0042DA28
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ChatRow other)
      {
        return this.Equals(other);
      }

      // Token: 0x06002D5B RID: 11611 RVA: 0x0042DA34 File Offset: 0x0042DA34
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ChatRow.HandOff other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<ChatHandOff>.Default.Equals(this.<Entry>k__BackingField, other.<Entry>k__BackingField));
      }

      // Token: 0x06002D5D RID: 11613 RVA: 0x0042DA68 File Offset: 0x0042DA68
      [CompilerGenerated]
      private HandOff(ChatRow.HandOff original)
        : base(original)
      {
        this.Entry = original.<Entry>k__BackingField;
      }

      // Token: 0x06002D5E RID: 11614 RVA: 0x0042DA80 File Offset: 0x0042DA80
      [CompilerGenerated]
      public void Deconstruct(out ChatHandOff Entry)
      {
        Entry = this.Entry;
      }
    }

    // Token: 0x02000435 RID: 1077
    [NullableContext(2)]
    [Nullable(0)]
    public sealed class Typing : ChatRow, IEquatable<ChatRow.Typing>
    {
      // Token: 0x06002D5F RID: 11615 RVA: 0x0042DA8C File Offset: 0x0042DA8C
      public Typing(string StatusText)
      {
        this.StatusText = StatusText;
        base..ctor();
      }

      // Token: 0x17000850 RID: 2128
      // (get) Token: 0x06002D60 RID: 11616 RVA: 0x0042DA9C File Offset: 0x0042DA9C
      [Nullable(1)]
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [NullableContext(1)]
        [CompilerGenerated]
        get
        {
          return typeof(ChatRow.Typing);
        }
      }

      // Token: 0x17000851 RID: 2129
      // (get) Token: 0x06002D61 RID: 11617 RVA: 0x0042DAA8 File Offset: 0x0042DAA8
      // (set) Token: 0x06002D62 RID: 11618 RVA: 0x0042DAB0 File Offset: 0x0042DAB0
      public string StatusText { get; set; }

      // Token: 0x17000852 RID: 2130
      // (get) Token: 0x06002D63 RID: 11619 RVA: 0x0042DABC File Offset: 0x0042DABC
      [Nullable(1)]
      public override string Id
      {
        [NullableContext(1)]
        get
        {
          return "typing";
        }
      }

      // Token: 0x06002D64 RID: 11620 RVA: 0x0042DAC4 File Offset: 0x0042DAC4
      [NullableContext(1)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Typing");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002D65 RID: 11621 RVA: 0x0042DB10 File Offset: 0x0042DB10
      [NullableContext(1)]
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("StatusText = ");
        builder.Append(this.StatusText);
        return true;
      }

      // Token: 0x06002D66 RID: 11622 RVA: 0x0042DB44 File Offset: 0x0042DB44
      [CompilerGenerated]
      public static bool operator !=(ChatRow.Typing left, ChatRow.Typing right)
      {
        return !(left == right);
      }

      // Token: 0x06002D67 RID: 11623 RVA: 0x0042DB50 File Offset: 0x0042DB50
      [CompilerGenerated]
      public static bool operator ==(ChatRow.Typing left, ChatRow.Typing right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002D68 RID: 11624 RVA: 0x0042DB64 File Offset: 0x0042DB64
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<StatusText>k__BackingField);
      }

      // Token: 0x06002D69 RID: 11625 RVA: 0x0042DB84 File Offset: 0x0042DB84
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ChatRow.Typing);
      }

      // Token: 0x06002D6A RID: 11626 RVA: 0x0042DB94 File Offset: 0x0042DB94
      [CompilerGenerated]
      public sealed override bool Equals(ChatRow other)
      {
        return this.Equals(other);
      }

      // Token: 0x06002D6B RID: 11627 RVA: 0x0042DBA0 File Offset: 0x0042DBA0
      [CompilerGenerated]
      public bool Equals(ChatRow.Typing other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<StatusText>k__BackingField, other.<StatusText>k__BackingField));
      }

      // Token: 0x06002D6D RID: 11629 RVA: 0x0042DBD4 File Offset: 0x0042DBD4
      [CompilerGenerated]
      private Typing([Nullable(1)] ChatRow.Typing original)
        : base(original)
      {
        this.StatusText = original.<StatusText>k__BackingField;
      }

      // Token: 0x06002D6E RID: 11630 RVA: 0x0042DBEC File Offset: 0x0042DBEC
      [CompilerGenerated]
      public void Deconstruct(out string StatusText)
      {
        StatusText = this.StatusText;
      }
    }

    // Token: 0x02000436 RID: 1078
    [Nullable(0)]
    public sealed class SentStatus : ChatRow, IEquatable<ChatRow.SentStatus>
    {
      // Token: 0x06002D6F RID: 11631 RVA: 0x0042DBF8 File Offset: 0x0042DBF8
      public SentStatus(string Text, string Key = "sent-status")
      {
        this.Text = Text;
        this.Key = Key;
        base..ctor();
      }

      // Token: 0x17000853 RID: 2131
      // (get) Token: 0x06002D70 RID: 11632 RVA: 0x0042DC10 File Offset: 0x0042DC10
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ChatRow.SentStatus);
        }
      }

      // Token: 0x17000854 RID: 2132
      // (get) Token: 0x06002D71 RID: 11633 RVA: 0x0042DC1C File Offset: 0x0042DC1C
      // (set) Token: 0x06002D72 RID: 11634 RVA: 0x0042DC24 File Offset: 0x0042DC24
      public string Text { get; set; }

      // Token: 0x17000855 RID: 2133
      // (get) Token: 0x06002D73 RID: 11635 RVA: 0x0042DC30 File Offset: 0x0042DC30
      // (set) Token: 0x06002D74 RID: 11636 RVA: 0x0042DC38 File Offset: 0x0042DC38
      public string Key { get; set; }

      // Token: 0x17000856 RID: 2134
      // (get) Token: 0x06002D75 RID: 11637 RVA: 0x0042DC44 File Offset: 0x0042DC44
      public override string Id
      {
        get
        {
          return this.Key;
        }
      }

      // Token: 0x06002D76 RID: 11638 RVA: 0x0042DC4C File Offset: 0x0042DC4C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("SentStatus");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002D77 RID: 11639 RVA: 0x0042DC98 File Offset: 0x0042DC98
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Text = ");
        builder.Append(this.Text);
        builder.Append(", Key = ");
        builder.Append(this.Key);
        return true;
      }

      // Token: 0x06002D78 RID: 11640 RVA: 0x0042DCF0 File Offset: 0x0042DCF0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ChatRow.SentStatus left, ChatRow.SentStatus right)
      {
        return !(left == right);
      }

      // Token: 0x06002D79 RID: 11641 RVA: 0x0042DCFC File Offset: 0x0042DCFC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ChatRow.SentStatus left, ChatRow.SentStatus right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002D7A RID: 11642 RVA: 0x0042DD10 File Offset: 0x0042DD10
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Key>k__BackingField);
      }

      // Token: 0x06002D7B RID: 11643 RVA: 0x0042DD48 File Offset: 0x0042DD48
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ChatRow.SentStatus);
      }

      // Token: 0x06002D7C RID: 11644 RVA: 0x0042DD58 File Offset: 0x0042DD58
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ChatRow other)
      {
        return this.Equals(other);
      }

      // Token: 0x06002D7D RID: 11645 RVA: 0x0042DD64 File Offset: 0x0042DD64
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ChatRow.SentStatus other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Key>k__BackingField, other.<Key>k__BackingField));
      }

      // Token: 0x06002D7F RID: 11647 RVA: 0x0042DDB8 File Offset: 0x0042DDB8
      [CompilerGenerated]
      private SentStatus(ChatRow.SentStatus original)
        : base(original)
      {
        this.Text = original.<Text>k__BackingField;
        this.Key = original.<Key>k__BackingField;
      }

      // Token: 0x06002D80 RID: 11648 RVA: 0x0042DDDC File Offset: 0x0042DDDC
      [CompilerGenerated]
      public void Deconstruct(out string Text, out string Key)
      {
        Text = this.Text;
        Key = this.Key;
      }
    }

    // Token: 0x02000437 RID: 1079
    [Nullable(0)]
    public sealed class LiveWork : ChatRow, IEquatable<ChatRow.LiveWork>
    {
      // Token: 0x06002D81 RID: 11649 RVA: 0x0042DDF0 File Offset: 0x0042DDF0
      public LiveWork(ChatHandOff Entry, string Key)
      {
        this.Entry = Entry;
        this.Key = Key;
        base..ctor();
      }

      // Token: 0x17000857 RID: 2135
      // (get) Token: 0x06002D82 RID: 11650 RVA: 0x0042DE08 File Offset: 0x0042DE08
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ChatRow.LiveWork);
        }
      }

      // Token: 0x17000858 RID: 2136
      // (get) Token: 0x06002D83 RID: 11651 RVA: 0x0042DE14 File Offset: 0x0042DE14
      // (set) Token: 0x06002D84 RID: 11652 RVA: 0x0042DE1C File Offset: 0x0042DE1C
      public ChatHandOff Entry { get; set; }

      // Token: 0x17000859 RID: 2137
      // (get) Token: 0x06002D85 RID: 11653 RVA: 0x0042DE28 File Offset: 0x0042DE28
      // (set) Token: 0x06002D86 RID: 11654 RVA: 0x0042DE30 File Offset: 0x0042DE30
      public string Key { get; set; }

      // Token: 0x1700085A RID: 2138
      // (get) Token: 0x06002D87 RID: 11655 RVA: 0x0042DE3C File Offset: 0x0042DE3C
      public override string Id
      {
        get
        {
          return this.Key;
        }
      }

      // Token: 0x06002D88 RID: 11656 RVA: 0x0042DE44 File Offset: 0x0042DE44
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("LiveWork");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002D89 RID: 11657 RVA: 0x0042DE90 File Offset: 0x0042DE90
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Entry = ");
        builder.Append(this.Entry);
        builder.Append(", Key = ");
        builder.Append(this.Key);
        return true;
      }

      // Token: 0x06002D8A RID: 11658 RVA: 0x0042DEE8 File Offset: 0x0042DEE8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ChatRow.LiveWork left, ChatRow.LiveWork right)
      {
        return !(left == right);
      }

      // Token: 0x06002D8B RID: 11659 RVA: 0x0042DEF4 File Offset: 0x0042DEF4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ChatRow.LiveWork left, ChatRow.LiveWork right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002D8C RID: 11660 RVA: 0x0042DF08 File Offset: 0x0042DF08
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<ChatHandOff>.Default.GetHashCode(this.<Entry>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Key>k__BackingField);
      }

      // Token: 0x06002D8D RID: 11661 RVA: 0x0042DF40 File Offset: 0x0042DF40
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ChatRow.LiveWork);
      }

      // Token: 0x06002D8E RID: 11662 RVA: 0x0042DF50 File Offset: 0x0042DF50
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ChatRow other)
      {
        return this.Equals(other);
      }

      // Token: 0x06002D8F RID: 11663 RVA: 0x0042DF5C File Offset: 0x0042DF5C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ChatRow.LiveWork other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<ChatHandOff>.Default.Equals(this.<Entry>k__BackingField, other.<Entry>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Key>k__BackingField, other.<Key>k__BackingField));
      }

      // Token: 0x06002D91 RID: 11665 RVA: 0x0042DFB0 File Offset: 0x0042DFB0
      [CompilerGenerated]
      private LiveWork(ChatRow.LiveWork original)
        : base(original)
      {
        this.Entry = original.<Entry>k__BackingField;
        this.Key = original.<Key>k__BackingField;
      }

      // Token: 0x06002D92 RID: 11666 RVA: 0x0042DFD4 File Offset: 0x0042DFD4
      [CompilerGenerated]
      public void Deconstruct(out ChatHandOff Entry, out string Key)
      {
        Entry = this.Entry;
        Key = this.Key;
      }
    }
  }
}
