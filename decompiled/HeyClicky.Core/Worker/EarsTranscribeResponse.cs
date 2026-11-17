using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000067 RID: 103
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class EarsTranscribeResponse : IEquatable<EarsTranscribeResponse>
  {
    // Token: 0x06000561 RID: 1377 RVA: 0x0035827C File Offset: 0x0035827C
    public EarsTranscribeResponse(string Text)
    {
      this.Text = Text;
      base..ctor();
    }

    // Token: 0x17000138 RID: 312
    // (get) Token: 0x06000562 RID: 1378 RVA: 0x0035828C File Offset: 0x0035828C
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(EarsTranscribeResponse);
      }
    }

    // Token: 0x17000139 RID: 313
    // (get) Token: 0x06000563 RID: 1379 RVA: 0x00358298 File Offset: 0x00358298
    // (set) Token: 0x06000564 RID: 1380 RVA: 0x003582A0 File Offset: 0x003582A0
    public string Text { get; set; }

    // Token: 0x06000565 RID: 1381 RVA: 0x003582AC File Offset: 0x003582AC
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("EarsTranscribeResponse");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000566 RID: 1382 RVA: 0x003582F8 File Offset: 0x003582F8
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Text = ");
      builder.Append(this.Text);
      return true;
    }

    // Token: 0x06000567 RID: 1383 RVA: 0x0035831C File Offset: 0x0035831C
    [CompilerGenerated]
    public static bool operator !=(EarsTranscribeResponse left, EarsTranscribeResponse right)
    {
      return !(left == right);
    }

    // Token: 0x06000568 RID: 1384 RVA: 0x00358328 File Offset: 0x00358328
    [CompilerGenerated]
    public static bool operator ==(EarsTranscribeResponse left, EarsTranscribeResponse right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000569 RID: 1385 RVA: 0x0035833C File Offset: 0x0035833C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField);
    }

    // Token: 0x0600056A RID: 1386 RVA: 0x00358368 File Offset: 0x00358368
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as EarsTranscribeResponse);
    }

    // Token: 0x0600056B RID: 1387 RVA: 0x00358378 File Offset: 0x00358378
    [CompilerGenerated]
    public bool Equals(EarsTranscribeResponse other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField));
    }

    // Token: 0x0600056D RID: 1389 RVA: 0x003583B8 File Offset: 0x003583B8
    [CompilerGenerated]
    private EarsTranscribeResponse([Nullable(1)] EarsTranscribeResponse original)
    {
      this.Text = original.<Text>k__BackingField;
    }

    // Token: 0x0600056E RID: 1390 RVA: 0x003583CC File Offset: 0x003583CC
    [CompilerGenerated]
    public void Deconstruct(out string Text)
    {
      Text = this.Text;
    }
  }
}
