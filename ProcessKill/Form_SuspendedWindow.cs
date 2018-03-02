using System.Drawing;
using System.Windows.Forms;

namespace ProcessKill
{
    public partial class Form_SuspendedWindow : Form
    {
        Point mousePoint;
        bool mouseLeftButton;

        public Form_SuspendedWindow()
        {
            InitializeComponent();
        }

        private void Form_SuspendedWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mousePoint = new Point(-e.X, -e.Y);
                mouseLeftButton = true;
            }
        }

        private void Form_SuspendedWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseLeftButton)
            {
                Point point = MousePosition;
                point.Offset(mousePoint.X, mousePoint.Y);
                Location = point;
            }
        }

        private void Form_SuspendedWindow_MouseUp(object sender, MouseEventArgs e)
        {
            if(mouseLeftButton)
            {
                mouseLeftButton = false;
            }
        }

        private void Form_SuspendedWindow_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Close();
        }
    }
}
