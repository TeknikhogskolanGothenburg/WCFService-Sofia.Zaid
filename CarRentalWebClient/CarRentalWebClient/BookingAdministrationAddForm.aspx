<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookingAdministrationAddForm.aspx.cs" Inherits="CarRentalWebClient.BookingAdministrationAddForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Make a car reservation</title>
    <style type="text/css">
        .auto-style4 {
            margin-left: 23em;
            margin-right: 1.4em;
            margin-top: 0.4em;
        }

        .auto-style6 {
            width: 660px;
        }

        .auto-style7 {
            width: 660px;
            height: 30px;
        }
    </style>
</head>
<body>

    <form id="bookingAddForm" runat="server">

        <h1 style="font-family: Arial">Booking Administration, make a reservation</h1>
        <table id=" BookingAdministrationAddForm" style="font: bold 1em arial">
            <tr>
                <td class="auto-style6">
                    <label>Choose start date for your reservation:</label></td>
                <td>
                    <label>Choose end date for your reservation:</label>
                    <%--<textarea id="txtAreaBookingConfirmation" runat="server" rows="2" class="auto-style1"></textarea>--%>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">
                    <asp:Calendar ID="CalendarFrom" runat="server" OnSelectionChanged="CalendarFrom_SelectionChanged"></asp:Calendar>
                </td>
                <td>
                    <asp:Calendar ID="CalendarTo" runat="server" CssClass="auto-style13" Height="159px" Style="margin-left: 0px" Width="257px" OnSelectionChanged="CalendarTo_SelectionChanged"></asp:Calendar>
                </td>

            </tr>
            <tr>
                <td class="auto-style6">
                    <label id="lblDateFrom" runat="server">
                        <asp:Button Style="margin-bottom: 2em;" ID="searchForCars" runat="server" Text="Find available cars" Width="253px" OnClick="searchForCars_Click" Height="24px" CssClass="auto-style4" BackColor="#66CCFF" />

                    </label>

                </td>
                <td>
                    <label id="lblDateTo" runat="server"></label>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">
                    <label>List of cars available for chosen date span:</label></td>
            </tr>
            <tr>
                <td class="auto-style6">
                    <asp:ListBox ID="lstBoxAvailableCars" runat="server" Width="33.4em"></asp:ListBox>
                    <asp:Button ID="btnChooseCar" runat="server" Text="Make booking for selected car" OnClick="btnChooseCar_Click" Enabled="false" BackColor="#66CCFF" /><asp:Label ID="lblErrorNocarChosen" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <p style="font: bold 2em 'Times New Roman', Times, serif">Customer Booking confirmation</p>
                    <asp:ListBox ID="lstBoxConfirmation" runat="server" Width="440px" BackColor="PaleGoldenrod" Style="margin-left: 1em; margin-right: 1em; margin-top: -1em;"></asp:ListBox></td>
            </tr>
            <tr>
                <td class="auto-style6">
                    <label>
                        <br />
                        <br />
                        Enter customer details:</label>
            </tr>

            <tr style="margin-top: 4em">
                <td class="auto-style7">
                    <asp:TextBox ID="txtCustomerID" runat="server" Text="Enter Customer Id for booking" Width="223px"></asp:TextBox>
                    <asp:Button ID="btnFindCustomer" runat="server" Text="Find customer" BackColor="#66CCFF" OnClick="btnFindCustomer_Click" />
                </td>
                <td>
                    <asp:Label ID="lblErrorCustomer" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">
                    <label>Customer firstname:</label>
                    <asp:TextBox ID="txtFirstName" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">
                    <label>Customer lastname:</label>
                    <asp:TextBox ID="txtLastName" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">
                    <label>Customer phonenumber:</label>
                    <asp:TextBox ID="txtPhone" runat="server" ReadOnly="true"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td class="auto-style6">
                    <label>Customer email:</label>
                    <asp:TextBox ID="txtEmail" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
    </form>

</body>
</html>
