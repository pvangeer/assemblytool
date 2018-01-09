using System;
using System.Runtime.CompilerServices;

namespace AssemblyTool.Kernel.ErrorHandling
{
    public class ProbabilityValidationException : Exception
    {
        public ProbabilityValidationException(ErrorCode errorCode, ProbabilityValidationException innerexception) :  base(errorCode.GetMessage(),innerexception)
        {
            Code = errorCode;
        }

        public ProbabilityValidationException(ErrorCode errorCode) : base(errorCode.GetMessage())
        {
            Code = errorCode;
        }

        public ErrorCode Code { get; }
    }
}