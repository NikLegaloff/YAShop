using System;

namespace YAShop.ImagesHost.Domain
{
    public class Env
    {
        private static Env _current;

        public Env()
        {
            var lower = AppDomain.CurrentDomain.BaseDirectory.ToLower();
            if (lower.Contains("prerelease")) Type=EnvType.Prerelease;
            else if (lower.Contains("live")) Type=EnvType.Live;
            else Type = EnvType.Devel;
        }

        public static Env Current =>_current?? (_current=new Env());
        public EnvType Type { get; private set; }

        public string ConnectionString
        {
            get { return "Data Source=.;Initial Catalog=ImageStore;Persist Security Info=True;User ID=sa;Password=Password1"; }
        }
        public string ImagesBasePath
        {
            get
            {
                if (Type == EnvType.Prerelease) return "Path for pre";
                if (Type == EnvType.Live) return "Path for live";
                return "C:\\Img\\";
            }
        }

    }
}