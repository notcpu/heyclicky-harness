using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents.Backend;
using HeyClicky.Agents.Jobs;

namespace HeyClicky.Agents.Turns
{
  // Token: 0x02000016 RID: 22
  [NullableContext(2)]
  [Nullable(0)]
  internal sealed class AgentTask
  {
    // Token: 0x170000A4 RID: 164
    // (get) Token: 0x060002B1 RID: 689 RVA: 0x000E3A80 File Offset: 0x000E3A80
    [Nullable(1)]
    public string Id
    {
      [NullableContext(1)]
      get;
    } = Guid.NewGuid().ToString("D");

    // Token: 0x170000A5 RID: 165
    // (get) Token: 0x060002B2 RID: 690 RVA: 0x000E3A88 File Offset: 0x000E3A88
    public Guid NumericId
    {
      get
      {
        return Guid.Parse(this.Id);
      }
    }

    // Token: 0x170000A6 RID: 166
    // (get) Token: 0x060002B3 RID: 691 RVA: 0x000E3A98 File Offset: 0x000E3A98
    // (set) Token: 0x060002B4 RID: 692 RVA: 0x000E3AA0 File Offset: 0x000E3AA0
    public DateTimeOffset StartedAt { get; set; } = DateTimeOffset.Now;

    // Token: 0x170000A7 RID: 167
    // (get) Token: 0x060002B5 RID: 693 RVA: 0x000E3AAC File Offset: 0x000E3AAC
    // (set) Token: 0x060002B6 RID: 694 RVA: 0x000E3AB4 File Offset: 0x000E3AB4
    public string ClickySlug { get; set; }

    // Token: 0x170000A8 RID: 168
    // (get) Token: 0x060002B7 RID: 695 RVA: 0x000E3AC0 File Offset: 0x000E3AC0
    // (set) Token: 0x060002B8 RID: 696 RVA: 0x000E3AC8 File Offset: 0x000E3AC8
    public string ClickyName { get; set; }

    // Token: 0x170000A9 RID: 169
    // (get) Token: 0x060002B9 RID: 697 RVA: 0x000E3AD4 File Offset: 0x000E3AD4
    // (set) Token: 0x060002BA RID: 698 RVA: 0x000E3ADC File Offset: 0x000E3ADC
    public string JobId { get; set; }

    // Token: 0x170000AA RID: 170
    // (get) Token: 0x060002BB RID: 699 RVA: 0x000E3AE8 File Offset: 0x000E3AE8
    // (set) Token: 0x060002BC RID: 700 RVA: 0x000E3AF0 File Offset: 0x000E3AF0
    public string WorkspacePath { get; set; }

    // Token: 0x170000AB RID: 171
    // (get) Token: 0x060002BD RID: 701 RVA: 0x000E3AFC File Offset: 0x000E3AFC
    // (set) Token: 0x060002BE RID: 702 RVA: 0x000E3B04 File Offset: 0x000E3B04
    public string ThreadId { get; set; }

    // Token: 0x170000AC RID: 172
    // (get) Token: 0x060002BF RID: 703 RVA: 0x000E3B10 File Offset: 0x000E3B10
    // (set) Token: 0x060002C0 RID: 704 RVA: 0x000E3B18 File Offset: 0x000E3B18
    public string TurnId { get; set; }

    // Token: 0x170000AD RID: 173
    // (get) Token: 0x060002C1 RID: 705 RVA: 0x000E3B24 File Offset: 0x000E3B24
    // (set) Token: 0x060002C2 RID: 706 RVA: 0x000E3B2C File Offset: 0x000E3B2C
    [Nullable(1)]
    public string Title
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = "HeyClicky Agent Task";

    // Token: 0x170000AE RID: 174
    // (get) Token: 0x060002C3 RID: 707 RVA: 0x000E3B38 File Offset: 0x000E3B38
    // (set) Token: 0x060002C4 RID: 708 RVA: 0x000E3B40 File Offset: 0x000E3B40
    public AgentTaskStatus Status { get; set; }

