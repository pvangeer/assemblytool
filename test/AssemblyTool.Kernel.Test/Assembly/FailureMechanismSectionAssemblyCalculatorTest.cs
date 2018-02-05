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

using System;
using AssemblyTool.Kernel.Assembly;
using AssemblyTool.Kernel.Data;
using AssemblyTool.Kernel.Data.AssemblyCategories;
using AssemblyTool.Kernel.Data.CalculationResults;
using NUnit.Framework;

namespace AssemblyTool.Kernel.Test.Assembly
{
    [TestFixture]
    public class FailureMechanismSectionAssemblyCalculatorTest
    {
        [Test]
        [TestCase(SimpleCalculationResult.NVT, FailureMechanismSectionCategoryGroup.None)]
        [TestCase(SimpleCalculationResult.FV, FailureMechanismSectionCategoryGroup.Iv)]
        [TestCase(SimpleCalculationResult.VB, FailureMechanismSectionCategoryGroup.VIIv)]
        public void SimpleAssessmentDirectFailureMechanismsReturnsCorrectCategory(
            SimpleCalculationResult calculationResult, object expectedResult)
        {
            var calculationOutput = new FailureMechanismSectionAssemblyCalculator().SimpleAssessmentDirectFailureMechanisms(calculationResult);

            Assert.IsNotNull(calculationOutput);
            Assert.IsEmpty(calculationOutput.WarningMessages);
            Assert.AreEqual(expectedResult, calculationOutput.Result);
        }

        [Test]
        public void SimpleAssessmentIndirectFailureMechanismsThrowsNotImplementedException()
        {
            try
            {
                new FailureMechanismSectionAssemblyCalculator().SimpleAssessmentIndirectFailureMechanisms(SimpleCalculationResult.FV);
                Assert.Fail("Not implemented exception expected.");
            }
            catch (NotImplementedException e)
            {
                // ok
            }
        }

        [Test]
        [TestCase(DetailedCalculationResult.V, FailureMechanismSectionCategoryGroup.IIv)]
        [TestCase(DetailedCalculationResult.VN, FailureMechanismSectionCategoryGroup.Vv)]
        [TestCase(DetailedCalculationResult.NGO, FailureMechanismSectionCategoryGroup.VIIv)]
        public void DetailedAssessmentDirectFailureMechanismsFromResultTranslatesResultCorrectly(DetailedCalculationResult result, FailureMechanismSectionCategoryGroup expectedCategoryGroup)
        {
            var calculationOutput = new FailureMechanismSectionAssemblyCalculator().DetailedAssessmentDirectFailureMechanismsFromResult(result);

            Assert.IsNotNull(calculationOutput);
            Assert.IsEmpty(calculationOutput.WarningMessages);
            Assert.AreEqual(expectedCategoryGroup,calculationOutput.Result);
        }

        [Test]
        public void DetailedAssessmentIndirectFailureMechanismsFromResultThrowsNotImplementedException()
        {
            try
            {
                new FailureMechanismSectionAssemblyCalculator().DetailedAssessmentIndirectFailureMechanismsFromResult(DetailedCalculationResult.V);
                Assert.Fail("Not implemented exception expected.");
            }
            catch (NotImplementedException e)
            {
                // ok
            }
        }

