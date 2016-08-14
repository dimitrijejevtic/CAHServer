using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Generator
{
    public class CardIDGen
    {
        private static Random random = new Random();
        public static string Randomize(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}