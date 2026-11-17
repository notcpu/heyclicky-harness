using System;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000301 RID: 769
  public static class DeckFocus
  {
    // Token: 0x0600219D RID: 8605 RVA: 0x0061D360 File Offset: 0x0061D360
    public static int Clamped(int? focus, int count)
    {
      if (count <= 0)
      {
        return 0;
      }
      return Math.Min(Math.Max(focus ?? ((count - 1) / 2), 0), count - 1);
    }
  }
}
