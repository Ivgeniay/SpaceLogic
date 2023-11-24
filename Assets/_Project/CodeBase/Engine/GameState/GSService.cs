using System.Collections;
using System.Collections.Generic;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.GameState
{
    internal class GSService : MonoBehaviour, IService
    {
        [SerializeField] private GSType currentGS;
        public GSType CurrentGS { get => currentGS; }

        private List<IGSDepended> dependeds = new();

        public void ChangeGS(GSType gState)
        {
            var prevGS = CurrentGS;
            this.currentGS = gState;

            foreach (var item in dependeds)
                item.OnGameStateChanged(prevGS, gState);
        }

        public void Register(IGSDepended gSDepended)
        {
            if (!dependeds.Contains(gSDepended))
                dependeds.Add(gSDepended);
        }

        public void UnRegister(IGSDepended gSDepended)
        {
            if (dependeds.Contains(gSDepended))
                dependeds.Remove(gSDepended);
        }

        public IEnumerator Initialize() { yield return null; }
    }
}
