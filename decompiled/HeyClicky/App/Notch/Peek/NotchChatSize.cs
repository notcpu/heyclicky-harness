using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using HeyClicky.Core.Configuration;
using HeyClicky.Core.Telemetry;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000171 RID: 369
  [NullableContext(2)]
  [Nullable(0)]
  public static class NotchChatSize
  {
    // Token: 0x170003B3 RID: 947
    // (get) Token: 0x06001478 RID: 5240 RVA: 0x005AC6B4 File Offset: 0x005AC6B4
    public static double MinimumContentWidth
    {
      get
      {
        return 500.0;
      }
    }

    // Token: 0x170003B4 RID: 948
    // (get) Token: 0x06001479 RID: 5241 RVA: 0x005AC6C0 File Offset: 0x005AC6C0
    public static double MaximumContentWidth
    {
      get
      {
        return 784.0;
      }
    }

    // Token: 0x1400005B RID: 91
    // (add) Token: 0x0600147A RID: 5242 RVA: 0x005AC6CC File Offset: 0x005AC6CC
    // (remove) Token: 0x0600147B RID: 5243 RVA: 0x005AC700 File Offset: 0x005AC700
    public static event Action Changed;

    // Token: 0x170003B5 RID: 949
    // (get) Token: 0x0600147C RID: 5244 RVA: 0x005AC734 File Offset: 0x005AC734
    // (set) Token: 0x0600147D RID: 5245 RVA: 0x005AC73C File Offset: 0x005AC73C
    public static bool IsResizing { get; private set; }

    // Token: 0x0600147E RID: 5246 RVA: 0x005AC744 File Offset: 0x005AC744
    private static void EnsureLoaded()
    {
      if (NotchChatSize.loaded)
      {
        return;
      }
      NotchChatSize.loaded = true;
      try
      {
        if (File.Exists(NotchChatSize.FilePath))
        {
          JsonNode jsonNode = JsonNode.Parse(File.ReadAllText(NotchChatSize.FilePath), null, default(JsonDocumentOptions));
          double? num;
          if (jsonNode == null)
          {
            num = null;
          }
          else
          {
            JsonNode jsonNode2 = jsonNode["width"];
            num = ((jsonNode2 != null) ? new double?(jsonNode2.GetValue<double>()) : null);
          }
          double? num2 = num;
          NotchChatSize.widthDelta = num2.GetValueOrDefault();
          double? num3;
          if (jsonNode == null)
          {
            num3 = null;
          }
          else
          {
            JsonNode jsonNode3 = jsonNode["height"];
            num3 = ((jsonNode3 != null) ? new double?(jsonNode3.GetValue<double>()) : null);
          }
          num2 = num3;
          NotchChatSize.heightDelta = num2.GetValueOrDefault();
        }
      }
      catch (Exception ex)
      {
        Log.Warn("notch-chat", "chat size unreadable: " + ex.Message);
      }
    }

    // Token: 0x170003B6 RID: 950
    // (get) Token: 0x0600147F RID: 5247 RVA: 0x005AC840 File Offset: 0x005AC840
    public static double ContentWidth
    {
      get
      {
        NotchChatSize.EnsureLoaded();
        return NotchChatSize.ClampWidth(500.0 + NotchChatSize.widthDelta);
      }
    }

    // Token: 0x170003B7 RID: 951
    // (get) Token: 0x06001480 RID: 5248 RVA: 0x005AC85C File Offset: 0x005AC85C
    public static double BodyHeight
    {
      get
      {
        NotchChatSize.EnsureLoaded();
        return NotchChatSize.ClampHeight(NotchPeekLayout.BodyHeight + NotchChatSize.heightDelta);
      }
    }

    // Token: 0x170003B8 RID: 952
    // (get) Token: 0x06001481 RID: 5249 RVA: 0x005AC874 File Offset: 0x005AC874
    public static bool IsDefaultSize
    {
      get
      {
        NotchChatSize.EnsureLoaded();
        return NotchChatSize.widthDelta == 0.0 && NotchChatSize.heightDelta == 0.0;
      }
    }

    // Token: 0x06001482 RID: 5250 RVA: 0x005AC8A0 File Offset: 0x005AC8A0
    public static void NoteScreenHeight(double screenHeightDip)
    {
      double num = Math.Min(900.0, Math.Max(120.0, screenHeightDip - 80.0));
      NotchChatSize.maximumBodyHeight = Math.Max(260.0, num - 30.0 - 8.0 - 12.0);
    }

    // Token: 0x06001483 RID: 5251 RVA: 0x005AC908 File Offset: 0x005AC908
    public static void BeginResizing()
    {
      NotchChatSize.IsResizing = true;
      Action changed = NotchChatSize.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x06001484 RID: 5252 RVA: 0x005AC920 File Offset: 0x005AC920
    public static void Resize(double contentWidth, double bodyHeight)
    {
      NotchChatSize.EnsureLoaded();
      double num = Math.Round(NotchChatSize.ClampWidth(contentWidth) - 500.0);
      double num2 = Math.Round(NotchChatSize.ClampHeight(bodyHeight) - NotchPeekLayout.BodyHeight);
      if (num == NotchChatSize.widthDelta && num2 == NotchChatSize.heightDelta)
      {
        return;
      }
      NotchChatSize.widthDelta = num;
      NotchChatSize.heightDelta = num2;
      Action changed = NotchChatSize.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x06001485 RID: 5253 RVA: 0x005AC988 File Offset: 0x005AC988
    public static void EndResizing()
    {
      NotchChatSize.IsResizing = false;
      NotchChatSize.Save();
      Action changed = NotchChatSize.Changed;
      if (changed != null)
      {
        changed();
      }
      string text = "notch_peek_action";
      string text2 = null;
      string text3 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "chat_resized";
      jsonObject["width"] = (int)NotchChatSize.ContentWidth;
      jsonObject["height"] = (int)NotchChatSize.BodyHeight;
      ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
    }

    // Token: 0x06001486 RID: 5254 RVA: 0x005ACA0C File Offset: 0x005ACA0C
    public static void ResetToDefault()
    {
      NotchChatSize.widthDelta = 0.0;
      NotchChatSize.heightDelta = 0.0;
      NotchChatSize.loaded = true;
      NotchChatSize.Save();
      Action changed = NotchChatSize.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x06001487 RID: 5255 RVA: 0x005ACA44 File Offset: 0x005ACA44
    private static void Save()
    {
      try
      {
        if (NotchChatSize.widthDelta == 0.0 && NotchChatSize.heightDelta == 0.0)
        {
          if (File.Exists(NotchChatSize.FilePath))
          {
            File.Delete(NotchChatSize.FilePath);
          }
        }
        else
        {
          Directory.CreateDirectory(Path.GetDirectoryName(NotchChatSize.FilePath));
          string filePath = NotchChatSize.FilePath;
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["width"] = NotchChatSize.widthDelta;
          jsonObject["height"] = NotchChatSize.heightDelta;
          File.WriteAllText(filePath, jsonObject.ToJsonString(null));
        }
      }
      catch (Exception ex)
      {
        Log.Warn("notch-chat", "chat size not saved: " + ex.Message);
      }
    }

    // Token: 0x06001488 RID: 5256 RVA: 0x005ACB10 File Offset: 0x005ACB10
    private static double ClampWidth(double width)
    {
      return Math.Min(Math.Max(width, NotchChatSize.MinimumContentWidth), NotchChatSize.MaximumContentWidth);
    }

    // Token: 0x06001489 RID: 5257 RVA: 0x005ACB28 File Offset: 0x005ACB28
    private static double ClampHeight(double height)
    {
      return Math.Min(Math.Max(height, 260.0), NotchChatSize.maximumBodyHeight);
    }

    // Token: 0x040009D4 RID: 2516
    public const double MinimumBodyHeight = 260.0;

    // Token: 0x040009D5 RID: 2517
    [Nullable(1)]
    private static readonly string FilePath = Path.Combine(AppBundleConfiguration.AppDataDirectory, "notch-chat-size.json");

    // Token: 0x040009D6 RID: 2518
    private static double widthDelta;

    // Token: 0x040009D7 RID: 2519
    private static double heightDelta;

    // Token: 0x040009D8 RID: 2520
    private static bool loaded;

    // Token: 0x040009D9 RID: 2521
    private static double maximumBodyHeight = 900.0;
  }
}
