using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PT_Lab5 {
    public class ArchiveWorker {
        private FolderBrowserDialog browser;

        public ArchiveWorker() {
            browser = new FolderBrowserDialog();
        }

        public void Compress() {
            if (browser.ShowDialog() == DialogResult.OK) {
                var dir = new DirectoryInfo(browser.SelectedPath);
                Task.Run(() => {
                    var files = dir.GetFiles().Where(file => !file.Name.EndsWith(".gz"));
                    Parallel.ForEach(files,
                        (file) => {
                            using (FileStream originalFileStream = file.OpenRead())
                            using (FileStream compressedFileStream = File.Create(file.FullName + ".gz"))
                            using (GZipStream compressionStream = new GZipStream(compressedFileStream,
                                CompressionMode.Compress)) {
                                originalFileStream.CopyTo(compressionStream);
                            }
                        });
                });
            }
        }

        public void Decompress() {
            if (browser.ShowDialog() == DialogResult.OK) {
                var dir = new DirectoryInfo(browser.SelectedPath);
                Task.Run(() => {
                    var files = dir.GetFiles("*.gz");
                    Parallel.ForEach(files,
                        file => {
                            using (
                                FileStream originalFileStream = new FileStream(file.FullName.Replace(".gz", ""),
                                    FileMode.Create))
                            using (
                                GZipStream compressionStream = new GZipStream(file.OpenRead(),
                                    CompressionMode.Decompress)) {
                                compressionStream.CopyTo(originalFileStream);
                            }
                        });
                });
            }
        }
    }
}