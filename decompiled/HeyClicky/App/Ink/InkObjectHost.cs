using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000247 RID: 583
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class InkObjectHost : Grid, IInkBaseline
  {
    // Token: 0x060019FF RID: 6655 RVA: 0x005DC148 File Offset: 0x005DC148
    public InkObjectHost(InkObjectState state, Func<FrameworkElement> build)
    {
      this.build = build;
      this.environment = ClickyInkEnvironment.Current;
      state.Attach(this);
      base.HorizontalAlignment = HorizontalAlignment.Left;
      base.Children.Add(build());
    }

    // Token: 0x17000487 RID: 1159
    // (get) Token: 0x06001A00 RID: 6656 RVA: 0x005DC184 File Offset: 0x005DC184
    public double? FirstBaseline
    {
      get
      {
        if (base.Children.Count <= 0)
        {
          return null;
        }
        return InkBaseline.First(base.Children[0]);
      }
    }

    // Token: 0x06001A01 RID: 6657 RVA: 0x005DC1BC File Offset: 0x005DC1BC
    public void Refresh()
    {
      FrameworkElement frameworkElement;
      using (ClickyInkEnvironment.Use(this.environment))
      {
        frameworkElement = this.build();
      }
      base.Children.Clear();
      base.Children.Add(frameworkElement);
    }

    // Token: 0x04000C88 RID: 3208
    private readonly Func<FrameworkElement> build;

    // Token: 0x04000C89 RID: 3209
    private readonly ClickyInkEnvironment environment;
  }
}
