using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Notch.Peek;
using HeyClicky.App.Proactive;
using HeyClicky.App.Services;
using HeyClicky.App.UI;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Telemetry;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x02000167 RID: 359
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class TextInputSurface : NotchSurface
  {
    // Token: 0x060013B3 RID: 5043 RVA: 0x005A44B4 File Offset: 0x005A44B4
    public TextInputSurface(NotchPresentation.TextInput p, bool signedIn, Func<IntPtr> hwnd)
    {
      this.hwnd = hwnd;
      this.composer = new ComposerBox("ask HeyClicky…", signedIn)
      {
        WindowHandle = hwnd,
        VerticalAlignment = VerticalAlignment.Top
      };
      if (!string.IsNullOrEmpty(p.Draft))
      {
        this.composer.Input.Text = p.Draft;
      }
      ValueTuple<Border, TextBlock> valueTuple = TextInputSurface.BuildFollowUpPill();
      this.followUpPill = valueTuple.Item1;
      this.followUpTitle = valueTuple.Item2;
      this.view.Children.Add(this.followUpPill);
      this.view.Children.Add(this.composer);
      this.ApplyFollowUp(p.AgentFollowUp);
      this.overlay = this.BuildEar();
    }

    // Token: 0x17000397 RID: 919
    // (get) Token: 0x060013B4 RID: 5044 RVA: 0x005A462C File Offset: 0x005A462C
    public override FrameworkElement View
    {
      get
      {
        return this.view;
      }
    }

    // Token: 0x17000398 RID: 920
    // (get) Token: 0x060013B5 RID: 5045 RVA: 0x005A4634 File Offset: 0x005A4634
    [Nullable(2)]
    public override FrameworkElement Overlay
    {
      [NullableContext(2)]
      get
      {
        return this.overlay;
      }
    }

    // Token: 0x17000399 RID: 921
    // (get) Token: 0x060013B6 RID: 5046 RVA: 0x005A463C File Offset: 0x005A463C
    public override bool WantsKeyboard
    {
      get
      {
        return true;
      }
    }

    // Token: 0x060013B7 RID: 5047 RVA: 0x005A4640 File Offset: 0x005A4640
    public override void FocusInput()
    {
      this.composer.FocusInput();
      this.composer.Input.SelectionStart = this.composer.Input.Text.Length;
    }

    // Token: 0x060013B8 RID: 5048 RVA: 0x005A4674 File Offset: 0x005A4674
    public override bool CanUpdate(NotchPresentation p)
    {
      return p is NotchPresentation.TextInput;
    }

    // Token: 0x060013B9 RID: 5049 RVA: 0x005A4680 File Offset: 0x005A4680
    public override void Update(NotchPresentation p)
    {
      NotchPresentation.TextInput textInput = p as NotchPresentation.TextInput;
      if (textInput == null)
      {
        return;
      }
      this.ApplyFollowUp(textInput.AgentFollowUp);
      if (!string.IsNullOrEmpty(textInput.Draft) && textInput.Draft != this.composer.Input.Text)
      {
        this.composer.Input.Text = textInput.Draft;
      }
    }

    // Token: 0x1700039A RID: 922
    // (get) Token: 0x060013BA RID: 5050 RVA: 0x005A46E4 File Offset: 0x005A46E4
    [Nullable(2)]
    internal NotchAgentFollowUp AgentFollowUp
    {
      [NullableContext(2)]
      get
      {
        return this.agentFollowUp;
      }
    }

    // Token: 0x1700039B RID: 923
    // (get) Token: 0x060013BB RID: 5051 RVA: 0x005A46EC File Offset: 0x005A46EC
    internal ComposerBox Composer
    {
      get
      {
        return this.composer;
      }
    }

    // Token: 0x060013BC RID: 5052 RVA: 0x005A46F4 File Offset: 0x005A46F4
    [NullableContext(2)]
    private void ApplyFollowUp(NotchAgentFollowUp followUp)
    {
      this.agentFollowUp = followUp;
      this.composer.SetPlaceholder((followUp == null) ? "ask HeyClicky…" : "follow up with agent…");
      this.followUpPill.Visibility = ((followUp == null) ? Visibility.Collapsed : Visibility.Visible);
      if (followUp == null)
      {
        return;
      }
      this.followUpTitle.Text = followUp.Title;
      ToolTipService.SetToolTip(this.followUpPill, "Following up with " + followUp.Title);
    }

    // Token: 0x060013BD RID: 5053 RVA: 0x005A4764 File Offset: 0x005A4764
    [return: TupleElementNames(new string[] { "Pill", "Title" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    private static ValueTuple<Border, TextBlock> BuildFollowUpPill()
    {
      TextBlock textBlock = Icons.Glyph("\ue89c", 8.5, DS.White(0.9));
      textBlock.FontWeight = FontWeights.Bold;
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      TextBlock textBlock2 = Ui.Text("", 11.0, FontWeights.SemiBold, DS.White(0.9), TextAlignment.Left, 1, false, false);
      textBlock2.TextTrimming = TextTrimming.CharacterEllipsis;
      textBlock2.VerticalAlignment = VerticalAlignment.Center;
      return new ValueTuple<Border, TextBlock>(new Border
      {
        Height = 22.0,
        Padding = new Thickness(9.0, 0.0, 9.0, 0.0),
        Margin = new Thickness(8.0, 0.0, 8.0, 0.0),
        CornerRadius = new CornerRadius(11.0),
        HorizontalAlignment = HorizontalAlignment.Left,
        Background = Ui.Brush(DS.White(0.08)),
        BorderBrush = Ui.Brush(DS.White(0.12)),
        BorderThickness = new Thickness(0.5),
        Tag = "agent-follow-up-pill",
        Child = TextInputSurface.PillRow(textBlock, textBlock2)
      }, textBlock2);
    }

    // Token: 0x060013BE RID: 5054 RVA: 0x005A48D0 File Offset: 0x005A48D0
    private static Grid PillRow(UIElement arrow, TextBlock title)
    {
      Grid grid = new Grid();
      grid.ColumnSpacing = 5.0;
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      Grid.SetColumn(title, 1);
      grid.Children.Add(arrow);
      grid.Children.Add(title);
      return grid;
    }

    // Token: 0x1700039C RID: 924
    // (get) Token: 0x060013BF RID: 5055 RVA: 0x005A4950 File Offset: 0x005A4950
    private bool IsOnboardingTextBox
    {
      get
      {
        if (this.agentFollowUp == null)
        {
          NotchPresenter notchPresenter = base.Host.Services.Presenter as NotchPresenter;
          return notchPresenter != null && !notchPresenter.RoutesTextModeToNotchChat;
        }
        return false;
      }
    }

    // Token: 0x060013C0 RID: 5056 RVA: 0x005A498C File Offset: 0x005A498C
    protected override void OnAttached()
    {
      if (this.IsOnboardingTextBox)
      {
        this.readAloudSlot.Visibility = Visibility.Collapsed;
        this.earDivider.Visibility = Visibility.Collapsed;
        this.composer.RemoveAttachButton();
      }
      else if (this.readAloudSlot.Children.Count == 0)
      {
        this.readAloudSlot.Children.Add(new ReadAloudSwitch(base.Host.Services, 20.0, true));
      }
      base.Host.SizeContext.TextInputExtraLines = 0;
      base.Host.SizeContext.TextInputAttachmentCount = 0;
      this.composer.MetricsChanged += delegate(int extra, int attachments)
      {
        base.Host.SizeContext.TextInputExtraLines = extra;
        base.Host.SizeContext.TextInputAttachmentCount = attachments;
        base.Host.Relayout();
      };
      this.composer.Submitted += delegate(string text, IReadOnlyList<string> files)
      {
        ProactiveCoordinator proactiveCoordinator = ProactiveCoordinator.Current;
        if (proactiveCoordinator != null && proactiveCoordinator.SubmitRemoteTaskModification(text))
        {
          base.Host.Services.Presenter.Present(new NotchPresentation.Idle());
          return;
        }
        NotchAgentFollowUp notchAgentFollowUp = this.agentFollowUp;
        string text2 = ((notchAgentFollowUp != null) ? notchAgentFollowUp.TaskId : null);
        if (text2 != null)
        {
          INotchCardActions notchCardActions = base.Host.Actions as INotchCardActions;
          if (notchCardActions != null)
          {
            base.Host.Services.Presenter.Present(new NotchPresentation.Idle());
            notchCardActions.SendAgentFollowUp(text2, text, files);
            return;
          }
        }
        if (base.Host.Services.Settings.LibraryModeEnabled)
        {
          base.Host.Services.Presenter.Present(new NotchPresentation.Idle());
        }
        base.Host.Services.Companion.SubmitTextAsync(text, files);
      };
      this.composer.Cancelled += delegate
      {
        ProactiveCoordinator proactiveCoordinator2 = ProactiveCoordinator.Current;
        if (proactiveCoordinator2 != null)
        {
          proactiveCoordinator2.CancelRemoteTaskModification();
        }
        base.Host.Services.Presenter.Present(new NotchPresentation.Idle());
      };
      this.AttachDropped(base.Host.TakePendingFiles());
      this.composer.Input.TextChanged += delegate(object _, TextChangedEventArgs _)
      {
        if (this.composer.Input.Text.Length > 0)
        {
          Action typing = TextInputSurface.Typing;
          if (typing == null)
          {
            return;
          }
          typing();
        }
      };
    }

    // Token: 0x14000054 RID: 84
    // (add) Token: 0x060013C1 RID: 5057 RVA: 0x005A4A98 File Offset: 0x005A4A98
    // (remove) Token: 0x060013C2 RID: 5058 RVA: 0x005A4ACC File Offset: 0x005A4ACC
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public static event Action Typing;

    // Token: 0x060013C3 RID: 5059 RVA: 0x005A4B00 File Offset: 0x005A4B00
    public override bool AcceptFiles(IReadOnlyList<string> paths)
    {
      this.AttachDropped(paths);
      return true;
    }

    // Token: 0x060013C4 RID: 5060 RVA: 0x005A4B0C File Offset: 0x005A4B0C
    private void AttachDropped(IReadOnlyList<string> paths)
    {
      if (paths.Count == 0)
      {
        return;
      }
      int num = this.composer.AddAttachments(paths);
      if (num > 0)
      {
        ClickyAnalytics.TrackNotchDroppedFilesAttached(num);
      }
    }

    // Token: 0x060013C5 RID: 5061 RVA: 0x005A4B3C File Offset: 0x005A4B3C
    public override void SetDropTargeting(bool on)
    {
      this.composer.SetDropTargeting(on, DS.CursorColor(base.Host.Services.Settings.CursorColorHex));
    }

    // Token: 0x060013C6 RID: 5062 RVA: 0x005A4B64 File Offset: 0x005A4B64
    public override void Detached()
    {
      base.Host.SizeContext.TextInputExtraLines = 0;
      base.Host.SizeContext.TextInputAttachmentCount = 0;
    }

    // Token: 0x060013C7 RID: 5063 RVA: 0x005A4B88 File Offset: 0x005A4B88
    private Grid BuildEar()
    {
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 8.0,
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Top,
        Margin = new Thickness(0.0, 5.0, 26.0, 0.0),
        Height = 24.0
      };
      stackPanel.Children.Add(this.readAloudSlot);
      stackPanel.Children.Add(this.earDivider);
      Pressable pressable = new Pressable(true)
      {
        Width = 24.0,
        Height = 24.0,
        VerticalAlignment = VerticalAlignment.Center
      }.Named("Close");
      pressable.Children.Add(new Ellipse
      {
        Width = 20.0,
        Height = 20.0,
        Fill = Ui.Brush(DS.White(0.1)),
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      });
      TextBlock textBlock = Icons.Glyph("\ue711", 8.5, DS.Colors.TextSecondary);
      textBlock.FontWeight = FontWeights.Bold;
      pressable.Children.Add(textBlock);
      pressable.Click += delegate
      {
        base.Host.Services.Presenter.Present(new NotchPresentation.Idle());
      };
      pressable.WithIconHover(24.0);
      stackPanel.Children.Add(pressable);
      return new Grid
      {
        IsHitTestVisible = true,
        Children = { stackPanel }
      };
    }

    // Token: 0x0400092D RID: 2349
    private readonly ComposerBox composer;

    // Token: 0x0400092E RID: 2350
    private readonly StackPanel view = new StackPanel
    {
      Spacing = 6.0,
      VerticalAlignment = VerticalAlignment.Top
    };

    // Token: 0x0400092F RID: 2351
    private readonly Border followUpPill;

    // Token: 0x04000930 RID: 2352
    private readonly TextBlock followUpTitle;

    // Token: 0x04000931 RID: 2353
    private readonly Grid overlay;

    // Token: 0x04000932 RID: 2354
    private readonly Grid readAloudSlot = new Grid
    {
      Width = 20.0,
      Height = 20.0,
      VerticalAlignment = VerticalAlignment.Center
    };

    // Token: 0x04000933 RID: 2355
    private readonly Rectangle earDivider = new Rectangle
    {
      Width = 1.0,
      Height = 12.0,
      RadiusX = 0.5,
      RadiusY = 0.5,
      Fill = Ui.Brush(DS.White(0.14)),
      VerticalAlignment = VerticalAlignment.Center
    };

    // Token: 0x04000934 RID: 2356
    private readonly Func<IntPtr> hwnd;

    // Token: 0x04000935 RID: 2357
    [Nullable(2)]
    private NotchAgentFollowUp agentFollowUp;
  }
}
