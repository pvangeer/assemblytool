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
using AssemblyTool.Kernel.Categories;
using AssemblyTool.Kernel.Data.AssemblyCategories;
using AssemblyTool.Kernel.Data.CalculationResults;
using AssemblyTool.Kernel.ErrorHandling;

namespace AssemblyTool.Kernel.Assembly.CalculatorInput
{
    public class TailorMadeCalculationInputFromProbabilityWithLengthEffectFactor : TailorMadeCalculationInputFromProbability
    {
        /// <param name="result">The calculation result that needs to be translated.</param>
        /// <param name="categories">The list of categories for this failure mechanism that is used in case of a probability result, obtained with <see cref="CategoriesCalculator.CalculateFailureMechanismSectionCategories"/></param>
        /// <param name="nValue">The length effect factor for the failuremechanism section that is being considered.</param>
        /// <returns><see cref="CalculationOutput{TResult}"/> containing the determined <see cref="FailureMechanismSectionCategoryGroup"/>.</returns>
        /// <exception cref="AssemblyToolKernelException">Thrown when the <see cref="result"/> equals null.</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when the <see cref="nValue"/> is NaN or smaller than 1.</exception>
        public TailorMadeCalculationInputFromProbabilityWithLengthEffectFactor(TailorMadeProbabilityCalculationResult result, FailureMechanismSectionCategory[] categories, double nValue) : base(result, categories)
        {
            ValidateNValue(nValue);
            NValue = nValue;
        }

        /// <summary>
        /// Section specific length effect factor.
        /// </summary>
        public double NValue { get; }

        private void ValidateNValue(double nValue)
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