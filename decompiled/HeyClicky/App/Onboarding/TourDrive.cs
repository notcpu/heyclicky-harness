using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Windows.UI;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x02000132 RID: 306
  [NullableContext(2)]
  [Nullable(0)]
  public static class TourDrive
  {
    // Token: 0x04000752 RID: 1874
    [Nullable(new byte[] { 2, 1, 1, 1 })]
    public static Func<Color, FrameworkElement, Func<bool>, FrameworkElement> StepContent;

    // Token: 0x04000753 RID: 1875
    public static Action Run;

    // Token: 0x04000754 RID: 1876
    public static Func<bool> IsDriving;

    // Token: 0x04000755 RID: 1877
    public static Action Cancel;

    // Token: 0x04000756 RID: 1878
    public static Action Completed;
  }
}
