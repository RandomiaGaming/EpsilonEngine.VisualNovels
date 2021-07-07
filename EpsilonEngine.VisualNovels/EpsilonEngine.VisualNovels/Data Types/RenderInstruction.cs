namespace EpsilonEngine
{
    public sealed class RenderInstruction
    {
        public Texture texture  = new Texture(1, 1);
        public Vector2Int position = Vector2Int.Zero;
        public RenderInstruction(Texture texture, Vector2Int position)
        {
            this.texture = texture;
            this.position = position;
        }
    }
}
