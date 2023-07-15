namespace AutoShutDown.UI
{
    public partial class Overlay : Form
    {
        private bool _drag = false;
        private Point _startMousePosition;

        public Overlay()
        {
            InitializeComponent();
            this.BackColor = Color.HotPink;
            this.TransparencyKey = Color.HotPink;

        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.HotPink, e.ClipRectangle);
        }

        public void UpdateText(string text)
        {
            StatusLabel.Invoke((MethodInvoker)delegate
            {
                StatusLabel.Text = text;
            });
        }

        private void StatusLabel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _drag = true;
                this.Cursor = Cursors.NoMove2D;
                _startMousePosition = e.Location;
            }
        }

        private void StatusLabel_MouseUp(object sender, MouseEventArgs e)
        {
            _drag = false;
            Cursor = Cursors.Default;
        }

        private void StatusLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_drag) return;
            var diffLoc = Location;
            diffLoc.X = diffLoc.X + e.Location.X - _startMousePosition.X;
            diffLoc.Y = diffLoc.Y + e.Location.Y - _startMousePosition.Y;

            Location = diffLoc;
        }

        private void Overlay_Load(object sender, EventArgs e)
        {
        }

        private void StatusLabel_TextChanged(object sender, EventArgs e)
        {

        }
    }
}