using NUnit.Framework;
using Programation_3_DnD;
using Programation_3_DnD.Composants;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Event;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Objects;
using Programation_3_DnD.Output;
public class WorkEventTest
{
    private IOutput _renderer;
    private GameEngine _engine;
    private EventManager _eventManager;

    private GameObject _player;
    private WorkEvent _workEvent;

    [SetUp]
    public void Setup()
    {
        string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "JsonTest");

        _renderer = new OutputManagerForTests();
        _engine = new GameEngine(_renderer, path);
        _eventManager = _engine.GetEventManager();

        GameManager manager = _engine.GetGameManager();
        _player = manager.GetPlayer();

        InventoryComposant inventory = _player.GetComposant<InventoryComposant>();
        WorkForceComposant work_force = _player.GetComposant<WorkForceComposant>();


        _workEvent = new WorkEvent(_engine, _player, 10, _renderer, _eventManager);
    }

    [Test]
    public void TryUpdateDoesNotCrash()
    {
        _workEvent.Update();
        Assert.Pass();
    }

    [Test]
    public void TryGoldIncrease()
    {
        InventoryComposant inventory = _player.GetComposant<InventoryComposant>();
        int gold_count = inventory.GetCount("Gold");

        _workEvent.Update();

        Assert.Greater(inventory.GetCount("Gold"), gold_count);
    }

    [Test]
    public void TryEventCompleted()
    {
        _workEvent.Update();
        Assert.IsTrue(_workEvent.GetIsCompleted());
    }

    [Test]
    public void TryRenderEventQueued()
    {
        _workEvent.Update();

        _eventManager.Update();

        Assert.Pass();
    }
}
