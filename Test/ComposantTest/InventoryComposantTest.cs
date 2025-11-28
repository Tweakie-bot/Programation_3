using NUnit.Framework;
using Programation_3_DnD.Composants;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Output;
using System;

namespace Test.ComposantTest
{
    public class InventoryComposantTest
    {
        private IOutput _renderer;
        private InventoryComposant _inventory;
        private ItemComposant _gold;
        private ItemComposant _potion;

        [SetUp]
        public void Setup()
        {
            _renderer = new OutputManagerForTests();
            _inventory = new InventoryComposant(_renderer);

            _gold = new ItemComposant("Gold", 1);
            _potion = new ItemComposant("Potion de soin", 10);
        }

        //
        [Test]
        public void TryAddItem()
        {
            _inventory.Add(_gold, 5);

            Assert.AreEqual(5, _inventory.GetCount("Gold"));
        }

        [Test]
        public void TryAddNullItem()
        {
            _inventory.Add(null, 5);

            Assert.AreEqual(0, _inventory.GetItemCount());
        }

        [Test]
        public void TryAddNegativeCount()
        {
            _inventory.Add(_gold, -3);

            Assert.AreEqual(0, _inventory.GetItemCount());
        }

        //
        [Test]
        public void TryAddByName()
        {
            _inventory.Add(_gold, 5);
            bool result = _inventory.AddByName("Gold", 3);

            Assert.IsTrue(result);
            Assert.AreEqual(8, _inventory.GetCount("Gold"));
        }

        [Test]
        public void TryAddByNameItemNotFound()
        {
            bool result = _inventory.AddByName("Sword", 2);

            Assert.IsFalse(result);
        }

        //
        [Test]
        public void TryRemoveByName()
        {
            _inventory.Add(_potion, 3);
            bool result = _inventory.RemoveByName("Potion de soin", 1);

            Assert.IsTrue(result);
            Assert.AreEqual(2, _inventory.GetCount("Potion de soin"));
        }

        [Test]
        public void TryRemoveByNameTooMuch()
        {
            _inventory.Add(_potion, 1);
            bool result = _inventory.RemoveByName("Potion de soin", 5);

            Assert.IsFalse(result);
            Assert.AreEqual(1, _inventory.GetCount("Potion de soin"));
        }

        [Test]
        public void TryRemoveByNameUntilZero()
        {
            _inventory.Add(_potion, 1);
            _inventory.RemoveByName("Potion de soin", 1);

            Assert.AreEqual(0, _inventory.GetItemCount());
        }

        //
        [Test]
        public void TryRemoveAtIndex()
        {
            _inventory.Add(_gold, 5);
            bool result = _inventory.RemoveAtIndex(0, 3);

            Assert.IsTrue(result);
            Assert.AreEqual(2, _inventory.GetCount("Gold"));
        }

        [Test]
        public void TryRemoveAtInvalidIndex()
        {
            bool result = _inventory.RemoveAtIndex(10, 1);

            Assert.IsFalse(result);
        }

        //
        [Test]
        public void TryGetItemByName()
        {
            _inventory.Add(_gold, 5);

            ItemComposant item = _inventory.GetItemByName("Gold");

            Assert.AreEqual(_gold, item);
        }

        [Test]
        public void TryGetItemByIndex()
        {
            _inventory.Add(_potion, 2);

            ItemComposant item = _inventory.GetItemByIndex(0);

            Assert.AreEqual("Potion de soin", item.GetName());
        }

        [Test]
        public void TryGetCountByIndex()
        {
            _inventory.Add(_gold, 7);

            Assert.AreEqual(7, _inventory.GetCountByIndex(0));
        }

        [Test]
        public void TryGetCountUnknownItem()
        {
            Assert.AreEqual(0, _inventory.GetCount("Ghost Item"));
        }

        //
        [Test]
        public void TryRenderEmptyInventory()
        {
            _inventory.Render();
            Assert.Pass();
        }

        [Test]
        public void TryRenderWithItems()
        {
            _inventory.Add(_potion, 2);
            _inventory.Render();
            Assert.Pass();
        }

        //
        [Test]
        public void TryProcessInputNoCrash()
        {
            _inventory.ProcessInput(ConsoleKey.A);
            Assert.Pass();
        }

        [Test]
        public void TryUpdateNoCrash()
        {
            _inventory.Update();
            Assert.Pass();
        }

        [Test]
        public void TryFixedUpdateNoCrash()
        {
            _inventory.FixedUpdate(2f);
            Assert.Pass();
        }
    }
}
