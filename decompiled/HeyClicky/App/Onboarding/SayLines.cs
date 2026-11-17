using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x0200011D RID: 285
  public static class SayLines
  {
    // Token: 0x06000F57 RID: 3927 RVA: 0x0057A3EC File Offset: 0x0057A3EC
    [NullableContext(1)]
    public static Dictionary<int, string> ByStep(string code)
    {
      SayLines.<>c__DisplayClass0_0 CS$<>8__locals1;
      CS$<>8__locals1.code = code;
      Dictionary<int, string> dictionary = new Dictionary<int, string>();
      List<string> list = new List<string>();
      int? num = null;
      CS$<>8__locals1.i = 0;
      while (CS$<>8__locals1.i < CS$<>8__locals1.code.Length)
      {
        if (SayLines.<ByStep>g__Starts|0_0("step", ref CS$<>8__locals1))
        {
          CS$<>8__locals1.i += 5;
          SayLines.<ByStep>g__SkipSpaces|0_1(ref CS$<>8__locals1);
          int i = CS$<>8__locals1.i;
          while (CS$<>8__locals1.i < CS$<>8__locals1.code.Length && char.IsDigit(CS$<>8__locals1.code[CS$<>8__locals1.i]))
          {
            int num2 = CS$<>8__locals1.i;
            CS$<>8__locals1.i = num2 + 1;
          }
          int num3;
          if (int.TryParse(CS$<>8__locals1.code.AsSpan(i, CS$<>8__locals1.i - i), out num3))
          {
            num = new int?(num3);
          }
        }
        else if (SayLines.<ByStep>g__Starts|0_0("say", ref CS$<>8__locals1))
        {
          CS$<>8__locals1.i += 4;
          SayLines.<ByStep>g__SkipSpaces|0_1(ref CS$<>8__locals1);
          bool flag = CS$<>8__locals1.i >= CS$<>8__locals1.code.Length;
          if (!flag)
          {
            char c = CS$<>8__locals1.code[CS$<>8__locals1.i];
            bool flag2 = c == '"' || c == '\'' || c == '`';
            flag = !flag2;
          }
          if (!flag)
          {
            string code2 = CS$<>8__locals1.code;
            int num2 = CS$<>8__locals1.i;
            CS$<>8__locals1.i = num2 + 1;
            char c2 = code2[num2];
            StringBuilder stringBuilder = new StringBuilder();
            while (CS$<>8__locals1.i < CS$<>8__locals1.code.Length && CS$<>8__locals1.code[CS$<>8__locals1.i] != c2)
            {
              if (CS$<>8__locals1.code[CS$<>8__locals1.i] == '\\' && CS$<>8__locals1.i + 1 < CS$<>8__locals1.code.Length)
              {
                char c3 = CS$<>8__locals1.code[CS$<>8__locals1.i + 1];
                StringBuilder stringBuilder2 = stringBuilder;
                flag = c3 == 'n' || c3 == 't';
                stringBuilder2.Append(flag ? ' ' : c3);
                CS$<>8__locals1.i += 2;
              }
              else
              {
                StringBuilder stringBuilder3 = stringBuilder;
                string code3 = CS$<>8__locals1.code;
                num2 = CS$<>8__locals1.i;
                CS$<>8__locals1.i = num2 + 1;
                stringBuilder3.Append(code3[num2]);
              }
            }
            num2 = CS$<>8__locals1.i;
            CS$<>8__locals1.i = num2 + 1;
            string text = stringBuilder.ToString().Trim();
            if (text.Length != 0)
            {
              list.Add(text);
              if (num != null)
              {
                int valueOrDefault = num.GetValueOrDefault();
                if (!dictionary.ContainsKey(valueOrDefault))
                {
                  dictionary[valueOrDefault] = text;
                }
              }
            }
          }
        }
        else
        {
          int num2 = CS$<>8__locals1.i;
          CS$<>8__locals1.i = num2 + 1;
        }
      }
      if (dictionary.Count == 0)
      {
        for (int j = 0; j < list.Count; j++)
        {
          dictionary[j + 1] = list[j];
        }
      }
      return dictionary;
    }

    // Token: 0x06000F58 RID: 3928 RVA: 0x0057A6F0 File Offset: 0x0057A6F0
    [NullableContext(1)]
    [CompilerGenerated]
    internal static bool <ByStep>g__Starts|0_0(string name, ref SayLines.<>c__DisplayClass0_0 A_1)
    {
      string text = name + "(";
      if (A_1.i + text.Length > A_1.code.Length || string.CompareOrdinal(A_1.code, A_1.i, text, 0, text.Length) != 0)
      {
        return false;
      }
      if (A_1.i > 0)
      {
        char c = A_1.code[A_1.i - 1];
        if (char.IsLetterOrDigit(c) || c == '_' || c == '.')
        {
          return false;
        }
      }
      return true;
    }

    // Token: 0x06000F59 RID: 3929 RVA: 0x0057A774 File Offset: 0x0057A774
    [CompilerGenerated]
    internal static void <ByStep>g__SkipSpaces|0_1(ref SayLines.<>c__DisplayClass0_0 A_0)
    {
      while (A_0.i < A_0.code.Length && char.IsWhiteSpace(A_0.code[A_0.i]))
      {
        int i = A_0.i;
        A_0.i = i + 1;
      }
    }
  }
}
