using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions.Generated;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000131 RID: 305
  [NullableContext(1)]
  [Nullable(0)]
  public struct ClickyInkNumber : IEquatable<ClickyInkNumber>
  {
    // Token: 0x170004A0 RID: 1184
    // (get) Token: 0x060013B5 RID: 5045 RVA: 0x003A7DE0 File Offset: 0x003A7DE0
    // (set) Token: 0x060013B6 RID: 5046 RVA: 0x003A7DE8 File Offset: 0x003A7DE8
    public double Value { readonly get; set; }

    // Token: 0x170004A1 RID: 1185
    // (get) Token: 0x060013B7 RID: 5047 RVA: 0x003A7DF4 File Offset: 0x003A7DF4
    // (set) Token: 0x060013B8 RID: 5048 RVA: 0x003A7DFC File Offset: 0x003A7DFC
    public string Prefix { readonly get; set; }

    // Token: 0x170004A2 RID: 1186
    // (get) Token: 0x060013B9 RID: 5049 RVA: 0x003A7E08 File Offset: 0x003A7E08
    // (set) Token: 0x060013BA RID: 5050 RVA: 0x003A7E10 File Offset: 0x003A7E10
    public string Suffix { readonly get; set; }

    // Token: 0x170004A3 RID: 1187
    // (get) Token: 0x060013BB RID: 5051 RVA: 0x003A7E1C File Offset: 0x003A7E1C
    // (set) Token: 0x060013BC RID: 5052 RVA: 0x003A7E24 File Offset: 0x003A7E24
    public int Decimals { readonly get; set; }

    // Token: 0x170004A4 RID: 1188
    // (get) Token: 0x060013BD RID: 5053 RVA: 0x003A7E30 File Offset: 0x003A7E30
    // (set) Token: 0x060013BE RID: 5054 RVA: 0x003A7E38 File Offset: 0x003A7E38
    public bool UsesGrouping { readonly get; set; }

    // Token: 0x170004A5 RID: 1189
    // (get) Token: 0x060013BF RID: 5055 RVA: 0x003A7E44 File Offset: 0x003A7E44
    // (set) Token: 0x060013C0 RID: 5056 RVA: 0x003A7E4C File Offset: 0x003A7E4C
    public double Scale { readonly get; set; }

    // Token: 0x170004A6 RID: 1190
    // (get) Token: 0x060013C1 RID: 5057 RVA: 0x003A7E58 File Offset: 0x003A7E58
    // (set) Token: 0x060013C2 RID: 5058 RVA: 0x003A7E60 File Offset: 0x003A7E60
    public bool ShowsPlus { readonly get; set; }

    // Token: 0x060013C3 RID: 5059 RVA: 0x003A7E6C File Offset: 0x003A7E6C
    [GeneratedRegex("^([+-]?)([^\\d+\\-.]{0,4}?)([+-]?)(\\d[\\d,]*(?:\\.\\d+)?|\\.\\d+)(.*)$")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex NumberRegex()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__NumberRegex_36.Instance;
    }

    // Token: 0x060013C4 RID: 5060 RVA: 0x003A7E74 File Offset: 0x003A7E74
    [NullableContext(2)]
    public static ClickyInkNumber? Parse(string input)
    {
      if (input == null)
      {
        return null;
      }
      string text = input.TrimWs();
      if (text.Length == 0 || text.CharacterCount() > 40)
      {
        return null;
      }
      text = text.Replace("−", "-").Replace("–", "-");
      Match match = ClickyInkNumber.NumberRegex().Match(text);
      if (!match.Success)
      {
        return null;
      }
      string text2 = match.Groups[1].Value + match.Groups[3].Value;
      string value = match.Groups[2].Value;
      IEnumerable<string> enumerable = value.Characters();
      Func<string, bool> func;
      if ((func = ClickyInkNumber.<>O.<0>__IsLetter) == null)
      {
        func = (ClickyInkNumber.<>O.<0>__IsLetter = new Func<string, bool>(InkText.IsLetter));
      }
      if (enumerable.Any<string>(func))
      {
        return null;
      }
      string value2 = match.Groups[4].Value;
      string value3 = match.Groups[5].Value;
      double? num = InkText.ParseDouble(value2.Replace(",", ""));
      if (num != null)
      {
        double valueOrDefault = num.GetValueOrDefault();
        double num2 = 1.0;
        string text3 = value3.Lowered();
        ClickyInkNumber.<>c__DisplayClass29_0 CS$<>8__locals1;
        CS$<>8__locals1.lowerCharacters = text3.Characters();
        if (text3.HasPrefix("bn") && ClickyInkNumber.<Parse>g__AfterLetter|29_0(2, ref CS$<>8__locals1))
        {
          num2 = 1000000000.0;
        }
        else if (text3.HasPrefix("k") && ClickyInkNumber.<Parse>g__AfterLetter|29_0(1, ref CS$<>8__locals1))
        {
          num2 = 1000.0;
        }
        else if (text3.HasPrefix("m") && ClickyInkNumber.<Parse>g__AfterLetter|29_0(1, ref CS$<>8__locals1) && !text3.HasPrefix("min") && !text3.HasPrefix("mo"))
        {
          num2 = 1000000.0;
        }
        else if (text3.HasPrefix("b") && ClickyInkNumber.<Parse>g__AfterLetter|29_0(1, ref CS$<>8__locals1))
        {
          num2 = 1000000000.0;
        }
        bool flag = text2.Contains('-');
        string[] array = value2.Split('.', StringSplitOptions.None);
        return new ClickyInkNumber?(new ClickyInkNumber
        {
          Value = (flag ? (-valueOrDefault) : valueOrDefault) * num2,
          Prefix = value,
          Suffix = value3,
          Decimals = ((array.Length > 1) ? array[1].CharacterCount() : 0),
          UsesGrouping = value2.Contains(','),
          Scale = num2,
          ShowsPlus = text2.Contains('+')
        });
      }
      return null;
    }

    // Token: 0x060013C5 RID: 5061 RVA: 0x003A8120 File Offset: 0x003A8120
    public readonly string Formatted(double newValue)
    {
      double num = newValue / this.Scale;
      string text = ClickyInkNumberFormatting.Decimal(Math.Abs(num), this.Decimals, this.UsesGrouping);
      bool flag = InkText.ParseDouble(text.Replace(",", "")).GetValueOrDefault() == 0.0;
      return ((num < 0.0 && !flag) ? "-" : ((this.ShowsPlus && !flag) ? "+" : "")) + this.Prefix + text + this.Suffix;
    }

    // Token: 0x060013C6 RID: 5062 RVA: 0x003A81B4 File Offset: 0x003A81B4
    public readonly bool IsSameKind(ClickyInkNumber other)
    {
      return this.Prefix == other.Prefix && this.Suffix.TrimWs().Lowered() == other.Suffix.TrimWs().Lowered();
    }

    // Token: 0x060013C7 RID: 5063 RVA: 0x003A81F4 File Offset: 0x003A81F4
    [NullableContext(2)]
    public static string Interpolated(string from, string to, double progress)
    {
      if (to != null)
      {
        ClickyInkNumber? clickyInkNumber = ClickyInkNumber.Parse(to);
        if (clickyInkNumber != null)
        {
          ClickyInkNumber valueOrDefault = clickyInkNumber.GetValueOrDefault();
          if (progress >= 1.0)
          {
            return to;
          }
          if (from != null)
          {
            clickyInkNumber = ClickyInkNumber.Parse(from);
            if (clickyInkNumber != null)
            {
              ClickyInkNumber valueOrDefault2 = clickyInkNumber.GetValueOrDefault();
              if (valueOrDefault2.IsSameKind(valueOrDefault))
              {
                ClickyInkNumber clickyInkNumber2 = valueOrDefault;
                clickyInkNumber2.Decimals = Math.Max(valueOrDefault.Decimals, valueOrDefault2.Decimals);
                clickyInkNumber2.UsesGrouping = valueOrDefault.UsesGrouping || valueOrDefault2.UsesGrouping;
                if (valueOrDefault2.Scale != valueOrDefault.Scale)
                {
                  clickyInkNumber2.Scale = valueOrDefault.Scale;
                }
                return clickyInkNumber2.Formatted(valueOrDefault2.Value + (valueOrDefault.Value - valueOrDefault2.Value) * Math.Min(1.0, Math.Max(0.0, progress)));
              }
            }
          }
          return null;
        }
      }
      return null;
    }

    // Token: 0x060013C8 RID: 5064 RVA: 0x003A82E8 File Offset: 0x003A82E8
    [NullableContext(2)]
    [return: TupleElementNames(new string[] { "Text", "Value" })]
    [return: Nullable(new byte[] { 0, 1 })]
    public static ValueTuple<string, double>? Difference(string from, string to)
    {
      if (from != null && to != null)
      {
        ClickyInkNumber? clickyInkNumber = ClickyInkNumber.Parse(from);
        if (clickyInkNumber != null)
        {
          ClickyInkNumber valueOrDefault = clickyInkNumber.GetValueOrDefault();
          clickyInkNumber = ClickyInkNumber.Parse(to);
          if (clickyInkNumber != null)
          {
            ClickyInkNumber valueOrDefault2 = clickyInkNumber.GetValueOrDefault();
            if (valueOrDefault.IsSameKind(valueOrDefault2))
            {
              double num = valueOrDefault2.Value - valueOrDefault.Value;
              ClickyInkNumber clickyInkNumber2 = valueOrDefault2;
              clickyInkNumber2.ShowsPlus = true;
              clickyInkNumber2.Decimals = Math.Max(valueOrDefault2.Decimals, valueOrDefault.Decimals);
              return new ValueTuple<string, double>?(new ValueTuple<string, double>(clickyInkNumber2.Formatted(num), num));
            }
          }
        }
      }
      return null;
    }

    // Token: 0x060013C9 RID: 5065 RVA: 0x003A838C File Offset: 0x003A838C
    [NullableContext(0)]
    [CompilerGenerated]
    public override readonly string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkNumber");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060013CA RID: 5066 RVA: 0x003A83D8 File Offset: 0x003A83D8
    [NullableContext(0)]
    [CompilerGenerated]
    private readonly bool PrintMembers(StringBuilder builder)
    {
      builder.Append("Value = ");
      builder.Append(this.Value.ToString());
      builder.Append(", Prefix = ");
      builder.Append(this.Prefix);
      builder.Append(", Suffix = ");
      builder.Append(this.Suffix);
      builder.Append(", Decimals = ");
      builder.Append(this.Decimals.ToString());
      builder.Append(", UsesGrouping = ");
      builder.Append(this.UsesGrouping.ToString());
      builder.Append(", Scale = ");
      builder.Append(this.Scale.ToString());
      builder.Append(", ShowsPlus = ");
      builder.Append(this.ShowsPlus.ToString());
      return true;
    }

    // Token: 0x060013CB RID: 5067 RVA: 0x003A84DC File Offset: 0x003A84DC
    [CompilerGenerated]
    public static bool operator !=(ClickyInkNumber left, ClickyInkNumber right)
    {
      return !(left == right);
    }

    // Token: 0x060013CC RID: 5068 RVA: 0x003A84E8 File Offset: 0x003A84E8
    [CompilerGenerated]
    public static bool operator ==(ClickyInkNumber left, ClickyInkNumber right)
    {
      return left.Equals(right);
    }

    // Token: 0x060013CD RID: 5069 RVA: 0x003A84F4 File Offset: 0x003A84F4
    [CompilerGenerated]
    public override readonly int GetHashCode()
    {
      return (((((EqualityComparer<double>.Default.GetHashCode(this.<Value>k__BackingField) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Prefix>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Suffix>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Decimals>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<UsesGrouping>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Scale>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ShowsPlus>k__BackingField);
    }

    // Token: 0x060013CE RID: 5070 RVA: 0x003A859C File Offset: 0x003A859C
    [NullableContext(0)]
    [CompilerGenerated]
    public override readonly bool Equals(object obj)
    {
      return obj is ClickyInkNumber && this.Equals((ClickyInkNumber)obj);
    }

    // Token: 0x060013CF RID: 5071 RVA: 0x003A85B4 File Offset: 0x003A85B4
    [CompilerGenerated]
    public readonly bool Equals(ClickyInkNumber other)
    {
      return EqualityComparer<double>.Default.Equals(this.<Value>k__BackingField, other.<Value>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Prefix>k__BackingField, other.<Prefix>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Suffix>k__BackingField, other.<Suffix>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Decimals>k__BackingField, other.<Decimals>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<UsesGrouping>k__BackingField, other.<UsesGrouping>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Scale>k__BackingField, other.<Scale>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<ShowsPlus>k__BackingField, other.<ShowsPlus>k__BackingField);
    }

    // Token: 0x060013D0 RID: 5072 RVA: 0x003A866C File Offset: 0x003A866C
    [CompilerGenerated]
    internal static bool <Parse>g__AfterLetter|29_0(int count, ref ClickyInkNumber.<>c__DisplayClass29_0 A_1)
    {
      return A_1.lowerCharacters.Length == count || count >= A_1.lowerCharacters.Length || !InkText.IsLetter(A_1.lowerCharacters[count]);
    }

    // Token: 0x020004B2 RID: 1202
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001DB1 RID: 7601
      [Nullable(0)]
      public static Func<string, bool> <0>__IsLetter;
    }
  }
}
