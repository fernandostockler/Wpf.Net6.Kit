# Add your introductions here!

# **CustomWindow**
� uma janela que permite a personaliza��o da �rea n�o-cliente, possu� um modo kiosk e tem um mecanismo para exibi��o de conte�do modal. A seguir as principais funcionalidades da classe CustomWindow:<br/><br/>

## �rea n�o-cliente personaliz�vel 
![CustomWindow sample](/Wpf.Net6.Kit/Docs/Assets/CustomWindow/CustomWindow_Mid_White.png?raw=true)
![CustomWindow sample](/Wpf.Net6.Kit/Docs/Assets/CustomWindow/CustomWindow_Mid_White_Left_Right_Blue.png?raw=true)
![CustomWindow sample](/Wpf.Net6.Kit/Docs/Assets/CustomWindow/CustomWindow_Mid_Blue_Gray_Left_Right.png?raw=true)
![CustomWindow sample](/Wpf.Net6.Kit/Docs/Assets/CustomWindow/CustomWindow_sample1_xaml.png?raw=true)

Est�o dispon�veis 4 regi�es na barra do t�tulo que s�o personaliz�veis atrav�s das seguintes propriedades:

Propriedades | Descri��o | Local na barra de t�tulo
-----   | ----- | ----
TitleBarIcon | Pode ser qualquer classe que derive de FrameworkElement. | A esquerda no in�cio da barra de t�tulo.
TitleBarLeftArea | Pode ser qualquer classe que derive de FrameworkElement. | A direita do TitleBarIcon.
Title and TitleDataTemplate | Propriedades originais da classe Window. | A direita de TitleBarLeftArea.
TitleBarRightArea | Pode ser qualquer classe que derive de FrameworkElement. | A direita do t�tulo e antes dos bot�es da janela.

<br/>

><br/>Nota: Para que os controles definidos para essas propriedades possam ser utilizados deve-se acrescentar o atributo WindowChrome.IsHitTestVisibleInChrome="True".<br/><br/>


### Outras propriedades que afetam a �rea n�o-cliente da janela:

Propriedade | Descri��o
---- | ----
TitleBarHeight | Gets or sets the height of the title bar.
TitleBarForeground | Gets or sets a brush that describes the foreground color of the window's title bar. Automatically calculated by OnTitleBarBackgroundChanged(d, e) when TitleBarForegroundIsAutomated is true.
TitleBarForegroundIsAutomated | Gets or sets a Boolean value representing whether or not the title bar foreground will automatically adapt to a new background.
TitleBarBackground | Gets or sets the brush that describes the background of the window's title bar.
TitleBarBorderThickness | Gets or sets the thickness of the border of the window's title bar.
TitleBarBorderBrush | Gets or sets the brush that describes the border of the window's title bar.

<br/>

## Modo Kiosk
Usado em aplica��es de ponto-de-venda, caixa eletr�nico, sinaliza��o digital ou quiosques. Lan�a a aplica��o em tela inteira e previne o acesso do usu�rio a outros systemas. Para sair do modo kiosk basta uma combina��o de teclas que pode ser personalizada. 

### Propriedades que afetam o modo kiosk:

Propriedade | Descri��o
---- | ----
KioskMode | Gets or sets a Boolean value representing whether KioskMode is turned on/off.
KioskModeExitKeyGesture | Gets or sets a key combination of type KioskExitKeyGesture that turns off kiosk mode.

<br/>

## Conte�do modal
Para exibir um conte�do, que pode ser qualquer classe derivada de FrameworkElement, encobrindo a janela com uma camada personaliz�vel impossibilitando qualquer intera��o com a aplica��o principal enquanto estiver no modo modal. Somente o conte�do modal estar� dispon�vel para o usu�rio at� que ele saia.

### Propriedades relacionadas:

Propriedade | Descri��o
---- | ----
OverlayBackground | Gets or sets a brush that represents the background of the layer covering the window.
ShowCustomDialog | Gets or sets the visibility of the layer that covers the window.
CustomDialog | Gets or sets a FrameworkElement that represents an interactive modal control that will only be visible if the ShowCustomDialog property is true.

![CustomWindow sample](/Wpf.Net6.Kit/Docs/Assets/CustomWindow/CustomWindow_Small_White_Dialog_Green.png?raw=true)
![CustomWindow sample](/Wpf.Net6.Kit/Docs/Assets/CustomWindow/CustomWindow_Mid_White_Dialog_Blue_Red.png?raw=true)
