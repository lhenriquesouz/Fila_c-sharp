using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace SistemaCadastro
{
    public partial class Form1 : Form
    {
        struct tdado
        {
            public string nome, cpf;
            
            public int idade;
        };
        int qtd = 0;
        
        Queue<tdado> fila = new Queue<tdado>();
        Queue<tdado> filaIdoso = new Queue<tdado>();


        public Form1()
        {
            InitializeComponent();
        }

  
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void mostrar()
        {
            txtFila.Clear();
            foreach(tdado x in fila)
                txtFila.AppendText(x.nome + " - " + x.cpf + " - " + x.idade + "\r\n");

            txtIdoso.Clear();
            foreach (tdado x in filaIdoso)
                txtIdoso.AppendText(x.nome + " - " + x.cpf + " - " + x.idade + "\r\n");

        }
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            tdado x;
            
            x.nome = txtNome.Text;
            x.cpf = txtCPF.Text;
            x.idade = Convert.ToInt32(txtIdade.Text);
            if (Convert.ToInt32(x.idade) >= 60)
            {
                filaIdoso.Enqueue(x);
            }
            else
            {
                fila.Enqueue(x);
            }
          
            mostrar();
            // limpando os campos
            txtNome.Clear();
            txtCPF.Clear();
            txtIdade.Clear();
            txtNome.Focus();// Ponteiro piscando no txt.Nome
           
        }
        void salvar()
        {
            BinaryWriter arq = new BinaryWriter(File.Open("fila.txt", FileMode.Create));
            arq.Write(fila.Count());
            foreach (tdado b in fila)
            {
                arq.Write(b.nome);
                arq.Write(b.cpf);
                arq.Write(b.idade);
            }
          
           
            arq.Close();
           // MessageBox.Show("Dados Salvos com Sucesso", "Mensagem");
        }

        void salvarIdoso()
        {
            BinaryWriter arq = new BinaryWriter(File.Open("filaIdoso.txt", FileMode.Create));
            arq.Write(filaIdoso.Count());
            foreach (tdado b in filaIdoso)
            {
                arq.Write(b.nome);
                arq.Write(b.cpf);
                arq.Write(b.idade);
            }
          
            arq.Close();
           // MessageBox.Show("Dados Salvos com Sucesso", "Mensagem");
        }
        void carregar()
        {

            BinaryReader arq = new BinaryReader(
                File.Open("fila.txt", FileMode.Open));
            if (File.Exists("fila.txt"))
            {
                qtd = arq.ReadInt32();
                for (int i = 0; i < qtd; i++)
                {
                    tdado b;
                    b.nome = arq.ReadString();
                    b.cpf = arq.ReadString();
                    b.idade = arq.ReadInt32();
                    fila.Enqueue(b);                   
                }// fim for
                arq.Close();
                mostrar();
            }// fim if 
            else
                MessageBox.Show("Arquivo não encontrado", "Erro");
        }

        void carregarIdoso()
        {

            BinaryReader arq = new BinaryReader(
                File.Open("filaIdoso.txt", FileMode.Open));
            if (File.Exists("filaIdoso.txt"))
            {
                qtd = arq.ReadInt32();
                for (int i = 0; i < qtd; i++)
                {
                    tdado b;
                    b.nome = arq.ReadString();
                    b.cpf = arq.ReadString();
                    b.idade = arq.ReadInt32();
                    filaIdoso.Enqueue(b);
                }// fim for
                arq.Close();
                mostrar();
            }// fim if 
            else
                MessageBox.Show("Arquivo não encontrado", "Erro");
        }

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            carregar();
            carregarIdoso();
            MessageBox.Show("Arquivos carregados com sucesso","Mensagem");
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            salvar();
            salvarIdoso();
            MessageBox.Show("Dados Salvos com Sucesso", "Mensagem");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int i = 1;
            foreach (tdado x in fila)
            {
                if(x.cpf.Equals(txtBuscar.Text))
                {
                    // achou fala a posicao i
                    MessageBox.Show(x.nome + " Está na " + i + "º  Posicao ");
                    //"x.nome esta na posicao i"
                    break;
                    
                }
                i++;

            }

            int Y = 1;
            foreach (tdado x in filaIdoso)
            {
                if (x.cpf.Equals(txtBuscar.Text))
                {
                   
                        // achou fala a posicao i
                        MessageBox.Show(x.nome + " Está na " + Y + "º Posicao fila Preferencial");
                        //"x.nome esta na posicao i"
                        break;
                    
                }
                Y++;

            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            tdado v;
            v.cpf = txtCPF.Text;


                DialogResult resp = MessageBox.Show("Tem certeza que deseja desistir?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resp == DialogResult.Yes) 
               //if (v.cpf.Equals(txtBuscar.Text))
                {

                    
                    v = fila.Dequeue();

                    MessageBox.Show(v.nome + " desistiu", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //lblAtendimento.Text = "Proximo:\n" + v.nome;

                    mostrar();
                }
            
        }

        
        private void btnAtender_Click(object sender, EventArgs e)
        {/*
            tdado r;
            if(Volta > 3)
            {
                Volta = 0;
            }
            
            if (fila.Count > 0 || filaIdoso.Count > 0)
            {    
                 if(filaIdoso.Count > 0 && Volta <= 2)
                {
                    r = filaIdoso.Dequeue();
                    MessageBox.Show("Próximo: " + r.nome);
                    lblAtendimento.Text = "Atendendo Pref:\n" + "Nome: " + r.nome + "\n" + "CPF: " + r.cpf + "\n" + "Idade:" + r.idade;
                    Volta++;
                }
                 else
                {
                    r = fila.Dequeue();
                    MessageBox.Show("Próximo: " + r.nome);
                    lblAtendimento.Text = "Atendendo:\n" + "Nome: " + r.nome + "\n" + "CPF: " + r.cpf + "\n" + "Idade:" + r.idade;
                    Volta++;
                }
                  
                
            }
            else
            {
                MessageBox.Show("Fila vazia :( ");
            }
               
            mostrar();*/
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtFila_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIdade_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void lblAtendimento_Click(object sender, EventArgs e)
        {

        }

        int Volta = 0;
        private void btnAtend_Click(object sender, EventArgs e)
        {
            tdado r;
            if (Volta > 3)
            {
                Volta = 0;
            }

            if (fila.Count > 0 || filaIdoso.Count > 0)
            {
                if (filaIdoso.Count > 0 && Volta <= 2)
                {
                    r = filaIdoso.Dequeue();
                    MessageBox.Show("Próximo: " + r.nome);
                    lblAtendimento.Text = "Atendendo:\n" + "Nome: " + r.nome + "\n" + "CPF: " + r.cpf + "\n" + "Idade:" + r.idade;
                    Volta++;
                }
                else
                {
                    r = fila.Dequeue();
                    MessageBox.Show("Próximo: " + r.nome);
                    lblAtendimento.Text = "Atendendo:\n" + "Nome: " + r.nome + "\n" + "CPF: " + r.cpf + "\n" + "Idade:" + r.idade;
                    Volta++;
                }


            }
            else
            {
                MessageBox.Show("Fila vazia :( ");
            }

            mostrar();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //MessageBox.Show("Luis Henrique \n(35) 99880-5492", "Suporte");
            System.Diagnostics.Process.Start("https://api.whatsapp.com/send?l=pt&phone=5535998805492");

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnBusca_Click(object sender, EventArgs e)
        {
            marcador.Height = btnBusca.Height;
            marcador.Top = btnBusca.Top;
            tabControl1.SelectedTab = tabControl1.TabPages[0];
        }

        private void btnCadastra_Click(object sender, EventArgs e)
        {
            marcador.Height = btnCadastra.Height;
            marcador.Top = btnCadastra.Top;
            tabControl1.SelectedTab = tabControl1.TabPages[1];
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnConfirmar_MouseEnter(object sender, EventArgs e)
        {
            btnConfirmar.BackColor = Color.FromArgb(27, 97, 196);
            btnConfirmar.ForeColor = Color.Yellow;

        }

        private void btnConfirmar_MouseLeave(object sender, EventArgs e)
        {
            btnConfirmar.BackColor = Color.Transparent;
            btnConfirmar.ForeColor = Color.FromArgb(66, 124, 206);


        }

        private void btnRemove_MouseEnter(object sender, EventArgs e)
        {
            btnRemove.ForeColor = Color.Red;
        }

        private void btnRemove_MouseLeave(object sender, EventArgs e)
        {
            btnRemove.ForeColor = Color.FromArgb(66, 124, 206);
        }
    }
}
