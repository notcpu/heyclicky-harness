using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Home;
using HeyClicky.Core.Notch;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000183 RID: 387
  [NullableContext(2)]
  [Nullable(0)]
  public static class NotchChat
  {
    // Token: 0x170003C0 RID: 960
    // (get) Token: 0x0600150A RID: 5386 RVA: 0x005B438C File Offset: 0x005B438C
    // (set) Token: 0x0600150B RID: 5387 RVA: 0x005B4394 File Offset: 0x005B4394
    public static ConversationHistory History { get; set; }

    // Token: 0x170003C1 RID: 961
    // (get) Token: 0x0600150C RID: 5388 RVA: 0x005B439C File Offset: 0x005B439C
    // (set) Token: 0x0600150D RID: 5389 RVA: 0x005B43A4 File Offset: 0x005B43A4
    public static ChatHandOffs HandOffs { get; set; }

    // Token: 0x170003C2 RID: 962
    // (get) Token: 0x0600150E RID: 5390 RVA: 0x005B43AC File Offset: 0x005B43AC
    // (set) Token: 0x0600150F RID: 5391 RVA: 0x005B43B4 File Offset: 0x005B43B4
    public static LessonService Lessons { get; set; }
  }
}
