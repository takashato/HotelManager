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
using HotelManager.db.model;
using System.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Windows.Controls.Primitives;

namespace HotelManager.gui
{
    /// <summary>
    /// Interaction logic for ThuePhong.xaml
    /// </summary>
    public partial class RentingWindow : Window
    {
        public ObservableCollection<KhachHang> ListGuestsRenting { get; set; } = new ObservableCollection<KhachHang>();
        private Room _roomToRent;


        public RentingWindow(Room roomToRent)
        {
            InitializeComponent();

            _roomToRent = roomToRent;
            txbRoomName.Text = "Phòng " + _roomToRent.Name;
        }

        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnHoanThanh_Click(object sender, RoutedEventArgs e)
        {
            _roomToRent.Status = Room.EStatus.NotAvailable;
            // TODO: Update Danh sách khách hàng thuê phòng cho _roomToRent
            this.Close();
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();          
        }

    }

    public class DataGridBehavior //Copy trên stackoverflow để fix lỗi khi xóa hoặc thêm một dòng thì row header không update
    {
        #region DisplayRowNumber

        public static DependencyProperty DisplayRowNumberProperty =
            DependencyProperty.RegisterAttached("DisplayRowNumber",
                                                typeof(bool),
                                                typeof(DataGridBehavior),
                                                new FrameworkPropertyMetadata(false, OnDisplayRowNumberChanged));
        public static bool GetDisplayRowNumber(DependencyObject target)
        {
            return (bool)target.GetValue(DisplayRowNumberProperty);
        }
        public static void SetDisplayRowNumber(DependencyObject target, bool value)
        {
            target.SetValue(DisplayRowNumberProperty, value);
        }

        private static void OnDisplayRowNumberChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            DataGrid dataGrid = target as DataGrid;
            if ((bool)e.NewValue == true)
            {
                EventHandler<DataGridRowEventArgs> loadedRowHandler = null;
                loadedRowHandler = (object sender, DataGridRowEventArgs ea) =>
                {
                    if (GetDisplayRowNumber(dataGrid) == false)
                    {
                        dataGrid.LoadingRow -= loadedRowHandler;
                        return;
                    }
                    ea.Row.Header = ea.Row.GetIndex() + 1;
                };
                dataGrid.LoadingRow += loadedRowHandler;

                ItemsChangedEventHandler itemsChangedHandler = null;
                itemsChangedHandler = (object sender, ItemsChangedEventArgs ea) =>
                {
                    if (GetDisplayRowNumber(dataGrid) == false)
                    {
                        dataGrid.ItemContainerGenerator.ItemsChanged -= itemsChangedHandler;
                        return;
                    }
                    GetVisualChildCollection<DataGridRow>(dataGrid).
                        ForEach(d => d.Header = d.GetIndex() + 1);
                };
                dataGrid.ItemContainerGenerator.ItemsChanged += itemsChangedHandler;
            }
        }

        #endregion // DisplayRowNumber

        #region Get Visuals

        private static List<T> GetVisualChildCollection<T>(object parent) where T : Visual
        {
            List<T> visualCollection = new List<T>();
            GetVisualChildCollection(parent as DependencyObject, visualCollection);
            return visualCollection;
        }

        private static void GetVisualChildCollection<T>(DependencyObject parent, List<T> visualCollection) where T : Visual
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    visualCollection.Add(child as T);
                }
                if (child != null)
                {
                    GetVisualChildCollection(child, visualCollection);
                }
            }
        }

        #endregion // Get Visuals
    }

    public enum LoaiKhachHang
    {
        [Description("Nội địa")]
        NoiDia,
        [Description("Nước ngoài")]
        NguocNgoai,
    }

    public class KhachHang
    {
        // Còn bug ở STT: Khi xóa một hàng (nhấn nút Delete), sau đó thêm một hàng (nhấn Enter). STT hiển thị sẽ không tăng dần như mong muốn (1 2 3 4) nữa
        public int STT { get; set; } = ++count;
        public string HoTen { get; set; } = HoTen_MacDinh;
        public LoaiKhachHang LoaiKh { get; set; }
        public string CMND { get; set; } = CMND_MacDinh;
        public string DiaChi { get; set; } = DiaChi_MacDinh;

        public static int count = 0;
        // Nếu giá trị bằng ThuocTinh_MacDinh tức là chưa được điền ở datagrid
        static string HoTen_MacDinh = "Họ tên";
        static string CMND_MacDinh = "123456789";
        static string DiaChi_MacDinh = "Địa chỉ";
    }
}
