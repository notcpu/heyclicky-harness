using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000300 RID: 768
  public static class TopArcGreetingLayout
  {
    // Token: 0x06002199 RID: 8601 RVA: 0x0061D10C File Offset: 0x0061D10C
    [return: TupleElementNames(new string[] { "W", "H" })]
    public static ValueTuple<double, double> PanelSize(double width, double headerRowHeight, [TupleElementNames(new string[] { "W", "H" })] ValueTuple<double, double>? frontCardSize)
    {
      double num = headerRowHeight + 64.0 + 44.0;
      if (frontCardSize != null)
      {
        ValueTuple<double, double> valueOrDefault = frontCardSize.GetValueOrDefault();
        width = Math.Max(width, valueOrDefault.Item1 + 56.0);
        num += 20.0 + valueOrDefault.Item2 + 22.0;
      }
      else
      {
        num += 20.0;
      }
      return new ValueTuple<double, double>(width, num);
    }

    // Token: 0x0600219A RID: 8602 RVA: 0x0061D18C File Offset: 0x0061D18C
    [NullableContext(1)]
    public static string Greeting(int hour, [Nullable(2)] string firstName)
    {
      string text = ((hour < 12) ? "Morning" : ((hour < 17) ? "Afternoon" : "Evening"));
      if (firstName == null || firstName.Length <= 0)
      {
        return text + ".";
      }
      return text + ", " + firstName + ".";
    }

    // Token: 0x0600219B RID: 8603 RVA: 0x0061D1E0 File Offset: 0x0061D1E0
    [NullableContext(2)]
    public static string FirstName(string displayName, string email)
    {
      string text = ((displayName != null) ? displayName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault<string>() : null);
      if (text != null && text.Length > 0)
      {
        return text;
      }
      string text2 = ((email != null) ? email.Split('@', StringSplitOptions.None).FirstOrDefault<string>() : null);
      if (text2 != null && text2.Length > 0)
      {
        string text3 = text2.Split(RuntimeHelpers.CreateSpan<char>(fieldof(<PrivateImplementationDetails>.73BF42C4F90EAC2D219F322C2FF3DD31C26B0124325CAED15C602CCAB325D1272).FieldHandle)).FirstOrDefault<string>((string p) => p.Length > 0) ?? text2;
        if (text3.Length == 0)
        {
          text3 = text2;
        }
        char c = char.ToUpperInvariant(text3[0]);
        ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(ref c);
        string text4 = text3;
        return readOnlySpan + text4.Substring(1, text4.Length - 1);
      }
      return null;
    }

    // Token: 0x0600219C RID: 8604 RVA: 0x0061D2B4 File Offset: 0x0061D2B4
    [NullableContext(1)]
    public static string StatusLine(int workingCount, int needsYouCount, string voiceHoldDescription)
    {
      List<string> list = new List<string>();
      if (workingCount > 0)
      {
        List<string> list2 = list;
        string text;
        if (workingCount != 1)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(13, 1);
          defaultInterpolatedStringHandler.AppendFormatted<int>(workingCount);
          defaultInterpolatedStringHandler.AppendLiteral(" jobs working");
          text = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        else
        {
          text = "1 job working";
        }
        list2.Add(text);
      }
      if (needsYouCount > 0)
      {
        List<string> list3 = list;
        string text2;
        if (needsYouCount != 1)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 1);
          defaultInterpolatedStringHandler.AppendFormatted<int>(needsYouCount);
          defaultInterpolatedStringHandler.AppendLiteral(" need you");
          text2 = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        else
        {
          text2 = "1 needs you";
        }
        list3.Add(text2);
      }
      if (list.Count > 0)
      {
        return string.Join(" · ", list);
      }
      return "Hold " + voiceHoldDescription + " and tell me what to take off your plate.";
    }

    // Token: 0x04001029 RID: 4137
    public const double BodyHeight = 64.0;

    // Token: 0x0400102A RID: 4138
    public const double EmptyDockExtraGap = 20.0;
  }
}
