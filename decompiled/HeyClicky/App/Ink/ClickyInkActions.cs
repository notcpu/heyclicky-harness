using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using HeyClicky.Core.Ink;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000285 RID: 645
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkActions
  {
    // Token: 0x170004A5 RID: 1189
    // (get) Token: 0x06001B1B RID: 6939 RVA: 0x005EAB74 File Offset: 0x005EAB74
    // (set) Token: 0x06001B1C RID: 6940 RVA: 0x005EAB7C File Offset: 0x005EAB7C
    public Action<string> Send { get; set; } = delegate(string _)
    {
    };

    // Token: 0x170004A6 RID: 1190
    // (get) Token: 0x06001B1D RID: 6941 RVA: 0x005EAB88 File Offset: 0x005EAB88
    // (set) Token: 0x06001B1E RID: 6942 RVA: 0x005EAB90 File Offset: 0x005EAB90
    public Action<string> Open { get; set; } = delegate(string _)
    {
    };

    // Token: 0x170004A7 RID: 1191
    // (get) Token: 0x06001B1F RID: 6943 RVA: 0x005EAB9C File Offset: 0x005EAB9C
    // (set) Token: 0x06001B20 RID: 6944 RVA: 0x005EABA4 File Offset: 0x005EABA4
    public Func<string, string> ResolvePath { get; set; }

    // Token: 0x170004A8 RID: 1192
    // (get) Token: 0x06001B21 RID: 6945 RVA: 0x005EABB0 File Offset: 0x005EABB0
    // (set) Token: 0x06001B22 RID: 6946 RVA: 0x005EABB8 File Offset: 0x005EABB8
    public Action<string, string> Toggle { get; set; }

    // Token: 0x170004A9 RID: 1193
    // (get) Token: 0x06001B23 RID: 6947 RVA: 0x005EABC4 File Offset: 0x005EABC4
    // (set) Token: 0x06001B24 RID: 6948 RVA: 0x005EABCC File Offset: 0x005EABCC
    public Action<ClickyInkNoteAction> Perform { get; set; }

    // Token: 0x170004AA RID: 1194
    // (get) Token: 0x06001B25 RID: 6949 RVA: 0x005EABD8 File Offset: 0x005EABD8
    // (set) Token: 0x06001B26 RID: 6950 RVA: 0x005EABE0 File Offset: 0x005EABE0
    public ClickyInkActionFeedback Feedback { get; set; }

    // Token: 0x170004AB RID: 1195
    // (get) Token: 0x06001B27 RID: 6951 RVA: 0x005EABEC File Offset: 0x005EABEC
    // (set) Token: 0x06001B28 RID: 6952 RVA: 0x005EABF4 File Offset: 0x005EABF4
    public IReadOnlyList<ClickyInkRoutineControl> Routines { get; set; }

    // Token: 0x170004AC RID: 1196
    // (get) Token: 0x06001B29 RID: 6953 RVA: 0x005EAC00 File Offset: 0x005EAC00
    // (set) Token: 0x06001B2A RID: 6954 RVA: 0x005EAC08 File Offset: 0x005EAC08
    public Action<string, bool> SetRoutinePaused { get; set; }

    // Token: 0x170004AD RID: 1197
    // (get) Token: 0x06001B2B RID: 6955 RVA: 0x005EAC14 File Offset: 0x005EAC14
    // (set) Token: 0x06001B2C RID: 6956 RVA: 0x005EAC1C File Offset: 0x005EAC1C
    public Action<string, int> SetRoutineInterval { get; set; }

    // Token: 0x170004AE RID: 1198
    // (get) Token: 0x06001B2D RID: 6957 RVA: 0x005EAC28 File Offset: 0x005EAC28
    // (set) Token: 0x06001B2E RID: 6958 RVA: 0x005EAC30 File Offset: 0x005EAC30
    public Func<string, bool> RunRoutineNow { get; set; }

    // Token: 0x170004AF RID: 1199
    // (get) Token: 0x06001B2F RID: 6959 RVA: 0x005EAC3C File Offset: 0x005EAC3C
    // (set) Token: 0x06001B30 RID: 6960 RVA: 0x005EAC44 File Offset: 0x005EAC44
    public Action<string, ClickyInkDocument.Kind, JsonObject> Track { get; set; }

    // Token: 0x06001B31 RID: 6961 RVA: 0x005EAC50 File Offset: 0x005EAC50
    public ClickyInkActions()
    {
      Func<string, string> func;
      if ((func = ClickyInkActions.<>O.<0>__ExpandingTilde) == null)
      {
        func = (ClickyInkActions.<>O.<0>__ExpandingTilde = new Func<string, string>(ClickyInkPaths.ExpandingTilde));
      }
      this.ResolvePath = func;
      this.Toggle = delegate(string _, string _)
      {
      };
      this.Perform = delegate(ClickyInkNoteAction _)
      {
      };
      this.Feedback = new ClickyInkActionFeedback();
      this.Routines = Array.Empty<ClickyInkRoutineControl>();
      this.SetRoutinePaused = delegate(string _, bool _)
      {
      };
      this.SetRoutineInterval = delegate(string _, int _)
      {
      };
      this.RunRoutineNow = (string _) => false;
      this.Track = delegate(string _, ClickyInkDocument.Kind _, JsonObject _)
      {
      };
      base..ctor();
    }

    // Token: 0x02000853 RID: 2131
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04002348 RID: 9032
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<string, string> <0>__ExpandingTilde;
    }
  }
}
