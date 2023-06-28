using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;
using Common;
using System.Text.RegularExpressions;

namespace KorisnickiInterfejs
{
    public partial class FrmLogin : Form
    {
        public Matrica matrica;

        public FrmLogin()
        {
            InitializeComponent();
            InitializeComboBox();
            Communication.Instance.Connect();
        }

        private void InitializeComboBox()
        {
            cbKategorija.Items.Add(new Matrica
            {
                Naziv = "3x3",
                xDimension = 3,
                yDimension = 3,
                BrojPokusaja = 5
            });
            cbKategorija.Items.Add(new Matrica
            {
                Naziv = "4x4",
                xDimension = 4,
                yDimension = 4,
                BrojPokusaja = 9
            });
            cbKategorija.Items.Add(new Matrica
            {
                Naziv = "5x5",
                xDimension = 5,
                yDimension = 5,
                BrojPokusaja = 13
            });
            cbKategorija.DisplayMember = "Naziv";
            cbKategorija.ValueMember = "Self";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validacija())
                {
                    matrica = (Matrica)cbKategorija.SelectedItem;
                    User user = new User
                    {
                        Email = txtEmail.Text,
                        Password = txtPassword.Text,
                        Matrica = matrica
                    };
                    Poruka poruka = new Poruka
                    {
                        Operations = Operations.Login,
                        PorukaObject = user
                    };
                    Communication.Instance.SendMessage(poruka);
                    poruka = Communication.Instance.ReadMessage<Poruka>();
                    if (poruka.isSuccessful)
                        DialogResult = DialogResult.OK;
                    else if(poruka.MessageText != null)
                        MessageBox.Show(poruka.MessageText);

                }
                else
                {
                    MessageBox.Show("Greska");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool Validacija()
        {
            if (cbKategorija.SelectedItem == null)
                return false;
            if (!Regex.IsMatch(txtEmail.Text, @"^[1-9a-zA-Z]+\@{1}[1-9a-zA-Z]+"))
                return false;
            if (!Regex.IsMatch(txtPassword.Text, @"^\d([1-9a-zA-Z]*_)+"))
                return false;
            return true;
        }
    }
}
