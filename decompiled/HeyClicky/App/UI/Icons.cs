using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace HeyClicky.App.UI
{
  // Token: 0x020000AB RID: 171
  [NullableContext(1)]
  [Nullable(0)]
  public static class Icons
  {
    // Token: 0x0600071D RID: 1821 RVA: 0x005356DC File Offset: 0x005356DC
    public static Path PathIcon(string data, double size, Color color)
    {
      Geometry geometry = (Geometry)XamlBindingHelper.ConvertValue(typeof(Geometry), data);
      return new Path
      {
        Data = geometry,
        Fill = Ui.Brush(color),
        Width = size,
        Height = size,
        Stretch = Stretch.Uniform,
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Center
      };
    }

    // Token: 0x0600071E RID: 1822 RVA: 0x0053573C File Offset: 0x0053573C
    [NullableContext(2)]
    [return: Nullable(1)]
    public static Viewbox Vector(double size, Color color, string fill, string stroke = null, double strokeWidth = 2.0)
    {
      Grid grid = new Grid
      {
        Width = 24.0,
        Height = 24.0
      };
      if (fill != null)
      {
        grid.Children.Add(new Path
        {
          Data = Icons.<Vector>g__Parse|50_0(fill),
          Fill = Ui.Brush(color)
        });
      }
      if (stroke != null)
      {
        grid.Children.Add(new Path
        {
          Data = Icons.<Vector>g__Parse|50_0(stroke),
          Stroke = Ui.Brush(color),
          StrokeThickness = strokeWidth,
          StrokeStartLineCap = PenLineCap.Round,
          StrokeEndLineCap = PenLineCap.Round,
          StrokeLineJoin = PenLineJoin.Round
        });
      }
      return new Viewbox
      {
        Width = size,
        Height = size,
        Child = grid,
        Stretch = Stretch.Uniform,
        IsHitTestVisible = false,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      };
    }

    // Token: 0x0600071F RID: 1823 RVA: 0x00535818 File Offset: 0x00535818
    public static Path StrokeIcon(string data, double size, Color color, double strokeThickness)
    {
      Geometry geometry = (Geometry)XamlBindingHelper.ConvertValue(typeof(Geometry), data);
      return new Path
      {
        Data = geometry,
        Stroke = Ui.Brush(color),
        StrokeThickness = strokeThickness,
        StrokeStartLineCap = PenLineCap.Round,
        StrokeEndLineCap = PenLineCap.Round,
        StrokeLineJoin = PenLineJoin.Round,
        Width = size,
        Height = size,
        Stretch = Stretch.Uniform,
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Center
      };
    }

    // Token: 0x06000720 RID: 1824 RVA: 0x00535894 File Offset: 0x00535894
    public static TextBlock Glyph(string glyph, double size, Color color)
    {
      return new TextBlock
      {
        Text = glyph,
        FontFamily = DS.Fonts.Symbols,
        FontSize = size,
        Foreground = Ui.Brush(color),
        IsTextScaleFactorEnabled = false,
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Center
      };
    }

    // Token: 0x06000721 RID: 1825 RVA: 0x005358E0 File Offset: 0x005358E0
    [CompilerGenerated]
    internal static Geometry <Vector>g__Parse|50_0(string d)
    {
      return (Geometry)XamlBindingHelper.ConvertValue(typeof(Geometry), d);
    }

    // Token: 0x040001E9 RID: 489
    public const string XMark = "\ue711";

    // Token: 0x040001EA RID: 490
    public const string Check = "\ue73e";

    // Token: 0x040001EB RID: 491
    public const string ArrowUp = "\ue74a";

    // Token: 0x040001EC RID: 492
    public const string Paperclip = "\ue723";

    // Token: 0x040001ED RID: 493
    public const string Copy = "\ue8c8";

    // Token: 0x040001EE RID: 494
    public const string Mic = "\ue720";

    // Token: 0x040001EF RID: 495
    public const string MicOff = "\uf781";

    // Token: 0x040001F0 RID: 496
    public const string Speaker = "\ue767";

    // Token: 0x040001F1 RID: 497
    public const string SpeakerMute = "\ue74f";

    // Token: 0x040001F2 RID: 498
    public const string Play = "\ue768";

    // Token: 0x040001F3 RID: 499
    public const string Pause = "\ue769";

    // Token: 0x040001F4 RID: 500
    public const string Clock = "\ue823";

    // Token: 0x040001F5 RID: 501
    public const string Link = "\ue71b";

    // Token: 0x040001F6 RID: 502
    public const string Calendar = "\ue787";

    // Token: 0x040001F7 RID: 503
    public const string ChevronUp = "\ue70e";

    // Token: 0x040001F8 RID: 504
    public const string ChevronDown = "\ue70d";

    // Token: 0x040001F9 RID: 505
    public const string ChevronLeft = "\ue76b";

    // Token: 0x040001FA RID: 506
    public const string ChevronRight = "\ue76c";

    // Token: 0x040001FB RID: 507
    public const string More = "\ue712";

    // Token: 0x040001FC RID: 508
    public const string Refresh = "\ue72c";

    // Token: 0x040001FD RID: 509
    public const string Settings = "\ue713";

    // Token: 0x040001FE RID: 510
    public const string Info = "\ue946";

    // Token: 0x040001FF RID: 511
    public const string Keyboard = "\ue765";

    // Token: 0x04000200 RID: 512
    public const string Video = "\ue714";

    // Token: 0x04000201 RID: 513
    public const string People = "\ue716";

    // Token: 0x04000202 RID: 514
    public const string Warning = "\ue7ba";

    // Token: 0x04000203 RID: 515
    public const string Send = "\ue724";

    // Token: 0x04000204 RID: 516
    public const string Pen = "\ue70f";

    // Token: 0x04000205 RID: 517
    public const string Lightbulb = "\ue82f";

    // Token: 0x04000206 RID: 518
    public const string OpenOut = "\ue8a7";

    // Token: 0x04000207 RID: 519
    public const string Stop = "\ue71a";

    // Token: 0x04000208 RID: 520
    public const string Window = "\ue737";

    // Token: 0x04000209 RID: 521
    public const string Upload = "\ue898";

    // Token: 0x0400020A RID: 522
    public const string AddFile = "\ue8e5";

    // Token: 0x0400020B RID: 523
    public const string Share = "\ue72d";

    // Token: 0x0400020C RID: 524
    public const string Sparkle = "\uf4a5";

    // Token: 0x0400020D RID: 525
    public const string Terminal = "\ue756";

    // Token: 0x0400020E RID: 526
    public const string Folder = "\ue8b7";

    // Token: 0x0400020F RID: 527
    public const string Expand = "\ue740";

    // Token: 0x04000210 RID: 528
    public const string Download = "\ue896";

    // Token: 0x04000211 RID: 529
    public const string Globe = "\ue774";

    // Token: 0x04000212 RID: 530
    public const string Home = "\ue80f";

    // Token: 0x04000213 RID: 531
    public const string Newspaper = "\ue736";

    // Token: 0x04000214 RID: 532
    public const string PlaySolid = "\uf5b0";

    // Token: 0x04000215 RID: 533
    public const string VideoSolid = "\uea0c";

    // Token: 0x04000216 RID: 534
    public const string MicSolid = "\uec71";

    // Token: 0x04000217 RID: 535
    public const string CheckCircleSolid = "\uec61";

    // Token: 0x04000218 RID: 536
    public const string ArrowUpToLine = "\ue898";

    // Token: 0x04000219 RID: 537
    public const string WindowOnRectangle = "\ue7c4";

    // Token: 0x0400021A RID: 538
    public const string ArrowUpRightPath = "M5,19 L19,5 M8,5 L19,5 L19,16";

    // Token: 0x0400021B RID: 539
    public const string SparklesPath = "M10,3 C10.6,7.6 12.4,9.4 17,10 C12.4,10.6 10.6,12.4 10,17 C9.4,12.4 7.6,10.6 3,10 C7.6,9.4 9.4,7.6 10,3 Z M18,13 C18.3,15.2 18.8,15.7 21,16 C18.8,16.3 18.3,16.8 18,19 C17.7,16.8 17.2,16.3 15,16 C17.2,15.7 17.7,15.2 18,13 Z M17,2 C17.2,3.4 17.6,3.8 19,4 C17.6,4.2 17.2,4.6 17,6 C16.8,4.6 16.4,4.2 15,4 C16.4,3.8 16.8,3.4 17,2 Z";

    // Token: 0x0400021C RID: 540
    public const string WaveformPath = "M3,10 h2 v4 h-2 z M7,7 h2 v10 h-2 z M11,3 h2 v18 h-2 z M15,6 h2 v12 h-2 z M19,9 h2 v6 h-2 z";
  }
}
