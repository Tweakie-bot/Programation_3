using NUnit.Framework;
using Programation_3_DnD_Core;
using Programation_3_DnD_Console;
using System;

public class OutputManagerTest
{
    private IOutput _output;

    [SetUp]
    public void Setup()
    {
        _output = new OutputManagerForTests();
    }

    [Test]
    public void TryWriteLineDoesNotCrash()
    {
        _output.WriteLine("Test message");
        Assert.Pass();
    }

    [Test]
    public void TryPassLineDoesNotCrash()
    {
        _output.PassLine();
        Assert.Pass();
    }

    [Test]
    public void OutputManagerIsNotNull()
    {
        Assert.IsNotNull(_output);
    }
}
