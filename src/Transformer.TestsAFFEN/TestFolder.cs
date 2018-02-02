﻿using System;
using System.IO;
using Environment = Transformer.Model.Environment;

namespace Transformer.Tests
{
    public class TestFolder : IDisposable
    {
        public DirectoryInfo DirectoryInfo { get; private set; }

        public TestFolder(System.Environment.SpecialFolder folder)
        {
            DirectoryInfo = Directory.CreateDirectory(Path.Combine(System.Environment.GetFolderPath(folder), Guid.NewGuid().ToString()));
        }

        public TestFolder()
        {
            DirectoryInfo = Directory.CreateDirectory(Path.Combine(System.Environment.CurrentDirectory, Guid.NewGuid().ToString()));
        }

        public void AddFolder(string name)
        {
            Directory.CreateDirectory(Path.Combine(DirectoryInfo.FullName, name));
        }

        public string AddFile(string relativeFilename, string content)
        {
            string absoluteFileName = Path.Combine(DirectoryInfo.FullName, relativeFilename);

            var fileInfo = new FileInfo(absoluteFileName);
            var targetDir = fileInfo.Directory;

            if (!targetDir.Exists)
            {
                targetDir.Create();
            }

            using (var filestream = File.CreateText(absoluteFileName))
            {
                filestream.Write(content);
                filestream.Flush();
            }

            return absoluteFileName;
        }

        public bool FileExists(string filename)
        {
            return File.Exists(Path.Combine(DirectoryInfo.FullName, filename));
        }

        public string ReadFile(string filename)
        {
            try
            {
                return File.ReadAllText(Path.Combine(DirectoryInfo.FullName, filename));
            }
            catch (Exception)
            {
                Console.WriteLine("File {0} not found, returning empty string.", filename);
                return string.Empty;
            }
        }

        public void Dispose()
        {
            DirectoryInfo.Delete(true);
        }
    }
}