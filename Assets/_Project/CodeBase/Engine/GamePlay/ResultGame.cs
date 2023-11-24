using CodeBase.GameState;
using CodeBase.Settings;
using CodeBase.UI.UiDispose;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.GamePlay
{
    internal class ResultGame : EngineDepended
    {
        [SerializeField] private TextMeshProUGUI resultText;
        [SerializeField] private TextMeshProUGUI movesText;

        [SerializeField] private Button backToMenuBtn;

        private GSService gsService;
        private UiDisposer uiDisposer;
        private SoundService soundService;
        protected override IEnumerator Start()
        {
            yield return base.Start();
            gsService = Engine.Instance.GetService<GSService>();
            uiDisposer = Engine.Instance.GetService<UiDisposer>();
            soundService = Engine.Instance.GetService<SoundService>();
        }
        private void Awake()
        {
            backToMenuBtn.onClick.AddListener(() =>
            {
                gsService.ChangeGS(GSType.Menu);
                uiDisposer.ChangeLayout(UI.UILayout.UiDispose.UILayoutType.MainMenu);
            });
        }

        public void OnLose(int leftMoves, float leftTime)
        {
            resultText.text = "TRY AGAIN!";
            movesText.text = $"Moves left: {leftMoves}";
            soundService.PlayMusicResult(false);
        }
        public void OnWin(int didMoves, float leftTime)
        {
            resultText.text = "CONGRATULATIONS!";
            movesText.text = $"Moves did: {didMoves}";
            soundService.PlayMusicResult(true);
        }
    }
}
