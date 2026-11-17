using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000136 RID: 310
  public readonly struct ClickyInkProgress : IEquatable<ClickyInkProgress>
  {
    // Token: 0x170004A7 RID: 1191
    // (get) Token: 0x060013D9 RID: 5081 RVA: 0x003A94B8 File Offset: 0x003A94B8
    public double? Fraction { get; }

    // Token: 0x170004A8 RID: 1192
    // (get) Token: 0x060013DA RID: 5082 RVA: 0x003A94C0 File Offset: 0x003A94C0
    [Nullable(1)]
    public string Shown
    {
      [NullableContext(1)]
      get;
    }

    // Token: 0x060013DB RID: 5083 RVA: 0x003A94C8 File Offset: 0x003A94C8
    [NullableContext(1)]
    public ClickyInkProgress(IReadOnlyDictionary<string, string> attributes)
    {
      ClickyInkProgress.<>c__DisplayClass6_0 CS$<>8__locals1;
      CS$<>8__locals1.attributes = attributes;
      string text;
      if ((text = ClickyInkProgress.<.ctor>g__Get|6_0("value", ref CS$<>8__locals1)) == null && (text = ClickyInkProgress.<.ctor>g__Get|6_0("progress", ref CS$<>8__locals1)) == null)
      {
        text = ClickyInkProgress.<.ctor>g__Get|6_0("percent", ref CS$<>8__locals1) ?? ClickyInkProgress.<.ctor>g__Get|6_0("done", ref CS$<>8__locals1);
      }
      string text2 = text;
      string text3 = ((text2 != null) ? text2.TrimWs() : null) ?? "";
      string text4;
      if ((text4 = ClickyInkProgress.<.ctor>g__Get|6_0("of", ref CS$<>8__locals1)) == null && (text4 = ClickyInkProgress.<.ctor>g__Get|6_0("total", ref CS$<>8__locals1)) == null && (text4 = ClickyInkProgress.<.ctor>g__Get|6_0("goal", ref CS$<>8__locals1)) == null)
      {
        text4 = ClickyInkProgress.<.ctor>g__Get|6_0("max", ref CS$<>8__locals1) ?? ClickyInkProgress.<.ctor>g__Get|6_0("target", ref CS$<>8__locals1);
      }
      string text5 = text4;
      ClickyInkNumber? clickyInkNumber;
      double? num;
      if (text5 != null)
      {
        clickyInkNumber = ClickyInkNumber.Parse(text5);
        num = ((clickyInkNumber != null) ? new double?(clickyInkNumber.GetValueOrDefault().Value) : null);
      }
      else
      {
        num = null;
      }
      double? num2 = num;
      foreach (string text6 in new string[] { "/", " of " })
      {
        if (text3.Contains(text6, StringComparison.Ordinal))
        {
          string[] array2 = text3.Components(text6);
          if (array2.Length == 2)
          {
            clickyInkNumber = ClickyInkNumber.Parse(array2[0]);
            double? num3 = ((clickyInkNumber != null) ? new double?(clickyInkNumber.GetValueOrDefault().Value) : null);
            if (num3 != null)
            {
              double valueOrDefault = num3.GetValueOrDefault();
              clickyInkNumber = ClickyInkNumber.Parse(array2[1]);
              num3 = ((clickyInkNumber != null) ? new double?(clickyInkNumber.GetValueOrDefault().Value) : null);
              if (num3 != null)
              {
                double valueOrDefault2 = num3.GetValueOrDefault();
                if (valueOrDefault2 > 0.0)
                {
                  this.Fraction = new double?(valueOrDefault / valueOrDefault2);
                  this.Shown = ClickyInkProgress.Compact(valueOrDefault) + " of " + ClickyInkProgress.Compact(valueOrDefault2);
                  return;
                }
              }
            }
          }
        }
      }
      clickyInkNumber = ClickyInkNumber.Parse(text3);
      if (clickyInkNumber == null)
      {
        this.Fraction = null;
        this.Shown = ((text3.CharacterCount() <= 12) ? text3 : "");
        return;
      }
      ClickyInkNumber valueOrDefault3 = clickyInkNumber.GetValueOrDefault();
      if (num2 != null)
      {
        double valueOrDefault4 = num2.GetValueOrDefault();
        if (valueOrDefault4 > 0.0)
        {
          this.Fraction = new double?(valueOrDefault3.Value / valueOrDefault4);
          this.Shown = ClickyInkProgress.Compact(valueOrDefault3.Value) + " of " + ClickyInkProgress.Compact(valueOrDefault4);
          return;
        }
      }
      if (valueOrDefault3.Suffix.TrimWs() == "%" || valueOrDefault3.Value > 1.0)
      {
        this.Fraction = new double?(valueOrDefault3.Value / 100.0);
        this.Shown = ClickyInkProgress.Compact(valueOrDefault3.Value) + "%";
        return;
      }
      this.Fraction = new double?(valueOrDefault3.Value);
      this.Shown = ClickyInkProgress.Compact(valueOrDefault3.Value * 100.0) + "%";
    }

    // Token: 0x170004A9 RID: 1193
    // (get) Token: 0x060013DC RID: 5084 RVA: 0x003A9820 File Offset: 0x003A9820
    public double Fill
    {
      get
      {
        return Math.Min(1.0, Math.Max(0.0, this.Fraction.GetValueOrDefault()));
      }
    }

    // Token: 0x060013DD RID: 5085 RVA: 0x003A9858 File Offset: 0x003A9858
    [NullableContext(1)]
    private static string Compact(double value)
    {
      if (Math.Round(value, MidpointRounding.ToEven) != value)
      {
        return ClickyInkNumberFormatting.Fixed(value, 1);
      }
      return InkText.InkClamping(value).ToString(CultureInfo.InvariantCulture);
    }

    // Token: 0x060013DE RID: 5086 RVA: 0x003A988C File Offset: 0x003A988C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkProgress");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060013DF RID: 5087 RVA: 0x003A98D8 File Offset: 0x003A98D8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("Fraction = ");
      builder.Append(this.Fraction.ToString());
      builder.Append(", Shown = ");
      builder.Append(this.Shown);
      builder.Append(", Fill = ");
      builder.Append(this.Fill.ToString());
      return true;
    }

    // Token: 0x060013E0 RID: 5088 RVA: 0x003A9950 File Offset: 0x003A9950
    [CompilerGenerated]
    public static bool operator !=(ClickyInkProgress left, ClickyInkProgress right)
    {
      return !(left == right);
    }

    // Token: 0x060013E1 RID: 5089 RVA: 0x003A995C File Offset: 0x003A995C
    [CompilerGenerated]
    public static bool operator ==(ClickyInkProgress left, ClickyInkProgress right)
    {
      return left.Equals(right);
    }

    // Token: 0x060013E2 RID: 5090 RVA: 0x003A9968 File Offset: 0x003A9968
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<double?>.Default.GetHashCode(this.<Fraction>k__BackingField) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Shown>k__BackingField);
    }

    // Token: 0x060013E3 RID: 5091 RVA: 0x003A9994 File Offset: 0x003A9994
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is ClickyInkProgress && this.Equals((ClickyInkProgress)obj);
    }

    // Token: 0x060013E4 RID: 5092 RVA: 0x003A99AC File Offset: 0x003A99AC
    [CompilerGenerated]
    public bool Equals(ClickyInkProgress other)
    {
      return EqualityComparer<double?>.Default.Equals(this.<Fraction>k__BackingField, other.<Fraction>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Shown>k__BackingField, other.<Shown>k__BackingField);
    }

    // Token: 0x060013E5 RID: 5093 RVA: 0x003A99E0 File Offset: 0x003A99E0
    [NullableContext(1)]
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <.ctor>g__Get|6_0(string key, ref ClickyInkProgress.<>c__DisplayClass6_0 A_1)
    {
      string text;
      if (!A_1.attributes.TryGetValue(key, out text))
      {
        return null;
      }
      return text;
    }
  }
}
