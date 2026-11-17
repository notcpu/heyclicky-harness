using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000147 RID: 327
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkDocumentKinds
  {
    // Token: 0x0600149B RID: 5275 RVA: 0x003ADC18 File Offset: 0x003ADC18
    public static string RawValue(this ClickyInkDocument.Kind kind)
    {
      return kind.ToString().ToLowerInvariant();
    }

    // Token: 0x0600149C RID: 5276 RVA: 0x003ADC2C File Offset: 0x003ADC2C
    public static string RawValue(this ClickyInkDocument.Mark mark)
    {
      return mark.ToString().ToLowerInvariant();
    }

    // Token: 0x0600149D RID: 5277 RVA: 0x003ADC40 File Offset: 0x003ADC40
    public static ClickyInkDocument.Mark? MarkFromWord(string word)
    {
      string text = word.ToLowerInvariant();
      if (text != null)
      {
        switch (text.Length)
        {
        case 2:
          if (!(text == "no"))
          {
            goto IL_0190;
          }
          break;
        case 3:
        {
          char c = text[1];
          if (c != 'e')
          {
            if (c != 'o')
            {
              if (c != 'u')
              {
                goto IL_0190;
              }
              if (!(text == "out"))
              {
                goto IL_0190;
              }
            }
            else
            {
              if (!(text == "now"))
              {
                goto IL_0190;
              }
              return new ClickyInkDocument.Mark?(ClickyInkDocument.Mark.Now);
            }
          }
          else
          {
            if (!(text == "new"))
            {
              goto IL_0190;
            }
            return new ClickyInkDocument.Mark?(ClickyInkDocument.Mark.New);
          }
          break;
        }
        case 4:
        {
          char c = text[0];
          if (c <= 'g')
          {
            if (c != 'd')
            {
              if (c != 'g')
              {
                goto IL_0190;
              }
              if (!(text == "gone"))
              {
                goto IL_0190;
              }
              return new ClickyInkDocument.Mark?(ClickyInkDocument.Mark.Gone);
            }
            else
            {
              if (!(text == "done"))
              {
                goto IL_0190;
              }
              return new ClickyInkDocument.Mark?(ClickyInkDocument.Mark.Done);
            }
          }
          else if (c != 'p')
          {
            if (c != 's')
            {
              goto IL_0190;
            }
            if (!(text == "skip"))
            {
              goto IL_0190;
            }
          }
          else
          {
            if (!(text == "pick"))
            {
              goto IL_0190;
            }
            return new ClickyInkDocument.Mark?(ClickyInkDocument.Mark.Pick);
          }
          break;
        }
        case 5:
          if (!(text == "needs"))
          {
            goto IL_0190;
          }
          return new ClickyInkDocument.Mark?(ClickyInkDocument.Mark.Needs);
        case 6:
        case 7:
          goto IL_0190;
        case 8:
          if (!(text == "ruledout"))
          {
            goto IL_0190;
          }
          break;
        case 9:
          if (!(text == "ruled-out"))
          {
            goto IL_0190;
          }
          break;
        default:
          goto IL_0190;
        }
        return new ClickyInkDocument.Mark?(ClickyInkDocument.Mark.Gone);
      }
      IL_0190:
      return null;
    }

    // Token: 0x0600149E RID: 5278 RVA: 0x003ADDEC File Offset: 0x003ADDEC
    public static ClickyInkDocument.Mark? FirstMark(IEnumerable<string> keys)
    {
      foreach (string text in keys)
      {
        ClickyInkDocument.Mark? mark = ClickyInkDocumentKinds.MarkFromWord(text);
        if (mark != null)
        {
          ClickyInkDocument.Mark valueOrDefault = mark.GetValueOrDefault();
          return new ClickyInkDocument.Mark?(valueOrDefault);
        }
      }
      return null;
    }
  }
}
