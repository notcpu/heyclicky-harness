using System;
using System.IO;
using System.Runtime.CompilerServices;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001D0 RID: 464
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyPaperFileKinds
  {
    // Token: 0x060017D3 RID: 6099 RVA: 0x005C6634 File Offset: 0x005C6634
    public static Color[] FootColors(this ClickyPaperFileKind kind)
    {
      Color[] array;
      switch (kind)
      {
      case ClickyPaperFileKind.Slides:
        array = new Color[]
        {
          PaperColor.Hex("#5AA2FA"),
          PaperColor.Hex("#2368DA")
        };
        break;
      case ClickyPaperFileKind.Pdf:
        array = new Color[]
        {
          PaperColor.Hex("#FF6B5E"),
          PaperColor.Hex("#E0392B")
        };
        break;
      case ClickyPaperFileKind.Sheet:
        array = new Color[]
        {
          PaperColor.Hex("#43C46B"),
          PaperColor.Hex("#23994A")
        };
        break;
      case ClickyPaperFileKind.Image:
        array = new Color[]
        {
          PaperColor.Hex("#B48BF5"),
          PaperColor.Hex("#7E57D9")
        };
        break;
      case ClickyPaperFileKind.Doc:
        array = new Color[]
        {
          PaperColor.Hex("#7FA6E8"),
          PaperColor.Hex("#4A73C9")
        };
        break;
      case ClickyPaperFileKind.Page:
        array = new Color[]
        {
          PaperColor.Hex("#F5A524"),
          PaperColor.Hex("#D9850B")
        };
        break;
      default:
        array = new Color[]
        {
          PaperColor.Hex("#B6BBC6"),
          PaperColor.Hex("#8F95A3")
        };
        break;
      }
      return array;
    }

    // Token: 0x060017D4 RID: 6100 RVA: 0x005C679C File Offset: 0x005C679C
    private static string Extension(string name)
    {
      string extension = Path.GetExtension(name);
      if (extension.Length <= 1)
      {
        return "";
      }
      string text = extension;
      return text.Substring(1, text.Length - 1).ToLowerInvariant();
    }

    // Token: 0x060017D5 RID: 6101 RVA: 0x005C67D8 File Offset: 0x005C67D8
    public static string ShortLabel(string fileName)
    {
      string text = ClickyPaperFileKinds.Extension(fileName);
      if (text != null)
      {
        switch (text.Length)
        {
        case 0:
          return "Folder";
        case 1:
        case 6:
          goto IL_031E;
        case 2:
          if (!(text == "md"))
          {
            goto IL_031E;
          }
          goto IL_0306;
        case 3:
        {
          char c = text[0];
          switch (c)
          {
          case 'c':
            if (!(text == "csv"))
            {
              goto IL_031E;
            }
            break;
          case 'd':
            if (!(text == "doc"))
            {
              goto IL_031E;
            }
            goto IL_030E;
          case 'e':
          case 'f':
          case 'i':
          case 'l':
          case 'm':
          case 'n':
          case 'o':
          case 'q':
            goto IL_031E;
          case 'g':
            if (!(text == "gif"))
            {
              goto IL_031E;
            }
            goto IL_02F6;
          case 'h':
            if (!(text == "htm"))
            {
              goto IL_031E;
            }
            goto IL_02FE;
          case 'j':
            if (!(text == "jpg"))
            {
              goto IL_031E;
            }
            goto IL_02F6;
          case 'k':
            if (!(text == "key"))
            {
              goto IL_031E;
            }
            return "Keynote";
          case 'p':
            if (text == "ppt")
            {
              goto IL_02CE;
            }
            if (text == "pdf")
            {
              return "PDF";
            }
            if (!(text == "png"))
            {
              goto IL_031E;
            }
            goto IL_02F6;
          case 'r':
            if (!(text == "rtf"))
            {
              goto IL_031E;
            }
            goto IL_0306;
          case 's':
            if (!(text == "svg"))
            {
              goto IL_031E;
            }
            goto IL_02F6;
          case 't':
            if (!(text == "tsv"))
            {
              if (!(text == "txt"))
              {
                goto IL_031E;
              }
              goto IL_0306;
            }
            break;
          default:
            if (c != 'x')
            {
              goto IL_031E;
            }
            if (!(text == "xls"))
            {
              goto IL_031E;
            }
            goto IL_02E6;
          }
          return "Sheet";
        }
        case 4:
        {
          char c = text[2];
          if (c <= 'i')
          {
            switch (c)
            {
            case 'b':
              if (!(text == "webp"))
              {
                goto IL_031E;
              }
              goto IL_02F6;
            case 'c':
              if (!(text == "docx"))
              {
                goto IL_031E;
              }
              goto IL_030E;
            case 'd':
              goto IL_031E;
            case 'e':
              if (!(text == "jpeg"))
              {
                goto IL_031E;
              }
              goto IL_02F6;
            default:
              if (c != 'i')
              {
                goto IL_031E;
              }
              if (!(text == "heic"))
              {
                goto IL_031E;
              }
              goto IL_02F6;
            }
          }
          else if (c != 'm')
          {
            if (c != 's')
            {
              if (c != 't')
              {
                goto IL_031E;
              }
              if (!(text == "pptx"))
              {
                goto IL_031E;
              }
            }
            else
            {
              if (!(text == "xlsx"))
              {
                goto IL_031E;
              }
              goto IL_02E6;
            }
          }
          else
          {
            if (!(text == "html"))
            {
              goto IL_031E;
            }
            goto IL_02FE;
          }
          break;
        }
        case 5:
          if (!(text == "pages"))
          {
            goto IL_031E;
          }
          goto IL_030E;
        case 7:
          if (!(text == "numbers"))
          {
            goto IL_031E;
          }
          return "Numbers";
        default:
          goto IL_031E;
        }
        IL_02CE:
        return "Slides";
        IL_02E6:
        return "Excel";
        IL_02F6:
        return "Image";
        IL_02FE:
        return "Page";
        IL_0306:
        return "Note";
        IL_030E:
        return "Doc";
      }
      IL_031E:
      return text.ToUpperInvariant();
    }

    // Token: 0x060017D6 RID: 6102 RVA: 0x005C6B0C File Offset: 0x005C6B0C
    public static ClickyPaperFileKind ForFileName(string name)
    {
      string text = ClickyPaperFileKinds.Extension(name);
      if (text != null)
      {
        switch (text.Length)
        {
        case 2:
          if (!(text == "md"))
          {
            goto IL_02C3;
          }
          goto IL_02BF;
        case 3:
        {
          char c = text[0];
          switch (c)
          {
          case 'c':
            if (!(text == "csv"))
            {
              goto IL_02C3;
            }
            goto IL_02B3;
          case 'd':
            if (!(text == "doc"))
            {
              goto IL_02C3;
            }
            goto IL_02BF;
          case 'e':
          case 'f':
          case 'i':
          case 'l':
          case 'm':
          case 'n':
          case 'o':
          case 'q':
            goto IL_02C3;
          case 'g':
            if (!(text == "gif"))
            {
              goto IL_02C3;
            }
            goto IL_02B7;
          case 'h':
            if (!(text == "htm"))
            {
              goto IL_02C3;
            }
            goto IL_02BB;
          case 'j':
            if (!(text == "jpg"))
            {
              goto IL_02C3;
            }
            goto IL_02B7;
          case 'k':
            if (!(text == "key"))
            {
              goto IL_02C3;
            }
            break;
          case 'p':
            if (!(text == "ppt"))
            {
              if (text == "pdf")
              {
                return ClickyPaperFileKind.Pdf;
              }
              if (!(text == "png"))
              {
                goto IL_02C3;
              }
              goto IL_02B7;
            }
            break;
          case 'r':
            if (!(text == "rtf"))
            {
              goto IL_02C3;
            }
            goto IL_02BF;
          case 's':
            if (!(text == "svg"))
            {
              goto IL_02C3;
            }
            goto IL_02B7;
          case 't':
            if (text == "tsv")
            {
              goto IL_02B3;
            }
            if (!(text == "txt"))
            {
              goto IL_02C3;
            }
            goto IL_02BF;
          default:
            if (c != 'x')
            {
              goto IL_02C3;
            }
            if (!(text == "xls"))
            {
              goto IL_02C3;
            }
            goto IL_02B3;
          }
          break;
        }
        case 4:
        {
          char c = text[2];
          if (c <= 'i')
          {
            switch (c)
            {
            case 'b':
              if (!(text == "webp"))
              {
                goto IL_02C3;
              }
              goto IL_02B7;
            case 'c':
              if (!(text == "docx"))
              {
                goto IL_02C3;
              }
              goto IL_02BF;
            case 'd':
              goto IL_02C3;
            case 'e':
              if (!(text == "jpeg"))
              {
                goto IL_02C3;
              }
              goto IL_02B7;
            default:
              if (c != 'i')
              {
                goto IL_02C3;
              }
              if (!(text == "heic"))
              {
                goto IL_02C3;
              }
              goto IL_02B7;
            }
          }
          else if (c != 'm')
          {
            if (c != 's')
            {
              if (c != 't')
              {
                goto IL_02C3;
              }
              if (!(text == "pptx"))
              {
                goto IL_02C3;
              }
            }
            else
            {
              if (!(text == "xlsx"))
              {
                goto IL_02C3;
              }
              goto IL_02B3;
            }
          }
          else
          {
            if (!(text == "html"))
            {
              goto IL_02C3;
            }
            goto IL_02BB;
          }
          break;
        }
        case 5:
          if (!(text == "pages"))
          {
            goto IL_02C3;
          }
          goto IL_02BF;
        case 6:
          goto IL_02C3;
        case 7:
          if (!(text == "numbers"))
          {
            goto IL_02C3;
          }
          goto IL_02B3;
        default:
          goto IL_02C3;
        }
        return ClickyPaperFileKind.Slides;
        IL_02B3:
        return ClickyPaperFileKind.Sheet;
        IL_02B7:
        return ClickyPaperFileKind.Image;
        IL_02BB:
        return ClickyPaperFileKind.Page;
        IL_02BF:
        return ClickyPaperFileKind.Doc;
      }
      IL_02C3:
      return ClickyPaperFileKind.Other;
    }
  }
}
