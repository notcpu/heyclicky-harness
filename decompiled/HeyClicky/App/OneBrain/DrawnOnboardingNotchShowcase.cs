using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using HeyClicky.App.Proactive;
using HeyClicky.App.UI;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Proactive;
using HeyClicky.Core.Telemetry;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App.OneBrain
{
  // Token: 0x02000116 RID: 278
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DrawnOnboardingNotchShowcase
  {
    // Token: 0x170002AE RID: 686
    // (get) Token: 0x06000EBC RID: 3772 RVA: 0x0057606C File Offset: 0x0057606C
    public static DrawnOnboardingNotchShowcase Shared { get; } = new DrawnOnboardingNotchShowcase();

    // Token: 0x170002AF RID: 687
    // (get) Token: 0x06000EBD RID: 3773 RVA: 0x00576074 File Offset: 0x00576074
    public IReadOnlyList<string> SampleIntegrationIds
    {
      get
      {
        return this.sampleIntegrationIds;
      }
    }

    // Token: 0x170002B0 RID: 688
    // (get) Token: 0x06000EBE RID: 3774 RVA: 0x0057607C File Offset: 0x0057607C
    // (set) Token: 0x06000EBF RID: 3775 RVA: 0x00576084 File Offset: 0x00576084
    public bool IsShowing { get; private set; }

    // Token: 0x170002B1 RID: 689
    // (get) Token: 0x06000EC0 RID: 3776 RVA: 0x00576090 File Offset: 0x00576090
    // (set) Token: 0x06000EC1 RID: 3777 RVA: 0x00576098 File Offset: 0x00576098
    public bool ShowsDock { get; private set; }

    // Token: 0x14000032 RID: 50
    // (add) Token: 0x06000EC2 RID: 3778 RVA: 0x005760A4 File Offset: 0x005760A4
    // (remove) Token: 0x06000EC3 RID: 3779 RVA: 0x005760DC File Offset: 0x005760DC
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Changed;

    // Token: 0x06000EC4 RID: 3780 RVA: 0x00576114 File Offset: 0x00576114
    private static void Track(string action, [Nullable(2)] JsonObject properties = null)
    {
      ClickyTourJobCards.TrackFlowAction(action, GoodMorningBrief.IsPlaying && !DrawnOnboardingTour.IsRunning, properties);
    }

    // Token: 0x06000EC5 RID: 3781 RVA: 0x00576134 File Offset: 0x00576134
    private DrawnOnboardingNotchShowcase()
    {
    }

    // Token: 0x06000EC6 RID: 3782 RVA: 0x00576148 File Offset: 0x00576148
    public void Show(AppHost host)
    {
      DrawnOnboardingNotchShowcase.<>c__DisplayClass29_0 CS$<>8__locals1 = new DrawnOnboardingNotchShowcase.<>c__DisplayClass29_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.host = host;
      if (this.IsShowing)
      {
        return;
      }
      this.IsShowing = true;
      this.ShowsDock = true;
      this.hasPoppedApps = false;
      this.holdOpenUntil = Environment.TickCount64 + 4500L;
      this.openedAt = new long?(Environment.TickCount64);
      DrawnOnboardingNotchShowcase.<>c__DisplayClass29_0 CS$<>8__locals2 = CS$<>8__locals1;
      int num = this.generation + 1;
      this.generation = num;
      CS$<>8__locals2.showGeneration = num;
      DrawnOnboardingNotchShowcase.Track("notch_shown", null);
      RealStack real = CS$<>8__locals1.host.Real;
      if (real != null)
      {
        real.Canvas.SetBelowNotch(true);
      }
      Action<bool> setDockTourShowcase = DrawnOnboardingNotchShowcase.SetDockTourShowcase;
      if (setDockTourShowcase != null)
      {
        setDockTourShowcase(true);
      }
      Action changed = this.Changed;
      if (changed != null)
      {
        changed();
      }
      Action openNotchOnHome = DrawnOnboardingNotchShowcase.OpenNotchOnHome;
      if (openNotchOnHome != null)
      {
        openNotchOnHome();
      }
      else
      {
        Log.Info("drawn tour", "notch shown (no notch Home to open in this build)");
      }
      Sounds.Play("clicky-text-open", 0.3);
      Log.Info("drawn tour", "notch shown");
      DrawnOnboardingNotchShowcase.After(CS$<>8__locals1.host, 7.0, delegate
      {
        if (CS$<>8__locals1.<>4__this.IsShowing && CS$<>8__locals1.<>4__this.generation == CS$<>8__locals1.showGeneration)
        {
          CS$<>8__locals1.<>4__this.PopApps(CS$<>8__locals1.host, false);
        }
      });
      DrawnOnboardingNotchShowcase.After(CS$<>8__locals1.host, 22.0, delegate
      {
        if (CS$<>8__locals1.<>4__this.IsShowing && CS$<>8__locals1.<>4__this.generation == CS$<>8__locals1.showGeneration)
        {
          CS$<>8__locals1.<>4__this.Close(CS$<>8__locals1.host, "timeout");
        }
      });
    }

    // Token: 0x06000EC7 RID: 3783 RVA: 0x0057628C File Offset: 0x0057628C
    public void PopApps(AppHost host, bool fromBeat = true)
    {
      if (!this.IsShowing)
      {
        this.Show(host);
      }
      if (this.hasPoppedApps)
      {
        return;
      }
      this.hasPoppedApps = true;
      int showGeneration = this.generation;
      ProactiveCoordinator proactive = host.Proactive;
      IReadOnlyList<AgentIntegrationInfo> readOnlyList = DrawnOnboardingNotchShowcase.SampleIntegrations(((proactive != null) ? proactive.Integrations : null) ?? Array.Empty<AgentIntegrationInfo>());
      string text = "apps_popped";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["count"] = readOnlyList.Count;
      jsonObject["from_beat"] = fromBeat;
      DrawnOnboardingNotchShowcase.Track(text, jsonObject);
      long num = Environment.TickCount64 + (long)((0.5 + 0.55 * (double)readOnlyList.Count + 2.5) * 1000.0);
      this.holdOpenUntil = Math.Max(this.holdOpenUntil, num);
      for (int i = 0; i < readOnlyList.Count; i++)
      {
        AgentIntegrationInfo integration = readOnlyList[i];
        DrawnOnboardingNotchShowcase.After(host, 0.5 + 0.55 * (double)i, delegate
        {
          if (!this.IsShowing || this.generation != showGeneration)
          {
            return;
          }
          this.sampleIntegrationIds.Add(integration.Id);
          Action changed = this.Changed;
          if (changed != null)
          {
            changed();
          }
          Sounds.Play("skill-up", 0.3);
        });
      }
    }

    // Token: 0x06000EC8 RID: 3784 RVA: 0x005763D8 File Offset: 0x005763D8
    public void Close(AppHost host, string reason = "tour_end")
    {
      DrawnOnboardingNotchShowcase.<>c__DisplayClass31_0 CS$<>8__locals1 = new DrawnOnboardingNotchShowcase.<>c__DisplayClass31_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.host = host;
      if (!this.IsShowing)
      {
        return;
      }
      this.IsShowing = false;
      string text = "notch_closed";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["reason"] = reason;
      string text2 = "seconds_open";
      long? num = this.openedAt;
      double num2;
      if (num != null)
      {
        long valueOrDefault = num.GetValueOrDefault();
        num2 = (double)(Environment.TickCount64 - valueOrDefault) / 1000.0;
      }
      else
      {
        num2 = 0.0;
      }
      jsonObject[text2] = ClickyAnalytics.RoundedSeconds(num2);
      jsonObject["apps_popped"] = this.hasPoppedApps;
      DrawnOnboardingNotchShowcase.Track(text, jsonObject);
      this.openedAt = null;
      DrawnOnboardingNotchShowcase.<>c__DisplayClass31_0 CS$<>8__locals2 = CS$<>8__locals1;
      int num3 = this.generation + 1;
      this.generation = num3;
      CS$<>8__locals2.closeGeneration = num3;
      Action foldNotch = DrawnOnboardingNotchShowcase.FoldNotch;
      if (foldNotch != null)
      {
        foldNotch();
      }
      else
      {
        CS$<>8__locals1.host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.AgentPeek));
      }
      Log.Info("drawn tour", "notch closed");
      DrawnOnboardingNotchShowcase.After(CS$<>8__locals1.host, 0.7, delegate
      {
        if (CS$<>8__locals1.<>4__this.generation != CS$<>8__locals1.closeGeneration)
        {
          return;
        }
        RealStack real = CS$<>8__locals1.host.Real;
        if (real != null)
        {
          real.Canvas.SetBelowNotch(false);
        }
        Action<bool> setDockTourShowcase = DrawnOnboardingNotchShowcase.SetDockTourShowcase;
        if (setDockTourShowcase != null)
        {
          setDockTourShowcase(false);
        }
        CS$<>8__locals1.<>4__this.ShowsDock = false;
        CS$<>8__locals1.<>4__this.sampleIntegrationIds.Clear();
        Action changed = CS$<>8__locals1.<>4__this.Changed;
        if (changed == null)
        {
          return;
        }
        changed();
      });
    }

    // Token: 0x06000EC9 RID: 3785 RVA: 0x0057651C File Offset: 0x0057651C
    public void FadeDrawingAway(AppHost host)
    {
      if (!this.IsShowing)
      {
        return;
      }
      double num = Math.Max(0.0, (double)(this.holdOpenUntil - Environment.TickCount64) / 1000.0);
      int showGeneration = this.generation;
      DrawnOnboardingNotchShowcase.After(host, num, delegate
      {
        if (this.generation == showGeneration)
        {
          this.Close(host, "fade_away");
        }
      });
    }

    // Token: 0x06000ECA RID: 3786 RVA: 0x00576590 File Offset: 0x00576590
    public void Reset(AppHost host)
    {
      this.Close(host, "reset");
    }

    // Token: 0x06000ECB RID: 3787 RVA: 0x005765A0 File Offset: 0x005765A0
    public static IReadOnlyList<AgentIntegrationInfo> SampleIntegrations(IReadOnlyList<AgentIntegrationInfo> catalog)
    {
      List<AgentIntegrationInfo> sample = new List<AgentIntegrationInfo>();
      string[] preferredAppKeys = DrawnOnboardingNotchShowcase.PreferredAppKeys;
      for (int j = 0; j < preferredAppKeys.Length; j++)
      {
        string key = preferredAppKeys[j];
        AgentIntegrationInfo agentIntegrationInfo = catalog.FirstOrDefault<AgentIntegrationInfo>((AgentIntegrationInfo i) => DrawnOnboardingNotchShowcase.<SampleIntegrations>g__Matches|34_0(i, key) && sample.All<AgentIntegrationInfo>((AgentIntegrationInfo s) => s.Id != i.Id));
        if (agentIntegrationInfo != null)
        {
          sample.Add(agentIntegrationInfo);
        }
      }
      using (IEnumerator<AgentIntegrationInfo> enumerator = catalog.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          AgentIntegrationInfo integration = enumerator.Current;
          if (sample.Count >= 4)
          {
            break;
          }
          if (sample.All<AgentIntegrationInfo>((AgentIntegrationInfo s) => s.Id != integration.Id))
          {
            sample.Add(integration);
          }
        }
      }
      return sample;
    }

    // Token: 0x06000ECC RID: 3788 RVA: 0x0057669C File Offset: 0x0057669C
    private static void After(AppHost host, double seconds, Action action)
    {
      DispatcherQueueTimer t = host.DispatcherQueue.CreateTimer().Rooted();
      t.Interval = TimeSpan.FromSeconds(Math.Max(0.001, seconds));
      t.IsRepeating = false;
      t.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        t.Stop();
        action();
      };
      t.Start();
    }

    // Token: 0x06000ECE RID: 3790 RVA: 0x00576754 File Offset: 0x00576754
    [CompilerGenerated]
    internal unsafe static bool <SampleIntegrations>g__Matches|34_0(AgentIntegrationInfo integration, string key)
    {
      string text = " ";
      <>y__InlineArray3<string> <>y__InlineArray = default(<>y__InlineArray3<string>);
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray3<string>, string>(ref <>y__InlineArray, 0) = integration.Id;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray3<string>, string>(ref <>y__InlineArray, 1) = integration.PrimaryToolkitSlug;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray3<string>, string>(ref <>y__InlineArray, 2) = integration.DisplayName;
      return string.Join(text, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray3<string>, string>(in <>y__InlineArray, 3)).ToLowerInvariant().Replace(" ", "")
        .Contains(key);
    }

    // Token: 0x040005FA RID: 1530
    private const string Area = "drawn tour";

    // Token: 0x040005FC RID: 1532
    [Nullable(2)]
    public static Action OpenNotchOnHome;

    // Token: 0x040005FD RID: 1533
    [Nullable(2)]
    public static Action FoldNotch;

    // Token: 0x040005FE RID: 1534
    [Nullable(2)]
    public static Action<bool> SetDockTourShowcase;

    // Token: 0x040005FF RID: 1535
    private readonly List<string> sampleIntegrationIds = new List<string>();

    // Token: 0x04000603 RID: 1539
    private int generation;

    // Token: 0x04000604 RID: 1540
    private long holdOpenUntil;

    // Token: 0x04000605 RID: 1541
    private bool hasPoppedApps;

    // Token: 0x04000606 RID: 1542
    private long? openedAt;

    // Token: 0x04000607 RID: 1543
    private const double MinimumOpenSeconds = 4.5;

    // Token: 0x04000608 RID: 1544
    public static readonly string[] PreferredAppKeys = new string[] { "gmail", "notion", "figma", "github" };
  }
}
