using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x0200002F RID: 47
  [NullableContext(2)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class ClickyAnswer : IEquatable<ClickyAnswer>
  {
    // Token: 0x17000131 RID: 305
    // (get) Token: 0x060004C1 RID: 1217 RVA: 0x000EDEB8 File Offset: 0x000EDEB8
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ClickyAnswer);
      }
    }

    // Token: 0x17000132 RID: 306
    // (get) Token: 0x060004C2 RID: 1218 RVA: 0x000EDEC4 File Offset: 0x000EDEC4
    // (set) Token: 0x060004C3 RID: 1219 RVA: 0x000EDECC File Offset: 0x000EDECC
    [Nullable(1)]
    [RequiredMember]
    [JsonPropertyName("line")]
    public string Line
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000133 RID: 307
    // (get) Token: 0x060004C4 RID: 1220 RVA: 0x000EDED8 File Offset: 0x000EDED8
    // (set) Token: 0x060004C5 RID: 1221 RVA: 0x000EDEE0 File Offset: 0x000EDEE0
    [Nullable(new byte[] { 2, 1 })]
    [JsonPropertyName("numbers")]
    public IReadOnlyList<ClickyAnswer.Number> Numbers
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000134 RID: 308
    // (get) Token: 0x060004C6 RID: 1222 RVA: 0x000EDEEC File Offset: 0x000EDEEC
    // (set) Token: 0x060004C7 RID: 1223 RVA: 0x000EDEF4 File Offset: 0x000EDEF4
    [Nullable(new byte[] { 2, 1 })]
    [JsonPropertyName("list")]
    public IReadOnlyList<ClickyAnswer.Row> List
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000135 RID: 309
    // (get) Token: 0x060004C8 RID: 1224 RVA: 0x000EDF00 File Offset: 0x000EDF00
    // (set) Token: 0x060004C9 RID: 1225 RVA: 0x000EDF08 File Offset: 0x000EDF08
    [JsonPropertyName("ranked")]
    public bool? Ranked { get; set; }

    // Token: 0x17000136 RID: 310
    // (get) Token: 0x060004CA RID: 1226 RVA: 0x000EDF14 File Offset: 0x000EDF14
    // (set) Token: 0x060004CB RID: 1227 RVA: 0x000EDF1C File Offset: 0x000EDF1C
    [Nullable(new byte[] { 2, 1 })]
    [JsonPropertyName("done")]
    public IReadOnlyList<ClickyAnswer.DoneRow> Done
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000137 RID: 311
    // (get) Token: 0x060004CC RID: 1228 RVA: 0x000EDF28 File Offset: 0x000EDF28
    // (set) Token: 0x060004CD RID: 1229 RVA: 0x000EDF30 File Offset: 0x000EDF30
    [JsonPropertyName("draft")]
    public ClickyAnswer.Draft DraftText { get; set; }

    // Token: 0x17000138 RID: 312
    // (get) Token: 0x060004CE RID: 1230 RVA: 0x000EDF3C File Offset: 0x000EDF3C
    // (set) Token: 0x060004CF RID: 1231 RVA: 0x000EDF44 File Offset: 0x000EDF44
    [Nullable(new byte[] { 2, 1 })]
    [JsonPropertyName("ask")]
    public IReadOnlyList<ClickyAnswer.Choice> Ask
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000139 RID: 313
    // (get) Token: 0x060004D0 RID: 1232 RVA: 0x000EDF50 File Offset: 0x000EDF50
    // (set) Token: 0x060004D1 RID: 1233 RVA: 0x000EDF58 File Offset: 0x000EDF58
    [JsonPropertyName("note")]
    public string Note { get; set; }

    // Token: 0x1700013A RID: 314
    // (get) Token: 0x060004D2 RID: 1234 RVA: 0x000EDF64 File Offset: 0x000EDF64
    // (set) Token: 0x060004D3 RID: 1235 RVA: 0x000EDF6C File Offset: 0x000EDF6C
    [JsonPropertyName("next")]
    public ClickyAnswer.NextStep Next { get; set; }

    // Token: 0x1700013B RID: 315
    // (get) Token: 0x060004D4 RID: 1236 RVA: 0x000EDF78 File Offset: 0x000EDF78
    // (set) Token: 0x060004D5 RID: 1237 RVA: 0x000EDF80 File Offset: 0x000EDF80
    [JsonPropertyName("quiet")]
    public bool? Quiet { get; set; }

    // Token: 0x1700013C RID: 316
    // (get) Token: 0x060004D6 RID: 1238 RVA: 0x000EDF8C File Offset: 0x000EDF8C
    [JsonIgnore]
    public ClickyAnswer.NumbersLookKind? NumbersLook
    {
      get
      {
        IReadOnlyList<ClickyAnswer.Number> numbers = this.Numbers;
        if (numbers == null || numbers.Count <= 0)
        {
          return null;
        }
        if (numbers.Any<ClickyAnswer.Number>((ClickyAnswer.Number n) => n.Was != null))
        {
          return new ClickyAnswer.NumbersLookKind?(ClickyAnswer.NumbersLookKind.Changes);
        }
        if (numbers.Any<ClickyAnswer.Number>((ClickyAnswer.Number n) => n.Of != null))
        {
          return new ClickyAnswer.NumbersLookKind?(ClickyAnswer.NumbersLookKind.Progress);
        }
        return new ClickyAnswer.NumbersLookKind?((numbers.Count == 1) ? ClickyAnswer.NumbersLookKind.Hero : ClickyAnswer.NumbersLookKind.Tiles);
      }
    }

    // Token: 0x1700013D RID: 317
    // (get) Token: 0x060004D7 RID: 1239 RVA: 0x000EE024 File Offset: 0x000EE024
    [Nullable(1)]
    [JsonIgnore]
    public IReadOnlyList<ClickyAnswer.Block> Blocks
    {
      [NullableContext(1)]
      get
      {
        if (this.Quiet.GetValueOrDefault())
        {
          return Array.Empty<ClickyAnswer.Block>();
        }
        List<ClickyAnswer.Block> list = new List<ClickyAnswer.Block>();
        IReadOnlyList<ClickyAnswer.Number> numbers = this.Numbers;
        if (numbers != null && numbers.Count > 0)
        {
          list.Add(ClickyAnswer.Block.Numbers);
        }
        IReadOnlyList<ClickyAnswer.Row> list2 = this.List;
        if (list2 != null && list2.Count > 0)
        {
          list.Add(ClickyAnswer.Block.List);
        }
        IReadOnlyList<ClickyAnswer.DoneRow> done = this.Done;
        if (done != null && done.Count > 0)
        {
          list.Add(ClickyAnswer.Block.Done);
        }
        if (this.DraftText != null)
        {
          list.Add(ClickyAnswer.Block.Draft);
        }
        IReadOnlyList<ClickyAnswer.Choice> ask = this.Ask;
        if (ask != null && ask.Count > 0)
        {
          list.Add(ClickyAnswer.Block.Ask);
        }
        if (list.Count > 2 && list.Contains(ClickyAnswer.Block.Ask))
        {
          return new <>z__ReadOnlyArray<ClickyAnswer.Block>(new ClickyAnswer.Block[]
          {
            list[0],
            ClickyAnswer.Block.Ask
          });
        }
        if (list.Count > 2 && list.Contains(ClickyAnswer.Block.Draft))
        {
          return new <>z__ReadOnlyArray<ClickyAnswer.Block>(new ClickyAnswer.Block[]
          {
            list[0],
            ClickyAnswer.Block.Draft
          });
        }
        return list.Take<ClickyAnswer.Block>(2).ToList<ClickyAnswer.Block>();
      }
    }

    // Token: 0x1700013E RID: 318
    // (get) Token: 0x060004D8 RID: 1240 RVA: 0x000EE130 File Offset: 0x000EE130
    [JsonIgnore]
    public bool IsQuiet
    {
      get
      {
        return this.Quiet.GetValueOrDefault();
      }
    }

    // Token: 0x1700013F RID: 319
    // (get) Token: 0x060004D9 RID: 1241 RVA: 0x000EE14C File Offset: 0x000EE14C
    [JsonIgnore]
    public string ReportJson
    {
      get
      {
        string text;
        try
        {
          JsonObject jsonObject = JsonSerializer.SerializeToNode<ClickyAnswer>(this, ClickyAnswer.ReportJsonOptions) as JsonObject;
          if (jsonObject == null)
          {
            text = null;
          }
          else
          {
            jsonObject.Remove("line");
            jsonObject["outcome"] = this.Line;
            text = ClickyAnswer.Sorted(jsonObject).ToJsonString(ClickyAnswer.ReportJsonOptions);
          }
        }
        catch (Exception)
        {
          text = null;
        }
        return text;
      }
    }

    // Token: 0x17000140 RID: 320
    // (get) Token: 0x060004DA RID: 1242 RVA: 0x000EE1BC File Offset: 0x000EE1BC
    [Nullable(1)]
    private static JsonSerializerOptions ReportJsonOptions
    {
      [NullableContext(1)]
      get
      {
        return ClickyAnswer.ReportJsonSettings;
      }
    }

    // Token: 0x060004DB RID: 1243 RVA: 0x000EE1C4 File Offset: 0x000EE1C4
    [NullableContext(1)]
    private static JsonNode Sorted(JsonNode node)
    {
      JsonObject jsonObject = node as JsonObject;
      JsonNode jsonNode;
      if (jsonObject == null)
      {
        JsonArray jsonArray = node as JsonArray;
        if (jsonArray == null)
        {
          jsonNode = node.DeepClone();
        }
        else
        {
          jsonNode = new JsonArray(jsonArray.Select<JsonNode, JsonNode>(delegate(JsonNode n)
          {
            if (n != null)
            {
              return ClickyAnswer.Sorted(n.DeepClone());
            }
            return null;
          }).ToArray<JsonNode>());
        }
      }
      else
      {
        jsonNode = new JsonObject(from kv in jsonObject.OrderBy<KeyValuePair<string, JsonNode>, string>((KeyValuePair<string, JsonNode> kv) => kv.Key, StringComparer.Ordinal)
          select KeyValuePair.Create<string, JsonNode>(kv.Key, (kv.Value == null) ? null : ClickyAnswer.Sorted(kv.Value.DeepClone())), null);
      }
      return jsonNode;
    }

    // Token: 0x060004DC RID: 1244 RVA: 0x000EE284 File Offset: 0x000EE284
    public static ClickyAnswer Parse(string blockText)
    {
      string text = ((blockText != null) ? blockText.Trim() : null);
      if (string.IsNullOrEmpty(text))
      {
        return null;
      }
      if (text.StartsWith("```", StringComparison.Ordinal))
      {
        text = string.Join("\n", text.Split('\n', StringSplitOptions.None).Skip<string>(1));
        int num = text.LastIndexOf("```", StringComparison.Ordinal);
        if (num >= 0)
        {
          text = text.Substring(0, num);
        }
        text = text.Trim();
      }
      JsonObject jsonObject = null;
      if (text.StartsWith('{'))
      {
        int num2 = text.IndexOf('{');
        int num3 = text.LastIndexOf('}');
        if (num3 > num2)
        {
          try
          {
            string text2 = text;
            int num4 = num2;
            jsonObject = JsonNode.Parse(text2.Substring(num4, num3 + 1 - num4), null, default(JsonDocumentOptions)) as JsonObject;
          }
          catch (JsonException)
          {
          }
        }
      }
      if (jsonObject == null)
      {
        string text3 = ClickyAnswer.Clean(text, 220);
        if (text3.Length != 0 && !text3.Contains('{'))
        {
          return new ClickyAnswer
          {
            Line = text3
          };
        }
        return null;
      }
      else
      {
        string text4 = ClickyAnswer.Clean(ClickyAnswer.Str(jsonObject["outcome"]) ?? ClickyAnswer.Str(jsonObject["line"]), 220);
        if (text4.Length == 0)
        {
          return null;
        }
        List<ClickyAnswer.Number> list = ClickyAnswer.Arr(jsonObject["numbers"]).Select<JsonNode, ClickyAnswer.Number>(delegate(JsonNode any)
        {
          JsonObject jsonObject4 = any as JsonObject;
          if (jsonObject4 != null)
          {
            string text11 = ClickyAnswer.Fitting(jsonObject4["value"], 12);
            if (text11 != null)
            {
              string text12 = ClickyAnswer.Str(jsonObject4["better"]);
              string text13 = ((text12 != null) ? text12.ToLowerInvariant() : null);
              ClickyAnswer.Number number = new ClickyAnswer.Number();
              number.Value = text11;
              number.Label = ClickyAnswer.Optional(jsonObject4["label"], 32);
              number.Was = ClickyAnswer.Fitting(jsonObject4["was"], 12);
              number.Of = ClickyAnswer.Fitting(jsonObject4["of"], 12);
              ClickyAnswer.Number number2 = number;
              bool flag4 = text13 == "higher" || text13 == "lower";
              number2.Better = (flag4 ? text13 : null);
              return number;
            }
          }
          return null;
        }).OfType<ClickyAnswer.Number>()
          .ToList<ClickyAnswer.Number>();
        List<ClickyAnswer.Row> list2 = ClickyAnswer.Arr(jsonObject["list"]).Select<JsonNode, ClickyAnswer.Row>(delegate(JsonNode any)
        {
          JsonObject jsonObject5 = any as JsonObject;
          if (jsonObject5 == null)
          {
            string text14 = ClickyAnswer.Fitting(any, 56);
            if (text14 == null)
            {
              return null;
            }
            return new ClickyAnswer.Row
            {
              Text = text14
            };
          }
          else
          {
            string text15 = ClickyAnswer.Fitting(jsonObject5["text"], 56);
            if (text15 == null)
            {
              return null;
            }
            return new ClickyAnswer.Row
            {
              Text = text15,
              Detail = ClickyAnswer.Fitting(jsonObject5["detail"], 24)
            };
          }
        }).OfType<ClickyAnswer.Row>()
          .ToList<ClickyAnswer.Row>();
        List<ClickyAnswer.DoneRow> list3 = ClickyAnswer.Arr(jsonObject["done"]).Select<JsonNode, ClickyAnswer.DoneRow>(delegate(JsonNode any)
        {
          JsonObject jsonObject6 = any as JsonObject;
          if (jsonObject6 == null)
          {
            string text16 = ClickyAnswer.Fitting(any, 60);
            if (text16 == null)
            {
              return null;
            }
            return new ClickyAnswer.DoneRow
            {
              Did = text16
            };
          }
          else
          {
            string text17 = ClickyAnswer.Fitting(jsonObject6["did"] ?? jsonObject6["text"], 60);
            if (text17 == null)
            {
              return null;
            }
            return new ClickyAnswer.DoneRow
            {
              Did = text17,
              What = ClickyAnswer.Fitting(jsonObject6["what"], 32),
              Link = ClickyAnswer.Target(jsonObject6["link"])
            };
          }
        }).OfType<ClickyAnswer.DoneRow>()
          .ToList<ClickyAnswer.DoneRow>();
        ClickyAnswer.Draft draft = null;
        JsonObject jsonObject2 = jsonObject["draft"] as JsonObject;
        if (jsonObject2 != null)
        {
          string text5 = ClickyAnswer.Str(jsonObject2["text"]);
          if (text5 != null)
          {
            string text6 = ClickyAnswer.Cap(text5.Trim(), 8000);
            if (text6.Length > 0)
            {
              draft = new ClickyAnswer.Draft
              {
                Text = text6,
                To = ClickyAnswer.Optional(jsonObject2["to"], 40)
              };
              goto IL_02A8;
            }
            goto IL_02A8;
          }
        }
        string text7 = ClickyAnswer.Str(jsonObject["draft"]);
        string text8 = ((text7 != null) ? text7.Trim() : null);
        if (text8 != null && text8.Length > 0)
        {
          draft = new ClickyAnswer.Draft
          {
            Text = ClickyAnswer.Cap(text8, 8000)
          };
        }
        IL_02A8:
        List<ClickyAnswer.Choice> list4 = ClickyAnswer.Arr(jsonObject["ask"]).Select<JsonNode, ClickyAnswer.Choice>(delegate(JsonNode any)
        {
          JsonObject jsonObject7 = any as JsonObject;
          if (jsonObject7 != null)
          {
            string text18 = ClickyAnswer.Target(jsonObject7["file"]);
            if (text18 != null && !text18.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
              return new ClickyAnswer.Choice
              {
                Answer = ClickyAnswer.Fitting(jsonObject7["answer"], 40),
                Detail = ClickyAnswer.Fitting(jsonObject7["detail"], 24),
                File = text18
              };
            }
            string text19 = ClickyAnswer.Fitting(jsonObject7["answer"] ?? jsonObject7["text"], 40);
            if (text19 == null)
            {
              return null;
            }
            return new ClickyAnswer.Choice
            {
              Answer = text19,
              Detail = ClickyAnswer.Fitting(jsonObject7["detail"], 24)
            };
          }
          else
          {
            string text20 = ClickyAnswer.Fitting(any, 40);
            if (text20 == null)
            {
              return null;
            }
            return new ClickyAnswer.Choice
            {
              Answer = text20
            };
          }
        }).OfType<ClickyAnswer.Choice>()
          .ToList<ClickyAnswer.Choice>();
        List<ClickyAnswer.Choice> list5 = list4.Where<ClickyAnswer.Choice>((ClickyAnswer.Choice c) => c.File != null).ToList<ClickyAnswer.Choice>();
        List<ClickyAnswer.Choice> list6 = list4.Where<ClickyAnswer.Choice>((ClickyAnswer.Choice c) => c.File == null).ToList<ClickyAnswer.Choice>();
        IReadOnlyList<ClickyAnswer.Choice> readOnlyList = ((list5.Count >= 2) ? list5.Take<ClickyAnswer.Choice>(3).ToList<ClickyAnswer.Choice>() : ((list6.Count >= 2) ? list6.Take<ClickyAnswer.Choice>(4).ToList<ClickyAnswer.Choice>() : null));
        ClickyAnswer.NextStep nextStep = null;
        JsonObject jsonObject3 = jsonObject["next"] as JsonObject;
        if (jsonObject3 != null)
        {
          string text9 = ClickyAnswer.Fitting(jsonObject3["label"], 26);
          if (text9 != null)
          {
            string text10 = ClickyAnswer.Target(jsonObject3["open"] ?? jsonObject3["link"]);
            if (text10 != null)
            {
              nextStep = new ClickyAnswer.NextStep
              {
                Label = text9,
                Open = text10
              };
            }
          }
        }
        List<ClickyAnswer.Row> list7 = ((list2.Count > 0) ? list2.Take<ClickyAnswer.Row>(4).ToList<ClickyAnswer.Row>() : null);
        ClickyAnswer clickyAnswer = new ClickyAnswer();
        clickyAnswer.Line = text4;
        clickyAnswer.Numbers = ((list.Count > 0) ? list.Take<ClickyAnswer.Number>(3).ToList<ClickyAnswer.Number>() : null);
        clickyAnswer.List = list7;
        bool flag2;
        bool flag;
        if (list7 != null)
        {
          JsonValue jsonValue = jsonObject["ranked"] as JsonValue;
          if (jsonValue != null)
          {
            flag = jsonValue.TryGetValue<bool>(out flag2);
            goto IL_0457;
          }
        }
        flag = false;
        IL_0457:
        clickyAnswer.Ranked = ((flag && flag2) ? new bool?(true) : null);
        clickyAnswer.Done = ((list3.Count > 0) ? list3.Take<ClickyAnswer.DoneRow>(3).ToList<ClickyAnswer.DoneRow>() : null);
        clickyAnswer.DraftText = draft;
        clickyAnswer.Ask = readOnlyList;
        clickyAnswer.Note = ClickyAnswer.Optional(jsonObject["note"], 100);
        clickyAnswer.Next = nextStep;
        JsonValue jsonValue2 = jsonObject["quiet"] as JsonValue;
        bool flag3;
        clickyAnswer.Quiet = ((jsonValue2 != null && jsonValue2.TryGetValue<bool>(out flag3) && flag3) ? new bool?(true) : null);
        return clickyAnswer;
      }
    }

    // Token: 0x060004DD RID: 1245 RVA: 0x000EE7A4 File Offset: 0x000EE7A4
    private static string Str(JsonNode node)
    {
      JsonValue jsonValue = node as JsonValue;
      if (jsonValue == null)
      {
        return null;
      }
      string text;
      if (jsonValue.TryGetValue<string>(out text))
      {
        return text;
      }
      if (jsonValue.GetValueKind() == JsonValueKind.Number)
      {
        return jsonValue.ToJsonString(null);
      }
      return null;
    }

    // Token: 0x060004DE RID: 1246 RVA: 0x000EE7DC File Offset: 0x000EE7DC
    [return: Nullable(new byte[] { 1, 2 })]
    private static IEnumerable<JsonNode> Arr(JsonNode node)
    {
      JsonArray jsonArray;
      if ((jsonArray = node as JsonArray) == null)
      {
        jsonArray = new JsonArray(null);
      }
      return jsonArray;
    }

    // Token: 0x060004DF RID: 1247 RVA: 0x000EE804 File Offset: 0x000EE804
    private static string Fitting(JsonNode node, int max)
    {
      string text = ClickyAnswer.Clean(ClickyAnswer.Str(node), 10000);
      if (text.Length != 0 && text.Length <= max)
      {
        return text;
      }
      return null;
    }

    // Token: 0x060004E0 RID: 1248 RVA: 0x000EE838 File Offset: 0x000EE838
    private static string Optional(JsonNode node, int max)
    {
      string text = ClickyAnswer.Clean(ClickyAnswer.Str(node), max);
      if (text.Length != 0)
      {
        return text;
      }
      return null;
    }

    // Token: 0x060004E1 RID: 1249 RVA: 0x000EE860 File Offset: 0x000EE860
    private static string Target(JsonNode node)
    {
      string text = ClickyAnswer.Str(node);
      string text2 = ((text != null) ? text.Trim() : null);
      if (string.IsNullOrEmpty(text2))
      {
        return null;
      }
      string text3 = text2.ToLowerInvariant();
      if (text3.StartsWith("https://", StringComparison.Ordinal) || text3.StartsWith("http://", StringComparison.Ordinal))
      {
        return text2;
      }
      string text4;
      if (!text2.StartsWith("~/", StringComparison.Ordinal) && !text2.StartsWith("~\\", StringComparison.Ordinal))
      {
        text4 = text2;
      }
      else
      {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string text5 = text2;
        text4 = Path.Combine(folderPath, text5.Substring(2, text5.Length - 2));
      }
      string text6 = text4;
      if (!Path.IsPathFullyQualified(text6))
      {
        return null;
      }
      return text6;
    }

    // Token: 0x060004E2 RID: 1250 RVA: 0x000EE8F8 File Offset: 0x000EE8F8
    [NullableContext(1)]
    public static string Clean([Nullable(2)] string raw, int max)
    {
      string text = (raw ?? "").Replace("\n", " ").Replace("**", "").Replace("`", "")
        .Trim();
      while (text.Contains("  ", StringComparison.Ordinal))
      {
        text = text.Replace("  ", " ");
      }
      if (text.Length <= max)
      {
        return text;
      }
      string text2 = text.Substring(0, max);
      int num = text2.LastIndexOf(' ');
      return ((num >= 0) ? text2.Substring(0, num) : text2).Trim() + "…";
    }

    // Token: 0x060004E3 RID: 1251 RVA: 0x000EE9A0 File Offset: 0x000EE9A0
    [NullableContext(1)]
    private static string Cap(string s, int n)
    {
      if (s.Length <= n)
      {
        return s;
      }
      return s.Substring(0, n);
    }

    // Token: 0x060004E4 RID: 1252 RVA: 0x000EE9B8 File Offset: 0x000EE9B8
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyAnswer");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060004E5 RID: 1253 RVA: 0x000EEA04 File Offset: 0x000EEA04
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Line = ");
      builder.Append(this.Line);
      builder.Append(", Numbers = ");
      builder.Append(this.Numbers);
      builder.Append(", List = ");
      builder.Append(this.List);
      builder.Append(", Ranked = ");
      builder.Append(this.Ranked.ToString());
      builder.Append(", Done = ");
      builder.Append(this.Done);
      builder.Append(", DraftText = ");
      builder.Append(this.DraftText);
      builder.Append(", Ask = ");
      builder.Append(this.Ask);
      builder.Append(", Note = ");
      builder.Append(this.Note);
      builder.Append(", Next = ");
      builder.Append(this.Next);
      builder.Append(", Quiet = ");
      builder.Append(this.Quiet.ToString());
      builder.Append(", NumbersLook = ");
      builder.Append(this.NumbersLook.ToString());
      builder.Append(", Blocks = ");
      builder.Append(this.Blocks);
      builder.Append(", IsQuiet = ");
      builder.Append(this.IsQuiet.ToString());
      builder.Append(", ReportJson = ");
      builder.Append(this.ReportJson);
      return true;
    }

    // Token: 0x060004E6 RID: 1254 RVA: 0x000EEBB0 File Offset: 0x000EEBB0
    [CompilerGenerated]
    public static bool operator !=(ClickyAnswer left, ClickyAnswer right)
    {
      return !(left == right);
    }

    // Token: 0x060004E7 RID: 1255 RVA: 0x000EEBBC File Offset: 0x000EEBBC
    [CompilerGenerated]
    public static bool operator ==(ClickyAnswer left, ClickyAnswer right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060004E8 RID: 1256 RVA: 0x000EEBD0 File Offset: 0x000EEBD0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Line>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<ClickyAnswer.Number>>.Default.GetHashCode(this.<Numbers>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<ClickyAnswer.Row>>.Default.GetHashCode(this.<List>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<Ranked>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<ClickyAnswer.DoneRow>>.Default.GetHashCode(this.<Done>k__BackingField)) * -1521134295 + EqualityComparer<ClickyAnswer.Draft>.Default.GetHashCode(this.<DraftText>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<ClickyAnswer.Choice>>.Default.GetHashCode(this.<Ask>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Note>k__BackingField)) * -1521134295 + EqualityComparer<ClickyAnswer.NextStep>.Default.GetHashCode(this.<Next>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<Quiet>k__BackingField);
    }

    // Token: 0x060004E9 RID: 1257 RVA: 0x000EECD4 File Offset: 0x000EECD4
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyAnswer);
    }

    // Token: 0x060004EA RID: 1258 RVA: 0x000EECE4 File Offset: 0x000EECE4
    [CompilerGenerated]
    public bool Equals(ClickyAnswer other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Line>k__BackingField, other.<Line>k__BackingField) && EqualityComparer<IReadOnlyList<ClickyAnswer.Number>>.Default.Equals(this.<Numbers>k__BackingField, other.<Numbers>k__BackingField) && EqualityComparer<IReadOnlyList<ClickyAnswer.Row>>.Default.Equals(this.<List>k__BackingField, other.<List>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<Ranked>k__BackingField, other.<Ranked>k__BackingField) && EqualityComparer<IReadOnlyList<ClickyAnswer.DoneRow>>.Default.Equals(this.<Done>k__BackingField, other.<Done>k__BackingField) && EqualityComparer<ClickyAnswer.Draft>.Default.Equals(this.<DraftText>k__BackingField, other.<DraftText>k__BackingField) && EqualityComparer<IReadOnlyList<ClickyAnswer.Choice>>.Default.Equals(this.<Ask>k__BackingField, other.<Ask>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Note>k__BackingField, other.<Note>k__BackingField) && EqualityComparer<ClickyAnswer.NextStep>.Default.Equals(this.<Next>k__BackingField, other.<Next>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<Quiet>k__BackingField, other.<Quiet>k__BackingField));
    }

    // Token: 0x060004EC RID: 1260 RVA: 0x000EEE1C File Offset: 0x000EEE1C
    [CompilerGenerated]
    [SetsRequiredMembers]
    private ClickyAnswer([Nullable(1)] ClickyAnswer original)
    {
      this.Line = original.<Line>k__BackingField;
      this.Numbers = original.<Numbers>k__BackingField;
      this.List = original.<List>k__BackingField;
      this.Ranked = original.<Ranked>k__BackingField;
      this.Done = original.<Done>k__BackingField;
      this.DraftText = original.<DraftText>k__BackingField;
      this.Ask = original.<Ask>k__BackingField;
      this.Note = original.<Note>k__BackingField;
      this.Next = original.<Next>k__BackingField;
      this.Quiet = original.<Quiet>k__BackingField;
    }

    // Token: 0x060004ED RID: 1261 RVA: 0x000EEEA8 File Offset: 0x000EEEA8
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public ClickyAnswer()
    {
    }

    // Token: 0x040001CF RID: 463
    [Nullable(1)]
    private static readonly JsonSerializerOptions ReportJsonSettings = new JsonSerializerOptions
    {
      DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
      Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    // Token: 0x02000138 RID: 312
    [Nullable(0)]
    [RequiredMember]
    public sealed class Number : IEquatable<ClickyAnswer.Number>
    {
      // Token: 0x170002A1 RID: 673
      // (get) Token: 0x06000C35 RID: 3125 RVA: 0x00112B2C File Offset: 0x00112B2C
      [Nullable(1)]
      [CompilerGenerated]
      private Type EqualityContract
      {
        [NullableContext(1)]
        [CompilerGenerated]
        get
        {
          return typeof(ClickyAnswer.Number);
        }
      }

      // Token: 0x170002A2 RID: 674
      // (get) Token: 0x06000C36 RID: 3126 RVA: 0x00112B38 File Offset: 0x00112B38
      // (set) Token: 0x06000C37 RID: 3127 RVA: 0x00112B40 File Offset: 0x00112B40
      [Nullable(1)]
      [RequiredMember]
      [JsonPropertyName("value")]
      public string Value
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x170002A3 RID: 675
      // (get) Token: 0x06000C38 RID: 3128 RVA: 0x00112B4C File Offset: 0x00112B4C
      // (set) Token: 0x06000C39 RID: 3129 RVA: 0x00112B54 File Offset: 0x00112B54
      [JsonPropertyName("label")]
      public string Label { get; set; }

      // Token: 0x170002A4 RID: 676
      // (get) Token: 0x06000C3A RID: 3130 RVA: 0x00112B60 File Offset: 0x00112B60
      // (set) Token: 0x06000C3B RID: 3131 RVA: 0x00112B68 File Offset: 0x00112B68
      [JsonPropertyName("was")]
      public string Was { get; set; }

      // Token: 0x170002A5 RID: 677
      // (get) Token: 0x06000C3C RID: 3132 RVA: 0x00112B74 File Offset: 0x00112B74
      // (set) Token: 0x06000C3D RID: 3133 RVA: 0x00112B7C File Offset: 0x00112B7C
      [JsonPropertyName("of")]
      public string Of { get; set; }

      // Token: 0x170002A6 RID: 678
      // (get) Token: 0x06000C3E RID: 3134 RVA: 0x00112B88 File Offset: 0x00112B88
      // (set) Token: 0x06000C3F RID: 3135 RVA: 0x00112B90 File Offset: 0x00112B90
      [JsonPropertyName("better")]
      public string Better { get; set; }

      // Token: 0x06000C40 RID: 3136 RVA: 0x00112B9C File Offset: 0x00112B9C
      [NullableContext(1)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Number");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000C41 RID: 3137 RVA: 0x00112BE8 File Offset: 0x00112BE8
      [NullableContext(1)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Value = ");
        builder.Append(this.Value);
        builder.Append(", Label = ");
        builder.Append(this.Label);
        builder.Append(", Was = ");
        builder.Append(this.Was);
        builder.Append(", Of = ");
        builder.Append(this.Of);
        builder.Append(", Better = ");
        builder.Append(this.Better);
        return true;
      }

      // Token: 0x06000C42 RID: 3138 RVA: 0x00112C78 File Offset: 0x00112C78
      [CompilerGenerated]
      public static bool operator !=(ClickyAnswer.Number left, ClickyAnswer.Number right)
      {
        return !(left == right);
      }

      // Token: 0x06000C43 RID: 3139 RVA: 0x00112C84 File Offset: 0x00112C84
      [CompilerGenerated]
      public static bool operator ==(ClickyAnswer.Number left, ClickyAnswer.Number right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000C44 RID: 3140 RVA: 0x00112C98 File Offset: 0x00112C98
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Value>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Label>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Was>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Of>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Better>k__BackingField);
      }

      // Token: 0x06000C45 RID: 3141 RVA: 0x00112D28 File Offset: 0x00112D28
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyAnswer.Number);
      }

      // Token: 0x06000C46 RID: 3142 RVA: 0x00112D38 File Offset: 0x00112D38
      [CompilerGenerated]
      public bool Equals(ClickyAnswer.Number other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Value>k__BackingField, other.<Value>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Label>k__BackingField, other.<Label>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Was>k__BackingField, other.<Was>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Of>k__BackingField, other.<Of>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Better>k__BackingField, other.<Better>k__BackingField));
      }

      // Token: 0x06000C48 RID: 3144 RVA: 0x00112DE8 File Offset: 0x00112DE8
      [CompilerGenerated]
      [SetsRequiredMembers]
      private Number([Nullable(1)] ClickyAnswer.Number original)
      {
        this.Value = original.<Value>k__BackingField;
        this.Label = original.<Label>k__BackingField;
        this.Was = original.<Was>k__BackingField;
        this.Of = original.<Of>k__BackingField;
        this.Better = original.<Better>k__BackingField;
      }

      // Token: 0x06000C49 RID: 3145 RVA: 0x00112E38 File Offset: 0x00112E38
      [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
      [CompilerFeatureRequired("RequiredMembers")]
      public Number()
      {
      }
    }

    // Token: 0x02000139 RID: 313
    [NullableContext(1)]
    [Nullable(0)]
    [RequiredMember]
    public sealed class Row : IEquatable<ClickyAnswer.Row>
    {
      // Token: 0x170002A7 RID: 679
      // (get) Token: 0x06000C4A RID: 3146 RVA: 0x00112E40 File Offset: 0x00112E40
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyAnswer.Row);
        }
      }

      // Token: 0x170002A8 RID: 680
      // (get) Token: 0x06000C4B RID: 3147 RVA: 0x00112E4C File Offset: 0x00112E4C
      // (set) Token: 0x06000C4C RID: 3148 RVA: 0x00112E54 File Offset: 0x00112E54
      [RequiredMember]
      [JsonPropertyName("text")]
      public string Text { get; set; }

      // Token: 0x170002A9 RID: 681
      // (get) Token: 0x06000C4D RID: 3149 RVA: 0x00112E60 File Offset: 0x00112E60
      // (set) Token: 0x06000C4E RID: 3150 RVA: 0x00112E68 File Offset: 0x00112E68
      [Nullable(2)]
      [JsonPropertyName("detail")]
      public string Detail
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x06000C4F RID: 3151 RVA: 0x00112E74 File Offset: 0x00112E74
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Row");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000C50 RID: 3152 RVA: 0x00112EC0 File Offset: 0x00112EC0
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Text = ");
        builder.Append(this.Text);
        builder.Append(", Detail = ");
        builder.Append(this.Detail);
        return true;
      }

      // Token: 0x06000C51 RID: 3153 RVA: 0x00112EFC File Offset: 0x00112EFC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyAnswer.Row left, ClickyAnswer.Row right)
      {
        return !(left == right);
      }

      // Token: 0x06000C52 RID: 3154 RVA: 0x00112F08 File Offset: 0x00112F08
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyAnswer.Row left, ClickyAnswer.Row right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000C53 RID: 3155 RVA: 0x00112F1C File Offset: 0x00112F1C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Detail>k__BackingField);
      }

      // Token: 0x06000C54 RID: 3156 RVA: 0x00112F5C File Offset: 0x00112F5C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyAnswer.Row);
      }

      // Token: 0x06000C55 RID: 3157 RVA: 0x00112F6C File Offset: 0x00112F6C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyAnswer.Row other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Detail>k__BackingField, other.<Detail>k__BackingField));
      }

      // Token: 0x06000C57 RID: 3159 RVA: 0x00112FD0 File Offset: 0x00112FD0
      [CompilerGenerated]
      [SetsRequiredMembers]
      private Row(ClickyAnswer.Row original)
      {
        this.Text = original.<Text>k__BackingField;
        this.Detail = original.<Detail>k__BackingField;
      }

      // Token: 0x06000C58 RID: 3160 RVA: 0x00112FF0 File Offset: 0x00112FF0
      [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
      [CompilerFeatureRequired("RequiredMembers")]
      public Row()
      {
      }
    }

    // Token: 0x0200013A RID: 314
    [Nullable(0)]
    [RequiredMember]
    public sealed class DoneRow : IEquatable<ClickyAnswer.DoneRow>
    {
      // Token: 0x170002AA RID: 682
      // (get) Token: 0x06000C59 RID: 3161 RVA: 0x00112FF8 File Offset: 0x00112FF8
      [Nullable(1)]
      [CompilerGenerated]
      private Type EqualityContract
      {
        [NullableContext(1)]
        [CompilerGenerated]
        get
        {
          return typeof(ClickyAnswer.DoneRow);
        }
      }

      // Token: 0x170002AB RID: 683
      // (get) Token: 0x06000C5A RID: 3162 RVA: 0x00113004 File Offset: 0x00113004
      // (set) Token: 0x06000C5B RID: 3163 RVA: 0x0011300C File Offset: 0x0011300C
      [Nullable(1)]
      [RequiredMember]
      [JsonPropertyName("did")]
      public string Did
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x170002AC RID: 684
      // (get) Token: 0x06000C5C RID: 3164 RVA: 0x00113018 File Offset: 0x00113018
      // (set) Token: 0x06000C5D RID: 3165 RVA: 0x00113020 File Offset: 0x00113020
      [JsonPropertyName("what")]
      public string What { get; set; }

      // Token: 0x170002AD RID: 685
      // (get) Token: 0x06000C5E RID: 3166 RVA: 0x0011302C File Offset: 0x0011302C
      // (set) Token: 0x06000C5F RID: 3167 RVA: 0x00113034 File Offset: 0x00113034
      [JsonPropertyName("link")]
      public string Link { get; set; }

      // Token: 0x06000C60 RID: 3168 RVA: 0x00113040 File Offset: 0x00113040
      [NullableContext(1)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("DoneRow");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000C61 RID: 3169 RVA: 0x0011308C File Offset: 0x0011308C
      [NullableContext(1)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Did = ");
        builder.Append(this.Did);
        builder.Append(", What = ");
        builder.Append(this.What);
        builder.Append(", Link = ");
        builder.Append(this.Link);
        return true;
      }

      // Token: 0x06000C62 RID: 3170 RVA: 0x001130EC File Offset: 0x001130EC
      [CompilerGenerated]
      public static bool operator !=(ClickyAnswer.DoneRow left, ClickyAnswer.DoneRow right)
      {
        return !(left == right);
      }

      // Token: 0x06000C63 RID: 3171 RVA: 0x001130F8 File Offset: 0x001130F8
      [CompilerGenerated]
      public static bool operator ==(ClickyAnswer.DoneRow left, ClickyAnswer.DoneRow right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000C64 RID: 3172 RVA: 0x0011310C File Offset: 0x0011310C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Did>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<What>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Link>k__BackingField);
      }

      // Token: 0x06000C65 RID: 3173 RVA: 0x00113170 File Offset: 0x00113170
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyAnswer.DoneRow);
      }

      // Token: 0x06000C66 RID: 3174 RVA: 0x00113180 File Offset: 0x00113180
      [CompilerGenerated]
      public bool Equals(ClickyAnswer.DoneRow other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Did>k__BackingField, other.<Did>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<What>k__BackingField, other.<What>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Link>k__BackingField, other.<Link>k__BackingField));
      }

      // Token: 0x06000C68 RID: 3176 RVA: 0x001131FC File Offset: 0x001131FC
      [CompilerGenerated]
      [SetsRequiredMembers]
      private DoneRow([Nullable(1)] ClickyAnswer.DoneRow original)
      {
        this.Did = original.<Did>k__BackingField;
        this.What = original.<What>k__BackingField;
        this.Link = original.<Link>k__BackingField;
      }

      // Token: 0x06000C69 RID: 3177 RVA: 0x00113228 File Offset: 0x00113228
      [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
      [CompilerFeatureRequired("RequiredMembers")]
      public DoneRow()
      {
      }
    }

    // Token: 0x0200013B RID: 315
    [NullableContext(1)]
    [Nullable(0)]
    [RequiredMember]
    public sealed class Draft : IEquatable<ClickyAnswer.Draft>
    {
      // Token: 0x170002AE RID: 686
      // (get) Token: 0x06000C6A RID: 3178 RVA: 0x00113230 File Offset: 0x00113230
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyAnswer.Draft);
        }
      }

      // Token: 0x170002AF RID: 687
      // (get) Token: 0x06000C6B RID: 3179 RVA: 0x0011323C File Offset: 0x0011323C
      // (set) Token: 0x06000C6C RID: 3180 RVA: 0x00113244 File Offset: 0x00113244
      [RequiredMember]
      [JsonPropertyName("text")]
      public string Text { get; set; }

      // Token: 0x170002B0 RID: 688
      // (get) Token: 0x06000C6D RID: 3181 RVA: 0x00113250 File Offset: 0x00113250
      // (set) Token: 0x06000C6E RID: 3182 RVA: 0x00113258 File Offset: 0x00113258
      [Nullable(2)]
      [JsonPropertyName("to")]
      public string To
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x06000C6F RID: 3183 RVA: 0x00113264 File Offset: 0x00113264
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Draft");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000C70 RID: 3184 RVA: 0x001132B0 File Offset: 0x001132B0
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Text = ");
        builder.Append(this.Text);
        builder.Append(", To = ");
        builder.Append(this.To);
        return true;
      }

      // Token: 0x06000C71 RID: 3185 RVA: 0x001132EC File Offset: 0x001132EC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyAnswer.Draft left, ClickyAnswer.Draft right)
      {
        return !(left == right);
      }

      // Token: 0x06000C72 RID: 3186 RVA: 0x001132F8 File Offset: 0x001132F8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyAnswer.Draft left, ClickyAnswer.Draft right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000C73 RID: 3187 RVA: 0x0011330C File Offset: 0x0011330C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<To>k__BackingField);
      }

      // Token: 0x06000C74 RID: 3188 RVA: 0x0011334C File Offset: 0x0011334C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyAnswer.Draft);
      }

      // Token: 0x06000C75 RID: 3189 RVA: 0x0011335C File Offset: 0x0011335C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyAnswer.Draft other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<To>k__BackingField, other.<To>k__BackingField));
      }

      // Token: 0x06000C77 RID: 3191 RVA: 0x001133C0 File Offset: 0x001133C0
      [CompilerGenerated]
      [SetsRequiredMembers]
      private Draft(ClickyAnswer.Draft original)
      {
        this.Text = original.<Text>k__BackingField;
        this.To = original.<To>k__BackingField;
      }

      // Token: 0x06000C78 RID: 3192 RVA: 0x001133E0 File Offset: 0x001133E0
      [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
      [CompilerFeatureRequired("RequiredMembers")]
      public Draft()
      {
      }
    }

    // Token: 0x0200013C RID: 316
    [Nullable(0)]
    public sealed class Choice : IEquatable<ClickyAnswer.Choice>
    {
      // Token: 0x170002B1 RID: 689
      // (get) Token: 0x06000C79 RID: 3193 RVA: 0x001133E8 File Offset: 0x001133E8
      [Nullable(1)]
      [CompilerGenerated]
      private Type EqualityContract
      {
        [NullableContext(1)]
        [CompilerGenerated]
        get
        {
          return typeof(ClickyAnswer.Choice);
        }
      }

      // Token: 0x170002B2 RID: 690
      // (get) Token: 0x06000C7A RID: 3194 RVA: 0x001133F4 File Offset: 0x001133F4
      // (set) Token: 0x06000C7B RID: 3195 RVA: 0x001133FC File Offset: 0x001133FC
      [JsonPropertyName("answer")]
      public string Answer { get; set; }

      // Token: 0x170002B3 RID: 691
      // (get) Token: 0x06000C7C RID: 3196 RVA: 0x00113408 File Offset: 0x00113408
      // (set) Token: 0x06000C7D RID: 3197 RVA: 0x00113410 File Offset: 0x00113410
      [JsonPropertyName("detail")]
      public string Detail { get; set; }

      // Token: 0x170002B4 RID: 692
      // (get) Token: 0x06000C7E RID: 3198 RVA: 0x0011341C File Offset: 0x0011341C
      // (set) Token: 0x06000C7F RID: 3199 RVA: 0x00113424 File Offset: 0x00113424
      [JsonPropertyName("file")]
      public string File { get; set; }

      // Token: 0x170002B5 RID: 693
      // (get) Token: 0x06000C80 RID: 3200 RVA: 0x00113430 File Offset: 0x00113430
      [Nullable(1)]
      [JsonIgnore]
      public string Reply
      {
        [NullableContext(1)]
        get
        {
          string text;
          if ((text = this.Answer) == null)
          {
            string file = this.File;
            if (file == null)
            {
              return "";
            }
            text = Path.GetFileName(file);
          }
          return text;
        }
      }

      // Token: 0x06000C81 RID: 3201 RVA: 0x00113460 File Offset: 0x00113460
      [NullableContext(1)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Choice");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000C82 RID: 3202 RVA: 0x001134AC File Offset: 0x001134AC
      [NullableContext(1)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Answer = ");
        builder.Append(this.Answer);
        builder.Append(", Detail = ");
        builder.Append(this.Detail);
        builder.Append(", File = ");
        builder.Append(this.File);
        builder.Append(", Reply = ");
        builder.Append(this.Reply);
        return true;
      }

      // Token: 0x06000C83 RID: 3203 RVA: 0x00113524 File Offset: 0x00113524
      [CompilerGenerated]
      public static bool operator !=(ClickyAnswer.Choice left, ClickyAnswer.Choice right)
      {
        return !(left == right);
      }

      // Token: 0x06000C84 RID: 3204 RVA: 0x00113530 File Offset: 0x00113530
      [CompilerGenerated]
      public static bool operator ==(ClickyAnswer.Choice left, ClickyAnswer.Choice right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000C85 RID: 3205 RVA: 0x00113544 File Offset: 0x00113544
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Answer>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Detail>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<File>k__BackingField);
      }

      // Token: 0x06000C86 RID: 3206 RVA: 0x001135A8 File Offset: 0x001135A8
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyAnswer.Choice);
      }

      // Token: 0x06000C87 RID: 3207 RVA: 0x001135B8 File Offset: 0x001135B8
      [CompilerGenerated]
      public bool Equals(ClickyAnswer.Choice other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Answer>k__BackingField, other.<Answer>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Detail>k__BackingField, other.<Detail>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<File>k__BackingField, other.<File>k__BackingField));
      }

      // Token: 0x06000C89 RID: 3209 RVA: 0x00113634 File Offset: 0x00113634
      [CompilerGenerated]
      private Choice([Nullable(1)] ClickyAnswer.Choice original)
      {
        this.Answer = original.<Answer>k__BackingField;
        this.Detail = original.<Detail>k__BackingField;
        this.File = original.<File>k__BackingField;
      }

      // Token: 0x06000C8A RID: 3210 RVA: 0x00113660 File Offset: 0x00113660
      public Choice()
      {
      }
    }

    // Token: 0x0200013D RID: 317
    [NullableContext(1)]
    [Nullable(0)]
    [RequiredMember]
    public sealed class NextStep : IEquatable<ClickyAnswer.NextStep>
    {
      // Token: 0x170002B6 RID: 694
      // (get) Token: 0x06000C8B RID: 3211 RVA: 0x00113668 File Offset: 0x00113668
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyAnswer.NextStep);
        }
      }

      // Token: 0x170002B7 RID: 695
      // (get) Token: 0x06000C8C RID: 3212 RVA: 0x00113674 File Offset: 0x00113674
      // (set) Token: 0x06000C8D RID: 3213 RVA: 0x0011367C File Offset: 0x0011367C
      [RequiredMember]
      [JsonPropertyName("label")]
      public string Label { get; set; }

      // Token: 0x170002B8 RID: 696
      // (get) Token: 0x06000C8E RID: 3214 RVA: 0x00113688 File Offset: 0x00113688
      // (set) Token: 0x06000C8F RID: 3215 RVA: 0x00113690 File Offset: 0x00113690
      [RequiredMember]
      [JsonPropertyName("open")]
      public string Open { get; set; }

      // Token: 0x06000C90 RID: 3216 RVA: 0x0011369C File Offset: 0x0011369C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("NextStep");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000C91 RID: 3217 RVA: 0x001136E8 File Offset: 0x001136E8
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Label = ");
        builder.Append(this.Label);
        builder.Append(", Open = ");
        builder.Append(this.Open);
        return true;
      }

      // Token: 0x06000C92 RID: 3218 RVA: 0x00113724 File Offset: 0x00113724
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyAnswer.NextStep left, ClickyAnswer.NextStep right)
      {
        return !(left == right);
      }

      // Token: 0x06000C93 RID: 3219 RVA: 0x00113730 File Offset: 0x00113730
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyAnswer.NextStep left, ClickyAnswer.NextStep right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000C94 RID: 3220 RVA: 0x00113744 File Offset: 0x00113744
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Label>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Open>k__BackingField);
      }

      // Token: 0x06000C95 RID: 3221 RVA: 0x00113784 File Offset: 0x00113784
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyAnswer.NextStep);
      }

      // Token: 0x06000C96 RID: 3222 RVA: 0x00113794 File Offset: 0x00113794
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyAnswer.NextStep other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Label>k__BackingField, other.<Label>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Open>k__BackingField, other.<Open>k__BackingField));
      }

      // Token: 0x06000C98 RID: 3224 RVA: 0x001137F8 File Offset: 0x001137F8
      [CompilerGenerated]
      [SetsRequiredMembers]
      private NextStep(ClickyAnswer.NextStep original)
      {
        this.Label = original.<Label>k__BackingField;
        this.Open = original.<Open>k__BackingField;
      }

      // Token: 0x06000C99 RID: 3225 RVA: 0x00113818 File Offset: 0x00113818
      [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
      [CompilerFeatureRequired("RequiredMembers")]
      public NextStep()
      {
      }
    }

    // Token: 0x0200013E RID: 318
    [NullableContext(0)]
    public enum NumbersLookKind
    {
      // Token: 0x040006AE RID: 1710
      Hero,
      // Token: 0x040006AF RID: 1711
      Tiles,
      // Token: 0x040006B0 RID: 1712
      Changes,
      // Token: 0x040006B1 RID: 1713
      Progress
    }

    // Token: 0x0200013F RID: 319
    [NullableContext(0)]
    public enum Block
    {
      // Token: 0x040006B3 RID: 1715
      Numbers,
      // Token: 0x040006B4 RID: 1716
      List,
      // Token: 0x040006B5 RID: 1717
      Done,
      // Token: 0x040006B6 RID: 1718
      Draft,
      // Token: 0x040006B7 RID: 1719
      Ask
    }
  }
}
