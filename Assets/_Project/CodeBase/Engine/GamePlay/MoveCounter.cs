using UnityEngine;
using System;
using TMPro;

namespace CodeBase.GamePlay
{
    internal class MoveCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private string prefix = "MOVES: ";

        private string[] sourse = null;

        public void OnMove(int max, int curr)
        {
            if (sourse == null)
            {
                sourse = new string[max + 1];
                for (int i = 0; i < sourse.Length; i++)
                    sourse[i] = prefix + i;
            }
            text.text = sourse[curr];
        }
    }
}
