public class OverManager : FlashingManager
{
    public override void SetFlashing()
    {
        StageManager.INSTANCE.OverAction += () =>
        {
            _flashingObject = Instantiate(_flashingCanvas, this.transform).gameObject;
        };
    }
}