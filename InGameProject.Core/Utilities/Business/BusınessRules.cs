using InGameProject.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace InGameProject.Core.Utilities.Business
{
    public class BusınessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;
                }
            }

            return null;
        }

    }
}
