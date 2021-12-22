﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Music_Player
{
    public partial class Settings : Form
    {
        public event EventHandler DataAvailable;

        int valueNumber;

        public int number
        {
            get { return valueNumber; }
            set { valueNumber = value; }
        }

        protected virtual void OnDataAvailable(EventArgs e)
        {
            EventHandler eh = DataAvailable;
            if (eh != null)
            {
                eh(this, e);
            }
        }

        public Settings()
        {
            InitializeComponent();

            btnConfirm.Enabled = false;

            string[] numbers = {"1", "5", "10", "15", "20"};

            cmbSelectNumber.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSelectNumber.Items.AddRange(numbers);
            lblDisplayValid.Text = "Select Volume you\nwant your music to\nminus or plus by";
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            valueNumber = Convert.ToInt32(cmbSelectNumber.SelectedItem.ToString());
            OnDataAvailable(e);
            this.Close();
        }

        private void cmbSelectNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnConfirm.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }
    }
}
