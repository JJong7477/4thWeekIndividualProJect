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
    
    public static Transform FindChild(Transform _parent, string _childName)
    {
        var findChild = TryFindChild(_parent, _childName);
        if (findChild == null) Debug.Log($"{_parent.name}에서 {_childName}라는 자식을 찾을 수 없음");

        return findChild;
    }

    private static Transform TryFindChild(Transform _parent, string _childName)
    {
        Transform findChild = null;

        for (int i = 0; i < _parent.childCount; i++)
        {
            var child = _parent.GetChild(i);
            findChild = child.name == _childName ? child : TryFindChild(child, _childName);
            if (findChild != null) break;
        }

        return findChild;
    }
}