using Programation_3_DnD_Console;
using Programation_3_DnD_Core;

namespace Test.ComposantTest
{
    public class IDComposantTest
    {
        private IDComposant _idComposant;

        [SetUp]
        public void Setup()
        {
            _idComposant = new IDComposant("Dragon");
        }

        [Test]
        public void TryGetName()
        {
            Assert.AreEqual("Dragon", _idComposant.GetName());
        }

        [Test]
        public void TryProcessInputNoCrash()
        {
            _idComposant.TreatInput(new InputProcessor());
            Assert.Pass();
        }

        [Test]
        public void TryUpdateNoCrash()
        {
            _idComposant.Update();
            Assert.Pass();
        }

        [Test]
        public void TryFixedUpdateNoCrash()
        {
            _idComposant.FixedUpdate(1.5f);
            Assert.Pass();
        }

        [Test]
        public void TryRenderNoCrash()
        {
            _idComposant.Render();
            Assert.Pass();
        }
    }
}
