using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200002F RID: 47
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__NowMarkRegex_45 : Regex
  {
    // Token: 0x0600005B RID: 91 RVA: 0x00332384 File Offset: 0x00332384
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__NowMarkRegex_45()
    {
      this.pattern = "\\s*\\{now\\}";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__NowMarkRegex_45.RunnerFactory();
      this.capsize = 1;
    }

    // Token: 0x0400002E RID: 46
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__NowMarkRegex_45 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__NowMarkRegex_45();

    // Token: 0x02000262 RID: 610
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x0600214E RID: 8526 RVA: 0x003F6F70 File Offset: 0x003F6F70
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__NowMarkRegex_45.RunnerFactory.Runner();
      }

      // Token: 0x020006CC RID: 1740
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003ADB RID: 15067 RVA: 0x004610EC File Offset: 0x004610EC
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

        // Token: 0x06003ADC RID: 15068 RVA: 0x0046112C File Offset: 0x0046112C
        private unsafe bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 5)
          {
            ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(runtextpos);
            int num = readOnlySpan.IndexOf<char>("{now}");
            if (num >= 0)
            {
              int num2 = num - 1;
              while (num2 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num2])))
              {
                num2--;
              }
              this.runtextpos = runtextpos + num2 + 1;
              this.runtrackpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003ADD RID: 15069 RVA: 0x004611B4 File Offset: 0x004611B4
        private bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          inputSpan.Slice(num);
          num = this.runtrackpos;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (this.runtextpos < num)
          {
            this.runtextpos = num;
          }
          if (!readOnlySpan.StartsWith<char>("{now}"))
          {
            return false;
          }
          num += 5;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }
      }
    }
  }
}
