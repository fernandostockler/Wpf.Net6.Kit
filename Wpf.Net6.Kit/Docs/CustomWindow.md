# **CustomWindow**
 É uma janela que permite a personalização da área não-cliente, possuí um modo kiosk e tem um mecanismo para exibição de conteúdo modal. A seguir as principais funcionalidades da classe CustomWindow:<br/><br/>

## Área não-cliente personalizável 

Estão disponíveis 4 regiões na barra do título que são personalizáveis através das seguintes propriedades:

Propriedades | Descrição | Local na barra de título
-----   | ----- | ----
TitleBarIcon | Pode ser qualquer classe que derive de FrameworkElement. | A esquerda no início da barra de título.
TitleBarLeftArea | Pode ser qualquer classe que derive de FrameworkElement. | A direita do TitleBarIcon.
Title and TitleDataTemplate | Propriedades originais da classe Window. | A direita de TitleBarLeftArea.
TitleBarRightArea | Pode ser qualquer classe que derive de FrameworkElement. | A direita do título e antes dos botões da janela.

<br/>

><br/>Nota: Para que os controles definidos para essas propriedades possam ser utilizados deve-se acrescentar o atributo WindowChrome.IsHitTestVisibleInChrome="True".<br/><br/>


### Outras propriedades que afetam a área não-cliente da janela:



Propriedade | Descrição
---- | ----
TitleBarHeight | Gets or sets the height of the title bar.
TitleBarForeground | Gets or sets a brush that describes the foreground color of the window's title bar. Automatically calculated by OnTitleBarBackgroundChanged(d, e) when TitleBarForegroundIsAutomated is true.
TitleBarForegroundIsAutomated | Gets or sets a Boolean value representing whether or not the title bar foreground will automatically adapt to a new background.
TitleBarBackground | Gets or sets the brush that describes the background of the window's title bar.
TitleBarBorderThickness | Gets or sets the thickness of the border of the window's title bar.
TitleBarBorderBrush | Gets or sets the brush that describes the border of the window's title bar.
<br/>

## Modo Kiosk
Usado em aplicações de ponto-de-venda, caixa eletrônico, sinalização digital ou quiosques. Lança a aplicação em tela inteira e previne o acesso do usuário a outros systemas. Para sair do modo kiosk basta uma combinação de teclas que pode ser personalizada. 

### Propriedades que afetam o modo kiosk:

Propriedade | Descrição
---- | ----
KioskMode | Gets or sets a Boolean value representing whether KioskMode is turned on/off.
KioskModeExitKeyGesture | Gets or sets a key combination of type KioskExitKeyGesture that turns off kiosk mode.

<br/>

## Conteúdo modal
Para exibir um conteúdo, que pode ser qualquer classe derivada de FrameworkElement, encobrindo a janela com uma camada personalizável impossibilitando qualquer interação com a aplicação principal enquanto estiver no modo modal. Somente o conteúdo modal estará disponível para o usuário até que ele saia.
     
### Propriedades relacionadas:

Propriedade | Descrição
---- | ----
OverlayBackground | Gets or sets a brush that represents the background of the layer covering the window.
ShowCustomDialog | Gets or sets the visibility of the layer that covers the window.
CustomDialog | Gets or sets a FrameworkElement that represents an interactive modal control that will only be visible if the ShowCustomDialog property is true.

![CustomWindow sample](/Docs/Assets/CustomWindow_Mid_White_Left_Right_Blue.png)