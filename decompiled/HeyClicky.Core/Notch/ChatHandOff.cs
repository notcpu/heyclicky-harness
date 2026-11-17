using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace HeyClicky.Core.Notch
{
  // Token: 0x020000FD RID: 253
  [NullableContext(2)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class ChatHandOff : IEquatable<ChatHandOff>
  {
    // Token: 0x170003BB RID: 955
    // (get) Token: 0x06001042 RID: 4162 RVA: 0x00391BE8 File Offset: 0x00391BE8
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ChatHandOff);
      }
    }

    // Token: 0x170003BC RID: 956
    // (get) Token: 0x06001043 RID: 4163 RVA: 0x00391BF4 File Offset: 0x00391BF4
    // (set) Token: 0x06001044 RID: 4164 RVA: 0x00391BFC File Offset: 0x00391BFC
    [Nullable(1)]
    [RequiredMember]
    public string Id
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170003BD RID: 957
    // (get) Token: 0x06001045 RID: 4165 RVA: 0x00391C08 File Offset: 0x00391C08
    // (set) Token: 0x06001046 RID: 4166 RVA: 0x00391C10 File Offset: 0x00391C10
    [RequiredMember]
    public ChatHandOffKind Kind { get; set; }

    // Token: 0x170003BE RID: 958
    // (get) Token: 0x06001047 RID: 4167 RVA: 0x00391C1C File Offset: 0x00391C1C
    // (set) Token: 0x06001048 RID: 4168 RVA: 0x00391C24 File Offset: 0x00391C24
    public string JobId { get; set; }

    // Token: 0x170003BF RID: 959
    // (get) Token: 0x06001049 RID: 4169 RVA: 0x00391C30 File Offset: 0x00391C30
    // (set) Token: 0x0600104A RID: 4170 RVA: 0x00391C38 File Offset: 0x00391C38
    public string ClickySlug { get; set; }

    // Token: 0x170003C0 RID: 960
    // (get) Token: 0x0600104B RID: 4171 RVA: 0x00391C44 File Offset: 0x00391C44
    // (set) Token: 0x0600104C RID: 4172 RVA: 0x00391C4C File Offset: 0x00391C4C
    [Nullable(1)]
    [RequiredMember]
    public string Title
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170003C1 RID: 961
    // (get) Token: 0x0600104D RID: 4173 RVA: 0x00391C58 File Offset: 0x00391C58
    // (set) Token: 0x0600104E RID: 4174 RVA: 0x00391C60 File Offset: 0x00391C60
    [RequiredMember]
    public DateTimeOffset CreatedAt { get; set; }

    // Token: 0x170003C2 RID: 962
    // (get) Token: 0x0600104F RID: 4175 RVA: 0x00391C6C File Offset: 0x00391C6C
    // (set) Token: 0x06001050 RID: 4176 RVA: 0x00391C74 File Offset: 0x00391C74
    public bool IsFollowUp { get; set; }

    // Token: 0x170003C3 RID: 963
    // (get) Token: 0x06001051 RID: 4177 RVA: 0x00391C80 File Offset: 0x00391C80
    // (set) Token: 0x06001052 RID: 4178 RVA: 0x00391C88 File Offset: 0x00391C88
    public bool IsSentFromWindow { get; set; }

    // Token: 0x170003C4 RID: 964
    // (get) Token: 0x06001053 RID: 4179 RVA: 0x00391C94 File Offset: 0x00391C94
    // (set) Token: 0x06001054 RID: 4180 RVA: 0x00391C9C File Offset: 0x00391C9C
    public ChatHandOffOutcome? Outcome { get; set; }

    // Token: 0x170003C5 RID: 965
    // (get) Token: 0x06001055 RID: 4181 RVA: 0x00391CA8 File Offset: 0x00391CA8
    // (set) Token: 0x06001056 RID: 4182 RVA: 0x00391CB0 File Offset: 0x00391CB0
    public string ReplyText { get; set; }

    // Token: 0x170003C6 RID: 966
    // (get) Token: 0x06001057 RID: 4183 RVA: 0x00391CBC File Offset: 0x00391CBC
    // (set) Token: 0x06001058 RID: 4184 RVA: 0x00391CC4 File Offset: 0x00391CC4
    public string LessonId { get; set; }

    // Token: 0x170003C7 RID: 967
    // (get) Token: 0x06001059 RID: 4185 RVA: 0x00391CD0 File Offset: 0x00391CD0
    // (set) Token: 0x0600105A RID: 4186 RVA: 0x00391CD8 File Offset: 0x00391CD8
    public string DeliveredText { get; set; }

    // Token: 0x170003C8 RID: 968
    // (get) Token: 0x0600105B RID: 4187 RVA: 0x00391CE4 File Offset: 0x00391CE4
    // (set) Token: 0x0600105C RID: 4188 RVA: 0x00391CEC File Offset: 0x00391CEC
    public string DeliveredToApp { get; set; }

    // Token: 0x170003C9 RID: 969
    // (get) Token: 0x0600105D RID: 4189 RVA: 0x00391CF8 File Offset: 0x00391CF8
    // (set) Token: 0x0600105E RID: 4190 RVA: 0x00391D00 File Offset: 0x00391D00
    public bool? WasCopied { get; set; }

    // Token: 0x170003CA RID: 970
    // (get) Token: 0x0600105F RID: 4191 RVA: 0x00391D0C File Offset: 0x00391D0C
    // (set) Token: 0x06001060 RID: 4192 RVA: 0x00391D14 File Offset: 0x00391D14
    public string FullReply { get; set; }

    // Token: 0x170003CB RID: 971
    // (get) Token: 0x06001061 RID: 4193 RVA: 0x00391D20 File Offset: 0x00391D20
    // (set) Token: 0x06001062 RID: 4194 RVA: 0x00391D28 File Offset: 0x00391D28
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> ArtifactEntries
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x170003CC RID: 972
    // (get) Token: 0x06001063 RID: 4195 RVA: 0x00391D34 File Offset: 0x00391D34
    // (set) Token: 0x06001064 RID: 4196 RVA: 0x00391D3C File Offset: 0x00391D3C
    public JsonObject Answer { get; set; }

    // Token: 0x170003CD RID: 973
    // (get) Token: 0x06001065 RID: 4197 RVA: 0x00391D48 File Offset: 0x00391D48
    // (set) Token: 0x06001066 RID: 4198 RVA: 0x00391D50 File Offset: 0x00391D50
    public string ThreadId { get; set; }

    // Token: 0x170003CE RID: 974
    // (get) Token: 0x06001067 RID: 4199 RVA: 0x00391D5C File Offset: 0x00391D5C
    // (set) Token: 0x06001068 RID: 4200 RVA: 0x00391D64 File Offset: 0x00391D64
    public JsonObject Card { get; set; }

    // Token: 0x170003CF RID: 975
    // (get) Token: 0x06001069 RID: 4201 RVA: 0x00391D70 File Offset: 0x00391D70
    // (set) Token: 0x0600106A RID: 4202 RVA: 0x00391D78 File Offset: 0x00391D78
    public bool? IsRoutineRun { get; set; }

    // Token: 0x170003D0 RID: 976
    // (get) Token: 0x0600106B RID: 4203 RVA: 0x00391D84 File Offset: 0x00391D84
    // (set) Token: 0x0600106C RID: 4204 RVA: 0x00391D8C File Offset: 0x00391D8C
    public string TaskTitle { get; set; }

    // Token: 0x170003D1 RID: 977
    // (get) Token: 0x0600106D RID: 4205 RVA: 0x00391D98 File Offset: 0x00391D98
    // (set) Token: 0x0600106E RID: 4206 RVA: 0x00391DA0 File Offset: 0x00391DA0
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> FollowUps
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x170003D2 RID: 978
    // (get) Token: 0x0600106F RID: 4207 RVA: 0x00391DAC File Offset: 0x00391DAC
    // (set) Token: 0x06001070 RID: 4208 RVA: 0x00391DB4 File Offset: 0x00391DB4
    public string SentText { get; set; }

    // Token: 0x170003D3 RID: 979
    // (get) Token: 0x06001071 RID: 4209 RVA: 0x00391DC0 File Offset: 0x00391DC0
    // (set) Token: 0x06001072 RID: 4210 RVA: 0x00391DC8 File Offset: 0x00391DC8
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> SentFiles
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x170003D4 RID: 980
    // (get) Token: 0x06001073 RID: 4211 RVA: 0x00391DD4 File Offset: 0x00391DD4
    // (set) Token: 0x06001074 RID: 4212 RVA: 0x00391DDC File Offset: 0x00391DDC
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> SitesRead
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x170003D5 RID: 981
    // (get) Token: 0x06001075 RID: 4213 RVA: 0x00391DE8 File Offset: 0x00391DE8
    [Nullable(1)]
    [JsonIgnore]
    public string TargetKey
    {
      [NullableContext(1)]
      get
      {
        ChatHandOffKind kind = this.Kind;
        string text;
        if (kind != ChatHandOffKind.Drawing)
        {
          if (kind != ChatHandOffKind.Text)
          {
            text = ((this.JobId != null) ? ("job:" + this.JobId) : ("clicky:" + this.ClickySlug));
          }
          else
          {
            text = "text:" + this.Id;
          }
        }
        else
        {
          text = "drawing:" + (this.LessonId ?? this.Id);
        }
        return text;
      }
    }

    // Token: 0x06001076 RID: 4214 RVA: 0x00391E64 File Offset: 0x00391E64
    [return: Nullable(1)]
    public static string TargetKeyFor(string jobId, string clickySlug)
    {
      if (jobId == null)
      {
        return "clicky:" + clickySlug;
      }
      return "job:" + jobId;
    }

    // Token: 0x06001077 RID: 4215 RVA: 0x00391E80 File Offset: 0x00391E80
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ChatHandOff");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001078 RID: 4216 RVA: 0x00391ECC File Offset: 0x00391ECC
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", JobId = ");
      builder.Append(this.JobId);
      builder.Append(", ClickySlug = ");
      builder.Append(this.ClickySlug);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", CreatedAt = ");
      builder.Append(this.CreatedAt.ToString());
      builder.Append(", IsFollowUp = ");
      builder.Append(this.IsFollowUp.ToString());
      builder.Append(", IsSentFromWindow = ");
      builder.Append(this.IsSentFromWindow.ToString());
      builder.Append(", Outcome = ");
      builder.Append(this.Outcome.ToString());
      builder.Append(", ReplyText = ");
      builder.Append(this.ReplyText);
      builder.Append(", LessonId = ");
      builder.Append(this.LessonId);
      builder.Append(", DeliveredText = ");
      builder.Append(this.DeliveredText);
      builder.Append(", DeliveredToApp = ");
      builder.Append(this.DeliveredToApp);
      builder.Append(", WasCopied = ");
      builder.Append(this.WasCopied.ToString());
      builder.Append(", FullReply = ");
      builder.Append(this.FullReply);
      builder.Append(", ArtifactEntries = ");
      builder.Append(this.ArtifactEntries);
      builder.Append(", Answer = ");
      builder.Append(this.Answer);
      builder.Append(", ThreadId = ");
      builder.Append(this.ThreadId);
      builder.Append(", Card = ");
      builder.Append(this.Card);
      builder.Append(", IsRoutineRun = ");
      builder.Append(this.IsRoutineRun.ToString());
      builder.Append(", TaskTitle = ");
      builder.Append(this.TaskTitle);
      builder.Append(", FollowUps = ");
      builder.Append(this.FollowUps);
      builder.Append(", SentText = ");
      builder.Append(this.SentText);
      builder.Append(", SentFiles = ");
      builder.Append(this.SentFiles);
      builder.Append(", SitesRead = ");
      builder.Append(this.SitesRead);
      builder.Append(", TargetKey = ");
      builder.Append(this.TargetKey);
      return true;
    }

    // Token: 0x06001079 RID: 4217 RVA: 0x003921D0 File Offset: 0x003921D0
    [CompilerGenerated]
    public static bool operator !=(ChatHandOff left, ChatHandOff right)
    {
      return !(left == right);
    }

    // Token: 0x0600107A RID: 4218 RVA: 0x003921DC File Offset: 0x003921DC
    [CompilerGenerated]
    public static bool operator ==(ChatHandOff left, ChatHandOff right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600107B RID: 4219 RVA: 0x003921F0 File Offset: 0x003921F0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((((((((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<ChatHandOffKind>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<JobId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<CreatedAt>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsFollowUp>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsSentFromWindow>k__BackingField)) * -1521134295 + EqualityComparer<ChatHandOffOutcome?>.Default.GetHashCode(this.<Outcome>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ReplyText>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LessonId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DeliveredText>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DeliveredToApp>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<WasCopied>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FullReply>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<ArtifactEntries>k__BackingField)) * -1521134295 + EqualityComparer<JsonObject>.Default.GetHashCode(this.<Answer>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<JsonObject>.Default.GetHashCode(this.<Card>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<IsRoutineRun>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TaskTitle>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<FollowUps>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SentText>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<SentFiles>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<SitesRead>k__BackingField);
    }

    // Token: 0x0600107C RID: 4220 RVA: 0x0039244C File Offset: 0x0039244C
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ChatHandOff);
    }

    // Token: 0x0600107D RID: 4221 RVA: 0x0039245C File Offset: 0x0039245C
    [CompilerGenerated]
    public bool Equals(ChatHandOff other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<ChatHandOffKind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<JobId>k__BackingField, other.<JobId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<CreatedAt>k__BackingField, other.<CreatedAt>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsFollowUp>k__BackingField, other.<IsFollowUp>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsSentFromWindow>k__BackingField, other.<IsSentFromWindow>k__BackingField) && EqualityComparer<ChatHandOffOutcome?>.Default.Equals(this.<Outcome>k__BackingField, other.<Outcome>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ReplyText>k__BackingField, other.<ReplyText>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LessonId>k__BackingField, other.<LessonId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DeliveredText>k__BackingField, other.<DeliveredText>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DeliveredToApp>k__BackingField, other.<DeliveredToApp>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<WasCopied>k__BackingField, other.<WasCopied>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FullReply>k__BackingField, other.<FullReply>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<ArtifactEntries>k__BackingField, other.<ArtifactEntries>k__BackingField) && EqualityComparer<JsonObject>.Default.Equals(this.<Answer>k__BackingField, other.<Answer>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<JsonObject>.Default.Equals(this.<Card>k__BackingField, other.<Card>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<IsRoutineRun>k__BackingField, other.<IsRoutineRun>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TaskTitle>k__BackingField, other.<TaskTitle>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<FollowUps>k__BackingField, other.<FollowUps>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SentText>k__BackingField, other.<SentText>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<SentFiles>k__BackingField, other.<SentFiles>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<SitesRead>k__BackingField, other.<SitesRead>k__BackingField));
    }

    // Token: 0x0600107F RID: 4223 RVA: 0x00392728 File Offset: 0x00392728
    [CompilerGenerated]
    [SetsRequiredMembers]
    private ChatHandOff([Nullable(1)] ChatHandOff original)
    {
      this.Id = original.<Id>k__BackingField;
      this.Kind = original.<Kind>k__BackingField;
      this.JobId = original.<JobId>k__BackingField;
      this.ClickySlug = original.<ClickySlug>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.CreatedAt = original.<CreatedAt>k__BackingField;
      this.IsFollowUp = original.<IsFollowUp>k__BackingField;
      this.IsSentFromWindow = original.<IsSentFromWindow>k__BackingField;
      this.Outcome = original.<Outcome>k__BackingField;
      this.ReplyText = original.<ReplyText>k__BackingField;
      this.LessonId = original.<LessonId>k__BackingField;
      this.DeliveredText = original.<DeliveredText>k__BackingField;
      this.DeliveredToApp = original.<DeliveredToApp>k__BackingField;
      this.WasCopied = original.<WasCopied>k__BackingField;
      this.FullReply = original.<FullReply>k__BackingField;
      this.ArtifactEntries = original.<ArtifactEntries>k__BackingField;
      this.Answer = original.<Answer>k__BackingField;
      this.ThreadId = original.<ThreadId>k__BackingField;
      this.Card = original.<Card>k__BackingField;
      this.IsRoutineRun = original.<IsRoutineRun>k__BackingField;
      this.TaskTitle = original.<TaskTitle>k__BackingField;
      this.FollowUps = original.<FollowUps>k__BackingField;
      this.SentText = original.<SentText>k__BackingField;
      this.SentFiles = original.<SentFiles>k__BackingField;
      this.SitesRead = original.<SitesRead>k__BackingField;
    }

    // Token: 0x06001080 RID: 4224 RVA: 0x00392868 File Offset: 0x00392868
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public ChatHandOff()
    {
    }
  }
}
