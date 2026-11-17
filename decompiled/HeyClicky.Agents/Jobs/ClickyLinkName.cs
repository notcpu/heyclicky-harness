using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000046 RID: 70
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyLinkName
  {
    // Token: 0x06000637 RID: 1591 RVA: 0x000F4D54 File Offset: 0x000F4D54
    [return: Nullable(2)]
    public static ClickyLinkName.Name For(string urlString)
    {
      Uri uri;
      if (!Uri.TryCreate(urlString, UriKind.Absolute, out uri) || string.IsNullOrEmpty(uri.Host))
      {
        return null;
      }
      string text = uri.Host.ToLowerInvariant();
      object obj;
      if (!text.StartsWith("www.", StringComparison.Ordinal))
      {
        obj = text;
      }
      else
      {
        string text2 = text;
        obj = text2.Substring(4, text2.Length - 4);
      }
      string text3 = Uri.UnescapeDataString(uri.AbsolutePath).ToLowerInvariant();
      string text4 = uri.Query.TrimStart('?').ToLowerInvariant();
      object obj2 = obj;
      string text5 = ClickyLinkName.SiteName(obj2);
      return new ClickyLinkName.Name(ClickyLinkName.Thing(obj2, text3, text4, text5), text5);
    }

    // Token: 0x06000638 RID: 1592 RVA: 0x000F4DE8 File Offset: 0x000F4DE8
    private static bool Has(string path, params string[] parts)
    {
      return parts.Any<string>(new Func<string, bool>(path.Contains));
    }

    // Token: 0x06000639 RID: 1593 RVA: 0x000F4DFC File Offset: 0x000F4DFC
    [return: Nullable(2)]
    private static string Thing(string host, string path, string query, string site)
    {
      if (host == "docs.google.com")
      {
        if (ClickyLinkName.Has(path, new string[] { "/spreadsheets" }))
        {
          return "Google Sheet";
        }
        if (ClickyLinkName.Has(path, new string[] { "/presentation" }))
        {
          return "Google Slides";
        }
        if (ClickyLinkName.Has(path, new string[] { "/forms" }))
        {
          return "Google Form";
        }
        return "Google Doc";
      }
      else
      {
        if (host == "sheets.google.com")
        {
          return "Google Sheet";
        }
        if (host == "slides.google.com")
        {
          return "Google Slides";
        }
        if (host == "drive.google.com")
        {
          return "Drive file";
        }
        if (host == "mail.google.com")
        {
          return "email";
        }
        if (host == "calendar.google.com")
        {
          return "calendar event";
        }
        if (host.StartsWith("maps.google.", StringComparison.Ordinal) || (host.StartsWith("google.", StringComparison.Ordinal) && ClickyLinkName.Has(path, new string[] { "/maps" })))
        {
          return "map";
        }
        if (host == "github.com")
        {
          if (ClickyLinkName.Has(path, new string[] { "/pull/" }))
          {
            return "pull request";
          }
          if (ClickyLinkName.Has(path, new string[] { "/issues/" }))
          {
            return "GitHub issue";
          }
          if (ClickyLinkName.Has(path, new string[] { "/releases" }))
          {
            return "GitHub release";
          }
          if (ClickyLinkName.Has(path, new string[] { "/gist" }))
          {
            return "gist";
          }
          if (path.Split('/', StringSplitOptions.RemoveEmptyEntries).Length < 2)
          {
            return null;
          }
          return "GitHub repo";
        }
        else
        {
          if (host == "gist.github.com")
          {
            return "gist";
          }
          if (host == "linear.app" && ClickyLinkName.Has(path, new string[] { "/issue/" }))
          {
            return "Linear issue";
          }
          bool flag = host == "youtube.com" || host == "youtu.be" || host == "m.youtube.com";
          if (flag)
          {
            if (!ClickyLinkName.Has(path, new string[] { "/playlist" }))
            {
              return "YouTube video";
            }
            return "YouTube playlist";
          }
          else
          {
            if (host == "figma.com")
            {
              return "Figma file";
            }
            if (host.EndsWith("notion.so", StringComparison.Ordinal) || host.EndsWith("notion.site", StringComparison.Ordinal))
            {
              return "Notion page";
            }
            if (host.EndsWith("vercel.app", StringComparison.Ordinal) || host.EndsWith("netlify.app", StringComparison.Ordinal) || host.EndsWith("pages.dev", StringComparison.Ordinal))
            {
              return "website";
            }
            if (ClickyLinkName.Has(path, new string[] { "/cart", "/basket", "/bag" }))
            {
              return site + " cart";
            }
            if (ClickyLinkName.Has(path, new string[] { "/checkout" }))
            {
              return site + " checkout";
            }
            if (ClickyLinkName.Has(path, new string[] { "/wishlist", "/registry" }))
            {
              return site + " wishlist";
            }
            if (ClickyLinkName.Has(path, new string[] { "/order", "/your-orders" }))
            {
              return site + " order";
            }
            if (ClickyLinkName.Has(path, new string[] { "/dp/", "/gp/product/", "/product", "/itm/", "/item", "/listing/", "/p/" }))
            {
              return site + " product";
            }
            if (ClickyLinkName.Has(path, new string[] { "/search", "/s/" }) || query.Contains("q=") || query.Contains("k=") || host.StartsWith("search.", StringComparison.Ordinal))
            {
              return site + " search";
            }
            if (ClickyLinkName.Has(path, new string[] { "/rooms/", "/listing" }))
            {
              return site + " listing";
            }
            if (ClickyLinkName.Has(path, new string[] { "/booking", "/reservation", "/hotel" }))
            {
              return site + " booking";
            }
            if (ClickyLinkName.Has(path, new string[] { "/flights" }))
            {
              return site + " flights";
            }
            if (ClickyLinkName.Has(path, new string[] { "/maps", "/place/", "/dir/" }))
            {
              return "map";
            }
            return null;
          }
        }
      }
    }

    // Token: 0x0600063A RID: 1594 RVA: 0x000F5288 File Offset: 0x000F5288
    public static string SiteName(string host)
    {
      List<string> list = host.Split('.', StringSplitOptions.RemoveEmptyEntries).ToList<string>();
      while (list.Count > 1)
      {
        HashSet<string> endings = ClickyLinkName.Endings;
        List<string> list2 = list;
        if (!endings.Contains(list2[list2.Count - 1]))
        {
          break;
        }
        list.RemoveAt(list.Count - 1);
      }
      string text;
      if (list.Count <= 0)
      {
        text = host;
      }
      else
      {
        List<string> list3 = list;
        text = list3[list3.Count - 1];
      }
      string text2 = text;
      string text3;
      if (ClickyLinkName.KnownSiteNames.TryGetValue(text2, out text3))
      {
        return text3;
      }
      if (text2.Length != 0)
      {
        char c = char.ToUpperInvariant(text2[0]);
        ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(ref c);
        string text4 = text2;
        return readOnlySpan + text4.Substring(1, text4.Length - 1);
      }
      return text2;
    }

    // Token: 0x0600063B RID: 1595 RVA: 0x000F5340 File Offset: 0x000F5340
    // Note: this type is marked as 'beforefieldinit'.
    static ClickyLinkName()
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary["github"] = "GitHub";
      dictionary["youtube"] = "YouTube";
      dictionary["youtu"] = "YouTube";
      dictionary["linkedin"] = "LinkedIn";
      dictionary["ebay"] = "eBay";
      dictionary["airbnb"] = "Airbnb";
      dictionary["booking"] = "Booking.com";
      dictionary["tripadvisor"] = "Tripadvisor";
      dictionary["doordash"] = "DoorDash";
      dictionary["ubereats"] = "Uber Eats";
      dictionary["instacart"] = "Instacart";
      dictionary["bestbuy"] = "Best Buy";
      dictionary["homedepot"] = "Home Depot";
      dictionary["walmart"] = "Walmart";
      dictionary["target"] = "Target";
      dictionary["etsy"] = "Etsy";
      dictionary["amazon"] = "Amazon";
      dictionary["flipkart"] = "Flipkart";
      dictionary["swiggy"] = "Swiggy";
      dictionary["zomato"] = "Zomato";
      dictionary["bigbasket"] = "BigBasket";
      dictionary["blinkit"] = "Blinkit";
      dictionary["zeptonow"] = "Zepto";
      dictionary["myntra"] = "Myntra";
      dictionary["nykaa"] = "Nykaa";
      dictionary["makemytrip"] = "MakeMyTrip";
      dictionary["x"] = "X";
      dictionary["twitter"] = "X";
      dictionary["reddit"] = "Reddit";
      dictionary["figma"] = "Figma";
      dictionary["notion"] = "Notion";
      dictionary["linear"] = "Linear";
      dictionary["stripe"] = "Stripe";
      dictionary["openai"] = "OpenAI";
      dictionary["chatgpt"] = "ChatGPT";
      dictionary["wikipedia"] = "Wikipedia";
      dictionary["imdb"] = "IMDb";
      dictionary["nytimes"] = "NYTimes";
      dictionary["producthunt"] = "Product Hunt";
      dictionary["ycombinator"] = "Y Combinator";
      dictionary["stackoverflow"] = "Stack Overflow";
      dictionary["apple"] = "Apple";
      dictionary["spotify"] = "Spotify";
      dictionary["netflix"] = "Netflix";
      dictionary["google"] = "Google";
      dictionary["kayak"] = "Kayak";
      dictionary["expedia"] = "Expedia";
      dictionary["skyscanner"] = "Skyscanner";
      dictionary["zillow"] = "Zillow";
      dictionary["craigslist"] = "Craigslist";
      ClickyLinkName.KnownSiteNames = dictionary;
      ClickyLinkName.Endings = new HashSet<string>
      {
        "com", "co", "org", "net", "io", "app", "ai", "dev", "in", "uk",
        "au", "ca", "de", "fr", "jp", "us", "so", "site", "me", "be",
        "ly", "tv", "gg"
      };
    }

    // Token: 0x04000252 RID: 594
    private static readonly Dictionary<string, string> KnownSiteNames;

    // Token: 0x04000253 RID: 595
    private static readonly HashSet<string> Endings;

    // Token: 0x0200016E RID: 366
    [Nullable(0)]
    public sealed class Name : IEquatable<ClickyLinkName.Name>
    {
      // Token: 0x06000D27 RID: 3367 RVA: 0x00114BD4 File Offset: 0x00114BD4
      public Name([Nullable(2)] string Thing, string Site)
      {
        this.Thing = Thing;
        this.Site = Site;
        base..ctor();
      }

      // Token: 0x170002B9 RID: 697
      // (get) Token: 0x06000D28 RID: 3368 RVA: 0x00114BEC File Offset: 0x00114BEC
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyLinkName.Name);
        }
      }

      // Token: 0x170002BA RID: 698
      // (get) Token: 0x06000D29 RID: 3369 RVA: 0x00114BF8 File Offset: 0x00114BF8
      // (set) Token: 0x06000D2A RID: 3370 RVA: 0x00114C00 File Offset: 0x00114C00
      [Nullable(2)]
      public string Thing
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x170002BB RID: 699
      // (get) Token: 0x06000D2B RID: 3371 RVA: 0x00114C0C File Offset: 0x00114C0C
      // (set) Token: 0x06000D2C RID: 3372 RVA: 0x00114C14 File Offset: 0x00114C14
      public string Site { get; set; }

      // Token: 0x06000D2D RID: 3373 RVA: 0x00114C20 File Offset: 0x00114C20
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Name");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000D2E RID: 3374 RVA: 0x00114C6C File Offset: 0x00114C6C
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Thing = ");
        builder.Append(this.Thing);
        builder.Append(", Site = ");
        builder.Append(this.Site);
        return true;
      }

      // Token: 0x06000D2F RID: 3375 RVA: 0x00114CA8 File Offset: 0x00114CA8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyLinkName.Name left, ClickyLinkName.Name right)
      {
        return !(left == right);
      }

      // Token: 0x06000D30 RID: 3376 RVA: 0x00114CB4 File Offset: 0x00114CB4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyLinkName.Name left, ClickyLinkName.Name right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000D31 RID: 3377 RVA: 0x00114CC8 File Offset: 0x00114CC8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Thing>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Site>k__BackingField);
      }

      // Token: 0x06000D32 RID: 3378 RVA: 0x00114D08 File Offset: 0x00114D08
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyLinkName.Name);
      }

      // Token: 0x06000D33 RID: 3379 RVA: 0x00114D18 File Offset: 0x00114D18
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyLinkName.Name other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Thing>k__BackingField, other.<Thing>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Site>k__BackingField, other.<Site>k__BackingField));
      }

      // Token: 0x06000D35 RID: 3381 RVA: 0x00114D7C File Offset: 0x00114D7C
      [CompilerGenerated]
      private Name(ClickyLinkName.Name original)
      {
        this.Thing = original.<Thing>k__BackingField;
        this.Site = original.<Site>k__BackingField;
      }

      // Token: 0x06000D36 RID: 3382 RVA: 0x00114D9C File Offset: 0x00114D9C
      [CompilerGenerated]
      public void Deconstruct([Nullable(2)] out string Thing, out string Site)
      {
        Thing = this.Thing;
        Site = this.Site;
      }
    }
  }
}
