using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000087 RID: 135
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CustomConnectorDescription : IEquatable<CustomConnectorDescription>
  {
    // Token: 0x06000825 RID: 2085 RVA: 0x00368DD4 File Offset: 0x00368DD4
    public CustomConnectorDescription(string Name, string Description)
    {
      this.Name = Name;
      this.Description = Description;
      base..ctor();
    }

    // Token: 0x170001CA RID: 458
    // (get) Token: 0x06000826 RID: 2086 RVA: 0x00368DEC File Offset: 0x00368DEC
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(CustomConnectorDescription);
      }
    }

    // Token: 0x170001CB RID: 459
    // (get) Token: 0x06000827 RID: 2087 RVA: 0x00368DF8 File Offset: 0x00368DF8
    // (set) Token: 0x06000828 RID: 2088 RVA: 0x00368E00 File Offset: 0x00368E00
    public string Name { get; set; }

    // Token: 0x170001CC RID: 460
    // (get) Token: 0x06000829 RID: 2089 RVA: 0x00368E0C File Offset: 0x00368E0C
    // (set) Token: 0x0600082A RID: 2090 RVA: 0x00368E14 File Offset: 0x00368E14
    public string Description { get; set; }

    // Token: 0x0600082B RID: 2091 RVA: 0x00368E20 File Offset: 0x00368E20
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("CustomConnectorDescription");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600082C RID: 2092 RVA: 0x00368E6C File Offset: 0x00368E6C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Name = ");
      builder.Append(this.Name);
      builder.Append(", Description = ");
      builder.Append(this.Description);
      return true;
    }

    // Token: 0x0600082D RID: 2093 RVA: 0x00368EA8 File Offset: 0x00368EA8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(CustomConnectorDescription left, CustomConnectorDescription right)
    {
      return !(left == right);
    }

    // Token: 0x0600082E RID: 2094 RVA: 0x00368EB4 File Offset: 0x00368EB4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(CustomConnectorDescription left, CustomConnectorDescription right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600082F RID: 2095 RVA: 0x00368EC8 File Offset: 0x00368EC8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Description>k__BackingField);
    }

    // Token: 0x06000830 RID: 2096 RVA: 0x00368F08 File Offset: 0x00368F08
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as CustomConnectorDescription);
    }

    // Token: 0x06000831 RID: 2097 RVA: 0x00368F18 File Offset: 0x00368F18
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(CustomConnectorDescription other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField));
    }

    // Token: 0x06000833 RID: 2099 RVA: 0x00368F7C File Offset: 0x00368F7C
    [CompilerGenerated]
    private CustomConnectorDescription(CustomConnectorDescription original)
    {
      this.Name = original.<Name>k__BackingField;
      this.Description = original.<Description>k__BackingField;
    }

    // Token: 0x06000834 RID: 2100 RVA: 0x00368F9C File Offset: 0x00368F9C
    [CompilerGenerated]
    public void Deconstruct(out string Name, out string Description)
    {
      Name = this.Name;
      Description = this.Description;
    }
  }
}
