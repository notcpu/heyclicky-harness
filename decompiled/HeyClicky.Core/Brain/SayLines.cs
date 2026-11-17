using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Brain
{
  // Token: 0x02000204 RID: 516
  [NullableContext(1)]
  [Nullable(0)]
  public static class SayLines
  {
    // Token: 0x06001E1F RID: 7711 RVA: 0x003EC57C File Offset: 0x003EC57C
    public static List<string> InOrder(string code)
    {
      List<string> lines = new List<string>();
      SayLines.Scan(code, delegate(int? _, string line)
      {
        lines.Add(line);
      });
      return lines;
    }

    // Token: 0x06001E20 RID: 7712 RVA: 0x003EC5B4 File Offset: 0x003EC5B4
    public static Dictionary<int, string> ByStep(string code)
    {
      Dictionary<int, string> byStep = new Dictionary<int, string>();
      List<string> inOrder = new List<string>();
      SayLines.Scan(code, delegate(int? step, string line)
      {
        inOrder.Add(line);
        if (step != null)
        {
          int valueOrDefault = step.GetValueOrDefault();
          byStep.TryAdd(valueOrDefault, line);
        }
      });
      if (byStep.Count < inOrder.Count && byStep.Count == 0)
      {
        for (int i = 0; i < inOrder.Count; i++)
        {
          byStep[i + 1] = inOrder[i];
        }
      }
      return byStep;
    }

    // Token: 0x06001E21 RID: 7713 RVA: 0x003EC64C File Offset: 0x003EC64C
    private static void Scan(string code, Action<int?, string> onLine)
    {
      SayLines.<>c__DisplayClass2_0 CS$<>8__locals1;
      CS$<>8__locals1.code = code;
      CS$<>8__locals1.index = 0;
      int? num = null;
      while (CS$<>8__locals1.index < CS$<>8__locals1.code.Length)
      {
        if (SayLines.StartsCall(CS$<>8__locals1.code, "step", CS$<>8__locals1.index))
        {
          CS$<>8__locals1.index += 5;
          SayLines.<Scan>g__SkipSpaces|2_0(ref CS$<>8__locals1);
          int index = CS$<>8__locals1.index;
          while (CS$<>8__locals1.index < CS$<>8__locals1.code.Length && char.IsDigit(CS$<>8__locals1.code[CS$<>8__locals1.index]))
          {
            int num2 = CS$<>8__locals1.index;
            CS$<>8__locals1.index = num2 + 1;
          }
          int num3;
          if (int.TryParse(CS$<>8__locals1.code.AsSpan(index, CS$<>8__locals1.index - index), out num3))
          {
            num = new int?(num3);
          }
        }
        else if (SayLines.StartsCall(CS$<>8__locals1.code, "say", CS$<>8__locals1.index))
        {
          CS$<>8__locals1.index += 4;
          SayLines.<Scan>g__SkipSpaces|2_0(ref CS$<>8__locals1);
          bool flag = CS$<>8__locals1.index >= CS$<>8__locals1.code.Length;
          if (!flag)
          {
            char c = CS$<>8__locals1.code[CS$<>8__locals1.index];
            bool flag2 = c == '"' || c == '\'' || c == '`';
            flag = !flag2;
          }
          if (!flag)
          {
            string code2 = CS$<>8__locals1.code;
            int num2 = CS$<>8__locals1.index;
            CS$<>8__locals1.index = num2 + 1;
            char c2 = code2[num2];
            StringBuilder stringBuilder = new StringBuilder();
            while (CS$<>8__locals1.index < CS$<>8__locals1.code.Length && CS$<>8__locals1.code[CS$<>8__locals1.index] != c2)
            {
              if (CS$<>8__locals1.code[CS$<>8__locals1.index] == '\\' && CS$<>8__locals1.index + 1 < CS$<>8__locals1.code.Length)
              {
                char c3 = CS$<>8__locals1.code[CS$<>8__locals1.index + 1];
                StringBuilder stringBuilder2 = stringBuilder;
                flag = c3 == 'n' || c3 == 't';
                stringBuilder2.Append(flag ? ' ' : c3);
                CS$<>8__locals1.index += 2;
              }
              else
              {
                StringBuilder stringBuilder3 = stringBuilder;
                string code3 = CS$<>8__locals1.code;
                num2 = CS$<>8__locals1.index;
                CS$<>8__locals1.index = num2 + 1;
                stringBuilder3.Append(code3[num2]);
              }
            }
            num2 = CS$<>8__locals1.index;
            CS$<>8__locals1.index = num2 + 1;
            string text = stringBuilder.ToString().Trim();
            if (text.Length > 0)
            {
              onLine(num, text);
            }
          }
        }
        else
        {
          int num2 = CS$<>8__locals1.index;
          CS$<>8__locals1.index = num2 + 1;
        }
      }
    }

    // Token: 0x06001E22 RID: 7714 RVA: 0x003EC900 File Offset: 0x003EC900
    private static bool StartsCall(string code, string name, int position)
    {
      if (position + name.Length + 1 > code.Length)
      {
        return false;
      }
      if (string.CompareOrdinal(code, position, name, 0, name.Length) != 0 || code[position + name.Length] != '(')
      {
        return false;
      }
      if (position > 0)
      {
        char c = code[position - 1];
        bool flag = char.IsLetterOrDigit(c);
        if (!flag)
        {
          bool flag2 = c == '.' || c == '_';
          flag = flag2;
        }
        if (flag)
        {
          return false;
        }
      }
      return true;
    }

    // Token: 0x06001E23 RID: 7715 RVA: 0x003EC978 File Offset: 0x003EC978
    [CompilerGenerated]
    internal static void <Scan>g__SkipSpaces|2_0(ref SayLines.<>c__DisplayClass2_0 A_0)
    {
      while (A_0.index < A_0.code.Length && char.IsWhiteSpace(A_0.code[A_0.index]))
      {
        int index = A_0.index;
        A_0.index = index + 1;
      }
    }
  }
}