    // Token: 0x170000AF RID: 175
    // (get) Token: 0x060002C5 RID: 709 RVA: 0x000E3B4C File Offset: 0x000E3B4C
    // (set) Token: 0x060002C6 RID: 710 RVA: 0x000E3B54 File Offset: 0x000E3B54
    [Nullable(1)]
    public string ProgressSummary
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = "Launching Agent Mode...";

    // Token: 0x170000B0 RID: 176
    // (get) Token: 0x060002C7 RID: 711 RVA: 0x000E3B60 File Offset: 0x000E3B60
    // (set) Token: 0x060002C8 RID: 712 RVA: 0x000E3B68 File Offset: 0x000E3B68
    public string ThinkingHeading { get; set; }

    // Token: 0x170000B1 RID: 177
    // (get) Token: 0x060002C9 RID: 713 RVA: 0x000E3B74 File Offset: 0x000E3B74
    // (set) Token: 0x060002CA RID: 714 RVA: 0x000E3B7C File Offset: 0x000E3B7C
    public bool IsRoutineRun { get; set; }

    // Token: 0x170000B2 RID: 178
    // (get) Token: 0x060002CB RID: 715 RVA: 0x000E3B88 File Offset: 0x000E3B88
    // (set) Token: 0x060002CC RID: 716 RVA: 0x000E3B90 File Offset: 0x000E3B90
    public bool IsSentFromItsChat { get; set; }

    // Token: 0x170000B3 RID: 179
    // (get) Token: 0x060002CD RID: 717 RVA: 0x000E3B9C File Offset: 0x000E3B9C
    [Nullable(1)]
    public List<string> SitesReadThisTurn
    {
      [NullableContext(1)]
      get;
    } = new List<string>();

    // Token: 0x060002CE RID: 718 RVA: 0x000E3BA4 File Offset: 0x000E3BA4
    [NullableContext(1)]
    public void NoteSitesRead(IEnumerable<string> hosts)
    {
      foreach (string text in hosts)
      {
        string text2 = text.Trim().ToLowerInvariant();
        string text3;
        if (!text2.StartsWith("www.", StringComparison.Ordinal))
        {
          text3 = text2;
        }
        else
        {
          string text4 = text2;
          text3 = text4.Substring(4, text4.Length - 4);
        }
        string text5 = text3;
        if (text5.Length != 0 && text5.Contains('.') && !this.SitesReadThisTurn.Contains(text5) && this.SitesReadThisTurn.Count < 60)
        {
          this.SitesReadThisTurn.Add(text5);
        }
      }
    }

    // Token: 0x170000B4 RID: 180
    // (get) Token: 0x060002CF RID: 719 RVA: 0x000E3C50 File Offset: 0x000E3C50
    // (set) Token: 0x060002D0 RID: 720 RVA: 0x000E3C58 File Offset: 0x000E3C58
    [Nullable(1)]
    public string LaunchSource
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = "voice";

    // Token: 0x170000B5 RID: 181
    // (get) Token: 0x060002D1 RID: 721 RVA: 0x000E3C64 File Offset: 0x000E3C64
    // (set) Token: 0x060002D2 RID: 722 RVA: 0x000E3C6C File Offset: 0x000E3C6C
    public bool IsDemo { get; set; }

    // Token: 0x170000B6 RID: 182
    // (get) Token: 0x060002D3 RID: 723 RVA: 0x000E3C78 File Offset: 0x000E3C78
    // (set) Token: 0x060002D4 RID: 724 RVA: 0x000E3C80 File Offset: 0x000E3C80
    public long? ProactiveSuggestionId { get; set; }

    // Token: 0x170000B7 RID: 183
    // (get) Token: 0x060002D5 RID: 725 RVA: 0x000E3C8C File Offset: 0x000E3C8C
    // (set) Token: 0x060002D6 RID: 726 RVA: 0x000E3C94 File Offset: 0x000E3C94
    public string PendingInitialMessageLog { get; set; }

