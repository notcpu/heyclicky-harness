using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000120 RID: 288
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ClickyEdgeStackInkReading
  {
    // Token: 0x1700045D RID: 1117
    // (get) Token: 0x060012CD RID: 4813 RVA: 0x003A3620 File Offset: 0x003A3620
    public ClickyEdgeStackInkReading.State CurrentState { get; }

    // Token: 0x1700045E RID: 1118
    // (get) Token: 0x060012CE RID: 4814 RVA: 0x003A3628 File Offset: 0x003A3628
    public ClickyInkLiveNote.Words Title { get; }

    // Token: 0x1700045F RID: 1119
    // (get) Token: 0x060012CF RID: 4815 RVA: 0x003A3630 File Offset: 0x003A3630
    public ClickyInkLiveNote.Block Lead { get; }

    // Token: 0x17000460 RID: 1120
    // (get) Token: 0x060012D0 RID: 4816 RVA: 0x003A3638 File Offset: 0x003A3638
    public ClickyInkLiveNote.Block Support { get; }

    // Token: 0x17000461 RID: 1121
    // (get) Token: 0x060012D1 RID: 4817 RVA: 0x003A3640 File Offset: 0x003A3640
    [Nullable(1)]
    public HashSet<string> ChangedItemIDs
    {
      [NullableContext(1)]
      get;
    } = new HashSet<string>();

    // Token: 0x17000462 RID: 1122
    // (get) Token: 0x060012D2 RID: 4818 RVA: 0x003A3648 File Offset: 0x003A3648
    public string WaitingBlockID { get; }

    // Token: 0x17000463 RID: 1123
    // (get) Token: 0x060012D3 RID: 4819 RVA: 0x003A3650 File Offset: 0x003A3650
    public string WaitingLine { get; }

    // Token: 0x17000464 RID: 1124
    // (get) Token: 0x060012D4 RID: 4820 RVA: 0x003A3658 File Offset: 0x003A3658
    public string PendingBlockID { get; }

    // Token: 0x17000465 RID: 1125
    // (get) Token: 0x060012D5 RID: 4821 RVA: 0x003A3660 File Offset: 0x003A3660
    public string PendingLine { get; }

    // Token: 0x17000466 RID: 1126
    // (get) Token: 0x060012D6 RID: 4822 RVA: 0x003A3668 File Offset: 0x003A3668
    [Nullable(1)]
    public ClickyEdgeStackInkReading.Changes ChangeCounts
    {
      [NullableContext(1)]
      get;
    } = new ClickyEdgeStackInkReading.Changes();

    // Token: 0x17000467 RID: 1127
    // (get) Token: 0x060012D7 RID: 4823 RVA: 0x003A3670 File Offset: 0x003A3670
    public bool TitleChanged { get; }

    // Token: 0x17000468 RID: 1128
    // (get) Token: 0x060012D8 RID: 4824 RVA: 0x003A3678 File Offset: 0x003A3678
    public bool LeadChanged { get; }

    // Token: 0x17000469 RID: 1129
    // (get) Token: 0x060012D9 RID: 4825 RVA: 0x003A3680 File Offset: 0x003A3680
    public bool IsPenBusy { get; }

    // Token: 0x060012DA RID: 4826 RVA: 0x003A3688 File Offset: 0x003A3688
    [NullableContext(1)]
    public ClickyEdgeStackInkReading(ClickyInkLiveNote note, double clock, bool thingIsRunning, bool thingWantsTheUser, ClickyEdgeStackInkReading.State restState, double? seenClock, ClickyInkActionFeedback feedback)
    {
      ClickyEdgeStackInkReading.<>c__DisplayClass42_0 CS$<>8__locals1 = new ClickyEdgeStackInkReading.<>c__DisplayClass42_0();
      CS$<>8__locals1.clock = clock;
      base..ctor();
      this.IsPenBusy = CS$<>8__locals1.clock < note.FinishedClock;
      this.Title = note.Title;
      List<ClickyInkLiveNote.Block> list = ((note.Pages.Count > 0) ? note.Pages[0].Blocks.Where<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.IsPresent(CS$<>8__locals1.clock) && !b.IsStruck(CS$<>8__locals1.clock)).ToList<ClickyInkLiveNote.Block>() : new List<ClickyInkLiveNote.Block>());
      ClickyInkLiveNote.Block answer = list.FirstOrDefault<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.Source.Kind == ClickyInkDocument.Kind.Answer);
      ClickyInkLiveNote.Block block;
      if ((block = answer) == null)
      {
        block = list.FirstOrDefault<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => ClickyEdgeStackInkReading.WordsKinds.Contains(b.Source.Kind));
      }
      ClickyInkLiveNote.Block block2 = block;
      this.Lead = ((block2 != null) ? block2.Resolved(CS$<>8__locals1.clock) : null);
      if (answer != null)
      {
        int num = list.FindIndex((ClickyInkLiveNote.Block b) => b.Id == answer.Id);
        if (num >= 0)
        {
          ClickyInkLiveNote.Block block3 = list.Skip<ClickyInkLiveNote.Block>(num + 1).FirstOrDefault<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => ClickyEdgeStackInkReading.WordsKinds.Contains(b.Source.Kind));
          this.Support = ((block3 != null) ? block3.Resolved(CS$<>8__locals1.clock) : null);
        }
      }
      ValueTuple<string, string>? valueTuple = null;
      foreach (ClickyInkLiveNote.Page page in note.Pages)
      {
        if (valueTuple != null)
        {
          break;
        }
        foreach (ClickyInkLiveNote.Block block4 in page.Blocks)
        {
          if (block4.IsPresent(CS$<>8__locals1.clock) && !block4.IsStruck(CS$<>8__locals1.clock))
          {
            string text = ClickyEdgeStackInkReading.WaitingLineFor(block4.SourceAt(CS$<>8__locals1.clock), feedback);
            if (text != null)
            {
              valueTuple = new ValueTuple<string, string>?(new ValueTuple<string, string>(block4.Id, text));
              break;
            }
          }
        }
      }
      this.WaitingBlockID = ((valueTuple != null) ? valueTuple.GetValueOrDefault().Item1 : null);
      this.WaitingLine = ((valueTuple != null) ? valueTuple.GetValueOrDefault().Item2 : null);
      ClickyInkSentAction pending = feedback.Pending;
      if (pending != null)
      {
        Func<ClickyInkLiveNote.Block, bool> <>9__5;
        foreach (ClickyInkLiveNote.Page page2 in note.Pages)
        {
          if (this.PendingBlockID != null)
          {
            break;
          }
          IEnumerable<ClickyInkLiveNote.Block> blocks = page2.Blocks;
          Func<ClickyInkLiveNote.Block, bool> func;
          if ((func = <>9__5) == null)
          {
            func = (<>9__5 = (ClickyInkLiveNote.Block b) => b.IsPresent(CS$<>8__locals1.clock) && ClickyInkObjectKey.Of(b.SourceAt(CS$<>8__locals1.clock)) == pending.ObjectKey);
          }
          ClickyInkLiveNote.Block block5 = blocks.FirstOrDefault<ClickyInkLiveNote.Block>(func);
          if (block5 != null)
          {
            this.PendingBlockID = block5.Id;
          }
        }
        this.PendingLine = pending.MarginWords;
      }
      if (thingIsRunning || this.IsPenBusy)
      {
        this.CurrentState = ClickyEdgeStackInkReading.State.Writing;
      }
      else if (valueTuple != null || thingWantsTheUser)
      {
        this.CurrentState = ClickyEdgeStackInkReading.State.NeedsYou;
      }
      else
      {
        bool flag = restState == ClickyEdgeStackInkReading.State.Ready || restState - ClickyEdgeStackInkReading.State.Watching <= 1;
        this.CurrentState = (flag ? restState : ClickyEdgeStackInkReading.State.Done);
      }
      if (seenClock != null)
      {
        double seen = seenClock.GetValueOrDefault();
        if (seen > 0.5 && seen < CS$<>8__locals1.clock)
        {
          Func<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>, bool> <>9__6;
          foreach (ClickyInkLiveNote.Page page3 in note.Pages)
          {
            foreach (ClickyInkLiveNote.Block block6 in page3.Blocks)
            {
              bool flag = block6.IsPreview;
              if (!flag)
              {
                ClickyInkDocument.Kind kind = block6.Source.Kind;
                bool flag2 = kind == ClickyInkDocument.Kind.Heading || kind == ClickyInkDocument.Kind.Rule;
                flag = flag2;
              }
              if (!flag)
              {
                double? num2 = block6.GoneAt;
                bool flag3;
                if (num2 != null)
                {
                  double valueOrDefault = num2.GetValueOrDefault();
                  flag3 = valueOrDefault <= CS$<>8__locals1.clock;
                }
                else
                {
                  flag3 = false;
                }
                bool flag4 = flag3;
                if (block6.LandsAt > seen)
                {
                  if (block6.LandsAt <= CS$<>8__locals1.clock && !flag4)
                  {
                    this.ChangeCounts.New++;
                  }
                }
                else
                {
                  num2 = block6.GoneAt;
                  if (num2 != null)
                  {
                    double valueOrDefault2 = num2.GetValueOrDefault();
                    if (valueOrDefault2 > seen && valueOrDefault2 <= CS$<>8__locals1.clock)
                    {
                      this.ChangeCounts.Gone++;
                      continue;
                    }
                  }
                  if (!flag4)
                  {
                    IEnumerable<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>> enumerable = block6.Stamps.Skip<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>>(1);
                    Func<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>, bool> func2;
                    if ((func2 = <>9__6) == null)
                    {
                      func2 = (<>9__6 = (ClickyInkLiveNote.Stamp<ClickyInkDocument.Block> s) => s.At > seen && s.At <= CS$<>8__locals1.clock);
                    }
                    if (enumerable.Any<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>>(func2))
                    {
                      this.ChangeCounts.Updated++;
                    }
                  }
                }
              }
            }
          }
          ClickyInkLiveNote.Words title = note.Title;
          this.TitleChanged = title != null && ClickyEdgeStackInkReading.Changed(title, seen, CS$<>8__locals1.clock);
          ClickyInkLiveNote.Block lead = this.Lead;
          if (lead != null)
          {
            this.LeadChanged = lead.Items.Count == 0 && (lead.LandsAt > seen || lead.ShownSince() > seen || ClickyEdgeStackInkReading.Changed(lead.Words, seen, CS$<>8__locals1.clock));
          }
          Func<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>, bool> <>9__7;
          foreach (ClickyInkLiveNote.Block block7 in new ClickyInkLiveNote.Block[] { this.Lead, this.Support })
          {
            if (block7 != null)
            {
              foreach (ClickyInkLiveNote.Item item in block7.Items)
              {
                if (item.LandsAt <= seen)
                {
                  IEnumerable<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>> enumerable2 = item.Stamps.Skip<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>>(1);
                  Func<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>, bool> func3;
                  if ((func3 = <>9__7) == null)
                  {
                    func3 = (<>9__7 = (ClickyInkLiveNote.Stamp<ClickyInkDocument.Item> s) => s.At > seen);
                  }
                  if (!enumerable2.Any<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>>(func3) && !ClickyEdgeStackInkReading.Changed(item.Words, seen, CS$<>8__locals1.clock))
                  {
                    continue;
                  }
                }
                this.ChangedItemIDs.Add(item.Id);
              }
            }
          }
          return;
        }
      }
    }

    // Token: 0x060012DB RID: 4827 RVA: 0x003A3E18 File Offset: 0x003A3E18
    [NullableContext(1)]
    [return: Nullable(2)]
    public static string WaitingLineFor(ClickyInkDocument.Block source, ClickyInkActionFeedback feedback)
    {
      ClickyInkDocument.Kind kind = source.Kind;
      if (kind != ClickyInkDocument.Kind.Question)
      {
        if (kind != ClickyInkDocument.Kind.Review)
        {
          return null;
        }
        ClickyInkSentAction clickyInkSentAction = feedback.AnswerFor(source);
        IReadOnlyList<string> readOnlyList = ((clickyInkSentAction != null) ? clickyInkSentAction.ReviewedItems : null);
        if (readOnlyList != null && readOnlyList.Count == 0)
        {
          return null;
        }
        IReadOnlyList<string> alreadySent = ((clickyInkSentAction != null) ? clickyInkSentAction.ReviewedItems : null) ?? Array.Empty<string>();
        List<ClickyInkDocument.Item> list = source.Items.Where<ClickyInkDocument.Item>((ClickyInkDocument.Item i) => ClickyInkReviewRules.IsOpen(i) && !ClickyInkReviewRules.WasSent(i, alreadySent)).ToList<ClickyInkDocument.Item>();
        if (list.Count == 0)
        {
          return null;
        }
        string text = ClickyInkReviewRules.Label(source);
        string text2;
        if (list.Count != 1)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
          defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
          defaultInterpolatedStringHandler.AppendLiteral(" to decide");
          text2 = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        else
        {
          text2 = "1 to decide";
        }
        string text3 = text2;
        if (text.Length != 0)
        {
          return text + " · " + text3;
        }
        return "Your OK · " + text3;
      }
      else
      {
        if (feedback.AnswerFor(source) != null)
        {
          return null;
        }
        string text4 = ClickyInkNoteActions.Plain(source.Text);
        if (text4.Length != 0)
        {
          return text4;
        }
        return null;
      }
    }

    // Token: 0x060012DC RID: 4828 RVA: 0x003A3F3C File Offset: 0x003A3F3C
    [NullableContext(1)]
    private static bool Changed(ClickyInkLiveNote.Words words, double seenClock, double clock)
    {
      return words.Segments.Any<ClickyInkLiveNote.Segment>(delegate(ClickyInkLiveNote.Segment segment)
      {
        if (segment.IsPreview)
        {
          return false;
        }
        InkRange? inkRange = segment.Written;
        bool flag;
        if (inkRange != null)
        {
          InkRange valueOrDefault = inkRange.GetValueOrDefault();
          if (valueOrDefault.LowerBound > seenClock)
          {
            flag = valueOrDefault.LowerBound <= clock;
            goto IL_0048;
          }
        }
        flag = false;
        IL_0048:
        inkRange = segment.Struck;
        bool flag2;
        if (inkRange != null)
        {
          InkRange valueOrDefault2 = inkRange.GetValueOrDefault();
          if (valueOrDefault2.LowerBound > seenClock)
          {
            flag2 = valueOrDefault2.LowerBound <= clock;
            goto IL_0085;
          }
        }
        flag2 = false;
        IL_0085:
        bool flag3 = flag2;
        return flag || flag3;
      });
    }

    // Token: 0x04000882 RID: 2178
    [Nullable(1)]
    private static readonly ClickyInkDocument.Kind[] WordsKinds = new ClickyInkDocument.Kind[]
    {
      ClickyInkDocument.Kind.Paragraph,
      ClickyInkDocument.Kind.List,
      ClickyInkDocument.Kind.Checklist,
      ClickyInkDocument.Kind.Numbered,
      ClickyInkDocument.Kind.Quiet,
      ClickyInkDocument.Kind.Quote
    };

    // Token: 0x0200048A RID: 1162
    [NullableContext(0)]
    public enum State
    {
      // Token: 0x04001D3C RID: 7484
      Writing,
      // Token: 0x04001D3D RID: 7485
      NeedsYou,
      // Token: 0x04001D3E RID: 7486
      Ready,
      // Token: 0x04001D3F RID: 7487
      Done,
      // Token: 0x04001D40 RID: 7488
      Watching,
      // Token: 0x04001D41 RID: 7489
      Paused
    }

    // Token: 0x0200048B RID: 1163
    [NullableContext(1)]
    [Nullable(0)]
    public sealed class Changes : IEquatable<ClickyEdgeStackInkReading.Changes>
    {
      // Token: 0x17000876 RID: 2166
      // (get) Token: 0x06002EE0 RID: 12000 RVA: 0x00430B00 File Offset: 0x00430B00
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyEdgeStackInkReading.Changes);
        }
      }

      // Token: 0x17000877 RID: 2167
      // (get) Token: 0x06002EE1 RID: 12001 RVA: 0x00430B0C File Offset: 0x00430B0C
      // (set) Token: 0x06002EE2 RID: 12002 RVA: 0x00430B14 File Offset: 0x00430B14
      public int New { get; set; }

      // Token: 0x17000878 RID: 2168
      // (get) Token: 0x06002EE3 RID: 12003 RVA: 0x00430B20 File Offset: 0x00430B20
      // (set) Token: 0x06002EE4 RID: 12004 RVA: 0x00430B28 File Offset: 0x00430B28
      public int Updated { get; set; }

      // Token: 0x17000879 RID: 2169
      // (get) Token: 0x06002EE5 RID: 12005 RVA: 0x00430B34 File Offset: 0x00430B34
      // (set) Token: 0x06002EE6 RID: 12006 RVA: 0x00430B3C File Offset: 0x00430B3C
      public int Gone { get; set; }

      // Token: 0x1700087A RID: 2170
      // (get) Token: 0x06002EE7 RID: 12007 RVA: 0x00430B48 File Offset: 0x00430B48
      public bool IsEmpty
      {
        get
        {
          return this.New + this.Updated + this.Gone == 0;
        }
      }

      // Token: 0x06002EE8 RID: 12008 RVA: 0x00430B64 File Offset: 0x00430B64
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Changes");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002EE9 RID: 12009 RVA: 0x00430BB0 File Offset: 0x00430BB0
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("New = ");
        builder.Append(this.New.ToString());
        builder.Append(", Updated = ");
        builder.Append(this.Updated.ToString());
        builder.Append(", Gone = ");
        builder.Append(this.Gone.ToString());
        builder.Append(", IsEmpty = ");
        builder.Append(this.IsEmpty.ToString());
        return true;
      }

      // Token: 0x06002EEA RID: 12010 RVA: 0x00430C60 File Offset: 0x00430C60
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyEdgeStackInkReading.Changes left, ClickyEdgeStackInkReading.Changes right)
      {
        return !(left == right);
      }

      // Token: 0x06002EEB RID: 12011 RVA: 0x00430C6C File Offset: 0x00430C6C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyEdgeStackInkReading.Changes left, ClickyEdgeStackInkReading.Changes right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002EEC RID: 12012 RVA: 0x00430C80 File Offset: 0x00430C80
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<New>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Updated>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Gone>k__BackingField);
      }

      // Token: 0x06002EED RID: 12013 RVA: 0x00430CE4 File Offset: 0x00430CE4
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyEdgeStackInkReading.Changes);
      }

      // Token: 0x06002EEE RID: 12014 RVA: 0x00430CF4 File Offset: 0x00430CF4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyEdgeStackInkReading.Changes other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<New>k__BackingField, other.<New>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Updated>k__BackingField, other.<Updated>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Gone>k__BackingField, other.<Gone>k__BackingField));
      }

      // Token: 0x06002EF0 RID: 12016 RVA: 0x00430D70 File Offset: 0x00430D70
      [CompilerGenerated]
      private Changes(ClickyEdgeStackInkReading.Changes original)
      {
        this.New = original.<New>k__BackingField;
        this.Updated = original.<Updated>k__BackingField;
        this.Gone = original.<Gone>k__BackingField;
      }

      // Token: 0x06002EF1 RID: 12017 RVA: 0x00430D9C File Offset: 0x00430D9C
      public Changes()
      {
      }
    }
  }
}
