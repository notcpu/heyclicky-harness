using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using HeyClicky.Core.Brain;
using HeyClicky.Core.Settings;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Shell
{
  // Token: 0x020000BE RID: 190
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class OneBrainStatsPanel
  {
    // Token: 0x060007B0 RID: 1968 RVA: 0x00539E8C File Offset: 0x00539E8C
    public OneBrainStatsPanel(SettingsStore settings, bool keepOutOfCapture = true)
    {
      this.settings = settings;
      this.keepOutOfCapture = keepOutOfCapture;
      this.dq = DispatcherQueue.GetForCurrentThread();
      OneBrainStats.Shared.IsEnabledProvider = () => settings.Read<bool>((AppSettings s) => s.ShowOneBrainStats);
    }

    // Token: 0x060007B1 RID: 1969 RVA: 0x00539EE0 File Offset: 0x00539EE0
    public void Start()
    {
      this.ApplyPreference();
      this.settings.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.dq.TryEnqueue(new DispatcherQueueHandler(this.ApplyPreference));
      };
      OneBrainStats.Shared.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        if (this.refreshQueued)
        {
          return;
        }
        this.refreshQueued = true;
        this.dq.TryEnqueue(delegate
        {
          this.refreshQueued = false;
          this.Refresh();
        });
      };
    }

    // Token: 0x060007B2 RID: 1970 RVA: 0x00539F18 File Offset: 0x00539F18
    private void ApplyPreference()
    {
      bool isEnabled = OneBrainStats.Shared.IsEnabled;
      if (isEnabled && this.window == null)
      {
        this.Show();
      }
      if (!isEnabled && this.window != null)
      {
        DispatcherQueueTimer dispatcherQueueTimer = this.tick;
        if (dispatcherQueueTimer != null)
        {
          dispatcherQueueTimer.Stop();
        }
        this.tick = null;
        this.window.HideWindow();
        this.window.Close();
        this.window = null;
        this.rows = null;
      }
    }

    // Token: 0x060007B3 RID: 1971 RVA: 0x00539F88 File Offset: 0x00539F88
    private void Show()
    {
      this.window = new OverlayWindow("HeyClicky one brain stats", this.keepOutOfCapture, true, false, true, false, false);
      if (this.keepOutOfCapture)
      {
        this.window.KeepOutOfEveryCapture();
      }
      this.rows = new StackPanel
      {
        Spacing = 5.0
      };
      Border border = new Border
      {
        Width = 340.0,
        Padding = new Thickness(10.0),
        CornerRadius = new CornerRadius(10.0),
        Background = Ui.Brush(DS.Black(0.78)),
        Child = this.rows,
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Top
      };
      this.window.Root.Children.Add(border);
      Displays.Monitor monitor = Displays.Primary();
      double scale = monitor.Scale;
      this.window.PlacePx((int)((double)monitor.Work.Left + 12.0 * scale), (int)((double)monitor.Work.Top + 12.0 * scale), (int)(340.0 * scale), (int)(380.0 * scale));
      this.Refresh();
      this.window.ShowNoActivate();
      this.tick = this.dq.CreateTimer();
      this.tick.Interval = TimeSpan.FromSeconds(1L);
      this.tick.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.Refresh();
      };
      this.tick.Start();
    }

    // Token: 0x060007B4 RID: 1972 RVA: 0x0053A120 File Offset: 0x0053A120
    private void Refresh()
    {
      if (this.rows == null)
      {
        return;
      }
      ValueTuple<List<ValueTuple<string, string>>, double, bool, int> valueTuple = OneBrainStats.Shared.Describe(DateTimeOffset.Now);
      List<ValueTuple<string, string>> item = valueTuple.Item1;
      double item2 = valueTuple.Item2;
      bool item3 = valueTuple.Item3;
      int item4 = valueTuple.Item4;
      this.rows.Children.Clear();
      for (int i = 0; i < item.Count; i++)
      {
        if (i == item4)
        {
          this.rows.Children.Add(OneBrainStatsPanel.ProgressBar(item2, item3));
        }
        ValueTuple<string, string> valueTuple2 = item[i];
        string item5 = valueTuple2.Item1;
        string item6 = valueTuple2.Item2;
        if (item6 == "divider")
        {
          this.rows.Children.Add(new Border
          {
            Height = 1.0,
            Background = Ui.Brush(DS.White(0.15))
          });
        }
        else
        {
          Color color;
          if (!(item6 == "dim"))
          {
            if (!(item6 == "faint") && !(item6 == "event"))
            {
              if (!(item6 == "hit"))
              {
                if (!(item6 == "miss"))
                {
                  color = DS.White(0.9);
                }
                else
                {
                  color = OneBrainStatsPanel.Orange;
                }
              }
              else
              {
                color = OneBrainStatsPanel.Green;
              }
            }
            else
            {
              color = DS.White(0.6);
            }
          }
          else
          {
            color = DS.White(0.7);
          }
          Color color2 = color;
          TextBlock textBlock = Ui.Text(item5, 10.5, (item6 == "bold") ? FontWeights.Bold : FontWeights.Normal, color2, TextAlignment.Left, 0, item6 != "event", true);
          this.rows.Children.Add(textBlock);
        }
      }
    }

    // Token: 0x060007B5 RID: 1973 RVA: 0x0053A2F0 File Offset: 0x0053A2F0
    private static FrameworkElement ProgressBar(double fraction, bool pastThreshold)
    {
      Grid grid = new Grid();
      grid.Height = 4.0;
      grid.CornerRadius = new CornerRadius(2.0);
      grid.Background = Ui.Brush(DS.White(0.15));
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(Math.Clamp(fraction, 0.0, 1.0), GridUnitType.Star)
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0 - Math.Clamp(fraction, 0.0, 1.0), GridUnitType.Star)
      });
      Border border = new Border
      {
        CornerRadius = new CornerRadius(2.0),
        Background = Ui.Brush(pastThreshold ? OneBrainStatsPanel.Orange : OneBrainStatsPanel.Blue)
      };
      grid.Children.Add(border);
      return grid;
    }

    // Token: 0x04000258 RID: 600
    private const double PanelWidth = 340.0;

    // Token: 0x04000259 RID: 601
    private const double PanelHeight = 380.0;

    // Token: 0x0400025A RID: 602
    private const double Inset = 12.0;

    // Token: 0x0400025B RID: 603
    private static readonly Color Green = DS.Hex(3458905U, 1.0);

    // Token: 0x0400025C RID: 604
    private static readonly Color Orange = DS.Hex(16749824U, 1.0);

    // Token: 0x0400025D RID: 605
    private static readonly Color Blue = DS.Hex(31487U, 1.0);

    // Token: 0x0400025E RID: 606
    private readonly SettingsStore settings;

    // Token: 0x0400025F RID: 607
    private readonly DispatcherQueue dq;

    // Token: 0x04000260 RID: 608
    [Nullable(2)]
    private OverlayWindow window;

    // Token: 0x04000261 RID: 609
    [Nullable(2)]
    private StackPanel rows;

    // Token: 0x04000262 RID: 610
    [Nullable(2)]
    private DispatcherQueueTimer tick;

    // Token: 0x04000263 RID: 611
    private bool refreshQueued;

    // Token: 0x04000264 RID: 612
    private readonly bool keepOutOfCapture;
  }
}
