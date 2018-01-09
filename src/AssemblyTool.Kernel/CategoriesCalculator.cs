using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AssemblyTool.Kernel.Data;

namespace AssemblyTool.Kernel
{
    public class CategoriesCalculator
    {
        /// <summary>
        /// WBI-2-1 
        /// </summary>
        /// <returns></returns>
        public AssessmentSectionCategoriesOutput[] CalculateAssessmentSectionCategories(double signalingStandard, double lowerBoundaryStandard)
        {
            //ProbabilityValidator.Validate(lowerBoundary);
            //ProbabilityValidator.Validate(upperBoundary);

            return new[]
            {
                new AssessmentSectionCategoriesOutput(AssemblyCategory.APlus,0,1/30.0*signalingStandard),
                new AssessmentSectionCategoriesOutput(AssemblyCategory.A,1/30.0*signalingStandard, signalingStandard),
                new AssessmentSectionCategoriesOutput(AssemblyCategory.B,signalingStandard, lowerBoundaryStandard),
                new AssessmentSectionCategoriesOutput(AssemblyCategory.C,lowerBoundaryStandard, 30 * lowerBoundaryStandard),
                new AssessmentSectionCategoriesOutput(AssemblyCategory.D, 30 * lowerBoundaryStandard, 1), 
            };
        }
    }
}
