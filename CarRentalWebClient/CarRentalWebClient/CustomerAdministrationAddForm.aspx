<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerAdministrationAddForm.aspx.cs" Inherits="CarRentalWebClient.CustomerAdministrationAddForm" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Add customer</title>
</head>
<body>
    <form id="customerAddForm" runat="server">
    <h1 style="font-family:Arial">Customer Administration: Add new customer</h1>
        <table style="font-family: Arial">
              <tr>
                <td>
                    <label>First Name</label>
                </td>
                <td>
                    <input type="text" id="txtFirstName" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Last Name</label>
                </td>
                <td>
                    <input type="text" id="txtLastName" runat="server"/>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Phone number</label>
                </td>
                <td>
                 <input type="text" id="txtPhoneNumber" runat="server"/>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Email address</label>
                </td>
                <td>
                     <input type="text" id="txtEmailAddress" runat="server"/>
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 2em">
                    <asp:Button style="padding: 1em, 1em" ID="btnSaveCustomer" runat="server" Text="Save Customer" OnClick="btnSaveCustomer_Click" Width="164px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <Label ID="lblMessage" runat="server" Font-Bold="true"></Label>
                </td>
            </tr>
            </table>
        </form>
</body>
</html>