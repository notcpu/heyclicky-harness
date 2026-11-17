using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200012F RID: 303
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkLiveNoteFolding
  {
    // Token: 0x060013B1 RID: 5041 RVA: 0x003A7C2C File Offset: 0x003A7C2C
    public static ClickyInkLiveNote FoldingStruckWords(this ClickyInkLiveNote note, double delay)
    {
      ClickyInkLiveNoteFolding.<>c__DisplayClass0_0 CS$<>8__locals1 = new ClickyInkLiveNoteFolding.<>c__DisplayClass0_0();
      CS$<>8__locals1.delay = delay;
      ClickyInkLiveNote clickyInkLiveNote = note.Clone();
      ClickyInkLiveNote.Words title = note.Title;
      clickyInkLiveNote.Title = ((title != null) ? CS$<>8__locals1.<FoldingStruckWords>g__Fold|0(title) : null);
      clickyInkLiveNote.Pages = note.Pages.Select<ClickyInkLiveNote.Page, ClickyInkLiveNote.Page>(delegate(ClickyInkLiveNote.Page page)
      {
        ClickyInkLiveNote.Page page2 = page.Clone();
        page2.Name = base.<FoldingStruckWords>g__Fold|0(page.Name);
        IEnumerable<ClickyInkLiveNote.Block> blocks = page.Blocks;
        Func<ClickyInkLiveNote.Block, ClickyInkLiveNote.Block> func;
        if ((func = CS$<>8__locals1.<>9__4) == null)
        {
          func = (CS$<>8__locals1.<>9__4 = delegate(ClickyInkLiveNote.Block block)
          {
            if (block.Source.Kind == ClickyInkDocument.Kind.Answer)
            {
              return block;
            }
            ClickyInkLiveNote.Block block2 = block.Clone();
            block2.Words = base.<FoldingStruckWords>g__Fold|0(block.Words);
            IEnumerable<ClickyInkLiveNote.Item> items = block.Items;
            Func<ClickyInkLiveNote.Item, ClickyInkLiveNote.Item> func2;
            if ((func2 = CS$<>8__locals1.<>9__5) == null)
            {
              func2 = (CS$<>8__locals1.<>9__5 = delegate(ClickyInkLiveNote.Item item)
              {
                ClickyInkLiveNote.Item item2 = item.Clone();
                item2.Words = base.<FoldingStruckWords>g__Fold|0(item.Words);
                ClickyInkLiveNote.Words detail = item.Detail;
                item2.Detail = ((detail != null) ? base.<FoldingStruckWords>g__Fold|0(detail) : null);
                return item2;
              });
            }
            block2.Items = items.Select<ClickyInkLiveNote.Item, ClickyInkLiveNote.Item>(func2).ToList<ClickyInkLiveNote.Item>();
            return block2;
          });
        }
        page2.Blocks = blocks.Select<ClickyInkLiveNote.Block, ClickyInkLiveNote.Block>(func).ToList<ClickyInkLiveNote.Block>();
        return page2;
      }).ToList<ClickyInkLiveNote.Page>();
      return clickyInkLiveNote;
    }

    // Token: 0x060013B2 RID: 5042 RVA: 0x003A7C88 File Offset: 0x003A7C88
    public static ClickyInkLiveNote FoldingStruckHeadlineWords(this ClickyInkLiveNote note, double delay)
    {
      ClickyInkLiveNote clickyInkLiveNote = note.Clone();
      Func<ClickyInkLiveNote.Segment, ClickyInkLiveNote.Segment> <>9__3;
      Func<ClickyInkLiveNote.Block, ClickyInkLiveNote.Block> <>9__1;
      clickyInkLiveNote.Pages = note.Pages.Select<ClickyInkLiveNote.Page, ClickyInkLiveNote.Page>(delegate(ClickyInkLiveNote.Page page)
      {
        ClickyInkLiveNote.Page page2 = page.Clone();
        IEnumerable<ClickyInkLiveNote.Block> blocks = page.Blocks;
        Func<ClickyInkLiveNote.Block, ClickyInkLiveNote.Block> func;
        if ((func = <>9__1) == null)
        {
          func = (<>9__1 = delegate(ClickyInkLiveNote.Block block)
          {
            if (block.Source.Kind == ClickyInkDocument.Kind.Answer)
            {
              if (block.Words.Segments.Any<ClickyInkLiveNote.Segment>((ClickyInkLiveNote.Segment s) => s.Struck != null))
              {
                ClickyInkLiveNote.Block block2 = block.Clone();
                ClickyInkLiveNote.Words words = block.Words.<Clone>$();
                IEnumerable<ClickyInkLiveNote.Segment> segments = block.Words.Segments;
                Func<ClickyInkLiveNote.Segment, ClickyInkLiveNote.Segment> func2;
                if ((func2 = <>9__3) == null)
                {
                  func2 = (<>9__3 = (ClickyInkLiveNote.Segment s) => ClickyInkLiveNoteFolding.FoldSegment(s, delay));
                }
                words.Segments = segments.Select<ClickyInkLiveNote.Segment, ClickyInkLiveNote.Segment>(func2).ToList<ClickyInkLiveNote.Segment>();
                block2.Words = words;
                return block2;
              }
            }
            return block;
          });
        }
        page2.Blocks = blocks.Select<ClickyInkLiveNote.Block, ClickyInkLiveNote.Block>(func).ToList<ClickyInkLiveNote.Block>();
        return page2;
      }).ToList<ClickyInkLiveNote.Page>();
      return clickyInkLiveNote;
    }

    // Token: 0x060013B3 RID: 5043 RVA: 0x003A7CCC File Offset: 0x003A7CCC
    private static ClickyInkLiveNote.Segment FoldSegment(ClickyInkLiveNote.Segment segment, double delay)
    {
      InkRange? struck = segment.Struck;
      if (struck != null)
      {
        InkRange valueOrDefault = struck.GetValueOrDefault();
        ClickyInkLiveNote.Segment segment2 = segment;
        segment2.VanishesAt = new double?(Math.Min(segment.VanishesAt.GetValueOrDefault(double.PositiveInfinity), valueOrDefault.UpperBound + delay));
        return segment2;
      }
      return segment;
    }
  }
}
