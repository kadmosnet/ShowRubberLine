using System;
using System.Windows.Forms;
using DXFReaderNET;
using DXFReaderNET.Entities;
namespace ShowRubberLine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        internal enum FunctionsEnum
        {
            None,
            Line1,
            Line2,
        }

        private Vector2 p1 = Vector2.Zero;
        private Vector2 p2 = Vector2.Zero;
        private FunctionsEnum CurrentFunction = FunctionsEnum.None;

        private void button1_Click(object sender, EventArgs e)
        {
            CurrentFunction = FunctionsEnum.Line1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dxfReaderNETControl1.NewDrawing();
            dxfReaderNETControl1.CustomCursor = CustomCursorType.CrossHair;
        }

        private void dxfReaderNETControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (CurrentFunction)
                {
                    case FunctionsEnum.Line2:
                        CurrentFunction = FunctionsEnum.None;

                        p2 = dxfReaderNETControl1.CurrentWCSpoint;

                        dxfReaderNETControl1.DrawEntity(dxfReaderNETControl1.AddLine(p1.ToVector3(), p2.ToVector3()));

                        break;
                    case FunctionsEnum.Line1:
                        CurrentFunction = FunctionsEnum.Line2;

                        p1 = dxfReaderNETControl1.CurrentWCSpoint;
                        break;
                }

            }
        }

        private void dxfReaderNETControl1_MouseMove(object sender, MouseEventArgs e)
        {
            switch (CurrentFunction)
            {
                case FunctionsEnum.Line2:
                    dxfReaderNETControl1.ShowRubberBandLine(p1, dxfReaderNETControl1.CurrentWCSpoint);
                    break;
            }
        }
        
    }
}

