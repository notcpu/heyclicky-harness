using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000069 RID: 105
  [NullableContext(1)]
  [Nullable(0)]
  public static class AppTeachingIds
  {
    // Token: 0x06000573 RID: 1395 RVA: 0x00358480 File Offset: 0x00358480
    public static string FrontAppIdentifier([Nullable(2)] string processName)
    {
      if (string.IsNullOrWhiteSpace(processName))
      {
        return "";
      }
      string text;
      if (!AppTeachingIds.ByProcess.TryGetValue(processName.Trim(), out text))
      {
        return processName.Trim();
      }
      return text;
    }

    // Token: 0x06000574 RID: 1396 RVA: 0x003584B8 File Offset: 0x003584B8
    // Note: this type is marked as 'beforefieldinit'.
    static AppTeachingIds()
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
      dictionary["Ableton Live 12 Suite"] = "com.ableton.live";
      dictionary["Ableton Live 12 Standard"] = "com.ableton.live";
      dictionary["Ableton Live 12 Intro"] = "com.ableton.live";
      dictionary["Ableton Live 11 Suite"] = "com.ableton.live";
      dictionary["Ableton Live 12 Lite"] = "com.ableton.live";
      dictionary["Acrobat"] = "com.adobe.Acrobat.Pro";
      dictionary["AfterFX"] = "com.adobe.AfterEffects";
      dictionary["studio64"] = "com.google.android.studio";
      dictionary["Antigravity"] = "com.google.antigravity";
      dictionary["Archi"] = "com.archimatetool.editor";
      dictionary["Arduino IDE"] = "cc.arduino.IDE2";
      dictionary["acad"] = "com.autodesk.AutoCAD2026";
      dictionary["blender"] = "org.blenderfoundation.blender";
      dictionary["Canva"] = "com.canva.canvaeditor";
      dictionary["CapCut"] = "com.lemon.lvoverseas";
      dictionary["CaptureOne"] = "com.captureone.captureone16";
      dictionary["ChatGPT"] = "com.openai.chat";
      dictionary["claude"] = "com.anthropic.claudefordesktop";
      dictionary["Codex"] = "com.openai.codex";
      dictionary["comet"] = "ai.perplexity.comet";
      dictionary["Cursor"] = "com.todesktop.230313mzl4w4u92";
      dictionary["Resolve"] = "com.blackmagic-design.DaVinciResolve";
      dictionary["Discord"] = "com.hnc.Discord";
      dictionary["Docker Desktop"] = "com.electron.dockerdesktop";
      dictionary["MailClient"] = "com.emclient.mail.client";
      dictionary["EXCEL"] = "com.microsoft.Excel";
      dictionary["Figma"] = "com.figma.Desktop";
      dictionary["FL64"] = "com.image-line.flstudio";
      dictionary["FL"] = "com.image-line.flstudio";
      dictionary["Framer"] = "com.framer.electron";
      dictionary["Fusion360"] = "com.autodesk.fusion360";
      dictionary["GitHubDesktop"] = "com.github.GitHubClient";
      dictionary["Godot"] = "org.godotengine.godot";
      dictionary["Illustrator"] = "com.adobe.illustrator";
      dictionary["InDesign"] = "com.adobe.InDesign";
      dictionary["Lightroom"] = "com.adobe.lightroomCC";
      dictionary["Linear"] = "com.linear";
      dictionary["LM Studio"] = "ai.elementlabs.lmstudio";
      dictionary["MATLAB"] = "org.cef.MATLABWindow";
      dictionary["MATLABWindow"] = "org.cef.MATLABWindow";
      dictionary["Notion"] = "notion.id";
      dictionary["obs64"] = "com.obsproject.obs-studio";
      dictionary["Obsidian"] = "md.obsidian";
      dictionary["OUTLOOK"] = "com.microsoft.Outlook";
      dictionary["olk"] = "com.microsoft.Outlook";
      dictionary["PacketTracer"] = "com.netacad.PacketTracer9.0.0";
      dictionary["Photoshop"] = "com.adobe.Photoshop";
      dictionary["POWERPNT"] = "com.microsoft.Powerpoint";
      dictionary["Adobe Premiere Pro"] = "com.adobe.PremierePro";
      dictionary["Rive"] = "app.rive.editor";
      dictionary["slack"] = "com.tinyspeck.slackmacgap";
      dictionary["Superhuman"] = "com.superhuman.electron";
      dictionary["Spotify"] = "com.spotify.client";
      dictionary["ms-teams"] = "com.microsoft.teams2";
      dictionary["Telegram"] = "ru.keepcoder.Telegram";
      dictionary["TouchDesigner"] = "ca.derivative.TouchDesigner";
      dictionary["TradingView"] = "com.tradingview.tradingviewapp.desktop";
      dictionary["Unity"] = "com.unity3d.UnityEditor5.x";
      dictionary["Code"] = "com.microsoft.VSCode";
      dictionary["Code - Insiders"] = "com.microsoft.VSCodeInsiders";
      dictionary["warp"] = "dev.warp.Warp-Stable";
      dictionary["Webull Desktop"] = "com.webull.desktop.v1";
      dictionary["WhatsApp"] = "net.whatsapp.WhatsApp";
      dictionary["WhatsApp.Root"] = "net.whatsapp.WhatsApp";
      dictionary["Windsurf"] = "com.exafunction.windsurf";
      dictionary["WINWORD"] = "com.microsoft.Word";
      dictionary["wps"] = "com.kingsoft.wpsoffice.mac.global";
      AppTeachingIds.ByProcess = dictionary;
    }

    // Token: 0x04000284 RID: 644
    private static readonly Dictionary<string, string> ByProcess;
  }
}
