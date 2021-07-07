using System.Collections.Generic;
namespace EpsilonEngine
{
    public sealed class RenderTexture
    {
        private List<RenderInstruction> buffer = new List<RenderInstruction>();
        public Texture Render(RectangleInt bounds)
        {
            Texture output = new Texture(bounds.max.x - bounds.min.x, bounds.max.y - bounds.min.y, Color.Clear);
            foreach (RenderInstruction renderInstruction in buffer)
            {
                    TextureHelper.Blitz(renderInstruction.texture, output, new Vector2Int(renderInstruction.position.x - bounds.min.x, renderInstruction.position.y - bounds.min.y));
            }
            return output;
        }
        public Texture Render(RectangleInt bounds, Color backgroundColor)
        {
            Texture output = new Texture(bounds.max.x - bounds.min.x, bounds.max.y - bounds.min.y, backgroundColor);
            foreach (RenderInstruction renderInstruction in buffer)
            {
                TextureHelper.Blitz(renderInstruction.texture, output, new Vector2Int(renderInstruction.position.x - bounds.min.x, renderInstruction.position.y - bounds.min.y));
            }
            return output;
        }
        public void Draw(Texture texture, Vector2Int position)
        {
            buffer.Add(new RenderInstruction(texture, position));
        }
        public void Merge(RenderTexture other)
        {
            buffer.AddRange(other.buffer);
        }
        public void Offset(Vector2Int offset)
        {
            for (int i = 0; i < buffer.Count; i++)
            {
                buffer[i].position += offset;
            }
        }
    }
}