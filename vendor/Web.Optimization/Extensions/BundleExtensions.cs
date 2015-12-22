﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Optimization;
using Web.Optimization.Common;

namespace Web.Optimization.Extensions
{
    public static class BundleExtensions
    {
        public static void IncludeRemoteFile(
            this Bundle instance, string url)
        {
            Uri uri;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
                throw new UriFormatException();

            instance.IncludeRemoteFile(uri);
        }

        public static void IncludeRemoteFile(this Bundle instance, Uri uri)
        {
            var fileName = Path.GetFileName(uri.LocalPath);

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException(
                    "File name seems to be invalid.",
                    "uri");
            
            var extension = Path.GetExtension(fileName).ToUpperInvariant();

            var path =
                Path.Combine(
                    HttpRuntime.AppDomainAppPath,
                    FileTypes.ScriptExtensions.Any(x => x.Equals(extension))
                        ? "Scripts"
                        : "Content",
                    "Remote",
                    fileName);
            
            var virtualPath = path.ToVirtualPath();

            var fileInfo = new FileInfo(path);

            if (fileInfo.Exists &&
                fileInfo.CreationTimeUtc > DateTime.UtcNow.AddDays(-7))
            {
                instance.Include(virtualPath);
                return;
            }

            var request = (HttpWebRequest)WebRequest.Create(uri);
            var response = request.GetResponse();

            using (var stream = response.GetResponseStream())
            {
                if (stream == null)
                    throw new Exception();

                // Would be awesome to add a file by passing 
                // the stream or just the file content, but NO PATH!

                using (var reader = new StreamReader(stream))
                {
                    var content = reader.ReadToEnd();

                    var directory = Path.GetDirectoryName(path);

                    if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    File.WriteAllText(
                        path,
                        content,
                        Encoding.UTF8);

                    instance.Include(virtualPath);
                }
            }
        }
    }
}