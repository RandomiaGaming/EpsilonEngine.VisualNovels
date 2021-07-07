using System.Collections.Generic;
using System;
namespace EpsilonEngine
{
    public class Game
    {
        public bool exited { get; private set; } = false;
        public Vector2Int viewportSize = new Vector2Int(256, 144);
        private List<Scene> scenes = new List<Scene>();
        private List<GameManager> gameManagers = new List<GameManager>();
        public GameInterface gameInterface
        {
            get
            {
                if (exited)
                {
                    throw new Exception("Game has been exited.");
                }
                return _gameInterface;
            }
        }
        private GameInterface _gameInterface = null;
        public Game(GameInterface gameInterface)
        {
            if (gameInterface is null)
            {
                throw new NullReferenceException();
            }
            _gameInterface = gameInterface;
        }
        public void Quit()
        {
            if (exited)
            {
                throw new Exception("Game has been exited.");
            }
            foreach (GameManager gameManager in gameManagers)
            {
                gameManager.CallDestroy();
            }
            foreach (Scene scene in scenes)
            {
                scene.Destroy();
            }
            exited = true;
        }
        public void Tick()
        {
            if (exited)
            {
                throw new Exception("Game has been quit.");
            }
            foreach (GameManager gameManager in gameManagers)
            {
                gameManager.CallPrepare();
            }
            foreach (Scene scene in scenes)
            {
                scene.Prepare();
            }
            foreach (GameManager gameManager in gameManagers)
            {
                gameManager.CallUpdate();
            }
            foreach (Scene scene in scenes)
            {
                scene.Update();
            }
            foreach (GameManager gameManager in gameManagers)
            {
                gameManager.CallCleanup();
            }
            foreach (Scene scene in scenes)
            {
                scene.Cleanup();
            }
            gameInterface.Draw(Render());
        }
        public Texture Render()
        {
            if (exited)
            {
                throw new Exception("Game has been quit.");
            }
            RenderTexture outputRT = new RenderTexture();
            foreach (GameManager gameManager in gameManagers)
            {
                outputRT.Merge(gameManager.CallRender());
            }
            foreach (Scene scene in scenes)
            {
                outputRT.Merge(scene.Render());
            }
            return outputRT.Render(new RectangleInt(0, 0, viewportSize.x, viewportSize.y), new Color(255, 155, 255, 255));
        }
        public GameManager GetGameManager(int index)
        {
            if (exited)
            {
                throw new Exception("Game has been exited.");
            }
            if (index < 0 || index >= gameManagers.Count)
            {
                throw new ArgumentException();
            }
            return gameManagers[index];
        }
        public GameManager GetGameManager(Type type)
        {
            if (exited)
            {
                throw new Exception("Game has been exited.");
            }
            if (type is null)
            {
                throw new NullReferenceException();
            }
            if (!type.IsAssignableFrom(typeof(GameManager)))
            {
                throw new ArgumentException();
            }
            for (int i = 0; i < gameManagers.Count; i++)
            {
                if (gameManagers[i].GetType().IsAssignableFrom(type))
                {
                    return gameManagers[i];
                }
            }
            return null;
        }
        public T GetGameManager<T>() where T : GameManager
        {
            if (exited)
            {
                throw new Exception("Game has been exited.");
            }
            for (int i = 0; i < gameManagers.Count; i++)
            {
                if (gameManagers[i].GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)gameManagers[i];
                }
            }
            return null;
        }
        public List<GameManager> GetGameManagers()
        {
            if (exited)
            {
                throw new Exception("Game has been exited.");
            }
            return new List<GameManager>(gameManagers);
        }
        public List<GameManager> GetGameManagers(Type type)
        {
            if (exited)
            {
                throw new Exception("Game has been exited.");
            }
            if (type is null)
            {
                throw new NullReferenceException();
            }
            if (!type.IsAssignableFrom(typeof(GameManager)))
            {
                throw new ArgumentException();
            }
            List<GameManager> output = new List<GameManager>();
            for (int i = 0; i < gameManagers.Count; i++)
            {
                if (gameManagers[i].GetType().IsAssignableFrom(type))
                {
                    output.Add(gameManagers[i]);
                }
            }
            return output;
        }
        public List<T> GetGameManagers<T>() where T : GameManager
        {
            if (exited)
            {
                throw new Exception("Game has been exited.");
            }
            List<T> output = new List<T>();
            for (int i = 0; i < gameManagers.Count; i++)
            {
                if (gameManagers[i].GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)gameManagers[i]);
                }
            }
            return output;
        }
        public int GetGameManagerCount()
        {
            if (exited)
            {
                throw new Exception("Game has been exited.");
            }
            return gameManagers.Count;
        }
        public void RemoveGameManager(GameManager gameManager)
        {
            if (exited)
            {
                throw new Exception("Game has been exited.");
            }
            if (gameManager is null)
            {
                throw new Exception("GameManager was null.");
            }
            if (gameManager.game != this)
            {
                throw new Exception("GameManager belongs on a different Game.");
            }
            for (int i = 0; i < gameManagers.Count; i++)
            {
                if (gameManagers[i] == gameManager)
                {
                    gameManagers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("GameManager not found on this scene.");
        }
        public void AddGameManager(GameManager gameManager)
        {
            if (exited)
            {
                throw new Exception("Game has been exited.");
            }
            if (gameManager is null)
            {
                throw new Exception("GameManager was null.");
            }
            if (gameManager.game != this)
            {
                throw new Exception("GameManager belongs to a different Game.");
            }
            for (int i = 0; i < gameManagers.Count; i++)
            {
                if (gameManagers[i] == gameManager)
                {
                    throw new Exception("GameManager was already added.");
                }
            }
            gameManagers.Add(gameManager);
            gameManager.CallInitialize();
        }
        public Scene GetScene(int index)
        {
            if (exited)
            {
                throw new Exception("Game has been exited.");
            }
            if (index < 0 || index >= scenes.Count)
            {
                throw new ArgumentException();
            }
            return scenes[index];
        }
        public List<Scene> GetScenes()
        {
            if (exited)
            {
                throw new Exception("Game has been exited.");
            }
            return new List<Scene>(scenes);
        }
        public int GetSceneCount()
        {
            if (exited)
            {
                throw new Exception("Game has been exited.");
            }
            return scenes.Count;
        }
        public void RemoveScene(Scene scene)
        {
            if (exited)
            {
                throw new Exception("Game has been exited.");
            }
            if (scene is null)
            {
                throw new Exception("Scene was null.");
            }
            if (scene.game != this)
            {
                throw new Exception("Scene belongs on a different Game.");
            }
            for (int i = 0; i < scenes.Count; i++)
            {
                if (scenes[i] == scene)
                {
                    scenes.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Scene not found on this Game.");
        }
        public void AddScene(Scene scene)
        {
            if (exited)
            {
                throw new Exception("Game has been exited.");
            }
            if (scene is null)
            {
                throw new Exception("Scene was null.");
            }
            if (scene.game != this)
            {
                throw new Exception("Scene belongs to a different Game.");
            }
            for (int i = 0; i < scenes.Count; i++)
            {
                if (scenes[i] == scene)
                {
                    throw new Exception("Scene was already added.");
                }
            }
            scenes.Add(scene);
        }
    }
}