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

namespace LanguageSchool.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        List<ModelData.Service> ServicesList = Classes.DataClass.RE.Service.ToList();
        double res;
        double DiscountDouble;
        TextBlock TBFalseCount;
        public AdminPage()
        {
            InitializeComponent();
            ServicesDG.ItemsSource = ServicesList;
        }
        int i = -1;
        private void MediaElement_Initialized(object sender, EventArgs e)
        {
            i++;
            if (i< ServicesList.Count)
            {
                MediaElement image = (MediaElement)sender;
                ModelData.Service ServiceImg = ServicesList[i];
                Uri PathImg = new Uri(ServiceImg.MainImagePath, UriKind.RelativeOrAbsolute);
                image.Source = PathImg;
            }
        }
        private void StackPanel_Initialized(object sender, EventArgs e)
        {
            if(i< ServicesList.Count)
            {
                StackPanel stpan = (StackPanel)sender;
                ModelData.Service ser = ServicesList[i];
                if(ser.Discount != 0)
                {
                    stpan.Background = new SolidColorBrush(Color.FromRgb(231, 250, 191));
                }
            }
        }
        private void Edit_Initialized(object sender, EventArgs e)
        {
            Button BEdit = (Button)sender;
            if(BEdit != null)
            {
                BEdit.Uid = Convert.ToString(i);
            }
        }

        private void Delete_Initialized(object sender, EventArgs e)
        {
            Button BDelete = (Button)sender;
            if (BDelete != null)
            {
                BDelete.Uid = Convert.ToString(i);
            }
        }

        private void NewOrder_Initialized(object sender, EventArgs e)
        {
            Button BNewOrder = (Button)sender;
            if (BNewOrder != null)
            {
                BNewOrder.Uid = Convert.ToString(i);
            }
        }

        private void Title_Initialized(object sender, EventArgs e)
        {
            if (i < ServicesList.Count)
            {
                TextBlock TBTitle = (TextBlock)sender;
                ModelData.Service ser = ServicesList[i];
                TBTitle.Text = ser.Title;
            }

        }
        private void Discount_Initialized(object sender, EventArgs e)
        {
            TextBlock TBDiscount = (TextBlock)sender;
            if (i< ServicesList.Count)
            {
                ModelData.Service ser = ServicesList[i];
                if (Convert.ToDouble(ser.Discount) != 0)
                {
                    
                    DiscountDouble = Convert.ToDouble(ser.Discount) * 100;
                    TBDiscount.Text = "* скидка " + DiscountDouble + " %";
                }
                else
                {
                    TBDiscount.Text = " ";
                }
            }
        }

        private void FalseCount_Initialized(object sender, EventArgs e)
        {
            TBFalseCount = (TextBlock)sender;
        }


        private void CountTime_Initialized(object sender, EventArgs e)
        {
            TextBlock TBCountTime = (TextBlock)sender;
            ModelData.Service ser = ServicesList[i];
            DiscountDouble = Convert.ToDouble(ser.Discount);
            if (i < ServicesList.Count)
            {
                if (DiscountDouble != 0)
                {
                    double PriceWithDiscount = Convert.ToDouble(ser.Cost) - (Convert.ToDouble(ser.Cost)* DiscountDouble);
                    TBFalseCount.Text = Convert.ToDouble(ser.Cost).ToString() + " ";
                    TBCountTime.Text = PriceWithDiscount + " рублей за " + ser.DurationInSeconds / 60 + " минут";
                }
                else
                {
                    TBCountTime.Text = Convert.ToDouble(ser.Cost) + " рублей за " + ser.DurationInSeconds / 60 + " минут";
                }
                
                
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Button BEdit = (Button)sender;
            int index = Convert.ToInt32(BEdit.Uid);
            ModelData.Service ser = ServicesList[index];
            MessageBox.Show("Редактировать данные у ячейки " + ser.Title);

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button BDelete = (Button)sender;
            int index = Convert.ToInt32(BDelete.Uid);
            ModelData.Service ser = ServicesList[index];
            MessageBox.Show("Удалить данные у ячейки " + ser.Title);
        }

        private void NewOrder_Click(object sender, RoutedEventArgs e)
        {
            Button BNewOrder = (Button)sender;
            int index = Convert.ToInt32(BNewOrder.Uid);
            ModelData.Service ser = ServicesList[index];
            MessageBox.Show("Новый заказ у ячейки " + ser.Title);
        }
    }
}
