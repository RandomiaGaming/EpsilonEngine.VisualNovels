namespace EpsilonEngine
{
    public sealed class TextureRenderer : Component
    {
        public Texture texture;
        public Vector2Int offset = Vector2Int.Zero;
        public TextureRenderer(GameObject gameObject) : base(gameObject)
        {

        }
        protected override void Update()
        {

        }
        protected override RenderTexture Render()
        {
            RenderTexture output = new RenderTexture();
            output.Draw(texture, offset);
            return output;
        }
    }
}
