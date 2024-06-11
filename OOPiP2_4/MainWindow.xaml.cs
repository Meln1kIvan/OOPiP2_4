using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfTextEditor
{
    public partial class MainWindow : Window
    {
        private string currentFilePath ="";
        private Button openButton;
        private Button saveButton;
        public MainWindow()
        {
            InitializeComponent();
            CreateDynamicInterface();
            AddEventHandlers();
        }

        private void AddEventHandlers()
        {
            // Event handler for Ctrl+T key combination
            textBox.PreviewKeyDown += (sender, e) =>
            {
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    if (e.Key == Key.T)
                    {
                        string selectedText = textBox.SelectedText;
                        string indentedText = IndentText(selectedText, 4);
                        textBox.SelectedText = indentedText;
                        e.Handled = true;
                    }
                }
            };
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            // Логика открытия файла
            OpenFile();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Логика сохранения файла
            SaveFile();
        }
        private void OpenFile()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;

            if (openFileDialog.ShowDialog() == true)
            {
                currentFilePath = openFileDialog.FileName;
                MessageBox.Show($"Выбран файл: {currentFilePath}");
            }
        }
        public void SaveFile() 
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                MessageBox.Show("Error: No file loaded");
                return;
            }

            try
            {
                File.WriteAllText(currentFilePath, textBox.Text);

                MessageBox.Show("File successfully saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while saving file: {ex.Message}");
            }
        }
        private void CreateDynamicInterface()
        {
            // Create a new Grid
            var grid = new Grid();

            // Create RowDefinitions
            var rowDefinition1 = new RowDefinition();
            var rowDefinition2 = new RowDefinition();
            rowDefinition2.Height = new GridLength(1, GridUnitType.Star);

            // Add RowDefinitions to the Grid
            grid.RowDefinitions.Add(rowDefinition1);
            grid.RowDefinitions.Add(rowDefinition2);

            // Create a StackPanel for the buttons and center it horizontally
            var buttonStackPanel = new StackPanel();
            buttonStackPanel.Orientation = Orientation.Horizontal;
            buttonStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            buttonStackPanel.Margin = new Thickness(10);

            // Create "Open" button as a small rectangle
            openButton = new Button();
            openButton.Content = "Open";
            openButton.Width = 60;
            openButton.Height = 30;
            openButton.Click += OpenButton_Click;
            buttonStackPanel.Children.Add(openButton);

            // Create "Save" button as a small rectangle
            saveButton = new Button();
            saveButton.Content = "Save";
            saveButton.Width = 60;
            saveButton.Height = 30;
            saveButton.Click += SaveButton_Click;
            buttonStackPanel.Children.Add(saveButton);

            // Set the StackPanel in the first row of the Grid
            Grid.SetRow(buttonStackPanel, 0);
            grid.Children.Add(buttonStackPanel);

            // Create a TextBox for the text editor
            textBox = new TextBox();
            textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            textBox.AcceptsReturn = true;
            textBox.AcceptsTab = true;

            // Set the TextBox in the second row of the Grid
            Grid.SetRow(textBox, 1);
            grid.Children.Add(textBox);

            // Set the Grid as the content of the window
            Content = grid;
        }
        public string IndentText(string text, int spaces)
        {
            // Логика смещения текста на указанное количество пробелов
            // (в данном примере просто добавляем указанное количество пробелов в начало каждой строки)
            string[] lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = new string(' ', spaces) + lines[i];
            }
            return string.Join(Environment.NewLine, lines);
        }
    }
}