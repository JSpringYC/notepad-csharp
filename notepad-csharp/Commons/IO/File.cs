using System;
using System.IO;

namespace Commons.IO
{
    /// <summary>
    /// 文件对象，包含对文件操作的一系列方法
    /// </summary>
    public class Files
    {
        public static bool IsFile(string Path)
        {
            return File.Exists(Path);
        }

        public static bool IsFile(FileSystemInfo fileSystemInfo)
        {
            return IsFile(fileSystemInfo.FullName);
        }

        public static bool IsDirectory(string Path)
        {
            return File.Exists(Path);
        }

        public static bool IsDirectory(FileSystemInfo fileSystemInfo)
        {
            return IsDirectory(fileSystemInfo.FullName);
        }

        public static bool Exists(string Path)
        {
            FileStream stream = File.OpenRead(Path);
            return File.Exists(Path) || Directory.Exists(Path);
        }

        public static bool Exists(FileSystemInfo fileSystemInfo)
        {
            return Exists(fileSystemInfo.FullName);
        }
    }
}
