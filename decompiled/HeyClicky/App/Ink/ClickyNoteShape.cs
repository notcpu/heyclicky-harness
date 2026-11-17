using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001C1 RID: 449
  public sealed class ClickyNoteShape : IClickyShape
  {
    // Token: 0x060017B0 RID: 6064 RVA: 0x005C4738 File Offset: 0x005C4738
    public ClickyNoteShape(bool isPerforated = true, double biteSpacing = 10.0, double biteRadius = 2.7, double biteCenterOffset = 0.8, double bottomRadius = 3.0)
    {
    }

    // Token: 0x060017B1 RID: 6065 RVA: 0x005C4768 File Offset: 0x005C4768
    [NullableContext(1)]
    public PaperPath PathIn(Rect rect)
    {
      PaperPath paperPath = new PaperPath();
      if (!this.<isPerforated>P)
      {
        return paperPath.AddRoundedRect(rect, this.<bottomRadius>P);
      }
      double num = Math.Sqrt(Math.Max(0.0, this.<biteRadius>P * this.<biteRadius>P - this.<biteCenterOffset>P * this.<biteCenterOffset>P));
      double num2 = Math.Asin(this.<biteCenterOffset>P / this.<biteRadius>P);
      paperPath.MoveTo(rect.Left, rect.Top);
      double num3 = rect.Left + this.<biteSpacing>P / 2.0;
      while (num3 + num <= rect.Right)
      {
        paperPath.LineTo(num3 - num, rect.Top);
        paperPath.AddArc(new Point(num3, rect.Top - this.<biteCenterOffset>P), this.<biteRadius>P, 3.141592653589793 - num2, num2, true);
        num3 += this.<biteSpacing>P;
      }
      paperPath.LineTo(rect.Right, rect.Top);
      paperPath.LineTo(rect.Right, rect.Bottom - this.<bottomRadius>P);
      paperPath.QuadTo(new Point(rect.Right - this.<bottomRadius>P, rect.Bottom), new Point(rect.Right, rect.Bottom));
      paperPath.LineTo(rect.Left + this.<bottomRadius>P, rect.Bottom);
      paperPath.QuadTo(new Point(rect.Left, rect.Bottom - this.<bottomRadius>P), new Point(rect.Left, rect.Bottom));
      return paperPath.Close();
    }

    // Token: 0x04000B81 RID: 2945
    [CompilerGenerated]
    private bool <isPerforated>P = isPerforated;

    // Token: 0x04000B82 RID: 2946
    [CompilerGenerated]
    private double <biteSpacing>P = biteSpacing;

    // Token: 0x04000B83 RID: 2947
    [CompilerGenerated]
    private double <biteRadius>P = biteRadius;

    // Token: 0x04000B84 RID: 2948
    [CompilerGenerated]
    private double <biteCenterOffset>P = biteCenterOffset;

    // Token: 0x04000B85 RID: 2949
    [CompilerGenerated]
    private double <bottomRadius>P = bottomRadius;
  }
}
