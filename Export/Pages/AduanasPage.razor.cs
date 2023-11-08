using Export.Data;
using Export.Shared;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Diagrams;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Popups;
using Syncfusion.Blazor.Schedule.Internal;
using System.Runtime.CompilerServices;

namespace Export.Pages {
    public partial class AduanasPage : ComponentBase{
        [Inject]
        IAduanasService AduanasService { get; set; }

        IEnumerable<Aduanas> aduana;
        public List<ItemModel> Toolbaritems = new List<ItemModel>();


        SfDialog DialogAddEditAduana;
        SfTextBox txtAduana;
        Aduanas addeditAduana = new Aduanas();

        public int SelectedAduanaId { get; set; } = 0;

        WarningPage Warning;
        string WarningHeaderMessage = "";
        string WarningContentMessage = "";
        string HeaderText = "";

        protected override async Task OnInitializedAsync() {
            aduana = await AduanasService.AduanasList();

            Toolbaritems.Add(new ItemModel() { Text = "Add", TooltipText = "Add a new Aduana", PrefixIcon = "e-add" });
            Toolbaritems.Add(new ItemModel() { Text = "Edit", TooltipText = "Edit a Aduana", PrefixIcon = "e-edit" });
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args) {
            if (args.Item.Text == "Add") {
                //Code for adding goes here
                addeditAduana = new Aduanas();             // Ensures a blank form when adding
                HeaderText = "Add Tax Rate";
                await this.DialogAddEditAduana.Show();
            }
            if (args.Item.Text == "Edit") {
                //Code for editing goes here
                //Check that a Tax Rate has been selected
                if (SelectedAduanaId == 0) {
                    WarningHeaderMessage = "Warning!";
                    WarningContentMessage = "Please select a Tax Rate from the grid.";
                    Warning.OpenDialog();
                }
                else {
                    //populate addeditTax (temporary data set used for the editing process)
                    HeaderText = "Edit Tax Rate";
                    addeditAduana = await AduanasService.Aduanas_GetOne(SelectedAduanaId);
                    await this.DialogAddEditAduana.Show();
                }

            }
        }


        public async Task RowSelectHandler(RowSelectEventArgs<Aduanas> args) {
            SelectedAduanaId = args.Data.AduanasID;
        }

        protected async Task AduanaSave() {
            if (addeditAduana.AduanasID == 0) {
                int Success = await AduanasService.AduanasInsert(addeditAduana.NombreAduana, addeditAduana.AbreviacionAduana);
                if (Success != 0) {
                    //Tax Rate already exists
                    WarningHeaderMessage = "Warning!";
                    WarningContentMessage = "This Tax Description already exists; it cannot be added again.";
                    Warning.OpenDialog();
                    // Data is left in the dialog so the user can see the problem.
                }
                else {
                    // Clears the dialog and is ready for another entry
                    // User must specifically close or cancel the dialog
                    addeditAduana = new Aduanas();
                }
            }
            else {
                // Item is being edited
                int Success = await AduanasService.AduanasUpdate(addeditAduana.NombreAduana, addeditAduana.AbreviacionAduana, SelectedAduanaId);
                if (Success != 0) {
                    //Tax Rate already exists
                    WarningHeaderMessage = "Warning!";
                    WarningContentMessage = "This Aduana already exists; it cannot be added again.";
                    Warning.OpenDialog();
                }
                else {
                    await this.DialogAddEditAduana.Hide();
                    this.StateHasChanged();
                    addeditAduana = new Aduanas();
                    SelectedAduanaId = 0;
                }
            }

            //Always refresh datagrid
            aduana = await AduanasService.AduanasList();
            StateHasChanged();
            txtAduana.FocusAsync();
        }



        private async Task CloseDialog() {
            await this.DialogAddEditAduana.HideAsync();
        }
    }
}
