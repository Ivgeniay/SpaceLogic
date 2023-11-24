using CodeBase.GameState;
using System.Collections;
using UnityEngine;

namespace CodeBase.UI.GamePlay
{
    internal class Moves : MonoBehaviour, IGSDepended
    {
        [SerializeField] private GameObject root;
        private GSService gSService;

        private IEnumerator Start()
        {
            gSService = Engine.Instance.GetService<GSService>();
            gSService.Register(this);
            yield return Engine.Instance.IsLoaded;
        }

        public void OnGameStateChanged(GSType prev, GSType curr)
        {
            switch(curr)
            {
                case GSType.Loading:
                    root.SetActive(false);
                    break;

                case GSType.Menu: 
                    root.SetActive(false);
                    break;

                case GSType.GamePlay: 
                    root.SetActive(true);
                    break;

                case GSType.Pause: 
                    root.SetActive(true);
                    break;
            }
        }
    }
}
