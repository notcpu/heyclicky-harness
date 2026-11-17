using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000161 RID: 353
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkLiveBlockResolving
  {
    // Token: 0x0600157B RID: 5499 RVA: 0x003B8458 File Offset: 0x003B8458
    public unsafe static ClickyInkLiveNote.Block Resolved(this ClickyInkLiveNote.Block block, double clock)
    {
      ClickyInkLiveNote.Block block2 = block.Clone();
      ClickyInkDocument.Block block3 = block.SourceAt(clock);
      ClickyInkDocument.Block block4 = block.PreviousSource(clock);
      if (block4 != null)
      {
        string text = block4["big"] ?? block4["value"];
        if (text != null && text != (block3["big"] ?? block3["value"]))
        {
          block3 = block3.Clone();
          block3.Attributes["_was"] = text;
        }
      }
      List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>> stamps = block.Stamps;
      int num = stamps.FindLastIndex(([Nullable(new byte[] { 0, 1 })] ClickyInkLiveNote.Stamp<ClickyInkDocument.Block> s) => s.At <= clock);
      if (num >= 0)
      {
        int first = num;
        while (first > 0 && ClickyInkLiveBlockResolving.<Resolved>g__Number|0_3(stamps[first - 1].Value) == ClickyInkLiveBlockResolving.<Resolved>g__Number|0_3(stamps[num].Value))
        {
          first--;
        }
        block2.NumberShownSince = new double?(stamps[first].At);
        double nextStamp = ((first + 1 < stamps.Count) ? stamps[first + 1].At : double.PositiveInfinity);
        List<double> list = (from s in block.Words.Segments.Where<ClickyInkLiveNote.Segment>(delegate(ClickyInkLiveNote.Segment s)
          {
            InkRange? written = s.Written;
            if (written != null)
            {
              InkRange valueOrDefault = written.GetValueOrDefault();
              if (valueOrDefault.LowerBound >= stamps[first].At - 0.001)
              {
                return valueOrDefault.LowerBound < nextStamp;
              }
            }
            return false;
          })
          select s.Written.Value.UpperBound).ToList<double>();
        block2.NumberLineWrittenAt = new double?((list.Count > 0) ? list.Max() : (stamps[first].At + 0.45));
      }
      ClickyInkLiveNote.Block block5 = block2;
      int num2 = 1;
      List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>> list2 = new List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>>(num2);
      CollectionsMarshal.SetCount<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>>(list2, num2);
      Span<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>> span = CollectionsMarshal.AsSpan<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>>(list2);
      int num3 = 0;
      *span[num3] = new ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>(ClickyInkLiveBlockResolving.EffectiveSince<ClickyInkDocument.Block>(stamps, clock), block3, null);
      block5.Stamps = list2;
      double num4 = double.PositiveInfinity;
      foreach (ClickyInkLiveNote.Stamp<ClickyInkDocument.Block> stamp in stamps)
      {
        if (stamp.At > clock)
        {
          num4 = stamp.At;
          break;
        }
      }
      ClickyInkLiveNote.Block block6 = block2;
      ClickyInkLiveNote.Words words = block.Words.<Clone>$();
      words.AsOf = new double?(clock);
      words.ReservedUntil = new double?(num4);
      block6.Words = words;
      Func<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>, bool> <>9__6;
      block2.Items = block.Items.Where<ClickyInkLiveNote.Item>((ClickyInkLiveNote.Item i) => i.IsPresent(clock)).Select<ClickyInkLiveNote.Item, ClickyInkLiveNote.Item>(delegate(ClickyInkLiveNote.Item item)
      {
        ClickyInkLiveNote.Item item2 = item.Clone();
        IEnumerable<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>> stamps2 = item.Stamps;
        Func<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>, bool> func;
        if ((func = <>9__6) == null)
        {
          func = (<>9__6 = (ClickyInkLiveNote.Stamp<ClickyInkDocument.Item> s) => s.At <= clock);
        }
        List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>> list3 = stamps2.Where<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>>(func).ToList<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>>();
        ClickyInkLiveNote.Item item3 = item2;
        List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>> list4;
        if (list3.Count != 0)
        {
          list4 = list3;
        }
        else
        {
          int num5 = 1;
          List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>> list5 = new List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>>(num5);
          CollectionsMarshal.SetCount<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>>(list5, num5);
          list4 = list5;
          Span<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>> span2 = CollectionsMarshal.AsSpan<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>>(list5);
          int num6 = 0;
          *span2[num6] = item.Stamps[0];
        }
        item3.Stamps = list4;
        double passEnds = double.PositiveInfinity;
        foreach (ClickyInkLiveNote.Stamp<ClickyInkDocument.Item> stamp2 in item.Stamps)
        {
          if (stamp2.At > clock)
          {
            passEnds = stamp2.At;
            break;
          }
        }
        ClickyInkLiveNote.Item item4 = item2;
        ClickyInkLiveNote.Words words2 = item.Words.<Clone>$();
        words2.AsOf = new double?(clock);
        words2.ReservedUntil = new double?(passEnds);
        item4.Words = words2;
        ClickyInkLiveNote.Item item5 = item2;
        ClickyInkLiveNote.Words words4;
        if (item.Detail != null)
        {
          ClickyInkLiveNote.Words words3 = item.Detail.<Clone>$();
          words3.AsOf = new double?(clock);
          words4 = words3;
          words3.ReservedUntil = new double?(passEnds);
        }
        else
        {
          words4 = null;
        }
        item5.Detail = words4;
        item2.Cells = item.Cells.Select<ClickyInkLiveNote.Words, ClickyInkLiveNote.Words>(delegate(ClickyInkLiveNote.Words cell)
        {
          ClickyInkLiveNote.Words words5 = cell.<Clone>$();
          words5.AsOf = new double?(clock);
          words5.ReservedUntil = new double?(passEnds);
          return words5;
        }).ToList<ClickyInkLiveNote.Words>();
        return item2;
      }).ToList<ClickyInkLiveNote.Item>();
      return block2;
    }

    // Token: 0x0600157C RID: 5500 RVA: 0x003B87CC File Offset: 0x003B87CC
    [NullableContext(2)]
    public static double EffectiveSince<TValue>([Nullable(new byte[] { 1, 0, 1 })] IReadOnlyList<ClickyInkLiveNote.Stamp<TValue>> stamps, double clock)
    {
      for (int i = stamps.Count - 1; i >= 0; i--)
      {
        if (stamps[i].At <= clock)
        {
          return stamps[i].At;
        }
      }
      return stamps[0].At;
    }

    // Token: 0x0600157D RID: 5501 RVA: 0x003B8820 File Offset: 0x003B8820
    public static double ShownSince(this ClickyInkLiveNote.Block block)
    {
      if (block.Stamps.Count <= 0)
      {
        return block.LandsAt;
      }
      List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>> stamps = block.Stamps;
      return stamps[stamps.Count - 1].At;
    }

    // Token: 0x0600157E RID: 5502 RVA: 0x003B8860 File Offset: 0x003B8860
    [return: Nullable(2)]
    public static ClickyInkDocument.Block PreviousSource(this ClickyInkLiveNote.Block block, double clock)
    {
      List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>> stamps = block.Stamps;
      int num = stamps.FindLastIndex(([Nullable(new byte[] { 0, 1 })] ClickyInkLiveNote.Stamp<ClickyInkDocument.Block> s) => s.At <= clock);
      if (num > 0)
      {
        int? num2 = stamps[num].Edit;
        if (num2 != null)
        {
          int valueOrDefault = num2.GetValueOrDefault();
          int i;
          for (i = num - 1; i > 0; i--)
          {
            num2 = stamps[i].Edit;
            int num3 = valueOrDefault;
            if (!((num2.GetValueOrDefault() == num3) & (num2 != null)))
            {
              break;
            }
          }
          return stamps[i].Value;
        }
      }
      return null;
    }

    // Token: 0x0600157F RID: 5503 RVA: 0x003B8910 File Offset: 0x003B8910
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <Resolved>g__Number|0_3(ClickyInkDocument.Block b)
    {
      return b["big"] ?? b["value"];
    }
  }
}
