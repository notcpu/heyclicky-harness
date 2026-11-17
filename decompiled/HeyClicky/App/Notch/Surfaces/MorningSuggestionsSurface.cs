using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.OneBrain;
using HeyClicky.App.Portraits;
using HeyClicky.App.Proactive;
using HeyClicky.App.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x02000154 RID: 340
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MorningSuggestionsSurface : NotchSurface
  {
    // Token: 0x060012E5 RID: 4837 RVA: 0x0059B098 File Offset: 0x0059B098
    public MorningSuggestionsSurface(NotchLiveEvent.MorningSuggestions e)
    {
      MorningSuggestionsSurface.<>c__DisplayClass1_0 CS$<>8__locals1;
      CS$<>8__locals1.constellation = new Canvas
      {
        Width = 161.2,
        Height = 59.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      PortraitAppearance portraitAppearance = PortraitAppearance.HomeMascot("#26C281");
      PortraitAppearance portraitAppearance2 = PortraitAppearance.HomeMascot("#3380FF");
      PortraitAppearance portraitAppearance3 = PortraitAppearance.HomeMascot("#FF4C4C");
      MorningSuggestionsSurface.<.ctor>g__Face|1_0(88.0, -64.0, -5.0, -9.0, portraitAppearance, 0.0, 3.2, ref CS$<>8__locals1);
      MorningSuggestionsSurface.<.ctor>g__Face|1_0(86.0, 64.0, -6.0, 8.0, portraitAppearance3, 0.12, 3.7, ref CS$<>8__locals1);
      MorningSuggestionsSurface.<.ctor>g__Face|1_0(120.0, 0.0, 4.0, -2.0, portraitAppearance2, 0.24, 3.4, ref CS$<>8__locals1);
      string text = MorningSuggestionsSurface.PaperHeadline();
      if (text != null)
      {
        TextBlock textBlock = Ui.Text(("This " + DateTime.Now.ToString("dddd", CultureInfo.GetCultureInfo("en-US")) + " morning").ToUpperInvariant(), 10.0, FontWeights.SemiBold, DS.Colors.TextSecondary, TextAlignment.Left, 1, false, false);
        textBlock.CharacterSpacing = 80;
        textBlock.Margin = new Thickness(0.0, 0.0, 0.0, 4.0);
        TextBlock textBlock2 = Ui.Text(text, 15.0, FontWeights.SemiBold, DS.Colors.TextPrimary, TextAlignment.Left, 2, true, false);
        textBlock2.Margin = new Thickness(0.0, 0.0, 0.0, 3.0);
        StackPanel stackPanel = new StackPanel
        {
          Width = 320.0,
          VerticalAlignment = VerticalAlignment.Center,
          Children = { textBlock, textBlock2 }
        };
        string text2 = MorningSuggestionsSurface.PaperSubtitle();
        if (text2 != null)
        {
          TextBlock textBlock3 = Ui.Text(text2, 12.0, FontWeights.Normal, DS.Colors.TextSecondary, TextAlignment.Left, 1, false, false);
          textBlock3.TextTrimming = TextTrimming.CharacterEllipsis;
          stackPanel.Children.Add(textBlock3);
        }
        GelButton gelButton = new GelButton("Read it", GelAppearance.PrimaryBlue, GelButton.Sizing.Normal, 100.0, "\ue736", null, null);
        gelButton.Click += delegate
        {
          ProactiveCoordinator proactiveCoordinator = ProactiveCoordinator.Current;
          if (proactiveCoordinator != null)
          {
            proactiveCoordinator.PlayMorningPaperFromNotch();
            return;
          }
          base.Host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.MorningSuggestions));
        };
        AutomationProperties.SetHelpText(gelButton, "Draws this morning's paper on your screen.");
        GelButton gelButton2 = new GelButton("Not now", GelAppearance.SecondaryOutline, GelButton.Sizing.Normal, 100.0, null, null, null);
        gelButton2.Click += delegate
        {
          ProactiveCoordinator proactiveCoordinator2 = ProactiveCoordinator.Current;
          if (proactiveCoordinator2 != null)
          {
            proactiveCoordinator2.DismissMorningNoticeFromExplicitClose();
            return;
          }
          base.Host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.MorningSuggestions));
        };
        AutomationProperties.SetHelpText(gelButton2, "Closes this card. Today's ideas stay in Suggestions.");
        StackPanel stackPanel2 = Ui.H(8.0, new UIElement[] { gelButton, gelButton2 });
        stackPanel2.Margin = new Thickness(0.0, 10.0, 0.0, 0.0);
        stackPanel.Children.Add(stackPanel2);
        StackPanel stackPanel3 = Ui.H(16.0, new UIElement[] { CS$<>8__locals1.constellation, stackPanel });
        stackPanel3.HorizontalAlignment = HorizontalAlignment.Center;
        stackPanel3.VerticalAlignment = VerticalAlignment.Top;
        stackPanel3.Margin = new Thickness(0.0, 36.0, 0.0, 0.0);
        this.view = new Grid
        {
          Children = { stackPanel3 }
        };
        return;
      }
      int count = e.Count;
      string text3;
      if (count != 1)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 1);
        defaultInterpolatedStringHandler.AppendLiteral("I can do ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(count);
        defaultInterpolatedStringHandler.AppendLiteral(" things for you today");
        text3 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text3 = "I can do 1 thing for you today";
      }
      FrameworkElement frameworkElement = Ui.Shrink(Ui.Text(text3, 14.0, FontWeights.SemiBold, DS.Colors.TextPrimary, TextAlignment.Left, 1, false, false), HorizontalAlignment.Left, 0.85);
      frameworkElement.Margin = new Thickness(0.0, 0.0, 0.0, 2.0);
      TextBlock textBlock4 = Ui.Text("Pick what you'd like me to take on.", 11.0, FontWeights.Normal, DS.Colors.TextSecondary, TextAlignment.Left, 1, false, false);
      textBlock4.Margin = new Thickness(0.0, 0.0, 0.0, 7.0);
      GelButton gelButton3 = new GelButton("Dismiss", GelAppearance.SecondaryOutline, GelButton.Sizing.Normal, 100.0, null, null, null);
      gelButton3.Click += delegate
      {
        ProactiveCoordinator proactiveCoordinator3 = ProactiveCoordinator.Current;
        if (proactiveCoordinator3 != null)
        {
          proactiveCoordinator3.DismissMorningNoticeFromExplicitClose();
          return;
        }
        base.Host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.MorningSuggestions));
      };
      GelButton gelButton4 = new GelButton("Show me", GelAppearance.PrimaryBlue, GelButton.Sizing.Normal, 100.0, "\ue82f", null, null);
      gelButton4.Click += delegate
      {
        ProactiveCoordinator proactiveCoordinator4 = ProactiveCoordinator.Current;
        if (proactiveCoordinator4 != null)
        {
          proactiveCoordinator4.OpenSuggestionsFromMorningNotch();
          return;
        }
        base.Host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.MorningSuggestions));
      };
      StackPanel stackPanel4 = new StackPanel
      {
        Width = 250.0,
        VerticalAlignment = VerticalAlignment.Center,
        Children = 
        {
          frameworkElement,
          textBlock4,
          Ui.H(8.0, new UIElement[] { gelButton3, gelButton4 })
        }
      };
      StackPanel stackPanel5 = Ui.H(14.0, new UIElement[] { CS$<>8__locals1.constellation, stackPanel4 });
      stackPanel5.HorizontalAlignment = HorizontalAlignment.Center;
      stackPanel5.VerticalAlignment = VerticalAlignment.Top;
      stackPanel5.Margin = new Thickness(0.0, 36.0, 0.0, 0.0);
      this.view = new Grid
      {
        Children = { stackPanel5 }
      };
    }

    // Token: 0x060012E6 RID: 4838 RVA: 0x0059B6E8 File Offset: 0x0059B6E8
    [NullableContext(2)]
    private static string PaperHeadline()
    {
      ProactiveCoordinator proactiveCoordinator = ProactiveCoordinator.Current;
      if (proactiveCoordinator == null)
      {
        return null;
      }
      ValueTuple<string, string>? valueTuple;
      string text = ((GoodMorningBrief.TodaysHeadline(proactiveCoordinator.CoreSettings, proactiveCoordinator.UserId) != null) ? valueTuple.GetValueOrDefault().Item1 : null);
      if (text != null)
      {
        return text;
      }
      if (!GoodMorningBrief.HasTodaysResearch(proactiveCoordinator.CoreSettings, proactiveCoordinator.UserId))
      {
        return null;
      }
      return "Your morning paper is ready";
    }

    // Token: 0x060012E7 RID: 4839 RVA: 0x0059B74C File Offset: 0x0059B74C
    [NullableContext(2)]
    private static string PaperSubtitle()
    {
      ProactiveCoordinator proactiveCoordinator = ProactiveCoordinator.Current;
      if (proactiveCoordinator == null)
      {
        return null;
      }
      if (GoodMorningBrief.TodaysHeadline(proactiveCoordinator.CoreSettings, proactiveCoordinator.UserId) == null)
      {
        return null;
      }
      ValueTuple<string, string>? valueTuple;
      return valueTuple.GetValueOrDefault().Item2;
    }

    // Token: 0x1700036D RID: 877
    // (get) Token: 0x060012E8 RID: 4840 RVA: 0x0059B790 File Offset: 0x0059B790
    public static bool ShowsPaper
    {
      get
      {
        GoodMorningBrief shared = GoodMorningBrief.Shared;
        return shared != null && (shared.NotchHeadline != null || shared.HasPaperToday);
      }
    }

    // Token: 0x060012E9 RID: 4841 RVA: 0x0059B7B8 File Offset: 0x0059B7B8
    private static void Arrive(UIElement e, double w, double h, double fromY, double delay)
    {
      MorningSuggestionsSurface.<>c__DisplayClass6_0 CS$<>8__locals1;
      CS$<>8__locals1.e = e;
      CS$<>8__locals1.delay = delay;
      Compositor compositor = Ui.Compositor;
      CubicBezierEasingFunction cubicBezierEasingFunction = compositor.CreateCubicBezierEasingFunction(new Vector2(0.22f, 1f), new Vector2(0.36f, 1f));
      CS$<>8__locals1.e.CenterPoint = new Vector3((float)w / 2f, (float)h / 2f, 0f);
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = compositor.CreateVector3KeyFrameAnimation();
      vector3KeyFrameAnimation.InsertKeyFrame(0f, new Vector3(0.84f, 0.84f, 1f));
      vector3KeyFrameAnimation.InsertKeyFrame(1f, Vector3.One, cubicBezierEasingFunction);
      MorningSuggestionsSurface.<Arrive>g__Run|6_0("Scale", MorningSuggestionsSurface.<Arrive>g__Timed|6_1(vector3KeyFrameAnimation, ref CS$<>8__locals1), ref CS$<>8__locals1);
      Vector3KeyFrameAnimation vector3KeyFrameAnimation2 = compositor.CreateVector3KeyFrameAnimation();
      vector3KeyFrameAnimation2.InsertKeyFrame(0f, new Vector3(0f, (float)fromY, 0f));
      vector3KeyFrameAnimation2.InsertKeyFrame(1f, Vector3.Zero, cubicBezierEasingFunction);
      MorningSuggestionsSurface.<Arrive>g__Run|6_0("Translation", MorningSuggestionsSurface.<Arrive>g__Timed|6_1(vector3KeyFrameAnimation2, ref CS$<>8__locals1), ref CS$<>8__locals1);
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = compositor.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(0f, 0f);
      scalarKeyFrameAnimation.InsertKeyFrame(1f, 1f, cubicBezierEasingFunction);
      MorningSuggestionsSurface.<Arrive>g__Run|6_0("Opacity", MorningSuggestionsSurface.<Arrive>g__Timed|6_1(scalarKeyFrameAnimation, ref CS$<>8__locals1), ref CS$<>8__locals1);
    }

    // Token: 0x1700036E RID: 878
    // (get) Token: 0x060012EA RID: 4842 RVA: 0x0059B8F8 File Offset: 0x0059B8F8
    public override FrameworkElement View
    {
      get
      {
        return this.view;
      }
    }

    // Token: 0x060012EB RID: 4843 RVA: 0x0059B900 File Offset: 0x0059B900
    [CompilerGenerated]
    internal static void <.ctor>g__Face|1_0(double size, double x, double y, double tilt, PortraitAppearance who, double delay, double duration, ref MorningSuggestionsSurface.<>c__DisplayClass1_0 A_7)
    {
      double num = 0.62;
      double num2 = size * num;
      double num3 = size * 0.74 * num;
      double num4 = size * 0.9 * num;
      PortraitView portraitView = new PortraitView(who, num4, num4, PortraitExpression.Happy, false, new Color?(who.HairColor.Hair()), size * 0.08 * num)
      {
        AnimatesWhileIdle = false,
        TracksPointer = false
      };
      Grid grid = new Grid
      {
        Width = num2,
        Height = num3,
        Children = { portraitView }
      };
      portraitView.HorizontalAlignment = HorizontalAlignment.Center;
      portraitView.VerticalAlignment = VerticalAlignment.Center;
      grid.CenterPoint = new Vector3((float)num2 / 2f, (float)num3 / 2f, 0f);
      grid.Rotation = (float)tilt;
      Grid grid2 = new Grid
      {
        Width = num2,
        Height = num3,
        Children = { grid }
      };
      Canvas.SetLeft(grid2, 80.6 + x * num - num2 / 2.0);
      Canvas.SetTop(grid2, 29.5 + y * num - num3 / 2.0);
      A_7.constellation.Children.Add(grid2);
      if (!Anim.Instant && !Anim.ReduceMotion)
      {
        MorningSuggestionsSurface.Arrive(grid2, num2, num3, -14.0 * num, delay);
      }
      if (!Anim.Instant)
      {
        double period = duration * 2.0;
        Anim.LoopScalar(grid, "Rotation", period, (double t) => (float)(tilt + 1.0 + Math.Sin(6.283185307179586 * (t / period + 0.75))), 48, 0.0);
        Anim.LoopVector3(grid, "Translation", period, (double t) => new Vector3(0f, (float)(-1.5 + 1.5 * Math.Sin(6.283185307179586 * (t / period + 0.25))), 0f), 48, 0.0);
      }
    }

    // Token: 0x060012F0 RID: 4848 RVA: 0x0059BBD8 File Offset: 0x0059BBD8
    [CompilerGenerated]
    internal static void <Arrive>g__Run|6_0(string target, CompositionAnimation a, ref MorningSuggestionsSurface.<>c__DisplayClass6_0 A_2)
    {
      a.Target = target;
      A_2.e.StartAnimation(a);
    }

    // Token: 0x060012F1 RID: 4849 RVA: 0x0059BBF0 File Offset: 0x0059BBF0
    [CompilerGenerated]
    internal static KeyFrameAnimation <Arrive>g__Timed|6_1(KeyFrameAnimation a, ref MorningSuggestionsSurface.<>c__DisplayClass6_0 A_1)
    {
      a.Duration = TimeSpan.FromSeconds(0.48);
      a.DelayTime = TimeSpan.FromSeconds(A_1.delay);
      a.DelayBehavior = AnimationDelayBehavior.SetInitialValueBeforeDelay;
      return a;
    }

    // Token: 0x040008B1 RID: 2225
    private readonly Grid view;
  }
}
