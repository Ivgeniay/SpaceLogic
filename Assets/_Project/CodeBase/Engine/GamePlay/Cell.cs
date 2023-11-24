using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.GamePlay
{
    internal class Cell : MonoBehaviour
    {
        internal event Action<Cell> OnCellClick;

        [SerializeField] private Image iconImage;
        [SerializeField] private Sprite empty;


        internal event Action<Cell> OnHideComplete;
        internal event Action<Cell> OnShowComplete;
        [Header("Animation")]
        [SerializeField] private float hideDuration = 0.65f;
        [SerializeField] private Ease hideEase;
        [SerializeField] private Vector3 hideAngle = new Vector3(0, 90, 0);
        [SerializeField] private Vector3 showAngle = new Vector3(0, 0, 0);

        private Quaternion startRotation; 
        private Tweener anim = null;

        internal bool IsShowed { get; private set; }
        internal bool IsChecked = false;
        internal bool IsAnimationContinue = false;
        internal Sprite Sprite { get => iconImage.sprite; }

        private void Awake()
        {
            startRotation = Quaternion.Euler(hideAngle);
        }

        internal void SetIcon(Sprite sprite)
        {
            iconImage.sprite = sprite;
        }
        
        internal void Rotate()
        {
            if (IsChecked) return;
            if (IsAnimationContinue) return;
            if (IsShowed) Hide();
            else Show();
        }
        internal void Show()
        {
            if (IsShowed || IsAnimationContinue) return;

            IsAnimationContinue = true;
            if (anim != null) anim.Kill();
            
            iconImage.rectTransform.DORotate(showAngle, hideDuration)
                .SetEase(hideEase)
                .OnComplete(() =>
                {
                    OnShowComplete?.Invoke(this);
                    IsShowed = true;
                    IsAnimationContinue = false;
                });
        }
        internal void Hide()
        {
            IsAnimationContinue = true;
            if (anim != null) anim.Kill();

            anim = iconImage.rectTransform.DORotate(hideAngle, hideDuration)
                .SetEase(hideEase)
                .OnComplete(() =>
                {
                    OnHideComplete?.Invoke(this);
                    IsShowed = false;
                    IsAnimationContinue= false;
                });
        }

        public void OnClick() =>
            OnCellClick?.Invoke(this);
        

        public void Dispose()
        {
            if (anim != null) anim.Kill();
            IsAnimationContinue = false;
            IsShowed = false;
            IsChecked = false;
            iconImage.transform.rotation = startRotation;
            iconImage.sprite = empty;
        }
    }
}
