namespace Assets.Scripts.Features.Events
{
    public interface IEventListener
    {
        void OnEvent(EventComponent e);
    }
}