using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000E0 RID: 224
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class UpcomingCalendarEvent : IEquatable<UpcomingCalendarEvent>
  {
    // Token: 0x06000E81 RID: 3713 RVA: 0x0038B4B4 File Offset: 0x0038B4B4
    public UpcomingCalendarEvent([Nullable(1)] string Id, [Nullable(1)] string Title, DateTimeOffset StartsAt, DateTimeOffset? EndsAt, bool IsAllDay, Uri MeetingUrl, string Location, Uri HtmlLink)
    {
      this.Id = Id;
      this.Title = Title;
      this.StartsAt = StartsAt;
      this.EndsAt = EndsAt;
      this.IsAllDay = IsAllDay;
      this.MeetingUrl = MeetingUrl;
      this.Location = Location;
      this.HtmlLink = HtmlLink;
      base..ctor();
    }

    // Token: 0x17000345 RID: 837
    // (get) Token: 0x06000E82 RID: 3714 RVA: 0x0038B504 File Offset: 0x0038B504
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(UpcomingCalendarEvent);
      }
    }

    // Token: 0x17000346 RID: 838
    // (get) Token: 0x06000E83 RID: 3715 RVA: 0x0038B510 File Offset: 0x0038B510
    // (set) Token: 0x06000E84 RID: 3716 RVA: 0x0038B518 File Offset: 0x0038B518
    [Nullable(1)]
    public string Id
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000347 RID: 839
    // (get) Token: 0x06000E85 RID: 3717 RVA: 0x0038B524 File Offset: 0x0038B524
    // (set) Token: 0x06000E86 RID: 3718 RVA: 0x0038B52C File Offset: 0x0038B52C
    [Nullable(1)]
    public string Title
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000348 RID: 840
    // (get) Token: 0x06000E87 RID: 3719 RVA: 0x0038B538 File Offset: 0x0038B538
    // (set) Token: 0x06000E88 RID: 3720 RVA: 0x0038B540 File Offset: 0x0038B540
    public DateTimeOffset StartsAt { get; set; }

    // Token: 0x17000349 RID: 841
    // (get) Token: 0x06000E89 RID: 3721 RVA: 0x0038B54C File Offset: 0x0038B54C
    // (set) Token: 0x06000E8A RID: 3722 RVA: 0x0038B554 File Offset: 0x0038B554
    public DateTimeOffset? EndsAt { get; set; }

    // Token: 0x1700034A RID: 842
    // (get) Token: 0x06000E8B RID: 3723 RVA: 0x0038B560 File Offset: 0x0038B560
    // (set) Token: 0x06000E8C RID: 3724 RVA: 0x0038B568 File Offset: 0x0038B568
    public bool IsAllDay { get; set; }

    // Token: 0x1700034B RID: 843
    // (get) Token: 0x06000E8D RID: 3725 RVA: 0x0038B574 File Offset: 0x0038B574
    // (set) Token: 0x06000E8E RID: 3726 RVA: 0x0038B57C File Offset: 0x0038B57C
    public Uri MeetingUrl { get; set; }

    // Token: 0x1700034C RID: 844
    // (get) Token: 0x06000E8F RID: 3727 RVA: 0x0038B588 File Offset: 0x0038B588
    // (set) Token: 0x06000E90 RID: 3728 RVA: 0x0038B590 File Offset: 0x0038B590
    public string Location { get; set; }

    // Token: 0x1700034D RID: 845
    // (get) Token: 0x06000E91 RID: 3729 RVA: 0x0038B59C File Offset: 0x0038B59C
    // (set) Token: 0x06000E92 RID: 3730 RVA: 0x0038B5A4 File Offset: 0x0038B5A4
    public Uri HtmlLink { get; set; }

    // Token: 0x1700034E RID: 846
    // (get) Token: 0x06000E93 RID: 3731 RVA: 0x0038B5B0 File Offset: 0x0038B5B0
    public Uri DirectionsUrl
    {
      get
      {
        if (!string.IsNullOrWhiteSpace(this.Location))
        {
          return new Uri("https://www.google.com/maps/search/?api=1&query=" + Uri.EscapeDataString(this.Location));
        }
        return null;
      }
    }

    // Token: 0x1700034F RID: 847
    // (get) Token: 0x06000E94 RID: 3732 RVA: 0x0038B5DC File Offset: 0x0038B5DC
    public Uri PrimaryActionUrl
    {
      get
      {
        return this.MeetingUrl ?? this.DirectionsUrl;
      }
    }

    // Token: 0x17000350 RID: 848
    // (get) Token: 0x06000E95 RID: 3733 RVA: 0x0038B5F0 File Offset: 0x0038B5F0
    public bool IsKnownMeetingProvider
    {
      get
      {
        Uri meetingUrl = this.MeetingUrl;
        string text = ((meetingUrl != null) ? meetingUrl.Host.ToLowerInvariant() : null);
        return text != null && (text == "meet.google.com" || text == "zoom.us" || text.EndsWith(".zoom.us") || text == "zoom.com" || text.EndsWith(".zoom.com") || text == "teams.microsoft.com" || text == "teams.live.com");
      }
    }

    // Token: 0x17000351 RID: 849
    // (get) Token: 0x06000E96 RID: 3734 RVA: 0x0038B678 File Offset: 0x0038B678
    public MeetingActionKind? PrimaryActionKind
    {
      get
      {
        if (this.MeetingUrl != null)
        {
          return new MeetingActionKind?(this.IsKnownMeetingProvider ? MeetingActionKind.Join : MeetingActionKind.Open);
        }
        if (this.DirectionsUrl == null)
        {
          return null;
        }
        return new MeetingActionKind?(MeetingActionKind.Directions);
      }
    }

    // Token: 0x17000352 RID: 850
    // (get) Token: 0x06000E97 RID: 3735 RVA: 0x0038B6B8 File Offset: 0x0038B6B8
    [Nullable(1)]
    public string AnnouncementKey
    {
      [NullableContext(1)]
      get
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
        defaultInterpolatedStringHandler.AppendFormatted(this.Id);
        defaultInterpolatedStringHandler.AppendLiteral("|");
        defaultInterpolatedStringHandler.AppendFormatted<long>(this.StartsAt.ToUnixTimeSeconds());
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
    }

    // Token: 0x06000E98 RID: 3736 RVA: 0x0038B704 File Offset: 0x0038B704
    public bool IsOngoing(DateTimeOffset now)
    {
      return !this.IsAllDay && this.StartsAt <= now && (this.EndsAt ?? this.StartsAt.AddHours(1.0)) > now;
    }

    // Token: 0x06000E99 RID: 3737 RVA: 0x0038B760 File Offset: 0x0038B760
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("UpcomingCalendarEvent");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000E9A RID: 3738 RVA: 0x0038B7AC File Offset: 0x0038B7AC
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", StartsAt = ");
      builder.Append(this.StartsAt.ToString());
      builder.Append(", EndsAt = ");
      builder.Append(this.EndsAt.ToString());
      builder.Append(", IsAllDay = ");
      builder.Append(this.IsAllDay.ToString());
      builder.Append(", MeetingUrl = ");
      builder.Append(this.MeetingUrl);
      builder.Append(", Location = ");
      builder.Append(this.Location);
      builder.Append(", HtmlLink = ");
      builder.Append(this.HtmlLink);
      builder.Append(", DirectionsUrl = ");
      builder.Append(this.DirectionsUrl);
      builder.Append(", PrimaryActionUrl = ");
      builder.Append(this.PrimaryActionUrl);
      builder.Append(", IsKnownMeetingProvider = ");
      builder.Append(this.IsKnownMeetingProvider.ToString());
      builder.Append(", PrimaryActionKind = ");
      builder.Append(this.PrimaryActionKind.ToString());
      builder.Append(", AnnouncementKey = ");
      builder.Append(this.AnnouncementKey);
      return true;
    }

    // Token: 0x06000E9B RID: 3739 RVA: 0x0038B94C File Offset: 0x0038B94C
    [CompilerGenerated]
    public static bool operator !=(UpcomingCalendarEvent left, UpcomingCalendarEvent right)
    {
      return !(left == right);
    }

    // Token: 0x06000E9C RID: 3740 RVA: 0x0038B958 File Offset: 0x0038B958
    [CompilerGenerated]
    public static bool operator ==(UpcomingCalendarEvent left, UpcomingCalendarEvent right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000E9D RID: 3741 RVA: 0x0038B96C File Offset: 0x0038B96C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<StartsAt>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<EndsAt>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsAllDay>k__BackingField)) * -1521134295 + EqualityComparer<Uri>.Default.GetHashCode(this.<MeetingUrl>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Location>k__BackingField)) * -1521134295 + EqualityComparer<Uri>.Default.GetHashCode(this.<HtmlLink>k__BackingField);
    }

    // Token: 0x06000E9E RID: 3742 RVA: 0x0038BA44 File Offset: 0x0038BA44
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as UpcomingCalendarEvent);
    }

    // Token: 0x06000E9F RID: 3743 RVA: 0x0038BA54 File Offset: 0x0038BA54
    [CompilerGenerated]
    public bool Equals(UpcomingCalendarEvent other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<StartsAt>k__BackingField, other.<StartsAt>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<EndsAt>k__BackingField, other.<EndsAt>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsAllDay>k__BackingField, other.<IsAllDay>k__BackingField) && EqualityComparer<Uri>.Default.Equals(this.<MeetingUrl>k__BackingField, other.<MeetingUrl>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Location>k__BackingField, other.<Location>k__BackingField) && EqualityComparer<Uri>.Default.Equals(this.<HtmlLink>k__BackingField, other.<HtmlLink>k__BackingField));
    }

    // Token: 0x06000EA1 RID: 3745 RVA: 0x0038BB54 File Offset: 0x0038BB54
    [CompilerGenerated]
    private UpcomingCalendarEvent([Nullable(1)] UpcomingCalendarEvent original)
    {
      this.Id = original.<Id>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.StartsAt = original.<StartsAt>k__BackingField;
      this.EndsAt = original.<EndsAt>k__BackingField;
      this.IsAllDay = original.<IsAllDay>k__BackingField;
      this.MeetingUrl = original.<MeetingUrl>k__BackingField;
      this.Location = original.<Location>k__BackingField;
      this.HtmlLink = original.<HtmlLink>k__BackingField;
    }

    // Token: 0x06000EA2 RID: 3746 RVA: 0x0038BBC8 File Offset: 0x0038BBC8
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out string Id, [Nullable(1)] out string Title, out DateTimeOffset StartsAt, out DateTimeOffset? EndsAt, out bool IsAllDay, out Uri MeetingUrl, out string Location, out Uri HtmlLink)
    {
      Id = this.Id;
      Title = this.Title;
      StartsAt = this.StartsAt;
      EndsAt = this.EndsAt;
      IsAllDay = this.IsAllDay;
      MeetingUrl = this.MeetingUrl;
      Location = this.Location;
      HtmlLink = this.HtmlLink;
    }
  }
}
