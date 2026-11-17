using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200025C RID: 604
  [NullableContext(1)]
  [Nullable(0)]
  public static class InkBaseline
  {
    // Token: 0x06001A6E RID: 6766 RVA: 0x005E2090 File Offset: 0x005E2090
    public static void SetFromBottom(UIElement e, double value)
    {
      e.SetValue(InkBaseline.FromBottomProperty, value);
    }

    // Token: 0x06001A6F RID: 6767 RVA: 0x005E20A4 File Offset: 0x005E20A4
    public static double GetFromBottom(UIElement e)
    {
      return (double)e.GetValue(InkBaseline.FromBottomProperty);
    }

    // Token: 0x06001A70 RID: 6768 RVA: 0x005E20B8 File Offset: 0x005E20B8
    public static T BaselineFromBottom<[Nullable(0)] T>(this T e, double value) where T : UIElement
    {
      InkBaseline.SetFromBottom(e, value);
      return e;
    }

    // Token: 0x06001A71 RID: 6769 RVA: 0x005E20C8 File Offset: 0x005E20C8
    public static double? First(UIElement element)
    {
      FrameworkElement frameworkElement = element as FrameworkElement;
      double num = ((frameworkElement != null) ? frameworkElement.Margin.Top : 0.0);
      double fromBottom = InkBaseline.GetFromBottom(element);
      if (!double.IsNaN(fromBottom))
      {
        double height = element.DesiredSize.Height;
        FrameworkElement frameworkElement2 = element as FrameworkElement;
        double num2 = height - ((frameworkElement2 != null) ? (frameworkElement2.Margin.Top + frameworkElement2.Margin.Bottom) : 0.0);
        return new double?(num + num2 - fromBottom);
      }
      IInkBaseline inkBaseline = element as IInkBaseline;
      if (inkBaseline == null)
      {
        TextBlock textBlock = element as TextBlock;
        if (textBlock != null)
        {
          return new double?(num + textBlock.BaselineOffset);
        }
        ClickyHandText clickyHandText = element as ClickyHandText;
        if (clickyHandText == null)
        {
          Border border = element as Border;
          if (border == null)
          {
            StackPanel stackPanel = element as StackPanel;
            Panel panel;
            if (stackPanel != null)
            {
              if (stackPanel.Orientation == Orientation.Vertical)
              {
                foreach (UIElement uielement in stackPanel.Children)
                {
                  if (uielement.Visibility != Visibility.Collapsed)
                  {
                    double? num3 = InkBaseline.First(uielement);
                    double? num4;
                    if (num3 != null)
                    {
                      double valueOrDefault = num3.GetValueOrDefault();
                      num4 = new double?(num + stackPanel.Padding.Top + valueOrDefault);
                    }
                    else
                    {
                      num3 = null;
                      num4 = num3;
                    }
                    return num4;
                  }
                }
                return null;
              }
              panel = (Panel)element;
            }
            else
            {
              panel = element as Panel;
              if (panel == null)
              {
                goto IL_02A4;
              }
            }
            foreach (UIElement uielement2 in panel.Children)
            {
              double? num5 = InkBaseline.First(uielement2);
              if (num5 != null)
              {
                double valueOrDefault2 = num5.GetValueOrDefault();
                return new double?(num + valueOrDefault2);
              }
            }
            return null;
          }
          UIElement child = border.Child;
          if (child != null)
          {
            double? num3 = InkBaseline.First(child);
            if (num3 != null)
            {
              double valueOrDefault3 = num3.GetValueOrDefault();
              return new double?(num + border.Padding.Top + border.BorderThickness.Top + valueOrDefault3);
            }
            num3 = null;
            return num3;
          }
          IL_02A4:
          return null;
        }
        return new double?(num + clickyHandText.FirstBaseline);
      }
      else
      {
        double? num3 = inkBaseline.FirstBaseline;
        if (num3 != null)
        {
          double valueOrDefault4 = num3.GetValueOrDefault();
          return new double?(num + valueOrDefault4);
        }
        num3 = null;
        return num3;
      }
    }

    // Token: 0x04000CB3 RID: 3251
    public static readonly DependencyProperty FromBottomProperty = DependencyProperty.RegisterAttached("FromBottom", typeof(double), typeof(InkBaseline), new PropertyMetadata(double.NaN));
  }
}
