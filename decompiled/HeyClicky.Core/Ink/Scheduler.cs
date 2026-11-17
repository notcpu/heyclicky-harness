using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200015F RID: 351
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class Scheduler
  {
    // Token: 0x0600155F RID: 5471 RVA: 0x003B6140 File Offset: 0x003B6140
    public Scheduler(double cursor, double arrival, int? edit, double haste)
    {
      this.Cursor = cursor;
      this.Arrival = arrival;
      this.Edit = edit;
      this.haste = Math.Max(0.6, haste);
    }

    // Token: 0x06001560 RID: 5472 RVA: 0x003B6174 File Offset: 0x003B6174
    public void Pause(double seconds)
    {
      this.Spend(seconds);
    }

    // Token: 0x06001561 RID: 5473 RVA: 0x003B6180 File Offset: 0x003B6180
    private InkRange Spend(double seconds)
    {
      double cursor = this.Cursor;
      this.Cursor += seconds / this.haste;
      return new InkRange(cursor, this.Cursor);
    }

    // Token: 0x06001562 RID: 5474 RVA: 0x003B61A8 File Offset: 0x003B61A8
    private void SpendMotion(double seconds)
    {
      this.Cursor += seconds / Math.Min(this.haste, 1.6);
    }

    // Token: 0x06001563 RID: 5475 RVA: 0x003B61D0 File Offset: 0x003B61D0
    public ClickyInkLiveNote.Words Written(string text, double pace)
    {
      if (text.Length == 0)
      {
        return ClickyInkLiveNote.Words.Empty;
      }
      InkRange? inkRange = new InkRange?(this.Spend(ClickyInkPen.Beats(text) / pace));
      int? edit = this.Edit;
      bool isPartial = this.IsPartial;
      return new ClickyInkLiveNote.Words(new <>z__ReadOnlySingleElementList<ClickyInkLiveNote.Segment>(new ClickyInkLiveNote.Segment(text, inkRange, null, null, edit, null, isPartial)), null, null);
    }

    // Token: 0x06001564 RID: 5476 RVA: 0x003B624C File Offset: 0x003B624C
    public ClickyInkLiveNote.Words Rewrite([Nullable(2)] ClickyInkLiveNote.Words old, string text, double pace)
    {
      Scheduler.<>c__DisplayClass14_0 CS$<>8__locals1 = new Scheduler.<>c__DisplayClass14_0();
      CS$<>8__locals1.<>4__this = this;
      if (old == null || old.Segments.Count == 0)
      {
        return this.Written(text, pace);
      }
      List<ClickyInkLiveNote.Segment> list = old.Segments.Select<ClickyInkLiveNote.Segment, ClickyInkLiveNote.Segment>(delegate(ClickyInkLiveNote.Segment segment)
      {
        if (segment.Struck != null && segment.VanishesAt == null)
        {
          if (CS$<>8__locals1.<>4__this.Edit != null)
          {
            int? struckInEdit = segment.StruckInEdit;
            int? edit = CS$<>8__locals1.<>4__this.Edit;
            if ((struckInEdit.GetValueOrDefault() == edit.GetValueOrDefault()) & (struckInEdit != null == (edit != null)))
            {
              return segment;
            }
          }
          ClickyInkLiveNote.Segment segment9 = segment;
          segment9.VanishesAt = new double?(CS$<>8__locals1.<>4__this.Arrival);
          return segment9;
        }
        return segment;
      }).ToList<ClickyInkLiveNote.Segment>();
      if (old.Current == text)
      {
        return new ClickyInkLiveNote.Words(list, null, null);
      }
      if (ClickyInkParser.Emphasis.Strip(old.Current) == ClickyInkParser.Emphasis.Strip(text))
      {
        List<ClickyInkLiveNote.Segment> list2 = list.Where<ClickyInkLiveNote.Segment>((ClickyInkLiveNote.Segment s) => s.Struck == null && s.VanishesAt == null).ToList<ClickyInkLiveNote.Segment>();
        if (list2.Count == 1)
        {
          List<ClickyInkLiveNote.Segment> list3 = list2;
          int num = 0;
          ClickyInkLiveNote.Segment segment8 = list2[0];
          segment8.Text = text;
          list3[num] = segment8;
          List<ClickyInkLiveNote.Segment> list4 = new List<ClickyInkLiveNote.Segment>();
          list4.AddRange(list2);
          list4.AddRange(list.Where<ClickyInkLiveNote.Segment>((ClickyInkLiveNote.Segment s) => s.VanishesAt != null));
          return new ClickyInkLiveNote.Words(new <>z__ReadOnlyList<ClickyInkLiveNote.Segment>(list4), null, null);
        }
      }
      List<Scheduler.Token> list5 = new List<Scheduler.Token>();
      CS$<>8__locals1.attachments = new Dictionary<int, List<ClickyInkLiveNote.Segment>>();
      foreach (ClickyInkLiveNote.Segment segment2 in list)
      {
        if (segment2.VanishesAt != null || segment2.Struck != null)
        {
          CS$<>8__locals1.<Rewrite>g__Attach|1(list5.Count - 1, segment2);
        }
        else
        {
          List<string> list6 = ClickyInkMatch.Tokens(segment2.Text);
          List<double> list7 = ClickyInkPen.CumulativeBeats(segment2.Text);
          double num2;
          if (list7.Count <= 0)
          {
            num2 = 0.0;
          }
          else
          {
            List<double> list8 = list7;
            num2 = list8[list8.Count - 1];
          }
          double num3 = Math.Max(num2, 1E-06);
          int num4 = 0;
          foreach (string text2 in list6)
          {
            ClickyInkLiveNote.Segment segment8 = segment2;
            segment8.Text = text2;
            ClickyInkLiveNote.Segment segment3 = segment8;
            int num5 = text2.CharacterCount();
            InkRange? written = segment2.Written;
            if (written != null)
            {
              InkRange valueOrDefault = written.GetValueOrDefault();
              double num6 = valueOrDefault.UpperBound - valueOrDefault.LowerBound;
              double num7 = valueOrDefault.LowerBound + num6 * list7[num4] / num3;
              double num8 = valueOrDefault.LowerBound + num6 * list7[Math.Min(list7.Count - 1, num4 + num5)] / num3;
              segment8 = segment3;
              segment8.Written = new InkRange?(new InkRange(num7, Math.Max(num7, num8)));
              segment3 = segment8;
            }
            num4 += num5;
            list5.Add(new Scheduler.Token(text2, segment3));
          }
        }
      }
      List<ValueTuple<string, ClickyInkMatch.Change>> list9 = ClickyInkMatch.Lcs(list5.Select<Scheduler.Token, string>((Scheduler.Token t) => t.Text).ToList<string>(), ClickyInkMatch.Tokens(text));
      if (this.IsPartial)
      {
        int num9 = 0;
        foreach (ValueTuple<string, ClickyInkMatch.Change> valueTuple in list9)
        {
          ClickyInkMatch.Change item = valueTuple.Item2;
          if (item != ClickyInkMatch.Change.Added)
          {
            if (item == ClickyInkMatch.Change.Removed && !list5[num9].Segment.IsPreview)
            {
              return new ClickyInkLiveNote.Words(list, null, null);
            }
            num9++;
          }
        }
      }
      if (this.Edit != null && !this.IsPartial)
      {
        if (Scheduler.IsHeavyRewrite(list5.Select<Scheduler.Token, ValueTuple<string, bool>>((Scheduler.Token t) => new ValueTuple<string, bool>(t.Text, t.Segment.IsPreview)).ToList<ValueTuple<string, bool>>(), list9, text))
        {
          List<ClickyInkLiveNote.Segment> list10 = new List<ClickyInkLiveNote.Segment>(CS$<>8__locals1.<Rewrite>g__Attached|2(-1));
          int num10 = list5.Where<Scheduler.Token>((Scheduler.Token t) => !t.Segment.IsPreview && !InkText.AllWhitespace(t.Text)).Sum<Scheduler.Token>((Scheduler.Token t) => t.Text.CharacterCount());
          double num11 = Math.Min(0.8, Math.Max(0.3, (double)num10 / 90.0));
          for (int i = 0; i < list5.Count; i++)
          {
            Scheduler.Token token = list5[i];
            ClickyInkLiveNote.Segment segment4 = token.Segment;
            if (segment4.IsPreview || InkText.AllWhitespace(token.Text))
            {
              segment4.VanishesAt = new double?(this.Cursor);
            }
            else
            {
              segment4.Struck = new InkRange?(this.Spend(num11 * (double)token.Text.CharacterCount() / (double)Math.Max(1, num10)));
              segment4.StruckInEdit = this.Edit;
            }
            list10.Add(segment4);
            list10.AddRange(CS$<>8__locals1.<Rewrite>g__Attached|2(i));
          }
          int num12 = list10.FindLastIndex((ClickyInkLiveNote.Segment s) => s.VanishesAt == null);
          if (num12 >= 0 && !list10[num12].Text.HasSuffix(" "))
          {
            List<ClickyInkLiveNote.Segment> list11 = list10;
            int num13 = num12;
            ClickyInkLiveNote.Segment segment8 = list10[num12];
            segment8.Text = list10[num12].Text + " ";
            list11[num13] = segment8;
          }
          List<ClickyInkLiveNote.Segment> list12 = list10;
          InkRange? inkRange = new InkRange?(this.Spend(ClickyInkPen.Beats(text) / pace));
          int? num14 = this.Edit;
          list12.Add(new ClickyInkLiveNote.Segment(text, inkRange, null, null, num14, null, false));
          return new ClickyInkLiveNote.Words(ClickyInkMatch.Merged(list10), null, null);
        }
      }
      List<ClickyInkLiveNote.Segment> list13 = new List<ClickyInkLiveNote.Segment>(CS$<>8__locals1.<Rewrite>g__Attached|2(-1));
      int num15 = 0;
      foreach (ValueTuple<string, ClickyInkMatch.Change> valueTuple2 in list9)
      {
        string item2 = valueTuple2.Item1;
        switch (valueTuple2.Item2)
        {
        case ClickyInkMatch.Change.Kept:
        {
          ClickyInkLiveNote.Segment segment5 = list5[num15].Segment;
          segment5.Text = ((segment5.Text.HasSuffix(" ") && !item2.HasSuffix(" ")) ? (item2 + " ") : item2);
          if (!this.IsPartial)
          {
            segment5.IsPreview = false;
          }
          list13.Add(segment5);
          list13.AddRange(CS$<>8__locals1.<Rewrite>g__Attached|2(num15));
          num15++;
          break;
        }
        case ClickyInkMatch.Change.Removed:
          if (list5[num15].Segment.IsPreview)
          {
            ClickyInkLiveNote.Segment segment6 = list5[num15].Segment;
            segment6.VanishesAt = new double?(this.Cursor);
            list13.Add(segment6);
            list13.AddRange(CS$<>8__locals1.<Rewrite>g__Attached|2(num15));
            num15++;
          }
          else if (this.IsPartial)
          {
            list13.Add(list5[num15].Segment);
            list13.AddRange(CS$<>8__locals1.<Rewrite>g__Attached|2(num15));
            num15++;
          }
          else
          {
            ClickyInkLiveNote.Segment segment7 = list5[num15].Segment;
            if (this.Edit != null && !InkText.AllWhitespace(item2))
            {
              segment7.Struck = new InkRange?(this.Spend(0.3 * Math.Min(1.0, (double)item2.CharacterCount() / 6.0 + 0.3)));
              segment7.StruckInEdit = this.Edit;
            }
            else
            {
              segment7.VanishesAt = new double?(this.Cursor);
            }
            list13.Add(segment7);
            list13.AddRange(CS$<>8__locals1.<Rewrite>g__Attached|2(num15));
            num15++;
          }
          break;
        case ClickyInkMatch.Change.Added:
        {
          int num16 = list13.FindLastIndex((ClickyInkLiveNote.Segment s) => s.VanishesAt == null);
          bool flag = !item2.HasPrefix(" ") && num16 >= 0 && !list13[num16].Text.HasSuffix(" ") && list13[num16].Text.Length > 0;
          string text3 = item2;
          if (flag)
          {
            if (list13[num16].Struck != null)
            {
              List<ClickyInkLiveNote.Segment> list14 = list13;
              int num17 = num16;
              ClickyInkLiveNote.Segment segment8 = list13[num16];
              segment8.Text = list13[num16].Text + " ";
              list14[num17] = segment8;
            }
            else
            {
              text3 = " " + item2;
            }
          }
          List<ClickyInkLiveNote.Segment> list15 = list13;
          string text4 = text3;
          InkRange? inkRange2 = new InkRange?(this.Spend(ClickyInkPen.Beats(text3) / pace));
          int? num14 = this.Edit;
          bool isPartial = this.IsPartial;
          list15.Add(new ClickyInkLiveNote.Segment(text4, inkRange2, null, null, num14, null, isPartial));
          break;
        }
        }
      }
      return new ClickyInkLiveNote.Words(ClickyInkMatch.Merged(list13), null, null);
    }

    // Token: 0x170004F5 RID: 1269
    // (get) Token: 0x06001565 RID: 5477 RVA: 0x003B6C78 File Offset: 0x003B6C78
    private bool IsJumpToPosition
    {
      get
      {
        if (this.Edit != null && !this.IsPartial)
        {
          ValueTuple<int, int>? valueTuple = this.Position;
          if (valueTuple != null)
          {
            ValueTuple<int, int> valueOrDefault = valueTuple.GetValueOrDefault();
            valueTuple = this.LastWorked;
            if (valueTuple != null)
            {
              ValueTuple<int, int> valueOrDefault2 = valueTuple.GetValueOrDefault();
              return Math.Abs(valueOrDefault.Item1 - valueOrDefault2.Item1) >= 2 || (valueOrDefault.Item1 == valueOrDefault2.Item1 && Math.Abs(valueOrDefault.Item2 - valueOrDefault2.Item2) >= 4);
            }
          }
        }
        return false;
      }
    }

    // Token: 0x06001566 RID: 5478 RVA: 0x003B6D10 File Offset: 0x003B6D10
    public unsafe ClickyInkLiveNote.Block Write(ClickyInkDocument.Block block, string id)
    {
      ValueTuple<int, int>? position = this.Position;
      if (position != null)
      {
        ValueTuple<int, int> valueOrDefault = position.GetValueOrDefault();
        this.LastWorked = new ValueTuple<int, int>?(valueOrDefault);
      }
      this.Spend(0.1);
      double cursor = this.Cursor;
      if (Scheduler.IsObject(block.Kind))
      {
        this.SpendMotion(0.55);
      }
      else
      {
        this.SpendMotion(0.13999999999999999);
      }
      ClickyInkLiveNote.Words words = this.Written(block.Text, Scheduler.Pace(block.Kind));
      List<ClickyInkLiveNote.Item> list = block.Items.Select<ClickyInkDocument.Item, ClickyInkLiveNote.Item>(delegate(ClickyInkDocument.Item item, int index)
      {
        Scheduler <>4__this = this;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
        defaultInterpolatedStringHandler.AppendFormatted(id);
        defaultInterpolatedStringHandler.AppendLiteral("-i");
        defaultInterpolatedStringHandler.AppendFormatted<int>(index);
        return <>4__this.WriteItem(item, defaultInterpolatedStringHandler.ToStringAndClear(), block.Kind);
      }).ToList<ClickyInkLiveNote.Item>();
      string id2 = id;
      int num = 1;
      List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>> list2 = new List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>>(num);
      CollectionsMarshal.SetCount<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>>(list2, num);
      Span<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>> span = CollectionsMarshal.AsSpan<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>>(list2);
      int num2 = 0;
      *span[num2] = new ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>(cursor, block, this.Edit);
      return new ClickyInkLiveNote.Block(id2, list2, words, list, cursor, this.Edit)
      {
        IsPreview = this.IsPartial
      };
    }

    // Token: 0x06001567 RID: 5479 RVA: 0x003B6E4C File Offset: 0x003B6E4C
    public unsafe ClickyInkLiveNote.Item WriteItem(ClickyInkDocument.Item item, string id, ClickyInkDocument.Kind kind)
    {
      double cursor = this.Cursor;
      this.SpendMotion(0.1);
      bool flag = Scheduler.UsesCells(kind) && item.Cells.Count > 0;
      List<ClickyInkLiveNote.Words> list = (flag ? item.Cells.Select<string, ClickyInkLiveNote.Words>((string c) => this.Written(c, 80.0)).ToList<ClickyInkLiveNote.Words>() : new List<ClickyInkLiveNote.Words>());
      ClickyInkLiveNote.Words words = (flag ? ClickyInkLiveNote.Words.Plain(item.Text) : this.Written(item.Text, 48.0));
      string detail = item.Detail;
      ClickyInkLiveNote.Words words2 = ((detail != null) ? this.Written(detail, 86.4) : null);
      int num = 1;
      List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>> list2 = new List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>>(num);
      CollectionsMarshal.SetCount<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>>(list2, num);
      Span<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>> span = CollectionsMarshal.AsSpan<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>>(list2);
      int num2 = 0;
      *span[num2] = new ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>(cursor, item, this.Edit);
      return new ClickyInkLiveNote.Item(id, list2, words, list, words2, cursor)
      {
        IsPreview = this.IsPartial
      };
    }

    // Token: 0x06001568 RID: 5480 RVA: 0x003B6F44 File Offset: 0x003B6F44
    public ClickyInkLiveNote.Block Update(ClickyInkLiveNote.Block old, ClickyInkDocument.Block block)
    {
      ClickyInkLiveNote.Block block2 = old.Clone();
      if (!this.IsPartial)
      {
        block2.IsPreview = false;
      }
      double cursor = this.Cursor;
      ClickyInkLiveNote.Block block3;
      try
      {
        if (old.Source != block && this.IsJumpToPosition)
        {
          block2.PenArrivals.Add(this.Cursor);
          this.SpendMotion(0.8);
        }
        if (old.Source != block)
        {
          block2.Stamps.Add(new ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>(this.Cursor, block, this.Edit));
        }
        foreach (string text in new string[] { "big", "value" })
        {
          string text2 = old.Source[text];
          if (text2 != null)
          {
            string text3 = block[text];
            if (text3 != null && text2 != text3)
            {
              this.Spend(0.45);
              break;
            }
          }
        }
        block2.Words = this.Rewrite(old.Words, block.Text, Scheduler.Pace(block.Kind));
        HashSet<string> hashSet = new HashSet<string>();
        List<ClickyInkLiveNote.Item> list = new List<ClickyInkLiveNote.Item>();
        List<ClickyInkLiveNote.Item> list2 = old.Items.Where<ClickyInkLiveNote.Item>((ClickyInkLiveNote.Item i) => i.GoneAt == null && i.VanishesAt == null).ToList<ClickyInkLiveNote.Item>();
        Dictionary<int, ClickyInkLiveNote.Item> dictionary = new Dictionary<int, ClickyInkLiveNote.Item>();
        for (int j = 0; j < block.Items.Count; j++)
        {
          ClickyInkDocument.Item item = block.Items[j];
          ValueTuple<ClickyInkLiveNote.Item, double>? valueTuple = null;
          foreach (ClickyInkLiveNote.Item item2 in list2)
          {
            if (!hashSet.Contains(item2.Id))
            {
              double num = ClickyInkMatch.Similarity(item2.Source.Text, item.Text);
              if (item2.Source.Cells.Count > 0 && item.Cells.Count > 0 && item2.Source.Cells[0] == item.Cells[0])
              {
                num += 0.3;
              }
              if (num > ((valueTuple != null) ? valueTuple.GetValueOrDefault().Item2 : 0.4))
              {
                valueTuple = new ValueTuple<ClickyInkLiveNote.Item, double>?(new ValueTuple<ClickyInkLiveNote.Item, double>(item2, num));
              }
            }
          }
          if (valueTuple != null)
          {
            ValueTuple<ClickyInkLiveNote.Item, double> valueOrDefault = valueTuple.GetValueOrDefault();
            dictionary[j] = valueOrDefault.Item1;
            hashSet.Add(valueOrDefault.Item1.Id);
          }
        }
        if (!Scheduler.UsesCells(block.Kind))
        {
          for (int k = 0; k < block.Items.Count; k++)
          {
            if (!dictionary.ContainsKey(k) && k < list2.Count && !hashSet.Contains(list2[k].Id))
            {
              dictionary[k] = list2[k];
              hashSet.Add(list2[k].Id);
            }
          }
        }
        for (int l = 0; l < block.Items.Count; l++)
        {
          ClickyInkDocument.Item item3 = block.Items[l];
          ClickyInkLiveNote.Item match;
          if (dictionary.TryGetValue(l, out match))
          {
            ClickyInkLiveNote.Item item4 = match.Clone();
            if (!this.IsPartial)
            {
              item4.IsPreview = false;
            }
            if (match.Source != item3)
            {
              item4.Stamps.Add(new ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>(this.Cursor, item3, this.Edit));
              bool? isChecked = match.Source.IsChecked;
              bool? isChecked2 = item3.IsChecked;
              if ((isChecked.GetValueOrDefault() == isChecked2.GetValueOrDefault()) & (isChecked != null == (isChecked2 != null)))
              {
                ClickyInkDocument.Mark? mark = match.Source.Mark;
                ClickyInkDocument.Mark? mark2 = item3.Mark;
                if ((mark.GetValueOrDefault() == mark2.GetValueOrDefault()) & (mark != null == (mark2 != null)))
                {
                  goto IL_0443;
                }
              }
              this.Spend(0.3);
            }
            IL_0443:
            if (Scheduler.UsesCells(block.Kind) && item3.Cells.Count > 0 && match.Cells.Count > 0)
            {
              item4.Cells = item3.Cells.Select<string, ClickyInkLiveNote.Words>(delegate(string cell, int cellIndex)
              {
                if (cellIndex >= match.Cells.Count)
                {
                  return this.Written(cell, 80.0);
                }
                return this.Rewrite(match.Cells[cellIndex], cell, 80.0);
              }).ToList<ClickyInkLiveNote.Words>();
              item4.Words = ClickyInkLiveNote.Words.Plain(item3.Text);
            }
            else
            {
              item4.Words = this.Rewrite(match.Words, item3.Text, 48.0);
            }
            string detail = item3.Detail;
            if (detail != null)
            {
              item4.Detail = this.Rewrite(match.Detail, detail, 86.4);
            }
            list.Add(item4);
          }
          else
          {
            List<ClickyInkLiveNote.Item> list3 = list;
            ClickyInkDocument.Item item5 = item3;
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 4);
            defaultInterpolatedStringHandler.AppendFormatted(old.Id);
            defaultInterpolatedStringHandler.AppendLiteral("-e");
            defaultInterpolatedStringHandler.AppendFormatted<int>(this.Edit.GetValueOrDefault());
            defaultInterpolatedStringHandler.AppendLiteral("-");
            defaultInterpolatedStringHandler.AppendFormatted(InkText.DoubleDescription(this.Cursor));
            defaultInterpolatedStringHandler.AppendLiteral("-i");
            defaultInterpolatedStringHandler.AppendFormatted<int>(l);
            list3.Add(this.WriteItem(item5, defaultInterpolatedStringHandler.ToStringAndClear(), block.Kind));
          }
        }
        string text4 = null;
        foreach (ClickyInkLiveNote.Item item6 in old.Items)
        {
          try
          {
            if (!hashSet.Contains(item6.Id))
            {
              ClickyInkLiveNote.Item item7 = item6;
              if (this.IsPartial && item6.GoneAt == null && item6.VanishesAt == null)
              {
                int num2 = Scheduler.ItemPositionAfter(list, text4) ?? list.Count;
                list.Insert(Math.Min(num2, list.Count), item7);
              }
              else
              {
                if (item6.GoneAt != null || item6.VanishesAt != null)
                {
                  bool flag;
                  if (this.Edit != null)
                  {
                    int? goneInEdit = item6.GoneInEdit;
                    int? edit = this.Edit;
                    flag = (goneInEdit.GetValueOrDefault() == edit.GetValueOrDefault()) & (goneInEdit != null == (edit != null));
                  }
                  else
                  {
                    flag = false;
                  }
                  bool flag2 = flag;
                  if (item7.VanishesAt == null && !flag2)
                  {
                    item7 = item7.Clone();
                    item7.VanishesAt = new double?(this.Arrival);
                  }
                }
                else if (this.Edit == null || item6.IsPreview)
                {
                  item7 = item7.Clone();
                  item7.VanishesAt = new double?(this.Cursor);
                }
                else
                {
                  item7 = item7.Clone();
                  item7.GoneAt = new double?(this.Spend(0.3).LowerBound);
                  item7.GoneInEdit = this.Edit;
                }
                int valueOrDefault2 = Scheduler.ItemPositionAfter(list, text4).GetValueOrDefault();
                list.Insert(Math.Min(valueOrDefault2, list.Count), item7);
              }
            }
          }
          finally
          {
            text4 = item6.Id;
          }
        }
        block2.Items = list;
        block3 = block2;
      }
      finally
      {
        if (this.Cursor > cursor)
        {
          ValueTuple<int, int>? position = this.Position;
          if (position != null)
          {
            ValueTuple<int, int> valueOrDefault3 = position.GetValueOrDefault();
            this.LastWorked = new ValueTuple<int, int>?(valueOrDefault3);
          }
        }
      }
      return block3;
    }

    // Token: 0x06001569 RID: 5481 RVA: 0x003B77AC File Offset: 0x003B77AC
    private static int? ItemPositionAfter(List<ClickyInkLiveNote.Item> items, [Nullable(2)] string previousId)
    {
      if (previousId == null)
      {
        return null;
      }
      int num = items.FindIndex((ClickyInkLiveNote.Item i) => i.Id == previousId);
      if (num >= 0)
      {
        return new int?(num + 1);
      }
      return null;
    }

    // Token: 0x0600156A RID: 5482 RVA: 0x003B7804 File Offset: 0x003B7804
    public ClickyInkLiveNote.Block Strike(ClickyInkLiveNote.Block old)
    {
      ClickyInkLiveNote.Block block = old.Clone();
      if (this.Edit == null || old.IsPreview)
      {
        block.VanishesAt = new double?(this.FoldTime ?? this.Cursor);
        return block;
      }
      block.GoneAt = new double?(this.Spend(0.3).LowerBound);
      block.GoneInEdit = this.Edit;
      ClickyInkDocument.Kind kind = old.Source.Kind;
      bool flag = kind == ClickyInkDocument.Kind.Checklist || kind - ClickyInkDocument.Kind.Ask <= 1 || kind == ClickyInkDocument.Kind.Quiet;
      if (flag)
      {
        block.VanishesAt = block.GoneAt;
      }
      return block;
    }

    // Token: 0x0600156B RID: 5483 RVA: 0x003B78C4 File Offset: 0x003B78C4
    public static bool IsHeavyRewrite([TupleElementNames(new string[] { "Text", "IsPreview" })] [Nullable(new byte[] { 1, 0, 1 })] List<ValueTuple<string, bool>> tokens, [TupleElementNames(new string[] { "Piece", "Change" })] [Nullable(new byte[] { 1, 0, 1 })] List<ValueTuple<string, ClickyInkMatch.Change>> diff, string text)
    {
      string text2 = string.Concat(from t in tokens
        where !t.Item2
        select t.Item1);
      if (ClickyInkMatch.Words(text2).Count < 5)
      {
        return false;
      }
      if (ClickyInkMatch.Similarity(text2, text) < 0.3)
      {
        return true;
      }
      int num = 0;
      bool flag = false;
      int num2 = 0;
      using (List<ValueTuple<string, ClickyInkMatch.Change>>.Enumerator enumerator = diff.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          switch (enumerator.Current.Item2)
          {
          case ClickyInkMatch.Change.Kept:
            flag = false;
            break;
          case ClickyInkMatch.Change.Removed:
          {
            ValueTuple<string, bool> valueTuple = tokens[num2];
            if (!valueTuple.Item2 && !InkText.AllWhitespace(valueTuple.Item1))
            {
              if (!flag)
              {
                num++;
              }
              flag = true;
            }
            break;
          }
          case ClickyInkMatch.Change.Added:
            continue;
          }
          num2++;
        }
      }
      return num >= 3;
    }

    // Token: 0x0600156C RID: 5484 RVA: 0x003B79D8 File Offset: 0x003B79D8
    public static bool UsesCells(ClickyInkDocument.Kind kind)
    {
      if (kind <= ClickyInkDocument.Kind.Receipt)
      {
        if (kind != ClickyInkDocument.Kind.Table && kind != ClickyInkDocument.Kind.Receipt)
        {
          goto IL_001E;
        }
      }
      else if (kind != ClickyInkDocument.Kind.Day && kind != ClickyInkDocument.Kind.Chart)
      {
        goto IL_001E;
      }
      return true;
      IL_001E:
      return false;
    }

    // Token: 0x0600156D RID: 5485 RVA: 0x003B7A08 File Offset: 0x003B7A08
    public static bool IsObject(ClickyInkDocument.Kind kind)
    {
      switch (kind)
      {
      case ClickyInkDocument.Kind.Table:
      case ClickyInkDocument.Kind.Figure:
      case ClickyInkDocument.Kind.Sticky:
      case ClickyInkDocument.Kind.Ticket:
      case ClickyInkDocument.Kind.Receipt:
      case ClickyInkDocument.Kind.Photo:
      case ClickyInkDocument.Kind.Place:
      case ClickyInkDocument.Kind.Day:
      case ClickyInkDocument.Kind.Letter:
      case ClickyInkDocument.Kind.Clip:
      case ClickyInkDocument.Kind.File:
      case ClickyInkDocument.Kind.Question:
      case ClickyInkDocument.Kind.Chart:
      case ClickyInkDocument.Kind.Form:
      case ClickyInkDocument.Kind.Connect:
      case ClickyInkDocument.Kind.Items:
      case ClickyInkDocument.Kind.Quiz:
      case ClickyInkDocument.Kind.Todo:
      case ClickyInkDocument.Kind.Gallery:
      case ClickyInkDocument.Kind.Event:
      case ClickyInkDocument.Kind.Criteria:
        return true;
      }
      return false;
    }

    // Token: 0x0600156E RID: 5486 RVA: 0x003B7ABC File Offset: 0x003B7ABC
    public static double Pace(ClickyInkDocument.Kind kind)
    {
      switch (kind)
      {
      case ClickyInkDocument.Kind.Heading:
      case ClickyInkDocument.Kind.Answer:
        return 31.200000000000003;
      case ClickyInkDocument.Kind.Paragraph:
      case ClickyInkDocument.Kind.Quote:
        return 62.400000000000006;
      case ClickyInkDocument.Kind.List:
      case ClickyInkDocument.Kind.Checklist:
      case ClickyInkDocument.Kind.Numbered:
      case ClickyInkDocument.Kind.Table:
        goto IL_0055;
      case ClickyInkDocument.Kind.Code:
        break;
      default:
        if (kind != ClickyInkDocument.Kind.Letter)
        {
          goto IL_0055;
        }
        break;
      }
      return 115.19999999999999;
      IL_0055:
      return 48.0;
    }

    // Token: 0x04000959 RID: 2393
    public double Cursor;

    // Token: 0x0400095A RID: 2394
    public readonly double Arrival;

    // Token: 0x0400095B RID: 2395
    public readonly int? Edit;

    // Token: 0x0400095C RID: 2396
    private readonly double haste;

    // Token: 0x0400095D RID: 2397
    public double? FoldTime;

    // Token: 0x0400095E RID: 2398
    public bool IsPartial;

    // Token: 0x0400095F RID: 2399
    [TupleElementNames(new string[] { "Page", "Block" })]
    [Nullable(0)]
    public ValueTuple<int, int>? Position;

    // Token: 0x04000960 RID: 2400
    [TupleElementNames(new string[] { "Page", "Block" })]
    [Nullable(0)]
    public ValueTuple<int, int>? LastWorked;

    // Token: 0x02000515 RID: 1301
    [Nullable(0)]
    private readonly struct Token : IEquatable<Scheduler.Token>
    {
      // Token: 0x06003282 RID: 12930 RVA: 0x0043BA88 File Offset: 0x0043BA88
      public Token(string Text, ClickyInkLiveNote.Segment Segment)
      {
        this.Text = Text;
        this.Segment = Segment;
      }

      // Token: 0x17000920 RID: 2336
      // (get) Token: 0x06003283 RID: 12931 RVA: 0x0043BA98 File Offset: 0x0043BA98
      // (set) Token: 0x06003284 RID: 12932 RVA: 0x0043BAA0 File Offset: 0x0043BAA0
      public string Text { get; set; }

      // Token: 0x17000921 RID: 2337
      // (get) Token: 0x06003285 RID: 12933 RVA: 0x0043BAAC File Offset: 0x0043BAAC
      // (set) Token: 0x06003286 RID: 12934 RVA: 0x0043BAB4 File Offset: 0x0043BAB4
      public ClickyInkLiveNote.Segment Segment { get; set; }

      // Token: 0x06003287 RID: 12935 RVA: 0x0043BAC0 File Offset: 0x0043BAC0
      [NullableContext(0)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Token");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003288 RID: 12936 RVA: 0x0043BB0C File Offset: 0x0043BB0C
      [NullableContext(0)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("Text = ");
        builder.Append(this.Text);
        builder.Append(", Segment = ");
        builder.Append(this.Segment.ToString());
        return true;
      }

      // Token: 0x06003289 RID: 12937 RVA: 0x0043BB5C File Offset: 0x0043BB5C
      [CompilerGenerated]
      public static bool operator !=(Scheduler.Token left, Scheduler.Token right)
      {
        return !(left == right);
      }

      // Token: 0x0600328A RID: 12938 RVA: 0x0043BB68 File Offset: 0x0043BB68
      [CompilerGenerated]
      public static bool operator ==(Scheduler.Token left, Scheduler.Token right)
      {
        return left.Equals(right);
      }

      // Token: 0x0600328B RID: 12939 RVA: 0x0043BB74 File Offset: 0x0043BB74
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField) * -1521134295 + EqualityComparer<ClickyInkLiveNote.Segment>.Default.GetHashCode(this.<Segment>k__BackingField);
      }

      // Token: 0x0600328C RID: 12940 RVA: 0x0043BBA0 File Offset: 0x0043BBA0
      [NullableContext(0)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is Scheduler.Token && this.Equals((Scheduler.Token)obj);
      }

      // Token: 0x0600328D RID: 12941 RVA: 0x0043BBB8 File Offset: 0x0043BBB8
      [CompilerGenerated]
      public bool Equals(Scheduler.Token other)
      {
        return EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<ClickyInkLiveNote.Segment>.Default.Equals(this.<Segment>k__BackingField, other.<Segment>k__BackingField);
      }

      // Token: 0x0600328E RID: 12942 RVA: 0x0043BBEC File Offset: 0x0043BBEC
      [CompilerGenerated]
      public void Deconstruct(out string Text, out ClickyInkLiveNote.Segment Segment)
      {
        Text = this.Text;
        Segment = this.Segment;
      }
    }
  }
}
