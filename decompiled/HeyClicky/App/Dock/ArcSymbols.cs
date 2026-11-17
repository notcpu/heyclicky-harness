using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x0200031A RID: 794
  public static class ArcSymbols
  {
    // Token: 0x06002268 RID: 8808 RVA: 0x00622CB4 File Offset: 0x00622CB4
    [NullableContext(1)]
    public static FrameworkElement Make(string name, double size, Color color, bool bold = true)
    {
      string text;
      if (name != null)
      {
        int num = name.Length;
        switch (num)
        {
        case 4:
          if (!(name == "link"))
          {
            goto IL_054E;
          }
          text = "\ue71b";
          goto IL_0550;
        case 5:
        {
          char c = name[0];
          if (c != 'c')
          {
            if (c != 'g')
            {
              goto IL_054E;
            }
            if (!(name == "globe"))
            {
              goto IL_054E;
            }
            text = "\ue774";
            goto IL_0550;
          }
          else
          {
            if (!(name == "clock"))
            {
              goto IL_054E;
            }
            text = "\ue823";
            goto IL_0550;
          }
          break;
        }
        case 6:
        case 7:
        case 16:
        case 19:
        case 20:
        case 22:
        case 23:
        case 24:
        case 25:
        case 26:
          goto IL_054E;
        case 8:
        {
          char c = name[0];
          if (c <= 'k')
          {
            if (c != 'd')
            {
              if (c != 'k')
              {
                goto IL_054E;
              }
              if (!(name == "keyboard"))
              {
                goto IL_054E;
              }
              text = "\ue765";
              goto IL_0550;
            }
            else
            {
              if (name == "doc.text")
              {
                text = "\ue8a5";
                goto IL_0550;
              }
              if (!(name == "doc.fill"))
              {
                goto IL_054E;
              }
            }
          }
          else if (c != 'p')
          {
            if (c != 'w')
            {
              goto IL_054E;
            }
            if (!(name == "waveform"))
            {
              goto IL_054E;
            }
            text = "\ue8d6";
            goto IL_0550;
          }
          else
          {
            if (!(name == "pin.fill"))
            {
              goto IL_054E;
            }
            text = "\ue842";
            goto IL_0550;
          }
          break;
        }
        case 9:
        {
          char c = name[0];
          switch (c)
          {
          case 'b':
            if (!(name == "bolt.fill"))
            {
              goto IL_054E;
            }
            text = "\ue945";
            goto IL_0550;
          case 'c':
            if (!(name == "checkmark"))
            {
              goto IL_054E;
            }
            text = "\ue73e";
            goto IL_0550;
          case 'd':
          case 'e':
            goto IL_054E;
          case 'f':
            if (!(name == "film.fill"))
            {
              goto IL_054E;
            }
            text = "\ue8b2";
            goto IL_0550;
          default:
            if (c != 'p')
            {
              if (c != 's')
              {
                goto IL_054E;
              }
              if (!(name == "stop.fill"))
              {
                goto IL_054E;
              }
              text = "\ue71a";
              goto IL_0550;
            }
            else
            {
              if (!(name == "play.fill"))
              {
                goto IL_054E;
              }
              text = "\uf5b0";
              goto IL_0550;
            }
            break;
          }
          break;
        }
        case 10:
        {
          char c = name[5];
          if (c != '.')
          {
            if (c != 'i')
            {
              if (c != 'n')
              {
                goto IL_054E;
              }
              if (!(name == "doc.on.doc"))
              {
                goto IL_054E;
              }
              text = "\ue8c8";
              goto IL_0550;
            }
            else
            {
              if (!(name == "doc.zipper"))
              {
                goto IL_054E;
              }
              text = "\uf012";
              goto IL_0550;
            }
          }
          else
          {
            if (!(name == "photo.fill"))
            {
              goto IL_054E;
            }
            text = "\ueb9f";
            goto IL_0550;
          }
          break;
        }
        case 11:
        {
          char c = name[0];
          if (c != 'c')
          {
            if (c != 'f')
            {
              goto IL_054E;
            }
            if (!(name == "folder.fill"))
            {
              goto IL_054E;
            }
            text = "\ue8b7";
            goto IL_0550;
          }
          else
          {
            if (!(name == "curlybraces"))
            {
              goto IL_054E;
            }
            goto IL_0526;
          }
          break;
        }
        case 12:
          if (!(name == "chevron.left"))
          {
            goto IL_054E;
          }
          text = "\ue76b";
          goto IL_0550;
        case 13:
        {
          char c = name[0];
          if (c != 'c')
          {
            if (c != 'd')
            {
              goto IL_054E;
            }
            if (!(name == "doc.text.fill"))
            {
              goto IL_054E;
            }
          }
          else
          {
            if (!(name == "chevron.right"))
            {
              goto IL_054E;
            }
            text = "\ue76c";
            goto IL_0550;
          }
          break;
        }
        case 14:
          if (!(name == "text.alignleft"))
          {
            goto IL_054E;
          }
          text = "\ue8e4";
          goto IL_0550;
        case 15:
        {
          char c = name[0];
          if (c != 'a')
          {
            if (c != 'd')
            {
              if (c != 't')
              {
                goto IL_054E;
              }
              if (!(name == "tablecells.fill"))
              {
                goto IL_054E;
              }
              text = "\ue80a";
              goto IL_0550;
            }
            else
            {
              if (!(name == "doc.on.doc.fill"))
              {
                goto IL_054E;
              }
              text = "\ue8c8";
              goto IL_0550;
            }
          }
          else
          {
            if (!(name == "arrow.clockwise"))
            {
              goto IL_054E;
            }
            text = "\ue72c";
            goto IL_0550;
          }
          break;
        }
        case 17:
          if (!(name == "doc.richtext.fill"))
          {
            goto IL_054E;
          }
          break;
        case 18:
          if (!(name == "doc.plaintext.fill"))
          {
            goto IL_054E;
          }
          break;
        case 21:
        {
          char c = name[0];
          if (c != 'a')
          {
            if (c != 'c')
            {
              goto IL_054E;
            }
            if (!(name == "checkmark.circle.fill"))
            {
              goto IL_054E;
            }
            text = "\uec61";
            goto IL_0550;
          }
          else
          {
            if (!(name == "arrow.up.right.square"))
            {
              goto IL_054E;
            }
            text = "\ue8a7";
            goto IL_0550;
          }
          break;
        }
        case 27:
          if (!(name == "rectangle.on.rectangle.fill"))
          {
            goto IL_054E;
          }
          text = "\ue8a1";
          goto IL_0550;
        default:
          if (num != 39)
          {
            goto IL_054E;
          }
          if (!(name == "chevron.left.forwardslash.chevron.right"))
          {
            goto IL_054E;
          }
          goto IL_0526;
        }
        text = "\ue8a5";
        goto IL_0550;
        IL_0526:
        text = "\ue943";
        goto IL_0550;
      }
      IL_054E:
      text = null;
      IL_0550:
      string text2 = text;
      if (text2 != null)
      {
        TextBlock textBlock = Icons.Glyph(text2, size, color);
        if (bold)
        {
          textBlock.FontWeight = FontWeights.Bold;
        }
        return textBlock;
      }
      if (name != null)
      {
        int num = name.Length;
        if (num <= 16)
        {
          if (num != 8)
          {
            if (num != 16)
            {
              goto IL_0674;
            }
            char c = name[0];
            if (c != 'h')
            {
              if (c != 's')
              {
                goto IL_0674;
              }
              if (!(name == "stop.circle.fill"))
              {
                goto IL_0674;
              }
              text = "F0 M12 2a10 10 0 1 1 0 20a10 10 0 1 1 0-20Z M8.6 8.6h6.8v6.8H8.6Z";
              goto IL_0676;
            }
            else if (!(name == "hand.raised.fill"))
            {
              goto IL_0674;
            }
          }
          else
          {
            char c = name[0];
            if (c != 'i')
            {
              if (c != 's')
              {
                goto IL_0674;
              }
              if (!(name == "sparkles"))
              {
                goto IL_0674;
              }
              text = "M10,3 C10.6,7.6 12.4,9.4 17,10 C12.4,10.6 10.6,12.4 10,17 C9.4,12.4 7.6,10.6 3,10 C7.6,9.4 9.4,7.6 10,3 Z M18,13 C18.3,15.2 18.8,15.7 21,16 C18.8,16.3 18.3,16.8 18,19 C17.7,16.8 17.2,16.3 15,16 C17.2,15.7 17.7,15.2 18,13 Z M17,2 C17.2,3.4 17.6,3.8 19,4 C17.6,4.2 17.2,4.6 17,6 C16.8,4.6 16.4,4.2 15,4 C16.4,3.8 16.8,3.4 17,2 Z";
              goto IL_0676;
            }
            else
            {
              if (!(name == "infinity"))
              {
                goto IL_0674;
              }
              text = null;
              goto IL_0676;
            }
          }
        }
        else if (num != 19)
        {
          if (num != 23)
          {
            if (num != 29)
            {
              goto IL_0674;
            }
            if (!(name == "exclamationmark.triangle.fill"))
            {
              goto IL_0674;
            }
            text = "F0 M12 2.6c.6 0 1.1.3 1.4.8l8.9 15.5c.6 1.1-.2 2.5-1.4 2.5H3.1c-1.2 0-2-1.4-1.4-2.5L10.6 3.4c.3-.5.8-.8 1.4-.8Z M10.9 8.3h2.2l-.3 6.4h-1.6Z M12 16.1a1.3 1.3 0 1 1 0 2.6a1.3 1.3 0 1 1 0-2.6Z";
            goto IL_0676;
          }
          else if (!(name == "hand.point.up.left.fill"))
          {
            goto IL_0674;
          }
        }
        else
        {
          if (!(name == "cursorarrow.click.2"))
          {
            goto IL_0674;
          }
          text = "M8.2 6.4l10.1 6.1-4.4 1.1 2.6 5.2-2 1-2.6-5.2-3.5 3.1Z";
          goto IL_0676;
        }
        text = "M8.4 4.2a1.45 1.45 0 0 1 2.9 0V10.4h.6V2.9a1.45 1.45 0 0 1 2.9 0V10.4h.6V4.4a1.45 1.45 0 0 1 2.9 0V14c0 4.3-2.9 7.6-7 7.6c-2.6 0-4.4-1.2-5.8-3.4L2.9 13.5a1.45 1.45 0 0 1 2.4-1.6L8.4 15Z";
        goto IL_0676;
      }
      IL_0674:
      text = null;
      IL_0676:
      string text3 = text;
      if (text3 != null)
      {
        return Icons.Vector(size * 1.15, color, text3, null, 2.0);
      }
      if (name == "arrow.up.right")
      {
        return Icons.StrokeIcon("M5,19 L19,5 M8,5 L19,5 L19,16", size, color, bold ? 1.9 : 1.5);
      }
      if (name == "infinity")
      {
        return Ui.Text("∞", size * 1.25, FontWeights.Bold, color, TextAlignment.Left, 0, false, false);
      }
      if (name == "hand.raised.fill")
      {
        return Icons.Glyph("\ue768", size, color);
      }
      return Icons.Glyph("\uf4a5", size, color);
    }
  }
}
