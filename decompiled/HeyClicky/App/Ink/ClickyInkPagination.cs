using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using HeyClicky.Core.Ink;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000294 RID: 660
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkPagination
  {
    // Token: 0x06001B5C RID: 7004 RVA: 0x005EDFEC File Offset: 0x005EDFEC
    public static string ItemKey(string blockID, string itemID)
    {
      return blockID + "|" + itemID;
    }

    // Token: 0x06001B5D RID: 7005 RVA: 0x005EDFFC File Offset: 0x005EDFFC
    public unsafe static ClickyInkPagination.PartsInfo Parts(ClickyInkLiveNote note, int pageIndex, ClickyHomeSpreadPaper paper)
    {
      ClickyInkPagination.<>c__DisplayClass4_0 CS$<>8__locals1 = new ClickyInkPagination.<>c__DisplayClass4_0();
      CS$<>8__locals1.paper = paper;
      CS$<>8__locals1.pageIndex = pageIndex;
      CS$<>8__locals1.note = note;
      ClickyInkLiveNote.Page page = CS$<>8__locals1.note.Pages[CS$<>8__locals1.pageIndex];
      ClickyInkPagination.<>c__DisplayClass4_0 CS$<>8__locals2 = CS$<>8__locals1;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 6);
      ClickyInkLiveNote.Words title = CS$<>8__locals1.note.Title;
      defaultInterpolatedStringHandler.AppendFormatted(((title != null) ? title.Current : null) ?? "");
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.note.Pages.Count);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(page.Id);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.note.VersionCount);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(InkText.DoubleDescription(CS$<>8__locals1.note.PenFreeAt));
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<ClickyHomeSpreadPaper>(CS$<>8__locals1.paper);
      CS$<>8__locals2.key = defaultInterpolatedStringHandler.ToStringAndClear();
      ClickyInkPagination.PartsInfo partsInfo;
      if (ClickyInkPagination.Cache.TryGetValue(CS$<>8__locals1.key, out partsInfo))
      {
        return partsInfo;
      }
      CS$<>8__locals1.clock = CS$<>8__locals1.note.FinishedClock + 30.0;
      CS$<>8__locals1.style = new ClickyInkStyle(CS$<>8__locals1.paper.Ink, CS$<>8__locals1.paper.Quiet, new Color?(CS$<>8__locals1.paper.Deep), CS$<>8__locals1.note.LatestEdit(CS$<>8__locals1.clock));
      List<ClickyInkGroup> list = ClickyInkGroup.Groups(page, CS$<>8__locals1.clock);
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      CS$<>8__locals1.part = 0;
      double num = 0.0;
      List<double> list2 = list.Select<ClickyInkGroup, double>((ClickyInkGroup g) => base.<Parts>g__Measure|0(g, 14.0)).ToList<double>();
      double num2 = ClickyInkLayout.PageSize.Height - 26.0 - ClickyInkLayout.ContentTopFor(CS$<>8__locals1.pageIndex == 0 && CS$<>8__locals1.note.Title != null, false);
      double num3 = list2.Sum();
      if (num3 > num2 + 6.0 && num3 <= num2 * 1.3)
      {
        return CS$<>8__locals1.<Parts>g__Store|2(new ClickyInkPagination.PartsInfo(1, page.Blocks.ToDictionary<ClickyInkLiveNote.Block, string, int>((ClickyInkLiveNote.Block b) => b.Id, (ClickyInkLiveNote.Block _) => 0), num2 / num3));
      }
      Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
      for (int i = 0; i < list.Count; i++)
      {
        ClickyInkGroup clickyInkGroup = list[i];
        double num4 = list2[i];
        if (ClickyInkPagination.SplittableKinds.Contains(clickyInkGroup.Kind) && clickyInkGroup.Blocks.Count == 1 && clickyInkGroup.Blocks[0].Items.Count > 1 && num4 > CS$<>8__locals1.<Parts>g__Available|5() - num + 6.0 && num4 > CS$<>8__locals1.<Parts>g__Available|5() - 40.0)
        {
          ClickyInkLiveNote.Block block = clickyInkGroup.Blocks[0];
          for (int j = 0; j < block.Items.Count; j++)
          {
            ClickyInkLiveNote.Item item = block.Items[j];
            ClickyInkLiveNote.Block block2 = block.Clone();
            ClickyInkLiveNote.Block block3 = block2;
            int num5 = 1;
            List<ClickyInkLiveNote.Item> list3 = new List<ClickyInkLiveNote.Item>(num5);
            CollectionsMarshal.SetCount<ClickyInkLiveNote.Item>(list3, num5);
            Span<ClickyInkLiveNote.Item> span = CollectionsMarshal.AsSpan<ClickyInkLiveNote.Item>(list3);
            int num6 = 0;
            *span[num6] = item;
            block3.Items = list3;
            ClickyInkPagination.<>c__DisplayClass4_0 CS$<>8__locals3 = CS$<>8__locals1;
            ClickyInkGroup clickyInkGroup2 = clickyInkGroup.<Clone>$();
            num6 = 1;
            List<ClickyInkLiveNote.Block> list4 = new List<ClickyInkLiveNote.Block>(num6);
            CollectionsMarshal.SetCount<ClickyInkLiveNote.Block>(list4, num6);
            Span<ClickyInkLiveNote.Block> span2 = CollectionsMarshal.AsSpan<ClickyInkLiveNote.Block>(list4);
            num5 = 0;
            *span2[num5] = block2;
            clickyInkGroup2.Blocks = list4;
            double num7 = CS$<>8__locals3.<Parts>g__Measure|0(clickyInkGroup2, 0.0) + ((j == 0) ? 0.0 : 7.0);
            if (num > 0.0 && num + num7 > CS$<>8__locals1.<Parts>g__Available|5() + 6.0)
            {
              CS$<>8__locals1.part++;
              num = 0.0;
            }
            if (j == 0)
            {
              dictionary[block.Id] = CS$<>8__locals1.part;
            }
            dictionary2[ClickyInkPagination.ItemKey(block.Id, item.Id)] = CS$<>8__locals1.part;
            num += num7;
          }
          num += 14.0;
        }
        else
        {
          if (num > 0.0 && num + num4 > CS$<>8__locals1.<Parts>g__Available|5() + 6.0)
          {
            CS$<>8__locals1.part++;
            num = 0.0;
          }
          num += num4;
          foreach (ClickyInkLiveNote.Block block4 in clickyInkGroup.Blocks)
          {
            dictionary[block4.Id] = CS$<>8__locals1.part;
          }
        }
      }
      if (CS$<>8__locals1.part > 0 && num < (ClickyInkLayout.PageSize.Height - 200.0) * 0.28 && num3 <= num2 * 1.45)
      {
        return CS$<>8__locals1.<Parts>g__Store|2(new ClickyInkPagination.PartsInfo(1, page.Blocks.ToDictionary<ClickyInkLiveNote.Block, string, int>((ClickyInkLiveNote.Block b) => b.Id, (ClickyInkLiveNote.Block _) => 0), num2 / num3));
      }
      int num8 = 0;
      foreach (ClickyInkLiveNote.Block block5 in page.Blocks)
      {
        int num9;
        if (dictionary.TryGetValue(block5.Id, out num9))
        {
          num8 = num9;
        }
        else
        {
          dictionary[block5.Id] = num8;
        }
      }
      return CS$<>8__locals1.<Parts>g__Store|2(new ClickyInkPagination.PartsInfo(CS$<>8__locals1.part + 1, dictionary, 1.0)
      {
        ItemPartOf = dictionary2
      });
    }

    // Token: 0x06001B5E RID: 7006 RVA: 0x005EE65C File Offset: 0x005EE65C
    [return: TupleElementNames(new string[] { "Page", "Part" })]
    [return: Nullable(new byte[] { 1, 0 })]
    public static List<ValueTuple<int, int>> Sheets(ClickyInkLiveNote note, ClickyHomeSpreadPaper? paper = null)
    {
      return Enumerable.Range(0, note.Pages.Count).SelectMany<int, ValueTuple<int, int>>((int index) => from p in Enumerable.Range(0, ClickyInkPagination.Parts(note, index, paper ?? ClickyHomeSpreadPaper.Note).Count)
        select new ValueTuple<int, int>(index, p)).ToList<ValueTuple<int, int>>();
    }

    // Token: 0x04000D19 RID: 3353
    private static readonly HashSet<ClickyInkDocument.Kind> SplittableKinds = new HashSet<ClickyInkDocument.Kind>
    {
      ClickyInkDocument.Kind.List,
      ClickyInkDocument.Kind.Checklist,
      ClickyInkDocument.Kind.Numbered
    };

    // Token: 0x04000D1A RID: 3354
    private static readonly Dictionary<string, ClickyInkPagination.PartsInfo> Cache = new Dictionary<string, ClickyInkPagination.PartsInfo>();

    // Token: 0x02000864 RID: 2148
    [Nullable(0)]
    public sealed class PartsInfo : IEquatable<ClickyInkPagination.PartsInfo>
    {
      // Token: 0x060038F1 RID: 14577 RVA: 0x006881C0 File Offset: 0x006881C0
      public PartsInfo(int Count, IReadOnlyDictionary<string, int> PartOf, double Scale = 1.0)
      {
        this.Count = Count;
        this.PartOf = PartOf;
        this.Scale = Scale;
        this.ItemPartOf = new Dictionary<string, int>();
        base..ctor();
      }

      // Token: 0x170007F2 RID: 2034
      // (get) Token: 0x060038F2 RID: 14578 RVA: 0x006881E8 File Offset: 0x006881E8
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkPagination.PartsInfo);
        }
      }

      // Token: 0x170007F3 RID: 2035
      // (get) Token: 0x060038F3 RID: 14579 RVA: 0x006881F4 File Offset: 0x006881F4
      // (set) Token: 0x060038F4 RID: 14580 RVA: 0x006881FC File Offset: 0x006881FC
      public int Count { get; set; }

      // Token: 0x170007F4 RID: 2036
      // (get) Token: 0x060038F5 RID: 14581 RVA: 0x00688208 File Offset: 0x00688208
      // (set) Token: 0x060038F6 RID: 14582 RVA: 0x00688210 File Offset: 0x00688210
      public IReadOnlyDictionary<string, int> PartOf { get; set; }

      // Token: 0x170007F5 RID: 2037
      // (get) Token: 0x060038F7 RID: 14583 RVA: 0x0068821C File Offset: 0x0068821C
      // (set) Token: 0x060038F8 RID: 14584 RVA: 0x00688224 File Offset: 0x00688224
      public double Scale { get; set; }

      // Token: 0x170007F6 RID: 2038
      // (get) Token: 0x060038F9 RID: 14585 RVA: 0x00688230 File Offset: 0x00688230
      // (set) Token: 0x060038FA RID: 14586 RVA: 0x00688238 File Offset: 0x00688238
      public IReadOnlyDictionary<string, int> ItemPartOf { get; set; }

      // Token: 0x060038FB RID: 14587 RVA: 0x00688244 File Offset: 0x00688244
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("PartsInfo");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060038FC RID: 14588 RVA: 0x00688290 File Offset: 0x00688290
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Count = ");
        builder.Append(this.Count.ToString());
        builder.Append(", PartOf = ");
        builder.Append(this.PartOf);
        builder.Append(", Scale = ");
        builder.Append(this.Scale.ToString());
        builder.Append(", ItemPartOf = ");
        builder.Append(this.ItemPartOf);
        return true;
      }

      // Token: 0x060038FD RID: 14589 RVA: 0x00688324 File Offset: 0x00688324
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkPagination.PartsInfo left, ClickyInkPagination.PartsInfo right)
      {
        return !(left == right);
      }

      // Token: 0x060038FE RID: 14590 RVA: 0x00688330 File Offset: 0x00688330
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkPagination.PartsInfo left, ClickyInkPagination.PartsInfo right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060038FF RID: 14591 RVA: 0x00688344 File Offset: 0x00688344
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Count>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyDictionary<string, int>>.Default.GetHashCode(this.<PartOf>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Scale>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyDictionary<string, int>>.Default.GetHashCode(this.<ItemPartOf>k__BackingField);
      }

      // Token: 0x06003900 RID: 14592 RVA: 0x006883C0 File Offset: 0x006883C0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkPagination.PartsInfo);
      }

      // Token: 0x06003901 RID: 14593 RVA: 0x006883D0 File Offset: 0x006883D0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkPagination.PartsInfo other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<Count>k__BackingField, other.<Count>k__BackingField) && EqualityComparer<IReadOnlyDictionary<string, int>>.Default.Equals(this.<PartOf>k__BackingField, other.<PartOf>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Scale>k__BackingField, other.<Scale>k__BackingField) && EqualityComparer<IReadOnlyDictionary<string, int>>.Default.Equals(this.<ItemPartOf>k__BackingField, other.<ItemPartOf>k__BackingField));
      }

      // Token: 0x06003903 RID: 14595 RVA: 0x00688464 File Offset: 0x00688464
      [CompilerGenerated]
      private PartsInfo(ClickyInkPagination.PartsInfo original)
      {
        this.Count = original.<Count>k__BackingField;
        this.PartOf = original.<PartOf>k__BackingField;
        this.Scale = original.<Scale>k__BackingField;
        this.ItemPartOf = original.<ItemPartOf>k__BackingField;
      }

      // Token: 0x06003904 RID: 14596 RVA: 0x0068849C File Offset: 0x0068849C
      [CompilerGenerated]
      public void Deconstruct(out int Count, out IReadOnlyDictionary<string, int> PartOf, out double Scale)
      {
        Count = this.Count;
        PartOf = this.PartOf;
        Scale = this.Scale;
      }
    }
  }
}
