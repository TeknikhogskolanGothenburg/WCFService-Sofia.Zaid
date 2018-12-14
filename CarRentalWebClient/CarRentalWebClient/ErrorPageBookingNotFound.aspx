<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPageBookingNotFound.aspx.cs" Inherits="CarRentalWebClient.ErrorPageBookingNotFound" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblErrorMsg" runat="server" Text="Something went wrong, please check input booking id, the booking might already be deleted."></asp:Label>
        </div>
    </form>
</body>
</html>
