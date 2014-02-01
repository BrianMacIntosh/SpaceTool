using System.Drawing;
using System.IO;
using System.Windows.Forms;

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

        public static Bitmap VerifyImage(string path, PictureBox warn, ToolTip toolTip1)
        {
            Bitmap b;
            if (File.Exists(path))
            {
                b = new Bitmap(path);
            }
            else
            {
                warn.Image = Common.CriticalIcon;
                toolTip1.SetToolTip(warn, "File not found.");
                return null;
            }

            //Display good icon
            warn.Image = Common.OkIcon;
            toolTip1.SetToolTip(warn, "File accepted.");

            return b;
        }

        public static void VerifyAspect(string path, float ax, float ay, PictureBox warn, ToolTip toolTip1)
        {
            if (!File.Exists(path))
                return;
            using (Bitmap b = new Bitmap(path))
            {
                if ((ax / ay) != (b.Width / (float)b.Height))
                {
                    warn.Image = Common.SeriousIcon;
                    toolTip1.SetToolTip(warn, "Aspect ratios do not match.");
                }
                else if (ax != b.Width || ay != b.Height)
                {
                    warn.Image = Common.WarningIcon;
                    toolTip1.SetToolTip(warn, "Image sizes do not match.");
                }
            }
        }
    }
}
