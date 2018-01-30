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

namespace AssemblyTool.Kernel.Data.AssessmentResults
{
    public class TailoreMadeProbabilityAssessmentResult
    {
        public TailoreMadeProbabilityAssessmentResult(TailorMadeProbabilisticAssessmentResult result)
        {
            if (result == TailorMadeProbabilisticAssessmentResult.Probability)
            {
                throw new AssemblyToolKernelException(ErrorCode.NoProbability);
            }

            AssessmentResult = result;
        }

        public TailoreMadeProbabilityAssessmentResult(Probability probability)
        {
            Probability = probability;
            AssessmentResult = TailorMadeProbabilisticAssessmentResult.Probability;
        }

        public TailorMadeProbabilisticAssessmentResult AssessmentResult { get; }

        public Probability Probability { get; }
    }
}
