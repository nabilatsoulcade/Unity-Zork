using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace Zork.Builder
{
    public partial class MainForm : Form
    {
        private GameViewModel mCurrentFile;

        public GameViewModel ViewModel { get; set; }

        public MainForm()
        {
            InitializeComponent();
            ViewModel = new GameViewModel();
        }

        // Exit from File dropdown
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Print from File dropdown
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
         if (PrintDialog.ShowDialog() == DialogResult.OK)
            {

            }   
        }

        // Open file from File dropdown
        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                ViewModel.FileName = OpenFileDialog.FileName;
                ViewModel.Game = Common.Game.Load(ViewModel.FileName);
                roomsBindingSource.DataSource = ViewModel.Rooms;
                

                // Programatic section
                /*foreach (Common.Room room in CurrentFile.Game.World.Rooms)
                {
                    RoomListBox.Items.Add(room.Name);
                }*/

                //RoomListBox.SelectedIndex = 0;
                //NameTextBox.Text = RoomListBox.SelectedItem.ToString();
            }
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        // Save from File dropdown
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        { 
                //string jsonText = 
                //SaveFileDialog.FileName = ZorkBuilderFile.FileName;
        }

        // SaveAs from File dropdown
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //string jsonText =
                SaveFileDialog.FileName = ViewModel.FileName;
            }
        }

        private void RoomListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Programatic section
            /*string currentSelected = RoomListBox.SelectedItem.ToString();
            NameTextBox.Text = CurrentFile.Game.World.RoomsByName[currentSelected].Name;
            LookTextBox.Text = CurrentFile.Game.World.RoomsByName[currentSelected].Description;
            */
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            /*string currentSelected = RoomListBox.SelectedItem.ToString();
            CurrentFile.Game.World.RoomsByName[currentSelected].Name = NameTextBox.Text;

            // Refresh section
            RoomListBox.Items.Clear();
            foreach (Common.Room room in CurrentFile.Game.World.Rooms)
            {
                RoomListBox.Items.Add(room.Name);
            }*/
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void WorldBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void LookTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (RoomListBox.SelectedItem != null)
            {
                int test = RoomListBox.SelectedIndex;
                ViewModel.Rooms.RemoveAt(test);
            }
        }
         
        private void AddRoomButton_Click(object sender, EventArgs e)
        {
            ViewModel.Rooms.Add(new Common.Room() { Name = "New Room", Description = "New Description" }) ;
        }
    }
}
