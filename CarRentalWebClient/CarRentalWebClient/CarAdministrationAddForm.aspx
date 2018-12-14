<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarAdministrationAddForm.aspx.cs" Inherits="CarRentalWebClient.AddCarForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1 style="font-family: Arial">Car Administration</h1>
    <form id="carAdministrationForm" runat="server">
    <table id="CarAdministrationForm" style="font: bold 1em arial">
        <tr>
            <td>
                <label>Brand</label></td>
            <td>
                <input id="txtBrand" type="text" runat="server"/></td>
            <td>
                <asp:Label style="margin-left:2em" ID="lblNotableToSaveCar" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <label>Model</label></td>
            <td>
                <input id="txtModel" type="text" runat="server" required/></td>
        </tr>
        <tr>
            <td>Registration Number</td>
            <td>
                <input id="txtRegNr" type="text" runat="server" required/></td>
        </tr>
        <tr>
            <td>ManufacturingYear</td>
            <td>
                <input id="txtManYear" type="text" runat="server" required/></td>
        </tr>
        <tr>
            <td><asp:Button style="padding: 1em, 1em" ID="btnSaveNewCar" runat="server" Text="Save new Car" OnClick="btnSaveNewCar_Click" Width="164px" /></td>
        </tr>
        <tr style="padding:1em 1em">
            <td><label ID="lblCarSaved" runat="server" style="font-weight:bold"></label></td>
        </tr>
    </table>
        </form>
</body>
</html>
