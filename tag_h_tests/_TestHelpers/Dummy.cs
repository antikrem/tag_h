﻿using System;
using System.Linq;

using tag_h.Helper.Extensions;


namespace tag_h_tests._TestHelpers
{
    static class Dummy
    {
        private static Random _random = new Random();

        public static int Int(int start = 0, int end = int.MaxValue) 
            => _random.Next(start, end);

        public static string String(int length = 32)
            => Enumerable.Range(0, length).Select(_ => (char)_random.Next('a', 'z')).Concat();
    }
}
