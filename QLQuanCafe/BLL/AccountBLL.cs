using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Input;
using MySql.Data.MySqlClient;
using QLQuanCafe.Common;
using QLQuanCafe.DAO;
using QLQuanCafe.DTO;
using QLQuanCafe.GUI;
using QLQuanCafe.GUI.Dialog;

namespace QLQuanCafe.BLL
{
    public class AccountBll : BllBase
    {

        #region Properties

        private List<AccountData> _listAccount;
        public List<AccountData> ListAccount
        {
            get
            {
                Load();
                return _listAccount;
            }
            set { SetProperty(ref _listAccount, value); }
        }

        private AccountData _accountSelected;
        public AccountData AccountSelected
        {
            get { return _accountSelected; }
            set { SetProperty(ref _accountSelected, value); }
        }

        private AccountData _accountToSave;
        public AccountData AccountToSave
        {
            get { return _accountToSave; }
            set { SetProperty(ref _accountToSave, value); }
        }

        private List<PermissionData> _listPermission;
        public List<PermissionData> ListPermission
        {
            get
            {
                Load();
                return _listPermission;
            }
            set { SetProperty(ref _listPermission, value); }
        }

        private PermissionData _permissionSelected;
        public PermissionData PermissionSelected
        {
            get { return _permissionSelected; }
            set { SetProperty(ref _permissionSelected, value); }
        }

        #endregion

        #region Commands

        private ICommand _selectAccountCommand;
        public ICommand SelectAccountCommand
        {
            get
            {
                if (_selectAccountCommand == null)
                    _selectAccountCommand = new RelayCommand<object>(
                        p => SelectAccountAction(p),
                        p => true);
                return _selectAccountCommand;
            }
            set { _selectAccountCommand = value; }
        }

        private ICommand _showAddAccountWindowCommand;
        public ICommand ShowAddAccountWindowCommand
        {
            get
            {
                if (_showAddAccountWindowCommand == null)
                    _showAddAccountWindowCommand = new RelayCommand<object>(
                        p =>
                        {
                            AccountToSave = new AccountData();

                            ListPermission = LocatorDataSource.AccountDS.GetAllPermission();
//                            PermissionSelected = null;

                            //HACK View
                            ThemTaiKhoan themTaiKhoan = new ThemTaiKhoan(); 
                            themTaiKhoan.ShowDialog();
                        },
                        p => true);
                return _showAddAccountWindowCommand;
            }
            set { _showAddAccountWindowCommand = value; }
        }


        private ICommand _deleteAccountCommand;
        public ICommand DeleteAccountCommand
        {
            get
            {
                if (_deleteAccountCommand == null)
                    _deleteAccountCommand = new RelayCommand<object>(
                        p =>
                        {
                            try
                            {
                                LocatorDataSource.AccountDS.DeleteAccount(AccountSelected);

                                ListAccount = LocatorDataSource.AccountDS.GetAllAccount();
                            }
                            catch (MySqlException ex)
                            {
                                MessageDialogHelper.CreateErrorMessage(ex.Message);
                            }
                        },
                        p => AccountSelected != null);
                return _deleteAccountCommand;
            }
            set { _deleteAccountCommand = value; }
        }

        #endregion

        #region Actions

        private void SelectAccountAction(object obj)
        {
            if (obj != null)
            {
                AccountSelected = obj as AccountData;
            }
        }

        #endregion

        #region Methods

        public override void Load()
        {
            try
            {
                ListAccount = LocatorDataSource.AccountDS.GetAllAccount();

                AccountSelected = null;
            }
            catch (MySqlException ex)
            {
                MessageDialogHelper.CreateErrorMessage(ex.Message);
            }
        }

        public bool AddAccount(string password)
        {
            try
            {
                AccountToSave.Password = password;
//                AccountToSave.Permission = PermissionSelected;
                LocatorDataSource.AccountDS.AddAccount(AccountToSave);

                ListAccount = LocatorDataSource.AccountDS.GetAllAccount();

                return true;
            }
            catch (MySqlException ex)
            {
                MessageDialogHelper.CreateErrorMessage(ex.Message);

                return false;
            }
        }

        #endregion
    }
}
