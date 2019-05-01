
using MercuryBOT.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MercuryBOT.AccSettings
{
    public partial class ProfilePrivacy : MetroFramework.Forms.MetroForm
    {
        public ProfilePrivacy()
        {
            InitializeComponent();
            this.components.SetStyle(this);
            Region = Region.FromHrgn(Helpers.Extensions.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
        }

        private void ProfilePrivacy_Load(object sender, EventArgs e)
        {
            CollectPrivacySettings.RunWorkerAsync();
            btn_changeprofSettings.Enabled = false;
        }

        private void CollectPrivacySettings_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                foreach (KeyValuePair<string, int> setting in AccountLogin.GetProfileSettings())
                {
                    if (setting.Key == "PrivacyProfile")
                    {

                        combox_profilePrivacy.Invoke((MethodInvoker)delegate
                        {
                            combox_profilePrivacy.SelectedIndex = setting.Value;
                        });
                    }
                    if (setting.Key == "PrivacyFriendsList")
                    {
                        combox_profilePrivacy.Invoke((MethodInvoker)delegate
                        {
                            combox_FriendsList.SelectedIndex = setting.Value;
                        });
                    }
                    if (setting.Key == "ECommentPermission")
                    {
                        combox_Comment.Invoke((MethodInvoker)delegate
                        {
                            combox_Comment.SelectedIndex = setting.Value;
                        });
                    }
                    /*------------------------------*/
                    if (setting.Key == "PrivacyInventory")
                    {
                        combox_InventoryPrivacy.Invoke((MethodInvoker)delegate
                        {
                            combox_InventoryPrivacy.SelectedIndex = setting.Value;
                        });
                    }
                    if (setting.Key == "PrivacyInventoryGifts")
                    {
                        combox_Gifts.Invoke((MethodInvoker)delegate
                        {
                            combox_Gifts.SelectedIndex = setting.Value;
                        });
                    }
                    /*------------------------------*/
                    if (setting.Key == "PrivacyOwnedGames")
                    {
                        combox_ownedGames.Invoke((MethodInvoker)delegate
                        {
                            combox_ownedGames.SelectedIndex = setting.Value;
                        });
                    }
                    if (setting.Key == "PrivacyPlaytime")
                    {
                        combox_PlayTime.Invoke((MethodInvoker)delegate
                        {
                            combox_PlayTime.SelectedIndex = setting.Value;
                        });
                    }
                }

                FidgetSpinner.Invoke((MethodInvoker)delegate
                {
                    FidgetSpinner.Visible = false;
                });

                olamongo.Invoke((MethodInvoker)delegate
                {
                    olamongo.Visible = false;
                });

                btn_changeprofSettings.Invoke((MethodInvoker)delegate
                {
                    btn_changeprofSettings.Enabled = true;
                });
            }
            catch (Exception)
            {
                Console.WriteLine("Login again");
            }
        }

        private void btn_changeprofSettings_Click(object sender, EventArgs e)
        {
            AccountLogin.ProfileSettings(combox_profilePrivacy.SelectedIndex,
                combox_InventoryPrivacy.SelectedIndex,
                combox_Gifts.SelectedIndex,
                combox_ownedGames.SelectedIndex,
                combox_PlayTime.SelectedIndex,
                combox_FriendsList.SelectedIndex,
                combox_Comment.SelectedIndex);
            Close();
        }

        private void combox_profilePrivacy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (combox_profilePrivacy.SelectedIndex)
            {
                //Unknown,Private, FriendsOnly,Public
                case 1://Private
                    combox_InventoryPrivacy.SelectedIndex = 1;
                    combox_Gifts.SelectedIndex = 1;
                    combox_ownedGames.SelectedIndex = 1;
                    combox_PlayTime.SelectedIndex = 1;
                    combox_FriendsList.SelectedIndex = 1;
                    combox_Comment.SelectedIndex = 2; //FriendsOnly,Public,Private
                    break;
                case 2://FriendsOnly
                    combox_InventoryPrivacy.SelectedIndex = 2;
                    combox_Gifts.SelectedIndex = 2;
                    combox_ownedGames.SelectedIndex = 2;
                    combox_PlayTime.SelectedIndex = 1;
                    combox_FriendsList.SelectedIndex = 2;
                    combox_Comment.SelectedIndex = 0; //FriendsOnly,Public,Private
                    break;

                case 3://Public
                    combox_InventoryPrivacy.SelectedIndex = 3;
                    combox_Gifts.SelectedIndex = 3;
                    combox_ownedGames.SelectedIndex = 3;
                    combox_PlayTime.SelectedIndex = 3;
                    combox_FriendsList.SelectedIndex = 3;
                    combox_Comment.SelectedIndex = 1; //FriendsOnly,Public,Private

                    break;
            }

        }

        private void combox_ownedGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Unknown,Private, FriendsOnly,Public
            switch (combox_ownedGames.SelectedIndex)
            {
                //combox_PlayTime privado =1 | desligar= 3;
                case 1:
                    combox_PlayTime.SelectedIndex = 3;
                    break;
            }
        }
    }
}
