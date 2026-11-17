using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using HeyClicky.Core.Notch;
using Microsoft.UI.Composition;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002F1 RID: 753
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class ChatWorkCard : Grid
  {
    // Token: 0x06002053 RID: 8275 RVA: 0x006162FC File Offset: 0x006162FC
    private ChatWorkCardMode Mode(ArcCardContent content)
    {
      bool isWorking = content.IsWorking;
      bool isFinalizing = content.IsFinalizing;
      CardFooterRequest request = content.Request;
      return ChatWorkCardRules.Mode(isWorking, isFinalizing, request != null && request.Kind != CardFooterRequest.Kinds.Ready, this.hidesFinalizing);
    }

    // Token: 0x06002054 RID: 8276 RVA: 0x0061633C File Offset: 0x0061633C
    private bool Shows(ArcCardContent content)
    {
      return this.Mode(content) > ChatWorkCardMode.Hidden;
    }

    // Token: 0x06002055 RID: 8277 RVA: 0x00616348 File Offset: 0x00616348
    public ChatWorkCard(ArcDock dock, string key, ArcCardContent content, bool hidesFinalizing = false)
    {
      ChatWorkCard <>4__this = this;
      this.dock = dock;
      this.key = key;
      this.hidesFinalizing = hidesFinalizing;
      base.MaxWidth = 420.0;
      base.HorizontalAlignment = HorizontalAlignment.Left;
      base.RequestedTheme = ElementTheme.Light;
      if (this.Shows(content))
      {
        this.Build(content);
      }
      else
      {
        base.Visibility = Visibility.Collapsed;
      }
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        if (!<>4__this.listening)
        {
          <>4__this.listening = true;
          dock.ContentChanged += <>4__this.OnContentChanged;
        }
        <>4__this.OnContentChanged();
        if (<>4__this.Visibility == Visibility.Visible)
        {
          <>4__this.Appear();
        }
      };
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        if (<>4__this.IsLoaded || !<>4__this.listening)
        {
          return;
        }
        <>4__this.listening = false;
        dock.ContentChanged -= <>4__this.OnContentChanged;
      };
    }

    // Token: 0x06002056 RID: 8278 RVA: 0x006163FC File Offset: 0x006163FC
    private void Appear()
    {
      if (this.appeared || !base.IsLoaded)
      {
        return;
      }
      this.appeared = true;
      if (Anim.ReduceMotion || Anim.Instant)
      {
        return;
      }
      base.Opacity = 0.0;
      Anim.Opacity(this, 1f, 0.25, null, 0.0);
      base.CenterPoint = Vector3.Zero;
      Compositor compositor = Ui.Compositor;
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = compositor.CreateVector3KeyFrameAnimation();
      vector3KeyFrameAnimation.InsertKeyFrame(0f, new Vector3(0.97f, 0.97f, 1f));
      vector3KeyFrameAnimation.InsertKeyFrame(1f, Vector3.One, Easing.EaseOut(compositor));
      vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(0.25);
      vector3KeyFrameAnimation.Target = "Scale";
      base.StartAnimation(vector3KeyFrameAnimation);
    }

    // Token: 0x06002057 RID: 8279 RVA: 0x006164D0 File Offset: 0x006164D0
    private string Look(ArcCardContent c)
    {
      string[] array = new string[5];
      array[0] = this.Mode(c).ToString();
      array[1] = "|";
      array[2] = c.Shape;
      array[3] = "|";
      int num = 4;
      ClickyArcPlan plan = c.Plan;
      string text2;
      if (plan == null || plan.IsEmpty)
      {
        string[] array2 = new string[7];
        int num2 = 0;
        ClickyLiveLine liveLine = c.LiveLine;
        array2[num2] = ((liveLine != null) ? liveLine.ToString() : null);
        array2[1] = "|";
        int num3 = 2;
        string text = ",";
        ClickyLiveLine liveLine2 = c.LiveLine;
        array2[num3] = string.Join(text, (((liveLine2 != null) ? liveLine2.Icons : null) ?? Array.Empty<ClickyLiveLineIcon>()).Select<ClickyLiveLineIcon, string>((ClickyLiveLineIcon i) => i.Host ?? i.Toolkit));
        array2[3] = "+";
        int num4 = 4;
        ClickyLiveLine liveLine3 = c.LiveLine;
        array2[num4] = ((liveLine3 != null) ? new int?(liveLine3.MoreIcons) : null).ToString();
        array2[5] = "|";
        array2[6] = c.CurrentStepLine;
        text2 = string.Concat(array2);
      }
      else
      {
        text2 = "";
      }
      array[num] = text2;
      return string.Concat(array);
    }

    // Token: 0x06002058 RID: 8280 RVA: 0x006165F8 File Offset: 0x006165F8
    private void OnContentChanged()
    {
      ArcCardContent arcCardContent = this.dock.ContentForKey(this.key);
      if (arcCardContent == null || !this.Shows(arcCardContent))
      {
        base.Visibility = Visibility.Collapsed;
        return;
      }
      if (base.Visibility != Visibility.Visible)
      {
        base.Visibility = Visibility.Visible;
        this.Appear();
      }
      if (this.Look(arcCardContent) == this.look)
      {
        return;
      }
      if (this.planRows != null && arcCardContent.Shape == this.shape)
      {
        ClickyArcPlan plan = arcCardContent.Plan;
        if (plan != null && !plan.IsEmpty)
        {
          this.planRows.Update(plan, arcCardContent.LiveLine);
          ArcProgressBar arcProgressBar = this.bar;
          if (arcProgressBar != null)
          {
            arcProgressBar.Update(plan);
          }
          this.look = this.Look(arcCardContent);
          return;
        }
      }
      this.Build(arcCardContent);
    }

    // Token: 0x06002059 RID: 8281 RVA: 0x006166BC File Offset: 0x006166BC
    private void Build(ArcCardContent content)
    {
      this.look = this.Look(content);
      this.shape = content.Shape;
      this.planRows = null;
      this.bar = null;
      base.Children.Clear();
      if (this.Mode(content) == ChatWorkCardMode.Ask)
      {
        CardFooterRequest request = content.Request;
        if (request != null)
        {
          base.Children.Add(new FooterRequestRow(this.dock, content, request, new double?((double)30), "chat_live_card", true)
          {
            Margin = new Thickness(0.0, 2.0, 0.0, 0.0)
          });
          return;
        }
      }
      LinearGradientBrush linearGradientBrush = Ui.Vertical(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, DS.Hex(15526893U, 1.0)),
        new ValueTuple<double, Color>(1.0, DS.Hex(16250871U, 1.0))
      });
      base.Children.Add(ChatBlockViews.BubblePlate(linearGradientBrush, null, true, false, 17.0, 7.0));
      base.Children.Add(ChatBlockViews.BubblePlate(Ui.Brush(content.Paper.TitleWash.WithAlpha(0.45)), null, true, false, 17.0, 7.0));
      Color ink = content.Paper.Ink;
      if (content.IsWorking)
      {
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 9.0,
          Padding = new Thickness(14.0, 12.0, 14.0, 19.0)
        };
        ClickyArcPlan plan = content.Plan;
        if (plan != null && !plan.IsEmpty)
        {
          this.planRows = new PlanRows(plan, content.LiveLine, ink, true);
          stackPanel.Children.Add(this.planRows);
          this.bar = new ArcProgressBar(plan, ink, false, true, true)
          {
            Margin = new Thickness(0.0, 2.0, 0.0, 0.0)
          };
          stackPanel.Children.Add(this.bar);
        }
        else
        {
          StackPanel stackPanel2 = new StackPanel
          {
            Orientation = Orientation.Horizontal,
            Spacing = 8.0
          };
          stackPanel2.Children.Add(new ProgressRing
          {
            IsActive = true,
            Width = 16.0,
            Height = 16.0,
            MinWidth = 16.0,
            MinHeight = 16.0,
            Foreground = Ui.Brush(ArcColors.Working),
            VerticalAlignment = VerticalAlignment.Center
          });
          ClickyLiveLine liveLine = content.LiveLine;
          if (liveLine != null)
          {
            stackPanel2.Children.Add(new LiveLineView(liveLine, ink, 12.5, false)
            {
              VerticalAlignment = VerticalAlignment.Center
            });
          }
          else
          {
            TextBlock textBlock = Ui.Text(content.CurrentStepLine ?? "Getting started…", 12.5, FontWeights.Medium, ink.WithAlpha(0.7), TextAlignment.Left, 2, false, false);
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            stackPanel2.Children.Add(textBlock);
          }
          stackPanel.Children.Add(stackPanel2);
          stackPanel.Children.Add(new ArcProgressBar(null, ink, false, false, true)
          {
            Margin = new Thickness(0.0, 2.0, 0.0, 0.0)
          });
        }
        base.Children.Add(stackPanel);
      }
    }

    // Token: 0x04000FA5 RID: 4005
    private readonly ArcDock dock;

    // Token: 0x04000FA6 RID: 4006
    private readonly string key;

    // Token: 0x04000FA7 RID: 4007
    private readonly bool hidesFinalizing;

    // Token: 0x04000FA8 RID: 4008
    private string look = "";

    // Token: 0x04000FA9 RID: 4009
    private string shape = "";

    // Token: 0x04000FAA RID: 4010
    [Nullable(2)]
    private PlanRows planRows;

    // Token: 0x04000FAB RID: 4011
    [Nullable(2)]
    private ArcProgressBar bar;

    // Token: 0x04000FAC RID: 4012
    private bool appeared;

    // Token: 0x04000FAD RID: 4013
    private bool listening;
  }
}
