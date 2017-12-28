using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{

    /// <summary>
    /// Ankit Bansal 
    /// 3-4-2017
    /// </summary>
    public partial class Form1 : Form
    {
        int i = 0, x = 0, y = 0, formX = 54, formY= 420, formYoutput=775;

        

        const int cmToPixel = 37;

        private void btnReset_Click(object sender, EventArgs e)
        {
            foreach (var item in this.Controls.OfType<Button>())
            {
                if (!item.Name.StartsWith("btn"))
                    this.Controls.Remove(item);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        List<Rectangle> inputRec = new List<Rectangle>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            formX = 54;
            List<Rectangle> outputRec = new List<Rectangle>();
            CallRecursive(inputRec, outputRec);

            foreach (var item in outputRec)
            {
                Button mybutton = new Button();
                mybutton.Text = item.height + " X " + item.width;
                mybutton.Height = item.height * cmToPixel;
                mybutton.Width = item.width * cmToPixel;
                mybutton.Location = new Point(formX + item.X* cmToPixel, formYoutput - (item.Y+ item.height)*cmToPixel);
                this.Controls.Add(mybutton);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Button mybutton = new Button();
            mybutton.Text = txtHeight.Text + " X " + txtWidth.Text;
            mybutton.Height = Convert.ToInt32( txtHeight.Text) * cmToPixel;
            mybutton.Width = Convert.ToInt32(txtWidth.Text) * cmToPixel;
            mybutton.Location = new Point(formX, formY- mybutton.Height);
            formX = formX + mybutton.Width;
            this.Controls.Add(mybutton);

            inputRec.Add(new Rectangle
            {
                height = Convert.ToInt32(txtHeight.Text),
                width = Convert.ToInt32(txtWidth.Text),
                sequence = i++,
                X = x,
                Y = 0
            });
            x = x + Convert.ToInt32(txtWidth.Text);
        }

        static void CallRecursive(List<Rectangle> inputRec, List<Rectangle> outputRec)
        {

            Rectangle rect = new Rectangle { height = inputRec.Min(r => r.height), width = inputRec.Sum(r => r.width), X = inputRec.Min(r => r.X), Y = inputRec.Min(r => r.Y) };
            outputRec.Add(rect);
            foreach (var item in inputRec)
            {
                item.height = item.height - rect.height;
                item.Y = item.Y + rect.height;
            }
            inputRec = inputRec.Where(r => r.height > 0).ToList();

            if (inputRec.Count > 0)
            {
                var result = inputRec.GroupBy(num => inputRec.Where(candidate => candidate.sequence >= num.sequence)
                                                .OrderBy(candidate => candidate.sequence)
                                                .TakeWhile((candidate, index) => candidate.sequence == num.sequence + index)
                                                .Last())
                           .Select(seq => seq.OrderBy(num => num.sequence)).ToList();
                foreach (var item in result)
                {
                    CallRecursive(item.ToList(), outputRec);
                }
            }
        }
    }

    class Rectangle
    {
        public int sequence { get; set; }
        public int height { get; set; }
        public int width { get; set; }


        public int X { set; get; }
        public int Y { set; get; }
    }
}
