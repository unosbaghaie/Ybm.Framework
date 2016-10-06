using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ybm.CodeGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static List<string> FileNames = new List<string>();
        private void buttonRun_Click(object sender, EventArgs e)
        {
            var basePath = Path.GetFullPath(Application.StartupPath) + "\\GeneratedCode";
            
            var textBoxBusinessPath = basePath + "\\Business\\Concrete\\";
            var textBoxIBusinessPath = basePath + "\\Business\\Interface\\";
            var textBoxMapperPath = basePath + "\\Mapper\\";
            var textBoxViewModelPath = basePath + "\\ViewModels\\";
            var textBoxMvcControllerPath = basePath + "\\MvcController\\";

            foreach (var path in new[] { textBoxBusinessPath, textBoxIBusinessPath, textBoxMapperPath, textBoxViewModelPath , textBoxMvcControllerPath })
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);




            var files = Directory.GetFiles(textBoxModelPath.Text, "*.cs", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                FileNames.Add(Path.GetFileNameWithoutExtension(file));
            }



                new Business(textBoxModelPath.Text,textBoxBusinessPath).Run();
            new IBusiness(textBoxModelPath.Text, textBoxIBusinessPath).Run();
            new Mapper(textBoxModelPath.Text, textBoxMapperPath).Run();
            new ViewModel(textBoxEntityDLLPath.Text, textBoxViewModelPath).Run();
            new MvcController(textBoxModelPath.Text, textBoxMvcControllerPath).Run();
            new IndexView(textBoxEntityDLLPath.Text, textBoxMvcControllerPath).Run();
            new AngularIndex(textBoxEntityDLLPath.Text, textBoxMvcControllerPath).Run();

            Process.Start(basePath);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            var info = File.ReadAllLines("info.txt");
            if (info != null && info.Any())
            {
                textBoxModelPath.Text = info[0];
                textBoxEntityDLLPath.Text = info[1];
            }
        }

        private void buttonSetpath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                textBoxModelPath.Text = f.SelectedPath; 
            }
        }

        private void buttonSetDllPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.DefaultExt = "*.dll";
            if (f.ShowDialog() == DialogResult.OK)
            {
                textBoxEntityDLLPath.Text = f.FileName;
            }
        }
    }
}
