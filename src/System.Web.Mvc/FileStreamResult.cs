// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.IO;

namespace System.Web.Mvc
{
    public class FileStreamResult : FileResult
    {
        // default buffer size as defined in BufferedStream type
        private const int BufferSize = 0x1000;

        public FileStreamResult(Stream fileStream, string contentType)
            : base(contentType)
        {
            if (fileStream == null)
            {
                throw new ArgumentNullException("fileStream");
            }

            FileStream = fileStream;
        }

        public Stream FileStream { get; private set; }

        protected override void WriteFile(HttpResponseBase response)
        {
            // grab chunks of data and write to the output stream
            Stream outputStream = response.OutputStream;

            var stream = FileStream;
            var context = HttpContext.Current;
            if (stream == null && context != null)
                throw new ArgumentNullException($"Filestream null : {context.Request.RawUrl}");

            using (FileStream)
            {
                byte[] buffer = new byte[BufferSize];
                try
                {

                    while (true)
                    {
                        int bytesRead = FileStream.Read(buffer, 0, BufferSize);
                        if (bytesRead == 0)
                        {
                            // no more data
                            break;
                        }

                        outputStream.Write(buffer, 0, bytesRead);
                    }
                } catch (NullReferenceException ex)   // ignore Object reference not set to an instance of an object.: 
                { if (context == null || context.Request.IsLocal) throw ex; }
            }
        }
    }
}
