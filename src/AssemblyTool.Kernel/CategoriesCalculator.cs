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
                new AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory.APlus,0,aPlusToA),
                new AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory.A,aPlusToA, signalingStandard),
                new AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory.B,signalingStandard, lowerBoundaryStandard),
                new AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory.C,lowerBoundaryStandard, cToD),
                new AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory.D, cToD, 1), 
            };
        }

        /// <summary>
        /// WBI-1-1
        /// </summary>
        /// <returns></returns>
        public static FailureMechanismCategoriesOutput[] CalculateFailureMechanismCategories(double signalingStandard, double lowerBoundaryStandard, double probabilityDistributionFactor)
        {
            ValidateStandards(signalingStandard, lowerBoundaryStandard);

            var iToII = 1 / 30.0 * probabilityDistributionFactor * signalingStandard;
            var iItoIII = probabilityDistributionFactor * signalingStandard;

            return new[]
            {
                new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.It,0,iToII),
                new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.IIt,iToII, iItoIII),
                new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.IIIt,iItoIII, signalingStandard),
                new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.IVt,probabilityDistributionFactor * lowerBoundaryStandard, lowerBoundaryStandard),
                new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.Vt, lowerBoundaryStandard, 30 * lowerBoundaryStandard),
                new FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory.VIt, 30 * lowerBoundaryStandard, 1),
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
