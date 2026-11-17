using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using HeyClicky.Core.Auth;

namespace HeyClicky.Core.Billing
{
  // Token: 0x0200020A RID: 522
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentTurnCostLedger
  {
    // Token: 0x170006BB RID: 1723
    // (get) Token: 0x06001E5F RID: 7775 RVA: 0x003ED7A8 File Offset: 0x003ED7A8
    // (set) Token: 0x06001E60 RID: 7776 RVA: 0x003ED7B0 File Offset: 0x003ED7B0
    public string CurrentLaneModel { get; private set; }

    // Token: 0x170006BC RID: 1724
    // (get) Token: 0x06001E61 RID: 7777 RVA: 0x003ED7BC File Offset: 0x003ED7BC
    // (set) Token: 0x06001E62 RID: 7778 RVA: 0x003ED7C4 File Offset: 0x003ED7C4
    public string CurrentLaneReasoningEffort { get; private set; }

    // Token: 0x170006BD RID: 1725
    // (get) Token: 0x06001E63 RID: 7779 RVA: 0x003ED7D0 File Offset: 0x003ED7D0
    // (set) Token: 0x06001E64 RID: 7780 RVA: 0x003ED7D8 File Offset: 0x003ED7D8
    public bool HasReadCurrentLane { get; private set; }

    // Token: 0x1400003D RID: 61
    // (add) Token: 0x06001E65 RID: 7781 RVA: 0x003ED7E4 File Offset: 0x003ED7E4
    // (remove) Token: 0x06001E66 RID: 7782 RVA: 0x003ED81C File Offset: 0x003ED81C
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Changed;

    // Token: 0x06001E67 RID: 7783 RVA: 0x003ED854 File Offset: 0x003ED854
    public AgentTurnCostLedger([Nullable(2)] string path, string defaultModel, string defaultReasoningEffort)
    {
      this.path = path;
      this.CurrentLaneModel = defaultModel;
      this.CurrentLaneReasoningEffort = defaultReasoningEffort;
      this.Load();
    }

    // Token: 0x170006BE RID: 1726
    // (get) Token: 0x06001E68 RID: 7784 RVA: 0x003ED890 File Offset: 0x003ED890
    public IReadOnlyList<AgentTurnUsageRecord> Records
    {
      get
      {
        IReadOnlyList<AgentTurnUsageRecord> readOnlyList;
        using (this.gate.EnterScope())
        {
          readOnlyList = this.records.ToList<AgentTurnUsageRecord>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x06001E69 RID: 7785 RVA: 0x003ED8D0 File Offset: 0x003ED8D0
    [NullableContext(2)]
    public bool ApplyModelPolicy(JsonObject policy)
    {
      JsonObject jsonObject = ((policy != null) ? policy["codexAgentLane"] : null) as JsonObject;
      if (jsonObject != null)
      {
        JsonNode jsonNode = jsonObject["model"];
        string text = ((jsonNode != null) ? jsonNode.GetValue<string>() : null);
        if (text != null && text.Length > 0)
        {
          using (this.gate.EnterScope())
          {
            this.CurrentLaneModel = text;
            JsonNode jsonNode2 = jsonObject["reasoningEffort"];
            string text2 = ((jsonNode2 != null) ? jsonNode2.GetValue<string>() : null);
            if (text2 != null && text2.Length > 0)
            {
              this.CurrentLaneReasoningEffort = text2;
            }
            this.HasReadCurrentLane = true;
          }
          Log.Info("agent cost", "proxy lane is " + this.CurrentLaneModel + "/" + this.CurrentLaneReasoningEffort);
          return true;
        }
      }
      return false;
    }

    // Token: 0x06001E6A RID: 7786 RVA: 0x003ED9A4 File Offset: 0x003ED9A4
    public bool Record(AgentTurnUsageRecord record)
    {
      if (record.InputTokens <= 0L && record.OutputTokens <= 0L)
      {
        return false;
      }
      using (this.gate.EnterScope())
      {
        this.records.Add(record);
        if (this.records.Count > 500)
        {
          this.records.RemoveRange(0, this.records.Count - 500);
        }
        this.Persist();
      }
      foreach (string text in this.CostLine(record).Split('\n', StringSplitOptions.None))
      {
        Log.Info("agent cost", text);
      }
      Action changed = this.Changed;
      if (changed != null)
      {
        changed();
      }
      return true;
    }

    // Token: 0x06001E6B RID: 7787 RVA: 0x003EDA70 File Offset: 0x003EDA70
    public bool RecordBilledCost(string leaseId, double billedCostUsd)
    {
      bool flag = false;
      using (this.gate.EnterScope())
      {
        for (int i = 0; i < this.records.Count; i++)
        {
          if (this.records[i].LeaseId == leaseId)
          {
            List<AgentTurnUsageRecord> list = this.records;
            int num = i;
            AgentTurnUsageRecord agentTurnUsageRecord = this.records[i].<Clone>$();
            agentTurnUsageRecord.BilledCostUsd = new double?(billedCostUsd);
            list[num] = agentTurnUsageRecord;
            flag = true;
          }
        }
        if (flag)
        {
          this.Persist();
        }
      }
      if (!flag)
      {
        return false;
      }
      Log.Info("agent cost", "billed by the Worker for lease " + leaseId.Substring(0, Math.Min(8, leaseId.Length)) + ": " + AgentTurnCostLedger.Usd(billedCostUsd));
      Action changed = this.Changed;
      if (changed != null)
      {
        changed();
      }
      return true;
    }

    // Token: 0x06001E6C RID: 7788 RVA: 0x003EDB54 File Offset: 0x003EDB54
    [NullableContext(2)]
    public AgentTurnUsageRecord RecordFor(string threadId, string turnId)
    {
      if (threadId == null || turnId == null)
      {
        return null;
      }
      AgentTurnUsageRecord agentTurnUsageRecord;
      using (this.gate.EnterScope())
      {
        agentTurnUsageRecord = this.records.LastOrDefault<AgentTurnUsageRecord>((AgentTurnUsageRecord r) => r.ThreadId == threadId && r.TurnId == turnId);
      }
      return agentTurnUsageRecord;
    }

    // Token: 0x06001E6D RID: 7789 RVA: 0x003EDBC8 File Offset: 0x003EDBC8
    [NullableContext(2)]
    public string CostLineFor(string threadId, string turnId)
    {
      AgentTurnUsageRecord agentTurnUsageRecord = this.RecordFor(threadId, turnId);
      if (agentTurnUsageRecord == null)
      {
        return null;
      }
      return this.CostLine(agentTurnUsageRecord);
    }

    // Token: 0x06001E6E RID: 7790 RVA: 0x003EDBEC File Offset: 0x003EDBEC
    public string CostLine(AgentTurnUsageRecord record)
    {
      List<string> list = new List<string>();
      double? billedCostUsd = record.BilledCostUsd;
      if (billedCostUsd != null)
      {
        double valueOrDefault = billedCostUsd.GetValueOrDefault();
        list.Add("billed " + AgentTurnCostLedger.Usd(valueOrDefault) + " by the Worker (incl. cache writes + web searches, whole agent message)");
      }
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(19, 3);
      defaultInterpolatedStringHandler.AppendFormatted(AgentTurnCostLedger.Tokens(record.InputTokens));
      defaultInterpolatedStringHandler.AppendLiteral(" in (");
      defaultInterpolatedStringHandler.AppendFormatted(AgentTurnCostLedger.Tokens(record.CachedInputTokens));
      defaultInterpolatedStringHandler.AppendLiteral(" cached), ");
      defaultInterpolatedStringHandler.AppendFormatted(AgentTurnCostLedger.Tokens(record.OutputTokens));
      defaultInterpolatedStringHandler.AppendLiteral(" out");
      string text = defaultInterpolatedStringHandler.ToStringAndClear();
      AgentModelPricing? recordPricing = AgentModelPricing.For(record.Model);
      List<string> list2 = list;
      string text2;
      if (recordPricing != null)
      {
        AgentModelPricing valueOrDefault2 = recordPricing.GetValueOrDefault();
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 4);
        defaultInterpolatedStringHandler.AppendLiteral("≈ ");
        defaultInterpolatedStringHandler.AppendFormatted(AgentTurnCostLedger.Usd(valueOrDefault2.CostUsd(record.InputTokens, record.CachedInputTokens, record.OutputTokens)));
        defaultInterpolatedStringHandler.AppendLiteral(" this turn on ");
        defaultInterpolatedStringHandler.AppendFormatted(record.Model);
        defaultInterpolatedStringHandler.AppendLiteral("/");
        defaultInterpolatedStringHandler.AppendFormatted(record.ReasoningEffort);
        defaultInterpolatedStringHandler.AppendLiteral(" · ");
        defaultInterpolatedStringHandler.AppendFormatted(text);
        text2 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 3);
        defaultInterpolatedStringHandler.AppendLiteral("this turn on ");
        defaultInterpolatedStringHandler.AppendFormatted(record.Model);
        defaultInterpolatedStringHandler.AppendLiteral("/");
        defaultInterpolatedStringHandler.AppendFormatted(record.ReasoningEffort);
        defaultInterpolatedStringHandler.AppendLiteral(" (no price on file) · ");
        defaultInterpolatedStringHandler.AppendFormatted(text);
        text2 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      list2.Add(text2);
      List<ValueTuple<string, AgentModelPricing>> list3 = AgentModelPricing.ComparisonTiers.Where<ValueTuple<string, AgentModelPricing>>(([TupleElementNames(new string[] { "Label", "Pricing" })] ValueTuple<string, AgentModelPricing> t) => t.Item2 != recordPricing).ToList<ValueTuple<string, AgentModelPricing>>();
      if (list3.Count > 0)
      {
        list.Add(string.Join(" · ", list3.Select<ValueTuple<string, AgentModelPricing>, string>(([TupleElementNames(new string[] { "Label", "Pricing" })] ValueTuple<string, AgentModelPricing> t) => t.Item1 + " ≈ " + AgentTurnCostLedger.Usd(t.Item2.CostUsd(record.InputTokens, record.CachedInputTokens, record.OutputTokens)))));
      }
      string slug = record.ClickySlug;
      if (slug != null)
      {
        List<AgentTurnUsageRecord> day;
        using (this.gate.EnterScope())
        {
          day = this.records.Where<AgentTurnUsageRecord>((AgentTurnUsageRecord r) => r.ClickySlug == slug && r.FinishedAt <= record.FinishedAt && record.FinishedAt - r.FinishedAt <= TimeSpan.FromHours(24)).ToList<AgentTurnUsageRecord>();
        }
        double num = day.Sum<AgentTurnUsageRecord>(delegate(AgentTurnUsageRecord r)
        {
          if (AgentModelPricing.For(r.Model) == null)
          {
            return 0.0;
          }
          AgentModelPricing? agentModelPricing;
          return agentModelPricing.GetValueOrDefault().CostUsd(r.InputTokens, r.CachedInputTokens, r.OutputTokens);
        });
        IEnumerable<string> enumerable = list3.Select<ValueTuple<string, AgentModelPricing>, string>(([TupleElementNames(new string[] { "Label", "Pricing" })] ValueTuple<string, AgentModelPricing> t) => t.Item1 + " " + AgentTurnCostLedger.Usd(day.Sum<AgentTurnUsageRecord>((AgentTurnUsageRecord r) => t.Item2.CostUsd(r.InputTokens, r.CachedInputTokens, r.OutputTokens))));
        List<string> list4 = list;
        string text3 = " · ";
        string[] array = new string[1];
        int num2 = 0;
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 3);
        defaultInterpolatedStringHandler.AppendFormatted(record.ClickyName ?? slug);
        defaultInterpolatedStringHandler.AppendLiteral(", last 24h (");
        defaultInterpolatedStringHandler.AppendFormatted<int>(day.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" turns): ");
        defaultInterpolatedStringHandler.AppendFormatted(AgentTurnCostLedger.Usd(num));
        array[num2] = defaultInterpolatedStringHandler.ToStringAndClear();
        list4.Add(string.Join(text3, array.Concat<string>(enumerable)));
      }
      return string.Join("\n", list);
    }

    // Token: 0x06001E6F RID: 7791 RVA: 0x003EDF8C File Offset: 0x003EDF8C
    public static string Usd(double value)
    {
      if (value < 0.01)
      {
        return "$" + value.ToString("0.0000", CultureInfo.InvariantCulture);
      }
      if (value >= 1.0)
      {
        return "$" + value.ToString("0.00", CultureInfo.InvariantCulture);
      }
      return "$" + value.ToString("0.000", CultureInfo.InvariantCulture);
    }

    // Token: 0x06001E70 RID: 7792 RVA: 0x003EE004 File Offset: 0x003EE004
    public static string Tokens(long count)
    {
      if (count >= 1000000L)
      {
        return ((double)count / 1000000.0).ToString("0.0", CultureInfo.InvariantCulture) + "M";
      }
      if (count < 1000L)
      {
        return count.ToString(CultureInfo.InvariantCulture);
      }
      return ((double)count / 1000.0).ToString("0.0", CultureInfo.InvariantCulture) + "k";
    }

    // Token: 0x06001E71 RID: 7793 RVA: 0x003EE084 File Offset: 0x003EE084
    private void Load()
    {
      if (this.path == null || !File.Exists(this.path))
      {
        return;
      }
      try
      {
        JsonArray jsonArray = JsonNode.Parse(File.ReadAllText(this.path), null, default(JsonDocumentOptions)) as JsonArray;
        if (jsonArray != null)
        {
          foreach (JsonObject jsonObject in jsonArray.OfType<JsonObject>())
          {
            JsonNode jsonNode = jsonObject["task_id"];
            string text = ((jsonNode != null) ? jsonNode.GetValue<string>() : null);
            if (text != null)
            {
              List<AgentTurnUsageRecord> list = this.records;
              string text2 = text;
              string text3 = AgentTurnCostLedger.<Load>g__Str|30_0(jsonObject, "thread_id");
              string text4 = AgentTurnCostLedger.<Load>g__Str|30_0(jsonObject, "turn_id");
              string text5 = AgentTurnCostLedger.<Load>g__Str|30_0(jsonObject, "clicky_slug");
              string text6 = AgentTurnCostLedger.<Load>g__Str|30_0(jsonObject, "clicky_name");
              JsonNode jsonNode2 = jsonObject["finished_at_ms"];
              DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds((jsonNode2 != null) ? jsonNode2.GetValue<long>() : 0L);
              string text7 = AgentTurnCostLedger.<Load>g__Str|30_0(jsonObject, "model") ?? "unknown";
              string text8 = AgentTurnCostLedger.<Load>g__Str|30_0(jsonObject, "reasoning_effort") ?? "";
              JsonNode jsonNode3 = jsonObject["input_tokens"];
              long num = ((jsonNode3 != null) ? jsonNode3.GetValue<long>() : 0L);
              JsonNode jsonNode4 = jsonObject["cached_input_tokens"];
              long num2 = ((jsonNode4 != null) ? jsonNode4.GetValue<long>() : 0L);
              JsonNode jsonNode5 = jsonObject["output_tokens"];
              long num3 = ((jsonNode5 != null) ? jsonNode5.GetValue<long>() : 0L);
              string text9 = AgentTurnCostLedger.<Load>g__Str|30_0(jsonObject, "lease_id");
              JsonNode jsonNode6 = jsonObject["billed_cost_usd"];
              list.Add(new AgentTurnUsageRecord(text2, text3, text4, text5, text6, dateTimeOffset, text7, text8, num, num2, num3, text9, (jsonNode6 != null) ? new double?(jsonNode6.GetValue<double>()) : null));
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("agent cost", "ledger unreadable, starting empty: " + ex.Message);
        this.records.Clear();
      }
    }

    // Token: 0x06001E72 RID: 7794 RVA: 0x003EE290 File Offset: 0x003EE290
    private void Persist()
    {
      if (this.path == null)
      {
        return;
      }
      try
      {
        JsonArray jsonArray = new JsonArray(null);
        foreach (AgentTurnUsageRecord agentTurnUsageRecord in this.records)
        {
          JsonArray jsonArray2 = jsonArray;
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["task_id"] = agentTurnUsageRecord.TaskId;
          jsonObject["thread_id"] = agentTurnUsageRecord.ThreadId;
          jsonObject["turn_id"] = agentTurnUsageRecord.TurnId;
          jsonObject["clicky_slug"] = agentTurnUsageRecord.ClickySlug;
          jsonObject["clicky_name"] = agentTurnUsageRecord.ClickyName;
          jsonObject["finished_at_ms"] = agentTurnUsageRecord.FinishedAt.ToUnixTimeMilliseconds();
          jsonObject["model"] = agentTurnUsageRecord.Model;
          jsonObject["reasoning_effort"] = agentTurnUsageRecord.ReasoningEffort;
          jsonObject["input_tokens"] = agentTurnUsageRecord.InputTokens;
          jsonObject["cached_input_tokens"] = agentTurnUsageRecord.CachedInputTokens;
          jsonObject["output_tokens"] = agentTurnUsageRecord.OutputTokens;
          jsonObject["lease_id"] = agentTurnUsageRecord.LeaseId;
          jsonObject["billed_cost_usd"] = agentTurnUsageRecord.BilledCostUsd;
          jsonArray2.Add(jsonObject);
        }
        Directory.CreateDirectory(Path.GetDirectoryName(this.path));
        AtomicFile.WriteAllBytes(this.path, Encoding.UTF8.GetBytes(jsonArray.ToJsonString(null)));
      }
      catch (Exception ex)
      {
        Log.Warn("agent cost", "ledger write failed: " + ex.Message);
      }
    }

    // Token: 0x06001E73 RID: 7795 RVA: 0x003EE4B0 File Offset: 0x003EE4B0
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <Load>g__Str|30_0(JsonObject o, string key)
    {
      JsonNode jsonNode = o[key];
      if (jsonNode == null)
      {
        return null;
      }
      return jsonNode.GetValue<string>();
    }

    // Token: 0x04000D09 RID: 3337
    private const int MaximumRecords = 500;

    // Token: 0x04000D0A RID: 3338
    [Nullable(2)]
    private readonly string path;

    // Token: 0x04000D0B RID: 3339
    private readonly Lock gate = new Lock();

    // Token: 0x04000D0C RID: 3340
    private readonly List<AgentTurnUsageRecord> records = new List<AgentTurnUsageRecord>();
  }
}
