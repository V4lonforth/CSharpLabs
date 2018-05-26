using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Lab1;
using Lab4;

namespace Lab5
{
    public partial class ConnectionWindow : Window
    {
        private const string providePathButtonText = "Provide path";
        private const string provideCreateButtonText = "Provide block's count";

        private Action<IPathControllerActions> setPathControllerActions;

        public ConnectionWindow(Action<IPathControllerActions> setActions)
        {
            InitializeComponent();
            setPathControllerActions = setActions;
        }

        private void ChangeText(TextBox textBox, string str, bool state)
        {
            if (state)
            {
                if (textBox.Text.CompareTo(str) == 0)
                    textBox.Text = String.Empty;
            }
            else
            {
                if (textBox.Text.CompareTo(String.Empty) == 0)
                    textBox.Text = str;
            }
            textBox.IsEnabled = state;
        }

        private void LoadButton_Checked(object sender, RoutedEventArgs e)
        {
            ChangeText(loadPathTextBox, providePathButtonText, true);
            loadPathTextBox.Focus();
        }
        private void LoadButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeText(loadPathTextBox, providePathButtonText, false);
        }

        private void CreateButton_Checked(object sender, RoutedEventArgs e)
        {
            ChangeText(createPathTextBox, provideCreateButtonText, true);
            createPathTextBox.Focus();
        }
        private void CreateButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeText(createPathTextBox, provideCreateButtonText, false);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ConnectButton_Click(this, null);
            else if (e.Key == Key.Escape)
                DialogResult = false;
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChannelFactory<IConnection> connectFactory = new ChannelFactory<IConnection>(new NetNamedPipeBinding(), new EndpointAddress(addressText.Text + "/connection"));
                IConnection connect = connectFactory.CreateChannel();
                int id = connect.Connect();

                ChannelFactory<IPathControllerActions> factory = new ChannelFactory<IPathControllerActions>(new NetNamedPipeBinding(), new EndpointAddress(addressText.Text + "/pathController/" + id.ToString()));
                IPathControllerActions actions = factory.CreateChannel();

                actions.CheckConnection();
                setPathControllerActions(actions);

                if (loadButton.IsChecked.Value)
                {
                   string error = actions.Load(loadPathTextBox.Text);
                    if (!(error is null))
                    {
                        MessageBox.Show(error);
                        return;
                    }
                }
                else
                {
                    actions.CreateBlocks(new BlockState[int.Parse(createPathTextBox.Text)]);
                }
                    
                DialogResult = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }
    }
}
