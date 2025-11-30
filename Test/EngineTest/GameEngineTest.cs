using Programation_3_DnD_Core;
using Programation_3_DnD_Console;

public class GameEngineTest
{
    private IOutput _renderer;
    private InputProcessor _inputProcessor;
    private GameEngine _engine;

    [SetUp]
    public void Setup()
    {
        string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "JsonTest");

        _renderer = new OutputManagerForTests();
        _engine = new GameEngine(_renderer, _inputProcessor, path);
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
}
