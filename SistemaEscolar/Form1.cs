// Aluno: Guilherme Vizzoni Haidmann
//Turma: CC4M

using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SistemaEscolar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InicializarBancoDeDados();
            PreencherComboBoxes();
        }

        private void InicializarBancoDeDados()
        {
            var connection = new MySqlConnection("Server=localhost;Uid=root;Pwd=123456"); //pode ser necessário alterar a senha

            // Cria o banco de dados se não existir
            string criarBanco = "CREATE DATABASE IF NOT EXISTS sistemaescolar"; //cria o banco de dados sistemaescolar
            MySqlCommand cmdBanco = new MySqlCommand(criarBanco, connection);
            connection.Open();
            cmdBanco.ExecuteNonQuery();
            connection.Close();

            connection = DatabaseConnection.GetInstance().GetConnection();

            string criarTabelaCurso = @"
                CREATE TABLE IF NOT EXISTS Curso (
                    CursoID INT AUTO_INCREMENT PRIMARY KEY,
                    NomeCurso VARCHAR(100) NOT NULL
                )";

            string criarTabelaAluno = @"
                CREATE TABLE IF NOT EXISTS Aluno (
                    AlunoID INT AUTO_INCREMENT PRIMARY KEY,
                    Matricula VARCHAR(20) NOT NULL UNIQUE,
                    Nome VARCHAR(100) NOT NULL,
                    DataNascimento DATE NOT NULL,
                    Endereco VARCHAR(200),
                    Periodo VARCHAR(50),
                    CursoID INT,
                    FOREIGN KEY (CursoID) REFERENCES Curso(CursoID)
                )";

            string inserirCursos = @"
                INSERT IGNORE INTO Curso (NomeCurso)
                VALUES 
                ('Ciência da Computação'),
                ('Direito'),
                ('Engenharia Elétrica'),
                ('Psicologia'),
                ('Medicina')";

            MySqlCommand cmdCurso = new MySqlCommand(criarTabelaCurso, connection);
            cmdCurso.ExecuteNonQuery();

            MySqlCommand cmdAluno = new MySqlCommand(criarTabelaAluno, connection);
            cmdAluno.ExecuteNonQuery();

            MySqlCommand cmdInserirCursos = new MySqlCommand(inserirCursos, connection);
            cmdInserirCursos.ExecuteNonQuery();
        }

        private void PreencherComboBoxes()
        {
            var connection = DatabaseConnection.GetInstance().GetConnection();

            // Limpa os itens existentes para evitar duplicatas
            cmbCurso.Items.Clear();
            cmbPeriodo.Items.Clear();

            // Preencher cursos
            string queryCursos = "SELECT DISTINCT NomeCurso FROM Curso";
            MySqlCommand cmdCursos = new MySqlCommand(queryCursos, connection);
            MySqlDataReader reader = cmdCursos.ExecuteReader();

            while (reader.Read())
            {
                cmbCurso.Items.Add(reader.GetString("NomeCurso"));
            }
            reader.Close();

            // Adicionar períodos (somente uma vez)
            cmbPeriodo.Items.Add("Manhã");
            cmbPeriodo.Items.Add("Tarde");
            cmbPeriodo.Items.Add("Noite");
        }


        private void CarregarAlunos(string curso = null, string periodo = null)
        {
            var connection = DatabaseConnection.GetInstance().GetConnection();

            string query = @"
                SELECT a.AlunoID AS ID, a.Matricula, a.Nome, a.DataNascimento, a.Endereco, 
                       c.NomeCurso AS Curso, a.Periodo 
                FROM Aluno a 
                LEFT JOIN Curso c ON a.CursoID = c.CursoID 
                WHERE (@Curso IS NULL OR c.NomeCurso = @Curso) 
                AND (@Periodo IS NULL OR a.Periodo = @Periodo)";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Curso", (object)curso ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Periodo", (object)periodo ?? DBNull.Value);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);

            dgvAlunos.DataSource = table;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string selectedCurso = cmbCurso.SelectedItem?.ToString();
            string selectedPeriodo = cmbPeriodo.SelectedItem?.ToString();

            CarregarAlunos(selectedCurso, selectedPeriodo);
        }

        private void btnAdicionarAluno_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMatricula.Text) || string.IsNullOrEmpty(txtNomeAluno.Text) ||
                string.IsNullOrEmpty(txtEndereco.Text))
            {
                MessageBox.Show("Por favor, preencha todos os campos obrigatórios.");
                return;
            }

            var connection = DatabaseConnection.GetInstance().GetConnection();

            string query = @"
    INSERT INTO Aluno (Matricula, Nome, DataNascimento, Endereco, Periodo, CursoID) 
    VALUES (@Matricula, @Nome, @DataNascimento, @Endereco, @Periodo, 
            (SELECT CursoID FROM Curso WHERE NomeCurso = @Curso LIMIT 1))";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Matricula", txtMatricula.Text);
            cmd.Parameters.AddWithValue("@Nome", txtNomeAluno.Text);
            cmd.Parameters.AddWithValue("@DataNascimento", dtpDataNasciment.Value);
            cmd.Parameters.AddWithValue("@Endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@Periodo", cmbPeriodo.SelectedItem?.ToString() ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Curso", cmbCurso.SelectedItem?.ToString() ?? (object)DBNull.Value);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Aluno adicionado com sucesso!");
                CarregarAlunos(); // Atualiza o DataGridView
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erro ao adicionar aluno: {ex.Message}");
            }
        }


        private void btnBuscarAluno_Click(object sender, EventArgs e)
        {
            var connection = DatabaseConnection.GetInstance().GetConnection();

            // Consulta SQL com filtros opcionais
            string query = @"
        SELECT a.Matricula, a.Nome, a.DataNascimento, a.Endereco, c.NomeCurso AS Curso, a.Periodo 
        FROM Aluno a 
        LEFT JOIN Curso c ON a.CursoID = c.CursoID 
        WHERE (@Matricula IS NULL OR a.Matricula = @Matricula)
          AND (@Nome IS NULL OR a.Nome LIKE @Nome)
          AND (@Endereco IS NULL OR a.Endereco LIKE @Endereco)
          AND (@Periodo IS NULL OR a.Periodo = @Periodo)
          AND (@Curso IS NULL OR c.NomeCurso = @Curso)";

            MySqlCommand cmd = new MySqlCommand(query, connection);

            // Adiciona os parâmetros com base nos campos preenchidos
            cmd.Parameters.AddWithValue("@Matricula", string.IsNullOrEmpty(txtMatricula.Text) ? DBNull.Value : txtMatricula.Text);
            cmd.Parameters.AddWithValue("@Nome", string.IsNullOrEmpty(txtNomeAluno.Text) ? DBNull.Value : $"%{txtNomeAluno.Text}%");
            cmd.Parameters.AddWithValue("@Endereco", string.IsNullOrEmpty(txtEndereco.Text) ? DBNull.Value : $"%{txtEndereco.Text}%");
            cmd.Parameters.AddWithValue("@Periodo", cmbPeriodo.SelectedItem == null ? DBNull.Value : cmbPeriodo.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Curso", cmbCurso.SelectedItem == null ? DBNull.Value : cmbCurso.SelectedItem.ToString());

            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                dgvAlunos.DataSource = table; // Atualiza o DataGridView com os resultados
                MessageBox.Show($"{table.Rows.Count} aluno(s) encontrado(s)!");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erro ao buscar alunos: {ex.Message}");
            }
        }



        private void btnAtualizarAluno_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMatricula.Text))
            {
                MessageBox.Show("Por favor, selecione ou insira a matrícula do aluno que deseja atualizar.");
                return;
            }

            var connection = DatabaseConnection.GetInstance().GetConnection();

            string query = @"
    UPDATE Aluno 
    SET Nome = @Nome, DataNascimento = @DataNascimento, Endereco = @Endereco, 
        Periodo = @Periodo, 
        CursoID = (SELECT CursoID FROM Curso WHERE NomeCurso = @Curso LIMIT 1) 
    WHERE Matricula = @Matricula";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Matricula", txtMatricula.Text);
            cmd.Parameters.AddWithValue("@Nome", txtNomeAluno.Text);
            cmd.Parameters.AddWithValue("@DataNascimento", dtpDataNasciment.Value);
            cmd.Parameters.AddWithValue("@Endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@Periodo", cmbPeriodo.SelectedItem?.ToString() ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Curso", cmbCurso.SelectedItem?.ToString() ?? (object)DBNull.Value);

            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Aluno atualizado com sucesso!");
                    CarregarAlunos(); // Atualiza o DataGridView
                }
                else
                {
                    MessageBox.Show("Aluno não encontrado.");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erro ao atualizar aluno: {ex.Message}");
            }
        }


        private void btnRemoverAluno_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMatricula.Text))
            {
                MessageBox.Show("Por favor, insira a matrícula do aluno para removê-lo.");
                return;
            }

            var connection = DatabaseConnection.GetInstance().GetConnection();

            string query = "DELETE FROM Aluno WHERE Matricula = @Matricula";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Matricula", txtMatricula.Text);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Aluno removido com sucesso!");
                CarregarAlunos();
            }
            else
            {
                MessageBox.Show("Aluno não encontrado.");
            }
        }

        private void dgvAlunos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvAlunos.Rows[e.RowIndex];

                // Preenche os campos com os valores da linha selecionada
                txtMatricula.Text = row.Cells["Matricula"].Value?.ToString();
                txtNomeAluno.Text = row.Cells["Nome"].Value?.ToString();

                // Verifica se o valor de DataNascimento é DBNull
                if (row.Cells["DataNascimento"].Value != DBNull.Value)
                {
                    dtpDataNasciment.Value = Convert.ToDateTime(row.Cells["DataNascimento"].Value);
                }
                else
                {
                    dtpDataNasciment.Value = DateTime.Now; // Valor padrão
                }

                // Preenche os outros campos com verificações para DBNull
                txtEndereco.Text = row.Cells["Endereco"].Value == DBNull.Value ? string.Empty : row.Cells["Endereco"].Value.ToString();
                cmbPeriodo.SelectedItem = row.Cells["Periodo"].Value == DBNull.Value ? null : row.Cells["Periodo"].Value.ToString();
                cmbCurso.SelectedItem = row.Cells["Curso"].Value == DBNull.Value ? null : row.Cells["Curso"].Value.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
