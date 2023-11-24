using CodeBase.UI.UILayout.UiDispose;
using UnityEngine;

namespace CodeBase.UI.BtnCallbacks
{
    internal class MenuBtn : BaseBtnBehaviour
    {
        [SerializeField] private UILayoutType layoutType;
        public override void OnClick()
        {
            base.OnClick();
            uiDisposer.ChangeLayout(layoutType);
        }
    }
}
