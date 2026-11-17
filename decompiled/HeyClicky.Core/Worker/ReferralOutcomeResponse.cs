using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000062 RID: 98
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ReferralOutcomeResponse : IEquatable<ReferralOutcomeResponse>
  {
    // Token: 0x060004F1 RID: 1265 RVA: 0x003570C0 File Offset: 0x003570C0
    public ReferralOutcomeResponse([Nullable(1)] string Outcome, string Message, double? DiscountPercent, string Code, string ShareUrl, string ShareDisplayText)
    {
      this.Outcome = Outcome;
      this.Message = Message;
      this.DiscountPercent = DiscountPercent;
      this.Code = Code;
      this.ShareUrl = ShareUrl;
      this.ShareDisplayText = ShareDisplayText;
      base..ctor();
    }

    // Token: 0x17000119 RID: 281
    // (get) Token: 0x060004F2 RID: 1266 RVA: 0x003570F8 File Offset: 0x003570F8
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ReferralOutcomeResponse);
      }
    }

    // Token: 0x1700011A RID: 282
    // (get) Token: 0x060004F3 RID: 1267 RVA: 0x00357104 File Offset: 0x00357104
    // (set) Token: 0x060004F4 RID: 1268 RVA: 0x0035710C File Offset: 0x0035710C
    [Nullable(1)]
    public string Outcome
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x1700011B RID: 283
    // (get) Token: 0x060004F5 RID: 1269 RVA: 0x00357118 File Offset: 0x00357118
    // (set) Token: 0x060004F6 RID: 1270 RVA: 0x00357120 File Offset: 0x00357120
    public string Message { get; set; }

    // Token: 0x1700011C RID: 284
    // (get) Token: 0x060004F7 RID: 1271 RVA: 0x0035712C File Offset: 0x0035712C
    // (set) Token: 0x060004F8 RID: 1272 RVA: 0x00357134 File Offset: 0x00357134
    public double? DiscountPercent { get; set; }

    // Token: 0x1700011D RID: 285
    // (get) Token: 0x060004F9 RID: 1273 RVA: 0x00357140 File Offset: 0x00357140
    // (set) Token: 0x060004FA RID: 1274 RVA: 0x00357148 File Offset: 0x00357148
    public string Code { get; set; }

    // Token: 0x1700011E RID: 286
    // (get) Token: 0x060004FB RID: 1275 RVA: 0x00357154 File Offset: 0x00357154
    // (set) Token: 0x060004FC RID: 1276 RVA: 0x0035715C File Offset: 0x0035715C
    public string ShareUrl { get; set; }

    // Token: 0x1700011F RID: 287
    // (get) Token: 0x060004FD RID: 1277 RVA: 0x00357168 File Offset: 0x00357168
    // (set) Token: 0x060004FE RID: 1278 RVA: 0x00357170 File Offset: 0x00357170
    public string ShareDisplayText { get; set; }

    // Token: 0x060004FF RID: 1279 RVA: 0x0035717C File Offset: 0x0035717C
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ReferralOutcomeResponse");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000500 RID: 1280 RVA: 0x003571C8 File Offset: 0x003571C8
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Outcome = ");
      builder.Append(this.Outcome);
      builder.Append(", Message = ");
      builder.Append(this.Message);
      builder.Append(", DiscountPercent = ");
      builder.Append(this.DiscountPercent.ToString());
      builder.Append(", Code = ");
      builder.Append(this.Code);
      builder.Append(", ShareUrl = ");
      builder.Append(this.ShareUrl);
      builder.Append(", ShareDisplayText = ");
      builder.Append(this.ShareDisplayText);
      return true;
    }

    // Token: 0x06000501 RID: 1281 RVA: 0x00357280 File Offset: 0x00357280
    [CompilerGenerated]
    public static bool operator !=(ReferralOutcomeResponse left, ReferralOutcomeResponse right)
    {
      return !(left == right);
    }

    // Token: 0x06000502 RID: 1282 RVA: 0x0035728C File Offset: 0x0035728C
    [CompilerGenerated]
    public static bool operator ==(ReferralOutcomeResponse left, ReferralOutcomeResponse right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000503 RID: 1283 RVA: 0x003572A0 File Offset: 0x003572A0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Outcome>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Message>k__BackingField)) * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(this.<DiscountPercent>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Code>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ShareUrl>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ShareDisplayText>k__BackingField);
    }

    // Token: 0x06000504 RID: 1284 RVA: 0x00357348 File Offset: 0x00357348
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ReferralOutcomeResponse);
    }

    // Token: 0x06000505 RID: 1285 RVA: 0x00357358 File Offset: 0x00357358
    [CompilerGenerated]
    public bool Equals(ReferralOutcomeResponse other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Outcome>k__BackingField, other.<Outcome>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Message>k__BackingField, other.<Message>k__BackingField) && EqualityComparer<double?>.Default.Equals(this.<DiscountPercent>k__BackingField, other.<DiscountPercent>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Code>k__BackingField, other.<Code>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ShareUrl>k__BackingField, other.<ShareUrl>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ShareDisplayText>k__BackingField, other.<ShareDisplayText>k__BackingField));
    }

    // Token: 0x06000507 RID: 1287 RVA: 0x00357424 File Offset: 0x00357424
    [CompilerGenerated]
    private ReferralOutcomeResponse([Nullable(1)] ReferralOutcomeResponse original)
    {
      this.Outcome = original.<Outcome>k__BackingField;
      this.Message = original.<Message>k__BackingField;
      this.DiscountPercent = original.<DiscountPercent>k__BackingField;
      this.Code = original.<Code>k__BackingField;
      this.ShareUrl = original.<ShareUrl>k__BackingField;
      this.ShareDisplayText = original.<ShareDisplayText>k__BackingField;
    }

    // Token: 0x06000508 RID: 1288 RVA: 0x00357480 File Offset: 0x00357480
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out string Outcome, out string Message, out double? DiscountPercent, out string Code, out string ShareUrl, out string ShareDisplayText)
    {
      Outcome = this.Outcome;
      Message = this.Message;
      DiscountPercent = this.DiscountPercent;
      Code = this.Code;
      ShareUrl = this.ShareUrl;
      ShareDisplayText = this.ShareDisplayText;
    }
  }
}
