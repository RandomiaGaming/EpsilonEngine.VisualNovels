using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public abstract class GameInterface
    {
        public Game game { get; private set; } = null;
        public GameInterface()
        {

        }
        public void Run(Game game)
        {
            if (game is null)
            {
                throw new NullReferenceException();
            }
            if (game.gameInterface != this)
            {
                throw new ArgumentException();
            }
            this.game = game;
            Initialize();
        }
        public abstract List<KeyboardState> GetKeyboardStates();
        public abstract KeyboardState GetPrimaryKeyboardState();
        public abstract List<MouseState> GetMouseStates();
        public abstract MouseState GetPrimaryMouseState();
        public abstract Vector2Int GetViewPortRect();
        public abstract void Draw(Texture frame);
        public abstract void Initialize();
    }
}
