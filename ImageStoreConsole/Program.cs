using System;
using System.IO;
using System.Windows.Forms;
using Sprut.ImageStoreClient;

namespace ImageStoreConsole
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var imageClient = new ImageStoreHostClient();

            string select = null;
            do
            {
                //Console.Clear();
                Console.WriteLine("Main menu");
                Console.WriteLine("1. Add new root folder");
                Console.WriteLine("2. Add new folder 2 level");
                Console.WriteLine("3. Add new folder 3 level");
                Console.WriteLine("4. Upload JPG file to root");
                Console.WriteLine("5. GetImageUrl");
                Console.WriteLine("0. Exit");
                select = Console.ReadLine();

                switch (select)
                {
                    case "1":
                        imageClient.AddFolder("Folder num.1", "");
                        break;
                    case "2":
                        imageClient.AddFolder("Folder 2 level", "Folder num.1");
                        break;
                    case "3":
                        imageClient.AddFolder("Folder 3 level", "Folder num.1\\Folder 2 level");
                        break;
                    case "4":
                        var openFileDialog = new OpenFileDialog();
                        openFileDialog.Filter = "jpg files (*.jpg)|*.jpg";
                        openFileDialog.ShowDialog();
                        var imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                        imageClient.UploadImage(imageBytes, openFileDialog.FileName, "");
                        break;
                    case "5":
                        Console.WriteLine(imageClient.GetImageUrl(Guid.Parse("A9EA5375-FCE0-4E90-A5B3-408B5F53C412")));
                        break;
                }
            } while (select != "0");
        }
    }
}