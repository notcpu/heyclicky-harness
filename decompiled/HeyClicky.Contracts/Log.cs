using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace HeyClicky
{
  // Token: 0x02000031 RID: 49
  [NullableContext(1)]
  [Nullable(0)]
  public static class Log
  {
    // Token: 0x1700009D RID: 157
    // (get) Token: 0x06000261 RID: 609 RVA: 0x00020878 File Offset: 0x00020878
    public static string Directory
    {
      get
      {
        return Log.LogDir;
      }
    }

    // Token: 0x06000262 RID: 610 RVA: 0x00020880 File Offset: 0x00020880
    public static void Info(string area, string message)
    {
      Log.Write("INF", area, message);
    }

    // Token: 0x06000263 RID: 611 RVA: 0x00020890 File Offset: 0x00020890
    public static void Warn(string area, string message)
    {
      Log.Write("WRN", area, message);
    }

    // Token: 0x06000264 RID: 612 RVA: 0x000208A0 File Offset: 0x000208A0
    public static void Error(string area, string message, [Nullable(2)] Exception ex = null)
    {
      string text = "ERR";
      string text2;
      if (ex != null)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
        defaultInterpolatedStringHandler.AppendFormatted(message);
        defaultInterpolatedStringHandler.AppendLiteral(": ");
        defaultInterpolatedStringHandler.AppendFormatted<Exception>(ex);
        text2 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text2 = message;
      }
      Log.Write(text, area, text2);
    }

    // Token: 0x06000265 RID: 613 RVA: 0x000208EC File Offset: 0x000208EC
    private static void Write(string level, string area, string message)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 4);
      defaultInterpolatedStringHandler.AppendFormatted<DateTimeOffset>(DateTimeOffset.Now, "HH:mm:ss.fff");
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted(level);
      defaultInterpolatedStringHandler.AppendLiteral(" [");
      defaultInterpolatedStringHandler.AppendFormatted(area);
      defaultInterpolatedStringHandler.AppendLiteral("] ");
      defaultInterpolatedStringHandler.AppendFormatted(message);
      string text = defaultInterpolatedStringHandler.ToStringAndClear();
      object gate = Log.Gate;
      lock (gate)
      {
        try
        {
          if (Log.writer == null)
          {
            global::System.IO.Directory.CreateDirectory(Log.LogDir);
            string processPath = Environment.ProcessPath;
            string text2 = ((processPath != null) ? Path.GetFileNameWithoutExtension(processPath) : "heyclicky");
            string logDir = Log.LogDir;
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 3);
            defaultInterpolatedStringHandler.AppendFormatted(text2);
            defaultInterpolatedStringHandler.AppendLiteral("-");
            defaultInterpolatedStringHandler.AppendFormatted<DateTime>(DateTime.Now, "yyyyMMdd");
            defaultInterpolatedStringHandler.AppendLiteral("-");
            defaultInterpolatedStringHandler.AppendFormatted<int>(Environment.ProcessId);
            defaultInterpolatedStringHandler.AppendLiteral(".log");
            Log.writer = new StreamWriter(new FileStream(Path.Combine(logDir, defaultInterpolatedStringHandler.ToStringAndClear()), FileMode.Append, FileAccess.Write, FileShare.Read | FileShare.Delete))
            {
              AutoFlush = true
            };
          }
          Log.writer.WriteLine(text);
        }
        catch (Exception ex)
        {
          Log.writer = null;
          try
          {
            string text3 = Path.Combine(Log.LogDir, "logger-failures.txt");
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 5);
            defaultInterpolatedStringHandler.AppendFormatted<DateTimeOffset>(DateTimeOffset.Now, "O");
            defaultInterpolatedStringHandler.AppendLiteral(" pid ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(Environment.ProcessId);
            defaultInterpolatedStringHandler.AppendLiteral(": ");
            defaultInterpolatedStringHandler.AppendFormatted(ex.GetType().Name);
            defaultInterpolatedStringHandler.AppendLiteral(": ");
            defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
            defaultInterpolatedStringHandler.AppendFormatted(Environment.NewLine);
            File.AppendAllText(text3, defaultInterpolatedStringHandler.ToStringAndClear());
          }
          catch
          {
          }
        }
      }
    }

    // Token: 0x040000A2 RID: 162
    private static readonly object Gate = new object();

    // Token: 0x040000A3 RID: 163
    private static readonly string LogDir = AppPaths.Combine(new string[] { "logs" });

    // Token: 0x040000A4 RID: 164
    [Nullable(2)]
    private static StreamWriter writer;
  }
}