    // Token: 0x170000B8 RID: 184
    // (get) Token: 0x060002D7 RID: 727 RVA: 0x000E3CA0 File Offset: 0x000E3CA0
    [Nullable(1)]
    public List<string> UserPrompts
    {
      [NullableContext(1)]
      get;
    } = new List<string>();

    // Token: 0x170000B9 RID: 185
    // (get) Token: 0x060002D8 RID: 728 RVA: 0x000E3CA8 File Offset: 0x000E3CA8
    [Nullable(1)]
    public List<ActivityRow> Activity
    {
      [NullableContext(1)]
      get;
    } = new List<ActivityRow>();

    // Token: 0x170000BA RID: 186
    // (get) Token: 0x060002D9 RID: 729 RVA: 0x000E3CB0 File Offset: 0x000E3CB0
    [Nullable(1)]
    public StringBuilder StreamingCommentary
    {
      [NullableContext(1)]
      get;
    } = new StringBuilder();

    // Token: 0x170000BB RID: 187
    // (get) Token: 0x060002DA RID: 730 RVA: 0x000E3CB8 File Offset: 0x000E3CB8
    [Nullable(1)]
    public StringBuilder ResponseAccumulator
    {
      [NullableContext(1)]
      get;
    } = new StringBuilder();

    // Token: 0x170000BC RID: 188
    // (get) Token: 0x060002DB RID: 731 RVA: 0x000E3CC0 File Offset: 0x000E3CC0
    // (set) Token: 0x060002DC RID: 732 RVA: 0x000E3CC8 File Offset: 0x000E3CC8
    public string CurrentMessagePhase { get; set; }

    // Token: 0x170000BD RID: 189
    // (get) Token: 0x060002DD RID: 733 RVA: 0x000E3CD4 File Offset: 0x000E3CD4
    // (set) Token: 0x060002DE RID: 734 RVA: 0x000E3CDC File Offset: 0x000E3CDC
    public string CurrentNarrationItemId { get; set; }

    // Token: 0x170000BE RID: 190
    // (get) Token: 0x060002DF RID: 735 RVA: 0x000E3CE8 File Offset: 0x000E3CE8
    // (set) Token: 0x060002E0 RID: 736 RVA: 0x000E3CF0 File Offset: 0x000E3CF0
    public int? ReasoningSummaryIndex { get; set; }

    // Token: 0x170000BF RID: 191
    // (get) Token: 0x060002E1 RID: 737 RVA: 0x000E3CFC File Offset: 0x000E3CFC
    [Nullable(1)]
    public StringBuilder ReasoningSummaryText
    {
      [NullableContext(1)]
      get;
    } = new StringBuilder();

    // Token: 0x170000C0 RID: 192
    // (get) Token: 0x060002E2 RID: 738 RVA: 0x000E3D04 File Offset: 0x000E3D04
    // (set) Token: 0x060002E3 RID: 739 RVA: 0x000E3D0C File Offset: 0x000E3D0C
    [Nullable(1)]
    public string LatestCommentary
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = "";

    // Token: 0x170000C1 RID: 193
    // (get) Token: 0x060002E4 RID: 740 RVA: 0x000E3D18 File Offset: 0x000E3D18
    [Nullable(1)]
    public List<string> CompletedReplies
    {
      [NullableContext(1)]
      get;
    } = new List<string>();

    // Token: 0x170000C2 RID: 194
    // (get) Token: 0x060002E5 RID: 741 RVA: 0x000E3D20 File Offset: 0x000E3D20
    // (set) Token: 0x060002E6 RID: 742 RVA: 0x000E3D28 File Offset: 0x000E3D28
    [Nullable(1)]
    public IReadOnlyList<AgentPlanStep> Plan
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = Array.Empty<AgentPlanStep>();

