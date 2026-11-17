using System;
using System.IO;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200020C RID: 524
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyEdgeStackInkCard : Grid
  {
    // Token: 0x17000470 RID: 1136
    // (get) Token: 0x0600191E RID: 6430 RVA: 0x005CEC38 File Offset: 0x005CEC38
    public ClickyInkCardSubject Thing { get; }

    // Token: 0x17000471 RID: 1137
    // (get) Token: 0x0600191F RID: 6431 RVA: 0x005CEC40 File Offset: 0x005CEC40
    public ClickyHomeInkNote Ink { get; }

    // Token: 0x17000472 RID: 1138
    // (get) Token: 0x06001920 RID: 6432 RVA: 0x005CEC48 File Offset: 0x005CEC48
    public ClickyEdgeStackInkPresentation Presentation { get; }

    // Token: 0x17000473 RID: 1139
    // (get) Token: 0x06001921 RID: 6433 RVA: 0x005CEC50 File Offset: 0x005CEC50
    [Nullable(2)]
    public string LiveStep
    {
      [NullableContext(2)]
      get;
    }

    // Token: 0x17000474 RID: 1140
    // (get) Token: 0x06001922 RID: 6434 RVA: 0x005CEC58 File Offset: 0x005CEC58
    public DateTimeOffset? LookedAt { get; }

    // Token: 0x17000475 RID: 1141
    // (get) Token: 0x06001923 RID: 6435 RVA: 0x005CEC60 File Offset: 0x005CEC60
    public ClickyInkActions Actions { get; }

    // Token: 0x17000476 RID: 1142
    // (get) Token: 0x06001924 RID: 6436 RVA: 0x005CEC68 File Offset: 0x005CEC68
    public double PulledRoomHeight { get; }

    // Token: 0x17000477 RID: 1143
    // (get) Token: 0x06001925 RID: 6437 RVA: 0x005CEC70 File Offset: 0x005CEC70
    public bool FillsScreen { get; }

    // Token: 0x17000478 RID: 1144
    // (get) Token: 0x06001926 RID: 6438 RVA: 0x005CEC78 File Offset: 0x005CEC78
    // (set) Token: 0x06001927 RID: 6439 RVA: 0x005CEC80 File Offset: 0x005CEC80
    public double? PulledHeight { get; private set; }

    // Token: 0x14000063 RID: 99
    // (add) Token: 0x06001928 RID: 6440 RVA: 0x005CEC8C File Offset: 0x005CEC8C
    // (remove) Token: 0x06001929 RID: 6441 RVA: 0x005CECC4 File Offset: 0x005CECC4
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<double> PulledHeightChanged;

    // Token: 0x0600192A RID: 6442 RVA: 0x005CECFC File Offset: 0x005CECFC
    [NullableContext(2)]
    public ClickyEdgeStackInkCard([Nullable(1)] ClickyInkCardSubject thing, [Nullable(1)] ClickyHomeInkNote ink, [Nullable(1)] ClickyEdgeStackInkPresentation presentation, string liveStep = null, DateTimeOffset? lookedAt = null, ClickyInkActions actions = null, Action onOpen = null, Action onPutBack = null, Action onUnpin = null, double pulledRoomHeight = 640.0, bool fillsScreen = false)
    {
      this.Thing = thing;
      this.Ink = ink;
      this.Presentation = presentation;
      this.LiveStep = liveStep;
      this.LookedAt = lookedAt;
      this.Actions = actions ?? new ClickyInkActions();
      Action action = onOpen;
      if (onOpen == null && (action = ClickyEdgeStackInkCard.<>c.<>9__37_0) == null)
      {
        action = (ClickyEdgeStackInkCard.<>c.<>9__37_0 = delegate
        {
        });
      }
      this.onOpen = action;
      Action action2 = onPutBack;
      if (onPutBack == null && (action2 = ClickyEdgeStackInkCard.<>c.<>9__37_1) == null)
      {
        action2 = (ClickyEdgeStackInkCard.<>c.<>9__37_1 = delegate
        {
        });
      }
      this.onPutBack = action2;
      Action action3 = onUnpin;
      if (onUnpin == null && (action3 = ClickyEdgeStackInkCard.<>c.<>9__37_2) == null)
      {
        action3 = (ClickyEdgeStackInkCard.<>c.<>9__37_2 = delegate
        {
        });
      }
      this.onUnpin = action3;
      this.PulledRoomHeight = pulledRoomHeight;
      this.FillsScreen = fillsScreen;
      this.isWriting = (DateTimeOffset.Now - ink.StartedAt).TotalSeconds < ink.Note.FinishedClock;
      this.timeline = new ClickyInkTimeline(new Func<DateTimeOffset, FrameworkElement>(this.Card), ClickyInkTimeline.Interval(this.Animates ? new double?(0.03333333333333333) : null));
      base.Children.Add(this.timeline);
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        this.WriteUntilThePenIsDone();
      };
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        DispatcherTimerHandle dispatcherTimerHandle = this.penDone;
        if (dispatcherTimerHandle != null)
        {
          dispatcherTimerHandle.Cancel();
        }
        this.penDone = null;
      };
    }

    // Token: 0x0600192B RID: 6443 RVA: 0x005CEE74 File Offset: 0x005CEE74
    public bool IsEquivalent(ClickyEdgeStackInkCard other)
    {
      if (this.Thing == other.Thing && this.Ink.StartedAt == other.Ink.StartedAt && this.Ink.Note.VersionCount == other.Ink.Note.VersionCount && this.Ink.Note.PenFreeAt == other.Ink.Note.PenFreeAt)
      {
        double? lastVersionAt = this.Ink.Note.LastVersionAt;
        double? lastVersionAt2 = other.Ink.Note.LastVersionAt;
        if (((lastVersionAt.GetValueOrDefault() == lastVersionAt2.GetValueOrDefault()) & (lastVersionAt != null == (lastVersionAt2 != null))) && this.Ink.Note.Pages.Count == other.Ink.Note.Pages.Count && this.Presentation == other.Presentation && this.LiveStep == other.LiveStep && this.LookedAt == other.LookedAt && this.Actions.Feedback.Equals(other.Actions.Feedback))
        {
          return this.PulledRoomHeight == other.PulledRoomHeight;
        }
      }
      return false;
    }

    // Token: 0x17000479 RID: 1145
    // (get) Token: 0x0600192C RID: 6444 RVA: 0x005CF010 File Offset: 0x005CF010
    private ClickyHomeSpreadPaper Paper
    {
      get
      {
        if (this.Thing.Kind != ClickyInkCardSubject.SubjectKind.Kept)
        {
          return ClickyHomeSpreadPaper.Note;
        }
        return ClickyHomeSpreadPaper.Of(this.Thing.Stock);
      }
    }

    // Token: 0x1700047A RID: 1146
    // (get) Token: 0x0600192D RID: 6445 RVA: 0x005CF038 File Offset: 0x005CF038
    private bool Animates
    {
      get
      {
        return this.isWriting && this.Presentation.LetsThePenMove && !Anim.ReduceMotion && !ClickyHomeRendering.IsOffscreen;
      }
    }

    // Token: 0x0600192E RID: 6446 RVA: 0x005CF060 File Offset: 0x005CF060
    private double StillClock(DateTimeOffset now)
    {
      double totalSeconds = (now - this.Ink.StartedAt).TotalSeconds;
      if (ClickyHomeRendering.IsOffscreen && this.Presentation.LetsThePenMove)
      {
        return totalSeconds;
      }
      return Math.Max(totalSeconds, this.Ink.Note.FinishedClock + 1.0);
    }

    // Token: 0x0600192F RID: 6447 RVA: 0x005CF0C0 File Offset: 0x005CF0C0
    private FrameworkElement Card(DateTimeOffset now)
    {
      double num = (this.Animates ? (now - this.Ink.StartedAt).TotalSeconds : this.StillClock(now));
      ClickyInkLiveNote note = this.Ink.Note;
      double num2 = num;
      ClickyInkCardSubject thing = this.Thing;
      DateTimeOffset? lookedAt = this.LookedAt;
      double? num3;
      if (lookedAt != null)
      {
        DateTimeOffset valueOrDefault = lookedAt.GetValueOrDefault();
        num3 = new double?((valueOrDefault - this.Ink.StartedAt).TotalSeconds);
      }
      else
      {
        num3 = null;
      }
      ClickyEdgeStackInkReading clickyEdgeStackInkReading = ClickyEdgeStackInkReadings.Make(note, num2, thing, num3, this.Actions.Feedback);
      bool flag = this.Thing.Kind != ClickyInkCardSubject.SubjectKind.Kept;
      if (this.Presentation is ClickyEdgeStackInkPresentation.Pulled)
      {
        ClickyEdgeStackInkPulledCard clickyEdgeStackInkPulledCard = new ClickyEdgeStackInkPulledCard(this.Ink.Note, clickyEdgeStackInkReading, num, this.Paper, flag, this.LiveStep, this.Actions, this.onOpen, this.onPutBack, this.onUnpin, Math.Max(200.0, this.PulledRoomHeight - 236.0));
        clickyEdgeStackInkPulledCard.VerticalAlignment = VerticalAlignment.Top;
        clickyEdgeStackInkPulledCard.SizeChanged += delegate(object _, SizeChangedEventArgs e)
        {
          double? pulledHeight = this.PulledHeight;
          double height = e.NewSize.Height;
          if ((pulledHeight.GetValueOrDefault() == height) & (pulledHeight != null))
          {
            return;
          }
          this.PulledHeight = new double?(e.NewSize.Height);
          Action<double> pulledHeightChanged = this.PulledHeightChanged;
          if (pulledHeightChanged == null)
          {
            return;
          }
          pulledHeightChanged(e.NewSize.Height);
        };
        return clickyEdgeStackInkPulledCard;
      }
      return new ClickyEdgeStackInkFace(this.Ink.Note, clickyEdgeStackInkReading, num, this.Paper, flag, this.LiveStep, this.Presentation is ClickyEdgeStackInkPresentation.Rest, this.FillsScreen);
    }

    // Token: 0x06001930 RID: 6448 RVA: 0x005CF224 File Offset: 0x005CF224
    private void WriteUntilThePenIsDone()
    {
      double num = this.Ink.Note.FinishedClock - (DateTimeOffset.Now - this.Ink.StartedAt).TotalSeconds;
      if (num <= 0.0)
      {
        if (this.isWriting)
        {
          this.isWriting = false;
          this.timeline.MinimumInterval = null;
        }
        return;
      }
      this.isWriting = true;
      this.timeline.MinimumInterval = ClickyInkTimeline.Interval(this.Animates ? new double?(0.03333333333333333) : null);
      DispatcherTimerHandle dispatcherTimerHandle = this.penDone;
      if (dispatcherTimerHandle != null)
      {
        dispatcherTimerHandle.Cancel();
      }
      this.penDone = DispatcherTimerHandle.After(base.DispatcherQueue, num, delegate
      {
        this.isWriting = false;
        this.timeline.MinimumInterval = null;
        this.timeline.Refresh();
      });
    }

    // Token: 0x06001931 RID: 6449 RVA: 0x005CF2F8 File Offset: 0x005CF2F8
    public static void OpenInItsApp(string target)
    {
      string text;
      if (target.Contains("://"))
      {
        Uri uri;
        if (!Uri.TryCreate(target, UriKind.Absolute, out uri))
        {
          return;
        }
        text = uri.AbsoluteUri;
      }
      else
      {
        text = ClickyInkPaths.ExpandingTilde(target);
      }
      try
      {
        ShellOpen.Start(text);
      }
      catch (Exception ex)
      {
        Log.Warn("ink", "couldn't open " + Path.GetFileName(text) + ": " + ex.Message);
      }
    }

    // Token: 0x04000C44 RID: 3140
    private readonly Action onOpen;

    // Token: 0x04000C45 RID: 3141
    private readonly Action onPutBack;

    // Token: 0x04000C46 RID: 3142
    private readonly Action onUnpin;

    // Token: 0x04000C47 RID: 3143
    private readonly ClickyInkTimeline timeline;

    // Token: 0x04000C48 RID: 3144
    [Nullable(2)]
    private DispatcherTimerHandle penDone;

    // Token: 0x04000C49 RID: 3145
    private bool isWriting;
  }
}
