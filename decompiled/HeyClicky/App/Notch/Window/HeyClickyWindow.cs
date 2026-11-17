using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using HeyClicky.Agents;
using HeyClicky.Agents.Clickys;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.Billing;
using HeyClicky.App.Design;
using HeyClicky.App.Dock;
using HeyClicky.App.Home;
using HeyClicky.App.Ink;
using HeyClicky.App.Interop;
using HeyClicky.App.Notch.Peek;
using HeyClicky.App.Services;
using HeyClicky.App.Settings;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using HeyClicky.Core.Billing;
using HeyClicky.Core.Configuration;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Telemetry;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Input;
using Microsoft.UI.Text;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Graphics;
using Windows.System;
using Windows.UI;
using WinRT.Interop;

namespace HeyClicky.App.Notch.Window
{
  // Token: 0x0200013F RID: 319
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class HeyClickyWindow
  {
    // Token: 0x17000340 RID: 832
    // (get) Token: 0x060011A1 RID: 4513 RVA: 0x0058E5FC File Offset: 0x0058E5FC
    public static HeyClickyWindow Shared { get; } = new HeyClickyWindow();

    // Token: 0x17000341 RID: 833
    // (get) Token: 0x060011A2 RID: 4514 RVA: 0x0058E604 File Offset: 0x0058E604
    private MessagesPalette Look
    {
      get
      {
        return this.palette.Messages;
      }
    }

    // Token: 0x060011A3 RID: 4515 RVA: 0x0058E614 File Offset: 0x0058E614
    public void Configure(INotchHost notchHost, [Nullable(2)] ArcDock dock)
    {
      this.notchHost = notchHost;
      this.dock = dock;
    }

    // Token: 0x17000342 RID: 834
    // (get) Token: 0x060011A4 RID: 4516 RVA: 0x0058E624 File Offset: 0x0058E624
    public bool IsOpen
    {
      get
      {
        return this.window != null;
      }
    }

    // Token: 0x17000343 RID: 835
    // (get) Token: 0x060011A5 RID: 4517 RVA: 0x0058E634 File Offset: 0x0058E634
    internal IntPtr Hwnd
    {
      get
      {
        return this.hwnd;
      }
    }

    // Token: 0x17000344 RID: 836
    // (get) Token: 0x060011A6 RID: 4518 RVA: 0x0058E63C File Offset: 0x0058E63C
    public bool IsInFront
    {
      get
      {
        return this.hwnd != 0 && Native.GetForegroundWindow() == this.hwnd;
      }
    }

    // Token: 0x17000345 RID: 837
    // (get) Token: 0x060011A7 RID: 4519 RVA: 0x0058E658 File Offset: 0x0058E658
    public static bool KeepsAgentLinesOffCursor
    {
      get
      {
        return HeyClickyWindow.Shared.IsInFront;
      }
    }

    // Token: 0x17000346 RID: 838
    // (get) Token: 0x060011A8 RID: 4520 RVA: 0x0058E664 File Offset: 0x0058E664
    public bool IsOpenOnScreen
    {
      get
      {
        return this.hwnd != 0 && Native.IsWindowVisible(this.hwnd) && !Native.IsIconic(this.hwnd);
      }
    }

    // Token: 0x060011A9 RID: 4521 RVA: 0x0058E68C File Offset: 0x0058E68C
    [NullableContext(2)]
    public string AgentKeyUnderPointer()
    {
      IntPtr intPtr = this.hwnd;
      if (intPtr != 0)
      {
        string text = this.shownAgentKey;
        if (text != null && Native.IsWindowVisible(intPtr) && !Native.IsIconic(intPtr))
        {
          Native.POINT point;
          if (!Native.GetCursorPos(out point))
          {
            return null;
          }
          IntPtr intPtr2 = Native.WindowFromPoint(point);
          if (intPtr2 == 0 || Native.GetAncestor(intPtr2, 2U) != intPtr)
          {
            return null;
          }
          return text;
        }
      }
      return null;
    }

    // Token: 0x060011AA RID: 4522 RVA: 0x0058E6E4 File Offset: 0x0058E6E4
    [NullableContext(2)]
    public void Open(string key = null, bool settingsPage = false, string document = null, string documentThreadId = null, [Nullable(1)] string from = "notch", HeyClickyWindowOpeningFlight flight = null, SettingsWindow.Page settingsSection = SettingsWindow.Page.Account)
    {
      if (this.notchHost == null)
      {
        if (flight != null)
        {
          flight.Abandon();
        }
        return;
      }
      bool flag = this.window != null;
      if (flag && HeyClickyWindowOpeningFlight.IsFlying && flight == null)
      {
        return;
      }
      bool flag2 = flag && this.IsOpenOnScreen && VirtualDesktops.IsOnCurrentDesktop(this.hwnd);
      if (!flag)
      {
        this.Build();
      }
      if (flag)
      {
        if (flight != null)
        {
          flight.Abandon();
        }
        flight = null;
      }
      if (flight != null)
      {
        HeyClickyWindowOpeningFlight.Cloak(this.hwnd, true);
      }
      if (settingsPage)
      {
        this.ShowSettings(settingsSection);
      }
      else
      {
        if (this.showingSettings)
        {
          this.LeaveSettings();
        }
        string text = key ?? this.selectedKey;
        this.Select(this.KeyStillExists(text) ? text : "heyclicky");
      }
      if (document != null)
      {
        this.ShowDocument(document, documentThreadId);
      }
      string text2 = "notch_peek_action";
      string text3 = null;
      string text4 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "window_opened";
      jsonObject["from"] = from;
      jsonObject["was_open"] = flag;
      ClickyAnalytics.TrackProductEvent(text2, text3, text4, jsonObject);
      if (flag)
      {
        if (Native.IsIconic(this.hwnd))
        {
          Native.ShowWindow(this.hwnd, 9);
        }
        VirtualDesktops.MoveToCurrentDesktop(this.hwnd);
      }
      this.window.Activate();
      Native.SetForegroundWindow(this.hwnd);
      this.ParkFocus(false);
      this.window.DispatcherQueue.TryEnqueue(delegate
      {
        this.ParkFocus(false);
      });
      if (flight != null)
      {
        try
        {
          this.Land(flight);
          return;
        }
        catch (Exception ex)
        {
          Log.Error("window", "flight: landing failed", ex);
          HeyClickyWindowOpeningFlight.Cloak(this.hwnd, false);
          flight.Abandon();
          return;
        }
      }
      if (!flag2)
      {
        this.Arrive(flag);
      }
    }

    // Token: 0x060011AB RID: 4523 RVA: 0x0058E8B8 File Offset: 0x0058E8B8
    private void Land(HeyClickyWindowOpeningFlight flight)
    {
      Native.RECT rect = HeyClickyWindowOpeningFlight.VisibleFrame(this.hwnd);
      Displays.Monitor monitor = Displays.ForWindow(this.notchHost.WindowHandle);
      if (Displays.ForWindow(this.hwnd).Handle != monitor.Handle && !OverlayWindow.Offscreen)
      {
        HeyClickyWindowOpeningFlight.Cloak(this.hwnd, false);
        flight.Abandon();
        this.Arrive(false);
        return;
      }
      IntPtr landing = this.hwnd;
      flight.Bloom(rect, delegate
      {
        if (this.hwnd != landing || this.root == null)
        {
          flight.Dispose();
          return;
        }
        HeyClickyWindowOpeningFlight.Cloak(this.hwnd, false);
        HeyClickyWindowOpeningFlight.LastUncoveredAt = DateTime.UtcNow;
        if (this.root.ActualWidth > 0.0)
        {
          Anim.ScaleUpFrom(this.root, 0.955f, new SwiftSpring(0.45, 0.85), new Vector3?(new Vector3((float)this.root.ActualWidth / 2f, (float)this.root.ActualHeight / 2f, 0f)));
        }
        flight.Reveal(null);
      });
    }

    // Token: 0x17000347 RID: 839
    // (get) Token: 0x060011AC RID: 4524 RVA: 0x0058E95C File Offset: 0x0058E95C
    public bool WasInFrontJustNow
    {
      get
      {
        return this.IsInFront || (this.hwnd != 0 && Environment.TickCount64 - this.lastDeactivatedTicks < 1000L);
      }
    }

    // Token: 0x060011AD RID: 4525 RVA: 0x0058E988 File Offset: 0x0058E988
    public bool BringOpenWindowToFront()
    {
      if (this.window == null || this.hwnd == 0)
      {
        return false;
      }
      if (Native.IsIconic(this.hwnd))
      {
        Native.ShowWindow(this.hwnd, 9);
      }
      this.window.Activate();
      Native.ForceForeground(this.hwnd);
      return true;
    }

    // Token: 0x060011AE RID: 4526 RVA: 0x0058E9DC File Offset: 0x0058E9DC
    private void Arrive(bool alreadyOpen)
    {
      HeyClickyWindow.<>c__DisplayClass67_0 CS$<>8__locals1 = new HeyClickyWindow.<>c__DisplayClass67_0();
      CS$<>8__locals1.<>4__this = this;
      if (this.root == null || Anim.ReduceMotion)
      {
        return;
      }
      this.root.Opacity = 0.0;
      CS$<>8__locals1.spring = new SwiftSpring(alreadyOpen ? 0.3 : 0.42, alreadyOpen ? 0.9 : 0.82);
      Anim.OpacitySpring(this.root, 1f, CS$<>8__locals1.spring);
      if (alreadyOpen)
      {
        this.root.Translation = new Vector3(0f, 14f, 0f);
        Anim.TranslationSpring(this.root, Vector3.Zero, CS$<>8__locals1.spring);
        return;
      }
      if (this.root.ActualWidth > 0.0)
      {
        Anim.ScaleUpFrom(this.root, 0.955f, CS$<>8__locals1.spring, new Vector3?(new Vector3((float)this.root.ActualWidth / 2f, 0f, 0f)));
        return;
      }
      this.root.SizeChanged += CS$<>8__locals1.<Arrive>g__Grow|0;
    }

    // Token: 0x060011AF RID: 4527 RVA: 0x0058EB0C File Offset: 0x0058EB0C
    private void Build()
    {
      this.window = new Window
      {
        Title = "HeyClicky"
      };
      this.hwnd = WindowNative.GetWindowHandle(this.window);
      this.appWindow = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(this.hwnd));
      OverlappedPresenter overlappedPresenter = this.appWindow.Presenter as OverlappedPresenter;
      if (overlappedPresenter != null)
      {
        overlappedPresenter.IsResizable = true;
        overlappedPresenter.IsMaximizable = true;
        overlappedPresenter.IsMinimizable = true;
        overlappedPresenter.PreferredMinimumWidth = new int?(720);
        overlappedPresenter.PreferredMinimumHeight = new int?(480);
      }
      this.window.ExtendsContentIntoTitleBar = true;
      this.appWindow.TitleBar.ButtonBackgroundColor = new Color?(Colors.Transparent);
      this.appWindow.TitleBar.ButtonInactiveBackgroundColor = new Color?(Colors.Transparent);
      this.palette = HeyClickyWindowPalette.Current();
      this.PaintCaptionButtons();
      try
      {
        this.appWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets", "AppIcon.ico"));
      }
      catch
      {
      }
      this.host = new HeyClickyWindowHost(this.notchHost, this, this.hwnd);
      this.root = this.BuildContent();
      this.ground = new Grid
      {
        Background = Ui.Brush(this.palette.Ground),
        RequestedTheme = (this.palette.IsDark ? ElementTheme.Dark : ElementTheme.Light)
      };
      this.ground.Children.Add(this.root);
      this.window.Content = this.ground;
      this.PlaceFromSavedFrame();
      Microsoft.UI.Dispatching.DispatcherQueue queue = this.window.DispatcherQueue;
      this.lookWatcher = new SystemLookWatcher(delegate
      {
        queue.TryEnqueue(new Microsoft.UI.Dispatching.DispatcherQueueHandler(this.ApplySystemLook));
      });
      BillingUi billing = this.notchHost.Services.Billing;
      if (billing != null)
      {
        billing.Changed += this.RebuildAccountRow;
      }
      NotchPresenter notchPresenter = this.notchHost.Services.Presenter as NotchPresenter;
      if (notchPresenter != null)
      {
        notchPresenter.PresentationChanged += this.OnPresentationChanged;
      }
      AgentService agentService = this.notchHost.Services.Agents as AgentService;
      if (agentService != null)
      {
        agentService.Jobs.Changed += this.OnAgentsChanged;
      }
      if (this.dock != null)
      {
        this.dock.ContentChanged += this.OnDockContentChanged;
      }
      this.window.Activated += delegate(object _, WindowActivatedEventArgs e)
      {
        bool flag = e.WindowActivationState != WindowActivationState.Deactivated;
        if (!flag)
        {
          this.lastDeactivatedTicks = Environment.TickCount64;
        }
        if (flag == this.isActive)
        {
          return;
        }
        this.isActive = flag;
        this.host.NoteActive(flag);
      };
      this.appWindow.Changed += delegate(AppWindow _, AppWindowChangedEventArgs e)
      {
        if (e.DidPositionChange || e.DidSizeChange)
        {
          this.SaveFrameSoon();
        }
      };
      this.window.Closed += delegate(object _, WindowEventArgs _)
      {
        this.OnClosed();
      };
    }

    // Token: 0x060011B0 RID: 4528 RVA: 0x0058EDCC File Offset: 0x0058EDCC
    private void OnClosed()
    {
      this.SaveFrame();
      this.mainChat = null;
      SettingsContent settingsContent = this.settings;
      if (settingsContent != null)
      {
        settingsContent.Hidden();
      }
      this.settings = null;
      this.documentViewer = null;
      this.documentPath = null;
      this.documentThreadId = null;
      this.window = null;
      this.appWindow = null;
      this.hwnd = (IntPtr)0;
      this.root = null;
      this.ground = null;
      this.mainCard = null;
      this.columnHeader = null;
      this.dragBand = null;
      this.headerButtons.Clear();
      this.shownAgentKey = null;
      this.shownChild = null;
      this.stage = null;
      INotchHost notchHost = this.notchHost;
      NotchPresenter notchPresenter = ((notchHost != null) ? notchHost.Services.Presenter : null) as NotchPresenter;
      if (notchPresenter != null)
      {
        notchPresenter.PresentationChanged -= this.OnPresentationChanged;
      }
      INotchHost notchHost2 = this.notchHost;
      NotchPresenter notchPresenter2 = ((notchHost2 != null) ? notchHost2.Services.Presenter : null) as NotchPresenter;
      if (notchPresenter2 != null)
      {
        notchPresenter2.SetWindowShowsMainThread(false);
      }
      ChatHandOffs handOffs = NotchChat.HandOffs;
      if (handOffs != null)
      {
        handOffs.Changed -= this.OnHandOffsChanged;
      }
      INotchHost notchHost3 = this.notchHost;
      AgentService agentService = ((notchHost3 != null) ? notchHost3.Services.Agents : null) as AgentService;
      if (agentService != null)
      {
        agentService.Jobs.Changed -= this.OnAgentsChanged;
      }
      if (this.dock != null)
      {
        this.dock.ContentChanged -= this.OnDockContentChanged;
      }
      CompositionTarget.Rendering -= this.OnColumnFrame;
      this.columnFramesRunning = false;
      CompositionTarget.Rendering -= this.OnWindowMoveFrame;
      this.windowMoving = false;
      this.focusRest = null;
      SystemLookWatcher systemLookWatcher = this.lookWatcher;
      if (systemLookWatcher != null)
      {
        systemLookWatcher.Dispose();
      }
      this.lookWatcher = null;
      INotchHost notchHost4 = this.notchHost;
      BillingUi billingUi = ((notchHost4 != null) ? notchHost4.Services.Billing : null);
      if (billingUi != null)
      {
        billingUi.Changed -= this.RebuildAccountRow;
      }
      this.accountSlot = null;
      this.dropOverlay = null;
      string text = "notch_peek_action";
      string text2 = null;
      string text3 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "window_closed";
      ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
    }

    // Token: 0x060011B1 RID: 4529 RVA: 0x0058EFF4 File Offset: 0x0058EFF4
    public void Close()
    {
      Window window = this.window;
      if (window == null)
      {
        return;
      }
      window.Close();
    }

