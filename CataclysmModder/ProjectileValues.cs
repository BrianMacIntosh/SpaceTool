using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CataclysmModder
{
    public partial class ProjectileValues : UserControl
    {
        public ProjectileValues()
        {
            InitializeComponent();

            idTextBox.Tag = new JsonFormTag(
                "id",
                "A unique string identifier for this template.");
            ptypeComboBox.Tag = new JsonFormTag(
                "type",
                "The type of projectile.");
            textureFileTextBox.Tag = new JsonFormTag(
                "textureFile",
                "Path to the main texture for this weapon. Paths are relative to the 'data' folder.");
            textureWNumeric.Tag = new JsonFormTag(
                "textureSize.x",
                "The width of the texture image.");
            textureHNumeric.Tag = new JsonFormTag(
                "textureSize.y",
                "The height of the texture image.");
            fixedCheckBox.Tag = new JsonFormTag(
                "fixedOrientation",
                "If set, the projectile will not rotate to face its velocity.");

            maxSpeedNumeric.Tag = new JsonFormTag(
                "stat.movementStats.maxSpeed",
                "The maximum movement speed (pixels per second).");
            minSpeedNumeric.Tag = new JsonFormTag(
                "stat.movementStats.minSpeed",
                "The minimum movement speed (pixels per second).",
                false);
            maxAccelNumeric.Tag = new JsonFormTag(
                "stat.movementStats.maxAccel",
                "The acceleration (pixels per second per second).");
            minDecelNumeric.Tag = new JsonFormTag(
                "stat.movementStats.minDecel",
                "The minimum decceleration (pixels per second per second).");

            pixelKillNumeric.Tag = new JsonFormTag(
                "stat.damagePacket.pixelKill",
                "Destroys unarmored pixels in this radius.");
            pixelDmgNumeric.Tag = new JsonFormTag(
                "stat.damagePacket.pixelDamage",
                "Damages unarmored pixels in this radius (damage is visual only).");
            pixelDebuffNumeric.Tag = new JsonFormTag(
                "stat.damagePacket.pixelDebuff",
                "Inflict the laser debuff on unarmored pixels in this radius.");
            armorKillNumeric.Tag = new JsonFormTag(
                "stat.damagePacket.armorKill",
                "Destroys armored pixels in this radius.");
            armorDmgNumeric.Tag = new JsonFormTag(
                "stat.damagePacket.armorDamage",
                "Damages armored pixels in this radius (damage is visual only).");
            armorDebuffNumeric.Tag = new JsonFormTag(
                "stat.damagePacket.armorDebuff",
                "Inflict the laser debuff on armored pixels in this radius.");
            coreDmgNumeric.Tag = new JsonFormTag(
                "stat.damagePacket.coreDamage",
                "Amount of damage done to ship core on direct impact.");

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
            using (Bitmap b = Common.VerifyImage(Common.GetPathForMedia(text), textureFileWarn, toolTip1))
            {
                if (b == null)
                    return;

                //Set sizes
                textureWNumeric.Value = b.Width;
                textureHNumeric.Value = b.Height;
            }
        }
    }
}
