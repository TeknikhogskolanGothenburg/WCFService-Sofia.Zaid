<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookingAdministrationDeleteForm.aspx.cs" Inherits="CarRentalWebClient.BookingAdministrationEditOrDeleteForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
 <h1>Delete existing booking</h1>
    <p style="font:italic 1em Arial">Put in a booking id in the text field below to find the customer booking
        you want to delete from the system.
    </p>
    <form id="deleteBookingForm" runat="server">
        <table style="font-family: Arial">
            <tr>
                <td><label>Booking ID</label> </td>
                <td>
                <input type="text" ID="txtbookingId" runat="server" /></td>
                <td><asp:Button ID="btnGetBookingById" runat="server" Text="Find Booking by booking id" OnClick="btnGetBookingById_Click" /></td>
            </tr>
             <tr>
                <td>
                    <label>Customer email adress</label>
                </td>
                <td>
                    <input type="text" id="txtEmailAddress" runat="server"/>
                </td>
                 <td>
                     <asp:Button ID="btnGetBookingByCustomerEmail" runat="server" Text="Find Booking by customer email" OnClick="btnGetBookingByCustomerEmail_Click" />
                 </td>
            </tr>
              <tr>
                <td>
                    <label>Start time</label>
                </td>
                <td>
                    <input type="text" id="txtStartTime" readonly="readonly" runat="server"/>
                </td>
            </tr>
            <tr>
                <td>
                    <label>End time</label>
                </td>
                <td>
                    <input type="text" id="txtEndTime" readonly="readonly" runat="server"/>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Car Brand</label>
                </td>
                <td>
                 <input type="text" id="txtCarBrand" readonly="readonly" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Car Model</label>
                </td>
                <td>
                 <input type="text" id="txtCarModel" readonly="readonly" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>Manufacturing year</label>
                </td>
                <td>
                     <input type="text" id="txtManYear" readonly="readonly" runat="server"/>
                </td>
                  <td>
                      <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                  </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID= "btnDeleteBooking" runat="server" Text="Delete this booking permanently" OnClick="btnDeleteBooking_Click" />
                </td>
            </tr>            
            </table>
    </form>
</body>
</html>