    // Token: 0x170000C3 RID: 195
    // (get) Token: 0x060002E7 RID: 743 RVA: 0x000E3D34 File Offset: 0x000E3D34
    // (set) Token: 0x060002E8 RID: 744 RVA: 0x000E3D3C File Offset: 0x000E3D3C
    public string FinalResponseMarkdown { get; set; }

    // Token: 0x170000C4 RID: 196
    // (get) Token: 0x060002E9 RID: 745 RVA: 0x000E3D48 File Offset: 0x000E3D48
    // (set) Token: 0x060002EA RID: 746 RVA: 0x000E3D50 File Offset: 0x000E3D50
    public string Summary { get; set; }

    // Token: 0x170000C5 RID: 197
    // (get) Token: 0x060002EB RID: 747 RVA: 0x000E3D5C File Offset: 0x000E3D5C
    // (set) Token: 0x060002EC RID: 748 RVA: 0x000E3D64 File Offset: 0x000E3D64
    public string DoneTitle { get; set; }

    // Token: 0x170000C6 RID: 198
    // (get) Token: 0x060002ED RID: 749 RVA: 0x000E3D70 File Offset: 0x000E3D70
    // (set) Token: 0x060002EE RID: 750 RVA: 0x000E3D78 File Offset: 0x000E3D78
    [Nullable(1)]
    public IReadOnlyList<string> NextActions
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = Array.Empty<string>();

    // Token: 0x170000C7 RID: 199
    // (get) Token: 0x060002EF RID: 751 RVA: 0x000E3D84 File Offset: 0x000E3D84
    // (set) Token: 0x060002F0 RID: 752 RVA: 0x000E3D8C File Offset: 0x000E3D8C
    [Nullable(1)]
    public IReadOnlyList<string> Artifacts
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = Array.Empty<string>();

    // Token: 0x170000C8 RID: 200
    // (get) Token: 0x060002F1 RID: 753 RVA: 0x000E3D98 File Offset: 0x000E3D98
    // (set) Token: 0x060002F2 RID: 754 RVA: 0x000E3DA0 File Offset: 0x000E3DA0
    public ClickyAnswer Answer { get; set; }

    // Token: 0x170000C9 RID: 201
    // (get) Token: 0x060002F3 RID: 755 RVA: 0x000E3DAC File Offset: 0x000E3DAC
    // (set) Token: 0x060002F4 RID: 756 RVA: 0x000E3DB4 File Offset: 0x000E3DB4
    public ClickyJobUserStep PendingUserStep { get; set; }

    // Token: 0x170000CA RID: 202
    // (get) Token: 0x060002F5 RID: 757 RVA: 0x000E3DC0 File Offset: 0x000E3DC0
    // (set) Token: 0x060002F6 RID: 758 RVA: 0x000E3DC8 File Offset: 0x000E3DC8
    [Nullable(1)]
    public IReadOnlyList<string> LearnedHowTos
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = Array.Empty<string>();

    // Token: 0x170000CB RID: 203
    // (get) Token: 0x060002F7 RID: 759 RVA: 0x000E3DD4 File Offset: 0x000E3DD4
    // (set) Token: 0x060002F8 RID: 760 RVA: 0x000E3DDC File Offset: 0x000E3DDC
    public string PendingComputerUseRequest { get; set; }

    // Token: 0x170000CC RID: 204
    // (get) Token: 0x060002F9 RID: 761 RVA: 0x000E3DE8 File Offset: 0x000E3DE8
    // (set) Token: 0x060002FA RID: 762 RVA: 0x000E3DF0 File Offset: 0x000E3DF0
    public string FailureMessage { get; set; }

    // Token: 0x170000CD RID: 205
    // (get) Token: 0x060002FB RID: 763 RVA: 0x000E3DFC File Offset: 0x000E3DFC
    // (set) Token: 0x060002FC RID: 764 RVA: 0x000E3E04 File Offset: 0x000E3E04
    public string FailureDiagnostic { get; set; }

