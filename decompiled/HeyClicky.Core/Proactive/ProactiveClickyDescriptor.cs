using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000C1 RID: 193
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ProactiveClickyDescriptor : IEquatable<ProactiveClickyDescriptor>
  {
    // Token: 0x06000CA8 RID: 3240 RVA: 0x0037F958 File Offset: 0x0037F958
    public ProactiveClickyDescriptor(string Slug, string Name, string Role, string Description, [TupleElementNames(new string[] { "Title", "EveryMinutes" })] [Nullable(new byte[] { 1, 0, 1 })] IReadOnlyList<ValueTuple<string, int>> Routines, IReadOnlyList<string> ThreadIds)
    {
      this.Slug = Slug;
      this.Name = Name;
      this.Role = Role;
      this.Description = Description;
      this.Routines = Routines;
      this.ThreadIds = ThreadIds;
      base..ctor();
    }

    // Token: 0x170002C0 RID: 704
    // (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0037F990 File Offset: 0x0037F990
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ProactiveClickyDescriptor);
      }
    }

    // Token: 0x170002C1 RID: 705
    // (get) Token: 0x06000CAA RID: 3242 RVA: 0x0037F99C File Offset: 0x0037F99C
    // (set) Token: 0x06000CAB RID: 3243 RVA: 0x0037F9A4 File Offset: 0x0037F9A4
    public string Slug { get; set; }

    // Token: 0x170002C2 RID: 706
    // (get) Token: 0x06000CAC RID: 3244 RVA: 0x0037F9B0 File Offset: 0x0037F9B0
    // (set) Token: 0x06000CAD RID: 3245 RVA: 0x0037F9B8 File Offset: 0x0037F9B8
    public string Name { get; set; }

    // Token: 0x170002C3 RID: 707
    // (get) Token: 0x06000CAE RID: 3246 RVA: 0x0037F9C4 File Offset: 0x0037F9C4
    // (set) Token: 0x06000CAF RID: 3247 RVA: 0x0037F9CC File Offset: 0x0037F9CC
    public string Role { get; set; }

    // Token: 0x170002C4 RID: 708
    // (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0037F9D8 File Offset: 0x0037F9D8
    // (set) Token: 0x06000CB1 RID: 3249 RVA: 0x0037F9E0 File Offset: 0x0037F9E0
    public string Description { get; set; }

    // Token: 0x170002C5 RID: 709
    // (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0037F9EC File Offset: 0x0037F9EC
    // (set) Token: 0x06000CB3 RID: 3251 RVA: 0x0037F9F4 File Offset: 0x0037F9F4
    [TupleElementNames(new string[] { "Title", "EveryMinutes" })]
    [Nullable(new byte[] { 1, 0, 1 })]
    public IReadOnlyList<ValueTuple<string, int>> Routines
    {
      [return: TupleElementNames(new string[] { "Title", "EveryMinutes" })]
      [return: Nullable(new byte[] { 1, 0, 1 })]
      get;
      [param: TupleElementNames(new string[] { "Title", "EveryMinutes" })]
      [param: Nullable(new byte[] { 1, 0, 1 })]
      set;
    }

    // Token: 0x170002C6 RID: 710
    // (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0037FA00 File Offset: 0x0037FA00
    // (set) Token: 0x06000CB5 RID: 3253 RVA: 0x0037FA08 File Offset: 0x0037FA08
    public IReadOnlyList<string> ThreadIds { get; set; }

    // Token: 0x06000CB6 RID: 3254 RVA: 0x0037FA14 File Offset: 0x0037FA14
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ProactiveClickyDescriptor");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000CB7 RID: 3255 RVA: 0x0037FA60 File Offset: 0x0037FA60
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Slug = ");
      builder.Append(this.Slug);
      builder.Append(", Name = ");
      builder.Append(this.Name);
      builder.Append(", Role = ");
      builder.Append(this.Role);
      builder.Append(", Description = ");
      builder.Append(this.Description);
      builder.Append(", Routines = ");
      builder.Append(this.Routines);
      builder.Append(", ThreadIds = ");
      builder.Append(this.ThreadIds);
      return true;
    }

    // Token: 0x06000CB8 RID: 3256 RVA: 0x0037FB0C File Offset: 0x0037FB0C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ProactiveClickyDescriptor left, ProactiveClickyDescriptor right)
    {
      return !(left == right);
    }

    // Token: 0x06000CB9 RID: 3257 RVA: 0x0037FB18 File Offset: 0x0037FB18
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ProactiveClickyDescriptor left, ProactiveClickyDescriptor right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000CBA RID: 3258 RVA: 0x0037FB2C File Offset: 0x0037FB2C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Slug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Role>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Description>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<ValueTuple<string, int>>>.Default.GetHashCode(this.<Routines>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<ThreadIds>k__BackingField);
    }

    // Token: 0x06000CBB RID: 3259 RVA: 0x0037FBD4 File Offset: 0x0037FBD4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ProactiveClickyDescriptor);
    }

    // Token: 0x06000CBC RID: 3260 RVA: 0x0037FBE4 File Offset: 0x0037FBE4
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ProactiveClickyDescriptor other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Slug>k__BackingField, other.<Slug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Role>k__BackingField, other.<Role>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField) && EqualityComparer<IReadOnlyList<ValueTuple<string, int>>>.Default.Equals(this.<Routines>k__BackingField, other.<Routines>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<ThreadIds>k__BackingField, other.<ThreadIds>k__BackingField));
    }

    // Token: 0x06000CBE RID: 3262 RVA: 0x0037FCB0 File Offset: 0x0037FCB0
    [CompilerGenerated]
    private ProactiveClickyDescriptor(ProactiveClickyDescriptor original)
    {
      this.Slug = original.<Slug>k__BackingField;
      this.Name = original.<Name>k__BackingField;
      this.Role = original.<Role>k__BackingField;
      this.Description = original.<Description>k__BackingField;
      this.Routines = original.<Routines>k__BackingField;
      this.ThreadIds = original.<ThreadIds>k__BackingField;
    }

    // Token: 0x06000CBF RID: 3263 RVA: 0x0037FD0C File Offset: 0x0037FD0C
    [CompilerGenerated]
    public void Deconstruct(out string Slug, out string Name, out string Role, out string Description, [TupleElementNames(new string[] { "Title", "EveryMinutes" })] [Nullable(new byte[] { 1, 0, 1 })] out IReadOnlyList<ValueTuple<string, int>> Routines, out IReadOnlyList<string> ThreadIds)
    {
      Slug = this.Slug;
      Name = this.Name;
      Role = this.Role;
      Description = this.Description;
      Routines = this.Routines;
      ThreadIds = this.ThreadIds;
    }
  }
}
