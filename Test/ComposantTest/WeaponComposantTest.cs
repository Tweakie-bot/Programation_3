using Programation_3_DnD_Core;

namespace Test.ComposantTest
{
    public class WeaponComposantTest
    {

        [Test]
        public void ConstructorAssignsCorrectValues()
        {
            WeaponComposant weapon = new WeaponComposant("Axe", 120, 30);

            Assert.AreEqual("Axe", weapon.GetName());
            Assert.AreEqual(120, weapon.GetPrice());
            Assert.AreEqual(30, weapon.GetDamage());
        }

        [Test]
        public void GetDamageReturnsExpectedValue()
        {
            WeaponComposant weapon = new WeaponComposant("Dagger", 25, 6);

            int result = weapon.GetDamage();

            Assert.AreEqual(6, result);
        }
    }
}
// Fait avec l'aide de chat gpt parce que je manquais de temps