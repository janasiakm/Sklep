using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace PROJEKT_BAZA_2014
{
    public partial class Form2 : Form
    {
        private SqlConnection Conn;
        public Form2(SqlConnection conn)
        {
            Conn = conn;
            InitializeComponent();
        }
        
        private string id;
        private readonly int DataGridCheckBoxStatus;

       

        private void ConnStart()
        {   try
            {
                Conn.Open();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "BŁĄD POŁĄCZNIA DO BAZY");
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();
            ConnStart();
            string query = "SELECT produktid,nazwa,producent,cenaNetto,stanMagazynowy FROM Produkty";
            SqlCommand cmd = new SqlCommand(query, Conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3], reader[4]);
            }
            reader.Close();
            Conn.Close();
        }

        private void DataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            tabControl1.SelectedIndex = 1;

            textBox4.Text = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
          
            id = textBox4.Text;
            textBox1.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            textBox2.Text = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            textBox3.Text = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            textBox9.Text = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
            textBox10.Text = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ConnStart();
            string query = "UPDATE `projekt`.`produkty` SET `produkty`.`nazwa`='" + textBox2.Text + "',`produkty`.`nr_katalogu`='" + textBox1.Text + "',`produkty`.`rodzaj`='" + textBox3.Text + "',`produkty`.`cena`='" + textBox9.Text + "',`produkty`.`ilosc_sztuk`='" + textBox10.Text + "'WHERE `produkty`.`id`='" + id + "'";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.ExecuteNonQuery();
            Conn.Close();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            tabControl1.SelectedIndex = 0;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            ConnStart();
            
            string query = "DELETE FROM `projekt`.`produkty` WHERE `produkty`.`id`=" + id;
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.ExecuteNonQuery();
            Conn.Close();
            textBox4.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            tabControl1.SelectedIndex = 0;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            ConnStart();
            string query = "INSERT INTO `produkty` (nr_katalogu,nazwa,rodzaj,cena,ilosc_sztuk) VALUES ('" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox11.Text + "','" + textBox12.Text + "')";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.ExecuteNonQuery();
            Conn.Close();
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            tabControl1.SelectedIndex = 0;

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            ConnStart();
            string query = "SELECT `produkty`.`id`,`produkty`.`nr_katalogu`,`produkty`.`nazwa`,`produkty`.`rodzaj`,`produkty`.`cena`,`produkty`.`ilosc_sztuk` FROM `produkty` WHERE `produkty`.`nr_katalogu` LIKE '%" + textBox8.Text + "%'OR `produkty`.`nazwa` LIKE '%" + textBox8.Text + "%'OR `produkty`.`rodzaj` LIKE '%" + textBox8.Text + "%'";
            SqlCommand cmd = new SqlCommand(query, Conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dataGridView2.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5],1);
            }
            reader.Close();
            Conn.Close();

        }

        private void OProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
           MessageBox.Show("Aplikacja została wykonana przez: \n Mateusza Janasiak ", "INFORMACJE", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

     
        private void Button6_Click(object sender, EventArgs e)
        {

            dataGridView3.Rows.Clear();
            ConnStart();
            string query = "SELECT `sprzedaz`.`numer`,`sprzedaz`.`imie`,`sprzedaz`.`nazwisko`,`sprzedaz`.`adres`,`sprzedaz`.`telefon`,`sprzedaz`.`zakupy`,`sprzedaz`.`suma` FROM `sprzedaz`";
            SqlCommand cmd = new SqlCommand(query, Conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dataGridView3.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());
            }
            reader.Close();
            Conn.Close();
        }

        private void DataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ConnStart();
            string query = "UPDATE `projekt`.`produkty` SET `produkty`.`zaznacz`='" + Convert.ToInt32(dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells[4].Value) + "'WHERE `produkty`.`id`='" + dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells[0].Value + "'";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.ExecuteNonQuery();
            Conn.Close();
        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.ColumnIndex == 9)
            {

                if (Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[5].Value) >= Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[6].Value))
                {

                    if(Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[6].Value)> 0)
                    {textBox17.Text = textBox17.Text + " " + dataGridView2[6, dataGridView2.CurrentRow.Index].Value.ToString() + " " + dataGridView2[2, dataGridView2.CurrentRow.Index].Value.ToString() + " " + dataGridView2[3, dataGridView2.CurrentRow.Index].Value.ToString() + ",\r\n" ;
                    textBox19.Text = (Convert.ToInt32(textBox19.Text) + (Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[4].Value)) * (Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[6].Value))).ToString(); 
   
                    ConnStart();
                    string query = "UPDATE `projekt`.`produkty` SET `produkty`.`ilosc_sztuk`='" + (Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[5].Value) - Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[6].Value)) + "'WHERE `produkty`.`id`='" + dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[0].Value + "'";
                    SqlCommand cmd = new SqlCommand(query, Conn);
                    cmd.ExecuteNonQuery();
                }}
                else { MessageBox.Show("Zbyt mało sztuk na magazynie!!!"); }
        }
            
            
            if (e.ColumnIndex == 7)
                dataGridView2.Rows[e.RowIndex].Cells[6].Value = (int)dataGridView2.Rows[e.RowIndex].Cells[6].Value + 1; 

            if (e.ColumnIndex == 8)
                if ((int)dataGridView2.Rows[e.RowIndex].Cells[6].Value == 0)
                    MessageBox.Show("Zaznaczasz zbyt malo sztuk!!!");
                else { 
                dataGridView2.Rows[e.RowIndex].Cells[6].Value = (int)dataGridView2.Rows[e.RowIndex].Cells[6].Value - 1;}
            
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            ConnStart();
            string query = "INSERT INTO `sprzedaz` (imie,nazwisko,adres,telefon,zakupy,suma) VALUES ('" + textBox13.Text + "','" + textBox14.Text + "','" + textBox15.Text + "','" + textBox16.Text + "','" + textBox17.Text + "','" + textBox19.Text + "')";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.ExecuteNonQuery();
            textBox19.Text = (Convert.ToInt32(textBox19.Text) - Convert.ToInt32(textBox19.Text)).ToString(); 
   
            Conn.Close();

            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";
            tabControl1.SelectedIndex = 5;
           

        }

        private void Button8_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
            ConnStart();
            string query = "SELECT `sprzedaz`.`numer`,`sprzedaz`.`imie`,`sprzedaz`.`nazwisko`,`sprzedaz`.`adres`,`sprzedaz`.`telefon`,`sprzedaz`.`zakupy`,`sprzedaz`.`suma`FROM `sprzedaz` WHERE `sprzedaz`.`imie` LIKE '%" + textBox18.Text + "%'OR `sprzedaz`.`nazwisko` LIKE '%" + textBox18.Text + "%'OR `sprzedaz`.`adres` LIKE '%" + textBox18.Text + "%'";
            SqlCommand cmd = new SqlCommand(query, Conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dataGridView3.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6]);
            }
            reader.Close();
            Conn.Close();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TextBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void TabPage6_Click(object sender, EventArgs e)
        {

        }

        private void Button9_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach ( DataGridViewRow row in dataGridView1.Rows)
            {
                comboBox1.Items.Add(row.Cells[1].Value.ToString());
            }
        }

        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button10_Click(object sender, EventArgs e)
        {
            ConnStart();
            string query = "UPDATE `projekt`.`produkty` SET `produkty`.`ilosc_sztuk`='" + (Convert.ToInt32(textBox20.Text)).ToString() +" ' + `produkty`.`ilosc_sztuk`  WHERE `produkty`.`nr_katalogu`='" + comboBox1.SelectedItem.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.ExecuteNonQuery();
            Conn.Close();
        }

        private void DataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button11_Click(object sender, EventArgs e)
        {
            dataGridView4.Rows.Clear();
            ConnStart();
            string query = "SELECT `produkty`.`id`,`produkty`.`nazwa`,`produkty`.`rodzaj`,`produkty`.`ilosc_sztuk` FROM `produkty` WHERE `produkty`.`nr_katalogu`='" + comboBox1.SelectedItem.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dataGridView4.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
            }
            reader.Close();
            Conn.Close();
        }
    }
}
