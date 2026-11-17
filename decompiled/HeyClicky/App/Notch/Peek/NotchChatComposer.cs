using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.Interop;
using HeyClicky.App.Notch.Surfaces;
using HeyClicky.App.Services;
using HeyClicky.App.UI;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Telemetry;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Input;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using WinRT.Interop;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x0200016F RID: 367
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchChatComposer : Grid
  {
    // Token: 0x06001441 RID: 5185 RVA: 0x005A9A20 File Offset: 0x005A9A20
    public static Grid GelSelection(double radius)
    {
      return new Grid
      {
        IsHitTestVisible = false,
        Visibility = Visibility.Collapsed,
        Children = 
        {
          new Border
          {
            CornerRadius = new CornerRadius(radius),
            BorderThickness = new Thickness(3.0),
            BorderBrush = Ui.Vertical(new ValueTuple<double, Color>[]
            {
              new ValueTuple<double, Color>(0.0, DS.Hex(15594751U, 1.0)),
              new ValueTuple<double, Color>(0.18, DS.Hex(14084095U, 1.0)),
              new ValueTuple<double, Color>(0.5, DS.Hex(10668031U, 1.0)),
              new ValueTuple<double, Color>(0.8, DS.Hex(10079224U, 1.0)),
              new ValueTuple<double, Color>(1.0, DS.Hex(10670839U, 1.0))
            })
          },
          new Border
          {
            CornerRadius = new CornerRadius(radius),
            BorderThickness = new Thickness(0.6),
            BorderBrush = Ui.Brush(DS.Hex(2234520U, 1.0))
          },
          new Border
          {
            Margin = new Thickness(2.4),
            CornerRadius = new CornerRadius(Math.Max(0.0, radius - 2.4)),
            BorderThickness = new Thickness(0.6),
            BorderBrush = Ui.Brush(DS.White(0.6))
          }
        }
      };
    }

    // Token: 0x14000058 RID: 88
    // (add) Token: 0x06001442 RID: 5186 RVA: 0x005A9C10 File Offset: 0x005A9C10
    // (remove) Token: 0x06001443 RID: 5187 RVA: 0x005A9C48 File Offset: 0x005A9C48
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<double> ExtraHeightChanged;

    // Token: 0x170003AC RID: 940
    // (get) Token: 0x06001444 RID: 5188 RVA: 0x005A9C80 File Offset: 0x005A9C80
    // (set) Token: 0x06001445 RID: 5189 RVA: 0x005A9C88 File Offset: 0x005A9C88
    public double ExtraHeight { get; private set; }

    // Token: 0x170003AD RID: 941
    // (get) Token: 0x06001446 RID: 5190 RVA: 0x005A9C94 File Offset: 0x005A9C94
    private bool IsFollowUp
    {
      get
      {
        return this.followUpEntryId != null || this.agentWithoutEvents != null;
      }
    }

    // Token: 0x06001447 RID: 5191 RVA: 0x005A9CAC File Offset: 0x005A9CAC
    public void RetargetFollowUp(string entryId)
    {
      if (this.IsFollowUp)
      {
        this.followUpEntryId = entryId;
      }
    }

    // Token: 0x06001448 RID: 5192 RVA: 0x005A9CC0 File Offset: 0x005A9CC0
    [NullableContext(2)]
    public NotchChatComposer([Nullable(1)] INotchHost host, string followUpEntryId = null, bool isInWindow = false, string placeholderText = null, ChatHandOff agentWithoutEvents = null)
    {
      NotchChatComposer.<>c__DisplayClass48_0 CS$<>8__locals1 = new NotchChatComposer.<>c__DisplayClass48_0();
      CS$<>8__locals1.host = host;
      base..ctor();
      CS$<>8__locals1.<>4__this = this;
      this.host = CS$<>8__locals1.host;
      this.followUpEntryId = followUpEntryId;
      this.isInWindow = isInWindow;
      this.agentWithoutEvents = agentWithoutEvents;
      this.presenter = CS$<>8__locals1.host.Services.Presenter as NotchPresenter;
      this.previewSize = (double)(isInWindow ? 52 : 40);
      if (isInWindow)
      {
        base.Padding = new Thickness(24.0, 10.0, 24.0, 12.0);
        base.MaxWidth = 728.0;
      }
      else
      {
        base.Padding = new Thickness(12.0, 10.0, 12.0, 12.0);
      }
      base.VerticalAlignment = VerticalAlignment.Bottom;
      Grid grid = new Grid
      {
        ColumnSpacing = 8.0,
        MinHeight = 42.0,
        VerticalAlignment = VerticalAlignment.Bottom
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      this.voicePlate = new Pressable(true)
      {
        Height = 42.0,
        VerticalAlignment = VerticalAlignment.Bottom
      };
      this.voicePlate.Children.Add(new GelSurface(GelAppearance.PrimaryBlue, 21.0));
      this.micGlyph = Icons.Glyph("\uec71", 13.0, DS.Black(0.85));
      this.micGlyph.FontWeight = FontWeights.SemiBold;
      this.micGlyph.HorizontalAlignment = HorizontalAlignment.Center;
      this.micGlyph.TextAlignment = TextAlignment.Center;
      this.voiceLabel = Ui.Text("", 13.0, FontWeights.SemiBold, DS.Black(0.85), TextAlignment.Left, 1, false, false);
      this.voiceLabel.VerticalAlignment = VerticalAlignment.Center;
      this.voiceLabel.Margin = new Thickness(8.0, 0.0, 0.0, 0.0);
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      };
      this.voiceIndicator.MinWidth = 14.0;
      this.voiceIndicator.HorizontalAlignment = HorizontalAlignment.Center;
      this.voiceIndicator.VerticalAlignment = VerticalAlignment.Center;
      this.micAtRest = Ui.Embossed(this.micGlyph, 0.8, 1.5, 1.0);
      this.voiceIndicator.Children.Add(this.micAtRest);
      stackPanel.Children.Add(this.voiceIndicator);
      stackPanel.Children.Add(Ui.Embossed(this.voiceLabel, 0.8, 1.5, 1.0));
      this.voicePlate.Children.Add(stackPanel);
      this.voicePlate.WithPressScale(0.97f, 0.14, false);
      this.voicePlate.Click += delegate
      {
        CS$<>8__locals1.<>4__this.SetFocused(false);
      };
      AutomationProperties.SetName(this.voicePlate, "Talk to HeyClicky");
      grid.Children.Add(this.voicePlate);
      double num = 21.0;
      this.textBoxPlate.CornerRadius = new CornerRadius(num);
      this.textBoxPlate.Background = Ui.Brush(this.PlateFill(false));
      this.textBoxStroke.CornerRadius = new CornerRadius(num);
      this.textBoxStroke.BorderThickness = new Thickness(1.0);
      this.textBoxStroke.BorderBrush = Ui.Brush(this.look.InkAt(0.22));
      this.selectionRing = NotchChatComposer.GelSelection(num);
      this.textBox.Children.Add(this.textBoxPlate);
      this.textBox.Children.Add(this.textBoxStroke);
      this.textBox.Children.Add(this.selectionRing);
      this.textBox.MinHeight = 42.0;
      this.textBox.Width = 150.0;
      this.textBox.VerticalAlignment = VerticalAlignment.Bottom;
      Grid.SetColumn(this.textBox, 1);
      grid.Children.Add(this.textBox);
      if (!isInWindow)
      {
        NotchPresenter notchPresenter = this.presenter;
        if (notchPresenter == null || notchPresenter.Tabs.Selected != NotchTab.Document)
        {
          grid.ColumnDefinitions.Add(new ColumnDefinition
          {
            Width = GridLength.Auto
          });
          Pressable pressable = NotchPeekSurface.ExpandToWindowButton(delegate
          {
            INotchActions notchActions = CS$<>8__locals1.host.Actions as INotchActions;
            if (notchActions == null)
            {
              return;
            }
            notchActions.ExpandNotchIntoWindow(CS$<>8__locals1.<>4__this.IsFollowUp ? "full_reply" : "chat");
          });
          pressable.VerticalAlignment = VerticalAlignment.Bottom;
          pressable.Margin = new Thickness(0.0, 0.0, 0.0, 5.0);
          Grid.SetColumn(pressable, 2);
          grid.Children.Add(pressable);
        }
      }
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = 6.0,
        Padding = new Thickness(16.0, 6.0, 16.0, 6.0),
        VerticalAlignment = VerticalAlignment.Center
      };
      this.previewRow = new ScrollViewer
      {
        Content = this.previews,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
        VerticalScrollBarVisibility = ScrollBarVisibility.Disabled,
        HorizontalScrollMode = ScrollMode.Enabled,
        VerticalScrollMode = ScrollMode.Disabled,
        Height = this.previewSize + 10.0,
        Visibility = Visibility.Collapsed
      };
      stackPanel2.Children.Add(this.previewRow);
      this.controls.MinHeight = 30.0;
      this.controls.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      this.controls.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      this.controls.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      this.controls.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      this.controls.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      double num2 = (double)(isInWindow ? 30 : 28);
      this.attach = new Pressable(true)
      {
        Width = num2,
        Height = num2,
        VerticalAlignment = VerticalAlignment.Center,
        Visibility = Visibility.Collapsed
      };
      Ellipse attachDisc = new Ellipse
      {
        Fill = Ui.Brush(this.look.InkAt(0.08))
      };
      this.attachGlyph = Icons.Glyph("\ue710", 13.0, this.look.InkAt(0.7));
      this.attachGlyph.FontWeight = FontWeights.SemiBold;
      this.attach.Children.Add(attachDisc);
      this.attach.Children.Add(this.attachGlyph);
      this.attach.HoverChanged += delegate(bool h)
      {
        bool flag = CS$<>8__locals1.<>4__this.attachments.Count >= 10;
        attachDisc.Fill = Ui.Brush(CS$<>8__locals1.<>4__this.look.InkAt((h && !flag) ? 0.14 : 0.08));
        CS$<>8__locals1.<>4__this.attachGlyph.Foreground = Ui.Brush(CS$<>8__locals1.<>4__this.look.InkAt(flag ? 0.3 : (h ? 0.95 : 0.7)));
      };
      this.attach.Click += this.PickFiles;
      this.attach.Tip("Attach files");
      AutomationProperties.SetName(this.attach, "Attach files");
      this.controls.Children.Add(this.attach);
      this.input = new TextBox
      {
        FontSize = 13.0,
        FontFamily = DS.Fonts.Text,
        Foreground = Ui.Brush(this.look.InkAt(0.92)),
        Background = Ui.Transparent,
        BorderThickness = new Thickness(0.0),
        Padding = new Thickness(0.0),
        MinHeight = 0.0,
        AcceptsReturn = true,
        TextWrapping = TextWrapping.Wrap,
        IsSpellCheckEnabled = false,
        IsTextScaleFactorEnabled = false,
        SelectionHighlightColor = Ui.Brush(DS.Colors.CursorBlue),
        VerticalAlignment = VerticalAlignment.Center,
        MaxHeight = ComposerBox.LineHeight * 4.0 + 2.0
      };
      ComposerBox.StripChrome(this.input);
      if (!this.look.IsDark)
      {
        foreach (string text in new string[] { "TextControlForeground", "TextControlForegroundPointerOver", "TextControlForegroundFocused" })
        {
          this.input.Resources[text] = Ui.Brush(this.look.InkAt(0.92));
        }
      }
      ScrollViewer.SetVerticalScrollBarVisibility(this.input, ScrollBarVisibility.Auto);
      AutomationProperties.SetName(this.input, ((placeholderText != null) ? placeholderText.TrimEnd('…') : null) ?? "Ask HeyClicky");
      this.placeholder = Ui.Text(placeholderText ?? (this.IsFollowUp ? "Follow up…" : "Ask HeyClicky…"), 13.0, FontWeights.Normal, this.look.InkAt(0.45), TextAlignment.Left, 1, false, false);
      this.placeholder.IsHitTestVisible = false;
      this.placeholder.VerticalAlignment = VerticalAlignment.Center;
      this.dropLabel = Ui.Text("Drop to attach", 13.0, FontWeights.Medium, this.look.InkAt(0.8), TextAlignment.Left, 1, false, false);
      this.dropLabel.IsHitTestVisible = false;
      this.dropLabel.VerticalAlignment = VerticalAlignment.Center;
      this.dropLabel.Opacity = 0.0;
      Grid grid2 = new Grid
      {
        VerticalAlignment = VerticalAlignment.Center,
        Children = { this.input, this.placeholder, this.dropLabel }
      };
      Grid.SetColumn(grid2, 1);
      this.controls.Children.Add(grid2);
      this.readAloud = new ReadAloudSwitch(CS$<>8__locals1.host.Services, 28.0, false)
      {
        Visibility = Visibility.Collapsed,
        VerticalAlignment = VerticalAlignment.Center
      };
      Grid.SetColumn(this.readAloud, 2);
      this.controls.Children.Add(this.readAloud);
      this.readAloud.Toggled += delegate(bool on)
      {
        if (!CS$<>8__locals1.<>4__this.isTyping)
        {
          return;
        }
        ClickyAnalytics.TrackProductEvent("notch_chat_action", null, null, NotchChatAnalytics.ReadAloudToggled(on, CS$<>8__locals1.<>4__this.CanSend));
      };
      this.send = new Pressable(true)
      {
        Width = 30.0,
        Height = 30.0,
        VerticalAlignment = VerticalAlignment.Center,
        Visibility = Visibility.Collapsed
      };
      this.sendGel = new GelSurface(GelAppearance.PrimaryBlue, 15.0);
      this.sendGlyph = Icons.Glyph("\ue74a", 13.0, DS.Black(0.85));
      this.sendGlyph.FontWeight = FontWeights.Bold;
      this.send.Children.Add(this.sendGel);
      this.send.Children.Add(this.sendGlyph);
      this.send.HoverChanged += delegate(bool h)
      {
        CS$<>8__locals1.<>4__this.sendGel.Hovered = h;
      };
      this.send.Click += this.Send;
      this.send.Tip("Send");
      Grid.SetColumn(this.send, 3);
      this.controls.Children.Add(this.send);
      stackPanel2.Children.Add(this.controls);
      this.textBox.Children.Add(stackPanel2);
      base.Children.Add(grid);
      this.capNotice = new Border
      {
        Height = 24.0,
        Padding = new Thickness(10.0, 0.0, 10.0, 0.0),
        CornerRadius = new CornerRadius(12.0),
        Background = Ui.Brush(DS.Black(0.75)),
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Top,
        Opacity = 0.0,
        IsHitTestVisible = false,
        Margin = new Thickness(0.0, -10.0, 0.0, 0.0),
        Child = Ui.Text(ComposerAttachmentPolicy.CapNotice, 11.0, FontWeights.Medium, DS.White(0.85), TextAlignment.Left, 0, false, false).Center<TextBlock>()
      };
      base.Children.Add(this.capNotice);
      this.textBox.PointerPressed += delegate(object _, PointerRoutedEventArgs _)
      {
        CS$<>8__locals1.<>4__this.FocusField();
      };
      this.input.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(delegate(object _, PointerRoutedEventArgs _)
      {
        CS$<>8__locals1.<>4__this.FocusField();
      }), true);
      this.input.GotFocus += delegate(object _, RoutedEventArgs _)
      {
        CS$<>8__locals1.<>4__this.SetFocused(true);
        FocusState focusState = CS$<>8__locals1.<>4__this.input.FocusState;
        bool flag2 = focusState - FocusState.Pointer <= 1;
        if (flag2)
        {
          CS$<>8__locals1.<>4__this.ClaimKeyboard("focus", false);
        }
      };
      this.input.LostFocus += delegate(object _, RoutedEventArgs _)
      {
        CS$<>8__locals1.<>4__this.SetFocused(false);
      };
      this.input.TextChanged += delegate(object _, TextChangedEventArgs _)
      {
        CS$<>8__locals1.<>4__this.placeholder.Visibility = ((CS$<>8__locals1.<>4__this.input.Text.Length == 0 && !CS$<>8__locals1.<>4__this.isDropTargeting) ? Visibility.Visible : Visibility.Collapsed);
        CS$<>8__locals1.<>4__this.Changed();
      };
      this.input.PreviewKeyDown += this.OnKey;
      this.input.Paste += this.OnPaste;
      base.SizeChanged += delegate(object _, SizeChangedEventArgs e)
      {
        double num3 = Math.Max(0.0, e.NewSize.Height - 64.0);
        if (Math.Abs(num3 - CS$<>8__locals1.<>4__this.ExtraHeight) > 0.5)
        {
          CS$<>8__locals1.<>4__this.ExtraHeight = num3;
          Action<double> extraHeightChanged = CS$<>8__locals1.<>4__this.ExtraHeightChanged;
          if (extraHeightChanged == null)
          {
            return;
          }
          extraHeightChanged(num3);
        }
      };
      this.RefreshVoiceLabel();
      this.Apply(false);
      if (!this.IsFollowUp && !isInWindow)
      {
        NotchChatComposer.EveryDraftCleared += this.ClearDraft;
      }
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        CS$<>8__locals1.host.KeyboardChanged += CS$<>8__locals1.<>4__this.OnKeyboardChanged;
        CS$<>8__locals1.host.Services.Companion.StateChanged += CS$<>8__locals1.<>4__this.OnVoiceStateChanged;
        CS$<>8__locals1.<>4__this.RefreshVoiceLabel();
      };
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        CS$<>8__locals1.host.KeyboardChanged -= CS$<>8__locals1.<>4__this.OnKeyboardChanged;
        CS$<>8__locals1.host.Services.Companion.StateChanged -= CS$<>8__locals1.<>4__this.OnVoiceStateChanged;
        if (CS$<>8__locals1.<>4__this.IsFollowUp)
        {
          CS$<>8__locals1.<>4__this.Detached();
        }
      };
    }

    // Token: 0x06001449 RID: 5193 RVA: 0x005AAC18 File Offset: 0x005AAC18
    private void OnKeyboardChanged(bool hasKeyboard)
    {
      if (!hasKeyboard && this.isFocused)
      {
        this.SetFocused(false);
      }
    }

    // Token: 0x170003AE RID: 942
    // (get) Token: 0x0600144A RID: 5194 RVA: 0x005AAC2C File Offset: 0x005AAC2C
    public bool HoldsKeyboard
    {
      get
      {
        return this.isFocused;
      }
    }

    // Token: 0x0600144B RID: 5195 RVA: 0x005AAC34 File Offset: 0x005AAC34
    private void OnVoiceStateChanged([Nullable(2)] object sender, EventArgs e)
    {
      base.DispatcherQueue.TryEnqueue(new Microsoft.UI.Dispatching.DispatcherQueueHandler(this.RefreshVoiceLabel));
    }

    // Token: 0x0600144C RID: 5196 RVA: 0x005AAC50 File Offset: 0x005AAC50
    private void ShowVoiceIndicator(VoiceState state)
    {
      if (state == this.shownVoiceState)
      {
        return;
      }
      this.shownVoiceState = state;
      this.voiceIndicator.Children.Clear();
      Color color = DS.Black(0.65);
      UIElementCollection children = this.voiceIndicator.Children;
      FrameworkElement frameworkElement;
      switch (state)
      {
      case VoiceState.Listening:
        frameworkElement = ActivityIndicators.ListeningBars(color).Item1;
        break;
      case VoiceState.Processing:
        frameworkElement = ActivityIndicators.ThinkingDots(color, 4.0, 3.0, 2.8, 0.85, 0.55, 0.45, 0.4, 0.6, 14.0);
        break;
      case VoiceState.Responding:
        frameworkElement = ActivityIndicators.SpeakingBars(color);
        break;
      default:
        frameworkElement = this.micAtRest;
        break;
      }
      children.Add(frameworkElement);
    }

    // Token: 0x0600144D RID: 5197 RVA: 0x005AAD30 File Offset: 0x005AAD30
    private void RefreshVoiceLabel()
    {
      string pushToTalkDisplay = this.host.Services.Settings.PushToTalkDisplay;
      VoiceState state = this.host.Services.Companion.State;
      TextBlock textBlock = this.voiceLabel;
      string text;
      switch (state)
      {
      case VoiceState.Listening:
        text = "Listening… release to send";
        break;
      case VoiceState.Processing:
        text = "Thinking…";
        break;
      case VoiceState.Responding:
        text = "Speaking…";
        break;
      default:
        text = "Hold " + pushToTalkDisplay + " to talk";
        break;
      }
      textBlock.Text = text;
      this.ShowVoiceIndicator(state);
      this.voicePlate.Tip(this.IsFollowUp ? ("Hold " + pushToTalkDisplay + " to follow up") : ("Hold " + pushToTalkDisplay + " to talk to HeyClicky"));
      AutomationProperties.SetHelpText(this.voicePlate, "Hold " + pushToTalkDisplay + " to talk. Release to send.");
    }

    // Token: 0x170003AF RID: 943
    // (get) Token: 0x0600144E RID: 5198 RVA: 0x005AAE10 File Offset: 0x005AAE10
    private bool CanSend
    {
      get
      {
        return this.input.Text.Trim().Length > 0 || this.attachments.Count > 0;
      }
    }

    // Token: 0x0600144F RID: 5199 RVA: 0x005AAE3C File Offset: 0x005AAE3C
    [NullableContext(2)]
    public void SetStopAction(Action stop)
    {
      this.stopAction = stop;
      if (stop == null)
      {
        if (this.stopButton != null)
        {
          this.stopButton.Visibility = Visibility.Collapsed;
        }
        return;
      }
      if (this.stopButton == null)
      {
        Pressable pressable = new Pressable(true)
        {
          Width = 30.0,
          Height = 30.0,
          VerticalAlignment = VerticalAlignment.Center
        };
        GelSurface surface = new GelSurface(GelAppearance.SecondaryWhite, 15.0);
        pressable.Children.Add(surface);
        Grid grid = new Grid
        {
          HorizontalAlignment = HorizontalAlignment.Center,
          VerticalAlignment = VerticalAlignment.Center,
          IsHitTestVisible = false
        };
        grid.Children.Add(new Rectangle
        {
          Width = 9.0,
          Height = 9.0,
          RadiusX = 1.5,
          RadiusY = 1.5,
          Fill = Ui.Brush(DS.White(0.8)),
          RenderTransform = new TranslateTransform
          {
            Y = 1.0
          }
        });
        grid.Children.Add(new Rectangle
        {
          Width = 9.0,
          Height = 9.0,
          RadiusX = 1.5,
          RadiusY = 1.5,
          Fill = Ui.Brush(DS.Black(0.85))
        });
        pressable.Children.Add(grid);
        pressable.HoverChanged += delegate(bool h)
        {
          surface.Hovered = h;
        };
        pressable.WithPressScale(0.97f, 0.14, false);
        pressable.Click += delegate
        {
          Action action = this.stopAction;
          if (action == null)
          {
            return;
          }
          action();
        };
        pressable.Tip("Stop this task");
        AutomationProperties.SetName(pressable, "Stop this task");
        AutomationProperties.SetHelpText(pressable, "Ends what it's doing now. The chat stays so you can retry.");
        Grid.SetColumn(pressable, 4);
        this.controls.Children.Add(pressable);
        this.stopButton = pressable;
      }
      this.stopButton.Visibility = Visibility.Visible;
    }

    // Token: 0x06001450 RID: 5200 RVA: 0x005AB060 File Offset: 0x005AB060
    private void Changed()
    {
      this.ReportDraft();
      this.Apply(true);
    }

    // Token: 0x06001451 RID: 5201 RVA: 0x005AB070 File Offset: 0x005AB070
    private void Apply(bool animated)
    {
      bool flag = this.isFocused || this.input.Text.Length > 0 || this.attachments.Count > 0 || this.isDropTargeting;
      bool flag2 = flag != this.isTyping;
      this.isTyping = flag;
      this.textBox.Width = ((flag || this.isInWindow) ? double.NaN : 150.0);
      Grid grid = this.textBox.Parent as Grid;
      if (grid != null)
      {
        grid.ColumnDefinitions[0].Width = (flag ? GridLength.Auto : new GridLength(1.0, GridUnitType.Star));
        grid.ColumnDefinitions[1].Width = ((flag || this.isInWindow) ? new GridLength(1.0, GridUnitType.Star) : GridLength.Auto);
      }
      this.voicePlate.Width = (flag ? 42.0 : double.NaN);
      this.voiceLabel.Visibility = (flag ? Visibility.Collapsed : Visibility.Visible);
      this.textBox.Children.OfType<StackPanel>().First<StackPanel>().Padding = (this.isInWindow ? new Thickness((double)(flag ? 8 : 14), 6.0, 6.0, 6.0) : new Thickness((double)(flag ? 8 : 16), 6.0, (double)(flag ? 6 : 16), 6.0));
      this.textBoxPlate.Background = Ui.Brush(this.PlateFill(flag));
      bool flag3 = this.isFocused || this.isDropTargeting;
      this.textBoxStroke.Visibility = (flag3 ? Visibility.Collapsed : Visibility.Visible);
      this.selectionRing.Visibility = (flag3 ? Visibility.Visible : Visibility.Collapsed);
      UIElement[] array = new UIElement[] { this.attach, this.send };
      for (int i = 0; i < array.Length; i++)
      {
        array[i].Visibility = (flag ? Visibility.Visible : Visibility.Collapsed);
      }
      this.readAloud.Visibility = ((flag && !this.isInWindow) ? Visibility.Visible : Visibility.Collapsed);
      bool canSend = this.CanSend;
      this.sendGel.Enabled = canSend;
      this.sendGlyph.Foreground = Ui.Brush(canSend ? DS.Black(0.85) : DS.Colors.GelDisabledContent);
      this.send.Enabled = canSend;
      bool flag4 = this.attachments.Count >= 10;
      this.attach.Enabled = !flag4;
      this.attachGlyph.Foreground = Ui.Brush(this.look.InkAt(flag4 ? 0.3 : 0.7));
      Pressable pressable = this.attach;
      string text;
      if (!flag4)
      {
        text = "Attach files";
      }
      else
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 1);
        defaultInterpolatedStringHandler.AppendLiteral("Up to ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(10);
        defaultInterpolatedStringHandler.AppendLiteral(" files");
        text = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      pressable.Tip(text);
      this.previewRow.Visibility = ((this.attachments.Count > 0) ? Visibility.Visible : Visibility.Collapsed);
      if (flag2 && animated && !Anim.ReduceMotion)
      {
        foreach (FrameworkElement frameworkElement in new FrameworkElement[] { this.attach, this.readAloud, this.send })
        {
          if (flag)
          {
            frameworkElement.Opacity = 0.0;
            Anim.Opacity(frameworkElement, 1f, 0.26, Easing.EaseOut(Ui.Compositor), 0.0);
          }
        }
      }
    }

    // Token: 0x06001452 RID: 5202 RVA: 0x005AB43C File Offset: 0x005AB43C
    private Color PlateFill(bool typing)
    {
      if (!this.isInWindow)
      {
        return DS.White(typing ? 0.16 : 0.12);
      }
      if (!typing)
      {
        return this.look.ComposerButtonFill;
      }
      return this.look.ComposerFieldFill;
    }

    // Token: 0x06001453 RID: 5203 RVA: 0x005AB488 File Offset: 0x005AB488
    private void SetFocused(bool focused)
    {
      if (this.isFocused == focused)
      {
        return;
      }
      this.isFocused = focused;
      if (!this.isInWindow)
      {
        NotchPresenter notchPresenter = this.presenter;
        if (notchPresenter != null)
        {
          notchPresenter.SetNotchAgentPeekHeldOpen(focused);
        }
        NotchPresenter notchPresenter2 = this.presenter;
        if (notchPresenter2 != null)
        {
          notchPresenter2.SetNotchChatComposerHasKeyboard(focused);
        }
      }
      if (!focused)
      {
        this.host.ReleaseKeyboardIfUnheld();
      }
      this.Apply(true);
    }

    // Token: 0x06001454 RID: 5204 RVA: 0x005AB4E8 File Offset: 0x005AB4E8
    private void ReportDraft()
    {
      if (this.IsFollowUp || this.isInWindow)
      {
        return;
      }
      NotchPresenter notchPresenter = this.presenter;
      if (notchPresenter == null)
      {
        return;
      }
      notchPresenter.SetNotchChatComposerHasDraft(this.input.Text.Trim().Length > 0 || this.attachments.Count > 0);
    }

    // Token: 0x06001455 RID: 5205 RVA: 0x005AB540 File Offset: 0x005AB540
    public void FocusField()
    {
      if (!this.IsFollowUp && !this.isInWindow)
      {
        NotchPresenter notchPresenter = this.presenter;
        if (notchPresenter != null && !notchPresenter.IsNotchChatSessionOpen)
        {
          notchPresenter.OpenNotchChatSession(NotchChatSessionOpenVia.ComposerClick);
        }
      }
      this.ClaimKeyboard("click", false);
    }

    // Token: 0x06001456 RID: 5206 RVA: 0x005AB584 File Offset: 0x005AB584
    public void TakeFocusIfRequested()
    {
      if (this.presenter == null || !this.presenter.Tabs.HasPendingComposerFocusRequest)
      {
        return;
      }
      this.ClaimKeyboard("request", true);
    }

    // Token: 0x06001457 RID: 5207 RVA: 0x005AB5B0 File Offset: 0x005AB5B0
    private void ClaimKeyboard(string via, bool consumesFocusRequest)
    {
      NotchChatComposer.<>c__DisplayClass67_0 CS$<>8__locals1 = new NotchChatComposer.<>c__DisplayClass67_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.consumesFocusRequest = consumesFocusRequest;
      CS$<>8__locals1.via = via;
      NotchChatComposer.<>c__DisplayClass67_0 CS$<>8__locals2 = CS$<>8__locals1;
      int num = this.focusGeneration + 1;
      this.focusGeneration = num;
      CS$<>8__locals2.generation = num;
      CS$<>8__locals1.startForeground = Native.GetForegroundWindow();
      CS$<>8__locals1.delays = new double[] { 0.0, 0.06, 0.15, 0.3, 0.5, 0.8, 1.2 };
      CS$<>8__locals1.done = false;
      CS$<>8__locals1.<ClaimKeyboard>g__Try|1(0);
      int num2 = 1;
      while (num2 < CS$<>8__locals1.delays.Length && !CS$<>8__locals1.done)
      {
        int tryIndex = num2;
        Microsoft.UI.Dispatching.DispatcherQueueTimer dispatcherQueueTimer = base.DispatcherQueue.CreateTimer().Rooted();
        dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(CS$<>8__locals1.delays[num2]);
        dispatcherQueueTimer.IsRepeating = false;
        dispatcherQueueTimer.Tick += delegate(Microsoft.UI.Dispatching.DispatcherQueueTimer _, object _)
        {
          CS$<>8__locals1.<ClaimKeyboard>g__Try|1(tryIndex);
        };
        dispatcherQueueTimer.Start();
        num2++;
      }
    }

    // Token: 0x170003B0 RID: 944
    // (get) Token: 0x06001458 RID: 5208 RVA: 0x005AB694 File Offset: 0x005AB694
    private bool IsFieldFocused
    {
      get
      {
        XamlRoot xamlRoot = base.XamlRoot;
        return xamlRoot != null && FocusManager.GetFocusedElement(xamlRoot) == this.input;
      }
    }

    // Token: 0x170003B1 RID: 945
    // (get) Token: 0x06001459 RID: 5209 RVA: 0x005AB6BC File Offset: 0x005AB6BC
    private bool HasKeyboardNow
    {
      get
      {
        return this.host.HasKeyboard && this.IsFieldFocused;
      }
    }

    // Token: 0x0600145A RID: 5210 RVA: 0x005AB6D4 File Offset: 0x005AB6D4
    public void Detached()
    {
      this.focusGeneration++;
      if (!this.isInWindow)
      {
        if (this.isFocused)
        {
          NotchPresenter notchPresenter = this.presenter;
          if (notchPresenter != null)
          {
            notchPresenter.SetNotchAgentPeekHeldOpen(false);
          }
        }
        NotchPresenter notchPresenter2 = this.presenter;
        if (notchPresenter2 != null)
        {
          notchPresenter2.SetNotchChatComposerHasKeyboard(false);
        }
      }
      this.isFocused = false;
    }

    // Token: 0x0600145B RID: 5211 RVA: 0x005AB72C File Offset: 0x005AB72C
    public static void ClearEveryDraft()
    {
      Action everyDraftCleared = NotchChatComposer.EveryDraftCleared;
      if (everyDraftCleared == null)
      {
        return;
      }
      everyDraftCleared();
    }

    // Token: 0x14000059 RID: 89
    // (add) Token: 0x0600145C RID: 5212 RVA: 0x005AB740 File Offset: 0x005AB740
    // (remove) Token: 0x0600145D RID: 5213 RVA: 0x005AB774 File Offset: 0x005AB774
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    private static event Action EveryDraftCleared;

    // Token: 0x0600145E RID: 5214 RVA: 0x005AB7A8 File Offset: 0x005AB7A8
    private void ClearDraft()
    {
      this.input.Text = "";
      this.attachments.Clear();
      this.previews.Children.Clear();
      this.Changed();
    }

    // Token: 0x0600145F RID: 5215 RVA: 0x005AB7DC File Offset: 0x005AB7DC
    public void ChatSessionClosed()
    {
      if (this.isFocused)
      {
        return;
      }
      this.Changed();
    }

    // Token: 0x06001460 RID: 5216 RVA: 0x005AB7F0 File Offset: 0x005AB7F0
    private void OnKey(object sender, KeyRoutedEventArgs e)
    {
      bool flag = InputKeyboardSource.GetKeyStateForCurrentThread(VirtualKey.Shift).HasFlag(CoreVirtualKeyStates.Down);
      if (e.Key == VirtualKey.Enter && !flag)
      {
        e.Handled = true;
        this.Send();
        return;
      }
      if (e.Key == VirtualKey.Escape)
      {
        e.Handled = true;
        this.focusGeneration++;
        this.SetFocused(false);
        this.host.ReleaseKeyboard();
      }
    }

    // Token: 0x06001461 RID: 5217 RVA: 0x005AB864 File Offset: 0x005AB864
    private void Send()
    {
      string text = this.input.Text.Trim();
      List<string> list = this.attachments.ToList<string>();
      if (text.Length == 0 && list.Count == 0)
      {
        return;
      }
      this.input.Text = "";
      this.attachments.Clear();
      this.previews.Children.Clear();
      if (this.IsFollowUp)
      {
        string text2 = this.followUpEntryId;
        ChatHandOff chatHandOff;
        if (text2 == null)
        {
          chatHandOff = null;
        }
        else
        {
          ChatHandOffs handOffs = NotchChat.HandOffs;
          chatHandOff = ((handOffs != null) ? handOffs.Entry(text2) : null);
        }
        ChatHandOff chatHandOff2 = chatHandOff ?? this.agentWithoutEvents;
        if (chatHandOff2 != null)
        {
          INotchActions notchActions = this.host.Actions as INotchActions;
          if (notchActions != null)
          {
            notchActions.SendTypedChatFollowUp(chatHandOff2, text, list, this.isInWindow);
          }
        }
        this.Changed();
        return;
      }
      if (!this.isInWindow)
      {
        NotchPresenter notchPresenter = this.presenter;
        if (notchPresenter != null && !notchPresenter.IsNotchChatSessionOpen)
        {
          notchPresenter.OpenNotchChatSession(NotchChatSessionOpenVia.Send);
        }
      }
      ConversationHistory history = NotchChat.History;
      if (history != null)
      {
        history.NoteUserSentText(text, list);
      }
      NotchPresenter notchPresenter2 = this.presenter;
      if (notchPresenter2 != null)
      {
        notchPresenter2.NoteTypedChatMessageSent(list.Count);
      }
      ClickyAnalytics.TrackProductEvent("notch_peek_action", null, null, NotchChatAnalytics.ChatSend(list.Count, text.Length > 0, this.host.Services.Settings.LibraryModeEnabled));
      INotchActions notchActions2 = this.host.Actions as INotchActions;
      if (notchActions2 != null)
      {
        notchActions2.SubmitChatText(text, list);
      }
      this.Changed();
    }

    // Token: 0x06001462 RID: 5218 RVA: 0x005AB9D0 File Offset: 0x005AB9D0
    private async void PickFiles()
    {
      try
      {
        FileOpenPicker fileOpenPicker = new FileOpenPicker();
        fileOpenPicker.FileTypeFilter.Add("*");
        InitializeWithWindow.Initialize(fileOpenPicker, this.host.WindowHandle);
        this.host.RequestKeyboard();
        IReadOnlyList<StorageFile> readOnlyList = await fileOpenPicker.PickMultipleFilesAsync();
        this.AddAttachments(readOnlyList.Select<StorageFile, string>((StorageFile f) => f.Path));
      }
      catch (Exception ex)
      {
        Log.Warn("notch-chat", "file picker failed: " + ex.Message);
      }
      this.FocusField();
    }

    // Token: 0x06001463 RID: 5219 RVA: 0x005ABA08 File Offset: 0x005ABA08
    public int AddAttachments(IEnumerable<string> paths)
    {
      List<string> list = paths.Where<string>((string p) => !this.attachments.Contains(p)).ToList<string>();
      ValueTuple<IReadOnlyList<string>, bool> valueTuple = ComposerAttachmentPolicy.Admit(this.attachments.Count, list);
      IReadOnlyList<string> item = valueTuple.Item1;
      bool item2 = valueTuple.Item2;
      foreach (string text in item)
      {
        this.attachments.Add(text);
        this.previews.Children.Add(this.Preview(text));
      }
      if (item2)
      {
        this.ShowCapNotice();
      }
      this.Changed();
      return item.Count;
    }

    // Token: 0x06001464 RID: 5220 RVA: 0x005ABABC File Offset: 0x005ABABC
    public void DrainDroppedAttachments()
    {
      if (this.presenter == null)
      {
        return;
      }
      IReadOnlyList<string> readOnlyList = this.presenter.TakePendingDroppedAttachments();
      if (readOnlyList.Count == 0)
      {
        return;
      }
      if (this.AddAttachments(readOnlyList) > 0)
      {
        ClickyAnalytics.TrackNotchDroppedFilesAttached(readOnlyList.Count);
      }
      this.FocusField();
    }

    // Token: 0x06001465 RID: 5221 RVA: 0x005ABB04 File Offset: 0x005ABB04
    private void ShowCapNotice()
    {
      Anim.Opacity(this.capNotice, 1f, 0.15, null, 0.0);
      NotchChatComposer.DispatcherQueueTimerHandle dispatcherQueueTimerHandle = this.capTimer;
      if (dispatcherQueueTimerHandle != null)
      {
        dispatcherQueueTimerHandle.Timer.Stop();
      }
      Microsoft.UI.Dispatching.DispatcherQueueTimer dispatcherQueueTimer = base.DispatcherQueue.CreateTimer().Rooted();
      dispatcherQueueTimer.Interval = ComposerAttachmentPolicy.CapNoticeDuration;
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(Microsoft.UI.Dispatching.DispatcherQueueTimer _, object _)
      {
        Anim.Opacity(this.capNotice, 0f, 0.15, null, 0.0);
      };
      dispatcherQueueTimer.Start();
      this.capTimer = new NotchChatComposer.DispatcherQueueTimerHandle(dispatcherQueueTimer);
    }

    // Token: 0x06001466 RID: 5222 RVA: 0x005ABB94 File Offset: 0x005ABB94
    private FrameworkElement Preview(string path)
    {
      Grid tile = new Grid
      {
        Width = this.previewSize + 6.0,
        Height = this.previewSize + 6.0
      };
      tile.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(11.0),
        Background = Ui.Brush(this.isInWindow ? this.look.InkAt(0.12) : DS.White(0.3))
      });
      Border border = new Border
      {
        Margin = new Thickness(3.0),
        CornerRadius = new CornerRadius(8.0),
        Background = (this.isInWindow ? Ui.Transparent : Ui.Brush(DS.Colors.Surface2))
      };
      if (ChatBlockViews.IsImage(path) && File.Exists(path))
      {
        border.Background = new ImageBrush
        {
          ImageSource = new BitmapImage(new Uri(path))
          {
            DecodePixelWidth = (int)(this.previewSize * 3.0)
          },
          Stretch = Stretch.UniformToFill
        };
      }
      else
      {
        ComposerBox.LoadShellIconAsync(border, path);
      }
      tile.Children.Add(border);
      Pressable remove = new Pressable(true)
      {
        Width = 13.0,
        Height = 13.0,
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Top,
        Margin = new Thickness(0.0, 3.0, 3.0, 0.0),
        Opacity = 0.0,
        IsHitTestVisible = false
      };
      remove.Children.Add(new Ellipse
      {
        Fill = Ui.Brush(DS.Black(0.6))
      });
      TextBlock textBlock = Icons.Glyph("\ue711", 7.0, Colors.White);
      textBlock.FontWeight = FontWeights.Bold;
      remove.Children.Add(textBlock);
      remove.Click += delegate
      {
        this.attachments.Remove(path);
        this.previews.Children.Remove(tile);
        this.Changed();
      };
      tile.Children.Add(remove);
      tile.PointerEntered += delegate(object _, PointerRoutedEventArgs _)
      {
        remove.IsHitTestVisible = true;
        Anim.Opacity(remove, 1f, 0.12, null, 0.0);
      };
      tile.PointerExited += delegate(object _, PointerRoutedEventArgs e)
      {
        if (PointerFrames.IsStale(e))
        {
          return;
        }
        remove.IsHitTestVisible = false;
        Anim.Opacity(remove, 0f, 0.12, null, 0.0);
      };
      ToolTipService.SetToolTip(tile, Path.GetFileName(path));
      return tile;
    }

    // Token: 0x06001467 RID: 5223 RVA: 0x005ABE6C File Offset: 0x005ABE6C
    private async void OnPaste(object sender, TextControlPasteEventArgs e)
    {
      DataPackageView content = Clipboard.GetContent();
      if (content.Contains(StandardDataFormats.StorageItems))
      {
        e.Handled = true;
        try
        {
          IReadOnlyList<string> readOnlyList = ComposerAttachmentPolicy.PastableImageFiles((await content.GetStorageItemsAsync()).Select<IStorageItem, string>((IStorageItem i) => i.Path));
          if (readOnlyList.Count > 0)
          {
            this.AddAttachments(readOnlyList);
          }
        }
        catch (Exception ex)
        {
          Log.Warn("notch-chat", "paste files failed: " + ex.Message);
        }
      }
      else if (content.Contains(StandardDataFormats.Bitmap))
      {
        e.Handled = true;
        try
        {
          RandomAccessStreamReference randomAccessStreamReference = await content.GetBitmapAsync();
          string tempPath = Path.GetTempPath();
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 1);
          defaultInterpolatedStringHandler.AppendLiteral("clicky-paste-");
          defaultInterpolatedStringHandler.AppendFormatted<long>(DateTimeOffset.Now.ToUnixTimeMilliseconds());
          defaultInterpolatedStringHandler.AppendLiteral(".png");
          string path = Path.Combine(tempPath, defaultInterpolatedStringHandler.ToStringAndClear());
          using (IRandomAccessStreamWithContentType src = await randomAccessStreamReference.OpenReadAsync())
          {
            using (FileStream dst = File.Create(path))
            {
              await src.AsStreamForRead().CopyToAsync(dst);
            }
            FileStream dst = null;
          }
          IRandomAccessStreamWithContentType src = null;
          this.AddAttachments(new <>z__ReadOnlySingleElementList<string>(path));
          path = null;
        }
        catch (Exception ex2)
        {
          Log.Warn("notch-chat", "paste image failed: " + ex2.Message);
        }
      }
    }

    // Token: 0x06001468 RID: 5224 RVA: 0x005ABEAC File Offset: 0x005ABEAC
    public void SetDropTargeting(bool on)
    {
      this.isDropTargeting = on;
      this.placeholder.Visibility = ((this.input.Text.Length == 0 && !on) ? Visibility.Visible : Visibility.Collapsed);
      Anim.Opacity(this.input, !on, 0.15, null, 0.0);
      Anim.Opacity(this.dropLabel, on > false, 0.15, null, 0.0);
      this.Apply(true);
    }

    // Token: 0x0400099D RID: 2461
    public const double WindowColumnWidth = 680.0;

    // Token: 0x0400099E RID: 2462
    public const double WindowColumnSidePadding = 24.0;

    // Token: 0x0400099F RID: 2463
    private readonly double previewSize;

    // Token: 0x040009A0 RID: 2464
    private readonly MessagesPalette look = MessagesPalette.Current;

    // Token: 0x040009A1 RID: 2465
    private readonly INotchHost host;

    // Token: 0x040009A2 RID: 2466
    [Nullable(2)]
    private readonly NotchPresenter presenter;

    // Token: 0x040009A3 RID: 2467
    private readonly Pressable voicePlate;

    // Token: 0x040009A4 RID: 2468
    private readonly TextBlock voiceLabel;

    // Token: 0x040009A5 RID: 2469
    private readonly TextBlock micGlyph;

    // Token: 0x040009A6 RID: 2470
    private readonly Grid micAtRest;

    // Token: 0x040009A7 RID: 2471
    private readonly Grid textBox = new Grid();

    // Token: 0x040009A8 RID: 2472
    private readonly Border textBoxPlate = new Border();

    // Token: 0x040009A9 RID: 2473
    private readonly Border textBoxStroke = new Border
    {
      IsHitTestVisible = false
    };

    // Token: 0x040009AA RID: 2474
    private readonly Grid selectionRing;

    // Token: 0x040009AB RID: 2475
    private readonly TextBox input;

    // Token: 0x040009AC RID: 2476
    private readonly TextBlock placeholder;

    // Token: 0x040009AD RID: 2477
    private readonly TextBlock dropLabel;

    // Token: 0x040009AE RID: 2478
    private readonly Pressable attach;

    // Token: 0x040009AF RID: 2479
    private readonly Pressable send;

    // Token: 0x040009B0 RID: 2480
    [Nullable(2)]
    private Pressable stopButton;

    // Token: 0x040009B1 RID: 2481
    [Nullable(2)]
    private Action stopAction;

    // Token: 0x040009B2 RID: 2482
    private readonly GelSurface sendGel;

    // Token: 0x040009B3 RID: 2483
    private readonly TextBlock sendGlyph;

    // Token: 0x040009B4 RID: 2484
    private readonly TextBlock attachGlyph;

    // Token: 0x040009B5 RID: 2485
    private readonly ReadAloudSwitch readAloud;

    // Token: 0x040009B6 RID: 2486
    private readonly StackPanel previews = new StackPanel
    {
      Orientation = Orientation.Horizontal,
      Spacing = 6.0,
      Padding = new Thickness(4.0, 4.0, 4.0, 0.0)
    };

    // Token: 0x040009B7 RID: 2487
    private readonly ScrollViewer previewRow;

    // Token: 0x040009B8 RID: 2488
    private readonly Grid controls = new Grid
    {
      ColumnSpacing = 8.0
    };

    // Token: 0x040009B9 RID: 2489
    private readonly List<string> attachments = new List<string>();

    // Token: 0x040009BA RID: 2490
    private readonly Border capNotice;

    // Token: 0x040009BB RID: 2491
    private bool isFocused;

    // Token: 0x040009BC RID: 2492
    private bool isDropTargeting;

    // Token: 0x040009BD RID: 2493
    private bool isTyping;

    // Token: 0x040009BE RID: 2494
    private int focusGeneration;

    // Token: 0x040009C1 RID: 2497
    [Nullable(2)]
    private string followUpEntryId;

    // Token: 0x040009C2 RID: 2498
    [Nullable(2)]
    private readonly ChatHandOff agentWithoutEvents;

    // Token: 0x040009C3 RID: 2499
    private readonly bool isInWindow;

    // Token: 0x040009C4 RID: 2500
    private readonly Grid voiceIndicator = new Grid();

    // Token: 0x040009C5 RID: 2501
    private VoiceState shownVoiceState;

    // Token: 0x040009C7 RID: 2503
    [Nullable(2)]
    private NotchChatComposer.DispatcherQueueTimerHandle capTimer;

    // Token: 0x020006F0 RID: 1776
    [Nullable(0)]
    private sealed class DispatcherQueueTimerHandle : IEquatable<NotchChatComposer.DispatcherQueueTimerHandle>
    {
      // Token: 0x060033B5 RID: 13237 RVA: 0x00677FAC File Offset: 0x00677FAC
      public DispatcherQueueTimerHandle(Microsoft.UI.Dispatching.DispatcherQueueTimer Timer)
      {
        this.Timer = Timer;
        base..ctor();
      }

      // Token: 0x170007AD RID: 1965
      // (get) Token: 0x060033B6 RID: 13238 RVA: 0x00677FBC File Offset: 0x00677FBC
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchChatComposer.DispatcherQueueTimerHandle);
        }
      }

      // Token: 0x170007AE RID: 1966
      // (get) Token: 0x060033B7 RID: 13239 RVA: 0x00677FC8 File Offset: 0x00677FC8
      // (set) Token: 0x060033B8 RID: 13240 RVA: 0x00677FD0 File Offset: 0x00677FD0
      public Microsoft.UI.Dispatching.DispatcherQueueTimer Timer { get; set; }

      // Token: 0x060033B9 RID: 13241 RVA: 0x00677FDC File Offset: 0x00677FDC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("DispatcherQueueTimerHandle");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060033BA RID: 13242 RVA: 0x00678028 File Offset: 0x00678028
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Timer = ");
        builder.Append(this.Timer);
        return true;
      }

      // Token: 0x060033BB RID: 13243 RVA: 0x0067804C File Offset: 0x0067804C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchChatComposer.DispatcherQueueTimerHandle left, NotchChatComposer.DispatcherQueueTimerHandle right)
      {
        return !(left == right);
      }

      // Token: 0x060033BC RID: 13244 RVA: 0x00678058 File Offset: 0x00678058
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchChatComposer.DispatcherQueueTimerHandle left, NotchChatComposer.DispatcherQueueTimerHandle right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060033BD RID: 13245 RVA: 0x0067806C File Offset: 0x0067806C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Microsoft.UI.Dispatching.DispatcherQueueTimer>.Default.GetHashCode(this.<Timer>k__BackingField);
      }

      // Token: 0x060033BE RID: 13246 RVA: 0x00678098 File Offset: 0x00678098
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchChatComposer.DispatcherQueueTimerHandle);
      }

      // Token: 0x060033BF RID: 13247 RVA: 0x006780A8 File Offset: 0x006780A8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchChatComposer.DispatcherQueueTimerHandle other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Microsoft.UI.Dispatching.DispatcherQueueTimer>.Default.Equals(this.<Timer>k__BackingField, other.<Timer>k__BackingField));
      }

      // Token: 0x060033C1 RID: 13249 RVA: 0x006780E8 File Offset: 0x006780E8
      [CompilerGenerated]
      private DispatcherQueueTimerHandle(NotchChatComposer.DispatcherQueueTimerHandle original)
      {
        this.Timer = original.<Timer>k__BackingField;
      }

      // Token: 0x060033C2 RID: 13250 RVA: 0x006780FC File Offset: 0x006780FC
      [CompilerGenerated]
      public void Deconstruct(out Microsoft.UI.Dispatching.DispatcherQueueTimer Timer)
      {
        Timer = this.Timer;
      }
    }
  }
}
