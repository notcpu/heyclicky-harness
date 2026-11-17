using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200019E RID: 414
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkTopOfNote : Panel
  {
    // Token: 0x06001672 RID: 5746 RVA: 0x005BA9C8 File Offset: 0x005BA9C8
    public ClickyInkTopOfNote(ClickyHomeInkNote ink, ClickyHomeSpreadPaper paper, ClickyInkActions actions, bool isWorking, bool animates, bool isGlancePreview = false)
    {
      this.ink = ink;
      this.paper = paper;
      this.actions = actions;
      this.isWorking = isWorking;
      this.animates = animates;
      this.isGlancePreview = isGlancePreview;
      base.IsHitTestVisible = true;
    }

    // Token: 0x170003FB RID: 1019
    // (get) Token: 0x06001673 RID: 5747 RVA: 0x005BAA2C File Offset: 0x005BAA2C
    private double Margins
    {
      get
      {
        return 44.0;
      }
    }

    // Token: 0x06001674 RID: 5748 RVA: 0x005BAA38 File Offset: 0x005BAA38
    private double ColumnWidthFor(double width)
    {
      if (!this.isGlancePreview)
      {
        return Math.Max(378.0, width / 0.95 - this.Margins);
      }
      return 380.0;
    }

    // Token: 0x06001675 RID: 5749 RVA: 0x005BAA6C File Offset: 0x005BAA6C
    private bool IsWriting(DateTimeOffset now)
    {
      return (now - this.ink.StartedAt).TotalSeconds < this.ink.Note.FinishedClock;
    }

    // Token: 0x06001676 RID: 5750 RVA: 0x005BAAA4 File Offset: 0x005BAAA4
    private void Build(double width)
    {
      this.columnWidth = this.ColumnWidthFor(width);
      base.Children.Clear();
      this.timeline = null;
      ClickyInkEnvironment clickyInkEnvironment = ClickyInkEnvironment.Default.<Clone>$();
      clickyInkEnvironment.ContentWidth = this.columnWidth;
      clickyInkEnvironment.IsWindowPreview = true;
      clickyInkEnvironment.IsGlancePreview = this.isGlancePreview;
      ClickyInkLiveNote note = this.ink.Note;
      DateTimeOffset startedAt = this.ink.StartedAt;
      using (ClickyInkEnvironment.Use(clickyInkEnvironment))
      {
        DateTimeOffset now = DateTimeOffset.Now;
        bool flag = this.IsWriting(now);
        if (this.animates && (flag || this.isWorking) && !Anim.ReduceMotion)
        {
          bool wasWriting = flag;
          ClickyInkLiveNote clickyInkLiveNote = note;
          double totalSeconds = (now - startedAt).TotalSeconds;
          ClickyHomeSpreadPaper? clickyHomeSpreadPaper = new ClickyHomeSpreadPaper?(this.paper);
          ClickyInkActions clickyInkActions = this.actions;
          bool flag2 = false;
          bool? flag3 = new bool?(this.isWorking);
          this.sheetView = new ClickyInkSheetView(clickyInkLiveNote, totalSeconds, clickyHomeSpreadPaper, clickyInkActions, flag2, null, null, flag3, null, false, delegate(DateTimeOffset at)
          {
            bool stillWriting = this.IsWriting(at);
            if (stillWriting != wasWriting)
            {
              wasWriting = stillWriting;
              this.DispatcherQueue.TryEnqueue(delegate
              {
                ClickyInkTimeline clickyInkTimeline = this.timeline;
                if (clickyInkTimeline != null)
                {
                  clickyInkTimeline.MinimumInterval = ClickyInkTimeline.Interval(new double?(stillWriting ? 0.03333333333333333 : 1.0));
                }
              });
            }
            return (at - startedAt).TotalSeconds;
          });
          ClickyInkSheetView view = this.sheetView;
          this.timeline = new ClickyInkTimeline((DateTimeOffset _) => view, ClickyInkTimeline.Interval(new double?(flag ? 0.03333333333333333 : 1.0)));
          this.sheet = this.timeline;
        }
        else
        {
          ClickyInkLiveNote clickyInkLiveNote2 = note;
          double num = note.FinishedClock + 30.0;
          ClickyHomeSpreadPaper? clickyHomeSpreadPaper2 = new ClickyHomeSpreadPaper?(this.paper);
          ClickyInkActions clickyInkActions2 = this.actions;
          bool flag4 = false;
          bool? flag3 = new bool?(this.isWorking);
          this.sheetView = new ClickyInkSheetView(clickyInkLiveNote2, num, clickyHomeSpreadPaper2, clickyInkActions2, flag4, null, null, flag3, null, false, null);
          this.sheet = this.sheetView;
        }
      }
      this.sheet.RenderTransform = this.scale;
      base.Children.Add(this.sheet);
    }

    // Token: 0x06001677 RID: 5751 RVA: 0x005BACD8 File Offset: 0x005BACD8
    protected override Size MeasureOverride(Size availableSize)
    {
      double num = (double.IsInfinity(availableSize.Width) ? (378.0 + this.Margins) : availableSize.Width);
      if (this.sheet == null || this.ColumnWidthFor(num) != this.columnWidth)
      {
        this.Build(num);
      }
      double num2 = this.columnWidth + this.Margins;
      this.sheet.Measure(new Size(num2, double.PositiveInfinity));
      double num3 = (double.IsInfinity(availableSize.Height) ? (this.sheet.DesiredSize.Height * num / num2) : availableSize.Height);
      return new Size(num, num3);
    }

    // Token: 0x06001678 RID: 5752 RVA: 0x005BAD8C File Offset: 0x005BAD8C
    protected override Size ArrangeOverride(Size finalSize)
    {
      if (this.sheet == null)
      {
        return finalSize;
      }
      double num = this.columnWidth + this.Margins;
      double num2 = finalSize.Width / num;
      this.scale.ScaleX = (this.scale.ScaleY = num2);
      this.sheet.Arrange(new Rect(0.0, 0.0, num, this.sheet.DesiredSize.Height));
      base.Clip = new RectangleGeometry
      {
        Rect = new Rect(0.0, 0.0, finalSize.Width, finalSize.Height)
      };
      return finalSize;
    }

    // Token: 0x04000B03 RID: 2819
    private const double LargestScale = 0.95;

    // Token: 0x04000B04 RID: 2820
    private const double GlanceColumnWidth = 380.0;

    // Token: 0x04000B05 RID: 2821
    private readonly ClickyHomeInkNote ink;

    // Token: 0x04000B06 RID: 2822
    private readonly ClickyHomeSpreadPaper paper;

    // Token: 0x04000B07 RID: 2823
    private readonly ClickyInkActions actions;

    // Token: 0x04000B08 RID: 2824
    private readonly bool isWorking;

    // Token: 0x04000B09 RID: 2825
    private readonly bool animates;

    // Token: 0x04000B0A RID: 2826
    private readonly bool isGlancePreview;

    // Token: 0x04000B0B RID: 2827
    private readonly ScaleTransform scale = new ScaleTransform();

    // Token: 0x04000B0C RID: 2828
    [Nullable(2)]
    private FrameworkElement sheet;

    // Token: 0x04000B0D RID: 2829
    [Nullable(2)]
    private ClickyInkSheetView sheetView;

    // Token: 0x04000B0E RID: 2830
    [Nullable(2)]
    private ClickyInkTimeline timeline;

    // Token: 0x04000B0F RID: 2831
    private double columnWidth = double.NaN;
  }
}
