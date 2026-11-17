using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using HeyClicky.Core.Voice;
using HeyClicky.Drawing.Media;
using Microsoft.VisualBasic.FileIO;

namespace HeyClicky.App.Home
{
  // Token: 0x020002B6 RID: 694
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class LessonStore
  {
    // Token: 0x170004F7 RID: 1271
    // (get) Token: 0x06001CA0 RID: 7328 RVA: 0x005FB234 File Offset: 0x005FB234
    public static string Root
    {
      get
      {
        return AppPaths.Combine(new string[] { "Lessons" });
      }
    }

    // Token: 0x14000072 RID: 114
    // (add) Token: 0x06001CA1 RID: 7329 RVA: 0x005FB24C File Offset: 0x005FB24C
    // (remove) Token: 0x06001CA2 RID: 7330 RVA: 0x005FB284 File Offset: 0x005FB284
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Changed;

    // Token: 0x06001CA3 RID: 7331 RVA: 0x005FB2BC File Offset: 0x005FB2BC
    public IReadOnlyList<StoredLesson> List()
    {
      IReadOnlyList<StoredLesson> readOnlyList;
      try
      {
        if (!Directory.Exists(LessonStore.Root))
        {
          readOnlyList = Array.Empty<StoredLesson>();
        }
        else
        {
          readOnlyList = (from StoredLesson l in from l in Directory.EnumerateDirectories(LessonStore.Root).Select<string, StoredLesson>(delegate(string folder)
              {
                StoredLesson storedLesson3;
                try
                {
                  StoredLesson.Meta meta = JsonSerializer.Deserialize<StoredLesson.Meta>(File.ReadAllText(Path.Combine(folder, "lesson.json")), LessonStore.Json);
                  StoredLesson storedLesson2;
                  if (meta != null)
                  {
                    StoredLesson storedLesson = new StoredLesson();
                    storedLesson.Folder = folder;
                    storedLesson2 = storedLesson;
                    storedLesson.Data = meta;
                  }
                  else
                  {
                    storedLesson2 = null;
                  }
                  storedLesson3 = storedLesson2;
                }
                catch
                {
                  storedLesson3 = null;
                }
                return storedLesson3;
              })
              where l != null
              select l
            orderby l.Data.CreatedAt descending
            select l).ToList<StoredLesson>();
        }
      }
      catch (Exception ex)
      {
        Log.Warn("lessons", "list: " + ex.Message);
        readOnlyList = Array.Empty<StoredLesson>();
      }
      return readOnlyList;
    }

    // Token: 0x06001CA4 RID: 7332 RVA: 0x005FB398 File Offset: 0x005FB398
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<StoredLesson> SaveAsync(DrawingRecord record, [Nullable(2)] byte[] poster, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, byte[]> photos = null)
    {
      LessonStore.<>c__DisplayClass7_0 CS$<>8__locals1 = new LessonStore.<>c__DisplayClass7_0();
      CS$<>8__locals1.record = record;
      CS$<>8__locals1.poster = poster;
      CS$<>8__locals1.photos = photos;
      StoredLesson storedLesson;
      try
      {
        LessonStore.<>c__DisplayClass7_0 CS$<>8__locals2 = CS$<>8__locals1;
        StoredLesson.Meta meta = new StoredLesson.Meta();
        meta.Id = Guid.NewGuid().ToString("N").Substring(0, 12);
        meta.Title = CS$<>8__locals1.record.Title;
        meta.CreatedAt = CS$<>8__locals1.record.At;
        meta.Question = CS$<>8__locals1.record.Question;
        meta.Lines = CS$<>8__locals1.record.Lines.Select<DrawingRecord.Line, StoredLesson.LineData>((DrawingRecord.Line l) => new StoredLesson.LineData
        {
          Text = l.Text,
          Start = l.Start,
          Length = l.Length
        }).ToList<StoredLesson.LineData>();
        meta.Duration = CS$<>8__locals1.record.Duration;
        meta.CanvasWidth = CS$<>8__locals1.record.Screen.PixelWidth;
        meta.CanvasHeight = CS$<>8__locals1.record.Screen.PixelHeight;
        meta.DisplayIndex = CS$<>8__locals1.record.Screen.Display.Index;
        meta.DisplayDevice = CS$<>8__locals1.record.Screen.Display.DeviceName;
        meta.DisplayBounds = new double[]
        {
          CS$<>8__locals1.record.Screen.Display.BoundsPx.X,
          CS$<>8__locals1.record.Screen.Display.BoundsPx.Y,
          CS$<>8__locals1.record.Screen.Display.BoundsPx.Width,
          CS$<>8__locals1.record.Screen.Display.BoundsPx.Height
        };
        meta.DisplayWork = new double[]
        {
          CS$<>8__locals1.record.Screen.Display.WorkAreaPx.X,
          CS$<>8__locals1.record.Screen.Display.WorkAreaPx.Y,
          CS$<>8__locals1.record.Screen.Display.WorkAreaPx.Width,
          CS$<>8__locals1.record.Screen.Display.WorkAreaPx.Height
        };
        meta.DisplayScale = CS$<>8__locals1.record.Screen.Display.Scale;
        meta.DisplayIsPrimary = CS$<>8__locals1.record.Screen.Display.IsPrimary;
        meta.CursorColorHex = CS$<>8__locals1.record.CursorColorHex;
        StoredLesson.Meta meta2 = meta;
        byte[] narration = CS$<>8__locals1.record.Narration;
        meta2.HasNarration = narration != null && narration.Length > 0 && CS$<>8__locals1.record.NarrationSampleRate > 0;
        CS$<>8__locals2.meta = meta;
        LessonStore.<>c__DisplayClass7_0 CS$<>8__locals3 = CS$<>8__locals1;
        string root = LessonStore.Root;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
        defaultInterpolatedStringHandler.AppendFormatted<DateTimeOffset>(CS$<>8__locals1.record.At.ToLocalTime(), "yyyy-MM-dd HHmmss");
        defaultInterpolatedStringHandler.AppendLiteral(" ");
        defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.meta.Id);
        CS$<>8__locals3.folder = Path.Combine(root, defaultInterpolatedStringHandler.ToStringAndClear());
        await Task.Run(delegate
        {
          Directory.CreateDirectory(CS$<>8__locals1.folder);
          File.WriteAllText(Path.Combine(CS$<>8__locals1.folder, "program.js"), CS$<>8__locals1.record.Program);
          File.WriteAllBytes(Path.Combine(CS$<>8__locals1.folder, "screen.jpg"), CS$<>8__locals1.record.Screen.Jpeg);
          if (CS$<>8__locals1.poster != null)
          {
            File.WriteAllBytes(Path.Combine(CS$<>8__locals1.folder, "poster.png"), CS$<>8__locals1.poster);
          }
          if (CS$<>8__locals1.meta.HasNarration)
          {
            File.WriteAllBytes(Path.Combine(CS$<>8__locals1.folder, "narration.wav"), LessonNarration.ToWav(CS$<>8__locals1.record.Narration, CS$<>8__locals1.record.NarrationSampleRate));
          }
          if (CS$<>8__locals1.photos != null && CS$<>8__locals1.photos.Count > 0)
          {
            string text3 = Path.Combine(CS$<>8__locals1.folder, "photos");
            Directory.CreateDirectory(text3);
            foreach (KeyValuePair<string, byte[]> keyValuePair in CS$<>8__locals1.photos)
            {
              string text4;
              byte[] array;
              keyValuePair.Deconstruct(out text4, out array);
              string text5 = text4;
              byte[] array2 = array;
              File.WriteAllBytes(Path.Combine(text3, PhotoLoader.SavedPhotoFileName(text5)), array2);
            }
          }
          File.WriteAllText(Path.Combine(CS$<>8__locals1.folder, "lesson.json"), JsonSerializer.Serialize<StoredLesson.Meta>(CS$<>8__locals1.meta, LessonStore.Json));
        });
        string text = "lessons";
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 5);
        defaultInterpolatedStringHandler.AppendLiteral("saved \"");
        defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.meta.Title);
        defaultInterpolatedStringHandler.AppendLiteral("\" (");
        defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.meta.Lines.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" lines, ");
        string text2;
        if (!CS$<>8__locals1.meta.HasNarration)
        {
          text2 = "no voice";
        }
        else
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(8, 1);
          defaultInterpolatedStringHandler2.AppendLiteral("voice ");
          defaultInterpolatedStringHandler2.AppendFormatted<double>((double)CS$<>8__locals1.record.Narration.Length / 2.0 / (double)CS$<>8__locals1.record.NarrationSampleRate, "0.0");
          defaultInterpolatedStringHandler2.AppendLiteral(" s");
          text2 = defaultInterpolatedStringHandler2.ToStringAndClear();
        }
        defaultInterpolatedStringHandler.AppendFormatted(text2);
        defaultInterpolatedStringHandler.AppendLiteral(", ");
        IReadOnlyDictionary<string, byte[]> photos2 = CS$<>8__locals1.photos;
        defaultInterpolatedStringHandler.AppendFormatted<int>((photos2 != null) ? photos2.Count : 0);
        defaultInterpolatedStringHandler.AppendLiteral(" photos) to ");
        defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.folder);
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        Action changed = this.Changed;
        if (changed != null)
        {
          changed();
        }
        storedLesson = new StoredLesson
        {
          Folder = CS$<>8__locals1.folder,
          Data = CS$<>8__locals1.meta
        };
      }
      catch (Exception ex)
      {
        Log.Warn("lessons", "save failed: " + ex.Message);
        storedLesson = null;
      }
      return storedLesson;
    }

    // Token: 0x06001CA5 RID: 7333 RVA: 0x005FB3F4 File Offset: 0x005FB3F4
    public void Delete(StoredLesson lesson)
    {
      try
      {
        FileSystem.DeleteDirectory(lesson.Folder, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
        Log.Info("lessons", "deleted \"" + lesson.Data.Title + "\"");
      }
      catch (Exception ex)
      {
        Log.Warn("lessons", "delete: " + ex.Message);
      }
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x04000DB4 RID: 3508
    private static readonly JsonSerializerOptions Json = new JsonSerializerOptions
    {
      WriteIndented = true
    };
  }
}
