using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.App.Home;
using HeyClicky.Core.Documents.Markdown;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002CD RID: 717
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MarkdownViewerInlineDocument : Grid
  {
    // Token: 0x06001E27 RID: 7719 RVA: 0x00606374 File Offset: 0x00606374
    public MarkdownViewerInlineDocument(string markdown, [Nullable(2)] string baseDirectory = null)
    {
      base.RequestedTheme = (MessagesPalette.Current.IsDark ? ElementTheme.Dark : ElementTheme.Light);
      base.Children.Add(this.textView);
      this.textView.WholeDocumentText = () => this.markdown;
      this.baseDirectory = baseDirectory;
      this.Markdown = markdown;
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        CancellationTokenSource cancellationTokenSource = this.imageLoad;
        if (cancellationTokenSource == null)
        {
          return;
        }
        cancellationTokenSource.Cancel();
      };
    }

    // Token: 0x1700055D RID: 1373
    // (get) Token: 0x06001E28 RID: 7720 RVA: 0x00606408 File Offset: 0x00606408
    // (set) Token: 0x06001E29 RID: 7721 RVA: 0x00606410 File Offset: 0x00606410
    public string Markdown
    {
      get
      {
        return this.markdown;
      }
      set
      {
        this.SetMarkdown(value, this.baseDirectory);
      }
    }

    // Token: 0x06001E2A RID: 7722 RVA: 0x00606420 File Offset: 0x00606420
    public void SetMarkdown(string newMarkdown, [Nullable(2)] string newBaseDirectory)
    {
      MarkdownViewerInlineDocument.<>c__DisplayClass11_0 CS$<>8__locals1 = new MarkdownViewerInlineDocument.<>c__DisplayClass11_0();
      CS$<>8__locals1.<>4__this = this;
      if (newMarkdown == this.markdown && newBaseDirectory == this.baseDirectory && this.textView.ActualHeight > 0.0)
      {
        return;
      }
      this.markdown = newMarkdown;
      this.baseDirectory = newBaseDirectory;
      CancellationTokenSource cancellationTokenSource = this.imageLoad;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      CS$<>8__locals1.cts = (this.imageLoad = new CancellationTokenSource());
      CS$<>8__locals1.key = this.baseDirectory + "\u001f" + this.markdown;
      MarkdownViewerInlineDocument.CacheEntry item = MarkdownViewerInlineDocument.RenderCache.FirstOrDefault<ValueTuple<string, MarkdownViewerInlineDocument.CacheEntry>>(([TupleElementNames(new string[] { "Key", "Entry" })] ValueTuple<string, MarkdownViewerInlineDocument.CacheEntry> e) => e.Item1 == CS$<>8__locals1.key).Item2;
      if (item != null)
      {
        this.textView.Install(item.Prepared);
        this.LoadRemoteImages(item, CS$<>8__locals1.cts.Token);
        return;
      }
      CS$<>8__locals1.blocks = MarkdownViewerParser.ParseBlocks(this.markdown);
      CS$<>8__locals1.preparing = MarkdownViewerPreparedDocument.PrepareAsync(CS$<>8__locals1.blocks, this.baseDirectory, null, default(CancellationToken));
      if (CS$<>8__locals1.preparing.IsCompletedSuccessfully)
      {
        this.Show(CS$<>8__locals1.key, CS$<>8__locals1.blocks, CS$<>8__locals1.preparing.Result, CS$<>8__locals1.cts.Token);
        return;
      }
      CS$<>8__locals1.<SetMarkdown>g__FinishAsync|1();
    }

    // Token: 0x06001E2B RID: 7723 RVA: 0x00606570 File Offset: 0x00606570
    private void Show(string key, IReadOnlyList<MarkdownViewerBlock> blocks, MarkdownViewerPreparedDocument prepared, CancellationToken ct)
    {
      MarkdownViewerInlineDocument.CacheEntry cacheEntry = new MarkdownViewerInlineDocument.CacheEntry(blocks, prepared);
      MarkdownViewerInlineDocument.RenderCache.Insert(0, new ValueTuple<string, MarkdownViewerInlineDocument.CacheEntry>(key, cacheEntry));
      if (MarkdownViewerInlineDocument.RenderCache.Count > 8)
      {
        MarkdownViewerInlineDocument.RenderCache.RemoveAt(MarkdownViewerInlineDocument.RenderCache.Count - 1);
      }
      this.textView.Install(prepared);
      this.LoadRemoteImages(cacheEntry, ct);
    }

    // Token: 0x06001E2C RID: 7724 RVA: 0x006065D0 File Offset: 0x006065D0
    private void LoadRemoteImages(MarkdownViewerInlineDocument.CacheEntry entry, CancellationToken ct)
    {
      MarkdownViewerInlineDocument.<>c__DisplayClass13_0 CS$<>8__locals1 = new MarkdownViewerInlineDocument.<>c__DisplayClass13_0();
      CS$<>8__locals1.ct = ct;
      CS$<>8__locals1.entry = entry;
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.sources = CS$<>8__locals1.entry.Prepared.RemoteImageSourcesToLoad;
      if (CS$<>8__locals1.sources.Count == 0)
      {
        return;
      }
      CS$<>8__locals1.dq = DispatcherQueue.GetForCurrentThread();
      CS$<>8__locals1.directory = this.baseDirectory;
      Task.Run(delegate
      {
        MarkdownViewerInlineDocument.<>c__DisplayClass13_0.<<LoadRemoteImages>b__0>d <<LoadRemoteImages>b__0>d;
        <<LoadRemoteImages>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<LoadRemoteImages>b__0>d.<>4__this = CS$<>8__locals1;
        <<LoadRemoteImages>b__0>d.<>1__state = -1;
        <<LoadRemoteImages>b__0>d.<>t__builder.Start<MarkdownViewerInlineDocument.<>c__DisplayClass13_0.<<LoadRemoteImages>b__0>d>(ref <<LoadRemoteImages>b__0>d);
        return <<LoadRemoteImages>b__0>d.<>t__builder.Task;
      }, CS$<>8__locals1.ct);
    }

    // Token: 0x04000E8A RID: 3722
    public const double TextScale = 0.875;

    // Token: 0x04000E8B RID: 3723
    [TupleElementNames(new string[] { "Key", "Entry" })]
    [Nullable(new byte[] { 1, 0, 1, 1 })]
    private static readonly List<ValueTuple<string, MarkdownViewerInlineDocument.CacheEntry>> RenderCache = new List<ValueTuple<string, MarkdownViewerInlineDocument.CacheEntry>>();

    // Token: 0x04000E8C RID: 3724
    private readonly MarkdownViewerTextView textView = new MarkdownViewerTextView(false, 0.875);

    // Token: 0x04000E8D RID: 3725
    private string markdown = "";

    // Token: 0x04000E8E RID: 3726
    [Nullable(2)]
    private string baseDirectory;

    // Token: 0x04000E8F RID: 3727
    [Nullable(2)]
    private CancellationTokenSource imageLoad;

    // Token: 0x020008F1 RID: 2289
    [Nullable(0)]
    private sealed class CacheEntry : IEquatable<MarkdownViewerInlineDocument.CacheEntry>
    {
      // Token: 0x06003B16 RID: 15126 RVA: 0x00693374 File Offset: 0x00693374
      public CacheEntry(IReadOnlyList<MarkdownViewerBlock> Blocks, MarkdownViewerPreparedDocument Prepared)
      {
        this.Blocks = Blocks;
        this.Prepared = Prepared;
        base..ctor();
      }

      // Token: 0x17000827 RID: 2087
      // (get) Token: 0x06003B17 RID: 15127 RVA: 0x0069338C File Offset: 0x0069338C
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(MarkdownViewerInlineDocument.CacheEntry);
        }
      }

      // Token: 0x17000828 RID: 2088
      // (get) Token: 0x06003B18 RID: 15128 RVA: 0x00693398 File Offset: 0x00693398
      // (set) Token: 0x06003B19 RID: 15129 RVA: 0x006933A0 File Offset: 0x006933A0
      public IReadOnlyList<MarkdownViewerBlock> Blocks { get; set; }

      // Token: 0x17000829 RID: 2089
      // (get) Token: 0x06003B1A RID: 15130 RVA: 0x006933AC File Offset: 0x006933AC
      // (set) Token: 0x06003B1B RID: 15131 RVA: 0x006933B4 File Offset: 0x006933B4
      public MarkdownViewerPreparedDocument Prepared { get; set; }

      // Token: 0x06003B1C RID: 15132 RVA: 0x006933C0 File Offset: 0x006933C0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("CacheEntry");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003B1D RID: 15133 RVA: 0x0069340C File Offset: 0x0069340C
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Blocks = ");
        builder.Append(this.Blocks);
        builder.Append(", Prepared = ");
        builder.Append(this.Prepared);
        return true;
      }

      // Token: 0x06003B1E RID: 15134 RVA: 0x00693448 File Offset: 0x00693448
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(MarkdownViewerInlineDocument.CacheEntry left, MarkdownViewerInlineDocument.CacheEntry right)
      {
        return !(left == right);
      }

      // Token: 0x06003B1F RID: 15135 RVA: 0x00693454 File Offset: 0x00693454
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(MarkdownViewerInlineDocument.CacheEntry left, MarkdownViewerInlineDocument.CacheEntry right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003B20 RID: 15136 RVA: 0x00693468 File Offset: 0x00693468
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<MarkdownViewerBlock>>.Default.GetHashCode(this.<Blocks>k__BackingField)) * -1521134295 + EqualityComparer<MarkdownViewerPreparedDocument>.Default.GetHashCode(this.<Prepared>k__BackingField);
      }

      // Token: 0x06003B21 RID: 15137 RVA: 0x006934A8 File Offset: 0x006934A8
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as MarkdownViewerInlineDocument.CacheEntry);
      }

      // Token: 0x06003B22 RID: 15138 RVA: 0x006934B8 File Offset: 0x006934B8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(MarkdownViewerInlineDocument.CacheEntry other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<MarkdownViewerBlock>>.Default.Equals(this.<Blocks>k__BackingField, other.<Blocks>k__BackingField) && EqualityComparer<MarkdownViewerPreparedDocument>.Default.Equals(this.<Prepared>k__BackingField, other.<Prepared>k__BackingField));
      }

      // Token: 0x06003B24 RID: 15140 RVA: 0x0069351C File Offset: 0x0069351C
      [CompilerGenerated]
      private CacheEntry(MarkdownViewerInlineDocument.CacheEntry original)
      {
        this.Blocks = original.<Blocks>k__BackingField;
        this.Prepared = original.<Prepared>k__BackingField;
      }

      // Token: 0x06003B25 RID: 15141 RVA: 0x0069353C File Offset: 0x0069353C
      [CompilerGenerated]
      public void Deconstruct(out IReadOnlyList<MarkdownViewerBlock> Blocks, out MarkdownViewerPreparedDocument Prepared)
      {
        Blocks = this.Blocks;
        Prepared = this.Prepared;
      }
    }
  }
}
