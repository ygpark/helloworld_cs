using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Text;
using Extensibility;
using System.Reflection;

namespace ExtensionsTest {
    public class SomeExtension_1 : IExtender {

        public SomeExtension_1() {
            String imagePath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Icon icn = new Icon(System.IO.Path.Combine(imagePath,"18.ico"));
            _Image = icn.ToBitmap();
        }

        private String _Name = "Some extension #1";
        private String _Description = "Description of Some Extension #1";
        private String _MenuText = "Extension 1";
        private Image _Image = null;
        private String _Provider = "My Company Name";

        #region IExtender Members

        string IExtender.Name {
            get { return _Name; }
        }

        string IExtender.Description {
            get { return _Description; }
        }

        string IExtender.MenuText {
            get { return _MenuText; }
        }

        string IExtender.DLLPath {
            get {
                return Assembly.GetExecutingAssembly().Location;
            }
        }

        Image IExtender.Image {
            get { return _Image; }
        }

        string IExtender.Provider {
            get { return _Provider; }
        }

        object IExtender.Execute() {
            return "The Execute method or operation for SomeExtension_1 is not implemented.";
        }

        #endregion
    }
}
