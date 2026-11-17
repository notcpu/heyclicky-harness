using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.App.Design;
using HeyClicky.App.Dock;
using HeyClicky.App.Proactive;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Onboarding;
using HeyClicky.Core.Proactive;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Telemetry;
using HeyClicky.Drawing;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Foundation;
using Windows.Graphics;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI;

namespace HeyClicky.App.OneBrain
{
  // Token: 0x02000114 RID: 276
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyTourJobCards
  {
    // Token: 0x1700029F RID: 671
    // (get) Token: 0x06000E77 RID: 3703 RVA: 0x00573C94 File Offset: 0x00573C94
    public static ClickyTourJobCards Shared { get; } = new ClickyTourJobCards();

    // Token: 0x170002A0 RID: 672
    // (get) Token: 0x06000E78 RID: 3704 RVA: 0x00573C9C File Offset: 0x00573C9C
    // (set) Token: 0x06000E79 RID: 3705 RVA: 0x00573CA4 File Offset: 0x00573CA4
    public ProactiveCheckReason SuggestionCheckReason { get; set; } = ProactiveCheckReason.Onboarding;

    // Token: 0x170002A1 RID: 673
    // (get) Token: 0x06000E7A RID: 3706 RVA: 0x00573CB0 File Offset: 0x00573CB0
    public int PlacedCardCount
    {
      get
      {
        return this.placedCount;
      }
    }

    // Token: 0x170002A2 RID: 674
    // (get) Token: 0x06000E7B RID: 3707 RVA: 0x00573CB8 File Offset: 0x00573CB8
    public int HeldNextQuestionCount
    {
      get
      {
        if (!this.hasHeldQuestions)
        {
          return 0;
        }
        return this.nextQuestions.Count;
      }
    }

    // Token: 0x06000E7C RID: 3708 RVA: 0x00573CD0 File Offset: 0x00573CD0
    private ClickyTourJobCards()
    {
    }

    // Token: 0x170002A3 RID: 675
    // (get) Token: 0x06000E7D RID: 3709 RVA: 0x00573D58 File Offset: 0x00573D58
    private bool IsMorningContext
    {
      get
      {
        return this.SuggestionCheckReason == ProactiveCheckReason.Morning;
      }
    }

    // Token: 0x06000E7E RID: 3710 RVA: 0x00573D64 File Offset: 0x00573D64
    public static void TrackFlowAction(string action, bool isMorning, [Nullable(2)] JsonObject properties = null)
    {
      JsonObject jsonObject = properties ?? new JsonObject(null);
      jsonObject["action"] = action;
      ClickyAnalytics.TrackProductEvent(isMorning ? "morning_brief_action" : "onboarding_finale_action", null, null, jsonObject);
    }

    // Token: 0x06000E7F RID: 3711 RVA: 0x00573DB0 File Offset: 0x00573DB0
    private void TrackCardAction(string action, [Nullable(2)] JsonObject properties = null)
    {
      ClickyTourJobCards.TrackFlowAction(action, this.IsMorningContext, properties);
    }

    // Token: 0x06000E80 RID: 3712 RVA: 0x00573DC0 File Offset: 0x00573DC0
    private JsonObject CardProperties(ClickyTourJob job)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["is_planned_card"] = job.PlannedCard != null;
      JsonObject jsonObject2 = jsonObject;
      int num;
      if (this.placedIndexByKey.TryGetValue(job.RailKey, out num))
      {
        jsonObject2["index"] = num;
      }
      return jsonObject2;
    }

    // Token: 0x06000E81 RID: 3713 RVA: 0x00573E20 File Offset: 0x00573E20
    private void NoteCardClickedDuringTour(ClickyTourJob job)
    {
      if (!this.cardKeysClickedDuringTour.Add(job.RailKey))
      {
        return;
      }
      this.TrackCardAction("card_clicked_during_tour", this.CardProperties(job));
    }

    // Token: 0x170002A4 RID: 676
    // (get) Token: 0x06000E82 RID: 3714 RVA: 0x00573E48 File Offset: 0x00573E48
    [TupleElementNames(new string[] { "Shown", "Tucked", "HeldQuestions", "QuestionButtons" })]
    [Nullable(0)]
    internal ValueTuple<int, int, bool, int> StateForTests
    {
      [NullableContext(0)]
      [return: TupleElementNames(new string[] { "Shown", "Tucked", "HeldQuestions", "QuestionButtons" })]
      get
      {
        return new ValueTuple<int, int, bool, int>(this.shown.Count, this.tuckedJobs.Count, this.hasHeldQuestions, this.questionButtons.Count);
      }
    }

    // Token: 0x170002A5 RID: 677
    // (get) Token: 0x06000E83 RID: 3715 RVA: 0x00573E78 File Offset: 0x00573E78
    internal IReadOnlyList<UIElement> CardRootsForTests
    {
      get
      {
        return this.shown.Select<ClickyTourJobCards.Shown, UIElement>((ClickyTourJobCards.Shown s) => s.Window.Root).ToList<UIElement>();
      }
    }

    // Token: 0x06000E84 RID: 3716 RVA: 0x00573EAC File Offset: 0x00573EAC
    public void Attach(AppHost app)
    {
      this.host = app;
    }

    // Token: 0x170002A6 RID: 678
    // (get) Token: 0x06000E85 RID: 3717 RVA: 0x00573EB8 File Offset: 0x00573EB8
    [Nullable(2)]
    private ScreenDrawingCanvas Canvas
    {
      [NullableContext(2)]
      get
      {
        AppHost appHost = this.host;
        if (appHost == null)
        {
          return null;
        }
        RealStack real = appHost.Real;
        if (real == null)
        {
          return null;
        }
        return real.Canvas;
      }
    }

