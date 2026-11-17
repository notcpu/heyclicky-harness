using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000153 RID: 339
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkQuestionAnswerRules
  {
    // Token: 0x060014FD RID: 5373 RVA: 0x003B2BF8 File Offset: 0x003B2BF8
    public static List<string> Options(ClickyInkDocument.Block block)
    {
      return ClickyInkChoices.Of(block["options"] ?? block["choices"]).Take<string>(6).ToList<string>();
    }

    // Token: 0x060014FE RID: 5374 RVA: 0x003B2C24 File Offset: 0x003B2C24
    [return: Nullable(2)]
    public static string TypedPlaceholder(ClickyInkDocument.Block block)
    {
      string[] array = new string[] { "input", "other" };
      int i = 0;
      while (i < array.Length)
      {
        string text = array[i];
        string text2 = block[text];
        if (text2 != null)
        {
          if (text2.Length != 0)
          {
            return text2;
          }
          if (!(text == "other"))
          {
            return "Type your answer";
          }
          return "Something else…";
        }
        else
        {
          i++;
        }
      }
      return null;
    }

    // Token: 0x060014FF RID: 5375 RVA: 0x003B2C8C File Offset: 0x003B2C8C
    public static bool HasAnswers(ClickyInkDocument.Block block)
    {
      return new string[] { "options", "choices", "input", "other", "file" }.Any<string>((string key) => block[key] != null);
    }
  }
}
