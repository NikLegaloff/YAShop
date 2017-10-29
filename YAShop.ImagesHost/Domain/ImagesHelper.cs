using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;

namespace YAShop.ImagesHost.Domain
{
    public class ImagesHelper
    {
        public void DeleteImageFile(Guid id)
        {
            File.Delete(GetImagePath(id));
            File.Delete(GetTmbPath(id));
        }

        public static string GetFolderPath(Guid id)
        {
            return $"{Env.Current.ImagesBasePath}{id.ToString().Substring(0, 2)}\\{id.ToString().Substring(2, 4)}\\";

        }
        public static string GetImagePath(Guid id)
        {
            return $"{GetFolderPath(id)}{id}.jpg";
        }

        public static string GetTmbPath(Guid id)
        {
            return $"{GetFolderPath(id)}{id}_tmb.jpg";
        }

        public Guid UploadFile(string name, byte[] data, Guid? folderId)
        {

            var all = DateTime.Now;
            var dateTime = DateTime.Now;
            var hash = GetHash(data);
            var time1 = DateTime.Now.Subtract(dateTime).TotalMilliseconds;
            dateTime = DateTime.Now;
            var size = data.Length;
            var existing = DB.ExecuteScalar<Guid?>("select id from image where hash=@hash and size=@size", new {hash, size});
            if (existing != null) return existing.Value;
            var id = Guid.NewGuid();
            var folderPath = GetFolderPath(id);
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            var time2 = DateTime.Now.Subtract(dateTime).TotalMilliseconds;
            dateTime = DateTime.Now;
            Resize(data, GetImagePath(id), Env.Current.MaxWidth);
            var time3 = DateTime.Now.Subtract(dateTime).TotalMilliseconds;
            dateTime = DateTime.Now;
            Resize(data, GetTmbPath(id), 150);
            var time4 = DateTime.Now.Subtract(dateTime).TotalMilliseconds;
            dateTime = DateTime.Now;
            DB.Execute("insert into Image(id,name,folderId,hash,size) values(@id,@name,@folderId,@hash,@size)", new {id,name,folderId,hash,size});
            File.AppendAllText("C:\\Img\\Log1.txt", time1 + "\t" + time2 + "\t" + time3 + "\t" + time4 + "\t" + DateTime.Now.Subtract(all).TotalMilliseconds + "\r\n");
            return id;
        }

        private void Resize(byte[] data, string path, int maxWidth)
        {
            if (File.Exists(path)) File.Delete(path);
            using (var ms = new MemoryStream(data))
            {
                using (var g = System.Drawing.Image.FromStream(ms))
                {
                    if (g.Width > maxWidth)
                    {
                        var thumbSize = new Size(g.Width, g.Height);

                        if (thumbSize.Width > maxWidth)
                            thumbSize = new Size(maxWidth, thumbSize.Height * maxWidth / thumbSize.Width);
                        using (var imgOutput = new Bitmap(thumbSize.Width, thumbSize.Height))
                        {
                            imgOutput.MakeTransparent();
                            imgOutput.MakeTransparent(Color.Black);
                            using (var newGraphics = Graphics.FromImage(imgOutput))
                            {
                                newGraphics.CompositingQuality = CompositingQuality.HighSpeed;
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
                    else
                    {
                        File.WriteAllBytes(path, data);
                    }
                }
            }
        }

        private static ImageCodecInfo GetImageCodecInfo(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageEncoders();

            foreach (var codec in codecs)
                if (codec.FormatID == format.Guid)
                    return codec;

            return null;
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