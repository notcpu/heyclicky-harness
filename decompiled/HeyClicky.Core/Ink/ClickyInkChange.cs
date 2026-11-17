using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200013D RID: 317
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkChange
  {
    // Token: 0x06001407 RID: 5127 RVA: 0x003AB0A4 File Offset: 0x003AB0A4
    [return: Nullable(2)]
    public static string Previous(ClickyInkDocument.Block block, string key)
    {
      return block["_prev_" + key];
    }

    // Token: 0x06001408 RID: 5128 RVA: 0x003AB0B8 File Offset: 0x003AB0B8
    public static double? ChangedAt(ClickyInkDocument.Block block)
    {
      return InkText.ParseDouble(block["_changed_at"]);
    }

    // Token: 0x06001409 RID: 5129 RVA: 0x003AB0CC File Offset: 0x003AB0CC
    public static int? ChangedEdit(ClickyInkDocument.Block block)
    {
      long? num = InkText.ParseInt(block["_changed_edit"]);
      if (num != null)
      {
        long valueOrDefault = num.GetValueOrDefault();
        return new int?((int)valueOrDefault);
      }
      return null;
    }

    // Token: 0x0600140A RID: 5130 RVA: 0x003AB110 File Offset: 0x003AB110
    public static bool IsLatest(ClickyInkDocument.Block block, int? latestEdit)
    {
      int? num = ClickyInkChange.ChangedEdit(block);
      if (num != null)
      {
        int valueOrDefault = num.GetValueOrDefault();
        int num2 = valueOrDefault;
        num = latestEdit;
        return (num2 == num.GetValueOrDefault()) & (num != null);
      }
      return false;
    }

    // Token: 0x0600140B RID: 5131 RVA: 0x003AB14C File Offset: 0x003AB14C
    public static bool Animates(ClickyInkDocument.Block block, int? latestEdit)
    {
      return ClickyInkChange.ChangedAt(block) != null && (ClickyInkChange.ChangedEdit(block) == null || ClickyInkChange.IsLatest(block, latestEdit));
    }

    // Token: 0x0600140C RID: 5132 RVA: 0x003AB184 File Offset: 0x003AB184
    public static double Progress(ClickyInkDocument.Block block, double clock, double delay = 0.0, double seconds = 0.9)
    {
      double? num = ClickyInkChange.ChangedAt(block);
      if (num != null)
      {
        double valueOrDefault = num.GetValueOrDefault();
        double num2 = Math.Min(1.0, Math.Max(0.0, (clock - valueOrDefault - delay) / seconds));
        return 1.0 - Math.Pow(1.0 - num2, 3.0);
      }
      return 1.0;
    }
  }
}
