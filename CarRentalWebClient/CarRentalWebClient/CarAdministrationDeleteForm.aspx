<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarAdministrationDeleteForm.aspx.cs" Inherits="CarRentalWebClient.CarAdministrationDeleteForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delete existing car</title>
</head>
<body>
    <h1>Delete existing car</h1>
    <p style="font:italic 1em Arial">Put in an id in the text field below to find the car
        you want to delete from the system.
    </p>
    <form id="deleteCarForm" runat="server">
        <table style="font-family: Arial">
            <tr>
                <td><label>Car ID</label>
                <input type="text" id="txtId" runat="server" /></td>
                <td><asp:Button ID="btnGetCar" runat="server" OnClick="btnGetCar_Click" Text="Find existing Car" />
                </td>
                <td>
                    <asp:Label style= "margin-left: 2em" ID="lblMessageDelete" runat="server" Text=""></asp:Label>
                </td>
            </tr>
              <tr>
                <td>
                    <label>Brand</label>
                </td>
                <td>
                    <input type="text" id="txtBrand" runat="server"/>
                </td>
                  <td>
                      <asp:Label ID="lblLicenseKey" runat="server" Text="Put in license key"></asp:Label>
                  </td>
            </tr>
            <tr>
                <td>
                    <label>Model</label>
                </td>
                <td>
                    <input type="text" id="txtModel" runat="server" readonly="readonly"/>
                </td>
                <td>
                    <asp:TextBox ID="txtLicenseKey" runat="server"></asp:TextBox>
                  
                </td>
            </tr>
            <tr>
                <td>
                    <label>Registration number</label>
                </td>
                <td>
                 <input type="text" id="txtRegNr" runat="server" readonly="readonly"/>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Manufacturing year</label>
                </td>
                <td>
                     <input type="text" id="txtManYear" runat="server" readonly="readonly"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID= "btnDeleteCar" onClick="btnDeleteCar_Click" runat="server" Text="Delete this car" />
                </td>
            </tr>
            
            </table>
    </form>
</body>
</html>
