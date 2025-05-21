using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class SingleTon<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (!_instance)
            {   string singletonName = typeof(T).Name;
                _instance = new GameObject(singletonName).AddComponent<T>();
            }return _instance;
        }

        set => _instance = value;
    }

    public virtual void Awake()
    {
        if (!_instance)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        
        
    }
    
}
