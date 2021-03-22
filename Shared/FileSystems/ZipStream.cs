using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading;

namespace DataPackChecker.Shared.FileSystems {
    public class ZipStream : Stream {
        public ZipStream(string zipFile, string entry) {
            Archive = ZipFile.OpenRead(zipFile);
            Stream = Archive.GetEntry(entry).Open();
        }

        public override bool CanRead => Stream.CanRead;

        public override bool CanSeek => Stream.CanSeek;

        public override bool CanWrite => Stream.CanWrite;

        public override long Length => Stream.Length;

        public override long Position { get => Stream.Position; set => Stream.Position = value; }
        private ZipArchive Archive { get; }
        private Stream Stream { get; }

        public override void Flush() {
            Stream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count) {
            return Stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin) {
            return Stream.Seek(offset, origin);
        }

        public override void SetLength(long value) {
            Stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count) {
            Stream.Write(buffer, offset, count);
        }

        protected override void Dispose(bool disposing) {
            Stream.Dispose();
            Archive.Dispose();
        }
    }
}
