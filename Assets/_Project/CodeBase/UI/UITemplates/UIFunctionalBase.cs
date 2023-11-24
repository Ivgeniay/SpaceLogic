using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.UI.UITemplates
{
    internal abstract class UIFunctionalBase : MonoBehaviour
    {
        public UnityEvent<object> OnValueChanged;
        public abstract void ValueChange(object value);
    }
}
