using System;

namespace FantasyLCS.Utilities
{
    public static class RNG
    {
        private static Random _rand = new Random();

        public static int Get(int lowerbound, int upperbound) {
            return _rand.Next(lowerbound, upperbound);
        }
    }
}
