using System;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour 
{
    private static Dictionary<GameEvents, List<Action>> observer = new Dictionary<GameEvents, List<Action>>(); 

    public static void AddObserver(GameEvents name, Action callback)
    {
        if(!observer.ContainsKey(name))
        {
            observer.Add(name, new List<Action>());
        }

        observer[name].Add(callback);
    }

    public static void RemoveObserver(GameEvents name, Action callback)
    {
        if(!observer.ContainsKey(name))
        {
            return;
        }

        observer[name].Remove(callback);
    }

    public static void Notify(GameEvents name)
    {
        if(!observer.ContainsKey(name))
        {
            return;
        }

        foreach(var item in observer[name])
        {
            item?.Invoke();
        }
    }
}