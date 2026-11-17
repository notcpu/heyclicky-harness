using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000050 RID: 80
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class StarterPlanClickyInput : IEquatable<StarterPlanClickyInput>
  {
    // Token: 0x06000391 RID: 913 RVA: 0x00353E1C File Offset: 0x00353E1C
    public StarterPlanClickyInput(string Slug, string Name, string Role, string Description, IReadOnlyList<StarterPlanRoutine> Routines, IReadOnlyList<string> ThreadIds)
    {
      this.Slug = Slug;
      this.Name = Name;
      this.Role = Role;
      this.Description = Description;
      this.Routines = Routines;
      this.ThreadIds = ThreadIds;
      base..ctor();
    }

    // Token: 0x170000C3 RID: 195
    // (get) Token: 0x06000392 RID: 914 RVA: 0x00353E54 File Offset: 0x00353E54
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(StarterPlanClickyInput);
      }
    }

    // Token: 0x170000C4 RID: 196
    // (get) Token: 0x06000393 RID: 915 RVA: 0x00353E60 File Offset: 0x00353E60
    // (set) Token: 0x06000394 RID: 916 RVA: 0x00353E68 File Offset: 0x00353E68
    public string Slug { get; set; }

    // Token: 0x170000C5 RID: 197
    // (get) Token: 0x06000395 RID: 917 RVA: 0x00353E74 File Offset: 0x00353E74
    // (set) Token: 0x06000396 RID: 918 RVA: 0x00353E7C File Offset: 0x00353E7C
    public string Name { get; set; }

    // Token: 0x170000C6 RID: 198
    // (get) Token: 0x06000397 RID: 919 RVA: 0x00353E88 File Offset: 0x00353E88
    // (set) Token: 0x06000398 RID: 920 RVA: 0x00353E90 File Offset: 0x00353E90
    public string Role { get; set; }

    // Token: 0x170000C7 RID: 199
    // (get) Token: 0x06000399 RID: 921 RVA: 0x00353E9C File Offset: 0x00353E9C
    // (set) Token: 0x0600039A RID: 922 RVA: 0x00353EA4 File Offset: 0x00353EA4
    public string Description { get; set; }

    // Token: 0x170000C8 RID: 200
    // (get) Token: 0x0600039B RID: 923 RVA: 0x00353EB0 File Offset: 0x00353EB0
    // (set) Token: 0x0600039C RID: 924 RVA: 0x00353EB8 File Offset: 0x00353EB8
    public IReadOnlyList<StarterPlanRoutine> Routines { get; set; }

    // Token: 0x170000C9 RID: 201
    // (get) Token: 0x0600039D RID: 925 RVA: 0x00353EC4 File Offset: 0x00353EC4
    // (set) Token: 0x0600039E RID: 926 RVA: 0x00353ECC File Offset: 0x00353ECC
    public IReadOnlyList<string> ThreadIds { get; set; }

    // Token: 0x0600039F RID: 927 RVA: 0x00353ED8 File Offset: 0x00353ED8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("StarterPlanClickyInput");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060003A0 RID: 928 RVA: 0x00353F24 File Offset: 0x00353F24
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

    // Token: 0x060003A1 RID: 929 RVA: 0x00353FD0 File Offset: 0x00353FD0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(StarterPlanClickyInput left, StarterPlanClickyInput right)
    {
      return !(left == right);
    }

    // Token: 0x060003A2 RID: 930 RVA: 0x00353FDC File Offset: 0x00353FDC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(StarterPlanClickyInput left, StarterPlanClickyInput right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060003A3 RID: 931 RVA: 0x00353FF0 File Offset: 0x00353FF0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Slug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Role>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Description>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<StarterPlanRoutine>>.Default.GetHashCode(this.<Routines>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<ThreadIds>k__BackingField);
    }

    // Token: 0x060003A4 RID: 932 RVA: 0x00354098 File Offset: 0x00354098
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as StarterPlanClickyInput);
    }

    // Token: 0x060003A5 RID: 933 RVA: 0x003540A8 File Offset: 0x003540A8
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(StarterPlanClickyInput other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Slug>k__BackingField, other.<Slug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Role>k__BackingField, other.<Role>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField) && EqualityComparer<IReadOnlyList<StarterPlanRoutine>>.Default.Equals(this.<Routines>k__BackingField, other.<Routines>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<ThreadIds>k__BackingField, other.<ThreadIds>k__BackingField));
    }

    // Token: 0x060003A7 RID: 935 RVA: 0x00354174 File Offset: 0x00354174
    [CompilerGenerated]
    private StarterPlanClickyInput(StarterPlanClickyInput original)
    {
      this.Slug = original.<Slug>k__BackingField;
      this.Name = original.<Name>k__BackingField;
      this.Role = original.<Role>k__BackingField;
      this.Description = original.<Description>k__BackingField;
      this.Routines = original.<Routines>k__BackingField;
      this.ThreadIds = original.<ThreadIds>k__BackingField;
    }

    // Token: 0x060003A8 RID: 936 RVA: 0x003541D0 File Offset: 0x003541D0
    [CompilerGenerated]
    public void Deconstruct(out string Slug, out string Name, out string Role, out string Description, out IReadOnlyList<StarterPlanRoutine> Routines, out IReadOnlyList<string> ThreadIds)
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
