using NUnit.Framework;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.State;
using Programation_3_DnD.Output;
using System;

namespace Test.StateTest
{
    public class DoesNotTradeEntityStateTest
    {
        private IOutput _renderer;
        private DoesNotTradeEntityState _state;

        [SetUp]
        public void Setup()
        {
            _renderer = new OutputManagerForTests();
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
            Assert.DoesNotThrow(() => { _state.ProcessInput(ConsoleKey.A); });
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
