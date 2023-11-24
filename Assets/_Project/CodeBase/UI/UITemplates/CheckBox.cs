using UnityEngine.Events;
using UnityEngine;
using System;
using System.Collections;
using CodeBase.Settings;

namespace CodeBase.UI.UITemplates
{
    internal class CheckBox : EngineDepended
    {
        public UnityEvent<bool> OnValueChanged;
        private SoundService soundService;

        private bool currentValue = true;


        protected override IEnumerator Start()
        {
            yield return base.Start();
            soundService = Engine.Instance.GetService<SoundService>();
        }

        public void ValueChange(bool value)
        {
            soundService.PlaySoundOnOff(value);
            OnValueChanged?.Invoke(value);
        }

        public void OnClick()
        {
            currentValue = !currentValue;
            ValueChange(currentValue);
        }
    }
}
