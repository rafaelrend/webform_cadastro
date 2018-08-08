<%@ control language="C#" autoeventwireup="true" inherits="ucFiltroBasico, App_Web_cvdgmgz4" %>
<%@ Reference Control = "UcFaseDatas.ascx" %>
 
 <table border="0">
	                 <tbody>
	                 <tr>
	                 <td colspan="3" align="center">
                         <asp:RadioButtonList ID="g_glob_tipo_filtro" runat="server" AutoPostBack="True" 
                             CssClass="rd_glob_filtroavancado" 
                             onselectedindexchanged="g_glob_tipo_filtro_SelectedIndexChanged" 
                             RepeatDirection="Horizontal">
                           <asp:ListItem Text="Filtro Rápido" Selected="True" Value="R"></asp:ListItem>
                           <asp:ListItem Text="Filtro Avançado" Value="A"></asp:ListItem>
                         </asp:RadioButtonList>
                         <div style="display:none">
                                <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
                         </div>
                         </td>
	                 </tr>
	                 
	                 <tr><td >
					   <label class="label_filtro">Filtrar</label></td>
					     <td>
                            <asp:DropDownList ID="dd_glob_cadastro" runat="server">
                            </asp:DropDownList>
						  </td>
						  <td >
			
                         <asp:TextBox ID="txt_glob_search" CssClass="search" runat="server" Width="120px"></asp:TextBox>
					
					</td>
					
					
					<td style="width: 90px;" valign="top">
                        <asp:Button ID="bt_glob_search" CssClass="submit" runat="server" Text="Filtrar"  />
                        &nbsp;
                        <asp:HiddenField ID="hd_tabela" runat="server" />
                        <asp:HiddenField ID="hd_caminho_filtros" runat="server" />
					</td>
					<td valign="top">&nbsp;<asp:ImageButton ID="imgMostraFiltroAvancado"  runat="server" 
                            ImageUrl="~/images/show_icon.png" ToolTip="Mostra filtro avançado" />
                            
                            
                            
                            
					</td>
					<td valign="top">&nbsp;
                        <asp:ImageButton ID="imgExportaExcel"  runat="server" 
                            ImageUrl="~/images/excel_btn2.jpg" ToolTip="Exportar resultado para excel" />
					</td>
					</tr>
					</tbody>
	</table>


<div class="div_filtro_avancado" runat="server" id="div_filtro_avancado" style="display:none">
<table style="width: 99%">
<tr>
   <th >
   <center>
    <label>Filtro Avançado</label>
 </center>
   </th>
<th style="width: 20px">
    <asp:ImageButton ID="ImageButton1" runat="server" 
        ImageUrl="~/images/hide_icon.png" ToolTip="Esconde filtro avançado"  />
       <!-- onclick="ImageButton1_Click" onclick="ImageButton2_Click" 
        -->
        
                </th>
<th style="width: 20px">
    <asp:ImageButton ID="ImageButton2" runat="server" 
        ImageUrl="~/images/page_white.png" ToolTip="Limpar filtro avançado"   />
                </th>
</tr>

</table>

 <table id="g_tbFiltro" runat="server" class="tb_filtro" style="width: 99%; " border="0">

 </table>
<div id="cmpAdicional" runat="server"></div>
</div>
