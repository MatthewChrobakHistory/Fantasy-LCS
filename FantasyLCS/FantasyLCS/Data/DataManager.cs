using FantasyLCS.Data.Models;
using FantasyLCS.Data.Models.Champions;
using System.Collections.Generic;

namespace FantasyLCS.Data
{
    public static class DataManager
    {
        public static List<BaseChampion> Champions = new List<BaseChampion>();
        public static Game Game = new Game(null, null, null, null);
    }
}
