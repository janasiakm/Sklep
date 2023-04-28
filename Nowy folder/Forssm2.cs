using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PROJEKT_BAZA_2014
{
    public partial class Form2 : Form
    {
        
        public Form2()
        {
            InitializeComponent();
        }
        private MySqlConnection conn;
        private string id,nazwa;
        private int dataGridCheckBoxStatus;
        private void connStart()
        {
            string connection = "datasource=localhost;username=root;password=vertrigo;database=projekt;port=3306;Charset=cp1250";
            conn = new MySqlConnection(connection);
            conn.Open();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();
            connStart();
            string zapytanie = "SELECT `produkty`.`id`,`produkty`.`nr_katalogu`,`produkty`.`nazwa`,`produkty`.`rodzaj`,`produkty`.`cena`,`produkty`.`ilosc_sztuk` FROM `produkty`";
            MySqlCommand cmd = new MySqlCommand(zapytanie, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString());
            }
            reader.Close();
            conn.Close();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            connStart();
            string zapytanie = "UPDATE `projekt`.`produkty` SET `produkty`.`nazwa`='" + textBox2.Text + "',`produkty`.`nr_katalogu`='" + textBox1.Text + "',`produkty`.`rodzaj`='" + textBox3.Text + "',`produkty`.`cena`='" + textBox9.Text + "',`produkty`.`ilosc_sztuk`='" + textBox10.Text + "'WHERE `produkty`.`id`='" + id + "'";
            MySqlCommand cmd = new MySqlCommand(zapytanie, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            tabControl1.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connStart();
            
            string zapytanie = "DELETE FROM `projekt`.`produkty` WHERE `produkty`.`id`=" + id;
            MySqlCommand cmd = new MySqlCommand(zapytanie, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            textBox4.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            tabControl1.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            connStart();
            string zapytanie = "INSERT INTO `produkty` (nr_katalogu,nazwa,rodzaj,cena,ilosc_sztuk) VALUES ('" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox11.Text + "','" + textBox12.Text + "')";
            MySqlCommand cmd = new MySqlCommand(zapytanie, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            tabControl1.SelectedIndex = 0;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            connStart();
            string zapytanie = "SELECT `produkty`.`id`,`produkty`.`nr_katalogu`,`produkty`.`nazwa`,`produkty`.`rodzaj`,`produkty`.`cena`,`produkty`.`ilosc_sztuk` FROM `produkty` WHERE `produkty`.`nr_katalogu` LIKE '%" + textBox8.Text + "%'OR `produkty`.`nazwa` LIKE '%" + textBox8.Text + "%'OR `produkty`.`rodzaj` LIKE '%" + textBox8.Text + "%'";
            MySqlCommand cmd = new MySqlCommand(zapytanie, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dataGridView2.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5],1);
            }
            reader.Close();
            conn.Close();

        }

        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
           MessageBox.Show("Aplikacja została wykonana przez: \n Mateusz Janasiak \n Krystian Czekaj \n III SSI \n wersja programu 1.0", "INFORMACJE", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

     
        private void button6_Click(object sender, EventArgs e)
        {

            dataGridView3.Rows.Clear();
            connStart();
            string zapytanie = "SELECT `sprzedaz`.`numer`,`sprzedaz`.`imie`,`sprzedaz`.`nazwisko`,`sprzedaz`.`adres`,`sprzedaz`.`telefon`,`sprzedaz`.`zakupy`,`sprzedaz`.`suma` FROM `sprzedaz`";
            MySqlCommand cmd = new MySqlCommand(zapytanie, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dataGridView3.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());
            }
            reader.Close();
            conn.Close();
        }

        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            connStart();
            string zapytanie = "UPDATE `projekt`.`produkty` SET `produkty`.`zaznacz`='" + Convert.ToInt32(dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells[4].Value) + "'WHERE `produkty`.`id`='" + dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells[0].Value + "'";
            MySqlCommand cmd = new MySqlCommand(zapytanie, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.ColumnIndex == 9)
            {

                if (Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[5].Value) >= Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[6].Value))
                {

                    if(Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[6].Value)> 0)
                    {textBox17.Text = textBox17.Text + " " + dataGridView2[6, dataGridView2.CurrentRow.Index].Value.ToString() + " " + dataGridView2[2, dataGridView2.CurrentRow.Index].Value.ToString() + " " + dataGridView2[3, dataGridView2.CurrentRow.Index].Value.ToString() + ",\r\n" ;
                    textBox19.Text = (Convert.ToInt32(textBox19.Text) + (Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[4].Value)) * (Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[6].Value))).ToString(); 
   
                    connStart();
                    string zapytanie = "UPDATE `projekt`.`produkty` SET `produkty`.`ilosc_sztuk`='" + (Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[5].Value) - Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[6].Value)) + "'WHERE `produkty`.`id`='" + dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[0].Value + "'";
                    MySqlCommand cmd = new MySqlCommand(zapytanie, conn);
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

        private void button7_Click(object sender, EventArgs e)
        {
            connStart();
            string zapytanie = "INSERT INTO `sprzedaz` (imie,nazwisko,adres,telefon,zakupy,suma) VALUES ('" + textBox13.Text + "','" + textBox14.Text + "','" + textBox15.Text + "','" + textBox16.Text + "','" + textBox17.Text + "','" + textBox19.Text + "')";
            MySqlCommand cmd = new MySqlCommand(zapytanie, conn);
            cmd.ExecuteNonQuery();
            textBox19.Text = (Convert.ToInt32(textBox19.Text) - Convert.ToInt32(textBox19.Text)).ToString(); 
   
            conn.Close();

            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";
            tabControl1.SelectedIndex = 5;
           

        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
            connStart();
            string zapytanie = "SELECT `sprzedaz`.`numer`,`sprzedaz`.`imie`,`sprzedaz`.`nazwisko`,`sprzedaz`.`adres`,`sprzedaz`.`telefon`,`sprzedaz`.`zakupy`,`sprzedaz`.`suma`FROM `sprzedaz` WHERE `sprzedaz`.`imie` LIKE '%" + textBox18.Text + "%'OR `sprzedaz`.`nazwisko` LIKE '%" + textBox18.Text + "%'OR `sprzedaz`.`adres` LIKE '%" + textBox18.Text + "%'";
            MySqlCommand cmd = new MySqlCommand(zapytanie, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dataGridView3.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6]);
            }
            reader.Close();
            conn.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach ( DataGridViewRow row in dataGridView1.Rows)
            {
                comboBox1.Items.Add(row.Cells[1].Value.ToString());
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            connStart();
            string zapytanie = "UPDATE `projekt`.`produkty` SET `produkty`.`ilosc_sztuk`='" + (Convert.ToInt32(textBox20.Text)).ToString() +" ' + `produkty`.`ilosc_sztuk`  WHERE `produkty`.`nr_katalogu`='" + comboBox1.SelectedItem.ToString() + "'";
            MySqlCommand cmd = new MySqlCommand(zapytanie, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView4.Rows.Clear();
            connStart();
            string zapytanie = "SELECT `produkty`.`id`,`produkty`.`nazwa`,`produkty`.`rodzaj`,`produkty`.`ilosc_sztuk` FROM `produkty` WHERE `produkty`.`nr_katalogu`='" + comboBox1.SelectedItem.ToString() + "'";
            MySqlCommand cmd = new MySqlCommand(zapytanie, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dataGridView4.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
            }
            reader.Close();
            conn.Close();
        }
    }
}
