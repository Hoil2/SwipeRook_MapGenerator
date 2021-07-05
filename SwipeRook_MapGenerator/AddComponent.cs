using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwipeRook_MapGenerator
{
    public partial class frmAddComponent : Form
    {
        frmMain main;
        public int minMovement = 3;
        public frmAddComponent(frmMain main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void txtMinMovement_TextChanged(object sender, EventArgs e)
        {
            try
            {
                minMovement = int.Parse(txtMinMovement.Text);
            }
            catch
            {
                minMovement = 3;
            }
            finally
            {
                minMovement = minMovement == 0 ? 3 : minMovement;
                main.desiredMinDistance = minMovement;
            }
        }
    }
}
