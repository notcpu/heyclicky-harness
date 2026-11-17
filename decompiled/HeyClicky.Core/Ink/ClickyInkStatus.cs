using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000135 RID: 309
  public static class ClickyInkStatus
  {
    // Token: 0x060013D7 RID: 5079 RVA: 0x003A8B2C File Offset: 0x003A8B2C
    [NullableContext(2)]
    public static ClickyInkStatusState State(string word)
    {
      string text = (word ?? "").TrimWs().Lowered();
      uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
      if (num <= 1860367182U)
      {
        if (num <= 1128467232U)
        {
          if (num <= 585126593U)
          {
            if (num <= 133175215U)
            {
              if (num != 18738364U)
              {
                if (num != 96429129U)
                {
                  if (num != 133175215U)
                  {
                    goto IL_0912;
                  }
                  if (!(text == "live"))
                  {
                    goto IL_0912;
                  }
                }
                else
                {
                  if (!(text == "yellow"))
                  {
                    goto IL_0912;
                  }
                  goto IL_090A;
                }
              }
              else if (!(text == "green"))
              {
                goto IL_0912;
              }
            }
            else if (num <= 508790637U)
            {
              if (num != 250267863U)
              {
                if (num != 508790637U)
                {
                  goto IL_0912;
                }
                if (!(text == "fail"))
                {
                  goto IL_0912;
                }
                goto IL_090E;
              }
              else if (!(text == "ontrack"))
              {
                goto IL_0912;
              }
            }
            else if (num != 563185489U)
            {
              if (num != 585126593U)
              {
                goto IL_0912;
              }
              if (!(text == "needs you"))
              {
                goto IL_0912;
              }
              goto IL_090A;
            }
            else
            {
              if (!(text == "error"))
              {
                goto IL_0912;
              }
              goto IL_090E;
            }
          }
          else if (num <= 736686895U)
          {
            if (num <= 667630371U)
            {
              if (num != 653492337U)
              {
                if (num != 667630371U)
                {
                  goto IL_0912;
                }
                if (!(text == "close"))
                {
                  goto IL_0912;
                }
                goto IL_090A;
              }
              else if (!(text == "done"))
              {
                goto IL_0912;
              }
            }
            else if (num != 730721325U)
            {
              if (num != 736686895U)
              {
                goto IL_0912;
              }
              if (!(text == "won"))
              {
                goto IL_0912;
              }
            }
            else
            {
              if (!(text == "off track"))
              {
                goto IL_0912;
              }
              goto IL_090E;
            }
          }
          else if (num <= 1035581717U)
          {
            if (num != 1000835248U)
            {
              if (num != 1035581717U)
              {
                goto IL_0912;
              }
              if (!(text == "down"))
              {
                goto IL_0912;
              }
              goto IL_090E;
            }
            else
            {
              if (!(text == "missed"))
              {
                goto IL_0912;
              }
              goto IL_090E;
            }
          }
          else if (num != 1089765596U)
          {
            if (num != 1128467232U)
            {
              goto IL_0912;
            }
            if (!(text == "up"))
            {
              goto IL_0912;
            }
          }
          else
          {
            if (!(text == "red"))
            {
              goto IL_0912;
            }
            goto IL_090E;
          }
        }
        else if (num <= 1605874216U)
        {
          if (num <= 1364460424U)
          {
            if (num != 1169454059U)
            {
              if (num != 1319056784U)
              {
                if (num != 1364460424U)
                {
                  goto IL_0912;
                }
                if (!(text == "critical"))
                {
                  goto IL_0912;
                }
                goto IL_090E;
              }
              else if (!(text == "yes"))
              {
                goto IL_0912;
              }
            }
            else
            {
              if (!(text == "orange"))
              {
                goto IL_0912;
              }
              goto IL_090A;
            }
          }
          else if (num <= 1516072627U)
          {
            if (num != 1472109122U)
            {
              if (num != 1516072627U)
              {
                goto IL_0912;
              }
              if (!(text == "blocked"))
              {
                goto IL_0912;
              }
              goto IL_090E;
            }
            else if (!(text == "healthy"))
            {
              goto IL_0912;
            }
          }
          else if (num != 1550717474U)
          {
            if (num != 1605874216U)
            {
              goto IL_0912;
            }
            if (!(text == "watch"))
            {
              goto IL_0912;
            }
            goto IL_090A;
          }
          else if (!(text == "clear"))
          {
            goto IL_0912;
          }
        }
        else if (num <= 1712242932U)
        {
          if (num <= 1630810064U)
          {
            if (num != 1622628984U)
            {
              if (num != 1630810064U)
              {
                goto IL_0912;
              }
              if (!(text == "on"))
              {
                goto IL_0912;
              }
            }
            else
            {
              if (!(text == "bad"))
              {
                goto IL_0912;
              }
              goto IL_090E;
            }
          }
          else if (num != 1647734778U)
          {
            if (num != 1712242932U)
            {
              goto IL_0912;
            }
            if (!(text == "ready"))
            {
              goto IL_0912;
            }
          }
          else
          {
            if (!(text == "no"))
            {
              goto IL_0912;
            }
            goto IL_090E;
          }
        }
        else if (num <= 1714698159U)
        {
          if (num != 1714636656U)
          {
            if (num != 1714698159U)
            {
              goto IL_0912;
            }
            if (!(text == "ok"))
            {
              goto IL_0912;
            }
          }
          else
          {
            if (!(text == "urgent"))
            {
              goto IL_0912;
            }
            goto IL_090E;
          }
        }
        else if (num != 1838498488U)
        {
          if (num != 1860367182U)
          {
            goto IL_0912;
          }
          if (!(text == "stopped"))
          {
            goto IL_0912;
          }
          goto IL_090E;
        }
        else
        {
          if (!(text == "slow"))
          {
            goto IL_0912;
          }
          goto IL_090A;
        }
      }
      else if (num <= 2920338483U)
      {
        if (num <= 2231003889U)
        {
          if (num <= 2032210671U)
          {
            if (num != 1876213440U)
            {
              if (num != 2020464800U)
              {
                if (num != 2032210671U)
                {
                  goto IL_0912;
                }
                if (!(text == "warning"))
                {
                  goto IL_0912;
                }
                goto IL_090A;
              }
              else
              {
                if (!(text == "broken"))
                {
                  goto IL_0912;
                }
                goto IL_090E;
              }
            }
            else
            {
              if (!(text == "amber"))
              {
                goto IL_0912;
              }
              goto IL_090A;
            }
          }
          else if (num <= 2195410209U)
          {
            if (num != 2071622424U)
            {
              if (num != 2195410209U)
              {
                goto IL_0912;
              }
              if (!(text == "late"))
              {
                goto IL_0912;
              }
              goto IL_090E;
            }
            else if (!(text == "pass"))
            {
              goto IL_0912;
            }
          }
          else if (num != 2195628171U)
          {
            if (num != 2231003889U)
            {
              goto IL_0912;
            }
            if (!(text == "warn"))
            {
              goto IL_0912;
            }
            goto IL_090A;
          }
          else
          {
            if (!(text == "overdue"))
            {
              goto IL_0912;
            }
            goto IL_090E;
          }
        }
        else if (num <= 2401173606U)
        {
          if (num <= 2330648023U)
          {
            if (num != 2235156922U)
            {
              if (num != 2330648023U)
              {
                goto IL_0912;
              }
              if (!(text == "on track"))
              {
                goto IL_0912;
              }
            }
            else
            {
              if (!(text == "needs"))
              {
                goto IL_0912;
              }
              goto IL_090A;
            }
          }
          else if (num != 2386851591U)
          {
            if (num != 2401173606U)
            {
              goto IL_0912;
            }
            if (!(text == "caution"))
            {
              goto IL_0912;
            }
            goto IL_090A;
          }
          else if (!(text == "paid"))
          {
            goto IL_0912;
          }
        }
        else if (num <= 2579463996U)
        {
          if (num != 2554978395U)
          {
            if (num != 2579463996U)
            {
              goto IL_0912;
            }
            if (!(text == "pending"))
            {
              goto IL_0912;
            }
            goto IL_090A;
          }
          else
          {
            if (!(text == "at risk"))
            {
              goto IL_0912;
            }
            goto IL_090A;
          }
        }
        else if (num != 2697722899U)
        {
          if (num != 2920338483U)
          {
            goto IL_0912;
          }
          if (!(text == "failing"))
          {
            goto IL_0912;
          }
          goto IL_090E;
        }
        else if (!(text == "steady"))
        {
          goto IL_0912;
        }
      }
      else if (num <= 3769421748U)
      {
        if (num <= 3083032825U)
        {
          if (num <= 3041399728U)
          {
            if (num != 3010534471U)
            {
              if (num != 3041399728U)
              {
                goto IL_0912;
              }
              if (!(text == "passing"))
              {
                goto IL_0912;
              }
            }
            else
            {
              if (!(text == "check"))
              {
                goto IL_0912;
              }
              goto IL_090A;
            }
          }
          else if (num != 3067314403U)
          {
            if (num != 3083032825U)
            {
              goto IL_0912;
            }
            if (!(text == "alert"))
            {
              goto IL_0912;
            }
            goto IL_090E;
          }
          else
          {
            if (!(text == "degraded"))
            {
              goto IL_0912;
            }
            goto IL_090A;
          }
        }
        else if (num <= 3203434221U)
        {
          if (num != 3130977097U)
          {
            if (num != 3203434221U)
            {
              goto IL_0912;
            }
            if (!(text == "fine"))
            {
              goto IL_0912;
            }
          }
          else if (!(text == "okay"))
          {
            goto IL_0912;
          }
        }
        else if (num != 3706554889U)
        {
          if (num != 3769421748U)
          {
            goto IL_0912;
          }
          if (!(text == "failed"))
          {
            goto IL_0912;
          }
          goto IL_090E;
        }
        else
        {
          if (!(text == "behind"))
          {
            goto IL_0912;
          }
          goto IL_090A;
        }
      }
      else if (num <= 3898276594U)
      {
        if (num <= 3795088187U)
        {
          if (num != 3778851499U)
          {
            if (num != 3795088187U)
            {
              goto IL_0912;
            }
            if (!(text == "lost"))
            {
              goto IL_0912;
            }
            goto IL_090E;
          }
          else
          {
            if (!(text == "maybe"))
            {
              goto IL_0912;
            }
            goto IL_090A;
          }
        }
        else if (num != 3867909202U)
        {
          if (num != 3898276594U)
          {
            goto IL_0912;
          }
          if (!(text == "soon"))
          {
            goto IL_0912;
          }
          goto IL_090A;
        }
        else if (!(text == "normal"))
        {
          goto IL_0912;
        }
      }
      else if (num <= 4079720170U)
      {
        if (num != 4007265960U)
        {
          if (num != 4079720170U)
          {
            goto IL_0912;
          }
          if (!(text == "risk"))
          {
            goto IL_0912;
          }
          goto IL_090A;
        }
        else
        {
          if (!(text == "waiting"))
          {
            goto IL_0912;
          }
          goto IL_090A;
        }
      }
      else if (num != 4200608216U)
      {
        if (num != 4212348398U)
        {
          goto IL_0912;
        }
        if (!(text == "great"))
        {
          goto IL_0912;
        }
      }
      else if (!(text == "good"))
      {
        goto IL_0912;
      }
      return ClickyInkStatusState.Ok;
      IL_090A:
      return ClickyInkStatusState.Watch;
      IL_090E:
      return ClickyInkStatusState.Bad;
      IL_0912:
      return ClickyInkStatusState.Off;
    }

    // Token: 0x060013D8 RID: 5080 RVA: 0x003A9450 File Offset: 0x003A9450
    [NullableContext(1)]
    public static string Word([Nullable(2)] string written, ClickyInkStatusState state)
    {
      string text = ((written != null) ? written.TrimWs() : null);
      if (!string.IsNullOrEmpty(text) && text.CharacterCount() <= 9)
      {
        return text.ToUpperInvariant();
      }
      string text2;
      switch (state)
      {
      case ClickyInkStatusState.Ok:
        text2 = "OK";
        break;
      case ClickyInkStatusState.Watch:
        text2 = "WATCH";
        break;
      case ClickyInkStatusState.Bad:
        text2 = "DOWN";
        break;
      default:
        text2 = "—";
        break;
      }
      return text2;
    }
  }
}
