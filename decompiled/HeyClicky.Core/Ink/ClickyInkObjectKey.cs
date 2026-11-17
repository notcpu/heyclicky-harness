using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000122 RID: 290
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkObjectKey
  {
    // Token: 0x06001301 RID: 4865 RVA: 0x003A4514 File Offset: 0x003A4514
    public static string Normalized(string text)
    {
      string text2 = ClickyInkParser.Emphasis.Strip(text).Lowered();
      string text3 = " ";
      string text4 = text2;
      Func<string, bool> func;
      if ((func = ClickyInkObjectKey.<>O.<0>__IsWhitespace) == null)
      {
        func = (ClickyInkObjectKey.<>O.<0>__IsWhitespace = new Func<string, bool>(InkText.IsWhitespace));
      }
      return string.Join(text3, text4.SplitCharacters(func)).TrimSet(" .,:;!?…\"'“”‘’");
    }

    // Token: 0x06001302 RID: 4866 RVA: 0x003A4564 File Offset: 0x003A4564
    public static string Of(ClickyInkDocument.Block block)
    {
      ClickyInkDocument.Kind kind = block.Kind;
      if (kind <= ClickyInkDocument.Kind.Ask)
      {
        if (kind != ClickyInkDocument.Kind.Letter)
        {
          if (kind == ClickyInkDocument.Kind.Ask)
          {
            string text = "ask|";
            string text2;
            if ((text2 = block["say"]) == null)
            {
              text2 = block["ask"] ?? block.Text;
            }
            return text + ClickyInkObjectKey.Normalized(text2);
          }
        }
        else
        {
          string text3 = block["to"] ?? "";
          string text4 = block["subject"] ?? "";
          if (text3.Length == 0 && text4.Length == 0)
          {
            return "letter|" + ClickyInkObjectKey.Normalized(block.Text.PrefixCharacters(80));
          }
          return "letter|" + ClickyInkObjectKey.Normalized(text3) + "|" + ClickyInkObjectKey.Normalized(text4);
        }
      }
      else
      {
        if (kind == ClickyInkDocument.Kind.Question)
        {
          return "question|" + ClickyInkObjectKey.Normalized(block.Text);
        }
        if (kind == ClickyInkDocument.Kind.Review)
        {
          string text5 = ClickyInkReviewRules.Label(block);
          if (text5.Length > 0)
          {
            return "review|" + ClickyInkObjectKey.Normalized(text5);
          }
          return "review|" + string.Join("|", from i in block.Items.Take<ClickyInkDocument.Item>(3)
            select ClickyInkObjectKey.Normalized(i.Text));
        }
      }
      return block.Kind.RawValue() + "|" + ClickyInkObjectKey.Normalized(block.Text);
    }

    // Token: 0x02000493 RID: 1171
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001D5A RID: 7514
      [Nullable(new byte[] { 0, 1 })]
      public static Func<string, bool> <0>__IsWhitespace;
    }
  }
}
