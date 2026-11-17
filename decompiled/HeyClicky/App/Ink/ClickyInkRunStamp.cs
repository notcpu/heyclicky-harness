using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000235 RID: 565
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkRunStamp
  {
    // Token: 0x060019B2 RID: 6578 RVA: 0x005D70B4 File Offset: 0x005D70B4
    internal static void SetEditing(string blockID, bool isEditing)
    {
      if (isEditing)
      {
        ClickyInkRunStamp.editing.Add(blockID);
        return;
      }
      ClickyInkRunStamp.editing.Remove(blockID);
    }

    // Token: 0x060019B3 RID: 6579 RVA: 0x005D70D4 File Offset: 0x005D70D4
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, double width, ClickyInkActions actions)
    {
      ClickyInkRunStamp.<>c__DisplayClass2_0 CS$<>8__locals1 = new ClickyInkRunStamp.<>c__DisplayClass2_0();
      CS$<>8__locals1.block = block;
      CS$<>8__locals1.actions = actions;
      CS$<>8__locals1.style = style;
      CS$<>8__locals1.clock = clock;
      CS$<>8__locals1.isWindowPreview = ClickyInkEnvironment.Current.IsWindowPreview;
      CS$<>8__locals1.host = new Grid
      {
        Width = Math.Max(0.0, width),
        HorizontalAlignment = HorizontalAlignment.Left
      };
      CS$<>8__locals1.<Create>g__Build|0(false);
      return CS$<>8__locals1.host;
    }

    // Token: 0x04000C6A RID: 3178
    private static readonly HashSet<string> editing = new HashSet<string>();
  }
}
