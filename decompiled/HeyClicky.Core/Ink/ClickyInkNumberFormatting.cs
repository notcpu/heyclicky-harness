using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000132 RID: 306
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkNumberFormatting
  {
    // Token: 0x060013D1 RID: 5073 RVA: 0x003A8698 File Offset: 0x003A8698
    public static string Decimal(double magnitude, int decimals, bool grouping)
    {
      string text = (grouping ? "#,##0" : "0") + ((decimals > 0) ? ("." + new string('0', decimals)) : "");
      string text2;
      try
      {
        decimal num = decimal.Parse(magnitude.ToString("R", CultureInfo.InvariantCulture), NumberStyles.Float, CultureInfo.InvariantCulture);
        text2 = Math.Round(num, Math.Min(decimals, 28), MidpointRounding.ToEven).ToString(text, CultureInfo.InvariantCulture);
      }
      catch (Exception obj) when (1)
      {
        text2 = magnitude.ToString(text, CultureInfo.InvariantCulture);
      }
      return text2;
    }

    // Token: 0x060013D2 RID: 5074 RVA: 0x003A8750 File Offset: 0x003A8750
    public static string Fixed(double value, int decimals)
    {
      string text;
      try
      {
        text = Math.Round(decimal.Parse(value.ToString("G17", CultureInfo.InvariantCulture), NumberStyles.Float, CultureInfo.InvariantCulture), decimals, MidpointRounding.ToEven).ToString("F" + decimals.ToString(), CultureInfo.InvariantCulture);
      }
      catch (Exception obj) when (1)
      {
        text = value.ToString("F" + decimals.ToString(), CultureInfo.InvariantCulture);
      }
      return text;
    }
  }
}
