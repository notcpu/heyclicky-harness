using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Brain
{
  // Token: 0x02000207 RID: 519
  [NullableContext(1)]
  [Nullable(0)]
  public static class DrawingContinuationDecisions
  {
    // Token: 0x06001E25 RID: 7717 RVA: 0x003ECA5C File Offset: 0x003ECA5C
    public static DrawingContinuationDecision Decide(string codeSoFar, bool isComplete)
    {
      string text = DrawingContinuationDecisions.FirstStatementText(codeSoFar);
      if (text.StartsWith("startOver(", StringComparison.Ordinal))
      {
        return DrawingContinuationDecision.StartOver;
      }
      if (!isComplete && "startOver(".StartsWith(text, StringComparison.Ordinal))
      {
        return DrawingContinuationDecision.Undecided;
      }
      return DrawingContinuationDecision.BuildOn;
    }

    // Token: 0x06001E26 RID: 7718 RVA: 0x003ECA94 File Offset: 0x003ECA94
    internal unsafe static string FirstStatementText(string code)
    {
      ReadOnlySpan<char> readOnlySpan = code.AsSpan();
      for (;;)
      {
        int num = 0;
        while (num < readOnlySpan.Length && (char.IsWhiteSpace((char)(*readOnlySpan[num])) || *readOnlySpan[num] == 59))
        {
          num++;
        }
        ref ReadOnlySpan<char> ptr = ref readOnlySpan;
        int num2 = num;
        readOnlySpan = ptr.Slice(num2, ptr.Length - num2);
        if (readOnlySpan.StartsWith<char>("//"))
        {
          int num3 = readOnlySpan.IndexOf('\n');
          if (num3 < 0)
          {
            break;
          }
          ptr = ref readOnlySpan;
          num2 = num3;
          readOnlySpan = ptr.Slice(num2, ptr.Length - num2);
        }
        else
        {
          if (!readOnlySpan.StartsWith<char>("/*"))
          {
            goto IL_00D9;
          }
          int num4 = readOnlySpan.IndexOf<char>("*/");
          if (num4 < 0)
          {
            goto Block_6;
          }
          ptr = ref readOnlySpan;
          num2 = num4 + 2;
          readOnlySpan = ptr.Slice(num2, ptr.Length - num2);
        }
      }
      return "";
      Block_6:
      return "";
      IL_00D9:
      return readOnlySpan.Slice(0, Math.Min(24, readOnlySpan.Length)).ToString().Replace(" ", "");
    }

    // Token: 0x04000CF0 RID: 3312
    private const string StartOverMarker = "startOver(";
  }
}
