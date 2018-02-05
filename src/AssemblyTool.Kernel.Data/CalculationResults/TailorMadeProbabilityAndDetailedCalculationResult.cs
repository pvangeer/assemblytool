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

using AssemblyTool.Kernel.ErrorHandling;

namespace AssemblyTool.Kernel.Data.CalculationResults
{
    /// <summary>
    /// This class is needed to specify the registered result for failure mechanisms that allow registration of all options for the simple assessment, detailed assessment and also a probability. 
    /// The result is a <see cref="TailorMadeProbabilityAndDetailedCalculationResultGroup"/>. In case the specified result is a probability, the probability should also be provided.
    /// </summary>
    public class TailorMadeProbabilityAndDetailedCalculationResult
    {
        /// <summary>
        /// Use this constructor for all results except probabilities.
        /// </summary>
        /// <param name="resultGroup">The specified calculation result.</param>
        public TailorMadeProbabilityAndDetailedCalculationResult(TailorMadeProbabilityAndDetailedCalculationResultGroup resultGroup)
        {
            if (resultGroup == TailorMadeProbabilityAndDetailedCalculationResultGroup.Probability)
            {
                throw new AssemblyToolKernelException(ErrorCode.NoProbabilityAllowedInConstructor);
            }

            CalculationResultGroup = resultGroup;
        }

        /// <summary>
        /// Use this constructor to specify a probability result.
        /// </summary>
        /// <param name="probability">The specified probability.</param>
        public TailorMadeProbabilityAndDetailedCalculationResult(Probability probability)
        {
            Probability = probability;
            CalculationResultGroup = TailorMadeProbabilityAndDetailedCalculationResultGroup.Probability;
        }

        /// <summary>
        /// The qualitative result. In case this returns <see cref="TailorMadeProbabilityAndDetailedCalculationResultGroup.Probability"/>, use the <see cref="Probability"/> property of this class.
        /// </summary>
        public TailorMadeProbabilityAndDetailedCalculationResultGroup CalculationResultGroup { get; }

        /// <summary>
        /// The probability as a result of a calculation. This property is only relevant in case <see cref="CalculationResultGroup"/> equals <see cref="TailorMadeProbabilityAndDetailedCalculationResultGroup.Probability"/>.
        /// </summary>
        public Probability Probability { get; }
    }
}
