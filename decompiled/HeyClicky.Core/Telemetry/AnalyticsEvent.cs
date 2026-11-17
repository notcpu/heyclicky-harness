using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Telemetry
{
  // Token: 0x020000A9 RID: 169
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AnalyticsEvent : IEquatable<AnalyticsEvent>
  {
    // Token: 0x06000A58 RID: 2648 RVA: 0x00374684 File Offset: 0x00374684
    public AnalyticsEvent(string Event, string DistinctId, JsonObject Properties, DateTimeOffset Timestamp, Guid Uuid)
    {
      this.Event = Event;
      this.DistinctId = DistinctId;
      this.Properties = Properties;
      this.Timestamp = Timestamp;
      this.Uuid = Uuid;
      base..ctor();
    }

    // Token: 0x1700023D RID: 573
    // (get) Token: 0x06000A59 RID: 2649 RVA: 0x003746B4 File Offset: 0x003746B4
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AnalyticsEvent);
      }
    }

    // Token: 0x1700023E RID: 574
    // (get) Token: 0x06000A5A RID: 2650 RVA: 0x003746C0 File Offset: 0x003746C0
    // (set) Token: 0x06000A5B RID: 2651 RVA: 0x003746C8 File Offset: 0x003746C8
    public string Event { get; set; }

    // Token: 0x1700023F RID: 575
    // (get) Token: 0x06000A5C RID: 2652 RVA: 0x003746D4 File Offset: 0x003746D4
    // (set) Token: 0x06000A5D RID: 2653 RVA: 0x003746DC File Offset: 0x003746DC
    public string DistinctId { get; set; }

    // Token: 0x17000240 RID: 576
    // (get) Token: 0x06000A5E RID: 2654 RVA: 0x003746E8 File Offset: 0x003746E8
    // (set) Token: 0x06000A5F RID: 2655 RVA: 0x003746F0 File Offset: 0x003746F0
    public JsonObject Properties { get; set; }

    // Token: 0x17000241 RID: 577
    // (get) Token: 0x06000A60 RID: 2656 RVA: 0x003746FC File Offset: 0x003746FC
    // (set) Token: 0x06000A61 RID: 2657 RVA: 0x00374704 File Offset: 0x00374704
    public DateTimeOffset Timestamp { get; set; }

    // Token: 0x17000242 RID: 578
    // (get) Token: 0x06000A62 RID: 2658 RVA: 0x00374710 File Offset: 0x00374710
    // (set) Token: 0x06000A63 RID: 2659 RVA: 0x00374718 File Offset: 0x00374718
    public Guid Uuid { get; set; }

    // Token: 0x06000A64 RID: 2660 RVA: 0x00374724 File Offset: 0x00374724
    public JsonObject ToPostHogJson()
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["event"] = this.Event;
      jsonObject["distinct_id"] = this.DistinctId;
      jsonObject["properties"] = this.Properties.DeepClone();
      jsonObject["timestamp"] = this.Timestamp.UtcDateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture);
      jsonObject["uuid"] = this.Uuid.ToString();
      return jsonObject;
    }

    // Token: 0x06000A65 RID: 2661 RVA: 0x003747D8 File Offset: 0x003747D8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AnalyticsEvent");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000A66 RID: 2662 RVA: 0x00374824 File Offset: 0x00374824
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Event = ");
      builder.Append(this.Event);
      builder.Append(", DistinctId = ");
      builder.Append(this.DistinctId);
      builder.Append(", Properties = ");
      builder.Append(this.Properties);
      builder.Append(", Timestamp = ");
      builder.Append(this.Timestamp.ToString());
      builder.Append(", Uuid = ");
      builder.Append(this.Uuid.ToString());
      return true;
    }

    // Token: 0x06000A67 RID: 2663 RVA: 0x003748D0 File Offset: 0x003748D0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AnalyticsEvent left, AnalyticsEvent right)
    {
      return !(left == right);
    }

    // Token: 0x06000A68 RID: 2664 RVA: 0x003748DC File Offset: 0x003748DC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AnalyticsEvent left, AnalyticsEvent right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000A69 RID: 2665 RVA: 0x003748F0 File Offset: 0x003748F0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Event>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DistinctId>k__BackingField)) * -1521134295 + EqualityComparer<JsonObject>.Default.GetHashCode(this.<Properties>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<Timestamp>k__BackingField)) * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(this.<Uuid>k__BackingField);
    }

    // Token: 0x06000A6A RID: 2666 RVA: 0x00374980 File Offset: 0x00374980
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AnalyticsEvent);
    }

    // Token: 0x06000A6B RID: 2667 RVA: 0x00374990 File Offset: 0x00374990
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AnalyticsEvent other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Event>k__BackingField, other.<Event>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DistinctId>k__BackingField, other.<DistinctId>k__BackingField) && EqualityComparer<JsonObject>.Default.Equals(this.<Properties>k__BackingField, other.<Properties>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<Timestamp>k__BackingField, other.<Timestamp>k__BackingField) && EqualityComparer<Guid>.Default.Equals(this.<Uuid>k__BackingField, other.<Uuid>k__BackingField));
    }

    // Token: 0x06000A6D RID: 2669 RVA: 0x00374A40 File Offset: 0x00374A40
    [CompilerGenerated]
    private AnalyticsEvent(AnalyticsEvent original)
    {
      this.Event = original.<Event>k__BackingField;
      this.DistinctId = original.<DistinctId>k__BackingField;
      this.Properties = original.<Properties>k__BackingField;
      this.Timestamp = original.<Timestamp>k__BackingField;
      this.Uuid = original.<Uuid>k__BackingField;
    }

    // Token: 0x06000A6E RID: 2670 RVA: 0x00374A90 File Offset: 0x00374A90
    [CompilerGenerated]
    public void Deconstruct(out string Event, out string DistinctId, out JsonObject Properties, out DateTimeOffset Timestamp, out Guid Uuid)
    {
      Event = this.Event;
      DistinctId = this.DistinctId;
      Properties = this.Properties;
      Timestamp = this.Timestamp;
      Uuid = this.Uuid;
    }
  }
}
