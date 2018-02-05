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

using AssemblyTool.Kernel.Categories.CalculatorInput;
using AssemblyTool.Kernel.Data.AssemblyCategories;
using AssemblyTool.Kernel.ErrorHandling;

namespace AssemblyTool.Kernel.Categories
{
    public interface ICategoriesCalculator
    {
        /// <summary>
        /// Calculates category boundaries (probabilities) for usages in assembly of WBI2017 assessment results on assessment section level. 
        /// This method implements "WBI-2-1" from the Functional Design.
        /// </summary>
        /// <param name="input">The input for this method, consisting of an signaling standard and a lower boundary standard.</param>
        /// <returns>A collection of <see cref="AssessmentSectionCategory"/> that contains all assembly categories with their boundaries.</returns>
        /// <exception cref="AssemblyToolKernelException">Thrown when inconsistent boundaries were derived.</exception>
        CalculationOutput<AssessmentSectionCategory[]> CalculateAssessmentSectionCategories(CalculateAssessmentSectionCategoriesInput input);

        /// <summary>
        /// Calculates category boundaries (probabilities) for usages in assembly of WBI2017 assessment results on failure mechanism level. 
        /// This method implements "WBI-1-1" from the Functional Design.
        /// </summary>
        /// <param name="input">The input for this method, consisting of an signaling standard, a lower boundary standard and probability distribution factor.</param>
        /// <returns>A collection of <see cref="FailureMechanismCategory"/> that contains all assembly categories with their boundaries.</returns>
        /// <exception cref="AssemblyToolKernelException">Thrown when inconsistent boundaries were calculated.</exception>
        CalculationOutput<FailureMechanismCategory[]> CalculateFailureMechanismCategories(CalculateFailureMechanismCategoriesInput input);

        /// <summary>
        /// Calculates category boundaries (probabilities) for usages in assembly of WBI2017 assessment results on failure mechanism level. 
        /// This method implements "WBI-0-1" from the Functional Design.
        /// </summary>
        /// <param name="input">The input for this method, consisting of an signaling standard, a lower boundary standard and probability distribution factor and an N-value that takes the length-effect into account.</param>
        /// <returns>A collection of <see cref="FailureMechanismCategory"/> that contains all assembly categories with their boundaries.</returns>
        /// <exception cref="AssemblyToolKernelException">Thrown when inconsistent boundaries were calculated.</exception>
        CalculationOutput<FailureMechanismSectionCategory[]> CalculateFailureMechanismSectionCategories(CalculateFailureMechanismSectionCategoriesInput input);

        /// <summary>
        /// Calculates category boundaries (probabilities) for usages in assembly of WBI2017 assessment results on failure mechanism level. 
        /// This method implements "WBI-0-2" from the Functional Design.
        /// </summary>
        /// <param name="input">The input for this method, consisting of an signaling standard, a lower boundary standard and probability distribution factor and an N-value that takes the length-effect into account.</param>
        /// <returns>A collection of <see cref="FailureMechanismCategory"/> that contains all assembly categories with their boundaries.</returns>
        /// <exception cref="AssemblyToolKernelException">Thrown when inconsistent boundaries were calculated.</exception>
        CalculationOutput<FailureMechanismSectionCategory[]> CalculateGeotechnicFailureMechanismSectionCategories(CalculateFailureMechanismSectionCategoriesInput input);
    }
}