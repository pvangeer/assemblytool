namespace AssemblyTool.Kernel.Data
{
    public class AssessmentSectionCategoriesOutput
    {
        public AssessmentSectionCategoriesOutput(AssemblyCategory category, double lowerBoundary, double upperBoundary)
        {
            //ProbabilityValidator.Validate(lowerBoundary);
            //ProbabilityValidator.Validate(upperBoundary);

            Category = category;
            LowerBoundary = lowerBoundary;
            UpperBoundary = upperBoundary;
        }

        public AssemblyCategory Category {get;}

        public double LowerBoundary { get; }

        public double UpperBoundary { get; }
    }
}
