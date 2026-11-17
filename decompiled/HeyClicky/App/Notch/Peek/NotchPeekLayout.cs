using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Notch;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000190 RID: 400
  public static class NotchPeekLayout
  {
    // Token: 0x170003EB RID: 1003
    // (get) Token: 0x060015B2 RID: 5554 RVA: 0x005B6DB0 File Offset: 0x005B6DB0
    private static double FallbackFrontCardHeight
    {
      get
      {
        return 219.79999999999998;
      }
    }

    // Token: 0x170003EC RID: 1004
    // (get) Token: 0x060015B3 RID: 5555 RVA: 0x005B6DBC File Offset: 0x005B6DBC
    public static double BodyHeight
    {
      get
      {
        return Math.Round(84.0 + NotchPeekLayout.FallbackFrontCardHeight + 22.0 + 44.0 - 8.0);
      }
    }

    // Token: 0x170003ED RID: 1005
    // (get) Token: 0x060015B4 RID: 5556 RVA: 0x005B6DF0 File Offset: 0x005B6DF0
    public static double ChatBodyFrameHeight
    {
      get
      {
        return NotchPeekLayout.BodyHeight - 6.0;
      }
    }

    // Token: 0x170003EE RID: 1006
    // (get) Token: 0x060015B5 RID: 5557 RVA: 0x005B6E04 File Offset: 0x005B6E04
    public static double ChatPanelHeight
    {
      get
      {
        return 38.0 + NotchPeekLayout.BodyHeight;
      }
    }

    // Token: 0x060015B6 RID: 5558 RVA: 0x005B6E18 File Offset: 0x005B6E18
    [return: TupleElementNames(new string[] { "W", "H" })]
    public static ValueTuple<double, double> PanelSize([Nullable(1)] NotchTabs tabs, [Nullable(2)] ChatHandOffs handOffs, [TupleElementNames(new string[] { "W", "H" })] ValueTuple<double, double>? frontCardSize)
    {
      double num = Math.Max(536.0, 300.0);
      switch (tabs.Selected)
      {
      case NotchTab.Chat:
      {
        string expandedReplyEntryId = tabs.ExpandedReplyEntryId;
        if (expandedReplyEntryId != null)
        {
          double? fullReplyContentHeight = tabs.FullReplyContentHeight;
          if (fullReplyContentHeight != null)
          {
            double valueOrDefault = fullReplyContentHeight.GetValueOrDefault();
            double num2 = 34.0;
            double num3 = num2 + valueOrDefault + NotchPeekLayout.FullReplyFooter(handOffs, expandedReplyEntryId) + 6.0;
            double num4 = 678.0;
            return new ValueTuple<double, double>(ChatReplyPreview.UsesWideLayout(ChatReplyPreview.FullReplyMarkdown((handOffs != null) ? handOffs.Entry(expandedReplyEntryId) : null)) ? Math.Max(num, 820.0) : num, Math.Min(num4, Math.Max(num2 + 140.0, num3)));
          }
        }
        return new ValueTuple<double, double>(Math.Max(num, NotchChatSize.ContentWidth + 36.0), 38.0 + NotchChatSize.BodyHeight);
      }
      case NotchTab.Settings:
        return new ValueTuple<double, double>(Math.Max(num, 820.0), 648.0);
      case NotchTab.Document:
        return new ValueTuple<double, double>(Math.Max(num, 820.0), 678.0);
      default:
      {
        double num5 = 138.0;
        if (frontCardSize != null)
        {
          ValueTuple<double, double> valueOrDefault2 = frontCardSize.GetValueOrDefault();
          num = Math.Max(num, valueOrDefault2.Item1 + 56.0);
          num5 += 20.0 + valueOrDefault2.Item2 + 22.0;
        }
        else
        {
          num5 += 20.0;
        }
        return new ValueTuple<double, double>(num, num5);
      }
      }
    }

    // Token: 0x060015B7 RID: 5559 RVA: 0x005B6FD4 File Offset: 0x005B6FD4
    [NullableContext(1)]
    public static double FullReplyFooter([Nullable(2)] ChatHandOffs handOffs, string entryId)
    {
      return 64.0;
    }

    // Token: 0x060015B8 RID: 5560 RVA: 0x005B6FE0 File Offset: 0x005B6FE0
    [NullableContext(2)]
    public static double FullReplyContentWidth(string markdown)
    {
      if (!ChatReplyPreview.UsesWideLayout(markdown))
      {
        return 500.0;
      }
      return 784.0;
    }

    // Token: 0x04000A56 RID: 2646
    public const double ContentWidth = 500.0;

    // Token: 0x04000A57 RID: 2647
    public const double EdgeInset = 20.0;

    // Token: 0x04000A58 RID: 2648
    public const double BottomEdgeInset = 18.0;

    // Token: 0x04000A59 RID: 2649
    public const double NotchContentHorizontalPadding = 18.0;

    // Token: 0x04000A5A RID: 2650
    public const double NotchSideContentInset = 8.0;

    // Token: 0x04000A5B RID: 2651
    public const double NotchBottomContentInset = 6.0;

    // Token: 0x04000A5C RID: 2652
    public const double SidePadding = 12.0;

    // Token: 0x04000A5D RID: 2653
    public const double BottomPadding = 12.0;

    // Token: 0x04000A5E RID: 2654
    public const double PanelWidth = 536.0;

    // Token: 0x04000A5F RID: 2655
    public const double MenuBarBandHeight = 8.0;

    // Token: 0x04000A60 RID: 2656
    public const double HeaderRowHeight = 30.0;

    // Token: 0x04000A61 RID: 2657
    public const double HeaderRowTopPadding = 4.0;

    // Token: 0x04000A62 RID: 2658
    public const double BlackToGreyFadeHeight = 56.0;

    // Token: 0x04000A63 RID: 2659
    private const double ArcCardWidth = 494.0;

    // Token: 0x04000A64 RID: 2660
    private const double ArcCardHeight = 314.0;

    // Token: 0x04000A65 RID: 2661
    private const double ArcFrontScale = 0.7;

    // Token: 0x04000A66 RID: 2662
    public const double ArcCardTopGap = 20.0;

    // Token: 0x04000A67 RID: 2663
    public const double ArcCardBottomPadding = 22.0;

    // Token: 0x04000A68 RID: 2664
    public const double ArcCardSidePadding = 28.0;

    // Token: 0x04000A69 RID: 2665
    public const double FooterPillHeight = 32.0;

    // Token: 0x04000A6A RID: 2666
    public const double FooterHeight = 44.0;

    // Token: 0x04000A6B RID: 2667
    public const double GreetingBodyHeight = 64.0;

    // Token: 0x04000A6C RID: 2668
    public const double FullReplyMinimumBodyHeight = 140.0;

    // Token: 0x04000A6D RID: 2669
    public const double SettingsPanelWidth = 820.0;

    // Token: 0x04000A6E RID: 2670
    public const double SettingsBodyHeight = 610.0;

    // Token: 0x04000A6F RID: 2671
    public const double DocumentBodyHeight = 640.0;

    // Token: 0x04000A70 RID: 2672
    public static readonly TimeSpan DocumentMountDelay = TimeSpan.FromMilliseconds(380L, 0L);

    // Token: 0x04000A71 RID: 2673
    public static readonly TimeSpan SettingsSettleDelay = TimeSpan.FromMilliseconds(360L, 0L);

    // Token: 0x04000A72 RID: 2674
    public const double PillTopInset = 10.0;

    // Token: 0x04000A73 RID: 2675
    public const double TranscriptFadeHeight = 28.0;

    // Token: 0x04000A74 RID: 2676
    public const double PillHeight = 42.0;

    // Token: 0x04000A75 RID: 2677
    public const double CollapsedTextBoxWidth = 150.0;

    // Token: 0x04000A76 RID: 2678
    public const double ComposerBottomPadding = 12.0;

    // Token: 0x04000A77 RID: 2679
    public const double ComposerHeight = 64.0;

    // Token: 0x04000A78 RID: 2680
    public const double TopFadeHeight = 52.0;

    // Token: 0x04000A79 RID: 2681
    public const double VoicePillHeight = 24.0;

    // Token: 0x04000A7A RID: 2682
    public const double VoicePillScale = 0.86;
  }
}
