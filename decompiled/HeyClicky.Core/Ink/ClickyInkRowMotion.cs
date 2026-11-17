using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000165 RID: 357
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkRowMotion : IEquatable<ClickyInkRowMotion>
  {
    // Token: 0x170004F9 RID: 1273
    // (get) Token: 0x0600159B RID: 5531 RVA: 0x003B8FC0 File Offset: 0x003B8FC0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkRowMotion);
      }
    }

    // Token: 0x170004FA RID: 1274
    // (get) Token: 0x0600159C RID: 5532 RVA: 0x003B8FCC File Offset: 0x003B8FCC
    public IReadOnlyList<InkRange> Windows { get; }

    // Token: 0x0600159D RID: 5533 RVA: 0x003B8FD4 File Offset: 0x003B8FD4
    public ClickyInkRowMotion(IEnumerable<InkRange> spans, double lead = 0.6, double tail = 1.2)
    {
      IEnumerable<InkRange> enumerable = from s in spans
        where double.IsFinite(s.LowerBound) && double.IsFinite(s.UpperBound)
        select new InkRange(s.LowerBound - lead, s.UpperBound + tail) into s
        orderby s.LowerBound
        select s;
      List<InkRange> list = new List<InkRange>();
      foreach (InkRange inkRange in enumerable)
      {
        if (list.Count > 0)
        {
          double lowerBound = inkRange.LowerBound;
          List<InkRange> list2 = list;
          if (lowerBound <= list2[list2.Count - 1].UpperBound)
          {
            List<InkRange> list3 = list;
            int num = list3.Count - 1;
            List<InkRange> list4 = list;
            double lowerBound2 = list4[list4.Count - 1].LowerBound;
            List<InkRange> list5 = list;
            list3[num] = new InkRange(lowerBound2, Math.Max(list5[list5.Count - 1].UpperBound, inkRange.UpperBound));
            continue;
          }
        }
        list.Add(inkRange);
      }
      this.Windows = list;
    }

    // Token: 0x0600159E RID: 5534 RVA: 0x003B911C File Offset: 0x003B911C
    public static ClickyInkRowMotion SwitchesAt(IEnumerable<double> times)
    {
      return new ClickyInkRowMotion(times.Select<double, InkRange>((double t) => new InkRange(t, t)), 0.0, 0.0);
    }

    // Token: 0x0600159F RID: 5535 RVA: 0x003B915C File Offset: 0x003B915C
    public ClickyInkRowMotion.State StateAt(double clock)
    {
      int i = 0;
      int num = this.Windows.Count;
      while (i < num)
      {
        int num2 = (i + num) / 2;
        if (this.Windows[num2].LowerBound <= clock)
        {
          i = num2 + 1;
        }
        else
        {
          num = num2;
        }
      }
      if (i == 0)
      {
        return new ClickyInkRowMotion.State.NotYet();
      }
      InkRange inkRange = this.Windows[i - 1];
      if (clock > inkRange.UpperBound)
      {
        return new ClickyInkRowMotion.State.Settled(inkRange.UpperBound);
      }
      return new ClickyInkRowMotion.State.Moving();
    }

    // Token: 0x060015A0 RID: 5536 RVA: 0x003B91D8 File Offset: 0x003B91D8
    public static ClickyInkRowMotion.Table TableFor(ClickyInkLiveNote note)
    {
      object gate = ClickyInkRowMotion.Gate;
      ClickyInkRowMotion.Table table3;
      lock (gate)
      {
        ValueTuple<ClickyInkLiveNote, ClickyInkRowMotion.Table>? valueTuple = ClickyInkRowMotion.last;
        if (valueTuple != null)
        {
          ValueTuple<ClickyInkLiveNote, ClickyInkRowMotion.Table> valueOrDefault = valueTuple.GetValueOrDefault();
          if (valueOrDefault.Item1 == note)
          {
            return valueOrDefault.Item2;
          }
        }
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 7);
        defaultInterpolatedStringHandler.AppendFormatted(string.Join(",", note.Pages.Select<ClickyInkLiveNote.Page, string>((ClickyInkLiveNote.Page p) => p.Id)));
        defaultInterpolatedStringHandler.AppendLiteral("|");
        defaultInterpolatedStringHandler.AppendFormatted(string.Join<int>(",", note.Pages.Select<ClickyInkLiveNote.Page, int>((ClickyInkLiveNote.Page p) => p.Blocks.Count)));
        defaultInterpolatedStringHandler.AppendLiteral("|");
        defaultInterpolatedStringHandler.AppendFormatted<int>(note.VersionCount);
        defaultInterpolatedStringHandler.AppendLiteral("|");
        defaultInterpolatedStringHandler.AppendFormatted(InkText.DoubleDescription(note.PenFreeAt));
        defaultInterpolatedStringHandler.AppendLiteral("|");
        defaultInterpolatedStringHandler.AppendFormatted(InkText.DoubleDescription(note.LastVersionAt.GetValueOrDefault()));
        defaultInterpolatedStringHandler.AppendLiteral("|");
        defaultInterpolatedStringHandler.AppendFormatted<int>(note.EditStarts.Count);
        defaultInterpolatedStringHandler.AppendLiteral("|");
        defaultInterpolatedStringHandler.AppendFormatted(InkText.DoubleDescription((note.VersionArrivals.Count > 0) ? note.VersionArrivals[0] : 0.0));
        string text = defaultInterpolatedStringHandler.ToStringAndClear();
        ClickyInkRowMotion.Table table;
        if (!ClickyInkRowMotion.Cache.TryGetValue(text, out table))
        {
          ClickyInkRowMotion.Table table2 = ClickyInkRowMotion.MakeTable(note, ClickyInkPenSchedule.Of(note)).<Clone>$();
          table2.Key = text;
          table = table2;
          if (ClickyInkRowMotion.Cache.Count > 60)
          {
            ClickyInkRowMotion.Cache.Clear();
          }
          ClickyInkRowMotion.Cache[text] = table;
        }
        ClickyInkRowMotion.last = new ValueTuple<ClickyInkLiveNote, ClickyInkRowMotion.Table>?(new ValueTuple<ClickyInkLiveNote, ClickyInkRowMotion.Table>(note, table));
        table3 = table;
      }
      return table3;
    }

    // Token: 0x060015A1 RID: 5537 RVA: 0x003B9400 File Offset: 0x003B9400
    public static ClickyInkRowMotion.Table MakeTable(ClickyInkLiveNote note, ClickyInkPenSchedule schedule)
    {
      Dictionary<string, ClickyInkRowMotion> dictionary = new Dictionary<string, ClickyInkRowMotion>();
      ClickyInkLiveNote.Words title = note.Title;
      if (title != null)
      {
        dictionary["title"] = new ClickyInkRowMotion(ClickyInkRowMotion.Spans(title), 0.6, 1.2);
      }
      foreach (ClickyInkLiveNote.Page page in note.Pages)
      {
        List<InkRange> list = ClickyInkRowMotion.Spans(page.Name).ToList<InkRange>();
        list.Add(new InkRange(page.LandsAt, page.LandsAt));
        foreach (ClickyInkLiveNote.Block block in page.Blocks)
        {
          list.Add(new InkRange(block.LandsAt, block.LandsAt));
          double? vanishesAt = block.VanishesAt;
          if (vanishesAt != null)
          {
            double valueOrDefault = vanishesAt.GetValueOrDefault();
            list.Add(new InkRange(valueOrDefault, valueOrDefault));
          }
          dictionary[block.Id] = new ClickyInkRowMotion(ClickyInkRowMotion.Spans(block), 0.6, ClickyInkRowMotion.Tail(block));
        }
        dictionary[ClickyInkLiveNote.PageFocusID(page.Id)] = new ClickyInkRowMotion(list, 0.6, 1.2);
      }
      List<double> list2 = schedule.Busy.SelectMany<InkRange, double>((InkRange b) => new double[]
      {
        b.LowerBound,
        b.UpperBound + 0.6
      }).ToList<double>();
      list2.AddRange(schedule.Arrivals.SelectMany<double, double>((double a) => new double[]
      {
        a,
        a + 120.0
      }));
      return new ClickyInkRowMotion.Table(dictionary, ClickyInkRowMotion.SwitchesAt(note.EditStarts), ClickyInkRowMotion.SwitchesAt(list2));
    }

    // Token: 0x060015A2 RID: 5538 RVA: 0x003B9620 File Offset: 0x003B9620
    public static double? SettledClock(ClickyInkLiveNote.Page page, double clock, ClickyInkRowMotion.Table table)
    {
      double num = double.NegativeInfinity;
      ClickyInkRowMotion.State state = table.Switches.StateAt(clock);
      double? num2;
      if (!(state is ClickyInkRowMotion.State.Moving))
      {
        ClickyInkRowMotion.State.Settled settled = state as ClickyInkRowMotion.State.Settled;
        if (settled != null)
        {
          num = settled.At;
        }
        foreach (ClickyInkLiveNote.Block block in page.Blocks)
        {
          ClickyInkRowMotion clickyInkRowMotion;
          if (!table.Places.TryGetValue(block.Id, out clickyInkRowMotion))
          {
            num2 = null;
            return num2;
          }
          state = clickyInkRowMotion.StateAt(clock);
          if (state is ClickyInkRowMotion.State.Moving)
          {
            return null;
          }
          ClickyInkRowMotion.State.Settled settled2 = state as ClickyInkRowMotion.State.Settled;
          if (settled2 != null)
          {
            num = Math.Max(num, settled2.At);
          }
        }
        if (!double.IsFinite(num))
        {
          return null;
        }
        return new double?(num);
      }
      num2 = null;
      return num2;
    }

    // Token: 0x060015A3 RID: 5539 RVA: 0x003B9724 File Offset: 0x003B9724
    public static double Tail(ClickyInkLiveNote.Block block)
    {
      HashSet<ClickyInkDocument.Kind> hashSet = block.Stamps.Select<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>, ClickyInkDocument.Kind>((ClickyInkLiveNote.Stamp<ClickyInkDocument.Block> s) => s.Value.Kind).ToHashSet<ClickyInkDocument.Kind>();
      HashSet<ClickyInkDocument.Kind> hashSet2 = new HashSet<ClickyInkDocument.Kind>(ClickyInkDashboardKinds.DashboardKinds)
      {
        ClickyInkDocument.Kind.Figure,
        ClickyInkDocument.Kind.Tiles,
        ClickyInkDocument.Kind.Drawing
      };
      if (hashSet.Overlaps(hashSet2))
      {
        return 8.0;
      }
      HashSet<ClickyInkDocument.Kind> hashSet3 = new HashSet<ClickyInkDocument.Kind>
      {
        ClickyInkDocument.Kind.Heading,
        ClickyInkDocument.Kind.Paragraph,
        ClickyInkDocument.Kind.Quote,
        ClickyInkDocument.Kind.Note,
        ClickyInkDocument.Kind.List,
        ClickyInkDocument.Kind.Checklist,
        ClickyInkDocument.Kind.Numbered,
        ClickyInkDocument.Kind.Answer,
        ClickyInkDocument.Kind.Code,
        ClickyInkDocument.Kind.Drawing,
        ClickyInkDocument.Kind.Rule,
        ClickyInkDocument.Kind.Unknown
      };
      if (!hashSet.IsSubsetOf(hashSet3))
      {
        return 2.2;
      }
      return 1.2;
    }

    // Token: 0x060015A4 RID: 5540 RVA: 0x003B9820 File Offset: 0x003B9820
    public static List<InkRange> Spans(ClickyInkLiveNote.Block block)
    {
      List<InkRange> list = ClickyInkRowMotion.Spans(block.Words).ToList<InkRange>();
      List<double> list2 = new List<double> { block.LandsAt };
      double? num = block.GoneAt;
      if (num != null)
      {
        double valueOrDefault = num.GetValueOrDefault();
        list2.Add(valueOrDefault);
      }
      num = block.VanishesAt;
      if (num != null)
      {
        double valueOrDefault2 = num.GetValueOrDefault();
        list2.Add(valueOrDefault2);
      }
      list2.AddRange(block.Stamps.Select<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>, double>((ClickyInkLiveNote.Stamp<ClickyInkDocument.Block> s) => s.At));
      list2.AddRange(block.PenArrivals);
      foreach (ClickyInkLiveNote.Item item in block.Items)
      {
        list2.Add(item.LandsAt);
        num = item.GoneAt;
        if (num != null)
        {
          double valueOrDefault3 = num.GetValueOrDefault();
          list2.Add(valueOrDefault3);
        }
        num = item.VanishesAt;
        if (num != null)
        {
          double valueOrDefault4 = num.GetValueOrDefault();
          list2.Add(valueOrDefault4);
        }
        list2.AddRange(item.Stamps.Select<ClickyInkLiveNote.Stamp<ClickyInkDocument.Item>, double>((ClickyInkLiveNote.Stamp<ClickyInkDocument.Item> s) => s.At));
        list.AddRange(ClickyInkRowMotion.Spans(item.Words));
        ClickyInkLiveNote.Words detail = item.Detail;
        if (detail != null)
        {
          list.AddRange(ClickyInkRowMotion.Spans(detail));
        }
        foreach (ClickyInkLiveNote.Words words in item.Cells)
        {
          list.AddRange(ClickyInkRowMotion.Spans(words));
        }
      }
      list.AddRange(list2.Select<double, InkRange>((double m) => new InkRange(m, m)));
      return list;
    }

    // Token: 0x060015A5 RID: 5541 RVA: 0x003B9A54 File Offset: 0x003B9A54
    public static IEnumerable<InkRange> Spans(ClickyInkLiveNote.Words words)
    {
      ClickyInkRowMotion.<Spans>d__22 <Spans>d__ = new ClickyInkRowMotion.<Spans>d__22(-2);
      <Spans>d__.<>3__words = words;
      return <Spans>d__;
    }

    // Token: 0x060015A6 RID: 5542 RVA: 0x003B9A64 File Offset: 0x003B9A64
    [NullableContext(2)]
    public bool Equals(ClickyInkRowMotion other)
    {
      return other != null && this.Windows.SequenceEqual<InkRange>(other.Windows);
    }

    // Token: 0x060015A7 RID: 5543 RVA: 0x003B9A7C File Offset: 0x003B9A7C
    public override int GetHashCode()
    {
      return this.Windows.Count;
    }

    // Token: 0x060015A8 RID: 5544 RVA: 0x003B9A8C File Offset: 0x003B9A8C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkRowMotion");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060015A9 RID: 5545 RVA: 0x003B9AD8 File Offset: 0x003B9AD8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Windows = ");
      builder.Append(this.Windows);
      return true;
    }

    // Token: 0x060015AA RID: 5546 RVA: 0x003B9AFC File Offset: 0x003B9AFC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkRowMotion left, ClickyInkRowMotion right)
    {
      return !(left == right);
    }

    // Token: 0x060015AB RID: 5547 RVA: 0x003B9B08 File Offset: 0x003B9B08
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkRowMotion left, ClickyInkRowMotion right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060015AC RID: 5548 RVA: 0x003B9B1C File Offset: 0x003B9B1C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkRowMotion);
    }

    // Token: 0x060015AE RID: 5550 RVA: 0x003B9B34 File Offset: 0x003B9B34
    [CompilerGenerated]
    private ClickyInkRowMotion(ClickyInkRowMotion original)
    {
      this.Windows = original.<Windows>k__BackingField;
    }

    // Token: 0x0400096C RID: 2412
    public const double LeadSeconds = 0.6;

    // Token: 0x0400096D RID: 2413
    public const double TailSeconds = 1.2;

    // Token: 0x0400096E RID: 2414
    public const double ObjectTailSeconds = 2.2;

    // Token: 0x0400096F RID: 2415
    public const double DashboardTailSeconds = 8.0;

    // Token: 0x04000971 RID: 2417
    private static readonly Dictionary<string, ClickyInkRowMotion.Table> Cache = new Dictionary<string, ClickyInkRowMotion.Table>();

    // Token: 0x04000972 RID: 2418
    [TupleElementNames(new string[] { "Note", "Table" })]
    [Nullable(new byte[] { 0, 1, 1 })]
    private static ValueTuple<ClickyInkLiveNote, ClickyInkRowMotion.Table>? last;

    // Token: 0x04000973 RID: 2419
    private static readonly object Gate = new object();

    // Token: 0x02000524 RID: 1316
    [Nullable(0)]
    public abstract class State : IEquatable<ClickyInkRowMotion.State>
    {
      // Token: 0x17000922 RID: 2338
      // (get) Token: 0x060032BD RID: 12989 RVA: 0x0043C27C File Offset: 0x0043C27C
      [CompilerGenerated]
      protected virtual Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkRowMotion.State);
        }
      }

      // Token: 0x060032BE RID: 12990 RVA: 0x0043C288 File Offset: 0x0043C288
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("State");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060032BF RID: 12991 RVA: 0x0043C2D4 File Offset: 0x0043C2D4
      [CompilerGenerated]
      protected virtual bool PrintMembers(StringBuilder builder)
      {
        return false;
      }

      // Token: 0x060032C0 RID: 12992 RVA: 0x0043C2D8 File Offset: 0x0043C2D8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkRowMotion.State left, ClickyInkRowMotion.State right)
      {
        return !(left == right);
      }

      // Token: 0x060032C1 RID: 12993 RVA: 0x0043C2E4 File Offset: 0x0043C2E4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkRowMotion.State left, ClickyInkRowMotion.State right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060032C2 RID: 12994 RVA: 0x0043C2F8 File Offset: 0x0043C2F8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
      }

      // Token: 0x060032C3 RID: 12995 RVA: 0x0043C30C File Offset: 0x0043C30C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkRowMotion.State);
      }

      // Token: 0x060032C4 RID: 12996 RVA: 0x0043C31C File Offset: 0x0043C31C
      [NullableContext(2)]
      [CompilerGenerated]
      public virtual bool Equals(ClickyInkRowMotion.State other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract);
      }

      // Token: 0x060032C6 RID: 12998 RVA: 0x0043C33C File Offset: 0x0043C33C
      [CompilerGenerated]
      protected State(ClickyInkRowMotion.State original)
      {
      }

      // Token: 0x060032C7 RID: 12999 RVA: 0x0043C344 File Offset: 0x0043C344
      protected State()
      {
      }

      // Token: 0x020006FF RID: 1791
      [Nullable(0)]
      public sealed class Moving : ClickyInkRowMotion.State, IEquatable<ClickyInkRowMotion.State.Moving>
      {
        // Token: 0x17000A11 RID: 2577
        // (get) Token: 0x06003C02 RID: 15362 RVA: 0x00466680 File Offset: 0x00466680
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkRowMotion.State.Moving);
          }
        }

        // Token: 0x06003C03 RID: 15363 RVA: 0x0046668C File Offset: 0x0046668C
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Moving");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003C04 RID: 15364 RVA: 0x004666D8 File Offset: 0x004666D8
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          return base.PrintMembers(builder);
        }

        // Token: 0x06003C05 RID: 15365 RVA: 0x004666E4 File Offset: 0x004666E4
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkRowMotion.State.Moving left, ClickyInkRowMotion.State.Moving right)
        {
          return !(left == right);
        }

        // Token: 0x06003C06 RID: 15366 RVA: 0x004666F0 File Offset: 0x004666F0
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkRowMotion.State.Moving left, ClickyInkRowMotion.State.Moving right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003C07 RID: 15367 RVA: 0x00466704 File Offset: 0x00466704
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode();
        }

        // Token: 0x06003C08 RID: 15368 RVA: 0x0046670C File Offset: 0x0046670C
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkRowMotion.State.Moving);
        }

        // Token: 0x06003C09 RID: 15369 RVA: 0x0046671C File Offset: 0x0046671C
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkRowMotion.State other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003C0A RID: 15370 RVA: 0x00466728 File Offset: 0x00466728
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkRowMotion.State.Moving other)
        {
          return this == other || base.Equals(other);
        }

        // Token: 0x06003C0C RID: 15372 RVA: 0x00466740 File Offset: 0x00466740
        [CompilerGenerated]
        private Moving(ClickyInkRowMotion.State.Moving original)
          : base(original)
        {
        }

        // Token: 0x06003C0D RID: 15373 RVA: 0x0046674C File Offset: 0x0046674C
        public Moving()
        {
        }
      }

      // Token: 0x02000700 RID: 1792
      [Nullable(0)]
      public sealed class Settled : ClickyInkRowMotion.State, IEquatable<ClickyInkRowMotion.State.Settled>
      {
        // Token: 0x06003C0E RID: 15374 RVA: 0x00466754 File Offset: 0x00466754
        public Settled(double At)
        {
          this.At = At;
          base..ctor();
        }

        // Token: 0x17000A12 RID: 2578
        // (get) Token: 0x06003C0F RID: 15375 RVA: 0x00466764 File Offset: 0x00466764
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkRowMotion.State.Settled);
          }
        }

        // Token: 0x17000A13 RID: 2579
        // (get) Token: 0x06003C10 RID: 15376 RVA: 0x00466770 File Offset: 0x00466770
        // (set) Token: 0x06003C11 RID: 15377 RVA: 0x00466778 File Offset: 0x00466778
        public double At { get; set; }

        // Token: 0x06003C12 RID: 15378 RVA: 0x00466784 File Offset: 0x00466784
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Settled");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003C13 RID: 15379 RVA: 0x004667D0 File Offset: 0x004667D0
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("At = ");
          builder.Append(this.At.ToString());
          return true;
        }

        // Token: 0x06003C14 RID: 15380 RVA: 0x0046681C File Offset: 0x0046681C
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkRowMotion.State.Settled left, ClickyInkRowMotion.State.Settled right)
        {
          return !(left == right);
        }

        // Token: 0x06003C15 RID: 15381 RVA: 0x00466828 File Offset: 0x00466828
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkRowMotion.State.Settled left, ClickyInkRowMotion.State.Settled right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003C16 RID: 15382 RVA: 0x0046683C File Offset: 0x0046683C
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<At>k__BackingField);
        }

        // Token: 0x06003C17 RID: 15383 RVA: 0x0046685C File Offset: 0x0046685C
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkRowMotion.State.Settled);
        }

        // Token: 0x06003C18 RID: 15384 RVA: 0x0046686C File Offset: 0x0046686C
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkRowMotion.State other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003C19 RID: 15385 RVA: 0x00466878 File Offset: 0x00466878
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkRowMotion.State.Settled other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<double>.Default.Equals(this.<At>k__BackingField, other.<At>k__BackingField));
        }

        // Token: 0x06003C1B RID: 15387 RVA: 0x004668AC File Offset: 0x004668AC
        [CompilerGenerated]
        private Settled(ClickyInkRowMotion.State.Settled original)
          : base(original)
        {
          this.At = original.<At>k__BackingField;
        }

        // Token: 0x06003C1C RID: 15388 RVA: 0x004668C4 File Offset: 0x004668C4
        [CompilerGenerated]
        public void Deconstruct(out double At)
        {
          At = this.At;
        }
      }

      // Token: 0x02000701 RID: 1793
      [Nullable(0)]
      public sealed class NotYet : ClickyInkRowMotion.State, IEquatable<ClickyInkRowMotion.State.NotYet>
      {
        // Token: 0x17000A14 RID: 2580
        // (get) Token: 0x06003C1D RID: 15389 RVA: 0x004668D0 File Offset: 0x004668D0
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkRowMotion.State.NotYet);
          }
        }

        // Token: 0x06003C1E RID: 15390 RVA: 0x004668DC File Offset: 0x004668DC
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("NotYet");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003C1F RID: 15391 RVA: 0x00466928 File Offset: 0x00466928
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          return base.PrintMembers(builder);
        }

        // Token: 0x06003C20 RID: 15392 RVA: 0x00466934 File Offset: 0x00466934
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkRowMotion.State.NotYet left, ClickyInkRowMotion.State.NotYet right)
        {
          return !(left == right);
        }

        // Token: 0x06003C21 RID: 15393 RVA: 0x00466940 File Offset: 0x00466940
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkRowMotion.State.NotYet left, ClickyInkRowMotion.State.NotYet right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003C22 RID: 15394 RVA: 0x00466954 File Offset: 0x00466954
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode();
        }

        // Token: 0x06003C23 RID: 15395 RVA: 0x0046695C File Offset: 0x0046695C
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkRowMotion.State.NotYet);
        }

        // Token: 0x06003C24 RID: 15396 RVA: 0x0046696C File Offset: 0x0046696C
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkRowMotion.State other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003C25 RID: 15397 RVA: 0x00466978 File Offset: 0x00466978
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkRowMotion.State.NotYet other)
        {
          return this == other || base.Equals(other);
        }

        // Token: 0x06003C27 RID: 15399 RVA: 0x00466990 File Offset: 0x00466990
        [CompilerGenerated]
        private NotYet(ClickyInkRowMotion.State.NotYet original)
          : base(original)
        {
        }

        // Token: 0x06003C28 RID: 15400 RVA: 0x0046699C File Offset: 0x0046699C
        public NotYet()
        {
        }
      }
    }

    // Token: 0x02000525 RID: 1317
    [Nullable(0)]
    public sealed class Table : IEquatable<ClickyInkRowMotion.Table>
    {
      // Token: 0x060032C8 RID: 13000 RVA: 0x0043C34C File Offset: 0x0043C34C
      public Table(IReadOnlyDictionary<string, ClickyInkRowMotion> Places, ClickyInkRowMotion Switches, ClickyInkRowMotion RestingSwitches)
      {
        this.Places = Places;
        this.Switches = Switches;
        this.RestingSwitches = RestingSwitches;
        this.Key = "";
        base..ctor();
      }

      // Token: 0x17000923 RID: 2339
      // (get) Token: 0x060032C9 RID: 13001 RVA: 0x0043C374 File Offset: 0x0043C374
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkRowMotion.Table);
        }
      }

      // Token: 0x17000924 RID: 2340
      // (get) Token: 0x060032CA RID: 13002 RVA: 0x0043C380 File Offset: 0x0043C380
      // (set) Token: 0x060032CB RID: 13003 RVA: 0x0043C388 File Offset: 0x0043C388
      public IReadOnlyDictionary<string, ClickyInkRowMotion> Places { get; set; }

      // Token: 0x17000925 RID: 2341
      // (get) Token: 0x060032CC RID: 13004 RVA: 0x0043C394 File Offset: 0x0043C394
      // (set) Token: 0x060032CD RID: 13005 RVA: 0x0043C39C File Offset: 0x0043C39C
      public ClickyInkRowMotion Switches { get; set; }

      // Token: 0x17000926 RID: 2342
      // (get) Token: 0x060032CE RID: 13006 RVA: 0x0043C3A8 File Offset: 0x0043C3A8
      // (set) Token: 0x060032CF RID: 13007 RVA: 0x0043C3B0 File Offset: 0x0043C3B0
      public ClickyInkRowMotion RestingSwitches { get; set; }

      // Token: 0x17000927 RID: 2343
      // (get) Token: 0x060032D0 RID: 13008 RVA: 0x0043C3BC File Offset: 0x0043C3BC
      // (set) Token: 0x060032D1 RID: 13009 RVA: 0x0043C3C4 File Offset: 0x0043C3C4
      public string Key { get; set; }

      // Token: 0x060032D2 RID: 13010 RVA: 0x0043C3D0 File Offset: 0x0043C3D0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Table");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060032D3 RID: 13011 RVA: 0x0043C41C File Offset: 0x0043C41C
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Places = ");
        builder.Append(this.Places);
        builder.Append(", Switches = ");
        builder.Append(this.Switches);
        builder.Append(", RestingSwitches = ");
        builder.Append(this.RestingSwitches);
        builder.Append(", Key = ");
        builder.Append(this.Key);
        return true;
      }

      // Token: 0x060032D4 RID: 13012 RVA: 0x0043C494 File Offset: 0x0043C494
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkRowMotion.Table left, ClickyInkRowMotion.Table right)
      {
        return !(left == right);
      }

      // Token: 0x060032D5 RID: 13013 RVA: 0x0043C4A0 File Offset: 0x0043C4A0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkRowMotion.Table left, ClickyInkRowMotion.Table right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060032D6 RID: 13014 RVA: 0x0043C4B4 File Offset: 0x0043C4B4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyDictionary<string, ClickyInkRowMotion>>.Default.GetHashCode(this.<Places>k__BackingField)) * -1521134295 + EqualityComparer<ClickyInkRowMotion>.Default.GetHashCode(this.<Switches>k__BackingField)) * -1521134295 + EqualityComparer<ClickyInkRowMotion>.Default.GetHashCode(this.<RestingSwitches>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Key>k__BackingField);
      }

      // Token: 0x060032D7 RID: 13015 RVA: 0x0043C530 File Offset: 0x0043C530
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkRowMotion.Table);
      }

      // Token: 0x060032D8 RID: 13016 RVA: 0x0043C540 File Offset: 0x0043C540
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkRowMotion.Table other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyDictionary<string, ClickyInkRowMotion>>.Default.Equals(this.<Places>k__BackingField, other.<Places>k__BackingField) && EqualityComparer<ClickyInkRowMotion>.Default.Equals(this.<Switches>k__BackingField, other.<Switches>k__BackingField) && EqualityComparer<ClickyInkRowMotion>.Default.Equals(this.<RestingSwitches>k__BackingField, other.<RestingSwitches>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Key>k__BackingField, other.<Key>k__BackingField));
      }

      // Token: 0x060032DA RID: 13018 RVA: 0x0043C5D4 File Offset: 0x0043C5D4
      [CompilerGenerated]
      private Table(ClickyInkRowMotion.Table original)
      {
        this.Places = original.<Places>k__BackingField;
        this.Switches = original.<Switches>k__BackingField;
        this.RestingSwitches = original.<RestingSwitches>k__BackingField;
        this.Key = original.<Key>k__BackingField;
      }

      // Token: 0x060032DB RID: 13019 RVA: 0x0043C60C File Offset: 0x0043C60C
      [CompilerGenerated]
      public void Deconstruct(out IReadOnlyDictionary<string, ClickyInkRowMotion> Places, out ClickyInkRowMotion Switches, out ClickyInkRowMotion RestingSwitches)
      {
        Places = this.Places;
        Switches = this.Switches;
        RestingSwitches = this.RestingSwitches;
      }
    }
  }
}
