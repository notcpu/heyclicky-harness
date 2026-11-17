using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Onboarding
{
  // Token: 0x020000EB RID: 235
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class HostedVideoFailure : IEquatable<HostedVideoFailure>
  {
    // Token: 0x06000F07 RID: 3847 RVA: 0x0038D728 File Offset: 0x0038D728
    public HostedVideoFailure(string VideoKind, string Phase, string Reason, string AssetUrl, string ErrorDomain, int ErrorCode, string ErrorMessage, string PlayerStatus = "none", string ItemStatus = "none", string TimeControlStatus = "none", int? HttpStatus = null)
    {
      this.VideoKind = VideoKind;
      this.Phase = Phase;
      this.Reason = Reason;
      this.AssetUrl = AssetUrl;
      this.ErrorDomain = ErrorDomain;
      this.ErrorCode = ErrorCode;
      this.ErrorMessage = ErrorMessage;
      this.PlayerStatus = PlayerStatus;
      this.ItemStatus = ItemStatus;
      this.TimeControlStatus = TimeControlStatus;
      this.HttpStatus = HttpStatus;
      base..ctor();
    }

    // Token: 0x1700035E RID: 862
    // (get) Token: 0x06000F08 RID: 3848 RVA: 0x0038D790 File Offset: 0x0038D790
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(HostedVideoFailure);
      }
    }

    // Token: 0x1700035F RID: 863
    // (get) Token: 0x06000F09 RID: 3849 RVA: 0x0038D79C File Offset: 0x0038D79C
    // (set) Token: 0x06000F0A RID: 3850 RVA: 0x0038D7A4 File Offset: 0x0038D7A4
    public string VideoKind { get; set; }

    // Token: 0x17000360 RID: 864
    // (get) Token: 0x06000F0B RID: 3851 RVA: 0x0038D7B0 File Offset: 0x0038D7B0
    // (set) Token: 0x06000F0C RID: 3852 RVA: 0x0038D7B8 File Offset: 0x0038D7B8
    public string Phase { get; set; }

    // Token: 0x17000361 RID: 865
    // (get) Token: 0x06000F0D RID: 3853 RVA: 0x0038D7C4 File Offset: 0x0038D7C4
    // (set) Token: 0x06000F0E RID: 3854 RVA: 0x0038D7CC File Offset: 0x0038D7CC
    public string Reason { get; set; }

    // Token: 0x17000362 RID: 866
    // (get) Token: 0x06000F0F RID: 3855 RVA: 0x0038D7D8 File Offset: 0x0038D7D8
    // (set) Token: 0x06000F10 RID: 3856 RVA: 0x0038D7E0 File Offset: 0x0038D7E0
    public string AssetUrl { get; set; }

    // Token: 0x17000363 RID: 867
    // (get) Token: 0x06000F11 RID: 3857 RVA: 0x0038D7EC File Offset: 0x0038D7EC
    // (set) Token: 0x06000F12 RID: 3858 RVA: 0x0038D7F4 File Offset: 0x0038D7F4
    public string ErrorDomain { get; set; }

    // Token: 0x17000364 RID: 868
    // (get) Token: 0x06000F13 RID: 3859 RVA: 0x0038D800 File Offset: 0x0038D800
    // (set) Token: 0x06000F14 RID: 3860 RVA: 0x0038D808 File Offset: 0x0038D808
    public int ErrorCode { get; set; }

    // Token: 0x17000365 RID: 869
    // (get) Token: 0x06000F15 RID: 3861 RVA: 0x0038D814 File Offset: 0x0038D814
    // (set) Token: 0x06000F16 RID: 3862 RVA: 0x0038D81C File Offset: 0x0038D81C
    public string ErrorMessage { get; set; }

    // Token: 0x17000366 RID: 870
    // (get) Token: 0x06000F17 RID: 3863 RVA: 0x0038D828 File Offset: 0x0038D828
    // (set) Token: 0x06000F18 RID: 3864 RVA: 0x0038D830 File Offset: 0x0038D830
    public string PlayerStatus { get; set; }

    // Token: 0x17000367 RID: 871
    // (get) Token: 0x06000F19 RID: 3865 RVA: 0x0038D83C File Offset: 0x0038D83C
    // (set) Token: 0x06000F1A RID: 3866 RVA: 0x0038D844 File Offset: 0x0038D844
    public string ItemStatus { get; set; }

    // Token: 0x17000368 RID: 872
    // (get) Token: 0x06000F1B RID: 3867 RVA: 0x0038D850 File Offset: 0x0038D850
    // (set) Token: 0x06000F1C RID: 3868 RVA: 0x0038D858 File Offset: 0x0038D858
    public string TimeControlStatus { get; set; }

    // Token: 0x17000369 RID: 873
    // (get) Token: 0x06000F1D RID: 3869 RVA: 0x0038D864 File Offset: 0x0038D864
    // (set) Token: 0x06000F1E RID: 3870 RVA: 0x0038D86C File Offset: 0x0038D86C
    public int? HttpStatus { get; set; }

    // Token: 0x06000F1F RID: 3871 RVA: 0x0038D878 File Offset: 0x0038D878
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("HostedVideoFailure");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000F20 RID: 3872 RVA: 0x0038D8C4 File Offset: 0x0038D8C4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("VideoKind = ");
      builder.Append(this.VideoKind);
      builder.Append(", Phase = ");
      builder.Append(this.Phase);
      builder.Append(", Reason = ");
      builder.Append(this.Reason);
      builder.Append(", AssetUrl = ");
      builder.Append(this.AssetUrl);
      builder.Append(", ErrorDomain = ");
      builder.Append(this.ErrorDomain);
      builder.Append(", ErrorCode = ");
      builder.Append(this.ErrorCode.ToString());
      builder.Append(", ErrorMessage = ");
      builder.Append(this.ErrorMessage);
      builder.Append(", PlayerStatus = ");
      builder.Append(this.PlayerStatus);
      builder.Append(", ItemStatus = ");
      builder.Append(this.ItemStatus);
      builder.Append(", TimeControlStatus = ");
      builder.Append(this.TimeControlStatus);
      builder.Append(", HttpStatus = ");
      builder.Append(this.HttpStatus.ToString());
      return true;
    }

    // Token: 0x06000F21 RID: 3873 RVA: 0x0038DA08 File Offset: 0x0038DA08
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(HostedVideoFailure left, HostedVideoFailure right)
    {
      return !(left == right);
    }

    // Token: 0x06000F22 RID: 3874 RVA: 0x0038DA14 File Offset: 0x0038DA14
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(HostedVideoFailure left, HostedVideoFailure right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000F23 RID: 3875 RVA: 0x0038DA28 File Offset: 0x0038DA28
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<VideoKind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Phase>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Reason>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AssetUrl>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ErrorDomain>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ErrorCode>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ErrorMessage>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PlayerStatus>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ItemStatus>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TimeControlStatus>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<HttpStatus>k__BackingField);
    }

    // Token: 0x06000F24 RID: 3876 RVA: 0x0038DB44 File Offset: 0x0038DB44
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as HostedVideoFailure);
    }

    // Token: 0x06000F25 RID: 3877 RVA: 0x0038DB54 File Offset: 0x0038DB54
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(HostedVideoFailure other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<VideoKind>k__BackingField, other.<VideoKind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Phase>k__BackingField, other.<Phase>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Reason>k__BackingField, other.<Reason>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AssetUrl>k__BackingField, other.<AssetUrl>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ErrorDomain>k__BackingField, other.<ErrorDomain>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ErrorCode>k__BackingField, other.<ErrorCode>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ErrorMessage>k__BackingField, other.<ErrorMessage>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<PlayerStatus>k__BackingField, other.<PlayerStatus>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ItemStatus>k__BackingField, other.<ItemStatus>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TimeControlStatus>k__BackingField, other.<TimeControlStatus>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<HttpStatus>k__BackingField, other.<HttpStatus>k__BackingField));
    }

    // Token: 0x06000F27 RID: 3879 RVA: 0x0038DCA8 File Offset: 0x0038DCA8
    [CompilerGenerated]
    private HostedVideoFailure(HostedVideoFailure original)
    {
      this.VideoKind = original.<VideoKind>k__BackingField;
      this.Phase = original.<Phase>k__BackingField;
      this.Reason = original.<Reason>k__BackingField;
      this.AssetUrl = original.<AssetUrl>k__BackingField;
      this.ErrorDomain = original.<ErrorDomain>k__BackingField;
      this.ErrorCode = original.<ErrorCode>k__BackingField;
      this.ErrorMessage = original.<ErrorMessage>k__BackingField;
      this.PlayerStatus = original.<PlayerStatus>k__BackingField;
      this.ItemStatus = original.<ItemStatus>k__BackingField;
      this.TimeControlStatus = original.<TimeControlStatus>k__BackingField;
      this.HttpStatus = original.<HttpStatus>k__BackingField;
    }

    // Token: 0x06000F28 RID: 3880 RVA: 0x0038DD40 File Offset: 0x0038DD40
    [CompilerGenerated]
    public void Deconstruct(out string VideoKind, out string Phase, out string Reason, out string AssetUrl, out string ErrorDomain, out int ErrorCode, out string ErrorMessage, out string PlayerStatus, out string ItemStatus, out string TimeControlStatus, out int? HttpStatus)
    {
      VideoKind = this.VideoKind;
      Phase = this.Phase;
      Reason = this.Reason;
      AssetUrl = this.AssetUrl;
      ErrorDomain = this.ErrorDomain;
      ErrorCode = this.ErrorCode;
      ErrorMessage = this.ErrorMessage;
      PlayerStatus = this.PlayerStatus;
      ItemStatus = this.ItemStatus;
      TimeControlStatus = this.TimeControlStatus;
      HttpStatus = this.HttpStatus;
    }
  }
}
