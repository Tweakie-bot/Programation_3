using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Programmation_3
{
    public class GameManager
    {
        private Thread? _thread;

        Stopwatch _stopwatch = new Stopwatch();

        private IOutput _renderer;

        private StateMachine? _stateMachine;
        private WorkStateMachine? _workStateMachine;

        private EventManager _eventManager = new EventManager();
        private GainMoneyAchievementManager _gainMoneyAchievementManager;
        private QuitGameEventManager _quitGameEventManager;
        private StateEventManager _stateEventManager;

        private GameObject? _player;
        private GameObject? _characterMya;
        private volatile string? _lastInput;

        private Location? _stormIsland;

        public GameManager(IOutput renderer)
        {
            _renderer = renderer;
            _workStateMachine = new WorkStateMachine();
        }

        private void Start()
        {
            _gainMoneyAchievementManager = new GainMoneyAchievementManager(_eventManager, _renderer);
            _quitGameEventManager = new QuitGameEventManager(_eventManager, _renderer);

            _stormIsland = new Location("Storm island", "The island ground emits growls as smoke surface form pits onto the irregular moutainous land", _renderer);

            Location sanctuary_00 = new Location("Dragon's rest, temple and sanctuary", "The temple is hit the harh wind of the island, standing on the side of a cliff on top of a hill, it seems as if nothing could make it tremble despite it's poor state", _renderer);

            Location sanctuary_01 = new Location("Cloister", "The cloister is a circular room giving access to several modest chambers, a stair lead to the next floor", _renderer);
            sanctuary_01.SetCanWork(true);
            sanctuary_01.SetCanTrade(true);

            Location sanctuary_02 = new Location("Stairs", "The stairs are irregular and difficult to climb, it leads to three rooms. The first room on your right gives off a delicious smell. The Second Room is a quiet and calm place. The room at the end of the stairs has a religious vibe", _renderer);
            Location sanctuary_031 = new Location("Kitchen", "The kitchen is brighten by candlelight, something is cooking, it smells like an apple pie", _renderer);
            Location sanctuary_032 = new Location("Library", "The place is small and doesn't contains much books, two decks are installed against a wall for writing or reading. A few seats are improvised on the floor.", _renderer);
            Location sanctuary_033 = new Location("Temple of the dragon", "Rock benches face the great statue of a dragon", _renderer);

            Location observatory_00 = new Location("Observatory", "The observatory, as you look upon it's tour, a chill runs your spine", _renderer);

            Location rose_wind_00 = new Location("Rose wind shipwreck", "The once proud embarcation lies destroyed by the cruel waves", _renderer);

            Location caves_00 = new Location("The myconids' caves", "The entrance of the cave is constantly crushed by the the torrent of the sea. You will have to be quick in order to cross", _renderer);



            _stormIsland.ConnectALocation(sanctuary_00);
            _stormIsland.ConnectALocation(rose_wind_00);
            _stormIsland.ConnectALocation(caves_00);
            _stormIsland.ConnectALocation(observatory_00);

            sanctuary_00.ConnectALocation(sanctuary_01);
            sanctuary_01.ConnectALocation(sanctuary_02);
            sanctuary_02.ConnectALocation(sanctuary_031);
            sanctuary_02.ConnectALocation(sanctuary_032);
            sanctuary_02.ConnectALocation(sanctuary_033);

            _player = new Player("Joueur", new LocationComponent(_stormIsland));
            _player.AddComponent(new InventoryComponent(_renderer));
            // Player doit être initialisé avant InGameState

            _characterMya = new Pnj("Mya", new LocationComponent(sanctuary_01));
            _characterMya.AddComponent(new InventoryComponent(_renderer));
            _characterMya.GetComponent<InventoryComponent>().Add(new Item("Steel Sword", "Sharpened by Mya", 40));
        }

        public void ProcessInput(string input)
        {
            _stateMachine.ProcessInput(input);
            _lastInput = null;
        }

        public void Update()
        {
            _stateMachine.Update();
        }

        public void Render()
        {
            _stateMachine.Render();
            Thread.Sleep(1000);
            Console.Clear();
        }

        public void Run()
        {
            Start();

            _stateMachine = new StateMachine(this, _renderer);

            _stateEventManager = new StateEventManager(_eventManager, _stateMachine);

            _thread = new Thread(ReadKey);
            _thread.Start();

            _stateMachine = new StateMachine(this, _renderer);

            _stateEventManager = new StateEventManager(_eventManager, _stateMachine);

            _stopwatch.Start();

            float last_time = 0;
            float lag = 0;

            while (!_quitGameEventManager.GetShouldQuitGame())
            {
                _renderer.WriteLine((GetCurrentTime() / 1000).ToString());

                float current_time = GetCurrentTime();
                float elasped_time = current_time - last_time;
                lag += elasped_time;

                ProcessInput(_lastInput);
                if (lag > 330f)
                {
                    FixedUpdate(last_time);
                    lag -= 330f;
                }
                Update();
                Render();
                last_time = current_time;
            }
        }

        private void FixedUpdate(float dt)
        {
            _workStateMachine.FixedUpdate(dt /1000);
        }

        private float GetCurrentTime()
        {
            return _stopwatch.ElapsedMilliseconds;
        }

        private void ReadKey()
        {
            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                _lastInput = GetInputFromKey(key);

                Thread.Sleep(10);
            }
        }
        private string GetInputFromKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Enter:
                    return "Enter";
                case ConsoleKey.Escape:
                    return "Escape";
                case ConsoleKey.NumPad0:
                    return "0";
                case ConsoleKey.NumPad1:
                    return "1";
                case ConsoleKey.NumPad2:
                    return "2";
                case ConsoleKey.NumPad3:
                    return "3";
                case ConsoleKey.NumPad4:
                    return "4";
                case ConsoleKey.NumPad5:
                    return "5";
                case ConsoleKey.NumPad6:
                    return "6";
                case ConsoleKey.NumPad7:
                    return "7";
                case ConsoleKey.NumPad8:
                    return "8";
                case ConsoleKey.NumPad9:
                    return "9";
                case ConsoleKey.T:
                    return "T";
                case ConsoleKey.F:
                    return "F";
                case ConsoleKey.I:
                    return "I";
                case ConsoleKey.W:
                    return "W";
                default:
                    return "Should do nothing";
            }
        }
        public GameObject GetPlayer()
        {
            return _player;
        }

        public GainMoneyAchievementManager GetMoneyAchievementManager()
        {
            return _gainMoneyAchievementManager;
        }

        public QuitGameEventManager GetQuitGameEventManager()
        {
            return _quitGameEventManager;
        }

        public StateEventManager GetStateEventManager()
        {
            return _stateEventManager;
        }

        public Character GetMya()
        {
            return (Character)_characterMya;
        }

        public WorkStateMachine GetWorkStateMachine()
        {
            return _workStateMachine;
        }
    }
}
