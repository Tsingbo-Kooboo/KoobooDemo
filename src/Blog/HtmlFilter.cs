using Kooboo.CMS.Sites.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Blog
{
    public class HtmlFilter : Stream
    {
        private Stream _responseStream { get; set; }
        private Encoding _encoding { get; set; }

        private IHtmlDecoration _service { get; set; }

        private string _savePath { get; set; }

        public HtmlFilter(Stream inputStream, Encoding encoding, string savePath)
        {
            this._responseStream = inputStream;
            this._encoding = encoding;
            this._savePath = savePath;
            _service = Kooboo.CMS.Common.Runtime.EngineContext.Current.Resolve<IHtmlDecoration>();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            string html = _encoding.GetString(buffer, offset, count);
            using (var sw = new StreamWriter(_savePath, false, _encoding))
            {
                html = _service.Output(html);
                sw.Write(html);
            }
            _responseStream.Write(buffer, offset, count);
        }
        #region Filter overrides
        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Close()
        {
            _responseStream.Close();
        }

        public override void Flush()
        {
            _responseStream.Flush();
        }

        public override long Length
        {
            get { return 0; }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _responseStream.Seek(offset, origin);
        }

        public override void SetLength(long length)
        {
            _responseStream.SetLength(length);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _responseStream.Read(buffer, offset, count);
        }
        public override long Position { get; set; }
        #endregion
    }
}
