using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000151 RID: 337
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkMadeLink
  {
    // Token: 0x060014F6 RID: 5366 RVA: 0x003B24CC File Offset: 0x003B24CC
    public static bool IsMadeThingHost(string host)
    {
      host = host.ToLowerInvariant();
      HashSet<string> hashSet = new HashSet<string>
      {
        "mail.google.com", "docs.google.com", "sheets.google.com", "drive.google.com", "calendar.google.com", "notion.so", "www.notion.so", "figma.com", "www.figma.com", "canva.com",
        "www.canva.com", "airtable.com"
      };
      string[] array = new string[] { ".notion.site", ".vercel.app", ".netlify.app", ".github.io", ".pages.dev" };
      return hashSet.Contains(host) || array.Any<string>((string s) => host.EndsWith(s, StringComparison.Ordinal));
    }

    // Token: 0x060014F7 RID: 5367 RVA: 0x003B25E0 File Offset: 0x003B25E0
    public static bool LabelOpensSomething(string label)
    {
      string words = ClickyInkParser.Emphasis.Strip(label).ToLowerInvariant().TrimWs();
      return new string[] { "open ", "open:", "view ", "edit ", "see the draft", "see the page" }.Any<string>((string p) => words.StartsWith(p, StringComparison.Ordinal)) || words == "open";
    }

    // Token: 0x060014F8 RID: 5368 RVA: 0x003B2664 File Offset: 0x003B2664
    [NullableContext(2)]
    public static string Host(string url)
    {
      Uri uri;
      if (string.IsNullOrEmpty(url) || !Uri.TryCreate(url, UriKind.Absolute, out uri) || uri.IsFile)
      {
        return null;
      }
      if (!string.IsNullOrEmpty(uri.Host))
      {
        return uri.Host;
      }
      return null;
    }

    // Token: 0x060014F9 RID: 5369 RVA: 0x003B26A4 File Offset: 0x003B26A4
    public static HashSet<string> Ids(ClickyInkLiveNote.Page page, IReadOnlyList<ClickyInkLiveNote.Block> present)
    {
      HashSet<string> hashSet = new HashSet<string>();
      if (!present.Any<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.Source.Kind == ClickyInkDocument.Kind.Link))
      {
        return hashSet;
      }
      bool flag = page.Blocks.Any<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.Source.Kind == ClickyInkDocument.Kind.File);
      string text = null;
      for (int i = 0; i < present.Count; i++)
      {
        ClickyInkLiveNote.Block block = present[i];
        ClickyInkDocument.Block source = block.Source;
        if (source.Kind == ClickyInkDocument.Kind.Heading)
        {
          text = block.Words.Current;
        }
        else if (source.Kind == ClickyInkDocument.Kind.Link && hashSet.Count < 3 && !ClickyInkGroup.NamesSources(text ?? page.Name.Current))
        {
          string text2 = block.Words.Current;
          string text3 = ClickyInkMadeLink.Host(source["url"]) ?? "";
          ClickyInkDocument.Kind? kind = ((i > 0) ? new ClickyInkDocument.Kind?(present[i - 1].Source.Kind) : null);
          bool flag2 = (i == 0 || present[i - 1].Source.Kind != ClickyInkDocument.Kind.Link) && (i + 1 >= present.Count || present[i + 1].Source.Kind != ClickyInkDocument.Kind.Link);
          if (ClickyInkMadeLink.LabelOpensSomething(text2) || (flag2 && kind.GetValueOrDefault() == ClickyInkDocument.Kind.Answer) || (flag2 && !flag && ClickyInkMadeLink.IsMadeThingHost(text3)))
          {
            hashSet.Add(block.Id);
          }
        }
      }
      return hashSet;
    }

    // Token: 0x060014FA RID: 5370 RVA: 0x003B2854 File Offset: 0x003B2854
    public static ClickyInkMadeLink.Kind KindOf(string url, string label)
    {
      Uri uri;
      Uri.TryCreate(url, UriKind.Absolute, out uri);
      string host = (ClickyInkMadeLink.Host(url) ?? "").ToLowerInvariant();
      string text = ((uri == null || uri.IsFile) ? "" : uri.AbsolutePath).ToLowerInvariant();
      string text2 = ClickyInkParser.Emphasis.Strip(label).ToLowerInvariant();
      if (host == "mail.google.com")
      {
        return new ClickyInkMadeLink.Kind((text2.Contains("draft") || url.Contains("#drafts", StringComparison.Ordinal)) ? "Gmail draft" : "Gmail", "envelope.fill", "#D93025");
      }
      if (host == "sheets.google.com" || (host == "docs.google.com" && text.StartsWith("/spreadsheets", StringComparison.Ordinal)))
      {
        return new ClickyInkMadeLink.Kind("Google Sheet", "tablecells.fill", "#188038");
      }
      if (host == "docs.google.com" && text.StartsWith("/presentation", StringComparison.Ordinal))
      {
        return new ClickyInkMadeLink.Kind("Google Slides", "rectangle.on.rectangle", "#E8A200");
      }
      if (host == "docs.google.com" && text.StartsWith("/forms", StringComparison.Ordinal))
      {
        return new ClickyInkMadeLink.Kind("Google Form", "list.bullet.rectangle", "#7248B9");
      }
      if (host == "docs.google.com")
      {
        return new ClickyInkMadeLink.Kind("Google Doc", "doc.text.fill", "#1A73E8");
      }
      if (host == "drive.google.com")
      {
        return new ClickyInkMadeLink.Kind("Google Drive", "folder.fill", "#1A73E8");
      }
      if (host == "calendar.google.com")
      {
        return new ClickyInkMadeLink.Kind("Calendar event", "calendar", "#1A73E8");
      }
      if (host.EndsWith("notion.so", StringComparison.Ordinal) || host.EndsWith(".notion.site", StringComparison.Ordinal))
      {
        return new ClickyInkMadeLink.Kind("Notion page", "doc.richtext", "#37352F");
      }
      if (host.EndsWith("figma.com", StringComparison.Ordinal))
      {
        return new ClickyInkMadeLink.Kind("Figma file", "paintpalette.fill", "#A259FF");
      }
      if (host.EndsWith("canva.com", StringComparison.Ordinal))
      {
        return new ClickyInkMadeLink.Kind("Canva design", "paintbrush.pointed.fill", "#00A3AD");
      }
      if (host.EndsWith("airtable.com", StringComparison.Ordinal))
      {
        return new ClickyInkMadeLink.Kind("Airtable", "square.grid.3x3.fill", "#D68A00");
      }
      if (new string[] { ".vercel.app", ".netlify.app", ".github.io", ".pages.dev" }.Any<string>((string s) => host.EndsWith(s, StringComparison.Ordinal)))
      {
        return new ClickyInkMadeLink.Kind("Live page", "globe", "#2F6FDB");
      }
      return new ClickyInkMadeLink.Kind((host.Length == 0) ? "Link" : ClickyInkMadeLink.Domain(url), "link", "#2F6FDB");
    }

    // Token: 0x060014FB RID: 5371 RVA: 0x003B2B50 File Offset: 0x003B2B50
    public static string Domain(string source)
    {
      string text = ClickyInkMadeLink.Host(source);
      if (text == null)
      {
        return source;
      }
      if (!text.StartsWith("www.", StringComparison.Ordinal))
      {
        return text;
      }
      string text2 = text;
      return text2.Substring(4, text2.Length - 4);
    }

    // Token: 0x04000922 RID: 2338
    public const string Attribute = "_made";

    // Token: 0x020004F4 RID: 1268
    [Nullable(0)]
    public sealed class Kind : IEquatable<ClickyInkMadeLink.Kind>
    {
      // Token: 0x06003175 RID: 12661 RVA: 0x00439218 File Offset: 0x00439218
      public Kind(string Name, string Symbol, string TintHex)
      {
        this.Name = Name;
        this.Symbol = Symbol;
        this.TintHex = TintHex;
        base..ctor();
      }

      // Token: 0x170008E2 RID: 2274
      // (get) Token: 0x06003176 RID: 12662 RVA: 0x00439238 File Offset: 0x00439238
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkMadeLink.Kind);
        }
      }

      // Token: 0x170008E3 RID: 2275
      // (get) Token: 0x06003177 RID: 12663 RVA: 0x00439244 File Offset: 0x00439244
      // (set) Token: 0x06003178 RID: 12664 RVA: 0x0043924C File Offset: 0x0043924C
      public string Name { get; set; }

      // Token: 0x170008E4 RID: 2276
      // (get) Token: 0x06003179 RID: 12665 RVA: 0x00439258 File Offset: 0x00439258
      // (set) Token: 0x0600317A RID: 12666 RVA: 0x00439260 File Offset: 0x00439260
      public string Symbol { get; set; }

      // Token: 0x170008E5 RID: 2277
      // (get) Token: 0x0600317B RID: 12667 RVA: 0x0043926C File Offset: 0x0043926C
      // (set) Token: 0x0600317C RID: 12668 RVA: 0x00439274 File Offset: 0x00439274
      public string TintHex { get; set; }

      // Token: 0x0600317D RID: 12669 RVA: 0x00439280 File Offset: 0x00439280
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Kind");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600317E RID: 12670 RVA: 0x004392CC File Offset: 0x004392CC
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Name = ");
        builder.Append(this.Name);
        builder.Append(", Symbol = ");
        builder.Append(this.Symbol);
        builder.Append(", TintHex = ");
        builder.Append(this.TintHex);
        return true;
      }

      // Token: 0x0600317F RID: 12671 RVA: 0x0043932C File Offset: 0x0043932C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkMadeLink.Kind left, ClickyInkMadeLink.Kind right)
      {
        return !(left == right);
      }

      // Token: 0x06003180 RID: 12672 RVA: 0x00439338 File Offset: 0x00439338
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkMadeLink.Kind left, ClickyInkMadeLink.Kind right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003181 RID: 12673 RVA: 0x0043934C File Offset: 0x0043934C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Symbol>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TintHex>k__BackingField);
      }

      // Token: 0x06003182 RID: 12674 RVA: 0x004393B0 File Offset: 0x004393B0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkMadeLink.Kind);
      }

      // Token: 0x06003183 RID: 12675 RVA: 0x004393C0 File Offset: 0x004393C0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkMadeLink.Kind other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Symbol>k__BackingField, other.<Symbol>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TintHex>k__BackingField, other.<TintHex>k__BackingField));
      }

      // Token: 0x06003185 RID: 12677 RVA: 0x0043943C File Offset: 0x0043943C
      [CompilerGenerated]
      private Kind(ClickyInkMadeLink.Kind original)
      {
        this.Name = original.<Name>k__BackingField;
        this.Symbol = original.<Symbol>k__BackingField;
        this.TintHex = original.<TintHex>k__BackingField;
      }

      // Token: 0x06003186 RID: 12678 RVA: 0x00439468 File Offset: 0x00439468
      [CompilerGenerated]
      public void Deconstruct(out string Name, out string Symbol, out string TintHex)
      {
        Name = this.Name;
        Symbol = this.Symbol;
        TintHex = this.TintHex;
      }
    }
  }
}
