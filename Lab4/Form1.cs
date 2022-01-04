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
//@author Ansari Mohammed Faisal - 000812671
namespace Lab4
{
    /// <summary>
    /// this class 2 global variables
    /// </summary>
    public partial class Form1 : Form
    {
        /// tagCoun is used to store  number of tags in tags
        int tagCount = 0;
        /// Stack tags is used to store html tags
        Stack<string> tags = new Stack<string>();


        public Form1()
        {
            InitializeComponent();
        }

        private void FIleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// this class loads file that user slected into a string 
        /// and then extracts all elements/tags from it into tags
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var singletonTags = new List<string> { "img", "br", "base", "area", "col", "command", "embed", "hr", "input", "keygen", "link", "meta", "param", "source", "track", "wbr" };
            checktagslistBox.Items.Clear();
            tags.Clear();
            string tag = "";
            bool start = false;
            openFileDialog1.Filter = "HTML file (*.html)|*.html";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(openFileDialog1.FileName);
                string filename = openFileDialog1.FileName;
                string data = File.ReadAllText(filename);
                stateLabel.Text = filename + " :Loaded Successfuly";
                foreach (char c in data)
                {
                    if (start == true & c != '>' & c != ' ')
                    {
                        tag += c;
                    }
                    else { }
                    if (c == '<')
                    {
                        start = true;
                    }
                    else if (c == '>' || c == ' ')
                    {
                        if (singletonTags.Contains(tag, StringComparer.OrdinalIgnoreCase))
                        {
                            tag = "";
                            start = false;
                        }
                        if (tag != "")
                        {
                            tags.Push(tag);
                            tag = "";
                            start = false;
                        }
                    }
                }
                tagCount = tags.Count;
            }
            else
                MessageBox.Show("You Selected No File..");
            
   

        }


        /// <summary>
        /// this class prints tags on to checktagsListBox
        /// this method also check if file is corect or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckTagsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stack<string> reverseTag = new Stack<string>();
            
                foreach (string s in tags)
                {
                reverseTag.Push(s);
                }
                foreach (string s in reverseTag)
                {
                    if (s.StartsWith("/"))
                    {
                        checktagslistBox.Items.Add("Found Closing Tag: <" + s + ">");
                    }
                    else
                    {
                        checktagslistBox.Items.Add("Found Opening Tag: <" + s + ">");
                    }
                }
                if (tagCount%2 == 0)
                {
                    stateLabel.Text = "File is good";
                }
                else
                {
                    stateLabel.Text = "File is not good";
                }

            
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
