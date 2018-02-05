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
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace AssemblyTool.Kernel.ErrorHandling.Test
{
    [TestFixture]
    public class AssemblyToolKernelExceptionTest
    {
        [Test]
        [TestCase(ErrorCode.SignallingStandardExceedsLowerBoundary)]
        [TestCase(ErrorCode.CategoryLowerBoundaryExceedsUpperBoundary)]
        [TestCase(ErrorCode.InvalidLowerBoundaryStandard)]
        [TestCase(ErrorCode.InvalidProbabilityDistributionFactor)]
        public void ConstructorPassesErrorCodeCorrectly(ErrorCode errorCode)
        {
            var exception = new AssemblyToolKernelException(errorCode);

            Assert.AreEqual(1,exception.Code.Length);
            Assert.AreEqual(errorCode,exception.Code[0]);
            Assert.AreEqual(errorCode.GetMessage(),exception.Message);
            Assert.IsNull(exception.InnerException);
        }

        [Test]
        public void ConstructorPassesErrorCodeAndInnerExceptionCorrectly()
        {
            var errorCode = ErrorCode.InvalidProbabilityDistributionFactor;
            var innerException = new AssemblyToolKernelException(ErrorCode.ValueBelowOne);
            var exception = new AssemblyToolKernelException(errorCode,innerException);

            Assert.AreEqual(1,exception.Code.Length);
            Assert.AreEqual(errorCode, exception.Code[0]);
            Assert.AreEqual(errorCode.GetMessage(), exception.Message);
            Assert.AreEqual(innerException,exception.InnerException);
        }
    }
}
