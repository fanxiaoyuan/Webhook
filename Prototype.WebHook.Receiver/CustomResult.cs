namespace Prototype.WebHook.Receiver
{
    public class CustomResult
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return $"{nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}";
        }
    }
}
