using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.Core.Notch;

namespace HeyClicky.App.Services
{
  // Token: 0x020000DE RID: 222
  [NullableContext(1)]
  public interface IAppActions
  {
    // Token: 0x06000A87 RID: 2695
    void OpenSettings();

    // Token: 0x06000A88 RID: 2696
    void OpenSettings(NotchSettingsSection section);

    // Token: 0x06000A89 RID: 2697
    void OpenNotchHome();

    // Token: 0x06000A8A RID: 2698
    void StartTutorial();

    // Token: 0x06000A8B RID: 2699
    void ShowIntro();

    // Token: 0x06000A8C RID: 2700
    void Quit();

    // Token: 0x06000A8D RID: 2701
    void Relaunch();

    // Token: 0x06000A8E RID: 2702
    void ResetAllDataAndQuit();

    // Token: 0x06000A8F RID: 2703
    void SetPaused(bool paused);

    // Token: 0x06000A90 RID: 2704
    void OpenUrl(string url);

    // Token: 0x06000A91 RID: 2705
    void OnboardingCardAction(int step);

    // Token: 0x06000A92 RID: 2706
    void DismissOnboardingPin();

    // Token: 0x06000A93 RID: 2707
    void SignIn();

    // Token: 0x170001AC RID: 428
    // (get) Token: 0x06000A94 RID: 2708
    bool DevSignInAvailable { get; }

    // Token: 0x06000A95 RID: 2709
    Task<bool> DevSignInAsync();
  }
}
