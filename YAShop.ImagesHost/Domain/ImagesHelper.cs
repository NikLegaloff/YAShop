using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace YAShop.ImagesHost.Domain
{
    public class ImagesHelper
    {
        private readonly Guid _contextId;

        public ImagesHelper(Guid contextId)
        {
            _contextId = contextId;
        }

        public void DeleteImageFile(Guid id)
        {
            File.Delete(GetImagePath(id));
            File.Delete(GetTmbPath(id));
        }

        public string GetFolderPath(Guid id)
        {
            return $"{Env.Current.ImagesBasePath}{_contextId}\\{id.ToString().Substring(0, 2)}\\{id.ToString().Substring(2, 2)}\\";

        }
        public string GetImagePath(Guid id)
        {
            return $"{GetFolderPath(id)}{id}.jpg";
        }

        public string GetTmbPath(Guid id)
        {
            return $"{GetFolderPath(id)}{id}_tmb.jpg";
        }

        public Guid UploadFile(string name, byte[] data, Guid? folderId)
        {

            var hash = GetHash(data);
            var size = data.Length;
            var existing = DB.ExecuteScalar<Guid?>("select id from image where contextId=@contextId and hash=@hash and size=@size", new {hash, size, contextId=_contextId});
            if (existing != null) return existing.Value;
            var id = Guid.NewGuid();
            var folderPath = GetFolderPath(id);
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            Resize(data, id);
            DB.Execute("insert into Image(id,name,folderId,hash,size, contextId) values(@id,@name,@folderId,@hash,@size,@contextId)", new {id,name,folderId,hash,size, contextId = _contextId });
            return id;
        }

        private void Resize(byte[] data, Guid id)
        {
            if (File.Exists(GetImagePath(id))) File.Delete(GetImagePath(id));
            if (File.Exists(GetTmbPath(id))) File.Delete(GetTmbPath(id));
            using (var ms = new MemoryStream(data))
            {
                using (var g = System.Drawing.Image.FromStream(ms))
                {
                    Draw(data, GetImagePath(id), Env.Current.MaxWidthImg, g);
                    Draw(data, GetTmbPath(id), Env.Current.MaxWidthTmb, g);
                }
            }
        }

        private static void Draw(byte[] data, string path, int maxWidth, System.Drawing.Image g)
        {
            if (g.Width <= maxWidth)
            {
                File.WriteAllBytes(path, data);
                return;
            }
            var thumbSize = new Size(maxWidth, g.Height * maxWidth / g.Width);
            using (var imgOutput = new Bitmap(thumbSize.Width, thumbSize.Height))
            {
                imgOutput.MakeTransparent(Color.Black);
                using (var newGraphics = Graphics.FromImage(imgOutput))
                {
                    newGraphics.CompositingQuality =
                        maxWidth < 300 ? CompositingQuality.HighQuality : CompositingQuality.HighSpeed;
                    newGraphics.InterpolationMode = InterpolationMode.Bicubic;
                    newGraphics.Clear(Color.FromArgb(255, 255, 255, 255));
                    newGraphics.DrawImage(g, 0, 0, thumbSize.Width, thumbSize.Height);
                    newGraphics.Flush();
                }
                var parameters = new EncoderParameters(1);
                var parameter = new EncoderParameter(Encoder.Quality, 75L);
                parameters.Param[0] = parameter;
                var codec = GetImageCodecInfo(ImageFormat.Jpeg);
                imgOutput.Save(path, codec, parameters);
            }
        }

        private static ImageCodecInfo GetImageCodecInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageEncoders().FirstOrDefault(codec => codec.FormatID == format.Guid);
        }

        private Guid GetHash(byte[] data)
        {
            using (var md5Hasher = MD5.Create())
            {
                return new Guid(md5Hasher.ComputeHash(data));
            }
        }
    }
}