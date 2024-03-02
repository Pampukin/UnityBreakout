public class PauseManager : FlashingManager
{
    public override void SetFlashing()
    {
        StageManager.INSTANCE.PauseAction += () => { _flashingObject = Instantiate(_flashingCanvas,this.transform).gameObject; };
        StageManager.INSTANCE.ResumeAction += () =>
        {
            if (_flashingObject != null)
            {
                Destroy(_flashingObject);
            }
        };
    }
}
