using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace HeyClicky.Core.Browser
{
  // Token: 0x020001E3 RID: 483
  [NullableContext(1)]
  [Nullable(0)]
  public static class BrowserUploads
  {
    // Token: 0x06001BBD RID: 7101 RVA: 0x003DD5E8 File Offset: 0x003DD5E8
    public static string OlderExtensionChunkScript(int fileIndex, string base64, bool first)
    {
      string text = (first ? "globalThis.__heyClickyUploadDisarm?.(); globalThis.__heyClickyUpload = [];\n" : "");
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(48, 1);
      defaultInterpolatedStringHandler.AppendLiteral("(globalThis.__heyClickyUpload ??= [])[");
      defaultInterpolatedStringHandler.AppendFormatted<int>(fileIndex);
      defaultInterpolatedStringHandler.AppendLiteral("] ??= [];\n");
      string text2 = defaultInterpolatedStringHandler.ToStringAndClear();
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(53, 2);
      defaultInterpolatedStringHandler.AppendLiteral("globalThis.__heyClickyUpload[");
      defaultInterpolatedStringHandler.AppendFormatted<int>(fileIndex);
      defaultInterpolatedStringHandler.AppendLiteral("].push('");
      defaultInterpolatedStringHandler.AppendFormatted(base64);
      defaultInterpolatedStringHandler.AppendLiteral("');\nreturn true;");
      return text + text2 + defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06001BBE RID: 7102 RVA: 0x003DD680 File Offset: 0x003DD680
    public static bool IsJobsOwnFile(string path, [Nullable(2)] string jobFolder)
    {
      if (string.IsNullOrEmpty(jobFolder))
      {
        return false;
      }
      string text;
      string text2;
      try
      {
        text = BrowserUploads.Resolve(jobFolder);
        text2 = BrowserUploads.Resolve(path);
      }
      catch
      {
        return false;
      }
      string text3 = text2;
      ReadOnlySpan<char> readOnlySpan = text;
      char directorySeparatorChar = Path.DirectorySeparatorChar;
      return text3.StartsWith(readOnlySpan + new ReadOnlySpan<char>(ref directorySeparatorChar), StringComparison.OrdinalIgnoreCase);
    }

    // Token: 0x06001BBF RID: 7103 RVA: 0x003DD6E0 File Offset: 0x003DD6E0
    public static bool CarriesUpload(JsonObject arguments, string operation)
    {
      if (!(operation == "upload"))
      {
        if (operation == "batch")
        {
          JsonArray jsonArray = arguments["steps"] as JsonArray;
          if (jsonArray != null)
          {
            return jsonArray.Any<JsonNode>(delegate(JsonNode s)
            {
              string text;
              if (s == null)
              {
                text = null;
              }
              else
              {
                JsonNode jsonNode = s["operation"];
                text = ((jsonNode != null) ? jsonNode.ToString() : null);
              }
              return text == "upload";
            });
          }
        }
        return false;
      }
      return true;
    }

    // Token: 0x06001BC0 RID: 7104 RVA: 0x003DD744 File Offset: 0x003DD744
    public static string ApprovalQuestion(IReadOnlyList<string> fileNames, [Nullable(2)] string site)
    {
      string text = site ?? "a website";
      if (fileNames.Count == 0)
      {
        return "Add files from your PC to " + text + "?";
      }
      List<string> list = new List<string>();
      int num = 0;
      foreach (string text2 in fileNames)
      {
        string text3 = ((text2.Length > 40) ? (text2.Substring(0, 39) + "…") : text2);
        if (list.Count > 0 && num + text3.Length > 64)
        {
          break;
        }
        list.Add(text3);
        num += text3.Length + 2;
      }
      int num2 = fileNames.Count - list.Count;
      string text4;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      if (num2 <= 0)
      {
        if (list.Count <= 1)
        {
          text4 = list[0];
        }
        else
        {
          string text5 = string.Join(", ", list.SkipLast<string>(1));
          string text6 = " and ";
          List<string> list2 = list;
          text4 = text5 + text6 + list2[list2.Count - 1];
        }
      }
      else
      {
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 2);
        defaultInterpolatedStringHandler.AppendFormatted(string.Join(", ", list));
        defaultInterpolatedStringHandler.AppendLiteral(" and ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
        defaultInterpolatedStringHandler.AppendLiteral(" more");
        text4 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      string text7 = text4;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 2);
      defaultInterpolatedStringHandler.AppendLiteral("Add ");
      defaultInterpolatedStringHandler.AppendFormatted(text7);
      defaultInterpolatedStringHandler.AppendLiteral(" to ");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("?");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06001BC1 RID: 7105 RVA: 0x003DD8E4 File Offset: 0x003DD8E4
    public static string ApprovalNote(string jobTitle)
    {
      string text = jobTitle.Trim();
      return ((text.Length == 0) ? "Your job" : ((text.Length > 40) ? (text.Substring(0, 39) + "…") : text)) + " is asking to add files from your PC. A yes covers this turn.";
    }

    // Token: 0x06001BC2 RID: 7106 RVA: 0x003DD934 File Offset: 0x003DD934
    [return: Nullable(2)]
    public static string Refusal(UploadApprovalAnswer answer, string files, string place)
    {
      string text;
      switch (answer)
      {
      case UploadApprovalAnswer.Allowed:
        text = null;
        break;
      case UploadApprovalAnswer.Declined:
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(159, 2);
        defaultInterpolatedStringHandler.AppendLiteral("The user said no to adding ");
        defaultInterpolatedStringHandler.AppendFormatted(files);
        defaultInterpolatedStringHandler.AppendLiteral(" to ");
        defaultInterpolatedStringHandler.AppendFormatted(place);
        defaultInterpolatedStringHandler.AppendLiteral(" this turn. Don't try again or copy the file somewhere else to get around this. Carry on without it and say what's still needed.");
        text = defaultInterpolatedStringHandler.ToStringAndClear();
        break;
      }
      case UploadApprovalAnswer.Unanswered:
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(190, 2);
        defaultInterpolatedStringHandler.AppendLiteral("HeyClicky asked the user about adding ");
        defaultInterpolatedStringHandler.AppendFormatted(files);
        defaultInterpolatedStringHandler.AppendLiteral(" to ");
        defaultInterpolatedStringHandler.AppendFormatted(place);
        defaultInterpolatedStringHandler.AppendLiteral(" and they haven't answered. Leave the page as it is and end your turn saying you need their yes; HeyClicky's card comes back when you try next turn.");
        text = defaultInterpolatedStringHandler.ToStringAndClear();
        break;
      }
      default:
        text = "HeyClicky is asking the user something else right now. Try this again in a moment.";
        break;
      }
      return text;
    }

    // Token: 0x06001BC3 RID: 7107 RVA: 0x003DD9FC File Offset: 0x003DD9FC
    public static string DisplayName(string file)
    {
      string text = ((Path.IsPathRooted(file) || file.StartsWith('~')) ? Path.GetFileName(file.Replace('\\', '/').Replace('/', Path.DirectorySeparatorChar)) : file);
      return string.Join(' ', text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
    }

    // Token: 0x06001BC4 RID: 7108 RVA: 0x003DDA5C File Offset: 0x003DDA5C
    public static string MimeType(string name)
    {
      string text = Path.GetExtension(name).ToLowerInvariant();
      if (text != null)
      {
        int length = text.Length;
        if (length != 4)
        {
          if (length != 5)
          {
            goto IL_03F6;
          }
          char c = text[3];
          if (c <= 'm')
          {
            switch (c)
            {
            case 'b':
              if (!(text == ".webp"))
              {
                goto IL_03F6;
              }
              return "image/webp";
            case 'c':
              if (!(text == ".docx"))
              {
                goto IL_03F6;
              }
              return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            case 'd':
            case 'g':
            case 'h':
              goto IL_03F6;
            case 'e':
              if (!(text == ".jpeg"))
              {
                goto IL_03F6;
              }
              break;
            case 'f':
              if (!(text == ".tiff"))
              {
                goto IL_03F6;
              }
              goto IL_0360;
            case 'i':
              if (!(text == ".heic"))
              {
                goto IL_03F6;
              }
              return "image/heic";
            default:
              if (c != 'm')
              {
                goto IL_03F6;
              }
              if (!(text == ".html"))
              {
                goto IL_03F6;
              }
              goto IL_038E;
            }
          }
          else if (c != 'o')
          {
            if (c != 's')
            {
              if (c != 't')
              {
                goto IL_03F6;
              }
              if (!(text == ".pptx"))
              {
                goto IL_03F6;
              }
              return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
            }
            else
            {
              if (!(text == ".xlsx"))
              {
                goto IL_03F6;
              }
              return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
          }
          else
          {
            if (!(text == ".json"))
            {
              goto IL_03F6;
            }
            return "application/json";
          }
        }
        else
        {
          switch (text[1])
          {
          case 'b':
            if (!(text == ".bmp"))
            {
              goto IL_03F6;
            }
            return "image/bmp";
          case 'c':
            if (!(text == ".csv"))
            {
              goto IL_03F6;
            }
            return "text/csv";
          case 'd':
            if (!(text == ".doc"))
            {
              goto IL_03F6;
            }
            return "application/msword";
          case 'e':
          case 'f':
          case 'i':
          case 'k':
          case 'l':
          case 'n':
          case 'o':
          case 'q':
          case 'r':
          case 'u':
          case 'v':
          case 'y':
            goto IL_03F6;
          case 'g':
            if (!(text == ".gif"))
            {
              goto IL_03F6;
            }
            return "image/gif";
          case 'h':
            if (!(text == ".htm"))
            {
              goto IL_03F6;
            }
            goto IL_038E;
          case 'j':
            if (!(text == ".jpg"))
            {
              goto IL_03F6;
            }
            break;
          case 'm':
            if (text == ".mp4")
            {
              return "video/mp4";
            }
            if (text == ".mov")
            {
              return "video/quicktime";
            }
            if (!(text == ".mp3"))
            {
              goto IL_03F6;
            }
            return "audio/mpeg";
          case 'p':
            if (text == ".png")
            {
              return "image/png";
            }
            if (text == ".pdf")
            {
              return "application/pdf";
            }
            if (!(text == ".ppt"))
            {
              goto IL_03F6;
            }
            return "application/vnd.ms-powerpoint";
          case 's':
            if (!(text == ".svg"))
            {
              goto IL_03F6;
            }
            return "image/svg+xml";
          case 't':
            if (text == ".tif")
            {
              goto IL_0360;
            }
            if (!(text == ".txt"))
            {
              goto IL_03F6;
            }
            return "text/plain";
          case 'w':
            if (!(text == ".wav"))
            {
              goto IL_03F6;
            }
            return "audio/wav";
          case 'x':
            if (!(text == ".xls"))
            {
              goto IL_03F6;
            }
            return "application/vnd.ms-excel";
          case 'z':
            if (!(text == ".zip"))
            {
              goto IL_03F6;
            }
            return "application/zip";
          default:
            goto IL_03F6;
          }
        }
        return "image/jpeg";
        IL_0360:
        return "image/tiff";
        IL_038E:
        return "text/html";
      }
      IL_03F6:
      return "";
    }

    // Token: 0x06001BC5 RID: 7109 RVA: 0x003DDE68 File Offset: 0x003DDE68
    [return: Nullable(new byte[] { 1, 2 })]
    public static Task<byte[]> ReadChunkAsync(string path, int index)
    {
      return Task.Run<byte[]>(delegate
      {
        byte[] array2;
        try
        {
          using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Write | FileShare.Delete))
          {
            fileStream.Seek((long)index * 524288L, SeekOrigin.Begin);
            byte[] array = new byte[524288];
            int num = 0;
            int num2;
            while (num < array.Length && (num2 = fileStream.Read(array, num, array.Length - num)) > 0)
            {
              num += num2;
            }
            array2 = RuntimeHelpers.GetSubArray<byte>(array, Range.EndAt(num));
          }
        }
        catch
        {
          array2 = null;
        }
        return array2;
      });
    }

    // Token: 0x06001BC6 RID: 7110 RVA: 0x003DDE90 File Offset: 0x003DDE90
    public static bool IsSecretFileName(string name)
    {
      string text = name.ToLowerInvariant();
      bool flag = text.StartsWith(".env", StringComparison.Ordinal) || text.EndsWith(".pem", StringComparison.Ordinal) || text.EndsWith(".key", StringComparison.Ordinal);
      if (!flag)
      {
        bool flag2 = text == ".netrc" || text == "_netrc" || text == ".git-credentials" || text == ".npmrc" || text == ".pypirc" || text == ".pgpass";
        flag = flag2;
      }
      return flag;
    }

    // Token: 0x17000622 RID: 1570
    // (get) Token: 0x06001BC7 RID: 7111 RVA: 0x003DDF2C File Offset: 0x003DDF2C
    private static string NeedsPaths
    {
      get
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(46, 1);
        defaultInterpolatedStringHandler.AppendLiteral("upload needs paths: 1 to ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(10);
        defaultInterpolatedStringHandler.AppendLiteral(" absolute file paths.");
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
    }

    // Token: 0x06001BC8 RID: 7112 RVA: 0x003DDF6C File Offset: 0x003DDF6C
    [NullableContext(2)]
    [return: TupleElementNames(new string[] { "Paths", "Error" })]
    [return: Nullable(new byte[] { 0, 2, 1, 2 })]
    public static ValueTuple<IReadOnlyList<string>, string> ValidatedPaths(JsonNode raw, string home = null, string dataRoot = null)
    {
      JsonArray jsonArray = raw as JsonArray;
      if (jsonArray != null && jsonArray.Count != 0 && jsonArray.Count <= 10)
      {
        if (!jsonArray.Any<JsonNode>(delegate(JsonNode i)
        {
          JsonValue jsonValue = i as JsonValue;
          return jsonValue == null || jsonValue.GetValueKind() != JsonValueKind.String;
        }))
        {
          home = BrowserUploads.Normalize(home ?? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
          List<string> list = BrowserUploads.RefusedFolders.Select<string, string>((string f) => Path.Combine(home, f)).ToList<string>();
          List<string> list2 = BrowserUploads.AllowedInsideRefused.Select<string, string>((string f) => Path.Combine(home, f)).ToList<string>();
          if (dataRoot != null)
          {
            dataRoot = BrowserUploads.Normalize(dataRoot);
            list.Add(dataRoot);
            list2.Add(Path.Combine(dataRoot, "Clickys"));
          }
          List<string> list3 = new List<string>();
          foreach (JsonNode jsonNode in jsonArray)
          {
            string value = jsonNode.GetValue<string>();
            string text;
            if (!value.StartsWith("~/", StringComparison.Ordinal) && !value.StartsWith("~\\", StringComparison.Ordinal))
            {
              text = value;
            }
            else
            {
              string home2 = home;
              string text2 = "\\";
              string text3 = value;
              text = home2 + text2 + text3.Substring(2, text3.Length - 2);
            }
            string text4 = text;
            if (!Path.IsPathFullyQualified(text4))
            {
              return new ValueTuple<IReadOnlyList<string>, string>(null, BrowserUploads.NeedsPaths);
            }
            string path;
            try
            {
              path = BrowserUploads.Resolve(BrowserUploads.Normalize(text4));
            }
            catch
            {
              return new ValueTuple<IReadOnlyList<string>, string>(null, "No file at " + value + ".");
            }
            if ((list.Any<string>((string r) => BrowserUploads.<ValidatedPaths>g__IsInside|23_1(path, r)) && !list2.Any<string>((string a) => BrowserUploads.<ValidatedPaths>g__IsInside|23_1(path, a))) || BrowserUploads.IsSecretFileName(Path.GetFileName(path)))
            {
              return new ValueTuple<IReadOnlyList<string>, string>(null, value + " is in a private place (keys, credentials, browser profiles or app settings), so it is never uploaded.");
            }
            if (Directory.Exists(path))
            {
              return new ValueTuple<IReadOnlyList<string>, string>(null, value + " is not a regular file; upload takes files.");
            }
            FileInfo fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
            {
              return new ValueTuple<IReadOnlyList<string>, string>(null, "No file at " + value + ".");
            }
            if ((fileInfo.Attributes & (FileAttributes.Directory | FileAttributes.Device)) != FileAttributes.None)
            {
              return new ValueTuple<IReadOnlyList<string>, string>(null, value + " is not a regular file; upload takes files.");
            }
            try
            {
              using (File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Write | FileShare.Delete))
              {
              }
            }
            catch
            {
              return new ValueTuple<IReadOnlyList<string>, string>(null, "HeyClicky can't read " + value + ".");
            }
            if (fileInfo.Length > 104857600L)
            {
              return new ValueTuple<IReadOnlyList<string>, string>(null, fileInfo.Name + " is larger than 100 MB.");
            }
            list3.Add(path);
          }
          return new ValueTuple<IReadOnlyList<string>, string>(list3, null);
        }
      }
      return new ValueTuple<IReadOnlyList<string>, string>(null, BrowserUploads.NeedsPaths);
    }

    // Token: 0x06001BC9 RID: 7113 RVA: 0x003DE2EC File Offset: 0x003DE2EC
    private static string Normalize(string path)
    {
      return Path.GetFullPath(path).TrimEnd(new char[] { '\\', '/' });
    }

    // Token: 0x06001BCA RID: 7114 RVA: 0x003DE30C File Offset: 0x003DE30C
    private static string Resolve(string path)
    {
      return BrowserUploads.Resolve(path, 0);
    }

    // Token: 0x06001BCB RID: 7115 RVA: 0x003DE318 File Offset: 0x003DE318
    private static string Resolve(string path, int depth)
    {
      if (depth > 32)
      {
        throw new IOException("Too many links on the way to " + path);
      }
      string fullPath = Path.GetFullPath(path);
      string pathRoot = Path.GetPathRoot(fullPath);
      if (pathRoot == null)
      {
        throw new IOException("No root in " + path);
      }
      string text = pathRoot;
      string text2 = fullPath;
      int length = pathRoot.Length;
      foreach (string text3 in text2.Substring(length, text2.Length - length).Split(new char[]
      {
        Path.DirectorySeparatorChar,
        Path.AltDirectorySeparatorChar
      }, StringSplitOptions.RemoveEmptyEntries))
      {
        string text4 = Path.Join(text, text3);
        FileSystemInfo fileSystemInfo = (Directory.Exists(text4) ? new DirectoryInfo(text4) : new FileInfo(text4));
        if (fileSystemInfo.LinkTarget != null)
        {
          FileSystemInfo fileSystemInfo2 = fileSystemInfo.ResolveLinkTarget(true);
          if (fileSystemInfo2 == null)
          {
            throw new IOException("Can't follow the link at " + text4);
          }
          text4 = BrowserUploads.Resolve(fileSystemInfo2.FullName, depth + 1);
        }
        text = text4;
      }
      return BrowserUploads.Normalize(text);
    }

    // Token: 0x06001BCC RID: 7116 RVA: 0x003DE410 File Offset: 0x003DE410
    [return: TupleElementNames(new string[] { "Arguments", "Error" })]
    [return: Nullable(new byte[] { 0, 1, 2 })]
    public static ValueTuple<JsonObject, string> Validating(JsonObject arguments, string operation, [Nullable(2)] string home = null, [Nullable(2)] string dataRoot = null)
    {
      if (operation == "upload")
      {
        ValueTuple<IReadOnlyList<string>, string> valueTuple = BrowserUploads.ValidatedPaths(arguments["paths"], home, dataRoot);
        IReadOnlyList<string> item = valueTuple.Item1;
        string item2 = valueTuple.Item2;
        if (item == null)
        {
          return new ValueTuple<JsonObject, string>(arguments, item2);
        }
        arguments["paths"] = new JsonArray(item.Select<string, JsonNode>((string p) => p).ToArray<JsonNode>());
      }
      if (operation == "batch")
      {
        JsonArray jsonArray = arguments["steps"] as JsonArray;
        if (jsonArray != null)
        {
          for (int i = 0; i < jsonArray.Count; i++)
          {
            JsonObject jsonObject = jsonArray[i] as JsonObject;
            if (jsonObject != null)
            {
              JsonNode jsonNode = jsonObject["operation"];
              if (!(((jsonNode != null) ? jsonNode.ToString() : null) != "upload"))
              {
                ValueTuple<IReadOnlyList<string>, string> valueTuple2 = BrowserUploads.ValidatedPaths(jsonObject["paths"], home, dataRoot);
                IReadOnlyList<string> item3 = valueTuple2.Item1;
                string item4 = valueTuple2.Item2;
                if (item3 == null)
                {
                  DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 2);
                  defaultInterpolatedStringHandler.AppendLiteral("Step ");
                  defaultInterpolatedStringHandler.AppendFormatted<int>(i + 1);
                  defaultInterpolatedStringHandler.AppendLiteral(": ");
                  defaultInterpolatedStringHandler.AppendFormatted(item4 ?? "");
                  defaultInterpolatedStringHandler.AppendLiteral(" Nothing ran.");
                  return new ValueTuple<JsonObject, string>(arguments, defaultInterpolatedStringHandler.ToStringAndClear());
                }
                jsonObject["paths"] = new JsonArray(item3.Select<string, JsonNode>((string p) => p).ToArray<JsonNode>());
              }
            }
          }
        }
      }
      return new ValueTuple<JsonObject, string>(arguments, null);
    }

    // Token: 0x06001BCE RID: 7118 RVA: 0x003DE6E4 File Offset: 0x003DE6E4
    [CompilerGenerated]
    internal static bool <ValidatedPaths>g__IsInside|23_1(string path, string root)
    {
      return path.Equals(root, StringComparison.OrdinalIgnoreCase) || path.StartsWith(root + "\\", StringComparison.OrdinalIgnoreCase);
    }

    // Token: 0x04000BDD RID: 3037
    public static readonly string[] RefusedFolders = new string[]
    {
      ".ssh", ".gnupg", ".aws", ".azure", ".kube", ".docker", ".config\\gcloud", ".config\\gh", ".codex", "AppData\\Roaming\\Microsoft\\Credentials",
      "AppData\\Local\\Microsoft\\Credentials", "AppData\\Local\\Microsoft\\Vault", "AppData\\Roaming\\Microsoft\\Vault", "AppData\\Roaming\\Microsoft\\Protect", "AppData\\Roaming\\Microsoft\\Crypto", "AppData\\Local\\Microsoft\\Windows\\INetCookies", "AppData\\Local\\Google", "AppData\\Local\\Chromium", "AppData\\Local\\BraveSoftware", "AppData\\Local\\Microsoft\\Edge",
      "AppData\\Local\\Vivaldi", "AppData\\Local\\Packages\\TheBrowserCompany.Arc", "AppData\\Roaming\\Mozilla", "AppData\\Local\\Mozilla", "AppData\\Local\\HeyClicky", "AppData\\Roaming\\HeyClicky"
    };

    // Token: 0x04000BDE RID: 3038
    public static readonly string[] AllowedInsideRefused = new string[] { "AppData\\Local\\HeyClicky\\Clickys", "AppData\\Local\\HeyClicky\\projects" };

    // Token: 0x04000BDF RID: 3039
    public const int MaxFiles = 10;

    // Token: 0x04000BE0 RID: 3040
    public const long MaxBytes = 104857600L;

    // Token: 0x04000BE1 RID: 3041
    public const int ChunkBytes = 524288;

    // Token: 0x04000BE2 RID: 3042
    public const string NeedsNewerExtensionHint = "This upload needs a newer version of the HeyClicky Chrome extension, which Chrome installs by itself (at the latest when Chrome restarts). For now, ask the user to add the file to the page themselves: name the file and the field.";

    // Token: 0x04000BE3 RID: 3043
    public const string TooLargeForOlderExtensionHint = "This Chrome still has an older HeyClicky extension, which can only upload files up to 2 MB. Chrome installs the new one by itself (at the latest when Chrome restarts). For now, ask the user to add the file to the page themselves: name the file and the field.";

    // Token: 0x04000BE4 RID: 3044
    public const long OlderExtensionMaxBytes = 2097152L;

    // Token: 0x04000BE5 RID: 3045
    public const int OlderExtensionChunkBytes = 14700;

    // Token: 0x04000BE6 RID: 3046
    public const string OlderExtensionArmScript = "globalThis.__heyClickyUploadDisarm?.();\nconst meta = __FILES__;\nconst parts = globalThis.__heyClickyUpload || [];\ndelete globalThis.__heyClickyUpload;\nconst files = meta.map((fileMeta, fileIndex) => new File((parts[fileIndex] || []).map(piece => {\n  const binary = atob(piece), bytes = new Uint8Array(binary.length);\n  for (let byteIndex = 0; byteIndex < binary.length; byteIndex++) bytes[byteIndex] = binary.charCodeAt(byteIndex);\n  return bytes;\n}), fileMeta.name, {type: fileMeta.type, lastModified: fileMeta.lastModified}));\nif (files.some((file, fileIndex) => file.size !== meta[fileIndex].size)) return {error: 'The page reloaded while the file was being sent.'};\nconst state = {given: null};\nconst click = HTMLInputElement.prototype.click, picker = HTMLInputElement.prototype.showPicker, openPicker = window.showOpenFilePicker;\nlet disarmed = false, disarmTimer = null;\nconst disarm = () => {\n  if (disarmed) return;\n  disarmed = true;\n  clearTimeout(disarmTimer);\n  window.removeEventListener('click', onClick, true);\n  HTMLInputElement.prototype.click = click;\n  if (picker) HTMLInputElement.prototype.showPicker = picker;\n  if (openPicker) window.showOpenFilePicker = openPicker;\n  if (globalThis.__heyClickyUploadDisarm === disarm) delete globalThis.__heyClickyUploadDisarm;\n};\nconst give = input => {\n  if (state.given) return;\n  if (!input.multiple && files.length > 1) state.given = {error: 'This file field takes one file.'};\n  else {\n    const chosen = new DataTransfer();\n    files.forEach(file => chosen.items.add(file));\n    input.files = chosen.files;\n    input.dispatchEvent(new Event('input', {bubbles: true, composed: true}));\n    input.dispatchEvent(new Event('change', {bubbles: true}));\n    state.given = {sizes: [...input.files].map(file => file.size)};\n  }\n  disarm();\n};\nconst isFileField = node => node instanceof HTMLInputElement && node.type === 'file';\nfunction onClick(event) {\n  const target = event.composedPath()[0];\n  if (isFileField(target)) { event.preventDefault(); give(target); }\n}\nwindow.addEventListener('click', onClick, true);\nHTMLInputElement.prototype.click = function () { return isFileField(this) ? give(this) : click.call(this); };\nif (picker) HTMLInputElement.prototype.showPicker = function () { return isFileField(this) ? give(this) : picker.call(this); };\nif (openPicker) window.showOpenFilePicker = async () => {\n  state.given = {error: 'This page picks files a way HeyClicky can\\'t answer yet.'};\n  disarm();\n  throw new DOMException('The user aborted a request.', 'AbortError');\n};\nglobalThis.__heyClickyUploadDisarm = disarm;\nglobalThis.__heyClickyUploadState = state;\ndisarmTimer = setTimeout(disarm, 8000);\nreturn {armed: true};";

    // Token: 0x04000BE7 RID: 3047
    public const string OlderExtensionResultScript = "const given = globalThis.__heyClickyUploadState?.given ?? null;\nif (given || __LAST__) { globalThis.__heyClickyUploadDisarm?.(); delete globalThis.__heyClickyUploadState; }\nreturn given;";
  }
}
