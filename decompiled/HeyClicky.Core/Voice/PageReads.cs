using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.Core.Voice
{
  // Token: 0x0200007D RID: 125
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PageReads
  {
    // Token: 0x06000782 RID: 1922 RVA: 0x00365454 File Offset: 0x00365454
    public PageReads(IPageReader reader, [Nullable(2)] Func<DateTimeOffset> clock = null)
    {
      this.reader = reader;
      Func<DateTimeOffset> func = clock;
      if (clock == null && (func = PageReads.<>c.<>9__10_0) == null)
      {
        func = (PageReads.<>c.<>9__10_0 = () => DateTimeOffset.Now);
      }
      this.clock = func;
    }

    // Token: 0x170001AE RID: 430
    // (get) Token: 0x06000783 RID: 1923 RVA: 0x003654A4 File Offset: 0x003654A4
    // (set) Token: 0x06000784 RID: 1924 RVA: 0x003654AC File Offset: 0x003654AC
    public bool StartedBeforeTurn { get; private set; }

    // Token: 0x06000785 RID: 1925 RVA: 0x003654B8 File Offset: 0x003654B8
    public void BeginRead(bool beforeTurn = true)
    {
      CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
      Task<PageReference> task = this.SafeReferenceAsync(cancellationTokenSource.Token);
      Task<PageContext> task2 = this.ExtractWhenReferencedAsync(task, cancellationTokenSource.Token);
      object obj = this.gate;
      PageReads.PendingRead pendingRead;
      lock (obj)
      {
        pendingRead = this.pending;
        long num = this.generation + 1L;
        this.generation = num;
        this.pending = new PageReads.PendingRead(num, task, task2, this.clock(), cancellationTokenSource);
        this.StartedBeforeTurn = beforeTurn;
      }
      if (pendingRead != null)
      {
        pendingRead.Cancellation.Cancel();
      }
    }

    // Token: 0x06000786 RID: 1926 RVA: 0x00365564 File Offset: 0x00365564
    public void BeginReadForTextInput(Func<bool> isStillOpen)
    {
      object obj = this.gate;
      long at;
      lock (obj)
      {
        at = this.generation;
      }
      Task.Delay(300).ContinueWith(delegate(Task _)
      {
        object obj2 = this.gate;
        lock (obj2)
        {
          if (this.generation != at)
          {
            return;
          }
        }
        if (!isStillOpen())
        {
          return;
        }
        this.BeginRead(true);
      }, TaskScheduler.Default);
    }

    // Token: 0x06000787 RID: 1927 RVA: 0x003655E0 File Offset: 0x003655E0
    public void TextInputClosed()
    {
      object obj = this.gate;
      long at;
      lock (obj)
      {
        at = this.generation;
      }
      Task.Delay(5000).ContinueWith(delegate(Task _)
      {
        object obj2 = this.gate;
        lock (obj2)
        {
          if (this.generation != at || this.pending == null)
          {
            return;
          }
        }
        this.Discard();
      }, TaskScheduler.Default);
    }

    // Token: 0x06000788 RID: 1928 RVA: 0x00365654 File Offset: 0x00365654
    public void Discard()
    {
      object obj = this.gate;
      PageReads.PendingRead pendingRead;
      lock (obj)
      {
        pendingRead = this.pending;
        this.pending = null;
        this.generation += 1L;
        this.StartedBeforeTurn = false;
      }
      if (pendingRead != null)
      {
        pendingRead.Cancellation.Cancel();
      }
    }

    // Token: 0x06000789 RID: 1929 RVA: 0x003656C0 File Offset: 0x003656C0
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<PageContext> TakeForTurnAsync(double? waitSeconds = null, CancellationToken ct = default(CancellationToken))
    {
      object obj = this.gate;
      double wait;
      lock (obj)
      {
        wait = waitSeconds ?? (this.StartedBeforeTurn ? 0.8 : 1.6);
      }
      Task<PageReference> currentTask = this.SafeReferenceAsync(ct);
      ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter = Task.WhenAny(currentTask, Task.Delay(PageReads.ReferenceBudget, CancellationToken.None)).ConfigureAwait(false).GetAwaiter();
      if (!configuredTaskAwaiter.IsCompleted)
      {
        await configuredTaskAwaiter;
        ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
        configuredTaskAwaiter = configuredTaskAwaiter2;
        configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
      }
      bool flag2 = configuredTaskAwaiter.GetResult() == currentTask;
      PageReference current = (flag2 ? currentTask.Result : null);
      obj = this.gate;
      PageReads.PendingRead read;
      lock (obj)
      {
        read = this.pending;
      }
      if (read != null)
      {
        PageReference pageReference = (read.Reference.IsCompletedSuccessfully ? read.Reference.Result : null);
        if (flag2 && current == null)
        {
          this.Discard();
          return null;
        }
        if (!((this.clock() - read.StartedAt > PageReads.FreshFor) | (current != null && read.Reference.IsCompleted && ((pageReference != null) ? pageReference.Reference : null) != current.Reference)))
        {
          goto IL_0295;
        }
        this.BeginRead(false);
        obj = this.gate;
        lock (obj)
        {
          read = this.pending;
          goto IL_0295;
        }
      }
      if (flag2 && current == null)
      {
        return null;
      }
      this.BeginRead(false);
      obj = this.gate;
      lock (obj)
      {
        read = this.pending;
      }
      IL_0295:
      PageContext pageContext;
      if (read == null)
      {
        pageContext = null;
      }
      else
      {
        obj = this.gate;
        lock (obj)
        {
          if (this.pending == read)
          {
            this.pending = null;
            this.StartedBeforeTurn = false;
          }
        }
        PageContext pageContext2 = await PageReads.WithBudget<PageContext>(read.Context, TimeSpan.FromSeconds(wait)).ConfigureAwait(false);
        if (pageContext2 != null)
        {
          pageContext = pageContext2;
        }
        else
        {
          PageReference pageReference2 = (read.Reference.IsCompletedSuccessfully ? read.Reference.Result : current);
          pageContext = ((pageReference2 == null) ? null : PageContext.ReferenceOnly(pageReference2));
        }
      }
      return pageContext;
    }

    // Token: 0x0600078A RID: 1930 RVA: 0x00365714 File Offset: 0x00365714
    [return: Nullable(new byte[] { 1, 2 })]
    private async Task<PageReference> SafeReferenceAsync(CancellationToken ct)
    {
      PageReference pageReference;
      try
      {
        pageReference = await this.reader.CaptureReferenceAsync(ct).ConfigureAwait(false);
      }
      catch (Exception ex)
      {
        Log.Warn("PageRead", "reference: " + ex.Message);
        pageReference = null;
      }
      return pageReference;
    }

    // Token: 0x0600078B RID: 1931 RVA: 0x00365760 File Offset: 0x00365760
    [return: Nullable(new byte[] { 1, 2 })]
    private async Task<PageContext> ExtractWhenReferencedAsync([Nullable(new byte[] { 1, 2 })] Task<PageReference> referenceTask, CancellationToken ct)
    {
      PageReference pageReference = await referenceTask.ConfigureAwait(false);
      PageReference reference = pageReference;
      PageContext pageContext;
      if (reference == null || ct.IsCancellationRequested)
      {
        pageContext = null;
      }
      else
      {
        try
        {
          long started = Environment.TickCount64;
          PageContext pageContext2 = await this.reader.ExtractAsync(reference, ct).ConfigureAwait(false);
          string text = "PageRead";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 4);
          defaultInterpolatedStringHandler.AppendFormatted(pageContext2.Kind);
          defaultInterpolatedStringHandler.AppendLiteral(" read in ");
          defaultInterpolatedStringHandler.AppendFormatted<long>(Environment.TickCount64 - started);
          defaultInterpolatedStringHandler.AppendLiteral(" ms: ");
          string text2 = pageContext2.Text;
          defaultInterpolatedStringHandler.AppendFormatted<int>((text2 != null) ? text2.Length : 0);
          defaultInterpolatedStringHandler.AppendLiteral(" chars");
          defaultInterpolatedStringHandler.AppendFormatted(pageContext2.AccessDenied ? ", access denied" : "");
          Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
          pageContext = pageContext2;
        }
        catch (Exception ex)
        {
          Log.Warn("PageRead", "extract: " + ex.Message);
          pageContext = PageContext.ReferenceOnly(reference);
        }
      }
      return pageContext;
    }

    // Token: 0x0600078C RID: 1932 RVA: 0x003657B4 File Offset: 0x003657B4
    [return: Nullable(new byte[] { 1, 2 })]
    private static async Task<T> WithBudget<T>([Nullable(new byte[] { 1, 2 })] Task<T> task, TimeSpan budget) where T : class
    {
      ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter = Task.WhenAny(task, Task.Delay(budget)).ConfigureAwait(false).GetAwaiter();
      if (!configuredTaskAwaiter.IsCompleted)
      {
        await configuredTaskAwaiter;
        ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
        configuredTaskAwaiter = configuredTaskAwaiter2;
        configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
      }
      T t;
      if (configuredTaskAwaiter.GetResult() != task)
      {
        t = default(T);
      }
      else
      {
        t = (task.IsCompletedSuccessfully ? task.Result : default(T));
      }
      return t;
    }

    // Token: 0x0400039C RID: 924
    public static readonly TimeSpan FreshFor = TimeSpan.FromSeconds(120L);

    // Token: 0x0400039D RID: 925
    public const double WaitSecondsWhenStartedEarly = 0.8;

    // Token: 0x0400039E RID: 926
    public const double WaitSecondsAtTurn = 1.6;

    // Token: 0x0400039F RID: 927
    private static readonly TimeSpan ReferenceBudget = TimeSpan.FromMilliseconds(600L, 0L);

    // Token: 0x040003A0 RID: 928
    private readonly IPageReader reader;

    // Token: 0x040003A1 RID: 929
    private readonly Func<DateTimeOffset> clock;

    // Token: 0x040003A2 RID: 930
    private readonly object gate = new object();

    // Token: 0x040003A3 RID: 931
    [Nullable(2)]
    private PageReads.PendingRead pending;

    // Token: 0x040003A4 RID: 932
    private long generation;

    // Token: 0x0200033A RID: 826
    [Nullable(0)]
    private sealed class PendingRead : IEquatable<PageReads.PendingRead>
    {
      // Token: 0x0600281C RID: 10268 RVA: 0x00417AFC File Offset: 0x00417AFC
      public PendingRead(long Generation, [Nullable(new byte[] { 1, 2 })] Task<PageReference> Reference, [Nullable(new byte[] { 1, 2 })] Task<PageContext> Context, DateTimeOffset StartedAt, CancellationTokenSource Cancellation)
      {
        this.Generation = Generation;
        this.Reference = Reference;
        this.Context = Context;
        this.StartedAt = StartedAt;
        this.Cancellation = Cancellation;
        base..ctor();
      }

      // Token: 0x170007E3 RID: 2019
      // (get) Token: 0x0600281D RID: 10269 RVA: 0x00417B2C File Offset: 0x00417B2C
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(PageReads.PendingRead);
        }
      }

      // Token: 0x170007E4 RID: 2020
      // (get) Token: 0x0600281E RID: 10270 RVA: 0x00417B38 File Offset: 0x00417B38
      // (set) Token: 0x0600281F RID: 10271 RVA: 0x00417B40 File Offset: 0x00417B40
      public long Generation { get; set; }

      // Token: 0x170007E5 RID: 2021
      // (get) Token: 0x06002820 RID: 10272 RVA: 0x00417B4C File Offset: 0x00417B4C
      // (set) Token: 0x06002821 RID: 10273 RVA: 0x00417B54 File Offset: 0x00417B54
      [Nullable(new byte[] { 1, 2 })]
      public Task<PageReference> Reference
      {
        [return: Nullable(new byte[] { 1, 2 })]
        get;
        [param: Nullable(new byte[] { 1, 2 })]
        set;
      }

      // Token: 0x170007E6 RID: 2022
      // (get) Token: 0x06002822 RID: 10274 RVA: 0x00417B60 File Offset: 0x00417B60
      // (set) Token: 0x06002823 RID: 10275 RVA: 0x00417B68 File Offset: 0x00417B68
      [Nullable(new byte[] { 1, 2 })]
      public Task<PageContext> Context
      {
        [return: Nullable(new byte[] { 1, 2 })]
        get;
        [param: Nullable(new byte[] { 1, 2 })]
        set;
      }

      // Token: 0x170007E7 RID: 2023
      // (get) Token: 0x06002824 RID: 10276 RVA: 0x00417B74 File Offset: 0x00417B74
      // (set) Token: 0x06002825 RID: 10277 RVA: 0x00417B7C File Offset: 0x00417B7C
      public DateTimeOffset StartedAt { get; set; }

      // Token: 0x170007E8 RID: 2024
      // (get) Token: 0x06002826 RID: 10278 RVA: 0x00417B88 File Offset: 0x00417B88
      // (set) Token: 0x06002827 RID: 10279 RVA: 0x00417B90 File Offset: 0x00417B90
      public CancellationTokenSource Cancellation { get; set; }

      // Token: 0x06002828 RID: 10280 RVA: 0x00417B9C File Offset: 0x00417B9C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("PendingRead");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002829 RID: 10281 RVA: 0x00417BE8 File Offset: 0x00417BE8
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Generation = ");
        builder.Append(this.Generation.ToString());
        builder.Append(", Reference = ");
        builder.Append(this.Reference);
        builder.Append(", Context = ");
        builder.Append(this.Context);
        builder.Append(", StartedAt = ");
        builder.Append(this.StartedAt.ToString());
        builder.Append(", Cancellation = ");
        builder.Append(this.Cancellation);
        return true;
      }

      // Token: 0x0600282A RID: 10282 RVA: 0x00417C94 File Offset: 0x00417C94
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(PageReads.PendingRead left, PageReads.PendingRead right)
      {
        return !(left == right);
      }

      // Token: 0x0600282B RID: 10283 RVA: 0x00417CA0 File Offset: 0x00417CA0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(PageReads.PendingRead left, PageReads.PendingRead right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600282C RID: 10284 RVA: 0x00417CB4 File Offset: 0x00417CB4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<Generation>k__BackingField)) * -1521134295 + EqualityComparer<Task<PageReference>>.Default.GetHashCode(this.<Reference>k__BackingField)) * -1521134295 + EqualityComparer<Task<PageContext>>.Default.GetHashCode(this.<Context>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<StartedAt>k__BackingField)) * -1521134295 + EqualityComparer<CancellationTokenSource>.Default.GetHashCode(this.<Cancellation>k__BackingField);
      }

      // Token: 0x0600282D RID: 10285 RVA: 0x00417D44 File Offset: 0x00417D44
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as PageReads.PendingRead);
      }

      // Token: 0x0600282E RID: 10286 RVA: 0x00417D54 File Offset: 0x00417D54
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(PageReads.PendingRead other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<long>.Default.Equals(this.<Generation>k__BackingField, other.<Generation>k__BackingField) && EqualityComparer<Task<PageReference>>.Default.Equals(this.<Reference>k__BackingField, other.<Reference>k__BackingField) && EqualityComparer<Task<PageContext>>.Default.Equals(this.<Context>k__BackingField, other.<Context>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<StartedAt>k__BackingField, other.<StartedAt>k__BackingField) && EqualityComparer<CancellationTokenSource>.Default.Equals(this.<Cancellation>k__BackingField, other.<Cancellation>k__BackingField));
      }

      // Token: 0x06002830 RID: 10288 RVA: 0x00417E04 File Offset: 0x00417E04
      [CompilerGenerated]
      private PendingRead(PageReads.PendingRead original)
      {
        this.Generation = original.<Generation>k__BackingField;
        this.Reference = original.<Reference>k__BackingField;
        this.Context = original.<Context>k__BackingField;
        this.StartedAt = original.<StartedAt>k__BackingField;
        this.Cancellation = original.<Cancellation>k__BackingField;
      }

      // Token: 0x06002831 RID: 10289 RVA: 0x00417E54 File Offset: 0x00417E54
      [CompilerGenerated]
      public void Deconstruct(out long Generation, [Nullable(new byte[] { 1, 2 })] out Task<PageReference> Reference, [Nullable(new byte[] { 1, 2 })] out Task<PageContext> Context, out DateTimeOffset StartedAt, out CancellationTokenSource Cancellation)
      {
        Generation = this.Generation;
        Reference = this.Reference;
        Context = this.Context;
        StartedAt = this.StartedAt;
        Cancellation = this.Cancellation;
      }
    }
  }
}
