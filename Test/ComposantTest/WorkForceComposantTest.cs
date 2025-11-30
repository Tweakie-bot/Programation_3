using Programation_3_DnD_Core;

namespace Test.ComposantTest
{
    public class WorkForceComposantTest
    {
        private WorkForceComposant _workForce;

        [SetUp]
        public void Setup()
        {
            _workForce = new WorkForceComposant();
        }

        [Test]
        public void InitialValue_IsZero()
        {
            Assert.AreEqual(0, _workForce.GetWorkForceValue());
        }

        [Test]
        public void ApplyWorkForceGain_IncreasesValue()
        {
            _workForce.ApplyWorkForceGain(5);

            Assert.AreEqual(5, _workForce.GetWorkForceValue());
        }

        [Test]
        public void ApplyWorkForceGain_AccumulatesProperly()
        {
            _workForce.ApplyWorkForceGain(3);
            _workForce.ApplyWorkForceGain(7);

            Assert.AreEqual(10, _workForce.GetWorkForceValue());
        }

        [Test]
        public void ApplyWorkForceGain_ReturnsNewValue()
        {
            int result = _workForce.ApplyWorkForceGain(4);

            Assert.AreEqual(4, result);
        }
    }
}
// Fait par chat gpt