using NUnit.Framework;
using Programation_3_DnD.Composants;
using Programation_3_DnD.Data;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using System;

namespace Test.ComposantTest
{
    public class ItemComposantTest
    {
        private ItemComposant _item;

        [SetUp]
        public void Setup()
        {
            _item = new ItemComposant("Sword", 25);
        }

        [Test]
        public void TryGetName()
        {
            Assert.AreEqual("Sword", _item.GetName());
        }

        [Test]
        public void TryGetPrice()
        {
            Assert.AreEqual(25, _item.GetPrice());
        }

        [Test]
        public void TryProcessInputNoCrash()
        {
            _item.ProcessInput(ConsoleKey.A);
            Assert.Pass();
        }

        [Test]
        public void TryUpdateNoCrash()
        {
            _item.Update();
            Assert.Pass();
        }

        [Test]
        public void TryFixedUpdateNoCrash()
        {
            _item.FixedUpdate(1f);
            Assert.Pass();
        }

        [Test]
        public void TryRenderNoCrash()
        {
            _item.Render();
            Assert.Pass();
        }
    }
}
