<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="EnvioEmail.aspx.cs" Inherits="pokedex_web_nivel3.EnvioEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-6 align-self-center">
                <label class="form-label">Nombre</label>
                <asp:TextBox runat="server" class="form-control" ID="txtNombreEmail"/>
                <label class="form-label">Asunto</label>
                <asp:TextBox runat="server" class="form-control" ID="txtAsuntoEmail"/>
                <label class="form-label">Mensaje</label>
                <asp:TextBox runat="server" ID="txtMensajeEmail" CssClass="form-control" TextMode="MultiLine"/>
                <asp:Button Text="Enviar" ID="btnEnviarEmail" CssClass="btn btn-primary" OnClick="btnEnviarEmail_Click" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