    // Token: 0x170002A7 RID: 679
    // (get) Token: 0x06000E86 RID: 3718 RVA: 0x00573ED8 File Offset: 0x00573ED8
    private DispatcherQueue Dq
    {
      get
      {
        return this.host.DispatcherQueue;
      }
    }

    // Token: 0x170002A8 RID: 680
    // (get) Token: 0x06000E87 RID: 3719 RVA: 0x00573EE8 File Offset: 0x00573EE8
    private bool PlaysSounds
    {
      get
      {
        AppHost appHost = this.host;
        bool? flag;
        if (appHost == null)
        {
          flag = null;
        }
        else
        {
          RealStack real = appHost.Real;
          if (real == null)
          {
            flag = null;
          }
          else
          {
            flag = new bool?(real.Core.Settings.Read<bool>((AppSettings s) => s.DrawingSoundsEnabled));
          }
        }
        bool? flag2 = flag;
        return flag2.GetValueOrDefault();
      }
    }

    // Token: 0x06000E88 RID: 3720 RVA: 0x00573F5C File Offset: 0x00573F5C
    public void Start(ClickyTourJob job)
    {
      int num = this.shown.FindIndex((ClickyTourJobCards.Shown s) => s.Job.RailKey == job.RailKey);
      if (num < 0)
      {
        return;
      }
      ClickyTourJobCards.Shown shown = this.shown[num];
      this.shown.RemoveAt(num);
      this.startedJobKeys.Add(job.RailKey);
      this.TrackCardAction("card_started", this.CardProperties(job));
      this.FlyAsync(shown);
      ProactiveSuggestionCard proactiveSuggestionCard = job.SuggestionCard(this.SuggestionCheckReason);
      AppHost appHost = this.host;
      ProactiveSuggestionCard proactiveSuggestionCard2;
      if (appHost == null)
      {
        proactiveSuggestionCard2 = null;
      }
      else
      {
        ProactiveCoordinator proactive = appHost.Proactive;
        proactiveSuggestionCard2 = ((proactive != null) ? proactive.AppendTourCard(proactiveSuggestionCard) : null);
      }
      ProactiveSuggestionCard proactiveSuggestionCard3 = proactiveSuggestionCard2;
      if (proactiveSuggestionCard3 == null)
      {
        return;
      }
      this.host.Proactive.Approve(proactiveSuggestionCard3.Id, null, false, "button");
      Log.Info("drawn tour", "started from its card: " + job.Title);
    }

