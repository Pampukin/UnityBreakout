public class ClearManager : FlashingManager
{
    public override void SetFlashing()
    {
        StageManager.INSTANCE.ClearAction += () => { _flashingObject = Instantiate(_flashingCanvas,this.transform).gameObject; };
    }
}
