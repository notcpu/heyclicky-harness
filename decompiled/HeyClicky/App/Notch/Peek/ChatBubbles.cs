using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Shapes;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000175 RID: 373
  [NullableContext(1)]
  [Nullable(0)]
  public static class ChatBubbles
  {
    // Token: 0x06001494 RID: 5268 RVA: 0x005AD060 File Offset: 0x005AD060
    private static Markdown.Options Incoming(MessagesPalette p)
    {
      return new Markdown.Options(13.0, p.IncomingBubbleText, p.SelectionBlue, p.SecondaryText)
      {
        LineSpacing = 2.0
      };
    }

    // Token: 0x06001495 RID: 5269 RVA: 0x005AD094 File Offset: 0x005AD094
    public static FrameworkElement Bubble(string text, bool outgoing, bool tail, [Nullable(2)] MessagesPalette look = null)
    {
      MessagesPalette messagesPalette = look ?? MessagesPalette.Current;
      TextBlock textBlock = Markdown.Inline(text, outgoing ? ChatBubbles.Outgoing : (messagesPalette.IsDark ? ChatBubbles.IncomingDark : ChatBubbles.IncomingLight), 13.0, FontWeights.Normal, null);
      return new Grid
      {
        HorizontalAlignment = (outgoing ? HorizontalAlignment.Right : HorizontalAlignment.Left),
        Children = 
        {
          outgoing ? ChatBlockViews.SentBubblePlate(tail) : ChatBlockViews.BubblePlate(Ui.Brush(messagesPalette.IncomingBubble), null, tail, false, 17.0, 7.0),
          new Border
          {
            Padding = new Thickness(12.0, 7.0, 12.0, 7.0 + (tail ? 7.0 : 0.0)),
            Child = textBlock
          }
        }
      };
    }

    // Token: 0x06001496 RID: 5270 RVA: 0x005AD194 File Offset: 0x005AD194
    public static FrameworkElement TypingBubble()
    {
      MessagesPalette messagesPalette = MessagesPalette.Current;
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 5.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      for (int i = 0; i < 3; i++)
      {
        Ellipse d = new Ellipse
        {
          Width = 8.0,
          Height = 8.0,
          Fill = Ui.Brush(messagesPalette.IsDark ? DS.White(0.72) : DS.Black(0.42))
        };
        double phase = (double)i / 6.0 * 1.1;
        d.Loaded += delegate(object _, RoutedEventArgs _)
        {
          if (Anim.ReduceMotion)
          {
            return;
          }
          d.CenterPoint = new Vector3(4f, 4f, 0f);
          Anim.LoopScalar(d, "Opacity", 1.1, (double x) => (float)(0.38 + 0.62 * ChatBubbles.Wave(x / 1.1)), 24, phase);
          Anim.LoopVector3(d, "Scale", 1.1, delegate(double x)
          {
            float num = (float)(0.86 + 0.22 * ChatBubbles.Wave(x / 1.1));
            return new Vector3(num, num, 1f);
          }, 24, phase);
        };
        stackPanel.Children.Add(d);
      }
      Grid grid = new Grid();
      grid.Height = 30.0;
      grid.HorizontalAlignment = HorizontalAlignment.Left;
      grid.Margin = new Thickness(8.0, 0.0, 0.0, 8.0);
      grid.Children.Add(new Ellipse
      {
        Width = 10.0,
        Height = 10.0,
        Fill = Ui.Brush(messagesPalette.IncomingBubble),
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Bottom,
        Margin = new Thickness(-2.0, 0.0, 0.0, -2.0)
      });
      grid.Children.Add(new Ellipse
      {
        Width = 5.0,
        Height = 5.0,
        Fill = Ui.Brush(messagesPalette.IncomingBubble),
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Bottom,
        Margin = new Thickness(-7.0, 0.0, 0.0, -8.0)
      });
      grid.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(15.0),
        Background = Ui.Brush(messagesPalette.IncomingBubble),
        Padding = new Thickness(13.0, 0.0, 13.0, 0.0),
        Child = stackPanel
      });
      AutomationProperties.SetName(grid, "Working");
      return grid;
    }

    // Token: 0x06001497 RID: 5271 RVA: 0x005AD438 File Offset: 0x005AD438
    private static double Wave(double u)
    {
      double num = (u % 1.0 + 1.0) % 1.0;
      if (num >= 0.5)
      {
        return ChatBubbles.Smooth(2.0 - num * 2.0);
      }
      return ChatBubbles.Smooth(num * 2.0);
    }

    // Token: 0x06001498 RID: 5272 RVA: 0x005AD49C File Offset: 0x005AD49C
    private static double Smooth(double t)
    {
      return t * t * (3.0 - 2.0 * t);
    }

    // Token: 0x06001499 RID: 5273 RVA: 0x005AD4B8 File Offset: 0x005AD4B8
    public static FrameworkElement DateSeparator(DateTimeOffset date)
    {
      DateTime dateTime = date.ToLocalTime().DateTime;
      CultureInfo cultureInfo = CultureInfo.GetCultureInfo("en-US");
      string text = dateTime.ToString("h:mm tt", cultureInfo);
      bool flag = dateTime.Date == DateTime.Today || dateTime.Date == DateTime.Today.AddDays(-1.0);
      string text2 = ((dateTime.Date == DateTime.Today) ? "Today" : ((dateTime.Date == DateTime.Today.AddDays(-1.0)) ? "Yesterday" : dateTime.ToString("ddd, MMM d", cultureInfo)));
      TextBlock textBlock = new TextBlock
      {
        FontSize = 11.0,
        FontFamily = DS.Fonts.Small,
        Foreground = Ui.Brush(MessagesPalette.Current.TertiaryText),
        HorizontalAlignment = HorizontalAlignment.Center,
        IsTextScaleFactorEnabled = false
      };
      textBlock.Inlines.Add(new Run
      {
        Text = text2,
        FontWeight = FontWeights.SemiBold
      });
      textBlock.Inlines.Add(new Run
      {
        Text = (flag ? " " : " at ") + text
      });
      return new Border
      {
        Padding = new Thickness(0.0, 6.0, 0.0, 6.0),
        Margin = new Thickness(0.0, 8.0, 0.0, 0.0),
        Child = textBlock
      };
    }

    // Token: 0x0600149A RID: 5274 RVA: 0x005AD680 File Offset: 0x005AD680
    public static string ShortTime(DateTimeOffset date)
    {
      return date.ToLocalTime().DateTime.ToString("h:mm tt", CultureInfo.GetCultureInfo("en-US"));
    }

    // Token: 0x0600149B RID: 5275 RVA: 0x005AD6B4 File Offset: 0x005AD6B4
    public static string Glyph(string sfSymbol)
    {
      if (sfSymbol != null)
      {
        switch (sfSymbol.Length)
        {
        case 4:
          if (!(sfSymbol == "link"))
          {
            goto IL_050B;
          }
          return "\ue71b";
        case 5:
        {
          char c = sfSymbol[0];
          if (c != 'b')
          {
            if (c != 'h')
            {
              if (c != 'p')
              {
                goto IL_050B;
              }
              if (!(sfSymbol == "photo"))
              {
                goto IL_050B;
              }
              return "\ue91b";
            }
            else
            {
              if (!(sfSymbol == "house"))
              {
                goto IL_050B;
              }
              return "\ue80f";
            }
          }
          else
          {
            if (!(sfSymbol == "brain"))
            {
              goto IL_050B;
            }
            return "\ue82f";
          }
          break;
        }
        case 6:
        {
          char c = sfSymbol[0];
          if (c <= 'f')
          {
            if (c != 'e')
            {
              if (c != 'f')
              {
                goto IL_050B;
              }
              if (!(sfSymbol == "folder"))
              {
                goto IL_050B;
              }
              return "\ue8b7";
            }
            else
            {
              if (!(sfSymbol == "eraser"))
              {
                goto IL_050B;
              }
              return "\ue75c";
            }
          }
          else if (c != 'r')
          {
            if (c != 's')
            {
              goto IL_050B;
            }
            if (!(sfSymbol == "safari"))
            {
              goto IL_050B;
            }
            return "\ue774";
          }
          else
          {
            if (!(sfSymbol == "repeat"))
            {
              goto IL_050B;
            }
            return "\ue8ee";
          }
          break;
        }
        case 7:
          if (!(sfSymbol == "forward"))
          {
            goto IL_050B;
          }
          return "\ue893";
        case 8:
        {
          char c = sfSymbol[0];
          if (c <= 'd')
          {
            if (c != 'c')
            {
              if (c != 'd')
              {
                goto IL_050B;
              }
              if (!(sfSymbol == "doc.text"))
              {
                goto IL_050B;
              }
            }
            else
            {
              if (!(sfSymbol == "calendar"))
              {
                goto IL_050B;
              }
              goto IL_04D3;
            }
          }
          else if (c != 'k')
          {
            if (c != 't')
            {
              goto IL_050B;
            }
            if (!(sfSymbol == "terminal"))
            {
              goto IL_050B;
            }
            return "\ue756";
          }
          else
          {
            if (!(sfSymbol == "keyboard"))
            {
              goto IL_050B;
            }
            return "\ue765";
          }
          break;
        }
        case 9:
        {
          char c = sfSymbol[0];
          if (c != 'h')
          {
            if (c != 't')
            {
              goto IL_050B;
            }
            if (!(sfSymbol == "tray.full"))
            {
              goto IL_050B;
            }
            return "\ue8f1";
          }
          else
          {
            if (!(sfSymbol == "hourglass"))
            {
              goto IL_050B;
            }
            return "\ue823";
          }
          break;
        }
        case 10:
          if (!(sfSymbol == "doc.on.doc"))
          {
            goto IL_050B;
          }
          return "\ue8c8";
        case 11:
          if (!(sfSymbol == "stop.circle"))
          {
            goto IL_050B;
          }
          return "\ue71a";
        case 12:
        case 14:
        case 18:
        case 20:
        case 23:
        case 25:
        case 26:
        case 27:
        case 28:
          goto IL_050B;
        case 13:
          if (!(sfSymbol == "bookmark.fill"))
          {
            goto IL_050B;
          }
          return "\ue8a4";
        case 15:
        {
          char c = sfSymbol[0];
          if (c != 'm')
          {
            if (c != 'p')
            {
              goto IL_050B;
            }
            if (!(sfSymbol == "paperplane.fill"))
            {
              goto IL_050B;
            }
            return "\ue724";
          }
          else
          {
            if (!(sfSymbol == "magnifyingglass"))
            {
              goto IL_050B;
            }
            return "\ue721";
          }
          break;
        }
        case 16:
        {
          char c = sfSymbol[0];
          if (c != 'c')
          {
            if (c != 'd')
            {
              if (c != 'p')
              {
                goto IL_050B;
              }
              if (!(sfSymbol == "play.circle.fill"))
              {
                goto IL_050B;
              }
              return "\ue768";
            }
            else
            {
              if (!(sfSymbol == "doc.on.clipboard"))
              {
                goto IL_050B;
              }
              return "\ue77f";
            }
          }
          else
          {
            if (!(sfSymbol == "checkmark.bubble"))
            {
              goto IL_050B;
            }
            return "\ue8bd";
          }
          break;
        }
        case 17:
          if (!(sfSymbol == "camera.viewfinder"))
          {
            goto IL_050B;
          }
          return "\ue722";
        case 19:
          if (!(sfSymbol == "calendar.badge.plus"))
          {
            goto IL_050B;
          }
          goto IL_04D3;
        case 21:
        {
          char c = sfSymbol[0];
          if (c != 'p')
          {
            if (c != 's')
            {
              goto IL_050B;
            }
            if (!(sfSymbol == "square.and.arrow.down"))
            {
              goto IL_050B;
            }
            return "\ue896";
          }
          else
          {
            if (!(sfSymbol == "person.text.rectangle"))
            {
              goto IL_050B;
            }
            return "\ue77b";
          }
          break;
        }
        case 22:
          if (!(sfSymbol == "clock.arrow.circlepath"))
          {
            goto IL_050B;
          }
          return "\ue81c";
        case 24:
          if (!(sfSymbol == "doc.text.magnifyingglass"))
          {
            goto IL_050B;
          }
          break;
        case 29:
          if (!(sfSymbol == "arrowshape.turn.up.right.fill"))
          {
            goto IL_050B;
          }
          return "\ue72a";
        default:
          goto IL_050B;
        }
        return "\ue8a5";
        IL_04D3:
        return "\ue787";
      }
      IL_050B:
      return "\ue90f";
    }

    // Token: 0x040009ED RID: 2541
    private static readonly Markdown.Options IncomingDark = ChatBubbles.Incoming(MessagesPalette.Dark);

    // Token: 0x040009EE RID: 2542
    private static readonly Markdown.Options IncomingLight = ChatBubbles.Incoming(MessagesPalette.Light);

    // Token: 0x040009EF RID: 2543
    private static readonly Markdown.Options Outgoing = new Markdown.Options(13.0, DS.Black(0.86), DS.Black(0.86), DS.Black(0.6))
    {
      LineSpacing = 2.0
    };
  }
}
