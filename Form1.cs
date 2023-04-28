using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;


namespace PROJEKT_BAZA_2014
{
    public partial class Form1 : Form
    {
        public string login = string.Empty;
        public string password = string.Empty;

        SqlConnection Conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=Shop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            login = textBox1.Text;

            ConnStart();
            //string query = "SELECT top 1 haslo FROM uzytkownicy where login='" + login + "'";
            //SqlCommand cmd = new SqlCommand(query, Conn);
            //SqlDataReader result = cmd.ExecuteReader();
            //while (result.Read())
            //{
            //    password = result[0].ToString();
            //}

            //if (textBox2.Text == password)
            //{
                
                Form1.ActiveForm.Visible = false;
                Form2 apikacja2 = new Form2(Conn);
                apikacja2.Show();
            //}
            //else
            //{
            //    MessageBox.Show("Błedny login lub hasło", "Logowanie", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            //    textBox1.Clear();
            //    textBox2.Clear();
            //}

            //result.Close();
            Conn.Close();

        }
        private void ConnStart()
        {
            try
            {
                Conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BŁĄD POŁĄCZNIA DO BAZY");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }


}


