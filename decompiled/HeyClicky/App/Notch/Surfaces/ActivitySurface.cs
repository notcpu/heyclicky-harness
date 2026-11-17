using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Portraits;
using HeyClicky.App.Services;
using HeyClicky.App.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Windows.UI;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x0200014B RID: 331
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ActivitySurface : NotchSurface
  {
    // Token: 0x060012A2 RID: 4770 RVA: 0x00598A4C File Offset: 0x00598A4C
    public ActivitySurface(ActivityPhase phase, string label, bool recording, Func<Color> cursorColor, double? compactGap = null)
    {
      this.cursorColor = cursorColor;
      this.isCompact = compactGap != null;
      this.root = new Grid
      {
        Margin = new Thickness((double)(this.isCompact ? 12 : 14), 0.0, (double)(this.isCompact ? 12 : 14), 0.0),
        ColumnSpacing = compactGap.GetValueOrDefault(8.0)
      };
      this.root.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      ColumnDefinitionCollection columnDefinitions = this.root.ColumnDefinitions;
      ColumnDefinition columnDefinition2;
      if (!this.isCompact)
      {
        ColumnDefinition columnDefinition = new ColumnDefinition();
        columnDefinition.Width = new GridLength(1.0, GridUnitType.Star);
        columnDefinition2 = columnDefinition;
        columnDefinition.MinWidth = 6.0;
      }
      else
      {
        (columnDefinition2 = new ColumnDefinition()).Width = new GridLength(0.0);
      }
      columnDefinitions.Add(columnDefinition2);
      this.root.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      if (this.isCompact)
      {
        this.root.ColumnDefinitions.RemoveAt(1);
        Grid.SetColumn(this.indicatorSlot, 1);
        this.root.HorizontalAlignment = HorizontalAlignment.Left;
      }
      else
      {
        Grid.SetColumn(this.indicatorSlot, 2);
      }
      this.root.Children.Add(this.labelSlot);
      this.root.Children.Add(this.indicatorSlot);
      this.Set(phase, label, recording);
    }

    // Token: 0x060012A3 RID: 4771 RVA: 0x00598C24 File Offset: 0x00598C24
    public void AttachAsPill(AppServices services)
    {
      this.pillServices = services;
      this.StartLevelPump();
    }

    // Token: 0x060012A4 RID: 4772 RVA: 0x00598C34 File Offset: 0x00598C34
    public void DetachPill()
    {
      DispatcherQueueTimer dispatcherQueueTimer = this.levelTimer;
      if (dispatcherQueueTimer == null)
      {
        return;
      }
      dispatcherQueueTimer.Stop();
    }

    // Token: 0x060012A5 RID: 4773 RVA: 0x00598C48 File Offset: 0x00598C48
    public bool Matches(ActivityPhase phase, string label)
    {
      return this.phase == phase && (phase != ActivityPhase.Custom || this.label == label);
    }

    // Token: 0x17000364 RID: 868
    // (get) Token: 0x060012A6 RID: 4774 RVA: 0x00598C68 File Offset: 0x00598C68
    public override FrameworkElement View
    {
      get
      {
        return this.root;
      }
    }

    // Token: 0x17000365 RID: 869
    // (get) Token: 0x060012A7 RID: 4775 RVA: 0x00598C70 File Offset: 0x00598C70
    public override VerticalAlignment ContentAlignment
    {
      get
      {
        return VerticalAlignment.Center;
      }
    }

    // Token: 0x060012A8 RID: 4776 RVA: 0x00598C74 File Offset: 0x00598C74
    public static string DisplayLabel(ActivityPhase p, string custom)
    {
      switch (p)
      {
      case ActivityPhase.Listening:
        return "Listening";
      case ActivityPhase.Thinking:
        return "Thinking";
      case ActivityPhase.Speaking:
        return "Speaking";
      case ActivityPhase.AgentMulti:
        return "Agents";
      case ActivityPhase.Docked:
        return "HeyClicky is docked";
      }
      return custom;
    }

    // Token: 0x060012A9 RID: 4777 RVA: 0x00598CCC File Offset: 0x00598CCC
    [return: Nullable(new byte[] { 0, 1 })]
    public static ValueTuple<ActivityPhase, string, bool>? Classify(NotchLiveEvent e)
    {
      ValueTuple<ActivityPhase, string, bool>? valueTuple;
      if (!(e is NotchLiveEvent.ActivityListening))
      {
        if (!(e is NotchLiveEvent.ActivityThinking))
        {
          if (!(e is NotchLiveEvent.ActivitySpeaking))
          {
            NotchLiveEvent.ActivityCustomStatus activityCustomStatus = e as NotchLiveEvent.ActivityCustomStatus;
            if (activityCustomStatus == null)
            {
              NotchLiveEvent.AgentMultiActivity agentMultiActivity = e as NotchLiveEvent.AgentMultiActivity;
              if (agentMultiActivity == null)
              {
                if (!(e is NotchLiveEvent.IntroHatchPeek))
                {
                  NotchLiveEvent.Dictation dictation = e as NotchLiveEvent.Dictation;
                  if (dictation == null)
                  {
                    valueTuple = null;
                  }
                  else
                  {
                    valueTuple = new ValueTuple<ActivityPhase, string, bool>?(new ValueTuple<ActivityPhase, string, bool>(ActivityPhase.Dictation, dictation.Label, dictation.IsRecording));
                  }
                }
                else
                {
                  valueTuple = new ValueTuple<ActivityPhase, string, bool>?(new ValueTuple<ActivityPhase, string, bool>(ActivityPhase.Docked, "", false));
                }
              }
              else
              {
                valueTuple = new ValueTuple<ActivityPhase, string, bool>?(new ValueTuple<ActivityPhase, string, bool>(ActivityPhase.AgentMulti, agentMultiActivity.LongestLabel, false));
              }
            }
            else
            {
              valueTuple = new ValueTuple<ActivityPhase, string, bool>?(new ValueTuple<ActivityPhase, string, bool>(ActivityPhase.Custom, activityCustomStatus.Label, false));
            }
          }
          else
          {
            valueTuple = new ValueTuple<ActivityPhase, string, bool>?(new ValueTuple<ActivityPhase, string, bool>(ActivityPhase.Speaking, "", false));
          }
        }
        else
        {
          valueTuple = new ValueTuple<ActivityPhase, string, bool>?(new ValueTuple<ActivityPhase, string, bool>(ActivityPhase.Thinking, "", false));
        }
      }
      else
      {
        valueTuple = new ValueTuple<ActivityPhase, string, bool>?(new ValueTuple<ActivityPhase, string, bool>(ActivityPhase.Listening, "", false));
      }
      return valueTuple;
    }

    // Token: 0x060012AA RID: 4778 RVA: 0x00598DC4 File Offset: 0x00598DC4
    public override bool CanUpdate(NotchPresentation p)
    {
      NotchPresentation.LiveEvent liveEvent = NotchGeometry.Shown(p) as NotchPresentation.LiveEvent;
      if (liveEvent != null)
      {
        NotchLiveEvent @event = liveEvent.Event;
        return ActivitySurface.Classify(@event) != null;
      }
      return false;
    }

    // Token: 0x060012AB RID: 4779 RVA: 0x00598E00 File Offset: 0x00598E00
    public override void Update(NotchPresentation p)
    {
      NotchPresentation.LiveEvent liveEvent = NotchGeometry.Shown(p) as NotchPresentation.LiveEvent;
      if (liveEvent != null)
      {
        NotchLiveEvent @event = liveEvent.Event;
        ValueTuple<ActivityPhase, string, bool>? valueTuple = ActivitySurface.Classify(@event);
        if (valueTuple != null)
        {
          ValueTuple<ActivityPhase, string, bool> valueOrDefault = valueTuple.GetValueOrDefault();
          ActivityPhase item = valueOrDefault.Item1;
          string item2 = valueOrDefault.Item2;
          bool item3 = valueOrDefault.Item3;
          this.Set(item, item2, item3);
        }
      }
    }

    // Token: 0x060012AC RID: 4780 RVA: 0x00598E5C File Offset: 0x00598E5C
    protected override void OnAttached()
    {
      if (this.phase == ActivityPhase.AgentMulti)
      {
        this.labelSlot.Content = this.AgentMultiLabel();
        this.indicatorSlot.Content = this.AgentMultiDots();
      }
      else
      {
        ActivityPhase activityPhase = this.phase;
        bool flag = activityPhase == ActivityPhase.Thinking || activityPhase == ActivityPhase.Custom;
        if (flag && this.AgentDriving)
        {
          if (this.phase == ActivityPhase.Custom)
          {
            this.labelSlot.Content = this.AgentCustomLabel();
          }
          this.indicatorSlot.Content = ActivityIndicators.ThinkingDots(this.ThinkingTint(), 4.0, 3.0, 2.8, 0.85, 0.55, 0.45, 0.4, 0.6, 14.0);
        }
      }
      base.Host.MultiAgentRotated += this.OnMultiAgentRotated;
      base.Host.BindToEdges(this.labelSlot, -1.0, 0.5);
      base.Host.BindToEdges(this.indicatorSlot, 1.0, 0.5);
      this.StartLevelPump();
    }

    // Token: 0x060012AD RID: 4781 RVA: 0x00598FA4 File Offset: 0x00598FA4
    public override void Detached()
    {
      DispatcherQueueTimer dispatcherQueueTimer = this.levelTimer;
      if (dispatcherQueueTimer != null)
      {
        dispatcherQueueTimer.Stop();
      }
      base.Host.MultiAgentRotated -= this.OnMultiAgentRotated;
    }

    // Token: 0x060012AE RID: 4782 RVA: 0x00598FD0 File Offset: 0x00598FD0
    private void Set(ActivityPhase newPhase, string newLabel, bool newRecording)
    {
      bool flag = newPhase != this.phase || this.labelSlot.Content == null;
      bool flag2 = flag || newRecording != this.recording;
      this.phase = newPhase;
      this.label = newLabel;
      this.recording = newRecording;
      if (this.phase == ActivityPhase.Docked)
      {
        this.labelSlot.Content = null;
      }
      else if (this.phase == ActivityPhase.AgentMulti)
      {
        this.labelSlot.Content = this.AgentMultiLabel();
      }
      else if (this.phase == ActivityPhase.Custom && this.AgentDriving)
      {
        this.labelSlot.Content = this.AgentCustomLabel();
      }
      else
      {
        string text = ActivitySurface.DisplayLabel(this.phase, (this.phase == ActivityPhase.Custom) ? ActivitySurface.ClampForStrip(this.label) : this.label);
        TextBlock textBlock = this.labelSlot.Content as TextBlock;
        if (textBlock != null && !flag)
        {
          textBlock.Text = text;
        }
        else
        {
          this.labelSlot.Content = ActivitySurface.StripText(text);
        }
      }
      if (flag2)
      {
        this.listeningClock = null;
        ContentControl contentControl = this.indicatorSlot;
        FrameworkElement frameworkElement;
        switch (this.phase)
        {
        case ActivityPhase.Listening:
          frameworkElement = this.Listening();
          goto IL_01EE;
        case ActivityPhase.Speaking:
          frameworkElement = ActivityIndicators.SpeakingBars(DS.Colors.SpeakingAccent);
          goto IL_01EE;
        case ActivityPhase.AgentMulti:
          if (base.Host != null)
          {
            frameworkElement = this.AgentMultiDots();
            goto IL_01EE;
          }
          break;
        case ActivityPhase.Docked:
          frameworkElement = ActivityIndicators.DockedCursor(this.cursorColor());
          goto IL_01EE;
        case ActivityPhase.Dictation:
          frameworkElement = ActivityIndicators.DictationDashes(DS.Colors.DictationAccent, this.recording);
          goto IL_01EE;
        }
        frameworkElement = ActivityIndicators.ThinkingDots(this.ThinkingTint(), 4.0, 3.0, 2.8, 0.85, 0.55, 0.45, 0.4, 0.6, 14.0);
        IL_01EE:
        contentControl.Content = frameworkElement;
      }
      if (base.Host != null || this.pillServices != null)
      {
        this.StartLevelPump();
      }
    }

    // Token: 0x060012AF RID: 4783 RVA: 0x005991EC File Offset: 0x005991EC
    private FrameworkElement Listening()
    {
      ValueTuple<FrameworkElement, CompositionPropertySet> valueTuple = ActivityIndicators.ListeningBars(DS.Colors.ListeningAccent);
      FrameworkElement item = valueTuple.Item1;
      CompositionPropertySet item2 = valueTuple.Item2;
      this.listeningClock = item2;
      return item;
    }

    // Token: 0x060012B0 RID: 4784 RVA: 0x00599218 File Offset: 0x00599218
    private void StartLevelPump()
    {
      if (this.phase == ActivityPhase.Listening)
      {
        if (this.levelTimer == null)
        {
          this.levelTimer = DispatcherQueue.GetForCurrentThread().CreateTimer();
          this.levelTimer.Interval = TimeSpan.FromMilliseconds(33L, 0L);
          this.levelTimer.Tick += delegate(DispatcherQueueTimer _, object _)
          {
            CompositionPropertySet compositionPropertySet = this.listeningClock;
            if (compositionPropertySet == null)
            {
              return;
            }
            compositionPropertySet.InsertScalar("Level", Math.Max(0.15f, Math.Min(1f, (this.pillServices ?? base.Host.Services).Microphone.Level)));
          };
        }
        this.levelTimer.Start();
        return;
      }
      DispatcherQueueTimer dispatcherQueueTimer = this.levelTimer;
      if (dispatcherQueueTimer == null)
      {
        return;
      }
      dispatcherQueueTimer.Stop();
    }

    // Token: 0x060012B1 RID: 4785 RVA: 0x00599290 File Offset: 0x00599290
    private static TextBlock StripText(string text)
    {
      TextBlock textBlock = Ui.Semibold(text, 11.0, DS.Colors.TextPrimary);
      Typography.SetNumeralAlignment(textBlock, FontNumeralAlignment.Tabular);
      return textBlock;
    }

    // Token: 0x17000366 RID: 870
    // (get) Token: 0x060012B2 RID: 4786 RVA: 0x005992B0 File Offset: 0x005992B0
    private bool AgentDriving
    {
      get
      {
        return base.Host != null && AgentColors.RunningTasks(base.Host.Services.Agents).Count > 0;
      }
    }

    // Token: 0x060012B3 RID: 4787 RVA: 0x005992DC File Offset: 0x005992DC
    private Color ThinkingTint()
    {
      Color? color = ((base.Host != null) ? AgentColors.FirstRunning(base.Host.Services.Agents) : null);
      if (color == null)
      {
        return DS.Colors.ThinkingAccent;
      }
      return color.GetValueOrDefault();
    }

    // Token: 0x060012B4 RID: 4788 RVA: 0x00599328 File Offset: 0x00599328
    private FrameworkElement AgentCustomLabel()
    {
      AgentTaskSnapshot agentTaskSnapshot = AgentColors.FirstRunningTask(base.Host.Services.Agents);
      PortraitView portraitView = ClickyFace.NotchBadge(((agentTaskSnapshot != null) ? agentTaskSnapshot.TaskId : null) ?? "agent-0", 26.0);
      if (base.Host.SizeContext.AgentLabelsHidden)
      {
        return portraitView;
      }
      TextBlock textBlock = ActivitySurface.StripText(ActivitySurface.ClampForStrip(this.label));
      textBlock.MaxWidth = 200.0;
      textBlock.TextTrimming = TextTrimming.CharacterEllipsis;
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      return new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 10.0,
        Children = { portraitView, textBlock }
      };
    }

    // Token: 0x060012B5 RID: 4789 RVA: 0x005993E4 File Offset: 0x005993E4
    private static string ClampForStrip(string s)
    {
      s = s.Trim();
      if (s.Length > 25)
      {
        return s.Substring(0, 24) + "…";
      }
      return s;
    }

    // Token: 0x060012B6 RID: 4790 RVA: 0x00599410 File Offset: 0x00599410
    [NullableContext(2)]
    private AgentTaskSnapshot DisplayedAgent()
    {
      if (base.Host != null)
      {
        return AgentColors.DisplayedRunningTask(base.Host.Services.Agents, base.Host.MultiAgentRotation, true);
      }
      return null;
    }

    // Token: 0x060012B7 RID: 4791 RVA: 0x00599440 File Offset: 0x00599440
    private FrameworkElement AgentMultiLabel()
    {
      AgentTaskSnapshot agentTaskSnapshot = this.DisplayedAgent();
      this.faceHost = new Grid
      {
        VerticalAlignment = VerticalAlignment.Center
      };
      this.faceHost.Children.Add(ClickyFace.NotchBadge(((agentTaskSnapshot != null) ? agentTaskSnapshot.TaskId : null) ?? "agent-0", 26.0));
      this.titleHost = null;
      this.currentTitle = null;
      INotchHost host = base.Host;
      if (host == null || host.SizeContext.AgentLabelsHidden)
      {
        return this.faceHost;
      }
      this.titleHost = new Grid
      {
        Height = 14.0,
        MaxWidth = 200.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      Ui.ClipRounded(this.titleHost, 0.0);
      this.currentTitle = ActivitySurface.TitleText(((agentTaskSnapshot != null) ? agentTaskSnapshot.Title : null) ?? "Agent working");
      this.titleHost.Children.Add(this.currentTitle);
      return new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 10.0,
        Children = { this.faceHost, this.titleHost }
      };
    }

    // Token: 0x060012B8 RID: 4792 RVA: 0x00599580 File Offset: 0x00599580
    private static TextBlock TitleText(string s)
    {
      return ActivitySurface.StripText(ActivitySurface.ClampForStrip(s));
    }

    // Token: 0x060012B9 RID: 4793 RVA: 0x00599590 File Offset: 0x00599590
    private FrameworkElement AgentMultiDots()
    {
      AgentTaskSnapshot agentTaskSnapshot = this.DisplayedAgent();
      return ActivityIndicators.ThinkingDots((agentTaskSnapshot != null) ? AgentColors.For(agentTaskSnapshot.TaskId, 0) : DS.Colors.ThinkingAccent, 4.0, 3.0, 2.8, 0.85, 0.55, 0.45, 0.4, 0.6, 14.0);
    }

    // Token: 0x060012BA RID: 4794 RVA: 0x00599610 File Offset: 0x00599610
    private void OnMultiAgentRotated()
    {
      if (this.phase == ActivityPhase.AgentMulti)
      {
        AgentTaskSnapshot agentTaskSnapshot = this.DisplayedAgent();
        if (agentTaskSnapshot != null)
        {
          CubicBezierEasingFunction cubicBezierEasingFunction = Easing.Expressive(Ui.Compositor);
          if (this.titleHost != null && this.currentTitle != null)
          {
            TextBlock textBlock = ActivitySurface.TitleText(agentTaskSnapshot.Title);
            TextBlock textBlock2 = this.currentTitle;
            textBlock.Translation = new Vector3(0f, 14f, 0f);
            textBlock.Opacity = 0.0;
            this.titleHost.Children.Add(textBlock);
            Anim.Translate(textBlock, Vector3.Zero, 0.34, cubicBezierEasingFunction);
            Anim.Opacity(textBlock, 1f, 0.34, cubicBezierEasingFunction, 0.0);
            Anim.Translate(textBlock2, new Vector3(0f, -14f, 0f), 0.34, cubicBezierEasingFunction);
            Anim.Opacity(textBlock2, 0f, 0.34, cubicBezierEasingFunction, 0.0);
            this.currentTitle = textBlock;
            ActivitySurface.RemoveAfterFade(this.titleHost, textBlock2);
          }
          if (this.faceHost != null)
          {
            PortraitView portraitView = ClickyFace.NotchBadge(agentTaskSnapshot.TaskId, 26.0);
            portraitView.Opacity = 0.0;
            foreach (UIElement uielement in this.faceHost.Children.ToList<UIElement>())
            {
              Anim.Opacity(uielement, 0f, 0.34, cubicBezierEasingFunction, 0.0);
              ActivitySurface.RemoveAfterFade(this.faceHost, uielement);
            }
            this.faceHost.Children.Add(portraitView);
            Anim.Opacity(portraitView, 1f, 0.34, cubicBezierEasingFunction, 0.0);
          }
          FrameworkElement frameworkElement = this.AgentMultiDots();
          frameworkElement.Opacity = 0.0;
          this.indicatorSlot.Content = frameworkElement;
          Anim.Opacity(frameworkElement, 1f, 0.34, cubicBezierEasingFunction, 0.0);
          return;
        }
      }
    }

    // Token: 0x060012BB RID: 4795 RVA: 0x00599844 File Offset: 0x00599844
    private static void RemoveAfterFade(Panel host, UIElement element)
    {
      DispatcherQueueTimer dispatcherQueueTimer = DispatcherQueue.GetForCurrentThread().CreateTimer();
      dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(0.4);
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        host.Children.Remove(element);
      };
      dispatcherQueueTimer.Start();
    }

    // Token: 0x04000897 RID: 2199
    private readonly Grid root;

    // Token: 0x04000898 RID: 2200
    private readonly ContentControl labelSlot = new ContentControl
    {
      VerticalAlignment = VerticalAlignment.Center,
      IsTabStop = false
    };

    // Token: 0x04000899 RID: 2201
    private readonly ContentControl indicatorSlot = new ContentControl
    {
      VerticalAlignment = VerticalAlignment.Center,
      HorizontalAlignment = HorizontalAlignment.Right,
      IsTabStop = false
    };

    // Token: 0x0400089A RID: 2202
    private ActivityPhase phase;

    // Token: 0x0400089B RID: 2203
    private string label = "";

    // Token: 0x0400089C RID: 2204
    private bool recording;

    // Token: 0x0400089D RID: 2205
    [Nullable(2)]
    private CompositionPropertySet listeningClock;

    // Token: 0x0400089E RID: 2206
    [Nullable(2)]
    private DispatcherQueueTimer levelTimer;

    // Token: 0x0400089F RID: 2207
    private readonly Func<Color> cursorColor;

    // Token: 0x040008A0 RID: 2208
    private readonly bool isCompact;

    // Token: 0x040008A1 RID: 2209
    [Nullable(2)]
    private AppServices pillServices;

    // Token: 0x040008A2 RID: 2210
    [Nullable(2)]
    private Grid faceHost;

    // Token: 0x040008A3 RID: 2211
    [Nullable(2)]
    private Grid titleHost;

    // Token: 0x040008A4 RID: 2212
    [Nullable(2)]
    private TextBlock currentTitle;
  }
}
