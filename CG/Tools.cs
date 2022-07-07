using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CriPakTools
{
    public static class Tools
    {

        public class CacheFile
        {
            string storagePath;

            public struct pathObject
            {
           
                public string fileName;
                public string filePath;

                public pathObject(string n, string n2)
                {
                    fileName = n;
                    filePath = n2;
                }
            }
           public List<pathObject> content= new List<pathObject>();
            public CacheFile(string cacheFilePath)
            {
                storagePath = cacheFilePath;

                string[] file = File.ReadAllLines(cacheFilePath);
      
                if (file.Length % 2 != 0)
                    return;


                for (int i = 0; i < file.Length; i+= 2)
                {
                    string fName = file[i];
                    string fPath = file[i + 1];
                    if (!File.Exists(fPath))
                    {
                        content.Clear();
                        return;
                    }
                    content.Add(new pathObject(fName, fPath));

                         
                }
                
                   
                
            }

            public string[] buildWriteString()
            {
                List<string> write = new List<string>();

                for(int i = 0; i < content.Count; i++)
                {
                    write.Add(content[i].fileName);
                    write.Add(content[i].filePath);
                }

                return write.ToArray();
            }
            public void addPath(string f,string p)
            {
                bool doesAlreadyExist = false;

                for(int i = 0; i < content.Count; i++)
                {
                    if (f == content[i].fileName)
                    {
                        doesAlreadyExist = true;
                        break;
                    }
                }

                if (doesAlreadyExist)
                    return;

                if (content.Count < 5)
                {
                    content.Add(new pathObject(f,p));
                }
                else
                {
                    content[4] = new pathObject(f, p);
                }
            }


            public string getPath(string fileName)
            {

                for (int i = 0; i < content.Count; i++)
                {
                    if (content[i].fileName == fileName)
                    {
                        return content[i].filePath;
                    }
                }
                return "NULL";
            
            }

            public void saveCacheFile(string p)
            {
                File.WriteAllLines(p, buildWriteString());
            }
        }

        public  struct FileOfIntereset
        {
            public  string filePath;
            public int fileIndex;
            public FileOfIntereset(string n, int i)
            {
                filePath = n;
                fileIndex = i;
            }
        }

        public struct FileReplace
        {
            public string ins_name;
            public byte[] replaceWith;
            public string path;
            public FileReplace(string n, byte[] n2,string n3 = "")
            {
                ins_name = n;
                replaceWith = n2;
                path = n3;
            }
        }
       

        public static string ReadCString(BinaryReader br, int MaxLength = -1, long lOffset = -1, Encoding enc = null)
        {
            int Max;
            if (MaxLength == -1)
                Max = 255;
            else
                Max = MaxLength;

            long fTemp = br.BaseStream.Position;
            byte bTemp = 0;
            int i = 0;
            string result = "";

            if (lOffset > -1)
            {
                br.BaseStream.Seek(lOffset, SeekOrigin.Begin);
            }

            do
            {
                bTemp = br.ReadByte();
                if (bTemp == 0)
                    break;
                i += 1;
            } while (i < Max);

            if (MaxLength == -1)
                Max = i + 1;
            else
                Max = MaxLength;

            if (lOffset > -1)
            {
                br.BaseStream.Seek(lOffset, SeekOrigin.Begin);

                if (enc == null)
                    result = Encoding.GetEncoding("SJIS").GetString(br.ReadBytes(i));
                else
                    result = enc.GetString(br.ReadBytes(i));

                br.BaseStream.Seek(fTemp, SeekOrigin.Begin);
            }
            else
            {
                br.BaseStream.Seek(fTemp, SeekOrigin.Begin);
                if (enc == null)
                    result = Encoding.GetEncoding("SJIS").GetString(br.ReadBytes(i));
                else
                    result = enc.GetString(br.ReadBytes(i));

                br.BaseStream.Seek(fTemp + Max, SeekOrigin.Begin);
            }

            return result;
        }

        public static void DeleteFileIfExists(string sPath)
        {
            if (File.Exists(sPath))
                File.Delete(sPath);
        }

        public static string GetPath(string input)
        {
            return Path.GetDirectoryName(input) + "\\" + Path.GetFileNameWithoutExtension(input);
        }

        public static byte[] GetData(BinaryReader br, long offset, int size)
        {
            byte[] result = null;
            long backup = br.BaseStream.Position;
            br.BaseStream.Seek(offset, SeekOrigin.Begin);
            result = br.ReadBytes(size);
            br.BaseStream.Seek(backup, SeekOrigin.Begin);
            return result;
        }

    }
}
