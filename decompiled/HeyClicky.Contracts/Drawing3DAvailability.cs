using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace HeyClicky
{
  // Token: 0x02000032 RID: 50
  [NullableContext(1)]
  [Nullable(0)]
  public static class Drawing3DAvailability
  {
    // Token: 0x1700009E RID: 158
    // (get) Token: 0x06000267 RID: 615 RVA: 0x00020B40 File Offset: 0x00020B40
    private static string MarkerPath
    {
      get
      {
        string text;
        if ((text = Drawing3DAvailability.markerPathOverride) == null)
        {
          text = AppPaths.Combine(new string[] { "3d-unavailable-build.txt" });
        }
        return text;
      }
    }

    // Token: 0x1700009F RID: 159
    // (get) Token: 0x06000268 RID: 616 RVA: 0x00020B60 File Offset: 0x00020B60
    public static string CurrentBuild { get; }

    // Token: 0x170000A0 RID: 160
    // (get) Token: 0x06000269 RID: 617 RVA: 0x00020B68 File Offset: 0x00020B68
    public static bool IsKnownUnavailable
    {
      get
      {
        object gate = Drawing3DAvailability.Gate;
        bool flag3;
        lock (gate)
        {
          bool? flag2 = Drawing3DAvailability.knownUnavailable;
          if (flag2 != null)
          {
            bool valueOrDefault = flag2.GetValueOrDefault();
            flag3 = valueOrDefault;
          }
          else
          {
            try
            {
              Drawing3DAvailability.knownUnavailable = new bool?(File.Exists(Drawing3DAvailability.MarkerPath) && File.ReadAllText(Drawing3DAvailability.MarkerPath).Trim() == Drawing3DAvailability.CurrentBuild);
            }
            catch (Exception)
            {
              Drawing3DAvailability.knownUnavailable = new bool?(false);
            }
            flag3 = Drawing3DAvailability.knownUnavailable.Value;
          }
        }
        return flag3;
      }
    }

    // Token: 0x0600026A RID: 618 RVA: 0x00020C1C File Offset: 0x00020C1C
    public static void MarkUnavailable(string reason)
    {
      object gate = Drawing3DAvailability.Gate;
      lock (gate)
      {
        if (Drawing3DAvailability.knownUnavailable.GetValueOrDefault())
        {
          return;
        }
        Drawing3DAvailability.knownUnavailable = new bool?(true);
        try
        {
          Directory.CreateDirectory(Path.GetDirectoryName(Drawing3DAvailability.MarkerPath));
          File.WriteAllText(Drawing3DAvailability.MarkerPath, Drawing3DAvailability.CurrentBuild);
        }
        catch (Exception ex)
        {
          Log.Warn("3D", "could not remember that 3D is unavailable: " + ex.Message);
        }
      }
      string text = "3D";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(59, 2);
      defaultInterpolatedStringHandler.AppendLiteral("3D is unavailable on this PC in build ");
      defaultInterpolatedStringHandler.AppendFormatted(Drawing3DAvailability.CurrentBuild);
      defaultInterpolatedStringHandler.AppendLiteral(": turns draw in 2D (");
      defaultInterpolatedStringHandler.AppendFormatted(reason);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Warn(text, defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x0600026B RID: 619 RVA: 0x00020D0C File Offset: 0x00020D0C
    public static void WriteFlags(Utf8JsonWriter writer)
    {
      if (Drawing3DAvailability.IsKnownUnavailable)
      {
        writer.WriteBoolean("figure3d", false);
        writer.WriteBoolean("no_3d", true);
        return;
      }
      writer.WriteBoolean("figure3d", true);
    }

    // Token: 0x170000A1 RID: 161
    // (get) Token: 0x0600026C RID: 620 RVA: 0x00020D3C File Offset: 0x00020D3C
    [Nullable(2)]
    public static string Note
    {
      [NullableContext(2)]
      get
      {
        if (!Drawing3DAvailability.IsKnownUnavailable)
        {
          return null;
        }
        return "3D can't be drawn on this PC: never call figure() or scene3d(). Show a thing with a shape as a 2D drawing or its photo instead.";
      }
    }

    // Token: 0x0600026D RID: 621 RVA: 0x00020D4C File Offset: 0x00020D4C
    [NullableContext(2)]
    public static void UseMarkerPathForTests(string markerPath)
    {
      object gate = Drawing3DAvailability.Gate;
      lock (gate)
      {
        Drawing3DAvailability.markerPathOverride = markerPath;
        Drawing3DAvailability.knownUnavailable = null;
      }
    }

    // Token: 0x0600026E RID: 622 RVA: 0x00020D98 File Offset: 0x00020D98
    // Note: this type is marked as 'beforefieldinit'.
    static Drawing3DAvailability()
    {
      AssemblyInformationalVersionAttribute customAttribute = (Assembly.GetEntryAssembly() ?? typeof(Drawing3DAvailability).Assembly).GetCustomAttribute<AssemblyInformationalVersionAttribute>();
      Drawing3DAvailability.CurrentBuild = ((customAttribute != null) ? customAttribute.InformationalVersion : null) ?? "unknown";
    }

    // Token: 0x040000A5 RID: 165
    public const string UnavailableNote = "3D can't be drawn on this PC: never call figure() or scene3d(). Show a thing with a shape as a 2D drawing or its photo instead.";

    // Token: 0x040000A6 RID: 166
    private static readonly object Gate = new object();

    // Token: 0x040000A7 RID: 167
    private static bool? knownUnavailable;

    // Token: 0x040000A8 RID: 168
    [Nullable(2)]
    private static string markerPathOverride;
  }
}
