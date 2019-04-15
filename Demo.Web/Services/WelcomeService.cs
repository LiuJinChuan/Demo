namespace Demo.Web
{
    public class WelcomeService : IWelcomeService
    {
        public string GetMessage()
        {
            return "Hello from IWelcome Service";
        }
    }
}