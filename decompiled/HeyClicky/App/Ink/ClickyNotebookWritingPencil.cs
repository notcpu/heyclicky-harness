using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001B9 RID: 441
  public sealed class ClickyNotebookWritingPencil : StackPanel
  {
    // Token: 0x06001760 RID: 5984 RVA: 0x005C0350 File Offset: 0x005C0350
    public ClickyNotebookWritingPencil(ClickyHomeSpreadPaper paper)
    {
      base.Orientation = Orientation.Horizontal;
      base.Spacing = 6.0;
      Grid holder = new Grid
      {
        Width = 18.0,
        Height = 18.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      TextBlock textBlock = ClickyInkIcon.Glyph("pencil", 15.0, paper.Ink.Opacity(0.75), true);
      holder.Children.Add(textBlock);
      base.Children.Add(holder);
      TextBlock textBlock2 = PaperText.Line("writing…", ClickyPaperFonts.Hand(19.0), paper.Quiet, TextAlignment.Left);
      textBlock2.VerticalAlignment = VerticalAlignment.Center;
      base.Children.Add(textBlock2);
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        holder.CenterPoint = new Vector3(9f, 9f, 0f);
        if (Anim.ReduceMotion || ClickyHomeRendering.IsOffscreen)
        {
          holder.Rotation = -6f;
          holder.Translation = new Vector3(0f, 1f, 0f);
          return;
        }
        Anim.LoopScalar(holder, "Rotation", 0.64, (double t) => (float)(-6.0 - 14.0 * ClickyNotebookWritingPencil.<.ctor>g__Phase|0_1(t)), 48, 0.0);
        Anim.LoopVector3(holder, "Translation", 0.64, (double t) => new Vector3(0f, (float)(1.0 - 3.0 * ClickyNotebookWritingPencil.<.ctor>g__Phase|0_1(t)), 0f), 48, 0.0);
      };
    }

    // Token: 0x06001761 RID: 5985 RVA: 0x005C0444 File Offset: 0x005C0444
    [CompilerGenerated]
    internal static double <.ctor>g__Phase|0_1(double t)
    {
      return (1.0 - Math.Cos(t * 3.141592653589793 / 0.32)) / 2.0;
    }
  }
}
