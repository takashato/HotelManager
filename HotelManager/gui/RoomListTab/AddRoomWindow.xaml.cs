﻿using System;
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
using HotelManager.db.model;

namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for AddRoomWindow.xaml
    /// </summary>
    public partial class AddRoomWindow : Window
    {
        public AddRoomWindow()
        {
            InitializeComponent();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbRoomType.ItemsSource = RoomType.GetRoomType();
            cbRoomType.DisplayMemberPath = "Type";
            cbRoomType.SelectedValuePath = "Price";
        }

        private string PriceStr => string.Format("{0:N0}", cbRoomType.SelectedValue);

        private void CbRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbPrice.Text = PriceStr;
        }

        private void BtnAddRoom_Click(object sender, RoutedEventArgs e)
        {

            if (Room.InsertRoom(txbRoomName.Text, cbRoomType.Text, txbNote.Text))
            {
                MessageBox.Show("Thêm thành công!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Thêm không thành công!\nTên phòng đã có");
                this.Close();
            }
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            List<Room> rooms = new List<Room>();
            rooms.Clear();
            rooms.AddRange(Room.GetAll());

            HotelManager.gui.RoomListUC.RoomList.Clear();
            foreach (var item in rooms)
            {
                item.RetrieveRentInfo();
                HotelManager.gui.RoomListUC.RoomList.Add(item);
            }
            CollectionViewSource.GetDefaultView(HotelManager.gui.RoomListUC.RoomList).Refresh();
        }
    }
}
