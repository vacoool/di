namespace FractalPainting.Infrastructure.UiActions
{
    public interface IUiAction
    {
        string Category { get; }
        string Name { get; }
        string Description { get; }
        void Perform();
        int Order { get; }
    }
}