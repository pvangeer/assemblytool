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

namespace AssemblyTool.Kernel.ErrorHandling
{
    public enum ErrorCode
    {
        /// <summary>
        ///  The value of this parameter is supposed not to be lower than 0, but it is.
        /// </summary>
        ValueBelowZero,

        /// <summary>
        /// The value of this parameter is supposed not to exceed 1, but it does.
        /// </summary>
        ValueAboveOne,

        /// <summary>
        /// The entered signaling standard exceeds the lower boundary. This should bot be the case.
        /// </summary>
        SignallingStandardExceedsLowerBoundary,

        /// <summary>
        /// The entered signaling standard is not valid. See the innerexception for more details.
        /// </summary>
        InvalidSignalingStandard,

        /// <summary>
        /// The entered lower boundary standard is not valid. See the innerexception for more details.
        /// </summary>
        InvalidLowerBoundaryStandard,

        /// <summary>
        /// The entered probability distribution factor is not valid. See the inner exception for more details.
        /// </summary>
        InvalidProbabilityDistributionFactor,

        /// <summary>
        /// The value of this double equals NaN.
        /// </summary>
        ValueIsNaN,

        /// <summary>
        /// The lower boundary (probability) of a category should be lower than the upperboundary (probability), but it is not.
        /// </summary>
        CategoryLowerBoundaryExceedsUpperBoundary,

        /// <summary>
        /// Value should be above one (or equal to one), but it is not
        /// </summary>
        ValueBelowOne,

        /// <summary>
        /// The specified N - value is invalid. See the inner exception for more details.
        /// </summary>
        InvalidNValue
    }
}