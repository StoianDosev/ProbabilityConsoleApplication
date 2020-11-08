using SlotMachine.Interfaces;

namespace SlotMachine
{
    public class SlotMashineApplication : ISlotMashineApplication
    {
        private readonly IMachine _machine;
        public SlotMashineApplication(IMachine machine)
        {
            _machine = machine;
        }

        public void Run()
        {
            _machine.ExecuteTurn();
        }
    }
}