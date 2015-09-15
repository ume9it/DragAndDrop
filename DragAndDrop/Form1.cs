using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DragAndDrop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // ドラッグアンドドロップの処理を実装
            new DragAndDrop(this.textBox1, AfterDragAndDrop);
        }
        private void AfterDragAndDrop(string[] strAllFilePath)
        {
            foreach(string strFilePath in strAllFilePath)
            {
                // ドラッグアンドドロップされた各ファイルに対して処理を行う

                // ドラッグアンドドロップ後の処理を記述
                MessageBox.Show(strFilePath);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
