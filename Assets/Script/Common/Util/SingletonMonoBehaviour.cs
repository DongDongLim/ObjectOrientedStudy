using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Study.Core
{
    [DisallowMultipleComponent]
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    CreateInstance();
                }

                return _instance;
            }
        }

        protected bool isInitialized;

        private static void CreateInstance()
        {
            _instance = FindObjectOfType<T>();
            if (_instance == null)
            {
                var go = new GameObject(typeof(T).Name);
                _instance = go.AddComponent<T>();
            }

            if (!_instance.isInitialized)
            {
                _instance.Initialize();
                _instance.isInitialized = true;
            }
        }

        /// <summary>
        /// Awake
        /// </summary>
        protected abstract void Initialize();

        private void Awake()
        {
            if (_instance == null)
            {
                CreateInstance();
            }

            if (this != Instance)
            {
                DestroyImmediate(gameObject);
                return;
            }
        }
    }
}