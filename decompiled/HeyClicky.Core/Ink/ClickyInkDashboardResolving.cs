using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200013C RID: 316
  public static class ClickyInkDashboardResolving
  {
    // Token: 0x06001406 RID: 5126 RVA: 0x003AAEC0 File Offset: 0x003AAEC0
    [NullableContext(1)]
    public unsafe static ClickyInkLiveNote.Block DashboardResolved(this ClickyInkLiveNote.Block block, double clock)
    {
      ClickyInkLiveNote.Block block2 = block.Resolved(clock);
      ClickyInkDocument.Kind kind = block.Source.Kind;
      if (!ClickyInkDashboardKinds.DashboardKinds.Contains(kind) && kind != ClickyInkDocument.Kind.Figure && kind != ClickyInkDocument.Kind.Answer)
      {
        return block2;
      }
      int num = block.Stamps.FindLastIndex(([Nullable(new byte[] { 0, 1 })] ClickyInkLiveNote.Stamp<ClickyInkDocument.Block> s) => s.At <= clock);
      if (num <= 0)
      {
        return block2;
      }
      ClickyInkLiveNote.Stamp<ClickyInkDocument.Block> stamp = block.Stamps[num];
      ClickyInkLiveNote.Stamp<ClickyInkDocument.Block> stamp2 = block.Stamps[num - 1];
      ClickyInkDocument.Block block3 = block2.Source.Clone();
      bool flag = false;
      foreach (KeyValuePair<string, string> keyValuePair in stamp.Value.Attributes)
      {
        string text;
        string text2;
        keyValuePair.Deconstruct(out text, out text2);
        string text3 = text;
        string text4 = text2;
        if (!text3.HasPrefix("_"))
        {
          string text5 = stamp2.Value[text3];
          if (!(text5 == text4))
          {
            if (text5 != null)
            {
              block3.Attributes["_prev_" + text3] = text5;
            }
            flag = true;
          }
        }
      }
      if (!flag)
      {
        return block2;
      }
      block3.Attributes["_changed_at"] = InkText.DoubleDescription(stamp.At);
      int? edit = stamp.Edit;
      if (edit != null)
      {
        int valueOrDefault = edit.GetValueOrDefault();
        block3.Attributes["_changed_edit"] = valueOrDefault.ToString(CultureInfo.InvariantCulture);
      }
      ClickyInkLiveNote.Block block4 = block2;
      int num2 = 1;
      List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>> list = new List<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>>(num2);
      CollectionsMarshal.SetCount<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>>(list, num2);
      Span<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>> span = CollectionsMarshal.AsSpan<ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>>(list);
      int num3 = 0;
      *span[num3] = new ClickyInkLiveNote.Stamp<ClickyInkDocument.Block>(0.0, block3, null);
      block4.Stamps = list;
      return block2;
    }
  }
}
