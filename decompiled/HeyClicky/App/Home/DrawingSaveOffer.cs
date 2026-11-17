using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using HeyClicky.Drawing;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Graphics;

namespace HeyClicky.App.Home
{
  // Token: 0x020002B8 RID: 696
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DrawingSaveOffer
  {
    // Token: 0x06001CC3 RID: 7363 RVA: 0x005FBC30 File Offset: 0x005FBC30
    public DrawingSaveOffer(bool excludeFromCapture)
    {
      this.window = new OverlayWindow("HeyClicky save drawing", excludeFromCapture, false, false, true, false, false);
      this.save = new GelButton("Save drawing", GelAppearance.PrimaryBlue, GelButton.Sizing.Compact, 100.0, "\ue896", null, null);
      this.clear = new GelButton("Clear", GelAppearance.SecondaryWhite, GelButton.Sizing.Compact, 100.0, "\ue711", null, null);
      this.save.Click += delegate
      {
        Action action = this.onSave;
        if (action == null)
        {
          return;
        }
        action();
      };
      this.clear.Click += delegate
      {
        this.Hide();
        Action action2 = this.onClear;
        if (action2 == null)
        {
          return;
        }
        action2();
      };
      StackPanel stackPanel = Ui.H(8.0, new UIElement[] { this.clear, this.save });
      stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
      stackPanel.VerticalAlignment = VerticalAlignment.Center;
      this.window.Root.Children.Add(stackPanel);
      this.dq = DispatcherQueue.GetForCurrentThread();
      NotchVisibleSurfaces.Changed += this.OnNotchChanged;
    }

    // Token: 0x170004FF RID: 1279
    // (get) Token: 0x06001CC4 RID: 7364 RVA: 0x005FBD60 File Offset: 0x005FBD60
    // (set) Token: 0x06001CC5 RID: 7365 RVA: 0x005FBD68 File Offset: 0x005FBD68
    public bool IsShown { get; private set; }

    // Token: 0x06001CC6 RID: 7366 RVA: 0x005FBD74 File Offset: 0x005FBD74
    public void Show(DisplayInfo display, [Nullable(2)] Action save, Action clear)
    {
      this.onSave = save;
      this.onClear = clear;
      this.save.Visibility = ((save == null) ? Visibility.Collapsed : Visibility.Visible);
      this.shownOn = display;
      this.Place(display);
      this.window.ShowNoActivate();
      this.IsShown = true;
      Log.Info("lessons", (save == null) ? "clear offer shown (Clear only)" : "save offer shown");
    }

    // Token: 0x06001CC7 RID: 7367 RVA: 0x005FBDDC File Offset: 0x005FBDDC
    private void Place(DisplayInfo display)
    {
      double scale = display.Scale;
      int num = (int)(300.0 * scale);
      int num2 = (int)(44.0 * scale);
      int num3 = (int)(display.BoundsPx.X + (display.BoundsPx.Width - (double)num) / 2.0);
      int num4 = (int)(display.BoundsPx.Y + 52.0 * scale);
      RectInt32 rectInt = new RectInt32(num3, num4, num, num2);
      RectInt32 rectInt2 = new RectInt32(num3 - (int)(8.0 * scale), num4 - (int)(8.0 * scale), num + (int)(16.0 * scale), num2 + (int)(16.0 * scale));
      RectInt32? frame = NotchVisibleSurfaces.Frame;
      if (frame != null)
      {
        RectInt32 valueOrDefault = frame.GetValueOrDefault();
        if (NotchVisibleSurfaces.Intersects(valueOrDefault, rectInt2))
        {
          num4 = Math.Min((int)(display.BoundsPx.Y + display.BoundsPx.Height - (double)num2 - 12.0 * scale), valueOrDefault.Y + valueOrDefault.Height + (int)(10.0 * scale));
        }
      }
      if (num4 != this.placedY && this.IsShown)
      {
        Log.Info("lessons", (num4 == rectInt.Y) ? "save offer back under the notch" : "save offer steps below the open notch surface");
      }
      this.placedY = num4;
      this.window.PlacePx(num3, num4, num, num2);
    }

    // Token: 0x06001CC8 RID: 7368 RVA: 0x005FBF64 File Offset: 0x005FBF64
    private void OnNotchChanged()
    {
      DispatcherQueue dispatcherQueue = this.dq;
      if (dispatcherQueue == null)
      {
        return;
      }
      dispatcherQueue.TryEnqueue(delegate
      {
        if (this.IsShown)
        {
          DisplayInfo displayInfo = this.shownOn;
          if (displayInfo != null)
          {
            this.Place(displayInfo);
          }
        }
      });
    }

    // Token: 0x06001CC9 RID: 7369 RVA: 0x005FBF84 File Offset: 0x005FBF84
    public void Hide()
    {
      if (!this.IsShown)
      {
        return;
      }
      this.IsShown = false;
      this.window.HideWindow();
    }

    // Token: 0x17000500 RID: 1280
    // (get) Token: 0x06001CCA RID: 7370 RVA: 0x005FBFA4 File Offset: 0x005FBFA4
    public IntPtr Hwnd
    {
      get
      {
        return this.window.Hwnd;
      }
    }

    // Token: 0x04000DC5 RID: 3525
    private readonly OverlayWindow window;

    // Token: 0x04000DC6 RID: 3526
    private readonly GelButton save;

    // Token: 0x04000DC7 RID: 3527
    private readonly GelButton clear;

    // Token: 0x04000DC8 RID: 3528
    [Nullable(2)]
    private Action onSave;

    // Token: 0x04000DC9 RID: 3529
    [Nullable(2)]
    private Action onClear;

    // Token: 0x04000DCB RID: 3531
    [Nullable(2)]
    private DisplayInfo shownOn;

    // Token: 0x04000DCC RID: 3532
    private int placedY = int.MinValue;

    // Token: 0x04000DCD RID: 3533
    [Nullable(2)]
    private DispatcherQueue dq;
  }
}
