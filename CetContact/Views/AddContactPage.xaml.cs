using CetContact.Model;

namespace CetContact.Views;

public partial class AddContactPage : ContentPage
{
    ContactRepository contactRepository;
	public AddContactPage()
	{
		InitializeComponent();
        contactRepository = new ContactRepository();
	}

    private void BackButton_Clicked(object sender, EventArgs e)
    {
		//Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
        //Shell.Current.GoToAsync("//"+nameof(ContactsPage));
       
        Shell.Current.GoToAsync("..");

    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text;
        string email = EmailEntry.Text;
        string phone = PhoneEntry.Text;
        string address = AdressEntry.Text;

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Dikkat!", "Ad ve E-Posta kýsmý boþ olamaz.", "OK");
            return;
        }

        ContactInfo contact = new ContactInfo
        {
            Name = name,
            Phone = phone,
            Address = address,
            Email = email
        };
        await contactRepository.AddContact(contact);
        await Shell.Current.GoToAsync("..");
    }
}