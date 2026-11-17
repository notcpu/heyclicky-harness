using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200012E RID: 302
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkDashboardSplit
  {
    // Token: 0x060013AC RID: 5036 RVA: 0x003A7828 File Offset: 0x003A7828
    public static bool IsWide(ClickyInkDashboardBoard.Section section)
    {
      return section.Groups.Any<ClickyInkGroup>(delegate(ClickyInkGroup group)
      {
        if (group.Kind == ClickyInkDocument.Kind.Table)
        {
          return Math.Max(group.Blocks[0].Source.Columns.Count, group.Blocks[0].Source.Items.Select<ClickyInkDocument.Item, int>((ClickyInkDocument.Item i) => i.Cells.Count).DefaultIfEmpty(0).Max()) >= 4;
        }
        return false;
      });
    }

    // Token: 0x060013AD RID: 5037 RVA: 0x003A7854 File Offset: 0x003A7854
    public static ClickyInkDashboardSplit.Rows Plan(ClickyInkLiveNote note, Func<ClickyInkGroup, double> groupHeight)
    {
      ClickyInkDashboardSplit.<>c__DisplayClass4_0 CS$<>8__locals1 = new ClickyInkDashboardSplit.<>c__DisplayClass4_0();
      CS$<>8__locals1.groupHeight = groupHeight;
      double num = note.FinishedClock + 30.0;
      CS$<>8__locals1.board = new ClickyInkDashboardBoard(note, num, "This run");
      ClickyInkDashboardSplit.<>c__DisplayClass4_0 CS$<>8__locals2 = CS$<>8__locals1;
      ClickyInkDashboardBoard.Section move = CS$<>8__locals1.board.Move;
      CS$<>8__locals2.moveHeight = ((move != null) ? CS$<>8__locals1.<Plan>g__Height|0(move.Groups) : 0.0);
      CS$<>8__locals1.rows = new List<ClickyInkDashboardSplit.Row>();
      CS$<>8__locals1.run = new List<ClickyInkDashboardBoard.Section>();
      CS$<>8__locals1.moveIsPlaced = false;
      CS$<>8__locals1.columnHeights = new Dictionary<int, ValueTuple<double, double>>();
      CS$<>8__locals1.sectionHeights = new Dictionary<string, double>();
      foreach (ClickyInkDashboardBoard.Section section in CS$<>8__locals1.board.Sections)
      {
        if (!CS$<>8__locals1.sectionHeights.ContainsKey(section.Id))
        {
          CS$<>8__locals1.sectionHeights[section.Id] = CS$<>8__locals1.<Plan>g__Height|0(section.Groups);
        }
      }
      CS$<>8__locals1.boardHeight = CS$<>8__locals1.sectionHeights.Values.Sum() + CS$<>8__locals1.moveHeight;
      foreach (ClickyInkDashboardBoard.Section section2 in CS$<>8__locals1.board.Sections)
      {
        if (ClickyInkDashboardSplit.IsWide(section2) || CS$<>8__locals1.<Plan>g__IsTall|2(section2))
        {
          CS$<>8__locals1.<Plan>g__CloseRun|1();
          CS$<>8__locals1.rows.Add(new ClickyInkDashboardSplit.Row.Wide(section2.Id));
        }
        else
        {
          CS$<>8__locals1.run.Add(section2);
        }
      }
      CS$<>8__locals1.<Plan>g__CloseRun|1();
      return new ClickyInkDashboardSplit.Rows(CS$<>8__locals1.rows);
    }

    // Token: 0x060013AE RID: 5038 RVA: 0x003A7A18 File Offset: 0x003A7A18
    public static string Key(ClickyInkLiveNote note)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
      defaultInterpolatedStringHandler.AppendFormatted(string.Join(",", note.Pages.Select<ClickyInkLiveNote.Page, string>((ClickyInkLiveNote.Page p) => p.Id)));
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<int>(note.VersionCount);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(InkText.DoubleDescription(note.PenFreeAt));
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x060013AF RID: 5039 RVA: 0x003A7AA8 File Offset: 0x003A7AA8
    private static bool IsListKind(ClickyInkDocument.Kind kind)
    {
      return kind - ClickyInkDocument.Kind.List <= 2;
    }

    // Token: 0x060013B0 RID: 5040 RVA: 0x003A7AC4 File Offset: 0x003A7AC4
    public unsafe static List<ClickyInkGroup> SpreadingLongList(List<ClickyInkGroup> groups)
    {
      if (groups.Count == 1)
      {
        ClickyInkGroup clickyInkGroup = groups[0];
        if (clickyInkGroup != null && ClickyInkDashboardSplit.IsListKind(clickyInkGroup.Kind) && clickyInkGroup.Blocks[0].Items.Count >= 4)
        {
          ClickyInkLiveNote.Block block = clickyInkGroup.Blocks[0];
          int num = (block.Items.Count + 1) / 2;
          ClickyInkLiveNote.Block block2 = block.Clone();
          block2.Items = block.Items.Take<ClickyInkLiveNote.Item>(num).ToList<ClickyInkLiveNote.Item>();
          ClickyInkLiveNote.Block block3 = block.Clone();
          block3.Items = block.Items.Skip<ClickyInkLiveNote.Item>(num).ToList<ClickyInkLiveNote.Item>();
          block3.Id = block.Id + "-second-half";
          block3.Words = ClickyInkLiveNote.Words.Empty;
          int num2 = 2;
          List<ClickyInkGroup> list = new List<ClickyInkGroup>(num2);
          CollectionsMarshal.SetCount<ClickyInkGroup>(list, num2);
          Span<ClickyInkGroup> span = CollectionsMarshal.AsSpan<ClickyInkGroup>(list);
          int num3 = 0;
          ref ClickyInkGroup ptr = ref span[num3];
          ClickyInkDocument.Kind kind = clickyInkGroup.Kind;
          int num4 = 1;
          List<ClickyInkLiveNote.Block> list2 = new List<ClickyInkLiveNote.Block>(num4);
          CollectionsMarshal.SetCount<ClickyInkLiveNote.Block>(list2, num4);
          Span<ClickyInkLiveNote.Block> span2 = CollectionsMarshal.AsSpan<ClickyInkLiveNote.Block>(list2);
          int num5 = 0;
          *span2[num5] = block2;
          ptr = new ClickyInkGroup(kind, list2);
          num3++;
          ref ClickyInkGroup ptr2 = ref span[num3];
          ClickyInkDocument.Kind kind2 = clickyInkGroup.Kind;
          num5 = 1;
          List<ClickyInkLiveNote.Block> list3 = new List<ClickyInkLiveNote.Block>(num5);
          CollectionsMarshal.SetCount<ClickyInkLiveNote.Block>(list3, num5);
          span2 = CollectionsMarshal.AsSpan<ClickyInkLiveNote.Block>(list3);
          num4 = 0;
          *span2[num4] = block3;
          ptr2 = new ClickyInkGroup(kind2, list3);
          return list;
        }
      }
      return groups;
    }

    // Token: 0x020004AA RID: 1194
    [Nullable(0)]
    public abstract class Row : IEquatable<ClickyInkDashboardSplit.Row>
    {
      // Token: 0x1700088C RID: 2188
      // (get) Token: 0x06002F85 RID: 12165 RVA: 0x00432448 File Offset: 0x00432448
      [CompilerGenerated]
      protected virtual Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkDashboardSplit.Row);
        }
      }

      // Token: 0x06002F86 RID: 12166 RVA: 0x00432454 File Offset: 0x00432454
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Row");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002F87 RID: 12167 RVA: 0x004324A0 File Offset: 0x004324A0
      [CompilerGenerated]
      protected virtual bool PrintMembers(StringBuilder builder)
      {
        return false;
      }

      // Token: 0x06002F88 RID: 12168 RVA: 0x004324A4 File Offset: 0x004324A4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkDashboardSplit.Row left, ClickyInkDashboardSplit.Row right)
      {
        return !(left == right);
      }

      // Token: 0x06002F89 RID: 12169 RVA: 0x004324B0 File Offset: 0x004324B0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkDashboardSplit.Row left, ClickyInkDashboardSplit.Row right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002F8A RID: 12170 RVA: 0x004324C4 File Offset: 0x004324C4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
      }

      // Token: 0x06002F8B RID: 12171 RVA: 0x004324D8 File Offset: 0x004324D8
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkDashboardSplit.Row);
      }

      // Token: 0x06002F8C RID: 12172 RVA: 0x004324E8 File Offset: 0x004324E8
      [NullableContext(2)]
      [CompilerGenerated]
      public virtual bool Equals(ClickyInkDashboardSplit.Row other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract);
      }

      // Token: 0x06002F8E RID: 12174 RVA: 0x00432508 File Offset: 0x00432508
      [CompilerGenerated]
      protected Row(ClickyInkDashboardSplit.Row original)
      {
      }

      // Token: 0x06002F8F RID: 12175 RVA: 0x00432510 File Offset: 0x00432510
      protected Row()
      {
      }

      // Token: 0x020006F2 RID: 1778
      [Nullable(0)]
      public sealed class Columns : ClickyInkDashboardSplit.Row, IEquatable<ClickyInkDashboardSplit.Row.Columns>
      {
        // Token: 0x06003B86 RID: 15238 RVA: 0x0046562C File Offset: 0x0046562C
        public Columns(List<string> Left, List<string> Right, bool HasMove, int MoveOnRight = 2147483647)
        {
          this.Left = Left;
          this.Right = Right;
          this.HasMove = HasMove;
          this.MoveOnRight = MoveOnRight;
          base..ctor();
        }

        // Token: 0x17000A03 RID: 2563
        // (get) Token: 0x06003B87 RID: 15239 RVA: 0x00465654 File Offset: 0x00465654
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkDashboardSplit.Row.Columns);
          }
        }

        // Token: 0x17000A04 RID: 2564
        // (get) Token: 0x06003B88 RID: 15240 RVA: 0x00465660 File Offset: 0x00465660
        // (set) Token: 0x06003B89 RID: 15241 RVA: 0x00465668 File Offset: 0x00465668
        public List<string> Left { get; set; }

        // Token: 0x17000A05 RID: 2565
        // (get) Token: 0x06003B8A RID: 15242 RVA: 0x00465674 File Offset: 0x00465674
        // (set) Token: 0x06003B8B RID: 15243 RVA: 0x0046567C File Offset: 0x0046567C
        public List<string> Right { get; set; }

        // Token: 0x17000A06 RID: 2566
        // (get) Token: 0x06003B8C RID: 15244 RVA: 0x00465688 File Offset: 0x00465688
        // (set) Token: 0x06003B8D RID: 15245 RVA: 0x00465690 File Offset: 0x00465690
        public bool HasMove { get; set; }

        // Token: 0x17000A07 RID: 2567
        // (get) Token: 0x06003B8E RID: 15246 RVA: 0x0046569C File Offset: 0x0046569C
        // (set) Token: 0x06003B8F RID: 15247 RVA: 0x004656A4 File Offset: 0x004656A4
        public int MoveOnRight { get; set; }

        // Token: 0x06003B90 RID: 15248 RVA: 0x004656B0 File Offset: 0x004656B0
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Columns");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003B91 RID: 15249 RVA: 0x004656FC File Offset: 0x004656FC
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Left = ");
          builder.Append(this.Left);
          builder.Append(", Right = ");
          builder.Append(this.Right);
          builder.Append(", HasMove = ");
          builder.Append(this.HasMove.ToString());
          builder.Append(", MoveOnRight = ");
          builder.Append(this.MoveOnRight.ToString());
          return true;
        }

        // Token: 0x06003B92 RID: 15250 RVA: 0x004657A0 File Offset: 0x004657A0
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkDashboardSplit.Row.Columns left, ClickyInkDashboardSplit.Row.Columns right)
        {
          return !(left == right);
        }

        // Token: 0x06003B93 RID: 15251 RVA: 0x004657AC File Offset: 0x004657AC
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkDashboardSplit.Row.Columns left, ClickyInkDashboardSplit.Row.Columns right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003B94 RID: 15252 RVA: 0x004657C0 File Offset: 0x004657C0
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return (((base.GetHashCode() * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(this.<Left>k__BackingField)) * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(this.<Right>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<HasMove>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MoveOnRight>k__BackingField);
        }

        // Token: 0x06003B95 RID: 15253 RVA: 0x00465830 File Offset: 0x00465830
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkDashboardSplit.Row.Columns);
        }

        // Token: 0x06003B96 RID: 15254 RVA: 0x00465840 File Offset: 0x00465840
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkDashboardSplit.Row other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003B97 RID: 15255 RVA: 0x0046584C File Offset: 0x0046584C
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkDashboardSplit.Row.Columns other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<List<string>>.Default.Equals(this.<Left>k__BackingField, other.<Left>k__BackingField) && EqualityComparer<List<string>>.Default.Equals(this.<Right>k__BackingField, other.<Right>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<HasMove>k__BackingField, other.<HasMove>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MoveOnRight>k__BackingField, other.<MoveOnRight>k__BackingField));
        }

        // Token: 0x06003B99 RID: 15257 RVA: 0x004658D0 File Offset: 0x004658D0
        [CompilerGenerated]
        private Columns(ClickyInkDashboardSplit.Row.Columns original)
          : base(original)
        {
          this.Left = original.<Left>k__BackingField;
          this.Right = original.<Right>k__BackingField;
          this.HasMove = original.<HasMove>k__BackingField;
          this.MoveOnRight = original.<MoveOnRight>k__BackingField;
        }

        // Token: 0x06003B9A RID: 15258 RVA: 0x0046590C File Offset: 0x0046590C
        [CompilerGenerated]
        public void Deconstruct(out List<string> Left, out List<string> Right, out bool HasMove, out int MoveOnRight)
        {
          Left = this.Left;
          Right = this.Right;
          HasMove = this.HasMove;
          MoveOnRight = this.MoveOnRight;
        }
      }

      // Token: 0x020006F3 RID: 1779
      [Nullable(0)]
      public sealed class Wide : ClickyInkDashboardSplit.Row, IEquatable<ClickyInkDashboardSplit.Row.Wide>
      {
        // Token: 0x06003B9B RID: 15259 RVA: 0x00465930 File Offset: 0x00465930
        public Wide(string Id)
        {
          this.Id = Id;
          base..ctor();
        }

        // Token: 0x17000A08 RID: 2568
        // (get) Token: 0x06003B9C RID: 15260 RVA: 0x00465940 File Offset: 0x00465940
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkDashboardSplit.Row.Wide);
          }
        }

        // Token: 0x17000A09 RID: 2569
        // (get) Token: 0x06003B9D RID: 15261 RVA: 0x0046594C File Offset: 0x0046594C
        // (set) Token: 0x06003B9E RID: 15262 RVA: 0x00465954 File Offset: 0x00465954
        public string Id { get; set; }

        // Token: 0x06003B9F RID: 15263 RVA: 0x00465960 File Offset: 0x00465960
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Wide");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003BA0 RID: 15264 RVA: 0x004659AC File Offset: 0x004659AC
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Id = ");
          builder.Append(this.Id);
          return true;
        }

        // Token: 0x06003BA1 RID: 15265 RVA: 0x004659E0 File Offset: 0x004659E0
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkDashboardSplit.Row.Wide left, ClickyInkDashboardSplit.Row.Wide right)
        {
          return !(left == right);
        }

        // Token: 0x06003BA2 RID: 15266 RVA: 0x004659EC File Offset: 0x004659EC
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkDashboardSplit.Row.Wide left, ClickyInkDashboardSplit.Row.Wide right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003BA3 RID: 15267 RVA: 0x00465A00 File Offset: 0x00465A00
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField);
        }

        // Token: 0x06003BA4 RID: 15268 RVA: 0x00465A20 File Offset: 0x00465A20
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkDashboardSplit.Row.Wide);
        }

        // Token: 0x06003BA5 RID: 15269 RVA: 0x00465A30 File Offset: 0x00465A30
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkDashboardSplit.Row other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003BA6 RID: 15270 RVA: 0x00465A3C File Offset: 0x00465A3C
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkDashboardSplit.Row.Wide other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField));
        }

        // Token: 0x06003BA8 RID: 15272 RVA: 0x00465A70 File Offset: 0x00465A70
        [CompilerGenerated]
        private Wide(ClickyInkDashboardSplit.Row.Wide original)
          : base(original)
        {
          this.Id = original.<Id>k__BackingField;
        }

        // Token: 0x06003BA9 RID: 15273 RVA: 0x00465A88 File Offset: 0x00465A88
        [CompilerGenerated]
        public void Deconstruct(out string Id)
        {
          Id = this.Id;
        }
      }
    }

    // Token: 0x020004AB RID: 1195
    [Nullable(0)]
    public abstract class Placed : IEquatable<ClickyInkDashboardSplit.Placed>
    {
      // Token: 0x1700088D RID: 2189
      // (get) Token: 0x06002F90 RID: 12176 RVA: 0x00432518 File Offset: 0x00432518
      [CompilerGenerated]
      protected virtual Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkDashboardSplit.Placed);
        }
      }

      // Token: 0x06002F91 RID: 12177 RVA: 0x00432524 File Offset: 0x00432524
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Placed");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002F92 RID: 12178 RVA: 0x00432570 File Offset: 0x00432570
      [CompilerGenerated]
      protected virtual bool PrintMembers(StringBuilder builder)
      {
        return false;
      }

      // Token: 0x06002F93 RID: 12179 RVA: 0x00432574 File Offset: 0x00432574
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkDashboardSplit.Placed left, ClickyInkDashboardSplit.Placed right)
      {
        return !(left == right);
      }

      // Token: 0x06002F94 RID: 12180 RVA: 0x00432580 File Offset: 0x00432580
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkDashboardSplit.Placed left, ClickyInkDashboardSplit.Placed right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002F95 RID: 12181 RVA: 0x00432594 File Offset: 0x00432594
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
      }

      // Token: 0x06002F96 RID: 12182 RVA: 0x004325A8 File Offset: 0x004325A8
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkDashboardSplit.Placed);
      }

      // Token: 0x06002F97 RID: 12183 RVA: 0x004325B8 File Offset: 0x004325B8
      [NullableContext(2)]
      [CompilerGenerated]
      public virtual bool Equals(ClickyInkDashboardSplit.Placed other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract);
      }

      // Token: 0x06002F99 RID: 12185 RVA: 0x004325D8 File Offset: 0x004325D8
      [CompilerGenerated]
      protected Placed(ClickyInkDashboardSplit.Placed original)
      {
      }

      // Token: 0x06002F9A RID: 12186 RVA: 0x004325E0 File Offset: 0x004325E0
      protected Placed()
      {
      }

      // Token: 0x020006F4 RID: 1780
      [Nullable(0)]
      public sealed class Columns : ClickyInkDashboardSplit.Placed, IEquatable<ClickyInkDashboardSplit.Placed.Columns>
      {
        // Token: 0x06003BAA RID: 15274 RVA: 0x00465A94 File Offset: 0x00465A94
        public Columns(List<ClickyInkDashboardBoard.Section> Left, List<ClickyInkDashboardBoard.Section> Right, bool HasMove, int MoveOnRight)
        {
          this.Left = Left;
          this.Right = Right;
          this.HasMove = HasMove;
          this.MoveOnRight = MoveOnRight;
          base..ctor();
        }

        // Token: 0x17000A0A RID: 2570
        // (get) Token: 0x06003BAB RID: 15275 RVA: 0x00465ABC File Offset: 0x00465ABC
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkDashboardSplit.Placed.Columns);
          }
        }

        // Token: 0x17000A0B RID: 2571
        // (get) Token: 0x06003BAC RID: 15276 RVA: 0x00465AC8 File Offset: 0x00465AC8
        // (set) Token: 0x06003BAD RID: 15277 RVA: 0x00465AD0 File Offset: 0x00465AD0
        public List<ClickyInkDashboardBoard.Section> Left { get; set; }

        // Token: 0x17000A0C RID: 2572
        // (get) Token: 0x06003BAE RID: 15278 RVA: 0x00465ADC File Offset: 0x00465ADC
        // (set) Token: 0x06003BAF RID: 15279 RVA: 0x00465AE4 File Offset: 0x00465AE4
        public List<ClickyInkDashboardBoard.Section> Right { get; set; }

        // Token: 0x17000A0D RID: 2573
        // (get) Token: 0x06003BB0 RID: 15280 RVA: 0x00465AF0 File Offset: 0x00465AF0
        // (set) Token: 0x06003BB1 RID: 15281 RVA: 0x00465AF8 File Offset: 0x00465AF8
        public bool HasMove { get; set; }

        // Token: 0x17000A0E RID: 2574
        // (get) Token: 0x06003BB2 RID: 15282 RVA: 0x00465B04 File Offset: 0x00465B04
        // (set) Token: 0x06003BB3 RID: 15283 RVA: 0x00465B0C File Offset: 0x00465B0C
        public int MoveOnRight { get; set; }

        // Token: 0x06003BB4 RID: 15284 RVA: 0x00465B18 File Offset: 0x00465B18
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Columns");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003BB5 RID: 15285 RVA: 0x00465B64 File Offset: 0x00465B64
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Left = ");
          builder.Append(this.Left);
          builder.Append(", Right = ");
          builder.Append(this.Right);
          builder.Append(", HasMove = ");
          builder.Append(this.HasMove.ToString());
          builder.Append(", MoveOnRight = ");
          builder.Append(this.MoveOnRight.ToString());
          return true;
        }

        // Token: 0x06003BB6 RID: 15286 RVA: 0x00465C08 File Offset: 0x00465C08
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkDashboardSplit.Placed.Columns left, ClickyInkDashboardSplit.Placed.Columns right)
        {
          return !(left == right);
        }

        // Token: 0x06003BB7 RID: 15287 RVA: 0x00465C14 File Offset: 0x00465C14
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkDashboardSplit.Placed.Columns left, ClickyInkDashboardSplit.Placed.Columns right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003BB8 RID: 15288 RVA: 0x00465C28 File Offset: 0x00465C28
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return (((base.GetHashCode() * -1521134295 + EqualityComparer<List<ClickyInkDashboardBoard.Section>>.Default.GetHashCode(this.<Left>k__BackingField)) * -1521134295 + EqualityComparer<List<ClickyInkDashboardBoard.Section>>.Default.GetHashCode(this.<Right>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<HasMove>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MoveOnRight>k__BackingField);
        }

        // Token: 0x06003BB9 RID: 15289 RVA: 0x00465C98 File Offset: 0x00465C98
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkDashboardSplit.Placed.Columns);
        }

        // Token: 0x06003BBA RID: 15290 RVA: 0x00465CA8 File Offset: 0x00465CA8
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkDashboardSplit.Placed other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003BBB RID: 15291 RVA: 0x00465CB4 File Offset: 0x00465CB4
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkDashboardSplit.Placed.Columns other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<List<ClickyInkDashboardBoard.Section>>.Default.Equals(this.<Left>k__BackingField, other.<Left>k__BackingField) && EqualityComparer<List<ClickyInkDashboardBoard.Section>>.Default.Equals(this.<Right>k__BackingField, other.<Right>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<HasMove>k__BackingField, other.<HasMove>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MoveOnRight>k__BackingField, other.<MoveOnRight>k__BackingField));
        }

        // Token: 0x06003BBD RID: 15293 RVA: 0x00465D38 File Offset: 0x00465D38
        [CompilerGenerated]
        private Columns(ClickyInkDashboardSplit.Placed.Columns original)
          : base(original)
        {
          this.Left = original.<Left>k__BackingField;
          this.Right = original.<Right>k__BackingField;
          this.HasMove = original.<HasMove>k__BackingField;
          this.MoveOnRight = original.<MoveOnRight>k__BackingField;
        }

        // Token: 0x06003BBE RID: 15294 RVA: 0x00465D74 File Offset: 0x00465D74
        [CompilerGenerated]
        public void Deconstruct(out List<ClickyInkDashboardBoard.Section> Left, out List<ClickyInkDashboardBoard.Section> Right, out bool HasMove, out int MoveOnRight)
        {
          Left = this.Left;
          Right = this.Right;
          HasMove = this.HasMove;
          MoveOnRight = this.MoveOnRight;
        }
      }

      // Token: 0x020006F5 RID: 1781
      [Nullable(0)]
      public sealed class Wide : ClickyInkDashboardSplit.Placed, IEquatable<ClickyInkDashboardSplit.Placed.Wide>
      {
        // Token: 0x06003BBF RID: 15295 RVA: 0x00465D98 File Offset: 0x00465D98
        public Wide(ClickyInkDashboardBoard.Section Section)
        {
          this.Section = Section;
          base..ctor();
        }

        // Token: 0x17000A0F RID: 2575
        // (get) Token: 0x06003BC0 RID: 15296 RVA: 0x00465DA8 File Offset: 0x00465DA8
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkDashboardSplit.Placed.Wide);
          }
        }

        // Token: 0x17000A10 RID: 2576
        // (get) Token: 0x06003BC1 RID: 15297 RVA: 0x00465DB4 File Offset: 0x00465DB4
        // (set) Token: 0x06003BC2 RID: 15298 RVA: 0x00465DBC File Offset: 0x00465DBC
        public ClickyInkDashboardBoard.Section Section { get; set; }

        // Token: 0x06003BC3 RID: 15299 RVA: 0x00465DC8 File Offset: 0x00465DC8
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Wide");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003BC4 RID: 15300 RVA: 0x00465E14 File Offset: 0x00465E14
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Section = ");
          builder.Append(this.Section);
          return true;
        }

        // Token: 0x06003BC5 RID: 15301 RVA: 0x00465E48 File Offset: 0x00465E48
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkDashboardSplit.Placed.Wide left, ClickyInkDashboardSplit.Placed.Wide right)
        {
          return !(left == right);
        }

        // Token: 0x06003BC6 RID: 15302 RVA: 0x00465E54 File Offset: 0x00465E54
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkDashboardSplit.Placed.Wide left, ClickyInkDashboardSplit.Placed.Wide right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003BC7 RID: 15303 RVA: 0x00465E68 File Offset: 0x00465E68
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<ClickyInkDashboardBoard.Section>.Default.GetHashCode(this.<Section>k__BackingField);
        }

        // Token: 0x06003BC8 RID: 15304 RVA: 0x00465E88 File Offset: 0x00465E88
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkDashboardSplit.Placed.Wide);
        }

        // Token: 0x06003BC9 RID: 15305 RVA: 0x00465E98 File Offset: 0x00465E98
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkDashboardSplit.Placed other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003BCA RID: 15306 RVA: 0x00465EA4 File Offset: 0x00465EA4
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkDashboardSplit.Placed.Wide other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<ClickyInkDashboardBoard.Section>.Default.Equals(this.<Section>k__BackingField, other.<Section>k__BackingField));
        }

        // Token: 0x06003BCC RID: 15308 RVA: 0x00465ED8 File Offset: 0x00465ED8
        [CompilerGenerated]
        private Wide(ClickyInkDashboardSplit.Placed.Wide original)
          : base(original)
        {
          this.Section = original.<Section>k__BackingField;
        }

        // Token: 0x06003BCD RID: 15309 RVA: 0x00465EF0 File Offset: 0x00465EF0
        [CompilerGenerated]
        public void Deconstruct(out ClickyInkDashboardBoard.Section Section)
        {
          Section = this.Section;
        }
      }
    }

    // Token: 0x020004AC RID: 1196
    [Nullable(0)]
    public sealed class Rows : IEquatable<ClickyInkDashboardSplit.Rows>
    {
      // Token: 0x06002F9B RID: 12187 RVA: 0x004325E8 File Offset: 0x004325E8
      public Rows(List<ClickyInkDashboardSplit.Row> Items)
      {
        this.Items = Items;
        base..ctor();
      }

      // Token: 0x1700088E RID: 2190
      // (get) Token: 0x06002F9C RID: 12188 RVA: 0x004325F8 File Offset: 0x004325F8
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkDashboardSplit.Rows);
        }
      }

      // Token: 0x1700088F RID: 2191
      // (get) Token: 0x06002F9D RID: 12189 RVA: 0x00432604 File Offset: 0x00432604
      // (set) Token: 0x06002F9E RID: 12190 RVA: 0x0043260C File Offset: 0x0043260C
      public List<ClickyInkDashboardSplit.Row> Items { get; set; }

      // Token: 0x06002F9F RID: 12191 RVA: 0x00432618 File Offset: 0x00432618
      public List<ClickyInkDashboardSplit.Placed> Placing(IReadOnlyList<ClickyInkDashboardBoard.Section> sections)
      {
        Dictionary<string, ClickyInkDashboardBoard.Section> byID = new Dictionary<string, ClickyInkDashboardBoard.Section>();
        foreach (ClickyInkDashboardBoard.Section section in sections)
        {
          byID.TryAdd(section.Id, section);
        }
        HashSet<string> known = new HashSet<string>(this.Items.SelectMany<ClickyInkDashboardSplit.Row, string>(delegate(ClickyInkDashboardSplit.Row row)
        {
          ClickyInkDashboardSplit.Row.Columns columns2 = row as ClickyInkDashboardSplit.Row.Columns;
          IEnumerable<string> enumerable3;
          if (columns2 == null)
          {
            ClickyInkDashboardSplit.Row.Wide wide2 = row as ClickyInkDashboardSplit.Row.Wide;
            if (wide2 == null)
            {
              enumerable3 = Array.Empty<string>();
            }
            else
            {
              enumerable3 = new <>z__ReadOnlySingleElementList<string>(wide2.Id);
            }
          }
          else
          {
            enumerable3 = columns2.Left.Concat<string>(columns2.Right);
          }
          return enumerable3;
        }));
        List<ClickyInkDashboardBoard.Section> list = sections.Where<ClickyInkDashboardBoard.Section>((ClickyInkDashboardBoard.Section s) => !known.Contains(s.Id)).ToList<ClickyInkDashboardBoard.Section>();
        List<ClickyInkDashboardSplit.Placed> list2 = new List<ClickyInkDashboardSplit.Placed>();
        Func<string, ClickyInkDashboardBoard.Section> <>9__2;
        Func<string, ClickyInkDashboardBoard.Section> <>9__3;
        foreach (ClickyInkDashboardSplit.Row row2 in this.Items)
        {
          ClickyInkDashboardSplit.Row.Columns columns = row2 as ClickyInkDashboardSplit.Row.Columns;
          if (columns == null)
          {
            ClickyInkDashboardSplit.Row.Wide wide = row2 as ClickyInkDashboardSplit.Row.Wide;
            if (wide != null)
            {
              ClickyInkDashboardBoard.Section section2;
              if (byID.TryGetValue(wide.Id, out section2))
              {
                list2.Add(new ClickyInkDashboardSplit.Placed.Wide(section2));
              }
            }
          }
          else
          {
            IEnumerable<string> enumerable = columns.Left.Where<string>(new Func<string, bool>(byID.ContainsKey));
            Func<string, ClickyInkDashboardBoard.Section> func;
            if ((func = <>9__2) == null)
            {
              func = (<>9__2 = (string id) => byID[id]);
            }
            List<ClickyInkDashboardBoard.Section> list3 = enumerable.Select<string, ClickyInkDashboardBoard.Section>(func).Concat<ClickyInkDashboardBoard.Section>(list).ToList<ClickyInkDashboardBoard.Section>();
            list = new List<ClickyInkDashboardBoard.Section>();
            List<ClickyInkDashboardSplit.Placed> list4 = list2;
            List<ClickyInkDashboardBoard.Section> list5 = list3;
            IEnumerable<string> enumerable2 = columns.Right.Where<string>(new Func<string, bool>(byID.ContainsKey));
            Func<string, ClickyInkDashboardBoard.Section> func2;
            if ((func2 = <>9__3) == null)
            {
              func2 = (<>9__3 = (string id) => byID[id]);
            }
            list4.Add(new ClickyInkDashboardSplit.Placed.Columns(list5, enumerable2.Select<string, ClickyInkDashboardBoard.Section>(func2).ToList<ClickyInkDashboardBoard.Section>(), columns.HasMove, columns.MoveOnRight));
          }
        }
        if (list.Count > 0)
        {
          list2.Add(new ClickyInkDashboardSplit.Placed.Columns(list, new List<ClickyInkDashboardBoard.Section>(), false, int.MaxValue));
        }
        return list2;
      }

      // Token: 0x06002FA0 RID: 12192 RVA: 0x00432854 File Offset: 0x00432854
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Rows");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002FA1 RID: 12193 RVA: 0x004328A0 File Offset: 0x004328A0
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Items = ");
        builder.Append(this.Items);
        return true;
      }

      // Token: 0x06002FA2 RID: 12194 RVA: 0x004328C4 File Offset: 0x004328C4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkDashboardSplit.Rows left, ClickyInkDashboardSplit.Rows right)
      {
        return !(left == right);
      }

      // Token: 0x06002FA3 RID: 12195 RVA: 0x004328D0 File Offset: 0x004328D0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkDashboardSplit.Rows left, ClickyInkDashboardSplit.Rows right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002FA4 RID: 12196 RVA: 0x004328E4 File Offset: 0x004328E4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<List<ClickyInkDashboardSplit.Row>>.Default.GetHashCode(this.<Items>k__BackingField);
      }

      // Token: 0x06002FA5 RID: 12197 RVA: 0x00432910 File Offset: 0x00432910
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkDashboardSplit.Rows);
      }

      // Token: 0x06002FA6 RID: 12198 RVA: 0x00432920 File Offset: 0x00432920
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkDashboardSplit.Rows other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<List<ClickyInkDashboardSplit.Row>>.Default.Equals(this.<Items>k__BackingField, other.<Items>k__BackingField));
      }

      // Token: 0x06002FA8 RID: 12200 RVA: 0x00432960 File Offset: 0x00432960
      [CompilerGenerated]
      private Rows(ClickyInkDashboardSplit.Rows original)
      {
        this.Items = original.<Items>k__BackingField;
      }

      // Token: 0x06002FA9 RID: 12201 RVA: 0x00432974 File Offset: 0x00432974
      [CompilerGenerated]
      public void Deconstruct(out List<ClickyInkDashboardSplit.Row> Items)
      {
        Items = this.Items;
      }
    }
  }
}
