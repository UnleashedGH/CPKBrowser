using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.IO.Compression;



namespace CG
{
    public class M2C
    {



        public class ReplaceFile
        {
            //DXT5 Decompressed
            public byte[] m_ReplaceData;
            public string m_ReplaceName;
 
      



            //public byte[] ddsPixels
            //{
            //    get;
            //    set;

            //}
            public ReplaceFile(byte[] data)
            {


                m_ReplaceData = data;
         
                //MessageBox.Show(m_img.NumMipMaps.ToString());





            }
         

        }

        public List<ReplaceFile> ReplaceFiles = new List<ReplaceFile>();
    



        public M2C()
        {


        }
        public void M2CRead(string path)
        {

         
            using (ZipArchive archive = ZipFile.OpenRead(path))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                
                    //if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                    //{
                    //    // Gets the full path to ensure that relative segments are removed.
                    //    string destinationPath = Path.GetFullPath(Path.Combine(extractPath, entry.FullName));

                    //    // Ordinal match is safest, case-sensitive volumes can be mounted within volumes that
                    //    // are case-insensitive.
                    //    if (destinationPath.StartsWith(extractPath, StringComparison.Ordinal))
                    //        entry.ExtractToFile(destinationPath);
                    //}
                }
            }

        }

        public void M2CReadWrite(string path)
        {

        }

    }
}
