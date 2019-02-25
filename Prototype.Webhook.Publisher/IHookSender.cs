namespace Prototype.WebHook.Publisher
{
    public interface IHookSender
    {
        void LaunchHook(string url, string secret, string action, object data);
    }
}
