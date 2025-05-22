using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static Player Player { get; private set; }
    public static UICondition UICondition { get; private set; }

    public static void Add(Player player)
    {
        Player = player;
    }

    public static void Add(UICondition uiCondition)
    {
        UICondition = uiCondition;
    }
}