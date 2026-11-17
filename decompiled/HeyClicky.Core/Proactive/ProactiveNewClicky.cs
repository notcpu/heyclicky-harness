using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000C9 RID: 201
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ProactiveNewClicky : IEquatable<ProactiveNewClicky>
  {
    // Token: 0x06000D09 RID: 3337 RVA: 0x003810AC File Offset: 0x003810AC
    public ProactiveNewClicky(string Name, string Role, string Description)
    {
      this.Name = Name;
      this.Role = Role;
      this.Description = Description;
      base..ctor();
    }

    // Token: 0x170002DA RID: 730
    // (get) Token: 0x06000D0A RID: 3338 RVA: 0x003810CC File Offset: 0x003810CC
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ProactiveNewClicky);
      }
    }

    // Token: 0x170002DB RID: 731
    // (get) Token: 0x06000D0B RID: 3339 RVA: 0x003810D8 File Offset: 0x003810D8
    // (set) Token: 0x06000D0C RID: 3340 RVA: 0x003810E0 File Offset: 0x003810E0
    public string Name { get; set; }

    // Token: 0x170002DC RID: 732
    // (get) Token: 0x06000D0D RID: 3341 RVA: 0x003810EC File Offset: 0x003810EC
    // (set) Token: 0x06000D0E RID: 3342 RVA: 0x003810F4 File Offset: 0x003810F4
    public string Role { get; set; }

    // Token: 0x170002DD RID: 733
    // (get) Token: 0x06000D0F RID: 3343 RVA: 0x00381100 File Offset: 0x00381100
    // (set) Token: 0x06000D10 RID: 3344 RVA: 0x00381108 File Offset: 0x00381108
    public string Description { get; set; }

    // Token: 0x06000D11 RID: 3345 RVA: 0x00381114 File Offset: 0x00381114
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ProactiveNewClicky");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000D12 RID: 3346 RVA: 0x00381160 File Offset: 0x00381160
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Name = ");
      builder.Append(this.Name);
      builder.Append(", Role = ");
      builder.Append(this.Role);
      builder.Append(", Description = ");
      builder.Append(this.Description);
      return true;
    }

    // Token: 0x06000D13 RID: 3347 RVA: 0x003811C0 File Offset: 0x003811C0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ProactiveNewClicky left, ProactiveNewClicky right)
    {
      return !(left == right);
    }

    // Token: 0x06000D14 RID: 3348 RVA: 0x003811CC File Offset: 0x003811CC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ProactiveNewClicky left, ProactiveNewClicky right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000D15 RID: 3349 RVA: 0x003811E0 File Offset: 0x003811E0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Role>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Description>k__BackingField);
    }

    // Token: 0x06000D16 RID: 3350 RVA: 0x00381244 File Offset: 0x00381244
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ProactiveNewClicky);
    }

    // Token: 0x06000D17 RID: 3351 RVA: 0x00381254 File Offset: 0x00381254
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ProactiveNewClicky other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Role>k__BackingField, other.<Role>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField));
    }

    // Token: 0x06000D19 RID: 3353 RVA: 0x003812D0 File Offset: 0x003812D0
    [CompilerGenerated]
    private ProactiveNewClicky(ProactiveNewClicky original)
    {
      this.Name = original.<Name>k__BackingField;
      this.Role = original.<Role>k__BackingField;
      this.Description = original.<Description>k__BackingField;
    }

    // Token: 0x06000D1A RID: 3354 RVA: 0x003812FC File Offset: 0x003812FC
    [CompilerGenerated]
    public void Deconstruct(out string Name, out string Role, out string Description)
    {
      Name = this.Name;
      Role = this.Role;
      Description = this.Description;
    }
  }
}
