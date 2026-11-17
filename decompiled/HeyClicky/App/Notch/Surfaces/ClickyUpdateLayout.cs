using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Notch.Peek;
using HeyClicky.Core.Notch;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;
using Windows.UI.Text;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x02000159 RID: 345
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyUpdateLayout
  {
    // Token: 0x17000375 RID: 885
    // (get) Token: 0x06001300 RID: 4864 RVA: 0x0059CA48 File Offset: 0x0059CA48
    private static double DetailTextWidth
    {
      get
      {
        return 458.0;
      }
    }

    // Token: 0x17000376 RID: 886
    // (get) Token: 0x06001301 RID: 4865 RVA: 0x0059CA54 File Offset: 0x0059CA54
    private static double DetailTextWidthBesidePreview
    {
      get
      {
        return ClickyUpdateLayout.DetailTextWidth - 132.0 - 14.0;
      }
    }

    // Token: 0x06001302 RID: 4866 RVA: 0x0059CA70 File Offset: 0x0059CA70
    [NullableContext(2)]
    public static double ContentHeight(NotchUpdateKind kind, string detail, bool hasPreview = false)
    {
      double num = ClickyUpdateLayout.LineHeight(11.0, FontWeights.Normal);
      if (kind != NotchUpdateKind.Done && detail != null)
      {
        int num2 = ((detail.Length == 0) ? 1 : Math.Min(4, ClickyUpdateLayout.MeasuredLineCount(detail, 11.0, ClickyUpdateLayout.DetailTextWidth)));
        double num3 = ClickyUpdateLayout.LineHeight(14.0, FontWeights.SemiBold) + 3.0 + (double)num2 * num;
        return Math.Ceiling(Math.Max(32.0, num3)) + 9.0 + 34.0;
      }
      string text = (string.IsNullOrEmpty(detail) ? null : detail);
      int num4 = ((text == null) ? 1 : Math.Min(3, ClickyUpdateLayout.MeasuredLineCount(text, 11.0, hasPreview ? ClickyUpdateLayout.DetailTextWidthBesidePreview : ClickyUpdateLayout.DetailTextWidth)));
      double num5 = ClickyUpdateLayout.FinishedTitleRowHeight() + ((text == null) ? 1.0 : 3.0) + (double)num4 * num;
      return Math.Ceiling(Math.Max(32.0, num5)) + ((text == null) ? 12.0 : 9.0) + 34.0;
    }

    // Token: 0x06001303 RID: 4867 RVA: 0x0059CBA0 File Offset: 0x0059CBA0
    private static double FinishedTitleRowHeight()
    {
      double? num = ClickyUpdateLayout.stampHeight;
      if (num == null)
      {
        FrameworkElement frameworkElement = ChatStamp.Create(ChatStamp.Status.Ready, "done", 0);
        frameworkElement.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        ClickyUpdateLayout.stampHeight = new double?(Math.Ceiling(frameworkElement.DesiredSize.Height));
      }
      return Math.Max(ClickyUpdateLayout.LineHeight(14.0, FontWeights.SemiBold), ClickyUpdateLayout.stampHeight.Value);
    }

    // Token: 0x06001304 RID: 4868 RVA: 0x0059CC28 File Offset: 0x0059CC28
    [NullableContext(2)]
    public static double PanelHeight(NotchUpdateKind kind, string detail, bool hasPreview)
    {
      return 34.0 + ClickyUpdateLayout.ContentHeight(kind, detail, hasPreview) + 20.0;
    }

    // Token: 0x17000377 RID: 887
    // (get) Token: 0x06001305 RID: 4869 RVA: 0x0059CC48 File Offset: 0x0059CC48
    public static double ContentTopPadding
    {
      get
      {
        return 28.0;
      }
    }

    // Token: 0x06001306 RID: 4870 RVA: 0x0059CC54 File Offset: 0x0059CC54
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "W", "H" })]
    public static ValueTuple<double, double> PreviewSize([Nullable(2)] string detail)
    {
      double num = ClickyUpdateLayout.ContentHeight(NotchUpdateKind.Done, detail, true);
      return new ValueTuple<double, double>(Math.Min(132.0, Math.Round(num * 1.3)), num);
    }

    // Token: 0x06001307 RID: 4871 RVA: 0x0059CC90 File Offset: 0x0059CC90
    private static double LineHeight(double size, FontWeight weight)
    {
      if (ClickyUpdateLayout.measurer == null)
      {
        ClickyUpdateLayout.measurer = new TextBlock
        {
          IsTextScaleFactorEnabled = false,
          TextWrapping = TextWrapping.Wrap
        };
      }
      ClickyUpdateLayout.measurer.FontSize = size;
      ClickyUpdateLayout.measurer.FontWeight = weight;
      ClickyUpdateLayout.measurer.FontFamily = DS.Fonts.For(size);
      ClickyUpdateLayout.measurer.Text = "Ag";
      ClickyUpdateLayout.measurer.Width = double.NaN;
      ClickyUpdateLayout.measurer.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      return Math.Ceiling(ClickyUpdateLayout.measurer.DesiredSize.Height);
    }

    // Token: 0x06001308 RID: 4872 RVA: 0x0059CD3C File Offset: 0x0059CD3C
    private static int MeasuredLineCount(string text, double size, double width)
    {
      double num = ClickyUpdateLayout.LineHeight(size, FontWeights.Normal);
      ClickyUpdateLayout.measurer.FontWeight = FontWeights.Normal;
      ClickyUpdateLayout.measurer.Text = text;
      ClickyUpdateLayout.measurer.Width = width;
      ClickyUpdateLayout.measurer.Measure(new Size(width, double.PositiveInfinity));
      return Math.Max(1, (int)Math.Round(ClickyUpdateLayout.measurer.DesiredSize.Height / num));
    }

    // Token: 0x06001309 RID: 4873 RVA: 0x0059CDB4 File Offset: 0x0059CDB4
    [return: Nullable(2)]
    public static string Detail(NotchUpdateKind kind, NotchUpdateContent content)
    {
      if (kind == NotchUpdateKind.Done)
      {
        return ClickyUpdateLayout.SummaryText(content);
      }
      NotchUpdateRequest request = content.Request;
      if (request == null)
      {
        return null;
      }
      return request.Detail;
    }

    // Token: 0x0600130A RID: 4874 RVA: 0x0059CDD4 File Offset: 0x0059CDD4
    [return: Nullable(2)]
    public static string SummaryText(NotchUpdateContent content)
    {
      string summary = content.Summary;
      if (summary == null)
      {
        return null;
      }
      string text = string.Join(" ", summary.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)).Replace("**", "").Trim();
      if (text.Length != 0)
      {
        return text;
      }
      return null;
    }

    // Token: 0x0600130B RID: 4875 RVA: 0x0059CE30 File Offset: 0x0059CE30
    public static string FinishedTitle(NotchUpdateContent content, [Nullable(2)] string clickyName)
    {
      NotchUpdateReadyOutput readyOutput = content.ReadyOutput;
      bool flag;
      if (readyOutput != null)
      {
        if (!readyOutput.IsLink)
        {
          if (readyOutput.OpensFullReplyOnly)
          {
            string text = readyOutput.ButtonTitle;
            if (text == null)
            {
              goto IL_005F;
            }
            if (text.Length <= 0)
            {
              goto IL_005F;
            }
          }
          flag = content.ReadyOutput != null;
          goto IL_0061;
        }
        if (content.RoutineTitle == null)
        {
          string linkTitle = readyOutput.LinkTitle;
          if (linkTitle != null)
          {
            return linkTitle;
          }
        }
        flag = true;
        goto IL_0061;
      }
      IL_005F:
      flag = false;
      IL_0061:
      string text2 = (content.RoutineTitle ?? content.Title).Trim();
      if (text2.Length == 0 || (content.RoutineTitle == null && text2 == clickyName))
      {
        string text3 = clickyName ?? "Your job";
        if (!flag)
        {
          return text3 + " is done";
        }
        return text3 + " has something ready";
      }
      else
      {
        string text4 = text2.Split(' ', StringSplitOptions.None).FirstOrDefault<string>() ?? "";
        if (text4.Length > 4 && text4.EndsWith("ing", StringComparison.Ordinal) && char.IsUpper(text4[0]))
        {
          string text = text4;
          IEnumerable<char> enumerable = text.Substring(1, text.Length - 1);
          Func<char, bool> func;
          if ((func = ClickyUpdateLayout.<>O.<0>__IsLower) == null)
          {
            func = (ClickyUpdateLayout.<>O.<0>__IsLower = new Func<char, bool>(char.IsLower));
          }
          if (enumerable.All<char>(func) && !ClickyUpdateLayout.IngWordsThatAreThings.Contains(text4.ToLowerInvariant()))
          {
            ReadOnlySpan<char> readOnlySpan = "Finished ";
            char c = char.ToLowerInvariant(text2[0]);
            ReadOnlySpan<char> readOnlySpan2 = new ReadOnlySpan<char>(ref c);
            text = text2;
            return readOnlySpan + readOnlySpan2 + text.Substring(1, text.Length - 1);
          }
        }
        if (!flag)
        {
          return text2 + " is done";
        }
        return text2 + " is ready";
      }
    }

    // Token: 0x040008B5 RID: 2229
    public const double Width = 560.0;

    // Token: 0x040008B6 RID: 2230
    public const double EdgeGap = 20.0;

    // Token: 0x040008B7 RID: 2231
    public const double SideInset = 12.0;

    // Token: 0x040008B8 RID: 2232
    public const double BottomInset = 14.0;

    // Token: 0x040008B9 RID: 2233
    public const double ContentTopGap = 4.0;

    // Token: 0x040008BA RID: 2234
    public const double ButtonHeight = 34.0;

    // Token: 0x040008BB RID: 2235
    public const int DetailLineLimit = 4;

    // Token: 0x040008BC RID: 2236
    public const int SummaryLineLimit = 3;

    // Token: 0x040008BD RID: 2237
    public const double TitleFontSize = 14.0;

    // Token: 0x040008BE RID: 2238
    public const double DetailFontSize = 11.0;

    // Token: 0x040008BF RID: 2239
    private const double TitleToDetailGap = 3.0;

    // Token: 0x040008C0 RID: 2240
    private const double WordsToButtonsGap = 9.0;

    // Token: 0x040008C1 RID: 2241
    private const double IconSize = 32.0;

    // Token: 0x040008C2 RID: 2242
    public const double PreviewGap = 14.0;

    // Token: 0x040008C3 RID: 2243
    public const double PreviewMaxWidth = 132.0;

    // Token: 0x040008C4 RID: 2244
    private static double? stampHeight;

    // Token: 0x040008C5 RID: 2245
    [Nullable(2)]
    private static TextBlock measurer;

    // Token: 0x040008C6 RID: 2246
    private static readonly HashSet<string> IngWordsThatAreThings = new HashSet<string>
    {
      "morning", "evening", "meeting", "wedding", "building", "spring", "string", "thing", "things", "ceiling",
      "pricing", "marketing", "shopping", "booking", "bookings", "listing", "listings", "clothing", "housing", "training",
      "funding", "hiring", "onboarding", "branding", "setting", "settings", "earnings", "savings", "ranking", "rankings",
      "landing", "parking", "holdings", "opening", "openings", "reading", "running", "spending", "billing", "shipping",
      "packing", "trending", "breaking", "upcoming", "outstanding", "pending"
    };

    // Token: 0x020006BE RID: 1726
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001E8E RID: 7822
      [Nullable(0)]
      public static Func<char, bool> <0>__IsLower;
    }
  }
}
