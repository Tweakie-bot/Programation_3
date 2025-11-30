using Programation_3_DnD_Core;
using Programation_3_DnD_Console;

namespace Test.StateTest
{
    public class DoesNotTradeEntityStateTest
    {
        private IOutput _renderer;
        private InputProcessor _inputProcessor;
        private DoesNotTradeEntityState _state;

        [SetUp]
        public void Setup()
        {
            _renderer = new OutputManagerForTests();
            _inputProcessor = new InputProcessor();

            _state = new DoesNotTradeEntityState(_renderer);
        }

        [Test]
        public void TryEnterDoesNotCrash()
        {
            Assert.DoesNotThrow(() => { _state.Enter(); });
        }

        [Test]
        public void TryExitDoesNotCrash()
        {
            Assert.DoesNotThrow(() => { _state.Exit(); });
        }

        [Test]
        public void TryProcessInputDoesNotCrash()
        {
            Assert.DoesNotThrow(() => 
            {
                _inputProcessor.ChangeLastKeyForTests(ConsoleKey.A);
                _state.TreatInput(_inputProcessor);
            });
        }

        [Test]
        public void TryUpdateDoesNotCrash()
        {
            Assert.DoesNotThrow(() => { _state.Update(); });
        }

        [Test]
        public void TryRenderDoesNotCrash()
        {
            Assert.DoesNotThrow(() => { _state.Render(); });
        }
    }
}
