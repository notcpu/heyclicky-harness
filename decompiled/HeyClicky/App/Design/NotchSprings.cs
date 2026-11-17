using System;

namespace HeyClicky.App.Design
{
  // Token: 0x0200034C RID: 844
  public static class NotchSprings
  {
    // Token: 0x04001182 RID: 4482
    public static readonly SwiftSpring Open = new SwiftSpring(0.42, 0.82);

    // Token: 0x04001183 RID: 4483
    public static readonly SwiftSpring Close = new SwiftSpring(0.45, 1.0);

    // Token: 0x04001184 RID: 4484
    public static readonly SwiftSpring Content = new SwiftSpring(0.34, 0.86);

    // Token: 0x04001185 RID: 4485
    public static readonly SwiftSpring TextInputOpen = new SwiftSpring(0.2, 0.9);

    // Token: 0x04001186 RID: 4486
    public static readonly SwiftSpring Control = new SwiftSpring(0.18, 0.84);

    // Token: 0x04001187 RID: 4487
    public const float ContentTransitionScale = 0.92f;
  }
}
