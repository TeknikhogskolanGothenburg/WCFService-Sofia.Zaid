<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PickUpAndDropOffCar.aspx.cs" Inherits="CarRentalWebClient.PickUpAndDropOffCar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 30px;
        }
    </style>
</head>
<body>
    <form id="CarLeasingPickUpDropOffForm" runat="server">
        <h1 style="font-family: Arial">Leasing administration</h1>
        <table id="pickUpDropOffCar" style="font: bold 1em arial">
            <tr>
                <td class="auto-style1">
                <label>Put in booking id to find booking for the car to pick up or drop off:</label></td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtBookingId" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style1">
                    <asp:Button ID="btnGetBooking" runat="server" Text="Find booking" OnClick="btnGetBooking_Click" />
                </td>
                <td class="auto-style1">
                    <asp:Label style="margin-left: 3em" ID="lblMessage" runat="server" Text=""></asp:Label>
                </td>
            </tr>
             <tr>
                <td>
                    <label>Booking starttime:</label>
                </td>
                <td>
                     <asp:TextBox ID="txtStartTime" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    <label>Booking endtime:</label>
                </td>
                <td>
                     <asp:TextBox ID="txtEndTime" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Car id:</label>
                </td>
                <td>
                     <asp:TextBox ID="txtCarId" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    <label>Car Brand:</label>
                </td>
                <td>
                     <asp:TextBox ID="txtCarBrand" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    <label>Car Model:</label>
                </td>
                <td>
                     <asp:TextBox ID="txtCarModel" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
       <%--         <td>
                    <asp:Button ID="btnPickUp" runat="server" Text="Lease car (customer pick up)" OnClick="btnPickUp_Click" />
                </td>--%>
                <td>
                    <asp:Button ID="btnDropOff" runat="server" Text="End car lease (customer returns car)" OnClick="btnDropOff_Click" />
                &nbsp;</td>
                 <td>
                    <asp:Button ID="btnPickUp" runat="server" Text="Start car lease (customer picks upp car)" OnClick="btnPickUp_Click1" />
                &nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
