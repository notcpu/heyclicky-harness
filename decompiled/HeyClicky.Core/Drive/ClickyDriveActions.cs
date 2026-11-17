using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Drive
{
  // Token: 0x02000193 RID: 403
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyDriveActions
  {
    // Token: 0x060017DD RID: 6109 RVA: 0x003C5FE0 File Offset: 0x003C5FE0
    public static string TypeOf(JsonObject action)
    {
      return (((string)action["type"]) ?? "").ToLowerInvariant();
    }

    // Token: 0x060017DE RID: 6110 RVA: 0x003C6000 File Offset: 0x003C6000
    public static bool IsObservationOnly(IEnumerable<JsonObject> actions)
    {
      return actions.All<JsonObject>((JsonObject a) => ClickyDriveActions.ObservationActionTypes.Contains(ClickyDriveActions.TypeOf(a)));
    }

    // Token: 0x060017DF RID: 6111 RVA: 0x003C6028 File Offset: 0x003C6028
    public static string NormalizedKeyName(string key)
    {
      string text = key.ToLowerInvariant();
      if (text != null)
      {
        switch (text.Length)
        {
        case 1:
          if (!(text == " "))
          {
            goto IL_049D;
          }
          goto IL_0495;
        case 2:
          if (!(text == "up"))
          {
            goto IL_049D;
          }
          goto IL_0455;
        case 3:
        {
          char c = text[0];
          switch (c)
          {
          case 'a':
            if (!(text == "alt"))
            {
              goto IL_049D;
            }
            goto IL_043D;
          case 'b':
            goto IL_049D;
          case 'c':
            if (!(text == "cmd"))
            {
              goto IL_049D;
            }
            goto IL_042D;
          case 'd':
            if (!(text == "del"))
            {
              goto IL_049D;
            }
            goto IL_047D;
          case 'e':
            if (!(text == "esc"))
            {
              goto IL_049D;
            }
            goto IL_044D;
          default:
            if (c != 'o')
            {
              if (c != 'w')
              {
                goto IL_049D;
              }
              if (!(text == "win"))
              {
                goto IL_049D;
              }
            }
            else
            {
              if (!(text == "opt"))
              {
                goto IL_049D;
              }
              goto IL_043D;
            }
            break;
          }
          break;
        }
        case 4:
        {
          char c = text[0];
          if (c <= 'd')
          {
            if (c != 'c')
            {
              if (c != 'd')
              {
                goto IL_049D;
              }
              if (!(text == "down"))
              {
                goto IL_049D;
              }
              goto IL_045D;
            }
            else
            {
              if (!(text == "ctrl"))
              {
                goto IL_049D;
              }
              goto IL_0435;
            }
          }
          else if (c != 'l')
          {
            if (c != 'm')
            {
              goto IL_049D;
            }
            if (!(text == "meta"))
            {
              goto IL_049D;
            }
          }
          else
          {
            if (!(text == "left"))
            {
              goto IL_049D;
            }
            goto IL_0465;
          }
          break;
        }
        case 5:
        {
          char c = text[1];
          if (c <= 'n')
          {
            if (c != 'i')
            {
              if (c != 'n')
              {
                goto IL_049D;
              }
              if (!(text == "enter"))
              {
                goto IL_049D;
              }
              goto IL_0445;
            }
            else
            {
              if (!(text == "right"))
              {
                goto IL_049D;
              }
              goto IL_046D;
            }
          }
          else if (c != 'p')
          {
            if (c != 'u')
            {
              goto IL_049D;
            }
            if (!(text == "super"))
            {
              goto IL_049D;
            }
          }
          else
          {
            if (!(text == "space"))
            {
              goto IL_049D;
            }
            goto IL_0495;
          }
          break;
        }
        case 6:
        {
          char c = text[0];
          if (c != 'd')
          {
            if (c != 'e')
            {
              switch (c)
              {
              case 'o':
                if (!(text == "option"))
                {
                  goto IL_049D;
                }
                goto IL_043D;
              case 'p':
                if (!(text == "pageup"))
                {
                  goto IL_049D;
                }
                return "pageup";
              case 'q':
                goto IL_049D;
              case 'r':
                if (!(text == "return"))
                {
                  goto IL_049D;
                }
                goto IL_0445;
              default:
                goto IL_049D;
              }
            }
            else
            {
              if (!(text == "escape"))
              {
                goto IL_049D;
              }
              goto IL_044D;
            }
          }
          else
          {
            if (!(text == "delete"))
            {
              goto IL_049D;
            }
            goto IL_047D;
          }
          break;
        }
        case 7:
        {
          char c = text[3];
          if (c <= 'm')
          {
            if (c != 'd')
            {
              if (c != 'm')
              {
                goto IL_049D;
              }
              if (!(text == "command"))
              {
                goto IL_049D;
              }
              goto IL_042D;
            }
            else if (!(text == "windows"))
            {
              goto IL_049D;
            }
          }
          else if (c != 'o')
          {
            if (c != 't')
            {
              goto IL_049D;
            }
            if (!(text == "control"))
            {
              goto IL_049D;
            }
            goto IL_0435;
          }
          else
          {
            if (!(text == "arrowup"))
            {
              goto IL_049D;
            }
            goto IL_0455;
          }
          break;
        }
        case 8:
        {
          char c = text[0];
          if (c != 'p')
          {
            if (c != 's')
            {
              goto IL_049D;
            }
            if (!(text == "spacebar"))
            {
              goto IL_049D;
            }
            goto IL_0495;
          }
          else
          {
            if (!(text == "pagedown"))
            {
              goto IL_049D;
            }
            return "pagedown";
          }
          break;
        }
        case 9:
        {
          char c = text[5];
          if (c != 'd')
          {
            if (c != 'l')
            {
              if (c != 'p')
              {
                goto IL_049D;
              }
              if (!(text == "backspace"))
              {
                goto IL_049D;
              }
              return "delete";
            }
            else
            {
              if (!(text == "arrowleft"))
              {
                goto IL_049D;
              }
              goto IL_0465;
            }
          }
          else
          {
            if (!(text == "arrowdown"))
            {
              goto IL_049D;
            }
            goto IL_045D;
          }
          break;
        }
        case 10:
          if (!(text == "arrowright"))
          {
            goto IL_049D;
          }
          goto IL_046D;
        default:
          goto IL_049D;
        }
        return "win";
        IL_042D:
        return "cmd";
        IL_0435:
        return "ctrl";
        IL_043D:
        return "opt";
        IL_0445:
        return "return";
        IL_044D:
        return "escape";
        IL_0455:
        return "up";
        IL_045D:
        return "down";
        IL_0465:
        return "left";
        IL_046D:
        return "right";
        IL_047D:
        return "forwarddelete";
        IL_0495:
        return "space";
      }
      IL_049D:
      return text;
    }

    // Token: 0x060017E0 RID: 6112 RVA: 0x003C64D8 File Offset: 0x003C64D8
    public static List<string> Keys(JsonObject action)
    {
      JsonArray jsonArray = action["keys"] as JsonArray;
      List<string> list;
      if (jsonArray == null)
      {
        list = null;
      }
      else
      {
        list = (from k in jsonArray.Select<JsonNode, string>(delegate(JsonNode k)
          {
            if (k == null || k.GetValueKind() != JsonValueKind.String)
            {
              return null;
            }
            return (string)k;
          })
          where k != null
          select ClickyDriveActions.NormalizedKeyName(k)).ToList<string>();
      }
      return list ?? new List<string>();
    }

    // Token: 0x060017E1 RID: 6113 RVA: 0x003C6578 File Offset: 0x003C6578
    public static double? Number(JsonObject action, string key)
    {
      JsonValue jsonValue = action[key] as JsonValue;
      double num;
      if (jsonValue == null || !jsonValue.TryGetValue<double>(out num))
      {
        return null;
      }
      return new double?(num);
    }

    // Token: 0x060017E2 RID: 6114 RVA: 0x003C65B0 File Offset: 0x003C65B0
    public static int ScrollLines(double scrollY)
    {
      if (scrollY == 0.0)
      {
        return 0;
      }
      if (scrollY <= 0.0)
      {
        return Math.Min(-1, (int)(scrollY / 50.0));
      }
      return Math.Max(1, (int)(scrollY / 50.0));
    }

    // Token: 0x060017E3 RID: 6115 RVA: 0x003C65FC File Offset: 0x003C65FC
    public static string Describe(JsonObject action)
    {
      IOrderedEnumerable<string> orderedEnumerable = action.Where<KeyValuePair<string, JsonNode>>((KeyValuePair<string, JsonNode> field) => field.Key != "type" && field.Value != null).Select<KeyValuePair<string, JsonNode>, string>(delegate(KeyValuePair<string, JsonNode> field)
      {
        JsonValue jsonValue = field.Value as JsonValue;
        string text2;
        string text = ((jsonValue != null && jsonValue.TryGetValue<string>(out text2)) ? text2 : field.Value.ToJsonString(null));
        text = text.Replace("\n", " ");
        return field.Key + "=" + ((text.Length > 60) ? text.Substring(0, 60) : text);
      }).OrderBy<string, string>((string f) => f, StringComparer.Ordinal);
      return (((string)action["type"]) ?? "?") + " " + string.Join(" ", orderedEnumerable);
    }

    // Token: 0x060017E4 RID: 6116 RVA: 0x003C66AC File Offset: 0x003C66AC
    public static string ClickLogLine(string type, JsonObject action)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 3);
      defaultInterpolatedStringHandler.AppendFormatted(type.Replace("_", " "));
      defaultInterpolatedStringHandler.AppendLiteral(" at ");
      defaultInterpolatedStringHandler.AppendFormatted<int>((int)ClickyDriveActions.Number(action, "x").GetValueOrDefault());
      defaultInterpolatedStringHandler.AppendLiteral(",");
      defaultInterpolatedStringHandler.AppendFormatted<int>((int)ClickyDriveActions.Number(action, "y").GetValueOrDefault());
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x060017E5 RID: 6117 RVA: 0x003C6730 File Offset: 0x003C6730
    public static string TypedLogLine(string text)
    {
      return "typed \"" + ((text.Length > 40) ? text.Substring(0, 40) : text) + "\"";
    }

    // Token: 0x060017E6 RID: 6118 RVA: 0x003C6758 File Offset: 0x003C6758
    public static PointD ScreenPoint(double canvasX, double canvasY, ScreenCaptureFrame frame)
    {
      RectD boundsPx = frame.Display.BoundsPx;
      double num = boundsPx.Width / (double)Math.Max(1, frame.PixelWidth);
      double num2 = boundsPx.Height / (double)Math.Max(1, frame.PixelHeight);
      return new PointD(boundsPx.X + canvasX * num, boundsPx.Y + canvasY * num2);
    }

    // Token: 0x060017E7 RID: 6119 RVA: 0x003C67B8 File Offset: 0x003C67B8
    public static PointD CanvasPoint(PointD screen, ScreenCaptureFrame frame)
    {
      RectD boundsPx = frame.Display.BoundsPx;
      double num = (double)frame.PixelWidth / Math.Max(1.0, boundsPx.Width);
      double num2 = (double)frame.PixelHeight / Math.Max(1.0, boundsPx.Height);
      return new PointD((screen.X - boundsPx.X) * num, (screen.Y - boundsPx.Y) * num2);
    }

    // Token: 0x060017E8 RID: 6120 RVA: 0x003C6834 File Offset: 0x003C6834
    public static string CursorPositionText(PointD pointer, ScreenCaptureFrame frame)
    {
      PointD pointD = ClickyDriveActions.CanvasPoint(pointer, frame);
      bool flag = pointD.X >= 0.0 && pointD.Y >= 0.0 && pointD.X <= (double)frame.PixelWidth && pointD.Y <= (double)frame.PixelHeight;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 2);
      defaultInterpolatedStringHandler.AppendLiteral("X=");
      defaultInterpolatedStringHandler.AppendFormatted<int>((int)Math.Round(pointD.X, MidpointRounding.AwayFromZero));
      defaultInterpolatedStringHandler.AppendLiteral(", Y=");
      defaultInterpolatedStringHandler.AppendFormatted<int>((int)Math.Round(pointD.Y, MidpointRounding.AwayFromZero));
      return defaultInterpolatedStringHandler.ToStringAndClear() + (flag ? "" : " (off this screen)");
    }

    // Token: 0x060017E9 RID: 6121 RVA: 0x003C68FC File Offset: 0x003C68FC
    public static RectD? ZoomRegionPx(JsonObject action, ScreenCaptureFrame frame)
    {
      JsonArray jsonArray = action["region"] as JsonArray;
      List<double> list;
      if (jsonArray == null)
      {
        list = null;
      }
      else
      {
        list = jsonArray.Select<JsonNode, double>(delegate(JsonNode n)
        {
          JsonValue jsonValue = n as JsonValue;
          double num3;
          if (jsonValue == null || !jsonValue.TryGetValue<double>(out num3))
          {
            return double.NaN;
          }
          return num3;
        }).ToList<double>();
      }
      List<double> list2 = list;
      if (list2 != null && list2.Count == 4)
      {
        IEnumerable<double> enumerable = list2;
        Func<double, bool> func;
        if ((func = ClickyDriveActions.<>O.<0>__IsNaN) == null)
        {
          func = (ClickyDriveActions.<>O.<0>__IsNaN = new Func<double, bool>(double.IsNaN));
        }
        if (!enumerable.Any<double>(func))
        {
          double num = frame.Display.BoundsPx.Width / (double)Math.Max(1, frame.PixelWidth);
          double num2 = frame.Display.BoundsPx.Height / (double)Math.Max(1, frame.PixelHeight);
          return new RectD?(new RectD(Math.Min(list2[0], list2[2]) * num, Math.Min(list2[1], list2[3]) * num2, Math.Abs(list2[2] - list2[0]) * num, Math.Abs(list2[3] - list2[1]) * num2));
        }
      }
      return null;
    }

    // Token: 0x04000A49 RID: 2633
    public static readonly IReadOnlySet<string> ObservationActionTypes = new HashSet<string> { "screenshot", "zoom", "cursor_position" };

    // Token: 0x02000579 RID: 1401
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x0400208E RID: 8334
      [Nullable(0)]
      public static Func<double, bool> <0>__IsNaN;
    }
  }
}
