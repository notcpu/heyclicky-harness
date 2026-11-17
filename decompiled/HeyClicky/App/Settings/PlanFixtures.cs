using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Billing;
using HeyClicky.Core.Worker;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000CF RID: 207
  [NullableContext(2)]
  [Nullable(0)]
  public static class PlanFixtures
  {
    // Token: 0x060008F6 RID: 2294 RVA: 0x005424B8 File Offset: 0x005424B8
    [NullableContext(1)]
    public static PlanSnapshot Make(string name)
    {
      string text = DateTimeOffset.UtcNow.AddDays(12.0).AddHours(3.0).ToString("O");
      string text2 = DateTimeOffset.UtcNow.AddDays(20.0).ToString("O");
      PlanSnapshot planSnapshot;
      if (!(name == "Free"))
      {
        if (!(name == "Free at cap"))
        {
          if (!(name == "Pro past due"))
          {
            if (!(name == "Pro cancelling"))
            {
              if (!(name == "Max"))
              {
                string text3 = "unlimited";
                PlanUsage planUsage = new PlanUsage(new PlanUsageBucket(40, 0), new PlanUsageBucket(20, 150), null, text);
                string text4 = "on";
                string text5 = "pro";
                string text6 = null;
                string text7 = null;
                PlanSubscription planSubscription = new PlanSubscription("active", false, null, null, null, null, "month");
                bool? flag = new bool?(true);
                planSnapshot = new PlanSnapshot(text3, planUsage, text4, text5, text6, text7, planSubscription, null, null, null, flag, null, null);
              }
              else
              {
                string text8 = "unlimited";
                PlanUsage planUsage2 = new PlanUsage(new PlanUsageBucket(40, 0), new PlanUsageBucket(120, 1000), null, text);
                string text9 = "on";
                string text10 = "max";
                string text11 = null;
                string text12 = null;
                PlanSubscription planSubscription2 = new PlanSubscription("active", false, null, null, null, null, "month");
                bool? flag = new bool?(true);
                planSnapshot = new PlanSnapshot(text8, planUsage2, text9, text10, text11, text12, planSubscription2, null, null, null, null, flag, null);
              }
            }
            else
            {
              string text13 = "unlimited";
              PlanUsage planUsage3 = new PlanUsage(new PlanUsageBucket(40, 0), new PlanUsageBucket(20, 150), null, text);
              string text14 = "on";
              string text15 = "pro";
              string text16 = null;
              string text17 = null;
              PlanSubscription planSubscription3 = new PlanSubscription("active", true, null, text2, null, null, "month");
              bool? flag = new bool?(true);
              planSnapshot = new PlanSnapshot(text13, planUsage3, text14, text15, text16, text17, planSubscription3, null, null, null, flag, null, null);
            }
          }
          else
          {
            string text18 = "unlimited";
            PlanUsage planUsage4 = new PlanUsage(new PlanUsageBucket(40, 0), new PlanUsageBucket(20, 150), null, text);
            string text19 = "on";
            string text20 = "pro";
            string text21 = null;
            string text22 = null;
            PlanSubscription planSubscription4 = new PlanSubscription("past_due", false, null, null, null, null, "month");
            int? num = null;
            int? num2 = null;
            int? num3 = null;
            bool? flag = null;
            bool? flag2 = flag;
            flag = null;
            bool? flag3 = flag;
            flag = null;
            planSnapshot = new PlanSnapshot(text18, planUsage4, text19, text20, text21, text22, planSubscription4, num, num2, num3, flag2, flag3, flag);
          }
        }
        else
        {
          string text23 = "free";
          PlanUsage planUsage5 = new PlanUsage(new PlanUsageBucket(25, 25), new PlanUsageBucket(10, 10), null, text);
          string text24 = "on";
          string text25 = "free";
          string text26 = null;
          string text27 = null;
          PlanSubscription planSubscription5 = null;
          int? num4 = null;
          int? num5 = null;
          int? num6 = null;
          bool? flag = null;
          bool? flag4 = flag;
          flag = null;
          bool? flag5 = flag;
          flag = null;
          planSnapshot = new PlanSnapshot(text23, planUsage5, text24, text25, text26, text27, planSubscription5, num4, num5, num6, flag4, flag5, flag);
        }
      }
      else
      {
        string text28 = "free";
        PlanUsage planUsage6 = new PlanUsage(new PlanUsageBucket(7, 25), new PlanUsageBucket(3, 10), null, text);
        string text29 = "on";
        string text30 = "free";
        string text31 = null;
        string text32 = null;
        PlanSubscription planSubscription6 = null;
        int? num7 = null;
        int? num8 = null;
        int? num9 = null;
        bool? flag = null;
        bool? flag6 = flag;
        flag = null;
        bool? flag7 = flag;
        flag = null;
        planSnapshot = new PlanSnapshot(text28, planUsage6, text29, text30, text31, text32, planSubscription6, num7, num8, num9, flag6, flag7, flag);
      }
      return planSnapshot;
    }

    // Token: 0x17000173 RID: 371
    // (get) Token: 0x060008F7 RID: 2295 RVA: 0x0054286C File Offset: 0x0054286C
    // (set) Token: 0x060008F8 RID: 2296 RVA: 0x00542874 File Offset: 0x00542874
    public static string Current { get; private set; }

    // Token: 0x060008F9 RID: 2297 RVA: 0x0054287C File Offset: 0x0054287C
    public static void Apply(BillingUi billing, string name)
    {
      PlanFixtures.Current = name;
      Log.Info("plan", "force plan state: " + (name ?? "live"));
      if (billing == null)
      {
        return;
      }
      billing.RefreshAsync();
    }

    // Token: 0x04000313 RID: 787
    [Nullable(1)]
    public static readonly string[] Names = new string[] { "Free", "Free at cap", "Pro", "Pro past due", "Pro cancelling", "Max" };
  }
}
