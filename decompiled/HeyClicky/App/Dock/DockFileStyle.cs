using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.App.Design;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000331 RID: 817
  [NullableContext(1)]
  [Nullable(0)]
  internal readonly struct DockFileStyle : IEquatable<DockFileStyle>
  {
    // Token: 0x060022DB RID: 8923 RVA: 0x00628918 File Offset: 0x00628918
    public DockFileStyle(string Glyph, Color Tint, string Kind)
    {
      this.Glyph = Glyph;
      this.Tint = Tint;
      this.Kind = Kind;
    }

    // Token: 0x1700066D RID: 1645
    // (get) Token: 0x060022DC RID: 8924 RVA: 0x00628930 File Offset: 0x00628930
    // (set) Token: 0x060022DD RID: 8925 RVA: 0x00628938 File Offset: 0x00628938
    public string Glyph { get; set; }

    // Token: 0x1700066E RID: 1646
    // (get) Token: 0x060022DE RID: 8926 RVA: 0x00628944 File Offset: 0x00628944
    // (set) Token: 0x060022DF RID: 8927 RVA: 0x0062894C File Offset: 0x0062894C
    public Color Tint { get; set; }

    // Token: 0x1700066F RID: 1647
    // (get) Token: 0x060022E0 RID: 8928 RVA: 0x00628958 File Offset: 0x00628958
    // (set) Token: 0x060022E1 RID: 8929 RVA: 0x00628960 File Offset: 0x00628960
    public string Kind { get; set; }

    // Token: 0x060022E2 RID: 8930 RVA: 0x0062896C File Offset: 0x0062896C
    public static DockFileStyle For(string entry)
    {
      if (DockFiles.IsWebLink(entry))
      {
        return new DockFileStyle("\ue71b", DS.Hex(3900150U, 1.0), "Link");
      }
      string text = Path.GetExtension(entry).TrimStart('.').ToLowerInvariant();
      if (text != null)
      {
        switch (text.Length)
        {
        case 0:
          return new DockFileStyle("\ue8b7", DS.Hex(959977U, 1.0), "Folder");
        case 1:
        {
          char c = text[0];
          if (c != 'c' && c != 'h')
          {
            goto IL_0919;
          }
          goto IL_08AA;
        }
        case 2:
        {
          char c = text[0];
          if (c != '7')
          {
            if (c != 'c')
            {
              switch (c)
              {
              case 'g':
                if (text == "go")
                {
                  goto IL_08AA;
                }
                if (!(text == "gz"))
                {
                  goto IL_0919;
                }
                goto IL_08CF;
              case 'h':
              case 'i':
              case 'l':
              case 'n':
              case 'o':
              case 'q':
                goto IL_0919;
              case 'j':
                if (!(text == "js"))
                {
                  goto IL_0919;
                }
                goto IL_08AA;
              case 'k':
                if (!(text == "kt"))
                {
                  goto IL_0919;
                }
                goto IL_08AA;
              case 'm':
                if (!(text == "md"))
                {
                  goto IL_0919;
                }
                goto IL_0792;
              case 'p':
                if (!(text == "py"))
                {
                  goto IL_0919;
                }
                goto IL_08AA;
              case 'r':
                if (!(text == "rb") && !(text == "rs"))
                {
                  goto IL_0919;
                }
                goto IL_08AA;
              case 's':
                if (!(text == "sh"))
                {
                  goto IL_0919;
                }
                goto IL_08AA;
              case 't':
                if (!(text == "ts"))
                {
                  goto IL_0919;
                }
                goto IL_08AA;
              default:
                goto IL_0919;
              }
            }
            else
            {
              if (!(text == "cs"))
              {
                goto IL_0919;
              }
              goto IL_08AA;
            }
          }
          else
          {
            if (!(text == "7z"))
            {
              goto IL_0919;
            }
            goto IL_08CF;
          }
          break;
        }
        case 3:
          switch (text[0])
          {
          case 'a':
            if (!(text == "aac"))
            {
              goto IL_0919;
            }
            goto IL_085A;
          case 'b':
            if (!(text == "bmp"))
            {
              if (!(text == "bz2"))
              {
                goto IL_0919;
              }
              goto IL_08CF;
            }
            break;
          case 'c':
            if (text == "csv")
            {
              goto IL_0742;
            }
            if (!(text == "cpp") && !(text == "css"))
            {
              goto IL_0919;
            }
            goto IL_08AA;
          case 'd':
            if (text == "doc")
            {
              goto IL_076A;
            }
            if (!(text == "dmg"))
            {
              goto IL_0919;
            }
            goto IL_08CF;
          case 'e':
          case 'f':
          case 'i':
          case 'l':
          case 'n':
          case 'q':
          case 'u':
          case 'v':
            goto IL_0919;
          case 'g':
            if (!(text == "gif"))
            {
              goto IL_0919;
            }
            break;
          case 'h':
            if (text == "htm")
            {
              goto IL_07E2;
            }
            if (!(text == "hpp"))
            {
              goto IL_0919;
            }
            goto IL_08AA;
          case 'j':
            if (!(text == "jpg"))
            {
              if (!(text == "jsx"))
              {
                goto IL_0919;
              }
              goto IL_08AA;
            }
            break;
          case 'k':
            if (!(text == "key"))
            {
              goto IL_0919;
            }
            goto IL_080A;
          case 'm':
            if (text == "mp4" || text == "mov" || text == "m4v")
            {
              goto IL_0832;
            }
            if (!(text == "mp3") && !(text == "m4a"))
            {
              goto IL_0919;
            }
            goto IL_085A;
          case 'o':
            if (!(text == "odt"))
            {
              goto IL_0919;
            }
            goto IL_076A;
          case 'p':
            if (text == "pdf")
            {
              return new DockFileStyle("\ue8a5", DS.Hex(15680580U, 1.0), "PDF");
            }
            if (!(text == "png"))
            {
              if (text == "ppt")
              {
                goto IL_080A;
              }
              if (!(text == "ps1"))
              {
                goto IL_0919;
              }
              goto IL_08AA;
            }
            break;
          case 'r':
            if (!(text == "rtf"))
            {
              goto IL_0919;
            }
            goto IL_076A;
          case 's':
            if (!(text == "svg"))
            {
              goto IL_0919;
            }
            break;
          case 't':
            if (text == "tsv")
            {
              goto IL_0742;
            }
            if (text == "txt")
            {
              return new DockFileStyle("\ue8a5", DS.Hex(6583435U, 1.0), "Text");
            }
            if (text == "tsx")
            {
              goto IL_08AA;
            }
            if (!(text == "tar"))
            {
              goto IL_0919;
            }
            goto IL_08CF;
          case 'w':
            if (!(text == "wav"))
            {
              goto IL_0919;
            }
            goto IL_085A;
          case 'x':
            if (text == "xls")
            {
              goto IL_0742;
            }
            if (!(text == "xml"))
            {
              goto IL_0919;
            }
            goto IL_0882;
          case 'y':
            if (!(text == "yml"))
            {
              goto IL_0919;
            }
            goto IL_0882;
          case 'z':
            if (!(text == "zip"))
            {
              goto IL_0919;
            }
            goto IL_08CF;
          default:
            goto IL_0919;
          }
          break;
        case 4:
        {
          char c = text[2];
          switch (c)
          {
          case 'a':
            if (!(text == "flac"))
            {
              goto IL_0919;
            }
            goto IL_085A;
          case 'b':
            if (!(text == "webp"))
            {
              if (!(text == "webm"))
              {
                goto IL_0919;
              }
              goto IL_0832;
            }
            break;
          case 'c':
            if (!(text == "docx"))
            {
              goto IL_0919;
            }
            goto IL_076A;
          case 'd':
          case 'g':
          case 'h':
          case 'j':
          case 'k':
          case 'l':
          case 'n':
            goto IL_0919;
          case 'e':
            if (!(text == "jpeg"))
            {
              goto IL_0919;
            }
            break;
          case 'f':
            if (!(text == "tiff"))
            {
              goto IL_0919;
            }
            break;
          case 'i':
            if (!(text == "heic"))
            {
              goto IL_0919;
            }
            break;
          case 'm':
            if (text == "html")
            {
              goto IL_07E2;
            }
            if (!(text == "yaml") && !(text == "toml"))
            {
              goto IL_0919;
            }
            goto IL_0882;
          case 'o':
            if (!(text == "json"))
            {
              goto IL_0919;
            }
            goto IL_0882;
          default:
            switch (c)
            {
            case 's':
              if (text == "xlsx")
              {
                goto IL_0742;
              }
              if (!(text == "scss"))
              {
                goto IL_0919;
              }
              goto IL_08AA;
            case 't':
              if (!(text == "pptx"))
              {
                goto IL_0919;
              }
              goto IL_080A;
            case 'u':
              goto IL_0919;
            case 'v':
              if (!(text == "java"))
              {
                goto IL_0919;
              }
              goto IL_08AA;
            default:
              goto IL_0919;
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
              goto IL_0919;
            }
            if (!(text == "swift"))
            {
              goto IL_0919;
            }
            goto IL_08AA;
          }
          else
          {
            if (!(text == "pages"))
            {
              goto IL_0919;
            }
            goto IL_076A;
          }
          break;
        }
        case 6:
          goto IL_0919;
        case 7:
          if (!(text == "numbers"))
          {
            goto IL_0919;
          }
          goto IL_0742;
        case 8:
          if (!(text == "markdown"))
          {
            goto IL_0919;
          }
          goto IL_0792;
        default:
          goto IL_0919;
        }
        return new DockFileStyle("\ueb9f", DS.Hex(15485081U, 1.0), "Image");
        IL_0742:
        return new DockFileStyle("\ue80a", DS.Hex(2278750U, 1.0), "Spreadsheet");
        IL_076A:
        return new DockFileStyle("\ue8a5", DS.Hex(2450411U, 1.0), "Document");
        IL_0792:
        return new DockFileStyle("\ue8e4", DS.Hex(6514417U, 1.0), "Markdown");
        IL_07E2:
        return new DockFileStyle("\ue774", DS.Hex(16347926U, 1.0), "Web page");
        IL_080A:
        return new DockFileStyle("\ue8a1", DS.Hex(16096779U, 1.0), "Slides");
        IL_0832:
        return new DockFileStyle("\ue8b2", DS.Hex(9133302U, 1.0), "Video");
        IL_085A:
        return new DockFileStyle("\ue8d6", DS.Hex(1357990U, 1.0), "Audio");
        IL_0882:
        return new DockFileStyle("\ue943", DS.Hex(15381256U, 1.0), "Data");
        IL_08AA:
        return new DockFileStyle("\ue943", DS.Hex(11032055U, 1.0), "Code");
        IL_08CF:
        return new DockFileStyle("\uf012", DS.Hex(7893356U, 1.0), "Archive");
      }
      IL_0919:
      return new DockFileStyle("\ue8a5", DS.Hex(9741240U, 1.0), text.ToUpperInvariant());
    }

    // Token: 0x060022E3 RID: 8931 RVA: 0x006292B8 File Offset: 0x006292B8
    [NullableContext(0)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DockFileStyle");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060022E4 RID: 8932 RVA: 0x00629304 File Offset: 0x00629304
    [NullableContext(0)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("Glyph = ");
      builder.Append(this.Glyph);
      builder.Append(", Tint = ");
      builder.Append(this.Tint.ToString());
      builder.Append(", Kind = ");
      builder.Append(this.Kind);
      return true;
    }

    // Token: 0x060022E5 RID: 8933 RVA: 0x0062936C File Offset: 0x0062936C
    [CompilerGenerated]
    public static bool operator !=(DockFileStyle left, DockFileStyle right)
    {
      return !(left == right);
    }

    // Token: 0x060022E6 RID: 8934 RVA: 0x00629378 File Offset: 0x00629378
    [CompilerGenerated]
    public static bool operator ==(DockFileStyle left, DockFileStyle right)
    {
      return left.Equals(right);
    }

    // Token: 0x060022E7 RID: 8935 RVA: 0x00629384 File Offset: 0x00629384
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<string>.Default.GetHashCode(this.<Glyph>k__BackingField) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Tint>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Kind>k__BackingField);
    }

    // Token: 0x060022E8 RID: 8936 RVA: 0x006293C4 File Offset: 0x006293C4
    [NullableContext(0)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is DockFileStyle && this.Equals((DockFileStyle)obj);
    }

    // Token: 0x060022E9 RID: 8937 RVA: 0x006293DC File Offset: 0x006293DC
    [CompilerGenerated]
    public bool Equals(DockFileStyle other)
    {
      return EqualityComparer<string>.Default.Equals(this.<Glyph>k__BackingField, other.<Glyph>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Tint>k__BackingField, other.<Tint>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField);
    }

    // Token: 0x060022EA RID: 8938 RVA: 0x00629434 File Offset: 0x00629434
    [CompilerGenerated]
    public void Deconstruct(out string Glyph, out Color Tint, out string Kind)
    {
      Glyph = this.Glyph;
      Tint = this.Tint;
      Kind = this.Kind;
    }
  }
}
