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
using System.Collections.Generic;

namespace AssemblyTool.Kernel.ErrorHandling
{
    public class AssemblyToolKernelException : Exception
    {
        public AssemblyToolKernelException(ErrorCode errorCode, AssemblyToolKernelException innerexception) : base(errorCode.GetMessage(), innerexception)
        {
            Code = new[] {errorCode};
        }

        public AssemblyToolKernelException(ErrorCode errorCode) : base(errorCode.GetMessage())
        {
            Code = new[] {errorCode};
        }

        public AssemblyToolKernelException(ErrorCode[] errorCodes, AssemblyToolKernelException innerexception) : base("Meerdere fouten zijn opgetreden", innerexception)
        {
            Code = errorCodes;
        }

        public AssemblyToolKernelException(ErrorCode[] errorCodes) : base("Meerdere fouten zijn opgetreden")
        {
            Code = errorCodes;
        }

        public ErrorCode[] Code { get; }
    }
}