

namespace SlotMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicesContainer container = new ServicesContainer();
            container.RegisterServices();
            container.RunApplication();
            container.DisposeServices();            
        }
    }
}
