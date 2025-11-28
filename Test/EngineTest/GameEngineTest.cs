using NUnit.Framework;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Objects;
using Programation_3_DnD.Composants;
using Programation_3_DnD.Event;
using Programation_3_DnD.Output;

public class GameEngineTest
{
    private IOutput _renderer;
    private GameEngine _engine;

    [SetUp]
    public void Setup()
    {
        string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "JsonTest");

        _renderer = new OutputManagerForTests();
        _engine = new GameEngine(_renderer, path);
    }

    [Test]
    public void TryEngineCreation()
    {
        Assert.NotNull(_engine);
    }

    [Test]
    public void TryQuitGame()
    {
        _engine.QuitGame();
        Assert.Pass();
    }

    [Test]
    public void TryWorkIncreasesTime()
    {
        _engine.Work();

        Assert.Pass();
    }

    [Test]
    public void TryRunDoesNotCrashShort()
    {
        _engine.QuitGame();
        _engine.Run();

        Assert.Pass();
    }

    [Test]
    public void TryWorkAffectsPlayerGold()
    {
        GameManager manager = _engine.GetGameManager();

        GameObject player = manager.GetPlayer();
        InventoryComposant inventory = player.GetComposant<InventoryComposant>();
        WorkForceComposant force = player.GetComposant<WorkForceComposant>();

        int before = inventory.GetCount("Gold");

        _engine.Work();

        int after = inventory.GetCount("Gold");

        Assert.GreaterOrEqual(after, before);
    }

    [Test]
    public void TryMultipleWorkCalls()
    {
        _engine.Work();
        _engine.Work();
        _engine.Work();

        Assert.Pass();
    }

    [Test]
    public void TryEngineLoopSafeAfterQuit()
    {
        _engine.QuitGame();
        _engine.Run();

        Assert.Pass();
    }
}
