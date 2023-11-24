using CodeBase.GameState;
using CodeBase.UI.UiDispose;
using CodeBase.UI.UILayout.UiDispose;
using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.UI.BtnCallbacks
{
    internal class BackBtn : BaseBtnBehaviour
    {
        private GSService gSService;
        protected override IEnumerator Start()
        {
            yield return base.Start();
            gSService = Engine.Instance.GetService<GSService>();
            uiDisposer.OnUILayoutChangedEvent += OnUILayoutChangedHandler;
        }

        private void OnUILayoutChangedHandler(UILayoutType obj)
        {
            //gSService.ChangeGS(GSType.Menu);

            switch (uiDisposer.CurrentUILayot)
            {
                case UILayoutType.Settings:
                    source.SetActive(true);
                    break;
                case UILayoutType.Rules:
                    source.SetActive(true);
                    break;
                case UILayoutType.Exit:
                    source.SetActive(true);
                    break;
                case UILayoutType.GamePlay:
                    source.SetActive(true);
                    break;

                default:
                    source.SetActive(false);
                    break;

            }
        }

        public override void OnClick()
        {
            switch (uiDisposer.CurrentUILayot) 
            {
                case UILayoutType.Loading:
                    uiDisposer.ChangeLayout(UILayoutType.MainMenu);
                    break;
                case UILayoutType.Settings:
                    if (gSService.CurrentGS == GSType.Pause)
                    {
                        uiDisposer.ChangeLayout(UILayoutType.GamePlay);
                        gSService.ChangeGS(GSType.GamePlay);
                    }
                    else uiDisposer.ChangeLayout(UILayoutType.MainMenu);
                    
                    break;
                case UILayoutType.Rules:
                    uiDisposer.ChangeLayout(UILayoutType.MainMenu);
                    break;
                case UILayoutType.GamePlay:
                    uiDisposer.ChangeLayout(UILayoutType.MainMenu);
                    gSService.ChangeGS(GSType.Menu);
                    break;
                case UILayoutType.Exit:
                    uiDisposer.ChangeLayout(UILayoutType.MainMenu);
                    break;
            }
        }

    }
}
