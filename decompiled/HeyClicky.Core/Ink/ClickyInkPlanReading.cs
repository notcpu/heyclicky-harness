using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200017A RID: 378
  [NullableContext(2)]
  [Nullable(0)]
  public static class ClickyInkPlanReading
  {
    // Token: 0x060016B9 RID: 5817 RVA: 0x003C0780 File Offset: 0x003C0780
    [NullableContext(1)]
    [return: TupleElementNames(new string[] { "Text", "IsDone" })]
    [return: Nullable(new byte[] { 2, 0, 1 })]
    public static IReadOnlyList<ValueTuple<string, bool>> ChecklistSteps(string noteText)
    {
      ClickyInkDocument.Block block = ClickyInkParser.Parse(noteText, false).AllBlocks.Select<ValueTuple<int, ClickyInkDocument.Block>, ClickyInkDocument.Block>(([TupleElementNames(new string[] { "Page", "Block" })] ValueTuple<int, ClickyInkDocument.Block> b) => b.Item2).FirstOrDefault<ClickyInkDocument.Block>((ClickyInkDocument.Block b) => b.Kind == ClickyInkDocument.Kind.Checklist);
      if (block == null)
      {
        return null;
      }
      List<ValueTuple<string, bool>> list = (from item in block.Items
        select new ValueTuple<string, bool>(item.Text.TrimWsNl(), item.IsChecked.GetValueOrDefault()) into i
        where i.Item1.Length > 0
        select i).ToList<ValueTuple<string, bool>>();
      if (list.Count != 0)
      {
        return list;
      }
      return null;
    }

    // Token: 0x060016BA RID: 5818 RVA: 0x003C0848 File Offset: 0x003C0848
    public static string LatestText(string notePath)
    {
      if (notePath != null && ClickyLiveNotePreferences.IsEnabled)
      {
        IReadOnlyList<ClickyInkNoteVersion> readOnlyList = ClickyInkNoteStore.Shared.Versions(notePath);
        if (readOnlyList != null && readOnlyList.Count > 0)
        {
          IReadOnlyList<ClickyInkNoteVersion> readOnlyList2 = readOnlyList;
          return readOnlyList2[readOnlyList2.Count - 1].Text;
        }
      }
      return null;
    }

    // Token: 0x060016BB RID: 5819 RVA: 0x003C088C File Offset: 0x003C088C
    public static string NoteTitle(string notePath)
    {
      string text = ClickyInkPlanReading.LatestText(notePath);
      if (text == null)
      {
        return null;
      }
      string text2 = (ClickyInkParser.Parse(text, false).Title ?? "").TrimWsNl();
      if (text2.Length != 0)
      {
        return text2;
      }
      return null;
    }
  }
}
