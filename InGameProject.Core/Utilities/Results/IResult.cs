﻿using System;
using System.Collections.Generic;
using System.Text;

namespace InGameProject.Core.Utilities.Results
{
    //Temel voidler için
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
