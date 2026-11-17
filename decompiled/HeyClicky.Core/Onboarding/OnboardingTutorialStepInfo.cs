using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Onboarding
{
  // Token: 0x020000F9 RID: 249
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class OnboardingTutorialStepInfo : IEquatable<OnboardingTutorialStepInfo>
  {
    // Token: 0x06000FD7 RID: 4055 RVA: 0x0039051C File Offset: 0x0039051C
    public OnboardingTutorialStepInfo(OnboardingTutorialStep Step, bool AllowsPushToTalk, bool AllowsTextMode, bool CompletesOnTurn, bool AutoAdvancesAfterCompletion, double AutoAdvanceDelaySeconds, string SuccessCursorLine, [Nullable(1)] string Title, string Body)
    {
      this.Step = Step;
      this.AllowsPushToTalk = AllowsPushToTalk;
      this.AllowsTextMode = AllowsTextMode;
      this.CompletesOnTurn = CompletesOnTurn;
      this.AutoAdvancesAfterCompletion = AutoAdvancesAfterCompletion;
      this.AutoAdvanceDelaySeconds = AutoAdvanceDelaySeconds;
      this.SuccessCursorLine = SuccessCursorLine;
      this.Title = Title;
      this.Body = Body;
      base..ctor();
    }

    // Token: 0x1700039F RID: 927
    // (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00390574 File Offset: 0x00390574
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(OnboardingTutorialStepInfo);
      }
    }

    // Token: 0x170003A0 RID: 928
    // (get) Token: 0x06000FD9 RID: 4057 RVA: 0x00390580 File Offset: 0x00390580
    // (set) Token: 0x06000FDA RID: 4058 RVA: 0x00390588 File Offset: 0x00390588
    public OnboardingTutorialStep Step { get; set; }

    // Token: 0x170003A1 RID: 929
    // (get) Token: 0x06000FDB RID: 4059 RVA: 0x00390594 File Offset: 0x00390594
    // (set) Token: 0x06000FDC RID: 4060 RVA: 0x0039059C File Offset: 0x0039059C
    public bool AllowsPushToTalk { get; set; }

    // Token: 0x170003A2 RID: 930
    // (get) Token: 0x06000FDD RID: 4061 RVA: 0x003905A8 File Offset: 0x003905A8
    // (set) Token: 0x06000FDE RID: 4062 RVA: 0x003905B0 File Offset: 0x003905B0
    public bool AllowsTextMode { get; set; }

    // Token: 0x170003A3 RID: 931
    // (get) Token: 0x06000FDF RID: 4063 RVA: 0x003905BC File Offset: 0x003905BC
    // (set) Token: 0x06000FE0 RID: 4064 RVA: 0x003905C4 File Offset: 0x003905C4
    public bool CompletesOnTurn { get; set; }

    // Token: 0x170003A4 RID: 932
    // (get) Token: 0x06000FE1 RID: 4065 RVA: 0x003905D0 File Offset: 0x003905D0
    // (set) Token: 0x06000FE2 RID: 4066 RVA: 0x003905D8 File Offset: 0x003905D8
    public bool AutoAdvancesAfterCompletion { get; set; }

    // Token: 0x170003A5 RID: 933
    // (get) Token: 0x06000FE3 RID: 4067 RVA: 0x003905E4 File Offset: 0x003905E4
    // (set) Token: 0x06000FE4 RID: 4068 RVA: 0x003905EC File Offset: 0x003905EC
    public double AutoAdvanceDelaySeconds { get; set; }

    // Token: 0x170003A6 RID: 934
    // (get) Token: 0x06000FE5 RID: 4069 RVA: 0x003905F8 File Offset: 0x003905F8
    // (set) Token: 0x06000FE6 RID: 4070 RVA: 0x00390600 File Offset: 0x00390600
    public string SuccessCursorLine { get; set; }

    // Token: 0x170003A7 RID: 935
    // (get) Token: 0x06000FE7 RID: 4071 RVA: 0x0039060C File Offset: 0x0039060C
    // (set) Token: 0x06000FE8 RID: 4072 RVA: 0x00390614 File Offset: 0x00390614
    [Nullable(1)]
    public string Title
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170003A8 RID: 936
    // (get) Token: 0x06000FE9 RID: 4073 RVA: 0x00390620 File Offset: 0x00390620
    // (set) Token: 0x06000FEA RID: 4074 RVA: 0x00390628 File Offset: 0x00390628
    public string Body { get; set; }

    // Token: 0x06000FEB RID: 4075 RVA: 0x00390634 File Offset: 0x00390634
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("OnboardingTutorialStepInfo");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000FEC RID: 4076 RVA: 0x00390680 File Offset: 0x00390680
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Step = ");
      builder.Append(this.Step.ToString());
      builder.Append(", AllowsPushToTalk = ");
      builder.Append(this.AllowsPushToTalk.ToString());
      builder.Append(", AllowsTextMode = ");
      builder.Append(this.AllowsTextMode.ToString());
      builder.Append(", CompletesOnTurn = ");
      builder.Append(this.CompletesOnTurn.ToString());
      builder.Append(", AutoAdvancesAfterCompletion = ");
      builder.Append(this.AutoAdvancesAfterCompletion.ToString());
      builder.Append(", AutoAdvanceDelaySeconds = ");
      builder.Append(this.AutoAdvanceDelaySeconds.ToString());
      builder.Append(", SuccessCursorLine = ");
      builder.Append(this.SuccessCursorLine);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", Body = ");
      builder.Append(this.Body);
      return true;
    }

    // Token: 0x06000FED RID: 4077 RVA: 0x003907C8 File Offset: 0x003907C8
    [CompilerGenerated]
    public static bool operator !=(OnboardingTutorialStepInfo left, OnboardingTutorialStepInfo right)
    {
      return !(left == right);
    }

    // Token: 0x06000FEE RID: 4078 RVA: 0x003907D4 File Offset: 0x003907D4
    [CompilerGenerated]
    public static bool operator ==(OnboardingTutorialStepInfo left, OnboardingTutorialStepInfo right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000FEF RID: 4079 RVA: 0x003907E8 File Offset: 0x003907E8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<OnboardingTutorialStep>.Default.GetHashCode(this.<Step>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<AllowsPushToTalk>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<AllowsTextMode>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<CompletesOnTurn>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<AutoAdvancesAfterCompletion>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<AutoAdvanceDelaySeconds>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SuccessCursorLine>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Body>k__BackingField);
    }

    // Token: 0x06000FF0 RID: 4080 RVA: 0x003908D4 File Offset: 0x003908D4
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as OnboardingTutorialStepInfo);
    }

    // Token: 0x06000FF1 RID: 4081 RVA: 0x003908E4 File Offset: 0x003908E4
    [CompilerGenerated]
    public bool Equals(OnboardingTutorialStepInfo other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<OnboardingTutorialStep>.Default.Equals(this.<Step>k__BackingField, other.<Step>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<AllowsPushToTalk>k__BackingField, other.<AllowsPushToTalk>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<AllowsTextMode>k__BackingField, other.<AllowsTextMode>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<CompletesOnTurn>k__BackingField, other.<CompletesOnTurn>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<AutoAdvancesAfterCompletion>k__BackingField, other.<AutoAdvancesAfterCompletion>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<AutoAdvanceDelaySeconds>k__BackingField, other.<AutoAdvanceDelaySeconds>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SuccessCursorLine>k__BackingField, other.<SuccessCursorLine>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Body>k__BackingField, other.<Body>k__BackingField));
    }

    // Token: 0x06000FF3 RID: 4083 RVA: 0x00390A00 File Offset: 0x00390A00
    [CompilerGenerated]
    private OnboardingTutorialStepInfo([Nullable(1)] OnboardingTutorialStepInfo original)
    {
      this.Step = original.<Step>k__BackingField;
      this.AllowsPushToTalk = original.<AllowsPushToTalk>k__BackingField;
      this.AllowsTextMode = original.<AllowsTextMode>k__BackingField;
      this.CompletesOnTurn = original.<CompletesOnTurn>k__BackingField;
      this.AutoAdvancesAfterCompletion = original.<AutoAdvancesAfterCompletion>k__BackingField;
      this.AutoAdvanceDelaySeconds = original.<AutoAdvanceDelaySeconds>k__BackingField;
      this.SuccessCursorLine = original.<SuccessCursorLine>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.Body = original.<Body>k__BackingField;
    }

    // Token: 0x06000FF4 RID: 4084 RVA: 0x00390A80 File Offset: 0x00390A80
    [CompilerGenerated]
    public void Deconstruct(out OnboardingTutorialStep Step, out bool AllowsPushToTalk, out bool AllowsTextMode, out bool CompletesOnTurn, out bool AutoAdvancesAfterCompletion, out double AutoAdvanceDelaySeconds, out string SuccessCursorLine, [Nullable(1)] out string Title, out string Body)
    {
      Step = this.Step;
      AllowsPushToTalk = this.AllowsPushToTalk;
      AllowsTextMode = this.AllowsTextMode;
      CompletesOnTurn = this.CompletesOnTurn;
      AutoAdvancesAfterCompletion = this.AutoAdvancesAfterCompletion;
      AutoAdvanceDelaySeconds = this.AutoAdvanceDelaySeconds;
      SuccessCursorLine = this.SuccessCursorLine;
      Title = this.Title;
      Body = this.Body;
    }
  }
}
