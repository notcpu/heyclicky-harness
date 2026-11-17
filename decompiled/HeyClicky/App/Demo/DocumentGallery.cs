using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HeyClicky.App.Documents;
using HeyClicky.App.Home;
using HeyClicky.App.Ink;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using HeyClicky.Core.Documents.Spreadsheet;
using HeyClicky.Core.Ink;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Demo
{
  // Token: 0x02000352 RID: 850
  [NullableContext(1)]
  [Nullable(0)]
  public static class DocumentGallery
  {
    // Token: 0x06002472 RID: 9330 RVA: 0x00632BB4 File Offset: 0x00632BB4
    public static List<DocumentGallery.Scene> Scenes()
    {
      List<DocumentGallery.Scene> list = new List<DocumentGallery.Scene>();
      DocumentGallery.AddSpreadsheetScenes(list);
      DocumentGallery.AddMarkdownScenes(list);
      DocumentGallery.AddInkScenes(list);
      return list;
    }

    // Token: 0x06002473 RID: 9331 RVA: 0x00632BD0 File Offset: 0x00632BD0
    private static void AddSpreadsheetScenes(List<DocumentGallery.Scene> list)
    {
      Color sidebarFill = HomeTheme.SidebarFill;
      list.Add(new DocumentGallery.Scene("sheet-csv", 900.0, 560.0, delegate
      {
        SpreadsheetDocumentView spreadsheetDocumentView = (SpreadsheetDocumentView)DocumentViewers.CreateViewer(SpreadsheetSampleFiles.WriteCsv(DocumentGallery.<AddSpreadsheetScenes>g__Sample|2_0("orders.csv")), null);
        spreadsheetDocumentView.PreviewReady += delegate(SpreadsheetPreview preview)
        {
          preview.GridView.Select(new SpreadsheetCellRange(1, 1, 3, 3));
        };
        return spreadsheetDocumentView;
      }, new Color?(sidebarFill), 1.6));
      list.Add(new DocumentGallery.Scene("sheet-semicolon", 700.0, 360.0, () => DocumentGallery.Preview(SpreadsheetSampleFiles.WriteSemicolonCsv(DocumentGallery.<AddSpreadsheetScenes>g__Sample|2_0("städte.csv")), 0, new SpreadsheetCellRange(1, 0, 1, 0)), new Color?(sidebarFill), 1.6));
      list.Add(new DocumentGallery.Scene("sheet-tsv", 700.0, 360.0, () => DocumentGallery.Preview(SpreadsheetSampleFiles.WriteTsv(DocumentGallery.<AddSpreadsheetScenes>g__Sample|2_0("scores.tsv")), 0, new SpreadsheetCellRange(1, 2, 3, 2)), new Color?(sidebarFill), 1.6));
      string xlsx = DocumentGallery.<AddSpreadsheetScenes>g__Sample|2_0("quarterly.xlsx");
      list.Add(new DocumentGallery.Scene("sheet-xlsx-revenue", 1000.0, 600.0, () => DocumentGallery.Preview(SpreadsheetSampleFiles.WriteWorkbook(xlsx), 0, new SpreadsheetCellRange(1, 1, 5, 2)), new Color?(sidebarFill), 1.6));
      list.Add(new DocumentGallery.Scene("sheet-xlsx-total", 1000.0, 600.0, () => DocumentGallery.Preview(SpreadsheetSampleFiles.WriteWorkbook(xlsx), 0, new SpreadsheetCellRange(5, 1, 5, 1)), new Color?(sidebarFill), 1.6));
      list.Add(new DocumentGallery.Scene("sheet-xlsx-summary", 800.0, 420.0, () => DocumentGallery.Preview(SpreadsheetSampleFiles.WriteWorkbook(xlsx), 1, new SpreadsheetCellRange(4, 0, 4, 0)), new Color?(sidebarFill), 1.6));
      list.Add(new DocumentGallery.Scene("sheet-xlsx-merged", 800.0, 420.0, () => DocumentGallery.Preview(SpreadsheetSampleFiles.WriteWorkbook(xlsx), 1, new SpreadsheetCellRange(0, 1, 0, 1)), new Color?(sidebarFill), 1.6));
      list.Add(new DocumentGallery.Scene("sheet-xlsx-wide", 1000.0, 520.0, () => DocumentGallery.Preview(SpreadsheetSampleFiles.WriteWorkbook(xlsx), 2, new SpreadsheetCellRange(2, 0, 8, 2)), new Color?(sidebarFill), 1.6));
    }

    // Token: 0x06002474 RID: 9332 RVA: 0x00632E1C File Offset: 0x00632E1C
    private static void AddMarkdownScenes(List<DocumentGallery.Scene> list)
    {
      string dir = Path.Combine(DocumentGallery.SampleDirectory, "markdown");
      Directory.CreateDirectory(dir);
      DocumentGallery.WriteSamplePicture(Path.Combine(dir, "chart.png"));
      string report = Path.Combine(dir, "report.md");
      File.WriteAllText(report, "# Q3 revenue report\nRevenue grew **12%** quarter over quarter, led by *annual* plans and a ~~flat~~ steady churn rate. See the\n[dashboard](https://example.com/dashboard) or run `npm run report` to rebuild it.\n\n## Highlights\n- Annual plans: **$412k** booked\n- Churn held at `2.1%`\n  - Enterprise churn: 0.4%\n  - Self-serve churn: 3.0%\n- [x] Pricing page shipped\n- [ ] Billing migration\n\n![Revenue by month](chart.png)\n\n### Next steps\n1. Review the cancellation requests with the team.\n2. Update the payment details for North Star Studio.\n10. Send the board memo.\n\n---\n\n> **Tip** · Ask Clicky to refresh this report every Monday.\n\n![A picture from the web](https://upload.wikimedia.org/wikipedia/commons/4/47/PNG_transparency_demonstration_1.png)");
      string blocks = Path.Combine(dir, "blocks.md");
      File.WriteAllText(blocks, "# Heading one\n## Heading two\n### Heading three\n#### Heading four\n##### Heading five\n###### Heading six\n\nSetext heading\n==============\n\nA paragraph with **bold**, *italic*, ***both***, `inline code`, ~~struck~~ and a [link](https://heyclicky.com).\nLines of one paragraph join into one.\n\n> A quote to lift out: a draft reply.\n>\n> - with a list\n> - and a second item\n>\n> ```\n> let x = 1\n> ```\n\n> **Note** · A callout has no copy button.\n\n```swift\nfunc greet(name: String) -> String {\n    return \"Hello, \\(name)! This line is long enough that it has to wrap inside the code block's plate on a narrow page.\"\n}\n```\n\n1. First\n2. Second\n    - nested bullet\n        - deeper\n3. Third\n\n***\n\nDone.");
      string tables = Path.Combine(dir, "tables.md");
      File.WriteAllText(tables, "# Tables\n\nBefore the table.\n\n| Customer | Plan | Revenue | Next step |\n| --- | --- | --- | --- |\n| North Star Studio | Professional annual | $12,450.00 | Contact the billing owner to update their payment details. |\n| Other studio | Enterprise | $145,000.00 | Review the cancellation request with the team. |\n\nAfter the table.\n\n| Option | Price | Verdict |\n|:--|--:|:-:|\n| ✓ Breville Bambino | $299 | **best** |\n| Gaggia Classic | $449 | `good` |\n\n| a | b | c | d | e | f |\n|---|---|---|---|---|---|\n| one | two | three | four | five | six |");
      list.Add(new DocumentGallery.Scene("md-report", 520.0, 900.0, () => DocumentGallery.OnSidebar(new MarkdownViewerContent(report)), new Color?(MarkdownViewerPalette.Dark.PageBackground), 3.5));
      list.Add(new DocumentGallery.Scene("md-blocks", 520.0, 1100.0, () => DocumentGallery.OnSidebar(new MarkdownViewerContent(blocks)), new Color?(MarkdownViewerPalette.Dark.PageBackground), 3.5));
      list.Add(new DocumentGallery.Scene("md-tables", 520.0, 760.0, delegate
      {
        MarkdownViewerContent content = new MarkdownViewerContent(tables);
        Once.After(2.5, delegate
        {
          content.TextView.SelectAllText();
          string text = content.TextView.SelectedPlainText();
          string text2 = "markdown";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(55, 4);
          defaultInterpolatedStringHandler.AppendLiteral("gallery copy check: tables=");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(text.Contains("$145,000.00") && text.Contains("Customer\tPlan"));
          defaultInterpolatedStringHandler.AppendLiteral(" ");
          defaultInterpolatedStringHandler.AppendLiteral("before=");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(text.Contains("Before the table."));
          defaultInterpolatedStringHandler.AppendLiteral(" after=");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(text.Contains("After the table."));
          defaultInterpolatedStringHandler.AppendLiteral(" placeholder=");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(text.Contains('￼'));
          Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
        }, DispatcherQueue.GetForCurrentThread());
        return DocumentGallery.OnSidebar(content);
      }, new Color?(MarkdownViewerPalette.Dark.PageBackground), 3.5));
      string remote = Path.Combine(dir, "remote.md");
      File.WriteAllText(remote, "# A picture from the web\n\n![](https://upload.wikimedia.org/wikipedia/commons/4/47/PNG_transparency_demonstration_1.png)\n\nAfter the picture.\n");
      list.Add(new DocumentGallery.Scene("md-remote", 520.0, 700.0, () => DocumentGallery.OnSidebar(new MarkdownViewerContent(remote)), new Color?(MarkdownViewerPalette.Dark.PageBackground), 6.0));
      list.Add(new DocumentGallery.Scene("md-wide", 980.0, 900.0, () => DocumentGallery.OnSidebar(new MarkdownViewerContent(report)), new Color?(MarkdownViewerPalette.Dark.PageBackground), 3.5));
      list.Add(new DocumentGallery.Scene("md-inline", 520.0, 1000.0, () => DocumentGallery.OnSidebar(new ScrollViewer
      {
        Content = new MarkdownViewerInlineDocument("# Heading one\n## Heading two\n### Heading three\n#### Heading four\n##### Heading five\n###### Heading six\n\nSetext heading\n==============\n\nA paragraph with **bold**, *italic*, ***both***, `inline code`, ~~struck~~ and a [link](https://heyclicky.com).\nLines of one paragraph join into one.\n\n> A quote to lift out: a draft reply.\n>\n> - with a list\n> - and a second item\n>\n> ```\n> let x = 1\n> ```\n\n> **Note** · A callout has no copy button.\n\n```swift\nfunc greet(name: String) -> String {\n    return \"Hello, \\(name)! This line is long enough that it has to wrap inside the code block's plate on a narrow page.\"\n}\n```\n\n1. First\n2. Second\n    - nested bullet\n        - deeper\n3. Third\n\n***\n\nDone.", dir)
        {
          Margin = new Thickness(16.0)
        },
        VerticalScrollBarVisibility = ScrollBarVisibility.Hidden
      }), new Color?(HomeTheme.SidebarFill), 3.5));
      list.Add(new DocumentGallery.Scene("md-loading-error", 520.0, 300.0, () => DocumentGallery.OnSidebar(new MarkdownViewerContent(Path.Combine(dir, "missing.md"))), new Color?(HomeTheme.SidebarFill), 2.0));
    }

    // Token: 0x06002475 RID: 9333 RVA: 0x006330E0 File Offset: 0x006330E0
    private static void AddInkScenes(List<DocumentGallery.Scene> list)
    {
      DocumentGallery.AddInkPaperScenes(list);
      DocumentGallery.AddInkViewScenes(list);
    }

    // Token: 0x1700069C RID: 1692
    // (get) Token: 0x06002476 RID: 9334 RVA: 0x006330F0 File Offset: 0x006330F0
    public static string SampleDirectory
    {
      get
      {
        string text = Path.Combine(Path.GetTempPath(), "HeyClicky-document-gallery");
        Directory.CreateDirectory(text);
        return text;
      }
    }

    // Token: 0x06002477 RID: 9335 RVA: 0x00633108 File Offset: 0x00633108
    internal static void AddSteps(AppHost host, [Nullable(new byte[] { 1, 1, 1, 1, 2 })] Action<string, Action, double, Func<UIElement>> add)
    {
      PanelWindow window = null;
      Func<UIElement> <>9__3;
      foreach (DocumentGallery.Scene scene in DocumentGallery.Scenes())
      {
        DocumentGallery.Scene s = scene;
        string text = "documents-" + s.Name;
        Action action = delegate
        {
          host.HideAuxWindows();
          PanelWindow window4 = window;
          if (window4 != null)
          {
            window4.HideWindow();
          }
          window = new PanelWindow("Documents " + s.Name, !host.Options.AllowCapture, true, 14.0, s.Fill, 0.1);
          try
          {
            window.Panel.Children.Add(s.Build());
          }
          catch (Exception ex)
          {
            Log.Error("demo", "document scene " + s.Name + " failed", ex);
          }
          window.SetPanelSize(s.Width, s.Height);
          window.ShowCentered();
        };
        double seconds = s.Seconds;
        Func<UIElement> func;
        if ((func = <>9__3) == null)
        {
          func = (<>9__3 = delegate
          {
            PanelWindow window2 = window;
            if (window2 == null)
            {
              return null;
            }
            return window2.Panel;
          });
        }
        add(text, action, seconds, func);
      }
      add("documents-end", delegate
      {
        PanelWindow window3 = window;
        if (window3 == null)
        {
          return;
        }
        window3.HideWindow();
      }, 0.3, () => null);
    }

    // Token: 0x06002478 RID: 9336 RVA: 0x00633220 File Offset: 0x00633220
    private static void AddInkPaperScenes(List<DocumentGallery.Scene> list)
    {
      DocumentGallery.<>c__DisplayClass8_0 CS$<>8__locals1;
      CS$<>8__locals1.list = list;
      ClickyHomeRendering.IsOffscreen = true;
      string text = "ink-paper-stocks";
      double num = 1060.0;
      double num2 = 520.0;
      Func<FrameworkElement> func;
      if ((func = DocumentGallery.<>O.<0>__PaperStocksScene) == null)
      {
        func = (DocumentGallery.<>O.<0>__PaperStocksScene = new Func<FrameworkElement>(DocumentGallery.PaperStocksScene));
      }
      DocumentGallery.<AddInkPaperScenes>g__Add|8_0(text, num, num2, func, DocumentGallery.GalleryDesk, ref CS$<>8__locals1);
      string text2 = "ink-paper-pieces";
      double num3 = 760.0;
      double num4 = 420.0;
      Func<FrameworkElement> func2;
      if ((func2 = DocumentGallery.<>O.<1>__PaperPiecesScene) == null)
      {
        func2 = (DocumentGallery.<>O.<1>__PaperPiecesScene = new Func<FrameworkElement>(DocumentGallery.PaperPiecesScene));
      }
      DocumentGallery.<AddInkPaperScenes>g__Add|8_0(text2, num3, num4, func2, DocumentGallery.GalleryDesk, ref CS$<>8__locals1);
      string text3 = "ink-paper-tags";
      double num5 = 560.0;
      double num6 = 120.0;
      Func<FrameworkElement> func3;
      if ((func3 = DocumentGallery.<>O.<2>__PaperTagsScene) == null)
      {
        func3 = (DocumentGallery.<>O.<2>__PaperTagsScene = new Func<FrameworkElement>(DocumentGallery.PaperTagsScene));
      }
      DocumentGallery.<AddInkPaperScenes>g__Add|8_0(text3, num5, num6, func3, PaperColor.Black, ref CS$<>8__locals1);
      string text4 = "ink-paper-glyphs";
      double num7 = 1000.0;
      double num8 = 980.0;
      Func<FrameworkElement> func4;
      if ((func4 = DocumentGallery.<>O.<3>__PaperGlyphsScene) == null)
      {
        func4 = (DocumentGallery.<>O.<3>__PaperGlyphsScene = new Func<FrameworkElement>(DocumentGallery.PaperGlyphsScene));
      }
      DocumentGallery.<AddInkPaperScenes>g__Add|8_0(text4, num7, num8, func4, DocumentGallery.GalleryDesk, ref CS$<>8__locals1);
      string text5 = "ink-paper-sketch";
      double num9 = 880.0;
      double num10 = 560.0;
      Func<FrameworkElement> func5;
      if ((func5 = DocumentGallery.<>O.<4>__PaperSketchScene) == null)
      {
        func5 = (DocumentGallery.<>O.<4>__PaperSketchScene = new Func<FrameworkElement>(DocumentGallery.PaperSketchScene));
      }
      DocumentGallery.<AddInkPaperScenes>g__Add|8_0(text5, num9, num10, func5, PaperColor.Hex("#F6F8FC"), ref CS$<>8__locals1);
      string text6 = "ink-paper-hand";
      double num11 = 980.0;
      double num12 = 420.0;
      Func<FrameworkElement> func6;
      if ((func6 = DocumentGallery.<>O.<5>__PaperHandScene) == null)
      {
        func6 = (DocumentGallery.<>O.<5>__PaperHandScene = new Func<FrameworkElement>(DocumentGallery.PaperHandScene));
      }
      DocumentGallery.<AddInkPaperScenes>g__Add|8_0(text6, num11, num12, func6, PaperColor.White, ref CS$<>8__locals1);
      string text7 = "ink-paper-cards";
      double num13 = 1130.0;
      double num14 = 300.0;
      Func<FrameworkElement> func7;
      if ((func7 = DocumentGallery.<>O.<6>__PaperCardsScene) == null)
      {
        func7 = (DocumentGallery.<>O.<6>__PaperCardsScene = new Func<FrameworkElement>(DocumentGallery.PaperCardsScene));
      }
      DocumentGallery.<AddInkPaperScenes>g__Add|8_0(text7, num13, num14, func7, DocumentGallery.GalleryDesk, ref CS$<>8__locals1);
      string text8 = "ink-paper-notes";
      double num15 = 820.0;
      double num16 = 440.0;
      Func<FrameworkElement> func8;
      if ((func8 = DocumentGallery.<>O.<7>__PaperNotesScene) == null)
      {
        func8 = (DocumentGallery.<>O.<7>__PaperNotesScene = new Func<FrameworkElement>(DocumentGallery.PaperNotesScene));
      }
      DocumentGallery.<AddInkPaperScenes>g__Add|8_0(text8, num15, num16, func8, DocumentGallery.GalleryDesk, ref CS$<>8__locals1);
      string text9 = "ink-paper-edge";
      double num17 = 760.0;
      double num18 = 360.0;
      Func<FrameworkElement> func9;
      if ((func9 = DocumentGallery.<>O.<8>__PaperEdgeScene) == null)
      {
        func9 = (DocumentGallery.<>O.<8>__PaperEdgeScene = new Func<FrameworkElement>(DocumentGallery.PaperEdgeScene));
      }
      DocumentGallery.<AddInkPaperScenes>g__Add|8_0(text9, num17, num18, func9, DocumentGallery.GalleryDesk, ref CS$<>8__locals1);
    }

    // Token: 0x06002479 RID: 9337 RVA: 0x00633470 File Offset: 0x00633470
    private static void AddInkViewScenes(List<DocumentGallery.Scene> list)
    {
      ClickyHomeRendering.IsOffscreen = true;
      DocumentGallery.AddInkPageScenes(list);
      DocumentGallery.AddInkObjectScenes(list);
      DocumentGallery.AddInkInteractiveScenes(list);
      DocumentGallery.AddInkDashboardScenes(list);
      DocumentGallery.AddInkFormScenes(list);
      DocumentGallery.AddInkFrontPageScenes(list);
      DocumentGallery.AddInkEdgeScenes(list);
    }

    // Token: 0x0600247A RID: 9338 RVA: 0x006334A4 File Offset: 0x006334A4
    private static FrameworkElement DashPinned(Func<FrameworkElement> build)
    {
      Func<DateTimeOffset> referenceNow = ClickyInkDates.ReferenceNow;
      ClickyInkDates.ReferenceNow = () => DocumentGallery.DashboardNow;
      FrameworkElement frameworkElement;
      try
      {
        frameworkElement = build();
      }
      finally
      {
        ClickyInkDates.ReferenceNow = referenceNow;
      }
      return frameworkElement;
    }

    // Token: 0x0600247B RID: 9339 RVA: 0x006334FC File Offset: 0x006334FC
    private static double DashChangeClock(ClickyInkLiveNote note)
    {
      double? num = ClickyInkChange.ChangedAt(note.Pages[0].Blocks.First<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.Source.Kind == ClickyInkDocument.Kind.Spark && b.Source.Text == "Leads found").DashboardResolved(note.FinishedClock + 30.0).Source);
      if (num == null)
      {
        return note.FinishedClock;
      }
      return num.GetValueOrDefault();
    }

    // Token: 0x0600247C RID: 9340 RVA: 0x00633578 File Offset: 0x00633578
    private static FrameworkElement DashColumns(ClickyInkLiveNote note, double clock, string caption)
    {
      List<ClickyInkGroup> list = ClickyInkGroup.Groups(note.Pages[0], clock);
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 20.0
      };
      List<StackPanel> list2 = (from _ in Enumerable.Range(0, 3)
        select new StackPanel
        {
          Spacing = 10.0
        }).ToList<StackPanel>();
      double[] array = new double[3];
      foreach (ClickyInkGroup clickyInkGroup in list)
      {
        ClickyInkGroup g = clickyInkGroup;
        int num = Array.IndexOf<double>(array, array.Min());
        array[num] += (double)((g.Kind == ClickyInkDocument.Kind.Tiles) ? 3 : 1);
        list2[num].Children.Add(DocumentGallery.InkGroupScene(note, (ClickyInkGroup x) => x.Id == g.Id, new double?(clock), 0, null, 378.0, null));
      }
      foreach (StackPanel stackPanel2 in list2)
      {
        stackPanel.Children.Add(stackPanel2);
      }
      return new StackPanel
      {
        Spacing = 10.0,
        Margin = new Thickness(24.0),
        Children = 
        {
          DocumentGallery.DashCaption(caption),
          stackPanel
        }
      };
    }

    // Token: 0x0600247D RID: 9341 RVA: 0x00633734 File Offset: 0x00633734
    private static FrameworkElement DashPreviewGroup(ClickyInkLiveNote note, Func<ClickyInkGroup, bool> pick, double clock, ClickyInkActions actions)
    {
      ClickyHomeSpreadPaper note2 = ClickyHomeSpreadPaper.Note;
      ClickyInkStyle clickyInkStyle = new ClickyInkStyle(note2.Ink, note2.Quiet, new Color?(note2.Deep), note.LatestEdit(clock)).Resting(note, null);
      ClickyInkGroup clickyInkGroup = ClickyInkGroup.Groups(note.Pages[0], clock).First<ClickyInkGroup>(pick);
      ClickyInkEnvironment clickyInkEnvironment = ClickyInkEnvironment.Default.<Clone>$();
      clickyInkEnvironment.IsWindowPreview = true;
      FrameworkElement frameworkElement;
      using (ClickyInkEnvironment.Use(clickyInkEnvironment))
      {
        frameworkElement = ClickyInkGroupView.Make(clickyInkGroup, clock, clickyInkStyle, note2, actions);
      }
      return DocumentGallery.DashOnPaper(frameworkElement, 378.0);
    }

    // Token: 0x0600247E RID: 9342 RVA: 0x006337E8 File Offset: 0x006337E8
    private static FrameworkElement DashOnPaper(FrameworkElement view, double width)
    {
      return new Grid
      {
        Width = width + 72.0,
        Padding = new Thickness(36.0, 20.0, 36.0, 20.0),
        HorizontalAlignment = HorizontalAlignment.Left,
        Children = 
        {
          new PaperSurface(ClickyShape.Rectangle, ClickyHomeSpreadPaper.Note.Fill, new double?(0.45))
          {
            Margin = new Thickness(-36.0, -20.0, -36.0, -20.0)
          },
          view
        }
      };
    }

    // Token: 0x0600247F RID: 9343 RVA: 0x006338AC File Offset: 0x006338AC
    private static TextBlock DashCaption(string text)
    {
      return Ui.Text(text, 11.0, FontWeights.SemiBold, PaperColor.Hex("#6B6F7A"), TextAlignment.Left, 0, false, false);
    }

    // Token: 0x06002480 RID: 9344 RVA: 0x006338D0 File Offset: 0x006338D0
    private static void AddInkEdgeScenes(List<DocumentGallery.Scene> list)
    {
      DocumentGallery.AddInkScene(list, "ink-edge-rest", 1500.0, 330.0, delegate
      {
        StackPanel stackPanel = new StackPanel();
        stackPanel.Orientation = Orientation.Horizontal;
        stackPanel.Spacing = 24.0;
        stackPanel.Margin = new Thickness(24.0);
        ClickyInkLiveNote clickyInkLiveNote = ClickyInkSamples.LiveNote("# Espresso pick\n\n## In short\n> Bambino Plus; budget $120 more for a grinder {big=\"$399\" note=\"best milk under $500\"}\n- Auto milk frothing that actually works\n- Heats up in 3 seconds\n::sticky[It needs a grinder: no beans pre-ground]{color=pink}\n::photo[Breville Bambino Plus]{query=\"Breville Bambino Plus espresso machine\"}\n\n## Compare\n| Machine | Price | Milk |\n|---|---|---|\n| **Bambino Plus** | $399 | auto |\n| Gaggia Classic Pro | $449 | manual |\n- De'Longhi Dedica {out}\n  steam wand too weak in 3 reviews\n:::chart[Reviewer scores]{unit=/10}\nBambino Plus: 9.1 {pick}\nGaggia Classic Pro: 8.4\n:::\n\n## Next\n::ask[Find a grinder to match]{say=\"Find the best grinder under $150 to pair with the Bambino Plus\"}\n::question[Mostly milk drinks?]{yes=\"Mostly lattes, keep the Bambino\" no=\"Mostly espresso, reconsider the Gaggia\"}\n::link[Wirecutter: espresso machines]{url=https://www.nytimes.com/wirecutter/reviews/best-espresso-machine/}");
        ClickyInkLiveNote clickyInkLiveNote2 = ClickyInkSamples.LiveNote("# Short clip editor\n## In short\n> Start with CapCut. {big=\"CapCut\"}\n- Auto captions speed up short edits.\n::question[What is the clip for?]{yes=\"Vertical social\" no=\"A polished Mac edit\" yes_label=\"Social\" no_label=\"Polished\"}");
        double num = clickyInkLiveNote.FinishedClock * 0.45;
        stackPanel.Children.Add(DocumentGallery.EdgeLabelled("writing (job running)", new ClickyEdgeStackInkCard(DocumentGallery.EdgeThing("job-a", ClickyInkCardSubject.SubjectKind.Job, ClickyInkCardSubject.SubjectPhase.Working, ClickyThingStatus.Done, ClickyPaperStock.Cream), ClickyInkSamples.HomeInkNote(clickyInkLiveNote, num, null), ClickyEdgeStackInkPresentation.AtRest, "reading rtings.com", null, null, null, null, null, 640.0, false)));
        stackPanel.Children.Add(DocumentGallery.EdgeLabelled("needs you (a question)", new ClickyEdgeStackInkCard(DocumentGallery.EdgeThing("job-b", ClickyInkCardSubject.SubjectKind.Job, ClickyInkCardSubject.SubjectPhase.Done, ClickyThingStatus.Done, ClickyPaperStock.Cream), ClickyInkSamples.HomeInkNote(clickyInkLiveNote2, clickyInkLiveNote2.FinishedClock + 20.0, null), ClickyEdgeStackInkPresentation.AtRest, null, null, null, null, null, null, 640.0, false)));
        ClickyInkLiveNote clickyInkLiveNote3 = ClickyInkSamples.LiveNote(ClickyInkSamples.DeskPickVersions);
        stackPanel.Children.Add(DocumentGallery.EdgeLabelled("ready (kept, sky)", new ClickyEdgeStackInkCard(DocumentGallery.EdgeThing("kept-c", ClickyInkCardSubject.SubjectKind.Kept, ClickyInkCardSubject.SubjectPhase.Done, ClickyThingStatus.Ready, ClickyPaperStock.Sky), ClickyInkSamples.HomeInkNote(clickyInkLiveNote3, clickyInkLiveNote3.FinishedClock + 20.0, null), ClickyEdgeStackInkPresentation.AtRest, null, null, null, null, null, null, 640.0, false)));
        stackPanel.Children.Add(DocumentGallery.EdgeLabelled("done (kept, mint): no tape", new ClickyEdgeStackInkCard(DocumentGallery.EdgeThing("kept-d", ClickyInkCardSubject.SubjectKind.Kept, ClickyInkCardSubject.SubjectPhase.Done, ClickyThingStatus.Done, ClickyPaperStock.Mint), ClickyInkSamples.HomeInkNote(clickyInkLiveNote3, clickyInkLiveNote3.FinishedClock + 20.0, null), ClickyEdgeStackInkPresentation.AtRest, null, null, null, null, null, null, 640.0, false)));
        return stackPanel;
      });
      DocumentGallery.AddInkScene(list, "ink-edge-fanned", 1150.0, 330.0, delegate
      {
        StackPanel stackPanel2 = new StackPanel();
        stackPanel2.Orientation = Orientation.Horizontal;
        stackPanel2.Spacing = 24.0;
        stackPanel2.Margin = new Thickness(24.0);
        ClickyInkLiveNote clickyInkLiveNote4 = ClickyInkSamples.LiveNote("# Espresso pick\n\n## In short\n> Bambino Plus; budget $120 more for a grinder {big=\"$399\" note=\"best milk under $500\"}\n- Auto milk frothing that actually works\n- Heats up in 3 seconds\n::sticky[It needs a grinder: no beans pre-ground]{color=pink}\n::photo[Breville Bambino Plus]{query=\"Breville Bambino Plus espresso machine\"}\n\n## Compare\n| Machine | Price | Milk |\n|---|---|---|\n| **Bambino Plus** | $399 | auto |\n| Gaggia Classic Pro | $449 | manual |\n- De'Longhi Dedica {out}\n  steam wand too weak in 3 reviews\n:::chart[Reviewer scores]{unit=/10}\nBambino Plus: 9.1 {pick}\nGaggia Classic Pro: 8.4\n:::\n\n## Next\n::ask[Find a grinder to match]{say=\"Find the best grinder under $150 to pair with the Bambino Plus\"}\n::question[Mostly milk drinks?]{yes=\"Mostly lattes, keep the Bambino\" no=\"Mostly espresso, reconsider the Gaggia\"}\n::link[Wirecutter: espresso machines]{url=https://www.nytimes.com/wirecutter/reviews/best-espresso-machine/}");
        ClickyInkLiveNote clickyInkLiveNote5 = ClickyInkSamples.LiveNote(ClickyInkSamples.DeskPickVersions);
        ClickyHomeInkNote clickyHomeInkNote = ClickyInkSamples.HomeInkNote(clickyInkLiveNote5, clickyInkLiveNote5.FinishedClock + 5.0, null);
        stackPanel2.Children.Add(DocumentGallery.EdgeLabelled("front, writing", new ClickyEdgeStackInkCard(DocumentGallery.EdgeThing("job-a", ClickyInkCardSubject.SubjectKind.Job, ClickyInkCardSubject.SubjectPhase.Working, ClickyThingStatus.Done, ClickyPaperStock.Cream), ClickyInkSamples.HomeInkNote(clickyInkLiveNote4, clickyInkLiveNote4.FinishedClock * 0.3, null), ClickyEdgeStackInkPresentation.InFan(true), "comparing milk frothers", null, null, null, null, null, 640.0, false)));
        stackPanel2.Children.Add(DocumentGallery.EdgeLabelled("behind: changed since you looked (looked at clock 20)", new ClickyEdgeStackInkCard(DocumentGallery.EdgeThing("job-e", ClickyInkCardSubject.SubjectKind.Job, ClickyInkCardSubject.SubjectPhase.Done, ClickyThingStatus.Ready, ClickyPaperStock.Cream), clickyHomeInkNote, ClickyEdgeStackInkPresentation.InFan(false), null, new DateTimeOffset?(clickyHomeInkNote.StartedAt.AddSeconds(20.0)), null, null, null, null, 640.0, false)));
        stackPanel2.Children.Add(DocumentGallery.EdgeLabelled("behind: kept, periwinkle", new ClickyEdgeStackInkCard(DocumentGallery.EdgeThing("kept-f", ClickyInkCardSubject.SubjectKind.Kept, ClickyInkCardSubject.SubjectPhase.Done, ClickyThingStatus.Done, ClickyPaperStock.Periwinkle), ClickyInkSamples.HomeInkNote(clickyInkLiveNote5, clickyInkLiveNote5.FinishedClock + 60.0, null), ClickyEdgeStackInkPresentation.InFan(false), null, null, null, null, null, null, 640.0, false)));
        return stackPanel2;
      });
      DocumentGallery.AddInkScene(list, "ink-edge-pulled", 1060.0, 800.0, delegate
      {
        StackPanel stackPanel3 = new StackPanel();
        stackPanel3.Orientation = Orientation.Horizontal;
        stackPanel3.Spacing = 60.0;
        stackPanel3.Margin = new Thickness(60.0);
        ClickyInkLiveNote clickyInkLiveNote6 = ClickyInkSamples.LiveNote("# Short clip editor\n## In short\n> Start with CapCut. {big=\"CapCut\"}\n- Auto captions speed up short edits.\n::question[What is the clip for?]{yes=\"Vertical social\" no=\"A polished Mac edit\" yes_label=\"Social\" no_label=\"Polished\"}");
        ClickyInkLiveNote clickyInkLiveNote7 = ClickyInkSamples.LiveNote("# Clean up Downloads\n## In short\n> Nine old installers take 3.1 GB. {big=\"3.1 GB\"}\n:::review[Move these to the Trash?]{go=\"Move these to the Trash\" skip=\"Keep them\"}\n- [x] Figma-124.dmg — an old installer\n- [ ] Zoom.pkg — installed in 2023\n- [x] Xcode_15.xip — replaced by Xcode 16\n:::\n## Kept\n- Your photo exports");
        stackPanel3.Children.Add(DocumentGallery.EdgeLabelled("pulled: a question", new ClickyEdgeStackInkCard(DocumentGallery.EdgeThing("job-b", ClickyInkCardSubject.SubjectKind.Job, ClickyInkCardSubject.SubjectPhase.Done, ClickyThingStatus.Done, ClickyPaperStock.Cream), ClickyInkSamples.HomeInkNote(clickyInkLiveNote6, clickyInkLiveNote6.FinishedClock + 20.0, null), ClickyEdgeStackInkPresentation.PulledOut, null, null, null, null, null, null, 640.0, false)));
        stackPanel3.Children.Add(DocumentGallery.EdgeLabelled("pulled: a review (kept, butter)", new ClickyEdgeStackInkCard(DocumentGallery.EdgeThing("kept-g", ClickyInkCardSubject.SubjectKind.Kept, ClickyInkCardSubject.SubjectPhase.Done, ClickyThingStatus.Done, ClickyPaperStock.Butter), ClickyInkSamples.HomeInkNote(clickyInkLiveNote7, clickyInkLiveNote7.FinishedClock + 20.0, null), ClickyEdgeStackInkPresentation.PulledOut, null, null, null, null, null, null, 640.0, false)));
        return stackPanel3;
      });
      DocumentGallery.AddInkScene(list, "ink-edge-screen", 560.0, 380.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote8 = ClickyInkSamples.LiveNote("# Espresso pick\n\n## In short\n> Bambino Plus; budget $120 more for a grinder {big=\"$399\" note=\"best milk under $500\"}\n- Auto milk frothing that actually works\n- Heats up in 3 seconds\n::sticky[It needs a grinder: no beans pre-ground]{color=pink}\n::photo[Breville Bambino Plus]{query=\"Breville Bambino Plus espresso machine\"}\n\n## Compare\n| Machine | Price | Milk |\n|---|---|---|\n| **Bambino Plus** | $399 | auto |\n| Gaggia Classic Pro | $449 | manual |\n- De'Longhi Dedica {out}\n  steam wand too weak in 3 reviews\n:::chart[Reviewer scores]{unit=/10}\nBambino Plus: 9.1 {pick}\nGaggia Classic Pro: 8.4\n:::\n\n## Next\n::ask[Find a grinder to match]{say=\"Find the best grinder under $150 to pair with the Bambino Plus\"}\n::question[Mostly milk drinks?]{yes=\"Mostly lattes, keep the Bambino\" no=\"Mostly espresso, reconsider the Gaggia\"}\n::link[Wirecutter: espresso machines]{url=https://www.nytimes.com/wirecutter/reviews/best-espresso-machine/}");
        return new Grid
        {
          Width = 480.0,
          Height = 300.0,
          Margin = new Thickness(40.0),
          Children = 
          {
            new ClickyEdgeStackInkScreenPaper(ClickyHomeSpreadPaper.Of(ClickyPaperStock.Sky), true),
            new ClickyEdgeStackInkCard(DocumentGallery.EdgeThing("kept-c", ClickyInkCardSubject.SubjectKind.Kept, ClickyInkCardSubject.SubjectPhase.Done, ClickyThingStatus.Ready, ClickyPaperStock.Sky), ClickyInkSamples.HomeInkNote(clickyInkLiveNote8, clickyInkLiveNote8.FinishedClock + 20.0, null), ClickyEdgeStackInkPresentation.InFan(true), null, null, null, null, null, null, 640.0, true)
          }
        };
      });
      DocumentGallery.AddInkScene(list, "ink-edge-face", 860.0, 320.0, delegate
      {
        StackPanel stackPanel4 = new StackPanel();
        stackPanel4.Orientation = Orientation.Horizontal;
        stackPanel4.Spacing = 40.0;
        stackPanel4.Margin = new Thickness(30.0);
        ClickyInkLiveNote clickyInkLiveNote9 = ClickyInkSamples.LiveNote("# Espresso pick\n\n## In short\n> Bambino Plus; budget $120 more for a grinder {big=\"$399\" note=\"best milk under $500\"}\n- Auto milk frothing that actually works\n- Heats up in 3 seconds\n::sticky[It needs a grinder: no beans pre-ground]{color=pink}\n::photo[Breville Bambino Plus]{query=\"Breville Bambino Plus espresso machine\"}\n\n## Compare\n| Machine | Price | Milk |\n|---|---|---|\n| **Bambino Plus** | $399 | auto |\n| Gaggia Classic Pro | $449 | manual |\n- De'Longhi Dedica {out}\n  steam wand too weak in 3 reviews\n:::chart[Reviewer scores]{unit=/10}\nBambino Plus: 9.1 {pick}\nGaggia Classic Pro: 8.4\n:::\n\n## Next\n::ask[Find a grinder to match]{say=\"Find the best grinder under $150 to pair with the Bambino Plus\"}\n::question[Mostly milk drinks?]{yes=\"Mostly lattes, keep the Bambino\" no=\"Mostly espresso, reconsider the Gaggia\"}\n::link[Wirecutter: espresso machines]{url=https://www.nytimes.com/wirecutter/reviews/best-espresso-machine/}");
        ClickyInkLiveNote clickyInkLiveNote10 = ClickyInkSamples.LiveNote("# Espresso pick\n\n## In short\n> Checking which machine under $500 makes the best milk\n- [x] Find the machines people recommend\n- [ ] Compare prices and milk {now}\n- [ ] Pick one and the catch\n\n## Compare\n| Machine | Price | Milk |\n|---|---|---|\n| Bambino Plus | $399 | auto |");
        stackPanel4.Children.Add(DocumentGallery.EdgeLabelled("face: finished", new ClickyInkFaceView(clickyInkLiveNote9, clickyInkLiveNote9.FinishedClock + 5.0, null, false, null)));
        stackPanel4.Children.Add(DocumentGallery.EdgeLabelled("face: working, sky", new ClickyInkFaceView(clickyInkLiveNote10, clickyInkLiveNote10.FinishedClock + 5.0, new <>z__ReadOnlySingleElementList<ClickyInkActivity>(new ClickyInkActivity(0.0, "search", "searching “best espresso under $500”")), true, new ClickyHomeSpreadPaper?(ClickyHomeSpreadPaper.Of(ClickyPaperStock.Sky)))));
        return stackPanel4;
      });
      DocumentGallery.AddInkScene(list, "ink-edge-edition", 960.0, 1100.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote11 = ClickyInkSamples.LiveNote("# Pocketlog daily\n\n## Today\n::run{n=12 at=\"Sat Oct 10 · 7:00\" next=\"Sun 7:00\" note=\"App Store, Play Console, Stripe\"}\n> Crashes jumped on Android 2.14; signups held. {big=\"98.1%\" note=\"crash-free, down\"}\n:::tiles\n::spark[Signups]{values=\"118, 121, 133, 129, 142, 138\"}\n::figure[Revenue]{value=\"$1,319\" delta=\"+3%\"}\n::status[Crash-free]{state=bad note=\"98.1%: 2.14 crashes at login\"}\n::countdown[2.15 review]{to=\"2026-10-12 09:00\"}\n:::\n::sticky[Pause the 2.14 rollout: 212 crashes, all at login]{color=pink}\n\n## Your move\n::question[Pause the 2.14 rollout?]{yes=\"Pause the Android 2.14 rollout in the Play Console\" no=\"Keep it going and watch it\" yes_label=\"Pause it\" no_label=\"Keep going\" since=\"Oct 10\"}\n\n## Worth a look\n- Android 2.14 crashes at login on Pixel 8 {new}\n  212 crashes, 97% on 2.14\n- Onboarding step 3 drop-off is down to 38%\n\n## Funnel\n:::pipeline[Yesterday]\nVisited: 4,050\nSigned up: 138\nStarted trial: 64\nPaid: 11\n:::");
        return new Grid
        {
          Margin = new Thickness(30.0),
          Children = 
          {
            new ClickyInkEditionView(clickyInkLiveNote11, clickyInkLiveNote11.FinishedClock + 5.0, 12, "Tuesday, October 21", null)
          }
        };
      });
      DocumentGallery.AddInkScene(list, "ink-edge-desk", 1440.0, 1100.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote12 = ClickyInkSamples.LiveNote("# Espresso pick\n\n## In short\n> Bambino Plus; budget $120 more for a grinder {big=\"$399\" note=\"best milk under $500\"}\n- Auto milk frothing that actually works\n- Heats up in 3 seconds\n::sticky[It needs a grinder: no beans pre-ground]{color=pink}\n::photo[Breville Bambino Plus]{query=\"Breville Bambino Plus espresso machine\"}\n\n## Compare\n| Machine | Price | Milk |\n|---|---|---|\n| **Bambino Plus** | $399 | auto |\n| Gaggia Classic Pro | $449 | manual |\n- De'Longhi Dedica {out}\n  steam wand too weak in 3 reviews\n:::chart[Reviewer scores]{unit=/10}\nBambino Plus: 9.1 {pick}\nGaggia Classic Pro: 8.4\n:::\n\n## Next\n::ask[Find a grinder to match]{say=\"Find the best grinder under $150 to pair with the Bambino Plus\"}\n::question[Mostly milk drinks?]{yes=\"Mostly lattes, keep the Bambino\" no=\"Mostly espresso, reconsider the Gaggia\"}\n::link[Wirecutter: espresso machines]{url=https://www.nytimes.com/wirecutter/reviews/best-espresso-machine/}");
        return new ClickyInkDeskView(clickyInkLiveNote12, clickyInkLiveNote12.FinishedClock + 5.0, 1440.0, null);
      });
    }

    // Token: 0x06002481 RID: 9345 RVA: 0x00633A84 File Offset: 0x00633A84
    private static ClickyInkCardSubject EdgeThing(string id, ClickyInkCardSubject.SubjectKind kind, ClickyInkCardSubject.SubjectPhase phase, ClickyThingStatus status = ClickyThingStatus.Done, ClickyPaperStock stock = ClickyPaperStock.Cream)
    {
      return new ClickyInkCardSubject(id, kind, phase == ClickyInkCardSubject.SubjectPhase.Working, phase, status, stock);
    }

    // Token: 0x06002482 RID: 9346 RVA: 0x00633A98 File Offset: 0x00633A98
    private static FrameworkElement EdgeLabelled(string label, FrameworkElement content)
    {
      return new StackPanel
      {
        Spacing = 6.0,
        Children = 
        {
          content,
          DocumentGallery.Caption(label)
        }
      };
    }

    // Token: 0x06002483 RID: 9347 RVA: 0x00633ACC File Offset: 0x00633ACC
    private static FrameworkElement Captioned(FrameworkElement view, string caption)
    {
      return new StackPanel
      {
        Spacing = 6.0,
        VerticalAlignment = VerticalAlignment.Top,
        Children = 
        {
          view,
          Ui.Text(caption, 11.0, FontWeights.SemiBold, PaperColor.Hex("#6B6F7A"), TextAlignment.Left, 0, false, false)
        }
      };
    }

    // Token: 0x06002484 RID: 9348 RVA: 0x00633B30 File Offset: 0x00633B30
    private static FrameworkElement Sheet(ClickyInkLiveNote note, double clock, string caption, ClickyHomeSpreadPaper? paper = null, bool? isWorking = null, double? followsPenInHeight = null)
    {
      return DocumentGallery.Captioned(new ClickyInkSheetView(note, clock, paper, null, false, followsPenInHeight, null, isWorking, null, true, null)
      {
        VerticalAlignment = VerticalAlignment.Top
      }, caption);
    }

    // Token: 0x06002485 RID: 9349 RVA: 0x00633B70 File Offset: 0x00633B70
    private static FrameworkElement TopOfNote(ClickyInkLiveNote note, double clock, bool isWorking, bool animates, bool glance, double width, double height, string caption, ClickyPaperStock stock = ClickyPaperStock.Sky)
    {
      ClickyHomeSpreadPaper clickyHomeSpreadPaper = ClickyHomeSpreadPaper.Of(stock);
      ClickyHomeInkNote clickyHomeInkNote = ClickyInkSamples.HomeInkNote(note, clock, null);
      return DocumentGallery.Captioned(new Grid
      {
        Width = width,
        Height = height,
        CornerRadius = new CornerRadius(10.0),
        Children = 
        {
          new ClickyEdgeStackInkScreenPaper(clickyHomeSpreadPaper, true),
          new ClickyInkTopOfNote(clickyHomeInkNote, clickyHomeSpreadPaper, new ClickyInkActions(), isWorking, animates, glance)
        }
      }, caption);
    }

    // Token: 0x06002486 RID: 9350 RVA: 0x00633BF4 File Offset: 0x00633BF4
    private static void AddInkFrontPageScenes(List<DocumentGallery.Scene> list)
    {
      DocumentGallery.AddInkScene(list, "ink-dashform-board", 960.0, 640.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote = DocumentGallery.<AddInkFrontPageScenes>g__Runs|26_0();
        FrameworkElement frameworkElement = ClickyInkDashboardView.Make(clickyInkLiveNote, clickyInkLiveNote.FinishedClock + 30.0, null, null, null, null, null, null, ClickyInkTiles.Look.Ruled);
        return new StackPanel
        {
          Margin = new Thickness(40.0, 30.0, 24.0, 24.0),
          Children = { DocumentGallery.<AddInkFrontPageScenes>g__Captioned|26_1(frameworkElement, "dashboard, run 13 at rest (two columns, 864 wide)") }
        };
      });
      DocumentGallery.AddInkScene(list, "ink-dashform-run", 960.0, 600.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote2 = ClickyInkSamples.LiveNote("# Pocketlog daily\n\n## Today\n::run{n=12 at=\"Sat Oct 10 · 7:00\" next=\"Sun 7:00\" note=\"App Store, Play Console, Stripe\"}\n> Crashes jumped on Android 2.14; signups held. {big=\"98.1%\" note=\"crash-free, down\"}\n:::tiles\n::spark[Signups]{values=\"118, 121, 133, 129, 142, 138\"}\n::figure[Revenue]{value=\"$1,319\" delta=\"+3%\"}\n::status[Crash-free]{state=bad note=\"98.1%: 2.14 crashes at login\"}\n::countdown[2.15 review]{to=\"2026-10-12 09:00\"}\n:::\n::sticky[Pause the 2.14 rollout: 212 crashes, all at login]{color=pink}\n\n## Your move\n::question[Pause the 2.14 rollout?]{yes=\"Pause the Android 2.14 rollout in the Play Console\" no=\"Keep it going and watch it\" yes_label=\"Pause it\" no_label=\"Keep going\" since=\"Oct 10\"}\n\n## Worth a look\n- Android 2.14 crashes at login on Pixel 8 {new}\n  212 crashes, 97% on 2.14\n- Onboarding step 3 drop-off is down to 38%\n\n## Funnel\n:::pipeline[Yesterday]\nVisited: 4,050\nSigned up: 138\nStarted trial: 64\nPaid: 11\n:::");
        double num = clickyInkLiveNote2.FinishedClock + 30.0;
        ClickyHomeSpreadPaper? clickyHomeSpreadPaper = new ClickyHomeSpreadPaper?(ClickyHomeSpreadPaper.Of(ClickyPaperStock.Sky));
        ClickyInkActions clickyInkActions = null;
        int? num2 = new int?(12);
        FrameworkElement frameworkElement2 = ClickyInkDashboardView.Make(clickyInkLiveNote2, num, clickyHomeSpreadPaper, clickyInkActions, null, num2, null, null, ClickyInkTiles.Look.Ruled);
        return new StackPanel
        {
          Margin = new Thickness(40.0, 30.0, 24.0, 24.0),
          Children = { DocumentGallery.<AddInkFrontPageScenes>g__Captioned|26_1(frameworkElement2, "dashboard, run 12 on sky paper (your move on the right)") }
        };
      });
      DocumentGallery.AddInkScene(list, "ink-dashform-narrow", 1160.0, 960.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote3 = DocumentGallery.<AddInkFrontPageScenes>g__Runs|26_0();
        StackPanel stackPanel = new StackPanel();
        stackPanel.Orientation = Orientation.Horizontal;
        stackPanel.Spacing = 40.0;
        stackPanel.Margin = new Thickness(40.0, 30.0, 24.0, 24.0);
        UIElementCollection children = stackPanel.Children;
        ClickyInkLiveNote clickyInkLiveNote4 = clickyInkLiveNote3;
        double num3 = clickyInkLiveNote3.FinishedClock + 30.0;
        double? num4 = new double?((double)520);
        children.Add(DocumentGallery.<AddInkFrontPageScenes>g__Captioned|26_1(ClickyInkDashboardView.Make(clickyInkLiveNote4, num3, null, null, num4, null, null, null, ClickyInkTiles.Look.Ruled), "one column (520 wide)"));
        ClickyInkLiveNote clickyInkLiveNote5 = ClickyInkSamples.LiveNote("# Espresso pick\n\n## In short\n> Bambino Plus; budget $120 more for a grinder {big=\"$399\" note=\"best milk under $500\"}\n- Auto milk frothing that actually works\n- Heats up in 3 seconds\n::sticky[It needs a grinder: no beans pre-ground]{color=pink}\n::photo[Breville Bambino Plus]{query=\"Breville Bambino Plus espresso machine\"}\n\n## Compare\n| Machine | Price | Milk |\n|---|---|---|\n| **Bambino Plus** | $399 | auto |\n| Gaggia Classic Pro | $449 | manual |\n- De'Longhi Dedica {out}\n  steam wand too weak in 3 reviews\n:::chart[Reviewer scores]{unit=/10}\nBambino Plus: 9.1 {pick}\nGaggia Classic Pro: 8.4\n:::\n\n## Next\n::ask[Find a grinder to match]{say=\"Find the best grinder under $150 to pair with the Bambino Plus\"}\n::question[Mostly milk drinks?]{yes=\"Mostly lattes, keep the Bambino\" no=\"Mostly espresso, reconsider the Gaggia\"}\n::link[Wirecutter: espresso machines]{url=https://www.nytimes.com/wirecutter/reviews/best-espresso-machine/}");
        UIElementCollection children2 = stackPanel.Children;
        ClickyInkLiveNote clickyInkLiveNote6 = clickyInkLiveNote5;
        double num5 = clickyInkLiveNote5.FinishedClock + 30.0;
        num4 = new double?((double)520);
        ClickyInkDashboardView.Job job = new ClickyInkDashboardView.Job("Made by Clicky · espresso", ClickyThingStatus.Ready, null, null);
        children2.Add(DocumentGallery.<AddInkFrontPageScenes>g__Captioned|26_1(ClickyInkDashboardView.Make(clickyInkLiveNote6, num5, null, null, num4, null, null, job, ClickyInkTiles.Look.Ruled), "a job's board"));
        return stackPanel;
      });
      DocumentGallery.AddInkScene(list, "ink-dashform-face", 1180.0, 300.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote7 = DocumentGallery.<AddInkFrontPageScenes>g__Runs|26_0();
        StackPanel stackPanel2 = new StackPanel();
        stackPanel2.Orientation = Orientation.Horizontal;
        stackPanel2.Spacing = 34.0;
        stackPanel2.Margin = new Thickness(36.0, 40.0, 24.0, 24.0);
        ClickyInkLiveNote clickyInkLiveNote8 = ClickyInkSamples.LiveNote("# Pocketlog daily\n\n## Today\n::run{n=12 at=\"Sat Oct 10 · 7:00\" next=\"Sun 7:00\" note=\"App Store, Play Console, Stripe\"}\n> Crashes jumped on Android 2.14; signups held. {big=\"98.1%\" note=\"crash-free, down\"}\n:::tiles\n::spark[Signups]{values=\"118, 121, 133, 129, 142, 138\"}\n::figure[Revenue]{value=\"$1,319\" delta=\"+3%\"}\n::status[Crash-free]{state=bad note=\"98.1%: 2.14 crashes at login\"}\n::countdown[2.15 review]{to=\"2026-10-12 09:00\"}\n:::\n::sticky[Pause the 2.14 rollout: 212 crashes, all at login]{color=pink}\n\n## Your move\n::question[Pause the 2.14 rollout?]{yes=\"Pause the Android 2.14 rollout in the Play Console\" no=\"Keep it going and watch it\" yes_label=\"Pause it\" no_label=\"Keep going\" since=\"Oct 10\"}\n\n## Worth a look\n- Android 2.14 crashes at login on Pixel 8 {new}\n  212 crashes, 97% on 2.14\n- Onboarding step 3 drop-off is down to 38%\n\n## Funnel\n:::pipeline[Yesterday]\nVisited: 4,050\nSigned up: 138\nStarted trial: 64\nPaid: 11\n:::");
        stackPanel2.Children.Add(DocumentGallery.<AddInkFrontPageScenes>g__Captioned|26_1(ClickyInkDashboardFaceView.Make(clickyInkLiveNote8, clickyInkLiveNote8.FinishedClock + 30.0, null, null), "face, run 12"));
        stackPanel2.Children.Add(DocumentGallery.<AddInkFrontPageScenes>g__Captioned|26_1(ClickyInkDashboardFaceView.Make(clickyInkLiveNote7, 41.2, null, null), "face, run 13 landing (clock 41.2)"));
        stackPanel2.Children.Add(DocumentGallery.<AddInkFrontPageScenes>g__Captioned|26_1(ClickyInkDashboardFaceView.Make(clickyInkLiveNote7, clickyInkLiveNote7.FinishedClock + 30.0, new ClickyHomeSpreadPaper?(ClickyHomeSpreadPaper.Of(ClickyPaperStock.Mint)), null), "face, run 13 at rest, mint"));
        return stackPanel2;
      });
      ValueTuple<ClickyInkFrontPageVariant, string>[] array = new ValueTuple<ClickyInkFrontPageVariant, string>[]
      {
        new ValueTuple<ClickyInkFrontPageVariant, string>(ClickyInkFrontPageVariant.Broadsheet, "broadsheet"),
        new ValueTuple<ClickyInkFrontPageVariant, string>(ClickyInkFrontPageVariant.MissionControl, "mission")
      };
      for (int i = 0; i < array.Length; i++)
      {
        ValueTuple<ClickyInkFrontPageVariant, string> valueTuple = array[i];
        ClickyInkFrontPageVariant item = valueTuple.Item1;
        string name = valueTuple.Item2;
        ClickyInkFrontPageVariant v = item;
        DocumentGallery.AddInkScene(list, "ink-front-" + name, 1000.0, 1240.0, delegate
        {
          ClickyInkLiveNote clickyInkLiveNote9 = DocumentGallery.<AddInkFrontPageScenes>g__Runs|26_0();
          ClickyInkLiveNote clickyInkLiveNote10 = ClickyInkSamples.LiveNote("# Espresso pick\n\n## In short\n> Bambino Plus; budget $120 more for a grinder {big=\"$399\" note=\"best milk under $500\"}\n- Auto milk frothing that actually works\n- Heats up in 3 seconds\n::sticky[It needs a grinder: no beans pre-ground]{color=pink}\n::photo[Breville Bambino Plus]{query=\"Breville Bambino Plus espresso machine\"}\n\n## Compare\n| Machine | Price | Milk |\n|---|---|---|\n| **Bambino Plus** | $399 | auto |\n| Gaggia Classic Pro | $449 | manual |\n- De'Longhi Dedica {out}\n  steam wand too weak in 3 reviews\n:::chart[Reviewer scores]{unit=/10}\nBambino Plus: 9.1 {pick}\nGaggia Classic Pro: 8.4\n:::\n\n## Next\n::ask[Find a grinder to match]{say=\"Find the best grinder under $150 to pair with the Bambino Plus\"}\n::question[Mostly milk drinks?]{yes=\"Mostly lattes, keep the Bambino\" no=\"Mostly espresso, reconsider the Gaggia\"}\n::link[Wirecutter: espresso machines]{url=https://www.nytimes.com/wirecutter/reviews/best-espresso-machine/}");
          return new StackPanel
          {
            Spacing = 24.0,
            Margin = new Thickness(40.0, 30.0, 24.0, 24.0),
            Children = 
            {
              DocumentGallery.<AddInkFrontPageScenes>g__Captioned|26_1(ClickyInkFrontPageView.Make(clickyInkLiveNote9, clickyInkLiveNote9.FinishedClock + 30.0, v, null, null, null, null, null), name + ": a kept routine's run 13"),
              DocumentGallery.<AddInkFrontPageScenes>g__Captioned|26_1(ClickyInkFrontPageView.Make(clickyInkLiveNote10, clickyInkLiveNote10.FinishedClock + 30.0, v, null, null, null, null, null), name + ": a one-off job")
            }
          };
        });
      }
      DocumentGallery.AddInkScene(list, "ink-front-writing", 1000.0, 640.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote11 = DocumentGallery.<AddInkFrontPageScenes>g__Runs|26_0();
        return new StackPanel
        {
          Spacing = 24.0,
          Margin = new Thickness(40.0, 30.0, 24.0, 24.0),
          Children = { DocumentGallery.<AddInkFrontPageScenes>g__Captioned|26_1(ClickyInkFrontPageView.Make(clickyInkLiveNote11, 41.5, ClickyInkFrontPageVariant.MissionControl, null, null, null, null, null), "mission control while run 13 is written (clock 41.5): LIVE") }
        };
      });
    }

    // Token: 0x06002487 RID: 9351 RVA: 0x00633DC8 File Offset: 0x00633DC8
    private static StackPanel InteractiveRow(params FrameworkElement[] children)
    {
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 22.0,
        Margin = new Thickness(24.0)
      };
      foreach (FrameworkElement frameworkElement in children)
      {
        frameworkElement.VerticalAlignment = VerticalAlignment.Top;
        stackPanel.Children.Add(frameworkElement);
      }
      return stackPanel;
    }

    // Token: 0x06002488 RID: 9352 RVA: 0x00633E30 File Offset: 0x00633E30
    private static FrameworkElement InteractiveCaptioned(FrameworkElement view, string caption)
    {
      return new StackPanel
      {
        Spacing = 6.0,
        Children = 
        {
          view,
          Ui.Text(caption, 11.0, FontWeights.SemiBold, PaperColor.Hex("#6B6F7A"), TextAlignment.Left, 0, false, false)
        }
      };
    }

    // Token: 0x06002489 RID: 9353 RVA: 0x00633E8C File Offset: 0x00633E8C
    private static FrameworkElement InkObjectScene(Func<FrameworkElement> make, string caption)
    {
      InkObjectState.ResetAll();
      ClickyHomeSpreadPaper note = ClickyHomeSpreadPaper.Note;
      return DocumentGallery.InteractiveCaptioned(new Grid
      {
        Width = 450.0,
        Padding = new Thickness(36.0, 24.0, 36.0, 24.0),
        Children = 
        {
          new PaperSurface(ClickyShape.Rectangle, note.Fill, new double?(0.45))
          {
            Margin = new Thickness(-36.0, -24.0, -36.0, -24.0)
          },
          make()
        }
      }, caption);
    }

    // Token: 0x0600248A RID: 9354 RVA: 0x00633F58 File Offset: 0x00633F58
    private static ClickyInkLiveNote.Block Block(string text, ClickyInkDocument.Kind kind)
    {
      return DocumentGallery.FindBlock(ClickyInkSamples.LiveNote(text), kind);
    }

    // Token: 0x0600248B RID: 9355 RVA: 0x00633F68 File Offset: 0x00633F68
    private static ClickyInkLiveNote.Block FindBlock(ClickyInkLiveNote note, ClickyInkDocument.Kind kind)
    {
      return note.Pages.SelectMany<ClickyInkLiveNote.Page, ClickyInkLiveNote.Block>((ClickyInkLiveNote.Page p) => p.Blocks).First<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.Source.Kind == kind);
    }

    // Token: 0x0600248C RID: 9356 RVA: 0x00633FC0 File Offset: 0x00633FC0
    [NullableContext(2)]
    [return: Nullable(1)]
    private static ClickyInkActions WithFeedback(ClickyInkNoteAction pending = null, ClickyInkNoteAction answered = null)
    {
      DateTimeOffset now = DateTimeOffset.Now;
      List<ClickyInkSentAction> list = new List<ClickyInkSentAction>();
      if (answered != null)
      {
        list.Add(new ClickyInkSentAction(answered, now.AddMinutes(-3.0), false));
      }
      if (pending != null)
      {
        list.Add(new ClickyInkSentAction(pending, now.AddSeconds(-2.0), false));
      }
      DateTimeOffset dateTimeOffset = ((pending == null) ? now.AddMinutes(-1.0) : now.AddMinutes(-2.0));
      return new ClickyInkActions
      {
        Feedback = ClickyInkActionFeedback.Make(list, new DateTimeOffset?(dateTimeOffset), true, null, null, new DateTimeOffset?(now))
      };
    }

    // Token: 0x0600248D RID: 9357 RVA: 0x00634068 File Offset: 0x00634068
    private static FrameworkElement FillForm(ClickyInkLiveNote note, ClickyInkLiveNote.Block form)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary["Your studio name"] = "Bright Pixel Studio";
      dictionary["Tone"] = "Friendly";
      dictionary["Portfolio"] = "C:\\Users\\maya\\Documents\\Studio portfolio 2026";
      ClickyInkForm.SetForGallery(form, dictionary);
      return DocumentGallery.InkGroupScene(note, (ClickyInkGroup g) => g.Kind == ClickyInkDocument.Kind.Form, null, 0, null, 378.0, null);
    }

    // Token: 0x0600248E RID: 9358 RVA: 0x006340F4 File Offset: 0x006340F4
    [return: Nullable(2)]
    private static ClickyInkInlineAnswerField FindField(FrameworkElement root)
    {
      return DocumentGallery.Descendants(root).OfType<ClickyInkInlineAnswerField>().FirstOrDefault<ClickyInkInlineAnswerField>();
    }

    // Token: 0x0600248F RID: 9359 RVA: 0x00634108 File Offset: 0x00634108
    private static IEnumerable<DependencyObject> Descendants(DependencyObject root)
    {
      DocumentGallery.<Descendants>d__35 <Descendants>d__ = new DocumentGallery.<Descendants>d__35(-2);
      <Descendants>d__.<>3__root = root;
      return <Descendants>d__;
    }

    // Token: 0x06002490 RID: 9360 RVA: 0x00634118 File Offset: 0x00634118
    [NullableContext(0)]
    private unsafe static void WriteSamplePng([Nullable(1)] string path, [TupleElementNames(new string[] { "R", "G", "B" })] ValueTuple<int, int, int> a, [TupleElementNames(new string[] { "R", "G", "B" })] ValueTuple<int, int, int> b, int size = 240)
    {
      if (File.Exists(path))
      {
        return;
      }
      byte[] array = new byte[size * (size * 3 + 1)];
      for (int i = 0; i < size; i++)
      {
        int num = i * (size * 3 + 1);
        array[num] = 0;
        for (int j = 0; j < size; j++)
        {
          double num2 = (double)(j + i) / (2.0 * (double)size);
          double num3 = (double)j - (double)size / 2.0;
          double num4 = (double)i - (double)size / 2.0;
          int num8;
          int num9;
          int num10;
          if (num3 * num3 + num4 * num4 >= (double)(size * size) / 9.0)
          {
            int num5 = (int)((double)a.Item1 + (double)(b.Item1 - a.Item1) * num2 * 0.5);
            int num6 = (int)((double)a.Item2 + (double)(b.Item2 - a.Item2) * num2 * 0.5);
            int num7 = (int)((double)a.Item3 + (double)(b.Item3 - a.Item3) * num2 * 0.5);
            num8 = num7;
            num9 = num6;
            num10 = num5;
          }
          else
          {
            num10 = b.Item1;
            num9 = b.Item2;
            num8 = b.Item3;
          }
          array[num + 1 + j * 3] = (byte)num10;
          array[num + 2 + j * 3] = (byte)num9;
          array[num + 3 + j * 3] = (byte)num8;
        }
      }
      DocumentGallery.<>c__DisplayClass36_0 CS$<>8__locals1;
      CS$<>8__locals1.file = File.Create(path);
      try
      {
        CS$<>8__locals1.file.Write(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.4C4B6A3BE1314AB86138BEF4314DDE022E600960D8689A2C8F8631802D20DAB6), 8));
        byte[] array2 = new byte[13];
        byte[] bytes = BitConverter.GetBytes(size);
        if (BitConverter.IsLittleEndian)
        {
          Array.Reverse<byte>(bytes);
        }
        bytes.CopyTo(array2, 0);
        bytes.CopyTo(array2, 4);
        array2[8] = 8;
        array2[9] = 2;
        DocumentGallery.<WriteSamplePng>g__Chunk|36_0("IHDR", array2, ref CS$<>8__locals1);
        using (MemoryStream memoryStream = new MemoryStream())
        {
          using (ZLibStream zlibStream = new ZLibStream(memoryStream, CompressionLevel.Optimal, true))
          {
            zlibStream.Write(array);
          }
          DocumentGallery.<WriteSamplePng>g__Chunk|36_0("IDAT", memoryStream.ToArray(), ref CS$<>8__locals1);
          DocumentGallery.<WriteSamplePng>g__Chunk|36_0("IEND", Array.Empty<byte>(), ref CS$<>8__locals1);
        }
      }
      finally
      {
        if (CS$<>8__locals1.file != null)
        {
          ((IDisposable)CS$<>8__locals1.file).Dispose();
        }
      }
    }

    // Token: 0x06002491 RID: 9361 RVA: 0x00634390 File Offset: 0x00634390
    private static uint Crc32(byte[] data)
    {
      uint num = uint.MaxValue;
      foreach (byte b in data)
      {
        num ^= (uint)b;
        for (int j = 0; j < 8; j++)
        {
          num = (((num & 1U) != 0U) ? (3988292384U ^ (num >> 1)) : (num >> 1));
        }
      }
      return ~num;
    }

    // Token: 0x06002492 RID: 9362 RVA: 0x006343E0 File Offset: 0x006343E0
    private static string TravelNote()
    {
      string text = DocumentGallery.SamplePicture("belem.png", new ValueTuple<int, int, int>(240, 170, 120), new ValueTuple<int, int, int>(90, 120, 170));
      string text2 = DocumentGallery.SamplePicture("market.png", new ValueTuple<int, int, int>(200, 90, 60), new ValueTuple<int, int, int>(250, 220, 150));
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(760, 3);
      defaultInterpolatedStringHandler.AppendLiteral("# Lisbon in May\n\n## In short\n> Fly ANA on the 12th; the whole trip comes to $3,380 {big=\"$3,380\" note=\"under budget by $420\"}\n::ticket[ANA NH7, nonstop]{from=\"Boston (BOS)\" to=\"Lisbon (LIS)\" when=\"May 12, 9:40 pm\" price=\"$842\" seat=\"32K\" kind=flight}\n:::receipt[Trip budget]\nFlights: $1,684\nHotel, 5 nights: $1,120\nFood and trams … $576\n:::\n::place[Time Out Market]{note=\"go hungry, 8 stalls\" rating=4.6 hours=\"10am–midnight\"}\n:::day[Saturday May 13 · Belém · 9 km]\n9:30 Pastéis de Belém — before the line\n11:00 Jerónimos Monastery\n1:00 pm Lunch at the market — Time Out, stall 12\n:::\n:::day[Proposal timeline]\n- Draft to Dana\n- Review with legal — Thursday\n:::\n::photo[Belém at sunset]{path=\"");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("\"}\n::photo[The market hall]{path=\"");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral("\"}\n::photo[A web picture]{url=\"");
      defaultInterpolatedStringHandler.AppendFormatted("https://upload.wikimedia.org/wikipedia/commons/thumb/4/47/PNG_transparency_demonstration_1.png/330px-PNG_transparency_demonstration_1.png");
      defaultInterpolatedStringHandler.AppendLiteral("\"}");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06002493 RID: 9363 RVA: 0x006344A4 File Offset: 0x006344A4
    private static string LinksNote()
    {
      string text = DocumentGallery.SamplePicture("site-shot.png", new ValueTuple<int, int, int>(245, 245, 240), new ValueTuple<int, int, int>(60, 110, 220));
      string sampleDirectory = DocumentGallery.SampleDirectory;
      File.WriteAllText(Path.Combine(sampleDirectory, "index.html"), "<html><body>Site</body></html>");
      foreach (string text2 in new string[] { "hero.jpg", "styles.css", "app.js", "data.json", "logo.svg", "about.html", "contact.html", "favicon.ico" })
      {
        File.WriteAllText(Path.Combine(sampleDirectory, text2), text2);
      }
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1278, 10);
      defaultInterpolatedStringHandler.AppendLiteral("# Launch reply\n\n## In short\n> The draft is ready in your Gmail {big=\"1\"}\n::link[Open the Gmail draft]{url=\"https://mail.google.com/mail/u/0/#drafts/abc\"}\n::file[The site, ready to open]{path=\"");
      defaultInterpolatedStringHandler.AppendFormatted(Path.Combine(sampleDirectory, "index.html"));
      defaultInterpolatedStringHandler.AppendLiteral("\" thumb=\"");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("\"}\n:::letter{to=\"Dana Reyes\" subject=\"Re: launch date\" send=\"Send this reply to Dana from my Gmail\"}\nHi Dana,\n\nThursday works. I moved the review to 3 pm so legal can join, and the deck is in the shared folder.\n\nTalk soon,\nMaya\n:::\n::clip[The steadiest desk we tested at standing height.]{by=Wirecutter source=https://www.nytimes.com/wirecutter/reviews/best-standing-desk/}\n::question[Mostly milk drinks?]{yes=\"Mostly lattes, keep the Bambino\" no=\"Mostly espresso, reconsider the Gaggia\"}\n::question[Which city first?]{options=\"Lisbon | Porto | Madeira\" other=\"Somewhere else…\" since=\"Oct 9\"}\n::ask[Find a grinder to match]{say=\"Find the best grinder under $150\"}\n::ask[Book the hotel]{say=\"Book the hotel in Belém\"}\n::file[Hero photo]{path=\"");
      defaultInterpolatedStringHandler.AppendFormatted(Path.Combine(sampleDirectory, "hero.jpg"));
      defaultInterpolatedStringHandler.AppendLiteral("\"}\n::file[Styles]{path=\"");
      defaultInterpolatedStringHandler.AppendFormatted(Path.Combine(sampleDirectory, "styles.css"));
      defaultInterpolatedStringHandler.AppendLiteral("\"}\n::file[Script]{path=\"");
      defaultInterpolatedStringHandler.AppendFormatted(Path.Combine(sampleDirectory, "app.js"));
      defaultInterpolatedStringHandler.AppendLiteral("\"}\n::file[Data]{path=\"");
      defaultInterpolatedStringHandler.AppendFormatted(Path.Combine(sampleDirectory, "data.json"));
      defaultInterpolatedStringHandler.AppendLiteral("\"}\n::file[Logo]{path=\"");
      defaultInterpolatedStringHandler.AppendFormatted(Path.Combine(sampleDirectory, "logo.svg"));
      defaultInterpolatedStringHandler.AppendLiteral("\"}\n::file[About]{path=\"");
      defaultInterpolatedStringHandler.AppendFormatted(Path.Combine(sampleDirectory, "about.html"));
      defaultInterpolatedStringHandler.AppendLiteral("\"}\n::file[Contact]{path=\"");
      defaultInterpolatedStringHandler.AppendFormatted(Path.Combine(sampleDirectory, "contact.html"));
      defaultInterpolatedStringHandler.AppendLiteral("\"}\n::file[Icon]{path=\"");
      defaultInterpolatedStringHandler.AppendFormatted(Path.Combine(sampleDirectory, "favicon.ico"));
      defaultInterpolatedStringHandler.AppendLiteral("\"}\n::link[Wirecutter: espresso machines]{url=https://www.nytimes.com/wirecutter/reviews/best-espresso-machine/}\n::link[Reddit thread]{url=https://www.reddit.com/r/espresso/}");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06002494 RID: 9364 RVA: 0x006346A8 File Offset: 0x006346A8
    private static void AddInkScene(List<DocumentGallery.Scene> list, string name, double width, double height, Func<FrameworkElement> build, double seconds)
    {
      list.Add(new DocumentGallery.Scene(name, width, height, () => new Grid
      {
        Background = Ui.Brush(DocumentGallery.InkDesk),
        Children = { build() }
      }, new Color?(DocumentGallery.InkDesk), seconds));
    }

    // Token: 0x06002495 RID: 9365 RVA: 0x006346EC File Offset: 0x006346EC
    private static FrameworkElement ObjectsRow(params FrameworkElement[] columns)
    {
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 22.0,
        Margin = new Thickness(24.0)
      };
      foreach (FrameworkElement frameworkElement in columns)
      {
        frameworkElement.VerticalAlignment = VerticalAlignment.Top;
        stackPanel.Children.Add(frameworkElement);
      }
      return stackPanel;
    }

    // Token: 0x06002496 RID: 9366 RVA: 0x00634754 File Offset: 0x00634754
    private static FrameworkElement ObjectsCaptioned(FrameworkElement view, string caption)
    {
      return new StackPanel
      {
        Spacing = 6.0,
        Children = 
        {
          view,
          Ui.Text(caption, 11.0, FontWeights.SemiBold, PaperColor.Hex("#6B6F7A"), TextAlignment.Left, 0, false, false)
        }
      };
    }

    // Token: 0x06002497 RID: 9367 RVA: 0x006347B0 File Offset: 0x006347B0
    [NullableContext(0)]
    [return: Nullable(1)]
    private unsafe static string SamplePicture([Nullable(1)] string name, [TupleElementNames(new string[] { "R", "G", "B" })] ValueTuple<int, int, int> sky, [TupleElementNames(new string[] { "R", "G", "B" })] ValueTuple<int, int, int> land)
    {
      string text = Path.Combine(DocumentGallery.SampleDirectory, name);
      if (File.Exists(text))
      {
        return text;
      }
      byte[] array = new byte[307440];
      for (int i = 0; i < 240; i++)
      {
        int num = i * 1281;
        array[num] = 0;
        for (int j = 0; j < 320; j++)
        {
          double num2 = 148.8 + Math.Sin((double)j / 37.0) * 18.0 + Math.Sin((double)j / 11.0) * 5.0;
          bool flag = Math.Pow((double)(j - 230), 2.0) + Math.Pow((double)(i - 70), 2.0) < 676.0;
          double num3 = (double)i / 240.0;
          double num7;
          double num8;
          double num9;
          if ((double)i <= num2)
          {
            if (!flag)
            {
              double num4 = (double)sky.Item1 + (double)(255 - sky.Item1) * num3 * 0.6;
              double num5 = (double)sky.Item2 + (double)(255 - sky.Item2) * num3 * 0.6;
              double num6 = (double)sky.Item3 + (double)(255 - sky.Item3) * num3 * 0.6;
              num7 = num6;
              num8 = num5;
              num9 = num4;
            }
            else
            {
              double num10 = 255.0;
              double num6 = 236.0;
              double num5 = 170.0;
              num7 = num5;
              num8 = num6;
              num9 = num10;
            }
          }
          else
          {
            double num11 = (double)land.Item1 * (1.0 - num3 * 0.3);
            double num5 = (double)land.Item2 * (1.0 - num3 * 0.3);
            double num6 = (double)land.Item3 * (1.0 - num3 * 0.3);
            num7 = num6;
            num8 = num5;
            num9 = num11;
          }
          int num12 = num + 1 + j * 4;
          array[num12] = (byte)num9;
          array[num12 + 1] = (byte)num8;
          array[num12 + 2] = (byte)num7;
          array[num12 + 3] = byte.MaxValue;
        }
      }
      DocumentGallery.<>c__DisplayClass45_0 CS$<>8__locals1;
      CS$<>8__locals1.file = File.Create(text);
      string text2;
      try
      {
        CS$<>8__locals1.file.Write(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.4C4B6A3BE1314AB86138BEF4314DDE022E600960D8689A2C8F8631802D20DAB6), 8));
        byte[] array2 = new byte[13];
        DocumentGallery.BigEndian(320U).CopyTo(array2, 0);
        DocumentGallery.BigEndian(240U).CopyTo(array2, 4);
        array2[8] = 8;
        array2[9] = 6;
        DocumentGallery.<SamplePicture>g__Chunk|45_0("IHDR", array2, ref CS$<>8__locals1);
        using (MemoryStream memoryStream = new MemoryStream())
        {
          using (ZLibStream zlibStream = new ZLibStream(memoryStream, CompressionLevel.Optimal, true))
          {
            zlibStream.Write(array);
          }
          DocumentGallery.<SamplePicture>g__Chunk|45_0("IDAT", memoryStream.ToArray(), ref CS$<>8__locals1);
          DocumentGallery.<SamplePicture>g__Chunk|45_0("IEND", Array.Empty<byte>(), ref CS$<>8__locals1);
          text2 = text;
        }
      }
      finally
      {
        if (CS$<>8__locals1.file != null)
        {
          ((IDisposable)CS$<>8__locals1.file).Dispose();
        }
      }
      return text2;
    }

    // Token: 0x06002498 RID: 9368 RVA: 0x00634B00 File Offset: 0x00634B00
    private static byte[] BigEndian(uint value)
    {
      return new byte[]
      {
        (byte)(value >> 24),
        (byte)(value >> 16),
        (byte)(value >> 8),
        (byte)value
      };
    }

    // Token: 0x06002499 RID: 9369 RVA: 0x00634B24 File Offset: 0x00634B24
    private static uint Crc(byte[] data)
    {
      uint num = uint.MaxValue;
      foreach (byte b in data)
      {
        num ^= (uint)b;
        for (int j = 0; j < 8; j++)
        {
          num = (((num & 1U) != 0U) ? (3988292384U ^ (num >> 1)) : (num >> 1));
        }
      }
      return num ^ uint.MaxValue;
    }

    // Token: 0x0600249A RID: 9370 RVA: 0x00634B74 File Offset: 0x00634B74
    private static TextBlock Caption(string text)
    {
      return Ui.Text(text, 11.0, FontWeights.SemiBold, PaperColor.Hex("#6B6F7A"), TextAlignment.Left, 0, false, false);
    }

    // Token: 0x0600249B RID: 9371 RVA: 0x00634B98 File Offset: 0x00634B98
    private static FrameworkElement Labelled(string label, FrameworkElement content)
    {
      return new StackPanel
      {
        Spacing = 6.0,
        Children = 
        {
          content,
          DocumentGallery.Caption(label)
        }
      };
    }

    // Token: 0x0600249C RID: 9372 RVA: 0x00634BCC File Offset: 0x00634BCC
    private static FrameworkElement PaperStocksScene()
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 22.0,
        Margin = new Thickness(24.0)
      };
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 18.0
      };
      foreach (ClickyPaperStock clickyPaperStock in ClickyPaperStockStyle.AllCases)
      {
        ClickyEdgeStackInkPaper clickyEdgeStackInkPaper = new ClickyEdgeStackInkPaper(ClickyHomeSpreadPaper.Of(clickyPaperStock), false, false, 9.0)
        {
          Width = 100.0,
          Height = 130.0
        };
        Grid grid = new Grid
        {
          Width = 100.0,
          Height = 130.0,
          Children = { clickyEdgeStackInkPaper }
        };
        grid.Children.Add(new StackPanel
        {
          Margin = new Thickness(12.0),
          Spacing = 4.0,
          Children = 
          {
            PaperText.Line(clickyPaperStock.RawValue(), ClickyPaperFonts.Hand(21.0), clickyPaperStock.Ink(), TextAlignment.Left),
            PaperText.Line("quiet ink", ClickyPaperFonts.Hand(16.0), clickyPaperStock.QuietInk(), TextAlignment.Left),
            PaperText.Line("deep", ClickyPaperFonts.Hand(16.0), clickyPaperStock.Deep(), TextAlignment.Left),
            new Border
            {
              Height = 14.0,
              Width = 60.0,
              Background = Ui.Brush(clickyPaperStock.Wash()),
              HorizontalAlignment = HorizontalAlignment.Left
            }
          }
        });
        stackPanel2.Children.Add(DocumentGallery.Labelled(clickyPaperStock.RawValue(), grid));
      }
      stackPanel.Children.Add(stackPanel2);
      StackPanel stackPanel3 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 24.0
      };
      stackPanel3.Children.Add(DocumentGallery.Labelled("job note (perforated)", new ClickyEdgeStackInkPaper(ClickyHomeSpreadPaper.Note, true, false, 9.0)
      {
        Width = 200.0,
        Height = 220.0
      }));
      stackPanel3.Children.Add(DocumentGallery.Labelled("kept, lifted", new ClickyEdgeStackInkPaper(ClickyHomeSpreadPaper.Of(ClickyPaperStock.Mint), false, true, 9.0)
      {
        Width = 200.0,
        Height = 220.0
      }));
      stackPanel3.Children.Add(DocumentGallery.Labelled("screen paper + board dots", new ClickyEdgeStackInkScreenPaper(ClickyHomeSpreadPaper.Of(ClickyPaperStock.Blush), true)
      {
        Width = 260.0,
        Height = 220.0
      }));
      stackPanel3.Children.Add(DocumentGallery.Labelled("cream note fill + grain", new PaperSurface(ClickyShape.Rectangle, ClickyHomeSpreadPaper.Note.Fill, new double?(0.45))
      {
        Width = 160.0,
        Height = 220.0
      }));
      stackPanel.Children.Add(stackPanel3);
      return stackPanel;
    }

    // Token: 0x0600249D RID: 9373 RVA: 0x00634F1C File Offset: 0x00634F1C
    private static FrameworkElement PaperPiecesScene()
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 20.0,
        Margin = new Thickness(24.0)
      };
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 18.0
      };
      foreach (ClickyThingStatus clickyThingStatus in Enum.GetValues<ClickyThingStatus>())
      {
        stackPanel2.Children.Add(new ClickyPaperStamp(clickyThingStatus, null, 9.5, -6.0));
      }
      stackPanel2.Children.Add(new ClickyPaperStamp(ClickyThingStatus.NeedsYou, null, 14.0, -5.0));
      stackPanel2.Children.Add(new ClickyPaperStamp(ClickyThingStatus.Ready, "sent", 12.0, -6.0));
      stackPanel.Children.Add(DocumentGallery.Labelled("stamps (every status at 9.5, NEEDS YOU at 14, \"sent\" at 12)", stackPanel2));
      StackPanel stackPanel3 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 24.0,
        Margin = new Thickness(6.0, 6.0, 0.0, 0.0)
      };
      foreach (ClickyPaperStickerKind clickyPaperStickerKind in Enum.GetValues<ClickyPaperStickerKind>())
      {
        stackPanel3.Children.Add(new ClickyPaperSticker(clickyPaperStickerKind, 42.0));
      }
      stackPanel.Children.Add(DocumentGallery.Labelled("stickers", stackPanel3));
      StackPanel stackPanel4 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 8.0
      };
      foreach (string text in new string[] { "Deck.key", "Quote.pdf", "Budget.xlsx", "Hero.png", "Brief.md", "index.html", "data.json" })
      {
        stackPanel4.Children.Add(new ClickyPaperFileChip(ClickyPaperFileKinds.ShortLabel(text), new ClickyPaperFileKind?(ClickyPaperFileKinds.ForFileName(text)), 24.0, 11.0));
      }
      stackPanel.Children.Add(DocumentGallery.Labelled("file chips", stackPanel4));
      StackPanel stackPanel5 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 10.0
      };
      foreach (ClickyPaperGel clickyPaperGel in Enum.GetValues<ClickyPaperGel>())
      {
        stackPanel5.Children.Add(new ClickyPaperGelButton(clickyPaperGel.ToString(), null, clickyPaperGel, 26.0, 13.0, 12.0, null));
      }
      stackPanel5.Children.Add(new ClickyPaperGelButton("Copied", null, ClickyPaperGel.Blue, 26.0, 13.0, 12.0, "checkmark"));
      stackPanel5.Children.Add(new ClickyPaperGelButton("Allow once", null, ClickyPaperGel.White, 26.0, 12.0, 12.0, null));
      stackPanel.Children.Add(DocumentGallery.Labelled("gel buttons", stackPanel5));
      StackPanel stackPanel6 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 18.0
      };
      stackPanel6.Children.Add(new ClickyPaperProgressBar(0.0, 74.0, 4.0, null, null));
      stackPanel6.Children.Add(new ClickyPaperProgressBar(0.4, 74.0, 4.0, null, null));
      stackPanel6.Children.Add(new ClickyPaperProgressBar(1.0, 74.0, 4.0, null, null));
      stackPanel6.Children.Add(new ClickyPaperProgressBar(0.66, 140.0, 6.0, null, null));
      stackPanel.Children.Add(DocumentGallery.Labelled("progress bars", stackPanel6));
      return stackPanel;
    }

    // Token: 0x0600249E RID: 9374 RVA: 0x006353A8 File Offset: 0x006353A8
    private static FrameworkElement PaperTagsScene()
    {
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 10.0,
        Margin = new Thickness(20.0),
        VerticalAlignment = VerticalAlignment.Center
      };
      foreach (ValueTuple<ClickyThingStatus, string> valueTuple in new ValueTuple<ClickyThingStatus, string>[]
      {
        new ValueTuple<ClickyThingStatus, string>(ClickyThingStatus.NeedsYou, "Needs you"),
        new ValueTuple<ClickyThingStatus, string>(ClickyThingStatus.Ready, "Ready"),
        new ValueTuple<ClickyThingStatus, string>(ClickyThingStatus.Working, "Writing"),
        new ValueTuple<ClickyThingStatus, string>(ClickyThingStatus.Watching, "Watching"),
        new ValueTuple<ClickyThingStatus, string>(ClickyThingStatus.Done, "Done"),
        new ValueTuple<ClickyThingStatus, string>(ClickyThingStatus.Paused, "Paused")
      })
      {
        ClickyThingStatus item = valueTuple.Item1;
        string item2 = valueTuple.Item2;
        stackPanel.Children.Add(new ClickyPaperStatusTag(item, item2, 22.0, 11.0));
      }
      return stackPanel;
    }

    // Token: 0x0600249F RID: 9375 RVA: 0x006354A8 File Offset: 0x006354A8
    private static FrameworkElement PaperGlyphsScene()
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 16.0,
        Margin = new Thickness(24.0)
      };
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 14.0
      };
      foreach (ClickyPaperGlyph clickyPaperGlyph in ClickyPaperGlyphs.AllCases)
      {
        stackPanel2.Children.Add(new StackPanel
        {
          Spacing = 6.0,
          Children = 
          {
            new ClickyPaperGlyphView(clickyPaperGlyph, ClickyPaperStock.Cream.Ink(), 32.0, 1.8),
            new ClickyPaperGlyphView(clickyPaperGlyph, ClickyPaperInk.ClickyBlue, 17.0, 1.8),
            DocumentGallery.Caption(clickyPaperGlyph.RawValue())
          }
        });
      }
      stackPanel.Children.Add(stackPanel2);
      StackPanel stackPanel3 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 20.0
      };
      Grid grid = new Grid
      {
        Width = 120.0,
        Height = 70.0,
        Background = Ui.Brush(PaperColor.Hex("#1F2025"))
      };
      grid.Children.Add(new ClickyBrandMark(64.0, null, 3.0)
      {
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      });
      stackPanel3.Children.Add(DocumentGallery.Labelled("brand mark, gradient", grid));
      stackPanel3.Children.Add(DocumentGallery.Labelled("ink, 21 @ 2.4", new ClickyBrandMark(21.0, new Color?(ClickyPaperStock.Sky.Ink()), 2.4)));
      stackPanel3.Children.Add(DocumentGallery.Labelled("blue, 48", new ClickyBrandMark(48.0, new Color?(ClickyPaperInk.ClickyBlue), 3.0)));
      stackPanel.Children.Add(stackPanel3);
      stackPanel.Children.Add(DocumentGallery.Caption("SF Symbols the Ink notes use, as Segoe Fluent Icons"));
      ClickyNotebookFlow clickyNotebookFlow = new ClickyNotebookFlow
      {
        Spacing = 6.0,
        LineSpacing = 6.0,
        Width = 950.0
      };
      foreach (string text in ClickyInkIcon.KnownNames.OrderBy<string, string>((string n) => n))
      {
        StackPanel stackPanel4 = new StackPanel
        {
          Width = 86.0,
          Spacing = 2.0
        };
        stackPanel4.Children.Add(ClickyInkIcon.Glyph(text, 18.0, ClickyPaperStock.Cream.Ink(), false));
        TextBlock textBlock = Ui.Text(text, 8.0, FontWeights.Normal, PaperColor.Hex("#6B6F7A"), TextAlignment.Center, 0, false, false);
        textBlock.TextTrimming = TextTrimming.CharacterEllipsis;
        textBlock.MaxWidth = 86.0;
        stackPanel4.Children.Add(textBlock);
        clickyNotebookFlow.Children.Add(stackPanel4);
      }
      stackPanel.Children.Add(clickyNotebookFlow);
      return stackPanel;
    }

    // Token: 0x060024A0 RID: 9376 RVA: 0x00635834 File Offset: 0x00635834
    private static FrameworkElement PaperSketchScene()
    {
      Grid grid = new Grid
      {
        Margin = new Thickness(24.0),
        ColumnSpacing = 24.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(380.0)
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      ClickySketchCard clickySketchCard = new ClickySketchCard(ClickySketchTone.Blue1, "Voice", 3L, true)
      {
        Height = 230.0,
        VerticalAlignment = VerticalAlignment.Top
      };
      clickySketchCard.With(new UIElement[]
      {
        new ClickyHandText("Cedar", 30.0, 1.1, null, 1.0, TextAlignment.Left).WithForeground(ClickySketchInk.BigWords),
        new ClickySketchTip("say something, the bars move", ClickySketchTone.Blue1),
        new ClickySketchMeter(new <>z__ReadOnlyArray<double>(new double[] { 0.1, 0.3, 0.6, 0.9, 0.4, 0.2, 0.05, 0.7, 0.5, 0.1 }), 15L)
        {
          Margin = new Thickness(0.0, 8.0, 0.0, 0.0)
        },
        new ClickySketchBar(0.62, 10.0, null, null, 8L)
        {
          Margin = new Thickness(0.0, 10.0, 0.0, 0.0)
        }
      });
      grid.Children.Add(clickySketchCard);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 14.0
      };
      UIElementCollection children = stackPanel.Children;
      double num = 10.0;
      UIElement[] array = new UIElement[4];
      array[0] = new ClickySketchButton("Upgrade", delegate
      {
      }, ClickySketchButtonKind.Teal, 34.0, 16.0, 18.0, 4L, null);
      array[1] = new ClickySketchButton("Change", delegate
      {
      }, ClickySketchButtonKind.Plain, 34.0, 16.0, 18.0, 4L, null);
      array[2] = new ClickySketchButton("Not now", delegate
      {
      }, ClickySketchButtonKind.Quiet, 34.0, 16.0, 18.0, 4L, null);
      array[3] = new ClickySketchButton("Refresh", delegate
      {
      }, ClickySketchButtonKind.Plain, 34.0, 16.0, 18.0, 4L, "arrow.clockwise");
      children.Add(Ui.H(num, array));
      UIElementCollection children2 = stackPanel.Children;
      double num2 = 18.0;
      UIElement[] array2 = new UIElement[3];
      array2[0] = new ClickySketchLink("delete account", delegate
      {
      }, null, 17.0);
      array2[1] = new ClickySketchCheck(null, 1.0);
      array2[2] = new ClickySketchCheck(null, 1.5);
      children2.Add(Ui.H(num2, array2));
      stackPanel.Children.Add(Ui.H(12.0, new UIElement[]
      {
        new ClickySketchToggle(true, 6L, ""),
        new ClickySketchToggle(false, 7L, "")
      }));
      stackPanel.Children.Add(new ClickySketchToggleRow("Speak finished work out loud, even when typed", true, ClickySketchTone.Blue2)
      {
        Width = 300.0,
        HorizontalAlignment = HorizontalAlignment.Left
      });
      stackPanel.Children.Add(new ClickySketchSegmented<string>(new <>z__ReadOnlyArray<string>(new string[] { "Calm", "Bright", "Bold" }), "Bright", (string s) => s, delegate(string _)
      {
      }, 17.0, 9L));
      stackPanel.Children.Add(new ClickySketchSlider(new <>z__ReadOnlyArray<string>(new string[] { "0.75×", "1×", "1.25×", "1.5×" }), 1, delegate(int _)
      {
      }, 262.0, 11L));
      StackPanel stackPanel2 = Ui.H(6.0, Array.Empty<UIElement>());
      foreach (ValueTuple<string, int> valueTuple in new string[] { "#3380FF", "#FF4C4C", "#D4A017", "#26C281", "#9B59F5" }.Select<string, ValueTuple<string, int>>((string h, int i) => new ValueTuple<string, int>(h, i)))
      {
        string item = valueTuple.Item1;
        int item2 = valueTuple.Item2;
        stackPanel2.Children.Add(new ClickySketchSwatch(PaperColor.Hex(item), item2 == 0, delegate
        {
        }, 24.0, (long)(13 + item2), ""));
      }
      stackPanel.Children.Add(stackPanel2);
      UIElementCollection children3 = stackPanel.Children;
      double num3 = 8.0;
      UIElement[] array3 = new UIElement[3];
      array3[0] = new ClickySketchIconTile((Color c) => ClickyInkIcon.Glyph("gearshape", 13.0, c, false), ClickySketchTone.BluePale, 20L, 26.0);
      array3[1] = new ClickySketchIconTile((Color c) => ClickyInkIcon.Glyph("person", 13.0, c, false), ClickySketchTone.Blue2, 21L, 26.0);
      array3[2] = new ClickySketchIconTile((Color c) => ClickyInkIcon.Glyph("mic", 13.0, c, false), ClickySketchTone.Blue3, 22L, 26.0);
      children3.Add(Ui.H(num3, array3));
      Grid.SetColumn(stackPanel, 1);
      grid.Children.Add(stackPanel);
      return grid;
    }

    // Token: 0x060024A1 RID: 9377 RVA: 0x00635F10 File Offset: 0x00635F10
    private static FrameworkElement PaperHandScene()
    {
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 26.0,
        Margin = new Thickness(24.0)
      };
      foreach (ValueTuple<double, double> valueTuple in new ValueTuple<double, double>[]
      {
        new ValueTuple<double, double>(16.0, 1.25),
        new ValueTuple<double, double>(19.0, 1.2),
        new ValueTuple<double, double>(24.0, 1.1)
      })
      {
        double item = valueTuple.Item1;
        double item2 = valueTuple.Item2;
        StackPanel stackPanel2 = new StackPanel
        {
          Width = 200.0,
          Spacing = 10.0
        };
        UIElementCollection children = stackPanel2.Children;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 2);
        defaultInterpolatedStringHandler.AppendLiteral("hand ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(item);
        defaultInterpolatedStringHandler.AppendLiteral(" × ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(item2);
        children.Add(DocumentGallery.Caption(defaultInterpolatedStringHandler.ToStringAndClear()));
        ClickyHandText clickyHandText = new ClickyHandText("Clicky writes the way the boards set it: every line box is size × line-height tall, with the glyphs centred in it.", item, item2, null, 1.0, TextAlignment.Left).WithForeground(ClickyPaperStock.Cream.Ink());
        stackPanel2.Children.Add(new Border
        {
          BorderBrush = Ui.Brush(PaperColor.Hex("#FFB0B0")),
          BorderThickness = new Thickness(0.5),
          Child = clickyHandText,
          HorizontalAlignment = HorizontalAlignment.Left
        });
        stackPanel2.Children.Add(new ClickyHandText("Clicky writes the way the boards set it: every line box is size × line-height tall, with the glyphs centred in it.", item, item2, new int?(2), 1.0, TextAlignment.Left).WithForeground(ClickyPaperInk.ClickyBlue));
        stackPanel2.Children.Add(new ClickyHandText("centred " + item.ToString(), item, item2, null, 1.0, TextAlignment.Center)
        {
          Width = 200.0
        }.WithForeground(ClickyPaperInk.Pencil));
        stackPanel2.Children.Add(new ClickyHandText("one line that is long enough to shrink a little", item, item2, new int?(1), 0.7, TextAlignment.Left)
        {
          Width = 200.0
        }.WithForeground(ClickyPaperStock.Cream.Ink()));
        stackPanel.Children.Add(stackPanel2);
      }
      StackPanel stackPanel3 = new StackPanel
      {
        Spacing = 12.0
      };
      stackPanel3.Children.Add(DocumentGallery.Caption("CSS text (system 13 @ 18)"));
      stackPanel3.Children.Add(ClickyHandText.CssText("Exact facts are typed in the system font and wrap greedily.", ClickyPaperFonts.Typed(13.0, null), 18.0, TextAlignment.Left, null).WithForeground(PaperColor.Black));
      stackPanel3.Children.Add(DocumentGallery.Caption("writing pencil (still offscreen)"));
      stackPanel3.Children.Add(new ClickyNotebookWritingPencil(ClickyHomeSpreadPaper.Note));
      stackPanel3.Children.Add(new ClickyNotebookWritingPencil(ClickyHomeSpreadPaper.Of(ClickyPaperStock.Periwinkle)));
      stackPanel3.Width = 220.0;
      stackPanel.Children.Add(stackPanel3);
      return stackPanel;
    }

    // Token: 0x060024A2 RID: 9378 RVA: 0x0063626C File Offset: 0x0063626C
    private static FrameworkElement PaperCardsScene()
    {
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 8.0,
        Margin = new Thickness(16.0)
      };
      stackPanel.Children.Add(new ClickyCardView(new ClickyCardFace("Tokyo flights", ClickyPaperGlyph.Plane, ClickyPaperStock.Sky)
      {
        Caps = "SFO → NRT · Oct 14",
        Headline = new ClickyCardFace.CardHeadline.Big("$612"),
        Detail = new ClickyCardFace.CardDetail.Sparkline(new double[] { 680.0, 664.0, 671.0, 652.0, 640.0, 655.0, 630.0, 612.0 }, true),
        Foot = "down $48 since Monday",
        Sticker = new ClickyPaperStickerKind?(ClickyPaperStickerKind.Live)
      }, true));
      stackPanel.Children.Add(new ClickyCardView(new ClickyCardFace("Inbox", ClickyPaperGlyph.Tray, ClickyPaperStock.Mint)
      {
        Caps = "this morning",
        Headline = new ClickyCardFace.CardHeadline.Count("3", "need you"),
        Detail = new ClickyCardFace.CardDetail.Waiting(new string[] { "Omar Ruiz", "Design review", "Invoice #2231" }),
        Foot = "all answered by noon",
        FootTone = ClickyCardFace.CardFootTone.Good,
        Sticker = new ClickyPaperStickerKind?(ClickyPaperStickerKind.NeedsYou)
      }, true));
      stackPanel.Children.Add(new ClickyCardView(new ClickyCardFace("Talk prep", ClickyPaperGlyph.Bubble, ClickyPaperStock.Peach)
      {
        Caps = "Thursday · 2 pm",
        Headline = new ClickyCardFace.CardHeadline.Sentence("Three talking points, drafted"),
        Detail = new ClickyCardFace.CardDetail.Bullets(new string[] { "open with the demo", "pricing last", "leave 10 min" }),
        Foot = "edited 5 min ago",
        FootTone = ClickyCardFace.CardFootTone.Quiet,
        Sticker = new ClickyPaperStickerKind?(ClickyPaperStickerKind.New)
      }, true));
      stackPanel.Children.Add(new ClickyCardView(new ClickyCardFace("Espresso picks", ClickyPaperGlyph.Cart, ClickyPaperStock.Kraft)
      {
        Caps = "under $500",
        Detail = new ClickyCardFace.CardDetail.Rows(new ClickyCardFace.CardRow[]
        {
          new ClickyCardFace.CardRow("Breville Bambino", "$299"),
          new ClickyCardFace.CardRow("Gaggia Classic", "$449"),
          new ClickyCardFace.CardRow("Rancilio Silvia", "$495"),
          new ClickyCardFace.CardRow("Lelit Anna", "$480")
        }),
        Foot = "best: Gaggia"
      }, true));
      stackPanel.Children.Add(new ClickyCardView(new ClickyCardFace("Reply to Omar", ClickyPaperGlyph.Mail, ClickyPaperStock.Butter)
      {
        Caps = "from Omar Ruiz",
        Headline = new ClickyCardFace.CardHeadline.Message("Omar", "Can we move Thursday to Friday? Something came up.", "Friday works, same time?")
      }, true));
      return stackPanel;
    }

    // Token: 0x060024A3 RID: 9379 RVA: 0x006364F4 File Offset: 0x006364F4
    private static FrameworkElement PaperNotesScene()
    {
      DocumentGallery.<>c__DisplayClass58_0 CS$<>8__locals1;
      CS$<>8__locals1.grid = new Grid
      {
        Margin = new Thickness(16.0),
        ColumnSpacing = 12.0,
        RowSpacing = 12.0
      };
      for (int i = 0; i < 3; i++)
      {
        CS$<>8__locals1.grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
      }
      for (int j = 0; j < 2; j++)
      {
        CS$<>8__locals1.grid.RowDefinitions.Add(new RowDefinition
        {
          Height = GridLength.Auto
        });
      }
      DocumentGallery.<PaperNotesScene>g__Add|58_0(new ClickyNoteView(new ClickyNoteFace("Book the dentist", ClickyPaperGlyph.Calendar, new ClickyNoteFace.NoteBody.Lines(new ClickyNoteFace.NoteLine[]
      {
        new ClickyNoteFace.NoteLine("found 3 openings")
        {
          Mark = new ClickyNoteFace.LineMark.Done()
        },
        new ClickyNoteFace.NoteLine("asking which works")
        {
          Mark = new ClickyNoteFace.LineMark.Now()
        },
        new ClickyNoteFace.NoteLine("book it")
        {
          Mark = new ClickyNoteFace.LineMark.Todo()
        }
      }))
      {
        Meta = "Dr. Patel · Mission St",
        Footer = new ClickyNoteFace.NoteFooter.Progress(2, 3, 0.66),
        Stamp = new ClickyThingStatus?(ClickyThingStatus.Working)
      }, null), 0, 0, ref CS$<>8__locals1);
      DocumentGallery.<PaperNotesScene>g__Add|58_0(new ClickyNoteView(new ClickyNoteFace("Q3 revenue", ClickyPaperGlyph.Chart, new ClickyNoteFace.NoteBody.Chart(new double[] { 12.0, 14.0, 13.0, 17.0, 19.0, 18.0, 22.0 }, true))
      {
        Footer = new ClickyNoteFace.NoteFooter.Files(new string[] { "Q3 deck.key", "numbers.xlsx", "summary.pdf", "chart.png", "notes.md" }),
        Stamp = new ClickyThingStatus?(ClickyThingStatus.Ready)
      }, null), 0, 1, ref CS$<>8__locals1);
      DocumentGallery.<PaperNotesScene>g__Add|58_0(new ClickyNoteView(new ClickyNoteFace("Weekend in Big Sur", ClickyPaperGlyph.Pin, new ClickyNoteFace.NoteBody.Lines(new ClickyNoteFace.NoteLine[]
      {
        new ClickyNoteFace.NoteLine("")
        {
          Runs = new ClickyNoteFace.LineRun[]
          {
            new ClickyNoteFace.LineRun("Leave ", false),
            new ClickyNoteFace.LineRun("Saturday 8 am", true),
            new ClickyNoteFace.LineRun(" from home", false)
          },
          HasCaret = true
        },
        new ClickyNoteFace.NoteLine("Ventana Inn")
        {
          Value = "$495",
          IsCircled = true
        },
        new ClickyNoteFace.NoteLine("Post Ranch")
        {
          Value = "$1,250"
        }
      }))
      {
        Meta = "from your calendar",
        MetaLogo = new ClickyPaperGlyph?(ClickyPaperGlyph.Calendar),
        MetaIsClicky = true,
        IsIdea = true,
        Footer = new ClickyNoteFace.NoteFooter.Actions(new ClickyNoteFace.NoteAction[]
        {
          new ClickyNoteFace.NoteAction("yes", "Yes, plan it", ClickyPaperGel.Blue),
          new ClickyNoteFace.NoteAction("no", "Not now", ClickyPaperGel.White)
        })
      }, null), 0, 2, ref CS$<>8__locals1);
      DocumentGallery.<PaperNotesScene>g__Add|58_0(new ClickyNoteView(new ClickyNoteFace("Reply to Omar", ClickyPaperGlyph.Bubble, new ClickyNoteFace.NoteBody.Lines(new ClickyNoteFace.NoteLine[]
      {
        new ClickyNoteFace.NoteLine("Friday works, same time?")
        {
          Tone = ClickyNoteFace.LineTone.Clicky
        },
        new ClickyNoteFace.NoteLine("waiting for your OK")
        {
          Tone = ClickyNoteFace.LineTone.Quiet
        }
      }))
      {
        Meta = "to Omar Ruiz · about Thursday",
        Footer = new ClickyNoteFace.NoteFooter.Actions(new ClickyNoteFace.NoteAction[]
        {
          new ClickyNoteFace.NoteAction("send", "Allow once", ClickyPaperGel.Blue),
          new ClickyNoteFace.NoteAction("not", "Not now", ClickyPaperGel.White)
        }),
        Stamp = new ClickyThingStatus?(ClickyThingStatus.NeedsYou)
      }, null), 1, 0, ref CS$<>8__locals1);
      DocumentGallery.<PaperNotesScene>g__Add|58_0(new ClickyNoteView(new ClickyNoteFace("Groceries", ClickyPaperGlyph.Cart, new ClickyNoteFace.NoteBody.Lines(new ClickyNoteFace.NoteLine[]
      {
        new ClickyNoteFace.NoteLine("oat milk")
        {
          Mark = new ClickyNoteFace.LineMark.Number(1),
          Value = "$4.99"
        },
        new ClickyNoteFace.NoteLine("sourdough")
        {
          Mark = new ClickyNoteFace.LineMark.Number(2),
          Value = "$7.50"
        },
        new ClickyNoteFace.NoteLine("coffee beans")
        {
          Mark = new ClickyNoteFace.LineMark.Number(3),
          Value = "$16"
        }
      }))
      {
        Footer = new ClickyNoteFace.NoteFooter.Text("delivery by 6 pm"),
        Stamp = new ClickyThingStatus?(ClickyThingStatus.Done)
      }, null), 1, 1, ref CS$<>8__locals1);
      return CS$<>8__locals1.grid;
    }

    // Token: 0x060024A4 RID: 9380 RVA: 0x00636908 File Offset: 0x00636908
    private static FrameworkElement PaperEdgeScene()
    {
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 30.0,
        Margin = new Thickness(30.0)
      };
      foreach (ClickyEdgeStackInkReading.State state in Enum.GetValues<ClickyEdgeStackInkReading.State>())
      {
        Grid grid = new Grid
        {
          Width = 90.0,
          Height = 260.0
        };
        grid.Children.Add(new ClickyEdgeStackInkPaper(ClickyHomeSpreadPaper.Of(ClickyPaperStock.Sky), false, false, 9.0));
        grid.Children.Add(new ClickyEdgeStackInkTape(state));
        stackPanel.Children.Add(DocumentGallery.Labelled(state.ToString() + (state.WantsAttention() ? " · attention" : ""), grid));
      }
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = 14.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      stackPanel2.Children.Add(ClickyEdgeStackHighlight.Wrap(PaperText.Line("what changed", ClickyPaperFonts.Hand(21.0), ClickyPaperStock.Cream.Ink(), TextAlignment.Left), true, 2));
      stackPanel2.Children.Add(ClickyEdgeStackHighlight.Wrap(PaperText.Line("unchanged", ClickyPaperFonts.Hand(21.0), ClickyPaperStock.Cream.Ink(), TextAlignment.Left), false, 3));
      stackPanel.Children.Add(DocumentGallery.Labelled("highlighter", stackPanel2));
      return stackPanel;
    }

    // Token: 0x060024A5 RID: 9381 RVA: 0x00636A8C File Offset: 0x00636A8C
    private static void AddInkObjectScenes(List<DocumentGallery.Scene> list)
    {
      DocumentGallery.AddInkScene(list, "ink-obj-pick", 1060.0, 900.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote = ClickyInkSamples.LiveNote("# Headphones\n\n## In short\n> Sony for flights; Bose if comfort comes first {big=\"$449.99\"}\n::figure[Battery]{value=\"30 h\" delta=\"+6 h\"}\n::figure[Noise cut]{value=\"-38 dB\" delta=\"down 2 dB\"}\n::figure[Weight]{value=\"250 g\" delta=\"lightest\"}\n| Headphones | Price | ANC battery | Best for |\n|---|---|---|---|\n| **Sony WH-1000XM6** {pick} | $449.99 | 30 h | Flights and the office, the best noise cancelling we tried |\n| Bose QuietComfort Ultra | $299.95 | 24 h | Comfort on long days |\n| Sennheiser Momentum 4 {out} | $379.95 | 60 h | Battery above everything |\n::sticky[Buy from the airline store: 10% off]{color=pink}\n::sticky[Return window is 30 days]\n::sticky[Case is sold apart]{color=blue}\n:::chart[Reviewer scores]{unit=/10}\nSony WH-1000XM6: 9.1 {pick}\nBose QuietComfort Ultra: 8.4\nSennheiser Momentum 4: 7.9\n:::");
        ClickyInkImages.LoadAsync("https://upload.wikimedia.org/wikipedia/commons/thumb/4/47/PNG_transparency_demonstration_1.png/330px-PNG_transparency_demonstration_1.png");
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 16.0
        };
        stackPanel.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Figure), null, 0, null, 378.0, null));
        stackPanel.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Table), null, 0, null, 378.0, null));
        stackPanel.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Chart), null, 0, null, 378.0, null));
        StackPanel stackPanel2 = new StackPanel
        {
          Spacing = 16.0
        };
        stackPanel2.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Sticky), null, 0, null, 378.0, null));
        stackPanel2.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Table), null, 0, null, 300.0, null));
        return DocumentGallery.ObjectsRow(new FrameworkElement[] { stackPanel, stackPanel2 });
      });
      DocumentGallery.AddInkScene(list, "ink-obj-trip", 1060.0, 980.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote2 = ClickyInkSamples.LiveNote(DocumentGallery.TravelNote());
        StackPanel stackPanel3 = new StackPanel
        {
          Spacing = 16.0
        };
        stackPanel3.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote2, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Ticket), null, 0, null, 378.0, null));
        stackPanel3.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote2, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Receipt), null, 0, null, 378.0, null));
        stackPanel3.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote2, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Place), null, 0, null, 378.0, null));
        StackPanel stackPanel4 = new StackPanel
        {
          Spacing = 16.0
        };
        stackPanel4.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote2, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Day), null, 0, null, 378.0, null));
        stackPanel4.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote2, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Day) && g.Blocks[0].Words.Current.Contains("timeline"), null, 0, null, 378.0, null));
        stackPanel4.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote2, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Photo), null, 0, null, 378.0, null));
        return DocumentGallery.ObjectsRow(new FrameworkElement[] { stackPanel3, stackPanel4 });
      }, 4.0);
      DocumentGallery.AddInkScene(list, "ink-obj-links", 1060.0, 1080.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote3 = ClickyInkSamples.LiveNote(DocumentGallery.LinksNote());
        StackPanel stackPanel5 = new StackPanel
        {
          Spacing = 16.0
        };
        stackPanel5.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote3, (ClickyInkGroup g) => g.IsMadeLink, null, 0, null, 378.0, null));
        stackPanel5.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote3, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.File) && !g.IsSupportingFiles, null, 0, null, 378.0, null));
        stackPanel5.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote3, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Letter), null, 0, null, 378.0, null));
        stackPanel5.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote3, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Clip), null, 0, null, 378.0, null));
        StackPanel stackPanel6 = new StackPanel
        {
          Spacing = 16.0
        };
        stackPanel6.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote3, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Question), null, 0, null, 378.0, null));
        stackPanel6.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote3, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Question) && g.Blocks[0].Source["options"] != null, null, 0, null, 378.0, null));
        stackPanel6.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote3, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Ask), null, 0, null, 378.0, null));
        stackPanel6.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote3, (ClickyInkGroup g) => g.IsSupportingFiles, null, 0, null, 378.0, null));
        stackPanel6.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote3, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Link) && !g.IsMadeLink, null, 0, null, 378.0, null));
        return DocumentGallery.ObjectsRow(new FrameworkElement[] { stackPanel5, stackPanel6 });
      }, 3.0);
      DocumentGallery.AddInkScene(list, "ink-obj-landing", 1060.0, 900.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote4 = ClickyInkSamples.LiveNote("# Headphones\n\n## In short\n> Sony for flights; Bose if comfort comes first {big=\"$449.99\"}\n::figure[Battery]{value=\"30 h\" delta=\"+6 h\"}\n::figure[Noise cut]{value=\"-38 dB\" delta=\"down 2 dB\"}\n::figure[Weight]{value=\"250 g\" delta=\"lightest\"}\n| Headphones | Price | ANC battery | Best for |\n|---|---|---|---|\n| **Sony WH-1000XM6** {pick} | $449.99 | 30 h | Flights and the office, the best noise cancelling we tried |\n| Bose QuietComfort Ultra | $299.95 | 24 h | Comfort on long days |\n| Sennheiser Momentum 4 {out} | $379.95 | 60 h | Battery above everything |\n::sticky[Buy from the airline store: 10% off]{color=pink}\n::sticky[Return window is 30 days]\n::sticky[Case is sold apart]{color=blue}\n:::chart[Reviewer scores]{unit=/10}\nSony WH-1000XM6: 9.1 {pick}\nBose QuietComfort Ultra: 8.4\nSennheiser Momentum 4: 7.9\n:::");
        ClickyInkLiveNote clickyInkLiveNote5 = ClickyInkSamples.LiveNote(DocumentGallery.TravelNote());
        StackPanel stackPanel7 = new StackPanel
        {
          Spacing = 16.0
        };
        stackPanel7.Children.Add(DocumentGallery.ObjectsCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote4, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Sticky), new double?(DocumentGallery.<AddInkObjectScenes>g__At|60_26(clickyInkLiveNote4, ClickyInkDocument.Kind.Sticky, 0.3)), 0, null, 378.0, null), "stickies landing (0.3 s)"));
        stackPanel7.Children.Add(DocumentGallery.ObjectsCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote4, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Table), new double?(DocumentGallery.<AddInkObjectScenes>g__At|60_26(clickyInkLiveNote4, ClickyInkDocument.Kind.Table, 1.2)), 0, null, 378.0, null), "table rows growing in, the pick's highlighter sweeping"));
        stackPanel7.Children.Add(DocumentGallery.ObjectsCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote4, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Chart), new double?(DocumentGallery.<AddInkObjectScenes>g__At|60_26(clickyInkLiveNote4, ClickyInkDocument.Kind.Chart, 1.4)), 0, null, 378.0, null), "chart bars growing"));
        StackPanel stackPanel8 = new StackPanel
        {
          Spacing = 16.0
        };
        stackPanel8.Children.Add(DocumentGallery.ObjectsCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote5, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Ticket), new double?(DocumentGallery.<AddInkObjectScenes>g__At|60_26(clickyInkLiveNote5, ClickyInkDocument.Kind.Ticket, 0.5)), 0, null, 378.0, null), "ticket landing, the route being drawn"));
        stackPanel8.Children.Add(DocumentGallery.ObjectsCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote5, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Place), new double?(DocumentGallery.<AddInkObjectScenes>g__At|60_26(clickyInkLiveNote5, ClickyInkDocument.Kind.Place, 0.5)), 0, null, 378.0, null), "the pin dropping"));
        stackPanel8.Children.Add(DocumentGallery.ObjectsCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote5, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Photo), new double?(DocumentGallery.<AddInkObjectScenes>g__At|60_26(clickyInkLiveNote5, ClickyInkDocument.Kind.Photo, 0.8)), 0, null, 378.0, null), "polaroids developing"));
        return DocumentGallery.ObjectsRow(new FrameworkElement[] { stackPanel7, stackPanel8 });
      }, 4.0);
      DocumentGallery.AddInkScene(list, "ink-obj-struck", 1060.0, 760.0, delegate
      {
        string text = "# Headphones\n\n## In short\n> Sony for flights; Bose if comfort comes first {big=\"$449.99\"}\n::figure[Battery]{value=\"30 h\" delta=\"+6 h\"}\n::figure[Noise cut]{value=\"-38 dB\" delta=\"down 2 dB\"}\n::figure[Weight]{value=\"250 g\" delta=\"lightest\"}\n| Headphones | Price | ANC battery | Best for |\n|---|---|---|---|\n| **Sony WH-1000XM6** {pick} | $449.99 | 30 h | Flights and the office, the best noise cancelling we tried |\n| Bose QuietComfort Ultra | $299.95 | 24 h | Comfort on long days |\n| Sennheiser Momentum 4 {out} | $379.95 | 60 h | Battery above everything |\n::sticky[Buy from the airline store: 10% off]{color=pink}\n::sticky[Return window is 30 days]\n::sticky[Case is sold apart]{color=blue}\n:::chart[Reviewer scores]{unit=/10}\nSony WH-1000XM6: 9.1 {pick}\nBose QuietComfort Ultra: 8.4\nSennheiser Momentum 4: 7.9\n:::";
        string text2 = "# Headphones\n\n## In short\n> Sony for flights; Bose if comfort comes first {big=\"$449.99\"}\n::figure[Battery]{value=\"30 h\" delta=\"+6 h\"}\n::figure[Noise cut]{value=\"-38 dB\" delta=\"down 2 dB\"}\n::figure[Weight]{value=\"250 g\" delta=\"lightest\"}\n| Headphones | Price | ANC battery | Best for |\n|---|---|---|---|\n| **Sony WH-1000XM6** {pick} | $449.99 | 30 h | Flights and the office, the best noise cancelling we tried |\n| Bose QuietComfort Ultra | $299.95 | 24 h | Comfort on long days |\n| Sennheiser Momentum 4 {out} | $379.95 | 60 h | Battery above everything |\n::sticky[Buy from the airline store: 10% off]{color=pink}\n::sticky[Return window is 30 days]\n::sticky[Case is sold apart]{color=blue}\n:::chart[Reviewer scores]{unit=/10}\nSony WH-1000XM6: 9.1 {pick}\nBose QuietComfort Ultra: 8.4\nSennheiser Momentum 4: 7.9\n:::".Replace("::sticky[Case is sold apart]{color=blue}\n", "").Replace("| Sennheiser Momentum 4 {out} | $379.95 | 60 h | Battery above everything |\n", "").Replace(":::chart[Reviewer scores]{unit=/10}\nSony WH-1000XM6: 9.1 {pick}\nBose QuietComfort Ultra: 8.4\nSennheiser Momentum 4: 7.9\n:::", "");
        ClickyInkLiveNote clickyInkLiveNote6 = ClickyInkSamples.LiveNote(new ValueTuple<string, double, bool>[]
        {
          new ValueTuple<string, double, bool>(text, 0.3, false),
          new ValueTuple<string, double, bool>(text2, 40.0, true)
        });
        double num = clickyInkLiveNote6.Pages[0].Blocks.First<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.Source.Kind == ClickyInkDocument.Kind.Chart).GoneAt.GetValueOrDefault(41.0) + 1.2;
        StackPanel stackPanel9 = new StackPanel
        {
          Spacing = 16.0
        };
        stackPanel9.Children.Add(DocumentGallery.ObjectsCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote6, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Chart), new double?(num), 0, null, 378.0, null), "the chart struck by the edit"));
        stackPanel9.Children.Add(DocumentGallery.ObjectsCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote6, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Table), new double?(num), 0, null, 378.0, null), "a row struck"));
        StackPanel stackPanel10 = new StackPanel
        {
          Spacing = 16.0
        };
        stackPanel10.Children.Add(DocumentGallery.ObjectsCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote6, (ClickyInkGroup g) => DocumentGallery.<AddInkObjectScenes>g__Kind|60_0(g, ClickyInkDocument.Kind.Sticky), new double?(num), 0, null, 378.0, null), "stickies after the edit"));
        return DocumentGallery.ObjectsRow(new FrameworkElement[] { stackPanel9, stackPanel10 });
      }, 2.4);
    }

    // Token: 0x060024A6 RID: 9382 RVA: 0x00636BEC File Offset: 0x00636BEC
    private static void AddInkInteractiveScenes(List<DocumentGallery.Scene> list)
    {
      DocumentGallery.AddInkScene(list, "ink-int-question", 1000.0, 330.0, delegate
      {
        FrameworkElement[] array = new FrameworkElement[2];
        array[0] = DocumentGallery.InkObjectScene(() => ClickyInkQuestionAnswers.Make(DocumentGallery.Block("# Studio launch\n## In short\n> Three things need you before the pitches go out {big=\"3\"}\n::question[Which offer should I lead with?]{options=\"Free audit | $299 site | Monthly SEO\" other=\"Something else…\" file=\"Attach a brief…\"}\n:::form[Before I write the pitches]{submit=\"Here are my details\"}\nYour studio name\nPrice: e.g. $299\nTone: Friendly | Formal | Playful\nPortfolio: folder\n:::\n:::items[Dentists to pitch]{actions=\"Draft pitch | Skip\"}\n- [Smile Studio](https://example.com) — 4.9★, no website\n- Bright Dental — 4.7★, site from 2014 {pick}\n- Harbor Family Dentistry — 4.8★, slow site\n- Old Town Dentistry {out}\n  closed for renovation\n- Riverside Smiles {done}\n:::\n:::gallery[Three logo directions]{pick=\"Use this logo\"}\noutput/logo-a.png — bold and round\noutput/logo-b.png — thin serif {pick}\noutput/jingle.mp3 — the jingle\nhttps://example.com/brand — the brand page\noutput/logo-c.png — retro {out}\n:::\n::event[Studio open day]{when=\"2026-11-14\" end=\"2026-11-15\" where=\"12 Main St, Lisbon\"}\n:::criteria[What I look for]\nRoles: designer, editor, illustrator\nArea: Lisbon | Porto\nPay: from €40/hour\n:::", ClickyInkDocument.Kind.Question), new ClickyInkActions()), "a question's extra answers");
        array[1] = DocumentGallery.InkObjectScene(delegate
        {
          FrameworkElement frameworkElement = ClickyInkQuestionAnswers.Make(DocumentGallery.Block("# Studio launch\n## In short\n> Three things need you before the pitches go out {big=\"3\"}\n::question[Which offer should I lead with?]{options=\"Free audit | $299 site | Monthly SEO\" other=\"Something else…\" file=\"Attach a brief…\"}\n:::form[Before I write the pitches]{submit=\"Here are my details\"}\nYour studio name\nPrice: e.g. $299\nTone: Friendly | Formal | Playful\nPortfolio: folder\n:::\n:::items[Dentists to pitch]{actions=\"Draft pitch | Skip\"}\n- [Smile Studio](https://example.com) — 4.9★, no website\n- Bright Dental — 4.7★, site from 2014 {pick}\n- Harbor Family Dentistry — 4.8★, slow site\n- Old Town Dentistry {out}\n  closed for renovation\n- Riverside Smiles {done}\n:::\n:::gallery[Three logo directions]{pick=\"Use this logo\"}\noutput/logo-a.png — bold and round\noutput/logo-b.png — thin serif {pick}\noutput/jingle.mp3 — the jingle\nhttps://example.com/brand — the brand page\noutput/logo-c.png — retro {out}\n:::\n::event[Studio open day]{when=\"2026-11-14\" end=\"2026-11-15\" where=\"12 Main St, Lisbon\"}\n:::criteria[What I look for]\nRoles: designer, editor, illustrator\nArea: Lisbon | Porto\nPay: from €40/hour\n:::", ClickyInkDocument.Kind.Question), new ClickyInkActions());
          ClickyInkInlineAnswerField clickyInkInlineAnswerField = DocumentGallery.FindField(frameworkElement);
          if (clickyInkInlineAnswerField == null)
          {
            return frameworkElement;
          }
          clickyInkInlineAnswerField.SetDraft("A bundle: audit then site");
          return frameworkElement;
        }, "typing an answer");
        return DocumentGallery.InteractiveRow(array);
      });
      DocumentGallery.AddInkScene(list, "ink-int-form", 1480.0, 600.0, delegate
      {
        InkObjectState.ResetAll();
        ClickyInkLiveNote clickyInkLiveNote = DocumentGallery.<AddInkInteractiveScenes>g__Note|61_12();
        ClickyInkLiveNote clickyInkLiveNote2 = DocumentGallery.<AddInkInteractiveScenes>g__Note|61_12();
        ClickyInkLiveNote clickyInkLiveNote3 = DocumentGallery.<AddInkInteractiveScenes>g__Note|61_12();
        ClickyInkNoteAction clickyInkNoteAction = ClickyInkNoteActions.Form(DocumentGallery.FindBlock(clickyInkLiveNote3, ClickyInkDocument.Kind.Form).Source, new <>z__ReadOnlySingleElementList<ValueTuple<string, string>>(new ValueTuple<string, string>("Your studio name", "Bright Pixel")), false);
        FrameworkElement[] array2 = new FrameworkElement[3];
        array2[0] = DocumentGallery.InteractiveCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote, new Func<ClickyInkGroup, bool>(DocumentGallery.<AddInkInteractiveScenes>g__IsForm|61_13), null, 0, null, 378.0, null), "empty: Send rests until something's filled in");
        array2[1] = DocumentGallery.InteractiveCaptioned(DocumentGallery.FillForm(clickyInkLiveNote2, DocumentGallery.FindBlock(clickyInkLiveNote2, ClickyInkDocument.Kind.Form)), "filled in, Friendly circled");
        int num = 2;
        ClickyInkLiveNote clickyInkLiveNote4 = clickyInkLiveNote3;
        Func<ClickyInkGroup, bool> func = new Func<ClickyInkGroup, bool>(DocumentGallery.<AddInkInteractiveScenes>g__IsForm|61_13);
        ClickyInkActions clickyInkActions = DocumentGallery.WithFeedback(null, clickyInkNoteAction);
        array2[num] = DocumentGallery.InteractiveCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote4, func, null, 0, null, 378.0, clickyInkActions), "sent: done, with what was said");
        return DocumentGallery.InteractiveRow(array2);
      });
      DocumentGallery.AddInkScene(list, "ink-int-connect", 1400.0, 900.0, delegate
      {
        InkObjectState.ResetAll();
        ClickyInkConnections shared = ClickyInkConnections.Shared;
        shared.SetApps(new <>z__ReadOnlyArray<ClickyInkConnections.App>(new ClickyInkConnections.App[]
        {
          new ClickyInkConnections.App("gmail", "Gmail", null, false, false),
          new ClickyInkConnections.App("slack", "Slack", null, false, false),
          new ClickyInkConnections.App("notion", "Notion", null, false, false),
          new ClickyInkConnections.App("github", "GitHub", null, false, false),
          new ClickyInkConnections.App("googlecalendar", "Google Calendar", null, false, true),
          new ClickyInkConnections.App("linear", "Linear", null, true, false),
          new ClickyInkConnections.App("figma", "Figma", null, false, false)
        }));
        shared.SetPhase(new ClickyInkConnections.Phase.Opening(), "slack");
        shared.SetPhase(new ClickyInkConnections.Phase.WaitingForSignIn(), "notion");
        shared.SetPhase(new ClickyInkConnections.Phase.Checking(), "github");
        shared.SetPhase(new ClickyInkConnections.Phase.Connected(), "figma");
        shared.SetPhase(new ClickyInkConnections.Phase.Failed("Couldn't open the sign-in page. Try again."), "dropbox");
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 16.0,
          Children = 
          {
            DocumentGallery.<AddInkInteractiveScenes>g__Card|61_14("Gmail", "not connected", ""),
            DocumentGallery.<AddInkInteractiveScenes>g__Card|61_14("Slack", "opening the sign-in page", ""),
            DocumentGallery.<AddInkInteractiveScenes>g__Card|61_14("Notion", "waiting for the sign-in", ""),
            DocumentGallery.<AddInkInteractiveScenes>g__Card|61_14("GitHub", "checking the connection", "")
          }
        };
        StackPanel stackPanel2 = new StackPanel
        {
          Spacing = 16.0,
          Children = 
          {
            DocumentGallery.<AddInkInteractiveScenes>g__Card|61_14("Figma", "connected: the stamp, the job told", ""),
            DocumentGallery.<AddInkInteractiveScenes>g__Card|61_14("Linear", "connected elsewhere: Carry on", ""),
            DocumentGallery.<AddInkInteractiveScenes>g__Card|61_14("Google Calendar", "its sign-in expired: Reconnect", ""),
            DocumentGallery.<AddInkInteractiveScenes>g__Card|61_14("Dropbox", "failed (not in the catalog)", " app=dropbox")
          }
        };
        return DocumentGallery.InteractiveRow(new FrameworkElement[] { stackPanel, stackPanel2 });
      });
      DocumentGallery.AddInkScene(list, "ink-int-items", 1480.0, 470.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote5 = ClickyInkSamples.LiveNote("# Studio launch\n## In short\n> Three things need you before the pitches go out {big=\"3\"}\n::question[Which offer should I lead with?]{options=\"Free audit | $299 site | Monthly SEO\" other=\"Something else…\" file=\"Attach a brief…\"}\n:::form[Before I write the pitches]{submit=\"Here are my details\"}\nYour studio name\nPrice: e.g. $299\nTone: Friendly | Formal | Playful\nPortfolio: folder\n:::\n:::items[Dentists to pitch]{actions=\"Draft pitch | Skip\"}\n- [Smile Studio](https://example.com) — 4.9★, no website\n- Bright Dental — 4.7★, site from 2014 {pick}\n- Harbor Family Dentistry — 4.8★, slow site\n- Old Town Dentistry {out}\n  closed for renovation\n- Riverside Smiles {done}\n:::\n:::gallery[Three logo directions]{pick=\"Use this logo\"}\noutput/logo-a.png — bold and round\noutput/logo-b.png — thin serif {pick}\noutput/jingle.mp3 — the jingle\nhttps://example.com/brand — the brand page\noutput/logo-c.png — retro {out}\n:::\n::event[Studio open day]{when=\"2026-11-14\" end=\"2026-11-15\" where=\"12 Main St, Lisbon\"}\n:::criteria[What I look for]\nRoles: designer, editor, illustrator\nArea: Lisbon | Porto\nPay: from €40/hour\n:::");
        ClickyInkLiveNote.Block block = DocumentGallery.FindBlock(clickyInkLiveNote5, ClickyInkDocument.Kind.Items);
        ClickyInkNoteAction clickyInkNoteAction2 = ClickyInkNoteActions.ItemAction(block.Source, block.Items[0].Source.Text, "Draft pitch");
        ClickyInkNoteAction clickyInkNoteAction3 = ClickyInkNoteActions.ItemAction(block.Source, block.Items[2].Source.Text, "Skip");
        ClickyInkLiveNote.Words words = block.Items[2].Words;
        double num2 = words.StartsAt.GetValueOrDefault() + (words.FinishedAt - words.StartsAt.GetValueOrDefault()) * 0.5;
        FrameworkElement[] array3 = new FrameworkElement[3];
        array3[0] = DocumentGallery.InteractiveCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote5, new Func<ClickyInkGroup, bool>(DocumentGallery.<AddInkInteractiveScenes>g__IsItems|61_16), null, 0, null, 378.0, null), "every row with its buttons");
        int num3 = 1;
        ClickyInkLiveNote clickyInkLiveNote6 = clickyInkLiveNote5;
        Func<ClickyInkGroup, bool> func2 = new Func<ClickyInkGroup, bool>(DocumentGallery.<AddInkInteractiveScenes>g__IsItems|61_16);
        ClickyInkActions clickyInkActions2 = DocumentGallery.WithFeedback(clickyInkNoteAction3, clickyInkNoteAction2);
        array3[num3] = DocumentGallery.InteractiveCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote6, func2, null, 0, null, 378.0, clickyInkActions2), "one asked, one on its way");
        int num4 = 2;
        FrameworkElement frameworkElement2 = DocumentGallery.InkGroupScene(clickyInkLiveNote5, new Func<ClickyInkGroup, bool>(DocumentGallery.<AddInkInteractiveScenes>g__IsItems|61_16), new double?(num2), 0, null, 378.0, null);
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(22, 1);
        defaultInterpolatedStringHandler.AppendLiteral("being written (clock ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(num2, "0.00");
        defaultInterpolatedStringHandler.AppendLiteral(")");
        array3[num4] = DocumentGallery.InteractiveCaptioned(frameworkElement2, defaultInterpolatedStringHandler.ToStringAndClear());
        return DocumentGallery.InteractiveRow(array3);
      });
      DocumentGallery.AddInkScene(list, "ink-int-quiz", 1480.0, 520.0, delegate
      {
        DocumentGallery.<>c__DisplayClass61_0 CS$<>8__locals1 = new DocumentGallery.<>c__DisplayClass61_0();
        CS$<>8__locals1.note = ClickyInkSamples.LiveNote("# Dentist outreach\n## In short\n> 6 dentists without a website {big=\"6\"}\n::question[Which offer should I lead with?]{options=\"Free audit | $299 site | Monthly SEO\" other=\"Something else…\"}\n:::form[Before I write the pitches]{submit=\"Here are my details\"}\nYour studio name\nPrice: e.g. $299\nTone: Friendly | Formal\nPortfolio: file\n:::\n::connect[Gmail]{why=\"to save the drafts\"}\n:::items[Dentists to pitch]{actions=\"Draft pitch | Skip\"}\n- [Smile Studio](https://example.com) — 4.9★, no website\n- Old Town Dentistry {out}\n  closed for renovation\n:::\n:::quiz[Drill]\nQ: They have a website guy.\nA: Offer a free audit.\nWhich call first? | The warmest lead\n:::\n:::todo[Plan]\n- [x] Day 1: send 5 pitches\nDay 2: follow up\n:::\n:::gallery[Logos]{pick=\"Use this logo\"}\noutput/logo-a.png — bold and round\noutput/logo-b.png\n:::\n::event[Call]{when=\"2026-10-20 15:00\" end=\"15:30\"}\n:::criteria[What I look for]\nArea: Lisbon, Porto\n:::");
        CS$<>8__locals1.quiz = DocumentGallery.FindBlock(CS$<>8__locals1.note, ClickyInkDocument.Kind.Quiz);
        FrameworkElement[] array4 = new FrameworkElement[3];
        array4[0] = DocumentGallery.InteractiveCaptioned(CS$<>8__locals1.<AddInkInteractiveScenes>g__Quiz|18(delegate
        {
        }), "fresh: tap to turn it over");
        array4[1] = DocumentGallery.InteractiveCaptioned(CS$<>8__locals1.<AddInkInteractiveScenes>g__Quiz|18(delegate
        {
          ClickyInkQuiz.SetForGallery(CS$<>8__locals1.quiz, new <>z__ReadOnlySingleElementList<string>(CS$<>8__locals1.quiz.Items[0].Id), new Dictionary<string, bool>());
        }), "the first turned over: Got it / Missed it");
        array4[2] = DocumentGallery.InteractiveCaptioned(CS$<>8__locals1.<AddInkInteractiveScenes>g__Quiz|18(delegate
        {
          ClickyInkLiveNote.Block quiz = CS$<>8__locals1.quiz;
          IEnumerable<string> enumerable = new <>z__ReadOnlyArray<string>(new string[]
          {
            CS$<>8__locals1.quiz.Items[0].Id,
            CS$<>8__locals1.quiz.Items[1].Id
          });
          Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
          string id = CS$<>8__locals1.quiz.Items[0].Id;
          dictionary[id] = true;
          string id2 = CS$<>8__locals1.quiz.Items[1].Id;
          dictionary[id2] = false;
          ClickyInkQuiz.SetForGallery(quiz, enumerable, dictionary);
        }), "graded: the score and Send my score");
        return DocumentGallery.InteractiveRow(array4);
      });
      DocumentGallery.AddInkScene(list, "ink-int-todo", 960.0, 420.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote7 = ClickyInkSamples.LiveNote("# Guitar\n## Plan\n:::todo[14-day guitar plan]\n- [x] Day 1: tune and learn E minor\n- [x] Day 2: G and C\n- [ ] Day 3: switch G → C in time\n  slow, 60 bpm\n- [ ] Day 4: first song\n:::\n");
        ClickyInkLiveNote clickyInkLiveNote8 = ClickyInkSamples.LiveNote("# Dentist outreach\n## In short\n> 6 dentists without a website {big=\"6\"}\n::question[Which offer should I lead with?]{options=\"Free audit | $299 site | Monthly SEO\" other=\"Something else…\"}\n:::form[Before I write the pitches]{submit=\"Here are my details\"}\nYour studio name\nPrice: e.g. $299\nTone: Friendly | Formal\nPortfolio: file\n:::\n::connect[Gmail]{why=\"to save the drafts\"}\n:::items[Dentists to pitch]{actions=\"Draft pitch | Skip\"}\n- [Smile Studio](https://example.com) — 4.9★, no website\n- Old Town Dentistry {out}\n  closed for renovation\n:::\n:::quiz[Drill]\nQ: They have a website guy.\nA: Offer a free audit.\nWhich call first? | The warmest lead\n:::\n:::todo[Plan]\n- [x] Day 1: send 5 pitches\nDay 2: follow up\n:::\n:::gallery[Logos]{pick=\"Use this logo\"}\noutput/logo-a.png — bold and round\noutput/logo-b.png\n:::\n::event[Call]{when=\"2026-10-20 15:00\" end=\"15:30\"}\n:::criteria[What I look for]\nArea: Lisbon, Porto\n:::");
        FrameworkElement[] array5 = new FrameworkElement[2];
        array5[0] = DocumentGallery.InteractiveCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote8, (ClickyInkGroup g) => g.Kind == ClickyInkDocument.Kind.Todo, null, 0, null, 378.0, null), "the Mac tests' plan");
        array5[1] = DocumentGallery.InteractiveCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote7, (ClickyInkGroup g) => g.Kind == ClickyInkDocument.Kind.Todo, null, 0, null, 378.0, null), "two of four done, a detail line");
        return DocumentGallery.InteractiveRow(array5);
      });
      DocumentGallery.AddInkScene(list, "ink-int-gallery", 960.0, 560.0, delegate
      {
        DocumentGallery.<>c__DisplayClass61_1 CS$<>8__locals2 = new DocumentGallery.<>c__DisplayClass61_1();
        InkObjectState.ResetAll();
        ClickyInkLiveNote clickyInkLiveNote9 = ClickyInkSamples.LiveNote("# Studio launch\n## In short\n> Three things need you before the pitches go out {big=\"3\"}\n::question[Which offer should I lead with?]{options=\"Free audit | $299 site | Monthly SEO\" other=\"Something else…\" file=\"Attach a brief…\"}\n:::form[Before I write the pitches]{submit=\"Here are my details\"}\nYour studio name\nPrice: e.g. $299\nTone: Friendly | Formal | Playful\nPortfolio: folder\n:::\n:::items[Dentists to pitch]{actions=\"Draft pitch | Skip\"}\n- [Smile Studio](https://example.com) — 4.9★, no website\n- Bright Dental — 4.7★, site from 2014 {pick}\n- Harbor Family Dentistry — 4.8★, slow site\n- Old Town Dentistry {out}\n  closed for renovation\n- Riverside Smiles {done}\n:::\n:::gallery[Three logo directions]{pick=\"Use this logo\"}\noutput/logo-a.png — bold and round\noutput/logo-b.png — thin serif {pick}\noutput/jingle.mp3 — the jingle\nhttps://example.com/brand — the brand page\noutput/logo-c.png — retro {out}\n:::\n::event[Studio open day]{when=\"2026-11-14\" end=\"2026-11-15\" where=\"12 Main St, Lisbon\"}\n:::criteria[What I look for]\nRoles: designer, editor, illustrator\nArea: Lisbon | Porto\nPay: from €40/hour\n:::");
        CS$<>8__locals2.folder = Path.Combine(DocumentGallery.SampleDirectory, "ink");
        Directory.CreateDirectory(Path.Combine(CS$<>8__locals2.folder, "output"));
        DocumentGallery.WriteSamplePng(Path.Combine(CS$<>8__locals2.folder, "output", "logo-a.png"), new ValueTuple<int, int, int>(45, 91, 227), new ValueTuple<int, int, int>(255, 199, 225), 240);
        DocumentGallery.WriteSamplePng(Path.Combine(CS$<>8__locals2.folder, "output", "logo-b.png"), new ValueTuple<int, int, int>(31, 138, 76), new ValueTuple<int, int, int>(254, 238, 199), 240);
        DocumentGallery.WriteSamplePng(Path.Combine(CS$<>8__locals2.folder, "output", "logo-c.png"), new ValueTuple<int, int, int>(194, 65, 12), new ValueTuple<int, int, int>(215, 229, 255), 240);
        ClickyInkNoteAction clickyInkNoteAction4 = ClickyInkNoteActions.Pick(DocumentGallery.FindBlock(clickyInkLiveNote9, ClickyInkDocument.Kind.Gallery).Source, "thin serif");
        ClickyInkLiveNote clickyInkLiveNote10 = ClickyInkSamples.LiveNote("# Studio launch\n## In short\n> Three things need you before the pitches go out {big=\"3\"}\n::question[Which offer should I lead with?]{options=\"Free audit | $299 site | Monthly SEO\" other=\"Something else…\" file=\"Attach a brief…\"}\n:::form[Before I write the pitches]{submit=\"Here are my details\"}\nYour studio name\nPrice: e.g. $299\nTone: Friendly | Formal | Playful\nPortfolio: folder\n:::\n:::items[Dentists to pitch]{actions=\"Draft pitch | Skip\"}\n- [Smile Studio](https://example.com) — 4.9★, no website\n- Bright Dental — 4.7★, site from 2014 {pick}\n- Harbor Family Dentistry — 4.8★, slow site\n- Old Town Dentistry {out}\n  closed for renovation\n- Riverside Smiles {done}\n:::\n:::gallery[Three logo directions]{pick=\"Use this logo\"}\noutput/logo-a.png — bold and round\noutput/logo-b.png — thin serif {pick}\noutput/jingle.mp3 — the jingle\nhttps://example.com/brand — the brand page\noutput/logo-c.png — retro {out}\n:::\n::event[Studio open day]{when=\"2026-11-14\" end=\"2026-11-15\" where=\"12 Main St, Lisbon\"}\n:::criteria[What I look for]\nRoles: designer, editor, illustrator\nArea: Lisbon | Porto\nPay: from €40/hour\n:::");
        FrameworkElement[] array6 = new FrameworkElement[2];
        int num5 = 0;
        ClickyInkLiveNote clickyInkLiveNote11 = clickyInkLiveNote9;
        Func<ClickyInkGroup, bool> func3 = new Func<ClickyInkGroup, bool>(DocumentGallery.<AddInkInteractiveScenes>g__IsGallery|61_24);
        ClickyInkActions clickyInkActions3 = CS$<>8__locals2.<AddInkInteractiveScenes>g__Actions|25(null);
        array6[num5] = DocumentGallery.InteractiveCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote11, func3, null, 0, null, 378.0, clickyInkActions3), "the agent's pick ringed; a sound, a page, one ruled out");
        int num6 = 1;
        ClickyInkLiveNote clickyInkLiveNote12 = clickyInkLiveNote10;
        Func<ClickyInkGroup, bool> func4 = new Func<ClickyInkGroup, bool>(DocumentGallery.<AddInkInteractiveScenes>g__IsGallery|61_24);
        clickyInkActions3 = CS$<>8__locals2.<AddInkInteractiveScenes>g__Actions|25(DocumentGallery.WithFeedback(null, clickyInkNoteAction4).Feedback);
        array6[num6] = DocumentGallery.InteractiveCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote12, func4, null, 0, null, 378.0, clickyInkActions3), "after Pick this: done, what was said");
        return DocumentGallery.InteractiveRow(array6);
      });
      DocumentGallery.AddInkScene(list, "ink-int-event", 960.0, 300.0, delegate
      {
        FrameworkElement[] array7 = new FrameworkElement[2];
        array7[0] = DocumentGallery.InteractiveCaptioned(DocumentGallery.InkGroupScene(ClickyInkSamples.LiveNote("# Dentist outreach\n## In short\n> 6 dentists without a website {big=\"6\"}\n::question[Which offer should I lead with?]{options=\"Free audit | $299 site | Monthly SEO\" other=\"Something else…\"}\n:::form[Before I write the pitches]{submit=\"Here are my details\"}\nYour studio name\nPrice: e.g. $299\nTone: Friendly | Formal\nPortfolio: file\n:::\n::connect[Gmail]{why=\"to save the drafts\"}\n:::items[Dentists to pitch]{actions=\"Draft pitch | Skip\"}\n- [Smile Studio](https://example.com) — 4.9★, no website\n- Old Town Dentistry {out}\n  closed for renovation\n:::\n:::quiz[Drill]\nQ: They have a website guy.\nA: Offer a free audit.\nWhich call first? | The warmest lead\n:::\n:::todo[Plan]\n- [x] Day 1: send 5 pitches\nDay 2: follow up\n:::\n:::gallery[Logos]{pick=\"Use this logo\"}\noutput/logo-a.png — bold and round\noutput/logo-b.png\n:::\n::event[Call]{when=\"2026-10-20 15:00\" end=\"15:30\"}\n:::criteria[What I look for]\nArea: Lisbon, Porto\n:::"), (ClickyInkGroup g) => g.Kind == ClickyInkDocument.Kind.Event, null, 0, null, 378.0, null), "a call, its times");
        array7[1] = DocumentGallery.InteractiveCaptioned(DocumentGallery.InkGroupScene(ClickyInkSamples.LiveNote("# Studio launch\n## In short\n> Three things need you before the pitches go out {big=\"3\"}\n::question[Which offer should I lead with?]{options=\"Free audit | $299 site | Monthly SEO\" other=\"Something else…\" file=\"Attach a brief…\"}\n:::form[Before I write the pitches]{submit=\"Here are my details\"}\nYour studio name\nPrice: e.g. $299\nTone: Friendly | Formal | Playful\nPortfolio: folder\n:::\n:::items[Dentists to pitch]{actions=\"Draft pitch | Skip\"}\n- [Smile Studio](https://example.com) — 4.9★, no website\n- Bright Dental — 4.7★, site from 2014 {pick}\n- Harbor Family Dentistry — 4.8★, slow site\n- Old Town Dentistry {out}\n  closed for renovation\n- Riverside Smiles {done}\n:::\n:::gallery[Three logo directions]{pick=\"Use this logo\"}\noutput/logo-a.png — bold and round\noutput/logo-b.png — thin serif {pick}\noutput/jingle.mp3 — the jingle\nhttps://example.com/brand — the brand page\noutput/logo-c.png — retro {out}\n:::\n::event[Studio open day]{when=\"2026-11-14\" end=\"2026-11-15\" where=\"12 Main St, Lisbon\"}\n:::criteria[What I look for]\nRoles: designer, editor, illustrator\nArea: Lisbon | Porto\nPay: from €40/hour\n:::"), (ClickyInkGroup g) => g.Kind == ClickyInkDocument.Kind.Event, null, 0, null, 378.0, null), "all day, two days, a place");
        return DocumentGallery.InteractiveRow(array7);
      });
      DocumentGallery.AddInkScene(list, "ink-int-criteria", 960.0, 340.0, delegate
      {
        InkObjectState.ResetAll();
        FrameworkElement frameworkElement3 = DocumentGallery.InkGroupScene(ClickyInkSamples.LiveNote("# Studio launch\n## In short\n> Three things need you before the pitches go out {big=\"3\"}\n::question[Which offer should I lead with?]{options=\"Free audit | $299 site | Monthly SEO\" other=\"Something else…\" file=\"Attach a brief…\"}\n:::form[Before I write the pitches]{submit=\"Here are my details\"}\nYour studio name\nPrice: e.g. $299\nTone: Friendly | Formal | Playful\nPortfolio: folder\n:::\n:::items[Dentists to pitch]{actions=\"Draft pitch | Skip\"}\n- [Smile Studio](https://example.com) — 4.9★, no website\n- Bright Dental — 4.7★, site from 2014 {pick}\n- Harbor Family Dentistry — 4.8★, slow site\n- Old Town Dentistry {out}\n  closed for renovation\n- Riverside Smiles {done}\n:::\n:::gallery[Three logo directions]{pick=\"Use this logo\"}\noutput/logo-a.png — bold and round\noutput/logo-b.png — thin serif {pick}\noutput/jingle.mp3 — the jingle\nhttps://example.com/brand — the brand page\noutput/logo-c.png — retro {out}\n:::\n::event[Studio open day]{when=\"2026-11-14\" end=\"2026-11-15\" where=\"12 Main St, Lisbon\"}\n:::criteria[What I look for]\nRoles: designer, editor, illustrator\nArea: Lisbon | Porto\nPay: from €40/hour\n:::"), new Func<ClickyInkGroup, bool>(DocumentGallery.<AddInkInteractiveScenes>g__IsCriteria|61_29), null, 0, null, 378.0, null);
        ClickyInkLiveNote clickyInkLiveNote13 = ClickyInkSamples.LiveNote("# Studio launch\n## In short\n> Three things need you before the pitches go out {big=\"3\"}\n::question[Which offer should I lead with?]{options=\"Free audit | $299 site | Monthly SEO\" other=\"Something else…\" file=\"Attach a brief…\"}\n:::form[Before I write the pitches]{submit=\"Here are my details\"}\nYour studio name\nPrice: e.g. $299\nTone: Friendly | Formal | Playful\nPortfolio: folder\n:::\n:::items[Dentists to pitch]{actions=\"Draft pitch | Skip\"}\n- [Smile Studio](https://example.com) — 4.9★, no website\n- Bright Dental — 4.7★, site from 2014 {pick}\n- Harbor Family Dentistry — 4.8★, slow site\n- Old Town Dentistry {out}\n  closed for renovation\n- Riverside Smiles {done}\n:::\n:::gallery[Three logo directions]{pick=\"Use this logo\"}\noutput/logo-a.png — bold and round\noutput/logo-b.png — thin serif {pick}\noutput/jingle.mp3 — the jingle\nhttps://example.com/brand — the brand page\noutput/logo-c.png — retro {out}\n:::\n::event[Studio open day]{when=\"2026-11-14\" end=\"2026-11-15\" where=\"12 Main St, Lisbon\"}\n:::criteria[What I look for]\nRoles: designer, editor, illustrator\nArea: Lisbon | Porto\nPay: from €40/hour\n:::".Replace("What I look for", "What I look for "));
        ClickyInkCriteria.SetEditingForGallery(DocumentGallery.FindBlock(clickyInkLiveNote13, ClickyInkDocument.Kind.Criteria));
        return DocumentGallery.InteractiveRow(new FrameworkElement[]
        {
          DocumentGallery.InteractiveCaptioned(frameworkElement3, "read: values stamped"),
          DocumentGallery.InteractiveCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote13, new Func<ClickyInkGroup, bool>(DocumentGallery.<AddInkInteractiveScenes>g__IsCriteria|61_29), null, 0, null, 378.0, null), "Edit: a blank for each")
        });
      });
      DocumentGallery.AddInkScene(list, "ink-int-review", 1400.0, 1500.0, delegate
      {
        DocumentGallery.<>c__DisplayClass61_2 CS$<>8__locals3 = new DocumentGallery.<>c__DisplayClass61_2();
        ClickyInkLiveNote clickyInkLiveNote14 = ClickyInkSamples.LiveNote("# Cleanup\n## Your move\n:::review[Delete these 8 old installers?]{go=\"Delete the ticked installers\" go_label=\"Delete\" skip=\"Keep them all\" skip_label=\"Keep all\"}\n- [x] /tmp/clicky-review-test/Zoom-6.2.4.dmg — 212 MB, from 2023\n- [x] ~/Downloads/old installers/Figma-116.5.dmg — 180 MB\n- [ ] ~/Downloads/IMG_2041 (1).HEIC — a duplicate photo\n- [x] Slack-4.33.dmg — superseded\n- [ ] Notion-2.1.exe — 90 MB {new}\n- [x] Docker.dmg — 640 MB {done}\n- [ ] Spotify.dmg {out}\n  in use: it couldn't be moved\n- [ ] Arc-1.2.dmg — 210 MB\n:::\n:::review[Unfollow these?]{go=\"Unfollow these on Instagram\" go_label=\"Unfollow\" skip=\"Keep them all for now\"}\n- [ ] @old_brand_account — hasn't posted since 2023\n- [x] @spam_giveaways — a giveaway bot\n- [x] @deals_daily — 40 posts a day\n:::");
        CS$<>8__locals3.reviews = clickyInkLiveNote14.Pages[0].Blocks.Where<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.Source.Kind == ClickyInkDocument.Kind.Review).ToList<ClickyInkLiveNote.Block>();
        ClickyInkDocument.Block source = CS$<>8__locals3.reviews[1].Source;
        ClickyInkDocument.Block block2 = source;
        IReadOnlyList<string> readOnlyList = ClickyInkReviewRules.ItemsToSend(source, Array.Empty<string>());
        IEnumerable<ClickyInkDocument.Item> items = source.Items;
        Func<ClickyInkDocument.Item, bool> func5;
        if ((func5 = DocumentGallery.<>O.<9>__IsOpen) == null)
        {
          func5 = (DocumentGallery.<>O.<9>__IsOpen = new Func<ClickyInkDocument.Item, bool>(ClickyInkReviewRules.IsOpen));
        }
        ClickyInkNoteAction clickyInkNoteAction5 = ClickyInkNoteActions.Review(block2, readOnlyList, items.Count<ClickyInkDocument.Item>(func5));
        ClickyInkNoteAction clickyInkNoteAction6 = ClickyInkNoteActions.SkipReview(source);
        ClickyInkLiveNote clickyInkLiveNote15 = ClickyInkSamples.LiveNote("# Cleanup\n## Your move\n:::review[Unfollow these?]{go=\"Unfollow\"}\n- [x] @spam_giveaways {done}\n- [x] @deals_daily {done}\n:::\n");
        StackPanel stackPanel3 = new StackPanel();
        stackPanel3.Spacing = 16.0;
        stackPanel3.Children.Add(DocumentGallery.InteractiveCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote14, new Func<ClickyInkGroup, bool>(CS$<>8__locals3.<AddInkInteractiveScenes>g__IsShort|32), null, 0, null, 378.0, null), "waiting on you: the go-ahead and Not now"));
        UIElementCollection children = stackPanel3.Children;
        ClickyInkLiveNote clickyInkLiveNote16 = clickyInkLiveNote14;
        Func<ClickyInkGroup, bool> func6 = new Func<ClickyInkGroup, bool>(CS$<>8__locals3.<AddInkInteractiveScenes>g__IsShort|32);
        ClickyInkActions clickyInkActions4 = DocumentGallery.WithFeedback(clickyInkNoteAction5, null);
        children.Add(DocumentGallery.InteractiveCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote16, func6, null, 0, null, 378.0, clickyInkActions4), "on its way: boxes rest, the slip in the margin"));
        UIElementCollection children2 = stackPanel3.Children;
        ClickyInkLiveNote clickyInkLiveNote17 = clickyInkLiveNote14;
        Func<ClickyInkGroup, bool> func7 = new Func<ClickyInkGroup, bool>(CS$<>8__locals3.<AddInkInteractiveScenes>g__IsShort|32);
        clickyInkActions4 = DocumentGallery.WithFeedback(null, clickyInkNoteAction6);
        children2.Add(DocumentGallery.InteractiveCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote17, func7, null, 0, null, 378.0, clickyInkActions4), "Keep them all: skipped"));
        stackPanel3.Children.Add(DocumentGallery.InteractiveCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote15, (ClickyInkGroup g) => g.Kind == ClickyInkDocument.Kind.Review, null, 0, null, 378.0, null), "all done"));
        StackPanel stackPanel4 = stackPanel3;
        return DocumentGallery.InteractiveRow(new FrameworkElement[]
        {
          DocumentGallery.InteractiveCaptioned(DocumentGallery.InkGroupScene(clickyInkLiveNote14, new Func<ClickyInkGroup, bool>(CS$<>8__locals3.<AddInkInteractiveScenes>g__IsLong|31), null, 0, null, 378.0, null), "a long review: one ruled line each, paths cut in the middle"),
          stackPanel4
        });
      });
    }

    // Token: 0x060024A7 RID: 9383 RVA: 0x00636E54 File Offset: 0x00636E54
    private static void AddInkDashboardScenes(List<DocumentGallery.Scene> list)
    {
      DocumentGallery.AddInkScene(list, "ink-dash-rest", 1500.0, 900.0, () => DocumentGallery.DashPinned(delegate
      {
        ClickyInkLiveNote clickyInkLiveNote = ClickyInkSamples.DashboardRuns();
        return DocumentGallery.DashColumns(clickyInkLiveNote, clickyInkLiveNote.FinishedClock + 30.0, "after run 6, at rest");
      }));
      DocumentGallery.AddInkScene(list, "ink-dash-before", 1500.0, 900.0, () => DocumentGallery.DashPinned(delegate
      {
        ClickyInkLiveNote clickyInkLiveNote2 = ClickyInkSamples.DashboardRuns();
        return DocumentGallery.DashColumns(clickyInkLiveNote2, DocumentGallery.DashChangeClock(clickyInkLiveNote2) - 2.0, "run 5, before run 6 lands");
      }));
      DocumentGallery.AddInkScene(list, "ink-dash-change", 1500.0, 900.0, () => DocumentGallery.DashPinned(delegate
      {
        ClickyInkLiveNote clickyInkLiveNote3 = ClickyInkSamples.DashboardRuns();
        double num = DocumentGallery.DashChangeClock(clickyInkLiveNote3) + 0.55;
        double num2 = num;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(80, 1);
        defaultInterpolatedStringHandler.AppendLiteral("run 6 landing (clock ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(num, "0.00");
        defaultInterpolatedStringHandler.AppendLiteral("): numbers ticking, new point drawing in, light recolouring");
        return DocumentGallery.DashColumns(clickyInkLiveNote3, num2, defaultInterpolatedStringHandler.ToStringAndClear());
      }));
      DocumentGallery.AddInkScene(list, "ink-dash-change-late", 1500.0, 900.0, () => DocumentGallery.DashPinned(delegate
      {
        ClickyInkLiveNote clickyInkLiveNote4 = ClickyInkSamples.DashboardRuns();
        double num3 = DocumentGallery.DashChangeClock(clickyInkLiveNote4) + 1.25;
        double num4 = num3;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(82, 1);
        defaultInterpolatedStringHandler2.AppendLiteral("run 6 just landed (clock ");
        defaultInterpolatedStringHandler2.AppendFormatted<double>(num3, "0.00");
        defaultInterpolatedStringHandler2.AppendLiteral("): highlighter under the new numbers, the old ones struck");
        return DocumentGallery.DashColumns(clickyInkLiveNote4, num4, defaultInterpolatedStringHandler2.ToStringAndClear());
      }));
      DocumentGallery.AddInkScene(list, "ink-dash-missing", 1500.0, 900.0, () => DocumentGallery.DashPinned(delegate
      {
        ClickyInkLiveNote clickyInkLiveNote5 = ClickyInkSamples.LiveNote("# Broken board\n## Today\n::spark[Unreadable series]{values=\"a, b, c\"}\n::spark[No series]\n::status[Mystery light]{state=purple}\n::progress[Unknown progress]{value=soon}\n::tally[Nothing counted]\n::streak[No days]\n::countdown[Someday]{to=\"whenever\"}\n::countdown[Agent's own words]{left=\"3 days\"}\n:::pipeline[Empty pipeline]\n:::\n::run\n:::tiles\n::figure[Blank]{value=}\n::spark[Empty]{values=\"\"}\n::progress[Odd]{value=\"?\"}\n:::");
        return DocumentGallery.DashColumns(clickyInkLiveNote5, clickyInkLiveNote5.FinishedClock + 30.0, "values that can't be read: labels and empty lines, never markup");
      }));
      DocumentGallery.AddInkScene(list, "ink-dash-quiet-run", 1500.0, 900.0, () => DocumentGallery.DashPinned(delegate
      {
        ClickyInkLiveNote clickyInkLiveNote6 = ClickyInkSamples.LiveNote("# Competitor watch\n## Today\n::run[Morning scan]{n=8 at=\"Tue Oct 13, 7:02\"}\n::quiet[Nothing new since yesterday]{checked=14 at=\"7:02\" runs=3}\n::delta[MRR]{value=\"$18.4k\" was=\"$17.9k\"}\n::spark[Price of Competitor C]{values=\"49, 49, 52, 55\" unit=$ delta=\"+3\" good=down}\n::figure[Load time]{value=\"1.8s\" delta=\"-0.3s\" good=down}\n::figure[Visa]{value=\"$6\" delta=\"Indian passport\"}");
        double num5 = clickyInkLiveNote6.FinishedClock + 30.0;
        List<ClickyInkRoutineControl> list2 = new List<ClickyInkRoutineControl>
        {
          new ClickyInkRoutineControl("r1", "Morning scan", 1440, false, false, false, DocumentGallery.DashboardNow.AddHours(22.0), 7),
          new ClickyInkRoutineControl("r2", "Price check", 240, true, true, false, DocumentGallery.DashboardNow.AddHours(3.0), 1)
        };
        ClickyInkActions clickyInkActions = new ClickyInkActions();
        clickyInkActions.Routines = list2;
        clickyInkActions.RunRoutineNow = (string _) => true;
        ClickyInkActions clickyInkActions2 = clickyInkActions;
        ClickyInkRunStamp.SetEditing(clickyInkLiveNote6.Pages[0].Blocks.First<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.Source.Kind == ClickyInkDocument.Kind.Run).Id, true);
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 14.0
        };
        stackPanel.Children.Add(DocumentGallery.DashCaption("the run line with routines, Edit open"));
        UIElementCollection children = stackPanel.Children;
        ClickyInkLiveNote clickyInkLiveNote7 = clickyInkLiveNote6;
        Func<ClickyInkGroup, bool> func = (ClickyInkGroup g) => g.Kind == ClickyInkDocument.Kind.Run;
        double? num6 = new double?(num5);
        int num7 = 0;
        ClickyInkActions clickyInkActions3 = clickyInkActions2;
        children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote7, func, num6, num7, null, 378.0, clickyInkActions3));
        stackPanel.Children.Add(DocumentGallery.DashCaption("the run line on a dock window's preview (no Edit)"));
        stackPanel.Children.Add(DocumentGallery.DashPreviewGroup(clickyInkLiveNote6, (ClickyInkGroup g) => g.Kind == ClickyInkDocument.Kind.Run, num5, clickyInkActions2));
        StackPanel stackPanel2 = new StackPanel
        {
          Spacing = 14.0
        };
        stackPanel2.Children.Add(DocumentGallery.DashCaption("a quiet run; changes with good=down; words in a delta"));
        ClickyInkDocument.Kind[] array = new ClickyInkDocument.Kind[]
        {
          ClickyInkDocument.Kind.Quiet,
          ClickyInkDocument.Kind.Spark
        };
        for (int i = 0; i < array.Length; i++)
        {
          ClickyInkDocument.Kind kind = array[i];
          stackPanel2.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote6, (ClickyInkGroup g) => g.Kind == kind, new double?(num5), 0, null, 378.0, null));
        }
        stackPanel2.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote6, (ClickyInkGroup g) => g.Kind == ClickyInkDocument.Kind.Figure, new double?(num5), 0, null, 378.0, null));
        stackPanel2.Children.Add(DocumentGallery.DashCaption("the quiet stamp landing (clock + 0.2 s)"));
        ClickyInkLiveNote.Block block = clickyInkLiveNote6.Pages[0].Blocks.First<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.Source.Kind == ClickyInkDocument.Kind.Quiet);
        stackPanel2.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote6, (ClickyInkGroup g) => g.Kind == ClickyInkDocument.Kind.Quiet, new double?(block.LandsAt + 0.2), 0, null, 378.0, null));
        return new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 24.0,
          Margin = new Thickness(24.0),
          Children = { stackPanel, stackPanel2 }
        };
      }));
      DocumentGallery.AddInkScene(list, "ink-dash-tiles", 1500.0, 900.0, () => DocumentGallery.DashPinned(delegate
      {
        ClickyInkLiveNote clickyInkLiveNote8 = ClickyInkSamples.DashboardRuns();
        double at = clickyInkLiveNote8.FinishedClock + 30.0;
        ClickyInkLiveNote.Page page = clickyInkLiveNote8.Pages[0];
        ClickyInkGroup clickyInkGroup = ClickyInkGroup.Groups(page, at).First<ClickyInkGroup>((ClickyInkGroup g) => g.Kind == ClickyInkDocument.Kind.Tiles);
        ClickyInkStyle clickyInkStyle = new ClickyInkStyle(ClickyHomeSpreadPaper.Note.Ink, ClickyHomeSpreadPaper.Note.Quiet, new Color?(ClickyHomeSpreadPaper.Note.Deep), clickyInkLiveNote8.LatestEdit(at));
        List<ClickyInkLiveNote.Block> list3 = clickyInkGroup.Blocks.Select<ClickyInkLiveNote.Block, ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.DashboardResolved(at)).ToList<ClickyInkLiveNote.Block>();
        StackPanel stackPanel3 = new StackPanel
        {
          Spacing = 14.0
        };
        foreach (ClickyInkTiles.Look look in Enum.GetValues<ClickyInkTiles.Look>())
        {
          UIElementCollection children2 = stackPanel3.Children;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(6, 1);
          defaultInterpolatedStringHandler3.AppendLiteral("look: ");
          defaultInterpolatedStringHandler3.AppendFormatted<ClickyInkTiles.Look>(look);
          children2.Add(DocumentGallery.DashCaption(defaultInterpolatedStringHandler3.ToStringAndClear()));
          UIElementCollection children3 = stackPanel3.Children;
          List<ClickyInkLiveNote.Block> list4 = list3.Take<ClickyInkLiveNote.Block>(4).ToList<ClickyInkLiveNote.Block>();
          double at2 = at;
          ClickyInkStyle clickyInkStyle2 = clickyInkStyle;
          double num8 = 600.0;
          ClickyInkTiles.Look look2 = look;
          children3.Add(DocumentGallery.DashOnPaper(ClickyInkTiles.Create(list4, at2, clickyInkStyle2, num8, null, look2), 600.0));
        }
        StackPanel stackPanel4 = new StackPanel
        {
          Spacing = 14.0
        };
        stackPanel4.Children.Add(DocumentGallery.DashCaption("a dock window's preview: one row, tighter"));
        ClickyInkEnvironment clickyInkEnvironment = ClickyInkEnvironment.Default.<Clone>$();
        clickyInkEnvironment.IsWindowPreview = true;
        clickyInkEnvironment.ContentWidth = 560.0;
        using (ClickyInkEnvironment.Use(clickyInkEnvironment))
        {
          stackPanel4.Children.Add(DocumentGallery.DashOnPaper(ClickyInkTiles.Create(list3.Take<ClickyInkLiveNote.Block>(3).ToList<ClickyInkLiveNote.Block>(), at, clickyInkStyle, 560.0, null, ClickyInkTiles.Look.Ruled), 560.0));
        }
        ClickyInkLiveNote clickyInkLiveNote9 = ClickyInkSamples.LiveNote("# Competitor watch\n## Today\n::run[Morning scan]{n=8 at=\"Tue Oct 13, 7:02\"}\n::quiet[Nothing new since yesterday]{checked=14 at=\"7:02\" runs=3}\n::delta[MRR]{value=\"$18.4k\" was=\"$17.9k\"}\n::spark[Price of Competitor C]{values=\"49, 49, 52, 55\" unit=$ delta=\"+3\" good=down}\n::figure[Load time]{value=\"1.8s\" delta=\"-0.3s\" good=down}\n::figure[Visa]{value=\"$6\" delta=\"Indian passport\"}");
        double quietAt = clickyInkLiveNote9.FinishedClock + 30.0;
        List<ClickyInkLiveNote.Block> list5 = (from b in clickyInkLiveNote9.Pages[0].Blocks.Where<ClickyInkLiveNote.Block>(delegate(ClickyInkLiveNote.Block b)
          {
            ClickyInkDocument.Kind kind2 = b.Source.Kind;
            return kind2 == ClickyInkDocument.Kind.Figure || kind2 == ClickyInkDocument.Kind.Spark;
          })
          select b.DashboardResolved(quietAt)).ToList<ClickyInkLiveNote.Block>();
        ClickyInkStyle clickyInkStyle3 = new ClickyInkStyle(ClickyHomeSpreadPaper.Note.Ink, ClickyHomeSpreadPaper.Note.Quiet, new Color?(ClickyHomeSpreadPaper.Note.Deep), clickyInkLiveNote9.LatestEdit(quietAt));
        stackPanel4.Children.Add(DocumentGallery.DashCaption("narrow tiles on a preview: a change too wide goes under its number"));
        ClickyInkEnvironment clickyInkEnvironment2 = ClickyInkEnvironment.Default.<Clone>$();
        clickyInkEnvironment2.IsWindowPreview = true;
        clickyInkEnvironment2.ContentWidth = 330.0;
        using (ClickyInkEnvironment.Use(clickyInkEnvironment2))
        {
          stackPanel4.Children.Add(DocumentGallery.DashOnPaper(ClickyInkTiles.Create(list5.Skip<ClickyInkLiveNote.Block>(1).ToList<ClickyInkLiveNote.Block>(), quietAt, clickyInkStyle3, 330.0, null, ClickyInkTiles.Look.Ruled), 330.0));
        }
        stackPanel4.Children.Add(DocumentGallery.DashCaption("a whole strip being ruled (first tile landing)"));
        double num9 = clickyInkGroup.LandsAt + 0.3;
        ClickyInkGroup clickyInkGroup2 = ClickyInkGroup.Groups(page, num9).First<ClickyInkGroup>((ClickyInkGroup g) => g.Kind == ClickyInkDocument.Kind.Tiles);
        ClickyInkStyle clickyInkStyle4 = new ClickyInkStyle(ClickyHomeSpreadPaper.Note.Ink, ClickyHomeSpreadPaper.Note.Quiet, new Color?(ClickyHomeSpreadPaper.Note.Deep), clickyInkLiveNote8.LatestEdit(num9));
        stackPanel4.Children.Add(DocumentGallery.DashOnPaper(ClickyInkTiles.Create(clickyInkGroup2.Blocks, num9, clickyInkStyle4, 560.0, null, ClickyInkTiles.Look.Ruled), 560.0));
        return new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 24.0,
          Margin = new Thickness(24.0),
          Children = { stackPanel3, stackPanel4 }
        };
      }));
    }

    // Token: 0x060024A8 RID: 9384 RVA: 0x00637008 File Offset: 0x00637008
    private static void AddInkFormScenes(List<DocumentGallery.Scene> list)
    {
      DocumentGallery.AddInkScene(list, "ink-sheet-espresso", 1460.0, 900.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote = ClickyInkSamples.LiveNote("# Espresso pick\n\n## In short\n> Bambino Plus; budget $120 more for a grinder {big=\"$399\" note=\"best milk under $500\"}\n- Auto milk frothing that actually works\n- Heats up in 3 seconds\n::sticky[It needs a grinder: no beans pre-ground]{color=pink}\n::photo[Breville Bambino Plus]{query=\"Breville Bambino Plus espresso machine\"}\n\n## Compare\n| Machine | Price | Milk |\n|---|---|---|\n| **Bambino Plus** | $399 | auto |\n| Gaggia Classic Pro | $449 | manual |\n- De'Longhi Dedica {out}\n  steam wand too weak in 3 reviews\n:::chart[Reviewer scores]{unit=/10}\nBambino Plus: 9.1 {pick}\nGaggia Classic Pro: 8.4\n:::\n\n## Next\n::ask[Find a grinder to match]{say=\"Find the best grinder under $150 to pair with the Bambino Plus\"}\n::question[Mostly milk drinks?]{yes=\"Mostly lattes, keep the Bambino\" no=\"Mostly espresso, reconsider the Gaggia\"}\n::link[Wirecutter: espresso machines]{url=https://www.nytimes.com/wirecutter/reviews/best-espresso-machine/}");
        ClickyInkLiveNote.Words words = clickyInkLiveNote.Pages[0].Blocks.First<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.Source.Kind == ClickyInkDocument.Kind.List).Items[0].Words;
        double num = words.StartsAt.GetValueOrDefault() + (words.FinishedAt - words.StartsAt.GetValueOrDefault()) * 0.55;
        StackPanel stackPanel = new StackPanel();
        stackPanel.Orientation = Orientation.Horizontal;
        stackPanel.Spacing = 30.0;
        stackPanel.Margin = new Thickness(30.0);
        UIElementCollection children = stackPanel.Children;
        ClickyInkLiveNote clickyInkLiveNote2 = clickyInkLiveNote;
        double num2 = num;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 1);
        defaultInterpolatedStringHandler.AppendLiteral("writing the list (clock ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(num, "0.00");
        defaultInterpolatedStringHandler.AppendLiteral(")");
        children.Add(DocumentGallery.Sheet(clickyInkLiveNote2, num2, defaultInterpolatedStringHandler.ToStringAndClear(), null, null, null));
        stackPanel.Children.Add(DocumentGallery.Sheet(clickyInkLiveNote, clickyInkLiveNote.FinishedClock + 30.0, "finished", null, null, null));
        stackPanel.Children.Add(DocumentGallery.Sheet(clickyInkLiveNote, clickyInkLiveNote.FinishedClock + 30.0, "finished, sky", new ClickyHomeSpreadPaper?(ClickyHomeSpreadPaper.Of(ClickyPaperStock.Sky)), null, null));
        return stackPanel;
      });
      DocumentGallery.AddInkScene(list, "ink-sheet-edit", 1460.0, 760.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote3 = ClickyInkSamples.LiveNote(ClickyInkSamples.DeskPickVersions);
        double editStart = ((clickyInkLiveNote3.EditStarts.Count > 0) ? clickyInkLiveNote3.EditStarts[0] : 30.0);
        ClickyInkLiveNote.Block answer = clickyInkLiveNote3.Pages[0].Blocks[0];
        double num3 = DocumentGallery.MidWriting(clickyInkLiveNote3, (ClickyInkLiveNote.PenFocus f) => f.PlaceID == answer.Id && f.At >= editStart, 0.6);
        StackPanel stackPanel2 = new StackPanel();
        stackPanel2.Orientation = Orientation.Horizontal;
        stackPanel2.Spacing = 30.0;
        stackPanel2.Margin = new Thickness(30.0);
        stackPanel2.Children.Add(DocumentGallery.Sheet(clickyInkLiveNote3, editStart - 1.0, "before the edit", null, null, null));
        UIElementCollection children2 = stackPanel2.Children;
        ClickyInkLiveNote clickyInkLiveNote4 = clickyInkLiveNote3;
        double num4 = num3;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(31, 1);
        defaultInterpolatedStringHandler2.AppendLiteral("the edit being written (clock ");
        defaultInterpolatedStringHandler2.AppendFormatted<double>(num3, "0.00");
        defaultInterpolatedStringHandler2.AppendLiteral(")");
        children2.Add(DocumentGallery.Sheet(clickyInkLiveNote4, num4, defaultInterpolatedStringHandler2.ToStringAndClear(), null, null, null));
        stackPanel2.Children.Add(DocumentGallery.Sheet(clickyInkLiveNote3, clickyInkLiveNote3.FinishedClock + 0.4, "just written: highlighter sweeping", null, null, null));
        return stackPanel2;
      });
      DocumentGallery.AddInkScene(list, "ink-sheet-working", 1000.0, 900.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote5 = ClickyInkSamples.LiveNote("# Espresso pick\n\n## In short\n> Checking which machine under $500 makes the best milk\n- [x] Find the machines people recommend\n- [ ] Compare prices and milk {now}\n- [ ] Pick one and the catch\n\n## Compare\n| Machine | Price | Milk |\n|---|---|---|\n| Bambino Plus | $399 | auto |");
        ClickyInkLiveNote clickyInkLiveNote6 = ClickyInkSamples.LiveNote(ClickyInkSamples.DeskPickVersions);
        StackPanel stackPanel3 = new StackPanel();
        stackPanel3.Orientation = Orientation.Horizontal;
        stackPanel3.Spacing = 30.0;
        stackPanel3.Margin = new Thickness(30.0);
        UIElementCollection children3 = stackPanel3.Children;
        ClickyInkLiveNote clickyInkLiveNote7 = clickyInkLiveNote5;
        double num5 = clickyInkLiveNote5.FinishedClock + 3.2;
        string text = "working: resting on {now}";
        bool? flag = new bool?(true);
        children3.Add(DocumentGallery.Sheet(clickyInkLiveNote7, num5, text, null, flag, null));
        stackPanel3.Children.Add(DocumentGallery.Sheet(clickyInkLiveNote6, clickyInkLiveNote6.FinishedClock + 3.2, "working, no {now}: the pencil at the end", new ClickyHomeSpreadPaper?(ClickyHomeSpreadPaper.Of(ClickyPaperStock.Mint)), new bool?(true), null));
        return stackPanel3;
      });
      DocumentGallery.AddInkScene(list, "ink-sheet-window", 1460.0, 520.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote8 = ClickyInkSamples.LiveNote("# Lisbon in May\n\n## In short\n> Fly ANA on the 12th, stay in Alfama. {big=\"$842\" note=\"nonstop, cheapest\"}\n::ticket[ANA NH7, nonstop]{from=\"Boston (BOS)\" to=\"Lisbon (LIS)\" when=\"May 12, 9:40 pm\" price=\"$842\" seat=\"32K\" kind=flight}\n:::receipt[Trip budget]{total=\"$3,380\"}\nFlights: $1,684\nHotel: $900\nFood: $520\nTrams and trains: $276\n:::\n::sticky[Book before Friday: the fare goes up $120]{color=yellow}\n\n## Saturday\n:::day[Saturday May 13]\n9:30 Pastéis de Belém — before the line\nLunch: Time Out Market\n18:00 Miradouro da Graça — sunset\n:::\n::place[Time Out Market]{note=\"go hungry, 8 stalls\" rating=4.6 hours=\"10am–midnight\"}\n::clip[The best custard tart in the city, still warm.]{by=Eater source=https://www.eater.com/lisbon}\n\n## Your move\n:::letter{to=\"Dana Reyes\" subject=\"Re: Lisbon dates\" send=\"Send this reply to Dana from my Gmail\"}\nHi Dana,\n\nThe 12th to the 19th works. I'll book the ANA flight tonight.\n\nBest,\nSam\n:::\n:::review[Cancel these holds?]{go=\"Cancel the ticked hotel holds\" go_label=\"Cancel\"}\n- [x] Hotel Avenida hold — the Alfama one is cheaper\n- [ ] TAP flight hold — keep as a backup\n:::\n:::sketch[How the trams run]{h=120}\nbox 10 40 100 40 \"Baixa\" blue fill\narrow 110 60 170 60 \"28\" curve\nbox 170 40 100 40 \"Alfama\" green\ncircle 330 60 26 \"Graça\" orange\n:::");
        StackPanel stackPanel4 = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 30.0,
          Margin = new Thickness(30.0)
        };
        foreach (double num6 in new double[] { 0.25, 0.6, 1.0 })
        {
          double num7 = ((num6 >= 1.0) ? (clickyInkLiveNote8.FinishedClock + 30.0) : (clickyInkLiveNote8.FinishedClock * num6));
          UIElementCollection children4 = stackPanel4.Children;
          ClickyInkLiveNote clickyInkLiveNote9 = clickyInkLiveNote8;
          double num8 = num7;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(18, 1);
          defaultInterpolatedStringHandler3.AppendLiteral("window 360, clock ");
          defaultInterpolatedStringHandler3.AppendFormatted<double>(num7, "0.0");
          string text2 = defaultInterpolatedStringHandler3.ToStringAndClear();
          ClickyHomeSpreadPaper? clickyHomeSpreadPaper = new ClickyHomeSpreadPaper?(ClickyHomeSpreadPaper.Of(ClickyPaperStock.Peach));
          double? num9 = new double?((double)360);
          children4.Add(DocumentGallery.Sheet(clickyInkLiveNote9, num8, text2, clickyHomeSpreadPaper, null, num9));
        }
        return stackPanel4;
      });
      DocumentGallery.AddInkScene(list, "ink-top-windows", 1620.0, 640.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote10 = ClickyInkSamples.LiveNote("# Espresso pick\n\n## In short\n> Bambino Plus; budget $120 more for a grinder {big=\"$399\" note=\"best milk under $500\"}\n- Auto milk frothing that actually works\n- Heats up in 3 seconds\n::sticky[It needs a grinder: no beans pre-ground]{color=pink}\n::photo[Breville Bambino Plus]{query=\"Breville Bambino Plus espresso machine\"}\n\n## Compare\n| Machine | Price | Milk |\n|---|---|---|\n| **Bambino Plus** | $399 | auto |\n| Gaggia Classic Pro | $449 | manual |\n- De'Longhi Dedica {out}\n  steam wand too weak in 3 reviews\n:::chart[Reviewer scores]{unit=/10}\nBambino Plus: 9.1 {pick}\nGaggia Classic Pro: 8.4\n:::\n\n## Next\n::ask[Find a grinder to match]{say=\"Find the best grinder under $150 to pair with the Bambino Plus\"}\n::question[Mostly milk drinks?]{yes=\"Mostly lattes, keep the Bambino\" no=\"Mostly espresso, reconsider the Gaggia\"}\n::link[Wirecutter: espresso machines]{url=https://www.nytimes.com/wirecutter/reviews/best-espresso-machine/}");
        ClickyInkLiveNote clickyInkLiveNote11 = ClickyInkSamples.LiveNote("# Espresso pick\n\n## In short\n> Checking which machine under $500 makes the best milk\n- [x] Find the machines people recommend\n- [ ] Compare prices and milk {now}\n- [ ] Pick one and the catch\n\n## Compare\n| Machine | Price | Milk |\n|---|---|---|\n| Bambino Plus | $399 | auto |");
        ClickyInkLiveNote clickyInkLiveNote12 = ClickyInkSamples.LiveNote("# Pocketlog daily\n\n## Today\n::run{n=12 at=\"Sat Oct 10 · 7:00\" next=\"Sun 7:00\" note=\"App Store, Play Console, Stripe\"}\n> Crashes jumped on Android 2.14; signups held. {big=\"98.1%\" note=\"crash-free, down\"}\n:::tiles\n::spark[Signups]{values=\"118, 121, 133, 129, 142, 138\"}\n::figure[Revenue]{value=\"$1,319\" delta=\"+3%\"}\n::status[Crash-free]{state=bad note=\"98.1%: 2.14 crashes at login\"}\n::countdown[2.15 review]{to=\"2026-10-12 09:00\"}\n:::\n::sticky[Pause the 2.14 rollout: 212 crashes, all at login]{color=pink}\n\n## Your move\n::question[Pause the 2.14 rollout?]{yes=\"Pause the Android 2.14 rollout in the Play Console\" no=\"Keep it going and watch it\" yes_label=\"Pause it\" no_label=\"Keep going\" since=\"Oct 10\"}\n\n## Worth a look\n- Android 2.14 crashes at login on Pixel 8 {new}\n  212 crashes, 97% on 2.14\n- Onboarding step 3 drop-off is down to 38%\n\n## Funnel\n:::pipeline[Yesterday]\nVisited: 4,050\nSigned up: 138\nStarted trial: 64\nPaid: 11\n:::");
        DocumentGallery.<>c__DisplayClass63_1 CS$<>8__locals2;
        CS$<>8__locals2.grid = new Grid
        {
          Margin = new Thickness(30.0),
          ColumnSpacing = 30.0,
          RowSpacing = 30.0
        };
        CS$<>8__locals2.grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
        CS$<>8__locals2.grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
        CS$<>8__locals2.grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
        CS$<>8__locals2.grid.RowDefinitions.Add(new RowDefinition
        {
          Height = GridLength.Auto
        });
        CS$<>8__locals2.grid.RowDefinitions.Add(new RowDefinition
        {
          Height = GridLength.Auto
        });
        DocumentGallery.<AddInkFormScenes>g__Put|63_7(DocumentGallery.TopOfNote(clickyInkLiveNote10, clickyInkLiveNote10.FinishedClock + 30.0, false, false, false, 480.0, 220.0, "done, the window to itself", ClickyPaperStock.Sky), 0, 0, ref CS$<>8__locals2);
        DocumentGallery.<AddInkFormScenes>g__Put|63_7(DocumentGallery.TopOfNote(clickyInkLiveNote10, clickyInkLiveNote10.FinishedClock + 30.0, false, false, false, 300.0, 220.0, "done, beside its files", ClickyPaperStock.Butter), 0, 1, ref CS$<>8__locals2);
        DocumentGallery.<AddInkFormScenes>g__Put|63_7(DocumentGallery.TopOfNote(clickyInkLiveNote11, clickyInkLiveNote11.FinishedClock + 2.0, true, false, false, 480.0, 220.0, "working (still: not the pointer's window)", ClickyPaperStock.Mint), 0, 2, ref CS$<>8__locals2);
        DocumentGallery.<AddInkFormScenes>g__Put|63_7(DocumentGallery.TopOfNote(clickyInkLiveNote10, clickyInkLiveNote10.FinishedClock + 30.0, false, false, true, 480.0, 220.0, "glance (the deck's window)", ClickyPaperStock.Blush), 1, 0, ref CS$<>8__locals2);
        DocumentGallery.<AddInkFormScenes>g__Put|63_7(DocumentGallery.TopOfNote(clickyInkLiveNote12, clickyInkLiveNote12.FinishedClock + 30.0, false, false, true, 480.0, 220.0, "glance, a dashboard", ClickyPaperStock.Periwinkle), 1, 1, ref CS$<>8__locals2);
        DocumentGallery.<AddInkFormScenes>g__Put|63_7(DocumentGallery.TopOfNote(clickyInkLiveNote10, clickyInkLiveNote10.FinishedClock * 0.45, true, true, false, 480.0, 220.0, "writing on the pointer's window (animates)", ClickyPaperStock.Peach), 1, 2, ref CS$<>8__locals2);
        return CS$<>8__locals2.grid;
      });
    }

    // Token: 0x060024A9 RID: 9385 RVA: 0x00637144 File Offset: 0x00637144
    private static void AddInkScene(List<DocumentGallery.Scene> list, string name, double width, double height, Func<FrameworkElement> build)
    {
      list.Add(new DocumentGallery.Scene(name, width, height, () => new Grid
      {
        Background = Ui.Brush(DocumentGallery.InkDesk),
        Children = { build() }
      }, new Color?(DocumentGallery.InkDesk), 2.2));
    }

    // Token: 0x060024AA RID: 9386 RVA: 0x0063718C File Offset: 0x0063718C
    public static FrameworkElement InkPageScene(ClickyInkLiveNote note, int pageIndex, double clock, ClickyHomeSpreadPaper? paper = null, [Nullable(2)] string caption = null, [Nullable(2)] ClickyInkActions actions = null, int part = 0)
    {
      ClickyHomeSpreadPaper clickyHomeSpreadPaper = paper ?? ClickyHomeSpreadPaper.Note;
      Grid grid = new Grid
      {
        Width = ClickyInkLayout.PageSize.Width,
        Height = ClickyInkLayout.PageSize.Height
      };
      grid.Children.Add(new PaperSurface(ClickyShape.Rectangle, clickyHomeSpreadPaper.Fill, new double?(0.45)).WithShadow(PaperColor.Black.Opacity(0.12), 6.0, 0.0, 2.0));
      grid.Children.Add(new ClickyInkPageView(note, pageIndex, clock, new ClickyHomeSpreadPaper?(clickyHomeSpreadPaper), actions, part, null));
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 6.0,
        Children = { grid }
      };
      if (caption != null)
      {
        stackPanel.Children.Add(Ui.Text(caption, 11.0, FontWeights.SemiBold, PaperColor.Hex("#6B6F7A"), TextAlignment.Left, 0, false, false));
      }
      return stackPanel;
    }

    // Token: 0x060024AB RID: 9387 RVA: 0x006372B0 File Offset: 0x006372B0
    public static FrameworkElement InkGroupScene(ClickyInkLiveNote note, Func<ClickyInkGroup, bool> pick, double? clock = null, int pageIndex = 0, ClickyHomeSpreadPaper? paper = null, double width = 378.0, [Nullable(2)] ClickyInkActions actions = null)
    {
      ClickyHomeSpreadPaper clickyHomeSpreadPaper = paper ?? ClickyHomeSpreadPaper.Note;
      double num = clock ?? (note.FinishedClock + 30.0);
      ClickyInkStyle clickyInkStyle = new ClickyInkStyle(clickyHomeSpreadPaper.Ink, clickyHomeSpreadPaper.Quiet, new Color?(clickyHomeSpreadPaper.Deep), note.LatestEdit(num)).Resting(note, null);
      ClickyInkGroup clickyInkGroup = ClickyInkGroup.Groups(note.Pages[pageIndex], num).FirstOrDefault<ClickyInkGroup>(pick);
      ClickyInkEnvironment clickyInkEnvironment = ClickyInkEnvironment.Default.<Clone>$();
      clickyInkEnvironment.ContentWidth = width;
      FrameworkElement frameworkElement;
      using (ClickyInkEnvironment.Use(clickyInkEnvironment))
      {
        frameworkElement = ((clickyInkGroup == null) ? Ui.Text("no such group", 12.0, FontWeights.SemiBold, PaperColor.Black, TextAlignment.Left, 0, false, false) : ClickyInkGroupView.Make(clickyInkGroup, num, clickyInkStyle, clickyHomeSpreadPaper, actions ?? new ClickyInkActions()));
      }
      return new Grid
      {
        Width = width + 72.0,
        Padding = new Thickness(36.0, 24.0, 36.0, 24.0),
        Children = 
        {
          new PaperSurface(ClickyShape.Rectangle, clickyHomeSpreadPaper.Fill, new double?(0.45))
          {
            Margin = new Thickness(-36.0, -24.0, -36.0, -24.0)
          },
          frameworkElement
        }
      };
    }

    // Token: 0x060024AC RID: 9388 RVA: 0x00637470 File Offset: 0x00637470
    private static double MidWriting(ClickyInkLiveNote note, Func<ClickyInkLiveNote.PenFocus, bool> placeMatches, double fraction = 0.5)
    {
      foreach (ClickyInkLiveNote.PenFocus penFocus in from f in note.PenFocusList()
        orderby f.At
        select f)
      {
        if (placeMatches(penFocus) && !penFocus.IsLanding && penFocus.ItemID != null && penFocus.Until > penFocus.At + 0.3)
        {
          return penFocus.At + (penFocus.Until - penFocus.At) * fraction;
        }
      }
      return note.FinishedClock * fraction;
    }

    // Token: 0x060024AD RID: 9389 RVA: 0x00637538 File Offset: 0x00637538
    private static void AddInkPageScenes(List<DocumentGallery.Scene> list)
    {
      DocumentGallery.AddInkScene(list, "ink-page-words", 1460.0, 640.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote = ClickyInkSamples.LiveNote("# Standing desk notes\n\n## What I read\n### The short version\nThree desks held steady at standing height; one wobbled.\n::note[The wobble only shows above 115 cm]\n> Reviewers keep coming back to the motor noise and the crossbar, not the price.\nA long paragraph is typed rather than handwritten so the page stays legible: the Uplift V2 and the Flexispot E7 Pro both passed the wobble test at full height, the Fully Jarvis wobbled a little, and the IKEA Trotten never got close. Noise ranged from forty to fifty decibels.\n- Flexispot E7 Pro is the quietest {new}\n- Uplift V2 has the steadiest frame {pick}\n- Check the return window {needs}\n- Order the cable tray {done}\n- IKEA Trotten {out}\n  a crank, not a motor\n- [The Wirecutter test](https://www.nytimes.com/wirecutter/reviews/best-standing-desk/) — the longest one\n1. Measure the room\n2. Pick the top size\n3. Order\n```\nheight_cm = 72 + 0.4 * (user_cm - 170)\n```\n---\n:::sketch[Where the desk goes]{h=110}\nbox 20 20 140 70 \"Desk\" blue fill\narrow 170 55 250 55 \"window\" curve\ncircle 300 55 30 \"Lamp\" orange\ndot 80 100 \"outlet\" small\n:::");
        StackPanel stackPanel = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 22.0,
          Margin = new Thickness(24.0)
        };
        List<ValueTuple<int, int>> list2 = ClickyInkPagination.Sheets(clickyInkLiveNote, null);
        foreach (ValueTuple<int, int> valueTuple in list2.Take<ValueTuple<int, int>>(3))
        {
          int page = valueTuple.Item1;
          int item = valueTuple.Item2;
          UIElementCollection children = stackPanel.Children;
          ClickyInkLiveNote clickyInkLiveNote2 = clickyInkLiveNote;
          int page2 = page;
          double num = clickyInkLiveNote.FinishedClock + 30.0;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(22, 3);
          defaultInterpolatedStringHandler.AppendLiteral("note page ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(page + 1);
          defaultInterpolatedStringHandler.AppendLiteral(", sheet ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(item + 1);
          defaultInterpolatedStringHandler.AppendLiteral(" of ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(list2.Count<ValueTuple<int, int>>(([TupleElementNames(new string[] { "Page", "Part" })] ValueTuple<int, int> s) => s.Item1 == page));
          string text = defaultInterpolatedStringHandler.ToStringAndClear();
          int num2 = item;
          children.Add(DocumentGallery.InkPageScene(clickyInkLiveNote2, page2, num, null, text, null, num2));
        }
        return stackPanel;
      });
      DocumentGallery.AddInkScene(list, "ink-page-espresso", 1460.0, 640.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote3 = ClickyInkSamples.LiveNote("# Espresso pick\n\n## In short\n> Bambino Plus; budget $120 more for a grinder {big=\"$399\" note=\"best milk under $500\"}\n- Auto milk frothing that actually works\n- Heats up in 3 seconds\n::sticky[It needs a grinder: no beans pre-ground]{color=pink}\n::photo[Breville Bambino Plus]{query=\"Breville Bambino Plus espresso machine\"}\n\n## Compare\n| Machine | Price | Milk |\n|---|---|---|\n| **Bambino Plus** | $399 | auto |\n| Gaggia Classic Pro | $449 | manual |\n- De'Longhi Dedica {out}\n  steam wand too weak in 3 reviews\n:::chart[Reviewer scores]{unit=/10}\nBambino Plus: 9.1 {pick}\nGaggia Classic Pro: 8.4\n:::\n\n## Next\n::ask[Find a grinder to match]{say=\"Find the best grinder under $150 to pair with the Bambino Plus\"}\n::question[Mostly milk drinks?]{yes=\"Mostly lattes, keep the Bambino\" no=\"Mostly espresso, reconsider the Gaggia\"}\n::link[Wirecutter: espresso machines]{url=https://www.nytimes.com/wirecutter/reviews/best-espresso-machine/}");
        ClickyInkLiveNote.Words words = clickyInkLiveNote3.Pages[0].Blocks.First<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.Source.Kind == ClickyInkDocument.Kind.List).Items[0].Words;
        double num3 = words.StartsAt.GetValueOrDefault() + (words.FinishedAt - words.StartsAt.GetValueOrDefault()) * 0.55;
        StackPanel stackPanel2 = new StackPanel();
        stackPanel2.Orientation = Orientation.Horizontal;
        stackPanel2.Spacing = 22.0;
        stackPanel2.Margin = new Thickness(24.0);
        stackPanel2.Children.Add(DocumentGallery.InkPageScene(clickyInkLiveNote3, 0, clickyInkLiveNote3.FinishedClock + 30.0, null, "finished, cream note", null, 0));
        stackPanel2.Children.Add(DocumentGallery.InkPageScene(clickyInkLiveNote3, 0, clickyInkLiveNote3.FinishedClock + 30.0, new ClickyHomeSpreadPaper?(ClickyHomeSpreadPaper.Of(ClickyPaperStock.Sky)), "finished, sky", null, 0));
        UIElementCollection children2 = stackPanel2.Children;
        ClickyInkLiveNote clickyInkLiveNote4 = clickyInkLiveNote3;
        int num4 = 0;
        double num5 = num3;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(25, 1);
        defaultInterpolatedStringHandler2.AppendLiteral("writing the list (clock ");
        defaultInterpolatedStringHandler2.AppendFormatted<double>(num3, "0.00");
        defaultInterpolatedStringHandler2.AppendLiteral(")");
        string text2 = defaultInterpolatedStringHandler2.ToStringAndClear();
        children2.Add(DocumentGallery.InkPageScene(clickyInkLiveNote4, num4, num5, null, text2, null, 0));
        return stackPanel2;
      });
      DocumentGallery.AddInkScene(list, "ink-page-edit", 1460.0, 640.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote5 = ClickyInkSamples.LiveNote(ClickyInkSamples.DeskPickVersions);
        double editStart = ((clickyInkLiveNote5.EditStarts.Count > 0) ? clickyInkLiveNote5.EditStarts[0] : 30.0);
        ClickyInkLiveNote.Block answer = clickyInkLiveNote5.Pages[0].Blocks[0];
        double num6 = DocumentGallery.MidWriting(clickyInkLiveNote5, (ClickyInkLiveNote.PenFocus f) => f.PlaceID == answer.Id && f.At >= editStart, 0.6);
        StackPanel stackPanel3 = new StackPanel();
        stackPanel3.Orientation = Orientation.Horizontal;
        stackPanel3.Spacing = 22.0;
        stackPanel3.Margin = new Thickness(24.0);
        stackPanel3.Children.Add(DocumentGallery.InkPageScene(clickyInkLiveNote5, 0, editStart - 1.0, null, "before the edit", null, 0));
        UIElementCollection children3 = stackPanel3.Children;
        ClickyInkLiveNote clickyInkLiveNote6 = clickyInkLiveNote5;
        int num7 = 0;
        double num8 = num6;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(31, 1);
        defaultInterpolatedStringHandler3.AppendLiteral("the edit being written (clock ");
        defaultInterpolatedStringHandler3.AppendFormatted<double>(num6, "0.00");
        defaultInterpolatedStringHandler3.AppendLiteral(")");
        string text3 = defaultInterpolatedStringHandler3.ToStringAndClear();
        children3.Add(DocumentGallery.InkPageScene(clickyInkLiveNote6, num7, num8, null, text3, null, 0));
        stackPanel3.Children.Add(DocumentGallery.InkPageScene(clickyInkLiveNote5, 0, clickyInkLiveNote5.FinishedClock + 0.4, null, "just written: highlighter sweeping", null, 0));
        return stackPanel3;
      });
      DocumentGallery.AddInkScene(list, "ink-page-working", 1000.0, 640.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote7 = ClickyInkSamples.LiveNote("# Espresso pick\n\n## In short\n> Checking which machine under $500 makes the best milk\n- [x] Find the machines people recommend\n- [ ] Compare prices and milk {now}\n- [ ] Pick one and the catch\n\n## Compare\n| Machine | Price | Milk |\n|---|---|---|\n| Bambino Plus | $399 | auto |");
        return new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 22.0,
          Margin = new Thickness(24.0),
          Children = 
          {
            DocumentGallery.InkPageScene(clickyInkLiveNote7, 0, clickyInkLiveNote7.FinishedClock + 3.2, null, "working: the pencil rests on {now}", null, 0),
            DocumentGallery.InkPageScene(clickyInkLiveNote7, 1, clickyInkLiveNote7.FinishedClock + 30.0, null, "its second page", null, 0)
          }
        };
      });
      DocumentGallery.AddInkScene(list, "ink-page-groups", 900.0, 560.0, delegate
      {
        ClickyInkLiveNote clickyInkLiveNote8 = ClickyInkSamples.LiveNote("# Standing desk notes\n\n## What I read\n### The short version\nThree desks held steady at standing height; one wobbled.\n::note[The wobble only shows above 115 cm]\n> Reviewers keep coming back to the motor noise and the crossbar, not the price.\nA long paragraph is typed rather than handwritten so the page stays legible: the Uplift V2 and the Flexispot E7 Pro both passed the wobble test at full height, the Fully Jarvis wobbled a little, and the IKEA Trotten never got close. Noise ranged from forty to fifty decibels.\n- Flexispot E7 Pro is the quietest {new}\n- Uplift V2 has the steadiest frame {pick}\n- Check the return window {needs}\n- Order the cable tray {done}\n- IKEA Trotten {out}\n  a crank, not a motor\n- [The Wirecutter test](https://www.nytimes.com/wirecutter/reviews/best-standing-desk/) — the longest one\n1. Measure the room\n2. Pick the top size\n3. Order\n```\nheight_cm = 72 + 0.4 * (user_cm - 170)\n```\n---\n:::sketch[Where the desk goes]{h=110}\nbox 20 20 140 70 \"Desk\" blue fill\narrow 170 55 250 55 \"window\" curve\ncircle 300 55 30 \"Lamp\" orange\ndot 80 100 \"outlet\" small\n:::");
        StackPanel stackPanel4 = new StackPanel();
        stackPanel4.Spacing = 14.0;
        stackPanel4.Margin = new Thickness(24.0);
        stackPanel4.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote8, (ClickyInkGroup g) => g.Kind == ClickyInkDocument.Kind.List, null, 0, null, 378.0, null));
        stackPanel4.Children.Add(DocumentGallery.InkGroupScene(clickyInkLiveNote8, (ClickyInkGroup g) => g.Kind == ClickyInkDocument.Kind.Drawing, new double?(clickyInkLiveNote8.Pages[0].Blocks.First<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.Source.Kind == ClickyInkDocument.Kind.Drawing).LandsAt + 1.2), 0, null, 378.0, null));
        return stackPanel4;
      });
    }

    // Token: 0x060024AE RID: 9390 RVA: 0x00637674 File Offset: 0x00637674
    private static Grid OnSidebar(FrameworkElement content)
    {
      return new Grid
      {
        Background = Ui.Brush(HomeTheme.SidebarFill),
        HorizontalAlignment = HorizontalAlignment.Stretch,
        VerticalAlignment = VerticalAlignment.Stretch,
        Children = { content }
      };
    }

    // Token: 0x060024AF RID: 9391 RVA: 0x006376A8 File Offset: 0x006376A8
    private static void WriteSamplePicture(string path)
    {
      DocumentGallery.<>c__DisplayClass74_0 CS$<>8__locals1 = new DocumentGallery.<>c__DisplayClass74_0();
      if (File.Exists(path))
      {
        return;
      }
      CS$<>8__locals1.pixels = new byte[422400];
      int[] array = new int[] { 60, 90, 120, 150, 140, 185 };
      for (int i = 0; i < 220; i++)
      {
        for (int j = 0; j < 480; j++)
        {
          int num = (i * 480 + j) * 4;
          byte b = 34;
          byte b2 = 36;
          byte b3 = 43;
          int num2 = (j - 30) / 72;
          int num3 = (j - 30) % 72;
          if (j >= 30 && num2 < array.Length && num3 < 48 && 200 - i < array[num2] && i < 200)
          {
            b = 91;
            b2 = 156;
            b3 = 248;
          }
          if (i == 200)
          {
            b2 = (b = (b3 = 96));
          }
          CS$<>8__locals1.pixels[num] = b3;
          CS$<>8__locals1.pixels[num + 1] = b2;
          CS$<>8__locals1.pixels[num + 2] = b;
          CS$<>8__locals1.pixels[num + 3] = byte.MaxValue;
        }
      }
      using (FileStream fileStream = File.Create(path))
      {
        Task<byte[]> task = Task.Run<byte[]>(delegate
        {
          DocumentGallery.<>c__DisplayClass74_0.<<WriteSamplePicture>b__0>d <<WriteSamplePicture>b__0>d;
          <<WriteSamplePicture>b__0>d.<>t__builder = AsyncTaskMethodBuilder<byte[]>.Create();
          <<WriteSamplePicture>b__0>d.<>4__this = CS$<>8__locals1;
          <<WriteSamplePicture>b__0>d.<>1__state = -1;
          <<WriteSamplePicture>b__0>d.<>t__builder.Start<DocumentGallery.<>c__DisplayClass74_0.<<WriteSamplePicture>b__0>d>(ref <<WriteSamplePicture>b__0>d);
          return <<WriteSamplePicture>b__0>d.<>t__builder.Task;
        });
        fileStream.Write(task.GetAwaiter().GetResult());
      }
    }

    // Token: 0x060024B0 RID: 9392 RVA: 0x0063781C File Offset: 0x0063781C
    private static FrameworkElement Preview(string path, int sheetIndex, SpreadsheetCellRange selection)
    {
      SpreadsheetWorkbook spreadsheetWorkbook = SpreadsheetWorkbook.Read(path);
      if (spreadsheetWorkbook == null)
      {
        throw new InvalidOperationException("couldn't read " + path);
      }
      SpreadsheetPreview spreadsheetPreview = new SpreadsheetPreview(spreadsheetWorkbook);
      spreadsheetPreview.SelectSheet(sheetIndex);
      spreadsheetPreview.GridView.Select(selection);
      return spreadsheetPreview;
    }

    // Token: 0x060024B2 RID: 9394 RVA: 0x006378B0 File Offset: 0x006378B0
    [CompilerGenerated]
    internal static string <AddSpreadsheetScenes>g__Sample|2_0(string name)
    {
      return Path.Combine(DocumentGallery.SampleDirectory, name);
    }

    // Token: 0x060024B3 RID: 9395 RVA: 0x006378C0 File Offset: 0x006378C0
    [CompilerGenerated]
    internal static void <AddInkPaperScenes>g__Add|8_0(string name, double width, double height, Func<FrameworkElement> build, Color desk, ref DocumentGallery.<>c__DisplayClass8_0 A_5)
    {
      A_5.list.Add(new DocumentGallery.Scene(name, width, height, () => new Grid
      {
        Background = Ui.Brush(desk),
        Children = { build() }
      }, new Color?(desk), 1.6));
    }

    // Token: 0x060024B4 RID: 9396 RVA: 0x00637918 File Offset: 0x00637918
    [CompilerGenerated]
    internal static ClickyInkLiveNote <AddInkFrontPageScenes>g__Runs|26_0()
    {
      return ClickyInkSamples.LiveNote(new ValueTuple<string, double, bool>[]
      {
        new ValueTuple<string, double, bool>("# Pocketlog daily\n\n## Today\n::run{n=12 at=\"Sat Oct 10 · 7:00\" next=\"Sun 7:00\" note=\"App Store, Play Console, Stripe\"}\n> Crashes jumped on Android 2.14; signups held. {big=\"98.1%\" note=\"crash-free, down\"}\n:::tiles\n::spark[Signups]{values=\"118, 121, 133, 129, 142, 138\"}\n::figure[Revenue]{value=\"$1,319\" delta=\"+3%\"}\n::status[Crash-free]{state=bad note=\"98.1%: 2.14 crashes at login\"}\n::countdown[2.15 review]{to=\"2026-10-12 09:00\"}\n:::\n::sticky[Pause the 2.14 rollout: 212 crashes, all at login]{color=pink}\n\n## Your move\n::question[Pause the 2.14 rollout?]{yes=\"Pause the Android 2.14 rollout in the Play Console\" no=\"Keep it going and watch it\" yes_label=\"Pause it\" no_label=\"Keep going\" since=\"Oct 10\"}\n\n## Worth a look\n- Android 2.14 crashes at login on Pixel 8 {new}\n  212 crashes, 97% on 2.14\n- Onboarding step 3 drop-off is down to 38%\n\n## Funnel\n:::pipeline[Yesterday]\nVisited: 4,050\nSigned up: 138\nStarted trial: 64\nPaid: 11\n:::", 0.0, false),
        new ValueTuple<string, double, bool>("# Pocketlog daily\n\n## Today\n::run{n=13 at=\"Sun Oct 11 · 7:00\" next=\"Mon 7:00\" note=\"App Store, Play Console, Stripe\"}\n> 2.14 paused; crash-free back to 99.6%. {big=\"99.6%\" note=\"crash-free, up\"}\n:::tiles\n::spark[Signups]{values=\"118, 121, 133, 129, 142, 138, 151\"}\n::figure[Revenue]{value=\"$1,402\" delta=\"+6%\"}\n::status[Crash-free]{state=ok note=\"99.6% since the pause\"}\n::countdown[2.15 review]{to=\"2026-10-12 09:00\"}\n:::\n\n## Worth a look\n- Android 2.14 rollout paused at 20% {new}\n- Onboarding step 3 drop-off is down to 38%\n\n## Funnel\n:::pipeline[Yesterday]\nVisited: 4,210\nSigned up: 151\nStarted trial: 70\nPaid: 13\n:::", 40.0, true)
      });
    }

    // Token: 0x060024B5 RID: 9397 RVA: 0x00637968 File Offset: 0x00637968
    [CompilerGenerated]
    internal static FrameworkElement <AddInkFrontPageScenes>g__Captioned|26_1(FrameworkElement view, string caption)
    {
      return new StackPanel
      {
        Spacing = 6.0,
        Children = 
        {
          view,
          Ui.Text(caption, 11.0, FontWeights.SemiBold, PaperColor.Hex("#6B6F7A"), TextAlignment.Left, 0, false, false)
        }
      };
    }

    // Token: 0x060024B6 RID: 9398 RVA: 0x006379C4 File Offset: 0x006379C4
    [CompilerGenerated]
    internal static void <WriteSamplePng>g__Chunk|36_0(string type, byte[] data, ref DocumentGallery.<>c__DisplayClass36_0 A_2)
    {
      byte[] bytes = Encoding.ASCII.GetBytes(type);
      byte[] bytes2 = BitConverter.GetBytes(data.Length);
      if (BitConverter.IsLittleEndian)
      {
        Array.Reverse<byte>(bytes2);
      }
      A_2.file.Write(bytes2);
      A_2.file.Write(bytes);
      A_2.file.Write(data);
      byte[] array = bytes;
      int num = 0;
      byte[] array2 = new byte[array.Length + data.Length];
      ReadOnlySpan<byte> readOnlySpan = new ReadOnlySpan<byte>(array);
      readOnlySpan.CopyTo(new Span<byte>(array2).Slice(num, readOnlySpan.Length));
      num += readOnlySpan.Length;
      ReadOnlySpan<byte> readOnlySpan2 = new ReadOnlySpan<byte>(data);
      readOnlySpan2.CopyTo(new Span<byte>(array2).Slice(num, readOnlySpan2.Length));
      num += readOnlySpan2.Length;
      byte[] bytes3 = BitConverter.GetBytes(DocumentGallery.Crc32(array2));
      if (BitConverter.IsLittleEndian)
      {
        Array.Reverse<byte>(bytes3);
      }
      A_2.file.Write(bytes3);
    }

    // Token: 0x060024B7 RID: 9399 RVA: 0x00637AD4 File Offset: 0x00637AD4
    [CompilerGenerated]
    internal static void <SamplePicture>g__Chunk|45_0(string type, byte[] data, ref DocumentGallery.<>c__DisplayClass45_0 A_2)
    {
      byte[] bytes = Encoding.ASCII.GetBytes(type);
      A_2.file.Write(DocumentGallery.BigEndian((uint)data.Length));
      A_2.file.Write(bytes);
      A_2.file.Write(data);
      A_2.file.Write(DocumentGallery.BigEndian(DocumentGallery.Crc(bytes.Concat<byte>(data).ToArray<byte>())));
    }

    // Token: 0x060024B8 RID: 9400 RVA: 0x00637B50 File Offset: 0x00637B50
    [CompilerGenerated]
    internal static void <PaperNotesScene>g__Add|58_0(FrameworkElement e, int row, int column, ref DocumentGallery.<>c__DisplayClass58_0 A_3)
    {
      Grid.SetRow(e, row);
      Grid.SetColumn(e, column);
      A_3.grid.Children.Add(e);
    }

    // Token: 0x060024B9 RID: 9401 RVA: 0x00637B74 File Offset: 0x00637B74
    [CompilerGenerated]
    internal static bool <AddInkObjectScenes>g__Kind|60_0(ClickyInkGroup g, ClickyInkDocument.Kind kind)
    {
      return g.Kind == kind;
    }

    // Token: 0x060024BA RID: 9402 RVA: 0x00637B80 File Offset: 0x00637B80
    [CompilerGenerated]
    internal static double <AddInkObjectScenes>g__At|60_26(ClickyInkLiveNote note, ClickyInkDocument.Kind kind, double after)
    {
      return note.Pages[0].Blocks.First<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.Source.Kind == kind).LandsAt + after;
    }

    // Token: 0x060024BB RID: 9403 RVA: 0x00637BC4 File Offset: 0x00637BC4
    [CompilerGenerated]
    internal static ClickyInkLiveNote <AddInkInteractiveScenes>g__Note|61_12()
    {
      return ClickyInkSamples.LiveNote("# Studio launch\n## In short\n> Three things need you before the pitches go out {big=\"3\"}\n::question[Which offer should I lead with?]{options=\"Free audit | $299 site | Monthly SEO\" other=\"Something else…\" file=\"Attach a brief…\"}\n:::form[Before I write the pitches]{submit=\"Here are my details\"}\nYour studio name\nPrice: e.g. $299\nTone: Friendly | Formal | Playful\nPortfolio: folder\n:::\n:::items[Dentists to pitch]{actions=\"Draft pitch | Skip\"}\n- [Smile Studio](https://example.com) — 4.9★, no website\n- Bright Dental — 4.7★, site from 2014 {pick}\n- Harbor Family Dentistry — 4.8★, slow site\n- Old Town Dentistry {out}\n  closed for renovation\n- Riverside Smiles {done}\n:::\n:::gallery[Three logo directions]{pick=\"Use this logo\"}\noutput/logo-a.png — bold and round\noutput/logo-b.png — thin serif {pick}\noutput/jingle.mp3 — the jingle\nhttps://example.com/brand — the brand page\noutput/logo-c.png — retro {out}\n:::\n::event[Studio open day]{when=\"2026-11-14\" end=\"2026-11-15\" where=\"12 Main St, Lisbon\"}\n:::criteria[What I look for]\nRoles: designer, editor, illustrator\nArea: Lisbon | Porto\nPay: from €40/hour\n:::");
    }

    // Token: 0x060024BC RID: 9404 RVA: 0x00637BD0 File Offset: 0x00637BD0
    [CompilerGenerated]
    internal static bool <AddInkInteractiveScenes>g__IsForm|61_13(ClickyInkGroup g)
    {
      return g.Kind == ClickyInkDocument.Kind.Form;
    }

    // Token: 0x060024BD RID: 9405 RVA: 0x00637BDC File Offset: 0x00637BDC
    [CompilerGenerated]
    internal static FrameworkElement <AddInkInteractiveScenes>g__Card|61_14(string app, string caption, string extra = "")
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(60, 2);
      defaultInterpolatedStringHandler.AppendLiteral("# Connect\n## In short\n::connect[");
      defaultInterpolatedStringHandler.AppendFormatted(app);
      defaultInterpolatedStringHandler.AppendLiteral("]{why=\"to save the drafts\"");
      defaultInterpolatedStringHandler.AppendFormatted(extra);
      defaultInterpolatedStringHandler.AppendLiteral("}\n");
      return DocumentGallery.InteractiveCaptioned(DocumentGallery.InkGroupScene(ClickyInkSamples.LiveNote(defaultInterpolatedStringHandler.ToStringAndClear()), (ClickyInkGroup g) => g.Kind == ClickyInkDocument.Kind.Connect, null, 0, null, 378.0, null), caption);
    }

    // Token: 0x060024BE RID: 9406 RVA: 0x00637C7C File Offset: 0x00637C7C
    [CompilerGenerated]
    internal static bool <AddInkInteractiveScenes>g__IsItems|61_16(ClickyInkGroup g)
    {
      return g.Kind == ClickyInkDocument.Kind.Items;
    }

    // Token: 0x060024BF RID: 9407 RVA: 0x00637C88 File Offset: 0x00637C88
    [CompilerGenerated]
    internal static bool <AddInkInteractiveScenes>g__IsQuiz|61_17(ClickyInkGroup g)
    {
      return g.Kind == ClickyInkDocument.Kind.Quiz;
    }

    // Token: 0x060024C0 RID: 9408 RVA: 0x00637C94 File Offset: 0x00637C94
    [CompilerGenerated]
    internal static bool <AddInkInteractiveScenes>g__IsGallery|61_24(ClickyInkGroup g)
    {
      return g.Kind == ClickyInkDocument.Kind.Gallery;
    }

    // Token: 0x060024C1 RID: 9409 RVA: 0x00637CA0 File Offset: 0x00637CA0
    [CompilerGenerated]
    internal static bool <AddInkInteractiveScenes>g__IsCriteria|61_29(ClickyInkGroup g)
    {
      return g.Kind == ClickyInkDocument.Kind.Criteria;
    }

    // Token: 0x060024C2 RID: 9410 RVA: 0x00637CAC File Offset: 0x00637CAC
    [CompilerGenerated]
    internal static void <AddInkFormScenes>g__Put|63_7(FrameworkElement e, int row, int column, ref DocumentGallery.<>c__DisplayClass63_1 A_3)
    {
      Grid.SetRow(e, row);
      Grid.SetColumn(e, column);
      A_3.grid.Children.Add(e);
    }

    // Token: 0x04001194 RID: 4500
    private static readonly DateTimeOffset DashboardNow = new DateTimeOffset(2026, 10, 21, 10, 0, 0, TimeZoneInfo.Local.GetUtcOffset(new DateTime(2026, 10, 21)));

    // Token: 0x04001195 RID: 4501
    private const string EdgeQuestionNote = "# Short clip editor\n## In short\n> Start with CapCut. {big=\"CapCut\"}\n- Auto captions speed up short edits.\n::question[What is the clip for?]{yes=\"Vertical social\" no=\"A polished Mac edit\" yes_label=\"Social\" no_label=\"Polished\"}";

    // Token: 0x04001196 RID: 4502
    private const string EdgeReviewNote = "# Clean up Downloads\n## In short\n> Nine old installers take 3.1 GB. {big=\"3.1 GB\"}\n:::review[Move these to the Trash?]{go=\"Move these to the Trash\" skip=\"Keep them\"}\n- [x] Figma-124.dmg — an old installer\n- [ ] Zoom.pkg — installed in 2023\n- [x] Xcode_15.xip — replaced by Xcode 16\n:::\n## Kept\n- Your photo exports";

    // Token: 0x04001197 RID: 4503
    private const double ClickyStickyScreenCorner = 10.0;

    // Token: 0x04001198 RID: 4504
    private const string RemotePhoto = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/47/PNG_transparency_demonstration_1.png/330px-PNG_transparency_demonstration_1.png";

    // Token: 0x04001199 RID: 4505
    private const string PickNote = "# Headphones\n\n## In short\n> Sony for flights; Bose if comfort comes first {big=\"$449.99\"}\n::figure[Battery]{value=\"30 h\" delta=\"+6 h\"}\n::figure[Noise cut]{value=\"-38 dB\" delta=\"down 2 dB\"}\n::figure[Weight]{value=\"250 g\" delta=\"lightest\"}\n| Headphones | Price | ANC battery | Best for |\n|---|---|---|---|\n| **Sony WH-1000XM6** {pick} | $449.99 | 30 h | Flights and the office, the best noise cancelling we tried |\n| Bose QuietComfort Ultra | $299.95 | 24 h | Comfort on long days |\n| Sennheiser Momentum 4 {out} | $379.95 | 60 h | Battery above everything |\n::sticky[Buy from the airline store: 10% off]{color=pink}\n::sticky[Return window is 30 days]\n::sticky[Case is sold apart]{color=blue}\n:::chart[Reviewer scores]{unit=/10}\nSony WH-1000XM6: 9.1 {pick}\nBose QuietComfort Ultra: 8.4\nSennheiser Momentum 4: 7.9\n:::";

    // Token: 0x0400119A RID: 4506
    private static readonly Color GalleryDesk = PaperColor.Hex("#EDEBE6");

    // Token: 0x0400119B RID: 4507
    private static readonly Color InkDesk = PaperColor.Hex("#E9E6DF");

    // Token: 0x0400119C RID: 4508
    internal const string SampleReport = "# Q3 revenue report\nRevenue grew **12%** quarter over quarter, led by *annual* plans and a ~~flat~~ steady churn rate. See the\n[dashboard](https://example.com/dashboard) or run `npm run report` to rebuild it.\n\n## Highlights\n- Annual plans: **$412k** booked\n- Churn held at `2.1%`\n  - Enterprise churn: 0.4%\n  - Self-serve churn: 3.0%\n- [x] Pricing page shipped\n- [ ] Billing migration\n\n![Revenue by month](chart.png)\n\n### Next steps\n1. Review the cancellation requests with the team.\n2. Update the payment details for North Star Studio.\n10. Send the board memo.\n\n---\n\n> **Tip** · Ask Clicky to refresh this report every Monday.\n\n![A picture from the web](https://upload.wikimedia.org/wikipedia/commons/4/47/PNG_transparency_demonstration_1.png)";

    // Token: 0x0400119D RID: 4509
    private const string SampleBlocks = "# Heading one\n## Heading two\n### Heading three\n#### Heading four\n##### Heading five\n###### Heading six\n\nSetext heading\n==============\n\nA paragraph with **bold**, *italic*, ***both***, `inline code`, ~~struck~~ and a [link](https://heyclicky.com).\nLines of one paragraph join into one.\n\n> A quote to lift out: a draft reply.\n>\n> - with a list\n> - and a second item\n>\n> ```\n> let x = 1\n> ```\n\n> **Note** · A callout has no copy button.\n\n```swift\nfunc greet(name: String) -> String {\n    return \"Hello, \\(name)! This line is long enough that it has to wrap inside the code block's plate on a narrow page.\"\n}\n```\n\n1. First\n2. Second\n    - nested bullet\n        - deeper\n3. Third\n\n***\n\nDone.";

    // Token: 0x0400119E RID: 4510
    private const string SampleTables = "# Tables\n\nBefore the table.\n\n| Customer | Plan | Revenue | Next step |\n| --- | --- | --- | --- |\n| North Star Studio | Professional annual | $12,450.00 | Contact the billing owner to update their payment details. |\n| Other studio | Enterprise | $145,000.00 | Review the cancellation request with the team. |\n\nAfter the table.\n\n| Option | Price | Verdict |\n|:--|--:|:-:|\n| ✓ Breville Bambino | $299 | **best** |\n| Gaggia Classic | $449 | `good` |\n\n| a | b | c | d | e | f |\n|---|---|---|---|---|---|\n| one | two | three | four | five | six |";

    // Token: 0x020009D8 RID: 2520
    [Nullable(0)]
    public sealed class Scene : IEquatable<DocumentGallery.Scene>
    {
      // Token: 0x06003EA6 RID: 16038 RVA: 0x006A39B8 File Offset: 0x006A39B8
      public Scene(string Name, double Width, double Height, Func<FrameworkElement> Build, Color? Fill = null, double Seconds = 1.6)
      {
        this.Name = Name;
        this.Width = Width;
        this.Height = Height;
        this.Build = Build;
        this.Fill = Fill;
        this.Seconds = Seconds;
        base..ctor();
      }

      // Token: 0x1700086D RID: 2157
      // (get) Token: 0x06003EA7 RID: 16039 RVA: 0x006A39F0 File Offset: 0x006A39F0
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(DocumentGallery.Scene);
        }
      }

      // Token: 0x1700086E RID: 2158
      // (get) Token: 0x06003EA8 RID: 16040 RVA: 0x006A39FC File Offset: 0x006A39FC
      // (set) Token: 0x06003EA9 RID: 16041 RVA: 0x006A3A04 File Offset: 0x006A3A04
      public string Name { get; set; }

      // Token: 0x1700086F RID: 2159
      // (get) Token: 0x06003EAA RID: 16042 RVA: 0x006A3A10 File Offset: 0x006A3A10
      // (set) Token: 0x06003EAB RID: 16043 RVA: 0x006A3A18 File Offset: 0x006A3A18
      public double Width { get; set; }

      // Token: 0x17000870 RID: 2160
      // (get) Token: 0x06003EAC RID: 16044 RVA: 0x006A3A24 File Offset: 0x006A3A24
      // (set) Token: 0x06003EAD RID: 16045 RVA: 0x006A3A2C File Offset: 0x006A3A2C
      public double Height { get; set; }

      // Token: 0x17000871 RID: 2161
      // (get) Token: 0x06003EAE RID: 16046 RVA: 0x006A3A38 File Offset: 0x006A3A38
      // (set) Token: 0x06003EAF RID: 16047 RVA: 0x006A3A40 File Offset: 0x006A3A40
      public Func<FrameworkElement> Build { get; set; }

      // Token: 0x17000872 RID: 2162
      // (get) Token: 0x06003EB0 RID: 16048 RVA: 0x006A3A4C File Offset: 0x006A3A4C
      // (set) Token: 0x06003EB1 RID: 16049 RVA: 0x006A3A54 File Offset: 0x006A3A54
      public Color? Fill { get; set; }

      // Token: 0x17000873 RID: 2163
      // (get) Token: 0x06003EB2 RID: 16050 RVA: 0x006A3A60 File Offset: 0x006A3A60
      // (set) Token: 0x06003EB3 RID: 16051 RVA: 0x006A3A68 File Offset: 0x006A3A68
      public double Seconds { get; set; }

      // Token: 0x06003EB4 RID: 16052 RVA: 0x006A3A74 File Offset: 0x006A3A74
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Scene");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003EB5 RID: 16053 RVA: 0x006A3AC0 File Offset: 0x006A3AC0
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Name = ");
        builder.Append(this.Name);
        builder.Append(", Width = ");
        builder.Append(this.Width.ToString());
        builder.Append(", Height = ");
        builder.Append(this.Height.ToString());
        builder.Append(", Build = ");
        builder.Append(this.Build);
        builder.Append(", Fill = ");
        builder.Append(this.Fill.ToString());
        builder.Append(", Seconds = ");
        builder.Append(this.Seconds.ToString());
        return true;
      }

      // Token: 0x06003EB6 RID: 16054 RVA: 0x006A3BA4 File Offset: 0x006A3BA4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(DocumentGallery.Scene left, DocumentGallery.Scene right)
      {
        return !(left == right);
      }

      // Token: 0x06003EB7 RID: 16055 RVA: 0x006A3BB0 File Offset: 0x006A3BB0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(DocumentGallery.Scene left, DocumentGallery.Scene right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003EB8 RID: 16056 RVA: 0x006A3BC4 File Offset: 0x006A3BC4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Width>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Height>k__BackingField)) * -1521134295 + EqualityComparer<Func<FrameworkElement>>.Default.GetHashCode(this.<Build>k__BackingField)) * -1521134295 + EqualityComparer<Color?>.Default.GetHashCode(this.<Fill>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Seconds>k__BackingField);
      }

      // Token: 0x06003EB9 RID: 16057 RVA: 0x006A3C6C File Offset: 0x006A3C6C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as DocumentGallery.Scene);
      }

      // Token: 0x06003EBA RID: 16058 RVA: 0x006A3C7C File Offset: 0x006A3C7C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(DocumentGallery.Scene other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Width>k__BackingField, other.<Width>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Height>k__BackingField, other.<Height>k__BackingField) && EqualityComparer<Func<FrameworkElement>>.Default.Equals(this.<Build>k__BackingField, other.<Build>k__BackingField) && EqualityComparer<Color?>.Default.Equals(this.<Fill>k__BackingField, other.<Fill>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Seconds>k__BackingField, other.<Seconds>k__BackingField));
      }

      // Token: 0x06003EBC RID: 16060 RVA: 0x006A3D48 File Offset: 0x006A3D48
      [CompilerGenerated]
      private Scene(DocumentGallery.Scene original)
      {
        this.Name = original.<Name>k__BackingField;
        this.Width = original.<Width>k__BackingField;
        this.Height = original.<Height>k__BackingField;
        this.Build = original.<Build>k__BackingField;
        this.Fill = original.<Fill>k__BackingField;
        this.Seconds = original.<Seconds>k__BackingField;
      }

      // Token: 0x06003EBD RID: 16061 RVA: 0x006A3DA4 File Offset: 0x006A3DA4
      [CompilerGenerated]
      public void Deconstruct(out string Name, out double Width, out double Height, out Func<FrameworkElement> Build, out Color? Fill, out double Seconds)
      {
        Name = this.Name;
        Width = this.Width;
        Height = this.Height;
        Build = this.Build;
        Fill = this.Fill;
        Seconds = this.Seconds;
      }
    }

    // Token: 0x020009D9 RID: 2521
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04002936 RID: 10550
      [Nullable(new byte[] { 0, 1 })]
      public static Func<FrameworkElement> <0>__PaperStocksScene;

      // Token: 0x04002937 RID: 10551
      [Nullable(new byte[] { 0, 1 })]
      public static Func<FrameworkElement> <1>__PaperPiecesScene;

      // Token: 0x04002938 RID: 10552
      [Nullable(new byte[] { 0, 1 })]
      public static Func<FrameworkElement> <2>__PaperTagsScene;

      // Token: 0x04002939 RID: 10553
      [Nullable(new byte[] { 0, 1 })]
      public static Func<FrameworkElement> <3>__PaperGlyphsScene;

      // Token: 0x0400293A RID: 10554
      [Nullable(new byte[] { 0, 1 })]
      public static Func<FrameworkElement> <4>__PaperSketchScene;

      // Token: 0x0400293B RID: 10555
      [Nullable(new byte[] { 0, 1 })]
      public static Func<FrameworkElement> <5>__PaperHandScene;

      // Token: 0x0400293C RID: 10556
      [Nullable(new byte[] { 0, 1 })]
      public static Func<FrameworkElement> <6>__PaperCardsScene;

      // Token: 0x0400293D RID: 10557
      [Nullable(new byte[] { 0, 1 })]
      public static Func<FrameworkElement> <7>__PaperNotesScene;

      // Token: 0x0400293E RID: 10558
      [Nullable(new byte[] { 0, 1 })]
      public static Func<FrameworkElement> <8>__PaperEdgeScene;

      // Token: 0x0400293F RID: 10559
      [Nullable(0)]
      public static Func<ClickyInkDocument.Item, bool> <9>__IsOpen;
    }
  }
}
