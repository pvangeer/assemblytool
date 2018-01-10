﻿using AssemblyTool.Kernel.Services;

namespace AssemblyTool.Kernel.Data
{
    public class AssessmentSectionCategoriesOutput
    {
        public AssessmentSectionCategoriesOutput(AssessmentSectionAssemblyCategory category, double lowerBoundary, double upperBoundary)
        {
            ProbabilityValidator.Validate(lowerBoundary);
            ProbabilityValidator.Validate(upperBoundary);

            Category = category;
            LowerBoundary = lowerBoundary;
            UpperBoundary = upperBoundary;
        }

        public AssessmentSectionAssemblyCategory Category {get;}

        public double LowerBoundary { get; }

        public double UpperBoundary { get; }
    }
}
