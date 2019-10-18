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

namespace ReplaceFilenames
{
    public partial class FormReplaceFilenames: Form
    {
        public FormReplaceFilenames()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = txtPath.Text;
            string oldString = txtOldText.Text;
            string newString = txtNewText.Text;

            SubstituirNomeArquivos(path, oldString, newString, true);
        }

        /// <summary>
        /// Substitui os nomes do arquivos da pasta
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="oldString"></param>
        /// <param name="newString"></param>
        /// <param name="mainFolder"></param>
        private void SubstituirNomeArquivos(string folder, string oldString, string newString, 
            bool mainFolder)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(folder);

            var files = dirInfo.GetFiles();

            foreach (FileInfo f in files)
            {
                if (f.Name.Contains(oldString))
                {
                    string fileName = f.DirectoryName + "\\" + f.Name.Replace(oldString, newString);

                    if (!File.Exists(fileName))
                    {
                        File.Move(f.FullName, fileName);
                    }
                }
            }

            var folders = dirInfo.GetDirectories();

            foreach (var subFolder in folders)
            {
                SubstituirNomeArquivos(subFolder.FullName, oldString, newString, false);
            }

            if (dirInfo.Name.Contains(oldString))
            {
                string newDirectory = dirInfo.Parent.FullName + "\\" + dirInfo.Name.Replace(oldString, newString);

                Directory.Move(dirInfo.FullName, newDirectory);
            }
        }
    }
}