    // Token: 0x06000E89 RID: 3721 RVA: 0x00574058 File Offset: 0x00574058
    public void Show(ClickyTourJob job, [Nullable(2)] DisplayInfo display)
    {
      if (this.host == null || this.placedCount >= 3)
      {
        return;
      }
      Displays.Monitor monitor = ((display == null) ? Displays.UnderCursor() : Displays.At((int)(display.BoundsPx.X + display.BoundsPx.Width / 2.0), (int)(display.BoundsPx.Y + display.BoundsPx.Height / 2.0)));
      int num = this.placedCount;
      this.placedCount = num + 1;
      int num2 = num;
      this.placedIndexByKey[job.RailKey] = num2;
      this.TrackCardAction("card_shown", this.CardProperties(job));
      double scale = monitor.Scale;
      double num3 = (double)monitor.Work.Right / scale;
      double num4 = (double)monitor.Work.Top / scale;
      double num5 = num3 - 335.92 - 56.0 - 22.0;
      double num6 = num4 + 70.0 - 22.0 + (double)num2 * 229.52;
      OverlayWindow overlayWindow = new OverlayWindow("HeyClicky tour card", true, false, false, true, false, false);
      FrameworkElement frameworkElement = ClickyTourJobCards.CardView(job, false);
      Grid grid = new Grid
      {
        Width = 379.92,
        Height = 257.52
      };
      Grid scaled = new Grid
      {
        Width = 494.0,
        Height = 314.0,
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Top,
        RenderTransform = new ScaleTransform
        {
          ScaleX = 0.68,
          ScaleY = 0.68
        },
        Margin = new Thickness(22.0)
      };
      scaled.Children.Add(frameworkElement);
      grid.Children.Add(scaled);
      Pressable catcher = new Pressable(!DrawnOnboardingTour.IsRunning)
      {
        Background = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0)),
        Margin = new Thickness(22.0)
      };
      catcher.HoverChanged += delegate(bool hovering)
      {
        if (this.startingKeys.Contains(job.RailKey))
        {
          return;
        }
        ArcCardView arcCardView = scaled.Children.FirstOrDefault<UIElement>() as ArcCardView;
        if (arcCardView != null)
        {
          arcCardView.SetPresentation(!hovering, false, true);
          return;
        }
        scaled.Children.Clear();
        scaled.Children.Add(ClickyTourJobCards.CardView(job, hovering));
      };
      catcher.Click += delegate
      {
        this.OnCardClicked(job, scaled, catcher);
      };
      AutomationProperties.SetName(catcher, job.Title);
      grid.Children.Add(catcher);
      overlayWindow.Root.Children.Add(grid);
      overlayWindow.PlacePx((int)Math.Round(num5 * scale), (int)Math.Round(num6 * scale), (int)Math.Round(379.92 * scale), (int)Math.Round(257.52 * scale));
      grid.Opacity = 0.0;
      overlayWindow.ShowNoActivate();
      this.shown.Add(new ClickyTourJobCards.Shown(job, overlayWindow, frameworkElement));
      if (!Anim.Instant && !Anim.ReduceMotion)
      {
        grid.CenterPoint = new Vector3(189.96f, 128.76f, 0f);
        grid.Translation = new Vector3(0f, -14f, 0f);
        SwiftSpring swiftSpring = new SwiftSpring(0.468, 0.522);
        Anim.ScaleUpFrom(grid, 0.92f, swiftSpring, null);
        Anim.TranslationSpring(grid, Vector3.Zero, swiftSpring);
        Anim.Opacity(grid, 1f, 0.22, Easing.EaseInOut(Ui.Compositor), 0.0);
      }
      else
      {
        grid.Opacity = 1.0;
      }
      if (this.PlaysSounds)
      {
        ScreenDrawingCanvas canvas = this.Canvas;
        if (canvas == null)
        {
          return;
        }
        canvas.PlaySound("paper-place", 0.35f);
      }
    }

    // Token: 0x06000E8A RID: 3722 RVA: 0x00574480 File Offset: 0x00574480
    private void OnCardClicked(ClickyTourJob job, Grid scaled, Pressable catcher)
    {
      if (DrawnOnboardingTour.IsRunning)
      {
        this.NoteCardClickedDuringTour(job);
        DrawnOnboardingTour.NudgeTourCardClicked(this.host);
        return;
      }
      if (!this.startingKeys.Add(job.RailKey))
      {
        return;
      }
      catcher.Enabled = false;
      scaled.Children.Clear();
      scaled.Children.Add(ClickyTourJobCards.CardView(job, false));
      Border border = new Border
      {
        Background = new SolidColorBrush(Color.FromArgb(198, 0, 0, 0)),
        CornerRadius = new CornerRadius(100.0),
        Padding = new Thickness(14.0, 9.0, 14.0, 9.0),
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        Child = Ui.H(8.0, new UIElement[]
        {
          new ProgressRing
          {
            IsActive = true,
            Width = 14.0,
            Height = 14.0,
            Foreground = new SolidColorBrush(Colors.White)
          },
          Ui.Text("Starting in the background", 12.5, FontWeights.SemiBold, Colors.White, TextAlignment.Left, 0, false, false)
        })
      }.Capsule();
      Grid grid = (Grid)scaled.Parent;
      border.Margin = new Thickness(22.0);
      grid.Children.Add(border);
      if (!Anim.Instant)
      {
        grid.CenterPoint = new Vector3(189.96f, 128.76f, 0f);
        Anim.ScaleSpring(grid, 0.97f, new SwiftSpring(0.3, 0.7), null);
      }
      Sounds.Play("clicky-text-send", 0.35);
      this.After(1.1, delegate
      {
        this.startingKeys.Remove(job.RailKey);
        this.Start(job);
      });
    }

    // Token: 0x06000E8B RID: 3723 RVA: 0x0057469C File Offset: 0x0057469C
    private static FrameworkElement CardView(ClickyTourJob job, bool hovered)
    {
      TourJobCardContent tourJobCardContent = new TourJobCardContent(job.RailKey, job.Title, (job.Pitch.Length == 0) ? null : job.Pitch, job.Paper, DrawnOnboardingTour.IsRunning);
      Func<TourJobCardContent, bool, FrameworkElement> arcCardFactory = ClickyTourJobCards.ArcCardFactory;
      if (arcCardFactory != null)
      {
        return arcCardFactory(tourJobCardContent, !hovered);
      }
      return ClickyTourJobCards.StandInCard(tourJobCardContent);
    }

    // Token: 0x06000E8C RID: 3724 RVA: 0x005746F8 File Offset: 0x005746F8
    private static FrameworkElement StandInCard(TourJobCardContent content)
    {
      ClickyTourJobCards.<>c__DisplayClass54_0 CS$<>8__locals1;
      CS$<>8__locals1.content = content;
      double num = 270.0 - (CS$<>8__locals1.content.IsTourRunning ? 50.0 : 0.0);
      StackPanel stackPanel = null;
      ValueTuple<double, double, double>[] suggested = ArcCover.Suggested;
      for (int i = 0; i < suggested.Length; i++)
      {
        StackPanel stackPanel2 = ClickyTourJobCards.<StandInCard>g__Block|54_0(suggested[i], ref CS$<>8__locals1);
        stackPanel2.Measure(new Size(428.0, double.PositiveInfinity));
        stackPanel = stackPanel2;
        if (stackPanel2.DesiredSize.Height <= num)
        {
          break;
        }
      }
      stackPanel.Margin = new Thickness(26.0, 20.0, 26.0, 10.0);
      Grid grid = new Grid();
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star)
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      grid.Children.Add(stackPanel);
      if (CS$<>8__locals1.content.IsTourRunning)
      {
        Color color = DS.Hex(3112182U, 1.0);
        StackPanel stackPanel3 = Ui.H(7.0, new UIElement[]
        {
          Icons.Glyph("\ue823", 13.0, color),
          Ui.Shrink(Ui.Text("Start it from your notch after the tour", 14.0, FontWeights.SemiBold, color, TextAlignment.Left, 0, false, false), HorizontalAlignment.Left, 0.8)
        });
        Border border = new Border
        {
          MinHeight = 32.0,
          Padding = new Thickness(18.0, 9.0, 18.0, 9.0),
          Child = stackPanel3
        };
        Grid.SetRow(border, 1);
        grid.Children.Add(border);
      }
      Border border2 = new Border
      {
        Background = new SolidColorBrush(CS$<>8__locals1.content.Paper.Paper()),
        CornerRadius = new CornerRadius(10.0),
        Child = grid,
        Width = 480.0,
        Height = 300.0
      };
      return new Border
      {
        Background = new SolidColorBrush(Color.FromArgb(byte.MaxValue, 22, 22, 24)),
        CornerRadius = new CornerRadius(17.0),
        Padding = new Thickness(7.0),
        Child = border2,
        Width = 494.0,
        Height = 314.0
      };
    }

    // Token: 0x06000E8D RID: 3725 RVA: 0x005749C4 File Offset: 0x005749C4
    public async Task TuckIntoNotchNowAsync()
    {
      List<ClickyTourJob> list = await this.PlugCardsIntoNotchAsync();
      if (list.Count > 0)
      {
        string text = "cards_tucked";
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["count"] = list.Count;
        jsonObject["trigger"] = "tuck_jobs";
        this.TrackCardAction(text, jsonObject);
      }
      this.tuckedJobs.AddRange(list);
      this.Park(list.Where<ClickyTourJob>((ClickyTourJob j) => !this.startedJobKeys.Contains(j.RailKey)).ToList<ClickyTourJob>());
    }

    // Token: 0x06000E8E RID: 3726 RVA: 0x00574A08 File Offset: 0x00574A08
    private async Task<List<ClickyTourJob>> PlugIntoNotchAsync()
    {
      List<ClickyTourJob> list = await this.PlugCardsIntoNotchAsync();
      if (list.Count > 0)
      {
        string text = "cards_tucked";
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["count"] = list.Count;
        jsonObject["trigger"] = "tour_end";
        this.TrackCardAction(text, jsonObject);
      }
      IEnumerable<ClickyTourJob> enumerable = this.tuckedJobs.Concat<ClickyTourJob>(list).ToList<ClickyTourJob>();
      this.tuckedJobs = new List<ClickyTourJob>();
      this.placedCount = 0;
      List<ClickyTourJob> list2 = enumerable.Where<ClickyTourJob>((ClickyTourJob j) => !this.startedJobKeys.Contains(j.RailKey) && !this.parkedJobKeys.Contains(j.RailKey)).ToList<ClickyTourJob>();
      this.startedJobKeys.Clear();
      this.parkedJobKeys.Clear();
      return list2;
    }

    // Token: 0x06000E8F RID: 3727 RVA: 0x00574A4C File Offset: 0x00574A4C
    public async Task TuckRemainingCardsIntoSuggestionsAsync()
    {
      this.waitingForQuestions = false;
      List<ClickyTourJob> list = await this.PlugIntoNotchAsync();
      this.Park(list);
      this.parkedJobKeys.Clear();
      if (this.parkedCountThisTour > 0)
      {
        string text = "cards_parked";
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["count"] = this.parkedCountThisTour;
        this.TrackCardAction(text, jsonObject);
      }
      this.parkedCountThisTour = 0;
    }

    // Token: 0x06000E90 RID: 3728 RVA: 0x00574A90 File Offset: 0x00574A90
    private void Park(List<ClickyTourJob> jobs)
    {
      foreach (ClickyTourJob clickyTourJob in jobs)
      {
        if (this.parkedJobKeys.Add(clickyTourJob.RailKey))
        {
          ProactiveSuggestionCard proactiveSuggestionCard = clickyTourJob.SuggestionCard(this.SuggestionCheckReason);
          AppHost appHost = this.host;
          ProactiveSuggestionCard proactiveSuggestionCard2;
          if (appHost == null)
          {
            proactiveSuggestionCard2 = null;
          }
          else
          {
            ProactiveCoordinator proactive = appHost.Proactive;
            proactiveSuggestionCard2 = ((proactive != null) ? proactive.AppendTourCard(proactiveSuggestionCard) : null);
          }
          ProactiveSuggestionCard proactiveSuggestionCard3 = proactiveSuggestionCard2;
          if (proactiveSuggestionCard3 != null)
          {
            this.parkedCountThisTour++;
            Action<string, string, Guid> createWaitingJob = ClickyTourJobCards.CreateWaitingJob;
            if (createWaitingJob != null)
            {
              createWaitingJob(clickyTourJob.Title, proactiveSuggestionCard3.Description, proactiveSuggestionCard3.Id);
            }
          }
        }
      }
    }

    // Token: 0x06000E91 RID: 3729 RVA: 0x00574B50 File Offset: 0x00574B50
    private async Task<List<ClickyTourJob>> PlugCardsIntoNotchAsync()
    {
      List<ClickyTourJobCards.Shown> cards = this.shown.ToList<ClickyTourJobCards.Shown>();
      this.shown.Clear();
      List<ClickyTourJob> list;
      if (cards.Count == 0)
      {
        list = new List<ClickyTourJob>();
      }
      else
      {
        if (this.PlaysSounds)
        {
          ScreenDrawingCanvas canvas = this.Canvas;
          if (canvas != null)
          {
            canvas.PlaySound("whoosh", 0.4f);
          }
        }
        for (int i = 0; i < cards.Count; i++)
        {
          if (i > 0)
          {
            await Task.Delay(170);
          }
          this.FlyAsync(cards[i]);
        }
        await Task.Delay(650);
        Sounds.Play("clicky-text-receive", 0.35);
        list = cards.Select<ClickyTourJobCards.Shown, ClickyTourJob>((ClickyTourJobCards.Shown c) => c.Job).ToList<ClickyTourJob>();
      }
      return list;
    }

    // Token: 0x06000E92 RID: 3730 RVA: 0x00574B94 File Offset: 0x00574B94
    private async Task FlyAsync(ClickyTourJobCards.Shown card)
    {
      ValueTuple<int, int, int, int> rectPx = card.Window.RectPx;
      int item = rectPx.Item1;
      int item2 = rectPx.Item2;
      double scale = card.Window.Scale;
      RectInt32 source = new RectInt32((int)Math.Round((double)item + 22.0 * scale), (int)Math.Round((double)item2 + 22.0 * scale), (int)Math.Round(335.92 * scale), (int)Math.Round(213.52 * scale));
      byte[] picture = null;
      try
      {
        byte[] array = await ClickyTourJobCards.RenderPngAsync(card.Laptop, source.Width, source.Height);
        picture = array;
      }
      catch (Exception ex)
      {
        Log.Info("drawn tour", "card picture: " + ex.Message);
      }
      card.Window.HideWindow();
      Displays.Monitor monitor = Displays.At(source.X + source.Width / 2, source.Y + source.Height / 2);
      DisplayInfo displayInfo = new DisplayInfo(0, "", new RectD((double)monitor.Bounds.Left, (double)monitor.Bounds.Top, (double)monitor.Bounds.Width, (double)monitor.Bounds.Height), new RectD((double)monitor.Work.Left, (double)monitor.Work.Top, (double)monitor.Work.Width, (double)monitor.Work.Height), monitor.Scale, false);
      if (picture != null)
      {
        ScreenDrawingCanvas canvas = this.Canvas;
        if (canvas != null)
        {
          canvas.FlyIntoNotch(picture, source, displayInfo, delegate
          {
          });
        }
      }
    }

    // Token: 0x06000E93 RID: 3731 RVA: 0x00574BE0 File Offset: 0x00574BE0
    [return: Nullable(new byte[] { 1, 2 })]
    private static async Task<byte[]> RenderPngAsync(FrameworkElement element, int widthPx, int heightPx)
    {
      RenderTargetBitmap bitmap = new RenderTargetBitmap();
      await bitmap.RenderAsync(element, widthPx, heightPx);
      IBuffer pixels = await bitmap.GetPixelsAsync();
      byte[] array;
      using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
      {
        object obj = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
        obj.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied, (uint)bitmap.PixelWidth, (uint)bitmap.PixelHeight, 96.0, 96.0, pixels.ToArray());
        await obj.FlushAsync();
        byte[] bytes = new byte[stream.Size];
        stream.Seek(0UL);
        await stream.ReadAsync(bytes.AsBuffer(), (uint)bytes.Length, InputStreamOptions.None);
        array = bytes;
      }
      return array;
    }

    // Token: 0x06000E94 RID: 3732 RVA: 0x00574C34 File Offset: 0x00574C34
    public void KeepCardsUntilQuestionsGo()
    {
      if (this.shown.Count == 0)
      {
        return;
      }
      this.waitingForQuestions = true;
      this.After(125.0, new Action(this.ReleaseWaitingCards));
    }

    // Token: 0x06000E95 RID: 3733 RVA: 0x00574C68 File Offset: 0x00574C68
    private void ReleaseWaitingCards()
    {
      if (!this.waitingForQuestions)
      {
        return;
      }
      this.waitingForQuestions = false;
      List<ClickyTourJobCards.Shown> list = this.shown.ToList<ClickyTourJobCards.Shown>();
      this.shown.Clear();
      this.placedCount = 0;
      int num = list.Count<ClickyTourJobCards.Shown>((ClickyTourJobCards.Shown c) => !this.startedJobKeys.Contains(c.Job.RailKey));
      if (num > 0)
      {
        string text = "cards_moved_to_suggestions";
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["count"] = num;
        this.TrackCardAction(text, jsonObject);
      }
      foreach (ClickyTourJobCards.Shown shown in list)
      {
        if (!this.startedJobKeys.Contains(shown.Job.RailKey))
        {
          AppHost appHost = this.host;
          if (appHost != null)
          {
            ProactiveCoordinator proactive = appHost.Proactive;
            if (proactive != null)
            {
              proactive.AppendTourCard(shown.Job.SuggestionCard(this.SuggestionCheckReason));
            }
          }
        }
        OverlayWindow window = shown.Window;
        UIElement uielement = window.Root.Children.FirstOrDefault<UIElement>();
        if (uielement != null && !Anim.Instant)
        {
          Anim.Opacity(uielement, 0f, 0.3, Easing.EaseInOut(Ui.Compositor), 0.0);
          this.After(0.3, new Action(window.HideWindow));
        }
        else
        {
          window.HideWindow();
        }
      }
      this.startedJobKeys.Clear();
      this.tuckedJobs = new List<ClickyTourJob>();
    }

    // Token: 0x06000E96 RID: 3734 RVA: 0x00574DF4 File Offset: 0x00574DF4
    public void NoteNextQuestions(string payload)
    {
      try
      {
        if (!(JsonNode.Parse(payload, null, default(JsonDocumentOptions)) is JsonArray))
        {
          return;
        }
      }
      catch (JsonException)
      {
        return;
      }
      this.nextQuestions = DrawnTourBriefs.ParseNextQuestions(payload).ToList<ValueTuple<string, string>>();
    }

    // Token: 0x06000E97 RID: 3735 RVA: 0x00574E48 File Offset: 0x00574E48
    [NullableContext(2)]
    public void HoldNextQuestions(DisplayInfo display)
    {
      if (this.nextQuestions.Count == 0)
      {
        return;
      }
      this.heldQuestionsDisplay = display;
      this.hasHeldQuestions = true;
    }

    // Token: 0x06000E98 RID: 3736 RVA: 0x00574E68 File Offset: 0x00574E68
    public void PopHeldNextQuestions(double afterDelay = 0.15)
    {
      if (!this.hasHeldQuestions)
      {
        return;
      }
      this.hasHeldQuestions = false;
      this.PopNextQuestions(this.heldQuestionsDisplay, afterDelay);
      this.heldQuestionsDisplay = null;
    }

    // Token: 0x06000E99 RID: 3737 RVA: 0x00574E90 File Offset: 0x00574E90
    [NullableContext(2)]
    public void PopNextQuestions(DisplayInfo display, double afterDelay = 0.15)
    {
      ClickyTourJobCards.<>c__DisplayClass67_0 CS$<>8__locals1 = new ClickyTourJobCards.<>c__DisplayClass67_0();
      CS$<>8__locals1.<>4__this = this;
      List<ValueTuple<string, string>> list = this.nextQuestions;
      this.nextQuestions = new List<ValueTuple<string, string>>();
      this.RetireQuestionsPanel();
      if (list.Count != 0)
      {
        CS$<>8__locals1.canvas = this.Canvas;
        if (CS$<>8__locals1.canvas != null && this.host != null)
        {
          Displays.Monitor monitor = ((display == null) ? Displays.UnderCursor() : Displays.At((int)(display.BoundsPx.X + display.BoundsPx.Width / 2.0), (int)(display.BoundsPx.Y + display.BoundsPx.Height / 2.0)));
          CS$<>8__locals1.plansByText = new Dictionary<string, string>();
          foreach (ValueTuple<string, string> valueTuple in list)
          {
            string item = valueTuple.Item1;
            string item2 = valueTuple.Item2;
            CS$<>8__locals1.plansByText.TryAdd(item, item2);
          }
          CS$<>8__locals1.isMorning = this.IsMorningContext;
          string text = "next_questions_shown";
          bool isMorning = CS$<>8__locals1.isMorning;
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["count"] = list.Count;
          ClickyTourJobCards.TrackFlowAction(text, isMorning, jsonObject);
          List<ValueTuple<string, string>> list2 = list.Select<ValueTuple<string, string>, ValueTuple<string, string>>(([TupleElementNames(new string[] { "Text", "Plan" })] ValueTuple<string, string> q) => new ValueTuple<string, string>(q.Item1, "ask")).Append(new ValueTuple<string, string>("Clear", "dismiss")).ToList<ValueTuple<string, string>>();
          List<ValueTuple<double, double>> list3 = list2.Select<ValueTuple<string, string>, ValueTuple<double, double>>(([TupleElementNames(new string[] { "Title", "Kind" })] ValueTuple<string, string> b) => CS$<>8__locals1.canvas.StandaloneButtonSize(b.Item1, b.Item2)).ToList<ValueTuple<double, double>>();
          CS$<>8__locals1.s = monitor.Scale;
          double num = (double)monitor.Bounds.Left / CS$<>8__locals1.s;
          double num2 = (double)monitor.Bounds.Top / CS$<>8__locals1.s;
          double num3 = (double)monitor.Bounds.Width / CS$<>8__locals1.s;
          double num4 = list3.Sum<ValueTuple<double, double>>(([TupleElementNames(new string[] { "Width", "Height" })] ValueTuple<double, double> z) => z.Item1) + 4.0 * (double)(list3.Count - 1);
          double num5 = ((double)monitor.Work.Top + (double)monitor.Work.Height / 2.0) / CS$<>8__locals1.s - num2;
          List<ValueTuple<double, double, double, double>> list4 = new List<ValueTuple<double, double, double, double>>();
          if (num4 <= num3 - 64.0)
          {
            double num6 = num3 / 2.0 - num4 / 2.0;
            using (List<ValueTuple<double, double>>.Enumerator enumerator2 = list3.GetEnumerator())
            {
              while (enumerator2.MoveNext())
              {
                ValueTuple<double, double> valueTuple2 = enumerator2.Current;
                list4.Add(new ValueTuple<double, double, double, double>(num6, num5 - valueTuple2.Item2 / 2.0, valueTuple2.Item1, valueTuple2.Item2));
                num6 += valueTuple2.Item1 + 4.0;
              }
              goto IL_0421;
            }
          }
          double num7 = list3.Sum<ValueTuple<double, double>>(([TupleElementNames(new string[] { "Width", "Height" })] ValueTuple<double, double> z) => z.Item2) + 4.0 * (double)(list3.Count - 1);
          double num8 = num5 - num7 / 2.0;
          double item3 = list3.FirstOrDefault<ValueTuple<double, double>>().Item2;
          double num9 = num8 + ((item3 > 0.0) ? item3 : 48.0) / 2.0;
          foreach (ValueTuple<double, double> valueTuple3 in list3)
          {
            list4.Add(new ValueTuple<double, double, double, double>(num3 / 2.0 - valueTuple3.Item1 / 2.0, num9 - valueTuple3.Item2 / 2.0, valueTuple3.Item1, valueTuple3.Item2));
            num9 += valueTuple3.Item2 + 4.0;
          }
          IL_0421:
          ClickyTourJobCards.<>c__DisplayClass67_0 CS$<>8__locals2 = CS$<>8__locals1;
          int num10 = this.questionsGeneration + 1;
          this.questionsGeneration = num10;
          CS$<>8__locals2.generation = num10;
          for (int i = 0; i < list2.Count; i++)
          {
            ValueTuple<string, string> valueTuple4 = list2[i];
            string title = valueTuple4.Item1;
            string kind = valueTuple4.Item2;
            ValueTuple<double, double, double, double> valueTuple5 = list4[i];
            RectInt32 rect = new RectInt32((int)Math.Round((num + valueTuple5.Item1) * CS$<>8__locals1.s), (int)Math.Round((num2 + valueTuple5.Item2) * CS$<>8__locals1.s), (int)Math.Round(valueTuple5.Item3 * CS$<>8__locals1.s), (int)Math.Round(valueTuple5.Item4 * CS$<>8__locals1.s));
            int index = i;
            DispatcherQueueHandler <>9__7;
            Action <>9__6;
            this.After(afterDelay + (double)index * 0.32, delegate
            {
              if (CS$<>8__locals1.<>4__this.questionsGeneration != CS$<>8__locals1.generation)
              {
                return;
              }
              List<IDisposable> list5 = CS$<>8__locals1.<>4__this.questionButtons;
              ScreenDrawingCanvas canvas = CS$<>8__locals1.canvas;
              string title2 = title;
              string kind2 = kind;
              RectInt32 rect2 = rect;
              double s = CS$<>8__locals1.s;
              Action action;
              if ((action = <>9__6) == null)
              {
                action = (<>9__6 = delegate
                {
                  DispatcherQueue dq = CS$<>8__locals1.<>4__this.Dq;
                  DispatcherQueueHandler dispatcherQueueHandler;
                  if ((dispatcherQueueHandler = <>9__7) == null)
                  {
                    dispatcherQueueHandler = (<>9__7 = delegate
                    {
                      CS$<>8__locals1.<>4__this.RetireNextQuestions();
                      if (kind != "ask")
                      {
                        ClickyTourJobCards.TrackFlowAction("next_questions_cleared", CS$<>8__locals1.isMorning, null);
                        return;
                      }
                      string text2 = "next_question_tapped";
                      bool isMorning2 = CS$<>8__locals1.isMorning;
                      JsonObject jsonObject2 = new JsonObject(null);
                      jsonObject2["index"] = index;
                      string text3;
                      jsonObject2["has_plan"] = CS$<>8__locals1.plansByText.TryGetValue(title, out text3) && text3.Length > 0;
                      ClickyTourJobCards.TrackFlowAction(text2, isMorning2, jsonObject2);
                      RealStack real = CS$<>8__locals1.<>4__this.host.Real;
                      if (real != null)
                      {
                        real.Cursor.HideMessage();
                      }
                      ClickyTourJobCards <>4__this = CS$<>8__locals1.<>4__this;
                      string text4;
                      string text5;
                      if (!CS$<>8__locals1.plansByText.TryGetValue(title, out text4) || text4.Length <= 0)
                      {
                        text5 = null;
                      }
                      else
                      {
                        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(171, 2);
                        defaultInterpolatedStringHandler.AppendLiteral("[Pill] They tapped your pill \"");
                        defaultInterpolatedStringHandler.AppendFormatted(title);
                        defaultInterpolatedStringHandler.AppendLiteral("\". When you wrote it, this is how you planned to answer it: ");
                        defaultInterpolatedStringHandler.AppendFormatted(text4);
                        defaultInterpolatedStringHandler.AppendLiteral(" Do that, unless their screen or what they've said since points somewhere better.");
                        text5 = defaultInterpolatedStringHandler.ToStringAndClear();
                      }
                      <>4__this.pendingPillPlanNote = text5;
                      CS$<>8__locals1.<>4__this.host.Services.Companion.AskFromDrawingAsync(title, "button");
                    });
                  }
                  dq.TryEnqueue(dispatcherQueueHandler);
                });
              }
              list5.Add(canvas.ShowStandaloneButton(title2, kind2, rect2, s, action));
              if (CS$<>8__locals1.<>4__this.PlaysSounds)
              {
                CS$<>8__locals1.canvas.PlaySound("badge-pop", 0.55f);
                if (index == 0)
                {
                  CS$<>8__locals1.canvas.PlaySound("sparkle", 0.35f);
                }
              }
            });
          }
          this.After(120.0, delegate
          {
            if (CS$<>8__locals1.<>4__this.questionsGeneration != CS$<>8__locals1.generation)
            {
              return;
            }
            ClickyTourJobCards.TrackFlowAction("next_questions_timed_out", CS$<>8__locals1.isMorning, null);
            CS$<>8__locals1.<>4__this.RetireNextQuestions();
          });
          return;
        }
      }
    }

    // Token: 0x06000E9A RID: 3738 RVA: 0x00575424 File Offset: 0x00575424
    [NullableContext(2)]
    public string TakePendingPillPlanNote()
    {
      return Interlocked.Exchange<string>(ref this.pendingPillPlanNote, null);
    }

    // Token: 0x06000E9B RID: 3739 RVA: 0x00575434 File Offset: 0x00575434
    private void RetireNextQuestions()
    {
      this.ReleaseWaitingCards();
      this.RetireQuestionsPanel();
    }

    // Token: 0x06000E9C RID: 3740 RVA: 0x00575444 File Offset: 0x00575444
    private void RetireQuestionsPanel()
    {
      this.questionsGeneration++;
      List<IDisposable> list = this.questionButtons.ToList<IDisposable>();
      this.questionButtons.Clear();
      foreach (IDisposable disposable in list)
      {
        disposable.Dispose();
      }
    }

    // Token: 0x06000E9D RID: 3741 RVA: 0x005754B4 File Offset: 0x005754B4
    public void DismissAll()
    {
      foreach (ClickyTourJobCards.Shown shown in this.shown)
      {
        shown.Window.HideWindow();
      }
      this.shown.Clear();
      this.tuckedJobs = new List<ClickyTourJob>();
      this.startedJobKeys.Clear();
      this.parkedJobKeys.Clear();
      this.nextQuestions = new List<ValueTuple<string, string>>();
      this.hasHeldQuestions = false;
      this.heldQuestionsDisplay = null;
      this.RetireNextQuestions();
      this.placedCount = 0;
      this.placedIndexByKey.Clear();
      this.cardKeysClickedDuringTour.Clear();
      this.parkedCountThisTour = 0;
    }

    // Token: 0x06000E9E RID: 3742 RVA: 0x00575578 File Offset: 0x00575578
    private void After(double seconds, Action action)
    {
      if (this.host == null)
      {
        return;
      }
      DispatcherQueueTimer t = this.Dq.CreateTimer().Rooted();
      t.Interval = TimeSpan.FromSeconds(Math.Max(0.001, seconds));
      t.IsRepeating = false;
      this.pending.Add(t);
      t.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        t.Stop();
        this.pending.Remove(t);
        action();
      };
      t.Start();
    }

    // Token: 0x06000EA0 RID: 3744 RVA: 0x00575624 File Offset: 0x00575624
    [NullableContext(0)]
    [CompilerGenerated]
    [return: Nullable(1)]
    internal static StackPanel <StandInCard>g__Block|54_0([TupleElementNames(new string[] { "Title", "Body", "Spacing" })] ValueTuple<double, double, double> z, ref ClickyTourJobCards.<>c__DisplayClass54_0 A_1)
    {
      TextBlock textBlock = Ui.Text(A_1.content.Title, z.Item1, FontWeights.Bold, A_1.content.Paper.Ink(), TextAlignment.Left, 0, true, false);
      textBlock.FontFamily = new FontFamily("Segoe UI Variable Display");
      StackPanel stackPanel = new StackPanel
      {
        Spacing = z.Item3,
        Children = { textBlock }
      };
      string summary = A_1.content.Summary;
      if (summary != null)
      {
        stackPanel.Children.Add(Ui.Text(ArcCover.CardPitch(summary, 200), z.Item2, FontWeights.Medium, A_1.content.Paper.Ink().WithAlpha(0.72), TextAlignment.Left, 5, true, false));
      }
      return stackPanel;
    }

    // Token: 0x040005D3 RID: 1491
    private const string Area = "drawn tour";

    // Token: 0x040005D5 RID: 1493
    [Nullable(new byte[] { 2, 1, 1 })]
    public static Func<TourJobCardContent, bool, FrameworkElement> ArcCardFactory;

    // Token: 0x040005D6 RID: 1494
    [Nullable(new byte[] { 2, 1, 1 })]
    public static Action<string, string, Guid> CreateWaitingJob;

    // Token: 0x040005D7 RID: 1495
    [Nullable(2)]
    private AppHost host;

    // Token: 0x040005D8 RID: 1496
    private readonly List<ClickyTourJobCards.Shown> shown = new List<ClickyTourJobCards.Shown>();

    // Token: 0x040005D9 RID: 1497
    private int placedCount;

    // Token: 0x040005DA RID: 1498
    private List<ClickyTourJob> tuckedJobs = new List<ClickyTourJob>();

    // Token: 0x040005DB RID: 1499
    private readonly HashSet<string> startedJobKeys = new HashSet<string>();

    // Token: 0x040005DC RID: 1500
    private readonly HashSet<string> parkedJobKeys = new HashSet<string>();

    // Token: 0x040005DD RID: 1501
    private bool waitingForQuestions;

    // Token: 0x040005DE RID: 1502
    [TupleElementNames(new string[] { "Text", "Plan" })]
    [Nullable(new byte[] { 1, 0, 1, 1 })]
    private List<ValueTuple<string, string>> nextQuestions = new List<ValueTuple<string, string>>();

    // Token: 0x040005DF RID: 1503
    [Nullable(2)]
    private volatile string pendingPillPlanNote;

    // Token: 0x040005E0 RID: 1504
    private readonly List<IDisposable> questionButtons = new List<IDisposable>();

    // Token: 0x040005E1 RID: 1505
    [Nullable(2)]
    private DisplayInfo heldQuestionsDisplay;

    // Token: 0x040005E2 RID: 1506
    private bool hasHeldQuestions;

    // Token: 0x040005E3 RID: 1507
    private int questionsGeneration;

    // Token: 0x040005E5 RID: 1509
    private readonly Dictionary<string, int> placedIndexByKey = new Dictionary<string, int>();

    // Token: 0x040005E6 RID: 1510
    private readonly HashSet<string> cardKeysClickedDuringTour = new HashSet<string>();

    // Token: 0x040005E7 RID: 1511
    private int parkedCountThisTour;

    // Token: 0x040005E8 RID: 1512
    private readonly HashSet<string> startingKeys = new HashSet<string>();

    // Token: 0x040005E9 RID: 1513
    private readonly HashSet<DispatcherQueueTimer> pending = new HashSet<DispatcherQueueTimer>();

    // Token: 0x020005ED RID: 1517
    [Nullable(0)]
    private sealed class Shown
    {
      // Token: 0x0600301B RID: 12315 RVA: 0x00666B54 File Offset: 0x00666B54
      public Shown(ClickyTourJob job, OverlayWindow window, FrameworkElement laptop)
      {
      }

      // Token: 0x1700075D RID: 1885
      // (get) Token: 0x0600301C RID: 12316 RVA: 0x00666B74 File Offset: 0x00666B74
      public ClickyTourJob Job { get; } = job;

      // Token: 0x1700075E RID: 1886
      // (get) Token: 0x0600301D RID: 12317 RVA: 0x00666B7C File Offset: 0x00666B7C
      public OverlayWindow Window { get; } = window;

      // Token: 0x1700075F RID: 1887
      // (get) Token: 0x0600301E RID: 12318 RVA: 0x00666B84 File Offset: 0x00666B84
      public FrameworkElement Laptop { get; } = laptop;
    }
  }
}
