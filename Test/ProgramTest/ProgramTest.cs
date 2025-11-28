using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Output;

public class ProgramTest
{
    [Test]
    public void CreatingOutputDoesNotCrash()
    {
        Assert.DoesNotThrow(() => { IOutput renderer = new OutputManagerForTests(); });
    }

    [Test]
    public void CreatingGameEngineDoesNotCrash()
    {
        string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "JsonTest");

        Assert.DoesNotThrow(() =>
        {
            IOutput renderer = new OutputManagerForTests();
            GameEngine engine = new GameEngine(renderer, path);
        });
    }

    [Test]
    public void ProgramClassExists()
    {
        Assert.NotNull(typeof(Program));
    }
}
