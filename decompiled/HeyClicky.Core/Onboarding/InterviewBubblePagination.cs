using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Onboarding
{
  // Token: 0x020000E8 RID: 232
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class InterviewBubblePagination
  {
    // Token: 0x1700035B RID: 859
    // (get) Token: 0x06000EFB RID: 3835 RVA: 0x0038D34C File Offset: 0x0038D34C
    public IReadOnlyList<string> Bubbles
    {
      get
      {
        return this.bubbles;
      }
    }

    // Token: 0x06000EFC RID: 3836 RVA: 0x0038D354 File Offset: 0x0038D354
    public void Update(string text, Func<string, bool> fits)
    {
      if (!text.StartsWith(this.source, StringComparison.Ordinal))
      {
        this.source = "";
        this.bubbles.Clear();
        this.startsNextBubble = false;
      }
      int num = this.source.Length;
      foreach (char c in text.Substring(num, text.Length - num))
      {
        if (this.bubbles.Count == 0)
        {
          this.bubbles.Add("");
        }
        if (char.IsWhiteSpace(c))
        {
          if (c != '\n')
          {
            List<string> list = this.bubbles;
            if (!InterviewBubblePagination.EndsSentence(list[list.Count - 1]))
            {
              List<string> list2 = this.bubbles;
              num = list2.Count - 1;
              List<string> list3 = list2;
              int num2 = num;
              ReadOnlySpan<char> readOnlySpan = list2[num];
              char c2 = c;
              list3[num2] = readOnlySpan + new ReadOnlySpan<char>(ref c2);
              goto IL_017F;
            }
          }
          this.startsNextBubble = true;
        }
        else
        {
          if (this.startsNextBubble)
          {
            this.bubbles.Add("");
            this.startsNextBubble = false;
          }
          List<string> list4 = this.bubbles;
          ReadOnlySpan<char> readOnlySpan2 = list4[list4.Count - 1];
          char c2 = c;
          string text3 = readOnlySpan2 + new ReadOnlySpan<char>(ref c2);
          List<string> list5 = this.bubbles;
          if (list5[list5.Count - 1].Length > 0 && !fits(text3))
          {
            this.bubbles.Add(c.ToString());
          }
          else
          {
            List<string> list6 = this.bubbles;
            list6[list6.Count - 1] = text3;
          }
        }
        IL_017F:;
      }
      this.source = text;
    }

    // Token: 0x06000EFD RID: 3837 RVA: 0x0038D4F8 File Offset: 0x0038D4F8
    private static bool EndsSentence(string text)
    {
      string text2 = text.Trim().TrimEnd(new char[] { '"', '\'', '”', '’', ')', ']' });
      if (text2.Length != 0)
      {
        string text3 = ".!?。！？";
        string text4 = text2;
        if (text3.Contains(text4[text4.Length - 1]))
        {
          string text5 = text2.Split(' ', StringSplitOptions.None).Last<string>().ToLowerInvariant();
          return !InterviewBubblePagination.Abbreviations.Contains(text5) && (text5.Length != 2 || !char.IsLetter(text5[0]));
        }
      }
      return false;
    }

    // Token: 0x04000661 RID: 1633
    private static readonly string[] Abbreviations = new string[] { "mr.", "mrs.", "ms.", "dr.", "e.g.", "i.e.", "etc." };

    // Token: 0x04000662 RID: 1634
    private string source = "";

    // Token: 0x04000663 RID: 1635
    private readonly List<string> bubbles = new List<string>();

    // Token: 0x04000664 RID: 1636
    private bool startsNextBubble;
  }
}