    // Token: 0x060011B2 RID: 4530 RVA: 0x0058F008 File Offset: 0x0058F008
    private Grid BuildContent()
    {
      Grid grid3;
      using (MessagesPalette.Use(this.Look))
      {
        MessagesPalette look = this.Look;
        Grid grid = new Grid();
        grid.Background = Ui.Brush(this.palette.Ground);
        grid.RequestedTheme = (this.palette.IsDark ? ElementTheme.Dark : ElementTheme.Light);
        this.sidebarDefinition = new ColumnDefinition
        {
          Width = new GridLength(this.saved.SidebarShown ? 244.0 : 0.0)
        };
        this.documentDefinition = new ColumnDefinition
        {
          Width = new GridLength(0.0)
        };
        grid.ColumnDefinitions.Add(this.sidebarDefinition);
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = new GridLength(1.0, GridUnitType.Star),
          MinWidth = 380.0
        });
        grid.ColumnDefinitions.Add(this.documentDefinition);
        this.focusRest = new ContentControl
        {
          Width = 1.0,
          Height = 1.0,
          IsTabStop = true,
          UseSystemFocusVisuals = false,
          Opacity = 0.0,
          IsHitTestVisible = false,
          HorizontalAlignment = HorizontalAlignment.Left,
          VerticalAlignment = VerticalAlignment.Top
        };
        AutomationProperties.SetName(this.focusRest, "HeyClicky");
        grid.Children.Add(this.focusRest);
        this.dragBand = new Grid
        {
          Height = 36.0,
          VerticalAlignment = VerticalAlignment.Top,
          Background = Ui.Transparent
        };
        Grid.SetColumnSpan(this.dragBand, 3);
        grid.Children.Add(this.dragBand);
        this.window.SetTitleBar(this.dragBand);
        this.headerButtons.Clear();
        this.sidebarColumn = this.BuildSidebar();
        this.sidebarColumn.Width = 244.0;
        this.sidebarColumn.HorizontalAlignment = HorizontalAlignment.Right;
        grid.Children.Add(this.sidebarColumn);
        this.sidebarShownness = new SpringValue(this.SidebarVisible > false, 0.0005);
        this.documentShownness = new SpringValue(0.0, 0.0005);
        grid.SizeChanged += delegate(object _, SizeChangedEventArgs _)
        {
          if (!this.columnFramesRunning)
          {
            this.ApplyColumnFrame();
          }
        };
        Grid grid2 = new Grid();
        grid2.RowDefinitions.Add(new RowDefinition
        {
          Height = GridLength.Auto
        });
        grid2.RowDefinitions.Add(new RowDefinition
        {
          Height = new GridLength(1.0, GridUnitType.Star)
        });
        this.chatDots = new ClickyPaperBoardDots(new Color?(look.ChatDots), true);
        Grid.SetRowSpan(this.chatDots, 2);
        grid2.Children.Add(this.chatDots);
        this.columnHeader = new Border();
        grid2.Children.Add(this.columnHeader);
        this.stage = new Grid();
        this.shownChild = null;
        this.mainColumn = new Border
        {
          Child = this.stage
        };
        Grid.SetRow(this.mainColumn, 1);
        grid2.Children.Add(this.mainColumn);
        this.mainCard = new Border
        {
          Background = Ui.Brush(look.PaneFill),
          CornerRadius = new CornerRadius(12.0),
          BorderBrush = Ui.Brush(look.InkAt(0.08)),
          BorderThickness = new Thickness(1.0),
          Child = grid2
        };
        Grid.SetColumn(this.mainCard, 1);
        grid.Children.Add(this.mainCard);
        Ui.ClipRounded(this.mainCard, 12.0);
        this.AcceptFileDrops(this.mainColumn);
        this.dropOverlay = this.DropTargetOverlay();
        this.dropTargeted = false;
        Grid.SetColumn(this.dropOverlay, 1);
        grid.Children.Add(this.dropOverlay);
        this.documentColumn = this.BuildDocumentColumn();
        Grid.SetColumn(this.documentColumn, 2);
        this.documentColumn.Visibility = Visibility.Collapsed;
        this.documentColumn.Opacity = 0.0;
        grid.Children.Add(this.documentColumn);
        this.sidebarButton = this.HeaderButton("\ue90c", this.SidebarButtonTip, new Action(this.ToggleSidebar));
        this.backButton = this.BackChip();
        this.backButton.Visibility = Visibility.Collapsed;
        this.topLeft = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 8.0,
          Height = 36.0,
          Margin = new Thickness(14.0, 0.0, 0.0, 0.0),
          HorizontalAlignment = HorizontalAlignment.Left,
          VerticalAlignment = VerticalAlignment.Top,
          Children = { this.sidebarButton, this.backButton }
        };
        Grid.SetColumnSpan(this.topLeft, 3);
        grid.Children.Add(this.topLeft);
        this.LayOutCards(false);
        grid.AddHandler(UIElement.PreviewKeyDownEvent, new KeyEventHandler(delegate(object _, KeyRoutedEventArgs e)
        {
          if (e.Key == VirtualKey.Escape && this.<BuildContent>g__LeaveTextInput|82_3())
          {
            e.Handled = true;
          }
        }), true);
        KeyboardAccelerator keyboardAccelerator = new KeyboardAccelerator
        {
          Key = VirtualKey.Escape
        };
        keyboardAccelerator.Invoked += delegate(KeyboardAccelerator _, KeyboardAcceleratorInvokedEventArgs e)
        {
          if (this.documentPath != null)
          {
            e.Handled = true;
            this.CloseDocument();
          }
        };
        KeyboardAccelerator keyboardAccelerator2 = new KeyboardAccelerator
        {
          Key = VirtualKey.W,
          Modifiers = VirtualKeyModifiers.Control
        };
        keyboardAccelerator2.Invoked += delegate(KeyboardAccelerator _, KeyboardAcceleratorInvokedEventArgs e)
        {
          e.Handled = true;
          this.Close();
        };
        grid.KeyboardAccelerators.Add(keyboardAccelerator);
        grid.KeyboardAccelerators.Add(keyboardAccelerator2);
        grid.KeyboardAcceleratorPlacementMode = KeyboardAcceleratorPlacementMode.Hidden;
        grid.LayoutUpdated += delegate([Nullable(2)] object _, object _)
        {
          this.LetHeaderButtonsThrough();
        };
        ChatHandOffs handOffs = NotchChat.HandOffs;
        if (handOffs != null)
        {
          handOffs.Changed -= this.OnHandOffsChanged;
          handOffs.Changed += this.OnHandOffsChanged;
        }
        grid3 = grid;
      }
      return grid3;
    }

    // Token: 0x17000348 RID: 840
    // (get) Token: 0x060011B3 RID: 4531 RVA: 0x0058F64C File Offset: 0x0058F64C
    private bool SidebarVisible
    {
      get
      {
        return this.saved.SidebarShown && !this.showingSettings;
      }
    }

    // Token: 0x060011B4 RID: 4532 RVA: 0x0058F668 File Offset: 0x0058F668
    private void LayOutCards(bool animated = true)
    {
      bool flag;
      if (animated && !Anim.ReduceMotion)
      {
        Grid grid = this.root;
        flag = grid != null && grid.IsLoaded;
      }
      else
      {
        flag = false;
      }
      bool flag2 = flag;
      SpringValue springValue = this.sidebarShownness;
      if (springValue != null)
      {
        springValue.Move(this.SidebarVisible > false, flag2 ? new SwiftSpring?(HeyClickyWindow.ColumnSpring) : null);
      }
      SpringValue springValue2 = this.documentShownness;
      if (springValue2 != null)
      {
        springValue2.Move(this.documentPath != null, flag2 ? new SwiftSpring?(HeyClickyWindow.ColumnSpring) : null);
      }
      if (!this.ApplyColumnFrame())
      {
        this.StartColumnFrames();
      }
      if (this.sidebarButton != null)
      {
        this.sidebarButton.Visibility = (this.showingSettings ? Visibility.Collapsed : Visibility.Visible);
      }
      if (this.backButton != null)
      {
        this.backButton.Visibility = (this.showingSettings ? Visibility.Visible : Visibility.Collapsed);
      }
      if (this.headerChild != null)
      {
        this.headerChild.Margin = this.ColumnHeaderInsets();
      }
    }

    // Token: 0x060011B5 RID: 4533 RVA: 0x0058F768 File Offset: 0x0058F768
    private void StartColumnFrames()
    {
      if (this.columnFramesRunning)
      {
        return;
      }
      this.columnFramesRunning = true;
      CompositionTarget.Rendering += this.OnColumnFrame;
    }

    // Token: 0x060011B6 RID: 4534 RVA: 0x0058F78C File Offset: 0x0058F78C
    private void OnColumnFrame([Nullable(2)] object sender, object e)
    {
      if (!this.ApplyColumnFrame() && this.root != null)
      {
        return;
      }
      CompositionTarget.Rendering -= this.OnColumnFrame;
      this.columnFramesRunning = false;
    }

    // Token: 0x060011B7 RID: 4535 RVA: 0x0058F7B8 File Offset: 0x0058F7B8
    private bool ApplyColumnFrame()
    {
      if (this.sidebarDefinition == null || this.documentDefinition == null || this.sidebarShownness == null || this.documentShownness == null)
      {
        return true;
      }
      double now = SpringCurve.Now;
      double num = Math.Clamp(this.sidebarShownness.ValueAt(now), 0.0, 1.0);
      double num2 = Math.Clamp(this.documentShownness.ValueAt(now), 0.0, 1.0);
      this.sidebarDefinition.Width = new GridLength(244.0 * num);
      if (this.sidebarColumn != null)
      {
        this.sidebarColumn.Opacity = num;
        this.sidebarColumn.Visibility = ((num > 0.0) ? Visibility.Visible : Visibility.Collapsed);
      }
      if (this.mainCard != null)
      {
        this.mainCard.Margin = new Thickness(8.0 * (1.0 - num), 36.0, 8.0, 8.0);
      }
      if (this.dropOverlay != null && this.mainCard != null)
      {
        this.dropOverlay.Margin = this.mainCard.Margin;
      }
      double num3 = this.ClampDocumentWidth(this.saved.DocumentWidth);
      this.documentDefinition.Width = new GridLength(num3 * num2);
      if (this.documentPlate != null)
      {
        this.documentPlate.Width = Math.Max(0.0, num3 - 8.0);
      }
      if (this.documentColumn != null)
      {
        this.documentColumn.Opacity = num2;
        this.documentColumn.Visibility = ((num2 > 0.0 || this.documentPath != null) ? Visibility.Visible : Visibility.Collapsed);
      }
      bool flag = num == this.sidebarShownness.Target && num2 == this.documentShownness.Target;
      if (flag && this.documentPath == null && this.documentViewer != null && this.documentViewerHolds)
      {
        this.documentViewerHolds = false;
        this.documentViewer.Show(null, null);
      }
      return flag;
    }

    // Token: 0x060011B8 RID: 4536 RVA: 0x0058F9C4 File Offset: 0x0058F9C4
    private Thickness ColumnHeaderInsets()
    {
      return new Thickness((double)(this.showingSettings ? 0 : 16), 0.0, 12.0, 0.0);
    }

    // Token: 0x060011B9 RID: 4537 RVA: 0x0058F9F4 File Offset: 0x0058F9F4
    private Pressable HeaderButton(string glyph, string tip, Action click)
    {
      MessagesPalette m = this.Look;
      Pressable p = new Pressable(true)
      {
        Width = 26.0,
        Height = 24.0,
        CornerRadius = new CornerRadius(6.0),
        VerticalAlignment = VerticalAlignment.Center
      };
      TextBlock g = Icons.Glyph(glyph, 12.0, m.InkAt(0.6));
      g.HorizontalAlignment = HorizontalAlignment.Center;
      g.VerticalAlignment = VerticalAlignment.Center;
      p.Children.Add(g);
      p.HoverChanged += delegate(bool h)
      {
        p.Background = (h ? Ui.Brush(m.InkAt(0.1)) : null);
        g.Foreground = Ui.Brush(m.InkAt(h ? 0.95 : 0.6));
      };
      p.Click += click;
      p.Tip(tip);
      AutomationProperties.SetName(p, tip);
      this.headerButtons.Add(p);
      return p;
    }

    // Token: 0x060011BA RID: 4538 RVA: 0x0058FB04 File Offset: 0x0058FB04
    private Pressable BackChip()
    {
      MessagesPalette m = this.Look;
      Pressable chip = new Pressable(true)
      {
        Height = 24.0,
        Padding = new Thickness(10.0, 0.0, 10.0, 0.0),
        CornerRadius = new CornerRadius(12.0),
        VerticalAlignment = VerticalAlignment.Center,
        Background = Ui.Brush(m.InkAt(0.08)),
        BorderBrush = Ui.Brush(m.InkAt(0.1)),
        BorderThickness = new Thickness(1.0)
      };
      TextBlock arrow = Icons.Glyph("\ue72b", 10.5, m.InkAt(0.78));
      arrow.FontWeight = FontWeights.Bold;
      arrow.VerticalAlignment = VerticalAlignment.Center;
      TextBlock words = Ui.Text("Back", 12.0, FontWeights.SemiBold, m.InkAt(0.78), TextAlignment.Left, 1, false, false);
      words.VerticalAlignment = VerticalAlignment.Center;
      chip.Children.Add(new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 5.0,
        VerticalAlignment = VerticalAlignment.Center,
        Children = { arrow, words }
      });
      chip.HoverChanged += delegate(bool h)
      {
        chip.Background = Ui.Brush(m.InkAt(h ? 0.14 : 0.08));
        arrow.Foreground = (words.Foreground = Ui.Brush(m.InkAt(h ? 0.95 : 0.78)));
      };
      chip.Click += this.LeaveSettings;
      chip.Tip("Back to chats");
      AutomationProperties.SetName(chip, "Back to chats");
      this.headerButtons.Add(chip);
      return chip;
    }

    // Token: 0x060011BB RID: 4539 RVA: 0x0058FD20 File Offset: 0x0058FD20
    private void LetHeaderButtonsThrough()
    {
      if (this.appWindow != null && this.root != null)
      {
        XamlRoot xamlRoot = this.root.XamlRoot;
        if (xamlRoot != null)
        {
          double rasterizationScale = xamlRoot.RasterizationScale;
          List<RectInt32> list = new List<RectInt32>();
          foreach (FrameworkElement frameworkElement in this.headerButtons)
          {
            if (frameworkElement.Visibility == Visibility.Visible && frameworkElement.ActualWidth > 0.0 && this.IsShown(frameworkElement))
            {
              Point point = frameworkElement.TransformToVisual(this.root).TransformPoint(new Point(0f, 0f));
              list.Add(new RectInt32((int)Math.Floor(point.X * rasterizationScale), (int)Math.Floor(point.Y * rasterizationScale), (int)Math.Ceiling(frameworkElement.ActualWidth * rasterizationScale), (int)Math.Ceiling(frameworkElement.ActualHeight * rasterizationScale)));
            }
          }
          string text = string.Join(";", list.Select<RectInt32, string>(delegate(RectInt32 r)
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 4);
            defaultInterpolatedStringHandler.AppendFormatted<int>(r.X);
            defaultInterpolatedStringHandler.AppendLiteral(",");
            defaultInterpolatedStringHandler.AppendFormatted<int>(r.Y);
            defaultInterpolatedStringHandler.AppendLiteral(",");
            defaultInterpolatedStringHandler.AppendFormatted<int>(r.Width);
            defaultInterpolatedStringHandler.AppendLiteral(",");
            defaultInterpolatedStringHandler.AppendFormatted<int>(r.Height);
            return defaultInterpolatedStringHandler.ToStringAndClear();
          }));
          if (text == this.passthroughShape)
          {
            return;
          }
          this.passthroughShape = text;
          try
          {
            InputNonClientPointerSource.GetForWindowId(this.appWindow.Id).SetRegionRects(NonClientRegionKind.Passthrough, list.ToArray());
          }
          catch (Exception ex)
          {
            Log.Warn("window", "header buttons not let through: " + ex.Message);
          }
          return;
        }
      }
    }

    // Token: 0x060011BC RID: 4540 RVA: 0x0058FEC4 File Offset: 0x0058FEC4
    private bool IsShown(FrameworkElement element)
    {
      DependencyObject dependencyObject = element;
      while (dependencyObject != null && dependencyObject != this.root)
      {
        UIElement uielement = dependencyObject as UIElement;
        if (uielement != null && uielement.Visibility == Visibility.Collapsed)
        {
          return false;
        }
        dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
      }
      return true;
    }

    // Token: 0x060011BD RID: 4541 RVA: 0x0058FF04 File Offset: 0x0058FF04
    private bool TextInputHasFocus()
    {
      Grid grid = this.root;
      XamlRoot xamlRoot = ((grid != null) ? grid.XamlRoot : null);
      bool flag = xamlRoot != null;
      if (flag)
      {
        object focusedElement = FocusManager.GetFocusedElement(xamlRoot);
        bool flag2 = focusedElement is TextBox || focusedElement is PasswordBox || focusedElement is RichEditBox || focusedElement is AutoSuggestBox;
        flag = flag2;
      }
      return flag;
    }

    // Token: 0x060011BE RID: 4542 RVA: 0x0058FF5C File Offset: 0x0058FF5C
    private void ParkFocus(bool force)
    {
      if (this.focusRest == null || (!force && !this.TextInputHasFocus()))
      {
        return;
      }
      this.focusRest.Focus(FocusState.Programmatic);
    }

    // Token: 0x060011BF RID: 4543 RVA: 0x0058FF80 File Offset: 0x0058FF80
    [NullableContext(2)]
    private bool FocusIsWithin(UIElement element)
    {
      if (element != null)
      {
        Grid grid = this.root;
        XamlRoot xamlRoot = ((grid != null) ? grid.XamlRoot : null);
        if (xamlRoot != null)
        {
          DependencyObject dependencyObject = FocusManager.GetFocusedElement(xamlRoot) as DependencyObject;
          if (dependencyObject != null)
          {
            for (DependencyObject dependencyObject2 = dependencyObject; dependencyObject2 != null; dependencyObject2 = VisualTreeHelper.GetParent(dependencyObject2))
            {
              if (dependencyObject2 == element)
              {
                return true;
              }
            }
            return false;
          }
        }
      }
      return false;
    }

    // Token: 0x060011C0 RID: 4544 RVA: 0x0058FFCC File Offset: 0x0058FFCC
    private void PaintCaptionButtons()
    {
      if (this.appWindow == null)
      {
        return;
      }
      this.appWindow.TitleBar.ButtonForegroundColor = new Color?(this.palette.InkAt(0.85));
      this.appWindow.TitleBar.ButtonHoverForegroundColor = new Color?(this.palette.InkAt(0.95));
      this.appWindow.TitleBar.ButtonHoverBackgroundColor = new Color?(this.palette.InkAt(0.08));
      this.appWindow.TitleBar.ButtonPressedBackgroundColor = new Color?(this.palette.InkAt(0.12));
      this.appWindow.TitleBar.ButtonInactiveForegroundColor = new Color?(this.palette.InkAt(0.4));
    }

    // Token: 0x060011C1 RID: 4545 RVA: 0x005900B0 File Offset: 0x005900B0
    private void ApplySystemLook()
    {
      if (this.root == null || this.ground == null)
      {
        return;
      }
      HeyClickyWindowPalette heyClickyWindowPalette = HeyClickyWindowPalette.Current();
      if (heyClickyWindowPalette.IsDark == this.palette.IsDark)
      {
        return;
      }
      this.palette = heyClickyWindowPalette;
      this.PaintCaptionButtons();
      this.mainChat = null;
      SettingsContent settingsContent = this.settings;
      if (settingsContent != null)
      {
        settingsContent.Hidden();
      }
      this.settings = null;
      this.documentViewer = null;
      string text = this.documentPath;
      string text2 = this.documentThreadId;
      this.documentPath = (this.documentThreadId = null);
      bool flag = this.showingSettings;
      this.showingSettings = false;
      this.ground.Background = Ui.Brush(this.palette.Ground);
      this.ground.RequestedTheme = (this.palette.IsDark ? ElementTheme.Dark : ElementTheme.Light);
      this.ground.Children.Clear();
      this.passthroughShape = "";
      this.root = this.BuildContent();
      this.ground.Children.Add(this.root);
      if (flag)
      {
        this.ShowSettings();
      }
      else
      {
        string text3 = this.selectedKey;
        this.selectedKey = "";
        this.Select(text3);
      }
      if (text != null)
      {
        this.ShowDocument(text, text2);
      }
      this.SettleColumns();
    }

    // Token: 0x060011C2 RID: 4546 RVA: 0x005901F0 File Offset: 0x005901F0
    private void SettleColumns()
    {
      this.sidebarShownness = new SpringValue(this.SidebarVisible > false, 0.0005);
      this.documentShownness = new SpringValue(this.documentPath != null, 0.0005);
      this.ApplyColumnFrame();
    }

    // Token: 0x060011C3 RID: 4547 RVA: 0x00590244 File Offset: 0x00590244
    private void AcceptFileDrops(Border column)
    {
      HeyClickyWindow.<>c__DisplayClass108_0 CS$<>8__locals1 = new HeyClickyWindow.<>c__DisplayClass108_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.column = column;
      CS$<>8__locals1.column.AllowDrop = true;
      CS$<>8__locals1.column.AddHandler(UIElement.DragOverEvent, new DragEventHandler(delegate(object _, DragEventArgs e)
      {
        if (!e.DataView.Contains(StandardDataFormats.StorageItems) || CS$<>8__locals1.<>4__this.DropTarget() == null)
        {
          return;
        }
        e.AcceptedOperation = DataPackageOperation.Copy;
        e.DragUIOverride.Caption = "Attach";
        CS$<>8__locals1.<>4__this.dragLeaveCount = CS$<>8__locals1.<>4__this.dragLeaveCount + 1;
        CS$<>8__locals1.<>4__this.ShowDropTargeting(true);
      }), true);
      CS$<>8__locals1.column.AddHandler(UIElement.DragLeaveEvent, new DragEventHandler(delegate(object _, DragEventArgs _)
      {
        HeyClickyWindow.<>c__DisplayClass108_1 CS$<>8__locals2 = new HeyClickyWindow.<>c__DisplayClass108_1();
        CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
        HeyClickyWindow.<>c__DisplayClass108_1 CS$<>8__locals3 = CS$<>8__locals2;
        HeyClickyWindow <>4__this = CS$<>8__locals1.<>4__this;
        int num = CS$<>8__locals1.<>4__this.dragLeaveCount + 1;
        <>4__this.dragLeaveCount = num;
        CS$<>8__locals3.leave = num;
        CS$<>8__locals2.t = CS$<>8__locals1.column.DispatcherQueue.CreateTimer().Rooted();
        CS$<>8__locals2.t.Interval = TimeSpan.FromSeconds(0.12);
        CS$<>8__locals2.t.IsRepeating = false;
        CS$<>8__locals2.t.Tick += delegate(Microsoft.UI.Dispatching.DispatcherQueueTimer _, object _)
        {
          CS$<>8__locals2.t.Stop();
          if (CS$<>8__locals2.leave == CS$<>8__locals2.CS$<>8__locals1.<>4__this.dragLeaveCount)
          {
            CS$<>8__locals2.CS$<>8__locals1.<>4__this.ShowDropTargeting(false);
          }
        };
        CS$<>8__locals2.t.Start();
      }), true);
      CS$<>8__locals1.column.AddHandler(UIElement.DropEvent, new DragEventHandler(delegate(object _, DragEventArgs e)
      {
        HeyClickyWindow.<>c__DisplayClass108_0.<<AcceptFileDrops>b__2>d <<AcceptFileDrops>b__2>d;
        <<AcceptFileDrops>b__2>d.<>t__builder = AsyncVoidMethodBuilder.Create();
        <<AcceptFileDrops>b__2>d.<>4__this = CS$<>8__locals1;
        <<AcceptFileDrops>b__2>d.e = e;
        <<AcceptFileDrops>b__2>d.<>1__state = -1;
        <<AcceptFileDrops>b__2>d.<>t__builder.Start<HeyClickyWindow.<>c__DisplayClass108_0.<<AcceptFileDrops>b__2>d>(ref <<AcceptFileDrops>b__2>d);
      }), true);
    }

    // Token: 0x060011C4 RID: 4548 RVA: 0x005902C8 File Offset: 0x005902C8
    [NullableContext(2)]
    private FrameworkElement DropTarget()
    {
      NotchChatView notchChatView;
      if (!this.showingSettings)
      {
        if ((notchChatView = this.shownChild as NotchChatView) == null)
        {
          return this.shownChild as HeyClickyWindowConversation;
        }
      }
      else
      {
        notchChatView = null;
      }
      return notchChatView;
    }

    // Token: 0x060011C5 RID: 4549 RVA: 0x005902F0 File Offset: 0x005902F0
    private void ShowDropTargeting(bool on)
    {
      Grid overlay = this.dropOverlay;
      if (overlay != null && this.dropTargeted != on)
      {
        this.dropTargeted = on;
        if (on)
        {
          overlay.Visibility = Visibility.Visible;
        }
        if (Anim.ReduceMotion)
        {
          overlay.Opacity = on > false;
          if (!on)
          {
            overlay.Visibility = Visibility.Collapsed;
          }
        }
        else
        {
          Anim.Opacity(overlay, on > false, 0.14, Easing.EaseOut(Ui.Compositor), 0.0);
          if (!on)
          {
            Microsoft.UI.Dispatching.DispatcherQueueTimer t = overlay.DispatcherQueue.CreateTimer().Rooted();
            t.Interval = TimeSpan.FromSeconds(0.16);
            t.IsRepeating = false;
            t.Tick += delegate(Microsoft.UI.Dispatching.DispatcherQueueTimer _, object _)
            {
              t.Stop();
              if (!this.dropTargeted)
              {
                overlay.Visibility = Visibility.Collapsed;
              }
            };
            t.Start();
          }
        }
      }
      FrameworkElement frameworkElement = this.DropTarget();
      NotchChatView notchChatView = frameworkElement as NotchChatView;
      if (notchChatView != null)
      {
        notchChatView.SetDropTargeting(on);
        return;
      }
      HeyClickyWindowConversation heyClickyWindowConversation = frameworkElement as HeyClickyWindowConversation;
      if (heyClickyWindowConversation == null)
      {
        return;
      }
      heyClickyWindowConversation.SetDropTargeting(on);
    }

    // Token: 0x060011C6 RID: 4550 RVA: 0x00590430 File Offset: 0x00590430
    private Grid DropTargetOverlay()
    {
      MessagesPalette look = this.Look;
      Grid grid = new Grid();
      grid.IsHitTestVisible = false;
      grid.Visibility = Visibility.Collapsed;
      grid.Opacity = 0.0;
      grid.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(12.0),
        Background = Ui.Brush(look.PaneFill.WithAlpha(0.72))
      });
      grid.Children.Add(new Rectangle
      {
        Margin = new Thickness(6.0),
        RadiusX = 11.0,
        RadiusY = 11.0,
        Stroke = Ui.Brush(DS.Hex(3112182U, 0.8)),
        StrokeThickness = 2.0,
        StrokeDashArray = new DoubleCollection { 4.0, 3.0 }
      });
      TextBlock textBlock = Icons.Glyph("\ue723", 22.0, look.PrimaryText);
      textBlock.FontWeight = FontWeights.SemiBold;
      textBlock.HorizontalAlignment = HorizontalAlignment.Center;
      TextBlock textBlock2 = Ui.Text("Drop to attach", 14.0, FontWeights.SemiBold, look.PrimaryText, TextAlignment.Left, 0, false, false);
      textBlock2.HorizontalAlignment = HorizontalAlignment.Center;
      grid.Children.Add(new StackPanel
      {
        Spacing = 8.0,
        VerticalAlignment = VerticalAlignment.Center,
        Children = { textBlock, textBlock2 }
      });
      return grid;
    }

    // Token: 0x17000349 RID: 841
    // (get) Token: 0x060011C7 RID: 4551 RVA: 0x005905D8 File Offset: 0x005905D8
    private string SidebarButtonTip
    {
      get
      {
        if (!this.saved.SidebarShown)
        {
          return "Show sidebar";
        }
        return "Hide sidebar";
      }
    }

    // Token: 0x060011C8 RID: 4552 RVA: 0x005905F4 File Offset: 0x005905F4
    private void ToggleSidebar()
    {
      HeyClickyWindow.State state = this.saved.<Clone>$();
      state.SidebarShown = !this.saved.SidebarShown;
      this.saved = state;
      if (this.sidebarButton != null)
      {
        this.sidebarButton.Tip(this.SidebarButtonTip);
        AutomationProperties.SetName(this.sidebarButton, this.SidebarButtonTip);
      }
      this.LayOutCards(true);
      this.SaveFrame();
    }

    // Token: 0x060011C9 RID: 4553 RVA: 0x00590660 File Offset: 0x00590660
    private Grid BuildSidebar()
    {
      Grid grid = new Grid();
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(44.0)
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star)
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      this.sidebarViews.Clear();
      this.sidebarSections.Clear();
      this.sidebarShape = "";
      this.sidebarRows = new StackPanel
      {
        Spacing = 3.0,
        Padding = new Thickness(8.0, 0.0, 8.0, 12.0)
      };
      ScrollViewer scrollViewer = new ScrollViewer
      {
        Content = this.sidebarRows,
        VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled
      };
      Grid.SetRow(scrollViewer, 1);
      grid.Children.Add(scrollViewer);
      this.accountSlot = new Border();
      Grid.SetRow(this.accountSlot, 2);
      grid.Children.Add(this.accountSlot);
      this.RebuildAccountRow();
      this.RebuildSidebar();
      return grid;
    }

    // Token: 0x060011CA RID: 4554 RVA: 0x005907A8 File Offset: 0x005907A8
    private List<HeyClickyWindow.SidebarEntry> SidebarEntries()
    {
      List<HeyClickyWindow.SidebarEntry> list = new List<HeyClickyWindow.SidebarEntry>();
      INotchHost notchHost = this.notchHost;
      AgentService agentService = ((notchHost != null) ? notchHost.Services.Agents : null) as AgentService;
      if (agentService == null)
      {
        return list;
      }
      ArcDock arcDock = this.dock;
      HashSet<string> hashSet = ((arcDock != null) ? arcDock.DockKeys().ToHashSet<string>() : null) ?? new HashSet<string>();
      foreach (ClickyJob clickyJob in agentService.Jobs.Jobs)
      {
        if (!clickyJob.IsArchived.GetValueOrDefault() && !clickyJob.IsWaitingToStart)
        {
          ArcDock arcDock2 = this.dock;
          ArcCardState? arcCardState;
          if (arcDock2 == null)
          {
            arcCardState = null;
          }
          else
          {
            ArcCardContent arcCardContent = arcDock2.ContentForKey(clickyJob.RailKey);
            arcCardState = ((arcCardContent != null) ? new ArcCardState?(arcCardContent.State) : null);
          }
          ArcCardState? arcCardState2 = arcCardState;
          ArcDock arcDock3 = this.dock;
          DateTimeOffset dateTimeOffset = ((arcDock3 != null) ? arcDock3.LatestActivity(clickyJob.RailKey) : (clickyJob.FinishedAt ?? clickyJob.CreatedAt));
          list.Add(new HeyClickyWindow.SidebarEntry(clickyJob.RailKey, HeyClickyWindow.ShortName(clickyJob.DisplayName), clickyJob.Id, null, arcCardState2, dateTimeOffset, hashSet.Contains(clickyJob.RailKey)));
        }
      }
      foreach (ClickyInfo clickyInfo in agentService.Clickys)
      {
        if (clickyInfo.ThreadId != null || agentService.LiveStatusForClicky(clickyInfo.Slug) != ClickyAppLiveStatus.Idle)
        {
          ArcDock arcDock4 = this.dock;
          ArcCardState? arcCardState3;
          if (arcDock4 == null)
          {
            arcCardState3 = null;
          }
          else
          {
            ArcCardContent arcCardContent2 = arcDock4.ContentForKey(clickyInfo.Slug);
            arcCardState3 = ((arcCardContent2 != null) ? new ArcCardState?(arcCardContent2.State) : null);
          }
          ArcCardState? arcCardState4 = arcCardState3;
          ArcDock arcDock5 = this.dock;
          DateTimeOffset dateTimeOffset2 = ((arcDock5 != null) ? arcDock5.LatestActivity(clickyInfo.Slug) : DateTimeOffset.MinValue);
          list.Add(new HeyClickyWindow.SidebarEntry(clickyInfo.Slug, HeyClickyWindow.ShortName(clickyInfo.Name), null, clickyInfo.Slug, arcCardState4, dateTimeOffset2, hashSet.Contains(clickyInfo.Slug)));
        }
      }
      return list.OrderByDescending<HeyClickyWindow.SidebarEntry, DateTimeOffset>((HeyClickyWindow.SidebarEntry e) => e.LatestAt).ToList<HeyClickyWindow.SidebarEntry>();
    }

    // Token: 0x060011CB RID: 4555 RVA: 0x00590A34 File Offset: 0x00590A34
    internal static string ShortName(string name)
    {
      string[] array = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
      if (array.Length <= 3)
      {
        return name;
      }
      string[] array2 = array.Where<string>((string w) => !HeyClickyWindow.LittleWords.Contains(w)).ToArray<string>();
      if (array2.Length == 0)
      {
        array2 = array;
      }
      return string.Join<string>(' ', array2.Take<string>(3));
    }

    // Token: 0x060011CC RID: 4556 RVA: 0x00590A94 File Offset: 0x00590A94
    private void RebuildSidebar()
    {
      HeyClickyWindow.<>c__DisplayClass123_0 CS$<>8__locals1 = new HeyClickyWindow.<>c__DisplayClass123_0();
      CS$<>8__locals1.<>4__this = this;
      if (this.sidebarRows == null)
      {
        return;
      }
      using (MessagesPalette.Use(this.Look))
      {
        HeyClickyWindow.<>c__DisplayClass123_0 CS$<>8__locals2 = CS$<>8__locals1;
        INotchHost notchHost = this.notchHost;
        NotchPresenter notchPresenter = ((notchHost != null) ? notchHost.Services.Presenter : null) as NotchPresenter;
        CS$<>8__locals2.answering = notchPresenter != null && notchPresenter.IsTextResponseInProgress;
        List<HeyClickyWindow.SidebarEntry> list = this.SidebarEntries();
        List<HeyClickyWindow.SidebarEntry> list2 = list.Where<HeyClickyWindow.SidebarEntry>(delegate(HeyClickyWindow.SidebarEntry e)
        {
          ArcCardState? state = e.State;
          if (state != null)
          {
            ArcCardState valueOrDefault = state.GetValueOrDefault();
            if (valueOrDefault <= ArcCardState.NeedsYou || valueOrDefault == ArcCardState.Pinning)
            {
              return true;
            }
          }
          return false;
        }).ToList<HeyClickyWindow.SidebarEntry>();
        List<HeyClickyWindow.SidebarEntry> list3 = (from e in list.Except<HeyClickyWindow.SidebarEntry>(list2)
          where e.OnDock
          select e).ToList<HeyClickyWindow.SidebarEntry>();
        CS$<>8__locals1.earlier = list.Except<HeyClickyWindow.SidebarEntry>(list2).Except<HeyClickyWindow.SidebarEntry>(list3).ToList<HeyClickyWindow.SidebarEntry>();
        bool flag = this.earlierUnfolded || (list2.Count == 0 && list3.Count == 0);
        CS$<>8__locals1.selected = (this.showingSettings ? "" : this.selectedKey);
        StringBuilder stringBuilder = new StringBuilder().Append(CS$<>8__locals1.answering).Append('|').Append(CS$<>8__locals1.selected)
          .Append('|')
          .Append(flag)
          .Append('|')
          .Append(CS$<>8__locals1.earlier.Count);
        foreach (ValueTuple<string, List<HeyClickyWindow.SidebarEntry>> valueTuple in new ValueTuple<string, List<HeyClickyWindow.SidebarEntry>>[]
        {
          new ValueTuple<string, List<HeyClickyWindow.SidebarEntry>>("w", list2),
          new ValueTuple<string, List<HeyClickyWindow.SidebarEntry>>("r", list3),
          new ValueTuple<string, List<HeyClickyWindow.SidebarEntry>>("e", CS$<>8__locals1.earlier)
        })
        {
          string item = valueTuple.Item1;
          foreach (HeyClickyWindow.SidebarEntry sidebarEntry in valueTuple.Item2)
          {
            stringBuilder.Append('\u001e').Append(item).Append(sidebarEntry.Key)
              .Append('\u001f')
              .Append(sidebarEntry.Name)
              .Append('\u001f')
              .Append(sidebarEntry.State);
          }
        }
        if (!(stringBuilder.ToString() == this.sidebarShape) || this.sidebarRows.Children.Count <= 0)
        {
          CS$<>8__locals1.firstBuild = this.sidebarShape.Length == 0;
          this.sidebarShape = stringBuilder.ToString();
          CS$<>8__locals1.views = new List<FrameworkElement>();
          CS$<>8__locals1.used = new HashSet<string>();
          CS$<>8__locals1.sections = new Dictionary<string, string>();
          List<FrameworkElement> views = CS$<>8__locals1.views;
          HeyClickyWindow.<>c__DisplayClass123_0 CS$<>8__locals3 = CS$<>8__locals1;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 2);
          defaultInterpolatedStringHandler.AppendLiteral("main\u001f");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(CS$<>8__locals1.answering);
          defaultInterpolatedStringHandler.AppendLiteral("\u001f");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(CS$<>8__locals1.selected == "heyclicky");
          views.Add(CS$<>8__locals3.<RebuildSidebar>g__View|2(defaultInterpolatedStringHandler.ToStringAndClear(), delegate
          {
            FrameworkElement frameworkElement = CS$<>8__locals1.<>4__this.SidebarRow("heyclicky", "HeyClicky", null, null, CS$<>8__locals1.answering ? new ArcCardState?(ArcCardState.Working) : null, true, null);
            frameworkElement.Margin = new Thickness(0.0, 8.0, 0.0, 10.0);
            return frameworkElement;
          }));
          CS$<>8__locals1.<RebuildSidebar>g__Section|5("Working", "\ue70f", 5, list2, false);
          CS$<>8__locals1.<RebuildSidebar>g__Section|5("Recents", "\ue823", 11, list3, true);
          if (CS$<>8__locals1.earlier.Count > 0)
          {
            List<FrameworkElement> views2 = CS$<>8__locals1.views;
            HeyClickyWindow.<>c__DisplayClass123_0 CS$<>8__locals4 = CS$<>8__locals1;
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 2);
            defaultInterpolatedStringHandler.AppendLiteral("earlier\u001f");
            defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.earlier.Count);
            defaultInterpolatedStringHandler.AppendLiteral("\u001f");
            defaultInterpolatedStringHandler.AppendFormatted<bool>(this.earlierUnfolded);
            views2.Add(CS$<>8__locals4.<RebuildSidebar>g__View|2(defaultInterpolatedStringHandler.ToStringAndClear(), () => CS$<>8__locals1.<>4__this.EarlierHeader(CS$<>8__locals1.earlier.Count)));
            if (flag)
            {
              foreach (HeyClickyWindow.SidebarEntry sidebarEntry2 in CS$<>8__locals1.earlier)
              {
                CS$<>8__locals1.<RebuildSidebar>g__Row|3(sidebarEntry2, "Earlier", false);
              }
            }
          }
          IEnumerable<string> keys = this.sidebarViews.Keys;
          Func<string, bool> func;
          if ((func = CS$<>8__locals1.<>9__11) == null)
          {
            func = (CS$<>8__locals1.<>9__11 = (string k) => !CS$<>8__locals1.used.Contains(k));
          }
          foreach (string text in keys.Where<string>(func).ToList<string>())
          {
            this.sidebarViews.Remove(text);
          }
          this.sidebarSections.Clear();
          foreach (KeyValuePair<string, string> keyValuePair in CS$<>8__locals1.sections)
          {
            string text2;
            string text3;
            keyValuePair.Deconstruct(out text2, out text3);
            string text4 = text2;
            string text5 = text3;
            this.sidebarSections[text4] = text5;
          }
          HeyClickyWindow.ReconcileChildren(this.sidebarRows.Children, CS$<>8__locals1.views);
        }
      }
    }

    // Token: 0x060011CD RID: 4557 RVA: 0x00591004 File Offset: 0x00591004
    private static void FadeInRow(FrameworkElement view)
    {
      if (Anim.ReduceMotion)
      {
        return;
      }
      view.Opacity = 0.0;
      Anim.Opacity(view, 1f, 0.22, Easing.EaseOut(Ui.Compositor), 0.0);
    }

    // Token: 0x060011CE RID: 4558 RVA: 0x00591044 File Offset: 0x00591044
    private static void ReconcileChildren(UIElementCollection children, List<FrameworkElement> views)
    {
      HashSet<UIElement> hashSet = new HashSet<UIElement>(views);
      for (int i = children.Count - 1; i >= 0; i--)
      {
        if (!hashSet.Contains(children[i]))
        {
          children.RemoveAt(i);
        }
      }
      for (int j = 0; j < views.Count; j++)
      {
        if (j >= children.Count || children[j] != views[j])
        {
          int num = children.IndexOf(views[j]);
          if (num >= 0)
          {
            children.RemoveAt(num);
          }
          children.Insert(j, views[j]);
        }
      }
    }

    // Token: 0x060011CF RID: 4559 RVA: 0x005910D4 File Offset: 0x005910D4
    private void MoveToEarlier(string key)
    {
      string text = "notch_peek_action";
      string text2 = null;
      string text3 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "window_sidebar_moved_to_earlier";
      jsonObject["is_job"] = key.StartsWith("job:", StringComparison.Ordinal);
      ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
      ArcDock arcDock = this.dock;
      if (arcDock != null)
      {
        arcDock.MoveToEarlier(key);
      }
      this.RebuildSidebar();
    }

    // Token: 0x060011D0 RID: 4560 RVA: 0x00591144 File Offset: 0x00591144
    private FrameworkElement SectionLabel(string title, string symbol, int seed)
    {
      MessagesPalette look = this.Look;
      TextBlock textBlock = Icons.Glyph(symbol, 9.0, look.TertiaryText);
      textBlock.FontWeight = FontWeights.Bold;
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      TextBlock textBlock2 = Ui.Text(title.ToUpperInvariant(), 10.0, FontWeights.Black, look.TertiaryText, TextAlignment.Left, 1, false, false);
      textBlock2.CharacterSpacing = 130;
      textBlock2.VerticalAlignment = VerticalAlignment.Center;
      PaperDrawing paperDrawing = new PaperDrawing
      {
        Width = 66.0,
        Height = 5.0,
        HorizontalAlignment = HorizontalAlignment.Left
      };
      paperDrawing.Stroke(ClickySketchPath.Line(new Point(0f, 2f), new Point(64.0, 2.5), (long)seed, 0.8, 8.0), new PaperStroke(look.InkAt(0.22), 1.2, PenLineCap.Round, PenLineJoin.Miter, null));
      return new StackPanel
      {
        Spacing = 3.0,
        Padding = new Thickness(10.0, 8.0, 10.0, 4.0),
        Children = 
        {
          new StackPanel
          {
            Orientation = Orientation.Horizontal,
            Spacing = 5.0,
            Children = { textBlock, textBlock2 }
          },
          paperDrawing
        }
      };
    }

    // Token: 0x060011D1 RID: 4561 RVA: 0x005912D4 File Offset: 0x005912D4
    private FrameworkElement EarlierHeader(int count)
    {
      MessagesPalette look = this.Look;
      Pressable pressable = new Pressable(true);
      pressable.Background = Ui.Transparent;
      Grid grid = new Grid
      {
        ColumnSpacing = 6.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      FrameworkElement frameworkElement = this.SectionLabel("Earlier", "\ue7b8", 19);
      frameworkElement.Margin = new Thickness(0.0, 0.0, -10.0, 0.0);
      grid.Children.Add(frameworkElement);
      TextBlock textBlock = Ui.Text(count.ToString(CultureInfo.InvariantCulture), 10.0, FontWeights.Bold, look.TertiaryText, TextAlignment.Left, 1, false, false);
      textBlock.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
      textBlock.VerticalAlignment = VerticalAlignment.Top;
      Grid.SetColumn(textBlock, 1);
      grid.Children.Add(textBlock);
      TextBlock textBlock2 = Icons.Glyph("\ue76c", 10.0, look.TertiaryText);
      textBlock2.FontWeight = FontWeights.Bold;
      textBlock2.Margin = new Thickness(0.0, 9.0, 12.0, 0.0);
      textBlock2.VerticalAlignment = VerticalAlignment.Top;
      textBlock2.RenderTransformOrigin = new Point(0.5, 0.5);
      textBlock2.RenderTransform = new RotateTransform
      {
        Angle = (double)(this.earlierUnfolded ? 90 : 0)
      };
      Grid.SetColumn(textBlock2, 3);
      grid.Children.Add(textBlock2);
      pressable.Children.Add(grid);
      pressable.Click += delegate
      {
        this.earlierUnfolded = !this.earlierUnfolded;
        this.RebuildSidebar();
      };
      pressable.Tip(this.earlierUnfolded ? "Hide earlier chats" : "Show earlier chats");
      return pressable;
    }

    // Token: 0x060011D2 RID: 4562 RVA: 0x00591530 File Offset: 0x00591530
    private FrameworkElement SidebarRow(string key, string name, [Nullable(2)] string jobId, [Nullable(2)] string clickySlug, ArcCardState? state, bool isMain = false, [Nullable(2)] Action moveToEarlier = null)
    {
      MessagesPalette m = this.Look;
      bool selected = !this.showingSettings && key == this.selectedKey;
      PaperStock paperStock = (isMain ? PaperStock.Sky : NotePaperColors.Stock((jobId != null) ? ("job:" + jobId) : ("clicky:" + clickySlug)));
      ArcPaper paper = new ArcPaper(paperStock);
      int seed = NotePaperColors.Seed(key);
      Pressable row = new Pressable(true)
      {
        Height = 38.0,
        Background = Ui.Transparent
      };
      if (selected)
      {
        row.Children.Add(HeyClickyWindow.SelectedSlip(paper, seed));
      }
      PaperSurface wash = null;
      Grid grid = new Grid
      {
        ColumnSpacing = 10.0,
        Padding = new Thickness(8.0, 0.0, 9.0, 0.0)
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      FrameworkElement frameworkElement = (isMain ? new HandoffChipView(HandoffGlyph.HeyClicky, key, 24.0, new PaperStock?(PaperStock.Sky), !selected) : new NoteChipView((jobId != null) ? ("job:" + jobId) : ("clicky:" + clickySlug), ClickyInkIcon.Inferred(name) ?? ((jobId != null) ? "checklist" : "sparkles"), seed, seed, 24.0, !selected));
      frameworkElement.VerticalAlignment = VerticalAlignment.Center;
      frameworkElement.RenderTransformOrigin = new Point(0.5, 0.5);
      CompositeTransform chipTurn = new CompositeTransform();
      frameworkElement.RenderTransform = chipTurn;
      grid.Children.Add(frameworkElement);
      TextBlock textBlock = Ui.Text(name, 13.0, selected ? FontWeights.SemiBold : FontWeights.Normal, selected ? paper.Ink : m.PrimaryText, TextAlignment.Left, 1, false, false);
      textBlock.TextTrimming = TextTrimming.CharacterEllipsis;
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      Grid.SetColumn(textBlock, 1);
      grid.Children.Add(textBlock);
      FrameworkElement frameworkElement2;
      if (state != null)
      {
        switch (state.GetValueOrDefault())
        {
        case ArcCardState.Queued:
          frameworkElement2 = ChatStamp.Create(ChatStamp.Status.Working, "queued", seed);
          goto IL_0393;
        case ArcCardState.Working:
        case ArcCardState.Pinning:
          frameworkElement2 = ChatStamp.Create(ChatStamp.Status.Working, "working", seed);
          goto IL_0393;
        case ArcCardState.NeedsYou:
          frameworkElement2 = ChatStamp.Create(ChatStamp.Status.NeedsYou, "you", seed);
          goto IL_0393;
        case ArcCardState.Failed:
          frameworkElement2 = ChatStamp.Create(ChatStamp.Status.NeedsYou, "oops", seed);
          goto IL_0393;
        case ArcCardState.New:
          frameworkElement2 = ChatStamp.Create(ChatStamp.Status.Ready, "new", seed);
          goto IL_0393;
        }
      }
      frameworkElement2 = null;
      IL_0393:
      FrameworkElement mark = frameworkElement2;
      if (mark != null)
      {
        mark.VerticalAlignment = VerticalAlignment.Center;
        Grid.SetColumn(mark, 2);
        grid.Children.Add(mark);
      }
      Pressable close = null;
      if (moveToEarlier != null)
      {
        Pressable closeButton = new Pressable(true)
        {
          Width = 18.0,
          Height = 18.0,
          CornerRadius = new CornerRadius(9.0),
          Visibility = Visibility.Collapsed,
          VerticalAlignment = VerticalAlignment.Center
        };
        TextBlock glyph = Icons.Glyph("\ue711", 9.0, m.TertiaryText);
        glyph.FontWeight = FontWeights.Bold;
        glyph.HorizontalAlignment = HorizontalAlignment.Center;
        glyph.VerticalAlignment = VerticalAlignment.Center;
        closeButton.Children.Add(glyph);
        closeButton.HoverChanged += delegate(bool h)
        {
          closeButton.Background = (h ? Ui.Brush(m.InkAt(0.12)) : Ui.Transparent);
          glyph.Foreground = Ui.Brush(h ? m.PrimaryText : m.TertiaryText);
        };
        closeButton.Click += moveToEarlier;
        AutomationProperties.SetName(closeButton, "Move to Earlier");
        Grid.SetColumn(closeButton, 2);
        grid.Children.Add(closeButton);
        close = closeButton;
      }
      row.Children.Add(grid);
      if (selected)
      {
        row.RenderTransformOrigin = new Point(0.5, 0.5);
        row.RenderTransform = new RotateTransform
        {
          Angle = -0.7
        };
      }
      double tilt = (double)(seed % 7 - 3);
      row.HoverChanged += delegate(bool h)
      {
        if (!selected)
        {
          if (h && wash == null)
          {
            wash = new PaperSurface(new ClickySketchRect(7.0, (long)(seed + 2), 0.5), PaperFill.Of(paper.TitleWash.WithAlpha(0.14)), null).WithOutline(new PaperStroke(paper.TitleWash.WithAlpha(0.35), 1.0, PenLineCap.Flat, PenLineJoin.Miter, null), false);
            row.Children.Insert(0, wash);
          }
          if (wash != null)
          {
            wash.Visibility = (h ? Visibility.Visible : Visibility.Collapsed);
          }
          chipTurn.Rotation = (h ? (tilt * -1.4) : 0.0);
          chipTurn.ScaleX = (chipTurn.ScaleY = (h ? 1.08 : 1.0));
        }
        if (close == null)
        {
          return;
        }
        close.Visibility = (h ? Visibility.Visible : Visibility.Collapsed);
        if (mark != null)
        {
          mark.Visibility = (h ? Visibility.Collapsed : Visibility.Visible);
        }
      };
      row.Click += delegate
      {
        string text = "notch_peek_action";
        string text2 = null;
        string text3 = null;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["action"] = "window_sidebar_opened";
        jsonObject["is_main"] = isMain;
        jsonObject["is_job"] = jobId != null;
        ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
        if (this.showingSettings)
        {
          this.LeaveSettings();
        }
        this.Select(key);
      };
      AutomationProperties.SetName(row, name);
      return row;
    }

    // Token: 0x060011D3 RID: 4563 RVA: 0x00591AF8 File Offset: 0x00591AF8
    private static FrameworkElement SelectedSlip(ArcPaper paper, int seed)
    {
      return new Grid
      {
        IsHitTestVisible = false,
        Children = 
        {
          new PaperSurface(new ClickySketchRect(7.0, (long)(seed + 2), 0.5), PaperFill.Of(paper.Fill), new double?(0.5)).WithOutline(new PaperStroke(paper.Deep.WithAlpha(0.4), 1.2, PenLineCap.Flat, PenLineJoin.Round, null), false).WithShadow(DS.Black(0.45), 3.0, 0.0, 1.5),
          new Border
          {
            Width = 24.0,
            Height = 8.0,
            Background = Ui.Brush(paper.TitleWash.WithAlpha(0.9)),
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(-6.0, -3.0, 0.0, 0.0),
            RenderTransformOrigin = new Point(0.5, 0.5),
            RenderTransform = new RotateTransform
            {
              Angle = -28.0
            }
          }
        }
      };
    }

    // Token: 0x060011D4 RID: 4564 RVA: 0x00591C74 File Offset: 0x00591C74
    private void RebuildAccountRow()
    {
      if (this.accountSlot == null || this.notchHost == null)
      {
        return;
      }
      using (MessagesPalette.Use(this.Look))
      {
        this.accountSlot.Child = this.AccountRow();
      }
    }

    // Token: 0x060011D5 RID: 4565 RVA: 0x00591CD0 File Offset: 0x00591CD0
    private static BitmapImage AvatarImage(Uri uri)
    {
      BitmapImage bitmapImage;
      if (!HeyClickyWindow.AvatarImages.TryGetValue(uri.AbsoluteUri, out bitmapImage))
      {
        Dictionary<string, BitmapImage> avatarImages = HeyClickyWindow.AvatarImages;
        string absoluteUri = uri.AbsoluteUri;
        BitmapImage bitmapImage2 = new BitmapImage();
        bitmapImage2.DecodePixelType = DecodePixelType.Logical;
        bitmapImage2.DecodePixelWidth = 28;
        bitmapImage2.UriSource = uri;
        bitmapImage = bitmapImage2;
        avatarImages[absoluteUri] = bitmapImage2;
      }
      return bitmapImage;
    }

    // Token: 0x060011D6 RID: 4566 RVA: 0x00591D20 File Offset: 0x00591D20
    private FrameworkElement AccountRow()
    {
      MessagesPalette m = this.Look;
      AppServices services = this.notchHost.Services;
      UserProfile user = services.Auth.User;
      string text = ((!string.IsNullOrWhiteSpace((user != null) ? user.DisplayName : null)) ? user.DisplayName.Trim() : (((user != null) ? user.Email : null) ?? "Your account"));
      char[] array = (from w in text.Split(new char[] { ' ', '@', '.' }, StringSplitOptions.RemoveEmptyEntries).Take<string>(2)
        select char.ToUpperInvariant(w[0])).ToArray<char>();
      string text2 = ((array.Length == 0) ? "?" : new string(array));
      string text4;
      if (user != null)
      {
        BillingUi billing = services.Billing;
        string text3;
        if (billing == null)
        {
          text3 = null;
        }
        else
        {
          string paidPlanName = billing.State.PaidPlanName;
          text3 = ((paidPlanName != null) ? paidPlanName.Replace("HeyClicky ", "") : null);
        }
        text4 = text3 ?? "Free";
      }
      else
      {
        text4 = "Signed out";
      }
      Grid grid = new Grid
      {
        Margin = new Thickness(8.0, 0.0, 8.0, 10.0)
      };
      grid.Children.Add(new Border
      {
        Height = 1.0,
        Background = Ui.Brush(m.InkAt(0.07)),
        Margin = new Thickness(6.0, -6.0, 6.0, 0.0),
        VerticalAlignment = VerticalAlignment.Top
      });
      Pressable row = new Pressable(true)
      {
        CornerRadius = new CornerRadius(10.0),
        Padding = new Thickness(10.0, 8.0, 10.0, 8.0)
      };
      Grid grid2 = new Grid
      {
        ColumnSpacing = 8.0
      };
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      Grid grid3 = new Grid
      {
        ColumnSpacing = 9.0
      };
      grid3.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid3.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      Grid grid4 = new Grid
      {
        Width = 28.0,
        Height = 28.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      grid4.Children.Add(new Ellipse
      {
        Fill = Ui.Brush(m.AvatarPlate)
      });
      grid4.Children.Add(Ui.Text(text2, 11.0, FontWeights.SemiBold, m.PrimaryText, TextAlignment.Left, 0, false, false).Center<TextBlock>());
      string text5 = ((user != null) ? user.AvatarUrl : null);
      Uri uri;
      if (text5 != null && text5.Length > 0 && Uri.TryCreate(text5, UriKind.Absolute, out uri))
      {
        grid4.Children.Add(new Ellipse
        {
          Fill = new ImageBrush
          {
            ImageSource = HeyClickyWindow.AvatarImage(uri),
            Stretch = Stretch.UniformToFill
          }
        });
      }
      grid3.Children.Add(grid4);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 1.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      TextBlock textBlock = Ui.Text(text, 12.5, FontWeights.SemiBold, m.PrimaryText, TextAlignment.Left, 1, false, false);
      textBlock.TextTrimming = TextTrimming.CharacterEllipsis;
      stackPanel.Children.Add(textBlock);
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 6.0,
        Height = 13.0
      };
      TextBlock textBlock2 = Ui.Text(text4, 11.0, FontWeights.Normal, m.SecondaryText, TextAlignment.Left, 1, false, false);
      textBlock2.VerticalAlignment = VerticalAlignment.Center;
      stackPanel2.Children.Add(textBlock2);
      BillingUi billing2 = services.Billing;
      BillingStateService billingStateService = ((billing2 != null) ? billing2.State : null);
      if (billingStateService != null)
      {
        BillingUsageRing billingUsageRing = BillingUsageRing.From(billingStateService.Plan, billingStateService.UsageResetsPhrase);
        if (billingUsageRing != null)
        {
          stackPanel2.Children.Add(new HeyClickyWindowUsageRing(billingUsageRing, this.palette)
          {
            Margin = new Thickness(-3.0)
          });
        }
      }
      stackPanel.Children.Add(stackPanel2);
      Grid.SetColumn(stackPanel, 1);
      grid3.Children.Add(stackPanel);
      grid2.Children.Add(grid3);
      Pressable gear = new Pressable(true)
      {
        Width = 30.0,
        Height = 30.0,
        CornerRadius = new CornerRadius(15.0),
        Background = Ui.Brush(m.InkAt(this.showingSettings ? 0.12 : 0.05))
      };
      TextBlock gearGlyph = Icons.Glyph("\ue713", 13.0, m.InkAt(this.showingSettings ? 0.95 : 0.6));
      gearGlyph.HorizontalAlignment = HorizontalAlignment.Center;
      gearGlyph.VerticalAlignment = VerticalAlignment.Center;
      gear.Children.Add(gearGlyph);
      gear.HoverChanged += delegate(bool h)
      {
        gear.Background = Ui.Brush(m.InkAt((h || this.showingSettings) ? 0.12 : 0.05));
        gearGlyph.Foreground = Ui.Brush(m.InkAt((h || this.showingSettings) ? 0.95 : 0.6));
      };
      gear.Click += delegate
      {
        this.ShowSettings(this.settingsPage);
      };
      gear.Tip("Settings");
      AutomationProperties.SetName(gear, "Settings");
      Grid.SetColumn(gear, 1);
      grid2.Children.Add(gear);
      row.Children.Add(grid2);
      row.Background = (this.showingSettings ? Ui.Brush(m.InkAt(0.09)) : Ui.Transparent);
      row.HoverChanged += delegate(bool h)
      {
        row.Background = Ui.Brush(m.InkAt(this.showingSettings ? 0.09 : (h ? 0.05 : 0.0)));
      };
      row.Click += delegate
      {
        this.ShowSettings(SettingsWindow.Page.Account);
      };
      string text6 = ((user != null) ? user.Email : null);
      if (text6 != null)
      {
        row.Tip(text6);
      }
      AutomationProperties.SetName(row, "Account and Settings");
      grid.Children.Add(row);
      return grid;
    }

    // Token: 0x060011D7 RID: 4567 RVA: 0x0059241C File Offset: 0x0059241C
    private void OnHandOffsChanged()
    {
      this.RefreshSoon();
    }

    // Token: 0x060011D8 RID: 4568 RVA: 0x00592424 File Offset: 0x00592424
    private void OnAgentsChanged([Nullable(2)] object sender, EventArgs e)
    {
      this.RefreshSoon();
    }

    // Token: 0x060011D9 RID: 4569 RVA: 0x0059242C File Offset: 0x0059242C
    private void OnDockContentChanged()
    {
      this.RefreshSoon();
    }

    // Token: 0x060011DA RID: 4570 RVA: 0x00592434 File Offset: 0x00592434
    private void RefreshSoon()
    {
      Window window = this.window;
      if (window == null || this.refreshQueued)
      {
        return;
      }
      this.refreshQueued = true;
      window.DispatcherQueue.TryEnqueue(delegate
      {
        this.refreshQueued = false;
        if (this.window == null)
        {
          return;
        }
        this.RebuildSidebar();
        if (this.showingSettings || this.selectedKey == "heyclicky")
        {
          return;
        }
        HeyClickyWindowConversation heyClickyWindowConversation = this.shownChild as HeyClickyWindowConversation;
        if (heyClickyWindowConversation != null)
        {
          heyClickyWindowConversation.Refresh();
          this.SyncComposerStop(heyClickyWindowConversation);
        }
        this.ShowColumnHeader();
      });
    }

    // Token: 0x060011DB RID: 4571 RVA: 0x00592474 File Offset: 0x00592474
    private void OnPresentationChanged([Nullable(2)] object sender, EventArgs e)
    {
      Window window = this.window;
      if (window == null)
      {
        return;
      }
      window.DispatcherQueue.TryEnqueue(delegate
      {
        INotchHost notchHost = this.notchHost;
        NotchPresenter notchPresenter = ((notchHost != null) ? notchHost.Services.Presenter : null) as NotchPresenter;
        bool flag = notchPresenter != null && notchPresenter.IsTextResponseInProgress;
        if (flag == this.wasAnswering)
        {
          return;
        }
        this.wasAnswering = flag;
        this.RebuildSidebar();
        if (!this.showingSettings && this.selectedKey == "heyclicky")
        {
          this.ShowColumnHeader();
        }
      });
    }

    // Token: 0x060011DC RID: 4572 RVA: 0x00592498 File Offset: 0x00592498
    [NullableContext(2)]
    private bool KeyStillExists(string key)
    {
      if (key == null || key.Length == 0)
      {
        return false;
      }
      if (key == "heyclicky")
      {
        return true;
      }
      INotchHost notchHost = this.notchHost;
      AgentService agentService = ((notchHost != null) ? notchHost.Services.Agents : null) as AgentService;
      if (agentService == null)
      {
        return true;
      }
      string text = ClickyJob.JobIdFromRailKey(key);
      if (text != null)
      {
        ClickyJob clickyJob = agentService.Jobs.Job(text);
        return clickyJob != null && !(clickyJob.IsArchived ?? false);
      }
      return agentService.FindClicky(key) != null;
    }

    // Token: 0x060011DD RID: 4573 RVA: 0x00592530 File Offset: 0x00592530
    private void Select(string key)
    {
      if (this.mainColumn == null || this.host == null)
      {
        return;
      }
      using (MessagesPalette.Use(this.Look))
      {
        bool flag = key != this.selectedKey || this.shownChild == null;
        if (key != this.selectedKey && this.selectedKey.Length > 0)
        {
          this.CloseDocument();
        }
        this.selectedKey = key;
        INotchHost notchHost = this.notchHost;
        NotchPresenter notchPresenter = ((notchHost != null) ? notchHost.Services.Presenter : null) as NotchPresenter;
        if (key == "heyclicky")
        {
          this.shownAgentKey = null;
          if (this.mainChat == null)
          {
            this.mainChat = new NotchChatView(this.host, true);
          }
          if (flag)
          {
            this.Swap(this.mainChat);
          }
          if (notchPresenter != null)
          {
            notchPresenter.SetWindowShowsMainThread(true);
          }
        }
        else
        {
          this.shownAgentKey = key;
          if (notchPresenter != null)
          {
            notchPresenter.SetWindowShowsMainThread(false);
          }
          if (flag)
          {
            HeyClickyWindowConversation heyClickyWindowConversation = new HeyClickyWindowConversation(this.host, key);
            this.Swap(heyClickyWindowConversation);
            this.SyncComposerStop(heyClickyWindowConversation);
            this.MarkRead(key);
          }
        }
        if (this.chatDots != null)
        {
          this.chatDots.Visibility = Visibility.Visible;
        }
        this.LayOutCards(true);
        this.ShowColumnHeader();
        this.RebuildSidebar();
      }
    }

    // Token: 0x060011DE RID: 4574 RVA: 0x00592694 File Offset: 0x00592694
    private void MarkRead(string key)
    {
      INotchHost notchHost = this.notchHost;
      AgentService agentService = ((notchHost != null) ? notchHost.Services.Agents : null) as AgentService;
      if (agentService == null)
      {
        return;
      }
      string text = ClickyJob.JobIdFromRailKey(key);
      if (text != null)
      {
        agentService.Jobs.MarkSeen(text);
        return;
      }
      if (agentService.FindClicky(key) != null)
      {
        ClickyReadMarks.MarkRead(key);
      }
    }

    // Token: 0x060011DF RID: 4575 RVA: 0x005926E8 File Offset: 0x005926E8
    private string AgentName(string key)
    {
      INotchHost notchHost = this.notchHost;
      AgentService agentService = ((notchHost != null) ? notchHost.Services.Agents : null) as AgentService;
      if (agentService == null)
      {
        return key;
      }
      string text = ClickyJob.JobIdFromRailKey(key);
      if (text != null)
      {
        ClickyJob clickyJob = agentService.Jobs.Job(text);
        return ((clickyJob != null) ? clickyJob.DisplayName : null) ?? "Job";
      }
      ClickyInfo clickyInfo = agentService.FindClicky(key);
      return ((clickyInfo != null) ? clickyInfo.Name : null) ?? key;
    }

    // Token: 0x060011E0 RID: 4576 RVA: 0x0059275C File Offset: 0x0059275C
    private unsafe void ShowColumnHeader()
    {
      if (this.columnHeader == null)
      {
        return;
      }
      using (MessagesPalette.Use(this.Look))
      {
        MessagesPalette look = this.Look;
        INotchHost notchHost = this.notchHost;
        NotchPresenter notchPresenter = ((notchHost != null) ? notchHost.Services.Presenter : null) as NotchPresenter;
        bool flag = notchPresenter != null && notchPresenter.IsTextResponseInProgress;
        string text = ((this.showingSettings || this.selectedKey == "heyclicky") ? "" : this.AgentName(this.selectedKey));
        ArcDock arcDock = this.dock;
        ArcCardState? arcCardState;
        if (arcDock == null)
        {
          arcCardState = null;
        }
        else
        {
          ArcCardContent arcCardContent = arcDock.ContentForKey(this.selectedKey);
          arcCardState = ((arcCardContent != null) ? new ArcCardState?(arcCardContent.State) : null);
        }
        ArcCardState? arcCardState2 = arcCardState;
        ArcCardState valueOrDefault = arcCardState2.GetValueOrDefault(ArcCardState.Idle);
        string text3;
        if (!this.showingSettings)
        {
          if (!(this.selectedKey == "heyclicky"))
          {
            string text2 = "\u001f";
            <>y__InlineArray4<object> <>y__InlineArray = default(<>y__InlineArray4<object>);
            *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<object>, object>(ref <>y__InlineArray, 0) = "agent";
            *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<object>, object>(ref <>y__InlineArray, 1) = this.selectedKey;
            *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<object>, object>(ref <>y__InlineArray, 2) = text;
            *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<object>, object>(ref <>y__InlineArray, 3) = valueOrDefault;
            text3 = string.Join(text2, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray4<object>, object>(in <>y__InlineArray, 4));
          }
          else
          {
            text3 = "main\u001f" + flag.ToString();
          }
        }
        else
        {
          text3 = "settings";
        }
        string text4 = text3;
        if (!(text4 == this.shownHeaderShape) || this.columnHeader.Child == null)
        {
          this.shownHeaderShape = text4;
          FrameworkElement frameworkElement;
          if (this.showingSettings)
          {
            frameworkElement = HeyClickyWindow.SettingsHeader();
          }
          else if (this.selectedKey == "heyclicky")
          {
            frameworkElement = HeyClickyWindow.ColumnHeader(new HandoffChipView(HandoffGlyph.HeyClicky, "heyclicky", 22.0, new PaperStock?(PaperStock.Sky), true), "HeyClicky", flag ? "Answering…" : "Your chat", look.SecondaryText);
          }
          else
          {
            ValueTuple<uint, double> valueTuple = ArcCardStates.Color(valueOrDefault);
            uint item = valueTuple.Item1;
            double item2 = valueTuple.Item2;
            bool flag2 = ArcCardStates.IsBusy(valueOrDefault);
            if (!flag2)
            {
              bool flag3 = valueOrDefault - ArcCardState.NeedsYou <= 1;
              flag2 = flag3;
            }
            bool flag4 = flag2;
            int num = NotePaperColors.Seed(this.selectedKey);
            frameworkElement = HeyClickyWindow.ColumnHeader(new NoteChipView(NotePaperColors.Key(this.selectedKey), ClickyInkIcon.Inferred(text) ?? "sparkles", num, num, 22.0, true), text, ArcCardStates.Title(valueOrDefault), flag4 ? DS.Hex(item, item2) : look.SecondaryText);
          }
          frameworkElement.Margin = this.ColumnHeaderInsets();
          Grid grid = new Grid
          {
            Height = 38.0
          };
          grid.Children.Add(frameworkElement);
          grid.Children.Add(new Border
          {
            Height = 1.0,
            VerticalAlignment = VerticalAlignment.Bottom,
            Background = Ui.Brush(look.InkAt(0.07)),
            IsHitTestVisible = false
          });
          this.columnHeader.Child = grid;
          this.headerChild = frameworkElement;
        }
      }
    }

    // Token: 0x060011E1 RID: 4577 RVA: 0x00592A8C File Offset: 0x00592A8C
    private void SyncComposerStop(HeyClickyWindowConversation conversation)
    {
      string agentKey = conversation.AgentKey;
      ArcDock arcDock = this.dock;
      ArcCardContent arcCardContent = ((arcDock != null) ? arcDock.ContentForKey(agentKey) : null);
      conversation.SetStopAction((arcCardContent != null && !arcCardContent.IsFinalizing && arcCardContent.TaskIsRunning) ? delegate
      {
        ArcDock arcDock2 = this.dock;
        ArcCardContent arcCardContent2 = ((arcDock2 != null) ? arcDock2.ContentForKey(agentKey) : null);
        if (arcCardContent2 == null || !arcCardContent2.TaskIsRunning)
        {
          return;
        }
        string text = "notch_peek_action";
        string text2 = null;
        string text3 = null;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["action"] = "window_agent_stopped";
        ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
        this.dock.StopWork(arcCardContent2);
      } : null);
    }

    // Token: 0x060011E2 RID: 4578 RVA: 0x00592AF8 File Offset: 0x00592AF8
    private static FrameworkElement ColumnHeader(FrameworkElement chip, string title, string subtitle, Color subtitleColor)
    {
      MessagesPalette messagesPalette = MessagesPalette.Current;
      chip.VerticalAlignment = VerticalAlignment.Center;
      TextBlock textBlock = Ui.Text(title, 13.0, FontWeights.SemiBold, messagesPalette.PrimaryText, TextAlignment.Left, 1, false, false);
      textBlock.TextTrimming = TextTrimming.CharacterEllipsis;
      TextBlock textBlock2 = Ui.Text(subtitle, 11.0, FontWeights.Medium, subtitleColor, TextAlignment.Left, 1, false, false);
      textBlock2.TextTrimming = TextTrimming.CharacterEllipsis;
      Grid grid = new Grid();
      grid.ColumnSpacing = 10.0;
      grid.IsHitTestVisible = false;
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid.Children.Add(chip);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 1.0,
        VerticalAlignment = VerticalAlignment.Center,
        Children = { textBlock, textBlock2 }
      };
      Grid.SetColumn(stackPanel, 1);
      grid.Children.Add(stackPanel);
      return grid;
    }

    // Token: 0x060011E3 RID: 4579 RVA: 0x00592C08 File Offset: 0x00592C08
    private static FrameworkElement SettingsHeader()
    {
      TextBlock textBlock = Ui.Text("Settings", 14.0, FontWeights.SemiBold, MessagesPalette.Current.PrimaryText, TextAlignment.Left, 1, false, false);
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      textBlock.Margin = new Thickness(261.0, 0.0, 0.0, 0.0);
      return new Grid
      {
        IsHitTestVisible = false,
        Children = { textBlock }
      };
    }

    // Token: 0x060011E4 RID: 4580 RVA: 0x00592C8C File Offset: 0x00592C8C
    private void Swap(FrameworkElement next)
    {
      if (this.stage == null || this.shownChild == next)
      {
        return;
      }
      FrameworkElement old = this.shownChild;
      SettingsContent settingsContent = old as SettingsContent;
      if (settingsContent != null)
      {
        settingsContent.Hidden();
      }
      bool flag = this.FocusIsWithin(old);
      this.shownChild = next;
      if (!this.stage.Children.Contains(next))
      {
        next.Opacity = Anim.ReduceMotion > false;
        this.stage.Children.Add(next);
      }
      next.IsHitTestVisible = true;
      if (flag)
      {
        this.ParkFocus(true);
      }
      if (old != null)
      {
        old.IsHitTestVisible = false;
        if (Anim.ReduceMotion)
        {
          this.stage.Children.Remove(old);
        }
        else
        {
          Anim.Opacity(old, 0f, 0.16, Easing.EaseOut(Ui.Compositor), 0.0);
          Grid leaving = this.stage;
          Microsoft.UI.Dispatching.DispatcherQueueTimer t = leaving.DispatcherQueue.CreateTimer().Rooted();
          t.Interval = TimeSpan.FromSeconds(0.18);
          t.IsRepeating = false;
          t.Tick += delegate(Microsoft.UI.Dispatching.DispatcherQueueTimer _, object _)
          {
            t.Stop();
            if (old != this.shownChild)
            {
              leaving.Children.Remove(old);
            }
          };
          t.Start();
        }
      }
      if (!Anim.ReduceMotion)
      {
        Anim.Opacity(next, 1f, 0.16, Easing.EaseOut(Ui.Compositor), 0.0);
      }
    }

    // Token: 0x060011E5 RID: 4581 RVA: 0x00592E34 File Offset: 0x00592E34
    private void ShowSettings()
    {
      this.ShowSettings(SettingsWindow.Page.Account);
    }

    // Token: 0x060011E6 RID: 4582 RVA: 0x00592E40 File Offset: 0x00592E40
    private void ShowSettings(SettingsWindow.Page page)
    {
      if (this.notchHost == null || this.mainColumn == null)
      {
        return;
      }
      this.CloseDocument();
      this.settingsPage = page;
      using (MessagesPalette.Use(this.Look))
      {
        if (!this.showingSettings)
        {
          this.keyBeforeSettings = this.selectedKey;
        }
        this.showingSettings = true;
        this.shownAgentKey = null;
        NotchPresenter notchPresenter = this.notchHost.Services.Presenter as NotchPresenter;
        if (notchPresenter != null)
        {
          notchPresenter.SetWindowShowsMainThread(false);
        }
        if (this.settings == null)
        {
          this.settings = new SettingsContent(this.notchHost.Services, this.notchHost.Actions, null)
          {
            Margin = new Thickness(0.0, 6.0, 12.0, 0.0)
          };
          this.settings.SidebarWidth = 224.0;
          this.settings.UseNotchGrid(12.0, 12.0);
          this.settings.PageChanged += delegate(SettingsWindow.Page p)
          {
            this.settingsPage = p;
          };
        }
        this.settings.IsSurfaceSettled = true;
        this.Swap(this.settings);
        this.settings.Show(new SettingsWindow.Page?(page));
        if (this.chatDots != null)
        {
          this.chatDots.Visibility = Visibility.Collapsed;
        }
        this.LayOutCards(true);
        this.ShowColumnHeader();
        this.RebuildAccountRow();
        string text = "notch_peek_action";
        string text2 = null;
        string text3 = null;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["action"] = "window_settings_opened";
        ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
      }
    }

    // Token: 0x060011E7 RID: 4583 RVA: 0x0059300C File Offset: 0x0059300C
    private void LeaveSettings()
    {
      if (!this.showingSettings)
      {
        return;
      }
      this.showingSettings = false;
      SettingsContent settingsContent = this.settings;
      if (settingsContent != null)
      {
        settingsContent.Hidden();
      }
      string text = (this.KeyStillExists(this.keyBeforeSettings) ? this.keyBeforeSettings : "heyclicky");
      this.selectedKey = "";
      this.Select(text);
      this.RebuildAccountRow();
    }

    // Token: 0x060011E8 RID: 4584 RVA: 0x00593070 File Offset: 0x00593070
    private Grid BuildDocumentColumn()
    {
      Grid grid3;
      using (MessagesPalette.Use(this.Look))
      {
        MessagesPalette look = this.Look;
        Grid grid = new Grid();
        grid.Margin = new Thickness(0.0, 36.0, 8.0, 8.0);
        Grid grid2 = new Grid();
        grid2.RowDefinitions.Add(new RowDefinition
        {
          Height = new GridLength(32.0)
        });
        grid2.RowDefinitions.Add(new RowDefinition
        {
          Height = new GridLength(1.0, GridUnitType.Star)
        });
        this.documentHeader = new Border();
        grid2.Children.Add(this.documentHeader);
        grid2.Children.Add(new Border
        {
          Height = 1.0,
          VerticalAlignment = VerticalAlignment.Bottom,
          Background = Ui.Brush(look.InkAt(0.07)),
          IsHitTestVisible = false
        });
        this.documentViewer = new NotchDocumentViewer(this.host, false);
        Grid.SetRow(this.documentViewer, 1);
        grid2.Children.Add(this.documentViewer);
        Border border = new Border
        {
          Background = Ui.Brush(look.SidebarFill),
          CornerRadius = new CornerRadius(12.0),
          BorderBrush = Ui.Brush(look.InkAt(0.08)),
          BorderThickness = new Thickness(1.0),
          Child = grid2,
          HorizontalAlignment = HorizontalAlignment.Left
        };
        Ui.ClipRounded(border, 12.0);
        grid.Children.Add(border);
        this.documentPlate = border;
        HeyClickyWindow.DocumentResizeHandle documentResizeHandle = new HeyClickyWindow.DocumentResizeHandle(this, look)
        {
          Width = 12.0,
          HorizontalAlignment = HorizontalAlignment.Left,
          Margin = new Thickness(-10.0, 0.0, 0.0, 0.0)
        };
        grid.Children.Add(documentResizeHandle);
        grid3 = grid;
      }
      return grid3;
    }

    // Token: 0x060011E9 RID: 4585 RVA: 0x005932BC File Offset: 0x005932BC
    private FrameworkElement DocumentHeader(string path)
    {
      FrameworkElement frameworkElement;
      using (MessagesPalette.Use(this.Look))
      {
        MessagesPalette look = this.Look;
        NotchDocumentStyle notchDocumentStyle = NotchDocumentStyle.For(path, false);
        Grid grid = new Grid
        {
          ColumnSpacing = 8.0,
          Padding = new Thickness(12.0, 0.0, 12.0, 0.0)
        };
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = new GridLength(1.0, GridUnitType.Star)
        });
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
        TextBlock textBlock = Icons.Glyph(notchDocumentStyle.Glyph, 9.5, Colors.White);
        textBlock.FontWeight = FontWeights.SemiBold;
        textBlock.HorizontalAlignment = HorizontalAlignment.Center;
        textBlock.VerticalAlignment = VerticalAlignment.Center;
        grid.Children.Add(new Border
        {
          Width = 18.0,
          Height = 18.0,
          CornerRadius = new CornerRadius(5.0),
          Background = Ui.Brush(notchDocumentStyle.Tint),
          Child = textBlock,
          VerticalAlignment = VerticalAlignment.Center
        });
        TextBlock textBlock2 = Ui.Text(Path.GetFileName(path), 12.5, FontWeights.SemiBold, look.PrimaryText, TextAlignment.Left, 1, false, false);
        textBlock2.TextTrimming = TextTrimming.CharacterEllipsis;
        textBlock2.VerticalAlignment = VerticalAlignment.Center;
        ToolTipService.SetToolTip(textBlock2, path);
        TextBlock textBlock3 = Ui.Text(notchDocumentStyle.KindLabel, 11.5, FontWeights.Normal, look.SecondaryText, TextAlignment.Left, 1, false, false);
        textBlock3.VerticalAlignment = VerticalAlignment.Center;
        StackPanel stackPanel = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 8.0,
          Children = { textBlock2, textBlock3 }
        };
        Grid.SetColumn(stackPanel, 1);
        grid.Children.Add(stackPanel);
        this.headerButtons.RemoveAll((FrameworkElement b) => b.Tag as string == "document");
        StackPanel stackPanel2 = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 8.0,
          VerticalAlignment = VerticalAlignment.Center
        };
        foreach (ValueTuple<string, string, Action> valueTuple in new ValueTuple<string, string, Action>[]
        {
          new ValueTuple<string, string, Action>("\ue8a7", "Open in its app", delegate
          {
            string text = this.documentPath;
            if (text != null)
            {
              INotchHost notchHost = this.notchHost;
              INotchActions notchActions = ((notchHost != null) ? notchHost.Actions : null) as INotchActions;
              if (notchActions == null)
              {
                return;
              }
              notchActions.OpenInItsApp(text, this.documentThreadId);
            }
          }),
          new ValueTuple<string, string, Action>("\ue8b7", "Show in Explorer", delegate
          {
            string text2 = this.documentPath;
            if (text2 != null)
            {
              INotchHost notchHost2 = this.notchHost;
              INotchActions notchActions2 = ((notchHost2 != null) ? notchHost2.Actions : null) as INotchActions;
              if (notchActions2 == null)
              {
                return;
              }
              notchActions2.RevealInExplorer(text2);
            }
          }),
          new ValueTuple<string, string, Action>("\ue711", "Close (esc)", new Action(this.CloseDocument))
        })
        {
          string item = valueTuple.Item1;
          string item2 = valueTuple.Item2;
          Action item3 = valueTuple.Item3;
          Pressable pressable = this.HeaderButton(item, item2, item3);
          pressable.Tag = "document";
          stackPanel2.Children.Add(pressable);
        }
        Grid.SetColumn(stackPanel2, 2);
        grid.Children.Add(stackPanel2);
        frameworkElement = grid;
      }
      return frameworkElement;
    }

    // Token: 0x060011EA RID: 4586 RVA: 0x00593630 File Offset: 0x00593630
    public void ShowDocument(string path, [Nullable(2)] string threadId)
    {
      if (this.documentColumn == null || this.documentDefinition == null || this.documentViewer == null)
      {
        return;
      }
      using (MessagesPalette.Use(this.Look))
      {
        bool flag = this.documentPath != null;
        this.documentPath = path;
        this.documentThreadId = threadId;
        if (this.documentHeader != null)
        {
          this.documentHeader.Child = this.DocumentHeader(path);
        }
        this.documentViewer.Show(null, null);
        this.documentViewer.Show(path, threadId);
        this.documentViewerHolds = true;
        this.LayOutCards(true);
        if (!flag)
        {
          this.WidenForDocument();
        }
        string text = "notch_peek_action";
        string text2 = null;
        string text3 = null;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["action"] = "window_document_opened";
        ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
      }
    }

    // Token: 0x060011EB RID: 4587 RVA: 0x00593718 File Offset: 0x00593718
    public void CloseDocument()
    {
      if (this.documentPath == null || this.documentColumn == null || this.documentDefinition == null)
      {
        return;
      }
      this.documentPath = null;
      this.documentThreadId = null;
      this.LayOutCards(true);
    }

    // Token: 0x060011EC RID: 4588 RVA: 0x00593748 File Offset: 0x00593748
    private double ClampDocumentWidth(double width)
    {
      double num;
      if (!this.windowMoving || this.windowMoveToContentWidth <= 0.0)
      {
        Grid grid = this.root;
        num = ((grid != null && grid.ActualWidth > 0.0) ? grid.ActualWidth : 1100.0);
      }
      else
      {
        num = this.windowMoveToContentWidth;
      }
      double num2 = num;
      double num3 = (this.SidebarVisible ? 244.0 : 0.0);
      double num4 = Math.Max(340.0, num2 - num3 - 380.0 - 24.0);
      return Math.Min(Math.Max(width, 340.0), num4);
    }

    // Token: 0x060011ED RID: 4589 RVA: 0x005937FC File Offset: 0x005937FC
    private void ResizeDocument(double width)
    {
      if (this.documentDefinition == null)
      {
        return;
      }
      HeyClickyWindow.State state = this.saved.<Clone>$();
      state.DocumentWidth = this.ClampDocumentWidth(width);
      this.saved = state;
      if (!this.columnFramesRunning)
      {
        this.ApplyColumnFrame();
      }
    }

    // Token: 0x060011EE RID: 4590 RVA: 0x00593834 File Offset: 0x00593834
    private void WidenForDocument()
    {
      if (this.appWindow != null)
      {
        Grid grid = this.root;
        if (grid != null && grid.ActualWidth > 0.0)
        {
          XamlRoot xamlRoot = grid.XamlRoot;
          if (xamlRoot != null && !HeyClickyWindowOpeningFlight.IsFlying)
          {
            OverlappedPresenter overlappedPresenter = this.appWindow.Presenter as OverlappedPresenter;
            if (overlappedPresenter == null || overlappedPresenter.State != OverlappedPresenterState.Restored)
            {
              return;
            }
            double rasterizationScale = xamlRoot.RasterizationScale;
            double num = (this.SidebarVisible ? 244.0 : 0.0) + 380.0 + Math.Max(340.0, this.saved.DocumentWidth) + 24.0;
            RectInt32 rectInt = new RectInt32(this.appWindow.Position.X, this.appWindow.Position.Y, this.appWindow.Size.Width, this.appWindow.Size.Height);
            int num2 = Math.Max(0, rectInt.Width - (int)Math.Round(this.root.ActualWidth * rasterizationScale));
            int num3 = (int)Math.Ceiling(num * rasterizationScale) + num2;
            if (rectInt.Width >= num3)
            {
              return;
            }
            Native.RECT work = Displays.ForWindow(this.hwnd).Work;
            RectInt32 rectInt2 = rectInt;
            rectInt2.Width = Math.Min(num3, work.Width);
            RectInt32 rectInt3 = rectInt2;
            if (!OverlayWindow.Offscreen && rectInt3.X + rectInt3.Width > work.Right)
            {
              rectInt3.X = Math.Max(work.Left, work.Right - rectInt3.Width);
            }
            this.windowMoveToContentWidth = (double)(rectInt3.Width - num2) / rasterizationScale;
            this.MoveWindow(rectInt3, 0.3);
            return;
          }
        }
      }
    }

    // Token: 0x060011EF RID: 4591 RVA: 0x00593A04 File Offset: 0x00593A04
    private void MoveWindow(RectInt32 to, double seconds)
    {
      if (this.appWindow == null)
      {
        return;
      }
      if (Anim.ReduceMotion || seconds <= 0.0)
      {
        this.appWindow.MoveAndResize(to);
        return;
      }
      this.windowMoveFrom = new RectInt32(this.appWindow.Position.X, this.appWindow.Position.Y, this.appWindow.Size.Width, this.appWindow.Size.Height);
      this.windowMoveTo = to;
      this.windowMoveStartedAt = SpringCurve.Now;
      this.windowMoveSeconds = seconds;
      if (this.windowMoving)
      {
        return;
      }
      this.windowMoving = true;
      CompositionTarget.Rendering += this.OnWindowMoveFrame;
    }

    // Token: 0x060011F0 RID: 4592 RVA: 0x00593AC0 File Offset: 0x00593AC0
    private void OnWindowMoveFrame([Nullable(2)] object sender, object e)
    {
      double num = ((this.appWindow == null) ? 1.0 : Math.Clamp((SpringCurve.Now - this.windowMoveStartedAt) / this.windowMoveSeconds, 0.0, 1.0));
      if (this.appWindow != null)
      {
        HeyClickyWindow.<>c__DisplayClass176_0 CS$<>8__locals1;
        CS$<>8__locals1.p = HeyClickyWindow.TimingCurve(0.2, 0.9, 0.25, 1.0, num);
        this.appWindow.MoveAndResize(new RectInt32(HeyClickyWindow.<OnWindowMoveFrame>g__Lerp|176_0(this.windowMoveFrom.X, this.windowMoveTo.X, ref CS$<>8__locals1), HeyClickyWindow.<OnWindowMoveFrame>g__Lerp|176_0(this.windowMoveFrom.Y, this.windowMoveTo.Y, ref CS$<>8__locals1), HeyClickyWindow.<OnWindowMoveFrame>g__Lerp|176_0(this.windowMoveFrom.Width, this.windowMoveTo.Width, ref CS$<>8__locals1), HeyClickyWindow.<OnWindowMoveFrame>g__Lerp|176_0(this.windowMoveFrom.Height, this.windowMoveTo.Height, ref CS$<>8__locals1)));
      }
      if (num < 1.0)
      {
        return;
      }
      CompositionTarget.Rendering -= this.OnWindowMoveFrame;
      this.windowMoving = false;
      this.windowMoveToContentWidth = 0.0;
      if (!this.columnFramesRunning)
      {
        this.ApplyColumnFrame();
      }
    }

    // Token: 0x060011F1 RID: 4593 RVA: 0x00593C10 File Offset: 0x00593C10
    internal static double TimingCurve(double x1, double y1, double x2, double y2, double t)
    {
      if (t <= 0.0)
      {
        return 0.0;
      }
      if (t >= 1.0)
      {
        return 1.0;
      }
      double num = 0.0;
      double num2 = 1.0;
      double num3 = t;
      for (int i = 0; i < 40; i++)
      {
        num3 = (num + num2) / 2.0;
        if (HeyClickyWindow.<TimingCurve>g__Bezier|177_0(x1, x2, num3) < t)
        {
          num = num3;
        }
        else
        {
          num2 = num3;
        }
      }
      return HeyClickyWindow.<TimingCurve>g__Bezier|177_0(y1, y2, num3);
    }

    // Token: 0x060011F2 RID: 4594 RVA: 0x00593C98 File Offset: 0x00593C98
    [return: Nullable(2)]
    public static string AgentKeyForEntry(ChatHandOff entry)
    {
      string jobId = entry.JobId;
      if (jobId == null)
      {
        return entry.ClickySlug;
      }
      return "job:" + jobId;
    }

    // Token: 0x060011F3 RID: 4595 RVA: 0x00593CC4 File Offset: 0x00593CC4
    internal void OpenAgentForEntry(string entryId)
    {
      ChatHandOffs handOffs = NotchChat.HandOffs;
      ChatHandOff chatHandOff = ((handOffs != null) ? handOffs.Entry(entryId) : null);
      if (chatHandOff != null)
      {
        string text = HeyClickyWindow.AgentKeyForEntry(chatHandOff);
        if (text != null)
        {
          if (this.showingSettings)
          {
            this.LeaveSettings();
          }
          this.Select(text);
          return;
        }
      }
    }

    // Token: 0x060011F4 RID: 4596 RVA: 0x00593D08 File Offset: 0x00593D08
    private void PlaceFromSavedFrame()
    {
      if (this.appWindow == null)
      {
        return;
      }
      Displays.Monitor monitor = Displays.ForWindow(this.notchHost.WindowHandle);
      Native.RECT work = monitor.Work;
      Displays.Monitor monitor2 = Displays.At(this.saved.X + 40, this.saved.Y + 20);
      Native.RECT work2 = monitor2.Work;
      if (OverlayWindow.Offscreen || this.saved.Width <= 0 || this.saved.Height <= 0 || work2.Left > this.saved.X + 40 || this.saved.X + 40 >= work2.Left + work2.Width || work2.Top > this.saved.Y + 20 || this.saved.Y + 20 >= work2.Top + work2.Height)
      {
        int num = (int)Math.Min((double)work.Width * 0.9, 1100.0 * monitor.Scale);
        int num2 = (int)Math.Min((double)work.Height * 0.9, 760.0 * monitor.Scale);
        int num3 = (OverlayWindow.Offscreen ? (-24000) : 0);
        this.appWindow.MoveAndResize(new RectInt32(work.Left + (work.Width - num) / 2 + num3, work.Top + (int)(40.0 * monitor.Scale) + num3, num, num2));
        return;
      }
      if (monitor2.Handle == monitor.Handle)
      {
        this.appWindow.MoveAndResize(new RectInt32(this.saved.X, this.saved.Y, this.saved.Width, this.saved.Height));
        return;
      }
      double num4 = monitor.Scale / Math.Max(0.5, monitor2.Scale);
      int num5 = (int)Math.Min((double)this.saved.Width * num4, (double)work.Width);
      int num6 = (int)Math.Min((double)this.saved.Height * num4, (double)work.Height);
      double num7 = Math.Clamp((double)(this.saved.X - work2.Left) / (double)Math.Max(1, work2.Width - this.saved.Width), 0.0, 1.0);
      double num8 = Math.Clamp((double)(this.saved.Y - work2.Top) / (double)Math.Max(1, work2.Height - this.saved.Height), 0.0, 1.0);
      this.appWindow.MoveAndResize(new RectInt32(work.Left + (int)(num7 * (double)(work.Width - num5)), work.Top + (int)(num8 * (double)(work.Height - num6)), num5, num6));
    }

    // Token: 0x060011F5 RID: 4597 RVA: 0x00594028 File Offset: 0x00594028
    private void SaveFrameSoon()
    {
      if (this.window == null)
      {
        return;
      }
      if (this.saveTimer == null)
      {
        this.saveTimer = new HeyClickyWindow.DispatcherQueueTimerHolder(this.window.DispatcherQueue, new Action(this.SaveFrame));
      }
      this.saveTimer.Restart(TimeSpan.FromSeconds(0.6));
    }

    // Token: 0x060011F6 RID: 4598 RVA: 0x00594084 File Offset: 0x00594084
    private void SaveFrame()
    {
      if (this.appWindow != null && !OverlayWindow.Offscreen)
      {
        OverlappedPresenter overlappedPresenter = this.appWindow.Presenter as OverlappedPresenter;
        if (overlappedPresenter != null && overlappedPresenter.State == OverlappedPresenterState.Restored)
        {
          HeyClickyWindow.State state = this.saved.<Clone>$();
          state.X = this.appWindow.Position.X;
          state.Y = this.appWindow.Position.Y;
          state.Width = this.appWindow.Size.Width;
          state.Height = this.appWindow.Size.Height;
          this.saved = state;
        }
      }
      this.saved.Save();
    }

    // Token: 0x060011FD RID: 4605 RVA: 0x00594310 File Offset: 0x00594310
    [CompilerGenerated]
    private bool <BuildContent>g__LeaveTextInput|82_3()
    {
      if (!this.TextInputHasFocus())
      {
        return false;
      }
      this.ParkFocus(true);
      return true;
    }

    // Token: 0x06001206 RID: 4614 RVA: 0x005944AC File Offset: 0x005944AC
    [CompilerGenerated]
    internal static int <OnWindowMoveFrame>g__Lerp|176_0(int a, int b, ref HeyClickyWindow.<>c__DisplayClass176_0 A_2)
    {
      return (int)Math.Round((double)a + (double)(b - a) * A_2.p);
    }

    // Token: 0x06001207 RID: 4615 RVA: 0x005944C4 File Offset: 0x005944C4
    [CompilerGenerated]
    internal static double <TimingCurve>g__Bezier|177_0(double a, double b, double s)
    {
      return 3.0 * a * s * (1.0 - s) * (1.0 - s) + 3.0 * b * s * s * (1.0 - s) + s * s * s;
    }

    // Token: 0x040007E1 RID: 2017
    public const string MainThreadKey = "heyclicky";

    // Token: 0x040007E2 RID: 2018
    [Nullable(2)]
    private INotchHost notchHost;

    // Token: 0x040007E3 RID: 2019
    [Nullable(2)]
    private ArcDock dock;

    // Token: 0x040007E4 RID: 2020
    [Nullable(2)]
    private Window window;

    // Token: 0x040007E5 RID: 2021
    [Nullable(2)]
    private AppWindow appWindow;

    // Token: 0x040007E6 RID: 2022
    private IntPtr hwnd;

    // Token: 0x040007E7 RID: 2023
    [Nullable(2)]
    private HeyClickyWindowHost host;

    // Token: 0x040007E8 RID: 2024
    [Nullable(2)]
    private Grid root;

    // Token: 0x040007E9 RID: 2025
    [Nullable(2)]
    private Grid sidebarColumn;

    // Token: 0x040007EA RID: 2026
    [Nullable(2)]
    private Grid documentColumn;

    // Token: 0x040007EB RID: 2027
    [Nullable(2)]
    private Grid ground;

    // Token: 0x040007EC RID: 2028
    [Nullable(2)]
    private Border mainCard;

    // Token: 0x040007ED RID: 2029
    [Nullable(2)]
    private Border columnHeader;

    // Token: 0x040007EE RID: 2030
    [Nullable(2)]
    private Border mainColumn;

    // Token: 0x040007EF RID: 2031
    [Nullable(2)]
    private FrameworkElement chatDots;

    // Token: 0x040007F0 RID: 2032
    [Nullable(2)]
    private StackPanel sidebarRows;

    // Token: 0x040007F1 RID: 2033
    [Nullable(2)]
    private StackPanel topLeft;

    // Token: 0x040007F2 RID: 2034
    [Nullable(2)]
    private Pressable sidebarButton;

    // Token: 0x040007F3 RID: 2035
    [Nullable(2)]
    private Pressable backButton;

    // Token: 0x040007F4 RID: 2036
    [Nullable(2)]
    private Grid dragBand;

    // Token: 0x040007F5 RID: 2037
    private readonly List<FrameworkElement> headerButtons = new List<FrameworkElement>();

    // Token: 0x040007F6 RID: 2038
    [Nullable(2)]
    private ColumnDefinition sidebarDefinition;

    // Token: 0x040007F7 RID: 2039
    [Nullable(2)]
    private ColumnDefinition documentDefinition;

    // Token: 0x040007F8 RID: 2040
    [Nullable(2)]
    private NotchChatView mainChat;

    // Token: 0x040007F9 RID: 2041
    [Nullable(2)]
    private SettingsContent settings;

    // Token: 0x040007FA RID: 2042
    [Nullable(2)]
    private NotchDocumentViewer documentViewer;

    // Token: 0x040007FB RID: 2043
    [Nullable(2)]
    private Border documentPlate;

    // Token: 0x040007FC RID: 2044
    [Nullable(2)]
    private Border documentHeader;

    // Token: 0x040007FD RID: 2045
    private string selectedKey = "heyclicky";

    // Token: 0x040007FE RID: 2046
    [Nullable(2)]
    private string keyBeforeSettings;

    // Token: 0x040007FF RID: 2047
    private bool showingSettings;

    // Token: 0x04000800 RID: 2048
    private bool earlierUnfolded;

    // Token: 0x04000801 RID: 2049
    private bool isActive;

    // Token: 0x04000802 RID: 2050
    [Nullable(2)]
    private string documentPath;

    // Token: 0x04000803 RID: 2051
    [Nullable(2)]
    private string documentThreadId;

    // Token: 0x04000804 RID: 2052
    [Nullable(2)]
    private volatile string shownAgentKey;

    // Token: 0x04000805 RID: 2053
    private HeyClickyWindow.State saved = HeyClickyWindow.State.Load();

    // Token: 0x04000806 RID: 2054
    private HeyClickyWindowPalette palette = HeyClickyWindowPalette.Dark;

    // Token: 0x04000807 RID: 2055
    [Nullable(2)]
    private SystemLookWatcher lookWatcher;

    // Token: 0x04000808 RID: 2056
    [Nullable(2)]
    private Border accountSlot;

    // Token: 0x04000809 RID: 2057
    [Nullable(2)]
    private Grid dropOverlay;

    // Token: 0x0400080A RID: 2058
    private bool dropTargeted;

    // Token: 0x0400080B RID: 2059
    private int dragLeaveCount;

    // Token: 0x0400080C RID: 2060
    private const int SW_RESTORE = 9;

    // Token: 0x0400080D RID: 2061
    private long lastDeactivatedTicks;

    // Token: 0x0400080E RID: 2062
    private const double HeaderHeight = 44.0;

    // Token: 0x0400080F RID: 2063
    private const double TitleBandHeight = 36.0;

    // Token: 0x04000810 RID: 2064
    private const double CardTopInset = 36.0;

    // Token: 0x04000811 RID: 2065
    private const double CardInset = 8.0;

    // Token: 0x04000812 RID: 2066
    private const double CardCornerRadius = 12.0;

    // Token: 0x04000813 RID: 2067
    private const double SidebarWidth = 244.0;

    // Token: 0x04000814 RID: 2068
    private const double CardHeaderHeight = 38.0;

    // Token: 0x04000815 RID: 2069
    private const double DocumentHeaderHeight = 32.0;

    // Token: 0x04000816 RID: 2070
    private const double MinimumDocumentWidth = 340.0;

    // Token: 0x04000817 RID: 2071
    private const double MinimumConversationWidth = 380.0;

    // Token: 0x04000818 RID: 2072
    private const double TopLeftLeading = 14.0;

    // Token: 0x04000819 RID: 2073
    private const string SidebarGlyph = "\ue90c";

    // Token: 0x0400081A RID: 2074
    private static readonly SwiftSpring ColumnSpring = new SwiftSpring(0.32, 0.88);

    // Token: 0x0400081B RID: 2075
    [Nullable(2)]
    private SpringValue sidebarShownness;

    // Token: 0x0400081C RID: 2076
    [Nullable(2)]
    private SpringValue documentShownness;

    // Token: 0x0400081D RID: 2077
    private bool columnFramesRunning;

    // Token: 0x0400081E RID: 2078
    private bool documentViewerHolds;

    // Token: 0x0400081F RID: 2079
    private string passthroughShape = "";

    // Token: 0x04000820 RID: 2080
    [Nullable(2)]
    private ContentControl focusRest;

    // Token: 0x04000821 RID: 2081
    private static readonly HashSet<string> LittleWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
      "a", "an", "the", "of", "for", "to", "and", "in", "on", "my",
      "your", "with", "about", "from", "at", "by", "me"
    };

    // Token: 0x04000822 RID: 2082
    private readonly Dictionary<string, FrameworkElement> sidebarViews = new Dictionary<string, FrameworkElement>();

    // Token: 0x04000823 RID: 2083
    private string sidebarShape = "";

    // Token: 0x04000824 RID: 2084
    private readonly Dictionary<string, string> sidebarSections = new Dictionary<string, string>();

    // Token: 0x04000825 RID: 2085
    private static readonly Dictionary<string, BitmapImage> AvatarImages = new Dictionary<string, BitmapImage>();

    // Token: 0x04000826 RID: 2086
    private bool refreshQueued;

    // Token: 0x04000827 RID: 2087
    private bool wasAnswering;

    // Token: 0x04000828 RID: 2088
    [Nullable(2)]
    private FrameworkElement headerChild;

    // Token: 0x04000829 RID: 2089
    [Nullable(2)]
    private string shownHeaderShape;

    // Token: 0x0400082A RID: 2090
    private const double SettingsPageListLeading = 12.0;

    // Token: 0x0400082B RID: 2091
    private const double SettingsPageListWidth = 224.0;

    // Token: 0x0400082C RID: 2092
    private const double SettingsPageContentLeading = 261.0;

    // Token: 0x0400082D RID: 2093
    [Nullable(2)]
    private FrameworkElement shownChild;

    // Token: 0x0400082E RID: 2094
    [Nullable(2)]
    private Grid stage;

    // Token: 0x0400082F RID: 2095
    private SettingsWindow.Page settingsPage;

    // Token: 0x04000830 RID: 2096
    private double windowMoveToContentWidth;

    // Token: 0x04000831 RID: 2097
    private RectInt32 windowMoveFrom;

    // Token: 0x04000832 RID: 2098
    private RectInt32 windowMoveTo;

    // Token: 0x04000833 RID: 2099
    private double windowMoveStartedAt;

    // Token: 0x04000834 RID: 2100
    private double windowMoveSeconds;

    // Token: 0x04000835 RID: 2101
    private bool windowMoving;

    // Token: 0x04000836 RID: 2102
    [Nullable(2)]
    private HeyClickyWindow.DispatcherQueueTimerHolder saveTimer;

    // Token: 0x0200068B RID: 1675
    [Nullable(0)]
    private sealed class SidebarEntry : IEquatable<HeyClickyWindow.SidebarEntry>
    {
      // Token: 0x06003266 RID: 12902 RVA: 0x00672480 File Offset: 0x00672480
      public SidebarEntry(string Key, string Name, [Nullable(2)] string JobId, [Nullable(2)] string ClickySlug, ArcCardState? State, DateTimeOffset LatestAt, bool OnDock)
      {
        this.Key = Key;
        this.Name = Name;
        this.JobId = JobId;
        this.ClickySlug = ClickySlug;
        this.State = State;
        this.LatestAt = LatestAt;
        this.OnDock = OnDock;
        base..ctor();
      }

      // Token: 0x17000799 RID: 1945
      // (get) Token: 0x06003267 RID: 12903 RVA: 0x006724C0 File Offset: 0x006724C0
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(HeyClickyWindow.SidebarEntry);
        }
      }

      // Token: 0x1700079A RID: 1946
      // (get) Token: 0x06003268 RID: 12904 RVA: 0x006724CC File Offset: 0x006724CC
      // (set) Token: 0x06003269 RID: 12905 RVA: 0x006724D4 File Offset: 0x006724D4
      public string Key { get; set; }

      // Token: 0x1700079B RID: 1947
      // (get) Token: 0x0600326A RID: 12906 RVA: 0x006724E0 File Offset: 0x006724E0
      // (set) Token: 0x0600326B RID: 12907 RVA: 0x006724E8 File Offset: 0x006724E8
      public string Name { get; set; }

      // Token: 0x1700079C RID: 1948
      // (get) Token: 0x0600326C RID: 12908 RVA: 0x006724F4 File Offset: 0x006724F4
      // (set) Token: 0x0600326D RID: 12909 RVA: 0x006724FC File Offset: 0x006724FC
      [Nullable(2)]
      public string JobId
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x1700079D RID: 1949
      // (get) Token: 0x0600326E RID: 12910 RVA: 0x00672508 File Offset: 0x00672508
      // (set) Token: 0x0600326F RID: 12911 RVA: 0x00672510 File Offset: 0x00672510
      [Nullable(2)]
      public string ClickySlug
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x1700079E RID: 1950
      // (get) Token: 0x06003270 RID: 12912 RVA: 0x0067251C File Offset: 0x0067251C
      // (set) Token: 0x06003271 RID: 12913 RVA: 0x00672524 File Offset: 0x00672524
      public ArcCardState? State { get; set; }

      // Token: 0x1700079F RID: 1951
      // (get) Token: 0x06003272 RID: 12914 RVA: 0x00672530 File Offset: 0x00672530
      // (set) Token: 0x06003273 RID: 12915 RVA: 0x00672538 File Offset: 0x00672538
      public DateTimeOffset LatestAt { get; set; }

      // Token: 0x170007A0 RID: 1952
      // (get) Token: 0x06003274 RID: 12916 RVA: 0x00672544 File Offset: 0x00672544
      // (set) Token: 0x06003275 RID: 12917 RVA: 0x0067254C File Offset: 0x0067254C
      public bool OnDock { get; set; }

      // Token: 0x06003276 RID: 12918 RVA: 0x00672558 File Offset: 0x00672558
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("SidebarEntry");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003277 RID: 12919 RVA: 0x006725A4 File Offset: 0x006725A4
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Key = ");
        builder.Append(this.Key);
        builder.Append(", Name = ");
        builder.Append(this.Name);
        builder.Append(", JobId = ");
        builder.Append(this.JobId);
        builder.Append(", ClickySlug = ");
        builder.Append(this.ClickySlug);
        builder.Append(", State = ");
        builder.Append(this.State.ToString());
        builder.Append(", LatestAt = ");
        builder.Append(this.LatestAt.ToString());
        builder.Append(", OnDock = ");
        builder.Append(this.OnDock.ToString());
        return true;
      }

      // Token: 0x06003278 RID: 12920 RVA: 0x00672690 File Offset: 0x00672690
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(HeyClickyWindow.SidebarEntry left, HeyClickyWindow.SidebarEntry right)
      {
        return !(left == right);
      }

      // Token: 0x06003279 RID: 12921 RVA: 0x0067269C File Offset: 0x0067269C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(HeyClickyWindow.SidebarEntry left, HeyClickyWindow.SidebarEntry right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600327A RID: 12922 RVA: 0x006726B0 File Offset: 0x006726B0
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Key>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<JobId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField)) * -1521134295 + EqualityComparer<ArcCardState?>.Default.GetHashCode(this.<State>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<LatestAt>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<OnDock>k__BackingField);
      }

      // Token: 0x0600327B RID: 12923 RVA: 0x00672770 File Offset: 0x00672770
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as HeyClickyWindow.SidebarEntry);
      }

      // Token: 0x0600327C RID: 12924 RVA: 0x00672780 File Offset: 0x00672780
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(HeyClickyWindow.SidebarEntry other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Key>k__BackingField, other.<Key>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<JobId>k__BackingField, other.<JobId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField) && EqualityComparer<ArcCardState?>.Default.Equals(this.<State>k__BackingField, other.<State>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<LatestAt>k__BackingField, other.<LatestAt>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<OnDock>k__BackingField, other.<OnDock>k__BackingField));
      }

      // Token: 0x0600327E RID: 12926 RVA: 0x00672868 File Offset: 0x00672868
      [CompilerGenerated]
      private SidebarEntry(HeyClickyWindow.SidebarEntry original)
      {
        this.Key = original.<Key>k__BackingField;
        this.Name = original.<Name>k__BackingField;
        this.JobId = original.<JobId>k__BackingField;
        this.ClickySlug = original.<ClickySlug>k__BackingField;
        this.State = original.<State>k__BackingField;
        this.LatestAt = original.<LatestAt>k__BackingField;
        this.OnDock = original.<OnDock>k__BackingField;
      }

      // Token: 0x0600327F RID: 12927 RVA: 0x006728D0 File Offset: 0x006728D0
      [CompilerGenerated]
      public void Deconstruct(out string Key, out string Name, [Nullable(2)] out string JobId, [Nullable(2)] out string ClickySlug, out ArcCardState? State, out DateTimeOffset LatestAt, out bool OnDock)
      {
        Key = this.Key;
        Name = this.Name;
        JobId = this.JobId;
        ClickySlug = this.ClickySlug;
        State = this.State;
        LatestAt = this.LatestAt;
        OnDock = this.OnDock;
      }
    }

    // Token: 0x0200068C RID: 1676
    [Nullable(0)]
    private sealed class DocumentResizeHandle : Grid
    {
      // Token: 0x06003280 RID: 12928 RVA: 0x00672924 File Offset: 0x00672924
      public DocumentResizeHandle(HeyClickyWindow owner, MessagesPalette look)
      {
        HeyClickyWindow.DocumentResizeHandle <>4__this = this;
        this.owner = owner;
        base.Background = Ui.Transparent;
        this.capsule = new Border
        {
          Width = 3.0,
          Height = 44.0,
          CornerRadius = new CornerRadius(1.5),
          Background = Ui.Brush(look.InkAt(0.3)),
          Opacity = 0.0,
          HorizontalAlignment = HorizontalAlignment.Center,
          VerticalAlignment = VerticalAlignment.Center,
          IsHitTestVisible = false
        };
        base.Children.Add(this.capsule);
        base.ProtectedCursor = InputSystemCursor.Create(InputSystemCursorShape.SizeWestEast);
        ToolTipService.SetToolTip(this, "Drag to resize");
        base.PointerEntered += delegate(object _, PointerRoutedEventArgs _)
        {
          <>4__this.hovered = true;
          <>4__this.Show();
        };
        base.PointerExited += delegate(object _, PointerRoutedEventArgs _)
        {
          <>4__this.hovered = false;
          <>4__this.Show();
        };
        base.PointerPressed += delegate(object _, PointerRoutedEventArgs e)
        {
          <>4__this.CapturePointer(e.Pointer);
          <>4__this.startX = new double?(e.GetCurrentPoint(owner.root).Position.X);
          HeyClickyWindow.DocumentResizeHandle <>4__this2 = <>4__this;
          ColumnDefinition documentDefinition = owner.documentDefinition;
          <>4__this2.startWidth = ((documentDefinition != null) ? new double?(documentDefinition.Width.Value) : null);
          <>4__this.Show();
          e.Handled = true;
        };
        base.PointerMoved += delegate(object _, PointerRoutedEventArgs e)
        {
          double? num = <>4__this.startX;
          if (num != null)
          {
            double valueOrDefault = num.GetValueOrDefault();
            num = <>4__this.startWidth;
            if (num != null)
            {
              double valueOrDefault2 = num.GetValueOrDefault();
              owner.ResizeDocument(valueOrDefault2 - (e.GetCurrentPoint(owner.root).Position.X - valueOrDefault));
              return;
            }
          }
        };
        base.PointerReleased += delegate(object _, PointerRoutedEventArgs e)
        {
          <>4__this.End(e.Pointer);
        };
        base.PointerCaptureLost += delegate(object _, PointerRoutedEventArgs e)
        {
          <>4__this.End(e.Pointer);
        };
      }

      // Token: 0x06003281 RID: 12929 RVA: 0x00672A74 File Offset: 0x00672A74
      private void Show()
      {
        UIElement uielement = this.capsule;
        float num2;
        if (!this.hovered)
        {
          double? num = this.startX;
          if (num == null)
          {
            num2 = (float)0;
            goto IL_0022;
          }
        }
        num2 = (float)1;
        IL_0022:
        Anim.Opacity(uielement, num2, 0.15, Easing.EaseOut(Ui.Compositor), 0.0);
      }

      // Token: 0x06003282 RID: 12930 RVA: 0x00672AC8 File Offset: 0x00672AC8
      private void End(Pointer pointer)
      {
        double? num = this.startX;
        if (num == null)
        {
          return;
        }
        this.startX = null;
        base.ReleasePointerCapture(pointer);
        this.Show();
        this.owner.SaveFrame();
      }

      // Token: 0x04001DE4 RID: 7652
      private readonly HeyClickyWindow owner;

      // Token: 0x04001DE5 RID: 7653
      private readonly Border capsule;

      // Token: 0x04001DE6 RID: 7654
      private double? startX;

      // Token: 0x04001DE7 RID: 7655
      private double? startWidth;

      // Token: 0x04001DE8 RID: 7656
      private bool hovered;
    }

    // Token: 0x0200068D RID: 1677
    [Nullable(0)]
    private sealed class State : IEquatable<HeyClickyWindow.State>
    {
      // Token: 0x06003283 RID: 12931 RVA: 0x00672B0C File Offset: 0x00672B0C
      public State(int X, int Y, int Width, int Height, bool SidebarShown, double DocumentWidth)
      {
        this.X = X;
        this.Y = Y;
        this.Width = Width;
        this.Height = Height;
        this.SidebarShown = SidebarShown;
        this.DocumentWidth = DocumentWidth;
        base..ctor();
      }

      // Token: 0x170007A1 RID: 1953
      // (get) Token: 0x06003284 RID: 12932 RVA: 0x00672B44 File Offset: 0x00672B44
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(HeyClickyWindow.State);
        }
      }

      // Token: 0x170007A2 RID: 1954
      // (get) Token: 0x06003285 RID: 12933 RVA: 0x00672B50 File Offset: 0x00672B50
      // (set) Token: 0x06003286 RID: 12934 RVA: 0x00672B58 File Offset: 0x00672B58
      public int X { get; set; }

      // Token: 0x170007A3 RID: 1955
      // (get) Token: 0x06003287 RID: 12935 RVA: 0x00672B64 File Offset: 0x00672B64
      // (set) Token: 0x06003288 RID: 12936 RVA: 0x00672B6C File Offset: 0x00672B6C
      public int Y { get; set; }

      // Token: 0x170007A4 RID: 1956
      // (get) Token: 0x06003289 RID: 12937 RVA: 0x00672B78 File Offset: 0x00672B78
      // (set) Token: 0x0600328A RID: 12938 RVA: 0x00672B80 File Offset: 0x00672B80
      public int Width { get; set; }

      // Token: 0x170007A5 RID: 1957
      // (get) Token: 0x0600328B RID: 12939 RVA: 0x00672B8C File Offset: 0x00672B8C
      // (set) Token: 0x0600328C RID: 12940 RVA: 0x00672B94 File Offset: 0x00672B94
      public int Height { get; set; }

      // Token: 0x170007A6 RID: 1958
      // (get) Token: 0x0600328D RID: 12941 RVA: 0x00672BA0 File Offset: 0x00672BA0
      // (set) Token: 0x0600328E RID: 12942 RVA: 0x00672BA8 File Offset: 0x00672BA8
      public bool SidebarShown { get; set; }

      // Token: 0x170007A7 RID: 1959
      // (get) Token: 0x0600328F RID: 12943 RVA: 0x00672BB4 File Offset: 0x00672BB4
      // (set) Token: 0x06003290 RID: 12944 RVA: 0x00672BBC File Offset: 0x00672BBC
      public double DocumentWidth { get; set; }

      // Token: 0x170007A8 RID: 1960
      // (get) Token: 0x06003291 RID: 12945 RVA: 0x00672BC8 File Offset: 0x00672BC8
      private static string FilePath
      {
        get
        {
          return Path.Combine(AppBundleConfiguration.AppDataDirectory, "heyclicky-window.json");
        }
      }

      // Token: 0x06003292 RID: 12946 RVA: 0x00672BDC File Offset: 0x00672BDC
      public static HeyClickyWindow.State Load()
      {
        try
        {
          if (File.Exists(HeyClickyWindow.State.FilePath))
          {
            JsonObject jsonObject = JsonNode.Parse(File.ReadAllText(HeyClickyWindow.State.FilePath), null, default(JsonDocumentOptions)) as JsonObject;
            if (jsonObject != null)
            {
              return new HeyClickyWindow.State(((int?)jsonObject["x"]).GetValueOrDefault(-1), ((int?)jsonObject["y"]).GetValueOrDefault(-1), ((int?)jsonObject["w"]).GetValueOrDefault(), ((int?)jsonObject["h"]).GetValueOrDefault(), ((bool?)jsonObject["sidebar"]).GetValueOrDefault(true), ((double?)jsonObject["document"]).GetValueOrDefault(520.0));
            }
          }
        }
        catch (Exception ex)
        {
          Log.Warn("window", "window frame unreadable: " + ex.Message);
        }
        return new HeyClickyWindow.State(-1, -1, 0, 0, true, 520.0);
      }

      // Token: 0x06003293 RID: 12947 RVA: 0x00672D14 File Offset: 0x00672D14
      public void Save()
      {
        try
        {
          Directory.CreateDirectory(Path.GetDirectoryName(HeyClickyWindow.State.FilePath));
          string filePath = HeyClickyWindow.State.FilePath;
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["x"] = this.X;
          jsonObject["y"] = this.Y;
          jsonObject["w"] = this.Width;
          jsonObject["h"] = this.Height;
          jsonObject["sidebar"] = this.SidebarShown;
          jsonObject["document"] = this.DocumentWidth;
          File.WriteAllText(filePath, jsonObject.ToJsonString(null));
        }
        catch (Exception ex)
        {
          Log.Warn("window", "window frame not saved: " + ex.Message);
        }
      }

      // Token: 0x06003294 RID: 12948 RVA: 0x00672E04 File Offset: 0x00672E04
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("State");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003295 RID: 12949 RVA: 0x00672E50 File Offset: 0x00672E50
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("X = ");
        builder.Append(this.X.ToString());
        builder.Append(", Y = ");
        builder.Append(this.Y.ToString());
        builder.Append(", Width = ");
        builder.Append(this.Width.ToString());
        builder.Append(", Height = ");
        builder.Append(this.Height.ToString());
        builder.Append(", SidebarShown = ");
        builder.Append(this.SidebarShown.ToString());
        builder.Append(", DocumentWidth = ");
        builder.Append(this.DocumentWidth.ToString());
        return true;
      }

      // Token: 0x06003296 RID: 12950 RVA: 0x00672F50 File Offset: 0x00672F50
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(HeyClickyWindow.State left, HeyClickyWindow.State right)
      {
        return !(left == right);
      }

      // Token: 0x06003297 RID: 12951 RVA: 0x00672F5C File Offset: 0x00672F5C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(HeyClickyWindow.State left, HeyClickyWindow.State right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003298 RID: 12952 RVA: 0x00672F70 File Offset: 0x00672F70
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<X>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Y>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Width>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Height>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<SidebarShown>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<DocumentWidth>k__BackingField);
      }

      // Token: 0x06003299 RID: 12953 RVA: 0x00673018 File Offset: 0x00673018
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as HeyClickyWindow.State);
      }

      // Token: 0x0600329A RID: 12954 RVA: 0x00673028 File Offset: 0x00673028
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(HeyClickyWindow.State other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<X>k__BackingField, other.<X>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Y>k__BackingField, other.<Y>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Width>k__BackingField, other.<Width>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Height>k__BackingField, other.<Height>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<SidebarShown>k__BackingField, other.<SidebarShown>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<DocumentWidth>k__BackingField, other.<DocumentWidth>k__BackingField));
      }

      // Token: 0x0600329C RID: 12956 RVA: 0x006730F4 File Offset: 0x006730F4
      [CompilerGenerated]
      private State(HeyClickyWindow.State original)
      {
        this.X = original.<X>k__BackingField;
        this.Y = original.<Y>k__BackingField;
        this.Width = original.<Width>k__BackingField;
        this.Height = original.<Height>k__BackingField;
        this.SidebarShown = original.<SidebarShown>k__BackingField;
        this.DocumentWidth = original.<DocumentWidth>k__BackingField;
      }

      // Token: 0x0600329D RID: 12957 RVA: 0x00673150 File Offset: 0x00673150
      [CompilerGenerated]
      public void Deconstruct(out int X, out int Y, out int Width, out int Height, out bool SidebarShown, out double DocumentWidth)
      {
        X = this.X;
        Y = this.Y;
        Width = this.Width;
        Height = this.Height;
        SidebarShown = this.SidebarShown;
        DocumentWidth = this.DocumentWidth;
      }
    }

    // Token: 0x0200068E RID: 1678
    [Nullable(0)]
    private sealed class DispatcherQueueTimerHolder
    {
      // Token: 0x0600329E RID: 12958 RVA: 0x00673188 File Offset: 0x00673188
      public DispatcherQueueTimerHolder(Microsoft.UI.Dispatching.DispatcherQueue queue, Action tick)
      {
      }

      // Token: 0x0600329F RID: 12959 RVA: 0x006731A0 File Offset: 0x006731A0
      public void Restart(TimeSpan after)
      {
        if (this.timer == null)
        {
          this.timer = this.<queue>P.CreateTimer();
        }
        this.timer.Stop();
        this.timer.Interval = after;
        this.timer.IsRepeating = false;
        this.timer.Tick -= this.OnTick;
        this.timer.Tick += this.OnTick;
        this.timer.Start();
      }

      // Token: 0x060032A0 RID: 12960 RVA: 0x00673224 File Offset: 0x00673224
      private void OnTick(Microsoft.UI.Dispatching.DispatcherQueueTimer sender, object args)
      {
        this.<tick>P();
      }

      // Token: 0x04001DEF RID: 7663
      [CompilerGenerated]
      private Microsoft.UI.Dispatching.DispatcherQueue <queue>P = queue;

      // Token: 0x04001DF0 RID: 7664
      [CompilerGenerated]
      private Action <tick>P = tick;

      // Token: 0x04001DF1 RID: 7665
      [Nullable(2)]
      private Microsoft.UI.Dispatching.DispatcherQueueTimer timer;
    }
  }
}
