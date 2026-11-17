using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Interop;
using HeyClicky.App.Shell;
using HeyClicky.Core.Notch;
using Microsoft.UI;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000173 RID: 371
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class NotchChatResizeGrip : Grid
  {
    // Token: 0x0600148C RID: 5260 RVA: 0x005ACD78 File Offset: 0x005ACD78
    public NotchChatResizeGrip(INotchHost host, bool left, bool right, bool bottom)
    {
      this.host = host;
      this.left = left;
      this.right = right;
      this.bottom = bottom;
      base.Background = new SolidColorBrush(Colors.Transparent);
      base.ProtectedCursor = InputSystemCursor.Create((left && bottom) ? InputSystemCursorShape.SizeNortheastSouthwest : ((right && bottom) ? InputSystemCursorShape.SizeNorthwestSoutheast : (bottom ? InputSystemCursorShape.SizeNorthSouth : InputSystemCursorShape.SizeWestEast)));
      base.PointerPressed += this.OnPressed;
      base.PointerMoved += this.OnMoved;
      base.PointerReleased += delegate(object _, PointerRoutedEventArgs e)
      {
        this.End(e.Pointer);
      };
      base.PointerCaptureLost += delegate(object _, PointerRoutedEventArgs e)
      {
        this.End(e.Pointer);
      };
      base.DoubleTapped += delegate(object _, DoubleTappedRoutedEventArgs e)
      {
        e.Handled = true;
        NotchChatSize.ResetToDefault();
      };
    }

    // Token: 0x0600148D RID: 5261 RVA: 0x005ACE5C File Offset: 0x005ACE5C
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "X", "Y" })]
    private static ValueTuple<int, int> Cursor()
    {
      Native.POINT point;
      Native.GetCursorPos(out point);
      return new ValueTuple<int, int>(point.X, point.Y);
    }

    // Token: 0x0600148E RID: 5262 RVA: 0x005ACE84 File Offset: 0x005ACE84
    private void OnPressed(object sender, PointerRoutedEventArgs e)
    {
      if (!e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
      {
        return;
      }
      e.Handled = true;
      base.CapturePointer(e.Pointer);
      this.dragStartPx = new ValueTuple<int, int>?(NotchChatResizeGrip.Cursor());
      this.scale = Displays.ForWindow(this.host.WindowHandle).Scale;
      this.dragStartWidth = NotchChatSize.ContentWidth;
      this.dragStartHeight = NotchChatSize.BodyHeight;
      NotchPresenter notchPresenter = this.host.Services.Presenter as NotchPresenter;
      if (notchPresenter != null)
      {
        notchPresenter.SetNotchChatBeingResized(true);
      }
      NotchChatSize.BeginResizing();
    }

    // Token: 0x0600148F RID: 5263 RVA: 0x005ACF24 File Offset: 0x005ACF24
    private void OnMoved(object sender, PointerRoutedEventArgs e)
    {
      ValueTuple<int, int>? valueTuple = this.dragStartPx;
      if (valueTuple != null)
      {
        ValueTuple<int, int> valueOrDefault = valueTuple.GetValueOrDefault();
        e.Handled = true;
        ValueTuple<int, int> valueTuple2 = NotchChatResizeGrip.Cursor();
        double num = (double)(valueTuple2.Item1 - valueOrDefault.Item1) / this.scale;
        double num2 = (double)(valueTuple2.Item2 - valueOrDefault.Item2) / this.scale;
        double num3 = this.dragStartWidth;
        double num4 = this.dragStartHeight;
        if (this.right)
        {
          num3 += 2.0 * num;
        }
        if (this.left)
        {
          num3 -= 2.0 * num;
        }
        if (this.bottom)
        {
          num4 += num2;
        }
        NotchChatSize.Resize(num3, num4);
        return;
      }
    }

    // Token: 0x06001490 RID: 5264 RVA: 0x005ACFD8 File Offset: 0x005ACFD8
    private void End(Pointer pointer)
    {
      ValueTuple<int, int>? valueTuple = this.dragStartPx;
      if (valueTuple == null)
      {
        return;
      }
      this.dragStartPx = null;
      base.ReleasePointerCapture(pointer);
      NotchChatSize.EndResizing();
      NotchPresenter notchPresenter = this.host.Services.Presenter as NotchPresenter;
      if (notchPresenter == null)
      {
        return;
      }
      notchPresenter.SetNotchChatBeingResized(false);
    }

    // Token: 0x040009DE RID: 2526
    private readonly INotchHost host;

    // Token: 0x040009DF RID: 2527
    private readonly bool left;

    // Token: 0x040009E0 RID: 2528
    private readonly bool right;

    // Token: 0x040009E1 RID: 2529
    private readonly bool bottom;

    // Token: 0x040009E2 RID: 2530
    [TupleElementNames(new string[] { "X", "Y" })]
    [Nullable(0)]
    private ValueTuple<int, int>? dragStartPx;

    // Token: 0x040009E3 RID: 2531
    private double dragStartWidth;

    // Token: 0x040009E4 RID: 2532
    private double dragStartHeight;

    // Token: 0x040009E5 RID: 2533
    private double scale = 1.0;
  }
}
