using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Runtime
{
  // Token: 0x0200001F RID: 31
  [NullableContext(1)]
  [Nullable(0)]
  public abstract class CustomConnectorTransport : IEquatable<CustomConnectorTransport>
  {
    // Token: 0x170000FE RID: 254
    // (get) Token: 0x060003A8 RID: 936 RVA: 0x000E67DC File Offset: 0x000E67DC
    [CompilerGenerated]
    protected virtual Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(CustomConnectorTransport);
      }
    }

    // Token: 0x060003A9 RID: 937 RVA: 0x000E67E8 File Offset: 0x000E67E8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("CustomConnectorTransport");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060003AA RID: 938 RVA: 0x000E6834 File Offset: 0x000E6834
    [CompilerGenerated]
    protected virtual bool PrintMembers(StringBuilder builder)
    {
      return false;
    }

    // Token: 0x060003AB RID: 939 RVA: 0x000E6838 File Offset: 0x000E6838
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(CustomConnectorTransport left, CustomConnectorTransport right)
    {
      return !(left == right);
    }

    // Token: 0x060003AC RID: 940 RVA: 0x000E6844 File Offset: 0x000E6844
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(CustomConnectorTransport left, CustomConnectorTransport right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060003AD RID: 941 RVA: 0x000E6858 File Offset: 0x000E6858
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
    }

    // Token: 0x060003AE RID: 942 RVA: 0x000E686C File Offset: 0x000E686C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as CustomConnectorTransport);
    }

    // Token: 0x060003AF RID: 943 RVA: 0x000E687C File Offset: 0x000E687C
    [NullableContext(2)]
    [CompilerGenerated]
    public virtual bool Equals(CustomConnectorTransport other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract);
    }

    // Token: 0x060003B1 RID: 945 RVA: 0x000E689C File Offset: 0x000E689C
    [CompilerGenerated]
    protected CustomConnectorTransport(CustomConnectorTransport original)
    {
    }

    // Token: 0x060003B2 RID: 946 RVA: 0x000E68A4 File Offset: 0x000E68A4
    protected CustomConnectorTransport()
    {
    }

    // Token: 0x020000FD RID: 253
    [Nullable(0)]
    public sealed class Remote : CustomConnectorTransport, IEquatable<CustomConnectorTransport.Remote>
    {
      // Token: 0x06000B38 RID: 2872 RVA: 0x0010E610 File Offset: 0x0010E610
      public Remote(string Url, [Nullable(2)] string BearerTokenEnvironmentVariable, IReadOnlyDictionary<string, string> HeaderEnvironmentVariables)
      {
        this.Url = Url;
        this.BearerTokenEnvironmentVariable = BearerTokenEnvironmentVariable;
        this.HeaderEnvironmentVariables = HeaderEnvironmentVariables;
        base..ctor();
      }

      // Token: 0x17000286 RID: 646
      // (get) Token: 0x06000B39 RID: 2873 RVA: 0x0010E630 File Offset: 0x0010E630
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(CustomConnectorTransport.Remote);
        }
      }

      // Token: 0x17000287 RID: 647
      // (get) Token: 0x06000B3A RID: 2874 RVA: 0x0010E63C File Offset: 0x0010E63C
      // (set) Token: 0x06000B3B RID: 2875 RVA: 0x0010E644 File Offset: 0x0010E644
      public string Url { get; set; }

      // Token: 0x17000288 RID: 648
      // (get) Token: 0x06000B3C RID: 2876 RVA: 0x0010E650 File Offset: 0x0010E650
      // (set) Token: 0x06000B3D RID: 2877 RVA: 0x0010E658 File Offset: 0x0010E658
      [Nullable(2)]
      public string BearerTokenEnvironmentVariable
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x17000289 RID: 649
      // (get) Token: 0x06000B3E RID: 2878 RVA: 0x0010E664 File Offset: 0x0010E664
      // (set) Token: 0x06000B3F RID: 2879 RVA: 0x0010E66C File Offset: 0x0010E66C
      public IReadOnlyDictionary<string, string> HeaderEnvironmentVariables { get; set; }

      // Token: 0x06000B40 RID: 2880 RVA: 0x0010E678 File Offset: 0x0010E678
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Remote");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000B41 RID: 2881 RVA: 0x0010E6C4 File Offset: 0x0010E6C4
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Url = ");
        builder.Append(this.Url);
        builder.Append(", BearerTokenEnvironmentVariable = ");
        builder.Append(this.BearerTokenEnvironmentVariable);
        builder.Append(", HeaderEnvironmentVariables = ");
        builder.Append(this.HeaderEnvironmentVariables);
        return true;
      }

      // Token: 0x06000B42 RID: 2882 RVA: 0x0010E734 File Offset: 0x0010E734
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(CustomConnectorTransport.Remote left, CustomConnectorTransport.Remote right)
      {
        return !(left == right);
      }

      // Token: 0x06000B43 RID: 2883 RVA: 0x0010E740 File Offset: 0x0010E740
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(CustomConnectorTransport.Remote left, CustomConnectorTransport.Remote right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000B44 RID: 2884 RVA: 0x0010E754 File Offset: 0x0010E754
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Url>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BearerTokenEnvironmentVariable>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyDictionary<string, string>>.Default.GetHashCode(this.<HeaderEnvironmentVariables>k__BackingField);
      }

      // Token: 0x06000B45 RID: 2885 RVA: 0x0010E7AC File Offset: 0x0010E7AC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as CustomConnectorTransport.Remote);
      }

      // Token: 0x06000B46 RID: 2886 RVA: 0x0010E7BC File Offset: 0x0010E7BC
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(CustomConnectorTransport other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000B47 RID: 2887 RVA: 0x0010E7C8 File Offset: 0x0010E7C8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(CustomConnectorTransport.Remote other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Url>k__BackingField, other.<Url>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<BearerTokenEnvironmentVariable>k__BackingField, other.<BearerTokenEnvironmentVariable>k__BackingField) && EqualityComparer<IReadOnlyDictionary<string, string>>.Default.Equals(this.<HeaderEnvironmentVariables>k__BackingField, other.<HeaderEnvironmentVariables>k__BackingField));
      }

      // Token: 0x06000B49 RID: 2889 RVA: 0x0010E834 File Offset: 0x0010E834
      [CompilerGenerated]
      private Remote(CustomConnectorTransport.Remote original)
        : base(original)
      {
        this.Url = original.<Url>k__BackingField;
        this.BearerTokenEnvironmentVariable = original.<BearerTokenEnvironmentVariable>k__BackingField;
        this.HeaderEnvironmentVariables = original.<HeaderEnvironmentVariables>k__BackingField;
      }

      // Token: 0x06000B4A RID: 2890 RVA: 0x0010E864 File Offset: 0x0010E864
      [CompilerGenerated]
      public void Deconstruct(out string Url, [Nullable(2)] out string BearerTokenEnvironmentVariable, out IReadOnlyDictionary<string, string> HeaderEnvironmentVariables)
      {
        Url = this.Url;
        BearerTokenEnvironmentVariable = this.BearerTokenEnvironmentVariable;
        HeaderEnvironmentVariables = this.HeaderEnvironmentVariables;
      }
    }

    // Token: 0x020000FE RID: 254
    [Nullable(0)]
    public sealed class Stdio : CustomConnectorTransport, IEquatable<CustomConnectorTransport.Stdio>
    {
      // Token: 0x06000B4B RID: 2891 RVA: 0x0010E880 File Offset: 0x0010E880
      public Stdio(string Command, IReadOnlyList<string> Arguments, IReadOnlyList<string> PassthroughEnvironmentVariables, string SearchPath)
      {
        this.Command = Command;
        this.Arguments = Arguments;
        this.PassthroughEnvironmentVariables = PassthroughEnvironmentVariables;
        this.SearchPath = SearchPath;
        base..ctor();
      }

      // Token: 0x1700028A RID: 650
      // (get) Token: 0x06000B4C RID: 2892 RVA: 0x0010E8A8 File Offset: 0x0010E8A8
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(CustomConnectorTransport.Stdio);
        }
      }

      // Token: 0x1700028B RID: 651
      // (get) Token: 0x06000B4D RID: 2893 RVA: 0x0010E8B4 File Offset: 0x0010E8B4
      // (set) Token: 0x06000B4E RID: 2894 RVA: 0x0010E8BC File Offset: 0x0010E8BC
      public string Command { get; set; }

      // Token: 0x1700028C RID: 652
      // (get) Token: 0x06000B4F RID: 2895 RVA: 0x0010E8C8 File Offset: 0x0010E8C8
      // (set) Token: 0x06000B50 RID: 2896 RVA: 0x0010E8D0 File Offset: 0x0010E8D0
      public IReadOnlyList<string> Arguments { get; set; }

      // Token: 0x1700028D RID: 653
      // (get) Token: 0x06000B51 RID: 2897 RVA: 0x0010E8DC File Offset: 0x0010E8DC
      // (set) Token: 0x06000B52 RID: 2898 RVA: 0x0010E8E4 File Offset: 0x0010E8E4
      public IReadOnlyList<string> PassthroughEnvironmentVariables { get; set; }

      // Token: 0x1700028E RID: 654
      // (get) Token: 0x06000B53 RID: 2899 RVA: 0x0010E8F0 File Offset: 0x0010E8F0
      // (set) Token: 0x06000B54 RID: 2900 RVA: 0x0010E8F8 File Offset: 0x0010E8F8
      public string SearchPath { get; set; }

      // Token: 0x06000B55 RID: 2901 RVA: 0x0010E904 File Offset: 0x0010E904
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Stdio");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000B56 RID: 2902 RVA: 0x0010E950 File Offset: 0x0010E950
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Command = ");
        builder.Append(this.Command);
        builder.Append(", Arguments = ");
        builder.Append(this.Arguments);
        builder.Append(", PassthroughEnvironmentVariables = ");
        builder.Append(this.PassthroughEnvironmentVariables);
        builder.Append(", SearchPath = ");
        builder.Append(this.SearchPath);
        return true;
      }

      // Token: 0x06000B57 RID: 2903 RVA: 0x0010E9D8 File Offset: 0x0010E9D8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(CustomConnectorTransport.Stdio left, CustomConnectorTransport.Stdio right)
      {
        return !(left == right);
      }

      // Token: 0x06000B58 RID: 2904 RVA: 0x0010E9E4 File Offset: 0x0010E9E4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(CustomConnectorTransport.Stdio left, CustomConnectorTransport.Stdio right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000B59 RID: 2905 RVA: 0x0010E9F8 File Offset: 0x0010E9F8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Command>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Arguments>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<PassthroughEnvironmentVariables>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SearchPath>k__BackingField);
      }

      // Token: 0x06000B5A RID: 2906 RVA: 0x0010EA68 File Offset: 0x0010EA68
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as CustomConnectorTransport.Stdio);
      }

      // Token: 0x06000B5B RID: 2907 RVA: 0x0010EA78 File Offset: 0x0010EA78
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(CustomConnectorTransport other)
      {
        return this.Equals(other);
      }

      // Token: 0x06000B5C RID: 2908 RVA: 0x0010EA84 File Offset: 0x0010EA84
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(CustomConnectorTransport.Stdio other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Command>k__BackingField, other.<Command>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Arguments>k__BackingField, other.<Arguments>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<PassthroughEnvironmentVariables>k__BackingField, other.<PassthroughEnvironmentVariables>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SearchPath>k__BackingField, other.<SearchPath>k__BackingField));
      }

      // Token: 0x06000B5E RID: 2910 RVA: 0x0010EB08 File Offset: 0x0010EB08
      [CompilerGenerated]
      private Stdio(CustomConnectorTransport.Stdio original)
        : base(original)
      {
        this.Command = original.<Command>k__BackingField;
        this.Arguments = original.<Arguments>k__BackingField;
        this.PassthroughEnvironmentVariables = original.<PassthroughEnvironmentVariables>k__BackingField;
        this.SearchPath = original.<SearchPath>k__BackingField;
      }

      // Token: 0x06000B5F RID: 2911 RVA: 0x0010EB44 File Offset: 0x0010EB44
      [CompilerGenerated]
      public void Deconstruct(out string Command, out IReadOnlyList<string> Arguments, out IReadOnlyList<string> PassthroughEnvironmentVariables, out string SearchPath)
      {
        Command = this.Command;
        Arguments = this.Arguments;
        PassthroughEnvironmentVariables = this.PassthroughEnvironmentVariables;
        SearchPath = this.SearchPath;
      }
    }
  }
}
