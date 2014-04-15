using CoinTNet.DO;
using CoinTNet.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using System.ComponentModel;

namespace CoinTNet.UI.Forms
{
    /// <summary>
    /// Form enabling the user to manage news sources
    /// </summary>
    partial class NewsSourcesForm : Form
    {
        #region Private members
        private Tuple<string, int>[] _expiryValues;
        private bool _ignore;

        private BindingSource _bindingSource;

        private CoinTNetContext _dbContext;

        #endregion

        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        public NewsSourcesForm()
        {
            InitializeComponent();
            this.Shown += (s, e) => PopulateNewsSourcesList();


            Tuple<string, NewsSourceType>[] sourceTypes = { Tuple.Create("RSS", NewsSourceType.RSS), Tuple.Create("Twitter", NewsSourceType.Twitter) };

            _expiryValues = new Tuple<string, int>[]  { Tuple.Create("1 hour", 1), Tuple.Create("5 hours", 5), Tuple.Create("1 day", 24)
                                                , Tuple.Create("2 days", 24 * 2), Tuple.Create("5 days", 24 * 5), Tuple.Create("10 days", 24 * 10)};

            cbbSourceType.PopulateCbbFromList(sourceTypes, t => t.Item1, sourceTypes[0]);
            cbbSourceType.SelectedValueChanged += (s, e) => PopulateNewsSourcesList();

            colExpiry.DataSource = _expiryValues;
            colExpiry.DisplayMember = "Item1";
            colExpiry.ValueMember = "Item2";
            dgvNews.AutoGenerateColumns = false;
            dgvNews.RowValidating += dgvNews_RowValidating;
            // dgvNews.DataError += dgvNews_DataError;
        }

        /// <summary>
        /// Checks that all required fiels are filled when the user edits a row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvNews_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (_ignore)
            {
                return;
            }
            bool allCellsOK = true;
            var row = dgvNews.Rows[e.RowIndex];
            NewsSourceType type = cbbSourceType.GetSelectedValue<Tuple<string, NewsSourceType>>().Item2;

            foreach (DataGridViewCell c in row.Cells)
            {
                if (/*c.ColumnIndex == colFilter.Index ||*/ c.ColumnIndex == colRemove.Index)
                {
                    continue;
                }
                if (c.ColumnIndex == colURL.Index && type == NewsSourceType.Twitter)
                {
                    continue;
                }
                if (c.Value == null || c.Value.ToString() == string.Empty)
                {
                    allCellsOK = false;
                    c.ErrorText = "Mandatory";
                }
                else
                {
                    c.ErrorText = string.Empty;
                }
            }
            if (!allCellsOK)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Populates the list of sources
        /// </summary>
        private void PopulateNewsSourcesList()
        {
            _ignore = true;
            dgvNews.SuspendLayout();
            NewsSourceType type = cbbSourceType.GetSelectedValue<Tuple<string, NewsSourceType>>().Item2;
            colURL.Visible = type == NewsSourceType.RSS;
            if (_dbContext != null)
            {
                _dbContext.SaveChanges();
                _dbContext.Dispose();
            }
            _dbContext = new CoinTNetContext();
            _dbContext.NewsSources.Where(ns => ns.Type == (int)type).Load();
            var newsList = _dbContext.NewsSources.Local.ToBindingList();
            _bindingSource = new BindingSource(newsList, null);
            _bindingSource.Filter = "Type == 0";
            dgvNews.DataSource = _bindingSource;
            dgvNews.ResumeLayout();
            _ignore = false;
        }

        /// <summary>
        /// Handles the click on the Delete icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvNews_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == colRemove.Index)
            {
                _bindingSource.Remove(dgvNews.Rows[e.RowIndex].DataBoundItem as NewsSource);
            }
        }

        /// <summary>
        /// The user clicked on the Add button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            _bindingSource.Add(new NewsSource
            {
                ExpiryInHours = 24 * 2,
                NbItems = 5,
                Type = (int)cbbSourceType.GetSelectedValue<Tuple<string, NewsSourceType>>().Item2
            });

            dgvNews.CurrentCell = dgvNews.Rows[dgvNews.Rows.Count - 1].Cells[colName.Index];
            dgvNews.BeginEdit(false);
        }

        /// <summary>
        /// Save changes if the user clicked on the close button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewsSourcesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                _dbContext.SaveChanges();
            }
            _dbContext.Dispose();
        }


    }
}
