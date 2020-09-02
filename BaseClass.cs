
namespace HatidPress
{
    public class HPHost
    {
        private static HPHost instance;
        public static HPHost Instance
        {
            get
            {
                if (instance == null)
                    instance = new HPHost();
                return instance;
            }
        }

        private bool isInitialized = false;
        private string baseUrl = "http://localhost";
        public string BaseDomain
        {
            get
            {
                return baseUrl + "/wp-json";
            }
        }

        public void Initialized(string url)
        {
            if (!isInitialized)
            {
                baseUrl = url;
                isInitialized = true;
            }
        }

    }
}
