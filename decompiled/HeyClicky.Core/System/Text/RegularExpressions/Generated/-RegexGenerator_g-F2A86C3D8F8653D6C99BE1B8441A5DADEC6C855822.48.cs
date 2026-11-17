using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000031 RID: 49
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__SpotifyId_47 : Regex
  {
    // Token: 0x0600005F RID: 95 RVA: 0x00332434 File Offset: 0x00332434
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__SpotifyId_47()
    {
      this.pattern = "^[A-Za-z0-9]{22}$";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__SpotifyId_47.RunnerFactory();
      this.capsize = 1;
    }

    // Token: 0x04000030 RID: 48
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__SpotifyId_47 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__SpotifyId_47();

    // Token: 0x02000264 RID: 612
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002152 RID: 8530 RVA: 0x003F6F90 File Offset: 0x003F6F90
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__SpotifyId_47.RunnerFactory.Runner();
      }

      // Token: 0x020006CE RID: 1742
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003AE3 RID: 15075 RVA: 0x004613B0 File Offset: 0x004613B0
        protected override void Scan(ReadOnlySpan<char> inputSpan)
        {
          if (this.TryFindNextPossibleStartingPosition(inputSpan) && !this.TryMatchAtCurrentPosition(inputSpan))
          {
            this.runtextpos = inputSpan.Length;
          }
        }

        // Token: 0x06003AE4 RID: 15076 RVA: 0x004613D4 File Offset: 0x004613D4
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 22 && runtextpos == 0)
          {
            return true;
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003AE5 RID: 15077 RVA: 0x00461408 File Offset: 0x00461408
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (num != 0)
          {
            return false;
          }
          if (readOnlySpan.Length < 22)
          {
            return false;
          }
          if (readOnlySpan.Slice(0, 22).IndexOfAnyExcept<char>(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_asciiLettersAndDigits) >= 0)
          {
            return false;
          }
          if (23 < readOnlySpan.Length || (22 < readOnlySpan.Length && *readOnlySpan[22] != 10))
          {
            return false;
          }
          num += 22;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }
      }
    }
  }
}
