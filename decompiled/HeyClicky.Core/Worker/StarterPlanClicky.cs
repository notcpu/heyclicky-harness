using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000053 RID: 83
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class StarterPlanClicky : IEquatable<StarterPlanClicky>
  {
    // Token: 0x060003D5 RID: 981 RVA: 0x00354900 File Offset: 0x00354900
    public StarterPlanClicky(string Key, string Name, string Role, string Description, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> IntroMessages, [Nullable(2)] string RevealLine, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> FirstAsks, [Nullable(2)] StarterPlanClickyRoutine Routine = null)
    {
      this.Key = Key;
      this.Name = Name;
      this.Role = Role;
      this.Description = Description;
      this.IntroMessages = IntroMessages;
      this.RevealLine = RevealLine;
      this.FirstAsks = FirstAsks;
      this.Routine = Routine;
      base..ctor();
    }

    // Token: 0x170000D6 RID: 214
    // (get) Token: 0x060003D6 RID: 982 RVA: 0x00354950 File Offset: 0x00354950
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(StarterPlanClicky);
      }
    }

    // Token: 0x170000D7 RID: 215
    // (get) Token: 0x060003D7 RID: 983 RVA: 0x0035495C File Offset: 0x0035495C
    // (set) Token: 0x060003D8 RID: 984 RVA: 0x00354964 File Offset: 0x00354964
    public string Key { get; set; }

    // Token: 0x170000D8 RID: 216
    // (get) Token: 0x060003D9 RID: 985 RVA: 0x00354970 File Offset: 0x00354970
    // (set) Token: 0x060003DA RID: 986 RVA: 0x00354978 File Offset: 0x00354978
    public string Name { get; set; }

    // Token: 0x170000D9 RID: 217
    // (get) Token: 0x060003DB RID: 987 RVA: 0x00354984 File Offset: 0x00354984
    // (set) Token: 0x060003DC RID: 988 RVA: 0x0035498C File Offset: 0x0035498C
    public string Role { get; set; }

    // Token: 0x170000DA RID: 218
    // (get) Token: 0x060003DD RID: 989 RVA: 0x00354998 File Offset: 0x00354998
    // (set) Token: 0x060003DE RID: 990 RVA: 0x003549A0 File Offset: 0x003549A0
    public string Description { get; set; }

    // Token: 0x170000DB RID: 219
    // (get) Token: 0x060003DF RID: 991 RVA: 0x003549AC File Offset: 0x003549AC
    // (set) Token: 0x060003E0 RID: 992 RVA: 0x003549B4 File Offset: 0x003549B4
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> IntroMessages
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x170000DC RID: 220
    // (get) Token: 0x060003E1 RID: 993 RVA: 0x003549C0 File Offset: 0x003549C0
    // (set) Token: 0x060003E2 RID: 994 RVA: 0x003549C8 File Offset: 0x003549C8
    [Nullable(2)]
    public string RevealLine
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170000DD RID: 221
    // (get) Token: 0x060003E3 RID: 995 RVA: 0x003549D4 File Offset: 0x003549D4
    // (set) Token: 0x060003E4 RID: 996 RVA: 0x003549DC File Offset: 0x003549DC
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> FirstAsks
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x170000DE RID: 222
    // (get) Token: 0x060003E5 RID: 997 RVA: 0x003549E8 File Offset: 0x003549E8
    // (set) Token: 0x060003E6 RID: 998 RVA: 0x003549F0 File Offset: 0x003549F0
    [Nullable(2)]
    public StarterPlanClickyRoutine Routine
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x060003E7 RID: 999 RVA: 0x003549FC File Offset: 0x003549FC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("StarterPlanClicky");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060003E8 RID: 1000 RVA: 0x00354A48 File Offset: 0x00354A48
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Key = ");
      builder.Append(this.Key);
      builder.Append(", Name = ");
      builder.Append(this.Name);
      builder.Append(", Role = ");
      builder.Append(this.Role);
      builder.Append(", Description = ");
      builder.Append(this.Description);
      builder.Append(", IntroMessages = ");
      builder.Append(this.IntroMessages);
      builder.Append(", RevealLine = ");
      builder.Append(this.RevealLine);
      builder.Append(", FirstAsks = ");
      builder.Append(this.FirstAsks);
      builder.Append(", Routine = ");
      builder.Append(this.Routine);
      return true;
    }

    // Token: 0x060003E9 RID: 1001 RVA: 0x00354B24 File Offset: 0x00354B24
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(StarterPlanClicky left, StarterPlanClicky right)
    {
      return !(left == right);
    }

    // Token: 0x060003EA RID: 1002 RVA: 0x00354B30 File Offset: 0x00354B30
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(StarterPlanClicky left, StarterPlanClicky right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060003EB RID: 1003 RVA: 0x00354B44 File Offset: 0x00354B44
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Key>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Role>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Description>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<IntroMessages>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RevealLine>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<FirstAsks>k__BackingField)) * -1521134295 + EqualityComparer<StarterPlanClickyRoutine>.Default.GetHashCode(this.<Routine>k__BackingField);
    }

    // Token: 0x060003EC RID: 1004 RVA: 0x00354C1C File Offset: 0x00354C1C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as StarterPlanClicky);
    }

    // Token: 0x060003ED RID: 1005 RVA: 0x00354C2C File Offset: 0x00354C2C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(StarterPlanClicky other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Key>k__BackingField, other.<Key>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Role>k__BackingField, other.<Role>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<IntroMessages>k__BackingField, other.<IntroMessages>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RevealLine>k__BackingField, other.<RevealLine>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<FirstAsks>k__BackingField, other.<FirstAsks>k__BackingField) && EqualityComparer<StarterPlanClickyRoutine>.Default.Equals(this.<Routine>k__BackingField, other.<Routine>k__BackingField));
    }

    // Token: 0x060003EF RID: 1007 RVA: 0x00354D2C File Offset: 0x00354D2C
    [CompilerGenerated]
    private StarterPlanClicky(StarterPlanClicky original)
    {
      this.Key = original.<Key>k__BackingField;
      this.Name = original.<Name>k__BackingField;
      this.Role = original.<Role>k__BackingField;
      this.Description = original.<Description>k__BackingField;
      this.IntroMessages = original.<IntroMessages>k__BackingField;
      this.RevealLine = original.<RevealLine>k__BackingField;
      this.FirstAsks = original.<FirstAsks>k__BackingField;
      this.Routine = original.<Routine>k__BackingField;
    }

    // Token: 0x060003F0 RID: 1008 RVA: 0x00354DA0 File Offset: 0x00354DA0
    [CompilerGenerated]
    public void Deconstruct(out string Key, out string Name, out string Role, out string Description, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<string> IntroMessages, [Nullable(2)] out string RevealLine, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<string> FirstAsks, [Nullable(2)] out StarterPlanClickyRoutine Routine)
    {
      Key = this.Key;
      Name = this.Name;
      Role = this.Role;
      Description = this.Description;
      IntroMessages = this.IntroMessages;
      RevealLine = this.RevealLine;
      FirstAsks = this.FirstAsks;
      Routine = this.Routine;
    }
  }
}
