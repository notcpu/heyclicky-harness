using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000250 RID: 592
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkInlineAnswerField : Grid
  {
    // Token: 0x1700048A RID: 1162
    // (get) Token: 0x06001A29 RID: 6697 RVA: 0x005DD848 File Offset: 0x005DD848
    // (set) Token: 0x06001A2A RID: 6698 RVA: 0x005DD850 File Offset: 0x005DD850
    public Action<string> OnSend { get; set; } = delegate(string _)
    {
    };

    // Token: 0x06001A2B RID: 6699 RVA: 0x005DD85C File Offset: 0x005DD85C
    public ClickyInkInlineAnswerField(string placeholder, string sendTitle = "Send")
    {
      this.line = new ClickyInkBlankLine(placeholder, PaperColor.Hex("#3B2A10"), null);
      this.send = new ClickyPaperGelButton(sendTitle, new Action(this.Send), ClickyPaperGel.Blue, 26.0, 13.0, 12.0, null);
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      base.ColumnSpacing = 10.0;
      this.line.VerticalAlignment = VerticalAlignment.Bottom;
      this.send.VerticalAlignment = VerticalAlignment.Bottom;
      Grid.SetColumn(this.send, 1);
      base.Children.Add(this.line);
      base.Children.Add(this.send);
      this.line.Submitted += this.Send;
      this.line.TextChanged += delegate(string _)
      {
        this.Refresh();
      };
      base.HorizontalAlignment = HorizontalAlignment.Stretch;
      this.Refresh();
    }

    // Token: 0x1700048B RID: 1163
    // (get) Token: 0x06001A2C RID: 6700 RVA: 0x005DD9B0 File Offset: 0x005DD9B0
    private string Trimmed
    {
      get
      {
        return this.line.Text.TrimWsNl();
      }
    }

    // Token: 0x06001A2D RID: 6701 RVA: 0x005DD9C4 File Offset: 0x005DD9C4
    private void Refresh()
    {
      bool flag = this.Trimmed.Length == 0;
      this.send.Enabled = !flag;
      this.send.Opacity = (flag ? 0.5 : 1.0);
    }

    // Token: 0x06001A2E RID: 6702 RVA: 0x005DDA14 File Offset: 0x005DDA14
    private void Send()
    {
      if (this.Trimmed.Length == 0)
      {
        return;
      }
      this.OnSend(this.Trimmed);
      this.line.Text = "";
    }

    // Token: 0x06001A2F RID: 6703 RVA: 0x005DDA48 File Offset: 0x005DDA48
    public void SetDraft(string text)
    {
      this.line.Text = text;
    }

    // Token: 0x04000C9D RID: 3229
    private readonly ClickyInkBlankLine line;

    // Token: 0x04000C9E RID: 3230
    private readonly ClickyPaperGelButton send;
  }
}
