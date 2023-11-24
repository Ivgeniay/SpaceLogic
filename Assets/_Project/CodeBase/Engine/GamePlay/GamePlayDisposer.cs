using System.Collections.Generic;
using CodeBase.UI.UiDispose;
using UnityEngine.Events;
using CodeBase.GameState;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.GamePlay
{
    internal class GamePlayDisposer : EngineDepended, IGSDepended
    {
        public UnityEvent<int, int> OnMove;
        public UnityEvent<float, float> OnTimer;
        public UnityEvent<int, float> OnWin;
        public UnityEvent<int, float> OnLose;

        [SerializeField] private Cell[] cells;
        [SerializeField] private Sprite[] icons;

        [SerializeField] private float timeToShow = 2f;
        [SerializeField] private float startGameTimer = 15f;
        [SerializeField] private int startMove = 30;

        internal int CurrentMove { get => currentMove; }

        private GSService gsService;
        private UiDisposer uiDisposer;
        private int currentMove;
        private float currentTimer;

        [SerializeField] private List<Cell> queue = new();

        private void Awake()
        {
            if (cells != null && cells.Length > 0)
                foreach (var item in cells)
                    item.OnCellClick += OnCellClickHandler;
        }

        protected override IEnumerator Start()
        {
            yield return base.Start();
            gsService = Engine.Instance.GetService<GSService>();
            gsService.Register(this);
            uiDisposer = Engine.Instance.GetService<UiDisposer>();
        }

        private void Update()
        {
            if (gsService == null) return;
            if (gsService.CurrentGS == GSType.GamePlay)
            {
                currentTimer -= Time.deltaTime;
                OnTimer?.Invoke(startGameTimer, currentTimer);
                CheckLose();
            }
        }

        public void OnGameStateChanged(GSType prev, GSType curr)
        {
            switch (curr)
            {
                case GSType.GamePlay:
                    if (prev == GSType.Menu) StartGameplay();
                    if (prev == GSType.Pause) Continue();
                    break;

                case GSType.Pause:
                    Pause();
                    
                    break;

                default:
                    foreach (var item in cells)
                        item.Dispose();
                    
                    break;
            }
        }
        public void Restart()
        {
            uiDisposer.ChangeLayout(UI.UILayout.UiDispose.UILayoutType.GamePlay);
            StartGameplay();
        }

        private void OnCellClickHandler(Cell obj)
        {
            if (currentMove <= 0) return;
            if (obj.IsChecked) return;
            if (obj.IsAnimationContinue) return;
            if (obj.IsShowed) return;

            currentMove -= 1;
            OnMove?.Invoke(startMove, currentMove);

            obj.Show();

            obj.OnShowComplete += OnCellShowComplete;
            obj.OnHideComplete += OnCellHideComplete;
        }

        private void OnCellHideComplete(Cell obj)
        {
            obj.OnShowComplete -= OnCellShowComplete;
            obj.OnHideComplete -= OnCellHideComplete;
        }
        private void OnCellShowComplete(Cell obj)
        {
            queue.Add(obj);
            int index = queue.IndexOf(obj);
            if (index != 0 )//&& index % 2 != 0)
                CompareCells(obj, queue[index - 1]);
        }

        private void CompareCells(Cell firstClick, Cell secondClick)
        {
            if ((firstClick && secondClick) && (!firstClick.Equals(secondClick)))
            {
                if (firstClick.Sprite == secondClick.Sprite)
                {
                    //Debug.Log("Check!");
                    firstClick.IsChecked = true;
                    secondClick.IsChecked = true;
                    CheckingWin();
                }
                else
                {
                    //Debug.Log("NotCheck!");
                    firstClick.Hide();
                    secondClick.Hide();
                }
            }
            queue.Remove(firstClick);
            queue.Remove(secondClick);
            CheckLose();
        }

        private void CheckingWin()
        {
            if (gsService.CurrentGS != GSType.GamePlay) return;
            bool isWin = cells.All(cell => cell.IsChecked == true);
            if (isWin)
            {
                gsService.ChangeGS(GSType.Win);
                uiDisposer.ChangeLayout(UI.UILayout.UiDispose.UILayoutType.Result);
                OnWin?.Invoke(startMove - currentMove, startGameTimer - currentTimer);
                //Debug.Log("GPDisposer Win");
            }
        }
        private void CheckLose()
        {
            if (gsService.CurrentGS != GSType.GamePlay) return;

            if (currentMove <= 0 || currentTimer <= 0)
            {
                gsService.ChangeGS(GSType.Lose);
                uiDisposer.ChangeLayout(UI.UILayout.UiDispose.UILayoutType.Result);
                OnLose?.Invoke(currentMove, currentTimer);
                //Debug.Log("GPDisposer Lose");
            }
        }
        private void Pause()
        {
            Debug.Log("GPDisp pause");
        }

        private void StartGameplay()
        {
            queue = new();
            foreach (Cell item in cells)
            {
                item.OnHideComplete -= OnCellHideComplete;
                item.OnShowComplete -= OnCellShowComplete;
                item.Dispose();
            }

            currentMove = startMove;
            OnMove?.Invoke(startMove, currentMove);
            currentTimer = startGameTimer;
            gsService.ChangeGS(GSType.GamePlay);

            InitializeGame();
        }

        private void Continue()
        {
            gsService.ChangeGS(GSType.GamePlay);
        }

        private void InitializeGame()
        {
            List<Sprite> pairedIcons = new List<Sprite>(cells.Length);
            for (int i = 0; i < cells.Length; i+=2)
            {
                Sprite str = icons[Random.Range(0, icons.Length)];
                pairedIcons.Add(str);
                pairedIcons.Add(str);
            }

            ShuffleList(pairedIcons);

            for (int i = 0; i < cells.Length; i++)
                cells[i].SetIcon(pairedIcons[i]);
        }
        private void ShuffleList<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
