using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Browser;

namespace HeyClicky.App.Browser
{
  // Token: 0x0200035D RID: 861
  [NullableContext(1)]
  [Nullable(0)]
  public static class ChromeSetupBriefs
  {
    // Token: 0x06002572 RID: 9586 RVA: 0x0063B458 File Offset: 0x0063B458
    public static string StoreSteps(string browserName)
    {
      string text;
      if (!(browserName == "Edge"))
      {
        if (!(browserName == "Brave"))
        {
          if (!(browserName == "Chrome") && !(browserName == "Chrome Beta") && !(browserName == "Chrome Dev") && !(browserName == "Chrome Canary"))
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(147, 2);
            defaultInterpolatedStringHandler.AppendLiteral("1) The page's blue add button (it may say \"Add to Chrome\" or \"Add to ");
            defaultInterpolatedStringHandler.AppendFormatted(browserName);
            defaultInterpolatedStringHandler.AppendLiteral("\"). 2) The confirmation in the small dialog ");
            defaultInterpolatedStringHandler.AppendFormatted(browserName);
            defaultInterpolatedStringHandler.AppendLiteral(" shows (\"Add extension\" or \"Add\").");
            text = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          else
          {
            text = "1) \"Add to Chrome\" on the page. 2) \"Add extension\" in the small dialog Chrome shows.";
          }
        }
        else
        {
          text = "1) \"Add to Brave\" on the page (it may say \"Add to Chrome\"). 2) \"Add extension\" in the small dialog Brave shows.";
        }
      }
      else
      {
        text = "1) Edge first has to allow extensions from other stores: a bar at the top of the page says \"Allow extensions from other stores\" with an Allow button, then Edge confirms with another Allow (skip both when the bar isn't there: Edge already allows them). 2) \"Add to Chrome\" on the page (some Edge versions say \"Get\"). 3) \"Add extension\" in the small dialog Edge shows.";
      }
      return text;
    }

    // Token: 0x06002573 RID: 9587 RVA: 0x0063B518 File Offset: 0x0063B518
    public static string StoreCaveats(string browserName)
    {
      return "The labels can differ by version: follow what's on screen and skip a step that's already done. If " + browserName + " shows a profile picker first, ask which profile they use for the sites the job needs. If the page says HeyClicky is blocked by their administrator, or that extensions from other stores aren't allowed, say so in one line and stop: the job can go on without it.";
    }

    // Token: 0x06002574 RID: 9588 RVA: 0x0063B52C File Offset: 0x0063B52C
    public static string RemoveOldCopySteps(string browserName)
    {
      return "1) On " + browserName + "'s extensions page, find the old HeyClicky: the second HeyClicky card, the one that's turned off, marked unpacked or showing an error (its ID starts with pijeb), never the new one from the Chrome Web Store. If there's only one HeyClicky, that part is done. 2) Its \"Remove\" button, then \"Remove\" again in the confirmation. Nothing else on the page: leave Developer mode and every other extension as they are, and don't mention them.";
    }

