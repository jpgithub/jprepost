using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Backend
{
    /// <summary>
    /// Generic Frame Object
    /// </summary>
    public class FrameObject
    {

        /// <summary>
        /// Reference Version Tag with associated Frame Size
        /// </summary>
        public enum Versions { V10 = 256, V11 = 512, V12 = 1024, V13 = 2048, V14 = 4096 };



        public uint FieldOne
        {
            get;
            set;
        }
        public uint FieldTwo
        {
            get;
            set;
        }

        public ushort[] FieldThree
        {
            get;
            set;
        }

        public uint FieldFour
        {
            get;
            set;
        }

        public uint FieldFive
        {
            get;
            set;
        }

        public uint FieldSix
        {
            get;
            set;
        }

        public uint FieldSeven
        {
            get;
            set;
        }

        public FrameObject ShallowCopy()
        {
            return (FrameObject)this.MemberwiseClone();
        }
    }

    /// <summary>
    /// This method will produce a Frame Object from a given object according to the frame version
    /// </summary>
    internal static class FrameParser
    {
        /// <summary>
        /// This get method return a new frame object for a given bytestream and frame object's version attribute number
        /// </summary>
        /// <param name="size"> Parse Frame according to size </param>
        /// <param name="bytestream"> Input Byte Stream for parsing </param>
        /// <param name="newFrame"> Output a new FrameObject </param>
        public static void SvaFrameParser(uint size, byte[] bytestream, out FrameObject newFrame)
        {
            FrameObject currentframe = new FrameObject();

            // Default Version is 1.0 = 10;
            currentframe.FieldOne = uint.MaxValue;
            currentframe.FieldTwo = uint.MinValue;


            ushort[] testbytes = { 0xff, 0x00, 0xff, 0xff, 0xff, 0x00, 0x00 };
            currentframe.FieldThree = testbytes;

            //append addition fields using a special switch case
            while ((uint)size != 256U)
            {
                currentframe.FieldFour = uint.MaxValue;
                if ((uint)size == 512U)
                {
                    break;
                }
                currentframe.FieldFive = uint.MaxValue;
                if ((uint)size == 1024U)
                {
                    break;
                }
                currentframe.FieldSix = uint.MaxValue;
                if ((uint)size == 2048U)
                {
                    break;
                }
                currentframe.FieldSeven = uint.MaxValue;
                if ((uint)size == 4096U)
                {
                    break;
                }
            }

            newFrame = currentframe.ShallowCopy();

        }

    }

    internal class TctFileReader
    {
        private readonly FileStream SourceStream;

        public TctFileReader(string filepath)
        {

            #region BackgroundWorker
            byte[] versionInBytes = new byte[20];
            try
            {
                if (File.Exists(filepath))
                {
                    throw new FileNotFoundException();
                }

                SourceStream = File.Open(filepath, FileMode.Open, FileAccess.Read);

                if (!SourceStream.CanRead || !SourceStream.CanSeek)
                {
                    throw new IOException();
                }

                string versionTag = ReadVersion(versionInBytes);
                uint expectedframesize = GetFrameSize(versionTag);

                if (expectedframesize != uint.MinValue)
                {
                    SourceStream.Seek(0, SeekOrigin.Begin);

                }
                else
                {
                    throw new Exception("No Version Number Found in File");
                }



                uint syncpattern = 0x5A5A5A; // Constant
                uint fieldone;
                uint fieldtwo;
                uint frameCount = 0;
                uint frameByteCount = 0;
                BinaryReader skreader = new BinaryReader(SourceStream);

                while (frameCount >= 0)
                {
                    fieldone = skreader.ReadUInt32();
                    fieldtwo = skreader.ReadUInt32();

                    if (fieldtwo == syncpattern)
                    {
                        //Further Process
                        FrameObject newFrame = new FrameObject();
                        FrameParser.SvaFrameParser(expectedframesize, skreader.ReadBytes((int)(expectedframesize - (sizeof(uint) * 2))), out newFrame);
                        //skreader.Seek(-sizeof(uint), SeekOrigin.Current);
                        //fill fieldone and fieldtwo value;
                        newFrame.FieldOne = fieldone;
                        newFrame.FieldTwo = fieldtwo;
                    }
                   
                    //expectedFramevalue.Item1 is expectedFramesize
                    if (((skreader.BaseStream.Length - skreader.BaseStream.Position) < expectedframesize) && (frameCount == uint.MinValue))
                    {
                        //End of Stream
                        break;
                    }

                }



            }
            catch (IOException)
            {
                ;
            }
            #endregion
        }

        /// <summary>
        /// This method will fetch frame size associated with a given version tag in constant time
        /// </summary>
        /// <param name="versionTag"> Version Tag </param>
        /// <returns> A frame size associated with given versionTag </returns>
        private static uint GetFrameSize(string versionTag)
        {
            uint expectedframesize = uint.MinValue;

            try
            {
                FrameObject.Versions versionValue = (FrameObject.Versions)Enum.Parse(typeof(FrameObject.Versions), versionTag);
                if (Enum.IsDefined(typeof(FrameObject.Versions), versionValue))
                {
                    Console.WriteLine("Converted '{0}' to {1}.", versionTag, versionValue);
                    expectedframesize = (uint)versionValue;
                }
                else
                {
                    ;// UnSupported Version Number Console.WriteLine("{0} is not an underlying value of the Colors enumeration.", versionTag);
                }
            }
            catch (ArgumentException)
            {
                ;// UnSupported Version Number
            }

            return expectedframesize;
        }

        /// <summary>
        /// This method convert version tag in bytes into string version tag
        /// </summary>
        /// <param name="versionInBytes"> VersionTag In Bytes </param>
        /// <returns> A VersionTag </returns>
        private string ReadVersion(byte[] versionInBytes)
        {
            int read = SourceStream.Read(versionInBytes, (int)(SourceStream.Length - versionInBytes.LongLength), versionInBytes.Length);
            if (read != versionInBytes.Length)
            {
                throw new Exception("Reading Error");
            }
            else
            {
                //Parse string and Trim period space
                return ASCIIEncoding.ASCII.GetString(versionInBytes).Trim('\0');
            }
        }



    }
}
