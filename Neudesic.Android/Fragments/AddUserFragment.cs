using System;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Neudesic.Core.Models;
using Neudesic.Core.Services;

namespace Neudesic.Android.Fragments
{
    public class AddUserFragment : BaseFragment
    {
        /// <summary>
        /// The fragment listener.
        /// </summary>
        private IFragmentListener fragmentListener;

        /// <summary>
        /// The first name.
        /// </summary>
        private TextInputEditText firstName;

        /// <summary>
        /// The first name layout.
        /// </summary>
        private TextInputLayout firstNameLayout;

        /// <summary>
        /// The last name.
        /// </summary>
        private TextInputEditText lastName;

        /// <summary>
        /// The last name layout.
        /// </summary>
        private TextInputLayout lastNameLayout;

        /// <summary>
        /// The password.
        /// </summary>
        private TextInputEditText password;

        /// <summary>
        /// The password layout.
        /// </summary>
        private TextInputLayout passwordLayout;

        /// <summary>
        /// The confirm password.
        /// </summary>
        private TextInputEditText confirmPassword;

        /// <summary>
        /// The confirm password layout.
        /// </summary>
        private TextInputLayout confirmPasswordLayout;

        /// <summary>
        /// The username.
        /// </summary>
        private TextInputEditText username;

        /// <summary>
        /// The username layout.
        /// </summary>
        private TextInputLayout usernameLayout;

        /// <summary>
        /// The password requirements text.
        /// </summary>
        private TextView passwordRequirementsText;

        /// <summary>
        /// The save user.
        /// </summary>
        private Button saveUser;

        /// <summary>
        /// Ons the activity created.
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            SetViews();
            BindEventHandlers();
        }

        /// <summary>
        /// The on attach override
        /// </summary>
        /// <param name="context">Context.</param>
        public override void OnAttach(Context context)
        {
            this.fragmentListener = (IFragmentListener)context;
            base.OnAttach(context);
        }

        /// <summary>
        /// Ons the create view.
        /// </summary>
        /// <returns>The create view.</returns>
        /// <param name="inflater">Inflater.</param>
        /// <param name="container">Container.</param>
        /// <param name="savedInstanceState">Saved instance state.</param>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.AddUserFragment, container, false);
        }

        /// <summary>
        /// Sets the views.
        /// </summary>
        private void SetViews() {
            this.firstName = this.View.FindViewById<TextInputEditText>(Resource.Id.firstName);
            this.firstNameLayout = this.View.FindViewById<TextInputLayout>(Resource.Id.firstNameLayout);
            this.lastName = this.View.FindViewById<TextInputEditText>(Resource.Id.lastName);
            this.lastNameLayout = this.View.FindViewById<TextInputLayout>(Resource.Id.lastNameLayout);
            this.username = this.View.FindViewById<TextInputEditText>(Resource.Id.username);
            this.usernameLayout = this.View.FindViewById<TextInputLayout>(Resource.Id.usernameLayout);
            this.password = this.View.FindViewById<TextInputEditText>(Resource.Id.password);
            this.passwordLayout = this.View.FindViewById<TextInputLayout>(Resource.Id.passwordLayout);
            this.confirmPassword = this.View.FindViewById<TextInputEditText>(Resource.Id.confirmPassword);
            this.confirmPasswordLayout = this.View.FindViewById<TextInputLayout>(Resource.Id.confirmPasswordLayout);
            this.saveUser = this.View.FindViewById<Button>(Resource.Id.saveUserButton);
            this.passwordRequirementsText = this.View.FindViewById<TextView>(Resource.Id.passwordRequirementsText);
        }

        /// <summary>
        /// Binds the event handlers.
        /// </summary>
        private void BindEventHandlers()
        {
            this.password.FocusChange += Password_OnFocusChange;
            this.saveUser.Click += SaveUser_ButtonClick;
        }

        /// <summary>
        /// Sets the text color on the password help text
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">FocuseChangeEventArgs e</param>
        private void Password_OnFocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (e.HasFocus)
            {
                var colorList = this.Context.GetColorStateList(Resource.Color.accent);
                this.passwordRequirementsText.SetTextColor(colorList);
            }
            else
            {
                var colorList = this.Context.GetColorStateList(Resource.Color.primary_text);
                this.passwordRequirementsText.SetTextColor(colorList);
            }
        }

        /// <summary>
        /// Saves the user button click.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">The Event Args</param>
        protected async void SaveUser_ButtonClick(object sender, EventArgs e)
        {
            if (ViewIsValid()) {
                var user = new User
                {
                    FirstName = this.firstName.Text.Trim(),
                    LastName = this.lastName.Text.Trim(),
                    Username = this.username.Text.Trim(),
                    Password = this.password.Text.Trim()
                };

                await this.userRepository.CreateAsync(user);
                this.fragmentListener.SetFragment(Resource.Id.menu_list);
            }
        }

        /// <summary>
        /// Validates the View Data
        /// </summary>
        /// <returns><c>true</c>, if is view is valid <c>false</c> otherwise.</returns>
        private bool ViewIsValid() {
            var isValid = true;
            if (string.IsNullOrWhiteSpace(this.username.Text))
            {
                usernameLayout.ErrorEnabled = true;
                username.Error = GetString(Resource.String.username_error);
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(this.firstName.Text))
            {
                firstNameLayout.ErrorEnabled = true;
                firstName.Error = GetString(Resource.String.first_name_error);
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(this.lastName.Text))
            {
                lastNameLayout.ErrorEnabled = true;
                lastName.Error = GetString(Resource.String.last_name_error);
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(this.password.Text)) {
                passwordLayout.ErrorEnabled = true;
                password.Error = GetString(Resource.String.password_error);
                isValid = false;
            }
            else {
                var passwordText = this.password.Text.Trim();
                if (passwordText != this.confirmPassword.Text.Trim())
                {
                    confirmPasswordLayout.ErrorEnabled = true;
                    confirmPassword.Error = GetString(Resource.String.mismatch_password_error);
                    isValid = false;
                }

                var validationResult = SecurityService.ValidatePasswordRules(passwordText);
                if (validationResult != SecurityService.ValidationResult.valid)
                {
                    isValid = false;
                    passwordLayout.ErrorEnabled = true;
                    switch (validationResult) {
                        case SecurityService.ValidationResult.length:
                            passwordLayout.Error = GetString(Resource.String.password_length_error);
                            break;
                        case SecurityService.ValidationResult.alphaNumeric:
                            passwordLayout.Error = GetString(Resource.String.password_alpha_numeric_error);
                            break;
                        case SecurityService.ValidationResult.sequence:
                            passwordLayout.Error = GetString(Resource.String.password_sequence_error);
                            break;
                    }
                }
            }

            return isValid;
        }
    }
}