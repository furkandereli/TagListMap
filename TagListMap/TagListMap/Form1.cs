using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagListMap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofg = new OpenFileDialog();
            if (ofg.ShowDialog()==DialogResult.OK)
            {
                textBox1.Text = ofg.FileName;
                textBox2.Text = File.ReadAllText(ofg.FileName);
            }
        }

        class WordCounter
        {
            public string Word { get; set; }
            public int Piece { get; set; }

            public WordCounter(string word, int piece)
            {
                this.Word = word;
                this.Piece = piece;
            }
        }

        private void buttonCount_Click(object sender, EventArgs e)
        {
            String allWords = textBox2.Text;
            String[] wordsArray = allWords.Split(',', '.', '!', '-');

            List<WordCounter> wC = new List<WordCounter>();

            foreach (string word in wordsArray)
            {
                WordCounter foundWord = wC.Find(x => x.Word == word);        
                if (foundWord == null)
                {
                    wC.Add(new WordCounter(word, 1));
                    
                }
                else
                {
                    foundWord.Piece++;
                }
            }
            
            
            listView1.Columns.Add("Piece", 50);
            listView1.Columns.Add("Word", 140);          
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.Sorting = SortOrder.Descending;

            foreach (WordCounter word in wC)
            {
                String[] rowItem = new string[] { word.Piece.ToString("D3"), word.Word };
                listView1.Items.Add(new ListViewItem(rowItem));
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }
    }
}
