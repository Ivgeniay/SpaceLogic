using CodeBase.Services;
using CodeBase.UI.UILayout;
using CodeBase.UI.UILayout.UiDispose;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI.UiDispose
{
    internal class UiDisposer : EngineDepended, IService
    {
        public event Action<UILayoutType> OnUILayoutChangedEvent;

        private List<UILayout.UILayout> uiLayouts = new();

        [SerializeField] private UILayoutType currentUILayot = UILayoutType.Loading;
        public UILayoutType CurrentUILayot { get => currentUILayot; private set => currentUILayot = value; }

        private void Awake()
        {
            uiLayouts.ForEach(e => e.Disable()); 
        }

        protected override IEnumerator Start()
        {
            yield return base.Start();
            ChangeLayout(currentUILayot);
        }

        public void RegisterLayout(UILayout.UILayout layout)
        {
            if (!uiLayouts.Contains(layout)) 
                uiLayouts.Add(layout);
        }

        public void ChangeLayout(int layoutType) => ChangeLayout((UILayoutType)layoutType);
        public void ChangeLayout(UILayoutType layoutType)
        {
            uiLayouts.ForEach(layout =>
            {
                CurrentUILayot = layoutType;
                if (layoutType == layout.GetLayoutType()) layout.Enable();
                else layout.Disable();
                OnUILayoutChangedEvent?.Invoke(layoutType);
            });
        }

        public IEnumerator Initialize() { yield return null; }
    }
}
