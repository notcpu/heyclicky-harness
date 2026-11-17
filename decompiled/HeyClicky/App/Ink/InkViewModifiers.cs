using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000262 RID: 610
  [NullableContext(1)]
  [Nullable(0)]
  public static class InkViewModifiers
  {
    // Token: 0x06001A87 RID: 6791 RVA: 0x005E2F38 File Offset: 0x005E2F38
    public static T Padding<[Nullable(0)] T>(this T e, double left, double top, double right, double bottom) where T : FrameworkElement
    {
      Thickness margin = e.Margin;
      e.Margin = new Thickness(margin.Left + left, margin.Top + top, margin.Right + right, margin.Bottom + bottom);
      return e;
    }

    // Token: 0x06001A88 RID: 6792 RVA: 0x005E2F88 File Offset: 0x005E2F88
    public static T WithOpacity<[Nullable(0)] T>(this T e, double opacity) where T : UIElement
    {
      ref T ptr = ref e;
      ptr.Opacity *= Math.Clamp(opacity, 0.0, 1.0);
      return e;
    }

    // Token: 0x06001A89 RID: 6793 RVA: 0x005E2FD0 File Offset: 0x005E2FD0
    public static T Transformed<[Nullable(0)] T>(this T e, double x = 0.0, double y = 0.0, double degrees = 0.0, double scale = 1.0, Point? anchor = null) where T : UIElement
    {
      TransformGroup transformGroup = (e.RenderTransform as TransformGroup) ?? new TransformGroup();
      if (scale != 1.0)
      {
        transformGroup.Children.Add(new ScaleTransform
        {
          ScaleX = scale,
          ScaleY = scale
        });
      }
      if (degrees != 0.0)
      {
        transformGroup.Children.Add(new RotateTransform
        {
          Angle = degrees
        });
      }
      if (x != 0.0 || y != 0.0)
      {
        transformGroup.Children.Add(new TranslateTransform
        {
          X = x,
          Y = y
        });
      }
      e.RenderTransform = transformGroup;
      e.RenderTransformOrigin = anchor ?? new Point(0.5, 0.5);
      return e;
    }

    // Token: 0x06001A8A RID: 6794 RVA: 0x005E30C4 File Offset: 0x005E30C4
    public static T Frame<[Nullable(0)] T>(this T e, double? width = null, double? height = null) where T : FrameworkElement
    {
      if (width != null)
      {
        double valueOrDefault = width.GetValueOrDefault();
        e.Width = Math.Max(0.0, valueOrDefault);
      }
      if (height != null)
      {
        double valueOrDefault2 = height.GetValueOrDefault();
        e.Height = Math.Max(0.0, valueOrDefault2);
      }
      return e;
    }

    // Token: 0x06001A8B RID: 6795 RVA: 0x005E3128 File Offset: 0x005E3128
    public static FrameworkElement Column(this FrameworkElement e, double width)
    {
      e.HorizontalAlignment = HorizontalAlignment.Left;
      return new Grid
      {
        Width = Math.Max(0.0, width),
        HorizontalAlignment = HorizontalAlignment.Left,
        Children = { e }
      };
    }
  }
}
