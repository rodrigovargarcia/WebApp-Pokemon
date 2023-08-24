<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="pokedex_web_nivel3.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <h1>Creá tu perfil Trainee</h1>
    </div>
    <div class="row">
        <div class="col-4">
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" id="txtEmail" CssClass="form-control"/>
                <label class="form-label">Contraseña</label>
                <asp:TextBox runat="server" id="txtPass" CssClass="form-control" Type="password"/>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-4">
            <asp:Button Text="Registrarse" CssClass="btn btn-primary" ID="btnRegistrarse" OnClick="btnRegistrarse_Click" runat="server" />
            <asp:Button Text="Cancelar" CssClass="btn" ID="btnCancelarRegistro" OnClick="btnCancelarRegistro_Click" runat="server" />
        </div>
    </div>
</asp:Content>
