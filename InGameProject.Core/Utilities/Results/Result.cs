using System;
using System.Collections.Generic;
using System.Text;

namespace InGameProject.Core.Utilities.Results
{
    public class Result : IResult
    {
        // Constructor'ları bu şeklilde yazdığın vakit
        // Business da kullanırken
        //return new Result(true,"Ürün eklendi..");  ve return new Result(true);   2 şekilde de kullanabilirsin.
        public Result(bool success, string message):this(success)
        {
            Message = message;
            
        }

        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
        public string Message { get; } 
    }
}
