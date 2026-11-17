using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000035 RID: 53
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyAppStore
  {
    // Token: 0x14000014 RID: 20
    // (add) Token: 0x06000543 RID: 1347 RVA: 0x000F0090 File Offset: 0x000F0090
    // (remove) Token: 0x06000544 RID: 1348 RVA: 0x000F00C8 File Offset: 0x000F00C8
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event Action<string> Changed;

    // Token: 0x06000545 RID: 1349 RVA: 0x000F0100 File Offset: 0x000F0100
    public ClickyAppStore(string rootFolder)
    {
      this.rootFolder = rootFolder;
    }

    // Token: 0x06000546 RID: 1350 RVA: 0x000F013C File Offset: 0x000F013C
    public string FolderPath(string slug)
    {
      string text = new string(slug.Select<char, char>(delegate(char c)
      {
        bool flag = char.IsAsciiLetterOrDigit(c);
        if (!flag)
        {
          bool flag2 = c == '-' || c == '_';
          flag = flag2;
        }
        if (!flag)
        {
          return '_';
        }
        return c;
      }).ToArray<char>());
      return Path.Combine(this.rootFolder, (text.Length == 0) ? "clicky" : text);
    }

    // Token: 0x06000547 RID: 1351 RVA: 0x000F0194 File Offset: 0x000F0194
    public IReadOnlyList<ClickyWorkPiece> Works(string slug)
    {
      this.LoadIfNeeded(slug);
      object obj = this.gate;
      IReadOnlyList<ClickyWorkPiece> readOnlyList;
      lock (obj)
      {
        List<ClickyWorkPiece> list;
        readOnlyList = (this.worksBySlug.TryGetValue(slug, out list) ? list.ToList<ClickyWorkPiece>() : new List<ClickyWorkPiece>());
      }
      return readOnlyList;
    }

    // Token: 0x06000548 RID: 1352 RVA: 0x000F01F4 File Offset: 0x000F01F4
    public ClickyWorkShelf Shelf(string slug)
    {
      return new ClickyWorkShelf(this.Works(slug));
    }

    // Token: 0x06000549 RID: 1353 RVA: 0x000F0204 File Offset: 0x000F0204
    public void LoadIfNeeded(string slug)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (!this.loadedSlugs.Add(slug))
        {
          return;
        }
      }
      List<ClickyWorkPiece> list = new List<ClickyWorkPiece>();
      try
      {
        string text = Path.Combine(this.FolderPath(slug), "works.json");
        if (File.Exists(text))
        {
          list = JsonSerializer.Deserialize<List<ClickyWorkPiece>>(File.ReadAllText(text), ClickyAppStore.Json) ?? new List<ClickyWorkPiece>();
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Agents", "works.json for " + slug + " unreadable: " + ex.Message);
      }
      this.Merge(list, slug, false);
    }

    // Token: 0x0600054A RID: 1354 RVA: 0x000F02C4 File Offset: 0x000F02C4
    public void Merge(IReadOnlyList<ClickyWorkPiece> pieces, string slug, bool persist = true)
    {
      if (pieces.Count == 0)
      {
        return;
      }
      if (persist)
      {
        this.LoadIfNeeded(slug);
      }
      object obj = this.gate;
      List<ClickyWorkPiece> list2;
      lock (obj)
      {
        Dictionary<string, ClickyWorkPiece> dictionary = new Dictionary<string, ClickyWorkPiece>(StringComparer.Ordinal);
        List<ClickyWorkPiece> list;
        if (this.worksBySlug.TryGetValue(slug, out list))
        {
          foreach (ClickyWorkPiece clickyWorkPiece in list)
          {
            dictionary[clickyWorkPiece.Id] = clickyWorkPiece;
          }
        }
        bool flag2 = false;
        foreach (ClickyWorkPiece clickyWorkPiece2 in pieces)
        {
          ClickyWorkPiece clickyWorkPiece3 = clickyWorkPiece2;
          if (!dictionary.ContainsKey(clickyWorkPiece3.Id))
          {
            string clientTurnKey = clickyWorkPiece3.ClientTurnKey;
            if (clientTurnKey != null)
            {
              ClickyWorkPiece clickyWorkPiece4 = dictionary.Values.FirstOrDefault<ClickyWorkPiece>((ClickyWorkPiece w) => w.ClientTurnKey == clientTurnKey);
              if (clickyWorkPiece4 != null)
              {
                clickyWorkPiece3 = clickyWorkPiece3.Reidentified(clickyWorkPiece4.Id, clickyWorkPiece4.FinishedAt);
              }
            }
          }
          ClickyWorkPiece clickyWorkPiece5;
          if (dictionary.TryGetValue(clickyWorkPiece3.Id, out clickyWorkPiece5))
          {
            ClickyWorkPiece clickyWorkPiece6 = clickyWorkPiece3.<Clone>$();
            clickyWorkPiece6.Reply = ((clickyWorkPiece3.Reply.Length < clickyWorkPiece5.Reply.Length) ? clickyWorkPiece5.Reply : clickyWorkPiece3.Reply);
            clickyWorkPiece6.ArtifactPaths = ((clickyWorkPiece3.ArtifactPaths.Count == 0) ? clickyWorkPiece5.ArtifactPaths : clickyWorkPiece3.ArtifactPaths);
            clickyWorkPiece6.Summary = ((clickyWorkPiece3.Summary.Length == 0) ? clickyWorkPiece5.Summary : clickyWorkPiece3.Summary);
            clickyWorkPiece6.Title = ((clickyWorkPiece3.Title.Length == 0) ? clickyWorkPiece5.Title : clickyWorkPiece3.Title);
            ClickyWorkPiece clickyWorkPiece7 = clickyWorkPiece6;
            if (!clickyWorkPiece7.Equals(clickyWorkPiece5))
            {
              dictionary[clickyWorkPiece3.Id] = clickyWorkPiece7;
              flag2 = true;
            }
          }
          else
          {
            dictionary[clickyWorkPiece3.Id] = clickyWorkPiece3;
            flag2 = true;
          }
        }
        if (!flag2)
        {
          return;
        }
        list2 = dictionary.Values.OrderByDescending<ClickyWorkPiece, DateTimeOffset>((ClickyWorkPiece w) => w.FinishedAt).Take<ClickyWorkPiece>(60).ToList<ClickyWorkPiece>();
        this.worksBySlug[slug] = list2;
      }
      if (persist)
      {
        this.Save(slug, list2);
      }
      try
      {
        Action<string> changed = this.Changed;
        if (changed != null)
        {
          changed(slug);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Agents", "ClickyAppStore.Changed: " + ex.Message);
      }
    }

    // Token: 0x0600054B RID: 1355 RVA: 0x000F05C8 File Offset: 0x000F05C8
    private void Save(string slug, List<ClickyWorkPiece> works)
    {
      string folder = this.FolderPath(slug);
      Task.Run(delegate
      {
        try
        {
          Directory.CreateDirectory(folder);
          string text = Path.Combine(folder, "works.json");
          string text2 = text + ".tmp";
          File.WriteAllText(text2, JsonSerializer.Serialize<List<ClickyWorkPiece>>(works, ClickyAppStore.Json));
          File.Move(text2, text, true);
        }
        catch (Exception ex)
        {
          Log.Warn("Agents", "works.json for " + slug + " not saved: " + ex.Message);
        }
      });
    }

    // Token: 0x0600054C RID: 1356 RVA: 0x000F0610 File Offset: 0x000F0610
    public void ForgetAll()
    {
      object obj = this.gate;
      List<string> list;
      lock (obj)
      {
        list = this.worksBySlug.Keys.ToList<string>();
        this.worksBySlug.Clear();
        this.loadedSlugs.Clear();
      }
      try
      {
        Directory.Delete(this.rootFolder, true);
      }
      catch (DirectoryNotFoundException)
      {
      }
      catch (Exception ex)
      {
        Log.Warn("Agents", "forget all works: " + ex.Message);
      }
      foreach (string text in list)
      {
        try
        {
          Action<string> changed = this.Changed;
          if (changed != null)
          {
            changed(text);
          }
        }
        catch
        {
        }
      }
    }

    // Token: 0x0600054D RID: 1357 RVA: 0x000F0714 File Offset: 0x000F0714
    public void Forget(string slug)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.worksBySlug.Remove(slug);
        this.loadedSlugs.Add(slug);
      }
      try
      {
        Directory.Delete(this.FolderPath(slug), true);
      }
      catch (DirectoryNotFoundException)
      {
      }
      catch (Exception ex)
      {
        Log.Warn("Agents", "forget works for " + slug + ": " + ex.Message);
      }
      try
      {
        Action<string> changed = this.Changed;
        if (changed != null)
        {
          changed(slug);
        }
      }
      catch
      {
      }
    }

    // Token: 0x040001EA RID: 490
    public const int MaximumWorksPerClicky = 60;

    // Token: 0x040001EB RID: 491
    private static readonly JsonSerializerOptions Json = new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    // Token: 0x040001EC RID: 492
    private readonly object gate = new object();

    // Token: 0x040001ED RID: 493
    private readonly string rootFolder;

    // Token: 0x040001EE RID: 494
    private readonly Dictionary<string, List<ClickyWorkPiece>> worksBySlug = new Dictionary<string, List<ClickyWorkPiece>>(StringComparer.Ordinal);

    // Token: 0x040001EF RID: 495
    private readonly HashSet<string> loadedSlugs = new HashSet<string>(StringComparer.Ordinal);
  }
}
