using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000065 RID: 101
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class DeepgramTokenResponse : IEquatable<DeepgramTokenResponse>
  {
    // Token: 0x0600053F RID: 1343 RVA: 0x00357E34 File Offset: 0x00357E34
    public DeepgramTokenResponse([Nullable(1)] string AccessToken, string Model, string Receipt)
    {
      this.AccessToken = AccessToken;
      this.Model = Model;
      this.Receipt = Receipt;
      base..ctor();
    }

    // Token: 0x17000131 RID: 305
    // (get) Token: 0x06000540 RID: 1344 RVA: 0x00357E54 File Offset: 0x00357E54
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(DeepgramTokenResponse);
      }
    }

    // Token: 0x17000132 RID: 306
    // (get) Token: 0x06000541 RID: 1345 RVA: 0x00357E60 File Offset: 0x00357E60
    // (set) Token: 0x06000542 RID: 1346 RVA: 0x00357E68 File Offset: 0x00357E68
    [Nullable(1)]
    public string AccessToken
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000133 RID: 307
    // (get) Token: 0x06000543 RID: 1347 RVA: 0x00357E74 File Offset: 0x00357E74
    // (set) Token: 0x06000544 RID: 1348 RVA: 0x00357E7C File Offset: 0x00357E7C
    public string Model { get; set; }

    // Token: 0x17000134 RID: 308
    // (get) Token: 0x06000545 RID: 1349 RVA: 0x00357E88 File Offset: 0x00357E88
    // (set) Token: 0x06000546 RID: 1350 RVA: 0x00357E90 File Offset: 0x00357E90
    public string Receipt { get; set; }

    // Token: 0x06000547 RID: 1351 RVA: 0x00357E9C File Offset: 0x00357E9C
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DeepgramTokenResponse");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000548 RID: 1352 RVA: 0x00357EE8 File Offset: 0x00357EE8
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("AccessToken = ");
      builder.Append(this.AccessToken);
      builder.Append(", Model = ");
      builder.Append(this.Model);
      builder.Append(", Receipt = ");
      builder.Append(this.Receipt);
      return true;
    }

    // Token: 0x06000549 RID: 1353 RVA: 0x00357F48 File Offset: 0x00357F48
    [CompilerGenerated]
    public static bool operator !=(DeepgramTokenResponse left, DeepgramTokenResponse right)
    {
      return !(left == right);
    }

    // Token: 0x0600054A RID: 1354 RVA: 0x00357F54 File Offset: 0x00357F54
    [CompilerGenerated]
    public static bool operator ==(DeepgramTokenResponse left, DeepgramTokenResponse right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600054B RID: 1355 RVA: 0x00357F68 File Offset: 0x00357F68
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AccessToken>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Model>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Receipt>k__BackingField);
    }

    // Token: 0x0600054C RID: 1356 RVA: 0x00357FCC File Offset: 0x00357FCC
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as DeepgramTokenResponse);
    }

    // Token: 0x0600054D RID: 1357 RVA: 0x00357FDC File Offset: 0x00357FDC
    [CompilerGenerated]
    public bool Equals(DeepgramTokenResponse other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<AccessToken>k__BackingField, other.<AccessToken>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Model>k__BackingField, other.<Model>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Receipt>k__BackingField, other.<Receipt>k__BackingField));
    }

    // Token: 0x0600054F RID: 1359 RVA: 0x00358058 File Offset: 0x00358058
    [CompilerGenerated]
    private DeepgramTokenResponse([Nullable(1)] DeepgramTokenResponse original)
    {
      this.AccessToken = original.<AccessToken>k__BackingField;
      this.Model = original.<Model>k__BackingField;
      this.Receipt = original.<Receipt>k__BackingField;
    }

    // Token: 0x06000550 RID: 1360 RVA: 0x00358084 File Offset: 0x00358084
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out string AccessToken, out string Model, out string Receipt)
    {
      AccessToken = this.AccessToken;
      Model = this.Model;
      Receipt = this.Receipt;
    }
  }
}
