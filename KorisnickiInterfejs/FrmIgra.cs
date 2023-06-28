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
using System.Threading;

namespace KorisnickiInterfejs
{
    public partial class FrmIgra : Form
    {
        public FrmIgra(Matrica matrica)
        {
            InitializeComponent();
            InitializeGridView(matrica);
            InitializeListener();
        }

        private void InitializeListener()
        {
            try
            {
                Thread nitPoruke = new Thread(CitajPoruke);
                nitPoruke.IsBackground = true;
                nitPoruke.Start();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CitajPoruke()
        {
            try
            {
                while (true)
                {
                    Poruka poruka = Communication.Instance.ReadMessage<Poruka>();
                    switch (poruka.Operations)
                    {
                        case Operations.Igra:
                            if (poruka.isSuccessful)
                            {
                                string znak = ((Igra)poruka.PorukaObject).Vrijednost;
                                Invoke(new Action(() => dgvIgra.EditMode = DataGridViewEditMode.EditOnEnter));
                                Invoke(new Action(() => dgvIgra[((Igra)poruka.PorukaObject).xDimension, ((Igra)poruka.PorukaObject).yDimension].Value = znak));
                                Invoke(new Action(() => dgvIgra.RefreshEdit()));
                            }
                            if(poruka.MessageText != null)
                            {
                                MessageBox.Show(poruka.MessageText);
                                Environment.Exit(0);
                            }
                            break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void InitializeGridView(Matrica matrica)
        {
            dgvIgra.DataSource = null;
            dgvIgra.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvIgra.DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
            dgvIgra.ColumnCount = matrica.xDimension;
            for (int i = 0; i < matrica.xDimension ; i++)
            {
                dgvIgra.Columns[i].Width = 40;
            }
            for (int i = 0; i < matrica.yDimension ; i++)
            {
                dgvIgra.Rows.Add();
            }
        }

        private void dgvIgra_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvIgra[e.ColumnIndex,e.RowIndex].Value == null)
            {
                Poruka poruka = new Poruka
                {
                    Operations = Operations.Igra,
                    PorukaObject = new Igra
                    {
                        xDimension = e.ColumnIndex,
                        yDimension = e.RowIndex
                    }
                };
                Communication.Instance.SendMessage(poruka);
            }
        }
    }
}
