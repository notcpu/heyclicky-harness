using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Brain
{
  // Token: 0x02000206 RID: 518
  [NullableContext(1)]
  [Nullable(0)]
  public static class DrawingDisplayChoices
  {
    // Token: 0x06001E24 RID: 7716 RVA: 0x003EC9C4 File Offset: 0x003EC9C4
    public static int Decide(string codeSoFar, bool isComplete, int displayCount)
    {
      string text = DrawingContinuationDecisions.FirstStatementText(codeSoFar);
      if (text.StartsWith("onScreen(", StringComparison.Ordinal))
      {
        string text2 = text;
        int length = "onScreen(".Length;
        string text3 = text2.Substring(length, text2.Length - length);
        int num = text3.IndexOf(')');
        if (num < 0)
        {
          if (!isComplete)
          {
            return -1;
          }
          return 0;
        }
        else
        {
          int num3;
          int num2 = (int.TryParse(text3.Substring(0, num), NumberStyles.Integer, CultureInfo.InvariantCulture, out num3) ? num3 : 1);
          if (num2 < 1 || num2 > displayCount)
          {
            return 0;
          }
          return num2 - 1;
        }
      }
      else
      {
        if (!isComplete && "onScreen(".StartsWith(text, StringComparison.Ordinal))
        {
          return -1;
        }
        return 0;
      }
    }

    // Token: 0x04000CEF RID: 3311
    private const string Marker = "onScreen(";
  }
}
