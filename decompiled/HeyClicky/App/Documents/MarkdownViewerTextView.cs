using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using HeyClicky.Core.Documents.Markdown;
using HeyClicky.Core.Telemetry;
using Microsoft.UI.Input;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Text;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002D9 RID: 729
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MarkdownViewerTextView : Grid
  {
    // Token: 0x17000587 RID: 1415
    // (get) Token: 0x06001EB5 RID: 7861 RVA: 0x0060880C File Offset: 0x0060880C
    public bool UsesPageLayout { get; }

    // Token: 0x17000588 RID: 1416
    // (get) Token: 0x06001EB6 RID: 7862 RVA: 0x00608814 File Offset: 0x00608814
    public double TextScale { get; }

    // Token: 0x17000589 RID: 1417
    // (get) Token: 0x06001EB7 RID: 7863 RVA: 0x0060881C File Offset: 0x0060881C
    // (set) Token: 0x06001EB8 RID: 7864 RVA: 0x00608824 File Offset: 0x00608824
    [Nullable(2)]
    public Func<string> WholeDocumentText
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06001EB9 RID: 7865 RVA: 0x00608830 File Offset: 0x00608830
    public MarkdownViewerTextView(bool usesPageLayout, double textScale = 1.0)
    {
      this.UsesPageLayout = usesPageLayout;
      this.TextScale = textScale;
      this.text = new RichTextBlock
      {
        IsTextSelectionEnabled = true,
        IsTextScaleFactorEnabled = false,
        TextWrapping = TextWrapping.Wrap,
        OpticalMarginAlignment = OpticalMarginAlignment.None,
        Foreground = Ui.Brush(this.colors.BodyText),
        FontFamily = MarkdownViewerFonts.System(16.0),
        FontSize = 16.0,
        SelectionHighlightColor = Ui.Brush(DS.Rgb(0.25, 0.42, 0.7, 1.0))
      };
      base.Children.Add(this.plateLayer);
      base.Children.Add(this.text);
      base.Children.Add(this.overlay);
      this.overlay.IsHitTestVisible = true;
      this.overlay.Background = null;
      if (usesPageLayout)
      {
        this.text.Margin = new Thickness(36.0, 52.0, 36.0, 52.0);
      }
      base.SizeChanged += delegate(object _, SizeChangedEventArgs e)
      {
        this.OnWidthChanged(e.NewSize.Width);
      };
      this.text.LayoutUpdated += delegate([Nullable(2)] object _, object _)
      {
        this.LayoutChrome();
      };
      this.text.AddHandler(UIElement.KeyDownEvent, new KeyEventHandler(this.OnKeyDown), true);
      MenuFlyout menuFlyout = new MenuFlyout();
      MenuFlyoutItem menuFlyoutItem = new MenuFlyoutItem
      {
        Text = "Copy"
      };
      menuFlyoutItem.Click += delegate(object _, RoutedEventArgs _)
      {
        this.CopySelection();
      };
      MenuFlyoutItem menuFlyoutItem2 = new MenuFlyoutItem
      {
        Text = "Select All"
      };
      menuFlyoutItem2.Click += delegate(object _, RoutedEventArgs _)
      {
        this.text.SelectAll();
      };
      menuFlyout.Items.Add(menuFlyoutItem);
      menuFlyout.Items.Add(menuFlyoutItem2);
      this.text.ContextFlyout = menuFlyout;
      this.text.SelectionFlyout = null;
    }

    // Token: 0x06001EBA RID: 7866 RVA: 0x00608AB8 File Offset: 0x00608AB8
    public void Install(MarkdownViewerPreparedDocument document)
    {
      this.paras.Clear();
      this.boxes.Clear();
      this.segments.Clear();
      this.tables.Clear();
      this.images.Clear();
      foreach (ValueTuple<MarkdownViewerTextView.Box, string, MarkdownViewerTextView.CopyBlockButton> valueTuple in this.copyButtons)
      {
        MarkdownViewerTextView.CopyBlockButton item = valueTuple.Item3;
        this.overlay.Children.Remove(item);
      }
      this.copyButtons.Clear();
      this.inlineCodeRuns.Clear();
      this.text.Blocks.Clear();
      this.text.TextHighlighters.Clear();
      this.lastLayoutSize = default(Size);
      this.lastAvailableWidth = -1.0;
      new MarkdownViewerTextView.Builder(this, document).Render(document.Blocks);
      if (!this.UsesPageLayout && this.paras.Count > 0)
      {
        List<MarkdownViewerTextView.Para> list = this.paras;
        list[list.Count - 1].SpacingAfter = 0.0;
      }
      this.ApplyParagraphMetrics();
      this.LayoutEmbeddedContent(true);
      base.InvalidateMeasure();
    }

    // Token: 0x06001EBB RID: 7867 RVA: 0x00608C00 File Offset: 0x00608C00
    private void OnWidthChanged(double width)
    {
      if (this.UsesPageLayout)
      {
        double num = Math.Max(36.0, (width - 660.0) / 2.0);
        if (Math.Abs(this.text.Margin.Left - num) > 0.5)
        {
          this.text.Margin = new Thickness(num, 52.0, num, 52.0);
        }
      }
      this.LayoutEmbeddedContent(false);
    }

    // Token: 0x1700058A RID: 1418
    // (get) Token: 0x06001EBC RID: 7868 RVA: 0x00608C8C File Offset: 0x00608C8C
    private double ColumnWidth
    {
      get
      {
        return Math.Max(0.0, base.ActualWidth - this.text.Margin.Left - this.text.Margin.Right);
      }
    }

    // Token: 0x06001EBD RID: 7869 RVA: 0x00608CD8 File Offset: 0x00608CD8
    private double AvailableWidth(MarkdownViewerTextView.Para para, double columnWidth)
    {
      return Math.Max(1.0, columnWidth - para.Boxes.Sum<MarkdownViewerTextView.Box>((MarkdownViewerTextView.Box b) => b.PadLeft + b.PadRight));
    }

    // Token: 0x06001EBE RID: 7870 RVA: 0x00608D14 File Offset: 0x00608D14
    private void LayoutEmbeddedContent(bool force)
    {
      double columnWidth = this.ColumnWidth;
      if (columnWidth <= 0.0)
      {
        return;
      }
      if (!force && Math.Abs(columnWidth - this.lastAvailableWidth) < 0.5)
      {
        return;
      }
      this.lastAvailableWidth = columnWidth;
      foreach (ValueTuple<MarkdownViewerTextView.Para, MarkdownViewerTableView, MarkdownViewerTableContent, InlineUIContainer> valueTuple in this.tables)
      {
        valueTuple.Item2.LayoutTable(this.AvailableWidth(valueTuple.Item1, columnWidth));
      }
      foreach (ValueTuple<MarkdownViewerTextView.Para, FrameworkElement, Size> valueTuple2 in this.images)
      {
        double num = 1.0;
        double num2 = this.AvailableWidth(valueTuple2.Item1, columnWidth);
        double num3 = 1.0;
        Size size = valueTuple2.Item3;
        double num4 = Math.Min(num, num2 / Math.Max(num3, size.Width));
        FrameworkElement item = valueTuple2.Item2;
        size = valueTuple2.Item3;
        item.Width = size.Width * num4;
        FrameworkElement item2 = valueTuple2.Item2;
        size = valueTuple2.Item3;
        item2.Height = size.Height * num4;
      }
    }

    // Token: 0x06001EBF RID: 7871 RVA: 0x00608E70 File Offset: 0x00608E70
    private void ApplyParagraphMetrics()
    {
      double num = 0.0;
      for (int i = 0; i < this.paras.Count; i++)
      {
        MarkdownViewerTextView.Para para = this.paras[i];
        double num2 = para.Indent;
        double num3 = para.ExtraRight;
        double num4 = para.SpacingBefore;
        double num5 = para.SpacingAfter;
        foreach (MarkdownViewerTextView.Box box in para.Boxes)
        {
          num2 += box.PadLeft;
          num3 += box.PadRight;
          if (box.First == para)
          {
            num4 += box.PadTop + box.MarginTop;
          }
          if (box.Last == para)
          {
            num5 += box.PadBottom + box.MarginBottom;
          }
        }
        bool flag = i == this.paras.Count - 1;
        para.P.Margin = new Thickness(num2, num + num4, num3, flag ? num5 : 0.0);
        num = num5;
      }
    }

    // Token: 0x06001EC0 RID: 7872 RVA: 0x00608FA0 File Offset: 0x00608FA0
    private static double OffsetAbove(MarkdownViewerTextView.Box target, MarkdownViewerTextView.Para para)
    {
      MarkdownViewerTextView.Box box;
      if (para.Boxes.Count <= 0)
      {
        box = null;
      }
      else
      {
        List<MarkdownViewerTextView.Box> list = para.Boxes;
        box = list[list.Count - 1];
      }
      MarkdownViewerTextView.Box box2 = box;
      double num = 0.0;
      if (box2 == null || box2.First != para)
      {
        num += para.SpacingBefore;
      }
      for (int i = para.Boxes.Count - 1; i >= 0; i--)
      {
        MarkdownViewerTextView.Box box3 = para.Boxes[i];
        if (box3.First == para)
        {
          num += box3.PadTop;
          if (box3 == target)
          {
            return num;
          }
          num += box3.MarginTop;
          if (box3 == box2)
          {
            num += para.SpacingBefore;
          }
        }
      }
      return num;
    }

    // Token: 0x06001EC1 RID: 7873 RVA: 0x00609048 File Offset: 0x00609048
    private static double OffsetBelow(MarkdownViewerTextView.Box target, MarkdownViewerTextView.Para para)
    {
      MarkdownViewerTextView.Box box;
      if (para.Boxes.Count <= 0)
      {
        box = null;
      }
      else
      {
        List<MarkdownViewerTextView.Box> list = para.Boxes;
        box = list[list.Count - 1];
      }
      MarkdownViewerTextView.Box box2 = box;
      double num = 0.0;
      if (box2 == null || box2.Last != para)
      {
        num += para.SpacingAfter;
      }
      for (int i = para.Boxes.Count - 1; i >= 0; i--)
      {
        MarkdownViewerTextView.Box box3 = para.Boxes[i];
        if (box3.Last == para)
        {
          num += box3.PadBottom;
          if (box3 == target)
          {
            return num;
          }
          num += box3.MarginBottom;
          if (box3 == box2)
          {
            num += para.SpacingAfter;
          }
        }
      }
      return num;
    }

    // Token: 0x06001EC2 RID: 7874 RVA: 0x006090F0 File Offset: 0x006090F0
    private void LayoutChrome()
    {
      if (this.paras.Count == 0)
      {
        return;
      }
      Size size = new Size(this.ColumnWidth, this.text.ActualHeight);
      if (size == this.lastLayoutSize || size.Width <= 0.0)
      {
        return;
      }
      this.lastLayoutSize = size;
      this.plateLayer.Children.Clear();
      double left = this.text.Margin.Left;
      double top = this.text.Margin.Top;
      double width = size.Width;
      Dictionary<MarkdownViewerTextView.Box, Rect> dictionary = new Dictionary<MarkdownViewerTextView.Box, Rect>();
      foreach (MarkdownViewerTextView.Box box in this.boxes)
      {
        if (box.First != null && box.Last != null)
        {
          Rect rect = this.LineRect(box.First, true);
          Rect rect2 = this.LineRect(box.Last, false);
          double num = rect.Top - MarkdownViewerTextView.OffsetAbove(box, box.First);
          double num2 = rect2.Bottom + MarkdownViewerTextView.OffsetBelow(box, box.Last);
          double num3 = 0.0;
          double num4 = width;
          foreach (MarkdownViewerTextView.Box box2 in box.First.Boxes)
          {
            if (box2 == box)
            {
              break;
            }
            num3 += box2.PadLeft;
            num4 -= box2.PadRight;
          }
          Rect rect3 = new Rect(left + num3, top + num, Math.Max(0.0, num4 - num3), Math.Max(0.0, num2 - num));
          dictionary[box] = rect3;
          this.DrawPlate(box, rect3);
        }
      }
      this.DrawInlineCodeChips(left, top);
      foreach (ValueTuple<MarkdownViewerTextView.Box, string, MarkdownViewerTextView.CopyBlockButton> valueTuple in this.copyButtons)
      {
        MarkdownViewerTextView.Box item = valueTuple.Item1;
        MarkdownViewerTextView.CopyBlockButton item2 = valueTuple.Item3;
        Rect rect4;
        if (dictionary.TryGetValue(item, out rect4))
        {
          Canvas.SetLeft(item2, Math.Min(rect4.Right, left + width) - 22.0 - 6.0);
          Canvas.SetTop(item2, rect4.Top + 6.0);
        }
      }
    }

    // Token: 0x06001EC3 RID: 7875 RVA: 0x006093C4 File Offset: 0x006093C4
    private Rect LineRect(MarkdownViewerTextView.Para para, bool start)
    {
      Rect rect;
      try
      {
        Rect characterRect = (start ? para.P.ContentStart : para.P.ContentEnd).GetCharacterRect(start ? LogicalDirection.Forward : LogicalDirection.Backward);
        if (para.LineHeight > 0.0)
        {
          double num = (start ? characterRect.Top : (characterRect.Bottom - para.LineHeight));
          rect = new Rect(characterRect.X, num, characterRect.Width, para.LineHeight);
        }
        else
        {
          rect = characterRect;
        }
      }
      catch
      {
        rect = default(Rect);
      }
      return rect;
    }

    // Token: 0x06001EC4 RID: 7876 RVA: 0x00609464 File Offset: 0x00609464
    private void DrawPlate(MarkdownViewerTextView.Box box, Rect rect)
    {
      MarkdownViewerTextView.BoxKind kind = box.Kind;
      if (kind != MarkdownViewerTextView.BoxKind.Quote)
      {
        if (kind - MarkdownViewerTextView.BoxKind.Code > 1)
        {
          return;
        }
        Rectangle rectangle = new Rectangle
        {
          Width = rect.Width,
          Height = ((box.Kind == MarkdownViewerTextView.BoxKind.Rule) ? box.RuleHeight : rect.Height),
          Fill = Ui.Brush((box.Kind == MarkdownViewerTextView.BoxKind.Code) ? this.colors.CodeBlockBackground : this.colors.Hairline)
        };
        Canvas.SetLeft(rectangle, rect.X);
        Canvas.SetTop(rectangle, rect.Y);
        this.plateLayer.Children.Add(rectangle);
      }
      else
      {
        Rectangle rectangle2 = new Rectangle
        {
          Width = Math.Max(0.0, rect.Width - 1.0),
          Height = Math.Max(0.0, rect.Height - 1.0),
          RadiusX = 8.0,
          RadiusY = 8.0,
          Fill = Ui.Brush(this.colors.QuotePlateFill),
          Stroke = Ui.Brush(this.colors.QuotePlateStroke),
          StrokeThickness = 1.0
        };
        Canvas.SetLeft(rectangle2, rect.X + 0.5);
        Canvas.SetTop(rectangle2, rect.Y + 0.5);
        this.plateLayer.Children.Add(rectangle2);
        double num = rect.Height - 2.0 * box.PadTop;
        if (num > 0.0)
        {
          Rectangle rectangle3 = new Rectangle
          {
            Width = 2.0,
            Height = num,
            RadiusX = 1.0,
            RadiusY = 1.0,
            Fill = Ui.Brush(this.colors.QuotePlateBar)
          };
          Canvas.SetLeft(rectangle3, rect.X + 12.0);
          Canvas.SetTop(rectangle3, rect.Y + box.PadTop);
          this.plateLayer.Children.Add(rectangle3);
          return;
        }
      }
    }

    // Token: 0x06001EC5 RID: 7877 RVA: 0x006096B0 File Offset: 0x006096B0
    private void DrawInlineCodeChips(double originX, double originY)
    {
      MarkdownViewerTextView.<>c__DisplayClass37_0 CS$<>8__locals1;
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.originX = originX;
      CS$<>8__locals1.originY = originY;
      foreach (Run run in this.inlineCodeRuns)
      {
        int num = run.ContentEnd.Offset - run.ContentStart.Offset;
        MarkdownViewerTextView.<>c__DisplayClass37_1 CS$<>8__locals2;
        CS$<>8__locals2.line = null;
        int i = 0;
        while (i < num)
        {
          Rect characterRect;
          Rect characterRect2;
          try
          {
            characterRect = run.ContentStart.GetPositionAtOffset(i, LogicalDirection.Forward).GetCharacterRect(LogicalDirection.Forward);
            characterRect2 = run.ContentStart.GetPositionAtOffset(i + 1, LogicalDirection.Forward).GetCharacterRect(LogicalDirection.Backward);
          }
          catch
          {
            goto IL_01A1;
          }
          goto IL_0096;
          IL_01A1:
          i++;
          continue;
          IL_0096:
          double num2 = ((Math.Abs(characterRect2.Top - characterRect.Top) < 0.5) ? characterRect2.X : (characterRect.X + characterRect.Width));
          Rect rect = new Rect(characterRect.X, characterRect.Top, Math.Max(0.0, num2 - characterRect.X), characterRect.Height);
          if (CS$<>8__locals2.line != null)
          {
            Rect valueOrDefault = CS$<>8__locals2.line.GetValueOrDefault();
            if (Math.Abs(valueOrDefault.Top - rect.Top) < 0.5)
            {
              CS$<>8__locals2.line = new Rect?(new Rect(valueOrDefault.X, valueOrDefault.Y, Math.Max(valueOrDefault.Width, rect.X + rect.Width - valueOrDefault.X), valueOrDefault.Height));
              goto IL_01A1;
            }
          }
          this.<DrawInlineCodeChips>g__Flush|37_0(ref CS$<>8__locals1, ref CS$<>8__locals2);
          CS$<>8__locals2.line = new Rect?(rect);
          goto IL_01A1;
        }
        this.<DrawInlineCodeChips>g__Flush|37_0(ref CS$<>8__locals1, ref CS$<>8__locals2);
      }
    }

    // Token: 0x06001EC6 RID: 7878 RVA: 0x006098C8 File Offset: 0x006098C8
    private void OnKeyDown(object sender, KeyRoutedEventArgs e)
    {
      if (InputKeyboardSource.GetKeyStateForCurrentThread(VirtualKey.Control).HasFlag(CoreVirtualKeyStates.Down) && e.Key == VirtualKey.C)
      {
        this.CopySelection();
        e.Handled = true;
      }
    }

    // Token: 0x06001EC7 RID: 7879 RVA: 0x006098FC File Offset: 0x006098FC
    public void SelectAllText()
    {
      this.text.SelectAll();
    }

    // Token: 0x06001EC8 RID: 7880 RVA: 0x0060990C File Offset: 0x0060990C
    public void CopySelection()
    {
      string text;
      if (string.IsNullOrEmpty(this.text.SelectedText))
      {
        Func<string> wholeDocumentText = this.WholeDocumentText;
        text = ((wholeDocumentText != null) ? wholeDocumentText() : null);
      }
      else
      {
        text = this.SelectedPlainText();
      }
      if (string.IsNullOrEmpty(text))
      {
        return;
      }
      DataPackage dataPackage = new DataPackage();
      dataPackage.SetText(text);
      Clipboard.SetContent(dataPackage);
    }

    // Token: 0x06001EC9 RID: 7881 RVA: 0x00609964 File Offset: 0x00609964
    public string SelectedPlainText()
    {
      int num = this.text.SelectionStart.Offset;
      int num2 = this.text.SelectionEnd.Offset;
      if (num2 < num)
      {
        int num3 = num2;
        num2 = num;
        num = num3;
      }
      StringBuilder stringBuilder = new StringBuilder();
      foreach (MarkdownViewerTextView.CopySegment copySegment in this.segments)
      {
        TextElement element = copySegment.Element;
        Run run = element as Run;
        if (run == null)
        {
          Paragraph paragraph = element as Paragraph;
          if (paragraph == null)
          {
            if (element != null)
            {
              if (element.ContentStart.Offset >= num && element.ContentEnd.Offset <= num2)
              {
                stringBuilder.Append(copySegment.Text);
              }
            }
          }
          else
          {
            int offset = paragraph.ContentEnd.Offset;
            if (offset >= num && offset < num2)
            {
              stringBuilder.Append(copySegment.Text);
            }
          }
        }
        else
        {
          int offset2 = run.ContentStart.Offset;
          int offset3 = run.ContentEnd.Offset;
          int num4 = Math.Max(num, offset2);
          int num5 = Math.Min(num2, offset3);
          if (num5 > num4)
          {
            stringBuilder.Append(run.Text.AsSpan(num4 - offset2, Math.Min(num5 - num4, run.Text.Length - (num4 - offset2))));
          }
        }
      }
      return stringBuilder.ToString();
    }

    // Token: 0x06001ECA RID: 7882 RVA: 0x00609AEC File Offset: 0x00609AEC
    private void AddCopyButton(MarkdownViewerTextView.Box target, string copyText)
    {
      MarkdownViewerTextView.CopyBlockButton copyBlockButton = new MarkdownViewerTextView.CopyBlockButton(copyText, this.look);
      this.overlay.Children.Add(copyBlockButton);
      this.copyButtons.Add(new ValueTuple<MarkdownViewerTextView.Box, string, MarkdownViewerTextView.CopyBlockButton>(target, copyText, copyBlockButton));
    }

    // Token: 0x06001ECF RID: 7887 RVA: 0x00609B70 File Offset: 0x00609B70
    [CompilerGenerated]
    private void <DrawInlineCodeChips>g__Flush|37_0(ref MarkdownViewerTextView.<>c__DisplayClass37_0 A_1, ref MarkdownViewerTextView.<>c__DisplayClass37_1 A_2)
    {
      if (A_2.line != null)
      {
        Rect valueOrDefault = A_2.line.GetValueOrDefault();
        if (valueOrDefault.Width > 0.0)
        {
          Rectangle rectangle = new Rectangle
          {
            Width = valueOrDefault.Width,
            Height = valueOrDefault.Height,
            Fill = Ui.Brush(this.colors.InlineCodeBackground)
          };
          Canvas.SetLeft(rectangle, A_1.originX + valueOrDefault.X);
          Canvas.SetTop(rectangle, A_1.originY + valueOrDefault.Y);
          this.plateLayer.Children.Add(rectangle);
          return;
        }
      }
    }

    // Token: 0x04000ED4 RID: 3796
    private readonly MessagesPalette look = MessagesPalette.Current;

    // Token: 0x04000ED5 RID: 3797
    private readonly MarkdownViewerPalette colors = MarkdownViewerPalette.Current;

    // Token: 0x04000ED6 RID: 3798
    private readonly Canvas plateLayer = new Canvas
    {
      IsHitTestVisible = false
    };

    // Token: 0x04000ED7 RID: 3799
    private readonly RichTextBlock text;

    // Token: 0x04000ED8 RID: 3800
    private readonly Canvas overlay = new Canvas();

    // Token: 0x04000ED9 RID: 3801
    private readonly List<MarkdownViewerTextView.Para> paras = new List<MarkdownViewerTextView.Para>();

    // Token: 0x04000EDA RID: 3802
    private readonly List<MarkdownViewerTextView.Box> boxes = new List<MarkdownViewerTextView.Box>();

    // Token: 0x04000EDB RID: 3803
    [TupleElementNames(new string[] { "Target", "Text", "Button" })]
    [Nullable(new byte[] { 1, 0, 1, 1, 1 })]
    private readonly List<ValueTuple<MarkdownViewerTextView.Box, string, MarkdownViewerTextView.CopyBlockButton>> copyButtons = new List<ValueTuple<MarkdownViewerTextView.Box, string, MarkdownViewerTextView.CopyBlockButton>>();

    // Token: 0x04000EDC RID: 3804
    [TupleElementNames(new string[] { "Para", "View", "Content", "Container" })]
    [Nullable(new byte[] { 1, 0, 1, 1, 1, 1 })]
    private readonly List<ValueTuple<MarkdownViewerTextView.Para, MarkdownViewerTableView, MarkdownViewerTableContent, InlineUIContainer>> tables = new List<ValueTuple<MarkdownViewerTextView.Para, MarkdownViewerTableView, MarkdownViewerTableContent, InlineUIContainer>>();

    // Token: 0x04000EDD RID: 3805
    [TupleElementNames(new string[] { "Para", "View", "Size" })]
    [Nullable(new byte[] { 1, 0, 1, 1 })]
    private readonly List<ValueTuple<MarkdownViewerTextView.Para, FrameworkElement, Size>> images = new List<ValueTuple<MarkdownViewerTextView.Para, FrameworkElement, Size>>();

    // Token: 0x04000EDE RID: 3806
    private readonly List<MarkdownViewerTextView.CopySegment> segments = new List<MarkdownViewerTextView.CopySegment>();

    // Token: 0x04000EDF RID: 3807
    private Size lastLayoutSize;

    // Token: 0x04000EE0 RID: 3808
    private double lastAvailableWidth = -1.0;

    // Token: 0x04000EE1 RID: 3809
    private readonly List<Run> inlineCodeRuns = new List<Run>();

    // Token: 0x02000902 RID: 2306
    [NullableContext(0)]
    private enum BoxKind
    {
      // Token: 0x04002614 RID: 9748
      Quote,
      // Token: 0x04002615 RID: 9749
      Code,
      // Token: 0x04002616 RID: 9750
      Rule
    }

    // Token: 0x02000903 RID: 2307
    [NullableContext(2)]
    [Nullable(0)]
    private sealed class Box
    {
      // Token: 0x04002617 RID: 9751
      public MarkdownViewerTextView.BoxKind Kind;

      // Token: 0x04002618 RID: 9752
      public double PadLeft;

      // Token: 0x04002619 RID: 9753
      public double PadRight;

      // Token: 0x0400261A RID: 9754
      public double PadTop;

      // Token: 0x0400261B RID: 9755
      public double PadBottom;

      // Token: 0x0400261C RID: 9756
      public double MarginTop;

      // Token: 0x0400261D RID: 9757
      public double MarginBottom;

      // Token: 0x0400261E RID: 9758
      public double RuleHeight = 1.0;

      // Token: 0x0400261F RID: 9759
      public MarkdownViewerTextView.Para First;

      // Token: 0x04002620 RID: 9760
      public MarkdownViewerTextView.Para Last;
    }

    // Token: 0x02000904 RID: 2308
    [Nullable(0)]
    [RequiredMember]
    private sealed class Para
    {
      // Token: 0x06003B54 RID: 15188 RVA: 0x006946A8 File Offset: 0x006946A8
      [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
      [CompilerFeatureRequired("RequiredMembers")]
      public Para()
      {
      }

      // Token: 0x04002621 RID: 9761
      [RequiredMember]
      public Paragraph P;

      // Token: 0x04002622 RID: 9762
      [RequiredMember]
      public List<MarkdownViewerTextView.Box> Boxes;

      // Token: 0x04002623 RID: 9763
      public double SpacingBefore;

      // Token: 0x04002624 RID: 9764
      public double SpacingAfter;

      // Token: 0x04002625 RID: 9765
      public double Indent;

      // Token: 0x04002626 RID: 9766
      public double ExtraRight;

      // Token: 0x04002627 RID: 9767
      public double LineHeight;
    }

    // Token: 0x02000905 RID: 2309
    [Nullable(0)]
    private sealed class CopySegment : IEquatable<MarkdownViewerTextView.CopySegment>
    {
      // Token: 0x06003B55 RID: 15189 RVA: 0x006946B0 File Offset: 0x006946B0
      public CopySegment(TextElement Element, string Text)
      {
        this.Element = Element;
        this.Text = Text;
        base..ctor();
      }

      // Token: 0x1700082C RID: 2092
      // (get) Token: 0x06003B56 RID: 15190 RVA: 0x006946C8 File Offset: 0x006946C8
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(MarkdownViewerTextView.CopySegment);
        }
      }

      // Token: 0x1700082D RID: 2093
      // (get) Token: 0x06003B57 RID: 15191 RVA: 0x006946D4 File Offset: 0x006946D4
      // (set) Token: 0x06003B58 RID: 15192 RVA: 0x006946DC File Offset: 0x006946DC
      public TextElement Element { get; set; }

      // Token: 0x1700082E RID: 2094
      // (get) Token: 0x06003B59 RID: 15193 RVA: 0x006946E8 File Offset: 0x006946E8
      // (set) Token: 0x06003B5A RID: 15194 RVA: 0x006946F0 File Offset: 0x006946F0
      public string Text { get; set; }

      // Token: 0x06003B5B RID: 15195 RVA: 0x006946FC File Offset: 0x006946FC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("CopySegment");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003B5C RID: 15196 RVA: 0x00694748 File Offset: 0x00694748
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Element = ");
        builder.Append(this.Element);
        builder.Append(", Text = ");
        builder.Append(this.Text);
        return true;
      }

      // Token: 0x06003B5D RID: 15197 RVA: 0x00694784 File Offset: 0x00694784
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(MarkdownViewerTextView.CopySegment left, MarkdownViewerTextView.CopySegment right)
      {
        return !(left == right);
      }

      // Token: 0x06003B5E RID: 15198 RVA: 0x00694790 File Offset: 0x00694790
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(MarkdownViewerTextView.CopySegment left, MarkdownViewerTextView.CopySegment right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003B5F RID: 15199 RVA: 0x006947A4 File Offset: 0x006947A4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<TextElement>.Default.GetHashCode(this.<Element>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField);
      }

      // Token: 0x06003B60 RID: 15200 RVA: 0x006947E4 File Offset: 0x006947E4
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as MarkdownViewerTextView.CopySegment);
      }

      // Token: 0x06003B61 RID: 15201 RVA: 0x006947F4 File Offset: 0x006947F4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(MarkdownViewerTextView.CopySegment other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<TextElement>.Default.Equals(this.<Element>k__BackingField, other.<Element>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField));
      }

      // Token: 0x06003B63 RID: 15203 RVA: 0x00694858 File Offset: 0x00694858
      [CompilerGenerated]
      private CopySegment(MarkdownViewerTextView.CopySegment original)
      {
        this.Element = original.<Element>k__BackingField;
        this.Text = original.<Text>k__BackingField;
      }

      // Token: 0x06003B64 RID: 15204 RVA: 0x00694878 File Offset: 0x00694878
      [CompilerGenerated]
      public void Deconstruct(out TextElement Element, out string Text)
      {
        Element = this.Element;
        Text = this.Text;
      }
    }

    // Token: 0x02000906 RID: 2310
    [NullableContext(0)]
    private static class QuotePlate
    {
      // Token: 0x1700082F RID: 2095
      // (get) Token: 0x06003B65 RID: 15205 RVA: 0x0069488C File Offset: 0x0069488C
      public static double TextLeadingInset
      {
        get
        {
          return 24.0;
        }
      }

      // Token: 0x0400262A RID: 9770
      public const double CornerRadius = 8.0;

      // Token: 0x0400262B RID: 9771
      public const double BarInset = 12.0;

      // Token: 0x0400262C RID: 9772
      public const double BarWidth = 2.0;
    }

    // Token: 0x02000907 RID: 2311
    [Nullable(0)]
    private sealed class Builder
    {
      // Token: 0x06003B66 RID: 15206 RVA: 0x00694898 File Offset: 0x00694898
      public Builder(MarkdownViewerTextView view, MarkdownViewerPreparedDocument document)
      {
      }

      // Token: 0x06003B67 RID: 15207 RVA: 0x006948B0 File Offset: 0x006948B0
      private double S(double size)
      {
        if (size > 1.0)
        {
          return Math.Round(size * this.<view>P.TextScale, MidpointRounding.ToEven);
        }
        return size;
      }

      // Token: 0x06003B68 RID: 15208 RVA: 0x006948D4 File Offset: 0x006948D4
      private double F(double metric)
      {
        return metric * this.<view>P.TextScale;
      }

      // Token: 0x06003B69 RID: 15209 RVA: 0x006948E4 File Offset: 0x006948E4
      public void Render(IReadOnlyList<MarkdownViewerBlock> blocks)
      {
        for (int i = 0; i < blocks.Count; i++)
        {
          if (i > 0)
          {
            this.AppendBlockSeparator();
          }
          this.AppendBlock(blocks[i], new List<MarkdownViewerTextView.Box>());
        }
      }

      // Token: 0x06003B6A RID: 15210 RVA: 0x00694920 File Offset: 0x00694920
      private void AppendBlockSeparator()
      {
        this.pendingSeparator = 1.0;
      }

      // Token: 0x06003B6B RID: 15211 RVA: 0x00694934 File Offset: 0x00694934
      private void AppendBlock(MarkdownViewerBlock block, List<MarkdownViewerTextView.Box> enclosing)
      {
        MarkdownViewerBlock.Heading heading = block as MarkdownViewerBlock.Heading;
        if (heading != null)
        {
          ValueTuple<double, double> valueTuple;
          switch (heading.Level)
          {
          case 1:
            valueTuple = new ValueTuple<double, double>(28.0, 18.0);
            break;
          case 2:
            valueTuple = new ValueTuple<double, double>(22.0, 24.0);
            break;
          case 3:
            valueTuple = new ValueTuple<double, double>(18.0, 18.0);
            break;
          default:
            valueTuple = new ValueTuple<double, double>(16.0, 14.0);
            break;
          }
          ValueTuple<double, double> valueTuple2 = valueTuple;
          double item = valueTuple2.Item1;
          double item2 = valueTuple2.Item2;
          MarkdownViewerTextView.Para para = this.NewParagraph(enclosing, this.F(item2), this.F(6.0), this.S(item), 0.0, true, 1.193);
          this.AppendInline(para, heading.InlineMarkdown, this.S(item), (heading.Level <= 2) ? MarkdownFontWeight.Bold : MarkdownFontWeight.Semibold, this.<view>P.colors.HeadingText);
          return;
        }
        MarkdownViewerBlock.Paragraph paragraph = block as MarkdownViewerBlock.Paragraph;
        if (paragraph == null)
        {
          MarkdownViewerBlock.CodeBlock codeBlock = block as MarkdownViewerBlock.CodeBlock;
          if (codeBlock == null)
          {
            MarkdownViewerBlock.Blockquote blockquote = block as MarkdownViewerBlock.Blockquote;
            if (blockquote == null)
            {
              MarkdownViewerBlock.List list = block as MarkdownViewerBlock.List;
              if (list != null)
              {
                this.AppendList(list.Items, enclosing);
                return;
              }
              MarkdownViewerBlock.Table table = block as MarkdownViewerBlock.Table;
              if (table != null)
              {
                this.AppendTable(table.HeaderCells, table.Rows, enclosing);
                return;
              }
              if (block is MarkdownViewerBlock.HorizontalRule)
              {
                this.AppendHorizontalRule(enclosing);
                return;
              }
              MarkdownViewerBlock.Image image = block as MarkdownViewerBlock.Image;
              if (image == null)
              {
                return;
              }
              this.AppendImage(image.AlternateText, image.Source, enclosing);
            }
            else
            {
              bool flag = MarkdownViewerParser.IsCallout(blockquote.ChildBlocks);
              MarkdownViewerTextView.Box box = new MarkdownViewerTextView.Box
              {
                Kind = MarkdownViewerTextView.BoxKind.Quote,
                PadLeft = MarkdownViewerTextView.QuotePlate.TextLeadingInset,
                PadRight = (flag ? 12.0 : 42.0),
                PadTop = 10.0,
                PadBottom = 10.0,
                MarginTop = 4.0,
                MarginBottom = 4.0
              };
              this.<view>P.boxes.Add(box);
              int count = this.<view>P.segments.Count;
              int count2 = this.<view>P.paras.Count;
              List<MarkdownViewerTextView.Box> list2 = new List<MarkdownViewerTextView.Box>(enclosing) { box };
              for (int i = 0; i < blockquote.ChildBlocks.Count; i++)
              {
                if (i > 0)
                {
                  this.AppendBlockSeparator();
                }
                this.AppendBlock(blockquote.ChildBlocks[i], list2);
              }
              if (!flag && this.<view>P.paras.Count > count2)
              {
                string text = string.Concat(from s in this.<view>P.segments.Skip<MarkdownViewerTextView.CopySegment>(count)
                  select s.Text).Trim();
                MarkdownViewerTextView.Para para2 = this.<view>P.paras[count2];
                if (text.Length > 0)
                {
                  MarkdownViewerTextView markdownViewerTextView = this.<view>P;
                  List<MarkdownViewerTextView.Box> boxes = para2.Boxes;
                  markdownViewerTextView.AddCopyButton(boxes[boxes.Count - 1], text);
                  return;
                }
              }
            }
          }
          else
          {
            MarkdownViewerTextView.Box box2 = this.AppendCodeBlock(codeBlock.Code, enclosing);
            if (codeBlock.Code.Length > 0)
            {
              this.<view>P.AddCopyButton(box2, codeBlock.Code);
              return;
            }
          }
          return;
        }
        MarkdownViewerTextView.Para para3 = this.NewParagraph(enclosing, this.F(7.0), this.F(7.0), this.S(16.0), this.F(5.5), true, 1.193);
        this.AppendInline(para3, paragraph.InlineMarkdown, this.S(16.0), MarkdownFontWeight.Regular, this.<view>P.colors.BodyText);
      }

      // Token: 0x06003B6C RID: 15212 RVA: 0x00694D48 File Offset: 0x00694D48
      private MarkdownViewerTextView.Para NewParagraph(List<MarkdownViewerTextView.Box> enclosing, double spacingBefore, double spacingAfter, double fontSize, double lineSpacing, bool fixedLineHeight = true, double lineHeightFactor = 1.193)
      {
        Paragraph paragraph = new Paragraph
        {
          FontSize = fontSize
        };
        MarkdownViewerTextView.Para para = new MarkdownViewerTextView.Para
        {
          P = paragraph,
          Boxes = new List<MarkdownViewerTextView.Box>(enclosing),
          SpacingBefore = spacingBefore + this.pendingSeparator,
          SpacingAfter = spacingAfter
        };
        if (this.<view>P.paras.Count > 0)
        {
          List<MarkdownViewerTextView.CopySegment> segments = this.<view>P.segments;
          List<MarkdownViewerTextView.Para> paras = this.<view>P.paras;
          segments.Add(new MarkdownViewerTextView.CopySegment(paras[paras.Count - 1].P, (this.pendingSeparator > 0.0) ? "\n\n" : "\n"));
        }
        this.pendingSeparator = 0.0;
        if (fixedLineHeight)
        {
          para.LineHeight = Math.Round(fontSize * lineHeightFactor + lineSpacing, 2);
          paragraph.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
          paragraph.LineHeight = para.LineHeight;
        }
        foreach (MarkdownViewerTextView.Box box in enclosing)
        {
          MarkdownViewerTextView.Box box3;
          MarkdownViewerTextView.Box box2 = (box3 = box);
          if (box3.First == null)
          {
            box3.First = para;
          }
          box2.Last = para;
        }
        this.<view>P.paras.Add(para);
        this.<view>P.text.Blocks.Add(paragraph);
        return para;
      }

      // Token: 0x06003B6D RID: 15213 RVA: 0x00694EA8 File Offset: 0x00694EA8
      private void AddRun(MarkdownViewerTextView.Para para, Run run, [Nullable(2)] InlineCollection target = null)
      {
        (target ?? para.P.Inlines).Add(run);
        this.<view>P.segments.Add(new MarkdownViewerTextView.CopySegment(run, run.Text));
      }

      // Token: 0x06003B6E RID: 15214 RVA: 0x00694EDC File Offset: 0x00694EDC
      private void AppendInline(MarkdownViewerTextView.Para para, string inlineMarkdown, double fontSize, MarkdownFontWeight weight, Color color)
      {
        foreach (MarkdownInlineRun markdownInlineRun in MarkdownInlineParser.Parse(MarkdownViewerParser.RewriteInlineImagesAsLinks(inlineMarkdown)))
        {
          Run run = new Run
          {
            Text = markdownInlineRun.Text,
            FontSize = fontSize,
            Foreground = Ui.Brush(color),
            FontFamily = MarkdownViewerFonts.System(fontSize)
          };
          MarkdownFontWeight markdownFontWeight = weight;
          if (markdownInlineRun.IsCode)
          {
            run.FontFamily = MarkdownViewerFonts.Mono;
            run.FontSize = fontSize - 2.0;
            if (markdownInlineRun.IsStrong)
            {
              markdownFontWeight = MarkdownFontWeight.Semibold;
            }
            this.<view>P.inlineCodeRuns.Add(run);
          }
          else
          {
            if (markdownInlineRun.IsStrong)
            {
              markdownFontWeight = MarkdownFontWeight.Semibold;
            }
            if (markdownInlineRun.IsEmphasized)
            {
              run.FontStyle = FontStyle.Italic;
            }
          }
          run.FontWeight = MarkdownViewerFonts.Weight(markdownFontWeight);
          if (markdownInlineRun.IsStrikethrough)
          {
            run.TextDecorations = TextDecorations.Strikethrough;
          }
          string link = markdownInlineRun.Link;
          if (link != null)
          {
            run.Foreground = Ui.Brush(this.<view>P.colors.LinkText);
            Hyperlink hyperlink = new Hyperlink
            {
              UnderlineStyle = UnderlineStyle.Single,
              Foreground = Ui.Brush(this.<view>P.colors.LinkText)
            };
            Uri uri = this.ResolveLink(link);
            if (uri != null)
            {
              string scheme = uri.Scheme;
              bool flag = scheme == "http" || scheme == "https" || scheme == "mailto";
              if (flag)
              {
                hyperlink.NavigateUri = uri;
              }
              hyperlink.Click += delegate(Hyperlink s, HyperlinkClickEventArgs _)
              {
                MarkdownViewerLinks.Opened(s.XamlRoot, uri);
              };
            }
            para.P.Inlines.Add(hyperlink);
            this.AddRun(para, run, hyperlink.Inlines);
          }
          else
          {
            this.AddRun(para, run, null);
          }
        }
      }

      // Token: 0x06003B6F RID: 15215 RVA: 0x006950E8 File Offset: 0x006950E8
      [return: Nullable(2)]
      private Uri ResolveLink(string link)
      {
        Uri uri;
        if (Uri.TryCreate(link, UriKind.Absolute, out uri) && (!uri.IsFile || link.StartsWith("file:", StringComparison.OrdinalIgnoreCase)))
        {
          return uri;
        }
        string baseDirectory = this.<document>P.BaseDirectory;
        if (baseDirectory != null)
        {
          try
          {
            return new Uri(Path.GetFullPath(Path.Combine(baseDirectory, Uri.UnescapeDataString(link.Split('#', StringSplitOptions.None)[0]))));
          }
          catch
          {
            return null;
          }
        }
        return null;
      }

      // Token: 0x06003B70 RID: 15216 RVA: 0x00695164 File Offset: 0x00695164
      private MarkdownViewerTextView.Box AppendCodeBlock(string code, List<MarkdownViewerTextView.Box> enclosing)
      {
        MarkdownViewerTextView.Box box = new MarkdownViewerTextView.Box
        {
          Kind = MarkdownViewerTextView.BoxKind.Code,
          PadLeft = 16.0,
          PadRight = 16.0,
          PadTop = 12.0,
          PadBottom = 12.0
        };
        this.<view>P.boxes.Add(box);
        List<MarkdownViewerTextView.Box> list = new List<MarkdownViewerTextView.Box>(enclosing) { box };
        string[] array = code.Split('\n', StringSplitOptions.None);
        double num = this.S(13.0);
        for (int i = 0; i < array.Length; i++)
        {
          bool flag = i == 0;
          bool flag2 = i == array.Length - 1;
          MarkdownViewerTextView.Para para = this.NewParagraph(list, flag ? this.F(8.0) : 0.0, flag2 ? this.F(8.0) : 0.0, num, this.F(3.5), true, 1.19);
          if (flag)
          {
            para.ExtraRight = 30.0;
          }
          para.P.FontFamily = MarkdownViewerFonts.Mono;
          this.AddRun(para, new Run
          {
            Text = array[i],
            FontFamily = MarkdownViewerFonts.Mono,
            FontSize = num,
            Foreground = Ui.Brush(this.<view>P.colors.BodyText)
          }, null);
        }
        return box;
      }

      // Token: 0x06003B71 RID: 15217 RVA: 0x006952E8 File Offset: 0x006952E8
      private void AppendList(IReadOnlyList<MarkdownViewerListItem> items, List<MarkdownViewerTextView.Box> enclosing)
      {
        Color bodyText = this.<view>P.colors.BodyText;
        double num = this.S(16.0);
        for (int i = 0; i < items.Count; i++)
        {
          MarkdownViewerListItem markdownViewerListItem = items[i];
          double num2 = this.F(24.0);
          double num3 = this.F((double)(markdownViewerListItem.IndentLevel * 22));
          double num4 = ((i == items.Count - 1) ? this.F(12.0) : this.F(5.0));
          MarkdownViewerTextView.Para para = this.NewParagraph(enclosing, (i == 0) ? this.F(7.0) : 0.0, num4, num, this.F(5.5), true, 1.193);
          para.Indent = num3 + num2;
          para.P.TextIndent = -num2;
          ValueTuple<string, double, Color, bool> valueTuple = this.ListItemMarker(markdownViewerListItem);
          string item = valueTuple.Item1;
          double item2 = valueTuple.Item2;
          Color item3 = valueTuple.Item3;
          bool item4 = valueTuple.Item4;
          Run run = new Run
          {
            Text = item,
            FontSize = this.S(item2),
            Foreground = Ui.Brush(item3),
            FontFamily = ((markdownViewerListItem.CheckboxState == null) ? MarkdownViewerFonts.System(this.S(item2)) : MarkdownViewerFonts.Symbol)
          };
          if (item4)
          {
            Typography.SetNumeralAlignment(run, FontNumeralAlignment.Tabular);
          }
          this.AddRun(para, run, null);
          double num5 = MarkdownViewerTextView.Builder.MeasureWidth(item, this.S(item2), item4, run.FontFamily);
          InlineUIContainer inlineUIContainer = new InlineUIContainer
          {
            Child = new Border
            {
              Width = Math.Max(this.F(4.0), num2 - num5),
              Height = 1.0
            }
          };
          para.P.Inlines.Add(inlineUIContainer);
          this.<view>P.segments.Add(new MarkdownViewerTextView.CopySegment(inlineUIContainer, "\t"));
          this.AppendInline(para, markdownViewerListItem.InlineMarkdown, num, MarkdownFontWeight.Regular, bodyText);
        }
      }

      // Token: 0x06003B72 RID: 15218 RVA: 0x00695510 File Offset: 0x00695510
      [return: TupleElementNames(new string[] { "Text", "Size", "Color", "Mono" })]
      [return: Nullable(new byte[] { 0, 1 })]
      private ValueTuple<string, double, Color, bool> ListItemMarker(MarkdownViewerListItem item)
      {
        bool? checkboxState = item.CheckboxState;
        if (checkboxState != null)
        {
          bool valueOrDefault = checkboxState.GetValueOrDefault();
          return new ValueTuple<string, double, Color, bool>(valueOrDefault ? "☑" : "☐", 14.0, valueOrDefault ? this.<view>P.colors.BodyText : this.<view>P.colors.SecondaryText, false);
        }
        string ordinalLabel = item.OrdinalLabel;
        if (ordinalLabel != null)
        {
          return new ValueTuple<string, double, Color, bool>(ordinalLabel, 16.0, this.<view>P.colors.SecondaryText, true);
        }
        return new ValueTuple<string, double, Color, bool>((item.IndentLevel == 0) ? "•" : "◦", 16.0, this.<view>P.colors.SecondaryText, false);
      }

      // Token: 0x06003B73 RID: 15219 RVA: 0x006955D8 File Offset: 0x006955D8
      private static double MeasureWidth(string text, double fontSize, bool tabular, FontFamily family)
      {
        if (MarkdownViewerTextView.Builder.measuring == null)
        {
          MarkdownViewerTextView.Builder.measuring = new TextBlock
          {
            IsTextScaleFactorEnabled = false
          };
        }
        MarkdownViewerTextView.Builder.measuring.Text = text;
        MarkdownViewerTextView.Builder.measuring.FontSize = fontSize;
        MarkdownViewerTextView.Builder.measuring.FontFamily = family;
        Typography.SetNumeralAlignment(MarkdownViewerTextView.Builder.measuring, tabular ? FontNumeralAlignment.Tabular : FontNumeralAlignment.Normal);
        MarkdownViewerTextView.Builder.measuring.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        return MarkdownViewerTextView.Builder.measuring.DesiredSize.Width;
      }

      // Token: 0x06003B74 RID: 15220 RVA: 0x00695664 File Offset: 0x00695664
      private void AppendTable(IReadOnlyList<string> headerCells, IReadOnlyList<IReadOnlyList<string>> rows, List<MarkdownViewerTextView.Box> enclosing)
      {
        int count = headerCells.Count;
        int num;
        if (rows.Count != 0)
        {
          num = rows.Max<IReadOnlyList<string>>((IReadOnlyList<string> r) => r.Count);
        }
        else
        {
          num = 0;
        }
        int num2 = Math.Max(count, num);
        if (num2 == 0)
        {
          return;
        }
        List<string> list = headerCells.Concat<string>(Enumerable.Repeat<string>("", num2 - headerCells.Count)).ToList<string>();
        string text = string.Join("\n", from row in new IReadOnlyList<string>[] { list }.Concat<IReadOnlyList<string>>(rows)
          select string.Join("\t", row.Select<string, string>((string cell) => MarkdownInlineParser.PlainText(cell))));
        MarkdownViewerTableContent markdownViewerTableContent = new MarkdownViewerTableContent(list, rows, text);
        MarkdownViewerTextView.Para para = this.NewParagraph(enclosing, this.F(8.0), this.F(8.0), this.S(13.5), 0.0, false, 1.193);
        MarkdownViewerTableView markdownViewerTableView = new MarkdownViewerTableView(markdownViewerTableContent, this.<view>P.look);
        InlineUIContainer inlineUIContainer = new InlineUIContainer
        {
          Child = markdownViewerTableView
        };
        para.P.Inlines.Add(inlineUIContainer);
        this.<view>P.segments.Add(new MarkdownViewerTextView.CopySegment(inlineUIContainer, text));
        this.<view>P.tables.Add(new ValueTuple<MarkdownViewerTextView.Para, MarkdownViewerTableView, MarkdownViewerTableContent, InlineUIContainer>(para, markdownViewerTableView, markdownViewerTableContent, inlineUIContainer));
      }

      // Token: 0x06003B75 RID: 15221 RVA: 0x006957CC File Offset: 0x006957CC
      private void AppendHorizontalRule(List<MarkdownViewerTextView.Box> enclosing)
      {
        MarkdownViewerTextView.Box box = new MarkdownViewerTextView.Box
        {
          Kind = MarkdownViewerTextView.BoxKind.Rule,
          RuleHeight = 1.0
        };
        this.<view>P.boxes.Add(box);
        MarkdownViewerTextView.Para para = this.NewParagraph(new List<MarkdownViewerTextView.Box>(enclosing) { box }, this.F(18.0), this.F(18.0), 1.0, 0.0, true, 1.193);
        para.LineHeight = 1.0;
        para.P.LineHeight = 1.0;
        this.AddRun(para, new Run
        {
          Text = "\u00a0",
          FontSize = 1.0
        }, null);
      }

      // Token: 0x06003B76 RID: 15222 RVA: 0x006958A0 File Offset: 0x006958A0
      private void AppendImage(string alternateText, string source, List<MarkdownViewerTextView.Box> enclosing)
      {
        bool flag = MarkdownViewerPreparedDocument.IsRemote(source);
        MarkdownViewerImage markdownViewerImage;
        this.<document>P.Images.TryGetValue(source, out markdownViewerImage);
        if (markdownViewerImage == null)
        {
          string text = (flag ? ((alternateText.Length == 0) ? "Loading image…" : alternateText) : ((alternateText.Length == 0) ? source : alternateText));
          double num = this.S(12.5);
          MarkdownViewerTextView.Para para = this.NewParagraph(enclosing, this.F(10.0), this.F(10.0), num, 0.0, true, 1.193);
          this.AddRun(para, new Run
          {
            Text = "\ud83d\uddbc " + text,
            FontSize = num,
            Foreground = Ui.Brush(this.<view>P.colors.SecondaryText),
            FontFamily = MarkdownViewerFonts.System(num)
          }, null);
          return;
        }
        double num2 = Math.Min(1.0, Math.Min(660.0 / Math.Max(markdownViewerImage.Width, 1.0), 360.0 / Math.Max(markdownViewerImage.Height, 1.0)));
        Size size = new Size(markdownViewerImage.Width * num2, markdownViewerImage.Height * num2);
        BitmapImage bitmapImage = new BitmapImage();
        string path = markdownViewerImage.Path;
        if (path != null)
        {
          bitmapImage.UriSource = new Uri(path);
        }
        else
        {
          byte[] bytes = markdownViewerImage.Bytes;
          if (bytes != null)
          {
            MarkdownViewerTextView.Builder.SetBytesAsync(bitmapImage, bytes);
          }
        }
        Image image = new Image
        {
          Source = bitmapImage,
          Stretch = Stretch.Fill,
          Width = size.Width,
          Height = size.Height
        };
        MarkdownViewerTextView.Para para2 = this.NewParagraph(enclosing, this.F(10.0), this.F(10.0), this.S(16.0), 0.0, false, 1.193);
        InlineUIContainer inlineUIContainer = new InlineUIContainer
        {
          Child = image
        };
        para2.P.Inlines.Add(inlineUIContainer);
        this.<view>P.segments.Add(new MarkdownViewerTextView.CopySegment(inlineUIContainer, ""));
        this.<view>P.images.Add(new ValueTuple<MarkdownViewerTextView.Para, FrameworkElement, Size>(para2, image, size));
      }

      // Token: 0x06003B77 RID: 15223 RVA: 0x00695B00 File Offset: 0x00695B00
      private static async Task SetBytesAsync(BitmapImage bitmap, byte[] bytes)
      {
        try
        {
          using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
          {
            await stream.WriteAsync(bytes.AsBuffer());
            stream.Seek(0UL);
            await bitmap.SetSourceAsync(stream);
          }
          InMemoryRandomAccessStream stream = null;
        }
        catch (Exception ex)
        {
          Log.Info("markdown", "image not shown: " + ex.Message);
        }
      }

      // Token: 0x0400262D RID: 9773
      [CompilerGenerated]
      private MarkdownViewerTextView <view>P = view;

      // Token: 0x0400262E RID: 9774
      [CompilerGenerated]
      private MarkdownViewerPreparedDocument <document>P = document;

      // Token: 0x0400262F RID: 9775
      private double pendingSeparator;

      // Token: 0x04002630 RID: 9776
      [Nullable(2)]
      private static TextBlock measuring;
    }

    // Token: 0x02000908 RID: 2312
    [Nullable(0)]
    private sealed class CopyBlockButton : Pressable
    {
      // Token: 0x06003B78 RID: 15224 RVA: 0x00695B4C File Offset: 0x00695B4C
      public CopyBlockButton(string copyText, MessagesPalette look)
        : base(true)
      {
        MarkdownViewerTextView.CopyBlockButton <>4__this = this;
        this.look = look;
        base.Width = 22.0;
        base.Height = 22.0;
        base.CornerRadius = new CornerRadius(6.0);
        this.glyph = Icons.Glyph("\ue8c8", 11.0, look.InkAt(0.5));
        this.glyph.FontWeight = FontWeights.Medium;
        base.Children.Add(this.glyph);
        this.Tip("Copy");
        AutomationProperties.SetName(this, "Copy");
        base.HoverChanged += delegate(bool _)
        {
          <>4__this.Refresh();
        };
        EventHandler<object> <>9__2;
        base.Click += delegate
        {
          DataPackage dataPackage = new DataPackage();
          dataPackage.SetText(copyText);
          Clipboard.SetContent(dataPackage);
          string text = "home_space_conversation_action";
          string text2 = null;
          string text3 = null;
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["action"] = "copy_block";
          ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
          <>4__this.didCopy = true;
          <>4__this.Refresh();
          DispatcherTimer dispatcherTimer = <>4__this.reset;
          if (dispatcherTimer != null)
          {
            dispatcherTimer.Stop();
          }
          <>4__this.reset = new DispatcherTimer
          {
            Interval = TimeSpan.FromSeconds(1.4)
          };
          DispatcherTimer dispatcherTimer2 = <>4__this.reset;
          EventHandler<object> eventHandler;
          if ((eventHandler = <>9__2) == null)
          {
            eventHandler = (<>9__2 = delegate([Nullable(2)] object _, object _)
            {
              DispatcherTimer dispatcherTimer3 = <>4__this.reset;
              if (dispatcherTimer3 != null)
              {
                dispatcherTimer3.Stop();
              }
              <>4__this.didCopy = false;
              <>4__this.Refresh();
            });
          }
          dispatcherTimer2.Tick += eventHandler;
          <>4__this.reset.Start();
        };
      }

      // Token: 0x06003B79 RID: 15225 RVA: 0x00695C34 File Offset: 0x00695C34
      private void Refresh()
      {
        this.glyph.Text = (this.didCopy ? "\ue73e" : "\ue8c8");
        this.glyph.Foreground = Ui.Brush(this.look.InkAt((this.didCopy || base.IsHovered) ? (this.look.IsDark ? 0.95 : 0.8) : 0.5));
        base.Background = Ui.Brush(this.look.InkAt(base.IsHovered ? (this.look.IsDark ? 0.1 : 0.07) : 0.0));
      }

      // Token: 0x04002631 RID: 9777
      public const double Size = 22.0;

      // Token: 0x04002632 RID: 9778
      private readonly TextBlock glyph;

      // Token: 0x04002633 RID: 9779
      private readonly MessagesPalette look;

      // Token: 0x04002634 RID: 9780
      private bool didCopy;

      // Token: 0x04002635 RID: 9781
      [Nullable(2)]
      private DispatcherTimer reset;
    }
  }
}
