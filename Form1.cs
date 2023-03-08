

using System.IO;
using System.Windows.Forms;
using System;
using System.Net.WebSockets;

namespace floderhelper
{
    public partial class Form1 : Form
    {
        int[] datapointer = { 0, 0 };//文件名放置位置
        
        public Form1()
        {
            InitializeComponent();
            
        }

        private FileSystemInfo[] folderbase(string folderstr)//通过文件夹路径获取文件夹下目录
        {
            
            
            DirectoryInfo dirs = new DirectoryInfo(folderstr);//
            FileSystemInfo[] fsis = dirs.GetFileSystemInfos();//获取该文件夹下目录,结果为数组
            return fsis;
            
            
            
        }

        private bool folderjudge(string folderpath)//判断是文件还是文件夹，true为文件夹，false为文件
        {
            if((File.GetAttributes(folderpath) & FileAttributes.Directory) == FileAttributes.Directory)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool emptyjudge(string folderpath)//简单地判断文件是否为空，适用于纯文本文件，不适用于doc，xls，zip等等
        {
            FileInfo fis=new FileInfo(folderpath);
            if (fis.Length == 0)
            {
                return true;
            }
            else return false;
        }

        private void folderstate(string folderstring)
        {
            
            TreeNode Gen1=new TreeNode(folderstring);//创建根节点
            FileSystemInfo[] fsi0 = folderbase(folderstring);//1级文件夹
            richTextBox1.ForeColor = Color.Red;
            
            if (fsi0.Length == 0)
            {
                richTextBox1.AppendText ( folderstring + "为空文件夹"+"\r\n");
                Gen1.BackColor = Color.Red;
            }
            else
            {
                foreach (FileSystemInfo FSI in fsi0)
                {
                    TreeNode Gen2=new TreeNode(FSI.Name);
                    if (folderjudge(FSI.FullName))//如果子文件夹仍是文件夹
                    {
                        FileSystemInfo[] fsi1 = folderbase(FSI.FullName);//二级文件夹
                        if (fsi1.Length == 0) 
                        { 
                            richTextBox1.AppendText(FSI.FullName + "为空文件夹" + "\r\n"); 
                            Gen2.BackColor = Color.Red;
                        }
                        else
                        {
                            foreach(FileSystemInfo FSI1 in fsi1)
                            {
                                TreeNode Gen3=new TreeNode(FSI1.Name);
                                if (folderjudge(FSI1.FullName))//如果子文件夹的子文件仍是文件夹
                                {
                                    FileSystemInfo[] fsi2 = folderbase(FSI1.FullName);//三级文件夹
                                    if (fsi2.Length == 0)
                                    {
                                        richTextBox1.AppendText(FSI1.FullName + "为空文件夹" + "\r\n");
                                        Gen3.BackColor = Color.Red;
                                    }
                                    else//如果子文件夹的子文件夹不为空
                                    {
                                        foreach( FileSystemInfo FSI2 in fsi2)
                                        {
                                            TreeNode Gen4=new TreeNode(FSI2.Name);
                                            if (folderjudge(FSI2.FullName))//如果子文夹的子文件夹的子文件夹仍是文件夹
                                            {
                                                FileSystemInfo[] fsi3 = folderbase(FSI2.FullName);//获取子文件夹的子文件夹的子文件夹的子文件夹信息（4级）
                                                if(fsi3.Length == 0)
                                                {
                                                    richTextBox1.AppendText(FSI2.FullName + "为空文件夹" + "\r\n");
                                                    Gen4.BackColor = Color.Red;
                                                }
                                                else
                                                {
                                                    foreach ( FileSystemInfo FSI3 in fsi3)
                                                    {
                                                        TreeNode Gen5=new TreeNode(FSI3.Name);
                                                        if (folderjudge(FSI3.FullName))//如果四级文件夹的子文件夹仍是文件夹
                                                        {

                                                        }
                                                        else
                                                        {
                                                            if (emptyjudge(FSI3.FullName))
                                                            {
                                                                richTextBox1.AppendText(FSI3.FullName + "为空文件" + "\r\n");
                                                                Gen5.BackColor = Color.Purple;
                                                            }
                                                        }
                                                        Gen4.Nodes.Add(Gen5);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (emptyjudge(FSI2.FullName))
                                                {
                                                    richTextBox1.AppendText(FSI2.FullName + "为空文件" + "\r\n");
                                                    Gen4.BackColor = Color.Purple;
                                                }
                                            }
                                            Gen3.Nodes.Add(Gen4);
                                        }
                                    }
                                }
                                else
                                {

                                }
                                Gen2.Nodes.Add(Gen3);

                            }
                        }

                    }
                    else//只是文件
                    {
                        if (emptyjudge(FSI.FullName))
                        {
                            richTextBox1.AppendText(FSI.FullName + "为空文件" + "\r\n");
                            Gen2.BackColor = Color.Purple;
                        } 
                    }
                    Gen1.Nodes.Add(Gen2);
                    
                }
            }
            treeView1.Nodes.Add(Gen1);
            treeView1.ExpandAll();



        }


        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();//清空树图
            richTextBox1.Clear();//清空富文本框
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                
                folderBrowserDialog.Description = "选择根目录文件夹";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    label1.Text = folderBrowserDialog.SelectedPath;
                    folderstate(folderBrowserDialog.SelectedPath);
                    //Read the contents of the file into a stream
                }
                else
                {
                    MessageBox.Show("请选择路径");
                }
                
            }
            
            
        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)//打开选中的文件
        {

        }

        private void 删除文件ToolStripMenuItem_Click(object sender, EventArgs e)//删除选中的文件
        {
            MessageBox.Show(this.treeView1.SelectedNode.Text);
        }
    }
}