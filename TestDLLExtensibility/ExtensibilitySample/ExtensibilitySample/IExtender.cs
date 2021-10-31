using System.Drawing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extensibility {
    public interface IExtender {
        String Name {
            get;
        }
        String Description {
            get;
        }
        String MenuText {
            get;
        }
        String DLLPath {
            get;
        }
        Image Image {
            get;
        }
        String Provider {
            get;
        }
        Object Execute();
    }
}
