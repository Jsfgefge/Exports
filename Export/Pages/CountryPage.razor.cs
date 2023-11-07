using Export.Data;
using Export.Shared;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Diagrams;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Popups;
using Syncfusion.Blazor.Schedule.Internal;

namespace Export.Pages {
    public partial class CountryPage : ComponentBase {
        [Inject]
        ICountryService CountryService { get; set; }

        IEnumerable<Country> country;
        SfDialog DialogAddEditCountry;
        WarningPage Warning;
        string WarningHeaderMessage = "";
        string WarningContentMessage = "";
        Country addeditCountry = new Country();
        string HeaderText = "";

        private List<ItemModel> Toolbaritems = new List<ItemModel>();
        public int SelectedCountryID { get; set; } = 0;
        protected override async Task OnInitializedAsync() {
            country = await CountryService.CountryList();

            Toolbaritems.Add(new ItemModel() { Text = "Add", TooltipText = "Add a new Country", PrefixIcon = "e-add" });
            Toolbaritems.Add(new ItemModel() { Text = "Edit", TooltipText = "Edit a Country", PrefixIcon = "e-edit" });
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args) {
            if (args.Item.Text == "Add") {
                addeditCountry = new Country();
                HeaderText = "Add Country";
                await this.DialogAddEditCountry.Show();
            }
            if (args.Item.Text == "Edit") {

                if (SelectedCountryID == 0) {

                    WarningHeaderMessage = "Warning";
                    WarningContentMessage = "Please select a Country from the grid.";
                    Warning.OpenDialog();

                }
                else {
                    HeaderText = "Edit Tax Rate";
                    addeditCountry = await CountryService.Country_GetOne(SelectedCountryID);
                    await this.DialogAddEditCountry.ShowAsync();
                }
            }
            //if (args.Item.Text == "Delete") {

            //}
        }

        protected async Task CountrySave() {
            if (addeditCountry.CountryID == 0) {
                int Success = await CountryService.CountryInsert(addeditCountry.CountryName, addeditCountry.CountryISO);
                if (Success != 0) {
                    WarningHeaderMessage = "Warning!";
                    WarningContentMessage = "This Country Name already exists; it cannot be added again.";
                    Warning.OpenDialog();
                    // Data is left in the dialog so the user can see the problem.
                }
                else {
                    addeditCountry = new Country();
                }
            }
            else {
                //Country is being edited
                int Success = await CountryService.CountryUpdate(addeditCountry.CountryName, addeditCountry.CountryISO, addeditCountry.CountryID);
                if(Success != 0) {
                    WarningHeaderMessage = "Warning";
                    WarningContentMessage = "This Country already exists; it cannot be added again.";
                    Warning.OpenDialog();
                }
                else {
                    await this.DialogAddEditCountry.HideAsync();
                    this.StateHasChanged();
                    addeditCountry = new Country();
                    SelectedCountryID = 0;
                }
            }
            addeditCountry = new Country();
            country = await CountryService.CountryList();
            StateHasChanged();
        }
        public void RowSelectHandler(RowSelectEventArgs<Country> args) {
            SelectedCountryID = args.Data.CountryID;
        }

        private async Task CloseDialog() {
            await this.DialogAddEditCountry.HideAsync();
        }
    }
}
