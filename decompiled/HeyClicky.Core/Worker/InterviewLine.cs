using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x0200004E RID: 78
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class InterviewLine : IEquatable<InterviewLine>
  {
    // Token: 0x06000371 RID: 881 RVA: 0x00353A4C File Offset: 0x00353A4C
    public InterviewLine(string Role, string Text)
    {
      this.Role = Role;
      this.Text = Text;
      base..ctor();
    }

    // Token: 0x170000BD RID: 189
    // (get) Token: 0x06000372 RID: 882 RVA: 0x00353A64 File Offset: 0x00353A64
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(InterviewLine);
      }
    }

    // Token: 0x170000BE RID: 190
    // (get) Token: 0x06000373 RID: 883 RVA: 0x00353A70 File Offset: 0x00353A70
    // (set) Token: 0x06000374 RID: 884 RVA: 0x00353A78 File Offset: 0x00353A78
    public string Role { get; set; }

    // Token: 0x170000BF RID: 191
    // (get) Token: 0x06000375 RID: 885 RVA: 0x00353A84 File Offset: 0x00353A84
    // (set) Token: 0x06000376 RID: 886 RVA: 0x00353A8C File Offset: 0x00353A8C
    public string Text { get; set; }

    // Token: 0x06000377 RID: 887 RVA: 0x00353A98 File Offset: 0x00353A98
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("InterviewLine");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000378 RID: 888 RVA: 0x00353AE4 File Offset: 0x00353AE4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Role = ");
      builder.Append(this.Role);
      builder.Append(", Text = ");
      builder.Append(this.Text);
      return true;
    }

    // Token: 0x06000379 RID: 889 RVA: 0x00353B20 File Offset: 0x00353B20
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(InterviewLine left, InterviewLine right)
    {
      return !(left == right);
    }

    // Token: 0x0600037A RID: 890 RVA: 0x00353B2C File Offset: 0x00353B2C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(InterviewLine left, InterviewLine right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600037B RID: 891 RVA: 0x00353B40 File Offset: 0x00353B40
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Role>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField);
    }

    // Token: 0x0600037C RID: 892 RVA: 0x00353B80 File Offset: 0x00353B80
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as InterviewLine);
    }

    // Token: 0x0600037D RID: 893 RVA: 0x00353B90 File Offset: 0x00353B90
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(InterviewLine other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Role>k__BackingField, other.<Role>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField));
    }

    // Token: 0x0600037F RID: 895 RVA: 0x00353BF4 File Offset: 0x00353BF4
    [CompilerGenerated]
    private InterviewLine(InterviewLine original)
    {
      this.Role = original.<Role>k__BackingField;
      this.Text = original.<Text>k__BackingField;
    }

    // Token: 0x06000380 RID: 896 RVA: 0x00353C14 File Offset: 0x00353C14
    [CompilerGenerated]
    public void Deconstruct(out string Role, out string Text)
    {
      Role = this.Role;
      Text = this.Text;
    }
  }
}
