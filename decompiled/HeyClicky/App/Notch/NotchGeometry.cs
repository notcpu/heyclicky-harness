using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Notch.Peek;
using HeyClicky.App.Notch.Surfaces;
using HeyClicky.Core.Notch;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Notch
{
  // Token: 0x02000135 RID: 309
  [NullableContext(1)]
  [Nullable(0)]
  public static class NotchGeometry
  {
    // Token: 0x17000311 RID: 785
    // (get) Token: 0x060010CF RID: 4303 RVA: 0x00589018 File Offset: 0x00589018
    public static bool HasHardwareNotchEar
    {
      get
      {
        return true;
      }
    }

    // Token: 0x060010D0 RID: 4304 RVA: 0x0058901C File Offset: 0x0058901C
    public static double EnvelopeHeight(double screenHeightDip, double contentScale = 1.0)
    {
      return Math.Min(900.0 * contentScale, Math.Max(120.0, screenHeightDip - 80.0)) / contentScale;
    }

    // Token: 0x060010D1 RID: 4305 RVA: 0x0058904C File Offset: 0x0058904C
    public static double ContentScale(bool streamMode)
    {
      if (!streamMode)
      {
        return 1.0;
      }
      return 1.5;
    }

    // Token: 0x060010D2 RID: 4306 RVA: 0x00589064 File Offset: 0x00589064
    public static NotchPresentation Shown(NotchPresentation p)
    {
      NotchPresentation.TextResponse textResponse = p as NotchPresentation.TextResponse;
      if (textResponse == null || !NotchGeometry.IsAwaitingProse(textResponse))
      {
        return p;
      }
      return new NotchPresentation.LiveEvent(new NotchLiveEvent.ActivityThinking());
    }

    // Token: 0x060010D3 RID: 4307 RVA: 0x00589090 File Offset: 0x00589090
    public static bool IsAwaitingProse(NotchPresentation.TextResponse r)
    {
      return r.IsThinking || string.IsNullOrWhiteSpace(r.Answer);
    }

    // Token: 0x060010D4 RID: 4308 RVA: 0x005890A8 File Offset: 0x005890A8
    public static double BottomCornerRadius(NotchPresentation p)
    {
      int num;
      if (NotchGeometry.IsWorkingBar(p))
      {
        num = 14;
      }
      else
      {
        NotchPresentation notchPresentation = NotchGeometry.Shown(p);
        int num2;
        if (!(notchPresentation is NotchPresentation.Idle))
        {
          if (!(notchPresentation is NotchPresentation.LiveEvent))
          {
            if (!(notchPresentation is NotchPresentation.OnboardingPinned))
            {
              num2 = 22;
            }
            else
            {
              num2 = 16;
            }
          }
          else
          {
            num2 = 18;
          }
        }
        else
        {
          num2 = 14;
        }
        num = num2;
      }
      return (double)num;
    }

    // Token: 0x060010D5 RID: 4309 RVA: 0x005890FC File Offset: 0x005890FC
    public static bool IsFloatingSliver(NotchPresentation p)
    {
      return p is NotchPresentation.Idle || NotchGeometry.IsWorkingBar(p);
    }

    // Token: 0x060010D6 RID: 4310 RVA: 0x00589110 File Offset: 0x00589110
    public static bool IsWorkingBar(NotchPresentation p)
    {
      if (NotchPresenter.RestingMarkIsGlowingBar)
      {
        NotchPresentation.LiveEvent liveEvent = p as NotchPresentation.LiveEvent;
        return liveEvent != null && liveEvent.Event is NotchLiveEvent.RestingMark;
      }
      return false;
    }

    // Token: 0x060010D7 RID: 4311 RVA: 0x00589140 File Offset: 0x00589140
    public static double ContentTopPadding(NotchPresentation p)
    {
      double num;
      if (NotchGeometry.Shown(p) is NotchPresentation.TextInput)
      {
        num = 34.0;
      }
      else if (NotchGeometry.NeedsHardwareNotchClearance(p))
      {
        num = Math.Max(28.0, 22.0);
      }
      else
      {
        num = 6.0;
      }
      return num;
    }

    // Token: 0x060010D8 RID: 4312 RVA: 0x00589194 File Offset: 0x00589194
    public static double ContentBottomPadding(NotchPresentation p)
    {
      if (!(NotchGeometry.Shown(p) is NotchPresentation.LiveEvent))
      {
        return NotchGeometry.BottomCornerRadius(p) / 2.0 + 4.0;
      }
      if (!NotchGeometry.NeedsHardwareNotchClearance(p))
      {
        return NotchGeometry.ContentTopPadding(p);
      }
      return 2.0;
    }

    // Token: 0x060010D9 RID: 4313 RVA: 0x005891E4 File Offset: 0x005891E4
    public static bool NeedsHardwareNotchClearance(NotchPresentation p)
    {
      bool flag = NotchGeometry.HasHardwareNotchEar;
      if (flag)
      {
        NotchPresentation.LiveEvent liveEvent = NotchGeometry.Shown(p) as NotchPresentation.LiveEvent;
        bool flag2;
        if (liveEvent != null)
        {
          NotchLiveEvent @event = liveEvent.Event;
          if (@event is NotchLiveEvent.IntegrationSuggestion || @event is NotchLiveEvent.IntegrationConnected)
          {
            flag2 = true;
            goto IL_0035;
          }
        }
        flag2 = false;
        IL_0035:
        flag = flag2;
      }
      return flag;
    }

    // Token: 0x060010DA RID: 4314 RVA: 0x0058922C File Offset: 0x0058922C
    public static SwiftSpring? SpringFor(NotchPresentation p)
    {
      SwiftSpring? swiftSpring;
      if (NotchGeometry.IsWorkingBar(p))
      {
        swiftSpring = new SwiftSpring?(NotchSprings.Close);
      }
      else
      {
        NotchPresentation notchPresentation = NotchGeometry.Shown(p);
        SwiftSpring? swiftSpring2;
        if (!(notchPresentation is NotchPresentation.Idle))
        {
          NotchPresentation.LiveEvent liveEvent = notchPresentation as NotchPresentation.LiveEvent;
          if (liveEvent != null)
          {
            if (liveEvent.Event is NotchLiveEvent.ActivityListening)
            {
              swiftSpring2 = null;
              goto IL_007A;
            }
          }
          else if (notchPresentation is NotchPresentation.TextInput)
          {
            swiftSpring2 = new SwiftSpring?(NotchSprings.TextInputOpen);
            goto IL_007A;
          }
          swiftSpring2 = new SwiftSpring?(NotchSprings.Open);
        }
        else
        {
          swiftSpring2 = new SwiftSpring?(NotchSprings.Close);
        }
        IL_007A:
        swiftSpring = swiftSpring2;
      }
      return swiftSpring;
    }

    // Token: 0x060010DB RID: 4315 RVA: 0x005892B8 File Offset: 0x005892B8
    [return: TupleElementNames(new string[] { "W", "H" })]
    [return: Nullable(0)]
    public static ValueTuple<double, double> PanelSize(NotchPresentation state, NotchGeometry.SizeContext ctx)
    {
      state = NotchGeometry.Shown(state);
      double num = 60.0;
      if (state is NotchPresentation.Idle)
      {
        return new ValueTuple<double, double>(num, 8.0);
      }
      NotchPresentation.LiveEvent liveEvent = state as NotchPresentation.LiveEvent;
      if (liveEvent != null)
      {
        NotchLiveEvent @event = liveEvent.Event;
        return NotchGeometry.LiveSize(@event, ctx);
      }
      NotchPresentation.TextInput textInput = state as NotchPresentation.TextInput;
      if (textInput != null)
      {
        double num2 = 34.0;
        double num3 = (double)(Math.Clamp(ctx.TextInputExtraLines, 0, 7) + 1) * NotchGeometry.InputLineHeight + 6.0;
        double num4 = (double)((ctx.TextInputAttachmentCount > 0) ? 54 : 0);
        double num5 = 16.0 + num4 + Math.Max(24.0, num3);
        double num6 = (double)((textInput.AgentFollowUp != null) ? 28 : 0);
        return new ValueTuple<double, double>(Math.Max(420.0, num + 220.0), num2 + num6 + num5 + 16.0);
      }
      NotchPresentation.TextResponse textResponse = state as NotchPresentation.TextResponse;
      if (textResponse == null)
      {
        if (!(state is NotchPresentation.OnboardingPinned))
        {
          return new ValueTuple<double, double>(num, 8.0);
        }
        return new ValueTuple<double, double>(440.0, Math.Min(170.0, ctx.ScreenHeightDip - 80.0));
      }
      else
      {
        string text = (textResponse.Answer ?? "").Trim();
        if ((textResponse.IsThinking && text.Length == 0) || text.Length == 0)
        {
          return new ValueTuple<double, double>(532.0, 70.0);
        }
        int num7 = 0;
        string[] array = text.Split('\n', StringSplitOptions.None);
        for (int i = 0; i < array.Length; i++)
        {
          int num8 = Math.Max(array[i].Length, 1);
          num7 += Math.Max(1, (num8 + 74 - 1) / 74);
        }
        double num9 = (double)(56 + 18 * Math.Max(3, num7));
        if (ctx.ResponseHasGallery)
        {
          num9 += 100.0;
        }
        num9 += 32.0;
        if (ctx.ResponseFinishedStreaming)
        {
          double num10 = (double)(Math.Clamp(ctx.FollowUpExtraLines, 0, 7) + 1) * NotchGeometry.InputLineHeight + 6.0;
          num9 += (double)(16 + ((ctx.FollowUpAttachmentCount > 0) ? 54 : 0)) + Math.Max(24.0, num10);
        }
        return new ValueTuple<double, double>(532.0, Math.Min(num9, ctx.ScreenHeightDip * 0.6));
      }
    }

    // Token: 0x060010DC RID: 4316 RVA: 0x0058954C File Offset: 0x0058954C
    [return: Nullable(0)]
    private static ValueTuple<double, double> LiveSize(NotchLiveEvent e, NotchGeometry.SizeContext ctx)
    {
      double num = 60.0;
      double num2 = 32.0;
      double num3 = 30.0;
      if (e is NotchLiveEvent.AgentTask)
      {
        return new ValueTuple<double, double>(Math.Max(220.0, num + 160.0), num2);
      }
      if (e is NotchLiveEvent.ActivityListening || e is NotchLiveEvent.ActivityThinking || e is NotchLiveEvent.ActivitySpeaking)
      {
        return new ValueTuple<double, double>(Math.Max(220.0, num + 160.0), num2);
      }
      NotchLiveEvent.AgentMultiActivity agentMultiActivity = e as NotchLiveEvent.AgentMultiActivity;
      if (agentMultiActivity != null)
      {
        bool agentLabelsHidden = ctx.AgentLabelsHidden;
        double num4 = (agentLabelsHidden ? 0.0 : Math.Min(NotchGeometry.MeasureLabel(agentMultiActivity.LongestLabel), 200.0));
        double num5 = 36.0;
        double num6 = Math.Max(num4 + num5, 18.0) + 32.0;
        return new ValueTuple<double, double>(Math.Min(Math.Max(agentLabelsHidden ? 200.0 : 220.0, num + num6 * 2.0), 700.0), num2);
      }
      NotchLiveEvent.ActivityCustomStatus activityCustomStatus = e as NotchLiveEvent.ActivityCustomStatus;
      if (activityCustomStatus != null)
      {
        bool agentDriving = ctx.AgentDriving;
        bool flag = agentDriving && ctx.AgentLabelsHidden;
        double num7 = (flag ? 0.0 : NotchGeometry.MeasureLabel(activityCustomStatus.Label));
        double num8 = ((agentDriving && !flag) ? Math.Min(num7, 200.0) : num7);
        double num9 = (agentDriving ? 36.0 : 0.0);
        double num10 = Math.Max(num8 + num9, 18.0) + (double)(agentDriving ? 32 : 10);
        return new ValueTuple<double, double>(Math.Min(Math.Max(flag ? 200.0 : 220.0, num + num10 * 2.0), (double)(agentDriving ? 700 : 820)), num2);
      }
      NotchLiveEvent.Dictation dictation = e as NotchLiveEvent.Dictation;
      if (dictation != null)
      {
        double num11 = Math.Max(NotchGeometry.MeasureLabel(dictation.Label), 30.0) + 10.0;
        return new ValueTuple<double, double>(Math.Min(Math.Max(220.0, num + num11 * 2.0), 700.0), num2);
      }
      if (e is NotchLiveEvent.IntroHatchPeek)
      {
        return new ValueTuple<double, double>(Math.Max(200.0, num + 70.0), num2);
      }
      if (!(e is NotchLiveEvent.RestingMark))
      {
        NotchLiveEvent.ClickyUpdate clickyUpdate = e as NotchLiveEvent.ClickyUpdate;
        if (clickyUpdate != null)
        {
          double num12 = Math.Max(560.0, num + 220.0);
          Func<NotchLiveEvent.ClickyUpdate, double> clickyUpdateHeight = ctx.ClickyUpdateHeight;
          return new ValueTuple<double, double>(num12, (clickyUpdateHeight != null) ? clickyUpdateHeight(clickyUpdate) : ClickyUpdateLayout.PanelHeight(NotchUpdateKindNames.Parse(clickyUpdate.Kind), null, false));
        }
        if (e is NotchLiveEvent.IntegrationConnected)
        {
          return new ValueTuple<double, double>(Math.Max(600.0, num + 380.0), ctx.IntegrationHasUseCases ? Math.Max(92.0, num3 + 84.0) : Math.Max(76.0, num3 + 58.0));
        }
        if (e is NotchLiveEvent.IntegrationSuggestion)
        {
          return new ValueTuple<double, double>(Math.Max(620.0, num + 420.0), ctx.IntegrationHasUseCases ? Math.Max(104.0, num3 + 84.0) : Math.Max(92.0, num3 + 58.0));
        }
        if (e is NotchLiveEvent.MeetingCountdown)
        {
          return new ValueTuple<double, double>(Math.Max(440.0, num + 210.0), NotchGeometry.<LiveSize>g__gapRow|39_0(34.0));
        }
        if (!(e is NotchLiveEvent.AgentPeek))
        {
          if (e is NotchLiveEvent.OnboardingTutorialResume)
          {
            return new ValueTuple<double, double>(Math.Max(480.0, num + 240.0), NotchGeometry.<LiveSize>g__gapRow|39_0((double)(ctx.TutorialResumeShowsContinue ? 108 : 44)));
          }
          if (!(e is NotchLiveEvent.MorningSuggestions))
          {
            if (e is NotchLiveEvent.OnboardingTutorialInvite)
            {
              return new ValueTuple<double, double>(Math.Max(480.0, num + 240.0), NotchGeometry.<LiveSize>g__gapRow|39_0(108.0));
            }
            if (e is NotchLiveEvent.AppUpdated)
            {
              return new ValueTuple<double, double>(Math.Max(460.0, num + 230.0), NotchGeometry.<LiveSize>g__gapRow|39_0(34.0));
            }
            if (e is NotchLiveEvent.MicrophoneTrouble)
            {
              return new ValueTuple<double, double>(ctx.MicrophoneShowsSettings ? Math.Max(540.0, num + 300.0) : Math.Max(500.0, num + 260.0), NotchGeometry.<LiveSize>g__gapRow|39_0(34.0));
            }
            if (e is NotchLiveEvent.DictationCopiedToClipboard)
            {
              return new ValueTuple<double, double>(Math.Max(360.0, num + 140.0), NotchGeometry.<LiveSize>g__gapRow|39_0(42.0));
            }
            if (e is NotchLiveEvent.VoiceReplyMutedCopiedToClipboard)
            {
              return new ValueTuple<double, double>(Math.Max(380.0, num + 160.0), NotchGeometry.<LiveSize>g__gapRow|39_0(90.0));
            }
            if (e is NotchLiveEvent.HandoffQueued || e is NotchLiveEvent.HandoffSent)
            {
              return new ValueTuple<double, double>(Math.Max(420.0, num + 220.0), 86.0);
            }
            if (e is NotchLiveEvent.SidecarLearningSuggestion)
            {
              return new ValueTuple<double, double>(Math.Max(620.0, num + 420.0), 92.0);
            }
            if (e is NotchLiveEvent.SpotifyVoiceTip)
            {
              return new ValueTuple<double, double>(Math.Max(680.0, num + 460.0), 92.0);
            }
            if (e is NotchLiveEvent.DictionaryWordLearned)
            {
              return new ValueTuple<double, double>(Math.Max(460.0, num + 230.0), NotchGeometry.<LiveSize>g__gapRow|39_0(34.0));
            }
            NotchLiveEvent.DocumentFolderAccessDenied documentFolderAccessDenied = e as NotchLiveEvent.DocumentFolderAccessDenied;
            if (documentFolderAccessDenied != null)
            {
              return new ValueTuple<double, double>(documentFolderAccessDenied.ShowsDontShowAgain ? Math.Max(630.0, num + 390.0) : Math.Max(500.0, num + 260.0), NotchGeometry.<LiveSize>g__gapRow|39_0(34.0));
            }
            NotchLiveEvent.IntroSkillPing introSkillPing = e as NotchLiveEvent.IntroSkillPing;
            if (introSkillPing != null)
            {
              return new ValueTuple<double, double>(num, 104.0 + Math.Min(18.0, Math.Max(0.0, introSkillPing.ScaleBias) * 44.0));
            }
            NotchLiveEvent.SkillPinned skillPinned = e as NotchLiveEvent.SkillPinned;
            if (skillPinned == null)
            {
              return new ValueTuple<double, double>(Math.Max(220.0, num + 160.0), num2);
            }
            return new ValueTuple<double, double>(Math.Max(200.0, num + 70.0 + (double)((Math.Clamp(skillPinned.Count, 1, 3) - 1) * 26)), num2);
          }
          else
          {
            if (!MorningSuggestionsSurface.ShowsPaper)
            {
              return new ValueTuple<double, double>(Math.Max(440.0, num + 220.0), num3 + 6.0 + 74.0 + 12.0);
            }
            return new ValueTuple<double, double>(Math.Max(540.0, num + 220.0), num3 + 6.0 + 112.0 + 12.0);
          }
        }
        else
        {
          Func<ValueTuple<double, double>> peekSize = ctx.PeekSize;
          if (peekSize == null)
          {
            return new ValueTuple<double, double>(536.0, NotchPeekLayout.ChatPanelHeight);
          }
          return peekSize();
        }
      }
      else
      {
        if (NotchPresenter.RestingMarkIsGlowingBar)
        {
          return new ValueTuple<double, double>(num, 8.0);
        }
        return new ValueTuple<double, double>(Math.Max(220.0, num + 160.0), num2);
      }
    }

    // Token: 0x060010DD RID: 4317 RVA: 0x00589DD8 File Offset: 0x00589DD8
    public static double MeasureLabel(string label)
    {
      if (string.IsNullOrEmpty(label))
      {
        return 0.0;
      }
      label += "...";
      if (NotchGeometry.measurer == null)
      {
        NotchGeometry.measurer = new TextBlock
        {
          FontSize = 11.0,
          FontWeight = FontWeights.SemiBold,
          FontFamily = DS.Fonts.For(11.0),
          IsTextScaleFactorEnabled = false
        };
      }
      NotchGeometry.measurer.Text = label;
      NotchGeometry.measurer.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      return Math.Ceiling(NotchGeometry.measurer.DesiredSize.Width);
    }

    // Token: 0x060010DF RID: 4319 RVA: 0x00589EA0 File Offset: 0x00589EA0
    [CompilerGenerated]
    internal static double <LiveSize>g__gapRow|39_0(double row)
    {
      return 40.0 + row + 16.0;
    }

    // Token: 0x04000765 RID: 1893
    public const double CollapsedTopRadiusPadding = 20.0;

    // Token: 0x04000766 RID: 1894
    public const double CollapsedWidth = 60.0;

    // Token: 0x04000767 RID: 1895
    public const double CollapsedHeight = 8.0;

    // Token: 0x04000768 RID: 1896
    public const double FloatingSliverTopGap = 4.0;

    // Token: 0x04000769 RID: 1897
    public const double MaxExpandedHeight = 280.0;

    // Token: 0x0400076A RID: 1898
    public const double ExpandedWidth = 532.0;

    // Token: 0x0400076B RID: 1899
    public const double OnboardingExpandedWidth = 440.0;

    // Token: 0x0400076C RID: 1900
    public const double StripHeight = 32.0;

    // Token: 0x0400076D RID: 1901
    public const double EnvelopeWidth = 820.0;

    // Token: 0x0400076E RID: 1902
    public const double EnvelopeHeightCap = 900.0;

    // Token: 0x0400076F RID: 1903
    public const double TopCornerRadius = 10.0;

    // Token: 0x04000770 RID: 1904
    public const double LibraryModeControlBandHeight = 30.0;

    // Token: 0x04000771 RID: 1905
    public const double ContentHorizontalPadding = 18.0;

    // Token: 0x04000772 RID: 1906
    public const double MeetingContentTopGap = 10.0;

    // Token: 0x04000773 RID: 1907
    public const double MeetingContentBottomGap = 16.0;

    // Token: 0x04000774 RID: 1908
    public const double TextInputBottomGap = 16.0;

    // Token: 0x04000775 RID: 1909
    public const double ComposerInternalVerticalPadding = 8.0;

    // Token: 0x04000776 RID: 1910
    public const double ActionButtonSize = 24.0;

    // Token: 0x04000777 RID: 1911
    public const double AgentLabelWidthCap = 200.0;

    // Token: 0x04000778 RID: 1912
    public const double AgentFaceBadgeSize = 26.0;

    // Token: 0x04000779 RID: 1913
    public const double StripMinWidth = 220.0;

    // Token: 0x0400077A RID: 1914
    public const double CompactStripMinWidth = 200.0;

    // Token: 0x0400077B RID: 1915
    public static double InputLineHeight = 16.0;

    // Token: 0x0400077C RID: 1916
    public const double HardwareNotchTopInset = 30.0;

    // Token: 0x0400077D RID: 1917
    [Nullable(2)]
    private static TextBlock measurer;

    // Token: 0x0200067E RID: 1662
    [NullableContext(0)]
    public sealed class SizeContext
    {
      // Token: 0x04001DAB RID: 7595
      public int TextInputExtraLines;

      // Token: 0x04001DAC RID: 7596
      public int TextInputAttachmentCount;

      // Token: 0x04001DAD RID: 7597
      public int FollowUpExtraLines;

      // Token: 0x04001DAE RID: 7598
      public int FollowUpAttachmentCount;

      // Token: 0x04001DAF RID: 7599
      public bool ResponseFinishedStreaming;

      // Token: 0x04001DB0 RID: 7600
      public bool ResponseHasGallery;

      // Token: 0x04001DB1 RID: 7601
      public bool IntegrationHasUseCases = true;

      // Token: 0x04001DB2 RID: 7602
      public bool MicrophoneShowsSettings = true;

      // Token: 0x04001DB3 RID: 7603
      public bool TutorialResumeShowsContinue = true;

      // Token: 0x04001DB4 RID: 7604
      [TupleElementNames(new string[] { "W", "H" })]
      [Nullable(new byte[] { 2, 0 })]
      public Func<ValueTuple<double, double>> PeekSize;

      // Token: 0x04001DB5 RID: 7605
      [Nullable(new byte[] { 2, 1 })]
      public Func<NotchLiveEvent.ClickyUpdate, double> ClickyUpdateHeight;

      // Token: 0x04001DB6 RID: 7606
      public bool AgentLabelsHidden = true;

      // Token: 0x04001DB7 RID: 7607
      public bool AgentDriving;

      // Token: 0x04001DB8 RID: 7608
      public double ScreenHeightDip = 1000.0;
    }
  }
}
