using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Notch
{
  // Token: 0x020000FF RID: 255
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ChatTaskEnd : IEquatable<ChatTaskEnd>
  {
    // Token: 0x170003D6 RID: 982
    // (get) Token: 0x06001085 RID: 4229 RVA: 0x00392870 File Offset: 0x00392870
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ChatTaskEnd);
      }
    }

    // Token: 0x170003D7 RID: 983
    // (get) Token: 0x06001086 RID: 4230 RVA: 0x0039287C File Offset: 0x0039287C
    // (set) Token: 0x06001087 RID: 4231 RVA: 0x00392884 File Offset: 0x00392884
    public string JobId { get; set; }

    // Token: 0x170003D8 RID: 984
    // (get) Token: 0x06001088 RID: 4232 RVA: 0x00392890 File Offset: 0x00392890
    // (set) Token: 0x06001089 RID: 4233 RVA: 0x00392898 File Offset: 0x00392898
    public string ClickySlug { get; set; }

    // Token: 0x170003D9 RID: 985
    // (get) Token: 0x0600108A RID: 4234 RVA: 0x003928A4 File Offset: 0x003928A4
    // (set) Token: 0x0600108B RID: 4235 RVA: 0x003928AC File Offset: 0x003928AC
    public string ThreadId { get; set; }

    // Token: 0x170003DA RID: 986
    // (get) Token: 0x0600108C RID: 4236 RVA: 0x003928B8 File Offset: 0x003928B8
    // (set) Token: 0x0600108D RID: 4237 RVA: 0x003928C0 File Offset: 0x003928C0
    public bool IsDemo { get; set; }

    // Token: 0x170003DB RID: 987
    // (get) Token: 0x0600108E RID: 4238 RVA: 0x003928CC File Offset: 0x003928CC
    // (set) Token: 0x0600108F RID: 4239 RVA: 0x003928D4 File Offset: 0x003928D4
    public string PendingComputerUseRequest { get; set; }

    // Token: 0x170003DC RID: 988
    // (get) Token: 0x06001090 RID: 4240 RVA: 0x003928E0 File Offset: 0x003928E0
    // (set) Token: 0x06001091 RID: 4241 RVA: 0x003928E8 File Offset: 0x003928E8
    public string PendingUserStep { get; set; }

    // Token: 0x170003DD RID: 989
    // (get) Token: 0x06001092 RID: 4242 RVA: 0x003928F4 File Offset: 0x003928F4
    // (set) Token: 0x06001093 RID: 4243 RVA: 0x003928FC File Offset: 0x003928FC
    public bool NeedsChromeConnection { get; set; }

    // Token: 0x170003DE RID: 990
    // (get) Token: 0x06001094 RID: 4244 RVA: 0x00392908 File Offset: 0x00392908
    // (set) Token: 0x06001095 RID: 4245 RVA: 0x00392910 File Offset: 0x00392910
    public string FinalSummary { get; set; }

    // Token: 0x170003DF RID: 991
    // (get) Token: 0x06001096 RID: 4246 RVA: 0x0039291C File Offset: 0x0039291C
    // (set) Token: 0x06001097 RID: 4247 RVA: 0x00392924 File Offset: 0x00392924
    public string FinalResponseMarkdown { get; set; }

    // Token: 0x170003E0 RID: 992
    // (get) Token: 0x06001098 RID: 4248 RVA: 0x00392930 File Offset: 0x00392930
    // (set) Token: 0x06001099 RID: 4249 RVA: 0x00392938 File Offset: 0x00392938
    [Nullable(1)]
    public IReadOnlyList<string> CompletedAssistantMessages
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170003E1 RID: 993
    // (get) Token: 0x0600109A RID: 4250 RVA: 0x00392944 File Offset: 0x00392944
    // (set) Token: 0x0600109B RID: 4251 RVA: 0x0039294C File Offset: 0x0039294C
    [Nullable(1)]
    public IReadOnlyList<string> UserFacingArtifactPaths
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170003E2 RID: 994
    // (get) Token: 0x0600109C RID: 4252 RVA: 0x00392958 File Offset: 0x00392958
    // (set) Token: 0x0600109D RID: 4253 RVA: 0x00392960 File Offset: 0x00392960
    public JsonObject FinalAnswer { get; set; }

    // Token: 0x170003E3 RID: 995
    // (get) Token: 0x0600109E RID: 4254 RVA: 0x0039296C File Offset: 0x0039296C
    // (set) Token: 0x0600109F RID: 4255 RVA: 0x00392974 File Offset: 0x00392974
    [Nullable(1)]
    public IReadOnlyList<string> SuggestedNextActions
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170003E4 RID: 996
    // (get) Token: 0x060010A0 RID: 4256 RVA: 0x00392980 File Offset: 0x00392980
    // (set) Token: 0x060010A1 RID: 4257 RVA: 0x00392988 File Offset: 0x00392988
    public bool IsScheduledRoutineRun { get; set; }

    // Token: 0x170003E5 RID: 997
    // (get) Token: 0x060010A2 RID: 4258 RVA: 0x00392994 File Offset: 0x00392994
    // (set) Token: 0x060010A3 RID: 4259 RVA: 0x0039299C File Offset: 0x0039299C
    public string CompletedThreadName { get; set; }

    // Token: 0x170003E6 RID: 998
    // (get) Token: 0x060010A4 RID: 4260 RVA: 0x003929A8 File Offset: 0x003929A8
    // (set) Token: 0x060010A5 RID: 4261 RVA: 0x003929B0 File Offset: 0x003929B0
    [Nullable(1)]
    public IReadOnlyList<string> SitesRead
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x060010A6 RID: 4262 RVA: 0x003929BC File Offset: 0x003929BC
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ChatTaskEnd");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060010A7 RID: 4263 RVA: 0x00392A08 File Offset: 0x00392A08
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("JobId = ");
      builder.Append(this.JobId);
      builder.Append(", ClickySlug = ");
      builder.Append(this.ClickySlug);
      builder.Append(", ThreadId = ");
      builder.Append(this.ThreadId);
      builder.Append(", IsDemo = ");
      builder.Append(this.IsDemo.ToString());
      builder.Append(", PendingComputerUseRequest = ");
      builder.Append(this.PendingComputerUseRequest);
      builder.Append(", PendingUserStep = ");
      builder.Append(this.PendingUserStep);
      builder.Append(", NeedsChromeConnection = ");
      builder.Append(this.NeedsChromeConnection.ToString());
      builder.Append(", FinalSummary = ");
      builder.Append(this.FinalSummary);
      builder.Append(", FinalResponseMarkdown = ");
      builder.Append(this.FinalResponseMarkdown);
      builder.Append(", CompletedAssistantMessages = ");
      builder.Append(this.CompletedAssistantMessages);
      builder.Append(", UserFacingArtifactPaths = ");
      builder.Append(this.UserFacingArtifactPaths);
      builder.Append(", FinalAnswer = ");
      builder.Append(this.FinalAnswer);
      builder.Append(", SuggestedNextActions = ");
      builder.Append(this.SuggestedNextActions);
      builder.Append(", IsScheduledRoutineRun = ");
      builder.Append(this.IsScheduledRoutineRun.ToString());
      builder.Append(", CompletedThreadName = ");
      builder.Append(this.CompletedThreadName);
      builder.Append(", SitesRead = ");
      builder.Append(this.SitesRead);
      return true;
    }

    // Token: 0x060010A8 RID: 4264 RVA: 0x00392BD8 File Offset: 0x00392BD8
    [CompilerGenerated]
    public static bool operator !=(ChatTaskEnd left, ChatTaskEnd right)
    {
      return !(left == right);
    }

    // Token: 0x060010A9 RID: 4265 RVA: 0x00392BE4 File Offset: 0x00392BE4
    [CompilerGenerated]
    public static bool operator ==(ChatTaskEnd left, ChatTaskEnd right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060010AA RID: 4266 RVA: 0x00392BF8 File Offset: 0x00392BF8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<JobId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsDemo>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PendingComputerUseRequest>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PendingUserStep>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<NeedsChromeConnection>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FinalSummary>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FinalResponseMarkdown>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<CompletedAssistantMessages>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<UserFacingArtifactPaths>k__BackingField)) * -1521134295 + EqualityComparer<JsonObject>.Default.GetHashCode(this.<FinalAnswer>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<SuggestedNextActions>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsScheduledRoutineRun>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CompletedThreadName>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<SitesRead>k__BackingField);
    }

    // Token: 0x060010AB RID: 4267 RVA: 0x00392D88 File Offset: 0x00392D88
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ChatTaskEnd);
    }

    // Token: 0x060010AC RID: 4268 RVA: 0x00392D98 File Offset: 0x00392D98
    [CompilerGenerated]
    public bool Equals(ChatTaskEnd other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<JobId>k__BackingField, other.<JobId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsDemo>k__BackingField, other.<IsDemo>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<PendingComputerUseRequest>k__BackingField, other.<PendingComputerUseRequest>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<PendingUserStep>k__BackingField, other.<PendingUserStep>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<NeedsChromeConnection>k__BackingField, other.<NeedsChromeConnection>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FinalSummary>k__BackingField, other.<FinalSummary>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FinalResponseMarkdown>k__BackingField, other.<FinalResponseMarkdown>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<CompletedAssistantMessages>k__BackingField, other.<CompletedAssistantMessages>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<UserFacingArtifactPaths>k__BackingField, other.<UserFacingArtifactPaths>k__BackingField) && EqualityComparer<JsonObject>.Default.Equals(this.<FinalAnswer>k__BackingField, other.<FinalAnswer>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<SuggestedNextActions>k__BackingField, other.<SuggestedNextActions>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsScheduledRoutineRun>k__BackingField, other.<IsScheduledRoutineRun>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CompletedThreadName>k__BackingField, other.<CompletedThreadName>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<SitesRead>k__BackingField, other.<SitesRead>k__BackingField));
    }

    // Token: 0x060010AE RID: 4270 RVA: 0x00392F70 File Offset: 0x00392F70
    [CompilerGenerated]
    private ChatTaskEnd([Nullable(1)] ChatTaskEnd original)
    {
      this.JobId = original.<JobId>k__BackingField;
      this.ClickySlug = original.<ClickySlug>k__BackingField;
      this.ThreadId = original.<ThreadId>k__BackingField;
      this.IsDemo = original.<IsDemo>k__BackingField;
      this.PendingComputerUseRequest = original.<PendingComputerUseRequest>k__BackingField;
      this.PendingUserStep = original.<PendingUserStep>k__BackingField;
      this.NeedsChromeConnection = original.<NeedsChromeConnection>k__BackingField;
      this.FinalSummary = original.<FinalSummary>k__BackingField;
      this.FinalResponseMarkdown = original.<FinalResponseMarkdown>k__BackingField;
      this.CompletedAssistantMessages = original.<CompletedAssistantMessages>k__BackingField;
      this.UserFacingArtifactPaths = original.<UserFacingArtifactPaths>k__BackingField;
      this.FinalAnswer = original.<FinalAnswer>k__BackingField;
      this.SuggestedNextActions = original.<SuggestedNextActions>k__BackingField;
      this.IsScheduledRoutineRun = original.<IsScheduledRoutineRun>k__BackingField;
      this.CompletedThreadName = original.<CompletedThreadName>k__BackingField;
      this.SitesRead = original.<SitesRead>k__BackingField;
    }

    // Token: 0x060010AF RID: 4271 RVA: 0x00393044 File Offset: 0x00393044
    public ChatTaskEnd()
    {
      this.CompletedAssistantMessages = Array.Empty<string>();
      this.UserFacingArtifactPaths = Array.Empty<string>();
      this.SuggestedNextActions = Array.Empty<string>();
      this.SitesRead = Array.Empty<string>();
      base..ctor();
    }
  }
}
