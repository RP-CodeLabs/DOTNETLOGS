using System;
using System.ComponentModel.DataAnnotations;

namespace DOTNET.LOGS.Models.Extensions
{
    public class CustomDateAttribute : RangeAttribute
    {
        public CustomDateAttribute() : base(typeof(DateTime), DateTime.Now.AddDays(-7).ToShortDateString(), DateTime.Now.AddDays(1).ToShortDateString())
        {
            
        }
    }
}