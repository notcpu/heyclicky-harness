using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using HeyClicky.Core.Documents.Spreadsheet;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002CA RID: 714
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SpreadsheetDocumentView : Grid
  {
    // Token: 0x17000559 RID: 1369
    // (get) Token: 0x06001E14 RID: 7700 RVA: 0x00605C28 File Offset: 0x00605C28
    public string Path { get; }

    // Token: 0x1700055A RID: 1370
    // (get) Token: 0x06001E15 RID: 7701 RVA: 0x00605C30 File Offset: 0x00605C30
    // (set) Token: 0x06001E16 RID: 7702 RVA: 0x00605C38 File Offset: 0x00605C38
    [Nullable(2)]
    public SpreadsheetPreview Preview
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x14000075 RID: 117
    // (add) Token: 0x06001E17 RID: 7703 RVA: 0x00605C44 File Offset: 0x00605C44
    // (remove) Token: 0x06001E18 RID: 7704 RVA: 0x00605C7C File Offset: 0x00605C7C
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event Action<SpreadsheetPreview> PreviewReady;

    // Token: 0x06001E19 RID: 7705 RVA: 0x00605CB4 File Offset: 0x00605CB4
    public SpreadsheetDocumentView(string path, [Nullable(new byte[] { 2, 1 })] Func<FrameworkElement> fallback)
    {
      this.Path = path;
      base.Children.Add(SpreadsheetDocumentView.LoadingView(null));
      this.LoadAsync(fallback);
    }

    // Token: 0x06001E1A RID: 7706 RVA: 0x00605CE8 File Offset: 0x00605CE8
    private async Task LoadAsync([Nullable(new byte[] { 2, 1 })] Func<FrameworkElement> fallback)
    {
      SpreadsheetWorkbook spreadsheetWorkbook = await SpreadsheetAssetCache.LoadAsync(this.Path);
      base.Children.Clear();
      if (spreadsheetWorkbook != null)
      {
        this.Preview = new SpreadsheetPreview(spreadsheetWorkbook);
        base.Children.Add(this.Preview);
        Action<SpreadsheetPreview> previewReady = this.PreviewReady;
        if (previewReady != null)
        {
          previewReady(this.Preview);
        }
      }
      else
      {
        using (MessagesPalette.Use(this.look))
        {
          base.Children.Add(((fallback != null) ? fallback() : null) ?? SpreadsheetDocumentView.LoadingView("This document couldn’t be loaded."));
        }
      }
    }

    // Token: 0x06001E1B RID: 7707 RVA: 0x00605D34 File Offset: 0x00605D34
    private static FrameworkElement LoadingView([Nullable(2)] string error)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 12.0,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      };
      if (error == null)
      {
        stackPanel.Children.Add(new ProgressRing
        {
          IsActive = true,
          Width = 16.0,
          Height = 16.0,
          HorizontalAlignment = HorizontalAlignment.Center
        });
      }
      stackPanel.Children.Add(Ui.Text(error ?? "Loading document…", 13.0, FontWeights.Normal, MessagesPalette.Current.SecondaryText, TextAlignment.Center, 0, false, false));
      return stackPanel;
    }

    // Token: 0x04000E7D RID: 3709
    private readonly MessagesPalette look = MessagesPalette.Current;
  }
}
