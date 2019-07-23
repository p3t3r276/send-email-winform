using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace EmailExample
{
    public partial class Form1 : Form
    {
        private SmtpClient smtpClient = null;
        private MailAddress fromAddress = null; 
        const string fromPassword = "password"; // TODO: Thêm pass của mính

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fromAddress = new MailAddress("email gửi", "Tên người gửi"); // TODO: Thêm email của mình
            smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com", // Mặc định của gmail
                Port = 587, // Mặc định của gmail
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false, // Nếu để true sẽ gửi thông tin đăng nhập của máy tính, ở đây mình dùng thông tin dăng nhập gmail
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
        }

        private void btnGui_Click(object sender, EventArgs e)
        {
            try
            {
                var toAddress = new MailAddress(txtReceiver.Text, txtReceptName.Text != String.Empty ? txtReceptName.Text : "Tên mặc định");
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = txtSubject.Text,
                    Body = txtContent.Text,
                    IsBodyHtml = true // Cho phép gửi HTML
                })
                {
                    smtpClient.Send(message);
                }
                MessageBox.Show("Thư của bạn đã gửi thành công đến " + toAddress.Address, "Gửi thư thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Xảy ra lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }     
        }
    }
}
