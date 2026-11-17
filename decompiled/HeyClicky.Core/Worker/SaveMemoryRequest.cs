using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x0200004B RID: 75
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SaveMemoryRequest : IEquatable<SaveMemoryRequest>
  {
    // Token: 0x06000335 RID: 821 RVA: 0x00353180 File Offset: 0x00353180
    public SaveMemoryRequest(string Memory)
    {
      this.Memory = Memory;
      base..ctor();
    }

    // Token: 0x170000AE RID: 174
    // (get) Token: 0x06000336 RID: 822 RVA: 0x00353190 File Offset: 0x00353190
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(SaveMemoryRequest);
      }
    }

    // Token: 0x170000AF RID: 175
    // (get) Token: 0x06000337 RID: 823 RVA: 0x0035319C File Offset: 0x0035319C
    // (set) Token: 0x06000338 RID: 824 RVA: 0x003531A4 File Offset: 0x003531A4
    public string Memory { get; set; }

    // Token: 0x06000339 RID: 825 RVA: 0x003531B0 File Offset: 0x003531B0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("SaveMemoryRequest");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600033A RID: 826 RVA: 0x003531FC File Offset: 0x003531FC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Memory = ");
      builder.Append(this.Memory);
      return true;
    }

    // Token: 0x0600033B RID: 827 RVA: 0x00353220 File Offset: 0x00353220
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(SaveMemoryRequest left, SaveMemoryRequest right)
    {
      return !(left == right);
    }

    // Token: 0x0600033C RID: 828 RVA: 0x0035322C File Offset: 0x0035322C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(SaveMemoryRequest left, SaveMemoryRequest right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600033D RID: 829 RVA: 0x00353240 File Offset: 0x00353240
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Memory>k__BackingField);
    }

    // Token: 0x0600033E RID: 830 RVA: 0x0035326C File Offset: 0x0035326C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as SaveMemoryRequest);
    }

    // Token: 0x0600033F RID: 831 RVA: 0x0035327C File Offset: 0x0035327C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(SaveMemoryRequest other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Memory>k__BackingField, other.<Memory>k__BackingField));
    }

    // Token: 0x06000341 RID: 833 RVA: 0x003532BC File Offset: 0x003532BC
    [CompilerGenerated]
    private SaveMemoryRequest(SaveMemoryRequest original)
    {
      this.Memory = original.<Memory>k__BackingField;
    }

    // Token: 0x06000342 RID: 834 RVA: 0x003532D0 File Offset: 0x003532D0
    [CompilerGenerated]
    public void Deconstruct(out string Memory)
    {
      Memory = this.Memory;
    }
  }
}
