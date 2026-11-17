using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000170 RID: 368
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkTableLayout : IEquatable<ClickyInkTableLayout>
  {
    // Token: 0x06001671 RID: 5745 RVA: 0x003BE4D4 File Offset: 0x003BE4D4
    public ClickyInkTableLayout(IReadOnlyList<double> Widths, IReadOnlyList<bool> Rigid, double FontScale)
    {
      this.Widths = Widths;
      this.Rigid = Rigid;
      this.FontScale = FontScale;
      base..ctor();
    }

    // Token: 0x17000526 RID: 1318
    // (get) Token: 0x06001672 RID: 5746 RVA: 0x003BE4F4 File Offset: 0x003BE4F4
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkTableLayout);
      }
    }

    // Token: 0x17000527 RID: 1319
    // (get) Token: 0x06001673 RID: 5747 RVA: 0x003BE500 File Offset: 0x003BE500
    // (set) Token: 0x06001674 RID: 5748 RVA: 0x003BE508 File Offset: 0x003BE508
    public IReadOnlyList<double> Widths { get; set; }

    // Token: 0x17000528 RID: 1320
    // (get) Token: 0x06001675 RID: 5749 RVA: 0x003BE514 File Offset: 0x003BE514
    // (set) Token: 0x06001676 RID: 5750 RVA: 0x003BE51C File Offset: 0x003BE51C
    public IReadOnlyList<bool> Rigid { get; set; }

    // Token: 0x17000529 RID: 1321
    // (get) Token: 0x06001677 RID: 5751 RVA: 0x003BE528 File Offset: 0x003BE528
    // (set) Token: 0x06001678 RID: 5752 RVA: 0x003BE530 File Offset: 0x003BE530
    public double FontScale { get; set; }

    // Token: 0x06001679 RID: 5753 RVA: 0x003BE53C File Offset: 0x003BE53C
    public static bool IsShortValue(string cell)
    {
      string text = ClickyInkParser.Emphasis.Strip(cell).TrimWs();
      int num = text.CharacterCount();
      return num <= 10 || (num <= 16 && ClickyInkNumber.Parse(text) != null);
    }

    // Token: 0x0600167A RID: 5754 RVA: 0x003BE580 File Offset: 0x003BE580
    public static ClickyInkTableLayout Make(IReadOnlyList<string> columns, IReadOnlyList<IReadOnlyList<string>> rows, double width, ClickyInkTableLayout.Measurer measure)
    {
      string[] array = new string[5];
      array[0] = string.Join<string>('\u001f', columns);
      array[1] = "\u001e";
      array[2] = string.Join<string>('\u001e', rows.Select<IReadOnlyList<string>, string>((IReadOnlyList<string> r) => string.Join<string>('\u001f', r)));
      array[3] = "\u001d";
      array[4] = InkText.DoubleDescription(width);
      string text = string.Concat(array);
      Dictionary<string, ClickyInkTableLayout> dictionary = ClickyInkTableLayout.Made;
      lock (dictionary)
      {
        ClickyInkTableLayout clickyInkTableLayout;
        if (ClickyInkTableLayout.Made.TryGetValue(text, out clickyInkTableLayout))
        {
          return clickyInkTableLayout;
        }
      }
      ClickyInkTableLayout clickyInkTableLayout2 = ClickyInkTableLayout.Measured(columns, rows, width, measure);
      dictionary = ClickyInkTableLayout.Made;
      lock (dictionary)
      {
        if (ClickyInkTableLayout.Made.Count > 64)
        {
          ClickyInkTableLayout.Made.Clear();
        }
        ClickyInkTableLayout.Made[text] = clickyInkTableLayout2;
      }
      return clickyInkTableLayout2;
    }

    // Token: 0x0600167B RID: 5755 RVA: 0x003BE688 File Offset: 0x003BE688
    [NullableContext(2)]
    public bool Equals(ClickyInkTableLayout other)
    {
      return other != null && this.Widths.SequenceEqual<double>(other.Widths) && this.Rigid.SequenceEqual<bool>(other.Rigid) && this.FontScale == other.FontScale;
    }

    // Token: 0x0600167C RID: 5756 RVA: 0x003BE6C4 File Offset: 0x003BE6C4
    public override int GetHashCode()
    {
      return HashCode.Combine<int, double>(this.Widths.Count, this.FontScale);
    }

    // Token: 0x0600167D RID: 5757 RVA: 0x003BE6DC File Offset: 0x003BE6DC
    private static ClickyInkTableLayout Measured(IReadOnlyList<string> columns, IReadOnlyList<IReadOnlyList<string>> rows, double width, ClickyInkTableLayout.Measurer measureFont)
    {
      ClickyInkTableLayout.<>c__DisplayClass21_0 CS$<>8__locals1 = new ClickyInkTableLayout.<>c__DisplayClass21_0();
      CS$<>8__locals1.rows = rows;
      CS$<>8__locals1.measureFont = measureFont;
      CS$<>8__locals1.width = width;
      int count = columns.Count;
      int num;
      if (CS$<>8__locals1.rows.Count != 0)
      {
        num = CS$<>8__locals1.rows.Max<IReadOnlyList<string>>((IReadOnlyList<string> r) => r.Count);
      }
      else
      {
        num = 1;
      }
      int num2 = Math.Max(Math.Max(count, num), 1);
      CS$<>8__locals1.rigid = Enumerable.Range(0, num2).Select<int, bool>(delegate(int column)
      {
        if (column <= 0)
        {
          return false;
        }
        List<string> list5 = CS$<>8__locals1.rows.Select<IReadOnlyList<string>, string>(delegate(IReadOnlyList<string> r)
        {
          if (column >= r.Count)
          {
            return "";
          }
          return r[column];
        }).ToList<string>();
        if (list5.Count > 0)
        {
          IEnumerable<string> enumerable7 = list5;
          Func<string, bool> func10;
          if ((func10 = ClickyInkTableLayout.<>O.<0>__IsShortValue) == null)
          {
            func10 = (ClickyInkTableLayout.<>O.<0>__IsShortValue = new Func<string, bool>(ClickyInkTableLayout.IsShortValue));
          }
          return enumerable7.All<string>(func10);
        }
        return false;
      }).ToList<bool>();
      ClickyInkTableLayout clickyInkTableLayout = null;
      double[] array = new double[] { 1.0, 0.94, 0.88, 0.82, 0.76, 0.7 };
      int k = 0;
      while (k < array.Length)
      {
        ClickyInkTableLayout.<>c__DisplayClass21_2 CS$<>8__locals2 = new ClickyInkTableLayout.<>c__DisplayClass21_2();
        CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
        CS$<>8__locals2.scale = array[k];
        ClickyInkTableLayout.<>c__DisplayClass21_3 CS$<>8__locals3 = new ClickyInkTableLayout.<>c__DisplayClass21_3();
        CS$<>8__locals3.CS$<>8__locals2 = CS$<>8__locals2;
        List<double> list = new List<double>();
        CS$<>8__locals3.minimum = new List<double>();
        int num7;
        int column;
        for (column = 0; column < num2; column = num7 + 1)
        {
          ClickyInkTableFont font = ((column == 0) ? ClickyInkTableFont.Name : ClickyInkTableFont.Cell);
          List<string> list2 = CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.rows.Select<IReadOnlyList<string>, string>(delegate(IReadOnlyList<string> r)
          {
            if (column >= r.Count)
            {
              return "";
            }
            return r[column];
          }).ToList<string>();
          string text = ((column < columns.Count) ? columns[column].ToUpperInvariant() : "");
          double num3 = ((list2.Count == 0) ? 0.0 : list2.Max<string>((string c) => CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<Measured>g__Measure|2(c, font, CS$<>8__locals3.CS$<>8__locals2.scale, 0.0)));
          string[] array2 = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
          double num4;
          if (array2.Length != 0)
          {
            IEnumerable<string> enumerable = array2;
            Func<string, double> func;
            if ((func = CS$<>8__locals3.CS$<>8__locals2.<>9__12) == null)
            {
              func = (CS$<>8__locals3.CS$<>8__locals2.<>9__12 = (string w) => CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<Measured>g__Measure|2(w, ClickyInkTableFont.Header, CS$<>8__locals3.CS$<>8__locals2.scale, 0.4));
            }
            num4 = enumerable.Max<string>(func);
          }
          else
          {
            num4 = 0.0;
          }
          double num5 = num4;
          List<string> list3 = list2.SelectMany<string, string>((string c) => c.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToList<string>();
          double num6 = ((list3.Count == 0) ? 0.0 : list3.Max<string>((string w) => CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<Measured>g__Measure|2(w, font, CS$<>8__locals3.CS$<>8__locals2.scale, 0.0)));
          if (CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.rigid[column])
          {
            list.Add(Math.Max(num3, num5) + 10.0);
            CS$<>8__locals3.minimum.Add(list[column]);
          }
          else
          {
            list.Add(Math.Max(Math.Max(Math.Min(num3, 200.0 * CS$<>8__locals3.CS$<>8__locals2.scale), num5), 40.0) + 10.0);
            CS$<>8__locals3.minimum.Add(Math.Max(num6, num5) + 10.0);
          }
          num7 = column;
        }
        ClickyInkTableLayout.<>c__DisplayClass21_3 CS$<>8__locals6 = CS$<>8__locals3;
        IEnumerable<double> enumerable2 = list;
        Func<double, int, bool> func2;
        if ((func2 = CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>9__4) == null)
        {
          func2 = (CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>9__4 = (double _, int i) => CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.rigid[i]);
        }
        CS$<>8__locals6.rigidTotal = enumerable2.Where<double>(func2).Sum();
        IEnumerable<double> minimum = CS$<>8__locals3.minimum;
        Func<double, int, bool> func3;
        if ((func3 = CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>9__5) == null)
        {
          func3 = (CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>9__5 = (double _, int i) => !CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.rigid[i]);
        }
        double num8 = minimum.Where<double>(func3).Sum();
        if (CS$<>8__locals3.rigidTotal + num8 > CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.width)
        {
          double total = CS$<>8__locals3.minimum.Sum();
          clickyInkTableLayout = new ClickyInkTableLayout(CS$<>8__locals3.minimum.Select<double, double>((double m) => m / Math.Max(total, 1.0) * CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.width).ToList<double>(), CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.rigid, CS$<>8__locals3.CS$<>8__locals2.scale);
          k++;
        }
        else
        {
          CS$<>8__locals3.available = CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.width - CS$<>8__locals3.rigidTotal;
          ClickyInkTableLayout.<>c__DisplayClass21_3 CS$<>8__locals8 = CS$<>8__locals3;
          IEnumerable<double> enumerable3 = list;
          Func<double, int, bool> func4;
          if ((func4 = CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>9__6) == null)
          {
            func4 = (CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>9__6 = (double _, int i) => !CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.rigid[i]);
          }
          CS$<>8__locals8.flexibleNatural = enumerable3.Where<double>(func4).Sum();
          if (CS$<>8__locals3.flexibleNatural <= 0.0)
          {
            return new ClickyInkTableLayout(list.Select<double, double>((double n) => n / CS$<>8__locals3.rigidTotal * CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.width).ToList<double>(), CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.rigid, CS$<>8__locals3.CS$<>8__locals2.scale);
          }
          if (CS$<>8__locals3.rigidTotal + CS$<>8__locals3.flexibleNatural <= CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.width)
          {
            return new ClickyInkTableLayout(list.Select<double, double>((double n) => n / (CS$<>8__locals3.rigidTotal + CS$<>8__locals3.flexibleNatural) * CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.width).ToList<double>(), CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.rigid, CS$<>8__locals3.CS$<>8__locals2.scale);
          }
          CS$<>8__locals3.widths = list.Select<double, double>(delegate(double columnWidth, int column)
          {
            if (!CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.rigid[column])
            {
              return columnWidth / CS$<>8__locals3.flexibleNatural * CS$<>8__locals3.available;
            }
            return columnWidth;
          }).ToList<double>();
          for (int j = 0; j < 4; j++)
          {
            ClickyInkTableLayout.<>c__DisplayClass21_7 CS$<>8__locals9 = new ClickyInkTableLayout.<>c__DisplayClass21_7();
            CS$<>8__locals9.CS$<>8__locals6 = CS$<>8__locals3;
            ClickyInkTableLayout.<>c__DisplayClass21_7 CS$<>8__locals10 = CS$<>8__locals9;
            IEnumerable<int> enumerable4 = Enumerable.Range(0, CS$<>8__locals9.CS$<>8__locals6.widths.Count);
            Func<int, bool> func5;
            if ((func5 = CS$<>8__locals9.CS$<>8__locals6.<>9__16) == null)
            {
              func5 = (CS$<>8__locals9.CS$<>8__locals6.<>9__16 = (int i) => !CS$<>8__locals9.CS$<>8__locals6.CS$<>8__locals2.CS$<>8__locals1.rigid[i] && CS$<>8__locals9.CS$<>8__locals6.widths[i] < CS$<>8__locals9.CS$<>8__locals6.minimum[i] - 0.5);
            }
            CS$<>8__locals10.shortColumns = enumerable4.Where<int>(func5).ToList<int>();
            if (CS$<>8__locals9.shortColumns.Count == 0)
            {
              break;
            }
            IEnumerable<int> shortColumns = CS$<>8__locals9.shortColumns;
            Func<int, double> func6;
            if ((func6 = CS$<>8__locals9.CS$<>8__locals6.<>9__17) == null)
            {
              func6 = (CS$<>8__locals9.CS$<>8__locals6.<>9__17 = (int i) => CS$<>8__locals9.CS$<>8__locals6.minimum[i] - CS$<>8__locals9.CS$<>8__locals6.widths[i]);
            }
            double num9 = shortColumns.Sum<int>(func6);
            foreach (int num10 in CS$<>8__locals9.shortColumns)
            {
              CS$<>8__locals9.CS$<>8__locals6.widths[num10] = CS$<>8__locals9.CS$<>8__locals6.minimum[num10];
            }
            List<int> list4 = (from i in Enumerable.Range(0, CS$<>8__locals9.CS$<>8__locals6.widths.Count)
              where !CS$<>8__locals9.CS$<>8__locals6.CS$<>8__locals2.CS$<>8__locals1.rigid[i] && !CS$<>8__locals9.shortColumns.Contains(i)
              select i).ToList<int>();
            IEnumerable<int> enumerable5 = list4;
            Func<int, double> func7;
            if ((func7 = CS$<>8__locals9.CS$<>8__locals6.<>9__19) == null)
            {
              func7 = (CS$<>8__locals9.CS$<>8__locals6.<>9__19 = (int i) => CS$<>8__locals9.CS$<>8__locals6.widths[i] - CS$<>8__locals9.CS$<>8__locals6.minimum[i]);
            }
            double num11 = enumerable5.Sum<int>(func7);
            if (num11 <= 0.0)
            {
              break;
            }
            IEnumerable<int> enumerable6 = list4;
            Func<int, int> func8 = (int i) => i;
            Func<int, double> func9;
            if ((func9 = CS$<>8__locals9.CS$<>8__locals6.<>9__21) == null)
            {
              func9 = (CS$<>8__locals9.CS$<>8__locals6.<>9__21 = (int i) => CS$<>8__locals9.CS$<>8__locals6.widths[i]);
            }
            Dictionary<int, double> dictionary = enumerable6.ToDictionary<int, int, double>(func8, func9);
            foreach (int num12 in list4)
            {
              List<double> widths = CS$<>8__locals9.CS$<>8__locals6.widths;
              num7 = num12;
              widths[num7] -= num9 * (dictionary[num12] - CS$<>8__locals9.CS$<>8__locals6.minimum[num12]) / num11;
            }
          }
          return new ClickyInkTableLayout(CS$<>8__locals3.widths, CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.rigid, CS$<>8__locals3.CS$<>8__locals2.scale);
        }
      }
      return clickyInkTableLayout ?? new ClickyInkTableLayout(Enumerable.Repeat<double>(CS$<>8__locals1.width / (double)num2, num2).ToList<double>(), CS$<>8__locals1.rigid, 0.7);
    }

    // Token: 0x0600167E RID: 5758 RVA: 0x003BEFFC File Offset: 0x003BEFFC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkTableLayout");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600167F RID: 5759 RVA: 0x003BF048 File Offset: 0x003BF048
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Widths = ");
      builder.Append(this.Widths);
      builder.Append(", Rigid = ");
      builder.Append(this.Rigid);
      builder.Append(", FontScale = ");
      builder.Append(this.FontScale.ToString());
      return true;
    }

    // Token: 0x06001680 RID: 5760 RVA: 0x003BF0B4 File Offset: 0x003BF0B4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkTableLayout left, ClickyInkTableLayout right)
    {
      return !(left == right);
    }

    // Token: 0x06001681 RID: 5761 RVA: 0x003BF0C0 File Offset: 0x003BF0C0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkTableLayout left, ClickyInkTableLayout right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001682 RID: 5762 RVA: 0x003BF0D4 File Offset: 0x003BF0D4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkTableLayout);
    }

    // Token: 0x06001684 RID: 5764 RVA: 0x003BF0EC File Offset: 0x003BF0EC
    [CompilerGenerated]
    private ClickyInkTableLayout(ClickyInkTableLayout original)
    {
      this.Widths = original.<Widths>k__BackingField;
      this.Rigid = original.<Rigid>k__BackingField;
      this.FontScale = original.<FontScale>k__BackingField;
    }

    // Token: 0x06001685 RID: 5765 RVA: 0x003BF118 File Offset: 0x003BF118
    [CompilerGenerated]
    public void Deconstruct(out IReadOnlyList<double> Widths, out IReadOnlyList<bool> Rigid, out double FontScale)
    {
      Widths = this.Widths;
      Rigid = this.Rigid;
      FontScale = this.FontScale;
    }

    // Token: 0x040009DA RID: 2522
    private static readonly Dictionary<string, ClickyInkTableLayout> Made = new Dictionary<string, ClickyInkTableLayout>();

    // Token: 0x02000539 RID: 1337
    // (Invoke) Token: 0x06003376 RID: 13174
    [NullableContext(0)]
    public delegate double Measurer(string text, ClickyInkTableFont font, double scale);

    // Token: 0x0200053A RID: 1338
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001FF1 RID: 8177
      [Nullable(0)]
      public static Func<string, bool> <0>__IsShortValue;
    }
  }
}
