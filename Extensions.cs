
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace BlackFridayBingo
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

        public static IServiceCollection UseNamespaceViewLocation(
            this IServiceCollection services,
            string prefix = null)
        {
            prefix = prefix ?? Assembly.GetCallingAssembly().GetName().Name;
            services.Configure<RazorViewEngineOptions>(config =>
            {
                config.ViewLocationFormats.Insert(0, "/{3}/{0}.cshtml");
                config.AreaViewLocationFormats.Insert(0, "/{3}/{0}.cshtml");
                config.ViewLocationExpanders.Insert(0, new NamespaceViewLocationExpander(prefix));
            });

            return services;
        }
    }
}