
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;

namespace blackfriday_bingo
{
    static class RandomExtensions
    {
        public static IEnumerable<T> Shuffle<T> (this IEnumerable<T> source, Random rng=null)
        {
            rng = rng ?? new Random();

            var array = source.ToArray();
            var n = array.Length;
            while (n > 1)
            {
                var k = rng.Next(n--);
                var temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }

            return array;
        }
    }
}