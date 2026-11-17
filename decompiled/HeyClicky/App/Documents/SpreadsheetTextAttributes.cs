using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Documents.Spreadsheet;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.UI.Text;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002E4 RID: 740
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SpreadsheetTextAttributes : ISpreadsheetTextMeasurer
  {
    // Token: 0x06001F69 RID: 8041 RVA: 0x0060F838 File Offset: 0x0060F838
    public static Color Rgb(double r, double g, double b, double a = 1.0)
    {
      return Color.FromArgb((byte)Math.Round(a * 255.0), (byte)Math.Round(r * 255.0), (byte)Math.Round(g * 255.0), (byte)Math.Round(b * 255.0));
    }

    // Token: 0x06001F6A RID: 8042 RVA: 0x0060F890 File Offset: 0x0060F890
    public static Color ColorOf(SpreadsheetColor c)
    {
      return SpreadsheetTextAttributes.Rgb(c.Red, c.Green, c.Blue, c.Alpha);
    }

    // Token: 0x06001F6B RID: 8043 RVA: 0x0060F8B4 File Offset: 0x0060F8B4
    public static double Ascender(double size)
    {
      return size * 0.9521484375;
    }

    // Token: 0x06001F6C RID: 8044 RVA: 0x0060F8C4 File Offset: 0x0060F8C4
    public static double LineHeight(double size)
    {
      return Math.Ceiling(size * 1.193359375);
    }

    // Token: 0x06001F6D RID: 8045 RVA: 0x0060F8D8 File Offset: 0x0060F8D8
    public static double LabelHeight(double size)
    {
      return size * 1.193359375;
    }

    // Token: 0x06001F6E RID: 8046 RVA: 0x0060F8E8 File Offset: 0x0060F8E8
    public static double WrappedLinePitch(double size)
    {
      return Math.Ceiling(size * 1.193359375) + 1.0;
    }

    // Token: 0x06001F6F RID: 8047 RVA: 0x0060F904 File Offset: 0x0060F904
    private static string FamilyFor(double size)
    {
      if (size < 12.0)
      {
        return "Segoe UI Variable Small";
      }
      if (size < 20.0)
      {
        return "Segoe UI Variable Text";
      }
      return "Segoe UI Variable Display";
    }

    // Token: 0x06001F70 RID: 8048 RVA: 0x0060F930 File Offset: 0x0060F930
    public CanvasTextFormat Format(double size, FontWeight weight, bool italic, bool wraps = false, CanvasHorizontalAlignment alignment = CanvasHorizontalAlignment.Left)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 5);
      defaultInterpolatedStringHandler.AppendFormatted<double>(size);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<ushort>(weight.Weight);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(italic);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(wraps);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<CanvasHorizontalAlignment>(alignment);
      string text = defaultInterpolatedStringHandler.ToStringAndClear();
      CanvasTextFormat canvasTextFormat;
      if (this.formats.TryGetValue(text, out canvasTextFormat))
      {
        return canvasTextFormat;
      }
      CanvasTextFormat canvasTextFormat2 = new CanvasTextFormat
      {
        FontFamily = SpreadsheetTextAttributes.FamilyFor(size),
        FontSize = (float)size,
        FontWeight = weight,
        FontStyle = (italic ? FontStyle.Italic : FontStyle.Normal),
        WordWrapping = (wraps ? CanvasWordWrapping.Wrap : CanvasWordWrapping.NoWrap),
        HorizontalAlignment = alignment,
        Options = CanvasDrawTextOptions.EnableColorFont
      };
      if (wraps)
      {
        canvasTextFormat2.LineSpacingMode = CanvasLineSpacingMode.Uniform;
        canvasTextFormat2.LineSpacing = (float)SpreadsheetTextAttributes.WrappedLinePitch(size);
        canvasTextFormat2.LineSpacingBaseline = (float)SpreadsheetTextAttributes.Ascender(size);
      }
      this.formats[text] = canvasTextFormat2;
      return canvasTextFormat2;
    }

    // Token: 0x06001F71 RID: 8049 RVA: 0x0060FA40 File Offset: 0x0060FA40
    public CanvasTextFormat CellFormat(SpreadsheetCellStyle style, bool wraps = false, CanvasHorizontalAlignment alignment = CanvasHorizontalAlignment.Left)
    {
      return this.Format(style.FontSize, style.IsBold ? FontWeights.SemiBold : FontWeights.Normal, style.IsItalic, wraps, alignment);
    }

    // Token: 0x06001F72 RID: 8050 RVA: 0x0060FA6C File Offset: 0x0060FA6C
    public double Baseline(CanvasTextFormat format)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 4);
      defaultInterpolatedStringHandler.AppendFormatted(format.FontFamily);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<float>(format.FontSize);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<ushort>(format.FontWeight.Weight);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<FontStyle>(format.FontStyle);
      string text = defaultInterpolatedStringHandler.ToStringAndClear();
      double num;
      if (this.baselines.TryGetValue(text, out num))
      {
        return num;
      }
      double num3;
      using (CanvasTextLayout canvasTextLayout = new CanvasTextLayout(CanvasDevice.GetSharedDevice(), "Ag", format, 1000f, 1000f))
      {
        double num2 = ((canvasTextLayout.LineMetrics.Length != 0) ? ((double)canvasTextLayout.LineMetrics[0].Baseline) : ((double)format.FontSize * 0.8));
        this.baselines[text] = num2;
        num3 = num2;
      }
      return num3;
    }

    // Token: 0x06001F73 RID: 8051 RVA: 0x0060FB74 File Offset: 0x0060FB74
    public CanvasTextLayout SingleLineLayout(ICanvasResourceCreator device, string text, CanvasTextFormat format, bool underline, bool strike)
    {
      CanvasTextLayout canvasTextLayout = new CanvasTextLayout(device, text, format, 100000f, 1000f);
      if (underline && text.Length > 0)
      {
        canvasTextLayout.SetUnderline(0, text.Length, true);
      }
      if (strike && text.Length > 0)
      {
        canvasTextLayout.SetStrikethrough(0, text.Length, true);
      }
      return canvasTextLayout;
    }

    // Token: 0x06001F74 RID: 8052 RVA: 0x0060FBCC File Offset: 0x0060FBCC
    public CanvasTextLayout WrappedLayout(ICanvasResourceCreator device, string text, SpreadsheetCellStyle style, double width, CanvasHorizontalAlignment alignment, bool underline)
    {
      CanvasTextLayout canvasTextLayout = new CanvasTextLayout(device, text, this.CellFormat(style, true, alignment), (float)Math.Max(1.0, width), 100000f);
      if ((style.IsUnderlined || underline) && text.Length > 0)
      {
        canvasTextLayout.SetUnderline(0, text.Length, true);
      }
      if (style.IsStruckThrough && text.Length > 0)
      {
        canvasTextLayout.SetStrikethrough(0, text.Length, true);
      }
      return canvasTextLayout;
    }

    // Token: 0x06001F75 RID: 8053 RVA: 0x0060FC44 File Offset: 0x0060FC44
    public double SingleLineWidth(string text, SpreadsheetCellStyle style)
    {
      double width;
      using (CanvasTextLayout canvasTextLayout = new CanvasTextLayout(CanvasDevice.GetSharedDevice(), text, this.CellFormat(style, false, CanvasHorizontalAlignment.Left), 100000f, 1000f))
      {
        width = canvasTextLayout.LayoutBoundsIncludingTrailingWhitespace.Width;
      }
      return width;
    }

    // Token: 0x06001F76 RID: 8054 RVA: 0x0060FC9C File Offset: 0x0060FC9C
    public double WrappedHeight(string text, SpreadsheetCellStyle style, double width)
    {
      double num;
      using (CanvasTextLayout canvasTextLayout = this.WrappedLayout(CanvasDevice.GetSharedDevice(), text, style, width, CanvasHorizontalAlignment.Left, false))
      {
        num = (double)canvasTextLayout.LineMetrics.Length * SpreadsheetTextAttributes.WrappedLinePitch(style.FontSize);
      }
      return num;
    }

    // Token: 0x04000F27 RID: 3879
    public static readonly SpreadsheetTextAttributes Shared = new SpreadsheetTextAttributes();

    // Token: 0x04000F28 RID: 3880
    public static readonly Color DefaultTextColor = SpreadsheetTextAttributes.Rgb(0.12, 0.12, 0.12, 1.0);

    // Token: 0x04000F29 RID: 3881
    public static readonly Color HyperlinkColor = SpreadsheetTextAttributes.Rgb(0.067, 0.333, 0.8, 1.0);

    // Token: 0x04000F2A RID: 3882
    private const double SfAscender = 0.9521484375;

    // Token: 0x04000F2B RID: 3883
    private const double SfDescender = 0.2412109375;

    // Token: 0x04000F2C RID: 3884
    private readonly Dictionary<string, CanvasTextFormat> formats = new Dictionary<string, CanvasTextFormat>();

    // Token: 0x04000F2D RID: 3885
    private readonly Dictionary<string, double> baselines = new Dictionary<string, double>();
  }
}
