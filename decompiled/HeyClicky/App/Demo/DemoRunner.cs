using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.Agents.Backend;
using HeyClicky.App.Billing;
using HeyClicky.App.DevFakes;
using HeyClicky.App.Drive;
using HeyClicky.App.Notch;
using HeyClicky.App.Notch.Surfaces;
using HeyClicky.App.Onboarding;
using HeyClicky.App.Settings;
using HeyClicky.App.UI;
using HeyClicky.Core.Billing;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Demo
{
  // Token: 0x02000351 RID: 849
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DemoRunner
  {
    // Token: 0x06002425 RID: 9253 RVA: 0x006311F0 File Offset: 0x006311F0
    public DemoRunner(AppHost host)
    {
      this.host = host;
      double num;
      if (double.TryParse(Environment.GetEnvironmentVariable("HEYCLICKY_DEMO_DWELL"), CultureInfo.InvariantCulture, out num))
      {
        DemoRunner.DwellSeconds = num;
      }
      this.BuildNotchSteps();
      this.BuildWindowSteps();
    }

    // Token: 0x1700069A RID: 1690
    // (get) Token: 0x06002426 RID: 9254 RVA: 0x0063124C File Offset: 0x0063124C
    private INotchPresenter P
    {
      get
      {
        return this.host.Services.Presenter;
      }
    }

    // Token: 0x06002427 RID: 9255 RVA: 0x00631260 File Offset: 0x00631260
    private void Live(NotchLiveEvent e)
    {
      this.P.ShowLiveEvent(e, null);
    }

    // Token: 0x06002428 RID: 9256 RVA: 0x00631284 File Offset: 0x00631284
    private void Add(string name, Action show, double seconds = 0.0, [Nullable(2)] Func<UIElement> snap = null)
    {
      this.steps.Add(new ValueTuple<string, Action, double, Func<UIElement>>(name, show, (seconds <= 0.0) ? DemoRunner.DwellSeconds : seconds, snap));
    }

    // Token: 0x06002429 RID: 9257 RVA: 0x006312B0 File Offset: 0x006312B0
    private void BuildNotchSteps()
    {
      this.Add("idle", delegate
      {
        this.P.ReturnToIdle();
      }, 0.0, null);
      this.Add("listening", delegate
      {
        this.Live(new NotchLiveEvent.ActivityListening());
      }, 0.0, null);
      this.Add("thinking", delegate
      {
        this.Live(new NotchLiveEvent.ActivityThinking());
      }, 0.0, null);
      this.Add("speaking", delegate
      {
        this.Live(new NotchLiveEvent.ActivitySpeaking());
      }, 0.0, null);
      this.Add("custom", delegate
      {
        this.Live(new NotchLiveEvent.ActivityCustomStatus("Searching the web"));
      }, 0.0, null);
      this.Add("custom-long", delegate
      {
        this.Live(new NotchLiveEvent.ActivityCustomStatus("Pulling up places near Union Square, San Francisco"));
      }, 0.0, null);
      this.Add("agent-multi", delegate
      {
        this.SeedAgents();
        this.Live(new NotchLiveEvent.AgentMultiActivity("Research flights to Tokyo", 2));
      }, 0.0, null);
      this.Add("agent-task", delegate
      {
        this.Live(new NotchLiveEvent.AgentTask("demo-1", "Research flights to Tokyo", "running"));
      }, 0.0, null);
      this.Add("custom-agent", delegate
      {
        this.SeedAgents();
        this.Live(new NotchLiveEvent.ActivityCustomStatus("Searching the web"));
      }, 0.0, null);
      this.Add("dictation-recording", delegate
      {
        this.Live(new NotchLiveEvent.Dictation("Dictating", true));
      }, 0.0, null);
      this.Add("dictation-transcribing", delegate
      {
        this.Live(new NotchLiveEvent.Dictation("Transcribing", false));
      }, 0.0, null);
      this.Add("resting-mark", delegate
      {
        this.Live(new NotchLiveEvent.RestingMark());
      }, 0.0, null);
      this.Add("hatch-peek", delegate
      {
        this.Live(new NotchLiveEvent.IntroHatchPeek());
      }, 0.0, null);
      this.Add("integration-suggestion", delegate
      {
        this.Live(new NotchLiveEvent.IntegrationSuggestion("gmail", "Gmail"));
      }, 0.0, null);
      this.Add("integration-connected", delegate
      {
        this.Live(new NotchLiveEvent.IntegrationConnected("github", null, null));
      }, 0.0, null);
      this.Add("meeting", delegate
      {
        this.Live(new NotchLiveEvent.MeetingCountdown("Design review", DateTimeOffset.Now.AddSeconds(59.0), new Uri("https://meet.google.com/abc-defg-hij"), null));
      }, 0.0, null);
      this.Add("mic-trouble", delegate
      {
        this.Live(new NotchLiveEvent.MicrophoneTrouble("USB Microphone isn't sending audio", null, true));
      }, 0.0, null);
      this.Add("muted", delegate
      {
        this.Live(new NotchLiveEvent.VoiceReplyMutedCopiedToClipboard("zero_volume"));
      }, 0.0, null);
      this.Add("muted-unmute", delegate
      {
        this.Live(new NotchLiveEvent.VoiceReplyMutedCopiedToClipboard("muted"));
      }, 0.0, null);
      this.Add("muted-came-back", delegate
      {
        this.Live(new NotchLiveEvent.VoiceReplyMutedCopiedToClipboard("sound_came_back"));
      }, 0.0, null);
      this.Add("dictation-clipboard", delegate
      {
        this.Live(new NotchLiveEvent.DictationCopiedToClipboard());
      }, 0.0, null);
      this.Add("app-updated", delegate
      {
        this.Live(new NotchLiveEvent.AppUpdated("0.1.0"));
      }, 0.0, null);
      this.Add("clicky-update", delegate
      {
        this.Live(new NotchLiveEvent.ClickyUpdate("research-buddy", "done"));
      }, 0.0, null);
      this.Add("tutorial-invite", delegate
      {
        this.Live(new NotchLiveEvent.OnboardingTutorialInvite());
      }, 0.0, null);
      this.Add("tutorial-resume", delegate
      {
        this.Live(new NotchLiveEvent.OnboardingTutorialResume());
      }, 0.0, null);
      this.Add("morning", delegate
      {
        this.Live(new NotchLiveEvent.MorningSuggestions(3));
      }, 0.0, null);
      this.Add("agent-peek", delegate
      {
        this.SeedAgents();
        this.Live(new NotchLiveEvent.AgentPeek(0.0));
      }, 0.0, null);
      this.Add("handoff-queued", delegate
      {
        this.Live(new NotchLiveEvent.HandoffQueued(2));
      }, 0.0, null);
      this.Add("handoff-sent", delegate
      {
        this.Live(new NotchLiveEvent.HandoffSent("Terminal"));
      }, 0.0, null);
      this.Add("sidecar-suggestion", delegate
      {
        this.Live(new NotchLiveEvent.SidecarLearningSuggestion("Blender", null));
      }, 0.0, null);
      this.Add("spotify-tip", delegate
      {
        this.Live(new NotchLiveEvent.SpotifyVoiceTip());
      }, 0.0, null);
      this.Add("dictionary-learned", delegate
      {
        this.Live(new NotchLiveEvent.DictionaryWordLearned("HeyClicky"));
      }, 0.0, null);
      this.Add("document-access", delegate
      {
        this.Live(new NotchLiveEvent.DocumentFolderAccessDenied("Allow access to your Documents folder", false, null));
      }, 0.0, null);
      this.Add("document-access-repeat", delegate
      {
        this.Live(new NotchLiveEvent.DocumentFolderAccessDenied("Allow access to your Documents folder", true, null));
      }, 0.0, null);
      this.Add("skill-pinned", delegate
      {
        this.Live(new NotchLiveEvent.SkillPinned(3, new string[] { "Figma Pro", "Notion", "Excel Wizard" }));
      }, 0.0, null);
      this.Add("intro-skill-ping", delegate
      {
        this.Live(new NotchLiveEvent.IntroSkillPing(0, true, 0.0, null));
      }, 0.0, null);
      this.Add("intro-skill-ping-grown", delegate
      {
        int token = 0;
        int i = token + 1;
        token = i;
        this.Live(new NotchLiveEvent.IntroSkillPing(i, true, 0.09, null));
        DispatcherQueue forCurrentThread = DispatcherQueue.GetForCurrentThread();
        ValueTuple<double, double>[] array = new ValueTuple<double, double>[]
        {
          new ValueTuple<double, double>(0.25, 0.18),
          new ValueTuple<double, double>(0.5, 0.27)
        };
        for (i = 0; i < array.Length; i++)
        {
          ValueTuple<double, double> valueTuple = array[i];
          double item = valueTuple.Item1;
          double item2 = valueTuple.Item2;
          double b = item2;
          Once.After(item, delegate
          {
            DemoRunner <>4__this = this;
            int num = token + 1;
            token = num;
            <>4__this.Live(new NotchLiveEvent.IntroSkillPing(num, true, b, null));
          }, forCurrentThread);
        }
      }, 0.0, null);
      this.Add("text-input", delegate
      {
        this.P.Present(new NotchPresentation.TextInput("", null));
      }, 0.0, null);
      this.Add("text-input-3-lines", delegate
      {
        this.P.Present(new NotchPresentation.TextInput("Can you look at the chart on my screen and tell me\nwhy revenue dipped in March?\nAlso suggest two fixes.", null));
      }, 0.0, null);
      this.Add("text-input-attachments", delegate
      {
        this.P.Present(new NotchPresentation.TextInput("Compare these", null));
        Once.After(0.2, delegate
        {
          TextInputSurface textInputSurface = this.host.NotchUi.ActiveSurfaceForTest as TextInputSurface;
          if (textInputSurface != null)
          {
            textInputSurface.Composer.AddAttachments(DemoRunner.DemoAttachments());
          }
        }, DispatcherQueue.GetForCurrentThread());
      }, 0.0, null);
      this.Add("text-input-drop", delegate
      {
        this.P.Present(new NotchPresentation.TextInput("", null));
        Once.After(0.2, delegate
        {
          NotchSurface activeSurfaceForTest = this.host.NotchUi.ActiveSurfaceForTest;
          if (activeSurfaceForTest == null)
          {
            return;
          }
          activeSurfaceForTest.SetDropTargeting(true);
        }, DispatcherQueue.GetForCurrentThread());
      }, 0.0, null);
      this.Add("text-thinking", delegate
      {
        this.P.Present(new NotchPresentation.TextResponse("why did revenue dip?", "", true));
      }, 0.0, null);
      this.Add("text-response", delegate
      {
        this.P.Present(new NotchPresentation.TextResponse("why did revenue dip?", "Here's what I see on your screen:\n\n1. The **notch** sits top-center and grows on a spring.\n2. Everything outside it stays *click-through*.\n\nTry `Ctrl+Alt` to talk, or double-tap **Ctrl** to type. More in the [docs](https://heyclicky.com).", false));
      }, 0.0, null);
      this.Add("onboarding-welcome", delegate
      {
        this.P.Present(new NotchPresentation.OnboardingPinned(0));
      }, 0.0, null);
      this.Add("onboarding-welcome-granted", delegate
      {
        FakePermissionService fakePermissionService = this.host.Services.Permissions as FakePermissionService;
        if (fakePermissionService != null)
        {
          fakePermissionService.Set(PermissionKind.Microphone, PermissionStatus.Granted);
          fakePermissionService.Set(PermissionKind.ScreenCapture, PermissionStatus.Granted);
        }
        this.P.Present(new NotchPresentation.OnboardingPinned(0));
      }, 0.0, null);
      this.Add("onboarding-welcome-micoff", delegate
      {
        FakePermissionService fakePermissionService2 = this.host.Services.Permissions as FakePermissionService;
        if (fakePermissionService2 != null)
        {
          fakePermissionService2.MicrophoneBlockedBy = MicrophoneBlock.DesktopApps;
          fakePermissionService2.Set(PermissionKind.ScreenCapture, PermissionStatus.Granted);
          fakePermissionService2.Set(PermissionKind.Microphone, PermissionStatus.Denied);
        }
        this.P.Present(new NotchPresentation.OnboardingPinned(0));
      }, 0.0, null);
      this.Add("onboarding-mic", delegate
      {
        this.P.Present(new NotchPresentation.OnboardingPinned(1));
      }, 0.0, null);
      this.Add("onboarding-screen", delegate
      {
        this.P.Present(new NotchPresentation.OnboardingPinned(2));
      }, 0.0, null);
      this.Add("onboarding-allset", delegate
      {
        this.P.Present(new NotchPresentation.OnboardingPinned(3));
      }, 0.0, null);
      this.Add("onboarding-signedout", delegate
      {
        this.P.Present(new NotchPresentation.OnboardingPinned(-1));
      }, 0.0, null);
      this.Add("idle-end", delegate
      {
        this.P.ReturnToIdle();
      }, 0.0, null);
    }

    // Token: 0x0600242A RID: 9258 RVA: 0x00631950 File Offset: 0x00631950
    private void SeedAgents()
    {
      FakeAgentService fakeAgentService = this.host.Services.Agents as FakeAgentService;
      if (fakeAgentService != null && fakeAgentService.Tasks.Count == 0)
      {
        fakeAgentService.Seed(new AgentTaskSnapshot[]
        {
          new AgentTaskSnapshot("demo-1", null, "Research flights to Tokyo", AgentTaskStatus.Running, "Comparing three airlines", null, DateTimeOffset.Now),
          new AgentTaskSnapshot("demo-2", null, "Draft the launch email", AgentTaskStatus.Running, "Writing the subject line", null, DateTimeOffset.Now),
          new AgentTaskSnapshot("demo-3", null, "Summarize Q3 metrics", AgentTaskStatus.Finished, "Done. Summary ready.", "## Q3\n\nRevenue grew **12%**.", DateTimeOffset.Now)
        });
      }
    }

    // Token: 0x0600242B RID: 9259 RVA: 0x006319F0 File Offset: 0x006319F0
    private static IEnumerable<string> DemoAttachments()
    {
      return new DemoRunner.<DemoAttachments>d__11(-2);
    }

    // Token: 0x0600242C RID: 9260 RVA: 0x006319FC File Offset: 0x006319FC
    private void BuildWindowSteps()
    {
      IntroWindow.Step[] values = Enum.GetValues<IntroWindow.Step>();
      Func<UIElement> <>9__14;
      for (int i = 0; i < values.Length; i++)
      {
        IntroWindow.Step step3 = values[i];
        IntroWindow.Step step5 = step3;
        IntroWindow shownIntro = null;
        string text = "intro-" + DemoRunner.Kebab(step5.ToString());
        string text2 = text;
        Action action = delegate
        {
          this.host.HideAuxWindows();
          this.P.ReturnToIdle();
          IntroWindow introWindow = (shownIntro = this.host.DemoIntro(step5));
          if (step5 == IntroWindow.Step.Hatched)
          {
            introWindow.SettleHatched();
          }
        };
        double num = ((step5 == IntroWindow.Step.Hatched) ? 4.0 : 0.0);
        Func<UIElement> func;
        if ((func = <>9__14) == null)
        {
          func = (<>9__14 = () => this.host.DemoIntroRoot());
        }
        this.Add(text2, action, num, func);
        this.windowSnaps[text] = new ValueTuple<Func<IntPtr>, double>(delegate
        {
          IntroWindow shownIntro2 = shownIntro;
          if (shownIntro2 == null)
          {
            return (IntPtr)0;
          }
          return shownIntro2.Window.Hwnd;
        }, 1.2);
      }
      TutorialWindow.Step[] values2 = Enum.GetValues<TutorialWindow.Step>();
      Func<UIElement> <>9__17;
      for (int i = 0; i < values2.Length; i++)
      {
        TutorialWindow.Step step2 = values2[i];
        TutorialWindow.Step step = step2;
        TutorialWindow shownTour = null;
        string text3 = "tutorial-" + DemoRunner.Kebab(step.ToString());
        string text4 = text3;
        Action action2 = delegate
        {
          this.host.HideAuxWindows();
          TutorialWindow tutorialWindow = (shownTour = this.host.DemoTutorial(step));
          TutorialWindow.Step step4 = step;
          bool flag = step4 == TutorialWindow.Step.VoiceHello || step4 == TutorialWindow.Step.DrawDemo;
          if (flag)
          {
            tutorialWindow.MarkCompleted(step);
          }
        };
        double num2 = ((step == TutorialWindow.Step.DrawToAsk) ? 3.0 : 0.0);
        Func<UIElement> func2;
        if ((func2 = <>9__17) == null)
        {
          func2 = (<>9__17 = () => this.host.DemoTutorialRoot());
        }
        this.Add(text4, action2, num2, func2);
        this.windowSnaps[text3] = new ValueTuple<Func<IntPtr>, double>(delegate
        {
          TutorialWindow shownTour2 = shownTour;
          if (shownTour2 == null)
          {
            return (IntPtr)0;
          }
          return shownTour2.Window.Hwnd;
        }, (step == TutorialWindow.Step.DrawToAsk) ? 2.5 : 1.2);
      }
      this.AddTourPanelSteps();
      this.Add("cursor-video", delegate
      {
        this.host.HideAuxWindows();
        string text6 = Path.Combine(AppContext.BaseDirectory, "Assets", "Video", "onboarding-intro-v2-windows.mp4");
        this.host.CursorCompanion.PlayVideo(text6, Array.Empty<CursorCompanion.Cue>(), delegate
        {
        }, 2.0, 60.0, 5.0, false);
      }, 5.0, () => this.host.CursorCompanion.WindowForSnapshot.Root);
      this.windowSnaps["cursor-video"] = new ValueTuple<Func<IntPtr>, double>(() => this.host.CursorCompanion.WindowForSnapshot.Hwnd, 3.5);
      this.Add("cursor-video-end", delegate
      {
        this.host.CursorCompanion.DismissVideo();
      }, 0.0, null);
      this.AddPaywallSteps();
      this.Add("paywall-end", delegate
      {
        PaywallWindow paywallWindow = this.demoPaywall;
        if (paywallWindow == null)
        {
          return;
        }
        paywallWindow.Dismiss();
      }, 0.0, null);
      SettingsContent.DemoCatalog = DemoRunner.DemoIntegrationCatalog();
      SettingsWindow.Page[] values3 = Enum.GetValues<SettingsWindow.Page>();
      Func<UIElement> <>9__21;
      for (int i = 0; i < values3.Length; i++)
      {
        SettingsWindow.Page page = values3[i];
        SettingsWindow.Page pg = page;
        string text5 = "settings-" + DemoRunner.Kebab(pg.ToString());
        Action action3 = delegate
        {
          this.host.HideAuxWindows();
          SettingsWindow settingsWindow = this.host.DemoSettings(pg);
          if (pg == SettingsWindow.Page.Referral)
          {
            settingsWindow.Content.SetReferralForTests(ReferralFixture.ReferralBody(false));
          }
        };
        double num3 = 0.0;
        Func<UIElement> func3;
        if ((func3 = <>9__21) == null)
        {
          func3 = (<>9__21 = () => this.host.DemoSettingsRoot());
        }
        this.Add(text5, action3, num3, func3);
      }
      this.Add("settings-integrations-connector", delegate
      {
        this.host.HideAuxWindows();
        this.host.DemoSettings(SettingsWindow.Page.Integrations).Content.DemoOpenConnectorEditor();
      }, 0.0, () => this.host.DemoSettingsRoot());
      this.Add("settings-referral-earned", delegate
      {
        this.host.HideAuxWindows();
        this.host.DemoSettings(SettingsWindow.Page.Referral).Content.SetReferralForTests(ReferralFixture.ReferralBody(true));
      }, 0.0, () => this.host.DemoSettingsRoot());
      PortraitGallery gallery = null;
      this.Add("portraits", delegate
      {
        this.host.HideAuxWindows();
        if (gallery == null)
        {
          gallery = new PortraitGallery(!this.host.Options.AllowCapture);
        }
        gallery.Window.ShowCentered();
      }, 3.0, delegate
      {
        PortraitGallery gallery3 = gallery;
        if (gallery3 == null)
        {
          return null;
        }
        return gallery3.Window.Root;
      });
      this.Add("windows-end", delegate
      {
        this.host.HideAuxWindows();
        PortraitGallery gallery2 = gallery;
        if (gallery2 == null)
        {
          return;
        }
        gallery2.Window.HideWindow();
      }, 0.0, null);
      DocumentGallery.AddSteps(this.host, delegate(string name, Action show, double seconds, [Nullable(new byte[] { 1, 2 })] Func<UIElement> snap)
      {
        this.Add(name, show, seconds, snap);
      });
    }

    // Token: 0x0600242D RID: 9261 RVA: 0x00631E28 File Offset: 0x00631E28
    [NullableContext(2)]
    public void Run(string only)
    {
      DemoRunner.<>c__DisplayClass13_0 CS$<>8__locals1 = new DemoRunner.<>c__DisplayClass13_0();
      CS$<>8__locals1.only = only;
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.dq = DispatcherQueue.GetForCurrentThread();
      CS$<>8__locals1.list = ((CS$<>8__locals1.only == null) ? this.steps : this.steps.Where<ValueTuple<string, Action, double, Func<UIElement>>>(([TupleElementNames(new string[] { "name", "show", "seconds", "snap" })] ValueTuple<string, Action, double, Func<UIElement>> s) => s.Item1.Equals(CS$<>8__locals1.only, StringComparison.OrdinalIgnoreCase) || (CS$<>8__locals1.only.EndsWith('*') && s.Item1.StartsWith(CS$<>8__locals1.only.TrimEnd('*'), StringComparison.OrdinalIgnoreCase))).ToList<ValueTuple<string, Action, double, Func<UIElement>>>());
      if (CS$<>8__locals1.list.Count == 0)
      {
        Log.Warn("demo", "no demo state named '" + CS$<>8__locals1.only + "'. Known: " + string.Join(", ", this.steps.Select<ValueTuple<string, Action, double, Func<UIElement>>, string>(([TupleElementNames(new string[] { "name", "show", "seconds", "snap" })] ValueTuple<string, Action, double, Func<UIElement>> s) => s.Item1)));
        return;
      }
      CS$<>8__locals1.i = 0;
      CS$<>8__locals1.timer = (this.stepTimer = CS$<>8__locals1.dq.CreateTimer());
      CS$<>8__locals1.timer.IsRepeating = false;
      CS$<>8__locals1.timer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        base.<Run>g__Next|2();
      };
      Once.After(0.4, new Action(CS$<>8__locals1.<Run>g__Next|2), CS$<>8__locals1.dq);
    }

    // Token: 0x1700069B RID: 1691
    // (get) Token: 0x0600242E RID: 9262 RVA: 0x00631F4C File Offset: 0x00631F4C
    public IEnumerable<string> Names
    {
      get
      {
        return this.steps.Select<ValueTuple<string, Action, double, Func<UIElement>>, string>(([TupleElementNames(new string[] { "name", "show", "seconds", "snap" })] ValueTuple<string, Action, double, Func<UIElement>> s) => s.Item1);
      }
    }

    // Token: 0x0600242F RID: 9263 RVA: 0x00631F78 File Offset: 0x00631F78
    private void AddPaywallSteps()
    {
      string text = DateTimeOffset.UtcNow.AddDays(12.0).ToString("O");
      this.Paywall("paywall-limit", PaywallPayloads.LocalMessagesLimit(25, 25, text, new int?(150), new int?(1000), true, true), PaywallSource.LimitHit, "free", null);
      this.Paywall("paywall-upgrade", PaywallPayloads.ManualUpgrade("free", new int?(150), new int?(1000), true, true), PaywallSource.SettingsUpgradeButton, "free", null);
      this.Paywall("paywall-upgrade-max", PaywallPayloads.ManualUpgrade("pro", new int?(150), new int?(1000), true, true), PaywallSource.SettingsUpgradeButton, "pro", null);
      this.Paywall("paywall-onboarding", PaywallPayloads.ManualUpgrade("free", new int?(150), new int?(1000), true, true), PaywallSource.OnboardingCompleted, "free", null);
      this.Paywall("paywall-referral", PaywallPayloads.ManualUpgrade("free", new int?(150), new int?(1000), true, true), PaywallSource.SettingsUpgradeButton, "free", new ReferralPaywallDiscount(50.0, 3, false, "Sam", null));
    }

    // Token: 0x06002430 RID: 9264 RVA: 0x006320B8 File Offset: 0x006320B8
    private void Paywall(string name, PaywallPayload payload, PaywallSource source, string tier, [Nullable(2)] ReferralPaywallDiscount referral)
    {
      this.Add(name, delegate
      {
        this.host.HideAuxWindows();
        PaywallPalette paywallPalette = PaywallPalette.Current();
        PaywallCard paywallCard = new PaywallCard(payload, source, paywallPalette, tier, referral, new DemoRunner.DemoPaywallHost());
        if (this.demoPaywall == null)
        {
          this.demoPaywall = new PaywallWindow(!this.host.Options.AllowCapture);
        }
        double num = (double)(source.OffersFreeOption() ? 800 : ((payload.UpgradeOptions.Count >= 2) ? 560 : 400));
        this.demoPaywall.Present(paywallCard, num, paywallPalette);
      }, 0.0, delegate
      {
        PaywallWindow paywallWindow = this.demoPaywall;
        if (paywallWindow == null)
        {
          return null;
        }
        return paywallWindow.Root;
      });
    }

    // Token: 0x06002431 RID: 9265 RVA: 0x00632118 File Offset: 0x00632118
    private void AddTourPanelSteps()
    {
      if (this.host.Options.DemoOnly != "tutorial-panel")
      {
        return;
      }
      this.Add("tutorial-panel", delegate
      {
        this.host.HideAuxWindows();
        if (TourDrive.StepContent == null)
        {
          TourDrive.StepContent = delegate(Color accent, FrameworkElement askRow, Func<bool> _)
          {
            FrameworkElement frameworkElement = OnboardingDriveDemo.Shared.CreateWindowView(accent);
            frameworkElement.HorizontalAlignment = HorizontalAlignment.Center;
            return new StackPanel
            {
              Spacing = 12.0,
              Children = 
              {
                frameworkElement,
                askRow,
                new Grid
                {
                  Height = 14.0
                }
              }
            };
          };
        }
        this.host.DemoTutorial(TutorialWindow.Step.VoiceHello);
        this.RunTourPanelAsync(DispatcherQueue.GetForCurrentThread());
      }, 40.0, () => this.host.DemoTutorialRoot());
    }

    // Token: 0x06002432 RID: 9266 RVA: 0x00632170 File Offset: 0x00632170
    private async Task RunTourPanelAsync(DispatcherQueue dq)
    {
      try
      {
        DemoRunner.<>c__DisplayClass22_0 CS$<>8__locals1 = new DemoRunner.<>c__DisplayClass22_0();
        CS$<>8__locals1.tour = this.host.DemoTutorialWindow();
        CS$<>8__locals1.root = CS$<>8__locals1.tour.Window.Root;
        await Task.Delay(1500);
        await DemoRunner.FramesAsync("tutorial-panel-grow-timing", delegate
        {
          CS$<>8__locals1.tour.Go(TutorialWindow.Step.ClickyInterview, "jump");
        }, CS$<>8__locals1.tour, false);
        await Task.Delay(600);
        await DemoRunner.FramesAsync("tutorial-panel-shrink-timing", delegate
        {
          CS$<>8__locals1.tour.Go(TutorialWindow.Step.DrawDemo, "jump");
        }, CS$<>8__locals1.tour, false);
        await Task.Delay(600);
        CS$<>8__locals1.tour.Go(TutorialWindow.Step.VoiceHello, "jump");
        await Task.Delay(1500);
        await DemoRunner.FramesAsync("tutorial-panel-grow", delegate
        {
          CS$<>8__locals1.tour.Go(TutorialWindow.Step.ClickyInterview, "jump");
        }, CS$<>8__locals1.tour, true);
        await Task.Delay(800);
        Log.Info("demo", "tutorial-panel: interview step open, focus: " + DemoRunner.Focused(CS$<>8__locals1.root));
        for (int i = 0; i < 40; i++)
        {
          TextBox textBox = DemoRunner.FindElement<TextBox>(CS$<>8__locals1.root, null);
          if (textBox == null || textBox.IsEnabled)
          {
            break;
          }
          await Task.Delay(250);
        }
        CS$<>8__locals1.box = DemoRunner.FindElement<TextBox>(CS$<>8__locals1.root, null);
        if (CS$<>8__locals1.box != null)
        {
          Log.Info("demo", "tutorial-panel: the field is " + (CS$<>8__locals1.box.IsEnabled ? "enabled" : "disabled") + ", focus: " + DemoRunner.Focused(CS$<>8__locals1.root));
          await CS$<>8__locals1.<RunTourPanelAsync>g__Attempt|3("a programmatic focus on the field", () => CS$<>8__locals1.box.Focus(FocusState.Programmatic));
          await CS$<>8__locals1.<RunTourPanelAsync>g__Attempt|3("a keyboard-state focus with no direction (an activation restore)", () => CS$<>8__locals1.box.Focus(FocusState.Keyboard));
          string text = "demo";
          string text2 = "tutorial-panel: XAML's first focusable element: ";
          DependencyObject dependencyObject = FocusManager.FindFirstFocusableElement(CS$<>8__locals1.root);
          Log.Info(text, text2 + (((dependencyObject != null) ? dependencyObject.GetType().Name : null) ?? "none"));
          await CS$<>8__locals1.<RunTourPanelAsync>g__Attempt|3("a pointer focus on the field (a click)", () => CS$<>8__locals1.box.Focus(FocusState.Pointer));
          await Snapshot.SaveAsync(CS$<>8__locals1.root, "tutorial-panel-field-clicked");
        }
        await DemoRunner.FramesAsync("tutorial-panel-shrink", delegate
        {
          CS$<>8__locals1.tour.Go(TutorialWindow.Step.DrawDemo, "jump");
        }, CS$<>8__locals1.tour, true);
        CS$<>8__locals1.tour.Go(TutorialWindow.Step.ClickyDrive, "jump");
        await Task.Delay(1200);
        await Snapshot.SaveAsync(CS$<>8__locals1.root, "tutorial-panel-drive");
        OnboardingDriveDemo shared = OnboardingDriveDemo.Shared;
        string[] array = new string[] { "row0.field", "row0.check", "row1.field", "row1.check", "row2.field" };
        for (int j = 0; j < array.Length; j++)
        {
          string text3 = array[j];
          string text4 = "demo";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 3);
          defaultInterpolatedStringHandler.AppendLiteral("tutorial-panel: drive target ");
          defaultInterpolatedStringHandler.AppendFormatted(text3);
          defaultInterpolatedStringHandler.AppendLiteral(" at ");
          defaultInterpolatedStringHandler.AppendFormatted<PointD?>(shared.ScreenCenter(text3));
          defaultInterpolatedStringHandler.AppendLiteral(" (window ");
          defaultInterpolatedStringHandler.AppendFormatted<ValueTuple<int, int, int, int>>(CS$<>8__locals1.tour.Window.RectPx);
          defaultInterpolatedStringHandler.AppendLiteral(")");
          Log.Info(text4, defaultInterpolatedStringHandler.ToStringAndClear());
        }
        TextBlock textBlock = DemoRunner.FindElement<TextBlock>(CS$<>8__locals1.root, (TextBlock t) => t.Text == "Your list");
        if (textBlock != null)
        {
          Point point = textBlock.TransformToVisual(CS$<>8__locals1.root).TransformPoint(new Point(0f, 0f));
          string text5 = "demo";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(52, 2);
          defaultInterpolatedStringHandler.AppendLiteral("tutorial-panel: \"Your list\" title at y ");
          defaultInterpolatedStringHandler.AppendFormatted<double>(point.Y, "0.0");
          defaultInterpolatedStringHandler.AppendLiteral(" (panel top ");
          defaultInterpolatedStringHandler.AppendFormatted<double>((CS$<>8__locals1.root.ActualHeight - CS$<>8__locals1.tour.Window.PanelHeight) / 2.0, "0.0");
          defaultInterpolatedStringHandler.AppendLiteral(")");
          Log.Info(text5, defaultInterpolatedStringHandler.ToStringAndClear());
        }
        CS$<>8__locals1 = null;
      }
      catch (Exception ex)
      {
        Log.Error("demo", "tutorial-panel", ex);
      }
    }

    // Token: 0x06002433 RID: 9267 RVA: 0x006321B4 File Offset: 0x006321B4
    private static async Task FramesAsync(string name, Action change, TutorialWindow tour, bool snapshots = true)
    {
      bool instant = Anim.Instant;
      Anim.Instant = false;
      Stopwatch clock = Stopwatch.StartNew();
      try
      {
        change();
      }
      finally
      {
        Anim.Instant = instant;
      }
      List<string> sizes = new List<string>();
      while (clock.Elapsed.TotalSeconds < 1.1)
      {
        double totalMilliseconds = clock.Elapsed.TotalMilliseconds;
        List<string> list = sizes;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 3);
        defaultInterpolatedStringHandler.AppendFormatted<double>(totalMilliseconds, "0");
        defaultInterpolatedStringHandler.AppendLiteral("ms ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(tour.Window.ShownWidth, "0.0");
        defaultInterpolatedStringHandler.AppendLiteral("x");
        defaultInterpolatedStringHandler.AppendFormatted<double>(tour.Window.ShownHeight, "0.0");
        list.Add(defaultInterpolatedStringHandler.ToStringAndClear());
        if (snapshots)
        {
          UIElement root = tour.Window.Root;
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
          defaultInterpolatedStringHandler.AppendFormatted(name);
          defaultInterpolatedStringHandler.AppendLiteral("-");
          defaultInterpolatedStringHandler.AppendFormatted<double>(totalMilliseconds, "0000");
          defaultInterpolatedStringHandler.AppendLiteral("ms");
          await Snapshot.SaveAsync(root, defaultInterpolatedStringHandler.ToStringAndClear());
        }
        else
        {
          await Task.Delay(50);
        }
      }
      Log.Info("demo", name + ": " + string.Join(", ", sizes));
    }

    // Token: 0x06002434 RID: 9268 RVA: 0x00632210 File Offset: 0x00632210
    private static string Focused(FrameworkElement root)
    {
      object focusedElement = FocusManager.GetFocusedElement(root.XamlRoot);
      string text;
      if (focusedElement != null)
      {
        TextBox textBox = focusedElement as TextBox;
        if (textBox == null)
        {
          text = focusedElement.GetType().Name;
        }
        else
        {
          text = "the text box \"" + textBox.PlaceholderText + "\"";
        }
      }
      else
      {
        text = "nothing";
      }
      return text;
    }

    // Token: 0x06002435 RID: 9269 RVA: 0x00632268 File Offset: 0x00632268
    [return: Nullable(2)]
    private static T FindElement<T>(DependencyObject root, [Nullable(new byte[] { 2, 1 })] Func<T, bool> match = null) where T : class
    {
      for (int i = 0; i < VisualTreeHelper.GetChildrenCount(root); i++)
      {
        DependencyObject child = VisualTreeHelper.GetChild(root, i);
        T t = child as T;
        if (t != null && (match == null || match(t)))
        {
          return t;
        }
        T t2 = DemoRunner.FindElement<T>(child, match);
        if (t2 != null)
        {
          return t2;
        }
      }
      return default(T);
    }

    // Token: 0x06002436 RID: 9270 RVA: 0x006322CC File Offset: 0x006322CC
    private static AgentIntegrationCatalog DemoIntegrationCatalog()
    {
      return new AgentIntegrationCatalog(true, new <>z__ReadOnlyArray<AgentIntegration>(new AgentIntegration[]
      {
        new AgentIntegration("gmail", "Gmail", "Read, search and draft email.", "connected", 1, 1, new <>z__ReadOnlySingleElementList<AgentIntegrationToolkit>(DemoRunner.<DemoIntegrationCatalog>g__Kit|26_0("gmail", "Gmail", true)), new <>z__ReadOnlyArray<AgentIntegrationAccount>(new AgentIntegrationAccount[]
        {
          DemoRunner.<DemoIntegrationCatalog>g__Account|26_1("ca_1", "maya@acme.com", "Work", "connected"),
          DemoRunner.<DemoIntegrationCatalog>g__Account|26_1("ca_2", "maya.lee@gmail.com", null, "connected")
        })),
        new AgentIntegration("github", "GitHub", "Issues, pull requests and code.", "connected", 1, 1, new <>z__ReadOnlySingleElementList<AgentIntegrationToolkit>(DemoRunner.<DemoIntegrationCatalog>g__Kit|26_0("github", "GitHub", true)), Array.Empty<AgentIntegrationAccount>()),
        new AgentIntegration("googlecalendar", "Google Calendar", "See and schedule events.", "needs_reconnect", 0, 1, new <>z__ReadOnlySingleElementList<AgentIntegrationToolkit>(DemoRunner.<DemoIntegrationCatalog>g__Kit|26_0("googlecalendar", "Google Calendar", false)), new <>z__ReadOnlySingleElementList<AgentIntegrationAccount>(DemoRunner.<DemoIntegrationCatalog>g__Account|26_1("ca_3", "maya@acme.com", null, "expired"))),
        new AgentIntegration("slack", "Slack", "Read channels and send messages.", "not_connected", 0, 1, new <>z__ReadOnlySingleElementList<AgentIntegrationToolkit>(DemoRunner.<DemoIntegrationCatalog>g__Kit|26_0("slack", "Slack", false)), Array.Empty<AgentIntegrationAccount>()),
        new AgentIntegration("notion", "Notion", "Pages, databases and docs.", "not_connected", 0, 1, new <>z__ReadOnlySingleElementList<AgentIntegrationToolkit>(DemoRunner.<DemoIntegrationCatalog>g__Kit|26_0("notion", "Notion", false)), Array.Empty<AgentIntegrationAccount>()),
        new AgentIntegration("linear", "Linear", "Track issues and projects.", "not_connected", 0, 1, new <>z__ReadOnlySingleElementList<AgentIntegrationToolkit>(DemoRunner.<DemoIntegrationCatalog>g__Kit|26_0("linear", "Linear", false)), Array.Empty<AgentIntegrationAccount>())
      }));
    }

    // Token: 0x06002437 RID: 9271 RVA: 0x0063248C File Offset: 0x0063248C
    private static string Kebab(string s)
    {
      return string.Concat(s.Select<char, string>(delegate(char ch, int i)
      {
        char c;
        if (!char.IsUpper(ch) || i <= 0)
        {
          c = char.ToLowerInvariant(ch);
          return c.ToString();
        }
        ReadOnlySpan<char> readOnlySpan = "-";
        c = char.ToLowerInvariant(ch);
        return readOnlySpan + new ReadOnlySpan<char>(ref c);
      }));
    }

    // Token: 0x06002470 RID: 9328 RVA: 0x00632B6C File Offset: 0x00632B6C
    [CompilerGenerated]
    internal static AgentIntegrationToolkit <DemoIntegrationCatalog>g__Kit|26_0(string slug, string name, bool on)
    {
      return new AgentIntegrationToolkit(slug, name, null, on);
    }

    // Token: 0x06002471 RID: 9329 RVA: 0x00632B78 File Offset: 0x00632B78
    [CompilerGenerated]
    internal static AgentIntegrationAccount <DemoIntegrationCatalog>g__Account|26_1(string id, [Nullable(2)] string name, [Nullable(2)] string label, string status = "connected")
    {
      return new AgentIntegrationAccount(id, name, label, null, null, status, DateTimeOffset.Now.AddDays(-12.0).ToString("O"));
    }

    // Token: 0x0400118E RID: 4494
    private readonly AppHost host;

    // Token: 0x0400118F RID: 4495
    [TupleElementNames(new string[] { "name", "show", "seconds", "snap" })]
    [Nullable(new byte[] { 1, 0, 1, 1, 2, 2 })]
    private readonly List<ValueTuple<string, Action, double, Func<UIElement>>> steps = new List<ValueTuple<string, Action, double, Func<UIElement>>>();

    // Token: 0x04001190 RID: 4496
    [TupleElementNames(new string[] { "hwnd", "after" })]
    [Nullable(new byte[] { 1, 1, 0, 1 })]
    private readonly Dictionary<string, ValueTuple<Func<IntPtr>, double>> windowSnaps = new Dictionary<string, ValueTuple<Func<IntPtr>, double>>();

    // Token: 0x04001191 RID: 4497
    public static double DwellSeconds = 1.5;

    // Token: 0x04001192 RID: 4498
    [Nullable(2)]
    private DispatcherQueueTimer stepTimer;

    // Token: 0x04001193 RID: 4499
    [Nullable(2)]
    private PaywallWindow demoPaywall;

    // Token: 0x020009C9 RID: 2505
    [NullableContext(0)]
    private sealed class DemoPaywallHost : IPaywallCardHost
    {
      // Token: 0x06003E5E RID: 15966 RVA: 0x006A210C File Offset: 0x006A210C
      [NullableContext(1)]
      [return: Nullable(new byte[] { 1, 2 })]
      public Task<string> StartCheckoutAsync(string plan, string interval, string surface, PaywallSource source)
      {
        return Task.FromResult<string>(null);
      }

      // Token: 0x06003E5F RID: 15967 RVA: 0x006A2114 File Offset: 0x006A2114
      [return: Nullable(new byte[] { 1, 2 })]
      public Task<string> OpenPortalAsync()
      {
        return Task.FromResult<string>(null);
      }

      // Token: 0x06003E60 RID: 15968 RVA: 0x006A211C File Offset: 0x006A211C
      public void Close(PaywallCloseKind kind)
      {
      }

      // Token: 0x06003E61 RID: 15969 RVA: 0x006A2120 File Offset: 0x006A2120
      public void ContentResized()
      {
      }
    }
  }
}
