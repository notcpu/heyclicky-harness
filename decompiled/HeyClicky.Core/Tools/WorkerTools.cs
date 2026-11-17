using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions.Generated;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.Core.Tools
{
  // Token: 0x020000A7 RID: 167
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class WorkerTools
  {
    // Token: 0x06000A4A RID: 2634 RVA: 0x003740C0 File Offset: 0x003740C0
    public WorkerTools(WorkerPost post, Func<string> timeZone, Func<string, bool> openUri, Func<bool> spotifyAppInstalled)
    {
      this.post = post;
      this.timeZone = timeZone;
      this.openUri = openUri;
      this.spotifyAppInstalled = spotifyAppInstalled;
    }

    // Token: 0x06000A4B RID: 2635 RVA: 0x003740E8 File Offset: 0x003740E8
    public Task<string> RunAsync(string toolName, ToolArgs args, CancellationToken ct = default(CancellationToken))
    {
      Task<string> task;
      if (!(toolName == "google_calendar_find_events"))
      {
        if (!(toolName == "google_calendar_create_event"))
        {
          if (!(toolName == "save_memory"))
          {
            if (!(toolName == "play_on_spotify"))
            {
              JsonObject jsonObject = new JsonObject(null);
              jsonObject["error"] = "not_implemented";
              jsonObject["tool"] = toolName;
              task = Task.FromResult<string>(ToolResults.Json(jsonObject));
            }
            else
            {
              task = this.PlayOnSpotifyAsync(args, ct);
            }
          }
          else
          {
            task = this.RouteAsync("/me/memory/save", args.Raw, ct);
          }
        }
        else
        {
          task = this.RouteAsync("/agent/realtime/google-calendar/events/create", this.WithTimeZone(args), ct);
        }
      }
      else
      {
        task = this.RouteAsync("/agent/realtime/google-calendar/events", this.WithTimeZone(args), ct);
      }
      return task;
    }

    // Token: 0x06000A4C RID: 2636 RVA: 0x003741B8 File Offset: 0x003741B8
    private string WithTimeZone(ToolArgs args)
    {
      JsonObject jsonObject = (JsonObject)args.Json.DeepClone();
      if (jsonObject["timeZone"] == null)
      {
        jsonObject["timeZone"] = this.timeZone();
      }
      return jsonObject.ToJsonString(null);
    }

    // Token: 0x06000A4D RID: 2637 RVA: 0x00374208 File Offset: 0x00374208
    private async Task<string> RouteAsync(string path, string body, CancellationToken ct)
    {
      ValueTuple<int, string>? valueTuple = await this.post(path, body, ct).ConfigureAwait(false);
      string text;
      if (valueTuple != null)
      {
        ValueTuple<int, string> valueOrDefault = valueTuple.GetValueOrDefault();
        int item = valueOrDefault.Item1;
        if (item < 200 || item >= 300)
        {
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["error"] = "upstream";
          jsonObject["status"] = valueOrDefault.Item1;
          jsonObject["body"] = valueOrDefault.Item2;
          text = ToolResults.Json(jsonObject);
        }
        else
        {
          text = (string.IsNullOrWhiteSpace(valueOrDefault.Item2) ? "{}" : valueOrDefault.Item2);
        }
      }
      else
      {
        text = "{\"error\":\"not_signed_in\"}";
      }
      return text;
    }

    // Token: 0x06000A4E RID: 2638 RVA: 0x00374264 File Offset: 0x00374264
    private async Task<string> PlayOnSpotifyAsync(ToolArgs args, CancellationToken ct)
    {
      string query = WorkerTools.SearchQuery(args);
      string text = args.Str(new string[] { "spotifyURI", "spotifyUri", "uri", "url" });
      string text2 = ((text != null) ? WorkerTools.NormalizedTrackUri(text) : null);
      Log.Info("Tools", "play_on_spotify query=" + (query ?? "-") + " uri=" + (text2 ?? "-"));
      WorkerTools.Track track;
      if (text2 != null)
      {
        track = new WorkerTools.Track(text2, null, null);
      }
      else
      {
        if (query == null)
        {
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["status"] = "invalid_request";
          jsonObject["summary"] = "No song or artist to play. Ask the user what they want to hear on Spotify.";
          jsonObject["toolName"] = "play_on_spotify";
          string text3 = "result";
          JsonObject jsonObject2 = new JsonObject(null);
          jsonObject2["played"] = false;
          jsonObject2["opened"] = false;
          jsonObject[text3] = jsonObject2;
          return ToolResults.Json(jsonObject);
        }
        object obj = await this.SearchAsync(query, ct).ConfigureAwait(false);
        WorkerTools.Track item = obj.Item1;
        string item2 = obj.Item2;
        string item3 = obj.Item3;
        if (item == null)
        {
          JsonObject jsonObject3 = new JsonObject(null);
          jsonObject3["status"] = item2;
          jsonObject3["summary"] = item3;
          jsonObject3["toolName"] = "play_on_spotify";
          string text4 = "result";
          JsonObject jsonObject4 = new JsonObject(null);
          jsonObject4["played"] = false;
          jsonObject4["opened"] = false;
          jsonObject4["query"] = query;
          jsonObject3[text4] = jsonObject4;
          return ToolResults.Json(jsonObject3);
        }
        track = item;
      }
      bool flag;
      string text5;
      if (this.spotifyAppInstalled())
      {
        flag = this.openUri(track.Uri);
        text5 = "url_scheme";
      }
      else
      {
        Func<string, bool> func = this.openUri;
        string text6 = "https://open.spotify.com/track/";
        string uri = track.Uri;
        int length = "spotify:track:".Length;
        flag = func(text6 + uri.Substring(length, uri.Length - length));
        text5 = "web_player";
      }
      string text7 = "Tools";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 3);
      defaultInterpolatedStringHandler.AppendLiteral("play_on_spotify ");
      defaultInterpolatedStringHandler.AppendFormatted(text5);
      defaultInterpolatedStringHandler.AppendLiteral(" opened=");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
      defaultInterpolatedStringHandler.AppendLiteral(" uri=");
      defaultInterpolatedStringHandler.AppendFormatted(track.Uri);
      Log.Info(text7, defaultInterpolatedStringHandler.ToStringAndClear());
      string text9;
      if (!flag)
      {
        JsonObject jsonObject5 = new JsonObject(null);
        jsonObject5["status"] = "failed";
        jsonObject5["summary"] = "Could not play or open the Spotify track.";
        jsonObject5["toolName"] = "play_on_spotify";
        string text8 = "result";
        JsonObject jsonObject6 = new JsonObject(null);
        jsonObject6["spotifyURI"] = track.Uri;
        jsonObject6["played"] = false;
        jsonObject6["opened"] = false;
        jsonObject5[text8] = jsonObject6;
        text9 = ToolResults.Json(jsonObject5);
      }
      else
      {
        JsonObject jsonObject7 = new JsonObject(null);
        jsonObject7["spotifyURI"] = track.Uri;
        jsonObject7["played"] = text5 == "url_scheme";
        jsonObject7["via"] = text5;
        JsonObject jsonObject8 = jsonObject7;
        string name = track.Name;
        if (name != null)
        {
          jsonObject8["trackName"] = name;
        }
        string artists = track.Artists;
        if (artists != null)
        {
          jsonObject8["artists"] = artists;
        }
        if (query != null)
        {
          jsonObject8["query"] = query;
        }
        string text10;
        if (track.Name != null)
        {
          if (track.Artists != null)
          {
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(28, 2);
            defaultInterpolatedStringHandler.AppendLiteral("Now playing ");
            defaultInterpolatedStringHandler.AppendFormatted(track.Name);
            defaultInterpolatedStringHandler.AppendLiteral(" by ");
            defaultInterpolatedStringHandler.AppendFormatted(track.Artists);
            defaultInterpolatedStringHandler.AppendLiteral(" on Spotify.");
            text10 = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          else
          {
            text10 = "Now playing " + track.Name + " on Spotify.";
          }
        }
        else
        {
          text10 = "Started Spotify playback.";
        }
        string text11 = text10;
        if (text5 == "web_player")
        {
          text11 = ((track.Name == null) ? "Opened the track" : ("Opened " + track.Name)) + " in Spotify's web player (the Spotify app isn't installed on this PC).";
        }
        JsonObject jsonObject9 = new JsonObject(null);
        jsonObject9["status"] = "completed";
        jsonObject9["summary"] = text11;
        jsonObject9["toolName"] = "play_on_spotify";
        jsonObject9["result"] = jsonObject8;
        text9 = ToolResults.Json(jsonObject9);
      }
      return text9;
    }

    // Token: 0x06000A4F RID: 2639 RVA: 0x003742B8 File Offset: 0x003742B8
    [return: TupleElementNames(new string[] { "Track", "Status", "Summary" })]
    [return: Nullable(new byte[] { 1, 0, 2, 1, 1 })]
    private async Task<ValueTuple<WorkerTools.Track, string, string>> SearchAsync(string query, CancellationToken ct)
    {
      ValueTuple<int, string>? valueTuple;
      try
      {
        WorkerPost workerPost = this.post;
        string text = "/agent/realtime/spotify/search";
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["query"] = query;
        valueTuple = await workerPost(text, jsonObject.ToJsonString(null), ct).ConfigureAwait(false);
      }
      catch (Exception ex) when (!(ex is OperationCanceledException))
      {
        Log.Warn("Tools", "spotify search: " + ex.Message);
        return new ValueTuple<WorkerTools.Track, string, string>(null, "error", "Couldn't reach Spotify right now. Ask the user to try again in a moment.");
      }
      ValueTuple<WorkerTools.Track, string, string> valueTuple2;
      if (valueTuple != null)
      {
        ValueTuple<int, string> valueOrDefault = valueTuple.GetValueOrDefault();
        int item = valueOrDefault.Item1;
        if (item < 200 || item >= 300)
        {
          valueTuple2 = new ValueTuple<WorkerTools.Track, string, string>(null, "error", "Couldn't search Spotify right now. Ask the user to try again in a moment.");
        }
        else
        {
          JsonObject jsonObject2;
          try
          {
            jsonObject2 = JsonNode.Parse(valueOrDefault.Item2, null, default(JsonDocumentOptions)) as JsonObject;
          }
          catch (JsonException)
          {
            jsonObject2 = null;
          }
          string text2 = ((string)((jsonObject2 != null) ? jsonObject2["status"] : null)) ?? "error";
          if (text2 == "ok")
          {
            JsonObject jsonObject3 = ((jsonObject2 != null) ? jsonObject2["track"] : null) as JsonObject;
            if (jsonObject3 != null)
            {
              string text3 = (string)jsonObject3["uri"];
              if (text3 != null)
              {
                string text4 = WorkerTools.NormalizedTrackUri(text3);
                if (text4 != null)
                {
                  return new ValueTuple<WorkerTools.Track, string, string>(new WorkerTools.Track(text4, WorkerTools.NonEmpty((string)jsonObject3["name"]), WorkerTools.NonEmpty((string)jsonObject3["artists"])), "ok", "");
                }
              }
            }
          }
          valueTuple2 = new ValueTuple<WorkerTools.Track, string, string>(null, text2, WorkerTools.NonEmpty((string)((jsonObject2 != null) ? jsonObject2["message"] : null)) ?? "Couldn't find that on Spotify. Ask the user to try a different song or artist.");
        }
      }
      else
      {
        valueTuple2 = new ValueTuple<WorkerTools.Track, string, string>(null, "not_signed_in", "Tell the user they need to be signed in to HeyClicky to play music on Spotify.");
      }
      return valueTuple2;
    }

    // Token: 0x06000A50 RID: 2640 RVA: 0x0037430C File Offset: 0x0037430C
    [return: Nullable(2)]
    internal static string SearchQuery(ToolArgs args)
    {
      string text = args.Str(new string[] { "query", "search", "searchQuery" });
      if (text != null)
      {
        return WorkerTools.Normalized(text);
      }
      List<string> list = (from p in new string[]
        {
          args.Str(new string[] { "song", "track", "title" }),
          args.Str(new string[] { "artist" }),
          args.Str(new string[] { "album" })
        }
        where p != null
        select WorkerTools.Normalized(p) into p
        where p.Length > 0
        select p).ToList<string>();
      if (list.Count != 0)
      {
        return string.Join(" ", list);
      }
      return null;
    }

    // Token: 0x06000A51 RID: 2641 RVA: 0x0037442C File Offset: 0x0037442C
    [return: Nullable(2)]
    internal static string NormalizedTrackUri(string raw)
    {
      string text = raw.Trim();
      if (!text.StartsWith("spotify:track:", StringComparison.OrdinalIgnoreCase))
      {
        return null;
      }
      string text2 = text;
      int length = "spotify:track:".Length;
      string text3 = text2.Substring(length, text2.Length - length);
      if (!WorkerTools.SpotifyId().IsMatch(text3))
      {
        return null;
      }
      return "spotify:track:" + text3;
    }

    // Token: 0x06000A52 RID: 2642 RVA: 0x00374488 File Offset: 0x00374488
    private static string Normalized(string text)
    {
      return string.Join(" ", text.Split(null, StringSplitOptions.RemoveEmptyEntries));
    }

    // Token: 0x06000A53 RID: 2643 RVA: 0x0037449C File Offset: 0x0037449C
    [NullableContext(2)]
    private static string NonEmpty(string text)
    {
      if (!string.IsNullOrWhiteSpace(text))
      {
        return text.Trim();
      }
      return null;
    }

    // Token: 0x06000A54 RID: 2644 RVA: 0x003744B0 File Offset: 0x003744B0
    [GeneratedRegex("^[A-Za-z0-9]{22}$")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex SpotifyId()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__SpotifyId_47.Instance;
    }

    // Token: 0x040004A3 RID: 1187
    public static readonly IReadOnlySet<string> Names = new HashSet<string> { "google_calendar_find_events", "google_calendar_create_event", "save_memory", "play_on_spotify" };

    // Token: 0x040004A4 RID: 1188
    private readonly WorkerPost post;

    // Token: 0x040004A5 RID: 1189
    private readonly Func<string> timeZone;

    // Token: 0x040004A6 RID: 1190
    private readonly Func<string, bool> openUri;

    // Token: 0x040004A7 RID: 1191
    private readonly Func<bool> spotifyAppInstalled;

    // Token: 0x020003B0 RID: 944
    [NullableContext(2)]
    [Nullable(0)]
    private sealed class Track : IEquatable<WorkerTools.Track>
    {
      // Token: 0x060029CA RID: 10698 RVA: 0x00423D70 File Offset: 0x00423D70
      public Track([Nullable(1)] string Uri, string Name, string Artists)
      {
        this.Uri = Uri;
        this.Name = Name;
        this.Artists = Artists;
        base..ctor();
      }

      // Token: 0x17000809 RID: 2057
      // (get) Token: 0x060029CB RID: 10699 RVA: 0x00423D90 File Offset: 0x00423D90
      [Nullable(1)]
      [CompilerGenerated]
      private Type EqualityContract
      {
        [NullableContext(1)]
        [CompilerGenerated]
        get
        {
          return typeof(WorkerTools.Track);
        }
      }

      // Token: 0x1700080A RID: 2058
      // (get) Token: 0x060029CC RID: 10700 RVA: 0x00423D9C File Offset: 0x00423D9C
      // (set) Token: 0x060029CD RID: 10701 RVA: 0x00423DA4 File Offset: 0x00423DA4
      [Nullable(1)]
      public string Uri
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x1700080B RID: 2059
      // (get) Token: 0x060029CE RID: 10702 RVA: 0x00423DB0 File Offset: 0x00423DB0
      // (set) Token: 0x060029CF RID: 10703 RVA: 0x00423DB8 File Offset: 0x00423DB8
      public string Name { get; set; }

      // Token: 0x1700080C RID: 2060
      // (get) Token: 0x060029D0 RID: 10704 RVA: 0x00423DC4 File Offset: 0x00423DC4
      // (set) Token: 0x060029D1 RID: 10705 RVA: 0x00423DCC File Offset: 0x00423DCC
      public string Artists { get; set; }

      // Token: 0x060029D2 RID: 10706 RVA: 0x00423DD8 File Offset: 0x00423DD8
      [NullableContext(1)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Track");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060029D3 RID: 10707 RVA: 0x00423E24 File Offset: 0x00423E24
      [NullableContext(1)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Uri = ");
        builder.Append(this.Uri);
        builder.Append(", Name = ");
        builder.Append(this.Name);
        builder.Append(", Artists = ");
        builder.Append(this.Artists);
        return true;
      }

      // Token: 0x060029D4 RID: 10708 RVA: 0x00423E84 File Offset: 0x00423E84
      [CompilerGenerated]
      public static bool operator !=(WorkerTools.Track left, WorkerTools.Track right)
      {
        return !(left == right);
      }

      // Token: 0x060029D5 RID: 10709 RVA: 0x00423E90 File Offset: 0x00423E90
      [CompilerGenerated]
      public static bool operator ==(WorkerTools.Track left, WorkerTools.Track right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060029D6 RID: 10710 RVA: 0x00423EA4 File Offset: 0x00423EA4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Uri>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Artists>k__BackingField);
      }

      // Token: 0x060029D7 RID: 10711 RVA: 0x00423F08 File Offset: 0x00423F08
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as WorkerTools.Track);
      }

      // Token: 0x060029D8 RID: 10712 RVA: 0x00423F18 File Offset: 0x00423F18
      [CompilerGenerated]
      public bool Equals(WorkerTools.Track other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Uri>k__BackingField, other.<Uri>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Artists>k__BackingField, other.<Artists>k__BackingField));
      }

      // Token: 0x060029DA RID: 10714 RVA: 0x00423F94 File Offset: 0x00423F94
      [CompilerGenerated]
      private Track([Nullable(1)] WorkerTools.Track original)
      {
        this.Uri = original.<Uri>k__BackingField;
        this.Name = original.<Name>k__BackingField;
        this.Artists = original.<Artists>k__BackingField;
      }

      // Token: 0x060029DB RID: 10715 RVA: 0x00423FC0 File Offset: 0x00423FC0
      [CompilerGenerated]
      public void Deconstruct([Nullable(1)] out string Uri, out string Name, out string Artists)
      {
        Uri = this.Uri;
        Name = this.Name;
        Artists = this.Artists;
      }
    }
  }
}
