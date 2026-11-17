using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HeyClicky.App.Design;
using HeyClicky.App.Interop;
using HeyClicky.App.Services;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using HeyClicky.Core.Onboarding;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Voice;
using HeyClicky.Platform.Input;
using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.System;
using Windows.System.UserProfile;
using Windows.UI;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x02000122 RID: 290
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class IntroWindow
  {
    // Token: 0x1400003C RID: 60
    // (add) Token: 0x06000FC8 RID: 4040 RVA: 0x0057E204 File Offset: 0x0057E204
    // (remove) Token: 0x06000FC9 RID: 4041 RVA: 0x0057E23C File Offset: 0x0057E23C
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<IntroWindow.Point2> FlightRequested;

    // Token: 0x1400003D RID: 61
    // (add) Token: 0x06000FCA RID: 4042 RVA: 0x0057E274 File Offset: 0x0057E274
    // (remove) Token: 0x06000FCB RID: 4043 RVA: 0x0057E2AC File Offset: 0x0057E2AC
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action ReducedMotionHandOff;

    // Token: 0x1400003E RID: 62
    // (add) Token: 0x06000FCC RID: 4044 RVA: 0x0057E2E4 File Offset: 0x0057E2E4
    // (remove) Token: 0x06000FCD RID: 4045 RVA: 0x0057E31C File Offset: 0x0057E31C
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Finished;

    // Token: 0x170002E7 RID: 743
    // (get) Token: 0x06000FCE RID: 4046 RVA: 0x0057E354 File Offset: 0x0057E354
    public PanelWindow Window
    {
      get
      {
        return this.window;
      }
    }

    // Token: 0x170002E8 RID: 744
    // (get) Token: 0x06000FCF RID: 4047 RVA: 0x0057E35C File Offset: 0x0057E35C
    // (set) Token: 0x06000FD0 RID: 4048 RVA: 0x0057E364 File Offset: 0x0057E364
    [Nullable(2)]
    public PreSignInIntroViewModel Vm
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06000FD1 RID: 4049 RVA: 0x0057E370 File Offset: 0x0057E370
    public void OnSignedInChanged(bool signedIn)
    {
      bool? flag = this.lastSignedIn;
      bool flag2 = !((flag.GetValueOrDefault() == signedIn) & (flag != null));
      this.lastSignedIn = new bool?(signedIn);
      if (signedIn && flag2 && this.IsShowing)
      {
        this.window.BringToFrontRecentered();
        ClickyAnalytics.TrackPreSignInIntroAttentionRequested(IntroWindow.AnalyticsName(this.step));
        Log.Info("intro", "signed in: intro brought to front and re-centered");
      }
      if (this.step == IntroWindow.Step.WhatIsClicky && signedIn)
      {
        this.Go(IntroWindow.Step.WhereHeardAbout);
        return;
      }
      IntroWindow.Step step = this.step;
      bool flag3 = step == IntroWindow.Step.WhatIsClicky || step == IntroWindow.Step.Hatched;
      if (flag3)
      {
        this.Render(false);
      }
    }

    // Token: 0x170002E9 RID: 745
    // (get) Token: 0x06000FD2 RID: 4050 RVA: 0x0057E414 File Offset: 0x0057E414
    public IntroWindow.Step CurrentStep
    {
      get
      {
        return this.step;
      }
    }

    // Token: 0x06000FD3 RID: 4051 RVA: 0x0057E41C File Offset: 0x0057E41C
    private void OnSignInErrorChanged()
    {
      if (this.step != IntroWindow.Step.WhatIsClicky || this.s.Auth.IsSignedIn)
      {
        return;
      }
      this.Render(false);
      if (this.IsShowing && this.s.Auth.LastSignInErrorMessage != null)
      {
        this.window.BringToFrontRecentered();
      }
    }

    // Token: 0x06000FD4 RID: 4052 RVA: 0x0057E470 File Offset: 0x0057E470
    public IntroWindow(AppServices services, IAppActions actions, bool excludeFromCapture)
    {
      this.s = services;
      this.actions = actions;
      this.dq = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
      this.lastSignedIn = new bool?(this.s.Auth.IsSignedIn);
      this.window = new PanelWindow("HeyClicky", excludeFromCapture, true, 14.0, null, 0.1);
      this.layout.Margin = new Thickness(24.0);
      this.layout.Width = 392.0;
      this.layout.MinimumHeight = 232.0;
      this.window.Panel.Children.Add(this.layout);
      this.layout.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        this.Grow();
      };
      this.s.Auth.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.dq.TryEnqueue(delegate
        {
          this.OnSignedInChanged(this.s.Auth.IsSignedIn);
        });
      };
      this.s.Auth.SignInErrorChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.dq.TryEnqueue(new Microsoft.UI.Dispatching.DispatcherQueueHandler(this.OnSignInErrorChanged));
      };
      this.window.Panel.PointerEntered += delegate(object _, PointerRoutedEventArgs _)
      {
        if (this.step == IntroWindow.Step.WhatIsClicky)
        {
          this.Shake();
        }
      };
      this.window.SetPanelSize(440.0, 280.0);
      this.Render(false);
    }

    // Token: 0x06000FD5 RID: 4053 RVA: 0x0057E5F0 File Offset: 0x0057E5F0
    public void Show(IntroWindow.Step at = IntroWindow.Step.WhatIsClicky)
    {
      this.step = at;
      this.Render(false);
      this.window.ShowCentered();
      ClickyAnalytics.TrackPreSignInIntroStepViewed(IntroWindow.AnalyticsName(at));
    }

    // Token: 0x06000FD6 RID: 4054 RVA: 0x0057E618 File Offset: 0x0057E618
    public static string AnalyticsName(IntroWindow.Step st)
    {
      string text;
      switch (st)
      {
      case IntroWindow.Step.WhatIsClicky:
        text = "what_is_clicky";
        break;
      case IntroWindow.Step.WhereHeardAbout:
        text = "where_heard_about";
        break;
      case IntroWindow.Step.PickLanguage:
        text = "pick_language";
        break;
      case IntroWindow.Step.OpenClicky:
        text = "open_clicky";
        break;
      default:
        text = "hatched";
        break;
      }
      return text;
    }

    // Token: 0x06000FD7 RID: 4055 RVA: 0x0057E664 File Offset: 0x0057E664
    private void TrackButton(string action, [Nullable(2)] string value = null)
    {
      ClickyAnalytics.TrackPreSignInIntroButtonTapped(IntroWindow.AnalyticsName(this.step), action, value);
    }

    // Token: 0x06000FD8 RID: 4056 RVA: 0x0057E678 File Offset: 0x0057E678
    public void Hide()
    {
      this.window.HideWindow();
    }

    // Token: 0x170002EA RID: 746
    // (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0057E688 File Offset: 0x0057E688
    public bool IsShowing
    {
      get
      {
        return Native.IsWindowVisible(this.window.Hwnd);
      }
    }

    // Token: 0x06000FDA RID: 4058 RVA: 0x0057E69C File Offset: 0x0057E69C
    private void Grow()
    {
      this.layout.Measure(new Size(392.0, double.PositiveInfinity));
      double num = Math.Max(this.grownHeight, this.layout.DesiredSize.Height + 48.0);
      if (num > this.grownHeight + 0.5)
      {
        this.grownHeight = num;
        this.window.SetPanelSize(440.0, num);
        Native.RECT rect;
        if (this.IsShowing && Native.GetWindowRectPublic(this.window.Hwnd, out rect))
        {
          string text = "intro";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 6);
          defaultInterpolatedStringHandler.AppendLiteral("grew to ");
          defaultInterpolatedStringHandler.AppendFormatted<double>(num, "0");
          defaultInterpolatedStringHandler.AppendLiteral(" pt on ");
          defaultInterpolatedStringHandler.AppendFormatted(IntroWindow.AnalyticsName(this.step));
          defaultInterpolatedStringHandler.AppendLiteral(": re-centered at ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(rect.Left);
          defaultInterpolatedStringHandler.AppendLiteral(",");
          defaultInterpolatedStringHandler.AppendFormatted<int>(rect.Top);
          defaultInterpolatedStringHandler.AppendLiteral(" ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(rect.Right - rect.Left);
          defaultInterpolatedStringHandler.AppendLiteral("x");
          defaultInterpolatedStringHandler.AppendFormatted<int>(rect.Bottom - rect.Top);
          defaultInterpolatedStringHandler.AppendLiteral(" px");
          Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        }
      }
    }

    // Token: 0x06000FDB RID: 4059 RVA: 0x0057E818 File Offset: 0x0057E818
    public void Go(IntroWindow.Step next)
    {
      if (next == this.step)
      {
        return;
      }
      this.step = next;
      ClickyAnalytics.TrackPreSignInIntroStepViewed(IntroWindow.AnalyticsName(next));
      this.Render(true);
      if (next == IntroWindow.Step.Hatched)
      {
        this.RunHatchTimeline();
      }
    }

    // Token: 0x170002EB RID: 747
    // (get) Token: 0x06000FDC RID: 4060 RVA: 0x0057E848 File Offset: 0x0057E848
    private IntroWindow.Step? Previous
    {
      get
      {
        IntroWindow.Step? step;
        switch (this.step)
        {
        case IntroWindow.Step.WhereHeardAbout:
          step = new IntroWindow.Step?(IntroWindow.Step.WhatIsClicky);
          break;
        case IntroWindow.Step.PickLanguage:
          step = new IntroWindow.Step?(IntroWindow.Step.WhereHeardAbout);
          break;
        case IntroWindow.Step.OpenClicky:
          step = new IntroWindow.Step?(this.LanguageStepEnabled ? IntroWindow.Step.PickLanguage : IntroWindow.Step.WhereHeardAbout);
          break;
        default:
          step = null;
          break;
        }
        return step;
      }
    }

    // Token: 0x06000FDD RID: 4061 RVA: 0x0057E8A4 File Offset: 0x0057E8A4
    private void Render(bool animate)
    {
      ValueTuple<string, string> valueTuple = this.Copy();
      string item = valueTuple.Item1;
      string item2 = valueTuple.Item2;
      FrameworkElement frameworkElement = this.Header(item, item2);
      FrameworkElement body = this.Body();
      FrameworkElement frameworkElement2 = this.Footer();
      bool flag = frameworkElement2 != null;
      UIElement body2 = this.layout.Body;
      this.layout.SetParts(frameworkElement, body, frameworkElement2, flag);
      this.footerHost = frameworkElement2;
      if (this.step == IntroWindow.Step.Hatched)
      {
        this.titleText.Opacity = 0.0;
        this.subtitleText.Opacity = 0.0;
        if (frameworkElement2 != null)
        {
          frameworkElement2.Opacity = 0.0;
        }
      }
      if (animate)
      {
        body.Opacity = 0.0;
        body.Translation = new Vector3(0f, 12f, 0f);
        body.Scale = new Vector3(0.985f, 0.985f, 1f);
        body.SizeChanged += delegate(object _, SizeChangedEventArgs _)
        {
          body.CenterPoint = new Vector3((float)body.ActualWidth / 2f, (float)body.ActualHeight / 2f, 0f);
        };
        Anim.OpacitySpring(body, 1f, IntroWindow.StepSpring);
        Anim.TranslationSpring(body, Vector3.Zero, IntroWindow.StepSpring);
        Anim.ScaleSpring(body, 1f, IntroWindow.StepSpring, null);
      }
    }

    // Token: 0x06000FDE RID: 4062 RVA: 0x0057EA18 File Offset: 0x0057EA18
    [return: Nullable(new byte[] { 0, 1, 1 })]
    private ValueTuple<string, string> Copy()
    {
      ValueTuple<string, string> valueTuple;
      switch (this.step)
      {
      case IntroWindow.Step.WhatIsClicky:
        valueTuple = new ValueTuple<string, string>("Welcome to HeyClicky.", "A tiny AI buddy that lives on your PC.");
        break;
      case IntroWindow.Step.WhereHeardAbout:
        valueTuple = new ValueTuple<string, string>("How’d you find this?", "We're a tiny team, and knowing where you heard about us helps a ton.");
        break;
      case IntroWindow.Step.PickLanguage:
        valueTuple = new ValueTuple<string, string>("What should I speak?", "Pick one or more. Automatic answers in whatever language you talk to me in.");
        break;
      case IntroWindow.Step.OpenClicky:
        valueTuple = new ValueTuple<string, string>("Pick a color", "Click one to bring me to life. Choose wisely.");
        break;
      default:
        valueTuple = new ValueTuple<string, string>("Hi, " + (this.FirstName() ?? "friend") + ".", "I'm HeyClicky.");
        break;
      }
      return valueTuple;
    }

    // Token: 0x06000FDF RID: 4063 RVA: 0x0057EAB8 File Offset: 0x0057EAB8
    [NullableContext(2)]
    private string FirstName()
    {
      UserProfile user = this.s.Auth.User;
      if (user == null)
      {
        return null;
      }
      string text = null;
      if (!string.IsNullOrWhiteSpace(user.DisplayName))
      {
        text = user.DisplayName.Trim().Split(' ', StringSplitOptions.None)[0];
      }
      else if (!string.IsNullOrEmpty(user.Email))
      {
        string text2 = user.Email.Split('@', StringSplitOptions.None)[0];
        string text3 = text2.Split(RuntimeHelpers.CreateSpan<char>(fieldof(<PrivateImplementationDetails>.E087D87E930FD023CB679FE3A3031DC2A4A4F4FAFFDDF74D1F5AC8267743B9E42).FieldHandle))[0];
        text = ((text3.Length >= 2) ? text3 : text2);
      }
      if (string.IsNullOrEmpty(text))
      {
        return null;
      }
      char c = char.ToUpperInvariant(text[0]);
      ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(ref c);
      string text4 = text;
      return readOnlySpan + text4.Substring(1, text4.Length - 1);
    }

    // Token: 0x06000FE0 RID: 4064 RVA: 0x0057EB80 File Offset: 0x0057EB80
    private FrameworkElement Header(string title, string subtitle)
    {
      Grid grid = new Grid
      {
        Height = 28.0,
        Margin = new Thickness(0.0, 0.0, 0.0, 12.0)
      };
      IntroWindow.Step? previous = this.Previous;
      if (previous != null)
      {
        IntroWindow.Step prev = previous.GetValueOrDefault();
        Pressable pressable = new Pressable(true)
        {
          HorizontalAlignment = HorizontalAlignment.Left,
          Padding = new Thickness(4.0, 6.0, 4.0, 6.0)
        };
        TextBlock textBlock = Icons.Glyph("\ue76b", 10.0, DS.Colors.TextSecondary);
        textBlock.FontWeight = FontWeights.SemiBold;
        pressable.Children.Add(Ui.H(3.0, new UIElement[]
        {
          textBlock,
          Ui.Medium("Back", 12.0, DS.Colors.TextSecondary)
        }));
        pressable.WithPressScale(0.97f, 0.14, false);
        pressable.Click += delegate
        {
          this.Go(prev);
        };
        grid.Children.Add(pressable);
      }
      this.titleText = Ui.Text(title, 20.0, FontWeights.SemiBold, DS.Colors.TextPrimary, TextAlignment.Center, 0, true, false);
      this.subtitleText = Ui.Text(subtitle, 13.0, FontWeights.Medium, DS.Colors.TextSecondary, TextAlignment.Center, 0, true, false);
      this.subtitleText.LineHeight = 19.29;
      this.subtitleText.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 8.0,
        Margin = new Thickness(0.0, 0.0, 0.0, 22.0),
        Children = { this.titleText, this.subtitleText }
      };
      return new StackPanel
      {
        Children = { grid, stackPanel }
      };
    }

    // Token: 0x06000FE1 RID: 4065 RVA: 0x0057EDC4 File Offset: 0x0057EDC4
    private FrameworkElement Body()
    {
      FrameworkElement frameworkElement;
      switch (this.step)
      {
      case IntroWindow.Step.WhatIsClicky:
        frameworkElement = this.WelcomeGlyph();
        break;
      case IntroWindow.Step.WhereHeardAbout:
        frameworkElement = this.Discovery();
        break;
      case IntroWindow.Step.PickLanguage:
        frameworkElement = this.LanguagePicker();
        break;
      case IntroWindow.Step.OpenClicky:
        frameworkElement = this.ColorPicker();
        break;
      default:
        frameworkElement = this.HatchGlyph();
        break;
      }
      return frameworkElement;
    }

    // Token: 0x06000FE2 RID: 4066 RVA: 0x0057EE1C File Offset: 0x0057EE1C
    [NullableContext(2)]
    private FrameworkElement Footer()
    {
      GelButton gelButton = null;
      switch (this.step)
      {
      case IntroWindow.Step.WhatIsClicky:
        if (this.s.Auth.IsSignedIn)
        {
          gelButton = new GelButton("Continue", GelAppearance.PrimaryBlue, GelButton.Sizing.Large, 100.0, null, null, null);
          gelButton.Click += this.ContinueFromWelcome;
        }
        else
        {
          gelButton = new GelButton("Sign in with Google", GelAppearance.PrimaryBlue, GelButton.Sizing.Large, 100.0, null, null, null);
          gelButton.Click += this.SignInFromWelcome;
        }
        break;
      case IntroWindow.Step.WhereHeardAbout:
        if (!this.otherPicked)
        {
          return null;
        }
        gelButton = new GelButton("Continue", GelAppearance.PrimaryBlue, GelButton.Sizing.Large, 100.0, null, null, null);
        gelButton.Click += this.CommitOther;
        break;
      case IntroWindow.Step.PickLanguage:
        gelButton = new GelButton("Continue", GelAppearance.PrimaryBlue, GelButton.Sizing.Large, 100.0, null, null, null);
        gelButton.Click += this.ContinueLanguage;
        break;
      case IntroWindow.Step.OpenClicky:
        return null;
      case IntroWindow.Step.Hatched:
        gelButton = new GelButton("Give me a home", GelAppearance.PrimaryBlue, GelButton.Sizing.Large, 100.0, null, null, null);
        gelButton.Click += this.GiveHome;
        break;
      }
      if (gelButton == null)
      {
        return null;
      }
      gelButton.HorizontalAlignment = HorizontalAlignment.Center;
      StackPanel stackPanel = new StackPanel
      {
        Padding = new Thickness(0.0, 22.0, 0.0, 0.0),
        Spacing = 8.0,
        Children = { gelButton }
      };
      if (this.step == IntroWindow.Step.WhatIsClicky && !this.s.Auth.IsSignedIn)
      {
        string lastSignInErrorMessage = this.s.Auth.LastSignInErrorMessage;
        if (lastSignInErrorMessage != null)
        {
          TextBlock textBlock = Ui.Text(lastSignInErrorMessage, 12.0, FontWeights.Normal, DS.Hex(16737129U, 1.0), TextAlignment.Center, 3, true, false);
          textBlock.MaxWidth = 320.0;
          stackPanel.Children.Add(textBlock);
        }
      }
      if (this.step == IntroWindow.Step.WhatIsClicky && !this.s.Auth.IsSignedIn)
      {
        GelButton gelButton2 = DevSignIn.Button(this.actions, GelButton.Sizing.Compact);
        if (gelButton2 != null)
        {
          stackPanel.Children.Add(gelButton2);
        }
      }
      return stackPanel;
    }

    // Token: 0x06000FE3 RID: 4067 RVA: 0x0057F0BC File Offset: 0x0057F0BC
    internal void ContinueFromWelcome()
    {
      this.TrackButton("continue", null);
      this.Go(IntroWindow.Step.WhereHeardAbout);
    }

    // Token: 0x06000FE4 RID: 4068 RVA: 0x0057F0D4 File Offset: 0x0057F0D4
    internal void SignInFromWelcome()
    {
      this.TrackButton("sign_in_with_google", null);
      PreSignInIntroViewModel vm = this.Vm;
      if (vm != null)
      {
        vm.ContinueFromWelcome();
        return;
      }
      this.actions.SignIn();
    }

    // Token: 0x06000FE5 RID: 4069 RVA: 0x0057F10C File Offset: 0x0057F10C
    private FrameworkElement WelcomeGlyph()
    {
      Color color = DS.Hex(2450411U, 1.0);
      Grid grid = new Grid();
      grid.Height = 58.0;
      grid.Width = 120.0;
      grid.HorizontalAlignment = HorizontalAlignment.Center;
      Path path = IntroMark.Create(38.0, color, 0.0);
      Grid grid2 = new Grid
      {
        Width = 38.0,
        Height = 38.0,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      };
      grid2.Children.Add(IntroWindow.ShapeGlow(path, color, 0.4, 36.0));
      grid2.Children.Add(IntroWindow.ShapeGlow(path, color, 0.65, 18.0));
      grid2.Children.Add(path);
      grid2.CenterPoint = new Vector3(19f, 19f, 0f);
      grid.Children.Add(grid2);
      this.welcomeMark = grid2;
      return grid;
    }

    // Token: 0x06000FE6 RID: 4070 RVA: 0x0057F224 File Offset: 0x0057F224
    private static Grid ShapeGlow(Shape shape, Color color, double alpha, double swiftRadius)
    {
      Grid grid = new Grid();
      grid.Width = shape.Width;
      grid.Height = shape.Height;
      grid.IsHitTestVisible = false;
      grid.HorizontalAlignment = HorizontalAlignment.Center;
      grid.VerticalAlignment = VerticalAlignment.Center;
      DropShadow dropShadow = Ui.Compositor.CreateDropShadow();
      dropShadow.Color = color;
      dropShadow.Opacity = (float)alpha;
      dropShadow.BlurRadius = (float)(swiftRadius * 2.0);
      dropShadow.Mask = shape.GetAlphaMask();
      SpriteVisual spriteVisual = Ui.Compositor.CreateSpriteVisual();
      spriteVisual.Size = new Vector2((float)shape.Width, (float)shape.Height);
      spriteVisual.Shadow = dropShadow;
      ElementCompositionPreview.SetElementChildVisual(grid, spriteVisual);
      return grid;
    }

    // Token: 0x06000FE7 RID: 4071 RVA: 0x0057F2D0 File Offset: 0x0057F2D0
    private void Shake()
    {
      if (this.welcomeMark == null || Anim.ReduceMotion || Environment.TickCount64 - this.lastShake < 450L)
      {
        return;
      }
      this.lastShake = Environment.TickCount64;
      Anim.KeyframesScalar(this.welcomeMark, "Rotation", new ValueTuple<float, double>[]
      {
        new ValueTuple<float, double>(0f, 0.04),
        new ValueTuple<float, double>(-12f, 0.07),
        new ValueTuple<float, double>(10f, 0.07),
        new ValueTuple<float, double>(-7f, 0.07),
        new ValueTuple<float, double>(4f, 0.07),
        new ValueTuple<float, double>(0f, 0.07)
      }, null);
    }

    // Token: 0x06000FE8 RID: 4072 RVA: 0x0057F3C0 File Offset: 0x0057F3C0
    private FrameworkElement Discovery()
    {
      CenteredWrapPanel centeredWrapPanel = new CenteredWrapPanel
      {
        HorizontalSpacing = 8.0,
        VerticalSpacing = 8.0
      };
      foreach (ValueTuple<string, string, string> valueTuple in IntroWindow.Channels)
      {
        string item = valueTuple.Item1;
        string item2 = valueTuple.Item2;
        string item3 = valueTuple.Item3;
        centeredWrapPanel.Children.Add(this.Chip(item, item2, item3));
      }
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 14.0,
        Children = { centeredWrapPanel }
      };
      if (this.otherPicked)
      {
        TextBox box = new TextBox
        {
          PlaceholderText = "Where'd you hear about us? (optional)",
          FontSize = 13.0,
          MaxLength = 280,
          MaxWidth = 320.0,
          Padding = new Thickness(14.0, 10.0, 14.0, 10.0),
          CornerRadius = new CornerRadius(12.0),
          HorizontalAlignment = HorizontalAlignment.Stretch,
          Background = Ui.Brush(DS.White(0.06)),
          BorderBrush = Ui.Brush(DS.White(0.16)),
          BorderThickness = new Thickness(0.5),
          Foreground = Ui.Brush(DS.Colors.TextPrimary),
          IsTextScaleFactorEnabled = false
        };
        box.Resources["TextControlBorderBrushFocused"] = Ui.Brush(DS.White(0.28));
        box.Resources["TextControlBackgroundFocused"] = Ui.Brush(DS.White(0.06));
        box.Resources["TextControlBackgroundPointerOver"] = Ui.Brush(DS.White(0.06));
        box.Resources["TextControlBorderThemeThicknessFocused"] = new Thickness(0.5);
        box.KeyDown += delegate(object _, KeyRoutedEventArgs e)
        {
          if (e.Key == VirtualKey.Enter)
          {
            this.CommitOther();
          }
        };
        box.Loaded += delegate(object _, RoutedEventArgs _)
        {
          box.Focus(FocusState.Programmatic);
        };
        this.otherBox = box;
        stackPanel.Children.Add(box);
      }
      return stackPanel;
    }

    // Token: 0x06000FE9 RID: 4073 RVA: 0x0057F64C File Offset: 0x0057F64C
    internal void CommitOther()
    {
      TextBox textBox = this.otherBox;
      string text = ((textBox != null) ? textBox.Text.Trim() : null) ?? "";
      PreSignInIntroViewModel vm = this.Vm;
      if (vm != null)
      {
        PreSignInIntroViewModel preSignInIntroViewModel = vm;
        TextBox textBox2 = this.otherBox;
        preSignInIntroViewModel.OtherDiscoveryText = ((textBox2 != null) ? textBox2.Text : null) ?? "";
        vm.CommitOtherDiscoveryChannelAsync();
      }
      else
      {
        this.s.Settings.DiscoveryChannel = "Other" + ((text.Length == 0) ? "" : (": " + text));
        ClickyAnalytics.TrackDiscoveryChannelSelected("other", (text.Length == 0) ? null : text);
      }
      this.TrackButton("submit_discovery_channel_other", (text.Length == 0) ? "skipped" : "filled");
      this.ApplyDefaultOnboardingLook();
      this.Go(this.StepAfterDiscovery);
    }

    // Token: 0x06000FEA RID: 4074 RVA: 0x0057F72C File Offset: 0x0057F72C
    private static async Task PersistDiscovery(PreSignInIntroViewModel vm, string id)
    {
      try
      {
        await vm.PickDiscoveryChannelAsync(id);
      }
      catch (Exception ex)
      {
        Log.Warn("intro", "discovery channel: " + ex.Message);
      }
    }

    // Token: 0x06000FEB RID: 4075 RVA: 0x0057F778 File Offset: 0x0057F778
    private FrameworkElement Chip(string label, [Nullable(2)] string logo, [Nullable(2)] string glyph)
    {
      IntroWindow.<>c__DisplayClass67_0 CS$<>8__locals1 = new IntroWindow.<>c__DisplayClass67_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.label = label;
      CS$<>8__locals1.picked = this.pickedChannel == CS$<>8__locals1.label;
      CS$<>8__locals1.p = new Pressable(true)
      {
        CornerRadius = new CornerRadius(100.0),
        Padding = new Thickness(13.0, 9.0, 13.0, 9.0),
        BorderThickness = new Thickness(0.5)
      };
      FrameworkElement frameworkElement;
      if (logo != null)
      {
        frameworkElement = new Image
        {
          Source = new SvgImageSource(new Uri(Path.Combine(AppContext.BaseDirectory, "Assets", "Logos", (CS$<>8__locals1.picked ? logo.Replace("-tinted", "") : logo) + ".svg")))
          {
            RasterizePixelWidth = 34.0,
            RasterizePixelHeight = 34.0
          },
          Width = 17.0,
          Height = 17.0,
          Stretch = Stretch.Uniform
        };
      }
      else
      {
        TextBlock textBlock = Icons.Glyph(glyph, 13.0, CS$<>8__locals1.picked ? DS.Black(0.7) : DS.Colors.TextSecondary);
        textBlock.FontWeight = FontWeights.SemiBold;
        frameworkElement = new Grid
        {
          Width = 17.0,
          Height = 17.0,
          Children = { textBlock }
        };
      }
      TextBlock textBlock2 = Ui.Medium(CS$<>8__locals1.label, 13.0, CS$<>8__locals1.picked ? DS.Black(0.9) : DS.Colors.TextPrimary);
      CS$<>8__locals1.p.Children.Add(Ui.H(7.0, new UIElement[] { frameworkElement, textBlock2 }));
      CS$<>8__locals1.<Chip>g__Paint|0(false);
      CS$<>8__locals1.p.HoverChanged += delegate(bool h)
      {
        base.<Chip>g__Paint|0(h);
        if (!CS$<>8__locals1.picked)
        {
          Anim.Scale(CS$<>8__locals1.p, h ? 1.05f : 1f, 0.14, Easing.EaseOut(Ui.Compositor), null);
        }
      };
      CS$<>8__locals1.p.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        CS$<>8__locals1.p.CenterPoint = new Vector3((float)CS$<>8__locals1.p.ActualWidth / 2f, (float)CS$<>8__locals1.p.ActualHeight / 2f, 0f);
      };
      CS$<>8__locals1.p.WithPressScale(0.97f, 0.14, false);
      CS$<>8__locals1.p.Click += delegate
      {
        CS$<>8__locals1.<>4__this.PickChannel(CS$<>8__locals1.label);
      };
      return CS$<>8__locals1.p;
    }

    // Token: 0x06000FEC RID: 4076 RVA: 0x0057F9E0 File Offset: 0x0057F9E0
    internal void PickChannel(string label)
    {
      Sounds.Play("skill-up", 0.3);
      this.pickedChannel = label;
      DiscoveryChannelOption discoveryChannelOption = OnboardingCatalog.DiscoveryChannels.FirstOrDefault<DiscoveryChannelOption>((DiscoveryChannelOption c) => c.Label == label);
      string text = ((discoveryChannelOption != null) ? discoveryChannelOption.Id : null) ?? "other";
      this.TrackButton("pick_discovery_channel", text);
      if (label == "Other")
      {
        this.otherPicked = true;
        PreSignInIntroViewModel vm = this.Vm;
        if (vm != null)
        {
          vm.PickDiscoveryChannelAsync("other");
        }
        this.Render(false);
        return;
      }
      this.otherPicked = false;
      PreSignInIntroViewModel vm2 = this.Vm;
      if (vm2 != null)
      {
        IntroWindow.PersistDiscovery(vm2, text);
      }
      else
      {
        this.s.Settings.DiscoveryChannel = label;
        ClickyAnalytics.TrackDiscoveryChannelSelected(text, null);
      }
      this.Render(false);
      this.After(0.14, delegate
      {
        this.ApplyDefaultOnboardingLook();
        this.Go(this.StepAfterDiscovery);
      });
    }

    // Token: 0x170002EC RID: 748
    // (get) Token: 0x06000FED RID: 4077 RVA: 0x0057FAEC File Offset: 0x0057FAEC
    private bool LanguageStepEnabled
    {
      get
      {
        PreSignInIntroViewModel vm = this.Vm;
        return vm != null && vm.IsLanguageStepEnabled;
      }
    }

    // Token: 0x170002ED RID: 749
    // (get) Token: 0x06000FEE RID: 4078 RVA: 0x0057FB00 File Offset: 0x0057FB00
    private IntroWindow.Step StepAfterDiscovery
    {
      get
      {
        if (!this.LanguageStepEnabled)
        {
          return IntroWindow.Step.OpenClicky;
        }
        return IntroWindow.Step.PickLanguage;
      }
    }

    // Token: 0x06000FEF RID: 4079 RVA: 0x0057FB10 File Offset: 0x0057FB10
    private static IReadOnlyList<string> SystemLanguageTags()
    {
      IReadOnlyList<string> readOnlyList;
      try
      {
        readOnlyList = GlobalizationPreferences.Languages.ToList<string>();
      }
      catch
      {
        readOnlyList = new <>z__ReadOnlySingleElementList<string>(CultureInfo.CurrentUICulture.Name);
      }
      return readOnlyList;
    }

    // Token: 0x06000FF0 RID: 4080 RVA: 0x0057FB50 File Offset: 0x0057FB50
    [NullableContext(2)]
    private static string DeviceRegion()
    {
      string text;
      try
      {
        text = GlobalizationPreferences.HomeGeographicRegion;
      }
      catch
      {
        text = RegionInfo.CurrentRegion.TwoLetterISORegionName;
      }
      return text;
    }

    // Token: 0x06000FF1 RID: 4081 RVA: 0x0057FB84 File Offset: 0x0057FB84
    private FrameworkElement LanguagePicker()
    {
      IntroWindow.<>c__DisplayClass82_0 CS$<>8__locals1 = new IntroWindow.<>c__DisplayClass82_0();
      CS$<>8__locals1.<>4__this = this;
      IReadOnlyList<string> readOnlyList = VoiceCatalog.SystemLanguageCodes(IntroWindow.SystemLanguageTags());
      if (this.suggestedLanguages == null)
      {
        this.suggestedLanguages = readOnlyList.Concat<string>(from code in VoiceCatalog.SystemLanguageCodes(KeyboardLayoutInfo.InstalledLayoutLanguageTags())
          where !VoiceCatalog.IsEnglish(code)
          select code).Distinct<string>().ToList<string>();
      }
      if (this.pickedLanguages == null)
      {
        PreSignInIntroViewModel vm = this.Vm;
        List<string> list = ((vm != null) ? vm.VoiceLanguages.ToList<string>() : null) ?? new List<string>();
        this.pickedLanguages = ((list.Count > 0) ? list : readOnlyList.Take<string>(1).ToList<string>());
      }
      if (!this.regionLanguagesRequested)
      {
        PreSignInIntroViewModel vm2 = this.Vm;
        if (vm2 != null)
        {
          this.regionLanguagesRequested = true;
          this.AddRegionLanguagesAsync(vm2);
        }
      }
      IntroWindow.<>c__DisplayClass82_0 CS$<>8__locals2 = CS$<>8__locals1;
      TextBox textBox = new TextBox();
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 1);
      defaultInterpolatedStringHandler.AppendLiteral("Search ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(VoiceCatalog.Languages.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" languages");
      textBox.PlaceholderText = defaultInterpolatedStringHandler.ToStringAndClear();
      textBox.Text = this.languageSearch;
      textBox.FontSize = 13.0;
      textBox.MaxWidth = 320.0;
      textBox.Padding = new Thickness(14.0, 10.0, 14.0, 10.0);
      textBox.CornerRadius = new CornerRadius(12.0);
      textBox.HorizontalAlignment = HorizontalAlignment.Stretch;
      textBox.Background = Ui.Brush(DS.White(0.06));
      textBox.BorderBrush = Ui.Brush(DS.White(0.16));
      textBox.BorderThickness = new Thickness(0.5);
      textBox.Foreground = Ui.Brush(DS.Colors.TextPrimary);
      textBox.IsTextScaleFactorEnabled = false;
      CS$<>8__locals2.box = textBox;
      CS$<>8__locals1.box.Resources["TextControlBorderBrushFocused"] = Ui.Brush(DS.White(0.28));
      CS$<>8__locals1.box.Resources["TextControlBackgroundFocused"] = Ui.Brush(DS.White(0.06));
      CS$<>8__locals1.box.Resources["TextControlBackgroundPointerOver"] = Ui.Brush(DS.White(0.06));
      CS$<>8__locals1.box.Resources["TextControlBorderThemeThicknessFocused"] = new Thickness(0.5);
      CS$<>8__locals1.box.TextChanged += delegate(object _, TextChangedEventArgs _)
      {
        CS$<>8__locals1.<>4__this.languageSearch = CS$<>8__locals1.box.Text;
        CS$<>8__locals1.<>4__this.FillLanguageChips();
      };
      this.languageSections = new StackPanel
      {
        Spacing = 14.0
      };
      this.languageNote = Ui.Text("", 12.0, FontWeights.Normal, DS.Colors.TextSecondary, TextAlignment.Center, 0, true, false);
      this.FillLanguageChips();
      return new StackPanel
      {
        Spacing = 14.0,
        Children = { CS$<>8__locals1.box, this.languageSections, this.languageNote }
      };
    }

    // Token: 0x06000FF2 RID: 4082 RVA: 0x0057FEC0 File Offset: 0x0057FEC0
    private async Task AddRegionLanguagesAsync(PreSignInIntroViewModel vm)
    {
      IReadOnlyList<string> readOnlyList = await vm.RegionLanguagesAsync(IntroWindow.DeviceRegion());
      IReadOnlyList<string> region = readOnlyList;
      this.dq.TryEnqueue(delegate
      {
        if (this.suggestedLanguages == null || region.Count == 0)
        {
          return;
        }
        int count = this.suggestedLanguages.Count;
        foreach (string text in region)
        {
          if (!this.suggestedLanguages.Contains(text) && this.suggestedLanguages.Count < 6)
          {
            this.suggestedLanguages.Add(text);
          }
        }
        if (this.suggestedLanguages.Count != count && this.step == IntroWindow.Step.PickLanguage)
        {
          this.FillLanguageChips();
        }
      });
    }

    // Token: 0x06000FF3 RID: 4083 RVA: 0x0057FF0C File Offset: 0x0057FF0C
    private void FillLanguageChips()
    {
      if (this.languageSections == null || this.pickedLanguages == null || this.suggestedLanguages == null)
      {
        return;
      }
      this.languageSections.Children.Clear();
      string text = this.languageSearch.Trim();
      int num;
      if (text.Length == 0)
      {
        List<VoiceLanguage> list = IntroWindow.<FillLanguageChips>g__Named|84_0(this.suggestedLanguages);
        List<VoiceLanguage> list2 = IntroWindow.<FillLanguageChips>g__Named|84_0(from c in VoiceCatalog.PopularCodes.Concat<string>(this.pickedLanguages.Where<string>((string c) => !VoiceCatalog.PopularCodes.Contains(c)))
          where !this.suggestedLanguages.Contains(c)
          select c);
        if (list.Count > 0)
        {
          this.languageSections.Children.Add(this.ChipSection("Suggested for you", list, false));
        }
        this.languageSections.Children.Add(this.ChipSection((list.Count > 0) ? "More languages" : null, list2, true));
        num = list.Count + list2.Count;
      }
      else
      {
        List<VoiceLanguage> list3 = VoiceCatalog.PickerList(text).Take<VoiceLanguage>(18).ToList<VoiceLanguage>();
        this.languageSections.Children.Add(this.ChipSection(null, list3, false));
        num = list3.Count;
      }
      if (this.languageNote != null)
      {
        TextBlock textBlock = this.languageNote;
        string text2;
        if (num != 0)
        {
          if (!this.languageLimitReached)
          {
            text2 = "";
          }
          else
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(46, 1);
            defaultInterpolatedStringHandler.AppendLiteral("Up to ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(5);
            defaultInterpolatedStringHandler.AppendLiteral(" languages. Take one off to add another.");
            text2 = defaultInterpolatedStringHandler.ToStringAndClear();
          }
        }
        else
        {
          text2 = "No languages match \"" + text + "\".";
        }
        textBlock.Text = text2;
        this.languageNote.Visibility = ((this.languageNote.Text.Length == 0) ? Visibility.Collapsed : Visibility.Visible);
      }
      this.Grow();
    }

    // Token: 0x06000FF4 RID: 4084 RVA: 0x005800D8 File Offset: 0x005800D8
    private FrameworkElement ChipSection([Nullable(2)] string title, IReadOnlyList<VoiceLanguage> languages, bool includesAutomatic)
    {
      CenteredWrapPanel centeredWrapPanel = new CenteredWrapPanel
      {
        HorizontalSpacing = 8.0,
        VerticalSpacing = 8.0
      };
      if (includesAutomatic)
      {
        centeredWrapPanel.Children.Add(IntroWindow.TextChip("Automatic", this.pickedLanguages.Count == 0, delegate
        {
          this.PickLanguage(null);
        }));
      }
      foreach (VoiceLanguage voiceLanguage in languages)
      {
        string code = voiceLanguage.Code;
        centeredWrapPanel.Children.Add(IntroWindow.TextChip(voiceLanguage.Name, this.pickedLanguages.Contains(code), delegate
        {
          this.PickLanguage(code);
        }));
      }
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 8.0
      };
      if (title != null)
      {
        TextBlock textBlock = Ui.Text(title.ToUpperInvariant(), 10.0, FontWeights.SemiBold, DS.Colors.TextTertiary, TextAlignment.Center, 0, false, false);
        textBlock.CharacterSpacing = 110;
        stackPanel.Children.Add(textBlock);
      }
      stackPanel.Children.Add(centeredWrapPanel);
      return stackPanel;
    }

    // Token: 0x06000FF5 RID: 4085 RVA: 0x00580220 File Offset: 0x00580220
    [NullableContext(2)]
    internal void PickLanguage(string code)
    {
      if (this.pickedLanguages == null)
      {
        this.pickedLanguages = new List<string>();
      }
      if (code == null)
      {
        this.pickedLanguages.Clear();
      }
      else if (!this.pickedLanguages.Remove(code))
      {
        if (this.pickedLanguages.Count >= 5)
        {
          this.languageLimitReached = true;
          this.FillLanguageChips();
          return;
        }
        this.pickedLanguages.Add(code);
      }
      this.languageLimitReached = false;
      Sounds.Play("skill-up", 0.3);
      this.TrackButton("pick_voice_language", code ?? "automatic");
      this.FillLanguageChips();
    }

    // Token: 0x06000FF6 RID: 4086 RVA: 0x005802BC File Offset: 0x005802BC
    internal void ContinueLanguage()
    {
      List<string> list = this.pickedLanguages ?? new List<string>();
      this.TrackButton("continue_voice_language", (list.Count == 0) ? "automatic" : string.Join(",", list));
      PreSignInIntroViewModel vm = this.Vm;
      if (vm != null)
      {
        vm.PickVoiceLanguages(list);
      }
      this.languageSearch = "";
      this.Go(IntroWindow.Step.OpenClicky);
    }

    // Token: 0x06000FF7 RID: 4087 RVA: 0x00580324 File Offset: 0x00580324
    private static FrameworkElement TextChip(string label, bool picked, Action onPick)
    {
      IntroWindow.<>c__DisplayClass88_0 CS$<>8__locals1 = new IntroWindow.<>c__DisplayClass88_0();
      CS$<>8__locals1.picked = picked;
      CS$<>8__locals1.p = new Pressable(true)
      {
        CornerRadius = new CornerRadius(100.0),
        Padding = new Thickness(13.0, 9.0, 13.0, 9.0),
        BorderThickness = new Thickness(0.5)
      };
      CS$<>8__locals1.p.Children.Add(Ui.Medium(label, 13.0, CS$<>8__locals1.picked ? DS.Black(0.9) : DS.Colors.TextPrimary));
      CS$<>8__locals1.<TextChip>g__Paint|0(false);
      CS$<>8__locals1.p.HoverChanged += delegate(bool h)
      {
        base.<TextChip>g__Paint|0(h);
        if (!CS$<>8__locals1.picked)
        {
          Anim.Scale(CS$<>8__locals1.p, h ? 1.05f : 1f, 0.14, Easing.EaseOut(Ui.Compositor), null);
        }
      };
      CS$<>8__locals1.p.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        CS$<>8__locals1.p.CenterPoint = new Vector3((float)CS$<>8__locals1.p.ActualWidth / 2f, (float)CS$<>8__locals1.p.ActualHeight / 2f, 0f);
      };
      CS$<>8__locals1.p.WithPressScale(0.97f, 0.14, false);
      CS$<>8__locals1.p.Click += onPick;
      return CS$<>8__locals1.p;
    }

    // Token: 0x06000FF8 RID: 4088 RVA: 0x00580440 File Offset: 0x00580440
    private void ApplyDefaultOnboardingLook()
    {
      PreSignInIntroViewModel vm = this.Vm;
      if (vm != null)
      {
        vm.ApplyDefaultOnboardingLook();
      }
      else
      {
        this.s.Settings.SquadPack = OnboardingCatalog.DefaultSquadPack.Id;
      }
      StarterCast.Outfit();
    }

    // Token: 0x06000FF9 RID: 4089 RVA: 0x00580480 File Offset: 0x00580480
    public static FrameworkElement GelCheckmark(double size)
    {
      Grid grid = new Grid();
      grid.Width = size;
      grid.Height = size;
      grid.Children.Add(new GelSurface(GelAppearance.PrimaryBlue, size / 2.0));
      TextBlock textBlock = Icons.Glyph("\ue73e", size * 0.45, Colors.Black);
      textBlock.FontWeight = FontWeights.Bold;
      grid.Children.Add(textBlock);
      return grid;
    }

    // Token: 0x06000FFA RID: 4090 RVA: 0x005804F0 File Offset: 0x005804F0
    private FrameworkElement ColorPicker()
    {
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 20.0,
        HorizontalAlignment = HorizontalAlignment.Center
      };
      List<FrameworkElement> dots = new List<FrameworkElement>();
      ValueTuple<string, Color>[] array = new ValueTuple<string, Color>[]
      {
        new ValueTuple<string, Color>("Red", DS.Colors.CursorRed),
        new ValueTuple<string, Color>("Blue", DS.Colors.CursorBlue),
        new ValueTuple<string, Color>("Yellow", DS.Colors.CursorYellow),
        new ValueTuple<string, Color>("Green", DS.Colors.CursorGreen)
      };
      for (int i = 0; i < array.Length; i++)
      {
        ValueTuple<string, Color> valueTuple = array[i];
        string name = valueTuple.Item1;
        Color color = valueTuple.Item2;
        Pressable p = new Pressable(true)
        {
          Width = 36.0,
          Height = 36.0,
          ShowsDefaultHover = false
        };
        if (DS.ToHex(color) == DS.ToHex(this.pickedColor))
        {
          bool flag = this.step != IntroWindow.Step.OpenClicky;
        }
        Ellipse ellipse = new Ellipse
        {
          Width = 26.0,
          Height = 26.0,
          Fill = Ui.Brush(color)
        };
        Grid restGlow = IntroWindow.ShapeGlow(ellipse, color, 0.35, 5.0);
        Grid pickedGlow = IntroWindow.ShapeGlow(ellipse, color, 0.65, 10.0);
        pickedGlow.Opacity = 0.0;
        p.Children.Add(restGlow);
        p.Children.Add(pickedGlow);
        p.Children.Add(ellipse);
        Ellipse ring = new Ellipse
        {
          Width = 32.0,
          Height = 32.0,
          Stroke = Ui.Brush(DS.White(0.95)),
          StrokeThickness = 1.5,
          Opacity = 0.0
        };
        p.Children.Add(ring);
        p.CenterPoint = new Vector3(18f, 18f, 0f);
        p.WithPressScale(0.97f, 0.14, false);
        p.Click += delegate
        {
          ring.Opacity = 1.0;
          Anim.Opacity(pickedGlow, 1f, 0.16, Easing.Expressive(Ui.Compositor), 0.0);
          Anim.Opacity(restGlow, 0f, 0.16, Easing.Expressive(Ui.Compositor), 0.0);
          Anim.Scale(p, 1.1f, 0.16, Easing.Expressive(Ui.Compositor), null);
          this.PickColor(name, color);
        };
        AutomationProperties.SetName(p, name);
        p.Opacity = 0.0;
        p.Translation = new Vector3(0f, 10f, 0f);
        p.Scale = new Vector3(0.78f, 0.78f, 1f);
        dots.Add(p);
        stackPanel.Children.Add(p);
      }
      this.After(Anim.ReduceMotion ? 0.02 : 0.12, delegate
      {
        for (int j = 0; j < dots.Count; j++)
        {
          IntroWindow.RevealDot(dots[j], j);
        }
      });
      return stackPanel;
    }

    // Token: 0x06000FFB RID: 4091 RVA: 0x005808AC File Offset: 0x005808AC
    private static void RevealDot(UIElement d, int index)
    {
      if (Anim.Instant || Anim.ReduceMotion)
      {
        d.Translation = Vector3.Zero;
        d.Scale = Vector3.One;
        Anim.Opacity(d, 1f, 0.12, Easing.EaseOut(Ui.Compositor), 0.0);
        return;
      }
      Compositor compositor = Ui.Compositor;
      LinearEasingFunction linearEasingFunction = compositor.CreateLinearEasingFunction();
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = compositor.CreateScalarKeyFrameAnimation();
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = compositor.CreateVector3KeyFrameAnimation();
      Vector3KeyFrameAnimation vector3KeyFrameAnimation2 = compositor.CreateVector3KeyFrameAnimation();
      for (int i = 1; i <= 36; i++)
      {
        float num = (float)i / 36f;
        float num2 = (float)Math.Clamp((IntroWindow.CubicBezier(0.23, 1.0, 0.32, 1.0, (double)num) - 0.11 * (double)index) / 0.56, 0.0, 1.0);
        scalarKeyFrameAnimation.InsertKeyFrame(num, num2, linearEasingFunction);
        vector3KeyFrameAnimation.InsertKeyFrame(num, new Vector3(0f, (1f - num2) * 10f, 0f), linearEasingFunction);
        float num3 = 0.78f + 0.22f * num2;
        vector3KeyFrameAnimation2.InsertKeyFrame(num, new Vector3(num3, num3, 1f), linearEasingFunction);
      }
      foreach (ValueTuple<KeyFrameAnimation, string> valueTuple in new ValueTuple<KeyFrameAnimation, string>[]
      {
        new ValueTuple<KeyFrameAnimation, string>(scalarKeyFrameAnimation, "Opacity"),
        new ValueTuple<KeyFrameAnimation, string>(vector3KeyFrameAnimation, "Translation"),
        new ValueTuple<KeyFrameAnimation, string>(vector3KeyFrameAnimation2, "Scale")
      })
      {
        KeyFrameAnimation item = valueTuple.Item1;
        string item2 = valueTuple.Item2;
        item.Duration = TimeSpan.FromSeconds(0.58);
        item.Target = item2;
        d.StartAnimation(item);
      }
    }

    // Token: 0x06000FFC RID: 4092 RVA: 0x00580A8C File Offset: 0x00580A8C
    private static double CubicBezier(double x1, double y1, double x2, double y2, double x)
    {
      double num = 0.0;
      double num2 = 1.0;
      double num3 = x;
      for (int i = 0; i < 40; i++)
      {
        num3 = (num + num2) / 2.0;
        if (IntroWindow.<CubicBezier>g__B|93_0(x1, x2, num3) < x)
        {
          num = num3;
        }
        else
        {
          num2 = num3;
        }
      }
      return IntroWindow.<CubicBezier>g__B|93_0(y1, y2, num3);
    }

    // Token: 0x06000FFD RID: 4093 RVA: 0x00580AE8 File Offset: 0x00580AE8
    internal void PickColor(string name, Color color)
    {
      this.pickedColor = color;
      PreSignInIntroViewModel vm = this.Vm;
      if (vm != null)
      {
        vm.PickCursorColorAsync(DS.ToHex(color));
      }
      else
      {
        this.s.Settings.CursorColorHex = DS.ToHex(color);
      }
      this.s.Cursor.SetColor(DS.ToHex(color));
      this.TrackButton("pick_color", name.ToLowerInvariant());
      this.After(0.12, delegate
      {
        this.Go(IntroWindow.Step.Hatched);
      });
    }

    // Token: 0x06000FFE RID: 4094 RVA: 0x00580B70 File Offset: 0x00580B70
    private FrameworkElement HatchGlyph()
    {
      Grid grid = new Grid
      {
        Width = 96.0,
        Height = 96.0,
        HorizontalAlignment = HorizontalAlignment.Center
      };
      this.hatchGlow = new Grid
      {
        Width = 88.0,
        Height = 88.0,
        Opacity = 0.0,
        IsHitTestVisible = false
      };
      this.hatchGlow.Children.Add(new ShadowLayer(44.0, Color.FromArgb((byte)Math.Round(140.25), this.pickedColor.R, this.pickedColor.G, this.pickedColor.B), 26.0, 0.0, 0.0));
      grid.Children.Add(this.hatchGlow);
      Grid grid2 = new Grid
      {
        Width = 44.0,
        Height = 44.0,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        Opacity = 0.0
      };
      Path path = IntroMark.Create(44.0, this.pickedColor, 0.0);
      grid2.Children.Add(IntroWindow.ShapeGlow(path, this.pickedColor, 0.35, 32.0));
      grid2.Children.Add(IntroWindow.ShapeGlow(path, this.pickedColor, 0.55, 16.0));
      grid2.Children.Add(path);
      grid2.CenterPoint = new Vector3(22f, 22f, 0f);
      grid2.Scale = new Vector3(0.3f, 0.3f, 1f);
      grid.Children.Add(grid2);
      this.hatchMark = grid2;
      this.hatchGlyphHost = grid;
      return grid;
    }

    // Token: 0x06000FFF RID: 4095 RVA: 0x00580D74 File Offset: 0x00580D74
    private void RunHatchTimeline()
    {
      IntroWindow.<>c__DisplayClass98_0 CS$<>8__locals1 = new IntroWindow.<>c__DisplayClass98_0();
      CS$<>8__locals1.<>4__this = this;
      Sounds.Stop("hatching");
      Sounds.Play("hatching", 0.85);
      if (this.hatchMark == null || this.hatchGlow == null)
      {
        return;
      }
      CS$<>8__locals1.c = Ui.Compositor;
      if (Anim.ReduceMotion)
      {
        Log.Info("intro", "hatch: reduced motion (a 0.6 s fade)");
        this.hatchMark.Scale = Vector3.One;
        this.hatchMark.Rotation = 0f;
        Anim.ReducedMotionFade(this.hatchGlow, 1f, 0.6);
        Anim.ReducedMotionFade(this.hatchMark, 1f, 0.6);
        this.After(0.7, delegate
        {
          if (CS$<>8__locals1.<>4__this.titleText != null)
          {
            Anim.ReducedMotionFade(CS$<>8__locals1.<>4__this.titleText, 1f, 0.18);
          }
        });
        this.After(0.73, delegate
        {
          if (CS$<>8__locals1.<>4__this.subtitleText != null)
          {
            Anim.ReducedMotionFade(CS$<>8__locals1.<>4__this.subtitleText, 1f, 0.18);
          }
        });
        this.After(0.76, delegate
        {
          if (CS$<>8__locals1.<>4__this.footerHost != null)
          {
            Anim.ReducedMotionFade(CS$<>8__locals1.<>4__this.footerHost, 1f, 0.18);
          }
        });
        return;
      }
      CS$<>8__locals1.inOut = Easing.EaseInOut(CS$<>8__locals1.c);
      Anim.Opacity(this.hatchGlow, 1f, 0.55, Easing.EaseOut(CS$<>8__locals1.c), 0.0);
      this.After(0.6, delegate
      {
        Anim.Opacity(CS$<>8__locals1.<>4__this.hatchMark, 1f, 0.3, Easing.EaseOut(CS$<>8__locals1.c), 0.0);
      });
      CS$<>8__locals1.<RunHatchTimeline>g__Pose|4(0.6, 0.5f, 14f, 0.35, IntroWindow.CompositionEasingFunctionKind.InOut);
      CS$<>8__locals1.<RunHatchTimeline>g__Pose|4(0.95, 0.34f, -12f, 0.35, IntroWindow.CompositionEasingFunctionKind.InOut);
      CS$<>8__locals1.<RunHatchTimeline>g__Pose|4(1.3, 0.55f, 16f, 0.35, IntroWindow.CompositionEasingFunctionKind.InOut);
      CS$<>8__locals1.<RunHatchTimeline>g__Pose|4(1.65, 0.32f, -10f, 0.35, IntroWindow.CompositionEasingFunctionKind.InOut);
      CS$<>8__locals1.<RunHatchTimeline>g__Pose|4(2.0, 0.22f, -6f, 0.4, IntroWindow.CompositionEasingFunctionKind.In);
      this.After(2.4, delegate
      {
        Anim.ScaleSpring(CS$<>8__locals1.<>4__this.hatchMark, 1f, new SwiftSpring(0.5, 0.55), null);
        Anim.Rotate(CS$<>8__locals1.<>4__this.hatchMark, 0f, 0.5, Easing.EaseOut(CS$<>8__locals1.c));
      });
      CS$<>8__locals1.reveal = Easing.Expressive(CS$<>8__locals1.c);
      this.After(3.1, delegate
      {
        if (CS$<>8__locals1.<>4__this.titleText != null)
        {
          Anim.Opacity(CS$<>8__locals1.<>4__this.titleText, 1f, 0.4, CS$<>8__locals1.reveal, 0.0);
        }
      });
      this.After(3.16, delegate
      {
        if (CS$<>8__locals1.<>4__this.subtitleText != null)
        {
          Anim.Opacity(CS$<>8__locals1.<>4__this.subtitleText, 1f, 0.4, CS$<>8__locals1.reveal, 0.0);
        }
      });
      this.After(3.22, delegate
      {
        if (CS$<>8__locals1.<>4__this.footerHost != null)
        {
          Anim.Opacity(CS$<>8__locals1.<>4__this.footerHost, 1f, 0.4, CS$<>8__locals1.reveal, 0.0);
        }
      });
    }

    // Token: 0x06001000 RID: 4096 RVA: 0x0058100C File Offset: 0x0058100C
    public void SettleHatched()
    {
      if (this.hatchMark == null || this.hatchGlow == null)
      {
        return;
      }
      this.hatchGlow.Opacity = 1.0;
      this.hatchMark.Opacity = 1.0;
      this.hatchMark.Scale = Vector3.One;
      this.hatchMark.Rotation = 0f;
      if (this.titleText != null)
      {
        this.titleText.Opacity = 1.0;
      }
      if (this.subtitleText != null)
      {
        this.subtitleText.Opacity = 1.0;
      }
      if (this.footerHost != null)
      {
        this.footerHost.Opacity = 1.0;
      }
    }

    // Token: 0x06001001 RID: 4097 RVA: 0x005810C8 File Offset: 0x005810C8
    internal void GiveHome()
    {
      this.TrackButton("give_clicky_a_home", null);
      Sounds.Play("clicky-question", 0.95);
      if (Anim.ReduceMotion)
      {
        Anim.Opacity(this.window.Root, 0f, 1.0, Easing.EaseOut(Ui.Compositor), 0.0);
        this.After(0.25, delegate
        {
          Action reducedMotionHandOff = this.ReducedMotionHandOff;
          if (reducedMotionHandOff == null)
          {
            return;
          }
          reducedMotionHandOff();
        });
        return;
      }
      if (this.hatchMark != null)
      {
        Anim.KeyframesScalar(this.hatchMark, "Rotation", new ValueTuple<float, double>[]
        {
          new ValueTuple<float, double>(0f, 0.04),
          new ValueTuple<float, double>(10f, 0.08),
          new ValueTuple<float, double>(-7f, 0.08),
          new ValueTuple<float, double>(0f, 0.1)
        }, null);
        if (!Anim.Instant)
        {
          Compositor compositor = Ui.Compositor;
          Vector3KeyFrameAnimation vector3KeyFrameAnimation = compositor.CreateVector3KeyFrameAnimation();
          double num = 0.3;
          double num2 = 0.0;
          foreach (ValueTuple<float, double> valueTuple in new ValueTuple<float, double>[]
          {
            new ValueTuple<float, double>(1f, 0.04),
            new ValueTuple<float, double>(1.18f, 0.1),
            new ValueTuple<float, double>(0.94f, 0.08),
            new ValueTuple<float, double>(1f, 0.08)
          })
          {
            float item = valueTuple.Item1;
            double item2 = valueTuple.Item2;
            num2 += item2;
            vector3KeyFrameAnimation.InsertKeyFrame((float)(num2 / num), new Vector3(item, item, 1f), Easing.EaseInOut(compositor));
          }
          vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(num);
          vector3KeyFrameAnimation.Target = "Scale";
          this.hatchMark.StartAnimation(vector3KeyFrameAnimation);
        }
      }
      Anim.Opacity(this.window.Root, 0f, 1.0, Easing.EaseOut(Ui.Compositor), 0.5);
      this.After(0.32, delegate
      {
        if (this.hatchGlyphHost != null)
        {
          Point point = this.hatchGlyphHost.TransformToVisual(null).TransformPoint(new Point(48f, 48f));
          double scale = this.window.Scale;
          Native.RECT rect;
          Native.GetWindowRectPublic(this.window.Hwnd, out rect);
          Action<IntroWindow.Point2> flightRequested = this.FlightRequested;
          if (flightRequested != null)
          {
            flightRequested(new IntroWindow.Point2((double)rect.Left + point.X * scale, (double)rect.Top + point.Y * scale));
          }
        }
        if (this.hatchMark != null)
        {
          Anim.Opacity(this.hatchMark, 0f, 0.18, null, 0.0);
        }
      });
    }

    // Token: 0x06001002 RID: 4098 RVA: 0x0058132C File Offset: 0x0058132C
    public void HandOffComplete()
    {
      this.window.HideWindow();
      this.window.Root.Opacity = 1.0;
      if (this.Vm == null)
      {
        this.s.Settings.HasCompletedIntro = true;
      }
      Action finished = this.Finished;
      if (finished == null)
      {
        return;
      }
      finished();
    }

    // Token: 0x06001003 RID: 4099 RVA: 0x00581388 File Offset: 0x00581388
    private void After(double seconds, Action a)
    {
      Microsoft.UI.Dispatching.DispatcherQueueTimer dispatcherQueueTimer = this.dq.CreateTimer().Rooted();
      dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(seconds);
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(Microsoft.UI.Dispatching.DispatcherQueueTimer _, object _)
      {
        a();
      };
      dispatcherQueueTimer.Start();
    }

    // Token: 0x0600100B RID: 4107 RVA: 0x005815BC File Offset: 0x005815BC
    [CompilerGenerated]
    internal static List<VoiceLanguage> <FillLanguageChips>g__Named|84_0(IEnumerable<string> codes)
    {
      Func<string, VoiceLanguage> func;
      if ((func = IntroWindow.<>O.<0>__Language) == null)
      {
        func = (IntroWindow.<>O.<0>__Language = new Func<string, VoiceLanguage>(VoiceCatalog.Language));
      }
      return codes.Select<string, VoiceLanguage>(func).OfType<VoiceLanguage>().ToList<VoiceLanguage>();
    }

    // Token: 0x0600100E RID: 4110 RVA: 0x0058160C File Offset: 0x0058160C
    [CompilerGenerated]
    internal static double <CubicBezier>g__B|93_0(double a, double b, double t)
    {
      return 3.0 * a * (1.0 - t) * (1.0 - t) * t + 3.0 * b * (1.0 - t) * t * t + t * t * t;
    }

    // Token: 0x040006CB RID: 1739
    private readonly AppServices s;

    // Token: 0x040006CC RID: 1740
    private readonly IAppActions actions;

    // Token: 0x040006CD RID: 1741
    private readonly PanelWindow window;

    // Token: 0x040006CE RID: 1742
    private readonly IntroStageLayout layout = new IntroStageLayout();

    // Token: 0x040006CF RID: 1743
    private readonly Microsoft.UI.Dispatching.DispatcherQueue dq;

    // Token: 0x040006D0 RID: 1744
    private IntroWindow.Step step;

    // Token: 0x040006D1 RID: 1745
    [Nullable(2)]
    private string pickedChannel;

    // Token: 0x040006D2 RID: 1746
    private bool otherPicked;

    // Token: 0x040006D3 RID: 1747
    private Color pickedColor = DS.Colors.CursorBlue;

    // Token: 0x040006D4 RID: 1748
    private double grownHeight;

    // Token: 0x040006D5 RID: 1749
    private bool? lastSignedIn;

    // Token: 0x040006D6 RID: 1750
    [Nullable(2)]
    private FrameworkElement hatchGlyphHost;

    // Token: 0x040006DA RID: 1754
    private static readonly SwiftSpring StepSpring = new SwiftSpring(0.38, 0.86);

    // Token: 0x040006DB RID: 1755
    private const double ContentWidth = 392.0;

    // Token: 0x040006DD RID: 1757
    [Nullable(2)]
    private TextBlock titleText;

    // Token: 0x040006DE RID: 1758
    [Nullable(2)]
    private TextBlock subtitleText;

    // Token: 0x040006DF RID: 1759
    [Nullable(2)]
    private FrameworkElement footerHost;

    // Token: 0x040006E0 RID: 1760
    [Nullable(2)]
    private FrameworkElement welcomeMark;

    // Token: 0x040006E1 RID: 1761
    private long lastShake;

    // Token: 0x040006E2 RID: 1762
    [TupleElementNames(new string[] { "label", "logo", "glyph" })]
    [Nullable(new byte[] { 1, 0, 1, 2, 2 })]
    private static readonly ValueTuple<string, string, string>[] Channels = new ValueTuple<string, string, string>[]
    {
      new ValueTuple<string, string, string>("Instagram Ad", "instagram-logo", null),
      new ValueTuple<string, string, string>("Instagram", "instagram-logo", null),
      new ValueTuple<string, string, string>("YouTube Ad", "youtube-logo", null),
      new ValueTuple<string, string, string>("YouTube", "youtube-logo", null),
      new ValueTuple<string, string, string>("TikTok", "tiktok-logo", null),
      new ValueTuple<string, string, string>("X/Twitter", "x-logo-tinted", null),
      new ValueTuple<string, string, string>("LinkedIn", "linkedin-logo", null),
      new ValueTuple<string, string, string>("Reddit", "reddit-logo", null),
      new ValueTuple<string, string, string>("ChatGPT", "chatgpt-logo-tinted", null),
      new ValueTuple<string, string, string>("Claude", "claude-logo", null),
      new ValueTuple<string, string, string>("Google", "google-logo", null),
      new ValueTuple<string, string, string>("A friend", null, "\ue716"),
      new ValueTuple<string, string, string>("Other", null, "\ue712")
    };

    // Token: 0x040006E3 RID: 1763
    [Nullable(2)]
    private TextBox otherBox;

    // Token: 0x040006E4 RID: 1764
    [Nullable(new byte[] { 2, 1 })]
    private List<string> pickedLanguages;

    // Token: 0x040006E5 RID: 1765
    [Nullable(new byte[] { 2, 1 })]
    private List<string> suggestedLanguages;

    // Token: 0x040006E6 RID: 1766
    private bool regionLanguagesRequested;

    // Token: 0x040006E7 RID: 1767
    private string languageSearch = "";

    // Token: 0x040006E8 RID: 1768
    private bool languageLimitReached;

    // Token: 0x040006E9 RID: 1769
    [Nullable(2)]
    private StackPanel languageSections;

    // Token: 0x040006EA RID: 1770
    [Nullable(2)]
    private TextBlock languageNote;

    // Token: 0x040006EB RID: 1771
    [Nullable(2)]
    private Grid hatchGlow;

    // Token: 0x040006EC RID: 1772
    [Nullable(2)]
    private FrameworkElement hatchMark;

    // Token: 0x02000647 RID: 1607
    [NullableContext(0)]
    public enum Step
    {
      // Token: 0x04001CE0 RID: 7392
      WhatIsClicky,
      // Token: 0x04001CE1 RID: 7393
      WhereHeardAbout,
      // Token: 0x04001CE2 RID: 7394
      PickLanguage,
      // Token: 0x04001CE3 RID: 7395
      OpenClicky,
      // Token: 0x04001CE4 RID: 7396
      Hatched
    }

    // Token: 0x02000648 RID: 1608
    [NullableContext(0)]
    public readonly struct Point2 : IEquatable<IntroWindow.Point2>
    {
      // Token: 0x06003188 RID: 12680 RVA: 0x0066E994 File Offset: 0x0066E994
      public Point2(double X, double Y)
      {
        this.X = X;
        this.Y = Y;
      }

      // Token: 0x17000786 RID: 1926
      // (get) Token: 0x06003189 RID: 12681 RVA: 0x0066E9A4 File Offset: 0x0066E9A4
      // (set) Token: 0x0600318A RID: 12682 RVA: 0x0066E9AC File Offset: 0x0066E9AC
      public double X { get; set; }

      // Token: 0x17000787 RID: 1927
      // (get) Token: 0x0600318B RID: 12683 RVA: 0x0066E9B8 File Offset: 0x0066E9B8
      // (set) Token: 0x0600318C RID: 12684 RVA: 0x0066E9C0 File Offset: 0x0066E9C0
      public double Y { get; set; }

      // Token: 0x0600318D RID: 12685 RVA: 0x0066E9CC File Offset: 0x0066E9CC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Point2");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600318E RID: 12686 RVA: 0x0066EA18 File Offset: 0x0066EA18
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("X = ");
        builder.Append(this.X.ToString());
        builder.Append(", Y = ");
        builder.Append(this.Y.ToString());
        return true;
      }

      // Token: 0x0600318F RID: 12687 RVA: 0x0066EA74 File Offset: 0x0066EA74
      [CompilerGenerated]
      public static bool operator !=(IntroWindow.Point2 left, IntroWindow.Point2 right)
      {
        return !(left == right);
      }

      // Token: 0x06003190 RID: 12688 RVA: 0x0066EA80 File Offset: 0x0066EA80
      [CompilerGenerated]
      public static bool operator ==(IntroWindow.Point2 left, IntroWindow.Point2 right)
      {
        return left.Equals(right);
      }

      // Token: 0x06003191 RID: 12689 RVA: 0x0066EA8C File Offset: 0x0066EA8C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<double>.Default.GetHashCode(this.<X>k__BackingField) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Y>k__BackingField);
      }

      // Token: 0x06003192 RID: 12690 RVA: 0x0066EAB8 File Offset: 0x0066EAB8
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is IntroWindow.Point2 && this.Equals((IntroWindow.Point2)obj);
      }

      // Token: 0x06003193 RID: 12691 RVA: 0x0066EAD0 File Offset: 0x0066EAD0
      [CompilerGenerated]
      public bool Equals(IntroWindow.Point2 other)
      {
        return EqualityComparer<double>.Default.Equals(this.<X>k__BackingField, other.<X>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Y>k__BackingField, other.<Y>k__BackingField);
      }

      // Token: 0x06003194 RID: 12692 RVA: 0x0066EB04 File Offset: 0x0066EB04
      [CompilerGenerated]
      public void Deconstruct(out double X, out double Y)
      {
        X = this.X;
        Y = this.Y;
      }
    }

    // Token: 0x02000649 RID: 1609
    [NullableContext(0)]
    private enum CompositionEasingFunctionKind
    {
      // Token: 0x04001CE8 RID: 7400
      InOut,
      // Token: 0x04001CE9 RID: 7401
      In
    }

    // Token: 0x0200064A RID: 1610
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001CEA RID: 7402
      [Nullable(0)]
      public static Func<string, VoiceLanguage> <0>__Language;
    }
  }
}
