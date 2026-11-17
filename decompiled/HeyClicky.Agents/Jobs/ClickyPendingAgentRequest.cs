using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x0200004E RID: 78
  [NullableContext(2)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class ClickyPendingAgentRequest : IEquatable<ClickyPendingAgentRequest>
  {
    // Token: 0x170001C5 RID: 453
    // (get) Token: 0x060006E6 RID: 1766 RVA: 0x000F95A4 File Offset: 0x000F95A4
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ClickyPendingAgentRequest);
      }
    }

    // Token: 0x170001C6 RID: 454
    // (get) Token: 0x060006E7 RID: 1767 RVA: 0x000F95B0 File Offset: 0x000F95B0
    // (set) Token: 0x060006E8 RID: 1768 RVA: 0x000F95B8 File Offset: 0x000F95B8
    [Nullable(1)]
    [RequiredMember]
    public string ThreadId
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170001C7 RID: 455
    // (get) Token: 0x060006E9 RID: 1769 RVA: 0x000F95C4 File Offset: 0x000F95C4
    // (set) Token: 0x060006EA RID: 1770 RVA: 0x000F95CC File Offset: 0x000F95CC
    public ClickyPendingAgentRequest.RequestKind Kind { get; set; }

    // Token: 0x170001C8 RID: 456
    // (get) Token: 0x060006EB RID: 1771 RVA: 0x000F95D8 File Offset: 0x000F95D8
    // (set) Token: 0x060006EC RID: 1772 RVA: 0x000F95E0 File Offset: 0x000F95E0
    [Nullable(1)]
    public string RequestText
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170001C9 RID: 457
    // (get) Token: 0x060006ED RID: 1773 RVA: 0x000F95EC File Offset: 0x000F95EC
    // (set) Token: 0x060006EE RID: 1774 RVA: 0x000F95F4 File Offset: 0x000F95F4
    public string LeaseId { get; set; }

    // Token: 0x170001CA RID: 458
    // (get) Token: 0x060006EF RID: 1775 RVA: 0x000F9600 File Offset: 0x000F9600
    // (set) Token: 0x060006F0 RID: 1776 RVA: 0x000F9608 File Offset: 0x000F9608
    public string LeaseTurnId { get; set; }

    // Token: 0x170001CB RID: 459
    // (get) Token: 0x060006F1 RID: 1777 RVA: 0x000F9614 File Offset: 0x000F9614
    // (set) Token: 0x060006F2 RID: 1778 RVA: 0x000F961C File Offset: 0x000F961C
    public string ClickySlug { get; set; }

    // Token: 0x170001CC RID: 460
    // (get) Token: 0x060006F3 RID: 1779 RVA: 0x000F9628 File Offset: 0x000F9628
    // (set) Token: 0x060006F4 RID: 1780 RVA: 0x000F9630 File Offset: 0x000F9630
    public string JobId { get; set; }

    // Token: 0x170001CD RID: 461
    // (get) Token: 0x060006F5 RID: 1781 RVA: 0x000F963C File Offset: 0x000F963C
    // (set) Token: 0x060006F6 RID: 1782 RVA: 0x000F9644 File Offset: 0x000F9644
    public string Title { get; set; }

    // Token: 0x170001CE RID: 462
    // (get) Token: 0x060006F7 RID: 1783 RVA: 0x000F9650 File Offset: 0x000F9650
    // (set) Token: 0x060006F8 RID: 1784 RVA: 0x000F9658 File Offset: 0x000F9658
    [Nullable(1)]
    public string Prompt
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170001CF RID: 463
    // (get) Token: 0x060006F9 RID: 1785 RVA: 0x000F9664 File Offset: 0x000F9664
    // (set) Token: 0x060006FA RID: 1786 RVA: 0x000F966C File Offset: 0x000F966C
    public string FinalSummary { get; set; }

    // Token: 0x170001D0 RID: 464
    // (get) Token: 0x060006FB RID: 1787 RVA: 0x000F9678 File Offset: 0x000F9678
    // (set) Token: 0x060006FC RID: 1788 RVA: 0x000F9680 File Offset: 0x000F9680
    public DateTimeOffset AskedAt { get; set; }

    // Token: 0x060006FD RID: 1789 RVA: 0x000F968C File Offset: 0x000F968C
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyPendingAgentRequest");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060006FE RID: 1790 RVA: 0x000F96D8 File Offset: 0x000F96D8
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ThreadId = ");
      builder.Append(this.ThreadId);
      builder.Append(", Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", RequestText = ");
      builder.Append(this.RequestText);
      builder.Append(", LeaseId = ");
      builder.Append(this.LeaseId);
      builder.Append(", LeaseTurnId = ");
      builder.Append(this.LeaseTurnId);
      builder.Append(", ClickySlug = ");
      builder.Append(this.ClickySlug);
      builder.Append(", JobId = ");
      builder.Append(this.JobId);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", Prompt = ");
      builder.Append(this.Prompt);
      builder.Append(", FinalSummary = ");
      builder.Append(this.FinalSummary);
      builder.Append(", AskedAt = ");
      builder.Append(this.AskedAt.ToString());
      return true;
    }

    // Token: 0x060006FF RID: 1791 RVA: 0x000F981C File Offset: 0x000F981C
    [CompilerGenerated]
    public static bool operator !=(ClickyPendingAgentRequest left, ClickyPendingAgentRequest right)
    {
      return !(left == right);
    }

    // Token: 0x06000700 RID: 1792 RVA: 0x000F9828 File Offset: 0x000F9828
    [CompilerGenerated]
    public static bool operator ==(ClickyPendingAgentRequest left, ClickyPendingAgentRequest right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000701 RID: 1793 RVA: 0x000F983C File Offset: 0x000F983C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<ClickyPendingAgentRequest.RequestKind>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RequestText>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LeaseId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LeaseTurnId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<JobId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Prompt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FinalSummary>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<AskedAt>k__BackingField);
    }

    // Token: 0x06000702 RID: 1794 RVA: 0x000F9958 File Offset: 0x000F9958
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyPendingAgentRequest);
    }

    // Token: 0x06000703 RID: 1795 RVA: 0x000F9968 File Offset: 0x000F9968
    [CompilerGenerated]
    public bool Equals(ClickyPendingAgentRequest other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<ClickyPendingAgentRequest.RequestKind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RequestText>k__BackingField, other.<RequestText>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LeaseId>k__BackingField, other.<LeaseId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LeaseTurnId>k__BackingField, other.<LeaseTurnId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<JobId>k__BackingField, other.<JobId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Prompt>k__BackingField, other.<Prompt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FinalSummary>k__BackingField, other.<FinalSummary>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<AskedAt>k__BackingField, other.<AskedAt>k__BackingField));
    }

    // Token: 0x06000705 RID: 1797 RVA: 0x000F9ABC File Offset: 0x000F9ABC
    [CompilerGenerated]
    [SetsRequiredMembers]
    private ClickyPendingAgentRequest([Nullable(1)] ClickyPendingAgentRequest original)
    {
      this.ThreadId = original.<ThreadId>k__BackingField;
      this.Kind = original.<Kind>k__BackingField;
      this.RequestText = original.<RequestText>k__BackingField;
      this.LeaseId = original.<LeaseId>k__BackingField;
      this.LeaseTurnId = original.<LeaseTurnId>k__BackingField;
      this.ClickySlug = original.<ClickySlug>k__BackingField;
      this.JobId = original.<JobId>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.Prompt = original.<Prompt>k__BackingField;
      this.FinalSummary = original.<FinalSummary>k__BackingField;
      this.AskedAt = original.<AskedAt>k__BackingField;
    }

    // Token: 0x06000706 RID: 1798 RVA: 0x000F9B54 File Offset: 0x000F9B54
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public ClickyPendingAgentRequest()
    {
      this.RequestText = "";
      this.Prompt = "";
      base..ctor();
    }

    // Token: 0x02000186 RID: 390
    [NullableContext(0)]
    [JsonConverter(typeof(JsonStringEnumConverter<ClickyPendingAgentRequest.RequestKind>))]
    public enum RequestKind
    {
      // Token: 0x04000788 RID: 1928
      [JsonStringEnumMemberName("computerUse")]
      ComputerUse,
      // Token: 0x04000789 RID: 1929
      [JsonStringEnumMemberName("extraUsage")]
      ExtraUsage
    }
  }
}
