using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using HeyClicky.Core.Telemetry;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200012C RID: 300
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkConnections
  {
    // Token: 0x17000493 RID: 1171
    // (get) Token: 0x06001384 RID: 4996 RVA: 0x003A6DA4 File Offset: 0x003A6DA4
    public static ClickyInkConnections Shared { get; } = new ClickyInkConnections();

    // Token: 0x17000494 RID: 1172
    // (get) Token: 0x06001385 RID: 4997 RVA: 0x003A6DAC File Offset: 0x003A6DAC
    public IReadOnlyDictionary<string, ClickyInkConnections.App> Apps
    {
      get
      {
        return this.apps;
      }
    }

    // Token: 0x17000495 RID: 1173
    // (get) Token: 0x06001386 RID: 4998 RVA: 0x003A6DB4 File Offset: 0x003A6DB4
    public IReadOnlyDictionary<string, ClickyInkConnections.Phase> Phases
    {
      get
      {
        return this.phases;
      }
    }

    // Token: 0x14000032 RID: 50
    // (add) Token: 0x06001387 RID: 4999 RVA: 0x003A6DBC File Offset: 0x003A6DBC
    // (remove) Token: 0x06001388 RID: 5000 RVA: 0x003A6DF4 File Offset: 0x003A6DF4
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Changed;

    // Token: 0x17000496 RID: 1174
    // (get) Token: 0x06001389 RID: 5001 RVA: 0x003A6E2C File Offset: 0x003A6E2C
    // (set) Token: 0x0600138A RID: 5002 RVA: 0x003A6E34 File Offset: 0x003A6E34
    [Nullable(new byte[] { 2, 1 })]
    public Action<string> Connect
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x0600138B RID: 5003 RVA: 0x003A6E40 File Offset: 0x003A6E40
    public void SetApps(IEnumerable<ClickyInkConnections.App> list)
    {
      Dictionary<string, ClickyInkConnections.App> dictionary = new Dictionary<string, ClickyInkConnections.App>();
      foreach (ClickyInkConnections.App app in list)
      {
        dictionary[ClickyInkConnections.Normalized(app.Slug)] = app;
      }
      if (dictionary.Count == this.apps.Count && dictionary.All<KeyValuePair<string, ClickyInkConnections.App>>(delegate(KeyValuePair<string, ClickyInkConnections.App> kv)
      {
        ClickyInkConnections.App app2;
        return this.apps.TryGetValue(kv.Key, out app2) && app2 == kv.Value;
      }))
      {
        return;
      }
      this.apps = dictionary;
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x0600138C RID: 5004 RVA: 0x003A6ED8 File Offset: 0x003A6ED8
    [return: Nullable(2)]
    public ClickyInkConnections.App AppNamed(string name)
    {
      string wanted = ClickyInkConnections.Normalized(name);
      if (wanted.Length == 0)
      {
        return null;
      }
      ClickyInkConnections.App app;
      if (this.apps.TryGetValue(wanted, out app))
      {
        return app;
      }
      return this.apps.Values.FirstOrDefault<ClickyInkConnections.App>((ClickyInkConnections.App a) => ClickyInkConnections.Normalized(a.DisplayName) == wanted) ?? this.apps.Values.FirstOrDefault<ClickyInkConnections.App>((ClickyInkConnections.App a) => ClickyInkConnections.Normalized(a.DisplayName).HasPrefix(wanted) || wanted.HasPrefix(ClickyInkConnections.Normalized(a.DisplayName)));
    }

    // Token: 0x0600138D RID: 5005 RVA: 0x003A6F5C File Offset: 0x003A6F5C
    [return: Nullable(2)]
    public ClickyInkConnections.Phase PhaseFor(string slug)
    {
      ClickyInkConnections.Phase phase;
      if (!this.phases.TryGetValue(ClickyInkConnections.Normalized(slug), out phase))
      {
        return null;
      }
      return phase;
    }

    // Token: 0x0600138E RID: 5006 RVA: 0x003A6F84 File Offset: 0x003A6F84
    public void StartConnecting(string slug, Action whenConnected)
    {
      string text = ClickyInkConnections.Normalized(slug);
      this.completions[text] = whenConnected;
      this.phases[text] = new ClickyInkConnections.Phase.Opening();
      Action changed = this.Changed;
      if (changed != null)
      {
        changed();
      }
      Action<string> connect = this.Connect;
      if (connect == null)
      {
        return;
      }
      connect(text);
    }

    // Token: 0x0600138F RID: 5007 RVA: 0x003A6FD8 File Offset: 0x003A6FD8
    public void SetPhase([Nullable(2)] ClickyInkConnections.Phase phase, string slug)
    {
      string text = ClickyInkConnections.Normalized(slug);
      ClickyInkConnections.Phase phase2 = this.PhaseFor(text);
      if (phase == null)
      {
        this.phases.Remove(text);
      }
      else
      {
        this.phases[text] = phase;
      }
      Action changed = this.Changed;
      if (changed != null)
      {
        changed();
      }
      Action action;
      if (phase is ClickyInkConnections.Phase.Connected && this.completions.Remove(text, out action))
      {
        action();
      }
      if (phase is ClickyInkConnections.Phase.Failed && this.completions.Remove(text))
      {
        string text2 = "note_object_action";
        string text3 = null;
        string text4 = null;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["action"] = "connect_failed";
        jsonObject["object"] = ClickyInkDocument.Kind.Connect.RawValue();
        jsonObject["sends_to_agent"] = false;
        jsonObject["stage"] = ClickyInkConnections.AnalyticsStage(phase2);
        ClickyAnalytics.TrackProductEvent(text2, text3, text4, jsonObject);
      }
    }

    // Token: 0x06001390 RID: 5008 RVA: 0x003A70C8 File Offset: 0x003A70C8
    public static string AnalyticsStage([Nullable(2)] ClickyInkConnections.Phase phase)
    {
      string text;
      if (!(phase is ClickyInkConnections.Phase.Opening))
      {
        if (!(phase is ClickyInkConnections.Phase.WaitingForSignIn))
        {
          if (!(phase is ClickyInkConnections.Phase.Checking))
          {
            if (!(phase is ClickyInkConnections.Phase.Connected))
            {
              if (!(phase is ClickyInkConnections.Phase.Failed))
              {
                text = "none";
              }
              else
              {
                text = "failed";
              }
            }
            else
            {
              text = "connected";
            }
          }
          else
          {
            text = "checking";
          }
        }
        else
        {
          text = "waiting_for_sign_in";
        }
      }
      else
      {
        text = "opening";
      }
      return text;
    }

    // Token: 0x06001391 RID: 5009 RVA: 0x003A7130 File Offset: 0x003A7130
    public bool IsConnectingFromNote(string slug)
    {
      ClickyInkConnections.Phase phase = this.PhaseFor(slug);
      return phase != null && !(phase is ClickyInkConnections.Phase.Connected);
    }

    // Token: 0x06001392 RID: 5010 RVA: 0x003A7158 File Offset: 0x003A7158
    public static string Normalized(string text)
    {
      return string.Concat(from c in text.Lowered().Characters()
        where InkText.IsLetter(c) || InkText.IsNumber(c)
        select c);
    }

    // Token: 0x040008B4 RID: 2228
    private Dictionary<string, ClickyInkConnections.App> apps = new Dictionary<string, ClickyInkConnections.App>();

    // Token: 0x040008B5 RID: 2229
    private readonly Dictionary<string, ClickyInkConnections.Phase> phases = new Dictionary<string, ClickyInkConnections.Phase>();

    // Token: 0x040008B8 RID: 2232
    private readonly Dictionary<string, Action> completions = new Dictionary<string, Action>();

    // Token: 0x020004A1 RID: 1185
    [Nullable(0)]
    public sealed class App : IEquatable<ClickyInkConnections.App>
    {
      // Token: 0x06002F29 RID: 12073 RVA: 0x0043136C File Offset: 0x0043136C
      public App(string Slug, string DisplayName, [Nullable(2)] Uri LogoUrl, bool IsConnected, bool NeedsReconnect)
      {
        this.Slug = Slug;
        this.DisplayName = DisplayName;
        this.LogoUrl = LogoUrl;
        this.IsConnected = IsConnected;
        this.NeedsReconnect = NeedsReconnect;
        base..ctor();
      }

      // Token: 0x1700087B RID: 2171
      // (get) Token: 0x06002F2A RID: 12074 RVA: 0x0043139C File Offset: 0x0043139C
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkConnections.App);
        }
      }

      // Token: 0x1700087C RID: 2172
      // (get) Token: 0x06002F2B RID: 12075 RVA: 0x004313A8 File Offset: 0x004313A8
      // (set) Token: 0x06002F2C RID: 12076 RVA: 0x004313B0 File Offset: 0x004313B0
      public string Slug { get; set; }

      // Token: 0x1700087D RID: 2173
      // (get) Token: 0x06002F2D RID: 12077 RVA: 0x004313BC File Offset: 0x004313BC
      // (set) Token: 0x06002F2E RID: 12078 RVA: 0x004313C4 File Offset: 0x004313C4
      public string DisplayName { get; set; }

      // Token: 0x1700087E RID: 2174
      // (get) Token: 0x06002F2F RID: 12079 RVA: 0x004313D0 File Offset: 0x004313D0
      // (set) Token: 0x06002F30 RID: 12080 RVA: 0x004313D8 File Offset: 0x004313D8
      [Nullable(2)]
      public Uri LogoUrl
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x1700087F RID: 2175
      // (get) Token: 0x06002F31 RID: 12081 RVA: 0x004313E4 File Offset: 0x004313E4
      // (set) Token: 0x06002F32 RID: 12082 RVA: 0x004313EC File Offset: 0x004313EC
      public bool IsConnected { get; set; }

      // Token: 0x17000880 RID: 2176
      // (get) Token: 0x06002F33 RID: 12083 RVA: 0x004313F8 File Offset: 0x004313F8
      // (set) Token: 0x06002F34 RID: 12084 RVA: 0x00431400 File Offset: 0x00431400
      public bool NeedsReconnect { get; set; }

      // Token: 0x06002F35 RID: 12085 RVA: 0x0043140C File Offset: 0x0043140C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("App");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002F36 RID: 12086 RVA: 0x00431458 File Offset: 0x00431458
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Slug = ");
        builder.Append(this.Slug);
        builder.Append(", DisplayName = ");
        builder.Append(this.DisplayName);
        builder.Append(", LogoUrl = ");
        builder.Append(this.LogoUrl);
        builder.Append(", IsConnected = ");
        builder.Append(this.IsConnected.ToString());
        builder.Append(", NeedsReconnect = ");
        builder.Append(this.NeedsReconnect.ToString());
        return true;
      }

      // Token: 0x06002F37 RID: 12087 RVA: 0x00431504 File Offset: 0x00431504
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkConnections.App left, ClickyInkConnections.App right)
      {
        return !(left == right);
      }

      // Token: 0x06002F38 RID: 12088 RVA: 0x00431510 File Offset: 0x00431510
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkConnections.App left, ClickyInkConnections.App right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002F39 RID: 12089 RVA: 0x00431524 File Offset: 0x00431524
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Slug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<Uri>.Default.GetHashCode(this.<LogoUrl>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsConnected>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<NeedsReconnect>k__BackingField);
      }

      // Token: 0x06002F3A RID: 12090 RVA: 0x004315B4 File Offset: 0x004315B4
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkConnections.App);
      }

      // Token: 0x06002F3B RID: 12091 RVA: 0x004315C4 File Offset: 0x004315C4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkConnections.App other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Slug>k__BackingField, other.<Slug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<Uri>.Default.Equals(this.<LogoUrl>k__BackingField, other.<LogoUrl>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsConnected>k__BackingField, other.<IsConnected>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<NeedsReconnect>k__BackingField, other.<NeedsReconnect>k__BackingField));
      }

      // Token: 0x06002F3D RID: 12093 RVA: 0x00431674 File Offset: 0x00431674
      [CompilerGenerated]
      private App(ClickyInkConnections.App original)
      {
        this.Slug = original.<Slug>k__BackingField;
        this.DisplayName = original.<DisplayName>k__BackingField;
        this.LogoUrl = original.<LogoUrl>k__BackingField;
        this.IsConnected = original.<IsConnected>k__BackingField;
        this.NeedsReconnect = original.<NeedsReconnect>k__BackingField;
      }

      // Token: 0x06002F3E RID: 12094 RVA: 0x004316C4 File Offset: 0x004316C4
      [CompilerGenerated]
      public void Deconstruct(out string Slug, out string DisplayName, [Nullable(2)] out Uri LogoUrl, out bool IsConnected, out bool NeedsReconnect)
      {
        Slug = this.Slug;
        DisplayName = this.DisplayName;
        LogoUrl = this.LogoUrl;
        IsConnected = this.IsConnected;
        NeedsReconnect = this.NeedsReconnect;
      }
    }

    // Token: 0x020004A2 RID: 1186
    [Nullable(0)]
    public abstract class Phase : IEquatable<ClickyInkConnections.Phase>
    {
      // Token: 0x17000881 RID: 2177
      // (get) Token: 0x06002F3F RID: 12095 RVA: 0x004316F0 File Offset: 0x004316F0
      [CompilerGenerated]
      protected virtual Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkConnections.Phase);
        }
      }

      // Token: 0x06002F40 RID: 12096 RVA: 0x004316FC File Offset: 0x004316FC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Phase");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002F41 RID: 12097 RVA: 0x00431748 File Offset: 0x00431748
      [CompilerGenerated]
      protected virtual bool PrintMembers(StringBuilder builder)
      {
        return false;
      }

      // Token: 0x06002F42 RID: 12098 RVA: 0x0043174C File Offset: 0x0043174C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkConnections.Phase left, ClickyInkConnections.Phase right)
      {
        return !(left == right);
      }

      // Token: 0x06002F43 RID: 12099 RVA: 0x00431758 File Offset: 0x00431758
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkConnections.Phase left, ClickyInkConnections.Phase right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002F44 RID: 12100 RVA: 0x0043176C File Offset: 0x0043176C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
      }

      // Token: 0x06002F45 RID: 12101 RVA: 0x00431780 File Offset: 0x00431780
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkConnections.Phase);
      }

      // Token: 0x06002F46 RID: 12102 RVA: 0x00431790 File Offset: 0x00431790
      [NullableContext(2)]
      [CompilerGenerated]
      public virtual bool Equals(ClickyInkConnections.Phase other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract);
      }

      // Token: 0x06002F48 RID: 12104 RVA: 0x004317B0 File Offset: 0x004317B0
      [CompilerGenerated]
      protected Phase(ClickyInkConnections.Phase original)
      {
      }

      // Token: 0x06002F49 RID: 12105 RVA: 0x004317B8 File Offset: 0x004317B8
      protected Phase()
      {
      }

      // Token: 0x020006EA RID: 1770
      [Nullable(0)]
      public sealed class Opening : ClickyInkConnections.Phase, IEquatable<ClickyInkConnections.Phase.Opening>
      {
        // Token: 0x170009FD RID: 2557
        // (get) Token: 0x06003B39 RID: 15161 RVA: 0x004650B0 File Offset: 0x004650B0
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkConnections.Phase.Opening);
          }
        }

        // Token: 0x06003B3A RID: 15162 RVA: 0x004650BC File Offset: 0x004650BC
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Opening");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003B3B RID: 15163 RVA: 0x00465108 File Offset: 0x00465108
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          return base.PrintMembers(builder);
        }

        // Token: 0x06003B3C RID: 15164 RVA: 0x00465114 File Offset: 0x00465114
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkConnections.Phase.Opening left, ClickyInkConnections.Phase.Opening right)
        {
          return !(left == right);
        }

        // Token: 0x06003B3D RID: 15165 RVA: 0x00465120 File Offset: 0x00465120
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkConnections.Phase.Opening left, ClickyInkConnections.Phase.Opening right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003B3E RID: 15166 RVA: 0x00465134 File Offset: 0x00465134
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode();
        }

        // Token: 0x06003B3F RID: 15167 RVA: 0x0046513C File Offset: 0x0046513C
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkConnections.Phase.Opening);
        }

        // Token: 0x06003B40 RID: 15168 RVA: 0x0046514C File Offset: 0x0046514C
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkConnections.Phase other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003B41 RID: 15169 RVA: 0x00465158 File Offset: 0x00465158
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkConnections.Phase.Opening other)
        {
          return this == other || base.Equals(other);
        }

        // Token: 0x06003B43 RID: 15171 RVA: 0x00465170 File Offset: 0x00465170
        [CompilerGenerated]
        private Opening(ClickyInkConnections.Phase.Opening original)
          : base(original)
        {
        }

        // Token: 0x06003B44 RID: 15172 RVA: 0x0046517C File Offset: 0x0046517C
        public Opening()
        {
        }
      }

      // Token: 0x020006EB RID: 1771
      [Nullable(0)]
      public sealed class WaitingForSignIn : ClickyInkConnections.Phase, IEquatable<ClickyInkConnections.Phase.WaitingForSignIn>
      {
        // Token: 0x170009FE RID: 2558
        // (get) Token: 0x06003B45 RID: 15173 RVA: 0x00465184 File Offset: 0x00465184
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkConnections.Phase.WaitingForSignIn);
          }
        }

        // Token: 0x06003B46 RID: 15174 RVA: 0x00465190 File Offset: 0x00465190
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("WaitingForSignIn");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003B47 RID: 15175 RVA: 0x004651DC File Offset: 0x004651DC
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          return base.PrintMembers(builder);
        }

        // Token: 0x06003B48 RID: 15176 RVA: 0x004651E8 File Offset: 0x004651E8
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkConnections.Phase.WaitingForSignIn left, ClickyInkConnections.Phase.WaitingForSignIn right)
        {
          return !(left == right);
        }

        // Token: 0x06003B49 RID: 15177 RVA: 0x004651F4 File Offset: 0x004651F4
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkConnections.Phase.WaitingForSignIn left, ClickyInkConnections.Phase.WaitingForSignIn right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003B4A RID: 15178 RVA: 0x00465208 File Offset: 0x00465208
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode();
        }

        // Token: 0x06003B4B RID: 15179 RVA: 0x00465210 File Offset: 0x00465210
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkConnections.Phase.WaitingForSignIn);
        }

        // Token: 0x06003B4C RID: 15180 RVA: 0x00465220 File Offset: 0x00465220
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkConnections.Phase other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003B4D RID: 15181 RVA: 0x0046522C File Offset: 0x0046522C
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkConnections.Phase.WaitingForSignIn other)
        {
          return this == other || base.Equals(other);
        }

        // Token: 0x06003B4F RID: 15183 RVA: 0x00465244 File Offset: 0x00465244
        [CompilerGenerated]
        private WaitingForSignIn(ClickyInkConnections.Phase.WaitingForSignIn original)
          : base(original)
        {
        }

        // Token: 0x06003B50 RID: 15184 RVA: 0x00465250 File Offset: 0x00465250
        public WaitingForSignIn()
        {
        }
      }

      // Token: 0x020006EC RID: 1772
      [Nullable(0)]
      public sealed class Checking : ClickyInkConnections.Phase, IEquatable<ClickyInkConnections.Phase.Checking>
      {
        // Token: 0x170009FF RID: 2559
        // (get) Token: 0x06003B51 RID: 15185 RVA: 0x00465258 File Offset: 0x00465258
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkConnections.Phase.Checking);
          }
        }

        // Token: 0x06003B52 RID: 15186 RVA: 0x00465264 File Offset: 0x00465264
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Checking");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003B53 RID: 15187 RVA: 0x004652B0 File Offset: 0x004652B0
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          return base.PrintMembers(builder);
        }

        // Token: 0x06003B54 RID: 15188 RVA: 0x004652BC File Offset: 0x004652BC
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkConnections.Phase.Checking left, ClickyInkConnections.Phase.Checking right)
        {
          return !(left == right);
        }

        // Token: 0x06003B55 RID: 15189 RVA: 0x004652C8 File Offset: 0x004652C8
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkConnections.Phase.Checking left, ClickyInkConnections.Phase.Checking right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003B56 RID: 15190 RVA: 0x004652DC File Offset: 0x004652DC
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode();
        }

        // Token: 0x06003B57 RID: 15191 RVA: 0x004652E4 File Offset: 0x004652E4
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkConnections.Phase.Checking);
        }

        // Token: 0x06003B58 RID: 15192 RVA: 0x004652F4 File Offset: 0x004652F4
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkConnections.Phase other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003B59 RID: 15193 RVA: 0x00465300 File Offset: 0x00465300
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkConnections.Phase.Checking other)
        {
          return this == other || base.Equals(other);
        }

        // Token: 0x06003B5B RID: 15195 RVA: 0x00465318 File Offset: 0x00465318
        [CompilerGenerated]
        private Checking(ClickyInkConnections.Phase.Checking original)
          : base(original)
        {
        }

        // Token: 0x06003B5C RID: 15196 RVA: 0x00465324 File Offset: 0x00465324
        public Checking()
        {
        }
      }

      // Token: 0x020006ED RID: 1773
      [Nullable(0)]
      public sealed class Connected : ClickyInkConnections.Phase, IEquatable<ClickyInkConnections.Phase.Connected>
      {
        // Token: 0x17000A00 RID: 2560
        // (get) Token: 0x06003B5D RID: 15197 RVA: 0x0046532C File Offset: 0x0046532C
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkConnections.Phase.Connected);
          }
        }

        // Token: 0x06003B5E RID: 15198 RVA: 0x00465338 File Offset: 0x00465338
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Connected");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003B5F RID: 15199 RVA: 0x00465384 File Offset: 0x00465384
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          return base.PrintMembers(builder);
        }

        // Token: 0x06003B60 RID: 15200 RVA: 0x00465390 File Offset: 0x00465390
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkConnections.Phase.Connected left, ClickyInkConnections.Phase.Connected right)
        {
          return !(left == right);
        }

        // Token: 0x06003B61 RID: 15201 RVA: 0x0046539C File Offset: 0x0046539C
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkConnections.Phase.Connected left, ClickyInkConnections.Phase.Connected right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003B62 RID: 15202 RVA: 0x004653B0 File Offset: 0x004653B0
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode();
        }

        // Token: 0x06003B63 RID: 15203 RVA: 0x004653B8 File Offset: 0x004653B8
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkConnections.Phase.Connected);
        }

        // Token: 0x06003B64 RID: 15204 RVA: 0x004653C8 File Offset: 0x004653C8
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkConnections.Phase other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003B65 RID: 15205 RVA: 0x004653D4 File Offset: 0x004653D4
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkConnections.Phase.Connected other)
        {
          return this == other || base.Equals(other);
        }

        // Token: 0x06003B67 RID: 15207 RVA: 0x004653EC File Offset: 0x004653EC
        [CompilerGenerated]
        private Connected(ClickyInkConnections.Phase.Connected original)
          : base(original)
        {
        }

        // Token: 0x06003B68 RID: 15208 RVA: 0x004653F8 File Offset: 0x004653F8
        public Connected()
        {
        }
      }

      // Token: 0x020006EE RID: 1774
      [Nullable(0)]
      public sealed class Failed : ClickyInkConnections.Phase, IEquatable<ClickyInkConnections.Phase.Failed>
      {
        // Token: 0x06003B69 RID: 15209 RVA: 0x00465400 File Offset: 0x00465400
        public Failed(string Message)
        {
          this.Message = Message;
          base..ctor();
        }

        // Token: 0x17000A01 RID: 2561
        // (get) Token: 0x06003B6A RID: 15210 RVA: 0x00465410 File Offset: 0x00465410
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkConnections.Phase.Failed);
          }
        }

        // Token: 0x17000A02 RID: 2562
        // (get) Token: 0x06003B6B RID: 15211 RVA: 0x0046541C File Offset: 0x0046541C
        // (set) Token: 0x06003B6C RID: 15212 RVA: 0x00465424 File Offset: 0x00465424
        public string Message { get; set; }

        // Token: 0x06003B6D RID: 15213 RVA: 0x00465430 File Offset: 0x00465430
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Failed");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06003B6E RID: 15214 RVA: 0x0046547C File Offset: 0x0046547C
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Message = ");
          builder.Append(this.Message);
          return true;
        }

        // Token: 0x06003B6F RID: 15215 RVA: 0x004654B0 File Offset: 0x004654B0
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkConnections.Phase.Failed left, ClickyInkConnections.Phase.Failed right)
        {
          return !(left == right);
        }

        // Token: 0x06003B70 RID: 15216 RVA: 0x004654BC File Offset: 0x004654BC
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkConnections.Phase.Failed left, ClickyInkConnections.Phase.Failed right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06003B71 RID: 15217 RVA: 0x004654D0 File Offset: 0x004654D0
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Message>k__BackingField);
        }

        // Token: 0x06003B72 RID: 15218 RVA: 0x004654F0 File Offset: 0x004654F0
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkConnections.Phase.Failed);
        }

        // Token: 0x06003B73 RID: 15219 RVA: 0x00465500 File Offset: 0x00465500
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkConnections.Phase other)
        {
          return this.Equals(other);
        }

        // Token: 0x06003B74 RID: 15220 RVA: 0x0046550C File Offset: 0x0046550C
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkConnections.Phase.Failed other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Message>k__BackingField, other.<Message>k__BackingField));
        }

        // Token: 0x06003B76 RID: 15222 RVA: 0x00465540 File Offset: 0x00465540
        [CompilerGenerated]
        private Failed(ClickyInkConnections.Phase.Failed original)
          : base(original)
        {
          this.Message = original.<Message>k__BackingField;
        }

        // Token: 0x06003B77 RID: 15223 RVA: 0x00465558 File Offset: 0x00465558
        [CompilerGenerated]
        public void Deconstruct(out string Message)
        {
          Message = this.Message;
        }
      }
    }
  }
}
