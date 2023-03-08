

using System.IO;
using System.Windows.Forms;
using System;
using System.Net.WebSockets;

namespace floderhelper
{
    public partial class Form1 : Form
    {
        int[] datapointer = { 0, 0 };//�ļ�������λ��
        
        public Form1()
        {
            InitializeComponent();
            
        }

        private FileSystemInfo[] folderbase(string folderstr)//ͨ���ļ���·����ȡ�ļ�����Ŀ¼
        {
            
            
            DirectoryInfo dirs = new DirectoryInfo(folderstr);//
            FileSystemInfo[] fsis = dirs.GetFileSystemInfos();//��ȡ���ļ�����Ŀ¼,���Ϊ����
            return fsis;
            
            
            
        }

        private bool folderjudge(string folderpath)//�ж����ļ������ļ��У�trueΪ�ļ��У�falseΪ�ļ�
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

        private bool emptyjudge(string folderpath)//�򵥵��ж��ļ��Ƿ�Ϊ�գ������ڴ��ı��ļ�����������doc��xls��zip�ȵ�
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
            
            TreeNode Gen1=new TreeNode(folderstring);//�������ڵ�
            FileSystemInfo[] fsi0 = folderbase(folderstring);//1���ļ���
            richTextBox1.ForeColor = Color.Red;
            
            if (fsi0.Length == 0)
            {
                richTextBox1.AppendText ( folderstring + "Ϊ���ļ���"+"\r\n");
                Gen1.BackColor = Color.Red;
            }
            else
            {
                foreach (FileSystemInfo FSI in fsi0)
                {
                    TreeNode Gen2=new TreeNode(FSI.Name);
                    if (folderjudge(FSI.FullName))//������ļ��������ļ���
                    {
                        FileSystemInfo[] fsi1 = folderbase(FSI.FullName);//�����ļ���
                        if (fsi1.Length == 0) 
                        { 
                            richTextBox1.AppendText(FSI.FullName + "Ϊ���ļ���" + "\r\n"); 
                            Gen2.BackColor = Color.Red;
                        }
                        else
                        {
                            foreach(FileSystemInfo FSI1 in fsi1)
                            {
                                TreeNode Gen3=new TreeNode(FSI1.Name);
                                if (folderjudge(FSI1.FullName))//������ļ��е����ļ������ļ���
                                {
                                    FileSystemInfo[] fsi2 = folderbase(FSI1.FullName);//�����ļ���
                                    if (fsi2.Length == 0)
                                    {
                                        richTextBox1.AppendText(FSI1.FullName + "Ϊ���ļ���" + "\r\n");
                                        Gen3.BackColor = Color.Red;
                                    }
                                    else//������ļ��е����ļ��в�Ϊ��
                                    {
                                        foreach( FileSystemInfo FSI2 in fsi2)
                                        {
                                            TreeNode Gen4=new TreeNode(FSI2.Name);
                                            if (folderjudge(FSI2.FullName))//������ļе����ļ��е����ļ��������ļ���
                                            {
                                                FileSystemInfo[] fsi3 = folderbase(FSI2.FullName);//��ȡ���ļ��е����ļ��е����ļ��е����ļ�����Ϣ��4����
                                                if(fsi3.Length == 0)
                                                {
                                                    richTextBox1.AppendText(FSI2.FullName + "Ϊ���ļ���" + "\r\n");
                                                    Gen4.BackColor = Color.Red;
                                                }
                                                else
                                                {
                                                    foreach ( FileSystemInfo FSI3 in fsi3)
                                                    {
                                                        TreeNode Gen5=new TreeNode(FSI3.Name);
                                                        if (folderjudge(FSI3.FullName))//����ļ��ļ��е����ļ��������ļ���
                                                        {

                                                        }
                                                        else
                                                        {
                                                            if (emptyjudge(FSI3.FullName))
                                                            {
                                                                richTextBox1.AppendText(FSI3.FullName + "Ϊ���ļ�" + "\r\n");
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
                                                    richTextBox1.AppendText(FSI2.FullName + "Ϊ���ļ�" + "\r\n");
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
                    else//ֻ���ļ�
                    {
                        if (emptyjudge(FSI.FullName))
                        {
                            richTextBox1.AppendText(FSI.FullName + "Ϊ���ļ�" + "\r\n");
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
            treeView1.Nodes.Clear();//�����ͼ
            richTextBox1.Clear();//��ո��ı���
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                
                folderBrowserDialog.Description = "ѡ���Ŀ¼�ļ���";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    label1.Text = folderBrowserDialog.SelectedPath;
                    folderstate(folderBrowserDialog.SelectedPath);
                    //Read the contents of the file into a stream
                }
                else
                {
                    MessageBox.Show("��ѡ��·��");
                }
                
            }
            
            
        }

        private void ���ļ�ToolStripMenuItem_Click(object sender, EventArgs e)//��ѡ�е��ļ�
        {

        }

        private void ɾ���ļ�ToolStripMenuItem_Click(object sender, EventArgs e)//ɾ��ѡ�е��ļ�
        {
            MessageBox.Show(this.treeView1.SelectedNode.Text);
        }
    }
}