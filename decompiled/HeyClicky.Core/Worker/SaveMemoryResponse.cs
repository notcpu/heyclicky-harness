using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x0200004C RID: 76
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class SaveMemoryResponse : IEquatable<SaveMemoryResponse>
  {
    // Token: 0x06000343 RID: 835 RVA: 0x003532DC File Offset: 0x003532DC
    public SaveMemoryResponse(bool Saved, string Error)
    {
      this.Saved = Saved;
      this.Error = Error;
      base..ctor();
    }

    // Token: 0x170000B0 RID: 176
    // (get) Token: 0x06000344 RID: 836 RVA: 0x003532F4 File Offset: 0x003532F4
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(SaveMemoryResponse);
      }
    }

    // Token: 0x170000B1 RID: 177
    // (get) Token: 0x06000345 RID: 837 RVA: 0x00353300 File Offset: 0x00353300
    // (set) Token: 0x06000346 RID: 838 RVA: 0x00353308 File Offset: 0x00353308
    public bool Saved { get; set; }

    // Token: 0x170000B2 RID: 178
    // (get) Token: 0x06000347 RID: 839 RVA: 0x00353314 File Offset: 0x00353314
    // (set) Token: 0x06000348 RID: 840 RVA: 0x0035331C File Offset: 0x0035331C
    public string Error { get; set; }

    // Token: 0x06000349 RID: 841 RVA: 0x00353328 File Offset: 0x00353328
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("SaveMemoryResponse");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600034A RID: 842 RVA: 0x00353374 File Offset: 0x00353374
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Saved = ");
      builder.Append(this.Saved.ToString());
      builder.Append(", Error = ");
      builder.Append(this.Error);
      return true;
    }

    // Token: 0x0600034B RID: 843 RVA: 0x003533C8 File Offset: 0x003533C8
    [CompilerGenerated]
    public static bool operator !=(SaveMemoryResponse left, SaveMemoryResponse right)
    {
      return !(left == right);
    }

    // Token: 0x0600034C RID: 844 RVA: 0x003533D4 File Offset: 0x003533D4
    [CompilerGenerated]
    public static bool operator ==(SaveMemoryResponse left, SaveMemoryResponse right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600034D RID: 845 RVA: 0x003533E8 File Offset: 0x003533E8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Saved>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Error>k__BackingField);
    }

    // Token: 0x0600034E RID: 846 RVA: 0x00353428 File Offset: 0x00353428
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as SaveMemoryResponse);
    }

    // Token: 0x0600034F RID: 847 RVA: 0x00353438 File Offset: 0x00353438
    [CompilerGenerated]
    public bool Equals(SaveMemoryResponse other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<bool>.Default.Equals(this.<Saved>k__BackingField, other.<Saved>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Error>k__BackingField, other.<Error>k__BackingField));
    }

    // Token: 0x06000351 RID: 849 RVA: 0x0035349C File Offset: 0x0035349C
    [CompilerGenerated]
    private SaveMemoryResponse([Nullable(1)] SaveMemoryResponse original)
    {
      this.Saved = original.<Saved>k__BackingField;
      this.Error = original.<Error>k__BackingField;
    }

    // Token: 0x06000352 RID: 850 RVA: 0x003534BC File Offset: 0x003534BC
    [CompilerGenerated]
    public void Deconstruct(out bool Saved, out string Error)
    {
      Saved = this.Saved;
      Error = this.Error;
    }
  }
}
