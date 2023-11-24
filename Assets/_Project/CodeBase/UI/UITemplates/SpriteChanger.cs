using System;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

namespace CodeBase.UI.UITemplates
{
    internal class SpriteChanger : MonoBehaviour
    {
        [SerializeField] private ProceduralImage image;

        [SerializeField] private Sprite OnSprite;
        [SerializeField] private Sprite OffSprite;

        public void SpriteChange(bool value)
        {
            if (value) image.sprite = OnSprite;
            else image.sprite = OffSprite;
        }
    }
}
