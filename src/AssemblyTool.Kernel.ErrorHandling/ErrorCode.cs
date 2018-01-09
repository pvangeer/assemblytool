namespace AssemblyTool.Kernel.ErrorHandling
{
    public enum ErrorCode
    {
        ProbabilityBelowZero,
        ProbabilityAboveOne,
        SignallingStandardExceedsLowerBoundary,
        IncorrectSignalingStandard,
        IncorrectLowerBoundaryStandard
    }
}