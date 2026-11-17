using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000246 RID: 582
  [NullableContext(1)]
  [Nullable(0)]
  public class InkObjectState
  {
    // Token: 0x17000486 RID: 1158
    // (get) Token: 0x060019F5 RID: 6645 RVA: 0x005DBEB0 File Offset: 0x005DBEB0
    public static double Now
    {
      get
      {
        return InkObjectState.clock.Elapsed.TotalSeconds;
      }
    }

    // Token: 0x060019F6 RID: 6646 RVA: 0x005DBED0 File Offset: 0x005DBED0
    public static T For<[Nullable(0)] T>(ClickyInkLiveNote.Block block, string kind) where T : InkObjectState, new()
    {
      object obj = ((block.Stamps.Count > 0) ? block.Stamps[0].Value : block);
      ClickyInkEnvironment clickyInkEnvironment = ClickyInkEnvironment.Current;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 4);
      defaultInterpolatedStringHandler.AppendFormatted(kind);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(block.Id);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(clickyInkEnvironment.IsWindowPreview ? "preview" : "");
      defaultInterpolatedStringHandler.AppendFormatted(clickyInkEnvironment.IsGlancePreview ? "glance" : "");
      string text = defaultInterpolatedStringHandler.ToStringAndClear();
      Dictionary<string, InkObjectState> orCreateValue = InkObjectState.states.GetOrCreateValue(obj);
      InkObjectState inkObjectState;
      if (orCreateValue.TryGetValue(text, out inkObjectState))
      {
        T t = inkObjectState as T;
        if (t != null && inkObjectState.generation == InkObjectState.currentGeneration)
        {
          return t;
        }
      }
      T t2 = new T();
      t2.generation = InkObjectState.currentGeneration;
      T t3 = t2;
      orCreateValue[text] = t3;
      return t3;
    }

    // Token: 0x060019F7 RID: 6647 RVA: 0x005DBFE4 File Offset: 0x005DBFE4
    public static void ResetAll()
    {
      InkObjectState.currentGeneration++;
    }

    // Token: 0x060019F8 RID: 6648 RVA: 0x005DBFF4 File Offset: 0x005DBFF4
    internal void Attach(InkObjectHost h)
    {
      this.host = new WeakReference<InkObjectHost>(h);
    }

    // Token: 0x060019F9 RID: 6649 RVA: 0x005DC004 File Offset: 0x005DC004
    public void Changed()
    {
      WeakReference<InkObjectHost> weakReference = this.host;
      InkObjectHost inkObjectHost;
      if (weakReference != null && weakReference.TryGetTarget(out inkObjectHost))
      {
        inkObjectHost.Refresh();
      }
    }

    // Token: 0x060019FA RID: 6650 RVA: 0x005DC030 File Offset: 0x005DC030
    public void Animate(double seconds)
    {
      this.animateUntil = Math.Max(this.animateUntil, InkObjectState.Now + seconds);
      if (this.ticking)
      {
        return;
      }
      this.ticking = true;
      CompositionTarget.Rendering += this.Tick;
    }

    // Token: 0x060019FB RID: 6651 RVA: 0x005DC06C File Offset: 0x005DC06C
    private void Tick([Nullable(2)] object sender, object e)
    {
      this.Changed();
      if (InkObjectState.Now < this.animateUntil)
      {
        WeakReference<InkObjectHost> weakReference = this.host;
        InkObjectHost inkObjectHost;
        if (weakReference != null && weakReference.TryGetTarget(out inkObjectHost))
        {
          return;
        }
      }
      CompositionTarget.Rendering -= this.Tick;
      this.ticking = false;
      this.Changed();
    }

    // Token: 0x060019FC RID: 6652 RVA: 0x005DC0C4 File Offset: 0x005DC0C4
    public TField Field<[Nullable(0)] TField>(string key, Func<TField> make) where TField : FrameworkElement
    {
      FrameworkElement frameworkElement;
      TField tfield;
      if (this.fields.TryGetValue(key, out frameworkElement))
      {
        tfield = frameworkElement as TField;
        if (tfield != null)
        {
          goto IL_003D;
        }
      }
      tfield = make();
      this.fields[key] = tfield;
      IL_003D:
      InkViews.Detach(tfield);
      return tfield;
    }

    // Token: 0x04000C7F RID: 3199
    private static readonly Stopwatch clock = Stopwatch.StartNew();

    // Token: 0x04000C80 RID: 3200
    public const double LongAgo = -1000.0;

    // Token: 0x04000C81 RID: 3201
    private static readonly ConditionalWeakTable<object, Dictionary<string, InkObjectState>> states = new ConditionalWeakTable<object, Dictionary<string, InkObjectState>>();

    // Token: 0x04000C82 RID: 3202
    private static int currentGeneration;

    // Token: 0x04000C83 RID: 3203
    private int generation;

    // Token: 0x04000C84 RID: 3204
    [Nullable(new byte[] { 2, 1 })]
    private WeakReference<InkObjectHost> host;

    // Token: 0x04000C85 RID: 3205
    private double animateUntil;

    // Token: 0x04000C86 RID: 3206
    private bool ticking;

    // Token: 0x04000C87 RID: 3207
    private readonly Dictionary<string, FrameworkElement> fields = new Dictionary<string, FrameworkElement>();
  }
}
