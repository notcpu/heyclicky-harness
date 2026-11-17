using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using HeyClicky.Agents;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000297 RID: 663
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkSheetView : Panel, IClickyInkClocked
  {
    // Token: 0x170004B6 RID: 1206
    // (get) Token: 0x06001B6D RID: 7021 RVA: 0x005EFBC8 File Offset: 0x005EFBC8
    // (set) Token: 0x06001B6E RID: 7022 RVA: 0x005EFBD0 File Offset: 0x005EFBD0
    public static bool FreezesRowsAtRest { get; set; } = AgentDefaults.Get<bool?>("ClickyInkSheetFreezesRowsAtRest").GetValueOrDefault(true);

    // Token: 0x170004B7 RID: 1207
    // (get) Token: 0x06001B6F RID: 7023 RVA: 0x005EFBD8 File Offset: 0x005EFBD8
    // (set) Token: 0x06001B70 RID: 7024 RVA: 0x005EFBE0 File Offset: 0x005EFBE0
    public ClickyInkPenAnchor? PenAnchor { get; private set; }

    // Token: 0x14000067 RID: 103
    // (add) Token: 0x06001B71 RID: 7025 RVA: 0x005EFBEC File Offset: 0x005EFBEC
    // (remove) Token: 0x06001B72 RID: 7026 RVA: 0x005EFC24 File Offset: 0x005EFC24
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<ClickyInkPenAnchor?> PenAnchorChanged;

    // Token: 0x170004B8 RID: 1208
    // (get) Token: 0x06001B73 RID: 7027 RVA: 0x005EFC5C File Offset: 0x005EFC5C
    // (set) Token: 0x06001B74 RID: 7028 RVA: 0x005EFC64 File Offset: 0x005EFC64
    public double? FollowOffset { get; private set; }

    // Token: 0x14000068 RID: 104
    // (add) Token: 0x06001B75 RID: 7029 RVA: 0x005EFC70 File Offset: 0x005EFC70
    // (remove) Token: 0x06001B76 RID: 7030 RVA: 0x005EFCA8 File Offset: 0x005EFCA8
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<double?> FollowOffsetChanged;

    // Token: 0x06001B77 RID: 7031 RVA: 0x005EFCE0 File Offset: 0x005EFCE0
    [NullableContext(2)]
    public ClickyInkSheetView([Nullable(1)] ClickyInkLiveNote note, double clock, ClickyHomeSpreadPaper? paper = null, ClickyInkActions actions = null, bool isEmbedded = false, double? followsPenInHeight = null, double? scrollFollowsPenInHeight = null, bool? isWorking = null, double? jobFinishedAt = null, bool drawsCard = true, Func<DateTimeOffset, double> clockAt = null)
    {
      this.note = note;
      this.paper = paper ?? ClickyHomeSpreadPaper.Note;
      this.actions = actions ?? new ClickyInkActions();
      this.isEmbedded = isEmbedded;
      this.followsPenInHeight = followsPenInHeight;
      this.scrollFollowsPenInHeight = scrollFollowsPenInHeight;
      this.isWorking = isWorking;
      this.jobFinishedAt = jobFinishedAt;
      this.drawsCard = drawsCard;
      this.clockAt = clockAt;
      this.environment = ClickyInkEnvironment.Current;
      this.Render(clock);
    }

    // Token: 0x170004B9 RID: 1209
    // (get) Token: 0x06001B78 RID: 7032 RVA: 0x005EFDA4 File Offset: 0x005EFDA4
    public ClickyInkLiveNote Note
    {
      get
      {
        return this.note;
      }
    }

    // Token: 0x06001B79 RID: 7033 RVA: 0x005EFDAC File Offset: 0x005EFDAC
    public void Render(DateTimeOffset now)
    {
      Func<DateTimeOffset, double> func = this.clockAt;
      this.Render((func != null) ? func(now) : this.clock);
    }

    // Token: 0x06001B7A RID: 7034 RVA: 0x005EFDCC File Offset: 0x005EFDCC
    public void Invalidate()
    {
      this.keptRows.Clear();
    }

    // Token: 0x06001B7B RID: 7035 RVA: 0x005EFDDC File Offset: 0x005EFDDC
    private new ClickyInkStyle Style(double at)
    {
      return new ClickyInkStyle(this.paper.Ink, this.paper.Quiet, new Color?(this.paper.Deep), this.note.LatestEdit(at)).Resting(this.note, this.isWorking);
    }

    // Token: 0x06001B7C RID: 7036 RVA: 0x005EFE34 File Offset: 0x005EFE34
    public void Render(double clock)
    {
      this.clock = clock;
      ClickyInkStyle clickyInkStyle = this.Style(clock);
      bool isWindowPreview = this.environment.IsWindowPreview;
      if (!this.isEmbedded)
      {
        double num = (isWindowPreview ? 8.0 : 28.0);
        double num2 = 30.0;
        this.topInset = num;
        this.bottomInset = num2;
      }
      else
      {
        double num2 = 30.0;
        double num = 40.0;
        this.topInset = num2;
        this.bottomInset = num;
      }
      this.windowMargin = (double)((this.followsPenInHeight != null && !this.isEmbedded && this.drawsCard) ? 14 : 0);
      this.horizontalInset = (isWindowPreview ? 22.0 : 36.0);
      double? num3 = this.followsPenInHeight;
      this.cameraHeight = ((num3 != null) ? num3 : this.scrollFollowsPenInHeight);
      List<ClickyInkLiveNote.PenFocus> list = ClickyInkSheetCamera.FocusOf(this.note);
      num3 = this.cameraHeight;
      this.focus = ((num3 == null) ? new List<ClickyInkLiveNote.PenFocus>() : list);
      num3 = this.cameraHeight;
      this.runwayFull = ((num3 == null) ? 1.0 : this.RunwayFull(clickyInkStyle));
      ClickyInkRowMotion.Table table = (ClickyInkSheetView.FreezesRowsAtRest ? ClickyInkRowMotion.TableFor(this.note) : null);
      List<ClickyInkSheetView.Row> list2 = this.Rows(table);
      ClickyInkLiveNote.PenFocus penFocus = this.focus.LastOrDefault<ClickyInkLiveNote.PenFocus>((ClickyInkLiveNote.PenFocus f) => f.At <= clock);
      this.isPenWriting = this.focus.Any<ClickyInkLiveNote.PenFocus>((ClickyInkLiveNote.PenFocus f) => f.At <= clock) && clock < penFocus.Until;
      HashSet<string> hashSet = (from f in list
        where f.At <= clock + 0.25 && clock < f.Until + 0.4
        select f.PlaceID).ToHashSet<string>();
      List<UIElement> list3 = new List<UIElement>();
      this.infos.Clear();
      using (ClickyInkEnvironment.Use(this.environment))
      {
        if (!this.isEmbedded && this.drawsCard)
        {
          if (this.background == null)
          {
            this.background = this.CardBackground();
          }
          list3.Add(this.background);
          this.infos.Add(new ClickyInkSheetRowInfo.Background());
        }
        HashSet<string> shownKeys = new HashSet<string>();
        foreach (ClickyInkSheetView.Row row in list2)
        {
          bool flag = ClickyInkSheetView.PlaceIDs(row).Any<string>(new Func<string, bool>(hashSet.Contains));
          double num4 = (flag ? clock : ((table != null) ? this.RowClock(row, table, clickyInkStyle) : clock));
          ClickyInkSheetRowKind clickyInkSheetRowKind = ((row.PageSettledAt == null) ? row.Kind.Resolved(num4, clock) : row.Kind);
          double num5 = (flag ? clock : 0.0);
          FrameworkElement frameworkElement2;
          if (num4 != clock)
          {
            string text = this.RowKey(row, num4, num5, clickyInkStyle, table);
            shownKeys.Add(text);
            FrameworkElement frameworkElement;
            if (!this.keptRows.TryGetValue(text, out frameworkElement))
            {
              frameworkElement = ClickyInkSheetRowView.Make(clickyInkSheetRowKind, num4, clickyInkStyle, this.paper, this.actions);
              this.keptRows[text] = frameworkElement;
            }
            frameworkElement2 = frameworkElement;
          }
          else
          {
            frameworkElement2 = ClickyInkSheetRowView.Make(clickyInkSheetRowKind, num4, clickyInkStyle, this.paper, this.actions);
          }
          list3.Add(frameworkElement2);
          this.infos.Add(row.Info);
        }
        IEnumerable<string> keys = this.keptRows.Keys;
        Func<string, bool> func;
        Func<string, bool> <>9__5;
        if ((func = <>9__5) == null)
        {
          func = (<>9__5 = (string k) => !shownKeys.Contains(k));
        }
        foreach (string text2 in keys.Where<string>(func).ToList<string>())
        {
          this.keptRows.Remove(text2);
        }
        if (this.isWorking.GetValueOrDefault())
        {
          num3 = clickyInkStyle.RestingSince(clock);
          if (num3 != null)
          {
            double valueOrDefault = num3.GetValueOrDefault();
            if (!list2.Any<ClickyInkSheetView.Row>((ClickyInkSheetView.Row r) => r.HasNowLine))
            {
              list3.Add(ClickyInkRestingPencil.Make(clock, valueOrDefault, clickyInkStyle.Ink, 15.0));
              this.infos.Add(new ClickyInkSheetRowInfo.Tail());
            }
          }
        }
      }
      this.Sync(list3);
      this.UpdateClip();
      base.InvalidateMeasure();
      base.InvalidateArrange();
    }

    // Token: 0x06001B7D RID: 7037 RVA: 0x005F0374 File Offset: 0x005F0374
    private void Sync(List<UIElement> wanted)
    {
      for (int i = 0; i < wanted.Count; i++)
      {
        if (i >= base.Children.Count || base.Children[i] != wanted[i])
        {
          int num = base.Children.IndexOf(wanted[i]);
          if (num >= 0)
          {
            base.Children.RemoveAt(num);
          }
          else
          {
            FrameworkElement frameworkElement = wanted[i] as FrameworkElement;
            if (frameworkElement != null)
            {
              Panel panel = frameworkElement.Parent as Panel;
              if (panel != null && panel != this)
              {
                panel.Children.Remove(wanted[i]);
              }
            }
          }
          base.Children.Insert(Math.Min(i, base.Children.Count), wanted[i]);
        }
      }
      while (base.Children.Count > wanted.Count)
      {
        base.Children.RemoveAt(base.Children.Count - 1);
      }
    }

    // Token: 0x06001B7E RID: 7038 RVA: 0x005F0470 File Offset: 0x005F0470
    private void UpdateClip()
    {
      double? num = this.followsPenInHeight;
      if (num != null)
      {
        double valueOrDefault = num.GetValueOrDefault();
        double sheetWidth = this.SheetWidth;
        base.Clip = new RectangleGeometry
        {
          Rect = new Rect(-40.0, 0.0, sheetWidth + 80.0, valueOrDefault)
        };
        return;
      }
      base.Clip = null;
    }

    // Token: 0x170004BA RID: 1210
    // (get) Token: 0x06001B7F RID: 7039 RVA: 0x005F04DC File Offset: 0x005F04DC
    private double ContentWidth
    {
      get
      {
        return this.environment.ContentWidth;
      }
    }

    // Token: 0x170004BB RID: 1211
    // (get) Token: 0x06001B80 RID: 7040 RVA: 0x005F04EC File Offset: 0x005F04EC
    private double SheetWidth
    {
      get
      {
        return this.ContentWidth + 2.0 * this.horizontalInset;
      }
    }

    // Token: 0x06001B81 RID: 7041 RVA: 0x005F0508 File Offset: 0x005F0508
    private double RunwayFull(ClickyInkStyle style)
    {
      ClickyInkPenSchedule penSchedule = style.PenSchedule;
      if (penSchedule != null)
      {
        double? num = penSchedule.IdleSince(this.clock);
        if (num != null)
        {
          double valueOrDefault = num.GetValueOrDefault();
          num = this.jobFinishedAt;
          double num2;
          if (num != null)
          {
            double valueOrDefault2 = num.GetValueOrDefault();
            num2 = valueOrDefault2;
          }
          else
          {
            bool? flag = this.isWorking;
            bool flag2 = false;
            if ((flag.GetValueOrDefault() == flag2) & (flag != null))
            {
              num2 = valueOrDefault;
            }
            else
            {
              if (this.isWorking != null || penSchedule.LooksWorking(this.clock))
              {
                return 1.0;
              }
              double? num3 = null;
              foreach (double num4 in penSchedule.Arrivals)
              {
                if (num4 <= this.clock)
                {
                  num3 = new double?(num4);
                }
              }
              num2 = num3.GetValueOrDefault(valueOrDefault) + 120.0;
            }
          }
          if (this.clock < num2)
          {
            return 1.0;
          }
          double num5 = Math.Max(valueOrDefault, num2) + 0.4;
          return 1.0 - ClickyInkClock.EaseInOut(ClickyInkClock.Progress(this.clock, num5, num5 + 1.2));
        }
      }
      return 1.0;
    }

    // Token: 0x06001B82 RID: 7042 RVA: 0x005F0678 File Offset: 0x005F0678
    private FrameworkElement CardBackground()
    {
      PaperSurface paperSurface = new PaperSurface(ClickyShape.RoundedRectangle(8.0), this.paper.Fill, new double?(0.45)).WithShadow(PaperColor.Black.Opacity(0.25), 10.0, 0.0, 4.0);
      Rectangle rectangle = new Rectangle
      {
        Width = 74.0,
        Height = 18.0,
        Fill = Ui.Brush(PaperColor.White.Opacity(0.55)),
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Top,
        IsHitTestVisible = false
      }.Transformed(0.0, -8.0, -2.0, 1.0, null);
      return new Grid
      {
        IsHitTestVisible = false,
        Children = { paperSurface, rectangle }
      };
    }

    // Token: 0x06001B83 RID: 7043 RVA: 0x005F0794 File Offset: 0x005F0794
    private static IReadOnlyList<string> PlaceIDs(ClickyInkSheetView.Row row)
    {
      ClickyInkSheetRowInfo.Row row2 = row.Info as ClickyInkSheetRowInfo.Row;
      if (row2 == null)
      {
        return Array.Empty<string>();
      }
      return row2.PlaceIDs;
    }

    // Token: 0x06001B84 RID: 7044 RVA: 0x005F07C0 File Offset: 0x005F07C0
    private List<ClickyInkSheetView.Row> Rows([Nullable(2)] ClickyInkRowMotion.Table motion)
    {
      bool isWindowPreview = this.environment.IsWindowPreview;
      bool isGlancePreview = this.environment.IsGlancePreview;
      Dictionary<string, double> dictionary = new Dictionary<string, double>();
      List<ClickyInkSheetView.Row> list = new List<ClickyInkSheetView.Row>();
      ClickyInkLiveNote.Words title = this.note.Title;
      if (title != null && !this.isEmbedded)
      {
        list.Add(new ClickyInkSheetView.Row("title", new ClickyInkSheetRowKind.Title(title), new ClickyInkSheetRowInfo.Row(new <>z__ReadOnlySingleElementList<string>("title"), dictionary), false, null));
      }
      bool flag = false;
      for (int i = 0; i < this.note.Pages.Count; i++)
      {
        ClickyInkLiveNote.Page page = this.note.Pages[i];
        if (isWindowPreview && flag)
        {
          break;
        }
        double? num = ((motion == null) ? null : ClickyInkRowMotion.SettledClock(page, this.clock, motion));
        List<ClickyInkGroup> list2;
        if (motion != null && num != null)
        {
          double valueOrDefault = num.GetValueOrDefault();
          list2 = ClickyInkRowMotionGroups.Groups(page, valueOrDefault, motion);
        }
        else
        {
          list2 = ClickyInkGroup.Groups(page, this.clock);
        }
        List<ClickyInkGroup> list3 = list2;
        double num2 = page.Name.StartsAt ?? page.LandsAt;
        bool flag2 = page.Blocks.Any<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.LandsAt > this.clock && b.VanishesAt == null);
        if (list3.Count != 0 || (this.clock >= num2 && flag2))
        {
          flag = true;
          if (!isWindowPreview && (i > 0 || !ClickyInkLiveNote.IsSilentPageName(page.Name.Current, i) || this.isEmbedded))
          {
            bool flag3;
            if (page.Blocks.Count > 0)
            {
              flag3 = page.Blocks.All<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.VanishesAt != null);
            }
            else
            {
              flag3 = false;
            }
            bool flag4 = flag3;
            double num3 = num2;
            double num4;
            if (list3.Count != 0)
            {
              num4 = list3.Min<ClickyInkGroup>((ClickyInkGroup g) => g.LandsAt);
            }
            else
            {
              num4 = num2;
            }
            double num5 = Math.Min(num3, num4);
            List<ClickyInkSheetView.Row> list4 = list;
            string text = ClickyInkLiveNote.PageFocusID(page.Id);
            int num6 = i;
            string id = page.Id;
            ClickyInkLiveNote.Words name = page.Name;
            double num7 = num5;
            double? num8;
            if (!flag4)
            {
              num8 = null;
            }
            else
            {
              num8 = new double?(page.Blocks.Max<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.VanishesAt.Value));
            }
            list4.Add(new ClickyInkSheetView.Row(text, new ClickyInkSheetRowKind.PageName(num6, id, name, num7, num8), new ClickyInkSheetRowInfo.Row(new <>z__ReadOnlySingleElementList<string>(ClickyInkLiveNote.PageFocusID(page.Id)), dictionary), false, null));
          }
          List<ClickyInkGroup> list5;
          if (!isGlancePreview)
          {
            list5 = list3;
          }
          else
          {
            list5 = list3.Where<ClickyInkGroup>((ClickyInkGroup g) => ClickyInkSheetView.GlanceKinds.Contains(g.Kind)).Take<ClickyInkGroup>(2).ToList<ClickyInkGroup>();
          }
          foreach (ClickyInkGroup clickyInkGroup in list5)
          {
            if (!isWindowPreview || clickyInkGroup.Kind != ClickyInkDocument.Kind.File)
            {
              Dictionary<string, double> dictionary2 = new Dictionary<string, double>();
              if (clickyInkGroup.Blocks.Count == 1 && clickyInkGroup.Blocks[0].Items.Count > 0)
              {
                List<ClickyInkLiveNote.Item> items = clickyInkGroup.Blocks[0].Items;
                List<double> list6 = items.Select<ClickyInkLiveNote.Item, double>(delegate(ClickyInkLiveNote.Item item)
                {
                  double num11 = ClickyInkClock.EaseInOut(ClickyInkClock.Progress(this.clock, item.LandsAt, item.LandsAt + 0.4));
                  double? vanishesAt = item.VanishesAt;
                  double num12;
                  if (vanishesAt != null)
                  {
                    double valueOrDefault3 = vanishesAt.GetValueOrDefault();
                    num12 = ClickyInkClock.EaseInOut(ClickyInkClock.Progress(this.clock, valueOrDefault3, valueOrDefault3 + 0.45));
                  }
                  else
                  {
                    num12 = 0.0;
                  }
                  double num13 = num12;
                  return num11 * (1.0 - num13);
                }).ToList<double>();
                double num9 = list6.Sum();
                double num10 = 0.0;
                for (int j = 0; j < items.Count; j++)
                {
                  num10 += list6[j];
                  dictionary2[items[j].Id] = ((num9 > 0.0) ? (num10 / num9) : ((double)(j + 1) / (double)items.Count));
                }
              }
              bool flag5 = clickyInkGroup.Blocks.Any<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.Items.Any<ClickyInkLiveNote.Item>((ClickyInkLiveNote.Item i) => i.Source.Mark.GetValueOrDefault() == ClickyInkDocument.Mark.Now));
              if (num != null)
              {
                goto IL_0409;
              }
              ValueTuple<double, ClickyInkGroup>? valueTuple = this.Resized(clickyInkGroup, page);
              if (valueTuple == null)
              {
                goto IL_0409;
              }
              ValueTuple<double, ClickyInkGroup> valueOrDefault2 = valueTuple.GetValueOrDefault();
              ClickyInkSheetRowKind.Resize resize = new ClickyInkSheetRowKind.Resize(valueOrDefault2.Item1, valueOrDefault2.Item2);
              IL_041F:
              ClickyInkSheetRowKind.Resize resize2 = resize;
              list.Add(new ClickyInkSheetView.Row(clickyInkGroup.Id, new ClickyInkSheetRowKind.Group(clickyInkGroup, resize2), new ClickyInkSheetRowInfo.Row(clickyInkGroup.Blocks.Select<ClickyInkLiveNote.Block, string>((ClickyInkLiveNote.Block b) => b.Id).ToList<string>(), dictionary2), flag5, num));
              continue;
              IL_0409:
              resize = null;
              goto IL_041F;
            }
          }
        }
      }
      return this.WithCollapsedPlaces(list);
    }

    // Token: 0x06001B85 RID: 7045 RVA: 0x005F0CA0 File Offset: 0x005F0CA0
    private List<ClickyInkSheetView.Row> WithCollapsedPlaces(List<ClickyInkSheetView.Row> rows)
    {
      ClickyInkSheetView.<>c__DisplayClass61_0 CS$<>8__locals1;
      CS$<>8__locals1.rows = rows;
      if ((this.followsPenInHeight == null && this.scrollFollowsPenInHeight == null) || CS$<>8__locals1.rows.Count == 0)
      {
        return CS$<>8__locals1.rows;
      }
      CS$<>8__locals1.rowOfPlace = new Dictionary<string, int>();
      for (int i = 0; i < CS$<>8__locals1.rows.Count; i++)
      {
        foreach (string text in ClickyInkSheetView.PlaceIDs(CS$<>8__locals1.rows[i]))
        {
          CS$<>8__locals1.rowOfPlace[text] = i;
        }
      }
      CS$<>8__locals1.pending = new List<string>();
      foreach (ClickyInkLiveNote.Page page in this.note.Pages)
      {
        if (this.clock >= page.LandsAt)
        {
          ClickyInkSheetView.<WithCollapsedPlaces>g__Attach|61_0(ClickyInkLiveNote.PageFocusID(page.Id), ref CS$<>8__locals1);
          foreach (ClickyInkLiveNote.Block block in page.Blocks)
          {
            if (this.clock >= block.LandsAt)
            {
              ClickyInkSheetView.<WithCollapsedPlaces>g__Attach|61_0(block.Id, ref CS$<>8__locals1);
            }
          }
        }
      }
      if (CS$<>8__locals1.pending.Count > 0)
      {
        List<ClickyInkSheetView.Row> rows2 = CS$<>8__locals1.rows;
        ClickyInkSheetRowInfo.Row row = rows2[rows2.Count - 1].Info as ClickyInkSheetRowInfo.Row;
        if (row != null)
        {
          List<ClickyInkSheetView.Row> rows3;
          int num = (rows3 = CS$<>8__locals1.rows).Count - 1;
          List<ClickyInkSheetView.Row> rows4 = CS$<>8__locals1.rows;
          ClickyInkSheetView.Row row2 = rows4[rows4.Count - 1].<Clone>$();
          ClickyInkSheetView.Row row3 = row2;
          ClickyInkSheetRowInfo.Row row4 = row.<Clone>$();
          ClickyInkSheetRowInfo.Row row5 = row4;
          IReadOnlyList<string> collapsedBelow = row.CollapsedBelow;
          List<string> pending = CS$<>8__locals1.pending;
          int num2 = 0;
          string[] array = new string[collapsedBelow.Count + pending.Count];
          foreach (string text2 in collapsedBelow)
          {
            array[num2] = text2;
            num2++;
          }
          Span<string> span = CollectionsMarshal.AsSpan<string>(pending);
          span.CopyTo(new Span<string>(array).Slice(num2, span.Length));
          num2 += span.Length;
          row5.CollapsedBelow = new <>z__ReadOnlyArray<string>(array);
          row3.Info = row4;
          rows3[num] = row2;
        }
      }
      return CS$<>8__locals1.rows;
    }

    // Token: 0x06001B86 RID: 7046 RVA: 0x005F0F68 File Offset: 0x005F0F68
    private double RowClock(ClickyInkSheetView.Row row, ClickyInkRowMotion.Table motion, ClickyInkStyle style)
    {
      if (ClickyHomeRendering.IsOffscreen)
      {
        return this.clock;
      }
      ClickyInkSheetRowKind kind = row.Kind;
      IReadOnlyList<string> readOnlyList;
      if (!(kind is ClickyInkSheetRowKind.Title))
      {
        ClickyInkSheetRowKind.PageName pageName = kind as ClickyInkSheetRowKind.PageName;
        if (pageName == null)
        {
          ClickyInkSheetRowKind.Group group = kind as ClickyInkSheetRowKind.Group;
          if (group == null)
          {
            return this.clock;
          }
          if (group.Resized != null || group.Value.Kind == ClickyInkDocument.Kind.Countdown)
          {
            return this.clock;
          }
          ClickyInkDocument.Kind kind2 = group.Value.Kind;
          bool flag = kind2 == ClickyInkDocument.Kind.Letter || kind2 - ClickyInkDocument.Kind.Ask <= 1;
          if (flag && this.actions.Feedback.Pending != null)
          {
            return this.clock;
          }
          if (row.HasNowLine && style.RestingSince(this.clock) != null)
          {
            return this.clock;
          }
          readOnlyList = group.Value.Blocks.Select<ClickyInkLiveNote.Block, string>((ClickyInkLiveNote.Block b) => b.Id).ToList<string>();
        }
        else
        {
          readOnlyList = new <>z__ReadOnlySingleElementList<string>(ClickyInkLiveNote.PageFocusID(pageName.PageID));
        }
      }
      else
      {
        readOnlyList = new <>z__ReadOnlySingleElementList<string>("title");
      }
      double? pageSettledAt = row.PageSettledAt;
      if (pageSettledAt != null)
      {
        double valueOrDefault = pageSettledAt.GetValueOrDefault();
        if (!row.HasNowLine)
        {
          return valueOrDefault;
        }
        ClickyInkRowMotion.State state = motion.RestingSwitches.StateAt(this.clock);
        double num;
        if (!(state is ClickyInkRowMotion.State.Moving))
        {
          ClickyInkRowMotion.State.Settled settled = state as ClickyInkRowMotion.State.Settled;
          if (settled == null)
          {
            num = valueOrDefault;
          }
          else
          {
            num = Math.Max(valueOrDefault, settled.At);
          }
        }
        else
        {
          num = this.clock;
        }
        return num;
      }
      else
      {
        double num2 = double.NegativeInfinity;
        ClickyInkRowMotion.State state = motion.Switches.StateAt(this.clock);
        if (!(state is ClickyInkRowMotion.State.Moving))
        {
          ClickyInkRowMotion.State.Settled settled2 = state as ClickyInkRowMotion.State.Settled;
          if (settled2 != null)
          {
            num2 = Math.Max(num2, settled2.At);
          }
          if (row.HasNowLine)
          {
            state = motion.RestingSwitches.StateAt(this.clock);
            if (state is ClickyInkRowMotion.State.Moving)
            {
              return this.clock;
            }
            ClickyInkRowMotion.State.Settled settled3 = state as ClickyInkRowMotion.State.Settled;
            if (settled3 != null)
            {
              num2 = Math.Max(num2, settled3.At);
            }
          }
          foreach (string text in readOnlyList)
          {
            ClickyInkRowMotion clickyInkRowMotion;
            if (!motion.Places.TryGetValue(text, out clickyInkRowMotion))
            {
              return this.clock;
            }
            state = clickyInkRowMotion.StateAt(this.clock);
            if (state is ClickyInkRowMotion.State.Moving)
            {
              return this.clock;
            }
            ClickyInkRowMotion.State.Settled settled4 = state as ClickyInkRowMotion.State.Settled;
            if (settled4 != null)
            {
              num2 = Math.Max(num2, settled4.At);
            }
          }
          if (!double.IsFinite(num2))
          {
            return this.clock;
          }
          return num2;
        }
        return this.clock;
      }
    }

    // Token: 0x06001B87 RID: 7047 RVA: 0x005F1248 File Offset: 0x005F1248
    private string RowKey(ClickyInkSheetView.Row row, double rowClock, double penClock, ClickyInkStyle style, ClickyInkRowMotion.Table motion)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 9);
      defaultInterpolatedStringHandler.AppendFormatted(motion.Key);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(row.Id);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(row.Kind.GetType().Name);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(InkText.DoubleDescription(rowClock));
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(InkText.DoubleDescription(penClock));
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<int?>(style.LatestEdit);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<bool?>(style.IsWorking);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      ClickyInkSentAction pending = this.actions.Feedback.Pending;
      defaultInterpolatedStringHandler.AppendFormatted<long?>((pending != null) ? new long?(pending.SentAt.ToUnixTimeMilliseconds()) : null);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<int>(this.actions.Feedback.Answered.Count);
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06001B88 RID: 7048 RVA: 0x005F1384 File Offset: 0x005F1384
    [return: TupleElementNames(new string[] { "At", "Before" })]
    [return: Nullable(new byte[] { 0, 1 })]
    private ValueTuple<double, ClickyInkGroup>? Resized(ClickyInkGroup group, ClickyInkLiveNote.Page page)
    {
      double? num = null;
      foreach (ClickyInkLiveNote.Block block in group.Blocks)
      {
        List<double> list = new List<double>();
        if (block.ShownSince() > block.LandsAt + 0.001)
        {
          list.Add(block.ShownSince());
        }
        list.AddRange(from i in block.Items
          where i.ShownSince > i.LandsAt + 0.001
          select i.ShownSince);
        foreach (double num2 in list)
        {
          if (this.clock >= num2 && this.clock < num2 + 0.4)
          {
            num = new double?(Math.Max(num.GetValueOrDefault(num2), num2));
          }
        }
      }
      if (num != null)
      {
        double valueOrDefault = num.GetValueOrDefault();
        if (group.Blocks.Count != 0)
        {
          string firstID = group.Blocks[0].Id;
          ClickyInkGroup clickyInkGroup = ClickyInkGroup.Groups(page, valueOrDefault - 0.001).FirstOrDefault<ClickyInkGroup>((ClickyInkGroup g) => g.Blocks.Count > 0 && g.Blocks[0].Id == firstID);
          if (clickyInkGroup != null)
          {
            return new ValueTuple<double, ClickyInkGroup>?(new ValueTuple<double, ClickyInkGroup>(valueOrDefault, clickyInkGroup));
          }
          return null;
        }
      }
      return null;
    }

    // Token: 0x06001B89 RID: 7049 RVA: 0x005F1570 File Offset: 0x005F1570
    protected override Size MeasureOverride(Size availableSize)
    {
      double sheetWidth = this.SheetWidth;
      List<double> list = this.StackHeights();
      double num = this.topInset + list.Sum() + this.bottomInset;
      double? num2 = this.cameraHeight;
      double num3;
      if (num2 != null)
      {
        double valueOrDefault = num2.GetValueOrDefault();
        num3 = ClickyInkSheetCamera.Runway(num, valueOrDefault, this.runwayFull);
      }
      else
      {
        num3 = 0.0;
      }
      double num4 = num3;
      int num5 = 0;
      while (num5 < base.Children.Count && num5 < this.infos.Count)
      {
        if (this.infos[num5] is ClickyInkSheetRowInfo.Background)
        {
          base.Children[num5].Measure(new Size(sheetWidth, num + num4));
        }
        num5++;
      }
      num2 = this.followsPenInHeight;
      if (num2 != null)
      {
        double valueOrDefault2 = num2.GetValueOrDefault();
        return new Size(sheetWidth, valueOrDefault2);
      }
      return new Size(sheetWidth, num + num4);
    }

    // Token: 0x06001B8A RID: 7050 RVA: 0x005F165C File Offset: 0x005F165C
    private List<double> StackHeights()
    {
      List<double> list = new List<double>(this.infos.Count);
      int num = 0;
      while (num < base.Children.Count && num < this.infos.Count)
      {
        ClickyInkSheetRowInfo clickyInkSheetRowInfo = this.infos[num];
        if (!(clickyInkSheetRowInfo is ClickyInkSheetRowInfo.Background))
        {
          if (!(clickyInkSheetRowInfo is ClickyInkSheetRowInfo.Tail))
          {
            base.Children[num].Measure(new Size(this.ContentWidth, double.PositiveInfinity));
            list.Add(base.Children[num].DesiredSize.Height);
          }
          else
          {
            base.Children[num].Measure(new Size(this.ContentWidth, double.PositiveInfinity));
            list.Add(0.0);
          }
        }
        else
        {
          list.Add(0.0);
        }
        num++;
      }
      return list;
    }

    // Token: 0x06001B8B RID: 7051 RVA: 0x005F1750 File Offset: 0x005F1750
    protected override Size ArrangeOverride(Size finalSize)
    {
      List<double> list = new List<double>(this.infos.Count);
      int num = 0;
      while (num < base.Children.Count && num < this.infos.Count)
      {
        list.Add((this.infos[num] is ClickyInkSheetRowInfo.Row) ? base.Children[num].DesiredSize.Height : 0.0);
        num++;
      }
      double num2 = this.topInset + list.Sum() + this.bottomInset;
      double num3 = 0.0;
      double num4 = 0.0;
      double? num5 = null;
      bool flag = this.followsPenInHeight != null;
      double? num6 = this.cameraHeight;
      if (num6 != null)
      {
        double valueOrDefault = num6.GetValueOrDefault();
        num4 = ClickyInkSheetCamera.Runway(num2, valueOrDefault, this.runwayFull);
        Dictionary<string, ClickyInkSheetCamera.Place> dictionary = new Dictionary<string, ClickyInkSheetCamera.Place>();
        Dictionary<string, double> dictionary2 = new Dictionary<string, double>();
        double num7 = this.topInset;
        for (int i = 0; i < list.Count; i++)
        {
          ClickyInkSheetRowInfo.Row row = this.infos[i] as ClickyInkSheetRowInfo.Row;
          if (row != null)
          {
            foreach (string text in row.PlaceIDs)
            {
              dictionary[text] = new ClickyInkSheetCamera.Place(num7, list[i], row.ItemFractions);
            }
            foreach (string text2 in row.CollapsedAbove)
            {
              dictionary[text2] = new ClickyInkSheetCamera.Place(num7, 0.0, dictionary2);
            }
            foreach (string text3 in row.CollapsedBelow)
            {
              dictionary[text3] = new ClickyInkSheetCamera.Place(num7 + list[i], 0.0, dictionary2);
            }
          }
          num7 += list[i];
        }
        double num8 = (flag ? this.windowMargin : 0.0);
        num3 = ClickyInkSheetCamera.Offset(this.focus, dictionary, this.clock, valueOrDefault, Math.Max(0.0, num8 + num2 + num4 - valueOrDefault)) - num8;
        num5 = ClickyInkSheetCamera.PenLine(this.focus, dictionary, this.clock);
      }
      double num9 = (flag ? num3 : 0.0);
      double num10 = this.topInset - num9;
      double sheetWidth = this.SheetWidth;
      for (int j = 0; j < list.Count; j++)
      {
        UIElement uielement = base.Children[j];
        ClickyInkSheetRowInfo clickyInkSheetRowInfo = this.infos[j];
        if (!(clickyInkSheetRowInfo is ClickyInkSheetRowInfo.Background))
        {
          if (!(clickyInkSheetRowInfo is ClickyInkSheetRowInfo.Tail))
          {
            uielement.Arrange(new Rect(this.horizontalInset, num10, this.ContentWidth, list[j]));
            num10 += list[j];
          }
          else
          {
            uielement.Arrange(new Rect(this.horizontalInset, num10, Math.Min(this.ContentWidth, uielement.DesiredSize.Width), uielement.DesiredSize.Height));
          }
        }
        else
        {
          uielement.Arrange(new Rect(0.0, -num9, sheetWidth, num2 + num4));
        }
      }
      num6 = this.cameraHeight;
      ClickyInkPenAnchor? clickyInkPenAnchor = ((num6 == null) ? null : new ClickyInkPenAnchor?(new ClickyInkPenAnchor(num5.GetValueOrDefault() - num9, this.isPenWriting)));
      double? num11 = ((this.scrollFollowsPenInHeight == null) ? null : new double?(num3));
      if (clickyInkPenAnchor != this.PenAnchor)
      {
        this.PenAnchor = clickyInkPenAnchor;
        Action<ClickyInkPenAnchor?> penAnchorChanged = this.PenAnchorChanged;
        if (penAnchorChanged != null)
        {
          penAnchorChanged(clickyInkPenAnchor);
        }
      }
      num6 = num11;
      double? followOffset = this.FollowOffset;
      if (!((num6.GetValueOrDefault() == followOffset.GetValueOrDefault()) & (num6 != null == (followOffset != null))))
      {
        this.FollowOffset = num11;
        Action<double?> followOffsetChanged = this.FollowOffsetChanged;
        if (followOffsetChanged != null)
        {
          followOffsetChanged(num11);
        }
      }
      return finalSize;
    }

    // Token: 0x06001B8F RID: 7055 RVA: 0x005F1D68 File Offset: 0x005F1D68
    [CompilerGenerated]
    internal static void <WithCollapsedPlaces>g__Attach|61_0(string placeID, ref ClickyInkSheetView.<>c__DisplayClass61_0 A_1)
    {
      int num;
      if (A_1.rowOfPlace.TryGetValue(placeID, out num))
      {
        if (A_1.pending.Count > 0)
        {
          ClickyInkSheetRowInfo.Row row = A_1.rows[num].Info as ClickyInkSheetRowInfo.Row;
          if (row != null)
          {
            List<ClickyInkSheetView.Row> rows = A_1.rows;
            int num2 = num;
            ClickyInkSheetView.Row row2 = A_1.rows[num].<Clone>$();
            ClickyInkSheetView.Row row3 = row2;
            ClickyInkSheetRowInfo.Row row4 = row.<Clone>$();
            ClickyInkSheetRowInfo.Row row5 = row4;
            IReadOnlyList<string> collapsedAbove = row.CollapsedAbove;
            List<string> pending = A_1.pending;
            int num3 = 0;
            string[] array = new string[collapsedAbove.Count + pending.Count];
            foreach (string text in collapsedAbove)
            {
              array[num3] = text;
              num3++;
            }
            Span<string> span = CollectionsMarshal.AsSpan<string>(pending);
            span.CopyTo(new Span<string>(array).Slice(num3, span.Length));
            num3 += span.Length;
            row5.CollapsedAbove = new <>z__ReadOnlyArray<string>(array);
            row3.Info = row4;
            rows[num2] = row2;
          }
        }
        A_1.pending = new List<string>();
        return;
      }
      A_1.pending.Add(placeID);
    }

    // Token: 0x04000D29 RID: 3369
    private readonly ClickyInkLiveNote note;

    // Token: 0x04000D2A RID: 3370
    private readonly ClickyHomeSpreadPaper paper;

    // Token: 0x04000D2B RID: 3371
    private readonly ClickyInkActions actions;

    // Token: 0x04000D2C RID: 3372
    private readonly bool isEmbedded;

    // Token: 0x04000D2D RID: 3373
    private readonly double? followsPenInHeight;

    // Token: 0x04000D2E RID: 3374
    private readonly double? scrollFollowsPenInHeight;

    // Token: 0x04000D2F RID: 3375
    private readonly bool? isWorking;

    // Token: 0x04000D30 RID: 3376
    private readonly double? jobFinishedAt;

    // Token: 0x04000D31 RID: 3377
    private readonly bool drawsCard;

    // Token: 0x04000D32 RID: 3378
    [Nullable(2)]
    private readonly Func<DateTimeOffset, double> clockAt;

    // Token: 0x04000D33 RID: 3379
    private readonly ClickyInkEnvironment environment;

    // Token: 0x04000D35 RID: 3381
    private static readonly HashSet<ClickyInkDocument.Kind> GlanceKinds = new HashSet<ClickyInkDocument.Kind>
    {
      ClickyInkDocument.Kind.Answer,
      ClickyInkDocument.Kind.Tiles,
      ClickyInkDocument.Kind.Checklist,
      ClickyInkDocument.Kind.Question,
      ClickyInkDocument.Kind.Paragraph,
      ClickyInkDocument.Kind.Status,
      ClickyInkDocument.Kind.Progress,
      ClickyInkDocument.Kind.Run,
      ClickyInkDocument.Kind.Quiet,
      ClickyInkDocument.Kind.Countdown,
      ClickyInkDocument.Kind.Connect,
      ClickyInkDocument.Kind.Spark
    };

    // Token: 0x04000D36 RID: 3382
    private const int GlanceGroupLimit = 2;

    // Token: 0x04000D37 RID: 3383
    private readonly List<ClickyInkSheetRowInfo> infos = new List<ClickyInkSheetRowInfo>();

    // Token: 0x04000D38 RID: 3384
    private double topInset;

    // Token: 0x04000D39 RID: 3385
    private double bottomInset;

    // Token: 0x04000D3A RID: 3386
    private double horizontalInset;

    // Token: 0x04000D3B RID: 3387
    private double windowMargin;

    // Token: 0x04000D3C RID: 3388
    private double runwayFull = 1.0;

    // Token: 0x04000D3D RID: 3389
    private double clock;

    // Token: 0x04000D3E RID: 3390
    private double? cameraHeight;

    // Token: 0x04000D3F RID: 3391
    private List<ClickyInkLiveNote.PenFocus> focus = new List<ClickyInkLiveNote.PenFocus>();

    // Token: 0x04000D40 RID: 3392
    private bool isPenWriting;

    // Token: 0x04000D41 RID: 3393
    [Nullable(2)]
    private FrameworkElement background;

    // Token: 0x04000D42 RID: 3394
    private readonly Dictionary<string, FrameworkElement> keptRows = new Dictionary<string, FrameworkElement>();

    // Token: 0x0200086D RID: 2157
    [Nullable(0)]
    private sealed class Row : IEquatable<ClickyInkSheetView.Row>
    {
      // Token: 0x06003924 RID: 14628 RVA: 0x006889EC File Offset: 0x006889EC
      public Row(string Id, ClickyInkSheetRowKind Kind, ClickyInkSheetRowInfo Info, bool HasNowLine = false, double? PageSettledAt = null)
      {
        this.Id = Id;
        this.Kind = Kind;
        this.Info = Info;
        this.HasNowLine = HasNowLine;
        this.PageSettledAt = PageSettledAt;
        base..ctor();
      }

      // Token: 0x170007F7 RID: 2039
      // (get) Token: 0x06003925 RID: 14629 RVA: 0x00688A1C File Offset: 0x00688A1C
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkSheetView.Row);
        }
      }

      // Token: 0x170007F8 RID: 2040
      // (get) Token: 0x06003926 RID: 14630 RVA: 0x00688A28 File Offset: 0x00688A28
      // (set) Token: 0x06003927 RID: 14631 RVA: 0x00688A30 File Offset: 0x00688A30
      public string Id { get; set; }

      // Token: 0x170007F9 RID: 2041
      // (get) Token: 0x06003928 RID: 14632 RVA: 0x00688A3C File Offset: 0x00688A3C
      // (set) Token: 0x06003929 RID: 14633 RVA: 0x00688A44 File Offset: 0x00688A44
      public ClickyInkSheetRowKind Kind { get; set; }

      // Token: 0x170007FA RID: 2042
      // (get) Token: 0x0600392A RID: 14634 RVA: 0x00688A50 File Offset: 0x00688A50
      // (set) Token: 0x0600392B RID: 14635 RVA: 0x00688A58 File Offset: 0x00688A58
      public ClickyInkSheetRowInfo Info { get; set; }

      // Token: 0x170007FB RID: 2043
      // (get) Token: 0x0600392C RID: 14636 RVA: 0x00688A64 File Offset: 0x00688A64
      // (set) Token: 0x0600392D RID: 14637 RVA: 0x00688A6C File Offset: 0x00688A6C
      public bool HasNowLine { get; set; }

      // Token: 0x170007FC RID: 2044
      // (get) Token: 0x0600392E RID: 14638 RVA: 0x00688A78 File Offset: 0x00688A78
      // (set) Token: 0x0600392F RID: 14639 RVA: 0x00688A80 File Offset: 0x00688A80
      public double? PageSettledAt { get; set; }

      // Token: 0x06003930 RID: 14640 RVA: 0x00688A8C File Offset: 0x00688A8C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Row");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003931 RID: 14641 RVA: 0x00688AD8 File Offset: 0x00688AD8
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Id = ");
        builder.Append(this.Id);
        builder.Append(", Kind = ");
        builder.Append(this.Kind);
        builder.Append(", Info = ");
        builder.Append(this.Info);
        builder.Append(", HasNowLine = ");
        builder.Append(this.HasNowLine.ToString());
        builder.Append(", PageSettledAt = ");
        builder.Append(this.PageSettledAt.ToString());
        return true;
      }

      // Token: 0x06003932 RID: 14642 RVA: 0x00688B84 File Offset: 0x00688B84
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkSheetView.Row left, ClickyInkSheetView.Row right)
      {
        return !(left == right);
      }

      // Token: 0x06003933 RID: 14643 RVA: 0x00688B90 File Offset: 0x00688B90
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkSheetView.Row left, ClickyInkSheetView.Row right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003934 RID: 14644 RVA: 0x00688BA4 File Offset: 0x00688BA4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<ClickyInkSheetRowKind>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<ClickyInkSheetRowInfo>.Default.GetHashCode(this.<Info>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<HasNowLine>k__BackingField)) * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(this.<PageSettledAt>k__BackingField);
      }

      // Token: 0x06003935 RID: 14645 RVA: 0x00688C34 File Offset: 0x00688C34
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkSheetView.Row);
      }

      // Token: 0x06003936 RID: 14646 RVA: 0x00688C44 File Offset: 0x00688C44
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkSheetView.Row other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<ClickyInkSheetRowKind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<ClickyInkSheetRowInfo>.Default.Equals(this.<Info>k__BackingField, other.<Info>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<HasNowLine>k__BackingField, other.<HasNowLine>k__BackingField) && EqualityComparer<double?>.Default.Equals(this.<PageSettledAt>k__BackingField, other.<PageSettledAt>k__BackingField));
      }

      // Token: 0x06003938 RID: 14648 RVA: 0x00688CF4 File Offset: 0x00688CF4
      [CompilerGenerated]
      private Row(ClickyInkSheetView.Row original)
      {
        this.Id = original.<Id>k__BackingField;
        this.Kind = original.<Kind>k__BackingField;
        this.Info = original.<Info>k__BackingField;
        this.HasNowLine = original.<HasNowLine>k__BackingField;
        this.PageSettledAt = original.<PageSettledAt>k__BackingField;
      }

      // Token: 0x06003939 RID: 14649 RVA: 0x00688D44 File Offset: 0x00688D44
      [CompilerGenerated]
      public void Deconstruct(out string Id, out ClickyInkSheetRowKind Kind, out ClickyInkSheetRowInfo Info, out bool HasNowLine, out double? PageSettledAt)
      {
        Id = this.Id;
        Kind = this.Kind;
        Info = this.Info;
        HasNowLine = this.HasNowLine;
        PageSettledAt = this.PageSettledAt;
      }
    }
  }
}