    // Token: 0x170000CE RID: 206
    // (get) Token: 0x060002FD RID: 765 RVA: 0x000E3E10 File Offset: 0x000E3E10
    // (set) Token: 0x060002FE RID: 766 RVA: 0x000E3E18 File Offset: 0x000E3E18
    public bool InterruptedUnexpectedly { get; set; }

    // Token: 0x170000CF RID: 207
    // (get) Token: 0x060002FF RID: 767 RVA: 0x000E3E24 File Offset: 0x000E3E24
    // (set) Token: 0x06000300 RID: 768 RVA: 0x000E3E2C File Offset: 0x000E3E2C
    public CompanionError Error { get; set; }

    // Token: 0x170000D0 RID: 208
    // (get) Token: 0x06000301 RID: 769 RVA: 0x000E3E38 File Offset: 0x000E3E38
    // (set) Token: 0x06000302 RID: 770 RVA: 0x000E3E40 File Offset: 0x000E3E40
    public AgentTurnLease Lease { get; set; }

    // Token: 0x170000D1 RID: 209
    // (get) Token: 0x06000303 RID: 771 RVA: 0x000E3E4C File Offset: 0x000E3E4C
    // (set) Token: 0x06000304 RID: 772 RVA: 0x000E3E54 File Offset: 0x000E3E54
    [TupleElementNames(new string[] { "Model", "Effort" })]
    [Nullable(new byte[] { 0, 1, 1 })]
    public ValueTuple<string, string>? DeveloperModel
    {
      [return: TupleElementNames(new string[] { "Model", "Effort" })]
      [return: Nullable(new byte[] { 0, 1, 1 })]
      get;
      [param: TupleElementNames(new string[] { "Model", "Effort" })]
      [param: Nullable(new byte[] { 0, 1, 1 })]
      set;
    }

    // Token: 0x170000D2 RID: 210
    // (get) Token: 0x06000305 RID: 773 RVA: 0x000E3E60 File Offset: 0x000E3E60
    // (set) Token: 0x06000306 RID: 774 RVA: 0x000E3E68 File Offset: 0x000E3E68
    public DateTimeOffset? LeaseRecordedAt { get; set; }

    // Token: 0x170000D3 RID: 211
    // (get) Token: 0x06000307 RID: 775 RVA: 0x000E3E74 File Offset: 0x000E3E74
    // (set) Token: 0x06000308 RID: 776 RVA: 0x000E3E7C File Offset: 0x000E3E7C
    public bool RequiresExtraUsageApproval { get; set; }

    // Token: 0x170000D4 RID: 212
    // (get) Token: 0x06000309 RID: 777 RVA: 0x000E3E88 File Offset: 0x000E3E88
    // (set) Token: 0x0600030A RID: 778 RVA: 0x000E3E90 File Offset: 0x000E3E90
    public bool DidRefundEarlyStop { get; set; }

    // Token: 0x170000D5 RID: 213
    // (get) Token: 0x0600030B RID: 779 RVA: 0x000E3E9C File Offset: 0x000E3E9C
    [Nullable(1)]
    public HashSet<string> IntegrationsUsedThisTurn
    {
      [NullableContext(1)]
      get;
    } = new HashSet<string>();

    // Token: 0x170000D6 RID: 214
    // (get) Token: 0x0600030C RID: 780 RVA: 0x000E3EA4 File Offset: 0x000E3EA4
    // (set) Token: 0x0600030D RID: 781 RVA: 0x000E3EAC File Offset: 0x000E3EAC
    public bool NeedsChromeConnection { get; set; }

    // Token: 0x170000D7 RID: 215
    // (get) Token: 0x0600030E RID: 782 RVA: 0x000E3EB8 File Offset: 0x000E3EB8
    // (set) Token: 0x0600030F RID: 783 RVA: 0x000E3EC0 File Offset: 0x000E3EC0
    public bool FoundChromeNotConnectedThisTurn { get; set; }

