using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Worker;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000E2 RID: 226
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class UpcomingMeetingsClient
  {
    // Token: 0x17000356 RID: 854
    // (get) Token: 0x06000EB3 RID: 3763 RVA: 0x0038BE18 File Offset: 0x0038BE18
    // (set) Token: 0x06000EB4 RID: 3764 RVA: 0x0038BE20 File Offset: 0x0038BE20
    public Func<byte[]> TestResponse { get; set; }

    // Token: 0x06000EB5 RID: 3765 RVA: 0x0038BE2C File Offset: 0x0038BE2C
    [NullableContext(1)]
    public UpcomingMeetingsClient(WorkerClient worker)
    {
      this.worker = worker;
    }

    // Token: 0x06000EB6 RID: 3766 RVA: 0x0038BE3C File Offset: 0x0038BE3C
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<UpcomingCalendarFetch> FetchAsync(CancellationToken ct = default(CancellationToken))
    {
      UpcomingCalendarFetch upcomingCalendarFetch;
      try
      {
        Func<byte[]> testResponse = this.TestResponse;
        byte[] array = ((testResponse != null) ? testResponse() : null);
        if (array == null)
        {
          array = await this.worker.SendRawAsync(HttpMethod.Get, "/me/calendar/upcoming", null, true, ct, new TimeSpan?(TimeSpan.FromSeconds(20L)), null).ConfigureAwait(false);
        }
        byte[] array2 = array;
        upcomingCalendarFetch = ((array2 == null) ? null : UpcomingMeetingsClient.Parse(array2));
      }
      catch (Exception ex)
      {
        Log.Info("Meetings", "upcoming-events fetch failed: " + ex.Message);
        upcomingCalendarFetch = null;
      }
      return upcomingCalendarFetch;
    }

    // Token: 0x06000EB7 RID: 3767 RVA: 0x0038BE88 File Offset: 0x0038BE88
    [NullableContext(1)]
    [return: Nullable(2)]
    public static UpcomingCalendarFetch Parse(byte[] bytes)
    {
      JsonObject jsonObject = JsonNode.Parse(bytes, null, default(JsonDocumentOptions)) as JsonObject;
      if (jsonObject == null)
      {
        return null;
      }
      List<UpcomingCalendarEvent> list = new List<UpcomingCalendarEvent>();
      JsonArray jsonArray;
      if ((jsonArray = jsonObject["events"] as JsonArray) == null)
      {
        jsonArray = new JsonArray(null);
      }
      foreach (JsonObject jsonObject2 in jsonArray.OfType<JsonObject>())
      {
        UpcomingMeetingsClient.<>c__DisplayClass7_0 CS$<>8__locals1;
        CS$<>8__locals1.e = jsonObject2;
        string text = UpcomingMeetingsClient.<Parse>g__Str|7_0("starts_at", ref CS$<>8__locals1);
        DateTimeOffset dateTimeOffset;
        if (text != null && DateTimeOffset.TryParse(text, out dateTimeOffset))
        {
          string text2 = UpcomingMeetingsClient.<Parse>g__Str|7_0("ends_at", ref CS$<>8__locals1);
          DateTimeOffset dateTimeOffset3;
          DateTimeOffset? dateTimeOffset2 = ((text2 != null && DateTimeOffset.TryParse(text2, out dateTimeOffset3)) ? new DateTimeOffset?(dateTimeOffset3) : null);
          List<UpcomingCalendarEvent> list2 = list;
          string text3 = UpcomingMeetingsClient.<Parse>g__Str|7_0("id", ref CS$<>8__locals1) ?? text;
          string text4 = UpcomingMeetingsClient.<Parse>g__Str|7_0("title", ref CS$<>8__locals1) ?? "Meeting";
          DateTimeOffset dateTimeOffset4 = dateTimeOffset;
          DateTimeOffset? dateTimeOffset5 = dateTimeOffset2;
          JsonValue jsonValue = CS$<>8__locals1.e["is_all_day"] as JsonValue;
          bool flag2;
          bool flag = jsonValue != null && jsonValue.TryGetValue<bool>(out flag2) && flag2;
          Uri uri = UpcomingMeetingsClient.<Parse>g__AsUri|7_1(UpcomingMeetingsClient.<Parse>g__Str|7_0("meeting_url", ref CS$<>8__locals1));
          string text5 = UpcomingMeetingsClient.<Parse>g__Str|7_0("location", ref CS$<>8__locals1);
          list2.Add(new UpcomingCalendarEvent(text3, text4, dateTimeOffset4, dateTimeOffset5, flag, uri, (text5 != null && text5.Length > 0) ? text5 : null, UpcomingMeetingsClient.<Parse>g__AsUri|7_1(UpcomingMeetingsClient.<Parse>g__Str|7_0("html_link", ref CS$<>8__locals1))));
        }
      }
      JsonValue jsonValue2 = jsonObject["connected"] as JsonValue;
      bool flag3;
      return new UpcomingCalendarFetch(jsonValue2 != null && jsonValue2.TryGetValue<bool>(out flag3) && flag3, list);
    }

    // Token: 0x06000EB8 RID: 3768 RVA: 0x0038C060 File Offset: 0x0038C060
    [NullableContext(1)]
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <Parse>g__Str|7_0(string k, ref UpcomingMeetingsClient.<>c__DisplayClass7_0 A_1)
    {
      JsonValue jsonValue = A_1.e[k] as JsonValue;
      string text;
      if (jsonValue == null || !jsonValue.TryGetValue<string>(out text))
      {
        return null;
      }
      return text;
    }

    // Token: 0x06000EB9 RID: 3769 RVA: 0x0038C090 File Offset: 0x0038C090
    [CompilerGenerated]
    internal static Uri <Parse>g__AsUri|7_1(string s)
    {
      Uri uri;
      bool flag = Uri.TryCreate(s, UriKind.Absolute, out uri);
      if (flag)
      {
        string scheme = uri.Scheme;
        bool flag2 = scheme == "http" || scheme == "https";
        flag = flag2;
      }
      if (!flag)
      {
        return null;
      }
      return uri;
    }

    // Token: 0x0400064F RID: 1615
    [Nullable(1)]
    private readonly WorkerClient worker;
  }
}
