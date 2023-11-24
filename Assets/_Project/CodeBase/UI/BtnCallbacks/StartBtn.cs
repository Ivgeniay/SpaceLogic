using CodeBase.GameState;
using System.Collections;
using UnityEngine;

namespace CodeBase.UI.BtnCallbacks
{
    internal class StartBtn : BaseBtnBehaviour
    {
        private GSService gsService;
        protected override IEnumerator Start()
        {
            yield return base.Start();
            gsService = Engine.Instance.GetService<GSService>();
        }
        public override void OnClick()
        {
            base.OnClick();
            uiDisposer.ChangeLayout(UILayout.UiDispose.UILayoutType.GamePlay);
            gsService.ChangeGS(GSType.GamePlay);
        }
    }
}
