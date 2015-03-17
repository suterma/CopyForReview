using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CopyForReview.Data;
using CopyForReview.Formatters;
using Microsoft.VisualStudio.PlatformUI;

namespace CopyForReview
{
    // Use this constructor to enable F1 Help. 
    public partial class MyModalDialog : DialogWindow
    {
        private ISnippet _snippet;
        // Use this constructor to provide a Help button and F1 support. 
        public MyModalDialog(string helpTopic)
            : base(helpTopic)
        {
            InitializeComponent();
            InitializeFormatterButtons();

        }

        /// <summary>
        /// Initializes the formatter buttons.
        /// </summary>
        private void InitializeFormatterButtons()
        {
            //Initialize Formatter Buttons
            var formatters = Factory.GetFormatters();
            foreach (var formatter in formatters)
            {
                var button = new FormatterImageButton(formatter);
                button.Click += ButtonReviewInFoswiki_Click;

                StackPanelFormatters.Children.Add(button);

                //Button btn = new Button();
                //btn.MinWidth = 300;
                //btn.MinHeight = 300;
                //btn.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                //btn.VerticalContentAlignment = VerticalAlignment.Stretch;
                //btn.Click += ButtonReviewInFoswiki_Click;
                //btn.Content = formatter.Name;
                //btn.ToolTip = formatter.Description;
                ////btn.Name = formatter.ToString(); //use the class name TODO later change and probably use some sort of GUID here
                //StackPanelFormatters.Children.Add(btn);
            }

        }

        // Use this constructor for minimize and maximize buttons and no F1 Help. 
        public MyModalDialog(ISnippet snippet)
        {
            this.HasMaximizeButton = true;
            this.HasMinimizeButton = true;
            InitializeComponent();
            InitializeFormatterButtons();

            _snippet = snippet;
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            var v = new MyModalDialog("Microsoft.VisualStudio.PlatformUI.DialogWindow");
            v.Content = "Here you go.";
            v.ShowModal();
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Okay.");
            this.Close();
        }

        private void ButtonReviewInFoswiki_Click(object sender, RoutedEventArgs e)
        {
            //find the formatter in question and invoke it
            var button = sender as FormatterImageButton;
            var formatter = Factory.GetFormatters().Single(item => item.Name == button.FormatterName); //TODO later use better matching, possibly using a GUID
            Clipboard.SetText(formatter.Format(_snippet));
            this.Close();
        }
    }
}