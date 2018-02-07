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
using AssemblyTool.Kernel.Data.CalculationResults;
using NUnit.Framework;

namespace AssemblyTool.Kernel.Data.Test.AssessmentResults
{
    [TestFixture]
    public class TailorMadeAssessmentResultTest
    {
        [Test]
        public void Values_ExpectedValues()
        {
            // Assert
            Assert.AreEqual(5, Enum.GetValues(typeof(TailorMadeCalculationResult)).Length);
            Assert.AreEqual(1, (int)TailorMadeCalculationResult.V);
            Assert.AreEqual(2, (int)TailorMadeCalculationResult.VN);
            Assert.AreEqual(3, (int)TailorMadeCalculationResult.NGO);
            Assert.AreEqual(4, (int)TailorMadeCalculationResult.FV);
            Assert.AreEqual(5, (int)TailorMadeCalculationResult.None);
        }
    }
}
