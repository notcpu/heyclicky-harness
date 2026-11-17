using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x0200004D RID: 77
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ErrorReport : IEquatable<ErrorReport>
  {
    // Token: 0x06000353 RID: 851 RVA: 0x003534D0 File Offset: 0x003534D0
    public ErrorReport(string Stage, string ErrorKind, string ErrorMessage, string UserFacingMessage, string AppVersion, int ErrorCode, [Nullable(2)] string ErrorDomain = null, [Nullable(2)] string SupabaseUserId = null, [Nullable(2)] string UserEmail = null)
    {
      this.Stage = Stage;
      this.ErrorKind = ErrorKind;
      this.ErrorMessage = ErrorMessage;
      this.UserFacingMessage = UserFacingMessage;
      this.AppVersion = AppVersion;
      this.ErrorCode = ErrorCode;
      this.ErrorDomain = ErrorDomain;
      this.SupabaseUserId = SupabaseUserId;
      this.UserEmail = UserEmail;
      base..ctor();
    }

    // Token: 0x170000B3 RID: 179
    // (get) Token: 0x06000354 RID: 852 RVA: 0x00353528 File Offset: 0x00353528
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ErrorReport);
      }
    }

    // Token: 0x170000B4 RID: 180
    // (get) Token: 0x06000355 RID: 853 RVA: 0x00353534 File Offset: 0x00353534
    // (set) Token: 0x06000356 RID: 854 RVA: 0x0035353C File Offset: 0x0035353C
    public string Stage { get; set; }

    // Token: 0x170000B5 RID: 181
    // (get) Token: 0x06000357 RID: 855 RVA: 0x00353548 File Offset: 0x00353548
    // (set) Token: 0x06000358 RID: 856 RVA: 0x00353550 File Offset: 0x00353550
    public string ErrorKind { get; set; }

    // Token: 0x170000B6 RID: 182
    // (get) Token: 0x06000359 RID: 857 RVA: 0x0035355C File Offset: 0x0035355C
    // (set) Token: 0x0600035A RID: 858 RVA: 0x00353564 File Offset: 0x00353564
    public string ErrorMessage { get; set; }

    // Token: 0x170000B7 RID: 183
    // (get) Token: 0x0600035B RID: 859 RVA: 0x00353570 File Offset: 0x00353570
    // (set) Token: 0x0600035C RID: 860 RVA: 0x00353578 File Offset: 0x00353578
    public string UserFacingMessage { get; set; }

    // Token: 0x170000B8 RID: 184
    // (get) Token: 0x0600035D RID: 861 RVA: 0x00353584 File Offset: 0x00353584
    // (set) Token: 0x0600035E RID: 862 RVA: 0x0035358C File Offset: 0x0035358C
    public string AppVersion { get; set; }

    // Token: 0x170000B9 RID: 185
    // (get) Token: 0x0600035F RID: 863 RVA: 0x00353598 File Offset: 0x00353598
    // (set) Token: 0x06000360 RID: 864 RVA: 0x003535A0 File Offset: 0x003535A0
    public int ErrorCode { get; set; }

    // Token: 0x170000BA RID: 186
    // (get) Token: 0x06000361 RID: 865 RVA: 0x003535AC File Offset: 0x003535AC
    // (set) Token: 0x06000362 RID: 866 RVA: 0x003535B4 File Offset: 0x003535B4
    [Nullable(2)]
    public string ErrorDomain
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170000BB RID: 187
    // (get) Token: 0x06000363 RID: 867 RVA: 0x003535C0 File Offset: 0x003535C0
    // (set) Token: 0x06000364 RID: 868 RVA: 0x003535C8 File Offset: 0x003535C8
    [Nullable(2)]
    public string SupabaseUserId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170000BC RID: 188
    // (get) Token: 0x06000365 RID: 869 RVA: 0x003535D4 File Offset: 0x003535D4
    // (set) Token: 0x06000366 RID: 870 RVA: 0x003535DC File Offset: 0x003535DC
    [Nullable(2)]
    public string UserEmail
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06000367 RID: 871 RVA: 0x003535E8 File Offset: 0x003535E8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ErrorReport");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000368 RID: 872 RVA: 0x00353634 File Offset: 0x00353634
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Stage = ");
      builder.Append(this.Stage);
      builder.Append(", ErrorKind = ");
      builder.Append(this.ErrorKind);
      builder.Append(", ErrorMessage = ");
      builder.Append(this.ErrorMessage);
      builder.Append(", UserFacingMessage = ");
      builder.Append(this.UserFacingMessage);
      builder.Append(", AppVersion = ");
      builder.Append(this.AppVersion);
      builder.Append(", ErrorCode = ");
      builder.Append(this.ErrorCode.ToString());
      builder.Append(", ErrorDomain = ");
      builder.Append(this.ErrorDomain);
      builder.Append(", SupabaseUserId = ");
      builder.Append(this.SupabaseUserId);
      builder.Append(", UserEmail = ");
      builder.Append(this.UserEmail);
      return true;
    }

    // Token: 0x06000369 RID: 873 RVA: 0x00353738 File Offset: 0x00353738
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ErrorReport left, ErrorReport right)
    {
      return !(left == right);
    }

    // Token: 0x0600036A RID: 874 RVA: 0x00353744 File Offset: 0x00353744
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ErrorReport left, ErrorReport right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600036B RID: 875 RVA: 0x00353758 File Offset: 0x00353758
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Stage>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ErrorKind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ErrorMessage>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UserFacingMessage>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AppVersion>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ErrorCode>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ErrorDomain>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SupabaseUserId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UserEmail>k__BackingField);
    }

    // Token: 0x0600036C RID: 876 RVA: 0x00353844 File Offset: 0x00353844
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ErrorReport);
    }

    // Token: 0x0600036D RID: 877 RVA: 0x00353854 File Offset: 0x00353854
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ErrorReport other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Stage>k__BackingField, other.<Stage>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ErrorKind>k__BackingField, other.<ErrorKind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ErrorMessage>k__BackingField, other.<ErrorMessage>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UserFacingMessage>k__BackingField, other.<UserFacingMessage>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AppVersion>k__BackingField, other.<AppVersion>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ErrorCode>k__BackingField, other.<ErrorCode>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ErrorDomain>k__BackingField, other.<ErrorDomain>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SupabaseUserId>k__BackingField, other.<SupabaseUserId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UserEmail>k__BackingField, other.<UserEmail>k__BackingField));
    }

    // Token: 0x0600036F RID: 879 RVA: 0x00353970 File Offset: 0x00353970
    [CompilerGenerated]
    private ErrorReport(ErrorReport original)
    {
      this.Stage = original.<Stage>k__BackingField;
      this.ErrorKind = original.<ErrorKind>k__BackingField;
      this.ErrorMessage = original.<ErrorMessage>k__BackingField;
      this.UserFacingMessage = original.<UserFacingMessage>k__BackingField;
      this.AppVersion = original.<AppVersion>k__BackingField;
      this.ErrorCode = original.<ErrorCode>k__BackingField;
      this.ErrorDomain = original.<ErrorDomain>k__BackingField;
      this.SupabaseUserId = original.<SupabaseUserId>k__BackingField;
      this.UserEmail = original.<UserEmail>k__BackingField;
    }

    // Token: 0x06000370 RID: 880 RVA: 0x003539F0 File Offset: 0x003539F0
    [CompilerGenerated]
    public void Deconstruct(out string Stage, out string ErrorKind, out string ErrorMessage, out string UserFacingMessage, out string AppVersion, out int ErrorCode, [Nullable(2)] out string ErrorDomain, [Nullable(2)] out string SupabaseUserId, [Nullable(2)] out string UserEmail)
    {
      Stage = this.Stage;
      ErrorKind = this.ErrorKind;
      ErrorMessage = this.ErrorMessage;
      UserFacingMessage = this.UserFacingMessage;
      AppVersion = this.AppVersion;
      ErrorCode = this.ErrorCode;
      ErrorDomain = this.ErrorDomain;
      SupabaseUserId = this.SupabaseUserId;
      UserEmail = this.UserEmail;
    }
  }
}
