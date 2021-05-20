﻿using Microsoft.Win32;
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
using System.Windows.Forms;
using Button = System.Windows.Controls.Button;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using LanguageSchool.ModelData;
using System.Text.RegularExpressions;

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

        ModelData.Service EditObject;
        string NewPathImg;
        string CroopNewPathImg;

        int GlobalTime;
        List<Client> ClientList = Classes.DataClass.RE.Client.ToList();
        ModelData.Service ServiceObject;
        public AdminPage()
        {
            InitializeComponent();
            ServicesDG.ItemsSource = ServicesList;
            ComboBoxNameClient.ItemsSource = ClientList;
            ComboBoxNameClient.SelectedValuePath = "ID";
            ComboBoxNameClient.DisplayMemberPath = "FIO";

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
        private void PathImg_EditAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog DialogWindow = new OpenFileDialog();
            DialogWindow.ShowDialog();
            NewPathImg = DialogWindow.FileName;
            int IndexForDelit = NewPathImg.IndexOf('У');
            int LengthLine = NewPathImg.Length - 1;
            CroopNewPathImg = NewPathImg.Remove(0, IndexForDelit);
            PathImgObject.Text = CroopNewPathImg;

        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Button BEdit = (Button)sender;
            int index = Convert.ToInt32(BEdit.Uid);
            ModelData.Service ser = ServicesList[index];

            Pattern.Visibility = Visibility.Collapsed; 
            Menu.Visibility = Visibility.Collapsed;
            FormForEdit.Visibility = Visibility.Visible;

            EditObject = ServicesList[index];
            IDObject.Text = EditObject.ID.ToString();
            TitleObject.Text = EditObject.Title;
            CostObject.Text = EditObject.Cost.ToString();
            TimeObject.Text = (EditObject.DurationInSeconds/60).ToString();
            DiscriptionObject.Text = EditObject.Description;
            DiscountObject.Text = (EditObject.Discount*100).ToString();
            PathImgObject.Text = EditObject.MainImagePath;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button BDelete = (Button)sender;
            int index = Convert.ToInt32(BDelete.Uid);

            DialogResult ApprovalDelete = (DialogResult)MessageBox.Show("Вы действительно хотите удалить услугу?", "Внимание!", (MessageBoxButton)MessageBoxButtons.YesNo);
            if (ApprovalDelete == DialogResult.Yes)
            {
                ModelData.Service ser = ServicesList[index];
                Classes.DataClass.RE.Service.Remove(ser);
                MessageBox.Show("Запись удалена");
                Classes.DataClass.RE.SaveChanges();
                ClassFrame.VariableFrame.Navigate(new Pages.AdminPage());
            }
            else if (ApprovalDelete == DialogResult.No)
            {
                MessageBox.Show("Запись оставлена без изменений");
            }
        }

        private void NewOrder_Click(object sender, RoutedEventArgs e)
        {
            Button BNewOrder = (Button)sender;
            int index = Convert.ToInt32(BNewOrder.Uid);
            ModelData.Service ser = ServicesList[index];

            RecordClientServis.Visibility = Visibility.Visible;
            Pattern.Visibility = Visibility.Collapsed;
            Menu.Visibility = Visibility.Collapsed;

            OutTitle.Text = ser.Title;
            OutTime.Text = Convert.ToString(ser.DurationInSeconds / 60) + " минут";
            GlobalTime = ser.DurationInSeconds / 60;
            ServiceObject = ser;

        }

        private void LogoImg_Initialized(object sender, EventArgs e)
        {
            MediaElement image = (MediaElement)sender;
            Uri PathImg = new Uri("Resource / school_logo.png", UriKind.RelativeOrAbsolute);
            image.Source = PathImg;

            


        }
        
        private void EditSave_Click(object sender, RoutedEventArgs e)
        {
            Pattern.Visibility = Visibility.Visible;
            Menu.Visibility = Visibility.Visible;
            FormForEdit.Visibility = Visibility.Collapsed;

            EditObject.ID = Convert.ToInt32(IDObject.Text);
            EditObject.Title = TitleObject.Text;
            if(Convert.ToDecimal(CostObject.Text) <= 0)
            {
                MessageBox.Show("Введите корректные данные о стоимости услуги");
            }
            else
            {
                EditObject.Cost = Convert.ToDecimal(CostObject.Text);
            }

            int TimeConvert = Convert.ToInt32(TimeObject.Text)/60;
            if (TimeConvert < 0 || TimeConvert > 4 ) 
            {
                MessageBox.Show("Неправильно введено время или привышен лимит в 4 часа.");
            }
            else
            {
                EditObject.DurationInSeconds = Convert.ToInt32(TimeObject.Text) * 60;
            };

            EditObject.Description = DiscriptionObject.Text;
            double ConvertDiscount = Convert.ToDouble(DiscountObject.Text);
            if(ConvertDiscount < 0 || ConvertDiscount > 100)
            {
                MessageBox.Show("Скидка может быть в размере от 0 до 100 %");
            }
            else
            {
                EditObject.Discount = ConvertDiscount / 100;
            }
            if(CroopNewPathImg != null)
            {
                EditObject.MainImagePath = CroopNewPathImg;
                MessageBox.Show(CroopNewPathImg);
            }
            else
            {
                
                EditObject.MainImagePath = PathImgObject.Text;
            }

            Classes.DataClass.RE.SaveChanges();
            ClassFrame.VariableFrame.Navigate(new Pages.AdminPage());


        }

        private void NewService_Add_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.VariableFrame.Navigate(new Pages.NewServiceAdd());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Menu.Visibility = Visibility.Visible;
            Pattern.Visibility = Visibility.Visible;
            FormForEdit.Visibility = Visibility.Collapsed;
        }

        private void Record_Click(object sender, RoutedEventArgs e)
        {
            int IndexClient = ComboBoxNameClient.SelectedIndex + 1; // IDClient
            int IndexService = ServiceObject.ID;
            Regex Time1 = new Regex("[0-1][0-9]:[0-5][0-9]");
            Regex Time2 = new Regex("2[0-3]:[0-5][0-9]");
            TimeSpan TS;
            DateTime DT;

            if (ComboBoxNameClient.SelectedItem != null)
            {
                if ((Time1.IsMatch(TextBoxInputTime.Text) || Time2.IsMatch(TextBoxInputTime.Text)) && TextBoxInputTime.Text.Length == 5)
                {
                    TS = TimeSpan.Parse(TextBoxInputTime.Text);
                    DT = Convert.ToDateTime(DateLection.SelectedDate);
                    DT = DT.Add(TS);//Дата и время
                    if (DT > DateTime.Now)
                    {
                        Record.IsEnabled = true;
                        ClientService NewClientService = new ClientService() { ClientID = IndexClient, ServiceID = IndexService, StartTime = DT };
                        Classes.DataClass.RE.ClientService.Add(NewClientService);
                        Classes.DataClass.RE.SaveChanges();
                        MessageBox.Show("Клиент записан");
                    }
                    else
                    {
                        MessageBox.Show("Занятие уже прошло");
                    }
                }
                else
                {
                    MessageBox.Show("Неверно указано время");
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены");
            }  
         }

        private void Back_Menu_Click(object sender, RoutedEventArgs e)
        {
            RecordClientServis.Visibility = Visibility.Collapsed;
            Pattern.Visibility = Visibility.Visible;
            Menu.Visibility = Visibility.Visible;
        }
    }
}
