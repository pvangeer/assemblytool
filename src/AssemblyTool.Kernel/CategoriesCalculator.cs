using AssemblyTool.Kernel.Data;
using AssemblyTool.Kernel.ErrorHandling;
using AssemblyTool.Kernel.Services;

namespace AssemblyTool.Kernel
{
    public static class CategoriesCalculator
    {
        /// <summary>
        /// WBI-2-1
        /// </summary>
        /// <returns></returns>
        public static AssessmentSectionCategoriesOutput[] CalculateAssessmentSectionCategories(double signalingStandard, double lowerBoundaryStandard)
        {
            ValidateStandards(signalingStandard, lowerBoundaryStandard);

            var aPlusToA = 1/30.0*signalingStandard;
            var cToD = 30 * lowerBoundaryStandard;

            return new[]
            {
                new AssessmentSectionCategoriesOutput(AssemblyCategory.APlus,0,aPlusToA),
                new AssessmentSectionCategoriesOutput(AssemblyCategory.A,aPlusToA, signalingStandard),
                new AssessmentSectionCategoriesOutput(AssemblyCategory.B,signalingStandard, lowerBoundaryStandard),
                new AssessmentSectionCategoriesOutput(AssemblyCategory.C,lowerBoundaryStandard, cToD),
                new AssessmentSectionCategoriesOutput(AssemblyCategory.D, cToD, 1), 
            };
        }

        private static void ValidateStandards(double signalingStandard, double lowerBoundaryStandard)
        {
            try
            {
                ProbabilityValidator.Validate(signalingStandard);
            }
            catch (ProbabilityValidationException e)
            {
                throw new ProbabilityValidationException(ErrorCode.IncorrectSignalingStandard, e);
            }

            try
            {
                ProbabilityValidator.Validate(lowerBoundaryStandard);
            }
            catch (ProbabilityValidationException e)
            {
                throw new ProbabilityValidationException(ErrorCode.IncorrectLowerBoundaryStandard, e);
            }

            if (signalingStandard > lowerBoundaryStandard)
            {
                throw new ProbabilityValidationException(ErrorCode.SignallingStandardExceedsLowerBoundary);
            }
        }
    }
}
