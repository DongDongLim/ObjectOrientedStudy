using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : class, new()
{
    public static T Instunce
    {
        private set { }

        get
        {
            if(_instunce == null)
            {
                CreateInstance();
            }

            return _instunce;
        }
    }

    private static T _instunce;

    private static void CreateInstance()
    {
        _instunce = new T();
    }

}
