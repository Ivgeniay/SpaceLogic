using CodeBase.UI.UILayout.UiDispose;
using System;
using UnityEngine;

namespace CodeBase.UI.BtnCallbacks
{
    internal class ExitBtnCallbacks : BaseBtnBehaviour
    {
        public void OnExit()
        {
            Application.Quit();
        }

        public void OnClose()
        {
            uiDisposer.ChangeLayout(UILayoutType.MainMenu);
        }
    }
}
