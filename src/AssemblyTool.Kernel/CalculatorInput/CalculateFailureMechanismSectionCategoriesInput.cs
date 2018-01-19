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
using AssemblyTool.Kernel.Data;
using AssemblyTool.Kernel.ErrorHandling;

namespace AssemblyTool.Kernel.CalculatorInput
{
    public class CalculateFailureMechanismSectionCategoriesInput : CalculateFailureMechanismCategoriesInput
    {
        /// <summary>
        /// Creates an instance of the input class that should be used for <seealso cref="CategoriesCalculator.CalculateFailureMechanismSectionCategories"/> or <seealso cref="CategoriesCalculator.CalculateGeotechnicFailureMechanismSectionCategories"/>
        /// </summary>
        /// <param name="signalingStandard">The signalling standard for this assessment section.</param>
        /// <param name="lowerBoundaryStandard">The lower boundary standard for this assessment section.</param>
        /// <param name="probabilityDistributionFactor">The (combined) contribution of this/these failure mechanism(s) to the total probability of failure of the assessment section in the failure probability distribution table.</param>
        /// <param name="nValue">The value of N to take into account the length effect.</param>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="signalingStandard"/> is no probability (not in the range [0-1]).</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="lowerBoundaryStandard"/> is no probability (not in the range [0-1]).</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="probabilityDistributionFactor"/> is not in the range [0-1].</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="signalingStandard"/> has a higher probability than <paramref name="lowerBoundaryStandard"/>.</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="nValue"/> is smaller than 1 or NaN.</exception>
        public CalculateFailureMechanismSectionCategoriesInput(Probability signalingStandard, Probability lowerBoundaryStandard, double probabilityDistributionFactor, double nValue) : base(signalingStandard, lowerBoundaryStandard, probabilityDistributionFactor)
        {
            ValidateNValue(nValue);
            NValue = nValue;
        }

        /// <summary>
        /// N-value to take into account length-effect
        /// </summary>
        public double NValue { get; }

        /// <summary>
        /// This method validates the specified N - value to take into account the length-effect. N needs to be a valid double >= 1.
        /// </summary>
        /// <param name="nValue">The N-value that needs to be validated.</param>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="nValue"/> equals NaN</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="nValue"/> is a double smaller than 1.0</exception>
        private static void ValidateNValue(double nValue)
        {
            if (Double.IsNaN(nValue))
            {
                throw new AssemblyToolKernelException(ErrorCode.InvalidNValue, new AssemblyToolKernelException(ErrorCode.ValueIsNaN));
            }

            if (nValue < 1)
            {
                throw new AssemblyToolKernelException(ErrorCode.InvalidNValue, new AssemblyToolKernelException(ErrorCode.ValueBelowOne));
            }
        }
    }
}
