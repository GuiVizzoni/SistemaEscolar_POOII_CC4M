namespace SistemaEscolar
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            dgvAlunos = new DataGridView();
            label1 = new Label();
            txtMatricula = new TextBox();
            txtNomeAluno = new TextBox();
            label2 = new Label();
            txtEndereco = new TextBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            cmbCurso = new ComboBox();
            cmbPeriodo = new ComboBox();
            dtpDataNasciment = new DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)dgvAlunos).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(793, 334);
            button1.Name = "button1";
            button1.Size = new Size(116, 28);
            button1.TabIndex = 0;
            button1.Text = "Adicionar Aluno";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnAdicionarAluno_Click;
            // 
            // button2
            // 
            button2.Location = new Point(915, 334);
            button2.Name = "button2";
            button2.Size = new Size(116, 28);
            button2.TabIndex = 1;
            button2.Text = "Buscar Aluno";
            button2.UseVisualStyleBackColor = true;
            button2.Click += btnBuscarAluno_Click;
            // 
            // button3
            // 
            button3.Location = new Point(1037, 334);
            button3.Name = "button3";
            button3.Size = new Size(116, 28);
            button3.TabIndex = 3;
            button3.Text = "Atualizar Aluno";
            button3.UseVisualStyleBackColor = true;
            button3.Click += btnAtualizarAluno_Click;
            // 
            // button4
            // 
            button4.Location = new Point(1159, 334);
            button4.Name = "button4";
            button4.Size = new Size(116, 28);
            button4.TabIndex = 2;
            button4.Text = "Remover Aluno";
            button4.UseVisualStyleBackColor = true;
            button4.Click += btnRemoverAluno_Click;
            // 
            // dgvAlunos
            // 
            dgvAlunos.AllowUserToOrderColumns = true;
            dgvAlunos.BackgroundColor = SystemColors.Control;
            dgvAlunos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAlunos.Location = new Point(745, 44);
            dgvAlunos.Name = "dgvAlunos";
            dgvAlunos.Size = new Size(584, 267);
            dgvAlunos.TabIndex = 4;
            dgvAlunos.CellDoubleClick += dgvAlunos_CellDoubleClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(148, 83);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 5;
            label1.Text = "Matrícula:";
            // 
            // txtMatricula
            // 
            txtMatricula.Location = new Point(248, 80);
            txtMatricula.Name = "txtMatricula";
            txtMatricula.Size = new Size(231, 23);
            txtMatricula.TabIndex = 6;
            // 
            // txtNomeAluno
            // 
            txtNomeAluno.Location = new Point(248, 109);
            txtNomeAluno.Name = "txtNomeAluno";
            txtNomeAluno.Size = new Size(231, 23);
            txtNomeAluno.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(148, 112);
            label2.Name = "label2";
            label2.Size = new Size(75, 15);
            label2.TabIndex = 7;
            label2.Text = "NomeAluno:";
            // 
            // txtEndereco
            // 
            txtEndereco.Location = new Point(248, 167);
            txtEndereco.Name = "txtEndereco";
            txtEndereco.Size = new Size(231, 23);
            txtEndereco.TabIndex = 12;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(148, 170);
            label3.Name = "label3";
            label3.Size = new Size(59, 15);
            label3.TabIndex = 11;
            label3.Text = "Endereço:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(148, 141);
            label4.Name = "label4";
            label4.Size = new Size(98, 15);
            label4.TabIndex = 9;
            label4.Text = "DataNascimento:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(148, 228);
            label5.Name = "label5";
            label5.Size = new Size(41, 15);
            label5.TabIndex = 15;
            label5.Text = "Curso:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(148, 199);
            label6.Name = "label6";
            label6.Size = new Size(51, 15);
            label6.TabIndex = 13;
            label6.Text = "Período:";
            // 
            // cmbCurso
            // 
            cmbCurso.FormattingEnabled = true;
            cmbCurso.Location = new Point(248, 225);
            cmbCurso.Name = "cmbCurso";
            cmbCurso.Size = new Size(231, 23);
            cmbCurso.TabIndex = 17;
            // 
            // cmbPeriodo
            // 
            cmbPeriodo.FormattingEnabled = true;
            cmbPeriodo.Location = new Point(248, 196);
            cmbPeriodo.Name = "cmbPeriodo";
            cmbPeriodo.Size = new Size(231, 23);
            cmbPeriodo.TabIndex = 18;
            // 
            // dtpDataNasciment
            // 
            dtpDataNasciment.Location = new Point(248, 138);
            dtpDataNasciment.Name = "dtpDataNasciment";
            dtpDataNasciment.Size = new Size(231, 23);
            dtpDataNasciment.TabIndex = 20;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fundo;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1461, 827);
            Controls.Add(dtpDataNasciment);
            Controls.Add(cmbPeriodo);
            Controls.Add(cmbCurso);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(txtEndereco);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(txtNomeAluno);
            Controls.Add(label2);
            Controls.Add(txtMatricula);
            Controls.Add(label1);
            Controls.Add(dgvAlunos);
            Controls.Add(button3);
            Controls.Add(button4);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAlunos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private DataGridView dgvAlunos;
        private Label label1;
        private TextBox txtMatricula;
        private TextBox txtNomeAluno;
        private Label label2;
        private TextBox txtEndereco;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private ComboBox cmbCurso;
        private ComboBox cmbPeriodo;
        private DateTimePicker dtpDataNasciment;
    }
}
