using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000146 RID: 326
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ClickyInkDocument : IEquatable<ClickyInkDocument>
  {
    // Token: 0x170004D3 RID: 1235
    // (get) Token: 0x0600148F RID: 5263 RVA: 0x003ADAC8 File Offset: 0x003ADAC8
    // (set) Token: 0x06001490 RID: 5264 RVA: 0x003ADAD0 File Offset: 0x003ADAD0
    public string Title { get; set; }

    // Token: 0x170004D4 RID: 1236
    // (get) Token: 0x06001491 RID: 5265 RVA: 0x003ADADC File Offset: 0x003ADADC
    // (set) Token: 0x06001492 RID: 5266 RVA: 0x003ADAE4 File Offset: 0x003ADAE4
    [Nullable(1)]
    public List<ClickyInkDocument.Page> Pages
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x06001493 RID: 5267 RVA: 0x003ADAF0 File Offset: 0x003ADAF0
    public ClickyInkDocument(string title = null, [Nullable(new byte[] { 2, 1 })] List<ClickyInkDocument.Page> pages = null)
    {
      this.Title = title;
      this.Pages = pages ?? new List<ClickyInkDocument.Page>();
    }

    // Token: 0x170004D5 RID: 1237
    // (get) Token: 0x06001494 RID: 5268 RVA: 0x003ADB10 File Offset: 0x003ADB10
    [Nullable(1)]
    public static ClickyInkDocument Empty
    {
      [NullableContext(1)]
      get
      {
        return new ClickyInkDocument(null, new List<ClickyInkDocument.Page>());
      }
    }

    // Token: 0x170004D6 RID: 1238
    // (get) Token: 0x06001495 RID: 5269 RVA: 0x003ADB20 File Offset: 0x003ADB20
    [TupleElementNames(new string[] { "Page", "Block" })]
    [Nullable(new byte[] { 1, 0, 1 })]
    public List<ValueTuple<int, ClickyInkDocument.Block>> AllBlocks
    {
      [return: TupleElementNames(new string[] { "Page", "Block" })]
      [return: Nullable(new byte[] { 1, 0, 1 })]
      get
      {
        List<ValueTuple<int, ClickyInkDocument.Block>> list = new List<ValueTuple<int, ClickyInkDocument.Block>>();
        for (int i = 0; i < this.Pages.Count; i++)
        {
          foreach (ClickyInkDocument.Block block in this.Pages[i].Blocks)
          {
            list.Add(new ValueTuple<int, ClickyInkDocument.Block>(i, block));
          }
        }
        return list;
      }
    }

    // Token: 0x06001496 RID: 5270 RVA: 0x003ADBA4 File Offset: 0x003ADBA4
    public bool Equals(ClickyInkDocument other)
    {
      return other != null && this.Title == other.Title && this.Pages.SequenceEqual<ClickyInkDocument.Page>(other.Pages);
    }

    // Token: 0x06001497 RID: 5271 RVA: 0x003ADBD0 File Offset: 0x003ADBD0
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkDocument);
    }

    // Token: 0x06001498 RID: 5272 RVA: 0x003ADBE0 File Offset: 0x003ADBE0
    public override int GetHashCode()
    {
      return HashCode.Combine<string, int>(this.Title, this.Pages.Count);
    }

    // Token: 0x06001499 RID: 5273 RVA: 0x003ADBF8 File Offset: 0x003ADBF8
    public static bool operator ==(ClickyInkDocument a, ClickyInkDocument b)
    {
      if (a != null)
      {
        return a.Equals(b);
      }
      return b == null;
    }

    // Token: 0x0600149A RID: 5274 RVA: 0x003ADC0C File Offset: 0x003ADC0C
    public static bool operator !=(ClickyInkDocument a, ClickyInkDocument b)
    {
      return !(a == b);
    }

    // Token: 0x020004CE RID: 1230
    [NullableContext(1)]
    [Nullable(0)]
    public sealed class Page : IEquatable<ClickyInkDocument.Page>
    {
      // Token: 0x17000898 RID: 2200
      // (get) Token: 0x06003025 RID: 12325 RVA: 0x00433B18 File Offset: 0x00433B18
      // (set) Token: 0x06003026 RID: 12326 RVA: 0x00433B20 File Offset: 0x00433B20
      public string Name { get; set; }

      // Token: 0x17000899 RID: 2201
      // (get) Token: 0x06003027 RID: 12327 RVA: 0x00433B2C File Offset: 0x00433B2C
      // (set) Token: 0x06003028 RID: 12328 RVA: 0x00433B34 File Offset: 0x00433B34
      public bool IsBoard { get; set; }

      // Token: 0x1700089A RID: 2202
      // (get) Token: 0x06003029 RID: 12329 RVA: 0x00433B40 File Offset: 0x00433B40
      // (set) Token: 0x0600302A RID: 12330 RVA: 0x00433B48 File Offset: 0x00433B48
      public List<ClickyInkDocument.Block> Blocks { get; set; }

      // Token: 0x0600302B RID: 12331 RVA: 0x00433B54 File Offset: 0x00433B54
      public Page(string name, bool isBoard, List<ClickyInkDocument.Block> blocks)
      {
        this.Name = name;
        this.IsBoard = isBoard;
        this.Blocks = blocks;
      }

      // Token: 0x0600302C RID: 12332 RVA: 0x00433B74 File Offset: 0x00433B74
      [NullableContext(2)]
      public bool Equals(ClickyInkDocument.Page other)
      {
        return other != null && this.Name == other.Name && this.IsBoard == other.IsBoard && this.Blocks.SequenceEqual<ClickyInkDocument.Block>(other.Blocks);
      }

      // Token: 0x0600302D RID: 12333 RVA: 0x00433BB0 File Offset: 0x00433BB0
      [NullableContext(2)]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkDocument.Page);
      }

      // Token: 0x0600302E RID: 12334 RVA: 0x00433BC0 File Offset: 0x00433BC0
      public override int GetHashCode()
      {
        return HashCode.Combine<string, bool, int>(this.Name, this.IsBoard, this.Blocks.Count);
      }
    }

    // Token: 0x020004CF RID: 1231
    [NullableContext(1)]
    [Nullable(0)]
    public sealed class Block : IEquatable<ClickyInkDocument.Block>
    {
      // Token: 0x1700089B RID: 2203
      // (get) Token: 0x0600302F RID: 12335 RVA: 0x00433BE0 File Offset: 0x00433BE0
      // (set) Token: 0x06003030 RID: 12336 RVA: 0x00433BE8 File Offset: 0x00433BE8
      public ClickyInkDocument.Kind Kind { get; set; }

      // Token: 0x1700089C RID: 2204
      // (get) Token: 0x06003031 RID: 12337 RVA: 0x00433BF4 File Offset: 0x00433BF4
      // (set) Token: 0x06003032 RID: 12338 RVA: 0x00433BFC File Offset: 0x00433BFC
      public string Text { get; set; }

      // Token: 0x1700089D RID: 2205
      // (get) Token: 0x06003033 RID: 12339 RVA: 0x00433C08 File Offset: 0x00433C08
      // (set) Token: 0x06003034 RID: 12340 RVA: 0x00433C10 File Offset: 0x00433C10
      public Dictionary<string, string> Attributes { get; set; }

      // Token: 0x1700089E RID: 2206
      // (get) Token: 0x06003035 RID: 12341 RVA: 0x00433C1C File Offset: 0x00433C1C
      // (set) Token: 0x06003036 RID: 12342 RVA: 0x00433C24 File Offset: 0x00433C24
      public List<ClickyInkDocument.Item> Items { get; set; }

      // Token: 0x1700089F RID: 2207
      // (get) Token: 0x06003037 RID: 12343 RVA: 0x00433C30 File Offset: 0x00433C30
      // (set) Token: 0x06003038 RID: 12344 RVA: 0x00433C38 File Offset: 0x00433C38
      public List<string> Columns { get; set; }

      // Token: 0x06003039 RID: 12345 RVA: 0x00433C44 File Offset: 0x00433C44
      public Block(ClickyInkDocument.Kind kind, string text = "", [Nullable(new byte[] { 2, 1, 1 })] Dictionary<string, string> attributes = null, [Nullable(new byte[] { 2, 1 })] List<ClickyInkDocument.Item> items = null, [Nullable(new byte[] { 2, 1 })] List<string> columns = null)
      {
        this.Kind = kind;
        this.Text = text;
        this.Attributes = attributes ?? new Dictionary<string, string>();
        this.Items = items ?? new List<ClickyInkDocument.Item>();
        this.Columns = columns ?? new List<string>();
      }

      // Token: 0x170008A0 RID: 2208
      [Nullable(2)]
      public string this[string key]
      {
        [return: Nullable(2)]
        get
        {
          string text;
          if (!this.Attributes.TryGetValue(key, out text))
          {
            return null;
          }
          return text;
        }
      }

      // Token: 0x0600303B RID: 12347 RVA: 0x00433CB8 File Offset: 0x00433CB8
      public ClickyInkDocument.Block Clone()
      {
        return new ClickyInkDocument.Block(this.Kind, this.Text, new Dictionary<string, string>(this.Attributes), this.Items.Select<ClickyInkDocument.Item, ClickyInkDocument.Item>((ClickyInkDocument.Item i) => i.Clone()).ToList<ClickyInkDocument.Item>(), this.Columns.ToList<string>());
      }

      // Token: 0x0600303C RID: 12348 RVA: 0x00433D1C File Offset: 0x00433D1C
      [NullableContext(2)]
      public bool Equals(ClickyInkDocument.Block other)
      {
        return other != null && this.Kind == other.Kind && this.Text == other.Text && this.Columns.SequenceEqual<string>(other.Columns) && this.Items.SequenceEqual<ClickyInkDocument.Item>(other.Items) && ClickyInkDocument.Block.AttributesEqual(this.Attributes, other.Attributes);
      }

      // Token: 0x0600303D RID: 12349 RVA: 0x00433D88 File Offset: 0x00433D88
      [NullableContext(2)]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkDocument.Block);
      }

      // Token: 0x0600303E RID: 12350 RVA: 0x00433D98 File Offset: 0x00433D98
      public override int GetHashCode()
      {
        return HashCode.Combine<ClickyInkDocument.Kind, string, int, int>(this.Kind, this.Text, this.Items.Count, this.Attributes.Count);
      }

      // Token: 0x0600303F RID: 12351 RVA: 0x00433DC4 File Offset: 0x00433DC4
      [NullableContext(2)]
      public static bool operator ==(ClickyInkDocument.Block a, ClickyInkDocument.Block b)
      {
        if (a != null)
        {
          return a.Equals(b);
        }
        return b == null;
      }

      // Token: 0x06003040 RID: 12352 RVA: 0x00433DD8 File Offset: 0x00433DD8
      [NullableContext(2)]
      public static bool operator !=(ClickyInkDocument.Block a, ClickyInkDocument.Block b)
      {
        return !(a == b);
      }

      // Token: 0x06003041 RID: 12353 RVA: 0x00433DE4 File Offset: 0x00433DE4
      internal static bool AttributesEqual(Dictionary<string, string> a, Dictionary<string, string> b)
      {
        if (a.Count != b.Count)
        {
          return false;
        }
        foreach (KeyValuePair<string, string> keyValuePair in a)
        {
          string text;
          string text2;
          keyValuePair.Deconstruct(out text, out text2);
          string text3 = text;
          string text4 = text2;
          string text5;
          if (!b.TryGetValue(text3, out text5) || text5 != text4)
          {
            return false;
          }
        }
        return true;
      }
    }

    // Token: 0x020004D0 RID: 1232
    [Nullable(0)]
    public sealed class Item : IEquatable<ClickyInkDocument.Item>
    {
      // Token: 0x170008A1 RID: 2209
      // (get) Token: 0x06003042 RID: 12354 RVA: 0x00433E6C File Offset: 0x00433E6C
      // (set) Token: 0x06003043 RID: 12355 RVA: 0x00433E74 File Offset: 0x00433E74
      [Nullable(1)]
      public string Text
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x170008A2 RID: 2210
      // (get) Token: 0x06003044 RID: 12356 RVA: 0x00433E80 File Offset: 0x00433E80
      // (set) Token: 0x06003045 RID: 12357 RVA: 0x00433E88 File Offset: 0x00433E88
      public string Detail { get; set; }

      // Token: 0x170008A3 RID: 2211
      // (get) Token: 0x06003046 RID: 12358 RVA: 0x00433E94 File Offset: 0x00433E94
      // (set) Token: 0x06003047 RID: 12359 RVA: 0x00433E9C File Offset: 0x00433E9C
      [Nullable(1)]
      public List<string> Cells
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x170008A4 RID: 2212
      // (get) Token: 0x06003048 RID: 12360 RVA: 0x00433EA8 File Offset: 0x00433EA8
      // (set) Token: 0x06003049 RID: 12361 RVA: 0x00433EB0 File Offset: 0x00433EB0
      public ClickyInkDocument.Mark? Mark { get; set; }

      // Token: 0x170008A5 RID: 2213
      // (get) Token: 0x0600304A RID: 12362 RVA: 0x00433EBC File Offset: 0x00433EBC
      // (set) Token: 0x0600304B RID: 12363 RVA: 0x00433EC4 File Offset: 0x00433EC4
      public bool? IsChecked { get; set; }

      // Token: 0x170008A6 RID: 2214
      // (get) Token: 0x0600304C RID: 12364 RVA: 0x00433ED0 File Offset: 0x00433ED0
      // (set) Token: 0x0600304D RID: 12365 RVA: 0x00433ED8 File Offset: 0x00433ED8
      public string Url { get; set; }

      // Token: 0x170008A7 RID: 2215
      // (get) Token: 0x0600304E RID: 12366 RVA: 0x00433EE4 File Offset: 0x00433EE4
      // (set) Token: 0x0600304F RID: 12367 RVA: 0x00433EEC File Offset: 0x00433EEC
      public int? Number { get; set; }

      // Token: 0x06003050 RID: 12368 RVA: 0x00433EF8 File Offset: 0x00433EF8
      public Item([Nullable(1)] string text, string detail = null, [Nullable(new byte[] { 2, 1 })] List<string> cells = null, ClickyInkDocument.Mark? mark = null, bool? isChecked = null, string url = null, int? number = null)
      {
        this.Text = text;
        this.Detail = detail;
        this.Cells = cells ?? new List<string>();
        this.Mark = mark;
        this.IsChecked = isChecked;
        this.Url = url;
        this.Number = number;
      }

      // Token: 0x06003051 RID: 12369 RVA: 0x00433F4C File Offset: 0x00433F4C
      [NullableContext(1)]
      public ClickyInkDocument.Item Clone()
      {
        return new ClickyInkDocument.Item(this.Text, this.Detail, this.Cells.ToList<string>(), this.Mark, this.IsChecked, this.Url, this.Number);
      }

      // Token: 0x06003052 RID: 12370 RVA: 0x00433F84 File Offset: 0x00433F84
      public bool Equals(ClickyInkDocument.Item other)
      {
        if (other != null && this.Text == other.Text && this.Detail == other.Detail && this.Cells.SequenceEqual<string>(other.Cells))
        {
          ClickyInkDocument.Mark? mark = this.Mark;
          ClickyInkDocument.Mark? mark2 = other.Mark;
          if ((mark.GetValueOrDefault() == mark2.GetValueOrDefault()) & (mark != null == (mark2 != null)))
          {
            bool? isChecked = this.IsChecked;
            bool? isChecked2 = other.IsChecked;
            if (((isChecked.GetValueOrDefault() == isChecked2.GetValueOrDefault()) & (isChecked != null == (isChecked2 != null))) && this.Url == other.Url)
            {
              int? number = this.Number;
              int? number2 = other.Number;
              return (number.GetValueOrDefault() == number2.GetValueOrDefault()) & (number != null == (number2 != null));
            }
          }
        }
        return false;
      }

      // Token: 0x06003053 RID: 12371 RVA: 0x00434084 File Offset: 0x00434084
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkDocument.Item);
      }

      // Token: 0x06003054 RID: 12372 RVA: 0x00434094 File Offset: 0x00434094
      public override int GetHashCode()
      {
        return HashCode.Combine<string, string, ClickyInkDocument.Mark?, bool?, string, int?>(this.Text, this.Detail, this.Mark, this.IsChecked, this.Url, this.Number);
      }

      // Token: 0x06003055 RID: 12373 RVA: 0x004340C0 File Offset: 0x004340C0
      public static bool operator ==(ClickyInkDocument.Item a, ClickyInkDocument.Item b)
      {
        if (a != null)
        {
          return a.Equals(b);
        }
        return b == null;
      }

      // Token: 0x06003056 RID: 12374 RVA: 0x004340D4 File Offset: 0x004340D4
      public static bool operator !=(ClickyInkDocument.Item a, ClickyInkDocument.Item b)
      {
        return !(a == b);
      }
    }

    // Token: 0x020004D1 RID: 1233
    [NullableContext(0)]
    public enum Mark
    {
      // Token: 0x04001DFF RID: 7679
      New,
      // Token: 0x04001E00 RID: 7680
      Needs,
      // Token: 0x04001E01 RID: 7681
      Done,
      // Token: 0x04001E02 RID: 7682
      Pick,
      // Token: 0x04001E03 RID: 7683
      Now,
      // Token: 0x04001E04 RID: 7684
      Gone
    }

    // Token: 0x020004D2 RID: 1234
    [NullableContext(0)]
    public enum Kind
    {
      // Token: 0x04001E06 RID: 7686
      Heading,
      // Token: 0x04001E07 RID: 7687
      Paragraph,
      // Token: 0x04001E08 RID: 7688
      Answer,
      // Token: 0x04001E09 RID: 7689
      Quote,
      // Token: 0x04001E0A RID: 7690
      List,
      // Token: 0x04001E0B RID: 7691
      Checklist,
      // Token: 0x04001E0C RID: 7692
      Numbered,
      // Token: 0x04001E0D RID: 7693
      Table,
      // Token: 0x04001E0E RID: 7694
      Code,
      // Token: 0x04001E0F RID: 7695
      Rule,
      // Token: 0x04001E10 RID: 7696
      Figure,
      // Token: 0x04001E11 RID: 7697
      Sticky,
      // Token: 0x04001E12 RID: 7698
      Ticket,
      // Token: 0x04001E13 RID: 7699
      Receipt,
      // Token: 0x04001E14 RID: 7700
      Photo,
      // Token: 0x04001E15 RID: 7701
      Place,
      // Token: 0x04001E16 RID: 7702
      Day,
      // Token: 0x04001E17 RID: 7703
      Letter,
      // Token: 0x04001E18 RID: 7704
      Clip,
      // Token: 0x04001E19 RID: 7705
      File,
      // Token: 0x04001E1A RID: 7706
      Link,
      // Token: 0x04001E1B RID: 7707
      Ask,
      // Token: 0x04001E1C RID: 7708
      Question,
      // Token: 0x04001E1D RID: 7709
      Chart,
      // Token: 0x04001E1E RID: 7710
      Note,
      // Token: 0x04001E1F RID: 7711
      Spark,
      // Token: 0x04001E20 RID: 7712
      Status,
      // Token: 0x04001E21 RID: 7713
      Progress,
      // Token: 0x04001E22 RID: 7714
      Tally,
      // Token: 0x04001E23 RID: 7715
      Streak,
      // Token: 0x04001E24 RID: 7716
      Countdown,
      // Token: 0x04001E25 RID: 7717
      Pipeline,
      // Token: 0x04001E26 RID: 7718
      Run,
      // Token: 0x04001E27 RID: 7719
      Quiet,
      // Token: 0x04001E28 RID: 7720
      Review,
      // Token: 0x04001E29 RID: 7721
      Tiles,
      // Token: 0x04001E2A RID: 7722
      Form,
      // Token: 0x04001E2B RID: 7723
      Connect,
      // Token: 0x04001E2C RID: 7724
      Items,
      // Token: 0x04001E2D RID: 7725
      Quiz,
      // Token: 0x04001E2E RID: 7726
      Todo,
      // Token: 0x04001E2F RID: 7727
      Gallery,
      // Token: 0x04001E30 RID: 7728
      Event,
      // Token: 0x04001E31 RID: 7729
      Criteria,
      // Token: 0x04001E32 RID: 7730
      Drawing,
      // Token: 0x04001E33 RID: 7731
      Unknown
    }
  }
}
