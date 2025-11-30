using UnityEngine;
using Programation_3_DnD_Core;
using System.IO;

public class UnityGameRunner : MonoBehaviour
{
    public UnityOutput _uiOutput;
    public UnityInput _uiInput;
    string _path;

    GameEngine _engine;
    GameStateMachine _gameStateMachine;

    private float _accumulator = 0f;

    void Start()
    {
        _path = Path.Combine(Application.dataPath, "Json");
        _engine = new GameEngine(_uiOutput, _uiInput, _path);
        _gameStateMachine = _engine.GetGameStateMachine();
    }

    private void Update()
    {

        if (_engine.GetShouldQuit())
        {
            Stop();
        }
        _engine.ProcessInput();
        _engine.Update();
        _uiInput.ResetInput();
        _engine.Render();
    }

    private void FixedUpdate()
    {
        if (_gameStateMachine.GetCurrentState() is InGameState)
        {
            _accumulator += Time.deltaTime;

            // Quand une seconde réelle est passée, on avance le temps du jeu
            if (_accumulator >= 1f)
            {
                _accumulator -= 1f;

                // +1 heure dans le jeu
                _engine.AddTime(1f);

                if (_engine.GetTime() > 24f)
                    _engine.AddTime(-24f);
            }

            // FixedUpdate moteur basé sur l'heure actuelle du jeu
            _engine.FixedUpdate(_engine.GetTime());

            // Un peu de chat gpt mais je comprend ce qu'il fait
        }
    }

    public IState GetCurrentState()
    {
        return _gameStateMachine.GetCurrentState();
    }

    public float GetTime()
    {
        return _engine.GetTime();
    }
    private void Stop()
    {
        Application.Quit();
    }
}
