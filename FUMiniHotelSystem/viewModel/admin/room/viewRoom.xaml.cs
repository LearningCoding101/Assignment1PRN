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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

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
            MessageBox.Show($"Connection String: {connectionString}", "Connection String Verification", MessageBoxButton.OK, MessageBoxImage.Information);

            _roomService = new RoomService(new RoomRepository(connectionString), new RoomTypeRepository(connectionString));
            LoadRoomData();
        }

        private void LoadRoomData()
        {
            List<RoomDTO> rooms = _roomService.GetAllRoom();
            RoomDataGrid.ItemsSource = rooms;
        }
    }
}
