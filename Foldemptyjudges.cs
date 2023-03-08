using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace floderhelper
{
    internal class Foldemptyjudges//用于判断文件类型并检测其是否为空(未完成）
    {
        public static bool IsAllowedExtension(string filepath)
        {
            FileStream stream=new FileStream(filepath, FileMode.Open, FileAccess.Read);
            BinaryReader reader=new BinaryReader(stream);
            string fileclass = "";
            try
            {
                for (int i = 0; i < 2; i++)
                {
                    fileclass+=reader.ReadByte().ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            FileInfo fis = new FileInfo(filepath);
            /*
            4946/1041116 txt
            7173         gif
            255216       jpg
            13780       png
            6677        bmp
            239187      txt,aspx,asp,sql
            08207       xls,doc,ppt
            6063        xml
            6033        htm,html
            4742        js
            8075        xlsx,zip,pptx,mmap,zip
            8297        rar
            01          accdb,mdb
            7790        exe,dll
            5666        psd
            255254      rdp
            10056       bt种子
            64101       bat
            4059        sgf
             
             */
            if (fileclass == "255216")
            {
                
            }
            return true;
        }

    }
}
