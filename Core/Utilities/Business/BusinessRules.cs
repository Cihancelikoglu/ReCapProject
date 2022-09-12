using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        //logic: iş kuralı demek
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.IsSuccess)
                {
                    return logic;
                }
            }
            return null;
        }
    }
}