    // Token: 0x170000D8 RID: 216
    // (get) Token: 0x06000310 RID: 784 RVA: 0x000E3ECC File Offset: 0x000E3ECC
    // (set) Token: 0x06000311 RID: 785 RVA: 0x000E3ED4 File Offset: 0x000E3ED4
    public bool IsNewJobRun { get; set; }

    // Token: 0x170000D9 RID: 217
    // (get) Token: 0x06000312 RID: 786 RVA: 0x000E3EE0 File Offset: 0x000E3EE0
    // (set) Token: 0x06000313 RID: 787 RVA: 0x000E3EE8 File Offset: 0x000E3EE8
    [Nullable(new byte[] { 2, 1 })]
    public TaskCompletionSource<string> TurnCompletion
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x170000DA RID: 218
    // (get) Token: 0x06000314 RID: 788 RVA: 0x000E3EF4 File Offset: 0x000E3EF4
    // (set) Token: 0x06000315 RID: 789 RVA: 0x000E3EFC File Offset: 0x000E3EFC
    public CancellationTokenSource TurnCts { get; set; }

    // Token: 0x170000DB RID: 219
    // (get) Token: 0x06000316 RID: 790 RVA: 0x000E3F08 File Offset: 0x000E3F08
    // (set) Token: 0x06000317 RID: 791 RVA: 0x000E3F10 File Offset: 0x000E3F10
    public bool HoldsSlot { get; set; }

    // Token: 0x170000DC RID: 220
    // (get) Token: 0x06000318 RID: 792 RVA: 0x000E3F1C File Offset: 0x000E3F1C
    // (set) Token: 0x06000319 RID: 793 RVA: 0x000E3F24 File Offset: 0x000E3F24
    public long LaunchTimestamp { get; set; }

    // Token: 0x170000DD RID: 221
    // (get) Token: 0x0600031A RID: 794 RVA: 0x000E3F30 File Offset: 0x000E3F30
    // (set) Token: 0x0600031B RID: 795 RVA: 0x000E3F38 File Offset: 0x000E3F38
    public long TurnStartedTimestamp { get; set; }

    // Token: 0x170000DE RID: 222
    // (get) Token: 0x0600031C RID: 796 RVA: 0x000E3F44 File Offset: 0x000E3F44
    // (set) Token: 0x0600031D RID: 797 RVA: 0x000E3F4C File Offset: 0x000E3F4C
    public long FirstTokenTimestamp { get; set; }

    // Token: 0x170000DF RID: 223
    // (get) Token: 0x0600031E RID: 798 RVA: 0x000E3F58 File Offset: 0x000E3F58
    public bool IsActive
    {
      get
      {
        AgentTaskStatus status = this.Status;
        return status <= AgentTaskStatus.Running;
      }
    }

    // Token: 0x0600031F RID: 799 RVA: 0x000E3F78 File Offset: 0x000E3F78
    [NullableContext(1)]
    public void ResetForNewTurn(string prompt)
    {
      this.UserPrompts.Add(prompt);
      this.TurnId = null;
      this.Status = AgentTaskStatus.Starting;
      this.ProgressSummary = "Launching Agent Mode...";
      this.ThinkingHeading = null;
      this.StreamingCommentary.Clear();
      this.ResponseAccumulator.Clear();
      this.CurrentMessagePhase = null;
      this.CurrentNarrationItemId = null;
      this.ReasoningSummaryIndex = null;
      this.ReasoningSummaryText.Clear();
      this.CompletedReplies.Clear();
      this.SitesReadThisTurn.Clear();
      this.Plan = Array.Empty<AgentPlanStep>();
      this.FinalResponseMarkdown = null;
      this.Summary = null;
      this.DoneTitle = null;
      this.NextActions = Array.Empty<string>();
      this.Artifacts = Array.Empty<string>();
      this.Answer = null;
      this.PendingUserStep = null;
      this.LearnedHowTos = Array.Empty<string>();
      this.FailureMessage = null;
      this.FailureDiagnostic = null;
      this.InterruptedUnexpectedly = false;
      this.Error = null;
      this.RequiresExtraUsageApproval = false;
      this.DidRefundEarlyStop = false;
      this.IntegrationsUsedThisTurn.Clear();
      this.NeedsChromeConnection = false;
      this.FoundChromeNotConnectedThisTurn = false;
      this.FirstTokenTimestamp = 0L;
      this.TurnStartedTimestamp = 0L;
    }

