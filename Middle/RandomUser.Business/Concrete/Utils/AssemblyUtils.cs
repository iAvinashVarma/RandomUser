using System;
using System.IO;
using System.Reflection;

namespace RandomUser.Business.Concrete.Utils
{
    public static class AssemblyUtils
    {
        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }

}
