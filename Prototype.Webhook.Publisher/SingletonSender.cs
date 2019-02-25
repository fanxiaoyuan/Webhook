namespace Prototype.WebHook.Publisher
{
    public class SingletonSender
    {
        private static SingletonSender _instance;

        public static SingletonSender Instance => _instance ?? (_instance = new SingletonSender());

        public IHookSender Sender { get; private set; }

        private SingletonSender() { }

        public void SetSender(IHookSender sender)
        {
            Sender = sender;
        }
    }
}
