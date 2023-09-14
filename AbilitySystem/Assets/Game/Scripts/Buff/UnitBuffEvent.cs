using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForBuff
{
    public static class UnitBuffEvent
    {
        public static event Action onBuffSpeed;
        public static void OnBuffSpeed()
        {
            if (onBuffSpeed != null)
                onBuffSpeed();
        }

        public static event Action onBuffSize;
        public static void OnBuffSize()
        {
            if (onBuffSize != null)
                onBuffSize();
        }
    }
}