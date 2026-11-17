using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.Notch.Surfaces;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x0200018C RID: 396
  [NullableContext(2)]
  [Nullable(0)]
  public static class NotchExtensions
  {
    // Token: 0x170003E2 RID: 994
    // (get) Token: 0x06001588 RID: 5512 RVA: 0x005B537C File Offset: 0x005B537C
    // (set) Token: 0x06001589 RID: 5513 RVA: 0x005B5384 File Offset: 0x005B5384
    public static INotchHomeDock HomeDock { get; set; }

    // Token: 0x170003E3 RID: 995
    // (get) Token: 0x0600158A RID: 5514 RVA: 0x005B538C File Offset: 0x005B538C
    // (set) Token: 0x0600158B RID: 5515 RVA: 0x005B5394 File Offset: 0x005B5394
    public static IChatLiveWork LiveWork { get; set; }

    // Token: 0x170003E4 RID: 996
    // (get) Token: 0x0600158C RID: 5516 RVA: 0x005B539C File Offset: 0x005B539C
    // (set) Token: 0x0600158D RID: 5517 RVA: 0x005B53A4 File Offset: 0x005B53A4
    public static INotchUpdateContentSource UpdateContent { get; set; }

    // Token: 0x170003E5 RID: 997
    // (get) Token: 0x0600158E RID: 5518 RVA: 0x005B53AC File Offset: 0x005B53AC
    // (set) Token: 0x0600158F RID: 5519 RVA: 0x005B53B4 File Offset: 0x005B53B4
    [Nullable(new byte[] { 2, 1 })]
    public static Action<NotchUpdateContent> AnnounceFinishedWorkOnRequest
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x170003E6 RID: 998
    // (get) Token: 0x06001590 RID: 5520 RVA: 0x005B53BC File Offset: 0x005B53BC
    // (set) Token: 0x06001591 RID: 5521 RVA: 0x005B53C4 File Offset: 0x005B53C4
    [Nullable(new byte[] { 2, 1 })]
    public static Action<NotchUpdateContent> MarkFinishedWorkSeen
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x170003E7 RID: 999
    // (get) Token: 0x06001592 RID: 5522 RVA: 0x005B53CC File Offset: 0x005B53CC
    // (set) Token: 0x06001593 RID: 5523 RVA: 0x005B53D4 File Offset: 0x005B53D4
    [Nullable(new byte[] { 2, 1 })]
    public static Func<NotchRestingMarkState> RestingMarkState
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x1400005F RID: 95
    // (add) Token: 0x06001594 RID: 5524 RVA: 0x005B53DC File Offset: 0x005B53DC
    // (remove) Token: 0x06001595 RID: 5525 RVA: 0x005B5410 File Offset: 0x005B5410
    public static event Action RestingMarkChanged;

    // Token: 0x06001596 RID: 5526 RVA: 0x005B5444 File Offset: 0x005B5444
    public static void RaiseRestingMarkChanged()
    {
      Action restingMarkChanged = NotchExtensions.RestingMarkChanged;
      if (restingMarkChanged == null)
      {
        return;
      }
      restingMarkChanged();
    }

    // Token: 0x170003E8 RID: 1000
    // (get) Token: 0x06001597 RID: 5527 RVA: 0x005B5458 File Offset: 0x005B5458
    // (set) Token: 0x06001598 RID: 5528 RVA: 0x005B5460 File Offset: 0x005B5460
    [Nullable(1)]
    public static IReadOnlyList<string> ShowcaseSampleIntegrationIds
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = Array.Empty<string>();

    // Token: 0x14000060 RID: 96
    // (add) Token: 0x06001599 RID: 5529 RVA: 0x005B5468 File Offset: 0x005B5468
    // (remove) Token: 0x0600159A RID: 5530 RVA: 0x005B549C File Offset: 0x005B549C
    public static event Action ShowcaseChanged;

    // Token: 0x0600159B RID: 5531 RVA: 0x005B54D0 File Offset: 0x005B54D0
    public static void RaiseShowcaseChanged()
    {
      Action showcaseChanged = NotchExtensions.ShowcaseChanged;
      if (showcaseChanged == null)
      {
        return;
      }
      showcaseChanged();
    }

    // Token: 0x0600159C RID: 5532 RVA: 0x005B54E4 File Offset: 0x005B54E4
    [NullableContext(1)]
    public static void RegisterDocumentRenderer(INotchDocumentRenderer renderer)
    {
      NotchExtensions.renderers.Insert(0, renderer);
    }

    // Token: 0x170003E9 RID: 1001
    // (get) Token: 0x0600159D RID: 5533 RVA: 0x005B54F4 File Offset: 0x005B54F4
    [Nullable(1)]
    public static IReadOnlyList<INotchDocumentRenderer> DocumentRenderers
    {
      [NullableContext(1)]
      get
      {
        return NotchExtensions.renderers;
      }
    }

    // Token: 0x04000A49 RID: 2633
    [Nullable(1)]
    private static readonly List<INotchDocumentRenderer> renderers = new List<INotchDocumentRenderer>();
  }
}
