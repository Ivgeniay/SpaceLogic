using CodeBase.UI.UiDispose;
using CodeBase.UI.UILayout.UiDispose;
using System.Collections;
using UnityEngine;

namespace CodeBase.UI.UILayout
{
    internal class UILayout : EngineDepended
    {
        [SerializeField] protected UILayoutType type;
        [SerializeField] protected GameObject root;

        protected override IEnumerator Start()
        {
            yield return base.Start();
            Engine.Instance.GetService<UiDisposer>().RegisterLayout(this);
        }

        public UILayoutType GetLayoutType() => type;
        public void Enable() => root?.SetActive(true);
        public void Disable() => root?.SetActive(false);
        
    }
}
