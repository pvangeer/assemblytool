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

using System.ComponentModel;
using AssemblyTool.Kernel.Categories;
using AssemblyTool.Kernel.Data.AssemblyCategories;
using AssemblyTool.Kernel.Data.CalculationResults;
using AssemblyTool.Kernel.ErrorHandling;

namespace AssemblyTool.Kernel.Assembly.CalculatorInput
{
    public class TailorMadeCalculationInputFromProbability
    {
        /// <summary>
        /// This class is inteded to be used as input in <see cref="IFailureMechanismSectionAssemblyCalculator.TailorMadeAssessmentDirectFailureMechanisms(TailorMadeCalculationInputFromProbability)"/>.
        /// </summary>
        /// <param name="result">The specified tailor made calculation result.</param>
        /// <param name="categories">The categories for this failure mechanisms obtained with <see cref="CategoriesCalculator.CalculateFailureMechanismSectionCategories"/>.</param>
        /// <exception cref="AssemblyToolKernelException">Thrown when the <see cref="result"/> equals null.</exception>
        /// <exception cref="AssemblyToolKernelException">Thrown when <see cref="categories"/> equals null or an emtpy list.</exception>
        public TailorMadeCalculationInputFromProbability(TailorMadeProbabilityCalculationResult result, FailureMechanismSectionCategory[] categories)
        {
            ValidateResult(result);
            ValidateCategories(categories);

            Result = result;
            Categories = categories;
        }

        /// <summary>
        /// The result that needs to be translated into a category.
        /// </summary>
        public TailorMadeProbabilityCalculationResult Result { get; }

        /// <summary>
        /// A list of categories describing the categories and category boundaries for the sections of a failure mechanism
        /// </summary>
        public FailureMechanismSectionCategory[] Categories { get; }

        private void ValidateCategories(FailureMechanismSectionCategory[] categories)
        {
            if (categories == null || categories.Length == 0)
            {
                throw new AssemblyToolKernelException(ErrorCode.InputIsNull);
            }
        }

        private static void ValidateResult(TailorMadeProbabilityCalculationResult result)
        {
            if (result == null)
            {
                throw new AssemblyToolKernelException(ErrorCode.InputIsNull);
            }
        }
    }
}