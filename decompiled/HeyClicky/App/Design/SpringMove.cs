using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.UI.Composition;
using Windows.UI.Composition;

namespace HeyClicky.App.Design
{
  // Token: 0x0200034B RID: 843
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SpringMove : IEquatable<SpringMove>
  {
    // Token: 0x060023E6 RID: 9190 RVA: 0x00630490 File Offset: 0x00630490
    public SpringMove(float[] Progress, float[] Values, TimeSpan Duration)
    {
      this.Progress = Progress;
      this.Values = Values;
      this.Duration = Duration;
      base..ctor();
    }

    // Token: 0x17000695 RID: 1685
    // (get) Token: 0x060023E7 RID: 9191 RVA: 0x006304B0 File Offset: 0x006304B0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(SpringMove);
      }
    }

    // Token: 0x17000696 RID: 1686
    // (get) Token: 0x060023E8 RID: 9192 RVA: 0x006304BC File Offset: 0x006304BC
    // (set) Token: 0x060023E9 RID: 9193 RVA: 0x006304C4 File Offset: 0x006304C4
    public float[] Progress { get; set; }

    // Token: 0x17000697 RID: 1687
    // (get) Token: 0x060023EA RID: 9194 RVA: 0x006304D0 File Offset: 0x006304D0
    // (set) Token: 0x060023EB RID: 9195 RVA: 0x006304D8 File Offset: 0x006304D8
    public float[] Values { get; set; }

    // Token: 0x17000698 RID: 1688
    // (get) Token: 0x060023EC RID: 9196 RVA: 0x006304E4 File Offset: 0x006304E4
    // (set) Token: 0x060023ED RID: 9197 RVA: 0x006304EC File Offset: 0x006304EC
    public TimeSpan Duration { get; set; }

    // Token: 0x060023EE RID: 9198 RVA: 0x006304F8 File Offset: 0x006304F8
    public Microsoft.UI.Composition.ScalarKeyFrameAnimation Animation(Microsoft.UI.Composition.Compositor c)
    {
      Microsoft.UI.Composition.LinearEasingFunction linearEasingFunction = c.CreateLinearEasingFunction();
      Microsoft.UI.Composition.ScalarKeyFrameAnimation scalarKeyFrameAnimation = c.CreateScalarKeyFrameAnimation();
      for (int i = 0; i < this.Progress.Length; i++)
      {
        scalarKeyFrameAnimation.InsertKeyFrame(this.Progress[i], this.Values[i], linearEasingFunction);
      }
      scalarKeyFrameAnimation.Duration = this.Duration;
      return scalarKeyFrameAnimation;
    }

    // Token: 0x060023EF RID: 9199 RVA: 0x0063054C File Offset: 0x0063054C
    public Windows.UI.Composition.ScalarKeyFrameAnimation Animation(Windows.UI.Composition.Compositor c)
    {
      Windows.UI.Composition.LinearEasingFunction linearEasingFunction = c.CreateLinearEasingFunction();
      Windows.UI.Composition.ScalarKeyFrameAnimation scalarKeyFrameAnimation = c.CreateScalarKeyFrameAnimation();
      for (int i = 0; i < this.Progress.Length; i++)
      {
        scalarKeyFrameAnimation.InsertKeyFrame(this.Progress[i], this.Values[i], linearEasingFunction);
      }
      scalarKeyFrameAnimation.Duration = this.Duration;
      return scalarKeyFrameAnimation;
    }

    // Token: 0x060023F0 RID: 9200 RVA: 0x006305A0 File Offset: 0x006305A0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("SpringMove");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060023F1 RID: 9201 RVA: 0x006305EC File Offset: 0x006305EC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Progress = ");
      builder.Append(this.Progress);
      builder.Append(", Values = ");
      builder.Append(this.Values);
      builder.Append(", Duration = ");
      builder.Append(this.Duration.ToString());
      return true;
    }

    // Token: 0x060023F2 RID: 9202 RVA: 0x00630658 File Offset: 0x00630658
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(SpringMove left, SpringMove right)
    {
      return !(left == right);
    }

    // Token: 0x060023F3 RID: 9203 RVA: 0x00630664 File Offset: 0x00630664
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(SpringMove left, SpringMove right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060023F4 RID: 9204 RVA: 0x00630678 File Offset: 0x00630678
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<float[]>.Default.GetHashCode(this.<Progress>k__BackingField)) * -1521134295 + EqualityComparer<float[]>.Default.GetHashCode(this.<Values>k__BackingField)) * -1521134295 + EqualityComparer<TimeSpan>.Default.GetHashCode(this.<Duration>k__BackingField);
    }

    // Token: 0x060023F5 RID: 9205 RVA: 0x006306DC File Offset: 0x006306DC
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as SpringMove);
    }

    // Token: 0x060023F6 RID: 9206 RVA: 0x006306EC File Offset: 0x006306EC
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(SpringMove other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<float[]>.Default.Equals(this.<Progress>k__BackingField, other.<Progress>k__BackingField) && EqualityComparer<float[]>.Default.Equals(this.<Values>k__BackingField, other.<Values>k__BackingField) && EqualityComparer<TimeSpan>.Default.Equals(this.<Duration>k__BackingField, other.<Duration>k__BackingField));
    }

    // Token: 0x060023F8 RID: 9208 RVA: 0x00630768 File Offset: 0x00630768
    [CompilerGenerated]
    private SpringMove(SpringMove original)
    {
      this.Progress = original.<Progress>k__BackingField;
      this.Values = original.<Values>k__BackingField;
      this.Duration = original.<Duration>k__BackingField;
    }

    // Token: 0x060023F9 RID: 9209 RVA: 0x00630794 File Offset: 0x00630794
    [CompilerGenerated]
    public void Deconstruct(out float[] Progress, out float[] Values, out TimeSpan Duration)
    {
      Progress = this.Progress;
      Values = this.Values;
      Duration = this.Duration;
    }
  }
}
