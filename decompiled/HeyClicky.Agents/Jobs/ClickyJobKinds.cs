using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x0200003C RID: 60
  public static class ClickyJobKinds
  {
    // Token: 0x060005B3 RID: 1459 RVA: 0x000F1BF0 File Offset: 0x000F1BF0
    [NullableContext(1)]
    public static ClickyJobKind Infer(IReadOnlyList<string> artifactEntries, string title, string prompt)
    {
      foreach (string text in artifactEntries)
      {
        string text2 = text.ToLowerInvariant();
        if (text2.StartsWith("http://", StringComparison.Ordinal) || text2.StartsWith("https://", StringComparison.Ordinal))
        {
          return ClickyJobKind.Page;
        }
        string text3 = Path.GetExtension(text2).TrimStart('.');
        if (text3 != null)
        {
          switch (text3.Length)
          {
          case 2:
          {
            char c = text3[0];
            if (c != 'g')
            {
              switch (c)
              {
              case 'j':
                if (!(text3 == "js"))
                {
                  continue;
                }
                goto IL_0530;
              case 'k':
              case 'l':
              case 'n':
              case 'o':
              case 'q':
                continue;
              case 'm':
                if (!(text3 == "md"))
                {
                  continue;
                }
                break;
              case 'p':
                if (!(text3 == "py"))
                {
                  continue;
                }
                goto IL_0530;
              case 'r':
                if (!(text3 == "rb") && !(text3 == "rs"))
                {
                  continue;
                }
                goto IL_0530;
              case 's':
                if (!(text3 == "sh"))
                {
                  continue;
                }
                goto IL_0530;
              case 't':
                if (!(text3 == "ts"))
                {
                  continue;
                }
                goto IL_0530;
              default:
                continue;
              }
            }
            else
            {
              if (!(text3 == "go"))
              {
                continue;
              }
              goto IL_0530;
            }
            break;
          }
          case 3:
            switch (text3[0])
            {
            case 'a':
              if (text3 == "avi")
              {
                goto IL_0522;
              }
              if (!(text3 == "app"))
              {
                continue;
              }
              goto IL_0530;
            case 'b':
            case 'e':
            case 'f':
            case 'i':
            case 'l':
            case 'n':
            case 'o':
            case 'q':
            case 'u':
            case 'v':
            case 'w':
              continue;
            case 'c':
              if (!(text3 == "csv"))
              {
                continue;
              }
              goto IL_050D;
            case 'd':
              if (!(text3 == "doc"))
              {
                continue;
              }
              break;
            case 'g':
              if (!(text3 == "gif"))
              {
                continue;
              }
              goto IL_051B;
            case 'h':
              if (!(text3 == "htm"))
              {
                continue;
              }
              goto IL_0529;
            case 'j':
              if (!(text3 == "jpg"))
              {
                continue;
              }
              goto IL_051B;
            case 'k':
              if (!(text3 == "key"))
              {
                continue;
              }
              goto IL_0514;
            case 'm':
              if (!(text3 == "mp4") && !(text3 == "mov") && !(text3 == "m4v") && !(text3 == "mkv"))
              {
                continue;
              }
              goto IL_0522;
            case 'p':
              if (text3 == "pdf")
              {
                return ClickyJobKind.Pdf;
              }
              if (text3 == "ppt")
              {
                goto IL_0514;
              }
              if (!(text3 == "png"))
              {
                continue;
              }
              goto IL_051B;
            case 'r':
              if (!(text3 == "rtf"))
              {
                continue;
              }
              break;
            case 's':
              if (!(text3 == "svg"))
              {
                continue;
              }
              goto IL_051B;
            case 't':
              if (!(text3 == "txt"))
              {
                if (text3 == "tsv")
                {
                  goto IL_050D;
                }
                if (!(text3 == "tsx"))
                {
                  continue;
                }
                goto IL_0530;
              }
              break;
            case 'x':
              if (!(text3 == "xls"))
              {
                continue;
              }
              goto IL_050D;
            default:
              continue;
            }
            break;
          case 4:
          {
            char c = text3[2];
            if (c <= 'm')
            {
              switch (c)
              {
              case 'b':
                if (text3 == "webp")
                {
                  goto IL_051B;
                }
                if (!(text3 == "webm"))
                {
                  continue;
                }
                goto IL_0522;
              case 'c':
                if (!(text3 == "docx"))
                {
                  continue;
                }
                break;
              case 'd':
                continue;
              case 'e':
                if (!(text3 == "jpeg"))
                {
                  continue;
                }
                goto IL_051B;
              default:
                if (c != 'i')
                {
                  if (c != 'm')
                  {
                    continue;
                  }
                  if (!(text3 == "html"))
                  {
                    continue;
                  }
                  goto IL_0529;
                }
                else
                {
                  if (!(text3 == "heic"))
                  {
                    continue;
                  }
                  goto IL_051B;
                }
                break;
              }
            }
            else if (c != 'o')
            {
              if (c != 's')
              {
                if (c != 't')
                {
                  continue;
                }
                if (!(text3 == "pptx"))
                {
                  continue;
                }
                goto IL_0514;
              }
              else
              {
                if (!(text3 == "xlsx"))
                {
                  continue;
                }
                goto IL_050D;
              }
            }
            else
            {
              if (!(text3 == "json"))
              {
                continue;
              }
              goto IL_0530;
            }
            break;
          }
          case 5:
          {
            char c = text3[0];
            if (c != 'p')
            {
              if (c != 's')
              {
                continue;
              }
              if (!(text3 == "swift"))
              {
                continue;
              }
              goto IL_0530;
            }
            else if (!(text3 == "pages"))
            {
              continue;
            }
            break;
          }
          case 6:
            continue;
          case 7:
            if (!(text3 == "numbers"))
            {
              continue;
            }
            goto IL_050D;
          default:
            continue;
          }
          return ClickyJobKind.Document;
          IL_050D:
          return ClickyJobKind.Sheet;
          IL_0514:
          return ClickyJobKind.Slides;
          IL_051B:
          return ClickyJobKind.Image;
          IL_0522:
          return ClickyJobKind.Video;
          IL_0529:
          return ClickyJobKind.Page;
          IL_0530:
          return ClickyJobKind.Code;
        }
      }
      string text4 = (title + " " + ((prompt.Length > 200) ? prompt.Substring(0, 200) : prompt)).ToLowerInvariant();
      foreach (ValueTuple<string[], ClickyJobKind> valueTuple in new ValueTuple<string[], ClickyJobKind>[]
      {
        new ValueTuple<string[], ClickyJobKind>(new string[] { "pdf" }, ClickyJobKind.Pdf),
        new ValueTuple<string[], ClickyJobKind>(new string[] { "spreadsheet", "sheet", "csv", "table" }, ClickyJobKind.Sheet),
        new ValueTuple<string[], ClickyJobKind>(new string[] { "slide", "deck", "presentation" }, ClickyJobKind.Slides),
        new ValueTuple<string[], ClickyJobKind>(new string[] { "website", "landing page", "web page", "site" }, ClickyJobKind.Page),
        new ValueTuple<string[], ClickyJobKind>(new string[] { "video", "clip", "reel", "movie" }, ClickyJobKind.Video),
        new ValueTuple<string[], ClickyJobKind>(new string[] { "image", "logo", "photo", "picture", "poster" }, ClickyJobKind.Image),
        new ValueTuple<string[], ClickyJobKind>(new string[] { "code", "script", "app", "bug", "repo" }, ClickyJobKind.Code),
        new ValueTuple<string[], ClickyJobKind>(new string[] { "message", "email", "text ", "whatsapp", "reply", "send" }, ClickyJobKind.Message),
        new ValueTuple<string[], ClickyJobKind>(new string[] { "doc", "essay", "report", "write", "draft", "notes" }, ClickyJobKind.Document)
      })
      {
        string[] item = valueTuple.Item1;
        ClickyJobKind item2 = valueTuple.Item2;
        if (item.Any<string>(new Func<string, bool>(text4.Contains)))
        {
          return item2;
        }
      }
      return ClickyJobKind.Research;
    }
  }
}
