using CodeBase.UI.UiDispose;
using System.Collections;
using UnityEngine;

namespace CodeBase.UI.BtnCallbacks
{
    internal class BaseBtnBehaviour : EngineDepended
    {
        [SerializeField] protected GameObject source;
        protected UiDisposer uiDisposer;

        protected override IEnumerator Start()
        {
            yield return base.Start();
            uiDisposer = Engine.Instance.GetService<UiDisposer>();
        }

        public virtual void OnClick() { }
    }
}
