using AssemblyTool.Kernel.Services;

namespace AssemblyTool.Kernel.Data
{
    public class FailureMechanismCategoriesOutput
    {
        public FailureMechanismCategoriesOutput(FailureMechanismAssemblyCategory category, double lowerBoundary, double upperBoundary)
        {
            ProbabilityValidator.Validate(lowerBoundary);
            ProbabilityValidator.Validate(upperBoundary);

            Category = category;
            LowerBoundary = lowerBoundary;
            UpperBoundary = upperBoundary;
        }

        public FailureMechanismAssemblyCategory Category {get;}

        public double LowerBoundary { get; }

        public double UpperBoundary { get; }
    }
}
