using FUMiniHotelSystem.dto;
using FUMiniHotelSystem.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FUMiniHotelSystem.viewModel.admin.room
{
    /// <summary>
    /// Interaction logic for AddRoomDialog.xaml
    /// </summary>
    public partial class AddRoomDialog : Window
    {
        private readonly RoomService _roomService;
        private readonly int? _roomId;

        public AddRoomDialog(RoomService roomService, int? roomId = null)
        {
            InitializeComponent();
            _roomService = roomService;
            _roomId = roomId;

            RoomTypeComboBox.ItemsSource = _roomService.GetAllRoomTypes();

            if (_roomId.HasValue)
            {
                LoadRoomData(_roomId.Value);
            }
        }

        private void LoadRoomData(int roomId)
        {
            var room = _roomService.GetRoomById(roomId);
            RoomNumberTextBox.Text = room.RoomNumber;
            RoomDescriptionTextBox.Text = room.RoomDetailDescription;
            RoomTypeComboBox.SelectedItem = RoomTypeComboBox.Items.Cast<RoomTypeDTO>()
        .FirstOrDefault(rt => rt.RoomTypeID == room.RoomType.RoomTypeID);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (RoomTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a room type.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var roomDTO = new RoomDTO
            {
                RoomID = _roomId ?? 0,
                RoomNumber = RoomNumberTextBox.Text,
                RoomDetailDescription = RoomDescriptionTextBox.Text,
                RoomType = (RoomTypeDTO)RoomTypeComboBox.SelectedItem
            };

            try
            {
                if (_roomId.HasValue)
                {
                    _roomService.UpdateRoom(roomDTO);
                }
                else
                {
                    _roomService.AddRoom(roomDTO);
                }

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
