using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Voice;
using HeyClicky.Drawing;

namespace HeyClicky.App.Home
{
  // Token: 0x020002B4 RID: 692
  [NullableContext(1)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class StoredLesson
  {
    // Token: 0x170004EE RID: 1262
    // (get) Token: 0x06001C8E RID: 7310 RVA: 0x005FADD4 File Offset: 0x005FADD4
    // (set) Token: 0x06001C8F RID: 7311 RVA: 0x005FADDC File Offset: 0x005FADDC
    [RequiredMember]
    public string Folder { get; set; }

    // Token: 0x170004EF RID: 1263
    // (get) Token: 0x06001C90 RID: 7312 RVA: 0x005FADE8 File Offset: 0x005FADE8
    // (set) Token: 0x06001C91 RID: 7313 RVA: 0x005FADF0 File Offset: 0x005FADF0
    [RequiredMember]
    public StoredLesson.Meta Data { get; set; }

    // Token: 0x170004F0 RID: 1264
    // (get) Token: 0x06001C92 RID: 7314 RVA: 0x005FADFC File Offset: 0x005FADFC
    public string ProgramPath
    {
      get
      {
        return Path.Combine(this.Folder, "program.js");
      }
    }

    // Token: 0x170004F1 RID: 1265
    // (get) Token: 0x06001C93 RID: 7315 RVA: 0x005FAE10 File Offset: 0x005FAE10
    public string ScreenPath
    {
      get
      {
        return Path.Combine(this.Folder, "screen.jpg");
      }
    }

    // Token: 0x170004F2 RID: 1266
    // (get) Token: 0x06001C94 RID: 7316 RVA: 0x005FAE24 File Offset: 0x005FAE24
    public string PosterPath
    {
      get
      {
        return Path.Combine(this.Folder, "poster.png");
      }
    }

    // Token: 0x170004F3 RID: 1267
    // (get) Token: 0x06001C95 RID: 7317 RVA: 0x005FAE38 File Offset: 0x005FAE38
    public string NarrationPath
    {
      get
      {
        return Path.Combine(this.Folder, "narration.wav");
      }
    }

    // Token: 0x170004F4 RID: 1268
    // (get) Token: 0x06001C96 RID: 7318 RVA: 0x005FAE4C File Offset: 0x005FAE4C
    public string PhotosFolder
    {
      get
      {
        return Path.Combine(this.Folder, "photos");
      }
    }

    // Token: 0x170004F5 RID: 1269
    // (get) Token: 0x06001C97 RID: 7319 RVA: 0x005FAE60 File Offset: 0x005FAE60
    public double ReplayShift
    {
      get
      {
        if (this.Data.Lines.Count <= 0)
        {
          return 0.0;
        }
        return Math.Max(0.0, this.Data.Lines[0].Start - 0.6);
      }
    }

    // Token: 0x06001C98 RID: 7320 RVA: 0x005FAEB8 File Offset: 0x005FAEB8
    [return: TupleElementNames(new string[] { "Pcm16Mono", "SampleRate" })]
    [return: Nullable(new byte[] { 0, 1 })]
    public ValueTuple<byte[], int>? ReadNarration()
    {
      ValueTuple<byte[], int>? valueTuple;
      if (!this.Data.HasNarration)
      {
        valueTuple = null;
        return valueTuple;
      }
      try
      {
        ValueTuple<byte[], int>? valueTuple2;
        if (!File.Exists(this.NarrationPath))
        {
          valueTuple = null;
          valueTuple2 = valueTuple;
        }
        else
        {
          valueTuple2 = LessonNarration.FromWav(File.ReadAllBytes(this.NarrationPath));
        }
        valueTuple = valueTuple2;
      }
      catch (Exception ex)
      {
        Log.Warn("lessons", "narration read: " + ex.Message);
        valueTuple = null;
      }
      return valueTuple;
    }

    // Token: 0x06001C99 RID: 7321 RVA: 0x005FAF48 File Offset: 0x005FAF48
    [NullableContext(2)]
    public LessonPlaybackScript PlaybackScript()
    {
      LessonPlaybackScript lessonPlaybackScript;
      try
      {
        double shift = this.ReplayShift;
        double num = ((this.Data.DisplayScale > 0.0) ? this.Data.DisplayScale : 1.0);
        double num2 = ((this.Data.DisplayBounds.Length == 4 && this.Data.DisplayBounds[2] > 0.0) ? (this.Data.DisplayBounds[2] / num) : ((double)this.Data.CanvasWidth / num));
        lessonPlaybackScript = new LessonPlaybackScript(File.ReadAllText(this.ProgramPath), File.ReadAllBytes(this.ScreenPath), this.Data.CanvasWidth, this.Data.CanvasHeight, num2, num, this.Data.Lines.Select<StoredLesson.LineData, ValueTuple<double, double?>>((StoredLesson.LineData l) => new ValueTuple<double, double?>(l.Start - shift, l.Length)).ToList<ValueTuple<double, double?>>(), Math.Max(1.0, this.Data.Duration - shift), this.Data.CursorColorHex);
      }
      catch (Exception ex)
      {
        Log.Warn("lessons", "playback read: " + ex.Message);
        lessonPlaybackScript = null;
      }
      return lessonPlaybackScript;
    }

    // Token: 0x06001C9A RID: 7322 RVA: 0x005FB09C File Offset: 0x005FB09C
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public StoredLesson()
    {
    }

    // Token: 0x02000895 RID: 2197
    [Nullable(0)]
    public sealed class LineData
    {
      // Token: 0x17000807 RID: 2055
      // (get) Token: 0x060039D3 RID: 14803 RVA: 0x0068ADE8 File Offset: 0x0068ADE8
      // (set) Token: 0x060039D4 RID: 14804 RVA: 0x0068ADF0 File Offset: 0x0068ADF0
      public string Text { get; set; } = "";

      // Token: 0x17000808 RID: 2056
      // (get) Token: 0x060039D5 RID: 14805 RVA: 0x0068ADFC File Offset: 0x0068ADFC
      // (set) Token: 0x060039D6 RID: 14806 RVA: 0x0068AE04 File Offset: 0x0068AE04
      public double Start { get; set; }

      // Token: 0x17000809 RID: 2057
      // (get) Token: 0x060039D7 RID: 14807 RVA: 0x0068AE10 File Offset: 0x0068AE10
      // (set) Token: 0x060039D8 RID: 14808 RVA: 0x0068AE18 File Offset: 0x0068AE18
      public double? Length { get; set; }
    }

    // Token: 0x02000896 RID: 2198
    [Nullable(0)]
    public sealed class Meta
    {
      // Token: 0x1700080A RID: 2058
      // (get) Token: 0x060039DA RID: 14810 RVA: 0x0068AE38 File Offset: 0x0068AE38
      // (set) Token: 0x060039DB RID: 14811 RVA: 0x0068AE40 File Offset: 0x0068AE40
      public string Id { get; set; } = "";

      // Token: 0x1700080B RID: 2059
      // (get) Token: 0x060039DC RID: 14812 RVA: 0x0068AE4C File Offset: 0x0068AE4C
      // (set) Token: 0x060039DD RID: 14813 RVA: 0x0068AE54 File Offset: 0x0068AE54
      public string Title { get; set; } = "";

      // Token: 0x1700080C RID: 2060
      // (get) Token: 0x060039DE RID: 14814 RVA: 0x0068AE60 File Offset: 0x0068AE60
      // (set) Token: 0x060039DF RID: 14815 RVA: 0x0068AE68 File Offset: 0x0068AE68
      public DateTimeOffset CreatedAt { get; set; }

      // Token: 0x1700080D RID: 2061
      // (get) Token: 0x060039E0 RID: 14816 RVA: 0x0068AE74 File Offset: 0x0068AE74
      // (set) Token: 0x060039E1 RID: 14817 RVA: 0x0068AE7C File Offset: 0x0068AE7C
      public string Question { get; set; } = "";

      // Token: 0x1700080E RID: 2062
      // (get) Token: 0x060039E2 RID: 14818 RVA: 0x0068AE88 File Offset: 0x0068AE88
      // (set) Token: 0x060039E3 RID: 14819 RVA: 0x0068AE90 File Offset: 0x0068AE90
      public List<StoredLesson.LineData> Lines { get; set; } = new List<StoredLesson.LineData>();

      // Token: 0x1700080F RID: 2063
      // (get) Token: 0x060039E4 RID: 14820 RVA: 0x0068AE9C File Offset: 0x0068AE9C
      // (set) Token: 0x060039E5 RID: 14821 RVA: 0x0068AEA4 File Offset: 0x0068AEA4
      public double Duration { get; set; }

      // Token: 0x17000810 RID: 2064
      // (get) Token: 0x060039E6 RID: 14822 RVA: 0x0068AEB0 File Offset: 0x0068AEB0
      // (set) Token: 0x060039E7 RID: 14823 RVA: 0x0068AEB8 File Offset: 0x0068AEB8
      public int CanvasWidth { get; set; }

      // Token: 0x17000811 RID: 2065
      // (get) Token: 0x060039E8 RID: 14824 RVA: 0x0068AEC4 File Offset: 0x0068AEC4
      // (set) Token: 0x060039E9 RID: 14825 RVA: 0x0068AECC File Offset: 0x0068AECC
      public int CanvasHeight { get; set; }

      // Token: 0x17000812 RID: 2066
      // (get) Token: 0x060039EA RID: 14826 RVA: 0x0068AED8 File Offset: 0x0068AED8
      // (set) Token: 0x060039EB RID: 14827 RVA: 0x0068AEE0 File Offset: 0x0068AEE0
      public int DisplayIndex { get; set; }

      // Token: 0x17000813 RID: 2067
      // (get) Token: 0x060039EC RID: 14828 RVA: 0x0068AEEC File Offset: 0x0068AEEC
      // (set) Token: 0x060039ED RID: 14829 RVA: 0x0068AEF4 File Offset: 0x0068AEF4
      public string DisplayDevice { get; set; } = "";

      // Token: 0x17000814 RID: 2068
      // (get) Token: 0x060039EE RID: 14830 RVA: 0x0068AF00 File Offset: 0x0068AF00
      // (set) Token: 0x060039EF RID: 14831 RVA: 0x0068AF08 File Offset: 0x0068AF08
      public double[] DisplayBounds { get; set; } = Array.Empty<double>();

      // Token: 0x17000815 RID: 2069
      // (get) Token: 0x060039F0 RID: 14832 RVA: 0x0068AF14 File Offset: 0x0068AF14
      // (set) Token: 0x060039F1 RID: 14833 RVA: 0x0068AF1C File Offset: 0x0068AF1C
      public double[] DisplayWork { get; set; } = Array.Empty<double>();

      // Token: 0x17000816 RID: 2070
      // (get) Token: 0x060039F2 RID: 14834 RVA: 0x0068AF28 File Offset: 0x0068AF28
      // (set) Token: 0x060039F3 RID: 14835 RVA: 0x0068AF30 File Offset: 0x0068AF30
      public double DisplayScale { get; set; } = 1.0;

      // Token: 0x17000817 RID: 2071
      // (get) Token: 0x060039F4 RID: 14836 RVA: 0x0068AF3C File Offset: 0x0068AF3C
      // (set) Token: 0x060039F5 RID: 14837 RVA: 0x0068AF44 File Offset: 0x0068AF44
      public bool DisplayIsPrimary { get; set; }

      // Token: 0x17000818 RID: 2072
      // (get) Token: 0x060039F6 RID: 14838 RVA: 0x0068AF50 File Offset: 0x0068AF50
      // (set) Token: 0x060039F7 RID: 14839 RVA: 0x0068AF58 File Offset: 0x0068AF58
      public string CursorColorHex { get; set; } = "#3380FF";

      // Token: 0x17000819 RID: 2073
      // (get) Token: 0x060039F8 RID: 14840 RVA: 0x0068AF64 File Offset: 0x0068AF64
      // (set) Token: 0x060039F9 RID: 14841 RVA: 0x0068AF6C File Offset: 0x0068AF6C
      public bool HasNarration { get; set; }
    }
  }
}
