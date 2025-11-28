using NUnit.Framework;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.State;
using System;

namespace Test.StateTest
{
    public class DoesNotProposeWorkEntityStateTest
    {
        private IOutput _renderer;
        private DoesNotProposeWorkEntityState _state;

        [SetUp]
        public void Setup()
        {
            _renderer = new OutputManagerForTests();
            _state = new DoesNotProposeWorkEntityState(_renderer);
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
        public void TryFixedUpdateDoesNotCrash()
        {
            Assert.DoesNotThrow(() => { _state.FixedUpdate(); });
        }

        [Test]
        public void TryRenderDoesNotCrash()
        {
            Assert.DoesNotThrow(() => { _state.Render(); });
        }
    }
}
