/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/
namespace MercuryBOT.InfoForm
{
    class InfoHelper
    {
        //https://stackoverflow.com/questions/8622937/how-to-send-text-to-textbox-through-a-different-class
        // message to other form

        /// <summary>
        /// https://stackoverflow.com/questions/6932792/how-to-create-a-custom-messagebox
        /// Your custom message box helper.
        /// </summary>
        public static class CustomMessageBox
        {
            public static void Show(string title, string description)
            {
                // using construct ensures the resources are freed when form is closed
                using (var form = new Info(title, description))
                {
                    form.ShowDialog();
                }
            }
        }
    }
}


