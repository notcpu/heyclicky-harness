using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200014E RID: 334
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkFrontPageContent
  {
    // Token: 0x170004D8 RID: 1240
    // (get) Token: 0x060014CE RID: 5326 RVA: 0x003B1314 File Offset: 0x003B1314
    public ClickyInkLiveNote Note { get; }

    // Token: 0x170004D9 RID: 1241
    // (get) Token: 0x060014CF RID: 5327 RVA: 0x003B131C File Offset: 0x003B131C
    public ClickyInkDashboardBoard Board { get; }

    // Token: 0x170004DA RID: 1242
    // (get) Token: 0x060014D0 RID: 5328 RVA: 0x003B1324 File Offset: 0x003B1324
    public ClickyInkFrontPagePlanner.Plan Plan { get; }

    // Token: 0x170004DB RID: 1243
    // (get) Token: 0x060014D1 RID: 5329 RVA: 0x003B132C File Offset: 0x003B132C
    public double Clock { get; }

    // Token: 0x060014D2 RID: 5330 RVA: 0x003B1334 File Offset: 0x003B1334
    public ClickyInkFrontPageContent(ClickyInkLiveNote note, ClickyInkDashboardBoard board, ClickyInkFrontPagePlanner.Plan plan, double clock)
    {
      this.Note = note;
      this.Board = board;
      this.Plan = plan;
      this.Clock = clock;
      foreach (ClickyInkDashboardBoard.Section section in board.Sections)
      {
        foreach (ClickyInkGroup clickyInkGroup in section.Groups)
        {
          this.groupsByID[clickyInkGroup.Id] = clickyInkGroup;
          if (!plan.PlacedGroupIDs.Contains(clickyInkGroup.Id) && !plan.StatGroupIDs.Contains(clickyInkGroup.Id))
          {
            string text2;
            string text = (plan.StoryOfPage.TryGetValue(section.Id, out text2) ? text2 : plan.LastStoryID);
            List<ClickyInkGroup> list;
            if (!this.strays.TryGetValue(text, out list))
            {
              list = (this.strays[text] = new List<ClickyInkGroup>());
            }
            list.Add(clickyInkGroup);
          }
        }
      }
    }

    // Token: 0x170004DC RID: 1244
    // (get) Token: 0x060014D3 RID: 5331 RVA: 0x003B1488 File Offset: 0x003B1488
    public List<ClickyInkLiveNote.Block> StatBlocks
    {
      get
      {
        IEnumerable<ClickyInkLiveNote.Block> enumerable = (from g in this.Board.Sections.SelectMany<ClickyInkDashboardBoard.Section, ClickyInkGroup>((ClickyInkDashboardBoard.Section s) => s.Groups)
          where this.Plan.StatGroupIDs.Contains(g.Id)
          select g).SelectMany<ClickyInkGroup, ClickyInkLiveNote.Block>((ClickyInkGroup g) => g.Blocks);
        return this.Board.Strip.Concat<ClickyInkLiveNote.Block>(enumerable).Take<ClickyInkLiveNote.Block>(8).ToList<ClickyInkLiveNote.Block>();
      }
    }

    // Token: 0x170004DD RID: 1245
    // (get) Token: 0x060014D4 RID: 5332 RVA: 0x003B1518 File Offset: 0x003B1518
    public List<ClickyInkGroup> MoveGroups
    {
      get
      {
        ClickyInkDashboardBoard.Section move = this.Board.Move;
        return ((move != null) ? move.Groups : null) ?? new List<ClickyInkGroup>();
      }
    }

    // Token: 0x060014D5 RID: 5333 RVA: 0x003B153C File Offset: 0x003B153C
    [return: Nullable(2)]
    public ClickyInkGroup Group(string id)
    {
      ClickyInkGroup clickyInkGroup;
      if (!this.groupsByID.TryGetValue(id, out clickyInkGroup))
      {
        return null;
      }
      return clickyInkGroup;
    }

    // Token: 0x060014D6 RID: 5334 RVA: 0x003B155C File Offset: 0x003B155C
    public List<ClickyInkGroup> Groups(ClickyInkFrontPagePlanner.Chunk chunk)
    {
      List<ClickyInkGroup> list = new List<ClickyInkGroup>();
      using (List<ClickyInkFrontPagePlanner.Piece>.Enumerator enumerator = chunk.Pieces.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ClickyInkFrontPagePlanner.Piece piece = enumerator.Current;
          ClickyInkGroup clickyInkGroup;
          if (this.groupsByID.TryGetValue(piece.GroupID, out clickyInkGroup))
          {
            if (piece.ItemIDs != null || piece.TakesOtherItems)
            {
              HashSet<string> kept = new HashSet<string>(piece.ItemIDs ?? new List<string>());
              HashSet<string> claimed = piece.OtherClaimedItemIDs;
              ClickyInkLiveNote.Block block = clickyInkGroup.Blocks[0].Clone();
              block.Items = block.Items.Where<ClickyInkLiveNote.Item>((ClickyInkLiveNote.Item item) => kept.Contains(item.Id) || (piece.TakesOtherItems && !claimed.Contains(item.Id))).ToList<ClickyInkLiveNote.Item>();
              if (piece.Part > 0)
              {
                ClickyInkLiveNote.Block block2 = block;
                string id = block2.Id;
                DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
                defaultInterpolatedStringHandler.AppendLiteral("-part");
                defaultInterpolatedStringHandler.AppendFormatted<int>(piece.Part);
                block2.Id = id + defaultInterpolatedStringHandler.ToStringAndClear();
                block.Words = ClickyInkLiveNote.Words.Empty;
              }
              if (block.Items.Count == 0)
              {
                continue;
              }
              ClickyInkGroup clickyInkGroup2 = clickyInkGroup.<Clone>$();
              List<ClickyInkLiveNote.Block> list2 = new List<ClickyInkLiveNote.Block>();
              list2.Add(block);
              list2.AddRange(clickyInkGroup.Blocks.Skip<ClickyInkLiveNote.Block>(1));
              clickyInkGroup2.Blocks = list2;
              clickyInkGroup = clickyInkGroup2;
            }
            list.Add(clickyInkGroup);
          }
        }
      }
      List<ClickyInkGroup> list3;
      if (chunk.IsLastOfStory && this.strays.TryGetValue(chunk.StoryID, out list3))
      {
        list.AddRange(list3);
      }
      return list;
    }

    // Token: 0x060014D7 RID: 5335 RVA: 0x003B1734 File Offset: 0x003B1734
    [return: Nullable(2)]
    public ClickyInkLiveNote.Words StoryName(string storyID)
    {
      if (this.Board.Run == null)
      {
        ClickyInkLiveNote.Page page = this.Note.Pages.FirstOrDefault<ClickyInkLiveNote.Page>((ClickyInkLiveNote.Page p) => p.Id == storyID);
        if (page != null)
        {
          return page.Name;
        }
      }
      ClickyInkDashboardBoard.Section section = this.Board.Sections.FirstOrDefault<ClickyInkDashboardBoard.Section>((ClickyInkDashboardBoard.Section s) => s.Id == storyID);
      ClickyInkLiveNote.Words words;
      if ((words = ((section != null) ? section.Name : null)) == null)
      {
        ClickyInkLiveNote.Page page2 = this.Note.Pages.FirstOrDefault<ClickyInkLiveNote.Page>((ClickyInkLiveNote.Page p) => p.Id == storyID);
        if (page2 == null)
        {
          return null;
        }
        words = page2.Name;
      }
      return words;
    }

    // Token: 0x060014D8 RID: 5336 RVA: 0x003B17D8 File Offset: 0x003B17D8
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "New", "Needs" })]
    public ValueTuple<int, int> Counts([Nullable(1)] string storyID, double clock)
    {
      ClickyInkDashboardBoard.Section section = this.Board.Sections.FirstOrDefault<ClickyInkDashboardBoard.Section>((ClickyInkDashboardBoard.Section s) => s.Id == storyID);
      if (section == null)
      {
        return new ValueTuple<int, int>(0, 0);
      }
      List<ClickyInkLiveNote.Item> list = (from i in section.Groups.SelectMany<ClickyInkGroup, ClickyInkLiveNote.Block>((ClickyInkGroup g) => g.Blocks).SelectMany<ClickyInkLiveNote.Block, ClickyInkLiveNote.Item>((ClickyInkLiveNote.Block b) => b.Items)
        where !i.IsStruck(clock)
        select i).ToList<ClickyInkLiveNote.Item>();
      int num;
      if (!section.IsNamedNew)
      {
        num = list.Count<ClickyInkLiveNote.Item>(delegate(ClickyInkLiveNote.Item i)
        {
          ClickyInkDocument.Mark? mark = i.Source.Mark;
          ClickyInkDocument.Mark mark2 = ClickyInkDocument.Mark.New;
          return (mark.GetValueOrDefault() == mark2) & (mark != null);
        });
      }
      else
      {
        num = 0;
      }
      return new ValueTuple<int, int>(num, list.Count<ClickyInkLiveNote.Item>((ClickyInkLiveNote.Item i) => i.Source.Mark.GetValueOrDefault() == ClickyInkDocument.Mark.Needs));
    }

    // Token: 0x060014D9 RID: 5337 RVA: 0x003B18E0 File Offset: 0x003B18E0
    public bool HasBadStatus(string storyID)
    {
      ClickyInkDashboardBoard.Section section = this.Board.Sections.FirstOrDefault<ClickyInkDashboardBoard.Section>((ClickyInkDashboardBoard.Section s) => s.Id == storyID);
      if (section != null)
      {
        return section.Groups.SelectMany<ClickyInkGroup, ClickyInkLiveNote.Block>((ClickyInkGroup g) => g.Blocks).Any<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.Source.Kind == ClickyInkDocument.Kind.Status && ClickyInkStatus.State(b.Source["state"] ?? b.Source["status"]) == ClickyInkStatusState.Bad);
      }
      return false;
    }

    // Token: 0x0400091C RID: 2332
    private readonly Dictionary<string, ClickyInkGroup> groupsByID = new Dictionary<string, ClickyInkGroup>();

    // Token: 0x0400091D RID: 2333
    private readonly Dictionary<string, List<ClickyInkGroup>> strays = new Dictionary<string, List<ClickyInkGroup>>();
  }
}