    // Token: 0x06002575 RID: 9589 RVA: 0x0063B540 File Offset: 0x0063B540
    public static string ShowHowFact(string browserName, bool addsAgain, bool afterDrive)
    {
      string text = (afterDrive ? ("[Chrome setup] Your try at adding HeyClicky's " + browserName + " extension on their screen stopped partway, so now they do the rest while you show them how.") : ("[Chrome setup] They're adding HeyClicky's " + browserName + " extension themselves, with you showing them each click."));
      string text2 = (afterDrive ? "First, one short line on where things are now." : (addsAgain ? "First, one short line on what you're doing together and why: swapping in the new version of your extension, which keeps itself up to date (their old preview copy no longer connects)." : "First, one short line on what you're doing together and why: adding your extension, so their jobs can work in the sites they're signed into."));
      string text3 = (addsAgain ? " Once it's added, HeyClicky says it's connected and opens the extensions page, and you'll show them how to remove the old one." : "");
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(249, 7);
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral(" HeyClicky's page in the Chrome Web Store is open in ");
      defaultInterpolatedStringHandler.AppendFormatted(browserName);
      defaultInterpolatedStringHandler.AppendLiteral(", in front. Then point at each step with a click target, one short line per step: ");
      defaultInterpolatedStringHandler.AppendFormatted(ChromeSetupBriefs.StoreSteps(browserName));
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted(ChromeSetupBriefs.StoreCaveats(browserName));
      defaultInterpolatedStringHandler.AppendLiteral(" Don't do the steps yourself, and never press ");
      defaultInterpolatedStringHandler.AppendFormatted(browserName);
      defaultInterpolatedStringHandler.AppendLiteral("'s confirmation for them.");
      defaultInterpolatedStringHandler.AppendFormatted(text3);
      defaultInterpolatedStringHandler.AppendLiteral(" Stop once HeyClicky says it's connected.");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06002576 RID: 9590 RVA: 0x0063B648 File Offset: 0x0063B648
    public static string RemoveOldCopyFact(string browserName)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(432, 3);
      defaultInterpolatedStringHandler.AppendLiteral("[Chrome setup] The new HeyClicky extension is connected. One more thing: their old preview copy of HeyClicky is still in ");
      defaultInterpolatedStringHandler.AppendFormatted(browserName);
      defaultInterpolatedStringHandler.AppendLiteral(" and no longer works, so they remove it. ");
      defaultInterpolatedStringHandler.AppendFormatted(browserName);
      defaultInterpolatedStringHandler.AppendLiteral("'s extensions page is open in front. First, one short line: the new one's in, so the old one can go, one click and you're done. Then point at each step with a click target, one short line per step: ");
      defaultInterpolatedStringHandler.AppendFormatted(ChromeSetupBriefs.RemoveOldCopySteps(browserName));
      defaultInterpolatedStringHandler.AppendLiteral(" Don't do the steps yourself. End with one short line that it's all set.");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06002577 RID: 9591 RVA: 0x0063B6B8 File Offset: 0x0063B6B8
    private static string WhyHere(string browserName)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(306, 2);
      defaultInterpolatedStringHandler.AppendLiteral("This is HeyClicky's own setup, and only you can do it on screen: a job can't use ");
      defaultInterpolatedStringHandler.AppendFormatted(browserName);
      defaultInterpolatedStringHandler.AppendLiteral(" until the extension is in. They pressed \"Get new extension\" (or said yes), which is their yes to removing their old preview copy and adding the new one from the Chrome Web Store, ");
      defaultInterpolatedStringHandler.AppendFormatted(browserName);
      defaultInterpolatedStringHandler.AppendLiteral("'s own Remove and Add confirmations included.");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06002578 RID: 9592 RVA: 0x0063B710 File Offset: 0x0063B710
    private static string LeaveAlone(string browserName)
    {
      return "Do nothing else in " + browserName + ": don't sign in, don't touch Developer mode or any other setting, don't touch any other extension (unpacked or not) or open other pages. If something unexpected is in the way (a sign-in, an error, a profile picker you can't place), stop and say what it is.";
    }

    // Token: 0x06002579 RID: 9593 RVA: 0x0063B724 File Offset: 0x0063B724
    public static string SwapGoal(string browserName, string storeUrl)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1124, 11);
      defaultInterpolatedStringHandler.AppendLiteral("Swap their old preview copy of HeyClicky in ");
      defaultInterpolatedStringHandler.AppendFormatted(browserName);
      defaultInterpolatedStringHandler.AppendLiteral(" for the new one from the Chrome Web Store, in one go. ");
      defaultInterpolatedStringHandler.AppendFormatted(browserName);
      defaultInterpolatedStringHandler.AppendLiteral("'s extensions page is open in front (if it isn't, open ");
      defaultInterpolatedStringHandler.AppendFormatted(ChromiumBrowsers.ExtensionsPageUrl(browserName));
      defaultInterpolatedStringHandler.AppendLiteral(" in ");
      defaultInterpolatedStringHandler.AppendFormatted(browserName);
      defaultInterpolatedStringHandler.AppendLiteral(" by typing it in the address bar). 1) Find the old HeyClicky: the HeyClicky card that's turned off, marked unpacked or showing an error (its ID starts with pijeb). That card is the only thing you touch on this page. If there's no old HeyClicky, go to step 3. 2) Click that card's \"Remove\" button. ");
      defaultInterpolatedStringHandler.AppendFormatted(browserName);
      defaultInterpolatedStringHandler.AppendLiteral(" asks to confirm: press \"Remove\" in that dialog only when it names HeyClicky and you opened it from that card; if it names anything else, press \"Cancel\", end the take-over and say what you see. 3) In the same tab, type ");
      defaultInterpolatedStringHandler.AppendFormatted(storeUrl);
      defaultInterpolatedStringHandler.AppendLiteral(" in the address bar and press Return. 4) On HeyClicky's store page: ");
      defaultInterpolatedStringHandler.AppendFormatted(ChromeSetupBriefs.StoreSteps(browserName));
      defaultInterpolatedStringHandler.AppendLiteral(" Once ");
      defaultInterpolatedStringHandler.AppendFormatted(browserName);
      defaultInterpolatedStringHandler.AppendLiteral(" says HeyClicky was added, or the button turns into a Remove button, it's done: end the take-over. ");
      defaultInterpolatedStringHandler.AppendFormatted(ChromeSetupBriefs.StoreCaveats(browserName));
      defaultInterpolatedStringHandler.AppendLiteral(" Say what you're doing as you go: first one short line on what and why (swapping in the new version, which updates itself: the old one goes first), then a few words before each step (\"Removing the old one\", \"Opening the Chrome Web Store\", \"Adding the new one\", \"Confirming\"). ");
      defaultInterpolatedStringHandler.AppendFormatted(ChromeSetupBriefs.WhyHere(browserName));
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted(ChromeSetupBriefs.LeaveAlone(browserName));
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x0600257A RID: 9594 RVA: 0x0063B83C File Offset: 0x0063B83C
    public static string StoreGoal(string browserName, string storeUrl)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(453, 8);
      defaultInterpolatedStringHandler.AppendLiteral("Add the HeyClicky extension to ");
      defaultInterpolatedStringHandler.AppendFormatted(browserName);
      defaultInterpolatedStringHandler.AppendLiteral(" from the Chrome Web Store. Its HeyClicky page is open in front (if it isn't, open ");
      defaultInterpolatedStringHandler.AppendFormatted(storeUrl);
      defaultInterpolatedStringHandler.AppendLiteral(" in ");
      defaultInterpolatedStringHandler.AppendFormatted(browserName);
      defaultInterpolatedStringHandler.AppendLiteral("). ");
      defaultInterpolatedStringHandler.AppendFormatted(ChromeSetupBriefs.StoreSteps(browserName));
      defaultInterpolatedStringHandler.AppendLiteral(" Once ");
      defaultInterpolatedStringHandler.AppendFormatted(browserName);
      defaultInterpolatedStringHandler.AppendLiteral(" says HeyClicky was added, or the button turns into a Remove button, it's done: end the take-over. ");
      defaultInterpolatedStringHandler.AppendFormatted(ChromeSetupBriefs.StoreCaveats(browserName));
      defaultInterpolatedStringHandler.AppendLiteral(" Say what you're doing as you go: first one short line on what and why (adding the new version of your extension, which updates itself), then a few words before each step (\"Adding it from the Chrome Web Store\", \"Confirming\"). ");
      defaultInterpolatedStringHandler.AppendFormatted(ChromeSetupBriefs.WhyHere(browserName));
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted(ChromeSetupBriefs.LeaveAlone(browserName));
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }
  }
}
