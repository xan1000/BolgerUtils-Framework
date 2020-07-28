using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.BolgerUtils.Framework.EnumDisplay
{
    public static class Utils
    {
        public static List<T> GetEnumValues<T>() where T : Enum => Enum.GetValues(typeof(T)).Cast<T>().ToList();
    }
}
