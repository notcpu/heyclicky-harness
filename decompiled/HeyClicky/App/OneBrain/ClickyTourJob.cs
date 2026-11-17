using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using HeyClicky.Core.Onboarding;
using HeyClicky.Core.Proactive;
using HeyClicky.Core.Tools;

namespace HeyClicky.App.OneBrain
{
  // Token: 0x0200010F RID: 271
  [NullableContext(1)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class ClickyTourJob
  {
    // Token: 0x17000293 RID: 659
    // (get) Token: 0x06000E4D RID: 3661 RVA: 0x00573164 File Offset: 0x00573164
    // (set) Token: 0x06000E4E RID: 3662 RVA: 0x0057316C File Offset: 0x0057316C
    [RequiredMember]
    public string Title { get; set; }

    // Token: 0x17000294 RID: 660
    // (get) Token: 0x06000E4F RID: 3663 RVA: 0x00573178 File Offset: 0x00573178
    // (set) Token: 0x06000E50 RID: 3664 RVA: 0x00573180 File Offset: 0x00573180
    [RequiredMember]
    public string Pitch { get; set; }

    // Token: 0x17000295 RID: 661
    // (get) Token: 0x06000E51 RID: 3665 RVA: 0x0057318C File Offset: 0x0057318C
    // (set) Token: 0x06000E52 RID: 3666 RVA: 0x00573194 File Offset: 0x00573194
    [Nullable(2)]
    public JsonObject SpawnArguments
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000296 RID: 662
    // (get) Token: 0x06000E53 RID: 3667 RVA: 0x005731A0 File Offset: 0x005731A0
    // (set) Token: 0x06000E54 RID: 3668 RVA: 0x005731A8 File Offset: 0x005731A8
    [Nullable(2)]
    public ProactiveSuggestionCard PlannedCard
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000297 RID: 663
    // (get) Token: 0x06000E55 RID: 3669 RVA: 0x005731B4 File Offset: 0x005731B4
    // (set) Token: 0x06000E56 RID: 3670 RVA: 0x005731BC File Offset: 0x005731BC
    [RequiredMember]
    public TourPaperStock Paper { get; set; }

    // Token: 0x17000298 RID: 664
    // (get) Token: 0x06000E57 RID: 3671 RVA: 0x005731C8 File Offset: 0x005731C8
    public string RailKey { get; } = "tour-job:" + Guid.NewGuid().ToString().ToUpperInvariant();

    // Token: 0x06000E58 RID: 3672 RVA: 0x005731D0 File Offset: 0x005731D0
    [return: Nullable(2)]
    public static ClickyTourJob FromPayload(string payload)
    {
      ClickyTourJob.<>c__DisplayClass23_0 CS$<>8__locals1;
      try
      {
        CS$<>8__locals1.o = JsonNode.Parse(payload, null, default(JsonDocumentOptions)) as JsonObject;
      }
      catch (JsonException)
      {
        return null;
      }
      if (CS$<>8__locals1.o == null)
      {
        return null;
      }
      string text = ClickyTourJob.<FromPayload>g__Str|23_0("card", ref CS$<>8__locals1);
      string text2 = ((text != null) ? text.Trim().ToLowerInvariant() : null);
      ProactiveSuggestionCard proactiveSuggestionCard;
      if (text2 != null && DrawnOnboardingTour.PlannedCardsById.TryGetValue(text2, out proactiveSuggestionCard))
      {
        string text3 = ClickyTourJob.<FromPayload>g__Str|23_0("name", ref CS$<>8__locals1);
        string text4 = ((text3 != null) ? text3.Trim() : null) ?? "";
        return new ClickyTourJob
        {
          Title = ClickyTourJob.WholeWordTitle((text4.Length == 0 || text4.Length > 48) ? proactiveSuggestionCard.Title : text4, 80),
          Pitch = proactiveSuggestionCard.Description.Trim(),
          PlannedCard = proactiveSuggestionCard,
          Paper = (TourPaperStocks.Parse(ClickyTourJob.<FromPayload>g__Str|23_0("color", ref CS$<>8__locals1)) ?? TourPaperStocks.ForTitle(proactiveSuggestionCard.Title, proactiveSuggestionCard.Description))
        };
      }
      string text5 = ClickyTourJob.<FromPayload>g__Str|23_0("title", ref CS$<>8__locals1);
      string text6 = ((text5 != null) ? text5.Trim() : null);
      if (text6 == null || text6.Length <= 0)
      {
        return null;
      }
      string text7 = AgentTools.HandOffArguments(CS$<>8__locals1.o.ToJsonString(null));
      if (text7 != null)
      {
        JsonObject jsonObject = JsonNode.Parse(text7, null, default(JsonDocumentOptions)) as JsonObject;
        if (jsonObject != null)
        {
          string text8 = ClickyTourJob.<FromPayload>g__Str|23_0("pitch", ref CS$<>8__locals1);
          string text9 = ((text8 != null) ? text8.Trim() : null) ?? "";
          return new ClickyTourJob
          {
            Title = ClickyTourJob.WholeWordTitle(text6, 80),
            Pitch = text9,
            SpawnArguments = jsonObject,
            Paper = (TourPaperStocks.Parse(ClickyTourJob.<FromPayload>g__Str|23_0("color", ref CS$<>8__locals1)) ?? TourPaperStocks.ForTitle(text6, ClickyTourJob.<FromPayload>g__Str|23_0("pitch", ref CS$<>8__locals1) ?? ""))
          };
        }
      }
      return null;
    }

    // Token: 0x06000E59 RID: 3673 RVA: 0x005733FC File Offset: 0x005733FC
    public static string WholeWordTitle(string title, int maximumLength = 80)
    {
      return DrawnTourBriefs.WholeWordTitle(title, maximumLength);
    }

    // Token: 0x06000E5A RID: 3674 RVA: 0x00573408 File Offset: 0x00573408
    public ProactiveSuggestionCard SuggestionCard(ProactiveCheckReason checkReason)
    {
      ProactiveSuggestionCard plannedCard = this.PlannedCard;
      if (plannedCard != null)
      {
        return plannedCard;
      }
      JsonObject jsonObject;
      if ((jsonObject = this.SpawnArguments) == null)
      {
        jsonObject = new JsonObject(null);
      }
      JsonObject jsonObject2 = jsonObject;
      JsonObject jsonObject3 = jsonObject2["new_clicky"] as JsonObject;
      ProactiveNewClicky proactiveNewClicky;
      if (jsonObject3 != null)
      {
        JsonNode jsonNode = jsonObject3["name"];
        string text = ((jsonNode != null) ? jsonNode.GetValue<string>() : null);
        if (text != null && text.Length > 0)
        {
          string text2 = text;
          JsonNode jsonNode2 = jsonObject3["role"];
          string text3 = ((jsonNode2 != null) ? jsonNode2.GetValue<string>() : null) ?? "";
          JsonNode jsonNode3 = jsonObject3["description"];
          proactiveNewClicky = new ProactiveNewClicky(text2, text3, ((jsonNode3 != null) ? jsonNode3.GetValue<string>() : null) ?? "");
          goto IL_00AB;
        }
      }
      proactiveNewClicky = null;
      IL_00AB:
      ProactiveNewClicky proactiveNewClicky2 = proactiveNewClicky;
      ProactiveSuggestionCard proactiveSuggestionCard = new ProactiveSuggestionCard();
      proactiveSuggestionCard.Title = this.Title;
      proactiveSuggestionCard.Description = ((this.Pitch.Length == 0) ? this.Title : this.Pitch);
      JsonNode jsonNode4 = jsonObject2["task"];
      proactiveSuggestionCard.AgentPrompt = ((jsonNode4 != null) ? jsonNode4.GetValue<string>() : null) ?? this.Title;
      proactiveSuggestionCard.CheckReason = checkReason;
      JsonNode jsonNode5 = jsonObject2["clicky_slug"];
      proactiveSuggestionCard.ClickySlug = ((jsonNode5 != null) ? jsonNode5.GetValue<string>() : null);
      proactiveSuggestionCard.NewClicky = proactiveNewClicky2;
      return proactiveSuggestionCard;
    }

    // Token: 0x06000E5B RID: 3675 RVA: 0x00573548 File Offset: 0x00573548
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public ClickyTourJob()
    {
    }

    // Token: 0x06000E5C RID: 3676 RVA: 0x00573584 File Offset: 0x00573584
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <FromPayload>g__Str|23_0(string key, ref ClickyTourJob.<>c__DisplayClass23_0 A_1)
    {
      JsonValue jsonValue = A_1.o[key] as JsonValue;
      string text;
      if (jsonValue == null || !jsonValue.TryGetValue<string>(out text))
      {
        return null;
      }
      return text;
    }
  }
}
