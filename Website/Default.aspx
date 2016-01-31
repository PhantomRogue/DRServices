<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Website.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button runat="server" ID="btnLogWindow" Text="Log Window" OnClick="btnLogWindow_Click" />
    <br />
    <asp:Button runat="server" ID="btnExpWindow" Text="EXP Window" OnClick="btnExpWindow_Click" />
    <br />
    <asp:Button runat="server" ID="btnThoughts" Text="Thought Window" OnClick="btnThoughts_Click" />
    <br />
    <br />
    <br />
    <br />
    <div runat="server" id="divOutput"></div>
</asp:Content>
