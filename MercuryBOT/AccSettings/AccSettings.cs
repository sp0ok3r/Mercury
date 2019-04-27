using MercuryBOT.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MercuryBOT.AccSettings
{
    public partial class AccSettings : MetroFramework.Forms.MetroForm
    {
        public AccSettings()
        {
            InitializeComponent();
            this.components.SetStyle(this);
            Region = System.Drawing.Region.FromHrgn(Helpers.Extensions.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));

        }
    }
}
