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
using AssemblyTool.Kernel.ErrorHandling;

namespace AssemblyTool.Kernel.Services
{
    public static class ProbabilityValidator
    {
        /// <summary>
        /// Validates <paramref name="probability"/> for being a valid probability. This means a double within the range [0-1].
        /// </summary>
        /// <param name="probability">The probability to validate</param>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="probability"/> is NaN</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="probability"/> is smaller than 0</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="probability"/> exceeds 1</exception>
        public static void Validate(double probability)
        {
            if (Double.IsNaN(probability))
            {
                throw new AssemblyToolKernelException(ErrorCode.ValueIsNaN);
            }

            if (probability < 0)
            {
                throw new AssemblyToolKernelException(ErrorCode.ValueBelowZero);
            }

            if (probability > 1)
            {
                throw new AssemblyToolKernelException(ErrorCode.ValueAboveOne);
            }
        }

        /// <summary>
        /// Validates the lower and upper probabilities.
        /// </summary>
        /// <param name="signalingStandard">The signalling standard for this assessment section.</param>
        /// <param name="lowerBoundaryStandard">The lower boundary standard for this assessment section.</param>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="signalingStandard"/> is not a valid probability</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="lowerBoundaryStandard"/> is not a valid probability</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown in case <paramref name="signalingStandard"/> exceeds <paramref name="lowerBoundaryStandard"/></exception>
        public static void ValidateStandards(double signalingStandard, double lowerBoundaryStandard)
        {
            try
            {
                Validate(signalingStandard);
            }
            catch (AssemblyToolKernelException e)
            {
                throw new AssemblyToolKernelException(ErrorCode.InvalidSignalingStandard, e);
            }

            try
            {
                Validate(lowerBoundaryStandard);
            }
            catch (AssemblyToolKernelException e)
            {
                throw new AssemblyToolKernelException(ErrorCode.InvalidLowerBoundaryStandard, e);
            }

            if (signalingStandard > lowerBoundaryStandard)
            {
                throw new AssemblyToolKernelException(ErrorCode.SignallingStandardExceedsLowerBoundary);
            }
        }
    }
}
