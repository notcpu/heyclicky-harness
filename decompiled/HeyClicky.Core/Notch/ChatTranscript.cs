using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Notch
{
  // Token: 0x02000104 RID: 260
  [NullableContext(1)]
  [Nullable(0)]
  public static class ChatTranscript
  {
    // Token: 0x06001123 RID: 4387 RVA: 0x00399218 File Offset: 0x00399218
    [return: Nullable(2)]
    public static ChatLiveReply LiveReply(bool rendersInChat, bool inProgress, string rawText, [Nullable(2)] string customStatus, string liveReplyId, DateTimeOffset now)
    {
      if (!rendersInChat || !inProgress)
      {
        return null;
      }
      List<string> list = ConversationMessage.BubbleSegmentsFrom(rawText);
      if (!rawText.EndsWith("\n\n", StringComparison.Ordinal) && list.Count > 0)
      {
        list.RemoveAt(list.Count - 1);
      }
      if (list.Count == 0)
      {
        string text = ((customStatus != null) ? customStatus.Trim() : null);
        return new ChatLiveReply.Thinking(string.IsNullOrEmpty(text) ? null : text);
      }
      return new ChatLiveReply.Streaming(new ConversationMessage
      {
        Id = liveReplyId,
        Role = "assistant",
        Content = string.Join(" ", list),
        Source = "text",
        CreatedAt = now,
        BubbleSegments = list
      });
    }

    // Token: 0x06001124 RID: 4388 RVA: 0x003992C8 File Offset: 0x003992C8
    private static List<ChatTranscript.TimelineItem> Timeline(IReadOnlyList<ConversationMessage> messages, IReadOnlyList<ChatHandOff> handOffs)
    {
      ChatTranscript.<>c__DisplayClass5_0 CS$<>8__locals1;
      CS$<>8__locals1.insertionsAfterIndex = new Dictionary<int, List<ChatHandOff>>();
      List<ChatHandOff> list = new List<ChatHandOff>();
      List<ConversationMessage> list2 = messages.Where<ConversationMessage>((ConversationMessage m) => !m.IsPending).ToList<ConversationMessage>();
      using (IEnumerator<ChatHandOff> enumerator = handOffs.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ChatHandOff entry = enumerator.Current;
          int num = list2.FindIndex((ConversationMessage m) => m.CreatedAt >= entry.CreatedAt.AddSeconds(-1.0));
          if (num < 0)
          {
            list.Add(entry);
          }
          else
          {
            ChatHandOffKind kind = entry.Kind;
            if (kind == ChatHandOffKind.Started || kind == ChatHandOffKind.Text)
            {
              int num2 = num;
              if (list2[num2].IsUser && num2 + 1 < list2.Count && !list2[num2 + 1].IsUser)
              {
                num2++;
              }
              while (num2 + 1 < list2.Count && !list2[num2 + 1].IsUser && (list2[num2 + 1].CreatedAt - list2[num2].CreatedAt).TotalSeconds < 5.0)
              {
                num2++;
              }
              ChatTranscript.<Timeline>g__Insert|5_1(num2, entry, ref CS$<>8__locals1);
            }
            else
            {
              ChatTranscript.<Timeline>g__Insert|5_1((num == 0) ? (-1) : (num - 1), entry, ref CS$<>8__locals1);
            }
          }
        }
      }
      List<ChatTranscript.TimelineItem> list3 = new List<ChatTranscript.TimelineItem>();
      List<ChatHandOff> list4;
      if (CS$<>8__locals1.insertionsAfterIndex.TryGetValue(-1, out list4))
      {
        list3.AddRange(list4.Select<ChatHandOff, ChatTranscript.TimelineItem.Hand>((ChatHandOff e) => new ChatTranscript.TimelineItem.Hand(e)));
      }
      for (int i = 0; i < list2.Count; i++)
      {
        list3.Add(new ChatTranscript.TimelineItem.Msg(list2[i]));
        List<ChatHandOff> list5;
        if (CS$<>8__locals1.insertionsAfterIndex.TryGetValue(i, out list5))
        {
          list3.AddRange(list5.Select<ChatHandOff, ChatTranscript.TimelineItem.Hand>((ChatHandOff e) => new ChatTranscript.TimelineItem.Hand(e)));
        }
      }
      List<ChatTranscript.TimelineItem> list6 = (from m in messages
        where m.IsPending
        select new ChatTranscript.TimelineItem.Msg(m)).Concat<ChatTranscript.TimelineItem>(list.Select<ChatHandOff, ChatTranscript.TimelineItem.Hand>((ChatHandOff e) => new ChatTranscript.TimelineItem.Hand(e))).ToList<ChatTranscript.TimelineItem>();
      list3.AddRange(from p in list6.Select<ChatTranscript.TimelineItem, ValueTuple<ChatTranscript.TimelineItem, int>>((ChatTranscript.TimelineItem item, int order) => new ValueTuple<ChatTranscript.TimelineItem, int>(item, order))
        orderby p.Item1.Date, p.Item2
        select p.Item1);
      return list3;
    }

    // Token: 0x06001125 RID: 4389 RVA: 0x00399628 File Offset: 0x00399628
    public static List<ChatRow> Rows(IReadOnlyList<ConversationMessage> messages, IReadOnlyList<ChatHandOff> handOffs, [Nullable(2)] ChatLiveReply liveReply, DateTimeOffset? lastAnswerEndedAt, DateTimeOffset now)
    {
      List<ConversationMessage> list = messages.Where<ConversationMessage>(delegate(ConversationMessage m)
      {
        if (m.BubbleText.Length <= 0 && m.ActionCaption == null)
        {
          IReadOnlyList<string> attachmentPaths = m.AttachmentPaths;
          return attachmentPaths != null && attachmentPaths.Count > 0;
        }
        return true;
      }).ToList<ConversationMessage>();
      ChatLiveReply.Streaming streaming = liveReply as ChatLiveReply.Streaming;
      if (streaming != null)
      {
        list.Add(streaming.Message);
      }
      List<ChatTranscript.TimelineItem> list2 = ChatTranscript.Timeline(list, handOffs.Where<ChatHandOff>((ChatHandOff h) => !h.IsSentFromWindow).ToList<ChatHandOff>());
      List<ChatRow> list3 = new List<ChatRow>();
      DateTimeOffset? dateTimeOffset = null;
      int i = 0;
      while (i < list2.Count)
      {
        ChatTranscript.TimelineItem timelineItem = list2[i];
        bool flag;
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
          flag = timelineItem.Date - valueOrDefault > ChatTranscript.SeparatorGap;
        }
        else
        {
          flag = true;
        }
        bool flag2 = flag;
        if (flag2)
        {
          list3.Add(new ChatRow.Separator(timelineItem.Date));
        }
        if (dateTimeOffset == null)
        {
          goto IL_00FB;
        }
        DateTimeOffset valueOrDefault2 = dateTimeOffset.GetValueOrDefault();
        if (!(valueOrDefault2 > timelineItem.Date))
        {
          goto IL_00FB;
        }
        DateTimeOffset dateTimeOffset2 = valueOrDefault2;
        IL_0106:
        dateTimeOffset = new DateTimeOffset?(dateTimeOffset2);
        ChatTranscript.TimelineItem.Hand hand = timelineItem as ChatTranscript.TimelineItem.Hand;
        if (hand == null)
        {
          ChatTranscript.TimelineItem.Msg msg = timelineItem as ChatTranscript.TimelineItem.Msg;
          if (msg != null)
          {
            ConversationMessage value = msg.Value;
            if (i + 1 >= list2.Count)
            {
              goto IL_016B;
            }
            ChatTranscript.TimelineItem.Msg msg2 = list2[i + 1] as ChatTranscript.TimelineItem.Msg;
            if (msg2 == null)
            {
              goto IL_016B;
            }
            ConversationMessage conversationMessage = msg2.Value;
            IL_0175:
            ConversationMessage conversationMessage2 = conversationMessage;
            if (i <= 0)
            {
              goto IL_0191;
            }
            ChatTranscript.TimelineItem.Msg msg3 = list2[i - 1] as ChatTranscript.TimelineItem.Msg;
            if (msg3 == null)
            {
              goto IL_0191;
            }
            ConversationMessage conversationMessage3 = msg3.Value;
            IL_019B:
            ConversationMessage conversationMessage4 = conversationMessage3;
            bool flag3 = conversationMessage2 == null || conversationMessage2.CreatedAt - value.CreatedAt > ChatTranscript.SeparatorGap;
            bool flag4 = conversationMessage2 == null || conversationMessage2.IsUser != value.IsUser || flag3;
            bool flag5 = conversationMessage4 == null || conversationMessage4.IsUser != value.IsUser || flag2;
            list3.Add(new ChatRow.Message(value, flag4, flag5));
            goto IL_0212;
            IL_0191:
            conversationMessage3 = null;
            goto IL_019B;
            IL_016B:
            conversationMessage = null;
            goto IL_0175;
          }
        }
        else
        {
          list3.Add(new ChatRow.HandOff(hand.Entry));
        }
        IL_0212:
        i++;
        continue;
        IL_00FB:
        dateTimeOffset2 = timelineItem.Date;
        goto IL_0106;
      }
      ChatLiveReply.Thinking thinking = liveReply as ChatLiveReply.Thinking;
      if (thinking != null)
      {
        list3.Add(new ChatRow.Typing(thinking.Status));
      }
      else if (liveReply is ChatLiveReply.Streaming)
      {
        list3.Add(new ChatRow.Typing(null));
      }
      else if (liveReply == null)
      {
        ConversationMessage conversationMessage5 = list.LastOrDefault<ConversationMessage>();
        if (conversationMessage5 != null && conversationMessage5.IsPending && conversationMessage5.IsUser && now - conversationMessage5.CreatedAt < ChatTranscript.TypingLifetime)
        {
          if (lastAnswerEndedAt != null)
          {
            DateTimeOffset valueOrDefault3 = lastAnswerEndedAt.GetValueOrDefault();
            if (!(conversationMessage5.CreatedAt > valueOrDefault3))
            {
              goto IL_02C4;
            }
          }
          list3.Add(new ChatRow.Typing(null));
        }
      }
      IL_02C4:
      ChatTranscript.InsertSentStatus(list3, now, handOffs);
      return list3;
    }

    // Token: 0x06001126 RID: 4390 RVA: 0x00399904 File Offset: 0x00399904
    private static void InsertSentStatus(List<ChatRow> rows, DateTimeOffset now, IReadOnlyList<ChatHandOff> handOffs)
    {
      for (int i = rows.Count - 1; i >= 0; i--)
      {
        ChatRow chatRow = rows[i];
        ChatRow.Message sentTo = chatRow as ChatRow.Message;
        if (sentTo != null)
        {
          ConversationMessage value = sentTo.Value;
          if (value != null)
          {
            string sentToAgentName = value.SentToAgentName;
            if (sentToAgentName != null)
            {
              ChatHandOff start = (from h in handOffs
                where h.IsSentFromWindow && (h.CreatedAt - sentTo.Value.CreatedAt).Duration() < ChatTranscript.SentFromWindowMatch
                orderby (h.CreatedAt - sentTo.Value.CreatedAt).Duration()
                select h).FirstOrDefault<ChatHandOff>();
              if (start != null)
              {
                ChatHandOff chatHandOff = handOffs.LastOrDefault<ChatHandOff>((ChatHandOff h) => h.TargetKey == start.TargetKey);
                if (((chatHandOff != null) ? chatHandOff.Id : null) == start.Id)
                {
                  rows.Insert(i + 1, new ChatRow.LiveWork(start, "sent-to-live-" + sentTo.Value.RowId));
                }
              }
              rows.Insert(i + 1, new ChatRow.SentStatus("Sent to " + sentToAgentName, "sent-to-" + sentTo.Value.RowId));
            }
          }
        }
      }
      int num = rows.FindLastIndex(delegate(ChatRow r)
      {
        ChatRow.Message message2 = r as ChatRow.Message;
        if (message2 != null)
        {
          ConversationMessage value2 = message2.Value;
          if (value2 != null)
          {
            return value2.IsUser;
          }
        }
        return false;
      });
      if (num >= 0)
      {
        ChatRow.Message message = rows[num] as ChatRow.Message;
        if (message != null && message.Value.SentToAgentName == null && !(now - message.Value.CreatedAt >= TimeSpan.FromDays(1)))
        {
          bool flag = rows.Skip<ChatRow>(num + 1).Any<ChatRow>(delegate(ChatRow r)
          {
            ChatRow.Message message3 = r as ChatRow.Message;
            bool flag2;
            if (message3 == null)
            {
              flag2 = r is ChatRow.Typing || r is ChatRow.HandOff;
            }
            else
            {
              flag2 = !message3.Value.IsUser;
            }
            return flag2;
          });
          rows.Insert(num + 1, new ChatRow.SentStatus(flag ? "Read" : "Delivered", "sent-status"));
          return;
        }
      }
    }

    // Token: 0x06001128 RID: 4392 RVA: 0x00399B30 File Offset: 0x00399B30
    [CompilerGenerated]
    internal static void <Timeline>g__Insert|5_1(int at, ChatHandOff entry, ref ChatTranscript.<>c__DisplayClass5_0 A_2)
    {
      List<ChatHandOff> list;
      if (!A_2.insertionsAfterIndex.TryGetValue(at, out list))
      {
        list = (A_2.insertionsAfterIndex[at] = new List<ChatHandOff>());
      }
      list.Add(entry);
    }

    // Token: 0x0400076E RID: 1902
    public static readonly TimeSpan SeparatorGap = TimeSpan.FromHours(1);

    // Token: 0x0400076F RID: 1903
    public static readonly TimeSpan TypingLifetime = TimeSpan.FromSeconds(90L);

    // Token: 0x04000770 RID: 1904
    public const int RowPageSize = 40;

    // Token: 0x04000771 RID: 1905
    private static readonly TimeSpan SentFromWindowMatch = TimeSpan.FromSeconds(5L);

    // Token: 0x0200043A RID: 1082
    [Nullable(0)]
    private abstract class TimelineItem : IEquatable<ChatTranscript.TimelineItem>
    {
      // Token: 0x1700085F RID: 2143
      // (get) Token: 0x06002DB1 RID: 11697 RVA: 0x0042E2B0 File Offset: 0x0042E2B0
      [CompilerGenerated]
      protected virtual Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ChatTranscript.TimelineItem);
        }
      }

      // Token: 0x17000860 RID: 2144
      // (get) Token: 0x06002DB2 RID: 11698
      public abstract DateTimeOffset Date { get; }

      // Token: 0x06002DB3 RID: 11699 RVA: 0x0042E2BC File Offset: 0x0042E2BC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("TimelineItem");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002DB4 RID: 11700 RVA: 0x0042E308 File Offset: 0x0042E308
      [CompilerGenerated]
      protected virtual bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Date = ");
        builder.Append(this.Date.ToString());
        return true;
      }

      // Token: 0x06002DB5 RID: 11701 RVA: 0x0042E344 File Offset: 0x0042E344
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ChatTranscript.TimelineItem left, ChatTranscript.TimelineItem right)
      {
        return !(left == right);
      }

      // Token: 0x06002DB6 RID: 11702 RVA: 0x0042E350 File Offset: 0x0042E350
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ChatTranscript.TimelineItem left, ChatTranscript.TimelineItem right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002DB7 RID: 11703 RVA: 0x0042E364 File Offset: 0x0042E364
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
      }

      // Token: 0x06002DB8 RID: 11704 RVA: 0x0042E378 File Offset: 0x0042E378
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ChatTranscript.TimelineItem);
      }

      // Token: 0x06002DB9 RID: 11705 RVA: 0x0042E388 File Offset: 0x0042E388
      [NullableContext(2)]
      [CompilerGenerated]
      public virtual bool Equals(ChatTranscript.TimelineItem other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract);
      }

      // Token: 0x06002DBB RID: 11707 RVA: 0x0042E3A8 File Offset: 0x0042E3A8
      [CompilerGenerated]
      protected TimelineItem(ChatTranscript.TimelineItem original)
      {
      }

      // Token: 0x06002DBC RID: 11708 RVA: 0x0042E3B0 File Offset: 0x0042E3B0
      protected TimelineItem()
      {
      }

      // Token: 0x020006E4 RID: 1764
      [Nullable(0)]
      public sealed class Msg : ChatTranscript.TimelineItem, IEquatable<ChatTranscript.TimelineItem.Msg>
      {
        // Token: 0x06003B11 RID: 15121 RVA: 0x00464564 File Offset: 0x00464564
        public Msg(ConversationMessage Value)
        {
          this.Value = Value;
          base..ctor();
        }

        // Token: 0x170009F7 RID: 2551
        // (get) Token: 0x06003B12 RID: 15122 RVA: 0x00464574 File Offset: 0x00464574
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ChatTranscript.TimelineItem.Msg);
          }
        }

        // Token: 0x170009F8 RID: 2552
        // (get) Token: 0x06003B13 RID: 15123 RVA: 0x00464580 File Offset: 0x00464580
        // (set) Token: 0x06003B14 RID: 15124 RVA: 0x00464588 File Offset: 0x00464588
        public ConversationMessage Value { get; set; }

        // Token: 0x170009F9 RID: 2553
        // (get) Token: 0x06003B15 RID: 15125 RVA: 0x00464594 File Offset: 0x00464594
        public override DateTimeOffset Date
        {
          get
          {
            return this.Value.CreatedAt;
          }
        }

        // Token: 0x06003B16 RID: 15126 RVA: 0x004645A4 File Offset: 0x004645A4
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Msg");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003B17 RID: 15127 RVA: 0x004645F0 File Offset: 0x004645F0
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Value = ");
          builder.Append(this.Value);
          return true;
        }

        // Token: 0x06003B18 RID: 15128 RVA: 0x00464624 File Offset: 0x00464624
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ChatTranscript.TimelineItem.Msg left, ChatTranscript.TimelineItem.Msg right)
        {
          return !(left == right);
        }

        // Token: 0x06003B19 RID: 15129 RVA: 0x00464630 File Offset: 0x00464630
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ChatTranscript.TimelineItem.Msg left, ChatTranscript.TimelineItem.Msg right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003B1A RID: 15130 RVA: 0x00464644 File Offset: 0x00464644
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<ConversationMessage>.Default.GetHashCode(this.<Value>k__BackingField);
        }

        // Token: 0x06003B1B RID: 15131 RVA: 0x00464664 File Offset: 0x00464664
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ChatTranscript.TimelineItem.Msg);
        }

        // Token: 0x06003B1C RID: 15132 RVA: 0x00464674 File Offset: 0x00464674
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ChatTranscript.TimelineItem other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003B1D RID: 15133 RVA: 0x00464680 File Offset: 0x00464680
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ChatTranscript.TimelineItem.Msg other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<ConversationMessage>.Default.Equals(this.<Value>k__BackingField, other.<Value>k__BackingField));
        }

        // Token: 0x06003B1F RID: 15135 RVA: 0x004646B4 File Offset: 0x004646B4
        [CompilerGenerated]
        private Msg(ChatTranscript.TimelineItem.Msg original)
          : base(original)
        {
          this.Value = original.<Value>k__BackingField;
        }

        // Token: 0x06003B20 RID: 15136 RVA: 0x004646CC File Offset: 0x004646CC
        [CompilerGenerated]
        public void Deconstruct(out ConversationMessage Value)
        {
          Value = this.Value;
        }
      }

      // Token: 0x020006E5 RID: 1765
      [Nullable(0)]
      public sealed class Hand : ChatTranscript.TimelineItem, IEquatable<ChatTranscript.TimelineItem.Hand>
      {
        // Token: 0x06003B21 RID: 15137 RVA: 0x004646D8 File Offset: 0x004646D8
        public Hand(ChatHandOff Entry)
        {
          this.Entry = Entry;
          base..ctor();
        }

        // Token: 0x170009FA RID: 2554
        // (get) Token: 0x06003B22 RID: 15138 RVA: 0x004646E8 File Offset: 0x004646E8
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ChatTranscript.TimelineItem.Hand);
          }
        }

        // Token: 0x170009FB RID: 2555
        // (get) Token: 0x06003B23 RID: 15139 RVA: 0x004646F4 File Offset: 0x004646F4
        // (set) Token: 0x06003B24 RID: 15140 RVA: 0x004646FC File Offset: 0x004646FC
        public ChatHandOff Entry { get; set; }

        // Token: 0x170009FC RID: 2556
        // (get) Token: 0x06003B25 RID: 15141 RVA: 0x00464708 File Offset: 0x00464708
        public override DateTimeOffset Date
        {
          get
          {
            return this.Entry.CreatedAt;
          }
        }

        // Token: 0x06003B26 RID: 15142 RVA: 0x00464718 File Offset: 0x00464718
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Hand");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003B27 RID: 15143 RVA: 0x00464764 File Offset: 0x00464764
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

        // Token: 0x06003B28 RID: 15144 RVA: 0x00464798 File Offset: 0x00464798
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ChatTranscript.TimelineItem.Hand left, ChatTranscript.TimelineItem.Hand right)
        {
          return !(left == right);
        }

        // Token: 0x06003B29 RID: 15145 RVA: 0x004647A4 File Offset: 0x004647A4
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ChatTranscript.TimelineItem.Hand left, ChatTranscript.TimelineItem.Hand right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003B2A RID: 15146 RVA: 0x004647B8 File Offset: 0x004647B8
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<ChatHandOff>.Default.GetHashCode(this.<Entry>k__BackingField);
        }

        // Token: 0x06003B2B RID: 15147 RVA: 0x004647D8 File Offset: 0x004647D8
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ChatTranscript.TimelineItem.Hand);
        }

        // Token: 0x06003B2C RID: 15148 RVA: 0x004647E8 File Offset: 0x004647E8
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ChatTranscript.TimelineItem other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003B2D RID: 15149 RVA: 0x004647F4 File Offset: 0x004647F4
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ChatTranscript.TimelineItem.Hand other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<ChatHandOff>.Default.Equals(this.<Entry>k__BackingField, other.<Entry>k__BackingField));
        }

        // Token: 0x06003B2F RID: 15151 RVA: 0x00464828 File Offset: 0x00464828
        [CompilerGenerated]
        private Hand(ChatTranscript.TimelineItem.Hand original)
          : base(original)
        {
          this.Entry = original.<Entry>k__BackingField;
        }

        // Token: 0x06003B30 RID: 15152 RVA: 0x00464840 File Offset: 0x00464840
        [CompilerGenerated]
        public void Deconstruct(out ChatHandOff Entry)
        {
          Entry = this.Entry;
        }
      }
    }
  }
}
