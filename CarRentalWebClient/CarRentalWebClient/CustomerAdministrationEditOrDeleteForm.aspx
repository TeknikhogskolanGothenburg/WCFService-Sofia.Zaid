<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerAdministrationEditOrDeleteForm.aspx.cs" Inherits="CarRentalWebClient.CustomerAdministrationEditOrDeleteForm" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Edit or delete existing customer</title>
</head>
<body>
    <h1>Edit or delete existing customer</h1>
    <p style="font:italic 1em Arial">Put in an id in the text field below to find the customer
        you either want to edit or delete from the system.
    </p>
    <form id="editCustomerForm" runat="server">
        <table style="font-family: Arial">
            <tr>
                <td><label>Customer ID</label>
                <input type="text" id="txtId" runat="server" /></td>
                <td><asp:Button ID="btnGetCustomer" runat="server" OnClick="btnGetCustomer_Click" Text="Find existing Customer" />
                    
                </td>
                <td>
                    <asp:Label style="margin-left:2em" ID="lblCustomerSaved" runat="server" Text=""></asp:Label>
                </td>
            </tr>
              <tr>
                <td>
                    <label>First Name</label>
                </td>
                <td>
                    <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Last Name</label>
                </td>
                <td>
                    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Phone number</label>
                </td>
                <td>
                    <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Email address</label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmailAddress" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID= "btnSaveEditedCustomer" runat="server" Text="Save edited customer info" OnClick="btnSaveEditedCustomer_Click" />
                </td>
                <td>
                    <%--<button runat="server">Delete customer</button>--%>
                    <asp:Button ID= "btnDeleteCustomer" onClick="btnDeleteCustomer_Click" runat="server" Text="Delete this customer" />
                </td>
            </tr>
          <%--  <tr>
                <td><label style="font-size:0.8em">Click on this button to get to the view where you can edit a customer</label>
                    <asp:Button ID="btnGoToEditView" runat="server" Text="Go to edit mode" /></td>

            </tr>--%>
            
            </table>
    </form>
</body>
</html>

