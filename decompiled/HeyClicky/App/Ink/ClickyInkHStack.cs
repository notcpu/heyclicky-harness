using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200025F RID: 607
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkHStack : Panel, IInkBaseline
  {
    // Token: 0x17000493 RID: 1171
    // (get) Token: 0x06001A74 RID: 6772 RVA: 0x005E23DC File Offset: 0x005E23DC
    // (set) Token: 0x06001A75 RID: 6773 RVA: 0x005E23E4 File Offset: 0x005E23E4
    public double Spacing { get; set; }

    // Token: 0x17000494 RID: 1172
    // (get) Token: 0x06001A76 RID: 6774 RVA: 0x005E23F0 File Offset: 0x005E23F0
    // (set) Token: 0x06001A77 RID: 6775 RVA: 0x005E23F8 File Offset: 0x005E23F8
    public InkVerticalAlignment Alignment { get; set; } = InkVerticalAlignment.Center;

    // Token: 0x06001A78 RID: 6776 RVA: 0x005E2404 File Offset: 0x005E2404
    public static T Flexible<[Nullable(0)] T>(T e) where T : UIElement
    {
      e.SetValue(ClickyInkHStack.FlexibleProperty, true);
      return e;
    }

    // Token: 0x06001A79 RID: 6777 RVA: 0x005E2420 File Offset: 0x005E2420
    public ClickyInkHStack(double spacing = 8.0, InkVerticalAlignment alignment = InkVerticalAlignment.Center, params UIElement[] children)
    {
      this.Spacing = spacing;
      this.Alignment = alignment;
      foreach (UIElement uielement in children)
      {
        base.Children.Add(uielement);
      }
    }

    // Token: 0x17000495 RID: 1173
    // (get) Token: 0x06001A7A RID: 6778 RVA: 0x005E2468 File Offset: 0x005E2468
    public double? FirstBaseline
    {
      get
      {
        return this.firstBaseline;
      }
    }

    // Token: 0x06001A7B RID: 6779 RVA: 0x005E2470 File Offset: 0x005E2470
    private static bool IsFlexible(UIElement e)
    {
      if (!(bool)e.GetValue(ClickyInkHStack.FlexibleProperty) && !(e is InkTextView) && !(e is ClickyHandText) && !(e is ClickyInkSpacer))
      {
        TextBlock textBlock = e as TextBlock;
        return textBlock != null && textBlock.TextWrapping != TextWrapping.NoWrap;
      }
      return true;
    }

    // Token: 0x06001A7C RID: 6780 RVA: 0x005E24C4 File Offset: 0x005E24C4
    protected override Size MeasureOverride(Size availableSize)
    {
      List<UIElement> list = base.Children.Where<UIElement>((UIElement c) => c.Visibility == Visibility.Visible).ToList<UIElement>();
      double num = this.Spacing * (double)Math.Max(0, list.Count - 1);
      foreach (UIElement uielement in list.Where<UIElement>((UIElement c) => !ClickyInkHStack.IsFlexible(c)))
      {
        uielement.Measure(new Size(double.PositiveInfinity, availableSize.Height));
        num += uielement.DesiredSize.Width;
      }
      double num2 = Math.Max(0.0, availableSize.Width - num);
      foreach (UIElement uielement2 in list.Where<UIElement>((UIElement c) => ClickyInkHStack.IsFlexible(c) && !(c is ClickyInkSpacer)))
      {
        uielement2.Measure(new Size(double.IsInfinity(availableSize.Width) ? double.PositiveInfinity : num2, availableSize.Height));
        num2 = Math.Max(0.0, num2 - uielement2.DesiredSize.Width);
        num += uielement2.DesiredSize.Width;
      }
      foreach (ClickyInkSpacer clickyInkSpacer in list.OfType<ClickyInkSpacer>())
      {
        clickyInkSpacer.Measure(new Size(0f, 0f));
      }
      double num3 = ((list.Any<UIElement>((UIElement c) => c is ClickyInkSpacer) && !double.IsInfinity(availableSize.Width)) ? Math.Max(num, availableSize.Width) : num);
      Dictionary<UIElement, double> dictionary;
      ValueTuple<double, double?> valueTuple = this.VerticalLayout(list, out dictionary);
      double item = valueTuple.Item1;
      double? item2 = valueTuple.Item2;
      this.firstBaseline = item2;
      return new Size(num3, item);
    }

    // Token: 0x06001A7D RID: 6781 RVA: 0x005E2730 File Offset: 0x005E2730
    [return: TupleElementNames(new string[] { "Height", "Baseline" })]
    [return: Nullable(0)]
    private ValueTuple<double, double?> VerticalLayout(List<UIElement> visible, out Dictionary<UIElement, double> tops)
    {
      tops = new Dictionary<UIElement, double>();
      if (this.Alignment == InkVerticalAlignment.FirstBaseline)
      {
        double num = 0.0;
        double num2 = 0.0;
        Dictionary<UIElement, double> dictionary = new Dictionary<UIElement, double>();
        foreach (UIElement uielement in visible)
        {
          double num3 = InkBaseline.First(uielement) ?? uielement.DesiredSize.Height;
          dictionary[uielement] = num3;
          num = Math.Max(num, num3);
          num2 = Math.Max(num2, uielement.DesiredSize.Height - num3);
        }
        foreach (UIElement uielement2 in visible)
        {
          tops[uielement2] = num - dictionary[uielement2];
        }
        return new ValueTuple<double, double?>(num + num2, (visible.Count == 0) ? null : new double?(num));
      }
      double num4;
      if (visible.Count != 0)
      {
        num4 = visible.Max<UIElement>((UIElement c) => c.DesiredSize.Height);
      }
      else
      {
        num4 = 0.0;
      }
      double num5 = num4;
      double? num6 = null;
      foreach (UIElement uielement3 in visible)
      {
        InkVerticalAlignment alignment = this.Alignment;
        double num7;
        if (alignment != InkVerticalAlignment.Top)
        {
          if (alignment != InkVerticalAlignment.Bottom)
          {
            num7 = (num5 - uielement3.DesiredSize.Height) / 2.0;
          }
          else
          {
            num7 = num5 - uielement3.DesiredSize.Height;
          }
        }
        else
        {
          num7 = 0.0;
        }
        double num8 = num7;
        tops[uielement3] = num8;
        if (num6 == null)
        {
          double? num9 = InkBaseline.First(uielement3);
          if (num9 != null)
          {
            double valueOrDefault = num9.GetValueOrDefault();
            num6 = new double?(num8 + valueOrDefault);
          }
        }
      }
      return new ValueTuple<double, double?>(num5, num6);
    }

    // Token: 0x06001A7E RID: 6782 RVA: 0x005E298C File Offset: 0x005E298C
    protected override Size ArrangeOverride(Size finalSize)
    {
      List<UIElement> list = base.Children.Where<UIElement>((UIElement c) => c.Visibility == Visibility.Visible).ToList<UIElement>();
      double num = list.Where<UIElement>((UIElement c) => !(c is ClickyInkSpacer)).Sum<UIElement>((UIElement c) => c.DesiredSize.Width) + this.Spacing * (double)Math.Max(0, list.Count - 1);
      int num2 = list.Count<UIElement>((UIElement c) => c is ClickyInkSpacer);
      double num3 = ((num2 == 0) ? 0.0 : (Math.Max(0.0, finalSize.Width - num) / (double)num2));
      Dictionary<UIElement, double> dictionary;
      this.VerticalLayout(list, out dictionary);
      double num4 = 0.0;
      foreach (UIElement uielement in list)
      {
        double num5 = ((uielement is ClickyInkSpacer) ? num3 : uielement.DesiredSize.Width);
        uielement.Arrange(new Rect(num4, dictionary[uielement], num5, uielement.DesiredSize.Height));
        num4 += num5 + this.Spacing;
      }
      foreach (UIElement uielement2 in base.Children.Where<UIElement>((UIElement c) => c.Visibility > Visibility.Visible))
      {
        uielement2.Arrange(new Rect(0f, 0f, 0f, 0f));
      }
      return finalSize;
    }

    // Token: 0x04000CBB RID: 3259
    private double? firstBaseline;

    // Token: 0x04000CBC RID: 3260
    public static readonly DependencyProperty FlexibleProperty = DependencyProperty.RegisterAttached("Flexible", typeof(bool), typeof(ClickyInkHStack), new PropertyMetadata(false));
  }
}
