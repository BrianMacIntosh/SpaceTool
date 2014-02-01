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
        private BindingList<GroupedData> firesData = new BindingList<GroupedData>();

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
            ((JsonFormTag)overrideAngleNumeric.Tag).hasValue = overrideAngleCheck;
            overrideAngleCheck.Tag = new JsonFormTag(
                null,
                "Uncheck this box to set no value for the override angle.");
            ((JsonFormTag)overrideAngleCheck.Tag).hasValueOwner = overrideAngleNumeric;

            firesListBox.Tag = new JsonFormTag(
                "stat.fires",
                "The list of projectiles this weapon fires.");
            ListBoxTagData listBoxData = new ListBoxTagData();
            listBoxData.backingList = firesData;
            listBoxData.defaultValue = new Dictionary<string, object>();
            listBoxData.deleteButton = deleteFiresButton;
            listBoxData.newButton = newFiresButton;
            ((JsonFormTag)firesListBox.Tag).listBoxData = listBoxData;

            projectileTemplateTextBox.Tag = new JsonFormTag(
                "stat.fires[].template",
                "The string ID of the projectile this source fires.",
                false);
            ((JsonFormTag)projectileTemplateTextBox.Tag).ownerListBox = firesListBox;
            fireRateNumeric.Tag = new JsonFormTag(
                "stat.fires[].rate",
                "The fire rate of this source in shots per second.");
            ((JsonFormTag)fireRateNumeric.Tag).ownerListBox = firesListBox;
            atAngleNumeric.Tag = new JsonFormTag(
                "stat.fires[].atAngle",
                "An angle value in radians to add to the player's aim angle.",
                false);
            ((JsonFormTag)atAngleNumeric.Tag).ownerListBox = firesListBox;
            firePointXNumeric.Tag = new JsonFormTag(
                "stat.fires[].firePoint.x",
                "The X offset from the weapon where the projectile spawns.",
                false);
            ((JsonFormTag)firePointXNumeric.Tag).ownerListBox = firesListBox;
            firePointYNumeric.Tag = new JsonFormTag(
                "stat.fires[].firePoint.y",
                "The Y offset from the weapon where the projectile spawns.",
                false);
            ((JsonFormTag)firePointYNumeric.Tag).ownerListBox = firesListBox;
            spreadMeanNumeric.Tag = new JsonFormTag(
                "stat.fires[].spread.mean",
                "The mean fire spread, in radians.",
                false);
            ((JsonFormTag)spreadMeanNumeric.Tag).ownerListBox = firesListBox;
            stdDevNumeric.Tag = new JsonFormTag(
                "stat.fires[].spread.stdDev",
                "The standard deviation of the spread, in radians.",
                false);
            ((JsonFormTag)stdDevNumeric.Tag).ownerListBox = firesListBox;
            soundFileTextBox.Tag = new JsonFormTag(
                "stat.fires[].soundFile",
                "Relative path to a sound to play when the weapon is fired.",
                false);
            ((JsonFormTag)soundFileTextBox.Tag).ownerListBox = firesListBox;
            kickNumeric.Tag = new JsonFormTag(
                "stat.fires[].kick",
                "Pixels of kick to apply to the screen of the weapon's user when fired.",
                false);
            ((JsonFormTag)kickNumeric.Tag).ownerListBox = firesListBox;

            textureFileTextBox.TextChanged += TextureFileChanged;
            soundFileTextBox.TextChanged += SoundFileChanged;

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
            using (Bitmap b = Common.VerifyImage(Common.GetPathForMedia(text), textureFileWarn, toolTip1))
            {
                if (b == null)
                    return;

                //Set sizes
                textureWNumeric.Value = b.Width;
                textureHNumeric.Value = b.Height;
            }
        }

        void SoundFileChanged(object sender, EventArgs e)
        {
            string text = ((Control)sender).Text;

            if (string.IsNullOrEmpty(text))
            {
                soundFilePictureBox.Image = null;
                toolTip1.SetToolTip(soundFilePictureBox, null);
                return;
            }

            //Verify file exists
            if (!File.Exists(Common.GetPathForMedia(text)))
            {
                soundFilePictureBox.Image = Common.CriticalIcon;
                toolTip1.SetToolTip(soundFilePictureBox, "File not found.");
            }
            else
            {
                soundFilePictureBox.Image = Common.OkIcon;
                toolTip1.SetToolTip(soundFilePictureBox, "File accepted.");
            }
        }
    }
}
