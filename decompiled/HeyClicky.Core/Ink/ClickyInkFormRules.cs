using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000156 RID: 342
  public static class ClickyInkFormRules
  {
    // Token: 0x06001502 RID: 5378 RVA: 0x003B2E2C File Offset: 0x003B2E2C
    [NullableContext(1)]
    public static ClickyInkFormRules.FieldKind Kind(string hint)
    {
      if (ClickyInkChoices.Of(hint).Count > 1)
      {
        return ClickyInkFormRules.FieldKind.Choices;
      }
      string text = hint.Lowered();
      if (!(text == "file") && !(text == "folder"))
      {
        return ClickyInkFormRules.FieldKind.Blank;
      }
      return ClickyInkFormRules.FieldKind.File;
    }

    // Token: 0x020004FC RID: 1276
    public enum FieldKind
    {
      // Token: 0x04001EE8 RID: 7912
      Choices,
      // Token: 0x04001EE9 RID: 7913
      File,
      // Token: 0x04001EEA RID: 7914
      Blank
    }
  }
}
