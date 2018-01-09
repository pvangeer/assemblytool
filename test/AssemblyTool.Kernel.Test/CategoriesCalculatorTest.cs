using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Assert.AreEqual(AssemblyCategory.APlus,firstCategory.Category);
            Assert.AreEqual(0,firstCategory.LowerBoundary);
            Assert.AreEqual(1/30.0*signalingStandard,firstCategory.UpperBoundary);
            var category2 = result[1];
            Assert.AreEqual(AssemblyCategory.A, category2.Category);
            Assert.AreEqual(1 / 30.0 * signalingStandard, category2.LowerBoundary);
            Assert.AreEqual(signalingStandard, category2.UpperBoundary);
            var category3 = result[2];
            Assert.AreEqual(AssemblyCategory.B, category3.Category);
            Assert.AreEqual(signalingStandard, category3.LowerBoundary);
            Assert.AreEqual(lowerBoundaryStandard, category3.UpperBoundary);
            var category4 = result[3];
            Assert.AreEqual(AssemblyCategory.C, category4.Category);
            Assert.AreEqual(lowerBoundaryStandard, category4.LowerBoundary);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category4.UpperBoundary);
            var category5 = result[4];
            Assert.AreEqual(AssemblyCategory.D, category5.Category);
            Assert.AreEqual(30.0 * lowerBoundaryStandard, category5.LowerBoundary);
            Assert.AreEqual(1, category5.UpperBoundary);
        }
    }
}
