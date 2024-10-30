using Counter.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Counter.Web.UI
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

		//private async Task<string> GetCountBySerialNumber(string serialNumber)
		//{
		//	using (HttpClient client = new HttpClient())
		//	{
		//		client.BaseAddress = new Uri("https://localhost:5001/"); // Update the base URL if needed
		//		var response = await client.GetAsync($"api/Count/LastCount?seriNumarasi={serialNumber}");

		//		if (response.IsSuccessStatusCode)
		//		{
		//			var countValue = await response.Content.ReadAsStringAsync();
		//			lblMessage.Text = "Query successful!";
		//			return countValue;  // Return the count value
		//		}
		//		else
		//		{
		//			lblMessage.Text = "Error: No count found for this serial number.";
		//			return "0";  // Return 0 if there's an error
		//		}
		//	}
		//}

		//protected async void queryCount()
		//{
		//	string serialNumber = txtSerialNumber.Text.Trim();
		//	if (serialNumber.Length != 8)
		//	{
		//		lblMessage.Text = "Serial number must be 8 characters long.";
		//		return;
		//	}

		//	// Call the API
		//	string result = await GetCountBySerialNumber(serialNumber);
		//	lblCounter.Text = result;
		//}

	}
}