<%@ Page Title="Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Counter.Web.UI.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="container d-flex align-items-center justify-content-center min-vh-100">
            <div class="text-center bg-white p-5 rounded shadow w-100" style="max-width: 600px;">
                <h1 class="display-4 mb-4">Rapor Yönetimi</h1>
                
                <div class="d-flex justify-content-center mb-3">
                    <input type="text" id="txtSerialNumber" class="form-control" placeholder="Seri Numarası Girin" maxlength="8">
                </div>
                <div class="d-flex justify-content-center mb-3">
                    <div id="loader" class="spinner-border text-primary" style="display:none;" role="status">
                        <span class="visually-hidden">Yükleniyor...</span>
                    </div>
                </div>
                <button type="button" class="btn btn-success btn-lg mb-3" id="btnRequestReport" onclick="requestReport()">Rapor Talep Et</button>
                <div id="lblMessage" class="text-danger"></div>
                <h2 class="mt-4">Mevcut Raporlar</h2>
                <div id="reportList" class="mb-4"></div>
                <div id="reportDetails" class="mt-4" style="display:none;">
                    <h3>Rapor Detayları</h3>
                    <span id="lblReportDetails"></span>
                </div>      
            </div>
        </div>
    </main>

    <script>
        async function requestReport() {
            const serialNumber = document.getElementById("txtSerialNumber").value.trim();

            if (serialNumber.length !== 8) {
                document.getElementById("lblMessage").innerText = "Seri numarası 8 karakter olmalıdır.";
                return;
            }

            document.getElementById("loader").style.display = "block";

            try {
                const response = await fetch(`https://localhost:5179/api/Report/Request?serialNumber=${serialNumber}`, { method: 'POST' });

                if (response.ok) {
                    loadReports();
                    document.getElementById("lblMessage").innerText = "Rapor talebi başarıyla oluşturuldu.";
                } else {
                    document.getElementById("lblMessage").innerText = "Rapor talep edilirken bir hata oluştu.";
                }
            } catch (error) {
                document.getElementById("lblMessage").innerText = "API ile bağlantı kurulurken bir hata oluştu.";
            } finally {
                document.getElementById("loader").style.display = "none";
            }
        }

        async function loadReports() {
            const response = await fetch('https://localhost:5179/api/Report/Reports');
            const reports = await response.json();
            const reportList = document.getElementById("reportList");
            reportList.innerHTML = '';

            reports.forEach(report => {
                const reportItem = document.createElement('div');
                reportItem.innerHTML = `
                    <div class="card my-2">
                        <div class="card-body">
                            <h5 class="card-title">Seri Numarası: ${report.Icerik.SeriNumarasi}</h5>
                            <button class="btn btn-info" onclick="viewReport('${report.Icerik.SeriNumarasi}')">Görüntüle</button>
                        </div>
                    </div>
                `;
                reportList.appendChild(reportItem);
            });
        }

        async function viewReport(serialNumber) {
            const response = await fetch(`https://localhost:5179/api/Report/Reports?serialNumber=${serialNumber}`);
            const report = await response.json();

            document.getElementById("lblReportDetails").innerHTML = `
                <strong>Talep Tarihi:</strong> ${new Date(report.TalepTarihi).toLocaleString()}<br>
                <strong>Durum:</strong> ${report.Durum}<br>
                <strong>Ölçüm Zamanı:</strong> ${new Date(report.Icerik.OlcumZamani).toLocaleString()}<br>
                <strong>Son Endeks:</strong> ${report.Icerik.SonEndeks}<br>
                <strong>Voltaj:</strong> ${report.Icerik.Voltaj}<br>
                <strong>Akim:</strong> ${report.Icerik.Akim}<br>
            `;

            document.getElementById("reportDetails").style.display = "block";
        }

        window.onload = loadReports;
    </script>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
