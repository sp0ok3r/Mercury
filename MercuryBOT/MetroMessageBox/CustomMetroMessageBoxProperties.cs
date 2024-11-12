using System.Windows.Forms;

namespace Mercury.MetroMessageBox
{
    public class CustomMetroMessageBoxProperties
    {
        public CustomMetroMessageBoxProperties(CustomMetroMessageBoxControl owner)
        {
            Owner = owner;
        }

        public MessageBoxButtons Buttons { get; set; }

        public MessageBoxDefaultButton DefaultButton { get; set; }

        public MessageBoxIcon Icon { get; set; }

        public string Message { get; set; }

        public CustomMetroMessageBoxControl Owner { get; }

        public string Title { get; set; }
    }
}