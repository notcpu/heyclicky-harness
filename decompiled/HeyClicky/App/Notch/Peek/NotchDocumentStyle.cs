using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.App.Design;
using Windows.UI;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000181 RID: 385
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchDocumentStyle : IEquatable<NotchDocumentStyle>
  {
    // Token: 0x060014EB RID: 5355 RVA: 0x005B2E74 File Offset: 0x005B2E74
    public NotchDocumentStyle(string Glyph, Color Tint, string KindLabel)
    {
      this.Glyph = Glyph;
      this.Tint = Tint;
      this.KindLabel = KindLabel;
      base..ctor();
    }

    // Token: 0x170003BB RID: 955
    // (get) Token: 0x060014EC RID: 5356 RVA: 0x005B2E94 File Offset: 0x005B2E94
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(NotchDocumentStyle);
      }
    }

    // Token: 0x170003BC RID: 956
    // (get) Token: 0x060014ED RID: 5357 RVA: 0x005B2EA0 File Offset: 0x005B2EA0
    // (set) Token: 0x060014EE RID: 5358 RVA: 0x005B2EA8 File Offset: 0x005B2EA8
    public string Glyph { get; set; }

    // Token: 0x170003BD RID: 957
    // (get) Token: 0x060014EF RID: 5359 RVA: 0x005B2EB4 File Offset: 0x005B2EB4
    // (set) Token: 0x060014F0 RID: 5360 RVA: 0x005B2EBC File Offset: 0x005B2EBC
    public Color Tint { get; set; }

    // Token: 0x170003BE RID: 958
    // (get) Token: 0x060014F1 RID: 5361 RVA: 0x005B2EC8 File Offset: 0x005B2EC8
    // (set) Token: 0x060014F2 RID: 5362 RVA: 0x005B2ED0 File Offset: 0x005B2ED0
    public string KindLabel { get; set; }

    // Token: 0x060014F3 RID: 5363 RVA: 0x005B2EDC File Offset: 0x005B2EDC
    public static NotchDocumentStyle For(string path, bool isWebLink = false)
    {
      if (isWebLink)
      {
        return new NotchDocumentStyle("\ue71b", DS.Hex(3900150U, 1.0), "Link");
      }
      string text = Path.GetExtension(path).TrimStart('.').ToLowerInvariant();
      if (text != null)
      {
        switch (text.Length)
        {
        case 0:
          return new NotchDocumentStyle("\ue8b7", DS.Hex(959977U, 1.0), "Folder");
        case 1:
        {
          char c = text[0];
          if (c != 'c' && c != 'h')
          {
            goto IL_0934;
          }
          goto IL_089D;
        }
        case 2:
        {
          char c = text[0];
          if (c != '7')
          {
            switch (c)
            {
            case 'g':
              if (text == "go")
              {
                goto IL_089D;
              }
              if (!(text == "gz"))
              {
                goto IL_0934;
              }
              goto IL_08EA;
            case 'h':
            case 'i':
            case 'l':
            case 'n':
            case 'o':
            case 'q':
              goto IL_0934;
            case 'j':
              if (!(text == "js"))
              {
                goto IL_0934;
              }
              goto IL_089D;
            case 'k':
              if (!(text == "kt"))
              {
                goto IL_0934;
              }
              goto IL_089D;
            case 'm':
              if (!(text == "md"))
              {
                goto IL_0934;
              }
              goto IL_0785;
            case 'p':
              if (!(text == "py"))
              {
                goto IL_0934;
              }
              goto IL_089D;
            case 'r':
              if (!(text == "rb") && !(text == "rs"))
              {
                goto IL_0934;
              }
              goto IL_089D;
            case 's':
              if (!(text == "sh"))
              {
                goto IL_0934;
              }
              goto IL_089D;
            case 't':
              if (!(text == "ts"))
              {
                goto IL_0934;
              }
              goto IL_089D;
            default:
              goto IL_0934;
            }
          }
          else
          {
            if (!(text == "7z"))
            {
              goto IL_0934;
            }
            goto IL_08EA;
          }
          break;
        }
        case 3:
          switch (text[0])
          {
          case 'a':
            if (text == "aac")
            {
              goto IL_084D;
            }
            if (!(text == "app"))
            {
              goto IL_0934;
            }
            break;
          case 'b':
            if (text == "bmp")
            {
              goto IL_070D;
            }
            if (!(text == "bz2"))
            {
              goto IL_0934;
            }
            goto IL_08EA;
          case 'c':
            if (text == "csv")
            {
              goto IL_0735;
            }
            if (!(text == "cpp") && !(text == "css"))
            {
              goto IL_0934;
            }
            goto IL_089D;
          case 'd':
            if (text == "doc")
            {
              goto IL_075D;
            }
            if (!(text == "dmg"))
            {
              goto IL_0934;
            }
            goto IL_08EA;
          case 'e':
            if (!(text == "exe"))
            {
              goto IL_0934;
            }
            break;
          case 'f':
          case 'i':
          case 'l':
          case 'n':
          case 'q':
          case 'u':
          case 'v':
            goto IL_0934;
          case 'g':
            if (!(text == "gif"))
            {
              goto IL_0934;
            }
            goto IL_070D;
          case 'h':
            if (text == "htm")
            {
              goto IL_07D5;
            }
            if (!(text == "hpp"))
            {
              goto IL_0934;
            }
            goto IL_089D;
          case 'j':
            if (text == "jpg")
            {
              goto IL_070D;
            }
            if (!(text == "jsx"))
            {
              goto IL_0934;
            }
            goto IL_089D;
          case 'k':
            if (!(text == "key"))
            {
              goto IL_0934;
            }
            goto IL_07FD;
          case 'm':
            if (text == "mp4" || text == "mov" || text == "m4v")
            {
              goto IL_0825;
            }
            if (!(text == "mp3") && !(text == "m4a"))
            {
              goto IL_0934;
            }
            goto IL_084D;
          case 'o':
            if (!(text == "odt"))
            {
              goto IL_0934;
            }
            goto IL_075D;
          case 'p':
            if (text == "pdf")
            {
              return new NotchDocumentStyle("\uea90", DS.Hex(15680580U, 1.0), "PDF");
            }
            if (text == "png")
            {
              goto IL_070D;
            }
            if (!(text == "ppt"))
            {
              goto IL_0934;
            }
            goto IL_07FD;
          case 'r':
            if (!(text == "rtf"))
            {
              goto IL_0934;
            }
            goto IL_075D;
          case 's':
            if (!(text == "svg"))
            {
              goto IL_0934;
            }
            goto IL_070D;
          case 't':
            if (text == "tsv")
            {
              goto IL_0735;
            }
            if (text == "txt")
            {
              return new NotchDocumentStyle("\ue8a5", DS.Hex(6583435U, 1.0), "Text");
            }
            if (text == "tsx")
            {
              goto IL_089D;
            }
            if (!(text == "tar"))
            {
              goto IL_0934;
            }
            goto IL_08EA;
          case 'w':
            if (!(text == "wav"))
            {
              goto IL_0934;
            }
            goto IL_084D;
          case 'x':
            if (text == "xls")
            {
              goto IL_0735;
            }
            if (!(text == "xml"))
            {
              goto IL_0934;
            }
            goto IL_0875;
          case 'y':
            if (!(text == "yml"))
            {
              goto IL_0934;
            }
            goto IL_0875;
          case 'z':
            if (!(text == "zip"))
            {
              goto IL_0934;
            }
            goto IL_08EA;
          default:
            goto IL_0934;
          }
          return new NotchDocumentStyle("\ue737", DS.Hex(959977U, 1.0), "App");
        case 4:
        {
          char c = text[2];
          switch (c)
          {
          case 'a':
            if (!(text == "flac"))
            {
              goto IL_0934;
            }
            goto IL_084D;
          case 'b':
            if (!(text == "webp"))
            {
              if (!(text == "webm"))
              {
                goto IL_0934;
              }
              goto IL_0825;
            }
            break;
          case 'c':
            if (!(text == "docx"))
            {
              goto IL_0934;
            }
            goto IL_075D;
          case 'd':
          case 'g':
          case 'h':
          case 'j':
          case 'k':
          case 'l':
          case 'n':
            goto IL_0934;
          case 'e':
            if (!(text == "jpeg"))
            {
              goto IL_0934;
            }
            break;
          case 'f':
            if (!(text == "tiff"))
            {
              goto IL_0934;
            }
            break;
          case 'i':
            if (!(text == "heic"))
            {
              goto IL_0934;
            }
            break;
          case 'm':
            if (text == "html")
            {
              goto IL_07D5;
            }
            if (!(text == "yaml") && !(text == "toml"))
            {
              goto IL_0934;
            }
            goto IL_0875;
          case 'o':
            if (!(text == "json"))
            {
              goto IL_0934;
            }
            goto IL_0875;
          default:
            switch (c)
            {
            case 's':
              if (text == "xlsx")
              {
                goto IL_0735;
              }
              if (!(text == "scss"))
              {
                goto IL_0934;
              }
              goto IL_089D;
            case 't':
              if (!(text == "pptx"))
              {
                goto IL_0934;
              }
              goto IL_07FD;
            case 'u':
              goto IL_0934;
            case 'v':
              if (!(text == "java"))
              {
                goto IL_0934;
              }
              goto IL_089D;
            default:
              goto IL_0934;
            }
            break;
          }
          break;
        }
        case 5:
        {
          char c = text[0];
          if (c != 'p')
          {
            if (c != 's')
            {
              goto IL_0934;
            }
            if (!(text == "swift"))
            {
              goto IL_0934;
            }
            goto IL_089D;
          }
          else
          {
            if (!(text == "pages"))
            {
              goto IL_0934;
            }
            goto IL_075D;
          }
          break;
        }
        case 6:
          goto IL_0934;
        case 7:
          if (!(text == "numbers"))
          {
            goto IL_0934;
          }
          goto IL_0735;
        case 8:
          if (!(text == "markdown"))
          {
            goto IL_0934;
          }
          goto IL_0785;
        default:
          goto IL_0934;
        }
        IL_070D:
        return new NotchDocumentStyle("\ue91b", DS.Hex(15485081U, 1.0), "Image");
        IL_0735:
        return new NotchDocumentStyle("\ue80a", DS.Hex(2278750U, 1.0), "Spreadsheet");
        IL_075D:
        return new NotchDocumentStyle("\ue8a5", DS.Hex(2450411U, 1.0), "Document");
        IL_0785:
        return new NotchDocumentStyle("\ue8e4", DS.Hex(6514417U, 1.0), "Markdown");
        IL_07D5:
        return new NotchDocumentStyle("\ue774", DS.Hex(16347926U, 1.0), "Web page");
        IL_07FD:
        return new NotchDocumentStyle("\ue8fd", DS.Hex(16096779U, 1.0), "Slides");
        IL_0825:
        return new NotchDocumentStyle("\ue8b2", DS.Hex(9133302U, 1.0), "Video");
        IL_084D:
        return new NotchDocumentStyle("\ue8d6", DS.Hex(1357990U, 1.0), "Audio");
        IL_0875:
        return new NotchDocumentStyle("\ue943", DS.Hex(15381256U, 1.0), "Data");
        IL_089D:
        return new NotchDocumentStyle("\ue943", DS.Hex(11032055U, 1.0), "Code");
        IL_08EA:
        return new NotchDocumentStyle("\uf012", DS.Hex(7893356U, 1.0), "Archive");
      }
      IL_0934:
      return new NotchDocumentStyle("\ue8a5", DS.Hex(9741240U, 1.0), text.ToUpperInvariant());
    }

    // Token: 0x060014F4 RID: 5364 RVA: 0x005B3844 File Offset: 0x005B3844
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("NotchDocumentStyle");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060014F5 RID: 5365 RVA: 0x005B3890 File Offset: 0x005B3890
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Glyph = ");
      builder.Append(this.Glyph);
      builder.Append(", Tint = ");
      builder.Append(this.Tint.ToString());
      builder.Append(", KindLabel = ");
      builder.Append(this.KindLabel);
      return true;
    }

    // Token: 0x060014F6 RID: 5366 RVA: 0x005B38FC File Offset: 0x005B38FC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(NotchDocumentStyle left, NotchDocumentStyle right)
    {
      return !(left == right);
    }

    // Token: 0x060014F7 RID: 5367 RVA: 0x005B3908 File Offset: 0x005B3908
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(NotchDocumentStyle left, NotchDocumentStyle right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060014F8 RID: 5368 RVA: 0x005B391C File Offset: 0x005B391C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Glyph>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Tint>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<KindLabel>k__BackingField);
    }

    // Token: 0x060014F9 RID: 5369 RVA: 0x005B3980 File Offset: 0x005B3980
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as NotchDocumentStyle);
    }

    // Token: 0x060014FA RID: 5370 RVA: 0x005B3990 File Offset: 0x005B3990
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(NotchDocumentStyle other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Glyph>k__BackingField, other.<Glyph>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Tint>k__BackingField, other.<Tint>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<KindLabel>k__BackingField, other.<KindLabel>k__BackingField));
    }

    // Token: 0x060014FC RID: 5372 RVA: 0x005B3A0C File Offset: 0x005B3A0C
    [CompilerGenerated]
    private NotchDocumentStyle(NotchDocumentStyle original)
    {
      this.Glyph = original.<Glyph>k__BackingField;
      this.Tint = original.<Tint>k__BackingField;
      this.KindLabel = original.<KindLabel>k__BackingField;
    }

    // Token: 0x060014FD RID: 5373 RVA: 0x005B3A38 File Offset: 0x005B3A38
    [CompilerGenerated]
    public void Deconstruct(out string Glyph, out Color Tint, out string KindLabel)
    {
      Glyph = this.Glyph;
      Tint = this.Tint;
      KindLabel = this.KindLabel;
    }
  }
}
