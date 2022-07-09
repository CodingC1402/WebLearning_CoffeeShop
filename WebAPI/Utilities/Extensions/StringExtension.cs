using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Utilities.Extensions
{
    public static class StringExtension
    {
        public static string CapitalizeFirstLetter(this string str) {
            return String.Concat(Char.ToUpper(str[0]).ToString(), str.AsSpan(1));
        }
    }
}