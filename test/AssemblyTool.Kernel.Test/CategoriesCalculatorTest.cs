// Copyright (C) Stichting Deltares 2018. All rights reserved.
//
// This file is part of AssemblyTool.
//
// AssemblyTool is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.
//
// All names, logos, and references to "Deltares" are registered trademarks of
// Stichting Deltares and remain full property of Stichting Deltares at all times.
// All rights reserved.

using AssemblyTool.Kernel.Data;
using AssemblyTool.Kernel.ErrorHandling;
using NUnit.Framework;

namespace AssemblyTool.Kernel.Test
{
    [TestFixture]
    public class CategoriesCalculatorTest
    {
        #region CalculateAssessmentSectionCategories

        [TestCase(1 / 3000.0, 1 / 1000.0)]
        [TestCase(1 / 1000.0, 1 / 1000.0)]
        [TestCase(1 / 1000.0, 1 / 3000.0)]
        public void CalculateAssessmentSectionCategoriesTest(double signalingStandard,double lowerBoundaryStandard)
        {
            var calculationResult = CategoriesCalculator.CalculateAssessmentSectionCategories(new Probability(signalingStandard), new Probability(lowerBoundaryStandard));

            if (lowerBoundaryStandard < signalingStandard)
            {
                Assert.IsNull(calculationResult.Result);
                Assert.IsEmpty(calculationResult.WarningMessages);
                Assert.IsNotNull(calculationResult.ErrorMessage);

                Assert.AreEqual(ErrorCode.SignallingStandardExceedsLowerBoundary, calculationResult.ErrorMessage.Code);
                return;
            }

            Assert.IsNotNull(calculationResult.Result);
            Assert.IsNull(calculationResult.ErrorMessage);
            Assert.IsNotNull(calculationResult.WarningMessages);
            Assert.IsEmpty(calculationResult.WarningMessages);

            var result = calculationResult.Result;

            Assert.AreEqual(5,result.Length);
            var firstCategory = result[0];
            Assert.AreEqual(AssessmentSectionAssemblyCategory.APlus,firstCategory.Category);
            Assert.AreEqual(0,firstCategory.LowerBoundary,1e-8);
            Assert.AreEqual(1/30.0*signalingStandard,firstCategory.UpperBoundary, 1e-8);
            var category2 = result[1];
            Assert.AreEqual(AssessmentSectionAssemblyCategory.A, category2.Category);
            Assert.AreEqual(1 / 30.0 * signalingStandard, category2.LowerBoundary, 1e-8);
            Assert.AreEqual(signalingStandard, category2.UpperBoundary, 1e-8);
            var category3 = result[2];
            Assert.AreEqual(AssessmentSectionAssemblyCategory.B, category3.Category);
            Assert.AreEqual(signalingStandard, category3.LowerBoundary, 1e-8);
            Assert.AreEqual(lowerBoundaryStandard, category3.UpperBoundary, 1e-8);
            var category4 = result[3];
            Assert.AreEqual(AssessmentSectionAssemblyCategory.C, category4.Category);
            Assert.AreEqual(lowerBoundaryStandard, category4.LowerBoundary, 1e-8);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category4.UpperBoundary, 1e-8);
            var category5 = result[4];
            Assert.AreEqual(AssessmentSectionAssemblyCategory.D, category5.Category);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category5.LowerBoundary, 1e-8);
            Assert.AreEqual(1, category5.UpperBoundary, 1e-8);
        }

        #endregion

        #region CalculateFailureMechanismCategories

        [TestCase(2, 1 / 1000.0, 1 / 1000.0, ErrorCode.InvalidProbabilityDistributionFactor)]
        [TestCase(0.04, 1 / 500.0, 1 / 1000.0, ErrorCode.SignallingStandardExceedsLowerBoundary)]
        public void CalculateFailureMechanismCategoriesCallsValidation(double factor, double signalingStandard,double lowerBoundary, ErrorCode expectedError)
        {
            var calculationResult = CategoriesCalculator.CalculateFailureMechanismCategories(new Probability(signalingStandard), new Probability(lowerBoundary), factor);

            Assert.IsNull(calculationResult.Result);
            Assert.IsNotNull(calculationResult.ErrorMessage);
            Assert.IsNotNull(calculationResult.WarningMessages);
            Assert.IsEmpty(calculationResult.WarningMessages);
            Assert.AreEqual(expectedError,calculationResult.ErrorMessage.Code);
        }

        [TestCase(1 / 3000, 1 / 1000)]
        [TestCase(1 / 1000, 1 / 1000)]
        public void CalculateFailureMechanismCategoriesTest(double signalingStandard, double lowerBoundaryStandard)
        {
            var probabilityDistributionFactor = 0.5;
            var calculationResult = CategoriesCalculator.CalculateFailureMechanismCategories(new Probability(signalingStandard), new Probability(lowerBoundaryStandard),probabilityDistributionFactor);

            Assert.IsNotNull(calculationResult.Result);
            Assert.IsNull(calculationResult.ErrorMessage);
            Assert.IsNotNull(calculationResult.WarningMessages);
            Assert.IsEmpty(calculationResult.WarningMessages);

            var result = calculationResult.Result;

            Assert.AreEqual(6, result.Length);
            var firstCategory = result[0];
            Assert.AreEqual(FailureMechanismAssemblyCategory.It, firstCategory.Category);
            Assert.AreEqual(0, firstCategory.LowerBoundary, 1e-8);
            Assert.AreEqual(1 / 30.0 * probabilityDistributionFactor * signalingStandard, firstCategory.UpperBoundary, 1e-8);
            var category2 = result[1];
            Assert.AreEqual(FailureMechanismAssemblyCategory.IIt, category2.Category);
            Assert.AreEqual(1 / 30.0 * probabilityDistributionFactor * signalingStandard, category2.LowerBoundary, 1e-8);
            Assert.AreEqual(probabilityDistributionFactor * signalingStandard, category2.UpperBoundary, 1e-8);
            var category3 = result[2];
            Assert.AreEqual(FailureMechanismAssemblyCategory.IIIt, category3.Category);
            Assert.AreEqual(probabilityDistributionFactor * signalingStandard, category3.LowerBoundary, 1e-8);
            Assert.AreEqual(probabilityDistributionFactor * lowerBoundaryStandard, category3.UpperBoundary, 1e-8);
            var category4 = result[3];
            Assert.AreEqual(FailureMechanismAssemblyCategory.IVt, category4.Category);
            Assert.AreEqual(probabilityDistributionFactor * lowerBoundaryStandard, category4.LowerBoundary, 1e-8);
            Assert.AreEqual(lowerBoundaryStandard, category4.UpperBoundary, 1e-8);
            var category5 = result[4];
            Assert.AreEqual(FailureMechanismAssemblyCategory.Vt, category5.Category);
            Assert.AreEqual(lowerBoundaryStandard, category5.LowerBoundary, 1e-8);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category5.UpperBoundary, 1e-8);
            var category6 = result[5];
            Assert.AreEqual(FailureMechanismAssemblyCategory.VIt, category6.Category);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category6.LowerBoundary, 1e-8);
            Assert.AreEqual(1, category6.UpperBoundary, 1e-8);
        }

        #endregion

        #region CalculateFailureMechanismSectionCategories

        [TestCase(1 / 3000.0, 1 / 1000.0)]
        [TestCase(1 / 1000.0, 1 / 1000.0)]
        public void CalculateFailureMechanismSectionCategoriesTest(double signalingStandard, double lowerBoundaryStandard)
        {
            var probabilityDistributionFactor = 0.5;
            var nValue = 2.5;
            var calculationResult = CategoriesCalculator.CalculateFailureMechanismSectionCategories((Probability)signalingStandard, (Probability)lowerBoundaryStandard, probabilityDistributionFactor, nValue);

            Assert.IsNotNull(calculationResult.Result);
            Assert.IsNull(calculationResult.ErrorMessage);
            Assert.IsNotNull(calculationResult.WarningMessages);
            Assert.IsEmpty(calculationResult.WarningMessages);

            var result = calculationResult.Result;

            var signalingStandardOnSection = signalingStandard * probabilityDistributionFactor / nValue;
            var lowerBoundaryOnSection = lowerBoundaryStandard * probabilityDistributionFactor / nValue;

            Assert.AreEqual(6, result.Length);
            var firstCategory = result[0];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategory.Iv, firstCategory.Category);
            Assert.AreEqual(0, firstCategory.LowerBoundary, 1e-8);
            Assert.AreEqual(1 / 30.0 * signalingStandardOnSection, firstCategory.UpperBoundary, 1e-8);
            var category2 = result[1];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategory.IIv, category2.Category);
            Assert.AreEqual(1 / 30.0 * signalingStandardOnSection, category2.LowerBoundary, 1e-8);
            Assert.AreEqual(signalingStandardOnSection, category2.UpperBoundary, 1e-8);
            var category3 = result[2];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategory.IIIv, category3.Category);
            Assert.AreEqual(signalingStandardOnSection, category3.LowerBoundary, 1e-8);
            Assert.AreEqual(lowerBoundaryOnSection, category3.UpperBoundary, 1e-8);
            var category4 = result[3];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategory.IVv, category4.Category);
            Assert.AreEqual(lowerBoundaryOnSection, category4.LowerBoundary, 1e-8);
            Assert.AreEqual(lowerBoundaryStandard, category4.UpperBoundary, 1e-8);
            var category5 = result[4];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategory.Vv, category5.Category);
            Assert.AreEqual(lowerBoundaryStandard, category5.LowerBoundary, 1e-8);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category5.UpperBoundary, 1e-8);
            var category6 = result[5];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategory.VIv, category6.Category);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category6.LowerBoundary, 1e-8);
            Assert.AreEqual(1, category6.UpperBoundary, 1e-8);
        }

        [TestCase(1 / 500.0, 1 / 1000.0, 0.5, 2.5, ErrorCode.SignallingStandardExceedsLowerBoundary)]
        [TestCase(1 / 3000.0, 1 / 1000.0, 2, 2.5, ErrorCode.InvalidProbabilityDistributionFactor)]
        [TestCase(1 / 3000.0, 1 / 1000.0, 0.5, -5, ErrorCode.InvalidNValue)]
        public void CalculateFailureMechanismSectionCategoriesCallsValidationTest(double signalingStandard, double lowerBoundaryStandard, double probabilityDistributionFactor, double nValue, ErrorCode expectedError)
        {
            var calculationResult = CategoriesCalculator.CalculateFailureMechanismSectionCategories((Probability)signalingStandard,(Probability)lowerBoundaryStandard, probabilityDistributionFactor, nValue);

            Assert.IsNull(calculationResult.Result);
            Assert.IsNotNull(calculationResult.ErrorMessage);
            Assert.IsNotNull(calculationResult.WarningMessages);
            Assert.IsEmpty(calculationResult.WarningMessages);

            Assert.AreEqual(expectedError,calculationResult.ErrorMessage.Code);
        }
        #endregion

        #region CalculateGeotechnicFailureMechanismSectionCategories

        [Test]
        public void CalculateGeotechnicFailureMechanismSectionCategoriesTest()
        {
            var signalingStandard = new Probability(1 / 3000.0);
            var lowerBoundaryStandard = new Probability(1 / 1000.0);
            var probabilityDistributionFactor = 0.04;
            var nValue = 2.5;
            var calculationResult = CategoriesCalculator.CalculateGeotechnicFailureMechanismSectionCategories(signalingStandard, lowerBoundaryStandard, probabilityDistributionFactor, nValue);

            Assert.IsNotNull(calculationResult.Result);
            Assert.IsNull(calculationResult.ErrorMessage);
            Assert.IsNotNull(calculationResult.WarningMessages);
            Assert.IsEmpty(calculationResult.WarningMessages);

            var result = calculationResult.Result;

            var signalingStandardOnSection = signalingStandard * probabilityDistributionFactor *10 / nValue;
            var lowerBoundaryOnSection = lowerBoundaryStandard * probabilityDistributionFactor *10 / nValue;

            Assert.AreEqual(6, result.Length);
            var firstCategory = result[0];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategory.Iv, firstCategory.Category);
            Assert.AreEqual(0, firstCategory.LowerBoundary, 1e-8);
            Assert.AreEqual(1 / 30.0 * signalingStandardOnSection, firstCategory.UpperBoundary, 1e-8);
            var category2 = result[1];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategory.IIv, category2.Category);
            Assert.AreEqual(1 / 30.0 * signalingStandardOnSection, category2.LowerBoundary, 1e-8);
            Assert.AreEqual(signalingStandardOnSection, category2.UpperBoundary, 1e-8);
            var category3 = result[2];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategory.IIIv, category3.Category);
            Assert.AreEqual(signalingStandardOnSection, category3.LowerBoundary, 1e-8);
            Assert.AreEqual(lowerBoundaryOnSection, category3.UpperBoundary, 1e-8);
            var category4 = result[3];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategory.IVv, category4.Category);
            Assert.AreEqual(lowerBoundaryOnSection, category4.LowerBoundary, 1e-8);
            Assert.AreEqual(lowerBoundaryStandard, category4.UpperBoundary, 1e-8);
            var category5 = result[4];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategory.Vv, category5.Category);
            Assert.AreEqual(lowerBoundaryStandard, category5.LowerBoundary, 1e-8);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category5.UpperBoundary, 1e-8);
            var category6 = result[5];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategory.VIv, category6.Category);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category6.LowerBoundary, 1e-8);
            Assert.AreEqual(1, category6.UpperBoundary, 1e-8);
        }

        [Test]
        public void CalculateGeotechnicFailureMechanismSectionCategoriesWithHighNTest()
        {
            var signalingStandard = new Probability(1 / 3000.0);
            var lowerBoundaryStandard = new Probability(1 / 1000.0);
            var probabilityDistributionFactor = 0.5;
            var nValue = 2.5;
            var calculationResult = CategoriesCalculator.CalculateGeotechnicFailureMechanismSectionCategories(signalingStandard, lowerBoundaryStandard, probabilityDistributionFactor, nValue);

            Assert.IsNotNull(calculationResult.Result);
            Assert.IsNull(calculationResult.ErrorMessage);
            Assert.IsNotNull(calculationResult.WarningMessages);

            Assert.AreEqual(1, calculationResult.WarningMessages.Length);
            Assert.AreEqual(WarningMessage.CorrectedSectionSpecificNValue, calculationResult.WarningMessages[0]);

            var result = calculationResult.Result;
            var signalingStandardOnSection = signalingStandard;
            var lowerBoundaryOnSection = lowerBoundaryStandard;
            Assert.AreEqual(6, result.Length);
            var firstCategory = result[0];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategory.Iv, firstCategory.Category);
            Assert.AreEqual(0, firstCategory.LowerBoundary, 1e-8);
            Assert.AreEqual(1 / 30.0 * signalingStandardOnSection, firstCategory.UpperBoundary, 1e-8);
            var category2 = result[1];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategory.IIv, category2.Category);
            Assert.AreEqual(1 / 30.0 * signalingStandardOnSection, category2.LowerBoundary, 1e-8);
            Assert.AreEqual(signalingStandardOnSection, category2.UpperBoundary, 1e-8);
            var category3 = result[2];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategory.IIIv, category3.Category);
            Assert.AreEqual(signalingStandardOnSection, category3.LowerBoundary, 1e-8);
            Assert.AreEqual(lowerBoundaryOnSection, category3.UpperBoundary, 1e-8);
            var category4 = result[3];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategory.IVv, category4.Category);
            Assert.AreEqual(lowerBoundaryOnSection, category4.LowerBoundary, 1e-8);
            Assert.AreEqual(lowerBoundaryStandard, category4.UpperBoundary, 1e-8);
            var category5 = result[4];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategory.Vv, category5.Category);
            Assert.AreEqual(lowerBoundaryStandard, category5.LowerBoundary, 1e-8);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category5.UpperBoundary, 1e-8);
            var category6 = result[5];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategory.VIv, category6.Category);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category6.LowerBoundary, 1e-8);
            Assert.AreEqual(1, category6.UpperBoundary, 1e-8);
        }

        #endregion

        #region ValidateNValue

        [TestCase(double.NaN, ErrorCode.ValueIsNaN)]
        [TestCase(0, ErrorCode.ValueBelowOne)]
        public void CalculateGeotechnicFailureMechanismSectionCategoriesValidatesNValueNaNTest(double nValue, ErrorCode expectedSubError)
        {
            var signalingStandard = new Probability(1 / 3000.0);
            var lowerBoundaryStandard = new Probability(1 / 1000.0);
            var probabilityDistributionFactor = 0.04;
            var calculationResult = CategoriesCalculator.CalculateGeotechnicFailureMechanismSectionCategories(signalingStandard, lowerBoundaryStandard, probabilityDistributionFactor, nValue);

            Assert.IsNull(calculationResult.Result);
            Assert.IsNotNull(calculationResult.ErrorMessage);
            Assert.IsNotNull(calculationResult.WarningMessages);
            Assert.IsEmpty(calculationResult.WarningMessages);

            Assert.AreEqual(ErrorCode.InvalidNValue, calculationResult.ErrorMessage.Code);
            Assert.IsInstanceOf<AssemblyToolKernelException>(calculationResult.ErrorMessage.InnerException);
            Assert.AreEqual(expectedSubError, ((AssemblyToolKernelException)calculationResult.ErrorMessage.InnerException).Code);
        }

        #endregion

        #region ValidaeProbabilityDistributionFactor

        [TestCase(-1,ErrorCode.ValueBelowZero)]
        [TestCase(2, ErrorCode.ValueAboveOne)]
        [TestCase(double.NaN, ErrorCode.ValueIsNaN)]
        public void ValidateProbabilityDistributionFactorTest(double factor, ErrorCode expectedSubError)
        {
            var signalingStandard = new Probability(1 / 3000.0);
            var lowerBoundaryStandard = new Probability(1 / 1000.0);
            var nValue = 10;
            var calculationResult = CategoriesCalculator.CalculateGeotechnicFailureMechanismSectionCategories(signalingStandard, lowerBoundaryStandard, factor, nValue);

            Assert.IsNull(calculationResult.Result);
            Assert.IsNotNull(calculationResult.ErrorMessage);
            Assert.IsNotNull(calculationResult.WarningMessages);
            Assert.IsEmpty(calculationResult.WarningMessages);

            Assert.AreEqual(ErrorCode.InvalidProbabilityDistributionFactor, calculationResult.ErrorMessage.Code);
            Assert.IsInstanceOf<AssemblyToolKernelException>(calculationResult.ErrorMessage.InnerException);
            Assert.AreEqual(expectedSubError, ((AssemblyToolKernelException)calculationResult.ErrorMessage.InnerException).Code);
        }

        #endregion
    }
}
