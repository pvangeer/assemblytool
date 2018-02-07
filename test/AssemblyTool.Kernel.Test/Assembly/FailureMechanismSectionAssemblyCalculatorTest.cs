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
using System.ComponentModel;
using AssemblyTool.Kernel.Assembly;
using AssemblyTool.Kernel.Assembly.CalculatorInput;
using AssemblyTool.Kernel.Data;
using AssemblyTool.Kernel.Data.AssemblyCategories;
using AssemblyTool.Kernel.Data.CalculationResults;
using AssemblyTool.Kernel.ErrorHandling;
using NUnit.Framework;

namespace AssemblyTool.Kernel.Test.Assembly
{
    [TestFixture]
    public class FailureMechanismSectionAssemblyCalculatorTest
    {
        #region SimpleAssessment

        [Test]
        [TestCase(SimpleCalculationResult.NVT, FailureMechanismSectionCategoryGroup.NotApplicable, 0.0)]
        [TestCase(SimpleCalculationResult.FV, FailureMechanismSectionCategoryGroup.Iv, 0.0)]
        [TestCase(SimpleCalculationResult.VB, FailureMechanismSectionCategoryGroup.VIIv, double.NaN)]
        [TestCase(SimpleCalculationResult.None, FailureMechanismSectionCategoryGroup.None, double.NaN)]
        public void SimpleAssessmentDirectFailureMechanismsReturnsCorrectCategory(SimpleCalculationResult calculationResult, FailureMechanismSectionCategoryGroup expectedResult, double expectedProbability)
        {
            var calculationOutput = new FailureMechanismSectionAssemblyCalculator().SimpleAssessmentDirectFailureMechanisms(calculationResult);

            Assert.IsNotNull(calculationOutput);
            Assert.IsEmpty(calculationOutput.WarningMessages);
            Assert.IsNotNull(calculationOutput.Result);
            Assert.AreEqual(expectedResult, calculationOutput.Result.CategoryGroup);
            Assert.AreEqual(expectedProbability, calculationOutput.Result.EstimatedProbabilityOfFailure);
        }

        [Test]
        public void SimpleAssessmentDirectFailureMechanismsThrowsOnInvalidEnum()
        {
            const SimpleCalculationResult invalidEnum = (SimpleCalculationResult)15;
            Assert.Throws<InvalidEnumArgumentException>(() => new FailureMechanismSectionAssemblyCalculator().SimpleAssessmentDirectFailureMechanisms(invalidEnum));
        }

        [Test]
        public void SimpleAssessmentIndirectFailureMechanismsThrowsNotImplementedException()
        {
            Assert.Throws<NotImplementedException>(() => new FailureMechanismSectionAssemblyCalculator().SimpleAssessmentIndirectFailureMechanisms(SimpleCalculationResult.FV));
        }

        [Test]
        [TestCase(SimpleCalculationResultValidityOnly.NVT, FailureMechanismSectionCategoryGroup.NotApplicable, 0.0)]
        [TestCase(SimpleCalculationResultValidityOnly.WVT, FailureMechanismSectionCategoryGroup.VIIv, double.NaN)]
        [TestCase(SimpleCalculationResultValidityOnly.None, FailureMechanismSectionCategoryGroup.None, double.NaN)]
        public void SimpleAssessmentDirectFailureMechanismsValidityOnlyResultsCorrectCategory(SimpleCalculationResultValidityOnly calculationResult, FailureMechanismSectionCategoryGroup expectedResult,double expectedProbability)
        {
            var calculationOutput = new FailureMechanismSectionAssemblyCalculator().SimpleAssessmentDirectFailureMechanisms(calculationResult);

            Assert.IsNotNull(calculationOutput);
            Assert.IsEmpty(calculationOutput.WarningMessages);
            Assert.IsNotNull(calculationOutput.Result);
            Assert.AreEqual(expectedResult, calculationOutput.Result.CategoryGroup);
            Assert.AreEqual(expectedProbability, calculationOutput.Result.EstimatedProbabilityOfFailure);
        }

        [Test]
        public void SimpleAssessmentDirectFailureMechanismsValidityOnlyThrowsOnInvalidEnum()
        {
            const SimpleCalculationResultValidityOnly invalidEnum = (SimpleCalculationResultValidityOnly)15;
            Assert.Throws<InvalidEnumArgumentException>(() => new FailureMechanismSectionAssemblyCalculator().SimpleAssessmentDirectFailureMechanisms(invalidEnum));
        }

