using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001FB RID: 507
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickySketchSlider : Grid
  {
    // Token: 0x0600185F RID: 6239 RVA: 0x005CAF84 File Offset: 0x005CAF84
    public ClickySketchSlider(IReadOnlyList<string> stops, int selectedIndex, Action<int> onSelect, double width = 262.0, long seed = 11L)
    {
      ClickySketchSlider <>4__this = this;
      this.stops = stops;
      this.selectedIndex = selectedIndex;
      this.onSelect = onSelect;
      this.width = width;
      this.seed = seed;
      base.Width = width;
      base.Height = 52.0;
      base.HorizontalAlignment = HorizontalAlignment.Left;
      base.VerticalAlignment = VerticalAlignment.Top;
      base.Background = Ui.Transparent;
      base.ProtectedCursor = Pressable.ClickableCursor;
      base.Children.Add(this.canvas);
      this.Draw();
      bool dragging = false;
      base.PointerPressed += delegate(object _, PointerRoutedEventArgs e)
      {
        dragging = true;
        <>4__this.CapturePointer(e.Pointer);
        <>4__this.Pick(e.GetCurrentPoint(<>4__this).Position.X);
        e.Handled = true;
      };
      base.PointerMoved += delegate(object _, PointerRoutedEventArgs e)
      {
        if (dragging)
        {
          <>4__this.Pick(e.GetCurrentPoint(<>4__this).Position.X);
        }
      };
      base.PointerReleased += delegate(object _, PointerRoutedEventArgs e)
      {
        dragging = false;
        <>4__this.ReleasePointerCapture(e.Pointer);
      };
      AutomationProperties.SetName(this, "Speed");
    }

    // Token: 0x1700044D RID: 1101
    // (get) Token: 0x06001860 RID: 6240 RVA: 0x005CB07C File Offset: 0x005CB07C
    // (set) Token: 0x06001861 RID: 6241 RVA: 0x005CB084 File Offset: 0x005CB084
    public int SelectedIndex
    {
      get
      {
        return this.selectedIndex;
      }
      set
      {
        if (this.selectedIndex != value)
        {
          this.selectedIndex = value;
          this.Draw();
        }
      }
    }

    // Token: 0x1700044E RID: 1102
    // (get) Token: 0x06001862 RID: 6242 RVA: 0x005CB09C File Offset: 0x005CB09C
    private double[] Xs
    {
      get
      {
        if (this.stops.Count <= 1)
        {
          return new double[] { this.width / 2.0 };
        }
        return (from i in Enumerable.Range(0, this.stops.Count)
          select 12.0 + (double)i * (this.width - 24.0) / (double)(this.stops.Count - 1)).ToArray<double>();
      }
    }

    // Token: 0x06001863 RID: 6243 RVA: 0x005CB0F8 File Offset: 0x005CB0F8
    private void Pick(double x)
    {
      double[] xs = this.Xs;
      int num = this.selectedIndex;
      double num2 = double.MaxValue;
      for (int i = 0; i < xs.Length; i++)
      {
        if (Math.Abs(xs[i] - x) < num2)
        {
          num2 = Math.Abs(xs[i] - x);
          num = i;
        }
      }
      if (num != this.selectedIndex)
      {
        this.onSelect(num);
      }
    }

    // Token: 0x06001864 RID: 6244 RVA: 0x005CB15C File Offset: 0x005CB15C
    private void Draw()
    {
      this.canvas.Children.Clear();
      double[] xs = this.Xs;
      PaperDrawing paperDrawing = new PaperDrawing();
      PaperDrawing paperDrawing2 = paperDrawing;
      Point point = new Point(xs.FirstOrDefault(12.0), 14.0);
      double num;
      if (xs.Length == 0)
      {
        num = this.width - 12.0;
      }
      else
      {
        double[] array = xs;
        num = array[array.Length - 1];
      }
      paperDrawing2.Stroke(ClickySketchPath.Line(point, new Point(num, 14.0), this.seed, 0.5, 10.0), new PaperStroke(PaperColor.Hex("#B3C7DF"), 5.0, PenLineCap.Round, PenLineJoin.Miter, null));
      PaperPath paperPath = new PaperPath();
      foreach (double num2 in xs)
      {
        paperPath.MoveTo(num2, 9.0).LineTo(num2, 19.0);
      }
      paperDrawing.Stroke(paperPath, new PaperStroke(PaperColor.Hex("#7F96B4"), 1.4, PenLineCap.Round, PenLineJoin.Miter, null));
      this.canvas.Children.Add(paperDrawing);
      PaperPath paperPath2 = new ClickySketchRect(10.0, this.seed + 3L, 0.35).PathIn(new Rect(0f, 0f, 20f, 20f));
      PaperDrawing paperDrawing3 = new PaperDrawing().Fill(paperPath2, PaperColor.Hex("#FBFDFF")).Stroke(paperPath2, new PaperStroke(ClickySketchInk.TealLine, 1.6, PenLineCap.Flat, PenLineJoin.Miter, null));
      Canvas.SetLeft(paperDrawing3, xs[Math.Clamp(this.selectedIndex, 0, xs.Length - 1)] - 10.0);
      Canvas.SetTop(paperDrawing3, 4.0);
      this.canvas.Children.Add(paperDrawing3);
      for (int j = 0; j < this.stops.Count; j++)
      {
        ClickyHandText clickyHandText = new ClickyHandText(this.stops[j], 16.0, 1.0, new int?(1), 1.0, TextAlignment.Center)
        {
          Width = 48.0
        }.WithForeground((j == this.selectedIndex) ? PaperColor.Hex("#22395A") : PaperColor.Hex("#6E86A6"));
        Canvas.SetLeft(clickyHandText, xs[j] - 24.0);
        Canvas.SetTop(clickyHandText, 30.0);
        this.canvas.Children.Add(clickyHandText);
      }
    }

    // Token: 0x04000C0B RID: 3083
    private readonly IReadOnlyList<string> stops;

    // Token: 0x04000C0C RID: 3084
    private readonly double width;

    // Token: 0x04000C0D RID: 3085
    private readonly long seed;

    // Token: 0x04000C0E RID: 3086
    private readonly Action<int> onSelect;

    // Token: 0x04000C0F RID: 3087
    private int selectedIndex;

    // Token: 0x04000C10 RID: 3088
    private readonly Canvas canvas = new Canvas();
  }
}
