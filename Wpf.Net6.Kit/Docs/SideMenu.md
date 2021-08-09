# **SideMenu**



### Propriedades para navegação

Propriedade | Descrição
----- | -----
**`Pages`** | Obtem ou define um dicionário com as páginas (qualquer FrameWorkElement) para navegação usado pelo Frame interno. 
**`BackgroundPage`** | Obtem ou define um FrameworkElement que representa a página de fundo da aplicação, que será exibida se nenhum item estiver selecionado ou se a propriedade Pages não contiver nenhuma entrada.
**`NavigationUIVisibility`** | Obtem ou define um valor que representa a visibilidade da barra de navegação do frame.

<br/>

A propriedade **`Pages`** trabalha em conjunto com o controle SideMenuItem. 
Defina a propriedade **`PageTypeName`** em SideMenuItem com o nome da classe para qual deseja navegar.
Em **`SideMenu.Pages`** inclua no dicionário uma entrada em que a chave seja o nome da classe e o valor seja uma 
instância desta classe.

Veja o exemplo:

~~~~
<controls:SideMenu x:Name="sideMenu" >
    <controls:SideMenu.Resources>
        <Style TargetType="controls:SideMenuItem">
            <Setter Property="SymbolForeground" Value="DodgerBlue" />
            <Setter Property="SymbolFontSize" Value="26" />
            <Setter Property="SelectionIndicatorMargin" Value="0,0,10,0" />
            <Setter Property="Height" Value="48" />
        </Style>
    </controls:SideMenu.Resources>
    <controls:SideMenuItem Content="Page1" PageTypeName="Page1"/>
    <controls:SideMenuItem Content="Page2" PageTypeName="Page2" Symbol="&#xE136;" SymbolForeground="MediumSlateBlue"/>
    <controls:SideMenuItem Content="UserControl1" PageTypeName="UserControl1" Symbol="&#xE163;" SymbolForeground="OrangeRed"/>
    <controls:SideMenuItem Content="UserControl2" PageTypeName="UserControl2" Symbol="&#xE726;" SymbolForeground="Yellow"/>
</controls:SideMenu>
~~~~

In code-behind:

~~~~c#
public MainWindow()
{
    InitializeComponent();
    sideMenu.Pages.Clear();
    sideMenu.Pages.Add(nameof(Page1), new Page1());
    sideMenu.Pages.Add(nameof(Page2), new Page2());
    sideMenu.Pages.Add(nameof(UserControl1), new UserControl1());
    sideMenu.Pages.Add(nameof(UserControl2), new UserControl2());
}
~~~~

Se nenhum item estiver selecionado ou se a propriedade Pages não contiver nenhuma entrada esta mensagem padrão será exibida.

![SideMenu sample](/Wpf.Net6.Kit/Docs/Assets/SideMenu/SideMenu_NoItemSelected_message.png?raw=true)

A navegação ocorre selecionado-se um item.

![SideMenu sample](/Wpf.Net6.Kit/Docs/Assets/SideMenu/SideMenu_ItemSelected.png?raw=true)

<br/>

### Propriedades do hamburger menu

Propriedade | Descrição
----- | -----
**`MenuMinWidth`** | Obtem ou define um valor que representa a parte sempre visível do painel esquerdo.
**`MenuMaxWidth`** | Obtem ou define um valor que representa a largura máxima do painel esquerdo.
**`MenuWidth`** | Obtem ou define um valor que representa a largura do painel esquerdo.
**`HamburgerMenuForeground`** | Obtem ou define um pincel que descreve a cor do primeiro plano do botão que abre/fecha o painel esquerdo.
**`GridSplitterBackground`** | Obtem ou define um pincel que descreve a cor do GridSplitter.
**`GridSplitterWidth`** | Obtem ou define um valor que representa a espessura da GridSplitter.
**`GridSplitterIsEnabled`** | Obtem ou define um valor que representa se o GridSplitter está habilitado.

<br/>

### Propriedade CustomArea


![SideMenu sample](/Wpf.Net6.Kit/Docs/Assets/SideMenu/SideMenu.png?raw=true)

