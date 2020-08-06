using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEncoder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CreatePath(String dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                Console.WriteLine($"Directory Exists: {dirPath}");
                return;
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(dirPath);
                    
                }
                catch (Exception error)
                {
                    Console.WriteLine($"Create Path Error:{error}");
                    MessageBox.Show($"Create Path Error:{error}");
                    return;
                }
            }

        }
        private void openFilesBtn_Click(object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.Stream myStream;
                string fileName;
                
                fileName = openFileDialog1.FileName;
                foreach (String filePath in openFileDialog1.FileNames)
                {
                    try
                    {
                        if ((myStream = openFileDialog1.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                Console.WriteLine($"{Path.GetFileName(filePath)}");
                                listView1.Items.Add($"{Path.GetFileName(filePath)}").SubItems.Add($"{filePath}");
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }       
            }
        }
        private void convertBtn_Click(object sender, EventArgs e)
        {

            // 1.) Grab all Items in the List
            foreach (ListViewItem item in listView1.Items)
            {
                
                //Console.WriteLine($"{item.SubItems[1].Text}");
                String encodedFile;
                String newDir = $"{Path.GetDirectoryName($"{item.SubItems[1].Text}")}/Encoded DXF";
                String newPath = $"{Path.GetDirectoryName($"{item.SubItems[1].Text}")}/Encoded DXF/{Path.GetFileName(item.SubItems[1].Text)}";

                // 2.) Foreach Item Grab the Path and read each file to the end 
                using (StreamReader sr = new StreamReader($"{item.SubItems[1].Text}"))
                {
                    String fileContents = sr.ReadToEnd();
                    // (?=^|\r?\n)\bTEXT\b
                    // Regex Pattern
                    String pattern1 = @"(?=^|\r?\n)\bTEXT\b";
                    String pattern2 = @"(?=^|\r?\n)\bAcDbText\b";
                    // Replacement Text
                    String replacement1 = "MTEXT";
                    String replacement2 = "AcDbMText";

                    // Multiline option to read not only the first line
                    RegexOptions options = RegexOptions.Multiline;


                    Regex rgx1 = new Regex(pattern1, options);
                    Regex rgx2 = new Regex(pattern2, options);

                    // Looks for "TEXT" && "AcDbText" and Replaces it with "MTEXT" && "AcDbMText"
                    encodedFile = rgx1.Replace(fileContents, replacement1);
                    encodedFile = rgx2.Replace(encodedFile, replacement2);


                    // Prints out new Encoded String
                    Console.WriteLine($"{encodedFile}");

                }

                // 3.) Create Directory if it is not already created
                CreatePath(newDir);
                // 4.) Write Encoded String to new File inside of file directory (Encoded DXF/Encoded File)
                using (StreamWriter sw = new StreamWriter(newPath))
                {
                    sw.WriteLine($"{encodedFile}");
                }

                // 5.) Remove File from the List
                item.Remove();
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection list = listView1.SelectedItems;

            foreach (ListViewItem item in list)
            {
                item.Remove();
            }
        }

        private void deleteAllBtn_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }
    }
}
