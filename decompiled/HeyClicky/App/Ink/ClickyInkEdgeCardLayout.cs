using System;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001BD RID: 445
  public static class ClickyInkEdgeCardLayout
  {
    // Token: 0x1700042D RID: 1069
    // (get) Token: 0x06001774 RID: 6004 RVA: 0x005C08EC File Offset: 0x005C08EC
    public static Size SheetSize
    {
      get
      {
        return ClickyInkLayout.PageSize;
      }
    }

    // Token: 0x1700042E RID: 1070
    // (get) Token: 0x06001775 RID: 6005 RVA: 0x005C08F4 File Offset: 0x005C08F4
    public static Size Size
    {
      get
      {
        return ClickyInkEdgeCardLayout.SizeFor(ClickyInkEdgeCardLayout.SheetSize, 46.0);
      }
    }

    // Token: 0x06001776 RID: 6006 RVA: 0x005C090C File Offset: 0x005C090C
    public static Size SizeFor(Size sheet, double footerHeight = 46.0)
    {
      return new Size(sheet.Width + 12.0, 34.0 + sheet.Height + footerHeight + 12.0);
    }

    // Token: 0x06001777 RID: 6007 RVA: 0x005C0944 File Offset: 0x005C0944
    public static Size SheetFor(Size card, double footerHeight = 46.0)
    {
      return new Size(card.Width - 12.0, card.Height - 34.0 - footerHeight - 12.0);
    }

    // Token: 0x04000B68 RID: 2920
    public const double FooterHeight = 46.0;

    // Token: 0x04000B69 RID: 2921
    public const double TitleBarHeight = 34.0;

    // Token: 0x04000B6A RID: 2922
    public const double Bezel = 6.0;

    // Token: 0x04000B6B RID: 2923
    public const double ScreenCornerRadius = 12.0;

    // Token: 0x04000B6C RID: 2924
    public const double OpenedFooterHeight = 60.0;
  }
}
