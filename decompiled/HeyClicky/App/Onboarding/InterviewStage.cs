using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Portraits;
using HeyClicky.App.Services;
using HeyClicky.App.UI;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.System;
using Windows.UI;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x02000120 RID: 288
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class InterviewStage : Grid
  {
    // Token: 0x06000FB3 RID: 4019 RVA: 0x0057C770 File Offset: 0x0057C770
    public InterviewStage(AppServices services, InterviewController interview)
    {
      InterviewStage <>4__this = this;
      this.s = services;
      this.interview = interview;
      base.Width = 594.0;
      base.Height = 330.0;
      base.HorizontalAlignment = HorizontalAlignment.Center;
      base.Children.Add(this.focusSink);
      base.Children.Add(this.characterFrame);
      this.thoughts.HorizontalAlignment = HorizontalAlignment.Left;
      this.thoughts.VerticalAlignment = VerticalAlignment.Bottom;
      this.thoughts.Margin = new Thickness(194.0, 0.0, 0.0, 112.0);
      base.Children.Add(this.thoughts);
      this.cta.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(28.5),
        Margin = new Thickness(-2.5),
        BorderBrush = Ui.Brush(DS.White(0.35)),
        BorderThickness = new Thickness(1.0),
        IsHitTestVisible = false
      });
      this.cta.Children.Add(new ShadowLayer(26.0, DS.Black(0.18), 9.0, 5.0, 0.0));
      this.cta.Children.Add(new GelSurface(GelAppearance.PrimaryBlue, 26.0));
      this.cta.Children.Add(this.ctaContent);
      Pressable pressable = new Pressable(true)
      {
        Children = { this.cta }
      };
      pressable.Click += delegate
      {
        <>4__this.prefersVoice = true;
        <>4__this.DropFieldFocus();
        <>4__this.Update();
      };
      AutomationProperties.SetHelpText(pressable, "Switch to voice mode, then hold the keyboard shortcut to answer. Release to send.");
      this.fieldRim.Background = Ui.Vertical(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, DS.Hex(2302755U, 0.95)),
        new ValueTuple<double, Color>(1.0, DS.Hex(2302755U, 1.0))
      });
      this.field.Children.Add(this.fieldRim);
      TextBlock textBlock = Icons.Glyph("\ue765", 12.0, DS.Colors.TextTertiary.WithAlpha(0.6));
      textBlock.FontWeight = FontWeights.SemiBold;
      this.input = new TextBox
      {
        PlaceholderText = "Or type here",
        FontSize = 13.5,
        Background = Ui.Transparent,
        BorderThickness = new Thickness(0.0),
        VerticalAlignment = VerticalAlignment.Center,
        Foreground = Ui.Brush(DS.Hex(16053492U, 1.0)),
        IsTextScaleFactorEnabled = false
      };
      this.input.Resources["TextControlBackgroundFocused"] = Ui.Transparent;
      this.input.Resources["TextControlBackgroundPointerOver"] = Ui.Transparent;
      this.input.Resources["TextControlBorderThemeThicknessFocused"] = new Thickness(0.0);
      this.input.Resources["TextControlBackgroundDisabled"] = Ui.Transparent;
      this.input.Resources["TextControlPlaceholderForeground"] = Ui.Brush(DS.Colors.TextTertiary.WithAlpha(0.7));
      this.input.KeyDown += delegate(object _, KeyRoutedEventArgs e)
      {
        if (e.Key == VirtualKey.Enter)
        {
          <>4__this.Submit();
        }
      };
      this.input.TextChanged += delegate(object _, TextChangedEventArgs _)
      {
        <>4__this.Update();
      };
      Microsoft.UI.Dispatching.DispatcherQueueHandler <>9__8;
      this.input.GettingFocus += delegate(UIElement _, GettingFocusEventArgs e)
      {
        if (InterviewFieldFocusPolicy.Allows(e.FocusState == FocusState.Pointer || (e.InputDevice == FocusInputDeviceKind.Keyboard && e.Direction != FocusNavigationDirection.None), <>4__this.fieldFocusRequested))
        {
          return;
        }
        bool flag = e.TryCancel() || e.TrySetNewFocusedElement(<>4__this.focusSink);
        if (!flag)
        {
          <>4__this.unwantedFocusArriving = true;
          Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue = <>4__this.DispatcherQueue;
          Microsoft.UI.Dispatching.DispatcherQueueHandler dispatcherQueueHandler;
          if ((dispatcherQueueHandler = <>9__8) == null)
          {
            dispatcherQueueHandler = (<>9__8 = delegate
            {
              <>4__this.unwantedFocusArriving = false;
              <>4__this.DropFieldFocus();
            });
          }
          dispatcherQueue.TryEnqueue(dispatcherQueueHandler);
        }
        string text = "interview";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(54, 4);
        defaultInterpolatedStringHandler.AppendLiteral("typed field: a ");
        defaultInterpolatedStringHandler.AppendFormatted<FocusState>(e.FocusState);
        defaultInterpolatedStringHandler.AppendLiteral(" focus it didn't ask for refused (");
        defaultInterpolatedStringHandler.AppendFormatted<FocusInputDeviceKind>(e.InputDevice);
        defaultInterpolatedStringHandler.AppendLiteral(", ");
        defaultInterpolatedStringHandler.AppendFormatted<FocusNavigationDirection>(e.Direction);
        defaultInterpolatedStringHandler.AppendLiteral(", ");
        defaultInterpolatedStringHandler.AppendFormatted(flag ? "cancelled" : "handed back");
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      };
      this.input.GotFocus += delegate(object _, RoutedEventArgs _)
      {
        if (<>4__this.unwantedFocusArriving)
        {
          return;
        }
        <>4__this.focused = true;
        <>4__this.prefersVoice = false;
        <>4__this.Update();
      };
      this.input.LostFocus += delegate(object _, RoutedEventArgs _)
      {
        <>4__this.focused = false;
        <>4__this.Update();
      };
      this.send.Children.Add(new GelSurface(GelAppearance.PrimaryBlue, 13.0));
      TextBlock textBlock2 = Icons.Glyph("\ue74a", 11.0, Colors.Black);
      textBlock2.FontWeight = FontWeights.Bold;
      this.send.Children.Add(textBlock2);
      this.send.Click += this.Submit;
      AutomationProperties.SetName(this.send, "Send");
      Grid grid = new Grid
      {
        Margin = new Thickness(14.0, 6.0, 6.0, 6.0),
        ColumnSpacing = 8.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      Grid.SetColumn(this.input, 1);
      Grid.SetColumn(this.send, 2);
      grid.Children.Add(textBlock);
      grid.Children.Add(this.input);
      grid.Children.Add(this.send);
      this.field.Children.Add(grid);
      this.field.Tapped += delegate(object _, TappedRoutedEventArgs e)
      {
        if (<>4__this.input.IsEnabled)
        {
          if (<>4__this.send.IsHitTestVisible)
          {
            DependencyObject dependencyObject = e.OriginalSource as DependencyObject;
            if (dependencyObject != null && InterviewStage.IsInside(dependencyObject, <>4__this.send))
            {
              return;
            }
          }
          <>4__this.prefersVoice = false;
          if (!<>4__this.focused)
          {
            <>4__this.FocusField();
          }
          return;
        }
      };
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 16.0
      };
      stackPanel.Children.Add(pressable);
      stackPanel.Children.Add(this.field);
      this.controls.Children.Add(stackPanel);
      base.Children.Add(this.controls);
      base.Children.Add(this.hands);
      interview.Changed += this.Update;
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        interview.Changed -= <>4__this.Update;
      };
      this.Update();
    }

    // Token: 0x06000FB4 RID: 4020 RVA: 0x0057CFF8 File Offset: 0x0057CFF8
    private void FocusField()
    {
      this.fieldFocusRequested = true;
      try
      {
        this.input.Focus(FocusState.Programmatic);
      }
      finally
      {
        this.fieldFocusRequested = false;
      }
    }

    // Token: 0x06000FB5 RID: 4021 RVA: 0x0057D034 File Offset: 0x0057D034
    private void DropFieldFocus()
    {
      this.focusSink.Focus(FocusState.Programmatic);
    }

    // Token: 0x06000FB6 RID: 4022 RVA: 0x0057D044 File Offset: 0x0057D044
    private static bool IsInside(DependencyObject element, DependencyObject ancestor)
    {
      for (DependencyObject dependencyObject = element; dependencyObject != null; dependencyObject = VisualTreeHelper.GetParent(dependencyObject))
      {
        if (dependencyObject == ancestor)
        {
          return true;
        }
      }
      return false;
    }

    // Token: 0x06000FB7 RID: 4023 RVA: 0x0057D06C File Offset: 0x0057D06C
    private void Submit()
    {
      string text = this.input.Text.Trim();
      if (text.Length == 0 || !this.TypedActive)
      {
        return;
      }
      this.input.Text = "";
      this.interview.SubmitTyped(text);
    }

    // Token: 0x170002E3 RID: 739
    // (get) Token: 0x06000FB8 RID: 4024 RVA: 0x0057D0B8 File Offset: 0x0057D0B8
    private bool Completed
    {
      get
      {
        return this.interview.WasCompleted || (!this.interview.IsActive && this.interview.HasHeardFirstQuestion);
      }
    }

    // Token: 0x170002E4 RID: 740
    // (get) Token: 0x06000FB9 RID: 4025 RVA: 0x0057D0E4 File Offset: 0x0057D0E4
    private bool TypedActive
    {
      get
      {
        return this.interview.IsUsersTurn && this.s.Companion.State != VoiceState.Listening;
      }
    }

    // Token: 0x170002E5 RID: 741
    // (get) Token: 0x06000FBA RID: 4026 RVA: 0x0057D10C File Offset: 0x0057D10C
    private bool EditorExpanded
    {
      get
      {
        return !this.prefersVoice && (this.focused || this.input.Text.Trim().Length > 0);
      }
    }

    // Token: 0x170002E6 RID: 742
    // (get) Token: 0x06000FBB RID: 4027 RVA: 0x0057D13C File Offset: 0x0057D13C
    private InterviewStage.Stage StageState
    {
      get
      {
        if (!this.interview.IsActive && this.interview.HasHeardFirstQuestion)
        {
          return InterviewStage.Stage.Idle;
        }
        VoiceState voiceStateForStage = this.interview.VoiceStateForStage;
        if (voiceStateForStage == VoiceState.Listening)
        {
          return InterviewStage.Stage.Listening;
        }
        if (voiceStateForStage == VoiceState.Processing)
        {
          return InterviewStage.Stage.Thinking;
        }
        if (voiceStateForStage == VoiceState.Responding)
        {
          return InterviewStage.Stage.Speaking;
        }
        if (this.interview.IsWrappingUp || this.interview.IsPlanInFlight)
        {
          return InterviewStage.Stage.Thinking;
        }
        if (!this.interview.HasHeardFirstQuestion)
        {
          return InterviewStage.Stage.Preparing;
        }
        return InterviewStage.Stage.Idle;
      }
    }

    // Token: 0x06000FBC RID: 4028 RVA: 0x0057D1B0 File Offset: 0x0057D1B0
    public void Update()
    {
      this.EnsurePortrait();
      InterviewStage.Stage stageState = this.StageState;
      if (this.portrait != null)
      {
        PortraitView portraitView = this.portrait;
        PortraitExpression portraitExpression;
        switch (stageState)
        {
        case InterviewStage.Stage.Preparing:
          portraitExpression = PortraitExpression.Curious;
          goto IL_0053;
        case InterviewStage.Stage.Listening:
          portraitExpression = PortraitExpression.Listening;
          goto IL_0053;
        case InterviewStage.Stage.Thinking:
          portraitExpression = PortraitExpression.Thinking;
          goto IL_0053;
        case InterviewStage.Stage.Speaking:
          portraitExpression = PortraitExpression.Excited;
          goto IL_0053;
        }
        portraitExpression = PortraitExpression.Happy;
        IL_0053:
        portraitView.Expression = portraitExpression;
        if (stageState.ToString() != this.lastStageState)
        {
          this.lastStageState = stageState.ToString();
          ValueTuple<float, float> valueTuple;
          switch (stageState)
          {
          case InterviewStage.Stage.Listening:
            valueTuple = new ValueTuple<float, float>(1.06f, -6f);
            break;
          case InterviewStage.Stage.Thinking:
            valueTuple = new ValueTuple<float, float>(0.97f, 3f);
            break;
          case InterviewStage.Stage.Speaking:
            valueTuple = new ValueTuple<float, float>(1.03f, -3f);
            break;
          default:
            valueTuple = new ValueTuple<float, float>(1f, 0f);
            break;
          }
          ValueTuple<float, float> valueTuple2 = valueTuple;
          float item = valueTuple2.Item1;
          float item2 = valueTuple2.Item2;
          Anim.Scale(this.characterFrame, item, 0.3, Easing.EaseInOut(Ui.Compositor), null);
          Anim.Translate(this.characterFrame, new Vector3(0f, item2, 0f), 0.3, Easing.EaseInOut(Ui.Compositor));
        }
        if (stageState == InterviewStage.Stage.Speaking && !this.greeted && this.interview.IsActive)
        {
          this.greeted = true;
          this.portrait.Perform(PortraitReaction.Wave);
        }
      }
      this.UpdateBubbles(stageState);
      bool completed = this.Completed;
      this.controls.Visibility = (completed ? Visibility.Collapsed : Visibility.Visible);
      this.hands.Visibility = (completed ? Visibility.Collapsed : Visibility.Visible);
      bool editorExpanded = this.EditorExpanded;
      this.cta.Width = (double)(editorExpanded ? 52 : 330);
      this.field.Width = (double)(editorExpanded ? 478 : 200);
      bool typedActive = this.TypedActive;
      bool isEnabled = this.input.IsEnabled;
      if (!typedActive && isEnabled && this.focused)
      {
        this.refocusWhenEnabled = true;
      }
      this.input.IsEnabled = typedActive;
      if (typedActive && !isEnabled && this.refocusWhenEnabled)
      {
        this.refocusWhenEnabled = false;
        this.FocusField();
      }
      this.field.Opacity = (typedActive ? 1.0 : 0.62);
      if (!typedActive && this.focused)
      {
        this.DropFieldFocus();
      }
      this.fieldRim.BorderBrush = (this.focused ? Ui.Vertical(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, DS.Hex(15594751U, 1.0)),
        new ValueTuple<double, Color>(0.5, DS.Hex(10668031U, 1.0)),
        new ValueTuple<double, Color>(1.0, DS.Hex(10670839U, 1.0))
      }) : Ui.Brush(DS.Hex(4276545U, 1.0)));
      this.fieldRim.BorderThickness = new Thickness((double)(this.focused ? 2 : 1));
      bool flag = this.input.Text.Trim().Length > 0;
      this.send.Opacity = flag > false;
      this.send.IsHitTestVisible = flag;
      this.ctaContent.Children.Clear();
      ValueTuple<string, bool> valueTuple3 = this.Glyph(stageState);
      string item3 = valueTuple3.Item1;
      FrameworkElement frameworkElement = (valueTuple3.Item2 ? Icons.PathIcon(item3, 15.0, Colors.Black) : InterviewStage.Bold(Icons.Glyph(item3, 13.0, Colors.Black)));
      this.ctaContent.Children.Add(frameworkElement);
      if (!editorExpanded)
      {
        this.ctaContent.Children.Add(Ui.Embossed(Ui.Semibold(this.Title(stageState), 14.0, Colors.Black), 0.8, 1.5, 1.0));
      }
    }

    // Token: 0x06000FBD RID: 4029 RVA: 0x0057D5F4 File Offset: 0x0057D5F4
    private static TextBlock Bold(TextBlock t)
    {
      t.FontWeight = FontWeights.Bold;
      return t;
    }

    // Token: 0x06000FBE RID: 4030 RVA: 0x0057D604 File Offset: 0x0057D604
    private string Title(InterviewStage.Stage stage)
    {
      if (this.interview.HasHeardFirstQuestion && !this.interview.IsActive && !this.interview.IsPlanInFlight)
      {
        return "You're all set";
      }
      if (stage == InterviewStage.Stage.Listening)
      {
        return "Listening… release to send";
      }
      if (stage == InterviewStage.Stage.Speaking)
      {
        return "HeyClicky is speaking…";
      }
      if (this.interview.IsPlanInFlight)
      {
        return "Setting up your space…";
      }
      if (this.interview.IsWrappingUp)
      {
        return "Got it…";
      }
      if (stage == InterviewStage.Stage.Thinking)
      {
        return "Thinking…";
      }
      if (stage == InterviewStage.Stage.Preparing)
      {
        return "HeyClicky is getting ready…";
      }
      return "Hold " + this.s.Settings.PushToTalkDisplay + " to answer";
    }

    // Token: 0x06000FBF RID: 4031 RVA: 0x0057D6AC File Offset: 0x0057D6AC
    [return: Nullable(new byte[] { 0, 1 })]
    private ValueTuple<string, bool> Glyph(InterviewStage.Stage stage)
    {
      if (stage == InterviewStage.Stage.Listening)
      {
        return new ValueTuple<string, bool>("M3,10 h2 v4 h-2 z M7,7 h2 v10 h-2 z M11,3 h2 v18 h-2 z M15,6 h2 v12 h-2 z M19,9 h2 v6 h-2 z", true);
      }
      if (stage == InterviewStage.Stage.Speaking)
      {
        return new ValueTuple<string, bool>("\ue767", false);
      }
      if (this.interview.IsPlanInFlight)
      {
        return new ValueTuple<string, bool>("M10,3 C10.6,7.6 12.4,9.4 17,10 C12.4,10.6 10.6,12.4 10,17 C9.4,12.4 7.6,10.6 3,10 C7.6,9.4 9.4,7.6 10,3 Z M18,13 C18.3,15.2 18.8,15.7 21,16 C18.8,16.3 18.3,16.8 18,19 C17.7,16.8 17.2,16.3 15,16 C17.2,15.7 17.7,15.2 18,13 Z M17,2 C17.2,3.4 17.6,3.8 19,4 C17.6,4.2 17.2,4.6 17,6 C16.8,4.6 16.4,4.2 15,4 C16.4,3.8 16.8,3.4 17,2 Z", true);
      }
      if (this.interview.IsWrappingUp || (!this.interview.IsActive && this.interview.HasHeardFirstQuestion))
      {
        return new ValueTuple<string, bool>("\ue73e", false);
      }
      bool flag = stage == InterviewStage.Stage.Preparing || stage == InterviewStage.Stage.Thinking;
      if (flag)
      {
        return new ValueTuple<string, bool>("\ue712", false);
      }
      return new ValueTuple<string, bool>("\ue720", false);
    }

    // Token: 0x06000FC0 RID: 4032 RVA: 0x0057D74C File Offset: 0x0057D74C
    private void EnsurePortrait()
    {
      PortraitAppearance appearance = this.interview.Appearance;
      if (this.shownAppearance == appearance)
      {
        return;
      }
      this.shownAppearance = appearance;
      bool flag = appearance.Pack.IsCloud();
      double num = 195.0 * (flag ? 0.9 : 1.0);
      double num2 = 195.0 * (flag ? 0.74 : 1.0);
      double num3 = (flag ? (-52.0 + num2 * 0.22) : (-12.0));
      this.characterFrame.Children.Clear();
      this.characterFrame.Width = 195.0;
      this.characterFrame.Height = num2;
      this.characterFrame.HorizontalAlignment = HorizontalAlignment.Left;
      this.characterFrame.VerticalAlignment = VerticalAlignment.Bottom;
      this.characterFrame.Margin = new Thickness(0.0, 0.0, 0.0, -num3);
      this.characterFrame.CenterPoint = new Vector3(97.5f, (float)num2 / 2f, 0f);
      this.portrait = new PortraitView(appearance, num, num, PortraitExpression.Curious, false, new Color?(appearance.HairColor.Hair()), num * 0.08)
      {
        AnimatesWhileIdle = true,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        Margin = new Thickness(0.0, -(num - num2), 0.0, -(num - num2))
      };
      this.characterFrame.Children.Add(this.portrait);
      this.hands.Children.Clear();
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 56.0,
        HorizontalAlignment = HorizontalAlignment.Center
      };
      for (int i = 0; i < 2; i++)
      {
        FrameworkElement frameworkElement = InterviewStage.LedgeHand(appearance);
        frameworkElement.CenterPoint = new Vector3(14f, 11f, 0f);
        frameworkElement.Rotation = (float)((i == 0) ? (-8) : 8);
        stackPanel.Children.Add(frameworkElement);
      }
      this.hands.Children.Add(stackPanel);
    }

    // Token: 0x06000FC1 RID: 4033 RVA: 0x0057D99C File Offset: 0x0057D99C
    internal static FrameworkElement LedgeHand(PortraitAppearance a)
    {
      Grid grid = new Grid
      {
        Width = 28.0,
        Height = 22.0
      };
      if (a.Pack == PortraitPack.ClassicClouds)
      {
        Path path = new Path
        {
          Data = PortraitArt.LedgeHand(28.0, 22.0).ToXaml(),
          Fill = Ui.Brush(a.Hair)
        };
        WriteableBitmap writeableBitmap = PortraitWash.For(a, null);
        if (writeableBitmap != null)
        {
          path.Fill = new ImageBrush
          {
            ImageSource = writeableBitmap,
            Stretch = Stretch.Fill
          };
        }
        grid.Children.Add(path);
      }
      else if (a.Pack.IsCloud())
      {
        LinearGradientBrush linearGradientBrush = new LinearGradientBrush
        {
          StartPoint = new Point(0f, 0f),
          EndPoint = new Point(1f, 1f)
        };
        linearGradientBrush.GradientStops.Add(new GradientStop
        {
          Color = a.HairColor.CloudFill(0.87),
          Offset = 0.0
        });
        linearGradientBrush.GradientStops.Add(new GradientStop
        {
          Color = a.HairColor.CloudFill(0.55),
          Offset = 0.5
        });
        linearGradientBrush.GradientStops.Add(new GradientStop
        {
          Color = a.HairColor.CloudFill(0.12),
          Offset = 1.0
        });
        grid.Children.Add(new Path
        {
          Data = PortraitArt.LedgeHand(28.0, 22.0).ToXaml(),
          Fill = linearGradientBrush,
          Stroke = Ui.Brush(PortraitData.Ink),
          StrokeThickness = 1.2
        });
      }
      else
      {
        grid.Children.Add(new Border
        {
          CornerRadius = new CornerRadius(11.0),
          Background = Ui.Brush(a.Skin),
          BorderBrush = Ui.Brush(Colors.Black),
          BorderThickness = new Thickness(1.5)
        });
      }
      return grid;
    }

    // Token: 0x06000FC2 RID: 4034 RVA: 0x0057DBF0 File Offset: 0x0057DBF0
    private void UpdateBubbles(InterviewStage.Stage stage)
    {
      string spokenText = this.interview.SpokenText;
      bool flag = spokenText.Length == 0 && this.interview.IsActive && (stage == InterviewStage.Stage.Thinking || !this.interview.HasHeardFirstQuestion);
      this.thoughts.Show(spokenText, flag);
    }

    // Token: 0x040006A3 RID: 1699
    public const double StageWidth = 594.0;

    // Token: 0x040006A4 RID: 1700
    public const double StageHeight = 330.0;

    // Token: 0x040006A5 RID: 1701
    public const double CharacterSize = 195.0;

    // Token: 0x040006A6 RID: 1702
    private readonly AppServices s;

    // Token: 0x040006A7 RID: 1703
    private readonly InterviewController interview;

    // Token: 0x040006A8 RID: 1704
    private readonly Grid characterFrame = new Grid
    {
      IsHitTestVisible = false
    };

    // Token: 0x040006A9 RID: 1705
    [Nullable(2)]
    private PortraitView portrait;

    // Token: 0x040006AA RID: 1706
    [Nullable(2)]
    private PortraitAppearance shownAppearance;

    // Token: 0x040006AB RID: 1707
    private readonly InterviewThoughtBubbles thoughts = new InterviewThoughtBubbles(360.0, 216.0);

    // Token: 0x040006AC RID: 1708
    private readonly Grid controls = new Grid
    {
      Width = 546.0,
      Height = 52.0,
      HorizontalAlignment = HorizontalAlignment.Left,
      VerticalAlignment = VerticalAlignment.Bottom,
      Margin = new Thickness(24.0, 0.0, 0.0, 0.0)
    };

    // Token: 0x040006AD RID: 1709
    private readonly Grid cta = new Grid
    {
      Height = 52.0,
      HorizontalAlignment = HorizontalAlignment.Left
    };

    // Token: 0x040006AE RID: 1710
    private readonly StackPanel ctaContent = new StackPanel
    {
      Orientation = Orientation.Horizontal,
      Spacing = 8.0,
      HorizontalAlignment = HorizontalAlignment.Center,
      VerticalAlignment = VerticalAlignment.Center
    };

    // Token: 0x040006AF RID: 1711
    private readonly Grid field = new Grid
    {
      Height = 52.0,
      HorizontalAlignment = HorizontalAlignment.Left
    };

    // Token: 0x040006B0 RID: 1712
    private readonly Border fieldRim = new Border
    {
      CornerRadius = new CornerRadius(26.0),
      BorderThickness = new Thickness(1.0)
    };

    // Token: 0x040006B1 RID: 1713
    private readonly TextBox input;

    // Token: 0x040006B2 RID: 1714
    private readonly Pressable send = new Pressable(true)
    {
      Width = 26.0,
      Height = 26.0,
      VerticalAlignment = VerticalAlignment.Center
    };

    // Token: 0x040006B3 RID: 1715
    private readonly Grid hands = new Grid
    {
      Width = 195.0,
      Height = 22.0,
      HorizontalAlignment = HorizontalAlignment.Left,
      VerticalAlignment = VerticalAlignment.Bottom,
      Margin = new Thickness(0.0, 0.0, 0.0, 42.0),
      IsHitTestVisible = false
    };

    // Token: 0x040006B4 RID: 1716
    private bool prefersVoice;

    // Token: 0x040006B5 RID: 1717
    private bool focused;

    // Token: 0x040006B6 RID: 1718
    private bool greeted;

    // Token: 0x040006B7 RID: 1719
    private bool refocusWhenEnabled;

    // Token: 0x040006B8 RID: 1720
    private string lastStageState = "";

    // Token: 0x040006B9 RID: 1721
    private readonly ContentControl focusSink = new ContentControl
    {
      Width = 0.0,
      Height = 0.0,
      IsTabStop = true,
      TabIndex = int.MaxValue,
      UseSystemFocusVisuals = false,
      IsHitTestVisible = false,
      HorizontalAlignment = HorizontalAlignment.Left,
      VerticalAlignment = VerticalAlignment.Top
    };

    // Token: 0x040006BA RID: 1722
    private bool fieldFocusRequested;

    // Token: 0x040006BB RID: 1723
    private bool unwantedFocusArriving;

    // Token: 0x02000643 RID: 1603
    [NullableContext(0)]
    private enum Stage
    {
      // Token: 0x04001CC6 RID: 7366
      Preparing,
      // Token: 0x04001CC7 RID: 7367
      Idle,
      // Token: 0x04001CC8 RID: 7368
      Listening,
      // Token: 0x04001CC9 RID: 7369
      Thinking,
      // Token: 0x04001CCA RID: 7370
      Speaking
    }
  }
}
