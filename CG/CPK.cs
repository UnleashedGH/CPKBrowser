using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace CriPakTools
{
    public class CPK
    {
        public List<FileEntry> FileTable;
        public TreeNode rootNode;
        public ulong  CPK_Files_Size = 0;
        int IndexOfFileTable;
        string cName;


        UTF utf;
        UTF files;

        public CPK()
        {
           
            isUtfEncrypted = false;

            CPK_Files_Size = 0;
            FileTable = new List<FileEntry>();
            rootNode = new TreeNode();

           
      
          
        }

        
      
        public bool ReadCPK(string sPath)
        {
            if (File.Exists(sPath))
            {
                uint Files;
                ushort Align;
                cName = sPath;
                EndianReader br = new EndianReader(File.OpenRead(sPath), true);
                MemoryStream ms;
                EndianReader utfr;
                //   MessageBox.Show((br.BaseStream.Position).ToString());
                if (Tools.ReadCString(br, 4) != "CPK ")
                {
                    br.Close();
                    return false;
                }
                // MessageBox.Show((br.BaseStream.Position).ToString());
                ReadUTFData(br);
                //   MessageBox.Show((br.BaseStream.Position).ToString());
                CPK_packet = utf_packet; // Unleashed : so this is the CPK UTF Table

                 // MessageBox.Show((br.BaseStream.Position + 0x10).ToString());
                //File.WriteAllBytes("CPK_PACK.d", CPK_packet);
               // MessageBox.Show((CPK_packet.Length).ToString());
                FileEntry CPAK_entry = new FileEntry
                {
                    FileName = "CPK_HDR",
                    FileOffsetPos = br.BaseStream.Position + 0x10,
                    FileSize = CPK_packet.Length,
                    Encrypted = isUtfEncrypted,
                    FileType = "CPK"
                };

                FileTable.Add(CPAK_entry);

                ms = new MemoryStream(utf_packet);
                utfr = new EndianReader(ms, false);

                utf = new UTF();
                if (!utf.ReadUTF(utfr))
                {
                    br.Close();
                    return false;
                }

                utfr.Close();
                ms.Close();

          

          

                TocOffset = (ulong)GetColumsData(utf, 0, "TocOffset", 3);
                long TocOffsetPos = GetColumnPostion(utf, 0, "TocOffset");

                EtocOffset = (ulong)GetColumsData(utf, 0, "EtocOffset", 3);
                long ETocOffsetPos = GetColumnPostion(utf, 0, "EtocOffset");

                ItocOffset = (ulong)GetColumsData(utf, 0, "ItocOffset", 3);
                long ITocOffsetPos = GetColumnPostion(utf, 0, "ItocOffset");

                GtocOffset = (ulong)GetColumsData(utf, 0, "GtocOffset", 3);
                long GTocOffsetPos = GetColumnPostion(utf, 0, "GtocOffset");

                ContentOffset = (ulong)GetColumsData(utf, 0, "ContentOffset", 3);
                long ContentOffsetPos = GetColumnPostion(utf, 0, "ContentOffset");
                FileTable.Add(CreateFileEntry("CONTENT_OFFSET", ContentOffset, typeof(ulong), ContentOffsetPos, "CPK", "CONTENT", false));

                Files = (uint)GetColumsData(utf, 0, "Files", 2);
                Align = (ushort)GetColumsData(utf, 0, "Align", 1);

                if (TocOffset != 0xFFFFFFFFFFFFFFFF)
                {
                    FileEntry entry = CreateFileEntry("TOC_HDR", TocOffset, typeof(ulong), TocOffsetPos, "CPK", "HDR", false);
                    FileTable.Add(entry);

                    if (!ReadTOC(br, TocOffset, ContentOffset))
                        return false;
                }

                if (EtocOffset != 0xFFFFFFFFFFFFFFFF)
                {
                    FileEntry entry = CreateFileEntry("ETOC_HDR", EtocOffset, typeof(ulong), ETocOffsetPos, "CPK", "HDR", false);
                    FileTable.Add(entry);

                    if (!ReadETOC(br, EtocOffset))
                        return false;
                }

                if (ItocOffset != 0xFFFFFFFFFFFFFFFF)
                {
                    //FileEntry ITOC_entry = new FileEntry { 
                    //    FileName = "ITOC_HDR",
                    //    FileOffset = ItocOffset, FileOffsetType = typeof(ulong), FileOffsetPos = ITocOffsetPos,
                    //    TOCName = "CPK",
                    //    FileType = "FILE", Encrypted = true,
                    //};

                    FileEntry entry = CreateFileEntry("ITOC_HDR", ItocOffset, typeof(ulong), ITocOffsetPos, "CPK", "HDR", false);
                    FileTable.Add(entry);

                    if (!ReadITOC(br, ItocOffset, ContentOffset, Align))
                        return false;
                }

                if (GtocOffset != 0xFFFFFFFFFFFFFFFF)
                {
                    FileEntry entry = CreateFileEntry("GTOC_HDR", GtocOffset, typeof(ulong), GTocOffsetPos, "CPK", "HDR", false);
                    FileTable.Add(entry);

                    if (!ReadGTOC(br, GtocOffset))
                        return false;
                }

                br.Close();

                // at this point, we should have all needed file info

                //utf = null;
                files = null;
                return true;
            }
            return false;
        }

        FileEntry CreateFileEntry(string FileName, ulong FileOffset, Type FileOffsetType, long FileOffsetPos, string TOCName, string FileType, bool encrypted)
        {
            FileEntry entry = new FileEntry
            {
                FileName = FileName,
                FileOffset = FileOffset,
                FileOffsetType = FileOffsetType,
                FileOffsetPos = FileOffsetPos,
                TOCName = TOCName,
                FileType = FileType,
                Encrypted = encrypted,
            };

            return entry;
        }

        public bool ReadTOC(EndianReader br, ulong TocOffset, ulong ContentOffset)
        {
            ulong add_offset = 0;

            if (ContentOffset < 0)
                add_offset = TocOffset;
            else
            {
                if (TocOffset < 0)
                    add_offset = ContentOffset;
                else
                {
                    if (ContentOffset < TocOffset)
                        add_offset = ContentOffset;
                    else
                        add_offset = TocOffset;
                }
            }

            br.BaseStream.Seek((long)TocOffset, SeekOrigin.Begin);

            if (Tools.ReadCString(br, 4) != "TOC ")
            {
                br.Close();
                return false;
            }

            ReadUTFData(br);

            // Store unencrypted TOC
            TOC_packet = utf_packet;
            //Dump TOC
            //File.WriteAllBytes("U_TOC", TOC_packet);

            FileEntry toc_entry = FileTable.Where(x => x.FileName.ToString() == "TOC_HDR").Single();
            toc_entry.Encrypted = isUtfEncrypted;
            toc_entry.FileSize = TOC_packet.Length;

            MemoryStream ms = new MemoryStream(utf_packet);
            EndianReader utfr = new EndianReader(ms, false);

            files = new UTF();
            if (!files.ReadUTF(utfr))
            {
                br.Close();
                return false;
            }

            utfr.Close();
            ms.Close();

            FileEntry temp;
            
            // my crappy code
            IndexOfFileTable = FileTable.Count - 1; // current Index of Table of Contents (TOC)

            string FullPath;
            string[] pathParts;
 			
            	TreeNode currentnode;
		        //const char Separator = '/';
                char[] delimiters = new char[] { '/' };
            // my crappy code
            for (int i = 0; i < files.num_rows; i++)
            {
                temp = new FileEntry();

                temp.TOCName = "TOC";

                temp.DirName = GetColumnData(files, i, "DirName");
                temp.FileName = GetColumnData(files, i, "FileName");

                temp.FileSize = GetColumnData(files, i, "FileSize");
                temp.FileSizePos = GetColumnPostion(files, i, "FileSize");
                temp.FileSizeType = GetColumnType(files, i, "FileSize");

                temp.ExtractSize = GetColumnData(files, i, "ExtractSize");
                temp.ExtractSizePos = GetColumnPostion(files, i, "ExtractSize");
                temp.ExtractSizeType = GetColumnType(files, i, "ExtractSize");

                 temp.FileOffset = ((ulong)GetColumnData(files, i, "FileOffset") + (ulong)add_offset);
                temp.FileOffsetPos = GetColumnPostion(files, i, "FileOffset");
                temp.FileOffsetType = GetColumnType(files, i, "FileOffset");

                temp.FileType = "FILE";

                temp.Offset = add_offset;

                temp.ID = GetColumnData(files, i, "ID");
                temp.UserString = GetColumnData(files, i, "UserString");
                
              FileTable.Add(temp);
              IndexOfFileTable++;
              CPK_Files_Size += Convert.ToUInt64(temp.ExtractSize);
               //do work..
              
               FullPath = string.Format("{0}/{1}",temp.DirName,temp.FileName);
               pathParts = FullPath.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                   currentnode = rootNode;
                 //  MessageBox.Show(FullPath);
                   
                   for(int j = 0; j < pathParts.Length;j++){
                 	//MessageBox.Show(pathParts[j]);
                   	if (null == currentnode.Nodes[pathParts[j]])
		                {

                           

                            //MessageBox.Show("FULL PATH : " + FullPath);
                            //MessageBox.Show("PART PATH: " + pathParts[j] + " " + j.ToString());
                        
                   		if (endsWithExtension(pathParts[j]) || (pathParts[j] == temp.FileName.ToString())){
                   		    	currentnode.Tag += string.Format("{0},",IndexOfFileTable);
                   		  }
                   		  
                   		  else{
                   		  	
                   		  	currentnode = currentnode.Nodes.Add(pathParts[j], pathParts[j]);
                   		  }


                   		}


		          	 else{
		               		currentnode = currentnode.Nodes[pathParts[j]];  
                   		 }
                   	currentnode.ImageIndex = 0;
                   	
		            }
 
            }
               
            
            files = null;
          

            
            return true;
        }
    
     
        bool endsWithExtension(string n){
        	//return (n.Substring(n.Length - 4,n.Length)[0] == '.');
        	return (n.Contains("."));
        }

        public void WriteCPK(BinaryWriter cpk)
        {
            WritePacket(cpk, "CPK ", 0, CPK_packet);

            cpk.BaseStream.Seek(0x800 - 6, SeekOrigin.Begin);
            cpk.Write(Encoding.ASCII.GetBytes("(c)CRI"));
        }

        public void WriteTOC(BinaryWriter cpk)
        {
            WritePacket(cpk, "TOC ", TocOffset, TOC_packet);
        }

        public void WriteITOC(BinaryWriter cpk)
        {
            WritePacket(cpk, "ITOC", ItocOffset, ITOC_packet);
        }

        public void WriteETOC(BinaryWriter cpk)
        {
            WritePacket(cpk, "ETOC", EtocOffset, ETOC_packet);
        }

        public void WriteGTOC(BinaryWriter cpk)
        {
            WritePacket(cpk, "GTOC", GtocOffset, GTOC_packet);
        }

        public void WritePacket(BinaryWriter cpk, string ID, ulong position, byte[] packet)
        {
            if (position != 0xffffffffffffffff)
            {
                cpk.BaseStream.Seek((long)position, SeekOrigin.Begin);
                //UNLEASHED: for debugging purposes, lets store this without encryption.
                byte[] encrypted = DecryptUTF(packet); // Yes it says decrypt...  

                //byte[] encrypted =(packet);
                cpk.Write(Encoding.ASCII.GetBytes(ID));
                cpk.Write((Int32)0);

       
                cpk.Write((UInt64)encrypted.Length);
                cpk.Write(encrypted);
            }
        }

        public bool ReadITOC(EndianReader br, ulong startoffset, ulong ContentOffset, ushort Align)
        {
            br.BaseStream.Seek((long)startoffset, SeekOrigin.Begin);

            if (Tools.ReadCString(br, 4) != "ITOC")
            {
                br.Close();
                return false;
            }

            ReadUTFData(br);

            ITOC_packet = utf_packet;
            //Dump ITOC
            //UNLEASHED: HERE!!!!
            //File.WriteAllBytes("U_ITOC", ITOC_packet);

            FileEntry itoc_entry = FileTable.Where(x => x.FileName.ToString() == "ITOC_HDR").Single();
            itoc_entry.Encrypted = isUtfEncrypted;
            itoc_entry.FileSize = ITOC_packet.Length;

            MemoryStream ms = new MemoryStream(utf_packet);
            EndianReader utfr = new EndianReader(ms, false);

            files = new UTF();
            if (!files.ReadUTF(utfr))
            {
                br.Close();
                return false;
            }

            utfr.Close();
            ms.Close();

            //uint FilesL = (uint)GetColumnData(files, 0, "FilesL");
            //uint FilesH = (uint)GetColumnData(files, 0, "FilesH");
            byte[] DataL = (byte[])GetColumnData(files, 0, "DataL");
            long DataLPos = GetColumnPostion(files, 0, "DataL");

            byte[] DataH = (byte[])GetColumnData(files, 0, "DataH");
            long DataHPos = GetColumnPostion(files, 0, "DataH");

            //MemoryStream ms;
            //EndianReader ir;
            UTF utfDataL, utfDataH;
            Dictionary<int, uint> SizeTable, CSizeTable;
            Dictionary<int, long> SizePosTable, CSizePosTable;
            Dictionary<int, Type> SizeTypeTable, CSizeTypeTable;

            List<int> IDs = new List<int>();

            SizeTable = new Dictionary<int, uint>();
            SizePosTable = new Dictionary<int, long>();
            SizeTypeTable = new Dictionary<int, Type>();

            CSizeTable = new Dictionary<int, uint>();
            CSizePosTable = new Dictionary<int, long>();
            CSizeTypeTable = new Dictionary<int, Type>();

            ushort ID, size1;
            uint size2;
            long pos;
            Type type;

            if (DataL != null)
            {
                ms = new MemoryStream(DataL);
                utfr = new EndianReader(ms, false);
                utfDataL = new UTF();
                utfDataL.ReadUTF(utfr);

                for (int i = 0; i < utfDataL.num_rows; i++)
                {
                    ID = (ushort)GetColumnData(utfDataL, i, "ID");
                    size1 = (ushort)GetColumnData(utfDataL, i, "FileSize");
                    SizeTable.Add((int)ID, (uint)size1);

                    pos = GetColumnPostion(utfDataL, i, "FileSize");
                    SizePosTable.Add((int)ID, pos + DataLPos);

                    type = GetColumnType(utfDataL, i, "FileSize");
                    SizeTypeTable.Add((int)ID, type);

                    if ((GetColumnData(utfDataL, i, "ExtractSize")) != null)
                    {
                        size1 = (ushort)GetColumnData(utfDataL, i, "ExtractSize");
                        CSizeTable.Add((int)ID, (uint)size1);

                        pos = GetColumnPostion(utfDataL, i, "ExtractSize");
                        CSizePosTable.Add((int)ID, pos + DataLPos);

                        type = GetColumnType(utfDataL, i, "ExtractSize");
                        CSizeTypeTable.Add((int)ID, type);
                    }

                    IDs.Add(ID);
                }
            }

            if (DataH != null)
            {
                ms = new MemoryStream(DataH);
                utfr = new EndianReader(ms, false);
                utfDataH = new UTF();
                utfDataH.ReadUTF(utfr);

                for (int i = 0; i < utfDataH.num_rows; i++)
                {
                    ID = (ushort)GetColumnData(utfDataH, i, "ID");
                    size2 = (uint)GetColumnData(utfDataH, i, "FileSize");
                    SizeTable.Add(ID, size2);

                    pos = GetColumnPostion(utfDataH, i, "FileSize");
                    SizePosTable.Add((int)ID, pos + DataHPos);

                    type = GetColumnType(utfDataH, i, "FileSize");
                    SizeTypeTable.Add((int)ID, type);

                    if ((GetColumnData(utfDataH, i, "ExtractSize")) != null)
                    {
                        size2 = (uint)GetColumnData(utfDataH, i, "ExtractSize");
                        CSizeTable.Add(ID, size2);

                        pos = GetColumnPostion(utfDataH, i, "ExtractSize");
                        CSizePosTable.Add((int)ID, pos + DataHPos);

                        type = GetColumnType(utfDataH, i, "ExtractSize");
                        CSizeTypeTable.Add((int)ID, type);
                    }

                    IDs.Add(ID);
                }
            }

            FileEntry temp;
            //int id = 0;
            uint value = 0, value2 = 0;
            ulong baseoffset = ContentOffset;

            // Seems ITOC can mix up the IDs..... but they'll alwaysy be in order...
            IDs = IDs.OrderBy(x => x).ToList();


            for (int i = 0; i < IDs.Count; i++)
            {
                int id = IDs[i];

                temp = new FileEntry();
                SizeTable.TryGetValue(id, out value);
                CSizeTable.TryGetValue(id, out value2);

                temp.TOCName = "ITOC";

                temp.DirName = null;
                temp.FileName = id.ToString("D4");

                temp.FileSize = value;
                temp.FileSizePos = SizePosTable[id];
                temp.FileSizeType = SizeTypeTable[id];

                if (CSizeTable.Count > 0 && CSizeTable.ContainsKey(id))
                {
                    temp.ExtractSize = value2;
                    temp.ExtractSizePos = CSizePosTable[id];
                    temp.ExtractSizeType = CSizeTypeTable[id];
                }

                temp.FileType = "FILE";


                temp.FileOffset = baseoffset;
                temp.ID = id;
                temp.UserString = null;

                FileTable.Add(temp);

                if ((value % Align) > 0)
                    baseoffset += value + (Align - (value % Align));
                else
                    baseoffset += value;


                //id++;
            }

            files = null;
            utfDataL = null;
            utfDataH = null;
          
            ms.Close();
            utfr.Close();


            return true;
        }

        private void ReadUTFData(EndianReader br)
        {
            isUtfEncrypted = false;
            br.IsLittleEndian = true;

            unk1 = br.ReadInt32();
            utf_size = br.ReadInt64();
            utf_packet = br.ReadBytes((int)utf_size);

            if (utf_packet[0] != 0x40 && utf_packet[1] != 0x55 && utf_packet[2] != 0x54 && utf_packet[3] != 0x46) //@UTF
            {
                utf_packet = DecryptUTF(utf_packet);
                isUtfEncrypted = true;
            }

            br.IsLittleEndian = false;
        }

        public bool ReadGTOC(EndianReader br, ulong startoffset)
        {
            br.BaseStream.Seek((long)startoffset, SeekOrigin.Begin);

            if (Tools.ReadCString(br, 4) != "GTOC")
            {
                br.Close();
                return false;
            }

            ReadUTFData(br);

            GTOC_packet = utf_packet;
            FileEntry gtoc_entry = FileTable.Where(x => x.FileName.ToString() == "GTOC_HDR").Single();
            gtoc_entry.Encrypted = isUtfEncrypted;
            gtoc_entry.FileSize = GTOC_packet.Length;


            return true;
        }

        public bool ReadETOC(EndianReader br, ulong startoffset)
        {
            br.BaseStream.Seek((long)startoffset, SeekOrigin.Begin);

            if (Tools.ReadCString(br, 4) != "ETOC")
            {
                br.Close();
                return false;
            }

            //br.BaseStream.Seek(0xC, SeekOrigin.Current); //skip header data

            ReadUTFData(br);

            ETOC_packet = utf_packet;
            //Dump ETOC
            //File.WriteAllBytes("U_ETOC", ETOC_packet);

            FileEntry etoc_entry = FileTable.Where(x => x.FileName.ToString() == "ETOC_HDR").Single();
            etoc_entry.Encrypted = isUtfEncrypted;
            etoc_entry.FileSize = ETOC_packet.Length;

            MemoryStream ms = new MemoryStream(utf_packet);
            EndianReader utfr = new EndianReader(ms, false);

            files = new UTF();
            if (!files.ReadUTF(utfr))
            {
                br.Close();
                return false;
            }

            utfr.Close();
            ms.Close();

            List<FileEntry> fileEntries = FileTable.Where(x => x.FileType == "FILE").ToList();

            for (int i = 0; i < fileEntries.Count; i++)
            {
                FileTable[i].LocalDir = GetColumnData(files, i, "LocalDir");
            }

            return true;
        }

        public byte[] DecryptUTF(byte[] input)
        {

            byte[] result = new byte[input.Length];

            int m, t;
            byte d;

            m = 0x0000655f;
            t = 0x00004115;
            
            for (int i = 0; i < input.Length; i++)
            {
   
                d = input[i];
                d = (byte)(d ^ (byte)(m & 0xff));
                result[i] = d;
                m = unchecked(m * t);

            }

            return result;
        }





        unsafe public int CRICompress(byte* dest, int* destLen, byte* src, int srcLen)
     {
         int n = srcLen - 1, m = *destLen - 0x1, T = 0, d = 0;
         int p, q = 0, i, j, k;
         byte* odest = dest;
         for (; n >= 0x100; )
         {
             j = n + 3 + 0x2000;
             if (j > srcLen) j = srcLen;
             for (i = n + 3, p = 0; i < j; i++)
             {
                 for (k = 0; k <= n - 0x100; k++)
                 {
                     if (*(src + n - k) != *(src + i - k)) break;
                 }
                 if (k > p)
                 {
                     q = i - n - 3;
                     p = k;
                 }
             }
             if (p < 3)
             {
                 d = (d << 9) | (*(src + n--));
                 T += 9;
             }
             else
             {
                 d = (((d << 1) | 1) << 13) | q;
                 T += 14; n -= p;
                 if (p < 6)
                 {
                     d = (d << 2) | (p - 3); T += 2;
                 }
                 else if (p < 13)
                 {
                     d = (((d << 2) | 3) << 3) | (p - 6); T += 5;
                 }
                 else if (p < 44)
                 {
                     d = (((d << 5) | 0x1f) << 5) | (p - 13); T += 10;
                 }
                 else
                 {
                     d = ((d << 10) | 0x3ff); T += 10; p -= 44;
                     for (; ; )
                     {
                         for (; T >= 8; )
                         {
                             *(dest + m--) = (byte)((d >> (T - 8)) & 0xff);
                             T -= 8; d = d & ((1 << T) - 1);
                         }
                         if (p < 255) break;
                         d = (d << 8) | 0xff; T += 8; p = p - 0xff;
                     }
                     d = (d << 8) | p; T += 8;
                 }
             }
             for (; T >= 8; )
             {
                 *(dest + m--) = (byte)((d >> (T - 8)) & 0xff);
                 T -= 8;
                 d = d & ((1 << T) - 1);
             }
         }
         if (T != 0)
         {
             *(dest + m--) = (byte)(d << (8 - T));
         }
         *(dest + m--) = 0; *(dest + m) = 0;
         for (; ; )
         {
             if (((*destLen - m) & 3) == 0) break;
             *(dest + m--) = 0;
         }
         *destLen = *destLen - m;
         dest += m;
         int[] l = { 0x4c495243, 0x414c5941, srcLen - 0x100, *destLen };
         for (j = 0; j < 4; j++)
         {
             for (i = 0; i < 4; i++)
             {
                 *(odest + i + j * 4) = (byte)(l[j] & 0xff);
                 l[j] >>= 8;
             }
         }
         for (j = 0, odest += 0x10; j < *destLen; j++)
         {
             *(odest++) = *(dest + j);
         }
         for (j = 0; j < 0x100; j++)
         {
             *(odest++) = *(src + j);
         }
         *destLen += 0x110;
         return *destLen;
     }





        unsafe public byte[] CompressCRILAYLA(byte[] input)

        {
            unsafe
            {
                fixed (byte* src = input, dst = new byte[input.Length])
                {
                    //Move cricompress to CLR
                    int destLength = (int)input.Length;

                    int result = CRICompress(dst, &destLength, src, input.Length);
                    byte[] arr = new byte[destLength];
                    System.Runtime.InteropServices.Marshal.Copy((IntPtr)dst, arr, 0, destLength);
                    return arr;
                } 
            }
            
            /*unsafe
            {
                int destLength = (int)input.Length;
                fixed (byte* src = input)
                fixed (byte* dest = new byte[input.Length])
                {
                    destLength = CRICompress(dest, &destLength, src, input.Length);
                    byte[] arr = new byte[destLength];
                    Marshal.Copy((IntPtr)dest, arr, 0, destLength);
                    
                    return arr;
                }
            }*/

        }


       
        public byte[] DecompressCRILAYLA(byte[] input,int compressedSize,int uncompressedSize,int CriHeaderLen)
        {


            if (compressedSize < 0x10)
                return input;
        		
        
            byte[] result;// = new byte[USize];
         
            MemoryStream ms = new MemoryStream(input);
            EndianReader br = new EndianReader(ms, true);


            br.BaseStream.Seek(CriHeaderLen, SeekOrigin.Begin); // Skip CRILAYLA
        	 
           
         
            
            int file_size = br.ReadInt32();
  
                            
            if(file_size+0x100 != uncompressedSize){
                ms.Close();
                br.Close();
               // MessageBox.Show("Failure 1");
                return input;
            }
            	
            int uncompressed_header_offset = br.ReadInt32();

            result = new byte[file_size + 0x100];

            if (uncompressed_header_offset + 0x10 + 0x100 > compressedSize)
            {
                ms.Close();
                br.Close();
                //MessageBox.Show("Failure 2");
                return input;
            }
            // do some error checks here.........
     
		    

            
            // copy uncompressed 0x100 header to start of file
        
             Array.Copy(input, uncompressed_header_offset + 0x10, result, 0, 0x100);
             
            int input_end = input.Length - 0x100 - 1;
            int input_offset = input_end;
            int output_end = 0x100 + file_size - 1;
            byte bit_pool = 0;
            int bits_left = 0, bytes_output = 0;
            int[] vle_lens = new int[4] { 2, 3, 5, 8 };

            while (bytes_output < file_size)
            {
                if (get_next_bits(input, ref input_offset, ref  bit_pool, ref bits_left, 1) > 0)
                {
                    int backreference_offset = output_end - bytes_output + get_next_bits(input, ref input_offset, ref  bit_pool, ref bits_left, 13) + 3;
                    int backreference_length = 3;
                    int vle_level;

                    for (vle_level = 0; vle_level < vle_lens.Length; vle_level++)
                    {
                        int this_level = get_next_bits(input, ref input_offset, ref  bit_pool, ref bits_left, vle_lens[vle_level]);
                        backreference_length += this_level;
                        if (this_level != ((1 << vle_lens[vle_level]) - 1)) break;
                    }

                    if (vle_level == vle_lens.Length)
                    {
                        int this_level;
                        do
                        {
                            this_level = get_next_bits(input, ref input_offset, ref  bit_pool, ref bits_left, 8);
                            backreference_length += this_level;
                        } while (this_level == 255);
                    }

                    for (int i = 0; i < backreference_length; i++)
                    {
                        result[output_end - bytes_output] = result[backreference_offset--];
                        bytes_output++;
                    }
                }
                else
                {
                    // verbatim byte
                    result[output_end - bytes_output] = (byte)get_next_bits(input, ref input_offset, ref  bit_pool, ref bits_left, 8);
                    bytes_output++;
                }
            }

            br.Close();
            ms.Close();

            return result;
          
        }

        private ushort get_next_bits(byte[] input, ref int offset_p, ref byte bit_pool_p, ref int bits_left_p, int bit_count)
        {
            ushort out_bits = 0;
            int num_bits_produced = 0;
            int bits_this_round;

            while (num_bits_produced < bit_count)
            {
                if (bits_left_p == 0)
                {
                    bit_pool_p = input[offset_p];
                    bits_left_p = 8;
                    offset_p--;
                }

                if (bits_left_p > (bit_count - num_bits_produced))
                    bits_this_round = bit_count - num_bits_produced;
                else
                    bits_this_round = bits_left_p;

                out_bits <<= bits_this_round;

                out_bits |= (ushort)((ushort)(bit_pool_p >> (bits_left_p - bits_this_round)) & ((1 << bits_this_round) - 1));

                bits_left_p -= bits_this_round;
                num_bits_produced += bits_this_round;
            }

            return out_bits;
        }

        public object GetColumsData(UTF utf, int row, string Name, int type)
        {
            object Temp = GetColumnData(utf, row, Name);

            if (Temp == null)
            {
                switch (type)
                {
                    case 0: // byte
                        return (byte)0xFF;
                    case 1: // short
                        return (ushort)0xFFFF;
                    case 2: // int
                        return 0xFFFFFFFF;
                    case 3: // long
                        return 0xFFFFFFFFFFFFFFFF;
                }
            }

            if (Temp is ulong)
            {
                return (Temp == null) ? 0xFFFFFFFFFFFFFFFF : (ulong)Temp;
            }

            if (Temp is uint)
            {
                return (Temp == null) ? 0xFFFFFFFF : (uint)Temp;
            }

            if (Temp is ushort)
            {
                return (Temp == null) ? (ushort)0xFFFF : (ushort)Temp;
            }

            return 0;
        }

        public object GetColumnData(UTF utf, int row, string Name)
        {
               object result = null;

            
                for (int i = 0; i < utf.num_columns; i++)
                {
                	
             
                	int storage_flag = (utf.columns[i].flags & (int)UTF.COLUMN_FLAGS.STORAGE_MASK);
					int ctype = (utf.columns[i].flags &  (int)UTF.COLUMN_FLAGS.TYPE_MASK);
					
					if (storage_flag == (int)UTF.COLUMN_FLAGS.STORAGE_CONSTANT){
						if (utf.columns[i].name == Name){
						
							result = utf.columns[i].GetValue();
							break;
				
						}
						
						
					}
		
					
					if (storage_flag == (int)UTF.COLUMN_FLAGS.STORAGE_NONE || storage_flag == (int)UTF.COLUMN_FLAGS.STORAGE_ZERO)
						{
								continue;
						}
									
					
					
					
						
                    if (utf.columns[i].name == Name)
                    {
                    	
                        result = utf.rows[row].rows[i].GetValue();
                        break;
                    }
                }
            
         


            return result;
        }

        public long GetColumnPostion(UTF utf, int row, string Name)
        {
            long result = -1;

            
                for (int i = 0; i < utf.num_columns; i++)
                {
                	
                	
                	
                	
                	  int storage_flag = (utf.columns[i].flags & (int)UTF.COLUMN_FLAGS.STORAGE_MASK);
					 int ctype = (utf.columns[i].flags &  (int)UTF.COLUMN_FLAGS.TYPE_MASK);
					
					if (storage_flag == (int)UTF.COLUMN_FLAGS.STORAGE_CONSTANT){
						if (utf.columns[i].name == Name){
						
							result = utf.columns[i].position;
							break;
							
							
							
					
						}
						
						
					}
					
              
                		if (storage_flag == (int)UTF.COLUMN_FLAGS.STORAGE_NONE || storage_flag == (int)UTF.COLUMN_FLAGS.STORAGE_ZERO)
						{
								continue;
						}
							
                	
                	
                    if (utf.columns[i].name == Name)
                    {
                        result = utf.rows[row].rows[i].position;
                        break;
                    }
                }
            
       
            return result;
        }

        public Type GetColumnType(UTF utf, int row, string Name)
        {
            Type result = null;

            
                for (int i = 0; i < utf.num_columns; i++)
                {
                	
                	
                	  int storage_flag = (utf.columns[i].flags & (int)UTF.COLUMN_FLAGS.STORAGE_MASK);
					int ctype = (utf.columns[i].flags &  (int)UTF.COLUMN_FLAGS.TYPE_MASK);
					
					if (storage_flag == (int)UTF.COLUMN_FLAGS.STORAGE_CONSTANT){
						if (utf.columns[i].name == Name){
						
							result = utf.columns[i].GetType();
							break;
							
							
							
					
						}
						
						
					}
					
                	
                	
                		if (storage_flag == (int)UTF.COLUMN_FLAGS.STORAGE_NONE || storage_flag == (int)UTF.COLUMN_FLAGS.STORAGE_ZERO)
						{
								continue;
						}
							
                	
                	
                    if (utf.columns[i].name == Name)
                    {
                        result = utf.rows[row].rows[i].GetType();
                        break;
                    }
                }
            
        

            return result;
        }

        public void UpdateFileEntry(FileEntry fileEntry)
        {
            if (fileEntry.FileType == "FILE" || fileEntry.FileType == "HDR")
            {
                byte[] updateMe = null;
                switch (fileEntry.TOCName)
                {
                    case "CPK":
                        updateMe = CPK_packet;
                        break;
                    case "TOC":
                        updateMe = TOC_packet;
                        break;
                    case "ITOC":
                        updateMe = ITOC_packet;
                        break;
                    case "ETOC":
                        updateMe = ETOC_packet;
                        break;
                    default:
                        throw new Exception("I need to implement this TOC!");
                        //break;
                }


                //Update ExtractSize
                if (fileEntry.ExtractSizePos > 0)
                    UpdateValue(ref updateMe, fileEntry.ExtractSize, fileEntry.ExtractSizePos, fileEntry.ExtractSizeType);

                //Update FileSize
                if (fileEntry.FileSizePos > 0)
                    UpdateValue(ref updateMe, fileEntry.FileSize, fileEntry.FileSizePos, fileEntry.FileSizeType);

                //Update FileOffset
                if (fileEntry.FileOffsetPos > 0)
                    UpdateValue(ref updateMe, fileEntry.FileOffset - (ulong)((fileEntry.TOCName == "TOC") ? 0x800 : 0), fileEntry.FileOffsetPos, fileEntry.FileOffsetType);

                switch (fileEntry.TOCName)
                {
                    case "CPK":
                        updateMe = CPK_packet;
                        break;
                    case "TOC":
                        TOC_packet = updateMe;
                        break;
                    case "ITOC":
                        ITOC_packet = updateMe;
                        break;
                    case "ETOC":
                        updateMe = ETOC_packet;
                        break;
                    default:
                        throw new Exception("I need to implement this TOC!");
                        //break;
                }
            }
        }

        public void UpdateValue(ref byte[] packet, object value, long pos, Type type)
        {
            MemoryStream temp = new MemoryStream();
            temp.Write(packet, 0, packet.Length);

            EndianWriter toc = new EndianWriter(temp, false);
            toc.Seek((int)pos, SeekOrigin.Begin);

            value = Convert.ChangeType(value, type);

            if (type == typeof(Byte))
            {
                toc.Write((Byte)value);
            }
            else if (type == typeof(UInt16))
            {
                toc.Write((UInt16)value);
            }
            else if (type == typeof(UInt32))
            {
                toc.Write((UInt32)value);
            }
            else if (type == typeof(UInt64))
            {
                toc.Write((UInt64)value);
            }
            else if (type == typeof(Single))
            {
                toc.Write((Single)value);
            }
            else
            {
                throw new Exception("Not supported type!");
            }

            toc.Close();

            MemoryStream myStream = (MemoryStream)toc.BaseStream;
            packet = myStream.ToArray();
        }

        public bool isUtfEncrypted { get; set; }
        public int unk1 { get; set; }
        public long utf_size { get; set; }
        public byte[] utf_packet { get; set; }

        public byte[] CPK_packet { get; set; }
        public byte[] TOC_packet { get; set; }
        public byte[] ITOC_packet { get; set; }
        public byte[] ETOC_packet { get; set; }
        public byte[] GTOC_packet { get; set; }

        public ulong TocOffset, EtocOffset, ItocOffset, GtocOffset, ContentOffset;
    }

    public class UTF
    {
        public enum COLUMN_FLAGS : int
        {
            STORAGE_MASK = 0xf0,
            STORAGE_NONE = 0x00,
            STORAGE_ZERO = 0x10,
            STORAGE_CONSTANT = 0x30,
            STORAGE_PERROW = 0x50,


            TYPE_MASK = 0x0f,
            TYPE_DATA = 0x0b,
            TYPE_STRING = 0x0a,
            TYPE_FLOAT = 0x08,
            TYPE_8BYTE2 = 0x07,
            TYPE_8BYTE = 0x06,
            TYPE_4BYTE2 = 0x05,
            TYPE_4BYTE = 0x04,
            TYPE_2BYTE2 = 0x03,
            TYPE_2BYTE = 0x02,
            TYPE_1BYTE2 = 0x01,
            TYPE_1BYTE = 0x00,
        }

        public List<COLUMN> columns;
        public List<ROWS> rows;

       

        public UTF()
        {
          
        }

        public bool ReadUTF(EndianReader br)
        {
            //UTF Table seems to be in Big Endian (switch that in HxD)
            long offset = br.BaseStream.Position;
            // MessageBox.Show((br.BaseStream.Position).ToString());
            if (Tools.ReadCString(br, 4) != "@UTF")
            {
                return false;
            }
             //MessageBox.Show((br.BaseStream.Position).ToString());
            table_size = br.ReadInt32();
            // MessageBox.Show((table_size).ToString());
            rows_offset = br.ReadInt32();
            strings_offset = br.ReadInt32();
            data_offset = br.ReadInt32();

            // CPK Header & UTF Header are ignored, so add 8 to each offset
            rows_offset += (offset + 8);
            strings_offset += (offset + 8);
            data_offset += (offset + 8);

            table_name = br.ReadInt32();
            num_columns = br.ReadInt16();
            row_length = br.ReadInt16();
            num_rows = br.ReadInt32();

            //read Columns
            columns = new List<COLUMN>();
            COLUMN column;

            for (int i = 0; i < num_columns; i++)
            {
                column = new COLUMN();
                column.flags = br.ReadByte();
                if (column.flags == 0)
                {
                    br.BaseStream.Seek(3, SeekOrigin.Current);
                    column.flags = br.ReadByte();
                }

                column.name = Tools.ReadCString(br, -1, (long)(br.ReadInt32() + strings_offset));
                
                //CRAPPY CODE AHEAD : PORTED FROM YACE FOR READING THE CONSTANT STR
         
                if ((column.flags & (int)UTF.COLUMN_FLAGS.STORAGE_MASK) == (int)UTF.COLUMN_FLAGS.STORAGE_CONSTANT){
                	
                	column.type = (column.flags & (int)UTF.COLUMN_FLAGS.TYPE_MASK);
                	column.position =  br.BaseStream.Position;
                	switch (column.type){
                			
                		case 0:
                        case 1:
                            column.uint8 = br.ReadByte();
                            break;

                        case 2:
                        case 3:
                            column.uint16 = br.ReadUInt16();
                            break;

                        case 4:
                        case 5:
                            column.uint32 = br.ReadUInt32();
                            break;

                        case 6:
                        case 7:
                           column.uint64 = br.ReadUInt64();
                        
                            break;

                        case 8:
                            column.ufloat = br.ReadSingle();
                            break;

                        case 0xA:
                           column.str = Tools.ReadCString(br, -1, br.ReadInt32() + strings_offset);
                     
                            break;

                        case 0xB:
                            long position = br.ReadInt32() + data_offset;
                            column.position = position;
                            column.data = Tools.GetData(br, position, br.ReadInt32());
                            break;

                        default: throw new NotImplementedException();
                			
                	}
                }
            
                columns.Add(column);
            }

            //read Rows

            rows = new List<ROWS>();
            ROWS current_entry;
            ROW current_row;
            int storage_flag;

            for (int j = 0; j < num_rows; j++)
            {
                br.BaseStream.Seek(rows_offset + (j * row_length), SeekOrigin.Begin);

                current_entry = new ROWS();

                for (int i = 0; i < num_columns; i++)
                {
                    current_row = new ROW();

                    storage_flag = (columns[i].flags & (int)COLUMN_FLAGS.STORAGE_MASK);

                    if (storage_flag == (int)COLUMN_FLAGS.STORAGE_NONE) // 0x00
                    {
                        current_entry.rows.Add(current_row);
                        continue;
                    }

                    if (storage_flag == (int)COLUMN_FLAGS.STORAGE_ZERO) // 0x10
                    {
                        current_entry.rows.Add(current_row);
                        continue;
                    }

                    if (storage_flag == (int)COLUMN_FLAGS.STORAGE_CONSTANT) // 0x30
                    {
                        current_entry.rows.Add(current_row);
                        continue;
                    }

                    // 0x50

                    current_row.type = columns[i].flags & (int)COLUMN_FLAGS.TYPE_MASK;

                    current_row.position = br.BaseStream.Position;

                    switch (current_row.type)
                    {
                        case 0:
                        case 1:
                            current_row.uint8 = br.ReadByte();
                            break;

                        case 2:
                        case 3:
                            current_row.uint16 = br.ReadUInt16();
                            break;

                        case 4:
                        case 5:
                            current_row.uint32 = br.ReadUInt32();
                            break;

                        case 6:
                        case 7:
                            current_row.uint64 = br.ReadUInt64();
                            break;

                        case 8:
                            current_row.ufloat = br.ReadSingle();
                            break;

                        case 0xA:
                            current_row.str = Tools.ReadCString(br, -1, br.ReadInt32() + strings_offset);
                            break;

                        case 0xB:
                            long position = br.ReadInt32() + data_offset;
                            current_row.position = position;
                            current_row.data = Tools.GetData(br, position, br.ReadInt32());
                            break;

                        default: throw new NotImplementedException();
                    }


                    current_entry.rows.Add(current_row);
                }

                rows.Add(current_entry);
            }

            return true;
        }

        public int table_size { get; set; }

        public long rows_offset { get; set; }
        public long strings_offset { get; set; }
        public long data_offset { get; set; }
        public int table_name { get; set; }
        public short num_columns { get; set; }
        public short row_length { get; set; }
        public int num_rows { get; set; }
    }

    public class COLUMN
    {
        public COLUMN()
        {
        	type = -1;
        }

        
        
        public int type { get; set; }
        
        public byte flags { get; set; }
        public string name { get; set; }
        
        
        
        public object GetValue()
        {
            object result = -1;

            switch (this.type)
            {
                case 0:
                case 1: return this.uint8;

                case 2:
                case 3: return this.uint16;

                case 4:
                case 5: return this.uint32;

                case 6:
                case 7: return this.uint64;

                case 8: return this.ufloat;

                case 0xA: return this.str;

                case 0xB: return this.data;

                default: return null;
            }
        }

        public new Type GetType()
        {
            object result = -1;

            switch (this.type)
            {
                case 0:
                case 1: return this.uint8.GetType();

                case 2:
                case 3: return this.uint16.GetType();

                case 4:
                case 5: return this.uint32.GetType();

                case 6:
                case 7: return this.uint64.GetType();

                case 8: return this.ufloat.GetType();

                case 0xA: return this.str.GetType();

                case 0xB: return this.data.GetType();

                default: return null;
            }
        }

        //column based datatypes
        public byte uint8 { get; set; }
        public ushort uint16 { get; set; }
        public uint uint32 { get; set; }
        public ulong uint64 { get; set; }
        public float ufloat { get; set; }
        public string str { get; set; }
        public byte[] data { get; set; }
        public long position { get; set; }

    }

    public class ROWS
    {
        public List<ROW> rows;

        public ROWS()
        {
            rows = new List<ROW>();
        }
    }

    public class ROW
    {
        public ROW()
        {
            type = -1;
        }

        public int type { get; set; }

        public object GetValue()
        {
            object result = -1;

            switch (this.type)
            {
                case 0:
                case 1: return this.uint8;

                case 2:
                case 3: return this.uint16;

                case 4:
                case 5: return this.uint32;

                case 6:
                case 7: return this.uint64;

                case 8: return this.ufloat;

                case 0xA: return this.str;

                case 0xB: return this.data;

                default: return null;
            }
        }

        public new  Type GetType() 
        {
            object result = -1;

            switch (this.type)
            {
                case 0:
                case 1: return this.uint8.GetType();

                case 2:
                case 3: return this.uint16.GetType();

                case 4:
                case 5: return this.uint32.GetType();

                case 6:
                case 7: return this.uint64.GetType();

                case 8: return this.ufloat.GetType();

                case 0xA: return this.str.GetType();

                case 0xB: return this.data.GetType();

                default: return null;
            }
        }

        //column based datatypes
        public byte uint8 { get; set; }
        public ushort uint16 { get; set; }
        public uint uint32 { get; set; }
        public ulong uint64 { get; set; }
        public float ufloat { get; set; }
        public string str { get; set; }
        public byte[] data { get; set; }
        public long position { get; set; }
    }

    public class FileEntry
    {
        public FileEntry()
        {
            DirName = null;
            FileName = null;
            FileSize = null;
            ExtractSize = null;
            ID = null;
            UserString = null;
            LocalDir = null;

            FileOffset = 0;
            UpdateDateTime = 0;
        }

        public object DirName { get; set; } // string
        public object FileName { get; set; } // string

        public object FileSize { get; set; }
        public long FileSizePos { get; set; }
        public Type FileSizeType { get; set; }

        public object ExtractSize { get; set; } // int
        public long ExtractSizePos { get; set; }
        public Type ExtractSizeType { get; set; }

        public ulong FileOffset { get; set; }
        public long FileOffsetPos { get; set; }
        public Type FileOffsetType { get; set; }


        public ulong Offset { get; set; }
        public object ID { get; set; } // int
        public object UserString { get; set; } // string
        public ulong UpdateDateTime { get; set; }
        public object LocalDir { get; set; } // string
        public string TOCName { get; set; }

        public bool Encrypted { get; set; }

        public string FileType { get; set; }
    }
}
