using System;

namespace LoL
{
    public static class Rng
    {
        private static Random _rng = new Random();

        public static int Next(int min, int max) {
            return _rng.Next(min, max);
        }
    }
}
