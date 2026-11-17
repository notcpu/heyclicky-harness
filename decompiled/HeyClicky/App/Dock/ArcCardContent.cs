using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using HeyClicky.Agents.Jobs;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002E5 RID: 741
  [NullableContext(2)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class ArcCardContent : IEquatable<ArcCardContent>
  {
    // Token: 0x170005A9 RID: 1449
    // (get) Token: 0x06001F79 RID: 8057 RVA: 0x0060FD84 File Offset: 0x0060FD84
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ArcCardContent);
      }
    }

    // Token: 0x170005AA RID: 1450
    // (get) Token: 0x06001F7A RID: 8058 RVA: 0x0060FD90 File Offset: 0x0060FD90
    // (set) Token: 0x06001F7B RID: 8059 RVA: 0x0060FD98 File Offset: 0x0060FD98
    [Nullable(1)]
    [RequiredMember]
    public string RailKey
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170005AB RID: 1451
    // (get) Token: 0x06001F7C RID: 8060 RVA: 0x0060FDA4 File Offset: 0x0060FDA4
    // (set) Token: 0x06001F7D RID: 8061 RVA: 0x0060FDAC File Offset: 0x0060FDAC
    [Nullable(1)]
    [RequiredMember]
    public string Title
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170005AC RID: 1452
    // (get) Token: 0x06001F7E RID: 8062 RVA: 0x0060FDB8 File Offset: 0x0060FDB8
    // (set) Token: 0x06001F7F RID: 8063 RVA: 0x0060FDC0 File Offset: 0x0060FDC0
    [RequiredMember]
    public ArcCardState State { get; set; }

    // Token: 0x170005AD RID: 1453
    // (get) Token: 0x06001F80 RID: 8064 RVA: 0x0060FDCC File Offset: 0x0060FDCC
    // (set) Token: 0x06001F81 RID: 8065 RVA: 0x0060FDD4 File Offset: 0x0060FDD4
    [RequiredMember]
    public ArcPaper Paper { get; set; }

    // Token: 0x170005AE RID: 1454
    // (get) Token: 0x06001F82 RID: 8066 RVA: 0x0060FDE0 File Offset: 0x0060FDE0
    // (set) Token: 0x06001F83 RID: 8067 RVA: 0x0060FDE8 File Offset: 0x0060FDE8
    public ClickyArcPlan Plan { get; set; }

    // Token: 0x170005AF RID: 1455
    // (get) Token: 0x06001F84 RID: 8068 RVA: 0x0060FDF4 File Offset: 0x0060FDF4
    // (set) Token: 0x06001F85 RID: 8069 RVA: 0x0060FDFC File Offset: 0x0060FDFC
    public string CurrentStepLine { get; set; }

    // Token: 0x170005B0 RID: 1456
    // (get) Token: 0x06001F86 RID: 8070 RVA: 0x0060FE08 File Offset: 0x0060FE08
    // (set) Token: 0x06001F87 RID: 8071 RVA: 0x0060FE10 File Offset: 0x0060FE10
    public string Summary { get; set; }

    // Token: 0x170005B1 RID: 1457
    // (get) Token: 0x06001F88 RID: 8072 RVA: 0x0060FE1C File Offset: 0x0060FE1C
    // (set) Token: 0x06001F89 RID: 8073 RVA: 0x0060FE24 File Offset: 0x0060FE24
    [Nullable(1)]
    public IReadOnlyList<string> Files
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170005B2 RID: 1458
    // (get) Token: 0x06001F8A RID: 8074 RVA: 0x0060FE30 File Offset: 0x0060FE30
    // (set) Token: 0x06001F8B RID: 8075 RVA: 0x0060FE38 File Offset: 0x0060FE38
    public string ThreadId { get; set; }

    // Token: 0x170005B3 RID: 1459
    // (get) Token: 0x06001F8C RID: 8076 RVA: 0x0060FE44 File Offset: 0x0060FE44
    // (set) Token: 0x06001F8D RID: 8077 RVA: 0x0060FE4C File Offset: 0x0060FE4C
    public string ClickySlug { get; set; }

    // Token: 0x170005B4 RID: 1460
    // (get) Token: 0x06001F8E RID: 8078 RVA: 0x0060FE58 File Offset: 0x0060FE58
    // (set) Token: 0x06001F8F RID: 8079 RVA: 0x0060FE60 File Offset: 0x0060FE60
    public string JobId { get; set; }

    // Token: 0x170005B5 RID: 1461
    // (get) Token: 0x06001F90 RID: 8080 RVA: 0x0060FE6C File Offset: 0x0060FE6C
    // (set) Token: 0x06001F91 RID: 8081 RVA: 0x0060FE74 File Offset: 0x0060FE74
    public string TaskId { get; set; }

    // Token: 0x170005B6 RID: 1462
    // (get) Token: 0x06001F92 RID: 8082 RVA: 0x0060FE80 File Offset: 0x0060FE80
    // (set) Token: 0x06001F93 RID: 8083 RVA: 0x0060FE88 File Offset: 0x0060FE88
    public string WorkspacePath { get; set; }

    // Token: 0x170005B7 RID: 1463
    // (get) Token: 0x06001F94 RID: 8084 RVA: 0x0060FE94 File Offset: 0x0060FE94
    // (set) Token: 0x06001F95 RID: 8085 RVA: 0x0060FE9C File Offset: 0x0060FE9C
    public bool TaskIsRunning { get; set; }

    // Token: 0x170005B8 RID: 1464
    // (get) Token: 0x06001F96 RID: 8086 RVA: 0x0060FEA8 File Offset: 0x0060FEA8
    // (set) Token: 0x06001F97 RID: 8087 RVA: 0x0060FEB0 File Offset: 0x0060FEB0
    public CardFooterRequest Request { get; set; }

    // Token: 0x170005B9 RID: 1465
    // (get) Token: 0x06001F98 RID: 8088 RVA: 0x0060FEBC File Offset: 0x0060FEBC
    // (set) Token: 0x06001F99 RID: 8089 RVA: 0x0060FEC4 File Offset: 0x0060FEC4
    public CardReadyOutput ReadyOutput { get; set; }

    // Token: 0x170005BA RID: 1466
    // (get) Token: 0x06001F9A RID: 8090 RVA: 0x0060FED0 File Offset: 0x0060FED0
    // (set) Token: 0x06001F9B RID: 8091 RVA: 0x0060FED8 File Offset: 0x0060FED8
    public ClickyLiveLine LiveLine { get; set; }

    // Token: 0x170005BB RID: 1467
    // (get) Token: 0x06001F9C RID: 8092 RVA: 0x0060FEE4 File Offset: 0x0060FEE4
    // (set) Token: 0x06001F9D RID: 8093 RVA: 0x0060FEEC File Offset: 0x0060FEEC
    public ClickyAnswer Answer { get; set; }

    // Token: 0x170005BC RID: 1468
    // (get) Token: 0x06001F9E RID: 8094 RVA: 0x0060FEF8 File Offset: 0x0060FEF8
    // (set) Token: 0x06001F9F RID: 8095 RVA: 0x0060FF00 File Offset: 0x0060FF00
    public bool IsFinalizing { get; set; }

    // Token: 0x170005BD RID: 1469
    // (get) Token: 0x06001FA0 RID: 8096 RVA: 0x0060FF0C File Offset: 0x0060FF0C
    // (set) Token: 0x06001FA1 RID: 8097 RVA: 0x0060FF14 File Offset: 0x0060FF14
    public string NotePath { get; set; }

    // Token: 0x170005BE RID: 1470
    // (get) Token: 0x06001FA2 RID: 8098 RVA: 0x0060FF20 File Offset: 0x0060FF20
    public bool IsWorking
    {
      get
      {
        return ArcCardStates.IsWorking(this.State);
      }
    }

    // Token: 0x170005BF RID: 1471
    // (get) Token: 0x06001FA3 RID: 8099 RVA: 0x0060FF30 File Offset: 0x0060FF30
    public bool IsDone
    {
      get
      {
        return ArcCardStates.IsDone(this.State);
      }
    }

    // Token: 0x170005C0 RID: 1472
    // (get) Token: 0x06001FA4 RID: 8100 RVA: 0x0060FF40 File Offset: 0x0060FF40
    [Nullable(1)]
    public unsafe string Shape
    {
      [NullableContext(1)]
      get
      {
        string text = "|";
        <>y__InlineArray15<object> <>y__InlineArray = default(<>y__InlineArray15<object>);
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray15<object>, object>(ref <>y__InlineArray, 0) = this.State;
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray15<object>, object>(ref <>y__InlineArray, 1) = this.IsFinalizing;
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray15<object>, object>(ref <>y__InlineArray, 2) = this.Title;
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray15<object>, object>(ref <>y__InlineArray, 3) = this.Summary;
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray15<object>, object>(ref <>y__InlineArray, 4) = string.Join(",", this.Files);
        ref object ptr = ref <PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray15<object>, object>(ref <>y__InlineArray, 5);
        CardFooterRequest request = this.Request;
        ptr = ((request != null) ? new CardFooterRequest.Kinds?(request.Kind) : null);
        ref object ptr2 = ref <PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray15<object>, object>(ref <>y__InlineArray, 6);
        CardFooterRequest request2 = this.Request;
        ptr2 = ((request2 != null) ? request2.Text : null);
        ref object ptr3 = ref <PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray15<object>, object>(ref <>y__InlineArray, 7);
        CardReadyOutput readyOutput = this.ReadyOutput;
        ptr3 = ((readyOutput != null) ? new CardReadyOutput.Kinds?(readyOutput.Kind) : null);
        ref object ptr4 = ref <PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray15<object>, object>(ref <>y__InlineArray, 8);
        CardReadyOutput readyOutput2 = this.ReadyOutput;
        ptr4 = ((readyOutput2 != null) ? readyOutput2.ButtonTitle : null);
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray15<object>, object>(ref <>y__InlineArray, 9) = ((this.Answer == null) ? "" : JsonSerializer.Serialize<ClickyAnswer>(this.Answer, null));
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray15<object>, object>(ref <>y__InlineArray, 10) = this.TaskIsRunning;
        ref object ptr5 = ref <PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray15<object>, object>(ref <>y__InlineArray, 11);
        string text2;
        if (this.Plan != null)
        {
          text2 = string.Join(",", this.Plan.Steps.Select<ClickyArcPlan.Step, string>((ClickyArcPlan.Step s) => s.Text));
        }
        else
        {
          text2 = "-";
        }
        ptr5 = text2;
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray15<object>, object>(ref <>y__InlineArray, 12) = ((this.Plan == null || this.Plan.IsEmpty) ? "noplan" : "plan");
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray15<object>, object>(ref <>y__InlineArray, 13) = ((this.LiveLine == null) ? "noline" : "line");
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray15<object>, object>(ref <>y__InlineArray, 14) = ((this.CurrentStepLine == null) ? "nostep" : "step");
        return string.Join(text, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray15<object>, object>(in <>y__InlineArray, 15));
      }
    }

    // Token: 0x06001FA5 RID: 8101 RVA: 0x0061014C File Offset: 0x0061014C
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ArcCardContent");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001FA6 RID: 8102 RVA: 0x00610198 File Offset: 0x00610198
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("RailKey = ");
      builder.Append(this.RailKey);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", State = ");
      builder.Append(this.State.ToString());
      builder.Append(", Paper = ");
      builder.Append(this.Paper.ToString());
      builder.Append(", Plan = ");
      builder.Append(this.Plan);
      builder.Append(", CurrentStepLine = ");
      builder.Append(this.CurrentStepLine);
      builder.Append(", Summary = ");
      builder.Append(this.Summary);
      builder.Append(", Files = ");
      builder.Append(this.Files);
      builder.Append(", ThreadId = ");
      builder.Append(this.ThreadId);
      builder.Append(", ClickySlug = ");
      builder.Append(this.ClickySlug);
      builder.Append(", JobId = ");
      builder.Append(this.JobId);
      builder.Append(", TaskId = ");
      builder.Append(this.TaskId);
      builder.Append(", WorkspacePath = ");
      builder.Append(this.WorkspacePath);
      builder.Append(", TaskIsRunning = ");
      builder.Append(this.TaskIsRunning.ToString());
      builder.Append(", Request = ");
      builder.Append(this.Request);
      builder.Append(", ReadyOutput = ");
      builder.Append(this.ReadyOutput);
      builder.Append(", LiveLine = ");
      builder.Append(this.LiveLine);
      builder.Append(", Answer = ");
      builder.Append(this.Answer);
      builder.Append(", IsFinalizing = ");
      builder.Append(this.IsFinalizing.ToString());
      builder.Append(", NotePath = ");
      builder.Append(this.NotePath);
      builder.Append(", IsWorking = ");
      builder.Append(this.IsWorking.ToString());
      builder.Append(", IsDone = ");
      builder.Append(this.IsDone.ToString());
      builder.Append(", Shape = ");
      builder.Append(this.Shape);
      return true;
    }

    // Token: 0x06001FA7 RID: 8103 RVA: 0x00610440 File Offset: 0x00610440
    [CompilerGenerated]
    public static bool operator !=(ArcCardContent left, ArcCardContent right)
    {
      return !(left == right);
    }

    // Token: 0x06001FA8 RID: 8104 RVA: 0x0061044C File Offset: 0x0061044C
    [CompilerGenerated]
    public static bool operator ==(ArcCardContent left, ArcCardContent right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001FA9 RID: 8105 RVA: 0x00610460 File Offset: 0x00610460
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RailKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<ArcCardState>.Default.GetHashCode(this.<State>k__BackingField)) * -1521134295 + EqualityComparer<ArcPaper>.Default.GetHashCode(this.<Paper>k__BackingField)) * -1521134295 + EqualityComparer<ClickyArcPlan>.Default.GetHashCode(this.<Plan>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CurrentStepLine>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Summary>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Files>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<JobId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TaskId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WorkspacePath>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<TaskIsRunning>k__BackingField)) * -1521134295 + EqualityComparer<CardFooterRequest>.Default.GetHashCode(this.<Request>k__BackingField)) * -1521134295 + EqualityComparer<CardReadyOutput>.Default.GetHashCode(this.<ReadyOutput>k__BackingField)) * -1521134295 + EqualityComparer<ClickyLiveLine>.Default.GetHashCode(this.<LiveLine>k__BackingField)) * -1521134295 + EqualityComparer<ClickyAnswer>.Default.GetHashCode(this.<Answer>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsFinalizing>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NotePath>k__BackingField);
    }

    // Token: 0x06001FAA RID: 8106 RVA: 0x0061064C File Offset: 0x0061064C
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ArcCardContent);
    }

    // Token: 0x06001FAB RID: 8107 RVA: 0x0061065C File Offset: 0x0061065C
    [CompilerGenerated]
    public bool Equals(ArcCardContent other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<RailKey>k__BackingField, other.<RailKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<ArcCardState>.Default.Equals(this.<State>k__BackingField, other.<State>k__BackingField) && EqualityComparer<ArcPaper>.Default.Equals(this.<Paper>k__BackingField, other.<Paper>k__BackingField) && EqualityComparer<ClickyArcPlan>.Default.Equals(this.<Plan>k__BackingField, other.<Plan>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CurrentStepLine>k__BackingField, other.<CurrentStepLine>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Summary>k__BackingField, other.<Summary>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Files>k__BackingField, other.<Files>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<JobId>k__BackingField, other.<JobId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TaskId>k__BackingField, other.<TaskId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<WorkspacePath>k__BackingField, other.<WorkspacePath>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<TaskIsRunning>k__BackingField, other.<TaskIsRunning>k__BackingField) && EqualityComparer<CardFooterRequest>.Default.Equals(this.<Request>k__BackingField, other.<Request>k__BackingField) && EqualityComparer<CardReadyOutput>.Default.Equals(this.<ReadyOutput>k__BackingField, other.<ReadyOutput>k__BackingField) && EqualityComparer<ClickyLiveLine>.Default.Equals(this.<LiveLine>k__BackingField, other.<LiveLine>k__BackingField) && EqualityComparer<ClickyAnswer>.Default.Equals(this.<Answer>k__BackingField, other.<Answer>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsFinalizing>k__BackingField, other.<IsFinalizing>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<NotePath>k__BackingField, other.<NotePath>k__BackingField));
    }

    // Token: 0x06001FAD RID: 8109 RVA: 0x006108A0 File Offset: 0x006108A0
    [CompilerGenerated]
    [SetsRequiredMembers]
    private ArcCardContent([Nullable(1)] ArcCardContent original)
    {
      this.RailKey = original.<RailKey>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.State = original.<State>k__BackingField;
      this.Paper = original.<Paper>k__BackingField;
      this.Plan = original.<Plan>k__BackingField;
      this.CurrentStepLine = original.<CurrentStepLine>k__BackingField;
      this.Summary = original.<Summary>k__BackingField;
      this.Files = original.<Files>k__BackingField;
      this.ThreadId = original.<ThreadId>k__BackingField;
      this.ClickySlug = original.<ClickySlug>k__BackingField;
      this.JobId = original.<JobId>k__BackingField;
      this.TaskId = original.<TaskId>k__BackingField;
      this.WorkspacePath = original.<WorkspacePath>k__BackingField;
      this.TaskIsRunning = original.<TaskIsRunning>k__BackingField;
      this.Request = original.<Request>k__BackingField;
      this.ReadyOutput = original.<ReadyOutput>k__BackingField;
      this.LiveLine = original.<LiveLine>k__BackingField;
      this.Answer = original.<Answer>k__BackingField;
      this.IsFinalizing = original.<IsFinalizing>k__BackingField;
      this.NotePath = original.<NotePath>k__BackingField;
    }

    // Token: 0x06001FAE RID: 8110 RVA: 0x006109A4 File Offset: 0x006109A4
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public ArcCardContent()
    {
      this.Files = Array.Empty<string>();
      base..ctor();
    }
  }
}
