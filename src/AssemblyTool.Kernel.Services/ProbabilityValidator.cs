using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyTool.Kernel.ErrorHandling;

namespace AssemblyTool.Kernel.Services
{
    public static class ProbabilityValidator
    {
        public static void Validate(double probability)
        {
            if (probability < 0)
            {
                throw new ProbabilityValidationException(ErrorCode.ProbabilityBelowZero);
            }

            if (probability > 1)
            {
                throw new ProbabilityValidationException(ErrorCode.ProbabilityAboveOne);
            }
        }
    }
}
