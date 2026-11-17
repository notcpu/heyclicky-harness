using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace HeyClicky.Agents.Backend
{
  // Token: 0x02000076 RID: 118
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class UserMemoryCache
  {
    // Token: 0x0600092B RID: 2347 RVA: 0x00103D0C File Offset: 0x00103D0C
    public UserMemoryCache(string stateDirectory)
    {
    }

    // Token: 0x0600092C RID: 2348 RVA: 0x00103D30 File Offset: 0x00103D30
    [NullableContext(2)]
    public string Resolve(string userId, UserMemoryFetchStatus status, string markdown)
    {
      if (status != UserMemoryFetchStatus.Profile)
      {
        if (status != UserMemoryFetchStatus.NoProfile)
        {
          string text = this.Read(userId);
          string text2 = "Agents";
          string text3;
          if (text != null)
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(57, 1);
            defaultInterpolatedStringHandler.AppendLiteral("user memory: fetch failed, using the cached copy (");
            defaultInterpolatedStringHandler.AppendFormatted<int>(text.Length);
            defaultInterpolatedStringHandler.AppendLiteral(" chars)");
            text3 = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          else
          {
            text3 = "user memory: fetch failed, no cached copy";
          }
          Log.Info(text2, text3);
          return text;
        }
      }
      else if (!string.IsNullOrWhiteSpace(markdown))
      {
        this.Store(userId, markdown);
        string text4 = "Agents";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 1);
        defaultInterpolatedStringHandler.AppendLiteral("user memory: fetched fresh (");
        defaultInterpolatedStringHandler.AppendFormatted<int>(markdown.Length);
        defaultInterpolatedStringHandler.AppendLiteral(" chars), cached");
        Log.Info(text4, defaultInterpolatedStringHandler.ToStringAndClear());
        return markdown;
      }
      this.Clear(userId);
      Log.Info("Agents", "user memory: the Worker reports no profile; injecting nothing");
      return null;
    }

    // Token: 0x0600092D RID: 2349 RVA: 0x00103E08 File Offset: 0x00103E08
    [NullableContext(2)]
    public string Read(string userId)
    {
      string text = this.PathFor(userId);
      if (text == null)
      {
        return null;
      }
      string text4;
      using (this.gate.EnterScope())
      {
        try
        {
          string text3;
          if (File.Exists(text))
          {
            string text2 = File.ReadAllText(text);
            if (text2 != null && !string.IsNullOrWhiteSpace(text2))
            {
              text3 = text2;
              goto IL_0037;
            }
          }
          text3 = null;
          IL_0037:
          text4 = text3;
        }
        catch (IOException)
        {
          text4 = null;
        }
      }
      return text4;
    }

    // Token: 0x0600092E RID: 2350 RVA: 0x00103E7C File Offset: 0x00103E7C
    private void Store([Nullable(2)] string userId, string markdown)
    {
      string text = this.PathFor(userId);
      if (text == null)
      {
        return;
      }
      using (this.gate.EnterScope())
      {
        try
        {
          Directory.CreateDirectory(this.directory);
          File.WriteAllText(text + ".tmp", markdown);
          File.Move(text + ".tmp", text, true);
        }
        catch (IOException ex)
        {
          Log.Warn("Agents", "user memory cache write failed: " + ex.Message);
        }
      }
    }

    // Token: 0x0600092F RID: 2351 RVA: 0x00103F14 File Offset: 0x00103F14
    [NullableContext(2)]
    public void Clear(string userId)
    {
      string text = this.PathFor(userId);
      if (text == null)
      {
        return;
      }
      using (this.gate.EnterScope())
      {
        try
        {
          File.Delete(text);
        }
        catch (IOException)
        {
        }
      }
    }

    // Token: 0x06000930 RID: 2352 RVA: 0x00103F68 File Offset: 0x00103F68
    [NullableContext(2)]
    private string PathFor(string userId)
    {
      if (string.IsNullOrWhiteSpace(userId))
      {
        return null;
      }
      string text = new string(userId.Where<char>(delegate(char c)
      {
        bool flag = char.IsAsciiLetterOrDigit(c);
        if (!flag)
        {
          bool flag2 = c == '-' || c == '_';
          flag = flag2;
        }
        return flag;
      }).ToArray<char>());
      if (text.Length != 0)
      {
        return Path.Combine(this.directory, text + ".md");
      }
      return null;
    }

    // Token: 0x0400033C RID: 828
    private readonly string directory = Path.Combine(stateDirectory, "user-memory");

    // Token: 0x0400033D RID: 829
    private readonly Lock gate = new Lock();
  }
}
