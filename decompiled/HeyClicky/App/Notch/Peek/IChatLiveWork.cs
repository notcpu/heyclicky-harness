using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Notch;
using Microsoft.UI.Xaml;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000186 RID: 390
  [NullableContext(1)]
  public interface IChatLiveWork
  {
    // Token: 0x06001519 RID: 5401
    [return: Nullable(2)]
    FrameworkElement WorkCard(ChatHandOff entry);

    // Token: 0x0600151A RID: 5402 RVA: 0x005B43BC File Offset: 0x005B43BC
    [return: Nullable(2)]
    FrameworkElement WorkCard(ChatHandOff entry, bool hidesFinalizing)
    {
      return this.WorkCard(entry);
    }

    // Token: 0x0600151B RID: 5403 RVA: 0x005B43C8 File Offset: 0x005B43C8
    [return: Nullable(2)]
    FrameworkElement NextStepButton(ChatHandOff entry)
    {
      return null;
    }

    // Token: 0x0600151C RID: 5404
    bool IsWorking(ChatHandOff entry);

    // Token: 0x1400005D RID: 93
    // (add) Token: 0x0600151D RID: 5405
    // (remove) Token: 0x0600151E RID: 5406
    [Nullable(2)]
    event Action Changed;

    // Token: 0x0600151F RID: 5407
    [return: Nullable(2)]
    string DisplayName(ChatHandOff entry);

    // Token: 0x06001520 RID: 5408
    [return: Nullable(2)]
    FrameworkElement Chip(ChatHandOff entry, double size);

    // Token: 0x06001521 RID: 5409
    [return: Nullable(2)]
    FrameworkElement FileCards(IReadOnlyList<string> entries, Action<string> open);

    // Token: 0x06001522 RID: 5410 RVA: 0x005B43CC File Offset: 0x005B43CC
    [return: Nullable(2)]
    FrameworkElement PlanFold(ChatHandOff entry, Action onFinished)
    {
      return null;
    }
  }
}
