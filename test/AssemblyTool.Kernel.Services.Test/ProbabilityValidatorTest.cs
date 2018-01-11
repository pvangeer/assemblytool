using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AssemblyTool.Kernel.ErrorHandling;
using NUnit.Framework;

namespace AssemblyTool.Kernel.Services.Test
{
    [TestFixture]
    public class ProbabilityValidatorTest
    {
        [TestCase(-1,ErrorCode.ValueBelowZero)]
        [TestCase(2, ErrorCode.ValueAboveOne)]
        [TestCase(double.NaN, ErrorCode.ValueIsNaN)]
        public void ValidateProbabilityTest(double probability, ErrorCode expectedErrorCode)
        {
            try
            {
                ProbabilityValidator.Validate(probability);
                Assert.Fail(string.Format("An exception with error {0} was expected.",expectedErrorCode));
            }
            catch (AssemblyToolKernelException e)
            {
                Assert.AreEqual(expectedErrorCode,e.Code);
            }
        }

        [TestCase(-1, 1/1000, ErrorCode.InvalidSignalingStandard)]
        [TestCase(1/3000, 4, ErrorCode.InvalidLowerBoundaryStandard)]
        [TestCase(0.8, 0.2, ErrorCode.SignallingStandardExceedsLowerBoundary)]
        public void ValidateStandardsTest(double signalingStandard, double lowerBoundaryStandard, ErrorCode expectedErrorCode)
        {
            try
            {
                ProbabilityValidator.ValidateStandards(signalingStandard,lowerBoundaryStandard);
                Assert.Fail(string.Format("An exception with error {0} was expected.", expectedErrorCode));
            }
            catch (AssemblyToolKernelException e)
            {
                Assert.AreEqual(expectedErrorCode, e.Code);
            }
        }
        
    }
}
