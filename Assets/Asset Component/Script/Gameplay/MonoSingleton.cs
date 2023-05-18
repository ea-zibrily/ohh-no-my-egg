using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // Kalau instance kosong, maka script akan mencari object lain
                // yang memiliki tipe atau class yang sama dengan target
                    
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    // Jika masih kosong, maka script akan membuat gameObject baru
                    // dengan class yang sama dengan target
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            // jika instance masih kosong, derived class (class yang ngeextend MonoSingleton)
            // akan di assign ke instance
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
