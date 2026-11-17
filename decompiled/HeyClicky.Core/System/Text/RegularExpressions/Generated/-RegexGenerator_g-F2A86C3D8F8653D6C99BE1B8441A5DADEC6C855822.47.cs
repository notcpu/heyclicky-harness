using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000030 RID: 48
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BigRegex_46 : Regex
  {
    // Token: 0x0600005D RID: 93 RVA: 0x003323DC File Offset: 0x003323DC
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BigRegex_46()
    {
      this.pattern = "\\{[^}]*big=";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BigRegex_46.RunnerFactory();
      this.capsize = 1;
    }

    // Token: 0x0400002F RID: 47
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BigRegex_46 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BigRegex_46();

    // Token: 0x02000263 RID: 611
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002150 RID: 8528 RVA: 0x003F6F80 File Offset: 0x003F6F80
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BigRegex_46.RunnerFactory.Runner();
      }

      // Token: 0x020006CD RID: 1741
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003ADF RID: 15071 RVA: 0x00461224 File Offset: 0x00461224
        protected override void Scan(ReadOnlySpan<char> inputSpan)
        {
          while (this.TryFindNextPossibleStartingPosition(inputSpan) && !this.TryMatchAtCurrentPosition(inputSpan) && this.runtextpos != inputSpan.Length)
          {
            this.runtextpos++;
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
          }
        }

        // Token: 0x06003AE0 RID: 15072 RVA: 0x00461264 File Offset: 0x00461264
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 5)
          {
            int num = inputSpan.Slice(runtextpos).IndexOf('{');
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003AE1 RID: 15073 RVA: 0x004612B4 File Offset: 0x004612B4
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 123)
          {
            return false;
          }
          num++;
          readOnlySpan = inputSpan.Slice(num);
          int num3 = num;
          int num4 = readOnlySpan.IndexOf('}');
          if (num4 < 0)
          {
            num4 = readOnlySpan.Length;
          }
          readOnlySpan = readOnlySpan.Slice(num4);
          num += num4;
          int num5 = num;
          while (!readOnlySpan.StartsWith<char>("big="))
          {
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            if (num3 >= num5 || (num5 = inputSpan.Slice(num3, Math.Min(inputSpan.Length, num5 + 3) - num3).LastIndexOf<char>("big=")) < 0)
            {
              return false;
            }
            num5 += num3;
            num = num5;
            readOnlySpan = inputSpan.Slice(num);
          }
          num += 4;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }
      }
    }
  }
}
