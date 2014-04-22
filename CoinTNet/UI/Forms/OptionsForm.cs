using CoinTNet.UI.Controls.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTNet.UI.Forms
{
    partial class OptionsForm : Form
    {
        private Dictionary<Type, Control> _types;

        public OptionsForm()
        {
            InitializeComponent();
            _types = new Dictionary<Type, Control>();
            InitialiseSections();

        }

        private void InitialiseSections()
        {
            var tnBitstamp = new TreeNode("Bitstamp") { Tag = typeof(BitstampKeysControl) };
            var tnBtce = new TreeNode("BTC-e") { Tag = typeof(BtceKeysControl) };
            var twitterKeys = new TreeNode("Twitter") { Tag = typeof(TwitterKeysControl) };
            var tnKeys = new TreeNode("Keys", new[] { tnBitstamp, tnBtce,twitterKeys });
            

            tvSections.Nodes.Add(tnKeys);
            tvSections.ExpandAll();
        }

        private void tvSections_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var type = e.Node.Tag as Type;
            var currNode = e.Node;
            while (type == null)
            {
                currNode = e.Node.Nodes[0];
                type = currNode.Tag as Type;
            }
            Control ctrl;
            if (!_types.TryGetValue(type, out ctrl))
            {
                ctrl = Activator.CreateInstance(type) as Control;
                _types[type] = ctrl;
            }
            pnlPlaceHolder.Controls.Clear();
            pnlPlaceHolder.Controls.Add(ctrl);
            //e.Node
        }

        private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                foreach(var kvp in _types)
                {
                    (kvp.Value as Interfaces.IOptionControl).Save();
                }
            }
        }
    }
}
