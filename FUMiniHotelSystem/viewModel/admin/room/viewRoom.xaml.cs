using FUMiniHotelSystem.dto;
using FUMiniHotelSystem.service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace FUMiniHotelSystem.viewModel.admin.room
{
    /// <summary>
    /// Interaction logic for viewRoom.xaml
    /// </summary>
    public partial class viewRoom : Page
    {
        private readonly RoomService _roomService;

        public viewRoom()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.AppSettings["MyDbConnectionString"];

            _roomService = new RoomService(new RoomRepository(connectionString), new RoomTypeRepository(connectionString));
            LoadRoomData();
        }

        private void LoadRoomData()
        {
            List<RoomDTO> rooms = _roomService.GetAllRooms();
            RoomDataGrid.ItemsSource = rooms;
        }

        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            var addRoomDialog = new AddRoomDialog(_roomService);
            if (addRoomDialog.ShowDialog() == true)
            {
                LoadRoomData(); 
            }
        }

        private void UpdateRoom_Click(object sender, RoutedEventArgs e)
        {
            int roomId = (int)((Button)sender).Tag;
            var UpdateRoomDialog = new AddRoomDialog(_roomService, roomId);
            if (UpdateRoomDialog.ShowDialog() == true)
            {
                LoadRoomData(); 
            }
        }

        private void DeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            int roomId = (int)((Button)sender).Tag;

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this room?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _roomService.DeleteRoom(roomId);
                LoadRoomData(); 
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = SearchTextBox.Text;
            var filteredRooms = _roomService.SearchRooms(keyword);
            RoomDataGrid.ItemsSource = filteredRooms;
        }
    }
}
