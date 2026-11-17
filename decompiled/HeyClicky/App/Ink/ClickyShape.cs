using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000201 RID: 513
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyShape : IClickyShape
  {
    // Token: 0x0600188C RID: 6284 RVA: 0x005CCE5C File Offset: 0x005CCE5C
    public ClickyShape(Func<Rect, PaperPath> path)
    {
    }

    // Token: 0x0600188D RID: 6285 RVA: 0x005CCE6C File Offset: 0x005CCE6C
    public PaperPath PathIn(Rect rect)
    {
      return this.<path>P(rect);
    }

    // Token: 0x0600188E RID: 6286 RVA: 0x005CCE7C File Offset: 0x005CCE7C
    public static IClickyShape RoundedRectangle(double radius)
    {
      return new ClickyShape((Rect r) => new PaperPath().AddRoundedRect(r, radius));
    }

    // Token: 0x0600188F RID: 6287 RVA: 0x005CCE9C File Offset: 0x005CCE9C
    public static IClickyShape UnevenRoundedRectangle(double topLeading, double bottomLeading, double bottomTrailing, double topTrailing)
    {
      return new ClickyShape((Rect r) => new PaperPath().AddUnevenRoundedRect(r, topLeading, bottomLeading, bottomTrailing, topTrailing));
    }

    // Token: 0x04000C15 RID: 3093
    [CompilerGenerated]
    private Func<Rect, PaperPath> <path>P = path;

    // Token: 0x04000C16 RID: 3094
    public static readonly IClickyShape Rectangle = new ClickyShape((Rect r) => new PaperPath().AddRect(r));

    // Token: 0x04000C17 RID: 3095
    public static readonly IClickyShape Capsule = new ClickyShape((Rect r) => new PaperPath().AddCapsule(r));

    // Token: 0x04000C18 RID: 3096
    public static readonly IClickyShape Circle = new ClickyShape(delegate(Rect r)
    {
      double num = Math.Min(r.Width, r.Height);
      return new PaperPath().AddEllipse(new Rect(r.X + (r.Width - num) / 2.0, r.Y + (r.Height - num) / 2.0, num, num));
    });

    // Token: 0x04000C19 RID: 3097
    public static readonly IClickyShape Ellipse = new ClickyShape((Rect r) => new PaperPath().AddEllipse(r));
  }
}
