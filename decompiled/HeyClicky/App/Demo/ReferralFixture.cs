using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;

namespace HeyClicky.App.Demo
{
  // Token: 0x02000357 RID: 855
  public static class ReferralFixture
  {
    // Token: 0x06002539 RID: 9529 RVA: 0x0063A550 File Offset: 0x0063A550
    [NullableContext(1)]
    internal static JsonObject ReferralBody(bool earned)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["enabled"] = true;
      jsonObject["code"] = "nilesh";
      jsonObject["share_url"] = "https://heyclicky.com/@nilesh";
      jsonObject["share_display_text"] = "heyclicky.com/@nilesh";
      jsonObject["share_display_prefix"] = "heyclicky.com/@";
      jsonObject["is_code_disabled"] = false;
      string text = "terms";
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["commission_percent"] = 25;
      jsonObject2["commission_months"] = 12;
      jsonObject2["customer_discount_percent"] = 50;
      jsonObject2["customer_discount_months"] = 1;
      jsonObject2["commission_hold_days"] = 30;
      jsonObject2["minimum_payout_usd_cents"] = 2500;
      jsonObject2["payout_contact_email"] = "hi@heyclicky.com";
      jsonObject[text] = jsonObject2;
      string text2 = "stats";
      JsonNode jsonNode;
      if (!earned)
      {
        JsonObject jsonObject3 = new JsonObject(null);
        jsonObject3["referred_count"] = 0;
        jsonObject3["paying_count"] = 0;
        jsonObject3["earned_usd_cents"] = 0;
        jsonObject3["available_usd_cents"] = 0;
        jsonObject3["on_hold_usd_cents"] = 0;
        jsonNode = jsonObject3;
        jsonObject3["paid_out_usd_cents"] = 0;
      }
      else
      {
        JsonObject jsonObject4 = new JsonObject(null);
        jsonObject4["referred_count"] = 2;
        jsonObject4["paying_count"] = 1;
        jsonObject4["earned_usd_cents"] = 6500;
        jsonObject4["available_usd_cents"] = 4000;
        jsonObject4["on_hold_usd_cents"] = 2500;
        jsonObject4["paid_out_usd_cents"] = 0;
        jsonNode = jsonObject4;
        jsonObject4["earned_last_30_days_usd_cents"] = 2500;
      }
      jsonObject[text2] = jsonNode;
      string text3 = "estimator";
      JsonObject jsonObject5 = new JsonObject(null);
      jsonObject5["max_referrals_per_month"] = 50;
      jsonObject5["default_months_retained"] = 6;
      string text4 = "plans";
      <>y__InlineArray2<JsonNode> <>y__InlineArray = default(<>y__InlineArray2<JsonNode>);
      ref JsonNode ptr = ref <PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray2<JsonNode>, JsonNode>(ref <>y__InlineArray, 0);
      JsonObject jsonObject6 = new JsonObject(null);
      jsonObject6["key"] = "pro";
      jsonObject6["name"] = "Pro";
      jsonObject6["monthly_price_usd_cents"] = 2000;
      jsonObject6["default_referrals_per_month"] = 3;
      ptr = jsonObject6;
      ref JsonNode ptr2 = ref <PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray2<JsonNode>, JsonNode>(ref <>y__InlineArray, 1);
      JsonObject jsonObject7 = new JsonObject(null);
      jsonObject7["key"] = "max";
      jsonObject7["name"] = "Max";
      jsonObject7["monthly_price_usd_cents"] = 10000;
      jsonObject7["default_referrals_per_month"] = 1;
      ptr2 = jsonObject7;
      jsonObject5[text4] = new JsonArray(<PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray2<JsonNode>, JsonNode>(in <>y__InlineArray, 2));
      jsonObject[text3] = jsonObject5;
      string text5 = "invited_friends";
      JsonNode jsonNode2;
      if (!earned)
      {
        jsonNode2 = new JsonArray(null);
      }
      else
      {
        <>y__InlineArray2<JsonNode> <>y__InlineArray2 = default(<>y__InlineArray2<JsonNode>);
        ref JsonNode ptr3 = ref <PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray2<JsonNode>, JsonNode>(ref <>y__InlineArray2, 0);
        JsonObject jsonObject8 = new JsonObject(null);
        jsonObject8["display_name"] = "Sam";
        jsonObject8["plan"] = "max";
        jsonObject8["subscribed_months"] = 2;
        jsonObject8["earned_usd_cents"] = 5000;
        jsonObject8["joined_at"] = "2026-08-02T10:00:00Z";
        ptr3 = jsonObject8;
        ref JsonNode ptr4 = ref <PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray2<JsonNode>, JsonNode>(ref <>y__InlineArray2, 1);
        JsonObject jsonObject9 = new JsonObject(null);
        jsonObject9["display_name"] = "Ada";
        jsonObject9["plan"] = "pro";
        jsonObject9["subscribed_months"] = 3;
        jsonObject9["earned_usd_cents"] = 1500;
        jsonObject9["joined_at"] = "2026-07-12T10:00:00Z";
        ptr4 = jsonObject9;
        jsonNode2 = new JsonArray(<PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray2<JsonNode>, JsonNode>(in <>y__InlineArray2, 2));
      }
      jsonObject[text5] = jsonNode2;
      jsonObject["monthly_earnings"] = new JsonArray(null);
      return jsonObject;
    }
  }
}
