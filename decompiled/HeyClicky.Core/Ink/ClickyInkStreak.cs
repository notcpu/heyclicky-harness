using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000138 RID: 312
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkStreak
  {
    // Token: 0x060013E6 RID: 5094 RVA: 0x003A9A00 File Offset: 0x003A9A00
    public static List<ClickyInkStreakDay> Days([Nullable(2)] string text)
    {
      string text2 = ((text != null) ? text.TrimWs() : null) ?? "";
      if (text2.Length == 0)
      {
        return new List<ClickyInkStreakDay>();
      }
      List<string> list = text2.SplitCharacters((string c) => c == "," || c == " ");
      List<ClickyInkStreakDay> list2;
      if (list.Count > 1)
      {
        if (list.Any<string>((string w) => w.CharacterCount() > 1))
        {
          IEnumerable<string> enumerable = list;
          Func<string, ClickyInkStreakDay?> func;
          if ((func = ClickyInkStreak.<>O.<0>__DayOfWord) == null)
          {
            func = (ClickyInkStreak.<>O.<0>__DayOfWord = new Func<string, ClickyInkStreakDay?>(ClickyInkStreak.DayOfWord));
          }
          list2 = (from d in enumerable.Select<string, ClickyInkStreakDay?>(func)
            where d != null
            select d.Value).ToList<ClickyInkStreakDay>();
          goto IL_0162;
        }
      }
      IEnumerable<string> enumerable2 = text2.Characters();
      Func<string, ClickyInkStreakDay?> func2;
      if ((func2 = ClickyInkStreak.<>O.<1>__DayOfCharacter) == null)
      {
        func2 = (ClickyInkStreak.<>O.<1>__DayOfCharacter = new Func<string, ClickyInkStreakDay?>(ClickyInkStreak.DayOfCharacter));
      }
      list2 = (from d in enumerable2.Select<string, ClickyInkStreakDay?>(func2)
        where d != null
        select d.Value).ToList<ClickyInkStreakDay>();
      IL_0162:
      return list2.Skip<ClickyInkStreakDay>(Math.Max(0, list2.Count - 42)).ToList<ClickyInkStreakDay>();
    }

    // Token: 0x060013E7 RID: 5095 RVA: 0x003A9B8C File Offset: 0x003A9B8C
    private static ClickyInkStreakDay? DayOfCharacter(string character)
    {
      if (character != null)
      {
        int length = character.Length;
        if (length == 1)
        {
          char c = character[0];
          if (c <= '–')
          {
            if (c <= 'Y')
            {
              if (c <= '?')
              {
                switch (c)
                {
                case '#':
                case '*':
                case '+':
                case '1':
                  goto IL_01BD;
                case '$':
                case '%':
                case '&':
                case '\'':
                case '(':
                case ')':
                case ',':
                  goto IL_01EA;
                case '-':
                case '/':
                  break;
                case '.':
                case '0':
                  goto IL_01C6;
                default:
                  if (c != '?')
                  {
                    goto IL_01EA;
                  }
                  goto IL_01D8;
                }
              }
              else
              {
                if (c == '@')
                {
                  goto IL_01D8;
                }
                switch (c)
                {
                case 'N':
                case 'O':
                  goto IL_01C6;
                case 'P':
                case 'Q':
                case 'S':
                case 'U':
                case 'W':
                  goto IL_01EA;
                case 'R':
                  break;
                case 'T':
                  goto IL_01D8;
                case 'V':
                case 'X':
                case 'Y':
                  goto IL_01BD;
                default:
                  goto IL_01EA;
                }
              }
            }
            else if (c <= 'y')
            {
              if (c == '_')
              {
                return new ClickyInkStreakDay?(ClickyInkStreakDay.Upcoming);
              }
              switch (c)
              {
              case 'n':
              case 'o':
                goto IL_01C6;
              case 'p':
              case 'q':
              case 's':
              case 'u':
              case 'w':
                goto IL_01EA;
              case 'r':
                break;
              case 't':
                goto IL_01D8;
              case 'v':
              case 'x':
              case 'y':
                goto IL_01BD;
              default:
                goto IL_01EA;
              }
            }
            else if (c != '~')
            {
              if (c == '×')
              {
                goto IL_01C6;
              }
              if (c != '–')
              {
                goto IL_01EA;
              }
            }
            return new ClickyInkStreakDay?(ClickyInkStreakDay.Rest);
          }
          if (c <= '○')
          {
            if (c <= '■')
            {
              if (c != '█' && c != '■')
              {
                goto IL_01EA;
              }
            }
            else
            {
              if (c != '□' && c != '○')
              {
                goto IL_01EA;
              }
              goto IL_01C6;
            }
          }
          else if (c <= '◐')
          {
            if (c != '●')
            {
              if (c != '◐')
              {
                goto IL_01EA;
              }
              goto IL_01D8;
            }
          }
          else
          {
            if (c == '☐')
            {
              goto IL_01C6;
            }
            if (c != '☑')
            {
              switch (c)
              {
              case '✓':
              case '✔':
                break;
              case '✕':
              case '✖':
                goto IL_01EA;
              case '✗':
                goto IL_01C6;
              default:
                goto IL_01EA;
              }
            }
          }
          IL_01BD:
          return new ClickyInkStreakDay?(ClickyInkStreakDay.Done);
          IL_01C6:
          return new ClickyInkStreakDay?(ClickyInkStreakDay.Missed);
          IL_01D8:
          return new ClickyInkStreakDay?(ClickyInkStreakDay.Today);
        }
      }
      IL_01EA:
      return null;
    }

    // Token: 0x060013E8 RID: 5096 RVA: 0x003A9D90 File Offset: 0x003A9D90
    private static ClickyInkStreakDay? DayOfWord(string word)
    {
      string text = word.Lowered();
      if (text != null)
      {
        switch (text.Length)
        {
        case 1:
        {
          char c = text[0];
          if (c <= '_')
          {
            switch (c)
            {
            case '-':
              goto IL_026B;
            case '.':
            case '/':
              goto IL_0286;
            case '0':
              goto IL_0262;
            case '1':
              break;
            default:
              if (c == '?')
              {
                goto IL_0274;
              }
              if (c != '_')
              {
                goto IL_0286;
              }
              goto IL_027D;
            }
          }
          else if (c <= 'x')
          {
            if (c == 'n')
            {
              goto IL_0262;
            }
            if (c != 'x')
            {
              goto IL_0286;
            }
          }
          else if (c != 'y' && c != '✓')
          {
            goto IL_0286;
          }
          break;
        }
        case 2:
          if (!(text == "no"))
          {
            goto IL_0286;
          }
          goto IL_0262;
        case 3:
        {
          char c = text[0];
          if (c <= 'h')
          {
            if (c != 'd')
            {
              if (c != 'h')
              {
                goto IL_0286;
              }
              if (!(text == "hit"))
              {
                goto IL_0286;
              }
            }
            else if (!(text == "did"))
            {
              goto IL_0286;
            }
          }
          else if (c != 'n')
          {
            if (c != 'o')
            {
              if (c != 'y')
              {
                goto IL_0286;
              }
              if (!(text == "yes"))
              {
                goto IL_0286;
              }
            }
            else
            {
              if (!(text == "off"))
              {
                goto IL_0286;
              }
              goto IL_026B;
            }
          }
          else
          {
            if (!(text == "now"))
            {
              goto IL_0286;
            }
            goto IL_0274;
          }
          break;
        }
        case 4:
        {
          char c = text[0];
          if (c <= 'm')
          {
            if (c != 'd')
            {
              if (c != 'm')
              {
                goto IL_0286;
              }
              if (!(text == "miss"))
              {
                goto IL_0286;
              }
              goto IL_0262;
            }
            else if (!(text == "done"))
            {
              goto IL_0286;
            }
          }
          else if (c != 'r')
          {
            if (c != 's')
            {
              goto IL_0286;
            }
            if (!(text == "skip"))
            {
              goto IL_0286;
            }
            goto IL_0262;
          }
          else
          {
            if (!(text == "rest"))
            {
              goto IL_0286;
            }
            goto IL_026B;
          }
          break;
        }
        case 5:
        {
          char c = text[0];
          if (c != 'l')
          {
            if (c != 't')
            {
              goto IL_0286;
            }
            if (!(text == "today"))
            {
              goto IL_0286;
            }
            goto IL_0274;
          }
          else
          {
            if (!(text == "later"))
            {
              goto IL_0286;
            }
            goto IL_027D;
          }
          break;
        }
        case 6:
          if (!(text == "missed"))
          {
            goto IL_0286;
          }
          goto IL_0262;
        case 7:
          if (!(text == "skipped"))
          {
            goto IL_0286;
          }
          goto IL_0262;
        case 8:
          if (!(text == "upcoming"))
          {
            goto IL_0286;
          }
          goto IL_027D;
        default:
          goto IL_0286;
        }
        return new ClickyInkStreakDay?(ClickyInkStreakDay.Done);
        IL_0262:
        return new ClickyInkStreakDay?(ClickyInkStreakDay.Missed);
        IL_026B:
        return new ClickyInkStreakDay?(ClickyInkStreakDay.Rest);
        IL_0274:
        return new ClickyInkStreakDay?(ClickyInkStreakDay.Today);
        IL_027D:
        return new ClickyInkStreakDay?(ClickyInkStreakDay.Upcoming);
      }
      IL_0286:
      return (word.CharacterCount() == 1) ? ClickyInkStreak.DayOfCharacter(word) : null;
    }

    // Token: 0x060013E9 RID: 5097 RVA: 0x003AA040 File Offset: 0x003AA040
    public static int CurrentStreak(IReadOnlyList<ClickyInkStreakDay> days)
    {
      int num = 0;
      for (int i = days.Count - 1; i >= 0; i--)
      {
        ClickyInkStreakDay clickyInkStreakDay = days[i];
        if (clickyInkStreakDay != ClickyInkStreakDay.Done)
        {
          if (clickyInkStreakDay == ClickyInkStreakDay.Missed)
          {
            return num;
          }
        }
        else
        {
          num++;
        }
      }
      return num;
    }

    // Token: 0x040008DC RID: 2268
    public const int MaximumDays = 42;

    // Token: 0x020004B8 RID: 1208
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001DBF RID: 7615
      [Nullable(0)]
      public static Func<string, ClickyInkStreakDay?> <0>__DayOfWord;

      // Token: 0x04001DC0 RID: 7616
      [Nullable(0)]
      public static Func<string, ClickyInkStreakDay?> <1>__DayOfCharacter;
    }
  }
}
