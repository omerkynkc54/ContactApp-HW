using CetContact.Model;

namespace CetContact.Views;

public partial class ContactsPage : ContentPage
{
    ContactRepository contactRepository;
    List<ContactInfo> allContacts;
    public ContactsPage()
	{
		InitializeComponent();
		contactRepository = new ContactRepository();
		
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        allContacts = await contactRepository.GetAllContacts();
        ContactsList.ItemsSource = allContacts;

    }
    private void AddContactButton_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync(nameof(AddContactPage));
       
    }
    private async void OnDeleteContactClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var contact = button?.BindingContext as ContactInfo;

        if (contact != null)
        {
            var answer = await DisplayAlert("Emin misiniz?", $"{contact.Name} kişisini silmek istediğinize emin misiniz?", "Evet", "Hayır");
            if (answer)
            {
                await contactRepository.DeleteContact(contact.Id);
                // Refresh contacts list
                ContactsList.ItemsSource = await contactRepository.GetAllContacts();
            }
        }
    }


    private void ContactsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		if(ContactsList.SelectedItem != null)
		{
            
			ContactInfo selectedContact= ContactsList.SelectedItem as ContactInfo;
            int selectedID= selectedContact.Id; 

            Shell.Current.GoToAsync($"{nameof(EditContactPage)}?id={selectedID}"); //EditContactPage?id=2

            ContactsList.SelectedItem = null;


        }
    }
    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = e.NewTextValue ?? string.Empty;

        if (string.IsNullOrWhiteSpace(searchText))
        {
            ContactsList.ItemsSource = allContacts;
        }
        else
        {
            ContactsList.ItemsSource = allContacts
                .Where(c => (c.Name?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (c.Email?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0))
                .ToList();
        }
    }


}