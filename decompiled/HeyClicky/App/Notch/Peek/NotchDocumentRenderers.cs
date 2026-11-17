using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using HeyClicky.Core.Documents;
using HeyClicky.Core.Voice;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.UI;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000182 RID: 386
  [NullableContext(1)]
  [Nullable(0)]
  public static class NotchDocumentRenderers
  {
    // Token: 0x170003BF RID: 959
    // (get) Token: 0x060014FE RID: 5374 RVA: 0x005B3A58 File Offset: 0x005B3A58
    private static Markdown.Options ReaderStyle
    {
      get
      {
        if (!MessagesPalette.Current.IsDark)
        {
          return NotchDocumentRenderers.LightReaderStyle;
        }
        return NotchDocumentRenderers.DarkReaderStyle;
      }
    }

    // Token: 0x060014FF RID: 5375 RVA: 0x005B3A74 File Offset: 0x005B3A74
    private static Markdown.Options ReaderStyleIn(MessagesPalette p)
    {
      return new Markdown.Options(14.0, p.PrimaryText, p.LinkBlue, p.SecondaryText)
      {
        BlockSpacing = 12.0,
        HeadingSizes = new double[] { 20.0, 17.0, 15.0, 14.0 },
        LineSpacing = 6.0,
        ListRowSpacing = 4.0,
        BulletIndent = 20.0,
        NumberIndent = 28.0,
        CodeFill = p.ComposerFieldFill,
        CodeStroke = p.ComposerFieldStroke,
        CodePadding = new Thickness(12.0, 10.0, 12.0, 10.0),
        CodeCopyButton = true
      };
    }

    // Token: 0x06001500 RID: 5376 RVA: 0x005B3B54 File Offset: 0x005B3B54
    public static INotchDocumentRenderer For(string path)
    {
      string text = Path.GetExtension(path).ToLowerInvariant();
      if (text != null)
      {
        switch (text.Length)
        {
        case 3:
          if (!(text == ".md"))
          {
            goto IL_031E;
          }
          break;
        case 4:
        {
          char c = text[1];
          if (c != 'b')
          {
            switch (c)
            {
            case 'g':
              if (!(text == ".gif"))
              {
                goto IL_031E;
              }
              goto IL_02A3;
            case 'h':
            case 'k':
            case 'l':
            case 'n':
            case 'q':
            case 's':
              goto IL_031E;
            case 'i':
              if (!(text == ".ico"))
              {
                goto IL_031E;
              }
              goto IL_02A3;
            case 'j':
              if (!(text == ".jpg"))
              {
                goto IL_031E;
              }
              goto IL_02A3;
            case 'm':
              if (!(text == ".mdx"))
              {
                goto IL_031E;
              }
              break;
            case 'o':
              if (!(text == ".odt"))
              {
                goto IL_031E;
              }
              goto IL_02F7;
            case 'p':
              if (text == ".png")
              {
                goto IL_02A3;
              }
              if (!(text == ".pdf"))
              {
                goto IL_031E;
              }
              return new NotchDocumentRenderers.Renderer((string p, Action _) => NotchDocumentRenderers.Pdf(p));
            case 'r':
              if (!(text == ".rtf"))
              {
                goto IL_031E;
              }
              goto IL_02F7;
            case 't':
              if (!(text == ".tif"))
              {
                goto IL_031E;
              }
              goto IL_02A3;
            default:
              goto IL_031E;
            }
          }
          else
          {
            if (!(text == ".bmp"))
            {
              goto IL_031E;
            }
            goto IL_02A3;
          }
          break;
        }
        case 5:
        {
          char c = text[1];
          if (c <= 'j')
          {
            if (c != 'd')
            {
              if (c != 'j')
              {
                goto IL_031E;
              }
              if (!(text == ".jpeg"))
              {
                goto IL_031E;
              }
              goto IL_02A3;
            }
            else
            {
              if (!(text == ".docx") && !(text == ".docm") && !(text == ".dotx"))
              {
                goto IL_031E;
              }
              goto IL_02F7;
            }
          }
          else if (c != 'p')
          {
            switch (c)
            {
            case 't':
              if (!(text == ".tiff"))
              {
                goto IL_031E;
              }
              goto IL_02A3;
            case 'u':
            case 'v':
              goto IL_031E;
            case 'w':
              if (!(text == ".webp"))
              {
                goto IL_031E;
              }
              goto IL_02A3;
            case 'x':
              if (!(text == ".xlsx"))
              {
                goto IL_031E;
              }
              goto IL_02F7;
            default:
              goto IL_031E;
            }
          }
          else
          {
            if (!(text == ".pptx"))
            {
              goto IL_031E;
            }
            goto IL_02F7;
          }
          break;
        }
        case 6:
        case 7:
        case 8:
          goto IL_031E;
        case 9:
          if (!(text == ".markdown"))
          {
            goto IL_031E;
          }
          break;
        default:
          goto IL_031E;
        }
        return new NotchDocumentRenderers.Renderer((string p, Action _) => NotchDocumentRenderers.Async(p, delegate(string text)
        {
          List<ValueTuple<string, string>> list;
          StackPanel stackPanel = Markdown.Render(text, NotchDocumentRenderers.ReaderStyle, out list);
          stackPanel.Margin = new Thickness(22.0, 18.0, 22.0, 28.0);
          stackPanel.MaxWidth = 760.0;
          return NotchDocumentRenderers.Scroll(stackPanel);
        }));
        IL_02A3:
        return new NotchDocumentRenderers.Renderer((string p, Action _) => new Border
        {
          CornerRadius = new CornerRadius(8.0),
          VerticalAlignment = VerticalAlignment.Center,
          Child = new Image
          {
            Source = new BitmapImage(new Uri(p)),
            Stretch = Stretch.Uniform,
            Margin = new Thickness(16.0)
          }
        });
        IL_02F7:
        return new NotchDocumentRenderers.Renderer((string p, Action _) => NotchDocumentRenderers.DocumentText(p));
      }
      IL_031E:
      NotchDocumentRenderers.Renderer renderer;
      if (HeyClicky.Core.Documents.DocumentText.LikelyYieldsText(path) || NotchDocumentRenderers.IsProbablyText(path))
      {
        renderer = new NotchDocumentRenderers.Renderer((string p, Action _) => NotchDocumentRenderers.Async(p, (string text) => NotchDocumentRenderers.Scroll(new TextBlock
        {
          Text = text,
          FontFamily = DS.Fonts.Mono,
          FontSize = 12.0,
          Foreground = Ui.Brush(MessagesPalette.Current.PrimaryText),
          TextWrapping = TextWrapping.Wrap,
          IsTextSelectionEnabled = true,
          IsTextScaleFactorEnabled = false,
          Margin = new Thickness(18.0, 14.0, 18.0, 24.0)
        })));
      }
      else
      {
        renderer = new NotchDocumentRenderers.Renderer((string p, Action _) => NotchDocumentRenderers.NoPreview(p));
      }
      return renderer;
    }

    // Token: 0x06001501 RID: 5377 RVA: 0x005B3EDC File Offset: 0x005B3EDC
    private static FrameworkElement Async(string path, Func<string, UIElement> build)
    {
      Grid host = new Grid
      {
        Children = { NotchDocumentRenderers.Message("Loading document…") }
      };
      DispatcherQueue dq = DispatcherQueue.GetForCurrentThread();
      MessagesPalette look = MessagesPalette.Current;
      Task.Run<string>(delegate
      {
        string text;
        try
        {
          using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Write | FileShare.Delete))
          {
            using (StreamReader streamReader = new StreamReader(fileStream))
            {
              char[] array = new char[200000];
              int num = streamReader.ReadBlock(array, 0, array.Length);
              text = new string(array, 0, num);
            }
          }
        }
        catch (Exception ex)
        {
          Log.Warn("notch-doc", "read: " + ex.Message);
          text = null;
        }
        return text;
      }).ContinueWith<bool>(([Nullable(new byte[] { 1, 0 })] Task<string> t) => dq.TryEnqueue(delegate
      {
        using (MessagesPalette.Use(look))
        {
          host.Children.Clear();
          UIElementCollection children = host.Children;
          string result = t.Result;
          children.Add((result != null) ? build(result) : NotchDocumentRenderers.Message("This document couldn’t be loaded."));
        }
      }), TaskScheduler.Default);
      return host;
    }

    // Token: 0x06001502 RID: 5378 RVA: 0x005B3F64 File Offset: 0x005B3F64
    private static FrameworkElement DocumentText(string path)
    {
      Grid host = new Grid
      {
        Children = { NotchDocumentRenderers.Message("Loading document…") }
      };
      DispatcherQueue dq = DispatcherQueue.GetForCurrentThread();
      MessagesPalette look = MessagesPalette.Current;
      Task.Run<PageContext>(() => HeyClicky.Core.Documents.DocumentText.Extract(path, 60000)).ContinueWith(delegate([Nullable(new byte[] { 1, 0 })] Task<PageContext> t)
      {
        string text = (t.IsCompletedSuccessfully ? t.Result.Text : null);
        dq.TryEnqueue(delegate
        {
          using (MessagesPalette.Use(look))
          {
            host.Children.Clear();
            host.Children.Add(string.IsNullOrWhiteSpace(text) ? NotchDocumentRenderers.Message("This document has no text to show.") : NotchDocumentRenderers.Scroll(NotchDocumentRenderers.Paragraphs(text)));
          }
        });
      }, TaskScheduler.Default);
      return host;
    }

    // Token: 0x06001503 RID: 5379 RVA: 0x005B3FE4 File Offset: 0x005B3FE4
    private static FrameworkElement Pdf(string path)
    {
      NotchDocumentRenderers.<>c__DisplayClass9_0 CS$<>8__locals1 = new NotchDocumentRenderers.<>c__DisplayClass9_0();
      CS$<>8__locals1.path = path;
      CS$<>8__locals1.pages = new StackPanel
      {
        Spacing = 12.0,
        Padding = new Thickness(16.0, 16.0, 16.0, 24.0)
      };
      CS$<>8__locals1.look = MessagesPalette.Current;
      ScrollViewer scrollViewer = NotchDocumentRenderers.Scroll(CS$<>8__locals1.pages);
      CS$<>8__locals1.<Pdf>g__Load|0();
      return scrollViewer;
    }

    // Token: 0x06001504 RID: 5380 RVA: 0x005B4064 File Offset: 0x005B4064
    private static bool IsProbablyText(string path)
    {
      bool flag;
      try
      {
        using (FileStream fileStream = File.OpenRead(path))
        {
          byte[] array = new byte[Math.Min(4096, (int)Math.Min(fileStream.Length, 4096L))];
          int num = fileStream.Read(array, 0, array.Length);
          for (int i = 0; i < num; i++)
          {
            if (array[i] == 0)
            {
              return false;
            }
          }
          flag = num > 0;
        }
      }
      catch
      {
        flag = false;
      }
      return flag;
    }

    // Token: 0x06001505 RID: 5381 RVA: 0x005B40F4 File Offset: 0x005B40F4
    private static ScrollViewer Scroll(UIElement content)
    {
      return new ScrollViewer
      {
        Content = content,
        VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled
      };
    }

    // Token: 0x06001506 RID: 5382 RVA: 0x005B4110 File Offset: 0x005B4110
    private static UIElement Paragraphs(string text)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 10.0,
        Margin = new Thickness(22.0, 18.0, 22.0, 28.0),
        MaxWidth = 760.0
      };
      foreach (string text2 in (from p in text.Replace("\r\n", "\n").Split('\n', StringSplitOptions.None)
        select p.Trim() into p
        where p.Length > 0
        select p).Take<string>(1500))
      {
        TextBlock textBlock = Ui.Text(text2, 13.5, FontWeights.Normal, MessagesPalette.Current.PrimaryText, TextAlignment.Left, 0, true, false);
        textBlock.IsTextSelectionEnabled = true;
        stackPanel.Children.Add(textBlock);
      }
      return stackPanel;
    }

    // Token: 0x06001507 RID: 5383 RVA: 0x005B4244 File Offset: 0x005B4244
    public static FrameworkElement Message(string text)
    {
      return Ui.Text(text, 13.0, FontWeights.Normal, MessagesPalette.Current.SecondaryText, TextAlignment.Center, 0, false, false).Center<TextBlock>();
    }

    // Token: 0x06001508 RID: 5384 RVA: 0x005B4270 File Offset: 0x005B4270
    private static FrameworkElement NoPreview(string path)
    {
      GelButton gelButton = new GelButton("Open", GelAppearance.SecondaryOutline, GelButton.Sizing.Compact, 100.0, "\ue8a7", null, null)
      {
        HorizontalAlignment = HorizontalAlignment.Center
      };
      gelButton.Click += delegate
      {
        ChatBlockViews.OpenPath(path);
      };
      StackPanel stackPanel = new StackPanel();
      stackPanel.Spacing = 12.0;
      stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
      stackPanel.VerticalAlignment = VerticalAlignment.Center;
      Color secondaryText = MessagesPalette.Current.SecondaryText;
      stackPanel.Children.Add(Icons.Glyph("\ue8e5", 30.0, secondaryText));
      stackPanel.Children.Add(Ui.Text("No preview for this " + ChatBlockViews.Kind(path).ToLowerInvariant() + " yet.", 13.0, FontWeights.Normal, secondaryText, TextAlignment.Center, 0, false, false));
      stackPanel.Children.Add(gelButton);
      return stackPanel;
    }

    // Token: 0x04000A22 RID: 2594
    private static readonly Markdown.Options DarkReaderStyle = NotchDocumentRenderers.ReaderStyleIn(MessagesPalette.Dark);

    // Token: 0x04000A23 RID: 2595
    private static readonly Markdown.Options LightReaderStyle = NotchDocumentRenderers.ReaderStyleIn(MessagesPalette.Light);

    // Token: 0x02000720 RID: 1824
    [Nullable(0)]
    private sealed class Renderer : INotchDocumentRenderer
    {
      // Token: 0x06003445 RID: 13381 RVA: 0x0067A0C4 File Offset: 0x0067A0C4
      public Renderer(Func<string, Action, FrameworkElement> render)
      {
      }

      // Token: 0x06003446 RID: 13382 RVA: 0x0067A0D4 File Offset: 0x0067A0D4
      public bool CanRender(string path)
      {
        return true;
      }

      // Token: 0x06003447 RID: 13383 RVA: 0x0067A0D8 File Offset: 0x0067A0D8
      public FrameworkElement Render(string path, Action reload)
      {
        return this.<render>P(path, reload);
      }

      // Token: 0x04001FD4 RID: 8148
      [CompilerGenerated]
      private Func<string, Action, FrameworkElement> <render>P = render;
    }
  }
}
