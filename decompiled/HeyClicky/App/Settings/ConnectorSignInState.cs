using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000C7 RID: 199
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ConnectorSignInState : IEquatable<ConnectorSignInState>
  {
    // Token: 0x06000851 RID: 2129 RVA: 0x0053E8FC File Offset: 0x0053E8FC
    public ConnectorSignInState(ConnectorSignInKind Kind, string Text = null)
    {
      this.Kind = Kind;
      this.Text = Text;
      base..ctor();
    }

    // Token: 0x17000149 RID: 329
    // (get) Token: 0x06000852 RID: 2130 RVA: 0x0053E914 File Offset: 0x0053E914
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ConnectorSignInState);
      }
    }

    // Token: 0x1700014A RID: 330
    // (get) Token: 0x06000853 RID: 2131 RVA: 0x0053E920 File Offset: 0x0053E920
    // (set) Token: 0x06000854 RID: 2132 RVA: 0x0053E928 File Offset: 0x0053E928
    public ConnectorSignInKind Kind { get; set; }

    // Token: 0x1700014B RID: 331
    // (get) Token: 0x06000855 RID: 2133 RVA: 0x0053E934 File Offset: 0x0053E934
    // (set) Token: 0x06000856 RID: 2134 RVA: 0x0053E93C File Offset: 0x0053E93C
    public string Text { get; set; }

    // Token: 0x06000857 RID: 2135 RVA: 0x0053E948 File Offset: 0x0053E948
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ConnectorSignInState");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000858 RID: 2136 RVA: 0x0053E994 File Offset: 0x0053E994
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", Text = ");
      builder.Append(this.Text);
      return true;
    }

    // Token: 0x06000859 RID: 2137 RVA: 0x0053E9E8 File Offset: 0x0053E9E8
    [CompilerGenerated]
    public static bool operator !=(ConnectorSignInState left, ConnectorSignInState right)
    {
      return !(left == right);
    }

    // Token: 0x0600085A RID: 2138 RVA: 0x0053E9F4 File Offset: 0x0053E9F4
    [CompilerGenerated]
    public static bool operator ==(ConnectorSignInState left, ConnectorSignInState right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600085B RID: 2139 RVA: 0x0053EA08 File Offset: 0x0053EA08
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ConnectorSignInKind>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField);
    }

    // Token: 0x0600085C RID: 2140 RVA: 0x0053EA48 File Offset: 0x0053EA48
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ConnectorSignInState);
    }

    // Token: 0x0600085D RID: 2141 RVA: 0x0053EA58 File Offset: 0x0053EA58
    [CompilerGenerated]
    public bool Equals(ConnectorSignInState other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ConnectorSignInKind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField));
    }

    // Token: 0x0600085F RID: 2143 RVA: 0x0053EABC File Offset: 0x0053EABC
    [CompilerGenerated]
    private ConnectorSignInState([Nullable(1)] ConnectorSignInState original)
    {
      this.Kind = original.<Kind>k__BackingField;
      this.Text = original.<Text>k__BackingField;
    }

    // Token: 0x06000860 RID: 2144 RVA: 0x0053EADC File Offset: 0x0053EADC
    [CompilerGenerated]
    public void Deconstruct(out ConnectorSignInKind Kind, out string Text)
    {
      Kind = this.Kind;
      Text = this.Text;
    }

    // Token: 0x040002E5 RID: 741
    [Nullable(1)]
    public static readonly ConnectorSignInState Unknown = new ConnectorSignInState(ConnectorSignInKind.Unknown, null);
  }
}
