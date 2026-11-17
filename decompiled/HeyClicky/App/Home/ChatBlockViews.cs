using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Portraits;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace HeyClicky.App.Home
{
  // Token: 0x020002AB RID: 683
  [NullableContext(1)]
  [Nullable(0)]
  public static class ChatBlockViews
  {
    // Token: 0x06001BFA RID: 7162 RVA: 0x005F74E0 File Offset: 0x005F74E0
    public static bool IsImage(string p)
    {
      string text = Path.GetExtension(p).ToLowerInvariant();
      if (text != null)
      {
        int length = text.Length;
        if (length != 4)
        {
          if (length != 5)
          {
            goto IL_00D5;
          }
          char c = text[1];
          if (c != 'j')
          {
            if (c != 't')
            {
              if (c != 'w')
              {
                goto IL_00D5;
              }
              if (!(text == ".webp"))
              {
                goto IL_00D5;
              }
            }
            else if (!(text == ".tiff"))
            {
              goto IL_00D5;
            }
          }
          else if (!(text == ".jpeg"))
          {
            goto IL_00D5;
          }
        }
        else
        {
          char c = text[1];
          if (c <= 'g')
          {
            if (c != 'b')
            {
              if (c != 'g')
              {
                goto IL_00D5;
              }
              if (!(text == ".gif"))
              {
                goto IL_00D5;
              }
            }
            else if (!(text == ".bmp"))
            {
              goto IL_00D5;
            }
          }
          else if (c != 'j')
          {
            if (c != 'p')
            {
              goto IL_00D5;
            }
            if (!(text == ".png"))
            {
              goto IL_00D5;
            }
          }
          else if (!(text == ".jpg"))
          {
            goto IL_00D5;
          }
        }
        return true;
      }
      IL_00D5:
      return false;
    }

    // Token: 0x06001BFB RID: 7163 RVA: 0x005F75C8 File Offset: 0x005F75C8
    public static FrameworkElement SentBubblePlate(bool tail)
    {
      return ChatBlockViews.BubblePlate(ChatBlockViews.SentFill(), ChatBlockViews.SentStroke(), tail, true, 17.0, 7.0);
    }

    // Token: 0x06001BFC RID: 7164 RVA: 0x005F75F0 File Offset: 0x005F75F0
    public static FrameworkElement IncomingBubblePlate(bool tail)
    {
      return ChatBlockViews.BubblePlate(Ui.Brush(HomeTheme.IncomingBubble), null, tail, false, 17.0, 7.0);
    }

    // Token: 0x06001BFD RID: 7165 RVA: 0x005F7618 File Offset: 0x005F7618
    private static Brush SentFill()
    {
      return Ui.Vertical(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, DS.Hex(15594751U, 1.0)),
        new ValueTuple<double, Color>(0.18, DS.Hex(14084095U, 1.0)),
        new ValueTuple<double, Color>(0.5, DS.Hex(10668031U, 1.0)),
        new ValueTuple<double, Color>(0.8, DS.Hex(10079224U, 1.0)),
        new ValueTuple<double, Color>(1.0, DS.Hex(10670839U, 1.0))
      });
    }

    // Token: 0x06001BFE RID: 7166 RVA: 0x005F76F8 File Offset: 0x005F76F8
    private static Brush SentStroke()
    {
      return Ui.Vertical(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, DS.Hex(2234520U, 1.0)),
        new ValueTuple<double, Color>(1.0, DS.Hex(6447714U, 1.0))
      });
    }

    // Token: 0x06001BFF RID: 7167 RVA: 0x005F7760 File Offset: 0x005F7760
    internal static FrameworkElement BubblePlate(Brush fill, [Nullable(2)] Brush stroke, bool tail, bool mirrored, double corner = 17.0, double tailDrop = 7.0)
    {
      if (!tail)
      {
        return new Border
        {
          CornerRadius = new CornerRadius(corner),
          Background = fill,
          IsHitTestVisible = false,
          BorderBrush = stroke,
          BorderThickness = new Thickness((stroke == null) ? 0.0 : 1.3)
        };
      }
      Path path = new Path
      {
        Fill = fill,
        Stroke = stroke,
        StrokeThickness = ((stroke == null) ? 0.0 : 1.3),
        IsHitTestVisible = false,
        Stretch = Stretch.None
      };
      Grid grid = new Grid();
      grid.IsHitTestVisible = false;
      grid.Children.Add(path);
      grid.SizeChanged += delegate(object _, SizeChangedEventArgs e)
      {
        double width = e.NewSize.Width;
        double height = e.NewSize.Height;
        if (width <= 0.0 || height <= 0.0)
        {
          return;
        }
        double num = ((stroke == null) ? 0.0 : 0.65);
        path.Data = ChatBlockViews.BubblePath(width - 2.0 * num, height - 2.0 * num, corner, tailDrop).ToXaml();
        UIElement path2 = path;
        Transform transform;
        if (!mirrored)
        {
          TranslateTransform translateTransform = new TranslateTransform();
          translateTransform.X = num;
          transform = translateTransform;
          translateTransform.Y = num;
        }
        else
        {
          CompositeTransform compositeTransform = new CompositeTransform();
          compositeTransform.ScaleX = -1.0;
          compositeTransform.TranslateX = width - num;
          transform = compositeTransform;
          compositeTransform.TranslateY = num;
        }
        path2.RenderTransform = transform;
      };
      return grid;
    }

    // Token: 0x06001C00 RID: 7168 RVA: 0x005F786C File Offset: 0x005F786C
    private static PPath BubblePath(double w, double h, double corner, double tailDrop)
    {
      ChatBlockViews.<>c__DisplayClass7_0 CS$<>8__locals1;
      CS$<>8__locals1.bodyH = h - tailDrop;
      double num = Math.Min(corner, CS$<>8__locals1.bodyH / 2.0);
      CS$<>8__locals1.s = tailDrop / 9.0;
      CS$<>8__locals1.p = new PPath().M(num, 0.0).L(w - num, 0.0);
      CS$<>8__locals1.p.C(w - num * 0.45, 0.0, w, num * 0.45, w, num).L(w, CS$<>8__locals1.bodyH - num).C(w, CS$<>8__locals1.bodyH - num * 0.45, w - num * 0.45, CS$<>8__locals1.bodyH, w - num, CS$<>8__locals1.bodyH);
      ValueTuple<double, double> valueTuple = ChatBlockViews.<BubblePath>g__C|7_0(32.13, 0.0, ref CS$<>8__locals1);
      CS$<>8__locals1.p.L(valueTuple.Item1, valueTuple.Item2);
      ChatBlockViews.<BubblePath>g__Cv|7_1(ChatBlockViews.<BubblePath>g__C|7_0(26.48, 0.0, ref CS$<>8__locals1), ChatBlockViews.<BubblePath>g__C|7_0(21.08, 1.97, ref CS$<>8__locals1), ChatBlockViews.<BubblePath>g__C|7_0(16.12, 4.69, ref CS$<>8__locals1), ref CS$<>8__locals1);
      ChatBlockViews.<BubblePath>g__Cv|7_1(ChatBlockViews.<BubblePath>g__C|7_0(12.04, 6.95, ref CS$<>8__locals1), ChatBlockViews.<BubblePath>g__C|7_0(8.53, 8.49, ref CS$<>8__locals1), ChatBlockViews.<BubblePath>g__C|7_0(7.66, 8.55, ref CS$<>8__locals1), ref CS$<>8__locals1);
      ChatBlockViews.<BubblePath>g__Cv|7_1(ChatBlockViews.<BubblePath>g__C|7_0(5.03, 8.76, ref CS$<>8__locals1), ChatBlockViews.<BubblePath>g__C|7_0(4.69, 6.28, ref CS$<>8__locals1), ChatBlockViews.<BubblePath>g__C|7_0(5.72, 4.94, ref CS$<>8__locals1), ref CS$<>8__locals1);
      ChatBlockViews.<BubblePath>g__Cv|7_1(ChatBlockViews.<BubblePath>g__C|7_0(6.74, 3.61, ref CS$<>8__locals1), ChatBlockViews.<BubblePath>g__C|7_0(7.91, 1.88, ref CS$<>8__locals1), ChatBlockViews.<BubblePath>g__C|7_0(8.36, 0.94, ref CS$<>8__locals1), ref CS$<>8__locals1);
      ChatBlockViews.<BubblePath>g__Cv|7_1(ChatBlockViews.<BubblePath>g__C|7_0(9.38, -1.19, ref CS$<>8__locals1), ChatBlockViews.<BubblePath>g__C|7_0(8.18, -3.45, ref CS$<>8__locals1), ChatBlockViews.<BubblePath>g__C|7_0(6.43, -5.03, ref CS$<>8__locals1), ref CS$<>8__locals1);
      ChatBlockViews.<BubblePath>g__Cv|7_1(ChatBlockViews.<BubblePath>g__C|7_0(2.48, -8.6, ref CS$<>8__locals1), new ValueTuple<double, double>(0.0, CS$<>8__locals1.bodyH - num * 0.86), new ValueTuple<double, double>(0.0, CS$<>8__locals1.bodyH - num), ref CS$<>8__locals1);
      CS$<>8__locals1.p.L(0.0, num).C(0.0, num * 0.45, num * 0.45, 0.0, num, 0.0).Z();
      return CS$<>8__locals1.p;
    }

    // Token: 0x06001C01 RID: 7169 RVA: 0x005F7BC4 File Offset: 0x005F7BC4
    public static void OpenPath(string path)
    {
      try
      {
        ShellOpen.Start(path);
      }
      catch (Exception ex)
      {
        Log.Warn("chat", "open " + path + ": " + ex.Message);
      }
    }

    // Token: 0x06001C02 RID: 7170 RVA: 0x005F7C0C File Offset: 0x005F7C0C
    public static void RevealPath(string path)
    {
      try
      {
        Process.Start("explorer.exe", "/select,\"" + path + "\"");
      }
      catch (Exception ex)
      {
        Log.Warn("chat", "reveal " + path + ": " + ex.Message);
      }
    }

    // Token: 0x06001C03 RID: 7171 RVA: 0x005F7C6C File Offset: 0x005F7C6C
    public static string Kind(string path)
    {
      string text = Path.GetExtension(path).TrimStart('.').ToLowerInvariant();
      if (text != null)
      {
        switch (text.Length)
        {
        case 0:
          return "Folder";
        case 1:
        {
          char c = text[0];
          if (c != 'c' && c != 'h')
          {
            goto IL_072B;
          }
          goto IL_070B;
        }
        case 2:
        {
          char c = text[0];
          if (c != '7')
          {
            switch (c)
            {
            case 'g':
              if (text == "go")
              {
                goto IL_070B;
              }
              if (!(text == "gz"))
              {
                goto IL_072B;
              }
              goto IL_071B;
            case 'h':
            case 'i':
            case 'l':
            case 'n':
            case 'o':
            case 'q':
              goto IL_072B;
            case 'j':
              if (!(text == "js"))
              {
                goto IL_072B;
              }
              goto IL_070B;
            case 'k':
              if (!(text == "kt"))
              {
                goto IL_072B;
              }
              goto IL_070B;
            case 'm':
              if (!(text == "md"))
              {
                goto IL_072B;
              }
              goto IL_06D3;
            case 'p':
              if (!(text == "py"))
              {
                goto IL_072B;
              }
              goto IL_070B;
            case 'r':
              if (!(text == "rb") && !(text == "rs"))
              {
                goto IL_072B;
              }
              goto IL_070B;
            case 's':
              if (!(text == "sh"))
              {
                goto IL_072B;
              }
              goto IL_070B;
            case 't':
              if (!(text == "ts"))
              {
                goto IL_072B;
              }
              goto IL_070B;
            default:
              goto IL_072B;
            }
          }
          else
          {
            if (!(text == "7z"))
            {
              goto IL_072B;
            }
            goto IL_071B;
          }
          break;
        }
        case 3:
          switch (text[0])
          {
          case 'a':
            if (text == "aac")
            {
              goto IL_06FB;
            }
            if (!(text == "app"))
            {
              goto IL_072B;
            }
            break;
          case 'b':
            if (text == "bmp")
            {
              goto IL_06BB;
            }
            if (!(text == "bz2"))
            {
              goto IL_072B;
            }
            goto IL_071B;
          case 'c':
            if (text == "csv")
            {
              goto IL_06C3;
            }
            if (!(text == "cpp") && !(text == "css"))
            {
              goto IL_072B;
            }
            goto IL_070B;
          case 'd':
            if (text == "doc")
            {
              goto IL_06CB;
            }
            if (!(text == "dmg"))
            {
              goto IL_072B;
            }
            goto IL_071B;
          case 'e':
            if (!(text == "exe"))
            {
              goto IL_072B;
            }
            break;
          case 'f':
          case 'i':
          case 'l':
          case 'n':
          case 'q':
          case 'u':
          case 'v':
            goto IL_072B;
          case 'g':
            if (!(text == "gif"))
            {
              goto IL_072B;
            }
            goto IL_06BB;
          case 'h':
            if (text == "htm")
            {
              goto IL_06E3;
            }
            if (!(text == "hpp"))
            {
              goto IL_072B;
            }
            goto IL_070B;
          case 'j':
            if (text == "jpg")
            {
              goto IL_06BB;
            }
            if (!(text == "jsx"))
            {
              goto IL_072B;
            }
            goto IL_070B;
          case 'k':
            if (!(text == "key"))
            {
              goto IL_072B;
            }
            goto IL_06EB;
          case 'm':
            if (text == "mp4" || text == "mov" || text == "m4v")
            {
              goto IL_06F3;
            }
            if (!(text == "mp3") && !(text == "m4a"))
            {
              goto IL_072B;
            }
            goto IL_06FB;
          case 'o':
            if (!(text == "odt"))
            {
              goto IL_072B;
            }
            goto IL_06CB;
          case 'p':
            if (text == "pdf")
            {
              return "PDF";
            }
            if (text == "png")
            {
              goto IL_06BB;
            }
            if (!(text == "ppt"))
            {
              goto IL_072B;
            }
            goto IL_06EB;
          case 'r':
            if (!(text == "rtf"))
            {
              goto IL_072B;
            }
            goto IL_06CB;
          case 's':
            if (!(text == "svg"))
            {
              goto IL_072B;
            }
            goto IL_06BB;
          case 't':
            if (text == "tsv")
            {
              goto IL_06C3;
            }
            if (text == "txt")
            {
              return "Text";
            }
            if (text == "tsx")
            {
              goto IL_070B;
            }
            if (!(text == "tar"))
            {
              goto IL_072B;
            }
            goto IL_071B;
          case 'w':
            if (!(text == "wav"))
            {
              goto IL_072B;
            }
            goto IL_06FB;
          case 'x':
            if (text == "xls")
            {
              goto IL_06C3;
            }
            if (!(text == "xml"))
            {
              goto IL_072B;
            }
            goto IL_0703;
          case 'y':
            if (!(text == "yml"))
            {
              goto IL_072B;
            }
            goto IL_0703;
          case 'z':
            if (!(text == "zip"))
            {
              goto IL_072B;
            }
            goto IL_071B;
          default:
            goto IL_072B;
          }
          return "App";
        case 4:
        {
          char c = text[2];
          switch (c)
          {
          case 'a':
            if (!(text == "flac"))
            {
              goto IL_072B;
            }
            goto IL_06FB;
          case 'b':
            if (!(text == "webp"))
            {
              if (!(text == "webm"))
              {
                goto IL_072B;
              }
              goto IL_06F3;
            }
            break;
          case 'c':
            if (!(text == "docx"))
            {
              goto IL_072B;
            }
            goto IL_06CB;
          case 'd':
          case 'g':
          case 'h':
          case 'j':
          case 'k':
          case 'l':
          case 'n':
            goto IL_072B;
          case 'e':
            if (!(text == "jpeg"))
            {
              goto IL_072B;
            }
            break;
          case 'f':
            if (!(text == "tiff"))
            {
              goto IL_072B;
            }
            break;
          case 'i':
            if (!(text == "heic"))
            {
              goto IL_072B;
            }
            break;
          case 'm':
            if (text == "html")
            {
              goto IL_06E3;
            }
            if (!(text == "yaml") && !(text == "toml"))
            {
              goto IL_072B;
            }
            goto IL_0703;
          case 'o':
            if (!(text == "json"))
            {
              goto IL_072B;
            }
            goto IL_0703;
          default:
            switch (c)
            {
            case 's':
              if (text == "xlsx")
              {
                goto IL_06C3;
              }
              if (!(text == "scss"))
              {
                goto IL_072B;
              }
              goto IL_070B;
            case 't':
              if (!(text == "pptx"))
              {
                goto IL_072B;
              }
              goto IL_06EB;
            case 'u':
              goto IL_072B;
            case 'v':
              if (!(text == "java"))
              {
                goto IL_072B;
              }
              goto IL_070B;
            default:
              goto IL_072B;
            }
            break;
          }
          break;
        }
        case 5:
        {
          char c = text[0];
          if (c != 'p')
          {
            if (c != 's')
            {
              goto IL_072B;
            }
            if (!(text == "swift"))
            {
              goto IL_072B;
            }
            goto IL_070B;
          }
          else
          {
            if (!(text == "pages"))
            {
              goto IL_072B;
            }
            goto IL_06CB;
          }
          break;
        }
        case 6:
          goto IL_072B;
        case 7:
          if (!(text == "numbers"))
          {
            goto IL_072B;
          }
          goto IL_06C3;
        case 8:
          if (!(text == "markdown"))
          {
            goto IL_072B;
          }
          goto IL_06D3;
        default:
          goto IL_072B;
        }
        IL_06BB:
        return "Image";
        IL_06C3:
        return "Spreadsheet";
        IL_06CB:
        return "Document";
        IL_06D3:
        return "Markdown";
        IL_06E3:
        return "Web page";
        IL_06EB:
        return "Slides";
        IL_06F3:
        return "Video";
        IL_06FB:
        return "Audio";
        IL_0703:
        return "Data";
        IL_070B:
        return "Code";
        IL_071B:
        return "Archive";
      }
      IL_072B:
      return text.ToUpperInvariant();
    }

    // Token: 0x06001C04 RID: 7172 RVA: 0x005F83AC File Offset: 0x005F83AC
    [NullableContext(0)]
    [CompilerGenerated]
    internal static ValueTuple<double, double> <BubblePath>g__C|7_0(double dx, double dy, ref ChatBlockViews.<>c__DisplayClass7_0 A_2)
    {
      return new ValueTuple<double, double>(dx * A_2.s, A_2.bodyH + dy * A_2.s);
    }

    // Token: 0x06001C05 RID: 7173 RVA: 0x005F83CC File Offset: 0x005F83CC
    [NullableContext(0)]
    [CompilerGenerated]
    internal static void <BubblePath>g__Cv|7_1(ValueTuple<double, double> c1, ValueTuple<double, double> c2, ValueTuple<double, double> to, ref ChatBlockViews.<>c__DisplayClass7_0 A_3)
    {
      A_3.p.C(c1.Item1, c1.Item2, c2.Item1, c2.Item2, to.Item1, to.Item2);
    }

    // Token: 0x04000D73 RID: 3443
    public const double BubbleTailDrop = 7.0;
  }
}
