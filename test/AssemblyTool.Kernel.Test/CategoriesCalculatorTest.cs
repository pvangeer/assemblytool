﻿// Copyright (C) Stichting Deltares 2018. All rights reserved.
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
using AssemblyTool.Kernel.Data;
using NUnit.Framework;

namespace AssemblyTool.Kernel.Test
{
    [TestFixture]
    public class CategoriesCalculatorTest
    {
        [Test]
        public void CalculateAssessmentSectionCategoriesTest()
        {
            var signalingStandard = 1 / 3000.0;
            var lowerBoundaryStandard = 1 / 1000.0;
            var result = CategoriesCalculator.CalculateAssessmentSectionCategories(signalingStandard, lowerBoundaryStandard);

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

        [Test]
        public void CalculateFailureMechanismCategoriesTest()
        {
            var signalingStandard = 1 / 3000.0;
            var lowerBoundaryStandard = 1 / 1000.0;
            var probabilityDistributionFactor = 0.5;
            var result = CategoriesCalculator.CalculateFailureMechanismCategories(signalingStandard, lowerBoundaryStandard,probabilityDistributionFactor);

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

        [Test]
        public void CalculateFailureMechanismSectionCategoriesTest()
        {
            var signalingStandard = 1 / 3000.0;
            var lowerBoundaryStandard = 1 / 1000.0;
            var probabilityDistributionFactor = 0.5;
            var nValue = 2.5;
            var result = CategoriesCalculator.CalculateFailureMechanismSectionCategories(signalingStandard, lowerBoundaryStandard, probabilityDistributionFactor, nValue);

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

        [Test]
        public void CalculateGeotechnicFailureMechanismSectionCategoriesTest()
        {
            var signalingStandard = 1 / 3000.0;
            var lowerBoundaryStandard = 1 / 1000.0;
            var probabilityDistributionFactor = 0.04;
            var nValue = 2.5;
            var result = CategoriesCalculator.CalculateGeotechnicFailureMechanismSectionCategories(signalingStandard, lowerBoundaryStandard, probabilityDistributionFactor, nValue);

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
            var signalingStandard = 1 / 3000.0;
            var lowerBoundaryStandard = 1 / 1000.0;
            var probabilityDistributionFactor = 0.5;
            var nValue = 2.5;
            var result = CategoriesCalculator.CalculateGeotechnicFailureMechanismSectionCategories(signalingStandard, lowerBoundaryStandard, probabilityDistributionFactor, nValue);

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
    }
}
