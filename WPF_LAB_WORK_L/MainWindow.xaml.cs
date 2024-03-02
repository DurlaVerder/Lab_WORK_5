using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;


namespace WPF_LAB_WORK_L
{
    
    public partial class MainWindow : Window
    {
        private SaveFileDialog saveFileDialog;
        public MainWindow()
        {
            InitializeComponent();
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            fontFamilyComboBox.Visibility = Visibility.Hidden;
            fontSizeComboBox.Visibility = Visibility.Hidden;
        }
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string text = File.ReadAllText(openFileDialog.FileName);
                    textBox.Text = text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при открытии файла: " + ex.Message);
                }
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                MessageBox.Show("Нельзя сохранить пустой файл.");
                return;
            }

            if (string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                SaveFileAs_Click(sender, e);
                return;
            }

            try
            {
                File.WriteAllText(saveFileDialog.FileName, textBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении файла: " + ex.Message);
            }
        }

        private void SaveFileAs_Click(object sender, RoutedEventArgs e)
        {
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, textBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении файла: " + ex.Message);
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = fontSizeComboBox.SelectedItem as ComboBoxItem;

            double fontSize = double.Parse(selectedItem.Content.ToString());
            textBox.FontSize = fontSize;

        }

        private void FontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = fontFamilyComboBox.SelectedItem as ComboBoxItem;

            string fontFamily = selectedItem.Content.ToString();
            textBox.FontFamily = new System.Windows.Media.FontFamily(fontFamily);

        }

        private void ChangeFont_Click(object sender, RoutedEventArgs e)
        {

            if (fontFamilyComboBox.Visibility == Visibility.Hidden)
            {
                fontFamilyComboBox.Visibility = Visibility.Visible;
                fontSizeComboBox.Visibility = Visibility.Visible;
            }
            else
            {
                fontFamilyComboBox.Visibility = Visibility.Hidden;
                fontSizeComboBox.Visibility = Visibility.Hidden;
            }
        }

    }
}
