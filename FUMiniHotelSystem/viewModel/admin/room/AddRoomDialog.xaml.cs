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

            // Load room data if editing existing room
            if (_roomId.HasValue)
            {
                LoadRoomData(_roomId.Value);
            }
            else
            {
                // Load room types into the combo box for new room addition
                RoomTypeComboBox.ItemsSource = _roomService.GetAllRoomTypes();
            }
        }

        private void LoadRoomData(int roomId)
        {
            var room = _roomService.GetRoomById(roomId);
            RoomNumberTextBox.Text = room.RoomNumber;
            RoomDescriptionTextBox.Text = room.RoomDetailDescription;
            RoomTypeComboBox.SelectedItem = room.RoomType.RoomTypeName;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var roomDTO = new RoomDTO
            {
                RoomID = _roomId ?? 0,
                RoomNumber = RoomNumberTextBox.Text,
                RoomDetailDescription = RoomDescriptionTextBox.Text,
                RoomType = (RoomTypeDTO)RoomTypeComboBox.SelectedItem
            };

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
    }
}
