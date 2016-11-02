using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using CatchUp.Core.Interfaces;
using CatchUp.Core.Database;
using System.Diagnostics;
using MvvmCross.Plugins.Validation;

// Author: Andreas Andersson n9795383, Marie-Luise Lux n9530801, Samuel Blight n8312885

namespace CatchUp.Core.ViewModels
{
	public class CreateUserViewModel
		: MvxViewModel
	{

		private IUserStorageDatabase dbLocal;
		private IUserDatabase dbAzure;

		private string firstname = "";
		public string Firstname
		{
			get { return firstname; }
			set
			{
				if (value != null && value != firstname)
				{
					firstname = value;
				}
			}
		}

		private string lastname = "";
		public string Lastname
		{
			get { return lastname; }
			set
			{
				if (value != null && value != lastname)
				{
					lastname = value;
				}
			}
		}


		private string email = "";
		public string Email
		{
			get { return email; }
			set
			{
				if (value != null && value != email)
				{
					email = value;
				}
			}
		}


		public ICommand BtnUserCommand { get; private set; }
		IMvxToastService toastService;
		IValidator validator;
		IDialogService dialog;


		public CreateUserViewModel(IDialogService dialog, IUserStorageDatabase dbLocal, IUserDatabase dbAzure,
									IValidator validator, IMvxToastService toastService)
		{
			this.dialog = dialog;
			this.dbLocal = dbLocal;
			this.dbAzure = dbAzure;
			this.toastService = toastService;
			this.validator = validator;
			Debug.WriteLine("Test:");

			BtnUserCommand = new MvxCommand(async () =>
		   {
			   Debug.WriteLine("Test2:");
			   //Create new user with all variables. 
			   if (await dbAzure.CheckIfExists(email))  //check if user exists
			   {
                   int button = await dialog.Show("The Email adress you are trying to use is already registered!", "Error", "OK");
               }
               else if(email.Length < 1 || firstname.Length < 1 || lastname.Length < 1)   //check of fields are filled
               {
                   int button = await dialog.Show("Please fill  in all the fields!", "Error", "OK");
               }
               else if (!email.Contains("@"))  // check if mail contains @
               {
                   int button = await dialog.Show("Please enter a valid email adress!", "Error", "OK");
               }
               else {
				   await dbAzure.CreateUser(Email, Firstname, Lastname);
				   await dbLocal.CreateUser(Email, Firstname, Lastname);
				   Debug.WriteLine("User created with email:" + email);
				   toastService.DisplayMessage("User created.");
				   ShowViewModel<HomeViewModel>();
			   }
		   });
		}
	}
}

