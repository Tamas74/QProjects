using System.Windows;

namespace gloStreamWebCamera.Contracts
{
    public interface IgloStreamWebCameraAddIn
    {
        string Name { get; }
        string Description { get; }
        bool HasConfiguration { get; }
        UIElement ConfigurationElement { get; }
    }
}