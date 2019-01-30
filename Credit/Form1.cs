using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;

namespace Credit
{
    public partial class Form1 : Form
    {

        double CapitalRembourse, salaire, InteretMens, Rembours, TauxM, taux, ResuM, PrixTotalCredit, ResuTot1, duree, montant, dureeCalc, test = 0.0;

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ouvrir = new OpenFileDialog();
            if (ouvrir.ShowDialog() == DialogResult.OK)
            {
                ouvrir.OpenFile();
            }
        }

        private void sauverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextWriter sauvegarde = new StreamWriter(@"F:\Sauvegarde.txt");
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int l = 0; l < dataGridView1.Columns.Count; l++)
                {
                    sauvegarde.Write("\t" + dataGridView1.Rows[i].Cells[l].Value.ToString() + "\t" + "|");
                }
                sauvegarde.WriteLine("");
                sauvegarde.WriteLine("---------------------------------------------------------------------------------------------------------------");
            }
            sauvegarde.Close();
            MessageBox.Show("Données Saved");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            duree = double.Parse(comboBox1.SelectedItem.ToString());
            montant = double.Parse(textBox1.Text);
            salaire = double.Parse(textBox2.Text);
            duree *= 12;
            TauxM = (taux / 100) / 12;
            if (testSalaire(salaire, ResuM) == false)
                MessageBox.Show("Vous ne gagnez pas assez d'argent par mois pour la durée de votre crédit");
            else
            {
                chart1.Visible = true;
                for (int i = 0; i < duree; i++)
                {
                    InteretMens = montant * TauxM; //interet 

                    CapitalRembourse = ResuM - InteretMens;  //Capital Rembourse
                    montant = (montant - ResuM) + InteretMens; //Nouveau Montant

                    CapitalRembourse = Math.Round(CapitalRembourse, 2);//Arrondi 10-2
                    InteretMens = Math.Round(InteretMens, 2);//Arrondi 10-2
                    montant = Math.Round(montant, 2); //Arrondi 10-2
                    InteretMens = montant * TauxM; //interet 
                    chart1.Series["montant"].Points.AddXY(i, CapitalRembourse);
                    chart1.Series["interet"].Points.AddXY(i, InteretMens);
                }
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public Form1()
        {
            InitializeComponent();

            for (int i = 1; i < 31; i++)
            {
                comboBox1.Items.Add(i);
            }
            textBox2.PasswordChar = '*';
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            duree = double.Parse(comboBox1.SelectedItem.ToString());
            montant = double.Parse(textBox1.Text);
            taux = double.Parse(textBox3.Text);
            salaire = double.Parse(textBox2.Text);
            duree *= 12;
            dureeCalc = duree * -1;
            TauxM = (taux / 100) / 12;
            ResuM = (montant * TauxM) / (1 - Math.Pow(1 + TauxM, dureeCalc));
            ResuM = Math.Round(ResuM, 2);
            PrixTotalCredit = ResuM * duree;
            string ResultatMensu = ResuM.ToString();
            string PrixTotalCreditStr = PrixTotalCredit.ToString();
            if (testSalaire(salaire, ResuM) == false)
                MessageBox.Show("Vous ne gagnez pas assez d'argent par mois pour la durée de votre crédit");
            else
            {
                label4.Text = "Montant de la mensualité : " + ResultatMensu + " € ";
                label5.Text = "Coût total de ce crédit : " + PrixTotalCreditStr + " €";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
            string id, ResultatTotal, ResultatMensu, interetMensStr, RemboursementStr = "";
            duree = double.Parse(comboBox1.SelectedItem.ToString());
            montant = double.Parse(textBox1.Text);
            taux = double.Parse(textBox3.Text);
            salaire = double.Parse(textBox2.Text);
            duree *= 12;
            TauxM = (taux / 100) / 12;
            ResuM = (montant * TauxM) / (1 - Math.Pow(1 + TauxM, dureeCalc)); //Calcul de la mensualite
            ResuM = Math.Round(ResuM, 2);//Arrondi 10-2
            PrixTotalCredit = ResuM * duree;//Calcul du prix de reviens du credit

            ResultatMensu = ResuM.ToString();//Conversion Double to String
            ResultatTotal = PrixTotalCredit.ToString();//Conversion Double to String

            if (testSalaire(salaire, ResuM) == false)
                MessageBox.Show("Vous ne gagnez pas assez d'argent par mois pour la durée de votre crédit");
            else
            {
                dataGridView1.Visible = true;
                for (int i = 2; i < duree + 1; i++)
                {

                    id = i.ToString(); //Numero du mois
                    InteretMens = montant * TauxM; //interet 

                    CapitalRembourse = ResuM - InteretMens;  //Capital Rembourse
                    montant = (montant - ResuM) + InteretMens; //Nouveau Montant

                    CapitalRembourse = Math.Round(CapitalRembourse, 2);//Arrondi 10-2
                    InteretMens = Math.Round(InteretMens, 2);//Arrondi 10-2
                    montant = Math.Round(montant, 2); //Arrondi 10-2

                    string MontantRestant = montant.ToString(); //Conversion Double to String
                    interetMensStr = InteretMens.ToString();//Conversion Double to String
                    RemboursementStr = CapitalRembourse.ToString();//Conversion Double to String

                    dataGridView1.Rows.Add(id, ResultatMensu, RemboursementStr, interetMensStr, MontantRestant); //Ajout dans le tableau
                }
            }
        }

        private Boolean testSalaire(double sal, double resu)
        {
            if (sal / 3 < resu)
                return false;
            return true;
        }
    }
}

