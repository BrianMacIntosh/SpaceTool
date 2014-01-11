using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CataclysmModder
{
    public partial class WeaponValues : UserControl
    {
        public WeaponValues()
        {
            InitializeComponent();

            idTextBox.Tag = new JsonFormTag(
                "id",
                "A unique string identifier for this ship.");
            nameTextBox.Tag = new JsonFormTag(
                "name",
                "The display name of this type of weapon.");
            shortDescTextBox.Tag = new JsonFormTag(
                "desc",
                "A description of a few sentences giving the characteristics and flavor of this weapon.");
            textureFileTextBox.Tag = new JsonFormTag(
                "textureFile",
                "Path to the main texture for this weapon. Paths are relative to the 'data' folder.");
            textureWNumeric.Tag = new JsonFormTag(
                "textureSize.x",
                "The width of the texture image.");
            textureHNumeric.Tag = new JsonFormTag(
                "textureSize.y",
                "The height of the texture image.");
            classNumeric.Tag = new JsonFormTag(
                "wclass",
                "The minimum hardpoint rating required to support this weapon.",
                true, 1m);
            autoFireCheckBox.Tag = new JsonFormTag(
                "stat.autoFire",
                "If true, the weapon can fire with the control held down.",
                false);
            bindsControlNumeric.Tag = new JsonFormTag(
                "stat.bindsControl",
                "The button that controls fires this weapon.  Use -1 and -3 for left and right mouse, 0-9 for number keys.");
            overrideAngleNumeric.Tag = new JsonFormTag(
                "stat.overrideAngle",
                "Locks the gun to the specified angle in radians.",
                false);

            textureFileTextBox.TextChanged += TextureFileChanged;

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
    }
}
