<%@ Page Title="Counter Query" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Counter.Web.UI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="container d-flex align-items-center justify-content-center min-vh-100">
            <div class="text-center bg-white p-5 rounded shadow w-100" style="max-width: 600px;">
                <h1 class="display-4 mb-4">Counter Application</h1>
                <div class="d-flex justify-content-center mb-3">
                    <input type="text" id="txtSerialNumber" class="form-control mb-3" placeholder="Enter Serial Number" maxlength="8">
                </div>
                <div class="d-flex justify-content-center mb-3">
                <div id="loader" class="spinner-border text-primary" style="display:none;" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div></div>
                <button type="button" class="btn btn-primary btn-lg mb-3" id="btnQuery" onclick="queryCount()">Query Results</button>
                <div class="mb-4">
                    <span id="lblCounter" class="display-1 text-primary"></span>
                </div>
                <div class="mt-4">
                    <span id="lblMessage" class="text-danger"></span>
                </div>
            </div>
        </div>
    </main>

    <script>
        async function queryCount() {
            const serialNumber = document.getElementById("txtSerialNumber").value.trim();

            if (serialNumber.length !== 8) {
                document.getElementById("lblMessage").innerText = "Serial number must be 8 characters long.";
                return;
            }

            document.getElementById("loader").style.display = "block";

            try {
                const response = await fetch(`https://localhost:5004/api/Count/LastCount?seriNumarasi=${serialNumber}`);

                if (response.ok) {
                    const data = await response.json();
                    document.getElementById("lblCounter").innerText = `
                        Serial Number: ${data.SeriNumarasi}
                        Last Index: ${data.SonEndeks}
                        Voltage: ${data.Voltaj}
                        Current: ${data.Akim}
                        Measurement Time: ${new Date(data.OlcumZamani).toLocaleString()}
                    `;
                    document.getElementById("lblMessage").innerText = "Query successful!";
                } else {
                    document.getElementById("lblMessage").innerText = "Error: No count found for this serial number.";
                    document.getElementById("lblCounter").innerText = "";
                }
            } catch (error) {
                document.getElementById("lblMessage").innerText = "Error connecting to API.";
                document.getElementById("lblCounter").innerText = "";
            } finally {
                document.getElementById("loader").style.display = "none";
            }
        }
    </script>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
