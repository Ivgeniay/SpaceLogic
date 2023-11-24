using CodeBase.UI.UiDispose;
using System.Collections;
using UnityEngine;

namespace CodeBase
{
    internal class EngineDepended : MonoBehaviour
    {
        protected virtual IEnumerator Start()
        {
            yield return Engine.Instance.IsLoaded;
        }
    }
}
