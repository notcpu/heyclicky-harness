using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000006 RID: 6
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordParagraphEnd_4 : Regex
  {
    // Token: 0x06000009 RID: 9 RVA: 0x00331554 File Offset: 0x00331554
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordParagraphEnd_4()
    {
      this.pattern = "</w:p>";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordParagraphEnd_4.RunnerFactory();
      this.capsize = 1;
    }

    // Token: 0x04000005 RID: 5
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordParagraphEnd_4 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordParagraphEnd_4();

    // Token: 0x02000239 RID: 569
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x060020FC RID: 8444 RVA: 0x003F6CE0 File Offset: 0x003F6CE0
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordParagraphEnd_4.RunnerFactory.Runner();
      }

      // Token: 0x020006A3 RID: 1699
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A1C RID: 14876 RVA: 0x0045AA5C File Offset: 0x0045AA5C
        protected override void Scan(ReadOnlySpan<char> inputSpan)
        {
          if (this.TryFindNextPossibleStartingPosition(inputSpan))
          {
            int runtextpos = this.runtextpos;
            int num = (this.runtextpos = runtextpos + 6);
            base.Capture(0, runtextpos, num);
          }
        }

        // Token: 0x06003A1D RID: 14877 RVA: 0x0045AA90 File Offset: 0x0045AA90
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 6)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_D474EFD1CA39D108A6187B4741FD0C789626191596A3DE21E8493678667EC153);
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }
      }
    }
  }
}
