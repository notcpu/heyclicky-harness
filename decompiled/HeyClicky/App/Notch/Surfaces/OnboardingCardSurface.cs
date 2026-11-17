using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using HeyClicky.Core.Onboarding;
using Microsoft.UI.Composition;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x02000163 RID: 355
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class OnboardingCardSurface : NotchSurface
  {
    // Token: 0x06001371 RID: 4977 RVA: 0x005A21F4 File Offset: 0x005A21F4
    public OnboardingCardSurface(int step)
    {
      this.step = step;
    }

    // Token: 0x1700038B RID: 907
    // (get) Token: 0x06001372 RID: 4978 RVA: 0x005A2228 File Offset: 0x005A2228
    public override FrameworkElement View
    {
      get
      {
        return this.view;
      }
    }

    // Token: 0x1700038C RID: 908
    // (get) Token: 0x06001373 RID: 4979 RVA: 0x005A2230 File Offset: 0x005A2230
    public override VerticalAlignment ContentAlignment
    {
      get
      {
        return VerticalAlignment.Stretch;
      }
    }

    // Token: 0x06001374 RID: 4980 RVA: 0x005A2234 File Offset: 0x005A2234
    public override bool CanUpdate(NotchPresentation p)
    {
      return p is NotchPresentation.OnboardingPinned;
    }

    // Token: 0x06001375 RID: 4981 RVA: 0x005A2240 File Offset: 0x005A2240
    public override void Update(NotchPresentation p)
    {
      NotchPresentation.OnboardingPinned onboardingPinned = p as NotchPresentation.OnboardingPinned;
      if (onboardingPinned != null && onboardingPinned.Step != this.step)
      {
        this.step = onboardingPinned.Step;
        this.Build(true);
      }
    }

    // Token: 0x06001376 RID: 4982 RVA: 0x005A2278 File Offset: 0x005A2278
    protected override void OnAttached()
    {
      base.Host.Services.Permissions.Changed += this.OnPermissions;
      base.Host.Services.Auth.SignInErrorChanged += this.OnSignInErrorChanged;
      this.Build(false);
    }

    // Token: 0x06001377 RID: 4983 RVA: 0x005A22D0 File Offset: 0x005A22D0
    public override void Detached()
    {
      base.Host.Services.Permissions.Changed -= this.OnPermissions;
      base.Host.Services.Auth.SignInErrorChanged -= this.OnSignInErrorChanged;
    }

    // Token: 0x06001378 RID: 4984 RVA: 0x005A2320 File Offset: 0x005A2320
    private void OnPermissions([Nullable(2)] object s, EventArgs e)
    {
      this.view.DispatcherQueue.TryEnqueue(delegate
      {
        this.Build(false);
      });
    }

    // Token: 0x06001379 RID: 4985 RVA: 0x005A2340 File Offset: 0x005A2340
    private void OnSignInErrorChanged([Nullable(2)] object s, EventArgs e)
    {
      this.view.DispatcherQueue.TryEnqueue(delegate
      {
        if (this.step == -1)
        {
          this.Build(false);
        }
      });
    }

    // Token: 0x0600137A RID: 4986 RVA: 0x005A2360 File Offset: 0x005A2360
    private void Build(bool swap)
    {
      FrameworkElement oldCard = this.shownCard;
      this.view.Children.Clear();
      FrameworkElement frameworkElement = (this.shownCard = this.Card());
      bool flag = this.step == -1;
      frameworkElement.VerticalAlignment = (flag ? VerticalAlignment.Center : VerticalAlignment.Top);
      frameworkElement.Margin = new Thickness(16.0, 12.0, 16.0, 12.0);
      if (swap && !Anim.ReduceMotion)
      {
        double num = Math.Max(this.view.ActualWidth, 300.0);
        CubicBezierEasingFunction cubicBezierEasingFunction = Easing.EaseInOut(Ui.Compositor);
        if (oldCard != null)
        {
          oldCard.IsHitTestVisible = false;
          this.view.Children.Add(oldCard);
          Anim.Opacity(oldCard, 0f, 0.22, cubicBezierEasingFunction, 0.0);
          Anim.Translate(oldCard, new Vector3((float)(-(float)num), 0f, 0f), 0.22, cubicBezierEasingFunction);
          Once.After(0.24, delegate
          {
            this.view.Children.Remove(oldCard);
          }, null);
        }
        frameworkElement.Opacity = 0.0;
        frameworkElement.Translation = new Vector3((float)num, 0f, 0f);
        Anim.Opacity(frameworkElement, 1f, 0.22, cubicBezierEasingFunction, 0.0);
        Anim.Translate(frameworkElement, Vector3.Zero, 0.22, cubicBezierEasingFunction);
      }
      else if (swap)
      {
        frameworkElement.Opacity = 0.0;
        Anim.Opacity(frameworkElement, 1f, 0.22, Easing.EaseInOut(Ui.Compositor), 0.0);
      }
      this.view.Children.Add(frameworkElement);
      if (this.step != 0)
      {
        this.view.Children.Add(this.ChromeRow());
      }
    }

    // Token: 0x0600137B RID: 4987 RVA: 0x005A2580 File Offset: 0x005A2580
    private FrameworkElement ChromeRow()
    {
      OnboardingCardSurface.<>c__DisplayClass22_0 CS$<>8__locals1 = new OnboardingCardSurface.<>c__DisplayClass22_0();
      CS$<>8__locals1.<>4__this = this;
      Grid grid = new Grid
      {
        Height = 22.0,
        Margin = new Thickness(8.0, 6.0, 12.0, 0.0),
        VerticalAlignment = VerticalAlignment.Top
      };
      CS$<>8__locals1.more = OnboardingCardSurface.ChromeButton("\ue712", "More", false);
      CS$<>8__locals1.flyout = new MenuFlyout();
      CS$<>8__locals1.<ChromeRow>g__Item|0("Check for Updates…", delegate
      {
        CS$<>8__locals1.<>4__this.Host.Actions.OpenUrl("https://heyclicky.com/download");
      });
      if (base.Host.Services.Auth.IsSignedIn)
      {
        CS$<>8__locals1.<ChromeRow>g__Item|0("Log Out", delegate
        {
          CS$<>8__locals1.<>4__this.Host.Services.Auth.SignOutAsync();
        });
      }
      CS$<>8__locals1.flyout.Items.Add(new MenuFlyoutSeparator());
      CS$<>8__locals1.<ChromeRow>g__Item|0("Quit HeyClicky", delegate
      {
        CS$<>8__locals1.<>4__this.Host.Actions.Quit();
      });
      CS$<>8__locals1.more.Click += delegate
      {
        CS$<>8__locals1.flyout.ShowAt(CS$<>8__locals1.more);
      };
      Pressable pressable = OnboardingCardSurface.ChromeButton("\ue711", "Close", true);
      pressable.Click += delegate
      {
        CS$<>8__locals1.<>4__this.Host.Actions.DismissOnboardingPin();
      };
      StackPanel stackPanel = Ui.H(4.0, new UIElement[] { CS$<>8__locals1.more, pressable });
      stackPanel.HorizontalAlignment = HorizontalAlignment.Right;
      grid.Children.Add(stackPanel);
      return grid;
    }

    // Token: 0x0600137C RID: 4988 RVA: 0x005A26E8 File Offset: 0x005A26E8
    private static Pressable ChromeButton(string glyph, string tip, bool pressFeedback = true)
    {
      Pressable pressable = new Pressable(true)
      {
        Width = 22.0,
        Height = 22.0
      };
      TextBlock g = Icons.Glyph(glyph, 10.0, DS.Colors.TextSecondary);
      g.FontWeight = FontWeights.SemiBold;
      pressable.Children.Add(g);
      pressable.WithIconHover(22.0);
      if (pressFeedback)
      {
        pressable.WithPressScale(0.96f, 0.12, true);
        pressable.PressedChanged += delegate(bool down)
        {
          Anim.Opacity(g, down ? 0.85f : 1f, 0.12, null, 0.0);
        };
      }
      return pressable.Tip(tip);
    }

    // Token: 0x0600137D RID: 4989 RVA: 0x005A27A0 File Offset: 0x005A27A0
    private FrameworkElement Card()
    {
      if (this.step != -1)
      {
        if (!base.Host.Services.Auth.IsSignedIn && this.step == 0)
        {
        }
        FrameworkElement frameworkElement;
        switch (this.step)
        {
        case 1:
          frameworkElement = this.PermissionCard(PermissionKind.Microphone, 1);
          break;
        case 2:
          frameworkElement = this.PermissionCard(PermissionKind.ScreenCapture, 2);
          break;
        case 3:
          frameworkElement = this.Simple("All set.", "Turn your sound on and tap the button below.", "Meet HeyClicky", delegate
          {
            base.Host.Actions.OnboardingCardAction(3);
          });
          break;
        default:
          if (this.PermissionsGranted)
          {
            frameworkElement = this.Simple("I live in your notch now.", "Turn your sound on and tap the button below.", "Meet HeyClicky", delegate
            {
              base.Host.Actions.OnboardingCardAction(0);
            });
          }
          else if (this.MicrophoneOff)
          {
            frameworkElement = this.MicrophoneOffCard();
          }
          else
          {
            frameworkElement = this.Simple("I live in your notch now.", "Need permissions to get started.", "Let's start", delegate
            {
              base.Host.Actions.OnboardingCardAction(0);
            });
          }
          break;
        }
        return frameworkElement;
      }
      return this.SignedOutPlaceholder();
    }

    // Token: 0x1700038D RID: 909
    // (get) Token: 0x0600137E RID: 4990 RVA: 0x005A289C File Offset: 0x005A289C
    private bool PermissionsGranted
    {
      get
      {
        return base.Host.Services.Permissions.GetStatus(PermissionKind.Microphone) == PermissionStatus.Granted && base.Host.Services.Permissions.GetStatus(PermissionKind.ScreenCapture) == PermissionStatus.Granted;
      }
    }

    // Token: 0x1700038E RID: 910
    // (get) Token: 0x0600137F RID: 4991 RVA: 0x005A28D4 File Offset: 0x005A28D4
    private bool MicrophoneOff
    {
      get
      {
        PermissionStatus status = base.Host.Services.Permissions.GetStatus(PermissionKind.Microphone);
        bool flag = status == PermissionStatus.WaitingInSettings || status == PermissionStatus.Denied;
        return flag && base.Host.Services.Permissions.GetStatus(PermissionKind.ScreenCapture) == PermissionStatus.Granted;
      }
    }

    // Token: 0x06001380 RID: 4992 RVA: 0x005A2924 File Offset: 0x005A2924
    private FrameworkElement MicrophoneOffCard()
    {
      ValueTuple<string, string, string> valueTuple = NotchOnboardingCardViewModel.MicrophoneOffCopy(base.Host.Services.Permissions.MicrophoneBlockedBy);
      string item = valueTuple.Item1;
      string item2 = valueTuple.Item2;
      string item3 = valueTuple.Item3;
      return this.Simple(item, item2, item3, delegate
      {
        base.Host.Actions.OnboardingCardAction(0);
      });
    }

    // Token: 0x06001381 RID: 4993 RVA: 0x005A2974 File Offset: 0x005A2974
    private FrameworkElement Simple(string title, string subtitle, string cta, Action action)
    {
      Border border = new Border
      {
        Width = 1.0,
        Height = 6.0
      };
      TextBlock textBlock = Ui.Text(title, 14.0, FontWeights.SemiBold, DS.Colors.TextPrimary, TextAlignment.Center, 0, true, false);
      TextBlock textBlock2 = Ui.Text(subtitle, 12.0, FontWeights.Normal, DS.Colors.TextSecondary, TextAlignment.Center, 0, true, false);
      GelButton gelButton = new GelButton(cta, GelAppearance.PrimaryBlue, GelButton.Sizing.Large, 100.0, null, null, null)
      {
        HorizontalAlignment = HorizontalAlignment.Center,
        Margin = new Thickness(0.0, 8.0, 0.0, 0.0)
      };
      gelButton.Click += action;
      return new StackPanel
      {
        Spacing = 10.0,
        Children = 
        {
          border,
          new StackPanel
          {
            Spacing = 6.0,
            Children = { textBlock, textBlock2 }
          },
          gelButton
        }
      };
    }

    // Token: 0x06001382 RID: 4994 RVA: 0x005A2AAC File Offset: 0x005A2AAC
    private FrameworkElement SignedOutPlaceholder()
    {
      TextBlock textBlock = Ui.Text("Sign in to use HeyClicky", 14.0, FontWeights.SemiBold, DS.Colors.TextPrimary, TextAlignment.Center, 0, true, false);
      string lastSignInErrorMessage = base.Host.Services.Auth.LastSignInErrorMessage;
      TextBlock textBlock2 = ((lastSignInErrorMessage != null) ? Ui.Text(lastSignInErrorMessage, 12.0, FontWeights.Normal, DS.Hex(16737129U, 1.0), TextAlignment.Center, 3, true, false) : Ui.Text("Sees your screen, talks back, runs your apps.", 12.0, FontWeights.Normal, DS.Colors.TextSecondary, TextAlignment.Center, 0, true, false));
      GelButton gelButton = new GelButton("Sign in with Google", GelAppearance.PrimaryBlue, GelButton.Sizing.Large, 100.0, null, null, null)
      {
        HorizontalAlignment = HorizontalAlignment.Center
      };
      gelButton.Click += delegate
      {
        base.Host.Actions.SignIn();
      };
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 14.0,
        Children = 
        {
          new StackPanel
          {
            Spacing = 6.0,
            Children = { textBlock, textBlock2 }
          },
          gelButton
        }
      };
      GelButton gelButton2 = DevSignIn.Button(base.Host.Actions, GelButton.Sizing.Compact);
      if (gelButton2 != null)
      {
        gelButton2.Margin = new Thickness(0.0, -6.0, 0.0, 0.0);
        stackPanel.Children.Add(gelButton2);
      }
      return stackPanel;
    }

    // Token: 0x06001383 RID: 4995 RVA: 0x005A2C38 File Offset: 0x005A2C38
    [return: TupleElementNames(new string[] { "title", "explanation", "button", "enabled", "success" })]
    [return: Nullable(new byte[] { 0, 1, 1, 1 })]
    public static ValueTuple<string, string, string, bool, bool> PermissionCopy(PermissionKind kind, PermissionStatus status)
    {
      ValueTuple<string, string, string, bool, bool> valueTuple;
      if (kind == PermissionKind.Microphone)
      {
        switch (status)
        {
        case PermissionStatus.Prompting:
          valueTuple = new ValueTuple<string, string, string, bool, bool>("This lets me hear your voice.", "Click Allow in the Windows prompt.", "Granting…", false, false);
          break;
        case PermissionStatus.WaitingInSettings:
        case PermissionStatus.WaitingForPicker:
        case PermissionStatus.Denied:
          valueTuple = new ValueTuple<string, string, string, bool, bool>("This lets me hear your voice.", "Turn on microphone access for HeyClicky, then come back.", "Open", true, false);
          break;
        case PermissionStatus.RestartRequired:
          valueTuple = new ValueTuple<string, string, string, bool, bool>("This lets me hear your voice.", "Already enabled in Settings? Reopen to apply it.", "Quit & Reopen HeyClicky", true, false);
          break;
        case PermissionStatus.Granted:
          valueTuple = new ValueTuple<string, string, string, bool, bool>("Got it!", "Microphone is ready.", "Granted", false, true);
          break;
        default:
          valueTuple = new ValueTuple<string, string, string, bool, bool>("This lets me hear your voice.", "Only while you’re holding the shortcut.", "Grant Microphone", true, false);
          break;
        }
        return valueTuple;
      }
      switch (status)
      {
      case PermissionStatus.Prompting:
        valueTuple = new ValueTuple<string, string, string, bool, bool>("I need screen share.", "Click Allow when Windows asks.", "Waiting…", false, false);
        break;
      case PermissionStatus.WaitingInSettings:
      case PermissionStatus.Denied:
        valueTuple = new ValueTuple<string, string, string, bool, bool>("I need screen share.", "Turn on screen capture for HeyClicky, then come back.", "Open", true, false);
        break;
      case PermissionStatus.WaitingForPicker:
        valueTuple = new ValueTuple<string, string, string, bool, bool>("I need screen share.", "Pick a display in the picker.", "Waiting…", false, false);
        break;
      case PermissionStatus.RestartRequired:
        valueTuple = new ValueTuple<string, string, string, bool, bool>("I need screen share.", "Already enabled in Settings? Reopen to apply it.", "Quit & Reopen HeyClicky", true, false);
        break;
      case PermissionStatus.Granted:
        valueTuple = new ValueTuple<string, string, string, bool, bool>("Got it!", "Screen capture is ready.", "Granted", false, true);
        break;
      default:
        valueTuple = new ValueTuple<string, string, string, bool, bool>("I need screen share.", "I only see your screen when you press the hotkey.", "Grant Screen Capture", true, false);
        break;
      }
      return valueTuple;
    }

    // Token: 0x06001384 RID: 4996 RVA: 0x005A2DA4 File Offset: 0x005A2DA4
    private FrameworkElement PermissionCard(PermissionKind kind, int progressIndex)
    {
      OnboardingCardSurface.<>c__DisplayClass33_0 CS$<>8__locals1 = new OnboardingCardSurface.<>c__DisplayClass33_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.kind = kind;
      CS$<>8__locals1.status = base.Host.Services.Permissions.GetStatus(CS$<>8__locals1.kind);
      ValueTuple<string, string, string, bool, bool> valueTuple = OnboardingCardSurface.PermissionCopy(CS$<>8__locals1.kind, CS$<>8__locals1.status);
      string item = valueTuple.Item1;
      string item2 = valueTuple.Item2;
      string item3 = valueTuple.Item3;
      bool item4 = valueTuple.Item4;
      bool item5 = valueTuple.Item5;
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 5.0,
        HorizontalAlignment = HorizontalAlignment.Center,
        Height = 6.0
      };
      for (int i = 0; i < 2; i++)
      {
        bool flag = i < progressIndex;
        stackPanel.Children.Add(new GelSurface(flag ? GelAppearance.PrimaryBlue : GelAppearance.SecondaryWhite, 3.0)
        {
          Width = 22.0,
          Height = 6.0,
          Opacity = (flag ? 1.0 : 0.3)
        });
      }
      DependencyObject dependencyObject = stackPanel;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 2);
      defaultInterpolatedStringHandler.AppendLiteral("Step ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(progressIndex);
      defaultInterpolatedStringHandler.AppendLiteral(" of ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(2);
      AutomationProperties.SetName(dependencyObject, defaultInterpolatedStringHandler.ToStringAndClear());
      TextBlock textBlock = Ui.Text(item, 14.0, FontWeights.SemiBold, DS.Colors.TextPrimary, TextAlignment.Center, 0, true, false);
      FrameworkElement frameworkElement = textBlock;
      if (item5)
      {
        TextBlock textBlock2 = Icons.Glyph("\uec61", 13.0, DS.Colors.Success);
        frameworkElement = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 6.0,
          HorizontalAlignment = HorizontalAlignment.Center,
          Children = { textBlock2, textBlock }
        };
      }
      TextBlock textBlock3 = Ui.Text(item2, 12.0, FontWeights.Normal, DS.Colors.TextSecondary, TextAlignment.Center, 0, true, false);
      FrameworkElement frameworkElement2;
      if (item5)
      {
        Grid grid = new Grid
        {
          HorizontalAlignment = HorizontalAlignment.Center
        };
        grid.Children.Add(new ShadowLayer(100.0, DS.Black(0.11), 5.0, 4.0, 0.0));
        grid.Children.Add(new GelSurface(GelAppearance.Mint, 100.0));
        TextBlock textBlock4 = Icons.Glyph("\ue73e", 13.0, DS.Black(0.85));
        textBlock4.FontWeight = FontWeights.Bold;
        Grid grid2 = Ui.Embossed(Ui.Text(item3, 14.0, FontWeights.SemiBold, DS.Black(0.85), TextAlignment.Left, 0, false, false), 0.8, 1.5, 1.0);
        grid.Children.Add(Ui.H(6.0, new UIElement[]
        {
          Ui.Embossed(textBlock4, 0.8, 1.5, 1.0),
          grid2
        }).Pad(22.0, 12.0, 22.0, 12.0));
        frameworkElement2 = grid;
      }
      else
      {
        GelButton gelButton = new GelButton(item3, GelAppearance.PrimaryBlue, GelButton.Sizing.Large, 100.0, (CS$<>8__locals1.status == PermissionStatus.RestartRequired) ? "\ue72c" : null, null, null);
        gelButton.HorizontalAlignment = HorizontalAlignment.Center;
        gelButton.IsEnabledGel = item4;
        gelButton.Click += delegate
        {
          OnboardingCardSurface.<>c__DisplayClass33_0.<<PermissionCard>b__0>d <<PermissionCard>b__0>d;
          <<PermissionCard>b__0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
          <<PermissionCard>b__0>d.<>4__this = CS$<>8__locals1;
          <<PermissionCard>b__0>d.<>1__state = -1;
          <<PermissionCard>b__0>d.<>t__builder.Start<OnboardingCardSurface.<>c__DisplayClass33_0.<<PermissionCard>b__0>d>(ref <<PermissionCard>b__0>d);
        };
        frameworkElement2 = gelButton;
      }
      frameworkElement2.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
      return new StackPanel
      {
        Spacing = 10.0,
        Children = 
        {
          stackPanel,
          new StackPanel
          {
            Spacing = 6.0,
            Children = { frameworkElement, textBlock3 }
          },
          frameworkElement2
        }
      };
    }

    // Token: 0x04000904 RID: 2308
    public const int Welcome = 0;

    // Token: 0x04000905 RID: 2309
    public const int Microphone = 1;

    // Token: 0x04000906 RID: 2310
    public const int ScreenCapture = 2;

    // Token: 0x04000907 RID: 2311
    public const int AllSet = 3;

    // Token: 0x04000908 RID: 2312
    public const int SignedOut = -1;

    // Token: 0x04000909 RID: 2313
    private const int TotalPermissionSteps = 2;

    // Token: 0x0400090A RID: 2314
    private readonly Grid view = new Grid();

    // Token: 0x0400090B RID: 2315
    private readonly ContentControl cardSlot = new ContentControl
    {
      HorizontalContentAlignment = HorizontalAlignment.Stretch,
      IsTabStop = false
    };

    // Token: 0x0400090C RID: 2316
    private int step;

    // Token: 0x0400090D RID: 2317
    [Nullable(2)]
    private FrameworkElement shownCard;
  }
}
