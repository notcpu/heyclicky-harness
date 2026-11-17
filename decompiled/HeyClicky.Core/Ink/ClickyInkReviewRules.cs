using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000126 RID: 294
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkReviewRules
  {
    // Token: 0x06001342 RID: 4930 RVA: 0x003A60BC File Offset: 0x003A60BC
    public static string Label(ClickyInkDocument.Block block)
    {
      return ClickyInkNoteActions.Plain(block.Text);
    }

    // Token: 0x06001343 RID: 4931 RVA: 0x003A60CC File Offset: 0x003A60CC
    public static string GoInstruction(ClickyInkDocument.Block block)
    {
      string text;
      if ((text = block["go"]) == null && (text = block["do"]) == null)
      {
        text = block["send"] ?? "";
      }
      string text2 = ClickyInkNoteActions.Plain(text).TrimSet(".:;!");
      if (text2.Length != 0)
      {
        return text2;
      }
      return "Go ahead with these";
    }

    // Token: 0x06001344 RID: 4932 RVA: 0x003A612C File Offset: 0x003A612C
    public static string GoLabel(ClickyInkDocument.Block block, int count)
    {
      string text;
      if ((text = block["go_label"]) == null)
      {
        text = block["label"] ?? "";
      }
      string text2 = ClickyInkNoteActions.Plain(text);
      if (count <= 0)
      {
        if (text2.Length != 0)
        {
          return text2;
        }
        return "Go ahead";
      }
      else
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
        if (text2.Length != 0)
        {
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
          defaultInterpolatedStringHandler.AppendFormatted(text2);
          defaultInterpolatedStringHandler.AppendLiteral(" ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(count);
          return defaultInterpolatedStringHandler.ToStringAndClear();
        }
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(14, 1);
        defaultInterpolatedStringHandler.AppendLiteral("Go ahead with ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(count);
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
    }

    // Token: 0x06001345 RID: 4933 RVA: 0x003A61D0 File Offset: 0x003A61D0
    public static bool IsOpen(ClickyInkDocument.Item item)
    {
      return item.Mark.GetValueOrDefault() != ClickyInkDocument.Mark.Done && item.Mark.GetValueOrDefault() != ClickyInkDocument.Mark.Gone;
    }

    // Token: 0x06001346 RID: 4934 RVA: 0x003A6204 File Offset: 0x003A6204
    public static string Action(string itemText)
    {
      string text = ClickyInkNoteActions.Plain(itemText);
      foreach (string text2 in new string[] { " — ", " – " })
      {
        int num = text.Find(text2);
        if (num >= 0)
        {
          return text.Substring(0, num).TrimWs();
        }
      }
      return text;
    }

    // Token: 0x06001347 RID: 4935 RVA: 0x003A6260 File Offset: 0x003A6260
    public static List<string> ItemsToSend(ClickyInkDocument.Block block, IEnumerable<string> alreadySent)
    {
      ClickyInkReviewRules.<>c__DisplayClass5_0 CS$<>8__locals1 = new ClickyInkReviewRules.<>c__DisplayClass5_0();
      ClickyInkReviewRules.<>c__DisplayClass5_0 CS$<>8__locals2 = CS$<>8__locals1;
      Func<string, string> func;
      if ((func = ClickyInkReviewRules.<>O.<0>__Normalized) == null)
      {
        func = (ClickyInkReviewRules.<>O.<0>__Normalized = new Func<string, string>(ClickyInkObjectKey.Normalized));
      }
      CS$<>8__locals2.sent = alreadySent.Select<string, string>(func).ToHashSet<string>();
      return (from i in block.Items
        where i.IsChecked.GetValueOrDefault() && ClickyInkReviewRules.IsOpen(i)
        select ClickyInkReviewRules.Action(i.Text) into a
        where !CS$<>8__locals1.sent.Contains(ClickyInkObjectKey.Normalized(a))
        select a).ToList<string>();
    }

    // Token: 0x06001348 RID: 4936 RVA: 0x003A6304 File Offset: 0x003A6304
    public static bool WasSent(ClickyInkDocument.Item item, IEnumerable<string> alreadySent)
    {
      Func<string, string> func;
      if ((func = ClickyInkReviewRules.<>O.<0>__Normalized) == null)
      {
        func = (ClickyInkReviewRules.<>O.<0>__Normalized = new Func<string, string>(ClickyInkObjectKey.Normalized));
      }
      return alreadySent.Select<string, string>(func).ToHashSet<string>().Contains(ClickyInkObjectKey.Normalized(ClickyInkReviewRules.Action(item.Text)));
    }

    // Token: 0x0200049A RID: 1178
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001D6B RID: 7531
      [Nullable(0)]
      public static Func<string, string> <0>__Normalized;
    }
  }
}
