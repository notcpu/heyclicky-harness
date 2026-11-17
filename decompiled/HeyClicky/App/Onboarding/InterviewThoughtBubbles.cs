using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using HeyClicky.Core.Onboarding;
using Microsoft.UI.Composition;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x02000121 RID: 289
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class InterviewThoughtBubbles : Grid
  {
    // Token: 0x06000FC3 RID: 4035 RVA: 0x0057DC44 File Offset: 0x0057DC44
    public InterviewThoughtBubbles(double maximumWidth, double maximumHeight)
    {
      this.textWidth = maximumWidth - 22.0;
      this.measurer.Text = "Ag";
      this.measurer.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      this.lineHeight = Math.Ceiling(this.measurer.DesiredSize.Height);
      this.stack.Children.Add(new Border
      {
        Height = 14.0
      });
      this.scroller = new ScrollViewer
      {
        Content = this.stack,
        MaxHeight = maximumHeight,
        Width = maximumWidth + 16.0,
        VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
        VerticalAlignment = VerticalAlignment.Bottom,
        IsHitTestVisible = false
      };
      base.Children.Add(this.scroller);
    }

    // Token: 0x06000FC4 RID: 4036 RVA: 0x0057DDC4 File Offset: 0x0057DDC4
    public unsafe void Show(string text, bool isLoading)
    {
      if (text == this.shownText && isLoading == this.shownLoading)
      {
        return;
      }
      this.shownText = text;
      this.shownLoading = isLoading;
      this.pagination.Update(text, (string candidate) => this.Measure(candidate).Height <= this.lineHeight * 6.0);
      IReadOnlyList<string> readOnlyList;
      if (this.pagination.Bubbles.Count <= 0)
      {
        if (!isLoading)
        {
          readOnlyList = new List<string>();
        }
        else
        {
          int num = 1;
          List<string> list = new List<string>(num);
          CollectionsMarshal.SetCount<string>(list, num);
          readOnlyList = list;
          Span<string> span = CollectionsMarshal.AsSpan<string>(list);
          int num2 = 0;
          *span[num2] = null;
        }
      }
      else
      {
        readOnlyList = this.pagination.Bubbles.Select<string, string>((string b) => b).ToList<string>();
      }
      IReadOnlyList<string> readOnlyList2 = readOnlyList;
      bool flag;
      if (this.rows.Count <= readOnlyList2.Count)
      {
        if (this.rows.Count > 0 && readOnlyList2.Count > 0)
        {
          string text2 = this.rows[0].Text;
          if (text2 != null)
          {
            string text3 = readOnlyList2[0];
            if (text3 != null)
            {
              flag = !text3.StartsWith(text2, StringComparison.Ordinal);
              goto IL_0115;
            }
          }
        }
        flag = false;
      }
      else
      {
        flag = true;
      }
      IL_0115:
      if (flag)
      {
        foreach (InterviewThoughtBubbles.Row row in this.rows)
        {
          this.stack.Children.Remove(row.View);
        }
        this.rows.Clear();
      }
      List<double> list2 = this.Bottoms();
      for (int i = 0; i < readOnlyList2.Count; i++)
      {
        bool flag2 = i == readOnlyList2.Count - 1;
        if (i < this.rows.Count)
        {
          this.rows[i].Set(readOnlyList2[i], flag2, this);
        }
        else
        {
          InterviewThoughtBubbles.Row row2 = new InterviewThoughtBubbles.Row();
          row2.Set(readOnlyList2[i], flag2, this);
          this.rows.Add(row2);
          this.stack.Children.Insert(this.stack.Children.Count - 1, row2.View);
          row2.Enter();
        }
      }
      List<double> list3 = this.Bottoms();
      int num3 = 0;
      while (num3 < list2.Count && num3 < this.rows.Count)
      {
        double num4 = list3[num3] - list2[num3];
        if (Math.Abs(num4) > 0.25)
        {
          this.rows[num3].Glide(num4);
        }
        num3++;
      }
      this.scroller.UpdateLayout();
      this.scroller.ChangeView(null, new double?(this.scroller.ScrollableHeight), null, true);
    }

    // Token: 0x06000FC5 RID: 4037 RVA: 0x0057E0A4 File Offset: 0x0057E0A4
    private List<double> Bottoms()
    {
      List<double> list = new List<double>(this.rows.Count);
      double num = 14.0;
      for (int i = this.rows.Count - 1; i >= 0; i--)
      {
        list.Insert(0, num);
        num += this.rows[i].Height;
      }
      return list;
    }

    // Token: 0x06000FC6 RID: 4038 RVA: 0x0057E104 File Offset: 0x0057E104
    [NullableContext(2)]
    private Size Measure(string text)
    {
      if (string.IsNullOrEmpty(text))
      {
        return new Size(32.0, this.lineHeight);
      }
      this.measurer.Text = text;
      this.measurer.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      double num = Math.Min(this.textWidth, Math.Ceiling(this.measurer.DesiredSize.Width) + 1.0);
      this.measurer.Measure(new Size(num, double.PositiveInfinity));
      return new Size(num, Math.Max(this.lineHeight, Math.Ceiling(this.measurer.DesiredSize.Height)));
    }

    // Token: 0x040006BC RID: 1724
    private const double FontSize = 13.0;

    // Token: 0x040006BD RID: 1725
    private const double HorizontalPadding = 11.0;

    // Token: 0x040006BE RID: 1726
    private const double VerticalPadding = 7.0;

    // Token: 0x040006BF RID: 1727
    private const double TailDrop = 6.0;

    // Token: 0x040006C0 RID: 1728
    private const double Corner = 12.0;

    // Token: 0x040006C1 RID: 1729
    private const double BottomRoom = 14.0;

    // Token: 0x040006C2 RID: 1730
    private readonly double textWidth;

    // Token: 0x040006C3 RID: 1731
    private readonly double lineHeight;

    // Token: 0x040006C4 RID: 1732
    private readonly InterviewBubblePagination pagination = new InterviewBubblePagination();

    // Token: 0x040006C5 RID: 1733
    private readonly StackPanel stack = new StackPanel
    {
      Padding = new Thickness(14.0, 2.0, 2.0, 0.0),
      VerticalAlignment = VerticalAlignment.Bottom
    };

    // Token: 0x040006C6 RID: 1734
    private readonly ScrollViewer scroller;

    // Token: 0x040006C7 RID: 1735
    private readonly List<InterviewThoughtBubbles.Row> rows = new List<InterviewThoughtBubbles.Row>();

    // Token: 0x040006C8 RID: 1736
    private readonly TextBlock measurer = Ui.Text("", 13.0, FontWeights.Medium, DS.Black(0.86), TextAlignment.Left, 0, true, false);

    // Token: 0x040006C9 RID: 1737
    private string shownText = "";

    // Token: 0x040006CA RID: 1738
    private bool shownLoading;

    // Token: 0x02000645 RID: 1605
    [Nullable(0)]
    private sealed class Row
    {
      // Token: 0x17000783 RID: 1923
      // (get) Token: 0x06003179 RID: 12665 RVA: 0x0066E0EC File Offset: 0x0066E0EC
      // (set) Token: 0x0600317A RID: 12666 RVA: 0x0066E0F4 File Offset: 0x0066E0F4
      [Nullable(2)]
      public string Text
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        private set;
      }

      // Token: 0x17000784 RID: 1924
      // (get) Token: 0x0600317B RID: 12667 RVA: 0x0066E100 File Offset: 0x0066E100
      // (set) Token: 0x0600317C RID: 12668 RVA: 0x0066E108 File Offset: 0x0066E108
      public double Height { get; private set; }

      // Token: 0x0600317D RID: 12669 RVA: 0x0066E114 File Offset: 0x0066E114
      public Row()
      {
        this.highlight.RenderTransform = new TranslateTransform
        {
          Y = 1.0
        };
        this.words.Children.Add(this.highlight);
        this.words.Children.Add(this.ink);
        this.bubble.Children.Add(this.words);
        this.View.Children.Add(this.bubble);
        this.dots.Children.Add(InterviewThoughtBubbles.Row.Dot(7.0, -6.0, 0.0));
        this.dots.Children.Add(InterviewThoughtBubbles.Row.Dot(5.0, -13.0, 5.0));
        this.dots.VerticalAlignment = VerticalAlignment.Bottom;
        this.dots.HorizontalAlignment = HorizontalAlignment.Left;
        this.View.Children.Add(this.dots);
      }

      // Token: 0x0600317E RID: 12670 RVA: 0x0066E30C File Offset: 0x0066E30C
      private static Ellipse Dot(double size, double left, double belowBottom)
      {
        Ellipse ellipse = new Ellipse();
        ellipse.Width = size;
        ellipse.Height = size;
        ellipse.Fill = Ui.Brush(DS.Hex(16768443U, 1.0));
        ellipse.Stroke = Ui.Brush(DS.Hex(10114328U, 1.0).WithAlpha(0.7));
        ellipse.StrokeThickness = 1.0;
        Canvas.SetLeft(ellipse, left);
        Canvas.SetTop(ellipse, belowBottom);
        return ellipse;
      }

      // Token: 0x0600317F RID: 12671 RVA: 0x0066E394 File Offset: 0x0066E394
      public void Set([Nullable(2)] string text, bool tail, InterviewThoughtBubbles owner)
      {
        if (text != this.Text || this.Height == 0.0)
        {
          this.Text = text;
          Size size = owner.Measure(text);
          this.bubble.Width = size.Width + 22.0;
          this.Height = (this.bubble.Height = (this.View.Height = size.Height + 14.0 + 6.0));
          if (text == null)
          {
            this.words.Visibility = Visibility.Collapsed;
            if (this.typing == null)
            {
              this.typing = InterviewThoughtBubbles.Row.TypingDots(size);
              this.bubble.Children.Add(this.typing);
            }
          }
          else
          {
            if (this.typing != null)
            {
              this.bubble.Children.Remove(this.typing);
              this.typing = null;
            }
            this.words.Visibility = Visibility.Visible;
            TextBlock textBlock = this.highlight;
            this.ink.Text = text;
            textBlock.Text = text;
          }
        }
        bool? flag = this.hasTail;
        if ((tail == flag.GetValueOrDefault()) & (flag != null))
        {
          return;
        }
        this.hasTail = new bool?(tail);
        if (this.plate != null)
        {
          this.bubble.Children.Remove(this.plate);
        }
        LinearGradientBrush linearGradientBrush = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Hex(16771542U, 1.0)),
          new ValueTuple<double, Color>(0.42, DS.Hex(16763289U, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Hex(16771542U, 1.0))
        });
        LinearGradientBrush linearGradientBrush2 = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Hex(10114328U, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Hex(6447714U, 1.0))
        });
        this.plate = ChatBlockViews.BubblePlate(linearGradientBrush, linearGradientBrush2, tail, false, 12.0, 6.0);
        if (!tail)
        {
          this.plate.Margin = new Thickness(0.0, 0.0, 0.0, 6.0);
        }
        this.bubble.Children.Insert(0, this.plate);
        this.dots.Visibility = (tail ? Visibility.Visible : Visibility.Collapsed);
      }

      // Token: 0x06003180 RID: 12672 RVA: 0x0066E660 File Offset: 0x0066E660
      private static FrameworkElement TypingDots(Size size)
      {
        StackPanel stackPanel = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 5.0,
          Margin = new Thickness(11.0, 7.0, 11.0, 13.0),
          VerticalAlignment = VerticalAlignment.Center,
          HorizontalAlignment = HorizontalAlignment.Center
        };
        for (int i = 0; i < 3; i++)
        {
          Ellipse ellipse = new Ellipse
          {
            Width = 6.0,
            Height = 6.0,
            Fill = Ui.Brush(DS.Black(0.7))
          };
          stackPanel.Children.Add(ellipse);
          if (!Anim.Instant && !Anim.ReduceMotion)
          {
            double phase = (double)i * 0.18;
            Anim.LoopScalar(ellipse, "Opacity", 1.2, (double t) => (float)(0.35 + 0.65 * Math.Max(0.0, Math.Sin(6.283185307179586 * (t / 1.2 - phase)))), 48, 0.0);
          }
        }
        AutomationProperties.SetName(stackPanel, "HeyClicky is thinking");
        return stackPanel;
      }

      // Token: 0x17000785 RID: 1925
      // (get) Token: 0x06003181 RID: 12673 RVA: 0x0066E780 File Offset: 0x0066E780
      private static bool StandsStill
      {
        get
        {
          return Anim.Instant || Anim.ReduceMotion;
        }
      }

      // Token: 0x06003182 RID: 12674 RVA: 0x0066E790 File Offset: 0x0066E790
      private static double Remaining(double seconds)
      {
        if (seconds < 0.42)
        {
          return (1.0 + 19.634954084936208 * seconds) * Math.Exp(-19.634954084936208 * seconds);
        }
        return 0.0;
      }

      // Token: 0x06003183 RID: 12675 RVA: 0x0066E7D0 File Offset: 0x0066E7D0
      public void Enter()
      {
        if (InterviewThoughtBubbles.Row.StandsStill)
        {
          return;
        }
        this.Glide(this.Height);
        this.View.Opacity = 0.0;
        Compositor compositor = Ui.Compositor;
        ScalarKeyFrameAnimation scalarKeyFrameAnimation = compositor.CreateScalarKeyFrameAnimation();
        for (int i = 1; i <= 24; i++)
        {
          scalarKeyFrameAnimation.InsertKeyFrame((float)i / 24f, (float)(1.0 - InterviewThoughtBubbles.Row.Remaining(0.42 * (double)i / 24.0)), compositor.CreateLinearEasingFunction());
        }
        scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(0.42);
        scalarKeyFrameAnimation.Target = "Opacity";
        this.View.StartAnimation(scalarKeyFrameAnimation);
      }

      // Token: 0x06003184 RID: 12676 RVA: 0x0066E884 File Offset: 0x0066E884
      public void Glide(double distance)
      {
        if (InterviewThoughtBubbles.Row.StandsStill)
        {
          return;
        }
        double num = (double)Environment.TickCount64;
        float num2 = (float)(distance + (double)this.glideFrom * InterviewThoughtBubbles.Row.Remaining((num - (double)this.glideStartedAt) / 1000.0));
        this.glideFrom = num2;
        this.glideStartedAt = (long)num;
        Compositor compositor = Ui.Compositor;
        Vector3KeyFrameAnimation vector3KeyFrameAnimation = compositor.CreateVector3KeyFrameAnimation();
        vector3KeyFrameAnimation.InsertKeyFrame(0f, new Vector3(0f, num2, 0f));
        for (int i = 1; i <= 24; i++)
        {
          vector3KeyFrameAnimation.InsertKeyFrame((float)i / 24f, new Vector3(0f, (float)((double)num2 * InterviewThoughtBubbles.Row.Remaining(0.42 * (double)i / 24.0)), 0f), compositor.CreateLinearEasingFunction());
        }
        vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(0.42);
        vector3KeyFrameAnimation.Target = "Translation";
        this.View.StartAnimation(vector3KeyFrameAnimation);
      }

      // Token: 0x04001CCE RID: 7374
      private const double Omega = 19.634954084936208;

      // Token: 0x04001CCF RID: 7375
      private const double Seconds = 0.42;

      // Token: 0x04001CD0 RID: 7376
      public readonly Grid View = new Grid
      {
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Top
      };

      // Token: 0x04001CD1 RID: 7377
      private readonly Grid bubble = new Grid
      {
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Top
      };

      // Token: 0x04001CD2 RID: 7378
      private readonly Grid words = new Grid
      {
        Margin = new Thickness(11.0, 7.0, 11.0, 13.0)
      };

      // Token: 0x04001CD3 RID: 7379
      private readonly TextBlock highlight = Ui.Text("", 13.0, FontWeights.Medium, DS.White(0.55), TextAlignment.Left, 0, true, false);

      // Token: 0x04001CD4 RID: 7380
      private readonly TextBlock ink = Ui.Text("", 13.0, FontWeights.Medium, DS.Black(0.86), TextAlignment.Left, 0, true, false);

      // Token: 0x04001CD5 RID: 7381
      private readonly Canvas dots = new Canvas
      {
        IsHitTestVisible = false
      };

      // Token: 0x04001CD6 RID: 7382
      [Nullable(2)]
      private FrameworkElement plate;

      // Token: 0x04001CD7 RID: 7383
      [Nullable(2)]
      private FrameworkElement typing;

      // Token: 0x04001CD8 RID: 7384
      private bool? hasTail;

      // Token: 0x04001CD9 RID: 7385
      private float glideFrom;

      // Token: 0x04001CDA RID: 7386
      private long glideStartedAt;
    }
  }
}
