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

namespace AssemblyTool.Kernel.Data
{
    public enum AssessmentSectionAssemblyCategory
    {
        /// <summary>
        /// A+ category, this categories is between a probability of 0 and 1/30 times the signaling standard
        /// </summary>
        APlus,

        /// <summary>
        /// Assembly category A. This category lies between a probability of the 1/30 times the signaling standard and the signaling standard
        /// </summary>
        A,

        /// <summary>
        /// Assembly category B. This category lies between a probability of the signaling standard and the lower boundary standard
        /// </summary>
        B,

        /// <summary>
        /// Assembly category C. This category lies between a probability of the lower boundary standard and 30 times the lower boundary standard
        /// </summary>
        C,
        
        /// <summary>
        /// Assembly category D. This category lies is between a probability of 30 times the lower boundary standard and 1
        /// </summary>
        D
    }
}