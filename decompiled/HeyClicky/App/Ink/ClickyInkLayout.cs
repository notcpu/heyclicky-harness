using System;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001BC RID: 444
  public static class ClickyInkLayout
  {
    // Token: 0x06001772 RID: 6002 RVA: 0x005C08AC File Offset: 0x005C08AC
    public static double ContentTopFor(bool carriesTitle, bool isContinuation)
    {
      if (carriesTitle)
      {
        return 92.0;
      }
      if (!isContinuation)
      {
        return 78.0;
      }
      return 60.0;
    }

    // Token: 0x04000B62 RID: 2914
    public static readonly Size PageSize = new Size(450f, 560f);

    // Token: 0x04000B63 RID: 2915
    public const double ContentLeft = 36.0;

    // Token: 0x04000B64 RID: 2916
    public const double WindowPreviewContentLeft = 22.0;

    // Token: 0x04000B65 RID: 2917
    public const double ContentTop = 78.0;

    // Token: 0x04000B66 RID: 2918
    public const double ContentWidth = 378.0;

    // Token: 0x04000B67 RID: 2919
    public const double ContentBottom = 26.0;
  }
}
