using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Drive
{
  // Token: 0x020002C6 RID: 710
  [NullableContext(2)]
  [Nullable(0)]
  public static class DriveNotchOwnership
  {
    // Token: 0x17000554 RID: 1364
    // (get) Token: 0x06001DF5 RID: 7669 RVA: 0x00604184 File Offset: 0x00604184
    // (set) Token: 0x06001DF6 RID: 7670 RVA: 0x0060418C File Offset: 0x0060418C
    public static Action<bool> OwnsNotchChanged { get; set; }

    // Token: 0x17000555 RID: 1365
    // (get) Token: 0x06001DF7 RID: 7671 RVA: 0x00604194 File Offset: 0x00604194
    public static bool OwnsNotch
    {
      get
      {
        return DriveNotchOwnership.consentShown || DriveNotchOwnership.drivingShown;
      }
    }

    // Token: 0x06001DF8 RID: 7672 RVA: 0x006041A4 File Offset: 0x006041A4
    public static void SetConsentShown(bool shown)
    {
      DriveNotchOwnership.Set(ref DriveNotchOwnership.consentShown, shown);
    }

    // Token: 0x06001DF9 RID: 7673 RVA: 0x006041B4 File Offset: 0x006041B4
    public static void SetDrivingShown(bool shown)
    {
      DriveNotchOwnership.Set(ref DriveNotchOwnership.drivingShown, shown);
    }

    // Token: 0x06001DFA RID: 7674 RVA: 0x006041C4 File Offset: 0x006041C4
    private static void Set(ref bool field, bool value)
    {
      bool ownsNotch = DriveNotchOwnership.OwnsNotch;
      field = value;
      if (DriveNotchOwnership.OwnsNotch != ownsNotch)
      {
        Action<bool> ownsNotchChanged = DriveNotchOwnership.OwnsNotchChanged;
        if (ownsNotchChanged == null)
        {
          return;
        }
        ownsNotchChanged(DriveNotchOwnership.OwnsNotch);
      }
    }

    // Token: 0x04000E61 RID: 3681
    private static bool consentShown;

    // Token: 0x04000E62 RID: 3682
    private static bool drivingShown;
  }
}
