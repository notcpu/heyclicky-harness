using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000286 RID: 646
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkPageView : Grid, IClickyInkClocked
  {
    // Token: 0x06001B32 RID: 6962 RVA: 0x005EADC4 File Offset: 0x005EADC4
    [NullableContext(2)]
    public ClickyInkPageView([Nullable(1)] ClickyInkLiveNote note, int pageIndex, double clock, ClickyHomeSpreadPaper? paper = null, ClickyInkActions actions = null, int part = 0, Func<DateTimeOffset, double> clockAt = null)
    {
      this.note = note;
      this.pageIndex = pageIndex;
      this.part = part;
      this.paper = paper ?? ClickyHomeSpreadPaper.Note;
      this.actions = actions ?? new ClickyInkActions();
      this.clockAt = clockAt;
      base.Width = ClickyInkLayout.PageSize.Width;
      base.Height = ClickyInkLayout.PageSize.Height;
      this.columnFrame.Children.Add(this.column);
      this.columnFrame.RenderTransform = this.columnScale;
      base.Children.Add(this.sheet);
      this.Render(clock);
    }

    // Token: 0x170004B0 RID: 1200
    // (get) Token: 0x06001B33 RID: 6963 RVA: 0x005EAED8 File Offset: 0x005EAED8
    private ClickyInkLiveNote.Page Page
    {
      get
      {
        return this.note.Pages[this.pageIndex];
      }
    }

    // Token: 0x170004B1 RID: 1201
    // (get) Token: 0x06001B34 RID: 6964 RVA: 0x005EAEF0 File Offset: 0x005EAEF0
    private bool CarriesTitle
    {
      get
      {
        return this.pageIndex == 0 && this.part == 0 && this.note.Title != null;
      }
    }

    // Token: 0x170004B2 RID: 1202
    // (get) Token: 0x06001B35 RID: 6965 RVA: 0x005EAF18 File Offset: 0x005EAF18
    private double ContentTop
    {
      get
      {
        return ClickyInkLayout.ContentTopFor(this.CarriesTitle, this.part > 0);
      }
    }

    // Token: 0x06001B36 RID: 6966 RVA: 0x005EAF30 File Offset: 0x005EAF30
    private ClickyInkStyle StyleAt(double clock)
    {
      return new ClickyInkStyle(this.paper.Ink, this.paper.Quiet, new Color?(this.paper.Deep), this.note.LatestEdit(clock)).Resting(this.note, null);
    }

    // Token: 0x06001B37 RID: 6967 RVA: 0x005EAF88 File Offset: 0x005EAF88
    public void Render(DateTimeOffset now)
    {
      Func<DateTimeOffset, double> func = this.clockAt;
      this.Render((func != null) ? func(now) : 0.0);
    }

    // Token: 0x06001B38 RID: 6968 RVA: 0x005EAFAC File Offset: 0x005EAFAC
    public void Invalidate()
    {
      this.restingRows.Clear();
      this.headingKey = "";
      this.rowKeys.Clear();
    }

    // Token: 0x06001B39 RID: 6969 RVA: 0x005EAFD0 File Offset: 0x005EAFD0
    public void Render(double clock)
    {
      List<ClickyInkGroup> list = this.Groups(clock);
      this.sheet.Children.Clear();
      if (this.part > 0 && list.Count == 0)
      {
        return;
      }
      ClickyInkStyle clickyInkStyle = this.StyleAt(clock);
      ClickyInkRowMotion.Table table = ClickyInkRowMotion.TableFor(this.note);
      double? num = ClickyInkPageView.RowClock(new <>z__ReadOnlyArray<string>(new string[]
      {
        "title",
        ClickyInkLiveNote.PageFocusID(this.Page.Id)
      }), clock, table);
      string text;
      if (num != null)
      {
        double valueOrDefault = num.GetValueOrDefault();
        text = table.Key + "|" + InkText.DoubleDescription(valueOrDefault);
      }
      else
      {
        text = "";
      }
      string text2 = text;
      if (this.heading == null || text2.Length == 0 || text2 != this.headingKey)
      {
        double valueOrDefault2 = num.GetValueOrDefault(clock);
        this.heading = this.Heading(valueOrDefault2, (num == null) ? clickyInkStyle : this.StyleAt(valueOrDefault2));
        this.headingKey = text2;
      }
      Canvas.SetLeft(this.heading, 36.0);
      Canvas.SetTop(this.heading, 26.0);
      this.sheet.Children.Add(this.heading);
      double scale = ClickyInkPagination.Parts(this.note, this.pageIndex, this.paper).Scale;
      double num2 = ClickyInkLayout.PageSize.Height - this.ContentTop - 26.0;
      this.columnFrame.Width = 378.0;
      this.columnFrame.Height = num2 / scale;
      this.columnScale.ScaleX = (this.columnScale.ScaleY = scale);
      Grid grid = new Grid
      {
        Width = 378.0,
        Height = num2,
        Clip = new RectangleGeometry
        {
          Rect = new Rect(0.0, 0.0, 378.0, num2)
        },
        Children = { this.columnFrame }
      };
      Grid grid2 = this.columnFrame.Parent as Grid;
      if (grid2 != null && grid2 != grid)
      {
        grid2.Children.Clear();
      }
      List<string> keys = new List<string>();
      List<FrameworkElement> list2 = new List<FrameworkElement>();
      foreach (ClickyInkGroup clickyInkGroup in list)
      {
        double? num3 = ClickyInkPageView.RowClock(clickyInkGroup.Blocks.Select<ClickyInkLiveNote.Block, string>((ClickyInkLiveNote.Block b) => b.Id), clock, table);
        string text3;
        if (num3 != null)
        {
          double valueOrDefault3 = num3.GetValueOrDefault();
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 4);
          defaultInterpolatedStringHandler.AppendFormatted(table.Key);
          defaultInterpolatedStringHandler.AppendLiteral("|");
          defaultInterpolatedStringHandler.AppendFormatted(clickyInkGroup.Id);
          defaultInterpolatedStringHandler.AppendLiteral("|");
          defaultInterpolatedStringHandler.AppendFormatted<int>(clickyInkGroup.Blocks.Count);
          defaultInterpolatedStringHandler.AppendLiteral("|");
          defaultInterpolatedStringHandler.AppendFormatted(InkText.DoubleDescription(valueOrDefault3));
          text3 = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        else
        {
          text3 = "";
        }
        string text4 = text3;
        FrameworkElement frameworkElement;
        FrameworkElement frameworkElement2;
        if (text4.Length > 0 && this.restingRows.TryGetValue(text4, out frameworkElement))
        {
          frameworkElement2 = frameworkElement;
        }
        else
        {
          double valueOrDefault4 = num3.GetValueOrDefault(clock);
          ClickyInkGroup clickyInkGroup2;
          if (num3 != null)
          {
            double valueOrDefault5 = num3.GetValueOrDefault();
            clickyInkGroup2 = this.Regrouped(clickyInkGroup, valueOrDefault5);
          }
          else
          {
            clickyInkGroup2 = clickyInkGroup;
          }
          ClickyInkGroup clickyInkGroup3 = clickyInkGroup2;
          FrameworkElement frameworkElement3 = ClickyInkGroupView.Make(clickyInkGroup3, valueOrDefault4, (num3 == null) ? clickyInkStyle : this.StyleAt(valueOrDefault4), this.paper, this.actions);
          frameworkElement3.Margin = new Thickness(0.0, 0.0, 0.0, 14.0);
          frameworkElement2 = ClickyInkGrow.Around(frameworkElement3, valueOrDefault4, clickyInkGroup3.LandsAt, clickyInkGroup3.VanishesAt);
          if (text4.Length > 0)
          {
            this.restingRows[text4] = frameworkElement2;
          }
        }
        keys.Add(text4);
        list2.Add(frameworkElement2);
      }
      IEnumerable<string> keys2 = this.restingRows.Keys;
      Func<string, bool> <>9__1;
      Func<string, bool> func;
      if ((func = <>9__1) == null)
      {
        func = (<>9__1 = (string k) => !keys.Contains(k));
      }
      foreach (string text5 in keys2.Where<string>(func).ToList<string>())
      {
        this.restingRows.Remove(text5);
      }
      for (int i = 0; i < list2.Count; i++)
      {
        if (i < this.column.Children.Count)
        {
          if (this.column.Children[i] != list2[i])
          {
            Panel panel = list2[i].Parent as Panel;
            if (panel != null && panel != this.column)
            {
              panel.Children.Remove(list2[i]);
            }
            if (this.column.Children.Contains(list2[i]))
            {
              this.column.Children.Remove(list2[i]);
            }
            this.column.Children.Insert(i, list2[i]);
          }
        }
        else
        {
          Panel panel2 = list2[i].Parent as Panel;
          if (panel2 != null && panel2 != this.column)
          {
            panel2.Children.Remove(list2[i]);
          }
          if (this.column.Children.Contains(list2[i]))
          {
            this.column.Children.Remove(list2[i]);
          }
          this.column.Children.Add(list2[i]);
        }
      }
      while (this.column.Children.Count > list2.Count)
      {
        this.column.Children.RemoveAt(this.column.Children.Count - 1);
      }
      this.rowKeys.Clear();
      this.rowKeys.AddRange(keys);
      Canvas.SetLeft(grid, 36.0);
      Canvas.SetTop(grid, this.ContentTop);
      this.sheet.Children.Add(grid);
    }

    // Token: 0x06001B3A RID: 6970 RVA: 0x005EB6B8 File Offset: 0x005EB6B8
    public static double? RowClock(IEnumerable<string> placeIDs, double clock, ClickyInkRowMotion.Table table)
    {
      if (ClickyHomeRendering.IsOffscreen)
      {
        double? num = null;
        return num;
      }
      double num2 = double.NegativeInfinity;
      Func<string, ClickyInkRowMotion> <>9__0;
      Func<string, ClickyInkRowMotion> func;
      if ((func = <>9__0) == null)
      {
        func = (<>9__0 = delegate(string id)
        {
          ClickyInkRowMotion clickyInkRowMotion2;
          if (!table.Places.TryGetValue(id, out clickyInkRowMotion2))
          {
            return null;
          }
          return clickyInkRowMotion2;
        });
      }
      foreach (ClickyInkRowMotion clickyInkRowMotion in placeIDs.Select<string, ClickyInkRowMotion>(func).Append(table.Switches).Append(table.RestingSwitches))
      {
        if (clickyInkRowMotion == null)
        {
          double? num = null;
          return num;
        }
        ClickyInkRowMotion.State state = clickyInkRowMotion.StateAt(clock);
        if (state is ClickyInkRowMotion.State.Moving)
        {
          return null;
        }
        ClickyInkRowMotion.State.Settled settled = state as ClickyInkRowMotion.State.Settled;
        if (settled != null)
        {
          num2 = Math.Max(num2, settled.At);
        }
      }
      if (!double.IsFinite(num2))
      {
        return null;
      }
      return new double?(num2);
    }

    // Token: 0x06001B3B RID: 6971 RVA: 0x005EB7D0 File Offset: 0x005EB7D0
    private ClickyInkGroup Regrouped(ClickyInkGroup group, double clock)
    {
      return ClickyInkGroup.Groups(this.Page, clock).FirstOrDefault<ClickyInkGroup>((ClickyInkGroup g) => g.Id == group.Id) ?? group;
    }

    // Token: 0x06001B3C RID: 6972 RVA: 0x005EB814 File Offset: 0x005EB814
    private FrameworkElement Heading(double clock, ClickyInkStyle style)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 2.0,
        Width = 378.0
      };
      if (this.CarriesTitle)
      {
        ClickyInkLiveNote.Words title = this.note.Title;
        if (title != null)
        {
          stackPanel.Children.Add(ClickyInkWordsView.Make(title, clock, style, ClickyPaperFonts.Hand(31.0), true, null, null).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(1);
            t.MinimumScale = 0.6;
          }));
          if (this.Page.Name.Current.ToLowerInvariant() != "in short")
          {
            stackPanel.Children.Add(ClickyInkWordsView.Make(this.Page.Name, clock, style, ClickyPaperFonts.Hand(18.0), true, new Color?(style.Quiet), null));
            return stackPanel;
          }
          return stackPanel;
        }
      }
      if (this.part > 0)
      {
        stackPanel.Children.Add(InkTextView.Plain(this.Page.Name.Current + ", continued", ClickyPaperFonts.Hand(18.0), style.Quiet).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
        }));
      }
      else
      {
        stackPanel.Children.Add(ClickyInkWordsView.Make(this.Page.Name, clock, style, ClickyPaperFonts.Hand(27.0), true, null, null).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
        }));
        ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyInkSquiggle(this.Page.Id.CharacterCount()).Trim(0.0, ClickyInkClock.Progress(clock, this.Page.Name.FinishedAt, this.Page.Name.FinishedAt + 0.3)), new PaperStroke(style.Ink.Opacity(0.35), 1.4, PenLineCap.Round, PenLineJoin.Miter, null));
        clickyShapeView.Width = 70.0;
        clickyShapeView.Height = 6.0;
        clickyShapeView.HorizontalAlignment = HorizontalAlignment.Left;
        stackPanel.Children.Add(clickyShapeView);
      }
      return stackPanel;
    }

    // Token: 0x06001B3D RID: 6973 RVA: 0x005EBAAC File Offset: 0x005EBAAC
    private unsafe List<ClickyInkGroup> Groups(double clock)
    {
      List<ClickyInkGroup> list = ClickyInkGroup.Groups(this.Page, clock);
      ClickyInkPagination.PartsInfo parts = ClickyInkPagination.Parts(this.note, this.pageIndex, this.paper);
      if (parts.Count <= 1)
      {
        return list;
      }
      List<ClickyInkGroup> list2 = new List<ClickyInkGroup>();
      using (List<ClickyInkGroup>.Enumerator enumerator = list.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ClickyInkGroup group = enumerator.Current;
          int num3;
          if (group.Blocks.Count == 1 && parts.ItemPartOf.Keys.Any<string>((string k) => k.StartsWith(group.Blocks[0].Id + "|", StringComparison.Ordinal)))
          {
            ClickyInkLiveNote.Block block = group.Blocks[0].Clone();
            block.Items = group.Blocks[0].Items.Where<ClickyInkLiveNote.Item>(delegate(ClickyInkLiveNote.Item item)
            {
              int num4;
              return (parts.ItemPartOf.TryGetValue(ClickyInkPagination.ItemKey(block.Id, item.Id), out num4) ? num4 : 0) == this.part;
            }).ToList<ClickyInkLiveNote.Item>();
            if (block.Items.Count > 0)
            {
              List<ClickyInkGroup> list3 = list2;
              ClickyInkGroup clickyInkGroup = group.<Clone>$();
              int num = 1;
              List<ClickyInkLiveNote.Block> list4 = new List<ClickyInkLiveNote.Block>(num);
              CollectionsMarshal.SetCount<ClickyInkLiveNote.Block>(list4, num);
              Span<ClickyInkLiveNote.Block> span = CollectionsMarshal.AsSpan<ClickyInkLiveNote.Block>(list4);
              int num2 = 0;
              *span[num2] = block;
              clickyInkGroup.Blocks = list4;
              list3.Add(clickyInkGroup);
            }
          }
          else if ((parts.PartOf.TryGetValue(group.Blocks[0].Id, out num3) ? num3 : 0) == this.part)
          {
            list2.Add(group);
          }
        }
      }
      return list2;
    }

    // Token: 0x04000D06 RID: 3334
    private readonly ClickyInkLiveNote note;

    // Token: 0x04000D07 RID: 3335
    private readonly int pageIndex;

    // Token: 0x04000D08 RID: 3336
    private readonly int part;

    // Token: 0x04000D09 RID: 3337
    private readonly ClickyHomeSpreadPaper paper;

    // Token: 0x04000D0A RID: 3338
    private readonly ClickyInkActions actions;

    // Token: 0x04000D0B RID: 3339
    [Nullable(2)]
    private readonly Func<DateTimeOffset, double> clockAt;

    // Token: 0x04000D0C RID: 3340
    private readonly Canvas sheet = new Canvas();

    // Token: 0x04000D0D RID: 3341
    private readonly StackPanel column = new StackPanel();

    // Token: 0x04000D0E RID: 3342
    private readonly Grid columnFrame = new Grid();

    // Token: 0x04000D0F RID: 3343
    private readonly ScaleTransform columnScale = new ScaleTransform();

    // Token: 0x04000D10 RID: 3344
    private readonly Dictionary<string, FrameworkElement> restingRows = new Dictionary<string, FrameworkElement>();

    // Token: 0x04000D11 RID: 3345
    [Nullable(2)]
    private FrameworkElement heading;

    // Token: 0x04000D12 RID: 3346
    private string headingKey = "";

    // Token: 0x04000D13 RID: 3347
    private readonly List<string> rowKeys = new List<string>();
  }
}
