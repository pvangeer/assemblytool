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

namespace AssemblyTool.Kernel.Data.CalculationResults
{
    public class DetailedCategoryBoundariesCalculationResult
    {
        /// <summary>
        /// This constructor accepts a detailed calculation result for each boundary between the assessment classes.
        /// </summary>
        /// <param name="iToII">Qualitative detailed calculation result for a probability of occurrance equal to the boundary between class Iv and IIv</param>
        /// <param name="iIToIII">Qualitative detailed calculation result for a probability of occurrance equal to the boundary between class IIv and IIIv</param>
        /// <param name="iIIToIV">Qualitative detailed calculation result for a probability of occurrance equal to the boundary between class IIIv and IVv</param>
        /// <param name="iVToV">Qualitative detailed calculation result for a probability of occurrance equal to the boundary between class IVv and Vv</param>
        /// <param name="vToVi">Qualitative detailed calculation result for a probability of occurrance equal to the boundary between class Vv and VIv</param>
        public DetailedCategoryBoundariesCalculationResult(DetailedCalculationResult iToII, DetailedCalculationResult iIToIII, DetailedCalculationResult iIIToIV, DetailedCalculationResult iVToV, DetailedCalculationResult vToVi)
        {
            ResultItoII = iToII;
            ResultIItoIII = iIToIII;
            ResultIIItoIV = iIIToIV;
            ResultIVtoV = iVToV;
            ResultVtoVI = vToVi;
        }

        /// <summary>
        /// Qualitative detailed calculation result for a probability of occurrance equal to the boundary between class Iv and IIv
        /// </summary>
        public DetailedCalculationResult ResultItoII { get; }

        /// <summary>
        /// Qualitative detailed calculation result for a probability of occurrance equal to the boundary between class IIv and IIIv
        /// </summary>
        public DetailedCalculationResult ResultIItoIII { get; }

        /// <summary>
        /// Qualitative detailed calculation result for a probability of occurrance equal to the boundary between class IIIv and IVv
        /// </summary>
        public DetailedCalculationResult ResultIIItoIV { get; }

        /// <summary>
        /// Qualitative detailed calculation result for a probability of occurrance equal to the boundary between class IVv and Vv
        /// </summary>
        public DetailedCalculationResult ResultIVtoV { get; }

        /// <summary>
        /// Qualitative detailed calculation result for a probability of occurrance equal to the boundary between class Vv and VIv
        /// </summary>
        public DetailedCalculationResult ResultVtoVI { get; }
    }
}