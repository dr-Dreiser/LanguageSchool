using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace LanguageSchool.Pages
{
    /// <summary>
    /// Логика взаимодействия для NewServiceAdd.xaml
    /// </summary>
    public partial class NewServiceAdd : Page
    {
        string ImgPath;


        public NewServiceAdd()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.VariableFrame.Navigate(new Pages.AdminPage());
        }

        private void DownloadImgNewService_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog DialogWin = new OpenFileDialog();
            DialogWin.ShowDialog();
            string FullPath = DialogWin.FileName;
            int IndexDelite = FullPath.IndexOf('У');
            int Lenght = FullPath.Length - 1;
            ImgPath = FullPath.Remove(0, IndexDelite);
        }

        private void SaveNewService_Click(object sender, RoutedEventArgs e)
        {
            string InputTitle;
            string InputDiscription;
            decimal ConvertCost;
            int ConvertTime;
            double ConvertDiscount;
            string InputPathImg;

            ModelData.Service obj = Classes.DataClass.RE.Service.FirstOrDefault(z => z.Title == TitleNewService.Text);

            if (TitleNewService.Text == null || CostNewService.Text == null || TimeNewService.Text == null || DiscountNewService.Text == null || ImgPath== null)
            {
                MessageBox.Show("Не все обязательные поля заполнены!");
            }
            if (obj != null)
            {
                MessageBox.Show("Введенно название уже существующей услуги");
            }
            else
            {
                InputTitle = TitleNewService.Text;
                if (Convert.ToDecimal(CostNewService.Text) < 0)
                {
                    MessageBox.Show("Введение отрицательного значения для стоимости услуги недопустимо!");
                }
                else
                {
                    ConvertCost = Convert.ToDecimal(CostNewService.Text);
                    if (Convert.ToInt32(TimeNewService.Text) / 60 > 4)
                    {
                        MessageBox.Show("Превышен лимит времени для проведения занятия. Максимальное значение - 4 часа (240 минут)");
                    }
                    else
                    {
                        ConvertTime = Convert.ToInt32(TimeNewService.Text) * 60;
                        if (Convert.ToDouble(DiscountNewService.Text) > 100 || Convert.ToDouble(DiscountNewService.Text) < 0)
                        {
                            MessageBox.Show("Размер скидки указан неверно. Разрешенный диапозон от 0 до 100");
                        }
                        else
                        {
                            ConvertDiscount = Convert.ToDouble(DiscountNewService.Text) / 100;
                            if (ImgPath == null)
                            {
                                MessageBox.Show("Загрузите изображение");
                            }
                            else
                            {
                                InputPathImg = ImgPath;
                                ModelData.Service NewSer = new ModelData.Service()
                                {
                                    Title = InputTitle,
                                    Cost = ConvertCost,
                                    DurationInSeconds = ConvertTime,
                                    Description = DescriptionNewService.Text,
                                    Discount = ConvertDiscount,
                                    MainImagePath = InputPathImg,
                                };
                            }
                        }
                    }
                }
            }
            
            
           
            
        }
    }
}
