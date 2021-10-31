using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Recovery.FileSystem.Hikvision
{
    class HikvisionFileSystem
    {


        private bool _canRead = false;
        public bool CanRead { get { return _canRead; } }

        private MasterSector _masterSector = new MasterSector();
        public MasterSector MasterSector { get { return _masterSector; } }

        public List<ChannelInfo> ChannelInfoMap = new List<ChannelInfo>();




        public HikvisionFileSystem(Stream stream)
        {
            bool success = ParseMasterSector(stream);
            if (!success)
                return;

            success = ParseDataBlockInfo(stream);
            if (!success)
                return;

            // Done
            _canRead = true;
        }

        public bool ParseMasterSector(Stream stream)
        {
            byte[] array = new byte[512];

            // check error
            if (!(stream != null && stream.CanRead && 1024 <= stream.Length))
            {
                return false;
            }

            //Parse Master Sector
            stream.Position = 512;
            stream.Read(array, 0, 512);
            _masterSector = ByteArrayToStructure<MasterSector>(array);

            return true;
        }

        // MasterSector를 참조해서 list<BlockInfo>()를 생성한다.
        public bool ParseDataBlockInfo(Stream stream)
        {

            // check error
            if (!(stream != null && stream.CanRead))
            {
                return false;
            }

            try
            {
                long OffsetToVideoDataArea = _masterSector.OffsetToVideoDataArea;
                long DatablockSize = _masterSector.DatablockSize;
                long dataBlockCount = _masterSector.DatablockCount;
                long currBlockStartOffset;//DataBlock 시작 위치
                long currBlockInfoOffset;//DataBlockInfo는 DataBlock 끝에서 1MB 앞에 있다.
                byte[] buffer = new byte[512];
                int readLen;

                for (long dataBlockIdx = 0; dataBlockIdx < dataBlockCount; dataBlockIdx++)
                {
                    currBlockStartOffset = OffsetToVideoDataArea + (dataBlockIdx * DatablockSize);
                    currBlockInfoOffset = currBlockStartOffset + DatablockSize - 1024 * 1024;/*1MB=1024*1024*/
                    stream.Position = currBlockInfoOffset;

                    //Part1. Read Header
                    readLen = stream.Read(buffer, 0, 512);
                    if (readLen == 0 || readLen != 512)
                        break;

                    var blockInfoHeader = ByteArrayToStructure<BlockInfoHeader>(buffer);

                    if (!blockInfoHeader.CanRead)
                        continue;

                    //Part2. Read BlockInfo
                    for (int j = 0; j < blockInfoHeader.InfoCount; j++)
                    {
                        readLen = stream.Read(buffer, 0, 512);
                        if (readLen == 0 || readLen != 512)
                            break;

                        var blockInfo = ByteArrayToStructure<BlockInfo>(buffer);
                        var channelInfo = new ChannelInfo()
                        {
                            Channel = blockInfoHeader.Channel,
                            StreamStartOffset = blockInfoHeader.DataBlockStartOffset + blockInfo.StartOffset,
                            StreamEndOffset = blockInfoHeader.DataBlockStartOffset + blockInfo.EndOffset
                        };
                        this.ChannelInfoMap.Add(channelInfo);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return true;
        }

        private T ByteArrayToStructure<T>(byte[] bytes)
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T stuff = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return stuff;
        }
    }
}
