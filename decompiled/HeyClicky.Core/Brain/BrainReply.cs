using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001FF RID: 511
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class BrainReply
  {
    // Token: 0x17000684 RID: 1668
    // (get) Token: 0x06001DA8 RID: 7592 RVA: 0x003E98F0 File Offset: 0x003E98F0
    // (set) Token: 0x06001DA9 RID: 7593 RVA: 0x003E98F8 File Offset: 0x003E98F8
    [Nullable(1)]
    public string Code
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = "";

    // Token: 0x17000685 RID: 1669
    // (get) Token: 0x06001DAA RID: 7594 RVA: 0x003E9904 File Offset: 0x003E9904
    // (set) Token: 0x06001DAB RID: 7595 RVA: 0x003E990C File Offset: 0x003E990C
    [Nullable(1)]
    public List<BrainComputerCall> ComputerCalls
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = new List<BrainComputerCall>();

    // Token: 0x17000686 RID: 1670
    // (get) Token: 0x06001DAC RID: 7596 RVA: 0x003E9918 File Offset: 0x003E9918
    // (set) Token: 0x06001DAD RID: 7597 RVA: 0x003E9920 File Offset: 0x003E9920
    public string SpokenSentence { get; set; }

    // Token: 0x17000687 RID: 1671
    // (get) Token: 0x06001DAE RID: 7598 RVA: 0x003E992C File Offset: 0x003E992C
    // (set) Token: 0x06001DAF RID: 7599 RVA: 0x003E9934 File Offset: 0x003E9934
    public string ScreenshotFile { get; set; }

    // Token: 0x17000688 RID: 1672
    // (get) Token: 0x06001DB0 RID: 7600 RVA: 0x003E9940 File Offset: 0x003E9940
    // (set) Token: 0x06001DB1 RID: 7601 RVA: 0x003E9948 File Offset: 0x003E9948
    public string ErrorCode { get; set; }

    // Token: 0x17000689 RID: 1673
    // (get) Token: 0x06001DB2 RID: 7602 RVA: 0x003E9954 File Offset: 0x003E9954
    // (set) Token: 0x06001DB3 RID: 7603 RVA: 0x003E995C File Offset: 0x003E995C
    public string DriveLeaseId { get; set; }

    // Token: 0x1700068A RID: 1674
    // (get) Token: 0x06001DB4 RID: 7604 RVA: 0x003E9968 File Offset: 0x003E9968
    // (set) Token: 0x06001DB5 RID: 7605 RVA: 0x003E9970 File Offset: 0x003E9970
    public int DriveMessagesRefunded { get; set; }

    // Token: 0x1700068B RID: 1675
    // (get) Token: 0x06001DB6 RID: 7606 RVA: 0x003E997C File Offset: 0x003E997C
    [Nullable(1)]
    public List<JsonNode> Items
    {
      [NullableContext(1)]
      get;
    } = new List<JsonNode>();

    // Token: 0x1700068C RID: 1676
    // (get) Token: 0x06001DB7 RID: 7607 RVA: 0x003E9984 File Offset: 0x003E9984
    [Nullable(1)]
    public List<string> SearchRecords
    {
      [NullableContext(1)]
      get;
    } = new List<string>();

    // Token: 0x1700068D RID: 1677
    // (get) Token: 0x06001DB8 RID: 7608 RVA: 0x003E998C File Offset: 0x003E998C
    // (set) Token: 0x06001DB9 RID: 7609 RVA: 0x003E9994 File Offset: 0x003E9994
    [Nullable(1)]
    public List<BrainToolCall> ToolCalls
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = new List<BrainToolCall>();

    // Token: 0x1700068E RID: 1678
    // (get) Token: 0x06001DBA RID: 7610 RVA: 0x003E99A0 File Offset: 0x003E99A0
    // (set) Token: 0x06001DBB RID: 7611 RVA: 0x003E99A8 File Offset: 0x003E99A8
    public string UserLayer { get; set; }

    // Token: 0x1700068F RID: 1679
    // (get) Token: 0x06001DBC RID: 7612 RVA: 0x003E99B4 File Offset: 0x003E99B4
    // (set) Token: 0x06001DBD RID: 7613 RVA: 0x003E99BC File Offset: 0x003E99BC
    public string UserLayerSignature { get; set; }

    // Token: 0x17000690 RID: 1680
    // (get) Token: 0x06001DBE RID: 7614 RVA: 0x003E99C8 File Offset: 0x003E99C8
    // (set) Token: 0x06001DBF RID: 7615 RVA: 0x003E99D0 File Offset: 0x003E99D0
    public string ModelIdentifier { get; set; }

    // Token: 0x17000691 RID: 1681
    // (get) Token: 0x06001DC0 RID: 7616 RVA: 0x003E99DC File Offset: 0x003E99DC
    // (set) Token: 0x06001DC1 RID: 7617 RVA: 0x003E99E4 File Offset: 0x003E99E4
    public int? GuidedMaxRounds { get; set; }

    // Token: 0x17000692 RID: 1682
    // (get) Token: 0x06001DC2 RID: 7618 RVA: 0x003E99F0 File Offset: 0x003E99F0
    // (set) Token: 0x06001DC3 RID: 7619 RVA: 0x003E99F8 File Offset: 0x003E99F8
    public int? FirstTokenMilliseconds { get; set; }

    // Token: 0x17000693 RID: 1683
    // (get) Token: 0x06001DC4 RID: 7620 RVA: 0x003E9A04 File Offset: 0x003E9A04
    // (set) Token: 0x06001DC5 RID: 7621 RVA: 0x003E9A0C File Offset: 0x003E9A0C
    public int TotalMilliseconds { get; set; }

    // Token: 0x17000694 RID: 1684
    // (get) Token: 0x06001DC6 RID: 7622 RVA: 0x003E9A18 File Offset: 0x003E9A18
    // (set) Token: 0x06001DC7 RID: 7623 RVA: 0x003E9A20 File Offset: 0x003E9A20
    public double? CostUsd { get; set; }

    // Token: 0x17000695 RID: 1685
    // (get) Token: 0x06001DC8 RID: 7624 RVA: 0x003E9A2C File Offset: 0x003E9A2C
    // (set) Token: 0x06001DC9 RID: 7625 RVA: 0x003E9A34 File Offset: 0x003E9A34
    public int LastInputTokens { get; set; }

    // Token: 0x17000696 RID: 1686
    // (get) Token: 0x06001DCA RID: 7626 RVA: 0x003E9A40 File Offset: 0x003E9A40
    // (set) Token: 0x06001DCB RID: 7627 RVA: 0x003E9A48 File Offset: 0x003E9A48
    public int InputTokens { get; set; }

    // Token: 0x17000697 RID: 1687
    // (get) Token: 0x06001DCC RID: 7628 RVA: 0x003E9A54 File Offset: 0x003E9A54
    // (set) Token: 0x06001DCD RID: 7629 RVA: 0x003E9A5C File Offset: 0x003E9A5C
    public bool KeepsReasoning { get; set; }

    // Token: 0x17000698 RID: 1688
    // (get) Token: 0x06001DCE RID: 7630 RVA: 0x003E9A68 File Offset: 0x003E9A68
    // (set) Token: 0x06001DCF RID: 7631 RVA: 0x003E9A70 File Offset: 0x003E9A70
    public string TurnToken { get; set; }

    // Token: 0x17000699 RID: 1689
    // (get) Token: 0x06001DD0 RID: 7632 RVA: 0x003E9A7C File Offset: 0x003E9A7C
    // (set) Token: 0x06001DD1 RID: 7633 RVA: 0x003E9A84 File Offset: 0x003E9A84
    public bool MessageRecorded { get; set; }

    // Token: 0x1700069A RID: 1690
    // (get) Token: 0x06001DD2 RID: 7634 RVA: 0x003E9A90 File Offset: 0x003E9A90
    // (set) Token: 0x06001DD3 RID: 7635 RVA: 0x003E9A98 File Offset: 0x003E9A98
    public bool MessageRefunded { get; set; }

    // Token: 0x1700069B RID: 1691
    // (get) Token: 0x06001DD4 RID: 7636 RVA: 0x003E9AA4 File Offset: 0x003E9AA4
    // (set) Token: 0x06001DD5 RID: 7637 RVA: 0x003E9AAC File Offset: 0x003E9AAC
    public bool SawDone { get; set; }

    // Token: 0x1700069C RID: 1692
    // (get) Token: 0x06001DD6 RID: 7638 RVA: 0x003E9AB8 File Offset: 0x003E9AB8
    // (set) Token: 0x06001DD7 RID: 7639 RVA: 0x003E9AC0 File Offset: 0x003E9AC0
    public string Error { get; set; }

    // Token: 0x1700069D RID: 1693
    // (get) Token: 0x06001DD8 RID: 7640 RVA: 0x003E9ACC File Offset: 0x003E9ACC
    // (set) Token: 0x06001DD9 RID: 7641 RVA: 0x003E9AD4 File Offset: 0x003E9AD4
    public JsonObject Usage { get; set; }
  }
}
