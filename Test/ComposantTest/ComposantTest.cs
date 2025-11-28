using NUnit.Framework;
using Programation_3_DnD.Composants;
using System;

public class FakeComposant : Composant
{
    public bool ProcessInputCalled = false;
    public bool UpdateCalled = false;
    public bool FixedUpdateCalled = false;
    public bool RenderCalled = false;

    public override void ProcessInput(ConsoleKey key)
    {
        ProcessInputCalled = true;
    }

    public override void Update()
    {
        UpdateCalled = true;
    }

    public override void FixedUpdate(float t)
    {
        FixedUpdateCalled = true;
    }

    public override void Render()
    {
        RenderCalled = true;
    }
}

public class ComposantTest
{
    private FakeComposant _composant;

    [SetUp]
    public void Setup()
    {
        _composant = new FakeComposant();
    }

    [Test]
    public void ProcessInputIsCalled()
    {
        _composant.ProcessInput(ConsoleKey.A);
        Assert.IsTrue(_composant.ProcessInputCalled);
    }

    [Test]
    public void UpdateIsCalled()
    {
        _composant.Update();
        Assert.IsTrue(_composant.UpdateCalled);
    }

    [Test]
    public void FixedUpdateIsCalled()
    {
        _composant.FixedUpdate(1f);
        Assert.IsTrue(_composant.FixedUpdateCalled);
    }

    [Test]
    public void RenderIsCalled()
    {
        _composant.Render();
        Assert.IsTrue(_composant.RenderCalled);
    }
}