    // Token: 0x06000320 RID: 800 RVA: 0x000E40AC File Offset: 0x000E40AC
    public string FinishStreamingCommentary(string itemId, string fallbackText = null)
    {
      if (itemId != null && this.completedCommentaryItemIds.Contains(itemId))
      {
        return null;
      }
      string text = ((fallbackText != null) ? fallbackText.Trim() : null) ?? "";
      string text2 = CitationMarkers.Strip((text.Length == 0) ? this.StreamingCommentary.ToString() : text).Trim();
      this.StreamingCommentary.Clear();
      if (text2.Length == 0)
      {
        return null;
      }
      this.Activity.Add(new ActivityRow(Guid.NewGuid().ToString("N"), AgentActivityKind.Commentary, text2, null, DateTimeOffset.Now));
      if (itemId != null)
      {
        this.completedCommentaryItemIds.Add(itemId);
      }
      this.LatestCommentary = text2;
      return text2;
    }

    // Token: 0x06000321 RID: 801 RVA: 0x000E4164 File Offset: 0x000E4164
    [NullableContext(1)]
    public void RecordToolActivity([Nullable(2)] string itemId, string text, bool isRunning)
    {
      int num;
      if (itemId != null && this.toolActivityIndex.TryGetValue(itemId, out num) && num < this.Activity.Count)
      {
        List<ActivityRow> activity = this.Activity;
        int num2 = num;
        ActivityRow activityRow = this.Activity[num].<Clone>$();
        activityRow.Text = text;
        activityRow.IsRunning = new bool?(isRunning);
        activity[num2] = activityRow;
        return;
      }
      this.Activity.Add(new ActivityRow(itemId ?? Guid.NewGuid().ToString("N"), AgentActivityKind.ToolCall, text, new bool?(isRunning), DateTimeOffset.Now));
      if (itemId != null)
      {
        this.toolActivityIndex[itemId] = this.Activity.Count - 1;
      }
    }

    // Token: 0x06000322 RID: 802 RVA: 0x000E4214 File Offset: 0x000E4214
    [NullableContext(1)]
    public void AddActivity(AgentActivityKind kind, string text)
    {
      if (this.Activity.Count > 0)
      {
        List<ActivityRow> activity = this.Activity;
        if (activity[activity.Count - 1].Kind == kind && kind == AgentActivityKind.ShellOutput)
        {
          List<ActivityRow> activity2 = this.Activity;
          int num = activity2.Count - 1;
          List<ActivityRow> activity3 = this.Activity;
          ActivityRow activityRow = activity3[activity3.Count - 1].<Clone>$();
          activityRow.Text = text;
          activityRow.At = DateTimeOffset.Now;
          activity2[num] = activityRow;
          return;
        }
      }
      this.Activity.Add(new ActivityRow(Guid.NewGuid().ToString("N"), kind, text, null, DateTimeOffset.Now));
      if (this.Activity.Count > 400)
      {
        this.Activity.RemoveRange(0, this.Activity.Count - 400);
      }
    }

    // Token: 0x06000323 RID: 803 RVA: 0x000E42F0 File Offset: 0x000E42F0
    [NullableContext(1)]
    public AgentTaskSnapshot Snapshot()
    {
      string text = ((this.StreamingCommentary.Length > 0) ? CitationMarkers.Strip(this.StreamingCommentary.ToString()) : this.LatestCommentary);
      if (text.Length == 0)
      {
        text = this.ProgressSummary;
      }
      string text2 = ((this.Status == AgentTaskStatus.Finished && this.DoneTitle != null) ? this.DoneTitle : this.Title);
      string text3 = this.FinalResponseMarkdown ?? ((this.ResponseAccumulator.Length > 0 && this.Status == AgentTaskStatus.Running) ? StructuredFinalReply.Parse(this.ResponseAccumulator.ToString()).Text : null);
      return new AgentTaskSnapshot(this.Id, this.ThreadId, text2, this.Status, text, text3, this.StartedAt);
    }

