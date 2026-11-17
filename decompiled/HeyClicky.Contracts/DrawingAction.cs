using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace HeyClicky
{
  // Token: 0x0200001F RID: 31
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DrawingAction : IEquatable<DrawingAction>
  {
    // Token: 0x06000152 RID: 338 RVA: 0x0001EC74 File Offset: 0x0001EC74
    public DrawingAction(string Kind, JsonElement Payload)
    {
      this.Kind = Kind;
      this.Payload = Payload;
      base..ctor();
    }

    // Token: 0x1700005B RID: 91
    // (get) Token: 0x06000153 RID: 339 RVA: 0x0001EC8C File Offset: 0x0001EC8C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(DrawingAction);
      }
    }

    // Token: 0x1700005C RID: 92
    // (get) Token: 0x06000154 RID: 340 RVA: 0x0001EC98 File Offset: 0x0001EC98
    // (set) Token: 0x06000155 RID: 341 RVA: 0x0001ECA0 File Offset: 0x0001ECA0
    public string Kind { get; set; }

    // Token: 0x1700005D RID: 93
    // (get) Token: 0x06000156 RID: 342 RVA: 0x0001ECAC File Offset: 0x0001ECAC
    // (set) Token: 0x06000157 RID: 343 RVA: 0x0001ECB4 File Offset: 0x0001ECB4
    public JsonElement Payload { get; set; }

    // Token: 0x06000158 RID: 344 RVA: 0x0001ECC0 File Offset: 0x0001ECC0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DrawingAction");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000159 RID: 345 RVA: 0x0001ED0C File Offset: 0x0001ED0C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Kind = ");
      builder.Append(this.Kind);
      builder.Append(", Payload = ");
      builder.Append(this.Payload.ToString());
      return true;
    }

    // Token: 0x0600015A RID: 346 RVA: 0x0001ED60 File Offset: 0x0001ED60
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(DrawingAction left, DrawingAction right)
    {
      return !(left == right);
    }

    // Token: 0x0600015B RID: 347 RVA: 0x0001ED6C File Offset: 0x0001ED6C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(DrawingAction left, DrawingAction right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600015C RID: 348 RVA: 0x0001ED80 File Offset: 0x0001ED80
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<JsonElement>.Default.GetHashCode(this.<Payload>k__BackingField);
    }

    // Token: 0x0600015D RID: 349 RVA: 0x0001EDC0 File Offset: 0x0001EDC0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as DrawingAction);
    }

    // Token: 0x0600015E RID: 350 RVA: 0x0001EDD0 File Offset: 0x0001EDD0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(DrawingAction other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<JsonElement>.Default.Equals(this.<Payload>k__BackingField, other.<Payload>k__BackingField));
    }

    // Token: 0x06000160 RID: 352 RVA: 0x0001EE34 File Offset: 0x0001EE34
    [CompilerGenerated]
    private DrawingAction(DrawingAction original)
    {
      this.Kind = original.<Kind>k__BackingField;
      this.Payload = original.<Payload>k__BackingField;
    }

    // Token: 0x06000161 RID: 353 RVA: 0x0001EE54 File Offset: 0x0001EE54
    [CompilerGenerated]
    public void Deconstruct(out string Kind, out JsonElement Payload)
    {
      Kind = this.Kind;
      Payload = this.Payload;
    }
  }
}
