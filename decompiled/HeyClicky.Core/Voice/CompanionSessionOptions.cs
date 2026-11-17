using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Brain;
using HeyClicky.Core.Drive;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Platform;
using HeyClicky.Core.Tools;

namespace HeyClicky.Core.Voice
{
  // Token: 0x0200006C RID: 108
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class CompanionSessionOptions
  {
    // Token: 0x1700014F RID: 335
    // (get) Token: 0x06000647 RID: 1607 RVA: 0x0035EDF0 File Offset: 0x0035EDF0
    // (set) Token: 0x06000648 RID: 1608 RVA: 0x0035EDF8 File Offset: 0x0035EDF8
    public SynchronizationContext UiContext { get; set; }

    // Token: 0x17000150 RID: 336
    // (get) Token: 0x06000649 RID: 1609 RVA: 0x0035EE04 File Offset: 0x0035EE04
    // (set) Token: 0x0600064A RID: 1610 RVA: 0x0035EE0C File Offset: 0x0035EE0C
    [Nullable(1)]
    public IDesktopActions DesktopActions
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = new WindowsDesktopActions();

    // Token: 0x17000151 RID: 337
    // (get) Token: 0x0600064B RID: 1611 RVA: 0x0035EE18 File Offset: 0x0035EE18
    // (set) Token: 0x0600064C RID: 1612 RVA: 0x0035EE20 File Offset: 0x0035EE20
    [Nullable(new byte[] { 2, 1, 1, 1, 1 })]
    public Func<BrainToolCall, CompanionToolContext, CancellationToken, Task<string>> ToolCallHandler
    {
      [return: Nullable(new byte[] { 2, 1, 1, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1, 1, 1 })]
      set;
    }

    // Token: 0x17000152 RID: 338
    // (get) Token: 0x0600064D RID: 1613 RVA: 0x0035EE2C File Offset: 0x0035EE2C
    // (set) Token: 0x0600064E RID: 1614 RVA: 0x0035EE34 File Offset: 0x0035EE34
    [Nullable(new byte[] { 2, 1 })]
    public Func<BrainUserContext> UserContext
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000153 RID: 339
    // (get) Token: 0x0600064F RID: 1615 RVA: 0x0035EE40 File Offset: 0x0035EE40
    // (set) Token: 0x06000650 RID: 1616 RVA: 0x0035EE48 File Offset: 0x0035EE48
    public Func<string> RosterNote { get; set; }

    // Token: 0x17000154 RID: 340
    // (get) Token: 0x06000651 RID: 1617 RVA: 0x0035EE54 File Offset: 0x0035EE54
    // (set) Token: 0x06000652 RID: 1618 RVA: 0x0035EE5C File Offset: 0x0035EE5C
    public Func<string> PlanNote { get; set; }

    // Token: 0x17000155 RID: 341
    // (get) Token: 0x06000653 RID: 1619 RVA: 0x0035EE68 File Offset: 0x0035EE68
    // (set) Token: 0x06000654 RID: 1620 RVA: 0x0035EE70 File Offset: 0x0035EE70
    public IPageReader PageReader { get; set; }

    // Token: 0x17000156 RID: 342
    // (get) Token: 0x06000655 RID: 1621 RVA: 0x0035EE7C File Offset: 0x0035EE7C
    // (set) Token: 0x06000656 RID: 1622 RVA: 0x0035EE84 File Offset: 0x0035EE84
    [Nullable(new byte[] { 2, 1 })]
    public Func<string, bool> DocumentAccessDenied
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000157 RID: 343
    // (get) Token: 0x06000657 RID: 1623 RVA: 0x0035EE90 File Offset: 0x0035EE90
    // (set) Token: 0x06000658 RID: 1624 RVA: 0x0035EE98 File Offset: 0x0035EE98
    public Func<string> OutputSilenceKind { get; set; }

    // Token: 0x17000158 RID: 344
    // (get) Token: 0x06000659 RID: 1625 RVA: 0x0035EEA4 File Offset: 0x0035EEA4
    // (set) Token: 0x0600065A RID: 1626 RVA: 0x0035EEAC File Offset: 0x0035EEAC
    [Nullable(1)]
    public Func<bool> IsOnboardingTutorialRestrictingNormalUse
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = () => false;

    // Token: 0x17000159 RID: 345
    // (get) Token: 0x0600065B RID: 1627 RVA: 0x0035EEB8 File Offset: 0x0035EEB8
    // (set) Token: 0x0600065C RID: 1628 RVA: 0x0035EEC0 File Offset: 0x0035EEC0
    [Nullable(1)]
    public Func<string> UsageContext
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = () => "companion";

    // Token: 0x1700015A RID: 346
    // (get) Token: 0x0600065D RID: 1629 RVA: 0x0035EECC File Offset: 0x0035EECC
    // (set) Token: 0x0600065E RID: 1630 RVA: 0x0035EED4 File Offset: 0x0035EED4
    public TimeSpan MinimumHold { get; set; } = TimeSpan.FromSeconds(0.5);

    // Token: 0x1700015B RID: 347
    // (get) Token: 0x0600065F RID: 1631 RVA: 0x0035EEE0 File Offset: 0x0035EEE0
    // (set) Token: 0x06000660 RID: 1632 RVA: 0x0035EEE8 File Offset: 0x0035EEE8
    public Func<string> LiveStateNote { get; set; }

    // Token: 0x1700015C RID: 348
    // (get) Token: 0x06000661 RID: 1633 RVA: 0x0035EEF4 File Offset: 0x0035EEF4
    // (set) Token: 0x06000662 RID: 1634 RVA: 0x0035EEFC File Offset: 0x0035EEFC
    public Func<bool, string> EdgeDockFocusNote { get; set; }

    // Token: 0x1700015D RID: 349
    // (get) Token: 0x06000663 RID: 1635 RVA: 0x0035EF08 File Offset: 0x0035EF08
    // (set) Token: 0x06000664 RID: 1636 RVA: 0x0035EF10 File Offset: 0x0035EF10
    [Nullable(new byte[] { 2, 1, 1 })]
    public Func<string, Task<bool>> TakeSpokenWordsDirectly
    {
      [return: Nullable(new byte[] { 2, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1 })]
      set;
    }

    // Token: 0x1700015E RID: 350
    // (get) Token: 0x06000665 RID: 1637 RVA: 0x0035EF1C File Offset: 0x0035EF1C
    // (set) Token: 0x06000666 RID: 1638 RVA: 0x0035EF24 File Offset: 0x0035EF24
    public Func<string> TurnFlow { get; set; }

    // Token: 0x1700015F RID: 351
    // (get) Token: 0x06000667 RID: 1639 RVA: 0x0035EF30 File Offset: 0x0035EF30
    // (set) Token: 0x06000668 RID: 1640 RVA: 0x0035EF38 File Offset: 0x0035EF38
    [Nullable(new byte[] { 2, 1, 1 })]
    public Func<RectD, DisplayInfo, CancellationToken, Task<RectD?>> SnapTargetToControl
    {
      [return: Nullable(new byte[] { 2, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1 })]
      set;
    }

    // Token: 0x17000160 RID: 352
    // (get) Token: 0x06000669 RID: 1641 RVA: 0x0035EF44 File Offset: 0x0035EF44
    // (set) Token: 0x0600066A RID: 1642 RVA: 0x0035EF4C File Offset: 0x0035EF4C
    public IPointerInput PointerInput { get; set; }

    // Token: 0x17000161 RID: 353
    // (get) Token: 0x0600066B RID: 1643 RVA: 0x0035EF58 File Offset: 0x0035EF58
    // (set) Token: 0x0600066C RID: 1644 RVA: 0x0035EF60 File Offset: 0x0035EF60
    [Nullable(new byte[] { 2, 1 })]
    public Func<string, bool> RefuseTurnOverMessageCap
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000162 RID: 354
    // (get) Token: 0x0600066D RID: 1645 RVA: 0x0035EF6C File Offset: 0x0035EF6C
    // (set) Token: 0x0600066E RID: 1646 RVA: 0x0035EF74 File Offset: 0x0035EF74
    public Action<bool> MessageCounted { get; set; }

    // Token: 0x17000163 RID: 355
    // (get) Token: 0x0600066F RID: 1647 RVA: 0x0035EF80 File Offset: 0x0035EF80
    // (set) Token: 0x06000670 RID: 1648 RVA: 0x0035EF88 File Offset: 0x0035EF88
    public Func<bool> KeepMouthWarm { get; set; }

    // Token: 0x17000164 RID: 356
    // (get) Token: 0x06000671 RID: 1649 RVA: 0x0035EF94 File Offset: 0x0035EF94
    // (set) Token: 0x06000672 RID: 1650 RVA: 0x0035EF9C File Offset: 0x0035EF9C
    [Nullable(new byte[] { 2, 1, 1, 1, 2 })]
    public Func<IReadOnlyList<HighlightStroke>, CancellationToken, Task<string>> HighlightText
    {
      [return: Nullable(new byte[] { 2, 1, 1, 1, 2 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1, 1, 2 })]
      set;
    }

    // Token: 0x17000165 RID: 357
    // (get) Token: 0x06000673 RID: 1651 RVA: 0x0035EFA8 File Offset: 0x0035EFA8
    // (set) Token: 0x06000674 RID: 1652 RVA: 0x0035EFB0 File Offset: 0x0035EFB0
    public Func<bool> MutedSpeakerReplyExempt { get; set; }

    // Token: 0x17000166 RID: 358
    // (get) Token: 0x06000675 RID: 1653 RVA: 0x0035EFBC File Offset: 0x0035EFBC
    // (set) Token: 0x06000676 RID: 1654 RVA: 0x0035EFC4 File Offset: 0x0035EFC4
    public Func<JsonObject> TakeFlowBrief { get; set; }

    // Token: 0x17000167 RID: 359
    // (get) Token: 0x06000677 RID: 1655 RVA: 0x0035EFD0 File Offset: 0x0035EFD0
    // (set) Token: 0x06000678 RID: 1656 RVA: 0x0035EFD8 File Offset: 0x0035EFD8
    public Func<string> TakePillPlanNote { get; set; }

    // Token: 0x17000168 RID: 360
    // (get) Token: 0x06000679 RID: 1657 RVA: 0x0035EFE4 File Offset: 0x0035EFE4
    // (set) Token: 0x0600067A RID: 1658 RVA: 0x0035EFEC File Offset: 0x0035EFEC
    public Func<bool, string> BrowserStateNote { get; set; }

    // Token: 0x17000169 RID: 361
    // (get) Token: 0x0600067B RID: 1659 RVA: 0x0035EFF8 File Offset: 0x0035EFF8
    // (set) Token: 0x0600067C RID: 1660 RVA: 0x0035F000 File Offset: 0x0035F000
    public Func<bool> IsOnboardingTurn { get; set; }

    // Token: 0x1700016A RID: 362
    // (get) Token: 0x0600067D RID: 1661 RVA: 0x0035F00C File Offset: 0x0035F00C
    // (set) Token: 0x0600067E RID: 1662 RVA: 0x0035F014 File Offset: 0x0035F014
    public Func<double> CaptureSettleSeconds { get; set; }

    // Token: 0x1700016B RID: 363
    // (get) Token: 0x0600067F RID: 1663 RVA: 0x0035F020 File Offset: 0x0035F020
    // (set) Token: 0x06000680 RID: 1664 RVA: 0x0035F028 File Offset: 0x0035F028
    public Func<bool> RendersTextInNotchChat { get; set; }

    // Token: 0x1700016C RID: 364
    // (get) Token: 0x06000681 RID: 1665 RVA: 0x0035F034 File Offset: 0x0035F034
    // (set) Token: 0x06000682 RID: 1666 RVA: 0x0035F03C File Offset: 0x0035F03C
    public Func<bool> StepNotchChatAsideForDrawing { get; set; }

    // Token: 0x1700016D RID: 365
    // (get) Token: 0x06000683 RID: 1667 RVA: 0x0035F048 File Offset: 0x0035F048
    // (set) Token: 0x06000684 RID: 1668 RVA: 0x0035F050 File Offset: 0x0035F050
    public Func<ConversationHistory> ChatHistory { get; set; }

    // Token: 0x1700016E RID: 366
    // (get) Token: 0x06000685 RID: 1669 RVA: 0x0035F05C File Offset: 0x0035F05C
    // (set) Token: 0x06000686 RID: 1670 RVA: 0x0035F064 File Offset: 0x0035F064
    [Nullable(new byte[] { 2, 1, 1 })]
    public Func<IReadOnlyList<string>> SystemLanguageTags
    {
      [return: Nullable(new byte[] { 2, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1 })]
      set;
    }

    // Token: 0x1700016F RID: 367
    // (get) Token: 0x06000687 RID: 1671 RVA: 0x0035F070 File Offset: 0x0035F070
    // (set) Token: 0x06000688 RID: 1672 RVA: 0x0035F078 File Offset: 0x0035F078
    public IClickyDriveTurns Drive { get; set; }
  }
}
