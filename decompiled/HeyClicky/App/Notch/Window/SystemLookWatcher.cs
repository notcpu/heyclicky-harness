using System;
using System.Runtime.CompilerServices;
using Windows.UI.ViewManagement;

namespace HeyClicky.App.Notch.Window
{
  // Token: 0x02000147 RID: 327
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class SystemLookWatcher : IDisposable
  {
    // Token: 0x0600128C RID: 4748 RVA: 0x005977F4 File Offset: 0x005977F4
    public SystemLookWatcher(Action changed)
    {
      this.changed = changed;
      this.settings.ColorValuesChanged += this.OnChanged;
    }

    // Token: 0x0600128D RID: 4749 RVA: 0x00597828 File Offset: 0x00597828
    private void OnChanged(UISettings sender, object args)
    {
      this.changed();
    }

    // Token: 0x0600128E RID: 4750 RVA: 0x00597838 File Offset: 0x00597838
    public void Dispose()
    {
      this.settings.ColorValuesChanged -= this.OnChanged;
    }

    // Token: 0x04000886 RID: 2182
    private readonly UISettings settings = new UISettings();

    // Token: 0x04000887 RID: 2183
    private readonly Action changed;
  }
}
