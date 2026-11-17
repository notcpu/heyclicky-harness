using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.Platform.Win32;

namespace HeyClicky.App
{
  // Token: 0x02000080 RID: 128
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class AppOptions : IEquatable<AppOptions>
  {
    // Token: 0x060004CC RID: 1228 RVA: 0x0052B50C File Offset: 0x0052B50C
    public AppOptions(bool DemoStates, bool SignedOut, bool AllowCapture, bool Background, bool ShowIntro, bool ShowTutorial, Uri DeepLink, string DemoOnly, bool Fakes, string E2E)
    {
      this.DemoStates = DemoStates;
      this.SignedOut = SignedOut;
      this.AllowCapture = AllowCapture;
      this.Background = Background;
      this.ShowIntro = ShowIntro;
      this.ShowTutorial = ShowTutorial;
      this.DeepLink = DeepLink;
      this.DemoOnly = DemoOnly;
      this.Fakes = Fakes;
      this.E2E = E2E;
      base..ctor();
    }

    // Token: 0x1700009A RID: 154
    // (get) Token: 0x060004CD RID: 1229 RVA: 0x0052B56C File Offset: 0x0052B56C
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(AppOptions);
      }
    }

    // Token: 0x1700009B RID: 155
    // (get) Token: 0x060004CE RID: 1230 RVA: 0x0052B578 File Offset: 0x0052B578
    // (set) Token: 0x060004CF RID: 1231 RVA: 0x0052B580 File Offset: 0x0052B580
    public bool DemoStates { get; set; }

    // Token: 0x1700009C RID: 156
    // (get) Token: 0x060004D0 RID: 1232 RVA: 0x0052B58C File Offset: 0x0052B58C
    // (set) Token: 0x060004D1 RID: 1233 RVA: 0x0052B594 File Offset: 0x0052B594
    public bool SignedOut { get; set; }

    // Token: 0x1700009D RID: 157
    // (get) Token: 0x060004D2 RID: 1234 RVA: 0x0052B5A0 File Offset: 0x0052B5A0
    // (set) Token: 0x060004D3 RID: 1235 RVA: 0x0052B5A8 File Offset: 0x0052B5A8
    public bool AllowCapture { get; set; }

    // Token: 0x1700009E RID: 158
    // (get) Token: 0x060004D4 RID: 1236 RVA: 0x0052B5B4 File Offset: 0x0052B5B4
    // (set) Token: 0x060004D5 RID: 1237 RVA: 0x0052B5BC File Offset: 0x0052B5BC
    public bool Background { get; set; }

    // Token: 0x1700009F RID: 159
    // (get) Token: 0x060004D6 RID: 1238 RVA: 0x0052B5C8 File Offset: 0x0052B5C8
    // (set) Token: 0x060004D7 RID: 1239 RVA: 0x0052B5D0 File Offset: 0x0052B5D0
    public bool ShowIntro { get; set; }

    // Token: 0x170000A0 RID: 160
    // (get) Token: 0x060004D8 RID: 1240 RVA: 0x0052B5DC File Offset: 0x0052B5DC
    // (set) Token: 0x060004D9 RID: 1241 RVA: 0x0052B5E4 File Offset: 0x0052B5E4
    public bool ShowTutorial { get; set; }

    // Token: 0x170000A1 RID: 161
    // (get) Token: 0x060004DA RID: 1242 RVA: 0x0052B5F0 File Offset: 0x0052B5F0
    // (set) Token: 0x060004DB RID: 1243 RVA: 0x0052B5F8 File Offset: 0x0052B5F8
    public Uri DeepLink { get; set; }

    // Token: 0x170000A2 RID: 162
    // (get) Token: 0x060004DC RID: 1244 RVA: 0x0052B604 File Offset: 0x0052B604
    // (set) Token: 0x060004DD RID: 1245 RVA: 0x0052B60C File Offset: 0x0052B60C
    public string DemoOnly { get; set; }

    // Token: 0x170000A3 RID: 163
    // (get) Token: 0x060004DE RID: 1246 RVA: 0x0052B618 File Offset: 0x0052B618
    // (set) Token: 0x060004DF RID: 1247 RVA: 0x0052B620 File Offset: 0x0052B620
    public bool Fakes { get; set; }

    // Token: 0x170000A4 RID: 164
    // (get) Token: 0x060004E0 RID: 1248 RVA: 0x0052B62C File Offset: 0x0052B62C
    // (set) Token: 0x060004E1 RID: 1249 RVA: 0x0052B634 File Offset: 0x0052B634
    public string E2E { get; set; }

    // Token: 0x170000A5 RID: 165
    // (get) Token: 0x060004E2 RID: 1250 RVA: 0x0052B640 File Offset: 0x0052B640
    public bool UseFakes
    {
      get
      {
        return this.Fakes || this.DemoStates || this.DemoOnly != null;
      }
    }

    // Token: 0x060004E3 RID: 1251 RVA: 0x0052B660 File Offset: 0x0052B660
    [NullableContext(1)]
    public static AppOptions Parse(IReadOnlyList<string> args)
    {
      AppOptions.<>c__DisplayClass45_0 CS$<>8__locals1;
      CS$<>8__locals1.args = args;
      Uri uri = ProtocolRegistration.FindDeepLink(CS$<>8__locals1.args);
      bool flag = AppOptions.<Parse>g__Has|45_0("--demo-states", ref CS$<>8__locals1);
      return new AppOptions(flag, AppOptions.<Parse>g__Has|45_0("--signed-out", ref CS$<>8__locals1), flag || AppOptions.<Parse>g__Has|45_0("--allow-capture", ref CS$<>8__locals1) || AppOptions.<Parse>g__Value|45_1("--demo-only", ref CS$<>8__locals1) != null, AppOptions.<Parse>g__Has|45_0("--background", ref CS$<>8__locals1) || AppOptions.<Parse>g__Has|45_0("--launched-at-login", ref CS$<>8__locals1), AppOptions.<Parse>g__Has|45_0("--intro", ref CS$<>8__locals1), AppOptions.<Parse>g__Has|45_0("--tutorial", ref CS$<>8__locals1), uri, AppOptions.<Parse>g__Value|45_1("--demo-only", ref CS$<>8__locals1), AppOptions.<Parse>g__Has|45_0("--fakes", ref CS$<>8__locals1), AppOptions.<Parse>g__Value|45_1("--e2e", ref CS$<>8__locals1));
    }

    // Token: 0x060004E4 RID: 1252 RVA: 0x0052B720 File Offset: 0x0052B720
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AppOptions");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060004E5 RID: 1253 RVA: 0x0052B76C File Offset: 0x0052B76C
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("DemoStates = ");
      builder.Append(this.DemoStates.ToString());
      builder.Append(", SignedOut = ");
      builder.Append(this.SignedOut.ToString());
      builder.Append(", AllowCapture = ");
      builder.Append(this.AllowCapture.ToString());
      builder.Append(", Background = ");
      builder.Append(this.Background.ToString());
      builder.Append(", ShowIntro = ");
      builder.Append(this.ShowIntro.ToString());
      builder.Append(", ShowTutorial = ");
      builder.Append(this.ShowTutorial.ToString());
      builder.Append(", DeepLink = ");
      builder.Append(this.DeepLink);
      builder.Append(", DemoOnly = ");
      builder.Append(this.DemoOnly);
      builder.Append(", Fakes = ");
      builder.Append(this.Fakes.ToString());
      builder.Append(", E2E = ");
      builder.Append(this.E2E);
      builder.Append(", UseFakes = ");
      builder.Append(this.UseFakes.ToString());
      return true;
    }

    // Token: 0x060004E6 RID: 1254 RVA: 0x0052B904 File Offset: 0x0052B904
    [CompilerGenerated]
    public static bool operator !=(AppOptions left, AppOptions right)
    {
      return !(left == right);
    }

    // Token: 0x060004E7 RID: 1255 RVA: 0x0052B910 File Offset: 0x0052B910
    [CompilerGenerated]
    public static bool operator ==(AppOptions left, AppOptions right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060004E8 RID: 1256 RVA: 0x0052B924 File Offset: 0x0052B924
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<DemoStates>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<SignedOut>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<AllowCapture>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Background>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ShowIntro>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ShowTutorial>k__BackingField)) * -1521134295 + EqualityComparer<Uri>.Default.GetHashCode(this.<DeepLink>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DemoOnly>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Fakes>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<E2E>k__BackingField);
    }

    // Token: 0x060004E9 RID: 1257 RVA: 0x0052BA28 File Offset: 0x0052BA28
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AppOptions);
    }

    // Token: 0x060004EA RID: 1258 RVA: 0x0052BA38 File Offset: 0x0052BA38
    [CompilerGenerated]
    public bool Equals(AppOptions other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<bool>.Default.Equals(this.<DemoStates>k__BackingField, other.<DemoStates>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<SignedOut>k__BackingField, other.<SignedOut>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<AllowCapture>k__BackingField, other.<AllowCapture>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Background>k__BackingField, other.<Background>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<ShowIntro>k__BackingField, other.<ShowIntro>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<ShowTutorial>k__BackingField, other.<ShowTutorial>k__BackingField) && EqualityComparer<Uri>.Default.Equals(this.<DeepLink>k__BackingField, other.<DeepLink>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DemoOnly>k__BackingField, other.<DemoOnly>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Fakes>k__BackingField, other.<Fakes>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<E2E>k__BackingField, other.<E2E>k__BackingField));
    }

    // Token: 0x060004EC RID: 1260 RVA: 0x0052BB70 File Offset: 0x0052BB70
    [CompilerGenerated]
    private AppOptions([Nullable(1)] AppOptions original)
    {
      this.DemoStates = original.<DemoStates>k__BackingField;
      this.SignedOut = original.<SignedOut>k__BackingField;
      this.AllowCapture = original.<AllowCapture>k__BackingField;
      this.Background = original.<Background>k__BackingField;
      this.ShowIntro = original.<ShowIntro>k__BackingField;
      this.ShowTutorial = original.<ShowTutorial>k__BackingField;
      this.DeepLink = original.<DeepLink>k__BackingField;
      this.DemoOnly = original.<DemoOnly>k__BackingField;
      this.Fakes = original.<Fakes>k__BackingField;
      this.E2E = original.<E2E>k__BackingField;
    }

    // Token: 0x060004ED RID: 1261 RVA: 0x0052BBFC File Offset: 0x0052BBFC
    [CompilerGenerated]
    public void Deconstruct(out bool DemoStates, out bool SignedOut, out bool AllowCapture, out bool Background, out bool ShowIntro, out bool ShowTutorial, out Uri DeepLink, out string DemoOnly, out bool Fakes, out string E2E)
    {
      DemoStates = this.DemoStates;
      SignedOut = this.SignedOut;
      AllowCapture = this.AllowCapture;
      Background = this.Background;
      ShowIntro = this.ShowIntro;
      ShowTutorial = this.ShowTutorial;
      DeepLink = this.DeepLink;
      DemoOnly = this.DemoOnly;
      Fakes = this.Fakes;
      E2E = this.E2E;
    }

    // Token: 0x060004EE RID: 1262 RVA: 0x0052BC60 File Offset: 0x0052BC60
    [NullableContext(1)]
    [CompilerGenerated]
    internal static bool <Parse>g__Has|45_0(string f, ref AppOptions.<>c__DisplayClass45_0 A_1)
    {
      return A_1.args.Any<string>((string a) => string.Equals(a, f, StringComparison.OrdinalIgnoreCase));
    }

    // Token: 0x060004EF RID: 1263 RVA: 0x0052BC94 File Offset: 0x0052BC94
    [NullableContext(1)]
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <Parse>g__Value|45_1(string f, ref AppOptions.<>c__DisplayClass45_0 A_1)
    {
      int num = A_1.args.ToList<string>().FindIndex((string a) => string.Equals(a, f, StringComparison.OrdinalIgnoreCase));
      if (num < 0 || num + 1 >= A_1.args.Count)
      {
        return null;
      }
      return A_1.args[num + 1];
    }
  }
}
