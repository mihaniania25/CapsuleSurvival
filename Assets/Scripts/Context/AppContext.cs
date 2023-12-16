﻿using CapsuleSurvival.Core;
using CapsuleSurvival.Utility;
using CapsuleSurvival.Impl;

namespace CapsuleSurvival
{
    public static class AppContext
    {
        public static UnityConfigsProvider ConfigsProvider { get; private set; }
        public static GameContext GameContext { get; private set; }

        static AppContext()
        {
            ConfigsProvider = new UnityConfigsProvider();
            GameContext = new GameContext();
        }
    }
}