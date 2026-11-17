using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents
{
  // Token: 0x0200000E RID: 14
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentTranscriptTurn : IEquatable<AgentTranscriptTurn>
  {
    // Token: 0x060000E2 RID: 226 RVA: 0x000D8B64 File Offset: 0x000D8B64
    public AgentTranscriptTurn([Nullable(2)] string TurnId, IReadOnlyList<string> UserMessages, IReadOnlyList<string> WorkSteps, IReadOnlyList<string> AssistantMessages, IReadOnlyList<string> NextActions, IReadOnlyList<string> Artifacts, string Status, [Nullable(2)] string ErrorMessage, DateTimeOffset? StartedAt = null, DateTimeOffset? FinishedAt = null)
    {
      this.TurnId = TurnId;
      this.UserMessages = UserMessages;
      this.WorkSteps = WorkSteps;
      this.AssistantMessages = AssistantMessages;
      this.NextActions = NextActions;
      this.Artifacts = Artifacts;
      this.Status = Status;
      this.ErrorMessage = ErrorMessage;
      this.StartedAt = StartedAt;
      this.FinishedAt = FinishedAt;
      base..ctor();
    }

    // Token: 0x1700004F RID: 79
    // (get) Token: 0x060000E3 RID: 227 RVA: 0x000D8BC4 File Offset: 0x000D8BC4
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentTranscriptTurn);
      }
    }

    // Token: 0x17000050 RID: 80
    // (get) Token: 0x060000E4 RID: 228 RVA: 0x000D8BD0 File Offset: 0x000D8BD0
    // (set) Token: 0x060000E5 RID: 229 RVA: 0x000D8BD8 File Offset: 0x000D8BD8
    [Nullable(2)]
    public string TurnId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000051 RID: 81
    // (get) Token: 0x060000E6 RID: 230 RVA: 0x000D8BE4 File Offset: 0x000D8BE4
    // (set) Token: 0x060000E7 RID: 231 RVA: 0x000D8BEC File Offset: 0x000D8BEC
    public IReadOnlyList<string> UserMessages { get; set; }

    // Token: 0x17000052 RID: 82
    // (get) Token: 0x060000E8 RID: 232 RVA: 0x000D8BF8 File Offset: 0x000D8BF8
    // (set) Token: 0x060000E9 RID: 233 RVA: 0x000D8C00 File Offset: 0x000D8C00
    public IReadOnlyList<string> WorkSteps { get; set; }

    // Token: 0x17000053 RID: 83
    // (get) Token: 0x060000EA RID: 234 RVA: 0x000D8C0C File Offset: 0x000D8C0C
    // (set) Token: 0x060000EB RID: 235 RVA: 0x000D8C14 File Offset: 0x000D8C14
    public IReadOnlyList<string> AssistantMessages { get; set; }

    // Token: 0x17000054 RID: 84
    // (get) Token: 0x060000EC RID: 236 RVA: 0x000D8C20 File Offset: 0x000D8C20
    // (set) Token: 0x060000ED RID: 237 RVA: 0x000D8C28 File Offset: 0x000D8C28
    public IReadOnlyList<string> NextActions { get; set; }

    // Token: 0x17000055 RID: 85
    // (get) Token: 0x060000EE RID: 238 RVA: 0x000D8C34 File Offset: 0x000D8C34
    // (set) Token: 0x060000EF RID: 239 RVA: 0x000D8C3C File Offset: 0x000D8C3C
    public IReadOnlyList<string> Artifacts { get; set; }

    // Token: 0x17000056 RID: 86
    // (get) Token: 0x060000F0 RID: 240 RVA: 0x000D8C48 File Offset: 0x000D8C48
    // (set) Token: 0x060000F1 RID: 241 RVA: 0x000D8C50 File Offset: 0x000D8C50
    public string Status { get; set; }

    // Token: 0x17000057 RID: 87
    // (get) Token: 0x060000F2 RID: 242 RVA: 0x000D8C5C File Offset: 0x000D8C5C
    // (set) Token: 0x060000F3 RID: 243 RVA: 0x000D8C64 File Offset: 0x000D8C64
    [Nullable(2)]
    public string ErrorMessage
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000058 RID: 88
    // (get) Token: 0x060000F4 RID: 244 RVA: 0x000D8C70 File Offset: 0x000D8C70
    // (set) Token: 0x060000F5 RID: 245 RVA: 0x000D8C78 File Offset: 0x000D8C78
    public DateTimeOffset? StartedAt { get; set; }

    // Token: 0x17000059 RID: 89
    // (get) Token: 0x060000F6 RID: 246 RVA: 0x000D8C84 File Offset: 0x000D8C84
    // (set) Token: 0x060000F7 RID: 247 RVA: 0x000D8C8C File Offset: 0x000D8C8C
    public DateTimeOffset? FinishedAt { get; set; }

    // Token: 0x060000F8 RID: 248 RVA: 0x000D8C98 File Offset: 0x000D8C98
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentTranscriptTurn");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060000F9 RID: 249 RVA: 0x000D8CE4 File Offset: 0x000D8CE4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("TurnId = ");
      builder.Append(this.TurnId);
      builder.Append(", UserMessages = ");
      builder.Append(this.UserMessages);
      builder.Append(", WorkSteps = ");
      builder.Append(this.WorkSteps);
      builder.Append(", AssistantMessages = ");
      builder.Append(this.AssistantMessages);
      builder.Append(", NextActions = ");
      builder.Append(this.NextActions);
      builder.Append(", Artifacts = ");
      builder.Append(this.Artifacts);
      builder.Append(", Status = ");
      builder.Append(this.Status);
      builder.Append(", ErrorMessage = ");
      builder.Append(this.ErrorMessage);
      builder.Append(", StartedAt = ");
      builder.Append(this.StartedAt.ToString());
      builder.Append(", FinishedAt = ");
      builder.Append(this.FinishedAt.ToString());
      return true;
    }

    // Token: 0x060000FA RID: 250 RVA: 0x000D8E10 File Offset: 0x000D8E10
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentTranscriptTurn left, AgentTranscriptTurn right)
    {
      return !(left == right);
    }

    // Token: 0x060000FB RID: 251 RVA: 0x000D8E1C File Offset: 0x000D8E1C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentTranscriptTurn left, AgentTranscriptTurn right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060000FC RID: 252 RVA: 0x000D8E30 File Offset: 0x000D8E30
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TurnId>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<UserMessages>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<WorkSteps>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<AssistantMessages>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<NextActions>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Artifacts>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Status>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ErrorMessage>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<StartedAt>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<FinishedAt>k__BackingField);
    }

    // Token: 0x060000FD RID: 253 RVA: 0x000D8F34 File Offset: 0x000D8F34
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentTranscriptTurn);
    }

    // Token: 0x060000FE RID: 254 RVA: 0x000D8F44 File Offset: 0x000D8F44
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentTranscriptTurn other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<TurnId>k__BackingField, other.<TurnId>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<UserMessages>k__BackingField, other.<UserMessages>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<WorkSteps>k__BackingField, other.<WorkSteps>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<AssistantMessages>k__BackingField, other.<AssistantMessages>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<NextActions>k__BackingField, other.<NextActions>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Artifacts>k__BackingField, other.<Artifacts>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ErrorMessage>k__BackingField, other.<ErrorMessage>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<StartedAt>k__BackingField, other.<StartedAt>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<FinishedAt>k__BackingField, other.<FinishedAt>k__BackingField));
    }

    // Token: 0x06000100 RID: 256 RVA: 0x000D907C File Offset: 0x000D907C
    [CompilerGenerated]
    private AgentTranscriptTurn(AgentTranscriptTurn original)
    {
      this.TurnId = original.<TurnId>k__BackingField;
      this.UserMessages = original.<UserMessages>k__BackingField;
      this.WorkSteps = original.<WorkSteps>k__BackingField;
      this.AssistantMessages = original.<AssistantMessages>k__BackingField;
      this.NextActions = original.<NextActions>k__BackingField;
      this.Artifacts = original.<Artifacts>k__BackingField;
      this.Status = original.<Status>k__BackingField;
      this.ErrorMessage = original.<ErrorMessage>k__BackingField;
      this.StartedAt = original.<StartedAt>k__BackingField;
      this.FinishedAt = original.<FinishedAt>k__BackingField;
    }

    // Token: 0x06000101 RID: 257 RVA: 0x000D9108 File Offset: 0x000D9108
    [CompilerGenerated]
    public void Deconstruct([Nullable(2)] out string TurnId, out IReadOnlyList<string> UserMessages, out IReadOnlyList<string> WorkSteps, out IReadOnlyList<string> AssistantMessages, out IReadOnlyList<string> NextActions, out IReadOnlyList<string> Artifacts, out string Status, [Nullable(2)] out string ErrorMessage, out DateTimeOffset? StartedAt, out DateTimeOffset? FinishedAt)
    {
      TurnId = this.TurnId;
      UserMessages = this.UserMessages;
      WorkSteps = this.WorkSteps;
      AssistantMessages = this.AssistantMessages;
      NextActions = this.NextActions;
      Artifacts = this.Artifacts;
      Status = this.Status;
      ErrorMessage = this.ErrorMessage;
      StartedAt = this.StartedAt;
      FinishedAt = this.FinishedAt;
    }
  }
}