        [Test]
        [TestCase(0,FailureMechanismSectionCategoryGroup.Iv)]
        [TestCase(0.1, FailureMechanismSectionCategoryGroup.Iv)]
        [TestCase(0.4, FailureMechanismSectionCategoryGroup.IVv)]
        [TestCase(0.9, FailureMechanismSectionCategoryGroup.VIv)]
        [TestCase(1, FailureMechanismSectionCategoryGroup.VIv)]
        public void DetailedAssessmentDirectFailureMechanismsFromProbabilitySelectsCorrectCategory(double probability, FailureMechanismSectionCategoryGroup expectedCategoryGroup)
        {
            var categories = new[]
            {
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.Iv,(Probability)0,(Probability)0.1),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.IIv,(Probability)0.1,(Probability)0.2),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.IIIv,(Probability)0.2,(Probability)0.3),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.IVv,(Probability)0.3,(Probability)0.4),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.Vv,(Probability)0.4,(Probability)0.5),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.VIv,(Probability)0.5,(Probability)1),
            };

            var calculationOutput = new FailureMechanismSectionAssemblyCalculator().DetailedAssessmentDirectFailureMechanismsFromProbability((Probability) probability, categories);

            Assert.IsNotNull(calculationOutput);
            Assert.IsEmpty(calculationOutput.WarningMessages);
            Assert.AreEqual(expectedCategoryGroup,calculationOutput.Result);
        }

        // TODO: Test all paths in DetailedAssessmentDirectFailureMechanismsFromCategoryBoundaries

        [Test]
        [TestCase(TailorMadeCalculationResult.FV, FailureMechanismSectionCategoryGroup.Iv)]
        [TestCase(TailorMadeCalculationResult.V, FailureMechanismSectionCategoryGroup.IIv)]
        [TestCase(TailorMadeCalculationResult.VN, FailureMechanismSectionCategoryGroup.Vv)]
        [TestCase(TailorMadeCalculationResult.NGO, FailureMechanismSectionCategoryGroup.VIIv)]
        public void TailorMadeAssessmentDirectFailureMechanismsFromResultTranslatesResultCorrectly(TailorMadeCalculationResult result, FailureMechanismSectionCategoryGroup expectedCategoryGroup)
        {
            var calculationOutput = new FailureMechanismSectionAssemblyCalculator().TailorMadeAssessmentDirectFailureMechanismsFromResult(result);

            Assert.IsNotNull(calculationOutput);
            Assert.IsEmpty(calculationOutput.WarningMessages);
            Assert.AreEqual(expectedCategoryGroup, calculationOutput.Result);
        }

        [Test]
        public void TailorMadeAssessmentIndirectFailureMechanismsFromResultThrowsNotImplementedException()
        {
            try
            {
                new FailureMechanismSectionAssemblyCalculator().TailorMadeAssessmentIndirectFailureMechanismsFromResult(TailorMadeCalculationResult.FV);
                Assert.Fail("Not implemented exception expected.");
            }
            catch (NotImplementedException e)
            {
                // ok
            }
        }

        [Test]
        [TestCase(TailorMadeProbabilityCalculationResultGroup.FV, FailureMechanismSectionCategoryGroup.Iv)]
        [TestCase(TailorMadeProbabilityCalculationResultGroup.NGO, FailureMechanismSectionCategoryGroup.VIIv)]
        public void TailorMadeAssessmentDirectFailureMechanismsFromProbabilityReturnsCorrectCategoryNoProbability(TailorMadeProbabilityCalculationResultGroup resultGroup, FailureMechanismSectionCategoryGroup expectedCategoryGroup)
        {
            var input = new TailorMadeProbabilityCalculationResult(resultGroup);
            var calculationOutput = new FailureMechanismSectionAssemblyCalculator().TailorMadeAssessmentDirectFailureMechanismsFromProbability(input, null);

            Assert.IsNotNull(calculationOutput);
            Assert.IsEmpty(calculationOutput.WarningMessages);
            Assert.AreEqual(expectedCategoryGroup, calculationOutput.Result);
        }

        [Test]
        public void TailorMadeAssessmentDirectFailureMechanismsFromProbabilityReturnsCorrectCategoryFromProbability()
        {
            var categories = new[]
            {
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.Iv,(Probability)0,(Probability)0.1),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.IIv,(Probability)0.1,(Probability)0.2),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.IIIv,(Probability)0.2,(Probability)0.3),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.IVv,(Probability)0.3,(Probability)0.4),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.Vv,(Probability)0.4,(Probability)0.5),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.VIv,(Probability)0.5,(Probability)1),
            };

            var input = new TailorMadeProbabilityCalculationResult((Probability)0.35);
            var calculationOutput = new FailureMechanismSectionAssemblyCalculator().TailorMadeAssessmentDirectFailureMechanismsFromProbability(input, categories);

            Assert.IsNotNull(calculationOutput);
            Assert.IsEmpty(calculationOutput.WarningMessages);
            Assert.AreEqual(FailureMechanismSectionCategoryGroup.IVv, calculationOutput.Result);
        }

        [Test]
        [TestCase(TailorMadeCategoryCalculationResult.NGO, FailureMechanismSectionCategoryGroup.VIIv)]
        [TestCase(TailorMadeCategoryCalculationResult.Iv, FailureMechanismSectionCategoryGroup.Iv)]
        [TestCase(TailorMadeCategoryCalculationResult.IIv, FailureMechanismSectionCategoryGroup.IIv)]
        [TestCase(TailorMadeCategoryCalculationResult.IIIv, FailureMechanismSectionCategoryGroup.IIIv)]
        [TestCase(TailorMadeCategoryCalculationResult.IVv, FailureMechanismSectionCategoryGroup.IVv)]
        [TestCase(TailorMadeCategoryCalculationResult.Vv, FailureMechanismSectionCategoryGroup.Vv)]
        [TestCase(TailorMadeCategoryCalculationResult.VIv, FailureMechanismSectionCategoryGroup.VIv)]
        [TestCase(TailorMadeCategoryCalculationResult.VIIv, FailureMechanismSectionCategoryGroup.VIIv)]
        [TestCase(TailorMadeCategoryCalculationResult.FV, FailureMechanismSectionCategoryGroup.Iv)]
        public void TailorMadeAssessmentDirectFailureMechanismsFromCategoryResultReturnsCorrectCategory(TailorMadeCategoryCalculationResult result, FailureMechanismSectionCategoryGroup expectedCategoryGroup)
        {
            var calculationOutput = new FailureMechanismSectionAssemblyCalculator().TailorMadeAssessmentDirectFailureMechanismsFromCategoryResult(result);

            Assert.IsNotNull(calculationOutput);
            Assert.IsEmpty(calculationOutput.WarningMessages);
            Assert.AreEqual(expectedCategoryGroup, calculationOutput.Result);
        }

        [Test]
        [TestCase(FailureMechanismSectionCategoryGroup.IIv,FailureMechanismSectionCategoryGroup.IVv,FailureMechanismSectionCategoryGroup.Iv,FailureMechanismSectionCategoryGroup.Iv)]
        [TestCase(FailureMechanismSectionCategoryGroup.IIv, FailureMechanismSectionCategoryGroup.IVv, FailureMechanismSectionCategoryGroup.VIIv, FailureMechanismSectionCategoryGroup.IVv)]
        [TestCase(FailureMechanismSectionCategoryGroup.IIv, FailureMechanismSectionCategoryGroup.VIIv, FailureMechanismSectionCategoryGroup.VIIv, FailureMechanismSectionCategoryGroup.IIv)]
        [TestCase(FailureMechanismSectionCategoryGroup.VIIv, FailureMechanismSectionCategoryGroup.VIIv, FailureMechanismSectionCategoryGroup.VIIv, FailureMechanismSectionCategoryGroup.VIIv)]
        public void CombinedAssessmentFromFailureMechanismSectionResultsSelectsCorrectResult(
            FailureMechanismSectionCategoryGroup simpleAssessmentResult,
            FailureMechanismSectionCategoryGroup detailedAssessmentResult,
            FailureMechanismSectionCategoryGroup tailorMadeAssessmentResult,
            FailureMechanismSectionCategoryGroup expectedCategoryGroup)
        {
            var calculationOutput = new FailureMechanismSectionAssemblyCalculator().CombinedAssessmentFromFailureMechanismSectionResults(simpleAssessmentResult,detailedAssessmentResult,tailorMadeAssessmentResult);

            Assert.IsNotNull(calculationOutput);
            Assert.IsEmpty(calculationOutput.WarningMessages);
            Assert.AreEqual(expectedCategoryGroup, calculationOutput.Result);
        }
    }
}
