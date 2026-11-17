using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200012D RID: 301
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkDashboardBoard
  {
    // Token: 0x17000497 RID: 1175
    // (get) Token: 0x06001396 RID: 5014 RVA: 0x003A71FC File Offset: 0x003A71FC
    // (set) Token: 0x06001397 RID: 5015 RVA: 0x003A7204 File Offset: 0x003A7204
    [Nullable(2)]
    public ClickyInkLiveNote.Block Run
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000498 RID: 1176
    // (get) Token: 0x06001398 RID: 5016 RVA: 0x003A7210 File Offset: 0x003A7210
    // (set) Token: 0x06001399 RID: 5017 RVA: 0x003A7218 File Offset: 0x003A7218
    [Nullable(2)]
    public ClickyInkGroup Answer
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000499 RID: 1177
    // (get) Token: 0x0600139A RID: 5018 RVA: 0x003A7224 File Offset: 0x003A7224
    // (set) Token: 0x0600139B RID: 5019 RVA: 0x003A722C File Offset: 0x003A722C
    [Nullable(2)]
    public ClickyInkGroup Quiet
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700049A RID: 1178
    // (get) Token: 0x0600139C RID: 5020 RVA: 0x003A7238 File Offset: 0x003A7238
    // (set) Token: 0x0600139D RID: 5021 RVA: 0x003A7240 File Offset: 0x003A7240
    public List<ClickyInkLiveNote.Block> Strip { get; set; } = new List<ClickyInkLiveNote.Block>();

    // Token: 0x1700049B RID: 1179
    // (get) Token: 0x0600139E RID: 5022 RVA: 0x003A724C File Offset: 0x003A724C
    // (set) Token: 0x0600139F RID: 5023 RVA: 0x003A7254 File Offset: 0x003A7254
    [Nullable(2)]
    public ClickyInkDashboardBoard.Section Move
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700049C RID: 1180
    // (get) Token: 0x060013A0 RID: 5024 RVA: 0x003A7260 File Offset: 0x003A7260
    // (set) Token: 0x060013A1 RID: 5025 RVA: 0x003A7268 File Offset: 0x003A7268
    public List<ClickyInkLiveNote.Block> Sources { get; set; } = new List<ClickyInkLiveNote.Block>();

    // Token: 0x1700049D RID: 1181
    // (get) Token: 0x060013A2 RID: 5026 RVA: 0x003A7274 File Offset: 0x003A7274
    // (set) Token: 0x060013A3 RID: 5027 RVA: 0x003A727C File Offset: 0x003A727C
    public List<ClickyInkDashboardBoard.Section> Sections { get; set; } = new List<ClickyInkDashboardBoard.Section>();

    // Token: 0x1700049E RID: 1182
    // (get) Token: 0x060013A4 RID: 5028 RVA: 0x003A7288 File Offset: 0x003A7288
    // (set) Token: 0x060013A5 RID: 5029 RVA: 0x003A7290 File Offset: 0x003A7290
    public ClickyInkStatusState Overall { get; set; }

    // Token: 0x1700049F RID: 1183
    // (get) Token: 0x060013A6 RID: 5030 RVA: 0x003A729C File Offset: 0x003A729C
    // (set) Token: 0x060013A7 RID: 5031 RVA: 0x003A72A4 File Offset: 0x003A72A4
    public ClickyInkDashboardBoard.ChangeCounts Changes { get; set; } = new ClickyInkDashboardBoard.ChangeCounts();

    // Token: 0x060013A8 RID: 5032 RVA: 0x003A72B0 File Offset: 0x003A72B0
    public ClickyInkDashboardBoard(ClickyInkLiveNote note, double clock, string firstSectionName = "This run")
    {
      List<ValueTuple<ClickyInkLiveNote.Page, List<ClickyInkGroup>>> list = note.Pages.Select<ClickyInkLiveNote.Page, ValueTuple<ClickyInkLiveNote.Page, List<ClickyInkGroup>>>((ClickyInkLiveNote.Page page) => new ValueTuple<ClickyInkLiveNote.Page, List<ClickyInkGroup>>(page, ClickyInkGroup.Groups(page, clock))).ToList<ValueTuple<ClickyInkLiveNote.Page, List<ClickyInkGroup>>>();
      ClickyInkGroup clickyInkGroup = list.SelectMany<ValueTuple<ClickyInkLiveNote.Page, List<ClickyInkGroup>>, ClickyInkGroup>(([TupleElementNames(new string[] { "Page", "Groups" })] ValueTuple<ClickyInkLiveNote.Page, List<ClickyInkGroup>> p) => p.Item2).FirstOrDefault<ClickyInkGroup>((ClickyInkGroup g) => g.Kind == ClickyInkDocument.Kind.Tiles);
      string text = ((clickyInkGroup != null) ? clickyInkGroup.Id : null);
      List<ClickyInkGroup> list2 = new List<ClickyInkGroup>();
      bool flag = false;
      bool flag2 = false;
      for (int j = 0; j < list.Count; j++)
      {
        ValueTuple<ClickyInkLiveNote.Page, List<ClickyInkGroup>> valueTuple = list[j];
        ClickyInkLiveNote.Page item = valueTuple.Item1;
        List<ClickyInkGroup> item2 = valueTuple.Item2;
        List<ClickyInkGroup> list3 = new List<ClickyInkGroup>();
        foreach (ClickyInkGroup clickyInkGroup2 in item2)
        {
          ClickyInkLiveNote.Block block = clickyInkGroup2.Blocks[0];
          ClickyInkDocument.Kind kind = clickyInkGroup2.Kind;
          if (kind == ClickyInkDocument.Kind.Run && this.Run == null)
          {
            this.Run = block;
          }
          else if (kind == ClickyInkDocument.Kind.Answer && this.Answer == null)
          {
            this.Answer = clickyInkGroup2;
          }
          else if (kind == ClickyInkDocument.Kind.Quiet)
          {
            if (this.Quiet == null && !block.IsStruck(clock))
            {
              this.Quiet = clickyInkGroup2;
            }
          }
          else if (kind == ClickyInkDocument.Kind.Tiles && clickyInkGroup2.Id == text)
          {
            this.Strip = clickyInkGroup2.Blocks.ToList<ClickyInkLiveNote.Block>();
          }
          else if (text == null && j == 0 && ClickyInkDashboardBoard.ImplicitStripKinds.Contains(kind) && this.Strip.Count < 8)
          {
            this.Strip.AddRange(clickyInkGroup2.Blocks.Take<ClickyInkLiveNote.Block>(8 - this.Strip.Count));
          }
          else
          {
            bool flag3 = kind - ClickyInkDocument.Kind.Ask <= 1 || kind == ClickyInkDocument.Kind.Review;
            if (flag3)
            {
              list2.Add(clickyInkGroup2);
              if (kind != ClickyInkDocument.Kind.Ask)
              {
                flag = true;
              }
            }
            else if (kind == ClickyInkDocument.Kind.Link && clickyInkGroup2.IsMadeLink)
            {
              list2.Add(clickyInkGroup2);
            }
            else if (kind == ClickyInkDocument.Kind.Link)
            {
              this.Sources.AddRange(clickyInkGroup2.Blocks);
            }
            else
            {
              list3.Add(clickyInkGroup2);
            }
          }
          if (clickyInkGroup2.Blocks.Any<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.Source.Kind == ClickyInkDocument.Kind.Status && ClickyInkStatus.State(b.Source["state"] ?? b.Source["status"]) == ClickyInkStatusState.Bad))
          {
            flag2 = true;
          }
          if (clickyInkGroup2.Blocks.SelectMany<ClickyInkLiveNote.Block, ClickyInkLiveNote.Item>((ClickyInkLiveNote.Block b) => b.Items).Any<ClickyInkLiveNote.Item>((ClickyInkLiveNote.Item i) => i.Source.Mark.GetValueOrDefault() == ClickyInkDocument.Mark.Needs))
          {
            flag = true;
          }
        }
        string text2 = ClickyInkDashboardBoard.TrimPunctuation(ClickyInkParser.Emphasis.Strip(item.Name.Current).ToLowerInvariant());
        if (ClickyInkDashboardBoard.MovePageNames.Contains(text2))
        {
          list2.AddRange(list3);
        }
        else if (list3.Count > 0)
        {
          bool flag4 = j == 0 && ClickyInkDashboardBoard.FirstPageNames.Contains(item.Name.Current.ToLowerInvariant());
          this.Sections.Add(new ClickyInkDashboardBoard.Section(item.Id, flag4 ? ClickyInkLiveNote.Words.Plain(firstSectionName) : item.Name, list3));
        }
      }
      if (list2.Count > 0)
      {
        this.Move = new ClickyInkDashboardBoard.Section("move", ClickyInkLiveNote.Words.Plain("Your move"), list2);
      }
      this.Overall = (flag2 ? ClickyInkStatusState.Bad : (flag ? ClickyInkStatusState.Watch : ClickyInkStatusState.Ok));
      this.Changes = ClickyInkDashboardBoard.ChangeCounts.Of(note, clock);
    }

    // Token: 0x060013A9 RID: 5033 RVA: 0x003A76B8 File Offset: 0x003A76B8
    public static ClickyInkDashboardBoard ForFrontPage(ClickyInkLiveNote note, double clock)
    {
      ClickyInkDashboardBoard clickyInkDashboardBoard = new ClickyInkDashboardBoard(note, clock, "This run");
      if (clickyInkDashboardBoard.Overall == ClickyInkStatusState.Ok)
      {
        ClickyInkDashboardBoard.Section move = clickyInkDashboardBoard.Move;
        bool flag;
        if (move == null)
        {
          flag = false;
        }
        else
        {
          flag = move.Groups.Any<ClickyInkGroup>((ClickyInkGroup g) => g.Kind != ClickyInkDocument.Kind.Ask && !g.IsMadeLink);
        }
        if (flag)
        {
          clickyInkDashboardBoard.Overall = ClickyInkStatusState.Watch;
        }
      }
      return clickyInkDashboardBoard;
    }

    // Token: 0x060013AA RID: 5034 RVA: 0x003A771C File Offset: 0x003A771C
    private static string TrimPunctuation(string text)
    {
      Func<char, bool> func;
      if ((func = ClickyInkDashboardBoard.<>O.<0>__IsPunctuation) == null)
      {
        func = (ClickyInkDashboardBoard.<>O.<0>__IsPunctuation = new Func<char, bool>(char.IsPunctuation));
      }
      return InkText.TrimWhere(text, func);
    }

    // Token: 0x040008C2 RID: 2242
    private static readonly HashSet<ClickyInkDocument.Kind> ImplicitStripKinds = new HashSet<ClickyInkDocument.Kind>
    {
      ClickyInkDocument.Kind.Figure,
      ClickyInkDocument.Kind.Spark,
      ClickyInkDocument.Kind.Progress,
      ClickyInkDocument.Kind.Tally,
      ClickyInkDocument.Kind.Streak,
      ClickyInkDocument.Kind.Countdown
    };

    // Token: 0x040008C3 RID: 2243
    private static readonly HashSet<string> MovePageNames = new HashSet<string> { "your move", "needs you", "for you", "your call", "decide", "decisions", "to decide" };

    // Token: 0x040008C4 RID: 2244
    private static readonly HashSet<string> FirstPageNames = new HashSet<string> { "in short", "today", "now", "this run" };

    // Token: 0x020004A5 RID: 1189
    [Nullable(0)]
    public sealed class Section : IEquatable<ClickyInkDashboardBoard.Section>
    {
      // Token: 0x06002F50 RID: 12112 RVA: 0x0043183C File Offset: 0x0043183C
      public Section(string Id, ClickyInkLiveNote.Words Name, List<ClickyInkGroup> Groups)
      {
        this.Id = Id;
        this.Name = Name;
        this.Groups = Groups;
        base..ctor();
      }

      // Token: 0x17000882 RID: 2178
      // (get) Token: 0x06002F51 RID: 12113 RVA: 0x0043185C File Offset: 0x0043185C
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkDashboardBoard.Section);
        }
      }

      // Token: 0x17000883 RID: 2179
      // (get) Token: 0x06002F52 RID: 12114 RVA: 0x00431868 File Offset: 0x00431868
      // (set) Token: 0x06002F53 RID: 12115 RVA: 0x00431870 File Offset: 0x00431870
      public string Id { get; set; }

      // Token: 0x17000884 RID: 2180
      // (get) Token: 0x06002F54 RID: 12116 RVA: 0x0043187C File Offset: 0x0043187C
      // (set) Token: 0x06002F55 RID: 12117 RVA: 0x00431884 File Offset: 0x00431884
      public ClickyInkLiveNote.Words Name { get; set; }

      // Token: 0x17000885 RID: 2181
      // (get) Token: 0x06002F56 RID: 12118 RVA: 0x00431890 File Offset: 0x00431890
      // (set) Token: 0x06002F57 RID: 12119 RVA: 0x00431898 File Offset: 0x00431898
      public List<ClickyInkGroup> Groups { get; set; }

      // Token: 0x17000886 RID: 2182
      // (get) Token: 0x06002F58 RID: 12120 RVA: 0x004318A4 File Offset: 0x004318A4
      public bool IsNamedNew
      {
        get
        {
          return this.Name.Current.ToLowerInvariant().Contains("new", StringComparison.Ordinal);
        }
      }

      // Token: 0x06002F59 RID: 12121 RVA: 0x004318C4 File Offset: 0x004318C4
      public int NewCount(double clock)
      {
        return this.Groups.SelectMany<ClickyInkGroup, ClickyInkLiveNote.Block>((ClickyInkGroup g) => g.Blocks).SelectMany<ClickyInkLiveNote.Block, ClickyInkLiveNote.Item>((ClickyInkLiveNote.Block b) => b.Items).Count<ClickyInkLiveNote.Item>(delegate(ClickyInkLiveNote.Item i)
        {
          ClickyInkDocument.Mark? mark = i.Source.Mark;
          ClickyInkDocument.Mark mark2 = ClickyInkDocument.Mark.New;
          return ((mark.GetValueOrDefault() == mark2) & (mark != null)) && !i.IsStruck(clock);
        });
      }

      // Token: 0x06002F5A RID: 12122 RVA: 0x00431940 File Offset: 0x00431940
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Section");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002F5B RID: 12123 RVA: 0x0043198C File Offset: 0x0043198C
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Id = ");
        builder.Append(this.Id);
        builder.Append(", Name = ");
        builder.Append(this.Name);
        builder.Append(", Groups = ");
        builder.Append(this.Groups);
        builder.Append(", IsNamedNew = ");
        builder.Append(this.IsNamedNew.ToString());
        return true;
      }

      // Token: 0x06002F5C RID: 12124 RVA: 0x00431A14 File Offset: 0x00431A14
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkDashboardBoard.Section left, ClickyInkDashboardBoard.Section right)
      {
        return !(left == right);
      }

      // Token: 0x06002F5D RID: 12125 RVA: 0x00431A20 File Offset: 0x00431A20
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkDashboardBoard.Section left, ClickyInkDashboardBoard.Section right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002F5E RID: 12126 RVA: 0x00431A34 File Offset: 0x00431A34
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<ClickyInkLiveNote.Words>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<List<ClickyInkGroup>>.Default.GetHashCode(this.<Groups>k__BackingField);
      }

      // Token: 0x06002F5F RID: 12127 RVA: 0x00431A98 File Offset: 0x00431A98
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkDashboardBoard.Section);
      }

      // Token: 0x06002F60 RID: 12128 RVA: 0x00431AA8 File Offset: 0x00431AA8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkDashboardBoard.Section other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<ClickyInkLiveNote.Words>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<List<ClickyInkGroup>>.Default.Equals(this.<Groups>k__BackingField, other.<Groups>k__BackingField));
      }

      // Token: 0x06002F62 RID: 12130 RVA: 0x00431B24 File Offset: 0x00431B24
      [CompilerGenerated]
      private Section(ClickyInkDashboardBoard.Section original)
      {
        this.Id = original.<Id>k__BackingField;
        this.Name = original.<Name>k__BackingField;
        this.Groups = original.<Groups>k__BackingField;
      }

      // Token: 0x06002F63 RID: 12131 RVA: 0x00431B50 File Offset: 0x00431B50
      [CompilerGenerated]
      public void Deconstruct(out string Id, out ClickyInkLiveNote.Words Name, out List<ClickyInkGroup> Groups)
      {
        Id = this.Id;
        Name = this.Name;
        Groups = this.Groups;
      }
    }

    // Token: 0x020004A6 RID: 1190
    [Nullable(0)]
    public sealed class ChangeCounts : IEquatable<ClickyInkDashboardBoard.ChangeCounts>
    {
      // Token: 0x17000887 RID: 2183
      // (get) Token: 0x06002F64 RID: 12132 RVA: 0x00431B6C File Offset: 0x00431B6C
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkDashboardBoard.ChangeCounts);
        }
      }

      // Token: 0x17000888 RID: 2184
      // (get) Token: 0x06002F65 RID: 12133 RVA: 0x00431B78 File Offset: 0x00431B78
      // (set) Token: 0x06002F66 RID: 12134 RVA: 0x00431B80 File Offset: 0x00431B80
      public int New { get; set; }

      // Token: 0x17000889 RID: 2185
      // (get) Token: 0x06002F67 RID: 12135 RVA: 0x00431B8C File Offset: 0x00431B8C
      // (set) Token: 0x06002F68 RID: 12136 RVA: 0x00431B94 File Offset: 0x00431B94
      public int Updated { get; set; }

      // Token: 0x1700088A RID: 2186
      // (get) Token: 0x06002F69 RID: 12137 RVA: 0x00431BA0 File Offset: 0x00431BA0
      // (set) Token: 0x06002F6A RID: 12138 RVA: 0x00431BA8 File Offset: 0x00431BA8
      public int Gone { get; set; }

      // Token: 0x1700088B RID: 2187
      // (get) Token: 0x06002F6B RID: 12139 RVA: 0x00431BB4 File Offset: 0x00431BB4
      public int Total
      {
        get
        {
          return this.New + this.Updated + this.Gone;
        }
      }

      // Token: 0x06002F6C RID: 12140 RVA: 0x00431BCC File Offset: 0x00431BCC
      public ChangeCounts()
      {
      }

      // Token: 0x06002F6D RID: 12141 RVA: 0x00431BD4 File Offset: 0x00431BD4
      public static ClickyInkDashboardBoard.ChangeCounts Of(ClickyInkLiveNote note, double clock)
      {
        int? num = note.LatestEdit(clock);
        if (num != null)
        {
          int valueOrDefault = num.GetValueOrDefault();
          int num2 = 0;
          int num3 = 0;
          int num4 = 0;
          foreach (ClickyInkLiveNote.Block block in note.Pages.SelectMany<ClickyInkLiveNote.Page, ClickyInkLiveNote.Block>((ClickyInkLiveNote.Page p) => p.Blocks))
          {
            ClickyInkDocument.Kind kind = block.Source.Kind;
            if (kind != ClickyInkDocument.Kind.Answer && kind - ClickyInkDocument.Kind.Run > 1)
            {
              num = block.BornInEdit;
              int num5 = valueOrDefault;
              if ((num.GetValueOrDefault() == num5) & (num != null))
              {
                if (clock >= block.LandsAt)
                {
                  num2 += Math.Max(1, block.Items.Count);
                }
              }
              else
              {
                num = block.GoneInEdit;
                num5 = valueOrDefault;
                if ((num.GetValueOrDefault() == num5) & (num != null))
                {
                  if (block.IsStruck(clock))
                  {
                    num4++;
                  }
                }
                else
                {
                  bool flag = false;
                  foreach (ClickyInkLiveNote.Item item in block.Items)
                  {
                    if (item.Stamps.Count > 0)
                    {
                      num = item.Stamps[0].Edit;
                      num5 = valueOrDefault;
                      if (((num.GetValueOrDefault() == num5) & (num != null)) && clock >= item.LandsAt)
                      {
                        num2++;
                        flag = true;
                        continue;
                      }
                    }
                    num = item.GoneInEdit;
                    num5 = valueOrDefault;
                    if (((num.GetValueOrDefault() == num5) & (num != null)) && item.IsStruck(clock))
                    {
                      num4++;
                      flag = true;
                    }
                    else if (item.Stamps.Count > 1)
                    {
                      List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>> stamps = item.Stamps;
                      ClickyInkLiveNote.Stamp<ClickyInkDocument.Item> stamp = stamps[stamps.Count - 1];
                      num = stamp.Edit;
                      num5 = valueOrDefault;
                      if (((num.GetValueOrDefault() == num5) & (num != null)) && stamp.At <= clock)
                      {
                        List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>> stamps2 = item.Stamps;
                        if (ClickyInkDashboardBoard.ChangeCounts.FactsChanged(stamps2[stamps2.Count - 2].Value, stamp.Value))
                        {
                          num3++;
                          flag = true;
                        }
                      }
                    }
                  }
                  if (!flag && block.Stamps.Count > 1)
                  {
                    List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>> stamps3 = block.Stamps;
                    ClickyInkLiveNote.Stamp<ClickyInkDocument.Block> stamp2 = stamps3[stamps3.Count - 1];
                    num = stamp2.Edit;
                    num5 = valueOrDefault;
                    if (((num.GetValueOrDefault() == num5) & (num != null)) && stamp2.At <= clock)
                    {
                      List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>> stamps4 = block.Stamps;
                      ClickyInkDocument.Block value = stamps4[stamps4.Count - 2].Value;
                      if ((block.Source.Kind != ClickyInkDocument.Kind.Spark || !(ClickyInkSeries.Tokens(stamp2.Value["values"]).LastOrDefault<string>() == ClickyInkSeries.Tokens(value["values"]).LastOrDefault<string>())) && ClickyInkDashboardBoard.ChangeCounts.FactsChanged(value, stamp2.Value))
                      {
                        num3++;
                      }
                    }
                  }
                }
              }
            }
          }
          return new ClickyInkDashboardBoard.ChangeCounts
          {
            New = num2,
            Updated = num3,
            Gone = num4
          };
        }
        return new ClickyInkDashboardBoard.ChangeCounts();
      }

      // Token: 0x06002F6E RID: 12142 RVA: 0x00431F80 File Offset: 0x00431F80
      public static bool FactsChanged(ClickyInkDocument.Item before, ClickyInkDocument.Item after)
      {
        if (!(before.Text != after.Text) && before.Cells.SequenceEqual<string>(after.Cells))
        {
          bool? isChecked = before.IsChecked;
          bool? isChecked2 = after.IsChecked;
          if ((isChecked.GetValueOrDefault() == isChecked2.GetValueOrDefault()) & (isChecked != null == (isChecked2 != null)))
          {
            return before.Detail != after.Detail;
          }
        }
        return true;
      }

      // Token: 0x06002F6F RID: 12143 RVA: 0x00431FF8 File Offset: 0x00431FF8
      public static bool FactsChanged(ClickyInkDocument.Block before, ClickyInkDocument.Block after)
      {
        if (!(before.Text != after.Text) && ClickyInkDocument.Block.AttributesEqual(ClickyInkDashboardBoard.ChangeCounts.<FactsChanged>g__Facts|20_0(before.Attributes), ClickyInkDashboardBoard.ChangeCounts.<FactsChanged>g__Facts|20_0(after.Attributes)))
        {
          return !before.Items.Select<ClickyInkDocument.Item, string>((ClickyInkDocument.Item i) => i.Text).SequenceEqual<string>(after.Items.Select<ClickyInkDocument.Item, string>((ClickyInkDocument.Item i) => i.Text));
        }
        return true;
      }

      // Token: 0x06002F70 RID: 12144 RVA: 0x00432094 File Offset: 0x00432094
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ChangeCounts");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002F71 RID: 12145 RVA: 0x004320E0 File Offset: 0x004320E0
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
        builder.Append(", Total = ");
        builder.Append(this.Total.ToString());
        return true;
      }

      // Token: 0x06002F72 RID: 12146 RVA: 0x00432190 File Offset: 0x00432190
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkDashboardBoard.ChangeCounts left, ClickyInkDashboardBoard.ChangeCounts right)
      {
        return !(left == right);
      }

      // Token: 0x06002F73 RID: 12147 RVA: 0x0043219C File Offset: 0x0043219C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkDashboardBoard.ChangeCounts left, ClickyInkDashboardBoard.ChangeCounts right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002F74 RID: 12148 RVA: 0x004321B0 File Offset: 0x004321B0
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<New>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Updated>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Gone>k__BackingField);
      }

      // Token: 0x06002F75 RID: 12149 RVA: 0x00432214 File Offset: 0x00432214
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkDashboardBoard.ChangeCounts);
      }

      // Token: 0x06002F76 RID: 12150 RVA: 0x00432224 File Offset: 0x00432224
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkDashboardBoard.ChangeCounts other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<New>k__BackingField, other.<New>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Updated>k__BackingField, other.<Updated>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Gone>k__BackingField, other.<Gone>k__BackingField));
      }

      // Token: 0x06002F78 RID: 12152 RVA: 0x004322A0 File Offset: 0x004322A0
      [CompilerGenerated]
      private ChangeCounts(ClickyInkDashboardBoard.ChangeCounts original)
      {
        this.New = original.<New>k__BackingField;
        this.Updated = original.<Updated>k__BackingField;
        this.Gone = original.<Gone>k__BackingField;
      }

      // Token: 0x06002F7A RID: 12154 RVA: 0x00432308 File Offset: 0x00432308
      [CompilerGenerated]
      internal static Dictionary<string, string> <FactsChanged>g__Facts|20_0(Dictionary<string, string> attributes)
      {
        return attributes.Where<KeyValuePair<string, string>>((KeyValuePair<string, string> p) => !ClickyInkDashboardBoard.ChangeCounts.Bookkeeping.Contains(p.Key)).ToDictionary<KeyValuePair<string, string>, string, string>((KeyValuePair<string, string> p) => p.Key, (KeyValuePair<string, string> p) => p.Value);
      }

      // Token: 0x04001D8D RID: 7565
      private static readonly HashSet<string> Bookkeeping = new HashSet<string> { "since", "asked", "_tiles", "new" };
    }

    // Token: 0x020004A7 RID: 1191
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001D8E RID: 7566
      [Nullable(0)]
      public static Func<char, bool> <0>__IsPunctuation;
    }
  }
}
