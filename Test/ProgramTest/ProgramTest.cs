using Programation_3_DnD_Core;
using Programation_3_DnD_Console;

public class ProgramTest
{
    InputProcessor _inputProcessor;

    [SetUp]
    public void SetUp()
    {
        _inputProcessor = new InputProcessor();
    }

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
            GameEngine engine = new GameEngine(renderer, _inputProcessor, path);
        });
    }
}
