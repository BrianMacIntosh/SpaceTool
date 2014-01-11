using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace CataclysmModder
{
    public partial class ShipValues : UserControl
    {
        public ShipValues()
        {
            InitializeComponent();

            idTextBox.Tag = new JsonFormTag(
                "id",
                "A unique string identifier for this ship.");
            nameTextBox.Tag = new JsonFormTag(
                "name",
                "The display name of this class of ship.");
            shortDescTextBox.Tag = new JsonFormTag(
                "descShort",
                "A short description of this ship’s main attributes for menus.");
            descriptionTextBox.Tag = new JsonFormTag(
                "desc",
                "A description of a few sentences or a paragraph giving the characteristics and flavor of this ship.");
            textureFileTextBox.Tag = new JsonFormTag(
                "textureFile",
                "Path to the main texture for this ship. Paths are relative to the 'data' folder.");
            textureWNumeric.Tag = new JsonFormTag(
                "textureSize.x",
                "The width of the texture image.");
            textureHNumeric.Tag = new JsonFormTag(
                "textureSize.y",
                "The height of the texture image.");
            maskFileTextBox.Tag = new JsonFormTag(
                "maskFile",
                "Path to the mask texture for this ship.");
            damageFileTextBox.Tag = new JsonFormTag(
                "damageFile",
                "Path to the damaged texture for this ship.");
            coreXNumeric.Tag = new JsonFormTag(
                "coreOffset.x",
                "The x position of the core in pixels relative to the image top-left.");
            coreYNumeric.Tag = new JsonFormTag(
                "coreOffset.y",
                "The y position of the core in pixels relative to the image top-left.");
            coreRadiusNumeric.Tag = new JsonFormTag(
                "coreRadius",
                "The radius, in pixels, of the core.",
                true, 12m);
            maxSpeedNumeric.Tag = new JsonFormTag(
                "stat.movementStats.maxSpeed",
                "The maximum movement speed of this ship (pixels per second).");
            minSpeedNumeric.Tag = new JsonFormTag(
                "stat.movementStats.minSpeed",
                "The minimum movement speed of this ship (pixels per second).",
                false);
            maxAccelNumeric.Tag = new JsonFormTag(
                "stat.movementStats.maxAccel",
                "The acceleration of this ship (pixels per second per second).");
            minDecelNumeric.Tag = new JsonFormTag(
                "stat.movementStats.minDecel",
                "The minimum decceleration of this ship when no input is given (pixels per second per second).");

            textureFileTextBox.TextChanged += TextureFileChanged;
            maskFileTextBox.TextChanged += MaskFileChanged;
            damageFileTextBox.TextChanged += DamageFileChanged;

            WinformsUtil.ControlsAttachHooks(this);
            WinformsUtil.TagsSetDefaults(this);
        }

        void TextureFileChanged(object sender, EventArgs e)
        {
            string text = ((Control)sender).Text;

            if (string.IsNullOrEmpty(text))
            {
                textureFileWarn.Image = null;
                toolTip1.SetToolTip(textureFileWarn, null);
                return;
            }

            //Verify file exists
            using (Bitmap b = VerifyImage(Common.GetPathForMedia(text), textureFileWarn))
            {
                if (b == null)
                    return;

                //Set sizes
                textureWNumeric.Value = b.Width;
                textureHNumeric.Value = b.Height;

                //Verify other image sizes
                MaskFileChanged(maskFileTextBox, null);
                DamageFileChanged(damageFileTextBox, null);
            }
        }

        void MaskFileChanged(object sender, EventArgs e)
        {
            string text = ((Control)sender).Text;

            if (string.IsNullOrEmpty(text))
            {
                maskFileWarn.Image = null;
                toolTip1.SetToolTip(maskFileWarn, null);
                return;
            }

            using (Bitmap b = VerifyImage(Common.GetPathForMedia(text), maskFileWarn))
            {
                if (b == null)
                    return;

                VerifyAspect(
                    Common.GetPathForMedia(((Control)sender).Text),
                    (float)textureWNumeric.Value, (float)textureHNumeric.Value,
                    maskFileWarn);
            }
        }

        void DamageFileChanged(object sender, EventArgs e)
        {
            string text = ((Control)sender).Text;

            if (string.IsNullOrEmpty(text))
            {
                damageFileWarn.Image = null;
                toolTip1.SetToolTip(damageFileWarn, null);
                return;
            }

            using (Bitmap b = VerifyImage(Common.GetPathForMedia(text), damageFileWarn))
            {
                if (b == null)
                    return;

                VerifyAspect(
                    Common.GetPathForMedia(((Control)sender).Text),
                    (float)textureWNumeric.Value, (float)textureHNumeric.Value,
                    damageFileWarn);
            }
        }

        private Bitmap VerifyImage(string path, PictureBox warn)
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

        private void VerifyAspect(string path, float ax, float ay, PictureBox warn)
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
