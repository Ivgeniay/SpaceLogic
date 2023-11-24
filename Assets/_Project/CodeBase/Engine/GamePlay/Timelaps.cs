using System;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

namespace CodeBase.GamePlay
{
    internal class Timelaps : MonoBehaviour
    {
        [SerializeField] private ProceduralImage slider;
        [SerializeField] private Gradient gradient;

        public void OnTimer(float max, float curr)
        {
            if (curr> 0)
            {
                float value = curr / max;
                slider.fillAmount = value;
                slider.color = gradient.Evaluate(value);
            }
        }
    }
}
