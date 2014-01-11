using System.Drawing;
using System.IO;

namespace CataclysmModder
{
    static class Common
    {
        public static Bitmap CriticalIcon
        {
            get
            {
                return CataclysmModder.Properties.Resources.Critical;
            }
        }
        public static Bitmap SeriousIcon
        {
            get
            {
                return CataclysmModder.Properties.Resources.Serious;
            }
        }
        public static Bitmap WarningIcon
        {
            get
            {
                return CataclysmModder.Properties.Resources.Warning;
            }
        }
        public static Bitmap OkIcon
        {
            get
            {
                return CataclysmModder.Properties.Resources.OK;
            }
        }

        public static string GetPathForMedia(string rel)
        {
            string[] fsplit = rel.Split('/'); //Get rid of file top-level directory
            string fname = string.Join("/", fsplit, 1, fsplit.Length - 1);
            return Path.Combine(Storage.WorkingDirectory, fname);
        }
    }
}
