using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.App.Wiring;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x0200011E RID: 286
  [NullableContext(1)]
  [Nullable(0)]
  public static class IntegrationCatalog
  {
    // Token: 0x06000F5A RID: 3930 RVA: 0x0057A7C0 File Offset: 0x0057A7C0
    public static async Task<IntegrationCatalog.Apps> FetchAsync(RealStack real)
    {
      IntegrationCatalog.Apps apps;
      try
      {
        JsonNode jsonNode = await real.Core.Worker.GetJsonObjectAsync("/agent/integrations", default(CancellationToken));
        IntegrationCatalog.<>c__DisplayClass1_0 CS$<>8__locals1;
        CS$<>8__locals1.all = new List<string>();
        CS$<>8__locals1.connected = new List<string>();
        IntegrationCatalog.<FetchAsync>g__Walk|1_0(jsonNode, ref CS$<>8__locals1);
        apps = new IntegrationCatalog.Apps(CS$<>8__locals1.all.Distinct<string>().Take<string>(80).ToList<string>(), CS$<>8__locals1.connected.Distinct<string>().ToList<string>());
      }
      catch (Exception ex)
      {
        Log.Warn("integrations", "catalog: " + ex.Message);
        apps = new IntegrationCatalog.Apps(Array.Empty<string>(), Array.Empty<string>());
      }
      return apps;
    }

    // Token: 0x06000F5B RID: 3931 RVA: 0x0057A804 File Offset: 0x0057A804
    [NullableContext(2)]
    [CompilerGenerated]
    internal static void <FetchAsync>g__Walk|1_0(JsonNode n, ref IntegrationCatalog.<>c__DisplayClass1_0 A_1)
    {
      JsonObject jsonObject = n as JsonObject;
      if (jsonObject != null)
      {
        JsonValue jsonValue = jsonObject["display_name"] as JsonValue;
        string text;
        if (jsonValue != null && jsonValue.TryGetValue<string>(out text) && text.Length > 0)
        {
          A_1.all.Add(text);
          JsonValue jsonValue2 = jsonObject["status"] as JsonValue;
          string text3;
          string text2 = ((jsonValue2 != null && jsonValue2.TryGetValue<string>(out text3)) ? text3 : "");
          bool flag = text2 == "connected" || text2 == "partial";
          if (flag)
          {
            JsonValue jsonValue3 = jsonObject["description"] as JsonValue;
            string text5;
            string text4 = ((jsonValue3 != null && jsonValue3.TryGetValue<string>(out text5)) ? text5.Trim() : "");
            A_1.connected.Add((text4.Length == 0) ? text : (text + ": " + text4));
            return;
          }
          return;
        }
        else
        {
          using (IEnumerator<KeyValuePair<string, JsonNode>> enumerator = jsonObject.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              KeyValuePair<string, JsonNode> keyValuePair = enumerator.Current;
              IntegrationCatalog.<FetchAsync>g__Walk|1_0(keyValuePair.Value, ref A_1);
            }
            return;
          }
        }
      }
      JsonArray jsonArray = n as JsonArray;
      if (jsonArray != null)
      {
        foreach (JsonNode jsonNode in jsonArray)
        {
          IntegrationCatalog.<FetchAsync>g__Walk|1_0(jsonNode, ref A_1);
        }
      }
    }

    // Token: 0x0200062E RID: 1582
    [Nullable(0)]
    public sealed class Apps : IEquatable<IntegrationCatalog.Apps>
    {
      // Token: 0x0600312E RID: 12590 RVA: 0x0066C2A4 File Offset: 0x0066C2A4
      public Apps(IReadOnlyList<string> All, IReadOnlyList<string> ConnectedLines)
      {
        this.All = All;
        this.ConnectedLines = ConnectedLines;
        base..ctor();
      }

      // Token: 0x17000780 RID: 1920
      // (get) Token: 0x0600312F RID: 12591 RVA: 0x0066C2BC File Offset: 0x0066C2BC
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(IntegrationCatalog.Apps);
        }
      }

      // Token: 0x17000781 RID: 1921
      // (get) Token: 0x06003130 RID: 12592 RVA: 0x0066C2C8 File Offset: 0x0066C2C8
      // (set) Token: 0x06003131 RID: 12593 RVA: 0x0066C2D0 File Offset: 0x0066C2D0
      public IReadOnlyList<string> All { get; set; }

      // Token: 0x17000782 RID: 1922
      // (get) Token: 0x06003132 RID: 12594 RVA: 0x0066C2DC File Offset: 0x0066C2DC
      // (set) Token: 0x06003133 RID: 12595 RVA: 0x0066C2E4 File Offset: 0x0066C2E4
      public IReadOnlyList<string> ConnectedLines { get; set; }

      // Token: 0x06003134 RID: 12596 RVA: 0x0066C2F0 File Offset: 0x0066C2F0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Apps");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003135 RID: 12597 RVA: 0x0066C33C File Offset: 0x0066C33C
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("All = ");
        builder.Append(this.All);
        builder.Append(", ConnectedLines = ");
        builder.Append(this.ConnectedLines);
        return true;
      }

      // Token: 0x06003136 RID: 12598 RVA: 0x0066C378 File Offset: 0x0066C378
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(IntegrationCatalog.Apps left, IntegrationCatalog.Apps right)
      {
        return !(left == right);
      }

      // Token: 0x06003137 RID: 12599 RVA: 0x0066C384 File Offset: 0x0066C384
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(IntegrationCatalog.Apps left, IntegrationCatalog.Apps right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003138 RID: 12600 RVA: 0x0066C398 File Offset: 0x0066C398
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<All>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<ConnectedLines>k__BackingField);
      }

      // Token: 0x06003139 RID: 12601 RVA: 0x0066C3D8 File Offset: 0x0066C3D8
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as IntegrationCatalog.Apps);
      }

      // Token: 0x0600313A RID: 12602 RVA: 0x0066C3E8 File Offset: 0x0066C3E8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(IntegrationCatalog.Apps other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<All>k__BackingField, other.<All>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<ConnectedLines>k__BackingField, other.<ConnectedLines>k__BackingField));
      }

      // Token: 0x0600313C RID: 12604 RVA: 0x0066C44C File Offset: 0x0066C44C
      [CompilerGenerated]
      private Apps(IntegrationCatalog.Apps original)
      {
        this.All = original.<All>k__BackingField;
        this.ConnectedLines = original.<ConnectedLines>k__BackingField;
      }

      // Token: 0x0600313D RID: 12605 RVA: 0x0066C46C File Offset: 0x0066C46C
      [CompilerGenerated]
      public void Deconstruct(out IReadOnlyList<string> All, out IReadOnlyList<string> ConnectedLines)
      {
        All = this.All;
        ConnectedLines = this.ConnectedLines;
      }
    }
  }
}
