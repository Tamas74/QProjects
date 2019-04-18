using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace gloStreamWebCamera.Contracts
{
    public interface IObjectDetector : IgloStreamWebCameraAddIn
    {
        event Action<IObjectDetector, DetectedObject, Frame> NewObject;
        event Action<IObjectDetector, DetectedObject, Frame> ObjectMoved;
        event Action<IObjectDetector, DetectedObject, Frame> ObjectRemoved;
        event Action<IObjectDetector, Frame, ReadOnlyCollection<DetectedObject>> FrameProcessed;
        
        ReadOnlyCollection<DetectedObject> DetectObjects(Frame frame);
    }
}
