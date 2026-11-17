using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Windows.Foundation;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200014D RID: 333
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkFrontPagePlanner
  {
    // Token: 0x060014C6 RID: 5318 RVA: 0x003B00E8 File Offset: 0x003B00E8
    public static int MissionTilesPerRow(int count, double width)
    {
      if (count <= 0)
      {
        return 1;
      }
      int num = Math.Max(1, Math.Min((int)(width / 190.0), 6));
      int num2 = (count + num - 1) / num;
      return Math.Max(1, (count + num2 - 1) / num2);
    }

    // Token: 0x060014C7 RID: 5319 RVA: 0x003B012C File Offset: 0x003B012C
    public static string Key(ClickyInkLiveNote note, ClickyInkFrontPageVariant variant)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 4);
      defaultInterpolatedStringHandler.AppendFormatted(variant.RawValue());
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(string.Join(",", note.Pages.Select<ClickyInkLiveNote.Page, string>((ClickyInkLiveNote.Page p) => p.Id)));
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<int>(note.VersionCount);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(InkText.DoubleDescription(note.PenFreeAt));
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x060014C8 RID: 5320 RVA: 0x003B01D4 File Offset: 0x003B01D4
    private static bool IsListKind(ClickyInkDocument.Kind kind)
    {
      return kind - ClickyInkDocument.Kind.List <= 2;
    }

    // Token: 0x060014C9 RID: 5321 RVA: 0x003B01F0 File Offset: 0x003B01F0
    public unsafe static ClickyInkFrontPagePlanner.Plan Make(ClickyInkLiveNote note, ClickyInkFrontPageVariant variant, IClickyInkFrontPageMeasurer measure)
    {
      ClickyInkFrontPagePlanner.<>c__DisplayClass16_0 CS$<>8__locals1 = new ClickyInkFrontPagePlanner.<>c__DisplayClass16_0();
      CS$<>8__locals1.measure = measure;
      CS$<>8__locals1.clock = note.FinishedClock + 30.0;
      CS$<>8__locals1.board = new ClickyInkDashboardBoard(note, CS$<>8__locals1.clock, "This run");
      CS$<>8__locals1.isMission = variant == ClickyInkFrontPageVariant.MissionControl;
      CS$<>8__locals1.statGroupIDs = new HashSet<string>();
      CS$<>8__locals1.statCount = CS$<>8__locals1.board.Strip.Count;
      List<ClickyInkDashboardBoard.Section> list = CS$<>8__locals1.board.Sections.Select<ClickyInkDashboardBoard.Section, ClickyInkDashboardBoard.Section>(delegate(ClickyInkDashboardBoard.Section section)
      {
        ClickyInkDashboardBoard.Section section3 = section.<Clone>$();
        IEnumerable<ClickyInkGroup> groups = section.Groups;
        Func<ClickyInkGroup, bool> func3;
        if ((func3 = CS$<>8__locals1.<>9__2) == null)
        {
          func3 = (CS$<>8__locals1.<>9__2 = delegate(ClickyInkGroup group)
          {
            if (group.Kind == ClickyInkDocument.Kind.Figure && CS$<>8__locals1.statCount + group.Blocks.Count <= 8)
            {
              IEnumerable<ClickyInkLiveNote.Block> blocks = group.Blocks;
              Func<ClickyInkLiveNote.Block, bool> func4;
              if ((func4 = CS$<>8__locals1.<>9__3) == null)
              {
                func4 = (CS$<>8__locals1.<>9__3 = (ClickyInkLiveNote.Block b) => b.IsStruck(CS$<>8__locals1.clock));
              }
              if (!blocks.Any<ClickyInkLiveNote.Block>(func4))
              {
                CS$<>8__locals1.statCount += group.Blocks.Count;
                CS$<>8__locals1.statGroupIDs.Add(group.Id);
                return false;
              }
            }
            return true;
          });
        }
        section3.Groups = groups.Where<ClickyInkGroup>(func3).ToList<ClickyInkGroup>();
        return section3;
      }).ToList<ClickyInkDashboardBoard.Section>();
      List<ClickyInkFrontPagePlanner.Story> list2 = new List<ClickyInkFrontPagePlanner.Story>();
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      List<ValueTuple<string, string, ClickyInkGroup>> list3 = new List<ValueTuple<string, string, ClickyInkGroup>>();
      for (int m = 0; m < list.Count; m++)
      {
        ClickyInkDashboardBoard.Section section2 = list[m];
        if (section2.Groups.Count != 0)
        {
          List<ClickyInkFrontPagePlanner.Piece> list4 = new List<ClickyInkFrontPagePlanner.Piece>();
          foreach (ClickyInkGroup clickyInkGroup in section2.Groups)
          {
            ClickyInkLiveNote.Block block = clickyInkGroup.Blocks[0];
            if (clickyInkGroup.Kind == ClickyInkDocument.Kind.Table && clickyInkGroup.Blocks.Count == 1)
            {
              if (Math.Max(block.Source.Columns.Count, block.Source.Items.Select<ClickyInkDocument.Item, int>((ClickyInkDocument.Item i) => i.Cells.Count).DefaultIfEmpty(0).Max()) >= 4 || block.Items.Count >= 6)
              {
                list3.Add(new ValueTuple<string, string, ClickyInkGroup>(section2.Id, ClickyInkParser.Emphasis.Strip(section2.Name.Current), clickyInkGroup));
                continue;
              }
            }
            if (clickyInkGroup.Blocks.Count == 1 && ClickyInkFrontPagePlanner.IsListKind(clickyInkGroup.Kind) && block.Items.Count >= 4)
            {
              List<List<ClickyInkLiveNote.Item>> list5 = new List<List<ClickyInkLiveNote.Item>>();
              for (int j = 0; j < block.Items.Count; j += 2)
              {
                list5.Add(block.Items.Skip<ClickyInkLiveNote.Item>(j).Take<ClickyInkLiveNote.Item>(2).ToList<ClickyInkLiveNote.Item>());
              }
              HashSet<string> hashSet = block.Items.Select<ClickyInkLiveNote.Item, string>((ClickyInkLiveNote.Item i) => i.Id).ToHashSet<string>();
              for (int k = 0; k < list5.Count; k++)
              {
                List<ClickyInkLiveNote.Item> list6 = list5[k];
                ClickyInkLiveNote.Block block2 = block.Clone();
                block2.Items = list6;
                if (k > 0)
                {
                  block2.Words = ClickyInkLiveNote.Words.Empty;
                }
                bool flag = k == list5.Count - 1;
                HashSet<string> hashSet2;
                if (!flag)
                {
                  hashSet2 = new HashSet<string>();
                }
                else
                {
                  hashSet2 = hashSet.Except<string>(list6.Select<ClickyInkLiveNote.Item, string>((ClickyInkLiveNote.Item i) => i.Id)).ToHashSet<string>();
                }
                HashSet<string> hashSet3 = hashSet2;
                List<ClickyInkFrontPagePlanner.Piece> list7 = list4;
                string id = clickyInkGroup.Id;
                ClickyInkFrontPagePlanner.<>c__DisplayClass16_0 CS$<>8__locals2 = CS$<>8__locals1;
                ClickyInkGroup clickyInkGroup2 = clickyInkGroup.<Clone>$();
                int num = 1;
                List<ClickyInkLiveNote.Block> list8 = new List<ClickyInkLiveNote.Block>(num);
                CollectionsMarshal.SetCount<ClickyInkLiveNote.Block>(list8, num);
                Span<ClickyInkLiveNote.Block> span = CollectionsMarshal.AsSpan<ClickyInkLiveNote.Block>(list8);
                int num2 = 0;
                *span[num2] = block2;
                clickyInkGroup2.Blocks = list8;
                ClickyInkFrontPagePlanner.Piece piece = new ClickyInkFrontPagePlanner.Piece(id, CS$<>8__locals2.<Make>g__Height|0(clickyInkGroup2));
                piece.ItemIDs = list6.Select<ClickyInkLiveNote.Item, string>((ClickyInkLiveNote.Item i) => i.Id).ToList<string>();
                piece.TakesOtherItems = flag;
                piece.OtherClaimedItemIDs = hashSet3;
                piece.Part = k;
                list7.Add(piece);
              }
            }
            else
            {
              list4.Add(new ClickyInkFrontPagePlanner.Piece(clickyInkGroup.Id, CS$<>8__locals1.<Make>g__Height|0(clickyInkGroup)));
            }
          }
          dictionary[section2.Id] = section2.Id;
          if (list4.Count != 0)
          {
            list2.Add(new ClickyInkFrontPagePlanner.Story(section2.Id, ClickyInkParser.Emphasis.Strip(section2.Name.Current), m == 0 && list2.Count == 0, list4));
          }
        }
      }
      ClickyInkDashboardBoard.Section move = CS$<>8__locals1.board.Move;
      List<ClickyInkGroup> list9 = ((move != null) ? move.Groups : null) ?? new List<ClickyInkGroup>();
      double num3 = ((list9.Count == 0) ? 0.0 : (list9.Sum<ClickyInkGroup>(new Func<ClickyInkGroup, double>(CS$<>8__locals1.<Make>g__Height|0)) + 44.0 + 32.0 - 12.0));
      ClickyInkFrontPagePlanner.Plan plan = null;
      CS$<>8__locals1.statBlocks = ClickyInkFrontPagePlanner.StatBlocks(CS$<>8__locals1.board, CS$<>8__locals1.board.Sections, CS$<>8__locals1.statGroupIDs);
      for (int l = 2; l <= 5; l++)
      {
        IEnumerable<ValueTuple<string, string, ClickyInkGroup>> enumerable = list3;
        Func<ValueTuple<string, string, ClickyInkGroup>, ValueTuple<string, string, string, Func<double, double>>> func;
        if ((func = CS$<>8__locals1.<>9__8) == null)
        {
          func = (CS$<>8__locals1.<>9__8 = ([TupleElementNames(new string[] { "StoryID", "Name", "Group" })] ValueTuple<string, string, ClickyInkGroup> table) => new ValueTuple<string, string, string, Func<double, double>>(table.Item1, table.Item2, table.Item3.Id, (double width) => CS$<>8__locals1.measure.Table(table.Item3, width) + 44.0 + 12.0));
        }
        List<ValueTuple<string, string, string, Func<double, double>>> list10 = enumerable.Select<ValueTuple<string, string, ClickyInkGroup>, ValueTuple<string, string, string, Func<double, double>>>(func).ToList<ValueTuple<string, string, string, Func<double, double>>>();
        int count = CS$<>8__locals1.statCount;
        int num4 = l;
        List<ClickyInkFrontPagePlanner.Story> list11 = list2;
        List<ValueTuple<string, string, string, Func<double, double>>> list12 = list10;
        ClickyInkDashboardBoard board = CS$<>8__locals1.board;
        int statCount = CS$<>8__locals1.statCount;
        double num5 = num3;
        double contentWidth = CS$<>8__locals1.measure.ContentWidth;
        Func<double, double> func2;
        if ((func2 = CS$<>8__locals1.<>9__9) == null)
        {
          func2 = (CS$<>8__locals1.<>9__9 = (double width) => CS$<>8__locals1.measure.Headline(CS$<>8__locals1.board, width, CS$<>8__locals1.isMission));
        }
        ClickyInkFrontPagePlanner.Plan plan2 = ClickyInkFrontPagePlanner.Layout(num4, variant, list11, list12, board, statCount, num5, contentWidth, func2, delegate(double width)
        {
          if (!CS$<>8__locals1.isMission)
          {
            return CS$<>8__locals1.measure.Tiles(CS$<>8__locals1.statBlocks, width) + 18.0;
          }
          return (double)((count + ClickyInkFrontPagePlanner.MissionTilesPerRow(count, width) - 1) / ClickyInkFrontPagePlanner.MissionTilesPerRow(count, width)) * (CS$<>8__locals1.measure.MissionTileHeight(CS$<>8__locals1.statBlocks.Select<ClickyInkLiveNote.Block, ClickyInkDocument.Block>((ClickyInkLiveNote.Block b) => b.Source).ToList<ClickyInkDocument.Block>()) + 14.0) - 14.0;
        }).<Clone>$();
        plan2.StatGroupIDs = CS$<>8__locals1.statGroupIDs;
        plan2.StoryOfPage = dictionary;
        plan = plan2;
        if (plan2.Fits)
        {
          break;
        }
      }
      return plan;
    }

    // Token: 0x060014CA RID: 5322 RVA: 0x003B07D4 File Offset: 0x003B07D4
    private static List<ClickyInkLiveNote.Block> StatBlocks(ClickyInkDashboardBoard board, IEnumerable<ClickyInkDashboardBoard.Section> sections, HashSet<string> ids)
    {
      return board.Strip.Concat<ClickyInkLiveNote.Block>((from g in sections.SelectMany<ClickyInkDashboardBoard.Section, ClickyInkGroup>((ClickyInkDashboardBoard.Section s) => s.Groups)
        where ids.Contains(g.Id)
        select g).SelectMany<ClickyInkGroup, ClickyInkLiveNote.Block>((ClickyInkGroup g) => g.Blocks)).Take<ClickyInkLiveNote.Block>(8).ToList<ClickyInkLiveNote.Block>();
    }

    // Token: 0x060014CB RID: 5323 RVA: 0x003B0860 File Offset: 0x003B0860
    private static double MaxY(Rect rect)
    {
      return rect.Y + rect.Height;
    }

    // Token: 0x060014CC RID: 5324 RVA: 0x003B0874 File Offset: 0x003B0874
    private unsafe static ClickyInkFrontPagePlanner.Plan Layout(int columns, ClickyInkFrontPageVariant variant, List<ClickyInkFrontPagePlanner.Story> stories, [TupleElementNames(new string[] { "StoryID", "Name", "GroupID", "Height" })] [Nullable(new byte[] { 1, 0, 1, 1, 1, 1 })] List<ValueTuple<string, string, string, Func<double, double>>> bandTables, ClickyInkDashboardBoard board, int statCount, double moveHeight, double contentWidth, Func<double, double> measureHeadline, Func<double, double> measureStats)
    {
      ClickyInkFrontPagePlanner.<>c__DisplayClass19_0 CS$<>8__locals1 = new ClickyInkFrontPagePlanner.<>c__DisplayClass19_0();
      CS$<>8__locals1.columns = columns;
      CS$<>8__locals1.stories = stories;
      CS$<>8__locals1.isMission = variant == ClickyInkFrontPageVariant.MissionControl;
      CS$<>8__locals1.columnWidth = contentWidth + (CS$<>8__locals1.isMission ? 32.0 : 0.0);
      CS$<>8__locals1.gutter = (double)(CS$<>8__locals1.isMission ? 18 : 30);
      CS$<>8__locals1.margin = (double)(CS$<>8__locals1.isMission ? 24 : 34);
      double num = (double)CS$<>8__locals1.columns * CS$<>8__locals1.columnWidth + (double)(CS$<>8__locals1.columns - 1) * CS$<>8__locals1.gutter;
      double num2 = num + 2.0 * CS$<>8__locals1.margin;
      double num3 = num2 * ClickyInkFrontPagePlanner.SpreadSize.Height / ClickyInkFrontPagePlanner.SpreadSize.Width;
      CS$<>8__locals1.storyGap = (double)(CS$<>8__locals1.isMission ? 16 : 14);
      double num4 = (double)(CS$<>8__locals1.isMission ? 62 : 120);
      Rect rect = new Rect(CS$<>8__locals1.margin, (double)(CS$<>8__locals1.isMission ? 20 : 22), num, num4);
      CS$<>8__locals1.y = ClickyInkFrontPagePlanner.MaxY(rect) + 16.0;
      bool flag = moveHeight > 0.0;
      bool flag2 = flag && CS$<>8__locals1.columns == 2;
      int num5 = ((flag && !flag2) ? (CS$<>8__locals1.columns - 1) : CS$<>8__locals1.columns);
      double num6 = (double)num5 * CS$<>8__locals1.columnWidth + (double)(num5 - 1) * CS$<>8__locals1.gutter;
      double num7 = ((board.Answer != null || board.Quiet != null) ? measureHeadline(num6) : 0.0);
      Rect rect2 = new Rect(CS$<>8__locals1.margin, CS$<>8__locals1.y, num6, Math.Max(0.0, num7));
      CS$<>8__locals1.y = ClickyInkFrontPagePlanner.MaxY(rect2) + (double)((num7 > 0.0) ? (CS$<>8__locals1.isMission ? 16 : 18) : 0);
      Rect? rect3 = null;
      if (statCount > 0)
      {
        double num8 = measureStats(num6);
        rect3 = new Rect?(new Rect(CS$<>8__locals1.margin, CS$<>8__locals1.y, num6, Math.Max(0.0, num8)));
        CS$<>8__locals1.y += num8 + (double)(CS$<>8__locals1.isMission ? 16 : 14);
      }
      List<ClickyInkFrontPagePlanner.Band> list = new List<ClickyInkFrontPagePlanner.Band>();
      foreach (ValueTuple<string, string, string, Func<double, double>> valueTuple in bandTables)
      {
        double num9 = valueTuple.Item4(num6);
        list.Add(new ClickyInkFrontPagePlanner.Band(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, new Rect(CS$<>8__locals1.margin, CS$<>8__locals1.y, num6, Math.Max(0.0, num9))));
        CS$<>8__locals1.y += num9 + (double)(CS$<>8__locals1.isMission ? 16 : 10);
      }
      Rect? rect4 = (flag ? new Rect?(new Rect(CS$<>8__locals1.<Layout>g__ColumnX|0(CS$<>8__locals1.columns - 1), flag2 ? (CS$<>8__locals1.y + 6.0) : rect2.Y, CS$<>8__locals1.columnWidth, moveHeight)) : null);
      double num10 = (double)((board.Sources.Count == 0) ? 0 : 30);
      Rect rect5 = new Rect(CS$<>8__locals1.margin, num3 - 18.0 - Math.Max(num10, 22.0), num, Math.Max(num10, 22.0));
      CS$<>8__locals1.limit = rect5.Y - 10.0;
      CS$<>8__locals1.tops = (from _ in Enumerable.Range(0, CS$<>8__locals1.columns)
        select CS$<>8__locals1.y + (double)(CS$<>8__locals1.isMission ? 0 : 6)).ToArray<double>();
      if (rect4 != null)
      {
        Rect valueOrDefault = rect4.GetValueOrDefault();
        CS$<>8__locals1.tops[CS$<>8__locals1.columns - 1] = ClickyInkFrontPagePlanner.MaxY(valueOrDefault) + CS$<>8__locals1.storyGap + 4.0;
      }
      CS$<>8__locals1.columnHeight = CS$<>8__locals1.limit - CS$<>8__locals1.tops.Min();
      CS$<>8__locals1.storyByID = new Dictionary<string, ClickyInkFrontPagePlanner.Story>();
      foreach (ClickyInkFrontPagePlanner.Story story in CS$<>8__locals1.stories)
      {
        CS$<>8__locals1.storyByID.TryAdd(story.Id, story);
      }
      ClickyInkFrontPagePlanner.<>c__DisplayClass19_0 CS$<>8__locals2 = CS$<>8__locals1;
      List<ClickyInkFrontPagePlanner.Story> list2;
      if (CS$<>8__locals1.stories.Count <= 0 || !CS$<>8__locals1.stories[0].IsLead)
      {
        list2 = new List<ClickyInkFrontPagePlanner.Story>();
      }
      else
      {
        (list2 = new List<ClickyInkFrontPagePlanner.Story>()).Add(CS$<>8__locals1.stories[0]);
      }
      CS$<>8__locals2.lead = list2;
      List<ClickyInkFrontPagePlanner.Story> list3 = CS$<>8__locals1.stories.Skip<ClickyInkFrontPagePlanner.Story>(CS$<>8__locals1.lead.Count).ToList<ClickyInkFrontPagePlanner.Story>();
      CS$<>8__locals1.orders = new List<List<ClickyInkFrontPagePlanner.Story>>();
      if (list3.Count <= 6)
      {
        CS$<>8__locals1.<Layout>g__Permute|5(new List<ClickyInkFrontPagePlanner.Story>(), list3);
      }
      else
      {
        ClickyInkFrontPagePlanner.<>c__DisplayClass19_0 CS$<>8__locals3 = CS$<>8__locals1;
        int num11 = 2;
        List<List<ClickyInkFrontPagePlanner.Story>> list4 = new List<List<ClickyInkFrontPagePlanner.Story>>(num11);
        CollectionsMarshal.SetCount<List<ClickyInkFrontPagePlanner.Story>>(list4, num11);
        Span<List<ClickyInkFrontPagePlanner.Story>> span = CollectionsMarshal.AsSpan<List<ClickyInkFrontPagePlanner.Story>>(list4);
        int num12 = 0;
        *span[num12] = CS$<>8__locals1.stories;
        num12++;
        ref List<ClickyInkFrontPagePlanner.Story> ptr = ref span[num12];
        List<ClickyInkFrontPagePlanner.Story> list5 = new List<ClickyInkFrontPagePlanner.Story>();
        list5.AddRange(CS$<>8__locals1.lead);
        list5.AddRange(list3.OrderByDescending<ClickyInkFrontPagePlanner.Story, int>((ClickyInkFrontPagePlanner.Story s) => s.Pieces.Count));
        ptr = list5;
        CS$<>8__locals3.orders = list4;
      }
      ValueTuple<ValueTuple<List<ClickyInkFrontPagePlanner.Column>, List<ClickyInkFrontPagePlanner.Story>>, double>? valueTuple2 = null;
      foreach (List<ClickyInkFrontPagePlanner.Story> list6 in CS$<>8__locals1.orders)
      {
        ValueTuple<ValueTuple<List<ClickyInkFrontPagePlanner.Column>, List<ClickyInkFrontPagePlanner.Story>>, double>? valueTuple3 = CS$<>8__locals1.<Layout>g__Best|4(list6);
        if (valueTuple3 != null)
        {
          ValueTuple<ValueTuple<List<ClickyInkFrontPagePlanner.Column>, List<ClickyInkFrontPagePlanner.Story>>, double> valueOrDefault2 = valueTuple3.GetValueOrDefault();
          if (valueOrDefault2.Item2 < ((valueTuple2 != null) ? valueTuple2.GetValueOrDefault().Item2 : double.PositiveInfinity))
          {
            valueTuple2 = new ValueTuple<ValueTuple<List<ClickyInkFrontPagePlanner.Column>, List<ClickyInkFrontPagePlanner.Story>>, double>?(valueOrDefault2);
          }
        }
      }
      ValueTuple<List<ClickyInkFrontPagePlanner.Column>, List<ClickyInkFrontPagePlanner.Story>> valueTuple4 = ((valueTuple2 != null) ? valueTuple2.GetValueOrDefault().Item1 : (CS$<>8__locals1.<Layout>g__Pour|3(CS$<>8__locals1.stories, CS$<>8__locals1.limit, true) ?? new ValueTuple<List<ClickyInkFrontPagePlanner.Column>, List<ClickyInkFrontPagePlanner.Story>>(new List<ClickyInkFrontPagePlanner.Column>(), CS$<>8__locals1.stories)));
      List<ClickyInkFrontPagePlanner.Column> item = valueTuple4.Item1;
      foreach (ClickyInkFrontPagePlanner.Column column in item)
      {
        int num13 = column.Chunks.Sum<ClickyInkFrontPagePlanner.Chunk>((ClickyInkFrontPagePlanner.Chunk c) => c.Pieces.Count) + column.Chunks.Count;
        if (num13 > 0 && valueTuple2 != null)
        {
          column.ExtraGap = Math.Max(0.0, Math.Min(36.0, (CS$<>8__locals1.limit - column.Bottom) / (double)num13));
          column.Bottom += column.ExtraGap * (double)num13;
        }
      }
      List<Rect> list7 = new List<Rect>();
      for (int i = 0; i < Math.Max(0, CS$<>8__locals1.columns - 1); i++)
      {
        ClickyInkFrontPagePlanner.Column column2 = ((i < item.Count) ? item[i] : null);
        ClickyInkFrontPagePlanner.Column column3 = ((i + 1 < item.Count) ? item[i + 1] : null);
        double num14 = Math.Min((column2 != null) ? column2.Top : CS$<>8__locals1.limit, (column3 != null) ? column3.Top : CS$<>8__locals1.limit) - 2.0;
        double num15 = Math.Min((column2 != null) ? column2.Bottom : num14, (column3 != null) ? column3.Bottom : num14);
        if (i == CS$<>8__locals1.columns - 2 && rect4 != null)
        {
          Rect valueOrDefault3 = rect4.GetValueOrDefault();
          num15 = Math.Max(num15, Math.Min(ClickyInkFrontPagePlanner.MaxY(valueOrDefault3), (column2 != null) ? column2.Bottom : ClickyInkFrontPagePlanner.MaxY(valueOrDefault3)));
        }
        if (num15 - num14 > 40.0)
        {
          list7.Add(new Rect(CS$<>8__locals1.<Layout>g__ColumnX|0(i + 1) - CS$<>8__locals1.gutter / 2.0, num14, 1.0, num15 - num14));
        }
      }
      HashSet<string> hashSet = (from p in item.SelectMany<ClickyInkFrontPagePlanner.Column, ClickyInkFrontPagePlanner.Chunk>((ClickyInkFrontPagePlanner.Column c) => c.Chunks).SelectMany<ClickyInkFrontPagePlanner.Chunk, ClickyInkFrontPagePlanner.Piece>((ClickyInkFrontPagePlanner.Chunk c) => c.Pieces)
        select p.GroupID).Concat<string>(list.Select<ClickyInkFrontPagePlanner.Band, string>((ClickyInkFrontPagePlanner.Band b) => b.GroupID)).ToHashSet<string>();
      ClickyInkFrontPagePlanner.Plan plan = new ClickyInkFrontPagePlanner.Plan();
      plan.Variant = variant;
      plan.Canvas = new Size(num2, num3);
      plan.Columns = CS$<>8__locals1.columns;
      plan.ColumnWidth = CS$<>8__locals1.columnWidth;
      plan.InnerWidth = num;
      plan.Masthead = rect;
      plan.Headline = rect2;
      plan.Stats = rect3;
      plan.Bands = list;
      plan.Move = rect4;
      plan.Flow = item;
      plan.Rules = list7;
      plan.Footer = rect5;
      plan.StoryGap = CS$<>8__locals1.storyGap;
      plan.PlacedGroupIDs = hashSet;
      string text;
      if (CS$<>8__locals1.stories.Count <= 0)
      {
        text = "";
      }
      else
      {
        List<ClickyInkFrontPagePlanner.Story> stories2 = CS$<>8__locals1.stories;
        text = stories2[stories2.Count - 1].Id;
      }
      plan.LastStoryID = text;
      plan.OverflowNames = valueTuple4.Item2.Select<ClickyInkFrontPagePlanner.Story, string>((ClickyInkFrontPagePlanner.Story s) => s.Name).ToList<string>();
      bool flag3;
      if (valueTuple2 != null)
      {
        if (rect4 != null)
        {
          Rect valueOrDefault4 = rect4.GetValueOrDefault();
          flag3 = ClickyInkFrontPagePlanner.MaxY(valueOrDefault4) <= CS$<>8__locals1.limit;
        }
        else
        {
          flag3 = true;
        }
      }
      else
      {
        flag3 = false;
      }
      plan.Fits = flag3;
      plan.LeadFallbackTitle = null;
      return plan;
    }

    // Token: 0x04000911 RID: 2321
    public const int MaximumStats = 8;

    // Token: 0x04000912 RID: 2322
    public const double PieceGap = 12.0;

    // Token: 0x04000913 RID: 2323
    public const double Kicker = 44.0;

    // Token: 0x04000914 RID: 2324
    public const double ContinuedLine = 24.0;

    // Token: 0x04000915 RID: 2325
    public const double PanelHeader = 30.0;

    // Token: 0x04000916 RID: 2326
    public const double BoxInset = 16.0;

    // Token: 0x04000917 RID: 2327
    public static readonly Size SpreadSize = new Size(900f, 560f);

    // Token: 0x020004DB RID: 1243
    [Nullable(0)]
    public sealed class Piece : IEquatable<ClickyInkFrontPagePlanner.Piece>
    {
      // Token: 0x06003097 RID: 12439 RVA: 0x00436B94 File Offset: 0x00436B94
      public Piece(string GroupID, double Height)
      {
        this.GroupID = GroupID;
        this.Height = Height;
        this.OtherClaimedItemIDs = new HashSet<string>();
        base..ctor();
      }

      // Token: 0x170008B1 RID: 2225
      // (get) Token: 0x06003098 RID: 12440 RVA: 0x00436BB8 File Offset: 0x00436BB8
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkFrontPagePlanner.Piece);
        }
      }

      // Token: 0x170008B2 RID: 2226
      // (get) Token: 0x06003099 RID: 12441 RVA: 0x00436BC4 File Offset: 0x00436BC4
      // (set) Token: 0x0600309A RID: 12442 RVA: 0x00436BCC File Offset: 0x00436BCC
      public string GroupID { get; set; }

      // Token: 0x170008B3 RID: 2227
      // (get) Token: 0x0600309B RID: 12443 RVA: 0x00436BD8 File Offset: 0x00436BD8
      // (set) Token: 0x0600309C RID: 12444 RVA: 0x00436BE0 File Offset: 0x00436BE0
      public double Height { get; set; }

      // Token: 0x170008B4 RID: 2228
      // (get) Token: 0x0600309D RID: 12445 RVA: 0x00436BEC File Offset: 0x00436BEC
      // (set) Token: 0x0600309E RID: 12446 RVA: 0x00436BF4 File Offset: 0x00436BF4
      [Nullable(new byte[] { 2, 1 })]
      public List<string> ItemIDs
      {
        [return: Nullable(new byte[] { 2, 1 })]
        get;
        [param: Nullable(new byte[] { 2, 1 })]
        set;
      }

      // Token: 0x170008B5 RID: 2229
      // (get) Token: 0x0600309F RID: 12447 RVA: 0x00436C00 File Offset: 0x00436C00
      // (set) Token: 0x060030A0 RID: 12448 RVA: 0x00436C08 File Offset: 0x00436C08
      public bool TakesOtherItems { get; set; }

      // Token: 0x170008B6 RID: 2230
      // (get) Token: 0x060030A1 RID: 12449 RVA: 0x00436C14 File Offset: 0x00436C14
      // (set) Token: 0x060030A2 RID: 12450 RVA: 0x00436C1C File Offset: 0x00436C1C
      public HashSet<string> OtherClaimedItemIDs { get; set; }

      // Token: 0x170008B7 RID: 2231
      // (get) Token: 0x060030A3 RID: 12451 RVA: 0x00436C28 File Offset: 0x00436C28
      // (set) Token: 0x060030A4 RID: 12452 RVA: 0x00436C30 File Offset: 0x00436C30
      public int Part { get; set; }

      // Token: 0x060030A5 RID: 12453 RVA: 0x00436C3C File Offset: 0x00436C3C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Piece");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060030A6 RID: 12454 RVA: 0x00436C88 File Offset: 0x00436C88
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("GroupID = ");
        builder.Append(this.GroupID);
        builder.Append(", Height = ");
        builder.Append(this.Height.ToString());
        builder.Append(", ItemIDs = ");
        builder.Append(this.ItemIDs);
        builder.Append(", TakesOtherItems = ");
        builder.Append(this.TakesOtherItems.ToString());
        builder.Append(", OtherClaimedItemIDs = ");
        builder.Append(this.OtherClaimedItemIDs);
        builder.Append(", Part = ");
        builder.Append(this.Part.ToString());
        return true;
      }

      // Token: 0x060030A7 RID: 12455 RVA: 0x00436D5C File Offset: 0x00436D5C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkFrontPagePlanner.Piece left, ClickyInkFrontPagePlanner.Piece right)
      {
        return !(left == right);
      }

      // Token: 0x060030A8 RID: 12456 RVA: 0x00436D68 File Offset: 0x00436D68
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkFrontPagePlanner.Piece left, ClickyInkFrontPagePlanner.Piece right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060030A9 RID: 12457 RVA: 0x00436D7C File Offset: 0x00436D7C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GroupID>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Height>k__BackingField)) * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(this.<ItemIDs>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<TakesOtherItems>k__BackingField)) * -1521134295 + EqualityComparer<HashSet<string>>.Default.GetHashCode(this.<OtherClaimedItemIDs>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Part>k__BackingField);
      }

      // Token: 0x060030AA RID: 12458 RVA: 0x00436E24 File Offset: 0x00436E24
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkFrontPagePlanner.Piece);
      }

      // Token: 0x060030AB RID: 12459 RVA: 0x00436E34 File Offset: 0x00436E34
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkFrontPagePlanner.Piece other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<GroupID>k__BackingField, other.<GroupID>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Height>k__BackingField, other.<Height>k__BackingField) && EqualityComparer<List<string>>.Default.Equals(this.<ItemIDs>k__BackingField, other.<ItemIDs>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<TakesOtherItems>k__BackingField, other.<TakesOtherItems>k__BackingField) && EqualityComparer<HashSet<string>>.Default.Equals(this.<OtherClaimedItemIDs>k__BackingField, other.<OtherClaimedItemIDs>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Part>k__BackingField, other.<Part>k__BackingField));
      }

      // Token: 0x060030AD RID: 12461 RVA: 0x00436F00 File Offset: 0x00436F00
      [CompilerGenerated]
      private Piece(ClickyInkFrontPagePlanner.Piece original)
      {
        this.GroupID = original.<GroupID>k__BackingField;
        this.Height = original.<Height>k__BackingField;
        this.ItemIDs = original.<ItemIDs>k__BackingField;
        this.TakesOtherItems = original.<TakesOtherItems>k__BackingField;
        this.OtherClaimedItemIDs = original.<OtherClaimedItemIDs>k__BackingField;
        this.Part = original.<Part>k__BackingField;
      }

      // Token: 0x060030AE RID: 12462 RVA: 0x00436F5C File Offset: 0x00436F5C
      [CompilerGenerated]
      public void Deconstruct(out string GroupID, out double Height)
      {
        GroupID = this.GroupID;
        Height = this.Height;
      }
    }

    // Token: 0x020004DC RID: 1244
    [Nullable(0)]
    public sealed class Chunk
    {
      // Token: 0x060030AF RID: 12463 RVA: 0x00436F70 File Offset: 0x00436F70
      public Chunk(string storyID, bool isLead, bool isContinuation, List<ClickyInkFrontPagePlanner.Piece> pieces, bool isLastOfStory = false)
      {
      }

      // Token: 0x170008B8 RID: 2232
      // (get) Token: 0x060030B0 RID: 12464 RVA: 0x00436FA0 File Offset: 0x00436FA0
      public string StoryID { get; } = storyID;

      // Token: 0x170008B9 RID: 2233
      // (get) Token: 0x060030B1 RID: 12465 RVA: 0x00436FA8 File Offset: 0x00436FA8
      public bool IsLead { get; } = isLead;

      // Token: 0x170008BA RID: 2234
      // (get) Token: 0x060030B2 RID: 12466 RVA: 0x00436FB0 File Offset: 0x00436FB0
      public bool IsContinuation { get; } = isContinuation;

      // Token: 0x170008BB RID: 2235
      // (get) Token: 0x060030B3 RID: 12467 RVA: 0x00436FB8 File Offset: 0x00436FB8
      // (set) Token: 0x060030B4 RID: 12468 RVA: 0x00436FC0 File Offset: 0x00436FC0
      public bool IsLastOfStory { get; set; } = isLastOfStory;

      // Token: 0x170008BC RID: 2236
      // (get) Token: 0x060030B5 RID: 12469 RVA: 0x00436FCC File Offset: 0x00436FCC
      public List<ClickyInkFrontPagePlanner.Piece> Pieces { get; } = pieces;
    }

    // Token: 0x020004DD RID: 1245
    [Nullable(0)]
    public sealed class Column
    {
      // Token: 0x060030B6 RID: 12470 RVA: 0x00436FD4 File Offset: 0x00436FD4
      public Column(double x, double top, double bottom)
      {
      }

      // Token: 0x170008BD RID: 2237
      // (get) Token: 0x060030B7 RID: 12471 RVA: 0x00436FFC File Offset: 0x00436FFC
      public double X { get; } = x;

      // Token: 0x170008BE RID: 2238
      // (get) Token: 0x060030B8 RID: 12472 RVA: 0x00437004 File Offset: 0x00437004
      public double Top { get; } = top;

      // Token: 0x170008BF RID: 2239
      // (get) Token: 0x060030B9 RID: 12473 RVA: 0x0043700C File Offset: 0x0043700C
      public List<ClickyInkFrontPagePlanner.Chunk> Chunks { get; } = new List<ClickyInkFrontPagePlanner.Chunk>();

      // Token: 0x170008C0 RID: 2240
      // (get) Token: 0x060030BA RID: 12474 RVA: 0x00437014 File Offset: 0x00437014
      // (set) Token: 0x060030BB RID: 12475 RVA: 0x0043701C File Offset: 0x0043701C
      public double Bottom { get; set; } = bottom;

      // Token: 0x170008C1 RID: 2241
      // (get) Token: 0x060030BC RID: 12476 RVA: 0x00437028 File Offset: 0x00437028
      // (set) Token: 0x060030BD RID: 12477 RVA: 0x00437030 File Offset: 0x00437030
      public double ExtraGap { get; set; }
    }

    // Token: 0x020004DE RID: 1246
    [Nullable(0)]
    public sealed class Band : IEquatable<ClickyInkFrontPagePlanner.Band>
    {
      // Token: 0x060030BE RID: 12478 RVA: 0x0043703C File Offset: 0x0043703C
      public Band(string StoryID, string Name, string GroupID, Rect Frame)
      {
        this.StoryID = StoryID;
        this.Name = Name;
        this.GroupID = GroupID;
        this.Frame = Frame;
        base..ctor();
      }

      // Token: 0x170008C2 RID: 2242
      // (get) Token: 0x060030BF RID: 12479 RVA: 0x00437064 File Offset: 0x00437064
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkFrontPagePlanner.Band);
        }
      }

      // Token: 0x170008C3 RID: 2243
      // (get) Token: 0x060030C0 RID: 12480 RVA: 0x00437070 File Offset: 0x00437070
      // (set) Token: 0x060030C1 RID: 12481 RVA: 0x00437078 File Offset: 0x00437078
      public string StoryID { get; set; }

      // Token: 0x170008C4 RID: 2244
      // (get) Token: 0x060030C2 RID: 12482 RVA: 0x00437084 File Offset: 0x00437084
      // (set) Token: 0x060030C3 RID: 12483 RVA: 0x0043708C File Offset: 0x0043708C
      public string Name { get; set; }

      // Token: 0x170008C5 RID: 2245
      // (get) Token: 0x060030C4 RID: 12484 RVA: 0x00437098 File Offset: 0x00437098
      // (set) Token: 0x060030C5 RID: 12485 RVA: 0x004370A0 File Offset: 0x004370A0
      public string GroupID { get; set; }

      // Token: 0x170008C6 RID: 2246
      // (get) Token: 0x060030C6 RID: 12486 RVA: 0x004370AC File Offset: 0x004370AC
      // (set) Token: 0x060030C7 RID: 12487 RVA: 0x004370B4 File Offset: 0x004370B4
      public Rect Frame { get; set; }

      // Token: 0x060030C8 RID: 12488 RVA: 0x004370C0 File Offset: 0x004370C0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Band");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060030C9 RID: 12489 RVA: 0x0043710C File Offset: 0x0043710C
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("StoryID = ");
        builder.Append(this.StoryID);
        builder.Append(", Name = ");
        builder.Append(this.Name);
        builder.Append(", GroupID = ");
        builder.Append(this.GroupID);
        builder.Append(", Frame = ");
        builder.Append(this.Frame.ToString());
        return true;
      }

      // Token: 0x060030CA RID: 12490 RVA: 0x00437194 File Offset: 0x00437194
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkFrontPagePlanner.Band left, ClickyInkFrontPagePlanner.Band right)
      {
        return !(left == right);
      }

      // Token: 0x060030CB RID: 12491 RVA: 0x004371A0 File Offset: 0x004371A0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkFrontPagePlanner.Band left, ClickyInkFrontPagePlanner.Band right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060030CC RID: 12492 RVA: 0x004371B4 File Offset: 0x004371B4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<StoryID>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<GroupID>k__BackingField)) * -1521134295 + EqualityComparer<Rect>.Default.GetHashCode(this.<Frame>k__BackingField);
      }

      // Token: 0x060030CD RID: 12493 RVA: 0x00437230 File Offset: 0x00437230
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkFrontPagePlanner.Band);
      }

      // Token: 0x060030CE RID: 12494 RVA: 0x00437240 File Offset: 0x00437240
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkFrontPagePlanner.Band other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<StoryID>k__BackingField, other.<StoryID>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<GroupID>k__BackingField, other.<GroupID>k__BackingField) && EqualityComparer<Rect>.Default.Equals(this.<Frame>k__BackingField, other.<Frame>k__BackingField));
      }

      // Token: 0x060030D0 RID: 12496 RVA: 0x004372D4 File Offset: 0x004372D4
      [CompilerGenerated]
      private Band(ClickyInkFrontPagePlanner.Band original)
      {
        this.StoryID = original.<StoryID>k__BackingField;
        this.Name = original.<Name>k__BackingField;
        this.GroupID = original.<GroupID>k__BackingField;
        this.Frame = original.<Frame>k__BackingField;
      }

      // Token: 0x060030D1 RID: 12497 RVA: 0x0043730C File Offset: 0x0043730C
      [CompilerGenerated]
      public void Deconstruct(out string StoryID, out string Name, out string GroupID, out Rect Frame)
      {
        StoryID = this.StoryID;
        Name = this.Name;
        GroupID = this.GroupID;
        Frame = this.Frame;
      }
    }

    // Token: 0x020004DF RID: 1247
    [Nullable(0)]
    public sealed class Plan : IEquatable<ClickyInkFrontPagePlanner.Plan>
    {
      // Token: 0x170008C7 RID: 2247
      // (get) Token: 0x060030D2 RID: 12498 RVA: 0x00437334 File Offset: 0x00437334
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkFrontPagePlanner.Plan);
        }
      }

      // Token: 0x170008C8 RID: 2248
      // (get) Token: 0x060030D3 RID: 12499 RVA: 0x00437340 File Offset: 0x00437340
      // (set) Token: 0x060030D4 RID: 12500 RVA: 0x00437348 File Offset: 0x00437348
      public ClickyInkFrontPageVariant Variant { get; set; }

      // Token: 0x170008C9 RID: 2249
      // (get) Token: 0x060030D5 RID: 12501 RVA: 0x00437354 File Offset: 0x00437354
      // (set) Token: 0x060030D6 RID: 12502 RVA: 0x0043735C File Offset: 0x0043735C
      public Size Canvas { get; set; }

      // Token: 0x170008CA RID: 2250
      // (get) Token: 0x060030D7 RID: 12503 RVA: 0x00437368 File Offset: 0x00437368
      // (set) Token: 0x060030D8 RID: 12504 RVA: 0x00437370 File Offset: 0x00437370
      public int Columns { get; set; }

      // Token: 0x170008CB RID: 2251
      // (get) Token: 0x060030D9 RID: 12505 RVA: 0x0043737C File Offset: 0x0043737C
      // (set) Token: 0x060030DA RID: 12506 RVA: 0x00437384 File Offset: 0x00437384
      public double ColumnWidth { get; set; }

      // Token: 0x170008CC RID: 2252
      // (get) Token: 0x060030DB RID: 12507 RVA: 0x00437390 File Offset: 0x00437390
      // (set) Token: 0x060030DC RID: 12508 RVA: 0x00437398 File Offset: 0x00437398
      public double InnerWidth { get; set; }

      // Token: 0x170008CD RID: 2253
      // (get) Token: 0x060030DD RID: 12509 RVA: 0x004373A4 File Offset: 0x004373A4
      // (set) Token: 0x060030DE RID: 12510 RVA: 0x004373AC File Offset: 0x004373AC
      public Rect Masthead { get; set; }

      // Token: 0x170008CE RID: 2254
      // (get) Token: 0x060030DF RID: 12511 RVA: 0x004373B8 File Offset: 0x004373B8
      // (set) Token: 0x060030E0 RID: 12512 RVA: 0x004373C0 File Offset: 0x004373C0
      public Rect Headline { get; set; }

      // Token: 0x170008CF RID: 2255
      // (get) Token: 0x060030E1 RID: 12513 RVA: 0x004373CC File Offset: 0x004373CC
      // (set) Token: 0x060030E2 RID: 12514 RVA: 0x004373D4 File Offset: 0x004373D4
      public Rect? Stats { get; set; }

      // Token: 0x170008D0 RID: 2256
      // (get) Token: 0x060030E3 RID: 12515 RVA: 0x004373E0 File Offset: 0x004373E0
      // (set) Token: 0x060030E4 RID: 12516 RVA: 0x004373E8 File Offset: 0x004373E8
      public List<ClickyInkFrontPagePlanner.Band> Bands { get; set; }

      // Token: 0x170008D1 RID: 2257
      // (get) Token: 0x060030E5 RID: 12517 RVA: 0x004373F4 File Offset: 0x004373F4
      // (set) Token: 0x060030E6 RID: 12518 RVA: 0x004373FC File Offset: 0x004373FC
      public Rect? Move { get; set; }

      // Token: 0x170008D2 RID: 2258
      // (get) Token: 0x060030E7 RID: 12519 RVA: 0x00437408 File Offset: 0x00437408
      // (set) Token: 0x060030E8 RID: 12520 RVA: 0x00437410 File Offset: 0x00437410
      public List<ClickyInkFrontPagePlanner.Column> Flow { get; set; }

      // Token: 0x170008D3 RID: 2259
      // (get) Token: 0x060030E9 RID: 12521 RVA: 0x0043741C File Offset: 0x0043741C
      // (set) Token: 0x060030EA RID: 12522 RVA: 0x00437424 File Offset: 0x00437424
      public List<Rect> Rules { get; set; }

      // Token: 0x170008D4 RID: 2260
      // (get) Token: 0x060030EB RID: 12523 RVA: 0x00437430 File Offset: 0x00437430
      // (set) Token: 0x060030EC RID: 12524 RVA: 0x00437438 File Offset: 0x00437438
      public Rect Footer { get; set; }

      // Token: 0x170008D5 RID: 2261
      // (get) Token: 0x060030ED RID: 12525 RVA: 0x00437444 File Offset: 0x00437444
      // (set) Token: 0x060030EE RID: 12526 RVA: 0x0043744C File Offset: 0x0043744C
      public double StoryGap { get; set; }

      // Token: 0x170008D6 RID: 2262
      // (get) Token: 0x060030EF RID: 12527 RVA: 0x00437458 File Offset: 0x00437458
      // (set) Token: 0x060030F0 RID: 12528 RVA: 0x00437460 File Offset: 0x00437460
      public HashSet<string> PlacedGroupIDs { get; set; }

      // Token: 0x170008D7 RID: 2263
      // (get) Token: 0x060030F1 RID: 12529 RVA: 0x0043746C File Offset: 0x0043746C
      // (set) Token: 0x060030F2 RID: 12530 RVA: 0x00437474 File Offset: 0x00437474
      public HashSet<string> StatGroupIDs { get; set; }

      // Token: 0x170008D8 RID: 2264
      // (get) Token: 0x060030F3 RID: 12531 RVA: 0x00437480 File Offset: 0x00437480
      // (set) Token: 0x060030F4 RID: 12532 RVA: 0x00437488 File Offset: 0x00437488
      public Dictionary<string, string> StoryOfPage { get; set; }

      // Token: 0x170008D9 RID: 2265
      // (get) Token: 0x060030F5 RID: 12533 RVA: 0x00437494 File Offset: 0x00437494
      // (set) Token: 0x060030F6 RID: 12534 RVA: 0x0043749C File Offset: 0x0043749C
      public string LastStoryID { get; set; }

      // Token: 0x170008DA RID: 2266
      // (get) Token: 0x060030F7 RID: 12535 RVA: 0x004374A8 File Offset: 0x004374A8
      // (set) Token: 0x060030F8 RID: 12536 RVA: 0x004374B0 File Offset: 0x004374B0
      public List<string> OverflowNames { get; set; }

      // Token: 0x170008DB RID: 2267
      // (get) Token: 0x060030F9 RID: 12537 RVA: 0x004374BC File Offset: 0x004374BC
      // (set) Token: 0x060030FA RID: 12538 RVA: 0x004374C4 File Offset: 0x004374C4
      public bool Fits { get; set; }

      // Token: 0x170008DC RID: 2268
      // (get) Token: 0x060030FB RID: 12539 RVA: 0x004374D0 File Offset: 0x004374D0
      // (set) Token: 0x060030FC RID: 12540 RVA: 0x004374D8 File Offset: 0x004374D8
      [Nullable(2)]
      public string LeadFallbackTitle
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x060030FD RID: 12541 RVA: 0x004374E4 File Offset: 0x004374E4
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

      // Token: 0x060030FE RID: 12542 RVA: 0x00437530 File Offset: 0x00437530
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Variant = ");
        builder.Append(this.Variant.ToString());
        builder.Append(", Canvas = ");
        builder.Append(this.Canvas.ToString());
        builder.Append(", Columns = ");
        builder.Append(this.Columns.ToString());
        builder.Append(", ColumnWidth = ");
        builder.Append(this.ColumnWidth.ToString());
        builder.Append(", InnerWidth = ");
        builder.Append(this.InnerWidth.ToString());
        builder.Append(", Masthead = ");
        builder.Append(this.Masthead.ToString());
        builder.Append(", Headline = ");
        builder.Append(this.Headline.ToString());
        builder.Append(", Stats = ");
        builder.Append(this.Stats.ToString());
        builder.Append(", Bands = ");
        builder.Append(this.Bands);
        builder.Append(", Move = ");
        builder.Append(this.Move.ToString());
        builder.Append(", Flow = ");
        builder.Append(this.Flow);
        builder.Append(", Rules = ");
        builder.Append(this.Rules);
        builder.Append(", Footer = ");
        builder.Append(this.Footer.ToString());
        builder.Append(", StoryGap = ");
        builder.Append(this.StoryGap.ToString());
        builder.Append(", PlacedGroupIDs = ");
        builder.Append(this.PlacedGroupIDs);
        builder.Append(", StatGroupIDs = ");
        builder.Append(this.StatGroupIDs);
        builder.Append(", StoryOfPage = ");
        builder.Append(this.StoryOfPage);
        builder.Append(", LastStoryID = ");
        builder.Append(this.LastStoryID);
        builder.Append(", OverflowNames = ");
        builder.Append(this.OverflowNames);
        builder.Append(", Fits = ");
        builder.Append(this.Fits.ToString());
        builder.Append(", LeadFallbackTitle = ");
        builder.Append(this.LeadFallbackTitle);
        return true;
      }

      // Token: 0x060030FF RID: 12543 RVA: 0x00437800 File Offset: 0x00437800
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkFrontPagePlanner.Plan left, ClickyInkFrontPagePlanner.Plan right)
      {
        return !(left == right);
      }

      // Token: 0x06003100 RID: 12544 RVA: 0x0043780C File Offset: 0x0043780C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkFrontPagePlanner.Plan left, ClickyInkFrontPagePlanner.Plan right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003101 RID: 12545 RVA: 0x00437820 File Offset: 0x00437820
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((((((((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ClickyInkFrontPageVariant>.Default.GetHashCode(this.<Variant>k__BackingField)) * -1521134295 + EqualityComparer<Size>.Default.GetHashCode(this.<Canvas>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Columns>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<ColumnWidth>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<InnerWidth>k__BackingField)) * -1521134295 + EqualityComparer<Rect>.Default.GetHashCode(this.<Masthead>k__BackingField)) * -1521134295 + EqualityComparer<Rect>.Default.GetHashCode(this.<Headline>k__BackingField)) * -1521134295 + EqualityComparer<Rect?>.Default.GetHashCode(this.<Stats>k__BackingField)) * -1521134295 + EqualityComparer<List<ClickyInkFrontPagePlanner.Band>>.Default.GetHashCode(this.<Bands>k__BackingField)) * -1521134295 + EqualityComparer<Rect?>.Default.GetHashCode(this.<Move>k__BackingField)) * -1521134295 + EqualityComparer<List<ClickyInkFrontPagePlanner.Column>>.Default.GetHashCode(this.<Flow>k__BackingField)) * -1521134295 + EqualityComparer<List<Rect>>.Default.GetHashCode(this.<Rules>k__BackingField)) * -1521134295 + EqualityComparer<Rect>.Default.GetHashCode(this.<Footer>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<StoryGap>k__BackingField)) * -1521134295 + EqualityComparer<HashSet<string>>.Default.GetHashCode(this.<PlacedGroupIDs>k__BackingField)) * -1521134295 + EqualityComparer<HashSet<string>>.Default.GetHashCode(this.<StatGroupIDs>k__BackingField)) * -1521134295 + EqualityComparer<Dictionary<string, string>>.Default.GetHashCode(this.<StoryOfPage>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LastStoryID>k__BackingField)) * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(this.<OverflowNames>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Fits>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LeadFallbackTitle>k__BackingField);
      }

      // Token: 0x06003102 RID: 12546 RVA: 0x00437A20 File Offset: 0x00437A20
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkFrontPagePlanner.Plan);
      }

      // Token: 0x06003103 RID: 12547 RVA: 0x00437A30 File Offset: 0x00437A30
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkFrontPagePlanner.Plan other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ClickyInkFrontPageVariant>.Default.Equals(this.<Variant>k__BackingField, other.<Variant>k__BackingField) && EqualityComparer<Size>.Default.Equals(this.<Canvas>k__BackingField, other.<Canvas>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Columns>k__BackingField, other.<Columns>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<ColumnWidth>k__BackingField, other.<ColumnWidth>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<InnerWidth>k__BackingField, other.<InnerWidth>k__BackingField) && EqualityComparer<Rect>.Default.Equals(this.<Masthead>k__BackingField, other.<Masthead>k__BackingField) && EqualityComparer<Rect>.Default.Equals(this.<Headline>k__BackingField, other.<Headline>k__BackingField) && EqualityComparer<Rect?>.Default.Equals(this.<Stats>k__BackingField, other.<Stats>k__BackingField) && EqualityComparer<List<ClickyInkFrontPagePlanner.Band>>.Default.Equals(this.<Bands>k__BackingField, other.<Bands>k__BackingField) && EqualityComparer<Rect?>.Default.Equals(this.<Move>k__BackingField, other.<Move>k__BackingField) && EqualityComparer<List<ClickyInkFrontPagePlanner.Column>>.Default.Equals(this.<Flow>k__BackingField, other.<Flow>k__BackingField) && EqualityComparer<List<Rect>>.Default.Equals(this.<Rules>k__BackingField, other.<Rules>k__BackingField) && EqualityComparer<Rect>.Default.Equals(this.<Footer>k__BackingField, other.<Footer>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<StoryGap>k__BackingField, other.<StoryGap>k__BackingField) && EqualityComparer<HashSet<string>>.Default.Equals(this.<PlacedGroupIDs>k__BackingField, other.<PlacedGroupIDs>k__BackingField) && EqualityComparer<HashSet<string>>.Default.Equals(this.<StatGroupIDs>k__BackingField, other.<StatGroupIDs>k__BackingField) && EqualityComparer<Dictionary<string, string>>.Default.Equals(this.<StoryOfPage>k__BackingField, other.<StoryOfPage>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LastStoryID>k__BackingField, other.<LastStoryID>k__BackingField) && EqualityComparer<List<string>>.Default.Equals(this.<OverflowNames>k__BackingField, other.<OverflowNames>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Fits>k__BackingField, other.<Fits>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LeadFallbackTitle>k__BackingField, other.<LeadFallbackTitle>k__BackingField));
      }

      // Token: 0x06003105 RID: 12549 RVA: 0x00437C90 File Offset: 0x00437C90
      [CompilerGenerated]
      private Plan(ClickyInkFrontPagePlanner.Plan original)
      {
        this.Variant = original.<Variant>k__BackingField;
        this.Canvas = original.<Canvas>k__BackingField;
        this.Columns = original.<Columns>k__BackingField;
        this.ColumnWidth = original.<ColumnWidth>k__BackingField;
        this.InnerWidth = original.<InnerWidth>k__BackingField;
        this.Masthead = original.<Masthead>k__BackingField;
        this.Headline = original.<Headline>k__BackingField;
        this.Stats = original.<Stats>k__BackingField;
        this.Bands = original.<Bands>k__BackingField;
        this.Move = original.<Move>k__BackingField;
        this.Flow = original.<Flow>k__BackingField;
        this.Rules = original.<Rules>k__BackingField;
        this.Footer = original.<Footer>k__BackingField;
        this.StoryGap = original.<StoryGap>k__BackingField;
        this.PlacedGroupIDs = original.<PlacedGroupIDs>k__BackingField;
        this.StatGroupIDs = original.<StatGroupIDs>k__BackingField;
        this.StoryOfPage = original.<StoryOfPage>k__BackingField;
        this.LastStoryID = original.<LastStoryID>k__BackingField;
        this.OverflowNames = original.<OverflowNames>k__BackingField;
        this.Fits = original.<Fits>k__BackingField;
        this.LeadFallbackTitle = original.<LeadFallbackTitle>k__BackingField;
      }

      // Token: 0x06003106 RID: 12550 RVA: 0x00437DA0 File Offset: 0x00437DA0
      public Plan()
      {
        this.Bands = new List<ClickyInkFrontPagePlanner.Band>();
        this.Flow = new List<ClickyInkFrontPagePlanner.Column>();
        this.Rules = new List<Rect>();
        this.PlacedGroupIDs = new HashSet<string>();
        this.StatGroupIDs = new HashSet<string>();
        this.StoryOfPage = new Dictionary<string, string>();
        this.LastStoryID = "";
        this.OverflowNames = new List<string>();
        this.Fits = true;
        base..ctor();
      }
    }

    // Token: 0x020004E0 RID: 1248
    [Nullable(0)]
    private sealed class Story : IEquatable<ClickyInkFrontPagePlanner.Story>
    {
      // Token: 0x06003107 RID: 12551 RVA: 0x00437E14 File Offset: 0x00437E14
      public Story(string Id, string Name, bool IsLead, List<ClickyInkFrontPagePlanner.Piece> Pieces)
      {
        this.Id = Id;
        this.Name = Name;
        this.IsLead = IsLead;
        this.Pieces = Pieces;
        base..ctor();
      }

      // Token: 0x170008DD RID: 2269
      // (get) Token: 0x06003108 RID: 12552 RVA: 0x00437E3C File Offset: 0x00437E3C
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkFrontPagePlanner.Story);
        }
      }

      // Token: 0x170008DE RID: 2270
      // (get) Token: 0x06003109 RID: 12553 RVA: 0x00437E48 File Offset: 0x00437E48
      // (set) Token: 0x0600310A RID: 12554 RVA: 0x00437E50 File Offset: 0x00437E50
      public string Id { get; set; }

      // Token: 0x170008DF RID: 2271
      // (get) Token: 0x0600310B RID: 12555 RVA: 0x00437E5C File Offset: 0x00437E5C
      // (set) Token: 0x0600310C RID: 12556 RVA: 0x00437E64 File Offset: 0x00437E64
      public string Name { get; set; }

      // Token: 0x170008E0 RID: 2272
      // (get) Token: 0x0600310D RID: 12557 RVA: 0x00437E70 File Offset: 0x00437E70
      // (set) Token: 0x0600310E RID: 12558 RVA: 0x00437E78 File Offset: 0x00437E78
      public bool IsLead { get; set; }

      // Token: 0x170008E1 RID: 2273
      // (get) Token: 0x0600310F RID: 12559 RVA: 0x00437E84 File Offset: 0x00437E84
      // (set) Token: 0x06003110 RID: 12560 RVA: 0x00437E8C File Offset: 0x00437E8C
      public List<ClickyInkFrontPagePlanner.Piece> Pieces { get; set; }

      // Token: 0x06003111 RID: 12561 RVA: 0x00437E98 File Offset: 0x00437E98
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Story");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003112 RID: 12562 RVA: 0x00437EE4 File Offset: 0x00437EE4
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Id = ");
        builder.Append(this.Id);
        builder.Append(", Name = ");
        builder.Append(this.Name);
        builder.Append(", IsLead = ");
        builder.Append(this.IsLead.ToString());
        builder.Append(", Pieces = ");
        builder.Append(this.Pieces);
        return true;
      }

      // Token: 0x06003113 RID: 12563 RVA: 0x00437F6C File Offset: 0x00437F6C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkFrontPagePlanner.Story left, ClickyInkFrontPagePlanner.Story right)
      {
        return !(left == right);
      }

      // Token: 0x06003114 RID: 12564 RVA: 0x00437F78 File Offset: 0x00437F78
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkFrontPagePlanner.Story left, ClickyInkFrontPagePlanner.Story right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003115 RID: 12565 RVA: 0x00437F8C File Offset: 0x00437F8C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsLead>k__BackingField)) * -1521134295 + EqualityComparer<List<ClickyInkFrontPagePlanner.Piece>>.Default.GetHashCode(this.<Pieces>k__BackingField);
      }

      // Token: 0x06003116 RID: 12566 RVA: 0x00438008 File Offset: 0x00438008
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkFrontPagePlanner.Story);
      }

      // Token: 0x06003117 RID: 12567 RVA: 0x00438018 File Offset: 0x00438018
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkFrontPagePlanner.Story other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsLead>k__BackingField, other.<IsLead>k__BackingField) && EqualityComparer<List<ClickyInkFrontPagePlanner.Piece>>.Default.Equals(this.<Pieces>k__BackingField, other.<Pieces>k__BackingField));
      }

      // Token: 0x06003119 RID: 12569 RVA: 0x004380AC File Offset: 0x004380AC
      [CompilerGenerated]
      private Story(ClickyInkFrontPagePlanner.Story original)
      {
        this.Id = original.<Id>k__BackingField;
        this.Name = original.<Name>k__BackingField;
        this.IsLead = original.<IsLead>k__BackingField;
        this.Pieces = original.<Pieces>k__BackingField;
      }

      // Token: 0x0600311A RID: 12570 RVA: 0x004380E4 File Offset: 0x004380E4
      [CompilerGenerated]
      public void Deconstruct(out string Id, out string Name, out bool IsLead, out List<ClickyInkFrontPagePlanner.Piece> Pieces)
      {
        Id = this.Id;
        Name = this.Name;
        IsLead = this.IsLead;
        Pieces = this.Pieces;
      }
    }
  }
}
