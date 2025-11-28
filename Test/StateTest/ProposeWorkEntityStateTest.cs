using NUnit.Framework;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Event;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Objects;
using Programation_3_DnD.State;
using System;

public class ProposeWorkEntityStateTest
{
    private IOutput _renderer;
    private GameEngine _engine;
    private EventManager _eventManager;
    private GameManager _gameManager;
    private GameObject _player;
    private ProposeWorkEntityState _state;

    [SetUp]
    public void Setup()
    {
        string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "JsonTest");

        _renderer = new OutputManagerForTests();
        _engine = new GameEngine(_renderer, path);
        _eventManager = _engine.GetEventManager();
        _gameManager = _engine.GetGameManager();
        _player = _gameManager.GetPlayer();

        _state = new ProposeWorkEntityState(_engine, _renderer, _eventManager, _player);
    }

    [Test]
    public void TryEnterDoesNotCrash()
    {
        Assert.DoesNotThrow(() => { _state.Enter(); });
    }

    [Test]
    public void TryExitDoesNotCrash()
    {
        Assert.DoesNotThrow(() => { _state.Exit(); });
    }

    [Test]
    public void TryUpdateDoesNotCrash()
    {
        Assert.DoesNotThrow(() => { _state.Update(); });
    }

    [Test]
    public void TryRenderDoesNotCrash()
    {
        Assert.DoesNotThrow(() => { _state.Render(); });
    }

    [Test]
    public void TryProcessInputWithOtherKeyDoesNotCrash()
    {
        Assert.DoesNotThrow(() => { _state.ProcessInput(ConsoleKey.A); });
    }

    [Test]
    public void TryProcessInputWorkKeyDoesNotCrash()
    {
        Assert.DoesNotThrow(() => { _state.ProcessInput(ConsoleKey.W); });
    }
}
