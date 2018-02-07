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

using System;
using AssemblyTool.Kernel.Data.AssemblyCategories;
using NUnit.Framework;

namespace AssemblyTool.Kernel.Data.Test.AssemblyCategories
{
    [TestFixture]
    public class FailureMechanismSectionAssemblyCategoryGroupTest
    {
        [Test]
        public void Values_ExpectedValues()
        {
            // Assert
            Assert.AreEqual(9, Enum.GetValues(typeof(FailureMechanismSectionCategoryGroup)).Length);
            Assert.AreEqual(0, (int)FailureMechanismSectionCategoryGroup.Iv);
            Assert.AreEqual(1, (int)FailureMechanismSectionCategoryGroup.IIv);
            Assert.AreEqual(2, (int)FailureMechanismSectionCategoryGroup.IIIv);
            Assert.AreEqual(3, (int)FailureMechanismSectionCategoryGroup.IVv);
            Assert.AreEqual(4, (int)FailureMechanismSectionCategoryGroup.Vv);
            Assert.AreEqual(5, (int)FailureMechanismSectionCategoryGroup.VIv);
            Assert.AreEqual(6, (int)FailureMechanismSectionCategoryGroup.VIIv);
            Assert.AreEqual(7, (int)FailureMechanismSectionCategoryGroup.NotApplicable);
            Assert.AreEqual(8, (int)FailureMechanismSectionCategoryGroup.None);
        }
    }
}
