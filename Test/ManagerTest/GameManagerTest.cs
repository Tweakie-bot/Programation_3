using NUnit.Framework;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Event;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Objects;
using Programation_3_DnD.Composants;
using Programation_3_DnD.State;
using System;

public class GameManagerTest
{
    private IOutput _renderer;
    private GameEngine _engine;
    private EventManager _eventManager;
    private GameManager _gameManager;
    private GameStateMachine _stateMachine;

    [SetUp]
    public void Setup()
    {
        string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "JsonTest");
        _renderer = new OutputManagerForTests();
        _engine = new GameEngine(_renderer, path);

        _eventManager = _engine.GetEventManager();
        _gameManager = _engine.GetGameManager();
        _stateMachine = _engine.GetGameStateMachine();
    }

    [Test]
    public void TryGameManagerInitialization()
    {
        Assert.IsNotNull(_gameManager);
    }

    [Test]
    public void TryGetPlayer()
    {
        GameObject player = _gameManager.GetPlayer();
        Assert.IsNotNull(player);
    }

    [Test]
    public void PlayerHasInventory()
    {
        GameObject player = _gameManager.GetPlayer();
        InventoryComposant inventory = player.GetComposant<InventoryComposant>();

        Assert.IsNotNull(inventory);
    }

    [Test]
    public void PlayerHasPosition()
    {
        GameObject player = _gameManager.GetPlayer();
        PositionComposant position = player.GetComposant<PositionComposant>();

        Assert.IsNotNull(position);
    }

    [Test]
    public void ProcessInputDoesNotCrash()
    {
        _gameManager.ProcessInput(ConsoleKey.A);
        Assert.Pass();
    }

    [Test]
    public void UpdateDoesNotCrash()
    {
        _gameManager.Update();
        Assert.Pass();
    }

    [Test]
    public void FixedUpdateDoesNotCrash()
    {
        _gameManager.FixedUpdate(1.2f);
        Assert.Pass();
    }

    [Test]
    public void RenderDoesNotCrash()
    {
        _gameManager.Render();
        Assert.Pass();
    }

    [Test]
    public void RendererIsNotNull()
    {
        Assert.IsNotNull(_gameManager.GetRenderer());
    }
}
