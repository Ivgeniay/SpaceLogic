using CodeBase.GameState;
using CodeBase.Services;
using System;
using System.Collections;
using UnityEngine;

namespace CodeBase
{
    internal class Engine : MonoBehaviour
    {
        internal bool IsLoaded { get; private set; }
        private static Engine instance;
        [SerializeField] private ServiceProvider serviceProvider;

        public static Engine Instance { get
            {
                if (instance == null)
                    instance = FindFirstObjectByType<Engine>();
                return instance;
            }
        }

        private void Awake()
        {
#if PLATFORM_ANDROID
            Application.targetFrameRate = 60;
#endif
            if (instance == null) instance = this;
            if (!serviceProvider) throw new NullReferenceException();
        }

        private IEnumerator Start()
        {
            yield return serviceProvider.Initialize();
            GSService gs = GetService<GSService>();
            yield return new WaitUntil(() => gs.CurrentGS == GSType.Menu);
            IsLoaded = true;
        }

        internal T GetService<T>() where T : IService => serviceProvider.GetService<T>();
    }
}
