using MyISolver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace MyBackpackForm
{
    public partial class Backpack : Form
    {
        ISolver MySolver = null;
        bool f = false;
        int M, n_1, n_2;
        int[] mWeight, mCost;
        public Backpack()
        {
            InitializeComponent();
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            string[] s = richTextBox1.Text.Split(' ');
            s = s.Where(val => val != "").ToArray();
            n_1 = s.Count();
            mWeight = new int[n_1];
            for (int i = 0; i < n_1; i++) mWeight[i] = Convert.ToInt32(s[i]);
            if (n_1 == n_2 && f) button_Get_list.Enabled = true;
            else button_Get_list.Enabled = false;
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            string[] s = richTextBox2.Text.Split(' ');
            s = s.Where(val => val != "").ToArray();
            n_2 = s.Count();
            mCost = new int[n_2];
            for (int i = 0; i < n_2; i++) mCost[i] = Convert.ToInt32(s[i]);
            if (n_1 == n_2 && f) button_Get_list.Enabled = true;
            else button_Get_list.Enabled = false;
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                M = Convert.ToInt32(richTextBox3.Text);
                f = true;
                if (n_1 == n_2) button_Get_list.Enabled = true;
                else button_Get_list.Enabled = false;
            }
            catch
            {
                button_Get_list.Enabled = false; f = false;
            }
        }
        private void button_dlg_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.Cancel) return;
            var AllTypes = Assembly.LoadFrom(dlg.FileName).GetTypes();
            foreach (var t in AllTypes)
            {
                if (t.GetInterface("ISolver") == null) continue;
                foreach (var c  in t.GetConstructors())
                {
                    if (c.GetParameters().Length > 0) continue;
                    MySolver = c.Invoke(new object[0]) as ISolver;
                    if (MySolver != null)
                    {
                        dlg_lbl.Text = MySolver.GetName(); return;
                    }
                }
            }
        }
        private void button_Get_list_Click(object sender, EventArgs e)
        {
            if (MySolver == null)
            {
                MessageBox.Show("Алгоритм не выбран"); return;
            }
            int[] res = MySolver.Solve(M, mWeight, mCost); string str ="";
            int temp;
            for (int i = 0; i < res.Length - 1; i++)
            {
                for (int j = i + 1; j < res.Length; j++)
                {
                    if (res[i] > res[j])
                    {
                        temp = res[i];
                        res[i] = res[j];
                        res[j] = temp;
                    }
                }
            }
            for (int i=0;i<res.Length;i++)
            {
                str += res[i]; str += " ";
                richTextBox1.Select(res[i] * 2, 1);
                richTextBox1.SelectionColor = Color.Red;
                richTextBox2.Select(res[i] * 2, 1);
                richTextBox2.SelectionColor = Color.Red;
            }
            res_lbl.Text = str;
        }        
    }
}
