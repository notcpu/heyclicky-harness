using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Notch
{
  // Token: 0x02000105 RID: 261
  [NullableContext(1)]
  [Nullable(0)]
  public static class ChatActionCaption
  {
    // Token: 0x06001129 RID: 4393 RVA: 0x00399B68 File Offset: 0x00399B68
    public static List<ChatActionCaption.Item> Items(string caption)
    {
      List<string> list = new List<string>();
      string text = null;
      string[] array = caption.Split("; ", StringSplitOptions.None);
      for (int j = 0; j < array.Length; j++)
      {
        string text2 = array[j].Trim();
        if (text2.StartsWith("called ", StringComparison.Ordinal))
        {
          List<string> list2 = list;
          string text3 = text2;
          int num = "called ".Length;
          list2.AddRange(from t in text3.Substring(num, text3.Length - num).Split(", ", StringSplitOptions.None)
            select t.Trim() into t
            where t.Length > 0
            select t);
        }
        else if (text2.StartsWith("sources: ", StringComparison.Ordinal))
        {
          string text3 = text2;
          int num = "sources: ".Length;
          text = text3.Substring(num, text3.Length - num);
        }
      }
      List<ChatActionCaption.Item> list3 = new List<ChatActionCaption.Item>();
      bool flag = false;
      foreach (string text4 in list)
      {
        ChatActionCaption.<>c__DisplayClass1_0 CS$<>8__locals1 = new ChatActionCaption.<>c__DisplayClass1_0();
        CS$<>8__locals1.item = ChatActionCaption.ItemForTool(text4);
        if (text4 == "web_search" && text != null)
        {
          ChatActionCaption.<>c__DisplayClass1_0 CS$<>8__locals2 = CS$<>8__locals1;
          ChatActionCaption.Item item = CS$<>8__locals1.item.<Clone>$();
          item.Text = CS$<>8__locals1.item.Text + " (sources: " + text + ")";
          CS$<>8__locals2.item = item;
          flag = true;
        }
        if (!list3.Any<ChatActionCaption.Item>((ChatActionCaption.Item i) => i.Text == CS$<>8__locals1.item.Text))
        {
          list3.Add(CS$<>8__locals1.item);
        }
      }
      if (text != null && !flag)
      {
        list3.Add(new ChatActionCaption.Item("link", "Sources: " + text));
      }
      return list3;
    }

    // Token: 0x0600112A RID: 4394 RVA: 0x00399D5C File Offset: 0x00399D5C
    private static ChatActionCaption.Item ItemForTool(string tool)
    {
      if (tool != null)
      {
        switch (tool.Length)
        {
        case 6:
          if (!(tool == "recall"))
          {
            goto IL_06A2;
          }
          return new ChatActionCaption.Item("clock.arrow.circlepath", "Looked back through earlier chats");
        case 7:
        case 19:
        case 21:
        case 22:
        case 24:
        case 26:
          goto IL_06A2;
        case 8:
        {
          char c = tool[0];
          if (c != 'o')
          {
            if (c != 'r')
            {
              goto IL_06A2;
            }
            if (!(tool == "routines"))
            {
              goto IL_06A2;
            }
            return new ChatActionCaption.Item("repeat", "Updated your routines");
          }
          else
          {
            if (!(tool == "open_url"))
            {
              goto IL_06A2;
            }
            return new ChatActionCaption.Item("safari", "Opened a page");
          }
          break;
        }
        case 9:
        {
          char c = tool[5];
          if (c != 'l')
          {
            if (c != 'r')
            {
              goto IL_06A2;
            }
            if (!(tool == "file_read"))
            {
              goto IL_06A2;
            }
            return new ChatActionCaption.Item("doc.text.magnifyingglass", "Read a file");
          }
          else
          {
            if (!(tool == "file_list"))
            {
              goto IL_06A2;
            }
            return new ChatActionCaption.Item("folder", "Looked through your files");
          }
          break;
        }
        case 10:
          switch (tool[0])
          {
          case 's':
            if (!(tool == "shell_exec"))
            {
              goto IL_06A2;
            }
            goto IL_05D9;
          case 't':
          case 'u':
            goto IL_06A2;
          case 'v':
            if (!(tool == "view_image"))
            {
              goto IL_06A2;
            }
            return new ChatActionCaption.Item("photo", "Looked at an image");
          case 'w':
            if (!(tool == "web_search"))
            {
              goto IL_06A2;
            }
            return new ChatActionCaption.Item("magnifyingglass", "Searched the web");
          default:
            goto IL_06A2;
          }
          break;
        case 11:
        {
          char c = tool[0];
          if (c != 'i')
          {
            if (c != 's')
            {
              goto IL_06A2;
            }
            if (!(tool == "save_memory"))
            {
              goto IL_06A2;
            }
            return new ChatActionCaption.Item("bookmark.fill", "Saved to memory");
          }
          else
          {
            if (!(tool == "insert_text"))
            {
              goto IL_06A2;
            }
            return new ChatActionCaption.Item("keyboard", "Typed it in");
          }
          break;
        }
        case 12:
        {
          char c = tool[0];
          if (c != 'e')
          {
            if (c != 's')
            {
              goto IL_06A2;
            }
            if (!(tool == "save_drawing"))
            {
              goto IL_06A2;
            }
            return new ChatActionCaption.Item("square.and.arrow.down", "Saved the drawing");
          }
          else
          {
            if (!(tool == "exec_command"))
            {
              goto IL_06A2;
            }
            goto IL_05D9;
          }
          break;
        }
        case 13:
        {
          char c = tool[0];
          if (c <= 'f')
          {
            if (c != 'c')
            {
              if (c != 'f')
              {
                goto IL_06A2;
              }
              if (!(tool == "forget_memory"))
              {
                goto IL_06A2;
              }
              return new ChatActionCaption.Item("eraser", "Forgot it");
            }
            else if (!(tool == "clicky_memory"))
            {
              goto IL_06A2;
            }
          }
          else if (c != 'o')
          {
            if (c != 's')
            {
              goto IL_06A2;
            }
            if (!(tool == "sessions_send"))
            {
              goto IL_06A2;
            }
            return new ChatActionCaption.Item("arrowshape.turn.up.right.fill", "Sent it to a background job");
          }
          else
          {
            if (!(tool == "open_artifact"))
            {
              goto IL_06A2;
            }
            return new ChatActionCaption.Item("doc.text", "Opened a file");
          }
          break;
        }
        case 14:
        {
          char c = tool[9];
          if (c <= 's')
          {
            if (c != 'c')
            {
              if (c != 's')
              {
                goto IL_06A2;
              }
              if (!(tool == "sessions_spawn"))
              {
                goto IL_06A2;
              }
              return new ChatActionCaption.Item("paperplane.fill", "Started a background job");
            }
            else
            {
              if (!(tool == "capture_screen"))
              {
                goto IL_06A2;
              }
              return new ChatActionCaption.Item("camera.viewfinder", "Looked at your screen");
            }
          }
          else if (c != 't')
          {
            if (c != 'y')
            {
              goto IL_06A2;
            }
            if (!(tool == "sessions_yield"))
            {
              goto IL_06A2;
            }
            return new ChatActionCaption.Item("hourglass", "Waited on a background job");
          }
          else if (!(tool == "session_status"))
          {
            goto IL_06A2;
          }
          break;
        }
        case 15:
        {
          char c = tool[0];
          if (c != 'p')
          {
            if (c != 'r')
            {
              goto IL_06A2;
            }
            if (!(tool == "reveal_artifact"))
            {
              goto IL_06A2;
            }
            return new ChatActionCaption.Item("folder", "Showed a file in Explorer");
          }
          else
          {
            if (!(tool == "play_on_spotify"))
            {
              goto IL_06A2;
            }
            return new ChatActionCaption.Item("play.circle.fill", "Played it on Spotify");
          }
          break;
        }
        case 16:
          if (!(tool == "clicky_artifacts"))
          {
            goto IL_06A2;
          }
          return new ChatActionCaption.Item("doc.on.doc", "Looked at a background job's files");
        case 17:
          if (!(tool == "copy_to_clipboard"))
          {
            goto IL_06A2;
          }
          return new ChatActionCaption.Item("doc.on.clipboard", "Copied to your clipboard");
        case 18:
          if (!(tool == "sessions_interrupt"))
          {
            goto IL_06A2;
          }
          return new ChatActionCaption.Item("stop.circle", "Stopped a background job");
        case 20:
        {
          char c = tool[0];
          if (c != 'o')
          {
            if (c != 's')
            {
              goto IL_06A2;
            }
            if (!(tool == "send_to_higher_model"))
            {
              goto IL_06A2;
            }
            return new ChatActionCaption.Item("brain", "Thought it through");
          }
          else
          {
            if (!(tool == "open_agent_dashboard"))
            {
              goto IL_06A2;
            }
            return new ChatActionCaption.Item("house", "Opened Home");
          }
          break;
        }
        case 23:
          if (!(tool == "sessions_answer_request"))
          {
            goto IL_06A2;
          }
          return new ChatActionCaption.Item("checkmark.bubble", "Answered a background job");
        case 25:
          if (!(tool == "skip_onboarding_interview"))
          {
            goto IL_06A2;
          }
          return new ChatActionCaption.Item("forward", "Skipped the interview");
        case 27:
          if (!(tool == "google_calendar_find_events"))
          {
            goto IL_06A2;
          }
          return new ChatActionCaption.Item("calendar", "Checked your calendar");
        case 28:
          if (!(tool == "google_calendar_create_event"))
          {
            goto IL_06A2;
          }
          return new ChatActionCaption.Item("calendar.badge.plus", "Added it to your calendar");
        default:
          goto IL_06A2;
        }
        return new ChatActionCaption.Item("person.text.rectangle", "Checked on a background job");
        IL_05D9:
        return new ChatActionCaption.Item("terminal", "Ran a quick command");
      }
      IL_06A2:
      ChatActionCaption.Item item;
      if (tool == "mcp" || tool.StartsWith("connected_apps", StringComparison.Ordinal) || tool.StartsWith("COMPOSIO", StringComparison.Ordinal))
      {
        item = new ChatActionCaption.Item("tray.full", "Read your apps");
      }
      else
      {
        item = new ChatActionCaption.Item("wrench.and.screwdriver", "Used " + tool.Replace('_', ' '));
      }
      return item;
    }

    // Token: 0x02000440 RID: 1088
    [Nullable(0)]
    public sealed class Item : IEquatable<ChatActionCaption.Item>
    {
      // Token: 0x06002DD4 RID: 11732 RVA: 0x0042E5BC File Offset: 0x0042E5BC
      public Item(string Symbol, string Text)
      {
        this.Symbol = Symbol;
        this.Text = Text;
        base..ctor();
      }

      // Token: 0x17000861 RID: 2145
      // (get) Token: 0x06002DD5 RID: 11733 RVA: 0x0042E5D4 File Offset: 0x0042E5D4
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ChatActionCaption.Item);
        }
      }

      // Token: 0x17000862 RID: 2146
      // (get) Token: 0x06002DD6 RID: 11734 RVA: 0x0042E5E0 File Offset: 0x0042E5E0
      // (set) Token: 0x06002DD7 RID: 11735 RVA: 0x0042E5E8 File Offset: 0x0042E5E8
      public string Symbol { get; set; }

      // Token: 0x17000863 RID: 2147
      // (get) Token: 0x06002DD8 RID: 11736 RVA: 0x0042E5F4 File Offset: 0x0042E5F4
      // (set) Token: 0x06002DD9 RID: 11737 RVA: 0x0042E5FC File Offset: 0x0042E5FC
      public string Text { get; set; }

      // Token: 0x06002DDA RID: 11738 RVA: 0x0042E608 File Offset: 0x0042E608
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Item");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002DDB RID: 11739 RVA: 0x0042E654 File Offset: 0x0042E654
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Symbol = ");
        builder.Append(this.Symbol);
        builder.Append(", Text = ");
        builder.Append(this.Text);
        return true;
      }

      // Token: 0x06002DDC RID: 11740 RVA: 0x0042E690 File Offset: 0x0042E690
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ChatActionCaption.Item left, ChatActionCaption.Item right)
      {
        return !(left == right);
      }

      // Token: 0x06002DDD RID: 11741 RVA: 0x0042E69C File Offset: 0x0042E69C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ChatActionCaption.Item left, ChatActionCaption.Item right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002DDE RID: 11742 RVA: 0x0042E6B0 File Offset: 0x0042E6B0
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Symbol>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField);
      }

      // Token: 0x06002DDF RID: 11743 RVA: 0x0042E6F0 File Offset: 0x0042E6F0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ChatActionCaption.Item);
      }

      // Token: 0x06002DE0 RID: 11744 RVA: 0x0042E700 File Offset: 0x0042E700
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ChatActionCaption.Item other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Symbol>k__BackingField, other.<Symbol>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField));
      }

      // Token: 0x06002DE2 RID: 11746 RVA: 0x0042E764 File Offset: 0x0042E764
      [CompilerGenerated]
      private Item(ChatActionCaption.Item original)
      {
        this.Symbol = original.<Symbol>k__BackingField;
        this.Text = original.<Text>k__BackingField;
      }

      // Token: 0x06002DE3 RID: 11747 RVA: 0x0042E784 File Offset: 0x0042E784
      [CompilerGenerated]
      public void Deconstruct(out string Symbol, out string Text)
      {
        Symbol = this.Symbol;
        Text = this.Text;
      }
    }
  }
}
