using CodeBase.GameState;
using CodeBase.UI.UiDispose;
using CodeBase.UI.UILayout.UiDispose;
using System.Collections;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

namespace CodeBase.Load
{
    internal class Loading : MonoBehaviour
    {
        [SerializeField] private ProceduralImage slider;
        private IEnumerator Start()
        {
            Engine.Instance.GetService<GSService>().ChangeGS(GSType.Loading);
            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(0.01f);
                slider.fillAmount = 1 - ((float)i / 100);
            }
            Engine.Instance.GetService<UiDisposer>().ChangeLayout(UILayoutType.MainMenu);
            Engine.Instance.GetService<GSService>().ChangeGS(GSType.Menu);
        }
    }
}
