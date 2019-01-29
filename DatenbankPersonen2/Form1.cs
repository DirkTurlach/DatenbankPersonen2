using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DatenbankPersonen2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Adressen;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; cnn = new SqlConnection(connetionString);
            cnn.Open();
            MessageBox.Show("Connection Open  !");
            
            //Daten lesen
            SqlCommand command;
            SqlDataReader dataReader;
            String sql, Output = "";

            sql = "SELECT vorname, nachname FROM personen Where PersonenID < 5";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + "\n";
            }

            MessageBox.Show(Output);

            dataReader.Close();

            // Daten schreiben
            SqlDataAdapter adapter = new SqlDataAdapter();

            sql = "INSERT into personen (anrede, vorname, nachname, strasse, plz, ort, land) values('Herr' , 'SqlEingefuegtVor', 'SqlEingefuegtNach', 'Str' , 66666, 'oertchen', 'S')";

            adapter.InsertCommand = new SqlCommand(sql, cnn);
            adapter.InsertCommand.ExecuteNonQuery();


            // Daten veraender
            sql = "UPDATE personen SET vorname = 'SQLgeaender' WHERE PersonenId=6993";

            adapter.UpdateCommand = new SqlCommand(sql, cnn);
            adapter.UpdateCommand.ExecuteNonQuery();

            ////Daten loeschen
            //sql = "DELETE FROM personen WHERE PersonenID > 1000";

            //adapter.DeleteCommand = new SqlCommand(sql, cnn);
            //adapter.DeleteCommand.ExecuteNonQuery();


            command.Dispose();
            cnn.Close();
        }

        private void btnSchliessen_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