    // Token: 0x06000324 RID: 804 RVA: 0x000E43B0 File Offset: 0x000E43B0
    [NullableContext(1)]
    public AgentTaskDetail Detail()
    {
      AgentTaskSnapshot agentTaskSnapshot = this.Snapshot();
      string clickySlug = this.ClickySlug;
      string clickyName = this.ClickyName;
      string workspacePath = this.WorkspacePath;
      string turnId = this.TurnId;
      string progressSummary = this.ProgressSummary;
      string thinkingHeading = this.ThinkingHeading;
      string text = CitationMarkers.Strip(this.ResponseAccumulator.ToString());
      string text2 = CitationMarkers.Strip(this.StreamingCommentary.ToString());
      IReadOnlyList<AgentActivityItem> readOnlyList = this.Activity.Select<ActivityRow, AgentActivityItem>((ActivityRow a) => new AgentActivityItem(a.Id, a.Kind, a.Text, a.IsRunning, a.At)).ToList<AgentActivityItem>();
      IReadOnlyList<string> readOnlyList2 = this.UserPrompts.ToList<string>();
      string summary = this.Summary;
      string doneTitle = this.DoneTitle;
      IReadOnlyList<string> nextActions = this.NextActions;
      IReadOnlyList<string> artifacts = this.Artifacts;
      string failureMessage = this.FailureMessage;
      CompanionError error = this.Error;
      bool requiresExtraUsageApproval = this.RequiresExtraUsageApproval;
      bool didRefundEarlyStop = this.DidRefundEarlyStop;
      AgentTurnLease lease = this.Lease;
      long inputTokens = this.InputTokens;
      long cachedInputTokens = this.CachedInputTokens;
      long outputTokens = this.OutputTokens;
      bool isRoutineRun = this.IsRoutineRun;
      bool isDemo = this.IsDemo;
      string launchSource = this.LaunchSource;
      string jobId = this.JobId;
      IReadOnlyList<AgentPlanStep> plan = this.Plan;
      ClickyAnswer answer = this.Answer;
      ClickyJobUserStep pendingUserStep = this.PendingUserStep;
      string turnId2 = this.TurnId;
      return new AgentTaskDetail(agentTaskSnapshot, clickySlug, clickyName, workspacePath, turnId, progressSummary, thinkingHeading, text, text2, readOnlyList, readOnlyList2, summary, doneTitle, nextActions, artifacts, failureMessage, error, requiresExtraUsageApproval, didRefundEarlyStop, lease, inputTokens, cachedInputTokens, outputTokens, isRoutineRun, isDemo, launchSource, jobId, plan, answer, pendingUserStep, turnId2 != null && turnId2.StartsWith("harness-turn-", StringComparison.Ordinal), this.IsNewJobRun, this.IsSentFromItsChat, this.SitesReadThisTurn.ToList<string>(), this.FailureDiagnostic, this.InterruptedUnexpectedly);
    }

    // Token: 0x040000F1 RID: 241
    [Nullable(1)]
    public readonly object Gate = new object();

    // Token: 0x04000107 RID: 263
    [Nullable(1)]
    private readonly Dictionary<string, int> toolActivityIndex = new Dictionary<string, int>();

    // Token: 0x0400010C RID: 268
    [Nullable(1)]
    private readonly HashSet<string> completedCommentaryItemIds = new HashSet<string>();

    // Token: 0x04000128 RID: 296
    public long InputTokens;

    // Token: 0x04000129 RID: 297
    public long CachedInputTokens;

    // Token: 0x0400012A RID: 298
    public long OutputTokens;
  }
}
