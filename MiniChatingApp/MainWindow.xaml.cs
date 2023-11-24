using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;

namespace MiniChatingApp
{

    public class MessageModel
    {
        public int Id { get; set; }
        public int RightOrLeft { get; set; }
        public string Message { get; set; }
    }
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;



        public static readonly DependencyProperty MessageOneProperty =
            DependencyProperty.Register("messageOne", typeof(ObservableCollection<Message>), typeof(MainWindow), new PropertyMetadata(null));

        public ObservableCollection<Message> messageOne
        {
            get { return (ObservableCollection<Message>)GetValue(MessageOneProperty); }
            set { SetValue(MessageOneProperty, value); }
        }

        //-----------------------------

        public static readonly DependencyProperty MessageOneProperty2 =
            DependencyProperty.Register("messageTwo", typeof(ObservableCollection<Message>), typeof(MainWindow), new PropertyMetadata(null));

        public ObservableCollection<Message> messageTwo
        {
            get { return (ObservableCollection<Message>)GetValue(MessageOneProperty2); }
            set { SetValue(MessageOneProperty2, value); }
        }
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTickAsync;
            timer.Start();
            DataContext = this;
        }
        private async void TimerTickAsync(object sender, EventArgs e)
        {
            var dbcontext = new UsersContext();
            messageOne = new(await dbcontext.Messages.Where(m => m.RightOrLeft == 1).ToListAsync());
            messageTwo = new(await dbcontext.Messages.Where(m => m.RightOrLeft == 2).ToListAsync());
        }





        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var dbcontext = new UsersContext();
            await dbcontext.Messages.AddAsync(new Message()
            {
                RightOrLeft = 1,
                Message1 = textbox1.Text
            });
            await dbcontext.SaveChangesAsync();
        }

        private async void Button_Click2(object sender, RoutedEventArgs e)
        {
            var dbcontext = new UsersContext();
            await dbcontext.Messages.AddAsync(new Message()
            {
                RightOrLeft = 2,
                Message1 = textbox2.Text
            });
            await dbcontext.SaveChangesAsync();

        }
    }
}


