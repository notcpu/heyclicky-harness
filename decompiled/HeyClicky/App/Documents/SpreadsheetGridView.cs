using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using HeyClicky.App.UI;
using HeyClicky.Core.Documents.Spreadsheet;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002E1 RID: 737
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SpreadsheetGridView : UserControl
  {
    // Token: 0x14000077 RID: 119
    // (add) Token: 0x06001F2B RID: 7979 RVA: 0x0060C948 File Offset: 0x0060C948
    // (remove) Token: 0x06001F2C RID: 7980 RVA: 0x0060C980 File Offset: 0x0060C980
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event Action<SpreadsheetSelectionSummary> SelectionChanged;

    // Token: 0x14000078 RID: 120
    // (add) Token: 0x06001F2D RID: 7981 RVA: 0x0060C9B8 File Offset: 0x0060C9B8
    // (remove) Token: 0x06001F2E RID: 7982 RVA: 0x0060C9F0 File Offset: 0x0060C9F0
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event Action<string> OpenHyperlink;

    // Token: 0x170005A0 RID: 1440
    // (get) Token: 0x06001F2F RID: 7983 RVA: 0x0060CA28 File Offset: 0x0060CA28
    // (set) Token: 0x06001F30 RID: 7984 RVA: 0x0060CA30 File Offset: 0x0060CA30
    [Nullable(2)]
    public SpreadsheetSheet Sheet
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x06001F31 RID: 7985 RVA: 0x0060CA3C File Offset: 0x0060CA3C
    public SpreadsheetGridView()
    {
      base.IsTabStop = true;
      base.UseSystemFocusVisuals = false;
      this.scrollView.Content = this.bodySpacer;
      this.root.Children.Add(this.canvas);
      this.root.Children.Add(this.scrollView);
      base.Content = this.root;
      this.canvas.Draw += delegate(CanvasControl _, CanvasDrawEventArgs e)
      {
        this.DrawPanes(e.DrawingSession);
      };
      this.scrollView.ViewChanging += delegate([Nullable(2)] object _, ScrollViewerViewChangingEventArgs e)
      {
        this.originX = e.NextView.HorizontalOffset;
        this.originY = e.NextView.VerticalOffset;
        this.canvas.Invalidate();
      };
      this.scrollView.ViewChanged += delegate([Nullable(2)] object _, ScrollViewerViewChangedEventArgs _)
      {
        this.originX = this.scrollView.HorizontalOffset;
        this.originY = this.scrollView.VerticalOffset;
        this.canvas.Invalidate();
      };
      base.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        this.ApplyGeometry();
      };
      this.root.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(this.OnPointerPressed), true);
      this.root.AddHandler(UIElement.PointerMovedEvent, new PointerEventHandler(this.OnPointerMoved), true);
      this.root.AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(this.OnPointerReleased), true);
      this.root.AddHandler(UIElement.PointerCaptureLostEvent, new PointerEventHandler(delegate(object _, PointerRoutedEventArgs _)
      {
        this.dragMode = null;
      }), true);
      this.root.PointerExited += delegate(object _, PointerRoutedEventArgs _)
      {
        if (this.dragMode == null)
        {
          base.ProtectedCursor = null;
        }
      };
      this.root.PointerWheelChanged += this.OnWheelOutsideBody;
      base.KeyDown += this.OnKeyDown;
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        SpreadsheetGridRenderer spreadsheetGridRenderer = this.renderer;
        if (spreadsheetGridRenderer == null)
        {
          return;
        }
        spreadsheetGridRenderer.Dispose();
      };
    }

    // Token: 0x06001F32 RID: 7986 RVA: 0x0060CC7C File Offset: 0x0060CC7C
    public void Show(SpreadsheetSheet newSheet)
    {
      if (newSheet == this.Sheet)
      {
        return;
      }
      this.Sheet = newSheet;
      SpreadsheetGridRenderer spreadsheetGridRenderer = this.renderer;
      if (spreadsheetGridRenderer != null)
      {
        spreadsheetGridRenderer.Dispose();
      }
      this.layout = new SpreadsheetGridLayout(newSheet, SpreadsheetTextAttributes.Shared);
      this.renderer = new SpreadsheetGridRenderer(this.layout);
      this.anchor = new SpreadsheetCellAddress(0, 0);
      this.extent = this.anchor;
      this.ApplyGeometry();
      this.scrollView.ChangeView(new double?(0.0), new double?(0.0), null, true);
      this.originX = (this.originY = 0.0);
      this.UpdateSelection(false);
    }

    // Token: 0x06001F33 RID: 7987 RVA: 0x0060CD40 File Offset: 0x0060CD40
    public void Select(SpreadsheetCellRange range)
    {
      this.anchor = new SpreadsheetCellAddress(range.FirstRow, range.FirstColumn);
      this.extent = new SpreadsheetCellAddress(range.LastRow, range.LastColumn);
      this.UpdateSelection(false);
    }

    // Token: 0x170005A1 RID: 1441
    // (get) Token: 0x06001F34 RID: 7988 RVA: 0x0060CD7C File Offset: 0x0060CD7C
    private double FixedTop
    {
      get
      {
        if (this.layout != null)
        {
          return this.layout.ColumnHeaderHeight + this.layout.FrozenHeight;
        }
        return 0.0;
      }
    }

    // Token: 0x170005A2 RID: 1442
    // (get) Token: 0x06001F35 RID: 7989 RVA: 0x0060CDA8 File Offset: 0x0060CDA8
    private double FixedLeft
    {
      get
      {
        if (this.layout != null)
        {
          return this.layout.RowHeaderWidth + this.layout.FrozenWidth;
        }
        return 0.0;
      }
    }

    // Token: 0x06001F36 RID: 7990 RVA: 0x0060CDD4 File Offset: 0x0060CDD4
    private void ApplyGeometry()
    {
      if (this.layout == null)
      {
        return;
      }
      this.scrollView.Margin = new Thickness(this.FixedLeft, this.FixedTop, 0.0, 0.0);
      this.bodySpacer.Width = Math.Max(this.layout.TotalWidth - this.layout.FrozenWidth, 1.0);
      this.bodySpacer.Height = Math.Max(this.layout.TotalHeight - this.layout.FrozenHeight, 1.0);
      this.canvas.Invalidate();
    }

    // Token: 0x170005A3 RID: 1443
    // (get) Token: 0x06001F37 RID: 7991 RVA: 0x0060CE84 File Offset: 0x0060CE84
    private double ViewportWidth
    {
      get
      {
        return Math.Max(0.0, base.ActualWidth - this.FixedLeft);
      }
    }

    // Token: 0x170005A4 RID: 1444
    // (get) Token: 0x06001F38 RID: 7992 RVA: 0x0060CEA4 File Offset: 0x0060CEA4
    private double ViewportHeight
    {
      get
      {
        return Math.Max(0.0, base.ActualHeight - this.FixedTop);
      }
    }

    // Token: 0x06001F39 RID: 7993 RVA: 0x0060CEC4 File Offset: 0x0060CEC4
    private void DrawPanes(CanvasDrawingSession ds)
    {
      SpreadsheetGridView.<>c__DisplayClass43_0 CS$<>8__locals1;
      CS$<>8__locals1.ds = ds;
      CS$<>8__locals1.<>4__this = this;
      if (this.layout == null || this.renderer == null)
      {
        return;
      }
      double columnHeaderHeight = this.layout.ColumnHeaderHeight;
      double rowHeaderWidth = this.layout.RowHeaderWidth;
      double fixedTop = this.FixedTop;
      double fixedLeft = this.FixedLeft;
      double actualWidth = base.ActualWidth;
      double actualHeight = base.ActualHeight;
      int frozenRowCount = this.layout.FrozenRowCount;
      int frozenColumnCount = this.layout.FrozenColumnCount;
      CS$<>8__locals1.oldTransform = CS$<>8__locals1.ds.Transform;
      double num = Math.Max(0.0, actualWidth - fixedLeft);
      double num2 = Math.Max(0.0, actualHeight - fixedTop);
      SpreadsheetRect spreadsheetRect = new SpreadsheetRect(this.originX, this.originY, num, num2);
      this.<DrawPanes>g__Pane|43_0(fixedLeft, fixedTop, num, num2, new SpreadsheetPaneGeometry(frozenRowCount, this.layout.RowCount, frozenColumnCount, this.layout.ColumnCount, -this.layout.FrozenWidth, -this.layout.FrozenHeight, 0.0, 0.0), spreadsheetRect, spreadsheetRect, false, false, ref CS$<>8__locals1);
      SpreadsheetRect spreadsheetRect2 = new SpreadsheetRect(0.0, 0.0, num, fixedTop);
      this.<DrawPanes>g__Pane|43_0(fixedLeft, 0.0, num, fixedTop, new SpreadsheetPaneGeometry(0, frozenRowCount, frozenColumnCount, this.layout.ColumnCount, -this.layout.FrozenWidth - this.originX, columnHeaderHeight, columnHeaderHeight, 0.0), spreadsheetRect2, spreadsheetRect2, frozenRowCount > 0, false, ref CS$<>8__locals1);
      SpreadsheetRect spreadsheetRect3 = new SpreadsheetRect(0.0, 0.0, fixedLeft, num2);
      this.<DrawPanes>g__Pane|43_0(0.0, fixedTop, fixedLeft, num2, new SpreadsheetPaneGeometry(frozenRowCount, this.layout.RowCount, 0, frozenColumnCount, rowHeaderWidth, -this.layout.FrozenHeight - this.originY, 0.0, rowHeaderWidth), spreadsheetRect3, spreadsheetRect3, false, frozenColumnCount > 0, ref CS$<>8__locals1);
      SpreadsheetRect spreadsheetRect4 = new SpreadsheetRect(0.0, 0.0, fixedLeft, fixedTop);
      this.<DrawPanes>g__Pane|43_0(0.0, 0.0, fixedLeft, fixedTop, new SpreadsheetPaneGeometry(0, frozenRowCount, 0, frozenColumnCount, rowHeaderWidth, columnHeaderHeight, columnHeaderHeight, rowHeaderWidth), spreadsheetRect4, spreadsheetRect4, frozenRowCount > 0, frozenColumnCount > 0, ref CS$<>8__locals1);
    }

    // Token: 0x06001F3A RID: 7994 RVA: 0x0060D128 File Offset: 0x0060D128
    private Point ContentPoint(Point point)
    {
      if (this.layout == null)
      {
        return default(Point);
      }
      double num = point.X - this.layout.RowHeaderWidth;
      double num2 = point.Y - this.layout.ColumnHeaderHeight;
      double num3 = ((num < this.layout.FrozenWidth) ? num : (num + this.originX));
      double num4 = ((num2 < this.layout.FrozenHeight) ? num2 : (num2 + this.originY));
      return new Point(num3, num4);
    }

    // Token: 0x06001F3B RID: 7995 RVA: 0x0060D1A8 File Offset: 0x0060D1A8
    private SpreadsheetGridView.Hit HitAt(Point point)
    {
      if (this.layout == null)
      {
        return new SpreadsheetGridView.CornerHit();
      }
      Point point2 = this.ContentPoint(point);
      bool flag = point.Y < this.layout.ColumnHeaderHeight;
      bool flag2 = point.X < this.layout.RowHeaderWidth;
      if (flag && flag2)
      {
        return new SpreadsheetGridView.CornerHit();
      }
      if (flag)
      {
        return new SpreadsheetGridView.ColumnHeaderHit(this.layout.Column(point2.X), point2.X);
      }
      if (flag2)
      {
        return new SpreadsheetGridView.RowHeaderHit(this.layout.Row(point2.Y), point2.Y);
      }
      return new SpreadsheetGridView.CellHit(new SpreadsheetCellAddress(this.layout.Row(point2.Y), this.layout.Column(point2.X)));
    }

    // Token: 0x06001F3C RID: 7996 RVA: 0x0060D274 File Offset: 0x0060D274
    private int? ColumnResizeTarget(Point point)
    {
      if (this.layout != null)
      {
        SpreadsheetGridView.ColumnHeaderHit columnHeaderHit = this.HitAt(point) as SpreadsheetGridView.ColumnHeaderHit;
        if (columnHeaderHit != null)
        {
          int num;
          double num2;
          columnHeaderHit.Deconstruct(out num, out num2);
          if (Math.Abs(num2 - this.layout.ColumnOffsets[num + 1]) <= 4.0)
          {
            return new int?(num);
          }
          if (num > 0 && Math.Abs(num2 - this.layout.ColumnOffsets[num]) <= 4.0)
          {
            int num3 = num - 1;
            while (num3 > 0 && this.layout.ColumnWidths[num3] == 0.0)
            {
              num3--;
            }
            return new int?(num3);
          }
          return null;
        }
      }
      return null;
    }

    // Token: 0x06001F3D RID: 7997 RVA: 0x0060D344 File Offset: 0x0060D344
    private int? RowResizeTarget(Point point)
    {
      if (this.layout != null)
      {
        SpreadsheetGridView.RowHeaderHit rowHeaderHit = this.HitAt(point) as SpreadsheetGridView.RowHeaderHit;
        if (rowHeaderHit != null)
        {
          int num;
          double num2;
          rowHeaderHit.Deconstruct(out num, out num2);
          if (Math.Abs(num2 - this.layout.RowOffsets[num + 1]) <= 3.0)
          {
            return new int?(num);
          }
          if (num > 0 && Math.Abs(num2 - this.layout.RowOffsets[num]) <= 3.0)
          {
            return new int?(num - 1);
          }
          return null;
        }
      }
      return null;
    }

    // Token: 0x06001F3E RID: 7998
    [DllImport("user32.dll")]
    private static extern uint GetDoubleClickTime();

    // Token: 0x06001F3F RID: 7999 RVA: 0x0060D3E4 File Offset: 0x0060D3E4
    private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
    {
      if (this.layout == null || this.IsInScrollBar(e.OriginalSource as DependencyObject))
      {
        return;
      }
      PointerPoint currentPoint = e.GetCurrentPoint(this);
      if (!currentPoint.Properties.IsLeftButtonPressed)
      {
        return;
      }
      base.Focus(FocusState.Pointer);
      Point position = currentPoint.Position;
      DateTime utcNow = DateTime.UtcNow;
      this.clickCount = (((utcNow - this.lastPressAt).TotalMilliseconds <= SpreadsheetGridView.DoubleClickMilliseconds && Math.Abs(position.X - this.lastPressPoint.X) <= 4.0 && Math.Abs(position.Y - this.lastPressPoint.Y) <= 4.0) ? (this.clickCount + 1) : 1);
      this.lastPressAt = utcNow;
      this.lastPressPoint = position;
      bool flag = (e.KeyModifiers & VirtualKeyModifiers.Shift) > VirtualKeyModifiers.None;
      this.root.CapturePointer(e.Pointer);
      int? num = this.ColumnResizeTarget(position);
      if (num != null)
      {
        int valueOrDefault = num.GetValueOrDefault();
        if (this.clickCount >= 2)
        {
          this.layout.SetColumnWidth(this.layout.FittingWidth(valueOrDefault), valueOrDefault);
          SpreadsheetGridRenderer spreadsheetGridRenderer = this.renderer;
          if (spreadsheetGridRenderer != null)
          {
            spreadsheetGridRenderer.InvalidateTextMeasurements(new int?(valueOrDefault));
          }
          this.ApplyGeometry();
          this.dragMode = null;
          return;
        }
        this.dragMode = new SpreadsheetGridView.ResizeColumnDrag(valueOrDefault, position.X, this.layout.ColumnWidths[valueOrDefault]);
        return;
      }
      else
      {
        num = this.RowResizeTarget(position);
        if (num != null)
        {
          int valueOrDefault2 = num.GetValueOrDefault();
          this.dragMode = new SpreadsheetGridView.ResizeRowDrag(valueOrDefault2, position.Y, this.layout.RowHeights[valueOrDefault2]);
          return;
        }
        SpreadsheetGridView.Hit hit = this.HitAt(position);
        if (!(hit is SpreadsheetGridView.CornerHit))
        {
          SpreadsheetGridView.ColumnHeaderHit columnHeaderHit = hit as SpreadsheetGridView.ColumnHeaderHit;
          if (columnHeaderHit != null)
          {
            int num2;
            double num3;
            columnHeaderHit.Deconstruct(out num2, out num3);
            int num4 = (flag ? this.anchor.Column : num2);
            this.anchor = new SpreadsheetCellAddress(0, num4);
            this.extent = new SpreadsheetCellAddress(this.layout.RowCount - 1, num2);
            this.dragMode = new SpreadsheetGridView.ColumnsDrag(num4);
            this.UpdateSelection(false);
          }
          else
          {
            SpreadsheetGridView.RowHeaderHit rowHeaderHit = hit as SpreadsheetGridView.RowHeaderHit;
            if (rowHeaderHit != null)
            {
              int num5;
              double num6;
              rowHeaderHit.Deconstruct(out num5, out num6);
              int num7 = (flag ? this.anchor.Row : num5);
              this.anchor = new SpreadsheetCellAddress(num7, 0);
              this.extent = new SpreadsheetCellAddress(num5, this.layout.ColumnCount - 1);
              this.dragMode = new SpreadsheetGridView.RowsDrag(num7);
              this.UpdateSelection(false);
            }
            else
            {
              SpreadsheetGridView.CellHit cellHit = hit as SpreadsheetGridView.CellHit;
              if (cellHit != null)
              {
                SpreadsheetCellAddress spreadsheetCellAddress;
                cellHit.Deconstruct(out spreadsheetCellAddress);
                if (this.clickCount >= 2)
                {
                  SpreadsheetSheet sheet = this.Sheet;
                  string text;
                  if (sheet != null && sheet.Hyperlinks.TryGetValue(this.TopLeft(spreadsheetCellAddress), out text))
                  {
                    this.OpenLink(text);
                    return;
                  }
                }
                if (flag)
                {
                  this.extent = spreadsheetCellAddress;
                }
                else
                {
                  this.anchor = this.TopLeft(spreadsheetCellAddress);
                  this.extent = spreadsheetCellAddress;
                }
                this.dragMode = new SpreadsheetGridView.CellsDrag();
                this.UpdateSelection(false);
              }
            }
          }
        }
        else
        {
          this.SelectAll();
          this.dragMode = null;
        }
        e.Handled = true;
        return;
      }
    }

    // Token: 0x06001F40 RID: 8000 RVA: 0x0060D738 File Offset: 0x0060D738
    [NullableContext(2)]
    private bool IsInScrollBar(DependencyObject element)
    {
      DependencyObject dependencyObject = element;
      while (dependencyObject != null && dependencyObject != this.scrollView)
      {
        if (dependencyObject is ScrollBar)
        {
          return true;
        }
        dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
      }
      return false;
    }

    // Token: 0x06001F41 RID: 8001 RVA: 0x0060D768 File Offset: 0x0060D768
    private void OnPointerMoved(object sender, PointerRoutedEventArgs e)
    {
      if (this.layout == null)
      {
        return;
      }
      Point position = e.GetCurrentPoint(this).Position;
      if (this.dragMode == null)
      {
        base.ProtectedCursor = ((this.ColumnResizeTarget(position) != null) ? InputSystemCursor.Create(InputSystemCursorShape.SizeWestEast) : ((this.RowResizeTarget(position) != null) ? InputSystemCursor.Create(InputSystemCursorShape.SizeNorthSouth) : null));
        return;
      }
      SpreadsheetGridView.DragMode dragMode = this.dragMode;
      SpreadsheetGridView.ResizeColumnDrag resizeColumnDrag = dragMode as SpreadsheetGridView.ResizeColumnDrag;
      if (resizeColumnDrag != null)
      {
        int num;
        double num2;
        double num3;
        resizeColumnDrag.Deconstruct(out num, out num2, out num3);
        this.layout.SetColumnWidth(num3 + position.X - num2, num);
        SpreadsheetGridRenderer spreadsheetGridRenderer = this.renderer;
        if (spreadsheetGridRenderer != null)
        {
          spreadsheetGridRenderer.InvalidateTextMeasurements(new int?(num));
        }
        this.ApplyGeometry();
        return;
      }
      SpreadsheetGridView.ResizeRowDrag resizeRowDrag = dragMode as SpreadsheetGridView.ResizeRowDrag;
      if (resizeRowDrag != null)
      {
        int num4;
        double num5;
        double num6;
        resizeRowDrag.Deconstruct(out num4, out num5, out num6);
        this.layout.SetRowHeight(num6 + position.Y - num5, num4);
        SpreadsheetGridRenderer spreadsheetGridRenderer2 = this.renderer;
        if (spreadsheetGridRenderer2 != null)
        {
          spreadsheetGridRenderer2.InvalidateTextMeasurements(null);
        }
        this.ApplyGeometry();
        return;
      }
      if (!(dragMode is SpreadsheetGridView.CellsDrag))
      {
        SpreadsheetGridView.ColumnsDrag columnsDrag = dragMode as SpreadsheetGridView.ColumnsDrag;
        if (columnsDrag != null)
        {
          int num7;
          columnsDrag.Deconstruct(out num7);
          Point point = this.ContentPoint(position);
          this.anchor = new SpreadsheetCellAddress(0, num7);
          this.extent = new SpreadsheetCellAddress(this.layout.RowCount - 1, this.layout.Column(point.X));
        }
        else
        {
          SpreadsheetGridView.RowsDrag rowsDrag = dragMode as SpreadsheetGridView.RowsDrag;
          if (rowsDrag != null)
          {
            int num8;
            rowsDrag.Deconstruct(out num8);
            Point point2 = this.ContentPoint(position);
            this.anchor = new SpreadsheetCellAddress(num8, 0);
            this.extent = new SpreadsheetCellAddress(this.layout.Row(point2.Y), this.layout.ColumnCount - 1);
          }
        }
      }
      else
      {
        Point point3 = this.ContentPoint(position);
        this.extent = new SpreadsheetCellAddress(this.layout.Row(point3.Y), this.layout.Column(point3.X));
      }
      this.Autoscroll(position);
      this.UpdateSelection(false);
    }

    // Token: 0x06001F42 RID: 8002 RVA: 0x0060D994 File Offset: 0x0060D994
    private void Autoscroll(Point point)
    {
      double num = 0.0;
      double num2 = 0.0;
      if (point.X > base.ActualWidth)
      {
        num = point.X - base.ActualWidth;
      }
      else if (point.X < this.FixedLeft && this.originX > 0.0)
      {
        num = point.X - this.FixedLeft;
      }
      if (point.Y > base.ActualHeight)
      {
        num2 = point.Y - base.ActualHeight;
      }
      else if (point.Y < this.FixedTop && this.originY > 0.0)
      {
        num2 = point.Y - this.FixedTop;
      }
      if (num != 0.0 || num2 != 0.0)
      {
        this.scrollView.ChangeView(new double?(Math.Max(0.0, this.originX + num)), new double?(Math.Max(0.0, this.originY + num2)), null, true);
      }
    }

    // Token: 0x06001F43 RID: 8003 RVA: 0x0060DAB8 File Offset: 0x0060DAB8
    private void OnPointerReleased(object sender, PointerRoutedEventArgs e)
    {
      this.dragMode = null;
      this.root.ReleasePointerCapture(e.Pointer);
    }

    // Token: 0x06001F44 RID: 8004 RVA: 0x0060DAD4 File Offset: 0x0060DAD4
    private void OnWheelOutsideBody(object sender, PointerRoutedEventArgs e)
    {
      if (this.layout == null)
      {
        return;
      }
      PointerPoint currentPoint = e.GetCurrentPoint(this);
      if (currentPoint.Position.X >= this.FixedLeft && currentPoint.Position.Y >= this.FixedTop)
      {
        return;
      }
      double num = (double)currentPoint.Properties.MouseWheelDelta / 120.0 * 48.0;
      if (currentPoint.Properties.IsHorizontalMouseWheel)
      {
        this.scrollView.ChangeView(new double?(Math.Max(0.0, this.originX + num)), null, null);
      }
      else if ((e.KeyModifiers & VirtualKeyModifiers.Shift) != VirtualKeyModifiers.None)
      {
        this.scrollView.ChangeView(new double?(Math.Max(0.0, this.originX - num)), null, null);
      }
      else
      {
        this.scrollView.ChangeView(null, new double?(Math.Max(0.0, this.originY - num)), null);
      }
      e.Handled = true;
    }

    // Token: 0x06001F45 RID: 8005 RVA: 0x0060DC14 File Offset: 0x0060DC14
    private void OpenLink(string link)
    {
      if (link.StartsWith('#'))
      {
        return;
      }
      Action<string> openHyperlink = this.OpenHyperlink;
      if (openHyperlink == null)
      {
        return;
      }
      openHyperlink(link);
    }

    // Token: 0x06001F46 RID: 8006 RVA: 0x0060DC34 File Offset: 0x0060DC34
    private static bool IsDown(VirtualKey key)
    {
      return (InputKeyboardSource.GetKeyStateForCurrentThread(key) & CoreVirtualKeyStates.Down) > CoreVirtualKeyStates.None;
    }

    // Token: 0x06001F47 RID: 8007 RVA: 0x0060DC44 File Offset: 0x0060DC44
    private void OnKeyDown(object sender, KeyRoutedEventArgs e)
    {
      if (this.layout == null)
      {
        return;
      }
      bool flag = SpreadsheetGridView.IsDown(VirtualKey.Shift);
      bool flag2 = SpreadsheetGridView.IsDown(VirtualKey.Control);
      if (flag2 && e.Key == VirtualKey.C)
      {
        this.Copy();
        e.Handled = true;
        return;
      }
      if (flag2 && e.Key == VirtualKey.A)
      {
        this.SelectAll();
        e.Handled = true;
        return;
      }
      SpreadsheetCellAddress spreadsheetCellAddress = (flag ? this.extent : this.anchor);
      int num = Math.Max(1, (int)(this.ViewportHeight / Math.Max(this.layout.Sheet.DefaultRowHeight, 12.0)) - 1);
      VirtualKey key = e.Key;
      if (key == VirtualKey.Tab)
      {
        this.anchor = this.Moved(this.anchor, 0, flag ? (-1) : 1, false);
        this.extent = this.anchor;
        this.UpdateSelection(true);
        e.Handled = true;
        return;
      }
      if (key != VirtualKey.Enter)
      {
        switch (key)
        {
        case VirtualKey.PageUp:
          spreadsheetCellAddress = this.Moved(spreadsheetCellAddress, -num, 0, false);
          break;
        case VirtualKey.PageDown:
          spreadsheetCellAddress = this.Moved(spreadsheetCellAddress, num, 0, false);
          break;
        case VirtualKey.End:
          spreadsheetCellAddress = new SpreadsheetCellAddress(flag2 ? Math.Max(0, this.layout.Sheet.LastDataRow) : spreadsheetCellAddress.Row, Math.Max(0, this.layout.Sheet.LastDataColumn));
          break;
        case VirtualKey.Home:
          spreadsheetCellAddress = new SpreadsheetCellAddress(flag2 ? 0 : spreadsheetCellAddress.Row, 0);
          break;
        case VirtualKey.Left:
          spreadsheetCellAddress = this.Moved(spreadsheetCellAddress, 0, -1, flag2);
          break;
        case VirtualKey.Up:
          spreadsheetCellAddress = this.Moved(spreadsheetCellAddress, -1, 0, flag2);
          break;
        case VirtualKey.Right:
          spreadsheetCellAddress = this.Moved(spreadsheetCellAddress, 0, 1, flag2);
          break;
        case VirtualKey.Down:
          spreadsheetCellAddress = this.Moved(spreadsheetCellAddress, 1, 0, flag2);
          break;
        default:
          return;
        }
        if (flag)
        {
          this.extent = spreadsheetCellAddress;
        }
        else
        {
          this.anchor = this.TopLeft(spreadsheetCellAddress);
          this.extent = spreadsheetCellAddress;
        }
        this.UpdateSelection(true);
        e.Handled = true;
        return;
      }
      this.anchor = this.Moved(this.anchor, flag ? (-1) : 1, 0, false);
      this.extent = this.anchor;
      this.UpdateSelection(true);
      e.Handled = true;
    }

    // Token: 0x06001F48 RID: 8008 RVA: 0x0060DE70 File Offset: 0x0060DE70
    private SpreadsheetCellAddress Moved(SpreadsheetCellAddress address, int rowDelta, int columnDelta, bool jumpsToEdge)
    {
      if (this.layout == null)
      {
        return address;
      }
      if (jumpsToEdge)
      {
        int num = Math.Max(0, this.layout.Sheet.LastDataRow);
        int num2 = Math.Max(0, this.layout.Sheet.LastDataColumn);
        return new SpreadsheetCellAddress((rowDelta > 0) ? num : ((rowDelta < 0) ? 0 : address.Row), (columnDelta > 0) ? num2 : ((columnDelta < 0) ? 0 : address.Column));
      }
      int num3 = address.Row;
      int num4 = address.Column;
      SpreadsheetGridRenderer spreadsheetGridRenderer = this.renderer;
      SpreadsheetCellRange? spreadsheetCellRange = ((spreadsheetGridRenderer != null) ? spreadsheetGridRenderer.MergedRange(num3, num4) : null);
      if (spreadsheetCellRange != null)
      {
        SpreadsheetCellRange valueOrDefault = spreadsheetCellRange.GetValueOrDefault();
        if (rowDelta > 0)
        {
          num3 = valueOrDefault.LastRow;
        }
        if (columnDelta > 0)
        {
          num4 = valueOrDefault.LastColumn;
        }
        if (rowDelta < 0)
        {
          num3 = valueOrDefault.FirstRow;
        }
        if (columnDelta < 0)
        {
          num4 = valueOrDefault.FirstColumn;
        }
      }
      num3 = Math.Min(Math.Max(0, num3 + rowDelta), this.layout.RowCount - 1);
      num4 = Math.Min(Math.Max(0, num4 + columnDelta), this.layout.ColumnCount - 1);
      int num5 = ((rowDelta >= 0) ? 1 : (-1));
      int num6 = ((columnDelta >= 0) ? 1 : (-1));
      while (this.layout.RowHeights[num3] == 0.0 && num3 + num5 >= 0)
      {
        if (num3 + num5 >= this.layout.RowCount)
        {
          break;
        }
        num3 += num5;
      }
      while (this.layout.ColumnWidths[num4] == 0.0 && num4 + num6 >= 0 && num4 + num6 < this.layout.ColumnCount)
      {
        num4 += num6;
      }
      return new SpreadsheetCellAddress(num3, num4);
    }

    // Token: 0x06001F49 RID: 8009 RVA: 0x0060E024 File Offset: 0x0060E024
    private SpreadsheetCellAddress TopLeft(SpreadsheetCellAddress address)
    {
      SpreadsheetGridRenderer spreadsheetGridRenderer = this.renderer;
      SpreadsheetCellRange? spreadsheetCellRange = ((spreadsheetGridRenderer != null) ? spreadsheetGridRenderer.MergedRange(address.Row, address.Column) : null);
      if (spreadsheetCellRange != null)
      {
        SpreadsheetCellRange valueOrDefault = spreadsheetCellRange.GetValueOrDefault();
        return new SpreadsheetCellAddress(valueOrDefault.FirstRow, valueOrDefault.FirstColumn);
      }
      return address;
    }

    // Token: 0x06001F4A RID: 8010 RVA: 0x0060E084 File Offset: 0x0060E084
    public void SelectAll()
    {
      if (this.layout == null)
      {
        return;
      }
      this.anchor = new SpreadsheetCellAddress(0, 0);
      this.extent = new SpreadsheetCellAddress(this.layout.RowCount - 1, this.layout.ColumnCount - 1);
      this.UpdateSelection(false);
    }

    // Token: 0x06001F4B RID: 8011 RVA: 0x0060E0D4 File Offset: 0x0060E0D4
    [NullableContext(2)]
    public string SelectionText()
    {
      SpreadsheetSheet sheet = this.Sheet;
      if (sheet != null)
      {
        SpreadsheetGridRenderer spreadsheetGridRenderer = this.renderer;
        SpreadsheetSelection? spreadsheetSelection;
        SpreadsheetCellRange? spreadsheetCellRange = ((spreadsheetGridRenderer != null) ? ((spreadsheetGridRenderer.Selection != null) ? new SpreadsheetCellRange?(spreadsheetSelection.GetValueOrDefault().Range) : null) : null);
        if (spreadsheetCellRange != null)
        {
          SpreadsheetCellRange valueOrDefault = spreadsheetCellRange.GetValueOrDefault();
          int num = Math.Min(valueOrDefault.LastRow, sheet.LastDataRow);
          int num2 = Math.Min(valueOrDefault.LastColumn, sheet.LastDataColumn);
          if (num < valueOrDefault.FirstRow || num2 < valueOrDefault.FirstColumn)
          {
            return null;
          }
          if ((long)(num - valueOrDefault.FirstRow + 1) * (long)(num2 - valueOrDefault.FirstColumn + 1) > 500000L)
          {
            return null;
          }
          List<string> list = new List<string>();
          for (int i = valueOrDefault.FirstRow; i <= num; i++)
          {
            List<string> list2 = new List<string>();
            for (int j = valueOrDefault.FirstColumn; j <= num2; j++)
            {
              SpreadsheetCell spreadsheetCell = sheet.Cell(i, j);
              string text = ((spreadsheetCell != null) ? spreadsheetCell.DisplayText : null) ?? "";
              if (text.Contains('\t') || text.Contains('\n') || text.Contains('"'))
              {
                text = "\"" + text.Replace("\"", "\"\"") + "\"";
              }
              list2.Add(text);
            }
            list.Add(string.Join<string>('\t', list2));
          }
          return string.Join<string>('\n', list);
        }
      }
      return null;
    }

    // Token: 0x06001F4C RID: 8012 RVA: 0x0060E270 File Offset: 0x0060E270
    public void Copy()
    {
      string text = this.SelectionText();
      if (text == null)
      {
        return;
      }
      DataPackage dataPackage = new DataPackage();
      dataPackage.SetText(text);
      Clipboard.SetContent(dataPackage);
    }

    // Token: 0x06001F4D RID: 8013 RVA: 0x0060E29C File Offset: 0x0060E29C
    private void UpdateSelection(bool scrollToActiveCell)
    {
      if (this.renderer != null && this.layout != null)
      {
        SpreadsheetSheet sheet = this.Sheet;
        if (sheet != null)
        {
          SpreadsheetCellRange spreadsheetCellRange = this.renderer.ExpandedForMerges(SpreadsheetCellRange.Spanning(this.anchor, this.extent));
          this.renderer.Selection = new SpreadsheetSelection?(new SpreadsheetSelection(this.anchor, this.extent, spreadsheetCellRange));
          this.canvas.Invalidate();
          if (scrollToActiveCell)
          {
            this.ScrollToVisible(this.extent);
          }
          SpreadsheetSelectionSummary spreadsheetSelectionSummary = this.MakeSummary(sheet, spreadsheetCellRange);
          if (spreadsheetSelectionSummary != this.lastSummary)
          {
            this.lastSummary = spreadsheetSelectionSummary;
            Action<SpreadsheetSelectionSummary> selectionChanged = this.SelectionChanged;
            if (selectionChanged == null)
            {
              return;
            }
            selectionChanged(spreadsheetSelectionSummary);
          }
          return;
        }
      }
    }

    // Token: 0x06001F4E RID: 8014 RVA: 0x0060E34C File Offset: 0x0060E34C
    private void ScrollToVisible(SpreadsheetCellAddress address)
    {
      if (this.layout == null)
      {
        return;
      }
      SpreadsheetRect spreadsheetRect = this.layout.CellRect(address.Row, address.Column);
      double num = this.originX;
      double num2 = this.originY;
      double viewportWidth = this.ViewportWidth;
      double viewportHeight = this.ViewportHeight;
      if (address.Column >= this.layout.FrozenColumnCount)
      {
        double num3 = spreadsheetRect.MinX - this.layout.FrozenWidth;
        double num4 = num3 + spreadsheetRect.Width;
        if (num3 < num)
        {
          num = num3;
        }
        else if (num4 > num + viewportWidth)
        {
          num = Math.Min(num3, num4 - viewportWidth);
        }
      }
      if (address.Row >= this.layout.FrozenRowCount)
      {
        double num5 = spreadsheetRect.MinY - this.layout.FrozenHeight;
        double num6 = num5 + spreadsheetRect.Height;
        if (num5 < num2)
        {
          num2 = num5;
        }
        else if (num6 > num2 + viewportHeight)
        {
          num2 = Math.Min(num5, num6 - viewportHeight);
        }
      }
      if (num != this.originX || num2 != this.originY)
      {
        this.scrollView.ChangeView(new double?(Math.Max(0.0, num)), new double?(Math.Max(0.0, num2)), null, true);
      }
    }

    // Token: 0x06001F4F RID: 8015 RVA: 0x0060E490 File Offset: 0x0060E490
    private SpreadsheetSelectionSummary MakeSummary(SpreadsheetSheet sheet, SpreadsheetCellRange range)
    {
      SpreadsheetCell spreadsheetCell = sheet.Cell(this.anchor.Row, this.anchor.Column);
      string reference = this.anchor.Reference;
      string text = ((spreadsheetCell != null) ? spreadsheetCell.FormulaBarText : null) ?? "";
      string text3;
      string text2 = ((sheet.Hyperlinks.TryGetValue(this.anchor, out text3) && !text3.StartsWith('#')) ? text3 : null);
      SpreadsheetSelectionSummary spreadsheetSelectionSummary = new SpreadsheetSelectionSummary(reference, text, text2, null);
      if (!range.IsSingleCell)
      {
        SpreadsheetGridRenderer spreadsheetGridRenderer = this.renderer;
        if (spreadsheetGridRenderer == null || !(spreadsheetGridRenderer.MergedRange(this.anchor.Row, this.anchor.Column) == range))
        {
          int num = Math.Min(range.LastRow, sheet.LastDataRow);
          int num2 = Math.Min(range.LastColumn, sheet.LastDataColumn);
          if (num < range.FirstRow || num2 < range.FirstColumn)
          {
            return spreadsheetSelectionSummary;
          }
          int num3 = 0;
          int num4 = 0;
          int num5 = 0;
          double num6 = 0.0;
          for (int i = range.FirstRow; i <= num; i++)
          {
            Dictionary<int, SpreadsheetCell> dictionary;
            if (sheet.CellsByRow.TryGetValue(i, out dictionary))
            {
              foreach (KeyValuePair<int, SpreadsheetCell> keyValuePair in dictionary)
              {
                int num7;
                SpreadsheetCell spreadsheetCell2;
                keyValuePair.Deconstruct(out num7, out spreadsheetCell2);
                int num8 = num7;
                SpreadsheetCell spreadsheetCell3 = spreadsheetCell2;
                if (num8 >= range.FirstColumn && num8 <= num2)
                {
                  num5++;
                  if (num5 > 400000)
                  {
                    break;
                  }
                  if (spreadsheetCell3.DisplayText.Length != 0)
                  {
                    num3++;
                    if (spreadsheetCell3.Kind == SpreadsheetCellKind.Number)
                    {
                      double? numericValue = spreadsheetCell3.NumericValue;
                      if (numericValue != null)
                      {
                        double valueOrDefault = numericValue.GetValueOrDefault();
                        num4++;
                        num6 += valueOrDefault;
                      }
                    }
                  }
                }
              }
            }
          }
          if (num4 >= 1)
          {
            double num9 = num6 / (double)num4;
            SpreadsheetSelectionSummary spreadsheetSelectionSummary2 = spreadsheetSelectionSummary.<Clone>$();
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 3);
            defaultInterpolatedStringHandler.AppendLiteral("Sum: ");
            defaultInterpolatedStringHandler.AppendFormatted(SpreadsheetGridView.StatisticText(num6));
            defaultInterpolatedStringHandler.AppendLiteral("   Average: ");
            defaultInterpolatedStringHandler.AppendFormatted(SpreadsheetGridView.StatisticText(num9));
            defaultInterpolatedStringHandler.AppendLiteral("   Count: ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(num3);
            spreadsheetSelectionSummary2.Statistics = defaultInterpolatedStringHandler.ToStringAndClear();
            return spreadsheetSelectionSummary2;
          }
          if (num3 > 0)
          {
            SpreadsheetSelectionSummary spreadsheetSelectionSummary3 = spreadsheetSelectionSummary.<Clone>$();
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
            defaultInterpolatedStringHandler.AppendLiteral("Count: ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(num3);
            spreadsheetSelectionSummary3.Statistics = defaultInterpolatedStringHandler.ToStringAndClear();
            return spreadsheetSelectionSummary3;
          }
          return spreadsheetSelectionSummary;
        }
      }
      return spreadsheetSelectionSummary;
    }

    // Token: 0x06001F50 RID: 8016 RVA: 0x0060E754 File Offset: 0x0060E754
    private static string StatisticText(double value)
    {
      if (!double.IsFinite(value))
      {
        return value.ToString(CultureInfo.CurrentCulture);
      }
      int num = ((Math.Abs(value) >= 1000.0) ? 2 : 4);
      return Math.Round(value, num, MidpointRounding.ToEven).ToString("#,##0." + new string('#', num), CultureInfo.CurrentCulture);
    }

    // Token: 0x06001F59 RID: 8025 RVA: 0x0060E86C File Offset: 0x0060E86C
    [CompilerGenerated]
    private void <DrawPanes>g__Pane|43_0(double x, double y, double w, double h, SpreadsheetPaneGeometry geometry, SpreadsheetRect dirty, SpreadsheetRect bounds, bool dividerBelow, bool dividerRight, ref SpreadsheetGridView.<>c__DisplayClass43_0 A_10)
    {
      if (w <= 0.0 || h <= 0.0)
      {
        return;
      }
      using (A_10.ds.CreateLayer(1f, new Rect(x, y, w, h)))
      {
        A_10.ds.Transform = Matrix3x2.CreateTranslation((float)(x - bounds.X), (float)(y - bounds.Y)) * A_10.oldTransform;
        this.renderer.Draw(A_10.ds, geometry, dirty, bounds);
        A_10.ds.Transform = Matrix3x2.CreateTranslation((float)x, (float)y) * A_10.oldTransform;
        if (dividerBelow)
        {
          A_10.ds.FillRectangle(new Rect(0.0, h - 2.0, w, 2.0), SpreadsheetGridPalette.FrozenDivider);
        }
        if (dividerRight)
        {
          A_10.ds.FillRectangle(new Rect(w - 2.0, 0.0, 2.0, h), SpreadsheetGridPalette.FrozenDivider);
        }
        A_10.ds.Transform = A_10.oldTransform;
      }
    }

    // Token: 0x04000F05 RID: 3845
    [Nullable(2)]
    private SpreadsheetGridLayout layout;

    // Token: 0x04000F06 RID: 3846
    [Nullable(2)]
    private SpreadsheetGridRenderer renderer;

    // Token: 0x04000F07 RID: 3847
    private readonly Grid root = new Grid
    {
      Background = Ui.Brush(Colors.White)
    };

    // Token: 0x04000F08 RID: 3848
    private readonly CanvasControl canvas = new CanvasControl
    {
      IsHitTestVisible = false,
      ClearColor = Colors.White
    };

    // Token: 0x04000F09 RID: 3849
    private readonly ScrollViewer scrollView = new ScrollViewer
    {
      HorizontalScrollMode = ScrollMode.Enabled,
      VerticalScrollMode = ScrollMode.Enabled,
      HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
      VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
      ZoomMode = ZoomMode.Disabled,
      IsTabStop = false,
      HorizontalAlignment = HorizontalAlignment.Stretch,
      VerticalAlignment = VerticalAlignment.Stretch
    };

    // Token: 0x04000F0A RID: 3850
    private readonly Border bodySpacer = new Border
    {
      Background = Ui.Transparent,
      HorizontalAlignment = HorizontalAlignment.Left,
      VerticalAlignment = VerticalAlignment.Top
    };

    // Token: 0x04000F0B RID: 3851
    private double originX;

    // Token: 0x04000F0C RID: 3852
    private double originY;

    // Token: 0x04000F0D RID: 3853
    private SpreadsheetCellAddress anchor = new SpreadsheetCellAddress(0, 0);

    // Token: 0x04000F0E RID: 3854
    private SpreadsheetCellAddress extent = new SpreadsheetCellAddress(0, 0);

    // Token: 0x04000F0F RID: 3855
    [Nullable(2)]
    private SpreadsheetSelectionSummary lastSummary;

    // Token: 0x04000F10 RID: 3856
    [Nullable(2)]
    private SpreadsheetGridView.DragMode dragMode;

    // Token: 0x04000F11 RID: 3857
    private DateTime lastPressAt;

    // Token: 0x04000F12 RID: 3858
    private Point lastPressPoint;

    // Token: 0x04000F13 RID: 3859
    private int clickCount;

    // Token: 0x04000F14 RID: 3860
    private static readonly uint DoubleClickMilliseconds = SpreadsheetGridView.GetDoubleClickTime();

    // Token: 0x02000910 RID: 2320
    [Nullable(0)]
    private abstract class DragMode : IEquatable<SpreadsheetGridView.DragMode>
    {
      // Token: 0x17000830 RID: 2096
      // (get) Token: 0x06003B8D RID: 15245 RVA: 0x00695F70 File Offset: 0x00695F70
      [CompilerGenerated]
      protected virtual Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(SpreadsheetGridView.DragMode);
        }
      }

      // Token: 0x06003B8E RID: 15246 RVA: 0x00695F7C File Offset: 0x00695F7C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("DragMode");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003B8F RID: 15247 RVA: 0x00695FC8 File Offset: 0x00695FC8
      [CompilerGenerated]
      protected virtual bool PrintMembers(StringBuilder builder)
      {
        return false;
      }

      // Token: 0x06003B90 RID: 15248 RVA: 0x00695FCC File Offset: 0x00695FCC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(SpreadsheetGridView.DragMode left, SpreadsheetGridView.DragMode right)
      {
        return !(left == right);
      }

      // Token: 0x06003B91 RID: 15249 RVA: 0x00695FD8 File Offset: 0x00695FD8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(SpreadsheetGridView.DragMode left, SpreadsheetGridView.DragMode right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003B92 RID: 15250 RVA: 0x00695FEC File Offset: 0x00695FEC
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
      }

      // Token: 0x06003B93 RID: 15251 RVA: 0x00696000 File Offset: 0x00696000
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as SpreadsheetGridView.DragMode);
      }

      // Token: 0x06003B94 RID: 15252 RVA: 0x00696010 File Offset: 0x00696010
      [NullableContext(2)]
      [CompilerGenerated]
      public virtual bool Equals(SpreadsheetGridView.DragMode other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract);
      }

      // Token: 0x06003B96 RID: 15254 RVA: 0x00696030 File Offset: 0x00696030
      [CompilerGenerated]
      protected DragMode(SpreadsheetGridView.DragMode original)
      {
      }

      // Token: 0x06003B97 RID: 15255 RVA: 0x00696038 File Offset: 0x00696038
      protected DragMode()
      {
      }
    }

    // Token: 0x02000911 RID: 2321
    [Nullable(0)]
    private sealed class CellsDrag : SpreadsheetGridView.DragMode, IEquatable<SpreadsheetGridView.CellsDrag>
    {
      // Token: 0x17000831 RID: 2097
      // (get) Token: 0x06003B98 RID: 15256 RVA: 0x00696040 File Offset: 0x00696040
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(SpreadsheetGridView.CellsDrag);
        }
      }

      // Token: 0x06003B99 RID: 15257 RVA: 0x0069604C File Offset: 0x0069604C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("CellsDrag");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003B9A RID: 15258 RVA: 0x00696098 File Offset: 0x00696098
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x06003B9B RID: 15259 RVA: 0x006960A4 File Offset: 0x006960A4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(SpreadsheetGridView.CellsDrag left, SpreadsheetGridView.CellsDrag right)
      {
        return !(left == right);
      }

      // Token: 0x06003B9C RID: 15260 RVA: 0x006960B0 File Offset: 0x006960B0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(SpreadsheetGridView.CellsDrag left, SpreadsheetGridView.CellsDrag right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003B9D RID: 15261 RVA: 0x006960C4 File Offset: 0x006960C4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x06003B9E RID: 15262 RVA: 0x006960CC File Offset: 0x006960CC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as SpreadsheetGridView.CellsDrag);
      }

      // Token: 0x06003B9F RID: 15263 RVA: 0x006960DC File Offset: 0x006960DC
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(SpreadsheetGridView.DragMode other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003BA0 RID: 15264 RVA: 0x006960E8 File Offset: 0x006960E8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(SpreadsheetGridView.CellsDrag other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x06003BA2 RID: 15266 RVA: 0x00696100 File Offset: 0x00696100
      [CompilerGenerated]
      private CellsDrag(SpreadsheetGridView.CellsDrag original)
        : base(original)
      {
      }

      // Token: 0x06003BA3 RID: 15267 RVA: 0x0069610C File Offset: 0x0069610C
      public CellsDrag()
      {
      }
    }

    // Token: 0x02000912 RID: 2322
    [Nullable(0)]
    private sealed class ColumnsDrag : SpreadsheetGridView.DragMode, IEquatable<SpreadsheetGridView.ColumnsDrag>
    {
      // Token: 0x06003BA4 RID: 15268 RVA: 0x00696114 File Offset: 0x00696114
      public ColumnsDrag(int AnchorColumn)
      {
        this.AnchorColumn = AnchorColumn;
        base..ctor();
      }

      // Token: 0x17000832 RID: 2098
      // (get) Token: 0x06003BA5 RID: 15269 RVA: 0x00696124 File Offset: 0x00696124
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(SpreadsheetGridView.ColumnsDrag);
        }
      }

      // Token: 0x17000833 RID: 2099
      // (get) Token: 0x06003BA6 RID: 15270 RVA: 0x00696130 File Offset: 0x00696130
      // (set) Token: 0x06003BA7 RID: 15271 RVA: 0x00696138 File Offset: 0x00696138
      public int AnchorColumn { get; set; }

      // Token: 0x06003BA8 RID: 15272 RVA: 0x00696144 File Offset: 0x00696144
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ColumnsDrag");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003BA9 RID: 15273 RVA: 0x00696190 File Offset: 0x00696190
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("AnchorColumn = ");
        builder.Append(this.AnchorColumn.ToString());
        return true;
      }

      // Token: 0x06003BAA RID: 15274 RVA: 0x006961DC File Offset: 0x006961DC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(SpreadsheetGridView.ColumnsDrag left, SpreadsheetGridView.ColumnsDrag right)
      {
        return !(left == right);
      }

      // Token: 0x06003BAB RID: 15275 RVA: 0x006961E8 File Offset: 0x006961E8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(SpreadsheetGridView.ColumnsDrag left, SpreadsheetGridView.ColumnsDrag right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003BAC RID: 15276 RVA: 0x006961FC File Offset: 0x006961FC
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<AnchorColumn>k__BackingField);
      }

      // Token: 0x06003BAD RID: 15277 RVA: 0x0069621C File Offset: 0x0069621C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as SpreadsheetGridView.ColumnsDrag);
      }

      // Token: 0x06003BAE RID: 15278 RVA: 0x0069622C File Offset: 0x0069622C
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(SpreadsheetGridView.DragMode other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003BAF RID: 15279 RVA: 0x00696238 File Offset: 0x00696238
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(SpreadsheetGridView.ColumnsDrag other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<AnchorColumn>k__BackingField, other.<AnchorColumn>k__BackingField));
      }

      // Token: 0x06003BB1 RID: 15281 RVA: 0x0069626C File Offset: 0x0069626C
      [CompilerGenerated]
      private ColumnsDrag(SpreadsheetGridView.ColumnsDrag original)
        : base(original)
      {
        this.AnchorColumn = original.<AnchorColumn>k__BackingField;
      }

      // Token: 0x06003BB2 RID: 15282 RVA: 0x00696284 File Offset: 0x00696284
      [CompilerGenerated]
      public void Deconstruct(out int AnchorColumn)
      {
        AnchorColumn = this.AnchorColumn;
      }
    }

    // Token: 0x02000913 RID: 2323
    [Nullable(0)]
    private sealed class RowsDrag : SpreadsheetGridView.DragMode, IEquatable<SpreadsheetGridView.RowsDrag>
    {
      // Token: 0x06003BB3 RID: 15283 RVA: 0x00696290 File Offset: 0x00696290
      public RowsDrag(int AnchorRow)
      {
        this.AnchorRow = AnchorRow;
        base..ctor();
      }

      // Token: 0x17000834 RID: 2100
      // (get) Token: 0x06003BB4 RID: 15284 RVA: 0x006962A0 File Offset: 0x006962A0
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(SpreadsheetGridView.RowsDrag);
        }
      }

      // Token: 0x17000835 RID: 2101
      // (get) Token: 0x06003BB5 RID: 15285 RVA: 0x006962AC File Offset: 0x006962AC
      // (set) Token: 0x06003BB6 RID: 15286 RVA: 0x006962B4 File Offset: 0x006962B4
      public int AnchorRow { get; set; }

      // Token: 0x06003BB7 RID: 15287 RVA: 0x006962C0 File Offset: 0x006962C0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("RowsDrag");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003BB8 RID: 15288 RVA: 0x0069630C File Offset: 0x0069630C
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("AnchorRow = ");
        builder.Append(this.AnchorRow.ToString());
        return true;
      }

      // Token: 0x06003BB9 RID: 15289 RVA: 0x00696358 File Offset: 0x00696358
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(SpreadsheetGridView.RowsDrag left, SpreadsheetGridView.RowsDrag right)
      {
        return !(left == right);
      }

      // Token: 0x06003BBA RID: 15290 RVA: 0x00696364 File Offset: 0x00696364
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(SpreadsheetGridView.RowsDrag left, SpreadsheetGridView.RowsDrag right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003BBB RID: 15291 RVA: 0x00696378 File Offset: 0x00696378
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<AnchorRow>k__BackingField);
      }

      // Token: 0x06003BBC RID: 15292 RVA: 0x00696398 File Offset: 0x00696398
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as SpreadsheetGridView.RowsDrag);
      }

      // Token: 0x06003BBD RID: 15293 RVA: 0x006963A8 File Offset: 0x006963A8
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(SpreadsheetGridView.DragMode other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003BBE RID: 15294 RVA: 0x006963B4 File Offset: 0x006963B4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(SpreadsheetGridView.RowsDrag other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<AnchorRow>k__BackingField, other.<AnchorRow>k__BackingField));
      }

      // Token: 0x06003BC0 RID: 15296 RVA: 0x006963E8 File Offset: 0x006963E8
      [CompilerGenerated]
      private RowsDrag(SpreadsheetGridView.RowsDrag original)
        : base(original)
      {
        this.AnchorRow = original.<AnchorRow>k__BackingField;
      }

      // Token: 0x06003BC1 RID: 15297 RVA: 0x00696400 File Offset: 0x00696400
      [CompilerGenerated]
      public void Deconstruct(out int AnchorRow)
      {
        AnchorRow = this.AnchorRow;
      }
    }

    // Token: 0x02000914 RID: 2324
    [Nullable(0)]
    private sealed class ResizeColumnDrag : SpreadsheetGridView.DragMode, IEquatable<SpreadsheetGridView.ResizeColumnDrag>
    {
      // Token: 0x06003BC2 RID: 15298 RVA: 0x0069640C File Offset: 0x0069640C
      public ResizeColumnDrag(int Column, double StartX, double StartWidth)
      {
        this.Column = Column;
        this.StartX = StartX;
        this.StartWidth = StartWidth;
        base..ctor();
      }

      // Token: 0x17000836 RID: 2102
      // (get) Token: 0x06003BC3 RID: 15299 RVA: 0x0069642C File Offset: 0x0069642C
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(SpreadsheetGridView.ResizeColumnDrag);
        }
      }

      // Token: 0x17000837 RID: 2103
      // (get) Token: 0x06003BC4 RID: 15300 RVA: 0x00696438 File Offset: 0x00696438
      // (set) Token: 0x06003BC5 RID: 15301 RVA: 0x00696440 File Offset: 0x00696440
      public int Column { get; set; }

      // Token: 0x17000838 RID: 2104
      // (get) Token: 0x06003BC6 RID: 15302 RVA: 0x0069644C File Offset: 0x0069644C
      // (set) Token: 0x06003BC7 RID: 15303 RVA: 0x00696454 File Offset: 0x00696454
      public double StartX { get; set; }

      // Token: 0x17000839 RID: 2105
      // (get) Token: 0x06003BC8 RID: 15304 RVA: 0x00696460 File Offset: 0x00696460
      // (set) Token: 0x06003BC9 RID: 15305 RVA: 0x00696468 File Offset: 0x00696468
      public double StartWidth { get; set; }

      // Token: 0x06003BCA RID: 15306 RVA: 0x00696474 File Offset: 0x00696474
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ResizeColumnDrag");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003BCB RID: 15307 RVA: 0x006964C0 File Offset: 0x006964C0
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Column = ");
        builder.Append(this.Column.ToString());
        builder.Append(", StartX = ");
        builder.Append(this.StartX.ToString());
        builder.Append(", StartWidth = ");
        builder.Append(this.StartWidth.ToString());
        return true;
      }

      // Token: 0x06003BCC RID: 15308 RVA: 0x00696558 File Offset: 0x00696558
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(SpreadsheetGridView.ResizeColumnDrag left, SpreadsheetGridView.ResizeColumnDrag right)
      {
        return !(left == right);
      }

      // Token: 0x06003BCD RID: 15309 RVA: 0x00696564 File Offset: 0x00696564
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(SpreadsheetGridView.ResizeColumnDrag left, SpreadsheetGridView.ResizeColumnDrag right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003BCE RID: 15310 RVA: 0x00696578 File Offset: 0x00696578
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Column>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<StartX>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<StartWidth>k__BackingField);
      }

      // Token: 0x06003BCF RID: 15311 RVA: 0x006965D0 File Offset: 0x006965D0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as SpreadsheetGridView.ResizeColumnDrag);
      }

      // Token: 0x06003BD0 RID: 15312 RVA: 0x006965E0 File Offset: 0x006965E0
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(SpreadsheetGridView.DragMode other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003BD1 RID: 15313 RVA: 0x006965EC File Offset: 0x006965EC
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(SpreadsheetGridView.ResizeColumnDrag other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Column>k__BackingField, other.<Column>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<StartX>k__BackingField, other.<StartX>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<StartWidth>k__BackingField, other.<StartWidth>k__BackingField));
      }

      // Token: 0x06003BD3 RID: 15315 RVA: 0x00696658 File Offset: 0x00696658
      [CompilerGenerated]
      private ResizeColumnDrag(SpreadsheetGridView.ResizeColumnDrag original)
        : base(original)
      {
        this.Column = original.<Column>k__BackingField;
        this.StartX = original.<StartX>k__BackingField;
        this.StartWidth = original.<StartWidth>k__BackingField;
      }

      // Token: 0x06003BD4 RID: 15316 RVA: 0x00696688 File Offset: 0x00696688
      [CompilerGenerated]
      public void Deconstruct(out int Column, out double StartX, out double StartWidth)
      {
        Column = this.Column;
        StartX = this.StartX;
        StartWidth = this.StartWidth;
      }
    }

    // Token: 0x02000915 RID: 2325
    [Nullable(0)]
    private sealed class ResizeRowDrag : SpreadsheetGridView.DragMode, IEquatable<SpreadsheetGridView.ResizeRowDrag>
    {
      // Token: 0x06003BD5 RID: 15317 RVA: 0x006966A4 File Offset: 0x006966A4
      public ResizeRowDrag(int Row, double StartY, double StartHeight)
      {
        this.Row = Row;
        this.StartY = StartY;
        this.StartHeight = StartHeight;
        base..ctor();
      }

      // Token: 0x1700083A RID: 2106
      // (get) Token: 0x06003BD6 RID: 15318 RVA: 0x006966C4 File Offset: 0x006966C4
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(SpreadsheetGridView.ResizeRowDrag);
        }
      }

      // Token: 0x1700083B RID: 2107
      // (get) Token: 0x06003BD7 RID: 15319 RVA: 0x006966D0 File Offset: 0x006966D0
      // (set) Token: 0x06003BD8 RID: 15320 RVA: 0x006966D8 File Offset: 0x006966D8
      public int Row { get; set; }

      // Token: 0x1700083C RID: 2108
      // (get) Token: 0x06003BD9 RID: 15321 RVA: 0x006966E4 File Offset: 0x006966E4
      // (set) Token: 0x06003BDA RID: 15322 RVA: 0x006966EC File Offset: 0x006966EC
      public double StartY { get; set; }

      // Token: 0x1700083D RID: 2109
      // (get) Token: 0x06003BDB RID: 15323 RVA: 0x006966F8 File Offset: 0x006966F8
      // (set) Token: 0x06003BDC RID: 15324 RVA: 0x00696700 File Offset: 0x00696700
      public double StartHeight { get; set; }

      // Token: 0x06003BDD RID: 15325 RVA: 0x0069670C File Offset: 0x0069670C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ResizeRowDrag");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003BDE RID: 15326 RVA: 0x00696758 File Offset: 0x00696758
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Row = ");
        builder.Append(this.Row.ToString());
        builder.Append(", StartY = ");
        builder.Append(this.StartY.ToString());
        builder.Append(", StartHeight = ");
        builder.Append(this.StartHeight.ToString());
        return true;
      }

      // Token: 0x06003BDF RID: 15327 RVA: 0x006967F0 File Offset: 0x006967F0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(SpreadsheetGridView.ResizeRowDrag left, SpreadsheetGridView.ResizeRowDrag right)
      {
        return !(left == right);
      }

      // Token: 0x06003BE0 RID: 15328 RVA: 0x006967FC File Offset: 0x006967FC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(SpreadsheetGridView.ResizeRowDrag left, SpreadsheetGridView.ResizeRowDrag right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003BE1 RID: 15329 RVA: 0x00696810 File Offset: 0x00696810
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Row>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<StartY>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<StartHeight>k__BackingField);
      }

      // Token: 0x06003BE2 RID: 15330 RVA: 0x00696868 File Offset: 0x00696868
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as SpreadsheetGridView.ResizeRowDrag);
      }

      // Token: 0x06003BE3 RID: 15331 RVA: 0x00696878 File Offset: 0x00696878
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(SpreadsheetGridView.DragMode other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003BE4 RID: 15332 RVA: 0x00696884 File Offset: 0x00696884
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(SpreadsheetGridView.ResizeRowDrag other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Row>k__BackingField, other.<Row>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<StartY>k__BackingField, other.<StartY>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<StartHeight>k__BackingField, other.<StartHeight>k__BackingField));
      }

      // Token: 0x06003BE6 RID: 15334 RVA: 0x006968F0 File Offset: 0x006968F0
      [CompilerGenerated]
      private ResizeRowDrag(SpreadsheetGridView.ResizeRowDrag original)
        : base(original)
      {
        this.Row = original.<Row>k__BackingField;
        this.StartY = original.<StartY>k__BackingField;
        this.StartHeight = original.<StartHeight>k__BackingField;
      }

      // Token: 0x06003BE7 RID: 15335 RVA: 0x00696920 File Offset: 0x00696920
      [CompilerGenerated]
      public void Deconstruct(out int Row, out double StartY, out double StartHeight)
      {
        Row = this.Row;
        StartY = this.StartY;
        StartHeight = this.StartHeight;
      }
    }

    // Token: 0x02000916 RID: 2326
    [Nullable(0)]
    private abstract class Hit : IEquatable<SpreadsheetGridView.Hit>
    {
      // Token: 0x1700083E RID: 2110
      // (get) Token: 0x06003BE8 RID: 15336 RVA: 0x0069693C File Offset: 0x0069693C
      [CompilerGenerated]
      protected virtual Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(SpreadsheetGridView.Hit);
        }
      }

      // Token: 0x06003BE9 RID: 15337 RVA: 0x00696948 File Offset: 0x00696948
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Hit");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003BEA RID: 15338 RVA: 0x00696994 File Offset: 0x00696994
      [CompilerGenerated]
      protected virtual bool PrintMembers(StringBuilder builder)
      {
        return false;
      }

      // Token: 0x06003BEB RID: 15339 RVA: 0x00696998 File Offset: 0x00696998
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(SpreadsheetGridView.Hit left, SpreadsheetGridView.Hit right)
      {
        return !(left == right);
      }

      // Token: 0x06003BEC RID: 15340 RVA: 0x006969A4 File Offset: 0x006969A4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(SpreadsheetGridView.Hit left, SpreadsheetGridView.Hit right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003BED RID: 15341 RVA: 0x006969B8 File Offset: 0x006969B8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
      }

      // Token: 0x06003BEE RID: 15342 RVA: 0x006969CC File Offset: 0x006969CC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as SpreadsheetGridView.Hit);
      }

      // Token: 0x06003BEF RID: 15343 RVA: 0x006969DC File Offset: 0x006969DC
      [NullableContext(2)]
      [CompilerGenerated]
      public virtual bool Equals(SpreadsheetGridView.Hit other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract);
      }

      // Token: 0x06003BF1 RID: 15345 RVA: 0x006969FC File Offset: 0x006969FC
      [CompilerGenerated]
      protected Hit(SpreadsheetGridView.Hit original)
      {
      }

      // Token: 0x06003BF2 RID: 15346 RVA: 0x00696A04 File Offset: 0x00696A04
      protected Hit()
      {
      }
    }

    // Token: 0x02000917 RID: 2327
    [Nullable(0)]
    private sealed class CornerHit : SpreadsheetGridView.Hit, IEquatable<SpreadsheetGridView.CornerHit>
    {
      // Token: 0x1700083F RID: 2111
      // (get) Token: 0x06003BF3 RID: 15347 RVA: 0x00696A0C File Offset: 0x00696A0C
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(SpreadsheetGridView.CornerHit);
        }
      }

      // Token: 0x06003BF4 RID: 15348 RVA: 0x00696A18 File Offset: 0x00696A18
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("CornerHit");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003BF5 RID: 15349 RVA: 0x00696A64 File Offset: 0x00696A64
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x06003BF6 RID: 15350 RVA: 0x00696A70 File Offset: 0x00696A70
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(SpreadsheetGridView.CornerHit left, SpreadsheetGridView.CornerHit right)
      {
        return !(left == right);
      }

      // Token: 0x06003BF7 RID: 15351 RVA: 0x00696A7C File Offset: 0x00696A7C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(SpreadsheetGridView.CornerHit left, SpreadsheetGridView.CornerHit right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003BF8 RID: 15352 RVA: 0x00696A90 File Offset: 0x00696A90
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x06003BF9 RID: 15353 RVA: 0x00696A98 File Offset: 0x00696A98
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as SpreadsheetGridView.CornerHit);
      }

      // Token: 0x06003BFA RID: 15354 RVA: 0x00696AA8 File Offset: 0x00696AA8
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(SpreadsheetGridView.Hit other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003BFB RID: 15355 RVA: 0x00696AB4 File Offset: 0x00696AB4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(SpreadsheetGridView.CornerHit other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x06003BFD RID: 15357 RVA: 0x00696ACC File Offset: 0x00696ACC
      [CompilerGenerated]
      private CornerHit(SpreadsheetGridView.CornerHit original)
        : base(original)
      {
      }

      // Token: 0x06003BFE RID: 15358 RVA: 0x00696AD8 File Offset: 0x00696AD8
      public CornerHit()
      {
      }
    }

    // Token: 0x02000918 RID: 2328
    [Nullable(0)]
    private sealed class ColumnHeaderHit : SpreadsheetGridView.Hit, IEquatable<SpreadsheetGridView.ColumnHeaderHit>
    {
      // Token: 0x06003BFF RID: 15359 RVA: 0x00696AE0 File Offset: 0x00696AE0
      public ColumnHeaderHit(int Column, double ContentX)
      {
        this.Column = Column;
        this.ContentX = ContentX;
        base..ctor();
      }

      // Token: 0x17000840 RID: 2112
      // (get) Token: 0x06003C00 RID: 15360 RVA: 0x00696AF8 File Offset: 0x00696AF8
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(SpreadsheetGridView.ColumnHeaderHit);
        }
      }

      // Token: 0x17000841 RID: 2113
      // (get) Token: 0x06003C01 RID: 15361 RVA: 0x00696B04 File Offset: 0x00696B04
      // (set) Token: 0x06003C02 RID: 15362 RVA: 0x00696B0C File Offset: 0x00696B0C
      public int Column { get; set; }

      // Token: 0x17000842 RID: 2114
      // (get) Token: 0x06003C03 RID: 15363 RVA: 0x00696B18 File Offset: 0x00696B18
      // (set) Token: 0x06003C04 RID: 15364 RVA: 0x00696B20 File Offset: 0x00696B20
      public double ContentX { get; set; }

      // Token: 0x06003C05 RID: 15365 RVA: 0x00696B2C File Offset: 0x00696B2C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ColumnHeaderHit");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003C06 RID: 15366 RVA: 0x00696B78 File Offset: 0x00696B78
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Column = ");
        builder.Append(this.Column.ToString());
        builder.Append(", ContentX = ");
        builder.Append(this.ContentX.ToString());
        return true;
      }

      // Token: 0x06003C07 RID: 15367 RVA: 0x00696BEC File Offset: 0x00696BEC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(SpreadsheetGridView.ColumnHeaderHit left, SpreadsheetGridView.ColumnHeaderHit right)
      {
        return !(left == right);
      }

      // Token: 0x06003C08 RID: 15368 RVA: 0x00696BF8 File Offset: 0x00696BF8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(SpreadsheetGridView.ColumnHeaderHit left, SpreadsheetGridView.ColumnHeaderHit right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003C09 RID: 15369 RVA: 0x00696C0C File Offset: 0x00696C0C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Column>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<ContentX>k__BackingField);
      }

      // Token: 0x06003C0A RID: 15370 RVA: 0x00696C44 File Offset: 0x00696C44
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as SpreadsheetGridView.ColumnHeaderHit);
      }

      // Token: 0x06003C0B RID: 15371 RVA: 0x00696C54 File Offset: 0x00696C54
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(SpreadsheetGridView.Hit other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003C0C RID: 15372 RVA: 0x00696C60 File Offset: 0x00696C60
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(SpreadsheetGridView.ColumnHeaderHit other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Column>k__BackingField, other.<Column>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<ContentX>k__BackingField, other.<ContentX>k__BackingField));
      }

      // Token: 0x06003C0E RID: 15374 RVA: 0x00696CB4 File Offset: 0x00696CB4
      [CompilerGenerated]
      private ColumnHeaderHit(SpreadsheetGridView.ColumnHeaderHit original)
        : base(original)
      {
        this.Column = original.<Column>k__BackingField;
        this.ContentX = original.<ContentX>k__BackingField;
      }

      // Token: 0x06003C0F RID: 15375 RVA: 0x00696CD8 File Offset: 0x00696CD8
      [CompilerGenerated]
      public void Deconstruct(out int Column, out double ContentX)
      {
        Column = this.Column;
        ContentX = this.ContentX;
      }
    }

    // Token: 0x02000919 RID: 2329
    [Nullable(0)]
    private sealed class RowHeaderHit : SpreadsheetGridView.Hit, IEquatable<SpreadsheetGridView.RowHeaderHit>
    {
      // Token: 0x06003C10 RID: 15376 RVA: 0x00696CEC File Offset: 0x00696CEC
      public RowHeaderHit(int Row, double ContentY)
      {
        this.Row = Row;
        this.ContentY = ContentY;
        base..ctor();
      }

      // Token: 0x17000843 RID: 2115
      // (get) Token: 0x06003C11 RID: 15377 RVA: 0x00696D04 File Offset: 0x00696D04
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(SpreadsheetGridView.RowHeaderHit);
        }
      }

      // Token: 0x17000844 RID: 2116
      // (get) Token: 0x06003C12 RID: 15378 RVA: 0x00696D10 File Offset: 0x00696D10
      // (set) Token: 0x06003C13 RID: 15379 RVA: 0x00696D18 File Offset: 0x00696D18
      public int Row { get; set; }

      // Token: 0x17000845 RID: 2117
      // (get) Token: 0x06003C14 RID: 15380 RVA: 0x00696D24 File Offset: 0x00696D24
      // (set) Token: 0x06003C15 RID: 15381 RVA: 0x00696D2C File Offset: 0x00696D2C
      public double ContentY { get; set; }

      // Token: 0x06003C16 RID: 15382 RVA: 0x00696D38 File Offset: 0x00696D38
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("RowHeaderHit");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003C17 RID: 15383 RVA: 0x00696D84 File Offset: 0x00696D84
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Row = ");
        builder.Append(this.Row.ToString());
        builder.Append(", ContentY = ");
        builder.Append(this.ContentY.ToString());
        return true;
      }

      // Token: 0x06003C18 RID: 15384 RVA: 0x00696DF8 File Offset: 0x00696DF8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(SpreadsheetGridView.RowHeaderHit left, SpreadsheetGridView.RowHeaderHit right)
      {
        return !(left == right);
      }

      // Token: 0x06003C19 RID: 15385 RVA: 0x00696E04 File Offset: 0x00696E04
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(SpreadsheetGridView.RowHeaderHit left, SpreadsheetGridView.RowHeaderHit right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003C1A RID: 15386 RVA: 0x00696E18 File Offset: 0x00696E18
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Row>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<ContentY>k__BackingField);
      }

      // Token: 0x06003C1B RID: 15387 RVA: 0x00696E50 File Offset: 0x00696E50
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as SpreadsheetGridView.RowHeaderHit);
      }

      // Token: 0x06003C1C RID: 15388 RVA: 0x00696E60 File Offset: 0x00696E60
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(SpreadsheetGridView.Hit other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003C1D RID: 15389 RVA: 0x00696E6C File Offset: 0x00696E6C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(SpreadsheetGridView.RowHeaderHit other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<Row>k__BackingField, other.<Row>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<ContentY>k__BackingField, other.<ContentY>k__BackingField));
      }

      // Token: 0x06003C1F RID: 15391 RVA: 0x00696EC0 File Offset: 0x00696EC0
      [CompilerGenerated]
      private RowHeaderHit(SpreadsheetGridView.RowHeaderHit original)
        : base(original)
      {
        this.Row = original.<Row>k__BackingField;
        this.ContentY = original.<ContentY>k__BackingField;
      }

      // Token: 0x06003C20 RID: 15392 RVA: 0x00696EE4 File Offset: 0x00696EE4
      [CompilerGenerated]
      public void Deconstruct(out int Row, out double ContentY)
      {
        Row = this.Row;
        ContentY = this.ContentY;
      }
    }

    // Token: 0x0200091A RID: 2330
    [Nullable(0)]
    private sealed class CellHit : SpreadsheetGridView.Hit, IEquatable<SpreadsheetGridView.CellHit>
    {
      // Token: 0x06003C21 RID: 15393 RVA: 0x00696EF8 File Offset: 0x00696EF8
      public CellHit(SpreadsheetCellAddress Address)
      {
        this.Address = Address;
        base..ctor();
      }

      // Token: 0x17000846 RID: 2118
      // (get) Token: 0x06003C22 RID: 15394 RVA: 0x00696F08 File Offset: 0x00696F08
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(SpreadsheetGridView.CellHit);
        }
      }

      // Token: 0x17000847 RID: 2119
      // (get) Token: 0x06003C23 RID: 15395 RVA: 0x00696F14 File Offset: 0x00696F14
      // (set) Token: 0x06003C24 RID: 15396 RVA: 0x00696F1C File Offset: 0x00696F1C
      public SpreadsheetCellAddress Address { get; set; }

      // Token: 0x06003C25 RID: 15397 RVA: 0x00696F28 File Offset: 0x00696F28
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("CellHit");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003C26 RID: 15398 RVA: 0x00696F74 File Offset: 0x00696F74
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Address = ");
        builder.Append(this.Address.ToString());
        return true;
      }

      // Token: 0x06003C27 RID: 15399 RVA: 0x00696FC0 File Offset: 0x00696FC0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(SpreadsheetGridView.CellHit left, SpreadsheetGridView.CellHit right)
      {
        return !(left == right);
      }

      // Token: 0x06003C28 RID: 15400 RVA: 0x00696FCC File Offset: 0x00696FCC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(SpreadsheetGridView.CellHit left, SpreadsheetGridView.CellHit right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003C29 RID: 15401 RVA: 0x00696FE0 File Offset: 0x00696FE0
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<SpreadsheetCellAddress>.Default.GetHashCode(this.<Address>k__BackingField);
      }

      // Token: 0x06003C2A RID: 15402 RVA: 0x00697000 File Offset: 0x00697000
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as SpreadsheetGridView.CellHit);
      }

      // Token: 0x06003C2B RID: 15403 RVA: 0x00697010 File Offset: 0x00697010
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(SpreadsheetGridView.Hit other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003C2C RID: 15404 RVA: 0x0069701C File Offset: 0x0069701C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(SpreadsheetGridView.CellHit other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<SpreadsheetCellAddress>.Default.Equals(this.<Address>k__BackingField, other.<Address>k__BackingField));
      }

      // Token: 0x06003C2E RID: 15406 RVA: 0x00697050 File Offset: 0x00697050
      [CompilerGenerated]
      private CellHit(SpreadsheetGridView.CellHit original)
        : base(original)
      {
        this.Address = original.<Address>k__BackingField;
      }

      // Token: 0x06003C2F RID: 15407 RVA: 0x00697068 File Offset: 0x00697068
      [CompilerGenerated]
      public void Deconstruct(out SpreadsheetCellAddress Address)
      {
        Address = this.Address;
      }
    }
  }
}
