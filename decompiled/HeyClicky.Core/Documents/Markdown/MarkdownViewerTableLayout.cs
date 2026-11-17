using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Documents.Markdown
{
  // Token: 0x020001BD RID: 445
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MarkdownViewerTableLayout : IEquatable<MarkdownViewerTableLayout>
  {
    // Token: 0x060019C9 RID: 6601 RVA: 0x003D43E8 File Offset: 0x003D43E8
    public MarkdownViewerTableLayout(IReadOnlyList<double> ColumnWidths, bool ScrollsHorizontally)
    {
      this.ColumnWidths = ColumnWidths;
      this.ScrollsHorizontally = ScrollsHorizontally;
      base..ctor();
    }

    // Token: 0x170005B4 RID: 1460
    // (get) Token: 0x060019CA RID: 6602 RVA: 0x003D4400 File Offset: 0x003D4400
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(MarkdownViewerTableLayout);
      }
    }

    // Token: 0x170005B5 RID: 1461
    // (get) Token: 0x060019CB RID: 6603 RVA: 0x003D440C File Offset: 0x003D440C
    // (set) Token: 0x060019CC RID: 6604 RVA: 0x003D4414 File Offset: 0x003D4414
    public IReadOnlyList<double> ColumnWidths { get; set; }

    // Token: 0x170005B6 RID: 1462
    // (get) Token: 0x060019CD RID: 6605 RVA: 0x003D4420 File Offset: 0x003D4420
    // (set) Token: 0x060019CE RID: 6606 RVA: 0x003D4428 File Offset: 0x003D4428
    public bool ScrollsHorizontally { get; set; }

    // Token: 0x060019CF RID: 6607 RVA: 0x003D4434 File Offset: 0x003D4434
    public static MarkdownViewerTableLayout Compute(IReadOnlyList<string> headers, IReadOnlyList<IReadOnlyList<string>> rows, double availableWidth, Func<string, double> measure)
    {
      string[] array = new string[5];
      array[0] = availableWidth.ToString("R", CultureInfo.InvariantCulture);
      array[1] = "\u001f";
      array[2] = string.Join("\u001e", headers);
      array[3] = "\u001d";
      array[4] = string.Join("\u001d", rows.Select<IReadOnlyList<string>, string>((IReadOnlyList<string> r) => string.Join("\u001e", r)));
      string text = string.Concat(array);
      object obj = MarkdownViewerTableLayout.MemoGate;
      lock (obj)
      {
        MarkdownViewerTableLayout markdownViewerTableLayout;
        if (MarkdownViewerTableLayout.Memo.TryGetValue(text, out markdownViewerTableLayout))
        {
          return markdownViewerTableLayout;
        }
      }
      MarkdownViewerTableLayout markdownViewerTableLayout2 = MarkdownViewerTableLayout.ComputeUncached(headers, rows, availableWidth, measure);
      obj = MarkdownViewerTableLayout.MemoGate;
      lock (obj)
      {
        if (MarkdownViewerTableLayout.Memo.Count >= 200)
        {
          MarkdownViewerTableLayout.Memo.Clear();
        }
        MarkdownViewerTableLayout.Memo[text] = markdownViewerTableLayout2;
      }
      return markdownViewerTableLayout2;
    }

    // Token: 0x060019D0 RID: 6608 RVA: 0x003D4550 File Offset: 0x003D4550
    public static string MeasuredText(string markdownCellText)
    {
      return markdownCellText.Replace("**", "").Replace("`", "").Replace("_", "");
    }

    // Token: 0x060019D1 RID: 6609 RVA: 0x003D4580 File Offset: 0x003D4580
    private static MarkdownViewerTableLayout ComputeUncached(IReadOnlyList<string> headers, IReadOnlyList<IReadOnlyList<string>> rows, double availableWidth, Func<string, double> measure)
    {
      MarkdownViewerTableLayout.<>c__DisplayClass19_0 CS$<>8__locals1 = new MarkdownViewerTableLayout.<>c__DisplayClass19_0();
      CS$<>8__locals1.measure = measure;
      int count = headers.Count;
      if (count == 0)
      {
        return new MarkdownViewerTableLayout(Array.Empty<double>(), false);
      }
      CS$<>8__locals1.naturalWidths = new double[count];
      for (int i = 0; i < count; i++)
      {
        double num = CS$<>8__locals1.<ComputeUncached>g__Measured|0(headers[i]);
        foreach (IReadOnlyList<string> readOnlyList in rows)
        {
          if (i < readOnlyList.Count)
          {
            num = Math.Max(num, CS$<>8__locals1.<ComputeUncached>g__Measured|0(readOnlyList[i]));
          }
        }
        double num2 = num + 24.0;
        CS$<>8__locals1.naturalWidths[i] = Math.Min(360.0, Math.Max(120.0, num2));
      }
      double num3 = CS$<>8__locals1.naturalWidths.Sum();
      if (availableWidth <= 0.0 || num3 <= 0.0)
      {
        return new MarkdownViewerTableLayout(CS$<>8__locals1.naturalWidths, true);
      }
      CS$<>8__locals1.scale = availableWidth / num3;
      if (CS$<>8__locals1.scale >= 1.0)
      {
        return new MarkdownViewerTableLayout(CS$<>8__locals1.naturalWidths.Select<double, double>((double w) => w * CS$<>8__locals1.scale).ToArray<double>(), false);
      }
      if (120.0 * (double)count <= availableWidth + 0.5)
      {
        double low = 120.0;
        double num4 = CS$<>8__locals1.naturalWidths.Max();
        for (int j = 0; j < 24; j++)
        {
          double num5 = (low + num4) / 2.0;
          if (CS$<>8__locals1.<ComputeUncached>g__Total|2(num5) <= availableWidth)
          {
            low = num5;
          }
          else
          {
            num4 = num5;
          }
        }
        double[] widths = CS$<>8__locals1.naturalWidths.Select<double, double>((double w) => Math.Min(w, Math.Max(low, 120.0))).ToArray<double>();
        List<int> list = (from index in Enumerable.Range(0, count)
          where CS$<>8__locals1.naturalWidths[index] > widths[index]
          select index).ToList<int>();
        double num6 = availableWidth - widths.Sum();
        if (num6 > 0.0 && list.Count > 0)
        {
          foreach (int num7 in list)
          {
            widths[num7] += num6 / (double)list.Count;
          }
        }
        return new MarkdownViewerTableLayout(widths, false);
      }
      return new MarkdownViewerTableLayout(CS$<>8__locals1.naturalWidths, true);
    }

    // Token: 0x060019D2 RID: 6610 RVA: 0x003D4850 File Offset: 0x003D4850
    public static bool IsBestRow(IReadOnlyList<string> cells)
    {
      return cells.Count > 0 && cells[0].TrimStart(new char[] { ' ', '\t' }).StartsWith('✓');
    }

    // Token: 0x060019D3 RID: 6611 RVA: 0x003D4884 File Offset: 0x003D4884
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("MarkdownViewerTableLayout");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060019D4 RID: 6612 RVA: 0x003D48D0 File Offset: 0x003D48D0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ColumnWidths = ");
      builder.Append(this.ColumnWidths);
      builder.Append(", ScrollsHorizontally = ");
      builder.Append(this.ScrollsHorizontally.ToString());
      return true;
    }

    // Token: 0x060019D5 RID: 6613 RVA: 0x003D4924 File Offset: 0x003D4924
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(MarkdownViewerTableLayout left, MarkdownViewerTableLayout right)
    {
      return !(left == right);
    }

    // Token: 0x060019D6 RID: 6614 RVA: 0x003D4930 File Offset: 0x003D4930
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(MarkdownViewerTableLayout left, MarkdownViewerTableLayout right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060019D7 RID: 6615 RVA: 0x003D4944 File Offset: 0x003D4944
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<double>>.Default.GetHashCode(this.<ColumnWidths>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ScrollsHorizontally>k__BackingField);
    }

    // Token: 0x060019D8 RID: 6616 RVA: 0x003D4984 File Offset: 0x003D4984
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as MarkdownViewerTableLayout);
    }

    // Token: 0x060019D9 RID: 6617 RVA: 0x003D4994 File Offset: 0x003D4994
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(MarkdownViewerTableLayout other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<double>>.Default.Equals(this.<ColumnWidths>k__BackingField, other.<ColumnWidths>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<ScrollsHorizontally>k__BackingField, other.<ScrollsHorizontally>k__BackingField));
    }

    // Token: 0x060019DB RID: 6619 RVA: 0x003D49F8 File Offset: 0x003D49F8
    [CompilerGenerated]
    private MarkdownViewerTableLayout(MarkdownViewerTableLayout original)
    {
      this.ColumnWidths = original.<ColumnWidths>k__BackingField;
      this.ScrollsHorizontally = original.<ScrollsHorizontally>k__BackingField;
    }

    // Token: 0x060019DC RID: 6620 RVA: 0x003D4A18 File Offset: 0x003D4A18
    [CompilerGenerated]
    public void Deconstruct(out IReadOnlyList<double> ColumnWidths, out bool ScrollsHorizontally)
    {
      ColumnWidths = this.ColumnWidths;
      ScrollsHorizontally = this.ScrollsHorizontally;
    }

    // Token: 0x04000AFB RID: 2811
    public const double CellHorizontalPadding = 12.0;

    // Token: 0x04000AFC RID: 2812
    public const double CellVerticalPadding = 8.0;

    // Token: 0x04000AFD RID: 2813
    public const double MinimumColumnWidth = 120.0;

    // Token: 0x04000AFE RID: 2814
    public const double MaximumNaturalColumnWidth = 360.0;

    // Token: 0x04000AFF RID: 2815
    private static readonly Dictionary<string, MarkdownViewerTableLayout> Memo = new Dictionary<string, MarkdownViewerTableLayout>();

    // Token: 0x04000B00 RID: 2816
    private static readonly object MemoGate = new object();
  }
}
