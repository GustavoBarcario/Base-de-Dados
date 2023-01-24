using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseDados
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            string strConnection = "server=127.0.0.1;User Id=root";
            //string strConnection2 = "server=127.0.0.1;User Id=root;database=curs_db;password=logan";

            MySqlConnection conexao = new MySqlConnection(strConnection);
            conexao.ConnectionString = strConnection;

            try
            {
                conexao.Open();
                lblResultado.Text = "Conectado MySQL";
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Erro ao Conectar MySQL \n" + ex;
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnCriarTabela_Click(object sender, EventArgs e)
        {
            string strConnection = "server=127.0.0.1;User Id=root;database=cadastro_db";
            MySqlConnection conexao = new MySqlConnection(strConnection);

            try
            {
                conexao.Open();

                MySqlCommand comando = new MySqlCommand();
                comando.Connection = conexao;

                comando.CommandText = "CREATE TABLE pessoas ( id INT NOT NULL, nome VARCHAR(50), email VARCHAR(50), PRIMARY KEY(id))";
                comando.ExecuteNonQuery();

                lblResultado.Text = "Tabela Criada MySQL";
                comando.Dispose();
            }
            catch (Exception ex)
            {
                lblResultado.Text = ex.Message;
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            string strConnection = "server=127.0.0.1;User Id=root;database=cadastro_db";
            MySqlConnection conexao = new MySqlConnection(strConnection);

            try
            {
                conexao.Open();

                MySqlCommand comando = new MySqlCommand();
                comando.Connection = conexao;

                int id = new Random(DateTime.Now.Millisecond).Next(0, 1000);
                string nome = txtNome.Text;
                string email = txtEmail.Text;

                comando.CommandText = "INSERT INTO pessoas VALUES (" + id + ", '" + nome + "', '" + email + "')";

                comando.ExecuteNonQuery();

                lblResultado.Text = "Registro inserido MySql";
                comando.Dispose();
            }
            catch (Exception ex)
            {
                lblResultado.Text = ex.Message;
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnProcurar_Click(object sender, EventArgs e)

        {
            lblResultado.Text = "";
            lista.Rows.Clear();

            string strConection = "server=127.0.0.1;User Id=root;database=cadastro_db";

            MySqlConnection conexao = new MySqlConnection(strConection);

            try
            {
                string query = "SELECT * FROM pessoas";

                if (txtNome.Text != "")
                {
                    query = "SELECT * FROM pessoas WHERE nome LIKE '" + txtNome.Text + "'";
                }

                DataTable dados = new DataTable();

                MySqlDataAdapter adaptador = new MySqlDataAdapter(query, strConection);

                conexao.Open();

                adaptador.Fill(dados);

                foreach (DataRow linha in dados.Rows)
                {
                    lista.Rows.Add(linha.ItemArray);
                }
            }
            catch (Exception ex)
            {
                lista.Rows.Clear();
                lblResultado.Text = ex.Message;
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}
