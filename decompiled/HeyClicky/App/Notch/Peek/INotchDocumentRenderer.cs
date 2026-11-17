using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000185 RID: 389
  [NullableContext(1)]
  public interface INotchDocumentRenderer
  {
    // Token: 0x06001517 RID: 5399
    bool CanRender(string path);

    // Token: 0x06001518 RID: 5400
    FrameworkElement Render(string path, Action reload);
  }
}
