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

namespace AssemblyTool.Kernel.Data.AssessmentResults
{
    public enum TailorMadeCategoryAssessmentResult
    {
        /// <summary>
        /// Assembly category Iv. This category lies between a probability of 0 and 1/30 * f * signaling standard / N
        /// </summary>
        Iv = 1,

        /// <summary>
        /// Assembly category IIv. This category lies between a probability of 1/30 * f * signaling standard / N and f * signaling standard / N
        /// </summary>
        IIv = 2,

        /// <summary>
        /// Assembly category IIIv. This category lies between a probability of f * signaling standard / N and f * lower boundary standard / N
        /// </summary>
        IIIv = 3,

        /// <summary>
        /// Assembly category IVv. This category lies between a probability of f * lower boundary standard / N and lower boundary standard
        /// </summary>
        IVv = 4,

        /// <summary>
        /// Assembly category Vv. This category lies between a probability of lower boundary standard and 30 * lower boundary standard
        /// </summary>
        Vv = 5,

        /// <summary>
        /// Assembly category VIv. This category lies between a probability of 30 * lower boundary standard and 1
        /// </summary>
        VIv = 6,

        /// <summary>
        /// Assembly category VIIv. No result yet (NGO - Nog geen oordeel)
        /// </summary>
        VIIv = 7,

        /// <summary>
        /// NGO - Nog Geen Oordeel, No result yet
        /// </summary>
        NGO = 8,

        /// <summary>
        /// FV - Faalkans Verwaarloosbaar, probability neglectible
        /// </summary>
        FV = 9
    }
}
