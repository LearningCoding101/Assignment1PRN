﻿using FUMiniHotelSystem.viewModel.admin.booking;
using FUMiniHotelSystem.viewModel.admin.customer;
using FUMiniHotelSystem.viewModel.admin.room;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace FUMiniHotelSystem.viewModel.admin
{
    /// <summary>
    /// Interaction logic for dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        private readonly Dictionary<string, Page> _pageCache;

        public Dashboard()
        {
            InitializeComponent();
            _pageCache = new Dictionary<string, Page>
            {
                { "ManageCustomers", new ManageCustomer() },
                { "ManageRooms", new viewRoom() },
                { "ManageBookings", new ManageBooking() },
                { "GenerateReports", new viewRoom() }
            };
        }

        private void NavigateToManageCustomers(object sender, RoutedEventArgs e)
        {
            NavigateToPage("ManageCustomers");
        }

        private void NavigateToManageRooms(object sender, RoutedEventArgs e)
        {
            NavigateToPage("ManageRooms");
        }

        private void NavigateToManageBookings(object sender, RoutedEventArgs e)
        {
            NavigateToPage("ManageBookings");
        }

        private void NavigateToGenerateReports(object sender, RoutedEventArgs e)
        {
            NavigateToPage("GenerateReports");
        }

        private void NavigateToPage(string pageKey)
        {
            if (_pageCache.TryGetValue(pageKey, out var page))
            {
                ContentFrame.Navigate(page);
            }
            else
            {
                MessageBox.Show($"Page {pageKey} not found.", "Navigation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
