using System.Numerics;
using Programmation_3;

namespace TestProject1
{
    public class Tests
    {
        // === LOCATION TESTS ===
        [Test]
        public void Location_ShouldConnectBothWays()
        {
            GameObject location_a = new Location("A", "desc", new OutputRenderer());
            GameObject location_b = new Location("B", "desc", new OutputRenderer());

            (a as Location).ConnectALocation(b as Location);

            Assert.Contains(b, ((Location)a).GetLocationList());
            Assert.Contains(a, (b as Location).GetLocationList());
        }

        [Test]
        public void Location_CanWork_And_CanTrade_Flags_ShouldBeSetProperly()
        {
            Location location = new Location("Market", "desc", new OutputRenderer());
            location.SetCanWork(true);
            location.SetCanTrade(true);

            Assert.IsTrue(location.CanWork());
            Assert.IsTrue(location.GetCanTrade());
        }

        [Test]
        public void Location_UpdateLocation_InvalidIndex_ReturnsSame()
        {
            Location location = new Location("A", "desc", new OutputRenderer());
            Location result = location.UpdateLocation(9);
            Assert.AreEqual(location, result);
        }

        // === INVENTORY TESTS ===
        [Test]
        public void Inventory_AddItem_IncreasesCount()
        {
            InventoryComponent inventory = new InventoryComponent(new OutputRenderer());
            Item item = new Item("Gold", "Currency", 5);

            inventory.Add(item);
            Assert.AreEqual(1, inventory.GetCount());
        }

        [Test]
        public void Inventory_AddQuantity_ShouldIncreaseExistingItem()
        {
            InventoryComponent inventory = new InventoryComponent(new OutputRenderer());
            inventory.Add(new Item("Gold", "Currency", 5));
            inventory.AddQuantity("Gold", 10);

            Item gold = inventory.GetItem("Gold");
            Assert.AreEqual(15, gold.GetCount());
        }

        [Test]
        public void Inventory_Contains_ReturnsTrue_WhenItemExists()
        {
            InventoryComponent inventory = new InventoryComponent(new OutputRenderer());
            inventory.Add(new Item("Apple", "Food", 2));
            Assert.IsTrue(inventory.Contains("Apple"));
        }

        // === EVENT MANAGER TESTS ===
        [Test]
        public void EventManager_TriggerEvent_CallsHandler()
        {
            EventManager manager = new EventManager();
            bool is_called = false;

            manager.RegisterToEvent<GainMoneyEvent>(e => is_called = true);
            manager.TriggerEvent(new GainMoneyEvent(null, 50));

            Assert.IsTrue(is_called);
        }

        // === PLAYER TESTS ===
        [Test]
        public void Player_HasInventory_And_Location()
        {
            Location location = new Location("Start", "desc", new OutputRenderer());
            Player player = new Player("Hero", new LocationComponent(location));
            player.AddComponent(new InventoryComponent(new OutputRenderer()));

            Assert.NotNull(player.GetComponent<LocationComponent>());
            Assert.NotNull(player.GetComponent<InventoryComponent>());
        }

        // === QUIT GAME EVENT TESTS ===
        [Test]
        public void QuitGameEventManager_ShouldSetFlag_WhenTriggered()
        {
            var event_manager = new EventManager();
            var quit_manager = new QuitGameEventManager(event_manager, new OutputRenderer());

            event_manager.TriggerEvent(new GameOverEvent(" "));
            Assert.IsTrue(quit_manager.GetShouldQuitGame());
        }


        // === CHARACTER / PNJ TESTS ===
        [Test]
        public void Pnj_ShouldHaveInventory()
        {
            Location location = new Location("Cloister", "desc", new OutputRenderer());
            Pnj mya = new Pnj("Mya", new LocationComponent(location));
            mya.AddComponent(new InventoryComponent(new OutputRenderer()));

            Assert.NotNull(mya.GetComponent<InventoryComponent>());
        }

        // === GAME MANAGER BASIC TEST ===
        [Test]
        public void GameManager_ShouldInitializeSystems()
        {
            GameManager game_manager = new GameManager(new OutputRenderer());
            GainMoneyAchievementManager money_anager = game_manager.GetMoneyAchievementManager();
            QuitGameEventManager quit_anager = game_manager.GetQuitGameEventManager();

            Assert.NotNull(money_anager);
            Assert.NotNull(quit_anager);
        }
    }
}