        #endregion

        #region Detailed assessment

        [Test]
        [TestCase(DetailedCalculationResult.V, FailureMechanismSectionCategoryGroup.IIv)]
        [TestCase(DetailedCalculationResult.VN, FailureMechanismSectionCategoryGroup.Vv)]
        [TestCase(DetailedCalculationResult.NGO, FailureMechanismSectionCategoryGroup.VIIv)]
        [TestCase(DetailedCalculationResult.None, FailureMechanismSectionCategoryGroup.None)]
        public void DetailedAssessmentDirectFailureMechanismsFromResultTranslatesResultCorrectly(DetailedCalculationResult result, FailureMechanismSectionCategoryGroup expectedCategoryGroup)
        {
            var calculationOutput = new FailureMechanismSectionAssemblyCalculator().DetailedAssessmentDirectFailureMechanisms(result);

            Assert.IsNotNull(calculationOutput);
            Assert.IsEmpty(calculationOutput.WarningMessages);
            Assert.AreEqual(expectedCategoryGroup,calculationOutput.Result);
        }

        [Test]
        public void DetailedAssessmentDirectFailureMechsnismsThrowsOnInvalidEnum()
        {
            const DetailedCalculationResult invalidEnum = (DetailedCalculationResult)15;
            Assert.Throws<InvalidEnumArgumentException>(() => new FailureMechanismSectionAssemblyCalculator().DetailedAssessmentDirectFailureMechanisms(invalidEnum));
        }

        [Test]
        public void DetailedAssessmentIndirectFailureMechanismsFromResultThrowsNotImplementedException()
        {
            Assert.Throws<NotImplementedException>(() => new FailureMechanismSectionAssemblyCalculator().DetailedAssessmentIndirectFailureMechanisms(DetailedCalculationResult.V));
        }

        [Test]
        [TestCase(0,FailureMechanismSectionCategoryGroup.Iv, 0.0)]
        [TestCase(0.1, FailureMechanismSectionCategoryGroup.Iv, 0.1)]
        [TestCase(0.4, FailureMechanismSectionCategoryGroup.IVv, 0.4)]
        [TestCase(0.9, FailureMechanismSectionCategoryGroup.VIv, 0.9)]
        [TestCase(1, FailureMechanismSectionCategoryGroup.VIv, 1.0)]
        [TestCase(double.NaN, FailureMechanismSectionCategoryGroup.None, double.NaN)]
        public void DetailedAssessmentDirectFailureMechanismsFromProbabilitySelectsCorrectCategory(double probability, FailureMechanismSectionCategoryGroup expectedCategoryGroup, double expectedProbability)
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

            var input = new DetailedCalculationInputFromProbability((Probability)probability, categories);
            var calculationOutput = new FailureMechanismSectionAssemblyCalculator().DetailedAssessmentDirectFailureMechanisms(input);

            Assert.IsNotNull(calculationOutput);
            Assert.IsEmpty(calculationOutput.WarningMessages);
            Assert.IsNotNull(calculationOutput.Result);
            Assert.AreEqual(expectedCategoryGroup, calculationOutput.Result.CategoryGroup);
            Assert.AreEqual(expectedProbability, calculationOutput.Result.EstimatedProbabilityOfFailure);
        }

        [Test]
        public void DetailedAssessmentDirectFailureMechanismsFromProbabilityThrowsOnNullInput()
        {
            try
            {
                new FailureMechanismSectionAssemblyCalculator().DetailedAssessmentDirectFailureMechanisms((DetailedCalculationInputFromProbability) null);
                Assert.Fail("Expected exception");
            }
            catch (AssemblyToolKernelException e)
            {
                Assert.AreEqual(1,e.Code.Length);
                Assert.AreEqual(ErrorCode.InputIsNull,e.Code[0]);
            }
        }

