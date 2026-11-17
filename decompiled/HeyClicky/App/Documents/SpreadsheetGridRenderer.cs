using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Documents.Spreadsheet;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.UI;
using Microsoft.UI.Text;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002DF RID: 735
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SpreadsheetGridRenderer
  {
    // Token: 0x17000598 RID: 1432
    // (get) Token: 0x06001F01 RID: 7937 RVA: 0x0060A6A8 File Offset: 0x0060A6A8
    public SpreadsheetGridLayout Layout { get; }

    // Token: 0x17000599 RID: 1433
    // (get) Token: 0x06001F02 RID: 7938 RVA: 0x0060A6B0 File Offset: 0x0060A6B0
    public SpreadsheetSheet Sheet
    {
      get
      {
        return this.Layout.Sheet;
      }
    }

    // Token: 0x1700059A RID: 1434
    // (get) Token: 0x06001F03 RID: 7939 RVA: 0x0060A6C0 File Offset: 0x0060A6C0
    // (set) Token: 0x06001F04 RID: 7940 RVA: 0x0060A6C8 File Offset: 0x0060A6C8
    public SpreadsheetSelection? Selection { get; set; }

    // Token: 0x06001F05 RID: 7941 RVA: 0x0060A6D4 File Offset: 0x0060A6D4
    public SpreadsheetGridRenderer(SpreadsheetGridLayout layout)
    {
      this.Layout = layout;
      for (int i = 0; i < layout.Sheet.MergedRanges.Count; i++)
      {
        SpreadsheetCellRange spreadsheetCellRange = layout.Sheet.MergedRanges[i];
        for (int j = spreadsheetCellRange.FirstRow; j <= Math.Min(spreadsheetCellRange.LastRow, spreadsheetCellRange.FirstRow + 5000); j++)
        {
          List<int> list;
          if (!this.mergeIndicesByRow.TryGetValue(j, out list))
          {
            list = (this.mergeIndicesByRow[j] = new List<int>());
          }
          list.Add(i);
        }
      }
    }

    // Token: 0x06001F06 RID: 7942 RVA: 0x0060A79C File Offset: 0x0060A79C
    public SpreadsheetCellRange? MergedRange(int row, int column)
    {
      List<int> list;
      if (!this.mergeIndicesByRow.TryGetValue(row, out list))
      {
        return null;
      }
      foreach (int num in list)
      {
        if (this.Sheet.MergedRanges[num].Contains(row, column))
        {
          return new SpreadsheetCellRange?(this.Sheet.MergedRanges[num]);
        }
      }
      return null;
    }

    // Token: 0x06001F07 RID: 7943 RVA: 0x0060A840 File Offset: 0x0060A840
    public SpreadsheetCellRange ExpandedForMerges(SpreadsheetCellRange range)
    {
      if (this.Sheet.MergedRanges.Count == 0)
      {
        return range;
      }
      SpreadsheetCellRange spreadsheetCellRange = range;
      bool flag = true;
      while (flag)
      {
        flag = false;
        foreach (SpreadsheetCellRange spreadsheetCellRange2 in this.Sheet.MergedRanges)
        {
          if (spreadsheetCellRange2.Intersects(spreadsheetCellRange))
          {
            SpreadsheetCellRange spreadsheetCellRange3 = spreadsheetCellRange.Union(spreadsheetCellRange2);
            if (spreadsheetCellRange3 != spreadsheetCellRange)
            {
              spreadsheetCellRange = spreadsheetCellRange3;
              flag = true;
            }
          }
        }
      }
      return spreadsheetCellRange;
    }

    // Token: 0x06001F08 RID: 7944 RVA: 0x0060A8D0 File Offset: 0x0060A8D0
    public void InvalidateTextMeasurements(int? column = null)
    {
      if (column != null)
      {
        int c = column.GetValueOrDefault();
        IEnumerable<SpreadsheetCellAddress> keys = this.singleLineTextCache.Keys;
        Func<SpreadsheetCellAddress, bool> <>9__0;
        Func<SpreadsheetCellAddress, bool> func;
        if ((func = <>9__0) == null)
        {
          func = (<>9__0 = (SpreadsheetCellAddress k) => k.Column == c);
        }
        using (List<SpreadsheetCellAddress>.Enumerator enumerator = keys.Where<SpreadsheetCellAddress>(func).ToList<SpreadsheetCellAddress>().GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            SpreadsheetCellAddress spreadsheetCellAddress = enumerator.Current;
            this.singleLineTextCache[spreadsheetCellAddress].Item3.Dispose();
            this.singleLineTextCache.Remove(spreadsheetCellAddress);
          }
          goto IL_00E8;
        }
      }
      foreach (ValueTuple<string, double, CanvasTextLayout> valueTuple in this.singleLineTextCache.Values)
      {
        valueTuple.Item3.Dispose();
      }
      this.singleLineTextCache.Clear();
      IL_00E8:
      foreach (ValueTuple<double, CanvasTextLayout> valueTuple2 in this.wrappedCache.Values)
      {
        valueTuple2.Item2.Dispose();
      }
      this.wrappedCache.Clear();
    }

    // Token: 0x06001F09 RID: 7945 RVA: 0x0060AA38 File Offset: 0x0060AA38
    private static Rect R(SpreadsheetRect r)
    {
      return new Rect(r.X, r.Y, Math.Max(0.0, r.Width), Math.Max(0.0, r.Height));
    }

    // Token: 0x06001F0A RID: 7946 RVA: 0x0060AA78 File Offset: 0x0060AA78
    private static Rect R(double x, double y, double w, double h)
    {
      return new Rect(x, y, Math.Max(0.0, w), Math.Max(0.0, h));
    }

    // Token: 0x06001F0B RID: 7947 RVA: 0x0060AAA0 File Offset: 0x0060AAA0
    public void Draw(CanvasDrawingSession ds, SpreadsheetPaneGeometry geometry, SpreadsheetRect dirtyRect, SpreadsheetRect paneBounds)
    {
      SpreadsheetRect spreadsheetRect = new SpreadsheetRect(geometry.RowHeaderWidth, geometry.ColumnHeaderHeight, Math.Max(0.0, paneBounds.Width - geometry.RowHeaderWidth), Math.Max(0.0, paneBounds.Height - geometry.ColumnHeaderHeight));
      SpreadsheetRect spreadsheetRect2 = dirtyRect.Intersection(spreadsheetRect);
      if (!spreadsheetRect2.IsEmpty && !geometry.RowsEmpty && !geometry.ColumnsEmpty)
      {
        using (ds.CreateLayer(1f, SpreadsheetGridRenderer.R(spreadsheetRect2)))
        {
          this.DrawCells(ds, geometry, spreadsheetRect2);
          goto IL_00B4;
        }
      }
      if (!spreadsheetRect2.IsEmpty)
      {
        ds.FillRectangle(SpreadsheetGridRenderer.R(spreadsheetRect2), SpreadsheetGridPalette.Paper);
      }
      IL_00B4:
      if (geometry.ColumnHeaderHeight > 0.0)
      {
        SpreadsheetRect spreadsheetRect3 = new SpreadsheetRect(geometry.RowHeaderWidth, 0.0, spreadsheetRect.Width, geometry.ColumnHeaderHeight);
        SpreadsheetRect spreadsheetRect4 = dirtyRect.Intersection(spreadsheetRect3);
        if (!spreadsheetRect4.IsEmpty)
        {
          using (ds.CreateLayer(1f, SpreadsheetGridRenderer.R(spreadsheetRect4)))
          {
            this.DrawColumnHeaders(ds, geometry, spreadsheetRect3, spreadsheetRect4);
          }
        }
      }
      if (geometry.RowHeaderWidth > 0.0)
      {
        SpreadsheetRect spreadsheetRect5 = new SpreadsheetRect(0.0, geometry.ColumnHeaderHeight, geometry.RowHeaderWidth, spreadsheetRect.Height);
        SpreadsheetRect spreadsheetRect6 = dirtyRect.Intersection(spreadsheetRect5);
        if (!spreadsheetRect6.IsEmpty)
        {
          using (ds.CreateLayer(1f, SpreadsheetGridRenderer.R(spreadsheetRect6)))
          {
            this.DrawRowHeaders(ds, geometry, spreadsheetRect5, spreadsheetRect6);
          }
        }
      }
      if (geometry.ColumnHeaderHeight > 0.0 && geometry.RowHeaderWidth > 0.0)
      {
        SpreadsheetRect spreadsheetRect7 = new SpreadsheetRect(0.0, 0.0, geometry.RowHeaderWidth, geometry.ColumnHeaderHeight);
        if (spreadsheetRect7.Intersects(dirtyRect))
        {
          ds.FillRectangle(SpreadsheetGridRenderer.R(spreadsheetRect7), SpreadsheetGridPalette.HeaderFill);
          ds.FillRectangle(SpreadsheetGridRenderer.R(spreadsheetRect7.MaxX - 1.0, 0.0, 1.0, spreadsheetRect7.Height), SpreadsheetGridPalette.HeaderLine);
          ds.FillRectangle(SpreadsheetGridRenderer.R(0.0, spreadsheetRect7.MaxY - 1.0, spreadsheetRect7.Width, 1.0), SpreadsheetGridPalette.HeaderLine);
        }
      }
    }

    // Token: 0x06001F0C RID: 7948 RVA: 0x0060AD58 File Offset: 0x0060AD58
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "Start", "End" })]
    private static ValueTuple<int, int> VisibleRange(int domainStart, int domainEnd, double minimum, double maximum, [Nullable(1)] Func<double, int> offsets)
    {
      if (domainEnd <= domainStart)
      {
        return new ValueTuple<int, int>(domainStart, domainEnd);
      }
      int num = Math.Max(domainStart, offsets(minimum));
      int num2 = Math.Min(domainEnd - 1, offsets(maximum));
      if (num > num2)
      {
        return new ValueTuple<int, int>(num, num);
      }
      return new ValueTuple<int, int>(num, num2 + 1);
    }

    // Token: 0x06001F0D RID: 7949 RVA: 0x0060ADA8 File Offset: 0x0060ADA8
    private void DrawCells(CanvasDrawingSession ds, SpreadsheetPaneGeometry geometry, SpreadsheetRect dirtyRect)
    {
      SpreadsheetGridRenderer.<>c__DisplayClass21_0 CS$<>8__locals1 = new SpreadsheetGridRenderer.<>c__DisplayClass21_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.geometry = geometry;
      CS$<>8__locals1.ox = CS$<>8__locals1.geometry.OffsetX;
      CS$<>8__locals1.oy = CS$<>8__locals1.geometry.OffsetY;
      ValueTuple<int, int> valueTuple = SpreadsheetGridRenderer.VisibleRange(CS$<>8__locals1.geometry.RowStart, CS$<>8__locals1.geometry.RowEnd, dirtyRect.MinY - CS$<>8__locals1.oy, dirtyRect.MaxY - CS$<>8__locals1.oy, new Func<double, int>(this.Layout.Row));
      int item = valueTuple.Item1;
      int item2 = valueTuple.Item2;
      ValueTuple<int, int> valueTuple2 = SpreadsheetGridRenderer.VisibleRange(CS$<>8__locals1.geometry.ColumnStart, CS$<>8__locals1.geometry.ColumnEnd, dirtyRect.MinX - CS$<>8__locals1.ox, dirtyRect.MaxX - CS$<>8__locals1.ox, new Func<double, int>(this.Layout.Column));
      int item3 = valueTuple2.Item1;
      int item4 = valueTuple2.Item2;
      bool flag = item2 <= item;
      bool flag2 = item4 <= item3;
      ds.FillRectangle(SpreadsheetGridRenderer.R(dirtyRect), SpreadsheetGridPalette.Paper);
      if (this.Sheet.ShowsGridLines && !flag && !flag2)
      {
        using (CanvasPathBuilder canvasPathBuilder = new CanvasPathBuilder(ds))
        {
          for (int i = item3; i < item4; i++)
          {
            if (this.Layout.ColumnWidths[i] > 0.0)
            {
              float num = (float)(this.Layout.ColumnOffsets[i + 1] + CS$<>8__locals1.ox - 0.5);
              canvasPathBuilder.BeginFigure(num, (float)dirtyRect.MinY);
              canvasPathBuilder.AddLine(num, (float)dirtyRect.MaxY);
              canvasPathBuilder.EndFigure(CanvasFigureLoop.Open);
            }
          }
          for (int j = item; j < item2; j++)
          {
            if (this.Layout.RowHeights[j] > 0.0)
            {
              float num2 = (float)(this.Layout.RowOffsets[j + 1] + CS$<>8__locals1.oy - 0.5);
              canvasPathBuilder.BeginFigure((float)dirtyRect.MinX, num2);
              canvasPathBuilder.AddLine((float)dirtyRect.MaxX, num2);
              canvasPathBuilder.EndFigure(CanvasFigureLoop.Open);
            }
          }
          using (CanvasGeometry canvasGeometry = CanvasGeometry.CreatePath(canvasPathBuilder))
          {
            ds.DrawGeometry(canvasGeometry, SpreadsheetGridPalette.GridLine, 1f);
          }
        }
      }
      for (int k = item; k < item2; k++)
      {
        Dictionary<int, SpreadsheetCell> dictionary;
        if (this.Sheet.CellsByRow.TryGetValue(k, out dictionary) && this.Layout.RowHeights[k] > 0.0)
        {
          for (int l = item3; l < item4; l++)
          {
            SpreadsheetCell spreadsheetCell;
            if (dictionary.TryGetValue(l, out spreadsheetCell))
            {
              SpreadsheetColor? spreadsheetColor = this.Sheet.Style(spreadsheetCell.StyleIndex).FillColor;
              if (spreadsheetColor != null)
              {
                SpreadsheetColor valueOrDefault = spreadsheetColor.GetValueOrDefault();
                if (this.MergedRange(k, l) == null)
                {
                  ds.FillRectangle(SpreadsheetGridRenderer.R(CS$<>8__locals1.<DrawCells>g__PaneRect|0(k, l)), SpreadsheetTextAttributes.ColorOf(valueOrDefault));
                }
              }
            }
          }
        }
      }
      CS$<>8__locals1.visibleBlock = new SpreadsheetCellRange(item, item3, Math.Max(item, item2 - 1), Math.Max(item3, item4 - 1));
      List<SpreadsheetCellRange> list = ((flag || flag2) ? new List<SpreadsheetCellRange>() : this.Sheet.MergedRanges.Where<SpreadsheetCellRange>((SpreadsheetCellRange m) => m.Intersects(CS$<>8__locals1.visibleBlock) && m.FirstRow >= CS$<>8__locals1.geometry.RowStart && m.FirstRow < CS$<>8__locals1.geometry.RowEnd && m.FirstColumn >= CS$<>8__locals1.geometry.ColumnStart && m.FirstColumn < CS$<>8__locals1.geometry.ColumnEnd).ToList<SpreadsheetCellRange>());
      foreach (SpreadsheetCellRange spreadsheetCellRange in list)
      {
        SpreadsheetCell spreadsheetCell2 = this.Sheet.Cell(spreadsheetCellRange.FirstRow, spreadsheetCellRange.FirstColumn);
        if (spreadsheetCell2 == null)
        {
          goto IL_03D9;
        }
        SpreadsheetColor? spreadsheetColor = this.Sheet.Style(spreadsheetCell2.StyleIndex).FillColor;
        if (spreadsheetColor == null)
        {
          goto IL_03D9;
        }
        SpreadsheetColor valueOrDefault2 = spreadsheetColor.GetValueOrDefault();
        Color color = SpreadsheetTextAttributes.ColorOf(valueOrDefault2);
        IL_03E7:
        Color color2 = color;
        SpreadsheetRect spreadsheetRect = CS$<>8__locals1.<DrawCells>g__PaneRange|1(spreadsheetCellRange);
        if (this.Sheet.ShowsGridLines)
        {
          SpreadsheetRect spreadsheetRect2 = spreadsheetRect;
          spreadsheetRect2.Width = spreadsheetRect.Width - 1.0;
          spreadsheetRect2.Height = spreadsheetRect.Height - 1.0;
          spreadsheetRect = spreadsheetRect2;
        }
        ds.FillRectangle(SpreadsheetGridRenderer.R(spreadsheetRect), color2);
        continue;
        IL_03D9:
        color = SpreadsheetGridPalette.Paper;
        goto IL_03E7;
      }
      int num3 = Math.Max(CS$<>8__locals1.geometry.ColumnStart, item3 - 12);
      for (int m2 = item; m2 < item2; m2++)
      {
        Dictionary<int, SpreadsheetCell> dictionary2;
        if (this.Sheet.CellsByRow.TryGetValue(m2, out dictionary2) && this.Layout.RowHeights[m2] > 0.0)
        {
          for (int n = num3; n < item4; n++)
          {
            SpreadsheetCell spreadsheetCell3;
            if (dictionary2.TryGetValue(n, out spreadsheetCell3) && spreadsheetCell3.DisplayText.Length != 0 && this.MergedRange(m2, n) == null)
            {
              this.DrawText(ds, spreadsheetCell3, new SpreadsheetCellAddress(m2, n), CS$<>8__locals1.<DrawCells>g__PaneRect|0(m2, n), dictionary2, CS$<>8__locals1.geometry.ColumnEnd, CS$<>8__locals1.ox, CS$<>8__locals1.oy, true);
            }
          }
        }
      }
      foreach (SpreadsheetCellRange spreadsheetCellRange2 in list)
      {
        SpreadsheetCell spreadsheetCell4 = this.Sheet.Cell(spreadsheetCellRange2.FirstRow, spreadsheetCellRange2.FirstColumn);
        if (spreadsheetCell4 != null && spreadsheetCell4.DisplayText.Length != 0)
        {
          this.DrawText(ds, spreadsheetCell4, new SpreadsheetCellAddress(spreadsheetCellRange2.FirstRow, spreadsheetCellRange2.FirstColumn), CS$<>8__locals1.<DrawCells>g__PaneRange|1(spreadsheetCellRange2), null, CS$<>8__locals1.geometry.ColumnEnd, CS$<>8__locals1.ox, CS$<>8__locals1.oy, false);
        }
      }
      for (int num4 = item; num4 < item2; num4++)
      {
        Dictionary<int, SpreadsheetCell> dictionary3;
        if (this.Sheet.CellsByRow.TryGetValue(num4, out dictionary3))
        {
          for (int num5 = item3; num5 < item4; num5++)
          {
            SpreadsheetCell spreadsheetCell5;
            if (dictionary3.TryGetValue(num5, out spreadsheetCell5))
            {
              SpreadsheetCellStyle spreadsheetCellStyle = this.Sheet.Style(spreadsheetCell5.StyleIndex);
              if (spreadsheetCellStyle.HasBorders)
              {
                SpreadsheetCellRange? spreadsheetCellRange3 = this.MergedRange(num4, num5);
                SpreadsheetRect spreadsheetRect3;
                if (spreadsheetCellRange3 != null)
                {
                  SpreadsheetCellRange valueOrDefault3 = spreadsheetCellRange3.GetValueOrDefault();
                  spreadsheetRect3 = CS$<>8__locals1.<DrawCells>g__PaneRange|1(valueOrDefault3);
                }
                else
                {
                  spreadsheetRect3 = CS$<>8__locals1.<DrawCells>g__PaneRect|0(num4, num5);
                }
                SpreadsheetRect spreadsheetRect4 = spreadsheetRect3;
                SpreadsheetGridRenderer.DrawBorders(ds, spreadsheetCellStyle, spreadsheetRect4);
              }
            }
          }
        }
      }
      SpreadsheetSelection? selection = this.Selection;
      if (selection != null)
      {
        SpreadsheetSelection valueOrDefault4 = selection.GetValueOrDefault();
        this.DrawSelection(ds, valueOrDefault4, CS$<>8__locals1.geometry);
      }
    }

    // Token: 0x06001F0E RID: 7950 RVA: 0x0060B4C0 File Offset: 0x0060B4C0
    private void DrawText(CanvasDrawingSession ds, SpreadsheetCell cell, SpreadsheetCellAddress address, SpreadsheetRect cellRect, [Nullable(new byte[] { 2, 1 })] Dictionary<int, SpreadsheetCell> rowCells, int columnLimit, double ox, double oy, bool allowsSpill)
    {
      if (cellRect.Width <= 0.0 || cellRect.Height <= 0.0)
      {
        return;
      }
      SpreadsheetCellStyle spreadsheetCellStyle = this.Sheet.Style(cell.StyleIndex);
      bool flag = this.Sheet.Hyperlinks.ContainsKey(address);
      double num = 4.0;
      double num2 = (double)spreadsheetCellStyle.IndentLevel * 9.0;
      SpreadsheetColor? spreadsheetColor = spreadsheetCellStyle.TextColor;
      Color color;
      if (spreadsheetColor != null)
      {
        SpreadsheetColor valueOrDefault = spreadsheetColor.GetValueOrDefault();
        color = SpreadsheetTextAttributes.ColorOf(valueOrDefault);
      }
      else
      {
        color = (flag ? SpreadsheetTextAttributes.HyperlinkColor : SpreadsheetTextAttributes.DefaultTextColor);
      }
      Color color2 = color;
      SpreadsheetHorizontalAlignment spreadsheetHorizontalAlignment = spreadsheetCellStyle.HorizontalAlignment;
      if (spreadsheetHorizontalAlignment == SpreadsheetHorizontalAlignment.General)
      {
        SpreadsheetCellKind kind = cell.Kind;
        SpreadsheetHorizontalAlignment spreadsheetHorizontalAlignment2;
        if (kind != SpreadsheetCellKind.Number)
        {
          if (kind - SpreadsheetCellKind.Boolean > 1)
          {
            spreadsheetHorizontalAlignment2 = SpreadsheetHorizontalAlignment.Left;
          }
          else
          {
            spreadsheetHorizontalAlignment2 = SpreadsheetHorizontalAlignment.Center;
          }
        }
        else
        {
          spreadsheetHorizontalAlignment2 = SpreadsheetHorizontalAlignment.Right;
        }
        spreadsheetHorizontalAlignment = spreadsheetHorizontalAlignment2;
      }
      SpreadsheetVerticalAlignment spreadsheetVerticalAlignment;
      double num4;
      if (spreadsheetCellStyle.WrapsText)
      {
        CanvasHorizontalAlignment canvasHorizontalAlignment;
        if (spreadsheetHorizontalAlignment != SpreadsheetHorizontalAlignment.Center)
        {
          if (spreadsheetHorizontalAlignment != SpreadsheetHorizontalAlignment.Right)
          {
            canvasHorizontalAlignment = CanvasHorizontalAlignment.Left;
          }
          else
          {
            canvasHorizontalAlignment = CanvasHorizontalAlignment.Right;
          }
        }
        else
        {
          canvasHorizontalAlignment = CanvasHorizontalAlignment.Center;
        }
        CanvasHorizontalAlignment canvasHorizontalAlignment2 = canvasHorizontalAlignment;
        double num3 = Math.Max(1.0, cellRect.Width - num * 2.0 - num2);
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 4);
        defaultInterpolatedStringHandler.AppendFormatted<int>(address.Row);
        defaultInterpolatedStringHandler.AppendLiteral(":");
        defaultInterpolatedStringHandler.AppendFormatted<int>(address.Column);
        defaultInterpolatedStringHandler.AppendLiteral(":");
        defaultInterpolatedStringHandler.AppendFormatted<int>((int)num3);
        defaultInterpolatedStringHandler.AppendLiteral(":");
        defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
        string text = defaultInterpolatedStringHandler.ToStringAndClear();
        ValueTuple<double, CanvasTextLayout> valueTuple;
        if (!this.wrappedCache.TryGetValue(text, out valueTuple))
        {
          CanvasTextLayout canvasTextLayout = this.attributes.WrappedLayout(ds, cell.DisplayText, spreadsheetCellStyle, num3, canvasHorizontalAlignment2, flag);
          valueTuple = new ValueTuple<double, CanvasTextLayout>(Math.Ceiling((double)canvasTextLayout.LineMetrics.Length * SpreadsheetTextAttributes.WrappedLinePitch(spreadsheetCellStyle.FontSize)), canvasTextLayout);
          this.wrappedCache[text] = valueTuple;
        }
        double item = valueTuple.Item1;
        spreadsheetVerticalAlignment = spreadsheetCellStyle.VerticalAlignment;
        if (spreadsheetVerticalAlignment != SpreadsheetVerticalAlignment.Top)
        {
          if (spreadsheetVerticalAlignment != SpreadsheetVerticalAlignment.Center)
          {
            num4 = Math.Max(cellRect.MinY + 2.0, cellRect.MaxY - 3.0 - item);
          }
          else
          {
            num4 = cellRect.MinY + Math.Max(2.0, (cellRect.Height - item) / 2.0);
          }
        }
        else
        {
          num4 = cellRect.MinY + 2.0;
        }
        double num5 = num4;
        using (ds.CreateLayer(1f, SpreadsheetGridRenderer.R(cellRect.Inset(1.0, 1.0))))
        {
          ds.DrawTextLayout(valueTuple.Item2, (float)(cellRect.MinX + num + num2), (float)num5, color2);
        }
        return;
      }
      ValueTuple<string, double, CanvasTextLayout> valueTuple2;
      if (!this.singleLineTextCache.TryGetValue(address, out valueTuple2))
      {
        string text2 = (cell.DisplayText.Contains('\n') ? cell.DisplayText.Replace("\n", " ") : cell.DisplayText);
        CanvasTextLayout canvasTextLayout2 = this.attributes.SingleLineLayout(ds, text2, this.attributes.CellFormat(spreadsheetCellStyle, false, CanvasHorizontalAlignment.Left), spreadsheetCellStyle.IsUnderlined || flag, spreadsheetCellStyle.IsStruckThrough);
        valueTuple2 = new ValueTuple<string, double, CanvasTextLayout>(text2, Math.Ceiling(canvasTextLayout2.LayoutBoundsIncludingTrailingWhitespace.Width), canvasTextLayout2);
        this.singleLineTextCache[address] = valueTuple2;
      }
      SpreadsheetRect spreadsheetRect = cellRect;
      if (allowsSpill && spreadsheetHorizontalAlignment == SpreadsheetHorizontalAlignment.Left && valueTuple2.Item2 + num * 2.0 + num2 > cellRect.Width)
      {
        double num6 = valueTuple2.Item2 + num * 2.0 + num2 - cellRect.Width;
        int num7 = address.Column + 1;
        double num8 = cellRect.MaxX;
        SpreadsheetCell spreadsheetCell;
        while (num6 > 0.0 && num7 < columnLimit && (rowCells == null || !rowCells.TryGetValue(num7, out spreadsheetCell) || spreadsheetCell.DisplayText.Length <= 0) && this.MergedRange(address.Row, num7) == null)
        {
          SpreadsheetRect spreadsheetRect2 = this.Layout.CellRect(address.Row, num7).Offset(ox, oy);
          SpreadsheetCell spreadsheetCell2;
          if (rowCells == null || !rowCells.TryGetValue(num7, out spreadsheetCell2))
          {
            goto IL_046C;
          }
          spreadsheetColor = this.Sheet.Style(spreadsheetCell2.StyleIndex).FillColor;
          if (spreadsheetColor == null)
          {
            goto IL_046C;
          }
          SpreadsheetColor valueOrDefault2 = spreadsheetColor.GetValueOrDefault();
          Color color3 = SpreadsheetTextAttributes.ColorOf(valueOrDefault2);
          IL_047A:
          Color color4 = color3;
          ds.FillRectangle(SpreadsheetGridRenderer.R(spreadsheetRect2.MinX - 1.0, spreadsheetRect2.MinY, spreadsheetRect2.Width, spreadsheetRect2.Height - (this.Sheet.ShowsGridLines > false)), color4);
          num8 = spreadsheetRect2.MaxX;
          num6 -= spreadsheetRect2.Width;
          num7++;
          continue;
          IL_046C:
          color3 = SpreadsheetGridPalette.Paper;
          goto IL_047A;
        }
        SpreadsheetRect spreadsheetRect3 = spreadsheetRect;
        spreadsheetRect3.Width = num8 - cellRect.MinX;
        spreadsheetRect = spreadsheetRect3;
      }
      double num9 = SpreadsheetTextAttributes.LineHeight(spreadsheetCellStyle.FontSize);
      spreadsheetVerticalAlignment = spreadsheetCellStyle.VerticalAlignment;
      if (spreadsheetVerticalAlignment != SpreadsheetVerticalAlignment.Top)
      {
        if (spreadsheetVerticalAlignment != SpreadsheetVerticalAlignment.Center)
        {
          num4 = cellRect.MaxY - 4.0 - num9;
        }
        else
        {
          num4 = cellRect.MinY + (cellRect.Height - num9) / 2.0;
        }
      }
      else
      {
        num4 = cellRect.MinY + 3.0;
      }
      double num10 = num4;
      if (spreadsheetHorizontalAlignment != SpreadsheetHorizontalAlignment.Center)
      {
        if (spreadsheetHorizontalAlignment == SpreadsheetHorizontalAlignment.Right)
        {
          num4 = cellRect.MaxX - num - valueTuple2.Item2 - num2;
        }
        else
        {
          num4 = cellRect.MinX + num + num2;
        }
      }
      else
      {
        num4 = cellRect.MidX - valueTuple2.Item2 / 2.0;
      }
      double num11 = num4;
      double num12 = Math.Round(num10, MidpointRounding.AwayFromZero) + SpreadsheetTextAttributes.Ascender(spreadsheetCellStyle.FontSize) - this.attributes.Baseline(this.attributes.CellFormat(spreadsheetCellStyle, false, CanvasHorizontalAlignment.Left));
      using (ds.CreateLayer(1f, SpreadsheetGridRenderer.R(spreadsheetRect.Inset(1.0, 0.0))))
      {
        ds.DrawTextLayout(valueTuple2.Item3, (float)num11, (float)num12, color2);
      }
    }

    // Token: 0x06001F0F RID: 7951 RVA: 0x0060BB30 File Offset: 0x0060BB30
    private static void DrawBorders(CanvasDrawingSession ds, SpreadsheetCellStyle style, SpreadsheetRect rect)
    {
      SpreadsheetGridRenderer.<DrawBorders>g__Stroke|23_0(ds, style.TopBorder, rect.MinX - 0.5, rect.MinY - 0.5, rect.MaxX - 0.5, rect.MinY - 0.5);
      SpreadsheetGridRenderer.<DrawBorders>g__Stroke|23_0(ds, style.BottomBorder, rect.MinX - 0.5, rect.MaxY - 0.5, rect.MaxX - 0.5, rect.MaxY - 0.5);
      SpreadsheetGridRenderer.<DrawBorders>g__Stroke|23_0(ds, style.LeftBorder, rect.MinX - 0.5, rect.MinY - 0.5, rect.MinX - 0.5, rect.MaxY - 0.5);
      SpreadsheetGridRenderer.<DrawBorders>g__Stroke|23_0(ds, style.RightBorder, rect.MaxX - 0.5, rect.MinY - 0.5, rect.MaxX - 0.5, rect.MaxY - 0.5);
    }

    // Token: 0x06001F10 RID: 7952 RVA: 0x0060BC80 File Offset: 0x0060BC80
    private void DrawSelection(CanvasDrawingSession ds, SpreadsheetSelection selection, SpreadsheetPaneGeometry geometry)
    {
      double offsetX = geometry.OffsetX;
      double offsetY = geometry.OffsetY;
      SpreadsheetRect spreadsheetRect = this.Layout.Rect(selection.Range).Offset(offsetX, offsetY);
      SpreadsheetCellRange spreadsheetCellRange = this.MergedRange(selection.Anchor.Row, selection.Anchor.Column) ?? new SpreadsheetCellRange(selection.Anchor);
      SpreadsheetRect spreadsheetRect2 = this.Layout.Rect(spreadsheetCellRange).Offset(offsetX, offsetY);
      if (!selection.Range.IsSingleCell && selection.Range != spreadsheetCellRange)
      {
        using (CanvasGeometry canvasGeometry = CanvasGeometry.CreateRectangle(ds, SpreadsheetGridRenderer.R(spreadsheetRect)))
        {
          using (CanvasGeometry canvasGeometry2 = CanvasGeometry.CreateRectangle(ds, SpreadsheetGridRenderer.R(spreadsheetRect2)))
          {
            using (CanvasGeometry canvasGeometry3 = canvasGeometry.CombineWith(canvasGeometry2, Matrix3x2.Identity, CanvasGeometryCombine.Exclude))
            {
              ds.FillGeometry(canvasGeometry3, SpreadsheetGridPalette.SelectionFill);
            }
          }
        }
        SpreadsheetRect spreadsheetRect3 = spreadsheetRect2.Inset(0.5, 0.5).Offset(-0.5, -0.5);
        ds.DrawRectangle(SpreadsheetGridRenderer.R(spreadsheetRect3), SpreadsheetGridPalette.SelectionBorder, 1f);
      }
      ds.DrawRectangle(SpreadsheetGridRenderer.R(spreadsheetRect.Offset(-0.5, -0.5)), SpreadsheetGridPalette.SelectionBorder, 2f);
      SpreadsheetRect spreadsheetRect4 = new SpreadsheetRect(spreadsheetRect.MaxX - 4.0, spreadsheetRect.MaxY - 4.0, 6.0, 6.0);
      ds.FillRectangle(SpreadsheetGridRenderer.R(spreadsheetRect4.Inset(-1.0, -1.0)), Colors.White);
      ds.FillRectangle(SpreadsheetGridRenderer.R(spreadsheetRect4), SpreadsheetGridPalette.SelectionBorder);
    }

    // Token: 0x06001F11 RID: 7953 RVA: 0x0060BEBC File Offset: 0x0060BEBC
    private static CanvasTextFormat HeaderFont(bool selected)
    {
      return SpreadsheetTextAttributes.Shared.Format(10.5, selected ? FontWeights.SemiBold : FontWeights.Medium, false, false, CanvasHorizontalAlignment.Left);
    }

    // Token: 0x06001F12 RID: 7954 RVA: 0x0060BEE4 File Offset: 0x0060BEE4
    private void DrawLabel(CanvasDrawingSession ds, string label, SpreadsheetRect rect, bool selected, Color color, bool onlyIfFits)
    {
      CanvasTextFormat canvasTextFormat = SpreadsheetGridRenderer.HeaderFont(selected);
      using (CanvasTextLayout canvasTextLayout = new CanvasTextLayout(ds, label, canvasTextFormat, 1000f, 100f))
      {
        double width = canvasTextLayout.LayoutBoundsIncludingTrailingWhitespace.Width;
        double num = SpreadsheetTextAttributes.LabelHeight(10.5);
        if (!onlyIfFits || rect.Height >= num - 2.0)
        {
          double num2 = rect.MidY - num / 2.0 + SpreadsheetTextAttributes.Ascender(10.5) - this.attributes.Baseline(canvasTextFormat);
          ds.DrawTextLayout(canvasTextLayout, (float)(rect.MidX - width / 2.0), (float)num2, color);
        }
      }
    }

    // Token: 0x06001F13 RID: 7955 RVA: 0x0060BFB4 File Offset: 0x0060BFB4
    private void DrawColumnHeaders(CanvasDrawingSession ds, SpreadsheetPaneGeometry geometry, SpreadsheetRect strip, SpreadsheetRect dirtyRect)
    {
      ds.FillRectangle(SpreadsheetGridRenderer.R(dirtyRect), SpreadsheetGridPalette.HeaderFill);
      double offsetX = geometry.OffsetX;
      ValueTuple<int, int> valueTuple = SpreadsheetGridRenderer.VisibleRange(geometry.ColumnStart, geometry.ColumnEnd, dirtyRect.MinX - offsetX, dirtyRect.MaxX - offsetX, new Func<double, int>(this.Layout.Column));
      int item = valueTuple.Item1;
      int item2 = valueTuple.Item2;
      SpreadsheetSelection? spreadsheetSelection;
      SpreadsheetCellRange? spreadsheetCellRange = ((this.Selection != null) ? new SpreadsheetCellRange?(spreadsheetSelection.GetValueOrDefault().Range) : null);
      bool flag;
      if (spreadsheetCellRange != null)
      {
        SpreadsheetCellRange valueOrDefault = spreadsheetCellRange.GetValueOrDefault();
        if (valueOrDefault.FirstRow == 0)
        {
          flag = valueOrDefault.LastRow >= this.Layout.RowCount - 1;
          goto IL_00C7;
        }
      }
      flag = false;
      IL_00C7:
      bool flag2 = flag;
      for (int i = item; i < item2; i++)
      {
        if (this.Layout.ColumnWidths[i] > 0.0)
        {
          SpreadsheetRect spreadsheetRect = new SpreadsheetRect(this.Layout.ColumnOffsets[i] + offsetX, strip.MinY, this.Layout.ColumnWidths[i], strip.Height);
          if (spreadsheetCellRange == null)
          {
            goto IL_0159;
          }
          SpreadsheetCellRange valueOrDefault2 = spreadsheetCellRange.GetValueOrDefault();
          if (i < valueOrDefault2.FirstColumn)
          {
            goto IL_0159;
          }
          bool flag3 = i <= valueOrDefault2.LastColumn;
          IL_015A:
          bool flag4 = flag3;
          if (flag4)
          {
            ds.FillRectangle(SpreadsheetGridRenderer.R(spreadsheetRect), flag2 ? SpreadsheetGridPalette.HeaderFullySelectedFill : SpreadsheetGridPalette.HeaderSelectedFill);
          }
          this.DrawLabel(ds, SpreadsheetCellAddress.ColumnLetters(i), spreadsheetRect, flag4, flag4 ? (flag2 ? Colors.White : SpreadsheetGridPalette.HeaderSelectedText) : SpreadsheetGridPalette.HeaderText, false);
          ds.FillRectangle(SpreadsheetGridRenderer.R(spreadsheetRect.MaxX - 1.0, spreadsheetRect.MinY, 1.0, spreadsheetRect.Height), SpreadsheetGridPalette.HeaderLine);
          goto IL_01E3;
          IL_0159:
          flag3 = false;
          goto IL_015A;
        }
        IL_01E3:;
      }
      ds.FillRectangle(SpreadsheetGridRenderer.R(dirtyRect.MinX, strip.MaxY - 1.0, dirtyRect.Width, 1.0), SpreadsheetGridPalette.HeaderLine);
      if (spreadsheetCellRange != null)
      {
        SpreadsheetCellRange valueOrDefault3 = spreadsheetCellRange.GetValueOrDefault();
        if (item2 > item)
        {
          int num = Math.Max(valueOrDefault3.FirstColumn, item);
          int num2 = Math.Min(valueOrDefault3.LastColumn, item2 - 1);
          if (num <= num2)
          {
            ds.FillRectangle(SpreadsheetGridRenderer.R(this.Layout.ColumnOffsets[num] + offsetX, strip.MaxY - 2.0, this.Layout.ColumnOffsets[num2 + 1] - this.Layout.ColumnOffsets[num], 2.0), SpreadsheetGridPalette.SelectionBorder);
          }
        }
      }
    }

    // Token: 0x06001F14 RID: 7956 RVA: 0x0060C294 File Offset: 0x0060C294
    private void DrawRowHeaders(CanvasDrawingSession ds, SpreadsheetPaneGeometry geometry, SpreadsheetRect strip, SpreadsheetRect dirtyRect)
    {
      ds.FillRectangle(SpreadsheetGridRenderer.R(dirtyRect), SpreadsheetGridPalette.HeaderFill);
      double offsetY = geometry.OffsetY;
      ValueTuple<int, int> valueTuple = SpreadsheetGridRenderer.VisibleRange(geometry.RowStart, geometry.RowEnd, dirtyRect.MinY - offsetY, dirtyRect.MaxY - offsetY, new Func<double, int>(this.Layout.Row));
      int item = valueTuple.Item1;
      int item2 = valueTuple.Item2;
      SpreadsheetSelection? spreadsheetSelection;
      SpreadsheetCellRange? spreadsheetCellRange = ((this.Selection != null) ? new SpreadsheetCellRange?(spreadsheetSelection.GetValueOrDefault().Range) : null);
      bool flag;
      if (spreadsheetCellRange != null)
      {
        SpreadsheetCellRange valueOrDefault = spreadsheetCellRange.GetValueOrDefault();
        if (valueOrDefault.FirstColumn == 0)
        {
          flag = valueOrDefault.LastColumn >= this.Layout.ColumnCount - 1;
          goto IL_00C7;
        }
      }
      flag = false;
      IL_00C7:
      bool flag2 = flag;
      for (int i = item; i < item2; i++)
      {
        if (this.Layout.RowHeights[i] > 0.0)
        {
          SpreadsheetRect spreadsheetRect = new SpreadsheetRect(strip.MinX, this.Layout.RowOffsets[i] + offsetY, strip.Width, this.Layout.RowHeights[i]);
          if (spreadsheetCellRange == null)
          {
            goto IL_0159;
          }
          SpreadsheetCellRange valueOrDefault2 = spreadsheetCellRange.GetValueOrDefault();
          if (i < valueOrDefault2.FirstRow)
          {
            goto IL_0159;
          }
          bool flag3 = i <= valueOrDefault2.LastRow;
          IL_015A:
          bool flag4 = flag3;
          if (flag4)
          {
            ds.FillRectangle(SpreadsheetGridRenderer.R(spreadsheetRect), flag2 ? SpreadsheetGridPalette.HeaderFullySelectedFill : SpreadsheetGridPalette.HeaderSelectedFill);
          }
          this.DrawLabel(ds, (i + 1).ToString(CultureInfo.InvariantCulture), spreadsheetRect, flag4, flag4 ? (flag2 ? Colors.White : SpreadsheetGridPalette.HeaderSelectedText) : SpreadsheetGridPalette.HeaderText, true);
          ds.FillRectangle(SpreadsheetGridRenderer.R(spreadsheetRect.MinX, spreadsheetRect.MaxY - 1.0, spreadsheetRect.Width, 1.0), SpreadsheetGridPalette.HeaderLine);
          goto IL_01EE;
          IL_0159:
          flag3 = false;
          goto IL_015A;
        }
        IL_01EE:;
      }
      ds.FillRectangle(SpreadsheetGridRenderer.R(strip.MaxX - 1.0, dirtyRect.MinY, 1.0, dirtyRect.Height), SpreadsheetGridPalette.HeaderLine);
      if (spreadsheetCellRange != null)
      {
        SpreadsheetCellRange valueOrDefault3 = spreadsheetCellRange.GetValueOrDefault();
        if (item2 > item)
        {
          int num = Math.Max(valueOrDefault3.FirstRow, item);
          int num2 = Math.Min(valueOrDefault3.LastRow, item2 - 1);
          if (num <= num2)
          {
            ds.FillRectangle(SpreadsheetGridRenderer.R(strip.MaxX - 2.0, this.Layout.RowOffsets[num] + offsetY, 2.0, this.Layout.RowOffsets[num2 + 1] - this.Layout.RowOffsets[num]), SpreadsheetGridPalette.SelectionBorder);
          }
        }
      }
    }

    // Token: 0x06001F15 RID: 7957 RVA: 0x0060C57C File Offset: 0x0060C57C
    public void Dispose()
    {
      this.InvalidateTextMeasurements(null);
    }

    // Token: 0x06001F16 RID: 7958 RVA: 0x0060C598 File Offset: 0x0060C598
    [CompilerGenerated]
    internal static void <DrawBorders>g__Stroke|23_0(CanvasDrawingSession ds, SpreadsheetBorderEdge? edge, double x1, double y1, double x2, double y2)
    {
      if (edge != null)
      {
        SpreadsheetBorderEdge valueOrDefault = edge.GetValueOrDefault();
        Color color = SpreadsheetTextAttributes.ColorOf(valueOrDefault.Color);
        if (valueOrDefault.IsDashed)
        {
          using (CanvasStrokeStyle canvasStrokeStyle = new CanvasStrokeStyle
          {
            CustomDashStyle = new float[]
            {
              (float)(3.0 / valueOrDefault.Width),
              (float)(2.0 / valueOrDefault.Width)
            },
            DashCap = CanvasCapStyle.Flat
          })
          {
            ds.DrawLine((float)x1, (float)y1, (float)x2, (float)y2, color, (float)valueOrDefault.Width, canvasStrokeStyle);
            return;
          }
        }
        ds.DrawLine((float)x1, (float)y1, (float)x2, (float)y2, color, (float)valueOrDefault.Width);
        return;
      }
    }

    // Token: 0x04000EFA RID: 3834
    private readonly SpreadsheetTextAttributes attributes = SpreadsheetTextAttributes.Shared;

    // Token: 0x04000EFB RID: 3835
    private readonly Dictionary<int, List<int>> mergeIndicesByRow = new Dictionary<int, List<int>>();

    // Token: 0x04000EFC RID: 3836
    [TupleElementNames(new string[] { "Text", "Width", "Layout" })]
    [Nullable(new byte[] { 1, 0, 1, 1 })]
    private readonly Dictionary<SpreadsheetCellAddress, ValueTuple<string, double, CanvasTextLayout>> singleLineTextCache = new Dictionary<SpreadsheetCellAddress, ValueTuple<string, double, CanvasTextLayout>>();

    // Token: 0x04000EFD RID: 3837
    [TupleElementNames(new string[] { "Height", "Layout" })]
    [Nullable(new byte[] { 1, 1, 0, 1 })]
    private readonly Dictionary<string, ValueTuple<double, CanvasTextLayout>> wrappedCache = new Dictionary<string, ValueTuple<double, CanvasTextLayout>>();
  }
}
