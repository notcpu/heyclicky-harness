using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Connectors
{
  // Token: 0x020001DB RID: 475
  [NullableContext(1)]
  [Nullable(0)]
  public abstract class McpVerification : IEquatable<McpVerification>
  {
    // Token: 0x17000602 RID: 1538
    // (get) Token: 0x06001B4A RID: 6986 RVA: 0x003DA9C4 File Offset: 0x003DA9C4
    [CompilerGenerated]
    protected virtual Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(McpVerification);
      }
    }

    // Token: 0x06001B4B RID: 6987 RVA: 0x003DA9D0 File Offset: 0x003DA9D0
    [NullableContext(2)]
    public static McpVerification Failure(int? statusCode)
    {
      McpVerification mcpVerification;
      if (statusCode != null)
      {
        int valueOrDefault = statusCode.GetValueOrDefault();
        if (valueOrDefault >= 200)
        {
          if (valueOrDefault < 300)
          {
            return null;
          }
          if (valueOrDefault == 401 || valueOrDefault == 403)
          {
            return new McpVerification.Unauthorized(statusCode.Value);
          }
        }
        mcpVerification = new McpVerification.ServerError(statusCode.Value);
      }
      else
      {
        mcpVerification = new McpVerification.Unreachable();
      }
      return mcpVerification;
    }

    // Token: 0x06001B4C RID: 6988 RVA: 0x003DAA3C File Offset: 0x003DAA3C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("McpVerification");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001B4D RID: 6989 RVA: 0x003DAA88 File Offset: 0x003DAA88
    [CompilerGenerated]
    protected virtual bool PrintMembers(StringBuilder builder)
    {
      return false;
    }

    // Token: 0x06001B4E RID: 6990 RVA: 0x003DAA8C File Offset: 0x003DAA8C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(McpVerification left, McpVerification right)
    {
      return !(left == right);
    }

    // Token: 0x06001B4F RID: 6991 RVA: 0x003DAA98 File Offset: 0x003DAA98
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(McpVerification left, McpVerification right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001B50 RID: 6992 RVA: 0x003DAAAC File Offset: 0x003DAAAC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
    }

    // Token: 0x06001B51 RID: 6993 RVA: 0x003DAAC0 File Offset: 0x003DAAC0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as McpVerification);
    }

    // Token: 0x06001B52 RID: 6994 RVA: 0x003DAAD0 File Offset: 0x003DAAD0
    [NullableContext(2)]
    [CompilerGenerated]
    public virtual bool Equals(McpVerification other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract);
    }

    // Token: 0x06001B54 RID: 6996 RVA: 0x003DAAF0 File Offset: 0x003DAAF0
    [CompilerGenerated]
    protected McpVerification(McpVerification original)
    {
    }

    // Token: 0x06001B55 RID: 6997 RVA: 0x003DAAF8 File Offset: 0x003DAAF8
    protected McpVerification()
    {
    }

    // Token: 0x020005F1 RID: 1521
    [Nullable(0)]
    public sealed class Reachable : McpVerification, IEquatable<McpVerification.Reachable>
    {
      // Token: 0x0600375F RID: 14175 RVA: 0x00449EBC File Offset: 0x00449EBC
      public Reachable(int ToolCount)
      {
        this.ToolCount = ToolCount;
        base..ctor();
      }

      // Token: 0x170009A6 RID: 2470
      // (get) Token: 0x06003760 RID: 14176 RVA: 0x00449ECC File Offset: 0x00449ECC
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(McpVerification.Reachable);
        }
      }

      // Token: 0x170009A7 RID: 2471
      // (get) Token: 0x06003761 RID: 14177 RVA: 0x00449ED8 File Offset: 0x00449ED8
      // (set) Token: 0x06003762 RID: 14178 RVA: 0x00449EE0 File Offset: 0x00449EE0
      public int ToolCount { get; set; }

      // Token: 0x06003763 RID: 14179 RVA: 0x00449EEC File Offset: 0x00449EEC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Reachable");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003764 RID: 14180 RVA: 0x00449F38 File Offset: 0x00449F38
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("ToolCount = ");
        builder.Append(this.ToolCount.ToString());
        return true;
      }

      // Token: 0x06003765 RID: 14181 RVA: 0x00449F84 File Offset: 0x00449F84
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(McpVerification.Reachable left, McpVerification.Reachable right)
      {
        return !(left == right);
      }

      // Token: 0x06003766 RID: 14182 RVA: 0x00449F90 File Offset: 0x00449F90
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(McpVerification.Reachable left, McpVerification.Reachable right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003767 RID: 14183 RVA: 0x00449FA4 File Offset: 0x00449FA4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ToolCount>k__BackingField);
      }

      // Token: 0x06003768 RID: 14184 RVA: 0x00449FC4 File Offset: 0x00449FC4
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as McpVerification.Reachable);
      }

      // Token: 0x06003769 RID: 14185 RVA: 0x00449FD4 File Offset: 0x00449FD4
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(McpVerification other)
      {
        return this.Equals(other);
      }

      // Token: 0x0600376A RID: 14186 RVA: 0x00449FE0 File Offset: 0x00449FE0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(McpVerification.Reachable other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<ToolCount>k__BackingField, other.<ToolCount>k__BackingField));
      }

      // Token: 0x0600376C RID: 14188 RVA: 0x0044A014 File Offset: 0x0044A014
      [CompilerGenerated]
      private Reachable(McpVerification.Reachable original)
        : base(original)
      {
        this.ToolCount = original.<ToolCount>k__BackingField;
      }

      // Token: 0x0600376D RID: 14189 RVA: 0x0044A02C File Offset: 0x0044A02C
      [CompilerGenerated]
      public void Deconstruct(out int ToolCount)
      {
        ToolCount = this.ToolCount;
      }
    }

    // Token: 0x020005F2 RID: 1522
    [Nullable(0)]
    public sealed class Unauthorized : McpVerification, IEquatable<McpVerification.Unauthorized>
    {
      // Token: 0x0600376E RID: 14190 RVA: 0x0044A038 File Offset: 0x0044A038
      public Unauthorized(int StatusCode)
      {
        this.StatusCode = StatusCode;
        base..ctor();
      }

      // Token: 0x170009A8 RID: 2472
      // (get) Token: 0x0600376F RID: 14191 RVA: 0x0044A048 File Offset: 0x0044A048
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(McpVerification.Unauthorized);
        }
      }

      // Token: 0x170009A9 RID: 2473
      // (get) Token: 0x06003770 RID: 14192 RVA: 0x0044A054 File Offset: 0x0044A054
      // (set) Token: 0x06003771 RID: 14193 RVA: 0x0044A05C File Offset: 0x0044A05C
      public int StatusCode { get; set; }

      // Token: 0x06003772 RID: 14194 RVA: 0x0044A068 File Offset: 0x0044A068
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Unauthorized");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003773 RID: 14195 RVA: 0x0044A0B4 File Offset: 0x0044A0B4
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("StatusCode = ");
        builder.Append(this.StatusCode.ToString());
        return true;
      }

      // Token: 0x06003774 RID: 14196 RVA: 0x0044A100 File Offset: 0x0044A100
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(McpVerification.Unauthorized left, McpVerification.Unauthorized right)
      {
        return !(left == right);
      }

      // Token: 0x06003775 RID: 14197 RVA: 0x0044A10C File Offset: 0x0044A10C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(McpVerification.Unauthorized left, McpVerification.Unauthorized right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003776 RID: 14198 RVA: 0x0044A120 File Offset: 0x0044A120
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<StatusCode>k__BackingField);
      }

      // Token: 0x06003777 RID: 14199 RVA: 0x0044A140 File Offset: 0x0044A140
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as McpVerification.Unauthorized);
      }

      // Token: 0x06003778 RID: 14200 RVA: 0x0044A150 File Offset: 0x0044A150
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(McpVerification other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003779 RID: 14201 RVA: 0x0044A15C File Offset: 0x0044A15C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(McpVerification.Unauthorized other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<StatusCode>k__BackingField, other.<StatusCode>k__BackingField));
      }

      // Token: 0x0600377B RID: 14203 RVA: 0x0044A190 File Offset: 0x0044A190
      [CompilerGenerated]
      private Unauthorized(McpVerification.Unauthorized original)
        : base(original)
      {
        this.StatusCode = original.<StatusCode>k__BackingField;
      }

      // Token: 0x0600377C RID: 14204 RVA: 0x0044A1A8 File Offset: 0x0044A1A8
      [CompilerGenerated]
      public void Deconstruct(out int StatusCode)
      {
        StatusCode = this.StatusCode;
      }
    }

    // Token: 0x020005F3 RID: 1523
    [Nullable(0)]
    public sealed class ServerError : McpVerification, IEquatable<McpVerification.ServerError>
    {
      // Token: 0x0600377D RID: 14205 RVA: 0x0044A1B4 File Offset: 0x0044A1B4
      public ServerError(int StatusCode)
      {
        this.StatusCode = StatusCode;
        base..ctor();
      }

      // Token: 0x170009AA RID: 2474
      // (get) Token: 0x0600377E RID: 14206 RVA: 0x0044A1C4 File Offset: 0x0044A1C4
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(McpVerification.ServerError);
        }
      }

      // Token: 0x170009AB RID: 2475
      // (get) Token: 0x0600377F RID: 14207 RVA: 0x0044A1D0 File Offset: 0x0044A1D0
      // (set) Token: 0x06003780 RID: 14208 RVA: 0x0044A1D8 File Offset: 0x0044A1D8
      public int StatusCode { get; set; }

      // Token: 0x06003781 RID: 14209 RVA: 0x0044A1E4 File Offset: 0x0044A1E4
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ServerError");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003782 RID: 14210 RVA: 0x0044A230 File Offset: 0x0044A230
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("StatusCode = ");
        builder.Append(this.StatusCode.ToString());
        return true;
      }

      // Token: 0x06003783 RID: 14211 RVA: 0x0044A27C File Offset: 0x0044A27C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(McpVerification.ServerError left, McpVerification.ServerError right)
      {
        return !(left == right);
      }

      // Token: 0x06003784 RID: 14212 RVA: 0x0044A288 File Offset: 0x0044A288
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(McpVerification.ServerError left, McpVerification.ServerError right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003785 RID: 14213 RVA: 0x0044A29C File Offset: 0x0044A29C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<StatusCode>k__BackingField);
      }

      // Token: 0x06003786 RID: 14214 RVA: 0x0044A2BC File Offset: 0x0044A2BC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as McpVerification.ServerError);
      }

      // Token: 0x06003787 RID: 14215 RVA: 0x0044A2CC File Offset: 0x0044A2CC
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(McpVerification other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003788 RID: 14216 RVA: 0x0044A2D8 File Offset: 0x0044A2D8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(McpVerification.ServerError other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<StatusCode>k__BackingField, other.<StatusCode>k__BackingField));
      }

      // Token: 0x0600378A RID: 14218 RVA: 0x0044A30C File Offset: 0x0044A30C
      [CompilerGenerated]
      private ServerError(McpVerification.ServerError original)
        : base(original)
      {
        this.StatusCode = original.<StatusCode>k__BackingField;
      }

      // Token: 0x0600378B RID: 14219 RVA: 0x0044A324 File Offset: 0x0044A324
      [CompilerGenerated]
      public void Deconstruct(out int StatusCode)
      {
        StatusCode = this.StatusCode;
      }
    }

    // Token: 0x020005F4 RID: 1524
    [Nullable(0)]
    public sealed class Unreachable : McpVerification, IEquatable<McpVerification.Unreachable>
    {
      // Token: 0x170009AC RID: 2476
      // (get) Token: 0x0600378C RID: 14220 RVA: 0x0044A330 File Offset: 0x0044A330
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(McpVerification.Unreachable);
        }
      }

      // Token: 0x0600378D RID: 14221 RVA: 0x0044A33C File Offset: 0x0044A33C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Unreachable");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600378E RID: 14222 RVA: 0x0044A388 File Offset: 0x0044A388
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        return base.PrintMembers(builder);
      }

      // Token: 0x0600378F RID: 14223 RVA: 0x0044A394 File Offset: 0x0044A394
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(McpVerification.Unreachable left, McpVerification.Unreachable right)
      {
        return !(left == right);
      }

      // Token: 0x06003790 RID: 14224 RVA: 0x0044A3A0 File Offset: 0x0044A3A0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(McpVerification.Unreachable left, McpVerification.Unreachable right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003791 RID: 14225 RVA: 0x0044A3B4 File Offset: 0x0044A3B4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      // Token: 0x06003792 RID: 14226 RVA: 0x0044A3BC File Offset: 0x0044A3BC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as McpVerification.Unreachable);
      }

      // Token: 0x06003793 RID: 14227 RVA: 0x0044A3CC File Offset: 0x0044A3CC
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(McpVerification other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003794 RID: 14228 RVA: 0x0044A3D8 File Offset: 0x0044A3D8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(McpVerification.Unreachable other)
      {
        return this == other || base.Equals(other);
      }

      // Token: 0x06003796 RID: 14230 RVA: 0x0044A3F0 File Offset: 0x0044A3F0
      [CompilerGenerated]
      private Unreachable(McpVerification.Unreachable original)
        : base(original)
      {
      }

      // Token: 0x06003797 RID: 14231 RVA: 0x0044A3FC File Offset: 0x0044A3FC
      public Unreachable()
      {
      }
    }
  }
}
