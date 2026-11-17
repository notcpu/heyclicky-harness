using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.Agents.Jobs;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002E6 RID: 742
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ArcDockHost
  {
    // Token: 0x170005C1 RID: 1473
    // (get) Token: 0x06001FAF RID: 8111 RVA: 0x006109B8 File Offset: 0x006109B8
    // (set) Token: 0x06001FB0 RID: 8112 RVA: 0x006109C0 File Offset: 0x006109C0
    [TupleElementNames(new string[] { "X", "Y", "W", "H" })]
    [Nullable(new byte[] { 1, 0 })]
    public Func<ValueTuple<int, int, int, int>?> NotchSurfacePx
    {
      [return: TupleElementNames(new string[] { "X", "Y", "W", "H" })]
      [return: Nullable(new byte[] { 1, 0 })]
      get;
      [param: TupleElementNames(new string[] { "X", "Y", "W", "H" })]
      [param: Nullable(new byte[] { 1, 0 })]
      set;
    } = () => null;

    // Token: 0x170005C2 RID: 1474
    // (get) Token: 0x06001FB1 RID: 8113 RVA: 0x006109CC File Offset: 0x006109CC
    // (set) Token: 0x06001FB2 RID: 8114 RVA: 0x006109D4 File Offset: 0x006109D4
    public Func<bool> NotchIsExpanded { get; set; } = () => false;

    // Token: 0x170005C3 RID: 1475
    // (get) Token: 0x06001FB3 RID: 8115 RVA: 0x006109E0 File Offset: 0x006109E0
    // (set) Token: 0x06001FB4 RID: 8116 RVA: 0x006109E8 File Offset: 0x006109E8
    public Func<bool> NotchIsOpenOnHome { get; set; } = () => false;

    // Token: 0x170005C4 RID: 1476
    // (get) Token: 0x06001FB5 RID: 8117 RVA: 0x006109F4 File Offset: 0x006109F4
    // (set) Token: 0x06001FB6 RID: 8118 RVA: 0x006109FC File Offset: 0x006109FC
    public Func<bool> NotchTabIsHome { get; set; } = () => true;

    // Token: 0x170005C5 RID: 1477
    // (get) Token: 0x06001FB7 RID: 8119 RVA: 0x00610A08 File Offset: 0x00610A08
    // (set) Token: 0x06001FB8 RID: 8120 RVA: 0x00610A10 File Offset: 0x00610A10
    public Func<bool> NotchHoldsOpenWithoutArc { get; set; } = () => false;

    // Token: 0x170005C6 RID: 1478
    // (get) Token: 0x06001FB9 RID: 8121 RVA: 0x00610A1C File Offset: 0x00610A1C
    // (set) Token: 0x06001FBA RID: 8122 RVA: 0x00610A24 File Offset: 0x00610A24
    public Action<bool> SetNotchHeldOpenByArc { get; set; } = delegate(bool _)
    {
    };

    // Token: 0x170005C7 RID: 1479
    // (get) Token: 0x06001FBB RID: 8123 RVA: 0x00610A30 File Offset: 0x00610A30
    // (set) Token: 0x06001FBC RID: 8124 RVA: 0x00610A38 File Offset: 0x00610A38
    public Action OpenNotchHome { get; set; } = delegate
    {
    };

    // Token: 0x170005C8 RID: 1480
    // (get) Token: 0x06001FBD RID: 8125 RVA: 0x00610A44 File Offset: 0x00610A44
    // (set) Token: 0x06001FBE RID: 8126 RVA: 0x00610A4C File Offset: 0x00610A4C
    public Action DismissNotch { get; set; } = delegate
    {
    };

    // Token: 0x170005C9 RID: 1481
    // (get) Token: 0x06001FBF RID: 8127 RVA: 0x00610A58 File Offset: 0x00610A58
    // (set) Token: 0x06001FC0 RID: 8128 RVA: 0x00610A60 File Offset: 0x00610A60
    public Action<string> OpenAgentInWindow { get; set; } = delegate(string _)
    {
    };

    // Token: 0x170005CA RID: 1482
    // (get) Token: 0x06001FC1 RID: 8129 RVA: 0x00610A6C File Offset: 0x00610A6C
    // (set) Token: 0x06001FC2 RID: 8130 RVA: 0x00610A74 File Offset: 0x00610A74
    public Func<bool> IsFullscreenOnDockDisplay { get; set; } = () => false;

    // Token: 0x170005CB RID: 1483
    // (get) Token: 0x06001FC3 RID: 8131 RVA: 0x00610A80 File Offset: 0x00610A80
    // (set) Token: 0x06001FC4 RID: 8132 RVA: 0x00610A88 File Offset: 0x00610A88
    [Nullable(new byte[] { 1, 2, 2 })]
    public Func<string, string, bool> HasLatestReply
    {
      [return: Nullable(new byte[] { 1, 2, 2 })]
      get;
      [param: Nullable(new byte[] { 1, 2, 2 })]
      set;
    } = (string _, string _) => false;

    // Token: 0x170005CC RID: 1484
    // (get) Token: 0x06001FC5 RID: 8133 RVA: 0x00610A94 File Offset: 0x00610A94
    // (set) Token: 0x06001FC6 RID: 8134 RVA: 0x00610A9C File Offset: 0x00610A9C
    [Nullable(new byte[] { 1, 2, 2 })]
    public Action<string, string> ShowFullReply
    {
      [return: Nullable(new byte[] { 1, 2, 2 })]
      get;
      [param: Nullable(new byte[] { 1, 2, 2 })]
      set;
    } = delegate(string _, string _)
    {
    };

    // Token: 0x170005CD RID: 1485
    // (get) Token: 0x06001FC7 RID: 8135 RVA: 0x00610AA8 File Offset: 0x00610AA8
    // (set) Token: 0x06001FC8 RID: 8136 RVA: 0x00610AB0 File Offset: 0x00610AB0
    [Nullable(new byte[] { 1, 2, 2 })]
    public Action<string, string> ShowChat
    {
      [return: Nullable(new byte[] { 1, 2, 2 })]
      get;
      [param: Nullable(new byte[] { 1, 2, 2 })]
      set;
    } = delegate(string _, string _)
    {
    };

    // Token: 0x170005CE RID: 1486
    // (get) Token: 0x06001FC9 RID: 8137 RVA: 0x00610ABC File Offset: 0x00610ABC
    // (set) Token: 0x06001FCA RID: 8138 RVA: 0x00610AC4 File Offset: 0x00610AC4
    public Action RequestComposerFocus { get; set; } = delegate
    {
    };

    // Token: 0x170005CF RID: 1487
    // (get) Token: 0x06001FCB RID: 8139 RVA: 0x00610AD0 File Offset: 0x00610AD0
    // (set) Token: 0x06001FCC RID: 8140 RVA: 0x00610AD8 File Offset: 0x00610AD8
    public Func<string, DateTimeOffset?> LatestChatEventAt { get; set; } = (string _) => null;

    // Token: 0x170005D0 RID: 1488
    // (get) Token: 0x06001FCD RID: 8141 RVA: 0x00610AE4 File Offset: 0x00610AE4
    // (set) Token: 0x06001FCE RID: 8142 RVA: 0x00610AEC File Offset: 0x00610AEC
    [Nullable(new byte[] { 1, 2, 2, 2 })]
    public Func<string, string, ClickyAnswer> LatestReplyAnswer
    {
      [return: Nullable(new byte[] { 1, 2, 2, 2 })]
      get;
      [param: Nullable(new byte[] { 1, 2, 2, 2 })]
      set;
    } = (string _, string _) => null;

    // Token: 0x170005D1 RID: 1489
    // (get) Token: 0x06001FCF RID: 8143 RVA: 0x00610AF8 File Offset: 0x00610AF8
    // (set) Token: 0x06001FD0 RID: 8144 RVA: 0x00610B00 File Offset: 0x00610B00
    public Func<string, bool> OpensInNotchDocumentViewer { get; set; } = (string _) => false;

    // Token: 0x170005D2 RID: 1490
    // (get) Token: 0x06001FD1 RID: 8145 RVA: 0x00610B0C File Offset: 0x00610B0C
    // (set) Token: 0x06001FD2 RID: 8146 RVA: 0x00610B14 File Offset: 0x00610B14
    [Nullable(new byte[] { 1, 1, 2, 2 })]
    public Func<string, string, string, bool> OpenInNotchViewer
    {
      [return: Nullable(new byte[] { 1, 1, 2, 2 })]
      get;
      [param: Nullable(new byte[] { 1, 1, 2, 2 })]
      set;
    } = ([Nullable(1)] string _, string _, string _) => false;

    // Token: 0x170005D3 RID: 1491
    // (get) Token: 0x06001FD3 RID: 8147 RVA: 0x00610B20 File Offset: 0x00610B20
    // (set) Token: 0x06001FD4 RID: 8148 RVA: 0x00610B28 File Offset: 0x00610B28
    [Nullable(new byte[] { 2, 1, 1 })]
    public Action<string, string> PresentNotchUpdate
    {
      [return: Nullable(new byte[] { 2, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1 })]
      set;
    }

    // Token: 0x170005D4 RID: 1492
    // (get) Token: 0x06001FD5 RID: 8149 RVA: 0x00610B34 File Offset: 0x00610B34
    // (set) Token: 0x06001FD6 RID: 8150 RVA: 0x00610B3C File Offset: 0x00610B3C
    public Action<string> DismissNotchUpdate { get; set; } = delegate(string _)
    {
    };

    // Token: 0x170005D5 RID: 1493
    // (get) Token: 0x06001FD7 RID: 8151 RVA: 0x00610B48 File Offset: 0x00610B48
    // (set) Token: 0x06001FD8 RID: 8152 RVA: 0x00610B50 File Offset: 0x00610B50
    public Func<bool> HasCompletedOnboarding { get; set; } = () => true;

    // Token: 0x170005D6 RID: 1494
    // (get) Token: 0x06001FD9 RID: 8153 RVA: 0x00610B5C File Offset: 0x00610B5C
    // (set) Token: 0x06001FDA RID: 8154 RVA: 0x00610B64 File Offset: 0x00610B64
    public Func<bool> OnboardingHidesDock { get; set; } = () => false;

    // Token: 0x170005D7 RID: 1495
    // (get) Token: 0x06001FDB RID: 8155 RVA: 0x00610B70 File Offset: 0x00610B70
    // (set) Token: 0x06001FDC RID: 8156 RVA: 0x00610B78 File Offset: 0x00610B78
    public Func<bool> SuppressesVisualSurfaces { get; set; } = () => false;

    // Token: 0x170005D8 RID: 1496
    // (get) Token: 0x06001FDD RID: 8157 RVA: 0x00610B84 File Offset: 0x00610B84
    // (set) Token: 0x06001FDE RID: 8158 RVA: 0x00610B8C File Offset: 0x00610B8C
    public Func<bool> IsTourRunning { get; set; } = () => false;

    // Token: 0x170005D9 RID: 1497
    // (get) Token: 0x06001FDF RID: 8159 RVA: 0x00610B98 File Offset: 0x00610B98
    // (set) Token: 0x06001FE0 RID: 8160 RVA: 0x00610BA0 File Offset: 0x00610BA0
    public Func<bool> PlaysSounds { get; set; } = () => true;

    // Token: 0x170005DA RID: 1498
    // (get) Token: 0x06001FE1 RID: 8161 RVA: 0x00610BAC File Offset: 0x00610BAC
    // (set) Token: 0x06001FE2 RID: 8162 RVA: 0x00610BB4 File Offset: 0x00610BB4
    public Func<string> VoiceHoldDescription { get; set; } = () => "Ctrl + Alt";

    // Token: 0x170005DB RID: 1499
    // (get) Token: 0x06001FE3 RID: 8163 RVA: 0x00610BC0 File Offset: 0x00610BC0
    // (set) Token: 0x06001FE4 RID: 8164 RVA: 0x00610BC8 File Offset: 0x00610BC8
    public Func<bool> IsListening { get; set; } = () => false;

    // Token: 0x170005DC RID: 1500
    // (get) Token: 0x06001FE5 RID: 8165 RVA: 0x00610BD4 File Offset: 0x00610BD4
    // (set) Token: 0x06001FE6 RID: 8166 RVA: 0x00610BDC File Offset: 0x00610BDC
    public Func<IReadOnlyList<string>> UnreadClickySlugs { get; set; } = () => Array.Empty<string>();

    // Token: 0x170005DD RID: 1501
    // (get) Token: 0x06001FE7 RID: 8167 RVA: 0x00610BE8 File Offset: 0x00610BE8
    // (set) Token: 0x06001FE8 RID: 8168 RVA: 0x00610BF0 File Offset: 0x00610BF0
    public Action<string> MarkClickyRead { get; set; } = delegate(string _)
    {
    };

    // Token: 0x170005DE RID: 1502
    // (get) Token: 0x06001FE9 RID: 8169 RVA: 0x00610BFC File Offset: 0x00610BFC
    // (set) Token: 0x06001FEA RID: 8170 RVA: 0x00610C04 File Offset: 0x00610C04
    public Func<ClickyJob, bool> StartWaitingJob { get; set; } = (ClickyJob _) => false;

    // Token: 0x170005DF RID: 1503
    // (get) Token: 0x06001FEB RID: 8171 RVA: 0x00610C10 File Offset: 0x00610C10
    // (set) Token: 0x06001FEC RID: 8172 RVA: 0x00610C18 File Offset: 0x00610C18
    [Nullable(new byte[] { 1, 2 })]
    public Func<ISet<Guid>> LiveSuggestionCardIds
    {
      [return: Nullable(new byte[] { 1, 2 })]
      get;
      [param: Nullable(new byte[] { 1, 2 })]
      set;
    } = () => null;

    // Token: 0x170005E0 RID: 1504
    // (get) Token: 0x06001FED RID: 8173 RVA: 0x00610C24 File Offset: 0x00610C24
    // (set) Token: 0x06001FEE RID: 8174 RVA: 0x00610C2C File Offset: 0x00610C2C
    [Nullable(new byte[] { 1, 1, 2 })]
    public Func<string, string> NoteTitle
    {
      [return: Nullable(new byte[] { 1, 1, 2 })]
      get;
      [param: Nullable(new byte[] { 1, 1, 2 })]
      set;
    } = (string _) => null;

    // Token: 0x170005E1 RID: 1505
    // (get) Token: 0x06001FEF RID: 8175 RVA: 0x00610C38 File Offset: 0x00610C38
    // (set) Token: 0x06001FF0 RID: 8176 RVA: 0x00610C40 File Offset: 0x00610C40
    [Nullable(new byte[] { 1, 1, 2 })]
    public Func<string, ClickyArcPlan> NoteChecklistPlan
    {
      [return: Nullable(new byte[] { 1, 1, 2 })]
      get;
      [param: Nullable(new byte[] { 1, 1, 2 })]
      set;
    } = (string _) => null;
  }
}