        [Test]
        public void DetailedAssessmentDirectFailureMechanismsFromProbabilityThrowsOnNonMatchingCategories()
        {
            var categories = new[]
            {
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.IVv,(Probability)0.3,(Probability)0.4),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.Vv,(Probability)0.4,(Probability)0.5),
                new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.VIv,(Probability)0.5,(Probability)1),
            };
            var input = new DetailedCalculationInputFromProbability((Probability)0.1549874856, categories);

            try
            {
                new FailureMechanismSectionAssemblyCalculator().DetailedAssessmentDirectFailureMechanisms(input);
                Assert.Fail("Expected exception");
            }
            catch (AssemblyToolKernelException e)
            {
                Assert.AreEqual(1, e.Code.Length);
                Assert.AreEqual(ErrorCode.NoMatchingCategory, e.Code[0]);
            }
        }

        [Test]
        [TestCase(DetailedCalculationResult.V,DetailedCalculationResult.NGO,DetailedCalculationResult.None,DetailedCalculationResult.NGO,DetailedCalculationResult.None, FailureMechanismSectionCategoryGroup.Iv)]
        [TestCase(DetailedCalculationResult.VN, DetailedCalculationResult.V, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, FailureMechanismSectionCategoryGroup.IIv)]
        [TestCase(DetailedCalculationResult.VN, DetailedCalculationResult.VN, DetailedCalculationResult.V, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, FailureMechanismSectionCategoryGroup.IIIv)]
        [TestCase(DetailedCalculationResult.VN, DetailedCalculationResult.VN, DetailedCalculationResult.VN, DetailedCalculationResult.V, DetailedCalculationResult.NGO, FailureMechanismSectionCategoryGroup.IVv)]
        [TestCase(DetailedCalculationResult.VN, DetailedCalculationResult.VN, DetailedCalculationResult.VN, DetailedCalculationResult.VN, DetailedCalculationResult.V, FailureMechanismSectionCategoryGroup.Vv)]
        [TestCase(DetailedCalculationResult.VN, DetailedCalculationResult.VN, DetailedCalculationResult.VN, DetailedCalculationResult.VN, DetailedCalculationResult.VN, FailureMechanismSectionCategoryGroup.VIv)]
        [TestCase(DetailedCalculationResult.NGO, DetailedCalculationResult.V, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, FailureMechanismSectionCategoryGroup.IIv)]
        [TestCase(DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, DetailedCalculationResult.V, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, FailureMechanismSectionCategoryGroup.IIIv)]
        [TestCase(DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, DetailedCalculationResult.V, DetailedCalculationResult.NGO, FailureMechanismSectionCategoryGroup.IVv)]
        [TestCase(DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, DetailedCalculationResult.V, FailureMechanismSectionCategoryGroup.Vv)]
        [TestCase(DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, FailureMechanismSectionCategoryGroup.VIIv)]
        [TestCase(DetailedCalculationResult.VN, DetailedCalculationResult.NGO, DetailedCalculationResult.VN, DetailedCalculationResult.NGO, DetailedCalculationResult.VN, FailureMechanismSectionCategoryGroup.VIv)]
        [TestCase(DetailedCalculationResult.None, DetailedCalculationResult.None, DetailedCalculationResult.None, DetailedCalculationResult.None, DetailedCalculationResult.None, FailureMechanismSectionCategoryGroup.None)]
        public void DetailedAssessmentDirectFailureMechanismsGroupThreeReturnsCorrectCategory(DetailedCalculationResult iToII, DetailedCalculationResult iIToIII, DetailedCalculationResult iIIToIV, DetailedCalculationResult iVToV, DetailedCalculationResult vToVI, FailureMechanismSectionCategoryGroup expectedCategoryGroup)
        {
            var input = new DetailedCategoryBoundariesCalculationResult(iToII,iIToIII,iIIToIV,iVToV,vToVI);
            var calculationOutput = new FailureMechanismSectionAssemblyCalculator().DetailedAssessmentDirectFailureMechanisms(input);

            Assert.IsNotNull(calculationOutput);
            Assert.IsEmpty(calculationOutput.WarningMessages);
            Assert.IsNotNull(calculationOutput.Result);
            Assert.AreEqual(expectedCategoryGroup, calculationOutput.Result);
        }

        [Test]
        [TestCase(DetailedCalculationResult.V, DetailedCalculationResult.VN, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO)]
        [TestCase(DetailedCalculationResult.V, DetailedCalculationResult.NGO, DetailedCalculationResult.VN, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO)]
        [TestCase(DetailedCalculationResult.V, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, DetailedCalculationResult.VN, DetailedCalculationResult.NGO)]
        [TestCase(DetailedCalculationResult.V, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, DetailedCalculationResult.VN)]
        [TestCase(DetailedCalculationResult.VN, DetailedCalculationResult.V, DetailedCalculationResult.VN, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO)]
        [TestCase(DetailedCalculationResult.VN, DetailedCalculationResult.V, DetailedCalculationResult.NGO, DetailedCalculationResult.VN, DetailedCalculationResult.NGO)]
        [TestCase(DetailedCalculationResult.VN, DetailedCalculationResult.V, DetailedCalculationResult.NGO, DetailedCalculationResult.NGO, DetailedCalculationResult.VN)]
        [TestCase(DetailedCalculationResult.VN, DetailedCalculationResult.VN, DetailedCalculationResult.V, DetailedCalculationResult.VN, DetailedCalculationResult.NGO)]
        [TestCase(DetailedCalculationResult.VN, DetailedCalculationResult.VN, DetailedCalculationResult.V, DetailedCalculationResult.NGO, DetailedCalculationResult.VN)]
        [TestCase(DetailedCalculationResult.VN, DetailedCalculationResult.VN, DetailedCalculationResult.VN, DetailedCalculationResult.V, DetailedCalculationResult.VN)]
        public void DetailedAssessmentDirectFailureMechanismsGroupThreeThrowsOnImpossibleResultCombination(DetailedCalculationResult iToII, DetailedCalculationResult iIToIII, DetailedCalculationResult iIIToIV, DetailedCalculationResult iVToV, DetailedCalculationResult vToVI)
        {
            var input = new DetailedCategoryBoundariesCalculationResult(iToII, iIToIII, iIIToIV, iVToV, vToVI);
            try
            {
                new FailureMechanismSectionAssemblyCalculator().DetailedAssessmentDirectFailureMechanisms(input);
                Assert.Fail("Expected exception.");
            }
            catch (AssemblyToolKernelException e)
            {
                Assert.AreEqual(1,e.Code.Length);
                Assert.AreEqual(ErrorCode.ImpossibleResultCombination,e.Code[0]);
            }
        }

        [Test]
        [TestCase(0,2, FailureMechanismSectionCategoryGroup.Iv, 0.0)]
        [TestCase(0.1,3.5, FailureMechanismSectionCategoryGroup.Iv, 0.1)]
        [TestCase(0.4,1.145, FailureMechanismSectionCategoryGroup.IVv, 0.4)]
        [TestCase(0.9, 2.542,FailureMechanismSectionCategoryGroup.VIv, 0.9)]
        [TestCase(1, 5.135, FailureMechanismSectionCategoryGroup.VIv, 1.0)]
        [TestCase(double.NaN, 5.135, FailureMechanismSectionCategoryGroup.None, double.NaN)]
        public void DetailedAssessmentDirectFailureMechanismsFromProbabilityWithNValueReturnsCorrectResult(double probability,double nValue, FailureMechanismSectionCategoryGroup expectedCategoryGroup, double expectedProbabilityWithoutNMultiplication)
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

            var input = new DetailedCalculationInputFromProbabilityWithLengthEffect((Probability)probability, categories,nValue);
            var calculationOutput = new FailureMechanismSectionAssemblyCalculator().DetailedAssessmentDirectFailureMechanisms(input);

            Assert.IsNotNull(calculationOutput);
            var expectedProbability = expectedProbabilityWithoutNMultiplication * nValue;
            if (expectedProbability > 1)
            {
                Assert.AreEqual(1,calculationOutput.WarningMessages.Length);
                Assert.AreEqual(WarningMessage.CorrectedProbability,calculationOutput.WarningMessages[0]);
            }
            else
            {
                Assert.IsEmpty(calculationOutput.WarningMessages);
            }
            
            Assert.IsNotNull(calculationOutput.Result);
            Assert.AreEqual(expectedCategoryGroup, calculationOutput.Result.CategoryGroup);
            Assert.AreEqual(Math.Min(1,expectedProbability), calculationOutput.Result.EstimatedProbabilityOfFailure);
        }

        [Test]
        public void DetailedAssessmentDirectFailureMEchanismsFromProbabilityWithNValueThrowsOnEmptyInput()
        {
            try
            {
                new FailureMechanismSectionAssemblyCalculator().DetailedAssessmentDirectFailureMechanisms((DetailedCalculationInputFromProbabilityWithLengthEffect)null);
                Assert.Fail("Expected exception.");
            }
            catch (AssemblyToolKernelException e)
            {
                Assert.AreEqual(1, e.Code.Length);
                Assert.AreEqual(ErrorCode.InputIsNull, e.Code[0]);
            }
        }

        #endregion

        #region Tailor made assessment

        [Test]
        [TestCase(TailorMadeCalculationResult.FV, FailureMechanismSectionCategoryGroup.Iv)]
        [TestCase(TailorMadeCalculationResult.V, FailureMechanismSectionCategoryGroup.IIv)]
        [TestCase(TailorMadeCalculationResult.VN, FailureMechanismSectionCategoryGroup.Vv)]
        [TestCase(TailorMadeCalculationResult.NGO, FailureMechanismSectionCategoryGroup.VIIv)]
        public void TailorMadeAssessmentDirectFailureMechanismsFromResultTranslatesResultCorrectly(TailorMadeCalculationResult result, FailureMechanismSectionCategoryGroup expectedCategoryGroup)
        {
            var calculationOutput = new FailureMechanismSectionAssemblyCalculator().TailorMadeAssessmentDirectFailureMechanisms(result);

            Assert.IsNotNull(calculationOutput);
            Assert.IsEmpty(calculationOutput.WarningMessages);
            Assert.AreEqual(expectedCategoryGroup, calculationOutput.Result);
        }

        [Test]
        public void TailorMadeAssessmentIndirectFailureMechanismsFromResultThrowsNotImplementedException()
        {
            Assert.Throws<NotImplementedException>(() => new FailureMechanismSectionAssemblyCalculator().TailorMadeAssessmentIndirectFailureMechanisms(TailorMadeCalculationResult.FV));
        }

        [Test]
        [TestCase(TailorMadeProbabilityCalculationResultGroup.FV, FailureMechanismSectionCategoryGroup.Iv, 0.0)]
        [TestCase(TailorMadeProbabilityCalculationResultGroup.NGO, FailureMechanismSectionCategoryGroup.VIIv, double.NaN)]
        public void TailorMadeAssessmentDirectFailureMechanismsFromProbabilityReturnsCorrectCategoryNoProbability(TailorMadeProbabilityCalculationResultGroup resultGroup, FailureMechanismSectionCategoryGroup expectedCategoryGroup, double expectedProbability)
        {
            var input = new TailorMadeCalculationInputFromProbability(
                new TailorMadeProbabilityCalculationResult(resultGroup),
                new[]
                {
                    new FailureMechanismSectionCategory(FailureMechanismSectionCategoryGroup.Iv, (Probability) 0,(Probability) 1)
                });
            
            var calculationOutput = new FailureMechanismSectionAssemblyCalculator().TailorMadeAssessmentDirectFailureMechanisms(input);

            Assert.IsNotNull(calculationOutput);
            Assert.IsEmpty(calculationOutput.WarningMessages);
            Assert.IsNotNull(calculationOutput.Result);
            Assert.AreEqual(expectedCategoryGroup, calculationOutput.Result.CategoryGroup);
            Assert.AreEqual(expectedProbability, calculationOutput.Result.EstimatedProbabilityOfFailure);
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

            var probability = (Probability)0.35;
            var result = new TailorMadeProbabilityCalculationResult(probability);
            var input = new TailorMadeCalculationInputFromProbability(result, categories);
            var calculationOutput = new FailureMechanismSectionAssemblyCalculator().TailorMadeAssessmentDirectFailureMechanisms(input);

            Assert.IsNotNull(calculationOutput);
            Assert.IsEmpty(calculationOutput.WarningMessages);
            Assert.IsNotNull(calculationOutput.Result);
            Assert.AreEqual(FailureMechanismSectionCategoryGroup.IVv, calculationOutput.Result.CategoryGroup);
            Assert.AreEqual(probability, calculationOutput.Result.EstimatedProbabilityOfFailure);
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
            var calculationOutput = new FailureMechanismSectionAssemblyCalculator().TailorMadeAssessmentDirectFailureMechanisms(result);

            Assert.IsNotNull(calculationOutput);
            Assert.IsEmpty(calculationOutput.WarningMessages);
            Assert.AreEqual(expectedCategoryGroup, calculationOutput.Result);
        }

        #endregion

        #region Combined assessment

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

        #endregion
    }
}
