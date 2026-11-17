using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions.Generated;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000179 RID: 377
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkPlanOff
  {
    // Token: 0x060016B4 RID: 5812 RVA: 0x003C05C8 File Offset: 0x003C05C8
    [GeneratedRegex("\\s*\\{now\\}")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex NowMarkRegex()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__NowMarkRegex_45.Instance;
    }

    // Token: 0x060016B5 RID: 5813 RVA: 0x003C05D0 File Offset: 0x003C05D0
    [GeneratedRegex("\\{[^}]*big=")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex BigRegex()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BigRegex_46.Instance;
    }

    // Token: 0x060016B6 RID: 5814 RVA: 0x003C05D8 File Offset: 0x003C05D8
    public static string TakePlanOff(string text)
    {
      int num = 0;
      bool flag = false;
      bool flag2 = false;
      List<string> list = new List<string>();
      string[] array = text.Split('\n', StringSplitOptions.None);
      int i = 0;
      while (i < array.Length)
      {
        string text2 = array[i];
        string text3 = text2.TrimWs();
        if (text2.HasPrefix("## "))
        {
          num += ((num == 0 && flag) ? 2 : 1);
        }
        else if (num == 0 && text3.Length > 0 && !text3.HasPrefix("# "))
        {
          flag = true;
        }
        if (!text3.HasPrefix(":::") || text3.Length <= 3)
        {
          goto IL_00B2;
        }
        string text4 = text3;
        if (text4.Substring(3, text4.Length - 3).HasSuffix(":::") || !char.IsLetter(text3, 3))
        {
          goto IL_00B2;
        }
        flag2 = true;
        IL_00C2:
        if (flag2 || num > 1 || !ClickyInkPlanOff.IsPlanBox(text3) || text3.Contains("{needs}", StringComparison.Ordinal))
        {
          list.Add(ClickyInkPlanOff.NowMarkRegex().Replace(text2, ""));
        }
        i++;
        continue;
        IL_00B2:
        if (text3 == ":::")
        {
          flag2 = false;
          goto IL_00C2;
        }
        goto IL_00C2;
      }
      string text5 = string.Join("\n", list);
      while (text5.Contains("\n\n\n", StringComparison.Ordinal))
      {
        text5 = text5.Replace("\n\n\n", "\n\n");
      }
      return text5;
    }

    // Token: 0x060016B7 RID: 5815 RVA: 0x003C0724 File Offset: 0x003C0724
    public static bool HasAnswer(string text)
    {
      return text.Split('\n', StringSplitOptions.None).Any<string>(delegate(string line)
      {
        if (!line.HasPrefix("> "))
        {
          return false;
        }
        string text2 = line.Substring(2, line.Length - 2).TrimWs();
        return ClickyInkPlanOff.BigRegex().IsMatch(line) || (text2.Length > 0 && !text2.HasPrefix("Checking") && !text2.HasPrefix("Looking") && !text2.HasPrefix("Finding"));
      });
    }

    // Token: 0x060016B8 RID: 5816 RVA: 0x003C0754 File Offset: 0x003C0754
    private static bool IsPlanBox(string trimmedLine)
    {
      return trimmedLine.HasPrefix("- [ ]") || trimmedLine.HasPrefix("- [x]") || trimmedLine.HasPrefix("- [X]");
    }
  }
}
