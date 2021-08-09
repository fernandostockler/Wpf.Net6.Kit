# **SideMenu**

### Propriedades para navegação

Propriedade | Descrição
----- | -----
**`Pages`** | Obtem ou define um dicionário com as páginas (qualquer FrameWorkElement) para navegação usado pelo Frame interno. 
**`BackgroundPage`** | Obtem ou define um FrameworkElement que representa a página de fundo da aplicação, que será exibida se nenhum item estiver selecionado ou se a propriedade Pages não contiver nenhuma entrada.
**`NavigationUIVisibility`** | Obtem ou define um valor que representa a visibilidade da barra de navegação do frame.

>A propriedade **`Pages`** trabalha em conjunto com o controle **`SideMenuItem`**. 
>Defina a propriedade **`PageTypeName`** em SideMenuItem com o nome da classe para qual deseja navegar.
>Em **`SideMenu.Pages`** inclua no dicionário uma entrada em que a chave seja o nome da classe e o valor seja uma 
>instância desta classe.

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
