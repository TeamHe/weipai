using System.Text;
using System.IO;
using System;
using System.ComponentModel;

namespace ResModel.nw
{
    public enum nw_UpdateResult
    {
        [Description("升级成功")]
        Success = 0,
        [Description("文件下载失败")]
        DownloadFail = 1,
        [Description("升级文件存储空间不足")]
        LowMemory = 2,
        [Description("文件格式错误")]
        FormatError = 3,
        [Description("文件校验出错")]
        CheckCodeErr = 4,
        [Description("固件版本与当前硬件不匹配")]
        UnCompatible = 5,
        [Description("电量过低")]
        LowPower = 6,
        [Description("升级失败：其他原因")]
        UpdateFaile = 7,
    }

    public class nw_update_info
    {
        private string filename;

        private string filepath;

        public nw_update_info() { 
            this.MaxPacLength = 1024;
        }

        public string Password { get;set; }

        public int ChannelNO { get; set; }

        public string FileName 
        {
            get { return this.filename; }
            set { 
                string name = value;
                if (!FileNameCheck(name))
                    throw new ArgumentException("文件名ASCII码可打印字符，不允许包含中文字符");
                this.filename = name;
            }
        }

        public string FilePath 
        { 
            get { return this.filepath; }
            set { 
                string path = value;
                if(!File.Exists(path))
                    throw new ArgumentException(string.Format("文件 \"{0}\" 不存在",path));
                this.FileName = Path.GetFileName(path);
                this.filepath = path;
            } 
        }

        public int MaxPacLength { get;set; }

        public FileStream stream { get; set; }

        public enum UpdateType
        {
            /// <summary>
            /// 普通升级
            /// </summary>
            Normal = 0,
            /// <summary>
            /// 断点续传
            /// </summary>
            Resum = 1,
        }

        public UpdateType Type { get; set; }

        public int PacNum { get;set; }

        public long FileLength { get; set; }

        /// <summary>
        /// 文件名检查
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool FileNameCheck(string filename)
        {
            // ASCII码可打印字符，不允许包含中文字符
            byte[] bytes = Encoding.Default.GetBytes(filename);
            foreach(byte b in bytes)
            {
                // https://baike.baidu.com/item/isprint/6973194?fr=ge_ala
                // 当c为可打印字符（0x20-0x7e）时，返回非零值，否则返回零。
                if (b <0x20 || b >0x7f)
                    return false;
            }
            return true;
        }

        public int GetPacNum()
        {
            if(this.FilePath == null)
                return -1;
            try
            {
                FileInfo fileInfo = new FileInfo(this.FilePath);
                this.FileLength = fileInfo.Length;
                int pnum = (int)(FileLength / this.MaxPacLength);
                if (FileLength % this.MaxPacLength > 0)
                    pnum++;
                this.PacNum = pnum;
                return pnum;

            }
            catch(Exception e)
            {
                return -1;
            }
        }

        public byte[] GetPacData(int pno)
        {
            try
            {
                if (this.stream == null)
                    this.stream = File.OpenRead(this.FilePath);

                long offset = pno * this.MaxPacLength;
                stream.Seek(offset, SeekOrigin.Begin);
                byte[] buffer = new byte[this.MaxPacLength];
                int length = stream.Read(buffer, 0, buffer.Length);
                if(length < buffer.Length)
                {
                    byte[] temp = new byte[length];
                    Buffer.BlockCopy(buffer,0,temp,0, length);
                    return temp;
                }
                else
                {
                    return buffer;
                }
            }catch
            {
                return null;
            }
        }
    }
}
