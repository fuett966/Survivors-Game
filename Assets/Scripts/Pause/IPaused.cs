public interface IPaused
{
    public bool IsPaused { get; set; }
    void OnPause();
    void OnResume();
}
