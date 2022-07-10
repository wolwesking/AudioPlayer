using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlayerApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Global variables
        String[] paths = { };
        String[] fileN = { };

        //Playlist
        int selectedFile = 0;

        //FileSystem
        OpenFileDialog ofd = new OpenFileDialog();

        private void BT_pc_Click(object sender, EventArgs e)
        {
            VideoPlayer.Ctlcontrols.stop();

            if (fileN.Length > 1)
            {
                fileN = null;

                PlayList.Items.Clear();

                selectedFile = 0;
                MessageBox.Show("Your playlist is cleard");
            }
            //Settings
            ofd.Filter = "Audio (*.mp3,*.acc,*.wma,*.m4a,*.avi,*.aif,*.aifc,*.aiff,*.wav,*.cda,*.adt,*.adts)|*.mp3;*.acc;*.wma;*.m4a;*.avi;*.aif;*.aifc;*.aiff;*.wav;*.cda;*.adt;*.adts|All Files (*.*)|*.*";
            ofd.Multiselect = true;

            //Open dialog
            DialogResult result = ofd.ShowDialog();

            //Check if its opened
            if (result == DialogResult.OK)
            {
                // Get data
                fileN = ofd.SafeFileNames;
                paths = ofd.FileNames;

                LoadPlayList();
            }

        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedFile = PlayList.SelectedIndex;
            PlayFile(PlayList.SelectedItem.ToString());

        }

        private void PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {

        }

        private void LoadPlayList()
        {
            VideoPlayer.currentPlaylist = VideoPlayer.newPlaylist("Playlist", "");

            foreach (string audio in paths)
            {
                VideoPlayer.currentPlaylist.appendItem(VideoPlayer.newMedia(audio));
                PlayList.Items.Add(audio);
            }

            if (paths.Length > 0)
            {
                PlayList.SelectedIndex = selectedFile;
                PlayFile(PlayList.SelectedItem.ToString());
            }
        }

        private void PlayFile(string url)
        {
            VideoPlayer.URL = url;
        }
    }
}
