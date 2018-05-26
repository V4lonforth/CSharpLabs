using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Lab1;
using Lab4;

namespace Lab5
{
    public partial class MainWindow : Window
    {
        private PathControllerViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            IPathControllerActions pathControllerActions = null;
            ConnectionWindow connectionWindow = new ConnectionWindow((IPathControllerActions actions) => { pathControllerActions = actions; });
            if (!connectionWindow.ShowDialog().Value)
                Close();
            else
            {
                viewModel = new PathControllerViewModel(pathControllerActions);
                DataContext = viewModel;
            }
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(viewModel.Check(int.Parse(firstBlockText.Text), int.Parse(lastBlockText.Text)));
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(viewModel.Save(savePathBlockText.Text));
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(viewModel.Load(loadPathBlockText.Text));
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Block block = (Block)((ComboBox)sender).DataContext;
            if (e.RemovedItems.Count > 0)
            {
                string error = viewModel.ChangeState(block.Index, (BlockState)block.State);
                if (!(error is null))
                {
                    MessageBox.Show(error);
                }
            }
        }

        private void CheckKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                CheckButton_Click(this, new RoutedEventArgs());
        }
        private void LoadKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                LoadButton_Click(this, new RoutedEventArgs());
        }
        private void SaveKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SaveButton_Click(this, new RoutedEventArgs());
        }
    }
}
