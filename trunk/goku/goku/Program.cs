using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace goku
{
    static class Program
    {
        static Mutex m = new Mutex(true, "{43617342726173696c_676f6b75_726f5346}");
        /// <summary>
        /// Ferramenta para verificação e envido de aquivos para o ro.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (m.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
                MessageBox.Show("Já existe uma instancia do roSF em funcionamento!");
        }
    }
}
