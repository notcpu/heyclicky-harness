using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000177 RID: 375
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkPatchStream
  {
    // Token: 0x060016AA RID: 5802 RVA: 0x003BFF64 File Offset: 0x003BFF64
    [return: Nullable(2)]
    public unsafe static string PartialText(string current, string kind, string diff)
    {
      if (kind == "add")
      {
        if (diff.Length != 0)
        {
          return diff;
        }
        return null;
      }
      else
      {
        if (kind != "update" || diff.Length == 0)
        {
          return null;
        }
        List<string> list = current.Split('\n', StringSplitOptions.None).ToList<string>();
        List<List<string>> list2 = new List<List<string>>();
        foreach (string text in diff.Split('\n', StringSplitOptions.None))
        {
          if (text.HasPrefix("@@"))
          {
            List<List<string>> list3 = list2;
            int num = 1;
            List<string> list4 = new List<string>(num);
            CollectionsMarshal.SetCount<string>(list4, num);
            Span<string> span = CollectionsMarshal.AsSpan<string>(list4);
            int num2 = 0;
            *span[num2] = text;
            list3.Add(list4);
          }
          else if (list2.Count > 0)
          {
            List<List<string>> list5 = list2;
            list5[list5.Count - 1].Add(text);
          }
        }
        int num3 = 0;
        for (int j = 0; j < list2.Count; j++)
        {
          List<string> list6 = list2[j];
          bool flag = j == list2.Count - 1;
          string text2 = list6[0];
          string text3 = text2.Substring(2, text2.Length - 2).TrimWs();
          if (text3.Length > 0)
          {
            int num4 = -1;
            for (int k = num3; k < list.Count; k++)
            {
              if (ClickyInkPatchStream.Same(list[k], text3))
              {
                num4 = k;
                break;
              }
            }
            if (num4 < 0)
            {
              return null;
            }
            num3 = num4 + 1;
          }
          List<string> list7 = (from l in list6.Skip<string>(1)
            where !l.HasPrefix("***") && !l.HasPrefix("\\")
            select l).ToList<string>();
          if (list7.Count > 0)
          {
            List<string> list8 = list7;
            if (list8[list8.Count - 1] == "")
            {
              list7.RemoveAt(list7.Count - 1);
            }
          }
          int? num5 = ClickyInkPatchStream.Location(list7.Where<string>((string l) => !l.HasPrefix("+")).Select<string, string>(delegate(string l)
          {
            if (l.Length != 0)
            {
              return l.Substring(1, l.Length - 1);
            }
            return "";
          }).ToList<string>(), list, num3);
          if (num5 == null)
          {
            return null;
          }
          int valueOrDefault = num5.GetValueOrDefault();
          int num6 = valueOrDefault;
          int m = 0;
          while (m < list7.Count)
          {
            string text4 = list7[m];
            if (!text4.HasPrefix("-") && !text4.HasPrefix("+"))
            {
              num6++;
              m++;
            }
            else
            {
              int num7 = 0;
              List<string> list9 = new List<string>();
              while (m < list7.Count)
              {
                if (!list7[m].HasPrefix("-"))
                {
                  break;
                }
                num7++;
                m++;
              }
              while (m < list7.Count && list7[m].HasPrefix("+"))
              {
                List<string> list10 = list9;
                text2 = list7[m];
                list10.Add(text2.Substring(1, text2.Length - 1));
                m++;
              }
              List<string> list11 = ((flag && m >= list7.Count && list9.Count < num7) ? list.GetRange(num6 + list9.Count, num7 - list9.Count) : new List<string>());
              list.RemoveRange(num6, num7);
              List<string> list12 = list;
              int num8 = num6;
              List<string> list13 = list9;
              List<string> list14 = list11;
              int i = 0;
              string[] array = new string[list13.Count + list14.Count];
              Span<string> span = CollectionsMarshal.AsSpan<string>(list13);
              span.CopyTo(new Span<string>(array).Slice(i, span.Length));
              i += span.Length;
              Span<string> span2 = CollectionsMarshal.AsSpan<string>(list14);
              span2.CopyTo(new Span<string>(array).Slice(i, span2.Length));
              i += span2.Length;
              list12.InsertRange(num8, new <>z__ReadOnlyArray<string>(array));
              num6 += list9.Count + list11.Count;
            }
          }
          num3 = Math.Min(num6, list.Count);
        }
        return string.Join("\n", list);
      }
    }

    // Token: 0x060016AB RID: 5803 RVA: 0x003C038C File Offset: 0x003C038C
    private static int? Location(List<string> old, List<string> lines, int start)
    {
      if (old.Count == 0)
      {
        return new int?(Math.Min(start, lines.Count));
      }
      if (old.Count > lines.Count || start > lines.Count - old.Count)
      {
        return null;
      }
      for (int i = start; i <= lines.Count - old.Count; i++)
      {
        bool flag = true;
        for (int j = 0; j < old.Count; j++)
        {
          if (!ClickyInkPatchStream.Same(lines[i + j], old[j]))
          {
            flag = false;
            break;
          }
        }
        if (flag)
        {
          return new int?(i);
        }
      }
      return null;
    }

    // Token: 0x060016AC RID: 5804 RVA: 0x003C0434 File Offset: 0x003C0434
    private static bool Same(string a, string b)
    {
      return a.TrimWs() == b.TrimWs();
    }
  }
}
