using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.Agents.Jobs;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000312 RID: 786
  [NullableContext(1)]
  [Nullable(0)]
  public static class ResultCardLayouts
  {
    // Token: 0x0600222A RID: 8746 RVA: 0x0062051C File Offset: 0x0062051C
    public static IReadOnlyList<ResultCardLayouts.Arrangement> Layouts(ClickyAnswer answer)
    {
      IReadOnlyList<ClickyAnswer.Block> blocks = answer.Blocks;
      ResultCardLayouts.<>c__DisplayClass1_0 CS$<>8__locals1;
      CS$<>8__locals1.hasNote = answer.Note != null;
      CS$<>8__locals1.layouts = new List<ResultCardLayouts.Arrangement>();
      ResultCardLayouts.<Layouts>g__TryBlocks|1_0(blocks, ref CS$<>8__locals1);
      if (blocks.Count > 1)
      {
        ClickyAnswer.Block block = blocks.FirstOrDefault((ClickyAnswer.Block b) => b - ClickyAnswer.Block.Draft <= 1, blocks[0]);
        if (!blocks.Any<ClickyAnswer.Block>((ClickyAnswer.Block b) => b - ClickyAnswer.Block.Draft <= 1))
        {
          block = blocks[0];
        }
        ResultCardLayouts.<Layouts>g__TryBlocks|1_0(new <>z__ReadOnlySingleElementList<ClickyAnswer.Block>(block), ref CS$<>8__locals1);
      }
      if (blocks.Count > 0)
      {
        CS$<>8__locals1.layouts.Add(new ResultCardLayouts.Arrangement(Array.Empty<ClickyAnswer.Block>(), false, false));
      }
      return CS$<>8__locals1.layouts;
    }

    // Token: 0x0600222B RID: 8747 RVA: 0x006205F0 File Offset: 0x006205F0
    public static double? Numeric(string text)
    {
      string text2 = text.ToLowerInvariant();
      double num;
      if (!double.TryParse(new string(text2.Where<char>((char ch) => char.IsDigit(ch) || ch == '.' || ch == '-').ToArray<char>()), NumberStyles.Float, CultureInfo.InvariantCulture, out num))
      {
        return null;
      }
      if (text2.EndsWith('k'))
      {
        num *= 1000.0;
      }
      else if (text2.EndsWith('m'))
      {
        num *= 1000000.0;
      }
      return new double?(num);
    }

    // Token: 0x0600222C RID: 8748 RVA: 0x00620684 File Offset: 0x00620684
    public static double? Fraction(ClickyAnswer.Number number)
    {
      string of = number.Of;
      if (of != null)
      {
        double? num = ResultCardLayouts.Numeric(of);
        if (num != null)
        {
          double valueOrDefault = num.GetValueOrDefault();
          if (valueOrDefault > 0.0)
          {
            num = ResultCardLayouts.Numeric(number.Value);
            if (num != null)
            {
              double valueOrDefault2 = num.GetValueOrDefault();
              return new double?(Math.Min(1.0, Math.Max(0.0, valueOrDefault2 / valueOrDefault)));
            }
          }
        }
      }
      return null;
    }

    // Token: 0x0600222D RID: 8749 RVA: 0x0062070C File Offset: 0x0062070C
    public static ResultCardLayouts.MoveKind Move(ClickyAnswer.Number number)
    {
      string was = number.Was;
      if (was == null)
      {
        return ResultCardLayouts.MoveKind.None;
      }
      if (was == number.Value)
      {
        return ResultCardLayouts.MoveKind.Same;
      }
      double? num = ResultCardLayouts.Numeric(was);
      if (num != null)
      {
        double valueOrDefault = num.GetValueOrDefault();
        num = ResultCardLayouts.Numeric(number.Value);
        if (num != null)
        {
          double valueOrDefault2 = num.GetValueOrDefault();
          if (valueOrDefault == valueOrDefault2)
          {
            return ResultCardLayouts.MoveKind.Same;
          }
          string better = number.Better;
          if (better == null)
          {
            return ResultCardLayouts.MoveKind.Plain;
          }
          if (better == "higher" != valueOrDefault2 > valueOrDefault)
          {
            return ResultCardLayouts.MoveKind.Worse;
          }
          return ResultCardLayouts.MoveKind.Better;
        }
      }
      return ResultCardLayouts.MoveKind.Plain;
    }

    // Token: 0x0600222E RID: 8750 RVA: 0x00620798 File Offset: 0x00620798
    public static string Moment(DateTimeOffset date, DateTimeOffset now)
    {
      CultureInfo currentCulture = CultureInfo.CurrentCulture;
      string text = date.ToLocalTime().ToString("t", currentCulture);
      DateTime date2 = date.ToLocalTime().Date;
      DateTime date3 = now.ToLocalTime().Date;
      if (date2 == date3)
      {
        return "at " + text;
      }
      if (date2 == date3.AddDays(1.0))
      {
        return "tomorrow " + text;
      }
      if (date2 == date3.AddDays(-1.0))
      {
        return "yesterday " + text;
      }
      if (Math.Abs((date - now).TotalSeconds) < 518400.0)
      {
        return date.ToLocalTime().ToString("dddd", currentCulture) + " " + text;
      }
      return date.ToLocalTime().ToString("MMM d", currentCulture);
    }

    // Token: 0x0600222F RID: 8751 RVA: 0x0062089C File Offset: 0x0062089C
    [CompilerGenerated]
    internal static void <Layouts>g__TryBlocks|1_0(IReadOnlyList<ClickyAnswer.Block> shown, ref ResultCardLayouts.<>c__DisplayClass1_0 A_1)
    {
      bool flag = shown.Contains(ClickyAnswer.Block.Ask) || shown.Contains(ClickyAnswer.Block.Numbers);
      if (A_1.hasNote)
      {
        A_1.layouts.Add(new ResultCardLayouts.Arrangement(shown, true, false));
        if (flag)
        {
          A_1.layouts.Add(new ResultCardLayouts.Arrangement(shown, true, true));
        }
      }
      A_1.layouts.Add(new ResultCardLayouts.Arrangement(shown, false, false));
      if (flag)
      {
        A_1.layouts.Add(new ResultCardLayouts.Arrangement(shown, false, true));
      }
    }

    // Token: 0x0200095A RID: 2394
    [Nullable(0)]
    public sealed class Arrangement : IEquatable<ResultCardLayouts.Arrangement>
    {
      // Token: 0x06003D12 RID: 15634 RVA: 0x00699540 File Offset: 0x00699540
      public Arrangement(IReadOnlyList<ClickyAnswer.Block> Blocks, bool ShowsNote, bool IsCompact = false)
      {
        this.Blocks = Blocks;
        this.ShowsNote = ShowsNote;
        this.IsCompact = IsCompact;
        base..ctor();
      }

      // Token: 0x17000854 RID: 2132
      // (get) Token: 0x06003D13 RID: 15635 RVA: 0x00699560 File Offset: 0x00699560
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ResultCardLayouts.Arrangement);
        }
      }

      // Token: 0x17000855 RID: 2133
      // (get) Token: 0x06003D14 RID: 15636 RVA: 0x0069956C File Offset: 0x0069956C
      // (set) Token: 0x06003D15 RID: 15637 RVA: 0x00699574 File Offset: 0x00699574
      public IReadOnlyList<ClickyAnswer.Block> Blocks { get; set; }

      // Token: 0x17000856 RID: 2134
      // (get) Token: 0x06003D16 RID: 15638 RVA: 0x00699580 File Offset: 0x00699580
      // (set) Token: 0x06003D17 RID: 15639 RVA: 0x00699588 File Offset: 0x00699588
      public bool ShowsNote { get; set; }

      // Token: 0x17000857 RID: 2135
      // (get) Token: 0x06003D18 RID: 15640 RVA: 0x00699594 File Offset: 0x00699594
      // (set) Token: 0x06003D19 RID: 15641 RVA: 0x0069959C File Offset: 0x0069959C
      public bool IsCompact { get; set; }

      // Token: 0x06003D1A RID: 15642 RVA: 0x006995A8 File Offset: 0x006995A8
      [NullableContext(2)]
      public bool Equals(ResultCardLayouts.Arrangement other)
      {
        return other != null && this.Blocks.SequenceEqual<ClickyAnswer.Block>(other.Blocks) && this.ShowsNote == other.ShowsNote && this.IsCompact == other.IsCompact;
      }

      // Token: 0x06003D1B RID: 15643 RVA: 0x006995E0 File Offset: 0x006995E0
      public override int GetHashCode()
      {
        return HashCode.Combine<int, bool, bool>(this.Blocks.Count, this.ShowsNote, this.IsCompact);
      }

      // Token: 0x06003D1C RID: 15644 RVA: 0x00699600 File Offset: 0x00699600
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Arrangement");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003D1D RID: 15645 RVA: 0x0069964C File Offset: 0x0069964C
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Blocks = ");
        builder.Append(this.Blocks);
        builder.Append(", ShowsNote = ");
        builder.Append(this.ShowsNote.ToString());
        builder.Append(", IsCompact = ");
        builder.Append(this.IsCompact.ToString());
        return true;
      }

      // Token: 0x06003D1E RID: 15646 RVA: 0x006996C8 File Offset: 0x006996C8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ResultCardLayouts.Arrangement left, ResultCardLayouts.Arrangement right)
      {
        return !(left == right);
      }

      // Token: 0x06003D1F RID: 15647 RVA: 0x006996D4 File Offset: 0x006996D4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ResultCardLayouts.Arrangement left, ResultCardLayouts.Arrangement right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003D20 RID: 15648 RVA: 0x006996E8 File Offset: 0x006996E8
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ResultCardLayouts.Arrangement);
      }

      // Token: 0x06003D22 RID: 15650 RVA: 0x00699700 File Offset: 0x00699700
      [CompilerGenerated]
      private Arrangement(ResultCardLayouts.Arrangement original)
      {
        this.Blocks = original.<Blocks>k__BackingField;
        this.ShowsNote = original.<ShowsNote>k__BackingField;
        this.IsCompact = original.<IsCompact>k__BackingField;
      }

      // Token: 0x06003D23 RID: 15651 RVA: 0x0069972C File Offset: 0x0069972C
      [CompilerGenerated]
      public void Deconstruct(out IReadOnlyList<ClickyAnswer.Block> Blocks, out bool ShowsNote, out bool IsCompact)
      {
        Blocks = this.Blocks;
        ShowsNote = this.ShowsNote;
        IsCompact = this.IsCompact;
      }
    }

    // Token: 0x0200095B RID: 2395
    [NullableContext(0)]
    public enum MoveKind
    {
      // Token: 0x04002754 RID: 10068
      None,
      // Token: 0x04002755 RID: 10069
      Same,
      // Token: 0x04002756 RID: 10070
      Better,
      // Token: 0x04002757 RID: 10071
      Worse,
      // Token: 0x04002758 RID: 10072
      Plain
    }
  }
}
