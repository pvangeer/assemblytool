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

using AssemblyTool.Kernel.Categories;
using AssemblyTool.Kernel.Categories.CalculatorInput;
using AssemblyTool.Kernel.Data;
using AssemblyTool.Kernel.Data.AssemblyCategories;
using AssemblyTool.Kernel.ErrorHandling;
using NUnit.Framework;

namespace AssemblyTool.Kernel.Test.Categories
{
    [TestFixture]
    public class CategoriesCalculatorTest
    {
        #region CalculateAssessmentSectionCategories

        [TestCase(1 / 3000.0, 1 / 1000.0)]
        [TestCase(1 / 1000.0, 1 / 1000.0)]
        public void CalculateAssessmentSectionCategoriesTest(double signalingStandard,double lowerBoundaryStandard)
        {
            var input = new CalculateAssessmentSectionCategoriesInput((Probability) signalingStandard, (Probability) lowerBoundaryStandard);
            var calculationResult = CategoriesCalculator.CalculateAssessmentSectionCategories(input);

            Assert.IsNotNull(calculationResult.Result);
            Assert.IsNull(calculationResult.ErrorMessage);
            Assert.IsNotNull(calculationResult.WarningMessages);
            Assert.IsEmpty(calculationResult.WarningMessages);

            var result = calculationResult.Result;

            Assert.AreEqual(5,result.Length);
            var firstCategory = result[0];
            Assert.AreEqual(AssessmentSectionAssemblyCategoryGroup.APlus,firstCategory.CategoryGroup);
            Assert.AreEqual(0,firstCategory.LowerBoundary,1e-8);
            Assert.AreEqual(1/30.0*signalingStandard,firstCategory.UpperBoundary, 1e-8);
            var category2 = result[1];
            Assert.AreEqual(AssessmentSectionAssemblyCategoryGroup.A, category2.CategoryGroup);
            Assert.AreEqual(1 / 30.0 * signalingStandard, category2.LowerBoundary, 1e-8);
            Assert.AreEqual(signalingStandard, category2.UpperBoundary, 1e-8);
            var category3 = result[2];
            Assert.AreEqual(AssessmentSectionAssemblyCategoryGroup.B, category3.CategoryGroup);
            Assert.AreEqual(signalingStandard, category3.LowerBoundary, 1e-8);
            Assert.AreEqual(lowerBoundaryStandard, category3.UpperBoundary, 1e-8);
            var category4 = result[3];
            Assert.AreEqual(AssessmentSectionAssemblyCategoryGroup.C, category4.CategoryGroup);
            Assert.AreEqual(lowerBoundaryStandard, category4.LowerBoundary, 1e-8);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category4.UpperBoundary, 1e-8);
            var category5 = result[4];
            Assert.AreEqual(AssessmentSectionAssemblyCategoryGroup.D, category5.CategoryGroup);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category5.LowerBoundary, 1e-8);
            Assert.AreEqual(1, category5.UpperBoundary, 1e-8);
        }

        #endregion

        #region CalculateFailureMechanismCategories

        [TestCase(1 / 3000, 1 / 1000)]
        [TestCase(1 / 1000, 1 / 1000)]
        public void CalculateFailureMechanismCategoriesTest(double signalingStandard, double lowerBoundaryStandard)
        {
            var probabilityDistributionFactor = 0.5;
            var input = new CalculateFailureMechanismCategoriesInput(new Probability(signalingStandard), new Probability(lowerBoundaryStandard), probabilityDistributionFactor);
            var calculationResult = CategoriesCalculator.CalculateFailureMechanismCategories(input);

            Assert.IsNotNull(calculationResult.Result);
            Assert.IsNull(calculationResult.ErrorMessage);
            Assert.IsNotNull(calculationResult.WarningMessages);
            Assert.IsEmpty(calculationResult.WarningMessages);

            var result = calculationResult.Result;

            Assert.AreEqual(6, result.Length);
            var firstCategory = result[0];
            Assert.AreEqual(FailureMechanismAssemblyCategoryGroup.It, firstCategory.CategoryGroup);
            Assert.AreEqual(0, firstCategory.LowerBoundary, 1e-8);
            Assert.AreEqual(1 / 30.0 * probabilityDistributionFactor * signalingStandard, firstCategory.UpperBoundary, 1e-8);
            var category2 = result[1];
            Assert.AreEqual(FailureMechanismAssemblyCategoryGroup.IIt, category2.CategoryGroup);
            Assert.AreEqual(1 / 30.0 * probabilityDistributionFactor * signalingStandard, category2.LowerBoundary, 1e-8);
            Assert.AreEqual(probabilityDistributionFactor * signalingStandard, category2.UpperBoundary, 1e-8);
            var category3 = result[2];
            Assert.AreEqual(FailureMechanismAssemblyCategoryGroup.IIIt, category3.CategoryGroup);
            Assert.AreEqual(probabilityDistributionFactor * signalingStandard, category3.LowerBoundary, 1e-8);
            Assert.AreEqual(probabilityDistributionFactor * lowerBoundaryStandard, category3.UpperBoundary, 1e-8);
            var category4 = result[3];
            Assert.AreEqual(FailureMechanismAssemblyCategoryGroup.IVt, category4.CategoryGroup);
            Assert.AreEqual(probabilityDistributionFactor * lowerBoundaryStandard, category4.LowerBoundary, 1e-8);
            Assert.AreEqual(lowerBoundaryStandard, category4.UpperBoundary, 1e-8);
            var category5 = result[4];
            Assert.AreEqual(FailureMechanismAssemblyCategoryGroup.Vt, category5.CategoryGroup);
            Assert.AreEqual(lowerBoundaryStandard, category5.LowerBoundary, 1e-8);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category5.UpperBoundary, 1e-8);
            var category6 = result[5];
            Assert.AreEqual(FailureMechanismAssemblyCategoryGroup.VIt, category6.CategoryGroup);
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
            var input = new CalculateFailureMechanismSectionCategoriesInput((Probability)signalingStandard, (Probability)lowerBoundaryStandard, probabilityDistributionFactor, nValue);
            var calculationResult = CategoriesCalculator.CalculateFailureMechanismSectionCategories(input);

            Assert.IsNotNull(calculationResult.Result);
            Assert.IsNull(calculationResult.ErrorMessage);
            Assert.IsNotNull(calculationResult.WarningMessages);
            Assert.IsEmpty(calculationResult.WarningMessages);

            var result = calculationResult.Result;

            var signalingStandardOnSection = signalingStandard * probabilityDistributionFactor / nValue;
            var lowerBoundaryOnSection = lowerBoundaryStandard * probabilityDistributionFactor / nValue;

            Assert.AreEqual(6, result.Length);
            var firstCategory = result[0];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategoryGroup.Iv, firstCategory.CategoryGroup);
            Assert.AreEqual(0, firstCategory.LowerBoundary, 1e-8);
            Assert.AreEqual(1 / 30.0 * signalingStandardOnSection, firstCategory.UpperBoundary, 1e-8);
            var category2 = result[1];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategoryGroup.IIv, category2.CategoryGroup);
            Assert.AreEqual(1 / 30.0 * signalingStandardOnSection, category2.LowerBoundary, 1e-8);
            Assert.AreEqual(signalingStandardOnSection, category2.UpperBoundary, 1e-8);
            var category3 = result[2];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategoryGroup.IIIv, category3.CategoryGroup);
            Assert.AreEqual(signalingStandardOnSection, category3.LowerBoundary, 1e-8);
            Assert.AreEqual(lowerBoundaryOnSection, category3.UpperBoundary, 1e-8);
            var category4 = result[3];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategoryGroup.IVv, category4.CategoryGroup);
            Assert.AreEqual(lowerBoundaryOnSection, category4.LowerBoundary, 1e-8);
            Assert.AreEqual(lowerBoundaryStandard, category4.UpperBoundary, 1e-8);
            var category5 = result[4];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategoryGroup.Vv, category5.CategoryGroup);
            Assert.AreEqual(lowerBoundaryStandard, category5.LowerBoundary, 1e-8);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category5.UpperBoundary, 1e-8);
            var category6 = result[5];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategoryGroup.VIv, category6.CategoryGroup);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category6.LowerBoundary, 1e-8);
            Assert.AreEqual(1, category6.UpperBoundary, 1e-8);
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
            var input = new CalculateFailureMechanismSectionCategoriesInput(signalingStandard,lowerBoundaryStandard,probabilityDistributionFactor,nValue);
            var calculationResult = CategoriesCalculator.CalculateGeotechnicFailureMechanismSectionCategories(input);

            Assert.IsNotNull(calculationResult.Result);
            Assert.IsNull(calculationResult.ErrorMessage);
            Assert.IsNotNull(calculationResult.WarningMessages);
            Assert.IsEmpty(calculationResult.WarningMessages);

            var result = calculationResult.Result;

            var signalingStandardOnSection = signalingStandard * probabilityDistributionFactor *10 / nValue;
            var lowerBoundaryOnSection = lowerBoundaryStandard * probabilityDistributionFactor *10 / nValue;

            Assert.AreEqual(6, result.Length);
            var firstCategory = result[0];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategoryGroup.Iv, firstCategory.CategoryGroup);
            Assert.AreEqual(0, firstCategory.LowerBoundary, 1e-8);
            Assert.AreEqual(1 / 30.0 * signalingStandardOnSection, firstCategory.UpperBoundary, 1e-8);
            var category2 = result[1];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategoryGroup.IIv, category2.CategoryGroup);
            Assert.AreEqual(1 / 30.0 * signalingStandardOnSection, category2.LowerBoundary, 1e-8);
            Assert.AreEqual(signalingStandardOnSection, category2.UpperBoundary, 1e-8);
            var category3 = result[2];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategoryGroup.IIIv, category3.CategoryGroup);
            Assert.AreEqual(signalingStandardOnSection, category3.LowerBoundary, 1e-8);
            Assert.AreEqual(lowerBoundaryOnSection, category3.UpperBoundary, 1e-8);
            var category4 = result[3];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategoryGroup.IVv, category4.CategoryGroup);
            Assert.AreEqual(lowerBoundaryOnSection, category4.LowerBoundary, 1e-8);
            Assert.AreEqual(lowerBoundaryStandard, category4.UpperBoundary, 1e-8);
            var category5 = result[4];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategoryGroup.Vv, category5.CategoryGroup);
            Assert.AreEqual(lowerBoundaryStandard, category5.LowerBoundary, 1e-8);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category5.UpperBoundary, 1e-8);
            var category6 = result[5];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategoryGroup.VIv, category6.CategoryGroup);
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
            var input = new CalculateFailureMechanismSectionCategoriesInput(signalingStandard, lowerBoundaryStandard, probabilityDistributionFactor, nValue);
            var calculationResult = CategoriesCalculator.CalculateGeotechnicFailureMechanismSectionCategories(input);

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
            Assert.AreEqual(FailureMechanismSectionAssemblyCategoryGroup.Iv, firstCategory.CategoryGroup);
            Assert.AreEqual(0, firstCategory.LowerBoundary, 1e-8);
            Assert.AreEqual(1 / 30.0 * signalingStandardOnSection, firstCategory.UpperBoundary, 1e-8);
            var category2 = result[1];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategoryGroup.IIv, category2.CategoryGroup);
            Assert.AreEqual(1 / 30.0 * signalingStandardOnSection, category2.LowerBoundary, 1e-8);
            Assert.AreEqual(signalingStandardOnSection, category2.UpperBoundary, 1e-8);
            var category3 = result[2];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategoryGroup.IIIv, category3.CategoryGroup);
            Assert.AreEqual(signalingStandardOnSection, category3.LowerBoundary, 1e-8);
            Assert.AreEqual(lowerBoundaryOnSection, category3.UpperBoundary, 1e-8);
            var category4 = result[3];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategoryGroup.IVv, category4.CategoryGroup);
            Assert.AreEqual(lowerBoundaryOnSection, category4.LowerBoundary, 1e-8);
            Assert.AreEqual(lowerBoundaryStandard, category4.UpperBoundary, 1e-8);
            var category5 = result[4];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategoryGroup.Vv, category5.CategoryGroup);
            Assert.AreEqual(lowerBoundaryStandard, category5.LowerBoundary, 1e-8);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category5.UpperBoundary, 1e-8);
            var category6 = result[5];
            Assert.AreEqual(FailureMechanismSectionAssemblyCategoryGroup.VIv, category6.CategoryGroup);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category6.LowerBoundary, 1e-8);
            Assert.AreEqual(1, category6.UpperBoundary, 1e-8);
        }

        #endregion
    }
}
