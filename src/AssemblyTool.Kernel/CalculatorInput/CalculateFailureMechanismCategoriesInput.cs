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
    public class CalculateFailureMechanismCategoriesInput : CalculateAssessmentSectionCategoriesInput
    {
        /// <summary>
        /// Creates an instance of the input class that should be used when calling <seealso cref="CategoriesCalculator.CalculateFailureMechanismCategories"/>.
        /// </summary>
        /// <param name="signalingStandard">The signalling standard for this assessment section.</param>
        /// <param name="lowerBoundaryStandard">The lower boundary standard for this assessment section.</param>
        /// <param name="probabilityDistributionFactor">The (combined) contribution of this/these failure mechanism(s) to the total probability of failure of the assessment section in the failure probability distribution table.</param>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="signalingStandard"/> is no probability (not in the range [0-1]).</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="lowerBoundaryStandard"/> is no probability (not in the range [0-1]).</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="probabilityDistributionFactor"/> is not in the range [0-1].</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <paramref name="signalingStandard"/> has a higher probability than <paramref name="lowerBoundaryStandard"/>.</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="probabilityDistributionFactor"/> is NaN</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="probabilityDistributionFactor"/> is smaller than 0</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="probabilityDistributionFactor"/> exceeds 1</exception>
        public CalculateFailureMechanismCategoriesInput(Probability signalingStandard, Probability lowerBoundaryStandard, double probabilityDistributionFactor) : base(signalingStandard, lowerBoundaryStandard)
        {
            ValidateProbabilityDistributionFactor(probabilityDistributionFactor);
            ProbabilityDistributionFactor = probabilityDistributionFactor;
        }
        
        /// <summary>
        /// The (combined) probability distribution factor provides the percentage of the norm that should be reserved for the specific failure mechanism(s) for which categories are calculated.
        /// </summary>
        public double ProbabilityDistributionFactor { get; }

        /// <summary>
        /// Validates the entered probability distrubution factor.
        /// </summary>
        /// <param name="probabilityDistributionFactor">The probability distribution factor to validate</param>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="probabilityDistributionFactor"/> is NaN</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="probabilityDistributionFactor"/> is smaller than 0</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="probabilityDistributionFactor"/> exceeds 1</exception>
        private static void ValidateProbabilityDistributionFactor(double probabilityDistributionFactor)
        {
            if (Double.IsNaN(probabilityDistributionFactor))
            {
                throw new AssemblyToolKernelException(ErrorCode.InvalidProbabilityDistributionFactor, new AssemblyToolKernelException(ErrorCode.ValueIsNaN));
            }

            if (probabilityDistributionFactor < 0)
            {
                throw new AssemblyToolKernelException(ErrorCode.InvalidProbabilityDistributionFactor, new AssemblyToolKernelException(ErrorCode.ValueBelowZero));
            }

            if (probabilityDistributionFactor > 1)
            {
                throw new AssemblyToolKernelException(ErrorCode.InvalidProbabilityDistributionFactor, new AssemblyToolKernelException(ErrorCode.ValueAboveOne));
            }
        }
    }
}
