using CodeBase.GameState;
using CodeBase.UI.BtnCallbacks;
using System.Collections;

namespace CodeBase.UI.BtnCallbacks
{
    internal class GamePlaySettingsBtn : BaseBtnBehaviour, IGSDepended
    {
        GSService gsService;
        protected override IEnumerator Start()
        {
            yield return base.Start();
            gsService = Engine.Instance.GetService<GSService>();
            gsService.Register(this);
        }

        public override void OnClick()
        {
            base.OnClick();
            gsService.ChangeGS(GSType.Pause);
            uiDisposer.ChangeLayout(UILayout.UiDispose.UILayoutType.Settings);
        }

        public void OnGameStateChanged(GSType prev, GSType curr)
        {
            switch (curr)
            {
                case GSType.GamePlay:
                    source.SetActive(true);
                    break;
                
                default: 
                    source.SetActive(false);
                    break;
            }
        }
    }
}
