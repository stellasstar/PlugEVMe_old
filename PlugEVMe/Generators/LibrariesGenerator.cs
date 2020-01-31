using System;
using System.Collections.Generic;

namespace PlugEVMe.Generators
{
    public class ReadLibraries
    {
        static string entries = "The London Library, London, UK, 51.507359, -0.136439 \n" +
                "The Seattle Central Library, Seattle, WA, USA, 47.606701, -122.332504 \n" +
                "The Library of the Arsenal, Paris, France, 48.853836, 2.363589 \n" +
                "The Library of Marsailles, Provence, France, 43.296398, 5.370000 \n" + 
                "The Hong Kong Central Library, Causeway Bay, Hong Kong Island, 22.279902, 114.189674 \n " +
                "The National library of Kazakhstan, Almaty, Kazakhstan, 43.238949, 76.889709 \n " +
                "The University of Tartu Library, Tartu, Estonia, 58.3770, 26.7213 \n" +
                "The National Library of Israel, Jerusalem, Israel, 31.7761, 35.1968";

        public List<string> libraries { get; } = new List<string>(entries.Split('\n'));

    }
}