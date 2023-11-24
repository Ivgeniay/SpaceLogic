namespace CodeBase.GameState
{
    internal interface IGSDepended
    {
        public void OnGameStateChanged(GSType prev, GSType curr);
    }
}
