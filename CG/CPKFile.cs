/*
 * Created by SharpDevelop.
 * User: Unleashed
 * Date: 1/18/2019
 * Time: 10:58 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using CG;

using System.IO;


namespace CriPakTools
{
	public class CPKFile{
		
		public CPK cpk;
        string cpkPathGlob;
		public Boolean isInit = false;
	    public string extractPath { get; set; }
	    public BinaryReader br;

        public  BinaryWriter bw; // EndainWriter?
        public string CPKName;


 

       public   byte[] SDBHWH_CPK_KEY = {
	        0x58, 0x58, 0x0E, 0x6A, 0x58, 0x68, 0x3B, 0x66, 0x4E, 0x6B, 0x12, 0x79,
	        0x22, 0x2E, 0x0D, 0x0F, 0x7C, 0x11, 0x0D, 0x38, 0x2F, 0x63, 0x12, 0x75,
	        0x58, 0x79, 0x7C, 0x12, 0x5A, 0x65, 0x4A, 0x6E
        };
        public  byte[] SDBHWH_CPK_IV = {
	        	    0x58, 0x63, 0x6E, 0x7A, 0x78, 0x6A, 0x58, 0x65, 0x68, 0x43, 0x45, 0x47,
	                0x46, 0x46, 0x52, 0x37
            };





	   
	    public  byte[] chunk;
	    public int compressedSize;
        public int uncompressedSize;
        public string UserString;
		
		
		
		
		public CPKFile(){
			
			
		}
	    
		public bool readCPK(string cpkPath,string cpkName){


            CPKName = cpkName;
            cpkPathGlob = cpkPath;
            cpk = new CPK();
            bool res = cpk.ReadCPK(cpkPath);
            cpk.rootNode.Text = CPKName;
            if (SharedSettings.extractionPath.Length > 0)
            {
                extractPath = SharedSettings.extractionPath + @"\";
            
            }else{
            	   extractPath = cpkPath.Substring(0,cpkPath.Length - 4) + @"\";
            }
         
         
            br = new BinaryReader(File.OpenRead(cpkPath));

      
            //BinaryWriter b = new BinaryWriter(File.OpenWrite(cpkPath),FileMode.Open);
            	
            isInit = true;
            return res;
		}

        public void ModifyFileInCPK(int fileIndex,byte[] newFileBytes)
        {
           // bw.BaseStream.Seek((long)cpk.FileTable[fileIndex].FileOffset, SeekOrigin.Begin);

            int oldSize = Int32.Parse(cpk.FileTable[fileIndex].FileSize.ToString());
   


            if (oldSize != newFileBytes.Length) // file is less / bigger than, or compression...
            {
           
                DialogResult dialogResult = MessageBox.Show("Current File Size : " + oldSize.ToString() + " bytes\n" +
                    "New File Size " + newFileBytes.Length.ToString() + " bytes \n\n\n" +
                    "File size mismatch, the file might be less than / bigger than the size of the original file\nperhaps the file is compressed with CriLayla\nattempt to compress new file with"+
                    " Crilayla?", "Size mismatch", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)  // lets try compress with CRILAYLA
                {
                   
                 
                    newFileBytes = cpk.CompressCRILAYLA(newFileBytes);


                    if (oldSize != newFileBytes.Length) // still not same size?
                    {



                        MessageBox.Show("Current File Size : " + newFileBytes.Length.ToString() + " bytes\n\n" + "Size mismatch again. replacement cancelled\nthis file might have been compressed with a different version of Crilayla.", "Size mismatch",MessageBoxButtons.OK,MessageBoxIcon.Error);

                            return;
                        
                     
                    }
                    else // after regular compression, size match.. replace.
                    {
                        br.Close();
                        bw = new BinaryWriter(File.OpenWrite(cpkPathGlob));
                        bw.BaseStream.Seek((long)cpk.FileTable[fileIndex].FileOffset, SeekOrigin.Begin);
                        bw.Write(newFileBytes);
                        bw.Close();
                        br = new BinaryReader(File.OpenRead(cpkPathGlob));
                        MessageBox.Show("File replaced sucessfully", "Sucesss", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                      

 
                }
              

            }

                // bytes match, replace...
            else{
                br.Close();
                bw = new BinaryWriter(File.OpenWrite(cpkPathGlob));
                bw.BaseStream.Seek((long)cpk.FileTable[fileIndex].FileOffset, SeekOrigin.Begin);
                bw.Write(newFileBytes);
                bw.Close();
                br = new BinaryReader(File.OpenRead(cpkPathGlob));
                MessageBox.Show("File replaced sucessfully", "Sucesss", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


           
        }

        public byte[] GetFileBytesFromCPK(string file)
        {

            for (int i = 0; i < cpk.FileTable.Count; i++)
            {

                if (cpk.FileTable[i].DirName + @"/" + cpk.FileTable[i].FileName == file)
                {
                 

                    br.BaseStream.Seek((long)cpk.FileTable[i].FileOffset, SeekOrigin.Begin);
                    chunk = br.ReadBytes(Int32.Parse(cpk.FileTable[i].FileSize.ToString()));
                    compressedSize = Convert.ToInt32(cpk.FileTable[i].FileSize.ToString());
                    uncompressedSize = Convert.ToInt32(cpk.FileTable[i].ExtractSize.ToString());
           
                    //int size = Int32.Parse((cpk.FileTable[index].ExtractSize ?? cpk.FileTable[index].FileSize).ToString());
                    chunk = cpk.DecompressCRILAYLA(chunk, compressedSize, uncompressedSize,8); // decompress the chunk


                    return chunk;
                }

            }
            return null;


        }

          public Tools.FileOfIntereset[] getFilePath(string file,StringComparison sc)
          {
              List<Tools.FileOfIntereset> sl = new List<Tools.FileOfIntereset>();
              //MessageBox.Show(cpk.FileTable.Count.ToString());
              Tools.FileOfIntereset tfoi;
              for (int i = 0; i < cpk.FileTable.Count; i++)
              {

                  if (string.Format("{0}/{1}", cpk.FileTable[i].DirName, cpk.FileTable[i].FileName).IndexOf(file, sc) >= 0)
                  {
                      tfoi = new Tools.FileOfIntereset(string.Format("{0}/{1}", cpk.FileTable[i].DirName, cpk.FileTable[i].FileName),
                       i);
                      sl.Add(tfoi);
                  }
                
                
              }
              return sl.ToArray();
          }

          public void ExtractWithPath(string path)
          {
              //MessageBox.Show(path);

              for (int i = 0; i < cpk.FileTable.Count; i++)
              {
                  if (cpk.FileTable[i].DirName != null) { 
                      if (cpk.FileTable[i].DirName.ToString().Contains(path))
                      {
                    
                          ExtractByIndex(i);
                      }
                  }
              }

          }
        
	    public void ExtractRange(string path,int startIndex,int endIndex = -1){
	    	if (endIndex == -1){ // worst case
	    		for(int i = startIndex; i < cpk.FileTable.Count;i++){
	    			
	    			if (string.Format("{0}/{1}",cpk.FileTable[i].DirName, cpk.FileTable[i].FileName).Contains(path))
	    				ExtractByIndex(i);
	    				else
	    					return;
	    		}
	    		
	    	} 
	    	else{
	    		// best case
	    		for (int i = startIndex; i < endIndex;i++){
							ExtractByIndex(i);
	    			}
	    		

			}
	    	
	    }
	    
		public void ExtractAll(){
			
			for (int i = 0; i < cpk.FileTable.Count;i++){
				
                    if(cpk.FileTable[i].DirName != null ){ // actual file and not a header
					ExtractByIndex(i);
                    
                 }
			}
		}
        public byte[] getChunk(string fileName)
        {
            int index = -1;
            if (fileName.Contains("CONTENT"))
                return null;
            for (int i = 0; i < cpk.FileTable.Count; i++)
            {

                if (cpk.FileTable[i].FileName.ToString() == fileName)
                { 
                    index = i;
                    break;

                }
            }

            if (index > -1)
            {
                try
                {
                    br.BaseStream.Seek((long)cpk.FileTable[index].FileOffset, SeekOrigin.Begin);
                    return br.ReadBytes(Int32.Parse(cpk.FileTable[index].FileSize.ToString()));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(index.ToString());
                }
              
            }
            return null;
        }
		public void ExtractByIndex(int index){

					   br.BaseStream.Seek((long)cpk.FileTable[index].FileOffset, SeekOrigin.Begin);
                       long size = Int64.Parse(cpk.FileTable[index].FileSize.ToString());
        
                       if (size <= (long)int.MaxValue)
                       {                    
                       chunk = br.ReadBytes(Int32.Parse(cpk.FileTable[index].FileSize.ToString()));
                       }else{
                           List<byte> rawBytes = new List<byte>();
                           rawBytes.AddRange(br.ReadBytes(int.MaxValue));
                          size -= int.MaxValue;
                           if (size > int.MaxValue)
                            return;
                          rawBytes.AddRange(br.ReadBytes((int)size));
                           chunk = rawBytes.ToArray();
                       }
       
                       compressedSize = Convert.ToInt32(cpk.FileTable[index].FileSize.ToString());
                       uncompressedSize = Convert.ToInt32(cpk.FileTable[index].ExtractSize.ToString());
                       UserString = cpk.FileTable[index].UserString.ToString();
                     //  MessageBox.Show(cpk.FileTable[index].FileOffset.ToString("X"));
                       //MessageBox.Show(cpk.FileTable[index].FileSize.ToString());
                       //MessageBox.Show(cpk.FileTable[index].ExtractSize.ToString());
                     //int size = Int32.Parse((cpk.FileTable[index].ExtractSize ?? cpk.FileTable[index].FileSize).ToString());
                       if (SharedSettings.encType == 1)
                           chunk = AES.AesCtrDecrypt(SDBHWH_CPK_KEY, SDBHWH_CPK_IV, chunk,UserString);
                       chunk = cpk.DecompressCRILAYLA(chunk,compressedSize,uncompressedSize,8); // decompress the chunk
                       chunk = cpk.DecompressCRILAYLA(chunk, compressedSize, uncompressedSize, 9); // decompress the chunk (CRILAYLA2)

                       Directory.CreateDirectory(extractPath + cpk.FileTable[index].DirName.ToString());
                       File.WriteAllBytes(string.Format("{0}{1}/{2}", extractPath, cpk.FileTable[index].DirName.ToString(), cpk.FileTable[index].FileName.ToString()), chunk);
   }
		}

    
		
		
	}

