using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions.Generated;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000176 RID: 374
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ClickyInkDayLabel : IEquatable<ClickyInkDayLabel>
  {
    // Token: 0x1700052A RID: 1322
    // (get) Token: 0x06001692 RID: 5778 RVA: 0x003BF794 File Offset: 0x003BF794
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkDayLabel);
      }
    }

    // Token: 0x1700052B RID: 1323
    // (get) Token: 0x06001693 RID: 5779 RVA: 0x003BF7A0 File Offset: 0x003BF7A0
    // (set) Token: 0x06001694 RID: 5780 RVA: 0x003BF7A8 File Offset: 0x003BF7A8
    public string Weekday { get; private set; }

    // Token: 0x1700052C RID: 1324
    // (get) Token: 0x06001695 RID: 5781 RVA: 0x003BF7B4 File Offset: 0x003BF7B4
    // (set) Token: 0x06001696 RID: 5782 RVA: 0x003BF7BC File Offset: 0x003BF7BC
    public string Month { get; private set; }

    // Token: 0x1700052D RID: 1325
    // (get) Token: 0x06001697 RID: 5783 RVA: 0x003BF7C8 File Offset: 0x003BF7C8
    // (set) Token: 0x06001698 RID: 5784 RVA: 0x003BF7D0 File Offset: 0x003BF7D0
    public string Day { get; private set; }

    // Token: 0x1700052E RID: 1326
    // (get) Token: 0x06001699 RID: 5785 RVA: 0x003BF7DC File Offset: 0x003BF7DC
    // (set) Token: 0x0600169A RID: 5786 RVA: 0x003BF7E4 File Offset: 0x003BF7E4
    public string Caption { get; private set; }

    // Token: 0x0600169B RID: 5787 RVA: 0x003BF7F0 File Offset: 0x003BF7F0
    [NullableContext(1)]
    [GeneratedRegex("\\d\\s*-\\s*\\d")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex DigitRange()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__DigitRange_44.Instance;
    }

    // Token: 0x0600169C RID: 5788 RVA: 0x003BF7F8 File Offset: 0x003BF7F8
    [NullableContext(1)]
    private static bool AllLetters(string s)
    {
      IEnumerable<string> enumerable = s.Characters();
      Func<string, bool> func;
      if ((func = ClickyInkDayLabel.<>O.<0>__IsLetter) == null)
      {
        func = (ClickyInkDayLabel.<>O.<0>__IsLetter = new Func<string, bool>(InkText.IsLetter));
      }
      return enumerable.All<string>(func);
    }

    // Token: 0x0600169D RID: 5789 RVA: 0x003BF820 File Offset: 0x003BF820
    [NullableContext(1)]
    private static bool AllNumbers(string s)
    {
      IEnumerable<string> enumerable = s.Characters();
      Func<string, bool> func;
      if ((func = ClickyInkDayLabel.<>O.<1>__IsNumber) == null)
      {
        func = (ClickyInkDayLabel.<>O.<1>__IsNumber = new Func<string, bool>(InkText.IsNumber));
      }
      return enumerable.All<string>(func);
    }

    // Token: 0x0600169E RID: 5790 RVA: 0x003BF848 File Offset: 0x003BF848
    [NullableContext(1)]
    public ClickyInkDayLabel(string label)
    {
      List<string> list = (from p in label.Split("·", StringSplitOptions.None)
        select p.TrimWs() into p
        where p.Length > 0
        select p).ToList<string>();
      List<string> list2 = list.TakeWhile<string>(new Func<string, bool>(ClickyInkDayLabel.<.ctor>g__IsDatePiece|24_2)).ToList<string>();
      string text = ((list2.Count == 0) ? (list.FirstOrDefault<string>() ?? "") : string.Join(" ", list2));
      List<string> list3 = list.Skip<string>(Math.Max(1, list2.Count)).ToList<string>();
      this.Caption = ((list3.Count == 0) ? null : string.Join(" · ", list3));
      if ((from w in text.ToLowerInvariant().SplitCharacters((string c) => !InkText.IsLetter(c))
        where ClickyInkDayLabel.MonthNames.Contains(w.PrefixCharacters(3)) && w.CharacterCount() >= 3
        select w).ToList<string>().Count > 1 || text.Contains('–') || ClickyInkDayLabel.DigitRange().IsMatch(text))
      {
        return;
      }
      foreach (string text2 in text.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries))
      {
        string text3 = text2.ToLowerInvariant();
        IEnumerable<string> enumerable = text2.Characters();
        Func<string, bool> func;
        if ((func = ClickyInkDayLabel.<>O.<1>__IsNumber) == null)
        {
          func = (ClickyInkDayLabel.<>O.<1>__IsNumber = new Func<string, bool>(InkText.IsNumber));
        }
        string text4 = string.Concat(enumerable.Where<string>(func));
        if (this.Weekday == null && ClickyInkDayLabel.WeekdayNames.Contains(text3.PrefixCharacters(3)) && text3.CharacterCount() >= 3 && ClickyInkDayLabel.AllLetters(text3))
        {
          this.Weekday = text2.PrefixCharacters(3).ToUpperInvariant();
        }
        else if (this.Month == null && ClickyInkDayLabel.MonthNames.Contains(text3.PrefixCharacters(3)) && text3.CharacterCount() >= 3 && ClickyInkDayLabel.AllLetters(text3))
        {
          this.Month = text2.PrefixCharacters(3).ToUpperInvariant();
        }
        else if (text3 == "day")
        {
          this.Weekday = "DAY";
        }
        else if (this.Day == null)
        {
          long? num = InkText.ParseInt(text4);
          if (num != null)
          {
            long valueOrDefault = num.GetValueOrDefault();
            if ((text4.CharacterCount() == text2.CharacterCount() || text2.HasSuffix("th") || text2.HasSuffix("st") || text2.HasSuffix("nd") || text2.HasSuffix("rd")) && valueOrDefault >= 1L && valueOrDefault <= 31L)
            {
              this.Day = valueOrDefault.ToString(CultureInfo.InvariantCulture);
            }
          }
        }
      }
    }

    // Token: 0x0600169F RID: 5791 RVA: 0x003BFB48 File Offset: 0x003BFB48
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkDayLabel");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060016A0 RID: 5792 RVA: 0x003BFB94 File Offset: 0x003BFB94
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Weekday = ");
      builder.Append(this.Weekday);
      builder.Append(", Month = ");
      builder.Append(this.Month);
      builder.Append(", Day = ");
      builder.Append(this.Day);
      builder.Append(", Caption = ");
      builder.Append(this.Caption);
      return true;
    }

    // Token: 0x060016A1 RID: 5793 RVA: 0x003BFC0C File Offset: 0x003BFC0C
    [CompilerGenerated]
    public static bool operator !=(ClickyInkDayLabel left, ClickyInkDayLabel right)
    {
      return !(left == right);
    }

    // Token: 0x060016A2 RID: 5794 RVA: 0x003BFC18 File Offset: 0x003BFC18
    [CompilerGenerated]
    public static bool operator ==(ClickyInkDayLabel left, ClickyInkDayLabel right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060016A3 RID: 5795 RVA: 0x003BFC2C File Offset: 0x003BFC2C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Weekday>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Month>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Day>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Caption>k__BackingField);
    }

    // Token: 0x060016A4 RID: 5796 RVA: 0x003BFCA8 File Offset: 0x003BFCA8
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkDayLabel);
    }

    // Token: 0x060016A5 RID: 5797 RVA: 0x003BFCB8 File Offset: 0x003BFCB8
    [CompilerGenerated]
    public bool Equals(ClickyInkDayLabel other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Weekday>k__BackingField, other.<Weekday>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Month>k__BackingField, other.<Month>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Day>k__BackingField, other.<Day>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Caption>k__BackingField, other.<Caption>k__BackingField));
    }

    // Token: 0x060016A7 RID: 5799 RVA: 0x003BFD4C File Offset: 0x003BFD4C
    [CompilerGenerated]
    private ClickyInkDayLabel([Nullable(1)] ClickyInkDayLabel original)
    {
      this.Weekday = original.<Weekday>k__BackingField;
      this.Month = original.<Month>k__BackingField;
      this.Day = original.<Day>k__BackingField;
      this.Caption = original.<Caption>k__BackingField;
    }

    // Token: 0x060016A8 RID: 5800 RVA: 0x003BFD84 File Offset: 0x003BFD84
    // Note: this type is marked as 'beforefieldinit'.
    static ClickyInkDayLabel()
    {
      string[] weekdayNames = ClickyInkDayLabel.WeekdayNames;
      string[] monthNames = ClickyInkDayLabel.MonthNames;
      int num = 0;
      string[] array = new string[1 + (weekdayNames.Length + monthNames.Length)];
      ReadOnlySpan<string> readOnlySpan = new ReadOnlySpan<string>(weekdayNames);
      readOnlySpan.CopyTo(new Span<string>(array).Slice(num, readOnlySpan.Length));
      num += readOnlySpan.Length;
      ReadOnlySpan<string> readOnlySpan2 = new ReadOnlySpan<string>(monthNames);
      readOnlySpan2.CopyTo(new Span<string>(array).Slice(num, readOnlySpan2.Length));
      num += readOnlySpan2.Length;
      array[num] = "day";
      ClickyInkDayLabel.DateWords = array;
    }

    // Token: 0x060016A9 RID: 5801 RVA: 0x003BFED0 File Offset: 0x003BFED0
    [NullableContext(1)]
    [CompilerGenerated]
    internal static bool <.ctor>g__IsDatePiece|24_2(string piece)
    {
      List<string> list = piece.ToLowerInvariant().SplitCharacters((string c) => !InkText.IsLetter(c) && !InkText.IsNumber(c));
      if (list.Any<string>((string w) => ClickyInkDayLabel.DateWords.Contains(w.PrefixCharacters(3)) && ClickyInkDayLabel.AllLetters(w)))
      {
        return true;
      }
      if (list.Count <= 2)
      {
        return list.Any<string>((string w) => ClickyInkDayLabel.AllNumbers(w) && InkText.ParseInt(w).GetValueOrDefault() <= 31L);
      }
      return false;
    }

    // Token: 0x040009DF RID: 2527
    [Nullable(1)]
    private static readonly string[] MonthNames = new string[]
    {
      "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct",
      "nov", "dec"
    };

    // Token: 0x040009E0 RID: 2528
    [Nullable(1)]
    private static readonly string[] WeekdayNames = new string[] { "mon", "tue", "wed", "thu", "fri", "sat", "sun" };

    // Token: 0x040009E1 RID: 2529
    [Nullable(1)]
    private static readonly string[] DateWords;

    // Token: 0x0200054B RID: 1355
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04002023 RID: 8227
      [Nullable(0)]
      public static Func<string, bool> <0>__IsLetter;

      // Token: 0x04002024 RID: 8228
      [Nullable(0)]
      public static Func<string, bool> <1>__IsNumber;
    }
  }
}
