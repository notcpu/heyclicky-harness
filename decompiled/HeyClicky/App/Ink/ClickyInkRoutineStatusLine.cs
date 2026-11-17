using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000236 RID: 566
  public static class ClickyInkRoutineStatusLine
  {
    // Token: 0x060019B5 RID: 6581 RVA: 0x005D7154 File Offset: 0x005D7154
    [NullableContext(1)]
    public static FrameworkElement Create(ClickyInkRoutineControl routine, ClickyInkStyle style)
    {
      Color color = (routine.IsPaused ? PaperColor.Hex("#B4541A") : (routine.IsRunning ? style.Fresh : style.Quiet));
      return InkTextView.Plain(ClickyInkDashboardViewMath.RoutineStatusWords(routine, null), ClickyPaperFonts.Hand(15.0), color).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(1);
      });
    }
  }
}
