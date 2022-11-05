using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UserStory4Tours.models;

namespace UserStory4Tours
{
    public partial class Form1 : Form
    {
        public int Key = 0;
        private decimal sum = 0;
        public DbContextOptions<ApplicationContext> options;
        public Form1()
        {
            InitializeComponent(); 
            ToursGridViev.AutoGenerateColumns = false;
            options = DataBaseHelper.Option();

            ToursGridViev.DataSource = ReadDB(options);
            CalculatScroll();
        }
       

        #region InfoButton
        private void Info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Николаев В.А ИП-20-3", "Горящие туры 4 вариант",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region AddButton
        private void AddTool_Click(object sender, EventArgs e)
        {
            var infoForm = new ToursInfoForm();
            
            if (infoForm.ShowDialog(this) == DialogResult.OK)
            {
                CreateDB(options, infoForm);
                CalculatScroll();  
                ToursGridViev.DataSource = ReadDB(options);  
            }
            
        }
        #endregion
        #region DeleteButton
        private void DeliteTool_Click(object sender, EventArgs e)
        {
            var id = (Tours)ToursGridViev.Rows[ToursGridViev.SelectedRows[0].Index].DataBoundItem;
            if(MessageBox.Show($"Вы действительно хотите удалить\n\rId:{id.Id}\n\rПуть:{id.direction}" +
                $"\n\rДата вылета:{id.DateDeparture:D}","Удаление записи", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                RemoveDB(options, id);           
                CalculatScroll();
                ToursGridViev.DataSource = ReadDB(options);
                
            }
        }
        #endregion
        #region ChangeButton
        private void ChangeTool_Click(object sender, EventArgs e)
        {
            var id = (Tours)ToursGridViev.Rows[ToursGridViev.SelectedRows[0].Index].DataBoundItem;
            var infoForm = new ToursInfoForm(id);
            if (infoForm.ShowDialog(this) == DialogResult.OK)
            {
                id.direction = infoForm.Tours.direction;
                id.DateDeparture = infoForm.Tours.DateDeparture;
                id.NumberNight = infoForm.Tours.NumberNight;
                id.CostVac = infoForm.Tours.CostVac;
                id.NumberVac = infoForm.Tours.NumberVac;
                id.Wi_Fi = infoForm.Tours.Wi_Fi;
                id.Surcharges = infoForm.Tours.Surcharges;

                UpdateDB(options, id);
                CalculatScroll();
                ToursGridViev.DataSource = ReadDB(options);



            }
        }
        #endregion

        #region CRUD_DB
        private static void CreateDB(DbContextOptions<ApplicationContext> options, ToursInfoForm form)
        {
            using (var db = new ApplicationContext(options))
            {
                Tours t = new Tours();
                form.Tours.Id = t.Id;
                db.ToursDB.Add(form.Tours);
                db.SaveChanges();
            }
        }
        private static void RemoveDB(DbContextOptions<ApplicationContext> options,Tours tours)
        {
            using (var db = new ApplicationContext(options))
            {
                var tourse = db.ToursDB.FirstOrDefault(u => u.Id == tours.Id);
                if (tourse != null)
                {
                    db.ToursDB.Remove(tourse); 
                    db.SaveChanges();
                }
               
            }
        }
        private static void UpdateDB(DbContextOptions<ApplicationContext> options, Tours tours)
        {
            using (var db = new ApplicationContext(options))
            {
                var tourse = db.ToursDB.FirstOrDefault(u => u.Id == tours.Id);
                if (tourse != null)
                {
                    tourse.direction = tours.direction;
                    tourse.DateDeparture = tours.DateDeparture;
                    tourse.NumberNight = tours.NumberNight;
                    tourse.CostVac = tours.CostVac;
                    tourse.NumberVac = tours.NumberVac;
                    tourse.Wi_Fi = tours.Wi_Fi;
                    tourse.Surcharges = tours.Surcharges;
                    db.SaveChanges();
                }
            }
        }
        private static List<Tours> ReadDB(DbContextOptions<ApplicationContext> options)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                return db.ToursDB
                    .OrderByDescending(x => x.Id)
                    .ToList();
            }
        }
        #endregion

        #region ButtonMenu
        private void AddMenu_Click(object sender, EventArgs e)
        {
            AddTool_Click(sender, e);
        }

        private void DeliteMenu_Click(object sender, EventArgs e)
        {
            DeliteTool_Click(sender, e);
        }

        private void ChangeMenu_Click(object sender, EventArgs e)
        {
            ChangeTool_Click(sender, e);
        }
        private void DeleteAllMenu_Click(object sender, EventArgs e)
        {
            foreach (var c in ReadDB(options))
            {
                RemoveDB(options, c);
            }
            CalculatScroll();
            ToursGridViev.DataSource = ReadDB(options);
            
        }
        #endregion

        #region CellFormating
        private void ToursGridViev_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (ToursGridViev.Columns[e.ColumnIndex].Name == "AmountAllColumn")
            {
                var data = (Tours)ToursGridViev.Rows[e.RowIndex].DataBoundItem;
                sum += (data.NumberNight * data.NumberVac * data.CostVac) + data.Surcharges;
                e.Value = sum;
                sum = 0;
            }

            if (ToursGridViev.Columns[e.ColumnIndex].Name == "DirectColumn")
            {
                var val = (Direction)e.Value;
                switch (val)
                {
                    case Direction.Turkey:
                        e.Value = "Турция";
                        break;
                    case Direction.Israel:
                        e.Value = "Израиль";
                        break;
                    case Direction.Abkhazia:
                        e.Value = "Абхазия";
                        break;
                    case Direction.Cyprus:
                        e.Value = "Кипр";
                        break;
                    case Direction.Shushary:
                        e.Value = "Шушары";
                        break;
                    case Direction.Thailand:
                        e.Value = "Таиланд";
                        break;
                }
            }
            if (ToursGridViev.Columns[e.ColumnIndex].Name == "Wi_FiColumn")
            {
                var val = (bool)e.Value;
                switch (val)
                {
                    case true:
                        e.Value = "Есть";
                        break;
                    case false:
                        e.Value = "Нет";
                        break;
                   
                }
            }
        }
        #endregion

        #region EnableButyon
        private void ToursGridViev_SelectionChanged(object sender, EventArgs e)
        {
            DeliteMenu.Enabled =
            ChangeMenu.Enabled =
            DeliteTool.Enabled = 
            ChangeTool.Enabled = 
            ToursGridViev.SelectedRows.Count > 0;
            DeleteAllMenu.Enabled = ReadDB(options).Count != 0;
        }
        #endregion

        #region CalculatScrollStatusBar
        private void CalculatScroll()
        {
            NumberToursStatus.Text = $"Всего туров {ReadDB(options).Count}";
            var sumAll = ReadDB(options).Sum(x => (x.NumberNight * x.NumberVac * x.CostVac) + x.Surcharges);
            var SurCount = ReadDB(options).Where(x => x.Surcharges != 0).Count();
            TotalAmount.Text = $"Общая сумма {sumAll}";
            NumerToursSurcharges.Text = $"Кол-во туров с доплатами {SurCount}";
            var sumSur = ReadDB(options).Sum(x => x.Surcharges);
            TotalAmountSurcharges.Text = $"Общая сумма доплат {sumSur}";
        }

        #endregion

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
