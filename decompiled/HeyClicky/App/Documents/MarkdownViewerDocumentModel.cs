using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002CE RID: 718
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class MarkdownViewerDocumentModel
  {
    // Token: 0x06001E30 RID: 7728 RVA: 0x00606674 File Offset: 0x00606674
    [NullableContext(1)]
    public static MarkdownViewerDocumentModel RetainedDocument(string path)
    {
      string full = Path.GetFullPath(path);
      MarkdownViewerDocumentModel markdownViewerDocumentModel = MarkdownViewerDocumentModel.RecentDocuments.FirstOrDefault<MarkdownViewerDocumentModel>((MarkdownViewerDocumentModel d) => string.Equals(d.FilePath, full, StringComparison.OrdinalIgnoreCase));
      if (markdownViewerDocumentModel != null)
      {
        MarkdownViewerDocumentModel.RecentDocuments.Remove(markdownViewerDocumentModel);
        MarkdownViewerDocumentModel.RecentDocuments.Insert(0, markdownViewerDocumentModel);
        return markdownViewerDocumentModel;
      }
      MarkdownViewerDocumentModel markdownViewerDocumentModel2 = new MarkdownViewerDocumentModel(full);
      MarkdownViewerDocumentModel.RecentDocuments.Insert(0, markdownViewerDocumentModel2);
      if (MarkdownViewerDocumentModel.RecentDocuments.Count > 3)
      {
        MarkdownViewerDocumentModel.RecentDocuments.RemoveAt(MarkdownViewerDocumentModel.RecentDocuments.Count - 1);
      }
      return markdownViewerDocumentModel2;
    }

    // Token: 0x1700055E RID: 1374
    // (get) Token: 0x06001E31 RID: 7729 RVA: 0x00606704 File Offset: 0x00606704
    [Nullable(1)]
    public string FilePath
    {
      [NullableContext(1)]
      get;
    }

    // Token: 0x1700055F RID: 1375
    // (get) Token: 0x06001E32 RID: 7730 RVA: 0x0060670C File Offset: 0x0060670C
    // (set) Token: 0x06001E33 RID: 7731 RVA: 0x00606714 File Offset: 0x00606714
    [Nullable(1)]
    public string MarkdownSource
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      private set;
    } = "";

    // Token: 0x17000560 RID: 1376
    // (get) Token: 0x06001E34 RID: 7732 RVA: 0x00606720 File Offset: 0x00606720
    // (set) Token: 0x06001E35 RID: 7733 RVA: 0x00606728 File Offset: 0x00606728
    public MarkdownViewerPreparedDocument Prepared { get; private set; }

    // Token: 0x17000561 RID: 1377
    // (get) Token: 0x06001E36 RID: 7734 RVA: 0x00606734 File Offset: 0x00606734
    // (set) Token: 0x06001E37 RID: 7735 RVA: 0x0060673C File Offset: 0x0060673C
    public string LoadingError { get; private set; }

    // Token: 0x17000562 RID: 1378
    // (get) Token: 0x06001E38 RID: 7736 RVA: 0x00606748 File Offset: 0x00606748
    [Nullable(1)]
    public string DisplayTitle
    {
      [NullableContext(1)]
      get
      {
        return Path.GetFileName(this.FilePath);
      }
    }

    // Token: 0x17000563 RID: 1379
    // (get) Token: 0x06001E39 RID: 7737 RVA: 0x00606758 File Offset: 0x00606758
    public string BaseDirectory
    {
      get
      {
        return Path.GetDirectoryName(this.FilePath);
      }
    }

    // Token: 0x14000076 RID: 118
    // (add) Token: 0x06001E3A RID: 7738 RVA: 0x00606768 File Offset: 0x00606768
    // (remove) Token: 0x06001E3B RID: 7739 RVA: 0x006067A0 File Offset: 0x006067A0
    public event Action Changed;

    // Token: 0x06001E3C RID: 7740 RVA: 0x006067D8 File Offset: 0x006067D8
    [NullableContext(1)]
    private MarkdownViewerDocumentModel(string path)
    {
      this.FilePath = path;
    }

    // Token: 0x06001E3D RID: 7741 RVA: 0x00606800 File Offset: 0x00606800
    public void ReloadFromDisk()
    {
      MarkdownViewerDocumentModel.<>c__DisplayClass28_0 CS$<>8__locals1 = new MarkdownViewerDocumentModel.<>c__DisplayClass28_0();
      CS$<>8__locals1.<>4__this = this;
      CancellationTokenSource cancellationTokenSource = this.loadCancellation;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      CancellationTokenSource cancellationTokenSource2 = (this.loadCancellation = new CancellationTokenSource());
      CS$<>8__locals1.ct = cancellationTokenSource2.Token;
      CS$<>8__locals1.generation = (this.loadGeneration = Guid.NewGuid());
      CS$<>8__locals1.dq = DispatcherQueue.GetForCurrentThread();
      CS$<>8__locals1.previousSource = this.fullyLoadedSource;
      CS$<>8__locals1.path = this.FilePath;
      CS$<>8__locals1.baseDirectory = this.BaseDirectory;
      this.LoadingError = null;
      Task.Run(delegate
      {
        MarkdownViewerDocumentModel.<>c__DisplayClass28_0.<<ReloadFromDisk>b__0>d <<ReloadFromDisk>b__0>d;
        <<ReloadFromDisk>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<ReloadFromDisk>b__0>d.<>4__this = CS$<>8__locals1;
        <<ReloadFromDisk>b__0>d.<>1__state = -1;
        <<ReloadFromDisk>b__0>d.<>t__builder.Start<MarkdownViewerDocumentModel.<>c__DisplayClass28_0.<<ReloadFromDisk>b__0>d>(ref <<ReloadFromDisk>b__0>d);
        return <<ReloadFromDisk>b__0>d.<>t__builder.Task;
      }, CS$<>8__locals1.ct);
    }

    // Token: 0x06001E3E RID: 7742 RVA: 0x006068A8 File Offset: 0x006068A8
    [NullableContext(1)]
    private void Publish([Nullable(2)] DispatcherQueue dq, Guid generation, Action apply)
    {
      MarkdownViewerDocumentModel.<>c__DisplayClass29_0 CS$<>8__locals1 = new MarkdownViewerDocumentModel.<>c__DisplayClass29_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.generation = generation;
      CS$<>8__locals1.apply = apply;
      if (dq == null || dq.HasThreadAccess)
      {
        CS$<>8__locals1.<Publish>g__Run|0();
        return;
      }
      dq.TryEnqueue(new DispatcherQueueHandler(CS$<>8__locals1.<Publish>g__Run|0));
    }

    // Token: 0x06001E3F RID: 7743 RVA: 0x006068F8 File Offset: 0x006068F8
    public void CancelLoading()
    {
      this.loadGeneration = Guid.NewGuid();
      CancellationTokenSource cancellationTokenSource = this.loadCancellation;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      this.loadCancellation = null;
    }

    // Token: 0x04000E90 RID: 3728
    [Nullable(1)]
    private static readonly List<MarkdownViewerDocumentModel> RecentDocuments = new List<MarkdownViewerDocumentModel>();

    // Token: 0x04000E96 RID: 3734
    private CancellationTokenSource loadCancellation;

    // Token: 0x04000E97 RID: 3735
    private Guid loadGeneration = Guid.NewGuid();

    // Token: 0x04000E98 RID: 3736
    private string fullyLoadedSource;
  }
}
