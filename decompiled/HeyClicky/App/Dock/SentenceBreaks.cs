using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Dock
{
  // Token: 0x0200030B RID: 779
  public static class SentenceBreaks
  {
    // Token: 0x060021D8 RID: 8664 RVA: 0x0061E33C File Offset: 0x0061E33C
    [NullableContext(1)]
    public static IEnumerable<string> Sentences(string text)
    {
      SentenceBreaks.<Sentences>d__1 <Sentences>d__ = new SentenceBreaks.<Sentences>d__1(-2);
      <Sentences>d__.<>3__text = text;
      return <Sentences>d__;
    }

    // Token: 0x060021D9 RID: 8665 RVA: 0x0061E34C File Offset: 0x0061E34C
    [NullableContext(1)]
    private static bool BreaksBefore(List<SentenceBreaks.Kind> c, int i)
    {
      SentenceBreaks.Kind kind = c[i - 1];
      SentenceBreaks.Kind kind2 = c[i];
      if (kind == SentenceBreaks.Kind.CR && kind2 == SentenceBreaks.Kind.LF)
      {
        return false;
      }
      bool flag = kind - SentenceBreaks.Kind.CR <= 2;
      if (flag)
      {
        return true;
      }
      if (kind == SentenceBreaks.Kind.ATerm && kind2 == SentenceBreaks.Kind.Numeric)
      {
        return false;
      }
      flag = kind == SentenceBreaks.Kind.ATerm && kind2 == SentenceBreaks.Kind.Upper && i >= 2;
      if (flag)
      {
        SentenceBreaks.Kind kind3 = c[i - 2];
        bool flag2 = kind3 - SentenceBreaks.Kind.Lower <= 1;
        flag = flag2;
      }
      if (flag)
      {
        return false;
      }
      int num = i - 1;
      while (num >= 0 && c[num] == SentenceBreaks.Kind.Sp)
      {
        num--;
      }
      bool flag3 = num < i - 1;
      while (num >= 0 && c[num] == SentenceBreaks.Kind.Close)
      {
        num--;
      }
      flag = num < 0;
      if (!flag)
      {
        SentenceBreaks.Kind kind3 = c[num];
        bool flag2 = kind3 - SentenceBreaks.Kind.ATerm <= 1;
        flag = !flag2;
      }
      if (flag)
      {
        return false;
      }
      if (c[num] == SentenceBreaks.Kind.ATerm)
      {
        int num2 = i;
        for (;;)
        {
          flag = num2 < c.Count;
          if (flag)
          {
            bool flag2;
            switch (c[num2])
            {
            case SentenceBreaks.Kind.CR:
            case SentenceBreaks.Kind.LF:
            case SentenceBreaks.Kind.Sep:
            case SentenceBreaks.Kind.Lower:
            case SentenceBreaks.Kind.Upper:
            case SentenceBreaks.Kind.OLetter:
            case SentenceBreaks.Kind.ATerm:
            case SentenceBreaks.Kind.STerm:
              flag2 = true;
              break;
            case SentenceBreaks.Kind.Sp:
            case SentenceBreaks.Kind.Numeric:
              goto IL_0147;
            default:
              goto IL_0147;
            }
            IL_014A:
            flag = !flag2;
            goto IL_0151;
            IL_0147:
            flag2 = false;
            goto IL_014A;
          }
          IL_0151:
          if (!flag)
          {
            break;
          }
          num2++;
        }
        if (num2 < c.Count && c[num2] == SentenceBreaks.Kind.Lower)
        {
          return false;
        }
      }
      flag = kind2 - SentenceBreaks.Kind.ATerm <= 1 || kind2 == SentenceBreaks.Kind.SContinue;
      if (flag)
      {
        return false;
      }
      flag = !flag3;
      if (flag)
      {
        bool flag2 = kind2 - SentenceBreaks.Kind.CR <= 3 || kind2 == SentenceBreaks.Kind.Close;
        flag = flag2;
      }
      if (flag)
      {
        return false;
      }
      flag = kind2 - SentenceBreaks.Kind.CR <= 3;
      return !flag;
    }

    // Token: 0x060021DA RID: 8666 RVA: 0x0061E51C File Offset: 0x0061E51C
    private static SentenceBreaks.Kind KindOf(Rune rune)
    {
      int value = rune.Value;
      if (value <= 2405)
      {
        if (value <= 133)
        {
          if (value <= 39)
          {
            if (value <= 13)
            {
              if (value == 10)
              {
                return SentenceBreaks.Kind.LF;
              }
              if (value != 13)
              {
                goto IL_02A1;
              }
              return SentenceBreaks.Kind.CR;
            }
            else
            {
              if (value == 33)
              {
                return SentenceBreaks.Kind.STerm;
              }
              if (value != 34 && value != 39)
              {
                goto IL_02A1;
              }
            }
          }
          else if (value <= 46)
          {
            if (value - 44 <= 1)
            {
              return SentenceBreaks.Kind.SContinue;
            }
            if (value != 46)
            {
              goto IL_02A1;
            }
            return SentenceBreaks.Kind.ATerm;
          }
          else
          {
            if (value == 58)
            {
              return SentenceBreaks.Kind.SContinue;
            }
            if (value == 63)
            {
              return SentenceBreaks.Kind.STerm;
            }
            if (value != 133)
            {
              goto IL_02A1;
            }
            return SentenceBreaks.Kind.Sep;
          }
        }
        else if (value <= 1549)
        {
          if (value <= 187)
          {
            if (value != 171 && value != 187)
            {
              goto IL_02A1;
            }
          }
          else
          {
            if (value == 1373)
            {
              return SentenceBreaks.Kind.SContinue;
            }
            if (value == 1417)
            {
              return SentenceBreaks.Kind.STerm;
            }
            if (value - 1548 > 1)
            {
              goto IL_02A1;
            }
            return SentenceBreaks.Kind.SContinue;
          }
        }
        else if (value <= 1748)
        {
          if (value != 1567 && value != 1748)
          {
            goto IL_02A1;
          }
          return SentenceBreaks.Kind.STerm;
        }
        else
        {
          if (value - 1792 <= 2)
          {
            return SentenceBreaks.Kind.STerm;
          }
          if (value == 2040)
          {
            return SentenceBreaks.Kind.SContinue;
          }
          if (value - 2404 > 1)
          {
            goto IL_02A1;
          }
          return SentenceBreaks.Kind.STerm;
        }
        return SentenceBreaks.Kind.Close;
      }
      if (value <= 65041)
      {
        if (value <= 8233)
        {
          if (value <= 6152)
          {
            if (value != 6146 && value != 6152)
            {
              goto IL_02A1;
            }
            return SentenceBreaks.Kind.SContinue;
          }
          else
          {
            if (value - 8211 <= 1)
            {
              return SentenceBreaks.Kind.SContinue;
            }
            if (value == 8228)
            {
              return SentenceBreaks.Kind.ATerm;
            }
            if (value - 8232 > 1)
            {
              goto IL_02A1;
            }
          }
        }
        else if (value <= 8265)
        {
          if (value - 8252 > 1 && value - 8263 > 2)
          {
            goto IL_02A1;
          }
          return SentenceBreaks.Kind.STerm;
        }
        else
        {
          if (value == 12289)
          {
            return SentenceBreaks.Kind.SContinue;
          }
          if (value == 12290)
          {
            return SentenceBreaks.Kind.STerm;
          }
          if (value - 65040 > 1)
          {
            goto IL_02A1;
          }
          return SentenceBreaks.Kind.SContinue;
        }
      }
      else if (value <= 65281)
      {
        if (value <= 65074)
        {
          if (value != 65043 && value - 65073 > 1)
          {
            goto IL_02A1;
          }
          return SentenceBreaks.Kind.SContinue;
        }
        else
        {
          switch (value)
          {
          case 65104:
          case 65105:
          case 65109:
          case 65112:
            return SentenceBreaks.Kind.SContinue;
          case 65106:
            return SentenceBreaks.Kind.ATerm;
          case 65107:
          case 65108:
            goto IL_02A1;
          case 65110:
          case 65111:
            return SentenceBreaks.Kind.STerm;
          default:
            if (value == 65123)
            {
              return SentenceBreaks.Kind.SContinue;
            }
            if (value != 65281)
            {
              goto IL_02A1;
            }
            return SentenceBreaks.Kind.STerm;
          }
        }
      }
      else if (value <= 65306)
      {
        if (value - 65292 <= 1)
        {
          return SentenceBreaks.Kind.SContinue;
        }
        if (value == 65294)
        {
          return SentenceBreaks.Kind.ATerm;
        }
        if (value != 65306)
        {
          goto IL_02A1;
        }
        return SentenceBreaks.Kind.SContinue;
      }
      else
      {
        if (value == 65311 || value == 65377)
        {
          return SentenceBreaks.Kind.STerm;
        }
        if (value != 65380)
        {
          goto IL_02A1;
        }
        return SentenceBreaks.Kind.SContinue;
      }
      return SentenceBreaks.Kind.Sep;
      IL_02A1:
      if (Rune.IsWhiteSpace(rune))
      {
        return SentenceBreaks.Kind.Sp;
      }
      if (Rune.IsDigit(rune))
      {
        return SentenceBreaks.Kind.Numeric;
      }
      switch (Rune.GetUnicodeCategory(rune))
      {
      case UnicodeCategory.UppercaseLetter:
      case UnicodeCategory.TitlecaseLetter:
        return SentenceBreaks.Kind.Upper;
      case UnicodeCategory.LowercaseLetter:
        return SentenceBreaks.Kind.Lower;
      case UnicodeCategory.ModifierLetter:
      case UnicodeCategory.OtherLetter:
      case UnicodeCategory.LetterNumber:
        return SentenceBreaks.Kind.OLetter;
      case UnicodeCategory.NonSpacingMark:
      case UnicodeCategory.SpacingCombiningMark:
      case UnicodeCategory.EnclosingMark:
      case UnicodeCategory.Format:
        return SentenceBreaks.Kind.Extend;
      case UnicodeCategory.OpenPunctuation:
      case UnicodeCategory.ClosePunctuation:
      case UnicodeCategory.InitialQuotePunctuation:
      case UnicodeCategory.FinalQuotePunctuation:
        return SentenceBreaks.Kind.Close;
      }
      return SentenceBreaks.Kind.Other;
    }

    // Token: 0x02000954 RID: 2388
    private enum Kind
    {
      // Token: 0x0400272A RID: 10026
      Other,
      // Token: 0x0400272B RID: 10027
      CR,
      // Token: 0x0400272C RID: 10028
      LF,
      // Token: 0x0400272D RID: 10029
      Sep,
      // Token: 0x0400272E RID: 10030
      Sp,
      // Token: 0x0400272F RID: 10031
      Lower,
      // Token: 0x04002730 RID: 10032
      Upper,
      // Token: 0x04002731 RID: 10033
      OLetter,
      // Token: 0x04002732 RID: 10034
      Numeric,
      // Token: 0x04002733 RID: 10035
      ATerm,
      // Token: 0x04002734 RID: 10036
      STerm,
      // Token: 0x04002735 RID: 10037
      Close,
      // Token: 0x04002736 RID: 10038
      SContinue,
      // Token: 0x04002737 RID: 10039
      Extend
    }
  }
}
