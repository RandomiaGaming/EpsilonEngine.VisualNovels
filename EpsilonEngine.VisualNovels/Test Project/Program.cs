using System;
using EpsilonEngine.Interfaces.MonoGame;
using EpsilonEngine;
namespace TestProject
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            GameInterface monogameInterface = new MonoGameInterface();
            Game catsInLove = new Game(monogameInterface);
            Scene mainScene = new Scene(catsInLove);
            mainScene.cameraPosition = new Vector2Int(-128, -72);
            GameObject cat = new GameObject(mainScene);
            TextureRenderer catRenderer = new TextureRenderer(cat);
            catRenderer.texture = AssetHelper.LoadAsset<TextureAsset>("Cat.png").data;
            catRenderer.offset.x = catRenderer.texture.width / 2 * -1;
            catRenderer.offset.y = catRenderer.texture.height / 2 * -1;
            monogameInterface.Run(catsInLove);
        }
    }
}