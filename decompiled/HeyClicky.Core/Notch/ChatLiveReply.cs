using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Notch
{
  // Token: 0x02000103 RID: 259
  [NullableContext(1)]
  [Nullable(0)]
  public abstract class ChatLiveReply : IEquatable<ChatLiveReply>
  {
    // Token: 0x17000400 RID: 1024
    // (get) Token: 0x06001118 RID: 4376 RVA: 0x00399148 File Offset: 0x00399148
    [CompilerGenerated]
    protected virtual Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ChatLiveReply);
      }
    }

    // Token: 0x06001119 RID: 4377 RVA: 0x00399154 File Offset: 0x00399154
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ChatLiveReply");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600111A RID: 4378 RVA: 0x003991A0 File Offset: 0x003991A0
    [CompilerGenerated]
    protected virtual bool PrintMembers(StringBuilder builder)
    {
      return false;
    }

    // Token: 0x0600111B RID: 4379 RVA: 0x003991A4 File Offset: 0x003991A4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ChatLiveReply left, ChatLiveReply right)
    {
      return !(left == right);
    }

    // Token: 0x0600111C RID: 4380 RVA: 0x003991B0 File Offset: 0x003991B0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ChatLiveReply left, ChatLiveReply right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600111D RID: 4381 RVA: 0x003991C4 File Offset: 0x003991C4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
    }

    // Token: 0x0600111E RID: 4382 RVA: 0x003991D8 File Offset: 0x003991D8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ChatLiveReply);
    }

    // Token: 0x0600111F RID: 4383 RVA: 0x003991E8 File Offset: 0x003991E8
    [NullableContext(2)]
    [CompilerGenerated]
    public virtual bool Equals(ChatLiveReply other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract);
    }

    // Token: 0x06001121 RID: 4385 RVA: 0x00399208 File Offset: 0x00399208
    [CompilerGenerated]
    protected ChatLiveReply(ChatLiveReply original)
    {
    }

    // Token: 0x06001122 RID: 4386 RVA: 0x00399210 File Offset: 0x00399210
    protected ChatLiveReply()
    {
    }

    // Token: 0x02000438 RID: 1080
    [NullableContext(2)]
    [Nullable(0)]
    public sealed class Thinking : ChatLiveReply, IEquatable<ChatLiveReply.Thinking>
    {
      // Token: 0x06002D93 RID: 11667 RVA: 0x0042DFE8 File Offset: 0x0042DFE8
      public Thinking(string Status)
      {
        this.Status = Status;
        base..ctor();
      }

      // Token: 0x1700085B RID: 2139
      // (get) Token: 0x06002D94 RID: 11668 RVA: 0x0042DFF8 File Offset: 0x0042DFF8
      [Nullable(1)]
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [NullableContext(1)]
        [CompilerGenerated]
        get
        {
          return typeof(ChatLiveReply.Thinking);
        }
      }

      // Token: 0x1700085C RID: 2140
      // (get) Token: 0x06002D95 RID: 11669 RVA: 0x0042E004 File Offset: 0x0042E004
      // (set) Token: 0x06002D96 RID: 11670 RVA: 0x0042E00C File Offset: 0x0042E00C
      public string Status { get; set; }

      // Token: 0x06002D97 RID: 11671 RVA: 0x0042E018 File Offset: 0x0042E018
      [NullableContext(1)]
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

      // Token: 0x06002D98 RID: 11672 RVA: 0x0042E064 File Offset: 0x0042E064
      [NullableContext(1)]
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Status = ");
        builder.Append(this.Status);
        return true;
      }

      // Token: 0x06002D99 RID: 11673 RVA: 0x0042E098 File Offset: 0x0042E098
      [CompilerGenerated]
      public static bool operator !=(ChatLiveReply.Thinking left, ChatLiveReply.Thinking right)
      {
        return !(left == right);
      }

      // Token: 0x06002D9A RID: 11674 RVA: 0x0042E0A4 File Offset: 0x0042E0A4
      [CompilerGenerated]
      public static bool operator ==(ChatLiveReply.Thinking left, ChatLiveReply.Thinking right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002D9B RID: 11675 RVA: 0x0042E0B8 File Offset: 0x0042E0B8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Status>k__BackingField);
      }

      // Token: 0x06002D9C RID: 11676 RVA: 0x0042E0D8 File Offset: 0x0042E0D8
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ChatLiveReply.Thinking);
      }

      // Token: 0x06002D9D RID: 11677 RVA: 0x0042E0E8 File Offset: 0x0042E0E8
      [CompilerGenerated]
      public sealed override bool Equals(ChatLiveReply other)
      {
        return this.Equals(other);
      }

      // Token: 0x06002D9E RID: 11678 RVA: 0x0042E0F4 File Offset: 0x0042E0F4
      [CompilerGenerated]
      public bool Equals(ChatLiveReply.Thinking other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField));
      }

      // Token: 0x06002DA0 RID: 11680 RVA: 0x0042E128 File Offset: 0x0042E128
      [CompilerGenerated]
      private Thinking([Nullable(1)] ChatLiveReply.Thinking original)
        : base(original)
      {
        this.Status = original.<Status>k__BackingField;
      }

      // Token: 0x06002DA1 RID: 11681 RVA: 0x0042E140 File Offset: 0x0042E140
      [CompilerGenerated]
      public void Deconstruct(out string Status)
      {
        Status = this.Status;
      }
    }

    // Token: 0x02000439 RID: 1081
    [Nullable(0)]
    public sealed class Streaming : ChatLiveReply, IEquatable<ChatLiveReply.Streaming>
    {
      // Token: 0x06002DA2 RID: 11682 RVA: 0x0042E14C File Offset: 0x0042E14C
      public Streaming(ConversationMessage Message)
      {
        this.Message = Message;
        base..ctor();
      }

      // Token: 0x1700085D RID: 2141
      // (get) Token: 0x06002DA3 RID: 11683 RVA: 0x0042E15C File Offset: 0x0042E15C
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ChatLiveReply.Streaming);
        }
      }

      // Token: 0x1700085E RID: 2142
      // (get) Token: 0x06002DA4 RID: 11684 RVA: 0x0042E168 File Offset: 0x0042E168
      // (set) Token: 0x06002DA5 RID: 11685 RVA: 0x0042E170 File Offset: 0x0042E170
      public ConversationMessage Message { get; set; }

      // Token: 0x06002DA6 RID: 11686 RVA: 0x0042E17C File Offset: 0x0042E17C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Streaming");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002DA7 RID: 11687 RVA: 0x0042E1C8 File Offset: 0x0042E1C8
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Message = ");
        builder.Append(this.Message);
        return true;
      }

      // Token: 0x06002DA8 RID: 11688 RVA: 0x0042E1FC File Offset: 0x0042E1FC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ChatLiveReply.Streaming left, ChatLiveReply.Streaming right)
      {
        return !(left == right);
      }

      // Token: 0x06002DA9 RID: 11689 RVA: 0x0042E208 File Offset: 0x0042E208
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ChatLiveReply.Streaming left, ChatLiveReply.Streaming right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002DAA RID: 11690 RVA: 0x0042E21C File Offset: 0x0042E21C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<ConversationMessage>.Default.GetHashCode(this.<Message>k__BackingField);
      }

      // Token: 0x06002DAB RID: 11691 RVA: 0x0042E23C File Offset: 0x0042E23C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ChatLiveReply.Streaming);
      }

      // Token: 0x06002DAC RID: 11692 RVA: 0x0042E24C File Offset: 0x0042E24C
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(ChatLiveReply other)
      {
        return this.Equals(other);
      }

      // Token: 0x06002DAD RID: 11693 RVA: 0x0042E258 File Offset: 0x0042E258
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ChatLiveReply.Streaming other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<ConversationMessage>.Default.Equals(this.<Message>k__BackingField, other.<Message>k__BackingField));
      }

      // Token: 0x06002DAF RID: 11695 RVA: 0x0042E28C File Offset: 0x0042E28C
      [CompilerGenerated]
      private Streaming(ChatLiveReply.Streaming original)
        : base(original)
      {
        this.Message = original.<Message>k__BackingField;
      }

      // Token: 0x06002DB0 RID: 11696 RVA: 0x0042E2A4 File Offset: 0x0042E2A4
      [CompilerGenerated]
      public void Deconstruct(out ConversationMessage Message)
      {
        Message = this.Message;
      }
    }
  }
}
