using System;

namespace gloStreamWebCamera.Contracts
{
    public interface IFrameSource : IgloStreamWebCameraAddIn
    {
        event Action<IFrameSource, Frame, double> NewFrame;

        void StartFrameCapture();
        void StopFrameCapture();
    }
}
