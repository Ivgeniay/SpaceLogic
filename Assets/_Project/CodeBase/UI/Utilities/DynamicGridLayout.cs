using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Utilities
{
    [RequireComponent(typeof(GridLayoutGroup))]
    internal class DynamicGridLayout : MonoBehaviour
    {
        private GridLayoutGroup gridLayoutGroup;

        public Vector2 cellSizeFor3040 = new Vector2(290, 290);
        private int screenHeight;

        void Start()
        {
            screenHeight = Screen.height;
            gridLayoutGroup = GetComponent<GridLayoutGroup>();
        }

        private void Update()
        {
            if (screenHeight != Screen.height)
            {
                screenHeight = Screen.height;
                AdaptGridLayout(screenHeight);
            }
        }

        void AdaptGridLayout(int screenHeight)
        {
            float scaleFactor = screenHeight / 3040f;
            gridLayoutGroup.cellSize = new Vector2(cellSizeFor3040.x * scaleFactor, cellSizeFor3040.y * scaleFactor);
        }
    }
}
