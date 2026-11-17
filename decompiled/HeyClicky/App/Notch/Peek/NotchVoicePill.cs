using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Notch.Surfaces;
using HeyClicky.App.UI;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000196 RID: 406
  [NullableContext(1)]
  [Nullable(0)]
  public static class NotchVoicePill
  {
    // Token: 0x06001602 RID: 5634 RVA: 0x005B9958 File Offset: 0x005B9958
    public static void Update(Grid layer, [TupleElementNames(new string[] { "Phase", "Label" })] [Nullable(new byte[] { 0, 2 })] ValueTuple<NotchActivityPhase, string>? phase, INotchHost host)
    {
      Grid grid = layer.Children.OfType<Grid>().FirstOrDefault<Grid>((Grid g) => g.Tag as string == "voice-pill");
      if (phase != null)
      {
        ValueTuple<NotchActivityPhase, string> valueOrDefault = phase.GetValueOrDefault();
        ActivityPhase activityPhase = (ActivityPhase)Enum.Parse(typeof(ActivityPhase), valueOrDefault.Item1.ToString());
        Border border = ((grid != null) ? grid.Children.LastOrDefault<UIElement>() : null) as Border;
        if (border != null)
        {
          Grid grid2 = border.Child as Grid;
          if (grid2 != null)
          {
            ActivitySurface activitySurface = grid2.Tag as ActivitySurface;
            if (activitySurface != null && activitySurface.Matches(activityPhase, valueOrDefault.Item2 ?? ""))
            {
              return;
            }
          }
        }
        if (grid != null)
        {
          border = grid.Children.LastOrDefault<UIElement>() as Border;
          if (border != null)
          {
            Grid grid2 = border.Child as Grid;
            if (grid2 != null)
            {
              ActivitySurface activitySurface2 = grid2.Tag as ActivitySurface;
              if (activitySurface2 != null)
              {
                activitySurface2.DetachPill();
              }
            }
          }
          layer.Children.Remove(grid);
        }
        ActivitySurface activitySurface3 = new ActivitySurface(activityPhase, valueOrDefault.Item2 ?? "", false, () => DS.CursorColor(host.Services.Settings.CursorColorHex), new double?((double)12));
        activitySurface3.AttachAsPill(host.Services);
        Grid grid3 = new Grid
        {
          Height = 24.0,
          Tag = activitySurface3,
          Children = { activitySurface3.View }
        };
        Border border2 = new Border
        {
          Background = Ui.Brush(Colors.Black),
          BorderBrush = Ui.Brush(DS.White(0.14)),
          BorderThickness = new Thickness(1.0),
          CornerRadius = new CornerRadius(12.0),
          Child = grid3
        };
        Grid pill = new Grid
        {
          Tag = "voice-pill",
          HorizontalAlignment = HorizontalAlignment.Center,
          VerticalAlignment = VerticalAlignment.Top
        };
        pill.Children.Add(new ShadowLayer(12.0, DS.Black(0.45), 10.0, 3.0, 0.0));
        pill.Children.Add(border2);
        float num = 0.86f;
        pill.Scale = new Vector3(num, num, 1f);
        pill.SizeChanged += delegate(object _, SizeChangedEventArgs e)
        {
          pill.CenterPoint = new Vector3((float)e.NewSize.Width / 2f, 0f, 0f);
        };
        layer.Children.Add(pill);
        if (Anim.ReduceMotion)
        {
          pill.Opacity = 0.0;
          Anim.Opacity(pill, 1f, 0.15, Easing.EaseOut(Ui.Compositor), 0.0);
          return;
        }
        SwiftSpring swiftSpring = new SwiftSpring(0.32, 0.82);
        pill.Opacity = 0.0;
        pill.Scale = new Vector3(num * 0.8f, num * 0.8f, 1f);
        Anim.ScaleSpring(pill, num, swiftSpring, null);
        Anim.OpacitySpring(pill, 1f, swiftSpring);
        return;
      }
      else
      {
        if (grid == null)
        {
          return;
        }
        grid.Tag = "voice-pill-leaving";
        Border border = grid.Children.LastOrDefault<UIElement>() as Border;
        if (border != null)
        {
          Grid grid2 = border.Child as Grid;
          if (grid2 != null)
          {
            ActivitySurface activitySurface4 = grid2.Tag as ActivitySurface;
            if (activitySurface4 != null)
            {
              activitySurface4.DetachPill();
            }
          }
        }
        NotchVoicePill.Leave(layer, grid);
        return;
      }
    }

    // Token: 0x06001603 RID: 5635 RVA: 0x005B9D38 File Offset: 0x005B9D38
    private static void Leave(Grid layer, Grid pill)
    {
      SwiftSpring swiftSpring = new SwiftSpring(0.32, 0.82);
      float num = 0.86f;
      if (!Anim.ReduceMotion)
      {
        Anim.ScaleSpring(pill, num * 0.8f, swiftSpring, null);
      }
      Anim.Opacity(pill, 0f, Anim.ReduceMotion ? 0.15 : 0.2, null, 0.0);
      DispatcherQueueTimer dispatcherQueueTimer = layer.DispatcherQueue.CreateTimer().Rooted();
      dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(0.3);
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        layer.Children.Remove(pill);
      };
      dispatcherQueueTimer.Start();
    }
  }
}
