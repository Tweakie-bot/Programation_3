using Programation_3_DnD_Core;
using Programation_3_DnD_Console;

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
            _item.TreatInput(new InputProcessor());
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
