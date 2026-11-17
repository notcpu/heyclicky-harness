using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.App.Billing;
using HeyClicky.App.Design;
using HeyClicky.App.Notch;
using HeyClicky.App.Onboarding;
using HeyClicky.App.Services;
using HeyClicky.App.UI;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Onboarding;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Telemetry;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace HeyClicky.App.Demo
{
  // Token: 0x02000350 RID: 848
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AnalyticsEndToEnd
  {
    // Token: 0x06002408 RID: 9224 RVA: 0x00630B48 File Offset: 0x00630B48
    private static IEnumerable<DependencyObject> Descendants(DependencyObject root)
    {
      AnalyticsEndToEnd.<Descendants>d__0 <Descendants>d__ = new AnalyticsEndToEnd.<Descendants>d__0(-2);
      <Descendants>d__.<>3__root = root;
      return <Descendants>d__;
    }

    // Token: 0x06002409 RID: 9225 RVA: 0x00630B58 File Offset: 0x00630B58
    private bool ClickText(DependencyObject root, string text, bool trailingControl = false)
    {
      TextBlock textBlock = AnalyticsEndToEnd.Descendants(root).OfType<TextBlock>().FirstOrDefault<TextBlock>((TextBlock x) => x.Text == text);
      if (textBlock == null)
      {
        this.<say>P("analytics: no \"" + text + "\" on screen");
        return false;
      }
      DependencyObject dependencyObject = textBlock;
      if (!trailingControl)
      {
        while (dependencyObject != null)
        {
          Pressable pressable = dependencyObject as Pressable;
          if (pressable != null)
          {
            pressable.RaiseClick();
            return true;
          }
          dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
        }
      }
      for (dependencyObject = textBlock; dependencyObject != null; dependencyObject = VisualTreeHelper.GetParent(dependencyObject))
      {
        Grid grid = dependencyObject as Grid;
        if (grid != null && grid.ColumnDefinitions.Count >= 2)
        {
          Pressable pressable2 = AnalyticsEndToEnd.Descendants(grid).OfType<Pressable>().LastOrDefault<Pressable>();
          if (pressable2 != null)
          {
            pressable2.RaiseClick();
            return true;
          }
        }
      }
      return false;
    }

    // Token: 0x0600240A RID: 9226 RVA: 0x00630C24 File Offset: 0x00630C24
    private async Task CoreAsync()
    {
      RealStack real = this.<host>P.Real;
      NotchPresenter presenter = this.<host>P.Services.Presenter as NotchPresenter;
      this.Mark();
      await this.OnUi(delegate
      {
        NotchPresenter presenter6 = presenter;
        if (presenter6 == null)
        {
          return;
        }
        presenter6.SetOnboardingPin(new int?(0));
      });
      await AnalyticsEndToEnd.Wait(700);
      await this.OnUi(delegate
      {
        NotchPresenter presenter2 = presenter;
        if (presenter2 == null)
        {
          return;
        }
        presenter2.SetOnboardingPin(null);
      });
      await this.Expect("notch", new <>z__ReadOnlyArray<ValueTuple<string, string[]>>(new ValueTuple<string, string[]>[]
      {
        new ValueTuple<string, string[]>("notch_expanded", new string[] { "trigger" }),
        new ValueTuple<string, string[]>("notch_collapsed", new string[] { "dwell_ms" })
      }), 1500);
      this.Mark();
      await this.OnUi(delegate
      {
        this.<host>P.OpenNotchHome();
      });
      await AnalyticsEndToEnd.Wait(1200);
      await this.OnUi(delegate
      {
        NotchPresenter presenter3 = presenter;
        if (presenter3 == null)
        {
          return;
        }
        presenter3.Tabs.Select(NotchTab.Chat);
      });
      await AnalyticsEndToEnd.Wait(600);
      await this.Expect("notch tabs", new <>z__ReadOnlySingleElementList<ValueTuple<string, string[]>>(new ValueTuple<string, string[]>("notch_peek_action", new string[] { "action" })), 1500);
      await this.OnUi(delegate
      {
        NotchPresenter presenter4 = presenter;
        if (presenter4 == null)
        {
          return;
        }
        presenter4.DismissNotchAgentPeek();
      });
      await AnalyticsEndToEnd.Wait(500);
      string dropped = Path.Combine(this.<outDir>P, "analytics-drop.txt");
      File.WriteAllText(dropped, "dropped on the notch");
      this.Mark();
      await this.OnUi(delegate
      {
        this.<host>P.Services.Presenter.Present(new NotchPresentation.TextInput("", null));
      });
      await AnalyticsEndToEnd.Wait(800);
      await this.OnUi(delegate
      {
        NotchSurface activeSurfaceForTest = this.<host>P.NotchUi.ActiveSurfaceForTest;
        if (activeSurfaceForTest == null)
        {
          return;
        }
        activeSurfaceForTest.AcceptFiles(new <>z__ReadOnlySingleElementList<string>(dropped));
      });
      await this.Expect("notch drop", "notch_dropped_files_attached", new string[] { "file_count" });
      await this.OnUi(delegate
      {
        this.<host>P.Services.Presenter.Present(new NotchPresentation.Idle());
      });
      await AnalyticsEndToEnd.Wait(500);
      this.Mark();
      await this.OnUi(delegate
      {
        this.<host>P.OpenSettings(NotchSettingsSection.General);
      });
      await AnalyticsEndToEnd.Wait(1500);
      await this.OnUi(delegate
      {
        this.ClickText(this.<host>P.NotchUi.Window.Root, "Show in screen recordings", true);
      });
      await AnalyticsEndToEnd.Wait(400);
      await this.OnUi(delegate
      {
        this.ClickText(this.<host>P.NotchUi.Window.Root, "Show in screen recordings", true);
      });
      await AnalyticsEndToEnd.Wait(400);
      await this.OnUi(delegate
      {
        this.<host>P.OpenSettings(NotchSettingsSection.Referral);
      });
      await AnalyticsEndToEnd.Wait(1200);
      await this.Expect("settings", new <>z__ReadOnlyArray<ValueTuple<string, string[]>>(new ValueTuple<string, string[]>[]
      {
        new ValueTuple<string, string[]>("setting_changed", new string[] { "setting", "value", "surface", "product_event_version" }),
        new ValueTuple<string, string[]>("referral_page_opened", new string[] { "entry_point" })
      }), 1500);
      await this.OnUi(delegate
      {
        NotchPresenter presenter5 = presenter;
        if (presenter5 == null)
        {
          return;
        }
        presenter5.DismissNotchAgentPeek();
      });
      await AnalyticsEndToEnd.Wait(800);
      int before = MainThreadStallMonitor.StallCount;
      await this.OnUi(delegate
      {
        Thread.Sleep(2500);
      });
      await AnalyticsEndToEnd.Wait(400);
      bool flag = MainThreadStallMonitor.StallCount > before && MainThreadStallMonitor.LongestStallMs >= 2000;
      if (flag)
      {
        this.passed++;
      }
      else
      {
        this.failed++;
      }
      Action<string> action = this.<say>P;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(50, 3);
      defaultInterpolatedStringHandler.AppendLiteral("analytics: ");
      defaultInterpolatedStringHandler.AppendFormatted(flag ? "PASS" : "FAIL");
      defaultInterpolatedStringHandler.AppendLiteral(" stall watchdog: ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(MainThreadStallMonitor.StallCount - before);
      defaultInterpolatedStringHandler.AppendLiteral(" stall(s), longest ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(MainThreadStallMonitor.LongestStallMs);
      defaultInterpolatedStringHandler.AppendLiteral(" ms");
      action(defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x0600240B RID: 9227 RVA: 0x00630C68 File Offset: 0x00630C68
    public AnalyticsEndToEnd(AppHost host, string outDir, Action<string> say)
    {
    }

    // Token: 0x17000699 RID: 1689
    // (get) Token: 0x0600240C RID: 9228 RVA: 0x00630C90 File Offset: 0x00630C90
    [Nullable(2)]
    private LogAnalyticsSink Sink
    {
      [NullableContext(2)]
      get
      {
        return ClickyAnalytics.Sink as LogAnalyticsSink;
      }
    }

    // Token: 0x0600240D RID: 9229 RVA: 0x00630C9C File Offset: 0x00630C9C
    private static Task Wait(int ms)
    {
      return Task.Delay(ms);
    }

    // Token: 0x0600240E RID: 9230 RVA: 0x00630CA4 File Offset: 0x00630CA4
    private Task OnUi(Action action)
    {
      TaskCompletionSource done = new TaskCompletionSource();
      this.<host>P.DispatcherQueue.TryEnqueue(delegate
      {
        try
        {
          action();
          done.TrySetResult();
        }
        catch (Exception ex)
        {
          done.TrySetException(ex);
        }
      });
      return done.Task;
    }

    // Token: 0x0600240F RID: 9231 RVA: 0x00630CF4 File Offset: 0x00630CF4
    private IReadOnlyList<AnalyticsEvent> Since()
    {
      LogAnalyticsSink sink = this.Sink;
      return ((sink != null) ? sink.Recent.Where<AnalyticsEvent>((AnalyticsEvent e) => e.Timestamp >= this.mark).ToList<AnalyticsEvent>() : null) ?? new List<AnalyticsEvent>();
    }

    // Token: 0x06002410 RID: 9232 RVA: 0x00630D28 File Offset: 0x00630D28
    private void Mark()
    {
      this.mark = DateTimeOffset.UtcNow;
    }

    // Token: 0x06002411 RID: 9233 RVA: 0x00630D38 File Offset: 0x00630D38
    private async Task Expect(string label, [TupleElementNames(new string[] { "Event", "Keys" })] [Nullable(new byte[] { 1, 0, 1, 1, 1 })] IEnumerable<ValueTuple<string, string[]>> expected, int waitMs = 1500)
    {
      List<ValueTuple<string, string[]>> list = expected.ToList<ValueTuple<string, string[]>>();
      DateTime deadline = DateTime.UtcNow.AddMilliseconds((double)waitMs);
      for (;;)
      {
        IReadOnlyList<AnalyticsEvent> events = this.Since();
        if (list.Where<ValueTuple<string, string[]>>(([TupleElementNames(new string[] { "Event", "Keys" })] ValueTuple<string, string[]> e) => !events.Any<AnalyticsEvent>((AnalyticsEvent x) => x.Event == e.Item1 && e.Item2.All<string>((string k) => x.Properties.ContainsKey(k)))).ToList<ValueTuple<string, string[]>>().Count == 0 || DateTime.UtcNow >= deadline)
        {
          break;
        }
        await AnalyticsEndToEnd.Wait(100);
      }
      using (List<ValueTuple<string, string[]>>.Enumerator enumerator = list.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ValueTuple<string, string[]> e = enumerator.Current;
          AnalyticsEvent hit = CS$<>8__locals1.events.LastOrDefault<AnalyticsEvent>((AnalyticsEvent x) => x.Event == e.Item1);
          bool flag = hit != null && e.Item2.All<string>((string k) => hit.Properties.ContainsKey(k));
          if (flag)
          {
            this.passed++;
          }
          else
          {
            this.failed++;
          }
          string text = ((hit == null) ? "" : (" " + string.Join(", ", e.Item2.Select<string, string>(delegate(string k)
          {
            string text2 = "=";
            JsonNode jsonNode = hit.Properties[k];
            return k + text2 + ((jsonNode != null) ? jsonNode.ToJsonString(null) : null);
          }))));
          Action<string> action = this.<say>P;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(14, 5);
          defaultInterpolatedStringHandler.AppendLiteral("analytics: ");
          defaultInterpolatedStringHandler.AppendFormatted(flag ? "PASS" : "FAIL");
          defaultInterpolatedStringHandler.AppendLiteral(" ");
          defaultInterpolatedStringHandler.AppendFormatted(label);
          defaultInterpolatedStringHandler.AppendLiteral(": ");
          defaultInterpolatedStringHandler.AppendFormatted(e.Item1);
          defaultInterpolatedStringHandler.AppendFormatted(text);
          defaultInterpolatedStringHandler.AppendFormatted((hit == null) ? " (not fired)" : ((!flag) ? " (missing properties)" : ""));
          action(defaultInterpolatedStringHandler.ToStringAndClear());
        }
      }
    }

    // Token: 0x06002412 RID: 9234 RVA: 0x00630D94 File Offset: 0x00630D94
    private Task Expect(string label, params string[] events)
    {
      return this.Expect(label, events.Select<string, ValueTuple<string, string[]>>((string e) => new ValueTuple<string, string[]>(e, Array.Empty<string>())), 1500);
    }

    // Token: 0x06002413 RID: 9235 RVA: 0x00630DC8 File Offset: 0x00630DC8
    private Task Expect(string label, string evt, params string[] keys)
    {
      return this.Expect(label, new <>z__ReadOnlySingleElementList<ValueTuple<string, string[]>>(new ValueTuple<string, string[]>(evt, keys)), 1500);
    }

    // Token: 0x06002414 RID: 9236 RVA: 0x00630DE4 File Offset: 0x00630DE4
    public async Task RunAsync()
    {
      if (this.Sink == null)
      {
        this.<say>P("analytics: FAIL the debug sink is off (set HEYCLICKY_ANALYTICS_DEBUG=1)");
      }
      else
      {
        string only = Environment.GetEnvironmentVariable("HEYCLICKY_E2E_ONLY");
        bool flag = only == null || only == "core";
        if (flag)
        {
          await this.RunCoreAsync();
        }
        flag = only == null || only == "onboarding";
        if (flag)
        {
          await this.RunOnboardingAsync();
        }
        Action<string> action = this.<say>P;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 2);
        defaultInterpolatedStringHandler.AppendLiteral("analytics: ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(this.passed);
        defaultInterpolatedStringHandler.AppendLiteral(" passed, ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(this.failed);
        defaultInterpolatedStringHandler.AppendLiteral(" failed");
        action(defaultInterpolatedStringHandler.ToStringAndClear());
      }
    }

    // Token: 0x06002415 RID: 9237 RVA: 0x00630E28 File Offset: 0x00630E28
    private void RunCore(ref Task task)
    {
      task = this.CoreAsync();
    }

    // Token: 0x06002416 RID: 9238 RVA: 0x00630E34 File Offset: 0x00630E34
    private async Task RunCoreAsync()
    {
      Task completedTask = Task.CompletedTask;
      this.RunCore(ref completedTask);
      await completedTask;
    }

    // Token: 0x06002417 RID: 9239 RVA: 0x00630E78 File Offset: 0x00630E78
    private void RunOnboarding(ref Task task)
    {
      task = this.OnboardingAsync();
    }

    // Token: 0x06002418 RID: 9240 RVA: 0x00630E84 File Offset: 0x00630E84
    private async Task RunOnboardingAsync()
    {
      Task completedTask = Task.CompletedTask;
      this.RunOnboarding(ref completedTask);
      await completedTask;
    }

    // Token: 0x06002419 RID: 9241 RVA: 0x00630EC8 File Offset: 0x00630EC8
    [return: TupleElementNames(new string[] { "Event", "Keys" })]
    [return: Nullable(new byte[] { 0, 1, 1, 1 })]
    private static ValueTuple<string, string[]> ObE(string evt, params string[] keys)
    {
      return new ValueTuple<string, string[]>(evt, keys);
    }

    // Token: 0x0600241A RID: 9242 RVA: 0x00630ED4 File Offset: 0x00630ED4
    private async Task ObExpectValue(string label, string evt, string key, string value, int waitMs = 1500)
    {
      DateTime deadline = DateTime.UtcNow.AddMilliseconds((double)waitMs);
      bool flag;
      for (;;)
      {
        flag = this.Since().Any<AnalyticsEvent>(delegate(AnalyticsEvent x)
        {
          if (x.Event == evt)
          {
            JsonNode jsonNode = x.Properties[key];
            if (jsonNode != null)
            {
              return jsonNode.ToJsonString(null).Trim('"') == value;
            }
          }
          return false;
        });
        if (flag || DateTime.UtcNow >= deadline)
        {
          break;
        }
        await AnalyticsEndToEnd.Wait(100);
      }
      if (flag)
      {
        this.passed++;
      }
      else
      {
        this.failed++;
      }
      Action<string> action = this.<say>P;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 6);
      defaultInterpolatedStringHandler.AppendLiteral("analytics: ");
      defaultInterpolatedStringHandler.AppendFormatted(flag ? "PASS" : "FAIL");
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted(label);
      defaultInterpolatedStringHandler.AppendLiteral(": ");
      defaultInterpolatedStringHandler.AppendFormatted(evt);
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted(key);
      defaultInterpolatedStringHandler.AppendLiteral("=");
      defaultInterpolatedStringHandler.AppendFormatted(value);
      defaultInterpolatedStringHandler.AppendFormatted(flag ? "" : " (not seen)");
      action(defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x0600241B RID: 9243 RVA: 0x00630F44 File Offset: 0x00630F44
    private Task ObOnUiAsync(Func<Task> action)
    {
      AnalyticsEndToEnd.<>c__DisplayClass26_0 CS$<>8__locals1 = new AnalyticsEndToEnd.<>c__DisplayClass26_0();
      CS$<>8__locals1.action = action;
      CS$<>8__locals1.done = new TaskCompletionSource();
      this.<host>P.DispatcherQueue.TryEnqueue(delegate
      {
        AnalyticsEndToEnd.<>c__DisplayClass26_0.<<ObOnUiAsync>b__0>d <<ObOnUiAsync>b__0>d;
        <<ObOnUiAsync>b__0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
        <<ObOnUiAsync>b__0>d.<>4__this = CS$<>8__locals1;
        <<ObOnUiAsync>b__0>d.<>1__state = -1;
        <<ObOnUiAsync>b__0>d.<>t__builder.Start<AnalyticsEndToEnd.<>c__DisplayClass26_0.<<ObOnUiAsync>b__0>d>(ref <<ObOnUiAsync>b__0>d);
      });
      return CS$<>8__locals1.done.Task;
    }

    // Token: 0x0600241C RID: 9244 RVA: 0x00630F94 File Offset: 0x00630F94
    private async Task OnboardingAsync()
    {
      RealStack real = this.<host>P.Real;
      if (real == null)
      {
        this.failed++;
        this.<say>P("analytics: FAIL onboarding: needs the real stack");
      }
      else
      {
        SettingsStore store = real.Core.Settings;
        ValueTuple<bool, bool, bool, double?, string, string, bool, ValueTuple<string, string, string, string>> saved = store.Read<ValueTuple<bool, bool, bool, double?, string, string, bool, ValueTuple<string, string, string, string>>>(([Nullable(1)] AppSettings s) => new ValueTuple<bool, bool, bool, double?, string, string, bool, ValueTuple<string, string, string, string>>(s.Onboarding.PreSignInIntroDismissed, s.Onboarding.HasAcknowledgedPermissionIntro, s.Onboarding.HasCompletedOnboarding, s.Onboarding.TutorialCompletedAtEpochSeconds, s.Onboarding.DiscoveryChannel, s.Onboarding.DiscoveryChannelOther, s.Onboarding.DiscoveryChannelSynced, new ValueTuple<string, string, string, string>(s.Onboarding.SquadPackId, s.SquadMemberId, s.VoiceIdentifier, s.CursorColorHex)));
        string noCues = Environment.GetEnvironmentVariable("HEYCLICKY_NO_VIDEO_CUES");
        object obj = null;
        try
        {
          try
          {
            this.mark = DateTimeOffset.MinValue;
            await this.Expect("launch", new ValueTuple<string, string[]>[] { AnalyticsEndToEnd.ObE("notch_first_shown", new string[] { "entry_context" }) }, 0);
            await this.ObIntroAsync(saved.Rest.Item1, saved.Rest.Item4);
            await this.ObPermissionsAsync(store);
            await this.ObVideoAndInviteAsync(store);
            await this.ObTutorialAsync();
            await this.ObInterviewAsync();
            await this.ObRevealAsync();
          }
          catch (Exception ex)
          {
            this.failed++;
            this.<say>P("analytics: FAIL onboarding: " + ex.GetType().Name + ": " + ex.Message);
          }
        }
        catch (object obj)
        {
        }
        Environment.SetEnvironmentVariable("HEYCLICKY_NO_VIDEO_CUES", noCues);
        store.Update(delegate(AppSettings s)
        {
          s.Onboarding.PreSignInIntroDismissed = saved.Item1;
          s.Onboarding.HasAcknowledgedPermissionIntro = saved.Item2;
          s.Onboarding.HasCompletedOnboarding = saved.Item3;
          s.Onboarding.TutorialCompletedAtEpochSeconds = saved.Item4;
          s.Onboarding.DiscoveryChannel = saved.Item5;
          s.Onboarding.DiscoveryChannelOther = saved.Item6;
          s.Onboarding.DiscoveryChannelSynced = saved.Item7;
          s.Onboarding.SquadPackId = saved.Rest.Item1;
          s.SquadMemberId = saved.Rest.Item2;
          s.VoiceIdentifier = saved.Rest.Item3;
          s.CursorColorHex = saved.Rest.Item4;
        });
        string item = saved.Item5;
        if (item != null)
        {
          try
          {
            await real.Core.Worker.SetDiscoveryChannelAsync(item, saved.Item6, default(CancellationToken));
          }
          catch (Exception ex2)
          {
            this.<say>P("analytics: onboarding: discovery channel not restored: " + ex2.Message);
          }
        }
        await this.OnUi(delegate
        {
          if (real.Tutorial.Phase == OnboardingTutorialPhase.InvitePresented)
          {
            real.Tutorial.BypassInvite();
          }
          BillingUi billing = this.<host>P.Billing;
          if (billing != null && billing.IsPaywallVisible)
          {
            billing.DismissPaywall();
          }
          this.<host>P.HideAuxWindows();
          NotchOnboardingCardViewModel cardVm = this.<host>P.CardVm;
          if (cardVm != null)
          {
            cardVm.IsIntroHomeWelcomeActive = false;
            cardVm.Refresh();
          }
        });
        object obj2 = obj;
        if (obj2 != null)
        {
          Exception ex3 = obj2 as Exception;
          if (ex3 == null)
          {
            throw obj2;
          }
          ExceptionDispatchInfo.Capture(ex3).Throw();
        }
        obj = null;
      }
    }

    // Token: 0x0600241D RID: 9245 RVA: 0x00630FD8 File Offset: 0x00630FD8
    private async Task ObIntroAsync([Nullable(2)] string savedSquad, string savedColorHex)
    {
      SettingsStore settingsStore = this.<host>P.Real.Core.Settings;
      bool hadLanguageStep = settingsStore.Read<bool>((AppSettings x) => x.OnboardingLanguageStepEnabled);
      settingsStore.Update(delegate(AppSettings x)
      {
        x.OnboardingLanguageStepEnabled = true;
      });
      try
      {
        await this.ObIntroStepsAsync(savedSquad, savedColorHex);
      }
      finally
      {
        settingsStore.Update(delegate(AppSettings x)
        {
          x.OnboardingLanguageStepEnabled = hadLanguageStep;
        });
      }
    }

    // Token: 0x0600241E RID: 9246 RVA: 0x0063102C File Offset: 0x0063102C
    private async Task ObIntroStepsAsync([Nullable(2)] string savedSquad, string savedColorHex)
    {
      AnalyticsEndToEnd.<>c__DisplayClass29_0 CS$<>8__locals1 = new AnalyticsEndToEnd.<>c__DisplayClass29_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.savedColorHex = savedColorHex;
      CS$<>8__locals1.intro = null;
      this.Mark();
      await this.OnUi(delegate
      {
        CS$<>8__locals1.intro = CS$<>8__locals1.<>4__this.<host>P.DemoIntro(IntroWindow.Step.WhatIsClicky);
      });
      await this.Expect("intro open", new ValueTuple<string, string[]>[] { AnalyticsEndToEnd.ObE("intro_viewed", new string[] { "step" }) }, 1500);
      await this.ObExpectValue("intro open", "intro_viewed", "step", "what_is_clicky", 1500);
      CS$<>8__locals1.w = CS$<>8__locals1.intro;
      this.Mark();
      await this.OnUi(new Action(CS$<>8__locals1.w.ContinueFromWelcome));
      await this.Expect("intro continue", new ValueTuple<string, string[]>[]
      {
        AnalyticsEndToEnd.ObE("intro_button_tapped", new string[] { "step", "action" }),
        AnalyticsEndToEnd.ObE("intro_viewed", new string[] { "step" })
      }, 1500);
      await this.ObExpectValue("intro continue", "intro_button_tapped", "action", "continue", 1500);
      await this.ObExpectValue("intro continue", "intro_viewed", "step", "where_heard_about", 1500);
      this.Mark();
      await this.OnUi(delegate
      {
        CS$<>8__locals1.w.PickChannel("Google");
      });
      await this.Expect("intro channel", new ValueTuple<string, string[]>[]
      {
        AnalyticsEndToEnd.ObE("intro_button_tapped", new string[] { "step", "action", "value" }),
        AnalyticsEndToEnd.ObE("discovery_channel_selected", new string[] { "channel" })
      }, 1500);
      await this.ObExpectValue("intro channel", "intro_button_tapped", "value", "google", 1500);
      await this.ObExpectValue("intro channel", "intro_viewed", "step", "pick_language", 1500);
      this.Mark();
      await this.OnUi(delegate
      {
        CS$<>8__locals1.w.PickLanguage(null);
        CS$<>8__locals1.w.ContinueLanguage();
      });
      await this.ObExpectValue("intro language", "intro_button_tapped", "value", "automatic", 1500);
      await this.ObExpectValue("intro language", "intro_viewed", "step", "open_clicky", 1500);
      ValueTuple<string, string> valueTuple = this.<host>P.Real.Core.Settings.Read<ValueTuple<string, string>>(([Nullable(1)] AppSettings s) => new ValueTuple<string, string>(s.Onboarding.SquadPackId, s.VoiceIdentifier));
      if (valueTuple.Item1 == "classicClouds" && valueTuple.Item2 == "cedar")
      {
        this.passed++;
        this.<say>P("analytics: PASS intro look: classicClouds with cedar");
      }
      else
      {
        this.failed++;
        this.<say>P("analytics: FAIL intro look: " + valueTuple.Item1 + " with " + valueTuple.Item2);
      }
      await this.OnUi(delegate
      {
        CS$<>8__locals1.w.Go(IntroWindow.Step.WhereHeardAbout);
      });
      this.Mark();
      await this.OnUi(delegate
      {
        CS$<>8__locals1.w.PickChannel("Other");
        CS$<>8__locals1.w.CommitOther();
      });
      await this.Expect("intro other", new ValueTuple<string, string[]>[]
      {
        AnalyticsEndToEnd.ObE("intro_button_tapped", new string[] { "step", "action", "value" }),
        AnalyticsEndToEnd.ObE("discovery_channel_selected", new string[] { "channel" })
      }, 1500);
      await this.ObExpectValue("intro other", "intro_button_tapped", "action", "submit_discovery_channel_other", 1500);
      await this.ObExpectValue("intro other", "intro_button_tapped", "value", "skipped", 1500);
      await this.ObExpectValue("intro other", "discovery_channel_selected", "channel", "other", 1500);
      await this.ObExpectValue("intro other", "intro_viewed", "step", "pick_language", 1500);
      await this.OnUi(new Action(CS$<>8__locals1.w.ContinueLanguage));
      CS$<>8__locals1.option = OnboardingCatalog.CursorColors.FirstOrDefault<CursorColorOption>((CursorColorOption c) => string.Equals(c.Hex, CS$<>8__locals1.savedColorHex, StringComparison.OrdinalIgnoreCase)) ?? OnboardingCatalog.CursorColors[1];
      AnalyticsEndToEnd.<>c__DisplayClass29_0 CS$<>8__locals2 = CS$<>8__locals1;
      char c2 = char.ToUpperInvariant(CS$<>8__locals1.option.Id[0]);
      ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(ref c2);
      string id = CS$<>8__locals1.option.Id;
      CS$<>8__locals2.swatch = readOnlySpan + id.Substring(1, id.Length - 1);
      this.Mark();
      await this.OnUi(delegate
      {
        CS$<>8__locals1.w.PickColor(CS$<>8__locals1.swatch, DS.CursorColor(CS$<>8__locals1.option.Hex));
      });
      await this.ObExpectValue("intro color", "intro_button_tapped", "value", CS$<>8__locals1.option.Id, 1500);
      await this.ObExpectValue("intro color", "intro_viewed", "step", "hatched", 1500);
      await AnalyticsEndToEnd.Wait(600);
      this.Mark();
      await this.OnUi(new Action(CS$<>8__locals1.w.GiveHome));
      await this.ObExpectValue("intro home", "intro_button_tapped", "action", "give_clicky_a_home", 1500);
      await this.Expect("intro home", new ValueTuple<string, string[]>[] { AnalyticsEndToEnd.ObE("intro_notch_welcome_shown", Array.Empty<string>()) }, 12000);
    }

    // Token: 0x0600241F RID: 9247 RVA: 0x00631078 File Offset: 0x00631078
    private async Task ObPermissionsAsync(SettingsStore store)
    {
      store.Update(delegate(AppSettings s)
      {
        s.Onboarding.HasCompletedOnboarding = false;
        s.Onboarding.HasAcknowledgedPermissionIntro = false;
        s.Onboarding.PreSignInIntroDismissed = false;
      });
      AnalyticsEndToEnd.ObStubPermissions stub = new AnalyticsEndToEnd.ObStubPermissions();
      NotchOnboardingCardViewModel vm = null;
      await this.OnUi(delegate
      {
        vm = new NotchOnboardingCardViewModel(store, stub)
        {
          IsSignedIn = true,
          GrantedSuccessHold = TimeSpan.Zero
        };
      });
      NotchOnboardingCardViewModel card = vm;
      this.Mark();
      await this.OnUi(delegate
      {
        card.IsIntroHomeWelcomeActive = true;
      });
      await this.ObOnUiAsync(new Func<Task>(card.PrimaryActionAsync));
      await this.Expect("card welcome", new ValueTuple<string, string[]>[]
      {
        AnalyticsEndToEnd.ObE("intro_notch_welcome_shown", Array.Empty<string>()),
        AnalyticsEndToEnd.ObE("intro_notch_final_setup_tapped", Array.Empty<string>()),
        AnalyticsEndToEnd.ObE("pre_sign_in_intro_dismissed", Array.Empty<string>()),
        AnalyticsEndToEnd.ObE("permission_flow_shown", new string[] { "entry_context", "required_permission_count" }),
        AnalyticsEndToEnd.ObE("permission_step_shown", new string[] { "permission", "permission_step_index" })
      }, 1500);
      await this.ObExpectValue("card welcome", "permission_flow_shown", "entry_context", "firstRun", 1500);
      await this.ObExpectValue("card welcome", "permission_step_shown", "permission", "microphone", 1500);
      this.Mark();
      stub.Next = PermissionStatus.Denied;
      await this.ObOnUiAsync(new Func<Task>(card.PrimaryActionAsync));
      await this.ObOnUiAsync(new Func<Task>(card.PrimaryActionAsync));
      await this.Expect("card microphone", new ValueTuple<string, string[]>[]
      {
        AnalyticsEndToEnd.ObE("permission_denied", new string[] { "permission" }),
        AnalyticsEndToEnd.ObE("permission_settings_opened", new string[] { "permission" })
      }, 1500);
      this.Mark();
      await this.OnUi(delegate
      {
        stub.Set(PermissionKind.Microphone, PermissionStatus.Granted);
      });
      stub.Next = PermissionStatus.WaitingInSettings;
      await this.ObOnUiAsync(new Func<Task>(card.PrimaryActionAsync));
      await this.OnUi(delegate
      {
        stub.Set(PermissionKind.ScreenCapture, PermissionStatus.Granted);
      });
      await this.Expect("card granted", new ValueTuple<string, string[]>[]
      {
        AnalyticsEndToEnd.ObE("permission_granted", new string[] { "permission" }),
        AnalyticsEndToEnd.ObE("permission_settings_opened", new string[] { "permission" }),
        AnalyticsEndToEnd.ObE("core_permissions_granted", Array.Empty<string>()),
        AnalyticsEndToEnd.ObE("all_permissions_granted", Array.Empty<string>()),
        AnalyticsEndToEnd.ObE("permission_flow_completed", new string[] { "entry_context" })
      }, 1500);
      await this.ObExpectValue("card granted", "permission_step_shown", "permission", "screen_recording", 1500);
      await this.ObExpectValue("card granted", "permission_granted", "permission", "microphone", 1500);
      await this.ObExpectValue("card granted", "permission_granted", "permission", "screen_recording", 1500);
      this.Mark();
      await this.ObOnUiAsync(new Func<Task>(card.PrimaryActionAsync));
      await this.Expect("card meet", new ValueTuple<string, string[]>[] { AnalyticsEndToEnd.ObE("onboarding_started", Array.Empty<string>()) }, 1500);
    }

    // Token: 0x06002420 RID: 9248 RVA: 0x006310C4 File Offset: 0x006310C4
    private async Task ObVideoAndInviteAsync(SettingsStore store)
    {
      store.Update(delegate(AppSettings s)
      {
        s.Onboarding.TutorialCompletedAtEpochSeconds = null;
      });
      Environment.SetEnvironmentVariable("HEYCLICKY_NO_VIDEO_CUES", "1");
      CursorCompanion companion = this.<host>P.CursorCompanion;
      this.Mark();
      await this.OnUi(delegate
      {
        this.<host>P.DemoIntroSequence(true);
      });
      await this.Expect("video", new ValueTuple<string, string[]>[] { AnalyticsEndToEnd.ObE("onboarding_video_started", new string[] { "video_kind", "phase", "asset_url", "player_status", "item_status", "time_control_status", "source" }) }, 30000);
      if (!companion.IsPlayingVideo)
      {
        this.failed++;
        this.<say>P("analytics: FAIL video: no video playing (missing asset?)");
      }
      else
      {
        this.Mark();
        await this.OnUi(new Action(companion.OnEscapePressed));
        await this.ObExpectValue("video escape", "onboarding_video_escape_pressed", "press_type", "armed_skip", 1500);
        await this.OnUi(new Action(companion.OnEscapePressed));
        await this.ObExpectValue("video escape", "onboarding_video_escape_pressed", "press_type", "confirmed_skip", 1500);
        await this.Expect("video escape", new ValueTuple<string, string[]>[]
        {
          AnalyticsEndToEnd.ObE("onboarding_video_escape_pressed", new string[] { "press_type", "seconds_remaining" }),
          AnalyticsEndToEnd.ObE("onboarding_skipped_by_shake", Array.Empty<string>())
        }, 1500);
        await this.Expect("invite", new ValueTuple<string, string[]>[] { AnalyticsEndToEnd.ObE("onboarding_tutorial_invite_presented", Array.Empty<string>()) }, 8000);
        this.Mark();
        HotkeyRouter hotkeys = this.<host>P.Hotkeys;
        if (hotkeys != null)
        {
          hotkeys.AllowsTextMode();
        }
        await this.ObExpectValue("invite bypass", "onboarding_tutorial_resolved", "outcome", "invite_bypassed_by_text", 1500);
        await AnalyticsEndToEnd.Wait(500);
        this.Mark();
        await this.OnUi(delegate
        {
          this.<host>P.DemoIntroSequence(true);
        });
        await this.Expect("video end", new ValueTuple<string, string[]>[] { AnalyticsEndToEnd.ObE("onboarding_video_started", new string[] { "video_kind" }) }, 30000);
        this.Mark();
        await this.OnUi(delegate
        {
          companion.SeekNearEnd(1.0);
        });
        await this.Expect("video end", new ValueTuple<string, string[]>[] { AnalyticsEndToEnd.ObE("onboarding_video_completed", Array.Empty<string>()) }, 8000);
        await this.Expect("invite", new ValueTuple<string, string[]>[] { AnalyticsEndToEnd.ObE("onboarding_tutorial_invite_presented", Array.Empty<string>()) }, 8000);
        this.Mark();
        HotkeyRouter hotkeys2 = this.<host>P.Hotkeys;
        if (hotkeys2 != null)
        {
          hotkeys2.AllowsPushToTalk();
        }
        await this.ObExpectValue("invite bypass", "onboarding_tutorial_resolved", "outcome", "invite_bypassed_by_ptt", 1500);
      }
    }

    // Token: 0x06002421 RID: 9249 RVA: 0x00631110 File Offset: 0x00631110
    private async Task ObTutorialAsync()
    {
      this.Mark();
      await this.OnUi(new Action(this.<host>P.StartTutorial));
      await this.Expect("tour start", new ValueTuple<string, string[]>[]
      {
        AnalyticsEndToEnd.ObE("onboarding_tutorial_started", Array.Empty<string>()),
        AnalyticsEndToEnd.ObE("onboarding_tutorial_step_viewed", new string[] { "step", "navigation" })
      }, 1500);
      await this.ObExpectValue("tour start", "onboarding_tutorial_step_viewed", "navigation", "start", 1500);
      TutorialWindow tour = this.<host>P.DemoTutorialWindow();
      if (tour == null)
      {
        this.failed++;
        this.<say>P("analytics: FAIL tour: no tour window");
      }
      else
      {
        this.Mark();
        TutorialWindow.Step reached = TutorialWindow.Step.MicCheck;
        await this.OnUi(delegate
        {
          tour.MarkCompleted(TutorialWindow.Step.MicCheck);
          tour.Advance();
          reached = tour.CurrentStep;
        });
        await this.OnUi(new Action(tour.ToggleMusic));
        await this.OnUi(new Action(tour.Back));
        await this.OnUi(delegate
        {
          tour.Go(reached, "jump");
        });
        await this.Expect("tour steps", new ValueTuple<string, string[]>[]
        {
          AnalyticsEndToEnd.ObE("onboarding_tutorial_step_completed", new string[] { "step" }),
          AnalyticsEndToEnd.ObE("onboarding_tutorial_step_viewed", new string[] { "step", "navigation" }),
          AnalyticsEndToEnd.ObE("onboarding_tutorial_music_toggled", new string[] { "muted" })
        }, 1500);
        await this.ObExpectValue("tour steps", "onboarding_tutorial_step_completed", "step", "mic_check", 1500);
        await this.ObExpectValue("tour steps", "onboarding_tutorial_step_viewed", "navigation", "advance", 1500);
        await this.ObExpectValue("tour steps", "onboarding_tutorial_step_viewed", "navigation", "back", 1500);
        await this.ObExpectValue("tour steps", "onboarding_tutorial_step_viewed", "navigation", "jump", 1500);
        this.Mark();
        await this.OnUi(new Action(this.<host>P.ResumeTutorial));
        await this.Expect("tour resume", new ValueTuple<string, string[]>[] { AnalyticsEndToEnd.ObE("onboarding_tutorial_resumed_from_notch", Array.Empty<string>()) }, 1500);
        this.Mark();
        await this.OnUi(delegate
        {
          tour.End("skipped");
        });
        await this.Expect("tour skip", new ValueTuple<string, string[]>[] { AnalyticsEndToEnd.ObE("onboarding_tutorial_resolved", new string[] { "outcome", "last_step", "duration_seconds" }) }, 1500);
        await this.ObExpectValue("tour skip", "onboarding_tutorial_resolved", "outcome", "skipped", 1500);
        for (int i = 0; i < 30; i++)
        {
          BillingUi billing = this.<host>P.Billing;
          if (billing == null || billing.IsPaywallVisible)
          {
            break;
          }
          await AnalyticsEndToEnd.Wait(100);
        }
        await this.OnUi(delegate
        {
          BillingUi billing2 = this.<host>P.Billing;
          if (billing2 != null && billing2.IsPaywallVisible)
          {
            billing2.DismissPaywall();
          }
        });
      }
    }

    // Token: 0x06002422 RID: 9250 RVA: 0x00631154 File Offset: 0x00631154
    private async Task ObInterviewAsync()
    {
      InterviewController interview = this.<host>P.Interview;
      if (interview == null)
      {
        this.failed++;
        this.<say>P("analytics: FAIL interview: no interview controller");
      }
      else
      {
        this.Mark();
        await this.OnUi(delegate
        {
          interview.Start();
          Func<string> liveStateNote = FlowHooks.LiveStateNote;
          if (liveStateNote != null)
          {
            liveStateNote();
          }
          Func<bool> skipInterview = FlowHooks.SkipInterview;
          if (skipInterview == null)
          {
            return;
          }
          skipInterview();
        });
        await this.OnUi(new Action(interview.Stop));
        await this.Expect("interview", new ValueTuple<string, string[]>[]
        {
          AnalyticsEndToEnd.ObE("home_space_new_clicky_interview_started", new string[] { "mode", "interview_id" }),
          AnalyticsEndToEnd.ObE("clicky_interview_answer_submitted", new string[] { "mode", "source", "answer_number", "interview_id" }),
          AnalyticsEndToEnd.ObE("onboarding_interview_skipped_by_voice", new string[] { "interview_id", "answer_count" }),
          AnalyticsEndToEnd.ObE("home_space_new_clicky_interview_ended", new string[] { "founded", "mode", "answer_count", "interview_id", "duration_seconds" })
        }, 1500);
        await this.ObExpectValue("interview", "clicky_interview_answer_submitted", "source", "voice", 1500);
        await this.ObExpectValue("interview", "clicky_interview_answer_submitted", "interview_id", interview.InterviewId, 1500);
      }
    }

    // Token: 0x06002423 RID: 9251 RVA: 0x00631198 File Offset: 0x00631198
    private async Task ObRevealAsync()
    {
      HomeTour reveal = null;
      Task<bool> run = null;
      this.Mark();
      await this.OnUi(delegate
      {
        StarterCast.Unlock();
        reveal = new HomeTour(this.<host>P, this.<host>P.Interview);
        run = reveal.RunAsync();
      });
      await this.Expect("reveal", new ValueTuple<string, string[]>[] { AnalyticsEndToEnd.ObE("onboarding_reveal_started", new string[] { "clicky_count", "suggestion_count", "interview_id", "is_debug_preview" }) }, 1500);
      await this.OnUi(delegate
      {
        reveal.AbortForEscape();
      });
      await Task.WhenAny(run, AnalyticsEndToEnd.Wait(20000));
      await this.Expect("reveal", new ValueTuple<string, string[]>[] { AnalyticsEndToEnd.ObE("onboarding_reveal_ended", new string[] { "interview_id", "outcome", "duration_seconds" }) }, 3000);
      await this.ObExpectValue("reveal", "onboarding_reveal_ended", "outcome", "aborted", 1500);
    }

    // Token: 0x04001188 RID: 4488
    [CompilerGenerated]
    private AppHost <host>P = host;

    // Token: 0x04001189 RID: 4489
    [CompilerGenerated]
    private string <outDir>P = outDir;

    // Token: 0x0400118A RID: 4490
    [CompilerGenerated]
    private Action<string> <say>P = say;

    // Token: 0x0400118B RID: 4491
    private int passed;

    // Token: 0x0400118C RID: 4492
    private int failed;

    // Token: 0x0400118D RID: 4493
    private DateTimeOffset mark = DateTimeOffset.MinValue;

    // Token: 0x020009A6 RID: 2470
    [NullableContext(2)]
    [Nullable(0)]
    private sealed class ObStubPermissions : IPermissionService
    {
      // Token: 0x17000868 RID: 2152
      // (get) Token: 0x06003DE3 RID: 15843 RVA: 0x0069BC34 File Offset: 0x0069BC34
      // (set) Token: 0x06003DE4 RID: 15844 RVA: 0x0069BC3C File Offset: 0x0069BC3C
      public PermissionStatus Next { get; set; }

      // Token: 0x1400008F RID: 143
      // (add) Token: 0x06003DE5 RID: 15845 RVA: 0x0069BC48 File Offset: 0x0069BC48
      // (remove) Token: 0x06003DE6 RID: 15846 RVA: 0x0069BC80 File Offset: 0x0069BC80
      public event EventHandler Changed;

      // Token: 0x06003DE7 RID: 15847 RVA: 0x0069BCB8 File Offset: 0x0069BCB8
      public PermissionStatus GetStatus(PermissionKind kind)
      {
        return this.status[kind];
      }

      // Token: 0x06003DE8 RID: 15848 RVA: 0x0069BCC8 File Offset: 0x0069BCC8
      [NullableContext(1)]
      public Task<PermissionStatus> RequestAsync(PermissionKind kind)
      {
        this.Set(kind, this.Next);
        return Task.FromResult<PermissionStatus>(this.status[kind]);
      }

      // Token: 0x06003DE9 RID: 15849 RVA: 0x0069BCE8 File Offset: 0x0069BCE8
      public void OpenSystemSettings(PermissionKind kind)
      {
      }

      // Token: 0x06003DEA RID: 15850 RVA: 0x0069BCEC File Offset: 0x0069BCEC
      public void StartPolling()
      {
      }

      // Token: 0x06003DEB RID: 15851 RVA: 0x0069BCF0 File Offset: 0x0069BCF0
      public void StopPolling()
      {
      }

      // Token: 0x06003DEC RID: 15852 RVA: 0x0069BCF4 File Offset: 0x0069BCF4
      public void Set(PermissionKind kind, PermissionStatus value)
      {
        this.status[kind] = value;
        EventHandler changed = this.Changed;
        if (changed == null)
        {
          return;
        }
        changed(this, EventArgs.Empty);
      }

      // Token: 0x06003DED RID: 15853 RVA: 0x0069BD1C File Offset: 0x0069BD1C
      public ObStubPermissions()
      {
        Dictionary<PermissionKind, PermissionStatus> dictionary = new Dictionary<PermissionKind, PermissionStatus>();
        dictionary[PermissionKind.Microphone] = PermissionStatus.NotDetermined;
        dictionary[PermissionKind.ScreenCapture] = PermissionStatus.NotDetermined;
        this.status = dictionary;
        this.Next = PermissionStatus.Granted;
        base..ctor();
      }

      // Token: 0x0400285C RID: 10332
      [Nullable(1)]
      private readonly Dictionary<PermissionKind, PermissionStatus> status;
    }
  }
}
