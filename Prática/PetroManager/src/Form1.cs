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


namespace PetroManager
{
    public partial class Form1 : Form
    {
        private SqlConnection cn;
        private int curPosto;
        private int curFunc;
        private bool adding;
        private int curCliente;
        private int curT;
            public Form1()
            {
            InitializeComponent();
            getSGBDConnection();
            verifySGBDConnection();
            LoadFuncListBox2();
            LoadPostosListBox1();
            LoadClienteListBox3();
            LoadTransacoesListBox4();
            LoadPrecariosListBox();
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            listBox2.SelectedIndexChanged += listBox2_SelectedIndexChanged;
            listBox3.SelectedIndexChanged += listBox3_SelectedIndexChanged;
            listBox4.SelectedIndexChanged += listBox4_SelectedIndexChanged;
            listBox5.SelectedIndexChanged += listBox5_SelectedIndexChanged;
            ShowButtons();
            ShowButtonsFunc();
            ShowButtonsCliente();
            ShowButtonsTransacao();
            initializeFiltro();
            initializeFiltroC();
            }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void initializeFiltroC()
        {
            Tfilterc.Items.Clear();
            Tfilterf.Items.Clear();

            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT Cliente_Nome FROM PM.TransacaoDetalhada", cn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string clienteNome = reader["Cliente_Nome"].ToString();
                        Tfilterc.Items.Add(clienteNome);
                    }
                    reader.Close();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT Funcionario_Nome FROM PM.TransacaoDetalhada", cn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string funcionarioNome = reader["Funcionario_Nome"].ToString();
                        Tfilterf.Items.Add(funcionarioNome);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void initializeFiltro()
        {
            FilterCity.Items.Clear();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM PM.CityID", cn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {


                    while (reader.Read())
                    {
                        string cidade = reader["Cidade"].ToString();
                        FilterCity.Items.Add(cidade);
                        
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading funcionarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private SqlConnection getSGBDConnection()
        {
            return new SqlConnection("data source= tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog = p3g1; uid = p3g1; password = Diogos24");
        }

        private bool verifySGBDConnection()
        {
            if (cn == null)
                cn = getSGBDConnection();

            if (cn.State != ConnectionState.Open)
                cn.Open();


            return cn.State == ConnectionState.Open;
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                curPosto = listBox1.SelectedIndex;
                ShowPosto();
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex >= 0)
            {
                curFunc = listBox2.SelectedIndex;
                ShowFunc();
            }
        }
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex >= 0)
            {
                curCliente = listBox3.SelectedIndex;
                ShowCliente();
            }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox4.SelectedIndex >= 0)
            {
                curT = listBox4.SelectedIndex;
                ShowTransacao();
            }
        }
        private void LoadPostosListBox1()
        {
            if (!verifySGBDConnection())
                return;

            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM PM.managerList", cn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    listBox1.Items.Clear();

                    while (reader.Read())
                    {
                        Posto P = new Posto
                        {
                            ID = reader["PostoID"].ToString(),
                            Cidade = reader["Cidade"].ToString(),
                            Contacto = reader["PostoContacto"].ToString(),
                            HoraAbertura = reader["Abertura"].ToString(),
                            HoraFecho = reader["Fecho"].ToString(),
                            MgrNome = reader["Nome"].ToString(),
                            MgrEmail = reader["Email"].ToString(),
                            MgrContacto = reader["Contacto"].ToString(),
                            MgrID = reader["ID"].ToString() 
                        };
                        listBox1.Items.Add(P);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading postos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadFuncListBox2()
        {   
            
            listBox2.Items.Clear();
            if (!verifySGBDConnection())
                return;

            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM PM.FuncDisplay", cn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    

                    while (reader.Read())
                    {
                        Func F = new Func
                        {
                            ID = reader["ID"].ToString(),
                            PCidade = reader["PCidade"].ToString(),
                            Contacto = reader["Contacto"].ToString(),
                            Email = reader["Email"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            Salario = reader["Salario"].ToString(),
                            PID = reader["PID"].ToString(),
                           
                        };
                        listBox2.Items.Add(F);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading funcionarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void LoadClienteListBox3()
        {

            listBox3.Items.Clear();
            if (!verifySGBDConnection())
                return;

            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM PM.Cliente", cn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {


                    while (reader.Read())
                    {
                        Cliente C = new Cliente
                        {
                            NIF = reader["NIF"].ToString(),
                            Contacto = reader["Contacto"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            Idade = reader["Idade"].ToString()

                        };
                        listBox3.Items.Add(C);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading funcionarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LoadTransacoesListBox4()
        {
            if (!verifySGBDConnection())
                return;

            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM PM.TransacaoDetalhada", cn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    listBox4.Items.Clear();

                    while (reader.Read())
                    {
                        Transacao T = new Transacao
                        {
                            TransacaoID = reader["Transacao_ID"].ToString(),
                            Data = reader["Data"].ToString(),
                            CombustivelID = reader["Combustivel_ID"].ToString(),
                            Quantidade = reader["Quantidade"].ToString(),
                            PrecoCombustivel = reader["Preco_Combustivel"].ToString(),
                            TotalCompra = reader["Total_Compra"].ToString(),
                            Nome = reader["Cliente_Nome"].ToString(),
                            Contacto = reader["Cliente_Contacto"].ToString(),
                            NIF = reader["Cliente_NIF"].ToString(),
                            FuncionarioNome = reader["Funcionario_Nome"].ToString(),
                            funcID = reader["Funcionario_ID"].ToString()
                        };
                        listBox4.Items.Add(T);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transacoes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ShowFunc()
        {
            Pcidade.BackColor = Color.Gainsboro;
            Fcontacto.BackColor = Color.Gainsboro;
            Femail.BackColor = Color.Gainsboro;
            PID.BackColor = Color.Gainsboro;
            FID.BackColor = Color.Gainsboro;
            Fname.BackColor = Color.Gainsboro;
            Fsalario.BackColor = Color.Gainsboro;
            if (listBox2.Items.Count == 0 | curFunc < 0)
                return;
            Func f = new Func();
            f = (Func)listBox2.Items[curFunc];
            Pcidade.Text = f.PCidade.ToString();
            Fcontacto.Text = f.Contacto.ToString();
            Fname.Text = f.Nome.ToString();
            PID.Text = f.PID.ToString();
            Femail.Text = f.Email.ToString();
            Fsalario.Text = f.Salario.ToString();
            FID.Text = f.ID.ToString();
        }

        public void ShowCliente()
        {
            
            if (listBox3.Items.Count == 0 | curFunc < 0)
                return;
            Cliente c = new Cliente();
            c = (Cliente)listBox3.Items[curCliente];
            Cname.Text = c.Nome.ToString();
            Ccontacto.Text = c.Contacto.ToString();
            CNIF.Text = c.NIF.ToString();
            Cage.Text = c.Idade.ToString();
            SqlCommand cmd = new SqlCommand("SELECT Total_Spent FROM PM.CustomerTotalSpent WHERE Customer_NIF = @NIF", cn);
            cmd.Parameters.AddWithValue("@NIF", int.Parse(c.NIF));
            object totals = cmd.ExecuteScalar();
            total.Text = $"{Convert.ToDecimal(totals):C}";
        }

        public void ShowTransacao()
        {
            clearFieldsTransacao();
            if (listBox4.Items.Count == 0 || curT < 0)
            {
                return;
            }
            Transacao t = (Transacao)listBox4.Items[curT];
            Tnome.Text = t.Nome.ToString();
            Tcontacto.Text = t.Contacto.ToString();
            TNIF.Text = t.NIF.ToString();
            Ttotal.Text = t.TotalCompra.ToString();
            Tprice.Text = t.PrecoCombustivel.ToString();
            Tquantidade.Text = t.Quantidade.ToString();
            TID.Text = t.TransacaoID.ToString();
            DateTime data = DateTime.Parse(t.Data);
            Tdata.Text = data.ToString("yyyy-MM-dd");
            Tfunc.Text = t.FuncionarioNome.ToString();
            TfuncID.Text = t.funcID.ToString();
            int IDCombustivel = int.Parse(t.CombustivelID);
            string produto;
            switch (IDCombustivel)
            {   
                case 1:
                    produto = "Diesel";
                    break;
                case 2:
                    produto = "Gasolina";
                    break;
                default:
                    produto = "GPL"; 
                    break;
            }
            Tproduto.Text = produto;
        }
        public void ShowPosto()
        {
            label10.Visible = true;
            label13.Visible = true;
            mgrContact.Visible = true;
            mgrEmail.Visible = true;
            city.BackColor = Color.Gainsboro;
            contacto.BackColor = Color.Gainsboro;
            Horario.BackColor = Color.Gainsboro;
            Gasolina.BackColor = Color.Gainsboro;
            Diesel.BackColor = Color.Gainsboro;
            GPL.BackColor = Color.Gainsboro;
            mgrName.BackColor = Color.Gainsboro;
            

            if (listBox1.Items.Count == 0 || curPosto < 0)
                return;

            Posto p = (Posto)listBox1.Items[curPosto];
            IDTXT.Text = p.ID;
            city.Text = p.Cidade;
            contacto.Text = p.Contacto;
            Horario.Text = p.HoraAbertura + " - " + p.HoraFecho;
            mgrContact.Text = p.MgrContacto;
            mgrEmail.Text = p.MgrEmail;
            mgrName.Text = p.MgrNome;
            mgrID.Text = p.MgrID; 

            for (int i = 1; i <= 3; i++)
            {
                int DepoID = int.Parse(p.ID) * 100 + i;
                try
                {
                    using (SqlCommand queryy = new SqlCommand("SELECT * FROM PM.capacidadesDepositos WHERE IDDeposito = @DepoID", cn))
                    {
                        queryy.Parameters.AddWithValue("@DepoID", DepoID);
                        using (SqlDataReader reader = queryy.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (i == 1)
                                    Diesel.Text = reader["CapacidadeAtual"].ToString();
                                if (i == 2)
                                    Gasolina.Text = reader["CapacidadeAtual"].ToString();
                                if (i == 3)
                                    GPL.Text = reader["CapacidadeAtual"].ToString();
                            }
                            reader.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading capacities for PostoID {p.ID}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            string mgrIDX = p.MgrID; 
            SqlCommand query = new SqlCommand("SELECT Nome, Email, Contacto FROM PM.managerList WHERE ID = @ID", cn);
            query.Parameters.AddWithValue("@ID", mgrIDX);

            try
            {
                using (SqlDataReader reader = query.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        mgrName.Text = reader["Nome"].ToString();
                        mgrEmail.Text = reader["Email"].ToString();
                        mgrContact.Text = reader["Contacto"].ToString();
                    }
                    else
                    {
                        Console.WriteLine($"No manager found for ID: {mgrID}");
                    }
                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
            }
        }



        private void SubmitPosto(Posto P)
        {
            if (!verifySGBDConnection())
                return;

            using (SqlCommand cmd = new SqlCommand("PM.AdicionarPosto", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_ID", int.Parse(P.ID));
                cmd.Parameters.AddWithValue("@p_Cidade", P.Cidade);
                cmd.Parameters.AddWithValue("@p_Contacto", int.Parse(P.Contacto));
                cmd.Parameters.AddWithValue("@p_Hora_Abertura", TimeSpan.Parse(P.HoraAbertura));
                cmd.Parameters.AddWithValue("@p_Hora_Fecho", TimeSpan.Parse(P.HoraFecho));
                cmd.Parameters.AddWithValue("@p_MgrID", int.Parse(P.MgrID));

                try
                {
                    cmd.ExecuteNonQuery();

                    for (int i = 1; i <= 3; i++)
                    {
                        int depoID = 100 * int.Parse(P.ID) + i;

                        using (SqlCommand inst = new SqlCommand("INSERT INTO PM.Deposito (ID, CapacidadeMax, CapacidadeAtual, ID_Posto) VALUES (@ID, @CapacidadeMax, @CapacidadeAtual, @ID_Posto)", cn))
                        {
                            inst.Parameters.AddWithValue("@ID", depoID);
                            inst.Parameters.AddWithValue("@CapacidadeMax", 50000);
                            inst.Parameters.AddWithValue("@CapacidadeAtual", 50000);
                            inst.Parameters.AddWithValue("@ID_Posto", int.Parse(P.ID));
                            inst.ExecuteNonQuery();
                        }

                        using (SqlCommand inst2 = new SqlCommand("INSERT INTO PM.Bomba (ID, IDCombustivel, IDDeposito, PostoID) VALUES (@ID, @IDCombustivel, @IDDeposito, @PostoID)", cn))
                        {
                            inst2.Parameters.AddWithValue("@ID", depoID);
                            inst2.Parameters.AddWithValue("@IDCombustivel", i);
                            inst2.Parameters.AddWithValue("@IDDeposito", depoID);
                            inst2.Parameters.AddWithValue("@PostoID", int.Parse(P.ID));
                            inst2.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Posto inserido com sucesso");
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to insert posto in database. \n ERROR MESSAGE: \n" + ex.Message);
                }
                LoadPostosListBox1();
            }
        }
        private void FilterFuncByName(string name)
        {
            if (!verifySGBDConnection())
                return;

            listBox2.Items.Clear();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM PM.FilterFuncByName(@Nome)", cn))
            {
                cmd.Parameters.AddWithValue("@Nome", name);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Func func = new Func
                        {
                            ID = reader["ID"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            Email = reader["Email"].ToString(),
                            Contacto = reader["Contacto"].ToString(),
                            Salario = reader["Salario"].ToString(),
                            PID = reader["PID"].ToString(),
                            PCidade = reader["PCidade"].ToString()
                        };
                        listBox2.Items.Add(func);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao filtrar funcionários pelo nome: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void FilterClienteByName(string name)
        {
            if (!verifySGBDConnection())
                return;

            listBox3.Items.Clear();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM PM.FilterClienteByName(@Nome)", cn))
            {
                cmd.Parameters.AddWithValue("@Nome", name);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Cliente c = new Cliente
                        {
                            NIF = reader["NIF"].ToString(),
                            Contacto = reader["Contacto"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            Idade = reader["Idade"].ToString()

                        };
                        listBox3.Items.Add(c);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao filtrar funcionários pelo nome: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FilterFuncByCity(string city)
        {
            if (!verifySGBDConnection())
                return;

            listBox2.Items.Clear();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM PM.FilterFuncByCity(@Cidade)", cn))
            {
                cmd.Parameters.AddWithValue("@Cidade", city);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Func func = new Func
                        {
                            ID = reader["ID"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            Email = reader["Email"].ToString(),
                            Contacto = reader["Contacto"].ToString(),
                            Salario = reader["Salario"].ToString(),
                            PID = reader["PID"].ToString(),
                            PCidade = reader["PCidade"].ToString()
                        };
                        listBox2.Items.Add(func);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao filtrar funcionários pela cidade: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void FilterTransacaoByClienteName(string name)
        {
            if (!verifySGBDConnection())
                return;

            listBox4.Items.Clear();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM PM.FilterTransacaoByClienteName(@Nome)", cn))
            {
                cmd.Parameters.AddWithValue("@Nome", name);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Transacao transacao = new Transacao
                        {
                            TransacaoID = reader["Transacao_ID"].ToString(),
                            Data = reader["Data"].ToString(),
                            CombustivelID = reader["Combustivel_ID"].ToString(),
                            Quantidade = reader["Quantidade"].ToString(),
                            PrecoCombustivel = reader["Preco_Combustivel"].ToString(),
                            TotalCompra = reader["Total_Compra"].ToString(),
                            Nome = reader["Cliente_Nome"].ToString(),
                            Contacto = reader["Cliente_Contacto"].ToString(),
                            NIF = reader["Cliente_NIF"].ToString(),
                            FuncionarioNome = reader["Funcionario_Nome"].ToString(),
                            funcID = reader["Funcionario_ID"].ToString()
                        };
                        listBox4.Items.Add(transacao);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao filtrar transações pelo nome do cliente: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FilterTransacaoByFuncionarioName(string name)
        {
            if (!verifySGBDConnection())
                return;

            listBox4.Items.Clear();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM PM.FilterTransacaoByFuncName(@Nome)", cn))
            {
                cmd.Parameters.AddWithValue("@Nome", name);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Transacao transacao = new Transacao
                        {
                            TransacaoID = reader["Transacao_ID"].ToString(),
                            Data = reader["Data"].ToString(),
                            CombustivelID = reader["Combustivel_ID"].ToString(),
                            Quantidade = reader["Quantidade"].ToString(),
                            PrecoCombustivel = reader["Preco_Combustivel"].ToString(),
                            TotalCompra = reader["Total_Compra"].ToString(),
                            Nome = reader["Cliente_Nome"].ToString(),
                            Contacto = reader["Cliente_Contacto"].ToString(),
                            NIF = reader["Cliente_NIF"].ToString(),
                            FuncionarioNome = reader["Funcionario_Nome"].ToString(),
                            funcID = reader["Funcionario_ID"].ToString()
                        };
                        listBox4.Items.Add(transacao);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao filtrar transações pelo nome do funcionário: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void UpdatePosto(Posto P)
        {
            if (!verifySGBDConnection())
                return;

            try
            {
                using (SqlCommand cmd = new SqlCommand("PM.UpdatePostoDetails", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_ID", int.Parse(P.ID));
                    cmd.Parameters.AddWithValue("@p_Cidade", P.Cidade);
                    cmd.Parameters.AddWithValue("@p_Contacto", int.Parse(P.Contacto));
                    cmd.Parameters.AddWithValue("@p_Hora_Abertura", TimeSpan.Parse(P.HoraAbertura));
                    cmd.Parameters.AddWithValue("@p_Hora_Fecho", TimeSpan.Parse(P.HoraFecho));
                    cmd.Parameters.AddWithValue("@p_MgrID", int.Parse(P.MgrID));

                    cmd.ExecuteNonQuery();
                }

                
                UpdateDepositosCapacidades(int.Parse(P.ID), int.Parse(Diesel.Text), int.Parse(Gasolina.Text), int.Parse(GPL.Text));

                MessageBox.Show("Posto atualizado com sucesso");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update posto in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            LoadPostosListBox1();
        }


        private void UpdateDepositosCapacidades(int postoID, int dieselCap, int gasolinaCap, int gplCap)
        {
            try
            {
                // Atualizar capacidade do Diesel
                using (SqlCommand cmd = new SqlCommand("UPDATE PM.capacidadesDepositos SET CapacidadeAtual = @cap WHERE IDDeposito = @id", cn))
                {
                    cmd.Parameters.AddWithValue("@cap", dieselCap);
                    cmd.Parameters.AddWithValue("@id", postoID * 100 + 1);
                    cmd.ExecuteNonQuery();
                }

                // Atualizar capacidade da Gasolina
                using (SqlCommand cmd = new SqlCommand("UPDATE PM.capacidadesDepositos SET CapacidadeAtual = @cap WHERE IDDeposito = @id", cn))
                {
                    cmd.Parameters.AddWithValue("@cap", gasolinaCap);
                    cmd.Parameters.AddWithValue("@id", postoID * 100 + 2);
                    cmd.ExecuteNonQuery();
                }

                // Atualizar capacidade do GPL
                using (SqlCommand cmd = new SqlCommand("UPDATE PM.capacidadesDepositos SET CapacidadeAtual = @cap WHERE IDDeposito = @id", cn))
                {
                    cmd.Parameters.AddWithValue("@cap", gplCap);
                    cmd.Parameters.AddWithValue("@id", postoID * 100 + 3);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update deposit capacities in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
        }



        private bool SavePosto()
        {
            Posto posto = new Posto();
            try
            {
                posto.ID = IDTXT.Text;
                posto.Cidade = city.Text;
                posto.Contacto = contacto.Text;
                string[] horario = Horario.Text.Split('-');
                posto.HoraAbertura = horario[0].Trim();
                posto.HoraFecho = horario[1].Trim();
                posto.MgrID = mgrID.Text;
                posto.MgrEmail = mgrEmail.Text;
                posto.MgrContacto = mgrContact.Text;
                posto.MgrNome = mgrName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            if (adding)
            {
                SubmitPosto(posto);
                LoadPostosListBox1();
            }
            else
            {
                UpdatePosto(posto);
            }
            LoadPostosListBox1();
            return true;
        }

        private bool SaveCliente()
        {
            Cliente c = new Cliente();
            try
            {
                c.Contacto = Ccontacto.Text;
                c.Idade = Cage.Text;
                c.Nome = Cname.Text;
                c.NIF = CNIF.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            if (adding)
            {
                SubmitCliente(c);
            }
            else
            {
                UpdateCliente(c);
            }
            LoadClienteListBox3();

            return true;
        }

        private void SaveFunc()
        {
            Func func = new Func();
            func.ID = FID.Text;
            func.Nome = Fname.Text;
            func.Contacto = Fcontacto.Text;

            string[] nomeFuncionarioParts = Fname.Text.ToLower().Split(' ');

            if (nomeFuncionarioParts.Length >= 2)
            {
                func.Email = $"{nomeFuncionarioParts[0]}.{nomeFuncionarioParts[1]}@PM.com";
            }
            else if (nomeFuncionarioParts.Length == 1)
            {
                func.Email = $"{nomeFuncionarioParts[0]}@PM.com";
            }

            func.Salario = Fsalario.Text;
            func.PID = PID.Text;


            if (!verifySGBDConnection())
                return;

            using (SqlCommand cmd = new SqlCommand("Select * from PM.CityID WHERE ID = @FuncID", cn))
            {
                cmd.Parameters.AddWithValue("@FuncID", int.Parse(func.ID));
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        func.PCidade = reader["Cidade"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao buscar a cidade: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }



            if (adding)
            {
                SubmitFunc(func);
            }
            else
            {
                UpdateFunc(func);
            }
            initializeFiltroC();
            LoadFuncListBox2();
        }

        private void SaveTransacao()
        {
            Transacao transacao = new Transacao();
            transacao.TransacaoID = TID.Text;
            transacao.funcID = TfuncID.Text;
            transacao.Data = Tdata.Text;
            transacao.CombustivelID = Tproduto.Text;
            transacao.Quantidade = Tquantidade.Text;
            transacao.Nome = Tnome.Text;
            transacao.Contacto = Tcontacto.Text;
            transacao.NIF = TNIF.Text;
            

            if (!verifySGBDConnection())
                return;

            // Get the fuel price from PM.Precario
            using (SqlCommand cmd = new SqlCommand("SELECT Preco FROM PM.Precario WHERE CombustivelID = @CombustivelID AND DataInicio <= @Data AND (DataFim IS NULL OR DataFim >= @Data)", cn))
            {
                cmd.Parameters.AddWithValue("@CombustivelID", int.Parse(transacao.CombustivelID));
                cmd.Parameters.AddWithValue("@Data", DateTime.Parse(transacao.Data).Date);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        transacao.PrecoCombustivel = reader["Preco"].ToString();
                        
                    }
                    else
                    {
                        MessageBox.Show("Preço do combustível não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao buscar o preço do combustível: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            // Calculate total purchase
            transacao.TotalCompra = (float.Parse(transacao.PrecoCombustivel) * float.Parse(transacao.Quantidade)).ToString();
            
            if (adding)
            {
                SubmitTransacao(transacao);
            }
            else
            {
                UpdateTransacao(transacao);
            }
        }

        private void SubmitTransacao(Transacao transacao)
        {
            using (SqlCommand cmd = new SqlCommand("PM.AddTransacao", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TransData", DateTime.Parse(transacao.Data).Date);
                cmd.Parameters.AddWithValue("@Litros", int.Parse(transacao.Quantidade));
                cmd.Parameters.AddWithValue("@IDCombustivel", int.Parse(transacao.CombustivelID));
                cmd.Parameters.AddWithValue("@Quantia", float.Parse(transacao.TotalCompra));
                cmd.Parameters.AddWithValue("@NIFCliente", int.Parse(transacao.NIF));
                cmd.Parameters.AddWithValue("@IDFunc", int.Parse(transacao.funcID));

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string message = reader["Message"].ToString();
                        MessageBox.Show(message, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar a transação: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                LoadTransacoesListBox4();
                
            }
        }

        private void UpdateTransacao(Transacao transacao)
        {
            using (SqlCommand cmd = new SqlCommand("PM.UpdateTransacao", cn))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TransacaoID", int.Parse(transacao.TransacaoID));
                cmd.Parameters.AddWithValue("@TransData", DateTime.Parse(transacao.Data).Date);
                cmd.Parameters.AddWithValue("@Litros", int.Parse(transacao.Quantidade));
                cmd.Parameters.AddWithValue("@IDCombustivel", int.Parse(transacao.CombustivelID));
                cmd.Parameters.AddWithValue("@Quantia", float.Parse(transacao.TotalCompra));
                cmd.Parameters.AddWithValue("@NIFCliente", int.Parse(transacao.NIF));
                cmd.Parameters.AddWithValue("@IDFunc", int.Parse(transacao.funcID));


                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    if (reader.Read())
                    {
                        
                        string message = reader["Message"].ToString();
                        MessageBox.Show(message, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar a transação: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                LoadTransacoesListBox4();
            }
        }

        private void SubmitFunc(Func func)
        {
            using (SqlCommand cmd = new SqlCommand("PM.AddFunc", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", int.Parse(func.ID));
                cmd.Parameters.AddWithValue("@Nome", func.Nome);
                cmd.Parameters.AddWithValue("@Contacto", int.Parse(func.Contacto));
                cmd.Parameters.AddWithValue("@Email", func.Email);
                cmd.Parameters.AddWithValue("@Salario", float.Parse(func.Salario));
                cmd.Parameters.AddWithValue("@PID", int.Parse(func.PID));

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Funcionário adicionado com sucesso", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao adicionar funcionário: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            LoadFuncListBox2();
            initializeFiltroC();
        }

        private void SubmitCliente(Cliente c)
        {
            using (SqlCommand cmd = new SqlCommand("PM.RegistarNovoCliente", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NIF", int.Parse(c.NIF));
                cmd.Parameters.AddWithValue("@Nome", c.Nome);
                cmd.Parameters.AddWithValue("@Contacto", c.Contacto);
                cmd.Parameters.AddWithValue("@Idade", int.Parse(c.Idade));

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cliente adicionado com sucesso", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao adicionar cliente: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            LoadClienteListBox3();
            initializeFiltroC();
        }

        private void UpdateFunc(Func func)
        {
            using (SqlCommand cmd = new SqlCommand("PM.UpdateFunc", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", int.Parse(func.ID));
                cmd.Parameters.AddWithValue("@Nome", func.Nome);
                cmd.Parameters.AddWithValue("@Contacto", int.Parse(func.Contacto));
                cmd.Parameters.AddWithValue("@Email", func.Email);
                cmd.Parameters.AddWithValue("@Salario", float.Parse(func.Salario));
                cmd.Parameters.AddWithValue("@PID", int.Parse(func.PID));

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Funcionário atualizado com sucesso", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar funcionário: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            LoadFuncListBox2();
            initializeFiltroC();
        }

        private void UpdateCliente(Cliente c)
        {
            using (SqlCommand cmd = new SqlCommand("PM.UpdateCliente", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NIF", int.Parse(c.NIF));
                cmd.Parameters.AddWithValue("@Nome", c.Nome);
                cmd.Parameters.AddWithValue("@Contacto", c.Contacto);
                cmd.Parameters.AddWithValue("@Idade", int.Parse(c.Idade));

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Funcionário atualizado com sucesso", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar funcionário: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            LoadClienteListBox3();
            initializeFiltroC();
        }

       

        private void DeletePosto(Posto p)
        {
            if (!verifySGBDConnection())
                return;



            try
            {
                using (SqlCommand cmd = new SqlCommand("PM.DeletePosto", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PostoID", p.ID);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Posto eliminado com sucesso", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao eliminar posto: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadPostosListBox1();
        }

        private void DeleteFunc(int postoID)
        {
            if (!verifySGBDConnection())
                return;

            try
            {
                using (SqlCommand cmd = new SqlCommand("PM.DeleteFunc", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FuncID", postoID);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Funcionário eliminado com sucesso", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao eliminar posto: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadFuncListBox2();
            initializeFiltroC();    
        }

        private void DeleteCliente(int NIF)
        {
            if (!verifySGBDConnection())
                return;

            try
            {
                using (SqlCommand cmd = new SqlCommand("PM.DeleteCliente", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NIF", NIF);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cliente eliminado com sucesso", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao eliminar cliente: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadClienteListBox3();
            initializeFiltroC();
        }


        private void clearFieldsPosto()
        {
            city.BackColor = Color.White;
            contacto.BackColor = Color.White;
            Horario.BackColor = Color.White;
            mgrName.BackColor = Color.White;
            city.Text = "";
            contacto.Text = "";
            Horario.Text = "";
            mgrName.Text = "";
            mgrEmail.Text = "";
            mgrContact.Text = "";
            GPL.Text = "";
            Gasolina.Text = "";
            Diesel.Text = "";
        }

        private void clearFieldsFunc()
        {
            Fcontacto.BackColor = Color.White;
            Fname.BackColor = Color.White;
            Femail.BackColor = Color.White;
            Fsalario.BackColor = Color.White;
            PID.BackColor = Color.White;
            Fcontacto.Text = "";
            Fname.Text = "";
            Femail.Text = "";
            Fsalario.Text = "";
            PID.Text = "";

            
        }

        private void clearFieldsTransacao()
        {

            Tcontacto.Text = "";
            Tnome.Text = "";
            Ttotal.Text = "";
            Tdata.Text = "";
            TNIF.Text = "";
            Tprice.Text = "";
            Tquantidade.Text = "";
            Tproduto.Text = "";
            Tfunc.Text = "";
            TfuncID.Text = "";
            TID.Text = "";

        }

        private void clearFieldsCliente()
        {
            Cname.BackColor = Color.White;
            Ccontacto.BackColor = Color.White;
            Cage.BackColor = Color.White;
            Cname.Text = "";
            Ccontacto.Text = "";
            Cage.Text = "";
        }

        public void ShowButtons()
        {
            LockControls();
            bttnAdd.Visible = true;
            btnDelete.Visible = true;
            bttnEdit.Visible = true;
            bttnOK.Visible = false;
            bttnCancel.Visible = false;

        }
        public void ShowButtonsFunc()
        {
            LockControlsFunc();
            bttnAdd2.Visible = true;
            bttnDelete2.Visible = true;
            bttnEdit2.Visible = true;
            bttnOK2.Visible = false;
            bttnCancel2.Visible = false;

        }

        public void ShowButtonsCliente()
        {
            LockControlsFunc();
            bttnAdd3.Visible = true;
            bttnDelete3.Visible = true;
            bttnEdit3.Visible = true;
            bttnOk3.Visible = false;
            bttnCancel3.Visible = false;
            label28.Visible = true;
            Cfilter.Visible = true;
            label24.Visible = true;
        }

        public void ShowButtonsTransacao()
        {
            LockControlsTransacao();
            Tfunc.Visible = true;
            bttnAdd4.Visible = true;
            bttnDelete4.Visible = true;
            bttnEdit4.Visible = true;
            bttnOk4.Visible = false;
            bttnCancel4.Visible = false;
            label33.Visible = true;
            Tfilterc.Visible = true;
            Tfilterf.Visible = true;
        }
        public void HideButtons()
        {
            UnlockControls();
            bttnAdd.Visible = false;
            btnDelete.Visible = false;
            bttnEdit.Visible = false;
            bttnOK.Visible = true;
            bttnCancel.Visible = true;
        }

        public void HideButtonsFunc()
        {
            UnlockControlsFunc();
            bttnAdd2.Visible = false;
            bttnDelete2.Visible = false;
            bttnEdit2.Visible = false;
            bttnOK2.Visible = true;
            bttnCancel2.Visible = true;
            Pcidade.Visible = false;
            label19.Visible = false;
            label15.Visible = false;
            label18.Visible = false;
            FilterCity.Visible = false;
            FilterName.Visible = false;
        }

        public void HideButtonsCliente()
        {
            UnlockControls();
            bttnAdd3.Visible = false;
            bttnDelete3.Visible = false;
            bttnEdit3.Visible = false;
            bttnOk3.Visible = true;
            bttnCancel3.Visible = true;
            label28.Visible = false;
            Cfilter.Visible = false;
            label24.Visible = false;

        }
        public void HideButtonsTransacao()
        {
            UnlockControlsTransacao();
            bttnAdd4.Visible = false;
            bttnDelete4.Visible = false;
            bttnEdit4.Visible = false;
            bttnOk4.Visible = true;
            bttnCancel4.Visible = true;
            Tfunc.Visible = false;
            label33.Visible = false;
            Tfilterc.Visible = false;
            Tfilterf.Visible = false;
        }
        public void UnlockControls()
        {
            city.BackColor = Color.White;
            contacto.BackColor = Color.White;
            Horario.BackColor = Color.White;
            mgrName.BackColor = Color.White;
            city.ReadOnly = false;
            contacto.ReadOnly = false;
            Horario.ReadOnly = false;
            mgrContact.ReadOnly = false;
            mgrEmail.ReadOnly = false;
            mgrName.ReadOnly = false;
        }

        public void UnlockControlsFunc()
        {
            Fsalario.ReadOnly = false;
            Fcontacto.ReadOnly = false;
            Fsalario.ReadOnly = false;
            Fname.ReadOnly = false;
            PID.ReadOnly = false;
            Fcontacto.BackColor = Color.White;
            Fname.BackColor = Color.White;
            Fsalario.BackColor = Color.White;
            PID.BackColor = Color.White;
        }

        public void UnlockControlsTransacao()
        {
            
            TNIF.ReadOnly = false;
            Tfunc.ReadOnly = false;
            Tquantidade.ReadOnly = false;
            Tproduto.ReadOnly = false;
            label31.Text = "ID Funcionario";
            TfuncID.Visible = true;
            TNIF.BackColor = Color.White;
            Tfunc.BackColor = Color.White;
            Tquantidade.BackColor = Color.White;
            Tproduto.BackColor = Color.White;
        }

        public void UnlockControlsCliente()
        {
            Cname.ReadOnly = false;
            Ccontacto.ReadOnly = false;
            Cage.ReadOnly = false;
        }

        public void LockControls()
        {
            city.BackColor = Color.Gainsboro;
            contacto.BackColor = Color.Gainsboro;
            Horario.BackColor = Color.Gainsboro;
            Gasolina.BackColor = Color.Gainsboro;
            Diesel.BackColor = Color.Gainsboro;
            GPL.BackColor = Color.Gainsboro;
            mgrName.BackColor = Color.Gainsboro;
            IDTXT.ReadOnly = true;
            city.ReadOnly = true;
            contacto.ReadOnly = true;
            Horario.ReadOnly = true;
            GPL.ReadOnly = true;
            Diesel.ReadOnly = true;
            Gasolina.ReadOnly = true;
            mgrContact.ReadOnly = true;
            mgrEmail.ReadOnly = true;
            mgrName.ReadOnly = true;
        }

        public void LockControlsFunc()
        {
            Fname.ReadOnly = true;
            Fcontacto.ReadOnly = true;
            Femail.ReadOnly = true;
            Fsalario.ReadOnly = true;
            PID.ReadOnly = true;
            Fcontacto.BackColor = Color.Gainsboro;
            Fname.BackColor = Color.Gainsboro;
            Femail.BackColor = Color.Gainsboro;
            Fsalario.BackColor = Color.Gainsboro;
            PID.BackColor = Color.Gainsboro;
        }

        public void LockControlsCliente()
        {
            Cname.ReadOnly = true;
            Ccontacto.ReadOnly = true;
            Cage.ReadOnly = true;
        }

        public void LockControlsTransacao()
        {
            TNIF.ReadOnly = true;
            Ttotal.ReadOnly = true;
            Tfunc.ReadOnly = true;
            Tquantidade.ReadOnly = true;
            Tproduto.ReadOnly = true;
            label31.Text = "Funcionário Responsável";
            TfuncID.Visible = false;
            TNIF.BackColor = Color.Gainsboro;
            Ttotal.BackColor = Color.Gainsboro;
            Tfunc.BackColor = Color.Gainsboro;
            Tquantidade.BackColor = Color.Gainsboro;
            Tproduto.BackColor = Color.Gainsboro;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            adding = true;
            clearFieldsPosto();
            HideButtons();
            listBox1.Enabled = false;
            label7.Text = "ID";
            mgrID.Visible = true;
            mgrID.Text = "";
            mgrName.Visible = false;
            Horario.Text = "HH:MM:00-HH:MM:00";
            label10.Visible = false;
            label13.Visible = false;
            mgrContact.Visible = false;
            mgrEmail.Visible = false;
            SqlCommand query = new SqlCommand("SELECT * FROM PM.lastIDPosto",cn);
            int ID = (int) query.ExecuteScalar() + 1;
            IDTXT.Text = ID.ToString();

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            curPosto = listBox1.SelectedIndex;
            if (curPosto < 0)
            {
                MessageBox.Show("Selecione um posto para editar.");
                return;
            }
            adding = false;
            HideButtons();
            listBox1.Enabled = false;
            label7.Text = "ID";
            mgrID.Visible = true;
            mgrName.Visible = false;
            mgrID.Text = "";
            Posto posto = (Posto)listBox1.Items[curPosto];
            mgrID.Text = posto.MgrID; 

            label10.Visible = false;
            label13.Visible = false;
            mgrContact.Visible = false;
            mgrEmail.Visible = false;
            UnlockControls();

        }

        private void bttnCancel_Click(object sender, EventArgs e)
        {
            listBox1.Enabled = true;
            clearFieldsPosto();
            ShowButtons();
            label7.Text = "Nome";
            mgrID.Visible = false;
            mgrName.Visible = true;
            label10.Visible = true;
            label13.Visible = true;
            mgrContact.Visible = true;
            mgrEmail.Visible = true;

        }

        private void bttnDelete2_Click(object sender, EventArgs e)
        {
            
            if (listBox2.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, selecione um funcionario para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        
            int selectedFuncID = int.Parse(((Func)listBox2.SelectedItem).ID);
            DialogResult result = MessageBox.Show("Tem a certeza de que deseja eliminar este funcionario?", "Confirmação de Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        
            if (result == DialogResult.Yes)
            {
                DeleteFunc(selectedFuncID);
                LoadFuncListBox2();
            }   
        }

        private void bttnDelete_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, selecione um funcionário para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Posto selectedPosto = (Posto)listBox1.SelectedItem;
            DialogResult result = MessageBox.Show("Tem a certeza de que deseja eliminar este posto?", "Confirmação de Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeletePosto(selectedPosto);
                LoadPostosListBox1();
                initializeFiltro();
            }
        }


        private void bttnOK_Click(object sender, EventArgs e)
        {
            try
            {
                SavePosto();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            label7.Text = "Nome";
            mgrID.Visible = false;
            mgrName.Visible = true;
            LoadPostosListBox1();
            initializeFiltro();
            ShowPosto();
            listBox1.Enabled = true;
            int idx = listBox1.FindString(IDTXT.Text);
            listBox1.SelectedIndex = idx;
            ShowButtons();
            LockControls();
            clearFieldsPosto();
            initializeFiltro();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, selecione um posto para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Posto selectedPosto =((Posto) listBox1.SelectedItem);
            DialogResult result = MessageBox.Show("Tem certeza de que deseja excluir este posto?", "Confirmação de Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeletePosto(selectedPosto);
                LoadPostosListBox1();
                initializeFiltro();
            }
        }


        private void bttnAdd2_Click_1(object sender, EventArgs e)
        {
            adding = true;
            UnlockControlsFunc();
            clearFieldsFunc();
            HideButtonsFunc();
            listBox2.Enabled = false;
            SqlCommand query = new SqlCommand("SELECT * FROM PM.lastIDFunc", cn);
            int ID = (int)query.ExecuteScalar() + 1;
            FID.Text = ID.ToString();
            Femail.BackColor = Color.Gainsboro;
        }

        private void bttnCancel2_Click_1(object sender, EventArgs e)
        {
            FilterCity.Visible = true;
            FilterName.Visible = true;
            listBox2.Enabled = true;
            if (listBox2.Items.Count > 0)
            {
                curFunc = listBox1.SelectedIndex;
                if (curFunc < 0)
                    curFunc = 0;
                ShowFunc();
            }
            else
            {
                clearFieldsFunc();
                LockControlsFunc();
            }
            LockControlsFunc();
            ShowButtonsFunc();
            Pcidade.Visible =true;
            label19.Visible =true;
            label15.Visible =true;
            label18.Visible = true;
            listBox2.Enabled = true;
            clearFieldsFunc();
        }

        private void bttnEdit2_Click(object sender, EventArgs e)
        {
            curFunc = listBox2.SelectedIndex;
            if (curFunc < 0)
            {
                MessageBox.Show("Selecione um funcionário para editar");
                return;
            }
            listBox2.Enabled = false;
            adding = false;
            UnlockControlsFunc();
            HideButtonsFunc();
            label19.Visible = false;
            label15.Visible = false;
            label18.Visible = false;
            FilterCity.Visible = false;
            FilterName.Visible = false;
        }

        private void bttnOK2_Click(object sender, EventArgs e)
        {

            try
            {
                SaveFunc();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            LockControlsFunc();
            Pcidade.Visible = true;
            label19.Visible = true;
            label15.Visible = true;
            label18.Visible = true;
            FilterCity.Visible = true;
            FilterName.Visible = true;
            listBox2.Enabled = true;
            ShowButtonsFunc();
            clearFieldsFunc();
        }

        private void FilterCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCity = FilterCity.SelectedItem.ToString();
            FilterFuncByCity(selectedCity);
        }

        private void FilterName_TextChanged(object sender, EventArgs e)
        {
            string name = FilterName.Text;
            FilterFuncByName(name);
        }

        private void bttnAdd3_Click(object sender, EventArgs e)
        {
            adding = true;
            UnlockControlsCliente();
            clearFieldsCliente();
            HideButtonsCliente();
            listBox3.Enabled = false;
            CNIF.BackColor = Color.White;
            CNIF.ReadOnly = false;
            CNIF.Text = "";
            total.Text = "";
            
        }

        private void bttnEdit3_Click(object sender, EventArgs e)
        {
            curCliente = listBox3.SelectedIndex;
            if (curCliente < 0)
            {
                MessageBox.Show("Selecione um cliente para editar");
                return;
            }
            listBox3.Enabled = false;
            adding = false;
            UnlockControlsCliente();
            HideButtonsCliente();
            Cname.BackColor = Color.White;
            Ccontacto.BackColor = Color.White;
            Cage.BackColor = Color.White;

        }

        private void bttnDelete3_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, selecione um cliente para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedClienteNIF = int.Parse(((Cliente)listBox3.SelectedItem).NIF);
            DialogResult result = MessageBox.Show("Tem certeza de que deseja excluir este cliente?", "Confirmação de Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeleteCliente(selectedClienteNIF);
                LoadClienteListBox3();
            }
        }



        private void bttnCancel3_Click(object sender, EventArgs e)
        {
            listBox3.Enabled = true;
            if (listBox3.Items.Count > 0)
            {
                curCliente = listBox3.SelectedIndex;
                if (curCliente < 0)
                    curCliente = 0;
                ShowCliente();
            }
            else
            {
                clearFieldsCliente();
               
            }
            clearFieldsCliente();
            LockControlsCliente();
            ShowButtonsCliente();
            Cname.BackColor = Color.Gainsboro;
            Ccontacto.BackColor = Color.Gainsboro;
            Cage.BackColor = Color.Gainsboro;

        }

        private void bttnOk3_Click(object sender, EventArgs e)
        {
            try
            {
                SaveCliente();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Cname.BackColor = Color.Gainsboro;
            Ccontacto.BackColor = Color.Gainsboro;
            Cage.BackColor = Color.Gainsboro;
            label28.Visible = false;
            Cfilter.Visible = false;
            label24.Visible = false;
            ShowButtonsCliente();
            CNIF.BackColor = Color.Gainsboro;
            CNIF.ReadOnly = true;
            listBox3.Enabled = true;
            clearFieldsCliente();
            CNIF.Text = "";
            total.Text = "";
        }

        private void Cfilter_TextChanged(object sender, EventArgs e)
        {
            string name = Cfilter.Text;
            FilterClienteByName(name);
        }


        private void bttnAdd4_Click(object sender, EventArgs e)
        {
            adding = true;
            UnlockControlsTransacao();
            clearFieldsTransacao();
            HideButtonsTransacao();
            SqlCommand query = new SqlCommand("SELECT * FROM PM.LastIDTransacao", cn);
            int ID = (int)query.ExecuteScalar() + 1;
            TID.Text = ID.ToString();
            Tdata.ReadOnly = false;
            Tdata.Text = "2024-MM-DD";
        }

        private void bttnEdit4_Click(object sender, EventArgs e)
        {
            curT = listBox4.SelectedIndex;
            if (curT < 0)
            {
                MessageBox.Show("Selecione uma transação para editar");
                return;
            }
            listBox4.Enabled = false;
            adding = false;
            UnlockControlsTransacao();
            HideButtonsTransacao();
            if (Tproduto.Text == "Diesel")
            {
                Tproduto.Text = "1";
            }
            else if (Tproduto.Text == "Gasolina")
            {
                Tproduto.Text = "2";
            }
            else
            {
                Tproduto.Text = "3"; 
            }

        }

        private void bttnCancel4_Click(object sender, EventArgs e)
        {
            LockControlsTransacao();
            ShowButtonsTransacao();
            listBox4.Enabled = true;
            if (listBox4.Items.Count > 0)
            {
                curT = listBox4.SelectedIndex;
                if (curT < 0)
                    curT = 0;
                ShowTransacao();
                Tdata.ReadOnly = true;
            }
            else
            {
                clearFieldsTransacao();
                LockControlsTransacao();
            }
        }

        private void bttnOk4_Click(object sender, EventArgs e)
        {
            try
            {
                SaveTransacao();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ShowButtonsTransacao();
            listBox4.Enabled = true;
            LockControlsTransacao();
            clearFieldsTransacao();
        }

        private void Tfilterc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selCliente = Tfilterc.SelectedItem.ToString();
            FilterTransacaoByClienteName(selCliente);
        }

        private void Tfilterf_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selfunc = Tfilterf.SelectedItem.ToString();
            FilterTransacaoByFuncionarioName(selfunc);
        }
        private void LoadPricesForWeek(string dataInicio)
        {
            if (!verifySGBDConnection()) return;
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT CombustivelID, Preco FROM PM.Precario WHERE DataInicio = @DataInicio", cn))
                {
                    cmd.Parameters.AddWithValue("@DataInicio", DateTime.Parse(dataInicio)); 

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                        txtGasolina.Text = "";
                        txtDiesel.Text = "";
                        txtGPL.Text = "";

                        while (reader.Read())
                        {
                            int combustivelID = reader.GetInt32(reader.GetOrdinal("CombustivelID"));
                            double preco = reader.GetDouble(reader.GetOrdinal("Preco")); 

                            switch (combustivelID)
                            {
                                case 1:
                                    txtDiesel.Text = preco.ToString("F3");
                                    break;
                                case 2:
                                    txtGasolina.Text = preco.ToString("F3");
                                    break;
                                case 3:
                                    txtGPL.Text = preco.ToString("F3");
                                    break;
                                default:
                                    throw new Exception("Unknown CombustivelID: " + combustivelID);
                            }
                        }
                    }
                }
            }
            catch (FormatException fe)
            {
                MessageBox.Show($"Invalid date format: {fe.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException se)
            {
                MessageBox.Show($"Database error: {se.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading prices for week: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePricesForWeek(int combustivelID, float novoPreco, string dataInicio)
        {
            if (!verifySGBDConnection())
                return;

            using (SqlCommand cmd = new SqlCommand("PM.updatePrice", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CombustivelID", combustivelID);
                cmd.Parameters.AddWithValue("@NovoPreco", novoPreco);
                cmd.Parameters.AddWithValue("@DataInicio", Convert.ToDateTime(dataInicio));

                cmd.ExecuteNonQuery();
            }
        }
        private void LoadPrecariosListBox()
        {
            listBox5.Items.Clear();
            if (!verifySGBDConnection())
                return;

            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT DataInicio FROM PM.Precario", cn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        DateTime dataInicio = Convert.ToDateTime(reader["DataInicio"]);
                        listBox5.Items.Add(dataInicio.ToString("yyyy-MM-dd"));
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading weeks: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdatePrice_Click(object sender, EventArgs e)
        {
            try
            {
                string dataInicio = DateTime.Today.ToString("yyyy-MM-dd");
                UpdatePricesForWeek(1, float.Parse(txtDiesel.Text), dataInicio);
                UpdatePricesForWeek(2, float.Parse(txtGasolina.Text), dataInicio);
                UpdatePricesForWeek(3, float.Parse(txtGPL.Text), dataInicio);

                MessageBox.Show("Preços dos combustíveis atualizados com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, insira preços válidos para os combustíveis.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar os preços dos combustíveis: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadPrecariosListBox();
        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox5.SelectedIndex >= 0)
            {
                string dataInicio = listBox5.SelectedItem.ToString();
                LoadPricesForWeek(dataInicio);
            }
        }

        private void bttnDelete4_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, selecione uma transação para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedTrans = int.Parse(((Transacao)listBox4.SelectedItem).TransacaoID);
            DialogResult result = MessageBox.Show("Tem certeza de que deseja excluir esta transação?", "Confirmação de Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeleteTransacao(selectedTrans);
                LoadTransacoesListBox4();   
            }
        }

        private void DeleteTransacao(int transacaoID)
        {
            if (!verifySGBDConnection())
                return;

            using (SqlCommand cmd = new SqlCommand("PM.DeleteTransacao", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TransacaoID", transacaoID);

                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Transação eliminada com sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Transação não encontrada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao eliminar a transação: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

}
