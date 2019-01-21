using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSDDienNuoc.UserControls
{
    public partial class baseUC : UserControl
    {
        public baseUC()
        {
            InitializeComponent();
        }

        public virtual void ReSizeDataGridView(DataGridViewAutoSizeColumnMode type)
        {
            
        }
        private void baseUC_Load(object sender, EventArgs e)
        {

        }
    }
}
