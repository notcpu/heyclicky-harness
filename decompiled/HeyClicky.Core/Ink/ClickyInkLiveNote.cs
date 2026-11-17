using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200015D RID: 349
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkLiveNote : IEquatable<ClickyInkLiveNote>
  {
    // Token: 0x170004E8 RID: 1256
    // (get) Token: 0x0600152A RID: 5418 RVA: 0x003B3D1C File Offset: 0x003B3D1C
    // (set) Token: 0x0600152B RID: 5419 RVA: 0x003B3D24 File Offset: 0x003B3D24
    [Nullable(2)]
    public ClickyInkLiveNote.Words Title
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170004E9 RID: 1257
    // (get) Token: 0x0600152C RID: 5420 RVA: 0x003B3D30 File Offset: 0x003B3D30
    // (set) Token: 0x0600152D RID: 5421 RVA: 0x003B3D38 File Offset: 0x003B3D38
    public List<ClickyInkLiveNote.Page> Pages { get; set; } = new List<ClickyInkLiveNote.Page>();

    // Token: 0x170004EA RID: 1258
    // (get) Token: 0x0600152E RID: 5422 RVA: 0x003B3D44 File Offset: 0x003B3D44
    // (set) Token: 0x0600152F RID: 5423 RVA: 0x003B3D4C File Offset: 0x003B3D4C
    public int VersionCount { get; set; }

    // Token: 0x170004EB RID: 1259
    // (get) Token: 0x06001530 RID: 5424 RVA: 0x003B3D58 File Offset: 0x003B3D58
    // (set) Token: 0x06001531 RID: 5425 RVA: 0x003B3D60 File Offset: 0x003B3D60
    public double PenFreeAt { get; set; } = double.NegativeInfinity;

    // Token: 0x170004EC RID: 1260
    // (get) Token: 0x06001532 RID: 5426 RVA: 0x003B3D6C File Offset: 0x003B3D6C
    // (set) Token: 0x06001533 RID: 5427 RVA: 0x003B3D74 File Offset: 0x003B3D74
    public List<double> EditStarts { get; set; } = new List<double>();

    // Token: 0x170004ED RID: 1261
    // (get) Token: 0x06001534 RID: 5428 RVA: 0x003B3D80 File Offset: 0x003B3D80
    // (set) Token: 0x06001535 RID: 5429 RVA: 0x003B3D88 File Offset: 0x003B3D88
    public double? LastVersionAt { get; set; }

    // Token: 0x170004EE RID: 1262
    // (get) Token: 0x06001536 RID: 5430 RVA: 0x003B3D94 File Offset: 0x003B3D94
    // (set) Token: 0x06001537 RID: 5431 RVA: 0x003B3D9C File Offset: 0x003B3D9C
    public bool LastVersionWasEdit { get; set; }

    // Token: 0x170004EF RID: 1263
    // (get) Token: 0x06001538 RID: 5432 RVA: 0x003B3DA8 File Offset: 0x003B3DA8
    // (set) Token: 0x06001539 RID: 5433 RVA: 0x003B3DB0 File Offset: 0x003B3DB0
    public bool LastVersionWasPartial { get; set; }

    // Token: 0x170004F0 RID: 1264
    // (get) Token: 0x0600153A RID: 5434 RVA: 0x003B3DBC File Offset: 0x003B3DBC
    // (set) Token: 0x0600153B RID: 5435 RVA: 0x003B3DC4 File Offset: 0x003B3DC4
    public List<double> VersionArrivals { get; set; } = new List<double>();

    // Token: 0x170004F1 RID: 1265
    // (get) Token: 0x0600153C RID: 5436 RVA: 0x003B3DD0 File Offset: 0x003B3DD0
    // (set) Token: 0x0600153D RID: 5437 RVA: 0x003B3DD8 File Offset: 0x003B3DD8
    public List<ClickyInkLiveNote.FocusKnown> FocusKnownSince { get; set; } = new List<ClickyInkLiveNote.FocusKnown>();

    // Token: 0x170004F2 RID: 1266
    // (get) Token: 0x0600153E RID: 5438 RVA: 0x003B3DE4 File Offset: 0x003B3DE4
    // (set) Token: 0x0600153F RID: 5439 RVA: 0x003B3DEC File Offset: 0x003B3DEC
    public int? PenLastPage { get; set; }

    // Token: 0x170004F3 RID: 1267
    // (get) Token: 0x06001540 RID: 5440 RVA: 0x003B3DF8 File Offset: 0x003B3DF8
    // (set) Token: 0x06001541 RID: 5441 RVA: 0x003B3E00 File Offset: 0x003B3E00
    public int? PenLastBlock { get; set; }

    // Token: 0x06001542 RID: 5442 RVA: 0x003B3E0C File Offset: 0x003B3E0C
    public ClickyInkLiveNote Clone()
    {
      return new ClickyInkLiveNote
      {
        Title = this.Title,
        Pages = this.Pages.ToList<ClickyInkLiveNote.Page>(),
        VersionCount = this.VersionCount,
        PenFreeAt = this.PenFreeAt,
        EditStarts = this.EditStarts.ToList<double>(),
        LastVersionAt = this.LastVersionAt,
        LastVersionWasEdit = this.LastVersionWasEdit,
        LastVersionWasPartial = this.LastVersionWasPartial,
        VersionArrivals = this.VersionArrivals.ToList<double>(),
        FocusKnownSince = this.FocusKnownSince.ToList<ClickyInkLiveNote.FocusKnown>(),
        PenLastPage = this.PenLastPage,
        PenLastBlock = this.PenLastBlock
      };
    }

    // Token: 0x06001543 RID: 5443 RVA: 0x003B3EC4 File Offset: 0x003B3EC4
    public int? LatestEdit(double clock)
    {
      int num = this.EditStarts.Count<double>((double s) => s <= clock);
      if (num != 0)
      {
        return new int?(num);
      }
      return null;
    }

    // Token: 0x06001544 RID: 5444 RVA: 0x003B3F0C File Offset: 0x003B3F0C
    [NullableContext(2)]
    public bool Equals(ClickyInkLiveNote other)
    {
      if (this != other)
      {
        if (other != null && object.Equals(this.Title, other.Title) && this.VersionCount == other.VersionCount && this.PenFreeAt.Equals(other.PenFreeAt) && this.EditStarts.SequenceEqual<double>(other.EditStarts))
        {
          double? lastVersionAt = this.LastVersionAt;
          double? lastVersionAt2 = other.LastVersionAt;
          if (((lastVersionAt.GetValueOrDefault() == lastVersionAt2.GetValueOrDefault()) & (lastVersionAt != null == (lastVersionAt2 != null))) && this.LastVersionWasEdit == other.LastVersionWasEdit && this.LastVersionWasPartial == other.LastVersionWasPartial && this.VersionArrivals.SequenceEqual<double>(other.VersionArrivals) && this.FocusKnownSince.SequenceEqual<ClickyInkLiveNote.FocusKnown>(other.FocusKnownSince))
          {
            int? num = this.PenLastPage;
            int? num2 = other.PenLastPage;
            if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
            {
              num2 = this.PenLastBlock;
              num = other.PenLastBlock;
              if ((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null)))
              {
                return this.Pages.SequenceEqual<ClickyInkLiveNote.Page>(other.Pages);
              }
            }
          }
        }
        return false;
      }
      return true;
    }

    // Token: 0x06001545 RID: 5445 RVA: 0x003B4074 File Offset: 0x003B4074
    [NullableContext(2)]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkLiveNote);
    }

    // Token: 0x06001546 RID: 5446 RVA: 0x003B4084 File Offset: 0x003B4084
    public override int GetHashCode()
    {
      return HashCode.Combine<int, int, double>(this.VersionCount, this.Pages.Count, this.PenFreeAt);
    }

    // Token: 0x06001547 RID: 5447 RVA: 0x003B40A4 File Offset: 0x003B40A4
    [NullableContext(2)]
    public static bool operator ==(ClickyInkLiveNote a, ClickyInkLiveNote b)
    {
      if (a != null)
      {
        return a.Equals(b);
      }
      return b == null;
    }

    // Token: 0x06001548 RID: 5448 RVA: 0x003B40B8 File Offset: 0x003B40B8
    [NullableContext(2)]
    public static bool operator !=(ClickyInkLiveNote a, ClickyInkLiveNote b)
    {
      return !(a == b);
    }

    // Token: 0x06001549 RID: 5449 RVA: 0x003B40C4 File Offset: 0x003B40C4
    public void Apply(ClickyInkDocument document, double time, bool isEdit = false, bool? startsNewEdit = null, bool isPartial = false)
    {
      double num = Math.Max(0.0, this.PenFreeAt - time);
      double num2 = ((this.VersionCount > 0 && num <= 0.0 && (!isPartial || !this.LastVersionWasPartial)) ? 0.35 : 0.0);
      double num3 = Math.Max(time + num2, this.PenFreeAt);
      ClickyInkLiveNote clickyInkLiveNote = this.Clone();
      clickyInkLiveNote.Apply(document, time, isEdit, startsNewEdit, isPartial, 1.0, num2);
      double num4 = Math.Max(0.0, clickyInkLiveNote.PenFreeAt - num3);
      double num5 = ClickyInkPen.Haste(num, num4);
      if (isPartial && num5 <= 1.0 && num < 0.25)
      {
        double? lastVersionAt = this.LastVersionAt;
        if (lastVersionAt != null)
        {
          double valueOrDefault = lastVersionAt.GetValueOrDefault();
          if (this.LastVersionWasPartial && num4 > 0.0)
          {
            double num6 = Math.Min(1.2, Math.Max(0.15, time - valueOrDefault));
            num5 = Math.Min(1.0, Math.Max(0.6, num4 / num6));
          }
        }
      }
      this.Apply(document, time, isEdit, startsNewEdit, isPartial, num5, num2);
    }

    // Token: 0x0600154A RID: 5450 RVA: 0x003B4214 File Offset: 0x003B4214
    private void Apply(ClickyInkDocument document, double time, bool isEdit, bool? startsNewEdit, bool isPartial, double haste, double travel)
    {
      ClickyInkLiveNote.<>c__DisplayClass63_0 CS$<>8__locals1 = new ClickyInkLiveNote.<>c__DisplayClass63_0();
      double num = Math.Max(time + travel, this.PenFreeAt);
      bool flag;
      if (startsNewEdit != null)
      {
        bool valueOrDefault = startsNewEdit.GetValueOrDefault();
        flag = !valueOrDefault;
      }
      else
      {
        if (this.LastVersionWasEdit)
        {
          double? lastVersionAt = this.LastVersionAt;
          if (lastVersionAt != null)
          {
            double valueOrDefault2 = lastVersionAt.GetValueOrDefault();
            flag = time - valueOrDefault2 < 600.0;
            goto IL_0063;
          }
        }
        flag = false;
      }
      IL_0063:
      bool flag2 = flag;
      if (isEdit && (!flag2 || this.EditStarts.Count <= 0))
      {
        this.EditStarts.Add(num);
      }
      this.LastVersionAt = new double?(time);
      this.LastVersionWasEdit = isEdit;
      this.LastVersionWasPartial = isPartial;
      this.VersionArrivals.Add(time);
      this.FocusKnownSince.Add(new ClickyInkLiveNote.FocusKnown(num, time));
      CS$<>8__locals1.scheduler = new Scheduler(num, time, isEdit ? new int?(this.EditStarts.Count) : null, haste)
      {
        IsPartial = isPartial
      };
      int? num2 = this.PenLastPage;
      if (num2 != null)
      {
        int valueOrDefault3 = num2.GetValueOrDefault();
        num2 = this.PenLastBlock;
        if (num2 != null)
        {
          int valueOrDefault4 = num2.GetValueOrDefault();
          CS$<>8__locals1.scheduler.LastWorked = new ValueTuple<int, int>?(new ValueTuple<int, int>(valueOrDefault3, valueOrDefault4));
        }
      }
      int num3 = document.Pages.Count - 1;
      string title = document.Title;
      if (title != null)
      {
        if (this.Title == null)
        {
          CS$<>8__locals1.scheduler.Pause(0.3);
        }
        this.Title = CS$<>8__locals1.scheduler.Rewrite(this.Title, title, 24.0);
      }
      CS$<>8__locals1.oldPages = this.Pages;
      List<ValueTuple<string, ClickyInkLiveNote.Block>> list = new List<ValueTuple<string, ClickyInkLiveNote.Block>>();
      foreach (ClickyInkLiveNote.Page page6 in CS$<>8__locals1.oldPages)
      {
        foreach (ClickyInkLiveNote.Block block7 in page6.Blocks)
        {
          if (block7.GoneAt == null && block7.VanishesAt == null)
          {
            list.Add(new ValueTuple<string, ClickyInkLiveNote.Block>(page6.Id, block7));
          }
        }
      }
      CS$<>8__locals1.usedOld = new HashSet<string>();
      CS$<>8__locals1.usedPages = new HashSet<string>();
      List<ClickyInkLiveNote.Page> list2 = new List<ClickyInkLiveNote.Page>();
      int pageIndex;
      int pageIndex2;
      for (pageIndex = 0; pageIndex < document.Pages.Count; pageIndex = pageIndex2 + 1)
      {
        ClickyInkDocument.Page page = document.Pages[pageIndex];
        ClickyInkLiveNote.Page page2 = CS$<>8__locals1.oldPages.FirstOrDefault<ClickyInkLiveNote.Page>((ClickyInkLiveNote.Page p) => !CS$<>8__locals1.usedPages.Contains(p.Id) && ClickyInkMatch.Similarity(p.Name.Current, page.Name) > 0.5) ?? CS$<>8__locals1.oldPages.Skip<ClickyInkLiveNote.Page>(pageIndex).FirstOrDefault<ClickyInkLiveNote.Page>((ClickyInkLiveNote.Page p) => !CS$<>8__locals1.usedPages.Contains(p.Id) && pageIndex < CS$<>8__locals1.oldPages.Count && p.Id == CS$<>8__locals1.oldPages[pageIndex].Id);
        if (page2 != null)
        {
          CS$<>8__locals1.usedPages.Add(page2.Id);
        }
        string text;
        if ((text = ((page2 != null) ? page2.Id : null)) == null)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
          defaultInterpolatedStringHandler.AppendLiteral("p");
          defaultInterpolatedStringHandler.AppendFormatted<int>(this.VersionCount);
          defaultInterpolatedStringHandler.AppendLiteral("-");
          defaultInterpolatedStringHandler.AppendFormatted<int>(pageIndex);
          text = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        string text2 = text;
        CS$<>8__locals1.scheduler.FoldTime = new double?(CS$<>8__locals1.scheduler.Cursor);
        bool flag3 = ClickyInkLiveNote.IsSilentPageName(page.Name, pageIndex);
        ClickyInkLiveNote.Words words = CS$<>8__locals1.scheduler.Rewrite((page2 != null) ? page2.Name : null, page.Name, flag3 ? double.PositiveInfinity : 24.0);
        List<ClickyInkLiveNote.Block> list3 = new List<ClickyInkLiveNote.Block>();
        Dictionary<int, int> dictionary = new Dictionary<int, int>();
        for (int i = 0; i < page.Blocks.Count; i++)
        {
          ClickyInkDocument.Block block2 = page.Blocks[i];
          ValueTuple<int, double>? valueTuple = null;
          for (int j = 0; j < list.Count; j++)
          {
            ValueTuple<string, ClickyInkLiveNote.Block> valueTuple2 = list[j];
            if (!CS$<>8__locals1.usedOld.Contains(valueTuple2.Item2.Id) && ClickyInkMatch.SameFamily(valueTuple2.Item2.Source.Kind, block2.Kind))
            {
              double num4 = ClickyInkMatch.Similarity(ClickyInkMatch.Signature(valueTuple2.Item2.Source), ClickyInkMatch.Signature(block2));
              if (valueTuple2.Item1 == text2)
              {
                num4 += 0.08;
              }
              if (valueTuple2.Item2.Source.Kind == block2.Kind)
              {
                num4 += 0.04;
              }
              if (num4 > ((valueTuple != null) ? valueTuple.GetValueOrDefault().Item2 : 0.36))
              {
                valueTuple = new ValueTuple<int, double>?(new ValueTuple<int, double>(j, num4));
              }
            }
          }
          if (valueTuple != null)
          {
            ValueTuple<int, double> valueOrDefault5 = valueTuple.GetValueOrDefault();
            dictionary[i] = valueOrDefault5.Item1;
            CS$<>8__locals1.usedOld.Add(list[valueOrDefault5.Item1].Item2.Id);
          }
        }
        if (page2 != null)
        {
          for (int k = 0; k < page.Blocks.Count; k++)
          {
            ClickyInkDocument.Block newBlock = page.Blocks[k];
            if (!dictionary.ContainsKey(k) && !ClickyInkLiveNote.EntityKinds.Contains(newBlock.Kind))
            {
              int ordinal = page.Blocks.Take<ClickyInkDocument.Block>(k).Count<ClickyInkDocument.Block>((ClickyInkDocument.Block b) => b.Kind == newBlock.Kind);
              List<ClickyInkLiveNote.Block> sameKindOld = page2.Blocks.Where<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.GoneAt == null && b.VanishesAt == null && b.Source.Kind == newBlock.Kind).ToList<ClickyInkLiveNote.Block>();
              if (ordinal < sameKindOld.Count && !CS$<>8__locals1.usedOld.Contains(sameKindOld[ordinal].Id))
              {
                int num5 = list.FindIndex(([TupleElementNames(new string[] { "PageId", "Block" })] [Nullable(new byte[] { 0, 1, 1 })] ValueTuple<string, ClickyInkLiveNote.Block> o) => o.Item2.Id == sameKindOld[ordinal].Id);
                if (num5 >= 0)
                {
                  dictionary[k] = num5;
                  CS$<>8__locals1.usedOld.Add(sameKindOld[ordinal].Id);
                }
              }
            }
          }
        }
        for (int l = 0; l < page.Blocks.Count; l++)
        {
          ClickyInkDocument.Block block3 = page.Blocks[l];
          int num6;
          if (dictionary.TryGetValue(l, out num6))
          {
            ClickyInkLiveNote.Block item = list[num6].Item2;
            if (isPartial && pageIndex == num3 && l == page.Blocks.Count - 1 && ClickyInkMatch.IsStillCatchingUp(item.Source, block3))
            {
              list3.Add(item);
            }
            else
            {
              CS$<>8__locals1.scheduler.Position = new ValueTuple<int, int>?(new ValueTuple<int, int>(pageIndex, l));
              list3.Add(CS$<>8__locals1.scheduler.Update(item, block3));
            }
          }
          else
          {
            CS$<>8__locals1.scheduler.Position = new ValueTuple<int, int>?(new ValueTuple<int, int>(pageIndex, l));
            List<ClickyInkLiveNote.Block> list4 = list3;
            Scheduler scheduler = CS$<>8__locals1.scheduler;
            ClickyInkDocument.Block block4 = block3;
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 3);
            defaultInterpolatedStringHandler.AppendLiteral("b");
            defaultInterpolatedStringHandler.AppendFormatted<int>(this.VersionCount);
            defaultInterpolatedStringHandler.AppendLiteral("-");
            defaultInterpolatedStringHandler.AppendFormatted<int>(pageIndex);
            defaultInterpolatedStringHandler.AppendLiteral("-");
            defaultInterpolatedStringHandler.AppendFormatted<int>(l);
            list4.Add(scheduler.Write(block4, defaultInterpolatedStringHandler.ToStringAndClear()));
          }
        }
        if (page2 != null)
        {
          string text3 = null;
          foreach (ClickyInkLiveNote.Block block5 in page2.Blocks)
          {
            try
            {
              if (!CS$<>8__locals1.usedOld.Contains(block5.Id))
              {
                ClickyInkLiveNote.Block block6 = block5;
                if (isPartial && block5.GoneAt == null && block5.VanishesAt == null)
                {
                  CS$<>8__locals1.usedOld.Add(block5.Id);
                  int num7 = ClickyInkLiveNote.PositionAfter(list3, text3) ?? list3.Count;
                  list3.Insert(Math.Min(num7, list3.Count), block6);
                }
                else
                {
                  if (block5.GoneAt != null || block5.VanishesAt != null)
                  {
                    bool flag4;
                    if (CS$<>8__locals1.scheduler.Edit != null)
                    {
                      num2 = block5.GoneInEdit;
                      int? edit = CS$<>8__locals1.scheduler.Edit;
                      flag4 = (num2.GetValueOrDefault() == edit.GetValueOrDefault()) & (num2 != null == (edit != null));
                    }
                    else
                    {
                      flag4 = false;
                    }
                    bool flag5 = flag4;
                    if (block6.VanishesAt == null && !flag5)
                    {
                      block6 = block6.Clone();
                      block6.VanishesAt = new double?(CS$<>8__locals1.scheduler.Arrival);
                    }
                  }
                  else
                  {
                    CS$<>8__locals1.usedOld.Add(block5.Id);
                    block6 = CS$<>8__locals1.scheduler.Strike(block5);
                  }
                  int valueOrDefault6 = ClickyInkLiveNote.PositionAfter(list3, text3).GetValueOrDefault();
                  list3.Insert(Math.Min(valueOrDefault6, list3.Count), block6);
                }
              }
            }
            finally
            {
              text3 = block5.Id;
            }
          }
        }
        list2.Add(new ClickyInkLiveNote.Page(text2, words, page.IsBoard, list3, (page2 != null) ? page2.LandsAt : CS$<>8__locals1.scheduler.Cursor));
        pageIndex2 = pageIndex;
      }
      foreach (ClickyInkLiveNote.Page page3 in CS$<>8__locals1.oldPages)
      {
        if (!CS$<>8__locals1.usedPages.Contains(page3.Id))
        {
          ClickyInkLiveNote.Page page4 = page3.Clone();
          List<ClickyInkLiveNote.Block> blocks = page4.Blocks;
          Predicate<ClickyInkLiveNote.Block> predicate;
          if ((predicate = CS$<>8__locals1.<>9__5) == null)
          {
            predicate = (CS$<>8__locals1.<>9__5 = (ClickyInkLiveNote.Block b) => CS$<>8__locals1.usedOld.Contains(b.Id));
          }
          blocks.RemoveAll(predicate);
          if (page4.Blocks.Count != 0)
          {
            if (isPartial)
            {
              list2.Add(page4);
            }
            else
            {
              ClickyInkLiveNote.Page page5 = page4;
              IEnumerable<ClickyInkLiveNote.Block> blocks2 = page4.Blocks;
              Func<ClickyInkLiveNote.Block, ClickyInkLiveNote.Block> func;
              if ((func = CS$<>8__locals1.<>9__6) == null)
              {
                func = (CS$<>8__locals1.<>9__6 = delegate(ClickyInkLiveNote.Block block)
                {
                  ClickyInkLiveNote.Block block8 = block.Clone();
                  if (block8.GoneAt == null && !block8.IsPreview)
                  {
                    block8.GoneAt = new double?(CS$<>8__locals1.scheduler.Arrival);
                  }
                  ClickyInkLiveNote.Block block9 = block8;
                  double? vanishesAt = block9.VanishesAt;
                  double num8 = vanishesAt.GetValueOrDefault();
                  if (vanishesAt == null)
                  {
                    num8 = CS$<>8__locals1.scheduler.Arrival + 0.3;
                    ClickyInkLiveNote.Block block10 = block9;
                    double? num9 = new double?(num8);
                    block10.VanishesAt = num9;
                  }
                  return block8;
                });
              }
              page5.Blocks = blocks2.Select<ClickyInkLiveNote.Block, ClickyInkLiveNote.Block>(func).ToList<ClickyInkLiveNote.Block>();
              list2.Add(page4);
            }
          }
        }
      }
      this.Pages = list2;
      this.VersionCount++;
      this.PenFreeAt = CS$<>8__locals1.scheduler.Cursor;
      ValueTuple<int, int>? lastWorked = CS$<>8__locals1.scheduler.LastWorked;
      if (lastWorked != null)
      {
        ValueTuple<int, int> valueOrDefault7 = lastWorked.GetValueOrDefault();
        this.PenLastPage = new int?(valueOrDefault7.Item1);
        this.PenLastBlock = new int?(valueOrDefault7.Item2);
      }
    }

    // Token: 0x0600154B RID: 5451 RVA: 0x003B4F70 File Offset: 0x003B4F70
    private static int? PositionAfter(List<ClickyInkLiveNote.Block> blocks, [Nullable(2)] string previousId)
    {
      if (previousId == null)
      {
        return null;
      }
      int num = blocks.FindIndex((ClickyInkLiveNote.Block b) => b.Id == previousId);
      if (num >= 0)
      {
        return new int?(num + 1);
      }
      return null;
    }

    // Token: 0x0600154C RID: 5452 RVA: 0x003B4FC8 File Offset: 0x003B4FC8
    public void Finish(double time)
    {
      ClickyInkLiveNote.<>c__DisplayClass66_0 CS$<>8__locals1 = new ClickyInkLiveNote.<>c__DisplayClass66_0();
      CS$<>8__locals1.time = time;
      if (this.PenFreeAt <= CS$<>8__locals1.time)
      {
        return;
      }
      CS$<>8__locals1.factor = ClickyInkPen.AfterDoneHaste(this.PenFreeAt - CS$<>8__locals1.time);
      if (CS$<>8__locals1.factor <= 1.0)
      {
        return;
      }
      this.Title = ((this.Title == null) ? null : CS$<>8__locals1.<Finish>g__WarpWords|2(this.Title));
      this.Pages = this.Pages.Select<ClickyInkLiveNote.Page, ClickyInkLiveNote.Page>(delegate(ClickyInkLiveNote.Page page)
      {
        string id = page.Id;
        ClickyInkLiveNote.Words words = base.<Finish>g__WarpWords|2(page.Name);
        bool isBoard = page.IsBoard;
        IEnumerable<ClickyInkLiveNote.Block> blocks = page.Blocks;
        Func<ClickyInkLiveNote.Block, ClickyInkLiveNote.Block> func;
        if ((func = CS$<>8__locals1.<>9__10) == null)
        {
          func = (CS$<>8__locals1.<>9__10 = delegate(ClickyInkLiveNote.Block block)
          {
            ClickyInkLiveNote.Block block2 = block.Clone();
            block2.Stamps = base.<Finish>g__WarpStamps|3<ClickyInkDocument.Block>(block.Stamps);
            block2.Words = base.<Finish>g__WarpWords|2(block.Words);
            block2.LandsAt = base.<Finish>g__Warp|0(block.LandsAt);
            block2.PenArrivals = block.PenArrivals.Select<double, double>(new Func<double, double>(base.<Finish>g__Warp|0)).ToList<double>();
            double? num = block.GoneAt;
            double? num2;
            if (num != null)
            {
              double valueOrDefault = num.GetValueOrDefault();
              num2 = new double?(base.<Finish>g__Warp|0(valueOrDefault));
            }
            else
            {
              num2 = null;
            }
            block2.GoneAt = num2;
            num = block.VanishesAt;
            double? num3;
            if (num != null)
            {
              double valueOrDefault2 = num.GetValueOrDefault();
              num3 = new double?(base.<Finish>g__Warp|0(valueOrDefault2));
            }
            else
            {
              num3 = null;
            }
            block2.VanishesAt = num3;
            IEnumerable<ClickyInkLiveNote.Item> items = block.Items;
            Func<ClickyInkLiveNote.Item, ClickyInkLiveNote.Item> func2;
            if ((func2 = CS$<>8__locals1.<>9__11) == null)
            {
              func2 = (CS$<>8__locals1.<>9__11 = delegate(ClickyInkLiveNote.Item item)
              {
                ClickyInkLiveNote.Item item2 = item.Clone();
                item2.Stamps = base.<Finish>g__WarpStamps|3<ClickyInkDocument.Item>(item.Stamps);
                item2.Words = base.<Finish>g__WarpWords|2(item.Words);
                item2.Cells = item.Cells.Select<ClickyInkLiveNote.Words, ClickyInkLiveNote.Words>(new Func<ClickyInkLiveNote.Words, ClickyInkLiveNote.Words>(base.<Finish>g__WarpWords|2)).ToList<ClickyInkLiveNote.Words>();
                item2.Detail = ((item.Detail == null) ? null : base.<Finish>g__WarpWords|2(item.Detail));
                item2.LandsAt = base.<Finish>g__Warp|0(item.LandsAt);
                double? num4 = item.GoneAt;
                double? num5;
                if (num4 != null)
                {
                  double valueOrDefault3 = num4.GetValueOrDefault();
                  num5 = new double?(base.<Finish>g__Warp|0(valueOrDefault3));
                }
                else
                {
                  num5 = null;
                }
                item2.GoneAt = num5;
                num4 = item.VanishesAt;
                double? num6;
                if (num4 != null)
                {
                  double valueOrDefault4 = num4.GetValueOrDefault();
                  num6 = new double?(base.<Finish>g__Warp|0(valueOrDefault4));
                }
                else
                {
                  num6 = null;
                }
                item2.VanishesAt = num6;
                return item2;
              });
            }
            block2.Items = items.Select<ClickyInkLiveNote.Item, ClickyInkLiveNote.Item>(func2).ToList<ClickyInkLiveNote.Item>();
            return block2;
          });
        }
        return new ClickyInkLiveNote.Page(id, words, isBoard, blocks.Select<ClickyInkLiveNote.Block, ClickyInkLiveNote.Block>(func).ToList<ClickyInkLiveNote.Block>(), base.<Finish>g__Warp|0(page.LandsAt));
      }).ToList<ClickyInkLiveNote.Page>();
      this.EditStarts = this.EditStarts.Select<double, double>(new Func<double, double>(CS$<>8__locals1.<Finish>g__Warp|0)).ToList<double>();
      this.PenFreeAt = CS$<>8__locals1.<Finish>g__Warp|0(this.PenFreeAt);
      List<ClickyInkLiveNote.FocusKnown> list = this.FocusKnownSince.Where<ClickyInkLiveNote.FocusKnown>((ClickyInkLiveNote.FocusKnown k) => k.From <= CS$<>8__locals1.time).ToList<ClickyInkLiveNote.FocusKnown>();
      list.Add(new ClickyInkLiveNote.FocusKnown(CS$<>8__locals1.time, CS$<>8__locals1.time));
      list.AddRange(from k in this.FocusKnownSince
        where k.From > CS$<>8__locals1.time
        select new ClickyInkLiveNote.FocusKnown(base.<Finish>g__Warp|0(k.From), Math.Max(k.Since, CS$<>8__locals1.time)));
      this.FocusKnownSince = list;
    }

    // Token: 0x0600154D RID: 5453 RVA: 0x003B50F8 File Offset: 0x003B50F8
    public double KnownSince(double time)
    {
      for (int i = this.FocusKnownSince.Count - 1; i >= 0; i--)
      {
        if (this.FocusKnownSince[i].From <= time + 1E-09)
        {
          return this.FocusKnownSince[i].Since;
        }
      }
      return double.NegativeInfinity;
    }

    // Token: 0x170004F4 RID: 1268
    // (get) Token: 0x0600154E RID: 5454 RVA: 0x003B515C File Offset: 0x003B515C
    public double FinishedClock
    {
      get
      {
        return this.PenFreeAt + 0.6;
      }
    }

    // Token: 0x0600154F RID: 5455 RVA: 0x003B5170 File Offset: 0x003B5170
    public static bool IsSilentPageName(string name, int pageIndex)
    {
      return pageIndex == 0 && ClickyInkParser.Emphasis.Strip(name).TrimWs().Lowered() == "in short";
    }

    // Token: 0x06001550 RID: 5456 RVA: 0x003B5194 File Offset: 0x003B5194
    public List<int> PageIndices(double clock)
    {
      List<int> list = new List<int>();
      Func<ClickyInkLiveNote.Block, bool> <>9__0;
      for (int i = 0; i < this.Pages.Count; i++)
      {
        ClickyInkLiveNote.Page page = this.Pages[i];
        if (clock >= page.LandsAt)
        {
          IEnumerable<ClickyInkLiveNote.Block> blocks = page.Blocks;
          Func<ClickyInkLiveNote.Block, bool> func;
          if ((func = <>9__0) == null)
          {
            func = (<>9__0 = (ClickyInkLiveNote.Block b) => b.IsPresent(clock));
          }
          if (blocks.Any<ClickyInkLiveNote.Block>(func))
          {
            list.Add(i);
          }
        }
      }
      return list;
    }

    // Token: 0x06001551 RID: 5457 RVA: 0x003B521C File Offset: 0x003B521C
    public static string PageFocusID(string pageId)
    {
      return "page:" + pageId;
    }

    // Token: 0x06001552 RID: 5458 RVA: 0x003B522C File Offset: 0x003B522C
    public List<ClickyInkLiveNote.PenFocus> PenFocusList()
    {
      ClickyInkLiveNote.<>c__DisplayClass75_0 CS$<>8__locals1;
      CS$<>8__locals1.marks = new List<ValueTuple<double, double, string, string, bool>>();
      ClickyInkLiveNote.<PenFocusList>g__AddWords|75_0(this.Title, "title", null, ref CS$<>8__locals1);
      foreach (ClickyInkLiveNote.Page page in this.Pages)
      {
        int count = CS$<>8__locals1.marks.Count;
        ClickyInkLiveNote.<PenFocusList>g__AddWords|75_0(page.Name, ClickyInkLiveNote.PageFocusID(page.Id), null, ref CS$<>8__locals1);
        for (int i = count; i < CS$<>8__locals1.marks.Count; i++)
        {
          if (CS$<>8__locals1.marks[i].Item1 < page.LandsAt + 1.0)
          {
            List<ValueTuple<double, double, string, string, bool>> marks = CS$<>8__locals1.marks;
            int num = i;
            ValueTuple<double, double, string, string, bool> valueTuple = CS$<>8__locals1.marks[i];
            valueTuple.Item5 = true;
            marks[num] = valueTuple;
          }
        }
        foreach (ClickyInkLiveNote.Block block in page.Blocks)
        {
          CS$<>8__locals1.marks.Add(new ValueTuple<double, double, string, string, bool>(block.LandsAt, block.LandsAt + 0.4, block.Id, null, true));
          foreach (double num2 in block.PenArrivals)
          {
            ValueTuple<string, bool>? valueTuple2 = ClickyInkLiveNote.FirstChangedRow(block, num2, 1.2000000000000002);
            CS$<>8__locals1.marks.Add(new ValueTuple<double, double, string, string, bool>(num2, num2 + 0.2, block.Id, (valueTuple2 != null) ? valueTuple2.GetValueOrDefault().Item1 : null, valueTuple2 != null && valueTuple2.GetValueOrDefault().Item2));
          }
          foreach (ClickyInkLiveNote.Stamp<ClickyInkDocument.Block> stamp in block.Stamps.Skip<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>>(1))
          {
            ValueTuple<string, bool>? valueTuple3 = ClickyInkLiveNote.FirstChangedRow(block, stamp.At, 0.3);
            CS$<>8__locals1.marks.Add(new ValueTuple<double, double, string, string, bool>(stamp.At, stamp.At + 0.2, block.Id, (valueTuple3 != null) ? valueTuple3.GetValueOrDefault().Item1 : null, valueTuple3 != null && valueTuple3.GetValueOrDefault().Item2));
          }
          double? num3 = block.GoneAt;
          if (num3 != null)
          {
            double valueOrDefault = num3.GetValueOrDefault();
            CS$<>8__locals1.marks.Add(new ValueTuple<double, double, string, string, bool>(valueOrDefault, valueOrDefault + 0.3, block.Id, null, false));
          }
          ClickyInkLiveNote.<PenFocusList>g__AddWords|75_0(block.Words, block.Id, null, ref CS$<>8__locals1);
          foreach (ClickyInkLiveNote.Item item in block.Items)
          {
            CS$<>8__locals1.marks.Add(new ValueTuple<double, double, string, string, bool>(item.LandsAt, item.LandsAt + 0.1, block.Id, item.Id, true));
            foreach (ClickyInkLiveNote.Stamp<ClickyInkDocument.Item> stamp2 in item.Stamps.Skip<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>>(1))
            {
              CS$<>8__locals1.marks.Add(new ValueTuple<double, double, string, string, bool>(stamp2.At, stamp2.At + 0.3, block.Id, item.Id, false));
            }
            num3 = item.GoneAt;
            if (num3 != null)
            {
              double valueOrDefault2 = num3.GetValueOrDefault();
              CS$<>8__locals1.marks.Add(new ValueTuple<double, double, string, string, bool>(valueOrDefault2, valueOrDefault2 + 0.3, block.Id, item.Id, false));
            }
            ClickyInkLiveNote.<PenFocusList>g__AddWords|75_0(item.Words, block.Id, item.Id, ref CS$<>8__locals1);
            foreach (ClickyInkLiveNote.Words words in item.Cells)
            {
              ClickyInkLiveNote.<PenFocusList>g__AddWords|75_0(words, block.Id, item.Id, ref CS$<>8__locals1);
            }
            ClickyInkLiveNote.<PenFocusList>g__AddWords|75_0(item.Detail, block.Id, item.Id, ref CS$<>8__locals1);
          }
        }
      }
      List<ValueTuple<double, double, string, string, bool>> list = (from m in CS$<>8__locals1.marks
        orderby m.Item1, m.Item2
        select m).ToList<ValueTuple<double, double, string, string, bool>>();
      List<ClickyInkLiveNote.PenFocus> list2 = new List<ClickyInkLiveNote.PenFocus>();
      foreach (ValueTuple<double, double, string, string, bool> valueTuple4 in list)
      {
        if (list2.Count > 0)
        {
          List<ClickyInkLiveNote.PenFocus> list3 = list2;
          if (list3[list3.Count - 1].PlaceID == valueTuple4.Item3)
          {
            List<ClickyInkLiveNote.PenFocus> list4 = list2;
            if (list4[list4.Count - 1].ItemID == valueTuple4.Item4)
            {
              List<ClickyInkLiveNote.PenFocus> list5 = list2;
              ClickyInkLiveNote.PenFocus penFocus = list5[list5.Count - 1];
              penFocus.Until = Math.Max(penFocus.Until, valueTuple4.Item2);
              List<ClickyInkLiveNote.PenFocus> list6 = list2;
              list6[list6.Count - 1] = penFocus;
              continue;
            }
          }
        }
        list2.Add(new ClickyInkLiveNote.PenFocus(valueTuple4.Item1, valueTuple4.Item2, valueTuple4.Item3, valueTuple4.Item4)
        {
          KnownSince = this.KnownSince(valueTuple4.Item1),
          IsLanding = valueTuple4.Item5
        });
      }
      return list2;
    }

    // Token: 0x06001553 RID: 5459 RVA: 0x003B58F4 File Offset: 0x003B58F4
    [return: TupleElementNames(new string[] { "Id", "IsNew" })]
    [return: Nullable(new byte[] { 0, 1 })]
    public static ValueTuple<string, bool>? FirstChangedRow(ClickyInkLiveNote.Block block, double time, double window)
    {
      if (block.Items.Count == 0)
      {
        return null;
      }
      InkRange inkRange = new InkRange(time - 0.001, time + window);
      if (ClickyInkLiveNote.<FirstChangedRow>g__Starts|76_0(block.Words).Any<double>(new Func<double, bool>(inkRange.Contains)))
      {
        return null;
      }
      ValueTuple<double, string>? valueTuple = null;
      foreach (ClickyInkLiveNote.Item item in block.Items)
      {
        IEnumerable<double> enumerable = ClickyInkLiveNote.<FirstChangedRow>g__Starts|76_0(item.Words).Concat<double>(ClickyInkLiveNote.<FirstChangedRow>g__Starts|76_0(item.Detail));
        IEnumerable<ClickyInkLiveNote.Words> cells = item.Cells;
        Func<ClickyInkLiveNote.Words, IEnumerable<double>> func;
        if ((func = ClickyInkLiveNote.<>O.<0>__Starts) == null)
        {
          func = (ClickyInkLiveNote.<>O.<0>__Starts = new Func<ClickyInkLiveNote.Words, IEnumerable<double>>(ClickyInkLiveNote.<FirstChangedRow>g__Starts|76_0));
        }
        List<double> list = enumerable.Concat<double>(cells.SelectMany<ClickyInkLiveNote.Words, double>(func)).ToList<double>();
        list.Add(item.LandsAt);
        list.AddRange(from s in item.Stamps.Skip<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>>(1)
          select s.At);
        double? goneAt = item.GoneAt;
        if (goneAt != null)
        {
          double valueOrDefault = goneAt.GetValueOrDefault();
          list.Add(valueOrDefault);
        }
        List<double> list2 = list.Where<double>(new Func<double, bool>(inkRange.Contains)).ToList<double>();
        if (list2.Count != 0)
        {
          double num = list2.Min();
          if (num < ((valueTuple != null) ? valueTuple.GetValueOrDefault().Item1 : double.PositiveInfinity))
          {
            valueTuple = new ValueTuple<double, string>?(new ValueTuple<double, string>(num, item.Id));
          }
        }
      }
      if (valueTuple == null)
      {
        return null;
      }
      ValueTuple<double, string> f = valueTuple.GetValueOrDefault();
      ClickyInkLiveNote.Item item2 = block.Items.FirstOrDefault<ClickyInkLiveNote.Item>((ClickyInkLiveNote.Item i) => i.Id == f.Item2);
      if (item2 == null)
      {
        return null;
      }
      return new ValueTuple<string, bool>?(new ValueTuple<string, bool>(item2.Id, item2.LandsAt >= time - 0.001));
    }

    // Token: 0x06001556 RID: 5462 RVA: 0x003B5C08 File Offset: 0x003B5C08
    [NullableContext(2)]
    [CompilerGenerated]
    internal static void <PenFocusList>g__AddWords|75_0(ClickyInkLiveNote.Words words, [Nullable(1)] string placeId, string itemId = null, ref ClickyInkLiveNote.<>c__DisplayClass75_0 A_3)
    {
      if (words == null)
      {
        return;
      }
      foreach (ClickyInkLiveNote.Segment segment in words.Segments)
      {
        InkRange? inkRange = segment.Written;
        if (inkRange != null)
        {
          InkRange valueOrDefault = inkRange.GetValueOrDefault();
          if (valueOrDefault.UpperBound > valueOrDefault.LowerBound)
          {
            A_3.marks.Add(new ValueTuple<double, double, string, string, bool>(valueOrDefault.LowerBound, valueOrDefault.UpperBound, placeId, itemId, false));
          }
        }
        inkRange = segment.Struck;
        if (inkRange != null)
        {
          InkRange valueOrDefault2 = inkRange.GetValueOrDefault();
          A_3.marks.Add(new ValueTuple<double, double, string, string, bool>(valueOrDefault2.LowerBound, valueOrDefault2.UpperBound, placeId, itemId, false));
        }
      }
    }

    // Token: 0x06001557 RID: 5463 RVA: 0x003B5CE0 File Offset: 0x003B5CE0
    [CompilerGenerated]
    internal static IEnumerable<double> <FirstChangedRow>g__Starts|76_0([Nullable(2)] ClickyInkLiveNote.Words words)
    {
      ClickyInkLiveNote.<<FirstChangedRow>g__Starts|76_0>d <<FirstChangedRow>g__Starts|76_0>d = new ClickyInkLiveNote.<<FirstChangedRow>g__Starts|76_0>d(-2);
      <<FirstChangedRow>g__Starts|76_0>d.<>3__words = words;
      return <<FirstChangedRow>g__Starts|76_0>d;
    }

    // Token: 0x04000937 RID: 2359
    private static readonly HashSet<ClickyInkDocument.Kind> EntityKinds = new HashSet<ClickyInkDocument.Kind>
    {
      ClickyInkDocument.Kind.Link,
      ClickyInkDocument.Kind.File,
      ClickyInkDocument.Kind.Ticket,
      ClickyInkDocument.Kind.Photo,
      ClickyInkDocument.Kind.Place,
      ClickyInkDocument.Kind.Ask,
      ClickyInkDocument.Kind.Clip,
      ClickyInkDocument.Kind.Status,
      ClickyInkDocument.Kind.Question
    };

    // Token: 0x04000938 RID: 2360
    public const string TitleFocusID = "title";

    // Token: 0x02000500 RID: 1280
    [Nullable(0)]
    public sealed class Page : IEquatable<ClickyInkLiveNote.Page>
    {
      // Token: 0x170008E6 RID: 2278
      // (get) Token: 0x060031A3 RID: 12707 RVA: 0x004395C8 File Offset: 0x004395C8
      // (set) Token: 0x060031A4 RID: 12708 RVA: 0x004395D0 File Offset: 0x004395D0
      public string Id { get; set; }

      // Token: 0x170008E7 RID: 2279
      // (get) Token: 0x060031A5 RID: 12709 RVA: 0x004395DC File Offset: 0x004395DC
      // (set) Token: 0x060031A6 RID: 12710 RVA: 0x004395E4 File Offset: 0x004395E4
      public ClickyInkLiveNote.Words Name { get; set; }

      // Token: 0x170008E8 RID: 2280
      // (get) Token: 0x060031A7 RID: 12711 RVA: 0x004395F0 File Offset: 0x004395F0
      // (set) Token: 0x060031A8 RID: 12712 RVA: 0x004395F8 File Offset: 0x004395F8
      public bool IsBoard { get; set; }

      // Token: 0x170008E9 RID: 2281
      // (get) Token: 0x060031A9 RID: 12713 RVA: 0x00439604 File Offset: 0x00439604
      // (set) Token: 0x060031AA RID: 12714 RVA: 0x0043960C File Offset: 0x0043960C
      public List<ClickyInkLiveNote.Block> Blocks { get; set; }

      // Token: 0x170008EA RID: 2282
      // (get) Token: 0x060031AB RID: 12715 RVA: 0x00439618 File Offset: 0x00439618
      // (set) Token: 0x060031AC RID: 12716 RVA: 0x00439620 File Offset: 0x00439620
      public double LandsAt { get; set; }

      // Token: 0x060031AD RID: 12717 RVA: 0x0043962C File Offset: 0x0043962C
      public Page(string id, ClickyInkLiveNote.Words name, bool isBoard, List<ClickyInkLiveNote.Block> blocks, double landsAt)
      {
        this.Id = id;
        this.Name = name;
        this.IsBoard = isBoard;
        this.Blocks = blocks;
        this.LandsAt = landsAt;
      }

      // Token: 0x060031AE RID: 12718 RVA: 0x0043965C File Offset: 0x0043965C
      public ClickyInkLiveNote.Page Clone()
      {
        return new ClickyInkLiveNote.Page(this.Id, this.Name, this.IsBoard, this.Blocks.ToList<ClickyInkLiveNote.Block>(), this.LandsAt);
      }

      // Token: 0x060031AF RID: 12719 RVA: 0x00439688 File Offset: 0x00439688
      [NullableContext(2)]
      public bool Equals(ClickyInkLiveNote.Page other)
      {
        return this == other || (other != null && this.Id == other.Id && this.Name.Equals(other.Name) && this.IsBoard == other.IsBoard && this.LandsAt.Equals(other.LandsAt) && this.Blocks.SequenceEqual<ClickyInkLiveNote.Block>(other.Blocks));
      }

      // Token: 0x060031B0 RID: 12720 RVA: 0x004396FC File Offset: 0x004396FC
      [NullableContext(2)]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkLiveNote.Page);
      }

      // Token: 0x060031B1 RID: 12721 RVA: 0x0043970C File Offset: 0x0043970C
      public override int GetHashCode()
      {
        return HashCode.Combine<string, int>(this.Id, this.Blocks.Count);
      }
    }

    // Token: 0x02000501 RID: 1281
    [Nullable(0)]
    public sealed class Block : IEquatable<ClickyInkLiveNote.Block>
    {
      // Token: 0x170008EB RID: 2283
      // (get) Token: 0x060031B2 RID: 12722 RVA: 0x00439724 File Offset: 0x00439724
      // (set) Token: 0x060031B3 RID: 12723 RVA: 0x0043972C File Offset: 0x0043972C
      public string Id { get; set; }

      // Token: 0x170008EC RID: 2284
      // (get) Token: 0x060031B4 RID: 12724 RVA: 0x00439738 File Offset: 0x00439738
      // (set) Token: 0x060031B5 RID: 12725 RVA: 0x00439740 File Offset: 0x00439740
      [Nullable(new byte[] { 1, 0, 1 })]
      public List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>> Stamps
      {
        [return: Nullable(new byte[] { 1, 0, 1 })]
        get;
        [param: Nullable(new byte[] { 1, 0, 1 })]
        set;
      }

      // Token: 0x170008ED RID: 2285
      // (get) Token: 0x060031B6 RID: 12726 RVA: 0x0043974C File Offset: 0x0043974C
      // (set) Token: 0x060031B7 RID: 12727 RVA: 0x00439754 File Offset: 0x00439754
      public ClickyInkLiveNote.Words Words { get; set; }

      // Token: 0x170008EE RID: 2286
      // (get) Token: 0x060031B8 RID: 12728 RVA: 0x00439760 File Offset: 0x00439760
      // (set) Token: 0x060031B9 RID: 12729 RVA: 0x00439768 File Offset: 0x00439768
      public List<ClickyInkLiveNote.Item> Items { get; set; }

      // Token: 0x170008EF RID: 2287
      // (get) Token: 0x060031BA RID: 12730 RVA: 0x00439774 File Offset: 0x00439774
      // (set) Token: 0x060031BB RID: 12731 RVA: 0x0043977C File Offset: 0x0043977C
      public double LandsAt { get; set; }

      // Token: 0x170008F0 RID: 2288
      // (get) Token: 0x060031BC RID: 12732 RVA: 0x00439788 File Offset: 0x00439788
      // (set) Token: 0x060031BD RID: 12733 RVA: 0x00439790 File Offset: 0x00439790
      public double? GoneAt { get; set; }

      // Token: 0x170008F1 RID: 2289
      // (get) Token: 0x060031BE RID: 12734 RVA: 0x0043979C File Offset: 0x0043979C
      // (set) Token: 0x060031BF RID: 12735 RVA: 0x004397A4 File Offset: 0x004397A4
      public double? VanishesAt { get; set; }

      // Token: 0x170008F2 RID: 2290
      // (get) Token: 0x060031C0 RID: 12736 RVA: 0x004397B0 File Offset: 0x004397B0
      // (set) Token: 0x060031C1 RID: 12737 RVA: 0x004397B8 File Offset: 0x004397B8
      public int? BornInEdit { get; set; }

      // Token: 0x170008F3 RID: 2291
      // (get) Token: 0x060031C2 RID: 12738 RVA: 0x004397C4 File Offset: 0x004397C4
      // (set) Token: 0x060031C3 RID: 12739 RVA: 0x004397CC File Offset: 0x004397CC
      public int? GoneInEdit { get; set; }

      // Token: 0x170008F4 RID: 2292
      // (get) Token: 0x060031C4 RID: 12740 RVA: 0x004397D8 File Offset: 0x004397D8
      // (set) Token: 0x060031C5 RID: 12741 RVA: 0x004397E0 File Offset: 0x004397E0
      public bool IsPreview { get; set; }

      // Token: 0x170008F5 RID: 2293
      // (get) Token: 0x060031C6 RID: 12742 RVA: 0x004397EC File Offset: 0x004397EC
      // (set) Token: 0x060031C7 RID: 12743 RVA: 0x004397F4 File Offset: 0x004397F4
      public List<double> PenArrivals { get; set; } = new List<double>();

      // Token: 0x170008F6 RID: 2294
      // (get) Token: 0x060031C8 RID: 12744 RVA: 0x00439800 File Offset: 0x00439800
      // (set) Token: 0x060031C9 RID: 12745 RVA: 0x00439808 File Offset: 0x00439808
      public double? NumberShownSince { get; set; }

      // Token: 0x170008F7 RID: 2295
      // (get) Token: 0x060031CA RID: 12746 RVA: 0x00439814 File Offset: 0x00439814
      // (set) Token: 0x060031CB RID: 12747 RVA: 0x0043981C File Offset: 0x0043981C
      public double? NumberLineWrittenAt { get; set; }

      // Token: 0x060031CC RID: 12748 RVA: 0x00439828 File Offset: 0x00439828
      public Block(string id, [Nullable(new byte[] { 1, 0, 1 })] List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>> stamps, ClickyInkLiveNote.Words words, List<ClickyInkLiveNote.Item> items, double landsAt, int? bornInEdit = null)
      {
        this.Id = id;
        this.Stamps = stamps;
        this.Words = words;
        this.Items = items;
        this.LandsAt = landsAt;
        this.BornInEdit = bornInEdit;
      }

      // Token: 0x060031CD RID: 12749 RVA: 0x00439868 File Offset: 0x00439868
      public ClickyInkLiveNote.Block Clone()
      {
        return new ClickyInkLiveNote.Block(this.Id, this.Stamps.ToList<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>>(), this.Words, this.Items.ToList<ClickyInkLiveNote.Item>(), this.LandsAt, this.BornInEdit)
        {
          GoneAt = this.GoneAt,
          VanishesAt = this.VanishesAt,
          GoneInEdit = this.GoneInEdit,
          IsPreview = this.IsPreview,
          PenArrivals = this.PenArrivals.ToList<double>(),
          NumberShownSince = this.NumberShownSince,
          NumberLineWrittenAt = this.NumberLineWrittenAt
        };
      }

      // Token: 0x170008F8 RID: 2296
      // (get) Token: 0x060031CE RID: 12750 RVA: 0x00439904 File Offset: 0x00439904
      public ClickyInkDocument.Block Source
      {
        get
        {
          List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>> stamps = this.Stamps;
          return stamps[stamps.Count - 1].Value;
        }
      }

      // Token: 0x060031CF RID: 12751 RVA: 0x0043992C File Offset: 0x0043992C
      public ClickyInkDocument.Block SourceAt(double clock)
      {
        return ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>.ValueIn(this.Stamps, clock);
      }

      // Token: 0x060031D0 RID: 12752 RVA: 0x0043993C File Offset: 0x0043993C
      public bool IsPresent(double clock)
      {
        if (clock < this.LandsAt)
        {
          return false;
        }
        double? vanishesAt = this.VanishesAt;
        if (vanishesAt != null)
        {
          double valueOrDefault = vanishesAt.GetValueOrDefault();
          return clock < valueOrDefault + 0.45;
        }
        return true;
      }

      // Token: 0x060031D1 RID: 12753 RVA: 0x0043997C File Offset: 0x0043997C
      public bool IsStruck(double clock)
      {
        double? goneAt = this.GoneAt;
        if (goneAt != null)
        {
          double valueOrDefault = goneAt.GetValueOrDefault();
          return clock >= valueOrDefault;
        }
        return false;
      }

      // Token: 0x060031D2 RID: 12754 RVA: 0x004399AC File Offset: 0x004399AC
      [NullableContext(2)]
      public bool Equals(ClickyInkLiveNote.Block other)
      {
        if (this != other)
        {
          if (other != null && this.Id == other.Id && this.Stamps.SequenceEqual<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>>(other.Stamps) && this.Words.Equals(other.Words) && this.Items.SequenceEqual<ClickyInkLiveNote.Item>(other.Items) && this.LandsAt.Equals(other.LandsAt))
          {
            double? num = this.GoneAt;
            double? num2 = other.GoneAt;
            if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
            {
              num2 = this.VanishesAt;
              num = other.VanishesAt;
              if ((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null)))
              {
                int? num3 = this.BornInEdit;
                int? num4 = other.BornInEdit;
                if ((num3.GetValueOrDefault() == num4.GetValueOrDefault()) & (num3 != null == (num4 != null)))
                {
                  num4 = this.GoneInEdit;
                  num3 = other.GoneInEdit;
                  if (((num4.GetValueOrDefault() == num3.GetValueOrDefault()) & (num4 != null == (num3 != null))) && this.IsPreview == other.IsPreview && this.PenArrivals.SequenceEqual<double>(other.PenArrivals))
                  {
                    num = this.NumberShownSince;
                    num2 = other.NumberShownSince;
                    if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
                    {
                      num2 = this.NumberLineWrittenAt;
                      num = other.NumberLineWrittenAt;
                      return (num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null));
                    }
                  }
                }
              }
            }
          }
          return false;
        }
        return true;
      }

      // Token: 0x060031D3 RID: 12755 RVA: 0x00439B90 File Offset: 0x00439B90
      [NullableContext(2)]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkLiveNote.Block);
      }

      // Token: 0x060031D4 RID: 12756 RVA: 0x00439BA0 File Offset: 0x00439BA0
      public override int GetHashCode()
      {
        return HashCode.Combine<string, int, int>(this.Id, this.Stamps.Count, this.Items.Count);
      }
    }

    // Token: 0x02000502 RID: 1282
    [Nullable(0)]
    public sealed class Item : IEquatable<ClickyInkLiveNote.Item>
    {
      // Token: 0x170008F9 RID: 2297
      // (get) Token: 0x060031D5 RID: 12757 RVA: 0x00439BC4 File Offset: 0x00439BC4
      // (set) Token: 0x060031D6 RID: 12758 RVA: 0x00439BCC File Offset: 0x00439BCC
      public string Id { get; set; }

      // Token: 0x170008FA RID: 2298
      // (get) Token: 0x060031D7 RID: 12759 RVA: 0x00439BD8 File Offset: 0x00439BD8
      // (set) Token: 0x060031D8 RID: 12760 RVA: 0x00439BE0 File Offset: 0x00439BE0
      [Nullable(new byte[] { 1, 0, 1 })]
      public List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>> Stamps
      {
        [return: Nullable(new byte[] { 1, 0, 1 })]
        get;
        [param: Nullable(new byte[] { 1, 0, 1 })]
        set;
      }

      // Token: 0x170008FB RID: 2299
      // (get) Token: 0x060031D9 RID: 12761 RVA: 0x00439BEC File Offset: 0x00439BEC
      // (set) Token: 0x060031DA RID: 12762 RVA: 0x00439BF4 File Offset: 0x00439BF4
      public ClickyInkLiveNote.Words Words { get; set; }

      // Token: 0x170008FC RID: 2300
      // (get) Token: 0x060031DB RID: 12763 RVA: 0x00439C00 File Offset: 0x00439C00
      // (set) Token: 0x060031DC RID: 12764 RVA: 0x00439C08 File Offset: 0x00439C08
      public List<ClickyInkLiveNote.Words> Cells { get; set; }

      // Token: 0x170008FD RID: 2301
      // (get) Token: 0x060031DD RID: 12765 RVA: 0x00439C14 File Offset: 0x00439C14
      // (set) Token: 0x060031DE RID: 12766 RVA: 0x00439C1C File Offset: 0x00439C1C
      [Nullable(2)]
      public ClickyInkLiveNote.Words Detail
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x170008FE RID: 2302
      // (get) Token: 0x060031DF RID: 12767 RVA: 0x00439C28 File Offset: 0x00439C28
      // (set) Token: 0x060031E0 RID: 12768 RVA: 0x00439C30 File Offset: 0x00439C30
      public double LandsAt { get; set; }

      // Token: 0x170008FF RID: 2303
      // (get) Token: 0x060031E1 RID: 12769 RVA: 0x00439C3C File Offset: 0x00439C3C
      // (set) Token: 0x060031E2 RID: 12770 RVA: 0x00439C44 File Offset: 0x00439C44
      public double? GoneAt { get; set; }

      // Token: 0x17000900 RID: 2304
      // (get) Token: 0x060031E3 RID: 12771 RVA: 0x00439C50 File Offset: 0x00439C50
      // (set) Token: 0x060031E4 RID: 12772 RVA: 0x00439C58 File Offset: 0x00439C58
      public double? VanishesAt { get; set; }

      // Token: 0x17000901 RID: 2305
      // (get) Token: 0x060031E5 RID: 12773 RVA: 0x00439C64 File Offset: 0x00439C64
      // (set) Token: 0x060031E6 RID: 12774 RVA: 0x00439C6C File Offset: 0x00439C6C
      public int? GoneInEdit { get; set; }

      // Token: 0x17000902 RID: 2306
      // (get) Token: 0x060031E7 RID: 12775 RVA: 0x00439C78 File Offset: 0x00439C78
      // (set) Token: 0x060031E8 RID: 12776 RVA: 0x00439C80 File Offset: 0x00439C80
      public bool IsPreview { get; set; }

      // Token: 0x060031E9 RID: 12777 RVA: 0x00439C8C File Offset: 0x00439C8C
      public Item(string id, [Nullable(new byte[] { 1, 0, 1 })] List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>> stamps, ClickyInkLiveNote.Words words, List<ClickyInkLiveNote.Words> cells, [Nullable(2)] ClickyInkLiveNote.Words detail, double landsAt)
      {
        this.Id = id;
        this.Stamps = stamps;
        this.Words = words;
        this.Cells = cells;
        this.Detail = detail;
        this.LandsAt = landsAt;
      }

      // Token: 0x060031EA RID: 12778 RVA: 0x00439CC4 File Offset: 0x00439CC4
      public ClickyInkLiveNote.Item Clone()
      {
        return new ClickyInkLiveNote.Item(this.Id, this.Stamps.ToList<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>>(), this.Words, this.Cells.ToList<ClickyInkLiveNote.Words>(), this.Detail, this.LandsAt)
        {
          GoneAt = this.GoneAt,
          VanishesAt = this.VanishesAt,
          GoneInEdit = this.GoneInEdit,
          IsPreview = this.IsPreview
        };
      }

      // Token: 0x17000903 RID: 2307
      // (get) Token: 0x060031EB RID: 12779 RVA: 0x00439D34 File Offset: 0x00439D34
      public ClickyInkDocument.Item Source
      {
        get
        {
          List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>> stamps = this.Stamps;
          return stamps[stamps.Count - 1].Value;
        }
      }

      // Token: 0x060031EC RID: 12780 RVA: 0x00439D5C File Offset: 0x00439D5C
      public ClickyInkDocument.Item SourceAt(double clock)
      {
        return ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>.ValueIn(this.Stamps, clock);
      }

      // Token: 0x17000904 RID: 2308
      // (get) Token: 0x060031ED RID: 12781 RVA: 0x00439D6C File Offset: 0x00439D6C
      public double ShownSince
      {
        get
        {
          if (this.Stamps.Count <= 0)
          {
            return this.LandsAt;
          }
          List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>> stamps = this.Stamps;
          return stamps[stamps.Count - 1].At;
        }
      }

      // Token: 0x060031EE RID: 12782 RVA: 0x00439DAC File Offset: 0x00439DAC
      public double? Since(Func<ClickyInkDocument.Item, bool> holds)
      {
        if (this.Stamps.Count != 0)
        {
          List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>> stamps = this.Stamps;
          if (holds(stamps[stamps.Count - 1].Value))
          {
            List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>> stamps2 = this.Stamps;
            double num = stamps2[stamps2.Count - 1].At;
            int num2 = this.Stamps.Count - 2;
            while (num2 >= 0 && holds(this.Stamps[num2].Value))
            {
              num = this.Stamps[num2].At;
              num2--;
            }
            return new double?(num);
          }
        }
        return null;
      }

      // Token: 0x060031EF RID: 12783 RVA: 0x00439E60 File Offset: 0x00439E60
      public bool IsPresent(double clock)
      {
        if (clock < this.LandsAt)
        {
          return false;
        }
        double? vanishesAt = this.VanishesAt;
        if (vanishesAt != null)
        {
          double valueOrDefault = vanishesAt.GetValueOrDefault();
          return clock < valueOrDefault + 0.45;
        }
        return true;
      }

      // Token: 0x060031F0 RID: 12784 RVA: 0x00439EA0 File Offset: 0x00439EA0
      public bool IsStruck(double clock)
      {
        double? goneAt = this.GoneAt;
        if (goneAt != null)
        {
          double valueOrDefault = goneAt.GetValueOrDefault();
          return clock >= valueOrDefault;
        }
        return false;
      }

      // Token: 0x060031F1 RID: 12785 RVA: 0x00439ED0 File Offset: 0x00439ED0
      [NullableContext(2)]
      public bool Equals(ClickyInkLiveNote.Item other)
      {
        if (this != other)
        {
          if (other != null && this.Id == other.Id && this.Stamps.SequenceEqual<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>>(other.Stamps) && this.Words.Equals(other.Words) && this.Cells.SequenceEqual<ClickyInkLiveNote.Words>(other.Cells) && object.Equals(this.Detail, other.Detail) && this.LandsAt.Equals(other.LandsAt))
          {
            double? num = this.GoneAt;
            double? num2 = other.GoneAt;
            if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
            {
              num2 = this.VanishesAt;
              num = other.VanishesAt;
              if ((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null)))
              {
                int? goneInEdit = this.GoneInEdit;
                int? goneInEdit2 = other.GoneInEdit;
                if ((goneInEdit.GetValueOrDefault() == goneInEdit2.GetValueOrDefault()) & (goneInEdit != null == (goneInEdit2 != null)))
                {
                  return this.IsPreview == other.IsPreview;
                }
              }
            }
          }
          return false;
        }
        return true;
      }

      // Token: 0x060031F2 RID: 12786 RVA: 0x0043A018 File Offset: 0x0043A018
      [NullableContext(2)]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkLiveNote.Item);
      }

      // Token: 0x060031F3 RID: 12787 RVA: 0x0043A028 File Offset: 0x0043A028
      public override int GetHashCode()
      {
        return HashCode.Combine<string, int>(this.Id, this.Stamps.Count);
      }
    }

    // Token: 0x02000503 RID: 1283
    [NullableContext(0)]
    public readonly struct Stamp<[Nullable(2)] TValue> : IEquatable<ClickyInkLiveNote.Stamp<TValue>>
    {
      // Token: 0x060031F4 RID: 12788 RVA: 0x0043A040 File Offset: 0x0043A040
      [NullableContext(1)]
      public Stamp(double At, TValue Value, int? Edit = null)
      {
        this.At = At;
        this.Value = Value;
        this.Edit = Edit;
      }

      // Token: 0x17000905 RID: 2309
      // (get) Token: 0x060031F5 RID: 12789 RVA: 0x0043A058 File Offset: 0x0043A058
      // (set) Token: 0x060031F6 RID: 12790 RVA: 0x0043A060 File Offset: 0x0043A060
      public double At { get; set; }

      // Token: 0x17000906 RID: 2310
      // (get) Token: 0x060031F7 RID: 12791 RVA: 0x0043A06C File Offset: 0x0043A06C
      // (set) Token: 0x060031F8 RID: 12792 RVA: 0x0043A074 File Offset: 0x0043A074
      [Nullable(1)]
      public TValue Value
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x17000907 RID: 2311
      // (get) Token: 0x060031F9 RID: 12793 RVA: 0x0043A080 File Offset: 0x0043A080
      // (set) Token: 0x060031FA RID: 12794 RVA: 0x0043A088 File Offset: 0x0043A088
      public int? Edit { get; set; }

      // Token: 0x060031FB RID: 12795 RVA: 0x0043A094 File Offset: 0x0043A094
      [NullableContext(1)]
      public static TValue ValueIn([Nullable(new byte[] { 1, 0, 1 })] IReadOnlyList<ClickyInkLiveNote.Stamp<TValue>> stamps, double clock)
      {
        for (int i = stamps.Count - 1; i >= 0; i--)
        {
          if (stamps[i].At <= clock)
          {
            return stamps[i].Value;
          }
        }
        return stamps[0].Value;
      }

      // Token: 0x060031FC RID: 12796 RVA: 0x0043A0E8 File Offset: 0x0043A0E8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Stamp");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060031FD RID: 12797 RVA: 0x0043A134 File Offset: 0x0043A134
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("At = ");
        builder.Append(this.At.ToString());
        builder.Append(", Value = ");
        builder.Append(this.Value);
        builder.Append(", Edit = ");
        builder.Append(this.Edit.ToString());
        return true;
      }

      // Token: 0x060031FE RID: 12798 RVA: 0x0043A1B0 File Offset: 0x0043A1B0
      [CompilerGenerated]
      public static bool operator !=(ClickyInkLiveNote.Stamp<TValue> left, ClickyInkLiveNote.Stamp<TValue> right)
      {
        return !(left == right);
      }

      // Token: 0x060031FF RID: 12799 RVA: 0x0043A1BC File Offset: 0x0043A1BC
      [CompilerGenerated]
      public static bool operator ==(ClickyInkLiveNote.Stamp<TValue> left, ClickyInkLiveNote.Stamp<TValue> right)
      {
        return left.Equals(right);
      }

      // Token: 0x06003200 RID: 12800 RVA: 0x0043A1C8 File Offset: 0x0043A1C8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<double>.Default.GetHashCode(this.<At>k__BackingField) * -1521134295 + EqualityComparer<TValue>.Default.GetHashCode(this.<Value>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<Edit>k__BackingField);
      }

      // Token: 0x06003201 RID: 12801 RVA: 0x0043A208 File Offset: 0x0043A208
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is ClickyInkLiveNote.Stamp<TValue> && this.Equals((ClickyInkLiveNote.Stamp<TValue>)obj);
      }

      // Token: 0x06003202 RID: 12802 RVA: 0x0043A220 File Offset: 0x0043A220
      [CompilerGenerated]
      public bool Equals(ClickyInkLiveNote.Stamp<TValue> other)
      {
        return EqualityComparer<double>.Default.Equals(this.<At>k__BackingField, other.<At>k__BackingField) && EqualityComparer<TValue>.Default.Equals(this.<Value>k__BackingField, other.<Value>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<Edit>k__BackingField, other.<Edit>k__BackingField);
      }

      // Token: 0x06003203 RID: 12803 RVA: 0x0043A278 File Offset: 0x0043A278
      [NullableContext(1)]
      [CompilerGenerated]
      public void Deconstruct(out double At, out TValue Value, out int? Edit)
      {
        At = this.At;
        Value = this.Value;
        Edit = this.Edit;
      }
    }

    // Token: 0x02000504 RID: 1284
    [Nullable(0)]
    public sealed class Words : IEquatable<ClickyInkLiveNote.Words>
    {
      // Token: 0x17000908 RID: 2312
      // (get) Token: 0x06003204 RID: 12804 RVA: 0x0043A29C File Offset: 0x0043A29C
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkLiveNote.Words);
        }
      }

      // Token: 0x17000909 RID: 2313
      // (get) Token: 0x06003205 RID: 12805 RVA: 0x0043A2A8 File Offset: 0x0043A2A8
      // (set) Token: 0x06003206 RID: 12806 RVA: 0x0043A2B0 File Offset: 0x0043A2B0
      public IReadOnlyList<ClickyInkLiveNote.Segment> Segments { get; set; }

      // Token: 0x1700090A RID: 2314
      // (get) Token: 0x06003207 RID: 12807 RVA: 0x0043A2BC File Offset: 0x0043A2BC
      // (set) Token: 0x06003208 RID: 12808 RVA: 0x0043A2C4 File Offset: 0x0043A2C4
      public double? AsOf { get; set; }

      // Token: 0x1700090B RID: 2315
      // (get) Token: 0x06003209 RID: 12809 RVA: 0x0043A2D0 File Offset: 0x0043A2D0
      // (set) Token: 0x0600320A RID: 12810 RVA: 0x0043A2D8 File Offset: 0x0043A2D8
      public double? ReservedUntil { get; set; }

      // Token: 0x0600320B RID: 12811 RVA: 0x0043A2E4 File Offset: 0x0043A2E4
      public Words(IReadOnlyList<ClickyInkLiveNote.Segment> segments, double? asOf = null, double? reservedUntil = null)
      {
        this.Segments = segments;
        this.AsOf = asOf;
        this.ReservedUntil = reservedUntil;
      }

      // Token: 0x1700090C RID: 2316
      // (get) Token: 0x0600320C RID: 12812 RVA: 0x0043A304 File Offset: 0x0043A304
      public string Current
      {
        get
        {
          return string.Concat(from s in this.Segments
            where s.Struck == null && s.VanishesAt == null
            select s.Text);
        }
      }

      // Token: 0x0600320D RID: 12813 RVA: 0x0043A364 File Offset: 0x0043A364
      public string TextAt(double clock)
      {
        return string.Concat(from s in this.Segments.Where<ClickyInkLiveNote.Segment>(delegate(ClickyInkLiveNote.Segment s)
          {
            InkRange? inkRange = s.Written;
            if (inkRange != null)
            {
              InkRange valueOrDefault = inkRange.GetValueOrDefault();
              if (clock < valueOrDefault.LowerBound)
              {
                return false;
              }
            }
            inkRange = s.Struck;
            if (inkRange != null)
            {
              InkRange valueOrDefault2 = inkRange.GetValueOrDefault();
              if (clock >= valueOrDefault2.LowerBound)
              {
                return false;
              }
            }
            double? vanishesAt = s.VanishesAt;
            if (vanishesAt != null)
            {
              double valueOrDefault3 = vanishesAt.GetValueOrDefault();
              return clock < valueOrDefault3;
            }
            return true;
          })
          select s.Text);
      }

      // Token: 0x1700090D RID: 2317
      // (get) Token: 0x0600320E RID: 12814 RVA: 0x0043A3C0 File Offset: 0x0043A3C0
      public double FinishedAt
      {
        get
        {
          List<InkRange> list = (from s in this.Segments
            where s.Written != null
            select s.Written.Value).ToList<InkRange>();
          double? asOf2 = this.AsOf;
          if (asOf2 != null)
          {
            double asOf = asOf2.GetValueOrDefault();
            List<InkRange> list2 = list.Where<InkRange>((InkRange w) => w.LowerBound <= asOf).ToList<InkRange>();
            if (list2.Count != 0)
            {
              return list2.Max<InkRange>((InkRange w) => w.UpperBound);
            }
            if (list.Count != 0)
            {
              return list.MinBy<InkRange, double>((InkRange w) => w.LowerBound).UpperBound;
            }
            return 0.0;
          }
          else
          {
            if (list.Count != 0)
            {
              return list.Max<InkRange>((InkRange w) => w.UpperBound);
            }
            return 0.0;
          }
        }
      }

      // Token: 0x1700090E RID: 2318
      // (get) Token: 0x0600320F RID: 12815 RVA: 0x0043A504 File Offset: 0x0043A504
      public double? StartsAt
      {
        get
        {
          double? num = null;
          foreach (ClickyInkLiveNote.Segment segment in this.Segments)
          {
            InkRange? written = segment.Written;
            if (written != null)
            {
              InkRange valueOrDefault = written.GetValueOrDefault();
              if (num != null)
              {
                double lowerBound = valueOrDefault.LowerBound;
                double? num2 = num;
                if (!((lowerBound < num2.GetValueOrDefault()) & (num2 != null)))
                {
                  continue;
                }
              }
              num = new double?(valueOrDefault.LowerBound);
            }
          }
          return num;
        }
      }

      // Token: 0x06003210 RID: 12816 RVA: 0x0043A5A0 File Offset: 0x0043A5A0
      public static ClickyInkLiveNote.Words Plain(string text)
      {
        IReadOnlyList<ClickyInkLiveNote.Segment> readOnlyList2;
        if (text.Length != 0)
        {
          IReadOnlyList<ClickyInkLiveNote.Segment> readOnlyList = new <>z__ReadOnlySingleElementList<ClickyInkLiveNote.Segment>(new ClickyInkLiveNote.Segment(text, null, null, null, null, null, false));
          readOnlyList2 = readOnlyList;
        }
        else
        {
          IReadOnlyList<ClickyInkLiveNote.Segment> readOnlyList = Array.Empty<ClickyInkLiveNote.Segment>();
          readOnlyList2 = readOnlyList;
        }
        return new ClickyInkLiveNote.Words(readOnlyList2, null, null);
      }

      // Token: 0x06003211 RID: 12817 RVA: 0x0043A610 File Offset: 0x0043A610
      [NullableContext(2)]
      public bool Equals(ClickyInkLiveNote.Words other)
      {
        if (this != other)
        {
          if (other != null)
          {
            double? num = this.AsOf;
            double? num2 = other.AsOf;
            if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
            {
              num2 = this.ReservedUntil;
              num = other.ReservedUntil;
              if ((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null)))
              {
                return this.Segments.SequenceEqual<ClickyInkLiveNote.Segment>(other.Segments);
              }
            }
          }
          return false;
        }
        return true;
      }

      // Token: 0x06003212 RID: 12818 RVA: 0x0043A69C File Offset: 0x0043A69C
      public override int GetHashCode()
      {
        return HashCode.Combine<int, double?, double?>(this.Segments.Count, this.AsOf, this.ReservedUntil);
      }

      // Token: 0x06003213 RID: 12819 RVA: 0x0043A6BC File Offset: 0x0043A6BC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Words");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003214 RID: 12820 RVA: 0x0043A708 File Offset: 0x0043A708
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Segments = ");
        builder.Append(this.Segments);
        builder.Append(", AsOf = ");
        builder.Append(this.AsOf.ToString());
        builder.Append(", ReservedUntil = ");
        builder.Append(this.ReservedUntil.ToString());
        builder.Append(", Current = ");
        builder.Append(this.Current);
        builder.Append(", FinishedAt = ");
        builder.Append(this.FinishedAt.ToString());
        builder.Append(", StartsAt = ");
        builder.Append(this.StartsAt.ToString());
        return true;
      }

      // Token: 0x06003215 RID: 12821 RVA: 0x0043A7EC File Offset: 0x0043A7EC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkLiveNote.Words left, ClickyInkLiveNote.Words right)
      {
        return !(left == right);
      }

      // Token: 0x06003216 RID: 12822 RVA: 0x0043A7F8 File Offset: 0x0043A7F8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkLiveNote.Words left, ClickyInkLiveNote.Words right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003217 RID: 12823 RVA: 0x0043A80C File Offset: 0x0043A80C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkLiveNote.Words);
      }

      // Token: 0x06003219 RID: 12825 RVA: 0x0043A824 File Offset: 0x0043A824
      [CompilerGenerated]
      private Words(ClickyInkLiveNote.Words original)
      {
        this.Segments = original.<Segments>k__BackingField;
        this.AsOf = original.<AsOf>k__BackingField;
        this.ReservedUntil = original.<ReservedUntil>k__BackingField;
      }

      // Token: 0x04001F14 RID: 7956
      public static readonly ClickyInkLiveNote.Words Empty = new ClickyInkLiveNote.Words(Array.Empty<ClickyInkLiveNote.Segment>(), null, null);
    }

    // Token: 0x02000505 RID: 1285
    [Nullable(0)]
    public struct Segment : IEquatable<ClickyInkLiveNote.Segment>
    {
      // Token: 0x0600321B RID: 12827 RVA: 0x0043A880 File Offset: 0x0043A880
      public Segment(string Text, InkRange? Written = null, InkRange? Struck = null, double? VanishesAt = null, int? Edit = null, int? StruckInEdit = null, bool IsPreview = false)
      {
        this.Text = Text;
        this.Written = Written;
        this.Struck = Struck;
        this.VanishesAt = VanishesAt;
        this.Edit = Edit;
        this.StruckInEdit = StruckInEdit;
        this.IsPreview = IsPreview;
      }

      // Token: 0x1700090F RID: 2319
      // (get) Token: 0x0600321C RID: 12828 RVA: 0x0043A8B8 File Offset: 0x0043A8B8
      // (set) Token: 0x0600321D RID: 12829 RVA: 0x0043A8C0 File Offset: 0x0043A8C0
      public string Text { readonly get; set; }

      // Token: 0x17000910 RID: 2320
      // (get) Token: 0x0600321E RID: 12830 RVA: 0x0043A8CC File Offset: 0x0043A8CC
      // (set) Token: 0x0600321F RID: 12831 RVA: 0x0043A8D4 File Offset: 0x0043A8D4
      public InkRange? Written { readonly get; set; }

      // Token: 0x17000911 RID: 2321
      // (get) Token: 0x06003220 RID: 12832 RVA: 0x0043A8E0 File Offset: 0x0043A8E0
      // (set) Token: 0x06003221 RID: 12833 RVA: 0x0043A8E8 File Offset: 0x0043A8E8
      public InkRange? Struck { readonly get; set; }

      // Token: 0x17000912 RID: 2322
      // (get) Token: 0x06003222 RID: 12834 RVA: 0x0043A8F4 File Offset: 0x0043A8F4
      // (set) Token: 0x06003223 RID: 12835 RVA: 0x0043A8FC File Offset: 0x0043A8FC
      public double? VanishesAt { readonly get; set; }

      // Token: 0x17000913 RID: 2323
      // (get) Token: 0x06003224 RID: 12836 RVA: 0x0043A908 File Offset: 0x0043A908
      // (set) Token: 0x06003225 RID: 12837 RVA: 0x0043A910 File Offset: 0x0043A910
      public int? Edit { readonly get; set; }

      // Token: 0x17000914 RID: 2324
      // (get) Token: 0x06003226 RID: 12838 RVA: 0x0043A91C File Offset: 0x0043A91C
      // (set) Token: 0x06003227 RID: 12839 RVA: 0x0043A924 File Offset: 0x0043A924
      public int? StruckInEdit { readonly get; set; }

      // Token: 0x17000915 RID: 2325
      // (get) Token: 0x06003228 RID: 12840 RVA: 0x0043A930 File Offset: 0x0043A930
      // (set) Token: 0x06003229 RID: 12841 RVA: 0x0043A938 File Offset: 0x0043A938
      public bool IsPreview { readonly get; set; }

      // Token: 0x0600322A RID: 12842 RVA: 0x0043A944 File Offset: 0x0043A944
      [NullableContext(0)]
      [CompilerGenerated]
      public override readonly string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Segment");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600322B RID: 12843 RVA: 0x0043A990 File Offset: 0x0043A990
      [NullableContext(0)]
      [CompilerGenerated]
      private readonly bool PrintMembers(StringBuilder builder)
      {
        builder.Append("Text = ");
        builder.Append(this.Text);
        builder.Append(", Written = ");
        builder.Append(this.Written.ToString());
        builder.Append(", Struck = ");
        builder.Append(this.Struck.ToString());
        builder.Append(", VanishesAt = ");
        builder.Append(this.VanishesAt.ToString());
        builder.Append(", Edit = ");
        builder.Append(this.Edit.ToString());
        builder.Append(", StruckInEdit = ");
        builder.Append(this.StruckInEdit.ToString());
        builder.Append(", IsPreview = ");
        builder.Append(this.IsPreview.ToString());
        return true;
      }

      // Token: 0x0600322C RID: 12844 RVA: 0x0043AAA4 File Offset: 0x0043AAA4
      [CompilerGenerated]
      public static bool operator !=(ClickyInkLiveNote.Segment left, ClickyInkLiveNote.Segment right)
      {
        return !(left == right);
      }

      // Token: 0x0600322D RID: 12845 RVA: 0x0043AAB0 File Offset: 0x0043AAB0
      [CompilerGenerated]
      public static bool operator ==(ClickyInkLiveNote.Segment left, ClickyInkLiveNote.Segment right)
      {
        return left.Equals(right);
      }

      // Token: 0x0600322E RID: 12846 RVA: 0x0043AABC File Offset: 0x0043AABC
      [CompilerGenerated]
      public override readonly int GetHashCode()
      {
        return (((((EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField) * -1521134295 + EqualityComparer<InkRange?>.Default.GetHashCode(this.<Written>k__BackingField)) * -1521134295 + EqualityComparer<InkRange?>.Default.GetHashCode(this.<Struck>k__BackingField)) * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(this.<VanishesAt>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<Edit>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<StruckInEdit>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsPreview>k__BackingField);
      }

      // Token: 0x0600322F RID: 12847 RVA: 0x0043AB64 File Offset: 0x0043AB64
      [NullableContext(0)]
      [CompilerGenerated]
      public override readonly bool Equals(object obj)
      {
        return obj is ClickyInkLiveNote.Segment && this.Equals((ClickyInkLiveNote.Segment)obj);
      }

      // Token: 0x06003230 RID: 12848 RVA: 0x0043AB7C File Offset: 0x0043AB7C
      [CompilerGenerated]
      public readonly bool Equals(ClickyInkLiveNote.Segment other)
      {
        return EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<InkRange?>.Default.Equals(this.<Written>k__BackingField, other.<Written>k__BackingField) && EqualityComparer<InkRange?>.Default.Equals(this.<Struck>k__BackingField, other.<Struck>k__BackingField) && EqualityComparer<double?>.Default.Equals(this.<VanishesAt>k__BackingField, other.<VanishesAt>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<Edit>k__BackingField, other.<Edit>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<StruckInEdit>k__BackingField, other.<StruckInEdit>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsPreview>k__BackingField, other.<IsPreview>k__BackingField);
      }

      // Token: 0x06003231 RID: 12849 RVA: 0x0043AC34 File Offset: 0x0043AC34
      [CompilerGenerated]
      public readonly void Deconstruct(out string Text, out InkRange? Written, out InkRange? Struck, out double? VanishesAt, out int? Edit, out int? StruckInEdit, out bool IsPreview)
      {
        Text = this.Text;
        Written = this.Written;
        Struck = this.Struck;
        VanishesAt = this.VanishesAt;
        Edit = this.Edit;
        StruckInEdit = this.StruckInEdit;
        IsPreview = this.IsPreview;
      }
    }

    // Token: 0x02000506 RID: 1286
    [NullableContext(0)]
    public readonly struct FocusKnown : IEquatable<ClickyInkLiveNote.FocusKnown>
    {
      // Token: 0x06003232 RID: 12850 RVA: 0x0043AC94 File Offset: 0x0043AC94
      public FocusKnown(double From, double Since)
      {
        this.From = From;
        this.Since = Since;
      }

      // Token: 0x17000916 RID: 2326
      // (get) Token: 0x06003233 RID: 12851 RVA: 0x0043ACA4 File Offset: 0x0043ACA4
      // (set) Token: 0x06003234 RID: 12852 RVA: 0x0043ACAC File Offset: 0x0043ACAC
      public double From { get; set; }

      // Token: 0x17000917 RID: 2327
      // (get) Token: 0x06003235 RID: 12853 RVA: 0x0043ACB8 File Offset: 0x0043ACB8
      // (set) Token: 0x06003236 RID: 12854 RVA: 0x0043ACC0 File Offset: 0x0043ACC0
      public double Since { get; set; }

      // Token: 0x06003237 RID: 12855 RVA: 0x0043ACCC File Offset: 0x0043ACCC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("FocusKnown");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003238 RID: 12856 RVA: 0x0043AD18 File Offset: 0x0043AD18
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("From = ");
        builder.Append(this.From.ToString());
        builder.Append(", Since = ");
        builder.Append(this.Since.ToString());
        return true;
      }

      // Token: 0x06003239 RID: 12857 RVA: 0x0043AD74 File Offset: 0x0043AD74
      [CompilerGenerated]
      public static bool operator !=(ClickyInkLiveNote.FocusKnown left, ClickyInkLiveNote.FocusKnown right)
      {
        return !(left == right);
      }

      // Token: 0x0600323A RID: 12858 RVA: 0x0043AD80 File Offset: 0x0043AD80
      [CompilerGenerated]
      public static bool operator ==(ClickyInkLiveNote.FocusKnown left, ClickyInkLiveNote.FocusKnown right)
      {
        return left.Equals(right);
      }

      // Token: 0x0600323B RID: 12859 RVA: 0x0043AD8C File Offset: 0x0043AD8C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<double>.Default.GetHashCode(this.<From>k__BackingField) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Since>k__BackingField);
      }

      // Token: 0x0600323C RID: 12860 RVA: 0x0043ADB8 File Offset: 0x0043ADB8
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is ClickyInkLiveNote.FocusKnown && this.Equals((ClickyInkLiveNote.FocusKnown)obj);
      }

      // Token: 0x0600323D RID: 12861 RVA: 0x0043ADD0 File Offset: 0x0043ADD0
      [CompilerGenerated]
      public bool Equals(ClickyInkLiveNote.FocusKnown other)
      {
        return EqualityComparer<double>.Default.Equals(this.<From>k__BackingField, other.<From>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Since>k__BackingField, other.<Since>k__BackingField);
      }

      // Token: 0x0600323E RID: 12862 RVA: 0x0043AE04 File Offset: 0x0043AE04
      [CompilerGenerated]
      public void Deconstruct(out double From, out double Since)
      {
        From = this.From;
        Since = this.Since;
      }
    }

    // Token: 0x02000507 RID: 1287
    [Nullable(0)]
    public struct PenFocus : IEquatable<ClickyInkLiveNote.PenFocus>
    {
      // Token: 0x0600323F RID: 12863 RVA: 0x0043AE18 File Offset: 0x0043AE18
      public PenFocus(double At, double Until, string PlaceID, [Nullable(2)] string ItemID = null)
      {
        this.At = At;
        this.Until = Until;
        this.PlaceID = PlaceID;
        this.ItemID = ItemID;
        this.KnownSince = double.NegativeInfinity;
        this.IsLanding = false;
      }

      // Token: 0x17000918 RID: 2328
      // (get) Token: 0x06003240 RID: 12864 RVA: 0x0043AE50 File Offset: 0x0043AE50
      // (set) Token: 0x06003241 RID: 12865 RVA: 0x0043AE58 File Offset: 0x0043AE58
      public double At { readonly get; set; }

      // Token: 0x17000919 RID: 2329
      // (get) Token: 0x06003242 RID: 12866 RVA: 0x0043AE64 File Offset: 0x0043AE64
      // (set) Token: 0x06003243 RID: 12867 RVA: 0x0043AE6C File Offset: 0x0043AE6C
      public double Until { readonly get; set; }

      // Token: 0x1700091A RID: 2330
      // (get) Token: 0x06003244 RID: 12868 RVA: 0x0043AE78 File Offset: 0x0043AE78
      // (set) Token: 0x06003245 RID: 12869 RVA: 0x0043AE80 File Offset: 0x0043AE80
      public string PlaceID { readonly get; set; }

      // Token: 0x1700091B RID: 2331
      // (get) Token: 0x06003246 RID: 12870 RVA: 0x0043AE8C File Offset: 0x0043AE8C
      // (set) Token: 0x06003247 RID: 12871 RVA: 0x0043AE94 File Offset: 0x0043AE94
      [Nullable(2)]
      public string ItemID
      {
        [NullableContext(2)]
        readonly get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x1700091C RID: 2332
      // (get) Token: 0x06003248 RID: 12872 RVA: 0x0043AEA0 File Offset: 0x0043AEA0
      // (set) Token: 0x06003249 RID: 12873 RVA: 0x0043AEA8 File Offset: 0x0043AEA8
      public double KnownSince { readonly get; set; }

      // Token: 0x1700091D RID: 2333
      // (get) Token: 0x0600324A RID: 12874 RVA: 0x0043AEB4 File Offset: 0x0043AEB4
      // (set) Token: 0x0600324B RID: 12875 RVA: 0x0043AEBC File Offset: 0x0043AEBC
      public bool IsLanding { readonly get; set; }

      // Token: 0x0600324C RID: 12876 RVA: 0x0043AEC8 File Offset: 0x0043AEC8
      [NullableContext(0)]
      [CompilerGenerated]
      public override readonly string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("PenFocus");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600324D RID: 12877 RVA: 0x0043AF14 File Offset: 0x0043AF14
      [NullableContext(0)]
      [CompilerGenerated]
      private readonly bool PrintMembers(StringBuilder builder)
      {
        builder.Append("At = ");
        builder.Append(this.At.ToString());
        builder.Append(", Until = ");
        builder.Append(this.Until.ToString());
        builder.Append(", PlaceID = ");
        builder.Append(this.PlaceID);
        builder.Append(", ItemID = ");
        builder.Append(this.ItemID);
        builder.Append(", KnownSince = ");
        builder.Append(this.KnownSince.ToString());
        builder.Append(", IsLanding = ");
        builder.Append(this.IsLanding.ToString());
        return true;
      }

      // Token: 0x0600324E RID: 12878 RVA: 0x0043AFF0 File Offset: 0x0043AFF0
      [CompilerGenerated]
      public static bool operator !=(ClickyInkLiveNote.PenFocus left, ClickyInkLiveNote.PenFocus right)
      {
        return !(left == right);
      }

      // Token: 0x0600324F RID: 12879 RVA: 0x0043AFFC File Offset: 0x0043AFFC
      [CompilerGenerated]
      public static bool operator ==(ClickyInkLiveNote.PenFocus left, ClickyInkLiveNote.PenFocus right)
      {
        return left.Equals(right);
      }

      // Token: 0x06003250 RID: 12880 RVA: 0x0043B008 File Offset: 0x0043B008
      [CompilerGenerated]
      public override readonly int GetHashCode()
      {
        return ((((EqualityComparer<double>.Default.GetHashCode(this.<At>k__BackingField) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Until>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PlaceID>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ItemID>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<KnownSince>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsLanding>k__BackingField);
      }

      // Token: 0x06003251 RID: 12881 RVA: 0x0043B098 File Offset: 0x0043B098
      [NullableContext(0)]
      [CompilerGenerated]
      public override readonly bool Equals(object obj)
      {
        return obj is ClickyInkLiveNote.PenFocus && this.Equals((ClickyInkLiveNote.PenFocus)obj);
      }

      // Token: 0x06003252 RID: 12882 RVA: 0x0043B0B0 File Offset: 0x0043B0B0
      [CompilerGenerated]
      public readonly bool Equals(ClickyInkLiveNote.PenFocus other)
      {
        return EqualityComparer<double>.Default.Equals(this.<At>k__BackingField, other.<At>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Until>k__BackingField, other.<Until>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<PlaceID>k__BackingField, other.<PlaceID>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ItemID>k__BackingField, other.<ItemID>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<KnownSince>k__BackingField, other.<KnownSince>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsLanding>k__BackingField, other.<IsLanding>k__BackingField);
      }

      // Token: 0x06003253 RID: 12883 RVA: 0x0043B150 File Offset: 0x0043B150
      [CompilerGenerated]
      public readonly void Deconstruct(out double At, out double Until, out string PlaceID, [Nullable(2)] out string ItemID)
      {
        At = this.At;
        Until = this.Until;
        PlaceID = this.PlaceID;
        ItemID = this.ItemID;
      }
    }

    // Token: 0x02000509 RID: 1289
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001F2B RID: 7979
      [Nullable(new byte[] { 0, 0, 1 })]
      public static Func<ClickyInkLiveNote.Words, IEnumerable<double>> <0>__Starts;
    }
  }
}
