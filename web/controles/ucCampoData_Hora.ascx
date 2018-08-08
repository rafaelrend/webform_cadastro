<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCampoData_Hora.ascx.cs" Inherits="controles_ucCampoData_Hora" %>

<table><tr><td>
<asp:TextBox
    ID="txtDtData" runat="server" CssClass="txtPadrao" MaxLength="10" Width="58px"></asp:TextBox>
   <ajaxToolkit:CalendarExtender ID="CalendarExtender1" 
runat="server" Format="dd/MM/yyyy"  TargetControlID="txtDtData" >
</ajaxToolkit:CalendarExtender>

</td><td id="td_hora" runat="server"> 
 <asp:TextBox  runat="server" ID="txtHora"  CssClass="txtPadrao" MaxLength="5" Width="30px"></asp:TextBox>
   

</td></tr></table>