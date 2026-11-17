using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001BE RID: 446
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkIcon
  {
    // Token: 0x06001778 RID: 6008 RVA: 0x005C097C File Offset: 0x005C097C
    [NullableContext(2)]
    public static string SymbolName(string icon, [Nullable(1)] string label, bool inferring = true)
    {
      string text = ((icon != null) ? icon.Trim() : null);
      if (!string.IsNullOrEmpty(text) && ClickyInkIcon.Exists(text))
      {
        return text;
      }
      if (!inferring)
      {
        return null;
      }
      return ClickyInkIcon.Inferred(label);
    }

    // Token: 0x06001779 RID: 6009 RVA: 0x005C09B4 File Offset: 0x005C09B4
    public static bool Exists(string name)
    {
      Dictionary<string, bool> dictionary = ClickyInkIcon.knownSymbols;
      bool flag3;
      lock (dictionary)
      {
        bool flag2;
        if (ClickyInkIcon.knownSymbols.TryGetValue(name, out flag2))
        {
          flag3 = flag2;
        }
        else
        {
          bool flag4 = ClickyInkIcon.Resolve(name) != null;
          ClickyInkIcon.knownSymbols[name] = flag4;
          flag3 = flag4;
        }
      }
      return flag3;
    }

    // Token: 0x0600177A RID: 6010 RVA: 0x005C0A28 File Offset: 0x005C0A28
    public static char? Resolve(string name)
    {
      string text = name;
      while (text.Length > 0)
      {
        char c;
        if (ClickyInkIcon.fluent.TryGetValue(text, out c))
        {
          return new char?(c);
        }
        int num = text.LastIndexOf('.');
        if (num <= 0)
        {
          break;
        }
        text = text.Substring(0, num);
      }
      foreach (string text2 in name.Split('.', StringSplitOptions.RemoveEmptyEntries))
      {
        string text3;
        char c2;
        if (ClickyInkIcon.families.TryGetValue(text2, out text3) && ClickyInkIcon.fluent.TryGetValue(text3, out c2))
        {
          return new char?(c2);
        }
      }
      return null;
    }

    // Token: 0x0600177B RID: 6011 RVA: 0x005C0AC4 File Offset: 0x005C0AC4
    [return: Nullable(2)]
    public static TextBlock Glyph(string name, double size, Color color, bool bold = false)
    {
      char? c = ClickyInkIcon.Resolve(name);
      if (c != null)
      {
        char valueOrDefault = c.GetValueOrDefault();
        return new TextBlock
        {
          Text = valueOrDefault.ToString(),
          FontFamily = DS.Fonts.Symbols,
          FontSize = size,
          FontWeight = (bold ? FontWeights.Bold : FontWeights.Normal),
          Foreground = Ui.Brush(color),
          IsTextScaleFactorEnabled = false,
          VerticalAlignment = VerticalAlignment.Center,
          HorizontalAlignment = HorizontalAlignment.Center,
          IsHitTestVisible = false
        };
      }
      return null;
    }

    // Token: 0x0600177C RID: 6012 RVA: 0x005C0B50 File Offset: 0x005C0B50
    [return: Nullable(2)]
    public static string Inferred(string label)
    {
      string lowered = label.ToLowerInvariant();
      if (lowered.Length == 0)
      {
        return null;
      }
      Func<string, bool> <>9__0;
      foreach (ValueTuple<string[], string> valueTuple in ClickyInkIcon.labelSymbols)
      {
        string[] item = valueTuple.Item1;
        string item2 = valueTuple.Item2;
        IEnumerable<string> enumerable = item;
        Func<string, bool> func;
        if ((func = <>9__0) == null)
        {
          func = (<>9__0 = (string w) => lowered.Contains(w, StringComparison.Ordinal));
        }
        if (enumerable.Any<string>(func))
        {
          return item2;
        }
      }
      return null;
    }

    // Token: 0x1700042F RID: 1071
    // (get) Token: 0x0600177D RID: 6013 RVA: 0x005C0BD4 File Offset: 0x005C0BD4
    public static IEnumerable<string> KnownNames
    {
      get
      {
        return ClickyInkIcon.fluent.Keys;
      }
    }

    // Token: 0x0600177E RID: 6014 RVA: 0x005C0BE0 File Offset: 0x005C0BE0
    // Note: this type is marked as 'beforefieldinit'.
    static ClickyInkIcon()
    {
      Dictionary<string, char> dictionary = new Dictionary<string, char>(StringComparer.Ordinal);
      dictionary["pencil"] = '\ue70f';
      dictionary["pencil.line"] = '\ue70f';
      dictionary["square.and.pencil"] = '\ue70f';
      dictionary["checkmark"] = '\ue73e';
      dictionary["checkmark.circle"] = '\ue930';
      dictionary["checkmark.circle.fill"] = '\uec61';
      dictionary["checkmark.seal"] = '\uec61';
      dictionary["checkmark.square"] = '\ue73a';
      dictionary["checklist"] = '\ue9d5';
      dictionary["photo"] = '\ue91b';
      dictionary["photo.on.rectangle"] = '\ue91b';
      dictionary["camera"] = '\ue722';
      dictionary["calendar"] = '\ue787';
      dictionary["calendar.badge.plus"] = '\ue787';
      dictionary["clock"] = '\ue823';
      dictionary["alarm"] = '\ue823';
      dictionary["timer"] = '\ue916';
      dictionary["link"] = '\ue71b';
      dictionary["globe"] = '\ue774';
      dictionary["safari"] = '\ue774';
      dictionary["cart"] = '\ue7bf';
      dictionary["bag"] = '\ue7bf';
      dictionary["creditcard"] = '\ue8c7';
      dictionary["dollarsign.circle"] = '\ue8c7';
      dictionary["banknote"] = '\ue8c7';
      dictionary["envelope"] = '\ue715';
      dictionary["envelope.fill"] = '\ue715';
      dictionary["envelope.open"] = '\ue8c3';
      dictionary["tray"] = '\ue7b8';
      dictionary["archivebox"] = '\ue7b8';
      dictionary["questionmark.bubble"] = '\ue9ce';
      dictionary["questionmark.circle"] = '\ue9ce';
      dictionary["questionmark"] = '\ue9ce';
      dictionary["exclamationmark.triangle"] = '\ue7ba';
      dictionary["exclamationmark.circle"] = '\ue783';
      dictionary["music.note"] = '\uec4f';
      dictionary["music.note.list"] = '\uec4f';
      dictionary["headphones"] = '\ue7f6';
      dictionary["doc"] = '\ue8a5';
      dictionary["doc.text"] = '\ue8a5';
      dictionary["doc.text.fill"] = '\ue8a5';
      dictionary["doc.richtext"] = '\ue8a5';
      dictionary["doc.plaintext"] = '\ue8a5';
      dictionary["doc.on.doc"] = '\ue8c8';
      dictionary["doc.on.doc.fill"] = '\ue8c8';
      dictionary["folder"] = '\ue8b7';
      dictionary["folder.fill"] = '\ue8b7';
      dictionary["paperclip"] = '\ue723';
      dictionary["person"] = '\ue77b';
      dictionary["person.2"] = '\ue716';
      dictionary["person.3"] = '\ue716';
      dictionary["person.crop.circle"] = '\ue77b';
      dictionary["mappin"] = '\ue7b7';
      dictionary["mappin.and.ellipse"] = '\ue7b7';
      dictionary["mappin.circle.fill"] = '\ue7b7';
      dictionary["map"] = '\ue826';
      dictionary["location"] = '\ue81d';
      dictionary["magnifyingglass"] = '\ue721';
      dictionary["text.magnifyingglass"] = '\ue721';
      dictionary["square.grid.2x2"] = '\ue8a9';
      dictionary["square.grid.3x3.fill"] = '\ue8a9';
      dictionary["tablecells.fill"] = '\ue8a9';
      dictionary["tablecells"] = '\ue8a9';
      dictionary["list.bullet"] = '\ue8fd';
      dictionary["list.bullet.rectangle"] = '\ue8fd';
      dictionary["arrow.triangle.2.circlepath"] = '\ue895';
      dictionary["arrow.clockwise"] = '\ue72c';
      dictionary["arrow.counterclockwise"] = '\ue7a7';
      dictionary["arrow.up.right"] = '\ue8a7';
      dictionary["arrow.up.right.square"] = '\ue8a7';
      dictionary["square.and.arrow.up"] = '\ue72d';
      dictionary["arrow.down.circle"] = '\ue896';
      dictionary["keyboard"] = '\ue765';
      dictionary["play.fill"] = '\uf5b0';
      dictionary["play"] = '\ue768';
      dictionary["pause.fill"] = '\ue769';
      dictionary["pause"] = '\ue769';
      dictionary["stop.fill"] = '\ue71a';
      dictionary["powerplug.fill"] = '\ue945';
      dictionary["bolt"] = '\ue945';
      dictionary["bolt.fill"] = '\ue945';
      dictionary["rectangle.on.rectangle"] = '\ue7c4';
      dictionary["paintpalette.fill"] = '\ue790';
      dictionary["paintpalette"] = '\ue790';
      dictionary["paintbrush.pointed.fill"] = '\ue771';
      dictionary["paintbrush"] = '\ue771';
      dictionary["ticket.fill"] = '\ue8ec';
      dictionary["ticket"] = '\ue8ec';
      dictionary["tag"] = '\ue8ec';
      dictionary["tram.fill"] = '\ue7c0';
      dictionary["bus.fill"] = '\ue806';
      dictionary["car.fill"] = '\ue804';
      dictionary["car"] = '\ue804';
      dictionary["airplane"] = '\ue709';
      dictionary["engine.combustion"] = '\ue90f';
      dictionary["wrench"] = '\ue90f';
      dictionary["hammer"] = '\ue90f';
      dictionary["sparkles"] = '\uf4a5';
      dictionary["star"] = '\ue734';
      dictionary["star.fill"] = '\ue735';
      dictionary["heart"] = '\ueb51';
      dictionary["heart.fill"] = '\ueb52';
      dictionary["house"] = '\ue80f';
      dictionary["bell"] = '\uea8f';
      dictionary["lock"] = '\ue72e';
      dictionary["gearshape"] = '\ue713';
      dictionary["gear"] = '\ue713';
      dictionary["flag"] = '\ue7c1';
      dictionary["book"] = '\ue82d';
      dictionary["lightbulb"] = '\ue82f';
      dictionary["graduationcap"] = '\ue7be';
      dictionary["phone"] = '\ue717';
      dictionary["video"] = '\ue714';
      dictionary["mic"] = '\ue720';
      dictionary["trash"] = '\ue74d';
      dictionary["bubble.left"] = '\ue8bd';
      dictionary["bubble.left.and.bubble.right"] = '\ue8f2';
      dictionary["message"] = '\ue8bd';
      dictionary["sun.max"] = '\ue706';
      dictionary["moon"] = '\ue708';
      dictionary["cloud"] = '\ue753';
      dictionary["xmark"] = '\ue711';
      dictionary["plus"] = '\ue710';
      dictionary["minus"] = '\ue738';
      dictionary["ellipsis"] = '\ue712';
      dictionary["info.circle"] = '\ue946';
      dictionary["chart.bar"] = '\ue9d2';
      dictionary["chart.line.uptrend.xyaxis"] = '\ue9d2';
      dictionary["chart.pie"] = '\ueb05';
      dictionary["building.2"] = '\ue825';
      dictionary["briefcase"] = '\ue821';
      dictionary["cup.and.saucer"] = '\uec32';
      dictionary["fork.knife"] = '\ued56';
      dictionary["leaf"] = '\uec0a';
      dictionary["desktopcomputer"] = '\ue7f4';
      dictionary["laptopcomputer"] = '\ue7f8';
      dictionary["iphone"] = '\ue8ea';
      dictionary["shield"] = '\uea18';
      dictionary["eye"] = '\ue7b3';
      dictionary["hand.thumbsup"] = '\ue8e1';
      dictionary["crown"] = '\ue734';
      dictionary["trophy"] = '\ue734';
      ClickyInkIcon.fluent = dictionary;
      ClickyInkIcon.knownSymbols = new Dictionary<string, bool>();
      Dictionary<string, string> dictionary2 = new Dictionary<string, string>(StringComparer.Ordinal);
      dictionary2["figure"] = "person";
      dictionary2["person"] = "person";
      dictionary2["people"] = "person.2";
      dictionary2["brain"] = "lightbulb";
      dictionary2["face"] = "person.crop.circle";
      dictionary2["hand"] = "hand.thumbsup";
      dictionary2["hands"] = "hand.thumbsup";
      dictionary2["eye"] = "eye";
      dictionary2["eyes"] = "eye";
      dictionary2["ear"] = "headphones";
      dictionary2["mouth"] = "bubble.left";
      dictionary2["lungs"] = "heart";
      dictionary2["staroflife"] = "heart";
      dictionary2["cross"] = "heart";
      dictionary2["pills"] = "heart";
      dictionary2["stethoscope"] = "heart";
      dictionary2["bandage"] = "heart";
      dictionary2["syringe"] = "heart";
      dictionary2["waveform"] = "music.note";
      dictionary2["bed"] = "house";
      dictionary2["sportscourt"] = "trophy";
      dictionary2["soccerball"] = "trophy";
      dictionary2["basketball"] = "trophy";
      dictionary2["football"] = "trophy";
      dictionary2["baseball"] = "trophy";
      dictionary2["tennisball"] = "trophy";
      dictionary2["volleyball"] = "trophy";
      dictionary2["tennis"] = "trophy";
      dictionary2["dumbbell"] = "trophy";
      dictionary2["medal"] = "trophy";
      dictionary2["rosette"] = "trophy";
      dictionary2["gamecontroller"] = "star";
      dictionary2["dice"] = "star";
      dictionary2["puzzlepiece"] = "square.grid.2x2";
      dictionary2["pawprint"] = "leaf";
      dictionary2["dog"] = "leaf";
      dictionary2["cat"] = "leaf";
      dictionary2["bird"] = "leaf";
      dictionary2["fish"] = "leaf";
      dictionary2["tortoise"] = "leaf";
      dictionary2["hare"] = "leaf";
      dictionary2["ant"] = "leaf";
      dictionary2["ladybug"] = "leaf";
      dictionary2["lizard"] = "leaf";
      dictionary2["tree"] = "leaf";
      dictionary2["camera.macro"] = "leaf";
      dictionary2["carrot"] = "leaf";
      dictionary2["apple"] = "leaf";
      dictionary2["sun"] = "sun.max";
      dictionary2["sunrise"] = "sun.max";
      dictionary2["sunset"] = "sun.max";
      dictionary2["moon"] = "moon";
      dictionary2["sparkle"] = "sparkles";
      dictionary2["cloud"] = "cloud";
      dictionary2["snowflake"] = "cloud";
      dictionary2["wind"] = "cloud";
      dictionary2["umbrella"] = "cloud";
      dictionary2["thermometer"] = "cloud";
      dictionary2["drop"] = "cloud";
      dictionary2["humidity"] = "cloud";
      dictionary2["tornado"] = "cloud";
      dictionary2["hurricane"] = "cloud";
      dictionary2["rainbow"] = "cloud";
      dictionary2["flame"] = "bolt";
      dictionary2["mountain"] = "map";
      dictionary2["globe"] = "globe";
      dictionary2["fork"] = "fork.knife";
      dictionary2["takeoutbag"] = "bag";
      dictionary2["birthday"] = "star";
      dictionary2["wineglass"] = "cup.and.saucer";
      dictionary2["mug"] = "cup.and.saucer";
      dictionary2["cup"] = "cup.and.saucer";
      dictionary2["frying"] = "fork.knife";
      dictionary2["refrigerator"] = "house";
      dictionary2["house"] = "house";
      dictionary2["building"] = "building.2";
      dictionary2["storefront"] = "building.2";
      dictionary2["tent"] = "house";
      dictionary2["car"] = "car";
      dictionary2["bus"] = "bus.fill";
      dictionary2["tram"] = "tram.fill";
      dictionary2["train"] = "tram.fill";
      dictionary2["bicycle"] = "car";
      dictionary2["scooter"] = "car";
      dictionary2["sailboat"] = "airplane";
      dictionary2["ferry"] = "airplane";
      dictionary2["airplane"] = "airplane";
      dictionary2["fuelpump"] = "car";
      dictionary2["map"] = "map";
      dictionary2["mappin"] = "mappin";
      dictionary2["location"] = "location";
      dictionary2["signpost"] = "map";
      dictionary2["suitcase"] = "briefcase";
      dictionary2["briefcase"] = "briefcase";
      dictionary2["bag"] = "bag";
      dictionary2["cart"] = "cart";
      dictionary2["basket"] = "cart";
      dictionary2["gift"] = "bag";
      dictionary2["shippingbox"] = "archivebox";
      dictionary2["archivebox"] = "archivebox";
      dictionary2["tray"] = "tray";
      dictionary2["box"] = "archivebox";
      dictionary2["creditcard"] = "creditcard";
      dictionary2["wallet"] = "creditcard";
      dictionary2["banknote"] = "banknote";
      dictionary2["dollarsign"] = "dollarsign.circle";
      dictionary2["eurosign"] = "dollarsign.circle";
      dictionary2["sterlingsign"] = "dollarsign.circle";
      dictionary2["yensign"] = "dollarsign.circle";
      dictionary2["indianrupeesign"] = "dollarsign.circle";
      dictionary2["bitcoinsign"] = "dollarsign.circle";
      dictionary2["chart"] = "chart.bar";
      dictionary2["percent"] = "chart.pie";
      dictionary2["function"] = "chart.line.uptrend.xyaxis";
      dictionary2["sum"] = "chart.bar";
      dictionary2["number"] = "list.bullet";
      dictionary2["tv"] = "desktopcomputer";
      dictionary2["display"] = "desktopcomputer";
      dictionary2["desktopcomputer"] = "desktopcomputer";
      dictionary2["pc"] = "desktopcomputer";
      dictionary2["server"] = "desktopcomputer";
      dictionary2["macbook"] = "laptopcomputer";
      dictionary2["laptopcomputer"] = "laptopcomputer";
      dictionary2["iphone"] = "iphone";
      dictionary2["ipad"] = "iphone";
      dictionary2["applewatch"] = "iphone";
      dictionary2["wifi"] = "globe";
      dictionary2["antenna"] = "globe";
      dictionary2["network"] = "globe";
      dictionary2["printer"] = "doc";
      dictionary2["scanner"] = "doc";
      dictionary2["keyboard"] = "keyboard";
      dictionary2["computermouse"] = "desktopcomputer";
      dictionary2["music"] = "music.note";
      dictionary2["guitars"] = "music.note";
      dictionary2["pianokeys"] = "music.note";
      dictionary2["speaker"] = "music.note";
      dictionary2["radio"] = "music.note";
      dictionary2["headphones"] = "headphones";
      dictionary2["film"] = "video";
      dictionary2["movieclapper"] = "video";
      dictionary2["video"] = "video";
      dictionary2["camera"] = "camera";
      dictionary2["photo"] = "photo";
      dictionary2["play"] = "play";
      dictionary2["pause"] = "pause";
      dictionary2["mic"] = "mic";
      dictionary2["phone"] = "phone";
      dictionary2["message"] = "message";
      dictionary2["bubble"] = "bubble.left";
      dictionary2["quote"] = "bubble.left";
      dictionary2["envelope"] = "envelope";
      dictionary2["paperplane"] = "envelope";
      dictionary2["at"] = "envelope";
      dictionary2["bell"] = "bell";
      dictionary2["megaphone"] = "bell";
      dictionary2["newspaper"] = "doc.text";
      dictionary2["magazine"] = "book";
      dictionary2["book"] = "book";
      dictionary2["books"] = "book";
      dictionary2["text"] = "doc.text";
      dictionary2["character"] = "doc.text";
      dictionary2["doc"] = "doc";
      dictionary2["folder"] = "folder";
      dictionary2["paperclip"] = "paperclip";
      dictionary2["list"] = "list.bullet";
      dictionary2["checklist"] = "checklist";
      dictionary2["note"] = "doc.text";
      dictionary2["calendar"] = "calendar";
      dictionary2["clock"] = "clock";
      dictionary2["timer"] = "timer";
      dictionary2["alarm"] = "alarm";
      dictionary2["hourglass"] = "clock";
      dictionary2["stopwatch"] = "timer";
      dictionary2["gear"] = "gear";
      dictionary2["gearshape"] = "gearshape";
      dictionary2["wrench"] = "wrench";
      dictionary2["hammer"] = "hammer";
      dictionary2["screwdriver"] = "wrench";
      dictionary2["scissors"] = "pencil";
      dictionary2["ruler"] = "pencil";
      dictionary2["pencil"] = "pencil";
      dictionary2["paintbrush"] = "paintbrush";
      dictionary2["paintpalette"] = "paintpalette";
      dictionary2["eyedropper"] = "paintbrush";
      dictionary2["wand"] = "sparkles";
      dictionary2["lightbulb"] = "lightbulb";
      dictionary2["graduationcap"] = "graduationcap";
      dictionary2["studentdesk"] = "graduationcap";
      dictionary2["backpack"] = "graduationcap";
      dictionary2["atom"] = "sparkles";
      dictionary2["flask"] = "sparkles";
      dictionary2["testtube"] = "sparkles";
      dictionary2["lock"] = "lock";
      dictionary2["key"] = "lock";
      dictionary2["shield"] = "shield";
      dictionary2["exclamationmark"] = "exclamationmark.circle";
      dictionary2["questionmark"] = "questionmark.circle";
      dictionary2["info"] = "info.circle";
      dictionary2["checkmark"] = "checkmark";
      dictionary2["xmark"] = "xmark";
      dictionary2["plus"] = "plus";
      dictionary2["minus"] = "minus";
      dictionary2["trash"] = "trash";
      dictionary2["flag"] = "flag";
      dictionary2["tag"] = "tag";
      dictionary2["ticket"] = "ticket";
      dictionary2["bookmark"] = "tag";
      dictionary2["link"] = "link";
      dictionary2["magnifyingglass"] = "magnifyingglass";
      dictionary2["arrow"] = "arrow.up.right";
      dictionary2["arrowshape"] = "arrow.up.right";
      dictionary2["square"] = "square.grid.2x2";
      dictionary2["circle"] = "info.circle";
      dictionary2["star"] = "star";
      dictionary2["heart"] = "heart";
      dictionary2["crown"] = "crown";
      dictionary2["trophy"] = "trophy";
      dictionary2["bolt"] = "bolt";
      dictionary2["battery"] = "bolt";
      dictionary2["powerplug"] = "powerplug.fill";
      dictionary2["leaf"] = "leaf";
      dictionary2["safari"] = "safari";
      dictionary2["person.crop"] = "person.crop.circle";
      ClickyInkIcon.families = dictionary2;
      ClickyInkIcon.labelSymbols = new ValueTuple<string[], string>[]
      {
        new ValueTuple<string[], string>(new string[] { "bug", "error", "fail", "broken", "blocked" }, "exclamationmark.triangle"),
        new ValueTuple<string[], string>(new string[] { "answered", "replied", "done", "passed", "verified", "check" }, "checkmark.seal"),
        new ValueTuple<string[], string>(new string[] { "inbox", "email", "mail", "repl", "message", "thread", "unread" }, "envelope"),
        new ValueTuple<string[], string>(new string[] { "ask", "request", "question" }, "questionmark.bubble"),
        new ValueTuple<string[], string>(new string[] { "album", "track", "song", "music", "playlist" }, "music.note"),
        new ValueTuple<string[], string>(new string[] { "image", "photo", "picture", "cover" }, "photo"),
        new ValueTuple<string[], string>(new string[] { "page", "site", "web" }, "globe"),
        new ValueTuple<string[], string>(new string[] { "pdf", "guide", "doc", "report", "file" }, "doc.richtext"),
        new ValueTuple<string[], string>(new string[] { "source", "link", "citation" }, "link"),
        new ValueTuple<string[], string>(new string[] { "cart", "item", "order", "purchase", "buy" }, "cart"),
        new ValueTuple<string[], string>(new string[] { "price", "cost", "budget", "revenue", "spend", "$", "€", "£", "₹" }, "dollarsign.circle"),
        new ValueTuple<string[], string>(new string[] { "profile", "people", "person", "user", "friend", "team", "contact" }, "person.2"),
        new ValueTuple<string[], string>(new string[] { "closed", "open", "hour", "time", "deadline", "late" }, "clock"),
        new ValueTuple<string[], string>(new string[] { "built", "date", "day", "year", "week", "month", "when" }, "calendar"),
        new ValueTuple<string[], string>(new string[] { "engine", "motor" }, "engine.combustion"),
        new ValueTuple<string[], string>(new string[] { "stroke", "turn", "cycle", "round", "repeat" }, "arrow.triangle.2.circlepath"),
        new ValueTuple<string[], string>(new string[] { "search", "scan", "checked", "found" }, "magnifyingglass"),
        new ValueTuple<string[], string>(new string[] { "change", "edit", "update" }, "pencil"),
        new ValueTuple<string[], string>(new string[] { "type", "kind", "categor" }, "square.grid.2x2"),
        new ValueTuple<string[], string>(new string[] { "place", "city", "map", "location", "trip" }, "mappin.and.ellipse")
      };
    }

    // Token: 0x04000B6D RID: 2925
    private static readonly Dictionary<string, char> fluent;

    // Token: 0x04000B6E RID: 2926
    private static readonly Dictionary<string, bool> knownSymbols;

    // Token: 0x04000B6F RID: 2927
    private static readonly Dictionary<string, string> families;

    // Token: 0x04000B70 RID: 2928
    [TupleElementNames(new string[] { "Words", "Symbol" })]
    [Nullable(new byte[] { 1, 0, 1, 1, 1 })]
    private static readonly ValueTuple<string[], string>[] labelSymbols;
  }
}
