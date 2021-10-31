using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Extensibility;

namespace ExtensibilityTest {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        void extensions_ExtensionClicked(object sender, EventArgs e) {
            IExtender extender = ((ToolStripMenuItemEx)sender).Extension;
            MessageBox.Show((String)extender.Execute());
        }

        private Extensions extensions = null;
        private void button1_Click(object sender, EventArgs e) {
            extensions = new Extensions();
            extensions.ExtensionClicked += new Extensions.extensionClickedEventHandler(extensions_ExtensionClicked);
            try {
                
                extensions.populate(Application.StartupPath, false, this.extensionsToolStripMenuItem);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}